using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using GoGo_Tester.Properties;
using Timer = System.Timers.Timer;

namespace GoGo_Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Regex rxMatchIp =
            new Regex(@"((25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})",
                RegexOptions.Compiled);

        private readonly DataTable IpTable = new DataTable();

        public static Queue<string> WaitQueue = new Queue<string>();
        public static Queue<object> TestQueue = new Queue<object>();

        private readonly Timer StdTestTimer = new Timer();
        private string GaHost;
        private int GaPort;
        public static string ValidIps;

        private static Random random = new Random();
        public static int TestTimeout = 6000;
        public static int MaxThreads = 24;


        private bool GaIsTesting;
        private bool StdIsTesting;

        private Process proxyProcess;

        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = Resources.GoGo_logo;

            IpTable.Columns.Add(new DataColumn("addr", typeof(string))
            {
                Unique = true,
            });
            IpTable.Columns.Add(new DataColumn("std", typeof(string)));
            IpTable.Columns.Add(new DataColumn("proxy", typeof(string)));

            dgvIpData.DataSource = IpTable;
            dgvIpData.Columns[0].Width = 100;
            dgvIpData.Columns[0].HeaderText = "地址";
            dgvIpData.Columns[1].Width = 60;
            dgvIpData.Columns[1].HeaderText = "标准测试";
            dgvIpData.Columns[2].Width = 60;
            dgvIpData.Columns[2].HeaderText = "代理测试";

            /// Std
            ServicePointManager.ServerCertificateValidationCallback = (o, certificate, chain, errors) => true;

            StdTestTimer.Interval = 50;
            StdTestTimer.Elapsed += StdTestTimerElapsed;
        }

        private void StdTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            int waitCount = WaitQueue.Count;
            int testCount = TestQueue.Count;

            SetStdProgress(testCount, waitCount);

            if (waitCount > 0 && testCount < MaxThreads)
            {
                var thread = new Thread(() => StdTestProcess(WaitQueue.Dequeue(), TestTimeout, random));
                thread.Start();
            }
            else if (waitCount == 0 && testCount == 0)
            {
                StdIsTesting = false;
                StdTestTimer.Stop();
            }
        }

        private delegate void SetProgressHandler(int testCount, int waitCount);
        private void SetStdProgress(int testCount, int waitCount)
        {
            if (InvokeRequired)
            {
                Invoke(new SetProgressHandler(SetStdProgress), new object[] { testCount, waitCount });
            }
            else
            {
                pbProgress.Value = pbProgress.Maximum - waitCount - testCount;
                lProgress.Text = testCount + " / " + waitCount;
            }
        }

        private void StdTestProcess(string addr, int timeout, Random ran)
        {
            Monitor.Enter(TestQueue);
            TestQueue.Enqueue(0);
            Monitor.Exit(TestQueue);

            var sbd = new StringBuilder();

            for (int i = 0; i < 150; i++)
                sbd.Append(ran.Next().ToString("D10"));

            var url = "https://" + addr + "/?" + Convert.ToBase64String(Encoding.UTF8.GetBytes(sbd.ToString()));

            var reqt = (HttpWebRequest)WebRequest.Create(url);
            reqt.Timeout = timeout;
            reqt.Method = "HEAD";
            reqt.AllowAutoRedirect = false;
            reqt.KeepAlive = false;
            reqt.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            try
            {
                using (var respt = (HttpWebResponse)reqt.GetResponse())
                {
                    if (respt.Server == "gws")
                    {
                        var req = (HttpWebRequest)WebRequest.Create(url);
                        req.Timeout = timeout;
                        req.Method = "HEAD";
                        req.AllowAutoRedirect = false;
                        req.KeepAlive = false;
                        req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                        var stopwatch = new Stopwatch();
                        stopwatch.Start();

                        using (var resp = (HttpWebResponse)req.GetResponse())
                        {
                            SetStdTestResult(addr, "_OK " + stopwatch.ElapsedMilliseconds.ToString("D4"));
                            resp.Close();
                        }
                        stopwatch.Stop();
                    }
                    else
                    {
                        SetStdTestResult(addr, "Invalid");
                    }

                    respt.Close();
                }
            }
            catch (Exception ex)
            {
                SetStdTestResult(addr, "Invalid");
            }

            Monitor.Enter(TestQueue);
            TestQueue.Dequeue();
            Monitor.Exit(TestQueue);
        }

        private delegate void SetResultHandler(string addr, string result);
        private void SetStdTestResult(string addr, string result)
        {
            if (InvokeRequired)
            {
                Invoke(new SetResultHandler(SetStdTestResult), new object[] { addr, result });
            }
            else
            {
                var rows = SelectByIP(addr);
                if (rows.Length > 0)
                    rows[0][1] = result;
            }
        }
        public void SetGaTestResult(string addr, string result)
        {
            if (InvokeRequired)
            {
                Invoke(new SetResultHandler(SetGaTestResult), new object[] { addr, result });
            }
            else
            {
                var rows = SelectByIP(addr);
                if (rows.Length > 0)
                    rows[0][2] = result;
            }
        }

        private void ImportIp(string addr)
        {
            try
            {
                var row = IpTable.NewRow();
                row[0] = addr;
                row[1] = "n/a";
                row[2] = "n/a";
                IpTable.Rows.Add(row);
            }
            catch (Exception) { }
        }

        private void RemoveIp(string addr)
        {
            var row = SelectByIP(addr);
            if (row.Length > 0)
            {
                IpTable.Rows.Remove(row[0]);
            }
        }

        private DataRow[] SelectByExpr(string expr, string order = null)
        {
            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByExpr(expr, order)));
            }

            return IpTable.Select(expr, order);
        }
        private DataRow[] SelectByIP(string addr, string order = null)
        {

            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByIP(addr, order)));
            }

            return IpTable.Select(string.Format("addr = '{0}'", addr), order);
        }

        private DataRow[] SelectNa(string key, string order = null)
        {
            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByIP(key, order)));
            }

            return IpTable.Select(string.Format("{0} = 'n/a'", key), order);
        }

        private void Tip_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as Control;

            lTip.Text = control.Tag.ToString();
        }

        private void bAddIpRange_Click(object sender, EventArgs e)
        {
            var str = tbIpRange.Text.Trim();
            tbIpRange.ResetText();
            if (str == "")
            {
                return;
            }

            var ranges = str.Split(@"`~!?@#$%^&*()=+\|/,<>;:'，。；：“”‘’？、".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var range in ranges)
            {
                var cope = range.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                if (cope.Length != 4)
                    continue;

                var start = new int[4];
                var end = new int[4];
                var copeValid = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (cope[i].Contains("-"))
                    {
                        var se = cope[i].Split('-');
                        if (se.Length != 2)
                            break;

                        try
                        {
                            start[i] = Convert.ToInt32(se[0].Trim());
                            end[i] = Convert.ToInt32(se[1].Trim());
                        }
                        catch (Exception) { break; }

                    }
                    else
                    {
                        try
                        {
                            start[i] = Convert.ToInt32(cope[i].Trim());
                            end[i] = Convert.ToInt32(cope[i].Trim());
                        }
                        catch (Exception) { break; }

                    }
                    start[i] = start[i] > 255 ? 255 : start[i];
                    end[i] = end[i] > 255 ? 255 : end[i];

                    copeValid++;
                }

                if (copeValid != 4)
                    continue;

                for (int a = start[0]; start[0] <= a && a <= end[0]; a++)
                {
                    for (int b = start[1]; start[1] <= b && b <= end[1]; b++)
                    {
                        for (int c = start[2]; start[2] <= c && c <= end[2]; c++)
                        {
                            for (int d = start[3]; start[3] <= d && d <= end[3]; d++)
                            {
                                ImportIp(a + "." + b + "." + c + "." + d);
                            }
                        }
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoveAllIps();
            while (TestQueue.Count > 0)
            {
                Application.DoEvents();
            }
        }

        private void mImportIpsInClipbord_Click(object sender, EventArgs e)
        {
            var str = "";
            try
            {
                str = Clipboard.GetText();
            }
            catch (Exception)
            {
                MessageBox.Show("操作剪切板可能失败！再试一次吧！");
                return;
            }

            if (str == "")
            {
                MessageBox.Show("剪切板是空的！");
                return;
            }

            var mc = rxMatchIp.Matches(str);

            foreach (Match m in mc)
            {
                ImportIp(m.Value);
            }
        }


        private void mStartStdTest_Click(object sender, EventArgs e)
        {
            if (GaIsTesting)
            {
                MessageBox.Show("正在运行代理测试！");
                return;
            }

            if (StdIsTesting)
            {
                MessageBox.Show("标准测试已运行！");
                return;
            }

            WaitQueue.Clear();

            var rows = SelectNa("std");

            if (rows.Length == 0)
            {
                MessageBox.Show("没有要测试的IP！");
                return;
            }

            pbProgress.Maximum = rows.Length;
            foreach (var row in rows)
            {
                WaitQueue.Enqueue(row[0].ToString());
            }

            StdIsTesting = true;
            StdTestTimer.Start();
        }


        private void RemoveAllIps()
        {
            IpTable.Clear();
            WaitQueue.Clear();
        }

        private void mRemoveAllIps_Click(object sender, EventArgs e)
        {
            RemoveAllIps();
        }

        private DataGridViewCell[] GetSelectdIpCells()
        {
            var cells = new List<DataGridViewCell>();

            foreach (DataGridViewCell cell in dgvIpData.SelectedCells)
            {
                if (cell.ColumnIndex == 0)
                {
                    cells.Add(cell);
                }
            }

            cells.Sort((x, y) =>
            {
                if (x.RowIndex > y.RowIndex)
                    return 1;

                if (x.RowIndex == y.RowIndex)
                    return 0;
                else
                    return -1;
            });

            return cells.ToArray();
        }

        private DataGridViewCell[] GetAllIpCells()
        {
            var cells = new List<DataGridViewCell>();

            foreach (DataGridViewRow row in dgvIpData.Rows)
            {
                cells.Add(row.Cells[0]);
            }

            cells.Sort((x, y) =>
            {
                if (x.RowIndex > y.RowIndex)
                    return 1;

                if (x.RowIndex == y.RowIndex)
                    return 0;
                else
                    return -1;
            });

            return cells.ToArray();
        }

        private string BuildIpString(DataGridViewCell[] cells)
        {
            var sbd = new StringBuilder(cells[0].Value.ToString());

            for (int i = 1; i < cells.Length; i++)
            {
                sbd.Append("|" + cells[i].Value);
            }

            return sbd.ToString();
        }
        private void mExportSelectedIps_Click(object sender, EventArgs e)
        {
            var cells = GetSelectdIpCells();

            if (cells.Length == 0)
            {
                MessageBox.Show("没有选中的IP！");
                return;
            }

            try
            {
                Clipboard.SetText(BuildIpString(cells));
            }
            catch (Exception) { MessageBox.Show("操作剪切板可能失败！再试一次吧！"); }
        }

        private void nTestTimeout_ValueChanged(object sender, EventArgs e)
        {
            TestTimeout = Convert.ToInt32(nTestTimeout.Value);
        }
        private void nMaxTest_ValueChanged(object sender, EventArgs e)
        {
            MaxThreads = Convert.ToInt32(nMaxThreads.Value);
        }
        private void mStopTest_Click(object sender, EventArgs e)
        {
            StdTestTimer.Stop();
            StopGaTest = true;

            pbProgress.Value = 0;
            lProgress.Text = "0 / 0";
        }

        private void mExportAllIps_Click(object sender, EventArgs e)
        {
            var cells = GetAllIpCells();

            if (cells.Length == 0)
            {
                MessageBox.Show("IP列表是空的！");
                return;
            }

            try
            {
                Clipboard.SetText(BuildIpString(cells));
            }
            catch (Exception) { MessageBox.Show("操作剪切板可能失败！再试一次吧！"); }
        }

        private void mRemoveSelectedIps_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvIpData.SelectedRows)
            {
                dgvIpData.Rows.Remove(row);
            }
        }

        private void mRemoveIpsInClipbord_Click(object sender, EventArgs e)
        {
            var str = "";
            try
            {
                str = Clipboard.GetText();
            }
            catch (Exception)
            {
                MessageBox.Show("操作剪切板可能失败！再试一次吧！");
                return;
            }

            if (str == "")
            {
                MessageBox.Show("剪切板是空的！");
                return;
            }

            var mc = rxMatchIp.Matches(str);

            foreach (Match m in mc)
            {
                RemoveIp(m.Value);
            }
        }

        private void mStartGaTest_Click(object sender, EventArgs e)
        {
            if (StdIsTesting)
            {
                MessageBox.Show("正在运行标准测试！");
                return;
            }

            if (GaIsTesting)
            {
                MessageBox.Show("代理测试已运行！");
                return;
            }

            WaitQueue.Clear();
            var rows = SelectNa("proxy", "std asc");

            if (rows.Length == 0)
            {
                MessageBox.Show("没有要测试的IP！");
                return;
            }

            pbProgress.Maximum = rows.Length;
            foreach (var row in rows)
            {
                WaitQueue.Enqueue(row[0].ToString());
            }

            if (!File.Exists("proxy.py"))
            {
                MessageBox.Show("没有找到'proxy.py'，请将本程序放入GoAgent目录！");
                return;
            }

            GaCopyFiles();

            if (IniFile == null)
            {
                GaInitIniFile();
            }

            if (GaStartInfo == null)
            {
                GaInitStartInfo();
            }

            var procs = Process.GetProcessesByName("python27go");
            foreach (var proc in procs)
            {
                proc.Kill();
                proc.WaitForExit();
                proc.Close();
            }


            var thread = new Thread(GaTestLoop);
            thread.Start();

            while (GaIsTesting)
            {
                Application.DoEvents();
            }

        }

        private delegate void GaProgressHandler(int left);

        private void SetGaProgress(int left)
        {
            if (InvokeRequired)
            {
                Invoke(new GaProgressHandler(SetGaProgress), left);
            }
            else
            {
                pbProgress.Value = pbProgress.Maximum - left;
                lProgress.Text = left + " / " + pbProgress.Maximum;
            }
        }


        private IniFile IniFile;
        private ProcessStartInfo GaStartInfo;

        public bool StopGaTest;


        public void GaCopyFiles()
        {
            Directory.CreateDirectory("gatester");
            Directory.CreateDirectory(@"gatester\certs");
            File.Copy("python27.exe", @"gatester\python27go.exe", true);
            File.Copy("python27.dll", @"gatester\python27.dll", true);
            File.Copy("python27.zip", @"gatester\python27.zip", true);
            File.Copy("pygeoip-0.3.1.egg", @"gatester\pygeoip-0.3.1.egg", true);
            File.Copy("proxy.py", @"gatester\proxy.py", true);
            File.Copy("dnslib-0.8.3.egg", @"gatester\dnslib-0.8.3.egg", true);
            File.Copy("CA.crt", @"gatester\CA.crt", true);

            foreach (var path in Directory.GetFiles("certs"))
            {
                File.Copy(path, @"gatester\certs\" + Path.GetFileName(path), true);
            }
        }


        public void GaTestLoop()
        {
            GaIsTesting = true;

            StopGaTest = false;

            var isTesting = false;

            var proxy = new WebProxy("127.0.0.1", 8082);
            var stopwatch = new Stopwatch();

            var url = tbUrl.Text;

            while (!StopGaTest && WaitQueue.Count > 0)
            {
                if (!isTesting)
                {
                    isTesting = true;
                    var addr = WaitQueue.Dequeue();

                    IniFile.WriteValue("iplist", "google_hk", addr);
                    IniFile.WriteValue("iplist", "google_cn", addr);
                    IniFile.WriteValue("iplist", "google_talk", addr);

                    IniFile.WriteFile(@"gatester\proxy.ini");

                    GaStartInfo.CreateNoWindow = !cbWindow.Checked;
                    var process = new Process { StartInfo = GaStartInfo };
                    process.Start();

                    Thread.Sleep(200);

                    var req = (HttpWebRequest)WebRequest.Create(url);
                    req.Timeout = TestTimeout;
                    req.Method = "HEAD";
                    req.Proxy = proxy;
                    req.AllowAutoRedirect = false;
                    req.KeepAlive = false;
                    req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                    stopwatch.Reset();
                    stopwatch.Start();
                    try
                    {
                        var resp = (HttpWebResponse)req.GetResponse();
                        if (resp.Server == "gws")
                        {
                            SetGaTestResult(addr, "_OK " + stopwatch.ElapsedMilliseconds.ToString("D4"));
                        }
                        else
                        {
                            SetGaTestResult(addr, "Invalid");
                        }

                        resp.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        SetGaTestResult(addr, "Invalid");
                    }
                    stopwatch.Stop();


                    process.Kill();
                    process.WaitForExit();
                    process.Close();
                    SetGaProgress(WaitQueue.Count);
                    isTesting = false;
                }
            }

            GaIsTesting = false;
        }

        private void GaInitIniFile()
        {
            IniFile = new IniFile("proxy.ini");

            if (File.Exists("proxy.user.ini"))
            {
                IniFile.OverLoad("proxy.user.ini");
            }

            IniFile.WriteValue("pac", "enable", "0");
            IniFile.WriteValue("php", "enable", "0");
            IniFile.WriteValue("proxy", "enable", "0");
            IniFile.WriteValue("dns", "enable", "0");
            IniFile.WriteValue("listen", "ip", "127.0.0.1");
            IniFile.WriteValue("listen", "port", "8082");
        }


        private void GaInitStartInfo()
        {
            GaStartInfo = new ProcessStartInfo();
            GaStartInfo.EnvironmentVariables.Add("GEVENT_LOOP", "uvent.loop.UVLoop");
            GaStartInfo.EnvironmentVariables.Add("GEVENT_RESOLVER", "block");
            GaStartInfo.EnvironmentVariables.Add("GOAGENT_LISTEN_VISIBLE", "1");
            GaStartInfo.Arguments = @"gatester\proxy.py";
            GaStartInfo.FileName = @"gatester\python27go.exe";
            GaStartInfo.UseShellExecute = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
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
        class TestResult
        {
            public IPAddress addr;
            public bool ok;
            public string msg;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private readonly Regex rxMatchIPv4 =
            new Regex(@"(?<!:)((2(5[0-5]|[0-4]\d)|1?\d?\d)\.){3}(2(5[0-5]|[0-4]\d)|1?\d?\d)", RegexOptions.Compiled);
        private readonly Regex rxMatchIPv6 =
           new Regex(@"(:|[\da-f]{1,4})(:?:[\da-f]{1,4})+(::)?",
               RegexOptions.Compiled);

        private readonly DataTable IpTable = new DataTable();


        public static HashSet<IPAddress> TestedSet = new HashSet<IPAddress>();
        public static Queue<IPAddress> WaitQueue = new Queue<IPAddress>();
        public static Queue<int> TestCountQueue = new Queue<int>();

        private readonly Timer StdTestTimer = new Timer();
        private readonly Timer RndTestTimer = new Timer();

        private static Random random = new Random();
        public static int PingTimeout = 660;
        public static int TestTimeout = 4400;
        public static int MaxThreads = 8;

        private bool StdIsTesting;
        private bool RndIsTesting;

        private bool HighSpeed = true;

        public static bool UseProxy = false;
        public static WebProxy TestProxy = new WebProxy("192.168.1.1", 8080);

        private static RequestCachePolicy CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        private static Stopwatch Watch = new Stopwatch();

        private void Form1_Load(object sender, EventArgs e)
        {
            int count = IpRange.Pool4B.Sum(range => range.Count);

            Text = string.Format("GoGo Tester 2 - Total {0} IPs", count);

            Icon = Resources.GoGo_logo;

            IpTable.Columns.Add(new DataColumn("addr", typeof(string))
            {
                Unique = true,
            });
            IpTable.Columns.Add(new DataColumn("std", typeof(string)));

            dgvIpData.DataSource = IpTable;
            dgvIpData.Columns[0].Width = 160;
            dgvIpData.Columns[0].HeaderText = "地址";
            dgvIpData.Columns[1].Width = 100;
            dgvIpData.Columns[1].HeaderText = "标准测试";

            /// Std
            ServicePointManager.ServerCertificateValidationCallback = (o, certificate, chain, errors) => true;

            StdTestTimer.Interval = 25;
            StdTestTimer.Elapsed += StdTestTimerElapsed;

            RndTestTimer.Interval = 25;
            RndTestTimer.Elapsed += RndTestTimerElapsed;


            Watch.Start();
        }

        private static int SetRange(int val, int min, int max)
        {
            val = val > min ? val : min;
            val = val < max ? val : max;
            return val;
        }

        private void SetAllNa()
        {
            foreach (var row in IpTable.Select())
            {
                row[1] = "n/a";
            }
        }

        private void RndTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var testCount = TestCountQueue.Count;
            var waitCount = dgvIpData.RowCount;
            var testedCount = TestedSet.Count;

            SetRndProgress(testCount, waitCount, testedCount);

            if (RndIsTesting && waitCount < Form2.RandomNumber && testCount < MaxThreads)
            {
                Monitor.Enter(TestedSet);
                IPAddress addr;
                do
                {
                    IpRange iprange = HighSpeed ? IpRange.Pool4B[random.Next(0, IpRange.Pool4B.Count)] : IpRange.Pool4C[random.Next(0, IpRange.Pool4C.Count)];
                    addr = iprange.GetRandomIp();
                } while (TestedSet.Contains(addr));
                TestedSet.Add(addr);
                Monitor.Exit(TestedSet);

                new Thread(() =>
                {
                    Enqueue(TestCountQueue);

                    var result = TestProcess(addr);
                    if (result.ok)
                    {
                        ImportIp(addr);
                        SetTestResult(result);
                    }

                    Dequeue(TestCountQueue);
                }).Start();
            }
            else if (testCount == 0)
            {
                RndTestTimer.Stop();
                if (RndIsTesting)
                {
                    PlaySound();
                }
                RndIsTesting = false;
            }
        }

        private static void Enqueue(Queue<int> queue)
        {
            Monitor.Enter(queue);
            queue.Enqueue(0);
            Monitor.Exit(queue);
        }
        private static void Dequeue(Queue<int> queue)
        {
            Monitor.Enter(queue);
            queue.Dequeue();
            Monitor.Exit(queue);
        }

        private void StdTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var testCount = TestCountQueue.Count;
            var waitCount = WaitQueue.Count;

            SetStdProgress(testCount, waitCount);

            if (StdIsTesting && waitCount > 0 && testCount < MaxThreads)
            {
                var addr = WaitQueue.Dequeue();
                new Thread(() =>
                {
                    Enqueue(TestCountQueue);

                    var result = TestProcess(addr);
                    SetTestResult(result);

                    Dequeue(TestCountQueue);
                }).Start();
            }
            else if (waitCount == 0 && testCount == 0)
            {
                StdTestTimer.Stop();
                if (StdIsTesting)
                {
                    PlaySound();
                }
                StdIsTesting = false;
            }
        }

        private readonly SoundPlayer SoundPlayer = new SoundPlayer { Stream = Resources.Windows_Ding };

        private void PlaySound()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(PlaySound));
            }
            else
            {
                SoundPlayer.Play();
            }
        }

        private void SetStdProgress(int testCount, int waitCount)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetStdProgress(testCount, waitCount)));
            }
            else
            {
                pbProgress.Value = SetRange(pbProgress.Maximum - waitCount - testCount, 0, pbProgress.Maximum);
                lProgress.Text = testCount + " / " + waitCount;
            }
        }

        private void SetRndProgress(int testCount, int waitCount, int failedCount)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetRndProgress(testCount, waitCount, failedCount)));
            }
            else
            {
                pbProgress.Value = SetRange(waitCount, 0, pbProgress.Maximum);
                lProgress.Text = testCount + " / " + waitCount + " / " + failedCount;
            }
        }

        private string GenMixedUrl(string head, IPAddress addr)
        {
            string url;

            if (addr.AddressFamily == AddressFamily.InterNetwork)
            {
                var sbd = new StringBuilder(1500);

                for (int i = 0; i < 150; i++)
                    sbd.Append(random.Next().ToString("D10"));
                url = head + "://" + addr + "/?" + Convert.ToBase64String(Encoding.UTF8.GetBytes(sbd.ToString()));
            }
            else
            {
                string addrStr = string.Format("[{0}]", addr);
                url = "http://" + addrStr;
            }

            return url;
        }

        private TestResult TestWithProxy(IPAddress addr)
        {
            var result = new TestResult
            {
                addr = addr,
                ok = false
            };

            var url = GenMixedUrl("http", addr);

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = TestTimeout;
            req.Method = "HEAD";
            req.Proxy = TestProxy;
            req.AllowAutoRedirect = false;
            req.KeepAlive = false;
            req.CachePolicy = CachePolicy;

            var stime = Watch.ElapsedMilliseconds;

            try
            {
                using (var resp = (HttpWebResponse)req.GetResponse())
                {
                    if (IsValidServer(resp.Server))
                    {
                        result.ok = true;
                        result.msg = "_OK P " + (Watch.ElapsedMilliseconds - stime).ToString("D4");
                    }
                    else
                    {
                        result.msg = "Invalid";
                    }

                    result.msg += " " + resp.Server;
                }
            }
            catch (Exception)
            {
                result.msg = "Failed";
            }

            return result;
        }

        private TestResult TestPing(IPAddress addr, int port, int timeout)
        {
            var result = new TestResult
            {
                addr = addr,
                ok = false,
            };

            var socket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.IP);

            var stime = Watch.ElapsedMilliseconds;

            try
            {
                if (socket.BeginConnect(addr, port, null, null).AsyncWaitHandle.WaitOne(timeout, true))
                {
                    if (socket.Connected)
                    {
                        result.ok = true;
                        result.msg = "_OK " + (Watch.ElapsedMilliseconds - stime).ToString("D4");

                        socket.Shutdown(SocketShutdown.Both);
                    }
                    else
                    {
                        result.msg = "Failed";
                    }
                }
                else
                {
                    result.msg = "Timeout";
                }
            }
            catch (Exception)
            {
                result.msg = "Errored";
            }

            socket.Close();

            return result;
        }

        private TestResult TestProcess(IPAddress addr)
        {
            if (UseProxy)
            {
                return TestWithProxy(addr);
            }

            TestResult result = TestPing(addr, 443, PingTimeout);

            if (!result.ok)
            {
                return result;
            }

            Thread.Sleep(100);

            var url = GenMixedUrl("https", addr);

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = TestTimeout;
            req.Method = "HEAD";
            req.AllowAutoRedirect = false;
            req.KeepAlive = false;
            req.CachePolicy = CachePolicy;

            try
            {
                using (var resp = (HttpWebResponse)req.GetResponse())
                {
                    if (!IsValidServer(resp.Server))
                    {
                        result.ok = false;
                    }

                    result.msg += " " + resp.Server;
                }
            }
            catch (Exception)
            {
                result.ok = false;
            }

            return result;
        }


        private static bool IsValidServer(string server)
        {
            server = server.ToLower();
            return (server == "gws" || server == "sffe");
        }

        private void SetTestResult(TestResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetTestResult(result)));
            }
            else
            {
                var rows = SelectByIp(result.addr);
                if (rows.Length > 0)
                    rows[0][1] = result.msg;
            }
        }

        private void ImportIp(IPAddress addr)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => ImportIp(addr)));
            }
            else
            {
                try
                {
                    var row = IpTable.NewRow();
                    row[0] = addr;
                    row[1] = "n/a";
                    IpTable.Rows.Add(row);
                }
                catch (Exception) { }
            }
        }

        private void RemoveIp(IPAddress addr)
        {
            var row = SelectByIp(addr);
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

        private DataRow[] SelectByIp(IPAddress addr)
        {
            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByIp(addr)));
            }

            return IpTable.Select(string.Format("addr = '{0}'", addr));
        }

        private DataRow[] SelectNa()
        {
            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new MethodInvoker(() => SelectNa()));
            }

            return IpTable.Select("std = 'n/a'");
        }

        private void Tip_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as Control;

            if (control != null)
            {
                lTip.Text = control.Tag.ToString();
            }
            else
            {
                var menu = sender as ToolStripMenuItem;
                if (menu != null)
                {
                    lTip.Text = menu.Tag.ToString();
                }
            }

        }

        private bool IsTesting()
        {
            if (StdIsTesting || RndIsTesting)
            {
                MessageBox.Show("有测试正在进行，无法完成操作！");
                return true;
            }
            return false;
        }

        private void bAddIpRange_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

            var str = tbIpRange.Text.Trim();
            tbIpRange.ResetText();
            if (str == "")
            {
                return;
            }

            var ranges = str.Split(@"`\|/~!?@#$%^&*()=+,<>;:'，。；：“”‘’？、".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var range in ranges)
            {
                var iprange = IpRange.CreateIpRange(range);
                if (iprange == null)
                {
                    continue;
                }

                for (int a = iprange.Cope[0, 0]; a <= iprange.Cope[0, 1]; a++)
                {
                    for (int b = iprange.Cope[1, 0]; b <= iprange.Cope[1, 1]; b++)
                    {
                        for (int c = iprange.Cope[2, 0]; c <= iprange.Cope[2, 1]; c++)
                        {
                            for (int d = iprange.Cope[3, 0]; d <= iprange.Cope[3, 1]; d++)
                            {
                                try
                                {
                                    ImportIp(IPAddress.Parse(a + "." + b + "." + c + "." + d));
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoveAllIps();
            while (TestCountQueue.Count > 0)
            {
                Application.DoEvents();
            }
        }

        private void mImportIpsInClipbord_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

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

            var ips = GetIpsInText(str);

            if (ips.Length == 0)
            {
                MessageBox.Show("剪切板内没有IP！");
                return;
            }

            foreach (var ip in ips)
            {
                ImportIp(ip);
            }
        }


        private void mStartStdTest_Click(object sender, EventArgs e)
        {
            if (RndIsTesting)
            {
                MessageBox.Show("正在运行随机测试！");
                return;
            }

            if (StdIsTesting)
            {
                MessageBox.Show("标准测试已运行！");
                return;
            }

            if (IpTable.Rows.Count == 0)
            {
                return;
            }

            WaitQueue.Clear();

            var rows = SelectNa();

            if (rows.Length == 0)
            {
                if (MessageBox.Show(this, "没有发现未测试的IP！是否重复测试已测试的IP？某些地区重复测试会导致IP被封锁！", "请确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    SetAllNa();
                    rows = SelectNa();
                }
                else
                {
                    return;
                }
            }

            if (rows.Length == 0)
            {
                MessageBox.Show("没有发现要测试的IP！");
                return;
            }

            pbProgress.Maximum = rows.Length;
            pbProgress.Value = 0;

            foreach (var row in rows)
            {
                WaitQueue.Enqueue(IPAddress.Parse(row[0].ToString()));
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
            if (IsTesting())
            {
                return;
            }

            RemoveAllIps();
        }

        private DataGridViewCell[] GetSelectdIpCells()
        {
            var cells = dgvIpData.SelectedCells.Cast<DataGridViewCell>().Where(cell => cell.ColumnIndex == 0).ToList();

            cells.Sort((x, y) =>
            {
                if (x.RowIndex > y.RowIndex)
                    return 1;

                if (x.RowIndex == y.RowIndex)
                    return 0;

                return -1;
            });

            return cells.ToArray();
        }

        private DataGridViewCell[] GetAllIpCells()
        {
            var cells = (from DataGridViewRow row in dgvIpData.Rows select row.Cells[0]).ToList();

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

        private string BuildIpString(IList<string> strs)
        {
            var sbd = new StringBuilder(strs[0]);

            for (int i = 1; i < strs.Count; i++)
            {
                sbd.Append("|" + strs[i]);
            }

            return sbd.ToString();
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
            //if (IsTesting())
            //{
            //    return;
            //}

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

        private void nPingTimeout_ValueChanged(object sender, EventArgs e)
        {
            PingTimeout = (int)(Convert.ToInt32(nPingTimeout.Value) * 1.1);
        }

        private void nTestTimeout_ValueChanged(object sender, EventArgs e)
        {
            TestTimeout = (int)(Convert.ToInt32(nTestTimeout.Value) * 1.1);
        }

        private void nMaxTest_ValueChanged(object sender, EventArgs e)
        {
            MaxThreads = Convert.ToInt32(nMaxThreads.Value);
        }

        private void mStopTest_Click(object sender, EventArgs e)
        {
            StdIsTesting = false;
            RndIsTesting = false;
        }

        private void mExportAllIps_Click(object sender, EventArgs e)
        {
            //if (IsTesting())
            //{
            //    return;
            //}

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
            if (IsTesting())
            {
                return;
            }

            foreach (DataGridViewRow row in dgvIpData.SelectedRows)
            {
                dgvIpData.Rows.Remove(row);
            }
        }

        private void mRemoveIpsInClipbord_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

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

            foreach (var ip in GetIpsInText(str))
            {
                RemoveIp(ip);
            }
        }

        private void mStartRndTest_Click(object sender, EventArgs e)
        {
            if (StdIsTesting)
            {
                MessageBox.Show("正在运行标准测试！");
                return;
            }

            if (RndIsTesting)
            {
                MessageBox.Show("随机测试已运行！");
                return;
            }

            //if (MessageBox.Show(this, "随机测试前会清除IP列表，是否继续操作？", "请确认操作！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            //{
            //    return;
            //}

            var form = new Form2();
            form.ShowDialog(this);

            if (Form2.RandomNumber == 0)
            {
                return;
            }

            pbProgress.Maximum = Form2.RandomNumber;
            pbProgress.Value = 0;

            //RemoveAllIps();
            //CacheQueue.Clear();

            RndIsTesting = true;
            RndTestTimer.Start();
        }

        private void mApplyValidIpsToUserConfig_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

            if (!File.Exists("proxy.py"))
            {
                MessageBox.Show("请将本程序放入GoAgent目录内！");
                return;
            }

            var vips = GetValidIps();

            if (vips.Length == 0)
            {
                MessageBox.Show("没有可用的IP！");
                return;
            }

            var ipstr = BuildIpString(vips);

            ApplyToUserConfig(ipstr);
        }

        private void ApplyToUserConfig(string ipstr)
        {
            if (!File.Exists("proxy.user.ini"))
            {
                File.WriteAllText("proxy.user.ini", "");
            }

            var inifile = new IniFile("proxy.user.ini");

            inifile.WriteValue("iplist", "google_cn", ipstr);
            inifile.WriteValue("iplist", "google_hk", ipstr);
            //    inifile.WriteValue("iplist", "google_talk", ipstr);

            inifile.WriteFile();

            MessageBox.Show("已写入proxy.user.ini！重新载入GoAgent就可生效！");
        }

        private string[] GetValidIps()
        {
            var ls = new List<string>();
            var rows = SelectByExpr(string.Format("std like '_OK%'"), "std asc");
            foreach (var row in rows)
            {
                ls.Add(row[0].ToString());
            }
            return ls.ToArray();
        }

        private DataRow[] GetInvalidIps()
        {
            var rows = SelectByExpr(string.Format("std <> 'n/a' and std not like '_OK%'"));
            return rows;
        }

        private IPAddress[] GetIpsInText(string str)
        {
            var ls = new List<IPAddress>();

            var hset = new HashSet<string>();
            var mcv4 = rxMatchIPv4.Matches(str);
            foreach (Match m in mcv4)
            {
                if (hset.Add(m.Value))
                {
                    try { ls.Add(IPAddress.Parse(m.Value)); }
                    catch (Exception) { }
                }
            }

            hset.Clear();
            var mcv6 = rxMatchIPv6.Matches(str);
            foreach (Match m in mcv6)
            {
                if (hset.Add(m.Value))
                {
                    try { ls.Add(IPAddress.Parse(m.Value)); }
                    catch (Exception) { }
                }
            }

            return ls.ToArray();
        }


        private void mApplySelectedIpsToUserConfig_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

            if (!File.Exists("proxy.py"))
            {
                MessageBox.Show("请将本程序放入GOAgent目录内！");
                return;
            }

            var cells = GetSelectdIpCells();

            if (cells.Length == 0)
            {
                MessageBox.Show("没有选中的IP！");
                return;
            }

            var ipstr = BuildIpString(cells);

            ApplyToUserConfig(ipstr);
        }

        private void mRemoveInvalidIps_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

            foreach (var row in GetInvalidIps())
            {
                IpTable.Rows.Remove(row);
            }
        }

        private void mSetTestProxy_Click(object sender, EventArgs e)
        {
            var form = new Form3();
            form.ShowDialog(this);
        }

        private void cbHighSpeed_CheckedChanged(object sender, EventArgs e)
        {
            HighSpeed = cbHighSpeed.Checked;
        }
    }
}

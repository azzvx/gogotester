using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GoGo_Tester.Properties;
using Timer = System.Timers.Timer;

namespace GoGo_Tester
{
    public partial class Form1 : Form
    {
        class TestInfomation
        {
            public IPEndPoint Target { get; private set; }
            public bool PortOk { get; set; }
            public bool HttpOk { get; set; }
            public string PortMsg { get; set; }
            public string HttpMsg { get; set; }

            public bool UseSsl
            {
                get { return Target.Port == 443; }
            }
            public string Protocol
            {
                get { return Target.Port == 443 ? "https" : "http"; }
            }
            public string Host
            {
                get { return Target.AddressFamily == AddressFamily.InterNetwork ? Target.Address.ToString() : string.Format("[{0}]", Target.Address); }
            }
            public int Port
            {
                get { return Target.Port; }
            }
            public TestInfomation(IPAddress addr, int port)
            {
                Target = new IPEndPoint(addr, port);
                HttpOk = PortOk = false;
                PortMsg = HttpMsg = "n/a";
            }
        }

        public Form1()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback = (o, certificate, chain, errors) => true;
        }

        private static readonly Regex rxMatchIPv4 = new Regex(@"(?<!:)((2(5[0-5]|[0-4]\d)|1?\d?\d)\.){3}(2(5[0-5]|[0-4]\d)|1?\d?\d)", RegexOptions.Compiled);
         private static readonly Regex rxMatchIPv6 = new Regex(@"(:|[\da-f]{1,4})(:?:[\da-f]{1,4})+(::)?", RegexOptions.Compiled);
        private static readonly Regex rxServerValid = new Regex(@"server:\s*(gws|sffe)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Random random = new Random();
        private static readonly Stopwatch Watch = new Stopwatch();

        private readonly DataTable IpTable = new DataTable();
        private readonly Timer StdTestTimer = new Timer();
        private readonly Timer RndTestTimer = new Timer();

        public static HashSet<IPAddress> CacheSet = new HashSet<IPAddress>();
        public static Queue<IPAddress> WaitQueue = new Queue<IPAddress>();
        public static Queue<int> CountQueue = new Queue<int>();

        private bool StdIsTesting;
        private bool RndIsTesting;


        private void DebugFunc()
        {
            // Config.UseProxy = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DebugFunc();

            int count = IpRange.Pool4B.Sum(range => range.Count);

            Text = string.Format("GoGo Tester 2 - Total {0} IPs", count);

            Icon = Resources.GoGo_logo;

            IpTable.Columns.Add(new DataColumn("addr", typeof(string))
            {
                Unique = true,
            });
            IpTable.Columns.Add(new DataColumn("port", typeof(string)));
            IpTable.Columns.Add(new DataColumn("http", typeof(string)));

            dgvIpData.DataSource = IpTable;
            dgvIpData.Columns[0].Width = 160;
            dgvIpData.Columns[0].HeaderText = "地址";
            dgvIpData.Columns[1].Width = 100;
            dgvIpData.Columns[1].HeaderText = "端口";
            dgvIpData.Columns[2].Width = 100;
            dgvIpData.Columns[2].HeaderText = "HTTP/S";

            StdTestTimer.Interval = 200;
            StdTestTimer.Elapsed += StdTestTimerElapsed;

            RndTestTimer.Interval = 200;
            RndTestTimer.Elapsed += RndTestTimerElapsed;

            LoadRndTestCache();

            Watch.Start();
        }

        private static int SetRange(int val, int min, int max)
        {
            val = val > min ? val : min;
            val = val < max ? val : max;
            return val;
        }

        private static void EnCount(Queue<int> queue)
        {
            Monitor.Enter(queue);
            queue.Enqueue(0);
            Monitor.Exit(queue);
        }
        private static void DeCount(Queue<int> queue)
        {
            Monitor.Enter(queue);
            queue.Dequeue();
            Monitor.Exit(queue);
        }


        private void StdTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var testCount = CountQueue.Count;
            var waitCount = WaitQueue.Count;

            SetStdProgress(testCount, waitCount);

            if (StdIsTesting && waitCount > 0 && testCount < Config.MaxThreads)
            {
                var addr = WaitQueue.Dequeue();
                new Thread(() =>
                {
                    EnCount(CountQueue);

                    var info = TestProcess(new TestInfomation(addr, 443));
                    SetTestResult(info);

                    DeCount(CountQueue);
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

        private void RndTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var testCount = CountQueue.Count;
            var waitCount = dgvIpData.RowCount;
            var testedCount = CacheSet.Count;

            SetRndProgress(testCount, waitCount, testedCount);

            if (RndIsTesting && waitCount < Form2.RandomNumber && testCount < Config.MaxThreads)
            {
                Monitor.Enter(CacheSet);
                IPAddress addr;
                do
                {
                    IpRange iprange = Config.HighSpeed ? IpRange.Pool4B[random.Next(0, IpRange.Pool4B.Count)] : IpRange.Pool4C[random.Next(0, IpRange.Pool4C.Count)];
                    addr = iprange.GetRandomIp();
                } while (!CacheSet.Add(addr));
                Monitor.Exit(CacheSet);

                new Thread(() =>
                {
                    EnCount(CountQueue);

                    var info = TestProcess(new TestInfomation(addr, 443));
                    if (info.HttpOk)
                    {
                        ImportIp(addr);
                        SetTestResult(info);
                    }
                    DeCount(CountQueue);

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
                SaveRndTestCache();
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
            var sbd = new StringBuilder(head + "://");

            if (addr.AddressFamily == AddressFamily.InterNetwork)
            {
                sbd.Append(addr + "/");

                for (int i = 10; i < 50; i++)
                {
                    sbd.Append("?");
                    sbd.Append(Convert.ToBase64String(Encoding.ASCII.GetBytes(random.Next().ToString())));
                    sbd.Append("=");
                    sbd.Append(Convert.ToBase64String(Encoding.ASCII.GetBytes(random.Next().ToString())));
                }
            }
            else
            {
                sbd.Append("[" + addr + "]");
            }

            return sbd.ToString();
        }

        private TestInfomation TestProcess(TestInfomation info)
        {
            if (Config.UseProxy && Config.ProxySocket.Connected)
            {
                TestHttpViaProxy(info);
            }
            else
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    SendTimeout = Config.SocketTimeout,
                    ReceiveTimeout = Config.SocketTimeout
                };
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

                TestPortViaSocket(socket, info, info.Target);
                TestHttpViaSocket(socket, info);

                socket.Close();
            }

            return info;
        }
        private bool TestPortViaSocket(Socket socket, TestInfomation info, IPEndPoint target)
        {
            var stime = Watch.ElapsedMilliseconds;
            try
            {
                if (socket.BeginConnect(target, null, null).AsyncWaitHandle.WaitOne(Config.PingTimeout))
                {
                    var ctime = Watch.ElapsedMilliseconds - stime;

                    if (socket.Connected)
                    {
                        info.PortOk = true;
                        info.PortMsg = (Config.UseProxy ? "_OK P " : "_OK ") + ctime.ToString("D4");
                    }
                    else
                    {
                        info.PortMsg = "Invalid";
                    }
                }
                else
                {
                    info.PortMsg = "Timeout";
                }
            }
            catch (Exception ex)
            {
                info.PortMsg = "Errored";
            }

            return info.PortOk;
        }

        private bool TestHttpViaProxy(TestInfomation info)
        {
            var url = GetRequestUrl("http", info.Host, 80, false);
            var header = GetRequestHeaders("HEAD", url, info.Host, 80, false);

            var data = Encoding.UTF8.GetBytes(header);

            var stime = Watch.ElapsedMilliseconds;

            Monitor.Enter(Config.ProxySocket);
            TestHttpRequest(info, stime, data);
            Monitor.Exit(Config.ProxySocket);

            return info.HttpOk;
        }

        private bool TestHttpViaSocket(Socket socket, TestInfomation info)
        {
            var url = GetRequestUrl(info.Protocol, info.Host, info.Port);
            var header = GetRequestHeaders("HEAD", url, info.Host, info.Port);

            var data = Encoding.UTF8.GetBytes(header);

            var stime = Watch.ElapsedMilliseconds;
            try
            {
                using (var nets = new NetworkStream(socket))
                {
                    if (((IPEndPoint)socket.RemoteEndPoint).Port == 443)
                    {
                        using (var ssls = new SslStream(nets, false, (o, xa, ab, s) => true))
                        {
                            ssls.AuthenticateAsClient("null");

                            if (ssls.IsAuthenticated)
                            {
                                TestHttpRequest(ssls, info, stime, data);
                            }
                            else
                            {
                                info.HttpMsg = "SslInvalid";
                            }
                        }
                    }
                    else
                    {
                        TestHttpRequest(nets, info, stime, data);
                    }
                }
            }
            catch (Exception)
            {
                info.HttpMsg = "Errored";
            }

            return info.HttpOk;
        }

        private void TestHttpRequest(Stream stream, TestInfomation info, long stime, byte[] data)
        {
            try
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();

                using (var sr = new StreamReader(stream))
                {
                    var content = sr.ReadToEnd();
                    var ctime = Watch.ElapsedMilliseconds - stime;
                    if (rxServerValid.IsMatch(content))
                    {
                        info.HttpOk = true;
                        info.HttpMsg = "_OK " + ctime.ToString("D4");
                    }
                    else
                    {
                        info.HttpMsg = "Invalid";
                    }
                }
            }
            catch (Exception)
            {
                info.HttpMsg = "Timeout";
            }
        }

        private void TestHttpRequest(TestInfomation info, long stime, byte[] data)
        {
            try
            {
                if (!Config.ProxySocket.Connected)
                {
                    Config.ProxySocket.Connect(Config.ProxyTarget);
                }

                Config.ProxySocket.Send(data);

                var buf = new byte[2048];
                Config.ProxySocket.Receive(buf);

                var content = Encoding.UTF8.GetString(buf);

                var ctime = Watch.ElapsedMilliseconds - stime;
                if (rxServerValid.IsMatch(content))
                {
                    info.HttpOk = true;
                    info.HttpMsg = "_OK " + ctime.ToString("D4");
                }
                else
                {
                    info.HttpMsg = "Invalid";
                }

            }
            catch (Exception ex)
            {
                info.HttpMsg = "Timeout" + ex.Message;
            }
        }


        private string GetRequestUrl(string protocol, string host, int port, bool genargs = true)
        {
            var ubd = new StringBuilder();
            ubd.Append(string.Format("{0}://{1}", protocol, host));

            if (genargs)
            {
                ubd.Append(string.Format("?{0}={1}", random.Next(), random.Next()));
                for (int i = 10; i < 30; i++)
                    ubd.Append(string.Format("&{0}={1}", random.Next(), random.Next()));
            }

            return ubd.ToString();
        }

        private string GetRequestHeaders(string method, string url, string host, int port, bool close = true, params string[] headers)
        {
            //http headers
            var sbd = new StringBuilder();
            sbd.AppendLine(string.Format("{0} {1} HTTP/1.1", method, url));
            sbd.AppendLine(string.Format("Host: {0}", host));

            if (close)
                sbd.AppendLine("Connection: Close");
            else
                sbd.AppendLine("Connection: Keep-Alive");

            foreach (var arg in headers)
                sbd.AppendLine(arg);

            //http headers ending
            sbd.AppendLine();

            return sbd.ToString();
        }



        private void SetTestResult(TestInfomation result)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetTestResult(result)));
            }
            else
            {
                var rows = SelectByIp(result.Target.Address);
                if (rows.Length > 0)
                {
                    rows[0][1] = result.PortMsg;
                    rows[0][2] = result.HttpMsg;
                }
            }
        }

        #region IpTable
        private void RemoveIp(IPAddress addr)
        {
            var row = SelectByIp(addr);
            if (row.Length > 0)
            {
                IpTable.Rows.Remove(row[0]);
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
                    row[2] = "n/a";
                    IpTable.Rows.Add(row);
                }
                catch (Exception) { }
            }
        }

        private void RemoveAllIps()
        {
            IpTable.Clear();
            WaitQueue.Clear();
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

            return IpTable.Select("port = 'n/a'");
        }
        private void SetAllNa()
        {
            foreach (var row in IpTable.Select())
            {
                row[2] = row[1] = "n/a";
            }
        }

        #endregion

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
            StopTest();
            while (CountQueue.Count > 0)
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

                if (x.RowIndex < y.RowIndex)
                    return -1;

                return 0;
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
            Config.PingTimeout = (int)(Convert.ToInt32(nPingTimeout.Value) * 1.1);
        }

        private void nTestTimeout_ValueChanged(object sender, EventArgs e)
        {
            Config.SocketTimeout = (int)(Convert.ToInt32(nTestTimeout.Value) * 0.55);
        }

        private void nMaxTest_ValueChanged(object sender, EventArgs e)
        {
            Config.MaxThreads = Convert.ToInt32(nMaxThreads.Value);
            StdTestTimer.Interval = 1000 / Config.MaxThreads;
            RndTestTimer.Interval = 1000 / Config.MaxThreads;
        }

        private void mStopTest_Click(object sender, EventArgs e)
        {
            StopTest();
        }

        private void StopTest()
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
            var rows = SelectByExpr(string.Format("http like '_OK%'"), "port asc");
            foreach (var row in rows)
            {
                ls.Add(row[0].ToString());
            }
            return ls.ToArray();
        }

        private DataRow[] GetInvalidIps()
        {
            var rows = SelectByExpr(string.Format("port <> 'n/a' and port not like '_OK%'"));
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
            Config.HighSpeed = cbHighSpeed.Checked;
        }

        private void linkLabel1_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        private void SaveRndTestCache()
        {
            using (var fs = File.Create("gogo_cache", 25000 * 4))
            {
                var spChars = new[] { '.' };
                var count = 0;

                foreach (var addr in CacheSet)
                {
                    var sps = addr.ToString().Split(spChars);
                    if (sps.Length == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            fs.WriteByte(byte.Parse(sps[i]));
                        }

                        count++;
                    }

                    if (count >= 25000)
                    {
                        fs.Flush();
                    }
                }

                if (count > 0)
                {
                    fs.Flush();
                }
            }
        }

        private void LoadRndTestCache()
        {
            if (!File.Exists("gogo_cache"))
                return;

            using (var fs = File.OpenRead("gogo_cache"))
            {
                var buf = new byte[4];
                var sbd = new StringBuilder(15);

                for (int i = 0; i < (fs.Length / 4); i++)
                {
                    fs.Read(buf, 0, 4);
                    sbd.Remove(0, sbd.Length);
                    sbd.Append((int)buf[0]);
                    for (int j = 1; j < 4; j++)
                    {
                        sbd.Append(".");
                        sbd.Append((int)buf[j]);
                    }

                    CacheSet.Add(IPAddress.Parse(sbd.ToString()));
                }
            }
        }

        private void mClearRndCache_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

            CacheSet.Clear();
            if (File.Exists("gogo_cache"))
                File.Delete("gogo_cache");
        }

        private void dgvIpData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var bounds = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvIpData.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvIpData.RowHeadersDefaultCellStyle.Font, bounds, dgvIpData.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

    }
}

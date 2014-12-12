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
using GoGo_Tester.Properties;
using Timer = System.Timers.Timer;

namespace GoGo_Tester
{
    public partial class Form1 : Form
    {
        private static int SetRange(int val, int min, int max)
        {
            val = val > min ? val : min;
            val = val < max ? val : max;
            return val;
        }
        private static void EnCount(Queue<int> q)
        {
            Monitor.Enter(q);
            q.Enqueue(0);
            Monitor.Exit(q);
        }
        private static void DeCount(Queue<int> q)
        {
            Monitor.Enter(q);
            q.Dequeue();
            Monitor.Exit(q);
        }

        public Form1()
        {
            InitializeComponent();
        }
        private static readonly Random Rand = new Random();
        private static readonly Regex RxMatchIPv4 = new Regex(@"(?<!:)((2(5[0-5]|[0-4]\d)|1?\d?\d)\.){3}(2(5[0-5]|[0-4]\d)|1?\d?\d)", RegexOptions.Compiled);
        private static readonly Regex RxMatchIPv6 = new Regex(@"(:|[\da-f]{1,4})(:?:[\da-f]{1,4})+(::)?", RegexOptions.Compiled);

        private static readonly Stopwatch Watch = new Stopwatch();
        private static readonly SoundPlayer SoundPlayer = new SoundPlayer { Stream = Resources.Windows_Ding };

        private readonly Dictionary<string, IpPool> PoolDic = new Dictionary<string, IpPool>();
        private IpPool CurPool;
        private readonly DataTable IpTable = new DataTable();
        private readonly Timer StdTestTimer = new Timer();
        private readonly Timer RndTestTimer = new Timer();
        private readonly Timer BndTestTimer = new Timer();

        public static HashSet<IPAddress> TestCaches = new HashSet<IPAddress>();
        public static Queue<IPAddress> WaitQueue = new Queue<IPAddress>();
        public static Queue<int> ThreadQueue = new Queue<int>();

        private volatile bool StdTestRunning;
        private volatile bool RndTestRunning;
        private volatile bool BndTestRunning;
        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = Resources.GoGo_logo;

            IpTable.Columns.Add(new DataColumn("addr", typeof(string))
            {
                Unique = true,
            });
            IpTable.Columns.Add(new DataColumn("port", typeof(string)));
            IpTable.Columns.Add(new DataColumn("sslc", typeof(string)));
            IpTable.Columns.Add(new DataColumn("pass", typeof(string)));
            IpTable.Columns.Add(new DataColumn("band", typeof(string)));

            dgvIpData.DataSource = IpTable;
            dgvIpData.Columns[0].Width = 160;
            dgvIpData.Columns[0].HeaderText = "地址";
            dgvIpData.Columns[1].Width = 60;
            dgvIpData.Columns[1].HeaderText = "端口";
            dgvIpData.Columns[2].Width = 60;
            dgvIpData.Columns[2].HeaderText = "状态";
            dgvIpData.Columns[3].Width = 40;
            dgvIpData.Columns[3].HeaderText = "计数";
            dgvIpData.Columns[4].Width = 80;
            dgvIpData.Columns[4].HeaderText = "速度";

            StdTestTimer.Interval = 200;
            StdTestTimer.Elapsed += StdTestTimerElapsed;

            RndTestTimer.Interval = 200;
            RndTestTimer.Elapsed += RndTestTimerElapsed;

            BndTestTimer.Interval = 200;
            BndTestTimer.Elapsed += BndTestTimer_Elapsed;

            LoadTestCache();
            LoadPools();

            Watch.Start();
        }

        private void LoadPools()
        {
            PoolDic.Add("@Inner", IpPool.CreateFromText(Resources.InnerIpSet));
            var fns = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.ip.txt");

            foreach (var fn in fns)
                using (var sr = File.OpenText(fn))
                {
                    var pool = IpPool.CreateFromText(sr.ReadToEnd());
                    if (pool.Count > 0)
                        PoolDic.Add(Path.GetFileNameWithoutExtension(fn), pool);
                }


            cbPools.DataSource = PoolDic.Keys.ToArray();
            cbPools.SelectedIndex = 0;
        }

        private void StdTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var threadCount = ThreadQueue.Count;
            var waitCount = WaitQueue.Count;

            SetStdProgress(threadCount, waitCount);

            if (StdTestRunning && waitCount > 0 && threadCount < Config.MaxThreads)
            {
                var addr = WaitQueue.Dequeue();
                new Thread(() =>
                {
                    EnCount(ThreadQueue);
                    SetTestResult(TestProcess(new TestInfo(addr)));
                    DeCount(ThreadQueue);
                }).Start();
            }
            else if (waitCount == 0 && threadCount == 0)
            {
                StdTestTimer.Stop();
                if (StdTestRunning)
                    PlaySound();
                StopTest();
            }
        }

        private void RndTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var threadCount = ThreadQueue.Count;
            var waitCount = dgvIpData.RowCount;
            var testedCount = TestCaches.Count;

            SetRndProgress(threadCount, waitCount, testedCount);

            if (RndTestRunning && waitCount < Form2.RandomNumber && threadCount < Config.MaxThreads)
            {
                IPAddress addr;
                Monitor.Enter(TestCaches);
                var loops = 0;
                do
                {
                    addr = CurPool.GetRandomIp();
                    loops++;
                    if (loops > 1000000)
                    {
                        if (RndTestRunning)
                            MessageBox.Show("发现死循环，测试终止！", "Error");

                        Monitor.Exit(TestCaches);
                        RndTestRunning = false;
                        return;
                    }
                } while (!TestCaches.Add(addr));
                Monitor.Exit(TestCaches);

                new Thread(() =>
                {
                    EnCount(ThreadQueue);

                    var info = TestProcess(new TestInfo(addr));

                    if (info.HttpOk)
                    {
                        ImportIp(addr);
                        SetTestResult(info);
                    }
                    DeCount(ThreadQueue);

                }).Start();
            }
            else if (threadCount == 0)
            {
                RndTestTimer.Stop();
                if (RndTestRunning)
                    PlaySound();
                StopTest();
                SaveTestCache();
            }
        }
        private void BndTestTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var threadCount = ThreadQueue.Count;
            var waitCount = WaitQueue.Count;

            SetStdProgress(threadCount, waitCount);

            if (BndTestRunning && waitCount > 0 && threadCount < 1)
            {
                var addr = WaitQueue.Dequeue();
                new Thread(() =>
                {
                    EnCount(ThreadQueue);
                    SetBandResult(TestBandwidth(new TestInfo(addr)));
                    DeCount(ThreadQueue);
                }).Start();
            }
            else if (waitCount == 0 && threadCount == 0)
            {
                BndTestTimer.Stop();
                if (BndTestRunning)
                    PlaySound();
                StopTest();
            }
        }

        private void PlaySound()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(PlaySound));
            else
                SoundPlayer.Play();
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

        private Socket GetSocket(TestInfo info, int m = 1)
        {
            var socket = new Socket(info.Target.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = Config.ConnTimeout * m,
                ReceiveTimeout = Config.ConnTimeout * m
            };
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

            return socket;
        }

        private TestInfo TestBandwidth(TestInfo info)
        {
            var m = 2;
            var socket = GetSocket(info, m);

            try
            {
                if (socket.BeginConnect(info.Target, null, null).AsyncWaitHandle.WaitOne(Config.ConnTimeout) && socket.Connected)
                {
                    using (var nets = new NetworkStream(socket))
                    {
                        using (var ssls = new SslStream(nets, false, (a, b, c, d) => true))
                        {
                            ssls.AuthenticateAsClient(string.Empty);
                            if (ssls.IsAuthenticated)
                            {
                                var data = Encoding.UTF8.GetBytes(string.Format("GET /git/{0}.bmp HTTP/1.1\r\nHost: gogo-tester.googlecode.com\r\nConnection: Close\r\n\r\n", Config.FileSize));

                                var time = Watch.ElapsedMilliseconds;

                                ssls.Write(data, 0, data.Length);
                                ssls.Flush();
                                using (var sr = new StreamReader(ssls))
                                {
                                    var buf = sr.ReadToEnd();
                                    if (buf.Length < 1000000)
                                        info.Bandwidth = buf.Substring(9, buf.IndexOf("\r"));
                                    else
                                        info.Bandwidth =
                                            (buf.Length / (Watch.ElapsedMilliseconds - time)).ToString("D0") + " KB/s";
                                }
                            }
                            else
                            {
                                info.Bandwidth = "SslInvalid";
                            }
                        }
                    }
                }
                else
                {
                    info.Bandwidth = "Timeout";
                }
            }
            catch (Exception ex)
            {
                info.Bandwidth = "Error: " + ex.Message;
            }

            socket.Close();
            return info;
        }

        private TestInfo TestProcess(TestInfo info)
        {
            do
            {
                using (var socket = GetSocket(info))
                {
                    if (TestPortViaSocket(socket, info) && TestHttpViaSocket(socket, info))
                    {
                        info.PassCount++;
                        if (info.PassCount < Config.PassCount)
                            Thread.Sleep(Config.ConnTimeout / 2);
                    }
                    else
                        break;
                }
            } while (info.PassCount < Config.PassCount);

            return info;
        }
        private bool TestPortViaSocket(Socket socket, TestInfo info)
        {
            if (socket.Connected)
                return true;

            var time = Watch.ElapsedMilliseconds;
            try
            {
                if (socket.BeginConnect(info.Target, null, null).AsyncWaitHandle.WaitOne(Config.ConnTimeout)
                    && socket.Connected)
                {

                    info.PortTime += Watch.ElapsedMilliseconds - time;
                    info.PortOk = true;
                    info.PortMsg = "_OK ";
                }
                else
                {
                    info.PortOk = false;
                    info.PortMsg = "Timeout";
                }
            }
            catch (Exception ex)
            {
                info.PortOk = false;
                info.PortMsg = "Error: " + ex.Message;
            }

            return info.PortOk;
        }


        private static readonly Regex RxIsGgcIp = new Regex(@"CN=(\*\.)?google\.com", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        private bool TestHttpViaSocket(Socket socket, TestInfo info)
        {
            var time = Watch.ElapsedMilliseconds;
            try
            {
                using (var nets = new NetworkStream(socket))
                {
                    using (var ssls = new SslStream(nets, false, (sender, cert, chain, sslpe) => RxIsGgcIp.IsMatch(cert.Subject)))
                    {
                        ssls.AuthenticateAsClient(string.Empty);
                        if (ssls.IsAuthenticated)
                        {
                            info.HttpTime += (Watch.ElapsedMilliseconds - time);
                            info.HttpOk = true;
                            var data = Encoding.UTF8.GetBytes(string.Format("HEAD /_gh HTTP/1.1\r\nHost: azzvxgoagent{0}.appspot.com\r\nConnection: close\r\n\r\n", Rand.Next(7)));

                            ssls.Write(data);
                            ssls.Flush();

                            using (var sr = new StreamReader(ssls))
                            {
                                var code = sr.ReadToEnd().Substring(9, 3);
                                if (code == "200")//|| code == "301" || code == "302")
                                    info.HttpMsg = "_OK ";
                                else
                                    info.HttpMsg = "_OK?";
                            }
                        }
                        else
                        {
                            info.HttpOk = false;
                            info.HttpMsg = "SslInvalid";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                info.HttpOk = false;
                info.HttpMsg = "Error: " + ex.Message;
            }

            return info.HttpOk;
        }

        private void SetTestResult(TestInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetTestResult(info)));
            }
            else
            {
                var rows = SelectByIp(info.Target.Address);
                if (rows.Length > 0)
                {
                    rows[0][1] = info.PortMsg + (info.PortOk ? (info.PortTime / (info.PassCount == 0 ? 1 : info.PassCount)).ToString("D4") : "");
                    rows[0][2] = info.HttpMsg + (info.HttpOk ? (info.HttpTime / (info.PassCount == 0 ? 1 : info.PassCount)).ToString("D4") : "");
                    rows[0][3] = info.PassCount.ToString();
                }
            }
        }
        private void SetBandResult(TestInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetBandResult(info)));
            }
            else
            {
                var rows = SelectByIp(info.Target.Address);
                if (rows.Length > 0)
                    rows[0][4] = info.Bandwidth;
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
                    row[3] = "n/a";
                    row[4] = "n/a";
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
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByExpr(expr, order)));
            else
                return IpTable.Select(expr, order);
        }

        private DataRow[] SelectByIp(IPAddress addr)
        {
            if (InvokeRequired)
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByIp(addr)));
            else
                return IpTable.Select(string.Format("addr = '{0}'", addr));
        }

        private DataRow[] SelectNa(string coln)
        {
            if (InvokeRequired)
                return (DataRow[])Invoke(new MethodInvoker(() => SelectNa(coln)));
            else
                return IpTable.Select(string.Format("{0} = 'n/a'", coln));
        }
        private void SetAllNa()
        {
            foreach (var row in IpTable.Select())
                row[4] = row[3] = row[2] = row[1] = "n/a";
        }
        private void SetNa(string coln)
        {
            foreach (var row in IpTable.Select())
                row[coln] = "n/a";
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
            if (StdTestRunning || RndTestRunning || BndTestRunning)
            {
                MessageBox.Show("有测试正在进行，无法继续操作！");
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

            var str = tbIpRange.Text;
            tbIpRange.ResetText();
            if (str == "") return;

            var pool = IpPool.CreateFromText(str);
            if (pool.Count == 0) return;
            pool.Copes.ForEach(t =>
            {
                for (uint i = t.Item1; i <= t.Item2; i++)
                    ImportIp(new IPAddress(i.GetRevBytes()));
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTest();
            while (ThreadQueue.Count > 0)
                Application.DoEvents();
        }

        private void mImportIpsInClipbord_Click(object sender, EventArgs e)
        {
            if (IsTesting()) return;

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
                ImportIp(ip);
        }

        private void mBandTest_Click(object sender, EventArgs e)
        {
            if (IsTesting() || IpTable.Rows.Count == 0) return;

            WaitQueue.Clear();

            var rows = SelectNa("band");

            if (rows.Length == 0)
                if (MessageBox.Show(this, "没有发现未测试的IP！是否重复测试已测试的IP？", "请确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    SetNa("band");
                else
                    return;

            rows = SelectNa("band");

            pbProgress.Maximum = rows.Length;
            pbProgress.Value = 0;

            foreach (var row in rows)
                WaitQueue.Enqueue(IPAddress.Parse(row[0].ToString()));

            BndTestRunning = true;
            BndTestTimer.Start();
            tlpConfig.Enabled = false;
        }
        private void mRndTest_Click(object sender, EventArgs e)
        {
            if (IsTesting()) return;

            var form = new Form2();
            form.ShowDialog(this);

            if (Form2.RandomNumber == 0)
                return;

            Form2.RandomNumber = Form2.RandomNumber > CurPool.Count ? CurPool.Count : Form2.RandomNumber;

            pbProgress.Maximum = Form2.RandomNumber;
            pbProgress.Value = 0;

            RndTestRunning = true;
            RndTestTimer.Start();

            tlpConfig.Enabled = false;
        }
        private void mStdTest_Click(object sender, EventArgs e)
        {
            if (IsTesting() || IpTable.Rows.Count == 0) return;

            WaitQueue.Clear();

            var rows = SelectNa("port");

            if (rows.Length == 0)
                if (MessageBox.Show(this, "没有发现未测试的IP！是否重复测试已测试的IP？", "请确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    SetAllNa();
                else
                    return;

            rows = SelectNa("port");

            pbProgress.Maximum = rows.Length;
            pbProgress.Value = 0;

            foreach (var row in rows)
                WaitQueue.Enqueue(IPAddress.Parse(row[0].ToString()));

            StdTestRunning = true;
            StdTestTimer.Start();
            tlpConfig.Enabled = false;
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
            Config.ConnTimeout = Convert.ToInt32(nPingTimeout.Value);
        }

        private void nMaxTest_ValueChanged(object sender, EventArgs e)
        {
            Config.MaxThreads = Convert.ToInt32(nMaxThreads.Value);
            StdTestTimer.Interval = (1000.0 / Config.MaxThreads);
            RndTestTimer.Interval = (1000.0 / Config.MaxThreads);
        }
        private void nTestCount_ValueChanged(object sender, EventArgs e)
        {
            Config.PassCount = Convert.ToInt32(nTestCount.Value);
        }


        private void mStopTest_Click(object sender, EventArgs e)
        {
            StopTest();
        }

        private void StopTest()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(StopTest));
            }
            else
            {
                StdTestRunning = RndTestRunning = BndTestRunning = false;
                tlpConfig.Enabled = true;
            }
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

            inifile.WriteFile();

            MessageBox.Show("已写入proxy.user.ini！重新载入GoAgent就可生效！");
        }

        private string[] GetValidIps()
        {
            var rows = SelectByExpr(string.Format("sslc like '_OK%'"), "sslc asc");
            return rows.Select(row => row[0].ToString()).ToArray();
        }

        private DataRow[] GetInvalidIps()
        {
            return SelectByExpr(string.Format("port <> 'n/a' and port not like '_OK%'"));
        }

        private IPAddress[] GetIpsInText(string str)
        {
            var ls = new List<IPAddress>();
            var hset = new HashSet<string>();
            var mcv4 = RxMatchIPv4.Matches(str);
            foreach (var m in from Match m in mcv4 where hset.Add(m.Value) select m)
            {
                try { ls.Add(IPAddress.Parse(m.Value)); }
                catch (Exception) { }
            }

            hset.Clear();
            var mcv6 = RxMatchIPv6.Matches(str);
            foreach (var m in from Match m in mcv6 where hset.Add(m.Value) select m)
            {
                try { ls.Add(IPAddress.Parse(m.Value)); }
                catch (Exception) { }
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
            if (IsTesting()) return;

            foreach (var row in GetInvalidIps())
                IpTable.Rows.Remove(row);
        }

        private void linkLabel1_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        private void SaveTestCache()
        {
            using (var fs = File.Create("gogo_cache", 25000 * 4))
            {
                var count = 0;
                foreach (var data in TestCaches.Select(addr => addr.GetAddressBytes()).Where(data => data.Length == 4))
                {
                    fs.Write(data, 0, data.Length);
                    count++;

                    if (count >= 25000)
                    {
                        fs.Flush();
                        count = 0;
                    }
                }

                if (count > 0)
                    fs.Flush();
            }
        }

        private void LoadTestCache()
        {
            if (!File.Exists("gogo_cache")) return;

            if (File.GetCreationTime("gogo_cache").AddDays(7) < DateTime.Now)
            {
                File.Delete("gogo_cache");
                return;
            }

            using (var fs = File.OpenRead("gogo_cache"))
            {
                var buf = new byte[4];
                for (int i = 0; i < (fs.Length / 4); i++)
                {
                    fs.Read(buf, 0, 4);
                    TestCaches.Add(new IPAddress(buf));
                }
            }

        }
        private void mClearRndCache_Click(object sender, EventArgs e)
        {
            if (IsTesting())
                return;

            TestCaches.Clear();
            if (File.Exists("gogo_cache"))
                File.Delete("gogo_cache");
        }
        private void dgvIpData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var bounds = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvIpData.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvIpData.RowHeadersDefaultCellStyle.Font, bounds, dgvIpData.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void cbFileSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.FileSize = cbFileSize.Text;
        }

        private void cbPools_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurPool = PoolDic[cbPools.SelectedItem.ToString()];
            Text = string.Format("GoGo Tester {0} - {1}", Application.ProductVersion, CurPool.Count);
            SetStdProgress(CurPool.Count, TestCaches.Count);
        }
    }
}

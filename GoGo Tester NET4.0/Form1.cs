using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace GoGo_Tester_NET4._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Regex _rxIp =
            new Regex(@"((25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})",
                RegexOptions.Compiled);

        private readonly DataTable _ipTable = new DataTable();

        private readonly Queue<string> _pingAddrQueue = new Queue<string>();
        private readonly List<object> _pingList = new List<object>();

        private int GetPingListCount()
        {
            Monitor.Enter(_pingList);
            int count = _pingList.Count;
            Monitor.Exit(_pingList);
            return count;
        }

        private int _maxPingCount = 20;
        private int _testTimeout = 6000;
        private int _pingTimeout = 1000;
        private readonly Timer _pingQueueTimer = new Timer();
        private string _gaipaddr;
        private int _gaport;
        public static string IpList;

        private void Form1_Load(object sender, EventArgs e)
        {
            _pingQueueTimer.Interval = 80;
            _pingQueueTimer.Elapsed += PingQueueTimerElapsed;

            _ipTable.Columns.Add(new DataColumn("IP", typeof(string))
            {
                Caption = "IP",
                Unique = true,
            });
            _ipTable.Columns.Add(new DataColumn("Ping", typeof(string))
            {
                Caption = "Ping"
            });
            _ipTable.Columns.Add(new DataColumn("Tcp80", typeof(string))
            {
                Caption = "Tcp:80"
            });
            _ipTable.Columns.Add(new DataColumn("Tcp443", typeof(string))
            {
                Caption = "Tcp:443"
            });
            _ipTable.Columns.Add(new DataColumn("SslStatus", typeof(string))
            {
                Caption = "SslStatus"
            });
            _ipTable.Columns.Add(new DataColumn("GoAgent", typeof(string))
            {
                Caption = "GoAgent"
            });

            dgvIpData.DataSource = _ipTable;
            dgvIpData.Columns[0].Width = 100;
            dgvIpData.Columns[1].Width = 60;
            dgvIpData.Columns[2].Width = 60;
            dgvIpData.Columns[3].Width = 60;
            dgvIpData.Columns[4].Width = 100;
            dgvIpData.Columns[5].Width = 100;
        }

        private void Ping(string addr)
        {
            using (var ping = new Ping())
            {
                Monitor.Enter(_pingList);
                _pingList.Add(ping);
                Monitor.Exit(_pingList);

                try
                {
                    var reply = ping.Send(addr, _pingTimeout);
                    if (reply.Status == IPStatus.Success)
                    {
                        SetPingResult(addr, "_OK " + reply.RoundtripTime.ToString("D4"));
                    }
                    else if (reply.Status == IPStatus.TimedOut)
                    {
                        SetPingResult(addr, "Timeout");
                    }
                    else
                    {
                        SetPingResult(addr, "Falied");
                    }

                }
                catch (Exception)
                {
                    SetPingResult(addr, "Falied");
                }

                Monitor.Enter(_pingList);
                _pingList.Remove(ping);
                Monitor.Exit(_pingList);
            }
        }

        private void TcpPing(string addr, int port)
        {
            var tcpok = false;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var stopwatch = new Stopwatch();
            try
            {
                socket.BeginConnect(addr, port, ar =>
                {
                    try
                    {
                        socket.EndConnect(ar);
                        if (socket.Connected)
                        {
                            SetTcpPingResult(addr, port,
                                "_OK " + stopwatch.ElapsedMilliseconds.ToString("D4"));

                            tcpok = true;
                        }
                        else
                        {
                            SetTcpPingResult(addr, port, "Failed");
                        }
                    }
                    catch (Exception ex)
                    {
                        SetTcpPingResult(addr, port, "Failed");
                    }
                    finally
                    {
                        stopwatch.Stop();
                        socket.Close();

                        if (tcpok && port == 443 && cbSslStatus.Checked)
                        {
                            GetSslStatus(addr);
                        }

                        Monitor.Enter(_pingList);
                        _pingList.Remove(socket);
                        Monitor.Exit(_pingList);
                    }

                }, null);

                Monitor.Enter(_pingList);
                _pingList.Add(socket);
                Monitor.Exit(_pingList);

                stopwatch.Start();

                Thread.Sleep(_pingTimeout);

                if (!socket.Poll(0, SelectMode.SelectWrite))
                {
                    SetTcpPingResult(addr, port, "Timeout");
                    stopwatch.Stop();
                    socket.Close();

                    Monitor.Enter(_pingList);
                    _pingList.Remove(socket);
                    Monitor.Exit(_pingList);
                }

            }
            catch (Exception ex)
            {
                SetTcpPingResult(addr, port, "Failed");
                socket.Close();
            }
        }


        private static Regex rxCName = new Regex(@"(?:CN=)(.+?)(?:,|$)");
        private void GetSslStatus(string addr)
        {
            var cname = new StringBuilder();

            bool cnameAdded = false;

            ServicePointManager.ServerCertificateValidationCallback = (o, certificate, chain, errors) =>
            {
                var m = rxCName.Match(certificate.Subject);

                if (!cnameAdded && m.Success)
                {
                    cnameAdded = true;
                    cname.Append(m.Groups[1].Value);
                }
                return true;
            };

            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://" + addr);
                request.Timeout = _testTimeout;
                var resp = (HttpWebResponse)request.GetResponse();
                cname.Insert(0, "_" + resp.StatusCode + " ");
                resp.Close();
            }
            catch (Exception)
            {
                cname.Insert(0, "Failed ");
            }

            if (cname.Length > 0)
            {
                SetSslStatus(addr, cname.ToString());
            }
        }


        private delegate void SetTcpResultHandler(string addr, int port, string result);
        private void SetTcpPingResult(string addr, int port, string result)
        {
            if (InvokeRequired)
            {
                Invoke(new SetTcpResultHandler(SetTcpPingResult), new object[] { addr, port, result });
            }
            else
            {
                var rows = SelectByIP(addr);
                if (rows.Length > 0 && rows[0]["Tcp" + port].ToString() == "n/a")
                    rows[0]["Tcp" + port] = result;
            }
        }

        private delegate void SetResultHandler(string addr, string result);
        private void SetPingResult(string addr, string result)
        {
            if (InvokeRequired)
            {
                Invoke(new SetResultHandler(SetPingResult), new object[] { addr, result });
            }
            else
            {
                var rows = SelectByIP(addr); ;
                if (rows.Length > 0 && rows[0]["Ping"].ToString() == "n/a")
                    rows[0]["Ping"] = result;
            }
        }
        private void SetSslStatus(string addr, string cname)
        {
            if (InvokeRequired)
            {
                Invoke(new SetResultHandler(SetSslStatus), new object[] { addr, cname });
            }
            else
            {
                var rows = SelectByIP(addr);
                if (rows.Length > 0 && rows[0]["SslStatus"].ToString() == "n/a")
                    rows[0]["SslStatus"] = cname;
            }
        }


        private void SetTestResult(string addr, string result)
        {
            if (InvokeRequired)
            {
                Invoke(new SetResultHandler(SetTestResult), new object[] { addr, result });
            }
            else
            {
                var rows = SelectByIP(addr);
                if (rows.Length > 0 && rows[0]["GoAgent"].ToString() == "n/a")
                    rows[0]["GoAgent"] = result;
            }
        }


        private delegate void LogHandler(string str, bool newline);

        private void Log(string str, bool newline = true)
        {
            if (InvokeRequired)
            {
                Invoke(new LogHandler(Log), new object[] { str, newline });
            }
            else
            {
                Console.Write(str);
                tbLog.Text += str;

                if (newline)
                {
                    Console.Write("\r\n");
                    tbLog.Text += "\r\n";
                }
            }
        }

        private delegate void LogVoidHandler();
        private void LogClear()
        {
            if (InvokeRequired)
            {
                Invoke(new LogVoidHandler(LogClear));
            }
            else
            {
                tbLog.ResetText();
            }
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            var str = "";
            try
            {
                str = Clipboard.GetText();
            }
            catch (Exception)
            {
                MessageBox.Show("剪切板貌似出问题了，再试一次吧！");
                return;
            }

            if (str == null || str == "")
            {
                MessageBox.Show("剪切板是空的！");
                return;
            }

            var mc = _rxIp.Matches(str);
            var countwait = 0;
            foreach (Match m in mc)
            {
                AddIp(m.Value);
                if (countwait > 100)
                {
                    Application.DoEvents();
                }
                countwait++;
            }

            if (cbAutoTest.Checked)
                _pingQueueTimer.Start();
        }

        private void AddIp(string addr)
        {
            try
            {
                var row = _ipTable.NewRow();
                row[0] = addr;
                row[1] = "n/a";
                row[2] = "n/a";
                row[3] = "n/a";
                row[4] = "n/a";
                row[5] = "n/a";
                _ipTable.Rows.Add(row);

                _pingAddrQueue.Enqueue(addr);
            }
            catch (Exception) { }
        }

        private delegate void StringHandler(string str);
        private void SetText(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new StringHandler(SetText), str);
            }
            else
            {
                Text = str;
            }
        }


        private void PingQueueTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var pingListCount = GetPingListCount();
            var pingAddrQueueCount = _pingAddrQueue.Count;

            SetText("GoGo Tester 2 - 正在测试 " + pingListCount + "/" + pingAddrQueueCount);

            if (pingAddrQueueCount > 0 && pingListCount < _maxPingCount)
            {
                var addr = _pingAddrQueue.Dequeue();

                if (cbPing.Checked)
                {
                    var pingThread = new Thread(() => Ping(addr));
                    pingThread.Start();
                }

                if (cbTcp80.Checked)
                {
                    var ping80Thread = new Thread(() => TcpPing(addr, 80));
                    ping80Thread.Start();
                }

                if (cbTcp443.Checked)
                {
                    var ping443Thread = new Thread(() => TcpPing(addr, 443));
                    ping443Thread.Start();
                }

            }
            else if (pingAddrQueueCount == 0 && pingListCount == 0)
            {
                _pingQueueTimer.Stop();
                SetText("GoGo Tester 2");
            }

        }

        private string _testingAddr;
        private IniFile _proxyIniFile;

        private bool _testComplete;
        private StreamWriter _autoSaveSW;
        private void bTest_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "SslStatus 一栏表示为 _OK 的 IP 基本可用，是否进行进一步测试？", "请确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            if (!File.Exists("proxy.bat"))
            {
                MessageBox.Show("没有找到'proxy.bat'，无法开启测试！请将本程序放入GoAgent目录！");
                return;
            }

            /////////////
            if (bGaTest.Text == "正在停止...")
                return;

            if (bGaTest.Text == "停止测试")
            {
                _stopTest = true;
                bGaTest.Text = "正在停止...";
                return;
            }

            try
            {
                var req = (HttpWebRequest)WebRequest.Create(tbTestUrl.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("无效的测试网址！");
                return;
            }

            while (!KillGa())
            {
                MessageBox.Show("请关闭当前运行的GoAgent！");
            }

            _stopTest = false;


            File.Copy("proxy.ini", "proxy.ini.bak", true);
            if (cbAutoSave.Checked)
            {
                File.Delete("validips.txt");
                File.WriteAllText("validips.txt", "GoGo Tester 2 自动保存\r\n");
                _autoSaveSW = File.AppendText("validips.txt");
                _autoSaveSW.AutoFlush = true;
            }


            _proxyIniFile = new IniFile("proxy.ini");

            _gaipaddr = _proxyIniFile.ReadValue("listen", "ip");
            _gaport = Convert.ToInt32(_proxyIniFile.ReadValue("listen", "port"));

            _proxy = new WebProxy(_gaipaddr, _gaport);

            bGaTest.Text = "停止测试";

            bImport.Enabled = bApply.Enabled = bClear.Enabled = bExportValid.Enabled = tbTestUrl.Enabled = tbTimeout.Enabled = tbMaxSockets.Enabled = tbTestTimeout.Enabled = cbAutoSave.Enabled = bExportSelect.Enabled = bDelete.Enabled = bTest.Enabled = false;

            _testComplete = false;
            var testThread = new Thread(TestLoop);

            testThread.Start();
            while (!_testComplete)
            {
                Application.DoEvents();
            }

            bGaTest.Text = "GoAgent测试";

            bImport.Enabled =
             bApply.Enabled = bClear.Enabled = bExportValid.Enabled = tbTestUrl.Enabled = tbTimeout.Enabled = tbMaxSockets.Enabled = tbTestTimeout.Enabled = cbAutoSave.Enabled = bExportSelect.Enabled = bDelete.Enabled = bTest.Enabled = true;

            KillGa();

            if (cbAutoSave.Checked)
            {
                _autoSaveSW.Close();
            }


            File.Copy("proxy.ini.bak", "proxy.ini", true);

            if (cbShutdown.Checked)
            {
                if (GetIpList())
                {
                    File.WriteAllText("gogoiplist.txt", IpList);
                }
                Process.Start("Shutdown", "/s /t 60 /f");
                Close();
            }
        }

        private bool GetIpList()
        {
            DataRow[] rows;

            if (cbTcp80.Checked)
            {
                rows = _ipTable.Select("SslStatus like '_OK%' or GoAgent like '_OK%'", "Tcp80 asc");
            }
            else if (cbTcp443.Checked)
            {
                rows = _ipTable.Select("SslStatus like '_OK%' or GoAgent like '_OK%'", "Tcp443 asc");
            }
            else if (cbPing.Checked)
            {
                rows = _ipTable.Select("SslStatus like '_OK%' or GoAgent like '_OK%'", "Ping asc");
            }
            else
            {
                rows = new DataRow[] { };
            }

            if (rows.Length > 0)
            {
                IpList = rows[0][0].ToString();

                for (int i = 1; i < rows.Length; i++)
                {
                    IpList += "|" + rows[i][0];
                }
                return true;
            }
            else
            {
                MessageBox.Show("没有有效的IP!");
                return false;
            }
        }

        private bool _stopTest;
        private bool _isTesting;
        private readonly Queue<DataRow> _testQueue = new Queue<DataRow>();
        private WebProxy _proxy;
        private readonly Stopwatch _testStopwatch = new Stopwatch();

        private delegate DataRow[] SelectHandler(string expr);
        private DataRow[] SelectByExpr(string expr)
        {
            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new SelectHandler(SelectByExpr), expr);

            }
            else
            {
                return _ipTable.Select(expr);
            }
        }
        private DataRow[] SelectByIP(string addr)
        {
            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new SelectHandler(SelectByIP), addr);
            }
            else
            {
                return _ipTable.Select(string.Format("IP = '{0}'", addr));
            }
        }



        private void TestLoop()
        {
            _isTesting = false;
            _testQueue.Clear();

            foreach (var row in SelectByExpr("GoAgent = 'n/a' and (Tcp443 like '_OK%' or Tcp80 like '_OK%')"))
                _testQueue.Enqueue(row);

            while (!_stopTest && (_testQueue.Count > 0 || GetPingListCount() > 0))
            {
                if (!_isTesting && _testQueue.Count > 0)
                {
                    _testingAddr = _testQueue.Peek()[0].ToString();

                    _isTesting = true;

                    _proxyIniFile.WriteValue("iplist", "google_hk", " " + _testingAddr);
                    _proxyIniFile.WriteValue("iplist", "google_cn", " " + _testingAddr);
                    _proxyIniFile.WriteValue("iplist", "google_talk", " " + _testingAddr);
                    _proxyIniFile.WriteValue("iplist", "google_ipv6", " " + _testingAddr);

                    _proxyIniFile.WriteValue("iplist", "mtalk.google.com", " " + _testingAddr);
                    _proxyIniFile.WriteFile();

                    KillGa();
                    WaitForGAClose();

                    LogClear();
                    Log("等待测试 " + _testingAddr);

                    StartGa();
                    WaitForGAStart();

                    Log("开始测试 " + _testingAddr);

                    var request = (HttpWebRequest)WebRequest.Create(tbTestUrl.Text);
                    request.AllowAutoRedirect = false;
                    request.Timeout = _testTimeout;
                    request.Proxy = _proxy;

                    _testStopwatch.Reset();
                    _testStopwatch.Start();
                    try
                    {
                        var resp = (HttpWebResponse)request.GetResponse();

                        SetTestResult(_testingAddr, "_OK " + _testStopwatch.ElapsedMilliseconds.ToString("D4") + " " + resp.StatusCode);

                        resp.Close();

                        if (cbAutoSave.Checked)
                        {
                            _autoSaveSW.Write(_testingAddr + "|");
                        }
                    }
                    catch (Exception ex)
                    {
                        SetTestResult(_testingAddr, "Invalid");
                    }
                    EndTest();
                }

                if (_testQueue.Count == 0)
                {
                    foreach (var row in SelectByExpr("GoAgent = 'n/a' and (Tcp443 like '_OK%' or Tcp80 like '_OK%')"))
                        _testQueue.Enqueue(row);

                    if (_testQueue.Count == 0 && GetPingListCount() == 0)
                        break;
                }

                Application.DoEvents();
            }
            _testComplete = true;
        }

        private void EndTest()
        {
            _testQueue.Dequeue();
            _isTesting = false;
        }

        private void StartGa()
        {
            Log("启动GoAgent");
            Process.Start("proxy.bat");
        }
        private bool KillGa()
        {
            Log("关闭GoAgent");

            var procs = new List<Process>();
            try
            {
                procs.AddRange(Process.GetProcessesByName("goagent"));
                procs.AddRange(Process.GetProcessesByName("goagent-win8"));
                procs.AddRange(Process.GetProcessesByName("python27"));

                for (int i = 0; i < procs.Count; i++)
                {
                    if (procs[i].MainModule.FileVersionInfo.FileDescription.Contains("goagent"))
                    {
                        procs[i].Kill();
                    }

                    procs[i].Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool FindGa()
        {
            new List<Process>();
            try
            {
                var procs = Process.GetProcessesByName("python27");

                for (int i = 0; i < procs.Length; i++)
                {
                    if (procs[i].MainModule.FileVersionInfo.FileDescription.Contains("goagent"))
                    {
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool _isGaSocketOpened;
        private bool _isGaSocketConnecting;
        private int _isFindGaDelay;
        void WaitForGAStart()
        {
            Log("等待GoAgent启动", false);
            _isGaSocketOpened = false;
            _isGaSocketConnecting = false;
            _isFindGaDelay = 0;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (!_isGaSocketOpened && !_stopTest)
            {
                if (_isFindGaDelay == 10)
                {
                    if (!FindGa())
                    {
                        break;
                    }

                    _isFindGaDelay = 0;
                }
                _isFindGaDelay++;

                Log(".", false);
                Application.DoEvents();
                Thread.Sleep(200);
                if (_isGaSocketConnecting)
                    continue;

                try
                {
                    socket.BeginConnect(_gaipaddr, _gaport,
                        ar =>
                        {
                            try
                            {
                                _isGaSocketConnecting = false;
                                socket.EndConnect(ar);
                                _isGaSocketOpened = socket.Connected;
                            }
                            catch (Exception) { }
                        }, null);
                    _isGaSocketConnecting = true;
                }
                catch (Exception) { }
            };
            socket.Close();
            Log("\r\nGoAgent已启动");
        }

        private void WaitForGAClose()
        {
            Log("等待GoAgent关闭", false);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (!_stopTest)
            {
                Log(".", false);
                Application.DoEvents();
                Thread.Sleep(200);
                try
                {
                    socket.Connect(_gaipaddr, _gaport);
                    socket.Disconnect(true);
                }
                catch (Exception) { break; }
            };
            socket.Close();
            Log("\r\nGoAgent已关闭");
        }

        private void tbTimeout_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _pingTimeout = Convert.ToInt32(tbTimeout.Text);
            }
            catch (Exception) { }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "确认清除？！", "确认操作", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Clear();
            }
        }

        private void Clear()
        {
            _ipTable.Clear();
            _pingAddrQueue.Clear();
            _testQueue.Clear();
        }


        private void bExport_Click(object sender, EventArgs e)
        {
            if (GetIpList())
            {
                try
                {
                    Clipboard.SetText(IpList);
                    LogClear();
                    Log("已导出到剪切板！");
                }
                catch (Exception)
                {
                    MessageBox.Show("操作剪切板失败！再试一次吧！");
                }
            }
        }

        private void bApply_Click(object sender, EventArgs e)
        {
            if (!File.Exists("proxy.ini"))
            {
                MessageBox.Show("没有找到'proxy.ini'！请将本程序放入GoAgent目录！");
                return;
            }

            if (GetIpList())
            {
                var now = DateTime.Now;
                File.Copy("proxy.ini", "proxy.ini.bak" + now.Month.ToString("D2") + now.Day.ToString("D2") + now.Hour.ToString("D2") + now.Minute.ToString("D2"), true);

                var proxyini = new IniFile("proxy.ini");

                proxyini.WriteValue("iplist", "google_hk", IpList);
                proxyini.WriteValue("iplist", "google_cn", IpList);
                proxyini.WriteFile();
                LogClear();
                Log("结果已应用！");
            }


        }

        private void tbTestTimeout_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _testTimeout = Convert.ToInt32(tbTestTimeout.Text);
            }
            catch (Exception) { }
        }

        private void tbMaxSockets_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _maxPingCount = Convert.ToInt32(tbMaxSockets.Text);
            }
            catch (Exception) { }
        }

        private void bExportSelect_Click(object sender, EventArgs e)
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

            LogClear();
            if (cells.Count > 0)
            {
                var sb = new StringBuilder(cells[0].Value.ToString());

                for (int i = 1; i < cells.Count; i++)
                {
                    sb.Append("|" + cells[i].Value);
                }

                try
                {
                    Clipboard.SetText(sb.ToString());
                }
                catch (Exception) { MessageBox.Show("操作剪切板失败！再试一次吧！"); }

                Log("已导出到剪切板！");

            }
            else
            {
                Log("没有选中的IP！");
            }



        }

        private void Tip_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as Control;

            lTip.Text = control.Tag.ToString();
        }

        private void cbTcp_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb.Checked == false)
            {
                if (cb == cbTcp443)
                    cbSslStatus.Enabled = false;

                if (cb == cbTcp80 && !cbTcp443.Checked)
                    cbTcp443.Checked = true;
                else if (cb == cbTcp443 && !cbTcp80.Checked)
                    cbTcp80.Checked = true;
            }
            else
            {
                if (cb == cbTcp443)
                    cbSslStatus.Enabled = true;
            }
        }

        private void cbPing_CheckedChanged(object sender, EventArgs e)
        {
            cbPing.Checked = true;
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            var str = "";
            try
            {
                str = Clipboard.GetText();
            }
            catch (Exception)
            {
                MessageBox.Show("剪切板貌似出问题了，再试一次吧！");
                return;
            }

            if (str == null || str == "")
            {
                MessageBox.Show("剪切板是空的！");
                return;
            }

            if (MessageBox.Show(this, "确定排除相同的的IP？！\r\n", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var mc = _rxIp.Matches(str);

                var sbd = new StringBuilder();

                foreach (Match m in mc)
                {
                    if (sbd.Length < 200)
                    {
                        sbd.Append(m.Value);
                        sbd.Append(" | ");
                    }
                    else
                    {
                        sbd.Append("...");
                        break;
                    }
                }

                foreach (Match m in mc)
                {
                    try
                    {
                        var rows = SelectByIP(m.Value);
                        if (rows.Length > 0)
                        {
                            foreach (var row in rows)
                            {
                                _ipTable.Rows.Remove(row);
                            }
                        }
                    }
                    catch (Exception) { }
                }

            }

        }

        private void bAddIpRange_Click(object sender, EventArgs e)
        {
            var str = tbIpRange.Text.Trim();
            if (str == "")
            {
                return;
            }

            var ranges = str.Split(",;，；".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var range in ranges)
            {
                var cope = range.Split(new[] { '.' });
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
                        start[i] = Convert.ToInt32(cope[i].Trim());
                        end[i] = Convert.ToInt32(cope[i].Trim());
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
                                AddIp(a + "." + b + "." + c + "." + d);
                            }
                        }
                    }
                    Application.DoEvents();
                }
            }

            if (cbAutoTest.Checked)
                _pingQueueTimer.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogClear();
            Log("正在等待所有线程退出！");

            Clear();
            while (GetPingListCount() > 0)
            {
                Application.DoEvents();
            }
        }

        private void bTest_Click_1(object sender, EventArgs e)
        {
            _pingQueueTimer.Start();
        }
    }
}

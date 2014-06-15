using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
        private List<IpRange> IpRangePool = new List<IpRange>();

        private void ImportIpRange(string range)
        {
            IpRangePool.Add(IpRange.CreateIpRange(range));
        }

        public Form1()
        {
            InitializeComponent();

            ImportIpRange("93.123.23.1-59");
            ImportIpRange("197.199.253.1-59");
            ImportIpRange("197.199.254.1-59");
            ImportIpRange("218.189.25.129-.187");
            ImportIpRange("218.253.0.76-187");
            ImportIpRange("149.126.86.1-59");
            ImportIpRange("111.92.162.4-59");
            ImportIpRange("62.201.216.196-251");
            ImportIpRange("218.176.242.4-251");
            ImportIpRange("41.84.159.12-30");
            ImportIpRange("121.78.74.68-123");
            ImportIpRange("41.206.96.1-251");
            ImportIpRange("88.159.13.196-251");
            ImportIpRange("193.90.147.0-123");
            ImportIpRange("103.25.178.4-59");
            ImportIpRange("178.45.251.4-123 ");
            ImportIpRange("84.235.77.1-251");
            ImportIpRange("213.240.44.5-27");
            ImportIpRange("203.116.165.129-255");
            ImportIpRange("203.117.34.132-187");
            ImportIpRange("62.197.198.193-251");
            ImportIpRange("87.244.198.161-187");
            ImportIpRange("123.205.250.68-190");
            ImportIpRange("123.205.251.68-123");
            ImportIpRange("163.28.116.1-59");
            ImportIpRange("163.28.83.143-187");
            ImportIpRange("202.39.143.1-123");
            ImportIpRange("203.211.0.4-59");
            ImportIpRange("203.66.124.129-251");
            ImportIpRange("210.61.221.65-187");
            ImportIpRange("60.199.175.1-187");
            ImportIpRange("61.219.131.65-251");
            ImportIpRange("118.174.25.4-251");
            ///
            ImportIpRange("210.153.73.20-59");
            ImportIpRange("210.153.73.84-123 ");
            ImportIpRange("106.162.192.148-187");
            ImportIpRange("106.162.198.84-123");
            ImportIpRange("106.162.216.20-59");
            ImportIpRange("106.162.216.84-123");
            ImportIpRange("210.139.253.20-59");
            ImportIpRange("210.139.253.84-123");
            ImportIpRange("210.139.253.148-187");
            ImportIpRange("210.139.253.212-251");
            ImportIpRange("218.176.242.20-59");
            ImportIpRange("218.176.242.84-123");
            ImportIpRange("218.176.242.148-187");
            ImportIpRange("218.176.242.212-251 ");
            ImportIpRange("111.168.255.20-59");
            ImportIpRange("111.168.255.84-123");
            ImportIpRange("111.168.255.148-187");
            ImportIpRange("203.165.13-14.210-251");
            ///
            ImportIpRange("66.185.84.0-255");
            ImportIpRange("24.156.131.0-255");
            ImportIpRange("88.159.13.0-255");
            ImportIpRange("1.179.248.0-255");
            ImportIpRange("69.17.141.0-255");
            ImportIpRange("59.78.209.0-255");
            ImportIpRange("202.106.93.0-255");
            ImportIpRange("202.69.26.0-255");
            ImportIpRange("121.194.0.0-255");
            ImportIpRange("192.86.102.0-255");
            ImportIpRange("108.166.34.0-255");
            ImportIpRange("119.147.146.0-255");
            ImportIpRange("124.160.89.0-255");
            ImportIpRange("119.57.55.0-255");
            ImportIpRange("58.240.77.0-255");
            ImportIpRange("58.205.224.0-255");
            ImportIpRange("121.195.178.0-255");
            ImportIpRange("108.166.34.0-255");
            ImportIpRange("110.75.151.0-255");
            ImportIpRange("192.193.133.0-255");
            ImportIpRange("118.174.25.0-255");
            ImportIpRange("61.219.131.0-255");
            ImportIpRange("111.92.162.0-255");
            ImportIpRange("66.185.84.0-255");
            ImportIpRange("24.156.131.0-255");
            ImportIpRange("88.159.13.0-255");
            ImportIpRange("1.179.248.0-255");
            ImportIpRange("69.17.141.0-255");
            ImportIpRange("123.205.250.0-255");
            ImportIpRange("84.235.77.0-255");
            ImportIpRange("60.199.175.0-255");
            ImportIpRange("65.55.58.0-255");
            ImportIpRange("210.61.221.0-255");
            ImportIpRange("192.30.252.0-255");
            ImportIpRange("193.90.147.0-255");
            ImportIpRange("41.206.96.0-255");
            ImportIpRange("62.201.216.0-255");
            ImportIpRange("118.174.25.0-255");
            ImportIpRange("61.219.131.0-255");
            ImportIpRange("111.92.162.0-255");
            ImportIpRange("203.117.34.0-255");
            ImportIpRange("197.199.254.0-255");
            ///
            ImportIpRange("93.123.23.0-255");
            ImportIpRange("89.207.224.0-255");
            ImportIpRange("84.235.77.0-255");
            ImportIpRange("8.8.8.0-255");
            ImportIpRange("8.8.4.0-255");
            ImportIpRange("8.6.48.0-255");
            ImportIpRange("8.35.200.0-255");
            ImportIpRange("8.35.192.0-255");
            ImportIpRange("8.34.216.0-255");
            ImportIpRange("8.34.208.0-255");
            ImportIpRange("8.22.56.0-255");
            ImportIpRange("8.15.202.0-255");
            ImportIpRange("74.125.90.0-255");
            ImportIpRange("74.125.88.0-255");
            ImportIpRange("74.125.76.0-255");
            ImportIpRange("74.125.74.0-255");
            ImportIpRange("74.125.73.0-255");
            ImportIpRange("74.125.72.0-255");
            ImportIpRange("74.125.70.0-255");
            ImportIpRange("74.125.68.0-255");
            ImportIpRange("74.125.63.0-255");
            ImportIpRange("74.125.62.0-255");
            ImportIpRange("74.125.58.0-255");
            ImportIpRange("74.125.54.0-255");
            ImportIpRange("74.125.45.0-255");
            ImportIpRange("74.125.43.0-255");
            ImportIpRange("74.125.42.0-255");
            ImportIpRange("74.125.41.0-255");
            ImportIpRange("74.125.40.0-255");
            ImportIpRange("74.125.37.0-255");
            ImportIpRange("74.125.36.0-255");
            ImportIpRange("74.125.31.0-255");
            ImportIpRange("74.125.30.0-255");
            ImportIpRange("74.125.29.0-255");
            ImportIpRange("74.125.28.0-255");
            ImportIpRange("74.125.27.0-255");
            ImportIpRange("74.125.26.0-255");
            ImportIpRange("74.125.25.0-255");
            ImportIpRange("74.125.246.0-255");
            ImportIpRange("74.125.245.0-255");
            ImportIpRange("74.125.244.0-255");
            ImportIpRange("74.125.24.0-255");
            ImportIpRange("74.125.239.0-255");
            ImportIpRange("74.125.238.0-255");
            ImportIpRange("74.125.237.0-255");
            ImportIpRange("74.125.236.0-255");
            ImportIpRange("74.125.235.0-255");
            ImportIpRange("74.125.234.0-255");
            ImportIpRange("74.125.233.0-255");
            ImportIpRange("74.125.232.0-255");
            ImportIpRange("74.125.231.0-255");
            ImportIpRange("74.125.230.0-255");
            ImportIpRange("74.125.23.0-255");
            ImportIpRange("74.125.229.0-255");
            ImportIpRange("74.125.228.0-255");
            ImportIpRange("74.125.227.0-255");
            ImportIpRange("74.125.226.0-255");
            ImportIpRange("74.125.225.0-255");
            ImportIpRange("74.125.224.0-255");
            ImportIpRange("74.125.22.0-255");
            ImportIpRange("74.125.21.0-255");
            ImportIpRange("74.125.207.0-255");
            ImportIpRange("74.125.206.0-255");
            ImportIpRange("74.125.205.0-255");
            ImportIpRange("74.125.204.0-255");
            ImportIpRange("74.125.203.0-255");
            ImportIpRange("74.125.201.0-255");
            ImportIpRange("74.125.200.0-255");
            ImportIpRange("74.125.20.0-255");
            ImportIpRange("74.125.198.0-255");
            ImportIpRange("74.125.196.0-255");
            ImportIpRange("74.125.195.0-255");
            ImportIpRange("74.125.194.0-255");
            ImportIpRange("74.125.193.0-255");
            ImportIpRange("74.125.192.0-255");
            ImportIpRange("74.125.191.0-255");
            ImportIpRange("74.125.190.0-255");
            ImportIpRange("74.125.19.0-255");
            ImportIpRange("74.125.189.0-255");
            ImportIpRange("74.125.188.0-255");
            ImportIpRange("74.125.187.0-255");
            ImportIpRange("74.125.186.0-255");
            ImportIpRange("74.125.185.0-255");
            ImportIpRange("74.125.184.0-255");
            ImportIpRange("74.125.183.0-255");
            ImportIpRange("74.125.182.0-255");
            ImportIpRange("74.125.181.0-255");
            ImportIpRange("74.125.180.0-255");
            ImportIpRange("74.125.18.0-255");
            ImportIpRange("74.125.179.0-255");
            ImportIpRange("74.125.178.0-255");
            ImportIpRange("74.125.177.0-255");
            ImportIpRange("74.125.176.0-255");
            ImportIpRange("74.125.17.0-255");
            ImportIpRange("74.125.16.0-255");
            ImportIpRange("74.125.151.0-255");
            ImportIpRange("74.125.150.0-255");
            ImportIpRange("74.125.149.0-255");
            ImportIpRange("74.125.148.0-255");
            ImportIpRange("74.125.143.0-255");
            ImportIpRange("74.125.142.0-255");
            ImportIpRange("74.125.141.0-255");
            ImportIpRange("74.125.140.0-255");
            ImportIpRange("74.125.139.0-255");
            ImportIpRange("74.125.138.0-255");
            ImportIpRange("74.125.137.0-255");
            ImportIpRange("74.125.136.0-255");
            ImportIpRange("74.125.135.0-255");
            ImportIpRange("74.125.134.0-255");
            ImportIpRange("74.125.133.0-255");
            ImportIpRange("74.125.132.0-255");
            ImportIpRange("74.125.131.0-255");
            ImportIpRange("74.125.130.0-255");
            ImportIpRange("74.125.129.0-255");
            ImportIpRange("74.125.128.0-255");
            ImportIpRange("74.125.125.0-255");
            ImportIpRange("74.125.123.0-255");
            ImportIpRange("74.125.122.0-255");
            ImportIpRange("74.125.121.0-255");
            ImportIpRange("74.125.120.0-255");
            ImportIpRange("74.125.119.0-255");
            ImportIpRange("74.125.118.0-255");
            ImportIpRange("74.125.117.0-255");
            ImportIpRange("74.125.116.0-255");
            ImportIpRange("74.125.0-255.0-255");
            ImportIpRange("72.14.252.0-255");
            ImportIpRange("72.14.244.0-255");
            ImportIpRange("72.14.228.0-255");
            ImportIpRange("72.14.226.0-255");
            ImportIpRange("72.14.225.0-255");
            ImportIpRange("72.14.208.0-255");
            ImportIpRange("72.14.202.0-255");
            ImportIpRange("72.14.199.0-255");
            ImportIpRange("72.14.192.0-255");
            ImportIpRange("70.32.158.0-255");
            ImportIpRange("70.32.148.0-255");
            ImportIpRange("70.32.146.0-255");
            ImportIpRange("70.32.144.0-255");
            ImportIpRange("70.32.142.0-255");
            ImportIpRange("70.32.140.0-255");
            ImportIpRange("70.32.134.0-255");
            ImportIpRange("70.32.133.0-255");
            ImportIpRange("70.32.132.0-255");
            ImportIpRange("70.32.131.0-255");
            ImportIpRange("70.32.130.0-255");
            ImportIpRange("70.32.128.0-255");
            ImportIpRange("66.249.93.0-255");
            ImportIpRange("66.249.92.0-255");
            ImportIpRange("66.249.91.0-255");
            ImportIpRange("66.249.90.0-255");
            ImportIpRange("66.249.89.0-255");
            ImportIpRange("66.249.88.0-255");
            ImportIpRange("66.249.85.0-255");
            ImportIpRange("66.249.84.0-255");
            ImportIpRange("66.249.83.0-255");
            ImportIpRange("66.249.82.0-255");
            ImportIpRange("66.249.81.0-255");
            ImportIpRange("66.249.80.0-255");
            ImportIpRange("66.249.79.0-255");
            ImportIpRange("66.249.78.0-255");
            ImportIpRange("66.249.77.0-255");
            ImportIpRange("66.249.76.0-255");
            ImportIpRange("66.249.75.0-255");
            ImportIpRange("66.249.74.0-255");
            ImportIpRange("66.249.73.0-255");
            ImportIpRange("66.249.72.0-255");
            ImportIpRange("66.249.71.0-255");
            ImportIpRange("66.249.70.0-255");
            ImportIpRange("66.249.69.0-255");
            ImportIpRange("66.249.68.0-255");
            ImportIpRange("66.249.67.0-255");
            ImportIpRange("66.249.66.0-255");
            ImportIpRange("66.249.65.0-255");
            ImportIpRange("66.249.64.0-255");
            ImportIpRange("66.249.64.0-255");
            ImportIpRange("66.102.4.0-255");
            ImportIpRange("66.102.3.0-255");
            ImportIpRange("66.102.2.0-255");
            ImportIpRange("66.102.0-255.0-255");
            ImportIpRange("64.9.224.0-255");
            ImportIpRange("64.9.224.0-255");
            ImportIpRange("64.233.172.0-255");
            ImportIpRange("64.233.171.0-255");
            ImportIpRange("64.233.168.0-255");
            ImportIpRange("64.233.168.0-255");
            ImportIpRange("64.233.165.0-255");
            ImportIpRange("64.233.164.0-255");
            ImportIpRange("64.233.160.0-255");
            ImportIpRange("64.233.160.0-255");
            ImportIpRange("63.243.168.0-255");
            ImportIpRange("62.201.216.0-255");
            ImportIpRange("41.84.159.0-255");
            ImportIpRange("41.206.96.0-255");
            ImportIpRange("23.255.128.0-255");
            ImportIpRange("23.251.128.0-255");
            ImportIpRange("23.236.48.0-255");
            ImportIpRange("23.228.128.0-255");
            ImportIpRange("217.163.7.0-255");
            ImportIpRange("216.58.192.0-255");
            ImportIpRange("216.239.60.0-255");
            ImportIpRange("216.239.44.0-255");
            ImportIpRange("216.239.39.0-255");
            ImportIpRange("216.239.38.0-255");
            ImportIpRange("216.239.36.0-255");
            ImportIpRange("216.239.35.0-255");
            ImportIpRange("216.239.34.0-255");
            ImportIpRange("216.239.33.0-255");
            ImportIpRange("216.239.32.0-255");
            ImportIpRange("216.239.32.0-255");
            ImportIpRange("216.21.160.0-255");
            ImportIpRange("212.188.15.0-255");
            ImportIpRange("209.85.238.0-255");
            ImportIpRange("209.85.228.0-255");
            ImportIpRange("209.85.128.0-255");
            ImportIpRange("207.223.160.0-255");
            ImportIpRange("199.223.232.0-255");
            ImportIpRange("199.192.112.0-255");
            ImportIpRange("197.199.254.0-255");
            ImportIpRange("197.199.253.0-255");
            ImportIpRange("193.186.4.0-255");
            ImportIpRange("193.142.125.0-255");
            ImportIpRange("192.200.224.0-255");
            ImportIpRange("192.178.0-255.0-255");
            ImportIpRange("192.158.28.0-255");
            ImportIpRange("192.119.21.0-255");
            ImportIpRange("192.119.20.0-255");
            ImportIpRange("192.119.16.0-255");
            ImportIpRange("173.255.112.0-255");
            ImportIpRange("173.194.99.0-255");
            ImportIpRange("173.194.98.0-255");
            ImportIpRange("173.194.97.0-255");
            ImportIpRange("173.194.96.0-255");
            ImportIpRange("173.194.79.0-255");
            ImportIpRange("173.194.78.0-255");
            ImportIpRange("173.194.77.0-255");
            ImportIpRange("173.194.76.0-255");
            ImportIpRange("173.194.75.0-255");
            ImportIpRange("173.194.74.0-255");
            ImportIpRange("173.194.73.0-255");
            ImportIpRange("173.194.72.0-255");
            ImportIpRange("173.194.71.0-255");
            ImportIpRange("173.194.70.0-255");
            ImportIpRange("173.194.69.0-255");
            ImportIpRange("173.194.68.0-255");
            ImportIpRange("173.194.67.0-255");
            ImportIpRange("173.194.66.0-255");
            ImportIpRange("173.194.65.0-255");
            ImportIpRange("173.194.64.0-255");
            ImportIpRange("173.194.47.0-255");
            ImportIpRange("173.194.46.0-255");
            ImportIpRange("173.194.45.0-255");
            ImportIpRange("173.194.44.0-255");
            ImportIpRange("173.194.43.0-255");
            ImportIpRange("173.194.42.0-255");
            ImportIpRange("173.194.41.0-255");
            ImportIpRange("173.194.40.0-255");
            ImportIpRange("173.194.39.0-255");
            ImportIpRange("173.194.38.0-255");
            ImportIpRange("173.194.37.0-255");
            ImportIpRange("173.194.36.0-255");
            ImportIpRange("173.194.35.0-255");
            ImportIpRange("173.194.34.0-255");
            ImportIpRange("173.194.33.0-255");
            ImportIpRange("173.194.32.0-255");
            ImportIpRange("173.194.142.0-255");
            ImportIpRange("173.194.141.0-255");
            ImportIpRange("173.194.140.0-255");
            ImportIpRange("173.194.136.0-255");
            ImportIpRange("173.194.127.0-255");
            ImportIpRange("173.194.126.0-255");
            ImportIpRange("173.194.124.0-255");
            ImportIpRange("173.194.121.0-255");
            ImportIpRange("173.194.120.0-255");
            ImportIpRange("173.194.119.0-255");
            ImportIpRange("173.194.118.0-255");
            ImportIpRange("173.194.117.0-255");
            ImportIpRange("173.194.113.0-255");
            ImportIpRange("173.194.112.0-255");
            ImportIpRange("173.194.0-255.0-255");
            ImportIpRange("172.253.0-255.0-255");
            ImportIpRange("172.217.0-255.0-255");
            ImportIpRange("162.222.176.0-255");
            ImportIpRange("162.216.148.0-255");
            ImportIpRange("149.3.177.0-255");
            ImportIpRange("149.126.86.0-255");
            ImportIpRange("146.148.0-255.0-255");
            ImportIpRange("142.250.0-255.0-255");
            ImportIpRange("130.211.0-255.0-255");
            ImportIpRange("12.216.80.0-255");
            ImportIpRange("118.174.27.0-255");
            ImportIpRange("118.174.26.0-255");
            ImportIpRange("118.174.25.0-255");
            ImportIpRange("118.174.24.0-255");
            ImportIpRange("113.197.106.0-255");
            ImportIpRange("113.197.105.0-255");
            ImportIpRange("111.92.162.0-255");
            ImportIpRange("108.59.80.0-255");
            ImportIpRange("108.177.0-255.0-255");
            ImportIpRange("108.170.192.0-255");
            ImportIpRange("107.188.128.0-255");
            ImportIpRange("107.178.192.0-255");
            ImportIpRange("107.167.160.0-255");
            ImportIpRange("103.25.178.0-255");
            ImportIpRange("103.246.187.0-255");
            ImportIpRange("1.179.253.0-255");
            ImportIpRange("1.179.252.0-255");
            ImportIpRange("1.179.251.0-255");
            ImportIpRange("1.179.250.0-255");
            ImportIpRange("1.179.249.0-255");
            ImportIpRange("1.179.248.0-255");
        }

        private readonly Regex rxMatchIp =
            new Regex(@"((25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})",
                RegexOptions.Compiled);

        private readonly DataTable IpTable = new DataTable();

        public static Queue<string> WaitQueue = new Queue<string>();
        public static Queue<object> TestQueue = new Queue<object>();

        private readonly Timer StdTestTimer = new Timer();
        private readonly Timer RndTestTimer = new Timer();
        private string GaHost;
        private int GaPort;
        public static string ValidIps;

        private static Random random = new Random();
        public static int TestTimeout = 6000;
        public static int MaxThreads = 40;

        private bool GaIsTesting;
        private bool StdIsTesting;
        private bool RndIsTesting;

        private Process proxyProcess;
        private IniFile IniFile;
        private ProcessStartInfo GaStartInfo;

        public bool StopGaTest;

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
            dgvIpData.Columns[1].Width = 80;
            dgvIpData.Columns[1].HeaderText = "标准测试";
            dgvIpData.Columns[2].Width = 80;
            dgvIpData.Columns[2].HeaderText = "代理测试";

            /// Std
            ServicePointManager.ServerCertificateValidationCallback = (o, certificate, chain, errors) => true;

            StdTestTimer.Interval = 30;
            StdTestTimer.Elapsed += StdTestTimerElapsed;

            RndTestTimer.Interval = 30;
            RndTestTimer.Elapsed += RndTestTimerElapsed;

        }

        private static int SetRange(int val, int min, int max)
        {
            val = val > min ? val : min;
            val = val < max ? val : max;
            return val;
        }

        private void RndTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var testCount = TestQueue.Count;
            var waitCount = dgvIpData.RowCount;

            SetRndProgress(testCount, waitCount);

            if (RndIsTesting && waitCount < Form2.RandomNumber && testCount < MaxThreads)
            {
                string addr;
                do
                {
                    IpRange iprange = IpRangePool[random.Next(0, IpRangePool.Count)];
                    addr = iprange.GetRandomIp();
                } while (WaitQueue.Contains(addr));

                var thread = new Thread(() =>
                {
                    Monitor.Enter(TestQueue);
                    TestQueue.Enqueue(0);
                    Monitor.Exit(TestQueue);

                    var result = StdTestProcess(addr, TestTimeout);
                    if (result.ok)
                    {
                        ImportIp(addr);
                        SetStdTestResult(result);
                    }

                    Monitor.Enter(TestQueue);
                    TestQueue.Dequeue();
                    Monitor.Exit(TestQueue);
                });
                thread.Start();
            }
            else if (testCount == 0)
            {
                RndIsTesting = false;
                RndTestTimer.Stop();
            }
        }

        private void StdTestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var testCount = TestQueue.Count;
            var waitCount = WaitQueue.Count;

            SetStdProgress(testCount, waitCount);

            if (StdIsTesting && waitCount > 0 && testCount < MaxThreads)
            {
                var addr = WaitQueue.Dequeue();
                var thread = new Thread(() =>
                {
                    Monitor.Enter(TestQueue);
                    TestQueue.Enqueue(0);
                    Monitor.Exit(TestQueue);

                    SetStdTestResult(StdTestProcess(addr, TestTimeout));

                    Monitor.Enter(TestQueue);
                    TestQueue.Dequeue();
                    Monitor.Exit(TestQueue);
                });
                thread.Start();
            }
            else if (waitCount == 0 && testCount == 0)
            {
                StdIsTesting = false;
                StdTestTimer.Stop();
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
        private void SetRndProgress(int testCount, int waitCount)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetRndProgress(testCount, waitCount)));
            }
            else
            {
                pbProgress.Value = SetRange(waitCount, 0, pbProgress.Maximum);
                lProgress.Text = testCount + " / " + waitCount;
            }
        }
        struct TestResult
        {
            public string addr;
            public bool ok;
            public string msg;
        }


        private TestResult StdTestProcess(string addr, int timeout)
        {
            bool pingOk = false;
            long pingTime = 0;

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var stopwatch = new Stopwatch();

            try
            {
                socket.BeginConnect(addr, 443, x =>
                   {
                       try
                       {
                           socket.EndConnect(x);
                           pingTime = stopwatch.ElapsedMilliseconds;
                           pingOk = true;
                       }
                       catch (Exception) { }

                       stopwatch.Stop();
                       socket.Close();

                   }, null);

                stopwatch.Start();

                while (!pingOk)
                {
                    if (stopwatch.ElapsedMilliseconds > TestTimeout)
                    {
                        stopwatch.Stop();
                        socket.Close();

                        return new TestResult()
                        {
                            addr = addr,
                            ok = false,
                            msg = "Timeout"
                        };
                    }

                    Thread.Sleep(20);
                }
            }
            catch (Exception)
            {
                stopwatch.Stop();
                socket.Close();

                return new TestResult()
                {
                    addr = addr,
                    ok = false,
                    msg = "Failed"
                };
            }

            TestResult result;

            var sbd = new StringBuilder();

            for (int i = 0; i < 150; i++)
                sbd.Append(random.Next().ToString("D10"));

            var url = "https://" + addr + "/?" + Convert.ToBase64String(Encoding.UTF8.GetBytes(sbd.ToString()));

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = timeout;
            req.Method = "HEAD";
            req.AllowAutoRedirect = false;
            req.KeepAlive = false;
            req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            try
            {
                using (var resp = (HttpWebResponse)req.GetResponse())
                {
                    if (resp.Server == "gws")
                    {
                        result = new TestResult()
                        {
                            addr = addr,
                            ok = true,
                            msg = "_OK " + pingTime.ToString("D4")
                        };
                    }
                    else
                    {
                        result = new TestResult()
                          {
                              addr = addr,
                              ok = false,
                              msg = "Invalid"
                          };
                    }
                    resp.Close();
                }
            }
            catch (Exception)
            {
                result = new TestResult()
                         {
                             addr = addr,
                             ok = false,
                             msg = "Failed"
                         };
            }

            return result;
        }

        private void SetStdTestResult(TestResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetStdTestResult(result)));
            }
            else
            {
                var rows = SelectByIP(result.addr);
                if (rows.Length > 0)
                    rows[0][1] = result.msg;
            }
        }
        public void SetGaTestResult(string addr, string result)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetGaTestResult(addr, result)));
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
            if (StdIsTesting || GaIsTesting || RndIsTesting)
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

            var ranges = str.Split(@"`~!?@#$%^&*()=+,<>;:'，。；：“”‘’？、".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var range in ranges)
            {
                var iprange = IpRange.CreateIpRange(range);
                if (iprange == null)
                {
                    continue;
                }

                for (int a = iprange.cope[0, 0]; a <= iprange.cope[0, 1]; a++)
                {
                    for (int b = iprange.cope[1, 0]; b <= iprange.cope[1, 1]; b++)
                    {
                        for (int c = iprange.cope[2, 0]; c <= iprange.cope[2, 1]; c++)
                        {
                            for (int d = iprange.cope[3, 0]; d <= iprange.cope[3, 1]; d++)
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

            var mc = rxMatchIp.Matches(str);

            foreach (Match m in mc)
            {
                ImportIp(m.Value);
            }
        }


        private void mStartStdTest_Click(object sender, EventArgs e)
        {
            if (RndIsTesting)
            {
                MessageBox.Show("正在运行随机测试！");
                return;
            }

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
            pbProgress.Value = 0;

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
            if (IsTesting())
            {
                return;
            }

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
            if (IsTesting())
            {
                return;
            }

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
            StopGaTest = true;
            StdIsTesting = false;
            RndIsTesting = false;

            pbProgress.Value = 0;
            lProgress.Text = "0 / 0";
        }

        private void mExportAllIps_Click(object sender, EventArgs e)
        {
            if (IsTesting())
            {
                return;
            }

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

            var mc = rxMatchIp.Matches(str);

            foreach (Match m in mc)
            {
                RemoveIp(m.Value);
            }
        }

        private void mStartGaTest_Click(object sender, EventArgs e)
        {
            if (RndIsTesting)
            {
                MessageBox.Show("正在运行随机测试！");
                return;
            }

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
            pbProgress.Value = 0;
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
                pbProgress.Value = SetRange(pbProgress.Maximum - left, 0, pbProgress.Maximum);
                lProgress.Text = left + " / " + pbProgress.Maximum;
            }
        }

        public void GaCopyFiles()
        {
            Directory.CreateDirectory("gatester");

            var sdir = Application.StartupPath;
            var tdir = Application.StartupPath + @"\gatester\";

            File.Copy("python27.exe", @"gatester\python27go.exe", true);
            var fpaths = Directory.GetFiles(sdir);
            foreach (var fpath in fpaths)
            {
                var fname = Path.GetFileName(fpath);
                if (fname.ToLower() == "python27.exe" || String.Equals(fpath, Application.ExecutablePath, StringComparison.CurrentCultureIgnoreCase))
                    continue;

                File.Copy(fname, tdir + fname, true);
            }

            Directory.CreateDirectory(@"gatester\certs");
            foreach (var path in Directory.GetFiles("certs"))
            {
                var fname = Path.GetFileName(path);
                File.Copy(path, @"gatester\certs\" + fname, true);
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
                        SetGaTestResult(addr, "Failed");
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

        private void mStartRndTest_Click(object sender, EventArgs e)
        {
            if (GaIsTesting)
            {
                MessageBox.Show("正在运行代理测试！");
                return;
            }

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

            if (MessageBox.Show(this, "随机测试前会清除IP列表，是否继续操作？", "请确认操作！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }

            var form = new Form2();
            form.ShowDialog(this);

            if (Form2.RandomNumber == 0)
            {
                return;
            }

            pbProgress.Maximum = Form2.RandomNumber;
            pbProgress.Value = 0;

            RemoveAllIps();

            RndIsTesting = true;
            RndTestTimer.Start();
        }
    }


    class IpRange
    {
        private IpRange() { }

        public int[,] cope = new int[4, 2];

        public string GetRandomIp()
        {
            var sbd = new StringBuilder();
            sbd.Append(rand.Next(cope[0, 0], cope[0, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(cope[1, 0], cope[1, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(cope[2, 0], cope[2, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(cope[3, 0], cope[3, 1] + 1));
            return sbd.ToString();
        }

        private static Random rand = new Random();
        private static int SetRange(int val)
        {
            val = val > 0 ? val : 0;
            val = val < 255 ? val : 255;
            return val;
        }
        public static IpRange CreateIpRange(string range)
        {
            try
            {
                var cope = range.Split(new[] { '.' });

                if (cope.Length != 4)
                {
                    return null;
                }

                var iprange = new IpRange();
                int swap;
                for (int i = 0; i < 4; i++)
                {
                    var str = cope[i].Trim();
                    if (str.Length == 0)
                    {
                        iprange.cope[i, 0] = 0;
                        iprange.cope[i, 1] = 0;

                    }
                    else
                    {
                        var sps = str.Split(@"-\|/".ToCharArray());

                        switch (sps.Length)
                        {
                            case 1:
                                iprange.cope[i, 0] = iprange.cope[i, 1] = SetRange(Convert.ToInt32(sps[0]));
                                break;
                            case 2:
                                iprange.cope[i, 0] = SetRange(Convert.ToInt32(sps[0]));
                                iprange.cope[i, 1] = SetRange(Convert.ToInt32(sps[1]));
                                if (iprange.cope[i, 0] > iprange.cope[i, 1])
                                {
                                    swap = iprange.cope[i, 1];
                                    iprange.cope[i, 1] = iprange.cope[i, 0];
                                    iprange.cope[i, 0] = swap;
                                }
                                break;
                            default:
                                iprange.cope[i, 0] = 0;
                                iprange.cope[i, 1] = 0;
                                break;
                        }
                    }
                }

                return iprange;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

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
            var iprange = IpRange.CreateIpRange(range);
            if (iprange != null)
            {
                IpRangePool.Add(iprange);
            }
        }

        public Form1()
        {
            InitializeComponent();

            ImportIpRange("1.179.248.0-255");
            ImportIpRange("1.179.249.0-255");
            ImportIpRange("1.179.250.0-255");
            ImportIpRange("1.179.251.0-255");
            ImportIpRange("1.179.252.0-255");
            ImportIpRange("1.179.253.0-255");
            ImportIpRange("103.246.187.0-255");
            ImportIpRange("103.25.178.0-255");
            ImportIpRange("106.162.192.148-187");
            ImportIpRange("106.162.198.84-123");
            ImportIpRange("106.162.216.0-255");
            ImportIpRange("107.167.160.0-255");
            ImportIpRange("107.178.192.0-255");
            ImportIpRange("107.188.128.0-255");
            ImportIpRange("108.166.34.0-255");
            ImportIpRange("108.170.192.0-255");
            ImportIpRange("108.177.0.0-255");
            ImportIpRange("108.177.1.0-255");
            ImportIpRange("108.177.10.0-255");
            ImportIpRange("108.177.100.0-255");
            ImportIpRange("108.177.101.0-255");
            ImportIpRange("108.177.102.0-255");
            ImportIpRange("108.177.103.0-255");
            ImportIpRange("108.177.104.0-255");
            ImportIpRange("108.177.105.0-255");
            ImportIpRange("108.177.106.0-255");
            ImportIpRange("108.177.107.0-255");
            ImportIpRange("108.177.108.0-255");
            ImportIpRange("108.177.109.0-255");
            ImportIpRange("108.177.11.0-255");
            ImportIpRange("108.177.110.0-255");
            ImportIpRange("108.177.111.0-255");
            ImportIpRange("108.177.112.0-255");
            ImportIpRange("108.177.113.0-255");
            ImportIpRange("108.177.114.0-255");
            ImportIpRange("108.177.115.0-255");
            ImportIpRange("108.177.116.0-255");
            ImportIpRange("108.177.117.0-255");
            ImportIpRange("108.177.118.0-255");
            ImportIpRange("108.177.119.0-255");
            ImportIpRange("108.177.12.0-255");
            ImportIpRange("108.177.120.0-255");
            ImportIpRange("108.177.121.0-255");
            ImportIpRange("108.177.122.0-255");
            ImportIpRange("108.177.123.0-255");
            ImportIpRange("108.177.124.0-255");
            ImportIpRange("108.177.125.0-255");
            ImportIpRange("108.177.126.0-255");
            ImportIpRange("108.177.127.0-255");
            ImportIpRange("108.177.128.0-255");
            ImportIpRange("108.177.129.0-255");
            ImportIpRange("108.177.13.0-255");
            ImportIpRange("108.177.130.0-255");
            ImportIpRange("108.177.131.0-255");
            ImportIpRange("108.177.132.0-255");
            ImportIpRange("108.177.133.0-255");
            ImportIpRange("108.177.134.0-255");
            ImportIpRange("108.177.135.0-255");
            ImportIpRange("108.177.136.0-255");
            ImportIpRange("108.177.137.0-255");
            ImportIpRange("108.177.138.0-255");
            ImportIpRange("108.177.139.0-255");
            ImportIpRange("108.177.14.0-255");
            ImportIpRange("108.177.140.0-255");
            ImportIpRange("108.177.141.0-255");
            ImportIpRange("108.177.142.0-255");
            ImportIpRange("108.177.143.0-255");
            ImportIpRange("108.177.144.0-255");
            ImportIpRange("108.177.145.0-255");
            ImportIpRange("108.177.146.0-255");
            ImportIpRange("108.177.147.0-255");
            ImportIpRange("108.177.148.0-255");
            ImportIpRange("108.177.149.0-255");
            ImportIpRange("108.177.15.0-255");
            ImportIpRange("108.177.150.0-255");
            ImportIpRange("108.177.151.0-255");
            ImportIpRange("108.177.152.0-255");
            ImportIpRange("108.177.153.0-255");
            ImportIpRange("108.177.154.0-255");
            ImportIpRange("108.177.155.0-255");
            ImportIpRange("108.177.156.0-255");
            ImportIpRange("108.177.157.0-255");
            ImportIpRange("108.177.158.0-255");
            ImportIpRange("108.177.159.0-255");
            ImportIpRange("108.177.16.0-255");
            ImportIpRange("108.177.160.0-255");
            ImportIpRange("108.177.161.0-255");
            ImportIpRange("108.177.162.0-255");
            ImportIpRange("108.177.163.0-255");
            ImportIpRange("108.177.164.0-255");
            ImportIpRange("108.177.165.0-255");
            ImportIpRange("108.177.166.0-255");
            ImportIpRange("108.177.167.0-255");
            ImportIpRange("108.177.168.0-255");
            ImportIpRange("108.177.169.0-255");
            ImportIpRange("108.177.17.0-255");
            ImportIpRange("108.177.170.0-255");
            ImportIpRange("108.177.171.0-255");
            ImportIpRange("108.177.172.0-255");
            ImportIpRange("108.177.173.0-255");
            ImportIpRange("108.177.174.0-255");
            ImportIpRange("108.177.175.0-255");
            ImportIpRange("108.177.176.0-255");
            ImportIpRange("108.177.177.0-255");
            ImportIpRange("108.177.178.0-255");
            ImportIpRange("108.177.179.0-255");
            ImportIpRange("108.177.18.0-255");
            ImportIpRange("108.177.180.0-255");
            ImportIpRange("108.177.181.0-255");
            ImportIpRange("108.177.182.0-255");
            ImportIpRange("108.177.183.0-255");
            ImportIpRange("108.177.184.0-255");
            ImportIpRange("108.177.185.0-255");
            ImportIpRange("108.177.186.0-255");
            ImportIpRange("108.177.187.0-255");
            ImportIpRange("108.177.188.0-255");
            ImportIpRange("108.177.189.0-255");
            ImportIpRange("108.177.19.0-255");
            ImportIpRange("108.177.190.0-255");
            ImportIpRange("108.177.191.0-255");
            ImportIpRange("108.177.192.0-255");
            ImportIpRange("108.177.193.0-255");
            ImportIpRange("108.177.194.0-255");
            ImportIpRange("108.177.195.0-255");
            ImportIpRange("108.177.196.0-255");
            ImportIpRange("108.177.197.0-255");
            ImportIpRange("108.177.198.0-255");
            ImportIpRange("108.177.199.0-255");
            ImportIpRange("108.177.2.0-255");
            ImportIpRange("108.177.20.0-255");
            ImportIpRange("108.177.200.0-255");
            ImportIpRange("108.177.201.0-255");
            ImportIpRange("108.177.202.0-255");
            ImportIpRange("108.177.203.0-255");
            ImportIpRange("108.177.204.0-255");
            ImportIpRange("108.177.205.0-255");
            ImportIpRange("108.177.206.0-255");
            ImportIpRange("108.177.207.0-255");
            ImportIpRange("108.177.208.0-255");
            ImportIpRange("108.177.209.0-255");
            ImportIpRange("108.177.21.0-255");
            ImportIpRange("108.177.210.0-255");
            ImportIpRange("108.177.211.0-255");
            ImportIpRange("108.177.212.0-255");
            ImportIpRange("108.177.213.0-255");
            ImportIpRange("108.177.214.0-255");
            ImportIpRange("108.177.215.0-255");
            ImportIpRange("108.177.216.0-255");
            ImportIpRange("108.177.217.0-255");
            ImportIpRange("108.177.218.0-255");
            ImportIpRange("108.177.219.0-255");
            ImportIpRange("108.177.22.0-255");
            ImportIpRange("108.177.220.0-255");
            ImportIpRange("108.177.221.0-255");
            ImportIpRange("108.177.222.0-255");
            ImportIpRange("108.177.223.0-255");
            ImportIpRange("108.177.224.0-255");
            ImportIpRange("108.177.225.0-255");
            ImportIpRange("108.177.226.0-255");
            ImportIpRange("108.177.227.0-255");
            ImportIpRange("108.177.228.0-255");
            ImportIpRange("108.177.229.0-255");
            ImportIpRange("108.177.23.0-255");
            ImportIpRange("108.177.230.0-255");
            ImportIpRange("108.177.231.0-255");
            ImportIpRange("108.177.232.0-255");
            ImportIpRange("108.177.233.0-255");
            ImportIpRange("108.177.234.0-255");
            ImportIpRange("108.177.235.0-255");
            ImportIpRange("108.177.236.0-255");
            ImportIpRange("108.177.237.0-255");
            ImportIpRange("108.177.238.0-255");
            ImportIpRange("108.177.239.0-255");
            ImportIpRange("108.177.24.0-255");
            ImportIpRange("108.177.240.0-255");
            ImportIpRange("108.177.241.0-255");
            ImportIpRange("108.177.242.0-255");
            ImportIpRange("108.177.243.0-255");
            ImportIpRange("108.177.244.0-255");
            ImportIpRange("108.177.245.0-255");
            ImportIpRange("108.177.246.0-255");
            ImportIpRange("108.177.247.0-255");
            ImportIpRange("108.177.248.0-255");
            ImportIpRange("108.177.249.0-255");
            ImportIpRange("108.177.25.0-255");
            ImportIpRange("108.177.250.0-255");
            ImportIpRange("108.177.251.0-255");
            ImportIpRange("108.177.252.0-255");
            ImportIpRange("108.177.253.0-255");
            ImportIpRange("108.177.254.0-255");
            ImportIpRange("108.177.255.0-255");
            ImportIpRange("108.177.26.0-255");
            ImportIpRange("108.177.27.0-255");
            ImportIpRange("108.177.28.0-255");
            ImportIpRange("108.177.29.0-255");
            ImportIpRange("108.177.3.0-255");
            ImportIpRange("108.177.30.0-255");
            ImportIpRange("108.177.31.0-255");
            ImportIpRange("108.177.32.0-255");
            ImportIpRange("108.177.33.0-255");
            ImportIpRange("108.177.34.0-255");
            ImportIpRange("108.177.35.0-255");
            ImportIpRange("108.177.36.0-255");
            ImportIpRange("108.177.37.0-255");
            ImportIpRange("108.177.38.0-255");
            ImportIpRange("108.177.39.0-255");
            ImportIpRange("108.177.4.0-255");
            ImportIpRange("108.177.40.0-255");
            ImportIpRange("108.177.41.0-255");
            ImportIpRange("108.177.42.0-255");
            ImportIpRange("108.177.43.0-255");
            ImportIpRange("108.177.44.0-255");
            ImportIpRange("108.177.45.0-255");
            ImportIpRange("108.177.46.0-255");
            ImportIpRange("108.177.47.0-255");
            ImportIpRange("108.177.48.0-255");
            ImportIpRange("108.177.49.0-255");
            ImportIpRange("108.177.5.0-255");
            ImportIpRange("108.177.50.0-255");
            ImportIpRange("108.177.51.0-255");
            ImportIpRange("108.177.52.0-255");
            ImportIpRange("108.177.53.0-255");
            ImportIpRange("108.177.54.0-255");
            ImportIpRange("108.177.55.0-255");
            ImportIpRange("108.177.56.0-255");
            ImportIpRange("108.177.57.0-255");
            ImportIpRange("108.177.58.0-255");
            ImportIpRange("108.177.59.0-255");
            ImportIpRange("108.177.6.0-255");
            ImportIpRange("108.177.60.0-255");
            ImportIpRange("108.177.61.0-255");
            ImportIpRange("108.177.62.0-255");
            ImportIpRange("108.177.63.0-255");
            ImportIpRange("108.177.64.0-255");
            ImportIpRange("108.177.65.0-255");
            ImportIpRange("108.177.66.0-255");
            ImportIpRange("108.177.67.0-255");
            ImportIpRange("108.177.68.0-255");
            ImportIpRange("108.177.69.0-255");
            ImportIpRange("108.177.7.0-255");
            ImportIpRange("108.177.70.0-255");
            ImportIpRange("108.177.71.0-255");
            ImportIpRange("108.177.72.0-255");
            ImportIpRange("108.177.73.0-255");
            ImportIpRange("108.177.74.0-255");
            ImportIpRange("108.177.75.0-255");
            ImportIpRange("108.177.76.0-255");
            ImportIpRange("108.177.77.0-255");
            ImportIpRange("108.177.78.0-255");
            ImportIpRange("108.177.79.0-255");
            ImportIpRange("108.177.8.0-255");
            ImportIpRange("108.177.80.0-255");
            ImportIpRange("108.177.81.0-255");
            ImportIpRange("108.177.82.0-255");
            ImportIpRange("108.177.83.0-255");
            ImportIpRange("108.177.84.0-255");
            ImportIpRange("108.177.85.0-255");
            ImportIpRange("108.177.86.0-255");
            ImportIpRange("108.177.87.0-255");
            ImportIpRange("108.177.88.0-255");
            ImportIpRange("108.177.89.0-255");
            ImportIpRange("108.177.9.0-255");
            ImportIpRange("108.177.90.0-255");
            ImportIpRange("108.177.91.0-255");
            ImportIpRange("108.177.92.0-255");
            ImportIpRange("108.177.93.0-255");
            ImportIpRange("108.177.94.0-255");
            ImportIpRange("108.177.95.0-255");
            ImportIpRange("108.177.96.0-255");
            ImportIpRange("108.177.97.0-255");
            ImportIpRange("108.177.98.0-255");
            ImportIpRange("108.177.99.0-255");
            ImportIpRange("108.59.80.0-255");
            ImportIpRange("110.75.151.0-255");
            ImportIpRange("111.168.255.0-255");
            ImportIpRange("111.92.162.0-255");
            ImportIpRange("113.197.105.0-255");
            ImportIpRange("113.197.106.0-255");
            ImportIpRange("118.174.24.0-255");
            ImportIpRange("118.174.25.0-255");
            ImportIpRange("118.174.26.0-255");
            ImportIpRange("118.174.27.0-255");
            ImportIpRange("119.147.146.0-255");
            ImportIpRange("119.57.55.0-255");
            ImportIpRange("12.216.80.0-255");
            ImportIpRange("121.194.0.0-255");
            ImportIpRange("121.195.178.0-255");
            ImportIpRange("121.78.74.68-123");
            ImportIpRange("123.205.250.0-255");
            ImportIpRange("123.205.251.68-123");
            ImportIpRange("124.160.89.0-255");
            ImportIpRange("130.211.0.0-255");
            ImportIpRange("130.211.1.0-255");
            ImportIpRange("130.211.10.0-255");
            ImportIpRange("130.211.100.0-255");
            ImportIpRange("130.211.101.0-255");
            ImportIpRange("130.211.102.0-255");
            ImportIpRange("130.211.103.0-255");
            ImportIpRange("130.211.104.0-255");
            ImportIpRange("130.211.105.0-255");
            ImportIpRange("130.211.106.0-255");
            ImportIpRange("130.211.107.0-255");
            ImportIpRange("130.211.108.0-255");
            ImportIpRange("130.211.109.0-255");
            ImportIpRange("130.211.11.0-255");
            ImportIpRange("130.211.110.0-255");
            ImportIpRange("130.211.111.0-255");
            ImportIpRange("130.211.112.0-255");
            ImportIpRange("130.211.113.0-255");
            ImportIpRange("130.211.114.0-255");
            ImportIpRange("130.211.115.0-255");
            ImportIpRange("130.211.116.0-255");
            ImportIpRange("130.211.117.0-255");
            ImportIpRange("130.211.118.0-255");
            ImportIpRange("130.211.119.0-255");
            ImportIpRange("130.211.12.0-255");
            ImportIpRange("130.211.120.0-255");
            ImportIpRange("130.211.121.0-255");
            ImportIpRange("130.211.122.0-255");
            ImportIpRange("130.211.123.0-255");
            ImportIpRange("130.211.124.0-255");
            ImportIpRange("130.211.125.0-255");
            ImportIpRange("130.211.126.0-255");
            ImportIpRange("130.211.127.0-255");
            ImportIpRange("130.211.128.0-255");
            ImportIpRange("130.211.129.0-255");
            ImportIpRange("130.211.13.0-255");
            ImportIpRange("130.211.130.0-255");
            ImportIpRange("130.211.131.0-255");
            ImportIpRange("130.211.132.0-255");
            ImportIpRange("130.211.133.0-255");
            ImportIpRange("130.211.134.0-255");
            ImportIpRange("130.211.135.0-255");
            ImportIpRange("130.211.136.0-255");
            ImportIpRange("130.211.137.0-255");
            ImportIpRange("130.211.138.0-255");
            ImportIpRange("130.211.139.0-255");
            ImportIpRange("130.211.14.0-255");
            ImportIpRange("130.211.140.0-255");
            ImportIpRange("130.211.141.0-255");
            ImportIpRange("130.211.142.0-255");
            ImportIpRange("130.211.143.0-255");
            ImportIpRange("130.211.144.0-255");
            ImportIpRange("130.211.145.0-255");
            ImportIpRange("130.211.146.0-255");
            ImportIpRange("130.211.147.0-255");
            ImportIpRange("130.211.148.0-255");
            ImportIpRange("130.211.149.0-255");
            ImportIpRange("130.211.15.0-255");
            ImportIpRange("130.211.150.0-255");
            ImportIpRange("130.211.151.0-255");
            ImportIpRange("130.211.152.0-255");
            ImportIpRange("130.211.153.0-255");
            ImportIpRange("130.211.154.0-255");
            ImportIpRange("130.211.155.0-255");
            ImportIpRange("130.211.156.0-255");
            ImportIpRange("130.211.157.0-255");
            ImportIpRange("130.211.158.0-255");
            ImportIpRange("130.211.159.0-255");
            ImportIpRange("130.211.16.0-255");
            ImportIpRange("130.211.160.0-255");
            ImportIpRange("130.211.161.0-255");
            ImportIpRange("130.211.162.0-255");
            ImportIpRange("130.211.163.0-255");
            ImportIpRange("130.211.164.0-255");
            ImportIpRange("130.211.165.0-255");
            ImportIpRange("130.211.166.0-255");
            ImportIpRange("130.211.167.0-255");
            ImportIpRange("130.211.168.0-255");
            ImportIpRange("130.211.169.0-255");
            ImportIpRange("130.211.17.0-255");
            ImportIpRange("130.211.170.0-255");
            ImportIpRange("130.211.171.0-255");
            ImportIpRange("130.211.172.0-255");
            ImportIpRange("130.211.173.0-255");
            ImportIpRange("130.211.174.0-255");
            ImportIpRange("130.211.175.0-255");
            ImportIpRange("130.211.176.0-255");
            ImportIpRange("130.211.177.0-255");
            ImportIpRange("130.211.178.0-255");
            ImportIpRange("130.211.179.0-255");
            ImportIpRange("130.211.18.0-255");
            ImportIpRange("130.211.180.0-255");
            ImportIpRange("130.211.181.0-255");
            ImportIpRange("130.211.182.0-255");
            ImportIpRange("130.211.183.0-255");
            ImportIpRange("130.211.184.0-255");
            ImportIpRange("130.211.185.0-255");
            ImportIpRange("130.211.186.0-255");
            ImportIpRange("130.211.187.0-255");
            ImportIpRange("130.211.188.0-255");
            ImportIpRange("130.211.189.0-255");
            ImportIpRange("130.211.19.0-255");
            ImportIpRange("130.211.190.0-255");
            ImportIpRange("130.211.191.0-255");
            ImportIpRange("130.211.192.0-255");
            ImportIpRange("130.211.193.0-255");
            ImportIpRange("130.211.194.0-255");
            ImportIpRange("130.211.195.0-255");
            ImportIpRange("130.211.196.0-255");
            ImportIpRange("130.211.197.0-255");
            ImportIpRange("130.211.198.0-255");
            ImportIpRange("130.211.199.0-255");
            ImportIpRange("130.211.2.0-255");
            ImportIpRange("130.211.20.0-255");
            ImportIpRange("130.211.200.0-255");
            ImportIpRange("130.211.201.0-255");
            ImportIpRange("130.211.202.0-255");
            ImportIpRange("130.211.203.0-255");
            ImportIpRange("130.211.204.0-255");
            ImportIpRange("130.211.205.0-255");
            ImportIpRange("130.211.206.0-255");
            ImportIpRange("130.211.207.0-255");
            ImportIpRange("130.211.208.0-255");
            ImportIpRange("130.211.209.0-255");
            ImportIpRange("130.211.21.0-255");
            ImportIpRange("130.211.210.0-255");
            ImportIpRange("130.211.211.0-255");
            ImportIpRange("130.211.212.0-255");
            ImportIpRange("130.211.213.0-255");
            ImportIpRange("130.211.214.0-255");
            ImportIpRange("130.211.215.0-255");
            ImportIpRange("130.211.216.0-255");
            ImportIpRange("130.211.217.0-255");
            ImportIpRange("130.211.218.0-255");
            ImportIpRange("130.211.219.0-255");
            ImportIpRange("130.211.22.0-255");
            ImportIpRange("130.211.220.0-255");
            ImportIpRange("130.211.221.0-255");
            ImportIpRange("130.211.222.0-255");
            ImportIpRange("130.211.223.0-255");
            ImportIpRange("130.211.224.0-255");
            ImportIpRange("130.211.225.0-255");
            ImportIpRange("130.211.226.0-255");
            ImportIpRange("130.211.227.0-255");
            ImportIpRange("130.211.228.0-255");
            ImportIpRange("130.211.229.0-255");
            ImportIpRange("130.211.23.0-255");
            ImportIpRange("130.211.230.0-255");
            ImportIpRange("130.211.231.0-255");
            ImportIpRange("130.211.232.0-255");
            ImportIpRange("130.211.233.0-255");
            ImportIpRange("130.211.234.0-255");
            ImportIpRange("130.211.235.0-255");
            ImportIpRange("130.211.236.0-255");
            ImportIpRange("130.211.237.0-255");
            ImportIpRange("130.211.238.0-255");
            ImportIpRange("130.211.239.0-255");
            ImportIpRange("130.211.24.0-255");
            ImportIpRange("130.211.240.0-255");
            ImportIpRange("130.211.241.0-255");
            ImportIpRange("130.211.242.0-255");
            ImportIpRange("130.211.243.0-255");
            ImportIpRange("130.211.244.0-255");
            ImportIpRange("130.211.245.0-255");
            ImportIpRange("130.211.246.0-255");
            ImportIpRange("130.211.247.0-255");
            ImportIpRange("130.211.248.0-255");
            ImportIpRange("130.211.249.0-255");
            ImportIpRange("130.211.25.0-255");
            ImportIpRange("130.211.250.0-255");
            ImportIpRange("130.211.251.0-255");
            ImportIpRange("130.211.252.0-255");
            ImportIpRange("130.211.253.0-255");
            ImportIpRange("130.211.254.0-255");
            ImportIpRange("130.211.255.0-255");
            ImportIpRange("130.211.26.0-255");
            ImportIpRange("130.211.27.0-255");
            ImportIpRange("130.211.28.0-255");
            ImportIpRange("130.211.29.0-255");
            ImportIpRange("130.211.3.0-255");
            ImportIpRange("130.211.30.0-255");
            ImportIpRange("130.211.31.0-255");
            ImportIpRange("130.211.32.0-255");
            ImportIpRange("130.211.33.0-255");
            ImportIpRange("130.211.34.0-255");
            ImportIpRange("130.211.35.0-255");
            ImportIpRange("130.211.36.0-255");
            ImportIpRange("130.211.37.0-255");
            ImportIpRange("130.211.38.0-255");
            ImportIpRange("130.211.39.0-255");
            ImportIpRange("130.211.4.0-255");
            ImportIpRange("130.211.40.0-255");
            ImportIpRange("130.211.41.0-255");
            ImportIpRange("130.211.42.0-255");
            ImportIpRange("130.211.43.0-255");
            ImportIpRange("130.211.44.0-255");
            ImportIpRange("130.211.45.0-255");
            ImportIpRange("130.211.46.0-255");
            ImportIpRange("130.211.47.0-255");
            ImportIpRange("130.211.48.0-255");
            ImportIpRange("130.211.49.0-255");
            ImportIpRange("130.211.5.0-255");
            ImportIpRange("130.211.50.0-255");
            ImportIpRange("130.211.51.0-255");
            ImportIpRange("130.211.52.0-255");
            ImportIpRange("130.211.53.0-255");
            ImportIpRange("130.211.54.0-255");
            ImportIpRange("130.211.55.0-255");
            ImportIpRange("130.211.56.0-255");
            ImportIpRange("130.211.57.0-255");
            ImportIpRange("130.211.58.0-255");
            ImportIpRange("130.211.59.0-255");
            ImportIpRange("130.211.6.0-255");
            ImportIpRange("130.211.60.0-255");
            ImportIpRange("130.211.61.0-255");
            ImportIpRange("130.211.62.0-255");
            ImportIpRange("130.211.63.0-255");
            ImportIpRange("130.211.64.0-255");
            ImportIpRange("130.211.65.0-255");
            ImportIpRange("130.211.66.0-255");
            ImportIpRange("130.211.67.0-255");
            ImportIpRange("130.211.68.0-255");
            ImportIpRange("130.211.69.0-255");
            ImportIpRange("130.211.7.0-255");
            ImportIpRange("130.211.70.0-255");
            ImportIpRange("130.211.71.0-255");
            ImportIpRange("130.211.72.0-255");
            ImportIpRange("130.211.73.0-255");
            ImportIpRange("130.211.74.0-255");
            ImportIpRange("130.211.75.0-255");
            ImportIpRange("130.211.76.0-255");
            ImportIpRange("130.211.77.0-255");
            ImportIpRange("130.211.78.0-255");
            ImportIpRange("130.211.79.0-255");
            ImportIpRange("130.211.8.0-255");
            ImportIpRange("130.211.80.0-255");
            ImportIpRange("130.211.81.0-255");
            ImportIpRange("130.211.82.0-255");
            ImportIpRange("130.211.83.0-255");
            ImportIpRange("130.211.84.0-255");
            ImportIpRange("130.211.85.0-255");
            ImportIpRange("130.211.86.0-255");
            ImportIpRange("130.211.87.0-255");
            ImportIpRange("130.211.88.0-255");
            ImportIpRange("130.211.89.0-255");
            ImportIpRange("130.211.9.0-255");
            ImportIpRange("130.211.90.0-255");
            ImportIpRange("130.211.91.0-255");
            ImportIpRange("130.211.92.0-255");
            ImportIpRange("130.211.93.0-255");
            ImportIpRange("130.211.94.0-255");
            ImportIpRange("130.211.95.0-255");
            ImportIpRange("130.211.96.0-255");
            ImportIpRange("130.211.97.0-255");
            ImportIpRange("130.211.98.0-255");
            ImportIpRange("130.211.99.0-255");
            ImportIpRange("142.250.0.0-255");
            ImportIpRange("142.250.1.0-255");
            ImportIpRange("142.250.10.0-255");
            ImportIpRange("142.250.100.0-255");
            ImportIpRange("142.250.101.0-255");
            ImportIpRange("142.250.102.0-255");
            ImportIpRange("142.250.103.0-255");
            ImportIpRange("142.250.104.0-255");
            ImportIpRange("142.250.105.0-255");
            ImportIpRange("142.250.106.0-255");
            ImportIpRange("142.250.107.0-255");
            ImportIpRange("142.250.108.0-255");
            ImportIpRange("142.250.109.0-255");
            ImportIpRange("142.250.11.0-255");
            ImportIpRange("142.250.110.0-255");
            ImportIpRange("142.250.111.0-255");
            ImportIpRange("142.250.112.0-255");
            ImportIpRange("142.250.113.0-255");
            ImportIpRange("142.250.114.0-255");
            ImportIpRange("142.250.115.0-255");
            ImportIpRange("142.250.116.0-255");
            ImportIpRange("142.250.117.0-255");
            ImportIpRange("142.250.118.0-255");
            ImportIpRange("142.250.119.0-255");
            ImportIpRange("142.250.12.0-255");
            ImportIpRange("142.250.120.0-255");
            ImportIpRange("142.250.121.0-255");
            ImportIpRange("142.250.122.0-255");
            ImportIpRange("142.250.123.0-255");
            ImportIpRange("142.250.124.0-255");
            ImportIpRange("142.250.125.0-255");
            ImportIpRange("142.250.126.0-255");
            ImportIpRange("142.250.127.0-255");
            ImportIpRange("142.250.128.0-255");
            ImportIpRange("142.250.129.0-255");
            ImportIpRange("142.250.13.0-255");
            ImportIpRange("142.250.130.0-255");
            ImportIpRange("142.250.131.0-255");
            ImportIpRange("142.250.132.0-255");
            ImportIpRange("142.250.133.0-255");
            ImportIpRange("142.250.134.0-255");
            ImportIpRange("142.250.135.0-255");
            ImportIpRange("142.250.136.0-255");
            ImportIpRange("142.250.137.0-255");
            ImportIpRange("142.250.138.0-255");
            ImportIpRange("142.250.139.0-255");
            ImportIpRange("142.250.14.0-255");
            ImportIpRange("142.250.140.0-255");
            ImportIpRange("142.250.141.0-255");
            ImportIpRange("142.250.142.0-255");
            ImportIpRange("142.250.143.0-255");
            ImportIpRange("142.250.144.0-255");
            ImportIpRange("142.250.145.0-255");
            ImportIpRange("142.250.146.0-255");
            ImportIpRange("142.250.147.0-255");
            ImportIpRange("142.250.148.0-255");
            ImportIpRange("142.250.149.0-255");
            ImportIpRange("142.250.15.0-255");
            ImportIpRange("142.250.150.0-255");
            ImportIpRange("142.250.151.0-255");
            ImportIpRange("142.250.152.0-255");
            ImportIpRange("142.250.153.0-255");
            ImportIpRange("142.250.154.0-255");
            ImportIpRange("142.250.155.0-255");
            ImportIpRange("142.250.156.0-255");
            ImportIpRange("142.250.157.0-255");
            ImportIpRange("142.250.158.0-255");
            ImportIpRange("142.250.159.0-255");
            ImportIpRange("142.250.16.0-255");
            ImportIpRange("142.250.160.0-255");
            ImportIpRange("142.250.161.0-255");
            ImportIpRange("142.250.162.0-255");
            ImportIpRange("142.250.163.0-255");
            ImportIpRange("142.250.164.0-255");
            ImportIpRange("142.250.165.0-255");
            ImportIpRange("142.250.166.0-255");
            ImportIpRange("142.250.167.0-255");
            ImportIpRange("142.250.168.0-255");
            ImportIpRange("142.250.169.0-255");
            ImportIpRange("142.250.17.0-255");
            ImportIpRange("142.250.170.0-255");
            ImportIpRange("142.250.171.0-255");
            ImportIpRange("142.250.172.0-255");
            ImportIpRange("142.250.173.0-255");
            ImportIpRange("142.250.174.0-255");
            ImportIpRange("142.250.175.0-255");
            ImportIpRange("142.250.176.0-255");
            ImportIpRange("142.250.177.0-255");
            ImportIpRange("142.250.178.0-255");
            ImportIpRange("142.250.179.0-255");
            ImportIpRange("142.250.18.0-255");
            ImportIpRange("142.250.180.0-255");
            ImportIpRange("142.250.181.0-255");
            ImportIpRange("142.250.182.0-255");
            ImportIpRange("142.250.183.0-255");
            ImportIpRange("142.250.184.0-255");
            ImportIpRange("142.250.185.0-255");
            ImportIpRange("142.250.186.0-255");
            ImportIpRange("142.250.187.0-255");
            ImportIpRange("142.250.188.0-255");
            ImportIpRange("142.250.189.0-255");
            ImportIpRange("142.250.19.0-255");
            ImportIpRange("142.250.190.0-255");
            ImportIpRange("142.250.191.0-255");
            ImportIpRange("142.250.192.0-255");
            ImportIpRange("142.250.193.0-255");
            ImportIpRange("142.250.194.0-255");
            ImportIpRange("142.250.195.0-255");
            ImportIpRange("142.250.196.0-255");
            ImportIpRange("142.250.197.0-255");
            ImportIpRange("142.250.198.0-255");
            ImportIpRange("142.250.199.0-255");
            ImportIpRange("142.250.2.0-255");
            ImportIpRange("142.250.20.0-255");
            ImportIpRange("142.250.200.0-255");
            ImportIpRange("142.250.201.0-255");
            ImportIpRange("142.250.202.0-255");
            ImportIpRange("142.250.203.0-255");
            ImportIpRange("142.250.204.0-255");
            ImportIpRange("142.250.205.0-255");
            ImportIpRange("142.250.206.0-255");
            ImportIpRange("142.250.207.0-255");
            ImportIpRange("142.250.208.0-255");
            ImportIpRange("142.250.209.0-255");
            ImportIpRange("142.250.21.0-255");
            ImportIpRange("142.250.210.0-255");
            ImportIpRange("142.250.211.0-255");
            ImportIpRange("142.250.212.0-255");
            ImportIpRange("142.250.213.0-255");
            ImportIpRange("142.250.214.0-255");
            ImportIpRange("142.250.215.0-255");
            ImportIpRange("142.250.216.0-255");
            ImportIpRange("142.250.217.0-255");
            ImportIpRange("142.250.218.0-255");
            ImportIpRange("142.250.219.0-255");
            ImportIpRange("142.250.22.0-255");
            ImportIpRange("142.250.220.0-255");
            ImportIpRange("142.250.221.0-255");
            ImportIpRange("142.250.222.0-255");
            ImportIpRange("142.250.223.0-255");
            ImportIpRange("142.250.224.0-255");
            ImportIpRange("142.250.225.0-255");
            ImportIpRange("142.250.226.0-255");
            ImportIpRange("142.250.227.0-255");
            ImportIpRange("142.250.228.0-255");
            ImportIpRange("142.250.229.0-255");
            ImportIpRange("142.250.23.0-255");
            ImportIpRange("142.250.230.0-255");
            ImportIpRange("142.250.231.0-255");
            ImportIpRange("142.250.232.0-255");
            ImportIpRange("142.250.233.0-255");
            ImportIpRange("142.250.234.0-255");
            ImportIpRange("142.250.235.0-255");
            ImportIpRange("142.250.236.0-255");
            ImportIpRange("142.250.237.0-255");
            ImportIpRange("142.250.238.0-255");
            ImportIpRange("142.250.239.0-255");
            ImportIpRange("142.250.24.0-255");
            ImportIpRange("142.250.240.0-255");
            ImportIpRange("142.250.241.0-255");
            ImportIpRange("142.250.242.0-255");
            ImportIpRange("142.250.243.0-255");
            ImportIpRange("142.250.244.0-255");
            ImportIpRange("142.250.245.0-255");
            ImportIpRange("142.250.246.0-255");
            ImportIpRange("142.250.247.0-255");
            ImportIpRange("142.250.248.0-255");
            ImportIpRange("142.250.249.0-255");
            ImportIpRange("142.250.25.0-255");
            ImportIpRange("142.250.250.0-255");
            ImportIpRange("142.250.251.0-255");
            ImportIpRange("142.250.252.0-255");
            ImportIpRange("142.250.253.0-255");
            ImportIpRange("142.250.254.0-255");
            ImportIpRange("142.250.255.0-255");
            ImportIpRange("142.250.26.0-255");
            ImportIpRange("142.250.27.0-255");
            ImportIpRange("142.250.28.0-255");
            ImportIpRange("142.250.29.0-255");
            ImportIpRange("142.250.3.0-255");
            ImportIpRange("142.250.30.0-255");
            ImportIpRange("142.250.31.0-255");
            ImportIpRange("142.250.32.0-255");
            ImportIpRange("142.250.33.0-255");
            ImportIpRange("142.250.34.0-255");
            ImportIpRange("142.250.35.0-255");
            ImportIpRange("142.250.36.0-255");
            ImportIpRange("142.250.37.0-255");
            ImportIpRange("142.250.38.0-255");
            ImportIpRange("142.250.39.0-255");
            ImportIpRange("142.250.4.0-255");
            ImportIpRange("142.250.40.0-255");
            ImportIpRange("142.250.41.0-255");
            ImportIpRange("142.250.42.0-255");
            ImportIpRange("142.250.43.0-255");
            ImportIpRange("142.250.44.0-255");
            ImportIpRange("142.250.45.0-255");
            ImportIpRange("142.250.46.0-255");
            ImportIpRange("142.250.47.0-255");
            ImportIpRange("142.250.48.0-255");
            ImportIpRange("142.250.49.0-255");
            ImportIpRange("142.250.5.0-255");
            ImportIpRange("142.250.50.0-255");
            ImportIpRange("142.250.51.0-255");
            ImportIpRange("142.250.52.0-255");
            ImportIpRange("142.250.53.0-255");
            ImportIpRange("142.250.54.0-255");
            ImportIpRange("142.250.55.0-255");
            ImportIpRange("142.250.56.0-255");
            ImportIpRange("142.250.57.0-255");
            ImportIpRange("142.250.58.0-255");
            ImportIpRange("142.250.59.0-255");
            ImportIpRange("142.250.6.0-255");
            ImportIpRange("142.250.60.0-255");
            ImportIpRange("142.250.61.0-255");
            ImportIpRange("142.250.62.0-255");
            ImportIpRange("142.250.63.0-255");
            ImportIpRange("142.250.64.0-255");
            ImportIpRange("142.250.65.0-255");
            ImportIpRange("142.250.66.0-255");
            ImportIpRange("142.250.67.0-255");
            ImportIpRange("142.250.68.0-255");
            ImportIpRange("142.250.69.0-255");
            ImportIpRange("142.250.7.0-255");
            ImportIpRange("142.250.70.0-255");
            ImportIpRange("142.250.71.0-255");
            ImportIpRange("142.250.72.0-255");
            ImportIpRange("142.250.73.0-255");
            ImportIpRange("142.250.74.0-255");
            ImportIpRange("142.250.75.0-255");
            ImportIpRange("142.250.76.0-255");
            ImportIpRange("142.250.77.0-255");
            ImportIpRange("142.250.78.0-255");
            ImportIpRange("142.250.79.0-255");
            ImportIpRange("142.250.8.0-255");
            ImportIpRange("142.250.80.0-255");
            ImportIpRange("142.250.81.0-255");
            ImportIpRange("142.250.82.0-255");
            ImportIpRange("142.250.83.0-255");
            ImportIpRange("142.250.84.0-255");
            ImportIpRange("142.250.85.0-255");
            ImportIpRange("142.250.86.0-255");
            ImportIpRange("142.250.87.0-255");
            ImportIpRange("142.250.88.0-255");
            ImportIpRange("142.250.89.0-255");
            ImportIpRange("142.250.9.0-255");
            ImportIpRange("142.250.90.0-255");
            ImportIpRange("142.250.91.0-255");
            ImportIpRange("142.250.92.0-255");
            ImportIpRange("142.250.93.0-255");
            ImportIpRange("142.250.94.0-255");
            ImportIpRange("142.250.95.0-255");
            ImportIpRange("142.250.96.0-255");
            ImportIpRange("142.250.97.0-255");
            ImportIpRange("142.250.98.0-255");
            ImportIpRange("142.250.99.0-255");
            ImportIpRange("146.148.0.0-255");
            ImportIpRange("146.148.1.0-255");
            ImportIpRange("146.148.10.0-255");
            ImportIpRange("146.148.100.0-255");
            ImportIpRange("146.148.101.0-255");
            ImportIpRange("146.148.102.0-255");
            ImportIpRange("146.148.103.0-255");
            ImportIpRange("146.148.104.0-255");
            ImportIpRange("146.148.105.0-255");
            ImportIpRange("146.148.106.0-255");
            ImportIpRange("146.148.107.0-255");
            ImportIpRange("146.148.108.0-255");
            ImportIpRange("146.148.109.0-255");
            ImportIpRange("146.148.11.0-255");
            ImportIpRange("146.148.110.0-255");
            ImportIpRange("146.148.111.0-255");
            ImportIpRange("146.148.112.0-255");
            ImportIpRange("146.148.113.0-255");
            ImportIpRange("146.148.114.0-255");
            ImportIpRange("146.148.115.0-255");
            ImportIpRange("146.148.116.0-255");
            ImportIpRange("146.148.117.0-255");
            ImportIpRange("146.148.118.0-255");
            ImportIpRange("146.148.119.0-255");
            ImportIpRange("146.148.12.0-255");
            ImportIpRange("146.148.120.0-255");
            ImportIpRange("146.148.121.0-255");
            ImportIpRange("146.148.122.0-255");
            ImportIpRange("146.148.123.0-255");
            ImportIpRange("146.148.124.0-255");
            ImportIpRange("146.148.125.0-255");
            ImportIpRange("146.148.126.0-255");
            ImportIpRange("146.148.127.0-255");
            ImportIpRange("146.148.128.0-255");
            ImportIpRange("146.148.129.0-255");
            ImportIpRange("146.148.13.0-255");
            ImportIpRange("146.148.130.0-255");
            ImportIpRange("146.148.131.0-255");
            ImportIpRange("146.148.132.0-255");
            ImportIpRange("146.148.133.0-255");
            ImportIpRange("146.148.134.0-255");
            ImportIpRange("146.148.135.0-255");
            ImportIpRange("146.148.136.0-255");
            ImportIpRange("146.148.137.0-255");
            ImportIpRange("146.148.138.0-255");
            ImportIpRange("146.148.139.0-255");
            ImportIpRange("146.148.14.0-255");
            ImportIpRange("146.148.140.0-255");
            ImportIpRange("146.148.141.0-255");
            ImportIpRange("146.148.142.0-255");
            ImportIpRange("146.148.143.0-255");
            ImportIpRange("146.148.144.0-255");
            ImportIpRange("146.148.145.0-255");
            ImportIpRange("146.148.146.0-255");
            ImportIpRange("146.148.147.0-255");
            ImportIpRange("146.148.148.0-255");
            ImportIpRange("146.148.149.0-255");
            ImportIpRange("146.148.15.0-255");
            ImportIpRange("146.148.150.0-255");
            ImportIpRange("146.148.151.0-255");
            ImportIpRange("146.148.152.0-255");
            ImportIpRange("146.148.153.0-255");
            ImportIpRange("146.148.154.0-255");
            ImportIpRange("146.148.155.0-255");
            ImportIpRange("146.148.156.0-255");
            ImportIpRange("146.148.157.0-255");
            ImportIpRange("146.148.158.0-255");
            ImportIpRange("146.148.159.0-255");
            ImportIpRange("146.148.16.0-255");
            ImportIpRange("146.148.160.0-255");
            ImportIpRange("146.148.161.0-255");
            ImportIpRange("146.148.162.0-255");
            ImportIpRange("146.148.163.0-255");
            ImportIpRange("146.148.164.0-255");
            ImportIpRange("146.148.165.0-255");
            ImportIpRange("146.148.166.0-255");
            ImportIpRange("146.148.167.0-255");
            ImportIpRange("146.148.168.0-255");
            ImportIpRange("146.148.169.0-255");
            ImportIpRange("146.148.17.0-255");
            ImportIpRange("146.148.170.0-255");
            ImportIpRange("146.148.171.0-255");
            ImportIpRange("146.148.172.0-255");
            ImportIpRange("146.148.173.0-255");
            ImportIpRange("146.148.174.0-255");
            ImportIpRange("146.148.175.0-255");
            ImportIpRange("146.148.176.0-255");
            ImportIpRange("146.148.177.0-255");
            ImportIpRange("146.148.178.0-255");
            ImportIpRange("146.148.179.0-255");
            ImportIpRange("146.148.18.0-255");
            ImportIpRange("146.148.180.0-255");
            ImportIpRange("146.148.181.0-255");
            ImportIpRange("146.148.182.0-255");
            ImportIpRange("146.148.183.0-255");
            ImportIpRange("146.148.184.0-255");
            ImportIpRange("146.148.185.0-255");
            ImportIpRange("146.148.186.0-255");
            ImportIpRange("146.148.187.0-255");
            ImportIpRange("146.148.188.0-255");
            ImportIpRange("146.148.189.0-255");
            ImportIpRange("146.148.19.0-255");
            ImportIpRange("146.148.190.0-255");
            ImportIpRange("146.148.191.0-255");
            ImportIpRange("146.148.192.0-255");
            ImportIpRange("146.148.193.0-255");
            ImportIpRange("146.148.194.0-255");
            ImportIpRange("146.148.195.0-255");
            ImportIpRange("146.148.196.0-255");
            ImportIpRange("146.148.197.0-255");
            ImportIpRange("146.148.198.0-255");
            ImportIpRange("146.148.199.0-255");
            ImportIpRange("146.148.2.0-255");
            ImportIpRange("146.148.20.0-255");
            ImportIpRange("146.148.200.0-255");
            ImportIpRange("146.148.201.0-255");
            ImportIpRange("146.148.202.0-255");
            ImportIpRange("146.148.203.0-255");
            ImportIpRange("146.148.204.0-255");
            ImportIpRange("146.148.205.0-255");
            ImportIpRange("146.148.206.0-255");
            ImportIpRange("146.148.207.0-255");
            ImportIpRange("146.148.208.0-255");
            ImportIpRange("146.148.209.0-255");
            ImportIpRange("146.148.21.0-255");
            ImportIpRange("146.148.210.0-255");
            ImportIpRange("146.148.211.0-255");
            ImportIpRange("146.148.212.0-255");
            ImportIpRange("146.148.213.0-255");
            ImportIpRange("146.148.214.0-255");
            ImportIpRange("146.148.215.0-255");
            ImportIpRange("146.148.216.0-255");
            ImportIpRange("146.148.217.0-255");
            ImportIpRange("146.148.218.0-255");
            ImportIpRange("146.148.219.0-255");
            ImportIpRange("146.148.22.0-255");
            ImportIpRange("146.148.220.0-255");
            ImportIpRange("146.148.221.0-255");
            ImportIpRange("146.148.222.0-255");
            ImportIpRange("146.148.223.0-255");
            ImportIpRange("146.148.224.0-255");
            ImportIpRange("146.148.225.0-255");
            ImportIpRange("146.148.226.0-255");
            ImportIpRange("146.148.227.0-255");
            ImportIpRange("146.148.228.0-255");
            ImportIpRange("146.148.229.0-255");
            ImportIpRange("146.148.23.0-255");
            ImportIpRange("146.148.230.0-255");
            ImportIpRange("146.148.231.0-255");
            ImportIpRange("146.148.232.0-255");
            ImportIpRange("146.148.233.0-255");
            ImportIpRange("146.148.234.0-255");
            ImportIpRange("146.148.235.0-255");
            ImportIpRange("146.148.236.0-255");
            ImportIpRange("146.148.237.0-255");
            ImportIpRange("146.148.238.0-255");
            ImportIpRange("146.148.239.0-255");
            ImportIpRange("146.148.24.0-255");
            ImportIpRange("146.148.240.0-255");
            ImportIpRange("146.148.241.0-255");
            ImportIpRange("146.148.242.0-255");
            ImportIpRange("146.148.243.0-255");
            ImportIpRange("146.148.244.0-255");
            ImportIpRange("146.148.245.0-255");
            ImportIpRange("146.148.246.0-255");
            ImportIpRange("146.148.247.0-255");
            ImportIpRange("146.148.248.0-255");
            ImportIpRange("146.148.249.0-255");
            ImportIpRange("146.148.25.0-255");
            ImportIpRange("146.148.250.0-255");
            ImportIpRange("146.148.251.0-255");
            ImportIpRange("146.148.252.0-255");
            ImportIpRange("146.148.253.0-255");
            ImportIpRange("146.148.254.0-255");
            ImportIpRange("146.148.255.0-255");
            ImportIpRange("146.148.26.0-255");
            ImportIpRange("146.148.27.0-255");
            ImportIpRange("146.148.28.0-255");
            ImportIpRange("146.148.29.0-255");
            ImportIpRange("146.148.3.0-255");
            ImportIpRange("146.148.30.0-255");
            ImportIpRange("146.148.31.0-255");
            ImportIpRange("146.148.32.0-255");
            ImportIpRange("146.148.33.0-255");
            ImportIpRange("146.148.34.0-255");
            ImportIpRange("146.148.35.0-255");
            ImportIpRange("146.148.36.0-255");
            ImportIpRange("146.148.37.0-255");
            ImportIpRange("146.148.38.0-255");
            ImportIpRange("146.148.39.0-255");
            ImportIpRange("146.148.4.0-255");
            ImportIpRange("146.148.40.0-255");
            ImportIpRange("146.148.41.0-255");
            ImportIpRange("146.148.42.0-255");
            ImportIpRange("146.148.43.0-255");
            ImportIpRange("146.148.44.0-255");
            ImportIpRange("146.148.45.0-255");
            ImportIpRange("146.148.46.0-255");
            ImportIpRange("146.148.47.0-255");
            ImportIpRange("146.148.48.0-255");
            ImportIpRange("146.148.49.0-255");
            ImportIpRange("146.148.5.0-255");
            ImportIpRange("146.148.50.0-255");
            ImportIpRange("146.148.51.0-255");
            ImportIpRange("146.148.52.0-255");
            ImportIpRange("146.148.53.0-255");
            ImportIpRange("146.148.54.0-255");
            ImportIpRange("146.148.55.0-255");
            ImportIpRange("146.148.56.0-255");
            ImportIpRange("146.148.57.0-255");
            ImportIpRange("146.148.58.0-255");
            ImportIpRange("146.148.59.0-255");
            ImportIpRange("146.148.6.0-255");
            ImportIpRange("146.148.60.0-255");
            ImportIpRange("146.148.61.0-255");
            ImportIpRange("146.148.62.0-255");
            ImportIpRange("146.148.63.0-255");
            ImportIpRange("146.148.64.0-255");
            ImportIpRange("146.148.65.0-255");
            ImportIpRange("146.148.66.0-255");
            ImportIpRange("146.148.67.0-255");
            ImportIpRange("146.148.68.0-255");
            ImportIpRange("146.148.69.0-255");
            ImportIpRange("146.148.7.0-255");
            ImportIpRange("146.148.70.0-255");
            ImportIpRange("146.148.71.0-255");
            ImportIpRange("146.148.72.0-255");
            ImportIpRange("146.148.73.0-255");
            ImportIpRange("146.148.74.0-255");
            ImportIpRange("146.148.75.0-255");
            ImportIpRange("146.148.76.0-255");
            ImportIpRange("146.148.77.0-255");
            ImportIpRange("146.148.78.0-255");
            ImportIpRange("146.148.79.0-255");
            ImportIpRange("146.148.8.0-255");
            ImportIpRange("146.148.80.0-255");
            ImportIpRange("146.148.81.0-255");
            ImportIpRange("146.148.82.0-255");
            ImportIpRange("146.148.83.0-255");
            ImportIpRange("146.148.84.0-255");
            ImportIpRange("146.148.85.0-255");
            ImportIpRange("146.148.86.0-255");
            ImportIpRange("146.148.87.0-255");
            ImportIpRange("146.148.88.0-255");
            ImportIpRange("146.148.89.0-255");
            ImportIpRange("146.148.9.0-255");
            ImportIpRange("146.148.90.0-255");
            ImportIpRange("146.148.91.0-255");
            ImportIpRange("146.148.92.0-255");
            ImportIpRange("146.148.93.0-255");
            ImportIpRange("146.148.94.0-255");
            ImportIpRange("146.148.95.0-255");
            ImportIpRange("146.148.96.0-255");
            ImportIpRange("146.148.97.0-255");
            ImportIpRange("146.148.98.0-255");
            ImportIpRange("146.148.99.0-255");
            ImportIpRange("149.126.86.0-255");
            ImportIpRange("149.3.177.0-255");
            ImportIpRange("162.216.148.0-255");
            ImportIpRange("162.222.176.0-255");
            ImportIpRange("163.28.116.1-59");
            ImportIpRange("163.28.83.143-187");
            ImportIpRange("172.217.0.0-255");
            ImportIpRange("172.217.1.0-255");
            ImportIpRange("172.217.10.0-255");
            ImportIpRange("172.217.100.0-255");
            ImportIpRange("172.217.101.0-255");
            ImportIpRange("172.217.102.0-255");
            ImportIpRange("172.217.103.0-255");
            ImportIpRange("172.217.104.0-255");
            ImportIpRange("172.217.105.0-255");
            ImportIpRange("172.217.106.0-255");
            ImportIpRange("172.217.107.0-255");
            ImportIpRange("172.217.108.0-255");
            ImportIpRange("172.217.109.0-255");
            ImportIpRange("172.217.11.0-255");
            ImportIpRange("172.217.110.0-255");
            ImportIpRange("172.217.111.0-255");
            ImportIpRange("172.217.112.0-255");
            ImportIpRange("172.217.113.0-255");
            ImportIpRange("172.217.114.0-255");
            ImportIpRange("172.217.115.0-255");
            ImportIpRange("172.217.116.0-255");
            ImportIpRange("172.217.117.0-255");
            ImportIpRange("172.217.118.0-255");
            ImportIpRange("172.217.119.0-255");
            ImportIpRange("172.217.12.0-255");
            ImportIpRange("172.217.120.0-255");
            ImportIpRange("172.217.121.0-255");
            ImportIpRange("172.217.122.0-255");
            ImportIpRange("172.217.123.0-255");
            ImportIpRange("172.217.124.0-255");
            ImportIpRange("172.217.125.0-255");
            ImportIpRange("172.217.126.0-255");
            ImportIpRange("172.217.127.0-255");
            ImportIpRange("172.217.128.0-255");
            ImportIpRange("172.217.129.0-255");
            ImportIpRange("172.217.13.0-255");
            ImportIpRange("172.217.130.0-255");
            ImportIpRange("172.217.131.0-255");
            ImportIpRange("172.217.132.0-255");
            ImportIpRange("172.217.133.0-255");
            ImportIpRange("172.217.134.0-255");
            ImportIpRange("172.217.135.0-255");
            ImportIpRange("172.217.136.0-255");
            ImportIpRange("172.217.137.0-255");
            ImportIpRange("172.217.138.0-255");
            ImportIpRange("172.217.139.0-255");
            ImportIpRange("172.217.14.0-255");
            ImportIpRange("172.217.140.0-255");
            ImportIpRange("172.217.141.0-255");
            ImportIpRange("172.217.142.0-255");
            ImportIpRange("172.217.143.0-255");
            ImportIpRange("172.217.144.0-255");
            ImportIpRange("172.217.145.0-255");
            ImportIpRange("172.217.146.0-255");
            ImportIpRange("172.217.147.0-255");
            ImportIpRange("172.217.148.0-255");
            ImportIpRange("172.217.149.0-255");
            ImportIpRange("172.217.15.0-255");
            ImportIpRange("172.217.150.0-255");
            ImportIpRange("172.217.151.0-255");
            ImportIpRange("172.217.152.0-255");
            ImportIpRange("172.217.153.0-255");
            ImportIpRange("172.217.154.0-255");
            ImportIpRange("172.217.155.0-255");
            ImportIpRange("172.217.156.0-255");
            ImportIpRange("172.217.157.0-255");
            ImportIpRange("172.217.158.0-255");
            ImportIpRange("172.217.159.0-255");
            ImportIpRange("172.217.16.0-255");
            ImportIpRange("172.217.160.0-255");
            ImportIpRange("172.217.161.0-255");
            ImportIpRange("172.217.162.0-255");
            ImportIpRange("172.217.163.0-255");
            ImportIpRange("172.217.164.0-255");
            ImportIpRange("172.217.165.0-255");
            ImportIpRange("172.217.166.0-255");
            ImportIpRange("172.217.167.0-255");
            ImportIpRange("172.217.168.0-255");
            ImportIpRange("172.217.169.0-255");
            ImportIpRange("172.217.17.0-255");
            ImportIpRange("172.217.170.0-255");
            ImportIpRange("172.217.171.0-255");
            ImportIpRange("172.217.172.0-255");
            ImportIpRange("172.217.173.0-255");
            ImportIpRange("172.217.174.0-255");
            ImportIpRange("172.217.175.0-255");
            ImportIpRange("172.217.176.0-255");
            ImportIpRange("172.217.177.0-255");
            ImportIpRange("172.217.178.0-255");
            ImportIpRange("172.217.179.0-255");
            ImportIpRange("172.217.18.0-255");
            ImportIpRange("172.217.180.0-255");
            ImportIpRange("172.217.181.0-255");
            ImportIpRange("172.217.182.0-255");
            ImportIpRange("172.217.183.0-255");
            ImportIpRange("172.217.184.0-255");
            ImportIpRange("172.217.185.0-255");
            ImportIpRange("172.217.186.0-255");
            ImportIpRange("172.217.187.0-255");
            ImportIpRange("172.217.188.0-255");
            ImportIpRange("172.217.189.0-255");
            ImportIpRange("172.217.19.0-255");
            ImportIpRange("172.217.190.0-255");
            ImportIpRange("172.217.191.0-255");
            ImportIpRange("172.217.192.0-255");
            ImportIpRange("172.217.193.0-255");
            ImportIpRange("172.217.194.0-255");
            ImportIpRange("172.217.195.0-255");
            ImportIpRange("172.217.196.0-255");
            ImportIpRange("172.217.197.0-255");
            ImportIpRange("172.217.198.0-255");
            ImportIpRange("172.217.199.0-255");
            ImportIpRange("172.217.2.0-255");
            ImportIpRange("172.217.20.0-255");
            ImportIpRange("172.217.200.0-255");
            ImportIpRange("172.217.201.0-255");
            ImportIpRange("172.217.202.0-255");
            ImportIpRange("172.217.203.0-255");
            ImportIpRange("172.217.204.0-255");
            ImportIpRange("172.217.205.0-255");
            ImportIpRange("172.217.206.0-255");
            ImportIpRange("172.217.207.0-255");
            ImportIpRange("172.217.208.0-255");
            ImportIpRange("172.217.209.0-255");
            ImportIpRange("172.217.21.0-255");
            ImportIpRange("172.217.210.0-255");
            ImportIpRange("172.217.211.0-255");
            ImportIpRange("172.217.212.0-255");
            ImportIpRange("172.217.213.0-255");
            ImportIpRange("172.217.214.0-255");
            ImportIpRange("172.217.215.0-255");
            ImportIpRange("172.217.216.0-255");
            ImportIpRange("172.217.217.0-255");
            ImportIpRange("172.217.218.0-255");
            ImportIpRange("172.217.219.0-255");
            ImportIpRange("172.217.22.0-255");
            ImportIpRange("172.217.220.0-255");
            ImportIpRange("172.217.221.0-255");
            ImportIpRange("172.217.222.0-255");
            ImportIpRange("172.217.223.0-255");
            ImportIpRange("172.217.224.0-255");
            ImportIpRange("172.217.225.0-255");
            ImportIpRange("172.217.226.0-255");
            ImportIpRange("172.217.227.0-255");
            ImportIpRange("172.217.228.0-255");
            ImportIpRange("172.217.229.0-255");
            ImportIpRange("172.217.23.0-255");
            ImportIpRange("172.217.230.0-255");
            ImportIpRange("172.217.231.0-255");
            ImportIpRange("172.217.232.0-255");
            ImportIpRange("172.217.233.0-255");
            ImportIpRange("172.217.234.0-255");
            ImportIpRange("172.217.235.0-255");
            ImportIpRange("172.217.236.0-255");
            ImportIpRange("172.217.237.0-255");
            ImportIpRange("172.217.238.0-255");
            ImportIpRange("172.217.239.0-255");
            ImportIpRange("172.217.24.0-255");
            ImportIpRange("172.217.240.0-255");
            ImportIpRange("172.217.241.0-255");
            ImportIpRange("172.217.242.0-255");
            ImportIpRange("172.217.243.0-255");
            ImportIpRange("172.217.244.0-255");
            ImportIpRange("172.217.245.0-255");
            ImportIpRange("172.217.246.0-255");
            ImportIpRange("172.217.247.0-255");
            ImportIpRange("172.217.248.0-255");
            ImportIpRange("172.217.249.0-255");
            ImportIpRange("172.217.25.0-255");
            ImportIpRange("172.217.250.0-255");
            ImportIpRange("172.217.251.0-255");
            ImportIpRange("172.217.252.0-255");
            ImportIpRange("172.217.253.0-255");
            ImportIpRange("172.217.254.0-255");
            ImportIpRange("172.217.255.0-255");
            ImportIpRange("172.217.26.0-255");
            ImportIpRange("172.217.27.0-255");
            ImportIpRange("172.217.28.0-255");
            ImportIpRange("172.217.29.0-255");
            ImportIpRange("172.217.3.0-255");
            ImportIpRange("172.217.30.0-255");
            ImportIpRange("172.217.31.0-255");
            ImportIpRange("172.217.32.0-255");
            ImportIpRange("172.217.33.0-255");
            ImportIpRange("172.217.34.0-255");
            ImportIpRange("172.217.35.0-255");
            ImportIpRange("172.217.36.0-255");
            ImportIpRange("172.217.37.0-255");
            ImportIpRange("172.217.38.0-255");
            ImportIpRange("172.217.39.0-255");
            ImportIpRange("172.217.4.0-255");
            ImportIpRange("172.217.40.0-255");
            ImportIpRange("172.217.41.0-255");
            ImportIpRange("172.217.42.0-255");
            ImportIpRange("172.217.43.0-255");
            ImportIpRange("172.217.44.0-255");
            ImportIpRange("172.217.45.0-255");
            ImportIpRange("172.217.46.0-255");
            ImportIpRange("172.217.47.0-255");
            ImportIpRange("172.217.48.0-255");
            ImportIpRange("172.217.49.0-255");
            ImportIpRange("172.217.5.0-255");
            ImportIpRange("172.217.50.0-255");
            ImportIpRange("172.217.51.0-255");
            ImportIpRange("172.217.52.0-255");
            ImportIpRange("172.217.53.0-255");
            ImportIpRange("172.217.54.0-255");
            ImportIpRange("172.217.55.0-255");
            ImportIpRange("172.217.56.0-255");
            ImportIpRange("172.217.57.0-255");
            ImportIpRange("172.217.58.0-255");
            ImportIpRange("172.217.59.0-255");
            ImportIpRange("172.217.6.0-255");
            ImportIpRange("172.217.60.0-255");
            ImportIpRange("172.217.61.0-255");
            ImportIpRange("172.217.62.0-255");
            ImportIpRange("172.217.63.0-255");
            ImportIpRange("172.217.64.0-255");
            ImportIpRange("172.217.65.0-255");
            ImportIpRange("172.217.66.0-255");
            ImportIpRange("172.217.67.0-255");
            ImportIpRange("172.217.68.0-255");
            ImportIpRange("172.217.69.0-255");
            ImportIpRange("172.217.7.0-255");
            ImportIpRange("172.217.70.0-255");
            ImportIpRange("172.217.71.0-255");
            ImportIpRange("172.217.72.0-255");
            ImportIpRange("172.217.73.0-255");
            ImportIpRange("172.217.74.0-255");
            ImportIpRange("172.217.75.0-255");
            ImportIpRange("172.217.76.0-255");
            ImportIpRange("172.217.77.0-255");
            ImportIpRange("172.217.78.0-255");
            ImportIpRange("172.217.79.0-255");
            ImportIpRange("172.217.8.0-255");
            ImportIpRange("172.217.80.0-255");
            ImportIpRange("172.217.81.0-255");
            ImportIpRange("172.217.82.0-255");
            ImportIpRange("172.217.83.0-255");
            ImportIpRange("172.217.84.0-255");
            ImportIpRange("172.217.85.0-255");
            ImportIpRange("172.217.86.0-255");
            ImportIpRange("172.217.87.0-255");
            ImportIpRange("172.217.88.0-255");
            ImportIpRange("172.217.89.0-255");
            ImportIpRange("172.217.9.0-255");
            ImportIpRange("172.217.90.0-255");
            ImportIpRange("172.217.91.0-255");
            ImportIpRange("172.217.92.0-255");
            ImportIpRange("172.217.93.0-255");
            ImportIpRange("172.217.94.0-255");
            ImportIpRange("172.217.95.0-255");
            ImportIpRange("172.217.96.0-255");
            ImportIpRange("172.217.97.0-255");
            ImportIpRange("172.217.98.0-255");
            ImportIpRange("172.217.99.0-255");
            ImportIpRange("172.253.0.0-255");
            ImportIpRange("172.253.1.0-255");
            ImportIpRange("172.253.10.0-255");
            ImportIpRange("172.253.100.0-255");
            ImportIpRange("172.253.101.0-255");
            ImportIpRange("172.253.102.0-255");
            ImportIpRange("172.253.103.0-255");
            ImportIpRange("172.253.104.0-255");
            ImportIpRange("172.253.105.0-255");
            ImportIpRange("172.253.106.0-255");
            ImportIpRange("172.253.107.0-255");
            ImportIpRange("172.253.108.0-255");
            ImportIpRange("172.253.109.0-255");
            ImportIpRange("172.253.11.0-255");
            ImportIpRange("172.253.110.0-255");
            ImportIpRange("172.253.111.0-255");
            ImportIpRange("172.253.112.0-255");
            ImportIpRange("172.253.113.0-255");
            ImportIpRange("172.253.114.0-255");
            ImportIpRange("172.253.115.0-255");
            ImportIpRange("172.253.116.0-255");
            ImportIpRange("172.253.117.0-255");
            ImportIpRange("172.253.118.0-255");
            ImportIpRange("172.253.119.0-255");
            ImportIpRange("172.253.12.0-255");
            ImportIpRange("172.253.120.0-255");
            ImportIpRange("172.253.121.0-255");
            ImportIpRange("172.253.122.0-255");
            ImportIpRange("172.253.123.0-255");
            ImportIpRange("172.253.124.0-255");
            ImportIpRange("172.253.125.0-255");
            ImportIpRange("172.253.126.0-255");
            ImportIpRange("172.253.127.0-255");
            ImportIpRange("172.253.128.0-255");
            ImportIpRange("172.253.129.0-255");
            ImportIpRange("172.253.13.0-255");
            ImportIpRange("172.253.130.0-255");
            ImportIpRange("172.253.131.0-255");
            ImportIpRange("172.253.132.0-255");
            ImportIpRange("172.253.133.0-255");
            ImportIpRange("172.253.134.0-255");
            ImportIpRange("172.253.135.0-255");
            ImportIpRange("172.253.136.0-255");
            ImportIpRange("172.253.137.0-255");
            ImportIpRange("172.253.138.0-255");
            ImportIpRange("172.253.139.0-255");
            ImportIpRange("172.253.14.0-255");
            ImportIpRange("172.253.140.0-255");
            ImportIpRange("172.253.141.0-255");
            ImportIpRange("172.253.142.0-255");
            ImportIpRange("172.253.143.0-255");
            ImportIpRange("172.253.144.0-255");
            ImportIpRange("172.253.145.0-255");
            ImportIpRange("172.253.146.0-255");
            ImportIpRange("172.253.147.0-255");
            ImportIpRange("172.253.148.0-255");
            ImportIpRange("172.253.149.0-255");
            ImportIpRange("172.253.15.0-255");
            ImportIpRange("172.253.150.0-255");
            ImportIpRange("172.253.151.0-255");
            ImportIpRange("172.253.152.0-255");
            ImportIpRange("172.253.153.0-255");
            ImportIpRange("172.253.154.0-255");
            ImportIpRange("172.253.155.0-255");
            ImportIpRange("172.253.156.0-255");
            ImportIpRange("172.253.157.0-255");
            ImportIpRange("172.253.158.0-255");
            ImportIpRange("172.253.159.0-255");
            ImportIpRange("172.253.16.0-255");
            ImportIpRange("172.253.160.0-255");
            ImportIpRange("172.253.161.0-255");
            ImportIpRange("172.253.162.0-255");
            ImportIpRange("172.253.163.0-255");
            ImportIpRange("172.253.164.0-255");
            ImportIpRange("172.253.165.0-255");
            ImportIpRange("172.253.166.0-255");
            ImportIpRange("172.253.167.0-255");
            ImportIpRange("172.253.168.0-255");
            ImportIpRange("172.253.169.0-255");
            ImportIpRange("172.253.17.0-255");
            ImportIpRange("172.253.170.0-255");
            ImportIpRange("172.253.171.0-255");
            ImportIpRange("172.253.172.0-255");
            ImportIpRange("172.253.173.0-255");
            ImportIpRange("172.253.174.0-255");
            ImportIpRange("172.253.175.0-255");
            ImportIpRange("172.253.176.0-255");
            ImportIpRange("172.253.177.0-255");
            ImportIpRange("172.253.178.0-255");
            ImportIpRange("172.253.179.0-255");
            ImportIpRange("172.253.18.0-255");
            ImportIpRange("172.253.180.0-255");
            ImportIpRange("172.253.181.0-255");
            ImportIpRange("172.253.182.0-255");
            ImportIpRange("172.253.183.0-255");
            ImportIpRange("172.253.184.0-255");
            ImportIpRange("172.253.185.0-255");
            ImportIpRange("172.253.186.0-255");
            ImportIpRange("172.253.187.0-255");
            ImportIpRange("172.253.188.0-255");
            ImportIpRange("172.253.189.0-255");
            ImportIpRange("172.253.19.0-255");
            ImportIpRange("172.253.190.0-255");
            ImportIpRange("172.253.191.0-255");
            ImportIpRange("172.253.192.0-255");
            ImportIpRange("172.253.193.0-255");
            ImportIpRange("172.253.194.0-255");
            ImportIpRange("172.253.195.0-255");
            ImportIpRange("172.253.196.0-255");
            ImportIpRange("172.253.197.0-255");
            ImportIpRange("172.253.198.0-255");
            ImportIpRange("172.253.199.0-255");
            ImportIpRange("172.253.2.0-255");
            ImportIpRange("172.253.20.0-255");
            ImportIpRange("172.253.200.0-255");
            ImportIpRange("172.253.201.0-255");
            ImportIpRange("172.253.202.0-255");
            ImportIpRange("172.253.203.0-255");
            ImportIpRange("172.253.204.0-255");
            ImportIpRange("172.253.205.0-255");
            ImportIpRange("172.253.206.0-255");
            ImportIpRange("172.253.207.0-255");
            ImportIpRange("172.253.208.0-255");
            ImportIpRange("172.253.209.0-255");
            ImportIpRange("172.253.21.0-255");
            ImportIpRange("172.253.210.0-255");
            ImportIpRange("172.253.211.0-255");
            ImportIpRange("172.253.212.0-255");
            ImportIpRange("172.253.213.0-255");
            ImportIpRange("172.253.214.0-255");
            ImportIpRange("172.253.215.0-255");
            ImportIpRange("172.253.216.0-255");
            ImportIpRange("172.253.217.0-255");
            ImportIpRange("172.253.218.0-255");
            ImportIpRange("172.253.219.0-255");
            ImportIpRange("172.253.22.0-255");
            ImportIpRange("172.253.220.0-255");
            ImportIpRange("172.253.221.0-255");
            ImportIpRange("172.253.222.0-255");
            ImportIpRange("172.253.223.0-255");
            ImportIpRange("172.253.224.0-255");
            ImportIpRange("172.253.225.0-255");
            ImportIpRange("172.253.226.0-255");
            ImportIpRange("172.253.227.0-255");
            ImportIpRange("172.253.228.0-255");
            ImportIpRange("172.253.229.0-255");
            ImportIpRange("172.253.23.0-255");
            ImportIpRange("172.253.230.0-255");
            ImportIpRange("172.253.231.0-255");
            ImportIpRange("172.253.232.0-255");
            ImportIpRange("172.253.233.0-255");
            ImportIpRange("172.253.234.0-255");
            ImportIpRange("172.253.235.0-255");
            ImportIpRange("172.253.236.0-255");
            ImportIpRange("172.253.237.0-255");
            ImportIpRange("172.253.238.0-255");
            ImportIpRange("172.253.239.0-255");
            ImportIpRange("172.253.24.0-255");
            ImportIpRange("172.253.240.0-255");
            ImportIpRange("172.253.241.0-255");
            ImportIpRange("172.253.242.0-255");
            ImportIpRange("172.253.243.0-255");
            ImportIpRange("172.253.244.0-255");
            ImportIpRange("172.253.245.0-255");
            ImportIpRange("172.253.246.0-255");
            ImportIpRange("172.253.247.0-255");
            ImportIpRange("172.253.248.0-255");
            ImportIpRange("172.253.249.0-255");
            ImportIpRange("172.253.25.0-255");
            ImportIpRange("172.253.250.0-255");
            ImportIpRange("172.253.251.0-255");
            ImportIpRange("172.253.252.0-255");
            ImportIpRange("172.253.253.0-255");
            ImportIpRange("172.253.254.0-255");
            ImportIpRange("172.253.255.0-255");
            ImportIpRange("172.253.26.0-255");
            ImportIpRange("172.253.27.0-255");
            ImportIpRange("172.253.28.0-255");
            ImportIpRange("172.253.29.0-255");
            ImportIpRange("172.253.3.0-255");
            ImportIpRange("172.253.30.0-255");
            ImportIpRange("172.253.31.0-255");
            ImportIpRange("172.253.32.0-255");
            ImportIpRange("172.253.33.0-255");
            ImportIpRange("172.253.34.0-255");
            ImportIpRange("172.253.35.0-255");
            ImportIpRange("172.253.36.0-255");
            ImportIpRange("172.253.37.0-255");
            ImportIpRange("172.253.38.0-255");
            ImportIpRange("172.253.39.0-255");
            ImportIpRange("172.253.4.0-255");
            ImportIpRange("172.253.40.0-255");
            ImportIpRange("172.253.41.0-255");
            ImportIpRange("172.253.42.0-255");
            ImportIpRange("172.253.43.0-255");
            ImportIpRange("172.253.44.0-255");
            ImportIpRange("172.253.45.0-255");
            ImportIpRange("172.253.46.0-255");
            ImportIpRange("172.253.47.0-255");
            ImportIpRange("172.253.48.0-255");
            ImportIpRange("172.253.49.0-255");
            ImportIpRange("172.253.5.0-255");
            ImportIpRange("172.253.50.0-255");
            ImportIpRange("172.253.51.0-255");
            ImportIpRange("172.253.52.0-255");
            ImportIpRange("172.253.53.0-255");
            ImportIpRange("172.253.54.0-255");
            ImportIpRange("172.253.55.0-255");
            ImportIpRange("172.253.56.0-255");
            ImportIpRange("172.253.57.0-255");
            ImportIpRange("172.253.58.0-255");
            ImportIpRange("172.253.59.0-255");
            ImportIpRange("172.253.6.0-255");
            ImportIpRange("172.253.60.0-255");
            ImportIpRange("172.253.61.0-255");
            ImportIpRange("172.253.62.0-255");
            ImportIpRange("172.253.63.0-255");
            ImportIpRange("172.253.64.0-255");
            ImportIpRange("172.253.65.0-255");
            ImportIpRange("172.253.66.0-255");
            ImportIpRange("172.253.67.0-255");
            ImportIpRange("172.253.68.0-255");
            ImportIpRange("172.253.69.0-255");
            ImportIpRange("172.253.7.0-255");
            ImportIpRange("172.253.70.0-255");
            ImportIpRange("172.253.71.0-255");
            ImportIpRange("172.253.72.0-255");
            ImportIpRange("172.253.73.0-255");
            ImportIpRange("172.253.74.0-255");
            ImportIpRange("172.253.75.0-255");
            ImportIpRange("172.253.76.0-255");
            ImportIpRange("172.253.77.0-255");
            ImportIpRange("172.253.78.0-255");
            ImportIpRange("172.253.79.0-255");
            ImportIpRange("172.253.8.0-255");
            ImportIpRange("172.253.80.0-255");
            ImportIpRange("172.253.81.0-255");
            ImportIpRange("172.253.82.0-255");
            ImportIpRange("172.253.83.0-255");
            ImportIpRange("172.253.84.0-255");
            ImportIpRange("172.253.85.0-255");
            ImportIpRange("172.253.86.0-255");
            ImportIpRange("172.253.87.0-255");
            ImportIpRange("172.253.88.0-255");
            ImportIpRange("172.253.89.0-255");
            ImportIpRange("172.253.9.0-255");
            ImportIpRange("172.253.90.0-255");
            ImportIpRange("172.253.91.0-255");
            ImportIpRange("172.253.92.0-255");
            ImportIpRange("172.253.93.0-255");
            ImportIpRange("172.253.94.0-255");
            ImportIpRange("172.253.95.0-255");
            ImportIpRange("172.253.96.0-255");
            ImportIpRange("172.253.97.0-255");
            ImportIpRange("172.253.98.0-255");
            ImportIpRange("172.253.99.0-255");
            ImportIpRange("173.194.0.0-255");
            ImportIpRange("173.194.1.0-255");
            ImportIpRange("173.194.10.0-255");
            ImportIpRange("173.194.100.0-255");
            ImportIpRange("173.194.101.0-255");
            ImportIpRange("173.194.102.0-255");
            ImportIpRange("173.194.103.0-255");
            ImportIpRange("173.194.104.0-255");
            ImportIpRange("173.194.105.0-255");
            ImportIpRange("173.194.106.0-255");
            ImportIpRange("173.194.107.0-255");
            ImportIpRange("173.194.108.0-255");
            ImportIpRange("173.194.109.0-255");
            ImportIpRange("173.194.11.0-255");
            ImportIpRange("173.194.110.0-255");
            ImportIpRange("173.194.111.0-255");
            ImportIpRange("173.194.112.0-255");
            ImportIpRange("173.194.113.0-255");
            ImportIpRange("173.194.114.0-255");
            ImportIpRange("173.194.115.0-255");
            ImportIpRange("173.194.116.0-255");
            ImportIpRange("173.194.117.0-255");
            ImportIpRange("173.194.118.0-255");
            ImportIpRange("173.194.119.0-255");
            ImportIpRange("173.194.12.0-255");
            ImportIpRange("173.194.120.0-255");
            ImportIpRange("173.194.121.0-255");
            ImportIpRange("173.194.122.0-255");
            ImportIpRange("173.194.123.0-255");
            ImportIpRange("173.194.124.0-255");
            ImportIpRange("173.194.125.0-255");
            ImportIpRange("173.194.126.0-255");
            ImportIpRange("173.194.127.0-255");
            ImportIpRange("173.194.128.0-255");
            ImportIpRange("173.194.129.0-255");
            ImportIpRange("173.194.13.0-255");
            ImportIpRange("173.194.130.0-255");
            ImportIpRange("173.194.131.0-255");
            ImportIpRange("173.194.132.0-255");
            ImportIpRange("173.194.133.0-255");
            ImportIpRange("173.194.134.0-255");
            ImportIpRange("173.194.135.0-255");
            ImportIpRange("173.194.136.0-255");
            ImportIpRange("173.194.137.0-255");
            ImportIpRange("173.194.138.0-255");
            ImportIpRange("173.194.139.0-255");
            ImportIpRange("173.194.14.0-255");
            ImportIpRange("173.194.140.0-255");
            ImportIpRange("173.194.141.0-255");
            ImportIpRange("173.194.142.0-255");
            ImportIpRange("173.194.143.0-255");
            ImportIpRange("173.194.144.0-255");
            ImportIpRange("173.194.145.0-255");
            ImportIpRange("173.194.146.0-255");
            ImportIpRange("173.194.147.0-255");
            ImportIpRange("173.194.148.0-255");
            ImportIpRange("173.194.149.0-255");
            ImportIpRange("173.194.15.0-255");
            ImportIpRange("173.194.150.0-255");
            ImportIpRange("173.194.151.0-255");
            ImportIpRange("173.194.152.0-255");
            ImportIpRange("173.194.153.0-255");
            ImportIpRange("173.194.154.0-255");
            ImportIpRange("173.194.155.0-255");
            ImportIpRange("173.194.156.0-255");
            ImportIpRange("173.194.157.0-255");
            ImportIpRange("173.194.158.0-255");
            ImportIpRange("173.194.159.0-255");
            ImportIpRange("173.194.16.0-255");
            ImportIpRange("173.194.160.0-255");
            ImportIpRange("173.194.161.0-255");
            ImportIpRange("173.194.162.0-255");
            ImportIpRange("173.194.163.0-255");
            ImportIpRange("173.194.164.0-255");
            ImportIpRange("173.194.165.0-255");
            ImportIpRange("173.194.166.0-255");
            ImportIpRange("173.194.167.0-255");
            ImportIpRange("173.194.168.0-255");
            ImportIpRange("173.194.169.0-255");
            ImportIpRange("173.194.17.0-255");
            ImportIpRange("173.194.170.0-255");
            ImportIpRange("173.194.171.0-255");
            ImportIpRange("173.194.172.0-255");
            ImportIpRange("173.194.173.0-255");
            ImportIpRange("173.194.174.0-255");
            ImportIpRange("173.194.175.0-255");
            ImportIpRange("173.194.176.0-255");
            ImportIpRange("173.194.177.0-255");
            ImportIpRange("173.194.178.0-255");
            ImportIpRange("173.194.179.0-255");
            ImportIpRange("173.194.18.0-255");
            ImportIpRange("173.194.180.0-255");
            ImportIpRange("173.194.181.0-255");
            ImportIpRange("173.194.182.0-255");
            ImportIpRange("173.194.183.0-255");
            ImportIpRange("173.194.184.0-255");
            ImportIpRange("173.194.185.0-255");
            ImportIpRange("173.194.186.0-255");
            ImportIpRange("173.194.187.0-255");
            ImportIpRange("173.194.188.0-255");
            ImportIpRange("173.194.189.0-255");
            ImportIpRange("173.194.19.0-255");
            ImportIpRange("173.194.190.0-255");
            ImportIpRange("173.194.191.0-255");
            ImportIpRange("173.194.192.0-255");
            ImportIpRange("173.194.193.0-255");
            ImportIpRange("173.194.194.0-255");
            ImportIpRange("173.194.195.0-255");
            ImportIpRange("173.194.196.0-255");
            ImportIpRange("173.194.197.0-255");
            ImportIpRange("173.194.198.0-255");
            ImportIpRange("173.194.199.0-255");
            ImportIpRange("173.194.2.0-255");
            ImportIpRange("173.194.20.0-255");
            ImportIpRange("173.194.200.0-255");
            ImportIpRange("173.194.201.0-255");
            ImportIpRange("173.194.202.0-255");
            ImportIpRange("173.194.203.0-255");
            ImportIpRange("173.194.204.0-255");
            ImportIpRange("173.194.205.0-255");
            ImportIpRange("173.194.206.0-255");
            ImportIpRange("173.194.207.0-255");
            ImportIpRange("173.194.208.0-255");
            ImportIpRange("173.194.209.0-255");
            ImportIpRange("173.194.21.0-255");
            ImportIpRange("173.194.210.0-255");
            ImportIpRange("173.194.211.0-255");
            ImportIpRange("173.194.212.0-255");
            ImportIpRange("173.194.213.0-255");
            ImportIpRange("173.194.214.0-255");
            ImportIpRange("173.194.215.0-255");
            ImportIpRange("173.194.216.0-255");
            ImportIpRange("173.194.217.0-255");
            ImportIpRange("173.194.218.0-255");
            ImportIpRange("173.194.219.0-255");
            ImportIpRange("173.194.22.0-255");
            ImportIpRange("173.194.220.0-255");
            ImportIpRange("173.194.221.0-255");
            ImportIpRange("173.194.222.0-255");
            ImportIpRange("173.194.223.0-255");
            ImportIpRange("173.194.224.0-255");
            ImportIpRange("173.194.225.0-255");
            ImportIpRange("173.194.226.0-255");
            ImportIpRange("173.194.227.0-255");
            ImportIpRange("173.194.228.0-255");
            ImportIpRange("173.194.229.0-255");
            ImportIpRange("173.194.23.0-255");
            ImportIpRange("173.194.230.0-255");
            ImportIpRange("173.194.231.0-255");
            ImportIpRange("173.194.232.0-255");
            ImportIpRange("173.194.233.0-255");
            ImportIpRange("173.194.234.0-255");
            ImportIpRange("173.194.235.0-255");
            ImportIpRange("173.194.236.0-255");
            ImportIpRange("173.194.237.0-255");
            ImportIpRange("173.194.238.0-255");
            ImportIpRange("173.194.239.0-255");
            ImportIpRange("173.194.24.0-255");
            ImportIpRange("173.194.240.0-255");
            ImportIpRange("173.194.241.0-255");
            ImportIpRange("173.194.242.0-255");
            ImportIpRange("173.194.243.0-255");
            ImportIpRange("173.194.244.0-255");
            ImportIpRange("173.194.245.0-255");
            ImportIpRange("173.194.246.0-255");
            ImportIpRange("173.194.247.0-255");
            ImportIpRange("173.194.248.0-255");
            ImportIpRange("173.194.249.0-255");
            ImportIpRange("173.194.25.0-255");
            ImportIpRange("173.194.250.0-255");
            ImportIpRange("173.194.251.0-255");
            ImportIpRange("173.194.252.0-255");
            ImportIpRange("173.194.253.0-255");
            ImportIpRange("173.194.254.0-255");
            ImportIpRange("173.194.255.0-255");
            ImportIpRange("173.194.26.0-255");
            ImportIpRange("173.194.27.0-255");
            ImportIpRange("173.194.28.0-255");
            ImportIpRange("173.194.29.0-255");
            ImportIpRange("173.194.3.0-255");
            ImportIpRange("173.194.30.0-255");
            ImportIpRange("173.194.31.0-255");
            ImportIpRange("173.194.32.0-255");
            ImportIpRange("173.194.33.0-255");
            ImportIpRange("173.194.34.0-255");
            ImportIpRange("173.194.35.0-255");
            ImportIpRange("173.194.36.0-255");
            ImportIpRange("173.194.37.0-255");
            ImportIpRange("173.194.38.0-255");
            ImportIpRange("173.194.39.0-255");
            ImportIpRange("173.194.4.0-255");
            ImportIpRange("173.194.40.0-255");
            ImportIpRange("173.194.41.0-255");
            ImportIpRange("173.194.42.0-255");
            ImportIpRange("173.194.43.0-255");
            ImportIpRange("173.194.44.0-255");
            ImportIpRange("173.194.45.0-255");
            ImportIpRange("173.194.46.0-255");
            ImportIpRange("173.194.47.0-255");
            ImportIpRange("173.194.48.0-255");
            ImportIpRange("173.194.49.0-255");
            ImportIpRange("173.194.5.0-255");
            ImportIpRange("173.194.50.0-255");
            ImportIpRange("173.194.51.0-255");
            ImportIpRange("173.194.52.0-255");
            ImportIpRange("173.194.53.0-255");
            ImportIpRange("173.194.54.0-255");
            ImportIpRange("173.194.55.0-255");
            ImportIpRange("173.194.56.0-255");
            ImportIpRange("173.194.57.0-255");
            ImportIpRange("173.194.58.0-255");
            ImportIpRange("173.194.59.0-255");
            ImportIpRange("173.194.6.0-255");
            ImportIpRange("173.194.60.0-255");
            ImportIpRange("173.194.61.0-255");
            ImportIpRange("173.194.62.0-255");
            ImportIpRange("173.194.63.0-255");
            ImportIpRange("173.194.64.0-255");
            ImportIpRange("173.194.65.0-255");
            ImportIpRange("173.194.66.0-255");
            ImportIpRange("173.194.67.0-255");
            ImportIpRange("173.194.68.0-255");
            ImportIpRange("173.194.69.0-255");
            ImportIpRange("173.194.7.0-255");
            ImportIpRange("173.194.70.0-255");
            ImportIpRange("173.194.71.0-255");
            ImportIpRange("173.194.72.0-255");
            ImportIpRange("173.194.73.0-255");
            ImportIpRange("173.194.74.0-255");
            ImportIpRange("173.194.75.0-255");
            ImportIpRange("173.194.76.0-255");
            ImportIpRange("173.194.77.0-255");
            ImportIpRange("173.194.78.0-255");
            ImportIpRange("173.194.79.0-255");
            ImportIpRange("173.194.8.0-255");
            ImportIpRange("173.194.80.0-255");
            ImportIpRange("173.194.81.0-255");
            ImportIpRange("173.194.82.0-255");
            ImportIpRange("173.194.83.0-255");
            ImportIpRange("173.194.84.0-255");
            ImportIpRange("173.194.85.0-255");
            ImportIpRange("173.194.86.0-255");
            ImportIpRange("173.194.87.0-255");
            ImportIpRange("173.194.88.0-255");
            ImportIpRange("173.194.89.0-255");
            ImportIpRange("173.194.9.0-255");
            ImportIpRange("173.194.90.0-255");
            ImportIpRange("173.194.91.0-255");
            ImportIpRange("173.194.92.0-255");
            ImportIpRange("173.194.93.0-255");
            ImportIpRange("173.194.94.0-255");
            ImportIpRange("173.194.95.0-255");
            ImportIpRange("173.194.96.0-255");
            ImportIpRange("173.194.97.0-255");
            ImportIpRange("173.194.98.0-255");
            ImportIpRange("173.194.99.0-255");
            ImportIpRange("173.255.112.0-255");
            ImportIpRange("178.45.251.4-123");
            ImportIpRange("178.60.128.1-63");
            ImportIpRange("192.119.16.0-255");
            ImportIpRange("192.119.20.0-255");
            ImportIpRange("192.119.21.0-255");
            ImportIpRange("192.158.28.0-255");
            ImportIpRange("192.178.0.0-255");
            ImportIpRange("192.178.1.0-255");
            ImportIpRange("192.178.10.0-255");
            ImportIpRange("192.178.100.0-255");
            ImportIpRange("192.178.101.0-255");
            ImportIpRange("192.178.102.0-255");
            ImportIpRange("192.178.103.0-255");
            ImportIpRange("192.178.104.0-255");
            ImportIpRange("192.178.105.0-255");
            ImportIpRange("192.178.106.0-255");
            ImportIpRange("192.178.107.0-255");
            ImportIpRange("192.178.108.0-255");
            ImportIpRange("192.178.109.0-255");
            ImportIpRange("192.178.11.0-255");
            ImportIpRange("192.178.110.0-255");
            ImportIpRange("192.178.111.0-255");
            ImportIpRange("192.178.112.0-255");
            ImportIpRange("192.178.113.0-255");
            ImportIpRange("192.178.114.0-255");
            ImportIpRange("192.178.115.0-255");
            ImportIpRange("192.178.116.0-255");
            ImportIpRange("192.178.117.0-255");
            ImportIpRange("192.178.118.0-255");
            ImportIpRange("192.178.119.0-255");
            ImportIpRange("192.178.12.0-255");
            ImportIpRange("192.178.120.0-255");
            ImportIpRange("192.178.121.0-255");
            ImportIpRange("192.178.122.0-255");
            ImportIpRange("192.178.123.0-255");
            ImportIpRange("192.178.124.0-255");
            ImportIpRange("192.178.125.0-255");
            ImportIpRange("192.178.126.0-255");
            ImportIpRange("192.178.127.0-255");
            ImportIpRange("192.178.128.0-255");
            ImportIpRange("192.178.129.0-255");
            ImportIpRange("192.178.13.0-255");
            ImportIpRange("192.178.130.0-255");
            ImportIpRange("192.178.131.0-255");
            ImportIpRange("192.178.132.0-255");
            ImportIpRange("192.178.133.0-255");
            ImportIpRange("192.178.134.0-255");
            ImportIpRange("192.178.135.0-255");
            ImportIpRange("192.178.136.0-255");
            ImportIpRange("192.178.137.0-255");
            ImportIpRange("192.178.138.0-255");
            ImportIpRange("192.178.139.0-255");
            ImportIpRange("192.178.14.0-255");
            ImportIpRange("192.178.140.0-255");
            ImportIpRange("192.178.141.0-255");
            ImportIpRange("192.178.142.0-255");
            ImportIpRange("192.178.143.0-255");
            ImportIpRange("192.178.144.0-255");
            ImportIpRange("192.178.145.0-255");
            ImportIpRange("192.178.146.0-255");
            ImportIpRange("192.178.147.0-255");
            ImportIpRange("192.178.148.0-255");
            ImportIpRange("192.178.149.0-255");
            ImportIpRange("192.178.15.0-255");
            ImportIpRange("192.178.150.0-255");
            ImportIpRange("192.178.151.0-255");
            ImportIpRange("192.178.152.0-255");
            ImportIpRange("192.178.153.0-255");
            ImportIpRange("192.178.154.0-255");
            ImportIpRange("192.178.155.0-255");
            ImportIpRange("192.178.156.0-255");
            ImportIpRange("192.178.157.0-255");
            ImportIpRange("192.178.158.0-255");
            ImportIpRange("192.178.159.0-255");
            ImportIpRange("192.178.16.0-255");
            ImportIpRange("192.178.160.0-255");
            ImportIpRange("192.178.161.0-255");
            ImportIpRange("192.178.162.0-255");
            ImportIpRange("192.178.163.0-255");
            ImportIpRange("192.178.164.0-255");
            ImportIpRange("192.178.165.0-255");
            ImportIpRange("192.178.166.0-255");
            ImportIpRange("192.178.167.0-255");
            ImportIpRange("192.178.168.0-255");
            ImportIpRange("192.178.169.0-255");
            ImportIpRange("192.178.17.0-255");
            ImportIpRange("192.178.170.0-255");
            ImportIpRange("192.178.171.0-255");
            ImportIpRange("192.178.172.0-255");
            ImportIpRange("192.178.173.0-255");
            ImportIpRange("192.178.174.0-255");
            ImportIpRange("192.178.175.0-255");
            ImportIpRange("192.178.176.0-255");
            ImportIpRange("192.178.177.0-255");
            ImportIpRange("192.178.178.0-255");
            ImportIpRange("192.178.179.0-255");
            ImportIpRange("192.178.18.0-255");
            ImportIpRange("192.178.180.0-255");
            ImportIpRange("192.178.181.0-255");
            ImportIpRange("192.178.182.0-255");
            ImportIpRange("192.178.183.0-255");
            ImportIpRange("192.178.184.0-255");
            ImportIpRange("192.178.185.0-255");
            ImportIpRange("192.178.186.0-255");
            ImportIpRange("192.178.187.0-255");
            ImportIpRange("192.178.188.0-255");
            ImportIpRange("192.178.189.0-255");
            ImportIpRange("192.178.19.0-255");
            ImportIpRange("192.178.190.0-255");
            ImportIpRange("192.178.191.0-255");
            ImportIpRange("192.178.192.0-255");
            ImportIpRange("192.178.193.0-255");
            ImportIpRange("192.178.194.0-255");
            ImportIpRange("192.178.195.0-255");
            ImportIpRange("192.178.196.0-255");
            ImportIpRange("192.178.197.0-255");
            ImportIpRange("192.178.198.0-255");
            ImportIpRange("192.178.199.0-255");
            ImportIpRange("192.178.2.0-255");
            ImportIpRange("192.178.20.0-255");
            ImportIpRange("192.178.200.0-255");
            ImportIpRange("192.178.201.0-255");
            ImportIpRange("192.178.202.0-255");
            ImportIpRange("192.178.203.0-255");
            ImportIpRange("192.178.204.0-255");
            ImportIpRange("192.178.205.0-255");
            ImportIpRange("192.178.206.0-255");
            ImportIpRange("192.178.207.0-255");
            ImportIpRange("192.178.208.0-255");
            ImportIpRange("192.178.209.0-255");
            ImportIpRange("192.178.21.0-255");
            ImportIpRange("192.178.210.0-255");
            ImportIpRange("192.178.211.0-255");
            ImportIpRange("192.178.212.0-255");
            ImportIpRange("192.178.213.0-255");
            ImportIpRange("192.178.214.0-255");
            ImportIpRange("192.178.215.0-255");
            ImportIpRange("192.178.216.0-255");
            ImportIpRange("192.178.217.0-255");
            ImportIpRange("192.178.218.0-255");
            ImportIpRange("192.178.219.0-255");
            ImportIpRange("192.178.22.0-255");
            ImportIpRange("192.178.220.0-255");
            ImportIpRange("192.178.221.0-255");
            ImportIpRange("192.178.222.0-255");
            ImportIpRange("192.178.223.0-255");
            ImportIpRange("192.178.224.0-255");
            ImportIpRange("192.178.225.0-255");
            ImportIpRange("192.178.226.0-255");
            ImportIpRange("192.178.227.0-255");
            ImportIpRange("192.178.228.0-255");
            ImportIpRange("192.178.229.0-255");
            ImportIpRange("192.178.23.0-255");
            ImportIpRange("192.178.230.0-255");
            ImportIpRange("192.178.231.0-255");
            ImportIpRange("192.178.232.0-255");
            ImportIpRange("192.178.233.0-255");
            ImportIpRange("192.178.234.0-255");
            ImportIpRange("192.178.235.0-255");
            ImportIpRange("192.178.236.0-255");
            ImportIpRange("192.178.237.0-255");
            ImportIpRange("192.178.238.0-255");
            ImportIpRange("192.178.239.0-255");
            ImportIpRange("192.178.24.0-255");
            ImportIpRange("192.178.240.0-255");
            ImportIpRange("192.178.241.0-255");
            ImportIpRange("192.178.242.0-255");
            ImportIpRange("192.178.243.0-255");
            ImportIpRange("192.178.244.0-255");
            ImportIpRange("192.178.245.0-255");
            ImportIpRange("192.178.246.0-255");
            ImportIpRange("192.178.247.0-255");
            ImportIpRange("192.178.248.0-255");
            ImportIpRange("192.178.249.0-255");
            ImportIpRange("192.178.25.0-255");
            ImportIpRange("192.178.250.0-255");
            ImportIpRange("192.178.251.0-255");
            ImportIpRange("192.178.252.0-255");
            ImportIpRange("192.178.253.0-255");
            ImportIpRange("192.178.254.0-255");
            ImportIpRange("192.178.255.0-255");
            ImportIpRange("192.178.26.0-255");
            ImportIpRange("192.178.27.0-255");
            ImportIpRange("192.178.28.0-255");
            ImportIpRange("192.178.29.0-255");
            ImportIpRange("192.178.3.0-255");
            ImportIpRange("192.178.30.0-255");
            ImportIpRange("192.178.31.0-255");
            ImportIpRange("192.178.32.0-255");
            ImportIpRange("192.178.33.0-255");
            ImportIpRange("192.178.34.0-255");
            ImportIpRange("192.178.35.0-255");
            ImportIpRange("192.178.36.0-255");
            ImportIpRange("192.178.37.0-255");
            ImportIpRange("192.178.38.0-255");
            ImportIpRange("192.178.39.0-255");
            ImportIpRange("192.178.4.0-255");
            ImportIpRange("192.178.40.0-255");
            ImportIpRange("192.178.41.0-255");
            ImportIpRange("192.178.42.0-255");
            ImportIpRange("192.178.43.0-255");
            ImportIpRange("192.178.44.0-255");
            ImportIpRange("192.178.45.0-255");
            ImportIpRange("192.178.46.0-255");
            ImportIpRange("192.178.47.0-255");
            ImportIpRange("192.178.48.0-255");
            ImportIpRange("192.178.49.0-255");
            ImportIpRange("192.178.5.0-255");
            ImportIpRange("192.178.50.0-255");
            ImportIpRange("192.178.51.0-255");
            ImportIpRange("192.178.52.0-255");
            ImportIpRange("192.178.53.0-255");
            ImportIpRange("192.178.54.0-255");
            ImportIpRange("192.178.55.0-255");
            ImportIpRange("192.178.56.0-255");
            ImportIpRange("192.178.57.0-255");
            ImportIpRange("192.178.58.0-255");
            ImportIpRange("192.178.59.0-255");
            ImportIpRange("192.178.6.0-255");
            ImportIpRange("192.178.60.0-255");
            ImportIpRange("192.178.61.0-255");
            ImportIpRange("192.178.62.0-255");
            ImportIpRange("192.178.63.0-255");
            ImportIpRange("192.178.64.0-255");
            ImportIpRange("192.178.65.0-255");
            ImportIpRange("192.178.66.0-255");
            ImportIpRange("192.178.67.0-255");
            ImportIpRange("192.178.68.0-255");
            ImportIpRange("192.178.69.0-255");
            ImportIpRange("192.178.7.0-255");
            ImportIpRange("192.178.70.0-255");
            ImportIpRange("192.178.71.0-255");
            ImportIpRange("192.178.72.0-255");
            ImportIpRange("192.178.73.0-255");
            ImportIpRange("192.178.74.0-255");
            ImportIpRange("192.178.75.0-255");
            ImportIpRange("192.178.76.0-255");
            ImportIpRange("192.178.77.0-255");
            ImportIpRange("192.178.78.0-255");
            ImportIpRange("192.178.79.0-255");
            ImportIpRange("192.178.8.0-255");
            ImportIpRange("192.178.80.0-255");
            ImportIpRange("192.178.81.0-255");
            ImportIpRange("192.178.82.0-255");
            ImportIpRange("192.178.83.0-255");
            ImportIpRange("192.178.84.0-255");
            ImportIpRange("192.178.85.0-255");
            ImportIpRange("192.178.86.0-255");
            ImportIpRange("192.178.87.0-255");
            ImportIpRange("192.178.88.0-255");
            ImportIpRange("192.178.89.0-255");
            ImportIpRange("192.178.9.0-255");
            ImportIpRange("192.178.90.0-255");
            ImportIpRange("192.178.91.0-255");
            ImportIpRange("192.178.92.0-255");
            ImportIpRange("192.178.93.0-255");
            ImportIpRange("192.178.94.0-255");
            ImportIpRange("192.178.95.0-255");
            ImportIpRange("192.178.96.0-255");
            ImportIpRange("192.178.97.0-255");
            ImportIpRange("192.178.98.0-255");
            ImportIpRange("192.178.99.0-255");
            ImportIpRange("192.193.133.0-255");
            ImportIpRange("192.200.224.0-255");
            ImportIpRange("192.30.252.0-255");
            ImportIpRange("192.86.102.0-255");
            ImportIpRange("193.120.166.64-127");
            ImportIpRange("193.142.125.0-255");
            ImportIpRange("193.186.4.0-255");
            ImportIpRange("193.200.222.0-255");
            ImportIpRange("193.90.147.0-255");
            ImportIpRange("193.92.133.0-63");
            ImportIpRange("194.100.132.128-143");
            ImportIpRange("194.110.194.0-255");
            ImportIpRange("194.78.20.16-31");
            ImportIpRange("194.78.99.0-255");
            ImportIpRange("195.100.224.112-127");
            ImportIpRange("195.141.3.24-27");
            ImportIpRange("195.205.170.64-79");
            ImportIpRange("195.229.194.88-95");
            ImportIpRange("195.244.106.0-255");
            ImportIpRange("195.244.120.144-159");
            ImportIpRange("195.249.20.192-255");
            ImportIpRange("195.65.133.128-135");
            ImportIpRange("195.76.16.136-143");
            ImportIpRange("195.81.83.176-207");
            ImportIpRange("197.199.253.0-255");
            ImportIpRange("197.199.254.0-255");
            ImportIpRange("199.192.112.0-255");
            ImportIpRange("199.223.232.0-255");
            ImportIpRange("202.106.93.0-255");
            ImportIpRange("202.39.143.1-123");
            ImportIpRange("202.69.26.0-255");
            ImportIpRange("203.116.165.129-255");
            ImportIpRange("203.117.34.0-255");
            ImportIpRange("203.165.13.210-251");
            ImportIpRange("203.165.14.210-251");
            ImportIpRange("203.211.0.4-59");
            ImportIpRange("203.66.124.129-251");
            ImportIpRange("207.223.160.0-255");
            ImportIpRange("208.117.224.0-255");
            ImportIpRange("208.117.225.0-255");
            ImportIpRange("208.117.226.0-255");
            ImportIpRange("208.117.227.0-255");
            ImportIpRange("208.117.228.0-255");
            ImportIpRange("208.117.229.0-255");
            ImportIpRange("208.117.230.0-255");
            ImportIpRange("208.117.231.0-255");
            ImportIpRange("208.117.232.0-255");
            ImportIpRange("208.117.233.0-255");
            ImportIpRange("208.117.234.0-255");
            ImportIpRange("208.117.235.0-255");
            ImportIpRange("208.117.236.0-255");
            ImportIpRange("208.117.237.0-255");
            ImportIpRange("208.117.238.0-255");
            ImportIpRange("208.117.239.0-255");
            ImportIpRange("208.117.240.0-255");
            ImportIpRange("208.117.241.0-255");
            ImportIpRange("208.117.242.0-255");
            ImportIpRange("208.117.243.0-255");
            ImportIpRange("208.117.244.0-255");
            ImportIpRange("208.117.245.0-255");
            ImportIpRange("208.117.246.0-255");
            ImportIpRange("208.117.247.0-255");
            ImportIpRange("208.117.248.0-255");
            ImportIpRange("208.117.249.0-255");
            ImportIpRange("208.117.250.0-255");
            ImportIpRange("208.117.251.0-255");
            ImportIpRange("208.117.252.0-255");
            ImportIpRange("208.117.253.0-255");
            ImportIpRange("208.117.254.0-255");
            ImportIpRange("208.117.255.0-255");
            ImportIpRange("208.65.152.0-255");
            ImportIpRange("208.65.153.0-255");
            ImportIpRange("208.65.154.0-255");
            ImportIpRange("208.65.155.0-255");
            ImportIpRange("209.85.128.0-255");
            ImportIpRange("209.85.228.0-255");
            ImportIpRange("209.85.238.0-255");
            ImportIpRange("210.139.253.0-255");
            ImportIpRange("210.153.73.0-127");
            ImportIpRange("210.242.125.20-59");
            ImportIpRange("210.61.221.0-255");
            ImportIpRange("210.61.221.65-187");
            ImportIpRange("212.154.168.224-255");
            ImportIpRange("212.162.51.64-127");
            ImportIpRange("212.181.117.144-159");
            ImportIpRange("212.188.15.0-255");
            ImportIpRange("213.186.229.0-63");
            ImportIpRange("213.187.184.68-71");
            ImportIpRange("213.240.44.0-31");
            ImportIpRange("213.252.15.0-31");
            ImportIpRange("213.31.219.80-87");
            ImportIpRange("216.21.160.0-255");
            ImportIpRange("216.239.32.0-255");
            ImportIpRange("216.239.33.0-255");
            ImportIpRange("216.239.34.0-255");
            ImportIpRange("216.239.35.0-255");
            ImportIpRange("216.239.36.0-255");
            ImportIpRange("216.239.38.0-255");
            ImportIpRange("216.239.39.0-255");
            ImportIpRange("216.239.44.0-255");
            ImportIpRange("216.239.60.0-255");
            ImportIpRange("216.58.192.0-255");
            ImportIpRange("217.149.45.16-31");
            ImportIpRange("217.163.7.0-255");
            ImportIpRange("217.193.96.38");
            ImportIpRange("217.28.250.44-47");
            ImportIpRange("217.28.253.32-33");
            ImportIpRange("217.30.152.192-223");
            ImportIpRange("217.33.127.208-223");
            ImportIpRange("218.176.242.0-255");
            ImportIpRange("218.189.25.129-.187");
            ImportIpRange("218.253.0.76-187");
            ImportIpRange("23.228.128.0-255");
            ImportIpRange("23.236.48.0-255");
            ImportIpRange("23.251.128.0-255");
            ImportIpRange("23.255.128.0-255");
            ImportIpRange("24.156.131.0-255");
            ImportIpRange("41.206.96.0-255");
            ImportIpRange("41.84.159.0-255");
            ImportIpRange("58.205.224.0-255");
            ImportIpRange("58.240.77.0-255");
            ImportIpRange("59.78.209.0-255");
            ImportIpRange("60.199.175.0-255");
            ImportIpRange("61.219.131.0-255");
            ImportIpRange("62.116.207.0-63");
            ImportIpRange("62.197.198.193-251");
            ImportIpRange("62.20.124.48-63");
            ImportIpRange("62.201.216.0-255");
            ImportIpRange("63.243.168.0-255");
            ImportIpRange("64.15.112.0-255");
            ImportIpRange("64.15.113.0-255");
            ImportIpRange("64.15.114.0-255");
            ImportIpRange("64.15.115.0-255");
            ImportIpRange("64.15.116.0-255");
            ImportIpRange("64.15.117.0-255");
            ImportIpRange("64.15.118.0-255");
            ImportIpRange("64.15.119.0-255");
            ImportIpRange("64.15.120.0-255");
            ImportIpRange("64.15.121.0-255");
            ImportIpRange("64.15.122.0-255");
            ImportIpRange("64.15.123.0-255");
            ImportIpRange("64.15.124.0-255");
            ImportIpRange("64.15.125.0-255");
            ImportIpRange("64.15.126.0-255");
            ImportIpRange("64.15.127.0-255");
            ImportIpRange("64.233.160.0-255");
            ImportIpRange("64.233.164.0-255");
            ImportIpRange("64.233.165.0-255");
            ImportIpRange("64.233.168.0-255");
            ImportIpRange("64.233.171.0-255");
            ImportIpRange("64.233.172.0-255");
            ImportIpRange("64.9.224.0-255");
            ImportIpRange("65.55.58.0-255");
            ImportIpRange("66.102.0.0-255");
            ImportIpRange("66.102.1.0-255");
            ImportIpRange("66.102.10.0-255");
            ImportIpRange("66.102.100.0-255");
            ImportIpRange("66.102.101.0-255");
            ImportIpRange("66.102.102.0-255");
            ImportIpRange("66.102.103.0-255");
            ImportIpRange("66.102.104.0-255");
            ImportIpRange("66.102.105.0-255");
            ImportIpRange("66.102.106.0-255");
            ImportIpRange("66.102.107.0-255");
            ImportIpRange("66.102.108.0-255");
            ImportIpRange("66.102.109.0-255");
            ImportIpRange("66.102.11.0-255");
            ImportIpRange("66.102.110.0-255");
            ImportIpRange("66.102.111.0-255");
            ImportIpRange("66.102.112.0-255");
            ImportIpRange("66.102.113.0-255");
            ImportIpRange("66.102.114.0-255");
            ImportIpRange("66.102.115.0-255");
            ImportIpRange("66.102.116.0-255");
            ImportIpRange("66.102.117.0-255");
            ImportIpRange("66.102.118.0-255");
            ImportIpRange("66.102.119.0-255");
            ImportIpRange("66.102.12.0-255");
            ImportIpRange("66.102.120.0-255");
            ImportIpRange("66.102.121.0-255");
            ImportIpRange("66.102.122.0-255");
            ImportIpRange("66.102.123.0-255");
            ImportIpRange("66.102.124.0-255");
            ImportIpRange("66.102.125.0-255");
            ImportIpRange("66.102.126.0-255");
            ImportIpRange("66.102.127.0-255");
            ImportIpRange("66.102.128.0-255");
            ImportIpRange("66.102.129.0-255");
            ImportIpRange("66.102.13.0-255");
            ImportIpRange("66.102.130.0-255");
            ImportIpRange("66.102.131.0-255");
            ImportIpRange("66.102.132.0-255");
            ImportIpRange("66.102.133.0-255");
            ImportIpRange("66.102.134.0-255");
            ImportIpRange("66.102.135.0-255");
            ImportIpRange("66.102.136.0-255");
            ImportIpRange("66.102.137.0-255");
            ImportIpRange("66.102.138.0-255");
            ImportIpRange("66.102.139.0-255");
            ImportIpRange("66.102.14.0-255");
            ImportIpRange("66.102.140.0-255");
            ImportIpRange("66.102.141.0-255");
            ImportIpRange("66.102.142.0-255");
            ImportIpRange("66.102.143.0-255");
            ImportIpRange("66.102.144.0-255");
            ImportIpRange("66.102.145.0-255");
            ImportIpRange("66.102.146.0-255");
            ImportIpRange("66.102.147.0-255");
            ImportIpRange("66.102.148.0-255");
            ImportIpRange("66.102.149.0-255");
            ImportIpRange("66.102.15.0-255");
            ImportIpRange("66.102.150.0-255");
            ImportIpRange("66.102.151.0-255");
            ImportIpRange("66.102.152.0-255");
            ImportIpRange("66.102.153.0-255");
            ImportIpRange("66.102.154.0-255");
            ImportIpRange("66.102.155.0-255");
            ImportIpRange("66.102.156.0-255");
            ImportIpRange("66.102.157.0-255");
            ImportIpRange("66.102.158.0-255");
            ImportIpRange("66.102.159.0-255");
            ImportIpRange("66.102.16.0-255");
            ImportIpRange("66.102.160.0-255");
            ImportIpRange("66.102.161.0-255");
            ImportIpRange("66.102.162.0-255");
            ImportIpRange("66.102.163.0-255");
            ImportIpRange("66.102.164.0-255");
            ImportIpRange("66.102.165.0-255");
            ImportIpRange("66.102.166.0-255");
            ImportIpRange("66.102.167.0-255");
            ImportIpRange("66.102.168.0-255");
            ImportIpRange("66.102.169.0-255");
            ImportIpRange("66.102.17.0-255");
            ImportIpRange("66.102.170.0-255");
            ImportIpRange("66.102.171.0-255");
            ImportIpRange("66.102.172.0-255");
            ImportIpRange("66.102.173.0-255");
            ImportIpRange("66.102.174.0-255");
            ImportIpRange("66.102.175.0-255");
            ImportIpRange("66.102.176.0-255");
            ImportIpRange("66.102.177.0-255");
            ImportIpRange("66.102.178.0-255");
            ImportIpRange("66.102.179.0-255");
            ImportIpRange("66.102.18.0-255");
            ImportIpRange("66.102.180.0-255");
            ImportIpRange("66.102.181.0-255");
            ImportIpRange("66.102.182.0-255");
            ImportIpRange("66.102.183.0-255");
            ImportIpRange("66.102.184.0-255");
            ImportIpRange("66.102.185.0-255");
            ImportIpRange("66.102.186.0-255");
            ImportIpRange("66.102.187.0-255");
            ImportIpRange("66.102.188.0-255");
            ImportIpRange("66.102.189.0-255");
            ImportIpRange("66.102.19.0-255");
            ImportIpRange("66.102.190.0-255");
            ImportIpRange("66.102.191.0-255");
            ImportIpRange("66.102.192.0-255");
            ImportIpRange("66.102.193.0-255");
            ImportIpRange("66.102.194.0-255");
            ImportIpRange("66.102.195.0-255");
            ImportIpRange("66.102.196.0-255");
            ImportIpRange("66.102.197.0-255");
            ImportIpRange("66.102.198.0-255");
            ImportIpRange("66.102.199.0-255");
            ImportIpRange("66.102.2.0-255");
            ImportIpRange("66.102.20.0-255");
            ImportIpRange("66.102.200.0-255");
            ImportIpRange("66.102.201.0-255");
            ImportIpRange("66.102.202.0-255");
            ImportIpRange("66.102.203.0-255");
            ImportIpRange("66.102.204.0-255");
            ImportIpRange("66.102.205.0-255");
            ImportIpRange("66.102.206.0-255");
            ImportIpRange("66.102.207.0-255");
            ImportIpRange("66.102.208.0-255");
            ImportIpRange("66.102.209.0-255");
            ImportIpRange("66.102.21.0-255");
            ImportIpRange("66.102.210.0-255");
            ImportIpRange("66.102.211.0-255");
            ImportIpRange("66.102.212.0-255");
            ImportIpRange("66.102.213.0-255");
            ImportIpRange("66.102.214.0-255");
            ImportIpRange("66.102.215.0-255");
            ImportIpRange("66.102.216.0-255");
            ImportIpRange("66.102.217.0-255");
            ImportIpRange("66.102.218.0-255");
            ImportIpRange("66.102.219.0-255");
            ImportIpRange("66.102.22.0-255");
            ImportIpRange("66.102.220.0-255");
            ImportIpRange("66.102.221.0-255");
            ImportIpRange("66.102.222.0-255");
            ImportIpRange("66.102.223.0-255");
            ImportIpRange("66.102.224.0-255");
            ImportIpRange("66.102.225.0-255");
            ImportIpRange("66.102.226.0-255");
            ImportIpRange("66.102.227.0-255");
            ImportIpRange("66.102.228.0-255");
            ImportIpRange("66.102.229.0-255");
            ImportIpRange("66.102.23.0-255");
            ImportIpRange("66.102.230.0-255");
            ImportIpRange("66.102.231.0-255");
            ImportIpRange("66.102.232.0-255");
            ImportIpRange("66.102.233.0-255");
            ImportIpRange("66.102.234.0-255");
            ImportIpRange("66.102.235.0-255");
            ImportIpRange("66.102.236.0-255");
            ImportIpRange("66.102.237.0-255");
            ImportIpRange("66.102.238.0-255");
            ImportIpRange("66.102.239.0-255");
            ImportIpRange("66.102.24.0-255");
            ImportIpRange("66.102.240.0-255");
            ImportIpRange("66.102.241.0-255");
            ImportIpRange("66.102.242.0-255");
            ImportIpRange("66.102.243.0-255");
            ImportIpRange("66.102.244.0-255");
            ImportIpRange("66.102.245.0-255");
            ImportIpRange("66.102.246.0-255");
            ImportIpRange("66.102.247.0-255");
            ImportIpRange("66.102.248.0-255");
            ImportIpRange("66.102.249.0-255");
            ImportIpRange("66.102.25.0-255");
            ImportIpRange("66.102.250.0-255");
            ImportIpRange("66.102.251.0-255");
            ImportIpRange("66.102.252.0-255");
            ImportIpRange("66.102.253.0-255");
            ImportIpRange("66.102.254.0-255");
            ImportIpRange("66.102.255.0-255");
            ImportIpRange("66.102.26.0-255");
            ImportIpRange("66.102.27.0-255");
            ImportIpRange("66.102.28.0-255");
            ImportIpRange("66.102.29.0-255");
            ImportIpRange("66.102.3.0-255");
            ImportIpRange("66.102.30.0-255");
            ImportIpRange("66.102.31.0-255");
            ImportIpRange("66.102.32.0-255");
            ImportIpRange("66.102.33.0-255");
            ImportIpRange("66.102.34.0-255");
            ImportIpRange("66.102.35.0-255");
            ImportIpRange("66.102.36.0-255");
            ImportIpRange("66.102.37.0-255");
            ImportIpRange("66.102.38.0-255");
            ImportIpRange("66.102.39.0-255");
            ImportIpRange("66.102.4.0-255");
            ImportIpRange("66.102.40.0-255");
            ImportIpRange("66.102.41.0-255");
            ImportIpRange("66.102.42.0-255");
            ImportIpRange("66.102.43.0-255");
            ImportIpRange("66.102.44.0-255");
            ImportIpRange("66.102.45.0-255");
            ImportIpRange("66.102.46.0-255");
            ImportIpRange("66.102.47.0-255");
            ImportIpRange("66.102.48.0-255");
            ImportIpRange("66.102.49.0-255");
            ImportIpRange("66.102.5.0-255");
            ImportIpRange("66.102.50.0-255");
            ImportIpRange("66.102.51.0-255");
            ImportIpRange("66.102.52.0-255");
            ImportIpRange("66.102.53.0-255");
            ImportIpRange("66.102.54.0-255");
            ImportIpRange("66.102.55.0-255");
            ImportIpRange("66.102.56.0-255");
            ImportIpRange("66.102.57.0-255");
            ImportIpRange("66.102.58.0-255");
            ImportIpRange("66.102.59.0-255");
            ImportIpRange("66.102.6.0-255");
            ImportIpRange("66.102.60.0-255");
            ImportIpRange("66.102.61.0-255");
            ImportIpRange("66.102.62.0-255");
            ImportIpRange("66.102.63.0-255");
            ImportIpRange("66.102.64.0-255");
            ImportIpRange("66.102.65.0-255");
            ImportIpRange("66.102.66.0-255");
            ImportIpRange("66.102.67.0-255");
            ImportIpRange("66.102.68.0-255");
            ImportIpRange("66.102.69.0-255");
            ImportIpRange("66.102.7.0-255");
            ImportIpRange("66.102.70.0-255");
            ImportIpRange("66.102.71.0-255");
            ImportIpRange("66.102.72.0-255");
            ImportIpRange("66.102.73.0-255");
            ImportIpRange("66.102.74.0-255");
            ImportIpRange("66.102.75.0-255");
            ImportIpRange("66.102.76.0-255");
            ImportIpRange("66.102.77.0-255");
            ImportIpRange("66.102.78.0-255");
            ImportIpRange("66.102.79.0-255");
            ImportIpRange("66.102.8.0-255");
            ImportIpRange("66.102.80.0-255");
            ImportIpRange("66.102.81.0-255");
            ImportIpRange("66.102.82.0-255");
            ImportIpRange("66.102.83.0-255");
            ImportIpRange("66.102.84.0-255");
            ImportIpRange("66.102.85.0-255");
            ImportIpRange("66.102.86.0-255");
            ImportIpRange("66.102.87.0-255");
            ImportIpRange("66.102.88.0-255");
            ImportIpRange("66.102.89.0-255");
            ImportIpRange("66.102.9.0-255");
            ImportIpRange("66.102.90.0-255");
            ImportIpRange("66.102.91.0-255");
            ImportIpRange("66.102.92.0-255");
            ImportIpRange("66.102.93.0-255");
            ImportIpRange("66.102.94.0-255");
            ImportIpRange("66.102.95.0-255");
            ImportIpRange("66.102.96.0-255");
            ImportIpRange("66.102.97.0-255");
            ImportIpRange("66.102.98.0-255");
            ImportIpRange("66.102.99.0-255");
            ImportIpRange("66.185.84.0-255");
            ImportIpRange("66.249.64.0-255");
            ImportIpRange("66.249.65.0-255");
            ImportIpRange("66.249.66.0-255");
            ImportIpRange("66.249.67.0-255");
            ImportIpRange("66.249.68.0-255");
            ImportIpRange("66.249.69.0-255");
            ImportIpRange("66.249.70.0-255");
            ImportIpRange("66.249.71.0-255");
            ImportIpRange("66.249.72.0-255");
            ImportIpRange("66.249.73.0-255");
            ImportIpRange("66.249.74.0-255");
            ImportIpRange("66.249.75.0-255");
            ImportIpRange("66.249.76.0-255");
            ImportIpRange("66.249.77.0-255");
            ImportIpRange("66.249.78.0-255");
            ImportIpRange("66.249.79.0-255");
            ImportIpRange("66.249.80.0-255");
            ImportIpRange("66.249.81.0-255");
            ImportIpRange("66.249.82.0-255");
            ImportIpRange("66.249.83.0-255");
            ImportIpRange("66.249.84.0-255");
            ImportIpRange("66.249.85.0-255");
            ImportIpRange("66.249.88.0-255");
            ImportIpRange("66.249.89.0-255");
            ImportIpRange("66.249.90.0-255");
            ImportIpRange("66.249.91.0-255");
            ImportIpRange("66.249.92.0-255");
            ImportIpRange("66.249.93.0-255");
            ImportIpRange("69.17.141.0-255");
            ImportIpRange("70.32.128.0-255");
            ImportIpRange("70.32.130.0-255");
            ImportIpRange("70.32.131.0-255");
            ImportIpRange("70.32.132.0-255");
            ImportIpRange("70.32.133.0-255");
            ImportIpRange("70.32.134.0-255");
            ImportIpRange("70.32.140.0-255");
            ImportIpRange("70.32.142.0-255");
            ImportIpRange("70.32.144.0-255");
            ImportIpRange("70.32.146.0-255");
            ImportIpRange("70.32.148.0-255");
            ImportIpRange("70.32.158.0-255");
            ImportIpRange("72.14.192.0-255");
            ImportIpRange("72.14.199.0-255");
            ImportIpRange("72.14.202.0-255");
            ImportIpRange("72.14.208.0-255");
            ImportIpRange("72.14.225.0-255");
            ImportIpRange("72.14.226.0-255");
            ImportIpRange("72.14.228.0-255");
            ImportIpRange("72.14.244.0-255");
            ImportIpRange("72.14.252.0-255");
            ImportIpRange("74.125.0.0-255");
            ImportIpRange("74.125.1.0-255");
            ImportIpRange("74.125.10.0-255");
            ImportIpRange("74.125.100.0-255");
            ImportIpRange("74.125.101.0-255");
            ImportIpRange("74.125.102.0-255");
            ImportIpRange("74.125.103.0-255");
            ImportIpRange("74.125.104.0-255");
            ImportIpRange("74.125.105.0-255");
            ImportIpRange("74.125.106.0-255");
            ImportIpRange("74.125.107.0-255");
            ImportIpRange("74.125.108.0-255");
            ImportIpRange("74.125.109.0-255");
            ImportIpRange("74.125.11.0-255");
            ImportIpRange("74.125.110.0-255");
            ImportIpRange("74.125.111.0-255");
            ImportIpRange("74.125.112.0-255");
            ImportIpRange("74.125.113.0-255");
            ImportIpRange("74.125.114.0-255");
            ImportIpRange("74.125.115.0-255");
            ImportIpRange("74.125.116.0-255");
            ImportIpRange("74.125.117.0-255");
            ImportIpRange("74.125.118.0-255");
            ImportIpRange("74.125.119.0-255");
            ImportIpRange("74.125.12.0-255");
            ImportIpRange("74.125.120.0-255");
            ImportIpRange("74.125.121.0-255");
            ImportIpRange("74.125.122.0-255");
            ImportIpRange("74.125.123.0-255");
            ImportIpRange("74.125.124.0-255");
            ImportIpRange("74.125.125.0-255");
            ImportIpRange("74.125.126.0-255");
            ImportIpRange("74.125.127.0-255");
            ImportIpRange("74.125.128.0-255");
            ImportIpRange("74.125.129.0-255");
            ImportIpRange("74.125.13.0-255");
            ImportIpRange("74.125.130.0-255");
            ImportIpRange("74.125.131.0-255");
            ImportIpRange("74.125.132.0-255");
            ImportIpRange("74.125.133.0-255");
            ImportIpRange("74.125.134.0-255");
            ImportIpRange("74.125.135.0-255");
            ImportIpRange("74.125.136.0-255");
            ImportIpRange("74.125.137.0-255");
            ImportIpRange("74.125.138.0-255");
            ImportIpRange("74.125.139.0-255");
            ImportIpRange("74.125.14.0-255");
            ImportIpRange("74.125.140.0-255");
            ImportIpRange("74.125.141.0-255");
            ImportIpRange("74.125.142.0-255");
            ImportIpRange("74.125.143.0-255");
            ImportIpRange("74.125.144.0-255");
            ImportIpRange("74.125.145.0-255");
            ImportIpRange("74.125.146.0-255");
            ImportIpRange("74.125.147.0-255");
            ImportIpRange("74.125.148.0-255");
            ImportIpRange("74.125.149.0-255");
            ImportIpRange("74.125.15.0-255");
            ImportIpRange("74.125.150.0-255");
            ImportIpRange("74.125.151.0-255");
            ImportIpRange("74.125.152.0-255");
            ImportIpRange("74.125.153.0-255");
            ImportIpRange("74.125.154.0-255");
            ImportIpRange("74.125.155.0-255");
            ImportIpRange("74.125.156.0-255");
            ImportIpRange("74.125.157.0-255");
            ImportIpRange("74.125.158.0-255");
            ImportIpRange("74.125.159.0-255");
            ImportIpRange("74.125.16.0-255");
            ImportIpRange("74.125.160.0-255");
            ImportIpRange("74.125.161.0-255");
            ImportIpRange("74.125.162.0-255");
            ImportIpRange("74.125.163.0-255");
            ImportIpRange("74.125.164.0-255");
            ImportIpRange("74.125.165.0-255");
            ImportIpRange("74.125.166.0-255");
            ImportIpRange("74.125.167.0-255");
            ImportIpRange("74.125.168.0-255");
            ImportIpRange("74.125.169.0-255");
            ImportIpRange("74.125.17.0-255");
            ImportIpRange("74.125.170.0-255");
            ImportIpRange("74.125.171.0-255");
            ImportIpRange("74.125.172.0-255");
            ImportIpRange("74.125.173.0-255");
            ImportIpRange("74.125.174.0-255");
            ImportIpRange("74.125.175.0-255");
            ImportIpRange("74.125.176.0-255");
            ImportIpRange("74.125.177.0-255");
            ImportIpRange("74.125.178.0-255");
            ImportIpRange("74.125.179.0-255");
            ImportIpRange("74.125.18.0-255");
            ImportIpRange("74.125.180.0-255");
            ImportIpRange("74.125.181.0-255");
            ImportIpRange("74.125.182.0-255");
            ImportIpRange("74.125.183.0-255");
            ImportIpRange("74.125.184.0-255");
            ImportIpRange("74.125.185.0-255");
            ImportIpRange("74.125.186.0-255");
            ImportIpRange("74.125.187.0-255");
            ImportIpRange("74.125.188.0-255");
            ImportIpRange("74.125.189.0-255");
            ImportIpRange("74.125.19.0-255");
            ImportIpRange("74.125.190.0-255");
            ImportIpRange("74.125.191.0-255");
            ImportIpRange("74.125.192.0-255");
            ImportIpRange("74.125.193.0-255");
            ImportIpRange("74.125.194.0-255");
            ImportIpRange("74.125.195.0-255");
            ImportIpRange("74.125.196.0-255");
            ImportIpRange("74.125.197.0-255");
            ImportIpRange("74.125.198.0-255");
            ImportIpRange("74.125.199.0-255");
            ImportIpRange("74.125.2.0-255");
            ImportIpRange("74.125.20.0-255");
            ImportIpRange("74.125.200.0-255");
            ImportIpRange("74.125.201.0-255");
            ImportIpRange("74.125.202.0-255");
            ImportIpRange("74.125.203.0-255");
            ImportIpRange("74.125.204.0-255");
            ImportIpRange("74.125.205.0-255");
            ImportIpRange("74.125.206.0-255");
            ImportIpRange("74.125.207.0-255");
            ImportIpRange("74.125.208.0-255");
            ImportIpRange("74.125.209.0-255");
            ImportIpRange("74.125.21.0-255");
            ImportIpRange("74.125.210.0-255");
            ImportIpRange("74.125.211.0-255");
            ImportIpRange("74.125.212.0-255");
            ImportIpRange("74.125.213.0-255");
            ImportIpRange("74.125.214.0-255");
            ImportIpRange("74.125.215.0-255");
            ImportIpRange("74.125.216.0-255");
            ImportIpRange("74.125.217.0-255");
            ImportIpRange("74.125.218.0-255");
            ImportIpRange("74.125.219.0-255");
            ImportIpRange("74.125.22.0-255");
            ImportIpRange("74.125.220.0-255");
            ImportIpRange("74.125.221.0-255");
            ImportIpRange("74.125.222.0-255");
            ImportIpRange("74.125.223.0-255");
            ImportIpRange("74.125.224.0-255");
            ImportIpRange("74.125.225.0-255");
            ImportIpRange("74.125.226.0-255");
            ImportIpRange("74.125.227.0-255");
            ImportIpRange("74.125.228.0-255");
            ImportIpRange("74.125.229.0-255");
            ImportIpRange("74.125.23.0-255");
            ImportIpRange("74.125.230.0-255");
            ImportIpRange("74.125.231.0-255");
            ImportIpRange("74.125.232.0-255");
            ImportIpRange("74.125.233.0-255");
            ImportIpRange("74.125.234.0-255");
            ImportIpRange("74.125.235.0-255");
            ImportIpRange("74.125.236.0-255");
            ImportIpRange("74.125.237.0-255");
            ImportIpRange("74.125.238.0-255");
            ImportIpRange("74.125.239.0-255");
            ImportIpRange("74.125.24.0-255");
            ImportIpRange("74.125.240.0-255");
            ImportIpRange("74.125.241.0-255");
            ImportIpRange("74.125.242.0-255");
            ImportIpRange("74.125.243.0-255");
            ImportIpRange("74.125.244.0-255");
            ImportIpRange("74.125.245.0-255");
            ImportIpRange("74.125.246.0-255");
            ImportIpRange("74.125.247.0-255");
            ImportIpRange("74.125.248.0-255");
            ImportIpRange("74.125.249.0-255");
            ImportIpRange("74.125.25.0-255");
            ImportIpRange("74.125.250.0-255");
            ImportIpRange("74.125.251.0-255");
            ImportIpRange("74.125.252.0-255");
            ImportIpRange("74.125.253.0-255");
            ImportIpRange("74.125.254.0-255");
            ImportIpRange("74.125.255.0-255");
            ImportIpRange("74.125.26.0-255");
            ImportIpRange("74.125.27.0-255");
            ImportIpRange("74.125.28.0-255");
            ImportIpRange("74.125.29.0-255");
            ImportIpRange("74.125.3.0-255");
            ImportIpRange("74.125.30.0-255");
            ImportIpRange("74.125.31.0-255");
            ImportIpRange("74.125.32.0-255");
            ImportIpRange("74.125.33.0-255");
            ImportIpRange("74.125.34.0-255");
            ImportIpRange("74.125.35.0-255");
            ImportIpRange("74.125.36.0-255");
            ImportIpRange("74.125.37.0-255");
            ImportIpRange("74.125.38.0-255");
            ImportIpRange("74.125.39.0-255");
            ImportIpRange("74.125.4.0-255");
            ImportIpRange("74.125.40.0-255");
            ImportIpRange("74.125.41.0-255");
            ImportIpRange("74.125.42.0-255");
            ImportIpRange("74.125.43.0-255");
            ImportIpRange("74.125.44.0-255");
            ImportIpRange("74.125.45.0-255");
            ImportIpRange("74.125.46.0-255");
            ImportIpRange("74.125.47.0-255");
            ImportIpRange("74.125.48.0-255");
            ImportIpRange("74.125.49.0-255");
            ImportIpRange("74.125.5.0-255");
            ImportIpRange("74.125.50.0-255");
            ImportIpRange("74.125.51.0-255");
            ImportIpRange("74.125.52.0-255");
            ImportIpRange("74.125.53.0-255");
            ImportIpRange("74.125.54.0-255");
            ImportIpRange("74.125.55.0-255");
            ImportIpRange("74.125.56.0-255");
            ImportIpRange("74.125.57.0-255");
            ImportIpRange("74.125.58.0-255");
            ImportIpRange("74.125.59.0-255");
            ImportIpRange("74.125.6.0-255");
            ImportIpRange("74.125.60.0-255");
            ImportIpRange("74.125.61.0-255");
            ImportIpRange("74.125.62.0-255");
            ImportIpRange("74.125.63.0-255");
            ImportIpRange("74.125.64.0-255");
            ImportIpRange("74.125.65.0-255");
            ImportIpRange("74.125.66.0-255");
            ImportIpRange("74.125.67.0-255");
            ImportIpRange("74.125.68.0-255");
            ImportIpRange("74.125.69.0-255");
            ImportIpRange("74.125.7.0-255");
            ImportIpRange("74.125.70.0-255");
            ImportIpRange("74.125.71.0-255");
            ImportIpRange("74.125.72.0-255");
            ImportIpRange("74.125.73.0-255");
            ImportIpRange("74.125.74.0-255");
            ImportIpRange("74.125.75.0-255");
            ImportIpRange("74.125.76.0-255");
            ImportIpRange("74.125.77.0-255");
            ImportIpRange("74.125.78.0-255");
            ImportIpRange("74.125.79.0-255");
            ImportIpRange("74.125.8.0-255");
            ImportIpRange("74.125.80.0-255");
            ImportIpRange("74.125.81.0-255");
            ImportIpRange("74.125.82.0-255");
            ImportIpRange("74.125.83.0-255");
            ImportIpRange("74.125.84.0-255");
            ImportIpRange("74.125.85.0-255");
            ImportIpRange("74.125.86.0-255");
            ImportIpRange("74.125.87.0-255");
            ImportIpRange("74.125.88.0-255");
            ImportIpRange("74.125.89.0-255");
            ImportIpRange("74.125.9.0-255");
            ImportIpRange("74.125.90.0-255");
            ImportIpRange("74.125.91.0-255");
            ImportIpRange("74.125.92.0-255");
            ImportIpRange("74.125.93.0-255");
            ImportIpRange("74.125.94.0-255");
            ImportIpRange("74.125.95.0-255");
            ImportIpRange("74.125.96.0-255");
            ImportIpRange("74.125.97.0-255");
            ImportIpRange("74.125.98.0-255");
            ImportIpRange("74.125.99.0-255");
            ImportIpRange("77.109.131.208-223");
            ImportIpRange("77.40.222.224-231");
            ImportIpRange("77.66.9.64-123");
            ImportIpRange("78.8.8.176-191");
            ImportIpRange("8.15.202.0-255");
            ImportIpRange("8.22.56.0-255");
            ImportIpRange("8.34.208.0-255");
            ImportIpRange("8.34.216.0-255");
            ImportIpRange("8.35.192.0-255");
            ImportIpRange("8.35.200.0-255");
            ImportIpRange("8.6.48.0-255");
            ImportIpRange("8.8.4.0-255");
            ImportIpRange("8.8.8.0-255");
            ImportIpRange("80.227.152.32-39");
            ImportIpRange("80.228.65.128-191");
            ImportIpRange("80.231.69.0-63");
            ImportIpRange("80.239.168.192-255");
            ImportIpRange("80.80.3.176-191");
            ImportIpRange("81.175.29.128-191");
            ImportIpRange("81.93.175.232-239");
            ImportIpRange("83.100.221.224-255");
            ImportIpRange("83.141.89.124-127");
            ImportIpRange("83.220.157.100-103");
            ImportIpRange("84.233.219.144-159");
            ImportIpRange("84.235.77.0-255");
            ImportIpRange("85.182.250.0-255");
            ImportIpRange("86.127.118.128-191");
            ImportIpRange("87.244.198.160-191");
            ImportIpRange("88.159.13.0-255");
            ImportIpRange("89.207.224.0-255");
            ImportIpRange("89.207.225.0-255");
            ImportIpRange("89.207.226.0-255");
            ImportIpRange("89.207.227.0-255");
            ImportIpRange("89.207.228.0-255");
            ImportIpRange("89.207.229.0-255");
            ImportIpRange("89.207.230.0-255");
            ImportIpRange("89.207.231.0-255");
            ImportIpRange("89.96.249.160-175");
            ImportIpRange("92.45.86.16-31");
            ImportIpRange("93.123.23.0-255");
            ImportIpRange("93.183.211.192-255");
            ImportIpRange("93.94.217.0-31");
            ImportIpRange("93.94.218.0-31");
            ImportIpRange("94.200.103.64-71");
            ImportIpRange("94.40.70.0-63");
        }

        private readonly Regex rxMatchIp =
            new Regex(@"((25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1\d\d|\d{1,2})",
                RegexOptions.Compiled);

        private readonly DataTable IpTable = new DataTable();

        public static Queue<string> WaitQueue = new Queue<string>();
        public static Queue<int> TestQueue = new Queue<int>();

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

            MessageBox.Show(this,
@"测试得到的结果请不要重复扫描，这会导致IP被重置，增加结果的不准确性。

一般iplist在使用的时候good_ipaddrs并不是固定不动的，而是一个动态的值，因为ISP（注意是ISP而不是墙）会临时性的重置有些IP。
所以good_ipaddrs存在一个最好的情况（比如good_ipaddrs=200）和最差的情况（比如good_ipaddrs=30），只要最差的情况依然能够正常上网（出现黄字是正常的，因为有些IP临时失效），那么这个iplist就是好用的。");

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

                    var result = StdTestProcess(addr, TestTimeout);
                    SetStdTestResult(result);

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
            long pingTime = 0;

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var stopwatch = new Stopwatch();

            try
            {
                var ar = socket.BeginConnect(addr, 443, x =>
                     {
                         try
                         {
                             socket.EndConnect(x);
                             pingTime = stopwatch.ElapsedMilliseconds;
                         }
                         catch (Exception) { }

                     }, null);

                stopwatch.Start();

                while (!ar.IsCompleted)
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

                stopwatch.Stop();

                if (!socket.Connected)
                {
                    socket.Close();
                    return new TestResult()
                    {
                        addr = addr,
                        ok = false,
                        msg = "Failed"
                    };
                }

                socket.Close();
            }
            catch (Exception)
            {
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
                var rows = SelectByIp(result.addr);
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
                var rows = SelectByIp(addr);
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
        private DataRow[] SelectByIp(string addr, string order = null)
        {

            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByIp(addr, order)));
            }

            return IpTable.Select(string.Format("addr = '{0}'", addr), order);
        }

        private DataRow[] SelectNa(string key, string order = null)
        {
            if (InvokeRequired)
            {
                return (DataRow[])Invoke(new MethodInvoker(() => SelectByIp(key, order)));
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

        private string BuildIpString(string[] strs)
        {
            var sbd = new StringBuilder(strs[0]);

            for (int i = 1; i < strs.Length; i++)
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

                    Thread.Sleep(500);

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

        private void mApplyToUserConfig_Click(object sender, EventArgs e)
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

            var vstd = GetStdValidIps();
            var vga = GetGaValidIps();
            List<string> vfinal;
            if (vstd.Count > 0 && vga.Count > 0)
            {
                vfinal = new List<string>();
                foreach (var str in vstd)
                {
                    if (vga.Contains(str))
                    {
                        vfinal.Add(str);
                    }
                }

            }
            else if (vstd.Count == 0)
            {
                vfinal = vga;
            }
            else
            {
                vfinal = vstd;
            }

            var ipstring = BuildIpString(vfinal.ToArray());

            if (!File.Exists("proxy.user.ini"))
            {
                File.WriteAllText("proxy.user.ini", "");
            }

            var inifile = new IniFile("proxy.user.ini");

            inifile.WriteValue("iplist", "google_cn", ipstring);
            inifile.WriteValue("iplist", "google_hk", ipstring);

            inifile.WriteFile();
            MessageBox.Show("已写入proxy.user.ini！重新载入GoAgent就可生效！");
        }
        private List<string> GetStdValidIps()
        {
            var ls = new List<string>();
            var rows = SelectByExpr(string.Format("std like '_OK%'"), "std asc");
            foreach (var row in rows)
            {
                ls.Add(row[0].ToString());
            }
            return ls;
        }
        private List<string> GetGaValidIps()
        {
            var ls = new List<string>();
            var rows = SelectByExpr(string.Format("proxy like '_OK%'"), "std asc, proxy asc");
            foreach (var row in rows)
            {
                ls.Add(row[0].ToString());
            }
            return ls;
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

        public int Count
        {
            get
            {
                var number =
               (cope[0, 1] - cope[0, 0] + 1) *
               (cope[1, 1] - cope[1, 0] + 1) *
               (cope[2, 1] - cope[2, 0] + 1) *
               (cope[3, 1] - cope[3, 0] + 1);

                return number;
            }
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

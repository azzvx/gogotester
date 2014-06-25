using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo_Tester
{
    class IpRange
    {
        private IpRange() { }

        public int[,] Cope = new int[4, 2];

        public string GetRandomIp()
        {
            var sbd = new StringBuilder();
            sbd.Append(rand.Next(Cope[0, 0], Cope[0, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(Cope[1, 0], Cope[1, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(Cope[2, 0], Cope[2, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(Cope[3, 0], Cope[3, 1] + 1));
            return sbd.ToString();
        }

        public int Count
        {
            get
            {
                var number =
               (Cope[0, 1] - Cope[0, 0] + 1) *
               (Cope[1, 1] - Cope[1, 0] + 1) *
               (Cope[2, 1] - Cope[2, 0] + 1) *
               (Cope[3, 1] - Cope[3, 0] + 1);

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

        private static int SetRange(string val)
        {
            try
            {
                return SetRange(Convert.ToInt32(val));
            }
            catch (Exception) { }
            return 0;
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
                    var sps = cope[i].Trim().Split(@"-\|/".ToCharArray());

                    for (int j = 0; j < sps.Length; j++)
                        sps[j] = sps[j].Trim();

                    if (sps.Length > 1)
                    {
                        if (sps[0].Length == 0)
                            sps[0] = "0";

                        if (sps[1].Length == 0)
                            sps[1] = "255";

                        iprange.Cope[i, 0] = SetRange(sps[0]);
                        iprange.Cope[i, 1] = SetRange(sps[1]);

                        if (iprange.Cope[i, 0] > iprange.Cope[i, 1])
                        {
                            swap = iprange.Cope[i, 1];
                            iprange.Cope[i, 1] = iprange.Cope[i, 0];
                            iprange.Cope[i, 0] = swap;
                        }
                    }
                    else
                    {
                        if (sps[0].Length == 0)
                        {
                            iprange.Cope[i, 0] = 0;
                            iprange.Cope[i, 1] = 255;
                        }
                        else
                        {
                            iprange.Cope[i, 0] = iprange.Cope[i, 1] = SetRange(sps[0]);
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

        public static List<IpRange> Pool = new List<IpRange>();

        private static void ImportIpRange(string range)
        {
            var iprange = IpRange.CreateIpRange(range);
            if (iprange != null)
            {
                Pool.Add(iprange);
            }
        }
        static IpRange()
        {
            ImportIpRange("1.179.248-255.0-255");
            ImportIpRange("103.246.187.0-255");
            ImportIpRange("103.25.178.4-59");
            ImportIpRange("106.162.192.148-187");
            ImportIpRange("106.162.198.84-123");
            ImportIpRange("106.162.216.20-123");
            ImportIpRange("107.167.160-191.0-255");
            ImportIpRange("107.178.192-255.0-255");
            ImportIpRange("107.188.128-255.0-255");
            ImportIpRange("108.170.192-255.0-255");
            ImportIpRange("108.177.0-127.0-255");
            ImportIpRange("108.59.80-95.0-255");
            ImportIpRange("109.232.83.64-127");
            ImportIpRange("111.168.255.20-187");
            ImportIpRange("111.92.162.4-59");
            ImportIpRange("113.197.105-106.0-255");
            ImportIpRange("118.174.24-27.0-255");
            ImportIpRange("12.216.80.0-255");
            ImportIpRange("121.78.74.68-123");
            ImportIpRange("123.205.250-251.68-190");
            ImportIpRange("130.211.0-255.0-255");
            ImportIpRange("130.211.0-255.0-255");
            ImportIpRange("142.250-251.0-255.0-255");
            ImportIpRange("146.148.0-127.0-255");
            ImportIpRange("149.126.86.1-59");
            ImportIpRange("149.3.177.0-255");
            ImportIpRange("162.216.148-151.0-255");
            ImportIpRange("162.222.176-183.0-255");
            ImportIpRange("163.28.116.1-59");
            ImportIpRange("163.28.83.143-187");
            ImportIpRange("172.217.0-255.0-255");
            ImportIpRange("172.253.0-255.0-255");
            ImportIpRange("173.194.0-255.0-255");
            ImportIpRange("173.255.112-127.0-255");
            ImportIpRange("178.45.251.4-123");
            ImportIpRange("178.60.128.1-63");
            ImportIpRange("185.25.28-29.0-255");
            ImportIpRange("192.119.16-31.0-255");
            ImportIpRange("192.158.28-31.0-255");
            ImportIpRange("192.178-179.0-255.0-255");
            ImportIpRange("192.200.224-255.0-255");
            ImportIpRange("193.120.166.64-127");
            ImportIpRange("193.134.255.0-255");
            ImportIpRange("193.142.125.0-255");
            ImportIpRange("193.186.4.0-255");
            ImportIpRange("193.192.226.128-191");
            ImportIpRange("193.192.250.128-191");
            ImportIpRange("193.200.222.0-255");
            ImportIpRange("193.247.193.0-255");
            ImportIpRange("193.90.147.0-123");
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
            ImportIpRange("196.3.58-59.0-255");
            ImportIpRange("197.199.253-254.1-59");
            ImportIpRange("197.84.128.0-63");
            ImportIpRange("199.192.112-115.0-255");
            ImportIpRange("199.223.232-239.0-255");
            ImportIpRange("202.39.143.1-123");
            ImportIpRange("203.116.165.129-255");
            ImportIpRange("203.117.34-37.132-187");
            ImportIpRange("203.165.13-14.210-251");
            ImportIpRange("203.211.0.4-59");
            ImportIpRange("203.66.124.129-251");
            ImportIpRange("207.223.160-175.0-255");
            ImportIpRange("208.117.224-255.0-255");
            ImportIpRange("208.65.152-155.0-255");
            ImportIpRange("209.85.128-255.0-255");
            ImportIpRange("210.139.253.20-251");
            ImportIpRange("210.153.73.20-123");
            ImportIpRange("210.242.125.20-59");
            ImportIpRange("210.61.221.65-187");
            ImportIpRange("212.154.168.224-255");
            ImportIpRange("212.162.51.64-127");
            ImportIpRange("212.181.117.144-159");
            ImportIpRange("212.188.10.0-255");
            ImportIpRange("212.188.15.0-255");
            ImportIpRange("212.188.7.0-255");
            ImportIpRange("213.186.229.0-63");
            ImportIpRange("213.187.184.68-71");
            ImportIpRange("213.240.44.0-31");
            ImportIpRange("213.252.15.0-31");
            ImportIpRange("213.31.219.80-87");
            ImportIpRange("216.21.160-175.0-255");
            ImportIpRange("216.239.32-63.0-255");
            ImportIpRange("216.58.192-223.0-255");
            ImportIpRange("217.149.45.16-31");
            ImportIpRange("217.163.7.0-255");
            ImportIpRange("217.193.96.38");
            ImportIpRange("217.28.250.44-47");
            ImportIpRange("217.28.253.32-33");
            ImportIpRange("217.30.152.192-223");
            ImportIpRange("217.33.127.208-223");
            ImportIpRange("218.176.242.4-251");
            ImportIpRange("218.189.25.129-.187");
            ImportIpRange("218.253.0.76-187");
            ImportIpRange("23.228.128-191.0-255");
            ImportIpRange("23.236.48-63.0-255");
            ImportIpRange("23.251.128-159.0-255");
            ImportIpRange("23.255.128-255.0-255");
            ImportIpRange("24.156.131.0-255");
            ImportIpRange("31.209.137.0-255");
            ImportIpRange("31.7.160.192-255");
            ImportIpRange("37.228.69.0-63");
            ImportIpRange("41.206.96.1-251");
            ImportIpRange("41.84.159.12-30");
            ImportIpRange("60.199.175.1-187");
            ImportIpRange("61.219.131.65-251");
            ImportIpRange("62.0.54.64-127");
            ImportIpRange("62.1.38.64-191");
            ImportIpRange("62.116.207.0-63");
            ImportIpRange("62.197.198.193-251");
            ImportIpRange("62.20.124.48-63");
            ImportIpRange("62.201.216.196-251");
            ImportIpRange("63.243.168.0-255");
            ImportIpRange("64.15.112-127.0-255");
            ImportIpRange("64.233.160-191.0-255");
            ImportIpRange("64.9.224-255.0-255");
            ImportIpRange("66.102.0-15.0-255");
            ImportIpRange("66.185.84.0-255");
            ImportIpRange("66.249.64-95.0-255");
            ImportIpRange("69.17.141.0-255");
            ImportIpRange("70.32.128-159.0-255");
            ImportIpRange("72.14.192-255.0-255");
            ImportIpRange("74.125.0-255.0-255");
            ImportIpRange("77.109.131.208-223");
            ImportIpRange("77.40.222.224-231");
            ImportIpRange("77.42.248-255.0-255");
            ImportIpRange("77.66.9.64-123");
            ImportIpRange("78.8.8.176-191");
            ImportIpRange("8.15.202.0-255");
            ImportIpRange("8.22.56.0-255");
            ImportIpRange("8.34.208-223.0-255");
            ImportIpRange("8.35.192-207.0-255");
            ImportIpRange("8.6.48-55.0-255");
            ImportIpRange("8.8.4.0-255");
            ImportIpRange("8.8.8.0-255");
            ImportIpRange("80.227.152.32-39");
            ImportIpRange("80.228.65.128-191");
            ImportIpRange("80.231.69.0-63");
            ImportIpRange("80.239.168.192-255");
            ImportIpRange("80.80.3.176-191");
            ImportIpRange("81.175.29.128-191");
            ImportIpRange("81.93.175.232-239");
            ImportIpRange("82.135.118.0-63");
            ImportIpRange("83.100.221.224-255");
            ImportIpRange("83.141.89.124-127");
            ImportIpRange("83.145.196.128-191");
            ImportIpRange("83.220.157.100-103");
            ImportIpRange("83.94.121.128-255");
            ImportIpRange("84.233.219.144-159");
            ImportIpRange("84.235.77.1-251");
            ImportIpRange("85.182.250.0-191");
            ImportIpRange("86.127.118.128-191");
            ImportIpRange("87.244.198.160-191");
            ImportIpRange("88.159.13.192-255");
            ImportIpRange("89.207.224-231.0-255");
            ImportIpRange("89.96.249.160-175");
            ImportIpRange("92.45.86.16-31");
            ImportIpRange("93.123.23.1-59");
            ImportIpRange("93.183.211.192-255");
            ImportIpRange("93.94.217-218.0-31");
            ImportIpRange("94.200.103.64-71");
            ImportIpRange("94.40.70.0-63");
            ImportIpRange("95.143.84.128-191");
            //ip range
            ImportIpRange("61.19.1-2.0-127");
            ImportIpRange("61.19.8.0-127");
            ImportIpRange("113.21.24.0-127");
            //thx for alienwaresky
            ImportIpRange("118.143.88.16-123");
            ImportIpRange("202.86.162.20-187");
            ImportIpRange("139.175.107.20-187");
            ImportIpRange("223.26.69.16-59");
            ImportIpRange("220.255.5-6.20-251");
            ImportIpRange("202.65.246.84-123");
            ImportIpRange("103.1.139.148-251");
            ImportIpRange("116.92.194.148-187");
            ImportIpRange("58.145.238.20-59");
            //
            ImportIpRange("41.201.128.20-59");
            ImportIpRange("41.201.164.20-59");
            //OpenerDNS
            //ImportIpRange("119.81.145.120-127");
            //ImportIpRange("119.81.142.202");
        }
    }
}

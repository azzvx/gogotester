using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace GoGo_Tester
{
    class IpRange
    {
        private IpRange() { }

        public int[,] Cope = new int[4, 2];

        public IPAddress GetRandomIp()
        {
            var sbd = new StringBuilder();
            sbd.Append(rand.Next(Cope[0, 0], Cope[0, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(Cope[1, 0], Cope[1, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(Cope[2, 0], Cope[2, 1] + 1));
            sbd.Append(".");
            sbd.Append(rand.Next(Cope[3, 0], Cope[3, 1] + 1));
            return IPAddress.Parse(sbd.ToString());
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
                    var sps = cope[i].Trim().Split(@"-".ToCharArray());

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

        public static List<IpRange> Pool4B = new List<IpRange>();
        public static List<IpRange> Pool4C = new List<IpRange>();

        private static void ImportIpv4Range(string str)
        {
            var range = CreateIpRange(str);

            if (range != null)
            {
                for (int a = range.Cope[0, 0]; a <= range.Cope[0, 1]; a++)
                {
                    for (int b = range.Cope[1, 0]; b <= range.Cope[1, 1]; b++)
                    {
                        Pool4B.Add(CreateIpRange(string.Format("{0}.{1}.{2}-{3}.{4}-{5}", a, b, range.Cope[2, 0], range.Cope[2, 1], range.Cope[3, 0], range.Cope[3, 1])));

                        for (int c = range.Cope[2, 0]; c <= range.Cope[2, 1]; c++)
                        {
                            Pool4C.Add(CreateIpRange(string.Format("{0}.{1}.{2}.{3}-{4}", a, b, c, range.Cope[3, 0], range.Cope[3, 1])));
                        }
                    }
                }
            }
        }
        static IpRange()
        {
            ImportIpv4Range("1.179.248-255.0-255");
            ImportIpv4Range("103.246.187.0-255");
            ImportIpv4Range("103.25.178.4-59");
            ImportIpv4Range("106.162.192.148-187");
            ImportIpv4Range("106.162.198.84-123");
            ImportIpv4Range("106.162.216.20-123");
            ImportIpv4Range("107.167.160-191.0-255");
            ImportIpv4Range("107.178.192-255.0-255");
            ImportIpv4Range("107.188.128-255.0-255");
            ImportIpv4Range("108.170.192-255.0-255");
            ImportIpv4Range("108.177.0-127.0-255");
            ImportIpv4Range("108.59.80-95.0-255");
            ImportIpv4Range("109.232.83.64-127");
            ImportIpv4Range("111.168.255.20-187");
            ImportIpv4Range("111.92.162.4-59");
            ImportIpv4Range("113.197.105-106.0-255");
            ImportIpv4Range("118.174.24-27.0-255");
            ImportIpv4Range("12.216.80.0-255");
            ImportIpv4Range("121.78.74.68-123");
            ImportIpv4Range("123.205.250-251.68-190");
            ImportIpv4Range("130.211.0-255.0-255");
            ImportIpv4Range("130.211.0-255.0-255");
            ImportIpv4Range("142.250-251.0-255.0-255");
            ImportIpv4Range("146.148.0-127.0-255");
            ImportIpv4Range("149.126.86.1-59");
            ImportIpv4Range("149.3.177.0-255");
            ImportIpv4Range("162.216.148-151.0-255");
            ImportIpv4Range("162.222.176-183.0-255");
            ImportIpv4Range("163.28.116.1-59");
            ImportIpv4Range("163.28.83.143-187");
            ImportIpv4Range("172.217.0-255.0-255");
            ImportIpv4Range("172.253.0-255.0-255");
            ImportIpv4Range("173.194.0-255.0-255");
            ImportIpv4Range("173.255.112-127.0-255");
            ImportIpv4Range("178.45.251.4-123");
            ImportIpv4Range("178.60.128.1-63");
            ImportIpv4Range("185.25.28-29.0-255");
            ImportIpv4Range("192.119.16-31.0-255");
            ImportIpv4Range("192.158.28-31.0-255");
            ImportIpv4Range("192.178-179.0-255.0-255");
            ImportIpv4Range("192.200.224-255.0-255");
            ImportIpv4Range("193.120.166.64-127");
            ImportIpv4Range("193.134.255.0-255");
            ImportIpv4Range("193.142.125.0-255");
            ImportIpv4Range("193.186.4.0-255");
            ImportIpv4Range("193.192.226.128-191");
            ImportIpv4Range("193.192.250.128-191");
            ImportIpv4Range("193.200.222.0-255");
            ImportIpv4Range("193.247.193.0-255");
            ImportIpv4Range("193.90.147.0-123");
            ImportIpv4Range("193.92.133.0-63");
            ImportIpv4Range("194.100.132.128-143");
            ImportIpv4Range("194.110.194.0-255");
            ImportIpv4Range("194.78.20.16-31");
            ImportIpv4Range("194.78.99.0-255");
            ImportIpv4Range("195.100.224.112-127");
            ImportIpv4Range("195.141.3.24-27");
            ImportIpv4Range("195.205.170.64-79");
            ImportIpv4Range("195.229.194.88-95");
            ImportIpv4Range("195.244.106.0-255");
            ImportIpv4Range("195.244.120.144-159");
            ImportIpv4Range("195.249.20.192-255");
            ImportIpv4Range("195.65.133.128-135");
            ImportIpv4Range("195.76.16.136-143");
            ImportIpv4Range("195.81.83.176-207");
            ImportIpv4Range("196.3.58-59.0-255");
            ImportIpv4Range("197.199.253-254.1-59");
            ImportIpv4Range("197.84.128.0-63");
            ImportIpv4Range("199.192.112-115.0-255");
            ImportIpv4Range("199.223.232-239.0-255");
            ImportIpv4Range("202.39.143.1-123");
            ImportIpv4Range("203.116.165.129-255");
            ImportIpv4Range("203.117.34-37.132-187");
            ImportIpv4Range("203.165.13-14.210-251");
            ImportIpv4Range("203.211.0.4-59");
            ImportIpv4Range("203.66.124.129-251");
            ImportIpv4Range("207.223.160-175.0-255");
            ImportIpv4Range("208.117.224-255.0-255");
            ImportIpv4Range("208.65.152-155.0-255");
            ImportIpv4Range("209.85.128-255.0-255");
            ImportIpv4Range("210.139.253.20-251");
            ImportIpv4Range("210.153.73.20-123");
            ImportIpv4Range("210.242.125.20-59");
            ImportIpv4Range("210.61.221.65-187");
            ImportIpv4Range("212.154.168.224-255");
            ImportIpv4Range("212.162.51.64-127");
            ImportIpv4Range("212.181.117.144-159");
            ImportIpv4Range("212.188.10.0-255");
            ImportIpv4Range("212.188.15.0-255");
            ImportIpv4Range("212.188.7.0-255");
            ImportIpv4Range("213.186.229.0-63");
            ImportIpv4Range("213.187.184.68-71");
            ImportIpv4Range("213.240.44.0-31");
            ImportIpv4Range("213.252.15.0-31");
            ImportIpv4Range("213.31.219.80-87");
            ImportIpv4Range("216.21.160-175.0-255");
            ImportIpv4Range("216.239.32-63.0-255");
            ImportIpv4Range("216.58.192-223.0-255");
            ImportIpv4Range("217.149.45.16-31");
            ImportIpv4Range("217.163.7.0-255");
            ImportIpv4Range("217.193.96.38");
            ImportIpv4Range("217.28.250.44-47");
            ImportIpv4Range("217.28.253.32-33");
            ImportIpv4Range("217.30.152.192-223");
            ImportIpv4Range("217.33.127.208-223");
            ImportIpv4Range("218.176.242.4-251");
            ImportIpv4Range("218.189.25.129-.187");
            ImportIpv4Range("218.253.0.76-187");
            ImportIpv4Range("23.228.128-191.0-255");
            ImportIpv4Range("23.236.48-63.0-255");
            ImportIpv4Range("23.251.128-159.0-255");
            ImportIpv4Range("23.255.128-255.0-255");
            ImportIpv4Range("24.156.131.0-255");
            ImportIpv4Range("31.209.137.0-255");
            ImportIpv4Range("31.7.160.192-255");
            ImportIpv4Range("37.228.69.0-63");
            ImportIpv4Range("41.206.96.1-251");
            ImportIpv4Range("41.84.159.12-30");
            ImportIpv4Range("60.199.175.1-187");
            ImportIpv4Range("61.219.131.65-251");
            ImportIpv4Range("62.0.54.64-127");
            ImportIpv4Range("62.1.38.64-191");
            ImportIpv4Range("62.116.207.0-63");
            ImportIpv4Range("62.197.198.193-251");
            ImportIpv4Range("62.20.124.48-63");
            ImportIpv4Range("62.201.216.196-251");
            ImportIpv4Range("63.243.168.0-255");
            ImportIpv4Range("64.15.112-127.0-255");
            ImportIpv4Range("64.233.160-191.0-255");
            ImportIpv4Range("64.9.224-255.0-255");
            ImportIpv4Range("66.102.0-15.0-255");
            ImportIpv4Range("66.185.84.0-255");
            ImportIpv4Range("66.249.64-95.0-255");
            ImportIpv4Range("69.17.141.0-255");
            ImportIpv4Range("70.32.128-159.0-255");
            ImportIpv4Range("72.14.192-255.0-255");
            ImportIpv4Range("74.125.0-255.0-255");
            ImportIpv4Range("77.109.131.208-223");
            ImportIpv4Range("77.40.222.224-231");
            ImportIpv4Range("77.42.248-255.0-255");
            ImportIpv4Range("77.66.9.64-123");
            ImportIpv4Range("78.8.8.176-191");
            ImportIpv4Range("8.15.202.0-255");
            ImportIpv4Range("8.22.56.0-255");
            ImportIpv4Range("8.34.208-223.0-255");
            ImportIpv4Range("8.35.192-207.0-255");
            ImportIpv4Range("8.6.48-55.0-255");
            ImportIpv4Range("8.8.4.0-255");
            ImportIpv4Range("8.8.8.0-255");
            ImportIpv4Range("80.227.152.32-39");
            ImportIpv4Range("80.228.65.128-191");
            ImportIpv4Range("80.231.69.0-63");
            ImportIpv4Range("80.239.168.192-255");
            ImportIpv4Range("80.80.3.176-191");
            ImportIpv4Range("81.175.29.128-191");
            ImportIpv4Range("81.93.175.232-239");
            ImportIpv4Range("82.135.118.0-63");
            ImportIpv4Range("83.100.221.224-255");
            ImportIpv4Range("83.141.89.124-127");
            ImportIpv4Range("83.145.196.128-191");
            ImportIpv4Range("83.220.157.100-103");
            ImportIpv4Range("83.94.121.128-255");
            ImportIpv4Range("84.233.219.144-159");
            ImportIpv4Range("84.235.77.1-251");
            ImportIpv4Range("85.182.250.0-191");
            ImportIpv4Range("86.127.118.128-191");
            ImportIpv4Range("87.244.198.160-191");
            ImportIpv4Range("88.159.13.192-255");
            ImportIpv4Range("89.207.224-231.0-255");
            ImportIpv4Range("89.96.249.160-175");
            ImportIpv4Range("92.45.86.16-31");
            ImportIpv4Range("93.123.23.1-59");
            ImportIpv4Range("93.183.211.192-255");
            ImportIpv4Range("93.94.217-218.0-31");
            ImportIpv4Range("94.200.103.64-71");
            ImportIpv4Range("94.40.70.0-63");
            ImportIpv4Range("95.143.84.128-191");
            //ip range
            ImportIpv4Range("61.19.1-2.0-127");
            ImportIpv4Range("61.19.8.0-127");
            ImportIpv4Range("113.21.24.0-127");
            //thx for alienwaresky
            ImportIpv4Range("118.143.88.16-123");
            ImportIpv4Range("202.86.162.20-187");
            ImportIpv4Range("139.175.107.20-187");
            ImportIpv4Range("223.26.69.16-59");
            ImportIpv4Range("220.255.5-6.20-251");
            ImportIpv4Range("202.65.246.84-123");
            ImportIpv4Range("103.1.139.148-251");
            ImportIpv4Range("116.92.194.148-187");
            ImportIpv4Range("58.145.238.20-59");
            //
            ImportIpv4Range("41.201.128.20-59");
            ImportIpv4Range("41.201.164.20-59");
            ImportIpv4Range("222.255.120.15-59");
            //OpenerDNS
            //ImportIpv4Range("119.81.145.120-127");
            //ImportIpv4Range("119.81.142.202");
            //ImportIpv4Range("23.239.5.106");
			//ImportIpv4Range(74.207.242.141");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace GoGo_Tester
{
    static class Exts
    {
        public static byte[] GetBytes(this uint o)
        {
            return BitConverter.GetBytes(o);
        }
        public static byte[] GetRevBytes(this uint o)
        {
            var d = BitConverter.GetBytes(o);
            Array.Reverse(d);
            return d;
        }

        public static void Reverse(this byte[] o)
        {
            Array.Reverse(o);
        }
    }
    class IpPool
    {

        private static readonly Regex RxIpv4 = new Regex(@"([\d\.]+\d)/(\d+)|([\d\.\-]+)|([\w\-\.]+)", RegexOptions.Compiled);
        private static readonly Random Rand = new Random();
        public static IpPool CreateFromText(string text)
        {
            var pool = new IpPool();
            var ms = RxIpv4.Matches(text);
            try
            {
                foreach (Match m in ms)
                {
                    if (m.Groups[2].Value != string.Empty)
                    {
                        var ip = IPAddress.Parse(m.Groups[1].Value);
                        var mov = 32 - Convert.ToInt32(m.Groups[2].Value);
                        var msk = (0xffffffff >> mov) << mov;
                        var bs = ip.GetAddressBytes();
                        Array.Reverse(bs);
                        var num = BitConverter.ToUInt32(bs, 0);
                        var min = num & msk;
                        var max = num | (0xffffffff ^ msk);
                        pool.Copes.Add(new Tuple<uint, uint>(min, max));
                    }
                    else if (m.Groups[3].Value != string.Empty)
                    {
                        var sps = m.Groups[3].Value.Split('.');
                        if (sps.Length == 4)
                        {
                            uint min = 0, max = 0;
                            for (int i = 0; i < 4; i++)
                            {
                                if (sps[i].Contains("-"))
                                {
                                    var tps = sps[i].Split('-');
                                    if (tps.Length == 2)
                                    {
                                        var va = uint.Parse(tps[0]);
                                        var vb = uint.Parse(tps[1]);

                                        min = (min << 8) | (va & 0xff);
                                        max = (max << 8) | (vb & 0xff);
                                    }
                                }
                                else
                                {
                                    var v = uint.Parse(sps[i]);
                                    min = (min << 8) | (v & 0xff);
                                    max = (max << 8) | (v & 0xff);
                                }
                            }
                            if ((min & 0xff000000) > 0)
                                pool.Copes.Add(new Tuple<uint, uint>(min, max));
                        }
                    }
                    else if (m.Groups[4].Value != string.Empty)
                    {
                        foreach (var num in from addr in Dns.GetHostAddresses(m.Groups[4].Value) select addr.GetAddressBytes() into data where data.Length == 4 select BitConverter.ToUInt32(data, 0))
                            pool.Copes.Add(new Tuple<uint, uint>(num, num));
                    }
                }
            }
            catch { }
            return pool;
        }

        private IpPool() { }

        public int Count
        {
            get { return (int)Copes.Sum(t => t.Item2 - t.Item1 + 1); }
        }

        public List<Tuple<uint, uint>> Copes = new List<Tuple<uint, uint>>();
        public IPAddress GetRandomIp()
        {
            var cope = Copes[Rand.Next(Copes.Count)];
            var val = (cope.Item2 - cope.Item1) + 1;
            var num = cope.Item1 + (uint)Rand.Next((int)val);

            var data = num.GetRevBytes();
            data[3] = (byte)((data[3] == 0) ? 1 : data[3]);
            data[3] = (byte)((data[3] == 255) ? 254 : data[3]);

            return new IPAddress(data);
        }

    }
}

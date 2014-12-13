using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    class IpPool : HashSet<IPAddress>
    {

        private static readonly Regex RxIpv4 = new Regex(@"([\d\.]+\d)/(\d+)|([\d\.\-]+)|([\w\-\.]+)", RegexOptions.Compiled);
        public static IpPool CreateFromText(string text)
        {
            var pool = new IpPool { OString = text };
            pool.LoadCopes();
            return pool;
        }

        private void LoadCopes()
        {
            try
            {
                Clear();
                var ms = RxIpv4.Matches(OString);
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

                        ImportRange(min, max);
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
                                ImportRange(min, max);
                        }
                    }
                    else if (m.Groups[4].Value != string.Empty)
                    {
                        foreach (var addr in from addr in Dns.GetHostAddresses(m.Groups[4].Value) where addr.AddressFamily == AddressFamily.InterNetwork let dat = addr.GetAddressBytes() where dat[3] != 0 && dat[3] != 255 select addr)
                            Add(addr);
                    }
                }
            }
            catch { }
        }
        private IpPool() { }

        private string OString = string.Empty;

        private void ImportRange(uint min, uint max)
        {
            for (var num = min; num <= max; num++)
            {
                var dat = num.GetRevBytes();

                if (dat[3] == 0 || dat[3] == 255) continue;

                Add(new IPAddress(dat));
            }
        }
    }
}

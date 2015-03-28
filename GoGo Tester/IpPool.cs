using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using ARSoft.Tools.Net.Dns;

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
    }

    public struct Ip
    {
        public byte[] AddressBytes;

        public Ip(byte[] bytes, bool _le = false)
        {
            if (bytes.Length == 4 || bytes.Length == 16)
            {
                if (_le) Array.Reverse(bytes);
                AddressBytes = bytes;
            }
            else
            {
                AddressBytes = new byte[] { 0, 0, 0, 0 };
            }
        }

        public IPAddress GetIpAddress()
        {
            return new IPAddress(AddressBytes);
        }

        public AddressFamily GetAddressFamily()
        {
            return AddressBytes.Length == 4 ? AddressFamily.InterNetwork : AddressFamily.InterNetworkV6;
        }

        public override int GetHashCode()
        {
            ulong sum = 0;
            for (var i = 0; i < AddressBytes.Length / 4; i++)
                sum += BitConverter.ToUInt32(AddressBytes, i * 4);
            var high = sum & 0xffffffff00000000;
            return (int)(sum + high);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            if (AddressBytes.Length == 4)
                return string.Format("{0}.{1}.{2}.{3}",
                    AddressBytes[0].ToString("D"), AddressBytes[1].ToString("D"), AddressBytes[2].ToString("D"), AddressBytes[3].ToString("D"));

            var sbd = new StringBuilder();
            for (int i = 0; i < AddressBytes.Length; i++)
            {
                if (i > 0 && i % 2 == 0)
                    sbd.Append(":");
                sbd.Append(AddressBytes[i].ToString("X02"));
            }

            while (sbd[0] == '0')
                sbd.Remove(0, 1);

            var fstr = sbd.Replace("0000", "").ToString();
            while (fstr.Contains(":0"))
                fstr = fstr.Replace(":0", ":");
            while (fstr.Contains(":::"))
                fstr = fstr.Replace(":::", "::");

            return fstr;
        }

        public static Ip Parse(string str)
        {
            return new Ip(IPAddress.Parse(str).GetAddressBytes());
        }
    }
    class IpPool : HashSet<Ip>
    {
        private static readonly Regex RxIpv4 = new Regex(@"(?<astr>(\d{1,3}\.){3}\d{1,3})/(?<mstr>\d{1,2})|(?<range>((\d{1,3}\-\d{1,3}|\d{1,3})\.){3}(\d{1,3}\-\d{1,3}|\d{1,3}))|(?<domain>[\w\-\.]+\.\w+)", RegexOptions.Compiled);

        public static IpPool CreateFromText(string text)
        {
            var pool = new IpPool();
            try
            {
                var ms = RxIpv4.Matches(text);
                foreach (Match m in ms)
                {
                    var astr = m.Groups["astr"].Value;
                    var mstr = m.Groups["mstr"].Value;
                    var range = m.Groups["range"].Value;
                    var domain = m.Groups["domain"].Value;
                    uint min = 0, max = 0;
                    if (mstr != string.Empty)
                    {
                        var ip = IPAddress.Parse(astr);
                        var mov = 32 - Convert.ToInt32(mstr);
                        var msk = (0xffffffff >> mov) << mov;
                        var bs = ip.GetAddressBytes();
                        Array.Reverse(bs);
                        var num = BitConverter.ToUInt32(bs, 0);
                        min = num & msk;
                        max = num | (0xffffffff ^ msk);

                        pool.ImportRange(min, max);
                    }
                    else if (range != string.Empty)
                    {
                        var sps = range.Split('.');
                        for (var i = 0; i < 4; i++)
                            if (sps[i].Contains("-"))
                            {
                                var tps = sps[i].Split('-');

                                var va = uint.Parse(tps[0]);
                                var vb = uint.Parse(tps[1]);

                                min = (min << 8) | (va & 0xff);
                                max = (max << 8) | (vb & 0xff);
                            }
                            else
                            {
                                var v = uint.Parse(sps[i]);
                                min = (min << 8) | (v & 0xff);
                                max = (max << 8) | (v & 0xff);
                            }

                        pool.ImportRange(min, max);
                    }
                    else if (domain != string.Empty)
                    {
                        foreach (var ip in from addr in Dns.GetHostAddresses(domain) where addr.AddressFamily == AddressFamily.InterNetwork let dat = addr.GetAddressBytes() where dat[3] != 0 && dat[3] != 255 select new Ip(dat))
                            pool.Add(ip);
                    }
                }
            }
            catch { }
            pool.TrimExcess();
            return pool;
        }

        private IpPool() { }


        private void ImportRange(uint min, uint max)
        {
            if ((min & 0xff000000) == 0) return;

            for (var num = min; num <= max; num++)
            {
                var dat = num.GetRevBytes();

                if (dat[3] == 0 || dat[3] == 255) continue;

                Add(new Ip(dat));
            }
        }

        #region spf
        public static IpPool CreateFromDomains(string[] domains)
        {
            var caches = new HashSet<string>(domains);
            var ranges = new HashSet<string>();

            var dns = DnsClient.Default;

            foreach (var dom in domains)
                Lookup(dom, dns, caches, ranges);

            var sbd = new StringBuilder();
            foreach (var range in ranges)
                sbd.Append(range).Append(" ");

            return CreateFromText(sbd.ToString());
        }
        private static readonly Regex RxSpfInclude = new Regex(@"include:([^\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RxSpfIpv4 = new Regex(@"ip4:([^\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static void Lookup(string domaim, DnsClient dns, ISet<string> caches, ISet<string> ranges)
        {
            foreach (var resp in dns.Resolve(domaim, RecordType.Txt).AnswerRecords.Select(resp => resp.ToString()))
            {
                foreach (Match m in RxSpfIpv4.Matches(resp))
                    ranges.Add(m.Groups[1].Value);

                foreach (
                    var dom in RxSpfInclude.Matches(resp).Cast<Match>().Select(m => m.Groups[1].Value).Where(caches.Add))
                    Lookup(dom, dns, caches, ranges);
            }
        }
        #endregion
    }
}

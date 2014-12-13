using System.Net;

namespace GoGo_Tester
{
    class TestInfo
    {
        public IPEndPoint Target { get; }
        public bool PortOk { get; set; }
        public long PortTime { get; set; }
        public string PortMsg { get; set; }
        public bool HttpOk { get; set; }
        public string HttpMsg { get; set; }
        public int PassCount { get; set; }
        public string Bandwidth { get; set; }
        public string CName { get; set; }
        public TestInfo(IPAddress addr)
        {
            Target = new IPEndPoint(addr, 443);
            HttpOk = PortOk = false;
            PortTime = 0;
            PortMsg = HttpMsg = "n/a";
            PassCount = 0;
            Bandwidth = "n/a";
            CName = "n/a";
        }
    }
}

using System.Net;

namespace GoGo_Tester
{
    class TestInfo
    {
        public Ip IP { get; private set; }
        public int Port { get; private set; }
        public bool PortOk { get; set; }
        public long PortTime { get; set; }
        public string PortMsg { get; set; }
        public bool HttpOk { get; set; }
        public string HttpMsg { get; set; }
        public int PassCount { get; set; }
        public string Bandwidth { get; set; }
        public string CName { get; set; }
        public EndPoint Target { get { return new IPEndPoint(IP.GetIpAddress(), Port); } }

        public TestInfo(Ip addr, int port = 443)
        {
            IP = addr;
            Port = port;
            HttpOk = PortOk = false;
            PortTime = 0;
            PortMsg = HttpMsg = "n/a";
            PassCount = 0;
            Bandwidth = "n/a";
            CName = "n/a";
        }

        public TestInfo(IPAddress addr) : this(new Ip(addr.GetAddressBytes())) { }
    }
}

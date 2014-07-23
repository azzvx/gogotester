using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace GoGo_Tester
{
    static class Config
    {
        public static bool HighSpeed = true;

        public static bool UseProxy = false;
        public static IPEndPoint ProxyTarget = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8087);
        public static bool UseProxyAuth = false;
        public static string ProxyAuthUser = string.Empty;
        public static string ProxyAuthPswd = string.Empty;
        public static string ProxyAuthBase64 = string.Empty;
        public static Socket ProxySocket;

        public static int PingTimeout = 660;
        public static int SocketTimeout = 1650;
        public static int MaxThreads = 5;

        public static bool TestProxy()
        {

            ProxySocket = new Socket(ProxyTarget.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (ProxySocket.BeginConnect(ProxyTarget, null, null).AsyncWaitHandle.WaitOne(3000) && ProxySocket.Connected)
                {

                    var hbd = new StringBuilder();
                    hbd.AppendLine(string.Format("CONNECT {0}:{1} HTTP/1.1", ProxyTarget.Address, ProxyTarget.Port));
                    hbd.AppendLine(string.Format("Host: {0}:{1}", ProxyTarget.Address, ProxyTarget.Port));
                    hbd.AppendLine("Proxy-Connection: Keep-Alive");

                    if (UseProxyAuth)
                    {
                        hbd.AppendLine(string.Format("Proxy-Authorization: Basic {0}", ProxyAuthBase64));
                    }

                    hbd.AppendLine();

                    var msg = Encoding.UTF8.GetBytes(hbd.ToString());


                    ProxySocket.Send(msg);

                    var buf = new byte[1024];

                    ProxySocket.Receive(buf);

                    var str = Encoding.UTF8.GetString(buf);

                   return str.Contains("200");
                }
            }
            catch (Exception) { }
            
            return false;
        }
    }
}

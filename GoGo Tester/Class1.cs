//using System;
//using System.Collections;
//using System.Diagnostics;
//using System.IO;
//using System.Net;
//using System.Net.Security;
//using System.Net.Sockets;
//using System.Security.Authentication;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
///************************************************************************/
///* Author:huliang
// * Email:huliang@yahoo.cn
// * QQ:12658501
// * 说明：转载请注明出处
///************************************************************************/

//namespace iGame
//{
//    class HttpArgs
//    {
//        public string Url { get; set; }
//        public string Host { get; set; }
//        public string Accept { get; set; }
//        public string Referer { get; set; }
//        public string Cookie { get; set; }
//        public string Body { get; set; }
//    }

//    static class HttpHelper
//    {
//        /// <summary>
//        /// 提交方法
//        /// </summary>
//        enum HttpMethod
//        {
//            GET,
//            POST
//        }

//        #region HttpWebRequest & HttpWebResponse

//        /// <summary>
//        /// Get方法
//        /// </summary>
//        /// <param name="geturl">请求地址</param>
//        /// <param name="cookieser">Cookies存储器</param>
//        /// <returns>请求返回的Stream</returns>
//        public static string Get(string url,
//            CookieContainer cookies,
//            Encoding encoding)
//        {
//            return InternalHttp(HttpMethod.GET, url, null, cookies, encoding);
//        }

//        public static Stream Get(string url,
//            CookieContainer cookies)
//        {
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
//            request.Method = "GET";
//            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1;MSIE 6.0;)";
//            request.CookieContainer = cookies;
//            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//            return response.GetResponseStream();
//        }

//        /// <summary>
//        /// Post方法
//        /// </summary>
//        /// <param name="posturl">请求地址</param>
//        /// <param name="bytes">Post数据</param>
//        /// <param name="cookieser">Cllkies存储器</param>
//        /// <returns>请求返回的流</returns>
//        public static string Post(string url,
//            byte[] bytes,
//            CookieContainer cookies,
//            Encoding encoding)
//        {
//            return InternalHttp(HttpMethod.POST, url, bytes, cookies, encoding);
//        }

//        /// <summary>
//        /// Http操作
//        /// </summary>
//        /// <param name="method">请求方式</param>
//        /// <param name="url">请求地址</param>
//        /// <param name="bytes">提交数据</param>
//        /// <param name="cookieser">Cookies存储器</param>
//        /// <returns>请求结果</returns>
//        static string InternalHttp(HttpMethod method,
//            string url,
//            byte[] bytes,
//            CookieContainer cookies,
//            Encoding encoding)
//        {
//            if (string.IsNullOrEmpty(url))
//                throw new ArgumentNullException("访问url不能为空");
//            if (method == HttpMethod.POST)
//            {
//                if (bytes == null)
//                    throw new ArgumentNullException("提交的post数据不能为空");
//            }
//            if (cookies == null)
//                throw new ArgumentNullException("Cookies存储器不能为空");
//            try
//            {
//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
//                request.Method = method.ToString();
//                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1;MSIE 9.0;)";
//                request.CookieContainer = cookies;
//                if (method == HttpMethod.POST)
//                {
//                    request.ContentType = "application/x-www-form-urlencoded";
//                    request.ContentLength = bytes.Length;
//                    using (Stream stream = request.GetRequestStream())
//                    {
//                        stream.Write(bytes, 0, bytes.Length);
//                        stream.Flush();
//                        stream.Close();
//                    }
//                }
//                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
//                    {
//                        return reader.ReadToEnd();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.Message);
//            }
//            return null;
//        }

//        #endregion

//        #region Ssl Socket

//        static bool ValidateServerCertificate(
//                 object sender,
//                 X509Certificate certificate,
//                 X509Chain chain,
//                 SslPolicyErrors sslPolicyErrors)
//        {
//            /*
//            if (sslPolicyErrors == SslPolicyErrors.None)
//                return true;
//            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
//            return false;
//            */
//            return true;
//        }

//        public static byte[] Get(IPEndPoint endpoint, HttpArgs args, X509CertificateCollection certificates)
//        {
//            return InternalSslSocketHttp(endpoint, certificates, args, HttpMethod.GET);
//        }

//        public static byte[] Post(IPEndPoint endpoint,
//            HttpArgs args,
//            X509CertificateCollection certificates)
//        {
//            return InternalSslSocketHttp(endpoint, certificates, args, HttpMethod.POST);
//        }

//        static byte[] InternalSslSocketHttp(IPEndPoint endpoint,
//            X509CertificateCollection certificates,
//            HttpArgs args,
//            HttpMethod method)
//        {
//            TcpClient tcp = new TcpClient();
//            try
//            {
//                tcp.Connect(endpoint);
//                if (tcp.Connected)
//                {
//                    using (SslStream ssl = new SslStream(tcp.GetStream(),
//                        false,
//                        new RemoteCertificateValidationCallback(ValidateServerCertificate),
//                        null))
//                    {
//                        ssl.AuthenticateAsClient("ServerName",
//                            certificates,
//                            SslProtocols.Tls,
//                            false);
//                        if (ssl.IsAuthenticated)
//                        {
//                            byte[] buff = ParseHttpArgs(method, args);  //生成协议包
//                            ssl.Write(buff);
//                            ssl.Flush();
//                            return ParseSslResponse(endpoint, ssl, args, certificates);

//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//            return null;
//        }

//        /// <summary>
//        /// 解析 Ssl Response
//        /// </summary>
//        /// <param name="endpoint"></param>
//        /// <param name="ssl"></param>
//        /// <param name="args"></param>
//        /// <param name="certificates"></param>
//        /// <returns></returns>
//        private static byte[] ParseSslResponse(IPEndPoint endpoint,
//            SslStream ssl,
//            HttpArgs args,
//            X509CertificateCollection certificates)
//        {
//            //尝试10秒时间读取协议头
//            CancellationTokenSource source = new CancellationTokenSource();
//            Task<string> myTask = Task.Factory.StartNew<string>(
//                new Func<object, string>(ReadSslHeaderProcess),
//                ssl,
//                source.Token);
//            if (myTask.Wait(10000))
//            {
//                string header = myTask.Result;
//                if (header.StartsWith("HTTP/1.1 302"))
//                {
//                    int start = header
//                        .ToUpper().IndexOf("LOCATION");
//                    if (start > 0)
//                    {
//                        string temp = header.Substring(start, header.Length - start);
//                        string[] sArry = Regex.Split(temp, "\r\n");
//                        args.Url = sArry[0].Remove(0, 10);
//                        return Get(endpoint, args, certificates);  //注意：302协议需要重定向
//                    }
//                }
//                else if (header.StartsWith("HTTP/1.1 200"))  //继续读取内容
//                {
//                    int start = header
//                           .ToUpper().IndexOf("CONTENT-LENGTH");
//                    int content_length = 0;
//                    if (start > 0)
//                    {
//                        string temp = header.Substring(start, header.Length - start);
//                        string[] sArry = Regex.Split(temp, "\r\n");
//                        content_length = Convert.ToInt32(sArry[0].Split(':')[1]);
//                        if (content_length > 0)
//                        {
//                            byte[] bytes = new byte[content_length];
//                            if (ssl.Read(bytes, 0, bytes.Length) > 0)
//                            {
//                                return bytes;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        //不存在Content-Length协议头
//                        return ParseSslResponse(ssl);
//                    }
//                }
//                else
//                {
//                    return Encoding.Default.GetBytes(header);
//                }
//            }
//            else
//            {
//                source.Cancel();  //超时的话，别忘记取消任务哦
//            }
//            return null;
//        }

//        /// <summary>
//        ///  读取协议头
//        /// </summary>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        static string ReadSslHeaderProcess(object args)
//        {
//            SslStream ssl = (SslStream)args;
//            StringBuilder bulider = new StringBuilder();
//            while (true)
//            {
//                int read = ssl.ReadByte();
//                if (read != -1)
//                {
//                    byte b = (byte)read;
//                    bulider.Append((char)b);
//                }
//                string temp = bulider.ToString();
//                if (temp.Contains("\r\n\r\n"))
//                {
//                    break;
//                }
//            }
//            return bulider.ToString();
//        }

//        /// <summary>
//        /// 注意：此函数可能产生死循环
//        /// </summary>
//        /// <param name="ssl"></param>
//        /// <returns></returns>
//        static byte[] ParseSslResponse(SslStream ssl)
//        {
//            //没有指定协议头，尝试读取至</html>
//            ArrayList array = new ArrayList();
//            StringBuilder bulider = new StringBuilder();
//            int length = 0;
//            while (true)
//            {
//                byte[] buff = new byte[1024];
//                int len = ssl.Read(buff, 0, buff.Length);
//                if (len > 0)
//                {
//                    length += len;
//                    byte[] reads = new byte[len];
//                    Array.Copy(buff, 0, reads, 0, len);
//                    array.Add(reads);
//                    bulider.Append(Encoding.Default.GetString(reads));
//                }
//                string temp = bulider.ToString();
//                if (temp.ToUpper().Contains("</HTML>"))
//                {
//                    break;
//                }
//            }
//            byte[] bytes = new byte[length];
//            int index = 0;
//            for (int i = 0; i < array.Count; i++)
//            {
//                byte[] temp = (byte[])array[i];
//                Array.Copy(temp, 0, bytes,
//                    index, temp.Length);
//                index += temp.Length;
//            }
//            return bytes;
//        }

//        #endregion

//        #region Socket

//        public static byte[] Get(IPEndPoint endpoint,
//            HttpArgs args)
//        {
//            return InternalSocketHttp(endpoint, args, HttpMethod.GET);
//        }

//        public static byte[] Post(IPEndPoint endpoint,
//            HttpArgs args)
//        {
//            return InternalSocketHttp(endpoint, args, HttpMethod.POST);
//        }

//        static byte[] InternalSocketHttp(IPEndPoint endpoint,
//           HttpArgs args,
//           HttpMethod method)
//        {
//            using (Socket sK = new Socket(AddressFamily.InterNetwork,
//                        SocketType.Stream,
//                        ProtocolType.Tcp))
//            {
//                try
//                {
//                    sK.Connect(endpoint);
//                    if (sK.Connected)
//                    {
//                        byte[] buff = ParseHttpArgs(method, args);
//                        if (sK.Send(buff) > 0)
//                        {
//                            return ParseResponse(endpoint, sK, args);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                }
//            }
//            return null;
//        }

//        private static byte[] ParseResponse(IPEndPoint endpoint,
//             Socket sK,
//             HttpArgs args)
//        {
//            //尝试10秒时间读取协议头
//            CancellationTokenSource source = new CancellationTokenSource();
//            Task<string> myTask = Task.Factory.StartNew<string>(
//                new Func<object, string>(ReadHeaderProcess),
//                sK,
//                source.Token);
//            if (myTask.Wait(10000))
//            {
//                string header = myTask.Result;
//                if (header.StartsWith("HTTP/1.1 302"))
//                {
//                    int start = header
//                        .ToUpper().IndexOf("LOCATION");
//                    if (start > 0)
//                    {
//                        string temp = header.Substring(start, header.Length - start);
//                        string[] sArry = Regex.Split(temp, "\r\n");
//                        args.Url = sArry[0].Remove(0, 10);
//                        return Get(endpoint, args);  //注意：302协议需要重定向
//                    }
//                }
//                else if (header.StartsWith("HTTP/1.1 200"))  //继续读取内容
//                {
//                    int start = header
//                           .ToUpper().IndexOf("CONTENT-LENGTH");
//                    int content_length = 0;
//                    if (start > 0)
//                    {
//                        string temp = header.Substring(start, header.Length - start);
//                        string[] sArry = Regex.Split(temp, "\r\n");
//                        content_length = Convert.ToInt32(sArry[0].Split(':')[1]);
//                        if (content_length > 0)
//                        {
//                            byte[] bytes = new byte[content_length];
//                            if (sK.Receive(bytes) > 0)
//                            {
//                                return bytes;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        //不存在Content-Length协议头
//                        return ParseResponse(sK);
//                    }
//                }
//                else
//                {
//                    return Encoding.Default.GetBytes(header);
//                }
//            }
//            else
//            {
//                source.Cancel();  //超时的话，别忘记取消任务哦
//            }
//            return null;
//        }

//        /// <summary>
//        ///  读取协议头
//        /// </summary>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        static string ReadHeaderProcess(object args)
//        {
//            Socket sK = (Socket)args;
//            StringBuilder bulider = new StringBuilder();
//            while (true)
//            {
//                byte[] buff = new byte[1];
//                int read = sK.Receive(buff, SocketFlags.None);
//                if (read > 0)
//                {
//                    bulider.Append((char)buff[0]);
//                }
//                string temp = bulider.ToString();
//                if (temp.Contains("\r\n\r\n"))
//                {
//                    break;
//                }
//            }
//            return bulider.ToString();
//        }

//        /// <summary>
//        /// 注意：此函数可能产生死循环
//        /// </summary>
//        /// <param name="ssl"></param>
//        /// <returns></returns>
//        static byte[] ParseResponse(Socket sK)
//        {
//            ArrayList array = new ArrayList();
//            StringBuilder bulider = new StringBuilder();
//            int length = 0;
//            while (true)
//            {
//                byte[] buff = new byte[1024];
//                int len = sK.Receive(buff);
//                if (len > 0)
//                {
//                    length += len;
//                    byte[] reads = new byte[len];
//                    Array.Copy(buff, 0, reads, 0, len);
//                    array.Add(reads);
//                    bulider.Append(Encoding.Default.GetString(reads));
//                }
//                string temp = bulider.ToString();
//                if (temp.ToUpper().Contains("</HTML>"))
//                {
//                    break;
//                }
//            }
//            byte[] bytes = new byte[length];
//            int index = 0;
//            for (int i = 0; i < array.Count; i++)
//            {
//                byte[] temp = (byte[])array[i];
//                Array.Copy(temp, 0, bytes,
//                    index, temp.Length);
//                index += temp.Length;
//            }
//            return bytes;
//        }
//        #endregion

//        #region  Helper

//        static byte[] ParseHttpArgs(HttpMethod method, HttpArgs args)
//        {
//            StringBuilder bulider = new StringBuilder();
//            if (method.Equals(HttpMethod.POST))
//            {
//                bulider.AppendLine(string.Format("POST {0} HTTP/1.1", args.Url));
//                bulider.AppendLine("Content-Type: application/x-www-form-urlencoded");
//            }
//            else
//            {
//                bulider.AppendLine(string.Format("GET {0} HTTP/1.1", args.Url));
//            }
//            bulider.AppendLine(string.Format("Host: {0}", args.Host));
//            bulider.AppendLine("User-Agent: Mozilla/5.0 (Windows NT 6.1; IE 9.0)");
//            if (!string.IsNullOrEmpty(args.Referer))
//                bulider.AppendLine(string.Format("Referer: {0}", args.Referer));
//            bulider.AppendLine("Connection: keep-alive");
//            bulider.AppendLine(string.Format("Accept: {0}", args.Accept));
//            bulider.AppendLine(string.Format("Cookie: {0}", args.Cookie));
//            if (method.Equals(HttpMethod.POST))
//            {
//                bulider.AppendLine(string.Format("Content-Length: {0}\r\n", Encoding.Default.GetBytes(args.Body).Length));
//                bulider.Append(args.Body);
//            }
//            else
//            {
//                bulider.Append("\r\n");
//            }
//            string header = bulider.ToString();
//            return Encoding.Default.GetBytes(header);
//        }

//        #endregion
//    }
//}
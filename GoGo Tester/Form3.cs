using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace GoGo_Tester
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            cbUseProxy.Checked = Config.UseProxy;
            tbAddr.Text = Config.ProxyTarget.Address.ToString();
            tbPort.Text = Config.ProxyTarget.Port.ToString();

            cbUseProxyAuth.Checked = Config.UseProxyAuth;
            tbUser.Text = Config.ProxyAuthUser;
            tbPswd.Text = Config.ProxyAuthPswd;
        }

        private void cbTestWithProxy_CheckedChanged(object sender, EventArgs e)
        {
            Config.UseProxy = cbUseProxy.Checked;
        }
        private void cbUseProxyAuth_CheckedChanged(object sender, EventArgs e)
        {
            Config.UseProxyAuth = cbUseProxyAuth.Checked;
        }


        private void bAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbUseProxy.Checked)
                {
                    Config.ProxyTarget = new IPEndPoint(IPAddress.Parse(tbAddr.Text.Trim()), Convert.ToInt32(tbPort.Text.Trim()));

                    if (cbUseProxyAuth.Checked)
                    {
                        Config.ProxyAuthUser = tbUser.Text.Trim();
                        Config.ProxyAuthPswd = tbPswd.Text.Trim();
                        if (Config.ProxyAuthUser == String.Empty || Config.ProxyAuthUser == String.Empty)
                        {
                            Config.UseProxyAuth = false;
                        }
                        else
                        {
                            Config.ProxyAuthBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", tbUser.Text, tbPswd.Text)));
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("存在不正确的信息！");
                return;
            }

            if (Config.TestProxy())
            {
                Close();
            }
            else
            {
                MessageBox.Show("无法连接到代理服务器！");
            }
        }

    }
}

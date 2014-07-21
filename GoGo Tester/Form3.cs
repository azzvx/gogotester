using System;
using System.Net;
using System.Windows.Forms;

namespace GoGo_Tester
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void cbTestWithProxy_CheckedChanged(object sender, EventArgs e)
        {
            Form1.UseProxy = cbTestWithProxy.Checked;
        }

        private void bAccept_Click(object sender, EventArgs e)
        {
            if (cbTestWithProxy.Checked)
            {
                try
                {
                    Form1.TestProxy = new WebProxy(tbAddr.Text, Convert.ToInt32(tbPort.Text))
                    {
                        Credentials = new NetworkCredential(tbUser.Text, tbPswd.Text)
                    };
                }
                catch (Exception) { }
            }

            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cbTestWithProxy.Checked = Form1.UseProxy;

            tbAddr.Text = Form1.TestProxy.Address.Host;
            tbPort.Text = Form1.TestProxy.Address.Port.ToString();
            if (Form1.TestProxy.Credentials != null)
            {
                tbUser.Text = ((NetworkCredential)Form1.TestProxy.Credentials).UserName;
                tbPswd.Text = ((NetworkCredential)Form1.TestProxy.Credentials).Password;
            }
        }
    }
}

using System;
using System.Windows.Forms;

namespace GoGo_Tester
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static int RandomNumber;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bOK_Click(object sender, EventArgs e)
        {
            RandomNumber = Convert.ToInt32(numericUpDown1.Value);
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            RandomNumber = 0;
            Close();
        }
    }
}

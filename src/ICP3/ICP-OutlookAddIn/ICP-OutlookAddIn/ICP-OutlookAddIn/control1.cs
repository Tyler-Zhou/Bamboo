using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP_OutlookAddIn
{
    public partial class control1 : UserControl
    {
        public control1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            textBox1.Text = "asdfas";
            textBox2.Text = "abcdef";
        }
    }
}

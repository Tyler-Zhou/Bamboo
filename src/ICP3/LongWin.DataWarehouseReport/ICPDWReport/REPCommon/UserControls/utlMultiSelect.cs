using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class utlMultiSelect : UserControl
    {
        public utlMultiSelect()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.DoSearching != null)
            {
                if (textBox1.Tag == null || string.IsNullOrEmpty(textBox1.Tag.ToString()))
                {
                    this.DoSearching(this, new SearchEventArgs(null));
                }
                else { this.DoSearching("Name", new SearchEventArgs(this.textBox1.Tag)); }
            }
        }


        /// <summary>
        /// ËÑË÷
        /// </summary>
        public event EventHandler DoClearing;

        /// <summary>
        /// ²éÑ¯
        /// </summary>
        public event EventHandler<SearchEventArgs> DoSearching;



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || (e.KeyCode == Keys.Back && textBox1.Text.Trim().Length == 1))
            {
                this.textBox1.Tag = null;
                this.textBox1.Text = string.Empty;
                if (this.DoClearing != null)
                {
                    this.DoClearing(this,new EventArgs());
                }
            }
        }

        public string SelectedText
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }

        public object SelectedValue
        {
            get { return this.textBox1.Tag; }
            set { this.textBox1.Tag = value; }
        }

       
    }

    public class SearchEventArgs:EventArgs
    {
        public SearchEventArgs(object selectedValue)
        {
            _selectedValue = selectedValue;
        }

        object _selectedValue;

        public object SelectedValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value; }
        }

    }
}

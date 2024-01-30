using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class SelectShippingLineCtl : UserControl
    {
        public SelectShippingLineCtl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                if (Utility.IsEnglish)
                {
                    this.multipleSelect1.DisplayMember = "EName";
                }
                else
                {
                    this.multipleSelect1.DisplayMember = "CName";
                }
                this.multipleSelect1.ValueMember = "Id";
                this.multipleSelect1.SearchColumns = new string[] { "CName","Code","EName"};
            }
        }

        public DataSet DataSource
        {
            set
            {
                if (value != null)
                {
                    this.multipleSelect1.DataSource = value.Tables[0].DefaultView;
                }
            }
        }

        public override string Text
        {
            get { return this.multipleSelect1.Text; }
        }
        public string Value
        {
            get { return this.multipleSelect1.Value; }
        }
    }
}

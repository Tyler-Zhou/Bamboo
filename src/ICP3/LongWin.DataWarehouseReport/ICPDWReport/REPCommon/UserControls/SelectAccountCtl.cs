using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using LongWin.Framework.ClientComponents;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class SelectAccountCtl : UserControl
    {
        public SelectAccountCtl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.selectAccount.DisplayMember = "Name";
            this.selectAccount.ValueMember = "Id";
            this.selectAccount.SearchColumns = new string[] { "Name" };
        }

        public ConditionGetDataMethod GetDataMethod
        {
            set
            {
                this.selectAccount.GetDataMethod = value;
            }
        }

        public override string Text
        {
            get
            {
                return this.selectAccount.Text;
            }
        }

        public string Value
        {
            get
            {
                return this.selectAccount.Value;
            }
        }
    }
}

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
    public partial class SelectCostItemCtl : UserControl
    {

        private DataSet _dataSource;
        public DataSet DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
                if (value != null)
                {
                    this.treeTextBox1.TreeDataSource = this._dataSource.Tables[0];
                }
            }
        }

        public SelectCostItemCtl()
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
                    this.treeTextBox1.TreeDisplayMember = "EName";
                }
                else
                {
                    this.treeTextBox1.TreeDisplayMember = "CName";
                }


                this.treeTextBox1.TreeValueMember = "Id";
                this.treeTextBox1.TreeParentMember = "ParentID";
            }
        }

        public override string Text
        {
            get { return this.treeTextBox1.Description; }
        }
        public object Value
        {
            get { return this.treeTextBox1.Value; }
        }
    }
}

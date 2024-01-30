using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

using LongWin.Framework.ClientComponents;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class SelectCRMSalesCtl : UserControl
    {
        #region 属性

        //属性

        public event System.EventHandler DataFilterChanged;

        private string _DataFilter = "";
        /// <summary>
        /// 过滤当前的数据集
        /// </summary>
        public string DataFilter
        {
            get
            {
                return this._DataFilter;
            }
            set
            {
                if (this._DataFilter != value)
                {
                    this._DataFilter = value;
                    if (this.DataFilterChanged != null)
                    {
                        this.DataFilterChanged(this, new System.EventArgs());
                    }
                }
            }
        }

        public event System.EventHandler DataSourceChanged;

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
                    this.SalesSelect.DataSource = this._dataSource.Tables[0].DefaultView;
                    if (this.DataSourceChanged != null)
                    {
                        this.DataSourceChanged(this, new System.EventArgs());
                    }
                }
            }
        }
        #endregion

        public SelectCRMSalesCtl()
        {
            InitializeComponent();
             this.DataSourceChanged += new EventHandler(SelectSalesCtl_DataSourceChanged);
        }
       

        void SelectSalesCtl_DataSourceChanged(object sender, EventArgs e)
        {
            //当数据源为空或者只有一条记录时，Enabled为false;
            if (this.DataSource == null)
            {
                this.Enabled = false;
            }
            else if ((this.DataSource as DataSet).Tables[0].Rows.Count == 1)
            {
                this.SalesSelect.Enabled = false;
                this.SalesSelect.Value = (this.SalesSelect.DataSource as DataView).DataViewManager.DataSet.Tables[0].Rows[0]["UserID"].ToString();
            }
            else
            {
                this.Enabled = true;
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                this.SalesSelect.DisplayMember = "UserName";
                this.SalesSelect.ValueMember = "UserID";
                this.SalesSelect.SearchColumns = new string[] { "UserCode", "UserName" };
            }

            this.DataFilterChanged += new EventHandler(SelectSalesCtl_DataFilterChanged);
        }

        void SelectSalesCtl_DataFilterChanged(object sender, EventArgs e)
        {
            this._dataSource.Tables[0].DefaultView.RowFilter = this._DataFilter;
        }

        public override string Text
        {
            get { return this.SalesSelect.Text; }
        }
        public string Value
        {
            get { return this.SalesSelect.Value; }
        }
    }
}

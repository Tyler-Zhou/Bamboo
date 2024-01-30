using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

using LongWin.Framework.ClientComponents;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class SelectBussinessType : UserControl
    {
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

        public SelectBussinessType()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                this.multipleSelect1.DisplayMember ="Name" ;
                this.multipleSelect1.ValueMember = "Code"; 
                this.multipleSelect1.SearchColumns = new string[] { "Code", "Name" };

                DataTable dtConveyanceType = null;
                dtConveyanceType = new DataTable();
                dtConveyanceType.Columns.Add("code", typeof(string));
                dtConveyanceType.Columns.Add("name", typeof(string));

                //dtConveyanceType.Rows.Add(new object[] { "YWLX01", "整箱出口" });
                ////dtConveyanceType.Rows.Add(new object[] { "YWLX02", "整箱进口" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX03", "拼箱出口" });
                ////dtConveyanceType.Rows.Add(new object[] { "YWLX04", "拼箱进口" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX06", "空运出口" });
                ////dtConveyanceType.Rows.Add(new object[] { "YWLX07", "空运进口" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX05", "散杂货" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX08", "其他业务" });

                dtConveyanceType.Rows.Add(new object[] { "FCL EXPORT", "整箱出口" });
                dtConveyanceType.Rows.Add(new object[] { "LCL EXPORT", "拼箱出口" });
                dtConveyanceType.Rows.Add(new object[] { "AIR EXPORT", "空运出口" });
                dtConveyanceType.Rows.Add(new object[] { "BULK EXPORT", "散货出口" });

                dtConveyanceType.Rows.Add(new object[] { "FCL IMPORTS", "整箱进口" });
                dtConveyanceType.Rows.Add(new object[] { "LCL IMPORTS", "拼箱进口" });
                dtConveyanceType.Rows.Add(new object[] { "AIR IMPORTS", "空运进口" });
                dtConveyanceType.Rows.Add(new object[] { "BULK IMPORTS", "散货进口" });

                dtConveyanceType.Rows.Add(new object[] { "OTHER", "其他业务" });
                dtConveyanceType.Rows.Add(new object[] { "TRAILER", "集运业务" });
                dtConveyanceType.Rows.Add(new object[] { "APPLY TO CUSTOMS", "报关业务" });

                this.multipleSelect1.DataSource = dtConveyanceType.DefaultView;
            }
        }

        public string Value
        {
            get { return this.multipleSelect1.Value; }
        }
        public override string Text
        {
            get { return this.multipleSelect1.Text; }
        }

    }
}

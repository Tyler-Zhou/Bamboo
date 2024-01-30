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
        //����

        public event System.EventHandler DataFilterChanged;

        private string _DataFilter = "";
        /// <summary>
        /// ���˵�ǰ�����ݼ�
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

                //dtConveyanceType.Rows.Add(new object[] { "YWLX01", "�������" });
                ////dtConveyanceType.Rows.Add(new object[] { "YWLX02", "�������" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX03", "ƴ�����" });
                ////dtConveyanceType.Rows.Add(new object[] { "YWLX04", "ƴ�����" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX06", "���˳���" });
                ////dtConveyanceType.Rows.Add(new object[] { "YWLX07", "���˽���" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX05", "ɢ�ӻ�" });
                //dtConveyanceType.Rows.Add(new object[] { "YWLX08", "����ҵ��" });

                dtConveyanceType.Rows.Add(new object[] { "FCL EXPORT", "�������" });
                dtConveyanceType.Rows.Add(new object[] { "LCL EXPORT", "ƴ�����" });
                dtConveyanceType.Rows.Add(new object[] { "AIR EXPORT", "���˳���" });
                dtConveyanceType.Rows.Add(new object[] { "BULK EXPORT", "ɢ������" });

                dtConveyanceType.Rows.Add(new object[] { "FCL IMPORTS", "�������" });
                dtConveyanceType.Rows.Add(new object[] { "LCL IMPORTS", "ƴ�����" });
                dtConveyanceType.Rows.Add(new object[] { "AIR IMPORTS", "���˽���" });
                dtConveyanceType.Rows.Add(new object[] { "BULK IMPORTS", "ɢ������" });

                dtConveyanceType.Rows.Add(new object[] { "OTHER", "����ҵ��" });
                dtConveyanceType.Rows.Add(new object[] { "TRAILER", "����ҵ��" });
                dtConveyanceType.Rows.Add(new object[] { "APPLY TO CUSTOMS", "����ҵ��" });

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

using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml.Linq;
using DevExpress.XtraEditors.Controls;

namespace ICP.ReportCenter.UI.FCMReports
{
    [ToolboxItem(false)]
    public partial class OEBussinesInfoSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region  init

        public OEBussinesInfoSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
             
            }
        }

        public void InitControls()
        {
            Utility.BulidComboboxItem<ReportSalesType>(cmbGroupBy, 6);

            cmbGroupBy.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Customer" : "客户", "Customer"));
            cmbGroupBy.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Company" : "业务发生地", "Company"));
            cmbGroupBy.SelectedIndex = 0;

            //设置默认问当前月
            DateTime now = DateTime.Now;
            dteFrom.DateTime = now.AddMonths(-1);

        }


        #endregion

        #region 属性

        public string XMLCondition
        {
            get
            {
                XElement xeRoot = new XElement("Root");
                xeRoot.Add(new XElement("CustomerIDs", ObjCustomerID.TagToSplitString(",")));
                xeRoot.Add(new XElement("SalesIDs", this.txtSales.Tag == null ? string.Empty : txtSales.Tag.ToString()));

                if (dteFrom.DateTime.ToShortDateString().Equals(DateTime.Now.AddMonths(-1).ToShortDateString()))
                {
                    xeRoot.Add(new XElement("DateFrom", DateTime.Now.AddMonths(-1).ToShortDateString()));
                    xeRoot.Add(new XElement("DateTo", DateTime.Now.ToShortDateString()));
                }
                else
                {
                    xeRoot.Add(new XElement("DateFrom", dteFrom.DateTime.ToShortDateString()));
                    xeRoot.Add(new XElement("DateTo", DateTime.MaxValue.ToShortDateString()));
                }
                xeRoot.Add(new XElement("GroupBy", cmbGroupBy.EditValue.ToString()));

                return xeRoot.ToString();
            }
        }

        //客户ID
        public object ObjCustomerID
        {
            get
            {
                return this.txtCustomer.Tag;
            }
            set
            {
                this.txtCustomer.Tag = value;
            }
        }
        //客户名
        public string CustomerName 
        {
            get 
            {
                return this.txtCustomer.Text;
            }
            set 
            {
                this.txtCustomer.Text = value;
            }
        }

        //从..时候开始
        public DateTime FromDate
        {
            get
            {
                return dteFrom.DateTime;
            }
            set
            {
                dteFrom.DateTime = value;
            }
        }

        #endregion

        #region event
        public void btnSearch_Click(object sender, EventArgs e)
        {
            this.btnSearch.Enabled = false;
            try
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }

            }
            finally
            {
                this.btnSearch.Enabled = true;
              
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Condition", XMLCondition));

            ReportData rd = new ReportData { Parameters = paramList, ReportName = "RPT_GetOEHblListForOI" };
            return rd;
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion
    }
}

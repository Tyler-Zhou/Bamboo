using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;

namespace ICP.ReportCenter.UI.FinanceOEReports
{
    [ToolboxItem(false)]
    public partial class CommisionSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        #endregion

        #region  init

        public CommisionSearchPart()
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

        private void InitControls()
        {
            ReportCenterHelper.BulidReportTypeAndGroups(this.reportOperationTypePart1,null,false,false);

            cmbGroupBy.Properties.Items.Clear();
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Sales,Shipper"));
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Sales"));
            cmbGroupBy.SelectedIndex =0;

            Utility.BulidComboboxItem<ReportYesNoType2>(cmbWriteOff, 2);
            Utility.BulidComboboxItem<ReportYesNoType2>(cmbCostWriteOff, 2);
            Utility.BulidComboboxItem<ReportFeeType>(cmbFeeType, 1);

            rdoOperationOrgial.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
            rdoOperationDepartment.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
         
            rdoDetail.CheckedChanged += delegate
            {
                labGroupBy.Visible = cmbGroupBy.Visible = rdoDetail.Checked;
            };
        }

        void rdoDepartment_CheckedChanged(object sender, EventArgs e)
        {
            bool showDepartment = true;
            if (rdoOperationOrgial.Checked)
            {
                showDepartment = false;
            }
            this.treeBoxSalesDep.ShowDepartment = showDepartment;
            this.treeBoxSalesDep.AddCompanyItems();

        }

        #endregion

        #region 属性

        /// <summary>
        /// 描述信息
        /// </summary>
        public string JobPlace
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                if (this.rdoOperationDepartment.Checked)
                {
                    strBuilder.Append(rdoOperationDepartment.Text + "  : "); 
                }
                else if (this.rdoOperationOrgial .Checked)
                {
                    strBuilder.Append(rdoOperationOrgial.Text + "  : ");
                }
                strBuilder.Append(treeBoxSalesDep.EditText);
                return strBuilder.ToString();
            }
        }

        public bool IsTotal
        {
            get
            {
                return rdoTotal.Checked;
            }
        }

        #endregion

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
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
            DateTime from=DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString());
            DateTime to = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString());

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("StructType", rdoOperationOrgial.Checked ? "0" : "1"));
            paramList.Add(new ReportParameter("StructNodeId", treeBoxSalesDep.GetAllEditValue.ToSplitString(",")));
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("SalesSet", txtSales.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("ShipperSet", txtBillTo.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("JobType", this.reportOperationTypePart1.EditValue));
            paramList.Add(new ReportParameter("VerifyFlag", cmbCostWriteOff.SelectedIndex.ToString()));
            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("JobPlace", this.JobPlace));
            paramList.Add(new ReportParameter("Groupby", LocalData.IsEnglish ? "Sales" : "业务员"));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));

            paramList.Add(new ReportParameter("IsOnlyCommision", cmbFeeType.SelectedIndex.ToString()));
            paramList.Add(new ReportParameter("IsReceivedForLocal", cmbWriteOff.SelectedIndex.ToString()));

            //如果是统计表
            string reportName = string.Empty;
            if (this.IsTotal)
            {
                reportName = "RPT_Commision_Total";
            }
            //如果详细表
            else
            {
                reportName = "RPT_Commision_Detail";
                paramList.Add(new ReportParameter("IsSales", this.cmbGroupBy.SelectedIndex.ToString()));

            }

            ReportData rd = new ReportData { Parameters = paramList, ReportName = reportName };
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

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
    public partial class DcNoteForAgentSearchPart : ReportBaseSearchPart
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

        public DcNoteForAgentSearchPart()
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


            Utility.BulidComboboxItem<ReportYesNoType2>(cmbWriteOff, 2);
            Utility.BulidComboboxItem<ReportYesNoType4>(cmbAttachment, 0);
            Utility.BulidComboboxItem<ReportYesNoType3>(cmbPPState, 0);

            Utility.BulidComboboxItem<ReportCurrencyType>(cmbCurrencyType, 0);

            rdoOperationOrgial.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
            rdoOperationDepartment.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
           

            rdoDetail.CheckedChanged += delegate
            {
                labAttachment.Visible = cmbAttachment.Visible = rdoDetail.Checked;
                cmbCurrencyType.Enabled = rdoDetail.Checked;
                if (rdoDetail.Checked == false) cmbCurrencyType.SelectedIndex = 0;
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
            DateTime from=this.operationDatePart1.FromDate;
            DateTime to = this.operationDatePart1.ToDate;

            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("StructType", rdoOperationOrgial.Checked ? "0" : "1"));
            paramList.Add(new ReportParameter("StructNodeId", treeBoxSalesDep.GetAllEditValue.ToSplitString(",")));
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ShipperSet", txtBillTo.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("VerifyFlag", cmbWriteOff.SelectedIndex.ToString()));
            paramList.Add(new ReportParameter("JobType", this.reportOperationTypePart1.EditValue));

            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.CShortName));//默认公司的C文名字
            paramList.Add(new ReportParameter("CompanyEName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.CAddress));
            paramList.Add(new ReportParameter("CompanyEAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("IsDisplayPrePay", cmbPPState.SelectedIndex.ToString()));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));

            //如果是统计表
            string reportName = string.Empty;
            if (rdoTotal.Checked)
            {
                reportName = "RPT_DebitnoteForAgent_Total";
                paramList.Add(new ReportParameter("JobPlace", this.JobPlace));
                paramList.Add(new ReportParameter("ConditionRemark", (LocalData.IsEnglish ? "部门" : "Department") + "  : " + treeBoxSalesDep.EditText));
            }
            //如果详细表
            else
            {
                reportName = "RPT_DebitnoteForAgent_Detail";
                paramList.Add(new ReportParameter("SalesSet", txtSales.Tag.TagToSplitString(",")));
                paramList.Add(new ReportParameter("CurrencyType", cmbCurrencyType.SelectedIndex.ToString()));
                paramList.Add(new ReportParameter("DisplayAttach", this.cmbAttachment.SelectedIndex.ToString()));

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

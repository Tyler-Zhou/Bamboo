using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceOEReports
{
    [ToolboxItem(false)]
    public partial class DcNoteCRSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        IDisposable chargingCodeFinder;
        Guid _SolutionID
        {
            get
            {
                Guid id = Guid.Empty;
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(Utility.UserDefaultCompanyID);
                if (configureInfo != null)
                {
                    id = configureInfo.SolutionID;
                }
                return id;
            }
        }
        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", _SolutionID, false);
            return conditions;
        }

        #endregion

        #region  init

        public DcNoteCRSearchPart()
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

            chkcmbGroupBy.AddItem(Guid.NewGuid(), LocalData.IsEnglish ? "Shipper" : "往来单位");
            chkcmbGroupBy.AddItem(Guid.NewGuid(), LocalData.IsEnglish ? "Sales" : "业务员");
            chkcmbGroupBy.Items[0].CheckState = CheckState.Checked;
            chkcmbGroupBy.RefreshText();

            Utility.BulidComboboxItem<ReportYesNoType2>(cmbWriteOff, 2);

            rdoOperationOrgial.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
            rdoOperationDepartment.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);

            chargingCodeFinder = SearchBoxAdapter.RegisterChargeCodeMultipleSearchBox(DataFindClientService, txtChargingCode, CommonFinderConstants.ChargingCodeFinder, GetSolutionChargingCodeSearchCondition);
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
            //if (reportOperationTypePart1.EditValue.IsNullOrEmpty())
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Operation type must choose one." : "请至少选择一种业务类型.");
            //    return null;
            //}

            if (chkcmbGroupBy.CheckCount ==0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "GroupBy must choose one." : "请至少选择一种分组方式.");
                return null;
            }

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("StructType", rdoOperationOrgial.Checked ? "0" : "1"));
            paramList.Add(new ReportParameter("StructNodeId", treeBoxSalesDep.GetAllEditValue.ToSplitString(",")));
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("SalesSet",txtSales.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("ShipperSet", txtBillTo.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("JobType", this.reportOperationTypePart1.EditValue));
            paramList.Add(new ReportParameter("VerifyFlag", cmbWriteOff.SelectedIndex.ToString()));
            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("JobPlace", this.JobPlace));
            paramList.Add(new ReportParameter("ConditionRemark", (LocalData.IsEnglish ? "部门" : "Department") + "  : " + treeBoxSalesDep.EditText));
            paramList.Add(new ReportParameter("AgentOfCarrier", this.txtAgentOfCarrier.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("Carrier", this.txtCarrier.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("LoadPortSet", this.stxtPOL.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("DiscPortSet", this.stxtPOD.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("ChargeCodeID", txtChargingCode.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("SCNO", this.txtContractNo.Text.Trim()));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));

            //如果是统计表
            string reportName = string.Empty;
            if (this.IsTotal)
            {
                reportName = "RPT_DebitnoteForCR_Total";
                paramList.Add(new ReportParameter("GroupFields", this.chkcmbGroupBy.EditText));
                paramList.Add(new ReportParameter("GroupCount", this.chkcmbGroupBy.CheckCount.ToString()));
            }
            //如果详细表
            else
            {
                reportName = "RPT_Debitnote_DetailCR";
                paramList.Add(new ReportParameter("OtherFields", this.chkcmbGroupBy.EditText));
                paramList.Add(new ReportParameter("ShippingLineSet", this.chkcmbShipLine.EditValue.ObjectsToSplitString(",")));
                paramList.Add(new ReportParameter("Type", "0"));
            }

            ReportData rd = new ReportData { Parameters = paramList ,ReportName =reportName };
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

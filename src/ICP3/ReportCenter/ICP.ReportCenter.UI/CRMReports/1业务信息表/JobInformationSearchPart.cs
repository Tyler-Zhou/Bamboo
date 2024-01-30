using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;

namespace ICP.ReportCenter.UI.CRMReports
{
    [ToolboxItem(false)]
    public partial class JobInformationSearchPart : ReportBaseSearchPart
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

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        #endregion

        #region  init

        private string ChargeViewBusiness = "CHARGEVIEWBUSINESS";
        bool isCharge = false;

        public JobInformationSearchPart()
        {
            InitializeComponent();
            Disposed += delegate {
                OnSearched = null;
                if (salesFinder != null)
                {
                    salesFinder.Dispose();
                    salesFinder = null;
                }
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
            if (!LocalData.IsDesignMode)
            {
                if (LocalCommonServices.PermissionService.HaveActionPermission(ChargeViewBusiness))
                {
                    isCharge = true;
                }
                InitControls();
               
            }
        }

        private void InitControls()
        {
            ReportCenterHelper.BulidReportTypeAndGroups(reportOperationTypePart1, null, false, false);

            List<TreeCheckControlSource> tss = new List<TreeCheckControlSource>();

            //if (isCharge)
            //{
            //    //计费--所有的部门信息
            //    foreach (var item in ReportCenterHelper.GetOrganizationList)
            //    {
            //        tss.Add(new TreeCheckControlSource { ID = item.ID, ParentID = item.ParentID, Name = LocalData.IsEnglish ? item.EShortName : item.CShortName });
            //    }

            //}
            //else
            //{



                //业务员--只能查询当前用户的部门结构
            foreach (var item in ReportCenterHelper.CrmOrganizationList)
                {
                    tss.Add(new TreeCheckControlSource { ID = item.ID, ParentID = item.ParentID, Name = LocalData.IsEnglish ? item.EShortName : item.CShortName });
                }
            //}




            treeBoxSalesDep.SetSource(tss);
            treeBoxSalesDep.EditValue = new List<Guid>() { Utility.UserDefaultDepartmentID };
            treeBoxSalesDep.EditText = Utility.UserDefaultDepartmentName;

            cmbSearchType.Properties.Items.Add(new ImageComboBoxItem("--请选择--"));
            cmbSearchType.Properties.Items.Add(new ImageComboBoxItem("业务号"));
            cmbSearchType.Properties.Items.Add(new ImageComboBoxItem("提单号"));
            cmbSearchType.Properties.Items.Add(new ImageComboBoxItem("订舱号"));
            cmbSearchType.Properties.Items.Add(new ImageComboBoxItem("合约号"));
            cmbSearchType.SelectedIndex = 0;
            cmbSearchType.SelectedIndexChanged += delegate
            {
                if (cmbSearchType.SelectedIndex == 0) txtSearchText.Enabled = true;
                else txtSearchText.Enabled = true;
            };

            if (isCharge)
            {
                //计费浏览
              salesFinder=  SearchBoxAdapter.RegisterMultipleSearchBox(DataFindClientService, txtSales, SystemFinderConstants.UserFinder);
            }
            else
            {
                //业务员
                Utility.BindUserList(Workitem, txtSales);
            }
        }

        private IDisposable salesFinder;

        #endregion

        #region 属性

        #region 是否盈利
        /// <summary>
        /// 是否盈利０－全部；１－盈利；２－亏损
        /// </summary>
        public int ProfitFlag
        {
            get
            {
                if (rdoAll.Checked) return 0;
                else if (rdoProfit.Checked) return 1;
                return 2;
            }
        }
        #endregion

        #endregion

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
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
                btnSearch.Enabled = true;
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            DateTime from = operationDatePart1.FromDate;
            DateTime to = operationDatePart1.ToDate;

            List<ReportParameter> paramList = new List<ReportParameter>
            {
                new ReportParameter("StructNodeId", treeBoxSalesDep.GetAllEditValue.ToSplitString(",")),
                new ReportParameter("Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")),
                new ReportParameter("Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")),
                new ReportParameter("ProfitType", ProfitFlag.ToString()),
                new ReportParameter("ConsignerSet", txtCustomer.Tag.TagToSplitString(",")),
                new ReportParameter("SalesSet", txtSales.Tag.TagToSplitString(",")),
                new ReportParameter("SeekField",
                    cmbSearchType.SelectedIndex == 0 ? string.Empty : txtSearchText.Text.Trim()),
                new ReportParameter("SeekValue",
                    cmbSearchType.SelectedIndex == 0 ? string.Empty : cmbSearchType.Text.Trim()),
                new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"),
                new ReportParameter("JobType", reportOperationTypePart1.EditValue),
                new ReportParameter("DateType", "0")
            };

            bool canViewShipper = LocalCommonServices.PermissionService.HaveActionPermission(ActionConstants.DWJobInformation_ViewShipper);
            paramList.Add(new ReportParameter("IsDepartment", canViewShipper.ToString()));
            ReportData rd = new ReportData { Parameters = paramList, ServiceReportPath = ReportPathConstants.CRMReport, ReportName = "RPT_ALLJobInformation_Detail" };
            return rd;
        }
        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceOIReports
{
    [ToolboxItem(false)]
    public partial class LocalStatementSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICP.FAM.ServiceInterface.IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<ICP.FAM.ServiceInterface.IFinanceReportService>();
            }
        }


        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        #endregion

        #region  init

        public LocalStatementSearchPart()
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

            #region 初始日期
            dtePeriodFrom.DateTime = DateTime.Now.AddMonths(-1).Date;
            dtePeriodTo.DateTime = Utility.GetEndDate(DateTime.Now);
            dteETAFrom.DateTime = dteETDFrom.DateTime = DateTime.Now.Date;
            dteETATo.DateTime = dteETDTo.DateTime = Utility.GetEndDate(DateTime.Now);
            chkETA.CheckedChanged += delegate
            {
                dteETATo.Enabled = dteETAFrom.Enabled = chkETA.Checked;
            };

            chkETD.CheckedChanged += delegate
            {
                dteETDTo.Enabled = dteETDFrom.Enabled = chkETD.Checked;
            };

            #endregion
        }


        #endregion

        #region 属性

        /// <summary>
        /// BillTypeName
        /// </summary>
        string BillTypeName
        {
            get
            {
                if (chkLocalInvoice.Checked && chkAccountsPayable.Checked) return string.Empty;
                else if (chkAccountsPayable.Checked) return chkAccountsPayable.Text;
                else if (chkLocalInvoice.Checked) return chkLocalInvoice.Text;

                return string.Empty;
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
             Guid? customerID = null;
            #region 验证必输项
             if (chkcmbCompany.EditValue == null || chkcmbCompany.EditValue.Count == 0)
             {
                 MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company type must choose one." : "请至少选择一个公司.");
                 return null;
             }

             if (txtCustomer.Tag != null) customerID = new Guid(txtCustomer.Tag.ToString());
             //if (customerID.IsNullOrEmpty())
             //{
             //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "CustomerName cannot be Spatial value." : "CustomerName cannot be Spatial value.");
             //    return null;
             //}

            #endregion

            #region Bulid SearchParameter

            List<Guid> companyIDs = this.chkcmbCompany.CompanyIDs;
            DateTime? etaFrom = null, etaTo = null, etdFrom = null, etdTo = null;
            LocalStatementOrderByEnum orderBy = rdoBillDate.Checked ? LocalStatementOrderByEnum.BillDate : LocalStatementOrderByEnum.DueDate;
            StatementBillStateEnum statementBillState= StatementBillStateEnum.All;
            if(rdoOpen.Checked) statementBillState = StatementBillStateEnum.Open;
            else if(rdoOpenWhitInvoice.Checked) statementBillState = StatementBillStateEnum.Paid;

            CheckType checkType= CheckType.None;
            if(chkLocalInvoice.Checked && chkAccountsPayable .Checked)checkType= CheckType.None;
            else if(chkAccountsPayable.Checked)checkType = CheckType.AP;
            else if (chkLocalInvoice.Checked) checkType = CheckType.AR;

            if(rdoOpen.Checked) statementBillState = StatementBillStateEnum.Open;
            else if(rdoOpenWhitInvoice.Checked) statementBillState = StatementBillStateEnum.Paid;

            if(chkETA.Checked)
            {
                etaFrom= DateTime.Parse(dteETAFrom.DateTime.ToShortDateString());
                etaTo= DateTime.Parse(dteETATo.DateTime.ToShortDateString());
            }
            if(chkETD.Checked)
            {
                etdFrom= DateTime.Parse(dteETDFrom.DateTime.ToShortDateString());
                etdTo= DateTime.Parse(dteETDTo.DateTime.ToShortDateString());
            }

            #endregion
            try
            {
                #region

                List<LocalStatementReportData> data = FinanceReportService.GetLocalStatementReportData
                        (customerID
                        , DateTime.Parse(dtePeriodFrom.DateTime.ToShortDateString())
                        , DateTime.Parse(dtePeriodTo.DateTime.ToShortDateString())
                        , orderBy
                        , checkType
                        , statementBillState
                        , companyIDs.ToArray()
                        , etaFrom, etaTo
                        , etdFrom, etdTo);

                if (data == null || data.Count == 0)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Sorry, Could not find relevant data ，Please re-importation of inquiries conditions !" : "对不起，找不到相关数据，请重新输入查询条件!");
                    //MessageBoxService.ShowInfo("Sorry, Could not find relevant data ，Please re-importation of inquiries conditions !", "Suggest");
                    return null;
                }

                //主报表的数据源
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("AGTLocalStatement_LocalDebit", data));

                List<LocalStatementTotalReportData> totallist = BulidTotalList(data);//汇总数据 ,用于子报表
                //子报表的数据源
                List<ReportDataSource> sbds = new List<ReportDataSource>();
                sbds.Add(new ReportDataSource("LocalStatementTotalReportData", totallist));


                //LocalStatementDataTable.TableName = "AGTLocalStatement_LocalDebit";
                List<ReportParameter> paramList = new List<ReportParameter>();

                #region 报表参数1
                CustomerInfo userCompany = ReportCenterHelper.UserCompanyInfo;
                if (companyIDs.Count == 1)
                {
                    userCompany = ReportCenterHelper.GetCompanyInfo(companyIDs[0]);
                }
                
                paramList.Add(new ReportParameter("BeginTime", dtePeriodFrom.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("EndTime", dtePeriodTo.DateTime.ToShortDateString()));
                paramList.Add(new ReportParameter("CompanyName", userCompany.EName));
                paramList.Add(new ReportParameter("UserAddress", userCompany.EAddress));
                paramList.Add(new ReportParameter("UserTel", userCompany.Tel1));
                paramList.Add(new ReportParameter("UserFax", userCompany.Fax));
                paramList.Add(new ReportParameter("ReportTypeName", this.BillTypeName));
                paramList.Add(new ReportParameter("DataType", orderBy == LocalStatementOrderByEnum.BillDate ? "By InvioceDate(SortByInvoiceNO)" : "By Due Date"));
                string openAll = "All";
                if (statementBillState == StatementBillStateEnum.Open) openAll = "Open";
                else if (statementBillState == StatementBillStateEnum.Paid) openAll = "Invoice";
                paramList.Add(new ReportParameter("OpenorAll", openAll));
                paramList.Add(new ReportParameter("IsAttached", chkAttached.Checked.ToString()));
                paramList.Add(new ReportParameter("CurrentUser", LocalData.UserInfo.LoginName));
                paramList.Add(new ReportParameter("AmountCurrency", string.Empty));

                if (totallist != null && totallist.Count > 0) paramList.Add(new ReportParameter("IsHideTotalReport", "False"));
                else paramList.Add(new ReportParameter("IsHideTotalReport", "True"));

                #endregion

                bool IsHideAttachReport = true;//是否隐藏子报表
                bool IsHideAttachReportForRelax = true;//是否隐藏子报表

                #region 子报表

                if (chkAttached.Checked == false)
                {
                    paramList.Add(new ReportParameter("IsHideAttachReport", "True"));
                }
                else
                {

                    List<LocalStatementReportDetailData> detailData = FinanceReportService.GetLocalStatementReportDetailData
                       (customerID
                       , DateTime.Parse(dtePeriodFrom.DateTime.ToShortDateString())
                       , DateTime.Parse(dtePeriodTo.DateTime.ToShortDateString())
                       , orderBy
                       , checkType
                       , statementBillState
                       , companyIDs.ToArray()
                       , etaFrom, etaTo
                       , etdFrom, etdTo);


                    //sbds.Add(new ReportDataSource("AGTLocalStatement_LocalDebitDetail", LocalStatementDetailDataTableAttached));
                    //sbds.Add(new ReportDataSource("AGTLocalStatement_LocalDebitDetailForRelax", LocalStatementDetailDataTableAttached));

                    if (detailData != null && detailData.Count > 0)
                    {
                        bool isRelax = ReportCenterHelper.LocalStatementIsRelax;

                        if (isRelax == false)
                        {
                            IsHideAttachReport = false;
                            sbds.Add(new ReportDataSource("AGTLocalStatement_LocalDebitDetail", detailData));
                        }
                        else
                        {
                            IsHideAttachReportForRelax = false;
                            sbds.Add(new ReportDataSource("AGTLocalStatement_LocalDebitDetailForRelax", detailData));
                        }
                    }
                }

                #endregion

                paramList.Add(new ReportParameter("IsHideAttachReport", IsHideAttachReport.ToString()));
                paramList.Add(new ReportParameter("IsHideAttachReportForRelax", IsHideAttachReportForRelax.ToString()));
     
                ReportData rd = new ReportData();

                if (customerID!=null&&customerID!=Guid.Empty)
                {
                    rd.CustomerID = customerID;
                }
                rd.IsLocalReport = true;
                rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RptLocalStatementReport.rdlc";
                rd.Parameters = paramList;
                rd.DataSource = ds;
                rd.SubDataSource = sbds;
                return rd;

                #endregion
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 分币种汇总
        /// </summary>
        private List<LocalStatementTotalReportData> BulidTotalList(List<LocalStatementReportData> data)
        {
             List<LocalStatementTotalReportData> totallist = new List<LocalStatementTotalReportData>();
            foreach (LocalStatementReportData row in data)
            {
                if (row.Balance != 0)
                {
                    LocalStatementTotalReportData total = new LocalStatementTotalReportData();
                    double day = (DateTime.Now.Date - row.InvoiceDate.Date).TotalDays;
                    total.CurrencyName = row.Currency;
                    total.Less30 = 0m;
                    total.Over30 = 0m;
                    total.Over45 = 0m;
                    total.Over60 = 0m;
                    total.Over90 = 0m;
                    if (day <= 30)
                    {
                        total.Less30 = row.Balance;
                    }
                    else if (day >= 31 && day <= 45)
                    {
                        total.Over30 = row.Balance;
                    }
                    else if (day >= 46 && day <= 60)
                    {
                        total.Over45 = row.Balance;
                    }
                    else if (day >= 61 && day <= 90)
                    {
                        total.Over60 = row.Balance;
                    }
                    else if (day > 90)
                    {
                        total.Over90 = row.Balance;
                    }
                    totallist.Add(total);
                }
            }

            return totallist;

        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion
    }

    /// <summary>
    /// 用于LocalStatementReport后面汇总
    /// </summary>
    [Serializable]
    public class LocalStatementTotalReportData
    {
        public string CurrencyName { get; set; }

        public decimal Less30 { get; set; }

        public decimal Over60 { get; set; }

        public decimal Over30 { get; set; }

        public decimal Over45 { get; set; }

        public decimal Over90 { get; set; }
    }
}

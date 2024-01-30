using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceOIReports
{
    [ToolboxItem(false)]
    public partial class AgentStatementSearchPart : ReportBaseSearchPart
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


        #endregion

        public AgentStatementSearchPart()
        {
            InitializeComponent();
            this.chkcmbOperaionType.CheckedTypes = new List<OperationType> { OperationType.OceanImport };
            this.Disposed += delegate
            {
                this.OnSearched = null;
                this._CompanyInfo = null;
                this._SearchParameter = null;

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
            dteFrom.DateTime = DateTime.Now.Date.AddMonths(-1);
            dteTo.DateTime = Utility.GetEndDate(DateTime.Now);
            this.radioGroup1.SelectedIndex = 3;

        }







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
        CompanyInfo _CompanyInfo;
        SearchParameter _SearchParameter;
        public override object GetData()
        {
            #region 验证必输项
            if (chkcmbCompany.EditValue == null || chkcmbCompany.EditValue.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company type must choose one." : "请至少选择一个公司.");
                return null;
            }
            if (chkcmbOperaionType.EditValue == null || chkcmbOperaionType.EditValue.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "OperaionType type must choose one." : "请至少选择一种业务类型.");
                return null;
            }

            #endregion

            #region Bulid SearchParameter



            _SearchParameter = new SearchParameter();
            _SearchParameter.UserCompanyID = LocalData.UserInfo.DefaultCompanyID;
            if (txtCustomer.Tag != null && Utility.IsGuid(txtCustomer.Tag.ToString()))
            {
                _SearchParameter.CustomerID = new Guid(txtCustomer.Tag.ToString());
            }

            _SearchParameter.AgentCompanyIDs = this.chkcmbCompany.CompanyIDs.ToArray();
            List<string> operactionTypeList = new List<string>();

            foreach (var obj in this.chkcmbOperaionType.SelectedOperationTypes)
            {
                operactionTypeList.Add(obj.GetHashCode().ToString());
            }

            _SearchParameter.OperactioType = operactionTypeList.ToArray().Join();
            if (this.rdoBillDate.Checked)
            {
                _SearchParameter.DateType = 0;
            }
            if (this.rdoETD.Checked)
            {
                _SearchParameter.DateType = 1;
            }
            if (this.cmbCurrency.EditValue != null)
            {
                _SearchParameter.CurrencyID = this.cmbCurrency.SelectedCurrencyId;
            }
            _SearchParameter.FromDate = DateTime.Parse(this.dteFrom.DateTime.ToShortDateString());
            _SearchParameter.ToDate = DateTime.Parse(this.dteTo.DateTime.ToShortDateString());

            _SearchParameter.BillType = (short)this.radioGroup2.SelectedIndex;
            _SearchParameter.IsShowPaidStatus = this.chkShowPaidStatus.Checked;
            _SearchParameter.IsAttached = this.chkAttached.Checked;
            if (radioGroup1.SelectedIndex == 0)
            {
                _SearchParameter.OrderByName = AgentStatementSortByEnum.ETD;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                _SearchParameter.OrderByName = AgentStatementSortByEnum.OurRefNo;
            }
            else if (radioGroup1.SelectedIndex == 2)
            {
                _SearchParameter.OrderByName = AgentStatementSortByEnum.MBLNo;
                _SearchParameter.IsOrderByMBLNo = true;
            }
            else if (radioGroup1.SelectedIndex == 3)
            {
                _SearchParameter.OrderByName = AgentStatementSortByEnum.InvoiceDate;
            }
            else if (radioGroup1.SelectedIndex == 4)
            {
                _SearchParameter.OrderByName = AgentStatementSortByEnum.RefNo;
            }




            ICP.FAM.ServiceInterface.DataObjects.AgentStatementReportDateTotal agentStatementTotal = FinanceReportService.GetAgentStatementReportDate(_SearchParameter.UserCompanyID,
                                                          _SearchParameter.CustomerID,
                                                          _SearchParameter.AgentCompanyIDs,
                                                          _SearchParameter.OperactioType,
                                                          _SearchParameter.DateType,
                                                          _SearchParameter.CurrencyID,
                                                          _SearchParameter.FromDate,
                                                          _SearchParameter.ToDate,
                                                          _SearchParameter.OrderByName,
                                                          _SearchParameter.BillType,
                                                          _SearchParameter.IsShowPaidStatus,
                                                          _SearchParameter.IsAttached);



            #endregion

            _CompanyInfo = agentStatementTotal.CompanyInfo;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("ETD_Beginning_Date", this._SearchParameter.FromDate.Value.ToShortDateString()));
            paramList.Add(new ReportParameter("ETD_Ending_Date", this._SearchParameter.ToDate.Value.ToShortDateString()));
            paramList.Add(new ReportParameter("CompanyName", LocalData.UserInfo.DefaultCompanyName));
            paramList.Add(new ReportParameter("DepartMentType", this.chkcmbOperaionType.EditText));
            paramList.Add(new ReportParameter("OpenAll", radioGroup2.SelectedIndex.ToString()));
            paramList.Add(new ReportParameter("UserAddress", agentStatementTotal.CompanyInfo.CompanyAddress));
            paramList.Add(new ReportParameter("UserTel", agentStatementTotal.CompanyInfo.CompanyTel));
            paramList.Add(new ReportParameter("UserFax", agentStatementTotal.CompanyInfo.CompanyFax));
            paramList.Add(new ReportParameter("IsPaid", _SearchParameter.IsShowPaidStatus.ToString()));
            paramList.Add(new ReportParameter("IsAttached", _SearchParameter.IsAttached.ToString()));
            paramList.Add(new ReportParameter("SortByMBlNo", this._SearchParameter.IsOrderByMBLNo.ToString()));

            ReportData rd = new ReportData();

            if (_SearchParameter.CustomerID!=null&&_SearchParameter.CustomerID!=Guid.Empty)
            {
                rd.CustomerID = _SearchParameter.CustomerID;
            }
            rd.IsLocalReport = true;

            if (chkShowPaidStatus.Checked == true)
            {
                if (radioGroup3.SelectedIndex == 1)
                {
                    agentStatementTotal.MasterDataList = agentStatementTotal.MasterDataList.FindAll(ff => ff.IsPaid == true);
                }
                else if(radioGroup3.SelectedIndex == 2)
                {
                    List<AgentStatementReportDate> MasterDataListtemp = new List<AgentStatementReportDate>();
                    MasterDataListtemp =  agentStatementTotal.MasterDataList.FindAll(ff => ff.IsPaid == false);
                    agentStatementTotal.MasterDataList = MasterDataListtemp; 
                }

                
            }

            rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.AgentStatementReport.rdlc";
            rd.Parameters = paramList;
            List<ReportDataSource> ds = new List<ReportDataSource>();
            ds.Add(new ReportDataSource("AGTAgentStateMent_REPDebit", agentStatementTotal.MasterDataList));
            rd.DataSource = ds;

            if (this.chkAttached.Checked)
            {
                List<ReportDataSource> subds = new List<ReportDataSource>();

                subds.Add(new ReportDataSource("AGTAgentStateMent_REPDebitDetail", agentStatementTotal.MasterAndDetailDataList));
                rd.SubDataSource = subds;
            }


            return rd;
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 钻取明细
        /// </summary>
        /// <param name="reportEmbeddedResource"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            try
            {
                Guid billID = new Guid(parameters[4].Values[0].ToString());


                List<AgentStatementReportDetailDate> list = FinanceReportService.GetAgentStatementReportDetailDate(billID);

                if (list == null || list.Count == 0)
                {
                    return null;
                }

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("CompanyName", LocalData.UserInfo.DefaultCompanyName));
                paramList.Add(new ReportParameter("UserAddress", _CompanyInfo.CompanyAddress));
                paramList.Add(new ReportParameter("UserTel", _CompanyInfo.CompanyTel));
                paramList.Add(new ReportParameter("UserFax", _CompanyInfo.CompanyFax));
                paramList.Add(new ReportParameter("BillToID", list[0].BillToID.ToString()));
                paramList.Add(new ReportParameter("BillID", list[0].BillID.ToString()));

                ReportData rd = new ReportData();
                List<ReportDataSource> ds = new List<ReportDataSource>();
                rd.Parameters = paramList;
                ds.Add(new ReportDataSource("AGTAgentStateMent_REPDebitDetail", list));
                rd.DataSource = ds;
                return rd;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得OperAllText
        /// </summary>
        private string GetOperAllText
        {
            get
            {
                string str = string.Empty;

                if (this.radioGroup2.SelectedIndex == 0)
                {
                    str = "All";
                }
                else if (this.radioGroup2.SelectedIndex == 1)
                {
                    str = "Open";
                }
                else if (this.radioGroup2.SelectedIndex == 2)
                {
                    str = "Paid";
                }
                return str;
            }
        }
        #endregion

        class SearchParameter
        {
            public Guid UserCompanyID { get; set; }
            public Guid? CustomerID { get; set; }
            public Guid[] AgentCompanyIDs { get; set; }
            public string OperactioType { get; set; }
            public Int16 DateType { get; set; }
            public Guid? CurrencyID { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public AgentStatementSortByEnum OrderByName { get; set; }
            public Int16 BillType { get; set; }
            public bool IsShowPaidStatus { get; set; }
            public bool IsOrderByMBLNo { get; set; }
            public bool IsAttached { get; set; }

        }

        private void chkShowPaidStatus_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = chkShowPaidStatus.Checked;
        }
    }
}

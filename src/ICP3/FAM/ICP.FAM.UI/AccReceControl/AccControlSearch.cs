using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.UI.AccReceControl
{
    /// <summary>
    /// 应收账款控制查询
    /// </summary>
    public partial class AccControlSearch : BaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        /// <summary>
        /// 应收账款控制查询
        /// </summary>
        public AccControlSearch()
        {
            InitializeComponent();
            chkcmbCompany.SplitText = "&";
            Disposed += delegate
            {
                OnSearched = null;
                _SearchParameter = null;
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

            dteEndingDate.DateTime = FAMUtility.GetEndDate(DateTime.Now);
            dteFrom.DateTime = DateTime.Now.Date;
            dteTo.DateTime = FAMUtility.GetEndDate(DateTime.Now);

            var types = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            List<OperationType> list = new List<OperationType>();
            foreach (var item in types)
            {
                list.Add(item.Value);
            }
            chkcmbOperaionType.CheckedTypes = list;

            cmbPastDueDate.SetDataSource<DayRange>();
            cmbPastDueDate.EditValue = DayRange.All;
            cmbInsureOver.SetDataSource<DayRange>();
            cmbInsureOver.EditValue = DayRange.Day60;

            rdoGroupTerm.SelectedIndexChanged += rdoGroupTerm_SelectedIndexChanged;
        }
        /// <summary>
        /// 
        /// </summary>
        void rdoGroupTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoGroupTerm.SelectedIndex == 1)
            {
                rdoGroupInsure.Enabled = true;
            }
            else
            {
                rdoGroupInsure.Enabled = false;
            }
        }

        #region 属性


        /// <summary>
        /// 用 & 分割的报表类型
        /// </summary>
        public string BillTypesString
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                if (chkAR.Checked)
                {
                    strBuilder = strBuilder.Append("AR&");
                }

                if (chkAP.Checked)
                {
                    strBuilder = strBuilder.Append("AP&");
                }

                if (chkDRCR.Checked)
                {
                    strBuilder = strBuilder.Append("CRDR&");
                }

                return strBuilder.ToString();
            }
        }

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
        public SearchParameter _SearchParameter;
        public override object GetData()
        {
            #region 验证必输项
            if (chkcmbCompany.EditValue == null || chkcmbCompany.EditValue.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Company type must choose one." : "请至少选择一个公司.");
                return null;
            }
            if (chkcmbOperaionType.EditValue == null || chkcmbOperaionType.EditValue.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "OperaionType type must choose one." : "请至少选择一种业务类型.");
                return null;
            }

            List<BillType> billTypes = new List<BillType>();
            if (chkAP.Checked) billTypes.Add(BillType.AP);

            if (chkAR.Checked) billTypes.Add(BillType.AR);

            if (chkDRCR.Checked) billTypes.Add(BillType.DC);

            if (billTypes.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "BillType type must choose one." : "请至少选择一种帐单类型.");
                return null;
            }
            #endregion

            #region Bulid SearchParameter


            _SearchParameter = new SearchParameter
            {
                BeginDate = DateTime.Parse(dteFrom.DateTime.ToShortDateString()),
                EndDate = DateTime.Parse(dteTo.DateTime.ToShortDateString()),
                BillTypes = billTypes,
                CompanyIDs = chkcmbCompany.CompanyIDs,
                OperationTypes = chkcmbOperaionType.SelectedOperationTypes,
                Currency = cmbCurrency.SelectedCurrencyName
                  ,
                CustomerID = txtCustomer.Tag == null ? Guid.Empty : new Guid(txtCustomer.Tag.ToString())
                  ,
                EndingDate = dteEndingDate.DateTime
                  ,
                OnlyOverPaid = chkOverPaid.Checked
                  ,
                SearchType = rdoFinanceDate.Checked ? (short)0 : (short)1
            };

            #endregion

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("ClosingDate", dteEndingDate.DateTime.ToShortDateString()));
            paramList.Add(new ReportParameter("CompanyName", chkcmbCompany.EditText));
            paramList.Add(new ReportParameter("ReportType", BillTypesString));

            try
            {
                AgingDateState agingDateSate = (AgingDateState)rdoAgingDateState.SelectedIndex;
                TermType termtype = (TermType)rdoGroupTerm.SelectedIndex + 1;
                AccReceControlSearchParameter arcSearchParameter = new AccReceControlSearchParameter()
                {
                    CompanyIDs = chkcmbCompany.CompanyIDs.ToArray()
                    , CustomerID = txtCustomer.Tag == null ? Guid.Empty : new Guid(txtCustomer.Tag.ToString())
                    , BillTypes = billTypes.ToArray()
                    , OperationTypes = chkcmbOperaionType.SelectedOperationTypes.ToArray()
                    , TermType = (TermType)(rdoGroupTerm.SelectedIndex + 1)
                    , InsuredType = (InsuredType)(rdoGroupInsure.SelectedIndex + 1)
                    , SearchType = rdoFinanceDate.Checked ? (short)0 : (short)1
                    ,EndingDate = dteEndingDate.DateTime
                    ,AgingDateState = agingDateSate
                    ,PastDueRange = (DayRange)cmbPastDueDate.EditValue
                };
                List<CustomerAgingList> lists = FinanceService.GetCustomerAgingList(arcSearchParameter);

                return lists;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }
        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion
    }
}

using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI.BankReceiptList.Finder
{
    [ToolboxItem(false)]
    public partial class FinderSearchPart : BaseSearchPart
    {
        #region serivce

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 财务服务
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region Property
        /// <summary>
        /// 唯一号码
        /// </summary>
        public string UniqueNO { get { return txtBusinessNO.Text; } }
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        Guid CompanyID { get; set; }
        #endregion

        #region init
        public FinderSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                OnSearched = null;
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
            InitControls();
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void InitControls()
        {
            dmdDate.IsEngish = LocalData.IsEnglish;
            DateTime beginDate = dmdDate.From.IsNull() ? DateTime.Now : dmdDate.From.Value;
            DateTime endDate = dmdDate.To.IsNull() ? DateTime.Now : dmdDate.To.Value;
        }
        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            List<BankReceiptListInfo> list = null;
            BankReceiptSearchParameter searchParameter = new BankReceiptSearchParameter()
            {
                CompanyIDs = new Guid[] { CompanyID },
                Status = BankReceiptStatus.Verified,
                FromDate = dmdDate.From,
                ToDate = dmdDate.To,
            };
            list = FinanceService.GetBankReceiptList(searchParameter);
            if (list != null)
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 "
                                               + list.Count.ToString() + " 条数据.");
            return list;
        }

        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        public override void Init(IDictionary<string, object> values)
        {
            btnClear.PerformClick();
            foreach (var item in values)
            {
                switch (item.Key.ToUpper())
                {
                    case "INPUTVALUE":
                        txtBusinessNO.Text = "" + item.Value;
                        break;
                    case "COMPANYID":
                        CompanyID = new Guid("" + item.Value);
                        break;
                }
            }
        }

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void ClearControl()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }
        }

        #endregion
    }
}

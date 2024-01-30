using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;

using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.UI.MonthlyClosingEntry
{
    public partial class SearchPanel : BaseSearchPart
    {
        public SearchPanel()
        {
            InitializeComponent();

            DevHelper.FormatSpinEditForInteger(nudTotalRecords);
            Disposed += delegate {
                RemoveKeyDownHandle();
                OnSearched = null;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        #region 服务

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
       
      
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
                SetKeyDownToSearch();
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnClear.PerformClick();
            }
        }

        private void InitControls()
        {
            dmcTime.IsEngish = LocalData.IsEnglish;
            dteFrom.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
            dteTo.DateTime = DateTime.Today;

            FAMUtility.BindCheckComboBoxByCompany(chkcmbCompany);
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values != nudTotalRecords && values.Count > 0)
            {
                txtCustomerName.Text = string.Empty;
                txtApplicant.Text = string.Empty;

                foreach (var item in values)
                {
                    string value = item.Value == null ? string.Empty : item.Value.ToString();
                    switch (item.Key)
                    {
                        case "CustomerName":
                            txtCustomerName.Text = value;
                            break;
                        case "CreateByName":
                            txtApplicant.Text = value;
                            break;
                    }
                }
            }
        }


        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        #endregion

        public override event SearchResultHandler OnSearched;


        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null) OnSearched(this, GetData());
            }
        }
     
        public override object GetData()
        {
            try
            {
                MonthlyClosingEntrySearchParameter param = new MonthlyClosingEntrySearchParameter()
                {
                    CompanyIDs = CompanyIDs.ToArray(),
                    CustomerName = txtCustomerName.Text.Trim(),
                    ApplicantName = txtApplicant.Text.Trim(),
                    From = dmcTime.From,
                    To = dmcTime.To,
                    ProfitFrom = dteFrom.DateTime,
                    ProfitTo = dteTo.DateTime,
                    IsInsured = lwchkIsInsured.Checked,
                    TotalRecords = (int)nudTotalRecords.Value,
                };

                List<MonthlyClosingEntryList> list = FinanceService.GetMonthlyClosingEntryLists(param);

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            txtApplicant.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            nudTotalRecords.Value = 100;

        }
    }
}

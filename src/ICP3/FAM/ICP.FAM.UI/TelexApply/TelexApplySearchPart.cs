using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.UI.TelexApply
{
    [ToolboxItem(false)]
    public partial class TelexApplySearchPart : BaseSearchPart
    {
        #region 服务注入
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

        #region 初始化

        public TelexApplySearchPart()
        {
            InitializeComponent();


            DevHelper.FormatSpinEditForInteger(nudTotalRecords);

            Disposed += delegate {
                OnSearched = null;
                RemoveKeyDownHandle();
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
                SetKeyDownToSearch();
            }

        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
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
            dmdApplyDate.IsEngish = LocalData.IsEnglish;

            FAMUtility.BindCheckComboBoxByCompany(chkcmbCompany);
        }


        #endregion
    
        #region ISearchPart 成员

        public override event SearchResultHandler OnSearched;


        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            txtApplicant.Text = string.Empty;
            txtConsigneeName.Text = string.Empty;
            txtCustomerName.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "CustomerName":
                        txtCustomerName.Text = value;
                        break;
                    case "ConsigneeName":
                        txtConsigneeName.Text = value;
                        break;
                    case "CreateByName":
                        txtApplicant.Text = value;
                        break;
                }
            }
        }


        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

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

        public override object GetData()
        {
            try
            {

                List<TelexApplyList> list = FinanceService.GetTelexApplyList(
                    CompanyIDs.ToArray(),
                    txtCustomerName.Text.Trim(),
                    txtConsigneeName.Text.Trim(),
                    txtApplicant.Text.Trim(),
                    dmdApplyDate.From,
                    dmdApplyDate.To,
                    (int)nudTotalRecords.Value);

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }

        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null) OnSearched(this, GetData());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is TextEdit
                         && (item is SpinEdit) == false
                         && item.Enabled == true
                         && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }
        }

        private void rdoDate_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion
    }
}

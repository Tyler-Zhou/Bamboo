using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface;

namespace ICP.FAM.UI.VerificationSheet
{
    [ToolboxItem(false)]
    public partial class VerificationSheetSearchPart : BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IVerificationSheetService VerificationSheetService
        {
            get
            {
                return ServiceClient.GetService<IVerificationSheetService>();
            }
        }

        #endregion

        #region 初始化

        public VerificationSheetSearchPart()
        {
            InitializeComponent();
            DevHelper.FormatSpinEditForInteger(nudTotalRecords);

            Disposed += delegate {
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
                item.KeyDown -= item_KeyDown;
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
            //this.dmdApplyDate.IsEngish = LocalData.IsEnglish;
        }

        #endregion
    
        #region ISearchPart 成员

        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override object GetData()
        {
            try
            {
                bool? isFreightArrive = null;
                if ((bool)chkIsFreightArrive.EditValue)
                {
                    isFreightArrive = true;
                }

                List<Common.ServiceInterface.DataObjects.VerificationSheet> list = VerificationSheetService.GetVerificationSheetList(
                    txtOperationNo.Text.Trim(),
                    txtSheetNo.Text.Trim(),
                    txtCustomerName.Text.Trim(),
                    txtExpressNO.Text.Trim(),
                    //(bool)this.chkIsFreightArrive.EditValue == false? null: true, 
                    isFreightArrive,
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

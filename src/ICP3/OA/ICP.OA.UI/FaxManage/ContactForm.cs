using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraGrid.Views.Base;

namespace ICP.OA.UI.EmailManage
{
    public partial class ContactForm : DevExpress.XtraEditors.XtraForm
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }

        #endregion

        #region 本地属性

        MailAccount CurrentMailAccount
        {
            get { return bsMailAccount.Current as MailAccount; }
            set
            {
                MailAccount current = CurrentMailAccount;
                if (current != null) current = value;
            }
        }

        List<MailAccount> SelectedMailAccount
        {
            get 
            {
                GridCell[] selectedCells = gvMailList.GetSelectedCells();
                if (selectedCells.Length == 0) return null;

                List<int> rowIndex = new List<int>();
                foreach (var item in selectedCells)
                {
                    if(rowIndex.Contains(item.RowHandle) )continue;
                    else rowIndex.Add(item.RowHandle);
                }

                List<MailAccount> tagers = new List<MailAccount>();
                foreach (var item in rowIndex)
                {
                    MailAccount ma = gvMailList.GetRow(item) as MailAccount;
                    if(ma !=null) tagers.Add(ma);
                }

                return tagers; 
            
            }
        }

        List<MailAccount> TOs
        {
            get { return bsTo.DataSource as List<MailAccount>; }
            set
            {
                bsTo.DataSource = value;
                bsTo.ResetBindings(false);
            }
        }

        List<MailAccount> CCs
        {
            get { return bsCC.DataSource as List<MailAccount>; }
            set
            {
                bsCC.DataSource = value;
                bsCC.ResetBindings(false);
            }
        }

        public List<MailAccount> ResulsTo = null;
        public List<MailAccount> ResulsCC = null;

        #endregion

        #region 初始化

        public ContactForm()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.ResulsCC = null;
                this.ResulsTo = null;
                this.gcMain.DataSource = null;
                
                if (this.bsCC != null)
                {
                    this.bsCC.DataSource = null;
                    this.bsCC = null;
                }
                if (this.bsTo != null)
                {
                    this.bsTo.DataSource = null;
                    this.bsTo = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            this.Text = "添加联系人";
            colDescription.Caption="描述";
            colEMail.Caption="邮箱";
            colUserName.Caption = "用户名";
            btnFind.Text = "查找(&F)";
            btnOK.Text = "确定(&O)";
            btnCancel.Text = "取消(&C)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #endregion

        #region 接口

        public void SetSource(List<string> tos,List<string> ccs ) 
        {
            List<MailAccount> mailAccounts = UserService.GetMailAccountList(null, null, null, null, null, true, 0);
            this.bsMailAccount.DataSource = mailAccounts;

            List<MailAccount> toMails = mailAccounts.FindAll(delegate(MailAccount item) { return tos.Contains(item.EMail); });
            if (toMails == null) toMails = new List<MailAccount>();
            this.TOs = toMails;

            List<MailAccount> ccMails = mailAccounts.FindAll(delegate(MailAccount item) { return ccs.Contains(item.EMail); });
            if (ccMails == null) ccMails = new List<MailAccount>();
            this.CCs = ccMails;
        }

        #endregion

        #region  Close

        private void btnOK_Click(object sender, EventArgs e)
        {
            ResulsTo = TOs;
            ResulsCC = CCs;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Delete ListBoxItem

        ListBoxControl currentListBox = null;
        private void lbTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            currentListBox = sender as ListBoxControl;

            DeleteListBoxItem();

        }

        private void lbTo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                currentListBox = sender as ListBoxControl;
                popupMenu1.ShowPopup(MousePosition);
            }
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteListBoxItem();
        }

        private void DeleteListBoxItem()
        {
            if (currentListBox == null || currentListBox.SelectedItems == null || currentListBox.SelectedItems.Count == 0) return;

            List<Guid> needRemoveIDs = new List<Guid>();
            foreach (MailAccount item in currentListBox.SelectedItems)
            {
                needRemoveIDs.Add(item.ID);
            }

            if (currentListBox.Name == lbTo.Name)
                TOs.RemoveAll(delegate(MailAccount item) { return needRemoveIDs.Contains(item.ID); });
            else
                CCs.RemoveAll(delegate(MailAccount item) { return needRemoveIDs.Contains(item.ID); });
        }

        #endregion

        #region To

        private void btnTo_Click(object sender, EventArgs e)
        {
            List<MailAccount> currentMailAccounts = SelectedMailAccount;
            if (currentMailAccounts == null || currentMailAccounts.Count == 0) return;

            List<MailAccount> tos = TOs;
            foreach (var item in currentMailAccounts)
            {
                if (tos.Find(delegate(MailAccount ma) { return ma.ID == item.ID; }) != null) continue;
                tos.Add(item);
            }
            TOs = tos;
        }

        private void btnCC_Click(object sender, EventArgs e)
        {
            List<MailAccount> currentMailAccounts = SelectedMailAccount;
            if (currentMailAccounts == null || currentMailAccounts.Count == 0) return;

            List<MailAccount> ccs = CCs;
            foreach (var item in currentMailAccounts)
            {
                if (ccs.Find(delegate(MailAccount ma) { return ma.ID == item.ID; }) != null) continue;
                ccs.Add(item);
            }
            CCs = ccs;
        }

        #endregion

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text))
                gvMailList.ActiveFilterString = string.Empty;
            else
                gvMailList.ActiveFilterString = "[UserName] Like '%" + txtFind.Text + "%'";
        }

        private void ContactForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
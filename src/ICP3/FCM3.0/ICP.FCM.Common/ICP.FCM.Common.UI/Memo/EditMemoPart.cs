using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.UI.Memo
{
    public partial class EditMemoPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public EditMemoPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (LocalData.IsEnglish == false) SetCNText();
        }

        private void SetCNText()
        {
           
            btnCancel.Text = "取消(&C)";
            btnOK.Text = "确定(&O)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
            InitControls();
        }

        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<MemoType>> memoTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<MemoType>(LocalData.IsEnglish);
            foreach (var item in memoTypes)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.SelectedIndex = 0;
            chkIsShowAgent.Checked = chkIsShowCustomer.Checked = false ;
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            CommonMemoInfo info = data as CommonMemoInfo;
            //info.SendDate = DateTime.Now;
            //info.SenderID = LocalData.UserInfo.LoginID;
            //info.SenderName = LocalData.UserInfo.LoginName;
            bindingSource1.DataSource = data;
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Save())
                this.FindForm().Close();
        }

        bool Save()
        {
            try
            {
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }

        #endregion

        private void txtAttachmentName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
            
            }
        }
    }
}

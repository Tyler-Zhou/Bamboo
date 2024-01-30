using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.UI.Document
{
    public partial class EditDocumentPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public EditDocumentPart()
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
            //List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DocumentType>> documentTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DocumentType>(LocalData.IsEnglish);
            //foreach (var item in documentTypes)
            //{
            //    cmbDocumentType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            //}
            //cmbDocumentType.SelectedIndex = 0;

            //List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DocumentReleaseMode>> documentReleaseModes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DocumentReleaseMode>(LocalData.IsEnglish);
            //foreach (var item in documentReleaseModes)
            //{
            //    cmbReleaseMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            //}
            //cmbReleaseMode.SelectedIndex = 0;
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            CommonDocumentInfo info = data as CommonDocumentInfo;
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

        private void EditDocumentPart_Load(object sender, EventArgs e)
        {

        }
    }
}

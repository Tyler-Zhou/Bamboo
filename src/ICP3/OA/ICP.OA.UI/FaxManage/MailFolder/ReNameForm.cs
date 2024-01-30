using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;
using ICP.OA.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.OA.UI.FaxManage
{
    public partial class ReNameForm : DevExpress.XtraEditors.XtraForm
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IFaxClientService FaxClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFaxClientService>();
            }
        }

        #endregion

        public MessageFolderList CurrentFolder = null;

        List <MessageFolderList> currentList= null;

        public ReNameForm()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.currentList = null;
                this.CurrentFolder = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtName.KeyDown += delegate(object o, KeyEventArgs ke)
            { if (ke.KeyCode == Keys.Enter) this.btnOK.PerformClick(); };
        }

        private void SetCnText()
        {
            labName.Text = "名称";
            this.Text = "重命名文件夹";
        }

        #region btn

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CurrentFolder == null) return;

            if (this.ValidateData() == false) return;

            try
            {

                FaxClientService.SaveMessageFolder(CurrentFolder.ID
                                            , CurrentFolder.ParentID.Value
                                            , this.txtName.Text.Trim()
                                            ,CurrentFolder.Type,
                                            CurrentFolder.UpdateDate);
                CurrentFolder.Name = txtName.Text.Trim();

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
            }
        }

        bool ValidateData()
        {
            this.Validate();

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                dxErrorProvider1.SetError(this.txtName, LocalData.IsEnglish ? "Folder Name Must Input." : "文件夹名称不能为空.");
                return false;
            }

            MessageFolderList existsFolder = currentList.Find(delegate(MessageFolderList item)
            { return item.ID != CurrentFolder.ID &&(item.Name==txtName.Text); });
            if (existsFolder != null)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Exists same name Folder,sure continue?" : " 已存在同名文件夹,是否继续保存?"
                                    , LocalData.IsEnglish ? "Tip" : " 提示"
                                    , MessageBoxButtons.YesNo) == DialogResult.Yes)
                    return true;
                else
                    return false;

            }
            return true;
        }

     

        #endregion

        public void SetSource(List<MessageFolderList> listData, MessageFolderList data)
        {
            currentList = listData;
            CurrentFolder = data;
            txtName.Text =data.Name;
        }
    }
}
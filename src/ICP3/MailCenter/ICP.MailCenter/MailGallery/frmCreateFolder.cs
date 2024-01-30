using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 创建文件夹窗体类
    /// </summary>
    /// 
    [ToolboxItem(false)]
    [SmartPart]
    public partial class frmCreateFolder : Form
    {
        #region const and target
        String entryId = String.Empty;
        public bool SameFolderFlag = false;
        #endregion

        #region Init constructed function
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="folderName">新建文件夹所属的父文件夹ID</param>
        public frmCreateFolder(String folderEntryId)
            : this()
        {
            entryId = folderEntryId;
        }
        /// <summary>
        /// 构造函数 
        /// </summary>
        public frmCreateFolder()
        {
            InitializeComponent();

            InitControl();
        }

        #endregion

        #region Methods
        /// <summary>
        /// init controls language
        /// </summary>
        private void InitControl()
        {
            if (LocalData.IsEnglish)
            {
                this.Text = "Create a new folder";
                this.lblFolderName.Text = "Folder Name";
                this.btnOK.Text = "OK";
                this.btnCancel.Text = "Cancel";
            }
        }

        /// <summary>
        /// 文件夹名称
        /// </summary>
        public String NewFolderName
        {
            get
            {
                return this.txtFolderName.Text;
            }
            set
            {
                this.txtFolderName.Text = value;
            }

        }

       
        #endregion

        #region Click
        private void btnOK_Click(object sender, EventArgs e)
        {
            ValidateFolderName();
        }
        /// <summary>
        /// 验证文件夹名称是否相同
        /// </summary>
        private void ValidateFolderName()
        {

            if (String.IsNullOrEmpty(this.txtFolderName.Text))
            {
                this.txtFolderName.Focus();
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "The folder name can not be blank." : "文件夹名称不允许为空");
                return;
            }

            IsSameFolderNameExists();
        }

        private void IsSameFolderNameExists()
        {
            if (String.IsNullOrEmpty(entryId))
                return;
            try
            {
                MAPIFolder parentFolder = MailListPresenter.GetFolderByEntryID(entryId);
                if (parentFolder == null || parentFolder.Folders.Count == 0)
                {
                    return;
                }
                Int32 subFolderCount = parentFolder.Folders.Count;
                for (int i = 1; i <= subFolderCount; i++)
                {
                    if (parentFolder.Folders[i].Name.Equals(NewFolderName))
                    {
                        SameFolderFlag = true;

                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "This folder name already exists, please try other name." : "改文件夹名称已存在，请尝试另一名称.");
                        MailUtility.ReleaseComObject(parentFolder);
                        break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }

        }

        #endregion

        private void frmCreateFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}

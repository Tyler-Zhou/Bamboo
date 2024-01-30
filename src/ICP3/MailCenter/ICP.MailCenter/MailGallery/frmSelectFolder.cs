using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 选择文件夹界面类
    /// </summary>
    public partial class frmSelectFolder : Form
    {
        #region 属性
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }        

        public EmailFolderPart FolderPart
        {
            get
            {
                return RootWorkItem.SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }
        #endregion

        #region Init constructed function
        public frmSelectFolder(String folderName)
            : this()
        {
            this.Text = string.Format("{0}{1}", folderName, LocalData.IsEnglish ? "  -  Mail Center" : "  -  邮件中心");
        }

        public frmSelectFolder()
        {
            InitializeComponent();
            this.Disposed += delegate { DisposedComponent(); };
            InitControl();
        }

        private void DisposedComponent()
        {
            this.tvwFolder.Dispose();
            this.tvwFolder = null;
        }

        #endregion

        #region Method
        /// <summary>
        /// Init control language
        /// </summary>
        void InitControl()
        {
            if (LocalData.IsEnglish)
            {
                lblTip.Text = "Selected to move the folder to folder";
                btnOK.Text = "OK";
                btnCancel.Text = "Cancel";
                btnCreateFolder.Text = "New Folder";
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;
                //WS_EX_COMPOSITED. Prevents flickering.

                cp.ExStyle |= 0x00080000; //WS_EX_LAYERED. Transparency key
                return cp;

            }
        }

        /// <summary>
        /// get select node name
        /// </summary>
        public String FolderName
        {
            get
            {
                if (tvwFolder.SelectedNode == null)
                {
                    return String.Empty;
                }
                else
                {
                    return tvwFolder.SelectedNode.Text;
                }
            }
            set
            {
                tvwFolder.SelectedNode.Text = value;
            }
        }
        /// <summary>
        /// get current select folder
        /// </summary>
        public void GetCurrentFolder()
        {
            if (tvwFolder.SelectedNode != null)
            {
                object objEntryID = tvwFolder.SelectedNode.Tag;
                if (objEntryID == null) return;
                ClientProperties.Copy_Folder_EntryID = objEntryID.ToString();
            }
        }
        #endregion

        #region Click
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                FolderPart.LoadControls(tvwFolder, false);
            }
        }

        /// <summary>
        /// get and set current new folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //get current select folder
            GetCurrentFolder();
        }

        /// <summary>
        /// create folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            if (tvwFolder.SelectedNode == null || string.IsNullOrEmpty(tvwFolder.SelectedNode.Text))
            {
                MessageBox.Show((LocalData.IsEnglish ? "Please select node!" : "请选择文件夹!"));
                return;
            }

            frmCreateFolder newFolder = new frmCreateFolder(tvwFolder.SelectedNode.Tag.ToString());
            if (newFolder.ShowDialog() == DialogResult.Cancel)
                return;
            if (newFolder.SameFolderFlag)
                return;
            String newFolderName = newFolder.txtFolderName.Text;
            if (String.IsNullOrEmpty(newFolderName))
                return;
            //get selected folder
            GetCurrentFolder();
            MAPIFolder CurrentFolder = null; Folders folders = null;
            try
            {
                CurrentFolder = MailListPresenter.GetFolderByEntryID(ClientProperties.Copy_Folder_EntryID);
                if (CurrentFolder == null)
                    return;

                folders = CurrentFolder.Folders;

                folders.Add(newFolderName, Type.Missing);
                //refersh the Folders                
                RefershFolders(folders, newFolderName);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "add folder failure: " : "添加文件夹失败: ") + ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(CurrentFolder);
                MailUtility.ReleaseComObject(folders);
            }
        }

        private void RefershFolders(Folders olFolders, string newNodeName)
        {
            //刷新当前窗体文件夹列表
            TreeViewPresenter.AddTreeNode(tvwFolder.SelectedNode, olFolders, newNodeName, false);
            //刷新文件夹列表
            TreeNode findNode = null;
            TreeViewPresenter.FindTreeNode(FolderPart.trvFolder.Nodes, tvwFolder.SelectedNode.Text, ref findNode);
            if (findNode != null)
            {
                TreeViewPresenter.AddTreeNode(findNode, olFolders, newNodeName, true);
            }
        }
        #endregion

        private void tvwFolder_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            FolderPart.ExpandSubTreeNodes(tvwFolder, e.Node, false);
        }

        private void frmSelectFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}

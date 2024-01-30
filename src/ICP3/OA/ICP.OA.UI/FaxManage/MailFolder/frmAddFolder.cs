using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;
using ICP.OA.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace ICP.OA.UI.FaxManage
{
    public partial class frmAddFolder : DevExpress.XtraEditors.XtraForm
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

        MessageFolderList CurrentFolder
        {
            get { return bsFolder.Current as MessageFolderList; }
        }
        public MessageFolderList NewFolder
        {
            get;
            set;
        }
        private string folderDefaultName = LocalData.IsEnglish ? "New Folder" : "新建文件夹";
        #region 初始

        public frmAddFolder()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.treeFolder.DataSource = null;
                if (this.bsFolder != null)
                {
                    this.bsFolder.DataSource = null;
                    this.bsFolder = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!LocalData.IsEnglish && !LocalData.IsDesignMode) SetCnText();
        }

        private void SetCnText()
        {
            this.Text = "新建文件夹";
            labName.Text = "名称";
            colCName.Visible = true;
            btnOK.Text = "确定(&O)";
            btnCancel.Text = "取消(&C)";
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtName.Text = folderDefaultName;
            if (_initIndex >= 0)
            {
                TreeListNode tn = treeFolder.FindNodeByID(_initIndex);
                if(tn !=null)
                    treeFolder.FocusedNode = tn;
            }
            txtName.SelectAll();
        }

        #endregion

        #region btn

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            if (this.ValidateData() == false) return;

            MessageFolderList folder = new MessageFolderList();       
            folder.ParentID = CurrentFolder.ID;
            folder.Name = txtName.Text.Trim();
            folder.ParentName = CurrentFolder.Name;
            folder.Type = MessageFolderType.UserDefined;
            folder.UserID = LocalData.UserInfo.LoginID;
            folder.UserName = LocalData.UserInfo.LoginName;
            folder.ID=Guid.NewGuid();
            NewFolder = folder;
            FaxClientService.SaveMessageFolder(folder.ID,folder.ParentID.Value,folder.Name,folder.Type,null);
        }

        bool ValidateData()
        {
            this.Validate();

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                dxErrorProvider1.SetError(this.txtName, LocalData.IsEnglish ? "Folder Name Must Input." : "文件夹名称不能为空.");
                return false;
            }

            List<MessageFolderList> currentSource = this.bsFolder.DataSource as List<MessageFolderList>;

            MessageFolderList existsFolder= currentSource.Find(delegate(MessageFolderList item)
            { return item.ParentID == CurrentFolder.ID && (item.Name == txtName.Text); });
            if (existsFolder != null)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The folder name already exists,Continue or not?" : " 已存在同名文件夹,是否继续保存?"
                                    , LocalData.IsEnglish ? "Tip" : " 提示"
                                    , MessageBoxButtons.YesNo) == DialogResult.Yes)
                    return true;
                else
                    return false;
                 
            }
            return true;
        }

        #endregion

        
        public void SetSource(BindingSource bs)
        {
            List<MessageFolderList> data = bs.DataSource as List<MessageFolderList>;
            this.bsFolder.DataSource = data;
            treeFolder.ExpandAll();
            MessageFolderList currentFolder = bs.Current as MessageFolderList;
            int index = bs.IndexOf(currentFolder);
            SetDefaultFolderName(data, currentFolder, index);
        }
        int _initIndex = -1;

        private void SetDefaultFolderName(List<MessageFolderList> data,MessageFolderList currentFolder,int index)
        {
            if (currentFolder != null)
            {
                 _initIndex = index;

                List<MessageFolderList> currentFolderChilds = data.FindAll(delegate(MessageFolderList item) { return item.ParentID == currentFolder.ID; });
                if (currentFolderChilds == null || currentFolderChilds.Count == 0) return;

                List<MessageFolderList> currentFolderChildsNewFoldeName
                    = currentFolderChilds.FindAll(delegate(MessageFolderList item)
                    { return item.Name.Contains(folderDefaultName); });

                if (currentFolderChildsNewFoldeName != null && currentFolderChildsNewFoldeName.Count != 0)
                {
                    int indexNum = -1;
                    foreach (var item in currentFolderChildsNewFoldeName)
                    {
                        string nameNum = item.Name.Replace(folderDefaultName, string.Empty);
                        int num = -1;
                        if (string.IsNullOrEmpty(nameNum)) num = 0;
                        else int.TryParse(nameNum, out num);

                        if (num > indexNum) indexNum = num;
                    }
                    if (indexNum >= 0)
                    {
                        indexNum++;
                        folderDefaultName = folderDefaultName + indexNum.ToString();

                    }
                }
            }
        }

        private void treeFolder_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            MessageFolderList folder = (MessageFolderList)treeFolder.GetDataRecordByNode(e.Node);
            if (folder == null) return;
            e.NodeImageIndex = (short)folder.Type;
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) btnOK.PerformClick();
        }
    }
}

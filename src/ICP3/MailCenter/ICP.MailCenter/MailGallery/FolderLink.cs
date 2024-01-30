using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 文件夹邮件菜单
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class FolderLink : LinkLabel
    {
        #region target or const
        public ContextMenu contractMenu = null;
        public ContextMenu folderMenu = null;
        public String address = string.Empty;
        public object RecipientType;
        String _entryID = String.Empty;
        public ContextMenu fileMenu = null;
        #endregion

        #region Property
        private static Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public static Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = ServiceClient.GetClientService<WorkItem>().SmartParts;
                return _SmartParts;
            }
        }

        public EmailFolderPart FolderPart
        {
            get
            {
                return SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }
        public EmailListPart ListPart
        {
            get
            {
                return SmartParts.Get<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart);
            }
        }

        public outlook.Application app
        {
            get
            {
                return ClientUtility.OlApp;
            }
        }
        public outlook.NameSpace outlookNS
        {
            get
            {
                return app.GetNamespace("MAPI");
            }
        }
        public string _FolderName = string.Empty;
        public string _folderName
        {
            get
            {
                if (_FolderName == string.Empty && OlFolder != null)
                {
                    _FolderName = OlFolder.Name;
                }
                return _FolderName;
            }
            set
            {
                _FolderName = value;
            }
        }

        public int ItemCount
        {
            get
            {
                if (OlFolder == null)
                    return -1;
                else
                    return OlFolder.Items.Count;
            }
        }

        public MAPIFolder OlFolder
        {
            get
            {
                if (!string.IsNullOrEmpty(_entryID))
                    return MailListPresenter.GetFolderByEntryID(_entryID);
                else
                    return ClientUtility.OlNS.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
            }
        }

        public bool IsRootFolder
        {
            get
            {
                if (OlFolder == null)
                    return false;
                else
                    return ClientProperties.RootFolders.Contains(OlFolder.Name.ToLower());
            }
        }

        #endregion

        #region public click
        public FolderLink() { }
        public FolderLink(object tag, string text)
        {
            base.LinkBehavior = LinkBehavior.NeverUnderline;
            base.AutoSize = true;
            base.LinkColor = Color.Black;
            base.Tag = tag;
            base.Text = text;
        }
        #endregion

        #region Property
        /// <summary>
        /// 获取真实的邮件地址
        /// </summary>
        public String EmailAddress
        {
            get
            {
                String _address = String.Empty;
                if (!LocalData.IsDesignMode)
                {

                    if (Process.GetProcessesByName("OUTLOOK").Length == 0)
                    {
                        //MailUtility.StartProcess();
                        ClientUtility.GetOutlookNewNameSpace();
                        try
                        {
                            ListPart.RefershCtl();
                            MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                        }
                        catch { }
                        finally
                        {
                            MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                            _address = MailListPresenter.GetEmailAddress(address, this.RecipientType, MailListPresenter.GetMailRecipients());
                        }
                    }
                    else
                    {
                        Recipients olRecipients = null;
                        if (ClientProperties.CurrentMailItem == null)
                        {
                            _address = this.address;
                        }
                        else
                        {
                            try
                            {
                                if (ClientProperties.CurrentMailItem.Recipients.Count != 0)
                                {
                                    olRecipients = ClientProperties.CurrentMailItem.Recipients;
                                }
                            }
                            catch
                            {
                                olRecipients = MailListPresenter.GetMailRecipients();
                            }
                            _address = MailListPresenter.GetEmailAddress(address, this.RecipientType, olRecipients);
                        }
                    }
                }
                return _address;
            }
            set
            {
                this.address = value;
            }
        }
        #endregion

        #region Folder ContextMenu Link
        public ContextMenuStrip contextMenuStrip = null;
        /// <summary>
        /// init folder context menu
        /// </summary>
        /// <param name="folderPart"></param>
        /// <param name="entryID"></param>
        /// <param name="folderName"></param>
        public void FolderContextMenu(String entryID, String folderName)
        {
            InitializeComponent();

            _entryID = entryID;
            _folderName = string.Empty;


            InitFolderContextMenuStrip();
            DisableSystemFolderContextMenuStrip();

            this.AutoSize = true;
            DisposedFileEvent();
            this.MouseUp += new MouseEventHandler(File_MouseUp);
        }

        public void DisposedFileEvent()
        {
            this.MouseUp -= new MouseEventHandler(File_MouseUp);
        }
        /// <summary>
        /// fix folder name
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private String GetFolderName(String folderName)
        {
            int indexStart = -1, indexEnd = -1;
            if (folderName.Contains("(") && folderName.EndsWith(")"))
            {
                indexStart = folderName.LastIndexOf("(");
                indexEnd = folderName.LastIndexOf(")");
            }
            else if (folderName.Contains("[") && folderName.EndsWith("]"))
            {
                indexStart = folderName.LastIndexOf("[");
                indexEnd = folderName.LastIndexOf("]");
            }


            if (indexStart < 0 || indexEnd < 0)
                return folderName;
            String mailCountString = folderName.Substring(indexStart + 1, indexEnd - indexStart - 1);
            Int32 mailCount;
            if (!Int32.TryParse(mailCountString, out mailCount))
            {
                return folderName;
            }
            if (mailCount < 0)
            {
                return folderName;
            }
            return folderName.Substring(0, indexStart);
        }

        private void InitFolderContextMenuStrip()
        {
            String text = String.Empty;

            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(OnFolderSelected);
            //ToolMenuItemObject toolMenuItemObject = new ToolMenuItemObject();
            //toolMenuItemObject.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Unknown;
            //toolMenuItemObject.FormType = ICP.Framework.CommonLibrary.Common.FormType.Unknown;

            CreateMenuItem(LocalData.IsEnglish ? "New Folder" : "新建文件夹", OperationFileType.Create, 0);
            CreateMenuItem(LocalData.IsEnglish ? "Delete Folder" : "删除文件夹", OperationFileType.Delete, 3);
            MAPIFolder olFolder = MailListPresenter.GetFolderByEntryID(_entryID);
            if (olFolder != null)
            {
                bool isContains = false;
                MailUtility.IsContainsFolders(MailUtility.ClearFolders, olFolder.FullFolderPath, ref isContains);
                if (isContains)
                {
                    CreateMenuItem(LocalData.IsEnglish ? "Clear Folder" : "清空文件夹", OperationFileType.Clear, 4);
                }
            }
            CreateMenuItem(LocalData.IsEnglish ? "Rename Folder" : "重命名文件夹", OperationFileType.Rename, 1);
            CreateMenuItem(LocalData.IsEnglish ? "Move Folder" : "移动文件夹", OperationFileType.Move, 2);
        }

        private void CreateMenuItem(string text, OperationFileType type, int imageIndex)
        {
            ToolStripMenuItem folderItem = MailUtility.CreateMenuItem(imageList.Images[imageIndex], text, type);
            contextMenuStrip.Items.Add(folderItem);
        }

        void OnFolderSelected(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            if (item != null && item.Tag != null)
            {
                //ToolMenuItemObject toolMenuItemObject = (ToolMenuItemObject)item.Tag;
                OperationFileType operationType = (OperationFileType)(Enum.Parse(typeof(OperationFileType), item.Tag.ToString()));
                switch (operationType)
                {
                    case OperationFileType.Create:
                        AddFolder();
                        break;
                    case OperationFileType.Delete:
                        DeleteFolder();
                        break;
                    case OperationFileType.Move:
                        MoveFolder();
                        break;
                    case OperationFileType.Rename:
                        RenameFolder();
                        break;
                    case OperationFileType.Clear:
                        ClearFolder();
                        break;
                }
            }
        }

        void ClearFolder()
        {
            MAPIFolder olFolder = MailListPresenter.GetFolderByEntryID(_entryID);

            string message = LocalData.IsEnglish ? string.Format("Sure to permanently delete all the items of “{0}” folder?", olFolder.Name) : string.Format("确定要永久删除文件夹“{0}”中的全部项目吗?", olFolder.Name);
            DialogResult result = MessageBox.Show(message, "Microsoft Office Outlook", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int tokenID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Deleting..." : "正在删除...");
                WaitCallback callback = (obj) =>
                {
                    MAPIFolder folder = null;
                    Items olItems = null;
                    try
                    {
                        folder = obj as MAPIFolder;
                        olItems = folder.Items;
                        int count = olItems.Count;
                        for (int i = count; i >= 1; i--)
                        {
                            MailItem olItem = olItems[i] as MailItem;
                            if (olItem == null)
                            {
                                ReportItem olReportItem = olItem as ReportItem;
                                if (olReportItem != null)
                                {
                                    olReportItem.Delete();
                                    MailUtility.ReleaseComObject(olReportItem);
                                }
                            }
                            else
                            {
                                olItem.Delete();
                                MailUtility.ReleaseComObject(olItem);
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                    }
                    finally
                    {
                        ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(tokenID);
                        MailUtility.ReleaseComObject(olItems);
                        MailUtility.ReleaseComObject(olFolder);
                    }
                };
                ThreadPool.QueueUserWorkItem(callback, olFolder);
            }
        }
        /// <summary>
        /// disable key folder 
        /// </summary>
        void DisableSystemFolderContextMenuStrip()
        {
            if (String.IsNullOrEmpty(_folderName))
            { return; }

            ToolStripItemCollection collections = this.contextMenuStrip.Items;
            bool isChineseLetter = MailUtility.IsChineseLetter(_folderName);
            if (MailUtility.EditFolders.Contains(_folderName.ToLower()))
            {
                for (int i = 1; i < collections.Count; i++)
                {
                    if (!(LocalData.IsEnglish ? "Clear Folder" : "清空文件夹").Equals(collections[i].Text))
                    {
                        collections[i].Enabled = false;
                    }
                    else
                    {
                        if (this.ItemCount == 0)
                        {
                            collections[i].Enabled = false;
                        }
                    }
                }
            }
            else
            {
                if (IsRootFolder)
                {
                    collections[1].Enabled = collections[3].Enabled = false;
                    collections[2].Enabled = true;
                }
            }
        }


        void File_MouseUp(object sender, MouseEventArgs e)
        {
            folderMenu.Show(this, e.Location);
        }

        /// <summary>
        /// add folder
        /// </summary>
        void AddFolder()
        {
            DisposedFileEvent();

            TreeNode selectedNode = FolderPart.trvFolder.SelectedNode;
            if (selectedNode == null && String.IsNullOrEmpty(selectedNode.Text))
            {
                MessageBox.Show((LocalData.IsEnglish ? "Please select node!" : "请选择文件夹!"));
                return;
            }

            using (frmCreateFolder frmNew = new frmCreateFolder(_entryID))
            {
                if (frmNew.ShowDialog() == DialogResult.Cancel)
                    return;
                if (frmNew.SameFolderFlag)
                    return;
                String newFolderName = frmNew.txtFolderName.Text;
                if (String.IsNullOrEmpty(newFolderName))
                    return;

                MAPIFolder currentFolder = null;
                Folders olFolders = null;
                try
                {
                    currentFolder = MailListPresenter.GetFolderByEntryID(_entryID);
                    if (currentFolder == null)
                        return;
                    olFolders = currentFolder.Folders;
                    //判断是否已经存在新文件夹名称
                    bool isContains = false;
                    MailUtility.IsContainsSubFolders(olFolders, newFolderName, ref isContains);
                    if (isContains)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Exsit the same folder name." : "存在相同的文件夹名称!"));
                        return;
                    }
                    olFolders.Add(newFolderName, Type.Missing);
                    TreeViewPresenter.AddTreeNode(FolderPart.trvFolder.SelectedNode, olFolders, newFolderName, true);
                    FolderPart.Sort(FolderPart.trvFolder.SelectedNode);
                    //_folderPart.LoadControls(_folderPart.trvFolder, null, false);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "add folder success." : "添加文件夹成功.");
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "add folder failure: " : "添加文件夹失败: ") + ex.Message);
                }
                finally
                {
                    MailUtility.ReleaseComObject(olFolders);
                    MailUtility.ReleaseComObject(currentFolder);
                }
            }
        }

        /// <summary>
        /// delete folder
        /// </summary>
        void DeleteFolder()
        {
            MAPIFolder currentFolder = null;
            MAPIFolder folder = null;

            DisposedFileEvent();
            try
            {
                currentFolder = MailListPresenter.GetFolderByEntryID(_entryID);
                if (currentFolder == null)
                    return;

                String name = currentFolder.Name;
                DialogResult msgResult = MessageBox.Show((LocalData.IsEnglish ? "Are you sure delete folder." : "确定删除文件夹?"), LocalData.IsEnglish ? "prompt Info" : "提示信息", MessageBoxButtons.YesNo);
                if (msgResult == DialogResult.Yes)
                {
                    currentFolder.Delete();
                    TreeNode selectedNode = FolderPart.trvFolder.SelectedNode;
                    TreeNode cloneNode = (TreeNode)selectedNode.Clone();
                    bool isContains = false;
                    MailUtility.IsContainsFolders(MailUtility.DeleteItemsFolders, selectedNode.FullPath, ref isContains); ;
                    if (isContains)
                    {
                        selectedNode.Remove();
                    }
                    else
                    {
                        selectedNode.Remove();
                        FindDeleteItemFolder(FolderPart.trvFolder.SelectedNode, cloneNode);
                    }
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "delete success." : "删除成功.");
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "delete folder failure: " : "删除文件夹失败: ") + ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(currentFolder);
                MailUtility.ReleaseComObject(folder);
            }
        }

        void FindDeleteItemFolder(TreeNode oNode, TreeNode cloneNode)
        {
            TreeNode pNode = oNode.Parent;
            foreach (TreeNode node in pNode.Nodes)
            {
                if (node.Text.Contains("已删除邮件") || node.Text.Contains("delete items"))
                {
                    TreeViewPresenter.AddToTreeNode(node, cloneNode);
                    return;
                }
            }

            FindDeleteItemFolder(pNode, cloneNode);
        }

        /// <summary>
        /// rename folder
        /// </summary>
        void RenameFolder()
        {
            DisposedFileEvent();
            FolderPart.CurrentSelectFolderName = FolderPart.trvFolder.SelectedNode.Text;
            FolderPart.BeginEditNodeName();

            MAPIFolder CurrentFolder = null;
            try
            {
                CurrentFolder = MailListPresenter.GetFolderByEntryID(_entryID);
                if (CurrentFolder != null)
                {
                    //judge if exsit the same folder name 
                    bool isContains = false;
                    MailUtility.IsContainsFolders(CurrentFolder.Folders, _folderName, ref isContains);
                    if (isContains)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),
                                                                    (LocalData.IsEnglish
                                                                         ? "Exsit the same folder name."
                                                                         : "存在相同的文件夹名称!"));
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),
                                                            (LocalData.IsEnglish
                                                                 ? "rename folder failure: "
                                                                 : "重命名文件夹失败: ") + ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(CurrentFolder);
            }
        }

        /// <summary>
        /// move folder
        /// </summary>
        void MoveFolder()
        {
            DisposedFileEvent();

            MAPIFolder Selectfolder = null;
            MAPIFolder TargetFolder = null;

            try
            {
                Selectfolder = MailListPresenter.GetFolderByEntryID(_entryID);
                if (Selectfolder == null)
                    return;

                using (frmSelectFolder frmFolder = new frmSelectFolder(Selectfolder.FullFolderPath))
                {
                    if (frmFolder.ShowDialog() == DialogResult.Cancel)
                        return;
                    if (String.IsNullOrEmpty(frmFolder.FolderName))
                        return;
                    if (Selectfolder.Name == frmFolder.FolderName)
                        return;
                    if (string.IsNullOrEmpty(ClientProperties.Copy_Folder_EntryID))
                        return;

                    TargetFolder = MailListPresenter.GetFolderByEntryID(ClientProperties.Copy_Folder_EntryID);
                    string oFName = Selectfolder.Name;
                    Selectfolder.MoveTo(TargetFolder);
                    //TreeViewPresenter.RegisterTreeNodeEvent(_folderPart, TargetFolder.Folders, oFName);
                    //refersh folders
                    TreeViewPresenter.MoveFolderToFolder(FolderPart.trvFolder.Nodes, TargetFolder, Selectfolder.Name, frmFolder.FolderName);
                    FolderPart.Sort(null);
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "move folder failure: " : "移动文件夹失败: ") + ex.Message);
                return;
            }
            finally
            {
                MailUtility.ReleaseComObject(Selectfolder);
                MailUtility.ReleaseComObject(TargetFolder);
            }
        }
        #endregion

        //#region  邮件单击邮件地址右键菜单
        ///// <summary>
        ///// init contract context menu
        ///// </summary>
        ///// <param name="entryID"></param>
        //public void InitContextMenu(String entryID, IOutLookService outlookSerivice)
        //{
        //    contractMenu = new ContextMenu();
        //    _entryID = entryID;
        //    outLookService = outlookSerivice;

        //    MenuItem newMailItem = new MenuItem();
        //    newMailItem.Text = LocalData.IsEnglish ? "New Mail" : "新增邮件";
        //    newMailItem.Tag = ContactType.NewMail;
        //    newMailItem.Click += new EventHandler(ContextMenu_Click);
        //    contractMenu.MenuItems.Add(newMailItem);

        //    MenuItem addContactItem = new MenuItem();
        //    addContactItem.Text = LocalData.IsEnglish ? "Add to Contact" : "添加到联系人";
        //    addContactItem.Tag = ContactType.AddToContact;
        //    addContactItem.Click += new EventHandler(ContextMenu_Click);
        //    contractMenu.MenuItems.Add(addContactItem);

        //    MenuItem copyContactItem = new MenuItem();
        //    copyContactItem.Text = LocalData.IsEnglish ? "Copy Contact" : "复制联系人";
        //    copyContactItem.Tag = ContactType.CopyContact;
        //    copyContactItem.Click += new EventHandler(ContextMenu_Click);
        //    contractMenu.MenuItems.Add(copyContactItem);

        //    MenuItem sendingByContactItem = new MenuItem();
        //    sendingByContactItem.Text = LocalData.IsEnglish ? "History Sending by the Contact" : "以联系人作为发件人历史记录";
        //    sendingByContactItem.Tag = ContactType.SendingByContact;
        //    sendingByContactItem.Click += new EventHandler(ContextMenu_Click);
        //    contractMenu.MenuItems.Add(sendingByContactItem);

        //    MenuItem receivingByContactItem = new MenuItem();
        //    receivingByContactItem.Text = LocalData.IsEnglish ? "History Receiving by the Contact" : "以联系人作为接收人历史记录";
        //    receivingByContactItem.Tag = ContactType.ReceivingByContact;
        //    receivingByContactItem.Click += new EventHandler(ContextMenu_Click);
        //    contractMenu.MenuItems.Add(receivingByContactItem);

        //    MenuItem sendingReceivingByContactItem = new MenuItem();
        //    sendingReceivingByContactItem.Text = LocalData.IsEnglish ? "History Sending/Receiving by the Contact" : "以联系人作为接收人和发件人历史记录";
        //    sendingReceivingByContactItem.Tag = ContactType.SendingReceivingByContact;
        //    sendingReceivingByContactItem.Click += new EventHandler(ContextMenu_Click);
        //    contractMenu.MenuItems.Add(sendingReceivingByContactItem);

        //    this.AutoSize = true;
        //    DisposedContactEvent();
        //    this.MouseUp += new MouseEventHandler(ContractLink_MouseUp);
        //}
        ///// <summary>
        /////  new mail
        ///// </summary>
        //public void NewMail()
        //{
        //    try
        //    {
        //        outLookService.AddNewEmail(EmailAddress);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "add failure: " : "添加失败: ") + ex.Message);
        //    }

        //    DisposedContactEvent();
        //}

        //public void DisposedContactEvent()
        //{
        //    this.MouseUp -= new MouseEventHandler(ContractLink_MouseUp);
        //}

        //public void ContractLink_MouseUp(object sender, MouseEventArgs e)
        //{
        //    contractMenu.Show(this, e.Location);
        //    address = base.Text;
        //    RecipientType = base.Tag;
        //}

        //public void ContextMenu_Click(object sender, EventArgs e)
        //{
        //    MenuItem item = sender as MenuItem;
        //    if (item != null && item.Tag != null && Enum.IsDefined(typeof(ContactType), item.Tag))
        //    {
        //        ContactType type = (ContactType)(Enum.Parse(typeof(ContactType), item.Tag.ToString()));
        //        switch (type)
        //        {
        //            case ContactType.NewMail:
        //                NewMail();
        //                break;
        //            case ContactType.CopyContact:
        //                CopyContact(EmailAddress);
        //                break;
        //            case ContactType.AddToContact:
        //                MailListPresenter.AddToContact(address, EmailAddress);
        //                break;
        //            default:
        //                try
        //                {
        //                    MailListPresenter.OpenAdvancedSearchPart(type, EmailAddress);
        //                }
        //                catch (System.Exception ex)
        //                {
        //                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        //                }
        //                break;
        //            //case ContactType.SendingByContact:
        //            //    //if (OnSearched != null) OnSearched(EmailAddress, String.Empty);
        //            //    break;
        //            //case ContactType.ReceivingByContact:
        //            //    //if (OnSearched != null) OnSearched(String.Empty, EmailAddress);
        //            //    ContractSearch(String.Empty, EmailAddress);
        //            //    break;
        //            //case ContactType.SendingReceivingByContact:
        //            //    //if (OnSearched != null) OnSearched(EmailAddress, EmailAddress);
        //            //    ContractSearch(EmailAddress, EmailAddress);
        //            //    break;
        //        }
        //    }

        //    DisposedContactEvent();
        //    item = null;
        //}

        //void CopyContact(string emailAddress)
        //{
        //    try
        //    {
        //        Clipboard.Clear();
        //    }
        //    catch (System.Runtime.InteropServices.ExternalException ex)
        //    {
        //        Clipboard.Clear();
        //    }
        //    catch (System.Threading.ThreadStateException t)
        //    {
        //        Clipboard.Clear();
        //    }
        //    finally
        //    {
        //        Clipboard.SetText(emailAddress);
        //    }
        //}
        /////// <summary>
        /////// Search Contract info
        /////// </summary>
        /////// <param name="sender"></param>
        /////// <param name="receivent"></param>
        ////void ContractSearch(String sender, String receivent)
        ////{
        ////    SearchForm searchForm = new SearchForm(_folderPart);
        ////    //get the selected folder
        ////    String entryID = String.Empty;
        ////    String folderName = String.Empty;

        ////    try
        ////    {
        ////        //FillSearchFormText(sender);
        ////        //SelectedMailFolder(ref entryID, ref folderName);
        ////        //ClientProperties._entryID = entryID;
        ////        //searchForm.TxtFolder = folderName;
        ////        //searchForm.chkFolder.Checked = true;
        ////        //searchForm.txtRecivepent.Text = receivent;
        ////        //searchForm.txtSender.Text = sender;
        ////        //searchForm.Show();
        ////        //searchForm.btnSearch.Enabled = true;
        ////    }
        ////    catch (System.Exception ex)
        ////    {
        ////        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        ////        return;
        ////    }
        ////    finally
        ////    {
        ////        this.MouseUp -= new MouseEventHandler(ContractLink_MouseUp);
        ////    }
        ////}

        ////void SelectedMailFolder(ref string entryID, ref string folderName)
        ////{
        ////    MAPIFolder CurrentFolder = null;
        ////    MAPIFolder parentFolder = null;

        ////    if (!String.IsNullOrEmpty(_entryID))
        ////    {
        ////        CurrentFolder = outlookNS.GetFolderFromID(_entryID, Type.Missing);
        ////        //find the parent folder
        ////        if (CurrentFolder != null)
        ////        {
        ////            parentFolder = CurrentFolder.Parent as MAPIFolder;
        ////            entryID = parentFolder.EntryID;
        ////            folderName = parentFolder.Name;
        ////            //GetParentFolder(CurrentFolder, CurrentFolder, ref entryID, ref folderName);
        ////        }
        ////    }

        ////    else
        ////    {
        ////        CurrentFolder = outlookNS.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
        ////        if (CurrentFolder != null)
        ////        {
        ////            entryID = CurrentFolder.EntryID;
        ////            folderName = CurrentFolder.Name;
        ////        }
        ////    }

        ////    if (CurrentFolder != null) Marshal.ReleaseComObject(CurrentFolder);
        ////    if (parentFolder != null) Marshal.ReleaseComObject(parentFolder);
        ////}

        //#endregion
    }
}

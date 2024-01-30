using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Office.Interop.Outlook;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.Commands;
using System.Threading;
using Microsoft.Practices.CompositeUI;
using System.Diagnostics;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件文件夹列表面板
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public partial class EmailFolderPart : System.Windows.Forms.UserControl
    {
        #region Services
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        #endregion

        #region 属性
        private Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = this.RootWorkItem.SmartParts;
                return _SmartParts;
            }
        }

        public EmailListPart ListPart
        {
            get
            {
                return SmartParts.Get<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart);
            }
        }

        private EmailToolBarPart _ToolBarPart;
        public EmailToolBarPart ToolPart
        {
            get
            {
                if (_ToolBarPart == null)
                    _ToolBarPart = SmartParts.Get<EmailToolBarPart>(MailCenterWorkSpaceConstants.EmailToolBarPart);

                return _ToolBarPart;
            }
        }


        private SearchFolderPart _SearchFolderPart;
        public SearchFolderPart SearchFolderPart
        {
            get
            {
                if (_SearchFolderPart == null)
                {
                    _SearchFolderPart = SmartParts.Get<SearchFolderPart>(MailCenterWorkSpaceConstants.SearchFolderPart);
                }
                return _SearchFolderPart;
            }
        }

        public EmailDetailPart DetailPart
        {
            get
            {
                return SmartParts.Get<EmailDetailPart>(MailCenterWorkSpaceConstants.EmailDetailPart);
            }
        }

        public IMainForm mainForm
        {
            get { return ServiceClient.GetClientService<IMainForm>(); }
        }
        private SearchFolderPart _searchFolder;
        public SearchFolderPart searchFolder
        {
            get
            {
                if (_searchFolder == null)
                    _searchFolder = RootWorkItem.Items.AddNew<SearchFolderPart>();
                return _searchFolder;
            }
        }

        #endregion

        #region 常量
        private EventHandler evtHandler = null;
        private Image loadingImage = null;
        bool isFirstLoadCfg = true;
        //标示是否加载完子节点
        public bool isLoadingFinished = true;
        /// <summary>
        /// 当前选择的文件夹节点名称
        /// </summary>
        public String CurrentSelectFolderName
        {
            get;
            set;
        }

        /// <summary>
        /// 文件夹数量文本
        /// </summary>
        string display_Folder_Count = string.Empty;
        string folder_Tag = string.Empty;

        private const string searchFolders = "SearchFolders";
        #endregion

        #region 事件
        private delegate void AddRootTreeNodeDelegate(TreeView treeView, TreeNode newNode);
        private delegate void AddTreeNodes(TreeNode parentNode, TreeNode[] _treeNodes);
        /// <summary>
        /// 删除树节点
        /// </summary>
        /// <param name="node"></param>
        private delegate void RemoveNodeDelegate(TreeView treeView, TreeNode selectedNode);
        /// <summary>
        /// 将节点集合添加到节点上
        /// </summary>
        /// <param name="treeNodes"></param>
        private delegate void AddRangeTreeNodesDelegate(TreeNode parentNode, List<TreeNode> treeNodes);
        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public EmailFolderPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                //this.lblLoading.Text = MailUtility.LoadingText;
                this.Disposed += delegate
                {
                    DisposedCompent();
                };
            }
        }
        private ScrollableControlMessageFilter filter;
        private void DisposedCompent()
        {
            _SmartParts = null;
            _ToolBarPart = null;
            TreeViewPresenter.Dispose();
            loadingImage = null;
            OnTreeNodeMouseClick(false);
            if (!LocalData.IsDesignMode)
            {
                if (mainForm != null)
                {
                    mainForm.MainFormActivated -= new EventHandler(mainForm_Activated);
                    mainForm.MainFormDeactivated -= new EventHandler(mainForm_Deactivated);
                    mainForm.ApplicationExit -= new EventHandler(MailFormClosed);
                }
            }
            if (this.RootWorkItem != null) RootWorkItem.Items.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                mainForm.MainFormActivated += new EventHandler(mainForm_Activated);
                mainForm.MainFormDeactivated += new EventHandler(mainForm_Deactivated);
                OnTreeNodeMouseClick(true);
                AddMessageFilter();
            }
            if (!LocalData.IsDesignMode && isFirstLoadCfg)
            {
                ShowLoadingImage();
                isFirstLoadCfg = false;
                LoadTreeViewConfig();
                ClientProperties.folder_FullPath = TreeViewPresenter.DefaultUseFolderFullPath();
                mainForm.ApplicationExit += new EventHandler(MailFormClosed);
            }
        }

        private void OnTreeNodeMouseClick(bool isRegister)
        {
            if (isRegister)
                this.trvFolder.NodeMouseClick +=
                    new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvFolder_NodeMouseClick);
            else
            {
                this.trvFolder.NodeMouseClick -=
                   new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvFolder_NodeMouseClick);
            }
        }

        /// <summary>
        /// 动态显示Loading图片
        /// </summary>
        private void ShowLoadingImage()
        {
            evtHandler = new EventHandler(OnImageAnimate);
            this.picLoading.Image = loadingImage = global::ICP.MailCenter.UI.Properties.Resources.loading;
        }

        #endregion

        #region 激活界面
        private void AddMessageFilter()
        {
            filter = new ScrollableControlMessageFilter(this.trvFolder);
            System.Windows.Forms.Application.AddMessageFilter(filter);
        }
        void mainForm_Deactivated(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.RemoveMessageFilter(filter);
        }

        void mainForm_Activated(object sender, EventArgs e)
        {
            AddMessageFilter();
        }
        #endregion

        #region 加载配置文件
        void LoadTreeViewConfig()
        {
            bool isLoadFormXml = true;
            BeginAnimate();
            try
            {
                TreeViewDataAccess.LoadTreeViewDataFromXml(trvFolder, MailUtility.ConfigPath);
                if (trvFolder.Nodes.Count == 0)
                    LoadTreeViewFormOutlook(out isLoadFormXml);
            }
            catch
            {
                LoadTreeViewFormOutlook(out isLoadFormXml);
            }
            finally
            {
                trvFolder.TreeViewNodeSorter = new TreeNodeComparer();
                if (isLoadFormXml)
                {
                    ClientProperties.IsRefershFolders = true;
                    RefershTreeView();
                    //TreeViewPresenter.GetDefaultUserFolderName();
                }
                else
                {
                    TreeViewPresenter.ReleaseMailCenterHandle();
                    //StopAnimate();
                }
            }
        }
        /// <summary>
        /// 第一次加载或者加载配置文件出异常时,需要重新加载outlook文件夹
        /// </summary>
        /// <param name="isLoadFormXml"></param>
        private void LoadTreeViewFormOutlook(out bool isLoadFormXml)
        {
            trvFolder.TreeViewNodeSorter = new TreeNodeComparer();
            LoadControls(trvFolder, true);
            isLoadFormXml = false;
        }
        #endregion

        #region 主窗体关闭时需要记录节点到配置文件中
        [CommandHandler(MailCenterCommandConstants.Command_CloseMainForm)]
        public void Command_CloseMainForm(object sender, EventArgs e)
        {
            // this.FindForm().Close();
            System.Windows.Forms.Application.Exit();
        }

        public void MailFormClosed(object sender, EventArgs e)
        {
            if (isLoadingFinished)
                Closed();
        }
        void Closed()
        {
            SaveToConfig();
        }

        void SaveToConfig()
        {
            try
            {
                if (ClientProperties.IsNeedDeleteConfigFile)
                    System.IO.File.Delete(MailUtility.ConfigPath);
                else
                    TreeViewDataAccess.SaveTreeViewDataToXml(trvFolder, MailUtility.ConfigPath, TreeViewPresenter.GetDefaultUserFolderEntryID());
                ClientUtility.LogOff();

                MailUtility.RegisterMinToTrayKey(string.Format("{0}.0", ClientUtility.olVersion));
                MailUtility.AutoSyncDisable(true);
            }
            catch (System.Exception ex) { }
            finally
            {
                MailUtility.ReleaseComObject(ClientUtility.OlNS);
                MailUtility.ReleaseComObject(ClientUtility.OlApp);
            }
        }

        #endregion

        #region 选择文件夹
        /// <summary>
        /// select folder mail list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvFolder_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null || e.Node.Tag == "" || e.Node.Text == null || e.Node.Tag == searchFolders)
                return;
            //点击+/-号时，不需要选中节点
            if (!e.Node.Bounds.Contains(e.Location))
                return;

            ClientProperties.CurrentSelectedNode = e.Node;
            // DetailPart.Visible = true;
            string entryID = ClientProperties.CurrentSelectedNode.Tag.ToString();
            ClientProperties.Folder_EntryID = entryID;
            ClientProperties.CurrentFolderName = ClientProperties.CurrentSelectedNode.Text;
            //为了解决选择文件夹跳动问题
            trvFolder.SelectedNode = ClientProperties.CurrentSelectedNode;
            if (e.Button == MouseButtons.Right)
            {
                //弹出上下文菜单
                ShowFolderContextMenu(entryID, ClientProperties.CurrentSelectedNode);
            }
            else if (e.Button == MouseButtons.Left)  //选择文件夹
            {     //选择的文件夹是根目录文件夹，不需要显示邮件列表
                if (MailListPresenter.IsContainsRootFolder(ClientProperties.CurrentSelectedNode.Text))
                {
                    DetailPart.Visible = false;
                    return;
                }
                //重复点击，直接返回
                if (!ClientProperties.Folder_EntryID.Equals(folder_Tag))
                {
                    CheckApplication();
                    this.RootWorkItem.State[MailCenterCommandConstants.CurrentEntryID] = entryID;
                    this.RootWorkItem.State[MailCenterCommandConstants.CurrentNodeText] = ClientProperties.CurrentSelectedNode.Text;
                    this.RootWorkItem.Commands[MailCenterCommandConstants.Command_CurrentFolderChanged].Execute();
                    folder_Tag = entryID;
                }
            }
        }
        private void CheckApplication()
        {
            if (Process.GetProcessesByName("outlook").Length == 0)
            {
                try
                {
                    MailUtility.StartProcess(false, string.Empty);
                    ClientUtility.GetOutlookNewNameSpace();
                }
                catch { }
                finally
                {
                    TreeViewPresenter.RegisterAllExpandedNodes(trvFolder, true);
                }
            }
        }

        /// <summary>
        /// show出文件夹上下文菜单
        /// </summary>
        /// <param name="entryID"></param>
        /// <param name="node"></param>
        private void ShowFolderContextMenu(string entryID, TreeNode node)
        {
            FolderLink folderLink = new FolderLink();
            folderLink.FolderContextMenu(entryID, node.Text);
            node.ContextMenuStrip = folderLink.contextMenuStrip;

        }

        /// <summary>
        /// rename the folder name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void trvFolder_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //if (e.Node.Parent == null) { return; }
            //取消编辑
            e.CancelEdit = true;
            if (e.Label == null || string.IsNullOrEmpty(e.Label.Trim())) //还原原来的值 
            {
                RefershFolder(e, CurrentSelectFolderName, false);
                return;
            }

            //判断是否包含不可重命名的文件夹
            bool isUnable_RenameFolder = false;
            MailUtility.IsEqualsFolders(MailUtility.EditFolders, e.Node.Text, ref isUnable_RenameFolder);
            if (!AllowEditRootFolder(e, e.Label.Trim()) || isUnable_RenameFolder)
            {

                e.Node.EndEdit(true);//不保存修改值
                trvFolder.LabelEdit = false;
                return;
            }

            MAPIFolder CurrentFolder = null;
            NameSpace olNS = null;
            MAPIFolder parentFolder = null;
            try
            {
                CurrentFolder = MailListPresenter.GetFolderByEntryID(e.Node.Tag.ToString());
                if (CurrentFolder != null)
                {
                    parentFolder = CurrentFolder.Parent as MAPIFolder;
                    if (parentFolder != null)
                    {
                        //如果当前文件夹父节点下所有文件有相同文件夹名称，则不允许更改文件件名称
                        bool isContains = false;
                        MailUtility.IsContainsFolders(parentFolder, e.Label, ref isContains);
                        if (isContains)
                        {
                            RefershFolder(e, CurrentSelectFolderName, false);
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),
                                                                        (LocalData.IsEnglish ? "Exsit the same folder name." : "存在相同的文件夹名称!"));
                            return;
                        }
                        else
                        {
                            RenameTreeNodeAndMAPIFolder(e, CurrentFolder);
                        }
                    }
                    else
                    {
                        RenameTreeNodeAndMAPIFolder(e, CurrentFolder);
                    }
                    display_Folder_Count = string.Empty;
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                                                                   LocalData.IsEnglish
                                                                       ? "rename Successfully."
                                                                       : "重命名成功.");
            }

            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),
                                                            (LocalData.IsEnglish ? "rename folder Failure: " : "重命名失败: ") +
                                                            ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(olNS);
                MailUtility.ReleaseComObject(CurrentFolder);
            }
        }

        private bool AllowEditRootFolder(NodeLabelEditEventArgs e, string nodeName)
        {
            if (e.Node.Parent == null)
            {
                return !MailListPresenter.IsContainsRootFolder(nodeName);
            }
            return true;
        }


        private void RenameTreeNodeAndMAPIFolder(NodeLabelEditEventArgs e, MAPIFolder olFolder)
        {
            string newName = string.Format("{0}{1}", e.Label, display_Folder_Count);
            RefershFolder(e, newName, true);

            MailListPresenter.RenameMAPIFolder(olFolder, e.Label);
            //olFolder.Name = e.Label;
            //olFolder.CopyTo(olFolder);
        }

        void RefershFolder(NodeLabelEditEventArgs e, string nodeName, bool isNewName)
        {
            e.Node.EndEdit(false);//保存修改的值
            trvFolder.LabelEdit = false;
            if (string.IsNullOrEmpty(nodeName)) return;

            if (isNewName)
            {
                //rename folder name
                if (!string.IsNullOrEmpty(display_Folder_Count))
                    e.Node.Text = nodeName;
                else
                    e.Node.Text = e.Label;
                Sort(e.Node.Parent);
            }
            else
                e.Node.Text = nodeName;

            trvFolder.Refresh();
        }

        #region 拖拽文件夹和邮件
        private void trvFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (trvFolder.SelectedNode == null) { return; }
            if (e.KeyCode == Keys.F2)
            {
                CurrentSelectFolderName = trvFolder.SelectedNode.Text;
                //判断重命名后的文件夹名称是否跟原来名称相同
                bool isSame = false;
                MailUtility.IsEqualsFolders(MailUtility.EditFolders, CurrentSelectFolderName.ToLower(), ref isSame);
                if (isSame == false)
                    BeginEditNodeName();
            }
        }
        #endregion

        #region 设置节点在编辑时显示的值
        public void BeginEditNodeName()
        {
            DisplayNodeName();
            trvFolder.LabelEdit = true;
            trvFolder.SelectedNode.BeginEdit();
        }

        private void DisplayNodeName()
        {
            if (trvFolder.SelectedNode.ForeColor != Color.Black)
            {
                string selectedNodeName = trvFolder.SelectedNode.FullPath;
                //设置需要显示所有邮件的文件夹的节点值
                bool isTatalFolder = HasDisplayTreeNodeText(MailUtility.DOJFolders, selectedNodeName, new char[1] { '[' }, "[");
                if (isTatalFolder) { return; }

                //设置需要显示未读邮件的文件夹节点值
                bool isUnreadFolder = HasDisplayTreeNodeText(MailUtility.UnreadFolders, selectedNodeName, new char[1] { '(' }, "(");
                if (isUnreadFolder) { return; }
                //其他新增的文件夹
                else
                {
                    DisplayTreeNodeText(new char[] { '(' }, "(");
                }
            }
        }
        void DisplayTreeNodeText(char[] chars, string character)
        {
            List<string> list = MailUtility.SplitStringToList(trvFolder.SelectedNode.Text, chars);
            if (list != null && list.Count > 1)
            {
                string displayText = string.Empty;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    displayText += list[i];
                }
                trvFolder.SelectedNode.Text = displayText;
                display_Folder_Count = string.Format("{0}{1}", character, list[list.Count - 1]);
            }
        }
        bool HasDisplayTreeNodeText(string[] totalFolders, string selectedNodeName, char[] chars, string character)
        {
            bool isContains = false;
            MailUtility.IsContainsFolders(totalFolders, selectedNodeName, ref isContains);
            if (isContains)
                DisplayTreeNodeText(chars, character);

            return isContains;
        }
        #endregion

        private void trvFolder_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;

        }

        #region 常量

        private System.Windows.Forms.Timer timer = null;

        private void StartTimer()
        {
            timer.Enabled = true;
            timer.Start();
        }

        private void StopTimer()
        {
            this.timer.Stop();
            this.timer.Enabled = false;
        }

        private int index = 0;
        private TreeNode destinationTreeNode = null;
        private void trvFolder_DragOver(object sender, DragEventArgs e)
        {
            if (timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 750;
                timer.Enabled = false;
                timer.Tick += new EventHandler(timer_Tick);
            }
            //trvFolder.Focus();
            Point targetPoint = this.PointToClient(new Point(e.X, e.Y));
            destinationTreeNode = trvFolder.GetNodeAt(targetPoint);
            if (destinationTreeNode != null)
            {
                trvFolder.SelectedNode = destinationTreeNode;
                if (!destinationTreeNode.IsExpanded && destinationTreeNode.FirstNode != null && destinationTreeNode.LastNode != null)
                {
                    StartTimer();
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            StopTimer();
            destinationTreeNode.Expand();
            trvFolder.SelectedNode = ClientProperties.CurrentSelectedNode;


        }

        private void trvFolder_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode tn = e.Item as TreeNode;
            if (tn.Parent != null && !string.IsNullOrEmpty(tn.Text))
            {
                bool isSame = false;
                MailUtility.IsEqualsFolders(MailUtility.AllFolders, tn.Text.ToLower(), ref isSame);
                if (!isSame)
                {
                    DoDragDrop(e.Item, DragDropEffects.Move);
                }
            }
        }

        private void trvFolder_DragDrop(object sender, DragEventArgs e)
        {
            //过滤附件拖拽到文件夹
            if (!e.Data.GetFormats().Contains("RenPrivateItem") && !e.Data.GetFormats().Contains("System.Windows.Forms.TreeNode"))
                return;

            MAPIFolder TargetFolder = null;
            MAPIFolder CurrentFolder = null;
            TreeNode _oNode = null;
            NameSpace olNS = null;

            int _iTargetIndex = 0;

            #region 获取目标文件夹
            Point _oMousePos = trvFolder.PointToClient(new Point(e.X, e.Y));
            //释放拖动时，鼠标所在的节点
            TreeNode _oTargetNode = trvFolder.GetNodeAt(_oMousePos);
            if (_oTargetNode != null)
            {
                if (_oTargetNode.Level < 0)  //根目录
                    return;
                //先保存
                _iTargetIndex = _oTargetNode.Index;
                if (_oTargetNode.Tag != null && _oTargetNode.Tag != "")
                    TargetFolder = MailListPresenter.GetFolderByEntryID(_oTargetNode.Tag.ToString());
            }
            else
            {
                return;
            }
            #endregion

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                _oNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            }
            else  //表示邮件拖拽到文件夹
            {
                //有根目录时，邮件不能拖拽到根目录
                if (ClientProperties.RootFolders.Count >= 0)
                    if (_oTargetNode.Level <= 0) return;
                    else
                        if (_oTargetNode.Level < 0) return;

                WaitCallback callback = MoveMailToFolder;
                ThreadPool.QueueUserWorkItem(callback, TargetFolder);
                return;
            }

            TreeNode newNode = null;
            try
            {
                //表示文件夹之间的拖拽
                if (_oNode != null && _oNode.TreeView == trvFolder && TargetFolder != null)
                {
                    CurrentFolder = MailListPresenter.GetFolderByEntryID(_oNode.Tag.ToString());
                    if (CurrentFolder == null)
                        return;
                    //说明：如果在一个账户的文件夹下，同一个文件夹存在相同的文件夹名称，是不可以拖拽的，直接跳出。
                    //如果存在多个账户文件夹，并且文件夹名称相同，这时将其中一个账户文件夹拖拽到另外一个账户文件夹下，需要重命名被拖拽的文件夹名称 FolderName=>FolderName1
                    if (CurrentFolder.FullFolderPath.Equals(TargetFolder.FullFolderPath))
                        return;

                    bool isContains = false;
                    MailUtility.IsContainsFolders(TargetFolder, CurrentFolder.Name, ref isContains);
                    if (isContains && CurrentFolder.FullFolderPath.Contains(TargetFolder.FullFolderPath)) return;

                    if (isContains)
                    {
                        string newFolderName = string.Format("{0}_1", CurrentFolder.Name);
                        CurrentFolder.Name = newFolderName;
                        TreeViewPresenter.RenameTreeNode(_oNode);
                    }

                    ClientProperties.isDragDropFolder = true;
                    if (_oTargetNode.FirstNode != null)
                    {
                        bool isNodeExpend = _oTargetNode.FirstNode.Text != MailUtility.LoadingText;
                        MoveFolderToFolder(CurrentFolder, TargetFolder, _oNode, _oTargetNode, isNodeExpend);
                    }
                    else
                    {
                        CurrentFolder.MoveTo(TargetFolder);
                        _oNode.Remove();
                        TreeViewPresenter.AddShamNode(_oTargetNode);
                    }
                }
                // 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
                else
                {
                    _oNode.Remove();
                    trvFolder.Nodes.Add(_oNode);
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Drag Folder Failure: " : "拖拽文件夹失败: ") + ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(CurrentFolder);
                MailUtility.ReleaseComObject(TargetFolder);
            }
        }

        void MoveFolderToFolder(MAPIFolder oFolder, MAPIFolder tFolder, TreeNode oNode, TreeNode tNode, bool isNodeExpend)
        {
            //将原文件夹拖拽到目标文件夹
            string oFName = oFolder.Name;
            string oFullPath = oFolder.FullFolderPath;
            oFolder.MoveTo(tFolder);
            trvFolder.SelectedNode = tNode;
            //将原文件夹删除，然后在插入到目标文件夹下                   
            TreeNode newNode = (TreeNode)oNode.Clone();
            oNode.Remove();
            if (isNodeExpend)
            {
                TreeViewPresenter.RegisterTreeNodeEvent(tFolder, oFName);
                // trvFolder.LabelEdit = true;
                //if (tNode.IsEditing == false)
                //    tNode.BeginEdit();
                string entryID = string.Empty;
                TreeViewPresenter.ReplaceEntryID(oFullPath, tFolder, newNode.Text, ref entryID);
                if (!string.IsNullOrEmpty(entryID))
                    newNode.Tag = entryID;

                tNode.Nodes.Add(newNode);
                //tNode.EndEdit(false);
                //trvFolder.LabelEdit = false;
            }
            Sort(tNode);
        }

        /// <summary>
        /// 编辑节点后，将其排序，并且选择相关的节点
        /// </summary>
        /// <param name="parentNode"></param>
        public void Sort(TreeNode parentNode)
        {
            trvFolder.BeginInvoke((System.Action)delegate
            {
                trvFolder.Sort();
                trvFolder.Refresh();
                if (parentNode != null)
                {
                    ClientProperties.CurrentSelectedNode = trvFolder.SelectedNode = parentNode;
                }
            });
        }
        /// <summary>
        /// 将邮件移至Outlook文件夹
        /// </summary>
        /// <param name="objFolder"></param>
        void MoveMailToFolder(object objFolder)
        {
            MAPIFolder targetFolder = objFolder as MAPIFolder;
            if (targetFolder == null)
                return;

            ListPart.axViewCtlEmailList.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => InnerMoveMailToFolder(targetFolder)));
        }

        void InnerMoveMailToFolder(MAPIFolder targetFolder)
        {
            bool isLastMail = false;
            Selection olSelection = ListPart.axViewCtlEmailList.Selection as Selection;
            if (olSelection != null && olSelection.Count > 0)
            {
                MAPIFolder mapiFolder = null;
                //同一文件夹之间邮件拖拽
                if (TreeViewPresenter.IsSameFolder(ClientProperties.CurrentSelectedNode, targetFolder))
                {
                    MailUtility.ReleaseComObject(targetFolder);
                    return;
                }

                ClientProperties.DragDropUnreadMail.Clear();
                int count = olSelection.Count;
                //如果邮件超过10封邮件，则需要显示进度条
                frmShowProgressBar progressBar = null;
                if (count >= 5)
                {
                    progressBar = new frmShowProgressBar(count, targetFolder.Name);
                    progressBar.InnerShow();
                }

                ClientProperties.isDragDropMail = true;
                ClientProperties.isDragDropLastOne = false;
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        if (i == count)
                        {
                            ClientProperties.isDragDropLastOne = true;
                            isLastMail = true;
                        }

                        TransformEntity(olSelection[i], targetFolder, isLastMail);
                        if (progressBar != null)
                        {
                            if (progressBar.IsDisposed == false)
                                progressBar.OnProgessChanged(i);
                            else
                                break;
                        }
                    }
                    ClientProperties.DragDropUnreadMail.Clear();
                }
                catch (System.Exception ex)
                {
                    ClientProperties.DragDropUnreadMail.Clear();
                    if (progressBar != null)
                        progressBar.Cancel();
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Drag Mail Failure: " : "拖拽邮件失败: ") + ex.Message);
                }
                finally
                {
                    MailUtility.ReleaseComObject(targetFolder);
                    trvFolder.SelectedNode = ClientProperties.CurrentSelectedNode;
                    MailUtility.ReleaseComObject(olSelection);
                }
            }
        }
        /// <summary>
        /// 找到相对应的实体，转换成实体
        /// </summary>
        /// <param name="objSelection"></param>
        /// <param name="targetFolder"></param>
        /// <param name="isLastMail"></param>
        private void TransformEntity(object objSelection, MAPIFolder targetFolder, bool isLastMail)
        {
            MAPIFolder orgainalFolder = null;
            _MailItem mailItem = objSelection as MailItem; //普通邮件
            if (mailItem != null)
            {
                //将邮件拖拽到文件夹中
                mailItem.Move(targetFolder);
                ClientProperties.DragDropUnreadMail.Add(mailItem.UnRead);
                RefershFolderFlagCount(targetFolder, isLastMail);
                MailUtility.ReleaseComObject(mailItem);
            }
            else
            {
                _ReportItem reportItem = objSelection as ReportItem; //回执邮件
                if (reportItem != null)
                {
                    //将邮件拖拽到文件夹中
                    reportItem.Move(targetFolder);
                    ClientProperties.DragDropUnreadMail.Add(reportItem.UnRead);
                    RefershFolderFlagCount(targetFolder, isLastMail);
                    MailUtility.ReleaseComObject(reportItem);
                }
                else
                {
                    _ContactItem contactItem = objSelection as ContactItem; //联系人
                    if (contactItem != null)
                    {
                        orgainalFolder = contactItem.Parent as MAPIFolder;
                        //将邮件拖拽到文件夹中
                        contactItem.Move(targetFolder);
                        ClientProperties.DragDropUnreadMail.Add(contactItem.UnRead);
                        RefershFolderFlagCount(targetFolder, isLastMail);
                        MailUtility.ReleaseComObject(contactItem);
                    }
                    else
                    {
                        _TaskItem taskItem = objSelection as TaskItem;  //任务
                        if (taskItem != null)
                        {
                            //将邮件拖拽到文件夹中
                            taskItem.Move(targetFolder);
                            ClientProperties.DragDropUnreadMail.Add(taskItem.UnRead);
                            RefershFolderFlagCount(targetFolder, isLastMail);
                            MailUtility.ReleaseComObject(taskItem);
                        }
                        else
                        {
                            _NoteItem noteItem = objSelection as NoteItem;  //笔记
                            if (noteItem != null)
                            {
                                //将邮件拖拽到文件夹中
                                noteItem.Move(targetFolder);
                                RefershFolderFlagCount(targetFolder, isLastMail);
                                MailUtility.ReleaseComObject(noteItem);
                            }
                            else
                            {
                                AppointmentItem appointmentItem = objSelection as AppointmentItem; //职位
                                if (appointmentItem != null)
                                {
                                    appointmentItem.Move(targetFolder);
                                    ClientProperties.DragDropUnreadMail.Add(appointmentItem.UnRead);
                                    RefershFolderFlagCount(targetFolder, isLastMail);
                                    MailUtility.ReleaseComObject(appointmentItem);
                                }
                                else
                                {
                                    JournalItem journalItem = objSelection as JournalItem; //日记
                                    if (journalItem != null)
                                    {
                                        journalItem.Move(targetFolder);
                                        ClientProperties.DragDropUnreadMail.Add(journalItem.UnRead);
                                        RefershFolderFlagCount(targetFolder, isLastMail);
                                        MailUtility.ReleaseComObject(journalItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        #region 刷新新增邮件文件夹数量
        public void RefershFolderFlagCount(MAPIFolder TargetFolder, bool isLastMail)
        {
            try
            {
                if (isLastMail)
                {
                    //Thread.Sleep(1000);
                    bool isHaveMailUnRead = MailListPresenter.HasUnreadMail(ClientProperties.DragDropUnreadMail);
                    RefershMailFoldercount(TargetFolder, isHaveMailUnRead);
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Drag drop failure: " : "拖拽失败: ") + ex.Message);
            }
            finally
            {
                MailUtility.ReleaseComObject(TargetFolder);
            }
        }


        void RefershMailFoldercount(MAPIFolder tFolder, bool isHaveMailUnRead)
        {
            bool isContainsTargetFolder = MailUtility.IsInDOJFolders(tFolder.Name);
            if (isContainsTargetFolder) //显示总数文件夹
            {
                this.UpdateTreeNode(tFolder.Items.Count, tFolder.Name, tFolder.FullFolderPath, tFolder.EntryID);
            }
            else
            {
                if (isHaveMailUnRead) //显示未读数量文件夹
                {
                    this.UpdateTreeNode(tFolder.UnReadItemCount, tFolder.Name, tFolder.FullFolderPath, tFolder.EntryID);
                }
            }

            //oFolder = MailListPresenter.GetFolderByEntryID(oFolder.EntryID);
            //if (oFolder != null)
            //{
            //    bool isContainsOrginalFolder = MailUtility.IsInDOJFolders(oFolder.Name);

            //    if (isContainsOrginalFolder)
            //    {
            //        this.DealTreeNodes(oFolder.Items.Count, oFolder.Name, oFolder.FullFolderPath);
            //    }
            //    else
            //    {
            //        if (isHaveMailUnRead)
            //        {
            //            this.DealTreeNodes(oFolder.UnReadItemCount, oFolder.Name, oFolder.FullFolderPath);
            //        }
            //    }
            //}
        }

        #endregion

        #endregion
        #endregion

        #region 刷新未读邮件文件夹列表

        /// <summary>
        ///更新文件夹未读项数量
        /// </summary>
        /// <param name="result"></param>
        public void UpdateFolderUnreadMailCount(MAPIFolder folder)
        {
            if (folder == null)
            {
                return;
            }
            ClientProperties.IsReceiveNewMail = false;
            UpdateTreeNode(folder.UnReadItemCount, folder.Name, folder.FullFolderPath, folder.EntryID);
            folder = null;
        }

        /// <summary>
        /// 更新文件夹数量和展开文件夹
        /// </summary>
        /// <param name="count"></param>
        /// <param name="folderName"></param>
        /// <param name="fullPath"></param>
        /// <param name="entityID"></param>
        public void UpdateTreeNode(int count, string folderName, string fullPath, string entryID)
        {
            if (trvFolder.IsDisposed)
                return;

            if (trvFolder.InvokeRequired)
            {
                trvFolder.Invoke((System.Windows.Forms.MethodInvoker)
                    (() => ChangedTreeNode(count, folderName, fullPath, entryID)));
            }
            else
                ChangedTreeNode(count, folderName, fullPath, entryID);
        }

        /// <summary>
        /// 收到新邮件后，需要将接收新邮件的文件夹展开
        /// </summary>
        /// <param name="olFolder"></param>
        public void Expand(MAPIFolder olFolder)
        {
            if (olFolder != null)
            {
                MAPIFolder parentFolder = olFolder.Parent as MAPIFolder;
                TreeViewPresenter.ExpandByMAPIFolder(trvFolder, new FolderInfo() { EntryID = parentFolder.EntryID, Name = parentFolder.Name, FullPath = parentFolder.FullFolderPath });
                MailUtility.ReleaseComObject(parentFolder);
            }
        }

        /// <summary>
        /// 更改TreeNode文件夹数量和字体
        /// </summary>
        /// <param name="count"></param>
        /// <param name="folderName"></param>
        /// <param name="fullPath"></param>
        /// <param name="entryID"></param>
        public void ChangedTreeNode(int count, String folderName, string fullPath, string entryID)
        {
            TreeNode targetNode = TreeViewPresenter.SetFolderNodeText(trvFolder.Nodes, count, folderName, fullPath, entryID);

            if (ClientProperties.IsReceiveNewMail)
            {
                SetWindowText(targetNode);
                ClientProperties.IsReceiveNewMail = false;
            }
        }
        /// <summary>
        /// 收到邮件时，设置窗体的标题，以提示收到新邮件
        /// </summary>
        void SetWindowText(TreeNode targetNode)
        {
            if (ClientProperties.isDragDropMail == false && ClientProperties.UnReadCount > 0)
            {
                if (targetNode != null)
                {
                    if (trvFolder.InvokeRequired)
                    {
                        trvFolder.Invoke((System.Action)(() => TreeViewPresenter.ExpandAllSubTreeNodes(targetNode)));
                    }
                    else
                        TreeViewPresenter.ExpandAllSubTreeNodes(targetNode);
                }

                this.FindForm().Text = ClientProperties.newMail_UnRead;
            }
        }

        #endregion

        #region 同步OutLook文件夹

        [CommandHandler(MailCenterWorkSpaceConstants.Command_SynchronFolders)]
        public void Command_SynchronFolders(object sender, EventArgs e)
        {
            ToolPart.SetSynchrousAndRefreshButtonEnable(false);
            BeginAnimate();
            TreeViewPresenter.RefershTreeView(trvFolder, false);
            StopAnimate();            
        }

        #endregion

        #region 刷新文件夹列表
        [CommandHandler(MailCenterWorkSpaceConstants.Command_RefershFolderList)]
        public void Command_RefershFolderList(object sender, EventArgs e)
        {
            TreeViewPresenter.DicFolderPathAndNodes.Clear();
            BeginAnimate();
            this.LoadControls(this.trvFolder, false);
            ListPart.axViewCtlEmailList.Folder = ClientProperties.ViewCtlDefaultFolderPath;
            SearchFolderPart.dicSearchTreeNodes.Clear();
        }

        #endregion

        #region  刷新所有展开的节点
        [CommandHandler(MailCenterWorkSpaceConstants.Command_RefershAllExpandNodes)]
        public void Command_RefershAllExpandNodes(object sender, EventArgs e)
        {
            RefershTreeView();
        }

        private void RefershTreeView()
        {
            TreeViewPresenter.DicFolderPathAndNodes.Clear();
            ToolPart.SetSynchrousAndRefreshButtonEnable(false);
            TreeViewPresenter.RefershTreeView(trvFolder, true);
        }

        public void OnRefershed()
        {
            if (trvFolder.InvokeRequired)
                trvFolder.BeginInvoke((System.Windows.Forms.MethodInvoker)InnerRefershed);
            else
                InnerRefershed();
        }

        private void InnerRefershed()
        {
            ToolPart.SetSynchrousAndRefreshButtonEnable(true);
            string rootFolderName = TreeViewPresenter.GetSameRootFolderName(ClientProperties.RootFolders);
            if (!string.IsNullOrEmpty(rootFolderName))
            {
                MailListPresenter.ShowReminderForm(
                                  LocalData.IsEnglish
                                       ? string.Format("MailCenter is not support the same root folder(top folder) name {0},please right click rename it!", rootFolderName)
                                       : string.Format("邮件中心不支持根目录(最顶层)有相同文件夹名称 {0}，请右键单击文件夹重命名！", rootFolderName), true);
            }
            else
            {

                ToolPart.timeReminder.Start();
                ToolPart.ActiveAutoSync();
            }
        }

        #endregion

        #region 加载所有文件夹列表
        public void LoadControls(TreeView treeView, bool registerEvent)
        {
            //pnlLoading.Visible = registerEvent;
            ClientProperties.IsSynchronizingFolders = false;
            ClientProperties.RootFolders.Clear();
            treeView.Nodes.Clear();
            WaitCallback callback = (obj) =>
            {
                var olTreeView = obj as TreeView;
                LoadAllNodes(olTreeView, registerEvent);
            };
            ThreadPool.QueueUserWorkItem(callback, treeView);
        }
        /// <summary>
        /// 直接根据outlook文件夹结构，创建文件夹树
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="registerEvent"></param>
        void LoadAllNodes(TreeView treeView, bool registerEvent)
        {
            NameSpace olNS = null;
            try
            {
                //会获取到三个或者两个根目录，需要过滤                                
                DynamicLoadNodes(treeView, registerEvent);
            }
            catch (ThreadAbortException te)
            {
            }
            catch (System.ObjectDisposedException ode)
            {
            }
            catch (System.AccessViolationException ave)
            {
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "loading folders failure: " : "加载文件夹失败: ") + ex.Message);
            }
            finally
            {
                string rootFolderName = TreeViewPresenter.GetSameRootFolderName(ClientProperties.RootFolders);
                if (!string.IsNullOrEmpty(rootFolderName))
                {
                    MailListPresenter.ShowReminderForm(
                                   LocalData.IsEnglish
                                        ? string.Format("MailCenter is not support the same root folder(top folder) name {0},please right click rename it!", rootFolderName)
                                        : string.Format("邮件中心不支持根目录(最顶层)有相同文件夹名称 {0}，请右键单击文件夹重命名！", rootFolderName), true);
                }
            }
        }

        /// <summary>
        /// 动态加载treeview节点
        /// </summary>
        /// <param name="rootFolders"></param>
        /// <param name="selectNodeName"></param>
        void DynamicLoadNodes(TreeView treeView, bool registerEvent)
        {
            TreeNode rootNode = null;
            Folders rootFolders = ClientUtility.CreateOutlookNameSpaceInstance().Folders;
            int count = rootFolders.Count;

            string defaultUserFolderName = TreeViewPresenter.DefaultUseFolderName;
            for (int i = 1; i <= count; i++)
            {
                MAPIFolder folder = rootFolders[i];
                string folderName = MailUtility.ValidateNewNodeName(folder.Name);
                MailListPresenter.AddRootFolderToList(folderName);
                //过滤不是默认账户的邮件夹列表，就不需要展开
                if (!string.IsNullOrEmpty(defaultUserFolderName) && defaultUserFolderName == folderName &&
                    !string.IsNullOrEmpty(folder.WebViewURL))
                {
                    //文件夹根目录
                    rootNode = new TreeNode(folderName)
                    {
                        Tag = folder.EntryID,
                        ImageIndex = 0,
                        SelectedImageIndex = 0
                    };
                    var addRootTreeNodeDelegate = new AddRootTreeNodeDelegate(AddRootTreeNode);
                    treeView.Invoke(addRootTreeNodeDelegate, treeView, rootNode);
                    //所有子文件夹                    
                    var addTreeNodes = new AddTreeNodes(AddRange);
                    treeView.Invoke(addTreeNodes, rootNode, TreeViewPresenter.ShowALLFolders(treeView, folder, registerEvent));

                    //加载所有子文件夹后，需要追加搜索文件夹                    
                    //rootNode.Nodes.Add(TreeViewPresenter.CreateShamAndRealNode("搜索文件夹", searchFolders));
                }
                else
                {
                    var addRootTreeNodeDelegate = new AddRootTreeNodeDelegate(AddRootTreeNode);
                    treeView.Invoke(addRootTreeNodeDelegate, treeView, TreeViewPresenter.CreateShamAndRealNode(folderName, folder.EntryID));
                }

                TreeViewPresenter.RegisterFolderEvents(folder, true);

                folder = null;
            }
            MailUtility.ReleaseComObject(rootFolders);
        }

        private void AddRange(TreeNode parentNode, TreeNode[] subNodes)
        {
            parentNode.Nodes.AddRange(subNodes);
            parentNode.Expand();
            StopAnimate();
        }

        private void AddRootTreeNode(TreeView treeView, TreeNode rootNode)
        {
            treeView.Nodes.Add(rootNode);
        }

        #endregion

        #region 展开子节点
        private void trvFolder_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ExpandSubTreeNodes(trvFolder, e.Node, true);
        }

        public void ExpandSubTreeNodes(TreeView treeView, TreeNode node, bool isRegister)
        {
            if (node != null && node.FirstNode != null)
            {
                TreeNode clickNode = node;
                if (clickNode.FirstNode.Text.Equals(MailUtility.LoadingText) ||
                    clickNode.LastNode.Text.Equals(MailUtility.LoadingText))
                {

                    MAPIFolder _folder = null;
                    treeView.BeginUpdate();
                    WaitCallback callback = (obj) =>
                    {
                        TreeViewPacked entity = obj as TreeViewPacked;
                        TreeNode firstNode = entity.ClickNode.FirstNode;
                        isLoadingFinished = false;
                        if (entity.ClickNode.Tag != null && entity.ClickNode.Tag != "")
                        {
                            _folder = MailListPresenter.GetFolderByEntryID(entity.ClickNode.Tag.ToString());
                            if (entity.TreeView.IsHandleCreated && _folder != null)
                            {
                                try
                                {
                                    //当用户在展开很多子节点时，突然把邮件中心关闭，需要重新加载子文件夹
                                    ClearUncleanNodes(entity.TreeView, entity.ClickNode);
                                    AddSubNodes(entity.TreeView, entity.ClickNode, _folder, isRegister);

                                    var removeNodeDelegate = new RemoveNodeDelegate(RemoveTreeNode);
                                    entity.TreeView.BeginInvoke(removeNodeDelegate, entity.TreeView, entity.ClickNode);
                                    entity = null;

                                }
                                catch (ThreadAbortException te)
                                {
                                }
                                catch (System.ObjectDisposedException ode)
                                {
                                }
                                catch (System.AccessViolationException ave)
                                {
                                }
                                catch (System.Exception ex)
                                {
                                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),
                                                                                (LocalData.IsEnglish
                                                                                     ? "Loading Sub-Node failure:"
                                                                                     : "加载子节点失败:") + ex.Message);
                                    ICP.Framework.CommonLibrary.Logger.Log.Error(
                                        ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                                }
                            }
                        }
                    };
                    TreeViewPacked packed = new TreeViewPacked(treeView, clickNode);
                    ThreadPool.QueueUserWorkItem(callback, packed);
                }
                else
                {
                    trvFolder.SelectedNode = ClientProperties.CurrentSelectedNode;
                    clickNode = null;
                }
            }
        }

        /// <summary>
        /// 释放等待线程，通知其他线程可以 Do SomeThing
        /// </summary>
        private void ReleaseEvent()
        {
            if (ClientProperties.ExpandResetEvent != null)
                ClientProperties.ExpandResetEvent.Set();
        }

        private void RemoveTreeNode(TreeView treeView, TreeNode selectedNode)
        {
            if (selectedNode.FirstNode != null && selectedNode.FirstNode.Text.Equals(MailUtility.LoadingText))
            {
                selectedNode.FirstNode.Remove();
            }
            if (selectedNode.LastNode != null &&
                selectedNode.LastNode.Text.Equals(MailUtility.LoadingText))
            {
                selectedNode.LastNode.Remove();
            }

            treeView.EndUpdate();
            treeView = null;
        }

        private void ClearUncleanNodes(TreeView treeView, TreeNode node)
        {
            if (treeView.InvokeRequired)
            {
                treeView.Invoke((System.Action)(() => ClearNode(node)));
            }
            else
            {
                ClearNode(node);
            }
        }

        private void ClearNode(TreeNode node)
        {
            node.Nodes.Clear();
        }

        /// <summary>
        /// 添加子节点，在添加孙伪节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="olFolder"></param>
        public void AddSubNodes(TreeView treeView, TreeNode node, MAPIFolder _folder, bool isRegister)
        {
            TreeNode firstNode = node.FirstNode;
            Folders _olfolders = null;
            try
            {
                _olfolders = _folder.Folders;
                int count = _olfolders.Count;
                if (count > 0)
                {
                    node.Expand();
                    List<TreeNode> treeNodes = new List<TreeNode>();
                    for (int i = 1; i <= count; i++)
                    {
                        MAPIFolder folder = _olfolders[i];
                        //过滤跟目录下不是跟邮件有关的文件夹
                        if (node.Level == 0)
                        {
                            if (folder.DefaultItemType != OlItemType.olMailItem)
                                continue;
                        }

                        if (TreeViewPresenter.FilterFolders.Any(item => item.Equals(folder.Name.ToLower())))
                            continue;

                        treeNodes.Add(GeneralTreeNode(folder, false));
                        folder = null;
                    }
                    var addRangeTreeNodes = new AddRangeTreeNodesDelegate(AddRange);
                    treeView.BeginInvoke(addRangeTreeNodes, node, treeNodes);

                    TreeViewPresenter.RegisterFolderEvents(_folder);
                    _folder = null;
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "loading folders failure:" : "加载文件夹失败:") + CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                MailUtility.ReleaseComObject(_olfolders);
            }
        }


        private void AddRange(TreeNode parentNode, List<TreeNode> treeNodes)
        {
            TreeNode[] _treeNodes = treeNodes.ToArray();
            parentNode.Nodes.AddRange(_treeNodes);
            isLoadingFinished = true;
            ReleaseEvent();
            _treeNodes = null;
            treeNodes = null;
        }

        private TreeNode GeneralTreeNode(MAPIFolder folder, bool isRegister)
        {
            TreeNode subNode = TreeViewPresenter.ConvertMAPIFolderToTreeNode(folder);
            if (isRegister)
                TreeViewPresenter.RegisterFolderEvents(folder);

            if (MailListPresenter.HasSubFolder(folder))
                TreeViewPresenter.AddShamNode(subNode);

            return subNode;
        }

        private void trvFolder_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            //如果没有加载完毕，则不允许用户将节点折叠
            if (!isLoadingFinished)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        #endregion

        #region 键盘按上下键切换文件夹
        private void trvFolder_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TreeView treeviewFolder = sender as TreeView;
            if (treeviewFolder.SelectedNode != null)
            {
                if (e.KeyCode == Keys.Down)
                {
                    OnSelectedNode(new TreeNodeParameters(treeviewFolder.SelectedNode, treeviewFolder.SelectedNode.NextVisibleNode));
                }
                if (e.KeyCode == Keys.Up)
                {
                    OnSelectedNode(new TreeNodeParameters(treeviewFolder.SelectedNode, treeviewFolder.SelectedNode.PrevVisibleNode));
                }
            }
        }

        /// <summary>
        /// 键盘上下键选择树节点
        /// </summary>
        /// <param name="currentSelectedNode"></param>
        /// <param name="nextVisibleNode"></param>
        void OnSelectedNode(TreeNodeParameters parameters)
        {
            if (parameters == null) return;
            MAPIFolder olFolder = null;
            try
            {
                trvFolder.SelectedNode = parameters.SelectedNode;
                if (parameters.VisibleTreeNode != null)
                {
                    olFolder = MailListPresenter.GetFolderByEntryID(parameters.VisibleTreeNode.Tag.ToString());
                    if (olFolder != null)
                        MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, olFolder.FullFolderPath, olFolder.Name);
                }
            }
            catch (System.Exception ex)
            {
                try
                {
                    olFolder = (MAPIFolder)ClientUtility.CreateOutlookNameSpaceInstance().GetFolderFromID(parameters.VisibleTreeNode.Tag.ToString(), Type.Missing);
                    if (olFolder != null)
                        MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, olFolder.FullFolderPath, olFolder.Name);
                }
                catch (System.Exception e)
                {
                    try
                    {
                        if (olFolder != null)
                        {
                            string folderName = olFolder.Name;
                            if (MailListPresenter.HasPermissionsChangedFolder(folderName))
                            {
                                olFolder.Name = string.Format("{0}_1", folderName);
                                olFolder.Name = folderName;
                            }
                            MailListPresenter.BindViewCtlData(ListPart.axViewCtlEmailList, olFolder.FullFolderPath,
                                                              olFolder.Name);
                        }
                    }
                    catch
                    {
                        ICP.Framework.CommonLibrary.LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
                    }
                }
                finally
                {
                    MailUtility.ReleaseComObject(olFolder);
                }
            }
            finally
            {
                if (parameters.VisibleTreeNode != null)
                {
                    base.TopLevelControl.Text = string.Format("{0}{1}", parameters.VisibleTreeNode.Text, LocalData.IsEnglish ? "  -  Mail Center" : "  -  邮件中心");
                    if (ListPart.axViewCtlEmailList.ItemCount == 0)
                    {
                        DetailPart.Visible = false;
                        ToolPart.SetToolsEnable(false);
                    }
                    else
                    {
                        DetailPart.Visible = true;
                        ClientProperties.selectedChanged = true;
                        RootWorkItem.Commands[MailCenterCommandConstants.Command_SelectionChanged].Execute();
                        ToolPart.SetToolsEnable(true);
                    }
                }

                MailUtility.ReleaseComObject(olFolder);
            }
        }
        #endregion
        #region 渲染Loading动画
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.BeginInvoke((System.Action)(UpdateImage));
            e.Graphics.DrawImage(loadingImage, new Point(loadingImage.Size));
        }

        private void UpdateImage()
        {
            ImageAnimator.UpdateFrames(loadingImage);
        }

        private void BeginAnimate()
        {
            if (loadingImage != null)
            {
                pnlLoading.Visible = true;
                ImageAnimator.Animate(loadingImage, evtHandler);
            }
        }

        /// <summary>
        /// 重绘Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImageAnimate(Object sender, EventArgs e)
        {
            this.Invalidate();
        }

        public void StopAnimate()
        {
            if (this.InvokeRequired)
                this.Invoke((System.Action)(End));

            else
                End();
        }

        private void End()
        {
            pnlLoading.Visible = false;
            ImageAnimator.StopAnimate(loadingImage, evtHandler);
            this.Refresh();
        }

        #endregion


    }


}

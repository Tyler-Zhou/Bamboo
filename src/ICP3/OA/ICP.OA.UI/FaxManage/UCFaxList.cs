using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.OA.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.OA.ServiceInterface.DataObjects;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Controls;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Message.ServiceInterface;
using System.Threading;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList.Nodes;


namespace ICP.OA.UI.FaxManage
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class UCFaxList : XtraUserControl
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
        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }



        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICP.MailCenter.CommonUI.UCFaxPreview ucFaxPreview { get; set; }
        #endregion

        #region 本地属性

        public bool IsClose = false;
        public MessageWay Type { get; set; }

        public object DataSource
        {
            get { return bsMailList.DataSource; }
            set
            {
                if (value == null)
                    bsMailList.DataSource = typeof(List<Message.ServiceInterface.Message>);
                else
                    bsMailList.DataSource = value;

                bsMailList.ResetBindings(false);
            }
        }

        /// <summary>
        /// 收件箱ID
        /// </summary>
        Guid _inbox_FolderID = Guid.Empty;
        public Guid InBox_FolderID
        {
            get
            {
                if (_inbox_FolderID == Guid.Empty)
                {
                    MessageFolderList sendedFodler = FolderList.Find(folder => folder.Type == MessageFolderType.Inbox);
                    _inbox_FolderID = sendedFodler.ID;
                }

                return _inbox_FolderID;
            }
        }
        /// <summary>
        /// 收件箱名称
        /// </summary>
        string _inbox_FolderName = string.Empty;
        public string InBox_FolderName
        {
            get
            {
                if (_inbox_FolderName == string.Empty)
                {
                    MessageFolderList sendedFodler = FolderList.Find(folder => folder.Type == MessageFolderType.Inbox);
                    _inbox_FolderName = sendedFodler.Name;
                }

                return _inbox_FolderName;
            }
        }
        /// <summary>
        /// 当前选择的文件夹唉
        /// </summary>
        MessageFolderList CurrentFolder
        {
            get { return bsSend.Current as MessageFolderList; }
            set
            {
                MessageFolderList current = CurrentFolder;
                if (current != null) current = value;
            }
        }
        /// <summary>
        /// 用户消息文件夹列表
        /// </summary>
        public List<MessageFolderList> FolderList
        {
            get { return bsSend.DataSource as List<MessageFolderList>; }
        }
        /// <summary>
        /// 选择分公司下所有传真集合
        /// </summary>
        public List<FaxMessageObjects> DataList { get; set; }

        /// <summary>
        /// 当前选择传真信息
        /// </summary>
        ICP.Message.ServiceInterface.Message CurrentMessage
        {
            get { return bsMailList.Current as ICP.Message.ServiceInterface.Message; }
        }

        /// <summary>
        /// 当前选择公司配置信息
        /// </summary>
        public ConfigureObjects CurrentCompany
        {
            get { return bsReceive.Current as ConfigureObjects; }
            set
            {
                ConfigureObjects current = CurrentCompany;
                if (current != null) current = value;
            }
        }

        /// <summary>
        /// 当前选择公司ID
        /// </summary>
        public Guid CompanyID
        {
            get
            {
                return CurrentCompany.CompanyID;
            }
        }

        /// <summary>
        /// 当前用户的公司列表
        /// </summary>
        private List<ConfigureObjects> _UserCompanyList = null;
        public List<ConfigureObjects> UserCompanyList
        {
            get
            {
                if (_UserCompanyList == null)
                {
                    _UserCompanyList =
                    (from d in LocalData.UserInfo.UserOrganizationList
                     where d.Type != LocalOrganizationType.Department
                     select new ConfigureObjects
                     {
                         ID = d.ID,
                         ParentID = d.ParentID,
                         CompanyID = d.ID,
                         Code = d.Code,
                         Type = d.Type,
                         CompanyName = LocalData.IsEnglish ? d.EShortName : d.CShortName
                     }).ToList();
                }

                return _UserCompanyList;
            }
        }
        /// <summary>
        /// 用户默认公司配置信息
        /// </summary>
        ConfigureObjects _DefaultComanyInfo = null;
        public ConfigureObjects DefaultCompanyInfo
        {
            get
            {
                if (_DefaultComanyInfo == null)
                    _DefaultComanyInfo = UserCompanyList.Find(o => o.CompanyID == LocalData.UserInfo.DefaultCompanyID);

                return _DefaultComanyInfo;
            }
        }

        /// <summary>
        /// 选择的传真信息
        /// </summary>
        List<ICP.Message.ServiceInterface.Message> SelectedMessages
        {
            get
            {
                int[] rowIndexs = gvMailList.GetSelectedRows();
                if (rowIndexs == null || rowIndexs.Length == 0) return new List<ICP.Message.ServiceInterface.Message>();

                List<ICP.Message.ServiceInterface.Message> tagers = new List<ICP.Message.ServiceInterface.Message>();
                foreach (var item in rowIndexs)
                {
                    ICP.Message.ServiceInterface.Message ma = gvMailList.GetRow(item) as ICP.Message.ServiceInterface.Message;
                    if (ma != null) tagers.Add(ma);
                }

                return tagers;
            }
        }
        /// <summary>
        /// 传真列表集合
        /// </summary>
        List<ICP.Message.ServiceInterface.Message> CurrentMessageList
        {
            get
            {
                return bsMailList.DataSource as List<ICP.Message.ServiceInterface.Message>;
            }
        }
        private ICP.Message.ServiceInterface.Message CurrentMessageDetailInfo;
        #endregion

        #region 初始化

        public UCFaxList()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                UnhookEvent();
                this._DefaultComanyInfo = null;
                this._UserCompanyList = null;
                this.CurrentMessageDetailInfo = null;
                gcMain.DataSource = null;
                this.treeFolder.DataSource = null;
                this.treeReceive.DataSource = null;
                if (this.bsMailList != null)
                {
                    this.bsMailList.DataSource = null;
                    this.bsMailList = null;
                }
                if (this.bsReceive != null)
                {
                    this.bsReceive.DataSource = null;
                    this.bsReceive = null;
                }
                if (this.bsSend != null)
                {
                    this.bsSend.DataSource = null;
                    this.bsSend = null;
                }
                if (this.ucFaxPreview != null)
                {
                    this.ucFaxPreview = null;
                }
                if (this.ucFaxQuery != null)
                {
                    this.ucFaxQuery.Dispose();
                    this.ucFaxQuery = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                };
            };
            if (LocalData.IsEnglish == false && !LocalData.IsDesignMode)
            {
                SetCnText();
                //this.ucFaxQuery.Expand = false;
                this.ucFaxQuery.QueryEvent += OnQuery;
                this.ucFaxQuery.DisplayEvent += OnDisplay;
            }
        }



        private void OnDisplay(object sender, CommonEventArgs<bool> args)
        {
            if (args.Data)
            {
                this.pnlFastSearch.Visible = true;
                this.pnlSearchPart.Height = pnlFastSearch.Height + 33;
            }
            else
            {
                this.pnlFastSearch.Visible = false;
                this.pnlSearchPart.Height = this.ucFaxQuery.Height;
            }
        }

        private void SetCnText()
        {
            dpFolder.Text = "文件夹";


            colMailCC.Caption = "抄送";
            colMailFrom.Caption = "发件人";
            colMailTo.Caption = "收件人";
            colSubject.Caption = "主题";
            this.colState.Caption = "状态";
            this.colPriority.Caption = "优先级";
            barAddFolder.Caption = "新建文件夹(&A)";
            barClose.Caption = "关闭(&C)";
            barDelete.Caption = "删除(&D)";
            barDeleteFolder.Caption = "删除文件夹(&D)";
            barNewEmail.Caption = "新增(&N)";
            barPrint.Caption = "打印(&P)";
            barReNameFolder.Caption = "重命名(&R)";
            barRevert.Caption = "回复(&R)";
            barRevertAll.Caption = "回复全部(&A)";
            barSAndR.Caption = "接收/发送(&R)";
            barReturn.Caption = "打回(&B)";
            barTSend.Caption = "转发(&T)";
            btnFastSearch.Text = "查询（&F)";
            this.txtKeyWord.Properties.NullValuePrompt = "-- 标题,发件人,收件人 --";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            if (!LocalData.IsDesignMode)
            {
                InitControls();
                WaitCallback callback = data =>
                {
                    try
                    {
                        Init();
                    }
                    catch (System.Exception ex)
                    {
                        // LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                ThreadPool.QueueUserWorkItem(callback);
                HookEvent();
            }
        }

        private void HookEvent()
        {
            this.ClientMessageService.MessageSent += new EventHandler<MessageSendFinishEventArgs>(ClientMessageService_MessageSent);
            this.FaxClientService.FlagChanged += new EventHandler<MessageFlagChangeEventArgs>(FaxClientService_FlagChanged);
            this.FaxClientService.FolderSaved += new EventHandler<MessageFolderSaveFinishEventArgs>(FaxClientService_FolderSaved);
            this.FaxClientService.MessageFolderChanged += new EventHandler<ChangeMessageFolderEventArgs>(FaxClientService_MessageFolderChanged);
            this.FaxClientService.MessageStateChanged += new EventHandler<MessageStateChangeEventArgs>(FaxClientService_MessageStateChanged);
        }

        void FaxClientService_MessageStateChanged(object sender, MessageStateChangeEventArgs e)
        {
            List<ICP.Message.ServiceInterface.Message> states = e.Data;
            Guid currentFolderId = CurrentFolder.ID;
            List<Guid> ids = (from item in states select item.Id).ToList();

            List<ICP.Message.ServiceInterface.Message> messagesBelongsToCurrentFolder = states.FindAll(message => message.FolderId == currentFolderId);
            if (messagesBelongsToCurrentFolder.Count > 0)
            {  
                CurrentMessageList.RemoveAll(message => ids.Contains(message.Id));
                CurrentMessageList.AddRange(messagesBelongsToCurrentFolder);
                bsMailList.ResetBindings(false);
                this.bsMailList_PositionChanged(this.bsMailList, EventArgs.Empty);
            }
            else
            {
                List<Guid> currentMessageIds = (from item in CurrentMessageList select item.Id).ToList();
                if (currentMessageIds.Count <= 0)
                {
                    return;
                }
                messagesBelongsToCurrentFolder = states.FindAll(message => currentMessageIds.Contains(message.Id));
                if (messagesBelongsToCurrentFolder.Count > 0)
                {
                    CurrentMessageList.RemoveAll(message => ids.Contains(message.Id));
                   
                    bsMailList.ResetBindings(false);
                    this.bsMailList_PositionChanged(this.bsMailList, EventArgs.Empty);
                }
            }
        }
   
        private void UnhookEvent()
        {
            this.ClientMessageService.MessageSent -= new EventHandler<MessageSendFinishEventArgs>(ClientMessageService_MessageSent);
            this.FaxClientService.FlagChanged -= new EventHandler<MessageFlagChangeEventArgs>(FaxClientService_FlagChanged);
            this.FaxClientService.FolderSaved -= new EventHandler<MessageFolderSaveFinishEventArgs>(FaxClientService_FolderSaved);
            this.FaxClientService.MessageFolderChanged -= new EventHandler<ChangeMessageFolderEventArgs>(FaxClientService_MessageFolderChanged);
            this.FaxClientService.MessageStateChanged -= new EventHandler<MessageStateChangeEventArgs>(FaxClientService_MessageStateChanged);
            if (this.ucFaxQuery != null)
            {
                this.ucFaxQuery.QueryEvent -= OnQuery;
                this.ucFaxQuery.DisplayEvent -= OnDisplay;
            }
        }

        void FaxClientService_MessageFolderChanged(object sender, ChangeMessageFolderEventArgs e)
        {

        }

        void FaxClientService_FolderSaved(object sender, MessageFolderSaveFinishEventArgs e)
        {

        }

        void FaxClientService_FlagChanged(object sender, MessageFlagChangeEventArgs e)
        {

        }

        void ClientMessageService_MessageSent(object sender, MessageSendFinishEventArgs e)
        {

        }
        private delegate void LoadDataDelegate();
        private void Init()
        {
            if (this.InvokeRequired)
            {
                LoadDataDelegate loadDelegate = new LoadDataDelegate(InnerInit);
                this.BeginInvoke(loadDelegate);

            }
            else
            {
                InnerInit();
            }
        }
        private void InnerInit()
        {
            InitData();

        }

        private void InitData()
        {
            #  region 初始列表数据源
            try
            {

                ucFaxQuery.IsSelectedFaxHall = true;
                ucFaxQuery.Expand = false;
                this.pnlFastSearch.Visible = true;
                this.pnlSearchPart.Height = pnlFastSearch.Height + 33;
                List<MessageFolderList> folderList = FaxClientService.GetMessageFolderList(LocalData.UserInfo.LoginID);
                //MessageFolderList tager = folderList.Find(delegate(MessageFolderList item) { return item.Type == MessageFolderType.Root; });
                // if (tager != null) folderList.Remove(tager);
                bsSend.DataSource = folderList;
                this.ucFaxQuery.OnFolderChanged(this, new CommonEventArgs<MessageFolderList>(new MessageFolderList() { Name = DefaultCompanyInfo.CompanyName, ID = InBox_FolderID }));

                bsReceive.DataSource = UserCompanyList;
                SelectedByDefaultCompany();
                //MessageFolderList inBoxFolder = GetInboxFolder(folderList);
                //DevExpress.XtraTreeList.Nodes.TreeListNode inBoxNode = GetInBoxNode(inBoxFolder);
                //treeReceive.SetFocusedNode(inBoxNode);
                //SelectFolder();
                // List<ICP.Message.ServiceInterface.Message> messageList = FaxClientService.GetMessageListByFolderId(inBoxFolder.ID);
                //this.bsMailList.PositionChanged -= this.bsMailList_PositionChanged;
                //bsMailList.DataSource = messageList;
                //this.bsMailList.PositionChanged += this.bsMailList_PositionChanged;
                //if (messageList.Count > 0)
                //{
                //    this.gvMailList.SelectRow(0);
                //}                
                //ClientMessageService.Transfer(UserCompanyList, LocalData.UserInfo.DefaultCompanyID);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
            }

            #endregion
        }

        private void SelectFolder()
        {
            ucFaxQuery.IsSelectedFaxHall = false;
            this.ucFaxQuery.OnFolderChanged(this, new CommonEventArgs<MessageFolderList>(CurrentFolder));
            if (CurrentFolder == null)
            {

                this.DataSource = new List<Message.ServiceInterface.Message>();
                this.ucFaxPreview.Visible = false;
                return;
            }

            SetColByFolderType();

            if (Utility.GuidIsNullOrEmpty(CurrentFolder.ParentID))
            {
                return;
            }
            else
            {
                WaitCallback callback = data =>
                {
                    try
                    {
                        LoadDataBySelectFolder();
                    }
                    catch (System.Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                ThreadPool.QueueUserWorkItem(callback);
            }

        }

        private void SelectedByDefaultCompany()
        {
            TreeListNode node;
            LoopGetDefaultTreeNode(treeReceive.Nodes, out node);
            if (node == null)
                treeReceive.ExpandAll();
            else
            {
                node.ExpandAll();
                treeReceive.SetFocusedNode(node);
                System.Windows.Forms.Application.DoEvents();
                Received(LocalData.UserInfo.DefaultCompanyID, false);
            }
        }

        private void LoopGetDefaultTreeNode(TreeListNodes nodes, out TreeListNode setNode)
        {
            setNode = null;
            foreach (TreeListNode node in nodes)
            {
                ConfigureObjects item = treeReceive.GetDataRecordByNode(node) as ConfigureObjects;

                if (item == this.DefaultCompanyInfo)
                {
                    setNode = node;
                    return;
                }

                LoopGetDefaultTreeNode(node.Nodes, out setNode);
            }
        }

        private DevExpress.XtraTreeList.Nodes.TreeListNode GetInBoxNode(MessageFolderList inBoxFolder)
        {
            int count = treeReceive.Nodes.Count;
            DevExpress.XtraTreeList.Nodes.TreeListNode inBoxNode = null;
            for (int i = 0; i < count; i++)
            {
                MessageFolderList folder = treeReceive.GetDataRecordByNode(treeReceive.Nodes[i]) as MessageFolderList;
                if (folder == inBoxFolder)
                {
                    inBoxNode = treeReceive.Nodes[i];
                    break;
                }
                else
                    continue;
            }
            return inBoxNode;
        }

        private MessageFolderList GetInboxFolder(List<MessageFolderList> folderList)
        {
            return folderList.First(folder => folder.Type == MessageFolderType.Inbox);
        }

        private void InitControls()
        {
            if (splitMainList.Panel2.Controls.Count == 0)
            {
                ucFaxPreview.Dock = DockStyle.Fill;
                splitMainList.Panel2.Controls.Add(ucFaxPreview);
            }
            this.ucFaxPreview.ReadOnly = true;
            this.ucFaxPreview.Visible = false;

            this.Font = LocalData.IsEnglish ? new Font("Arial", 9) : new Font("宋体", 9);
            // colCreateDate.Caption = LocalData.IsEnglish ? "Receive Date" : "接收日期";
            colCreateDate_Time.Caption = LocalData.IsEnglish ? "Receive Time" : "接收时间";

            #region

            #region combobox

            rcmbPriority.Items.Add(new ImageComboBoxItem("Normal", MessagePriority.Normal, 0));
            rcmbPriority.Items.Add(new ImageComboBoxItem("High", MessagePriority.High, 1));
            rcmbPriority.Items.Add(new ImageComboBoxItem("Low", MessagePriority.Low, 2));

            rcmbAttachment.Items.Add(new ImageComboBoxItem(string.Empty, false, -1));
            rcmbAttachment.Items.Add(new ImageComboBoxItem(string.Empty, true, 0));

            rcmbFlag.Items.Add(new ImageComboBoxItem("Unread", MessageFlag.UnRead, 1));
            rcmbFlag.Items.Add(new ImageComboBoxItem("Read", MessageFlag.Read, 2));
            rcmbFlag.Items.Add(new ImageComboBoxItem("Reply", MessageFlag.Reply, 3));
            rcmbFlag.Items.Add(new ImageComboBoxItem("Transfer", MessageFlag.Transfer, 4));

            rcmbState.Items.Add(new ImageComboBoxItem("Sending", MessageState.Sending, 0));
            rcmbState.Items.Add(new ImageComboBoxItem("Success", MessageState.Success, 1));
            rcmbState.Items.Add(new ImageComboBoxItem("Failure", MessageState.Failure, 2));
            rcmbState.Items.Add(new ImageComboBoxItem("Draft", MessageState.Draft, 3));
            #endregion

            #endregion

            #region SearchInit
            #endregion

            RefreshBarItemEnabled();
        }

        void RefreshBarItemEnabled()
        {
            if (CurrentFolder == null || CurrentMessage == null)
            {
                barSAndR.Enabled = btnFastSearch.Enabled = barPrint.Enabled = barReturn.Enabled = barRevert.Enabled
                    = barRevertAll.Enabled = barTSend.Enabled = barDelete.Enabled = barAccepted.Enabled = false;
            }
            else if (GetMailTypeByCurrentFolder() == MessageFolderType.Inbox)
            {
                barSAndR.Enabled = barAccepted.Enabled = false;
                btnFastSearch.Enabled = barPrint.Enabled = barReturn.Enabled = barRevert.Enabled = barRevertAll.Enabled = barTSend.Enabled = barDelete.Enabled = true;
            }
            else
            {
                btnFastSearch.Enabled = true;
                if (GetMailTypeByCurrentFolder() == MessageFolderType.Outbox)
                {
                    barSAndR.Enabled = barAccepted.Enabled = barReturn.Enabled = barRevert.Enabled = barRevertAll.Enabled = barTSend.Enabled = false;
                    barPrint.Enabled = barDelete.Enabled = true;
                }
                else
                {
                    barSAndR.Enabled = barAccepted.Enabled = barReturn.Enabled = barRevert.Enabled = barRevertAll.Enabled = barTSend.Enabled = false;
                    barPrint.Enabled = barDelete.Enabled = true;
                }
            }
        }

        #endregion

        #region Search

        private void OnQuery(object sender, CommonEventArgs<MailQuery> args)
        {
            MailQuery mailquery = args.Data;
            Guid? companyID = null;
            if (mailquery.IsFaxHall)
            {
                companyID = this.CompanyID;
            }

            List<ICP.Message.ServiceInterface.Message> messageList = FaxClientService.GetMessageList(LocalData.UserInfo.LoginID
                                               , string.Empty, mailquery.FolderId
                                               , null
                                               , string.Empty
                                               , mailquery.From
                                               , mailquery.To
                                               , mailquery.Subject
                                               , mailquery.IncludeAttachment
                                               , null, null
                                               , mailquery.FromTime
                                               , mailquery.ToTime
                                               , companyID);
            this.bsMailList.PositionChanged -= this.bsMailList_PositionChanged;
            bsMailList.DataSource = messageList;
            InnerRebindData();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? string.Format("Total search {0} data.", messageList.Count) : string.Format("共查询到{0}条数据.", messageList.Count));
            this.bsMailList.PositionChanged += this.bsMailList_PositionChanged;
            this.bsMailList_PositionChanged(bsMailList, EventArgs.Empty);
            SetMessageDetailInfo(messageList);
            RefreshBarItemEnabled();
        }

        private void btnFastSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Search();
            }
        }
        /// <summary>
        /// 临时存放为进行搜索的传真列表集合
        /// </summary>
        public List<Message.ServiceInterface.Message> TempMessageList;

        private void Search()
        {
            if (txtKeyWord.Text != (LocalData.IsEnglish ? "-- Title,Sender,Recipient --" : "-- 标题,发件人,收件人 --") && !string.IsNullOrEmpty(txtKeyWord.Text.Trim()))
            {
                TempMessageList = CurrentMessageList;
                string keyWord = txtKeyWord.Text.Trim();
                List<Message.ServiceInterface.Message> results = CurrentMessageList.FindAll(o => o.Subject.Contains(keyWord) || o.SendFrom.Contains(keyWord) || o.SendTo.Contains(keyWord));
                if (results != null)
                {
                    SetMailListDataSourceAndShowStateMessage(results.Cast<Message.ServiceInterface.Message>().OrderByDescending(o => o.CreateDate).ToList(), (LocalData.IsEnglish ? string.Format("total search {0} data.", results.Count) : string.Format("共搜索到{0}条数据.", results.Count)));
                }
                //List<Message.ServiceInterface.Message> results = null;
                //if (ucFaxQuery.IsSelectedFaxHall)
                //{
                //    results = FaxClientService.GetFaxMessageListByFastSearch(LocalData.UserInfo.LoginID, null, (CompanyID == Guid.Empty ? LocalData.UserInfo.DefaultCompanyID : this.CompanyID), txtKeyWord.Text.Trim());
                //}
                //else
                //{
                //    results = FaxClientService.GetFaxMessageListByFastSearch(LocalData.UserInfo.LoginID, GetFocusedNodeFolderID(), null, txtKeyWord.Text.Trim());
                //}

                //SetMailListDataSourceAndShowStateMessage(results, (LocalData.IsEnglish ? string.Format("total search {0} data.", results.Count) : string.Format("共搜索到{0}条数据.", results.Count)));
            }
            else
            {
                SetMailListDataSourceAndShowStateMessage(TempMessageList == null ? CurrentMessageList : TempMessageList, "");
            }
        }

        #endregion

        #region Tree Drag Enter

        private void treeFolder_DragEnter(object sender, DragEventArgs e)
        {

            e.Effect = DragDropEffects.Copy;
        }

        private void treeFolder_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = treeFolder.PointToClient(new Point(e.X, e.Y));
            TreeListHitInfo tnhitInfo = treeFolder.CalcHitInfo(pt);
            if (tnhitInfo == null || tnhitInfo.Node == null) return;

            MessageFolderList dragToFolder = treeFolder.GetDataRecordByNode(tnhitInfo.Node) as MessageFolderList;
            if (dragToFolder == null) return;


            bool isFaxMessageObject = false; bool isNullObject = false; Guid FolderID = Guid.Empty;
            if (e.Data.GetFormats().Any(s => s.Contains(typeof(ICP.Message.ServiceInterface.FaxMessageObjects).ToString())))
            {
                isFaxMessageObject = true;
                ICP.Message.ServiceInterface.FaxMessageObjects entry = e.Data.GetData(typeof(ICP.Message.ServiceInterface.FaxMessageObjects)) as ICP.Message.ServiceInterface.FaxMessageObjects;
                if (entry == null)
                {
                    isNullObject = true;
                }
                else
                    FolderID = entry.FolderId;
                if (dragToFolder.ID != InBox_FolderID)
                    return;
            }
            if (e.Data.GetFormats().Any(s => s.Contains(typeof(ICP.Message.ServiceInterface.Message).ToString())))
            {
                Message.ServiceInterface.Message entry = e.Data.GetData(typeof(ICP.Message.ServiceInterface.Message)) as ICP.Message.ServiceInterface.Message;
                if (entry == null)
                {
                    isNullObject = true;
                }
                else
                    FolderID = entry.FolderId;
            }

            if (!isNullObject)
            {
                List<ICP.Message.ServiceInterface.Message> selectedMessages = SelectedMessages;
                if (selectedMessages == null || selectedMessages.Count == 0) return;


                this.treeFolder.FocusedNode = tnhitInfo.Node;
                if (dragToFolder.ID == FolderID && !isFaxMessageObject) return;

                List<Guid> ids = new List<Guid>(selectedMessages.Count);
                List<DateTime?> updateDates = new List<DateTime?>(selectedMessages.Count);
                List<DateTime?> faxUpdateDates = new List<DateTime?>(selectedMessages.Count);
                List<Guid?> folderIDs = new List<Guid?>(selectedMessages.Count);
                List<ReceiveFaxState> states = new List<ReceiveFaxState>(selectedMessages.Count);
                foreach (var item in selectedMessages)
                {
                    ids.Add(item.Id);
                    updateDates.Add(item.UpdateDate);
                    FaxMessageObjects info = GetFaxMessageInfo(item.Id);
                    faxUpdateDates.Add(info == null ? null : info.FaxUpdateDate);
                    folderIDs.Add(item.FolderId);
                    states.Add(ReceiveFaxState.Received);
                }
                try
                {
                    ManyResult results = null;
                    if (isFaxMessageObject)
                    {
                        results = ClientMessageService.ChangeFaxState(
                            ids.ToArray(),
                            folderIDs.ToArray(),
                            states.ToArray(),
                            updateDates.ToArray(),
                            faxUpdateDates.ToArray()
                            );
                    }
                    else
                    {
                        results = FaxClientService.ChangeMessageFolder(ids.ToArray(), dragToFolder.ID, updateDates.ToArray());
                    }
                }

                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                    return;
                }
                List<ICP.Message.ServiceInterface.Message> messageList = SelectedMessages;
                // GetChangeMessageFolderData(messageList, results);
                CurrentMessageList.RemoveAll(message => SelectedMessages.Contains(message));
                bsMailList.ResetBindings(false);
                return;
            }
            DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = e.Data.GetData(typeof(DevExpress.XtraTreeList.Nodes.TreeListNode)) as DevExpress.XtraTreeList.Nodes.TreeListNode;
            if (treeNode != null)
            {
                MessageFolderList folder = treeReceive.GetDataRecordByNode(treeNode) as MessageFolderList;
                if (folder.ParentID == dragToFolder.ID)
                    return;
                try
                {

                    ICP.Framework.CommonLibrary.Common.ManyResultData result = FaxClientService.SaveMessageFolder(folder.ID
                                                 , dragToFolder.ID
                                                 , folder.Name
                                                 , folder.Type
                                                 , folder.UpdateDate);
                    folder.ParentID = dragToFolder.ID;
                    BatchChangeFolderUpdateDate(result);
                    bsSend.ResetBindings(false);

                }
                catch (System.Exception ex)
                {
                    if (tempParentID != null) folder.ParentID = tempParentID;//回滚父ID

                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
        }

        private List<ICP.Message.ServiceInterface.Message> GetChangeMessageFolderData(List<ICP.Message.ServiceInterface.Message> messageList, ManyResult results)
        {
            int count = messageList.Count;
            for (int i = 0; i < count; i++)
            {
                SingleResult result = results.Items[i];
                messageList[i].FolderId = result.GetValue<Guid>("FolderID");
                messageList[i].UpdateDate = result.GetValue<DateTime>("UpdateDate");

            }
            return messageList;
        }
        #endregion

        #region GridView Do Drag
        GridHitInfo hitInfo = null;
        private void gcMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                hitInfo = gvMailList.CalcHitInfo(new Point(e.X, e.Y));
        }

        private void gcMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (hitInfo == null) return;
            if (e.Button != MouseButtons.Left) return;
            Rectangle dragRect = new Rectangle(new Point(
                hitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                hitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
            if (!dragRect.Contains(new Point(e.X, e.Y)))
            {
                object data = gvMailList.GetRow(hitInfo.RowHandle);
                if (data == null) return;
                gcMain.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        private void gcMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenuMessageList.ShowPopup(MousePosition);
        }

        #endregion

        #region bsPositionChanged
        //Folder
        private void bsFolder_PositionChanged(object sender, EventArgs e)
        {

        }

        private void InnerLoadDataBySelectFolder()
        {
            List<ICP.Message.ServiceInterface.Message> messageList = FaxClientService.GetMessageListByFolderId(CurrentFolder.ID);
            bsMailList.PositionChanged -= this.bsMailList_PositionChanged;
            this.DataSource = messageList;
            SetRowConditionStyle(messageList.Count);
            bsMailList.PositionChanged += this.bsMailList_PositionChanged;
            DefaultSelectedFirstRow(messageList.Count);
            SetMessageDetailInfo(messageList);
            RefreshBarItemEnabled();
        }

        private void DefaultSelectedFirstRow(int count)
        {
            if (count > 0)
            {
                gvMailList.SelectRow(0);
                this.bsMailList_PositionChanged(this.bsMailList, EventArgs.Empty);
            }
            else
            {
                this.ucFaxPreview.Visible = false;
            }
        }

        private void LoadDataBySelectFolder()
        {
            if (this.InvokeRequired)
            {
                LoadDataDelegate loadDelegate = new LoadDataDelegate(InnerLoadDataBySelectFolder);
                this.Invoke(loadDelegate);
            }
            else
            {
                InnerLoadDataBySelectFolder();
            }
        }

        private void SetMessageDetailInfo(List<ICP.Message.ServiceInterface.Message> messageList)
        {
            //int unReadMail = messageList.Where(entry => entry.Flag == MessageFlag.UnRead).Count();
            //if (LocalData.IsEnglish)
            //    labMailInfo.Text = "Fax:" + messageList.Count.ToString() + "  UnRead:" + unReadMail.ToString();
            //else
            //    labMailInfo.Text = "传真:" + messageList.Count.ToString() + "  未读:" + unReadMail.ToString();
        }

        private void SetColByFolderType()
        {
            MessageFolderType type = CurrentFolder.Type;
            if (type == MessageFolderType.UserDefined && Utility.GuidIsNullOrEmpty(CurrentFolder.ParentID) == false)
            {
                type = FindParentTypeByFolderParentID(CurrentFolder.ParentID.Value);
            }

            if (type == MessageFolderType.UserDefined) type = MessageFolderType.Inbox;

            if (CurrentFolder.Type == MessageFolderType.Inbox || CurrentFolder.Type == MessageFolderType.Deleted)
            {
                colMailFrom.Visible = true;
                colMailCC.Visible = colMailTo.Visible = false;
                // colCreateDate.Caption = LocalData.IsEnglish ? "Receive Date" : "接收日期";
                colCreateDate_Time.Caption = LocalData.IsEnglish ? "Receive Time" : "接收时间";
            }
            else if (CurrentFolder.Type == MessageFolderType.Drafts)
            {
                colMailFrom.Visible = false;
                colMailCC.Visible = colMailTo.Visible = true;
                // colCreateDate.Caption = LocalData.IsEnglish ? "Receive Date" : "接收日期";
                colCreateDate_Time.Caption = LocalData.IsEnglish ? "Receive Time" : "接收时间";
            }
            else if (CurrentFolder.Type == MessageFolderType.Outbox || CurrentFolder.Type == MessageFolderType.Sended)
            {
                colMailFrom.Visible = false;
                colMailCC.Visible = colMailTo.Visible = true;
                // colCreateDate.Caption = LocalData.IsEnglish ? "Send Date" : "发送日期";
                colCreateDate_Time.Caption = LocalData.IsEnglish ? "Send Time" : "发送时间";
            }

        }

        private MessageFolderType FindParentTypeByFolderParentID(Guid parentID)
        {
            List<MessageFolderList> fl = bsSend.DataSource as List<MessageFolderList>;
            if (Utility.GuidIsNullOrEmpty(parentID) || fl == null) return MessageFolderType.UserDefined;

            for (int i = 0; i < 10; i++)//最多支持十层
            {
                MessageFolderList tager = fl.Find(delegate(MessageFolderList item) { return item.ID == parentID; });
                if (tager == null) return MessageFolderType.UserDefined;

                if (Utility.GuidIsNullOrEmpty(tager.ParentID)) return tager.Type;
                else parentID = tager.ParentID.Value;
            }

            return MessageFolderType.UserDefined;
        }

        //MailList
        private void bsMailList_PositionChanged(object sender, EventArgs e)
        {
            int count = this.CurrentMessageList.Count;
            this.ucFaxPreview.Visible = count > 0;
            if (count > 0)
            {
                if (CurrentMessage != null && !Utility.GuidIsNullOrEmpty(CurrentMessage.Id))
                    CurrentMessageDetailInfo = ClientMessageService.Get(CurrentMessage.Id);
                SetMessageReadFlag();
            }
            else
            {
                CurrentMessageDetailInfo = null;
                barPrint.Enabled = barReturn.Enabled = barRevert.Enabled = barRevertAll.Enabled = barTSend.Enabled = barDelete.Enabled = false;
            }
            ucFaxPreview.BindData(CurrentMessageDetailInfo);

        }

        private void SetMessageReadFlag()
        {
            if (CurrentMessage == null || Utility.GuidIsNullOrEmpty(CurrentMessage.Id))
                return;
            if (CurrentMessage.Flag != MessageFlag.UnRead)
                return;

            Guid id = CurrentMessage.Id;
            WaitCallback callback = data =>
            {

                Guid currentId = (Guid)data;
                ICP.Message.ServiceInterface.Message messsage = GetMessage(currentId);
                if (messsage != null && messsage.Flag == MessageFlag.UnRead)
                {
                    SingleResultData result = ClientMessageService.ChangeFlag(CurrentMessage.Id, MessageFlag.Read, CurrentMessage.UpdateDate);
                    messsage = GetMessage(currentId);
                    if (messsage != null && messsage.Flag == MessageFlag.UnRead)
                    {
                        messsage.UpdateDate = result.UpdateDate;
                        messsage.Flag = MessageFlag.Read;
                        if (this.InvokeRequired)
                        {
                            LoadDataDelegate loadDelegate = new LoadDataDelegate(InnerRebindData);
                            this.Invoke(loadDelegate);
                        }
                        else
                            bsMailList.ResetBindings(false);
                    }
                }
            };
            ThreadPool.QueueUserWorkItem(callback, id);




        }

        private void InnerRebindData()
        {
            bsMailList.ResetBindings(false);
        }
        private ICP.Message.ServiceInterface.Message GetMessage(Guid id)
        {

            return CurrentMessageList.Find(message => message.Id == id);
        }

        #endregion

        #region Folder
        private void treeFolder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = treeReceive.CalcHitInfo(e.Location);
                if (hitInfo == null || hitInfo.Node == null) return;
                this.treeReceive.FocusedNode = hitInfo.Node;
            }
        }
        private void treeFolder_MouseClick(object sender, MouseEventArgs e)
        {
            SelectFolder();
            if (e.Button == MouseButtons.Right)
            {
                if (CurrentFolder.Type != MessageFolderType.UserDefined)
                    barDeleteFolder.Enabled = barReNameFolder.Enabled = false;
                else
                    barDeleteFolder.Enabled = barReNameFolder.Enabled = true;

                popupMenuFolder.ShowPopup(MousePosition);
            }
        }

        private void barDeleteFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteFolder();
        }

        private void DeleteFolder()
        {
            if (CurrentFolder == null || CurrentFolder.Type != MessageFolderType.UserDefined) return;
            List<Guid> folderIds = GetChildIdsById(FolderList, CurrentFolder.ID);
            Guid deletedFolderId = FolderList.Find(item => item.Type == MessageFolderType.Deleted).ID;
            bool isInDeletedFolder = IsInDeletedFolder(CurrentFolder, deletedFolderId);
            ManyResultData result = null;
            try
            {
                result = FaxClientService.RemoveFolder(CurrentFolder.ID, CurrentFolder.UpdateDate);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
                return;
            }
            if (!isInDeletedFolder)
            {

                CurrentFolder.ParentID = deletedFolderId;
            }
            else
            {
                FolderList.RemoveAll(item => folderIds.Contains(item.ID));
            }
            BatchChangeFolderUpdateDate(result);
            bsSend.ResetBindings(false);
        }
        private void BatchChangeFolderUpdateDate(ManyResultData result)
        {
            int count = result.ChildResults.Count;
            for (int i = 0; i < count; i++)
            {
                SingleResultData childResult = result.ChildResults[i];
                MessageFolderList folder = FolderList.Find(item => item.ID == childResult.ID);
                if (folder != null)
                {
                    folder.UpdateDate = childResult.UpdateDate;
                }
            }
        }

        private bool IsInDeletedFolder(MessageFolderList folder, Guid deletedFolderId)
        {
            if (folder == null || folder.ParentID == null) return false;
            MessageFolderList parentFolder = FolderList.Find(item => item.ID == folder.ParentID);
            if (parentFolder == null) return false;
            if (parentFolder.ID == deletedFolderId) return true;
            else
            {
                return IsInDeletedFolder(parentFolder, deletedFolderId);
            }
        }


        /// <summary>
        /// 获取所有子项(包括自身)ID
        /// </summary>
        List<Guid> GetChildIdsById(List<MessageFolderList> data, Guid currentId)
        {
            List<Guid> childIds = new List<Guid>();
            childIds.Add(currentId);

            while (true)
            {
                List<MessageFolderList> childs = data.FindAll(delegate(MessageFolderList item)
                { return item.ParentID != null && childIds.Contains(item.ParentID.Value) && childIds.Contains(item.ID) == false; });

                if (childs == null || childs.Count == 0)
                    break;
                else
                {
                    foreach (MessageFolderList item in childs)
                    {
                        childIds.Add(item.ID);
                    }
                }
            }
            return childIds;
        }


        private void barAddFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAddFolder frmNew = Workitem.Items.AddNew<frmAddFolder>();
            frmNew.SetSource(this.bsSend);
            DialogResult result = frmNew.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                bsSend.Add(frmNew.NewFolder);
                bsSend.ResetBindings(false);
            }
            frmNew.Dispose();
        }


        private void barReName_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReNameForm af = Workitem.Items.AddNew<ReNameForm>();
            af.SetSource(FolderList, CurrentFolder);
            DialogResult result = af.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                bsSend.ResetBindings(false);
            }
        }

        void treeFolder_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = CurrentFolder.Type != MessageFolderType.UserDefined;
        }


        #endregion

        #region drag Node

        Guid? tempParentID = null;
        private void treeFolder_BeforeDragNode(object sender, DevExpress.XtraTreeList.BeforeDragNodeEventArgs e)
        {
            if (CurrentFolder == null) return;
            tempParentID = null;
            if (Utility.GuidIsNullOrEmpty(CurrentFolder.ParentID) || CurrentFolder.Type != MessageFolderType.UserDefined)
            {
                e.CanDrag = false;
                return;
            }
            tempParentID = CurrentFolder.ParentID;
        }

        #endregion

        #region ReadMail

        private void gvMailList_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentMessage == null) return;
            ICP.Message.ServiceInterface.Message message = ClientMessageService.Get(CurrentMessage.Id);
            if (CurrentFolder.Type == MessageFolderType.Drafts)
            {

                if (ClientMessageService.ShowSendForm(message))
                {
                    try
                    {
                        ManyResult[] results = ClientMessageService.Save(message);
                        if (message.State == MessageState.Sending)
                        {
                            this.bsMailList.Remove(message);

                        }
                        else if (message.State == MessageState.Draft)
                        {
                            ManyResult result = results[0];
                            message.UpdateDate = result.Items[0].GetValue<DateTime>("UpdateDate");
                        }
                        this.bsMailList.ResetBindings(false);

                    }
                    catch (System.Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                    }

                }

            }
            else
            {
                ClientMessageService.ShowReadForm(message);
            }
        }



        public void RemoveMail(Guid mailID, DateTime? updateDate)
        {
            MessageFolderType type = Utility.GuidIsNullOrEmpty(CurrentFolder.ParentID) ? CurrentFolder.Type
                                : FindParentTypeByFolderParentID(CurrentFolder.ParentID.Value);

            RemoveEmail(type, new Guid[1] { mailID }, new List<DateTime?> { updateDate });
        }



        #endregion

        #region Delete

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteMail();
        }

        private void DeleteMail()
        {
            if (CurrentFolder == null || CurrentMessage == null || SelectedMessages == null) return;

            if (Utility.EnquireIsDeleteCurrentData())
            {
                MessageFolderType type = GetMailTypeByCurrentFolder();

                List<Guid> ids = new List<Guid>(SelectedMessages.Count);
                List<DateTime?> updateDates = new List<DateTime?>(SelectedMessages.Count);
                foreach (var item in SelectedMessages)
                {
                    ids.Add(item.Id);
                    updateDates.Add(item.UpdateDate);
                }
                RemoveEmail(type, ids.ToArray(), updateDates);
            }
        }

        private void RemoveEmail(MessageFolderType type, Guid[] ids, List<DateTime?> updateDates)
        {
            try
            {
                if (type != MessageFolderType.Deleted)
                {
                    MessageFolderList deletedFolder = GetFolderByFolderType(MessageFolderType.Deleted);
                    FaxClientService.ChangeMessageFolder(ids.ToArray(), deletedFolder.ID, updateDates.ToArray());
                }
                else
                {
                    ClientMessageService.Remove(ids.ToArray(), updateDates.ToArray());
                }
                RemoveData(ids);
            }
            catch (System.Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        MessageFolderList GetFolderByFolderType(MessageFolderType type)
        {
            if (type == MessageFolderType.UserDefined) return null;

            List<MessageFolderList> folderList = bsSend.DataSource as List<MessageFolderList>;
            if (folderList == null || folderList.Count == 0) return null;

            MessageFolderList tager = folderList.Find(delegate(MessageFolderList item) { return item.Type == type; });
            return tager;
        }

        #endregion

        #region Send Email


        MessageFolderType GetMailTypeByCurrentFolder()
        {
            MessageFolderType type = CurrentFolder.Type;
            if (CurrentFolder.Type == MessageFolderType.UserDefined)
            {
                type = Utility.GuidIsNullOrEmpty(CurrentFolder.ParentID) ? CurrentFolder.Type
                              : FindParentTypeByFolderParentID(CurrentFolder.ParentID.Value);
                if (type == MessageFolderType.User || type == MessageFolderType.System)
                    type = MessageFolderType.UserDefined;
            }
            return type;
        }
        private void ChangeMessageFolder(List<Guid> ids, ManyResult results, Guid targetFolderId)
        {
            List<ICP.Message.ServiceInterface.Message> messages = CurrentMessageList.FindAll(message => ids.Contains(message.Id));
            foreach (ICP.Message.ServiceInterface.Message message in messages)
            {
                SingleResult item = results.Items.Find(result => result.GetValue<Guid>("ID") == message.Id);
                message.UpdateDate = item.GetValue<DateTime>("UpdateDate");
                message.FolderId = targetFolderId;
            }
        }

        private void barNewEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FaxClientUtility.AddNewMail(CurrentFolder, FolderList, bsMailList);
        }

        private void barRevert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reply();

        }
        private void Reply()
        {
            string subject = (LocalData.IsEnglish ? "Re:" : "回复:") + CurrentMessage.Subject;
            ICP.Message.ServiceInterface.Message message = InitMessage(subject);
            ClientMessageService.Reply(CurrentMessage.Id, CurrentMessage.UpdateDate, message);
            // this.bsMailList.Insert(0, message);
            // this.bsMailList.ResetBindings(false);
        }

        private void barRevertAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reply();
        }
        private ICP.Message.ServiceInterface.Message InitMessage(string subject)
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            message.Subject = subject;
            message.SendTo = CurrentMessage.SendFrom;
            message.CC = CurrentMessage.CC;
            message.UserProperties = CurrentMessage.UserProperties;
            message.Type = MessageType.Fax;
            message.Body = CurrentMessage.Body;
            message.BodyFormat = CurrentMessage.BodyFormat;
            return message;
        }

        private void barTSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string subject = (LocalData.IsEnglish ? "Fw:" : "转发:") + CurrentMessage.Subject;
            ICP.Message.ServiceInterface.Message message = InitMessage(subject);
            message.SendTo = string.Empty;
            message.Body = CurrentMessageDetailInfo.Body;
            message.BodyFormat = CurrentMessageDetailInfo.BodyFormat;
            message.HasAttachment = CurrentMessageDetailInfo.HasAttachment;
            message.Attachments = CurrentMessageDetailInfo.Attachments;
            ClientMessageService.Forward(CurrentMessage.Id, CurrentMessage.UpdateDate, message);
            //this.bsMailList.Insert(0, message);
            // this.bsMailList.ResetBindings(false);
        }

        #endregion

        #region Event

        private void gvMailList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            MailMessageList data = this.gvMailList.GetRow(e.RowHandle) as MailMessageList;
            if (data == null) return;
            if (data.Flag == MessageFlag.UnRead)
            {
                Utility.SetUnReadStyle(e.Appearance);
            }
        }

        private void treeFolder_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            MessageFolderList folder = (MessageFolderList)treeReceive.GetDataRecordByNode(e.Node);
            if (folder == null) return;
            e.NodeImageIndex = (short)folder.Type;
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsClose = true;
            this.FindForm().Close();
        }
        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(ucFaxPreview.AttachmentClientPath))
            {
                ucFaxPreview.PrintPDF();
            }
        }

        public void RemoveData(Guid[] ids)
        {
            CurrentMessageList.RemoveAll(item => ids.Contains<Guid>(item.Id));
            bsMailList.ResetBindings(false);
            gvMailList.ClearSelection();
            if (CurrentMessageList.Count > 0)
                this.gvMailList.SelectRow(0);
        }

        void InsertData(Message.ServiceInterface.Message messageInfo)
        {
            bsMailList.Insert(0, messageInfo);
            bsMailList.ResetBindings(false);
        }
        private void treeReceive_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            ConfigureObjects data = treeFolder.GetDataRecordByNode(e.Node) as ConfigureObjects;
            if (data == null)
                return;

            if (data.Type == LocalOrganizationType.Company)
                e.Node.StateImageIndex = 1;
            else
                e.Node.StateImageIndex = 0;
        }
        void SetMailListDataSourceAndShowStateMessage(List<Message.ServiceInterface.Message> dataList, string message)
        {
            this.DataSource = dataList;
            SetRowConditionStyle(dataList.Count);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
        }

        private void SetRowConditionStyle(int count)
        {
            if (count > 0)
            {
                DevExpress.XtraGrid.StyleFormatCondition rowCondition;

                rowCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                rowCondition.Tag = "FaxStyle";
                rowCondition.Appearance.Font = new System.Drawing.Font(gvMailList.Appearance.Row.Font, System.Drawing.FontStyle.Bold);
                rowCondition.Appearance.Options.UseFont = true;
                rowCondition.Condition = FormatConditionEnum.Expression;
                rowCondition.ApplyToRow = true;
                rowCondition.Expression = GetBusinessExpression(MessageFlag.UnRead);
                this.gvMailList.FormatConditions.Add(rowCondition);

            }
        }
        private string GetBusinessExpression(MessageFlag flag)
        {
            return string.Format("[Flag] == '{0}'", flag);
        }

        private FaxMessageObjects GetFaxMessageInfo(Guid id)
        {
            return DataList.Find(o => o.Id == id);
        }

        void RefershData(Message.ServiceInterface.Message message, ManyResult[] results)
        {
            if (results != null && results.Length > 0)
            {
                if (results[0].Items != null && results[0].Items.Count > 0)
                {
                    message.Id = results[0].Items[0].GetValue<Guid>("ID");
                    message.UpdateDate = results[0].Items[0].GetValue<DateTime?>("UpdateDate");
                    InsertData(message);
                }
            }
        }

        Message.ServiceInterface.Message ConvertMessageInfo(Message.ServiceInterface.Message message)
        {
            if (message == null)
                return null;
            message.Type = MessageType.Fax;
            message.Way = MessageWay.Receive;
            message.FolderId = InBox_FolderID;
            message.FolderName = InBox_FolderName;
            // message.ReceiveFaxID = Guid.NewGuid();
            message.UpdateDate = DateTime.Now;

            return message;
        }

        #endregion

        #region 接收传真
        private void barSAndR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Received(this.CompanyID, true);
        }

        private void Received(Guid companyID, bool isFilterCompanyType)
        {
            WaitCallback callback = data =>
            {
                try
                {
                    InnerReceived(companyID, isFilterCompanyType);
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Received failure: " : "接收失败: ") + ex.Message);
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
        }

        int totalCount = 0;
        private void InnerReceived(Guid comanyID, bool isFilterCompanyType)
        {
            this.BeginInvoke((System.Action)(() =>
            {
                totalCount = 0;
                List<Message.ServiceInterface.Message> totalMessageList = new List<ICP.Message.ServiceInterface.Message>();
                //获取数据库退回和接收到的传真
                //if ((CurrentCompany != null && CurrentCompany.Type == LocalOrganizationType.Company) || !isFilterCompanyType)
                //{
                //    DataList = ClientMessageService.GetMessageInfoByCompanyID(comanyID);
                //    if (DataList != null && DataList.Count > 0)
                //    {
                //        totalCount = DataList.Count;
                //        totalMessageList = DataList.Cast<Message.ServiceInterface.Message>().OrderByDescending(o => o.CreateDate).ToList();
                //        SetMailListDataSourceAndShowStateMessage(totalMessageList, LocalData.IsEnglish ? string.Format("Receive {0} fax. ", totalCount) : string.Format("收到{0}封传真.", totalCount));
                //    }
                //    else
                //        SetMailListDataSourceAndShowStateMessage(totalMessageList, LocalData.IsEnglish ? "No have receive the fax." : "没有收到传真.");

                //    DefaultSelectedFirstRow(totalCount);
                //}
                //else
                this.DataSource = totalMessageList;

                SetToolBarEnabled(totalCount);
            }));
        }

        #endregion

        #region 退回
        private void barReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Return();
        }


        private void Return()
        {
            WaitCallback callback = data =>
            {
                try
                {
                    //InnerReturn();
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Return failure: " : "打回失败: ") + ex.Message);
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
        }
        private void InnerReturn()
        {
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                if (SelectedMessages != null && SelectedMessages.Count > 0)
                {
                    int count = SelectedMessages.Count;
                    List<Guid> ids = new List<Guid>(count);
                    List<ReceiveFaxState> states = new List<ReceiveFaxState>(count);
                    List<DateTime?> faxUpdateDates = new List<DateTime?>(count);
                    List<DateTime?> updateDates = new List<DateTime?>(count);
                    List<Guid?> folderIds = new List<Guid?>();
                    foreach (var item in SelectedMessages)
                    {
                        ids.Add(item.Id);
                        Message.ServiceInterface.Message info = ClientMessageService.GetMessageInfoById(item.Id);
                        if (info != null)
                            faxUpdateDates.Add(info.UpdateDate);
                        else
                            faxUpdateDates.Add(null);

                        states.Add(ReceiveFaxState.Return);
                        updateDates.Add(null);
                        folderIds.Add(null);
                    }
                    //更改oa.ReceiveFax表中state为退回
                    ClientMessageService.ChangeFaxState(ids.ToArray<Guid>(), folderIds.ToArray(), states.ToArray(), updateDates.ToArray(), faxUpdateDates.ToArray());
                    RemoveData(ids.ToArray<Guid>());
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "return success." : "打回成功.");
                }
            });
        }
        #endregion

        #region 签收
        private void barAccepted_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Accpeted();
        }
        private void Accpeted()
        {
            WaitCallback callback = data =>
            {
                try
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        if (SelectedMessages != null && SelectedMessages.Count > 0)
                        {
                            List<Guid> ids = new List<Guid>(SelectedMessages.Count);
                            List<Guid?> folderIds = new List<Guid?>(SelectedMessages.Count);
                            List<DateTime?> updateDates = new List<DateTime?>(SelectedMessages.Count);
                            List<DateTime?> faxUpdateDates = new List<DateTime?>(SelectedMessages.Count);
                            List<ReceiveFaxState> states = new List<ReceiveFaxState>(SelectedMessages.Count);
                            foreach (var item in SelectedMessages)
                            {
                                if (DataList != null && DataList.Count > 0)
                                {
                                    FaxMessageObjects faxMessageInfo = GetFaxMessageInfo(item.Id);
                                    if (faxMessageInfo != null)
                                    {
                                        ids.Add(item.Id);
                                        folderIds.Add(InBox_FolderID);
                                        updateDates.Add(null);
                                        faxUpdateDates.Add(faxMessageInfo.FaxUpdateDate);
                                        //做接收动作
                                        if (faxMessageInfo.FaxState == ReceiveFaxState.Return)
                                        {
                                            states.Add(ReceiveFaxState.Received);
                                        }
                                        else
                                        {
                                            //做发送动作
                                            states.Add(ReceiveFaxState.Return);
                                        }
                                    }

                                    ClientMessageService.ChangeFaxState(ids.ToArray(), folderIds.ToArray(), states.ToArray(), updateDates.ToArray(), faxUpdateDates.ToArray());
                                    RemoveData(ids.ToArray());
                                }
                            }
                        }
                    });
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Return failure: " : "打回失败: ") + ex.Message);
                }

            };
            ThreadPool.QueueUserWorkItem(callback);
        }

        #endregion

        #region 设置工具栏Enabled
        private void pnlSendFax_MouseClick(object sender, MouseEventArgs e)
        {
            Type = MessageWay.Send;
            SetToolBarEnabled(totalCount);
        }


        private void pnlReceive_MouseClick(object sender, MouseEventArgs e)
        {
            if (treeFolder.Nodes.Count == 0)
                return;
            Type = MessageWay.Receive;
            SetToolBarEnabled(totalCount);
        }
        private void treeReceive_MouseClick(object sender, MouseEventArgs e)
        {
            if (treeFolder.Nodes.Count == 0)
                return;
            this.ucFaxQuery.OnFolderChanged(this, new CommonEventArgs<MessageFolderList>(new MessageFolderList() { Name = CurrentCompany == null ? "" : CurrentCompany.CompanyName, ID = InBox_FolderID }));
            Type = MessageWay.Receive;
            Received(this.CompanyID, true);
            ucFaxQuery.IsSelectedFaxHall = true;            
        }

        void SetToolBarEnabled(int count)
        {
            if (this.Type == MessageWay.Send)
            {
                barAccepted.Enabled = false;
                barSAndR.Enabled = false;
                barRevert.Enabled = true;
                barRevertAll.Enabled = true;
                barTSend.Enabled = true;
                barPrint.Enabled = false;
                barReturn.Enabled = true;
                barDelete.Enabled = true;
                barClose.Enabled = true;
                btnFastSearch.Enabled = true;
            }
            else
            {
                barRevert.Enabled = false;
                barRevertAll.Enabled = false;
                barDelete.Enabled = false;
                barClose.Enabled = true;
                barTSend.Enabled = false;
                barReturn.Enabled = false;
                if (CurrentCompany.Type == LocalOrganizationType.Company)
                {
                    barSAndR.Enabled = true;
                    if (count == 0)
                        btnFastSearch.Enabled = barAccepted.Enabled = barPrint.Enabled = false;
                    else
                        btnFastSearch.Enabled = barAccepted.Enabled = barPrint.Enabled = true;
                }
                else
                {
                    btnFastSearch.Enabled = barAccepted.Enabled = barSAndR.Enabled = barPrint.Enabled = false;
                }
            }
        }

        private void treeReceive_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            SetToolBarEnabled(CurrentMessageList == null ? 0 : CurrentMessageList.Count);
        }

        #endregion
    }
}

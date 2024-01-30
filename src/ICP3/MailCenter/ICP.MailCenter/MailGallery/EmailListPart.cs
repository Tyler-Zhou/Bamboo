using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using System.Threading;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件列表类
    /// </summary>    
    [ToolboxItem(false)]
    [SmartPart]
    public partial class EmailListPart : UserControl
    {
        private object syncObj = new object();

        #region 服务
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

        public EmailFolderPart FolderPart
        {
            get
            {
                return SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }

        public EmailDetailPart DetailPart
        {
            get
            {
                return SmartParts.Get<EmailDetailPart>(MailCenterWorkSpaceConstants.EmailDetailPart);
            }
        }

        private EmailToolBarPart _ToolBarPart;
        public EmailToolBarPart ToolBarPart
        {
            get
            {
                if (_ToolBarPart == null)
                    _ToolBarPart = SmartParts.Get<EmailToolBarPart>(MailCenterWorkSpaceConstants.EmailToolBarPart);
                return _ToolBarPart;
            }
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public EmailListPart()
        {
            InitializeComponent();

            this.Disposed += delegate { DisposeComponent(); };
            MailUtility.SingleProcess(true);
        }


        public void DisposeComponent()
        {
            _SmartParts = null;
            _ToolBarPart = null;

            axViewCtlEmailList.Validating -= OnViewCtlEmailListValidating;
            axViewCtlEmailList.Enter -= OnViewCtlEmailListEnter;
            axViewCtlEmailList.SelectionChange -= OnSelectionChanged;
            axViewCtlEmailList = null;
            if (RootWorkItem != null) this.RootWorkItem.Items.Remove(this);
        }

        #endregion

        #region Method

        public void RefershCtl()
        {
            try
            {
                axViewCtlEmailList.SuspendLayout();
                axViewCtlEmailList.Invalidate(true);
                axViewCtlEmailList.Refresh();
                axViewCtlEmailList.ResumeLayout(false);
            }
            catch { }
        }

        #endregion
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                ClientProperties.ViewXmlTemp = axViewCtlEmailList.ViewXML = MailListPresenter.DefaultViewXml;
                //当视图控件无效时，鼠标点击其他区域（不包括视图控件本身区域），激发该事件
                axViewCtlEmailList.Validating += OnViewCtlEmailListValidating;
                //当视图控件无效时，鼠标点击该区域，就会激发该事件
                axViewCtlEmailList.Enter += OnViewCtlEmailListEnter;

            }
        }


        public void RemoveSelectionChangedHandler()
        {
            this.axViewCtlEmailList.SelectionChange -= OnSelectionChanged;
        }
        public void AddSelectionChangeHandler()
        {
            this.axViewCtlEmailList.SelectionChange += OnSelectionChanged;
        }


        #region 更新树文件夹数量

        [CommandHandler("Command_SetMailRead")]
        public void Command_SetMailRead(object sender, EventArgs e)
        {
            try
            {
                SetCurentMailReadAndRefreshFolderUnReadMailCount(
                    this.RootWorkItem.State[MailCenterCommandConstants.CurrentSelection]);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        /// <summary>
        /// 设置当前选择项已读，并刷新当前邮件所属文件夹的未读项数量
        /// </summary>
        void SetCurentMailReadAndRefreshFolderUnReadMailCount(object currentItem)
        {
            MailItem currentMail = currentItem as MailItem;
            if (currentMail != null && currentMail.UnRead)
            {
                currentMail.UnRead = false;
                if (currentMail.UnRead)
                {
                    currentMail.UnRead = false;//将邮件设为已读
                    currentMail.Save();
                }
                ClientProperties.FlagMailItemPropertyChanged = true;
                RefershFolderUnReadCount(currentMail);//刷新收件箱未读数量与文件夹的样式
            }
            else
            {
                ReportItem currentReport = currentItem as ReportItem;
                if (currentReport != null && currentReport.UnRead)
                {
                    currentReport.UnRead = false;
                    if (currentReport.UnRead)
                    {
                        currentReport.UnRead = false;
                        currentReport.Save();
                    }
                    ClientProperties.FlagMailItemPropertyChanged = true;
                    RefershFolderUnReadCount(currentReport);
                }
            }
        }

        ///
        ///备注：当为文件夹"垃圾邮件","草稿","收件箱"时，则不需要刷新文件夹数量
        ///
        public void RefershFolderUnReadCount(object currentItem)
        {
            MailItem currentMail = currentItem as MailItem;
            if (currentMail != null)
            {
                MAPIFolder folder = currentMail.Parent as MAPIFolder;
                if (!MailUtility.IsInDOJFolders(folder.Name))
                {
                    FolderPart.UpdateFolderUnreadMailCount(folder);//更新文件夹未读数量
                }
                currentMail = null;
            }
            else
            {
                ReportItem currentReport = currentItem as ReportItem;
                if (currentReport != null)
                {
                    MAPIFolder folder = currentReport.Parent as MAPIFolder;
                    if (!MailUtility.IsInDOJFolders(folder.Name))
                    {
                        FolderPart.UpdateFolderUnreadMailCount(folder);
                    }
                    currentReport = null;
                }
            }


        }

        #endregion

        #region 选择一封邮件
        /// <summary>
        /// 选中邮件改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(MailCenterCommandConstants.Command_SelectionChanged)]
        public void OnSelectionChanged(object sender, EventArgs e)
        {
            //拖拽邮件和选择节点时需要过滤重复的预览
            if (!IsDragDropLastOne() || ClientProperties.FlagMailItemPropertyChanged || !ClientProperties.selectedChanged)
            {
                ClientProperties.FlagMailItemPropertyChanged = false;
                return;
            }

            Selection olSelection = this.axViewCtlEmailList.Selection as Selection;
            if (olSelection != null && olSelection.Count > 0)
            {
                lock (syncObj)
                {
                    RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] = olSelection[1];
                    DetailPart.OnCurrentMailItemChanged(null, EventArgs.Empty);//当前邮件项发生变化时执行
                    MailUtility.ReleaseComObject(olSelection);
                }
            }
            else
                DetailPart.Visible = false;

        }

        #endregion

        #region 改变邮件的类别图标
        /// <summary>
        /// 改变邮件的类别图标
        /// </summary>
        [CommandHandler("Command_ChangeEmailCategories")]
        public void Command_ChangeEmailCategories(object sender, EventArgs e)
        {
            try
            {
                MailItem currentMail = RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as MailItem;
                if (currentMail != null)
                {
                    //判断当前邮件是否已经设置过标识
                    if (currentMail.Categories == null ||
                        (!(currentMail.Categories.Equals(ClientUtility.GreenCategory))))
                    {
                        currentMail.Categories = ClientUtility.GreenCategory;
                        currentMail.Save();
                        RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] = currentMail;
                    }
                }
                else
                {
                    ReportItem currentReportItem =
                            RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] as ReportItem;
                    if (currentReportItem != null)
                    {
                        if (currentReportItem.Categories == null ||
                            !currentReportItem.Categories.Equals(ClientUtility.GreenCategory))
                        {
                            currentReportItem.Categories = ClientUtility.GreenCategory;
                            currentReportItem.Save();
                            RootWorkItem.State[MailCenterCommandConstants.CurrentSelection] = currentReportItem;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ex.Message);
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 拖拽邮件到最后一封邮件时，才需要去显示最后一封邮件的detail
        /// </summary>
        /// <returns></returns>
        private bool IsDragDropLastOne()
        {
            return ClientProperties.isDragDropLastOne;
        }

        #region 选择一个文件夹

        [CommandHandler(MailCenterCommandConstants.Command_CurrentFolderChanged)]
        public void CurrentFolderChanged(object sender, EventArgs e)
        {
            OnCurrentFolderChanged(this.RootWorkItem.State[MailCenterCommandConstants.CurrentEntryID].ToString(), this.RootWorkItem.State[MailCenterCommandConstants.CurrentNodeText].ToString());
        }

        private void OnCurrentFolderChanged(string olEntryID, string nodeText)
        {
            MAPIFolder olFolder = null;
            #region 此处如果出现异常，则表示outlook出现异常，则重新启动outlook
            try
            {
                olFolder = MailListPresenter.GetFolderByEntryID(olEntryID);
                if (olFolder == null)
                {
                    MailUtility.ReStartOutLook();
                    try
                    {
                        olFolder = ClientUtility.CreateOutlookNameSpaceInstance()
                                                .GetFolderFromID(olEntryID, Type.Missing);
                        TreeViewPresenter.RegisterAllExpandedNodes(FolderPart.trvFolder, true);//给展开的文件夹注册事件
                    }
                    catch
                    {
                        ShowErrorDialog();
                    }
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            #endregion
            finally
            {
                try
                {
                    //备注:由于设置了ViewCtl ViewXml后，就会默认执行                                                      //OnSelectionChanged方法,但如果没有设置ViewXml，
                    //直接设置FolderPath,就不会执行OnSelectionChanged
                    ViewCtlBindData(olFolder, olFolder.Name);//控件绑定数据

                    ClientProperties.selectedChanged = true;
                    OnSelectionChanged(null, EventArgs.Empty);//选中邮件

                    string selectFolderName = string.Format("{0}{1}", nodeText,
                        LocalData.IsEnglish ? "  -  Mail Center" : "  -  邮件中心");
                    base.TopLevelControl.Text = selectFolderName;
                    if (this.axViewCtlEmailList.ItemCount == 0)//没有邮件时隐藏控件
                    {
                        HideControls();
                    }
                }
                catch (System.Exception ex)
                {
                    #region 有时候选择一个文件夹会出现莫名异常，经过测试，将文件夹名称重命名后，再将其还原原文件夹名称即可。

                    try
                    {
                        ViewCtlBindData(ClientProperties.folder_FullPath, olFolder.Name);
                    }
                    catch (System.Exception e)
                    {
                        try
                        {
                            string folderName = olFolder.Name;
                            if (MailListPresenter.HasPermissionsChangedFolder(folderName))
                            {
                                olFolder.Name = string.Format("{0}_1", folderName);
                                olFolder.Name = folderName;
                            }
                            ViewCtlBindData(ClientProperties.folder_FullPath, olFolder.Name);
                        }
                        catch
                        {
                        }
                    }
                    finally
                    {
                        ClientProperties.selectedChanged = true;
                    }

                    #endregion
                }
                finally
                {
                    MailUtility.ReleaseComObject(olFolder);
                }
            }
        }

        #endregion

        /// <summary>
        /// 弹出加载配置文件错误的对话框
        /// </summary>
        void ShowErrorDialog()
        {
            string errorMsg = LocalData.IsEnglish ? "Load error config file, it's need restart MailCenter!" : "加载配置文件出错，需要重新启动邮件中心!";
            DialogResult result = MessageBox.Show(errorMsg, LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                ClientProperties.IsNeedDeleteConfigFile = true;
                try
                {
                    RootWorkItem.Commands[MailCenterCommandConstants.Command_CloseMainForm].Execute();
                }
                catch { }
            }
        }

        #region 读取邮件视图列的配置文件
        /// <summary>
        /// 绑定视图控件数据
        /// </summary>
        /// <param name="folderFullPath"></param>
        /// <param name="folderName"></param>
        public bool ViewCtlBindData(MAPIFolder olFolder, string folderName)
        {
            bool isSetViewXml = false;
            string viewXml = MailListPresenter.OrderByViewXml(folderName);
            if (!string.IsNullOrEmpty(viewXml))
            {
                ClientProperties.selectedChanged = false;
                axViewCtlEmailList.ViewXML = viewXml;
                isSetViewXml = true;
            }

            ClientProperties.selectedChanged = false;
            ClientProperties.folder_FullPath = axViewCtlEmailList.Folder = olFolder.FullFolderPath;

            return isSetViewXml;
        }

        /// <summary>
        /// 绑定视图控件数据
        /// </summary>
        /// <param name="folderFullPath"></param>
        /// <param name="folderName"></param>
        public bool ViewCtlBindData(string folderFullPath, string folderName)
        {
            bool isSetViewXml = false;
            string viewXml = MailListPresenter.OrderByViewXml(folderName);
            if (!string.IsNullOrEmpty(viewXml))
            {
                ClientProperties.selectedChanged = false;
                axViewCtlEmailList.ViewXML = viewXml;
                isSetViewXml = true;
            }
            ClientProperties.selectedChanged = false;
            ClientProperties.folder_FullPath = axViewCtlEmailList.Folder = folderFullPath;
            return isSetViewXml;
        }

        public void ViewCtlBindData(string folderFullPath)
        {
            if (!string.IsNullOrEmpty(folderFullPath))
            {
                axViewCtlEmailList.Invoke((System.Action)delegate
                {
                    axViewCtlEmailList.Folder = ClientProperties.folder_FullPath;
                });
            }
        }

        #endregion

        void HideControls()
        {
            DetailPart.Visible = false;
            ToolBarPart.SetToolsEnable(false);
            ClientProperties.IsEnableToolBar = false;
        }

        #region 验证视图控件无效
        private void OnViewCtlEmailListEnter(object sender, EventArgs e)
        {
            ValidatedViewCtl(true, true);
        }

        private void ValidatedViewCtl(bool isShowLoadingForm, bool isStartProcess)
        {
            WaitCallback callback = (obj) =>
                this.axViewCtlEmailList.BeginInvoke((System.Windows.Forms.MethodInvoker)(
                () => InnerSetViewCtlFolderFullPath(obj, isStartProcess)));
            ThreadPool.QueueUserWorkItem(callback, isShowLoadingForm);
        }

        private void InnerSetViewCtlFolderFullPath(object isShowLoadingForm, bool isStartProcess)
        {
            if (IsKilledOutlook())
            {
                bool showLoadingForm = bool.Parse(isShowLoadingForm.ToString());
                ClientProperties.isOutlookViewCtlValidate = false;
                try
                {
                    if (isStartProcess)
                    {
                        axViewCtlEmailList.Visible = false;
                        MailUtility.StartProcess(showLoadingForm, ClientProperties.ErrorMessage);
                    }
                    else
                        MailUtility.HideOutLookApp(showLoadingForm, ClientProperties.ErrorMessage);
                    ClientUtility.GetOutlookNewNameSpace();
                    RefershCtl();
                    MailListPresenter.BindViewCtlData(axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                }
                catch { }
                finally
                {
                    try
                    {
                        MailListPresenter.BindViewCtlData(axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty); ;
                    }
                    catch
                    {
                        try
                        {
                            MAPIFolder olFolder = ClientUtility.CreateOutlookNameSpaceInstance().GetFolderFromID(ClientProperties.Folder_EntryID, Type.Missing);
                            string folderName = olFolder.Name;
                            if (MailListPresenter.HasPermissionsChangedFolder(folderName))
                            {
                                olFolder.Name = string.Format("{0}_1", folderName);
                                olFolder.Name = folderName;
                            }

                            MailListPresenter.BindViewCtlData(axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                        }
                        catch { }
                    }
                    finally
                    {
                        axViewCtlEmailList.Visible = true;
                        ClientProperties.selectedChanged = true;
                        TreeViewPresenter.RegisterAllExpandedNodes(FolderPart.trvFolder, true);
                    }
                }
            }
        }

        [CommandHandler("Command_ValidateViewCtl")]
        public void Command_ValidateViewCtl(object sender, EventArgs e)
        {
            ValidatedViewCtl(false, false);
        }

        private void OnViewCtlEmailListValidating(object sender, CancelEventArgs e)
        {
            ValidatedViewCtl(false, true);
        }

        /// <summary>
        /// 当读取AxViewCtl控件属性时，没有出现ComException组件异常时，表示没有将outlook关闭，反之关闭
        /// </summary>
        /// <returns></returns>
        private bool IsKilledOutlook()
        {
            bool isKilled = false;
            try
            {

                if (axViewCtlEmailList.ItemCount == -1)
                {
                    isKilled = false;
                }
            }
            catch
            {
                isKilled = true;
            }

            return isKilled;
        }

        #endregion

        #region 邮件归档
        /// <summary>
        /// 邮件归档：将已关联邮件移动至已关联文件夹(已关联/IsRelation)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailArchiving")]
        public void Command_EmailArchiving(object sender, EventArgs e)
        {
            //1.显示归档窗体并自动执行归档操作
            FrmEmailArchiving frmArchiving = new FrmEmailArchiving();
            frmArchiving.Show();
            string defaultEntryID = "";
            string fullPath = "";
            frmArchiving.EmailArchiving(ref defaultEntryID, ref fullPath);
            //2.定位到默认账户的归档目录
            TreeNode RelationNode = null;
            TreeViewPresenter.FindTreeNodeByTag(FolderPart.trvFolder.Nodes, defaultEntryID, ref RelationNode);
            FolderPart.trvFolder.SelectedNode = RelationNode;
            ClientProperties.CurrentSelectedNode = RelationNode;
            ClientProperties.folder_FullPath = axViewCtlEmailList.Folder = fullPath;
        }
        #endregion

        #region IKeyboardEventHandleService 成员


        void Handle(ICP.Common.ServiceInterface.KeyboardEventInfo eventInfo)
        {
            ICP.Framework.CommonLibrary.LogHelper.SaveLog(string.Format("CTR:{0},KeyCode:{1},Selection==null:{2}", eventInfo.Ctrl.ToString(), eventInfo.KeyCode.ToString(), (this.axViewCtlEmailList.Selection == null).ToString()));

            if (eventInfo.Ctrl && eventInfo.KeyCode == Keys.C.ToString() && this.axViewCtlEmailList.Selection != null)
            {
                string saveFileName = ClientUtility.GetCurrentMailSubject();
                string basePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
                string savePath = System.IO.Path.Combine(basePath, saveFileName + ".msg");
                System.IO.Directory.CreateDirectory(basePath);
                MailUtility.SaveMailItemAs(ClientProperties.CurrentMailItem, savePath);
                System.Collections.Specialized.StringCollection stringCollection = new System.Collections.Specialized.StringCollection();
                stringCollection.Add(savePath);
                AddStringCollectionToClipboard(stringCollection);
            }

        }

        private void AddStringCollectionToClipboard(System.Collections.Specialized.StringCollection stringCollection)
        {
            try
            {
                MailListPresenter.CopyStringCollection(stringCollection);
            }
            catch (System.Exception ex)
            {
                try
                {
                    MailListPresenter.CopyStringCollection(stringCollection);
                }
                catch { }
            }
        }


        #endregion
    }
}

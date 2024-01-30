using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using System.Threading;
using ICP.Framework.CommonLibrary.Helper;
using System.IO;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Business.Common.UI.Document
{
    /// <summary>
    /// 用于业务界面的文档列表自定义用户控件
    /// <remarks>只显示文件名</remarks>
    /// </summary>
    public partial class UCBusinessDocumentList : XtraUserControl
    {

        //增加文档列表
        private List<DocumentInfo> listAddedDocuments = new List<DocumentInfo>();
        //移除文档列表
        private List<DocumentInfo> listDeletedDocuments = new List<DocumentInfo>();
        //private List<DocumentInfo> listOriginal = new List<DocumentInfo>();

        private string[] tempFileName;
        /// <summary>
        /// 
        /// </summary>
        //private bool isSelectDocumentType = false;
        /// <summary>
        /// 上传附件界面选择的文档类型
        /// </summary>
        public DocumentType? currentSelectDocumentType { get; set; }
        /// <summary>
        /// 上传附件界面默认覆盖重复的文件名称
        /// </summary>
        public bool CheckedOverride = true;
        /// <summary>
        /// 是否有初始化文档类型
        /// </summary>
        private bool isInitDocument = false;

        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem WorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// 客户端文档通知服务
        /// </summary>
        [ServiceDependency]
        public DocumentNotifyClientService DocumentNotifyClientService { get; set; }

        #region 客户端上传文件服务
        /// <summary>
        /// 
        /// </summary>
        private IClientFileService clientFileService = null;
        /// <summary>
        /// 客户端上传文件服务
        /// </summary>
        private IClientFileService ClientFileService
        {
            get
            {
                if (clientFileService != null)
                    return clientFileService;
                else
                {
                    clientFileService = ServiceClient.GetClientService<IClientFileService>();
                    return clientFileService;
                }

            }
        }
        #endregion

        /// <summary>
        /// 业务查询服务
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get
            {
                return ServiceClient.GetService<IBusinessQueryService>();
            }
        }

        /// <summary>
        /// 当前选中文档
        /// </summary>
        public DocumentInfo CurrentDocument
        {
            get
            {
                if (gridViewDocument.SelectedRowsCount <= 0)
                    return null;
                int currentIndex = gridViewDocument.GetFocusedDataSourceRowIndex();
                DocumentInfo currentDocument = DataSource[currentIndex];
                return currentDocument;

            }
        }

        /// <summary>
        /// 是否有修改
        /// </summary>
        public bool IsChanged { get; set; }

        #region 是否允许保存附件
        private bool _IsAllowSaveData = true;
        /// <summary>
        /// 是否允许保存附件
        /// </summary>
        public bool IsAllowSaveData
        {
            get { return _IsAllowSaveData; }
            set { _IsAllowSaveData = value; }
        }
        #endregion

        /// <summary>
        /// 当前文档列表
        /// </summary>
        public List<DocumentInfo> CurrentDocumentList
        {
            get { return DataSource; }
        }
        /// <summary>
        /// 当前文档列表
        /// </summary>
        public List<DocumentInfo> CurrentDocumentList_New
        {
            get;
            set;
        }

        /// <summary>
        /// 当前数据源
        /// </summary>
        public List<DocumentInfo> DataSource
        {
            get
            {
                if (bindingSource == null)
                {
                    return new List<DocumentInfo>();
                }
                else
                    return bindingSource.DataSource as List<DocumentInfo>;
            }
            set
            {
                if (value == null)
                    bindingSource.DataSource = typeof(DocumentInfo);
                else
                    bindingSource.DataSource = value;
                ResetDocumentBindings();
                gridControlDocument.DataSource = bindingSource;
            }
        }

        /// <summary>
        /// 业务操作上下文类
        /// </summary>
        public BusinessOperationContext Context
        {
            get;
            set;
        }
        /// <summary>
        /// 当前选中行
        /// </summary>
        public DocumentInfo CurrentRow
        {
            get { return bindingSource.Current as DocumentInfo; }
        }
        #region 文档类型集
        private List<CustomEnumInfo> _documentTypes;
        /// <summary>
        /// 文档类型集
        /// </summary>
        public List<CustomEnumInfo> DocumentTypes
        {
            get
            {
                return _documentTypes ??
                    (_documentTypes = EnumGetter.Current[typeof(DocumentType), true, Context == null ? OperationType.Unknown : Context.OperationType]);
            }
            set { _documentTypes = value; }
        }
        #endregion

        #region 文档扩展名
        private string[] _fileExtensions = null;
        /// <summary>
        /// 支持上传文档的扩展名
        /// </summary>
        public string[] FileExtensions
        {
            get { return _fileExtensions ?? (_fileExtensions = CommonUIUtility.FilterFilesExtension()); }
        }
        #endregion

        /// <summary>
        /// 开始上传事件
        /// </summary>
        public EventHandler UploadStartEventHandler;
        /// <summary>
        /// 上传失败事件
        /// </summary>
        public EventHandler<CommonEventArgs<string>> UploadFailedEventHandler;
        /// <summary>
        /// 上传成功事件
        /// </summary>
        public EventHandler UploadSuccessEventHandler;
        /// <summary>
        /// 数据绑定完成事件
        /// </summary>
        public EventHandler DataBindCompleteEventHandler;
        /// <summary>
        /// 文档存在事件
        /// </summary>
        public EventHandler<CommonEventArgs<string>> DocumentExistsEventHandler;

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="showMessage">显示消息</param>
        /// <returns></returns>
        public bool ValidateDate(bool showMessage)
        {
            if (CurrentDocumentList == null ||
                CurrentDocumentList.Count <= 0)
                return false;
            else
            {
                bool validated = !CurrentDocumentList.Any(item => item.DocumentType.HasValue == false);
                if (!validated)
                {
                    if (showMessage)
                        XtraMessageBox.Show(LocalData.IsEnglish ? "please set the attachment document type！"
                                                                   : "需要设置附档的单证类型！");
                }

                return validated;
            }
        }

        /// <summary>
        /// 刷新数据源
        /// </summary>
        public void ResetDocumentBindings()
        {
            bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// 保存
        /// <remarks>根据实际情况，进行新增和删除动作</remarks>
        /// </summary>
        public bool Save()
        {
            if (listAddedDocuments.Count <= 0 && listDeletedDocuments.Count <= 0)
                return false;

            if (UploadStartEventHandler != null)
            {
                UploadStartEventHandler(this, EventArgs.Empty);
            }

            if (Context == null || !IsAllowSaveData)
                return false;

            if (!CheckFileName(listAddedDocuments))
            {
                string message = LocalData.IsEnglish ? "The document name contains illegal characters，Please change the name and then upload it！" : "文档名称包含非法字符，请修改名称后重新上传！";
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                return false;
            }
            listAddedDocuments.ForEach(item => CommonUIUtility.InitDocumentInfo(item, Context));

            List<string> filePaths = (from document in listAddedDocuments
                                      select document.OriginalPath).ToList();
            List<Guid> listDeleteIds = (from document in listDeletedDocuments
                                        select document.Id).ToList();
            List<DateTime?> listUpdateDates = (from document in listDeletedDocuments
                                               select document.UpdateDate).ToList();
            try
            {
                ClientFileService.Save(listAddedDocuments, filePaths, listDeleteIds, listUpdateDates);
            }
            catch (Exception ex)
            {
                TriggerUploadFailedEvent(ex.Message);
                return false;
                //LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
            return true;
        }

        private bool CheckFileName(List<DocumentInfo> doclist)
        {
            List<Guid> returnList = new List<Guid>();
            Regex rx = new Regex(@"[0-9a-zA-Z_&#@() -]");
            foreach (DocumentInfo filepath in doclist)
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(filepath.Name);
                if (filename == null) continue;
                foreach (char charname in filename)
                {
                    if (!rx.IsMatch(charname.ToString()) && !CommonUIUtility.IsContainsChinese(charname.ToString()))
                    {
                        //string strTip = LocalData.IsEnglish ? "The document name contains illegal characters，Please change the name and then upload it！" : "文档名称包含非法字符，请修改名称后重新上传！";
                        //returnList.Add(filepath.Id);

                        return false;
                    }
                }
            }
            return true;
        }

        private void TriggerUploadFailedEvent(string errorMessage)
        {
            if (UploadFailedEventHandler != null)
            {
                UploadFailedEventHandler(this, new CommonEventArgs<string>(errorMessage));
            }
        }

        private void InnerBindData(List<DocumentInfo> documents)
        {
            DataSource = documents;

            ResetTempDocumentList();
        }

        private void ResetTempDocumentList()
        {
            listAddedDocuments.Clear();
            listDeletedDocuments.Clear();
        }

        private delegate void BindDataDelegate(List<DocumentInfo> documents);
        public void BindData(BusinessOperationContext context)
        {
            Context = context;
            WaitCallback callback = (operationContext) =>
            {
                try
                {
                    ClientHelper.SetApplicationContext();
                    BusinessOperationContext parameter = operationContext as BusinessOperationContext;
                    List<DocumentInfo> documents = ClientFileService.GetBusinessDocumentList(parameter);
                    InnerBindData(documents);

                    if (DataBindCompleteEventHandler != null)
                    {
                        DataBindCompleteEventHandler(this, EventArgs.Empty);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                    TriggerUploadFailedEvent("Upload Attachment: " + ex.Message);
                    //LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }

            };
            ThreadPool.QueueUserWorkItem(callback, context);

        }
        /// <summary>
        /// 
        /// </summary>
        public UCBusinessDocumentList()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                Locale();

                Load += delegate
                {
                    DocumentNotifyClientService.DocumentUploadFailed += OnDocumentUploadFailed;
                    gridViewDocument.MouseDown += OnGridViewMouseDown;
                    gridViewDocument.FocusedRowChanged += OnGridViewFocusesRowChanged;
                };
                Disposed += delegate
                {
                    listAddedDocuments = null;
                    listDeletedDocuments = null;
                    //listOriginal = null;
                    if (gridControlDocument != null)
                    {
                        gridControlDocument.DataSource = null;
                        gridControlDocument.Dispose();
                        gridControlDocument = null;
                    }
                    if (bindingSource != null)
                    {
                        bindingSource.DataSource = null;
                    }
                    bindingSource = null;
                    Context = null;
                    clientFileService = null;
                    DocumentTypes = null;
                    DocumentNotifyClientService.DocumentUploadFailed -= OnDocumentUploadFailed;
                    gridViewDocument.MouseDown -= OnGridViewMouseDown;
                    gridViewDocument.FocusedRowChanged -= OnGridViewFocusesRowChanged;
                    UploadStartEventHandler = null;
                    UploadSuccessEventHandler = null;
                    UploadFailedEventHandler = null;
                    DataBindCompleteEventHandler = null;
                    DocumentExistsEventHandler = null;
                    if (WorkItem != null)
                    {
                        WorkItem.Items.Remove(this);
                    }
                };
            }
        }

        /// <summary>
        /// 设置附件类型
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="isDirty"></param>
        public void UpdateDataListDocumentType(DocumentType? documentType, bool isDirty)
        {
            currentSelectDocumentType = documentType;
            if (isDirty)
                //找出未保存的附件，将设置其文档类型
                UpgradeDataSource(false, documentType);
        }

        private void UpgradeDataSource(bool setDirty, DocumentType? documentType)
        {
            if (DataSource == null)
                return;

            List<DocumentInfo> dirtyDocuments = DataSource.FindAll(item => item.IsDirty);
            if (dirtyDocuments != null && dirtyDocuments.Count > 0)
            {
                if (setDirty)
                    Array.ForEach(dirtyDocuments.ToArray(), item => item.IsDirty = false);
                else
                    Array.ForEach(dirtyDocuments.ToArray(), item => item.DocumentType = documentType);
                ResetDocumentBindings();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpgradeDirtyDataSource()
        {
            UpgradeDataSource(true, null);
        }


        private void OnDocumentUploadFailed(UploadFailedMessage message)
        {
            if (IsDisposed || FindForm().IsDisposed)
            {
                return;
            }
            List<DocumentInfo> documentsFailed = DataSource.Where(document => message.DocumentIds.Contains(document.Id)).ToList();
            if (documentsFailed != null)
            {
                DataSource.RemoveAll(document => documentsFailed.Contains(document));
                ResetDocumentBindings();
                TriggerUploadFailedEvent(message.ErrorMessage);
            }
        }
        private void OnDocumentUploadSucessed(DocumentInfo[] documents)
        {
            if (IsDisposed || FindForm().IsDisposed)
            {
                return;
            }
            Guid[] ids = (from document in documents
                          select document.Id).ToArray();
            DataSource.RemoveAll(document => ids.Contains(document.Id));
            for (int i = 0; i < documents.Length; i++)
            {
                documents[i].State = UploadState.Successed;
            }
            DataSource.AddRange(documents);
            ResetDocumentBindings();
            ResetTempDocumentList();
            if (UploadSuccessEventHandler != null)
            {
                UploadSuccessEventHandler(this, EventArgs.Empty);
            }
        }


        private void Locale()
        {
            if (LocalData.IsEnglish)
            {
                tsmiAdd.Text = "Add";
                tsmiDelete.Text = "Delete";
                tsmiOpen.Text = "Open";
                tsmiOpenWith.Text = "Open With...";
                tsmiSaveAs.Text = "Save As";
            }
            else
            {
                gridColumnType.Caption = "单证类型";
                gridColumnName.Caption = "文件";
            }
        }


        private bool InitUploadSubItems()
        {
            if (!tsmiAdd.HasDropDownItems)
            {
                //List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ICP.DataCache.ServiceInterface.DocumentType>> documentTypes = EnumHelper.GetEnumValues<ICP.DataCache.ServiceInterface.DocumentType>(LocalData.IsEnglish);
                if (DocumentTypes != null && DocumentTypes.Count > 0)
                {
                    int count = DocumentTypes.Count;
                    for (int i = 0; i < count; i++)
                    {
                        CustomEnumInfo info = DocumentTypes[i];
                        ToolStripMenuItem barItem = new ToolStripMenuItem();
                        if (info.HasChildNodes)
                        {
                            if (LocalData.IsEnglish)
                            {
                                barItem.Text = info.eCaption;
                            }
                            else
                            {
                                barItem.Text = info.cCaption;
                            }
                            tsmiAdd.DropDownItems.Add(barItem);

                            int subCount = info.ChildrenNodes.Count;
                            for (int child = 0; child < subCount; child++)
                            {
                                ToolStripMenuItem subBarItem = new ToolStripMenuItem();
                                ChildrenEnumInfo subEnumInfo = info.ChildrenNodes[child];
                                if (LocalData.IsEnglish)
                                    subBarItem.Text = subEnumInfo.Etip;
                                else
                                    subBarItem.Text = subEnumInfo.Ctip;

                                subBarItem.Tag = subEnumInfo.Caption;
                                subBarItem.Click += OnUploadItemClick;
                                barItem.DropDownItems.Add(subBarItem);
                                subBarItem = null;
                                subEnumInfo = null;
                            }
                        }
                        else
                        {
                            ChildrenEnumInfo childrenEnumInfo = info.ChildrenNodes == null ? info : info.ChildrenNodes[0];
                            if (LocalData.IsEnglish)
                            {
                                barItem.Text = childrenEnumInfo.Etip;
                            }
                            else
                            {
                                barItem.Text = childrenEnumInfo.Ctip;
                            }

                            barItem.Tag = childrenEnumInfo.Caption;

                            barItem.Click += OnUploadItemClick;
                            tsmiAdd.DropDownItems.Add(barItem);
                            childrenEnumInfo = null;
                        }

                        barItem = null;
                        info = null;
                    }
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 上传SI附件
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="promptRepeatFile"></param>
        /// <param name="documentType"></param>
        public bool AddDocuments(List<string> filePaths, List<string> previewPaths, bool? promptRepeatFile, DocumentType? documentType)
        {
            if (filePaths == null || filePaths.Count <= 0)
                return false;
            try
            {
                return InnerUpload(filePaths.ToArray(), previewPaths, promptRepeatFile, documentType);
            }
            catch (Exception ex)
            {
                TriggerUploadFailedEvent(ex.Message);
            }

            return true;
        }

        /// <summary>
        /// 设置是否启用右键菜单
        /// </summary>
        public void SetContextMenuItemEnabled()
        {
            if (CurrentDocumentList == null || CurrentDocumentList.Count == 0)
            {
                Enabled(false);
            }
            else
            {
                Enabled(true);
            }
        }

        private void Enabled(bool enabled)
        {
            tsmiDelete.Enabled =
                tsmiOpen.Enabled = tsmiOpenWith.Enabled = tsmiSaveAs.Enabled = enabled;
        }

        void OnUploadItemClick(object sender, EventArgs e)
        {
            //if (this.Context == null)
            //    return;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            //ICP.DataCache.ServiceInterface.DocumentType documentType = (ICP.DataCache.ServiceInterface.DocumentType)item.Tag;
            var documentType = (DocumentType)Enum.Parse(typeof(DocumentType), item.Tag.ToString(), false);
            String[] filePaths = CommonUIUtility.SelectFilesToUpload();
            try
            {
                InnerUpload(filePaths, null, CheckedOverride, documentType);
            }
            catch (Exception ex)
            {
                TriggerUploadFailedEvent(ex.Message);
            }

        }

        /// <summary>
        /// 数据源是否有值
        /// </summary>
        /// <returns></returns>
        private bool HasDataSource()
        {
            if (DataSource == null || DataSource.Count <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 上传附件 查找是否有保存过该附件，如果有保存，需要提取上次保存的DocumentType
        /// </summary>
        /// <param name="newDocumentType"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private DocumentType? GetDocumentType(DocumentType? newDocumentType, string fileName)
        {
            DocumentType? docType = newDocumentType;
            if (docType == null)
            {
                if (HasDataSource())
                {
                    DocumentInfo documentInfo = DataSource.Find(item => item.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));
                    if (documentInfo != null)
                        docType = documentInfo.DocumentType;
                }
            }

            return docType;
        }

        private bool InnerUpload(string[] filePaths, List<string> previewFilePaths, bool? promptRepeatFile, DocumentType? documentType)
        {
            if (filePaths == null || filePaths.Length <= 0)
                return false;

            if (!CommonUIUtility.ValidateFileInfo(filePaths))
                return false;

            int count = filePaths.Length;
            List<DocumentInfo> documents = new List<DocumentInfo>();

            DocumentInfo document = null;
            for (int i = 0; i < count; i++)
            {
                //documents[i] = CommonUIUtility.InitDocumentInfo(this.Context, documentType);
                string fileName = Path.GetFileName(filePaths[i]);
                string[] temp = FileExtensions.Where<string>(o => o.Equals(Path.GetExtension(fileName).ToLower())).ToArray();
                if (temp != null && temp.Length > 0)
                {
                    document = new DocumentInfo();

                    document.IsDirty = true;

                    document.OriginalPath = filePaths[i];
                    if (previewFilePaths != null && previewFilePaths.Count > 0)
                    {
                        document.PreviewPath = previewFilePaths[i];
                    }
                    document.DocumentType = GetDocumentType(documentType, fileName);
                    #region  如果出现同样名称的附件信息，重新命名附件名称
                    if (fileName != null)
                    {
                        string[] filesplit = fileName.Split('.');
                        if (filesplit.Any())
                        {
                            if (documents.FirstOrDefault(n => n.Name == fileName) != null)
                            {
                                fileName = filesplit[0] + i + "." + filesplit[1];
                            }
                        }
                    }
                    #endregion
                    //TODO:1 重命名文件名异常 2.MailCenter接收重命名后文件名异常
                    //fileName = fileName.ReplaceSpecialCharacters("_");
                    //文件名中含有特殊字符时使用下划线代替
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    string fileNameExtension = Path.GetExtension(fileName);
                    string fileNameTemp = fileNameWithoutExtension.ReplaceSpecialCharacters("_");
                    fileName = fileNameTemp + fileNameExtension;

                    document.Name = fileName;
                    //处理文件名重复
                    if (IsDocumentExists(fileName))
                    {
                        if (!promptRepeatFile.HasValue)
                        {
                            if (OverrideFileTip(fileName))
                                //先删除旧文件
                                Remove(fileName);
                            else
                            {
                                string newFileName = string.Empty;
                                GetNewOverrideFileName(fileName, ref  newFileName, 1);
                                document.Name = newFileName;
                            }
                        }
                        else
                        {
                            if (promptRepeatFile.Value)
                                Remove(fileName);
                            else
                            {
                                string newFileName = string.Empty;
                                GetNewOverrideFileName(fileName, ref newFileName, 1);
                                document.Name = newFileName;
                                if (DocumentExistsEventHandler != null)
                                    DocumentExistsEventHandler(this, new CommonEventArgs<string>(document.Name));
                            }
                        }
                    }

                    documents.Add(document);
                }
            }
            if (documents.Count == 0)
            {
                return false;
            }
            listAddedDocuments.AddRange(documents);
            AddDataToGrid(documents);
            IsChanged = true;
            return true;
        }

        private bool OverrideFileTip(string fileName)
        {
            string msg = LocalData.IsEnglish ? string.Format("the new attachments:{0} to be uploading is existing in document list.Are you sure to override it?", fileName)
                               : string.Format("要上传的附件:{0}与服务器上存在的文档名重复,是否要覆盖旧文件？", fileName);
            return XtraMessageBox.Show(msg, "Tip", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }

        /// <summary>
        /// 获取不覆盖重复名称的新文件名称
        /// </summary>
        /// <param name="realFileName">真实文件名</param>
        /// <param name="newFileName">新文件名</param>
        /// <param name="index">从1开始</param>
        /// <returns></returns>
        private void GetNewOverrideFileName(string realFileName, ref string newFileName, int index)
        {
            string _newFileName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(realFileName), index, Path.GetExtension(realFileName));
            newFileName = _newFileName;
            if (CurrentDocumentList.Any(item => item.Name.Equals(_newFileName, StringComparison.OrdinalIgnoreCase)))
            {
                GetNewOverrideFileName(realFileName, ref newFileName, index + 1);
            }
            else
            {
                return;
            }
        }

        private void AddDataToGrid(List<DocumentInfo> documents)
        {
            if (DataSource == null || DataSource.Count <= 0)
            {
                DataSource = documents;
            }
            else
            {
                DataSource.AddRange(documents.ToArray());
            }
            ResetDocumentBindings();
        }

        private bool IsDocumentExists(string name)
        {
            List<DocumentInfo> documents = CurrentDocumentList;
            if (documents == null)
                return false;
            if (documents.Exists(document => document.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }
            return false;
        }




        #region 上下文菜单命令处理


        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (gridViewDocument.SelectedRowsCount <= 0)
            {
                if (gridViewDocument.FocusedRowHandle > -1)
                {
                    gridViewDocument.SelectRow(gridViewDocument.FocusedRowHandle);
                }
                else
                {
                    return;
                }
            }
            int[] selectedRowIndex = gridViewDocument.GetSelectedRows();
            List<DocumentInfo> selectedDocuments = new List<DocumentInfo>();

            for (int i = 0; i < selectedRowIndex.Length; i++)
            {
                int dataSourceIndex = gridViewDocument.GetDataSourceRowIndex(selectedRowIndex[i]);
                DocumentInfo document = CurrentDocumentList[dataSourceIndex];
                selectedDocuments.Add(document);
            }
            if (selectedDocuments.Count <= 0)
                return;
            //List<Guid> ids = (from item in this.listAddedDocuments
            //                  select item.Id).ToList();
            //this.listAddedDocuments.RemoveAll(doc => ids.Contains(doc.Id));

            List<string> selectedNames = (from item in selectedDocuments
                                          select item.Name).ToList();
            List<string> newAddedNames = (from item in listAddedDocuments
                                          select item.Name).ToList();

            listAddedDocuments.RemoveAll(doc => selectedNames.Contains(doc.Name));



            List<DocumentInfo> listDeleted = selectedDocuments.FindAll(doc => !newAddedNames.Contains(doc.Name));
            if (listDeleted != null && listDeleted.Count > 0)
                listDeletedDocuments.AddRange(listDeleted);
            List<string> selectIds = (from item in selectedDocuments
                                      select item.Name).ToList();
            DataSource.RemoveAll(doc => selectIds.Contains(doc.Name));
            ResetDocumentBindings();

            IsChanged = true;

        }

        public void Remove(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                DataSource.RemoveAll(item => item.Name.Equals(fileName.Trim(), StringComparison.OrdinalIgnoreCase));
                ResetDocumentBindings();
            }
        }

        public void Delete(List<string> attachmentNames)
        {
            if (attachmentNames == null || attachmentNames.Count <= 0)
                return;
            List<DocumentInfo> documents = DataSource.FindAll(doc => attachmentNames.Contains(doc.Name));
            if (documents == null || documents.Count <= 0)
                return;

            foreach (DocumentInfo document in documents)
            {
                if (listAddedDocuments.Contains(document))
                {
                    listAddedDocuments.RemoveAll(d => d.Name.Equals(document.Name) && !string.IsNullOrEmpty(d.OriginalPath) && d.OriginalPath.Equals(document.OriginalPath));
                }
                else
                {
                    listDeletedDocuments.Add(document);
                }
                DataSource.RemoveAll(d => d.Name.Equals(document.Name) && !string.IsNullOrEmpty(d.OriginalPath) && d.OriginalPath.Equals(document.OriginalPath));
            }
            ResetDocumentBindings();
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            Open();
        }
        private void Open()
        {
            if (!IsSelectDocument())
                return;
            DocumentInfo currentDocument = CurrentDocument;

            if (IsNewDocument(currentDocument))
            {
                using (Process proc = Process.Start(GetLocalFilePath()))
                {
                    if (proc != null)
                        proc.Dispose();
                }
            }
            else
            {
                CommonUIUtility.OpenFile(currentDocument.Id);
            }
        }

        private string GetLocalFilePath()
        {
            string localPath = CurrentDocument.PreviewPath;
            if (string.IsNullOrEmpty(localPath))
            {
                localPath = CurrentDocument.OriginalPath;
            }

            return localPath;
        }

        private bool IsSelectDocument()
        {
            DocumentInfo currentDocument = CurrentDocument;
            if (currentDocument == null)
                return false;
            return true;
        }

        private void tsmiOpenWith_Click(object sender, EventArgs e)
        {
            if (!IsSelectDocument())
                return;
            DocumentInfo currentDocument = CurrentDocument;
            if (IsNewDocument(currentDocument))
            {
                Process.Start("rundll32.exe", @"shell32,OpenAs_RunDLL " + GetLocalFilePath());
            }
            else
            {
                CommonUIUtility.OpenWith(currentDocument.Id);
            }
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            if (!IsSelectDocument())
                return;
            try
            {
                DocumentInfo currentDocument = CurrentDocument;
                if (IsNewDocument(currentDocument))
                {
                    string fileSavePath = CommonUIUtility.GetFileSavePath(currentDocument);
                    if (!string.IsNullOrEmpty(fileSavePath))
                    {
                        File.Copy(GetLocalFilePath(), fileSavePath);
                    }
                }
                else
                {
                    CommonUIUtility.DownLoad(currentDocument);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool IsNewDocument(DocumentInfo document)
        {
            return !string.IsNullOrEmpty(document.OriginalPath);
        }
        #endregion



        private void gridViewDocument_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks != 2)
                return;
            Open();
        }

        private void OnGridViewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                SetContextMenuItemEnabled();
            }
        }

        private void OnGridViewFocusesRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (CurrentRow != null)
            {
                if (CurrentRow.IsDirty)
                {
                    gridViewDocument.OptionsBehavior.Editable = true;
                    gridColumnName.OptionsColumn.AllowEdit = true;
                }
                else
                {
                    gridViewDocument.OptionsBehavior.Editable = false;
                    gridColumnName.OptionsColumn.AllowEdit = false;
                }
            }
        }

        private void gridControlDocument_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                // e.Effect = DragDropEffects.Link;
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void gridControlDocument_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] MyFiles = MailUtility.GetDropFiles(e.Data);
                tempFileName = MyFiles;
                AddDocuments(MyFiles.ToList(), null, CheckedOverride, currentSelectDocumentType);
            }
        }

        private void cmsDocumentGrid_Opening(object sender, CancelEventArgs e)
        {
            if (isInitDocument == false)
            {
                InitUploadSubItems();
                isInitDocument = true;
            }

        }

        private void gridViewDocument_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            DocumentInfo row = gridViewDocument.GetRow(e.RowHandle) as DocumentInfo;
            if (row == null || row.IsDirty == false)
                return;

            GridHelper.SetColorStyle(e.Appearance,
                                                                      PresenceStyle.NewLine);
        }




    }
}

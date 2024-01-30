using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Helper;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors;
using Microsoft.Practices.ObjectBuilder;
using Wintellect.Threading.AsyncProgModel;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using ICP.FileSystem.ServiceInterface;
using EnumDocumentType = ICP.FileSystem.ServiceInterface.DocumentType;

namespace ICP.Business.Common.UI.Document
{   
    /// <summary>
    /// 文档历史表格控件类
    /// </summary>
    public partial class UCDocumentListGrid : UserControl, IDocumentInfoDataSource
    {  
        
        public DocumentListPresenter Presenter { get; set; }
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DocumentNotifyClientService DocumentNotifyService { get; set; }
        public UCDocumentListGrid()
        {
            InitializeComponent();
            AddDocumentTypeEnumEdit();
            AddStyleFormatCondition();
            InitDocumentTypeContextMenuStripItems();
            this.Load += (sender, e) =>
            {
                Locale();
                DocumentNotifyService.DocumentStateChanged += DocumentStateChange;
                //DocumentNotifyService.DocumentAdded += OnDocumentAdd;
                //DocumentNotifyService.DocumentDeleted += OnDocumentDeleted;
                DocumentNotifyService.DocumentUploadFailed += OnDocumentUploadFailed;
                DocumentNotifyService.DocumentUploadSucessed += OnDocumentUploadSucessed;
            };
        }

        private void AddStyleFormatCondition()
        {
            StyleFormatCondition styleCondition = new StyleFormatCondition();
            this.gridViewDocumentList.FormatConditions.Add(styleCondition);
            styleCondition.Column = this.gridColumnState;
            styleCondition.Condition = FormatConditionEnum.Equal;
            styleCondition.Value1 = styleCondition.Value2 = UploadState.Failed;
            styleCondition.Appearance.ForeColor = Color.Red;
        }
        private void AddDocumentTypeEnumEdit()
        {
            RepositoryItemImageComboBox repositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            repositoryItemImageComboBox.AutoHeight = false;
            repositoryItemImageComboBox.Items.AddEnum(typeof(EnumDocumentType));
            this.gridControlDocumentList.RepositoryItems.AddRange(new RepositoryItem[] {
            repositoryItemImageComboBox});
            this.gridColumnDocumentType.ColumnEdit = repositoryItemImageComboBox;
        }

        private IEnumerator<Int32> InnerOnDocumentAdd(DocumentInfo[] documents)
        {
            //ShowAddTip(documents.ToList());
            this.Presenter.DocumentList.AddRange(documents);
            ResetDataBinding();
            yield return 1;
        }
        private IEnumerator<Int32> InnerOnDocumentDeleted(List<Guid> ids)
        {
            List<DocumentInfo> documentsDeleted = GetDocuments(ids);
            if (documentsDeleted != null)
            {
                if (documentsDeleted.Count > 0)
                {
                    //  ShowDeleteTip(documentsDeleted);
                    this.Presenter.DocumentList.RemoveAll(document => ids.Contains(document.Id));
                    ResetDataBinding();
                }
            }
            yield return 1;
        }
        private void OnDocumentUploadFailed(UploadFailedMessage message)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(InnerOnDocumentUploadFailed(message), async.EndExecute);
        }
        private IEnumerator<Int32> InnerOnDocumentUploadFailed(UploadFailedMessage message)
        {
            List<DocumentInfo> documentsFailed = GetDocuments(message.DocumentIds);
            if (documentsFailed != null)
            {
                if (documentsFailed.Count > 0)
                {
                    ShowUploadFailedTip(documentsFailed);
                    // this.Presenter.DocumentList.RemoveAll(document => ids.Contains(document.Id));
                    // this.bindingSource.ResetBindings(false);
                    SetStateCellNewValue(documentsFailed, UploadState.Failed);
                }
            }
            LocalCommonServices.ErrorTrace.SetErrorInfo(this, message.ErrorMessage);
            yield return 1;
        }
        private void OnDocumentUploadSucessed(DocumentInfo[] documents)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(InnerOnDocumentUploadSucess(documents), async.EndExecute);
        }
        private IEnumerator<Int32> InnerOnDocumentUploadSucess(DocumentInfo[] documents)
        {
            Guid[] ids = (from document in documents
                          select document.Id).ToArray();
            this.Presenter.DocumentList.RemoveAll(document => ids.Contains(document.Id));
            for (int i = 0; i < documents.Length; i++)
            {
                documents[i].State = UploadState.Successed;
            }
            this.Presenter.DocumentList.AddRange(documents);
            ResetDataBinding();
            //SetStateCellNewValue(documents.ToList(), UploadState.Successed);
            yield return 1;
        }
        private string GetFileNames(List<DocumentInfo> documents)
        {
            return (from document in documents
                    select document.Name).ToList().Aggregate((i, j) => i + "," + j);
        }

        private void SetStateCellNewValue(List<DocumentInfo> documents, UploadState state)
        {
            for (int i = 0; i < documents.Count; i++)
            {
                int datasourceIndex = this.bindingSource.IndexOf(documents[i]);
                int rowHandle = this.gridViewDocumentList.GetRowHandle(datasourceIndex);
                if (rowHandle > -1)
                {
                    this.gridViewDocumentList.SetRowCellValue(rowHandle, this.gridColumnState, state);

                }
            }
        }
        private void ShowUploadFailedTip(List<DocumentInfo> documents)
        {
            var failedFileNames = GetFileNames(documents);
            string tip = string.Format(isEnglish ? "Documents:{0} upload failed,please upload again." : "文档: {0} 上传失败，请重试。", failedFileNames);
            XtraMessageBox.Show(tip, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private List<DocumentInfo> GetDocuments(List<Guid> ids)
        {
            if (DataSource == null) 
                return null;
            else
                return this.DataSource.Where(document => ids.Contains(document.Id)).ToList();
        }

        private void OnDocumentDeleted(List<Guid> ids)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(InnerOnDocumentDeleted(ids), async.EndExecute);
        }


        private void OnDocumentAdd(DocumentInfo[] documents)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(InnerOnDocumentAdd(documents), async.EndExecute);
        }

        private void DocumentStateChange(List<Guid> ids, UploadState state)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(InnerOnDocumentStateChange(ids, state), async.EndExecute);
        }
        private IEnumerator<Int32> InnerOnDocumentStateChange(List<Guid> ids, UploadState state)
        {
            List<DocumentInfo> documents = this.Presenter.DocumentList.Where(document => ids.Contains(document.Id)).ToList();
            for (int i = 0; i < documents.Count; i++)
            {
                documents[i].State = state;

            }
            ResetDataBinding();
            yield return 1;
        }
        private void ResetDataBinding()
        {
            this.gridControlDocumentList.DataSource = this.bindingSource.DataSource = null;
            this.bindingSource.DataSource = Presenter.DocumentList;
            this.gridControlDocumentList.DataSource = this.bindingSource;
        }
        private void Locale()
        {
            if (!LocalData.IsDesignMode && !isEnglish)
            {
                this.gridColumnDocumentType.Caption = "类型";
                this.gridColumnName.Caption = "名称";
                this.gridColumnCreateBy.Caption = "创建人";
                this.gridColumnCreateDate.Caption = "上传日期";
                this.gridColumnState.Caption = "上传状态";
         
            }
        }

        private void InitDocumentTypeContextMenuStripItems()
        {
            if (this.toolStripMenuItemAddDocument.DropDownItems.Count <= 0)
            {
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<EnumDocumentType>> documentTypes = EnumHelper.GetEnumValues<EnumDocumentType>(LocalData.IsEnglish);

                foreach (var type in documentTypes)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(type.Name);
                    item.Tag = type.Value;
                    item.Click += OnMenuItemClick;
                   // contextMenuStripUpload.Items.Add(item);
                    this.toolStripMenuItemAddDocument.DropDownItems.Add(item);

                }
            }
        }
        private bool IsSelectDocument()
        {
            return this.CurrentDocument != null;
        }
        private void OnMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            //为空时代表删除动作
            if (item.Tag == null)
            {
                DeleteDocument();
            }
            else
            {
                EnumDocumentType documentType = (EnumDocumentType)item.Tag;
                String[] filePaths = CommonUIUtility.SelectFilesToUpload();
                if (filePaths == null)
                    return;

                CommonUIUtility.ValidateFileInfo(filePaths);
    
                DocumentInfo[] documents = Presenter.Upload(filePaths, Presenter.BusinessContext,documentType);
                AddDataToGrid(documents);


            }
            

        }

        private void AddDataToGrid(DocumentInfo[] documents)
        {
            Presenter.DocumentList.AddRange(documents);
            ResetDataBinding();
            //bindingSource.ResetBindings(false);
            //ShowStatusBar();
        }
        private void DeleteDocument()
        {
            if (!IsSelectDocument())
                return;
            DialogResult result = XtraMessageBox.Show(isEnglish ? "Are you sure you want to delete the selected files?" : "你确定要删除所选择的文件吗?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int rowHandler = this.gridViewDocumentList.FocusedRowHandle;
                Guid id = CurrentDocument.Id;
                DateTime? updateDate = CurrentDocument.UpdateDate;
                this.gridViewDocumentList.DeleteRow(rowHandler);

                Presenter.Delete(new List<Guid> { id }, new List<DateTime?> { updateDate });
            }

        }


        /// <summary>
        /// 当前选择文档
        /// </summary>
        public DocumentInfo CurrentDocument
        {
            get { return this.bindingSource.Current as DocumentInfo; }

        }
        private bool isEnglish = LocalData.IsEnglish;

        public List<DocumentInfo> DataSource
        {
            get { return this.bindingSource.DataSource as List<DocumentInfo>; }
            set
            {
                this.bindingSource.DataSource = value;
                this.bindingSource.ResetBindings(false);
            }

        }
    }
}

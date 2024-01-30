using System.Threading;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Wintellect.Threading.AsyncProgModel;
using System.Text.RegularExpressions;
using System.IO;
using EnumDocumentType = ICP.FileSystem.ServiceInterface.DocumentType;
using ICP.Business.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Business.Common.UI.Document
{
    /// <summary>
    /// 文档列表控件
    /// </summary>
    [SmartPart]
    public partial class UCDocumentList : UserControl, IDocumentInfoDataSource, IDataBind
    {
        #region Fields & Property & Services & Delegate

        #region Fields
        /// <summary>
        /// 是否英文
        /// </summary>
        private bool _IsEnglish = LocalData.IsEnglish;
        /// <summary>
        /// 检查状态
        /// </summary>
        private bool m_checkStatus = false;
        private DataSearchType _DataSourceType = DataSearchType.Local;
        public bool simple = false;
        #endregion

        #region Property
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 是否预览
        /// </summary>
        public bool IsView
        {
            set
            {
                if (value)
                    barItemDelete.Visibility = barItemUpload.Visibility = barItemReupload.Visibility = BarItemVisibility.Never;
            }
        }

        /// <summary>
        /// 文档列表呈现类
        /// </summary>
        private DocumentListPresenter presenter;
        /// <summary>
        /// 文档列表呈现类
        /// </summary>
        public DocumentListPresenter Presenter
        {
            get
            {
                return presenter;
            }
            set
            {
                presenter = value;
            }
        }
        /// <summary>
        /// 文档上传状态通知服务
        /// 服务端回调
        /// </summary>
        public DocumentNotifyClientService DocumentNotifyService
        {
            get
            {
                return ClientHelper.Get<DocumentNotifyClientService, DocumentNotifyClientService>();
            }
        }
        /// <summary>
        /// ICP Common服务
        /// </summary>
        public IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }

        /// <summary>
        /// 客户端上传文档服务
        /// </summary>
        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }

        }
        /// <summary>
        /// 当前选择文档
        /// </summary>
        public DocumentInfo CurrentDocument
        {
            get { return bindingSource.Current as DocumentInfo; }

        }
        /// <summary>
        /// 数据源
        /// </summary>
        public List<DocumentInfo> DataSource
        {
            get { return bindingSource.DataSource as List<DocumentInfo>; }
            set
            {
                try
                {
                    bindingSource.DataSource = value;
                    InitUploadSubItems();
                    bindingSource.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    string exceptionstr = "UCDocumentList-DataSource" + ex.Message;
                    SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                        LocalData.SessionId, new byte[0], exceptionstr,
                        DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }
            }
        }
        /// <summary>
        /// 文档类型列表
        /// </summary>
        private List<CustomEnumInfo> _documentTypes;
        /// <summary>
        /// 文档类型列表
        /// </summary>
        public List<CustomEnumInfo> DocumentTypes
        {
            get
            {
                _documentTypes = EnumGetter.Current[typeof(EnumDocumentType), true, Presenter.BusinessContext.OperationType];
                return _documentTypes;
            }
        }
        /// <summary>
        /// 获取文档列表文件名集合
        /// </summary>
        public List<string> FilesName
        {
            get
            {
                return DataSource.Select(item => item.Name).ToList();
            }
            set
            {
                FilesName = value;
            }
        }
        /// <summary>
        /// 文档列表显示隐藏状态
        /// </summary>
        public bool ShowControlCheckState
        {
            get { return gridColumnchkBussiness.Visible; }
            set
            {
                if (value)
                {
                    gridColumnchkBussiness.VisibleIndex = 0;
                }
                gridColumnchkBussiness.Visible = value;
            }
        }
        /// <summary>
        /// 刷新数据源
        /// </summary>
        public bool ResetBindings
        {
            set
            {
                bindingSource.ResetBindings(false);
            }
        }
        #endregion

        #region Delegate
        /// <summary>
        /// 重设绑定数据
        /// </summary>
        private delegate void ResetDataBindingDelegate();
        /// <summary>
        /// 绑定该数据
        /// </summary>
        /// <param name="list">文档集合</param>
        private delegate void DataBindDelegate(List<DocumentInfo> list);
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCDocumentList()
        {
            string exceptionstr = string.Empty;
            try
            {
                InitializeComponent();
                if (!LocalData.IsDesignMode)
                {
                    AddStyleFormatCondition();
                    AddDocumentTypeEnumEdit();
                    AddDocumentUploadStateEnumEdit();
                    gridViewList.IndicatorWidth = 30;
                    gridViewList.CustomDrawRowIndicator += OnDrawCustomRowIndicator;
                    barItemOpen.ItemClick += OnOpen;
                    barItemOpenWith.ItemClick += OnOpenWith;
                    barItemPreview.ItemClick += OnPreview;
                    barItemDownload.ItemClick += OnDownload;
                    barItemDelete.ItemClick += OnDelete;
                    barItemReupload.ItemClick += OnReupload;
                    barRefresh.ItemClick += OnRefresh;
                    btnItemBusinessSync.ItemClick += OnManualSynchronize;
                    gridViewList.RowClick += OnDocumentRowClick;
                    exceptionstr += "Load += (sender, e)\r\n";
                    Load += (sender, e) =>
                    {
                        Locale();
                        DocumentNotifyService.DocumentStateChanged_New += DocumentStateChange;
                        //DocumentNotifyService.DocumentUploadFailed += OnDocumentUploadFailed;
                        DocumentNotifyService.DocumentUploadSucessed_New += OnDocumentUploadSucessed;
                        //DocumentNotifyService.DocumentDeleteSucessed += OnDocumentDeleteSucessed;
                    };

                    gridViewList.Click += gridView1_Click;
                    gridViewList.CustomDrawColumnHeader += gridView1_CustomDrawColumnHeader;
                    gridViewList.DataSourceChanged += gridView1_DataSourceChanged;
                    exceptionstr += "Disposed +=\r\n";
                    Disposed += delegate
                    {
                        try
                        {
                            if (DocumentNotifyService != null)
                            {
                                DocumentNotifyService.DocumentStateChanged_New -= DocumentStateChange;
                                //DocumentNotifyService.DocumentUploadFailed -= OnDocumentUploadFailed;
                                DocumentNotifyService.DocumentUploadSucessed_New -= OnDocumentUploadSucessed;
                                //DocumentNotifyService.DocumentDeleteSucessed -= OnDocumentDeleteSucessed;
                            }
                            gridControlList.DataSource = null;
                            if (bindingSource != null)
                            {
                                bindingSource.DataSource = null;
                                bindingSource = null;
                            }
                            if (Presenter != null)
                            {
                                Presenter.Dispose();
                            }
                            if (WorkItem != null)
                            {
                                WorkItem.Items.Remove(this);
                                WorkItem = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            exceptionstr += "UCDocumentList:Disposed" + ex.Message;
                            SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                                LocalData.SessionId, new byte[0], exceptionstr,
                                DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                        }
                    };
                    exceptionstr += "CreateHandle Before\r\n";
                    CreateHandle();
                    exceptionstr += "CreateHandle Behind\r\n";
                }
            }
            catch (Exception ex)
            {
                exceptionstr += ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        #endregion

        #region IDataBind 成员
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="business">业务操作上下文</param>
        public void DataBind(BusinessOperationContext business)
        {
            if (Presenter == null)
            {
                Presenter = WorkItem.Items.AddNew<DocumentListPresenter>();
            }
            Presenter.BusinessContext = business;
            DataSearchType dataSourceType = DataSearchType.Local;
            try
            {
                Presenter.ucList = this;
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList：Presenter.ucList = this;\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
            ThreadPool.QueueUserWorkItem((temp) =>
            {
                try
                {
                    List<DocumentInfo> list = ClientFileService.GetBusinessDocumentList(business, _DataSourceType);
                    if (IsDisposed)
                    {
                        return;
                    }
                    DataBindDelegate bindDelegate = InnerDataBind;
                    if (list.Any())
                    {
                        if (list[0].OperationID != business.OperationID)
                        {
                            list = new List<DocumentInfo>();
                        }
                    }
                    Invoke(bindDelegate, list);
                }
                catch (Exception ex)
                {
                    string exceptionstr = "UCDocumentList数据绑定：DataBind\r\n" + ex.Message;
                    SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                       LocalData.SessionId, new byte[0], exceptionstr,
                       DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));

                    if (!IsDisposed && Parent.IsDisposed)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                    }
                }
            });

        }
        /// <summary>
        /// 初始化绑定数据
        /// </summary>
        /// <param name="list">文档列表</param>
        private void InnerDataBind(List<DocumentInfo> list)
        {
            try
            {
                DataSource = list;
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList数据绑定：InnerDataBind\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }

        /// <summary>
        /// 设置只读
        /// </summary>
        /// <param name="flg"></param>
        public void ControlsReadOnly(bool flg)
        {
            barItemDelete.Enabled = flg;
            barItemDownload.Enabled = flg;
            barItemOpen.Enabled = flg;
            barItemOpenWith.Enabled = flg;
            barItemPreview.Enabled = flg;
            barItemReupload.Enabled = flg;
            barItemSyncErrorText.Enabled = flg;
            barItemUpload.Enabled = flg;
            barRefresh.Enabled = flg;
        }

        #endregion

        #region Windows Event

        #region 文档工具栏按钮点击事件处理
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUploadItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!ValidateDataSources())
                {
                    return;
                }
                if (!IsBusinessContextSet())
                    return;
                if (DataSource == null)
                {
                    string strTip = _IsEnglish ? "" : "文档列表数据正在加载中，请稍后！";
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), strTip);
                    return;
                }

                BarItem item = e.Item;

                EnumDocumentType documentType = GetDocumentTypeByCaption(item.Caption);

                String[] filePaths = CommonUIUtility.SelectFilesToUpload();
                if (filePaths == null)
                    return;
                //判断文件内容是否为空
                for (int i = 0; i < filePaths.Length; i++)
                {
                    Stream temp = new FileStream(filePaths[i], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (temp.Length == 0)
                    {
                        string strTip = string.Format(_IsEnglish ? "The document content is empty！[{0}]" :
                                    "所选择的文件内容为空！[{0}]", Path.GetFileName(filePaths[i]));
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), strTip);
                        return;
                    }
                }
                if (CommonUIUtility.ValidateFileNameDuplicate(FilesName, filePaths) || !CommonUIUtility.ValidateFileInfo(filePaths))
                    return;
                Regex rx = new Regex(@"[0-9a-zA-Z_&#@() -]");
                foreach (string filepath in filePaths)
                {
                    string filename = Path.GetFileNameWithoutExtension(filepath);
                    if (filename == null) continue;
                    foreach (char charname in filename)
                    {
                        if (!rx.IsMatch(charname.ToString()) && !CommonUIUtility.IsContainsChinese(charname.ToString()))
                        {
                            string strTip = _IsEnglish ? "The document name contains illegal characters，Please change the name and then upload it！" :
                                "文档名称包含非法字符，请修改名称后重新上传！";
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), strTip);
                            return;
                        }
                    }
                }
                DocumentInfo[] documents = Presenter.Upload(filePaths, Presenter.BusinessContext, documentType);
                AddDataToGrid(documents);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        void OnDelete(object sender, ItemClickEventArgs e)
        {
            if (!ValidateDataSources())
            {
                return;
            }
            if (!IsSelectDocument())
                return;

            string message = string.Empty;

            if (CurrentDocument.FileSources == FileSource.FDispatch && CurrentDocument.Type == OperationType.OceanImport)
            {
                message = _IsEnglish ? "The file from dispatch document, can't delete and cover it." : "文档来自分文件，不能删除也不能覆盖.";
            }
            else
            {
                message = _IsEnglish ? "Are you sure you want to delete the selected files?" : "你确定要删除所选择的文件吗?";
            }

            try
            {
                ShowCustomDialog(CurrentDocument.FileSources, message);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        void OnDownload(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument())
                return;
            try
            {
                Presenter.Download(CurrentDocument, _DataSourceType);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }

        }
        /// <summary>
        /// 预览
        /// </summary>
        void OnPreview(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument() || !IsDocumentLocalSaved())
                return;
            try
            {
                string fileExtension = CurrentDocument.Name.Substring(CurrentDocument.Name.LastIndexOf(".", StringComparison.Ordinal) + 1); ;
                if (!string.IsNullOrEmpty(fileExtension)
                    &&CurrentDocument.DocumentType == EnumDocumentType.NRAS
                    && "MSG".Equals(fileExtension.ToUpper()))
                    Presenter.Open(CurrentDocument.Id, _DataSourceType);
                else
                    Presenter.Preview(CurrentDocument.Id, _DataSourceType);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 选择打开方式
        /// </summary>
        void OnOpenWith(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument())
                return;
            try
            {
                Presenter.OpenWith(CurrentDocument.Id, _DataSourceType);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnOpen(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument())
                return;
            try
            {
                Presenter.Open(CurrentDocument.Id, _DataSourceType);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }

        }
        /// <summary>
        /// 重新上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnReupload(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument())
                return;
            if (!IsBusinessContextSet())
                return;
            if (!IsFailedState())
                return;
            Presenter.Reupload(new List<Guid> { CurrentDocument.Id });

        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnRefresh(object sender, ItemClickEventArgs e)
        {
            if (Presenter != null)
            {
                Presenter.BindData(Presenter.BusinessContext);
                ResetBindings = true;
            }
        }

        #endregion

        /// <summary>
        /// 添加数据到网格
        /// </summary>
        /// <param name="documents"></param>
        private void AddDataToGrid(IEnumerable<DocumentInfo> documents)
        {
            if (documents != null)
            {
                Presenter.DocumentList.AddRange(documents);
                ResetDataBinding();
            }
        }
        /// <summary>
        /// 重置数据源
        /// </summary>
        private void ResetDataBinding()
        {
            if (InvokeRequired)
            {
                ResetDataBindingDelegate resetDelegate = InnerSetDataBinding;
                Invoke(resetDelegate);
            }
            else
            {
                InnerSetDataBinding();
            }

        }
        /// <summary>
        /// 重置数据源
        /// </summary>
        private void InnerSetDataBinding()
        {
            try
            {
                if (bindingSource != null)
                {
                    try
                    {
                        bindingSource.ResetBindings(false);
                    }
                    catch
                    {
                        gridControlList.DataSource = bindingSource.DataSource = null;
                        bindingSource.DataSource = Presenter.DocumentList;
                        gridControlList.DataSource = bindingSource;
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList数据绑定：InnerSetDataBinding\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 网格数据源改变
        /// 启用复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                GridColumn column = gridViewList.Columns.ColumnByFieldName("Selected");
                if (column != null)
                {
                    column.Width = 30;
                    column.OptionsColumn.ShowCaption = false;
                    column.ColumnEdit = new RepositoryItemCheckEdit();
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList：gridView1_DataSourceChanged\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 在复选框列头绘制总复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column != null && e.Column.FieldName == "Selected")
                {
                    e.Info.InnerElements.Clear();
                    e.Painter.DrawObject(e.Info);
                    DevHelper.DrawCheckBox(e, m_checkStatus);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList：gridView1_CustomDrawColumnHeader\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 点击列头复选框时切换列复选框状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DevHelper.ClickGridCheckBox(gridViewList, "Selected", m_checkStatus))
                {
                    m_checkStatus = !m_checkStatus;
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList：gridView1_Click\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 列表单击击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column == gridColumnchkBussiness)
                {
                    if (CurrentDocument.State != UploadState.Successed)
                    {
                        string strMessage = _IsEnglish ? "Documents are uploading,Please wait..." : "文档正在上传中，请等待...";
                        XtraMessageBox.Show(strMessage, "Tips", MessageBoxButtons.OK);
                        return;
                    }
                    CurrentDocument.Selected = !CurrentDocument.Selected;
                    bindingSource.ResetCurrentItem();
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList：gridViewList_RowCellClick\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 文档行点击
        /// 1.双击预览文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDocumentRowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                barItemPreview.PerformClick();
            }
        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDrawCustomRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1));
            }
        }
        /// <summary>
        /// 网格鼠标移动
        /// 显示列提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GridHitInfo hitInfo = gridViewList.CalcHitInfo(e.Location);

                if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0 || hitInfo.Column != gridColumnDocumentType)
                {
                    toolTip1.Hide(this);
                    return;
                }

                DocumentInfo item = gridViewList.GetRow(hitInfo.RowHandle) as DocumentInfo;
                if (item == null) return;
                string s = toolTip1.GetToolTip(this);
                if (toolTip1.Active && s == EnumHelper.GetDescription(item.DocumentType, _IsEnglish)) return;

                Point pt = gridControlList.PointToClient(MousePosition);
                pt.X += 20;
                pt.Y += 30;
                toolTip1.Show(EnumHelper.GetDescription(item.DocumentType, _IsEnglish), this, pt, 5000);
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList：gridViewList_MouseMove\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 数据查询类型改变
        /// </summary>
        void comboBoxRangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DataSourceType = (DataSearchType)((ImageComboBoxEdit)sender).EditValue;
            if (Presenter != null && Presenter.BusinessContext != null)
            {
                DataBind(Presenter.BusinessContext);
            }
        }
        #endregion

        #region Delegate & Method
        /// <summary>
        /// 文档上传成功
        /// </summary>
        /// <param name="documents"></param>
        private void OnDocumentUploadSucessed(DocumentInfo[] documents)
        {
            if (documents != null)
            {
                try
                {
                    AsyncEnumerator async = new AsyncEnumerator();
                    async.BeginExecute(InnerOnDocumentUploadSuccess(documents), async.EndExecute);
                    DocumentInfo nrasdocInfo = documents.Where(item => item.DocumentType == EnumDocumentType.NRAS).FirstOrDefault();
                    //报价NRAS文档更新则刷新报价单列表
                    if (nrasdocInfo != null)
                    {
                        WorkItem.Commands["Command_QuotedPrice_Refresh"].Execute();
                    }
                    if(documents.Any(fItem=>fItem.DocumentType== EnumDocumentType.AN_C && fItem.IsDirty))
                    {
                        List<EventObjects>  events=ServiceClient.GetService<IFCMCommonService>().GetMemoList(Presenter.BusinessContext.OperationID, null);
                        if(events.Any(fItem=> "ANRC".Equals(fItem.Code)))
                        {
                            return;
                        }
                        var eventObjects = new EventObjects
                        {
                            OperationID = Presenter.BusinessContext.OperationID,
                            OperationType = OperationType.OceanImport,
                            Code = "ANRC",
                            Id = Guid.Empty,
                            FormID = Presenter.BusinessContext.OperationID,
                            FormType = FormType.Unknown,
                            IsShowAgent = false,
                            IsShowCustomer = true,
                            Subject = "已收到承运人到港通知书",
                            Description = "Have received carrier 's arrival notice",
                            Priority = MemoPriority.Normal,
                            Type = MemoType.EmailLog,
                            UpdateDate = DateTime.Now,
                            UpdateBy = LocalData.UserInfo.LoginID
                        };
                        ServiceClient.GetService<IFCMCommonService>().SaveMemoInfo(eventObjects);
                        ServiceClient.GetClientService<WorkItem>().Commands["Command_ListRefresh"].Execute();
                    }
                }
                catch (Exception ex)
                {
                    
                    string exceptionstr = "UCDocumentList:OnDocumentUploadSucessed"+CommonHelper.BuildExceptionString(ex);
                    SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                        LocalData.SessionId, new byte[0], exceptionstr,
                        DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }
            }
        }
        /// <summary>
        /// 文档删除成功
        /// </summary>
        /// <param name="documentIds"></param>
        private void OnDocumentDeleteSucessed(List<Guid> documentIds)
        {
            try
            {
                AsyncEnumerator async = new AsyncEnumerator();
                async.BeginExecute(InnerOnDocumentDeleteSuccess(documentIds), async.EndExecute);
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList:OnDocumentDeleteSucessed" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        

        /// <summary>
        /// 文档上传成功
        /// 刷新文档列表
        /// </summary>
        /// <param name="documents">文档列表</param>
        /// <returns></returns>
        private IEnumerator<Int32> InnerOnDocumentUploadSuccess(DocumentInfo[] documents)
        {
            string exceptionstr = string.Empty;
            if (documents == null || documents.Length <= 0 || Presenter.DocumentList == null)
            {
                yield return 1;
            }
            if (documents != null)
            {
                List<DocumentInfo> listDocument = documents.ToList();
                if (Presenter.BusinessContext != null)
                {
                    //移除不属于当前业务的文档信息
                    listDocument.RemoveAll(document => Presenter.BusinessContext.OperationID != document.OperationID);
                }
                if (Presenter.DocumentList != null)
                {
                    Guid[] ids = (from document in documents select document.Id).ToArray();
                    Presenter.DocumentList.RemoveAll(document => ids.Contains(document.Id));
                    for (int i = 0; i < documents.Length; i++)
                    {
                        documents[i].State = UploadState.Successed;
                    }
                }
                if (Presenter.DocumentList != null)
                {
                    Presenter.DocumentList.AddRange(listDocument);
                }
                ResetDataBinding();
            }
            yield return 1;
        }
        /// <summary>
        /// 文档删除成功
        /// 刷新文档列表
        /// </summary>
        /// <param name="documentIds">文档列表</param>
        /// <returns></returns>
        private IEnumerator<Int32> InnerOnDocumentDeleteSuccess(List<Guid> documentIds)
        {
            try
            {
                if (documentIds == null || documentIds.Count <= 0)
                    //yield return 1;
                    goto END;
                if (documentIds != null)
                {
                    List<DocumentInfo> documentList = Presenter.DocumentList.FindAll(document => documentIds.Contains(document.Id)).ToList();
                    if (documentList.Count <= 0)
                    {
                        //yield return 1;
                        goto END;
                    }
                    Presenter.DocumentList.RemoveAll(document => documentList.Contains(document));
                    ResetDataBinding();
                }
            END:
                string endmothed = string.Empty;
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList:InnerOnDocumentDeleteSuccess" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
            yield return 1;
        }
        /// <summary>
        /// 文档上传失败
        /// </summary>
        /// <param name="message"></param>
        private void OnDocumentUploadFailed(UploadFailedMessage message)
        {
            try
            {
                AsyncEnumerator async = new AsyncEnumerator();
                async.BeginExecute(InnerOnDocumentUploadFailed(message), async.EndExecute);
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList:OnDocumentUploadFailed" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 文档上传失败
        /// 更新上传状态
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private IEnumerator<Int32> InnerOnDocumentUploadFailed(UploadFailedMessage message)
        {
            try
            {
                List<DocumentInfo> documentsFailed = GetDocuments(message.DocumentIds);

                if (documentsFailed != null)
                {
                    // int count = documentsFailed.Count;
                    // if (count > 0)
                    //{
                    //for (int i = 0; i < count; i++)
                    //{
                    //   documentsFailed[i].State = UploadState.Failed;
                    //  }
                    Presenter.DocumentList.RemoveAll(document => documentsFailed.Contains(document));
                    ResetDataBinding();
                }

                LocalCommonServices.ErrorTrace.SetErrorInfo(this, message.ErrorMessage);
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList:InnerOnDocumentUploadFailed" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
            yield return 1;
        }
        /// <summary>
        /// 文档状态改变
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        private void DocumentStateChange(List<Guid> ids, UploadState state)
        {
            try
            {
                AsyncEnumerator async = new AsyncEnumerator();
                async.BeginExecute(InnerOnDocumentStateChange(ids, state), async.EndExecute);
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList:DocumentStateChange" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 文档状态改变
        /// 更新文档上传状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private IEnumerator<Int32> InnerOnDocumentStateChange(List<Guid> ids, UploadState state)
        {
            try
            {
                if (Presenter == null || Presenter.DocumentList == null)
                {
                    //yield return 1;
                }
                if (Presenter != null && Presenter.DocumentList != null)
                {
                    List<DocumentInfo> documents = Presenter.DocumentList.Where(document => ids.Contains(document.Id)).ToList();
                    for (int i = 0; i < documents.Count; i++)
                    {
                        documents[i].State = state;
                        ClientFileService.ChangeDocumentUploadState(new Guid[] { documents[i].Id }, state);
                    }

                    ResetDataBinding();
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList:InnerOnDocumentStateChange" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
            yield return 1;
        }
        /// <summary>
        /// 根据文档ID列表获取文档列表
        /// </summary>
        /// <param name="ids">文档ID列表</param>
        /// <returns>文档列表</returns>
        private List<DocumentInfo> GetDocuments(List<Guid> ids)
        {
            if (DataSource == null)
                return null;
            else
                return DataSource.Where(document => ids.Contains(document.Id)).ToList();
        }
        /// <summary>
        /// 根据文档列表获取文档名串联字符串
        /// </summary>
        /// <param name="documents">文档列表</param>
        /// <returns>文档名串联字符串</returns>
        private string GetFileNames(IEnumerable<DocumentInfo> documents)
        {
            return (from document in documents
                    select document.Name).ToList().Aggregate((i, j) => i + "," + j);
        }

        private void InitControls()
        {
            List<EnumHelper.ListItem<DataSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DataSearchType>(_IsEnglish);
            comboBoxRangs.BeginUpdate();
            comboBoxRangs.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                comboBoxRangs.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            comboBoxRangs.EndUpdate();
            comboBoxRangs.SelectedIndexChanged += comboBoxRangs_SelectedIndexChanged;

            barEditItemRangs.EditValue = DataSearchType.Local;
        }

        public void SetSimple()
        {
            if (simple)
            {
                barEditItemRangs.EditValue = DataSearchType.ALL;
                barEditItemRangs.Enabled = false;
            }
        }

        /// <summary>
        /// 本地化
        /// 1.设置控件文本
        /// </summary>
        private void Locale()
        {
            Text = _IsEnglish ? "Document List" : "文档列表";
            if (!LocalData.IsDesignMode && !_IsEnglish)
            {
                gridColumnDocumentType.Caption = "单证类型";
                gridColumnName.Caption = "名称";
                gridColumnCreateBy.Caption = "创建人";
                gridColumnchkBussiness.Caption = "选择";
                gridColumnCreateDate.Caption = "上传日期";
                gridColumnState.Caption = "上传状态";
                barItemDelete.Caption = "删除(&D)";
                barItemDownload.Caption = "下载(&X)";
                barItemOpen.Caption = "打开(&Q)";
                barItemOpenWith.Caption = "打开方式(&W)";
                barItemPreview.Caption = "预览(&P)";
                barItemUpload.Caption = "上传(&U)";
                barItemSyncErrorText.Caption = "文档数据更新有误，请手动更新。";
                barItemReupload.Caption = "重新上传(&R)";
                barRefresh.Caption = "刷新";
            }
        }
        /// <summary>
        /// 验证数据来源
        /// </summary>
        /// <returns>验证结果:可继续操作返回true;否则返回false</returns>
        bool ValidateDataSources()
        {
            if (_DataSourceType == DataSearchType.Agent || _DataSourceType == DataSearchType.ALL)
            {
                string strTip = string.Format(
                    _IsEnglish ?
                        "Document data contains [] cannot upload and delete documents"
                        : "文档数据包含[{0}]时不可上传及其删除文档！"
                    , EnumHelper.GetDescription(DataSearchType.Agent, _IsEnglish));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), strTip);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 自定义弹出框
        /// </summary>
        /// <param name="fileSources">文件来源</param>
        /// <param name="message">提示信息</param>
        void ShowCustomDialog(FileSource fileSources, string message)
        {
            DialogResult result;
            if (fileSources == FileSource.FDispatch && CurrentDocument.Type == OperationType.OceanImport)
            {
                result = XtraMessageBox.Show(message, "Tips", MessageBoxButtons.OK);
            }
            else
            {
                result = XtraMessageBox.Show(message, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int rowHandler = gridViewList.FocusedRowHandle;
                    Guid id = CurrentDocument.Id;
                    DateTime? updateDate = CurrentDocument.UpdateDate;
                    gridViewList.DeleteRow(rowHandler);
                    Presenter.Delete(new List<Guid> { id }, new List<DateTime?> { updateDate });
                }
            }
        }
        /// <summary>
        /// 是否上传失败状态
        /// </summary>
        /// <returns>True:上传失败    False:其他状态</returns>
        private bool IsFailedState()
        {
            return CurrentDocument.State == UploadState.Failed;
        }
        /// <summary>
        ///控制业务相关按钮是否显示
        /// </summary>
        /// <param name="Display">是否显示</param>
        public void ManageBusinessBarDisplay(bool Display)
        {
            try
            {
                BarItemVisibility visibility = BarItemVisibility.Always;
                visibility = Display ? BarItemVisibility.Always : BarItemVisibility.Never;
                barItemUpload.Visibility = barItemReupload.Visibility = barItemDelete.Visibility = visibility;
                gridColumnState.Visible = Display;
            }
            catch (Exception ex)
            {
                string exceptionstr = "UCDocumentList:ManageBusinessBarDisplay" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 添加行样式
        /// 1.上传状态列失败状态显示红色
        /// </summary>
        private void AddStyleFormatCondition()
        {
            StyleFormatCondition styleCondition = new StyleFormatCondition();
            gridViewList.FormatConditions.Add(styleCondition);
            styleCondition.Column = gridColumnState;
            styleCondition.Condition = FormatConditionEnum.Equal;
            styleCondition.Value1 = styleCondition.Value2 = UploadState.Failed;
            styleCondition.Appearance.ForeColor = Color.Red;
        }
        /// <summary>
        /// 上传状态列:添加上传状态集合
        /// </summary>
        private void AddDocumentUploadStateEnumEdit()
        {
            List<EnumHelper.ListItem<UploadState>> states = EnumHelper.GetEnumValues<UploadState>(_IsEnglish);
            List<ImageComboBoxItem> items = new List<ImageComboBoxItem>();
            foreach (var state in states)
            {
                ImageComboBoxItem item = new ImageComboBoxItem();
                item.Description = state.Name;
                item.Value = state.Value;
                items.Add(item);
            }
            RepositoryItemImageComboBox repositoryItemImageComboBox = new RepositoryItemImageComboBox();
            repositoryItemImageComboBox.AutoHeight = false;
            repositoryItemImageComboBox.Items.AddRange(items.ToArray());
            gridControlList.RepositoryItems.AddRange(new RepositoryItem[] {
            repositoryItemImageComboBox});
            gridColumnState.ColumnEdit = repositoryItemImageComboBox;
        }
        /// <summary>
        /// 是否选择文档
        /// </summary>
        /// <returns>True:存在选择文档  False:未选择文档</returns>
        private bool IsSelectDocument()
        {
            return CurrentDocument != null;
        }
        /// <summary>
        /// 文档是否保存到本地
        /// </summary>
        /// <returns>True:已保存 False:未保存</returns>
        private bool IsDocumentLocalSaved()
        {
            return CurrentDocument.State.GetHashCode() >= UploadState.LocalSaved.GetHashCode();
        }
        /// <summary>
        /// 列添加文档类型集合
        /// </summary>
        private void AddDocumentTypeEnumEdit()
        {
            RepositoryItemImageComboBox repositoryItemImageComboBox = new RepositoryItemImageComboBox();
            repositoryItemImageComboBox.AutoHeight = true;
            repositoryItemImageComboBox.Items.AddEnum(typeof(EnumDocumentType));
            gridControlList.RepositoryItems.AddRange(new RepositoryItem[] {
            repositoryItemImageComboBox});
            gridColumnDocumentType.ColumnEdit = repositoryItemImageComboBox;
        }
        /// <summary>
        /// 初始化上传菜单
        /// </summary>
        /// <returns></returns>
        private bool InitUploadSubItems()
        {
            try
            {
                //if (this.barItemUpload.ItemLinks.Count > 0)
                //{
                //    return true;
                //}
                if (simple)
                {
                     barItemUpload.ItemLinks.Clear();
                     CustomEnumInfo info = DocumentTypes[DocumentTypes.Count - 1];
                     BarButtonItem btnItem = new BarButtonItem();
                     ChildrenEnumInfo childrenEnumInfo = info.ChildrenNodes[0];
                     btnItem.Tag = childrenEnumInfo.Caption;
                     btnItem.Caption = _IsEnglish ? childrenEnumInfo.Etip : childrenEnumInfo.Ctip;
                     btnItem.ItemClick += OnUploadItemClick;
                     btnItem.ResetDropDownSuperTip();
                     barItemUpload.AddItem(btnItem);
                }
                else
                {
                    if (DocumentTypes != null && DocumentTypes.Count > 0)
                    {
                        barItemUpload.ItemLinks.Clear();

                        int count = DocumentTypes.Count;
                        for (int i = 0; i < count; i++)
                        {
                            CustomEnumInfo info = DocumentTypes[i];
                            BarSubItem barItem = new BarSubItem();
                            if (info.HasChildNodes)
                            {
                                barItem.Caption = _IsEnglish ? info.eCaption : info.cCaption;
                                barItemUpload.AddItem(barItem);
                                int subCount = info.ChildrenNodes.Count;
                                for (int child = 0; child < subCount; child++)
                                {
                                    BarButtonItem subBarItem = new BarButtonItem();
                                    subBarItem.Tag = info.ChildrenNodes[child].Caption;
                                    subBarItem.ItemClick += OnUploadItemClick;
                                    subBarItem.Caption = _IsEnglish ? info.ChildrenNodes[child].Etip : info.ChildrenNodes[child].Ctip;
                                    barItem.AddItem(subBarItem);
                                }
                            }
                            else
                            {
                                BarButtonItem btnItem = new BarButtonItem();
                                if (info.ChildrenNodes.Any())
                                {
                                    ChildrenEnumInfo childrenEnumInfo = info.ChildrenNodes[0];
                                    btnItem.Tag = childrenEnumInfo.Caption;
                                    btnItem.Caption = _IsEnglish ? childrenEnumInfo.Etip : childrenEnumInfo.Ctip;
                                    btnItem.ItemClick += OnUploadItemClick;
                                    btnItem.ResetDropDownSuperTip();
                                    barItemUpload.AddItem(btnItem);
                                }
                            }
                        }
                    }
                }

              
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                string exceptionstr = "UCDocumentList:InitUploadSubItems\r\n" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                   LocalData.SessionId, new byte[0], exceptionstr,
                   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }

            return true;
        }
        /// <summary>
        /// 通过文本获取文档类型
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private EnumDocumentType GetDocumentTypeByCaption(string caption)
        {
            EnumDocumentType type = EnumDocumentType.Other;
            CustomEnumInfo info = null;
            info = _IsEnglish ? DocumentTypes.Find(item => item.Etip.Equals(caption)) : DocumentTypes.Find(item => item.Ctip.Equals(caption));

            if (info != null)
            {
                type = (EnumDocumentType)Enum.Parse(typeof(EnumDocumentType), info.eCaption, false);
            }

            return type;
        }
        /// <summary>
        /// 是否业务集
        /// </summary>
        /// <returns></returns>
        private bool IsBusinessContextSet()
        {
            return Presenter.BusinessContext != null && Presenter.BusinessContext.OperationID != null;
        }
        #endregion

        #region Comment Code
        ///// <summary>
        ///// 是否有初始化文档类型
        ///// </summary>
        //private bool isInitDocument = false;
        private void ShowUploadFailedTip(IEnumerable<DocumentInfo> documents)
        {
            var failedFileNames = GetFileNames(documents);
            string tip = string.Format(_IsEnglish ? "Documents:{0} upload failed,please upload again." : "文档: {0} 上传失败，请重试。", failedFileNames);
            XtraMessageBox.Show(tip, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetStateCellNewValue(List<DocumentInfo> documents, UploadState state)
        {
            for (int i = 0; i < documents.Count; i++)
            {
                int datasourceIndex = bindingSource.IndexOf(documents[i]);
                int rowHandle = gridViewList.GetRowHandle(datasourceIndex);
                if (rowHandle > -1)
                {
                    gridViewList.SetRowCellValue(rowHandle, gridColumnState, state);

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager_HighlightedLinkChanged(object sender, HighlightedLinkChangedEventArgs e)
        {
            //if (e.Link == null)
            //{
            //    barManager.GetToolTipController().HideHint();
            //    return;
            //}
            //if (!e.Link.IsLinkInMenu || e.Link.Item.GetType().Name != "BarButtonItem")
            //    return;
            //DevExpress.XtraBars.BarButtonItem iem = e.Link.Item as DevExpress.XtraBars.BarButtonItem;

            //ToolTipControllerShowEventArgs te = new ToolTipControllerShowEventArgs();
            //te.ToolTipLocation = ToolTipLocation.Fixed;
            //te.SuperTip = new SuperToolTip();
            //te.SuperTip.Items.Add(iem.Tag.ToString());
            //Point linkPoint = new Point(e.Link.Bounds.Right, e.Link.Bounds.Bottom);
            //barManager.GetToolTipController().ShowHint(te, e.Link.LinkPointToScreen(linkPoint));
        }
        #region 手动同步数据
        void OnManualSynchronize(object sender, ItemClickEventArgs e)
        {
            //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this, isEnglish ? "Start synchronize data..." : "开始同步数据...");
            //SyncOperationStatistics statics = SynchronizationHelper.Current.Synchronize();
            //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this, isEnglish ? "Finish synchronize data." : "数据同步完成。");
            //DocumentMemoryCache.Clear();
            //this.barStatus.Visible = false;

        }
        #endregion
        #endregion

        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }
    }
}

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using System.IO;
using System.Text.RegularExpressions;

namespace ICP.FileSystem.UI
{
    /// <summary>
    /// 文档列表控件
    /// </summary>
    [SmartPart]
    public partial class BaseDocumentList : BasePart, IDataBind
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
        /// 当前选择文档
        /// </summary>
        public DocumentInfo CurrentDocument
        {
            get { return bsList.Current as DocumentInfo; }

        }
        /// <summary>
        /// 数据源
        /// </summary>
        public List<DocumentInfo> DataSource
        {
            get { return bsList.DataSource as List<DocumentInfo>; }
            set
            {
                bsList.DataSource = value;
                InitUploadSubItems();
                bsList.ResetBindings(false);
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
        public void SetResetBindings(bool value)
        {
            bsList.ResetBindings(false);
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
        public BaseDocumentList()
        {
            string exceptionstr = string.Empty;
            try
            {
                InitializeComponent();
                if (!LocalData.IsDesignMode)
                {
                    GridViewList.IndicatorWidth = 30;
                    GridViewList.CustomDrawRowIndicator += OnDrawCustomRowIndicator;
                    barItemOpen.ItemClick += OnOpen;
                    barItemOpenWith.ItemClick += OnOpenWith;
                    barItemPreview.ItemClick += OnPreview;
                    barItemDownload.ItemClick += OnDownload;
                    barItemDelete.ItemClick += OnDelete;
                    barItemReupload.ItemClick += OnReupload;
                    barRefresh.ItemClick += OnRefresh;
                    GridViewList.RowClick += OnDocumentRowClick;
                    exceptionstr += "Load += (sender, e)\r\n";
                    Load += (sender, e) =>
                    {
                        Locale();
                    };

                    GridViewList.Click += gridView1_Click;
                    GridViewList.CustomDrawColumnHeader += gridView1_CustomDrawColumnHeader;
                    GridViewList.DataSourceChanged += gridView1_DataSourceChanged;
                    exceptionstr += "Disposed +=\r\n";
                    Disposed += delegate
                    {
                        try
                        {
                            gridControlList.DataSource = null;
                            if (bsList != null)
                            {
                                bsList.DataSource = null;
                                bsList = null;
                            }
                            if (WorkItem != null)
                            {
                                WorkItem.Items.Remove(this);
                                WorkItem = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                        }
                    };
                    CreateHandle();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 重写加载:初始化控件值
        /// </summary>
        /// <param name="e"></param>
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
            BindData(business);
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
        public void OnUploadItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                string[] filePaths = FileSystemUIUtility.SelectFilesToUpload();
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
                if (FileSystemUIUtility.ValidateFileNameDuplicate(FilesName, filePaths) || !FileSystemUIUtility.ValidateFileInfo(filePaths))
                    return;
                Regex rx = new Regex(@"[0-9a-zA-Z_&#@() -]");
                foreach (string filepath in filePaths)
                {
                    string filename = Path.GetFileNameWithoutExtension(filepath);
                    if (filename == null) continue;
                    foreach (char charname in filename)
                    {
                        if (!rx.IsMatch(charname.ToString()) && !FileSystemUIUtility.IsContainsChinese(charname.ToString()))
                        {
                            string strTip = _IsEnglish ? "The document name contains illegal characters，Please change the name and then upload it！" :
                                "文档名称包含非法字符，请修改名称后重新上传！";
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), strTip);
                            return;
                        }
                    }
                }

                BarItem item = e.Item;
                DocumentUpload(filePaths,item.Caption);
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
            try
            {
                if (!IsSelectDocument())
                    return;
                DocumentDelete();
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
            
            try
            {
                if (!IsSelectDocument())
                    return;
                DocumentDownload();
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
            
            try
            {
                if (!IsSelectDocument() || !IsDocumentLocalSaved())
                    return;
                DocumentPreview();
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
            
            try
            {
                if (!IsSelectDocument())
                    return;
                DocumentOpenWith();
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
            
            try
            {
                if (!IsSelectDocument())
                    return;
                DocumentOpen();
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
            try
            {
                if (!IsSelectDocument())
                    return;
                if (!IsFailedState())
                    return;
                DocumentReupload();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnRefresh(object sender, ItemClickEventArgs e)
        {
            try
            {
                DocumentListRefresh();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
            
        }

        #endregion
        
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
                GridColumn column = GridViewList.Columns.ColumnByFieldName("Selected");
                if (column != null)
                {
                    column.Width = 30;
                    column.OptionsColumn.ShowCaption = false;
                    column.ColumnEdit = new RepositoryItemCheckEdit();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
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
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
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
                if (DevHelper.ClickGridCheckBox(GridViewList, "Selected", m_checkStatus))
                {
                    m_checkStatus = !m_checkStatus;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
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
                    bsList.ResetCurrentItem();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
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
                GridHitInfo hitInfo = GridViewList.CalcHitInfo(e.Location);

                if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0 || hitInfo.Column != gridColumnDocumentType)
                {
                    TipType.Hide(this);
                    return;
                }

                DocumentInfo item = GridViewList.GetRow(hitInfo.RowHandle) as DocumentInfo;
                if (item == null) return;
                string s = TipType.GetToolTip(this);
                if (TipType.Active && s == EnumHelper.GetDescription(item.DocumentType, _IsEnglish)) return;

                Point pt = gridControlList.PointToClient(MousePosition);
                pt.X += 20;
                pt.Y += 30;
                TipType.Show(EnumHelper.GetDescription(item.DocumentType, _IsEnglish), this, pt, 5000);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion

        #region virtual
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void InitControls()
        {
        }
        /// <summary>
        /// 初始化上传菜单
        /// </summary>
        /// <returns></returns>
        protected virtual void InitUploadSubItems()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="business"></param>
        protected virtual void BindData(BusinessOperationContext business)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void DocumentOpen()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void DocumentOpenWith()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void DocumentPreview()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void DocumentDelete()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void DocumentDownload()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="caption"></param>
        protected virtual void DocumentUpload(string[] filePaths,string caption)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void DocumentReupload()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void DocumentListRefresh()
        {

        }
        #endregion

        #region Delegate & Method
        
        
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
                barItemReupload.Caption = "重新上传(&R)";
                barRefresh.Caption = "刷新";
            }
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
        /// 是否上传失败状态
        /// </summary>
        /// <returns>True:上传失败    False:其他状态</returns>
        private bool IsFailedState()
        {
            return CurrentDocument.State == UploadState.Failed;
        }
        /// <summary>
        /// 文档是否保存到本地
        /// </summary>
        /// <returns>True:已保存 False:未保存</returns>
        private bool IsDocumentLocalSaved()
        {
            return CurrentDocument.State.GetHashCode() >= UploadState.LocalSaved.GetHashCode();
        }
        #endregion
    }
}

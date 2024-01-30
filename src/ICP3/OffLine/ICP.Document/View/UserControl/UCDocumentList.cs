#region Comment

/*
 * 
 * FileName:    UCDocumentPart.cs
 * CreatedOn:   2014/5/14 星期三 14:08:07
 * CreatedBy:   taylor
 * 
 * Description：
 *      ->文档列表面板:
 *      ->1.根据业务数据展示文档列表信息
 *      ->2.通过支持文档格式的程序打开文档
 *      ->3.通过选择程序打开文档
 *      ->4.在程序内预览文档，并支持打印
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


using System.Windows.Forms;
using System;
using System.Collections.Generic;
using DevExpress.XtraBars;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace ICP.Document
{
    public partial class UCDocumentList : ViewBase, IVDocument
    {
        #region 成员变量
        /// <summary>
        /// 当前选择文档
        /// </summary>
        public DocumentInfo CurrentDocument
        {
            get { return this.bdscDocument.Current as DocumentInfo; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCDocumentList()
        {
            InitializeComponent();
            barItemOpen.ItemClick += new ItemClickEventHandler(barItemOpen_ItemClick);
            barItemOpenWith.ItemClick += new ItemClickEventHandler(barItemOpenWith_ItemClick);
            barItemPreview.ItemClick += new ItemClickEventHandler(barItemPreview_ItemClick);
            gdvwDocument.MouseMove += new MouseEventHandler(gdvwDocument_MouseMove);
            this.gdvwDocument.RowClick += new RowClickEventHandler(gdvwDocument_RowClick);
            this.Disposed += (sender, e) =>
            {
                barItemOpen.ItemClick -= new ItemClickEventHandler(barItemOpen_ItemClick);
                barItemOpenWith.ItemClick -= new ItemClickEventHandler(barItemOpenWith_ItemClick);
                gdvwDocument.MouseMove -= new MouseEventHandler(gdvwDocument_MouseMove);
                this.gdvwDocument.RowClick -= new RowClickEventHandler(gdvwDocument_RowClick);
            };
        }
        

        protected override object CreatePresenter()
        {
            return new PDocument(this);
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// Open Document
        /// </summary>
        void barItemOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument())
                return;
            this.OpenDocument_Process(this, new DocEventArgs() { DocumentID = CurrentDocument.Id });
        }
        /// <summary>
        /// Open With
        /// </summary>
        void barItemOpenWith_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument())
                return;
            this.OpenDocument_OpenWith(this, new DocEventArgs() { DocumentID = CurrentDocument.Id });
        }
        /// <summary>
        /// Preview
        /// </summary>
        void barItemPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsSelectDocument())
                return;
            this.OpenDocument_Preview(this, new DocEventArgs() { DocumentID = CurrentDocument.Id });
        }
        /// <summary>
        /// 行点击
        /// </summary>
        void gdvwDocument_RowClick(object sender, RowClickEventArgs e)
        {
            //左键且点击两次
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                this.barItemPreview.PerformClick();
            }
        }
        /// <summary>
        /// GridView Mouse Move
        /// </summary>
        void gdvwDocument_MouseMove(object sender, MouseEventArgs e)
        {
            if (bdscDocument.DataSource == null)
                return;
            GridHitInfo hitInfo = gdvwDocument.CalcHitInfo(e.Location);
            
            if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0 || hitInfo.Column != gridColumnDType)
            {
                toolTip1.Hide(this);
                return;
            }

            DocumentInfo item = gdvwDocument.GetRow(hitInfo.RowHandle) as DocumentInfo;
            //if (item == null || item.HasError == false) return;
            string s = toolTip1.GetToolTip(this);
            if (toolTip1.Active && s == EnumHelper.GetDescription(item.DType,true)) return;

            Point pt = xggcDocument.PointToClient(MousePosition);
            pt.X += 20;
            pt.Y += 30;
            this.toolTip1.Show(EnumHelper.GetDescription(item.DType, true), this, pt, 5000);
        }
        #endregion

        #region IVDocument 成员
        /// <summary>
        /// 获取文档信息
        /// </summary>
        public event EventHandler<DocEventArgs> GetDocument;
        /// <summary>
        /// 以进程方式打开文档
        /// </summary>
        public event EventHandler<DocEventArgs> OpenDocument_Process;
        /// <summary>
        /// 选择打开方式
        /// </summary>
        public event EventHandler<DocEventArgs> OpenDocument_OpenWith;
        /// <summary>
        /// 预览文档
        /// </summary>
        public event EventHandler<DocEventArgs> OpenDocument_Preview;
        /// <summary>
        /// 填充文档信息到界面显示
        /// </summary>
        /// <param name="docList">文档列表集合</param>
        public void FillDocumentInfo(List<DocumentInfo> docList)
        {
            bdscDocument.DataSource = null;
            bdscDocument.DataSource = docList;
        }

        /// <summary>
        /// 预览文件
        /// </summary>
        /// <param name="paramFilePath">文件路径</param>
        public void Document_Preview(string paramFilePath)
        {
            GetPositionAndSize();
            FormFilePreview.Current.Preview(paramFilePath, _location, _size, true);
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 通过业务操作ID获取文档信息
        /// </summary>
        /// <param name="paramOperationID">业务操作ID</param>
        public void GetDocumentByOperationID(Guid paramOperationID)
        {
            this.GetDocument(this, new DocEventArgs() { OperationID = paramOperationID });
        }
        Point _location;
        Size _size;
        bool _isSet = false;
        /// <summary>
        /// 获取当前窗体位置及其大小
        /// </summary>
        private void GetPositionAndSize()
        {
            if (!_isSet)
            {
                Screen scr = Screen.FromPoint(this.Location);
                _location = new Point((int)(scr.WorkingArea.Width * 0.4), ClientUtility.Height);
                int height = scr.WorkingArea.Height - ClientUtility.Height;
                int width = (int)(scr.WorkingArea.Width * 0.6);
                _size = new Size(width, height);
                _isSet = true;
            }
        }

        private bool IsSelectDocument()
        {
            return this.CurrentDocument != null;
        }
        #endregion
    }
}

namespace ICP.FileSystem.UI
{
    partial class BaseDocumentList
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BatManageTool = new DevExpress.XtraBars.BarManager(this.components);
            this.BarMainTool = new DevExpress.XtraBars.Bar();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barItemOpen = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemOpenWith = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemPreview = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemUpload = new DevExpress.XtraBars.BarSubItem();
            this.barItemDownload = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemDelete = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemReupload = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControlList = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.GridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnchkBussiness = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TipType = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BatManageTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // BatManageTool
            // 
            this.BatManageTool.AllowCustomization = false;
            this.BatManageTool.AllowMoveBarOnToolbar = false;
            this.BatManageTool.AllowQuickCustomization = false;
            this.BatManageTool.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.BarMainTool});
            this.BatManageTool.DockControls.Add(this.barDockControlTop);
            this.BatManageTool.DockControls.Add(this.barDockControlBottom);
            this.BatManageTool.DockControls.Add(this.barDockControlLeft);
            this.BatManageTool.DockControls.Add(this.barDockControlRight);
            this.BatManageTool.Form = this;
            this.BatManageTool.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barItemUpload,
            this.barItemOpen,
            this.barItemOpenWith,
            this.barItemPreview,
            this.barItemDelete,
            this.barItemDownload,
            this.barItemReupload,
            this.barRefresh});
            this.BatManageTool.MainMenu = this.BarMainTool;
            this.BatManageTool.MaxItemId = 15;
            // 
            // BarMainTool
            // 
            this.BarMainTool.BarName = "Main menu";
            this.BarMainTool.DockCol = 0;
            this.BarMainTool.DockRow = 0;
            this.BarMainTool.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.BarMainTool.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemOpenWith),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemPreview),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemUpload),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDownload),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemReupload)});
            this.BarMainTool.OptionsBar.MultiLine = true;
            this.BarMainTool.OptionsBar.UseWholeRow = true;
            this.BarMainTool.Text = "Main menu";
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "Refresh";
            this.barRefresh.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Refresh;
            this.barRefresh.Id = 13;
            this.barRefresh.Name = "barRefresh";
            // 
            // barItemOpen
            // 
            this.barItemOpen.Caption = "&Open";
            this.barItemOpen.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemOpen.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Open;
            this.barItemOpen.Id = 3;
            this.barItemOpen.LargeImageIndex = 1;
            this.barItemOpen.Name = "barItemOpen";
            this.barItemOpen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemOpenWith
            // 
            this.barItemOpenWith.Caption = "Open &With";
            this.barItemOpenWith.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemOpenWith.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Open;
            this.barItemOpenWith.Id = 4;
            this.barItemOpenWith.LargeImageIndex = 2;
            this.barItemOpenWith.Name = "barItemOpenWith";
            this.barItemOpenWith.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemPreview
            // 
            this.barItemPreview.Caption = "&Preview";
            this.barItemPreview.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemPreview.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Preview;
            this.barItemPreview.Id = 5;
            this.barItemPreview.LargeImageIndex = 3;
            this.barItemPreview.Name = "barItemPreview";
            this.barItemPreview.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemUpload
            // 
            this.barItemUpload.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barItemUpload.Caption = "&Upload";
            this.barItemUpload.Description = "Upload File";
            this.barItemUpload.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Upload;
            this.barItemUpload.Id = 2;
            this.barItemUpload.LargeImageIndex = 5;
            this.barItemUpload.Name = "barItemUpload";
            this.barItemUpload.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemDownload
            // 
            this.barItemDownload.Caption = "Down&load";
            this.barItemDownload.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemDownload.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Download;
            this.barItemDownload.Id = 7;
            this.barItemDownload.LargeImageIndex = 6;
            this.barItemDownload.Name = "barItemDownload";
            this.barItemDownload.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemDelete
            // 
            this.barItemDelete.Caption = "&Delete";
            this.barItemDelete.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemDelete.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Delete;
            this.barItemDelete.Id = 6;
            this.barItemDelete.LargeImageIndex = 0;
            this.barItemDelete.Name = "barItemDelete";
            this.barItemDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemReupload
            // 
            this.barItemReupload.Caption = "&Reupload";
            this.barItemReupload.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemReupload.Glyph = global::ICP.FileSystem.UI.Properties.Resources.Return;
            this.barItemReupload.Id = 7;
            this.barItemReupload.LargeImageIndex = 5;
            this.barItemReupload.Name = "barItemReupload";
            this.barItemReupload.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barItemReupload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(813, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 308);
            this.barDockControlBottom.Size = new System.Drawing.Size(813, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 282);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(813, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 282);
            // 
            // gridControlList
            // 
            this.gridControlList.DataSource = this.bsList;
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.Location = new System.Drawing.Point(0, 26);
            this.gridControlList.MainView = this.GridViewList;
            this.gridControlList.MenuManager = this.BatManageTool;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlList.Size = new System.Drawing.Size(813, 282);
            this.gridControlList.TabIndex = 4;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewList});
            // 
            // bsList
            // 
            this.bsList.DataMember = "DocumentList";
            // 
            // gridViewList
            // 
            this.GridViewList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnId,
            this.gridColumnchkBussiness,
            this.gridColumnDocumentType,
            this.gridColumnName,
            this.gridColumnCreateBy,
            this.gridColumnCreateDate,
            this.gridColumnState});
            this.GridViewList.GridControl = this.gridControlList;
            this.GridViewList.Name = "gridViewList";
            this.GridViewList.OptionsBehavior.Editable = false;
            this.GridViewList.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridViewList.OptionsView.ShowGroupPanel = false;
            this.GridViewList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewList_RowCellClick);
            this.GridViewList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridViewList_MouseMove);
            // 
            // gridColumnId
            // 
            this.gridColumnId.Caption = "Id";
            this.gridColumnId.FieldName = "Id";
            this.gridColumnId.Name = "gridColumnId";
            this.gridColumnId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnId.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnchkBussiness
            // 
            this.gridColumnchkBussiness.Caption = "Choose";
            this.gridColumnchkBussiness.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumnchkBussiness.FieldName = "Selected";
            this.gridColumnchkBussiness.Name = "gridColumnchkBussiness";
            this.gridColumnchkBussiness.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnchkBussiness.OptionsColumn.AllowMove = false;
            this.gridColumnchkBussiness.OptionsColumn.AllowSize = false;
            this.gridColumnchkBussiness.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnchkBussiness.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnchkBussiness.OptionsFilter.AllowFilter = false;
            this.gridColumnchkBussiness.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.gridColumnchkBussiness.Width = 51;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // gridColumnDocumentType
            // 
            this.gridColumnDocumentType.Caption = "Type";
            this.gridColumnDocumentType.FieldName = "DocumentType";
            this.gridColumnDocumentType.Name = "gridColumnDocumentType";
            this.gridColumnDocumentType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDocumentType.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnDocumentType.OptionsFilter.AllowFilter = false;
            this.gridColumnDocumentType.Visible = true;
            this.gridColumnDocumentType.VisibleIndex = 0;
            this.gridColumnDocumentType.Width = 44;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnName.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnName.OptionsFilter.AllowFilter = false;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 264;
            // 
            // gridColumnCreateBy
            // 
            this.gridColumnCreateBy.Caption = "Create By";
            this.gridColumnCreateBy.FieldName = "CreateByName";
            this.gridColumnCreateBy.Name = "gridColumnCreateBy";
            this.gridColumnCreateBy.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnCreateBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnCreateBy.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnCreateBy.OptionsFilter.AllowFilter = false;
            this.gridColumnCreateBy.Visible = true;
            this.gridColumnCreateBy.VisibleIndex = 2;
            this.gridColumnCreateBy.Width = 86;
            // 
            // gridColumnCreateDate
            // 
            this.gridColumnCreateDate.Caption = "Upload Date";
            this.gridColumnCreateDate.DisplayFormat.FormatString = "s";
            this.gridColumnCreateDate.FieldName = "CreateDate";
            this.gridColumnCreateDate.Name = "gridColumnCreateDate";
            this.gridColumnCreateDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnCreateDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnCreateDate.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnCreateDate.OptionsFilter.AllowFilter = false;
            this.gridColumnCreateDate.Visible = true;
            this.gridColumnCreateDate.VisibleIndex = 3;
            this.gridColumnCreateDate.Width = 85;
            // 
            // gridColumnState
            // 
            this.gridColumnState.Caption = "Upload State";
            this.gridColumnState.FieldName = "State";
            this.gridColumnState.Name = "gridColumnState";
            this.gridColumnState.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnState.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnState.OptionsFilter.AllowFilter = false;
            this.gridColumnState.Visible = true;
            this.gridColumnState.VisibleIndex = 4;
            this.gridColumnState.Width = 89;
            // 
            // BaseDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlList);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BaseDocumentList";
            this.Size = new System.Drawing.Size(813, 308);
            ((System.ComponentModel.ISupportInitialize)(this.BatManageTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        /// <summary>
        /// 上传按钮
        /// </summary>
        public DevExpress.XtraBars.BarSubItem barItemUpload;
        private DevExpress.XtraBars.BarLargeButtonItem barItemOpen;
        private DevExpress.XtraBars.BarLargeButtonItem barItemOpenWith;
        private DevExpress.XtraBars.BarLargeButtonItem barItemPreview;
        private DevExpress.XtraBars.BarLargeButtonItem barItemDownload;
        private DevExpress.XtraBars.BarLargeButtonItem barItemDelete;
        private DevExpress.XtraBars.BarLargeButtonItem barItemReupload;
        private DevExpress.XtraGrid.GridControl gridControlList;
        /// <summary>
        /// 网格列表
        /// </summary>
        public DevExpress.XtraGrid.Views.Grid.GridView GridViewList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnState;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnchkBussiness;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDocumentType;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private System.Windows.Forms.ToolTip TipType;
        /// <summary>
        /// 工具栏管理器
        /// </summary>
        public DevExpress.XtraBars.BarManager BatManageTool;
        /// <summary>
        /// 工具栏-主要
        /// </summary>
        public DevExpress.XtraBars.Bar BarMainTool;
    }
}

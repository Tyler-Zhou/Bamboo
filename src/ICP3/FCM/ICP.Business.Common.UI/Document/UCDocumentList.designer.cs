using ICP.DataCache.ServiceInterface;
namespace ICP.Business.Common.UI.Document
{
    partial class UCDocumentList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDocumentList));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTool = new DevExpress.XtraBars.Bar();
            this.barItemUpload = new DevExpress.XtraBars.BarSubItem();
            this.barItemOpen = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemOpenWith = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemPreview = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemDownload = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemDelete = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemReupload = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barEditItemRangs = new DevExpress.XtraBars.BarEditItem();
            this.comboBoxRangs = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barStatus = new DevExpress.XtraBars.Bar();
            this.barItemSyncErrorText = new DevExpress.XtraBars.BarStaticItem();
            this.btnItemBusinessSync = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.barAccepted = new DevExpress.XtraBars.BarButtonItem();
            this.barUnAccepted = new DevExpress.XtraBars.BarButtonItem();
            this.barTransition = new DevExpress.XtraBars.BarButtonItem();
            this.barAssignTo = new DevExpress.XtraBars.BarButtonItem();
            this.gridControlList = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnchkBussiness = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRangs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTool,
            this.barStatus});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imageList;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barItemSyncErrorText,
            this.btnItemBusinessSync,
            this.barItemUpload,
            this.barItemOpen,
            this.barItemOpenWith,
            this.barItemPreview,
            this.barItemDelete,
            this.barItemDownload,
            this.barItemReupload,
            this.barAccepted,
            this.barUnAccepted,
            this.barTransition,
            this.barAssignTo,
            this.barRefresh,
            this.barEditItemRangs});
            this.barManager.LargeImages = this.imageList;
            this.barManager.MainMenu = this.barTool;
            this.barManager.MaxItemId = 15;
            this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.comboBoxRangs});
            this.barManager.StatusBar = this.barStatus;
            this.barManager.HighlightedLinkChanged += new DevExpress.XtraBars.HighlightedLinkChangedEventHandler(this.barManager_HighlightedLinkChanged);
            // 
            // barTool
            // 
            this.barTool.BarName = "Main menu";
            this.barTool.DockCol = 0;
            this.barTool.DockRow = 0;
            this.barTool.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTool.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemUpload),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemOpenWith),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemPreview),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDownload),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemReupload),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItemRangs),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTool.OptionsBar.MultiLine = true;
            this.barTool.OptionsBar.UseWholeRow = true;
            this.barTool.Text = "Main menu";
            // 
            // barItemUpload
            // 
            this.barItemUpload.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barItemUpload.Caption = "&Upload";
            this.barItemUpload.Description = "Upload File";
            this.barItemUpload.Id = 2;
            this.barItemUpload.LargeImageIndex = 5;
            this.barItemUpload.Name = "barItemUpload";
            this.barItemUpload.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemOpen
            // 
            this.barItemOpen.Caption = "&Open";
            this.barItemOpen.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemOpen.Id = 3;
            this.barItemOpen.LargeImageIndex = 1;
            this.barItemOpen.Name = "barItemOpen";
            this.barItemOpen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemOpenWith
            // 
            this.barItemOpenWith.Caption = "Open &With";
            this.barItemOpenWith.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemOpenWith.Id = 4;
            this.barItemOpenWith.LargeImageIndex = 2;
            this.barItemOpenWith.Name = "barItemOpenWith";
            this.barItemOpenWith.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemPreview
            // 
            this.barItemPreview.Caption = "&Preview";
            this.barItemPreview.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemPreview.Id = 5;
            this.barItemPreview.LargeImageIndex = 3;
            this.barItemPreview.Name = "barItemPreview";
            this.barItemPreview.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemDownload
            // 
            this.barItemDownload.Caption = "Down&load";
            this.barItemDownload.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemDownload.Id = 7;
            this.barItemDownload.LargeImageIndex = 6;
            this.barItemDownload.Name = "barItemDownload";
            this.barItemDownload.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemDelete
            // 
            this.barItemDelete.Caption = "&Delete";
            this.barItemDelete.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemDelete.Id = 6;
            this.barItemDelete.LargeImageIndex = 0;
            this.barItemDelete.Name = "barItemDelete";
            this.barItemDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemReupload
            // 
            this.barItemReupload.Caption = "&Reupload";
            this.barItemReupload.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemReupload.Id = 7;
            this.barItemReupload.LargeImageIndex = 5;
            this.barItemReupload.Name = "barItemReupload";
            this.barItemReupload.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barItemReupload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barEditItemRangs
            // 
            this.barEditItemRangs.Edit = this.comboBoxRangs;
            this.barEditItemRangs.Id = 14;
            this.barEditItemRangs.Name = "barEditItemRangs";
            this.barEditItemRangs.Width = 80;
            // 
            // comboBoxRangs
            // 
            this.comboBoxRangs.AutoHeight = false;
            this.comboBoxRangs.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxRangs.Name = "comboBoxRangs";
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "Refresh";
            this.barRefresh.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Refresh_161;
            this.barRefresh.Id = 13;
            this.barRefresh.Name = "barRefresh";
            // 
            // barStatus
            // 
            this.barStatus.BarName = "Status bar";
            this.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barStatus.DockCol = 0;
            this.barStatus.DockRow = 0;
            this.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barStatus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSyncErrorText),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItemBusinessSync)});
            this.barStatus.OptionsBar.AllowQuickCustomization = false;
            this.barStatus.OptionsBar.DrawDragBorder = false;
            this.barStatus.OptionsBar.UseWholeRow = true;
            this.barStatus.Text = "Status bar";
            this.barStatus.Visible = false;
            // 
            // barItemSyncErrorText
            // 
            this.barItemSyncErrorText.Appearance.ForeColor = System.Drawing.Color.Red;
            this.barItemSyncErrorText.Appearance.Options.UseForeColor = true;
            this.barItemSyncErrorText.Caption = "Error in synchronizing document data,Please synchronize manually.";
            this.barItemSyncErrorText.Id = 0;
            this.barItemSyncErrorText.Name = "barItemSyncErrorText";
            this.barItemSyncErrorText.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btnItemBusinessSync
            // 
            this.btnItemBusinessSync.Caption = "&Synchronize";
            this.btnItemBusinessSync.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.btnItemBusinessSync.Id = 1;
            this.btnItemBusinessSync.ImageIndex = 4;
            this.btnItemBusinessSync.Name = "btnItemBusinessSync";
            this.btnItemBusinessSync.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
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
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 278);
            this.barDockControlBottom.Size = new System.Drawing.Size(813, 30);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 252);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(813, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 252);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "delete_16.png");
            this.imageList.Images.SetKeyName(1, "open.png");
            this.imageList.Images.SetKeyName(2, "openwith.png");
            this.imageList.Images.SetKeyName(3, "preview.png");
            this.imageList.Images.SetKeyName(4, "sync.png");
            this.imageList.Images.SetKeyName(5, "upload.png");
            this.imageList.Images.SetKeyName(6, "Down_16.png");
            this.imageList.Images.SetKeyName(7, "docOpen.png");
            this.imageList.Images.SetKeyName(8, "docPreview.png");
            this.imageList.Images.SetKeyName(9, "docWith.png");
            this.imageList.Images.SetKeyName(10, "open2.png");
            // 
            // barAccepted
            // 
            this.barAccepted.Caption = "&Accepted";
            this.barAccepted.Id = 9;
            this.barAccepted.Name = "barAccepted";
            this.barAccepted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barUnAccepted
            // 
            this.barUnAccepted.Caption = "&Un-Accepted";
            this.barUnAccepted.Id = 10;
            this.barUnAccepted.Name = "barUnAccepted";
            this.barUnAccepted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barTransition
            // 
            this.barTransition.Caption = "&Transition";
            this.barTransition.Id = 11;
            this.barTransition.Name = "barTransition";
            this.barTransition.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barAssignTo
            // 
            this.barAssignTo.Caption = "Assign &To";
            this.barAssignTo.Id = 12;
            this.barAssignTo.Name = "barAssignTo";
            this.barAssignTo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // gridControlList
            // 
            this.gridControlList.DataSource = this.bindingSource;
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.Location = new System.Drawing.Point(0, 26);
            this.gridControlList.MainView = this.gridViewList;
            this.gridControlList.MenuManager = this.barManager;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlList.Size = new System.Drawing.Size(813, 252);
            this.gridControlList.TabIndex = 4;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "DocumentList";
            this.bindingSource.DataSource = typeof(ICP.Business.Common.UI.Document.DocumentListPresenter);
            // 
            // gridViewList
            // 
            this.gridViewList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnId,
            this.gridColumnchkBussiness,
            this.gridColumnDocumentType,
            this.gridColumnName,
            this.gridColumnCreateBy,
            this.gridColumnCreateDate,
            this.gridColumnState});
            this.gridViewList.GridControl = this.gridControlList;
            this.gridViewList.Name = "gridViewList";
            this.gridViewList.OptionsBehavior.Editable = false;
            this.gridViewList.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewList.OptionsView.ShowGroupPanel = false;
            this.gridViewList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewList_RowCellClick);
            this.gridViewList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridViewList_MouseMove);
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
            // UCDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlList);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCDocumentList";
            this.Size = new System.Drawing.Size(813, 308);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRangs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        public DevExpress.XtraBars.Bar barTool;
        private DevExpress.XtraBars.Bar barStatus;
        private DevExpress.XtraBars.BarStaticItem barItemSyncErrorText;
        private DevExpress.XtraBars.BarLargeButtonItem btnItemBusinessSync;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList;
        public DevExpress.XtraBars.BarSubItem barItemUpload;
        public DevExpress.XtraBars.BarLargeButtonItem barItemOpen;
        public DevExpress.XtraBars.BarLargeButtonItem barItemOpenWith;
        public DevExpress.XtraBars.BarLargeButtonItem barItemPreview;
        public DevExpress.XtraBars.BarLargeButtonItem barItemDownload;
        public DevExpress.XtraBars.BarLargeButtonItem barItemDelete;
        public DevExpress.XtraBars.BarLargeButtonItem barItemReupload;
        private DevExpress.XtraGrid.GridControl gridControlList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
        private System.Windows.Forms.BindingSource bindingSource;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnCreateBy;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnCreateDate;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnState;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnchkBussiness;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        public DevExpress.XtraBars.BarButtonItem barAccepted;
        public DevExpress.XtraBars.BarButtonItem barUnAccepted;
        public DevExpress.XtraBars.BarButtonItem barTransition;
        public DevExpress.XtraBars.BarButtonItem barAssignTo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDocumentType;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevExpress.XtraBars.BarEditItem barEditItemRangs;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox comboBoxRangs;
       
    }
}

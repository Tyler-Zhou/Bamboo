namespace ICP.OA.UI
{
    partial class BDocMainViewPart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BDocMainViewPart));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeMain = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lvMain = new System.Windows.Forms.ListView();
            this.colhName = new System.Windows.Forms.ColumnHeader();
            this.colhUpdateDate = new System.Windows.Forms.ColumnHeader();
            this.colhCreateBy = new System.Windows.Forms.ColumnHeader();
            this.colhCreateDate = new System.Windows.Forms.ColumnHeader();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAddFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barUpLoadFile = new DevExpress.XtraBars.BarButtonItem();
            this.barDeleteBar = new DevExpress.XtraBars.BarButtonItem();
            this.barBreak = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barViewStyle = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuViewStyle = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barLargeIcon = new DevExpress.XtraBars.BarButtonItem();
            this.barDetails = new DevExpress.XtraBars.BarButtonItem();
            this.barSmallIcon = new DevExpress.XtraBars.BarButtonItem();
            this.barList = new DevExpress.XtraBars.BarButtonItem();
            this.barTile = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDownLoadFile = new DevExpress.XtraBars.BarButtonItem();
            this.barLvNewFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barLvUpLoadFile = new DevExpress.XtraBars.BarButtonItem();
            this.barLvDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barLvDownLoadFile = new DevExpress.XtraBars.BarButtonItem();
            this.barLvReName = new DevExpress.XtraBars.BarButtonItem();
            this.barReName = new DevExpress.XtraBars.BarButtonItem();
            this.barAddlvFolderBar = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuTree = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuLv = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bsFileList = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuViewStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuLv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFileList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Collapsed = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 26);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(784, 357);
            this.splitContainerControl1.SplitterPosition = 214;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeMain
            // 
            this.treeMain.AllowDrop = true;
            this.treeMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeMain.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.DragNodes = true;
            this.treeMain.OptionsView.ShowColumns = false;
            this.treeMain.OptionsView.ShowHorzLines = false;
            this.treeMain.OptionsView.ShowIndicator = false;
            this.treeMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rTextEdit1});
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(214, 357);
            this.treeMain.StateImageList = this.imageList1;
            this.treeMain.TabIndex = 0;
            this.treeMain.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeMain_AfterDragNode);
            this.treeMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeMain_KeyDown);
            this.treeMain.BeforeDragNode += new DevExpress.XtraTreeList.BeforeDragNodeEventHandler(this.treeMain_BeforeDragNode);
            this.treeMain.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.treeMain_BeforeFocusNode);
            this.treeMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeMain_MouseClick);
            this.treeMain.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeMain_GetStateImage);
            this.treeMain.Leave += new System.EventHandler(this.treeMain_Leave);
            this.treeMain.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeMain_CellValueChanged);
            this.treeMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeMain_DragDrop);
            this.treeMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeMain_DragEnter);
            // 
            // colName
            // 
            this.colName.ColumnEdit = this.rTextEdit1;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 656;
            // 
            // rTextEdit1
            // 
            this.rTextEdit1.AutoHeight = false;
            this.rTextEdit1.Name = "rTextEdit1";
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.OA.ServiceInterface.DataObjects.DocumentFolderFileList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsFolderFile_PositionChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Data_16.png");
            this.imageList1.Images.SetKeyName(1, "Folder_16.png");
            this.imageList1.Images.SetKeyName(2, "Folder_Edit_16.png");
            this.imageList1.Images.SetKeyName(3, "Folder_Lock_16.png");
            this.imageList1.Images.SetKeyName(4, "BlueFile_16.png");
            this.imageList1.Images.SetKeyName(5, "BlueFile_Edit_16.png");
            this.imageList1.Images.SetKeyName(6, "BlueFile_Lock_16.png");
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lvMain);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(564, 357);
            this.panelControl1.TabIndex = 1;
            // 
            // lvMain
            // 
            this.lvMain.AllowDrop = true;
            this.lvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colhName,
            this.colhUpdateDate,
            this.colhCreateBy,
            this.colhCreateDate});
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.LabelEdit = true;
            this.lvMain.LargeImageList = this.imageList2;
            this.lvMain.Location = new System.Drawing.Point(2, 2);
            this.lvMain.Name = "lvMain";
            this.lvMain.ShowItemToolTips = true;
            this.lvMain.Size = new System.Drawing.Size(560, 353);
            this.lvMain.SmallImageList = this.imageList1;
            this.lvMain.TabIndex = 0;
            this.lvMain.TileSize = new System.Drawing.Size(100, 60);
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDoubleClick);
            this.lvMain.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvMain_AfterLabelEdit);
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            this.lvMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseUp);
            this.lvMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvMain_DragDrop);
            this.lvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMain_ColumnClick);
            this.lvMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseMove);
            this.lvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDown);
            this.lvMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvMain_DragEnter);
            this.lvMain.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvMain_BeforeLabelEdit);
            this.lvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMain_KeyDown);
            this.lvMain.MouseLeave += new System.EventHandler(this.lvMain_MouseLeave);
            // 
            // colhName
            // 
            this.colhName.Text = "Name";
            this.colhName.Width = 185;
            // 
            // colhUpdateDate
            // 
            this.colhUpdateDate.Text = "UpdateDate";
            this.colhUpdateDate.Width = 122;
            // 
            // colhCreateBy
            // 
            this.colhCreateBy.Text = "CreateBy";
            this.colhCreateBy.Width = 115;
            // 
            // colhCreateDate
            // 
            this.colhCreateDate.Text = "CreateDate";
            this.colhCreateDate.Width = 134;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Data_16.png");
            this.imageList2.Images.SetKeyName(1, "Folder_32.png");
            this.imageList2.Images.SetKeyName(2, "Folder_Edit_32.png");
            this.imageList2.Images.SetKeyName(3, "Folder_Lock_32.png");
            this.imageList2.Images.SetKeyName(4, "BlueFile_32.png");
            this.imageList2.Images.SetKeyName(5, "BlueFile_Edit_32.png");
            this.imageList2.Images.SetKeyName(6, "BlueFile_Lock_32.png");
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAddFolder,
            this.barUpLoadFile,
            this.barDelete,
            this.barClose,
            this.barRefresh,
            this.barViewStyle,
            this.barDetails,
            this.barLargeIcon,
            this.barList,
            this.barSmallIcon,
            this.barTile,
            this.barDownLoadFile,
            this.barLvNewFolder,
            this.barLvUpLoadFile,
            this.barLvDelete,
            this.barLvDownLoadFile,
            this.barLvReName,
            this.barReName,
            this.barDeleteBar,
            this.barBreak,
            this.barAddlvFolderBar});
            this.barManager1.MaxItemId = 22;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddFolder, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barUpLoadFile, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDeleteBar, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBreak, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barViewStyle, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barAddFolder
            // 
            this.barAddFolder.Caption = "&New Folder";
            this.barAddFolder.Glyph = global::ICP.OA.UI.Properties.Resources.AddFolder_S;
            this.barAddFolder.Id = 0;
            this.barAddFolder.Name = "barAddFolder";
            this.barAddFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddFolder_ItemClick);
            // 
            // barUpLoadFile
            // 
            this.barUpLoadFile.Caption = "&UpLoad File";
            this.barUpLoadFile.Glyph = global::ICP.OA.UI.Properties.Resources.Add_File_16;
            this.barUpLoadFile.Id = 1;
            this.barUpLoadFile.Name = "barUpLoadFile";
            this.barUpLoadFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUpLoadFile_ItemClick);
            // 
            // barDeleteBar
            // 
            this.barDeleteBar.Caption = "&Delete";
            this.barDeleteBar.Glyph = global::ICP.OA.UI.Properties.Resources.Delete_S;
            this.barDeleteBar.Id = 19;
            this.barDeleteBar.Name = "barDeleteBar";
            this.barDeleteBar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDeleteBar_ItemClick);
            // 
            // barBreak
            // 
            this.barBreak.Caption = "&Break";
            this.barBreak.Glyph = global::ICP.OA.UI.Properties.Resources.UP_16;
            this.barBreak.Id = 20;
            this.barBreak.Name = "barBreak";
            this.barBreak.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBreak_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = global::ICP.OA.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Id = 5;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barViewStyle
            // 
            this.barViewStyle.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barViewStyle.Caption = "LargeIcon";
            this.barViewStyle.DropDownControl = this.popupMenuViewStyle;
            this.barViewStyle.Glyph = global::ICP.OA.UI.Properties.Resources.Center_16;
            this.barViewStyle.Id = 6;
            this.barViewStyle.Name = "barViewStyle";
            this.barViewStyle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barViewStyle_ItemClick);
            // 
            // popupMenuViewStyle
            // 
            this.popupMenuViewStyle.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeIcon),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDetails),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSmallIcon),
            new DevExpress.XtraBars.LinkPersistInfo(this.barList),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTile)});
            this.popupMenuViewStyle.Manager = this.barManager1;
            this.popupMenuViewStyle.Name = "popupMenuViewStyle";
            // 
            // barLargeIcon
            // 
            this.barLargeIcon.Caption = "LargeIcon";
            this.barLargeIcon.Id = 8;
            this.barLargeIcon.Name = "barLargeIcon";
            this.barLargeIcon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeIcon_ItemClick);
            // 
            // barDetails
            // 
            this.barDetails.Caption = "Details";
            this.barDetails.Id = 7;
            this.barDetails.Name = "barDetails";
            this.barDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDetails_ItemClick);
            // 
            // barSmallIcon
            // 
            this.barSmallIcon.Caption = "SmallIcon";
            this.barSmallIcon.Id = 10;
            this.barSmallIcon.Name = "barSmallIcon";
            this.barSmallIcon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSmallIcon_ItemClick);
            // 
            // barList
            // 
            this.barList.Caption = "List";
            this.barList.Id = 9;
            this.barList.Name = "barList";
            this.barList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barList_ItemClick);
            // 
            // barTile
            // 
            this.barTile.Caption = "Tile";
            this.barTile.Id = 11;
            this.barTile.Name = "barTile";
            this.barTile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTile_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.OA.UI.Properties.Resources.Close_16;
            this.barClose.Id = 4;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(784, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 383);
            this.barDockControlBottom.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 357);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(784, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 357);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.OA.UI.Properties.Resources.Delete_S;
            this.barDelete.Id = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barDownLoadFile
            // 
            this.barDownLoadFile.Caption = "Down&Load File";
            this.barDownLoadFile.Glyph = global::ICP.OA.UI.Properties.Resources.Down_16;
            this.barDownLoadFile.Id = 12;
            this.barDownLoadFile.Name = "barDownLoadFile";
            this.barDownLoadFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDownLoadFile_ItemClick);
            // 
            // barLvNewFolder
            // 
            this.barLvNewFolder.Caption = "&New Folder";
            this.barLvNewFolder.Glyph = global::ICP.OA.UI.Properties.Resources.AddFolder_S;
            this.barLvNewFolder.Id = 13;
            this.barLvNewFolder.Name = "barLvNewFolder";
            this.barLvNewFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLvNewFolder_ItemClick);
            // 
            // barLvUpLoadFile
            // 
            this.barLvUpLoadFile.Caption = "&UpLoad File";
            this.barLvUpLoadFile.Glyph = global::ICP.OA.UI.Properties.Resources.Add_File_16;
            this.barLvUpLoadFile.Id = 14;
            this.barLvUpLoadFile.Name = "barLvUpLoadFile";
            this.barLvUpLoadFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLvUpLoadFile_ItemClick);
            // 
            // barLvDelete
            // 
            this.barLvDelete.Caption = "&Delete";
            this.barLvDelete.Glyph = global::ICP.OA.UI.Properties.Resources.Delete_S;
            this.barLvDelete.Id = 15;
            this.barLvDelete.Name = "barLvDelete";
            this.barLvDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLvDelete_ItemClick);
            // 
            // barLvDownLoadFile
            // 
            this.barLvDownLoadFile.Caption = "Down&Load File";
            this.barLvDownLoadFile.Glyph = global::ICP.OA.UI.Properties.Resources.Down_16;
            this.barLvDownLoadFile.Id = 16;
            this.barLvDownLoadFile.Name = "barLvDownLoadFile";
            this.barLvDownLoadFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLvDownLoadFile_ItemClick);
            // 
            // barLvReName
            // 
            this.barLvReName.Caption = "R&eName";
            this.barLvReName.Glyph = global::ICP.OA.UI.Properties.Resources.ReNameFolder_16;
            this.barLvReName.Id = 17;
            this.barLvReName.Name = "barLvReName";
            this.barLvReName.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLvReName_ItemClick);
            // 
            // barReName
            // 
            this.barReName.Caption = "R&eName";
            this.barReName.Glyph = global::ICP.OA.UI.Properties.Resources.ReNameFolder_16;
            this.barReName.Id = 18;
            this.barReName.Name = "barReName";
            this.barReName.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReName_ItemClick);
            // 
            // barAddlvFolderBar
            // 
            this.barAddlvFolderBar.Caption = "&New Folder";
            this.barAddlvFolderBar.Glyph = global::ICP.OA.UI.Properties.Resources.AddFolder_S;
            this.barAddlvFolderBar.Id = 21;
            this.barAddlvFolderBar.Name = "barAddlvFolderBar";
            this.barAddlvFolderBar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddlvFolderBar_ItemClick);
            // 
            // popupMenuTree
            // 
            this.popupMenuTree.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUpLoadFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReName)});
            this.popupMenuTree.Manager = this.barManager1;
            this.popupMenuTree.Name = "popupMenuTree";
            // 
            // popupMenuLv
            // 
            this.popupMenuLv.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLvDownLoadFile, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLvDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLvReName)});
            this.popupMenuLv.Manager = this.barManager1;
            this.popupMenuLv.Name = "popupMenuLv";
            // 
            // BDocMainViewPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BDocMainViewPart";
            this.Size = new System.Drawing.Size(784, 383);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuViewStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuLv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFileList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAddFolder;
        private DevExpress.XtraBars.BarButtonItem barUpLoadFile;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraBars.PopupMenu popupMenuTree;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraBars.BarButtonItem barViewStyle;
        private DevExpress.XtraBars.PopupMenu popupMenuViewStyle;
        private DevExpress.XtraBars.BarButtonItem barDetails;
        private DevExpress.XtraBars.BarButtonItem barLargeIcon;
        private DevExpress.XtraBars.BarButtonItem barList;
        private DevExpress.XtraBars.BarButtonItem barSmallIcon;
        private DevExpress.XtraBars.BarButtonItem barTile;
        private System.Windows.Forms.ColumnHeader colhName;
        private System.Windows.Forms.ColumnHeader colhUpdateDate;
        private System.Windows.Forms.ColumnHeader colhCreateBy;
        private System.Windows.Forms.ColumnHeader colhCreateDate;
        private DevExpress.XtraBars.BarButtonItem barDownLoadFile;
        private DevExpress.XtraBars.BarButtonItem barLvDelete;
        private DevExpress.XtraBars.BarButtonItem barLvNewFolder;
        private DevExpress.XtraBars.BarButtonItem barLvUpLoadFile;
        private DevExpress.XtraBars.PopupMenu popupMenuLv;
        private DevExpress.XtraBars.BarButtonItem barLvDownLoadFile;
        private DevExpress.XtraBars.BarButtonItem barLvReName;
        private DevExpress.XtraBars.BarButtonItem barReName;
        private DevExpress.XtraBars.BarButtonItem barDeleteBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barBreak;
        private DevExpress.XtraBars.BarButtonItem barAddlvFolderBar;
        private System.Windows.Forms.BindingSource bsFileList;
    }
}

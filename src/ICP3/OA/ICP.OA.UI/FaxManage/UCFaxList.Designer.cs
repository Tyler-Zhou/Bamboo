namespace ICP.OA.UI.FaxManage
{
    partial class UCFaxList
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
                this.treeFolder.Dispose();
                this.treeReceive.Dispose();
                this.gvMailList.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFaxList));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.bsMailList = new System.Windows.Forms.BindingSource(this.components);
            this.imageListPriority = new System.Windows.Forms.ImageList(this.components);
            this.imageListOther = new System.Windows.Forms.ImageList(this.components);
            this.imageListFlag = new System.Windows.Forms.ImageList(this.components);
            this.imageListState = new System.Windows.Forms.ImageList(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barNewEmail = new DevExpress.XtraBars.BarButtonItem();
            this.barRevert = new DevExpress.XtraBars.BarButtonItem();
            this.barRevertAll = new DevExpress.XtraBars.BarButtonItem();
            this.barTSend = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barSAndR = new DevExpress.XtraBars.BarButtonItem();
            this.barAccepted = new DevExpress.XtraBars.BarButtonItem();
            this.barReturn = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barAddFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barReNameFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barDeleteFolder = new DevExpress.XtraBars.BarButtonItem();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpFolder = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pnlSendFax = new BSE.Windows.Forms.Panel();
            this.treeFolder = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFolderID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsSend = new System.Windows.Forms.BindingSource(this.components);
            this.pnlReceiveFax = new BSE.Windows.Forms.Panel();
            this.treeReceive = new DevExpress.XtraTreeList.TreeList();
            this.colCompanyName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCompanyID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsReceive = new System.Windows.Forms.BindingSource(this.components);
            this.imageListOrgainization = new System.Windows.Forms.ImageList(this.components);
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.splitMainList = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMailList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colMailFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate_Time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMailTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMailCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriority = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbPriority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rcmbAttachment = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rcmbFlag = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rtxtTime = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.pnlSearchPart = new DevExpress.XtraEditors.PanelControl();
            this.pnlFastSearch = new DevExpress.XtraEditors.PanelControl();
            this.btnFastSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtKeyWord = new DevExpress.XtraEditors.TextEdit();
            this.ucFaxQuery = new ICP.OA.UI.FaxManage.UCFaxQuery();
            this.popupMenuAttachment = new DevExpress.XtraBars.PopupMenu(this.components);
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.popupMenuFolder = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuMessageList = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsMailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpFolder.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.pnlSendFax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSend)).BeginInit();
            this.pnlReceiveFax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainList)).BeginInit();
            this.splitMainList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearchPart)).BeginInit();
            this.pnlSearchPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFastSearch)).BeginInit();
            this.pnlFastSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuMessageList)).BeginInit();
            this.SuspendLayout();
            // 
            // bsMailList
            // 
            this.bsMailList.DataSource = typeof(ICP.OA.ServiceInterface.DataObjects.MailMessageList);
            this.bsMailList.PositionChanged += new System.EventHandler(this.bsMailList_PositionChanged);
            // 
            // imageListPriority
            // 
            this.imageListPriority.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPriority.ImageStream")));
            this.imageListPriority.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListPriority.Images.SetKeyName(0, "7575.png");
            this.imageListPriority.Images.SetKeyName(1, "High");
            this.imageListPriority.Images.SetKeyName(2, "Low");
            // 
            // imageListOther
            // 
            this.imageListOther.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOther.ImageStream")));
            this.imageListOther.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListOther.Images.SetKeyName(0, "");
            this.imageListOther.Images.SetKeyName(1, "");
            this.imageListOther.Images.SetKeyName(2, "1_0135.ico");
            // 
            // imageListFlag
            // 
            this.imageListFlag.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFlag.ImageStream")));
            this.imageListFlag.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListFlag.Images.SetKeyName(0, "1_0049.ico");
            this.imageListFlag.Images.SetKeyName(1, "UnRead");
            this.imageListFlag.Images.SetKeyName(2, "Readed");
            this.imageListFlag.Images.SetKeyName(3, "Revert_S.ico");
            this.imageListFlag.Images.SetKeyName(4, "Ts_S.ico");
            // 
            // imageListState
            // 
            this.imageListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListState.ImageStream")));
            this.imageListState.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListState.Images.SetKeyName(0, "send.png");
            this.imageListState.Images.SetKeyName(1, "sucessed.png");
            this.imageListState.Images.SetKeyName(2, "fail.png");
            this.imageListState.Images.SetKeyName(3, "copy.gif");
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barNewEmail,
            this.barAddFolder,
            this.barReNameFolder,
            this.barDeleteFolder,
            this.barRevert,
            this.barRevertAll,
            this.barTSend,
            this.barPrint,
            this.barSAndR,
            this.barReturn,
            this.barDelete,
            this.barClose,
            this.barAccepted});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 15;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewEmail, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRevert, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRevertAll, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barTSend, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSAndR, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAccepted, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReturn, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barNewEmail
            // 
            this.barNewEmail.Caption = "&New";
            this.barNewEmail.Glyph = global::ICP.OA.UI.Properties.Resources.Mail;
            this.barNewEmail.Id = 0;
            this.barNewEmail.Name = "barNewEmail";
            this.barNewEmail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewEmail_ItemClick);
            // 
            // barRevert
            // 
            this.barRevert.Caption = "&Revert";
            this.barRevert.Glyph = global::ICP.OA.UI.Properties.Resources.R;
            this.barRevert.Id = 4;
            this.barRevert.Name = "barRevert";
            this.barRevert.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRevert_ItemClick);
            // 
            // barRevertAll
            // 
            this.barRevertAll.Caption = "&RevertAll";
            this.barRevertAll.Glyph = global::ICP.OA.UI.Properties.Resources.R_All;
            this.barRevertAll.Id = 5;
            this.barRevertAll.Name = "barRevertAll";
            this.barRevertAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRevertAll_ItemClick);
            // 
            // barTSend
            // 
            this.barTSend.Caption = "&Transfer";
            this.barTSend.Glyph = global::ICP.OA.UI.Properties.Resources.Ts;
            this.barTSend.Id = 6;
            this.barTSend.Name = "barTSend";
            this.barTSend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTSend_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "&Print";
            this.barPrint.Enabled = false;
            this.barPrint.Glyph = global::ICP.OA.UI.Properties.Resources.Print;
            this.barPrint.Id = 8;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barSAndR
            // 
            this.barSAndR.Caption = "Send/&Receive";
            this.barSAndR.Enabled = false;
            this.barSAndR.Glyph = ((System.Drawing.Image)(resources.GetObject("barSAndR.Glyph")));
            this.barSAndR.Id = 9;
            this.barSAndR.Name = "barSAndR";
            this.barSAndR.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSAndR_ItemClick);
            // 
            // barAccepted
            // 
            this.barAccepted.Caption = "Accept";
            this.barAccepted.Glyph = global::ICP.OA.UI.Properties.Resources.arrow_down_5;
            this.barAccepted.Id = 14;
            this.barAccepted.Name = "barAccepted";
            this.barAccepted.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAccepted_ItemClick);
            // 
            // barReturn
            // 
            this.barReturn.Caption = "Return";
            this.barReturn.Glyph = global::ICP.OA.UI.Properties.Resources.Return_16;
            this.barReturn.Id = 13;
            this.barReturn.Name = "barReturn";
            this.barReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReturn_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.OA.UI.Properties.Resources.Delete;
            this.barDelete.Id = 7;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "Close";
            this.barClose.Glyph = global::ICP.OA.UI.Properties.Resources.Transfer;
            this.barClose.Id = 10;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1034, 42);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 604);
            this.barDockControlBottom.Size = new System.Drawing.Size(1034, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 42);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 562);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1034, 42);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 562);
            // 
            // barAddFolder
            // 
            this.barAddFolder.Caption = "Add Folder";
            this.barAddFolder.Glyph = global::ICP.OA.UI.Properties.Resources.AddFolder_S;
            this.barAddFolder.Id = 1;
            this.barAddFolder.Name = "barAddFolder";
            this.barAddFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddFolder_ItemClick);
            // 
            // barReNameFolder
            // 
            this.barReNameFolder.Caption = "ReName";
            this.barReNameFolder.Glyph = global::ICP.OA.UI.Properties.Resources.Fold_S;
            this.barReNameFolder.Id = 2;
            this.barReNameFolder.Name = "barReNameFolder";
            this.barReNameFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReName_ItemClick);
            // 
            // barDeleteFolder
            // 
            this.barDeleteFolder.Caption = "Delete";
            this.barDeleteFolder.Glyph = global::ICP.OA.UI.Properties.Resources.RemoveFolder_S;
            this.barDeleteFolder.Id = 3;
            this.barDeleteFolder.Name = "barDeleteFolder";
            this.barDeleteFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDeleteFolder_ItemClick);
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpFolder});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dpFolder
            // 
            this.dpFolder.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpFolder.Appearance.Options.UseBackColor = true;
            this.dpFolder.Controls.Add(this.dockPanel1_Container);
            this.dpFolder.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpFolder.ID = new System.Guid("58959204-ddd1-4e3e-a3f3-957830ada140");
            this.dpFolder.Location = new System.Drawing.Point(0, 42);
            this.dpFolder.Name = "dpFolder";
            this.dpFolder.OriginalSize = new System.Drawing.Size(189, 200);
            this.dpFolder.Size = new System.Drawing.Size(189, 562);
            this.dpFolder.Text = "Folder";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.panelControl1);
            this.dockPanel1_Container.Controls.Add(this.pnlReceiveFax);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(183, 534);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pnlSendFax);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 434);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(183, 100);
            this.panelControl1.TabIndex = 1;
            // 
            // pnlSendFax
            // 
            this.pnlSendFax.BackColor = System.Drawing.Color.Transparent;
            this.pnlSendFax.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.pnlSendFax.CaptionFont = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.pnlSendFax.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlSendFax.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlSendFax.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlSendFax.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(177)))), ((int)(((byte)(250)))));
            this.pnlSendFax.ColorCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(145)))));
            this.pnlSendFax.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(127)))), ((int)(((byte)(208)))));
            this.pnlSendFax.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.pnlSendFax.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.pnlSendFax.Controls.Add(this.treeFolder);
            this.pnlSendFax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSendFax.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlSendFax.Image = null;
            this.pnlSendFax.InnerBorderColor = System.Drawing.Color.White;
            this.pnlSendFax.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnlSendFax.Location = new System.Drawing.Point(2, 2);
            this.pnlSendFax.Name = "pnlSendFax";
            this.pnlSendFax.PanelStyle = BSE.Windows.Forms.PanelStyle.Default;
            this.pnlSendFax.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlSendFax.ShowExpandIcon = true;
            this.pnlSendFax.ShowXPanderPanelProfessionalStyle = true;
            this.pnlSendFax.Size = new System.Drawing.Size(179, 96);
            this.pnlSendFax.TabIndex = 8;
            this.pnlSendFax.Text = "个人文件夹";
            this.pnlSendFax.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlSendFax_MouseClick);
            // 
            // treeFolder
            // 
            this.treeFolder.AllowDrop = true;
            this.treeFolder.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeFolder.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeFolder.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeFolder.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeFolder.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeFolder.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.treeFolder.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.treeFolder.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.treeFolder.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName,
            this.colFolderID});
            this.treeFolder.DataSource = this.bsSend;
            this.treeFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFolder.Location = new System.Drawing.Point(1, 26);
            this.treeFolder.Name = "treeFolder";
            this.treeFolder.OptionsBehavior.DragNodes = true;
            this.treeFolder.OptionsBehavior.Editable = false;
            this.treeFolder.OptionsView.ShowColumns = false;
            this.treeFolder.OptionsView.ShowIndicator = false;
            this.treeFolder.Size = new System.Drawing.Size(177, 69);
            this.treeFolder.TabIndex = 1;
            this.treeFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeFolder_MouseDown);
            this.treeFolder.BeforeDragNode += new DevExpress.XtraTreeList.BeforeDragNodeEventHandler(this.treeFolder_BeforeDragNode);
            this.treeFolder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeFolder_MouseClick);
            this.treeFolder.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeFolder_GetStateImage);
            this.treeFolder.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.treeFolder_ShowingEditor);
            this.treeFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeFolder_DragDrop);
            this.treeFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeFolder_DragEnter);
            // 
            // colName
            // 
            this.colName.Caption = "Name";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 92;
            // 
            // colFolderID
            // 
            this.colFolderID.Caption = "FolderID";
            this.colFolderID.FieldName = "ID";
            this.colFolderID.Name = "colFolderID";
            // 
            // bsSend
            // 
            this.bsSend.DataSource = typeof(ICP.Message.ServiceInterface.MessageFolderList);
            // 
            // pnlReceiveFax
            // 
            this.pnlReceiveFax.BackColor = System.Drawing.Color.Transparent;
            this.pnlReceiveFax.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.pnlReceiveFax.CaptionFont = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.pnlReceiveFax.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlReceiveFax.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlReceiveFax.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlReceiveFax.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(177)))), ((int)(((byte)(250)))));
            this.pnlReceiveFax.ColorCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(145)))));
            this.pnlReceiveFax.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(127)))), ((int)(((byte)(208)))));
            this.pnlReceiveFax.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.pnlReceiveFax.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.pnlReceiveFax.Controls.Add(this.treeReceive);
            this.pnlReceiveFax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReceiveFax.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlReceiveFax.Image = null;
            this.pnlReceiveFax.InnerBorderColor = System.Drawing.Color.White;
            this.pnlReceiveFax.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnlReceiveFax.Location = new System.Drawing.Point(0, 0);
            this.pnlReceiveFax.Name = "pnlReceiveFax";
            this.pnlReceiveFax.PanelStyle = BSE.Windows.Forms.PanelStyle.Default;
            this.pnlReceiveFax.ShowExpandIcon = true;
            this.pnlReceiveFax.ShowXPanderPanelProfessionalStyle = true;
            this.pnlReceiveFax.Size = new System.Drawing.Size(183, 434);
            this.pnlReceiveFax.TabIndex = 0;
            this.pnlReceiveFax.Text = "传真大厅";
            this.pnlReceiveFax.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlReceive_MouseClick);
            // 
            // treeReceive
            // 
            this.treeReceive.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeReceive.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeReceive.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeReceive.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeReceive.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCompanyName,
            this.colCompanyID});
            this.treeReceive.DataSource = this.bsReceive;
            this.treeReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeReceive.Location = new System.Drawing.Point(1, 26);
            this.treeReceive.Name = "treeReceive";
            this.treeReceive.OptionsBehavior.Editable = false;
            this.treeReceive.OptionsView.ShowColumns = false;
            this.treeReceive.OptionsView.ShowIndicator = false;
            this.treeReceive.Size = new System.Drawing.Size(181, 407);
            this.treeReceive.TabIndex = 0;
            this.treeReceive.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeReceive_FocusedNodeChanged);
            this.treeReceive.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeReceive_MouseClick);
            this.treeReceive.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeReceive_GetStateImage);
           
            //
            //bsReceive
            //
            this.bsReceive.DataSource = typeof(ICP.Message.ServiceInterface.ConfigureObjects);
            // 
            // colCompanyName
            // 
            this.colCompanyName.FieldName = "CompanyName";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.Visible = true;
            this.colCompanyName.VisibleIndex = 0;
            // 
            // colCompanyID
            // 
            this.colCompanyID.Caption = "CompanyID";
            this.colCompanyID.FieldName = "CompanyID";
            this.colCompanyID.Name = "colCompanyID";
            // 
            // imageListOrgainization
            // 
            this.imageListOrgainization.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOrgainization.ImageStream")));
            this.imageListOrgainization.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListOrgainization.Images.SetKeyName(0, "Data_16.png");
            this.imageListOrgainization.Images.SetKeyName(1, "User_16.png");
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "1_0041.ico");
            this.imageListTree.Images.SetKeyName(1, "1_0021.ico");
            this.imageListTree.Images.SetKeyName(2, "1_0066.ico");
            this.imageListTree.Images.SetKeyName(3, "1_0226.ico");
            this.imageListTree.Images.SetKeyName(4, "1_0110.ico");
            this.imageListTree.Images.SetKeyName(5, "1_0031.ico");
            this.imageListTree.Images.SetKeyName(6, "1_0003.ico");
            // 
            // splitMainList
            // 
            this.splitMainList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMainList.Location = new System.Drawing.Point(189, 42);
            this.splitMainList.Name = "splitMainList";
            this.splitMainList.Panel1.Controls.Add(this.panelControl2);
            this.splitMainList.Panel1.Text = "Panel1";
            this.splitMainList.Panel2.Text = "Panel2";
            this.splitMainList.Size = new System.Drawing.Size(845, 562);
            this.splitMainList.SplitterPosition = 465;
            this.splitMainList.TabIndex = 2;
            this.splitMainList.Text = "splitContainerControl1";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gcMain);
            this.panelControl2.Controls.Add(this.pnlSearchPart);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(465, 562);
            this.panelControl2.TabIndex = 5;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsMailList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gcMain.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcMain.Location = new System.Drawing.Point(2, 218);
            this.gcMain.MainView = this.gvMailList;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbPriority,
            this.rcmbState,
            this.rcmbAttachment,
            this.rcmbFlag,
            this.rtxtTime});
            this.gcMain.Size = new System.Drawing.Size(461, 342);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMailList});
            this.gcMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gcMain_MouseClick);
            this.gcMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gcMain_MouseMove);
            this.gcMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcMain_MouseDown);
            // 
            // gvMailList
            // 
            this.gvMailList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colState,
            this.colMailFrom,
            this.colSubject,
            this.colCreateDate_Time,
            this.colMailTo,
            this.colMailCC,
            this.colPriority});
            this.gvMailList.GridControl = this.gcMain;
            this.gvMailList.Images = this.imageListOther;
            this.gvMailList.Name = "gvMailList";
            this.gvMailList.OptionsBehavior.Editable = false;
            this.gvMailList.OptionsCustomization.AllowGroup = false;
            this.gvMailList.OptionsDetail.AllowZoomDetail = false;
            this.gvMailList.OptionsDetail.EnableMasterViewMode = false;
            this.gvMailList.OptionsDetail.ShowDetailTabs = false;
            this.gvMailList.OptionsDetail.SmartDetailExpand = false;
            this.gvMailList.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled;
            this.gvMailList.OptionsNavigation.UseOfficePageNavigation = false;
            this.gvMailList.OptionsSelection.MultiSelect = true;
            this.gvMailList.OptionsView.ColumnAutoWidth = false;
            this.gvMailList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMailList.OptionsView.ShowDetailButtons = false;
            this.gvMailList.OptionsView.ShowGroupPanel = false;
            this.gvMailList.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCreateDate_Time, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvMailList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvMailList_RowCellStyle);
            this.gvMailList.DoubleClick += new System.EventHandler(this.gvMailList_DoubleClick);
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.ColumnEdit = this.rcmbState;
            this.colState.FieldName = "State";
            this.colState.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colState.MaxWidth = 80;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            this.colState.Width = 45;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rcmbState.Name = "rcmbState";
            this.rcmbState.SmallImages = this.imageListState;
            // 
            // colMailFrom
            // 
            this.colMailFrom.Caption = "From";
            this.colMailFrom.FieldName = "SendFrom";
            this.colMailFrom.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colMailFrom.MaxWidth = 100;
            this.colMailFrom.MinWidth = 10;
            this.colMailFrom.Name = "colMailFrom";
            this.colMailFrom.OptionsColumn.AllowEdit = false;
            this.colMailFrom.Visible = true;
            this.colMailFrom.VisibleIndex = 1;
            this.colMailFrom.Width = 100;
            // 
            // colSubject
            // 
            this.colSubject.FieldName = "Subject";
            this.colSubject.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colSubject.Name = "colSubject";
            this.colSubject.OptionsColumn.AllowEdit = false;
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 2;
            this.colSubject.Width = 130;
            // 
            // colCreateDate_Time
            // 
            this.colCreateDate_Time.Caption = "CreateDate";
            this.colCreateDate_Time.FieldName = "CreateDate";
            this.colCreateDate_Time.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colCreateDate_Time.Name = "colCreateDate_Time";
            this.colCreateDate_Time.OptionsColumn.AllowEdit = false;
            this.colCreateDate_Time.Visible = true;
            this.colCreateDate_Time.VisibleIndex = 3;
            this.colCreateDate_Time.Width = 80;
            // 
            // colMailTo
            // 
            this.colMailTo.Caption = "MailTo";
            this.colMailTo.FieldName = "SendTo";
            this.colMailTo.Name = "colMailTo";
            this.colMailTo.OptionsColumn.AllowEdit = false;
            this.colMailTo.Visible = true;
            this.colMailTo.VisibleIndex = 4;
            this.colMailTo.Width = 56;
            // 
            // colMailCC
            // 
            this.colMailCC.Caption = "CC";
            this.colMailCC.FieldName = "CC";
            this.colMailCC.Name = "colMailCC";
            this.colMailCC.OptionsColumn.AllowEdit = false;
            this.colMailCC.Visible = true;
            this.colMailCC.VisibleIndex = 5;
            this.colMailCC.Width = 56;
            // 
            // colPriority
            // 
            this.colPriority.Caption = "Priority";
            this.colPriority.ColumnEdit = this.rcmbPriority;
            this.colPriority.FieldName = "Priority";
            this.colPriority.Name = "colPriority";
            this.colPriority.OptionsColumn.AllowEdit = false;
            this.colPriority.Visible = true;
            this.colPriority.VisibleIndex = 6;
            this.colPriority.Width = 45;
            // 
            // rcmbPriority
            // 
            this.rcmbPriority.AutoHeight = false;
            this.rcmbPriority.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbPriority.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rcmbPriority.Name = "rcmbPriority";
            this.rcmbPriority.SmallImages = this.imageListPriority;
            // 
            // rcmbAttachment
            // 
            this.rcmbAttachment.AutoHeight = false;
            this.rcmbAttachment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbAttachment.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rcmbAttachment.Name = "rcmbAttachment";
            this.rcmbAttachment.SmallImages = this.imageListOther;
            // 
            // rcmbFlag
            // 
            this.rcmbFlag.AutoHeight = false;
            this.rcmbFlag.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbFlag.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rcmbFlag.Name = "rcmbFlag";
            this.rcmbFlag.SmallImages = this.imageListFlag;
            // 
            // rtxtTime
            // 
            this.rtxtTime.AutoHeight = false;
            this.rtxtTime.Name = "rtxtTime";
            // 
            // pnlSearchPart
            // 
            this.pnlSearchPart.Controls.Add(this.pnlFastSearch);
            this.pnlSearchPart.Controls.Add(this.ucFaxQuery);
            this.pnlSearchPart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchPart.Location = new System.Drawing.Point(2, 2);
            this.pnlSearchPart.Name = "pnlSearchPart";
            this.pnlSearchPart.Size = new System.Drawing.Size(461, 216);
            this.pnlSearchPart.TabIndex = 7;
            // 
            // pnlFastSearch
            // 
            this.pnlFastSearch.Controls.Add(this.btnFastSearch);
            this.pnlFastSearch.Controls.Add(this.txtKeyWord);
            this.pnlFastSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFastSearch.Location = new System.Drawing.Point(2, 184);
            this.pnlFastSearch.Name = "pnlFastSearch";
            this.pnlFastSearch.Size = new System.Drawing.Size(457, 32);
            this.pnlFastSearch.TabIndex = 6;
            // 
            // btnFastSearch
            // 
            this.btnFastSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFastSearch.Image = global::ICP.OA.UI.Properties.Resources.fastSearch;
            this.btnFastSearch.Location = new System.Drawing.Point(419, 1);
            this.btnFastSearch.Name = "btnFastSearch";
            this.btnFastSearch.Size = new System.Drawing.Size(34, 28);
            this.btnFastSearch.TabIndex = 1;
            this.btnFastSearch.Click += new System.EventHandler(this.btnFastSearch_Click);
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyWord.EditValue = "";
            this.txtKeyWord.Location = new System.Drawing.Point(6, 5);
            this.txtKeyWord.MenuManager = this.barManager1;
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Properties.NullValuePrompt = "-- Title,Sender,Recipient --";
            this.txtKeyWord.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtKeyWord.Properties.ValidateOnEnterKey = true;
            this.txtKeyWord.Size = new System.Drawing.Size(410, 21);
            this.txtKeyWord.TabIndex = 0;
            this.txtKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyWord_KeyDown);
            // 
            // ucFaxQuery
            // 
            this.ucFaxQuery.AutoSize = true;
            this.ucFaxQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucFaxQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFaxQuery.Expand = true;
            this.ucFaxQuery.Location = new System.Drawing.Point(2, 2);
            this.ucFaxQuery.MinimumSize = new System.Drawing.Size(299, 2);
            this.ucFaxQuery.Name = "ucFaxQuery";
            this.ucFaxQuery.Size = new System.Drawing.Size(457, 182);
            this.ucFaxQuery.TabIndex = 4;
            // 
            // popupMenuAttachment
            // 
            this.popupMenuAttachment.Manager = this.barManager1;
            this.popupMenuAttachment.Name = "popupMenuAttachment";
            // 
            // popupMenuFolder
            // 
            this.popupMenuFolder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReNameFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDeleteFolder, true)});
            this.popupMenuFolder.Manager = this.barManager1;
            this.popupMenuFolder.Name = "popupMenuFolder";
            // 
            // popupMenuMessageList
            // 
            this.popupMenuMessageList.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barNewEmail),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRevert),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRevertAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTSend),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSAndR, true),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.barReturn, "Return", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete)});
            this.popupMenuMessageList.Manager = this.barManager1;
            this.popupMenuMessageList.Name = "popupMenuMessageList";
            // 
            // UCFaxList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMainList);
            this.Controls.Add(this.dpFolder);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Name = "UCFaxList";
            this.Size = new System.Drawing.Size(1034, 604);
            ((System.ComponentModel.ISupportInitialize)(this.bsMailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpFolder.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.pnlSendFax.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSend)).EndInit();
            this.pnlReceiveFax.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeReceive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReceive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainList)).EndInit();
            this.splitMainList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearchPart)).EndInit();
            this.pnlSearchPart.ResumeLayout(false);
            this.pnlSearchPart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFastSearch)).EndInit();
            this.pnlFastSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuMessageList)).EndInit();
            this.ResumeLayout(false);

        }





        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barNewEmail;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.SplitContainerControl splitMainList;
        private DevExpress.XtraBars.Docking.DockPanel dpFolder;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.BindingSource bsSend;
        private System.Windows.Forms.BindingSource bsMailList;
        private DevExpress.XtraTreeList.TreeList treeReceive;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCompanyName;

        private DevExpress.XtraBars.BarButtonItem barAddFolder;
        private DevExpress.XtraBars.BarButtonItem barReNameFolder;
        private DevExpress.XtraBars.BarButtonItem barDeleteFolder;
        private DevExpress.XtraBars.PopupMenu popupMenuFolder;
        private DevExpress.XtraBars.BarButtonItem barRevert;
        private DevExpress.XtraBars.BarButtonItem barRevertAll;
        private DevExpress.XtraBars.BarButtonItem barTSend;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barSAndR;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.ImageList imageListPriority;
        private System.Windows.Forms.ImageList imageListFlag;
        private System.Windows.Forms.ImageList imageListOther;
        private System.Windows.Forms.ImageList imageListState;
        private DevExpress.XtraBars.PopupMenu popupMenuAttachment;
        private DevExpress.XtraBars.PopupMenu popupMenuMessageList;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMailList;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbPriority;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbFlag;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbAttachment;
        private DevExpress.XtraGrid.Columns.GridColumn colMailFrom;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtTime;
        private UCFaxQuery ucFaxQuery;
        private BSE.Windows.Forms.Panel pnlSendFax;
        private DevExpress.XtraTreeList.TreeList treeFolder;
        private BSE.Windows.Forms.Panel pnlReceiveFax;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.BarButtonItem barReturn;
        private System.Windows.Forms.BindingSource bsReceive;        
        private System.Windows.Forms.ImageList imageListOrgainization;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate_Time;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colMailTo;
        private DevExpress.XtraGrid.Columns.GridColumn colMailCC;
        private DevExpress.XtraGrid.Columns.GridColumn colPriority;
        private DevExpress.XtraBars.BarButtonItem barAccepted;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCompanyID;
        private DevExpress.XtraEditors.PanelControl pnlFastSearch;
        private DevExpress.XtraEditors.SimpleButton btnFastSearch;
        private DevExpress.XtraEditors.TextEdit txtKeyWord;
        private DevExpress.XtraEditors.PanelControl pnlSearchPart;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFolderID;
        // private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
    }
}

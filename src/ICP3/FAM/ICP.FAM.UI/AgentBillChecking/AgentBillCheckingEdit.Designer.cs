namespace ICP.FAM.UI
{
    partial class AgentBillCheckingEdit
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentBillCheckingEdit));
            this.gcBase = new DevExpress.XtraEditors.GroupControl();
            this.txtStatus = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barStartCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barChecking = new DevExpress.XtraBars.BarButtonItem();
            this.barNotifiedBillOwner = new DevExpress.XtraBars.BarButtonItem();
            this.barCompleted = new DevExpress.XtraBars.BarButtonItem();
            this.barWriteOff = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.cmbOperType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.bsAgentCheckBill = new System.Windows.Forms.BindingSource(this.components);
            this.mscCheckUser = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.mscLaunchUser = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.cmbCheckCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbLaunchCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.ckbIgnoreCheckBill = new DevExpress.XtraEditors.CheckEdit();
            this.txtCheckBillNo = new DevExpress.XtraEditors.TextEdit();
            this.labMessage = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.labCheckBillNo = new DevExpress.XtraEditors.LabelControl();
            this.dteEngingETD = new DevExpress.XtraEditors.DateEdit();
            this.labEndingETD = new DevExpress.XtraEditors.LabelControl();
            this.labOperType = new DevExpress.XtraEditors.LabelControl();
            this.labCheckUser = new DevExpress.XtraEditors.LabelControl();
            this.labCheckCompany = new DevExpress.XtraEditors.LabelControl();
            this.labbLaunchUser = new DevExpress.XtraEditors.LabelControl();
            this.labLaunchCompany = new DevExpress.XtraEditors.LabelControl();
            this.bcDetail = new DevExpress.XtraEditors.GroupControl();
            this.CheckList_gridControl = new DevExpress.XtraGrid.GridControl();
            this.bsDetailList = new System.Windows.Forms.BindingSource(this.components);
            this.AdvancedCheckList_gridView = new ICP.Framework.ClientComponents.Controls.LWBandedGridView();
            this.gbBase = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colETD = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBLNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbAgent1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBillNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgent1Debit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgent1Credit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgent1Balance = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbGap = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGap = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbAgent2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colAgent2Balance = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgent2Debit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgent2Credit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgent2BillNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.SimpleCheckList_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.etd_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.blno_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.currency_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.debit1_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.credit1_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.balance1_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gap_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.balance2_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.credit2_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.debit2_gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.Maximize_checkButton = new DevExpress.XtraEditors.CheckButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.ckbMuit = new DevExpress.XtraEditors.CheckEdit();
            this.ckbGap = new DevExpress.XtraEditors.CheckEdit();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.toolTipOperType = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcBase)).BeginInit();
            this.gcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAgentCheckBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLaunchCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIgnoreCheckBill.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckBillNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEngingETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEngingETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bcDetail)).BeginInit();
            this.bcDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CheckList_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdvancedCheckList_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimpleCheckList_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckbMuit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbGap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcBase
            // 
            this.gcBase.Controls.Add(this.txtStatus);
            this.gcBase.Controls.Add(this.cmbOperType);
            this.gcBase.Controls.Add(this.mscCheckUser);
            this.gcBase.Controls.Add(this.mscLaunchUser);
            this.gcBase.Controls.Add(this.cmbCheckCompany);
            this.gcBase.Controls.Add(this.cmbLaunchCompany);
            this.gcBase.Controls.Add(this.ckbIgnoreCheckBill);
            this.gcBase.Controls.Add(this.txtCheckBillNo);
            this.gcBase.Controls.Add(this.labMessage);
            this.gcBase.Controls.Add(this.labStatus);
            this.gcBase.Controls.Add(this.labCheckBillNo);
            this.gcBase.Controls.Add(this.dteEngingETD);
            this.gcBase.Controls.Add(this.labEndingETD);
            this.gcBase.Controls.Add(this.labOperType);
            this.gcBase.Controls.Add(this.labCheckUser);
            this.gcBase.Controls.Add(this.labCheckCompany);
            this.gcBase.Controls.Add(this.labbLaunchUser);
            this.gcBase.Controls.Add(this.labLaunchCompany);
            this.gcBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcBase.Location = new System.Drawing.Point(0, 26);
            this.gcBase.Name = "gcBase";
            this.gcBase.Size = new System.Drawing.Size(1000, 79);
            this.gcBase.TabIndex = 1;
            this.gcBase.Text = "对帐信息";
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtStatus.Location = new System.Drawing.Point(739, 27);
            this.txtStatus.MenuManager = this.barManager1;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(278, 21);
            this.txtStatus.TabIndex = 8;
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
            this.barSave,
            this.barStartCheck,
            this.barChecking,
            this.barNotifiedBillOwner,
            this.barCompleted,
            this.barPrint,
            this.barWriteOff,
            this.barClose,
            this.barRefresh});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 9;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barStartCheck, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barChecking, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNotifiedBillOwner, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCompleted, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barWriteOff, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barStartCheck
            // 
            this.barStartCheck.Caption = "发起对账(&L)";
            this.barStartCheck.Glyph = global::ICP.FAM.UI.Properties.Resources.DollarSign;
            this.barStartCheck.Id = 1;
            this.barStartCheck.Name = "barStartCheck";
            this.barStartCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barStartCheck_ItemClick);
            // 
            // barChecking
            // 
            this.barChecking.Caption = "核对账单(&B)";
            this.barChecking.Glyph = global::ICP.FAM.UI.Properties.Resources.WriteOffPrint;
            this.barChecking.Id = 2;
            this.barChecking.Name = "barChecking";
            this.barChecking.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barChecking_ItemClick);
            // 
            // barNotifiedBillOwner
            // 
            this.barNotifiedBillOwner.Caption = "通知修改账单(&N)";
            this.barNotifiedBillOwner.Glyph = global::ICP.FAM.UI.Properties.Resources.MultiCheck;
            this.barNotifiedBillOwner.Id = 3;
            this.barNotifiedBillOwner.Name = "barNotifiedBillOwner";
            this.barNotifiedBillOwner.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNotifiedBillOwner_ItemClick);
            // 
            // barCompleted
            // 
            this.barCompleted.Caption = "完成对账(&F)";
            this.barCompleted.Glyph = global::ICP.FAM.UI.Properties.Resources.Check_16;
            this.barCompleted.Id = 4;
            this.barCompleted.Name = "barCompleted";
            this.barCompleted.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCompleted_ItemClick);
            // 
            // barWriteOff
            // 
            this.barWriteOff.Caption = "销账(&W)";
            this.barWriteOff.Glyph = global::ICP.FAM.UI.Properties.Resources.Telex;
            this.barWriteOff.Id = 6;
            this.barWriteOff.Name = "barWriteOff";
            this.barWriteOff.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barWriteOff_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "刷新(&R)";
            this.barRefresh.Glyph = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.barRefresh.Id = 8;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印(&P)";
            this.barPrint.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 5;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 7;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1000, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 514);
            this.barDockControlBottom.Size = new System.Drawing.Size(1000, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 488);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1000, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 488);
            // 
            // cmbOperType
            // 
            this.cmbOperType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOperType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsAgentCheckBill, "OperValues", true));
            this.cmbOperType.EditValue = "";
            this.cmbOperType.Location = new System.Drawing.Point(399, 27);
            this.cmbOperType.Name = "cmbOperType";
            this.cmbOperType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbOperType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbOperType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOperType.Properties.SelectAllItemCaption = "全选";
            this.cmbOperType.Size = new System.Drawing.Size(114, 21);
            this.cmbOperType.TabIndex = 4;
            this.cmbOperType.TabStop = false;
            // 
            // bsAgentCheckBill
            // 
            this.bsAgentCheckBill.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.AgnetBillCheckList);
            // 
            // mscCheckUser
            // 
            this.mscCheckUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mscCheckUser.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsAgentCheckBill, "CheckUserID", true));
            this.mscCheckUser.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsAgentCheckBill, "CheckUserName", true));
            this.mscCheckUser.EditText = "";
            this.mscCheckUser.EditValue = null;
            this.mscCheckUser.Location = new System.Drawing.Point(241, 54);
            this.mscCheckUser.Name = "mscCheckUser";
            this.mscCheckUser.ReadOnly = false;
            this.mscCheckUser.RefreshButtonToolTip = "";
            this.mscCheckUser.ShowRefreshButton = false;
            this.mscCheckUser.Size = new System.Drawing.Size(83, 21);
            this.mscCheckUser.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.mscCheckUser.TabIndex = 3;
            this.mscCheckUser.ToolTip = "";
            // 
            // mscLaunchUser
            // 
            this.mscLaunchUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mscLaunchUser.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsAgentCheckBill, "LaunchUserID", true));
            this.mscLaunchUser.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsAgentCheckBill, "LaunchUserName", true));
            this.mscLaunchUser.EditText = "";
            this.mscLaunchUser.EditValue = null;
            this.mscLaunchUser.Location = new System.Drawing.Point(241, 27);
            this.mscLaunchUser.Name = "mscLaunchUser";
            this.mscLaunchUser.ReadOnly = false;
            this.mscLaunchUser.RefreshButtonToolTip = "";
            this.mscLaunchUser.ShowRefreshButton = false;
            this.mscLaunchUser.Size = new System.Drawing.Size(83, 21);
            this.mscLaunchUser.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.mscLaunchUser.TabIndex = 1;
            this.mscLaunchUser.ToolTip = "";
            // 
            // cmbCheckCompany
            // 
            this.cmbCheckCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCheckCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsAgentCheckBill, "CheckCompanyID", true));
            this.cmbCheckCompany.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAgentCheckBill, "CheckCompanyName", true));
            this.cmbCheckCompany.Location = new System.Drawing.Point(85, 54);
            this.cmbCheckCompany.Name = "cmbCheckCompany";
            this.cmbCheckCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCheckCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCheckCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCheckCompany.Size = new System.Drawing.Size(89, 21);
            this.cmbCheckCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbCheckCompany.TabIndex = 2;
            // 
            // cmbLaunchCompany
            // 
            this.cmbLaunchCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbLaunchCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsAgentCheckBill, "LaunchCompanyID", true));
            this.cmbLaunchCompany.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAgentCheckBill, "LaunchCompanyName", true));
            this.cmbLaunchCompany.Location = new System.Drawing.Point(85, 27);
            this.cmbLaunchCompany.MenuManager = this.barManager1;
            this.cmbLaunchCompany.Name = "cmbLaunchCompany";
            this.cmbLaunchCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbLaunchCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbLaunchCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLaunchCompany.Size = new System.Drawing.Size(89, 21);
            this.cmbLaunchCompany.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbLaunchCompany.TabIndex = 0;
            // 
            // ckbIgnoreCheckBill
            // 
            this.ckbIgnoreCheckBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbIgnoreCheckBill.EditValue = true;
            this.ckbIgnoreCheckBill.Enabled = false;
            this.ckbIgnoreCheckBill.Location = new System.Drawing.Point(397, 55);
            this.ckbIgnoreCheckBill.Name = "ckbIgnoreCheckBill";
            this.ckbIgnoreCheckBill.Properties.Caption = "忽略已对帐的帐单";
            this.ckbIgnoreCheckBill.Size = new System.Drawing.Size(116, 19);
            this.ckbIgnoreCheckBill.TabIndex = 5;
            // 
            // txtCheckBillNo
            // 
            this.txtCheckBillNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCheckBillNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAgentCheckBill, "No", true));
            this.txtCheckBillNo.EditValue = "";
            this.txtCheckBillNo.Location = new System.Drawing.Point(598, 27);
            this.txtCheckBillNo.Name = "txtCheckBillNo";
            this.txtCheckBillNo.Properties.ReadOnly = true;
            this.txtCheckBillNo.Size = new System.Drawing.Size(102, 21);
            this.txtCheckBillNo.TabIndex = 6;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labMessage.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labMessage.Appearance.Options.UseForeColor = true;
            this.labMessage.Location = new System.Drawing.Point(706, 57);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(312, 14);
            this.labMessage.TabIndex = 9;
            this.labMessage.Text = "提示：核对帐单时，以代理帐单的提单号为核对基准条件。";
            // 
            // labStatus
            // 
            this.labStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labStatus.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labStatus.Appearance.Options.UseForeColor = true;
            this.labStatus.Location = new System.Drawing.Point(708, 30);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(28, 14);
            this.labStatus.TabIndex = 82;
            this.labStatus.Text = "状态:";
            // 
            // labCheckBillNo
            // 
            this.labCheckBillNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labCheckBillNo.Location = new System.Drawing.Point(517, 29);
            this.labCheckBillNo.Name = "labCheckBillNo";
            this.labCheckBillNo.Size = new System.Drawing.Size(48, 14);
            this.labCheckBillNo.TabIndex = 82;
            this.labCheckBillNo.Text = "对帐单号";
            // 
            // dteEngingETD
            // 
            this.dteEngingETD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dteEngingETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsAgentCheckBill, "EndingETD", true));
            this.dteEngingETD.EditValue = null;
            this.dteEngingETD.Location = new System.Drawing.Point(599, 54);
            this.dteEngingETD.Name = "dteEngingETD";
            this.dteEngingETD.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.dteEngingETD.Properties.Appearance.Options.UseBackColor = true;
            this.dteEngingETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEngingETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEngingETD.Size = new System.Drawing.Size(101, 21);
            this.dteEngingETD.TabIndex = 7;
            // 
            // labEndingETD
            // 
            this.labEndingETD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labEndingETD.Location = new System.Drawing.Point(517, 56);
            this.labEndingETD.Name = "labEndingETD";
            this.labEndingETD.Size = new System.Drawing.Size(63, 14);
            this.labEndingETD.TabIndex = 80;
            this.labEndingETD.Text = "ETD 截止于";
            // 
            // labOperType
            // 
            this.labOperType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labOperType.Location = new System.Drawing.Point(330, 30);
            this.labOperType.Name = "labOperType";
            this.labOperType.Size = new System.Drawing.Size(48, 14);
            this.labOperType.TabIndex = 5;
            this.labOperType.Text = "业务类型";
            // 
            // labCheckUser
            // 
            this.labCheckUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labCheckUser.Location = new System.Drawing.Point(181, 57);
            this.labCheckUser.Name = "labCheckUser";
            this.labCheckUser.Size = new System.Drawing.Size(36, 14);
            this.labCheckUser.TabIndex = 1;
            this.labCheckUser.Text = "核对人";
            // 
            // labCheckCompany
            // 
            this.labCheckCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labCheckCompany.Location = new System.Drawing.Point(9, 57);
            this.labCheckCompany.Name = "labCheckCompany";
            this.labCheckCompany.Size = new System.Drawing.Size(48, 14);
            this.labCheckCompany.TabIndex = 1;
            this.labCheckCompany.Text = "核对公司";
            // 
            // labbLaunchUser
            // 
            this.labbLaunchUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labbLaunchUser.Location = new System.Drawing.Point(181, 30);
            this.labbLaunchUser.Name = "labbLaunchUser";
            this.labbLaunchUser.Size = new System.Drawing.Size(36, 14);
            this.labbLaunchUser.TabIndex = 1;
            this.labbLaunchUser.Text = "发起人";
            // 
            // labLaunchCompany
            // 
            this.labLaunchCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labLaunchCompany.Location = new System.Drawing.Point(9, 30);
            this.labLaunchCompany.Name = "labLaunchCompany";
            this.labLaunchCompany.Size = new System.Drawing.Size(48, 14);
            this.labLaunchCompany.TabIndex = 1;
            this.labLaunchCompany.Text = "发起公司";
            // 
            // bcDetail
            // 
            this.bcDetail.Controls.Add(this.CheckList_gridControl);
            this.bcDetail.Controls.Add(this.panelControl4);
            this.bcDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bcDetail.Location = new System.Drawing.Point(0, 105);
            this.bcDetail.Name = "bcDetail";
            this.bcDetail.Size = new System.Drawing.Size(1000, 409);
            this.bcDetail.TabIndex = 2;
            this.bcDetail.Text = "核对明细";
            // 
            // CheckList_gridControl
            // 
            this.CheckList_gridControl.DataSource = this.bsDetailList;
            this.CheckList_gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.CheckList_gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.CheckList_gridControl.Location = new System.Drawing.Point(2, 54);
            this.CheckList_gridControl.MainView = this.AdvancedCheckList_gridView;
            this.CheckList_gridControl.Name = "CheckList_gridControl";
            this.CheckList_gridControl.Size = new System.Drawing.Size(996, 353);
            this.CheckList_gridControl.TabIndex = 27;
            this.CheckList_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.AdvancedCheckList_gridView,
            this.SimpleCheckList_gridView});
            // 
            // bsDetailList
            // 
            this.bsDetailList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.AgentBillCheckDetail);
            // 
            // AdvancedCheckList_gridView
            // 
            this.AdvancedCheckList_gridView.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AdvancedCheckList_gridView.Appearance.FooterPanel.Options.UseBackColor = true;
            this.AdvancedCheckList_gridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gbBase,
            this.gbAgent1,
            this.gbGap,
            this.gbAgent2});
            this.AdvancedCheckList_gridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colETD,
            this.colBLNo,
            this.colCurrency,
            this.colBillNo,
            this.colAgent1Debit,
            this.colAgent1Credit,
            this.colAgent1Balance,
            this.colGap,
            this.colAgent2Balance,
            this.colAgent2Debit,
            this.colAgent2Credit,
            this.colAgent2BillNo});
            this.AdvancedCheckList_gridView.GridControl = this.CheckList_gridControl;
            this.AdvancedCheckList_gridView.Name = "AdvancedCheckList_gridView";
            this.AdvancedCheckList_gridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.AdvancedCheckList_gridView.OptionsBehavior.Editable = false;
            this.AdvancedCheckList_gridView.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.AdvancedCheckList_gridView.OptionsSelection.MultiSelect = true;
            this.AdvancedCheckList_gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.AdvancedCheckList_gridView.OptionsView.ColumnAutoWidth = false;
            this.AdvancedCheckList_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.AdvancedCheckList_gridView.OptionsView.ShowFooter = true;
            this.AdvancedCheckList_gridView.OptionsView.ShowGroupPanel = false;
            // 
            // gbBase
            // 
            this.gbBase.AppearanceHeader.Options.UseTextOptions = true;
            this.gbBase.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbBase.Caption = "基础";
            this.gbBase.Columns.Add(this.colETD);
            this.gbBase.Columns.Add(this.colBLNo);
            this.gbBase.Columns.Add(this.colCurrency);
            this.gbBase.Name = "gbBase";
            this.gbBase.Width = 236;
            // 
            // colETD
            // 
            this.colETD.AppearanceHeader.Options.UseTextOptions = true;
            this.colETD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colETD.Caption = "ETD";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.Width = 72;
            // 
            // colBLNo
            // 
            this.colBLNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colBLNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBLNo.Caption = "提单号";
            this.colBLNo.FieldName = "BLNO";
            this.colBLNo.Name = "colBLNo";
            this.colBLNo.Visible = true;
            this.colBLNo.Width = 108;
            // 
            // colCurrency
            // 
            this.colCurrency.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrency.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrency.Caption = "币种";
            this.colCurrency.FieldName = "CurrencyName";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.Width = 56;
            // 
            // gbAgent1
            // 
            this.gbAgent1.AppearanceHeader.Options.UseTextOptions = true;
            this.gbAgent1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbAgent1.Caption = "发起代理";
            this.gbAgent1.Columns.Add(this.colBillNo);
            this.gbAgent1.Columns.Add(this.colAgent1Debit);
            this.gbAgent1.Columns.Add(this.colAgent1Credit);
            this.gbAgent1.Columns.Add(this.colAgent1Balance);
            this.gbAgent1.Name = "gbAgent1";
            this.gbAgent1.Width = 345;
            // 
            // colBillNo
            // 
            this.colBillNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colBillNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBillNo.Caption = "帐单号";
            this.colBillNo.FieldName = "LaunchBillNOs";
            this.colBillNo.Name = "colBillNo";
            this.colBillNo.SummaryItem.FieldName = "Agent1BillNOsCount";
            this.colBillNo.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colBillNo.Visible = true;
            this.colBillNo.Width = 121;
            // 
            // colAgent1Debit
            // 
            this.colAgent1Debit.AppearanceHeader.Options.UseTextOptions = true;
            this.colAgent1Debit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAgent1Debit.Caption = "应收";
            this.colAgent1Debit.DisplayFormat.FormatString = "n";
            this.colAgent1Debit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAgent1Debit.FieldName = "LaunchDebit";
            this.colAgent1Debit.Name = "colAgent1Debit";
            this.colAgent1Debit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colAgent1Debit.Visible = true;
            this.colAgent1Debit.Width = 71;
            // 
            // colAgent1Credit
            // 
            this.colAgent1Credit.AppearanceHeader.Options.UseTextOptions = true;
            this.colAgent1Credit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAgent1Credit.Caption = "应付";
            this.colAgent1Credit.DisplayFormat.FormatString = "n";
            this.colAgent1Credit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAgent1Credit.FieldName = "LaunchCredit";
            this.colAgent1Credit.Name = "colAgent1Credit";
            this.colAgent1Credit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colAgent1Credit.Visible = true;
            this.colAgent1Credit.Width = 77;
            // 
            // colAgent1Balance
            // 
            this.colAgent1Balance.AppearanceHeader.Options.UseTextOptions = true;
            this.colAgent1Balance.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAgent1Balance.Caption = "余额";
            this.colAgent1Balance.DisplayFormat.FormatString = "n";
            this.colAgent1Balance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAgent1Balance.FieldName = "LaunchBalance";
            this.colAgent1Balance.Name = "colAgent1Balance";
            this.colAgent1Balance.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colAgent1Balance.Visible = true;
            this.colAgent1Balance.Width = 76;
            // 
            // gbGap
            // 
            this.gbGap.Columns.Add(this.colGap);
            this.gbGap.Name = "gbGap";
            this.gbGap.Width = 82;
            // 
            // colGap
            // 
            this.colGap.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colGap.AppearanceCell.Options.UseFont = true;
            this.colGap.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colGap.AppearanceHeader.Options.UseFont = true;
            this.colGap.AppearanceHeader.Options.UseTextOptions = true;
            this.colGap.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGap.Caption = "Gap";
            this.colGap.DisplayFormat.FormatString = "n";
            this.colGap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGap.FieldName = "Gap";
            this.colGap.Name = "colGap";
            this.colGap.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colGap.Visible = true;
            this.colGap.Width = 82;
            // 
            // gbAgent2
            // 
            this.gbAgent2.AppearanceHeader.Options.UseTextOptions = true;
            this.gbAgent2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbAgent2.Caption = "核对代理";
            this.gbAgent2.Columns.Add(this.colAgent2Balance);
            this.gbAgent2.Columns.Add(this.colAgent2Debit);
            this.gbAgent2.Columns.Add(this.colAgent2Credit);
            this.gbAgent2.Columns.Add(this.colAgent2BillNo);
            this.gbAgent2.Name = "gbAgent2";
            this.gbAgent2.Width = 354;
            // 
            // colAgent2Balance
            // 
            this.colAgent2Balance.AppearanceHeader.Options.UseTextOptions = true;
            this.colAgent2Balance.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAgent2Balance.Caption = "余额";
            this.colAgent2Balance.DisplayFormat.FormatString = "n";
            this.colAgent2Balance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAgent2Balance.FieldName = "CheckBalance";
            this.colAgent2Balance.Name = "colAgent2Balance";
            this.colAgent2Balance.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colAgent2Balance.Visible = true;
            this.colAgent2Balance.Width = 87;
            // 
            // colAgent2Debit
            // 
            this.colAgent2Debit.AppearanceHeader.Options.UseTextOptions = true;
            this.colAgent2Debit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAgent2Debit.Caption = "应收";
            this.colAgent2Debit.DisplayFormat.FormatString = "n";
            this.colAgent2Debit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAgent2Debit.FieldName = "CheckDebit";
            this.colAgent2Debit.Name = "colAgent2Debit";
            this.colAgent2Debit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colAgent2Debit.SummaryItem.Tag = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.colAgent2Debit.Visible = true;
            this.colAgent2Debit.Width = 87;
            // 
            // colAgent2Credit
            // 
            this.colAgent2Credit.AppearanceHeader.Options.UseTextOptions = true;
            this.colAgent2Credit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAgent2Credit.Caption = "应付";
            this.colAgent2Credit.DisplayFormat.FormatString = "n";
            this.colAgent2Credit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAgent2Credit.FieldName = "CheckCredit";
            this.colAgent2Credit.Name = "colAgent2Credit";
            this.colAgent2Credit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colAgent2Credit.SummaryItem.Tag = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.colAgent2Credit.Visible = true;
            this.colAgent2Credit.Width = 86;
            // 
            // colAgent2BillNo
            // 
            this.colAgent2BillNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colAgent2BillNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAgent2BillNo.Caption = "帐单号";
            this.colAgent2BillNo.FieldName = "CheckBillNOs";
            this.colAgent2BillNo.Name = "colAgent2BillNo";
            this.colAgent2BillNo.Visible = true;
            this.colAgent2BillNo.Width = 94;
            // 
            // SimpleCheckList_gridView
            // 
            this.SimpleCheckList_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.etd_gridColumn,
            this.blno_gridColumn,
            this.currency_gridColumn,
            this.debit1_gridColumn,
            this.credit1_gridColumn,
            this.balance1_gridColumn,
            this.gap_gridColumn,
            this.balance2_gridColumn,
            this.credit2_gridColumn,
            this.debit2_gridColumn});
            this.SimpleCheckList_gridView.GridControl = this.CheckList_gridControl;
            this.SimpleCheckList_gridView.Name = "SimpleCheckList_gridView";
            this.SimpleCheckList_gridView.OptionsBehavior.Editable = false;
            this.SimpleCheckList_gridView.OptionsSelection.MultiSelect = true;
            this.SimpleCheckList_gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.SimpleCheckList_gridView.OptionsView.ColumnAutoWidth = false;
            this.SimpleCheckList_gridView.OptionsView.ShowFooter = true;
            this.SimpleCheckList_gridView.OptionsView.ShowGroupPanel = false;
            // 
            // etd_gridColumn
            // 
            this.etd_gridColumn.Caption = "ETD";
            this.etd_gridColumn.FieldName = "ETD";
            this.etd_gridColumn.Name = "etd_gridColumn";
            this.etd_gridColumn.Visible = true;
            this.etd_gridColumn.VisibleIndex = 0;
            // 
            // blno_gridColumn
            // 
            this.blno_gridColumn.Caption = "提单号";
            this.blno_gridColumn.FieldName = "BLNO";
            this.blno_gridColumn.Name = "blno_gridColumn";
            this.blno_gridColumn.Visible = true;
            this.blno_gridColumn.VisibleIndex = 1;
            this.blno_gridColumn.Width = 88;
            // 
            // currency_gridColumn
            // 
            this.currency_gridColumn.Caption = "币种";
            this.currency_gridColumn.FieldName = "CurrencyName";
            this.currency_gridColumn.Name = "currency_gridColumn";
            this.currency_gridColumn.Visible = true;
            this.currency_gridColumn.VisibleIndex = 2;
            this.currency_gridColumn.Width = 44;
            // 
            // debit1_gridColumn
            // 
            this.debit1_gridColumn.Caption = "应收";
            this.debit1_gridColumn.DisplayFormat.FormatString = "n";
            this.debit1_gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.debit1_gridColumn.FieldName = "LaunchCredit";
            this.debit1_gridColumn.Name = "debit1_gridColumn";
            this.debit1_gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.debit1_gridColumn.Visible = true;
            this.debit1_gridColumn.VisibleIndex = 3;
            // 
            // credit1_gridColumn
            // 
            this.credit1_gridColumn.Caption = "应付";
            this.credit1_gridColumn.DisplayFormat.FormatString = "n";
            this.credit1_gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.credit1_gridColumn.FieldName = "LaunchDebit";
            this.credit1_gridColumn.Name = "credit1_gridColumn";
            this.credit1_gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.credit1_gridColumn.Visible = true;
            this.credit1_gridColumn.VisibleIndex = 4;
            // 
            // balance1_gridColumn
            // 
            this.balance1_gridColumn.Caption = "余额";
            this.balance1_gridColumn.DisplayFormat.FormatString = "n";
            this.balance1_gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.balance1_gridColumn.FieldName = "LaunchBalance";
            this.balance1_gridColumn.Name = "balance1_gridColumn";
            this.balance1_gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.balance1_gridColumn.Visible = true;
            this.balance1_gridColumn.VisibleIndex = 5;
            // 
            // gap_gridColumn
            // 
            this.gap_gridColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gap_gridColumn.AppearanceCell.Options.UseFont = true;
            this.gap_gridColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gap_gridColumn.AppearanceHeader.Options.UseFont = true;
            this.gap_gridColumn.Caption = "Gap";
            this.gap_gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gap_gridColumn.FieldName = "Gap";
            this.gap_gridColumn.Name = "gap_gridColumn";
            this.gap_gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gap_gridColumn.Visible = true;
            this.gap_gridColumn.VisibleIndex = 6;
            // 
            // balance2_gridColumn
            // 
            this.balance2_gridColumn.Caption = "余额";
            this.balance2_gridColumn.DisplayFormat.FormatString = "n";
            this.balance2_gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.balance2_gridColumn.FieldName = "CheckBalance";
            this.balance2_gridColumn.Name = "balance2_gridColumn";
            this.balance2_gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.balance2_gridColumn.Visible = true;
            this.balance2_gridColumn.VisibleIndex = 7;
            // 
            // credit2_gridColumn
            // 
            this.credit2_gridColumn.Caption = "应付";
            this.credit2_gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.credit2_gridColumn.FieldName = "CheckDebit";
            this.credit2_gridColumn.Name = "credit2_gridColumn";
            this.credit2_gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.credit2_gridColumn.Visible = true;
            this.credit2_gridColumn.VisibleIndex = 8;
            // 
            // debit2_gridColumn
            // 
            this.debit2_gridColumn.Caption = "应收";
            this.debit2_gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.debit2_gridColumn.FieldName = "CheckCredit";
            this.debit2_gridColumn.Name = "debit2_gridColumn";
            this.debit2_gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.debit2_gridColumn.Visible = true;
            this.debit2_gridColumn.VisibleIndex = 9;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.Maximize_checkButton);
            this.panelControl4.Controls.Add(this.simpleButton6);
            this.panelControl4.Controls.Add(this.ckbMuit);
            this.panelControl4.Controls.Add(this.ckbGap);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(2, 23);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(996, 31);
            this.panelControl4.TabIndex = 28;
            // 
            // Maximize_checkButton
            // 
            this.Maximize_checkButton.Location = new System.Drawing.Point(7, 4);
            this.Maximize_checkButton.Name = "Maximize_checkButton";
            this.Maximize_checkButton.Size = new System.Drawing.Size(59, 23);
            this.Maximize_checkButton.TabIndex = 88;
            this.Maximize_checkButton.Text = "最大化";
            this.Maximize_checkButton.CheckedChanged += new System.EventHandler(this.Maximize_checkButton_CheckedChanged);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(512, 5);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(114, 23);
            this.simpleButton6.TabIndex = 87;
            this.simpleButton6.Text = "查看帐单信息";
            this.simpleButton6.Visible = false;
            // 
            // ckbMuit
            // 
            this.ckbMuit.Location = new System.Drawing.Point(362, 6);
            this.ckbMuit.Name = "ckbMuit";
            this.ckbMuit.Properties.Caption = "简洁栏位";
            this.ckbMuit.Size = new System.Drawing.Size(100, 19);
            this.ckbMuit.TabIndex = 86;
            this.ckbMuit.CheckedChanged += new System.EventHandler(this.AdvancedColumn_checkEdit_CheckedChanged);
            // 
            // ckbGap
            // 
            this.ckbGap.Location = new System.Drawing.Point(80, 6);
            this.ckbGap.Name = "ckbGap";
            this.ckbGap.Properties.Caption = "仅显示Gap不等于0的记录";
            this.ckbGap.Size = new System.Drawing.Size(175, 19);
            this.ckbGap.TabIndex = 86;
            this.ckbGap.CheckedChanged += new System.EventHandler(this.ckbGap_CheckedChanged);
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // AgentBillCheckingEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.bcDetail);
            this.Controls.Add(this.gcBase);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "AgentBillCheckingEdit";
            this.Size = new System.Drawing.Size(1000, 514);
            ((System.ComponentModel.ISupportInitialize)(this.gcBase)).EndInit();
            this.gcBase.ResumeLayout(false);
            this.gcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAgentCheckBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCheckCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLaunchCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIgnoreCheckBill.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckBillNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEngingETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEngingETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bcDetail)).EndInit();
            this.bcDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CheckList_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdvancedCheckList_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimpleCheckList_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckbMuit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbGap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcBase;
        private DevExpress.XtraEditors.LabelControl labLaunchCompany;
        private DevExpress.XtraEditors.LabelControl labOperType;
        private DevExpress.XtraEditors.CheckEdit ckbIgnoreCheckBill;
        private DevExpress.XtraEditors.TextEdit txtCheckBillNo;
        private DevExpress.XtraEditors.LabelControl labCheckBillNo;
        private DevExpress.XtraEditors.DateEdit dteEngingETD;
        private DevExpress.XtraEditors.LabelControl labEndingETD;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.GroupControl bcDetail;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private System.Windows.Forms.BindingSource bsDetailList;
        private DevExpress.XtraEditors.CheckEdit ckbGap;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.LabelControl labMessage;
        private DevExpress.XtraEditors.CheckEdit ckbMuit;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.CheckButton Maximize_checkButton;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barStartCheck;
        private DevExpress.XtraBars.BarButtonItem barChecking;
        private DevExpress.XtraBars.BarButtonItem barNotifiedBillOwner;
        private DevExpress.XtraBars.BarButtonItem barCompleted;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barWriteOff;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbLaunchCompany;
        private DevExpress.XtraEditors.LabelControl labbLaunchUser;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscCheckUser;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscLaunchUser;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCheckCompany;
        private DevExpress.XtraEditors.LabelControl labCheckUser;
        private DevExpress.XtraEditors.LabelControl labCheckCompany;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbOperType;
        private DevExpress.XtraEditors.TextEdit txtStatus;
        private System.Windows.Forms.BindingSource bsAgentCheckBill;
        private System.Windows.Forms.ToolTip toolTipOperType;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraGrid.GridControl CheckList_gridControl;
        private ICP.Framework.ClientComponents.Controls.LWBandedGridView AdvancedCheckList_gridView;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbBase;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colETD;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBLNo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCurrency;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbAgent1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBillNo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgent1Debit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgent1Credit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgent1Balance;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbGap;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGap;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbAgent2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgent2Balance;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgent2Debit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgent2Credit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgent2BillNo;
        private DevExpress.XtraGrid.Views.Grid.GridView SimpleCheckList_gridView;
        private DevExpress.XtraGrid.Columns.GridColumn etd_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn blno_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn currency_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn debit1_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn credit1_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn balance1_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn gap_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn balance2_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn credit2_gridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn debit2_gridColumn;


    }
}

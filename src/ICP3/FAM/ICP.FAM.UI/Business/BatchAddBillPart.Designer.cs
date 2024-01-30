namespace ICP.FAM.UI.Business
{
    partial class BatchAddBillPart
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.bsChargeList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rseAmount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOriginalProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupBaseInfo = new System.Windows.Forms.GroupBox();
            this.stxtChargingCode = new DevExpress.XtraEditors.ButtonEdit();
            this.labChargingCode = new DevExpress.XtraEditors.LabelControl();
            this.labBank = new DevExpress.XtraEditors.LabelControl();
            this.cmbBank = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.btnBulid = new DevExpress.XtraEditors.SimpleButton();
            this.rdoProfitType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.seAmount = new DevExpress.XtraEditors.SpinEdit();
            this.sePercentage = new DevExpress.XtraEditors.SpinEdit();
            this.cmbCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.groupFeeInfo = new System.Windows.Forms.GroupBox();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupProfit = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.groupBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChargingCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoProfitType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePercentage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            this.groupFeeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupProfit.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsChargeList
            // 
            this.bsChargeList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BatchChargeList);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsChargeList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gcMain.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcMain.Location = new System.Drawing.Point(3, 44);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbCurrency,
            this.rseAmount});
            this.gcMain.Size = new System.Drawing.Size(994, 293);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperationNO,
            this.colAmount,
            this.colProfit,
            this.colOriginalProfit});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanged);
            // 
            // colOperationNO
            // 
            this.colOperationNO.FieldName = "OperationNO";
            this.colOperationNO.Name = "colOperationNO";
            this.colOperationNO.Visible = true;
            this.colOperationNO.VisibleIndex = 0;
            this.colOperationNO.Width = 197;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.rseAmount;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            this.colAmount.Width = 211;
            // 
            // rseAmount
            // 
            this.rseAmount.AutoHeight = false;
            this.rseAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rseAmount.Name = "rseAmount";
            // 
            // colProfit
            // 
            this.colProfit.Caption = "Profit";
            this.colProfit.FieldName = "Profit";
            this.colProfit.Name = "colProfit";
            this.colProfit.OptionsColumn.AllowEdit = false;
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 3;
            this.colProfit.Width = 294;
            // 
            // colOriginalProfit
            // 
            this.colOriginalProfit.Caption = "OriginalProfit";
            this.colOriginalProfit.FieldName = "OriginalProfit";
            this.colOriginalProfit.Name = "colOriginalProfit";
            this.colOriginalProfit.OptionsColumn.AllowEdit = false;
            this.colOriginalProfit.Visible = true;
            this.colOriginalProfit.VisibleIndex = 2;
            this.colOriginalProfit.Width = 271;
            // 
            // rcmbCurrency
            // 
            this.rcmbCurrency.AutoHeight = false;
            this.rcmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbCurrency.Name = "rcmbCurrency";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barRemove,
            this.barClose});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Down_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar2.FloatLocation = new System.Drawing.Point(381, 249);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar2.Text = "Custom 3";
            // 
            // barRemove
            // 
            this.barRemove.Caption = "&Remove";
            this.barRemove.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 1;
            this.barRemove.Name = "barRemove";
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(3, 18);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(994, 26);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
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
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 439);
            this.barDockControlBottom.Size = new System.Drawing.Size(1000, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 413);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1000, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 413);
            // 
            // groupBaseInfo
            // 
            this.groupBaseInfo.Controls.Add(this.stxtChargingCode);
            this.groupBaseInfo.Controls.Add(this.labChargingCode);
            this.groupBaseInfo.Controls.Add(this.labBank);
            this.groupBaseInfo.Controls.Add(this.cmbBank);
            this.groupBaseInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBaseInfo.Name = "groupBaseInfo";
            this.groupBaseInfo.Size = new System.Drawing.Size(395, 73);
            this.groupBaseInfo.TabIndex = 12;
            this.groupBaseInfo.TabStop = false;
            this.groupBaseInfo.Text = "BaseInfo";
            // 
            // stxtChargingCode
            // 
            this.dxErrorProvider1.SetIconAlignment(this.stxtChargingCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtChargingCode.Location = new System.Drawing.Point(89, 46);
            this.stxtChargingCode.MenuManager = this.barManager1;
            this.stxtChargingCode.Name = "stxtChargingCode";
            this.stxtChargingCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtChargingCode.Size = new System.Drawing.Size(297, 21);
            this.stxtChargingCode.TabIndex = 1;
            // 
            // labChargingCode
            // 
            this.labChargingCode.Location = new System.Drawing.Point(8, 47);
            this.labChargingCode.Name = "labChargingCode";
            this.labChargingCode.Size = new System.Drawing.Size(75, 14);
            this.labChargingCode.TabIndex = 52;
            this.labChargingCode.Text = "ChargingCode";
            // 
            // labBank
            // 
            this.labBank.Location = new System.Drawing.Point(8, 22);
            this.labBank.Name = "labBank";
            this.labBank.Size = new System.Drawing.Size(26, 14);
            this.labBank.TabIndex = 52;
            this.labBank.Text = "Bank";
            // 
            // cmbBank
            // 
            this.cmbBank.EditValue = "公司配置下对应的客户";
            this.dxErrorProvider1.SetIconAlignment(this.cmbBank, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbBank.Location = new System.Drawing.Point(87, 19);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBank.Size = new System.Drawing.Size(299, 21);
            this.cmbBank.TabIndex = 0;
            this.cmbBank.TabStop = false;
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(18, 22);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(48, 14);
            this.labCurrency.TabIndex = 52;
            this.labCurrency.Text = "Currency";
            // 
            // btnBulid
            // 
            this.btnBulid.Location = new System.Drawing.Point(479, 18);
            this.btnBulid.Name = "btnBulid";
            this.btnBulid.Size = new System.Drawing.Size(64, 48);
            this.btnBulid.TabIndex = 4;
            this.btnBulid.Text = "Bulid";
            this.btnBulid.Click += new System.EventHandler(this.btnBulidBill_Click);
            // 
            // rdoProfitType
            // 
            this.dxErrorProvider1.SetIconAlignment(this.rdoProfitType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.rdoProfitType.Location = new System.Drawing.Point(348, 18);
            this.rdoProfitType.MenuManager = this.barManager1;
            this.rdoProfitType.Name = "rdoProfitType";
            this.rdoProfitType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Percentage"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Amount")});
            this.rdoProfitType.Size = new System.Drawing.Size(127, 48);
            this.rdoProfitType.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(330, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "%";
            // 
            // seAmount
            // 
            this.seAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.seAmount, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.seAmount.Location = new System.Drawing.Point(197, 45);
            this.seAmount.Name = "seAmount";
            this.seAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seAmount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.seAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.seAmount.Size = new System.Drawing.Size(127, 21);
            this.seAmount.TabIndex = 2;
            this.seAmount.TabStop = false;
            // 
            // sePercentage
            // 
            this.sePercentage.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.sePercentage, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.sePercentage.Location = new System.Drawing.Point(197, 19);
            this.sePercentage.Name = "sePercentage";
            this.sePercentage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sePercentage.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.sePercentage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.sePercentage.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.sePercentage.Size = new System.Drawing.Size(127, 21);
            this.sePercentage.TabIndex = 1;
            this.sePercentage.TabStop = false;
            // 
            // cmbCurrency
            // 
            this.dxErrorProvider1.SetIconAlignment(this.cmbCurrency, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCurrency.Location = new System.Drawing.Point(72, 19);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(119, 21);
            this.cmbCurrency.TabIndex = 0;
            this.cmbCurrency.TabStop = false;
            // 
            // groupFeeInfo
            // 
            this.groupFeeInfo.Controls.Add(this.gcMain);
            this.groupFeeInfo.Controls.Add(this.standaloneBarDockControl1);
            this.groupFeeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupFeeInfo.Location = new System.Drawing.Point(0, 99);
            this.groupFeeInfo.Name = "groupFeeInfo";
            this.groupFeeInfo.Size = new System.Drawing.Size(1000, 340);
            this.groupFeeInfo.TabIndex = 13;
            this.groupFeeInfo.TabStop = false;
            this.groupFeeInfo.Text = "Bill Info";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupProfit);
            this.panel1.Controls.Add(this.groupBaseInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 73);
            this.panel1.TabIndex = 18;
            // 
            // groupProfit
            // 
            this.groupProfit.Controls.Add(this.sePercentage);
            this.groupProfit.Controls.Add(this.labCurrency);
            this.groupProfit.Controls.Add(this.seAmount);
            this.groupProfit.Controls.Add(this.labelControl1);
            this.groupProfit.Controls.Add(this.rdoProfitType);
            this.groupProfit.Controls.Add(this.btnBulid);
            this.groupProfit.Controls.Add(this.cmbCurrency);
            this.groupProfit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupProfit.Location = new System.Drawing.Point(395, 0);
            this.groupProfit.Name = "groupProfit";
            this.groupProfit.Size = new System.Drawing.Size(605, 73);
            this.groupProfit.TabIndex = 13;
            this.groupProfit.TabStop = false;
            this.groupProfit.Text = "Profit";
            // 
            // BatchAddBillPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupFeeInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BatchAddBillPart";
            this.Size = new System.Drawing.Size(1000, 439);
            ((System.ComponentModel.ISupportInitialize)(this.bsChargeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.groupBaseInfo.ResumeLayout(false);
            this.groupBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtChargingCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoProfitType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePercentage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            this.groupFeeInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupProfit.ResumeLayout(false);
            this.groupProfit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsChargeList;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colProfit;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.GroupBox groupFeeInfo;
        private System.Windows.Forms.GroupBox groupBaseInfo;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraEditors.SpinEdit sePercentage;
        private DevExpress.XtraEditors.RadioGroup rdoProfitType;
        protected DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit seAmount;
        private DevExpress.XtraEditors.SimpleButton btnBulid;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNO;
        private DevExpress.XtraBars.BarButtonItem barClose;
        protected DevExpress.XtraEditors.LabelControl labBank;
        protected DevExpress.XtraEditors.LabelControl labCurrency;
        protected DevExpress.XtraEditors.LabelControl labChargingCode;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBank;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rseAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colOriginalProfit;
        private DevExpress.XtraEditors.ButtonEdit stxtChargingCode;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private System.Windows.Forms.GroupBox groupProfit;
        private System.Windows.Forms.Panel panel1;

    }
}

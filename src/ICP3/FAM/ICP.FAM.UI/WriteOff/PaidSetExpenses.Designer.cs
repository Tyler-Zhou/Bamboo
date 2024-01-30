namespace ICP.FAM.UI.WriteOff
{
    partial class PaidSetExpenses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaidSetExpenses));
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.labMessage = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgType = new System.Windows.Forms.ImageList(this.components);
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCurrencyID = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cheComChargeName = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.cmbGL = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheComChargeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlTop.Appearance.ForeColor = System.Drawing.Color.Red;
            this.pnlTop.Appearance.Options.UseBackColor = true;
            this.pnlTop.Appearance.Options.UseForeColor = true;
            this.pnlTop.Controls.Add(this.labMessage);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(694, 24);
            this.pnlTop.TabIndex = 1;
            // 
            // labMessage
            // 
            this.labMessage.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Appearance.Options.UseForeColor = true;
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.Location = new System.Drawing.Point(2, 2);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(0, 14);
            this.labMessage.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcMain);
            this.pnlMain.Controls.Add(this.labelControl1);
            this.pnlMain.Controls.Add(this.barDockControlLeft);
            this.pnlMain.Controls.Add(this.barDockControlRight);
            this.pnlMain.Controls.Add(this.barDockControlBottom);
            this.pnlMain.Controls.Add(this.barDockControlTop);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(694, 250);
            this.pnlMain.TabIndex = 2;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 42);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbCurrencyID,
            this.cheComChargeName,
            this.cmbGL,
            this.cmbWay});
            this.gcMain.Size = new System.Drawing.Size(690, 206);
            this.gcMain.TabIndex = 35;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.WriteOffCharge);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWay,
            this.colCustomerName,
            this.colBillNo,
            this.colChargeID,
            this.colCurrencyID,
            this.colAmount,
            this.colRate,
            this.colRemark});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colWay
            // 
            this.colWay.Caption = "方向";
            this.colWay.ColumnEdit = this.cmbWay;
            this.colWay.FieldName = "Way";
            this.colWay.Name = "colWay";
            this.colWay.OptionsColumn.AllowEdit = false;
            this.colWay.Visible = true;
            this.colWay.VisibleIndex = 0;
            this.colWay.Width = 49;
            // 
            // cmbWay
            // 
            this.cmbWay.AutoHeight = false;
            this.cmbWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWay.Name = "cmbWay";
            this.cmbWay.SmallImages = this.imgType;
            // 
            // imgType
            // 
            this.imgType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgType.ImageStream")));
            this.imgType.TransparentColor = System.Drawing.Color.Transparent;
            this.imgType.Images.SetKeyName(0, "+.png");
            this.imgType.Images.SetKeyName(1, "-.png");
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            this.colCustomerName.Width = 139;
            // 
            // colBillNo
            // 
            this.colBillNo.Caption = "账单号";
            this.colBillNo.FieldName = "BillNo";
            this.colBillNo.Name = "colBillNo";
            this.colBillNo.Visible = true;
            this.colBillNo.VisibleIndex = 2;
            this.colBillNo.Width = 62;
            // 
            // colChargeID
            // 
            this.colChargeID.Caption = "会计科目";
            this.colChargeID.FieldName = "GLFullName";
            this.colChargeID.Name = "colChargeID";
            this.colChargeID.Visible = true;
            this.colChargeID.VisibleIndex = 3;
            this.colChargeID.Width = 108;
            // 
            // colCurrencyID
            // 
            this.colCurrencyID.Caption = "币种";
            this.colCurrencyID.ColumnEdit = this.cmbCurrencyID;
            this.colCurrencyID.FieldName = "CurrencyID";
            this.colCurrencyID.Name = "colCurrencyID";
            this.colCurrencyID.Visible = true;
            this.colCurrencyID.VisibleIndex = 4;
            this.colCurrencyID.Width = 46;
            // 
            // cmbCurrencyID
            // 
            this.cmbCurrencyID.AutoHeight = false;
            this.cmbCurrencyID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrencyID.Name = "cmbCurrencyID";
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 5;
            this.colAmount.Width = 64;
            // 
            // colRate
            // 
            this.colRate.Caption = "汇率";
            this.colRate.FieldName = "ExchangeRate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 6;
            this.colRate.Width = 57;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 7;
            this.colRemark.Width = 144;
            // 
            // cheComChargeName
            // 
            this.cheComChargeName.AutoHeight = false;
            this.cheComChargeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cheComChargeName.Name = "cheComChargeName";
            // 
            // cmbGL
            // 
            this.cmbGL.AutoHeight = false;
            this.cmbGL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGL.Name = "cmbGL";
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(2, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(0, 14);
            this.labelControl1.TabIndex = 0;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 220);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(692, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 220);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 248);
            this.barDockControlBottom.Size = new System.Drawing.Size(690, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(690, 26);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.pnlMain;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barSave,
            this.barClose,
            this.barAdd});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&A)";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 3;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 1;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "保存";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // PaidSetExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "PaidSetExpenses";
            this.Size = new System.Drawing.Size(694, 274);
            this.Load += new System.EventHandler(this.PaidSetExpenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheComChargeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.LabelControl labMessage;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNo;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeID;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbGL;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyID;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrencyID;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit cheComChargeName;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.BindingSource bsList;
        private System.Windows.Forms.ImageList imgType;
        private DevExpress.XtraBars.BarButtonItem barAdd;
    }
}

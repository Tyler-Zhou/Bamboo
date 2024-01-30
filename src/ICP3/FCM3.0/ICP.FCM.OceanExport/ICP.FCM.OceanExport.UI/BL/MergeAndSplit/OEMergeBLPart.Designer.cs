namespace ICP.FCM.OceanExport.UI.BL
{
    partial class OEMergeBLPart
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colReservation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsignee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotifyParty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurementUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeightUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labTip = new DevExpress.XtraEditors.LabelControl();
            this.bsContainer = new System.Windows.Forms.BindingSource(this.components);
            this.gcCtn = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvCtn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCtnNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSealNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRelation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Location = new System.Drawing.Point(15, 32);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(709, 311);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.OceanExport.UI.BL.MergeBLInfo);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colReservation,
            this.colNO,
            this.colShipper,
            this.colConsignee,
            this.colNotifyParty,
            this.colContainerNo,
            this.colMeasurement,
            this.colMeasurementUnitName,
            this.colQuantity,
            this.colQuantityUnitName,
            this.colWeight,
            this.colWeightUnitName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanged);
            // 
            // colReservation
            // 
            this.colReservation.Caption = "Reservation";
            this.colReservation.ColumnEdit = this.rCheckEdit1;
            this.colReservation.CustomizationCaption = "Reservation";
            this.colReservation.FieldName = "Reservation";
            this.colReservation.Name = "colReservation";
            this.colReservation.Visible = true;
            this.colReservation.VisibleIndex = 0;
            this.colReservation.Width = 83;
            // 
            // colNO
            // 
            this.colNO.Caption = "NO";
            this.colNO.FieldName = "No";
            this.colNO.Name = "colNO";
            this.colNO.OptionsColumn.AllowEdit = false;
            this.colNO.Visible = true;
            this.colNO.VisibleIndex = 1;
            this.colNO.Width = 108;
            // 
            // colShipper
            // 
            this.colShipper.Caption = "Shipper";
            this.colShipper.FieldName = "ShipperName";
            this.colShipper.Name = "colShipper";
            this.colShipper.OptionsColumn.AllowEdit = false;
            this.colShipper.Visible = true;
            this.colShipper.VisibleIndex = 2;
            this.colShipper.Width = 108;
            // 
            // colConsignee
            // 
            this.colConsignee.Caption = "Consignee";
            this.colConsignee.FieldName = "ConsigneeName";
            this.colConsignee.Name = "colConsignee";
            this.colConsignee.OptionsColumn.AllowEdit = false;
            this.colConsignee.Visible = true;
            this.colConsignee.VisibleIndex = 3;
            this.colConsignee.Width = 108;
            // 
            // colNotifyParty
            // 
            this.colNotifyParty.Caption = "NotifyParty";
            this.colNotifyParty.FieldName = "NotifyPartyName";
            this.colNotifyParty.Name = "colNotifyParty";
            this.colNotifyParty.OptionsColumn.AllowEdit = false;
            this.colNotifyParty.Visible = true;
            this.colNotifyParty.VisibleIndex = 4;
            this.colNotifyParty.Width = 108;
            // 
            // colContainerNo
            // 
            this.colContainerNo.Caption = "CtnNo";
            this.colContainerNo.FieldName = "ContainerNo";
            this.colContainerNo.Name = "colContainerNo";
            this.colContainerNo.OptionsColumn.AllowEdit = false;
            this.colContainerNo.Visible = true;
            this.colContainerNo.VisibleIndex = 5;
            this.colContainerNo.Width = 171;
            // 
            // colMeasurement
            // 
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 6;
            this.colMeasurement.Width = 96;
            // 
            // colMeasurementUnitName
            // 
            this.colMeasurementUnitName.Caption = "Unit";
            this.colMeasurementUnitName.FieldName = "MeasurementUnitName";
            this.colMeasurementUnitName.Name = "colMeasurementUnitName";
            this.colMeasurementUnitName.Visible = true;
            this.colMeasurementUnitName.VisibleIndex = 7;
            this.colMeasurementUnitName.Width = 96;
            // 
            // colQuantity
            // 
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 8;
            this.colQuantity.Width = 96;
            // 
            // colQuantityUnitName
            // 
            this.colQuantityUnitName.Caption = "Unit";
            this.colQuantityUnitName.FieldName = "QuantityUnitName";
            this.colQuantityUnitName.Name = "colQuantityUnitName";
            this.colQuantityUnitName.Visible = true;
            this.colQuantityUnitName.VisibleIndex = 9;
            this.colQuantityUnitName.Width = 96;
            // 
            // colWeight
            // 
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 10;
            this.colWeight.Width = 96;
            // 
            // colWeightUnitName
            // 
            this.colWeightUnitName.Caption = "Unit";
            this.colWeightUnitName.FieldName = "WeightUnitName";
            this.colWeightUnitName.Name = "colWeightUnitName";
            this.colWeightUnitName.Visible = true;
            this.colWeightUnitName.VisibleIndex = 11;
            this.colWeightUnitName.Width = 96;
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
            this.barClose});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 1;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(738, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 369);
            this.barDockControlBottom.Size = new System.Drawing.Size(738, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 343);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(738, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 343);
            // 
            // labTip
            // 
            this.labTip.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labTip.Appearance.Options.UseBackColor = true;
            this.labTip.Appearance.Options.UseForeColor = true;
            this.labTip.Location = new System.Drawing.Point(15, 349);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(67, 14);
            this.labTip.TabIndex = 5;
            this.labTip.Text = "MeasureInfo";
            this.labTip.Visible = false;
            // 
            // bsContainer
            // 
            this.bsContainer.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBLContainerList);
            // 
            // gcCtn
            // 
            this.gcCtn.DataSource = this.bsContainer;
            this.gcCtn.Location = new System.Drawing.Point(15, 101);
            this.gcCtn.MainView = this.gvCtn;
            this.gcCtn.Name = "gcCtn";
            this.gcCtn.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbType});
            this.gcCtn.Size = new System.Drawing.Size(709, 242);
            this.gcCtn.TabIndex = 10;
            this.gcCtn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCtn});
            this.gcCtn.Visible = false;
            // 
            // gvCtn
            // 
            this.gvCtn.ChildGridLevelName = "Level1";
            this.gvCtn.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRelation,
            this.colCtnNo,
            this.colType,
            this.colSealNo});
            this.gvCtn.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
            this.gvCtn.GridControl = this.gcCtn;
            this.gvCtn.LevelIndent = 0;
            this.gvCtn.Name = "gvCtn";
            this.gvCtn.OptionsBehavior.Editable = false;
            this.gvCtn.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvCtn.OptionsSelection.MultiSelect = true;
            this.gvCtn.OptionsView.EnableAppearanceEvenRow = true;
            this.gvCtn.OptionsView.ShowDetailButtons = false;
            this.gvCtn.OptionsView.ShowGroupPanel = false;
            this.gvCtn.PreviewFieldName = "Date";
            // 
            // colCtnNo
            // 
            this.colCtnNo.Caption = "No";
            this.colCtnNo.FieldName = "No";
            this.colCtnNo.Name = "colCtnNo";
            this.colCtnNo.Visible = true;
            this.colCtnNo.VisibleIndex = 1;
            this.colCtnNo.Width = 164;
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.ColumnEdit = this.cmbType;
            this.colType.FieldName = "TypeID";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 146;
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Name = "cmbType";
            // 
            // colSealNo
            // 
            this.colSealNo.Caption = "SealNo";
            this.colSealNo.FieldName = "SealNo";
            this.colSealNo.Name = "colSealNo";
            this.colSealNo.Visible = true;
            this.colSealNo.VisibleIndex = 3;
            this.colSealNo.Width = 378;
            // 
            // colRelation
            // 
            this.colRelation.FieldName = "Relation";
            this.colRelation.Name = "colRelation";
            this.colRelation.Visible = true;
            this.colRelation.VisibleIndex = 0;
            // 
            // rCheckEdit1
            // 
            this.rCheckEdit1.AutoHeight = false;
            this.rCheckEdit1.Name = "rCheckEdit1";
            // 
            // OEMergeBLPart
            // 
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcCtn);
            this.Controls.Add(this.labTip);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OEMergeBLPart";
            this.Size = new System.Drawing.Size(738, 369);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colNO;
        private DevExpress.XtraGrid.Columns.GridColumn colShipper;
        private DevExpress.XtraGrid.Columns.GridColumn colConsignee;
        private DevExpress.XtraGrid.Columns.GridColumn colNotifyParty;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNo;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn colReservation;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurementUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colWeightUnitName;
        private DevExpress.XtraEditors.LabelControl labTip;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcCtn;
        private System.Windows.Forms.BindingSource bsContainer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCtn;
        private DevExpress.XtraGrid.Columns.GridColumn colRelation;
        private DevExpress.XtraGrid.Columns.GridColumn colCtnNo;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraGrid.Columns.GridColumn colSealNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rCheckEdit1;
    }
}

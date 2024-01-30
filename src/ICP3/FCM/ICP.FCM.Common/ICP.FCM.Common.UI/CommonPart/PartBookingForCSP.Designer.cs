namespace ICP.FCM.Common.UI.CommonPart
{
    partial class PartBookingForCSP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartBookingForCSP));
            this.barManagerMain = new DevExpress.XtraBars.BarManager(this.components);
            this.barMainMenu = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSetDefault = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControlMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bindingSourceMain = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsDefault = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsTruck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDeclaration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsInsurance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommodity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCustomer = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelFill = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).BeginInit();
            this.panelFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManagerMain
            // 
            this.barManagerMain.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMainMenu});
            this.barManagerMain.DockControls.Add(this.barDockControlTop);
            this.barManagerMain.DockControls.Add(this.barDockControlBottom);
            this.barManagerMain.DockControls.Add(this.barDockControlLeft);
            this.barManagerMain.DockControls.Add(this.barDockControlRight);
            this.barManagerMain.Form = this;
            this.barManagerMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDelete,
            this.barSetDefault});
            this.barManagerMain.MainMenu = this.barMainMenu;
            this.barManagerMain.MaxItemId = 9;
            // 
            // barMainMenu
            // 
            this.barMainMenu.BarName = "Main menu";
            this.barMainMenu.DockCol = 0;
            this.barMainMenu.DockRow = 0;
            this.barMainMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMainMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSetDefault, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barMainMenu.OptionsBar.MultiLine = true;
            this.barMainMenu.OptionsBar.UseWholeRow = true;
            this.barMainMenu.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("barAdd.Glyph")));
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barDelete.Glyph")));
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barSetDefault
            // 
            this.barSetDefault.Caption = "Set Default";
            this.barSetDefault.Glyph = ((System.Drawing.Image)(resources.GetObject("barSetDefault.Glyph")));
            this.barSetDefault.Id = 8;
            this.barSetDefault.Name = "barSetDefault";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(916, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 525);
            this.barDockControlBottom.Size = new System.Drawing.Size(916, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 499);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(916, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 499);
            // 
            // gridControlMain
            // 
            this.gridControlMain.DataSource = this.bindingSourceMain;
            this.gridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMain.Location = new System.Drawing.Point(2, 2);
            this.gridControlMain.MainView = this.gridViewMain;
            this.gridControlMain.Name = "gridControlMain";
            this.gridControlMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbCustomer});
            this.gridControlMain.Size = new System.Drawing.Size(912, 495);
            this.gridControlMain.TabIndex = 3;
            this.gridControlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // bindingSourceMain
            // 
            this.bindingSourceMain.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.BookingDelegate);
            // 
            // gridViewMain
            // 
            this.gridViewMain.ChildGridLevelName = "Level1";
            this.gridViewMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsDefault,
            this.colBookingName,
            this.colBookingDate,
            this.colIsTruck,
            this.colIsDeclaration,
            this.colIsInsurance,
            this.colContainers,
            this.colETD,
            this.colETA,
            this.colMarks,
            this.colCommodity,
            this.colCustomerName,
            this.colShipperName,
            this.colConsigneeName,
            this.colPOLName,
            this.colPOLAddress,
            this.colPODName,
            this.colPODAddress,
            this.colQuantity,
            this.colWeight,
            this.colMeasurement});
            this.gridViewMain.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
            this.gridViewMain.GridControl = this.gridControlMain;
            this.gridViewMain.LevelIndent = 0;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewMain.OptionsSelection.MultiSelect = true;
            this.gridViewMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewMain.OptionsView.ColumnAutoWidth = false;
            this.gridViewMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMain.OptionsView.ShowDetailButtons = false;
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            this.gridViewMain.PreviewFieldName = "Date";
            // 
            // colIsDefault
            // 
            this.colIsDefault.FieldName = "IsDefault";
            this.colIsDefault.Name = "colIsDefault";
            this.colIsDefault.Visible = true;
            this.colIsDefault.VisibleIndex = 0;
            // 
            // colBookingName
            // 
            this.colBookingName.FieldName = "BookingName";
            this.colBookingName.Name = "colBookingName";
            this.colBookingName.Visible = true;
            this.colBookingName.VisibleIndex = 1;
            // 
            // colBookingDate
            // 
            this.colBookingDate.FieldName = "BookingDate";
            this.colBookingDate.Name = "colBookingDate";
            this.colBookingDate.Visible = true;
            this.colBookingDate.VisibleIndex = 2;
            // 
            // colIsTruck
            // 
            this.colIsTruck.FieldName = "IsTruck";
            this.colIsTruck.Name = "colIsTruck";
            this.colIsTruck.Visible = true;
            this.colIsTruck.VisibleIndex = 3;
            // 
            // colIsDeclaration
            // 
            this.colIsDeclaration.FieldName = "IsDeclaration";
            this.colIsDeclaration.Name = "colIsDeclaration";
            this.colIsDeclaration.Visible = true;
            this.colIsDeclaration.VisibleIndex = 4;
            // 
            // colIsInsurance
            // 
            this.colIsInsurance.FieldName = "IsInsurance";
            this.colIsInsurance.Name = "colIsInsurance";
            this.colIsInsurance.Visible = true;
            this.colIsInsurance.VisibleIndex = 5;
            // 
            // colContainers
            // 
            this.colContainers.FieldName = "Containers";
            this.colContainers.Name = "colContainers";
            this.colContainers.Visible = true;
            this.colContainers.VisibleIndex = 6;
            // 
            // colETD
            // 
            this.colETD.FieldName = "ETDForPOL";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 7;
            // 
            // colETA
            // 
            this.colETA.FieldName = "ETAForPOD";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 8;
            // 
            // colMarks
            // 
            this.colMarks.FieldName = "Marks";
            this.colMarks.Name = "colMarks";
            this.colMarks.Visible = true;
            this.colMarks.VisibleIndex = 9;
            // 
            // colCommodity
            // 
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 10;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 11;
            // 
            // colShipperName
            // 
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 12;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 13;
            // 
            // colPOLName
            // 
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 14;
            // 
            // colPOLAddress
            // 
            this.colPOLAddress.FieldName = "POLAddress";
            this.colPOLAddress.Name = "colPOLAddress";
            this.colPOLAddress.Visible = true;
            this.colPOLAddress.VisibleIndex = 15;
            // 
            // colPODName
            // 
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 16;
            // 
            // colPODAddress
            // 
            this.colPODAddress.FieldName = "PODAddress";
            this.colPODAddress.Name = "colPODAddress";
            this.colPODAddress.Visible = true;
            this.colPODAddress.VisibleIndex = 17;
            // 
            // colQuantity
            // 
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 18;
            // 
            // colWeight
            // 
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 19;
            // 
            // colMeasurement
            // 
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 20;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.AutoHeight = false;
            this.cmbCustomer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomer.Name = "cmbCustomer";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.gridControlMain);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 26);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(916, 499);
            this.panelFill.TabIndex = 33;
            // 
            // PartBookingForCSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PartBookingForCSP";
            this.Size = new System.Drawing.Size(916, 525);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).EndInit();
            this.panelFill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerMain;
        private DevExpress.XtraBars.Bar barMainMenu;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl panelFill;
        private Framework.ClientComponents.Controls.LWGridControl gridControlMain;
        private System.Windows.Forms.BindingSource bindingSourceMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraBars.BarButtonItem barSetDefault;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDefault;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingName;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsTruck;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDeclaration;
        private DevExpress.XtraGrid.Columns.GridColumn colIsInsurance;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colMarks;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodity;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colContainers;
    }
}

namespace ICP.FCM.Common.UI.CommonPart
{
    partial class PartPOItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartPOItems));
            this.barManagerMain = new DevExpress.XtraBars.BarManager(this.components);
            this.barMainMenu = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barImport = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControlMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bindingSourceMain = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBillOfLadingNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurchaseOrderNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockKeepingUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturerPartNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCartonCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitCostPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.barImport,
            this.barSave});
            this.barManagerMain.MainMenu = this.barMainMenu;
            this.barManagerMain.MaxItemId = 10;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barImport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            // barImport
            // 
            this.barImport.Caption = "Import";
            this.barImport.Glyph = ((System.Drawing.Image)(resources.GetObject("barImport.Glyph")));
            this.barImport.Id = 8;
            this.barImport.Name = "barImport";
            // 
            // barSave
            // 
            this.barSave.Caption = "Save";
            this.barSave.Glyph = global::ICP.FCM.Common.UI.Properties.Resources.Save_16;
            this.barSave.Id = 9;
            this.barSave.Name = "barSave";
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
            this.bindingSourceMain.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.PurchaseOrderItem);
            // 
            // gridViewMain
            // 
            this.gridViewMain.ChildGridLevelName = "Level1";
            this.gridViewMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBillOfLadingNO,
            this.colContainerNO,
            this.colPurchaseOrderNO,
            this.colProductName,
            this.colStockKeepingUnit,
            this.colManufacturerPartNumber,
            this.colCartonCount,
            this.colQuantity,
            this.colUnitCostPrice,
            this.colWeight,
            this.colVolume});
            this.gridViewMain.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
            this.gridViewMain.GridControl = this.gridControlMain;
            this.gridViewMain.LevelIndent = 0;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewMain.OptionsSelection.MultiSelect = true;
            this.gridViewMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMain.OptionsView.ShowDetailButtons = false;
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            this.gridViewMain.PreviewFieldName = "Date";
            // 
            // colBillOfLadingNO
            // 
            this.colBillOfLadingNO.FieldName = "BillOfLadingNO";
            this.colBillOfLadingNO.Name = "colBillOfLadingNO";
            this.colBillOfLadingNO.Visible = true;
            this.colBillOfLadingNO.VisibleIndex = 0;
            this.colBillOfLadingNO.Width = 118;
            // 
            // colContainerNO
            // 
            this.colContainerNO.FieldName = "ContainerNO";
            this.colContainerNO.Name = "colContainerNO";
            this.colContainerNO.Visible = true;
            this.colContainerNO.VisibleIndex = 1;
            this.colContainerNO.Width = 118;
            // 
            // colPurchaseOrderNO
            // 
            this.colPurchaseOrderNO.Caption = "PO #";
            this.colPurchaseOrderNO.FieldName = "PurchaseOrderNO";
            this.colPurchaseOrderNO.Name = "colPurchaseOrderNO";
            this.colPurchaseOrderNO.Visible = true;
            this.colPurchaseOrderNO.VisibleIndex = 2;
            this.colPurchaseOrderNO.Width = 118;
            // 
            // colProductName
            // 
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 3;
            this.colProductName.Width = 79;
            // 
            // colStockKeepingUnit
            // 
            this.colStockKeepingUnit.Caption = "SKU";
            this.colStockKeepingUnit.FieldName = "StockKeepingUnit";
            this.colStockKeepingUnit.Name = "colStockKeepingUnit";
            this.colStockKeepingUnit.Visible = true;
            this.colStockKeepingUnit.VisibleIndex = 4;
            this.colStockKeepingUnit.Width = 118;
            // 
            // colManufacturerPartNumber
            // 
            this.colManufacturerPartNumber.Caption = "MPN";
            this.colManufacturerPartNumber.FieldName = "ManufacturerPartNumber";
            this.colManufacturerPartNumber.Name = "colManufacturerPartNumber";
            this.colManufacturerPartNumber.Visible = true;
            this.colManufacturerPartNumber.VisibleIndex = 5;
            this.colManufacturerPartNumber.Width = 118;
            // 
            // colCartonCount
            // 
            this.colCartonCount.Caption = "Carton";
            this.colCartonCount.FieldName = "CartonCount";
            this.colCartonCount.Name = "colCartonCount";
            this.colCartonCount.Visible = true;
            this.colCartonCount.VisibleIndex = 6;
            this.colCartonCount.Width = 49;
            // 
            // colQuantity
            // 
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 7;
            this.colQuantity.Width = 49;
            // 
            // colUnitCostPrice
            // 
            this.colUnitCostPrice.Caption = "Unit Cost";
            this.colUnitCostPrice.FieldName = "UnitCostPrice";
            this.colUnitCostPrice.Name = "colUnitCostPrice";
            this.colUnitCostPrice.Visible = true;
            this.colUnitCostPrice.VisibleIndex = 8;
            this.colUnitCostPrice.Width = 49;
            // 
            // colWeight
            // 
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 9;
            this.colWeight.Width = 49;
            // 
            // colVolume
            // 
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 10;
            this.colVolume.Width = 50;
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
            // PartPOItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PartPOItems";
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
        private DevExpress.XtraBars.BarButtonItem barImport;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colBillOfLadingNO;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNO;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchaseOrderNO;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colStockKeepingUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturerPartNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colCartonCount;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitCostPrice;
    }
}

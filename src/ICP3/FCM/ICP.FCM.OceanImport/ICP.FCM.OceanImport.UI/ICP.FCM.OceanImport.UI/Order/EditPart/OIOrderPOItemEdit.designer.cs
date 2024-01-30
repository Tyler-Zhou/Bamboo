namespace ICP.FCM.OceanImport.UI
{
    partial class POItemEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POItemEditPart));
            this.gridViewItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxt_20 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCartons = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riseNumber = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colColor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxt_50 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxt_200 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colHTSCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnits = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riseFloat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewPO = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxt_100 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colVendorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinalDestination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInWarehouseDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colOrderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.errorPO = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_200)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_100)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorPO)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewItem
            // 
            this.gridViewItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemNo,
            this.colCartons,
            this.colColor,
            this.colDescription,
            this.colHTSCode,
            this.colSize,
            this.colUnits,
            this.colWeight,
            this.colVolume});
            this.gridViewItem.GridControl = this.gridControl1;
            this.gridViewItem.Name = "gridViewItem";
            this.gridViewItem.NewItemRowText = "Click here to add a new row.Select column header and press key delete to delete r" +
                "ow.";
            this.gridViewItem.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewItem.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewItem.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewItem.OptionsSelection.MultiSelect = true;
            this.gridViewItem.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewItem.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridViewItem.OptionsView.ShowGroupPanel = false;
            this.gridViewItem.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewItem_CellValueChanged);
            this.gridViewItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridViewItem_KeyDown);
            this.gridViewItem.Click += new System.EventHandler(this.gridViewItem_Click);
            this.gridViewItem.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewItem_CellValueChanging);
            this.gridViewItem.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewItem_ShowingEditor);
            // 
            // colItemNo
            // 
            this.colItemNo.Caption = "No";
            this.colItemNo.ColumnEdit = this.rtxt_20;
            this.colItemNo.FieldName = "No";
            this.colItemNo.Name = "colItemNo";
            this.colItemNo.Visible = true;
            this.colItemNo.VisibleIndex = 0;
            // 
            // rtxt_20
            // 
            this.rtxt_20.AutoHeight = false;
            this.rtxt_20.MaxLength = 20;
            this.rtxt_20.Name = "rtxt_20";
            // 
            // colCartons
            // 
            this.colCartons.Caption = "Cartons";
            this.colCartons.ColumnEdit = this.riseNumber;
            this.colCartons.DisplayFormat.FormatString = "N00";
            this.colCartons.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCartons.FieldName = "Cartons";
            this.colCartons.Name = "colCartons";
            this.colCartons.Visible = true;
            this.colCartons.VisibleIndex = 1;
            // 
            // riseNumber
            // 
            this.riseNumber.AutoHeight = false;
            this.riseNumber.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riseNumber.DisplayFormat.FormatString = "N00";
            this.riseNumber.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riseNumber.EditFormat.FormatString = "N00";
            this.riseNumber.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riseNumber.IsFloatValue = false;
            this.riseNumber.Mask.EditMask = "N00";
            this.riseNumber.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.riseNumber.Name = "riseNumber";
            // 
            // colColor
            // 
            this.colColor.Caption = "Color";
            this.colColor.ColumnEdit = this.rtxt_50;
            this.colColor.FieldName = "Color";
            this.colColor.Name = "colColor";
            this.colColor.Visible = true;
            this.colColor.VisibleIndex = 2;
            // 
            // rtxt_50
            // 
            this.rtxt_50.AutoHeight = false;
            this.rtxt_50.MaxLength = 50;
            this.rtxt_50.Name = "rtxt_50";
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.ColumnEdit = this.rtxt_200;
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            // 
            // rtxt_200
            // 
            this.rtxt_200.AutoHeight = false;
            this.rtxt_200.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtxt_200.MaxLength = 200;
            this.rtxt_200.Name = "rtxt_200";
            this.rtxt_200.ShowIcon = false;
            // 
            // colHTSCode
            // 
            this.colHTSCode.Caption = "HTSCode";
            this.colHTSCode.ColumnEdit = this.rtxt_20;
            this.colHTSCode.FieldName = "HTSCode";
            this.colHTSCode.Name = "colHTSCode";
            this.colHTSCode.Visible = true;
            this.colHTSCode.VisibleIndex = 4;
            // 
            // colSize
            // 
            this.colSize.Caption = "Size";
            this.colSize.ColumnEdit = this.rtxt_50;
            this.colSize.FieldName = "Size";
            this.colSize.Name = "colSize";
            this.colSize.Visible = true;
            this.colSize.VisibleIndex = 5;
            // 
            // colUnits
            // 
            this.colUnits.Caption = "Units";
            this.colUnits.ColumnEdit = this.riseNumber;
            this.colUnits.DisplayFormat.FormatString = "N00";
            this.colUnits.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUnits.FieldName = "Units";
            this.colUnits.Name = "colUnits";
            this.colUnits.Visible = true;
            this.colUnits.VisibleIndex = 6;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "Weight";
            this.colWeight.ColumnEdit = this.riseFloat;
            this.colWeight.DisplayFormat.FormatString = "F3";
            this.colWeight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 7;
            // 
            // riseFloat
            // 
            this.riseFloat.AutoHeight = false;
            this.riseFloat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riseFloat.DisplayFormat.FormatString = "F3";
            this.riseFloat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riseFloat.EditFormat.FormatString = "F3";
            this.riseFloat.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riseFloat.Mask.EditMask = "F3";
            this.riseFloat.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.riseFloat.Name = "riseFloat";
            this.riseFloat.NullValuePromptShowForEmptyValue = true;
            // 
            // colVolume
            // 
            this.colVolume.Caption = "Volume";
            this.colVolume.ColumnEdit = this.riseFloat;
            this.colVolume.DisplayFormat.FormatString = "F3";
            this.colVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 8;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gridViewItem;
            gridLevelNode1.RelationName = "Items";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridViewPO;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riseFloat,
            this.rDateEdit1,
            this.rtxt_20,
            this.rtxt_200,
            this.rtxt_100,
            this.rtxt_50,
            this.riseNumber});
            this.gridControl1.Size = new System.Drawing.Size(705, 242);
            this.gridControl1.TabIndex = 11;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPO,
            this.gridViewItem});
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OceanImportPOList);
            // 
            // gridViewPO
            // 
            this.gridViewPO.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gridViewPO.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridViewPO.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colPODescription,
            this.colVendorName,
            this.colBuyerName,
            this.colFinalDestination,
            this.colInWarehouseDate,
            this.colOrderDate,
            this.colCreateByName,
            this.colCreateDate});
            this.gridViewPO.GridControl = this.gridControl1;
            this.gridViewPO.Name = "gridViewPO";
            this.gridViewPO.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridViewPO.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewPO.OptionsPrint.ExpandAllDetails = true;
            this.gridViewPO.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewPO.OptionsView.ShowGroupPanel = false;
            this.gridViewPO.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewPO_CellValueChanged);
            // 
            // colNo
            // 
            this.colNo.Caption = "PO No";
            this.colNo.ColumnEdit = this.rtxt_20;
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            // 
            // colPODescription
            // 
            this.colPODescription.Caption = "PO Description";
            this.colPODescription.ColumnEdit = this.rtxt_100;
            this.colPODescription.FieldName = "PODescription";
            this.colPODescription.Name = "colPODescription";
            this.colPODescription.Visible = true;
            this.colPODescription.VisibleIndex = 1;
            // 
            // rtxt_100
            // 
            this.rtxt_100.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtxt_100.MaxLength = 100;
            this.rtxt_100.Name = "rtxt_100";
            this.rtxt_100.ShowIcon = false;
            // 
            // colVendorName
            // 
            this.colVendorName.Caption = "Vendor";
            this.colVendorName.ColumnEdit = this.rtxt_200;
            this.colVendorName.FieldName = "VendorName";
            this.colVendorName.Name = "colVendorName";
            this.colVendorName.Visible = true;
            this.colVendorName.VisibleIndex = 2;
            // 
            // colBuyerName
            // 
            this.colBuyerName.Caption = "Buyer";
            this.colBuyerName.ColumnEdit = this.rtxt_200;
            this.colBuyerName.FieldName = "BuyerName";
            this.colBuyerName.Name = "colBuyerName";
            this.colBuyerName.Visible = true;
            this.colBuyerName.VisibleIndex = 3;
            // 
            // colFinalDestination
            // 
            this.colFinalDestination.Caption = "Final Dest";
            this.colFinalDestination.ColumnEdit = this.rtxt_100;
            this.colFinalDestination.FieldName = "FinalDestination";
            this.colFinalDestination.Name = "colFinalDestination";
            this.colFinalDestination.Visible = true;
            this.colFinalDestination.VisibleIndex = 4;
            // 
            // colInWarehouseDate
            // 
            this.colInWarehouseDate.Caption = "In Whs Date";
            this.colInWarehouseDate.ColumnEdit = this.rDateEdit1;
            this.colInWarehouseDate.FieldName = "InWarehouseDate";
            this.colInWarehouseDate.Name = "colInWarehouseDate";
            this.colInWarehouseDate.Visible = true;
            this.colInWarehouseDate.VisibleIndex = 5;
            // 
            // rDateEdit1
            // 
            this.rDateEdit1.AutoHeight = false;
            this.rDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rDateEdit1.Name = "rDateEdit1";
            this.rDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // colOrderDate
            // 
            this.colOrderDate.Caption = "Order Date";
            this.colOrderDate.ColumnEdit = this.rDateEdit1;
            this.colOrderDate.FieldName = "OrderDate";
            this.colOrderDate.Name = "colOrderDate";
            this.colOrderDate.Visible = true;
            this.colOrderDate.VisibleIndex = 6;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "Create By";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.AllowEdit = false;
            this.colCreateByName.OptionsFilter.AllowAutoFilter = false;
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 7;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "Create Date";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.OptionsFilter.AllowAutoFilter = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 8;
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
            this.barAdd,
            this.barDelete});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&New";
            this.barAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("barAdd.Glyph")));
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Remove";
            this.barDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barDelete.Glyph")));
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(705, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 268);
            this.barDockControlBottom.Size = new System.Drawing.Size(705, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 242);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(705, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 242);
            // 
            // errorPO
            // 
            this.errorPO.ContainerControl = this;
            this.errorPO.DataSource = this.bindingSource1;
            // 
            // POItemEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "POItemEditPart";
            this.Size = new System.Drawing.Size(705, 268);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_200)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_100)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorPO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorPO;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPO;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPODescription;
        private DevExpress.XtraGrid.Columns.GridColumn colVendorName;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyerName;
        private DevExpress.XtraGrid.Columns.GridColumn colFinalDestination;
        private DevExpress.XtraGrid.Columns.GridColumn colInWarehouseDate;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseFloat;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItem;
        private DevExpress.XtraGrid.Columns.GridColumn colItemNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCartons;
        private DevExpress.XtraGrid.Columns.GridColumn colColor;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colHTSCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSize;
        private DevExpress.XtraGrid.Columns.GridColumn colUnits;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxt_20;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rtxt_200;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rtxt_100;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxt_50;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseNumber;
    }
}

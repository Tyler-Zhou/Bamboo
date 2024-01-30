namespace ICP.FCM.OceanExport.UI.Container
{
    partial class ContainerCargosList
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRemove = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.lwgrid = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsContainerCargo = new System.Windows.Forms.BindingSource(this.components);
            this.gvCargo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricbxMBLs = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colMarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommodity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityUnitID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbQuantityUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeightUnitID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbWeightUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurementUnitID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbMeasurementUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.riseFloat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.riseNumber = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwgrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainerCargo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCargo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricbxMBLs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbQuantityUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbWeightUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbMeasurementUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiNew,
            this.bbiRemove});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbiNew
            // 
            this.bbiNew.Caption = "New";
            this.bbiNew.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Add_16;
            this.bbiNew.Id = 0;
            this.bbiNew.Name = "bbiNew";
            this.bbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNew_ItemClick);
            // 
            // bbiRemove
            // 
            this.bbiRemove.Caption = "Remove";
            this.bbiRemove.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Delete_16;
            this.bbiRemove.Id = 1;
            this.bbiRemove.Name = "bbiRemove";
            this.bbiRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRemove_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(698, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 445);
            this.barDockControlBottom.Size = new System.Drawing.Size(698, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 419);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(698, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 419);
            // 
            // lwgrid
            // 
            this.lwgrid.DataSource = this.bsContainerCargo;
            this.lwgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwgrid.Location = new System.Drawing.Point(0, 26);
            this.lwgrid.MainView = this.gvCargo;
            this.lwgrid.MenuManager = this.barManager1;
            this.lwgrid.Name = "lwgrid";
            this.lwgrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricbxMBLs,
            this.rcmbQuantityUnit,
            this.rcmbWeightUnit,
            this.rcmbMeasurementUnit,
            this.riseFloat,
            this.riseNumber});
            this.lwgrid.Size = new System.Drawing.Size(698, 419);
            this.lwgrid.TabIndex = 4;
            this.lwgrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCargo});
            // 
            // bsContainerCargo
            // 
            this.bsContainerCargo.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanContainerCargoList);
            // 
            // gvCargo
            // 
            this.gvCargo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMBLNo,
            this.colMarks,
            this.colCommodity,
            this.colQuantity,
            this.colQuantityUnitID,
            this.colWeight,
            this.colWeightUnitID,
            this.colMeasurement,
            this.colMeasurementUnitID});
            this.gvCargo.GridControl = this.lwgrid;
            this.gvCargo.Name = "gvCargo";
            this.gvCargo.OptionsView.ShowGroupPanel = false;
            // 
            // colMBLNo
            // 
            this.colMBLNo.ColumnEdit = this.ricbxMBLs;
            this.colMBLNo.FieldName = "MBLNo";
            this.colMBLNo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colMBLNo.Name = "colMBLNo";
            this.colMBLNo.Visible = true;
            this.colMBLNo.VisibleIndex = 0;
            // 
            // ricbxMBLs
            // 
            this.ricbxMBLs.AutoHeight = false;
            this.ricbxMBLs.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricbxMBLs.Name = "ricbxMBLs";
            // 
            // colMarks
            // 
            this.colMarks.FieldName = "Marks";
            this.colMarks.Name = "colMarks";
            this.colMarks.Visible = true;
            this.colMarks.VisibleIndex = 1;
            // 
            // colCommodity
            // 
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 2;
            // 
            // colQuantity
            // 
            this.colQuantity.ColumnEdit = this.riseNumber;
            this.colQuantity.DisplayFormat.FormatString = "N00";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 3;
            // 
            // colQuantityUnitID
            // 
            this.colQuantityUnitID.ColumnEdit = this.rcmbQuantityUnit;
            this.colQuantityUnitID.FieldName = "QuantityUnitID";
            this.colQuantityUnitID.Name = "colQuantityUnitID";
            this.colQuantityUnitID.Visible = true;
            this.colQuantityUnitID.VisibleIndex = 4;
            // 
            // rcmbQuantityUnit
            // 
            this.rcmbQuantityUnit.AutoHeight = false;
            this.rcmbQuantityUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbQuantityUnit.Name = "rcmbQuantityUnit";
            // 
            // colWeight
            // 
            this.colWeight.ColumnEdit = this.riseFloat;
            this.colWeight.DisplayFormat.FormatString = "F3";
            this.colWeight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 5;
            // 
            // colWeightUnitID
            // 
            this.colWeightUnitID.ColumnEdit = this.rcmbWeightUnit;
            this.colWeightUnitID.FieldName = "WeightUnitID";
            this.colWeightUnitID.Name = "colWeightUnitID";
            this.colWeightUnitID.Visible = true;
            this.colWeightUnitID.VisibleIndex = 6;
            // 
            // rcmbWeightUnit
            // 
            this.rcmbWeightUnit.AutoHeight = false;
            this.rcmbWeightUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbWeightUnit.Name = "rcmbWeightUnit";
            // 
            // colMeasurement
            // 
            this.colMeasurement.ColumnEdit = this.riseFloat;
            this.colMeasurement.DisplayFormat.FormatString = "F3";
            this.colMeasurement.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 7;
            // 
            // colMeasurementUnitID
            // 
            this.colMeasurementUnitID.ColumnEdit = this.rcmbMeasurementUnit;
            this.colMeasurementUnitID.FieldName = "MeasurementUnitID";
            this.colMeasurementUnitID.Name = "colMeasurementUnitID";
            this.colMeasurementUnitID.Visible = true;
            this.colMeasurementUnitID.VisibleIndex = 8;
            // 
            // rcmbMeasurementUnit
            // 
            this.rcmbMeasurementUnit.AutoHeight = false;
            this.rcmbMeasurementUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbMeasurementUnit.Name = "rcmbMeasurementUnit";
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
            10000000,
            0,
            0,
            0});
            this.riseFloat.Name = "riseFloat";
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
            this.riseNumber.Mask.EditMask = "N00";
            this.riseNumber.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.riseNumber.Name = "riseNumber";
            // 
            // ContainerCargosList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lwgrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ContainerCargosList";
            this.Size = new System.Drawing.Size(698, 467);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwgrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainerCargo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCargo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricbxMBLs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbQuantityUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbWeightUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbMeasurementUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private DevExpress.XtraBars.BarButtonItem bbiRemove;
        private ICP.Framework.ClientComponents.Controls.LWGridControl lwgrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCargo;
        private System.Windows.Forms.BindingSource bsContainerCargo;
        private DevExpress.XtraGrid.Columns.GridColumn colMBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colMarks;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityUnitID;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colWeightUnitID;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurementUnitID;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox ricbxMBLs;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbQuantityUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbWeightUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbMeasurementUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseNumber;
    }
}

namespace ICP.FCM.OtherBusiness.UI.Common
{
    partial class FeePart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeePart));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsFee = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewFee = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgType = new System.Windows.Forms.ImageList(this.components);
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEditCustomer = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colChargingCodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbChargingCode = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.unitPriceSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.qtySpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.amountSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEditChargingCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChargingCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitPriceSpinEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtySpinEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountSpinEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditChargingCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
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
            this.barManager1.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("barAdd.Glyph")));
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barDelete.Glyph")));
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(656, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 241);
            this.barDockControlBottom.Size = new System.Drawing.Size(656, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 215);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(656, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 215);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsFee;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridViewFee;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnEditCustomer,
            this.btnEditChargingCode,
            this.cmbCurrency,
            this.qtySpinEdit,
            this.unitPriceSpinEdit,
            this.amountSpinEdit,
            this.cmbWay,
            this.cmbChargingCode});
            this.gridControl1.Size = new System.Drawing.Size(656, 215);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFee});
            // 
            // bsFee
            // 
            this.bsFee.DataSource = typeof(ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.OBFeeList);
            // 
            // gridViewFee
            // 
            this.gridViewFee.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gridViewFee.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridViewFee.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWay,
            this.colCustomerName,
            this.colChargingCodeName,
            this.colUnitPrice,
            this.colQuantity,
            this.colCurrency,
            this.colAmount,
            this.colRemark});
            this.gridViewFee.GridControl = this.gridControl1;
            this.gridViewFee.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewFee.Name = "gridViewFee";
            this.gridViewFee.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewFee.OptionsSelection.MultiSelect = true;
            this.gridViewFee.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewFee.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewFee.OptionsView.ShowGroupPanel = false;
            this.gridViewFee.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewFee.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewFee_CellValueChanged);
            // 
            // colWay
            // 
            this.colWay.ColumnEdit = this.cmbWay;
            this.colWay.FieldName = "Way";
            this.colWay.Name = "colWay";
            this.colWay.Visible = true;
            this.colWay.VisibleIndex = 0;
            this.colWay.Width = 48;
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
            this.imgType.Images.SetKeyName(0, "+-.png");
            this.imgType.Images.SetKeyName(1, "+.png");
            this.imgType.Images.SetKeyName(2, "-.png");
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.ColumnEdit = this.btnEditCustomer;
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            this.colCustomerName.Width = 99;
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.AutoHeight = false;
            this.btnEditCustomer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditCustomer.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.btnEditCustomer.Name = "btnEditCustomer";
            // 
            // colChargingCodeName
            // 
            this.colChargingCodeName.Caption = "ChargingCode";
            this.colChargingCodeName.ColumnEdit = this.cmbChargingCode;
            this.colChargingCodeName.FieldName = "ChargingCodeName";
            this.colChargingCodeName.Name = "colChargingCodeName";
            this.colChargingCodeName.Visible = true;
            this.colChargingCodeName.VisibleIndex = 2;
            this.colChargingCodeName.Width = 99;
            // 
            // cmbChargingCode
            // 
            this.cmbChargingCode.AutoHeight = false;
            this.cmbChargingCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbChargingCode.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.cmbChargingCode.Name = "cmbChargingCode";
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.Caption = "UnitPrice";
            this.colUnitPrice.ColumnEdit = this.unitPriceSpinEdit;
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 3;
            this.colUnitPrice.Width = 76;
            // 
            // unitPriceSpinEdit
            // 
            this.unitPriceSpinEdit.AutoHeight = false;
            this.unitPriceSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.unitPriceSpinEdit.DisplayFormat.FormatString = "N2";
            this.unitPriceSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.unitPriceSpinEdit.Mask.EditMask = "N2";
            //this.unitPriceSpinEdit.MaxValue = new decimal(new int[] {
            //-1530494977,
            //232830,
            //0,
            //131072});
            this.unitPriceSpinEdit.Name = "unitPriceSpinEdit";
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "QTY";
            this.colQuantity.ColumnEdit = this.qtySpinEdit;
            this.colQuantity.DisplayFormat.FormatString = "N00";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 4;
            this.colQuantity.Width = 60;
            // 
            // qtySpinEdit
            // 
            this.qtySpinEdit.AutoHeight = false;
            this.qtySpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.qtySpinEdit.DisplayFormat.FormatString = "N00";
            this.qtySpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.qtySpinEdit.EditFormat.FormatString = "N00";
            this.qtySpinEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.qtySpinEdit.Mask.EditMask = "N00";
            this.qtySpinEdit.MaxLength = 9;
            this.qtySpinEdit.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            196608});
            this.qtySpinEdit.Name = "qtySpinEdit";
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "Currency";
            this.colCurrency.ColumnEdit = this.cmbCurrency;
            this.colCurrency.FieldName = "CurrencyID";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 5;
            this.colCurrency.Width = 82;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoHeight = false;
            this.cmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // colAmount
            // 
            this.colAmount.AppearanceCell.BackColor = System.Drawing.Color.LightBlue;
            this.colAmount.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colAmount.AppearanceCell.Options.UseBackColor = true;
            this.colAmount.AppearanceCell.Options.UseForeColor = true;
            this.colAmount.ColumnEdit = this.amountSpinEdit;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 6;
            this.colAmount.Width = 77;
            // 
            // amountSpinEdit
            // 
            this.amountSpinEdit.AutoHeight = false;
            this.amountSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.amountSpinEdit.DisplayFormat.FormatString = "N2";
            this.amountSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.amountSpinEdit.Mask.EditMask = "N2";
            this.amountSpinEdit.Name = "amountSpinEdit";
            this.amountSpinEdit.ReadOnly = true;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 7;
            this.colRemark.Width = 85;
            // 
            // btnEditChargingCode
            // 
            this.btnEditChargingCode.AutoHeight = false;
            this.btnEditChargingCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditChargingCode.Name = "btnEditChargingCode";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsFee;
            // 
            // FeePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FeePart";
            this.Size = new System.Drawing.Size(656, 241);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbChargingCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitPriceSpinEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtySpinEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountSpinEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditChargingCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;

        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private System.Windows.Forms.BindingSource bsFee;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colChargingCodeName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEditCustomer;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEditChargingCode;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit qtySpinEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit unitPriceSpinEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit amountSpinEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private System.Windows.Forms.ImageList imgType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbChargingCode;
        private DevExpress.XtraGrid.Columns.GridColumn colWay;
        public ICP.Framework.ClientComponents.Controls.LWGridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewFee;
    }
}

namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class QuotedPriceRatesPart
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
            this.bmRates = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.barRatesAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barRatesRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bsQPRatesList = new System.Windows.Forms.BindingSource(this.components);
            this.gcRatesList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvRatesList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuotedPriceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfReceiptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarrier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCarrier = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.colTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit40HQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSurchargeDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSurcharges = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsSurcharge = new System.Windows.Forms.BindingSource(this.components);
            this.gvSurcharges = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChargeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rilueCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.panelFill = new DevExpress.XtraEditors.PanelControl();
            this.bmSurcharges = new DevExpress.XtraBars.BarManager(this.components);
            this.barSurchargeTools = new DevExpress.XtraBars.Bar();
            this.barSurchargesAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barSurchargesDelete = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bmRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsQPRatesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRatesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRatesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSurcharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurcharge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSurcharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilueCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).BeginInit();
            this.panelFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bmSurcharges)).BeginInit();
            this.SuspendLayout();
            // 
            // bmRates
            // 
            this.bmRates.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools});
            this.bmRates.DockControls.Add(this.barDockControlTop);
            this.bmRates.DockControls.Add(this.barDockControlBottom);
            this.bmRates.DockControls.Add(this.barDockControlLeft);
            this.bmRates.DockControls.Add(this.barDockControlRight);
            this.bmRates.Form = this;
            this.bmRates.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barRatesAdd,
            this.barRatesRemove});
            this.bmRates.MaxItemId = 4;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRatesAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRatesRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DrawDragBorder = false;
            this.barTools.Text = "Tools";
            // 
            // barRatesAdd
            // 
            this.barRatesAdd.Caption = "Add";
            this.barRatesAdd.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barRatesAdd.Id = 0;
            this.barRatesAdd.Name = "barRatesAdd";
            // 
            // barRatesRemove
            // 
            this.barRatesRemove.Caption = "Delete";
            this.barRatesRemove.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Delete_16;
            this.barRatesRemove.Id = 1;
            this.barRatesRemove.Name = "barRatesRemove";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(790, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 500);
            this.barDockControlBottom.Size = new System.Drawing.Size(790, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 474);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(790, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 474);
            // 
            // bsQPRatesList
            // 
            this.bsQPRatesList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.QuotedPriceRatesList);
            // 
            // gcRatesList
            // 
            this.gcRatesList.DataSource = this.bsQPRatesList;
            this.gcRatesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRatesList.Location = new System.Drawing.Point(2, 2);
            this.gcRatesList.MainView = this.gvRatesList;
            this.gcRatesList.MenuManager = this.bmRates;
            this.gcRatesList.Name = "gcRatesList";
            this.gcRatesList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbCarrier});
            this.gcRatesList.Size = new System.Drawing.Size(786, 320);
            this.gcRatesList.TabIndex = 5;
            this.gcRatesList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRatesList});
            // 
            // gvRatesList
            // 
            this.gvRatesList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colQuotedPriceID,
            this.colPlaceOfReceiptName,
            this.colPOLName,
            this.colPODName,
            this.colPlaceOfDeliveryName,
            this.colCarrier,
            this.colTT,
            this.colUnit20,
            this.colUnit40,
            this.colUnit40HQ,
            this.colUnit45,
            this.colSurchargeDescription});
            this.gvRatesList.GridControl = this.gcRatesList;
            this.gvRatesList.IndicatorWidth = 27;
            this.gvRatesList.Name = "gvRatesList";
            this.gvRatesList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvRatesList.OptionsView.ColumnAutoWidth = false;
            this.gvRatesList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvRatesList.OptionsView.ShowDetailButtons = false;
            this.gvRatesList.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colQuotedPriceID
            // 
            this.colQuotedPriceID.FieldName = "QuotedPriceID";
            this.colQuotedPriceID.Name = "colQuotedPriceID";
            // 
            // colPlaceOfReceiptName
            // 
            this.colPlaceOfReceiptName.Caption = "Place Of Receipt";
            this.colPlaceOfReceiptName.FieldName = "PlaceOfReceiptName";
            this.colPlaceOfReceiptName.Name = "colPlaceOfReceiptName";
            this.colPlaceOfReceiptName.Visible = true;
            this.colPlaceOfReceiptName.VisibleIndex = 0;
            this.colPlaceOfReceiptName.Width = 150;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "POL";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 1;
            this.colPOLName.Width = 150;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "POD";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 2;
            this.colPODName.Width = 150;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.Caption = "Place Of Delivery";
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 3;
            this.colPlaceOfDeliveryName.Width = 150;
            // 
            // colCarrier
            // 
            this.colCarrier.Caption = "Carrier";
            this.colCarrier.ColumnEdit = this.cmbCarrier;
            this.colCarrier.FieldName = "Carrier";
            this.colCarrier.Name = "colCarrier";
            this.colCarrier.Visible = true;
            this.colCarrier.VisibleIndex = 4;
            this.colCarrier.Width = 100;
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.AutoHeight = false;
            this.cmbCarrier.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCarrier.Name = "cmbCarrier";
            this.cmbCarrier.SeparatorChar = '/';
            // 
            // colTT
            // 
            this.colTT.Caption = "T/T";
            this.colTT.FieldName = "TT";
            this.colTT.Name = "colTT";
            this.colTT.Visible = true;
            this.colTT.VisibleIndex = 5;
            this.colTT.Width = 40;
            // 
            // colUnit20
            // 
            this.colUnit20.Caption = "20\'";
            this.colUnit20.DisplayFormat.FormatString = "${0:D}";
            this.colUnit20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colUnit20.FieldName = "Unit20";
            this.colUnit20.Name = "colUnit20";
            this.colUnit20.Visible = true;
            this.colUnit20.VisibleIndex = 6;
            this.colUnit20.Width = 70;
            // 
            // colUnit40
            // 
            this.colUnit40.Caption = "40\'";
            this.colUnit40.DisplayFormat.FormatString = "${0:D}";
            this.colUnit40.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colUnit40.FieldName = "Unit40";
            this.colUnit40.Name = "colUnit40";
            this.colUnit40.Visible = true;
            this.colUnit40.VisibleIndex = 7;
            this.colUnit40.Width = 70;
            // 
            // colUnit40HQ
            // 
            this.colUnit40HQ.Caption = "40HQ\'";
            this.colUnit40HQ.DisplayFormat.FormatString = "${0:D}";
            this.colUnit40HQ.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colUnit40HQ.FieldName = "Unit40HQ";
            this.colUnit40HQ.Name = "colUnit40HQ";
            this.colUnit40HQ.Visible = true;
            this.colUnit40HQ.VisibleIndex = 8;
            this.colUnit40HQ.Width = 70;
            // 
            // colUnit45
            // 
            this.colUnit45.Caption = "45\'";
            this.colUnit45.DisplayFormat.FormatString = "${0:D}";
            this.colUnit45.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colUnit45.FieldName = "Unit45";
            this.colUnit45.Name = "colUnit45";
            this.colUnit45.Visible = true;
            this.colUnit45.VisibleIndex = 9;
            this.colUnit45.Width = 70;
            // 
            // colSurchargeDescription
            // 
            this.colSurchargeDescription.Caption = "Surcharge Description";
            this.colSurchargeDescription.FieldName = "SurchargeDescription";
            this.colSurchargeDescription.Name = "colSurchargeDescription";
            // 
            // gcSurcharges
            // 
            this.gcSurcharges.DataSource = this.bsSurcharge;
            this.gcSurcharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSurcharges.Location = new System.Drawing.Point(2, 28);
            this.gcSurcharges.MainView = this.gvSurcharges;
            this.gcSurcharges.MenuManager = this.bmRates;
            this.gcSurcharges.Name = "gcSurcharges";
            this.gcSurcharges.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rilueCurrency,
            this.cmbType});
            this.gcSurcharges.Size = new System.Drawing.Size(786, 120);
            this.gcSurcharges.TabIndex = 10;
            this.gcSurcharges.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSurcharges});
            // 
            // bsSurcharge
            // 
            this.bsSurcharge.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.QPSurcharge);
            // 
            // gvSurcharges
            // 
            this.gvSurcharges.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChargeName,
            this.colCurrencyName,
            this.colQuantity,
            this.colContainerType,
            this.colUnitPrice});
            this.gvSurcharges.GridControl = this.gcSurcharges;
            this.gvSurcharges.IndicatorWidth = 27;
            this.gvSurcharges.Name = "gvSurcharges";
            this.gvSurcharges.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvSurcharges.OptionsSelection.MultiSelect = true;
            this.gvSurcharges.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvSurcharges.OptionsView.ColumnAutoWidth = false;
            this.gvSurcharges.OptionsView.EnableAppearanceEvenRow = true;
            this.gvSurcharges.OptionsView.ShowDetailButtons = false;
            this.gvSurcharges.OptionsView.ShowGroupPanel = false;
            // 
            // colChargeName
            // 
            this.colChargeName.Caption = "Charge Name";
            this.colChargeName.FieldName = "ChargeName";
            this.colChargeName.Name = "colChargeName";
            this.colChargeName.Visible = true;
            this.colChargeName.VisibleIndex = 0;
            this.colChargeName.Width = 150;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "Currency Name";
            this.colCurrencyName.ColumnEdit = this.rilueCurrency;
            this.colCurrencyName.FieldName = "CurrencyID";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 1;
            this.colCurrencyName.Width = 80;
            // 
            // rilueCurrency
            // 
            this.rilueCurrency.AutoHeight = false;
            this.rilueCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rilueCurrency.Name = "rilueCurrency";
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 2;
            this.colQuantity.Width = 100;
            // 
            // colContainerType
            // 
            this.colContainerType.Caption = "Per Container/Per Bill";
            this.colContainerType.ColumnEdit = this.cmbType;
            this.colContainerType.FieldName = "PerTypeName";
            this.colContainerType.Name = "colContainerType";
            this.colContainerType.Visible = true;
            this.colContainerType.VisibleIndex = 3;
            this.colContainerType.Width = 100;
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Name = "cmbType";
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.Caption = "Unit Price";
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 4;
            this.colUnitPrice.Width = 100;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.gcSurcharges);
            this.panelBottom.Controls.Add(this.barDockControl3);
            this.panelBottom.Controls.Add(this.barDockControl4);
            this.panelBottom.Controls.Add(this.barDockControl2);
            this.panelBottom.Controls.Add(this.barDockControl1);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 350);
            this.panelBottom.MinimumSize = new System.Drawing.Size(0, 150);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(790, 150);
            this.panelBottom.TabIndex = 11;
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(2, 28);
            this.barDockControl3.Size = new System.Drawing.Size(0, 120);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(788, 28);
            this.barDockControl4.Size = new System.Drawing.Size(0, 120);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(2, 148);
            this.barDockControl2.Size = new System.Drawing.Size(786, 0);
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(2, 2);
            this.barDockControl1.Size = new System.Drawing.Size(786, 26);
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.gcRatesList);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 26);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(790, 324);
            this.panelFill.TabIndex = 12;
            // 
            // bmSurcharges
            // 
            this.bmSurcharges.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barSurchargeTools});
            this.bmSurcharges.DockControls.Add(this.barDockControl1);
            this.bmSurcharges.DockControls.Add(this.barDockControl2);
            this.bmSurcharges.DockControls.Add(this.barDockControl3);
            this.bmSurcharges.DockControls.Add(this.barDockControl4);
            this.bmSurcharges.Form = this.panelBottom;
            this.bmSurcharges.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSurchargesAdd,
            this.barSurchargesDelete});
            this.bmSurcharges.MaxItemId = 4;
            // 
            // barSurchargeTools
            // 
            this.barSurchargeTools.BarName = "Surcharge Tools";
            this.barSurchargeTools.DockCol = 0;
            this.barSurchargeTools.DockRow = 0;
            this.barSurchargeTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barSurchargeTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSurchargesAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSurchargesDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barSurchargeTools.OptionsBar.AllowQuickCustomization = false;
            this.barSurchargeTools.OptionsBar.DrawDragBorder = false;
            this.barSurchargeTools.Text = "Tools";
            // 
            // barSurchargesAdd
            // 
            this.barSurchargesAdd.Caption = "Add";
            this.barSurchargesAdd.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barSurchargesAdd.Id = 0;
            this.barSurchargesAdd.Name = "barSurchargesAdd";
            // 
            // barSurchargesDelete
            // 
            this.barSurchargesDelete.Caption = "Delete";
            this.barSurchargesDelete.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Delete_16;
            this.barSurchargesDelete.Id = 1;
            this.barSurchargesDelete.Name = "barSurchargesDelete";
            // 
            // QuotedPriceRatesPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "QuotedPriceRatesPart";
            this.Size = new System.Drawing.Size(790, 500);
            ((System.ComponentModel.ISupportInitialize)(this.bmRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsQPRatesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRatesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRatesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSurcharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSurcharge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSurcharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilueCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).EndInit();
            this.panelFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bmSurcharges)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager bmRates;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.BindingSource bsQPRatesList;
        private Framework.ClientComponents.Controls.LWGridControl gcRatesList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRatesList;
        private DevExpress.XtraBars.BarButtonItem barRatesAdd;
        private DevExpress.XtraBars.BarButtonItem barRatesRemove;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colQuotedPriceID;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfReceiptName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrier;
        private DevExpress.XtraGrid.Columns.GridColumn colTT;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit20;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit40;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit40HQ;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit45;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit cmbCarrier;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private Framework.ClientComponents.Controls.LWGridControl gcSurcharges;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSurcharges;
        private DevExpress.XtraEditors.PanelControl panelFill;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarManager bmSurcharges;
        private DevExpress.XtraBars.Bar barSurchargeTools;
        private DevExpress.XtraBars.BarButtonItem barSurchargesAdd;
        private DevExpress.XtraBars.BarButtonItem barSurchargesDelete;
        private System.Windows.Forms.BindingSource bsSurcharge;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeName;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colSurchargeDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilueCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
    }
}

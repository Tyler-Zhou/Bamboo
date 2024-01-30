namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class SelectQuotedPrice
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.dtpEndDate = new DevExpress.XtraEditors.DateEdit();
            this.dteStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.stxtNo = new DevExpress.XtraEditors.TextEdit();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.stxtDelivery = new DevExpress.XtraEditors.TextEdit();
            this.labDelivery = new DevExpress.XtraEditors.LabelControl();
            this.stxtPOD = new DevExpress.XtraEditors.TextEdit();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.stxtReceipt = new DevExpress.XtraEditors.TextEdit();
            this.labReceipt = new DevExpress.XtraEditors.LabelControl();
            this.stxtPOL = new DevExpress.XtraEditors.TextEdit();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.panelFill = new System.Windows.Forms.Panel();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransportClauseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfReceiptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommodity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReceipt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            this.panelFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.dtpEndDate);
            this.panelTop.Controls.Add(this.dteStartDate);
            this.panelTop.Controls.Add(this.labTo);
            this.panelTop.Controls.Add(this.labFrom);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.stxtNo);
            this.panelTop.Controls.Add(this.labNo);
            this.panelTop.Controls.Add(this.stxtDelivery);
            this.panelTop.Controls.Add(this.labDelivery);
            this.panelTop.Controls.Add(this.stxtPOD);
            this.panelTop.Controls.Add(this.labPOD);
            this.panelTop.Controls.Add(this.stxtReceipt);
            this.panelTop.Controls.Add(this.labReceipt);
            this.panelTop.Controls.Add(this.stxtPOL);
            this.panelTop.Controls.Add(this.labPOL);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(828, 58);
            this.panelTop.TabIndex = 0;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.EditValue = new System.DateTime(2016, 2, 29, 13, 51, 15, 149);
            this.dtpEndDate.Location = new System.Drawing.Point(466, 30);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.Mask.EditMask = "";
            this.dtpEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtpEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpEndDate.Size = new System.Drawing.Size(115, 21);
            this.dtpEndDate.TabIndex = 15;
            // 
            // dteStartDate
            // 
            this.dteStartDate.EditValue = new System.DateTime(2016, 2, 29, 13, 51, 27, 3);
            this.dteStartDate.Location = new System.Drawing.Point(274, 30);
            this.dteStartDate.Name = "dteStartDate";
            this.dteStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStartDate.Properties.Mask.EditMask = "";
            this.dteStartDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteStartDate.Size = new System.Drawing.Size(115, 21);
            this.dteStartDate.TabIndex = 14;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(395, 33);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 17;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(205, 33);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 16;
            this.labFrom.Text = "From";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(669, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search";
            // 
            // stxtNo
            // 
            this.stxtNo.Location = new System.Drawing.Point(84, 3);
            this.stxtNo.Name = "stxtNo";
            this.stxtNo.Size = new System.Drawing.Size(111, 21);
            this.stxtNo.TabIndex = 12;
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(8, 6);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(15, 14);
            this.labNo.TabIndex = 11;
            this.labNo.Text = "No";
            this.labNo.ToolTip = "Port Of Loading";
            // 
            // stxtDelivery
            // 
            this.stxtDelivery.Location = new System.Drawing.Point(84, 30);
            this.stxtDelivery.Name = "stxtDelivery";
            this.stxtDelivery.Size = new System.Drawing.Size(115, 21);
            this.stxtDelivery.TabIndex = 12;
            // 
            // labDelivery
            // 
            this.labDelivery.Location = new System.Drawing.Point(8, 33);
            this.labDelivery.Name = "labDelivery";
            this.labDelivery.Size = new System.Drawing.Size(42, 14);
            this.labDelivery.TabIndex = 11;
            this.labDelivery.Text = "Delivery";
            this.labDelivery.ToolTip = "Port Of Loading";
            // 
            // stxtPOD
            // 
            this.stxtPOD.Location = new System.Drawing.Point(654, 3);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Size = new System.Drawing.Size(115, 21);
            this.stxtPOD.TabIndex = 12;
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(586, 6);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 11;
            this.labPOD.Text = "POD";
            this.labPOD.ToolTip = "Port Of Loading";
            // 
            // stxtReceipt
            // 
            this.stxtReceipt.Location = new System.Drawing.Point(275, 3);
            this.stxtReceipt.Name = "stxtReceipt";
            this.stxtReceipt.Size = new System.Drawing.Size(115, 21);
            this.stxtReceipt.TabIndex = 12;
            // 
            // labReceipt
            // 
            this.labReceipt.Location = new System.Drawing.Point(205, 6);
            this.labReceipt.Name = "labReceipt";
            this.labReceipt.Size = new System.Drawing.Size(41, 14);
            this.labReceipt.TabIndex = 11;
            this.labReceipt.Text = "Receipt";
            this.labReceipt.ToolTip = "Port Of Loading";
            // 
            // stxtPOL
            // 
            this.stxtPOL.Location = new System.Drawing.Point(466, 3);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Size = new System.Drawing.Size(115, 21);
            this.stxtPOL.TabIndex = 12;
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(395, 6);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 11;
            this.labPOL.Text = "POL";
            this.labPOL.ToolTip = "Port Of Loading";
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.gcMain);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 58);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(828, 336);
            this.panelFill.TabIndex = 1;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(828, 336);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.QuotedPricePartInfo);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colNo,
            this.colTransportClauseName,
            this.colPlaceOfReceiptName,
            this.colPOLName,
            this.colPODName,
            this.colPlaceOfDeliveryName,
            this.colCommodity});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 27;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsDetail.AllowZoomDetail = false;
            this.gvMain.OptionsDetail.EnableMasterViewMode = false;
            this.gvMain.OptionsDetail.ShowDetailTabs = false;
            this.gvMain.OptionsDetail.SmartDetailExpand = false;
            this.gvMain.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsSelection.UseIndicatorForSelection = false;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "QuotedPriceID";
            this.colID.Name = "colID";
            // 
            // colNo
            // 
            this.colNo.FieldName = "QuotedPriceNo";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 120;
            // 
            // colTransportClauseName
            // 
            this.colTransportClauseName.Caption = "Mode";
            this.colTransportClauseName.FieldName = "TransportClauseName";
            this.colTransportClauseName.Name = "colTransportClauseName";
            this.colTransportClauseName.Visible = true;
            this.colTransportClauseName.VisibleIndex = 1;
            this.colTransportClauseName.Width = 100;
            // 
            // colPlaceOfReceiptName
            // 
            this.colPlaceOfReceiptName.Caption = "Place Of Receipt";
            this.colPlaceOfReceiptName.FieldName = "PlaceOfReceiptName";
            this.colPlaceOfReceiptName.Name = "colPlaceOfReceiptName";
            this.colPlaceOfReceiptName.Visible = true;
            this.colPlaceOfReceiptName.VisibleIndex = 2;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "POL";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 3;
            this.colPOLName.Width = 120;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "POD";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 4;
            this.colPODName.Width = 120;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.Caption = "Place Of Delivery";
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 5;
            this.colPlaceOfDeliveryName.Width = 120;
            // 
            // colCommodity
            // 
            this.colCommodity.Caption = "Commodity";
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 6;
            this.colCommodity.Width = 120;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 394);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(828, 42);
            this.panelBottom.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(325, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(447, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            // 
            // SelectQuotedPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "SelectQuotedPrice";
            this.Size = new System.Drawing.Size(828, 436);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtReceipt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            this.panelFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.BindingSource bsList;
        /// <summary>
        /// 
        /// </summary>
        protected Framework.ClientComponents.Controls.LWGridControl gcMain;
        /// <summary>
        /// 
        /// </summary>
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodity;
        private DevExpress.XtraGrid.Columns.GridColumn colTransportClauseName;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.TextEdit stxtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.TextEdit stxtPOL;
        private DevExpress.XtraEditors.TextEdit stxtPOD;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit dtpEndDate;
        private DevExpress.XtraEditors.DateEdit dteStartDate;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.TextEdit stxtReceipt;
        private DevExpress.XtraEditors.LabelControl labReceipt;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfReceiptName;
        private DevExpress.XtraEditors.TextEdit stxtDelivery;
        private DevExpress.XtraEditors.LabelControl labDelivery;
    }
}

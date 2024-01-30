namespace IC.FRM.Prototype
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colContractName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingLineName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotifyNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentTermName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublisherName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.inquireList1 = new IC.FRM.Prototype.InquireList();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colContractName,
            this.colShippingLineName,
            this.colContractNo,
            this.colCarrierName,
            this.colConsigneeNames,
            this.colNotifyNames,
            this.colPaymentTermName,
            this.colCurrencyName,
            this.colContractType,
            this.colFromDate,
            this.colToDate,
            this.colState,
            this.colPublisherName});
            this.gvMain.IndicatorWidth = 27;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsSelection.UseIndicatorForSelection = false;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colContractName
            // 
            this.colContractName.Caption = "Name";
            this.colContractName.FieldName = "ContractName";
            this.colContractName.Name = "colContractName";
            this.colContractName.Visible = true;
            this.colContractName.VisibleIndex = 0;
            this.colContractName.Width = 120;
            // 
            // colShippingLineName
            // 
            this.colShippingLineName.Caption = "ShippingLine";
            this.colShippingLineName.FieldName = "ShippingLineName";
            this.colShippingLineName.Name = "colShippingLineName";
            this.colShippingLineName.Visible = true;
            this.colShippingLineName.VisibleIndex = 1;
            this.colShippingLineName.Width = 120;
            // 
            // colContractNo
            // 
            this.colContractNo.Caption = "ContractNo";
            this.colContractNo.FieldName = "ContractNo";
            this.colContractNo.Name = "colContractNo";
            this.colContractNo.Visible = true;
            this.colContractNo.VisibleIndex = 2;
            this.colContractNo.Width = 120;
            // 
            // colCarrierName
            // 
            this.colCarrierName.Caption = "Carrier";
            this.colCarrierName.FieldName = "CarrierName";
            this.colCarrierName.Name = "colCarrierName";
            this.colCarrierName.Visible = true;
            this.colCarrierName.VisibleIndex = 3;
            this.colCarrierName.Width = 120;
            // 
            // colConsigneeNames
            // 
            this.colConsigneeNames.Caption = "Consignee";
            this.colConsigneeNames.FieldName = "ConsigneeNames";
            this.colConsigneeNames.Name = "colConsigneeNames";
            this.colConsigneeNames.Visible = true;
            this.colConsigneeNames.VisibleIndex = 4;
            this.colConsigneeNames.Width = 120;
            // 
            // colNotifyNames
            // 
            this.colNotifyNames.Caption = "Notify";
            this.colNotifyNames.FieldName = "NotifyPartyNames";
            this.colNotifyNames.Name = "colNotifyNames";
            this.colNotifyNames.Visible = true;
            this.colNotifyNames.VisibleIndex = 5;
            this.colNotifyNames.Width = 120;
            // 
            // colPaymentTermName
            // 
            this.colPaymentTermName.Caption = "Payment";
            this.colPaymentTermName.FieldName = "PaymentTermName";
            this.colPaymentTermName.Name = "colPaymentTermName";
            this.colPaymentTermName.Visible = true;
            this.colPaymentTermName.VisibleIndex = 6;
            this.colPaymentTermName.Width = 100;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.Caption = "Currency";
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 7;
            this.colCurrencyName.Width = 80;
            // 
            // colContractType
            // 
            this.colContractType.Caption = "Type";
            this.colContractType.FieldName = "ContractType";
            this.colContractType.Name = "colContractType";
            this.colContractType.Visible = true;
            this.colContractType.VisibleIndex = 8;
            this.colContractType.Width = 80;
            // 
            // colFromDate
            // 
            this.colFromDate.Caption = "(Duration)From";
            this.colFromDate.FieldName = "FromDate";
            this.colFromDate.Name = "colFromDate";
            this.colFromDate.Visible = true;
            this.colFromDate.VisibleIndex = 9;
            this.colFromDate.Width = 103;
            // 
            // colToDate
            // 
            this.colToDate.Caption = "(Duration)To";
            this.colToDate.FieldName = "ToDate";
            this.colToDate.Name = "colToDate";
            this.colToDate.Visible = true;
            this.colToDate.VisibleIndex = 10;
            this.colToDate.Width = 93;
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 11;
            this.colState.Width = 80;
            // 
            // colPublisherName
            // 
            this.colPublisherName.Caption = "Publisher";
            this.colPublisherName.FieldName = "PublisherName";
            this.colPublisherName.Name = "colPublisherName";
            this.colPublisherName.Visible = true;
            this.colPublisherName.VisibleIndex = 12;
            this.colPublisherName.Width = 80;
            // 
            // inquireList1
            // 
            this.inquireList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inquireList1.Location = new System.Drawing.Point(0, 0);
            this.inquireList1.Name = "inquireList1";
            this.inquireList1.Size = new System.Drawing.Size(789, 392);
            this.inquireList1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 392);
            this.Controls.Add(this.inquireList1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colContractName;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingLineName;
        private DevExpress.XtraGrid.Columns.GridColumn colContractNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeNames;
        private DevExpress.XtraGrid.Columns.GridColumn colNotifyNames;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentTermName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colContractType;
        private DevExpress.XtraGrid.Columns.GridColumn colFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn colToDate;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colPublisherName;
        private InquireList inquireList1;
    }
}


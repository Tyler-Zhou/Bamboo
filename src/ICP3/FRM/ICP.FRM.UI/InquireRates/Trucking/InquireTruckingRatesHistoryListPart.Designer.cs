using System.Windows.Forms;
namespace ICP.FRM.UI.InquireRates
{
    partial class InquireTruckingRatesHistoryListPart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InquireTruckingRatesHistoryListPart));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.colNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPOL = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPlaceOfDelivery = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colZipCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCurrency = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colRate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.txtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colFUEL = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotal = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rSpinEditTotal = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDurationFrom = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDurationTo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRemark = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colShare = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCarrier = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCommodity = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTransportClause = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rcmbTransportClause = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colShipline = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbShipline = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUpdateTime = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rSpinEditcolRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rSpinEditFUEL = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.txtRate1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbTransportClause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditcolRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditFUEL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.UI.InquireRates.ClientInquierTruckingRate);
            // 
            // treeMain
            // 
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colState,
            this.colNo,
            this.colPOL,
            this.colPlaceOfDelivery,
            this.colZipCode,
            this.colCurrency,
            this.colRate,
            this.colFUEL,
            this.colTotal,
            this.colDurationFrom,
            this.colDurationTo,
            this.colRemark,
            this.colShare,
            this.colCarrier,
            this.colCommodity,
            this.colTransportClause,
            this.colShipline,
            this.colUpdateTime});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.IndicatorWidth = 35;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsSelection.MultiSelect = true;
            this.treeMain.OptionsView.AutoWidth = false;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.ParentFieldName = "MainRecordID";
            this.treeMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox2,
            this.cmbCurrency,
            this.rcmbTransportClause,
            this.repositoryItemMemoExEdit1,
            this.rSpinEditRate,
            this.rSpinEditcolRate,
            this.rSpinEditFUEL,
            this.rSpinEditTotal,
            this.cmbShipline,
            this.txtRate1,
            this.txtRate});
            this.treeMain.RootValue = null;
            this.treeMain.RowHeight = 30;
            this.treeMain.Size = new System.Drawing.Size(754, 441);
            this.treeMain.TabIndex = 1;
            // 
            // colState
            // 
            this.colState.Caption = " ";
            this.colState.ColumnEdit = this.repositoryItemImageComboBox2;
            this.colState.FieldName = "HasUnRead";
            this.colState.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colState.MinWidth = 40;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.OptionsColumn.AllowSize = false;
            this.colState.OptionsColumn.ReadOnly = true;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            this.colState.Width = 40;
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            this.repositoryItemImageComboBox2.SmallImages = this.imageList2;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "unRead.png");
            // 
            // colNo
            // 
            this.colNo.Caption = "No";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowEdit = false;
            this.colNo.OptionsColumn.ReadOnly = true;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 3;
            // 
            // colPOL
            // 
            this.colPOL.Caption = "From";
            this.colPOL.FieldName = "POLName";
            this.colPOL.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colPOL.Name = "colPOL";
            this.colPOL.OptionsColumn.AllowEdit = false;
            this.colPOL.OptionsColumn.ReadOnly = true;
            this.colPOL.Visible = true;
            this.colPOL.VisibleIndex = 1;
            // 
            // colPlaceOfDelivery
            // 
            this.colPlaceOfDelivery.Caption = "To";
            this.colPlaceOfDelivery.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDelivery.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colPlaceOfDelivery.Name = "colPlaceOfDelivery";
            this.colPlaceOfDelivery.OptionsColumn.AllowEdit = false;
            this.colPlaceOfDelivery.OptionsColumn.ReadOnly = true;
            this.colPlaceOfDelivery.Visible = true;
            this.colPlaceOfDelivery.VisibleIndex = 2;
            // 
            // colZipCode
            // 
            this.colZipCode.Caption = "Zip Code";
            this.colZipCode.FieldName = "ZipCode";
            this.colZipCode.Name = "colZipCode";
            this.colZipCode.OptionsColumn.AllowEdit = false;
            this.colZipCode.OptionsColumn.ReadOnly = true;
            this.colZipCode.Visible = true;
            this.colZipCode.VisibleIndex = 4;
            this.colZipCode.Width = 60;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "Currency";
            this.colCurrency.ColumnEdit = this.cmbCurrency;
            this.colCurrency.FieldName = "CurrencyID";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.OptionsColumn.AllowEdit = false;
            this.colCurrency.OptionsColumn.ReadOnly = true;
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 5;
            this.colCurrency.Width = 60;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoHeight = false;
            this.cmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // colRate
            // 
            this.colRate.Caption = "Rate";
            this.colRate.ColumnEdit = this.txtRate;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.OptionsColumn.AllowEdit = false;
            this.colRate.OptionsColumn.ReadOnly = true;
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 6;
            // 
            // txtRate
            // 
            this.txtRate.AutoHeight = false;
            this.txtRate.DisplayFormat.FormatString = "F2";
            this.txtRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRate.EditFormat.FormatString = "F2";
            this.txtRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRate.Mask.EditMask = "F2";
            this.txtRate.Name = "txtRate";
            // 
            // colFUEL
            // 
            this.colFUEL.Caption = "FUEL";
            this.colFUEL.ColumnEdit = this.txtRate;
            this.colFUEL.FieldName = "FUEL";
            this.colFUEL.Name = "colFUEL";
            this.colFUEL.OptionsColumn.AllowEdit = false;
            this.colFUEL.OptionsColumn.ReadOnly = true;
            this.colFUEL.Visible = true;
            this.colFUEL.VisibleIndex = 7;
            // 
            // colTotal
            // 
            this.colTotal.Caption = "Total";
            this.colTotal.ColumnEdit = this.rSpinEditTotal;
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.AllowEdit = false;
            this.colTotal.OptionsColumn.ReadOnly = true;
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 8;
            // 
            // rSpinEditTotal
            // 
            this.rSpinEditTotal.Name = "rSpinEditTotal";
            // 
            // colDurationFrom
            // 
            this.colDurationFrom.Caption = "(Duration)From";
            this.colDurationFrom.FieldName = "DurationFrom";
            this.colDurationFrom.Name = "colDurationFrom";
            this.colDurationFrom.OptionsColumn.AllowEdit = false;
            this.colDurationFrom.OptionsColumn.ReadOnly = true;
            this.colDurationFrom.Visible = true;
            this.colDurationFrom.VisibleIndex = 9;
            // 
            // colDurationTo
            // 
            this.colDurationTo.Caption = "(Duration)To";
            this.colDurationTo.FieldName = "DurationTo";
            this.colDurationTo.Name = "colDurationTo";
            this.colDurationTo.OptionsColumn.AllowEdit = false;
            this.colDurationTo.OptionsColumn.ReadOnly = true;
            this.colDurationTo.Visible = true;
            this.colDurationTo.VisibleIndex = 10;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.OptionsColumn.AllowEdit = false;
            this.colRemark.OptionsColumn.ReadOnly = true;
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 11;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // colShare
            // 
            this.colShare.Caption = "Share";
            this.colShare.FieldName = "Shared";
            this.colShare.Name = "colShare";
            this.colShare.OptionsColumn.AllowEdit = false;
            this.colShare.OptionsColumn.ReadOnly = true;
            this.colShare.Visible = true;
            this.colShare.VisibleIndex = 12;
            // 
            // colCarrier
            // 
            this.colCarrier.Caption = "Carrier";
            this.colCarrier.FieldName = "CarrierName";
            this.colCarrier.MinWidth = 120;
            this.colCarrier.Name = "colCarrier";
            this.colCarrier.OptionsColumn.AllowEdit = false;
            this.colCarrier.OptionsColumn.AllowMove = false;
            this.colCarrier.OptionsColumn.AllowSize = false;
            this.colCarrier.OptionsColumn.ReadOnly = true;
            this.colCarrier.Visible = true;
            this.colCarrier.VisibleIndex = 13;
            this.colCarrier.Width = 120;
            // 
            // colCommodity
            // 
            this.colCommodity.Caption = "Commodity";
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.OptionsColumn.AllowEdit = false;
            this.colCommodity.OptionsColumn.ReadOnly = true;
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 14;
            // 
            // colTransportClause
            // 
            this.colTransportClause.Caption = "Term";
            this.colTransportClause.ColumnEdit = this.rcmbTransportClause;
            this.colTransportClause.FieldName = "TransportClauseID";
            this.colTransportClause.Name = "colTransportClause";
            this.colTransportClause.OptionsColumn.AllowEdit = false;
            this.colTransportClause.OptionsColumn.ReadOnly = true;
            this.colTransportClause.Visible = true;
            this.colTransportClause.VisibleIndex = 15;
            // 
            // rcmbTransportClause
            // 
            this.rcmbTransportClause.AutoHeight = false;
            this.rcmbTransportClause.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbTransportClause.Name = "rcmbTransportClause";
            // 
            // colShipline
            // 
            this.colShipline.Caption = "Shipline";
            this.colShipline.ColumnEdit = this.cmbShipline;
            this.colShipline.FieldName = "ShippingLineID";
            this.colShipline.Name = "colShipline";
            this.colShipline.OptionsColumn.AllowEdit = false;
            this.colShipline.OptionsColumn.ReadOnly = true;
            this.colShipline.Visible = true;
            this.colShipline.VisibleIndex = 16;
            // 
            // cmbShipline
            // 
            this.cmbShipline.AutoHeight = false;
            this.cmbShipline.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShipline.Name = "cmbShipline";
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.Caption = "Update Time";
            this.colUpdateTime.FieldName = "BizUpdateTime";
            this.colUpdateTime.Name = "colUpdateTime";
            this.colUpdateTime.OptionsColumn.AllowEdit = false;
            this.colUpdateTime.OptionsColumn.ReadOnly = true;
            this.colUpdateTime.Visible = true;
            this.colUpdateTime.VisibleIndex = 17;
            // 
            // rSpinEditRate
            // 
            this.rSpinEditRate.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.rSpinEditRate.AutoHeight = false;
            this.rSpinEditRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEditRate.Mask.EditMask = "F2";
            this.rSpinEditRate.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.rSpinEditRate.Name = "rSpinEditRate";
            // 
            // rSpinEditcolRate
            // 
            this.rSpinEditcolRate.Name = "rSpinEditcolRate";
            // 
            // rSpinEditFUEL
            // 
            this.rSpinEditFUEL.Name = "rSpinEditFUEL";
            // 
            // txtRate1
            // 
            this.txtRate1.AutoHeight = false;
            this.txtRate1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtRate1.DisplayFormat.FormatString = "F2";
            this.txtRate1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRate1.EditFormat.FormatString = "F2";
            this.txtRate1.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRate1.Mask.EditMask = "F2";
            this.txtRate1.Name = "txtRate1";
            // 
            // InquireTruckingRatesHistoryListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.IsMultiLanguage = false;
            this.Name = "InquireTruckingRatesHistoryListPart";
            this.Size = new System.Drawing.Size(754, 441);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbTransportClause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditcolRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditFUEL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate1)).EndInit();
            this.ResumeLayout(false);

        }      

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCarrier;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPOL;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colZipCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPlaceOfDelivery;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCurrency;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCommodity;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTransportClause;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFUEL;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotal;   
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDurationFrom;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDurationTo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRemark;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShare;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShipline;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUpdateTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbTransportClause;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEditRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbShipline;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEditcolRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEditFUEL;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEditTotal;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private ImageList imageList2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit txtRate1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtRate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNo;
    }
}

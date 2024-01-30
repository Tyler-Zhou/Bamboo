using System.Windows.Forms;
namespace ICP.FRM.UI.InquireRates
{
    partial class InquireAirRatesListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InquireAirRatesListPart));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.colCarrier = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPOL = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPlaceOfDelivery = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCurrency = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colRate_MIN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.txtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colRate_45 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRate_100 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRate_300 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRate_500 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRate_800 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRate_1000 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRate_1300 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCommodity = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rbtnEditComm = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colSchedule = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRouting = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDurationFrom = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDurationTo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRemark = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colShare = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colShipline = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbShipline = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUpdateTime = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnEditComm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.UI.InquireRates.ClientInquierAirRate);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // treeMain
            // 
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colState,
            this.colCarrier,
            this.colPOL,
            this.colPlaceOfDelivery,
            this.colCurrency,
            this.colRate_MIN,
            this.colRate_45,
            this.colRate_100,
            this.colRate_300,
            this.colRate_500,
            this.colRate_800,
            this.colRate_1000,
            this.colRate_1300,
            this.colCommodity,
            this.colSchedule,
            this.colRouting,
            this.colDurationFrom,
            this.colDurationTo,
            this.colRemark,
            this.colShare,
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
            this.rbtnEditComm,
            this.repositoryItemMemoExEdit1,
            this.rSpinEditRate,
            this.cmbShipline,
            this.txtRate});
            this.treeMain.RootValue = null;
            this.treeMain.RowHeight = 30;
            this.treeMain.Size = new System.Drawing.Size(754, 441);
            this.treeMain.TabIndex = 1;
            this.treeMain.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(this.treeMain_CustomDrawNodeIndicator);
            this.treeMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeMain_NodeCellStyle);
            this.treeMain.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.treeMain_BeforeFocusNode);
            this.treeMain.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.treeMain_ShowingEditor);
            // 
            // colState
            // 
            this.colState.Caption = " ";
            this.colState.ColumnEdit = this.repositoryItemImageComboBox2;
            this.colState.FieldName = "HasUnRead";
            this.colState.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colState.MinWidth = 80;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.OptionsColumn.AllowSize = false;
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
            // colCarrier
            // 
            this.colCarrier.Caption = "Carrier";
            this.colCarrier.FieldName = "CarrierName";
            this.colCarrier.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colCarrier.MinWidth = 120;
            this.colCarrier.Name = "colCarrier";
            this.colCarrier.OptionsColumn.AllowMove = false;
            this.colCarrier.OptionsColumn.AllowSize = false;
            this.colCarrier.Visible = true;
            this.colCarrier.VisibleIndex = 1;
            this.colCarrier.Width = 120;
            // 
            // colPOL
            // 
            this.colPOL.Caption = "From(Airport)";
            this.colPOL.FieldName = "POLName";
            this.colPOL.Name = "colPOL";
            this.colPOL.OptionsColumn.AllowEdit = false;
            this.colPOL.Visible = true;
            this.colPOL.VisibleIndex = 2;
            // 
            // colPlaceOfDelivery
            // 
            this.colPlaceOfDelivery.Caption = "To(Airport)";
            this.colPlaceOfDelivery.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDelivery.Name = "colPlaceOfDelivery";
            this.colPlaceOfDelivery.OptionsColumn.AllowEdit = false;
            this.colPlaceOfDelivery.Visible = true;
            this.colPlaceOfDelivery.VisibleIndex = 3;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "Currency";
            this.colCurrency.ColumnEdit = this.cmbCurrency;
            this.colCurrency.FieldName = "CurrencyID";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 4;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoHeight = false;
            this.cmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // colRate_MIN
            // 
            this.colRate_MIN.Caption = "Min";
            this.colRate_MIN.ColumnEdit = this.txtRate;
            this.colRate_MIN.FieldName = "Rate_MIN";
            this.colRate_MIN.Name = "colRate_MIN";
            // 
            // txtRate
            // 
            this.txtRate.AutoHeight = false;
            this.txtRate.DisplayFormat.FormatString = "F2";
            this.txtRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRate.EditFormat.FormatString = "F2";
            this.txtRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRate.Mask.EditMask = "F2";
            this.txtRate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRate.Name = "txtRate";
            // 
            // colRate_45
            // 
            this.colRate_45.Caption = "+45";
            this.colRate_45.ColumnEdit = this.txtRate;
            this.colRate_45.FieldName = "Rate_45";
            this.colRate_45.Name = "colRate_45";
            // 
            // colRate_100
            // 
            this.colRate_100.Caption = "+100";
            this.colRate_100.ColumnEdit = this.txtRate;
            this.colRate_100.FieldName = "Rate_100";
            this.colRate_100.Name = "colRate_100";
            // 
            // colRate_300
            // 
            this.colRate_300.Caption = "+300";
            this.colRate_300.ColumnEdit = this.txtRate;
            this.colRate_300.FieldName = "Rate_300";
            this.colRate_300.Name = "colRate_300";
            // 
            // colRate_500
            // 
            this.colRate_500.Caption = "+500";
            this.colRate_500.ColumnEdit = this.txtRate;
            this.colRate_500.FieldName = "Rate_500";
            this.colRate_500.Name = "colRate_500";
            // 
            // colRate_800
            // 
            this.colRate_800.Caption = "+800";
            this.colRate_800.ColumnEdit = this.txtRate;
            this.colRate_800.FieldName = "Rate_800";
            this.colRate_800.Name = "colRate_800";
            // 
            // colRate_1000
            // 
            this.colRate_1000.Caption = "+1000";
            this.colRate_1000.ColumnEdit = this.txtRate;
            this.colRate_1000.FieldName = "Rate_1000";
            this.colRate_1000.Name = "colRate_1000";
            // 
            // colRate_1300
            // 
            this.colRate_1300.Caption = "+1300";
            this.colRate_1300.ColumnEdit = this.txtRate;
            this.colRate_1300.FieldName = "Rate_1300";
            this.colRate_1300.Name = "colRate_1300";
            // 
            // colCommodity
            // 
            this.colCommodity.Caption = "Commodity";
            this.colCommodity.ColumnEdit = this.rbtnEditComm;
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 5;
            // 
            // rbtnEditComm
            // 
            this.rbtnEditComm.AutoHeight = false;
            this.rbtnEditComm.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rbtnEditComm.Name = "rbtnEditComm";
            this.rbtnEditComm.Leave += new System.EventHandler(this.rbtnEditComm_Leave);
            this.rbtnEditComm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtnEditComm_KeyDown);
            this.rbtnEditComm.Enter += new System.EventHandler(this.rbtnEditComm_Enter);
            this.rbtnEditComm.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rbtnEditComm_ButtonClick);
            // 
            // colSchedule
            // 
            this.colSchedule.Caption = "Schedule";
            this.colSchedule.FieldName = "Schedule";
            this.colSchedule.Name = "colSchedule";
            this.colSchedule.Visible = true;
            this.colSchedule.VisibleIndex = 6;
            // 
            // colRouting
            // 
            this.colRouting.Caption = "Routing";
            this.colRouting.FieldName = "Routing";
            this.colRouting.Name = "colRouting";
            this.colRouting.Visible = true;
            this.colRouting.VisibleIndex = 7;
            // 
            // colDurationFrom
            // 
            this.colDurationFrom.Caption = "(Duration)From";
            this.colDurationFrom.FieldName = "DurationFrom";
            this.colDurationFrom.Name = "colDurationFrom";
            this.colDurationFrom.Visible = true;
            this.colDurationFrom.VisibleIndex = 8;
            // 
            // colDurationTo
            // 
            this.colDurationTo.Caption = "(Duration)To";
            this.colDurationTo.FieldName = "DurationTo";
            this.colDurationTo.Name = "colDurationTo";
            this.colDurationTo.Visible = true;
            this.colDurationTo.VisibleIndex = 9;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 10;
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
            this.colShare.Visible = true;
            this.colShare.VisibleIndex = 11;
            // 
            // colShipline
            // 
            this.colShipline.Caption = "Shipline";
            this.colShipline.ColumnEdit = this.cmbShipline;
            this.colShipline.FieldName = "ShippingLineID";
            this.colShipline.Name = "colShipline";
            this.colShipline.Visible = true;
            this.colShipline.VisibleIndex = 12;
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
            this.colUpdateTime.Visible = true;
            this.colUpdateTime.VisibleIndex = 13;
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
            // InquireAirRatesListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.IsMultiLanguage = false;
            this.Name = "InquireAirRatesListPart";
            this.Size = new System.Drawing.Size(754, 441);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnEditComm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCarrier;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPOL;
        //private DevExpress.XtraTreeList.Columns.TreeListColumn colPOD;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPlaceOfDelivery;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCurrency;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_MIN;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_45;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_100;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_300;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_500;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_800;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_1000;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate_1300;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCommodity;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSchedule;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRouting; 
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDurationFrom;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDurationTo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRemark;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShare;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShipline;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUpdateTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnEditComm;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEditRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbShipline;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private ImageList imageList2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtRate;
    }
}

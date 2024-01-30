namespace ICP.FRM.UI.OceanPrice
{
    partial class OceanRateExportToExcel
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colCarrier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelivery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommodity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTerm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSurCharge = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCLS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDurationFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDurationTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20GP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colRate_40GP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40HQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45HQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20NOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40NOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20HQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20HT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20OT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20FR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20RF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20RH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_20TK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40FR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40HT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40OT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40RF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40RH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_40TK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45FR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45GP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45HT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45OT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45RF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45RH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_45TK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate_53HQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colFinalDestinationName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.OceanRateDataObject);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rDateEdit1,
            this.rSpinEditRate});
            this.gcMain.Size = new System.Drawing.Size(763, 403);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCarrier,
            this.colPOL,
            this.colVIA,
            this.colPOD,
            this.colDelivery,
            this.colFinalDestinationName,
            this.colItemCode,
            this.colCommodity,
            this.colTerm,
            this.colSurCharge,
            this.colCLS,
            this.colDurationFrom,
            this.colDurationTo,
            this.colDescription,
            this.colRate_20GP,
            this.colRate_40GP,
            this.colRate_40HQ,
            this.colRate_45HQ,
            this.colRate_20NOR,
            this.colRate_40NOR,
            this.colRate_20HQ,
            this.colRate_20HT,
            this.colRate_20OT,
            this.colRate_20FR,
            this.colRate_20RF,
            this.colRate_20RH,
            this.colRate_20TK,
            this.colRate_40FR,
            this.colRate_40HT,
            this.colRate_40OT,
            this.colRate_40RF,
            this.colRate_40RH,
            this.colRate_40TK,
            this.colRate_45FR,
            this.colRate_45GP,
            this.colRate_45HT,
            this.colRate_45OT,
            this.colRate_45RF,
            this.colRate_45RH,
            this.colRate_45TK,
            this.colRate_53HQ});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 27;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colCarrier
            // 
            this.colCarrier.Caption = "Carrier";
            this.colCarrier.FieldName = "CarrierName";
            this.colCarrier.Name = "colCarrier";
            this.colCarrier.OptionsColumn.AllowEdit = false;
            this.colCarrier.Visible = true;
            this.colCarrier.VisibleIndex = 0;
            this.colCarrier.Width = 120;
            // 
            // colPOL
            // 
            this.colPOL.Caption = "POL";
            this.colPOL.FieldName = "POLName";
            this.colPOL.Name = "colPOL";
            this.colPOL.OptionsColumn.AllowEdit = false;
            this.colPOL.Visible = true;
            this.colPOL.VisibleIndex = 1;
            this.colPOL.Width = 120;
            // 
            // colVIA
            // 
            this.colVIA.Caption = "VIA";
            this.colVIA.FieldName = "VIAName";
            this.colVIA.Name = "colVIA";
            this.colVIA.OptionsColumn.AllowEdit = false;
            this.colVIA.Visible = true;
            this.colVIA.VisibleIndex = 2;
            this.colVIA.Width = 120;
            // 
            // colPOD
            // 
            this.colPOD.Caption = "POD";
            this.colPOD.FieldName = "PODName";
            this.colPOD.Name = "colPOD";
            this.colPOD.OptionsColumn.AllowEdit = false;
            this.colPOD.Visible = true;
            this.colPOD.VisibleIndex = 3;
            this.colPOD.Width = 120;
            // 
            // colDelivery
            // 
            this.colDelivery.Caption = "Delivery";
            this.colDelivery.FieldName = "DeliveryName";
            this.colDelivery.Name = "colDelivery";
            this.colDelivery.OptionsColumn.AllowEdit = false;
            this.colDelivery.Visible = true;
            this.colDelivery.VisibleIndex = 4;
            this.colDelivery.Width = 120;
            // 
            // colItemCode
            // 
            this.colItemCode.Caption = "ItemCode";
            this.colItemCode.FieldName = "ItemCode";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.OptionsColumn.AllowEdit = false;
            this.colItemCode.Visible = true;
            this.colItemCode.VisibleIndex = 6;
            this.colItemCode.Width = 120;
            // 
            // colCommodity
            // 
            this.colCommodity.Caption = "Commodity";
            this.colCommodity.FieldName = "Comm";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.OptionsColumn.AllowEdit = false;
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 7;
            this.colCommodity.Width = 120;
            // 
            // colTerm
            // 
            this.colTerm.Caption = "Term";
            this.colTerm.FieldName = "Term";
            this.colTerm.Name = "colTerm";
            this.colTerm.OptionsColumn.AllowEdit = false;
            this.colTerm.Visible = true;
            this.colTerm.VisibleIndex = 8;
            this.colTerm.Width = 120;
            // 
            // colSurCharge
            // 
            this.colSurCharge.Caption = "SurCharge";
            this.colSurCharge.FieldName = "SurCharge";
            this.colSurCharge.Name = "colSurCharge";
            this.colSurCharge.OptionsColumn.AllowEdit = false;
            this.colSurCharge.Visible = true;
            this.colSurCharge.VisibleIndex = 9;
            this.colSurCharge.Width = 120;
            // 
            // colCLS
            // 
            this.colCLS.Caption = "CLS";
            this.colCLS.FieldName = "CLS";
            this.colCLS.Name = "colCLS";
            this.colCLS.OptionsColumn.AllowEdit = false;
            this.colCLS.Visible = true;
            this.colCLS.VisibleIndex = 10;
            this.colCLS.Width = 120;
            // 
            // colDurationFrom
            // 
            this.colDurationFrom.Caption = "Duration(From)";
            this.colDurationFrom.FieldName = "DurationForm";
            this.colDurationFrom.Name = "colDurationFrom";
            this.colDurationFrom.Visible = true;
            this.colDurationFrom.VisibleIndex = 11;
            this.colDurationFrom.Width = 150;
            // 
            // colDurationTo
            // 
            this.colDurationTo.Caption = "Duration(To)";
            this.colDurationTo.FieldName = "DurationTo";
            this.colDurationTo.Name = "colDurationTo";
            this.colDurationTo.Visible = true;
            this.colDurationTo.VisibleIndex = 12;
            this.colDurationTo.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 13;
            this.colDescription.Width = 150;
            // 
            // colRate_20GP
            // 
            this.colRate_20GP.Caption = "20GP";
            this.colRate_20GP.ColumnEdit = this.rSpinEditRate;
            this.colRate_20GP.FieldName = "Rate_20GP";
            this.colRate_20GP.Name = "colRate_20GP";
            // 
            // rSpinEditRate
            // 
            this.rSpinEditRate.AutoHeight = false;
            this.rSpinEditRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEditRate.Mask.EditMask = "F2";
            this.rSpinEditRate.Name = "rSpinEditRate";
            // 
            // colRate_40GP
            // 
            this.colRate_40GP.Caption = "40GP";
            this.colRate_40GP.ColumnEdit = this.rSpinEditRate;
            this.colRate_40GP.FieldName = "Rate_40GP";
            this.colRate_40GP.Name = "colRate_40GP";
            // 
            // colRate_40HQ
            // 
            this.colRate_40HQ.Caption = "40HQ";
            this.colRate_40HQ.ColumnEdit = this.rSpinEditRate;
            this.colRate_40HQ.FieldName = "Rate_40HQ";
            this.colRate_40HQ.Name = "colRate_40HQ";
            // 
            // colRate_45HQ
            // 
            this.colRate_45HQ.Caption = "45HQ";
            this.colRate_45HQ.ColumnEdit = this.rSpinEditRate;
            this.colRate_45HQ.FieldName = "Rate_45HQ";
            this.colRate_45HQ.Name = "colRate_45HQ";
            // 
            // colRate_20NOR
            // 
            this.colRate_20NOR.Caption = "20NOR";
            this.colRate_20NOR.ColumnEdit = this.rSpinEditRate;
            this.colRate_20NOR.FieldName = "Rate_20NOR";
            this.colRate_20NOR.Name = "colRate_20NOR";
            // 
            // colRate_40NOR
            // 
            this.colRate_40NOR.Caption = "40NOR";
            this.colRate_40NOR.ColumnEdit = this.rSpinEditRate;
            this.colRate_40NOR.FieldName = "Rate_40NOR";
            this.colRate_40NOR.Name = "colRate_40NOR";
            // 
            // colRate_20HQ
            // 
            this.colRate_20HQ.Caption = "20HQ";
            this.colRate_20HQ.ColumnEdit = this.rSpinEditRate;
            this.colRate_20HQ.FieldName = "Rate_20HQ";
            this.colRate_20HQ.Name = "colRate_20HQ";
            // 
            // colRate_20HT
            // 
            this.colRate_20HT.Caption = "20HT";
            this.colRate_20HT.ColumnEdit = this.rSpinEditRate;
            this.colRate_20HT.FieldName = "Rate_20HT";
            this.colRate_20HT.Name = "colRate_20HT";
            // 
            // colRate_20OT
            // 
            this.colRate_20OT.Caption = "20OT";
            this.colRate_20OT.ColumnEdit = this.rSpinEditRate;
            this.colRate_20OT.FieldName = "Rate_20OT";
            this.colRate_20OT.Name = "colRate_20OT";
            // 
            // colRate_20FR
            // 
            this.colRate_20FR.Caption = "20FR";
            this.colRate_20FR.ColumnEdit = this.rSpinEditRate;
            this.colRate_20FR.FieldName = "Rate_20FR";
            this.colRate_20FR.Name = "colRate_20FR";
            // 
            // colRate_20RF
            // 
            this.colRate_20RF.Caption = "20RF";
            this.colRate_20RF.ColumnEdit = this.rSpinEditRate;
            this.colRate_20RF.FieldName = "Rate_20RF";
            this.colRate_20RF.Name = "colRate_20RF";
            // 
            // colRate_20RH
            // 
            this.colRate_20RH.Caption = "20RH";
            this.colRate_20RH.ColumnEdit = this.rSpinEditRate;
            this.colRate_20RH.FieldName = "Rate_20RH";
            this.colRate_20RH.Name = "colRate_20RH";
            // 
            // colRate_20TK
            // 
            this.colRate_20TK.Caption = "20TK";
            this.colRate_20TK.ColumnEdit = this.rSpinEditRate;
            this.colRate_20TK.FieldName = "Rate_20TK";
            this.colRate_20TK.Name = "colRate_20TK";
            // 
            // colRate_40FR
            // 
            this.colRate_40FR.Caption = "40FR";
            this.colRate_40FR.ColumnEdit = this.rSpinEditRate;
            this.colRate_40FR.FieldName = "Rate_40FR";
            this.colRate_40FR.Name = "colRate_40FR";
            // 
            // colRate_40HT
            // 
            this.colRate_40HT.Caption = "40HT";
            this.colRate_40HT.ColumnEdit = this.rSpinEditRate;
            this.colRate_40HT.FieldName = "Rate_40HT";
            this.colRate_40HT.Name = "colRate_40HT";
            // 
            // colRate_40OT
            // 
            this.colRate_40OT.Caption = "40OT";
            this.colRate_40OT.ColumnEdit = this.rSpinEditRate;
            this.colRate_40OT.FieldName = "Rate_40OT";
            this.colRate_40OT.Name = "colRate_40OT";
            // 
            // colRate_40RF
            // 
            this.colRate_40RF.Caption = "40RF";
            this.colRate_40RF.ColumnEdit = this.rSpinEditRate;
            this.colRate_40RF.FieldName = "Rate_40RF";
            this.colRate_40RF.Name = "colRate_40RF";
            // 
            // colRate_40RH
            // 
            this.colRate_40RH.Caption = "40RH";
            this.colRate_40RH.ColumnEdit = this.rSpinEditRate;
            this.colRate_40RH.FieldName = "Rate_40RH";
            this.colRate_40RH.Name = "colRate_40RH";
            // 
            // colRate_40TK
            // 
            this.colRate_40TK.Caption = "40TK";
            this.colRate_40TK.ColumnEdit = this.rSpinEditRate;
            this.colRate_40TK.FieldName = "Rate_40TK";
            this.colRate_40TK.Name = "colRate_40TK";
            // 
            // colRate_45FR
            // 
            this.colRate_45FR.Caption = "45FR";
            this.colRate_45FR.ColumnEdit = this.rSpinEditRate;
            this.colRate_45FR.FieldName = "Rate_45FR";
            this.colRate_45FR.Name = "colRate_45FR";
            // 
            // colRate_45GP
            // 
            this.colRate_45GP.Caption = "45GP";
            this.colRate_45GP.ColumnEdit = this.rSpinEditRate;
            this.colRate_45GP.FieldName = "Rate_45GP";
            this.colRate_45GP.Name = "colRate_45GP";
            // 
            // colRate_45HT
            // 
            this.colRate_45HT.Caption = "45HT";
            this.colRate_45HT.ColumnEdit = this.rSpinEditRate;
            this.colRate_45HT.FieldName = "Rate_45HT";
            this.colRate_45HT.Name = "colRate_45HT";
            // 
            // colRate_45OT
            // 
            this.colRate_45OT.Caption = "45OT";
            this.colRate_45OT.ColumnEdit = this.rSpinEditRate;
            this.colRate_45OT.FieldName = "Rate_45OT";
            this.colRate_45OT.Name = "colRate_45OT";
            // 
            // colRate_45RF
            // 
            this.colRate_45RF.Caption = "45RF";
            this.colRate_45RF.ColumnEdit = this.rSpinEditRate;
            this.colRate_45RF.FieldName = "Rate_45RF";
            this.colRate_45RF.Name = "colRate_45RF";
            // 
            // colRate_45RH
            // 
            this.colRate_45RH.Caption = "45RH";
            this.colRate_45RH.ColumnEdit = this.rSpinEditRate;
            this.colRate_45RH.FieldName = "Rate_45RH";
            this.colRate_45RH.Name = "colRate_45RH";
            // 
            // colRate_45TK
            // 
            this.colRate_45TK.Caption = "45TK";
            this.colRate_45TK.ColumnEdit = this.rSpinEditRate;
            this.colRate_45TK.FieldName = "Rate_45TK";
            this.colRate_45TK.Name = "colRate_45TK";
            // 
            // colRate_53HQ
            // 
            this.colRate_53HQ.Caption = "53HQ";
            this.colRate_53HQ.ColumnEdit = this.rSpinEditRate;
            this.colRate_53HQ.FieldName = "Rate_53HQ";
            this.colRate_53HQ.Name = "colRate_53HQ";
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
            // colFinalDestinationName
            // 
            this.colFinalDestinationName.Caption = "Final Destination";
            this.colFinalDestinationName.FieldName = "FinalDestinationName";
            this.colFinalDestinationName.Name = "colFinalDestinationName";
            this.colFinalDestinationName.Visible = true;
            this.colFinalDestinationName.VisibleIndex = 5;
            this.colFinalDestinationName.Width = 120;
            // 
            // OceanRateExportToExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.IsMultiLanguage = false;
            this.Name = "OceanRateExportToExcel";
            this.Size = new System.Drawing.Size(763, 403);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colPOL;
        private DevExpress.XtraGrid.Columns.GridColumn colPOD;
        private DevExpress.XtraGrid.Columns.GridColumn colDelivery;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20GP;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEditRate;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40GP;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40HQ;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45HQ;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20NOR;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40NOR;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20HQ;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20HT;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20OT;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20FR;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20RF;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20RH;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_20TK;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40FR;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40HT;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40OT;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40RF;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40RH;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_40TK;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45FR;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45GP;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45HT;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45OT;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45RF;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45RH;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_45TK;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rDateEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrier;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodity;
        private DevExpress.XtraGrid.Columns.GridColumn colTerm;
        private DevExpress.XtraGrid.Columns.GridColumn colSurCharge;
        private DevExpress.XtraGrid.Columns.GridColumn colCLS;
        private DevExpress.XtraGrid.Columns.GridColumn colDurationTo;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colRate_53HQ;
        private DevExpress.XtraGrid.Columns.GridColumn colVIA;
        private DevExpress.XtraGrid.Columns.GridColumn colItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDurationFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colFinalDestinationName;
    }
}

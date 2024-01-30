namespace ICP.FCM.AirExport.UI.BL
{
    partial class AEBLListPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colReleaseType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRefNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFilightNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colETA = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colShipper = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colConsignee = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNotifyParty = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAgentOfCarrierName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPKGS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colKGS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLBS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colChargeKGS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCharLBS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDeparture = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDetination = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colETD = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCustomerName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSales = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBookingerName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFiler = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIssueType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUpdateByName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUpdateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLD = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBLList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // treeMain
            // 
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colState,
            this.colNo,
            this.colReleaseType,
            this.colRefNo,
            this.colFilightNo,
            this.colETA,
            this.colShipper,
            this.colConsignee,
            this.colNotifyParty,
            this.colAgentOfCarrierName,
            this.colPKGS,
            this.colKGS,
            this.colLBS,
            this.colChargeKGS,
            this.colCharLBS,
            this.colDeparture,
            this.colDetination,
            this.colPlaceOfDeliveryName,
            this.colETD,
            this.colCustomerName,
            this.colSales,
            this.colBookingerName,
            this.colFiler,
            this.colRBLD,
            this.colIssueType,
            this.colCreateDate,
            this.colUpdateByName,
            this.colUpdateDate});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsSelection.MultiSelect = true;
            this.treeMain.OptionsView.AutoWidth = false;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.ParentFieldName = "MBLID";
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(754, 441);
            this.treeMain.TabIndex = 1;
            this.treeMain.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(this.treeMain_CustomDrawNodeIndicator);
            this.treeMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeMain_NodeCellStyle);
            this.treeMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeMain_KeyDown);
            this.treeMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeMain_MouseDoubleClick);
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.FieldName = "StateName";
            this.colState.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colState.MinWidth = 80;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowMove = false;
            this.colState.OptionsColumn.AllowSize = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            this.colState.Width = 80;
            // 
            // colNo
            // 
            this.colNo.FieldName = "No";
            this.colNo.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colNo.MinWidth = 120;
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowMove = false;
            this.colNo.OptionsColumn.AllowSize = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 120;
            // 
            // colReleaseType
            // 
            this.colReleaseType.Name = "colReleaseType";
            // 
            // colRefNo
            // 
            this.colRefNo.FieldName = "RefNo";
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.OptionsColumn.AllowSize = false;
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 2;
            this.colRefNo.Width = 128;
            // 
            // colFilightNo
            // 
            this.colFilightNo.Caption = "Filight No";
            this.colFilightNo.FieldName = "FilightNo";
            this.colFilightNo.Name = "colFilightNo";
            this.colFilightNo.Visible = true;
            this.colFilightNo.VisibleIndex = 3;
            // 
            // colETA
            // 
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.OptionsColumn.AllowEdit = false;
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 18;
            this.colETA.Width = 80;
            // 
            // colShipper
            // 
            this.colShipper.Caption = "Shipper";
            this.colShipper.FieldName = "ShipperName";
            this.colShipper.Name = "colShipper";
            this.colShipper.Visible = true;
            this.colShipper.VisibleIndex = 5;
            // 
            // colConsignee
            // 
            this.colConsignee.Caption = "Consignee";
            this.colConsignee.FieldName = "ConsigneeName";
            this.colConsignee.Name = "colConsignee";
            this.colConsignee.Visible = true;
            this.colConsignee.VisibleIndex = 6;
            // 
            // colNotifyParty
            // 
            this.colNotifyParty.Caption = "NotifyParty";
            this.colNotifyParty.FieldName = "NotifyPartyName";
            this.colNotifyParty.Name = "colNotifyParty";
            this.colNotifyParty.Visible = true;
            this.colNotifyParty.VisibleIndex = 7;
            // 
            // colAgentOfCarrierName
            // 
            this.colAgentOfCarrierName.Caption = "AgentOfCarrier";
            this.colAgentOfCarrierName.FieldName = "AgentOfCarrierName";
            this.colAgentOfCarrierName.Name = "colAgentOfCarrierName";
            this.colAgentOfCarrierName.Visible = true;
            this.colAgentOfCarrierName.VisibleIndex = 8;
            // 
            // colPKGS
            // 
            this.colPKGS.Caption = "PKGS";
            this.colPKGS.FieldName = "Quantity";
            this.colPKGS.Name = "colPKGS";
            this.colPKGS.Visible = true;
            this.colPKGS.VisibleIndex = 9;
            // 
            // colKGS
            // 
            this.colKGS.Caption = "KGS";
            this.colKGS.FieldName = "GrossKGS";
            this.colKGS.Name = "colKGS";
            this.colKGS.Visible = true;
            this.colKGS.VisibleIndex = 10;
            // 
            // colLBS
            // 
            this.colLBS.Caption = "LBS";
            this.colLBS.FieldName = "GrossLBS";
            this.colLBS.Name = "colLBS";
            this.colLBS.Visible = true;
            this.colLBS.VisibleIndex = 11;
            // 
            // colChargeKGS
            // 
            this.colChargeKGS.Caption = "ChargeKGS";
            this.colChargeKGS.FieldName = "ChargeKGS";
            this.colChargeKGS.Name = "colChargeKGS";
            this.colChargeKGS.Visible = true;
            this.colChargeKGS.VisibleIndex = 12;
            // 
            // colCharLBS
            // 
            this.colCharLBS.Caption = "CharLBS";
            this.colCharLBS.FieldName = "ChargeLBS";
            this.colCharLBS.Name = "colCharLBS";
            this.colCharLBS.Visible = true;
            this.colCharLBS.VisibleIndex = 13;
            // 
            // colDeparture
            // 
            this.colDeparture.Caption = "Departure";
            this.colDeparture.FieldName = "DepartureName";
            this.colDeparture.Name = "colDeparture";
            this.colDeparture.Visible = true;
            this.colDeparture.VisibleIndex = 14;
            this.colDeparture.Width = 100;
            // 
            // colDetination
            // 
            this.colDetination.Caption = "Detination";
            this.colDetination.FieldName = "DetinationName";
            this.colDetination.Name = "colDetination";
            this.colDetination.Visible = true;
            this.colDetination.VisibleIndex = 15;
            this.colDetination.Width = 100;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 16;
            this.colPlaceOfDeliveryName.Width = 100;
            // 
            // colETD
            // 
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.OptionsColumn.AllowEdit = false;
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 17;
            this.colETD.Width = 80;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 4;
            this.colCustomerName.Width = 120;
            // 
            // colSales
            // 
            this.colSales.Caption = "Sales";
            this.colSales.FieldName = "SalesName";
            this.colSales.Name = "colSales";
            this.colSales.Visible = true;
            this.colSales.VisibleIndex = 19;
            // 
            // colBookingerName
            // 
            this.colBookingerName.Caption = "Booking";
            this.colBookingerName.FieldName = "BookingerName";
            this.colBookingerName.Name = "colBookingerName";
            this.colBookingerName.Visible = true;
            this.colBookingerName.VisibleIndex = 20;
            // 
            // colFiler
            // 
            this.colFiler.Caption = "Filer";
            this.colFiler.FieldName = "FilerName";
            this.colFiler.Name = "colFiler";
            this.colFiler.Visible = true;
            this.colFiler.VisibleIndex = 21;
            // 
            // colIssueType
            // 
            this.colIssueType.Caption = "IssueType";
            this.colIssueType.FieldName = "IsClosed";
            this.colIssueType.Name = "colIssueType";
            this.colIssueType.Visible = true;
            this.colIssueType.VisibleIndex = 22;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 24;
            this.colCreateDate.Width = 80;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "UpdateByName";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 25;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.Caption = "UpdateDate";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 26;
            // 
            // colRBLD
            // 
            this.colRBLD.Caption = "RBLD";
            this.colRBLD.FieldName = "RBLD";
            this.colRBLD.Name = "colRBLD";
            this.colRBLD.Visible = true;
            this.colRBLD.VisibleIndex = 23;
            // 
            // AEBLListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Name = "AEBLListPart";
            this.Size = new System.Drawing.Size(754, 441);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRefNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCustomerName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFilightNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDeparture;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDetination;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPlaceOfDeliveryName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colETA;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colETD;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIssueType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSales;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBookingerName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFiler;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colReleaseType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShipper;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colConsignee;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNotifyParty;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAgentOfCarrierName;

        private DevExpress.XtraTreeList.Columns.TreeListColumn colPKGS;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colKGS;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLBS;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colChargeKGS;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCharLBS;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUpdateByName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUpdateDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLD;
    }
}

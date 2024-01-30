namespace ICP.FCM.OceanExport.UI.BL
{
    partial class OEBLListPart
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
            this.colCustomerName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRefNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colVesselVoyage = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colETA = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSoNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colContainerNos = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colShipper = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colConsignee = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNotifyParty = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAgentOfCarrierName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPOLName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPODName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colETD = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSales = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBookingerName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFiler = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colOverseasFiler = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIssueType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAgentName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLA = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLD = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLRcv = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBLRC = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colReleaseType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTelexNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colReleaseDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUpdateByName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUpdateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDispatchState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDispatchDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIsToAgent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBLList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // treeMain
            // 
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colState,
            this.colNo,
            this.colCustomerName,
            this.colRefNo,
            this.colVesselVoyage,
            this.colETA,
            this.colSoNo,
            this.colContainerNos,
            this.colShipper,
            this.colConsignee,
            this.colNotifyParty,
            this.colAgentOfCarrierName,
            this.colPOLName,
            this.colPODName,
            this.colPlaceOfDeliveryName,
            this.colETD,
            this.colSales,
            this.colBookingerName,
            this.colFiler,
            this.colOverseasFiler,
            this.colIssueType,
            this.colCreateDate,
            this.colAgentName,
            this.colRBLA,
            this.colRBLD,
            this.colRBLRcv,
            this.colBLRC,
            this.colReleaseType,
            this.colTelexNo,
            this.colReleaseDate,
            this.colUpdateByName,
            this.colUpdateDate,
            this.colDispatchState,
            this.colDispatchDate,
            this.colIsToAgent});
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
            this.treeMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeMain_NodeCellStyle);
            this.treeMain.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(this.treeMain_CustomDrawNodeIndicator);
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
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 2;
            this.colCustomerName.Width = 180;
            // 
            // colRefNo
            // 
            this.colRefNo.FieldName = "RefNo";
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.OptionsColumn.AllowSize = false;
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 3;
            this.colRefNo.Width = 128;
            // 
            // colVesselVoyage
            // 
            this.colVesselVoyage.FieldName = "VesselVoyage";
            this.colVesselVoyage.Name = "colVesselVoyage";
            this.colVesselVoyage.Visible = true;
            this.colVesselVoyage.VisibleIndex = 4;
            this.colVesselVoyage.Width = 160;
            // 
            // colETA
            // 
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.OptionsColumn.AllowEdit = false;
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 5;
            this.colETA.Width = 80;
            // 
            // colSoNo
            // 
            this.colSoNo.Caption = "So No";
            this.colSoNo.FieldName = "SONO";
            this.colSoNo.Name = "colSoNo";
            this.colSoNo.Visible = true;
            this.colSoNo.VisibleIndex = 6;
            this.colSoNo.Width = 110;
            // 
            // colContainerNos
            // 
            this.colContainerNos.FieldName = "ContainerNos";
            this.colContainerNos.Name = "colContainerNos";
            this.colContainerNos.Visible = true;
            this.colContainerNos.VisibleIndex = 7;
            this.colContainerNos.Width = 160;
            // 
            // colShipper
            // 
            this.colShipper.Caption = "Shipper";
            this.colShipper.FieldName = "ShipperName";
            this.colShipper.Name = "colShipper";
            this.colShipper.Visible = true;
            this.colShipper.VisibleIndex = 8;
            this.colShipper.Width = 160;
            // 
            // colConsignee
            // 
            this.colConsignee.Caption = "Consignee";
            this.colConsignee.FieldName = "ConsigneeName";
            this.colConsignee.Name = "colConsignee";
            this.colConsignee.Visible = true;
            this.colConsignee.VisibleIndex = 9;
            this.colConsignee.Width = 160;
            // 
            // colNotifyParty
            // 
            this.colNotifyParty.Caption = "NotifyParty";
            this.colNotifyParty.FieldName = "NotifyPartyName";
            this.colNotifyParty.Name = "colNotifyParty";
            this.colNotifyParty.Visible = true;
            this.colNotifyParty.VisibleIndex = 10;
            this.colNotifyParty.Width = 160;
            // 
            // colAgentOfCarrierName
            // 
            this.colAgentOfCarrierName.Caption = "AgentOfCarrier";
            this.colAgentOfCarrierName.FieldName = "AgentOfCarrierName";
            this.colAgentOfCarrierName.Name = "colAgentOfCarrierName";
            this.colAgentOfCarrierName.Visible = true;
            this.colAgentOfCarrierName.VisibleIndex = 11;
            this.colAgentOfCarrierName.Width = 160;
            // 
            // colPOLName
            // 
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 12;
            this.colPOLName.Width = 100;
            // 
            // colPODName
            // 
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 13;
            this.colPODName.Width = 100;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 14;
            this.colPlaceOfDeliveryName.Width = 100;
            // 
            // colETD
            // 
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.OptionsColumn.AllowEdit = false;
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 15;
            this.colETD.Width = 80;
            // 
            // colSales
            // 
            this.colSales.Caption = "Sales";
            this.colSales.FieldName = "SalesName";
            this.colSales.Name = "colSales";
            this.colSales.Visible = true;
            this.colSales.VisibleIndex = 16;
            // 
            // colBookingerName
            // 
            this.colBookingerName.Caption = "Booking";
            this.colBookingerName.FieldName = "BookingerName";
            this.colBookingerName.Name = "colBookingerName";
            this.colBookingerName.Visible = true;
            this.colBookingerName.VisibleIndex = 17;
            // 
            // colFiler
            // 
            this.colFiler.Caption = "Filer";
            this.colFiler.FieldName = "FilerName";
            this.colFiler.Name = "colFiler";
            this.colFiler.Visible = true;
            this.colFiler.VisibleIndex = 18;
            // 
            // colOverseasFiler
            // 
            this.colOverseasFiler.Caption = "Overseas Filer";
            this.colOverseasFiler.FieldName = "OverseasFilerName";
            this.colOverseasFiler.Name = "colOverseasFiler";
            this.colOverseasFiler.Visible = true;
            this.colOverseasFiler.VisibleIndex = 19;
            // 
            // colIssueType
            // 
            this.colIssueType.Caption = "IssueType";
            this.colIssueType.FieldName = "IssueTypeName";
            this.colIssueType.Name = "colIssueType";
            this.colIssueType.Visible = true;
            this.colIssueType.VisibleIndex = 20;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 21;
            this.colCreateDate.Width = 80;
            // 
            // colAgentName
            // 
            this.colAgentName.Caption = "Agent Name";
            this.colAgentName.FieldName = "AgentName";
            this.colAgentName.Name = "colAgentName";
            this.colAgentName.Visible = true;
            this.colAgentName.VisibleIndex = 22;
            this.colAgentName.Width = 160;
            // 
            // colRBLA
            // 
            this.colRBLA.Caption = "RBLA";
            this.colRBLA.FieldName = "RBLA";
            this.colRBLA.Name = "colRBLA";
            this.colRBLA.Visible = true;
            this.colRBLA.VisibleIndex = 23;
            // 
            // colRBLD
            // 
            this.colRBLD.Caption = "RBLD";
            this.colRBLD.FieldName = "RBLD";
            this.colRBLD.Name = "colRBLD";
            this.colRBLD.Visible = true;
            this.colRBLD.VisibleIndex = 24;
            // 
            // colRBLRcv
            // 
            this.colRBLRcv.Caption = "RBLRcv";
            this.colRBLRcv.FieldName = "RBLRcv";
            this.colRBLRcv.Name = "colRBLRcv";
            this.colRBLRcv.Visible = true;
            this.colRBLRcv.VisibleIndex = 25;
            // 
            // colBLRC
            // 
            this.colBLRC.Caption = "BLRC";
            this.colBLRC.FieldName = "BLRC";
            this.colBLRC.Name = "colBLRC";
            this.colBLRC.Visible = true;
            this.colBLRC.VisibleIndex = 26;
            // 
            // colReleaseType
            // 
            this.colReleaseType.FieldName = "ReleaseTypeName";
            this.colReleaseType.Name = "colReleaseType";
            this.colReleaseType.Visible = true;
            this.colReleaseType.VisibleIndex = 27;
            this.colReleaseType.Width = 80;
            // 
            // colTelexNo
            // 
            this.colTelexNo.Caption = "TelexNo";
            this.colTelexNo.FieldName = "TelexNo";
            this.colTelexNo.Name = "colTelexNo";
            this.colTelexNo.Visible = true;
            this.colTelexNo.VisibleIndex = 28;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.Caption = "ReleaseDate";
            this.colReleaseDate.FieldName = "ReleaseDate";
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.Visible = true;
            this.colReleaseDate.VisibleIndex = 29;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "UpdateByName";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 30;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.Caption = "UpdateDate";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 31;
            // 
            // colDispatchState
            // 
            this.colDispatchState.Caption = "Dispatch State";
            this.colDispatchState.FieldName = "DispatchState";
            this.colDispatchState.Name = "colDispatchState";
            this.colDispatchState.Visible = true;
            this.colDispatchState.VisibleIndex = 32;
            this.colDispatchState.Width = 90;
            // 
            // colDispatchDate
            // 
            this.colDispatchDate.Caption = "Dispatch Date";
            this.colDispatchDate.FieldName = "DispatchUpdateDate";
            this.colDispatchDate.Name = "colDispatchDate";
            this.colDispatchDate.Visible = true;
            this.colDispatchDate.VisibleIndex = 33;
            this.colDispatchDate.Width = 90;
            // 
            // colIsToAgent
            // 
            this.colIsToAgent.Caption = "IsToAgent";
            this.colIsToAgent.FieldName = "IsToAgent";
            this.colIsToAgent.Name = "colIsToAgent";
            this.colIsToAgent.Visible = true;
            this.colIsToAgent.VisibleIndex = 34;
            // 
            // OEBLListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Name = "OEBLListPart";
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
        private DevExpress.XtraTreeList.Columns.TreeListColumn colVesselVoyage;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCustomerName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colContainerNos;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSoNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPOLName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPODName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPlaceOfDeliveryName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colETA;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colETD;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIssueType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSales;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBookingerName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFiler;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOverseasFiler;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colReleaseType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShipper;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colConsignee;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNotifyParty;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAgentOfCarrierName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAgentName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUpdateByName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUpdateDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLA;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLD;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLRcv;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBLRC;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDispatchState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDispatchDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colReleaseDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTelexNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsToAgent;
    }
}

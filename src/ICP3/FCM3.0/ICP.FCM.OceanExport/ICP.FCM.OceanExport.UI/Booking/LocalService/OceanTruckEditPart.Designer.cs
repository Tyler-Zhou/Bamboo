﻿using System.Windows.Forms;
namespace ICP.FCM.OceanExport.UI.Booking
{
    partial class OceanTruckEditPart
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
            this.bsOceanTruckList = new System.Windows.Forms.BindingSource(this.components);
            this.bsContainers = new System.Windows.Forms.BindingSource(this.components);
            this.containersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bsTruckInfo = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTruckNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTruckerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoadingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryAtName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl2 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvContainer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCtnNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colSealNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbContainer = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colDriver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colArriveDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingOrderNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBase = new System.Windows.Forms.GroupBox();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.txtTotalPkgs = new DevExpress.XtraEditors.TextEdit();
            this.labTotalPkgs = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.btnPUEmptyCNTR = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.labEmptyCNTR = new DevExpress.XtraEditors.LabelControl();
            this.stxtBillToID = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dteCreateOn = new DevExpress.XtraEditors.DateEdit();
            this.dteLoadingTime = new DevExpress.XtraEditors.DateEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barNew = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labCreateOn = new DevExpress.XtraEditors.LabelControl();
            this.containerDemandControl1 = new ICP.Framework.ClientComponents.Controls.ContainerDemandControl();
            this.labContainerDemand = new DevExpress.XtraEditors.LabelControl();
            this.txtFreigtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labNO = new DevExpress.XtraEditors.LabelControl();
            this.labShippingOrderNo = new DevExpress.XtraEditors.LabelControl();
            this.stxtTrucker = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtCustomerBroker = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.labFreigtDescription = new DevExpress.XtraEditors.LabelControl();
            this.stxtDeliveryAt = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.txtDeliveryAtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labCustomsBroker = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.txtShipperDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labTrucker = new DevExpress.XtraEditors.LabelControl();
            this.chkIsDrivingLicence = new DevExpress.XtraEditors.CheckEdit();
            this.labShipper = new DevExpress.XtraEditors.LabelControl();
            this.labLoadingTime = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateByName = new DevExpress.XtraEditors.TextEdit();
            this.txtVesselVoyage = new DevExpress.XtraEditors.TextEdit();
            this.labDeliveryAt = new DevExpress.XtraEditors.LabelControl();
            this.txtCarrier = new DevExpress.XtraEditors.TextEdit();
            this.labVesselVoyage = new DevExpress.XtraEditors.LabelControl();
            this.txtNO = new DevExpress.XtraEditors.TextEdit();
            this.txtShippingOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.groupContainer = new System.Windows.Forms.GroupBox();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.dxErrorContainers = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelScroll = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.labDeliveryDate = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsOceanTruckList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.containersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            this.groupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalPkgs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPUEmptyCNTR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBillToID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateOn.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateOn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteLoadingTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteLoadingTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreigtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtTrucker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomerBroker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeliveryAt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryAtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDrivingLicence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateByName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            this.groupContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorContainers)).BeginInit();
            this.panelScroll.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsOceanTruckList
            // 
            this.bsOceanTruckList.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanTruckInfo);
            // 
            // bsContainers
            // 
            this.bsContainers.DataSource = this.containersBindingSource;
            // 
            // containersBindingSource
            // 
            this.containersBindingSource.DataMember = "Containers";
            this.containersBindingSource.DataSource = this.bsTruckInfo;
            // 
            // bsTruckInfo
            // 
            this.bsTruckInfo.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanTruckInfo);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsOceanTruckList;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvMain;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(800, 115);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTruckNO,
            this.colTruckerName,
            this.colShipperName,
            this.colLoadingTime,
            this.colDeliveryAtName});
            this.gvMain.GridControl = this.gridControl1;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colTruckNO
            // 
            this.colTruckNO.FieldName = "NO";
            this.colTruckNO.Name = "colTruckNO";
            this.colTruckNO.OptionsColumn.AllowEdit = false;
            this.colTruckNO.Visible = true;
            this.colTruckNO.VisibleIndex = 0;
            this.colTruckNO.Width = 120;
            // 
            // colTruckerName
            // 
            this.colTruckerName.Caption = "Trucker";
            this.colTruckerName.FieldName = "TruckerName";
            this.colTruckerName.Name = "colTruckerName";
            this.colTruckerName.OptionsColumn.AllowEdit = false;
            this.colTruckerName.Visible = true;
            this.colTruckerName.VisibleIndex = 1;
            this.colTruckerName.Width = 150;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "Shipper";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.OptionsColumn.AllowEdit = false;
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 2;
            this.colShipperName.Width = 150;
            // 
            // colLoadingTime
            // 
            this.colLoadingTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.colLoadingTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLoadingTime.FieldName = "LoadingTime";
            this.colLoadingTime.MinWidth = 50;
            this.colLoadingTime.Name = "colLoadingTime";
            this.colLoadingTime.OptionsColumn.AllowEdit = false;
            this.colLoadingTime.Visible = true;
            this.colLoadingTime.VisibleIndex = 3;
            this.colLoadingTime.Width = 120;
            // 
            // colDeliveryAtName
            // 
            this.colDeliveryAtName.Caption = "DeliveryAt";
            this.colDeliveryAtName.FieldName = "DeliveryAtName";
            this.colDeliveryAtName.Name = "colDeliveryAtName";
            this.colDeliveryAtName.OptionsColumn.AllowEdit = false;
            this.colDeliveryAtName.Visible = true;
            this.colDeliveryAtName.VisibleIndex = 4;
            this.colDeliveryAtName.Width = 239;
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.bsContainers;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(3, 44);
            this.gridControl2.MainView = this.gvContainer;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbContainer,
            this.repositoryItemTextEdit1,
            this.repositoryItemDateEdit1});
            this.gridControl2.Size = new System.Drawing.Size(794, 82);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvContainer});
            // 
            // gvContainer
            // 
            this.gvContainer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCtnNo,
            this.colSealNo,
            this.colTypeID,
            this.colDriver,
            this.colCarNo,
            this.colDeliveryDate,
            this.colArriveDate,
            this.colReturnDate,
            this.colShippingOrderNo});
            this.gvContainer.GridControl = this.gridControl2;
            this.gvContainer.Name = "gvContainer";
            this.gvContainer.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvContainer.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvContainer.OptionsSelection.MultiSelect = true;
            this.gvContainer.OptionsView.EnableAppearanceEvenRow = true;
            this.gvContainer.OptionsView.ShowGroupPanel = false;
            this.gvContainer.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvContainer_InitNewRow);
            this.gvContainer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvContainer_KeyDown);
            // 
            // colCtnNo
            // 
            this.colCtnNo.Caption = "No";
            this.colCtnNo.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCtnNo.FieldName = "No";
            this.colCtnNo.Name = "colCtnNo";
            this.colCtnNo.Visible = true;
            this.colCtnNo.VisibleIndex = 0;
            this.colCtnNo.Width = 80;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colSealNo
            // 
            this.colSealNo.Caption = "SealNo";
            this.colSealNo.FieldName = "SealNo";
            this.colSealNo.Name = "colSealNo";
            this.colSealNo.Visible = true;
            this.colSealNo.VisibleIndex = 1;
            this.colSealNo.Width = 70;
            // 
            // colTypeID
            // 
            this.colTypeID.Caption = "Type";
            this.colTypeID.ColumnEdit = this.rcmbContainer;
            this.colTypeID.FieldName = "TypeID";
            this.colTypeID.Name = "colTypeID";
            this.colTypeID.Visible = true;
            this.colTypeID.VisibleIndex = 2;
            this.colTypeID.Width = 80;
            // 
            // rcmbContainer
            // 
            this.rcmbContainer.AutoHeight = false;
            this.rcmbContainer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbContainer.Name = "rcmbContainer";
            // 
            // colDriver
            // 
            this.colDriver.Caption = "Driver";
            this.colDriver.FieldName = "DriverName";
            this.colDriver.Name = "colDriver";
            this.colDriver.Visible = true;
            this.colDriver.VisibleIndex = 3;
            this.colDriver.Width = 80;
            // 
            // colCarNo
            // 
            this.colCarNo.FieldName = "CarNo";
            this.colCarNo.Name = "colCarNo";
            this.colCarNo.Visible = true;
            this.colCarNo.VisibleIndex = 4;
            this.colCarNo.Width = 100;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.Caption = "Delivery Time";
            this.colDeliveryDate.ColumnEdit = this.repositoryItemDateEdit1;
            this.colDeliveryDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.colDeliveryDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDeliveryDate.FieldName = "DeliveryDate";
            this.colDeliveryDate.Name = "colDeliveryDate";
            this.colDeliveryDate.Visible = true;
            this.colDeliveryDate.VisibleIndex = 5;
            this.colDeliveryDate.Width = 120;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // colArriveDate
            // 
            this.colArriveDate.Caption = "Arrive Time";
            this.colArriveDate.ColumnEdit = this.repositoryItemDateEdit1;
            this.colArriveDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.colArriveDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colArriveDate.FieldName = "ArriveDate";
            this.colArriveDate.Name = "colArriveDate";
            this.colArriveDate.Visible = true;
            this.colArriveDate.VisibleIndex = 6;
            this.colArriveDate.Width = 120;
            // 
            // colReturnDate
            // 
            this.colReturnDate.Caption = "Return Time";
            this.colReturnDate.ColumnEdit = this.repositoryItemDateEdit1;
            this.colReturnDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.colReturnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colReturnDate.FieldName = "ReturnDate";
            this.colReturnDate.Name = "colReturnDate";
            this.colReturnDate.Visible = true;
            this.colReturnDate.VisibleIndex = 7;
            this.colReturnDate.Width = 120;
            // 
            // colShippingOrderNo
            // 
            this.colShippingOrderNo.Caption = "ShippingOrderNo";
            this.colShippingOrderNo.FieldName = "ShippingOrderNo";
            this.colShippingOrderNo.Name = "colShippingOrderNo";
            this.colShippingOrderNo.Visible = true;
            this.colShippingOrderNo.VisibleIndex = 8;
            // 
            // groupBase
            // 
            this.groupBase.Controls.Add(this.txtDeliveryDate);
            this.groupBase.Controls.Add(this.labDeliveryDate);
            this.groupBase.Controls.Add(this.memoEdit1);
            this.groupBase.Controls.Add(this.txtTotalPkgs);
            this.groupBase.Controls.Add(this.labTotalPkgs);
            this.groupBase.Controls.Add(this.labelControl8);
            this.groupBase.Controls.Add(this.btnPUEmptyCNTR);
            this.groupBase.Controls.Add(this.labEmptyCNTR);
            this.groupBase.Controls.Add(this.stxtBillToID);
            this.groupBase.Controls.Add(this.labelControl6);
            this.groupBase.Controls.Add(this.dteCreateOn);
            this.groupBase.Controls.Add(this.dteLoadingTime);
            this.groupBase.Controls.Add(this.labCreateOn);
            this.groupBase.Controls.Add(this.containerDemandControl1);
            this.groupBase.Controls.Add(this.labContainerDemand);
            this.groupBase.Controls.Add(this.txtFreigtDescription);
            this.groupBase.Controls.Add(this.labNO);
            this.groupBase.Controls.Add(this.labShippingOrderNo);
            this.groupBase.Controls.Add(this.stxtTrucker);
            this.groupBase.Controls.Add(this.stxtCustomerBroker);
            this.groupBase.Controls.Add(this.stxtShipper);
            this.groupBase.Controls.Add(this.labFreigtDescription);
            this.groupBase.Controls.Add(this.stxtDeliveryAt);
            this.groupBase.Controls.Add(this.txtDeliveryAtDescription);
            this.groupBase.Controls.Add(this.labCustomsBroker);
            this.groupBase.Controls.Add(this.labCreateBy);
            this.groupBase.Controls.Add(this.txtShipperDescription);
            this.groupBase.Controls.Add(this.labTrucker);
            this.groupBase.Controls.Add(this.chkIsDrivingLicence);
            this.groupBase.Controls.Add(this.labShipper);
            this.groupBase.Controls.Add(this.labLoadingTime);
            this.groupBase.Controls.Add(this.txtCreateByName);
            this.groupBase.Controls.Add(this.txtVesselVoyage);
            this.groupBase.Controls.Add(this.labDeliveryAt);
            this.groupBase.Controls.Add(this.txtCarrier);
            this.groupBase.Controls.Add(this.labVesselVoyage);
            this.groupBase.Controls.Add(this.txtNO);
            this.groupBase.Controls.Add(this.txtShippingOrderNo);
            this.groupBase.Controls.Add(this.labCarrier);
            this.groupBase.Controls.Add(this.labRemark);
            this.groupBase.Controls.Add(this.txtRemark);
            this.groupBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBase.Location = new System.Drawing.Point(0, 115);
            this.groupBase.Name = "groupBase";
            this.groupBase.Size = new System.Drawing.Size(800, 388);
            this.groupBase.TabIndex = 0;
            this.groupBase.TabStop = false;
            // 
            // memoEdit1
            // 
            this.memoEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "Commodity", true));
            this.dxErrorContainers.SetIconAlignment(this.memoEdit1, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.memoEdit1, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.memoEdit1.Location = new System.Drawing.Point(103, 305);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(230, 48);
            this.memoEdit1.TabIndex = 42;
            // 
            // txtTotalPkgs
            // 
            this.txtTotalPkgs.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "TotalPkgs", true));
            this.txtTotalPkgs.Location = new System.Drawing.Point(469, 179);
            this.txtTotalPkgs.Name = "txtTotalPkgs";
            this.txtTotalPkgs.Size = new System.Drawing.Size(230, 21);
            this.txtTotalPkgs.TabIndex = 40;
            // 
            // labTotalPkgs
            // 
            this.labTotalPkgs.Location = new System.Drawing.Point(372, 182);
            this.labTotalPkgs.Name = "labTotalPkgs";
            this.labTotalPkgs.Size = new System.Drawing.Size(61, 14);
            this.labTotalPkgs.TabIndex = 41;
            this.labTotalPkgs.Text = "Total Pkgs.";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(8, 308);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 39;
            this.labelControl8.Text = "品名";
            // 
            // btnPUEmptyCNTR
            // 
            this.btnPUEmptyCNTR.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "PUEmptyCNTRID", true));
            this.btnPUEmptyCNTR.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "PUEmptyCNTRName", true));
            this.btnPUEmptyCNTR.Location = new System.Drawing.Point(469, 153);
            this.btnPUEmptyCNTR.Name = "btnPUEmptyCNTR";
            this.btnPUEmptyCNTR.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.btnPUEmptyCNTR.Properties.ActionButtonIndex = 1;
            this.btnPUEmptyCNTR.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.btnPUEmptyCNTR.Properties.Appearance.Options.UseBackColor = true;
            this.btnPUEmptyCNTR.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.btnPUEmptyCNTR.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.btnPUEmptyCNTR.Properties.PopupSizeable = false;
            this.btnPUEmptyCNTR.Properties.ShowPopupCloseButton = false;
            this.btnPUEmptyCNTR.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.btnPUEmptyCNTR.Size = new System.Drawing.Size(230, 21);
            this.btnPUEmptyCNTR.TabIndex = 36;
            // 
            // labEmptyCNTR
            // 
            this.labEmptyCNTR.Location = new System.Drawing.Point(372, 156);
            this.labEmptyCNTR.Name = "labEmptyCNTR";
            this.labEmptyCNTR.Size = new System.Drawing.Size(93, 14);
            this.labEmptyCNTR.TabIndex = 37;
            this.labEmptyCNTR.Text = "P/U Empty CNTR";
            // 
            // stxtBillToID
            // 
            this.stxtBillToID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "BillToID", true));
            this.stxtBillToID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "BillToName", true));
            this.stxtBillToID.Location = new System.Drawing.Point(103, 278);
            this.stxtBillToID.Name = "stxtBillToID";
            this.stxtBillToID.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtBillToID.Properties.ActionButtonIndex = 1;
            this.stxtBillToID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtBillToID.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBillToID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtBillToID.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtBillToID.Properties.PopupSizeable = false;
            this.stxtBillToID.Properties.ShowPopupCloseButton = false;
            this.stxtBillToID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtBillToID.Size = new System.Drawing.Size(230, 21);
            this.stxtBillToID.TabIndex = 34;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(8, 281);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 35;
            this.labelControl6.Text = "帐单寄送";
            // 
            // dteCreateOn
            // 
            this.dteCreateOn.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOceanTruckList, "CreateDate", true));
            this.dteCreateOn.EditValue = new System.DateTime(2011, 12, 22, 11, 32, 42, 125);
            this.dteCreateOn.Location = new System.Drawing.Point(469, 85);
            this.dteCreateOn.Name = "dteCreateOn";
            this.dteCreateOn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCreateOn.Properties.DisplayFormat.FormatString = "s";
            this.dteCreateOn.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteCreateOn.Properties.EditFormat.FormatString = "s";
            this.dteCreateOn.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteCreateOn.Properties.Mask.EditMask = "s";
            this.dteCreateOn.Properties.ReadOnly = true;
            this.dteCreateOn.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCreateOn.Size = new System.Drawing.Size(230, 21);
            this.dteCreateOn.TabIndex = 33;
            // 
            // dteLoadingTime
            // 
            this.dteLoadingTime.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOceanTruckList, "LoadingTime", true));
            this.dteLoadingTime.EditValue = new System.DateTime(2011, 12, 22, 11, 32, 31, 484);
            this.dteLoadingTime.Location = new System.Drawing.Point(103, 85);
            this.dteLoadingTime.MenuManager = this.barManager1;
            this.dteLoadingTime.Name = "dteLoadingTime";
            this.dteLoadingTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteLoadingTime.Properties.DisplayFormat.FormatString = "s";
            this.dteLoadingTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteLoadingTime.Properties.EditFormat.FormatString = "s";
            this.dteLoadingTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteLoadingTime.Properties.Mask.EditMask = "s";
            this.dteLoadingTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteLoadingTime.Size = new System.Drawing.Size(230, 21);
            this.dteLoadingTime.TabIndex = 3;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barAdd,
            this.barDelete,
            this.barClose,
            this.barNew,
            this.barRemove,
            this.barPrint});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 12;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 1;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "&Print";
            this.barPrint.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 11;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 4;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(95, 617);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 3";
            // 
            // barNew
            // 
            this.barNew.Caption = "New";
            this.barNew.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Add_16;
            this.barNew.Id = 6;
            this.barNew.Name = "barNew";
            this.barNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNew_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "&Remove";
            this.barRemove.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 7;
            this.barRemove.Name = "barRemove";
            this.barRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemove_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(3, 18);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(794, 26);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(807, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 674);
            this.barDockControlBottom.Size = new System.Drawing.Size(807, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 648);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(807, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 648);
            // 
            // labCreateOn
            // 
            this.labCreateOn.Location = new System.Drawing.Point(372, 88);
            this.labCreateOn.Name = "labCreateOn";
            this.labCreateOn.Size = new System.Drawing.Size(52, 14);
            this.labCreateOn.TabIndex = 29;
            this.labCreateOn.Text = "CreateOn";
            // 
            // containerDemandControl1
            // 
            this.containerDemandControl1.Location = new System.Drawing.Point(103, 358);
            this.containerDemandControl1.Name = "containerDemandControl1";
            this.containerDemandControl1.Size = new System.Drawing.Size(596, 21);
            this.containerDemandControl1.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.containerDemandControl1.TabIndex = 12;
            // 
            // labContainerDemand
            // 
            this.labContainerDemand.Location = new System.Drawing.Point(8, 362);
            this.labContainerDemand.Name = "labContainerDemand";
            this.labContainerDemand.Size = new System.Drawing.Size(68, 14);
            this.labContainerDemand.TabIndex = 26;
            this.labContainerDemand.Text = "Ctn Demand";
            // 
            // txtFreigtDescription
            // 
            this.txtFreigtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "FreigtDescription", true));
            this.dxErrorContainers.SetIconAlignment(this.txtFreigtDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtFreigtDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtFreigtDescription.Location = new System.Drawing.Point(469, 206);
            this.txtFreigtDescription.Name = "txtFreigtDescription";
            this.txtFreigtDescription.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtFreigtDescription.Properties.Appearance.Options.UseBackColor = true;
            this.txtFreigtDescription.Size = new System.Drawing.Size(230, 69);
            this.txtFreigtDescription.TabIndex = 10;
            // 
            // labNO
            // 
            this.labNO.Location = new System.Drawing.Point(8, 19);
            this.labNO.Name = "labNO";
            this.labNO.Size = new System.Drawing.Size(17, 14);
            this.labNO.TabIndex = 3;
            this.labNO.Text = "NO";
            // 
            // labShippingOrderNo
            // 
            this.labShippingOrderNo.Location = new System.Drawing.Point(8, 42);
            this.labShippingOrderNo.Name = "labShippingOrderNo";
            this.labShippingOrderNo.Size = new System.Drawing.Size(92, 14);
            this.labShippingOrderNo.TabIndex = 3;
            this.labShippingOrderNo.Text = "ShippingOrderNo";
            // 
            // stxtTrucker
            // 
            this.stxtTrucker.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "TruckerID", true));
            this.stxtTrucker.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "TruckerName", true));
            this.stxtTrucker.Location = new System.Drawing.Point(103, 62);
            this.stxtTrucker.Name = "stxtTrucker";
            this.stxtTrucker.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtTrucker.Properties.ActionButtonIndex = 1;
            this.stxtTrucker.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtTrucker.Properties.Appearance.Options.UseBackColor = true;
            this.stxtTrucker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtTrucker.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtTrucker.Properties.PopupSizeable = false;
            this.stxtTrucker.Properties.ShowPopupCloseButton = false;
            this.stxtTrucker.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtTrucker.Size = new System.Drawing.Size(230, 21);
            this.stxtTrucker.TabIndex = 2;
            // 
            // stxtCustomerBroker
            // 
            this.stxtCustomerBroker.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "CustomsBrokerID", true));
            this.stxtCustomerBroker.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "CustomsBrokerName", true));
            this.stxtCustomerBroker.Location = new System.Drawing.Point(469, 108);
            this.stxtCustomerBroker.Name = "stxtCustomerBroker";
            this.stxtCustomerBroker.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtCustomerBroker.Properties.ActionButtonIndex = 1;
            this.stxtCustomerBroker.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtCustomerBroker.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomerBroker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtCustomerBroker.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtCustomerBroker.Properties.PopupSizeable = false;
            this.stxtCustomerBroker.Properties.ShowPopupCloseButton = false;
            this.stxtCustomerBroker.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtCustomerBroker.Size = new System.Drawing.Size(230, 21);
            this.stxtCustomerBroker.TabIndex = 5;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "ShipperName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "ShipperID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtShipper.Location = new System.Drawing.Point(103, 108);
            this.stxtShipper.Name = "stxtShipper";
            this.stxtShipper.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtShipper.Properties.ActionButtonIndex = 1;
            this.stxtShipper.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtShipper.Properties.Appearance.Options.UseBackColor = true;
            this.stxtShipper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtShipper.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtShipper.Properties.PopupSizeable = false;
            this.stxtShipper.Properties.ShowPopupCloseButton = false;
            this.stxtShipper.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtShipper.Size = new System.Drawing.Size(230, 21);
            this.stxtShipper.TabIndex = 4;
            // 
            // labFreigtDescription
            // 
            this.labFreigtDescription.Location = new System.Drawing.Point(372, 209);
            this.labFreigtDescription.Name = "labFreigtDescription";
            this.labFreigtDescription.Size = new System.Drawing.Size(91, 14);
            this.labFreigtDescription.TabIndex = 14;
            this.labFreigtDescription.Text = "FreigtDescription";
            // 
            // stxtDeliveryAt
            // 
            this.stxtDeliveryAt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "DeliveryAtName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtDeliveryAt.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfo, "DeliveryAtID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtDeliveryAt.Location = new System.Drawing.Point(103, 203);
            this.stxtDeliveryAt.Name = "stxtDeliveryAt";
            this.stxtDeliveryAt.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtDeliveryAt.Properties.ActionButtonIndex = 1;
            this.stxtDeliveryAt.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtDeliveryAt.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDeliveryAt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtDeliveryAt.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtDeliveryAt.Properties.PopupSizeable = false;
            this.stxtDeliveryAt.Properties.ShowPopupCloseButton = false;
            this.stxtDeliveryAt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtDeliveryAt.Size = new System.Drawing.Size(230, 21);
            this.stxtDeliveryAt.TabIndex = 8;
            // 
            // txtDeliveryAtDescription
            // 
            this.dxErrorContainers.SetIconAlignment(this.txtDeliveryAtDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtDeliveryAtDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtDeliveryAtDescription.Location = new System.Drawing.Point(103, 226);
            this.txtDeliveryAtDescription.Name = "txtDeliveryAtDescription";
            this.txtDeliveryAtDescription.Size = new System.Drawing.Size(230, 48);
            this.txtDeliveryAtDescription.TabIndex = 9;
            // 
            // labCustomsBroker
            // 
            this.labCustomsBroker.Location = new System.Drawing.Point(372, 108);
            this.labCustomsBroker.Name = "labCustomsBroker";
            this.labCustomsBroker.Size = new System.Drawing.Size(81, 14);
            this.labCustomsBroker.TabIndex = 7;
            this.labCustomsBroker.Text = "CustomsBroker";
            // 
            // labCreateBy
            // 
            this.labCreateBy.Location = new System.Drawing.Point(372, 68);
            this.labCreateBy.Name = "labCreateBy";
            this.labCreateBy.Size = new System.Drawing.Size(49, 14);
            this.labCreateBy.TabIndex = 7;
            this.labCreateBy.Text = "CreateBy";
            // 
            // txtShipperDescription
            // 
            this.dxErrorContainers.SetIconAlignment(this.txtShipperDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtShipperDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtShipperDescription.Location = new System.Drawing.Point(103, 131);
            this.txtShipperDescription.Name = "txtShipperDescription";
            this.txtShipperDescription.Size = new System.Drawing.Size(230, 42);
            this.txtShipperDescription.TabIndex = 6;
            // 
            // labTrucker
            // 
            this.labTrucker.Location = new System.Drawing.Point(8, 66);
            this.labTrucker.Name = "labTrucker";
            this.labTrucker.Size = new System.Drawing.Size(42, 14);
            this.labTrucker.TabIndex = 5;
            this.labTrucker.Text = "Trucker";
            // 
            // chkIsDrivingLicence
            // 
            this.chkIsDrivingLicence.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfo, "IsDrivingLicence", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsDrivingLicence.Location = new System.Drawing.Point(469, 131);
            this.chkIsDrivingLicence.Name = "chkIsDrivingLicence";
            this.chkIsDrivingLicence.Properties.Caption = "DrivingLicence";
            this.chkIsDrivingLicence.Size = new System.Drawing.Size(230, 19);
            this.chkIsDrivingLicence.TabIndex = 7;
            // 
            // labShipper
            // 
            this.labShipper.Location = new System.Drawing.Point(8, 111);
            this.labShipper.Name = "labShipper";
            this.labShipper.Size = new System.Drawing.Size(41, 14);
            this.labShipper.TabIndex = 5;
            this.labShipper.Text = "Shipper";
            // 
            // labLoadingTime
            // 
            this.labLoadingTime.Location = new System.Drawing.Point(8, 89);
            this.labLoadingTime.Name = "labLoadingTime";
            this.labLoadingTime.Size = new System.Drawing.Size(69, 14);
            this.labLoadingTime.TabIndex = 5;
            this.labLoadingTime.Text = "LoadingTime";
            // 
            // txtCreateByName
            // 
            this.txtCreateByName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "CreateByName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCreateByName.Enabled = false;
            this.txtCreateByName.Location = new System.Drawing.Point(469, 62);
            this.txtCreateByName.Name = "txtCreateByName";
            this.txtCreateByName.Properties.ReadOnly = true;
            this.txtCreateByName.Size = new System.Drawing.Size(230, 21);
            this.txtCreateByName.TabIndex = 12;
            this.txtCreateByName.TabStop = false;
            // 
            // txtVesselVoyage
            // 
            this.txtVesselVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "VesselVoyage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtVesselVoyage.Enabled = false;
            this.txtVesselVoyage.Location = new System.Drawing.Point(469, 39);
            this.txtVesselVoyage.Name = "txtVesselVoyage";
            this.txtVesselVoyage.Properties.ReadOnly = true;
            this.txtVesselVoyage.Size = new System.Drawing.Size(230, 21);
            this.txtVesselVoyage.TabIndex = 13;
            this.txtVesselVoyage.TabStop = false;
            // 
            // labDeliveryAt
            // 
            this.labDeliveryAt.Location = new System.Drawing.Point(8, 206);
            this.labDeliveryAt.Name = "labDeliveryAt";
            this.labDeliveryAt.Size = new System.Drawing.Size(55, 14);
            this.labDeliveryAt.TabIndex = 5;
            this.labDeliveryAt.Text = "DeliveryAt";
            // 
            // txtCarrier
            // 
            this.txtCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "CarrierName", true));
            this.txtCarrier.Enabled = false;
            this.txtCarrier.Location = new System.Drawing.Point(469, 16);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Properties.ReadOnly = true;
            this.txtCarrier.Size = new System.Drawing.Size(230, 21);
            this.txtCarrier.TabIndex = 1;
            this.txtCarrier.TabStop = false;
            // 
            // labVesselVoyage
            // 
            this.labVesselVoyage.Location = new System.Drawing.Point(372, 42);
            this.labVesselVoyage.Name = "labVesselVoyage";
            this.labVesselVoyage.Size = new System.Drawing.Size(75, 14);
            this.labVesselVoyage.TabIndex = 5;
            this.labVesselVoyage.Text = "VesselVoyage";
            // 
            // txtNO
            // 
            this.txtNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "NO", true));
            this.txtNO.Enabled = false;
            this.txtNO.Location = new System.Drawing.Point(103, 16);
            this.txtNO.Name = "txtNO";
            this.txtNO.Properties.ReadOnly = true;
            this.txtNO.Size = new System.Drawing.Size(230, 21);
            this.txtNO.TabIndex = 0;
            this.txtNO.TabStop = false;
            // 
            // txtShippingOrderNo
            // 
            this.txtShippingOrderNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "ShippingOrderNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtShippingOrderNo.Location = new System.Drawing.Point(103, 39);
            this.txtShippingOrderNo.Name = "txtShippingOrderNo";
            this.txtShippingOrderNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtShippingOrderNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtShippingOrderNo.Size = new System.Drawing.Size(230, 21);
            this.txtShippingOrderNo.TabIndex = 1;
            this.txtShippingOrderNo.TabStop = false;
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(372, 19);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 7;
            this.labCarrier.Text = "Carrier";
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(372, 281);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 3;
            this.labRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfo, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorContainers.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtRemark.Location = new System.Drawing.Point(469, 281);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(230, 72);
            this.txtRemark.TabIndex = 11;
            // 
            // groupContainer
            // 
            this.groupContainer.Controls.Add(this.gridControl2);
            this.groupContainer.Controls.Add(this.standaloneBarDockControl1);
            this.groupContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupContainer.Location = new System.Drawing.Point(0, 503);
            this.groupContainer.Name = "groupContainer";
            this.groupContainer.Size = new System.Drawing.Size(800, 129);
            this.groupContainer.TabIndex = 1;
            this.groupContainer.TabStop = false;
            this.groupContainer.Text = "Container";
            this.groupContainer.Visible = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsOceanTruckList;
            // 
            // dxErrorContainers
            // 
            this.dxErrorContainers.ContainerControl = this;
            this.dxErrorContainers.DataSource = this.bsContainers;
            // 
            // panelScroll
            // 
            this.panelScroll.AllowDrop = true;
            this.panelScroll.Controls.Add(this.panel1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 26);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(807, 648);
            this.panelScroll.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupContainer);
            this.panel1.Controls.Add(this.groupBase);
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 632);
            this.panel1.TabIndex = 4;
            // 
            // txtDeliveryDate
            // 
            this.txtDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOceanTruckList, "DeliveryDate", true));
            this.txtDeliveryDate.EditValue = new System.DateTime(2011, 12, 22, 11, 32, 31, 484);
            this.txtDeliveryDate.Location = new System.Drawing.Point(103, 178);
            this.txtDeliveryDate.MenuManager = this.barManager1;
            this.txtDeliveryDate.Name = "txtDeliveryDate";
            this.txtDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDeliveryDate.Properties.DisplayFormat.FormatString = "s";
            this.txtDeliveryDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDeliveryDate.Properties.EditFormat.FormatString = "s";
            this.txtDeliveryDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDeliveryDate.Properties.Mask.EditMask = "s";
            this.txtDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDeliveryDate.Size = new System.Drawing.Size(230, 21);
            this.txtDeliveryDate.TabIndex = 43;
            // 
            // labDeliveryDate
            // 
            this.labDeliveryDate.Location = new System.Drawing.Point(8, 182);
            this.labDeliveryDate.Name = "labDeliveryDate";
            this.labDeliveryDate.Size = new System.Drawing.Size(68, 14);
            this.labDeliveryDate.TabIndex = 44;
            this.labDeliveryDate.Text = "DeliveryDate";
            // 
            // OceanTruckEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OceanTruckEditPart";
            this.Size = new System.Drawing.Size(807, 674);
            ((System.ComponentModel.ISupportInitialize)(this.bsOceanTruckList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.containersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            this.groupBase.ResumeLayout(false);
            this.groupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalPkgs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPUEmptyCNTR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBillToID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateOn.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateOn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteLoadingTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteLoadingTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreigtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtTrucker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomerBroker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeliveryAt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryAtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipperDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDrivingLicence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateByName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            this.groupContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorContainers)).EndInit();
            this.panelScroll.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsOceanTruckList;
        private System.Windows.Forms.BindingSource bsContainers;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvContainer;
        private DevExpress.XtraGrid.Columns.GridColumn colCtnNo;
        private DevExpress.XtraGrid.Columns.GridColumn colSealNo;
        private System.Windows.Forms.GroupBox groupBase;
        private System.Windows.Forms.GroupBox groupContainer;
        private DevExpress.XtraEditors.TextEdit txtShippingOrderNo;
        private DevExpress.XtraEditors.LabelControl labShippingOrderNo;
        private DevExpress.XtraEditors.LabelControl labTrucker;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorContainers;
        private System.Windows.Forms.BindingSource containersBindingSource;
        private DevExpress.XtraEditors.LabelControl labVesselVoyage;
        private DevExpress.XtraEditors.CheckEdit chkIsDrivingLicence;
        private DevExpress.XtraEditors.LabelControl labCustomsBroker;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtDeliveryAt;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private DevExpress.XtraEditors.TextEdit txtCreateByName;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.TextEdit txtVesselVoyage;
        private DevExpress.XtraEditors.TextEdit txtCarrier;
        private DevExpress.XtraEditors.LabelControl labDeliveryAt;
        private DevExpress.XtraEditors.LabelControl labShipper;
        private DevExpress.XtraEditors.LabelControl labFreigtDescription;
        private DevExpress.XtraEditors.MemoEdit txtFreigtDescription;
        private DevExpress.XtraEditors.MemoEdit txtDeliveryAtDescription;
        private DevExpress.XtraEditors.MemoEdit txtShipperDescription;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbContainer;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryAtName;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckerName;
        private Panel panelScroll;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private BindingSource bsTruckInfo;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barNew;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryDate;
        private DevExpress.XtraGrid.Columns.GridColumn colArriveDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDriver;
        private DevExpress.XtraGrid.Columns.GridColumn colCarNo;
        private DevExpress.XtraEditors.LabelControl labLoadingTime;
        private Panel panel1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private ICP.Framework.ClientComponents.Controls.ContainerDemandControl containerDemandControl1;
        private DevExpress.XtraEditors.LabelControl labContainerDemand;
        private DevExpress.XtraGrid.Columns.GridColumn colLoadingTime;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckNO;
        private DevExpress.XtraEditors.LabelControl labNO;
        private DevExpress.XtraEditors.TextEdit txtNO;
        private DevExpress.XtraEditors.LabelControl labCreateOn;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingOrderNo;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraEditors.DateEdit dteCreateOn;
        private DevExpress.XtraEditors.DateEdit dteLoadingTime;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtTrucker;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtCustomerBroker;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtBillToID;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit btnPUEmptyCNTR;
        private DevExpress.XtraEditors.LabelControl labEmptyCNTR;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtTotalPkgs;
        private DevExpress.XtraEditors.LabelControl labTotalPkgs;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.DateEdit txtDeliveryDate;
        private DevExpress.XtraEditors.LabelControl labDeliveryDate;
    }
}

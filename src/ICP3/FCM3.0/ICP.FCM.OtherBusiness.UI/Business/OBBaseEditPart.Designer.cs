using System.Windows.Forms;
namespace ICP.FCM.OtherBusiness.UI.Business
{
    partial class OBBaseEditPart
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OBBaseEditPart));
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.bsOtherInfo = new System.Windows.Forms.BindingSource(this.components);
            this.stxtCustomer = new DevExpress.XtraEditors.PopupContainerEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barTruck = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.txtState = new DevExpress.XtraEditors.TextEdit();
            this.txtMBL = new DevExpress.XtraEditors.TextEdit();
            this.txtHBL = new DevExpress.XtraEditors.TextEdit();
            this.customerPopupContainerEdit1 = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtAgentOfCarrier = new DevExpress.XtraEditors.ButtonEdit();
            this.txtGoods = new DevExpress.XtraEditors.TextEdit();
            this.stxtDeparture = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtDetination = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtDes = new DevExpress.XtraEditors.ButtonEdit();
            this.lwPackages = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.spinPackages = new DevExpress.XtraEditors.SpinEdit();
            this.cmbWeightUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.cmbMeasurementUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.txtCount = new DevExpress.XtraEditors.TextEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit5 = new DevExpress.XtraEditors.TextEdit();
            this.chkIsWarehouse = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsQuarantineInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCommodityInspection = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsCustoms = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsTruck = new DevExpress.XtraEditors.CheckEdit();
            this.btnWare = new DevExpress.XtraEditors.ButtonEdit();
            this.btnCustom = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtOperationNo = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtShipper = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.treeOperation = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.textEdit6 = new DevExpress.XtraEditors.TextEdit();
            this.lwNotifyParty = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbPaymentTerm = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.mcmbOverseasFiler = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.stxtVesselVoyage = new ICP.Common.UI.UCVoyageLookupEdit();
            this.tabPageBase = new DevExpress.XtraTab.XtraTabPage();
            this.panelScroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl2 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.orderFeeEditPart1 = new ICP.FCM.OtherBusiness.UI.Business.OrderFeeEditPart();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labOverseasFiler = new DevExpress.XtraEditors.LabelControl();
            this.labPaymentTerm = new DevExpress.XtraEditors.LabelControl();
            this.labSONo = new DevExpress.XtraEditors.LabelControl();
            this.memoRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.groupLocalService = new System.Windows.Forms.GroupBox();
            this.labOperation = new DevExpress.XtraEditors.LabelControl();
            this.labVesselVoyage = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labShutDate = new DevExpress.XtraEditors.LabelControl();
            this.dateShutDate = new DevExpress.XtraEditors.DateEdit();
            this.chkShutSingle = new DevExpress.XtraEditors.CheckEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labPackages = new DevExpress.XtraEditors.LabelControl();
            this.labBusinessDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit3 = new DevExpress.XtraEditors.DateEdit();
            this.labETA = new DevExpress.XtraEditors.LabelControl();
            this.dteETA = new DevExpress.XtraEditors.DateEdit();
            this.labFETA = new DevExpress.XtraEditors.LabelControl();
            this.dateFETA = new DevExpress.XtraEditors.DateEdit();
            this.labETD = new DevExpress.XtraEditors.LabelControl();
            this.dteETD = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labDetination = new DevExpress.XtraEditors.LabelControl();
            this.labDeparture = new DevExpress.XtraEditors.LabelControl();
            this.treeSelectBox1 = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labGoods = new DevExpress.XtraEditors.LabelControl();
            this.multiSearchCommonBox1 = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labNotify = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labShiper = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labMBL = new DevExpress.XtraEditors.LabelControl();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lwGridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsContainer = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbBoxType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSealNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colQuantityName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Unit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.labCount = new DevExpress.XtraEditors.LabelControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.navBarcontainerInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOtherInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerPopupContainerEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoods.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwPackages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPackages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWare.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOperation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwNotifyParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbOverseasFiler.Properties)).BeginInit();
            this.tabPageBase.SuspendLayout();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).BeginInit();
            this.navBarControl2.SuspendLayout();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).BeginInit();
            this.groupLocalService.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateShutDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateShutDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShutSingle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoxType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Unit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "NO", true));
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "NO", true));
            this.txtNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNo.Location = new System.Drawing.Point(101, 3);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(119, 21);
            this.txtNo.TabIndex = 0;
            this.txtNo.TabStop = false;
            // 
            // bsOtherInfo
            // 
            this.bsOtherInfo.DataSource = typeof(ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.OtherBusinessInfo);
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "CustomerID", true));
            this.stxtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "CustomerName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtCustomer.Location = new System.Drawing.Point(101, 57);
            this.stxtCustomer.MenuManager = this.barManager1;
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtCustomer.Properties.CloseOnLostFocus = false;
            this.stxtCustomer.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.stxtCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtCustomer.ShowToolTips = false;
            this.stxtCustomer.Size = new System.Drawing.Size(286, 21);
            toolTipTitleItem2.Text = "提示";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "没有业务";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.stxtCustomer.SuperTip = superToolTip2;
            this.stxtCustomer.TabIndex = 4;
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
            this.barSave,
            this.barSaveAs,
            this.barPrint,
            this.barClose,
            this.barRefresh,
            this.barButtonItem1,
            this.barTruck,
            this.barButtonItem2});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaveAs, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTruck, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = ((System.Drawing.Image)(resources.GetObject("barSave.Glyph")));
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barSaveAs
            // 
            this.barSaveAs.Caption = "Save&As";
            this.barSaveAs.Glyph = ((System.Drawing.Image)(resources.GetObject("barSaveAs.Glyph")));
            this.barSaveAs.Id = 1;
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaveAs_ItemClick);
            // 
            // barTruck
            // 
            this.barTruck.Caption = "Truck";
            this.barTruck.Id = 8;
            this.barTruck.Name = "barTruck";
            this.barTruck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTruck_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "&Print";
            this.barPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("barPrint.Glyph")));
            this.barPrint.Id = 2;
            this.barPrint.Name = "barPrint";
            this.barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("barRefresh.Glyph")));
            this.barRefresh.Id = 6;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Bill";
            this.barButtonItem2.Glyph = global::ICP.FCM.OtherBusiness.UI.Properties.Resources.Memo_16;
            this.barButtonItem2.Id = 9;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OtherBusiness.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 5;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(862, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 880);
            this.barDockControlBottom.Size = new System.Drawing.Size(862, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 854);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(862, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 854);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Preview";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // txtState
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtState, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtState.Location = new System.Drawing.Point(272, 3);
            this.txtState.Name = "txtState";
            this.txtState.Properties.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(115, 21);
            this.txtState.TabIndex = 1;
            this.txtState.TabStop = false;
            // 
            // txtMBL
            // 
            this.txtMBL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "Mblno", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtMBL, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtMBL.Location = new System.Drawing.Point(101, 111);
            this.txtMBL.Name = "txtMBL";
            this.txtMBL.Size = new System.Drawing.Size(93, 21);
            this.txtMBL.TabIndex = 7;
            this.txtMBL.TabStop = false;
            // 
            // txtHBL
            // 
            this.txtHBL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "Hblno", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtHBL, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtHBL.Location = new System.Drawing.Point(297, 111);
            this.txtHBL.Name = "txtHBL";
            this.txtHBL.Size = new System.Drawing.Size(90, 21);
            this.txtHBL.TabIndex = 8;
            this.txtHBL.TabStop = false;
            // 
            // customerPopupContainerEdit1
            // 
            this.customerPopupContainerEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "ConsigneeID", true));
            this.customerPopupContainerEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "ConsigneeName", true));
            this.dxErrorProvider1.SetIconAlignment(this.customerPopupContainerEdit1, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.customerPopupContainerEdit1.Location = new System.Drawing.Point(101, 165);
            this.customerPopupContainerEdit1.Name = "customerPopupContainerEdit1";
            this.customerPopupContainerEdit1.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.customerPopupContainerEdit1.Properties.ActionButtonIndex = 1;
            this.customerPopupContainerEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.customerPopupContainerEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.customerPopupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.customerPopupContainerEdit1.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.customerPopupContainerEdit1.Properties.PopupSizeable = false;
            this.customerPopupContainerEdit1.Properties.ShowPopupCloseButton = false;
            this.customerPopupContainerEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.customerPopupContainerEdit1.Size = new System.Drawing.Size(284, 21);
            this.customerPopupContainerEdit1.TabIndex = 10;
            // 
            // stxtAgentOfCarrier
            // 
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "AgentofCarrierID", true));
            this.stxtAgentOfCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "AgengofCarrierName", true));
            this.stxtAgentOfCarrier.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtAgentOfCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtAgentOfCarrier.Location = new System.Drawing.Point(101, 219);
            this.stxtAgentOfCarrier.Name = "stxtAgentOfCarrier";
            this.stxtAgentOfCarrier.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAgentOfCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgentOfCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtAgentOfCarrier.Size = new System.Drawing.Size(284, 21);
            this.stxtAgentOfCarrier.TabIndex = 12;
            // 
            // txtGoods
            // 
            this.txtGoods.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "Commodity", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtGoods, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtGoods.Location = new System.Drawing.Point(513, 165);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(282, 21);
            this.txtGoods.TabIndex = 30;
            this.txtGoods.TabStop = false;
            // 
            // stxtDeparture
            // 
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "PolID", true));
            this.stxtDeparture.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PolName", true));
            this.stxtDeparture.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtDeparture, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDeparture.Location = new System.Drawing.Point(513, 3);
            this.stxtDeparture.Name = "stxtDeparture";
            this.stxtDeparture.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtDeparture.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDeparture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDeparture.Size = new System.Drawing.Size(133, 21);
            this.stxtDeparture.TabIndex = 19;
            // 
            // stxtDetination
            // 
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "PodID", true));
            this.stxtDetination.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PodName", true));
            this.stxtDetination.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtDetination, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDetination.Location = new System.Drawing.Point(513, 30);
            this.stxtDetination.Name = "stxtDetination";
            this.stxtDetination.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtDetination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDetination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDetination.Size = new System.Drawing.Size(133, 21);
            this.stxtDetination.TabIndex = 21;
            // 
            // stxtDes
            // 
            this.stxtDes.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "FinalDestinationID", true));
            this.stxtDes.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "FinalDestinationName", true));
            this.stxtDes.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtDes, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtDes.Location = new System.Drawing.Point(513, 57);
            this.stxtDes.Name = "stxtDes";
            this.stxtDes.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtDes.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtDes.Size = new System.Drawing.Size(133, 21);
            this.stxtDes.TabIndex = 23;
            // 
            // lwPackages
            // 
            this.lwPackages.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "QuantityUnitID", true));
            this.dxErrorProvider1.SetIconAlignment(this.lwPackages, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lwPackages.Location = new System.Drawing.Point(617, 192);
            this.lwPackages.Name = "lwPackages";
            this.lwPackages.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lwPackages.Properties.Appearance.Options.UseBackColor = true;
            this.lwPackages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lwPackages.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.lwPackages.Size = new System.Drawing.Size(179, 21);
            this.lwPackages.SpecifiedBackColor = System.Drawing.Color.White;
            this.lwPackages.TabIndex = 32;
            // 
            // spinPackages
            // 
            this.spinPackages.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Quantity", true));
            this.spinPackages.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.spinPackages, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.spinPackages.Location = new System.Drawing.Point(513, 192);
            this.spinPackages.Name = "spinPackages";
            this.spinPackages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinPackages.Properties.DisplayFormat.FormatString = "F3";
            this.spinPackages.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPackages.Properties.EditFormat.FormatString = "F3";
            this.spinPackages.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPackages.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinPackages.Properties.Mask.EditMask = "F3";
            this.spinPackages.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinPackages.Size = new System.Drawing.Size(98, 21);
            this.spinPackages.TabIndex = 31;
            // 
            // cmbWeightUnit
            // 
            this.cmbWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "WeightUnitID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbWeightUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbWeightUnit.Location = new System.Drawing.Point(617, 219);
            this.cmbWeightUnit.Name = "cmbWeightUnit";
            this.cmbWeightUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWeightUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbWeightUnit.Size = new System.Drawing.Size(178, 21);
            this.cmbWeightUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbWeightUnit.TabIndex = 34;
            // 
            // spinEdit1
            // 
            this.spinEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Weight", true));
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.spinEdit1, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.spinEdit1.Location = new System.Drawing.Point(513, 219);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.DisplayFormat.FormatString = "F3";
            this.spinEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit1.Properties.EditFormat.FormatString = "F3";
            this.spinEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit1.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinEdit1.Properties.Mask.EditMask = "F3";
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(98, 21);
            this.spinEdit1.TabIndex = 33;
            // 
            // cmbMeasurementUnit
            // 
            this.cmbMeasurementUnit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "MeasurementUnitID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbMeasurementUnit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMeasurementUnit.Location = new System.Drawing.Point(617, 246);
            this.cmbMeasurementUnit.Name = "cmbMeasurementUnit";
            this.cmbMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbMeasurementUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeasurementUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbMeasurementUnit.Size = new System.Drawing.Size(178, 21);
            this.cmbMeasurementUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbMeasurementUnit.TabIndex = 36;
            // 
            // spinEdit2
            // 
            this.spinEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Measurement", true));
            this.spinEdit2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dxErrorProvider1.SetIconAlignment(this.spinEdit2, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.spinEdit2.Location = new System.Drawing.Point(513, 246);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit2.Properties.DisplayFormat.FormatString = "F3";
            this.spinEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit2.Properties.EditFormat.FormatString = "F3";
            this.spinEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit2.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinEdit2.Properties.Mask.EditMask = "F3";
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.spinEdit2.Size = new System.Drawing.Size(98, 21);
            this.spinEdit2.TabIndex = 35;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "CompanyID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCompany, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCompany.Location = new System.Drawing.Point(101, 327);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCompany.Size = new System.Drawing.Size(284, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 16;
            // 
            // textEdit2
            // 
            this.dxErrorProvider1.SetIconAlignment(this.textEdit2, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEdit2.Location = new System.Drawing.Point(106, 15);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.ReadOnly = true;
            this.textEdit2.Size = new System.Drawing.Size(98, 21);
            this.textEdit2.TabIndex = 0;
            this.textEdit2.TabStop = false;
            // 
            // txtCount
            // 
            this.dxErrorProvider1.SetIconAlignment(this.txtCount, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCount.Location = new System.Drawing.Point(259, 2);
            this.txtCount.Name = "txtCount";
            this.txtCount.Properties.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(93, 21);
            this.txtCount.TabIndex = 669;
            this.txtCount.TabStop = false;
            // 
            // textEdit3
            // 
            this.textEdit3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PolName", true));
            this.dxErrorProvider1.SetIconAlignment(this.textEdit3, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEdit3.Location = new System.Drawing.Point(652, 3);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(143, 21);
            this.textEdit3.TabIndex = 20;
            this.textEdit3.TabStop = false;
            // 
            // textEdit4
            // 
            this.textEdit4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "PodName", true));
            this.dxErrorProvider1.SetIconAlignment(this.textEdit4, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEdit4.Location = new System.Drawing.Point(652, 30);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(143, 21);
            this.textEdit4.TabIndex = 22;
            this.textEdit4.TabStop = false;
            // 
            // textEdit5
            // 
            this.textEdit5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "FinalDestinationName", true));
            this.dxErrorProvider1.SetIconAlignment(this.textEdit5, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEdit5.Location = new System.Drawing.Point(652, 57);
            this.textEdit5.Name = "textEdit5";
            this.textEdit5.Size = new System.Drawing.Size(143, 21);
            this.textEdit5.TabIndex = 24;
            this.textEdit5.TabStop = false;
            // 
            // chkIsWarehouse
            // 
            this.chkIsWarehouse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "IsWareHouse", true));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsWarehouse, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsWarehouse.Location = new System.Drawing.Point(6, 17);
            this.chkIsWarehouse.Name = "chkIsWarehouse";
            this.chkIsWarehouse.Properties.Caption = "Warehouse";
            this.chkIsWarehouse.Size = new System.Drawing.Size(85, 19);
            this.chkIsWarehouse.TabIndex = 690;
            // 
            // chkIsQuarantineInspection
            // 
            this.chkIsQuarantineInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "IsQuarantineInspection", true));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsQuarantineInspection, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsQuarantineInspection.Location = new System.Drawing.Point(243, 68);
            this.chkIsQuarantineInspection.Name = "chkIsQuarantineInspection";
            this.chkIsQuarantineInspection.Properties.Caption = "QuarantineInspection";
            this.chkIsQuarantineInspection.Size = new System.Drawing.Size(151, 19);
            this.chkIsQuarantineInspection.TabIndex = 3;
            // 
            // chkIsCommodityInspection
            // 
            this.chkIsCommodityInspection.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "IsCommodityInspection", true));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsCommodityInspection, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsCommodityInspection.Location = new System.Drawing.Point(104, 68);
            this.chkIsCommodityInspection.Name = "chkIsCommodityInspection";
            this.chkIsCommodityInspection.Properties.Caption = "CommodityInspection";
            this.chkIsCommodityInspection.Size = new System.Drawing.Size(152, 19);
            this.chkIsCommodityInspection.TabIndex = 2;
            // 
            // chkIsCustoms
            // 
            this.chkIsCustoms.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "IsCustoms", true));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsCustoms, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsCustoms.Location = new System.Drawing.Point(6, 43);
            this.chkIsCustoms.Name = "chkIsCustoms";
            this.chkIsCustoms.Properties.Caption = "Customs";
            this.chkIsCustoms.Size = new System.Drawing.Size(69, 19);
            this.chkIsCustoms.TabIndex = 710;
            // 
            // chkIsTruck
            // 
            this.chkIsTruck.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "IsTruck", true));
            this.dxErrorProvider1.SetIconAlignment(this.chkIsTruck, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.chkIsTruck.Location = new System.Drawing.Point(6, 68);
            this.chkIsTruck.Name = "chkIsTruck";
            this.chkIsTruck.Properties.Caption = "Truck";
            this.chkIsTruck.Size = new System.Drawing.Size(68, 19);
            this.chkIsTruck.TabIndex = 700;
            // 
            // btnWare
            // 
            this.btnWare.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "WarehouseID", true));
            this.btnWare.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "WarehouseName", true));
            this.btnWare.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.btnWare, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.btnWare.Location = new System.Drawing.Point(106, 16);
            this.btnWare.Name = "btnWare";
            this.btnWare.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.btnWare.Properties.Appearance.Options.UseBackColor = true;
            this.btnWare.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnWare.Size = new System.Drawing.Size(282, 21);
            this.btnWare.TabIndex = 0;
            // 
            // btnCustom
            // 
            this.btnCustom.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "CustomsBrokerID", true));
            this.btnCustom.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "CustomsBrokerName", true));
            this.btnCustom.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.btnCustom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.btnCustom.Location = new System.Drawing.Point(107, 41);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCustom.Properties.Appearance.Options.UseBackColor = true;
            this.btnCustom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustom.Size = new System.Drawing.Size(281, 21);
            this.btnCustom.TabIndex = 1;
            // 
            // stxtOperationNo
            // 
            this.stxtOperationNo.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "OperationID", true));
            this.stxtOperationNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "OperationNo", true));
            this.stxtOperationNo.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.stxtOperationNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtOperationNo.Location = new System.Drawing.Point(272, 30);
            this.stxtOperationNo.Name = "stxtOperationNo";
            this.stxtOperationNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtOperationNo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtOperationNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtOperationNo.Size = new System.Drawing.Size(115, 21);
            this.stxtOperationNo.TabIndex = 3;
            // 
            // stxtShipper
            // 
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "ShipperID", true));
            this.stxtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "ShipperName", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtShipper, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtShipper.Location = new System.Drawing.Point(101, 138);
            this.stxtShipper.Name = "stxtShipper";
            this.stxtShipper.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtShipper.Properties.ActionButtonIndex = 1;
            this.stxtShipper.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtShipper.Properties.Appearance.Options.UseBackColor = true;
            this.stxtShipper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtShipper.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtShipper.Properties.PopupSizeable = false;
            this.stxtShipper.Properties.ShowPopupCloseButton = false;
            this.stxtShipper.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtShipper.Size = new System.Drawing.Size(284, 21);
            this.stxtShipper.TabIndex = 9;
            // 
            // treeOperation
            // 
            this.treeOperation.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "OperatorID", true));
            this.dxErrorProvider1.SetIconAlignment(this.treeOperation, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.treeOperation.Location = new System.Drawing.Point(101, 354);
            this.treeOperation.Name = "treeOperation";
            this.treeOperation.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.treeOperation.Properties.Appearance.Options.UseBackColor = true;
            this.treeOperation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.treeOperation.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.treeOperation.Size = new System.Drawing.Size(284, 21);
            this.treeOperation.SpecifiedBackColor = System.Drawing.Color.White;
            this.treeOperation.TabIndex = 17;
            // 
            // textEdit6
            // 
            this.textEdit6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "SoNo", true));
            this.dxErrorProvider1.SetIconAlignment(this.textEdit6, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEdit6.Location = new System.Drawing.Point(101, 84);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Size = new System.Drawing.Size(93, 21);
            this.textEdit6.TabIndex = 5;
            this.textEdit6.TabStop = false;
            // 
            // lwNotifyParty
            // 
            this.lwNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "NotifyPartyID", true));
            this.lwNotifyParty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "NotifyPartyName", true));
            this.dxErrorProvider1.SetIconAlignment(this.lwNotifyParty, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lwNotifyParty.Location = new System.Drawing.Point(101, 192);
            this.lwNotifyParty.Name = "lwNotifyParty";
            this.lwNotifyParty.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.lwNotifyParty.Properties.ActionButtonIndex = 1;
            this.lwNotifyParty.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lwNotifyParty.Properties.Appearance.Options.UseBackColor = true;
            this.lwNotifyParty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.lwNotifyParty.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.lwNotifyParty.Properties.PopupSizeable = false;
            this.lwNotifyParty.Properties.ShowPopupCloseButton = false;
            this.lwNotifyParty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lwNotifyParty.Size = new System.Drawing.Size(284, 21);
            this.lwNotifyParty.TabIndex = 11;
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "OtOperationType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbType.Location = new System.Drawing.Point(101, 30);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbType.Size = new System.Drawing.Size(119, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbType.TabIndex = 2;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "PaymentTypeID", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbPaymentTerm, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbPaymentTerm.Location = new System.Drawing.Point(297, 84);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentTerm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPaymentTerm.Size = new System.Drawing.Size(90, 21);
            this.cmbPaymentTerm.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPaymentTerm.TabIndex = 6;
            // 
            // mcmbOverseasFiler
            // 
            this.mcmbOverseasFiler.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "OverseasFilerID", true));
            this.dxErrorProvider1.SetIconAlignment(this.mcmbOverseasFiler, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.mcmbOverseasFiler.Location = new System.Drawing.Point(101, 381);
            this.mcmbOverseasFiler.Name = "mcmbOverseasFiler";
            this.mcmbOverseasFiler.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mcmbOverseasFiler.Properties.Appearance.Options.UseBackColor = true;
            this.mcmbOverseasFiler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mcmbOverseasFiler.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.mcmbOverseasFiler.Size = new System.Drawing.Size(284, 21);
            this.mcmbOverseasFiler.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbOverseasFiler.TabIndex = 18;
            // 
            // stxtVesselVoyage
            // 
            this.stxtVesselVoyage.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsOtherInfo, "VesselVoyage", true));
            this.stxtVesselVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsOtherInfo, "VoyageID", true));
            this.stxtVesselVoyage.EditText = "";
            this.stxtVesselVoyage.EditValue = null;
            this.stxtVesselVoyage.Location = new System.Drawing.Point(512, 83);
            this.stxtVesselVoyage.Name = "stxtVesselVoyage";
            this.stxtVesselVoyage.ReadOnly = false;
            this.stxtVesselVoyage.RefreshButtonToolTip = "刷新以获取与输入相匹配的船名/航次";
            this.stxtVesselVoyage.ShowRefreshButton = true;
            this.stxtVesselVoyage.Size = new System.Drawing.Size(284, 21);
            this.stxtVesselVoyage.SpecifiedBackColor = System.Drawing.Color.White;
            this.stxtVesselVoyage.TabIndex = 25;
            this.stxtVesselVoyage.ToolTip = "";
            // 
            // tabPageBase
            // 
            this.tabPageBase.AutoScroll = true;
            this.tabPageBase.Controls.Add(this.panelScroll);
            this.tabPageBase.Name = "tabPageBase";
            this.tabPageBase.Size = new System.Drawing.Size(832, 847);
            this.tabPageBase.Text = "Base Info";
            // 
            // panelScroll
            // 
            this.panelScroll.AutoScroll = false;
            this.panelScroll.Controls.Add(this.navBarControl2);
            this.panelScroll.Controls.Add(this.navBarControl1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelScroll.Location = new System.Drawing.Point(0, 0);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(815, 1043);
            this.panelScroll.TabIndex = 5;
            // 
            // navBarControl2
            // 
            this.navBarControl2.ActiveGroup = this.navBarGroup1;
            this.navBarControl2.Controls.Add(this.navBarGroupControlContainer3);
            this.navBarControl2.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl2.Location = new System.Drawing.Point(5, 690);
            this.navBarControl2.Name = "navBarControl2";
            this.navBarControl2.OptionsNavPane.ExpandedWidth = 813;
            this.navBarControl2.Size = new System.Drawing.Size(815, 271);
            this.navBarControl2.TabIndex = 4;
            this.navBarControl2.Text = "Others";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "navBarGroup1";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 209;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.orderFeeEditPart1);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(811, 207);
            this.navBarGroupControlContainer3.TabIndex = 3;
            // 
            // orderFeeEditPart1
            // 
            this.orderFeeEditPart1.AutoScroll = true;
            this.orderFeeEditPart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderFeeEditPart1.Location = new System.Drawing.Point(0, 0);
            this.orderFeeEditPart1.Name = "orderFeeEditPart1";
            this.orderFeeEditPart1.Size = new System.Drawing.Size(811, 207);
            this.orderFeeEditPart1.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarcontainerInfo});
            this.navBarControl1.Location = new System.Drawing.Point(3, -2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 810;
            this.navBarControl1.Size = new System.Drawing.Size(812, 800);
            this.navBarControl1.TabIndex = 3;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "Base Info";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 478;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.mcmbOverseasFiler);
            this.navBarGroupControlContainer1.Controls.Add(this.labOverseasFiler);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbPaymentTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.labPaymentTerm);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbType);
            this.navBarGroupControlContainer1.Controls.Add(this.lwNotifyParty);
            this.navBarGroupControlContainer1.Controls.Add(this.textEdit6);
            this.navBarGroupControlContainer1.Controls.Add(this.labSONo);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtShipper);
            this.navBarGroupControlContainer1.Controls.Add(this.memoRemark);
            this.navBarGroupControlContainer1.Controls.Add(this.labRemark);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtOperationNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl9);
            this.navBarGroupControlContainer1.Controls.Add(this.groupLocalService);
            this.navBarGroupControlContainer1.Controls.Add(this.labOperation);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtVesselVoyage);
            this.navBarGroupControlContainer1.Controls.Add(this.labVesselVoyage);
            this.navBarGroupControlContainer1.Controls.Add(this.textEdit5);
            this.navBarGroupControlContainer1.Controls.Add(this.textEdit4);
            this.navBarGroupControlContainer1.Controls.Add(this.textEdit3);
            this.navBarGroupControlContainer1.Controls.Add(this.groupBox1);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl8);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbMeasurementUnit);
            this.navBarGroupControlContainer1.Controls.Add(this.spinEdit2);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbWeightUnit);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl7);
            this.navBarGroupControlContainer1.Controls.Add(this.spinEdit1);
            this.navBarGroupControlContainer1.Controls.Add(this.lwPackages);
            this.navBarGroupControlContainer1.Controls.Add(this.labPackages);
            this.navBarGroupControlContainer1.Controls.Add(this.spinPackages);
            this.navBarGroupControlContainer1.Controls.Add(this.labBusinessDate);
            this.navBarGroupControlContainer1.Controls.Add(this.dateEdit3);
            this.navBarGroupControlContainer1.Controls.Add(this.labETA);
            this.navBarGroupControlContainer1.Controls.Add(this.dteETA);
            this.navBarGroupControlContainer1.Controls.Add(this.labFETA);
            this.navBarGroupControlContainer1.Controls.Add(this.dateFETA);
            this.navBarGroupControlContainer1.Controls.Add(this.labETD);
            this.navBarGroupControlContainer1.Controls.Add(this.dteETD);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtDes);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl6);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtDetination);
            this.navBarGroupControlContainer1.Controls.Add(this.labDetination);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtDeparture);
            this.navBarGroupControlContainer1.Controls.Add(this.labDeparture);
            this.navBarGroupControlContainer1.Controls.Add(this.treeSelectBox1);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl5);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbSales);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl4);
            this.navBarGroupControlContainer1.Controls.Add(this.txtGoods);
            this.navBarGroupControlContainer1.Controls.Add(this.labGoods);
            this.navBarGroupControlContainer1.Controls.Add(this.multiSearchCommonBox1);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl3);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtAgentOfCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labAgentOfCarrier);
            this.navBarGroupControlContainer1.Controls.Add(this.labNotify);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer1.Controls.Add(this.customerPopupContainerEdit1);
            this.navBarGroupControlContainer1.Controls.Add(this.labShiper);
            this.navBarGroupControlContainer1.Controls.Add(this.txtHBL);
            this.navBarGroupControlContainer1.Controls.Add(this.txtMBL);
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl2);
            this.navBarGroupControlContainer1.Controls.Add(this.labMBL);
            this.navBarGroupControlContainer1.Controls.Add(this.txtState);
            this.navBarGroupControlContainer1.Controls.Add(this.labState);
            this.navBarGroupControlContainer1.Controls.Add(this.stxtCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labNo);
            this.navBarGroupControlContainer1.Controls.Add(this.txtNo);
            this.navBarGroupControlContainer1.Controls.Add(this.labCustomer);
            this.navBarGroupControlContainer1.Controls.Add(this.labType);
            this.navBarGroupControlContainer1.Controls.Add(this.treeOperation);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(808, 476);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // labOverseasFiler
            // 
            this.labOverseasFiler.Location = new System.Drawing.Point(4, 384);
            this.labOverseasFiler.Name = "labOverseasFiler";
            this.labOverseasFiler.Size = new System.Drawing.Size(36, 14);
            this.labOverseasFiler.TabIndex = 728;
            this.labOverseasFiler.Text = "O.FIler";
            // 
            // labPaymentTerm
            // 
            this.labPaymentTerm.Location = new System.Drawing.Point(216, 87);
            this.labPaymentTerm.Name = "labPaymentTerm";
            this.labPaymentTerm.Size = new System.Drawing.Size(77, 14);
            this.labPaymentTerm.TabIndex = 726;
            this.labPaymentTerm.Text = "PaymentTerm";
            // 
            // labSONo
            // 
            this.labSONo.Location = new System.Drawing.Point(4, 87);
            this.labSONo.Name = "labSONo";
            this.labSONo.Size = new System.Drawing.Size(29, 14);
            this.labSONo.TabIndex = 722;
            this.labSONo.Text = "SoNo";
            // 
            // memoRemark
            // 
            this.memoRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsOtherInfo, "Remark", true));
            this.memoRemark.Location = new System.Drawing.Point(101, 411);
            this.memoRemark.MenuManager = this.barManager1;
            this.memoRemark.Name = "memoRemark";
            this.memoRemark.Size = new System.Drawing.Size(695, 59);
            this.memoRemark.TabIndex = 37;
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(4, 413);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 717;
            this.labRemark.Text = "Remark";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(226, 33);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(45, 14);
            this.labelControl9.TabIndex = 711;
            this.labelControl9.Text = "Business";
            // 
            // groupLocalService
            // 
            this.groupLocalService.Controls.Add(this.btnCustom);
            this.groupLocalService.Controls.Add(this.btnWare);
            this.groupLocalService.Controls.Add(this.chkIsWarehouse);
            this.groupLocalService.Controls.Add(this.chkIsQuarantineInspection);
            this.groupLocalService.Controls.Add(this.chkIsCommodityInspection);
            this.groupLocalService.Controls.Add(this.chkIsCustoms);
            this.groupLocalService.Controls.Add(this.chkIsTruck);
            this.groupLocalService.Font = new System.Drawing.Font("Tahoma", 8F);
            this.groupLocalService.Location = new System.Drawing.Point(407, 274);
            this.groupLocalService.Name = "groupLocalService";
            this.groupLocalService.Size = new System.Drawing.Size(398, 88);
            this.groupLocalService.TabIndex = 710;
            this.groupLocalService.TabStop = false;
            this.groupLocalService.Text = "LocalService";
            // 
            // labOperation
            // 
            this.labOperation.Location = new System.Drawing.Point(4, 357);
            this.labOperation.Name = "labOperation";
            this.labOperation.Size = new System.Drawing.Size(54, 14);
            this.labOperation.TabIndex = 708;
            this.labOperation.Text = "Operation";
            // 
            // labVesselVoyage
            // 
            this.labVesselVoyage.Location = new System.Drawing.Point(416, 87);
            this.labVesselVoyage.Name = "labVesselVoyage";
            this.labVesselVoyage.Size = new System.Drawing.Size(75, 14);
            this.labVesselVoyage.TabIndex = 706;
            this.labVesselVoyage.Text = "VesselVoyage";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labShutDate);
            this.groupBox1.Controls.Add(this.dateShutDate);
            this.groupBox1.Controls.Add(this.textEdit2);
            this.groupBox1.Controls.Add(this.chkShutSingle);
            this.groupBox1.Location = new System.Drawing.Point(408, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 40);
            this.groupBox1.TabIndex = 702;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ShutSingle";
            // 
            // labShutDate
            // 
            this.labShutDate.Location = new System.Drawing.Point(217, 19);
            this.labShutDate.Name = "labShutDate";
            this.labShutDate.Size = new System.Drawing.Size(52, 14);
            this.labShutDate.TabIndex = 702;
            this.labShutDate.Text = "ShutDate";
            // 
            // dateShutDate
            // 
            this.dateShutDate.EditValue = null;
            this.dateShutDate.Location = new System.Drawing.Point(282, 15);
            this.dateShutDate.Name = "dateShutDate";
            this.dateShutDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateShutDate.Properties.Mask.EditMask = "";
            this.dateShutDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateShutDate.Properties.ReadOnly = true;
            this.dateShutDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateShutDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateShutDate.Size = new System.Drawing.Size(105, 21);
            this.dateShutDate.TabIndex = 1;
            this.dateShutDate.TabStop = false;
            // 
            // chkShutSingle
            // 
            this.chkShutSingle.Enabled = false;
            this.chkShutSingle.Location = new System.Drawing.Point(5, 17);
            this.chkShutSingle.MenuManager = this.barManager1;
            this.chkShutSingle.Name = "chkShutSingle";
            this.chkShutSingle.Properties.Caption = "ShutSingle";
            this.chkShutSingle.Size = new System.Drawing.Size(92, 19);
            this.chkShutSingle.TabIndex = 0;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(4, 330);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 696;
            this.labCompany.Text = "Company";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(416, 249);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(74, 14);
            this.labelControl8.TabIndex = 693;
            this.labelControl8.Text = "Measurement";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(416, 222);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 14);
            this.labelControl7.TabIndex = 690;
            this.labelControl7.Text = "Weight";
            // 
            // labPackages
            // 
            this.labPackages.Location = new System.Drawing.Point(416, 195);
            this.labPackages.Name = "labPackages";
            this.labPackages.Size = new System.Drawing.Size(50, 14);
            this.labPackages.TabIndex = 687;
            this.labPackages.Text = "Packages";
            // 
            // labBusinessDate
            // 
            this.labBusinessDate.Location = new System.Drawing.Point(617, 141);
            this.labBusinessDate.Name = "labBusinessDate";
            this.labBusinessDate.Size = new System.Drawing.Size(71, 14);
            this.labBusinessDate.TabIndex = 685;
            this.labBusinessDate.Text = "BusinessDate";
            // 
            // dateEdit3
            // 
            this.dateEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "OperationDate", true));
            this.dateEdit3.EditValue = null;
            this.dateEdit3.Location = new System.Drawing.Point(693, 138);
            this.dateEdit3.Name = "dateEdit3";
            this.dateEdit3.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dateEdit3.Properties.Appearance.Options.UseBackColor = true;
            this.dateEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit3.Properties.Mask.EditMask = "";
            this.dateEdit3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit3.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit3.Size = new System.Drawing.Size(103, 21);
            this.dateEdit3.TabIndex = 29;
            this.dateEdit3.TabStop = false;
            // 
            // labETA
            // 
            this.labETA.Location = new System.Drawing.Point(416, 141);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(23, 14);
            this.labETA.TabIndex = 683;
            this.labETA.Text = "ETA";
            // 
            // dteETA
            // 
            this.dteETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Eta", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteETA.EditValue = null;
            this.dteETA.Location = new System.Drawing.Point(513, 138);
            this.dteETA.Name = "dteETA";
            this.dteETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETA.Properties.Mask.EditMask = "";
            this.dteETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETA.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETA.Size = new System.Drawing.Size(98, 21);
            this.dteETA.TabIndex = 28;
            // 
            // labFETA
            // 
            this.labFETA.Location = new System.Drawing.Point(618, 114);
            this.labFETA.Name = "labFETA";
            this.labFETA.Size = new System.Drawing.Size(29, 14);
            this.labFETA.TabIndex = 681;
            this.labFETA.Text = "FETA";
            // 
            // dateFETA
            // 
            this.dateFETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Feta", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateFETA.EditValue = null;
            this.dateFETA.Location = new System.Drawing.Point(693, 111);
            this.dateFETA.Name = "dateFETA";
            this.dateFETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFETA.Properties.Mask.EditMask = "";
            this.dateFETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateFETA.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateFETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateFETA.Size = new System.Drawing.Size(102, 21);
            this.dateFETA.TabIndex = 27;
            this.dateFETA.TabStop = false;
            // 
            // labETD
            // 
            this.labETD.Location = new System.Drawing.Point(416, 114);
            this.labETD.Name = "labETD";
            this.labETD.Size = new System.Drawing.Size(23, 14);
            this.labETD.TabIndex = 679;
            this.labETD.Text = "ETD";
            // 
            // dteETD
            // 
            this.dteETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "Etd", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteETD.EditValue = null;
            this.dteETD.Location = new System.Drawing.Point(513, 111);
            this.dteETD.Name = "dteETD";
            this.dteETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETD.Properties.Mask.EditMask = "";
            this.dteETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETD.Size = new System.Drawing.Size(98, 21);
            this.dteETD.TabIndex = 26;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(416, 60);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(83, 14);
            this.labelControl6.TabIndex = 677;
            this.labelControl6.Text = "PlaceOfDelivery";
            // 
            // labDetination
            // 
            this.labDetination.Location = new System.Drawing.Point(416, 33);
            this.labDetination.Name = "labDetination";
            this.labDetination.Size = new System.Drawing.Size(56, 14);
            this.labDetination.TabIndex = 675;
            this.labDetination.Text = "Detination";
            // 
            // labDeparture
            // 
            this.labDeparture.Location = new System.Drawing.Point(416, 7);
            this.labDeparture.Name = "labDeparture";
            this.labDeparture.Size = new System.Drawing.Size(55, 14);
            this.labDeparture.TabIndex = 673;
            this.labDeparture.Text = "Departure";
            // 
            // treeSelectBox1
            // 
            this.treeSelectBox1.AllText = "Selecte ALL";
            this.treeSelectBox1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "SalesDepartmentID", true));
            this.treeSelectBox1.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.treeSelectBox1.Location = new System.Drawing.Point(101, 300);
            this.treeSelectBox1.Name = "treeSelectBox1";
            this.treeSelectBox1.ReadOnly = false;
            this.treeSelectBox1.Size = new System.Drawing.Size(284, 21);
            this.treeSelectBox1.SpecifiedBackColor = System.Drawing.Color.White;
            this.treeSelectBox1.TabIndex = 15;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(4, 303);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(53, 14);
            this.labelControl5.TabIndex = 671;
            this.labelControl5.Text = "Sales Dep";
            // 
            // cmbSales
            // 
            this.cmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsOtherInfo, "SalesName", true));
            this.cmbSales.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "SalesID", true));
            this.cmbSales.EditText = "";
            this.cmbSales.EditValue = null;
            this.cmbSales.Location = new System.Drawing.Point(101, 273);
            this.cmbSales.Name = "cmbSales";
            this.cmbSales.ReadOnly = false;
            this.cmbSales.RefreshButtonToolTip = "";
            this.cmbSales.ShowRefreshButton = false;
            this.cmbSales.Size = new System.Drawing.Size(284, 21);
            this.cmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbSales.TabIndex = 14;
            this.cmbSales.ToolTip = "";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(4, 276);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(27, 14);
            this.labelControl4.TabIndex = 669;
            this.labelControl4.Text = "Sales";
            // 
            // labGoods
            // 
            this.labGoods.Location = new System.Drawing.Point(416, 168);
            this.labGoods.Name = "labGoods";
            this.labGoods.Size = new System.Drawing.Size(34, 14);
            this.labGoods.TabIndex = 667;
            this.labGoods.Text = "Goods";
            // 
            // multiSearchCommonBox1
            // 
            this.multiSearchCommonBox1.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bsOtherInfo, "CarrierName", true));
            this.multiSearchCommonBox1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsOtherInfo, "CarrierID", true));
            this.multiSearchCommonBox1.EditText = "";
            this.multiSearchCommonBox1.EditValue = null;
            this.multiSearchCommonBox1.Location = new System.Drawing.Point(101, 246);
            this.multiSearchCommonBox1.Name = "multiSearchCommonBox1";
            this.multiSearchCommonBox1.ReadOnly = false;
            this.multiSearchCommonBox1.RefreshButtonToolTip = "";
            this.multiSearchCommonBox1.ShowRefreshButton = false;
            this.multiSearchCommonBox1.Size = new System.Drawing.Size(284, 21);
            this.multiSearchCommonBox1.SpecifiedBackColor = System.Drawing.Color.White;
            this.multiSearchCommonBox1.TabIndex = 13;
            this.multiSearchCommonBox1.ToolTip = "";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(4, 249);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(34, 14);
            this.labelControl3.TabIndex = 664;
            this.labelControl3.Text = "Carrier";
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(4, 222);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(81, 14);
            this.labAgentOfCarrier.TabIndex = 662;
            this.labAgentOfCarrier.Text = "AgentOfCarrier";
            // 
            // labNotify
            // 
            this.labNotify.Location = new System.Drawing.Point(4, 195);
            this.labNotify.Name = "labNotify";
            this.labNotify.Size = new System.Drawing.Size(60, 14);
            this.labNotify.TabIndex = 197;
            this.labNotify.Text = "NotifyParty";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 168);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 14);
            this.labelControl1.TabIndex = 195;
            this.labelControl1.Text = "Consignee";
            // 
            // labShiper
            // 
            this.labShiper.Location = new System.Drawing.Point(4, 141);
            this.labShiper.Name = "labShiper";
            this.labShiper.Size = new System.Drawing.Size(41, 14);
            this.labShiper.TabIndex = 193;
            this.labShiper.Text = "Shipper";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(216, 114);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 14);
            this.labelControl2.TabIndex = 143;
            this.labelControl2.Text = "HBL/HAWB NO";
            // 
            // labMBL
            // 
            this.labMBL.Location = new System.Drawing.Point(4, 114);
            this.labMBL.Name = "labMBL";
            this.labMBL.Size = new System.Drawing.Size(84, 14);
            this.labMBL.TabIndex = 142;
            this.labMBL.Text = "MBL/MAWB NO";
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(226, 7);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 39;
            this.labState.Text = "State";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(4, 6);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(17, 14);
            this.labNo.TabIndex = 0;
            this.labNo.Text = "NO";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(4, 60);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 0;
            this.labCustomer.Text = "Customer";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(4, 33);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.lwGridControl1);
            this.navBarGroupControlContainer2.Controls.Add(this.labCount);
            this.navBarGroupControlContainer2.Controls.Add(this.txtCount);
            this.navBarGroupControlContainer2.Controls.Add(this.btnDelete);
            this.navBarGroupControlContainer2.Controls.Add(this.btnAdd);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(808, 164);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // lwGridControl1
            // 
            this.lwGridControl1.DataSource = this.bsContainer;
            this.lwGridControl1.Location = new System.Drawing.Point(0, 26);
            this.lwGridControl1.MainView = this.gridView1;
            this.lwGridControl1.MenuManager = this.barManager1;
            this.lwGridControl1.Name = "lwGridControl1";
            this.lwGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemSpinEdit2,
            this.repositoryItemSpinEdit3,
            this.cmbBoxType,
            this.Unit});
            this.lwGridControl1.Size = new System.Drawing.Size(810, 102);
            this.lwGridControl1.TabIndex = 671;
            this.lwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bsContainer
            // 
            this.bsContainer.DataSource = typeof(ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ContainerList);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colTypeID,
            this.colSealNo,
            this.colSoNo,
            this.colQuantity,
            this.colQuantityName,
            this.colWeight,
            this.colMeasurement});
            this.gridView1.GridControl = this.lwGridControl1;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // colNo
            // 
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            // 
            // colTypeID
            // 
            this.colTypeID.ColumnEdit = this.cmbBoxType;
            this.colTypeID.FieldName = "TypeID";
            this.colTypeID.Name = "colTypeID";
            this.colTypeID.Visible = true;
            this.colTypeID.VisibleIndex = 1;
            // 
            // cmbBoxType
            // 
            this.cmbBoxType.AutoHeight = false;
            this.cmbBoxType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBoxType.Name = "cmbBoxType";
            // 
            // colSealNo
            // 
            this.colSealNo.FieldName = "SealNo";
            this.colSealNo.Name = "colSealNo";
            this.colSealNo.Visible = true;
            this.colSealNo.VisibleIndex = 2;
            // 
            // colSoNo
            // 
            this.colSoNo.FieldName = "SoNo";
            this.colSoNo.Name = "colSoNo";
            this.colSoNo.Visible = true;
            this.colSoNo.VisibleIndex = 3;
            // 
            // colQuantity
            // 
            this.colQuantity.ColumnEdit = this.repositoryItemSpinEdit3;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 4;
            // 
            // repositoryItemSpinEdit3
            // 
            this.repositoryItemSpinEdit3.AutoHeight = false;
            this.repositoryItemSpinEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit3.Name = "repositoryItemSpinEdit3";
            // 
            // colQuantityName
            // 
            this.colQuantityName.ColumnEdit = this.Unit;
            this.colQuantityName.FieldName = "QuantityUnitID";
            this.colQuantityName.Name = "colQuantityName";
            this.colQuantityName.Visible = true;
            this.colQuantityName.VisibleIndex = 5;
            // 
            // Unit
            // 
            this.Unit.AutoHeight = false;
            this.Unit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Unit.Name = "Unit";
            // 
            // colWeight
            // 
            this.colWeight.ColumnEdit = this.repositoryItemSpinEdit1;
            this.colWeight.DisplayFormat.FormatString = "N2";
            this.colWeight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 6;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // colMeasurement
            // 
            this.colMeasurement.ColumnEdit = this.repositoryItemSpinEdit2;
            this.colMeasurement.DisplayFormat.FormatString = "N2";
            this.colMeasurement.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 7;
            // 
            // repositoryItemSpinEdit2
            // 
            this.repositoryItemSpinEdit2.AutoHeight = false;
            this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            // 
            // labCount
            // 
            this.labCount.Location = new System.Drawing.Point(168, 6);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(85, 14);
            this.labCount.TabIndex = 670;
            this.labCount.Text = "ContainerCount";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(82, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(1, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // navBarcontainerInfo
            // 
            this.navBarcontainerInfo.Caption = "ContainerInfo";
            this.navBarcontainerInfo.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarcontainerInfo.Expanded = true;
            this.navBarcontainerInfo.GroupClientHeight = 166;
            this.navBarcontainerInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarcontainerInfo.Name = "navBarcontainerInfo";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 26);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageBase;
            this.xtraTabControl1.Size = new System.Drawing.Size(862, 854);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageBase});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // bar4
            // 
            this.bar4.BarName = "Status bar";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Status bar";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "navBarGroup1";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // OBBaseEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OBBaseEditPart";
            this.Size = new System.Drawing.Size(862, 880);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOtherInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerPopupContainerEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgentOfCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoods.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeparture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDetination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwPackages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPackages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeasurementUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsQuarantineInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommodityInspection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCustoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWare.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOperation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwNotifyParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mcmbOverseasFiler.Properties)).EndInit();
            this.tabPageBase.ResumeLayout(false);
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).EndInit();
            this.navBarControl2.ResumeLayout(false);
            this.navBarGroupControlContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).EndInit();
            this.groupLocalService.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateShutDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateShutDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShutSingle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoxType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Unit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageBase;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private ICP.FCM.OtherBusiness.UI.Common.CargoDescriptionPart cargoDescriptionPart1;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.XtraScrollableControl panelScroll;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup navBarcontainerInfo;
        private DevExpress.XtraEditors.PopupContainerEdit stxtCustomer;
        private DevExpress.XtraEditors.TextEdit txtState;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.TextEdit txtHBL;
        private DevExpress.XtraEditors.TextEdit txtMBL;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labMBL;
        private DevExpress.XtraEditors.LabelControl labShiper;
        private DevExpress.XtraEditors.LabelControl labNotify;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit customerPopupContainerEdit1;
        private DevExpress.XtraEditors.ButtonEdit stxtAgentOfCarrier;
        private DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        private DevExpress.XtraEditors.TextEdit txtGoods;
        private DevExpress.XtraEditors.LabelControl labGoods;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox multiSearchCommonBox1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbSales;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private ICP.Framework.ClientComponents.Controls.TreeSelectBox treeSelectBox1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ButtonEdit stxtDeparture;
        private DevExpress.XtraEditors.LabelControl labDeparture;
        private DevExpress.XtraEditors.ButtonEdit stxtDetination;
        private DevExpress.XtraEditors.LabelControl labDetination;
        private DevExpress.XtraEditors.ButtonEdit stxtDes;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labBusinessDate;
        private DevExpress.XtraEditors.DateEdit dateEdit3;
        private DevExpress.XtraEditors.LabelControl labETA;
        private DevExpress.XtraEditors.DateEdit dteETA;
        private DevExpress.XtraEditors.LabelControl labFETA;
        private DevExpress.XtraEditors.DateEdit dateFETA;
        private DevExpress.XtraEditors.LabelControl labETD;
        private DevExpress.XtraEditors.DateEdit dteETD;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit lwPackages;
        private DevExpress.XtraEditors.LabelControl labPackages;
        private DevExpress.XtraEditors.SpinEdit spinPackages;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbWeightUnit;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbMeasurementUnit;
        private DevExpress.XtraEditors.SpinEdit spinEdit2;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit chkShutSingle;
        private DevExpress.XtraEditors.LabelControl labShutDate;
        private DevExpress.XtraEditors.DateEdit dateShutDate;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraEditors.TextEdit txtCount;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.LabelControl labCount;
        private ICP.Framework.ClientComponents.Controls.LWGridControl lwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraEditors.TextEdit textEdit5;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private ICP.Common.UI.UCVoyageLookupEdit stxtVesselVoyage;
        private DevExpress.XtraEditors.LabelControl labVesselVoyage;
        private DevExpress.XtraEditors.LabelControl labOperation;
        private GroupBox groupLocalService;
        private DevExpress.XtraEditors.CheckEdit chkIsWarehouse;
        private DevExpress.XtraEditors.CheckEdit chkIsQuarantineInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsCommodityInspection;
        private DevExpress.XtraEditors.CheckEdit chkIsCustoms;
        private DevExpress.XtraEditors.CheckEdit chkIsTruck;
        private DevExpress.XtraEditors.ButtonEdit btnWare;
        private DevExpress.XtraEditors.ButtonEdit btnCustom;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.ButtonEdit stxtOperationNo;
        private BindingSource bsOtherInfo;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.MemoEdit memoRemark;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
        private ICP.FCM.OtherBusiness.UI.Business.OrderFeeEditPart orderFeeEditPart1;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtShipper;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit treeOperation;
        private DevExpress.XtraEditors.TextEdit textEdit6;
        private DevExpress.XtraEditors.LabelControl labSONo;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit lwNotifyParty;
        private BindingSource bsContainer;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colSealNo;
        private DevExpress.XtraGrid.Columns.GridColumn colSoNo;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityName;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeID;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbBoxType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox Unit;
        private DevExpress.XtraBars.BarButtonItem barTruck;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPaymentTerm;
        private DevExpress.XtraEditors.LabelControl labPaymentTerm;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbOverseasFiler;
        private DevExpress.XtraEditors.LabelControl labOverseasFiler;
    }
}

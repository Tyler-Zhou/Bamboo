using System.Windows.Forms;
namespace ICP.FCM.OceanImport.UI
{
    partial class OIBusinessTruckEdit
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
            this.bsTruckInfoList = new System.Windows.Forms.BindingSource(this.components);
            this.gcTruck = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTruckerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPickUpAtName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoadingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryAtName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTruckTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBase = new System.Windows.Forms.GroupBox();
            this.stxtBillToID = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.bsTruckInfoEdit = new System.Windows.Forms.BindingSource(this.components);
            this.stxtDeliveryAtID = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtPickUpAtID = new ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit();
            this.stxtTruckerID = new ICP.Business.Common.UI.BusinessContactPopupContainerEdit();
            this.dtpDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpPickUpDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpETA = new DevExpress.XtraEditors.DateEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barNew = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.txtCommodity = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.dtpCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labNO = new DevExpress.XtraEditors.LabelControl();
            this.labShippingOrderNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labTrucker = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateByName = new DevExpress.XtraEditors.TextEdit();
            this.txtVesselVoyage = new DevExpress.XtraEditors.TextEdit();
            this.txtCarrier = new DevExpress.XtraEditors.TextEdit();
            this.labVesselVoyage = new DevExpress.XtraEditors.LabelControl();
            this.txtSubNo = new DevExpress.XtraEditors.TextEdit();
            this.txtNO = new DevExpress.XtraEditors.TextEdit();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.groupContainer = new System.Windows.Forms.GroupBox();
            this.UCBoxList = new ICP.FCM.OceanImport.UI.Business.UCBusinessBoxList();
            this.panelScroll = new System.Windows.Forms.Panel();
            this.grbOther = new System.Windows.Forms.GroupBox();
            this.dteOrderDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTruck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.groupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBillToID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeliveryAtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPickUpAtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtTruckerID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateByName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNO.Properties)).BeginInit();
            this.groupContainer.SuspendLayout();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsTruckInfoList
            // 
            this.bsTruckInfoList.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OceanImportTruckInfo);
            this.bsTruckInfoList.CurrentChanged += new System.EventHandler(this.bsTruckInfoList_CurrentChanged);
            // 
            // gcTruck
            // 
            this.gcTruck.DataSource = this.bsTruckInfoList;
            this.gcTruck.Location = new System.Drawing.Point(3, 0);
            this.gcTruck.MainView = this.gvMain;
            this.gcTruck.Name = "gcTruck";
            this.gcTruck.Size = new System.Drawing.Size(719, 110);
            this.gcTruck.TabIndex = 3;
            this.gcTruck.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNO,
            this.colTruckerName,
            this.colPickUpAtName,
            this.colLoadingTime,
            this.colDeliveryAtName,
            this.colTruckTime,
            this.colCreateName,
            this.colCreateDate});
            this.gvMain.GridControl = this.gcTruck;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            // 
            // colNO
            // 
            this.colNO.Caption = "提货通知单号";
            this.colNO.FieldName = "NO";
            this.colNO.Name = "colNO";
            this.colNO.OptionsColumn.AllowEdit = false;
            this.colNO.Visible = true;
            this.colNO.VisibleIndex = 0;
            this.colNO.Width = 120;
            // 
            // colTruckerName
            // 
            this.colTruckerName.Caption = "拖车行";
            this.colTruckerName.FieldName = "TruckerName";
            this.colTruckerName.Name = "colTruckerName";
            this.colTruckerName.OptionsColumn.AllowEdit = false;
            this.colTruckerName.Visible = true;
            this.colTruckerName.VisibleIndex = 1;
            this.colTruckerName.Width = 150;
            // 
            // colPickUpAtName
            // 
            this.colPickUpAtName.Caption = "提货地";
            this.colPickUpAtName.FieldName = "PickUpAtName";
            this.colPickUpAtName.Name = "colPickUpAtName";
            this.colPickUpAtName.Visible = true;
            this.colPickUpAtName.VisibleIndex = 2;
            // 
            // colLoadingTime
            // 
            this.colLoadingTime.Caption = "提货日";
            this.colLoadingTime.FieldName = "PickUpDate";
            this.colLoadingTime.Name = "colLoadingTime";
            this.colLoadingTime.Visible = true;
            this.colLoadingTime.VisibleIndex = 3;
            // 
            // colDeliveryAtName
            // 
            this.colDeliveryAtName.Caption = "交货地";
            this.colDeliveryAtName.FieldName = "DeliveryAtName";
            this.colDeliveryAtName.Name = "colDeliveryAtName";
            this.colDeliveryAtName.Visible = true;
            this.colDeliveryAtName.VisibleIndex = 4;
            // 
            // colTruckTime
            // 
            this.colTruckTime.Caption = "交货时间";
            this.colTruckTime.FieldName = "DeliveryDate";
            this.colTruckTime.Name = "colTruckTime";
            this.colTruckTime.Visible = true;
            this.colTruckTime.VisibleIndex = 5;
            // 
            // colCreateName
            // 
            this.colCreateName.Caption = "创建人";
            this.colCreateName.FieldName = "CreateByName";
            this.colCreateName.Name = "colCreateName";
            this.colCreateName.Visible = true;
            this.colCreateName.VisibleIndex = 6;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 7;
            // 
            // groupBase
            // 
            this.groupBase.Controls.Add(this.stxtBillToID);
            this.groupBase.Controls.Add(this.stxtDeliveryAtID);
            this.groupBase.Controls.Add(this.stxtPickUpAtID);
            this.groupBase.Controls.Add(this.stxtTruckerID);
            this.groupBase.Controls.Add(this.dtpDeliveryDate);
            this.groupBase.Controls.Add(this.dtpPickUpDate);
            this.groupBase.Controls.Add(this.dtpETA);
            this.groupBase.Controls.Add(this.txtRemark);
            this.groupBase.Controls.Add(this.txtCommodity);
            this.groupBase.Controls.Add(this.labelControl9);
            this.groupBase.Controls.Add(this.labelControl8);
            this.groupBase.Controls.Add(this.dtpCreateDate);
            this.groupBase.Controls.Add(this.labelControl7);
            this.groupBase.Controls.Add(this.labelControl6);
            this.groupBase.Controls.Add(this.labelControl5);
            this.groupBase.Controls.Add(this.labelControl3);
            this.groupBase.Controls.Add(this.labNO);
            this.groupBase.Controls.Add(this.labShippingOrderNo);
            this.groupBase.Controls.Add(this.labelControl4);
            this.groupBase.Controls.Add(this.labCreateBy);
            this.groupBase.Controls.Add(this.labelControl2);
            this.groupBase.Controls.Add(this.labTrucker);
            this.groupBase.Controls.Add(this.labelControl1);
            this.groupBase.Controls.Add(this.txtCreateByName);
            this.groupBase.Controls.Add(this.txtVesselVoyage);
            this.groupBase.Controls.Add(this.txtCarrier);
            this.groupBase.Controls.Add(this.labVesselVoyage);
            this.groupBase.Controls.Add(this.txtSubNo);
            this.groupBase.Controls.Add(this.txtNO);
            this.groupBase.Controls.Add(this.labCarrier);
            this.groupBase.Location = new System.Drawing.Point(3, 108);
            this.groupBase.Name = "groupBase";
            this.groupBase.Size = new System.Drawing.Size(719, 255);
            this.groupBase.TabIndex = 1;
            this.groupBase.TabStop = false;
            // 
            // stxtBillToID
            // 
            this.stxtBillToID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfoList, "BillToID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtBillToID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "BillToName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtBillToID.Location = new System.Drawing.Point(93, 148);
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
            this.stxtBillToID.Size = new System.Drawing.Size(224, 21);
            this.stxtBillToID.TabIndex = 5;
            // 
            // bsTruckInfoEdit
            // 
            this.bsTruckInfoEdit.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OceanImportTruckInfo);
            // 
            // stxtDeliveryAtID
            // 
            this.stxtDeliveryAtID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfoEdit, "DeliveryAtID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtDeliveryAtID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "DeliveryAtName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtDeliveryAtID.Location = new System.Drawing.Point(93, 121);
            this.stxtDeliveryAtID.Name = "stxtDeliveryAtID";
            this.stxtDeliveryAtID.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtDeliveryAtID.Properties.ActionButtonIndex = 1;
            this.stxtDeliveryAtID.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtDeliveryAtID.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDeliveryAtID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtDeliveryAtID.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtDeliveryAtID.Properties.PopupSizeable = false;
            this.stxtDeliveryAtID.Properties.ShowPopupCloseButton = false;
            this.stxtDeliveryAtID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtDeliveryAtID.Size = new System.Drawing.Size(224, 21);
            this.stxtDeliveryAtID.TabIndex = 3;
            // 
            // stxtPickUpAtID
            // 
            this.stxtPickUpAtID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "PickUpAtName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPickUpAtID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfoEdit, "PickUpAtID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtPickUpAtID.Location = new System.Drawing.Point(93, 94);
            this.stxtPickUpAtID.Name = "stxtPickUpAtID";
            this.stxtPickUpAtID.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtPickUpAtID.Properties.ActionButtonIndex = 1;
            this.stxtPickUpAtID.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtPickUpAtID.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPickUpAtID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtPickUpAtID.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtPickUpAtID.Properties.PopupSizeable = false;
            this.stxtPickUpAtID.Properties.ShowPopupCloseButton = false;
            this.stxtPickUpAtID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtPickUpAtID.Size = new System.Drawing.Size(224, 21);
            this.stxtPickUpAtID.TabIndex = 1;
            // 
            // stxtTruckerID
            // 
            this.stxtTruckerID.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
            
            this.stxtTruckerID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTruckInfoEdit, "TruckerID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtTruckerID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "TruckerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtTruckerID.Location = new System.Drawing.Point(455, 67);
            this.stxtTruckerID.Name = "stxtTruckerID";
            this.stxtTruckerID.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.stxtTruckerID.Properties.ActionButtonIndex = 1;
            this.stxtTruckerID.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtTruckerID.Properties.Appearance.Options.UseBackColor = true;
            this.stxtTruckerID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.stxtTruckerID.Properties.CloseOnLostFocus = false;
            this.stxtTruckerID.Properties.CloseOnOuterMouseClick = false;
            this.stxtTruckerID.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtTruckerID.Properties.PopupSizeable = false;
            this.stxtTruckerID.Properties.ShowPopupCloseButton = false;
            this.stxtTruckerID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.stxtTruckerID.Size = new System.Drawing.Size(247, 21);
            this.stxtTruckerID.TabIndex = 0;
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfoEdit, "DeliveryDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpDeliveryDate.EditValue = null;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(455, 122);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dtpDeliveryDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtpDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDeliveryDate.Properties.Mask.EditMask = "";
            this.dtpDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtpDeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpDeliveryDate.Size = new System.Drawing.Size(247, 21);
            this.dtpDeliveryDate.TabIndex = 4;
            // 
            // dtpPickUpDate
            // 
            this.dtpPickUpDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfoEdit, "PickUpDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpPickUpDate.EditValue = null;
            this.dtpPickUpDate.Location = new System.Drawing.Point(455, 94);
            this.dtpPickUpDate.Name = "dtpPickUpDate";
            this.dtpPickUpDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dtpPickUpDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtpPickUpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpPickUpDate.Properties.Mask.EditMask = "";
            this.dtpPickUpDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtpPickUpDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpPickUpDate.Size = new System.Drawing.Size(247, 21);
            this.dtpPickUpDate.TabIndex = 2;
            // 
            // dtpETA
            // 
            this.dtpETA.EditValue = null;
            this.dtpETA.Enabled = false;
            this.dtpETA.Location = new System.Drawing.Point(93, 67);
            this.dtpETA.Name = "dtpETA";
            this.dtpETA.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dtpETA.Properties.Appearance.Options.UseBackColor = true;
            this.dtpETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpETA.Properties.Mask.EditMask = "";
            this.dtpETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtpETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpETA.Size = new System.Drawing.Size(224, 21);
            this.dtpETA.TabIndex = 4;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "Remark", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtRemark.Location = new System.Drawing.Point(93, 202);
            this.txtRemark.MenuManager = this.barManager1;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(609, 50);
            this.txtRemark.TabIndex = 9;
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
            this.barAdd,
            this.barDelete,
            this.barPrint,
            this.barClose,
            this.barNew,
            this.barRemove});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 8;
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
            this.barAdd.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 1;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "&Print";
            this.barPrint.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 3;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 4;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
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
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 899);
            this.barDockControlBottom.Size = new System.Drawing.Size(807, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 873);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(807, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 873);
            // 
            // barNew
            // 
            this.barNew.Caption = "New";
            this.barNew.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Add_16;
            this.barNew.Id = 6;
            this.barNew.Name = "barNew";
            // 
            // barRemove
            // 
            this.barRemove.Caption = "&Remove";
            this.barRemove.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 7;
            this.barRemove.Name = "barRemove";
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "Commodity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCommodity.Location = new System.Drawing.Point(93, 175);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Size = new System.Drawing.Size(609, 21);
            this.txtCommodity.TabIndex = 8;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 205);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 37;
            this.labelControl9.Text = "备注";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 178);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 37;
            this.labelControl8.Text = "品名";
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTruckInfoEdit, "CreateDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpCreateDate.EditValue = null;
            this.dtpCreateDate.Location = new System.Drawing.Point(587, 148);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dtpCreateDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtpCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpCreateDate.Properties.DisplayFormat.FormatString = "H";
            this.dtpCreateDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpCreateDate.Properties.EditFormat.FormatString = "H";
            this.dtpCreateDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpCreateDate.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.dtpCreateDate.Properties.Mask.EditMask = "";
            this.dtpCreateDate.Properties.ReadOnly = true;
            this.dtpCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpCreateDate.Size = new System.Drawing.Size(115, 21);
            this.dtpCreateDate.TabIndex = 7;
            this.dtpCreateDate.TabStop = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(548, 151);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(36, 14);
            this.labelControl7.TabIndex = 34;
            this.labelControl7.Text = "创建日";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 151);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 33;
            this.labelControl6.Text = "帐单寄送";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(377, 125);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 30;
            this.labelControl5.Text = "交货日";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(377, 97);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "提货日";
            // 
            // labNO
            // 
            this.labNO.Location = new System.Drawing.Point(12, 16);
            this.labNO.Name = "labNO";
            this.labNO.Size = new System.Drawing.Size(72, 14);
            this.labNO.TabIndex = 3;
            this.labNO.Text = "提货通知单号";
            // 
            // labShippingOrderNo
            // 
            this.labShippingOrderNo.Location = new System.Drawing.Point(12, 43);
            this.labShippingOrderNo.Name = "labShippingOrderNo";
            this.labShippingOrderNo.Size = new System.Drawing.Size(48, 14);
            this.labShippingOrderNo.TabIndex = 3;
            this.labShippingOrderNo.Text = "分提单号";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 125);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "交货地";
            // 
            // labCreateBy
            // 
            this.labCreateBy.Location = new System.Drawing.Point(377, 151);
            this.labCreateBy.Name = "labCreateBy";
            this.labCreateBy.Size = new System.Drawing.Size(36, 14);
            this.labCreateBy.TabIndex = 7;
            this.labCreateBy.Text = "创建人";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 97);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "提货地";
            // 
            // labTrucker
            // 
            this.labTrucker.Location = new System.Drawing.Point(377, 70);
            this.labTrucker.Name = "labTrucker";
            this.labTrucker.Size = new System.Drawing.Size(36, 14);
            this.labTrucker.TabIndex = 5;
            this.labTrucker.Text = "拖车行";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 70);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "离港日";
            // 
            // txtCreateByName
            // 
            this.txtCreateByName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "CreateByName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCreateByName.Enabled = false;
            this.txtCreateByName.Location = new System.Drawing.Point(455, 148);
            this.txtCreateByName.Name = "txtCreateByName";
            this.txtCreateByName.Properties.ReadOnly = true;
            this.txtCreateByName.Size = new System.Drawing.Size(87, 21);
            this.txtCreateByName.TabIndex = 6;
            this.txtCreateByName.TabStop = false;
            // 
            // txtVesselVoyage
            // 
            this.txtVesselVoyage.Enabled = false;
            this.txtVesselVoyage.Location = new System.Drawing.Point(455, 40);
            this.txtVesselVoyage.Name = "txtVesselVoyage";
            this.txtVesselVoyage.Properties.ReadOnly = true;
            this.txtVesselVoyage.Size = new System.Drawing.Size(247, 21);
            this.txtVesselVoyage.TabIndex = 3;
            this.txtVesselVoyage.TabStop = false;
            // 
            // txtCarrier
            // 
            this.txtCarrier.Enabled = false;
            this.txtCarrier.Location = new System.Drawing.Point(455, 13);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Properties.ReadOnly = true;
            this.txtCarrier.Size = new System.Drawing.Size(247, 21);
            this.txtCarrier.TabIndex = 1;
            this.txtCarrier.TabStop = false;
            // 
            // labVesselVoyage
            // 
            this.labVesselVoyage.Location = new System.Drawing.Point(377, 43);
            this.labVesselVoyage.Name = "labVesselVoyage";
            this.labVesselVoyage.Size = new System.Drawing.Size(48, 14);
            this.labVesselVoyage.TabIndex = 5;
            this.labVesselVoyage.Text = "船名航次";
            // 
            // txtSubNo
            // 
            this.txtSubNo.Enabled = false;
            this.txtSubNo.Location = new System.Drawing.Point(93, 41);
            this.txtSubNo.Name = "txtSubNo";
            this.txtSubNo.Properties.ReadOnly = true;
            this.txtSubNo.Size = new System.Drawing.Size(224, 21);
            this.txtSubNo.TabIndex = 2;
            this.txtSubNo.TabStop = false;
            // 
            // txtNO
            // 
            this.txtNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTruckInfoEdit, "NO", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNO.Enabled = false;
            this.txtNO.Location = new System.Drawing.Point(93, 14);
            this.txtNO.Name = "txtNO";
            this.txtNO.Properties.ReadOnly = true;
            this.txtNO.Size = new System.Drawing.Size(224, 21);
            this.txtNO.TabIndex = 0;
            this.txtNO.TabStop = false;
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(377, 16);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(36, 14);
            this.labCarrier.TabIndex = 7;
            this.labCarrier.Text = "船公司";
            // 
            // groupContainer
            // 
            this.groupContainer.Controls.Add(this.UCBoxList);
            this.groupContainer.Location = new System.Drawing.Point(3, 365);
            this.groupContainer.Name = "groupContainer";
            this.groupContainer.Size = new System.Drawing.Size(719, 197);
            this.groupContainer.TabIndex = 1;
            this.groupContainer.TabStop = false;
            this.groupContainer.Text = "Container";
            // 
            // UCBoxList
            // 
            this.UCBoxList.BaseMultiLanguageList = null;
            this.UCBoxList.BasePartList = null;
            this.UCBoxList.BusinessID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UCBoxList.CodeValuePairs = null;
            this.UCBoxList.ControlsList = null;
            this.UCBoxList.DataSource = null;
            this.UCBoxList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UCBoxList.FormName = "UCBusinessBoxList";
            this.UCBoxList.IsChanged = false;
            this.UCBoxList.IsMultiLanguage = true;
            this.UCBoxList.Location = new System.Drawing.Point(3, 18);
            this.UCBoxList.MBLID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UCBoxList.Name = "UCBoxList";
            this.UCBoxList.Resources = null;
            this.UCBoxList.Size = new System.Drawing.Size(713, 176);
            this.UCBoxList.TabIndex = 0;
            this.UCBoxList.UsedMessages = null;
            // 
            // panelScroll
            // 
            this.panelScroll.AllowDrop = true;
            this.panelScroll.AutoScroll = true;
            this.panelScroll.Controls.Add(this.grbOther);
            this.panelScroll.Controls.Add(this.groupContainer);
            this.panelScroll.Controls.Add(this.gcTruck);
            this.panelScroll.Controls.Add(this.groupBase);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 26);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(807, 873);
            this.panelScroll.TabIndex = 6;
            // 
            // grbOther
            // 
            this.grbOther.Location = new System.Drawing.Point(6, 567);
            this.grbOther.Name = "grbOther";
            this.grbOther.Size = new System.Drawing.Size(716, 293);
            this.grbOther.TabIndex = 1;
            this.grbOther.TabStop = false;
            this.grbOther.Text = "Contact Info";
            // 
            // dteOrderDate
            // 
            this.dteOrderDate.EditValue = null;
            this.dteOrderDate.Location = new System.Drawing.Point(93, 67);
            this.dteOrderDate.Name = "dteOrderDate";
            this.dteOrderDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dteOrderDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteOrderDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteOrderDate.Properties.Mask.EditMask = "";
            this.dteOrderDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteOrderDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteOrderDate.Size = new System.Drawing.Size(224, 21);
            this.dteOrderDate.TabIndex = 141;
            // 
            // OIBusinessTruckEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OIBusinessTruckEdit";
            this.Size = new System.Drawing.Size(807, 899);
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTruck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.groupBase.ResumeLayout(false);
            this.groupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBillToID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTruckInfoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDeliveryAtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPickUpAtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtTruckerID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDeliveryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickUpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateByName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNO.Properties)).EndInit();
            this.groupContainer.ResumeLayout(false);
            this.panelScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOrderDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcTruck;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private System.Windows.Forms.GroupBox groupBase;
        private System.Windows.Forms.GroupBox groupContainer;
        private DevExpress.XtraEditors.LabelControl labShippingOrderNo;
        private DevExpress.XtraEditors.LabelControl labTrucker;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.LabelControl labVesselVoyage;
        private DevExpress.XtraEditors.TextEdit txtCreateByName;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.TextEdit txtVesselVoyage;
        private DevExpress.XtraEditors.TextEdit txtCarrier;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckerName;
        private Panel panelScroll;
        private BindingSource bsTruckInfoList;
        private DevExpress.XtraBars.BarButtonItem barNew;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraGrid.Columns.GridColumn colNO;
        private DevExpress.XtraEditors.LabelControl labNO;
        private DevExpress.XtraEditors.TextEdit txtNO;
        private ICP.FCM.OceanImport.UI.Business.UCBusinessBoxList UCBoxList;
        private DevExpress.XtraGrid.Columns.GridColumn colPickUpAtName;
        private DevExpress.XtraGrid.Columns.GridColumn colLoadingTime;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryAtName;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckTime;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.TextEdit txtSubNo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit dtpCreateDate;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtCommodity;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public BindingSource bsTruckInfoEdit;
        private DevExpress.XtraEditors.DateEdit dtpPickUpDate;
        private DevExpress.XtraEditors.DateEdit dtpETA;
        private DevExpress.XtraEditors.DateEdit dteOrderDate;
        private DevExpress.XtraEditors.DateEdit dtpDeliveryDate;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtBillToID;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtDeliveryAtID;
        private ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit stxtPickUpAtID;
        private ICP.Business.Common.UI.BusinessContactPopupContainerEdit stxtTruckerID;
        private GroupBox grbOther;
    }
}

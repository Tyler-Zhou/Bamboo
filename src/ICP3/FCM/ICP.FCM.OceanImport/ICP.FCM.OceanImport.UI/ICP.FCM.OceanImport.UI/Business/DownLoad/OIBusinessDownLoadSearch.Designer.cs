using ICP.Framework.ClientComponents.Controls;
namespace ICP.FCM.OceanImport.UI
{
    partial class OIBusinessDownLoadSearch
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

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
            this.bcMain = new DevExpress.XtraNavBar.NavBarControl();
            this.bgBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbDocState = new LWImageComboBoxEdit();
            this.lblDispatchDoc = new DevExpress.XtraEditors.LabelControl();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbAgentID = new LWImageComboBoxEdit();
            this.cmbCompanyID = new LWImageComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.labAgentOfCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labCtnNo = new DevExpress.XtraEditors.LabelControl();
            this.labBLNO = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.txtBLNo = new DevExpress.XtraEditors.TextEdit();
            this.txtBoxNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbState = new LWImageComboBoxEdit();
            this.txtConsignee = new DevExpress.XtraEditors.TextEdit();
            this.txtPOL = new DevExpress.XtraEditors.TextEdit();
            this.txtVesselName = new DevExpress.XtraEditors.TextEdit();
            this.txtPOD = new DevExpress.XtraEditors.TextEdit();
            this.txtVoyage = new DevExpress.XtraEditors.TextEdit();
            this.txtPlaceOfDelivery = new DevExpress.XtraEditors.TextEdit();
            this.bgcDate = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.txtBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbDateType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.bgDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cmbReleaseCarogo = new LWImageComboBoxEdit();
            this.labReleaseCargo = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).BeginInit();
            this.bcMain.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDelivery.Properties)).BeginInit();
            this.bgcDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseCarogo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bcMain
            // 
            this.bcMain.ActiveGroup = this.bgBase;
            this.bcMain.Controls.Add(this.bgcBase);
            this.bcMain.Controls.Add(this.bgcDate);
            this.bcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.bcMain.ExplorerBarGroupInterval = 2;
            this.bcMain.ExplorerBarGroupOuterIndent = 2;
            this.bcMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgBase,
            this.bgDate});
            this.bcMain.Location = new System.Drawing.Point(0, 0);
            this.bcMain.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.bcMain.Name = "bcMain";
            this.bcMain.OptionsNavPane.ExpandedWidth = 140;
            this.bcMain.Size = new System.Drawing.Size(201, 516);
            this.bcMain.TabIndex = 1;
            this.bcMain.Text = "navBarControl1";
            // 
            // bgBase
            // 
            this.bgBase.Caption = "基础";
            this.bgBase.ControlContainer = this.bgcBase;
            this.bgBase.Expanded = true;
            this.bgBase.GroupClientHeight = 372;
            this.bgBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBase.Name = "bgBase";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.cmbReleaseCarogo);
            this.bgcBase.Controls.Add(this.cmbDocState);
            this.bgcBase.Controls.Add(this.labReleaseCargo);
            this.bgcBase.Controls.Add(this.lblDispatchDoc);
            this.bgcBase.Controls.Add(this.numMax);
            this.bgcBase.Controls.Add(this.labelControl2);
            this.bgcBase.Controls.Add(this.cmbAgentID);
            this.bgcBase.Controls.Add(this.cmbCompanyID);
            this.bgcBase.Controls.Add(this.labelControl5);
            this.bgcBase.Controls.Add(this.labPlaceOfDelivery);
            this.bgcBase.Controls.Add(this.labelControl4);
            this.bgcBase.Controls.Add(this.labPOD);
            this.bgcBase.Controls.Add(this.labelControl1);
            this.bgcBase.Controls.Add(this.labPOL);
            this.bgcBase.Controls.Add(this.labAgentOfCarrier);
            this.bgcBase.Controls.Add(this.labCustomer);
            this.bgcBase.Controls.Add(this.labCtnNo);
            this.bgcBase.Controls.Add(this.labBLNO);
            this.bgcBase.Controls.Add(this.labCompany);
            this.bgcBase.Controls.Add(this.txtBLNo);
            this.bgcBase.Controls.Add(this.txtBoxNo);
            this.bgcBase.Controls.Add(this.cmbState);
            this.bgcBase.Controls.Add(this.txtConsignee);
            this.bgcBase.Controls.Add(this.txtPOL);
            this.bgcBase.Controls.Add(this.txtVesselName);
            this.bgcBase.Controls.Add(this.txtPOD);
            this.bgcBase.Controls.Add(this.txtVoyage);
            this.bgcBase.Controls.Add(this.txtPlaceOfDelivery);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(193, 370);
            this.bgcBase.TabIndex = 0;
            // 
            // cmbDocState
            // 
            this.cmbDocState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDocState.Location = new System.Drawing.Point(66, 317);
            this.cmbDocState.Name = "cmbDocState";
            this.cmbDocState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbDocState.Properties.Appearance.Options.UseBackColor = true;
            this.cmbDocState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDocState.Size = new System.Drawing.Size(104, 21);
            this.cmbDocState.TabIndex = 16;
            // 
            // lblDispatchDoc
            // 
            this.lblDispatchDoc.Location = new System.Drawing.Point(6, 319);
            this.lblDispatchDoc.Name = "lblDispatchDoc";
            this.lblDispatchDoc.Size = new System.Drawing.Size(48, 14);
            this.lblDispatchDoc.TabIndex = 15;
            this.lblDispatchDoc.Text = "文件状态";
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(66, 289);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = "N00";
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMax.Size = new System.Drawing.Size(104, 21);
            this.numMax.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 292);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "最大行数";
            // 
            // cmbAgentID
            // 
            this.cmbAgentID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAgentID.Location = new System.Drawing.Point(66, 211);
            this.cmbAgentID.Name = "cmbAgentID";
            this.cmbAgentID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbAgentID.Properties.Appearance.Options.UseBackColor = true;
            this.cmbAgentID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAgentID.Size = new System.Drawing.Size(104, 21);
            this.cmbAgentID.TabIndex = 10;
            // 
            // cmbCompanyID
            // 
            this.cmbCompanyID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompanyID.Location = new System.Drawing.Point(66, 237);
            this.cmbCompanyID.Name = "cmbCompanyID";
            this.cmbCompanyID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompanyID.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompanyID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompanyID.Size = new System.Drawing.Size(104, 21);
            this.cmbCompanyID.TabIndex = 11;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 84);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "航次";
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(5, 162);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(36, 14);
            this.labPlaceOfDelivery.TabIndex = 1;
            this.labPlaceOfDelivery.Text = "交货地";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 58);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "船名";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(5, 136);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(36, 14);
            this.labPOD.TabIndex = 1;
            this.labPOD.Text = "卸货港";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 188);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "收货人";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(5, 110);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(36, 14);
            this.labPOL.TabIndex = 1;
            this.labPOL.Text = "装货港";
            // 
            // labAgentOfCarrier
            // 
            this.labAgentOfCarrier.Location = new System.Drawing.Point(5, 214);
            this.labAgentOfCarrier.Name = "labAgentOfCarrier";
            this.labAgentOfCarrier.Size = new System.Drawing.Size(48, 14);
            this.labAgentOfCarrier.TabIndex = 1;
            this.labAgentOfCarrier.Text = "代理公司";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(5, 266);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(24, 14);
            this.labCustomer.TabIndex = 1;
            this.labCustomer.Text = "状态";
            // 
            // labCtnNo
            // 
            this.labCtnNo.Location = new System.Drawing.Point(5, 32);
            this.labCtnNo.Name = "labCtnNo";
            this.labCtnNo.Size = new System.Drawing.Size(24, 14);
            this.labCtnNo.TabIndex = 1;
            this.labCtnNo.Text = "箱号";
            // 
            // labBLNO
            // 
            this.labBLNO.Location = new System.Drawing.Point(5, 6);
            this.labBLNO.Name = "labBLNO";
            this.labBLNO.Size = new System.Drawing.Size(36, 14);
            this.labBLNO.TabIndex = 1;
            this.labBLNO.Text = "提单号";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(5, 240);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(48, 14);
            this.labCompany.TabIndex = 1;
            this.labCompany.Text = "操作口岸";
            // 
            // txtBLNo
            // 
            this.txtBLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBLNo.Location = new System.Drawing.Point(66, 3);
            this.txtBLNo.Name = "txtBLNo";
            this.txtBLNo.Size = new System.Drawing.Size(104, 21);
            this.txtBLNo.TabIndex = 2;
            // 
            // txtBoxNo
            // 
            this.txtBoxNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxNo.Location = new System.Drawing.Point(66, 29);
            this.txtBoxNo.Name = "txtBoxNo";
            this.txtBoxNo.Size = new System.Drawing.Size(104, 21);
            this.txtBoxNo.TabIndex = 3;
            // 
            // cmbState
            // 
            this.cmbState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbState.Location = new System.Drawing.Point(66, 263);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbState.Properties.Appearance.Options.UseBackColor = true;
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Size = new System.Drawing.Size(104, 21);
            this.cmbState.TabIndex = 12;
            // 
            // txtConsignee
            // 
            this.txtConsignee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsignee.Location = new System.Drawing.Point(66, 185);
            this.txtConsignee.Name = "txtConsignee";
            this.txtConsignee.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtConsignee.Properties.Appearance.Options.UseBackColor = true;
            this.txtConsignee.Size = new System.Drawing.Size(104, 21);
            this.txtConsignee.TabIndex = 8;
            // 
            // txtPOL
            // 
            this.txtPOL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOL.Location = new System.Drawing.Point(66, 107);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPOL.Properties.Appearance.Options.UseBackColor = true;
            this.txtPOL.Size = new System.Drawing.Size(104, 21);
            this.txtPOL.TabIndex = 9;
            // 
            // txtVesselName
            // 
            this.txtVesselName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVesselName.Location = new System.Drawing.Point(66, 55);
            this.txtVesselName.Name = "txtVesselName";
            this.txtVesselName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtVesselName.Properties.Appearance.Options.UseBackColor = true;
            this.txtVesselName.Size = new System.Drawing.Size(104, 21);
            this.txtVesselName.TabIndex = 12;
            // 
            // txtPOD
            // 
            this.txtPOD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOD.Location = new System.Drawing.Point(66, 133);
            this.txtPOD.Name = "txtPOD";
            this.txtPOD.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPOD.Properties.Appearance.Options.UseBackColor = true;
            this.txtPOD.Size = new System.Drawing.Size(104, 21);
            this.txtPOD.TabIndex = 10;
            // 
            // txtVoyage
            // 
            this.txtVoyage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVoyage.Location = new System.Drawing.Point(66, 81);
            this.txtVoyage.Name = "txtVoyage";
            this.txtVoyage.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtVoyage.Properties.Appearance.Options.UseBackColor = true;
            this.txtVoyage.Size = new System.Drawing.Size(104, 21);
            this.txtVoyage.TabIndex = 13;
            // 
            // txtPlaceOfDelivery
            // 
            this.txtPlaceOfDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlaceOfDelivery.Location = new System.Drawing.Point(66, 159);
            this.txtPlaceOfDelivery.Name = "txtPlaceOfDelivery";
            this.txtPlaceOfDelivery.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPlaceOfDelivery.Properties.Appearance.Options.UseBackColor = true;
            this.txtPlaceOfDelivery.Size = new System.Drawing.Size(104, 21);
            this.txtPlaceOfDelivery.TabIndex = 11;
            // 
            // bgcDate
            // 
            this.bgcDate.Controls.Add(this.txtEndDate);
            this.bgcDate.Controls.Add(this.txtBeginDate);
            this.bgcDate.Controls.Add(this.cmbDateType);
            this.bgcDate.Controls.Add(this.labTo);
            this.bgcDate.Controls.Add(this.labFrom);
            this.bgcDate.Name = "bgcDate";
            this.bgcDate.Size = new System.Drawing.Size(193, 87);
            this.bgcDate.TabIndex = 1;
            // 
            // txtEndDate
            // 
            this.txtEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndDate.EditValue = null;
            this.txtEndDate.Enabled = false;
            this.txtEndDate.Location = new System.Drawing.Point(66, 56);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtEndDate.Size = new System.Drawing.Size(104, 21);
            this.txtEndDate.TabIndex = 0;
            // 
            // txtBeginDate
            // 
            this.txtBeginDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBeginDate.EditValue = null;
            this.txtBeginDate.Enabled = false;
            this.txtBeginDate.Location = new System.Drawing.Point(66, 31);
            this.txtBeginDate.Name = "txtBeginDate";
            this.txtBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBeginDate.Size = new System.Drawing.Size(104, 21);
            this.txtBeginDate.TabIndex = 1;
            // 
            // cmbDateType
            // 
            this.cmbDateType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDateType.Location = new System.Drawing.Point(5, 4);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbDateType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbDateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDateType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbDateType.Size = new System.Drawing.Size(168, 21);
            this.cmbDateType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbDateType.TabIndex = 2;
            this.cmbDateType.EditValueChanged += new System.EventHandler(this.cmbDateType_EditValueChanged);
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(8, 59);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(48, 14);
            this.labTo.TabIndex = 13;
            this.labTo.Text = "结束时间";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(8, 34);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(48, 14);
            this.labFrom.TabIndex = 12;
            this.labFrom.Text = "开始时间";
            // 
            // bgDate
            // 
            this.bgDate.Caption = "日期";
            this.bgDate.ControlContainer = this.bgcDate;
            this.bgDate.Expanded = true;
            this.bgDate.GroupClientHeight = 89;
            this.bgDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgDate.Name = "bgDate";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 519);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(201, 43);
            this.panelControl1.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(38, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(67, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空(&L)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(124, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(67, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.bcMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(201, 519);
            this.pnlMain.TabIndex = 0;
            // 
            // cmbReleaseCarogo
            // 
            this.cmbReleaseCarogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReleaseCarogo.Location = new System.Drawing.Point(65, 341);
            this.cmbReleaseCarogo.Name = "cmbReleaseCarogo";
            this.cmbReleaseCarogo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbReleaseCarogo.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReleaseCarogo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseCarogo.Size = new System.Drawing.Size(105, 21);
            this.cmbReleaseCarogo.TabIndex = 18;
            // 
            // labReleaseCargo
            // 
            this.labReleaseCargo.Location = new System.Drawing.Point(5, 343);
            this.labReleaseCargo.Name = "labReleaseCargo";
            this.labReleaseCargo.Size = new System.Drawing.Size(48, 14);
            this.labReleaseCargo.TabIndex = 17;
            this.labReleaseCargo.Text = "放单状态";
            // 
            // OIBusinessDownLoadSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panelControl1);
            this.Name = "OIBusinessDownLoadSearch";
            this.Size = new System.Drawing.Size(201, 562);
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).EndInit();
            this.bcMain.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaceOfDelivery.Properties)).EndInit();
            this.bgcDate.ResumeLayout(false);
            this.bgcDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseCarogo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraNavBar.NavBarControl bcMain;
        protected DevExpress.XtraNavBar.NavBarGroup bgBase;
        protected DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        protected DevExpress.XtraEditors.LabelControl labBLNO;
        protected DevExpress.XtraEditors.TextEdit txtBLNo;
        protected DevExpress.XtraEditors.LabelControl labCtnNo;
        protected DevExpress.XtraNavBar.NavBarGroupControlContainer bgcDate;
        protected DevExpress.XtraNavBar.NavBarGroup bgDate;
        protected LWImageComboBoxEdit cmbDateType;
        protected DevExpress.XtraEditors.LabelControl labCompany;
        protected DevExpress.XtraEditors.TextEdit txtBoxNo;
        protected DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        protected DevExpress.XtraEditors.LabelControl labPOD;
        protected DevExpress.XtraEditors.LabelControl labPOL;
        protected DevExpress.XtraEditors.LabelControl labAgentOfCarrier;
        protected DevExpress.XtraEditors.LabelControl labCustomer;
        protected DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.SimpleButton btnClear;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        protected System.Windows.Forms.Panel pnlMain;
        protected DevExpress.XtraEditors.TextEdit txtPOL;
        protected DevExpress.XtraEditors.TextEdit txtPOD;
        protected DevExpress.XtraEditors.TextEdit txtPlaceOfDelivery;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        protected DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.TextEdit txtConsignee;
        protected DevExpress.XtraEditors.LabelControl labelControl5;
        protected DevExpress.XtraEditors.LabelControl labelControl4;
        protected DevExpress.XtraEditors.TextEdit txtVesselName;
        protected DevExpress.XtraEditors.TextEdit txtVoyage;
        protected LWImageComboBoxEdit cmbState;
        private LWImageComboBoxEdit cmbCompanyID;
        private LWImageComboBoxEdit cmbAgentID;
        private DevExpress.XtraEditors.DateEdit txtBeginDate;
        private DevExpress.XtraEditors.DateEdit txtEndDate;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblDispatchDoc;
        protected LWImageComboBoxEdit cmbDocState;
        protected LWImageComboBoxEdit cmbReleaseCarogo;
        private DevExpress.XtraEditors.LabelControl labReleaseCargo;
    }
}


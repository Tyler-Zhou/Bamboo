namespace ICP.FCM.DomesticTrade.UI.Order
{
    partial class OrderSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderSearchPart));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelChecking = new System.Windows.Forms.Panel();
            this.lblFinished = new DevExpress.XtraEditors.LabelControl();
            this.lblReject = new DevExpress.XtraEditors.LabelControl();
            this.lblCancelled = new DevExpress.XtraEditors.LabelControl();
            this.treeBoxSalesDep = new ICP.Framework.ClientComponents.Controls.TreeCheckBox();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.mcmbCarrier = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.cmbState = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labDestination = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labSalesDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.txtOperationNo = new DevExpress.XtraEditors.TextEdit();
            this.stxtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.stxtPOL = new DevExpress.XtraEditors.TextEdit();
            this.stxtPOD = new DevExpress.XtraEditors.TextEdit();
            this.stxtDestination = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.fromToDateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.cmbDateSearchType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDestination.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDate});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(238, 584);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 330;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.panel3);
            this.navBarGroupBase.Controls.Add(this.panel2);
            this.navBarGroupBase.Controls.Add(this.panelChecking);
            this.navBarGroupBase.Controls.Add(this.lblFinished);
            this.navBarGroupBase.Controls.Add(this.lblReject);
            this.navBarGroupBase.Controls.Add(this.lblCancelled);
            this.navBarGroupBase.Controls.Add(this.treeBoxSalesDep);
            this.navBarGroupBase.Controls.Add(this.mcmbSales);
            this.navBarGroupBase.Controls.Add(this.mcmbCarrier);
            this.navBarGroupBase.Controls.Add(this.lwchkIsValid);
            this.navBarGroupBase.Controls.Add(this.labIsValid);
            this.navBarGroupBase.Controls.Add(this.cmbState);
            this.navBarGroupBase.Controls.Add(this.numMax);
            this.navBarGroupBase.Controls.Add(this.labState);
            this.navBarGroupBase.Controls.Add(this.labSales);
            this.navBarGroupBase.Controls.Add(this.labDestination);
            this.navBarGroupBase.Controls.Add(this.labPOD);
            this.navBarGroupBase.Controls.Add(this.labPOL);
            this.navBarGroupBase.Controls.Add(this.labCarrier);
            this.navBarGroupBase.Controls.Add(this.labCustomer);
            this.navBarGroupBase.Controls.Add(this.labMax);
            this.navBarGroupBase.Controls.Add(this.labSalesDepartment);
            this.navBarGroupBase.Controls.Add(this.labOperationNo);
            this.navBarGroupBase.Controls.Add(this.txtOperationNo);
            this.navBarGroupBase.Controls.Add(this.stxtCustomer);
            this.navBarGroupBase.Controls.Add(this.stxtPOL);
            this.navBarGroupBase.Controls.Add(this.stxtPOD);
            this.navBarGroupBase.Controls.Add(this.stxtDestination);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(214, 328);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(179, 299);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(14, 14);
            this.panel3.TabIndex = 44;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Location = new System.Drawing.Point(114, 299);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(14, 14);
            this.panel2.TabIndex = 43;
            // 
            // panelChecking
            // 
            this.panelChecking.BackColor = System.Drawing.Color.Gray;
            this.panelChecking.Location = new System.Drawing.Point(50, 299);
            this.panelChecking.Name = "panelChecking";
            this.panelChecking.Size = new System.Drawing.Size(14, 14);
            this.panelChecking.TabIndex = 42;
            // 
            // lblFinished
            // 
            this.lblFinished.Location = new System.Drawing.Point(137, 299);
            this.lblFinished.Name = "lblFinished";
            this.lblFinished.Size = new System.Drawing.Size(36, 14);
            this.lblFinished.TabIndex = 41;
            this.lblFinished.Text = "已完成";
            // 
            // lblReject
            // 
            this.lblReject.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblReject.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblReject.Appearance.Options.UseBackColor = true;
            this.lblReject.Appearance.Options.UseForeColor = true;
            this.lblReject.Location = new System.Drawing.Point(72, 299);
            this.lblReject.Name = "lblReject";
            this.lblReject.Size = new System.Drawing.Size(36, 14);
            this.lblReject.TabIndex = 40;
            this.lblReject.Text = "已打回";
            // 
            // lblCancelled
            // 
            this.lblCancelled.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Strikeout);
            this.lblCancelled.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblCancelled.Appearance.Options.UseFont = true;
            this.lblCancelled.Appearance.Options.UseForeColor = true;
            this.lblCancelled.Location = new System.Drawing.Point(8, 299);
            this.lblCancelled.Name = "lblCancelled";
            this.lblCancelled.Size = new System.Drawing.Size(36, 14);
            this.lblCancelled.TabIndex = 39;
            this.lblCancelled.Text = "已取消";
            // 
            // treeBoxSalesDep
            // 
            this.treeBoxSalesDep.AllText = "Selecte ALL";
            this.treeBoxSalesDep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBoxSalesDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeBoxSalesDep.EditValue")));
            this.treeBoxSalesDep.Location = new System.Drawing.Point(80, 6);
            this.treeBoxSalesDep.Name = "treeBoxSalesDep";
            this.treeBoxSalesDep.ReadOnly = false;
            this.treeBoxSalesDep.Size = new System.Drawing.Size(129, 21);
            this.treeBoxSalesDep.TabIndex = 18;
            // 
            // mcmbSales
            // 
            this.mcmbSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbSales.EditText = "";
            this.mcmbSales.EditValue = null;
            this.mcmbSales.Location = new System.Drawing.Point(80, 188);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.ReadOnly = false;
            this.mcmbSales.Size = new System.Drawing.Size(129, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 17;
            // 
            // mcmbCarrier
            // 
            this.mcmbCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbCarrier.EditText = "";
            this.mcmbCarrier.EditValue = null;
            this.mcmbCarrier.Location = new System.Drawing.Point(80, 84);
            this.mcmbCarrier.Name = "mcmbCarrier";
            this.mcmbCarrier.ReadOnly = false;
            this.mcmbCarrier.Size = new System.Drawing.Size(129, 21);
            this.mcmbCarrier.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbCarrier.TabIndex = 16;
            // 
            // lwchkIsValid
            // 
            this.lwchkIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Location = new System.Drawing.Point(80, 240);
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.Size = new System.Drawing.Size(129, 21);
            this.lwchkIsValid.TabIndex = 10;
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // labIsValid
            // 
            this.labIsValid.Location = new System.Drawing.Point(7, 244);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(34, 14);
            this.labIsValid.TabIndex = 11;
            this.labIsValid.Text = "IsValid";
            // 
            // cmbState
            // 
            this.cmbState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbState.Location = new System.Drawing.Point(80, 214);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbState.Properties.Appearance.Options.UseBackColor = true;
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbState.Size = new System.Drawing.Size(129, 21);
            this.cmbState.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbState.TabIndex = 8;
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
            this.numMax.Location = new System.Drawing.Point(80, 266);
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
            this.numMax.Size = new System.Drawing.Size(129, 21);
            this.numMax.TabIndex = 9;
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(7, 218);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(30, 14);
            this.labState.TabIndex = 1;
            this.labState.Text = "State";
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(7, 192);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(27, 14);
            this.labSales.TabIndex = 1;
            this.labSales.Text = "Sales";
            // 
            // labDestination
            // 
            this.labDestination.Location = new System.Drawing.Point(7, 166);
            this.labDestination.Name = "labDestination";
            this.labDestination.Size = new System.Drawing.Size(61, 14);
            this.labDestination.TabIndex = 1;
            this.labDestination.Text = "Destination";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(7, 140);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 1;
            this.labPOD.Text = "POD";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(7, 114);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 1;
            this.labPOL.Text = "POL";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(7, 88);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 1;
            this.labCarrier.Text = "Carrier";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(7, 62);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 1;
            this.labCustomer.Text = "Customer";
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(7, 270);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(58, 14);
            this.labMax.TabIndex = 1;
            this.labMax.Text = "Max Count";
            // 
            // labSalesDepartment
            // 
            this.labSalesDepartment.Location = new System.Drawing.Point(7, 10);
            this.labSalesDepartment.Name = "labSalesDepartment";
            this.labSalesDepartment.Size = new System.Drawing.Size(53, 14);
            this.labSalesDepartment.TabIndex = 1;
            this.labSalesDepartment.Text = "Sales Dep";
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(7, 36);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(69, 14);
            this.labOperationNo.TabIndex = 1;
            this.labOperationNo.Text = "OperationNo";
            // 
            // txtOperationNo
            // 
            this.txtOperationNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperationNo.Location = new System.Drawing.Point(80, 32);
            this.txtOperationNo.Name = "txtOperationNo";
            this.txtOperationNo.Size = new System.Drawing.Size(129, 21);
            this.txtOperationNo.TabIndex = 1;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtCustomer.Location = new System.Drawing.Point(80, 58);
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Size = new System.Drawing.Size(129, 21);
            this.stxtCustomer.TabIndex = 2;
            // 
            // stxtPOL
            // 
            this.stxtPOL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtPOL.Location = new System.Drawing.Point(80, 110);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPOL.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOL.Size = new System.Drawing.Size(129, 21);
            this.stxtPOL.TabIndex = 4;
            // 
            // stxtPOD
            // 
            this.stxtPOD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtPOD.Location = new System.Drawing.Point(80, 136);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPOD.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPOD.Size = new System.Drawing.Size(129, 21);
            this.stxtPOD.TabIndex = 5;
            // 
            // stxtDestination
            // 
            this.stxtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtDestination.Location = new System.Drawing.Point(80, 162);
            this.stxtDestination.Name = "stxtDestination";
            this.stxtDestination.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtDestination.Properties.Appearance.Options.UseBackColor = true;
            this.stxtDestination.Size = new System.Drawing.Size(129, 21);
            this.stxtDestination.TabIndex = 6;
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.fromToDateMonthControl1);
            this.navBarGroupControlContainer1.Controls.Add(this.labTo);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbDateSearchType);
            this.navBarGroupControlContainer1.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(214, 166);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // fromToDateMonthControl1
            // 
            this.fromToDateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fromToDateMonthControl1.IsEngish = true;
            this.fromToDateMonthControl1.Location = new System.Drawing.Point(80, 30);
            this.fromToDateMonthControl1.Name = "fromToDateMonthControl1";
            this.fromToDateMonthControl1.Size = new System.Drawing.Size(130, 133);
            this.fromToDateMonthControl1.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(7, 143);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 11;
            this.labTo.Text = "To";
            // 
            // cmbDateSearchType
            // 
            this.cmbDateSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDateSearchType.Location = new System.Drawing.Point(80, 3);
            this.cmbDateSearchType.Name = "cmbDateSearchType";
            this.cmbDateSearchType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbDateSearchType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbDateSearchType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDateSearchType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbDateSearchType.Size = new System.Drawing.Size(129, 21);
            this.cmbDateSearchType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbDateSearchType.TabIndex = 0;
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(7, 116);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 1;
            this.labFrom.Text = "From";
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "Date";
            this.nbarDate.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 168;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "UnChecked.png");
            this.imageList1.Images.SetKeyName(1, "Checked.png");
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 575);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(259, 55);
            this.panelControl1.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(68, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(167, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(259, 575);
            this.panelControl2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.navBarControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 571);
            this.panel1.TabIndex = 0;
            // 
            // OrderSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "OrderSearchPart";
            this.Size = new System.Drawing.Size(259, 630);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtDestination.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labOperationNo;
        private DevExpress.XtraEditors.TextEdit txtOperationNo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbDateSearchType;
        private DevExpress.XtraEditors.LabelControl labSalesDepartment;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbState;
        private DevExpress.XtraEditors.LabelControl labState;
        private DevExpress.XtraEditors.LabelControl labSales;
        private DevExpress.XtraEditors.LabelControl labDestination;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.XtraScrollableControl panel1;
        private DevExpress.XtraEditors.TextEdit stxtCustomer;
        private DevExpress.XtraEditors.TextEdit stxtPOL;
        private DevExpress.XtraEditors.TextEdit stxtPOD;
        private DevExpress.XtraEditors.TextEdit stxtDestination;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl fromToDateMonthControl1;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private System.Windows.Forms.ImageList imageList1;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbCarrier;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbSales;
        private ICP.Framework.ClientComponents.Controls.TreeCheckBox treeBoxSalesDep;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelChecking;
        private DevExpress.XtraEditors.LabelControl lblFinished;
        private DevExpress.XtraEditors.LabelControl lblReject;
        private DevExpress.XtraEditors.LabelControl lblCancelled;
    }
}

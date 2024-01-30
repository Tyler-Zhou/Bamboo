namespace ICP.Common.UI.Configure.EDIConfigure
{
    partial class EDIConfigureEditPart
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtServerAddress = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.icbServerName = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.textComponent = new DevExpress.XtraEditors.TextEdit();
            this.labelServerName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtReceiveAddress = new DevExpress.XtraEditors.TextEdit();
            this.icbSendMode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCarrier = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.textFTP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.textFileFormat = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.textRegularFile = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.textDataFormat = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.textStoredProcedure = new DevExpress.XtraEditors.TextEdit();
            this.checkBox1 = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.icbItemType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.checkCarrier = new DevExpress.XtraEditors.CheckEdit();
            this.txtCustomer = new ICP.ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textComponent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbSendMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFTP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFileFormat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRegularFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDataFormat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textStoredProcedure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbItemType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsDataSource;
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.EDIConfigureList);
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "ServerAddress", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtServerAddress, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtServerAddress.Location = new System.Drawing.Point(76, 101);
            this.txtServerAddress.MenuManager = this.barManager1;
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtServerAddress.Properties.Appearance.Options.UseBackColor = true;
            this.txtServerAddress.Size = new System.Drawing.Size(240, 21);
            this.txtServerAddress.TabIndex = 3;
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
            this.barSave});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Id = 2;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(754, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 229);
            this.barDockControlBottom.Size = new System.Drawing.Size(754, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 203);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(754, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 203);
            // 
            // txtUserName
            // 
            this.txtUserName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "UserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtUserName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtUserName.Location = new System.Drawing.Point(76, 125);
            this.txtUserName.MenuManager = this.barManager1;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtUserName.Properties.Appearance.Options.UseBackColor = true;
            this.txtUserName.Size = new System.Drawing.Size(240, 21);
            this.txtUserName.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.EditValue = "";
            this.dxErrorProvider1.SetIconAlignment(this.txtPassword, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtPassword.Location = new System.Drawing.Point(76, 149);
            this.txtPassword.MenuManager = this.barManager1;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtPassword.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassword.Size = new System.Drawing.Size(240, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // icbServerName
            // 
            this.icbServerName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "ServiceConfigureKeyName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.icbServerName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "ServiceConfigureKeyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.icbServerName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.icbServerName.Location = new System.Drawing.Point(76, 29);
            this.icbServerName.Name = "icbServerName";
            this.icbServerName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.icbServerName.Properties.Appearance.Options.UseBackColor = true;
            this.icbServerName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icbServerName.Size = new System.Drawing.Size(240, 21);
            this.icbServerName.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.icbServerName.TabIndex = 0;
            // 
            // textComponent
            // 
            this.textComponent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "Component", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.textComponent, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textComponent.Location = new System.Drawing.Point(490, 29);
            this.textComponent.MenuManager = this.barManager1;
            this.textComponent.Name = "textComponent";
            this.textComponent.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.textComponent.Properties.Appearance.Options.UseBackColor = true;
            this.textComponent.Size = new System.Drawing.Size(240, 21);
            this.textComponent.TabIndex = 7;
            // 
            // labelServerName
            // 
            this.labelServerName.Location = new System.Drawing.Point(3, 32);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(36, 14);
            this.labelServerName.TabIndex = 0;
            this.labelServerName.Text = "服务名";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "船公司";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 78);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "发送模式";
            // 
            // labelControl3
            // 
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.labelControl3.Location = new System.Drawing.Point(3, 101);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(62, 16);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "服务器地址";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(3, 126);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "帐号";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(3, 149);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "密码";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(3, 175);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "接收地址";
            // 
            // txtReceiveAddress
            // 
            this.txtReceiveAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "ReceiveAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtReceiveAddress.Location = new System.Drawing.Point(76, 173);
            this.txtReceiveAddress.MenuManager = this.barManager1;
            this.txtReceiveAddress.Name = "txtReceiveAddress";
            this.txtReceiveAddress.Size = new System.Drawing.Size(240, 21);
            this.txtReceiveAddress.TabIndex = 6;
            // 
            // icbSendMode
            // 
            this.icbSendMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "UploadMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.icbSendMode.Location = new System.Drawing.Point(76, 77);
            this.icbSendMode.MenuManager = this.barManager1;
            this.icbSendMode.Name = "icbSendMode";
            this.icbSendMode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.icbSendMode.Properties.Appearance.Options.UseBackColor = true;
            this.icbSendMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icbSendMode.Size = new System.Drawing.Size(240, 21);
            this.icbSendMode.SpecifiedBackColor = System.Drawing.Color.White;
            this.icbSendMode.TabIndex = 2;
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "CarrierID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbCarrier.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "CarrierName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbCarrier.Location = new System.Drawing.Point(76, 53);
            this.cmbCarrier.MenuManager = this.barManager1;
            this.cmbCarrier.Name = "cmbCarrier";
            this.cmbCarrier.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCarrier.Size = new System.Drawing.Size(240, 21);
            this.cmbCarrier.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCarrier.TabIndex = 1;
            this.cmbCarrier.TabStop = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(405, 32);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 23;
            this.labelControl7.Text = "组件";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(405, 57);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(45, 14);
            this.labelControl9.TabIndex = 25;
            this.labelControl9.Text = "FTP目录";
            // 
            // textFTP
            // 
            this.textFTP.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "FTP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textFTP.Location = new System.Drawing.Point(490, 53);
            this.textFTP.MenuManager = this.barManager1;
            this.textFTP.Name = "textFTP";
            this.textFTP.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.textFTP.Properties.Appearance.Options.UseBackColor = true;
            this.textFTP.Size = new System.Drawing.Size(240, 21);
            this.textFTP.TabIndex = 8;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(405, 83);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 27;
            this.labelControl10.Text = "文件格式";
            // 
            // textFileFormat
            // 
            this.textFileFormat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "FileFormat", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textFileFormat.Location = new System.Drawing.Point(490, 78);
            this.textFileFormat.MenuManager = this.barManager1;
            this.textFileFormat.Name = "textFileFormat";
            this.textFileFormat.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.textFileFormat.Properties.Appearance.Options.UseBackColor = true;
            this.textFileFormat.Size = new System.Drawing.Size(240, 21);
            this.textFileFormat.TabIndex = 9;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(405, 129);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(48, 14);
            this.labelControl11.TabIndex = 31;
            this.labelControl11.Text = "规则文件";
            // 
            // textRegularFile
            // 
            this.textRegularFile.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "RegularFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textRegularFile.Location = new System.Drawing.Point(490, 126);
            this.textRegularFile.MenuManager = this.barManager1;
            this.textRegularFile.Name = "textRegularFile";
            this.textRegularFile.Size = new System.Drawing.Size(240, 21);
            this.textRegularFile.TabIndex = 11;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(405, 105);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(48, 14);
            this.labelControl12.TabIndex = 29;
            this.labelControl12.Text = "数据格式";
            // 
            // textDataFormat
            // 
            this.textDataFormat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "DataFormat", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textDataFormat.Location = new System.Drawing.Point(490, 102);
            this.textDataFormat.MenuManager = this.barManager1;
            this.textDataFormat.Name = "textDataFormat";
            this.textDataFormat.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.textDataFormat.Properties.Appearance.Options.UseBackColor = true;
            this.textDataFormat.Size = new System.Drawing.Size(240, 21);
            this.textDataFormat.TabIndex = 10;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(405, 153);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(72, 14);
            this.labelControl13.TabIndex = 33;
            this.labelControl13.Text = "存储过程名称";
            // 
            // textStoredProcedure
            // 
            this.textStoredProcedure.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "StoredProcedure", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textStoredProcedure.Location = new System.Drawing.Point(490, 150);
            this.textStoredProcedure.MenuManager = this.barManager1;
            this.textStoredProcedure.Name = "textStoredProcedure";
            this.textStoredProcedure.Size = new System.Drawing.Size(240, 21);
            this.textStoredProcedure.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "IsValid", true));
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(712, 175);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 19);
            this.checkBox1.TabIndex = 13;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(658, 176);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(48, 14);
            this.labelControl14.TabIndex = 36;
            this.labelControl14.Text = "是否有效";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(405, 176);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(53, 14);
            this.labelControl8.TabIndex = 36;
            this.labelControl8.Text = "订舱/补料";
            // 
            // icbItemType
            // 
            this.icbItemType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "EDIMode", true));
            this.icbItemType.Location = new System.Drawing.Point(490, 173);
            this.icbItemType.MenuManager = this.barManager1;
            this.icbItemType.Name = "icbItemType";
            this.icbItemType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.icbItemType.Properties.Appearance.Options.UseBackColor = true;
            this.icbItemType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icbItemType.Size = new System.Drawing.Size(127, 21);
            this.icbItemType.SpecifiedBackColor = System.Drawing.SystemColors.Window;
            this.icbItemType.TabIndex = 41;
            // 
            // checkCarrier
            // 
            this.checkCarrier.Location = new System.Drawing.Point(318, 55);
            this.checkCarrier.MenuManager = this.barManager1;
            this.checkCarrier.Name = "checkCarrier";
            this.checkCarrier.Properties.Caption = "Forward";
            this.checkCarrier.Size = new System.Drawing.Size(70, 19);
            this.checkCarrier.TabIndex = 841;
            this.checkCarrier.CheckedChanged += new System.EventHandler(this.checkCarrier_CheckedChanged);
            // 
            // txtCustomer
            // 
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsDataSource, "CarrierID", true));
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("FinderName", this.bsDataSource, "CarrierName", true));
            this.txtCustomer.FinderName = "CustomerAgentOfCarrierFinder";
            this.txtCustomer.Location = new System.Drawing.Point(76, 53);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(240, 21);
            this.txtCustomer.TabIndex = 846;
            this.txtCustomer.Visible = false;
            this.txtCustomer.EditValueChanged += new System.EventHandler(this.txtCustomer_EditValueChanged);
            // 
            // EDIConfigureEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.checkCarrier);
            this.Controls.Add(this.icbItemType);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.textStoredProcedure);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.textRegularFile);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.textDataFormat);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.textFileFormat);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.textFTP);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.textComponent);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtReceiveAddress);
            this.Controls.Add(this.icbSendMode);
            this.Controls.Add(this.cmbCarrier);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.icbServerName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtServerAddress);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelServerName);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "EDIConfigureEditPart";
            this.Size = new System.Drawing.Size(754, 229);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textComponent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbSendMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFTP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFileFormat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRegularFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDataFormat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textStoredProcedure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icbItemType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }                

        #endregion

        private System.Windows.Forms.BindingSource bsDataSource;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtReceiveAddress;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit icbSendMode;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCarrier;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit icbServerName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtServerAddress;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelServerName;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit textComponent;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit textStoredProcedure;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit textRegularFile;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit textDataFormat;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit textFileFormat;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit textFTP;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.CheckEdit checkBox1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit icbItemType;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.CheckEdit checkCarrier;
        private ReportCenter.UI.Comm.Controls.SingleCustomerFinderButtonEdit txtCustomer;
    }
}
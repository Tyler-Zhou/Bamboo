namespace ICP.FCM.OceanExport.UI
{
    partial class CSCLBookingEditPart
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
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtHSCode = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cmbDeliveryTerm = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbReleaseCargoType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtRemarksCN = new DevExpress.XtraEditors.MemoEdit();
            this.txtBookingRemarksCN = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txtHBLNO = new DevExpress.XtraEditors.TextEdit();
            this.txtSCACCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNotify = new DevExpress.XtraEditors.MemoEdit();
            this.txtConsignee = new DevExpress.XtraEditors.MemoEdit();
            this.txtShipper = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtRealNotify = new DevExpress.XtraEditors.MemoEdit();
            this.txtRealConsignee = new DevExpress.XtraEditors.MemoEdit();
            this.txtRealShipper = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.chkAMS = new System.Windows.Forms.CheckBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.cmbBLTitle = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHSCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDeliveryTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseCargoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarksCN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingRemarksCN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSCACCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotify.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealNotify.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealShipper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBLTitle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.labelControl2);
            this.groupControl5.Controls.Add(this.txtHSCode);
            this.groupControl5.Controls.Add(this.cmbDeliveryTerm);
            this.groupControl5.Controls.Add(this.cmbReleaseCargoType);
            this.groupControl5.Controls.Add(this.txtRemarksCN);
            this.groupControl5.Controls.Add(this.labelControl17);
            this.groupControl5.Controls.Add(this.labelControl1);
            this.groupControl5.Controls.Add(this.labelControl16);
            this.groupControl5.Location = new System.Drawing.Point(6, 556);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(805, 125);
            this.groupControl5.TabIndex = 382;
            this.groupControl5.Text = "服务信息";
            this.groupControl5.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(41, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 14);
            this.labelControl2.TabIndex = 386;
            this.labelControl2.Text = "HS Code";
            this.labelControl2.Visible = false;
            // 
            // txtHSCode
            // 
            this.txtHSCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "HSCode", true));
            this.txtHSCode.Location = new System.Drawing.Point(95, 26);
            this.txtHSCode.Name = "txtHSCode";
            this.txtHSCode.Size = new System.Drawing.Size(150, 21);
            this.txtHSCode.TabIndex = 0;
            this.txtHSCode.Visible = false;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.CSCLBookingInfo);
            // 
            // cmbDeliveryTerm
            // 
            this.cmbDeliveryTerm.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DeliveryTerm", true));
            this.cmbDeliveryTerm.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "DeliveryTerm", true));
            this.cmbDeliveryTerm.Location = new System.Drawing.Point(624, 26);
            this.cmbDeliveryTerm.Name = "cmbDeliveryTerm";
            this.cmbDeliveryTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDeliveryTerm.Properties.Items.AddRange(new object[] {
            "CY-CY",
            "CY-DOOR",
            "CY-TACKLE",
            "CY-LO",
            "CY-FO",
            "CY-HOOK"});
            this.cmbDeliveryTerm.Size = new System.Drawing.Size(150, 21);
            this.cmbDeliveryTerm.TabIndex = 2;
            this.cmbDeliveryTerm.Visible = false;
            // 
            // cmbReleaseCargoType
            // 
            this.cmbReleaseCargoType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ReleaseCargoType", true));
            this.cmbReleaseCargoType.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "ReleaseCargoType", true));
            this.cmbReleaseCargoType.Location = new System.Drawing.Point(349, 26);
            this.cmbReleaseCargoType.Name = "cmbReleaseCargoType";
            this.cmbReleaseCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseCargoType.Properties.Items.AddRange(new object[] {
            "WAYBILL",
            "正本提单",
            "电放提单",
            "异地出单",
            "自出单"});
            this.cmbReleaseCargoType.Size = new System.Drawing.Size(150, 21);
            this.cmbReleaseCargoType.TabIndex = 1;
            this.cmbReleaseCargoType.Visible = false;
            // 
            // txtRemarksCN
            // 
            this.txtRemarksCN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RemarksCN", true));
            this.txtRemarksCN.EditValue = "";
            this.txtRemarksCN.Location = new System.Drawing.Point(88, 53);
            this.txtRemarksCN.Name = "txtRemarksCN";
            this.txtRemarksCN.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtRemarksCN.Properties.Appearance.Options.UseBackColor = true;
            this.txtRemarksCN.Size = new System.Drawing.Size(705, 60);
            this.txtRemarksCN.TabIndex = 4;
            this.txtRemarksCN.Visible = false;
            // 
            // txtBookingRemarksCN
            // 
            this.txtBookingRemarksCN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "BookingRemarksCN", true));
            this.txtBookingRemarksCN.EditValue = "";
            this.txtBookingRemarksCN.Location = new System.Drawing.Point(89, 241);
            this.txtBookingRemarksCN.Name = "txtBookingRemarksCN";
            this.txtBookingRemarksCN.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtBookingRemarksCN.Properties.Appearance.Options.UseBackColor = true;
            this.txtBookingRemarksCN.Size = new System.Drawing.Size(705, 60);
            this.txtBookingRemarksCN.TabIndex = 3;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(565, 29);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(48, 14);
            this.labelControl17.TabIndex = 379;
            this.labelControl17.Text = "运输条款";
            this.labelControl17.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 376;
            this.labelControl1.Text = "附加说明";
            this.labelControl1.Visible = false;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(12, 244);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(48, 14);
            this.labelControl15.TabIndex = 366;
            this.labelControl15.Text = "提箱要求";
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(295, 29);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(48, 14);
            this.labelControl16.TabIndex = 361;
            this.labelControl16.Text = "放货方式";
            this.labelControl16.Visible = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.cmbBLTitle);
            this.groupControl3.Controls.Add(this.labelControl11);
            this.groupControl3.Controls.Add(this.txtNotify);
            this.groupControl3.Controls.Add(this.txtConsignee);
            this.groupControl3.Controls.Add(this.txtSCACCode);
            this.groupControl3.Controls.Add(this.txtShipper);
            this.groupControl3.Controls.Add(this.txtBookingRemarksCN);
            this.groupControl3.Controls.Add(this.labelControl8);
            this.groupControl3.Controls.Add(this.labelControl15);
            this.groupControl3.Controls.Add(this.labelControl9);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.labelControl10);
            this.groupControl3.Location = new System.Drawing.Point(3, 3);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(805, 305);
            this.groupControl3.TabIndex = 381;
            this.groupControl3.Text = "基本信息";
            // 
            // txtHBLNO
            // 
            this.txtHBLNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "HBLNO", true));
            this.txtHBLNO.Location = new System.Drawing.Point(542, 312);
            this.txtHBLNO.Name = "txtHBLNO";
            this.txtHBLNO.Properties.MaxLength = 12;
            this.txtHBLNO.Size = new System.Drawing.Size(255, 21);
            this.txtHBLNO.TabIndex = 1;
            this.txtHBLNO.Visible = false;
            // 
            // txtSCACCode
            // 
            this.txtSCACCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "SCACCode", true));
            this.txtSCACCode.Location = new System.Drawing.Point(539, 25);
            this.txtSCACCode.Name = "txtSCACCode";
            this.txtSCACCode.Properties.MaxLength = 5;
            this.txtSCACCode.Size = new System.Drawing.Size(255, 21);
            this.txtSCACCode.TabIndex = 0;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(446, 315);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(90, 14);
            this.labelControl6.TabIndex = 368;
            this.labelControl6.Text = "NVOCC HB/LNO.";
            this.labelControl6.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(436, 28);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(97, 14);
            this.labelControl4.TabIndex = 367;
            this.labelControl4.Text = "SCAC/8000 CODE";
            // 
            // txtNotify
            // 
            this.txtNotify.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Notify", true));
            this.txtNotify.EditValue = "";
            this.txtNotify.Location = new System.Drawing.Point(89, 177);
            this.txtNotify.Name = "txtNotify";
            this.txtNotify.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtNotify.Properties.Appearance.Options.UseBackColor = true;
            this.txtNotify.Properties.MaxLength = 200;
            this.txtNotify.Size = new System.Drawing.Size(705, 60);
            this.txtNotify.TabIndex = 2;
            // 
            // txtConsignee
            // 
            this.txtConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Consignee", true));
            this.txtConsignee.EditValue = "";
            this.txtConsignee.Location = new System.Drawing.Point(89, 113);
            this.txtConsignee.Name = "txtConsignee";
            this.txtConsignee.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtConsignee.Properties.Appearance.Options.UseBackColor = true;
            this.txtConsignee.Properties.MaxLength = 200;
            this.txtConsignee.Size = new System.Drawing.Size(705, 60);
            this.txtConsignee.TabIndex = 1;
            // 
            // txtShipper
            // 
            this.txtShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Shipper", true));
            this.txtShipper.EditValue = "";
            this.txtShipper.Location = new System.Drawing.Point(89, 49);
            this.txtShipper.Name = "txtShipper";
            this.txtShipper.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtShipper.Properties.Appearance.Options.UseBackColor = true;
            this.txtShipper.Properties.MaxLength = 200;
            this.txtShipper.Size = new System.Drawing.Size(705, 60);
            this.txtShipper.TabIndex = 0;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 50);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(41, 14);
            this.labelControl8.TabIndex = 361;
            this.labelControl8.Text = "Shipper";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 180);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(32, 14);
            this.labelControl9.TabIndex = 353;
            this.labelControl9.Text = "Notify";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 116);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(56, 14);
            this.labelControl10.TabIndex = 357;
            this.labelControl10.Text = "Consignee";
            // 
            // txtRealNotify
            // 
            this.txtRealNotify.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RealNotify", true));
            this.txtRealNotify.EditValue = "";
            this.txtRealNotify.Location = new System.Drawing.Point(95, 151);
            this.txtRealNotify.Name = "txtRealNotify";
            this.txtRealNotify.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtRealNotify.Properties.Appearance.Options.UseBackColor = true;
            this.txtRealNotify.Properties.MaxLength = 200;
            this.txtRealNotify.Size = new System.Drawing.Size(705, 60);
            this.txtRealNotify.TabIndex = 2;
            // 
            // txtRealConsignee
            // 
            this.txtRealConsignee.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RealConsignee", true));
            this.txtRealConsignee.EditValue = "";
            this.txtRealConsignee.Location = new System.Drawing.Point(95, 87);
            this.txtRealConsignee.Name = "txtRealConsignee";
            this.txtRealConsignee.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtRealConsignee.Properties.Appearance.Options.UseBackColor = true;
            this.txtRealConsignee.Properties.MaxLength = 200;
            this.txtRealConsignee.Size = new System.Drawing.Size(705, 60);
            this.txtRealConsignee.TabIndex = 1;
            // 
            // txtRealShipper
            // 
            this.txtRealShipper.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RealShipper", true));
            this.txtRealShipper.EditValue = "";
            this.txtRealShipper.Location = new System.Drawing.Point(95, 24);
            this.txtRealShipper.Name = "txtRealShipper";
            this.txtRealShipper.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtRealShipper.Properties.Appearance.Options.UseBackColor = true;
            this.txtRealShipper.Properties.MaxLength = 200;
            this.txtRealShipper.Size = new System.Drawing.Size(705, 60);
            this.txtRealShipper.TabIndex = 0;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(11, 27);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(63, 14);
            this.labelControl7.TabIndex = 361;
            this.labelControl7.Text = "RealShipper";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 154);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 14);
            this.labelControl3.TabIndex = 353;
            this.labelControl3.Text = "RealNotify";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 90);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 14);
            this.labelControl5.TabIndex = 357;
            this.labelControl5.Text = "RealConsignee";
            // 
            // chkAMS
            // 
            this.chkAMS.AutoSize = true;
            this.chkAMS.Location = new System.Drawing.Point(15, 314);
            this.chkAMS.Name = "chkAMS";
            this.chkAMS.Size = new System.Drawing.Size(98, 18);
            this.chkAMS.TabIndex = 383;
            this.chkAMS.Text = "船东代发AMS";
            this.chkAMS.UseVisualStyleBackColor = true;
            this.chkAMS.CheckedChanged += new System.EventHandler(this.chkAMS_CheckedChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtRealNotify);
            this.groupControl1.Controls.Add(this.txtRealConsignee);
            this.groupControl1.Controls.Add(this.txtRealShipper);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Location = new System.Drawing.Point(3, 334);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(805, 215);
            this.groupControl1.TabIndex = 379;
            this.groupControl1.Text = "真实收发通，只适用于美加线。公司名称输第一行,地址第二行起输";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(13, 29);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(48, 14);
            this.labelControl11.TabIndex = 369;
            this.labelControl11.Text = "订舱单位";
            // 
            // cmbBLTitle
            // 
            this.cmbBLTitle.Location = new System.Drawing.Point(89, 25);
            this.cmbBLTitle.Name = "cmbBLTitle";
            this.cmbBLTitle.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbBLTitle.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBLTitle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBLTitle.Size = new System.Drawing.Size(238, 21);
            this.cmbBLTitle.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbBLTitle.TabIndex = 370;
            this.cmbBLTitle.SelectedIndexChanged += new System.EventHandler(this.cmbBLTitle_SelectedIndexChanged);
            // 
            // CSCLBookingEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.txtHBLNO);
            this.Controls.Add(this.chkAMS);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl6);
            this.Name = "CSCLBookingEditPart";
            this.Size = new System.Drawing.Size(811, 557);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHSCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDeliveryTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseCargoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarksCN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingRemarksCN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHBLNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSCACCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotify.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealNotify.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealShipper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBLTitle.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDeliveryTerm;
        private DevExpress.XtraEditors.ComboBoxEdit cmbReleaseCargoType;
        private DevExpress.XtraEditors.MemoEdit txtRemarksCN;
        private DevExpress.XtraEditors.MemoEdit txtBookingRemarksCN;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.TextEdit txtHBLNO;
        private DevExpress.XtraEditors.TextEdit txtSCACCode;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit txtNotify;
        private DevExpress.XtraEditors.MemoEdit txtConsignee;
        private DevExpress.XtraEditors.MemoEdit txtShipper;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.MemoEdit txtRealNotify;
        private DevExpress.XtraEditors.MemoEdit txtRealConsignee;
        private DevExpress.XtraEditors.MemoEdit txtRealShipper;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtHSCode;
        private System.Windows.Forms.CheckBox chkAMS;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBLTitle;
    }
}

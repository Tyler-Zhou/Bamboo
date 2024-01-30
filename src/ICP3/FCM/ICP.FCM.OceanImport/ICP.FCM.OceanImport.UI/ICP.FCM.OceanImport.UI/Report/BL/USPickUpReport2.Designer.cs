namespace ICP.FCM.OceanImport.UI.Report.BL
{
    partial class USPickUpReport2
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
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtCompanyAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.txtTelephone = new DevExpress.XtraEditors.TextEdit();
            this.txtCompanyName = new DevExpress.XtraEditors.TextEdit();
            this.labFax = new DevExpress.XtraEditors.LabelControl();
            this.labTelephone = new DevExpress.XtraEditors.LabelControl();
            this.labCompanyAddress = new DevExpress.XtraEditors.LabelControl();
            this.chkManualTitle = new DevExpress.XtraEditors.CheckEdit();
            this.labCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.rdoArrivalNotice = new DevExpress.XtraEditors.RadioGroup();
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkManualTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoArrivalNotice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 451);
            this.pnlQuery.Size = new System.Drawing.Size(202, 38);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(51, 8);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "Show";
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.navBarControl1);
            this.pnlCondition.Size = new System.Drawing.Size(202, 489);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(791, 489);
            this.splitContainerControl.SplitterPosition = 202;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 197;
            this.navBarControl1.Size = new System.Drawing.Size(202, 489);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "PickUpDelivery";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 423;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.txtEmail);
            this.navBarGroupControlContainer1.Controls.Add(this.labEmail);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCompanyAddress);
            this.navBarGroupControlContainer1.Controls.Add(this.txtFax);
            this.navBarGroupControlContainer1.Controls.Add(this.txtTelephone);
            this.navBarGroupControlContainer1.Controls.Add(this.txtCompanyName);
            this.navBarGroupControlContainer1.Controls.Add(this.labFax);
            this.navBarGroupControlContainer1.Controls.Add(this.labTelephone);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompanyAddress);
            this.navBarGroupControlContainer1.Controls.Add(this.chkManualTitle);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompanyName);
            this.navBarGroupControlContainer1.Controls.Add(this.rdoArrivalNotice);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(198, 421);
            this.navBarGroupControlContainer1.TabIndex = 0;
            this.navBarGroupControlContainer1.Text = "提货通知书";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(4, 395);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(185, 21);
            this.txtEmail.TabIndex = 6;
            // 
            // labEmail
            // 
            this.labEmail.Location = new System.Drawing.Point(4, 375);
            this.labEmail.Name = "labEmail";
            this.labEmail.Size = new System.Drawing.Size(27, 14);
            this.labEmail.TabIndex = 11;
            this.labEmail.Text = "Email";
            // 
            // txtCompanyAddress
            // 
            this.txtCompanyAddress.Location = new System.Drawing.Point(4, 211);
            this.txtCompanyAddress.Name = "txtCompanyAddress";
            this.txtCompanyAddress.Size = new System.Drawing.Size(185, 51);
            this.txtCompanyAddress.TabIndex = 3;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(4, 343);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(185, 21);
            this.txtFax.TabIndex = 5;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(4, 292);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(185, 21);
            this.txtTelephone.TabIndex = 4;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(4, 161);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(185, 21);
            this.txtCompanyName.TabIndex = 2;
            // 
            // labFax
            // 
            this.labFax.Location = new System.Drawing.Point(4, 324);
            this.labFax.Name = "labFax";
            this.labFax.Size = new System.Drawing.Size(18, 14);
            this.labFax.TabIndex = 5;
            this.labFax.Text = "Fax";
            // 
            // labTelephone
            // 
            this.labTelephone.Location = new System.Drawing.Point(4, 273);
            this.labTelephone.Name = "labTelephone";
            this.labTelephone.Size = new System.Drawing.Size(59, 14);
            this.labTelephone.TabIndex = 4;
            this.labTelephone.Text = "Telephone";
            // 
            // labCompanyAddress
            // 
            this.labCompanyAddress.Location = new System.Drawing.Point(4, 192);
            this.labCompanyAddress.Name = "labCompanyAddress";
            this.labCompanyAddress.Size = new System.Drawing.Size(97, 14);
            this.labCompanyAddress.TabIndex = 3;
            this.labCompanyAddress.Text = "Company Address";
            // 
            // chkManualTitle
            // 
            this.chkManualTitle.Location = new System.Drawing.Point(3, 114);
            this.chkManualTitle.Name = "chkManualTitle";
            this.chkManualTitle.Properties.Caption = "Manual title";
            this.chkManualTitle.Size = new System.Drawing.Size(153, 19);
            this.chkManualTitle.TabIndex = 1;
            this.chkManualTitle.CheckedChanged += new System.EventHandler(this.chkManualTitle_CheckedChanged);
            // 
            // labCompanyName
            // 
            this.labCompanyName.Location = new System.Drawing.Point(4, 142);
            this.labCompanyName.Name = "labCompanyName";
            this.labCompanyName.Size = new System.Drawing.Size(85, 14);
            this.labCompanyName.TabIndex = 1;
            this.labCompanyName.Text = "Company Name";
            // 
            // rdoArrivalNotice
            // 
            this.rdoArrivalNotice.Location = new System.Drawing.Point(3, 4);
            this.rdoArrivalNotice.Name = "rdoArrivalNotice";
            this.rdoArrivalNotice.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Pick Up _Delivery"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Pick Up_ Delivery(short)")});
            this.rdoArrivalNotice.Size = new System.Drawing.Size(187, 91);
            this.rdoArrivalNotice.TabIndex = 0;
            // 
            // USPickUpReport2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "USPickUpReport2";
            this.Size = new System.Drawing.Size(791, 489);
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkManualTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoArrivalNotice.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labEmail;
        private DevExpress.XtraEditors.MemoEdit txtCompanyAddress;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.TextEdit txtTelephone;
        private DevExpress.XtraEditors.TextEdit txtCompanyName;
        private DevExpress.XtraEditors.LabelControl labFax;
        private DevExpress.XtraEditors.LabelControl labTelephone;
        private DevExpress.XtraEditors.LabelControl labCompanyAddress;
        private DevExpress.XtraEditors.CheckEdit chkManualTitle;
        private DevExpress.XtraEditors.LabelControl labCompanyName;
        private DevExpress.XtraEditors.RadioGroup rdoArrivalNotice;
    }
}

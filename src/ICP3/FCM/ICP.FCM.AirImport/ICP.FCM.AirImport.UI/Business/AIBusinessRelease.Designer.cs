namespace ICP.FCM.AirImport.UI
{
    partial class OIBusinessRelease
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OIBusinessRelease));
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtSubNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtMBLNo = new DevExpress.XtraEditors.TextEdit();
            this.labBLNO = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.txtConsigneeName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl44 = new DevExpress.XtraEditors.LabelControl();
            this.dtpReleaseDate = new DevExpress.XtraEditors.DateEdit();
            this.labOrderDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSales = new DevExpress.XtraEditors.TextEdit();
            this.errorList = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.cmbReleaseType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(407, 195);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(321, 195);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dtpReleaseDate);
            this.panelControl1.Controls.Add(this.labOrderDate);
            this.panelControl1.Controls.Add(this.labelControl44);
            this.panelControl1.Controls.Add(this.cmbReleaseType);
            this.panelControl1.Controls.Add(this.txtSales);
            this.panelControl1.Controls.Add(this.txtSubNo);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.txtMBLNo);
            this.panelControl1.Controls.Add(this.labBLNO);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labOperationNo);
            this.panelControl1.Controls.Add(this.txtCustomerName);
            this.panelControl1.Controls.Add(this.txtNo);
            this.panelControl1.Controls.Add(this.txtConsigneeName);
            this.panelControl1.Location = new System.Drawing.Point(17, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(485, 172);
            this.panelControl1.TabIndex = 2;
            // 
            // txtSubNo
            // 
            this.txtSubNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubNo.Location = new System.Drawing.Point(85, 133);
            this.txtSubNo.Name = "txtSubNo";
            this.txtSubNo.Properties.ReadOnly = true;
            this.txtSubNo.Size = new System.Drawing.Size(117, 21);
            this.txtSubNo.TabIndex = 4;
            this.txtSubNo.TabStop = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(26, 45);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 64;
            this.labelControl3.Text = "收货人";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(26, 136);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 69;
            this.labelControl6.Text = "分提单号";
            // 
            // txtMBLNo
            // 
            this.txtMBLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMBLNo.Location = new System.Drawing.Point(85, 103);
            this.txtMBLNo.Name = "txtMBLNo";
            this.txtMBLNo.Properties.ReadOnly = true;
            this.txtMBLNo.Size = new System.Drawing.Size(117, 21);
            this.txtMBLNo.TabIndex = 3;
            this.txtMBLNo.TabStop = false;
            // 
            // labBLNO
            // 
            this.labBLNO.Location = new System.Drawing.Point(26, 106);
            this.labBLNO.Name = "labBLNO";
            this.labBLNO.Size = new System.Drawing.Size(48, 14);
            this.labBLNO.TabIndex = 69;
            this.labBLNO.Text = "主提单号";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(26, 15);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 68;
            this.labelControl5.Text = "客户";
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(26, 77);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(36, 14);
            this.labOperationNo.TabIndex = 68;
            this.labOperationNo.Text = "业务号";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerName.Location = new System.Drawing.Point(85, 12);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Properties.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(374, 21);
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.TabStop = false;
            // 
            // txtNo
            // 
            this.txtNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNo.Location = new System.Drawing.Point(85, 74);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(117, 21);
            this.txtNo.TabIndex = 2;
            this.txtNo.TabStop = false;
            // 
            // txtConsigneeName
            // 
            this.txtConsigneeName.Location = new System.Drawing.Point(85, 42);
            this.txtConsigneeName.Name = "txtConsigneeName";
            this.txtConsigneeName.Properties.ReadOnly = true;
            this.txtConsigneeName.Size = new System.Drawing.Size(374, 21);
            this.txtConsigneeName.TabIndex = 1;
            this.txtConsigneeName.TabStop = false;
            // 
            // labelControl44
            // 
            this.labelControl44.Location = new System.Drawing.Point(265, 106);
            this.labelControl44.Name = "labelControl44";
            this.labelControl44.Size = new System.Drawing.Size(48, 14);
            this.labelControl44.TabIndex = 86;
            this.labelControl44.Text = "放货类型";
            // 
            // dtpReleaseDate
            // 
            this.dtpReleaseDate.EditValue = null;
            this.errorList.SetIconAlignment(this.dtpReleaseDate, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dtpReleaseDate.Location = new System.Drawing.Point(339, 133);
            this.dtpReleaseDate.Name = "dtpReleaseDate";
            this.dtpReleaseDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dtpReleaseDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtpReleaseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReleaseDate.Properties.Mask.EditMask = "";
            this.dtpReleaseDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtpReleaseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpReleaseDate.Size = new System.Drawing.Size(119, 21);
            this.dtpReleaseDate.TabIndex = 7;
            // 
            // labOrderDate
            // 
            this.labOrderDate.Location = new System.Drawing.Point(265, 136);
            this.labOrderDate.Name = "labOrderDate";
            this.labOrderDate.Size = new System.Drawing.Size(48, 14);
            this.labOrderDate.TabIndex = 143;
            this.labOrderDate.Text = "放货日期";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(265, 77);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 69;
            this.labelControl1.Text = "揽货人";
            // 
            // txtSales
            // 
            this.txtSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSales.Location = new System.Drawing.Point(339, 74);
            this.txtSales.Name = "txtSales";
            this.txtSales.Properties.ReadOnly = true;
            this.txtSales.Size = new System.Drawing.Size(119, 21);
            this.txtSales.TabIndex = 5;
            this.txtSales.TabStop = false;
            // 
            // errorList
            // 
            this.errorList.ContainerControl = this;
            // 
            // cmbReleaseType
            // 
            this.errorList.SetIconAlignment(this.cmbReleaseType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbReleaseType.Location = new System.Drawing.Point(339, 103);
            this.cmbReleaseType.Name = "cmbReleaseType";
            this.cmbReleaseType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbReleaseType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReleaseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseType.Size = new System.Drawing.Size(119, 21);
            this.cmbReleaseType.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbReleaseType.TabIndex = 6;
            // 
            // OIBusinessRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelControl1);
            this.Name = "OIBusinessRelease";
            this.Size = new System.Drawing.Size(526, 228);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsigneeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.TextEdit txtSubNo;
        protected DevExpress.XtraEditors.LabelControl labelControl3;
        protected DevExpress.XtraEditors.LabelControl labelControl6;
        protected DevExpress.XtraEditors.TextEdit txtMBLNo;
        protected DevExpress.XtraEditors.LabelControl labBLNO;
        protected DevExpress.XtraEditors.LabelControl labelControl5;
        protected DevExpress.XtraEditors.LabelControl labOperationNo;
        protected DevExpress.XtraEditors.TextEdit txtCustomerName;
        protected DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.TextEdit txtConsigneeName;
        private DevExpress.XtraEditors.LabelControl labelControl44;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReleaseType;
        private DevExpress.XtraEditors.DateEdit dtpReleaseDate;
        private DevExpress.XtraEditors.LabelControl labOrderDate;
        protected DevExpress.XtraEditors.TextEdit txtSales;
        protected DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorList;
    }
}

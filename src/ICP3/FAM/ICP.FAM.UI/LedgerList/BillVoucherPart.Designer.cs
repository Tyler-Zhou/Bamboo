namespace ICP.FAM.UI
{
    partial class BillVoucherPart
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labCompanyID = new DevExpress.XtraEditors.LabelControl();
            this.labMonth = new DevExpress.XtraEditors.LabelControl();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbMonth = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labDate = new DevExpress.XtraEditors.LabelControl();
            this.dtpChargingCloseDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 77);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(134, 77);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labCompanyID
            // 
            this.labCompanyID.Location = new System.Drawing.Point(17, 14);
            this.labCompanyID.Name = "labCompanyID";
            this.labCompanyID.Size = new System.Drawing.Size(24, 14);
            this.labCompanyID.TabIndex = 2;
            this.labCompanyID.Text = "公司";
            // 
            // labMonth
            // 
            this.labMonth.Location = new System.Drawing.Point(17, 46);
            this.labMonth.Name = "labMonth";
            this.labMonth.Size = new System.Drawing.Size(24, 14);
            this.labMonth.TabIndex = 3;
            this.labMonth.Text = "月份";
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.Location = new System.Drawing.Point(53, 11);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(155, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 4;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.Location = new System.Drawing.Point(53, 43);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMonth.Size = new System.Drawing.Size(155, 21);
            this.cmbMonth.TabIndex = 5;
            // 
            // labDate
            // 
            this.labDate.Location = new System.Drawing.Point(17, 46);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(24, 14);
            this.labDate.TabIndex = 6;
            this.labDate.Text = "日期";
            this.labDate.Visible = false;
            // 
            // dtpChargingCloseDate
            // 
            this.dtpChargingCloseDate.EditValue = null;
            this.dtpChargingCloseDate.Location = new System.Drawing.Point(53, 43);
            this.dtpChargingCloseDate.Name = "dtpChargingCloseDate";
            this.dtpChargingCloseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpChargingCloseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpChargingCloseDate.Size = new System.Drawing.Size(155, 21);
            this.dtpChargingCloseDate.TabIndex = 7;
            this.dtpChargingCloseDate.Visible = false;
            // 
            // BillVoucherPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpChargingCloseDate);
            this.Controls.Add(this.labDate);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.labMonth);
            this.Controls.Add(this.labCompanyID);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Name = "BillVoucherPart";
            this.Size = new System.Drawing.Size(233, 116);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.labCompanyID, 0);
            this.Controls.SetChildIndex(this.labMonth, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.cmbMonth, 0);
            this.Controls.SetChildIndex(this.labDate, 0);
            this.Controls.SetChildIndex(this.dtpChargingCloseDate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labCompanyID;
        private DevExpress.XtraEditors.LabelControl labMonth;
        public ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbMonth;
        private DevExpress.XtraEditors.LabelControl labDate;
        private DevExpress.XtraEditors.DateEdit dtpChargingCloseDate;
    }
}

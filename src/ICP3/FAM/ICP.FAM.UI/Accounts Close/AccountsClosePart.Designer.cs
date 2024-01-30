namespace ICP.FAM.UI
{
    partial class AccountsClosePart
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
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labChargingCloseDate = new DevExpress.XtraEditors.LabelControl();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.dtpChargingCloseDate = new DevExpress.XtraEditors.DateEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labAccountingClosingDate = new DevExpress.XtraEditors.LabelControl();
            this.dtpAccountingClosingDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAccountingClosingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAccountingClosingDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(11, 12);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "Company";
            // 
            // labChargingCloseDate
            // 
            this.labChargingCloseDate.Location = new System.Drawing.Point(11, 45);
            this.labChargingCloseDate.Name = "labChargingCloseDate";
            this.labChargingCloseDate.Size = new System.Drawing.Size(109, 14);
            this.labChargingCloseDate.TabIndex = 0;
            this.labChargingCloseDate.Text = "Charging Close Date";
            // 
            // cmbCompany
            // 
            this.cmbCompany.Location = new System.Drawing.Point(147, 9);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(144, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // dtpChargingCloseDate
            // 
            this.dtpChargingCloseDate.EditValue = null;
            this.dtpChargingCloseDate.Location = new System.Drawing.Point(147, 42);
            this.dtpChargingCloseDate.Name = "dtpChargingCloseDate";
            this.dtpChargingCloseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpChargingCloseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpChargingCloseDate.Size = new System.Drawing.Size(144, 21);
            this.dtpChargingCloseDate.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(43, 116);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(176, 116);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labAccountingClosingDate
            // 
            this.labAccountingClosingDate.Location = new System.Drawing.Point(11, 81);
            this.labAccountingClosingDate.Name = "labAccountingClosingDate";
            this.labAccountingClosingDate.Size = new System.Drawing.Size(133, 14);
            this.labAccountingClosingDate.TabIndex = 0;
            this.labAccountingClosingDate.Text = "Accounting Closing Date";
            // 
            // dtpAccountingClosingDate
            // 
            this.dtpAccountingClosingDate.EditValue = null;
            this.dtpAccountingClosingDate.Location = new System.Drawing.Point(147, 78);
            this.dtpAccountingClosingDate.Name = "dtpAccountingClosingDate";
            this.dtpAccountingClosingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpAccountingClosingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpAccountingClosingDate.Size = new System.Drawing.Size(144, 21);
            this.dtpAccountingClosingDate.TabIndex = 2;
            // 
            // AccountsClosePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpChargingCloseDate);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.dtpAccountingClosingDate);
            this.Controls.Add(this.labChargingCloseDate);
            this.Controls.Add(this.labAccountingClosingDate);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Name = "AccountsClosePart";
            this.Size = new System.Drawing.Size(316, 150);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChargingCloseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAccountingClosingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAccountingClosingDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labChargingCloseDate;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.DateEdit dtpChargingCloseDate;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labAccountingClosingDate;
        private DevExpress.XtraEditors.DateEdit dtpAccountingClosingDate;
    }
}

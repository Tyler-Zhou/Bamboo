namespace ICP.FAM.UI
{
    partial class InvoiceCountPart
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
            this.labBeginDate = new DevExpress.XtraEditors.LabelControl();
            this.labEndDate = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.dtpBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpEndDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(16, 14);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(48, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "所属公司";
            // 
            // labBeginDate
            // 
            this.labBeginDate.Location = new System.Drawing.Point(16, 44);
            this.labBeginDate.Name = "labBeginDate";
            this.labBeginDate.Size = new System.Drawing.Size(48, 14);
            this.labBeginDate.TabIndex = 0;
            this.labBeginDate.Text = "开始日期";
            // 
            // labEndDate
            // 
            this.labEndDate.Location = new System.Drawing.Point(16, 74);
            this.labEndDate.Name = "labEndDate";
            this.labEndDate.Size = new System.Drawing.Size(48, 14);
            this.labEndDate.TabIndex = 0;
            this.labEndDate.Text = "结束日期";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(50, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(157, 111);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbCompany
            // 
            this.cmbCompany.Location = new System.Drawing.Point(96, 11);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(152, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 2;
            // 
            // dtpBeginDate
            // 
            this.dtpBeginDate.EditValue = null;
            this.dtpBeginDate.Location = new System.Drawing.Point(96, 41);
            this.dtpBeginDate.Name = "dtpBeginDate";
            this.dtpBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpBeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpBeginDate.Size = new System.Drawing.Size(152, 21);
            this.dtpBeginDate.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.EditValue = null;
            this.dtpEndDate.Location = new System.Drawing.Point(96, 71);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpEndDate.Size = new System.Drawing.Size(152, 21);
            this.dtpEndDate.TabIndex = 2;
            // 
            // InvoiceCountPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpBeginDate);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labEndDate);
            this.Controls.Add(this.labBeginDate);
            this.Controls.Add(this.labCompany);
            this.Name = "InvoiceCountPart";
            this.Size = new System.Drawing.Size(270, 144);
            this.Load += new System.EventHandler(this.InvoiceCountPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.LabelControl labBeginDate;
        private DevExpress.XtraEditors.LabelControl labEndDate;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.DateEdit dtpBeginDate;
        private DevExpress.XtraEditors.DateEdit dtpEndDate;
    }
}

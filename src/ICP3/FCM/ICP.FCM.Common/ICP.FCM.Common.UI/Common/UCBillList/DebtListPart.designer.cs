namespace ICP.FCM.Common.UI.UCDebtList
{
    partial class UCDebtListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDebtListPart));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoShow = new DevExpress.XtraEditors.RadioGroup();
            this.cmbCustomer = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoShow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.grpCompany.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpCompany);
            this.panel1.Controls.Add(this.cmbCustomer);
            this.panel1.Controls.Add(this.btnShow);
            this.panel1.Controls.Add(this.labBillTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 51);
            this.panel1.TabIndex = 7;
            // 
            // rdoShow
            // 
            this.rdoShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoShow.EditValue = true;
            this.rdoShow.Location = new System.Drawing.Point(8, 16);
            this.rdoShow.Name = "rdoShow";
            this.rdoShow.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Local"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "All")});
            this.rdoShow.Size = new System.Drawing.Size(178, 25);
            this.rdoShow.TabIndex = 12;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Location = new System.Drawing.Point(76, 18);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomer.Size = new System.Drawing.Size(354, 21);
            this.cmbCustomer.TabIndex = 7;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(651, 18);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(81, 21);
            this.btnShow.TabIndex = 9;
            this.btnShow.Text = "&Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click_1);
            // 
            // labBillTo
            // 
            this.labBillTo.Location = new System.Drawing.Point(7, 21);
            this.labBillTo.Name = "labBillTo";
            this.labBillTo.Size = new System.Drawing.Size(28, 14);
            this.labBillTo.TabIndex = 8;
            this.labBillTo.Text = "BillTo";
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 51);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(744, 176);
            this.pnlMain.TabIndex = 8;
            // 
            // grpCompany
            // 
            this.grpCompany.Controls.Add(this.rdoShow);
            this.grpCompany.Location = new System.Drawing.Point(448, 3);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Size = new System.Drawing.Size(192, 47);
            this.grpCompany.TabIndex = 0;
            this.grpCompany.TabStop = false;
            this.grpCompany.Text = "Company";
            // 
            // UCDebtListPart
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Enabled = false;
            this.Name = "UCDebtListPart";
            this.Size = new System.Drawing.Size(744, 227);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoShow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.grpCompany.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCustomer;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.RadioGroup rdoShow;
        private System.Windows.Forms.GroupBox grpCompany;

    }
}

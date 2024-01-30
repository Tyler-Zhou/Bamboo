namespace ICP.FAM.UI.ReleaseBL
{
    partial class ReleaseBLDebtListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseBLDebtListPart));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbCustomer = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.grbCompany = new System.Windows.Forms.GroupBox();
            this.rdoShow = new DevExpress.XtraEditors.RadioGroup();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.grbCompany.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoShow.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grbCompany);
            this.panel1.Controls.Add(this.cmbCustomer);
            this.panel1.Controls.Add(this.btnShow);
            this.panel1.Controls.Add(this.labBillTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 50);
            this.panel1.TabIndex = 7;
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
            this.btnShow.Location = new System.Drawing.Point(646, 18);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(739, 177);
            this.pnlMain.TabIndex = 8;
            // 
            // grbCompany
            // 
            this.grbCompany.Controls.Add(this.rdoShow);
            this.grbCompany.Location = new System.Drawing.Point(441, 3);
            this.grbCompany.Name = "grbCompany";
            this.grbCompany.Size = new System.Drawing.Size(194, 46);
            this.grbCompany.TabIndex = 0;
            this.grbCompany.TabStop = false;
            this.grbCompany.Text = "Company";
            // 
            // rdoShow
            // 
            this.rdoShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoShow.EditValue = true;
            this.rdoShow.Location = new System.Drawing.Point(12, 15);
            this.rdoShow.Name = "rdoShow";
            this.rdoShow.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Local"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "All")});
            this.rdoShow.Size = new System.Drawing.Size(176, 25);
            this.rdoShow.TabIndex = 12;
            // 
            // ReleaseBLDebtListPart
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Enabled = false;
            this.Name = "ReleaseBLDebtListPart";
            this.Size = new System.Drawing.Size(739, 227);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.grbCompany.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdoShow.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCustomer;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private System.Windows.Forms.GroupBox grbCompany;
        private DevExpress.XtraEditors.RadioGroup rdoShow;

    }
}

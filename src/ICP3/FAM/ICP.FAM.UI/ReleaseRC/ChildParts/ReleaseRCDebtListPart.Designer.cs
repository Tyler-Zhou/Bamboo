namespace ICP.FAM.UI.ReleaseRC
{
    partial class ReleaseRCDebtListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseRCDebtListPart));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbCustomer = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.ReportWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbCustomer);
            this.panel1.Controls.Add(this.btnShow);
            this.panel1.Controls.Add(this.labBillTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 33);
            this.panel1.TabIndex = 7;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Location = new System.Drawing.Point(76, 6);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomer.Size = new System.Drawing.Size(354, 21);
            this.cmbCustomer.TabIndex = 7;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(446, 6);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(81, 21);
            this.btnShow.TabIndex = 9;
            this.btnShow.Text = "&Show";
            // 
            // labBillTo
            // 
            this.labBillTo.Location = new System.Drawing.Point(7, 11);
            this.labBillTo.Name = "labBillTo";
            this.labBillTo.Size = new System.Drawing.Size(28, 14);
            this.labBillTo.TabIndex = 8;
            this.labBillTo.Text = "BillTo";
            // 
            // ReportWorkspace
            // 
            this.ReportWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportWorkspace.Location = new System.Drawing.Point(0, 33);
            this.ReportWorkspace.Name = "ReportWorkspace";
            this.ReportWorkspace.Size = new System.Drawing.Size(640, 194);
            this.ReportWorkspace.TabIndex = 8;
            this.ReportWorkspace.Text = "deckWorkspace1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ICP.FAM.UI.Properties.Resources.debts;
            this.pictureBox1.Location = new System.Drawing.Point(0, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 194);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // ReleaseBLDebtListPart
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ReportWorkspace);
            this.Controls.Add(this.panel1);
            this.Enabled = false;
            this.Name = "ReleaseBLDebtListPart";
            this.Size = new System.Drawing.Size(640, 227);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCustomer;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ReportWorkspace;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

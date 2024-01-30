namespace ICP.FCM.AirImport.UI.Report
{
    partial class OIBLPrintPart2
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
            this.groupStyle = new System.Windows.Forms.GroupBox();
            this.labHBLNo = new DevExpress.XtraEditors.LabelControl();
            this.cmbHBLNo = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            this.groupStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 432);
            this.pnlQuery.Size = new System.Drawing.Size(210, 39);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(49, 10);
            this.btnQuery.Text = "查询";
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.groupStyle);
            this.pnlCondition.Size = new System.Drawing.Size(210, 471);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(791, 471);
            // 
            // groupStyle
            // 
            this.groupStyle.Controls.Add(this.labHBLNo);
            this.groupStyle.Controls.Add(this.cmbHBLNo);
            this.groupStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupStyle.Location = new System.Drawing.Point(0, 0);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Size = new System.Drawing.Size(210, 161);
            this.groupStyle.TabIndex = 3;
            this.groupStyle.TabStop = false;
            // 
            // labHBLNo
            // 
            this.labHBLNo.Location = new System.Drawing.Point(6, 24);
            this.labHBLNo.Name = "labHBLNo";
            this.labHBLNo.Size = new System.Drawing.Size(40, 14);
            this.labHBLNo.TabIndex = 0;
            this.labHBLNo.Text = "HBL No";
            // 
            // cmbHBLNo
            // 
            this.cmbHBLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbHBLNo.Location = new System.Drawing.Point(65, 21);
            this.cmbHBLNo.Name = "cmbHBLNo";
            this.cmbHBLNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbHBLNo.Properties.Appearance.Options.UseBackColor = true;
            this.cmbHBLNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHBLNo.Size = new System.Drawing.Size(137, 21);
            this.cmbHBLNo.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbHBLNo.TabIndex = 3;
            // 
            // OIBLPrintPart2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "OIBLPrintPart2";
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            this.groupStyle.ResumeLayout(false);
            this.groupStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHBLNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupStyle;
        private DevExpress.XtraEditors.LabelControl labHBLNo;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbHBLNo;
    }
}

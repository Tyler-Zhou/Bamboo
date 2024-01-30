namespace ICP.ReportCenter.UI.Comm.Controls
{
    partial class OperationDateByYearPart
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
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.rdoSpecify = new System.Windows.Forms.RadioButton();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.rdoCustom = new System.Windows.Forms.RadioButton();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.rdoLastYear = new System.Windows.Forms.RadioButton();
            this.rdoThisYear = new System.Windows.Forms.RadioButton();
            this.cmbMonthType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = null;
            this.dteTo.Enabled = false;
            this.dteTo.Location = new System.Drawing.Point(60, 98);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(134, 21);
            this.dteTo.TabIndex = 21;
            // 
            // rdoSpecify
            // 
            this.rdoSpecify.AutoSize = true;
            this.rdoSpecify.Location = new System.Drawing.Point(3, 51);
            this.rdoSpecify.Name = "rdoSpecify";
            this.rdoSpecify.Size = new System.Drawing.Size(61, 18);
            this.rdoSpecify.TabIndex = 26;
            this.rdoSpecify.Text = "自定义";
            this.rdoSpecify.UseVisualStyleBackColor = true;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = null;
            this.dteFrom.Enabled = false;
            this.dteFrom.Location = new System.Drawing.Point(60, 71);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(134, 21);
            this.dteFrom.TabIndex = 20;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(3, 101);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(12, 14);
            this.labTo.TabIndex = 22;
            this.labTo.Text = "到";
            // 
            // rdoCustom
            // 
            this.rdoCustom.AutoSize = true;
            this.rdoCustom.Location = new System.Drawing.Point(3, 31);
            this.rdoCustom.Name = "rdoCustom";
            this.rdoCustom.Size = new System.Drawing.Size(14, 13);
            this.rdoCustom.TabIndex = 27;
            this.rdoCustom.UseVisualStyleBackColor = true;
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(3, 74);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(12, 14);
            this.labFrom.TabIndex = 23;
            this.labFrom.Text = "从";
            // 
            // rdoLastYear
            // 
            this.rdoLastYear.AutoSize = true;
            this.rdoLastYear.Location = new System.Drawing.Point(92, 3);
            this.rdoLastYear.Name = "rdoLastYear";
            this.rdoLastYear.Size = new System.Drawing.Size(49, 18);
            this.rdoLastYear.TabIndex = 25;
            this.rdoLastYear.Text = "去年";
            this.rdoLastYear.UseVisualStyleBackColor = true;
            // 
            // rdoThisYear
            // 
            this.rdoThisYear.AutoSize = true;
            this.rdoThisYear.Checked = true;
            this.rdoThisYear.Location = new System.Drawing.Point(3, 3);
            this.rdoThisYear.Name = "rdoThisYear";
            this.rdoThisYear.Size = new System.Drawing.Size(49, 18);
            this.rdoThisYear.TabIndex = 24;
            this.rdoThisYear.TabStop = true;
            this.rdoThisYear.Text = "今年";
            this.rdoThisYear.UseVisualStyleBackColor = true;
            // 
            // cmbMonthType
            // 
            this.cmbMonthType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMonthType.Enabled = false;
            this.cmbMonthType.Location = new System.Drawing.Point(60, 28);
            this.cmbMonthType.Name = "cmbMonthType";
            this.cmbMonthType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMonthType.Size = new System.Drawing.Size(134, 21);
            this.cmbMonthType.TabIndex = 28;
            // 
            // OperationDateByYearPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbMonthType);
            this.Controls.Add(this.dteTo);
            this.Controls.Add(this.rdoSpecify);
            this.Controls.Add(this.dteFrom);
            this.Controls.Add(this.labTo);
            this.Controls.Add(this.rdoCustom);
            this.Controls.Add(this.labFrom);
            this.Controls.Add(this.rdoLastYear);
            this.Controls.Add(this.rdoThisYear);
            this.Name = "OperationDateByYearPart";
            this.Size = new System.Drawing.Size(198, 129);
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dteTo;
        private System.Windows.Forms.RadioButton rdoSpecify;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labTo;
        private System.Windows.Forms.RadioButton rdoCustom;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private System.Windows.Forms.RadioButton rdoLastYear;
        private System.Windows.Forms.RadioButton rdoThisYear;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbMonthType;
    }
}

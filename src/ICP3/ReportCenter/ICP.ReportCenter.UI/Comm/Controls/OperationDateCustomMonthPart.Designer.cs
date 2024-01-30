namespace ICP.ReportCenter.UI.Comm.Controls
{
    partial class OperationDateCustomMonthPart
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
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.rdoLastMonth = new System.Windows.Forms.RadioButton();
            this.rdoThisMonth = new System.Windows.Forms.RadioButton();
            this.rdoThreeMonths = new System.Windows.Forms.RadioButton();
            this.rdoSixMonths = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
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
            this.dteTo.Size = new System.Drawing.Size(127, 21);
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
            this.dteFrom.Size = new System.Drawing.Size(127, 21);
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
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(3, 74);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(12, 14);
            this.labFrom.TabIndex = 23;
            this.labFrom.Text = "从";
            // 
            // rdoLastMonth
            // 
            this.rdoLastMonth.AutoSize = true;
            this.rdoLastMonth.Checked = true;
            this.rdoLastMonth.Location = new System.Drawing.Point(92, 3);
            this.rdoLastMonth.Name = "rdoLastMonth";
            this.rdoLastMonth.Size = new System.Drawing.Size(49, 18);
            this.rdoLastMonth.TabIndex = 25;
            this.rdoLastMonth.TabStop = true;
            this.rdoLastMonth.Text = "上月";
            this.rdoLastMonth.UseVisualStyleBackColor = true;
            // 
            // rdoThisMonth
            // 
            this.rdoThisMonth.AutoSize = true;
            this.rdoThisMonth.Location = new System.Drawing.Point(3, 3);
            this.rdoThisMonth.Name = "rdoThisMonth";
            this.rdoThisMonth.Size = new System.Drawing.Size(49, 18);
            this.rdoThisMonth.TabIndex = 24;
            this.rdoThisMonth.Text = "本月";
            this.rdoThisMonth.UseVisualStyleBackColor = true;
            // 
            // rdoThreeMonths
            // 
            this.rdoThreeMonths.AutoSize = true;
            this.rdoThreeMonths.Location = new System.Drawing.Point(3, 27);
            this.rdoThreeMonths.Name = "rdoThreeMonths";
            this.rdoThreeMonths.Size = new System.Drawing.Size(73, 18);
            this.rdoThreeMonths.TabIndex = 24;
            this.rdoThreeMonths.Text = "前三个月";
            this.rdoThreeMonths.UseVisualStyleBackColor = true;
            // 
            // rdoSixMonths
            // 
            this.rdoSixMonths.AutoSize = true;
            this.rdoSixMonths.Location = new System.Drawing.Point(92, 27);
            this.rdoSixMonths.Name = "rdoSixMonths";
            this.rdoSixMonths.Size = new System.Drawing.Size(73, 18);
            this.rdoSixMonths.TabIndex = 24;
            this.rdoSixMonths.Text = "前六个月";
            this.rdoSixMonths.UseVisualStyleBackColor = true;
            // 
            // OperationDateCustomMonthPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dteTo);
            this.Controls.Add(this.rdoSpecify);
            this.Controls.Add(this.dteFrom);
            this.Controls.Add(this.labTo);
            this.Controls.Add(this.labFrom);
            this.Controls.Add(this.rdoLastMonth);
            this.Controls.Add(this.rdoSixMonths);
            this.Controls.Add(this.rdoThreeMonths);
            this.Controls.Add(this.rdoThisMonth);
            this.Name = "OperationDateCustomMonthPart";
            this.Size = new System.Drawing.Size(191, 122);
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dteTo;
        private System.Windows.Forms.RadioButton rdoSpecify;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private System.Windows.Forms.RadioButton rdoLastMonth;
        private System.Windows.Forms.RadioButton rdoThisMonth;
        private System.Windows.Forms.RadioButton rdoThreeMonths;
        private System.Windows.Forms.RadioButton rdoSixMonths;
    }
}

namespace ICP.ReportCenter.UI.Comm.Controls
{
    partial class OperationDateByWeekPart
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
            this.cmbMonth = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbYear = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.rdoSpecify = new System.Windows.Forms.RadioButton();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.rdoCustom = new System.Windows.Forms.RadioButton();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.rdoCurrentWeek = new System.Windows.Forms.RadioButton();
            this.rdoThisMonth = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMonth
            // 
            this.cmbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMonth.Location = new System.Drawing.Point(140, 27);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMonth.Size = new System.Drawing.Size(46, 21);
            this.cmbMonth.TabIndex = 29;
            // 
            // cmbYear
            // 
            this.cmbYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbYear.Location = new System.Drawing.Point(60, 27);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbYear.Size = new System.Drawing.Size(75, 21);
            this.cmbYear.TabIndex = 28;
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
            // rdoCurrentWeek
            // 
            this.rdoCurrentWeek.AutoSize = true;
            this.rdoCurrentWeek.Checked = true;
            this.rdoCurrentWeek.Location = new System.Drawing.Point(92, 3);
            this.rdoCurrentWeek.Name = "rdoCurrentWeek";
            this.rdoCurrentWeek.Size = new System.Drawing.Size(49, 18);
            this.rdoCurrentWeek.TabIndex = 25;
            this.rdoCurrentWeek.TabStop = true;
            this.rdoCurrentWeek.Text = "本周";
            this.rdoCurrentWeek.UseVisualStyleBackColor = true;
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
            // OperationDateByWeekPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.dteTo);
            this.Controls.Add(this.rdoSpecify);
            this.Controls.Add(this.dteFrom);
            this.Controls.Add(this.labTo);
            this.Controls.Add(this.rdoCustom);
            this.Controls.Add(this.labFrom);
            this.Controls.Add(this.rdoCurrentWeek);
            this.Controls.Add(this.rdoThisMonth);
            this.Name = "OperationDateByWeekPart";
            this.Size = new System.Drawing.Size(191, 122);
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ImageComboBoxEdit cmbMonth;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbYear;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private System.Windows.Forms.RadioButton rdoSpecify;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labTo;
        private System.Windows.Forms.RadioButton rdoCustom;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private System.Windows.Forms.RadioButton rdoCurrentWeek;
        private System.Windows.Forms.RadioButton rdoThisMonth;
    }
}

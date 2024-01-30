namespace ICP.FAM.UI.WriteOff.Dialogs
{
    partial class AutoBillsFinderWizard
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
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnNextStep = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(112, 33);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.spinEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(100, 21);
            this.spinEdit1.TabIndex = 24;
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(112, 73);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("联通支行RMB", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("联通支行USD", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("香港汇丰沙田HKD", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("香港汇丰沙田USD", null, -1)});
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(221, 21);
            this.imageComboBoxEdit1.TabIndex = 29;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(46, 39);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "金额";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(112, 114);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(221, 21);
            this.textEdit2.TabIndex = 28;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(46, 76);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 27;
            this.labelControl9.Text = "银行";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(46, 117);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(36, 14);
            this.labelControl7.TabIndex = 26;
            this.labelControl7.Text = "业务号";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(339, 117);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(34, 14);
            this.labelControl1.TabIndex = 30;
            this.labelControl1.Text = "(可选)";
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(339, 194);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(197, 33);
            this.btnNextStep.TabIndex = 31;
            this.btnNextStep.Text = "开始智能查找账单";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(46, 151);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(324, 14);
            this.labelControl2.TabIndex = 32;
            this.labelControl2.Text = "系统会根据业务关系和规则，未您查找账单并提供匹配方案。";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ICP.FAM.UI.Properties.Resources.BrightFinder1;
            this.pictureBox1.Location = new System.Drawing.Point(420, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 137);
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // AutoBillsFinderWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnNextStep);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.imageComboBoxEdit1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl7);
            this.Name = "AutoBillsFinderWizard";
            this.Size = new System.Drawing.Size(586, 294);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnNextStep;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

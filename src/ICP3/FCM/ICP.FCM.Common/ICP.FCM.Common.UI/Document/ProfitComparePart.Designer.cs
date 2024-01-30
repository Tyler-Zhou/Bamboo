namespace ICP.FCM.Common.UI.Document
{
    partial class ProfitComparePart
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
            this.cmbProfitCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblNewProfitValue = new System.Windows.Forms.Label();
            this.lblNewProfitCaption = new System.Windows.Forms.Label();
            this.lblOldProfitValue = new System.Windows.Forms.Label();
            this.lblOldProfitCaption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProfitCurrency.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProfitCurrency
            // 
            this.cmbProfitCurrency.Location = new System.Drawing.Point(8, 1);
            this.cmbProfitCurrency.Name = "cmbProfitCurrency";
            this.cmbProfitCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProfitCurrency.Properties.NullText = "USD";
            this.cmbProfitCurrency.Size = new System.Drawing.Size(73, 21);
            this.cmbProfitCurrency.TabIndex = 13;
            // 
            // lblNewProfitValue
            // 
            this.lblNewProfitValue.AutoSize = true;
            this.lblNewProfitValue.BackColor = System.Drawing.Color.Transparent;
            this.lblNewProfitValue.Location = new System.Drawing.Point(362, 4);
            this.lblNewProfitValue.Name = "lblNewProfitValue";
            this.lblNewProfitValue.Size = new System.Drawing.Size(25, 14);
            this.lblNewProfitValue.TabIndex = 12;
            this.lblNewProfitValue.Text = "0.0";
            // 
            // lblNewProfitCaption
            // 
            this.lblNewProfitCaption.AutoSize = true;
            this.lblNewProfitCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNewProfitCaption.Location = new System.Drawing.Point(267, 4);
            this.lblNewProfitCaption.Name = "lblNewProfitCaption";
            this.lblNewProfitCaption.Size = new System.Drawing.Size(67, 14);
            this.lblNewProfitCaption.TabIndex = 11;
            this.lblNewProfitCaption.Text = "新利润值：";
            // 
            // lblOldProfitValue
            // 
            this.lblOldProfitValue.AutoSize = true;
            this.lblOldProfitValue.BackColor = System.Drawing.Color.Transparent;
            this.lblOldProfitValue.Location = new System.Drawing.Point(178, 4);
            this.lblOldProfitValue.Name = "lblOldProfitValue";
            this.lblOldProfitValue.Size = new System.Drawing.Size(25, 14);
            this.lblOldProfitValue.TabIndex = 10;
            this.lblOldProfitValue.Text = "0.0";
            // 
            // lblOldProfitCaption
            // 
            this.lblOldProfitCaption.AutoSize = true;
            this.lblOldProfitCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblOldProfitCaption.Location = new System.Drawing.Point(92, 4);
            this.lblOldProfitCaption.Name = "lblOldProfitCaption";
            this.lblOldProfitCaption.Size = new System.Drawing.Size(67, 14);
            this.lblOldProfitCaption.TabIndex = 9;
            this.lblOldProfitCaption.Text = "旧利润值：";
            // 
            // ProfitComparePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbProfitCurrency);
            this.Controls.Add(this.lblNewProfitValue);
            this.Controls.Add(this.lblNewProfitCaption);
            this.Controls.Add(this.lblOldProfitValue);
            this.Controls.Add(this.lblOldProfitCaption);
            this.Name = "ProfitComparePart";
            this.Size = new System.Drawing.Size(447, 22);
            this.Load += new System.EventHandler(this.ProfitComparePart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbProfitCurrency.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ImageComboBoxEdit cmbProfitCurrency;
        protected System.Windows.Forms.Label lblNewProfitValue;
        protected System.Windows.Forms.Label lblNewProfitCaption;
        protected System.Windows.Forms.Label lblOldProfitValue;
        protected System.Windows.Forms.Label lblOldProfitCaption;
    }
}

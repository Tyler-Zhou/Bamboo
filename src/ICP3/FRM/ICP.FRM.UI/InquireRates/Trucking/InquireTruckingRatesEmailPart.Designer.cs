namespace ICP.FRM.UI.InquireRates
{
    partial class InquireTruckingRatesEmailPart
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
            this.EmailPart = new ICP.FRM.UI.InquireRates.InquireRatesEmailPart();
            this.SuspendLayout();
            // 
            // EmailPart
            // 
            this.EmailPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmailPart.Location = new System.Drawing.Point(0, 0);
            this.EmailPart.Name = "EmailPart";
            this.EmailPart.Presenter = null;
            this.EmailPart.Size = new System.Drawing.Size(603, 441);
            this.EmailPart.TabIndex = 0;
            // 
            // InquireTruckingRatesEmailPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EmailPart);
            this.Name = "InquireTruckingRatesEmailPart";
            this.Size = new System.Drawing.Size(230, 226);
            this.ResumeLayout(false);

        }

        #endregion

        private InquireRatesEmailPart EmailPart;
    }
}

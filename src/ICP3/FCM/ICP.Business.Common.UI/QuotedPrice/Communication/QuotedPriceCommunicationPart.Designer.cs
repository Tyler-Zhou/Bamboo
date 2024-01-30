namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class QuotedPriceCommunicationPart
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
            this.ucCommunicationPart = new ICP.Business.Common.UI.Communication.UCCommunicationHistory();
            this.SuspendLayout();
            // 
            // ucCommunicationPart
            // 
            this.ucCommunicationPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCommunicationPart.Location = new System.Drawing.Point(0, 0);
            this.ucCommunicationPart.Name = "ucCommunicationPart";
            this.ucCommunicationPart.Presenter = null;
            this.ucCommunicationPart.Size = new System.Drawing.Size(601, 409);
            this.ucCommunicationPart.TabIndex = 0;
            this.ucCommunicationPart.Workitem = null;
            // 
            // QuotedPriceCommunicationPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucCommunicationPart);
            this.Name = "QuotedPriceCommunicationPart";
            this.ResumeLayout(false);

        }

        #endregion

        private Communication.UCCommunicationHistory ucCommunicationPart;
    }
}

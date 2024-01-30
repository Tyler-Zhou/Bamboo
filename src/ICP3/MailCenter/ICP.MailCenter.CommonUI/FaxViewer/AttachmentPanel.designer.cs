namespace ICP.MailCenter.CommonUI
{
    partial class AttachmentPanel
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
            this.SuspendLayout();
            // 
            // AttachmentPanel
            // 
            this.AutoScroll = true;
            this.AutoSize = true;
            this.DoubleBuffered = true;
            this.HorizontalScroll.Visible = false;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.MinimumSize = new System.Drawing.Size(200, 30);
            this.MouseEnter += new System.EventHandler(this.AttachmentPanel_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

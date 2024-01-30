namespace ICP.Sys.UI.SystemHelp
{
    partial class FeedbackMainWorkspce
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
            this.FeedbackMainWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.SuspendLayout();
            // 
            // FeedbackMainWorkspace
            // 
            this.FeedbackMainWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeedbackMainWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FeedbackMainWorkspace.Name = "FeedbackMainWorkspace";
            this.FeedbackMainWorkspace.Size = new System.Drawing.Size(601, 409);
            this.FeedbackMainWorkspace.TabIndex = 1;
            this.FeedbackMainWorkspace.Text = "deckWorkspace1";
            // 
            // FeedbackMainWorkspce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FeedbackMainWorkspace);
            this.Name = "FeedbackMainWorkspce";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FeedbackMainWorkspace;
    }
}

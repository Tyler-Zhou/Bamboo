namespace ICP.Business.Common.UI.Communication
{
    partial class UCCommunicationHistory
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
            this.ucCommunicationHistoryList = new ICP.Business.Common.UI.Communication.UCCommunicationHistoryList();
            this.ucCommunicationHistoryOperationBar = new ICP.Business.Common.UI.Communication.UCCommunicationHistoryOperationBar();
            this.SuspendLayout();
            // 
            // ucCommunicationHistoryList
            // 
            this.ucCommunicationHistoryList.DataSource = null;
            this.ucCommunicationHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCommunicationHistoryList.Location = new System.Drawing.Point(0, 25);
            this.ucCommunicationHistoryList.Name = "ucCommunicationHistoryList";
            this.ucCommunicationHistoryList.Size = new System.Drawing.Size(702, 104);
            this.ucCommunicationHistoryList.TabIndex = 1;
            // 
            // ucCommunicationHistoryOperationBar
            // 
            this.ucCommunicationHistoryOperationBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucCommunicationHistoryOperationBar.ListPresenter = null;
            this.ucCommunicationHistoryOperationBar.Location = new System.Drawing.Point(0, 0);
            this.ucCommunicationHistoryOperationBar.Name = "ucCommunicationHistoryOperationBar";
            this.ucCommunicationHistoryOperationBar.Size = new System.Drawing.Size(702, 25);
            this.ucCommunicationHistoryOperationBar.TabIndex = 0;
            this.ucCommunicationHistoryOperationBar.ucList = null;
            // 
            // UCCommunicationHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucCommunicationHistoryList);
            this.Controls.Add(this.ucCommunicationHistoryOperationBar);
            this.Name = "UCCommunicationHistory";
            this.Size = new System.Drawing.Size(702, 129);
            this.ResumeLayout(false);

        }

        #endregion

        private UCCommunicationHistoryOperationBar ucCommunicationHistoryOperationBar;
        private UCCommunicationHistoryList ucCommunicationHistoryList;
    }
}

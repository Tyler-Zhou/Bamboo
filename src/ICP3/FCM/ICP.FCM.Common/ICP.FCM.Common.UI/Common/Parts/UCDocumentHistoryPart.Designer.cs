namespace ICP.FCM.Common.UI.Common.Parts
{
    partial class UCDocumentHistoryPart
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
            this.grpControlHistory = new DevExpress.XtraEditors.GroupControl();
            this.pnlControlHistory = new DevExpress.XtraEditors.PanelControl();
            this.grpControlCurrent = new DevExpress.XtraEditors.GroupControl();
            this.pnlControlCurrent = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpControlHistory)).BeginInit();
            this.grpControlHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControlHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControlCurrent)).BeginInit();
            this.grpControlCurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControlCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControlHistory
            // 
            this.grpControlHistory.Controls.Add(this.pnlControlHistory);
            this.grpControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpControlHistory.Location = new System.Drawing.Point(0, 0);
            this.grpControlHistory.Name = "grpControlHistory";
            this.grpControlHistory.Size = new System.Drawing.Size(400, 372);
            this.grpControlHistory.TabIndex = 0;
            this.grpControlHistory.Text = "Dispatch Document History List";
            // 
            // pnlControlHistory
            // 
            this.pnlControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControlHistory.Location = new System.Drawing.Point(2, 23);
            this.pnlControlHistory.Name = "pnlControlHistory";
            this.pnlControlHistory.Size = new System.Drawing.Size(396, 347);
            this.pnlControlHistory.TabIndex = 0;
            // 
            // grpControlCurrent
            // 
            this.grpControlCurrent.Controls.Add(this.pnlControlCurrent);
            this.grpControlCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpControlCurrent.Location = new System.Drawing.Point(0, 0);
            this.grpControlCurrent.Name = "grpControlCurrent";
            this.grpControlCurrent.Size = new System.Drawing.Size(427, 372);
            this.grpControlCurrent.TabIndex = 1;
            this.grpControlCurrent.Text = "Dispatch Document Current List";
            // 
            // pnlControlCurrent
            // 
            this.pnlControlCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControlCurrent.Location = new System.Drawing.Point(2, 23);
            this.pnlControlCurrent.Name = "pnlControlCurrent";
            this.pnlControlCurrent.Size = new System.Drawing.Size(423, 347);
            this.pnlControlCurrent.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grpControlHistory);
            this.splitContainerControl1.Panel1.MinSize = 300;
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.grpControlCurrent);
            this.splitContainerControl1.Panel2.MinSize = 300;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(833, 372);
            this.splitContainerControl1.SplitterPosition = 400;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // UCDocumentHistoryPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "UCDocumentHistoryPart";
            this.Size = new System.Drawing.Size(833, 372);
            this.Resize += new System.EventHandler(this.UCDocumentDispatchPart_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grpControlHistory)).EndInit();
            this.grpControlHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlControlHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControlCurrent)).EndInit();
            this.grpControlCurrent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlControlCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpControlHistory;
        private DevExpress.XtraEditors.GroupControl grpControlCurrent;
        private DevExpress.XtraEditors.PanelControl pnlControlHistory;
        private DevExpress.XtraEditors.PanelControl pnlControlCurrent;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}

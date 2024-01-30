namespace ICP.Common.UI.ReportView
{
    partial class FastReportViewerPart
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
            this.report1 = new FastReport.Report();
            this.previewControl1 = new FastReport.Preview.PreviewControl();
            ((System.ComponentModel.ISupportInitialize)(this.report1)).BeginInit();
            this.SuspendLayout();
            // 
            // report1
            // 
            this.report1.ReportResourceString = "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Report ReportInfo.Created=\"09/27/2010 1" +
                "4:12:23\" ReportInfo.Modified=\"06/15/2012 12:08:21\" ReportInfo.CreatorVersion=\"1." +
                "2.47.0\">\r\n  <Dictionary/>\r\n</Report>\r\n";
            // 
            // previewControl1
            // 
            this.previewControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewControl1.DefaultEdit = false;
            this.previewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.previewControl1.Location = new System.Drawing.Point(0, 0);
            this.previewControl1.Name = "previewControl1";
            this.previewControl1.ShowRefreshButton = true;
            this.previewControl1.Size = new System.Drawing.Size(559, 464);
            this.previewControl1.TabIndex = 0;
            this.previewControl1.UIStyle = FastReport.Utils.UIStyle.Office2003;
            // 
            // FastReportViewerPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewControl1);
            this.Name = "FastReportViewerPart";
            this.Size = new System.Drawing.Size(559, 464);
            ((System.ComponentModel.ISupportInitialize)(this.report1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public FastReport.Report report1;
        public FastReport.Preview.PreviewControl previewControl1;
    }
}

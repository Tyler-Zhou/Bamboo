﻿namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class QuotedPriceView
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
            this.sccMain = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.sccMain)).BeginInit();
            this.sccMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // sccMain
            // 
            this.sccMain.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.sccMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sccMain.Horizontal = false;
            this.sccMain.Location = new System.Drawing.Point(0, 0);
            this.sccMain.Name = "sccMain";
            this.sccMain.Panel1.Text = "Panel1";
            this.sccMain.Panel2.Text = "Panel2";
            this.sccMain.Size = new System.Drawing.Size(820, 437);
            this.sccMain.SplitterPosition = 229;
            this.sccMain.TabIndex = 0;
            this.sccMain.Text = "sccMain";
            // 
            // QuotedPriceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sccMain);
            this.Name = "QuotedPriceView";
            this.Size = new System.Drawing.Size(820, 437);
            ((System.ComponentModel.ISupportInitialize)(this.sccMain)).EndInit();
            this.sccMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl sccMain;
    }
}
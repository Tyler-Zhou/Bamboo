using AxMicrosoft.Office.Interop.OutlookViewCtl;

namespace ICP.MailCenter.UI
{
    partial class EmailListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailListPart));
          //  this.pnlListPart = new System.Windows.Forms.Panel();
            this.axViewCtlEmailList = new AxMicrosoft.Office.Interop.OutlookViewCtl.AxViewCtl();
            //this.pnlListPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axViewCtlEmailList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlListPart
            // 
            //this.pnlListPart.Controls.Add(this.axViewCtlEmailList);
            //this.pnlListPart.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.pnlListPart.ForeColor = Color.Transparent;
            //this.pnlListPart.Location = new System.Drawing.Point(0, 0);
            //this.pnlListPart.Name = "pnlListPart";
            //this.pnlListPart.Size = new System.Drawing.Size(278, 633);
            //this.pnlListPart.TabIndex = 1;
            // 
            // axViewCtl1
            // 
            this.axViewCtlEmailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axViewCtlEmailList.Enabled = true;
            this.axViewCtlEmailList.Location = new System.Drawing.Point(0, 0);
            this.axViewCtlEmailList.Name = "axViewCtl1";
            this.axViewCtlEmailList.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axViewCtl1.OcxState")));
            this.axViewCtlEmailList.Size = new System.Drawing.Size(278, 633);
            this.axViewCtlEmailList.TabIndex = 0;
            // 
            // EmailListPart
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axViewCtlEmailList);
            this.Name = "EmailListPart";
            this.Size = new System.Drawing.Size(278, 633);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
           // this.pnlListPart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axViewCtlEmailList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

       // private Panel pnlListPart;
        public AxViewCtl axViewCtlEmailList;
    }
}

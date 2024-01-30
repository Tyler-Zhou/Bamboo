namespace ICP.Common.Business.ServiceInterface
{
    partial class TabBaseBusinessPart
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
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.listBaseBusinessPart = new ICP.Common.Business.ServiceInterface.ListBaseBusinessPart();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Horizontal = false;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Controls.Add(this.listBaseBusinessPart);
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Controls.Add(this.xtraTabControl);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.Size = new System.Drawing.Size(702, 544);
            this.splitContainerControl.SplitterPositionChanged += new System.EventHandler(splitContainerControl_SplitterPositionChanged);
            this.splitContainerControl.SplitterPosition = 332;
            this.splitContainerControl.TabIndex = 0;
            // 
            // listBaseBusinessPart
            // 
            this.listBaseBusinessPart.AdvanceQueryString = null;
           
           
            this.listBaseBusinessPart.Dock = System.Windows.Forms.DockStyle.Fill;
           
            this.listBaseBusinessPart.FormID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.listBaseBusinessPart.Location = new System.Drawing.Point(0, 0);
            this.listBaseBusinessPart.Name = "listBaseBusinessPart";
            this.listBaseBusinessPart.OperationID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.listBaseBusinessPart.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Unknown;
          
            this.listBaseBusinessPart.SelectedCompanyIds = "";
            this.listBaseBusinessPart.Size = new System.Drawing.Size(702, 332);
            this.listBaseBusinessPart.TabIndex = 0;
            this.listBaseBusinessPart.TemplateCode = null;
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.Size = new System.Drawing.Size(702, 206);
            this.xtraTabControl.TabIndex = 0;
            // 
            // TabBaseBusinessPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl);
            this.Name = "TabBaseBusinessPart";
            this.Size = new System.Drawing.Size(702, 544);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.ResumeLayout(false);

        }

    

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private ListBaseBusinessPart listBaseBusinessPart;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
    }
}

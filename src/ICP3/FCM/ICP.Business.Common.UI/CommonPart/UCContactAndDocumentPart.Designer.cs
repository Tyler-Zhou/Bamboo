namespace ICP.Business.Common.UI.Common
{
    partial class UCContactAndDocumentPart
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
            this.grpAttachment = new DevExpress.XtraEditors.GroupControl();
            this.pnlCustomer = new DevExpress.XtraEditors.PanelControl();
            this.pnlCustomerList = new DevExpress.XtraEditors.PanelControl();
            this.pnlCustomerToolBar = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomer)).BeginInit();
            this.pnlCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerToolBar)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAttachment
            // 
            this.grpAttachment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAttachment.Location = new System.Drawing.Point(0, 250);
            this.grpAttachment.MaximumSize = new System.Drawing.Size(0, 150);
            this.grpAttachment.MinimumSize = new System.Drawing.Size(0, 150);
            this.grpAttachment.Name = "grpAttachment";
            this.grpAttachment.Size = new System.Drawing.Size(886, 150);
            this.grpAttachment.TabIndex = 0;
            this.grpAttachment.Text = "订单附件";
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.Controls.Add(this.pnlCustomerList);
            this.pnlCustomer.Controls.Add(this.pnlCustomerToolBar);
            this.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomer.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(886, 250);
            this.pnlCustomer.TabIndex = 11;
            // 
            // pnlCustomerList
            // 
            this.pnlCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomerList.Location = new System.Drawing.Point(2, 30);
            this.pnlCustomerList.Name = "pnlCustomerList";
            this.pnlCustomerList.Size = new System.Drawing.Size(882, 218);
            this.pnlCustomerList.TabIndex = 0;
            // 
            // pnlCustomerToolBar
            // 
            this.pnlCustomerToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerToolBar.Location = new System.Drawing.Point(2, 2);
            this.pnlCustomerToolBar.Name = "pnlCustomerToolBar";
            this.pnlCustomerToolBar.Size = new System.Drawing.Size(882, 28);
            this.pnlCustomerToolBar.TabIndex = 9;
            // 
            // UCContactAndDocumentPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCustomer);
            this.Controls.Add(this.grpAttachment);
            this.Name = "UCContactAndDocumentPart";
            this.Size = new System.Drawing.Size(886, 400);
            ((System.ComponentModel.ISupportInitialize)(this.grpAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomer)).EndInit();
            this.pnlCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerToolBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpAttachment;
        private DevExpress.XtraEditors.PanelControl pnlCustomer;
        private DevExpress.XtraEditors.PanelControl pnlCustomerToolBar;
        private DevExpress.XtraEditors.PanelControl pnlCustomerList;


    }
}

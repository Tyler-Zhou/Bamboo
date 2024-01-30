namespace ICP.Operation.Common.UI
{
    partial class InternalMailBaseBusinessPart
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
            this.pnlInternalMail = new DevExpress.XtraEditors.PanelControl();
            this.pnlInternalMailRegion = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.baseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInternalMail)).BeginInit();
            this.pnlInternalMail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInternalMailRegion)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInternalMail
            // 
            this.pnlInternalMail.Controls.Add(this.pnlInternalMailRegion);
            this.pnlInternalMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInternalMail.Location = new System.Drawing.Point(0, 24);
            this.pnlInternalMail.Name = "pnlInternalMail";
            this.pnlInternalMail.Size = new System.Drawing.Size(505, 190);
            this.pnlInternalMail.TabIndex = 4;
            // 
            // pnlInternalMailRegion
            // 
            this.pnlInternalMailRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInternalMailRegion.Location = new System.Drawing.Point(2, 2);
            this.pnlInternalMailRegion.Name = "pnlInternalMailRegion";
            this.pnlInternalMailRegion.Size = new System.Drawing.Size(501, 186);
            this.pnlInternalMailRegion.TabIndex = 0;
            // 
            // InternalMailBaseBusinessPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlInternalMail);
            this.Name = "InternalMailBaseBusinessPart";
            this.Controls.SetChildIndex(this.pnlInternalMail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.baseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInternalMail)).EndInit();
            this.pnlInternalMail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlInternalMailRegion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlInternalMail;
        private DevExpress.XtraEditors.PanelControl pnlInternalMailRegion;
    }
}

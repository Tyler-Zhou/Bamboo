namespace ICP.FCM.Common.UI.DispatchCompare
{
    partial class OIDispachCompareNew
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.butAccept = new DevExpress.XtraEditors.SimpleButton();
            this.gcRemark = new DevExpress.XtraEditors.GroupControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.layoutContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).BeginInit();
            this.gcRemark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // layoutContainerControl1
            // 
            this.layoutContainerControl1.Controls.Add(this.pnlTop);
            this.layoutContainerControl1.Controls.Add(this.panelControl1);
            this.layoutContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutContainerControl1.Name = "layoutContainerControl1";
            this.layoutContainerControl1.Size = new System.Drawing.Size(1041, 454);
            this.layoutContainerControl1.TabIndex = 4;
            this.layoutContainerControl1.Text = "layoutContainerControl1";
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1041, 389);
            this.pnlTop.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.butAccept);
            this.panelControl1.Controls.Add(this.gcRemark);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 389);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1041, 65);
            this.panelControl1.TabIndex = 1;
            // 
            // butAccept
            // 
            this.butAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.butAccept.Location = new System.Drawing.Point(5, 5);
            this.butAccept.Name = "butAccept";
            this.butAccept.Size = new System.Drawing.Size(101, 55);
            this.butAccept.TabIndex = 3;
            this.butAccept.Text = "签    收";
            this.butAccept.Click += new System.EventHandler(this.butAccept_Click);
            // 
            // gcRemark
            // 
            this.gcRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcRemark.Controls.Add(this.txtRemark);
            this.gcRemark.Location = new System.Drawing.Point(110, 0);
            this.gcRemark.Name = "gcRemark";
            this.gcRemark.Size = new System.Drawing.Size(931, 62);
            this.gcRemark.TabIndex = 2;
            this.gcRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Enabled = false;
            this.txtRemark.Location = new System.Drawing.Point(2, 23);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.AllowFocused = false;
            this.txtRemark.Size = new System.Drawing.Size(927, 37);
            this.txtRemark.TabIndex = 0;
            this.txtRemark.TabStop = false;
            // 
            // OIDispachCompareNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutContainerControl1);
            this.Name = "OIDispachCompareNew";
            this.Size = new System.Drawing.Size(1041, 454);
            this.Load += new System.EventHandler(this.OIDispachCompareNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.layoutContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).EndInit();
            this.gcRemark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl gcRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.SimpleButton butAccept;
        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace tabWsCampare;
    }
}

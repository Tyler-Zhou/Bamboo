namespace ICP.FCM.Common.UI.DispatchCompare
{
    partial class OIDispachCompare
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.butReject = new DevExpress.XtraEditors.SimpleButton();
            this.gcRemark = new DevExpress.XtraEditors.GroupControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.butAccept = new DevExpress.XtraEditors.SimpleButton();
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).BeginInit();
            this.gcRemark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            this.layoutContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.butReject);
            this.panelControl1.Controls.Add(this.gcRemark);
            this.panelControl1.Controls.Add(this.butAccept);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 365);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1115, 71);
            this.panelControl1.TabIndex = 1;
            // 
            // butReject
            // 
            this.butReject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.butReject.Location = new System.Drawing.Point(11, 38);
            this.butReject.Name = "butReject";
            this.butReject.Size = new System.Drawing.Size(82, 23);
            this.butReject.TabIndex = 3;
            this.butReject.Text = "拒    签";
            this.butReject.Click += new System.EventHandler(this.butReject_Click);
            // 
            // gcRemark
            // 
            this.gcRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gcRemark.Controls.Add(this.txtRemark);
            this.gcRemark.Location = new System.Drawing.Point(106, 2);
            this.gcRemark.Name = "gcRemark";
            this.gcRemark.Size = new System.Drawing.Size(1007, 67);
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
            this.txtRemark.Size = new System.Drawing.Size(1003, 42);
            this.txtRemark.TabIndex = 0;
            this.txtRemark.TabStop = false;
            // 
            // butAccept
            // 
            this.butAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.butAccept.Location = new System.Drawing.Point(11, 6);
            this.butAccept.Name = "butAccept";
            this.butAccept.Size = new System.Drawing.Size(82, 23);
            this.butAccept.TabIndex = 0;
            this.butAccept.Text = "签    收";
            this.butAccept.Click += new System.EventHandler(this.butAccept_Click);
            // 
            // layoutContainerControl1
            // 
            this.layoutContainerControl1.Controls.Add(this.pnlTop);
            this.layoutContainerControl1.Controls.Add(this.panelControl1);
            this.layoutContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutContainerControl1.Name = "layoutContainerControl1";
            this.layoutContainerControl1.Size = new System.Drawing.Size(1115, 436);
            this.layoutContainerControl1.TabIndex = 3;
            this.layoutContainerControl1.Text = "layoutContainerControl1";
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1115, 365);
            this.pnlTop.TabIndex = 3;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.layoutContainerControl1;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // OIDispachCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutContainerControl1);
            this.Name = "OIDispachCompare";
            this.Size = new System.Drawing.Size(1115, 436);
            this.Load += new System.EventHandler(this.OIDispachCompare_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).EndInit();
            this.gcRemark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            this.layoutContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace tabWsCampare;
        private DevExpress.XtraEditors.SimpleButton butAccept;
        private DevExpress.XtraEditors.GroupControl gcRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.SimpleButton butReject;

    }
}

namespace ICP.FCM.Common.UI.DispatchCompare
{
    partial class OIAcceptHistoryCompare
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
            this.gcRemark = new DevExpress.XtraEditors.GroupControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.pnlFill = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).BeginInit();
            this.gcRemark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcRemark);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 590);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(802, 0);
            this.panelControl1.TabIndex = 1;
            this.panelControl1.Visible = false;
            // 
            // gcRemark
            // 
            this.gcRemark.Controls.Add(this.txtRemark);
            this.gcRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRemark.Location = new System.Drawing.Point(2, 1);
            this.gcRemark.Name = "gcRemark";
            this.gcRemark.Size = new System.Drawing.Size(798, 0);
            this.gcRemark.TabIndex = 2;
            this.gcRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(2, 1);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(794, 0);
            this.txtRemark.TabIndex = 0;
            // 
            // pnlFill
            // 
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 0);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(802, 590);
            this.pnlFill.TabIndex = 3;
            this.pnlFill.Text = "pnlFill";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.pnlFill;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // OIAcceptHistoryCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.panelControl1);
            this.Name = "OIAcceptHistoryCompare";
            this.Size = new System.Drawing.Size(802, 590);
            this.Load += new System.EventHandler(this.OIDispachCompare_Load);
            this.Resize += new System.EventHandler(this.OIDispachCompare_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).EndInit();
            this.gcRemark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace tabWsCampare;
        private DevExpress.XtraEditors.GroupControl gcRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl pnlFill;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;

    }
}

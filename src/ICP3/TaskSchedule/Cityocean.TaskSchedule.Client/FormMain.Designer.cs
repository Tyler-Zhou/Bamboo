namespace Cityocean.TaskSchedule.Client
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.tsMenuItemSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemServiceStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemKillJSEXE = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemTerminal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemCargoTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemSailingSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemClean = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemSystem,
            this.tsMenuItemMonitor,
            this.tsMenuItemTest});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1003, 25);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // tsMenuItemSystem
            // 
            this.tsMenuItemSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemExit});
            this.tsMenuItemSystem.Name = "tsMenuItemSystem";
            this.tsMenuItemSystem.Size = new System.Drawing.Size(44, 21);
            this.tsMenuItemSystem.Text = "系统";
            // 
            // tsMenuItemExit
            // 
            this.tsMenuItemExit.Name = "tsMenuItemExit";
            this.tsMenuItemExit.Size = new System.Drawing.Size(100, 22);
            this.tsMenuItemExit.Tag = "Exit";
            this.tsMenuItemExit.Text = "退出";
            this.tsMenuItemExit.Click += new System.EventHandler(this.tsMenuItem_Click);
            // 
            // tsMenuItemMonitor
            // 
            this.tsMenuItemMonitor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemServiceStatus,
            this.tsMenuItemKillJSEXE});
            this.tsMenuItemMonitor.Name = "tsMenuItemMonitor";
            this.tsMenuItemMonitor.Size = new System.Drawing.Size(44, 21);
            this.tsMenuItemMonitor.Text = "监控";
            // 
            // tsMenuItemServiceStatus
            // 
            this.tsMenuItemServiceStatus.Name = "tsMenuItemServiceStatus";
            this.tsMenuItemServiceStatus.Size = new System.Drawing.Size(135, 22);
            this.tsMenuItemServiceStatus.Tag = "ServiceStatus";
            this.tsMenuItemServiceStatus.Text = "服务状态";
            this.tsMenuItemServiceStatus.Click += new System.EventHandler(this.tsMenuItem_Click);
            // 
            // tsMenuItemKillJSEXE
            // 
            this.tsMenuItemKillJSEXE.Name = "tsMenuItemKillJSEXE";
            this.tsMenuItemKillJSEXE.Size = new System.Drawing.Size(135, 22);
            this.tsMenuItemKillJSEXE.Tag = "KillJSEXE";
            this.tsMenuItemKillJSEXE.Text = "Kill JS APP";
            this.tsMenuItemKillJSEXE.Click += new System.EventHandler(this.tsMenuItem_Click);
            // 
            // tsMenuItemTest
            // 
            this.tsMenuItemTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemTerminal,
            this.tsMenuItemCargoTracking,
            this.tsMenuItemSailingSchedule,
            this.tsMenuItemClean});
            this.tsMenuItemTest.Name = "tsMenuItemTest";
            this.tsMenuItemTest.Size = new System.Drawing.Size(44, 21);
            this.tsMenuItemTest.Text = "测试";
            // 
            // tsMenuItemTerminal
            // 
            this.tsMenuItemTerminal.Name = "tsMenuItemTerminal";
            this.tsMenuItemTerminal.Size = new System.Drawing.Size(152, 22);
            this.tsMenuItemTerminal.Tag = "Terminal";
            this.tsMenuItemTerminal.Text = "码头";
            this.tsMenuItemTerminal.Click += new System.EventHandler(this.tsMenuItem_Click);
            // 
            // tsMenuItemCargoTracking
            // 
            this.tsMenuItemCargoTracking.Name = "tsMenuItemCargoTracking";
            this.tsMenuItemCargoTracking.Size = new System.Drawing.Size(152, 22);
            this.tsMenuItemCargoTracking.Tag = "CargoTracking";
            this.tsMenuItemCargoTracking.Text = "货物动态";
            this.tsMenuItemCargoTracking.Click += new System.EventHandler(this.tsMenuItem_Click);
            // 
            // tsMenuItemSailingSchedule
            // 
            this.tsMenuItemSailingSchedule.Name = "tsMenuItemSailingSchedule";
            this.tsMenuItemSailingSchedule.Size = new System.Drawing.Size(152, 22);
            this.tsMenuItemSailingSchedule.Tag = "SailingSchedule";
            this.tsMenuItemSailingSchedule.Text = "船期";
            this.tsMenuItemSailingSchedule.Click += new System.EventHandler(this.tsMenuItem_Click);
            // 
            // tsMenuItemClean
            // 
            this.tsMenuItemClean.Name = "tsMenuItemClean";
            this.tsMenuItemClean.Size = new System.Drawing.Size(152, 22);
            this.tsMenuItemClean.Tag = "Clean";
            this.tsMenuItemClean.Text = "清理";
            this.tsMenuItemClean.Click += new System.EventHandler(this.tsMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 577);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICP数据抓取";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemSystem;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemMonitor;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemServiceStatus;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemTest;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemTerminal;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemCargoTracking;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemSailingSchedule;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemKillJSEXE;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemClean;
    }
}
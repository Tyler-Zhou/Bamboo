namespace Cityocean.TaskSchedule.Client
{
    partial class FormServiceStatus
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServiceStatus));
            this.panelCenter = new System.Windows.Forms.Panel();
            this.lvMessage = new System.Windows.Forms.ListView();
            this.colHIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHOwerJob = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHEvent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnInstallation = new System.Windows.Forms.Button();
            this._ICPWebCrawler = new System.ServiceProcess.ServiceController();
            this.panelCenter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.lvMessage);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(1093, 515);
            this.panelCenter.TabIndex = 0;
            // 
            // lvMessage
            // 
            this.lvMessage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHIndex,
            this.colHTime,
            this.colHOwerJob,
            this.colHEvent});
            this.lvMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMessage.Location = new System.Drawing.Point(0, 0);
            this.lvMessage.Name = "lvMessage";
            this.lvMessage.Size = new System.Drawing.Size(1093, 515);
            this.lvMessage.TabIndex = 0;
            this.lvMessage.UseCompatibleStateImageBehavior = false;
            this.lvMessage.View = System.Windows.Forms.View.Details;
            // 
            // colHIndex
            // 
            this.colHIndex.Text = "序号";
            // 
            // colHTime
            // 
            this.colHTime.Text = "时间";
            this.colHTime.Width = 140;
            // 
            // colHOwerJob
            // 
            this.colHOwerJob.Text = "任务";
            this.colHOwerJob.Width = 120;
            // 
            // colHEvent
            // 
            this.colHEvent.Text = "事件";
            this.colHEvent.Width = 889;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnStop);
            this.panelBottom.Controls.Add(this.btnStart);
            this.panelBottom.Controls.Add(this.btnUninstall);
            this.panelBottom.Controls.Add(this.btnInstallation);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 515);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1093, 39);
            this.panelBottom.TabIndex = 1;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(712, 7);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(99, 26);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(532, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 26);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(223, 7);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(99, 26);
            this.btnUninstall.TabIndex = 0;
            this.btnUninstall.Text = "Uninstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            // 
            // btnInstallation
            // 
            this.btnInstallation.Location = new System.Drawing.Point(43, 7);
            this.btnInstallation.Name = "btnInstallation";
            this.btnInstallation.Size = new System.Drawing.Size(99, 26);
            this.btnInstallation.TabIndex = 0;
            this.btnInstallation.Text = "Installation";
            this.btnInstallation.UseVisualStyleBackColor = true;
            // 
            // _ICPWebCrawler
            // 
            this._ICPWebCrawler.ServiceName = "ICPWebCrawlerService";
            // 
            // FormServiceStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 554);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormServiceStatus";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务状态监控";
            this.panelCenter.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.ListView lvMessage;
        private System.Windows.Forms.ColumnHeader colHIndex;
        private System.Windows.Forms.ColumnHeader colHTime;
        private System.Windows.Forms.ColumnHeader colHEvent;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnInstallation;
        private System.ServiceProcess.ServiceController _ICPWebCrawler;
        private System.Windows.Forms.ColumnHeader colHOwerJob;
    }
}


namespace ICP.WF.UI
{
    partial class TaskWorkListMainWorkSpace
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
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlWorkView = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWorkView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(488, 27);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 431);
            this.splitterControl1.TabIndex = 4;
            this.splitterControl1.TabStop = false;
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(687, 27);
            this.pnlTop.TabIndex = 1;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 27);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(494, 431);
            this.pnlMain.TabIndex = 3;
            // 
            // pnlWorkView
            // 
            this.pnlWorkView.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlWorkView.Location = new System.Drawing.Point(494, 27);
            this.pnlWorkView.Name = "pnlWorkView";
            this.pnlWorkView.Size = new System.Drawing.Size(193, 431);
            this.pnlWorkView.TabIndex = 2;
            // 
            // TaskWorkListMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlWorkView);
            this.Controls.Add(this.pnlTop);
            this.Name = "TaskWorkListMainWorkSpace";
            this.Size = new System.Drawing.Size(687, 458);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWorkView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlWorkView;

    }
}

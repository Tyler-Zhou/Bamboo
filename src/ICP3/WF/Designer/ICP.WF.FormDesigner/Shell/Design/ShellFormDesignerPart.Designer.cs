namespace ICP.WF.FormDesigner
{
    partial class ShellFormDesignerPart
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
            this.components = new System.ComponentModel.Container();
            this.tabMainDesign = new DevExpress.XtraTab.XtraTabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.barCloseCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.barCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tabMainDesign)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMainDesign
            // 
            this.tabMainDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMainDesign.Location = new System.Drawing.Point(0, 0);
            this.tabMainDesign.Margin = new System.Windows.Forms.Padding(0);
            this.tabMainDesign.Name = "tabMainDesign";
            this.tabMainDesign.Size = new System.Drawing.Size(500, 413);
            this.tabMainDesign.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barCloseCurrent,
            this.barCloseAll});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 48);
            // 
            // barCloseCurrent
            // 
            this.barCloseCurrent.Name = "barCloseCurrent";
            this.barCloseCurrent.Size = new System.Drawing.Size(154, 22);
            this.barCloseCurrent.Text = "关闭";
            this.barCloseCurrent.Click += new System.EventHandler(this.barCloseCurrent_Click);
            // 
            // barCloseAll
            // 
            this.barCloseAll.Name = "barCloseAll";
            this.barCloseAll.Size = new System.Drawing.Size(154, 22);
            this.barCloseAll.Text = "除此之外全关闭";
            this.barCloseAll.Click += new System.EventHandler(this.barCloseAll_Click);
            // 
            // ShellFormDesignerPart
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.tabMainDesign);
            this.Name = "ShellFormDesignerPart";
            this.Size = new System.Drawing.Size(500, 413);
            ((System.ComponentModel.ISupportInitialize)(this.tabMainDesign)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabMainDesign;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem barCloseCurrent;
        private System.Windows.Forms.ToolStripMenuItem barCloseAll;
    }
}

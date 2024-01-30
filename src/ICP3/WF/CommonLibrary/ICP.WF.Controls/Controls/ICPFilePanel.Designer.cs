namespace ICP.WF.Controls
{
    partial class ICPFilePanel
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnUpLoad = new System.Windows.Forms.ToolStripButton();
            this.btnDownLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.gridList = new System.Windows.Forms.DataGridView();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.colFileName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUpLoad,
            this.btnDownLoad,
            this.toolStripSeparator1,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(283, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Image = global::ICP.WF.Controls.Properties.Resources.Refresh_16;
            this.btnUpLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(52, 22);
            this.btnUpLoad.Text = "上传";
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.Image = global::ICP.WF.Controls.Properties.Resources.Down_16;
            this.btnDownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(52, 22);
            this.btnDownLoad.Text = "下载";
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::ICP.WF.Controls.Properties.Resources.Delete_16;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(52, 22);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gridList
            // 
            this.gridList.AllowUserToAddRows = false;
            this.gridList.AllowUserToDeleteRows = false;
            this.gridList.AllowUserToOrderColumns = true;
            this.gridList.AutoGenerateColumns = false;
            this.gridList.BackgroundColor = System.Drawing.Color.White;
            this.gridList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName});
            this.gridList.DataSource = this.bsList;
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 25);
            this.gridList.Name = "gridList";
            this.gridList.ReadOnly = true;
            this.gridList.RowTemplate.Height = 23;
            this.gridList.Size = new System.Drawing.Size(283, 128);
            this.gridList.TabIndex = 1;
            this.gridList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(gridList_MouseDoubleClick);
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.WF.Controls.WFFileItem);
            // 
            // colFileName
            // 
            this.colFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFileName.DataPropertyName = "FileName";
            this.colFileName.HeaderText = "文件名";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            // 
            // ICPFilePanel
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.toolStrip1);
            this.Size = new System.Drawing.Size(283, 153);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnUpLoad;
        private System.Windows.Forms.ToolStripButton btnDownLoad;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView gridList;
        private System.Windows.Forms.BindingSource bsList;
        private System.Windows.Forms.DataGridViewLinkColumn colFileName;

    }
}

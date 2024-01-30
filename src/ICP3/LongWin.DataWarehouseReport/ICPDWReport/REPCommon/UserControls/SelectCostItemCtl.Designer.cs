namespace LongWin.DataWarehouseReport.WinUI
{
    partial class SelectCostItemCtl
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
            this.treeTextBox1 = new global::LongWin.Framework.ClientComponents.TreeTextBox();
            this.SuspendLayout();
            // 
            // treeTextBox1
            // 
            this.treeTextBox1.AllowClear = true;
            this.treeTextBox1.Description = "";
            this.treeTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTextBox1.Location = new System.Drawing.Point(0, 0);
            this.treeTextBox1.Multiple = false;
            this.treeTextBox1.Name = "treeTextBox1";
            this.treeTextBox1.Size = new System.Drawing.Size(180, 21);
            this.treeTextBox1.TabIndex = 0;
            this.treeTextBox1.TreeDataSource = null;
            this.treeTextBox1.TreeDisplayMember = null;
            this.treeTextBox1.TreeInitParentKey = null;
            this.treeTextBox1.TreeParentMember = null;
            this.treeTextBox1.TreeValueMember = null;
            this.treeTextBox1.Value = null;
            // 
            // SelectCostItemCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeTextBox1);
            this.Name = "SelectCostItemCtl";
            this.Size = new System.Drawing.Size(180, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private global::LongWin.Framework.ClientComponents.TreeTextBox treeTextBox1;

    }
}

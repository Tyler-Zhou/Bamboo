namespace ICP.ReportCenter.UI.Comm.Controls
{
    partial class ReportOperationTypePart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportOperationTypePart));
            this.treeCheckControl1 = new ICP.Framework.ClientComponents.Controls.TreeCheckControl();
            this.SuspendLayout();
            // 
            // treeCheckControl1
            // 
            this.treeCheckControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeCheckControl1.EditText = "";
            this.treeCheckControl1.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeCheckControl1.EditValue")));
            this.treeCheckControl1.Location = new System.Drawing.Point(0, 0);
            this.treeCheckControl1.Name = "treeCheckControl1";
            this.treeCheckControl1.ReadOnly = false;
            this.treeCheckControl1.Size = new System.Drawing.Size(243, 21);
            this.treeCheckControl1.TabIndex = 48;
            // 
            // ReportOperationTypePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeCheckControl1);
            this.Name = "ReportOperationTypePart";
            this.Size = new System.Drawing.Size(243, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.TreeCheckControl treeCheckControl1;
    }
}

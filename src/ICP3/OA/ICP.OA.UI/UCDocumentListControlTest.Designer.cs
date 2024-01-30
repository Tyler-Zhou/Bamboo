namespace ICP.OA.UI
{
    partial class UCDocumentListControlTest
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
            this.ucDocumentList1 = new ICP.Business.Common.UI.Document.UCDocumentList();
            this.SuspendLayout();
            // 
            // ucDocumentList1
            // 
            this.ucDocumentList1.DataSource = null;
            this.ucDocumentList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDocumentList1.Location = new System.Drawing.Point(0, 0);
            this.ucDocumentList1.Name = "ucDocumentList1";
            
            this.ucDocumentList1.Size = new System.Drawing.Size(795, 414);
            this.ucDocumentList1.TabIndex = 0;
            this.ucDocumentList1.WorkItem = null;
            // 
            // UCDocumentListControlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucDocumentList1);
            this.Name = "UCDocumentListControlTest";
            this.Size = new System.Drawing.Size(795, 414);
            this.ResumeLayout(false);

        }

        #endregion

        public ICP.Business.Common.UI.Document.UCDocumentList ucDocumentList1;
    }
}

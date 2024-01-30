namespace LongWin.DataWarehouseReport.WinUI
{
    partial class SelectShippingLineCtl
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
            this.multipleSelect1 = new global::LongWin.Framework.ClientComponents.MultipleSelect();
            this.SuspendLayout();
            // 
            // multipleSelect1
            // 
            this.multipleSelect1.DataSource = null;
            this.multipleSelect1.DisplayMember = null;
            this.multipleSelect1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multipleSelect1.Location = new System.Drawing.Point(0, 0);
            this.multipleSelect1.Name = "multipleSelect1";
            this.multipleSelect1.SearchColumns = null;
            this.multipleSelect1.Size = new System.Drawing.Size(150, 21);
            this.multipleSelect1.TabIndex = 1;
            this.multipleSelect1.Value = "";
            this.multipleSelect1.ValueMember = null;
            // 
            // SelectShippingLineCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.multipleSelect1);
            this.Name = "SelectShippingLineCtl";
            this.Size = new System.Drawing.Size(150, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private global::LongWin.Framework.ClientComponents.MultipleSelect multipleSelect1;
    }
}

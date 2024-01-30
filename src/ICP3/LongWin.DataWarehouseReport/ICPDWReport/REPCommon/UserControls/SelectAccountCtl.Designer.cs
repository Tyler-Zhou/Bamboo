namespace LongWin.DataWarehouseReport.WinUI
{
    partial class SelectAccountCtl
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
            this.selectAccount = new global::LongWin.Framework.ClientComponents.MultipleSelect();
            this.SuspendLayout();
            // 
            // selectAccount
            // 
            this.selectAccount.DataSource = null;
            this.selectAccount.DisplayMember = null;
            this.selectAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectAccount.Location = new System.Drawing.Point(0, 0);
            this.selectAccount.Name = "selectAccount";
            this.selectAccount.SearchColumns = null;
            this.selectAccount.Size = new System.Drawing.Size(150, 21);
            this.selectAccount.TabIndex = 1;
            this.selectAccount.Value = "";
            this.selectAccount.ValueMember = null;
            // 
            // SelectAccountCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectAccount);
            this.Name = "SelectAccountCtl";
            this.Size = new System.Drawing.Size(150, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private global::LongWin.Framework.ClientComponents.MultipleSelect selectAccount;
    }
}

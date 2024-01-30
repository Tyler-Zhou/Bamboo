namespace ICP.OA.UI.Contact
{
    partial class ContactSearchPart
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
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.treeDepartment = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.lblDpt = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(662, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(113, 27);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // treeDepartment
            // 
            this.treeDepartment.AllText = "Selecte ALL";
            this.treeDepartment.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.treeDepartment.Location = new System.Drawing.Point(372, 15);
            this.treeDepartment.Name = "treeDepartment";
            this.treeDepartment.ReadOnly = false;
            this.treeDepartment.Size = new System.Drawing.Size(234, 21);
            this.treeDepartment.SpecifiedBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.treeDepartment.TabIndex = 4;
            // 
            // lblDpt
            // 
            this.lblDpt.Location = new System.Drawing.Point(330, 20);
            this.lblDpt.Name = "lblDpt";
            this.lblDpt.Size = new System.Drawing.Size(36, 14);
            this.lblDpt.TabIndex = 5;
            this.lblDpt.Text = "部门：";
            // 
            // ContactSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDpt);
            this.Controls.Add(this.treeDepartment);
            this.Controls.Add(this.btnSearch);
            this.Name = "ContactSearchPart";
            this.Size = new System.Drawing.Size(775, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private ICP.Framework.ClientComponents.Controls.TreeSelectBox treeDepartment;
        private DevExpress.XtraEditors.LabelControl lblDpt;
    }
}

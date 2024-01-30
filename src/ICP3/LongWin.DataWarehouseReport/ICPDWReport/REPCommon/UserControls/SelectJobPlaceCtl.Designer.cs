namespace LongWin.DataWarehouseReport.WinUI
{
    partial class SelectJobPlaceCtl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectJobPlaceCtl));
            this.rabJobPlace = new System.Windows.Forms.RadioButton();
            this.rabUserDepartment = new System.Windows.Forms.RadioButton();
            this.cmbJobPlace = new LongWin.Framework.ClientComponents.ComboBoxTreeView();
            this.cmbUserDepartment = new LongWin.Framework.ClientComponents.ComboBoxTreeView();
            this.SuspendLayout();
            // 
            // rabJobPlace
            // 
            resources.ApplyResources(this.rabJobPlace, "rabJobPlace");
            this.rabJobPlace.Checked = true;
            this.rabJobPlace.Name = "rabJobPlace";
            this.rabJobPlace.TabStop = true;
            this.rabJobPlace.UseVisualStyleBackColor = true;
            this.rabJobPlace.CheckedChanged += new System.EventHandler(this.rabJobPlace_CheckedChanged);
            // 
            // rabUserDepartment
            // 
            resources.ApplyResources(this.rabUserDepartment, "rabUserDepartment");
            this.rabUserDepartment.Name = "rabUserDepartment";
            this.rabUserDepartment.UseVisualStyleBackColor = true;
            this.rabUserDepartment.CheckedChanged += new System.EventHandler(this.rabUserDepartment_CheckedChanged);
            // 
            // cmbJobPlace
            // 
            resources.ApplyResources(this.cmbJobPlace, "cmbJobPlace");
            this.cmbJobPlace.DisplayMember = null;
            this.cmbJobPlace.FormattingEnabled = true;
            this.cmbJobPlace.FullDisplayMember = null;
            this.cmbJobPlace.InitParentKey = null;
            this.cmbJobPlace.Name = "cmbJobPlace";
            this.cmbJobPlace.ParentMember = null;
            this.cmbJobPlace.ValueMember = null;
            // 
            // cmbUserDepartment
            // 
            resources.ApplyResources(this.cmbUserDepartment, "cmbUserDepartment");
            this.cmbUserDepartment.DisplayMember = null;
            this.cmbUserDepartment.FormattingEnabled = true;
            this.cmbUserDepartment.FullDisplayMember = null;
            this.cmbUserDepartment.InitParentKey = null;
            this.cmbUserDepartment.Name = "cmbUserDepartment";
            this.cmbUserDepartment.ParentMember = null;
            this.cmbUserDepartment.ValueMember = null;
            // 
            // SelectJobPlaceCtl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.rabUserDepartment);
            this.Controls.Add(this.rabJobPlace);
            this.Controls.Add(this.cmbUserDepartment);
            this.Controls.Add(this.cmbJobPlace);
            this.Name = "SelectJobPlaceCtl";
            this.Load += new System.EventHandler(this.SelectJobPlaceCtl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rabJobPlace;
        private System.Windows.Forms.RadioButton rabUserDepartment;
        private global::LongWin.Framework.ClientComponents.ComboBoxTreeView cmbJobPlace;
        private global::LongWin.Framework.ClientComponents.ComboBoxTreeView cmbUserDepartment;

    }
}

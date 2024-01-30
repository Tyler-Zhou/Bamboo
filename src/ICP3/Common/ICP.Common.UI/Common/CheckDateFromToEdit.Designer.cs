namespace ICP.Common.UI
{
    partial class CheckDateFromToEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckDateFromToEdit));
            this.chkEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkEnabled
            // 
            resources.ApplyResources(this.chkEnabled, "chkEnabled");
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Properties.Caption = resources.GetString("chkEnabled.Properties.Caption");
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // dteFrom
            // 
            resources.ApplyResources(this.dteFrom, "dteFrom");
            this.dteFrom.EditValue = null;
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteFrom.Properties.Buttons"))))});
            this.dteFrom.Properties.Mask.EditMask = resources.GetString("dteFrom.Properties.Mask.EditMask");
            this.dteFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dteFrom.Properties.Mask.MaskType")));
            this.dteFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // labFrom
            // 
            resources.ApplyResources(this.labFrom, "labFrom");
            this.labFrom.Name = "labFrom";
            // 
            // dteTo
            // 
            resources.ApplyResources(this.dteTo, "dteTo");
            this.dteTo.EditValue = null;
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteTo.Properties.Buttons"))))});
            this.dteTo.Properties.Mask.EditMask = resources.GetString("dteTo.Properties.Mask.EditMask");
            this.dteTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dteTo.Properties.Mask.MaskType")));
            this.dteTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // labTo
            // 
            resources.ApplyResources(this.labTo, "labTo");
            this.labTo.Name = "labTo";
            // 
            // CheckDateFromToEdit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.dteFrom);
            this.Controls.Add(this.labFrom);
            this.Controls.Add(this.dteTo);
            this.Controls.Add(this.labTo);
            this.Name = "CheckDateFromToEdit";
            ((System.ComponentModel.ISupportInitialize)(this.chkEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkEnabled;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.DateEdit dteTo;
    }
}

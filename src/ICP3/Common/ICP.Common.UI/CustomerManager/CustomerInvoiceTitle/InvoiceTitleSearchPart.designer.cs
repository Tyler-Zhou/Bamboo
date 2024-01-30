namespace ICP.Common.UI.CustomerManager
{
    partial class InvoiceTitleSearchPart
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
            this.labTaxNo = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtTaxNo = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.btnClaer = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mcbCreateBy = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTaxNo
            // 
            this.labTaxNo.Location = new System.Drawing.Point(2, 18);
            this.labTaxNo.Name = "labTaxNo";
            this.labTaxNo.Size = new System.Drawing.Size(24, 14);
            this.labTaxNo.TabIndex = 0;
            this.labTaxNo.Text = "税号";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(2, 45);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(24, 14);
            this.labName.TabIndex = 0;
            this.labName.Text = "名称";
            // 
            // txtTaxNo
            // 
            this.txtTaxNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTaxNo.Location = new System.Drawing.Point(51, 15);
            this.txtTaxNo.Name = "txtTaxNo";
            this.txtTaxNo.Size = new System.Drawing.Size(135, 21);
            this.txtTaxNo.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(51, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 21);
            this.txtName.TabIndex = 1;
            // 
            // btnClaer
            // 
            this.btnClaer.Location = new System.Drawing.Point(13, 12);
            this.btnClaer.Name = "btnClaer";
            this.btnClaer.Size = new System.Drawing.Size(75, 23);
            this.btnClaer.TabIndex = 2;
            this.btnClaer.Text = "清空(&C)";
            this.btnClaer.Click += new System.EventHandler(this.btnClaer_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(103, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.btnClaer);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 422);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(186, 45);
            this.panelControl1.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(2, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "所属公司";
            // 
            // mcbCreateBy
            // 
            this.mcbCreateBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcbCreateBy.EditText = "";
            this.mcbCreateBy.EditValue = null;
            this.mcbCreateBy.Location = new System.Drawing.Point(51, 70);
            this.mcbCreateBy.Name = "mcbCreateBy";
            this.mcbCreateBy.ReadOnly = false;
            this.mcbCreateBy.RefreshButtonToolTip = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.mcbCreateBy.ShowRefreshButton = false;
            this.mcbCreateBy.Size = new System.Drawing.Size(134, 21);
            this.mcbCreateBy.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcbCreateBy.TabIndex = 2;
            this.mcbCreateBy.ToolTip = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            // 
            // InvoiceTitleSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mcbCreateBy);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtTaxNo);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.labTaxNo);
            this.Name = "InvoiceTitleSearchPart";
            this.Size = new System.Drawing.Size(186, 467);
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labTaxNo;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtTaxNo;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SimpleButton btnClaer;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcbCreateBy;
    }
}

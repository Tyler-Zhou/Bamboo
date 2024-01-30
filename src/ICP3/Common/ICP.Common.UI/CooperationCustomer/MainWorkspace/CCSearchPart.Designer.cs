using ICP.Framework.ClientComponents.Controls;
namespace ICP.Common.UI.CC
{
    partial class CCSearchPart
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClean = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.mcmbSales = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labSales = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labShipLine = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.cmbShipLine = new LWImageComboBoxEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipLine.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClean);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 506);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 59);
            this.panel1.TabIndex = 0;
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(17, 16);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 1;
            this.btnClean.Text = "C&lean";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(106, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Sea&rch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 506);
            this.panel2.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(220, 506);
            this.navBarControl1.TabIndex = 0;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 122;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.mcmbSales);
            this.navBarGroupBase.Controls.Add(this.chkcmbCompany);
            this.navBarGroupBase.Controls.Add(this.labSales);
            this.navBarGroupBase.Controls.Add(this.labCompany);
            this.navBarGroupBase.Controls.Add(this.labShipLine);
            this.navBarGroupBase.Controls.Add(this.labName);
            this.navBarGroupBase.Controls.Add(this.txtName);
            this.navBarGroupBase.Controls.Add(this.cmbShipLine);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(196, 120);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // mcmbSales
            // 
            this.mcmbSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mcmbSales.EditText = "";
            this.mcmbSales.EditValue = null;
            this.mcmbSales.Location = new System.Drawing.Point(66, 56);
            this.mcmbSales.Name = "mcmbSales";
            this.mcmbSales.ReadOnly = false;
            this.mcmbSales.Size = new System.Drawing.Size(123, 21);
            this.mcmbSales.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbSales.TabIndex = 51;
            this.mcmbSales.ToolTip = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(66, 3);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(123, 21);
            this.chkcmbCompany.TabIndex = 50;
            // 
            // labSales
            // 
            this.labSales.Location = new System.Drawing.Point(5, 59);
            this.labSales.Name = "labSales";
            this.labSales.Size = new System.Drawing.Size(27, 14);
            this.labSales.TabIndex = 53;
            this.labSales.Text = "Sales";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(5, 6);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 52;
            this.labCompany.Text = "Company";
            // 
            // labShipLine
            // 
            this.labShipLine.Location = new System.Drawing.Point(5, 86);
            this.labShipLine.Name = "labShipLine";
            this.labShipLine.Size = new System.Drawing.Size(45, 14);
            this.labShipLine.TabIndex = 12;
            this.labShipLine.Text = "ShipLine";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(5, 32);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 13;
            this.labName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(66, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(123, 21);
            this.txtName.TabIndex = 1;
            // 
            // cmbShipLine
            // 
            this.cmbShipLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShipLine.Location = new System.Drawing.Point(66, 83);
            this.cmbShipLine.Name = "cmbShipLine";
            this.cmbShipLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShipLine.Size = new System.Drawing.Size(123, 21);
            this.cmbShipLine.TabIndex = 3;
            // 
            // CCSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CCSearchPart";
            this.Size = new System.Drawing.Size(220, 565);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipLine.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.LabelControl labShipLine;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SimpleButton btnClean;
        private LWImageComboBoxEdit cmbShipLine;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbSales;
        protected DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        protected DevExpress.XtraEditors.LabelControl labSales;
        protected DevExpress.XtraEditors.LabelControl labCompany;
    }
}

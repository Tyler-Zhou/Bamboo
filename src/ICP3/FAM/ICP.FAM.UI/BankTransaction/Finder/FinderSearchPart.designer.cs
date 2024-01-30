namespace ICP.FAM.UI.BankTransaction.Finder
{
    partial class FinderSearchPart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinderSearchPart));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.txtRelativeAccountNo = new DevExpress.XtraEditors.TextEdit();
            this.labRelativeAccountNo = new DevExpress.XtraEditors.LabelControl();
            this.txtAccountNO = new DevExpress.XtraEditors.TextEdit();
            this.labAccountNo = new DevExpress.XtraEditors.LabelControl();
            this.txtBusinessNO = new DevExpress.XtraEditors.TextEdit();
            this.labBusinessNO = new DevExpress.XtraEditors.LabelControl();
            this.labWaterNo = new DevExpress.XtraEditors.LabelControl();
            this.txtWaterNO = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dmdDate = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.bsGeography = new System.Windows.Forms.BindingSource(this.components);
            this.bsCompany = new System.Windows.Forms.BindingSource(this.components);
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pnlCenter = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelativeAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWaterNO.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).BeginInit();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            resources.ApplyResources(this.navBarControl1, "navBarControl1");
            this.navBarControl1.ExplorerBarGroupInterval = 5;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 5;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDate});
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            // 
            // nbarBase
            // 
            resources.ApplyResources(this.nbarBase, "nbarBase");
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 119;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.txtRelativeAccountNo);
            this.navBarGroupBase.Controls.Add(this.labRelativeAccountNo);
            this.navBarGroupBase.Controls.Add(this.txtAccountNO);
            this.navBarGroupBase.Controls.Add(this.labAccountNo);
            this.navBarGroupBase.Controls.Add(this.txtBusinessNO);
            this.navBarGroupBase.Controls.Add(this.labBusinessNO);
            this.navBarGroupBase.Controls.Add(this.labWaterNo);
            this.navBarGroupBase.Controls.Add(this.txtWaterNO);
            this.navBarGroupBase.Name = "navBarGroupBase";
            resources.ApplyResources(this.navBarGroupBase, "navBarGroupBase");
            // 
            // txtRelativeAccountNo
            // 
            resources.ApplyResources(this.txtRelativeAccountNo, "txtRelativeAccountNo");
            this.txtRelativeAccountNo.Name = "txtRelativeAccountNo";
            // 
            // labRelativeAccountNo
            // 
            resources.ApplyResources(this.labRelativeAccountNo, "labRelativeAccountNo");
            this.labRelativeAccountNo.Name = "labRelativeAccountNo";
            // 
            // txtAccountNO
            // 
            resources.ApplyResources(this.txtAccountNO, "txtAccountNO");
            this.txtAccountNO.Name = "txtAccountNO";
            // 
            // labAccountNo
            // 
            resources.ApplyResources(this.labAccountNo, "labAccountNo");
            this.labAccountNo.Name = "labAccountNo";
            // 
            // txtBusinessNO
            // 
            resources.ApplyResources(this.txtBusinessNO, "txtBusinessNO");
            this.txtBusinessNO.Name = "txtBusinessNO";
            // 
            // labBusinessNO
            // 
            resources.ApplyResources(this.labBusinessNO, "labBusinessNO");
            this.labBusinessNO.Name = "labBusinessNO";
            // 
            // labWaterNo
            // 
            resources.ApplyResources(this.labWaterNo, "labWaterNo");
            this.labWaterNo.Name = "labWaterNo";
            // 
            // txtWaterNO
            // 
            resources.ApplyResources(this.txtWaterNO, "txtWaterNO");
            this.txtWaterNO.Name = "txtWaterNO";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.dmdDate);
            this.navBarGroupControlContainer1.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.labTo);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            resources.ApplyResources(this.navBarGroupControlContainer1, "navBarGroupControlContainer1");
            // 
            // dmdDate
            // 
            this.dmdDate.From = null;
            this.dmdDate.IsEngish = true;
            resources.ApplyResources(this.dmdDate, "dmdDate");
            this.dmdDate.Name = "dmdDate";
            this.dmdDate.To = null;
            // 
            // labFrom
            // 
            resources.ApplyResources(this.labFrom, "labFrom");
            this.labFrom.Name = "labFrom";
            // 
            // labTo
            // 
            resources.ApplyResources(this.labTo, "labTo");
            this.labTo.Name = "labTo";
            // 
            // nbarDate
            // 
            resources.ApplyResources(this.nbarDate, "nbarDate");
            this.nbarDate.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 185;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // bsGeography
            // 
            this.bsGeography.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CountryProvinceList);
            // 
            // bsCompany
            // 
            this.bsCompany.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.OrganizationList);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClear);
            this.pnlBottom.Controls.Add(this.btnSearch);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.pnlCenter);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.navBarControl1);
            resources.ApplyResources(this.pnlCenter, "pnlCenter");
            this.pnlCenter.Name = "pnlCenter";
            // 
            // FinderSearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.pnlBottom);
            this.Name = "FinderSearchPart";
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelativeAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWaterNO.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).EndInit();
            this.pnlCenter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.LabelControl labWaterNo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private System.Windows.Forms.BindingSource bsGeography;
        private System.Windows.Forms.BindingSource bsCompany;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl pnlCenter;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.TextEdit txtWaterNO;
        private DevExpress.XtraEditors.TextEdit txtBusinessNO;
        private DevExpress.XtraEditors.LabelControl labBusinessNO;
        private DevExpress.XtraEditors.TextEdit txtAccountNO;
        private DevExpress.XtraEditors.LabelControl labAccountNo;
        private DevExpress.XtraEditors.TextEdit txtRelativeAccountNo;
        private DevExpress.XtraEditors.LabelControl labRelativeAccountNo;
        private Framework.ClientComponents.Controls.DateMonthControl dmdDate;
    }
}


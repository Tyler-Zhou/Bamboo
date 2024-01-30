namespace ICP.Common.UI.Geography.Location
{
    partial class LocationFinderSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationFinderSearchPart));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.panelLwChk = new System.Windows.Forms.Panel();
            this.labIsOcean = new DevExpress.XtraEditors.LabelControl();
            this.labIsAir = new DevExpress.XtraEditors.LabelControl();
            this.labIsOther = new DevExpress.XtraEditors.LabelControl();
            this.lwchkIsOther = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lwchkIsOcean = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.lwchkIsAir = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.panelTextBox = new System.Windows.Forms.Panel();
            this.cmbCountryProvince = new ICP.Framework.ClientComponents.Controls.LWComboBoxTree();
            this.txtCodeOrName = new DevExpress.XtraEditors.TextEdit();
            this.labCodeOrName = new DevExpress.XtraEditors.LabelControl();
            this.labGeography = new DevExpress.XtraEditors.LabelControl();
            this.bsGeography = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClean = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            this.panelLwChk.SuspendLayout();
            this.panelTextBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountryProvince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeOrName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            resources.ApplyResources(this.navBarControl1, "navBarControl1");
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase});
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            // 
            // nbarBase
            // 
            resources.ApplyResources(this.nbarBase, "nbarBase");
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 220;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.panelBottom);
            this.navBarGroupBase.Controls.Add(this.panelLwChk);
            this.navBarGroupBase.Controls.Add(this.panelTextBox);
            this.navBarGroupBase.Name = "navBarGroupBase";
            resources.ApplyResources(this.navBarGroupBase, "navBarGroupBase");
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lwchkIsValid);
            this.panelBottom.Controls.Add(this.labIsValid);
            this.panelBottom.Controls.Add(this.labMax);
            this.panelBottom.Controls.Add(this.numMax);
            resources.ApplyResources(this.panelBottom, "panelBottom");
            this.panelBottom.Name = "panelBottom";
            // 
            // lwchkIsValid
            // 
            resources.ApplyResources(this.lwchkIsValid, "lwchkIsValid");
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // labIsValid
            // 
            resources.ApplyResources(this.labIsValid, "labIsValid");
            this.labIsValid.Name = "labIsValid";
            // 
            // labMax
            // 
            resources.ApplyResources(this.labMax, "labMax");
            this.labMax.Name = "labMax";
            // 
            // numMax
            // 
            resources.ApplyResources(this.numMax, "numMax");
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = resources.GetString("numMax.Properties.Mask.EditMask");
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMax.Properties.NullText = resources.GetString("numMax.Properties.NullText");
            this.numMax.Properties.NullValuePrompt = resources.GetString("numMax.Properties.NullValuePrompt");
            this.numMax.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("numMax.Properties.NullValuePromptShowForEmptyValue")));
            // 
            // panelLwChk
            // 
            this.panelLwChk.Controls.Add(this.labIsOcean);
            this.panelLwChk.Controls.Add(this.labIsAir);
            this.panelLwChk.Controls.Add(this.labIsOther);
            this.panelLwChk.Controls.Add(this.lwchkIsOther);
            this.panelLwChk.Controls.Add(this.lwchkIsOcean);
            this.panelLwChk.Controls.Add(this.lwchkIsAir);
            resources.ApplyResources(this.panelLwChk, "panelLwChk");
            this.panelLwChk.Name = "panelLwChk";
            // 
            // labIsOcean
            // 
            resources.ApplyResources(this.labIsOcean, "labIsOcean");
            this.labIsOcean.Name = "labIsOcean";
            // 
            // labIsAir
            // 
            resources.ApplyResources(this.labIsAir, "labIsAir");
            this.labIsAir.Name = "labIsAir";
            // 
            // labIsOther
            // 
            resources.ApplyResources(this.labIsOther, "labIsOther");
            this.labIsOther.Name = "labIsOther";
            // 
            // lwchkIsOther
            // 
            resources.ApplyResources(this.lwchkIsOther, "lwchkIsOther");
            this.lwchkIsOther.Checked = null;
            this.lwchkIsOther.CheckedText = "TRUE";
            this.lwchkIsOther.Name = "lwchkIsOther";
            this.lwchkIsOther.NULLText = "ALL";
            this.lwchkIsOther.UnCheckedText = "FALSE";
            // 
            // lwchkIsOcean
            // 
            resources.ApplyResources(this.lwchkIsOcean, "lwchkIsOcean");
            this.lwchkIsOcean.Checked = null;
            this.lwchkIsOcean.CheckedText = "TRUE";
            this.lwchkIsOcean.Name = "lwchkIsOcean";
            this.lwchkIsOcean.NULLText = "ALL";
            this.lwchkIsOcean.UnCheckedText = "FALSE";
            // 
            // lwchkIsAir
            // 
            resources.ApplyResources(this.lwchkIsAir, "lwchkIsAir");
            this.lwchkIsAir.Checked = null;
            this.lwchkIsAir.CheckedText = "TRUE";
            this.lwchkIsAir.Name = "lwchkIsAir";
            this.lwchkIsAir.NULLText = "ALL";
            this.lwchkIsAir.UnCheckedText = "FALSE";
            // 
            // panelTextBox
            // 
            this.panelTextBox.Controls.Add(this.cmbCountryProvince);
            this.panelTextBox.Controls.Add(this.txtCodeOrName);
            this.panelTextBox.Controls.Add(this.labCodeOrName);
            this.panelTextBox.Controls.Add(this.labGeography);
            resources.ApplyResources(this.panelTextBox, "panelTextBox");
            this.panelTextBox.Name = "panelTextBox";
            // 
            // cmbCountryProvince
            // 
            this.cmbCountryProvince.AllowMultSelect = false;
            resources.ApplyResources(this.cmbCountryProvince, "cmbCountryProvince");
            this.cmbCountryProvince.DataSource = null;
            this.cmbCountryProvince.DisplayMember = "CName";
            this.cmbCountryProvince.Name = "cmbCountryProvince";
            this.cmbCountryProvince.ParentMember = "ParentID";
            this.cmbCountryProvince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbCountryProvince.Properties.Buttons"))))});
            this.cmbCountryProvince.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCountryProvince.Properties.PopupSizeable = false;
            this.cmbCountryProvince.Properties.ShowPopupCloseButton = false;
            this.cmbCountryProvince.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbCountryProvince.RootValue = 0;
            this.cmbCountryProvince.SelectedValue = null;
            this.cmbCountryProvince.Separator = ",";
            this.cmbCountryProvince.ValueMember = "ID";
            this.cmbCountryProvince.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCountryProvince_KeyDown);
            // 
            // txtCodeOrName
            // 
            resources.ApplyResources(this.txtCodeOrName, "txtCodeOrName");
            this.txtCodeOrName.Name = "txtCodeOrName";
            // 
            // labCodeOrName
            // 
            resources.ApplyResources(this.labCodeOrName, "labCodeOrName");
            this.labCodeOrName.Name = "labCodeOrName";
            // 
            // labGeography
            // 
            resources.ApplyResources(this.labGeography, "labGeography");
            this.labGeography.Name = "labGeography";
            // 
            // bsGeography
            // 
            this.bsGeography.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CountryProvinceList);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClean);
            this.panelControl1.Controls.Add(this.btnSearch);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // btnClean
            // 
            resources.ApplyResources(this.btnClean, "btnClean");
            this.btnClean.Name = "btnClean";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            this.btnClean.Location = new System.Drawing.Point(4,15);
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel2);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Name = "panel2";
            // 
            // LocationFinderSearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "LocationFinderSearchPart";
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            this.panelLwChk.ResumeLayout(false);
            this.panelLwChk.PerformLayout();
            this.panelTextBox.ResumeLayout(false);
            this.panelTextBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountryProvince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeOrName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        protected ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private DevExpress.XtraEditors.LabelControl labGeography;
        private DevExpress.XtraEditors.LabelControl labCodeOrName;
        protected DevExpress.XtraEditors.TextEdit txtCodeOrName;
        private System.Windows.Forms.BindingSource bsGeography;
        protected ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsOther;
        protected ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsAir;
        protected ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsOcean;
        private DevExpress.XtraEditors.LabelControl labIsOther;
        private DevExpress.XtraEditors.LabelControl labIsAir;
        private DevExpress.XtraEditors.LabelControl labIsOcean;
        protected System.Windows.Forms.Panel panelTextBox;
        protected System.Windows.Forms.Panel panelBottom;
        protected System.Windows.Forms.Panel panelLwChk;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClean;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel2;
        private ICP.Framework.ClientComponents.Controls.LWComboBoxTree cmbCountryProvince;
    }
}

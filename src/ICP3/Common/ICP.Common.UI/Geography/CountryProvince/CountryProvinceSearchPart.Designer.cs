﻿namespace ICP.Common.UI.Geography.CountryProvince
{
    partial class CountryProvinceSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountryProvinceSearchPart));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.lookUpCountry = new DevExpress.XtraEditors.LookUpEdit();
            this.bsCountry = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.nbarBase.GroupClientHeight = 139;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.lwchkIsValid);
            this.navBarGroupBase.Controls.Add(this.numMax);
            this.navBarGroupBase.Controls.Add(this.labMax);
            this.navBarGroupBase.Controls.Add(this.labIsValid);
            this.navBarGroupBase.Controls.Add(this.labCountry);
            this.navBarGroupBase.Controls.Add(this.labName);
            this.navBarGroupBase.Controls.Add(this.labCode);
            this.navBarGroupBase.Controls.Add(this.txtName);
            this.navBarGroupBase.Controls.Add(this.txtCode);
            this.navBarGroupBase.Controls.Add(this.lookUpCountry);
            this.navBarGroupBase.Name = "navBarGroupBase";
            resources.ApplyResources(this.navBarGroupBase, "navBarGroupBase");
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
            // numMax
            // 
            resources.ApplyResources(this.numMax, "numMax");
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = resources.GetString("numMax.Properties.Mask.EditMask");
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labMax
            // 
            resources.ApplyResources(this.labMax, "labMax");
            this.labMax.Name = "labMax";
            // 
            // labIsValid
            // 
            resources.ApplyResources(this.labIsValid, "labIsValid");
            this.labIsValid.Name = "labIsValid";
            // 
            // labCountry
            // 
            resources.ApplyResources(this.labCountry, "labCountry");
            this.labCountry.Name = "labCountry";
            // 
            // labName
            // 
            resources.ApplyResources(this.labName, "labName");
            this.labName.Name = "labName";
            // 
            // labCode
            // 
            resources.ApplyResources(this.labCode, "labCode");
            this.labCode.Name = "labCode";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            // 
            // lookUpCountry
            // 
            resources.ApplyResources(this.lookUpCountry, "lookUpCountry");
            this.lookUpCountry.Name = "lookUpCountry";
            this.lookUpCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lookUpCountry.Properties.Buttons"))))});
            this.lookUpCountry.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpCountry.Properties.Columns"), resources.GetString("lookUpCountry.Properties.Columns1"), ((int)(resources.GetObject("lookUpCountry.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lookUpCountry.Properties.Columns3"))), resources.GetString("lookUpCountry.Properties.Columns4"), ((bool)(resources.GetObject("lookUpCountry.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lookUpCountry.Properties.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpCountry.Properties.Columns7"), resources.GetString("lookUpCountry.Properties.Columns8"), ((int)(resources.GetObject("lookUpCountry.Properties.Columns9"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lookUpCountry.Properties.Columns10"))), resources.GetString("lookUpCountry.Properties.Columns11"), ((bool)(resources.GetObject("lookUpCountry.Properties.Columns12"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lookUpCountry.Properties.Columns13"))))});
            this.lookUpCountry.Properties.DataSource = this.bsCountry;
            this.lookUpCountry.Properties.DisplayMember = "CName";
            this.lookUpCountry.Properties.NullText = resources.GetString("lookUpCountry.Properties.NullText");
            this.lookUpCountry.Properties.PopupSizeable = false;
            this.lookUpCountry.Properties.ShowHeader = false;
            this.lookUpCountry.Properties.ValueMember = "ID";
            // 
            // bsCountry
            // 
            this.bsCountry.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CountryList);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
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
            this.panelControl2.Controls.Add(this.panel1);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.navBarControl1);
            this.panel1.Name = "panel1";
            // 
            // CountryProvinceSearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "CountryProvinceSearchPart";
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private DevExpress.XtraEditors.LabelControl labCountry;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LookUpEdit lookUpCountry;
        private System.Windows.Forms.BindingSource bsCountry;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel1;
    }
}

namespace ICP.Sys.UI.Job
{
    partial class JobEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobEditPart));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labDescription = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.bsParent = new System.Windows.Forms.BindingSource(this.components);
            this.labParent = new DevExpress.XtraEditors.LabelControl();
            this.popParent = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeParent = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnClearPop = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelScroll = new System.Windows.Forms.Panel();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).BeginInit();
            this.popControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.JobInfo);
            // 
            // labEName
            // 
            resources.ApplyResources(this.labEName, "labEName");
            this.labEName.Name = "labEName";
            // 
            // labDescription
            // 
            resources.ApplyResources(this.labDescription, "labDescription");
            this.labDescription.Name = "labDescription";
            // 
            // labCName
            // 
            resources.ApplyResources(this.labCName, "labCName");
            this.labCName.Name = "labCName";
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtEName, "txtEName");
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 100;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtCName, "txtCName");
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 100;
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.MaxLength = 500;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 20;
            // 
            // labCode
            // 
            resources.ApplyResources(this.labCode, "labCode");
            this.labCode.Name = "labCode";
            // 
            // bsParent
            // 
            this.bsParent.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.JobList);
            // 
            // labParent
            // 
            resources.ApplyResources(this.labParent, "labParent");
            this.labParent.Name = "labParent";
            // 
            // popParent
            // 
            this.popParent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ParentName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.popParent, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.popParent, "popParent");
            this.popParent.Name = "popParent";
            this.popParent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("popParent.Properties.Buttons"))))});
            this.popParent.Properties.PopupControl = this.popControl1;
            this.popParent.Properties.PopupSizeable = false;
            this.popParent.Properties.ShowPopupCloseButton = false;
            // 
            // popControl1
            // 
            this.popControl1.Controls.Add(this.treeParent);
            this.popControl1.Controls.Add(this.btnClearPop);
            resources.ApplyResources(this.popControl1, "popControl1");
            this.popControl1.Name = "popControl1";
            // 
            // treeParent
            // 
            this.treeParent.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeParent.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeParent.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeParent.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeParent.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeParent.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeParent.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCName,
            this.colEName});
            this.treeParent.DataSource = this.bsParent;
            resources.ApplyResources(this.treeParent, "treeParent");
            this.treeParent.Name = "treeParent";
            this.treeParent.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeParent.OptionsBehavior.PopulateServiceColumns = true;
            this.treeParent.OptionsLayout.AddNewColumns = false;
            this.treeParent.OptionsView.EnableAppearanceEvenRow = true;
            this.treeParent.OptionsView.ShowIndicator = false;
            this.treeParent.DoubleClick += new System.EventHandler(this.treeParent_DoubleClick);
            // 
            // colCName
            // 
            resources.ApplyResources(this.colCName, "colCName");
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.OptionsColumn.AllowEdit = false;
            this.colCName.OptionsColumn.ReadOnly = true;
            // 
            // colEName
            // 
            resources.ApplyResources(this.colEName, "colEName");
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.OptionsColumn.AllowEdit = false;
            this.colEName.OptionsColumn.ReadOnly = true;
            // 
            // btnClearPop
            // 
            resources.ApplyResources(this.btnClearPop, "btnClearPop");
            this.btnClearPop.Name = "btnClearPop";
            this.btnClearPop.Click += new System.EventHandler(this.btnClearPop_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // panelScroll
            // 
            resources.ApplyResources(this.panelScroll, "panelScroll");
            this.panelScroll.BackColor = System.Drawing.Color.Transparent;
            this.panelScroll.Controls.Add(this.labCode);
            this.panelScroll.Controls.Add(this.popParent);
            this.panelScroll.Controls.Add(this.popControl1);
            this.panelScroll.Controls.Add(this.txtDescription);
            this.panelScroll.Controls.Add(this.labParent);
            this.panelScroll.Controls.Add(this.txtCName);
            this.panelScroll.Controls.Add(this.txtCode);
            this.panelScroll.Controls.Add(this.txtEName);
            this.panelScroll.Controls.Add(this.labEName);
            this.panelScroll.Controls.Add(this.labDescription);
            this.panelScroll.Controls.Add(this.labCName);
            this.panelScroll.Name = "panelScroll";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barSave
            // 
            resources.ApplyResources(this.barSave, "barSave");
            this.barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // barButtonItem2
            // 
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // JobEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "JobEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).EndInit();
            this.popControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.panelScroll.ResumeLayout(false);
            this.panelScroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labDescription;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private System.Windows.Forms.BindingSource bsParent;
        private DevExpress.XtraEditors.LabelControl labParent;
        private DevExpress.XtraEditors.PopupContainerEdit popParent;
        private DevExpress.XtraEditors.PopupContainerControl popControl1;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeParent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private DevExpress.XtraEditors.SimpleButton btnClearPop;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private System.Windows.Forms.Panel panelScroll;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    }
}

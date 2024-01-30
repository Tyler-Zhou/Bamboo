namespace ICP.Sys.UI.PermissionManage
{
    partial class PermissionEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissionEditPart));
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labDescription = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.bsFunction = new System.Windows.Forms.BindingSource(this.components);
            this.labFunction = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeFunction = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.popControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.popFunction = new DevExpress.XtraEditors.PopupContainerEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFunction)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).BeginInit();
            this.popControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popFunction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            // labCode
            // 
            resources.ApplyResources(this.labCode, "labCode");
            this.labCode.Name = "labCode";
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
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.FunctionInfo);
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
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
            // bsFunction
            // 
            this.bsFunction.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.FunctionList);
            // 
            // labFunction
            // 
            resources.ApplyResources(this.labFunction, "labFunction");
            this.labFunction.Name = "labFunction";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.txtCName);
            this.panel1.Controls.Add(this.labDescription);
            this.panel1.Controls.Add(this.labCode);
            this.panel1.Controls.Add(this.txtEName);
            this.panel1.Controls.Add(this.labEName);
            this.panel1.Controls.Add(this.labCName);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // colCName
            // 
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.OptionsColumn.AllowEdit = false;
            // 
            // colEName
            // 
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.OptionsColumn.AllowEdit = false;
            // 
            // colDescription
            // 
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            // 
            // treeFunction
            // 
            this.treeFunction.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeFunction.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeFunction.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.treeFunction.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeFunction.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeFunction.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeFunction.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCName,
            this.colEName});
            this.treeFunction.DataSource = this.bsFunction;
            resources.ApplyResources(this.treeFunction, "treeFunction");
            this.treeFunction.ImageIndexFieldName = "FunctionType";
            this.treeFunction.Name = "treeFunction";
            this.treeFunction.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeFunction.OptionsView.EnableAppearanceEvenRow = true;
            this.treeFunction.OptionsView.ShowIndicator = false;
            this.treeFunction.DoubleClick += new System.EventHandler(this.treeFunction_DoubleClick);
            // 
            // popControl1
            // 
            this.popControl1.Controls.Add(this.treeFunction);
            resources.ApplyResources(this.popControl1, "popControl1");
            this.popControl1.Name = "popControl1";
            // 
            // popFunction
            // 
            this.dxErrorProvider1.SetIconAlignment(this.popFunction, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.popFunction, "popFunction");
            this.popFunction.Name = "popFunction";
            this.popFunction.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("popFunction.Properties.Buttons"))))});
            this.popFunction.Properties.PopupControl = this.popControl1;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
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
            this.barClose});
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.MultiLine = true;
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
            // barClose
            // 
            resources.ApplyResources(this.barClose, "barClose");
            this.barClose.Id = 1;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
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
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.labFunction);
            this.panel2.Controls.Add(this.popFunction);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.popControl1);
            this.panel2.Name = "panel2";
            // 
            // PermissionEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PermissionEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFunction)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).EndInit();
            this.popControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popFunction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labDescription;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private System.Windows.Forms.BindingSource bsFunction;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labFunction;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDescription;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeFunction;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private DevExpress.XtraEditors.PopupContainerControl popControl1;
        private DevExpress.XtraEditors.PopupContainerEdit popFunction;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private System.Windows.Forms.Panel panel2;
    }
}

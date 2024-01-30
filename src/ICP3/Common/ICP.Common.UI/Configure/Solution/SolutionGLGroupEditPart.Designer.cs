namespace ICP.Common.UI.Configure.Solution
{
    partial class SolutionGLGroupEditPart
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
            this.popParent = new DevExpress.XtraEditors.PopupContainerEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.popControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeParent = new DevExpress.XtraTreeList.TreeList();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsParent = new System.Windows.Forms.BindingSource(this.components);
            this.btnClearPop = new DevExpress.XtraEditors.SimpleButton();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.labParent = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).BeginInit();
            this.popControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // popParent
            // 
            this.popParent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ParentName", true));
            this.dxErrorProvider1.SetIconAlignment(this.popParent, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.popParent.Location = new System.Drawing.Point(69, 72);
            this.popParent.Name = "popParent";
            this.popParent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popParent.Properties.PopupControl = this.popControl1;
            this.popParent.Properties.PopupSizeable = false;
            this.popParent.Properties.ShowPopupCloseButton = false;
            this.popParent.Size = new System.Drawing.Size(213, 21);
            this.popParent.TabIndex = 3;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.SolutionGLGroupInfo);
            // 
            // popControl1
            // 
            this.popControl1.Controls.Add(this.treeParent);
            this.popControl1.Controls.Add(this.btnClearPop);
            this.popControl1.Location = new System.Drawing.Point(69, 141);
            this.popControl1.Name = "popControl1";
            this.popControl1.Size = new System.Drawing.Size(213, 208);
            this.popControl1.TabIndex = 51;
            // 
            // treeParent
            // 
            this.treeParent.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeParent.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeParent.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCName,
            this.colEName});
            this.treeParent.DataSource = this.bsParent;
            this.treeParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeParent.Location = new System.Drawing.Point(0, 0);
            this.treeParent.Name = "treeParent";
            this.treeParent.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeParent.OptionsBehavior.PopulateServiceColumns = true;
            this.treeParent.OptionsLayout.AddNewColumns = false;
            this.treeParent.OptionsView.EnableAppearanceEvenRow = true;
            this.treeParent.OptionsView.ShowIndicator = false;
            this.treeParent.Size = new System.Drawing.Size(213, 189);
            this.treeParent.TabIndex = 0;
            this.treeParent.DoubleClick += new System.EventHandler(this.treeParent_DoubleClick);
            // 
            // colCName
            // 
            this.colCName.Caption = "Name";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.OptionsColumn.AllowEdit = false;
            this.colCName.OptionsColumn.ReadOnly = true;
            this.colCName.Width = 87;
            // 
            // colEName
            // 
            this.colEName.Caption = "Name";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.OptionsColumn.AllowEdit = false;
            this.colEName.OptionsColumn.ReadOnly = true;
            this.colEName.Width = 79;
            // 
            // bsParent
            // 
            this.bsParent.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.SolutionGLGroupList);
            // 
            // btnClearPop
            // 
            this.btnClearPop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearPop.Location = new System.Drawing.Point(0, 189);
            this.btnClearPop.Name = "btnClearPop";
            this.btnClearPop.Size = new System.Drawing.Size(213, 19);
            this.btnClearPop.TabIndex = 0;
            this.btnClearPop.Text = "Clear";
            this.btnClearPop.Click += new System.EventHandler(this.btnClearPop_Click);
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(10, 5);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 47;
            this.labCode.Text = "Code";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(10, 51);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 46;
            this.labEName.Text = "EName";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(10, 28);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(38, 14);
            this.labCName.TabIndex = 48;
            this.labCName.Text = "CName";
            // 
            // labParent
            // 
            this.labParent.Location = new System.Drawing.Point(10, 75);
            this.labParent.Name = "labParent";
            this.labParent.Size = new System.Drawing.Size(36, 14);
            this.labParent.TabIndex = 49;
            this.labParent.Text = "Parent";
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EName", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(69, 49);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 100;
            this.txtEName.Size = new System.Drawing.Size(213, 21);
            this.txtEName.TabIndex = 2;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCode.Location = new System.Drawing.Point(69, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
            this.txtCode.Size = new System.Drawing.Size(213, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CName", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(69, 26);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 100;
            this.txtCName.Size = new System.Drawing.Size(213, 21);
            this.txtCName.TabIndex = 1;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // SolutionGLGroupEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.popParent);
            this.Controls.Add(this.popControl1);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labEName);
            this.Controls.Add(this.labCName);
            this.Controls.Add(this.labParent);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtCName);
            this.Name = "SolutionGLGroupEditPart";
            this.Size = new System.Drawing.Size(345, 140);
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).EndInit();
            this.popControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit popParent;
        private DevExpress.XtraEditors.PopupContainerControl popControl1;
        private DevExpress.XtraTreeList.TreeList treeParent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private System.Windows.Forms.BindingSource bsParent;
        private DevExpress.XtraEditors.SimpleButton btnClearPop;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.LabelControl labParent;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}

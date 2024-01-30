namespace ICP.WF.Activities.Common
{
	partial class SetMainWorkItemDataForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetMainWorkItemDataForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.dataGridView1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.mainWorkItemFieldCombBoxColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MainWorkItemFieldColumn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.selfItemFieldCombBoxColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SelfItemFieldColumn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rewriteModeComBoxColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RewriteModeColumn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.MinDeleteBtn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnMainDelete = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.dataGridView2 = new DevExpress.XtraGrid.GridControl();
            this.bsConstants = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colConstantMainFormProperty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConstantComBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colConstantSelfPropertyCombBox = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConstantSelfItemField = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colConstantRewriteModeCombox = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConstantRewriteMode = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colConstantDelBtn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConstantDel = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainWorkItemFieldColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelfItemFieldColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RewriteModeColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConstants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantComBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantSelfItemField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantRewriteMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.AccessibleDescription = null;
            this.splitContainerControl1.AccessibleName = null;
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Name = "splitContainerControl1";
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel1.Controls.Add(this.dataGridView1);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainerControl1.SplitterPosition = 247;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleDescription = null;
            this.dataGridView1.AccessibleName = null;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.BackgroundImage = null;
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.EmbeddedNavigator.AccessibleDescription = null;
            this.dataGridView1.EmbeddedNavigator.AccessibleName = null;
            this.dataGridView1.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("dataGridView1.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.dataGridView1.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dataGridView1.EmbeddedNavigator.Anchor")));
            this.dataGridView1.EmbeddedNavigator.BackgroundImage = null;
            this.dataGridView1.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("dataGridView1.EmbeddedNavigator.BackgroundImageLayout")));
            this.dataGridView1.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dataGridView1.EmbeddedNavigator.ImeMode")));
            this.dataGridView1.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("dataGridView1.EmbeddedNavigator.TextLocation")));
            this.dataGridView1.EmbeddedNavigator.ToolTip = resources.GetString("dataGridView1.EmbeddedNavigator.ToolTip");
            this.dataGridView1.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("dataGridView1.EmbeddedNavigator.ToolTipIconType")));
            this.dataGridView1.EmbeddedNavigator.ToolTipTitle = resources.GetString("dataGridView1.EmbeddedNavigator.ToolTipTitle");
            this.dataGridView1.Font = null;
            this.dataGridView1.MainView = this.gridView1;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.MainWorkItemFieldColumn,
            this.SelfItemFieldColumn,
            this.RewriteModeColumn,
            this.btnMainDelete});
            this.dataGridView1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.mainWorkItemFieldCombBoxColumn,
            this.selfItemFieldCombBoxColumn,
            this.rewriteModeComBoxColumn,
            this.MinDeleteBtn});
            this.gridView1.GridControl = this.dataGridView1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // mainWorkItemFieldCombBoxColumn
            // 
            resources.ApplyResources(this.mainWorkItemFieldCombBoxColumn, "mainWorkItemFieldCombBoxColumn");
            this.mainWorkItemFieldCombBoxColumn.ColumnEdit = this.MainWorkItemFieldColumn;
            this.mainWorkItemFieldCombBoxColumn.FieldName = "MainWorkItemField";
            this.mainWorkItemFieldCombBoxColumn.Name = "mainWorkItemFieldCombBoxColumn";
            // 
            // MainWorkItemFieldColumn
            // 
            this.MainWorkItemFieldColumn.AccessibleDescription = null;
            this.MainWorkItemFieldColumn.AccessibleName = null;
            resources.ApplyResources(this.MainWorkItemFieldColumn, "MainWorkItemFieldColumn");
            this.MainWorkItemFieldColumn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("MainWorkItemFieldColumn.Buttons"))))});
            this.MainWorkItemFieldColumn.Name = "MainWorkItemFieldColumn";
            // 
            // selfItemFieldCombBoxColumn
            // 
            resources.ApplyResources(this.selfItemFieldCombBoxColumn, "selfItemFieldCombBoxColumn");
            this.selfItemFieldCombBoxColumn.ColumnEdit = this.SelfItemFieldColumn;
            this.selfItemFieldCombBoxColumn.FieldName = "SelfItemField";
            this.selfItemFieldCombBoxColumn.Name = "selfItemFieldCombBoxColumn";
            // 
            // SelfItemFieldColumn
            // 
            this.SelfItemFieldColumn.AccessibleDescription = null;
            this.SelfItemFieldColumn.AccessibleName = null;
            resources.ApplyResources(this.SelfItemFieldColumn, "SelfItemFieldColumn");
            this.SelfItemFieldColumn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("SelfItemFieldColumn.Buttons"))))});
            this.SelfItemFieldColumn.Name = "SelfItemFieldColumn";
            // 
            // rewriteModeComBoxColumn
            // 
            resources.ApplyResources(this.rewriteModeComBoxColumn, "rewriteModeComBoxColumn");
            this.rewriteModeComBoxColumn.ColumnEdit = this.RewriteModeColumn;
            this.rewriteModeComBoxColumn.FieldName = "RewriteMode";
            this.rewriteModeComBoxColumn.Name = "rewriteModeComBoxColumn";
            // 
            // RewriteModeColumn
            // 
            this.RewriteModeColumn.AccessibleDescription = null;
            this.RewriteModeColumn.AccessibleName = null;
            resources.ApplyResources(this.RewriteModeColumn, "RewriteModeColumn");
            this.RewriteModeColumn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("RewriteModeColumn.Buttons"))))});
            this.RewriteModeColumn.Name = "RewriteModeColumn";
            // 
            // MinDeleteBtn
            // 
            resources.ApplyResources(this.MinDeleteBtn, "MinDeleteBtn");
            this.MinDeleteBtn.ColumnEdit = this.btnMainDelete;
            this.MinDeleteBtn.Name = "MinDeleteBtn";
            this.MinDeleteBtn.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // btnMainDelete
            // 
            this.btnMainDelete.AccessibleDescription = null;
            this.btnMainDelete.AccessibleName = null;
            resources.ApplyResources(this.btnMainDelete, "btnMainDelete");
            this.btnMainDelete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnMainDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("btnMainDelete.Buttons"))), resources.GetString("btnMainDelete.Buttons1"), ((int)(resources.GetObject("btnMainDelete.Buttons2"))), ((bool)(resources.GetObject("btnMainDelete.Buttons3"))), ((bool)(resources.GetObject("btnMainDelete.Buttons4"))), ((bool)(resources.GetObject("btnMainDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("btnMainDelete.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("btnMainDelete.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("btnMainDelete.Buttons8"), null, null, ((bool)(resources.GetObject("btnMainDelete.Buttons9"))))});
            this.btnMainDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnMainDelete.Name = "btnMainDelete";
            this.btnMainDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnMainDelete.UseParentBackground = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AccessibleDescription = null;
            this.dataGridView2.AccessibleName = null;
            resources.ApplyResources(this.dataGridView2, "dataGridView2");
            this.dataGridView2.BackgroundImage = null;
            this.dataGridView2.DataSource = this.bsConstants;
            this.dataGridView2.EmbeddedNavigator.AccessibleDescription = null;
            this.dataGridView2.EmbeddedNavigator.AccessibleName = null;
            this.dataGridView2.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("dataGridView2.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.dataGridView2.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dataGridView2.EmbeddedNavigator.Anchor")));
            this.dataGridView2.EmbeddedNavigator.BackgroundImage = null;
            this.dataGridView2.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("dataGridView2.EmbeddedNavigator.BackgroundImageLayout")));
            this.dataGridView2.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dataGridView2.EmbeddedNavigator.ImeMode")));
            this.dataGridView2.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("dataGridView2.EmbeddedNavigator.TextLocation")));
            this.dataGridView2.EmbeddedNavigator.ToolTip = resources.GetString("dataGridView2.EmbeddedNavigator.ToolTip");
            this.dataGridView2.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("dataGridView2.EmbeddedNavigator.ToolTipIconType")));
            this.dataGridView2.EmbeddedNavigator.ToolTipTitle = resources.GetString("dataGridView2.EmbeddedNavigator.ToolTipTitle");
            this.dataGridView2.Font = null;
            this.dataGridView2.MainView = this.gridView2;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.colConstantDel,
            this.colConstantComBox,
            this.colConstantSelfItemField,
            this.colConstantRewriteMode});
            this.dataGridView2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            resources.ApplyResources(this.gridView2, "gridView2");
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colConstantMainFormProperty,
            this.colConstantSelfPropertyCombBox,
            this.colConstantRewriteModeCombox,
            this.colConstantDelBtn});
            this.gridView2.GridControl = this.dataGridView2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colConstantMainFormProperty
            // 
            resources.ApplyResources(this.colConstantMainFormProperty, "colConstantMainFormProperty");
            this.colConstantMainFormProperty.ColumnEdit = this.colConstantComBox;
            this.colConstantMainFormProperty.FieldName = "MainWorkItemField";
            this.colConstantMainFormProperty.Name = "colConstantMainFormProperty";
            // 
            // colConstantComBox
            // 
            this.colConstantComBox.AccessibleDescription = null;
            this.colConstantComBox.AccessibleName = null;
            resources.ApplyResources(this.colConstantComBox, "colConstantComBox");
            this.colConstantComBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("colConstantComBox.Buttons"))))});
            this.colConstantComBox.Name = "colConstantComBox";
            // 
            // colConstantSelfPropertyCombBox
            // 
            resources.ApplyResources(this.colConstantSelfPropertyCombBox, "colConstantSelfPropertyCombBox");
            this.colConstantSelfPropertyCombBox.ColumnEdit = this.colConstantSelfItemField;
            this.colConstantSelfPropertyCombBox.FieldName = "SelfItemField";
            this.colConstantSelfPropertyCombBox.Name = "colConstantSelfPropertyCombBox";
            // 
            // colConstantSelfItemField
            // 
            this.colConstantSelfItemField.AccessibleDescription = null;
            this.colConstantSelfItemField.AccessibleName = null;
            resources.ApplyResources(this.colConstantSelfItemField, "colConstantSelfItemField");
            this.colConstantSelfItemField.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("colConstantSelfItemField.Mask.AutoComplete")));
            this.colConstantSelfItemField.Mask.BeepOnError = ((bool)(resources.GetObject("colConstantSelfItemField.Mask.BeepOnError")));
            this.colConstantSelfItemField.Mask.EditMask = resources.GetString("colConstantSelfItemField.Mask.EditMask");
            this.colConstantSelfItemField.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("colConstantSelfItemField.Mask.IgnoreMaskBlank")));
            this.colConstantSelfItemField.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("colConstantSelfItemField.Mask.MaskType")));
            this.colConstantSelfItemField.Mask.PlaceHolder = ((char)(resources.GetObject("colConstantSelfItemField.Mask.PlaceHolder")));
            this.colConstantSelfItemField.Mask.SaveLiteral = ((bool)(resources.GetObject("colConstantSelfItemField.Mask.SaveLiteral")));
            this.colConstantSelfItemField.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("colConstantSelfItemField.Mask.ShowPlaceHolders")));
            this.colConstantSelfItemField.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("colConstantSelfItemField.Mask.UseMaskAsDisplayFormat")));
            this.colConstantSelfItemField.Name = "colConstantSelfItemField";
            // 
            // colConstantRewriteModeCombox
            // 
            resources.ApplyResources(this.colConstantRewriteModeCombox, "colConstantRewriteModeCombox");
            this.colConstantRewriteModeCombox.ColumnEdit = this.colConstantRewriteMode;
            this.colConstantRewriteModeCombox.FieldName = "RewriteMode";
            this.colConstantRewriteModeCombox.Name = "colConstantRewriteModeCombox";
            // 
            // colConstantRewriteMode
            // 
            this.colConstantRewriteMode.AccessibleDescription = null;
            this.colConstantRewriteMode.AccessibleName = null;
            resources.ApplyResources(this.colConstantRewriteMode, "colConstantRewriteMode");
            this.colConstantRewriteMode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("colConstantRewriteMode.Buttons"))))});
            this.colConstantRewriteMode.Name = "colConstantRewriteMode";
            // 
            // colConstantDelBtn
            // 
            resources.ApplyResources(this.colConstantDelBtn, "colConstantDelBtn");
            this.colConstantDelBtn.ColumnEdit = this.colConstantDel;
            this.colConstantDelBtn.Name = "colConstantDelBtn";
            this.colConstantDelBtn.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // colConstantDel
            // 
            this.colConstantDel.AccessibleDescription = null;
            this.colConstantDel.AccessibleName = null;
            resources.ApplyResources(this.colConstantDel, "colConstantDel");
            this.colConstantDel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.colConstantDel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("colConstantDel.Buttons"))), resources.GetString("colConstantDel.Buttons1"), ((int)(resources.GetObject("colConstantDel.Buttons2"))), ((bool)(resources.GetObject("colConstantDel.Buttons3"))), ((bool)(resources.GetObject("colConstantDel.Buttons4"))), ((bool)(resources.GetObject("colConstantDel.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("colConstantDel.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("colConstantDel.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("colConstantDel.Buttons8"), null, null, ((bool)(resources.GetObject("colConstantDel.Buttons9"))))});
            this.colConstantDel.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.colConstantDel.Name = "colConstantDel";
            this.colConstantDel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.colConstantDel.UseParentBackground = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.AccessibleDescription = null;
            this.pnlBottom.AccessibleName = null;
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Name = "pnlBottom";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // SetMainWorkItemDataForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.pnlBottom);
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetMainWorkItemDataForm";
            this.Load += new System.EventHandler(this.SetMainWorkItemDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainWorkItemFieldColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelfItemFieldColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RewriteModeColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConstants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantComBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantSelfItemField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantRewriteMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colConstantDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bsConstants;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl dataGridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn MinDeleteBtn;
        private DevExpress.XtraGrid.GridControl dataGridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colConstantDelBtn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox colConstantDel;
        private DevExpress.XtraGrid.Columns.GridColumn mainWorkItemFieldCombBoxColumn;
        private DevExpress.XtraGrid.Columns.GridColumn selfItemFieldCombBoxColumn;
        private DevExpress.XtraGrid.Columns.GridColumn rewriteModeComBoxColumn;
        private DevExpress.XtraGrid.Columns.GridColumn colConstantMainFormProperty;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox colConstantComBox;
        private DevExpress.XtraGrid.Columns.GridColumn colConstantSelfPropertyCombBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit colConstantSelfItemField;
        private DevExpress.XtraGrid.Columns.GridColumn colConstantRewriteModeCombox;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox colConstantRewriteMode;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox MainWorkItemFieldColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox SelfItemFieldColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox RewriteModeColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox btnMainDelete;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
	}
}
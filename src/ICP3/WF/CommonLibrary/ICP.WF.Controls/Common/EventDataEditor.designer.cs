namespace ICP.WF.Controls
{
	partial class EventDataEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventDataEditor));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.bsEventList = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbEventType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbSource = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.clTarget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbTarget = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.MinDeleteBtn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnMainDelete = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsEventList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEventType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsEventList
            // 
            this.bsEventList.DataSource = typeof(ICP.WF.Controls.MappingRelationItem);
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // cmbEventType
            // 
            resources.ApplyResources(this.cmbEventType, "cmbEventType");
            this.cmbEventType.Name = "cmbEventType";
            this.cmbEventType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbEventType.Properties.Buttons"))))});
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsEventList;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnMainDelete,
            this.cmbSource,
            this.cmbTarget});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clSource,
            this.clTarget,
            this.MinDeleteBtn});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // clSource
            // 
            resources.ApplyResources(this.clSource, "clSource");
            this.clSource.ColumnEdit = this.cmbSource;
            this.clSource.FieldName = "SourceField";
            this.clSource.Name = "clSource";
            // 
            // cmbSource
            // 
            resources.ApplyResources(this.cmbSource, "cmbSource");
            this.cmbSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSource.Buttons"))))});
            this.cmbSource.Name = "cmbSource";
            // 
            // clTarget
            // 
            resources.ApplyResources(this.clTarget, "clTarget");
            this.clTarget.ColumnEdit = this.cmbTarget;
            this.clTarget.FieldName = "TargetField";
            this.clTarget.Name = "clTarget";
            // 
            // cmbTarget
            // 
            resources.ApplyResources(this.cmbTarget, "cmbTarget");
            this.cmbTarget.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbTarget.Buttons"))))});
            this.cmbTarget.Name = "cmbTarget";
            // 
            // MinDeleteBtn
            // 
            this.MinDeleteBtn.ColumnEdit = this.btnMainDelete;
            this.MinDeleteBtn.Name = "MinDeleteBtn";
            this.MinDeleteBtn.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.MinDeleteBtn, "MinDeleteBtn");
            // 
            // btnMainDelete
            // 
            this.btnMainDelete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnMainDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("btnMainDelete.Buttons"))), resources.GetString("btnMainDelete.Buttons1"), ((int)(resources.GetObject("btnMainDelete.Buttons2"))), ((bool)(resources.GetObject("btnMainDelete.Buttons3"))), ((bool)(resources.GetObject("btnMainDelete.Buttons4"))), ((bool)(resources.GetObject("btnMainDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("btnMainDelete.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("btnMainDelete.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("btnMainDelete.Buttons8"), null, null, ((bool)(resources.GetObject("btnMainDelete.Buttons9"))))});
            this.btnMainDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnMainDelete.Name = "btnMainDelete";
            this.btnMainDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnMainDelete.UseParentBackground = true;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmbEventType);
            this.pnlTop.Controls.Add(this.labelControl1);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gridControl1);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsEventList;
            // 
            // EventDataEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventDataEditor";
            ((System.ComponentModel.ISupportInitialize)(this.bsEventList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEventType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.BindingSource bsEventList;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbEventType;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn clSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbSource;
        private DevExpress.XtraGrid.Columns.GridColumn clTarget;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbTarget;
        private DevExpress.XtraGrid.Columns.GridColumn MinDeleteBtn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox btnMainDelete;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
	}
}
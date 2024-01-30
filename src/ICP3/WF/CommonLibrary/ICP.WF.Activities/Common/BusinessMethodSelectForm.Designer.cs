namespace ICP.WF.Activities.Common
{
	partial class BusinessMethodSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessMethodSelectForm));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeMetods = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.txtMethodDesc = new DevExpress.XtraEditors.MemoEdit();
            this.dgvParams = new DevExpress.XtraGrid.GridControl();
            this.bsParams = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.aliasNameDataGridViewTextBoxColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.parameterNameDataGridViewTextBoxColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imgCombBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.ValueComBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.txtParameterDesc = new DevExpress.XtraEditors.MemoEdit();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMetods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMethodDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCombBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueComBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParameterDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeMetods);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtMethodDesc);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel2.Controls.Add(this.dgvParams);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtParameterDesc);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.SplitterPosition = 238;
            // 
            // treeMetods
            // 
            this.treeMetods.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            resources.ApplyResources(this.treeMetods, "treeMetods");
            this.treeMetods.Name = "treeMetods";
            this.treeMetods.OptionsBehavior.Editable = false;
            this.treeMetods.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeMetods_AfterSelect);
            this.treeMetods.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeMetods_CellValueChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.FieldName = "AliasName";
            this.treeListColumn1.Name = "treeListColumn1";
            resources.ApplyResources(this.treeListColumn1, "treeListColumn1");
            // 
            // txtMethodDesc
            // 
            resources.ApplyResources(this.txtMethodDesc, "txtMethodDesc");
            this.txtMethodDesc.Name = "txtMethodDesc";
            this.txtMethodDesc.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtMethodDesc.Properties.Appearance.Options.UseForeColor = true;
            this.txtMethodDesc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtMethodDesc.Properties.ReadOnly = true;
            // 
            // dgvParams
            // 
            this.dgvParams.DataSource = this.bsParams;
            resources.ApplyResources(this.dgvParams, "dgvParams");
            this.dgvParams.MainView = this.gridView1;
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.imgCombBox,
            this.ValueComBox});
            this.dgvParams.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bsParams
            // 
            this.bsParams.CurrentChanged += new System.EventHandler(this.bsParams_CurrentChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.aliasNameDataGridViewTextBoxColumn,
            this.parameterNameDataGridViewTextBoxColumn});
            this.gridView1.GridControl = this.dgvParams;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // aliasNameDataGridViewTextBoxColumn
            // 
            resources.ApplyResources(this.aliasNameDataGridViewTextBoxColumn, "aliasNameDataGridViewTextBoxColumn");
            this.aliasNameDataGridViewTextBoxColumn.ColumnEdit = this.repositoryItemTextEdit1;
            this.aliasNameDataGridViewTextBoxColumn.FieldName = "AliasName";
            this.aliasNameDataGridViewTextBoxColumn.Name = "aliasNameDataGridViewTextBoxColumn";
            // 
            // repositoryItemTextEdit1
            // 
            resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // parameterNameDataGridViewTextBoxColumn
            // 
            resources.ApplyResources(this.parameterNameDataGridViewTextBoxColumn, "parameterNameDataGridViewTextBoxColumn");
            this.parameterNameDataGridViewTextBoxColumn.ColumnEdit = this.imgCombBox;
            this.parameterNameDataGridViewTextBoxColumn.FieldName = "ParameterOriginalValue";
            this.parameterNameDataGridViewTextBoxColumn.Name = "parameterNameDataGridViewTextBoxColumn";
            // 
            // imgCombBox
            // 
            resources.ApplyResources(this.imgCombBox, "imgCombBox");
            this.imgCombBox.Name = "imgCombBox";
            // 
            // ValueComBox
            // 
            this.ValueComBox.Name = "ValueComBox";
            // 
            // txtParameterDesc
            // 
            resources.ApplyResources(this.txtParameterDesc, "txtParameterDesc");
            this.txtParameterDesc.Name = "txtParameterDesc";
            this.txtParameterDesc.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtParameterDesc.Properties.Appearance.Options.UseForeColor = true;
            this.txtParameterDesc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtParameterDesc.Properties.ReadOnly = true;
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // BusinessMethodSelectForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.pnlBottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BusinessMethodSelectForm";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMetods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMethodDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCombBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueComBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParameterDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.BindingSource bsParams;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.MemoEdit txtMethodDesc;
        private DevExpress.XtraTreeList.TreeList treeMetods;
        private DevExpress.XtraEditors.MemoEdit txtParameterDesc;
        private DevExpress.XtraGrid.GridControl dgvParams;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn aliasNameDataGridViewTextBoxColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn parameterNameDataGridViewTextBoxColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imgCombBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox ValueComBox;
	}
}
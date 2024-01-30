namespace  ICP.WF.Controls
{
    partial class DataItemEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataItemEditor));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.bsItemList = new System.Windows.Forms.BindingSource(this.components);
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.clType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.clLength = new DevExpress.XtraGrid.Columns.GridColumn();
            this.numLength = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.clMust = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckbMust = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.clCpation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtCaption = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.MinDeleteBtn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnMainDelete = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbMust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsItemList
            // 
            this.bsItemList.DataSource = typeof(ICP.WF.Controls.DataColumnItem);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gridControl1);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsItemList;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnMainDelete,
            this.txtName,
            this.cmbType,
            this.numLength,
            this.ckbMust,
            this.txtCaption});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clName,
            this.clType,
            this.clLength,
            this.clMust,
            this.clCpation,
            this.MinDeleteBtn});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // clName
            // 
            resources.ApplyResources(this.clName, "clName");
            this.clName.ColumnEdit = this.txtName;
            this.clName.FieldName = "ColumnName";
            this.clName.Name = "clName";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // clType
            // 
            resources.ApplyResources(this.clType, "clType");
            this.clType.ColumnEdit = this.cmbType;
            this.clType.FieldName = "ColumnType";
            this.clType.Name = "clType";
            // 
            // cmbType
            // 
            resources.ApplyResources(this.cmbType, "cmbType");
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbType.Buttons"))))});
            this.cmbType.Name = "cmbType";
            // 
            // clLength
            // 
            resources.ApplyResources(this.clLength, "clLength");
            this.clLength.ColumnEdit = this.numLength;
            this.clLength.FieldName = "MaxLength";
            this.clLength.Name = "clLength";
            // 
            // numLength
            // 
            resources.ApplyResources(this.numLength, "numLength");
            this.numLength.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("numLength.Buttons"))))});
            this.numLength.Name = "numLength";
            // 
            // clMust
            // 
            resources.ApplyResources(this.clMust, "clMust");
            this.clMust.ColumnEdit = this.ckbMust;
            this.clMust.FieldName = "AllowNull";
            this.clMust.Name = "clMust";
            // 
            // ckbMust
            // 
            resources.ApplyResources(this.ckbMust, "ckbMust");
            this.ckbMust.Name = "ckbMust";
            // 
            // clCpation
            // 
            resources.ApplyResources(this.clCpation, "clCpation");
            this.clCpation.ColumnEdit = this.txtCaption;
            this.clCpation.FieldName = "Caption";
            this.clCpation.Name = "clCpation";
            // 
            // txtCaption
            // 
            resources.ApplyResources(this.txtCaption, "txtCaption");
            this.txtCaption.Name = "txtCaption";
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Clic);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsItemList;
            // 
            // DataItemEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Name = "DataItemEditor";
            ((System.ComponentModel.ISupportInitialize)(this.bsItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbMust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsItemList;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn MinDeleteBtn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox btnMainDelete;
        private DevExpress.XtraGrid.Columns.GridColumn clName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtName;
        private DevExpress.XtraGrid.Columns.GridColumn clType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraGrid.Columns.GridColumn clLength;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit numLength;
        private DevExpress.XtraGrid.Columns.GridColumn clMust;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckbMust;
        private DevExpress.XtraGrid.Columns.GridColumn clCpation;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtCaption;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}

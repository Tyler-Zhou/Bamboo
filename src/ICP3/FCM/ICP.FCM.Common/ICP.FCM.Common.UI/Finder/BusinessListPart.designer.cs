namespace ICP.FCM.Common.UI.Finder
{
    partial class BusinessListPart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOperationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbOEOperationType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.pccHyperLinks = new DevExpress.XtraEditors.PopupContainerControl();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOEOperationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pccHyperLinks)).BeginInit();
            this.pccHyperLinks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.BusinessData);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbState,
            this.repositoryItemCheckEdit1,
            this.cmbOEOperationType});
            this.gcMain.Size = new System.Drawing.Size(707, 376);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperationType,
            this.colState,
            this.colNo,
            this.colCustomerName,
            this.colMBLNo,
            this.colHBLNo,
            this.colPOLName,
            this.colPODName,
            this.colETD,
            this.colETA,
            this.colCreateDate,
            this.colSalesName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvMain_CustomDrawRowIndicator);
            this.gvMain.MouseEnter += new System.EventHandler(this.gvMain_MouseEnter);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.DoubleClick += new System.EventHandler(gvMain_DoubleClick);
            // 
            // colOperationType
            // 
            this.colOperationType.ColumnEdit = this.cmbOEOperationType;
            this.colOperationType.FieldName = "OperationType";
            this.colOperationType.Name = "colOperationType";
            this.colOperationType.Visible = true;
            this.colOperationType.VisibleIndex = 0;
            // 
            // cmbOEOperationType
            // 
            this.cmbOEOperationType.AutoHeight = false;
            this.cmbOEOperationType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOEOperationType.Name = "cmbOEOperationType";
            // 
            // colState
            // 
            this.colState.ColumnEdit = this.rcmbState;
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 1;
            this.colState.Width = 60;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // colNo
            // 
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowMove = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 2;
            this.colNo.Width = 120;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 3;
            this.colCustomerName.Width = 120;
            // 
            // colMBLNo
            // 
            this.colMBLNo.FieldName = "MBLNo";
            this.colMBLNo.Name = "colMBLNo";
            this.colMBLNo.OptionsColumn.FixedWidth = true;
            this.colMBLNo.Visible = true;
            this.colMBLNo.VisibleIndex = 4;
            this.colMBLNo.Width = 120;
            // 
            // colHBLNo
            // 
            this.colHBLNo.FieldName = "HBLNo";
            this.colHBLNo.Name = "colHBLNo";
            this.colHBLNo.OptionsColumn.FixedWidth = true;
            this.colHBLNo.Visible = true;
            this.colHBLNo.VisibleIndex = 5;
            this.colHBLNo.Width = 120;
            // 
            // colPOLName
            // 
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 6;
            this.colPOLName.Width = 120;
            // 
            // colPODName
            // 
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 7;
            this.colPODName.Width = 120;
            // 
            // colETD
            // 
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 8;
            this.colETD.Width = 80;
            // 
            // colETA
            // 
            this.colETA.Caption = "ETA";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 9;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 11;
            this.colCreateDate.Width = 80;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 10;
            this.colSalesName.Width = 80;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // pccHyperLinks
            // 
            this.pccHyperLinks.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.pccHyperLinks.Controls.Add(this.hyperLinkEdit1);
            this.pccHyperLinks.Location = new System.Drawing.Point(198, 92);
            this.pccHyperLinks.Name = "pccHyperLinks";
            this.pccHyperLinks.Size = new System.Drawing.Size(200, 100);
            this.pccHyperLinks.TabIndex = 1;
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.Location = new System.Drawing.Point(22, 21);
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Size = new System.Drawing.Size(100, 21);
            this.hyperLinkEdit1.TabIndex = 0;
            // 
            // BusinessListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pccHyperLinks);
            this.Controls.Add(this.gcMain);
            this.Name = "BusinessListPart";
            this.Size = new System.Drawing.Size(707, 376);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOEOperationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pccHyperLinks)).EndInit();
            this.pccHyperLinks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }   

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colMBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colHBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraEditors.PopupContainerControl pccHyperLinks;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbOEOperationType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationType;
    }
}

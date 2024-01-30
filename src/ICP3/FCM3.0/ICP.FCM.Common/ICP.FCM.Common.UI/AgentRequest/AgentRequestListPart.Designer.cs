namespace ICP.FCM.Common.UI.AgentRequest
{
    partial class AgentRequestListPart
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
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSenderRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSenderName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSenderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSolverName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSolveDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.AgentRequestInfo);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbType});
            this.gcMain.Size = new System.Drawing.Size(714, 248);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperationNo,
            this.colAgentName,
            this.colPOD,
            this.colType,
            this.colSenderRemark,
            this.colSenderName,
            this.colSenderDate,
            this.colSolverName,
            this.colSolveDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colOperationNo
            // 
            this.colOperationNo.Caption = "OperationNo";
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 0;
            this.colOperationNo.Width = 120;
            // 
            // colAgentName
            // 
            this.colAgentName.Caption = "Agent";
            this.colAgentName.FieldName = "AgentName";
            this.colAgentName.Name = "colAgentName";
            this.colAgentName.Visible = true;
            this.colAgentName.VisibleIndex = 1;
            this.colAgentName.Width = 120;
            // 
            // colPOD
            // 
            this.colPOD.Caption = "POD";
            this.colPOD.FieldName = "POD";
            this.colPOD.Name = "colPOD";
            this.colPOD.Visible = true;
            this.colPOD.VisibleIndex = 2;
            this.colPOD.Width = 120;
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.ColumnEdit = this.rcmbType;
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 3;
            this.colType.Width = 80;
            // 
            // rcmbType
            // 
            this.rcmbType.AutoHeight = false;
            this.rcmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbType.Name = "rcmbType";
            // 
            // colSenderRemark
            // 
            this.colSenderRemark.Caption = "SenderRemark";
            this.colSenderRemark.FieldName = "SenderRemark";
            this.colSenderRemark.Name = "colSenderRemark";
            this.colSenderRemark.Visible = true;
            this.colSenderRemark.VisibleIndex = 4;
            this.colSenderRemark.Width = 120;
            // 
            // colSenderName
            // 
            this.colSenderName.Caption = "Sender";
            this.colSenderName.FieldName = "SenderName";
            this.colSenderName.Name = "colSenderName";
            this.colSenderName.Visible = true;
            this.colSenderName.VisibleIndex = 5;
            this.colSenderName.Width = 100;
            // 
            // colSenderDate
            // 
            this.colSenderDate.Caption = "SendDate";
            this.colSenderDate.FieldName = "SendDate";
            this.colSenderDate.Name = "colSenderDate";
            this.colSenderDate.Visible = true;
            this.colSenderDate.VisibleIndex = 6;
            this.colSenderDate.Width = 100;
            // 
            // colSolverName
            // 
            this.colSolverName.Caption = "Solver";
            this.colSolverName.FieldName = "SolverName";
            this.colSolverName.Name = "colSolverName";
            this.colSolverName.Visible = true;
            this.colSolverName.VisibleIndex = 7;
            this.colSolverName.Width = 100;
            // 
            // colSolveDate
            // 
            this.colSolveDate.Caption = "SolveDate";
            this.colSolveDate.FieldName = "SolveDate";
            this.colSolveDate.Name = "colSolveDate";
            this.colSolveDate.Visible = true;
            this.colSolveDate.VisibleIndex = 8;
            this.colSolveDate.Width = 100;
            // 
            // AgentRequestListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "AgentRequestListPart";
            this.Size = new System.Drawing.Size(714, 248);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbType;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAgentName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOD;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colSenderRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colSenderName;
        private DevExpress.XtraGrid.Columns.GridColumn colSenderDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSolverName;
        private DevExpress.XtraGrid.Columns.GridColumn colSolveDate;
    }
}

namespace ICP.Sys.UI.Organization
{
    partial class OrganizationListPart
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
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateBy = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.OrganizationList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // treeMain
            // 
            this.treeMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeMain.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCode,
            this.colCName,
            this.colEName,
            this.colCreateBy,
            this.colCreateDate});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.DragNodes = true;
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsSelection.InvertSelection = true;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(661, 350);
            this.treeMain.TabIndex = 6;
            this.treeMain.IndicatorWidth = 45;
            this.treeMain.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(this.treeMain_CustomDrawNodeIndicator);     
            this.treeMain.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeMain_AfterDragNode);
            this.treeMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeMain_NodeCellStyle);
            this.treeMain.BeforeDragNode += new DevExpress.XtraTreeList.BeforeDragNodeEventHandler(this.treeMain_BeforeDragNode);
            this.treeMain.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.treeMain_BeforeFocusNode);
           
            // 
            // colCode
            // 
            this.colCode.Caption = "Code";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 68;
            // 
            // colCName
            // 
            this.colCName.Caption = "CName";
            this.colCName.FieldName = "CShortName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 1;
            this.colCName.Width = 68;
            // 
            // colEName
            // 
            this.colEName.Caption = "EName";
            this.colEName.FieldName = "EShortName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 2;
            this.colEName.Width = 68;
            // 
            // colCreateBy
            // 
            this.colCreateBy.FieldName = "CreateBy";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 3;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 4;
            // 
            // OrganizationListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Name = "OrganizationListPart";
            this.Size = new System.Drawing.Size(661, 350);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateBy;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateDate;
    }
}

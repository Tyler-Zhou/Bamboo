namespace ICP.FAM.UI.TelexApply
{
    partial class TelexApplyListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelexApplyListPart));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCustomerCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColConsignee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValidDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchk = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCustomerEName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchk});
            this.gcMain.Size = new System.Drawing.Size(802, 409);
            this.gcMain.TabIndex = 35;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.TelexApplyList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCustomerCName,
            this.ColConsignee,
            this.colApplyDate,
            this.colCreateByName,
            this.colIsValid,
            this.colValidDate,
            this.colRemark,
            this.colCustomerEName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colCustomerCName
            // 
            this.colCustomerCName.Caption = "客户中文名称";
            this.colCustomerCName.FieldName = "CustomerCName";
            this.colCustomerCName.Name = "colCustomerCName";
            this.colCustomerCName.OptionsColumn.ReadOnly = true;
            this.colCustomerCName.Visible = true;
            this.colCustomerCName.VisibleIndex = 0;
            this.colCustomerCName.Width = 150;
            // 
            // ColConsignee
            // 
            this.ColConsignee.Caption = "收货人";
            this.ColConsignee.FieldName = "ConsigneeName";
            this.ColConsignee.Name = "ColConsignee";
            this.ColConsignee.OptionsColumn.ReadOnly = true;
            this.ColConsignee.Visible = true;
            this.ColConsignee.VisibleIndex = 2;
            this.ColConsignee.Width = 220;
            // 
            // colApplyDate
            // 
            this.colApplyDate.Caption = "申请时间";
            this.colApplyDate.FieldName = "ApplyTime";
            this.colApplyDate.Name = "colApplyDate";
            this.colApplyDate.OptionsColumn.ReadOnly = true;
            this.colApplyDate.Visible = true;
            this.colApplyDate.VisibleIndex = 3;
            this.colApplyDate.Width = 80;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "申请人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.ReadOnly = true;
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 4;
            this.colCreateByName.Width = 80;
            // 
            // colIsValid
            // 
            this.colIsValid.Caption = "有效性";
            this.colIsValid.FieldName = "IsValid";
            this.colIsValid.Name = "colIsValid";
            this.colIsValid.Visible = true;
            this.colIsValid.VisibleIndex = 5;
            // 
            // colValidDate
            // 
            this.colValidDate.Caption = "有效期";
            this.colValidDate.FieldName = "ValidDate";
            this.colValidDate.Name = "colValidDate";
            this.colValidDate.Visible = true;
            this.colValidDate.VisibleIndex = 6;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.OptionsColumn.ReadOnly = true;
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 7;
            this.colRemark.Width = 100;
            // 
            // rchk
            // 
            this.rchk.AutoHeight = false;
            this.rchk.Name = "rchk";
            // 
            // colCustomerEName
            // 
            this.colCustomerEName.Caption = "客户英文名称";
            this.colCustomerEName.FieldName = "CustomerEName";
            this.colCustomerEName.Name = "colCustomerEName";
            this.colCustomerEName.Visible = true;
            this.colCustomerEName.VisibleIndex = 1;
            this.colCustomerEName.Width = 109;
            // 
            // TelexApplyListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Name = "TelexApplyListPart";
            this.Size = new System.Drawing.Size(802, 409);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerCName;
        private DevExpress.XtraGrid.Columns.GridColumn ColConsignee;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchk;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colValidDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsValid;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerEName;
    }
}

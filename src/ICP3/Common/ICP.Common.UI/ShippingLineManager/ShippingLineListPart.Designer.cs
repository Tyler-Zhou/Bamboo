namespace ICP.Common.UI.ShippingLineManager
{
    partial class ShippingLineListPart
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
            this.treeSelectBox3 = new ICP.Framework.ClientComponents.Controls.TreeSelectBox();
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateBy = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            this.SuspendLayout();
            // 
            // treeSelectBox3
            // 
            this.treeSelectBox3.AllText = "Selecte ALL";
            this.treeSelectBox3.EditValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.treeSelectBox3.Location = new System.Drawing.Point(0, 0);
            this.treeSelectBox3.Name = "treeSelectBox3";
            //this.treeSelectBox3.OnlyLeafNodeCanSelect = false;
            this.treeSelectBox3.ReadOnly = false;
            this.treeSelectBox3.Size = new System.Drawing.Size(243, 21);
            this.treeSelectBox3.SpecifiedBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.treeSelectBox3.TabIndex = 2;
            // 
            // treeMain
            // 
            this.treeMain.AllowDrop = true;
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
            this.treeMain.IndicatorWidth = 45;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.DragNodes = true;
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsSelection.InvertSelection = true;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(601, 409);
            this.treeMain.TabIndex = 6;
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
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 1;
            this.colCName.Width = 68;
            // 
            // colEName
            // 
            this.colEName.Caption = "EName";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 2;
            this.colEName.Width = 68;
            // 
            // colCreateBy
            // 
            this.colCreateBy.FieldName = "CreateByName";
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
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ShippingLineList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // ShippingLineListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Name = "ShippingLineListPart";
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.TreeSelectBox treeSelectBox3;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateBy;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateDate;

    }
}

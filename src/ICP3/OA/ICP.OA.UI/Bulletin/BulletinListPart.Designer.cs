namespace ICP.OA.UI.Bulletin
{
    partial class BulletinListPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublisher = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.OA.ServiceInterface.DataObjects.BulletinData);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(935, 409);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSubject,
            this.colFromTime,
            this.colToTime,
            this.colPublisher});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 27;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsSelection.UseIndicatorForSelection = false;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            // 
            // colSubject
            // 
            this.colSubject.Caption = "Subject";
            this.colSubject.FieldName = "Subject";
            this.colSubject.Name = "colSubject";
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 0;
            this.colSubject.Width = 406;
            // 
            // colFromTime
            // 
            this.colFromTime.Caption = "FromTime";
            this.colFromTime.FieldName = "FromTime";
            this.colFromTime.Name = "colFromTime";
            this.colFromTime.Visible = true;
            this.colFromTime.VisibleIndex = 1;
            this.colFromTime.Width = 109;
            // 
            // colToTime
            // 
            this.colToTime.Caption = "ToTime";
            this.colToTime.FieldName = "ToTime";
            this.colToTime.Name = "colToTime";
            this.colToTime.Visible = true;
            this.colToTime.VisibleIndex = 2;
            this.colToTime.Width = 147;
            // 
            // colPublisher
            // 
            this.colPublisher.Caption = "Publisher";
            this.colPublisher.FieldName = "Publisher";
            this.colPublisher.Name = "colPublisher";
            this.colPublisher.Visible = true;
            this.colPublisher.VisibleIndex = 3;
            this.colPublisher.Width = 88;
            // 
            // BulletinListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "BulletinListPart";
            this.Size = new System.Drawing.Size(935, 409);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colFromTime;
        private DevExpress.XtraGrid.Columns.GridColumn colToTime;
        private DevExpress.XtraGrid.Columns.GridColumn colPublisher;

    }
}

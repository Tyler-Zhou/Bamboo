namespace ICP.Operation.Common.UI
{
    partial class ListBaseBusinessPart
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
            this.gridControlList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewList = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.splitBusinessPart = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelMore = new System.Windows.Forms.Panel();
            this.linkLabelMore = new System.Windows.Forms.LinkLabel();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.baseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitBusinessPart)).BeginInit();
            this.splitBusinessPart.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMore.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlList
            // 
            this.gridControlList.AllowDrop = true;
            this.gridControlList.ContextMenuStrip = this.contextMenuStrip;
            this.gridControlList.DataSource = this.bindingSource;
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.Location = new System.Drawing.Point(0, 0);
            this.gridControlList.MainView = this.gridViewList;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.Size = new System.Drawing.Size(505, 355);
            this.gridControlList.TabIndex = 5;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            this.gridControlList.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControlList_DragDrop);
            this.gridControlList.DragEnter += new System.Windows.Forms.DragEventHandler(this.gridControlList_DragEnter);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // gridViewList
            // 
            this.gridViewList.GridControl = this.gridControlList;
            this.gridViewList.Name = "gridViewList";
            this.gridViewList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridViewList.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewList.OptionsCustomization.AllowRowSizing = true;
            this.gridViewList.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewList.OptionsSelection.MultiSelect = true;
            this.gridViewList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewList.OptionsView.ColumnAutoWidth = false;
            this.gridViewList.OptionsView.ShowGroupPanel = false;
            this.gridViewList.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewList_RowClick);
            this.gridViewList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewList_RowCellClick);
            this.gridViewList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewList_RowCellStyle);
            this.gridViewList.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewList_RowStyle);
            this.gridViewList.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewList_CustomRowCellEdit);
            this.gridViewList.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.OnColumnWidthChanged);
            this.gridViewList.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridViewList_SelectionChanged);
            this.gridViewList.ColumnPositionChanged += new System.EventHandler(this.OnColumnPositionChanged);
            this.gridViewList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnGridViewFocusedRowChanged);
            this.gridViewList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.OnGeidViewCellValueChanged);
            this.gridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridViewList_KeyDown);
            this.gridViewList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewList_MouseDown);
            this.gridViewList.RowCountChanged += new System.EventHandler(this.gridViewList_RowCountChanged);
            // 
            // splitBusinessPart
            // 
            this.splitBusinessPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBusinessPart.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitBusinessPart.Horizontal = false;
            this.splitBusinessPart.Location = new System.Drawing.Point(0, 24);
            this.splitBusinessPart.Name = "splitBusinessPart";
            this.splitBusinessPart.Panel1.Controls.Add(this.panel2);
            this.splitBusinessPart.Panel1.Controls.Add(this.panelMore);
            this.splitBusinessPart.Panel1.Text = "split4GridControl";
            this.splitBusinessPart.Panel2.Text = "Split4customerList";
            this.splitBusinessPart.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitBusinessPart.Size = new System.Drawing.Size(505, 376);
            this.splitBusinessPart.SplitterPosition = 87;
            this.splitBusinessPart.TabIndex = 6;
            this.splitBusinessPart.Text = "BusinessPart";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridControlList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 355);
            this.panel2.TabIndex = 7;
            // 
            // panelMore
            // 
            this.panelMore.Controls.Add(this.linkLabelMore);
            this.panelMore.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMore.Location = new System.Drawing.Point(0, 355);
            this.panelMore.Name = "panelMore";
            this.panelMore.Size = new System.Drawing.Size(505, 21);
            this.panelMore.TabIndex = 6;
            // 
            // linkLabelMore
            // 
            this.linkLabelMore.AutoSize = true;
            this.linkLabelMore.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabelMore.Location = new System.Drawing.Point(0, 0);
            this.linkLabelMore.Name = "linkLabelMore";
            this.linkLabelMore.Size = new System.Drawing.Size(0, 14);
            this.linkLabelMore.TabIndex = 0;
            this.linkLabelMore.Click += new System.EventHandler(this.linkLabelMore_Click);
            // 
            // ListBaseBusinessPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitBusinessPart);
            this.Name = "ListBaseBusinessPart";
            this.Size = new System.Drawing.Size(505, 400);
            this.Controls.SetChildIndex(this.splitBusinessPart, 0);
            ((System.ComponentModel.ISupportInitialize)(this.baseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitBusinessPart)).EndInit();
            this.splitBusinessPart.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelMore.ResumeLayout(false);
            this.panelMore.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public ICP.Framework.ClientComponents.Controls.LWGridControl gridControlList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        public ICP.Framework.ClientComponents.Controls.LWGridView gridViewList;
        public System.Windows.Forms.BindingSource bindingSource;
        public DevExpress.XtraEditors.SplitContainerControl splitBusinessPart;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panelMore;
        private System.Windows.Forms.LinkLabel linkLabelMore;

    }
}

namespace ICP.FRM.UI.InquireRates
{
    partial class DiscussingPart
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
            this.labToMan = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colDiscussingFromName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBizSentTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBizContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // labToMan
            // 
            this.labToMan.Location = new System.Drawing.Point(5, 1);
            this.labToMan.Name = "labToMan";
            this.labToMan.Size = new System.Drawing.Size(30, 14);
            this.labToMan.TabIndex = 0;
            this.labToMan.Text = "Carrie";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtContent);
            this.pnlMain.Controls.Add(this.panelControl1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(417, 68);
            this.pnlMain.TabIndex = 1;
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(103, 2);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(312, 64);
            this.txtContent.TabIndex = 14;
            this.txtContent.ToolTip = "Press [Ctrl + Enter] to send the discussing directly.";
            this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSend);
            this.panelControl1.Controls.Add(this.labToMan);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(101, 64);
            this.panelControl1.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(5, 40);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 23);
            this.btnSend.TabIndex = 15;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcMain.Location = new System.Drawing.Point(0, 68);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gcMain.Size = new System.Drawing.Size(417, 224);
            this.gcMain.TabIndex = 16;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.InquireDiscussing);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDiscussingFromName,
            this.colBizSentTime,
            this.colBizContent});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.gvMain_CalcRowHeight);
            
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colDiscussingFromName
            // 
            this.colDiscussingFromName.Caption = "From";
            this.colDiscussingFromName.FieldName = "DiscussingFromName";
            this.colDiscussingFromName.MaxWidth = 70;
            this.colDiscussingFromName.Name = "colDiscussingFromName";
            this.colDiscussingFromName.Visible = true;
            this.colDiscussingFromName.VisibleIndex = 0;
            this.colDiscussingFromName.Width = 70;
            // 
            // colBizSentTime
            // 
            this.colBizSentTime.Caption = "Sent Time";
            this.colBizSentTime.FieldName = "BizSentTime";
            this.colBizSentTime.MaxWidth = 80;
            this.colBizSentTime.Name = "colBizSentTime";
            this.colBizSentTime.Visible = true;
            this.colBizSentTime.VisibleIndex = 1;
            this.colBizSentTime.Width = 80;
            // 
            // colBizContent
            // 
            this.colBizContent.Caption = "Content";
            this.colBizContent.FieldName = "BizContent";
            this.colBizContent.Name = "colBizContent";
            this.colBizContent.OptionsColumn.AllowEdit = false;
            this.colBizContent.Visible = true;
            this.colBizContent.VisibleIndex = 2;
            this.colBizContent.Width = 246;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // DiscussingPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlMain);
            this.Name = "DiscussingPart";
            this.Size = new System.Drawing.Size(417, 292);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }      

        #endregion

        private DevExpress.XtraEditors.LabelControl labToMan;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscussingFromName;
        private DevExpress.XtraGrid.Columns.GridColumn colBizContent;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBizSentTime;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}

namespace ICP.Business.Common.UI.Communication_History
{
    partial class EdiLogList
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
            this.gridControlList = new DevExpress.XtraGrid.GridControl();
            this.logDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEDIFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDIContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSenderName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSendTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBoxPriority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBoxWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBoxHasAttachment = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btDown = new System.Windows.Forms.ToolStripButton();
            this.btWeb = new System.Windows.Forms.ToolStripButton();
            this.barItemNew = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxHasAttachment)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlList
            // 
            this.gridControlList.DataSource = this.logDataBindingSource;
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.Location = new System.Drawing.Point(0, 30);
            this.gridControlList.MainView = this.gridViewList;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemImageComboBoxPriority,
            this.repositoryItemImageComboBoxWay,
            this.repositoryItemImageComboBoxHasAttachment});
            this.gridControlList.Size = new System.Drawing.Size(747, 379);
            this.gridControlList.TabIndex = 5;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            // 
            // logDataBindingSource
            // 
            this.logDataBindingSource.DataSource = typeof(ICP.EDI.ServiceInterface.DataObjects.LogData);
            // 
            // gridViewList
            // 
            this.gridViewList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewList.Appearance.Row.Options.UseTextOptions = true;
            this.gridViewList.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEDIFlag,
            this.colSubject,
            this.colEDIContent,
            this.colSenderName,
            this.colSendTime,
            this.colState,
            this.colRemark,
            this.colFileName});
            this.gridViewList.GridControl = this.gridControlList;
            this.gridViewList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewList.Name = "gridViewList";
            this.gridViewList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridViewList.OptionsBehavior.Editable = false;
            this.gridViewList.OptionsCustomization.AllowSort = false;
            this.gridViewList.OptionsDetail.AllowZoomDetail = false;
            this.gridViewList.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewList.OptionsDetail.ShowDetailTabs = false;
            this.gridViewList.OptionsDetail.SmartDetailExpand = false;
            this.gridViewList.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled;
            this.gridViewList.OptionsSelection.MultiSelect = true;
            this.gridViewList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewList.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewList.OptionsView.ShowDetailButtons = false;
            this.gridViewList.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewList.OptionsView.ShowGroupPanel = false;
            this.gridViewList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewList_CustomDrawRowIndicator);
            this.gridViewList.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewList_RowStyle);
            this.gridViewList.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewList_InitNewRow);
            this.gridViewList.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridViewList_CustomColumnDisplayText);
            this.gridViewList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewList_MouseDown);
            this.gridViewList.DoubleClick += new System.EventHandler(this.gridViewList_DoubleClick);
            // 
            // colEDIFlag
            // 
            this.colEDIFlag.FieldName = "EDIFlag";
            this.colEDIFlag.Name = "colEDIFlag";
            this.colEDIFlag.Visible = true;
            this.colEDIFlag.VisibleIndex = 0;
            this.colEDIFlag.Width = 92;
            // 
            // colSubject
            // 
            this.colSubject.FieldName = "Subject";
            this.colSubject.Name = "colSubject";
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 1;
            this.colSubject.Width = 218;
            // 
            // colEDIContent
            // 
            this.colEDIContent.FieldName = "EDIContent";
            this.colEDIContent.Name = "colEDIContent";
            this.colEDIContent.Visible = true;
            this.colEDIContent.VisibleIndex = 6;
            this.colEDIContent.Width = 229;
            // 
            // colSenderName
            // 
            this.colSenderName.FieldName = "SenderName";
            this.colSenderName.Name = "colSenderName";
            // 
            // colSendTime
            // 
            this.colSendTime.DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
            this.colSendTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSendTime.FieldName = "SendTime";
            this.colSendTime.Name = "colSendTime";
            this.colSendTime.Visible = true;
            this.colSendTime.VisibleIndex = 2;
            this.colSendTime.Width = 214;
            // 
            // colState
            // 
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 3;
            this.colState.Width = 85;
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 4;
            this.colRemark.Width = 257;
            // 
            // colFileName
            // 
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 5;
            this.colFileName.Width = 155;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemImageComboBoxPriority
            // 
            this.repositoryItemImageComboBoxPriority.AutoHeight = false;
            this.repositoryItemImageComboBoxPriority.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxPriority.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ICP.Message.ServiceInterface.MessagePriority.Normal, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ICP.Message.ServiceInterface.MessagePriority.High, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ICP.Message.ServiceInterface.MessagePriority.Low, 0)});
            this.repositoryItemImageComboBoxPriority.Name = "repositoryItemImageComboBoxPriority";
            // 
            // repositoryItemImageComboBoxWay
            // 
            this.repositoryItemImageComboBoxWay.AutoHeight = false;
            this.repositoryItemImageComboBoxWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxWay.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ICP.Message.ServiceInterface.MessageWay.Send, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ICP.Message.ServiceInterface.MessageWay.Receive, 1)});
            this.repositoryItemImageComboBoxWay.Name = "repositoryItemImageComboBoxWay";
            // 
            // repositoryItemImageComboBoxHasAttachment
            // 
            this.repositoryItemImageComboBoxHasAttachment.AutoHeight = false;
            this.repositoryItemImageComboBoxHasAttachment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxHasAttachment.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, -1)});
            this.repositoryItemImageComboBoxHasAttachment.Name = "repositoryItemImageComboBoxHasAttachment";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 30);
            this.panel1.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btDown,
            this.btWeb});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(747, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btDown
            // 
            this.btDown.Image = global::ICP.Business.Common.UI.Properties.Resources.open;
            this.btDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(76, 22);
            this.btDown.Text = "下载附件";
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btWeb
            // 
            this.btWeb.Image = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.btWeb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btWeb.Name = "btWeb";
            this.btWeb.Size = new System.Drawing.Size(76, 22);
            this.btWeb.Text = "打开网页";
            this.btWeb.Click += new System.EventHandler(this.btWeb_Click);
            // 
            // barItemNew
            // 
            this.barItemNew.Caption = "&New";
            this.barItemNew.Id = 6;
            this.barItemNew.Name = "barItemNew";
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 76);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(747, 76);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 48);
            this.barDockControlBottom.Size = new System.Drawing.Size(747, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(747, 76);
            // 
            // EdiLogList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlList);
            this.Controls.Add(this.panel1);
            this.Name = "EdiLogList";
            this.Size = new System.Drawing.Size(747, 409);
            this.Load += new System.EventHandler(this.EdiLogList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxHasAttachment)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxPriority;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxHasAttachment;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxWay;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.BarLargeButtonItem barItemNew;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btDown;
        private System.Windows.Forms.BindingSource logDataBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colEDIFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colEDIContent;
        private DevExpress.XtraGrid.Columns.GridColumn colSenderName;
        private DevExpress.XtraGrid.Columns.GridColumn colSendTime;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private System.Windows.Forms.ToolStripButton btWeb;
    }
}

namespace ICP.Business.Common.UI.Communication
{
    partial class UCCommunicationHistoryList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCommunicationHistoryList));
            this.gridControlList = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSend = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemrepl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAllrepl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAllReplyAttachment = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemforwardin = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMarkNRAS = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEntryID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSendTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSendFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIschoose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.colContactStage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPriority = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBoxPriority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListPriority = new System.Windows.Forms.ImageList(this.components);
            this.gridColumnHasAttachment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBoxHasAttachment = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListHasAttachment = new System.Windows.Forms.ImageList(this.components);
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSendFromName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStateDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageListGridView = new System.Windows.Forms.ImageList(this.components);
            this.repositoryItemImageComboBoxWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListWay = new System.Windows.Forms.ImageList(this.components);
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxHasAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxWay)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlList
            // 
            this.gridControlList.ContextMenuStrip = this.contextMenuStrip;
            this.gridControlList.DataSource = this.bindingSource;
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.Location = new System.Drawing.Point(0, 0);
            this.gridControlList.MainView = this.gridViewList;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemImageComboBoxPriority,
            this.repositoryItemImageComboBoxWay,
            this.repositoryItemImageComboBoxHasAttachment});
            this.gridControlList.Size = new System.Drawing.Size(586, 150);
            this.gridControlList.TabIndex = 4;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemOpen,
            this.MenuItemSend,
            this.MenuItemrepl,
            this.MenuItemAllrepl,
            this.MenuItemAllReplyAttachment,
            this.MenuItemforwardin,
            this.MenuItemMarkNRAS});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(200, 158);
            // 
            // MenuItemOpen
            // 
            this.MenuItemOpen.Name = "MenuItemOpen";
            this.MenuItemOpen.Size = new System.Drawing.Size(199, 22);
            this.MenuItemOpen.Text = "Open";
            // 
            // MenuItemSend
            // 
            this.MenuItemSend.Name = "MenuItemSend";
            this.MenuItemSend.Size = new System.Drawing.Size(199, 22);
            this.MenuItemSend.Text = "Send";
            // 
            // MenuItemrepl
            // 
            this.MenuItemrepl.Image = global::ICP.Business.Common.UI.Properties.Resources.toolReply_Image;
            this.MenuItemrepl.Name = "MenuItemrepl";
            this.MenuItemrepl.Size = new System.Drawing.Size(199, 22);
            this.MenuItemrepl.Text = "Reply";
            // 
            // MenuItemAllrepl
            // 
            this.MenuItemAllrepl.Image = global::ICP.Business.Common.UI.Properties.Resources.toolReplyAll_Image;
            this.MenuItemAllrepl.Name = "MenuItemAllrepl";
            this.MenuItemAllrepl.Size = new System.Drawing.Size(199, 22);
            this.MenuItemAllrepl.Text = "ReplyAll ";
            // 
            // MenuItemAllReplyAttachment
            // 
            this.MenuItemAllReplyAttachment.Image = global::ICP.Business.Common.UI.Properties.Resources.toolReplyAll_Image;
            this.MenuItemAllReplyAttachment.Name = "MenuItemAllReplyAttachment";
            this.MenuItemAllReplyAttachment.Size = new System.Drawing.Size(199, 22);
            this.MenuItemAllReplyAttachment.Text = "Reply All(Attachment)";
            // 
            // MenuItemforwardin
            // 
            this.MenuItemforwardin.Image = global::ICP.Business.Common.UI.Properties.Resources.toolForward_Image;
            this.MenuItemforwardin.Name = "MenuItemforwardin";
            this.MenuItemforwardin.Size = new System.Drawing.Size(199, 22);
            this.MenuItemforwardin.Text = "Forward";
            // 
            // MenuItemMarkNRAS
            // 
            this.MenuItemMarkNRAS.Image = global::ICP.Business.Common.UI.Properties.Resources.Check_16;
            this.MenuItemMarkNRAS.Name = "MenuItemMarkNRAS";
            this.MenuItemMarkNRAS.Size = new System.Drawing.Size(199, 22);
            this.MenuItemMarkNRAS.Text = "Mark NRAS";
            this.MenuItemMarkNRAS.Visible = false;
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "CommunicationList";
            // 
            // gridViewList
            // 
            this.gridViewList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEntryID,
            this.colId,
            this.colSendTo,
            this.colSendFrom,
            this.colIschoose,
            this.colType,
            this.colContactStage,
            this.gridColumnPriority,
            this.gridColumnHasAttachment,
            this.colSubject,
            this.colSendFromName,
            this.colCreateDate,
            this.colStateDescription,
            this.colRemark});
            this.gridViewList.GridControl = this.gridControlList;
            this.gridViewList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewList.Images = this.imageListGridView;
            this.gridViewList.Name = "gridViewList";
            this.gridViewList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridViewList.OptionsDetail.AllowZoomDetail = false;
            this.gridViewList.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewList.OptionsDetail.ShowDetailTabs = false;
            this.gridViewList.OptionsDetail.SmartDetailExpand = false;
            this.gridViewList.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled;
            this.gridViewList.OptionsSelection.MultiSelect = true;
            this.gridViewList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewList.OptionsView.ColumnAutoWidth = false;
            this.gridViewList.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewList.OptionsView.ShowDetailButtons = false;
            this.gridViewList.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewList.OptionsView.ShowGroupPanel = false;
            this.gridViewList.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCreateDate, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridViewList.DoubleClick += new System.EventHandler(this.gridControlList_DoubleClick);
            // 
            // colEntryID
            // 
            this.colEntryID.Caption = "EntryID";
            this.colEntryID.FieldName = "EntryId";
            this.colEntryID.Name = "colEntryID";
            this.colEntryID.OptionsColumn.AllowEdit = false;
            this.colEntryID.OptionsColumn.ReadOnly = true;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            this.colId.OptionsColumn.ReadOnly = true;
            // 
            // colSendTo
            // 
            this.colSendTo.Caption = "Send To";
            this.colSendTo.FieldName = "SendTo";
            this.colSendTo.MinWidth = 160;
            this.colSendTo.Name = "colSendTo";
            this.colSendTo.OptionsColumn.AllowEdit = false;
            this.colSendTo.OptionsColumn.ReadOnly = true;
            this.colSendTo.Width = 160;
            // 
            // colSendFrom
            // 
            this.colSendFrom.Caption = "Send From";
            this.colSendFrom.FieldName = "FromName";
            this.colSendFrom.MinWidth = 160;
            this.colSendFrom.Name = "colSendFrom";
            this.colSendFrom.OptionsColumn.AllowEdit = false;
            this.colSendFrom.OptionsColumn.ReadOnly = true;
            this.colSendFrom.Width = 160;
            // 
            // colIschoose
            // 
            this.colIschoose.Caption = "Choose";
            this.colIschoose.FieldName = "IsChoose";
            this.colIschoose.Name = "colIschoose";
            this.colIschoose.Width = 60;
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colType.FieldName = "Type";
            this.colType.MaxWidth = 85;
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.OptionsColumn.ReadOnly = true;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 0;
            this.colType.Width = 51;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageList;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "email.gif");
            this.imageList.Images.SetKeyName(1, "fax.gif");
            this.imageList.Images.SetKeyName(2, "EDI.png");
            // 
            // colContactStage
            // 
            this.colContactStage.Caption = "ContactStage";
            this.colContactStage.FieldName = "ContactStage";
            this.colContactStage.MinWidth = 45;
            this.colContactStage.Name = "colContactStage";
            this.colContactStage.OptionsColumn.AllowEdit = false;
            this.colContactStage.OptionsColumn.ReadOnly = true;
            this.colContactStage.Visible = true;
            this.colContactStage.VisibleIndex = 1;
            this.colContactStage.Width = 45;
            // 
            // gridColumnPriority
            // 
            this.gridColumnPriority.Caption = " ";
            this.gridColumnPriority.ColumnEdit = this.repositoryItemImageComboBoxPriority;
            this.gridColumnPriority.FieldName = "Priority";
            this.gridColumnPriority.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.gridColumnPriority.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.gridColumnPriority.ImageIndex = 2;
            this.gridColumnPriority.MaxWidth = 30;
            this.gridColumnPriority.MinWidth = 30;
            this.gridColumnPriority.Name = "gridColumnPriority";
            this.gridColumnPriority.OptionsColumn.AllowEdit = false;
            this.gridColumnPriority.OptionsColumn.ReadOnly = true;
            this.gridColumnPriority.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnPriority.OptionsFilter.AllowFilter = false;
            this.gridColumnPriority.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnPriority.Visible = true;
            this.gridColumnPriority.VisibleIndex = 2;
            this.gridColumnPriority.Width = 30;
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
            this.repositoryItemImageComboBoxPriority.LargeImages = this.imageListPriority;
            this.repositoryItemImageComboBoxPriority.Name = "repositoryItemImageComboBoxPriority";
            this.repositoryItemImageComboBoxPriority.SmallImages = this.imageListPriority;
            // 
            // imageListPriority
            // 
            this.imageListPriority.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPriority.ImageStream")));
            this.imageListPriority.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPriority.Images.SetKeyName(0, "imp-low.gif");
            this.imageListPriority.Images.SetKeyName(1, "imp-high.gif");
            // 
            // gridColumnHasAttachment
            // 
            this.gridColumnHasAttachment.Caption = " ";
            this.gridColumnHasAttachment.ColumnEdit = this.repositoryItemImageComboBoxHasAttachment;
            this.gridColumnHasAttachment.FieldName = "HasAttachment";
            this.gridColumnHasAttachment.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.gridColumnHasAttachment.ImageIndex = 0;
            this.gridColumnHasAttachment.MaxWidth = 30;
            this.gridColumnHasAttachment.MinWidth = 30;
            this.gridColumnHasAttachment.Name = "gridColumnHasAttachment";
            this.gridColumnHasAttachment.OptionsColumn.AllowEdit = false;
            this.gridColumnHasAttachment.OptionsColumn.ReadOnly = true;
            this.gridColumnHasAttachment.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnHasAttachment.OptionsFilter.AllowFilter = false;
            this.gridColumnHasAttachment.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnHasAttachment.Visible = true;
            this.gridColumnHasAttachment.VisibleIndex = 3;
            this.gridColumnHasAttachment.Width = 30;
            // 
            // repositoryItemImageComboBoxHasAttachment
            // 
            this.repositoryItemImageComboBoxHasAttachment.AutoHeight = false;
            this.repositoryItemImageComboBoxHasAttachment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxHasAttachment.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, -1)});
            this.repositoryItemImageComboBoxHasAttachment.LargeImages = this.imageListHasAttachment;
            this.repositoryItemImageComboBoxHasAttachment.Name = "repositoryItemImageComboBoxHasAttachment";
            // 
            // imageListHasAttachment
            // 
            this.imageListHasAttachment.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHasAttachment.ImageStream")));
            this.imageListHasAttachment.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListHasAttachment.Images.SetKeyName(0, "attach.gif");
            // 
            // colSubject
            // 
            this.colSubject.Caption = "Subject";
            this.colSubject.FieldName = "Subject";
            this.colSubject.MinWidth = 600;
            this.colSubject.Name = "colSubject";
            this.colSubject.OptionsColumn.AllowEdit = false;
            this.colSubject.OptionsColumn.ReadOnly = true;
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 4;
            this.colSubject.Width = 600;
            // 
            // colSendFromName
            // 
            this.colSendFromName.Caption = "Send From";
            this.colSendFromName.FieldName = "FromName";
            this.colSendFromName.Name = "colSendFromName";
            this.colSendFromName.OptionsColumn.AllowEdit = false;
            this.colSendFromName.OptionsColumn.ReadOnly = true;
            this.colSendFromName.Visible = true;
            this.colSendFromName.VisibleIndex = 5;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "Send Time";
            this.colCreateDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colCreateDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateDate.FieldName = "SentDateTimeZone";
            this.colCreateDate.MaxWidth = 130;
            this.colCreateDate.MinWidth = 130;
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.OptionsColumn.ReadOnly = true;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 6;
            this.colCreateDate.Width = 130;
            // 
            // colStateDescription
            // 
            this.colStateDescription.Caption = "State";
            this.colStateDescription.FieldName = "StateDescription";
            this.colStateDescription.MaxWidth = 65;
            this.colStateDescription.MinWidth = 65;
            this.colStateDescription.Name = "colStateDescription";
            this.colStateDescription.OptionsColumn.AllowEdit = false;
            this.colStateDescription.OptionsColumn.ReadOnly = true;
            this.colStateDescription.Visible = true;
            this.colStateDescription.VisibleIndex = 7;
            this.colStateDescription.Width = 65;
            // 
            // imageListGridView
            // 
            this.imageListGridView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGridView.ImageStream")));
            this.imageListGridView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListGridView.Images.SetKeyName(0, "1.gif");
            this.imageListGridView.Images.SetKeyName(1, "2.gif");
            this.imageListGridView.Images.SetKeyName(2, "3.gif");
            // 
            // repositoryItemImageComboBoxWay
            // 
            this.repositoryItemImageComboBoxWay.AutoHeight = false;
            this.repositoryItemImageComboBoxWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxWay.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ICP.Message.ServiceInterface.MessageWay.Send, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ICP.Message.ServiceInterface.MessageWay.Receive, 1)});
            this.repositoryItemImageComboBoxWay.LargeImages = this.imageListWay;
            this.repositoryItemImageComboBoxWay.Name = "repositoryItemImageComboBoxWay";
            // 
            // imageListWay
            // 
            this.imageListWay.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListWay.ImageStream")));
            this.imageListWay.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListWay.Images.SetKeyName(0, "icon-msg-forward.gif");
            this.imageListWay.Images.SetKeyName(1, "icon-msg-reply.gif");
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 8;
            this.colRemark.Width = 222;
            // 
            // UCCommunicationHistoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlList);
            this.Name = "UCCommunicationHistoryList";
            this.Size = new System.Drawing.Size(586, 150);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxHasAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxWay)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraGrid.GridControl gridControlList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewList;
        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colSendTo;
        private DevExpress.XtraGrid.Columns.GridColumn colSendFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colStateDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colContactStage;
        private DevExpress.XtraGrid.Columns.GridColumn colEntryID;
        private System.Windows.Forms.ImageList imageListGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPriority;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxPriority;
        private System.Windows.Forms.ImageList imageListPriority;
        private System.Windows.Forms.ImageList imageListWay;
        private System.Windows.Forms.ImageList imageListHasAttachment;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxWay;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnHasAttachment;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxHasAttachment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuItemrepl;
        private System.Windows.Forms.ToolStripMenuItem MenuItemforwardin;
        private DevExpress.XtraGrid.Columns.GridColumn colSendFromName;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSend;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAllrepl;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAllReplyAttachment;
        private DevExpress.XtraGrid.Columns.GridColumn colIschoose;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMarkNRAS;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;

    }
}

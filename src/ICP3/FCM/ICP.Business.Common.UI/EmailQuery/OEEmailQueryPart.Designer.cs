using DevExpress.XtraGrid.Columns;
using ICP.Message.ServiceInterface;

namespace ICP.Business.Common.UI
{
    partial class OEEmailQueryPart
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OEEmailQueryPart));
            this.textEditCustomer = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEditArea = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditDateType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControlDate = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditMailType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEditMail = new DevExpress.XtraEditors.TextEdit();
            this.labelControlMail = new DevExpress.XtraEditors.LabelControl();
            this.labelControlcustomer = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditPhas = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControlPhas = new DevExpress.XtraEditors.LabelControl();
            this.labelControlType = new DevExpress.XtraEditors.LabelControl();
            this.textEditNowords = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxEditType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditNoType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControlNo = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditLocation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControlIn = new DevExpress.XtraEditors.LabelControl();
            this.textEditwords = new DevExpress.XtraEditors.TextEdit();
            this.labelControlWords = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonsimpleButtonnNewSearch = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonStop = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemrepl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAllrepl = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAllAttachment = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemforwardin = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColumnSendFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColumnContactStage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColumnPriority = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBoxPriority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListPriority = new System.Windows.Forms.ImageList(this.components);
            this.ColumnHasAttachment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBoxHasAttachment = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListHasAttachment = new System.Windows.Forms.ImageList(this.components);
            this.ColumnSendFromName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColumnSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColumnReceivingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColumnSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColumnEntryID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColumnMessageID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageListGridView = new System.Windows.Forms.ImageList(this.components);
            this.repositoryItemImageComboBoxWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListWay = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.GroupControlConditions = new DevExpress.XtraEditors.GroupControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxLoding = new System.Windows.Forms.PictureBox();
            this.simpleButtonFind = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMailType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPhas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNowords.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditNoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditwords.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxHasAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxWay)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControlConditions)).BeginInit();
            this.GroupControlConditions.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoding)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditCustomer
            // 
            this.textEditCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditCustomer.Location = new System.Drawing.Point(62, 172);
            this.textEditCustomer.Name = "textEditCustomer";
            this.textEditCustomer.Size = new System.Drawing.Size(630, 21);
            this.textEditCustomer.TabIndex = 19;
            // 
            // comboBoxEditArea
            // 
            this.comboBoxEditArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditArea.Location = new System.Drawing.Point(358, 238);
            this.comboBoxEditArea.Name = "comboBoxEditArea";
            this.comboBoxEditArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditArea.Size = new System.Drawing.Size(334, 21);
            this.comboBoxEditArea.TabIndex = 18;
            // 
            // comboBoxEditDateType
            // 
            this.comboBoxEditDateType.Location = new System.Drawing.Point(62, 238);
            this.comboBoxEditDateType.Name = "comboBoxEditDateType";
            this.comboBoxEditDateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditDateType.Size = new System.Drawing.Size(290, 21);
            this.comboBoxEditDateType.TabIndex = 17;
            this.comboBoxEditDateType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditDateType_SelectedIndexChanged);
            // 
            // labelControlDate
            // 
            this.labelControlDate.Location = new System.Drawing.Point(8, 241);
            this.labelControlDate.Name = "labelControlDate";
            this.labelControlDate.Size = new System.Drawing.Size(24, 14);
            this.labelControlDate.TabIndex = 16;
            this.labelControlDate.Text = "时间";
            // 
            // comboBoxEditMailType
            // 
            this.comboBoxEditMailType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditMailType.Location = new System.Drawing.Point(358, 210);
            this.comboBoxEditMailType.Name = "comboBoxEditMailType";
            this.comboBoxEditMailType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditMailType.Size = new System.Drawing.Size(334, 21);
            this.comboBoxEditMailType.TabIndex = 15;
            // 
            // textEditMail
            // 
            this.textEditMail.Location = new System.Drawing.Point(62, 210);
            this.textEditMail.Name = "textEditMail";
            this.textEditMail.Size = new System.Drawing.Size(290, 21);
            this.textEditMail.TabIndex = 14;
            // 
            // labelControlMail
            // 
            this.labelControlMail.Location = new System.Drawing.Point(8, 214);
            this.labelControlMail.Name = "labelControlMail";
            this.labelControlMail.Size = new System.Drawing.Size(24, 14);
            this.labelControlMail.TabIndex = 13;
            this.labelControlMail.Text = "邮箱";
            // 
            // labelControlcustomer
            // 
            this.labelControlcustomer.Location = new System.Drawing.Point(8, 175);
            this.labelControlcustomer.Name = "labelControlcustomer";
            this.labelControlcustomer.Size = new System.Drawing.Size(24, 14);
            this.labelControlcustomer.TabIndex = 11;
            this.labelControlcustomer.Text = "客户";
            // 
            // comboBoxEditPhas
            // 
            this.comboBoxEditPhas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditPhas.Location = new System.Drawing.Point(62, 145);
            this.comboBoxEditPhas.Name = "comboBoxEditPhas";
            this.comboBoxEditPhas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditPhas.Size = new System.Drawing.Size(630, 21);
            this.comboBoxEditPhas.TabIndex = 10;
            // 
            // labelControlPhas
            // 
            this.labelControlPhas.Location = new System.Drawing.Point(8, 148);
            this.labelControlPhas.Name = "labelControlPhas";
            this.labelControlPhas.Size = new System.Drawing.Size(24, 14);
            this.labelControlPhas.TabIndex = 9;
            this.labelControlPhas.Text = "阶段";
            // 
            // labelControlType
            // 
            this.labelControlType.Location = new System.Drawing.Point(8, 122);
            this.labelControlType.Name = "labelControlType";
            this.labelControlType.Size = new System.Drawing.Size(24, 14);
            this.labelControlType.TabIndex = 7;
            this.labelControlType.Text = "分类";
            // 
            // textEditNowords
            // 
            this.textEditNowords.Location = new System.Drawing.Point(62, 91);
            this.textEditNowords.Name = "textEditNowords";
            this.textEditNowords.Size = new System.Drawing.Size(290, 21);
            this.textEditNowords.TabIndex = 5;
            // 
            // comboBoxEditType
            // 
            this.comboBoxEditType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditType.Location = new System.Drawing.Point(62, 119);
            this.comboBoxEditType.Name = "comboBoxEditType";
            this.comboBoxEditType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditType.Size = new System.Drawing.Size(630, 21);
            this.comboBoxEditType.TabIndex = 8;
            // 
            // comboBoxEditNoType
            // 
            this.comboBoxEditNoType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditNoType.Location = new System.Drawing.Point(358, 91);
            this.comboBoxEditNoType.Name = "comboBoxEditNoType";
            this.comboBoxEditNoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditNoType.Size = new System.Drawing.Size(334, 21);
            this.comboBoxEditNoType.TabIndex = 6;
            // 
            // labelControlNo
            // 
            this.labelControlNo.Location = new System.Drawing.Point(8, 94);
            this.labelControlNo.Name = "labelControlNo";
            this.labelControlNo.Size = new System.Drawing.Size(24, 14);
            this.labelControlNo.TabIndex = 4;
            this.labelControlNo.Text = "单号";
            // 
            // comboBoxEditLocation
            // 
            this.comboBoxEditLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditLocation.EditValue = "";
            this.comboBoxEditLocation.Location = new System.Drawing.Point(62, 53);
            this.comboBoxEditLocation.Name = "comboBoxEditLocation";
            this.comboBoxEditLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditLocation.Size = new System.Drawing.Size(630, 21);
            this.comboBoxEditLocation.TabIndex = 3;
            // 
            // labelControlIn
            // 
            this.labelControlIn.Location = new System.Drawing.Point(8, 56);
            this.labelControlIn.Name = "labelControlIn";
            this.labelControlIn.Size = new System.Drawing.Size(24, 14);
            this.labelControlIn.TabIndex = 2;
            this.labelControlIn.Text = "位置";
            // 
            // textEditwords
            // 
            this.textEditwords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditwords.Location = new System.Drawing.Point(62, 26);
            this.textEditwords.Name = "textEditwords";
            this.textEditwords.Size = new System.Drawing.Size(630, 21);
            this.textEditwords.TabIndex = 1;
            // 
            // labelControlWords
            // 
            this.labelControlWords.Location = new System.Drawing.Point(8, 28);
            this.labelControlWords.Name = "labelControlWords";
            this.labelControlWords.Size = new System.Drawing.Size(48, 14);
            this.labelControlWords.TabIndex = 0;
            this.labelControlWords.Text = "查找文字";
            // 
            // simpleButtonsimpleButtonnNewSearch
            // 
            this.simpleButtonsimpleButtonnNewSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.simpleButtonsimpleButtonnNewSearch.Location = new System.Drawing.Point(25, 128);
            this.simpleButtonsimpleButtonnNewSearch.Name = "simpleButtonsimpleButtonnNewSearch";
            this.simpleButtonsimpleButtonnNewSearch.Size = new System.Drawing.Size(93, 27);
            this.simpleButtonsimpleButtonnNewSearch.TabIndex = 4;
            this.simpleButtonsimpleButtonnNewSearch.Text = "新搜索";
            this.simpleButtonsimpleButtonnNewSearch.Click += new System.EventHandler(this.simpleButtonsimpleButtonnNewSearch_Click);
            // 
            // simpleButtonStop
            // 
            this.simpleButtonStop.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.simpleButtonStop.Location = new System.Drawing.Point(25, 94);
            this.simpleButtonStop.Name = "simpleButtonStop";
            this.simpleButtonStop.Size = new System.Drawing.Size(93, 27);
            this.simpleButtonStop.TabIndex = 3;
            this.simpleButtonStop.Text = "停止";
            this.simpleButtonStop.Click += new System.EventHandler(this.simpleButtonStop_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl);
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(830, 550);
            this.panelControl1.TabIndex = 1;
            // 
            // gridControl
            // 
            this.gridControl.ContextMenuStrip = this.contextMenuStrip;
            this.gridControl.DataSource = this.bindingSource;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(2, 270);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBoxPriority,
            this.repositoryItemImageComboBoxWay,
            this.repositoryItemImageComboBoxHasAttachment});
            this.gridControl.Size = new System.Drawing.Size(826, 278);
            this.gridControl.TabIndex = 5;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemOpen,
            this.MenuItemrepl,
            this.MenuItemAllrepl,
            this.MenuItemAllAttachment,
            this.MenuItemforwardin});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(169, 114);
            // 
            // MenuItemOpen
            // 
            this.MenuItemOpen.Name = "MenuItemOpen";
            this.MenuItemOpen.Size = new System.Drawing.Size(168, 22);
            this.MenuItemOpen.Text = "打开";
            this.MenuItemOpen.Click += new System.EventHandler(this.MenuItemOpen_Click);
            // 
            // MenuItemrepl
            // 
            this.MenuItemrepl.Image = global::ICP.Business.Common.UI.Properties.Resources.toolReply_Image;
            this.MenuItemrepl.Name = "MenuItemrepl";
            this.MenuItemrepl.Size = new System.Drawing.Size(168, 22);
            this.MenuItemrepl.Text = "答复";
            this.MenuItemrepl.Click += new System.EventHandler(this.MenuItemrepl_Click);
            // 
            // MenuItemAllrepl
            // 
            this.MenuItemAllrepl.Image = global::ICP.Business.Common.UI.Properties.Resources.toolReplyAll_Image;
            this.MenuItemAllrepl.Name = "MenuItemAllrepl";
            this.MenuItemAllrepl.Size = new System.Drawing.Size(168, 22);
            this.MenuItemAllrepl.Text = "全部答复";
            this.MenuItemAllrepl.Click += new System.EventHandler(this.MenuItemAllrepl_Click);
            // 
            // MenuItemAllAttachment
            // 
            this.MenuItemAllAttachment.Image = global::ICP.Business.Common.UI.Properties.Resources.toolReplyAll_Image;
            this.MenuItemAllAttachment.Name = "MenuItemAllAttachment";
            this.MenuItemAllAttachment.Size = new System.Drawing.Size(168, 22);
            this.MenuItemAllAttachment.Text = "全部答复(含附件)";
            this.MenuItemAllAttachment.Click += new System.EventHandler(this.MenuItemAllAttachment_Click);
            // 
            // MenuItemforwardin
            // 
            this.MenuItemforwardin.Image = global::ICP.Business.Common.UI.Properties.Resources.toolForward_Image;
            this.MenuItemforwardin.Name = "MenuItemforwardin";
            this.MenuItemforwardin.Size = new System.Drawing.Size(168, 22);
            this.MenuItemforwardin.Text = "转发";
            this.MenuItemforwardin.Click += new System.EventHandler(this.MenuItemforwardin_Click);
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColumnSendFrom,
            this.ColumnContactStage,
            this.ColumnPriority,
            this.ColumnHasAttachment,
            this.ColumnSendFromName,
            this.ColumnSubject,
            this.ColumnReceivingTime,
            this.ColumnSize,
            this.ColumnEntryID,
            this.ColumnMessageID});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Images = this.imageListGridView;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView.OptionsView.ShowDetailButtons = false;
            this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
            // 
            // ColumnSendFrom
            // 
            this.ColumnSendFrom.Caption = "发件人";
            this.ColumnSendFrom.FieldName = "SendFrom";
            this.ColumnSendFrom.Name = "ColumnSendFrom";
            this.ColumnSendFrom.OptionsFilter.AllowAutoFilter = false;
            this.ColumnSendFrom.OptionsFilter.AllowFilter = false;
            this.ColumnSendFrom.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.ColumnSendFrom.Width = 150;
            // 
            // ColumnContactStage
            // 
            this.ColumnContactStage.Caption = "阶段";
            this.ColumnContactStage.FieldName = "ContactStage";
            this.ColumnContactStage.MinWidth = 45;
            this.ColumnContactStage.Name = "ColumnContactStage";
            this.ColumnContactStage.Visible = true;
            this.ColumnContactStage.VisibleIndex = 0;
            this.ColumnContactStage.Width = 45;
            // 
            // ColumnPriority
            // 
            this.ColumnPriority.Caption = " ";
            this.ColumnPriority.ColumnEdit = this.repositoryItemImageComboBoxPriority;
            this.ColumnPriority.FieldName = "Priority";
            this.ColumnPriority.ImageIndex = 2;
            this.ColumnPriority.MaxWidth = 30;
            this.ColumnPriority.Name = "ColumnPriority";
            this.ColumnPriority.OptionsFilter.AllowAutoFilter = false;
            this.ColumnPriority.OptionsFilter.AllowFilter = false;
            this.ColumnPriority.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.ColumnPriority.Visible = true;
            this.ColumnPriority.VisibleIndex = 1;
            this.ColumnPriority.Width = 30;
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
            // ColumnHasAttachment
            // 
            this.ColumnHasAttachment.Caption = " ";
            this.ColumnHasAttachment.ColumnEdit = this.repositoryItemImageComboBoxHasAttachment;
            this.ColumnHasAttachment.FieldName = "HasAttachment";
            this.ColumnHasAttachment.ImageIndex = 0;
            this.ColumnHasAttachment.MaxWidth = 30;
            this.ColumnHasAttachment.Name = "ColumnHasAttachment";
            this.ColumnHasAttachment.OptionsFilter.AllowAutoFilter = false;
            this.ColumnHasAttachment.OptionsFilter.AllowFilter = false;
            this.ColumnHasAttachment.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.ColumnHasAttachment.Visible = true;
            this.ColumnHasAttachment.VisibleIndex = 2;
            this.ColumnHasAttachment.Width = 30;
            // 
            // repositoryItemImageComboBoxHasAttachment
            // 
            this.repositoryItemImageComboBoxHasAttachment.AutoHeight = false;
            this.repositoryItemImageComboBoxHasAttachment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxHasAttachment.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0)});
            this.repositoryItemImageComboBoxHasAttachment.LargeImages = this.imageListHasAttachment;
            this.repositoryItemImageComboBoxHasAttachment.Name = "repositoryItemImageComboBoxHasAttachment";
            this.repositoryItemImageComboBoxHasAttachment.SmallImages = this.imageListHasAttachment;
            // 
            // imageListHasAttachment
            // 
            this.imageListHasAttachment.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHasAttachment.ImageStream")));
            this.imageListHasAttachment.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListHasAttachment.Images.SetKeyName(0, "attach.gif");
            // 
            // ColumnSendFromName
            // 
            this.ColumnSendFromName.Caption = "发件人";
            this.ColumnSendFromName.FieldName = "FromName";
            this.ColumnSendFromName.Name = "ColumnSendFromName";
            this.ColumnSendFromName.Visible = true;
            this.ColumnSendFromName.VisibleIndex = 3;
            this.ColumnSendFromName.Width = 100;
            // 
            // ColumnSubject
            // 
            this.ColumnSubject.Caption = "主题";
            this.ColumnSubject.FieldName = "Subject";
            this.ColumnSubject.Name = "ColumnSubject";
            this.ColumnSubject.OptionsFilter.AllowAutoFilter = false;
            this.ColumnSubject.OptionsFilter.AllowFilter = false;
            this.ColumnSubject.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.ColumnSubject.Visible = true;
            this.ColumnSubject.VisibleIndex = 4;
            this.ColumnSubject.Width = 400;
            // 
            // ColumnReceivingTime
            // 
            this.ColumnReceivingTime.Caption = "接收时间";
            this.ColumnReceivingTime.DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
            this.ColumnReceivingTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ColumnReceivingTime.FieldName = "ReceivingTime";
            this.ColumnReceivingTime.Name = "ColumnReceivingTime";
            this.ColumnReceivingTime.OptionsFilter.AllowAutoFilter = false;
            this.ColumnReceivingTime.OptionsFilter.AllowFilter = false;
            this.ColumnReceivingTime.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.ColumnReceivingTime.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.ColumnReceivingTime.Visible = true;
            this.ColumnReceivingTime.VisibleIndex = 5;
            this.ColumnReceivingTime.Width = 150;
            // 
            // ColumnSize
            // 
            this.ColumnSize.Caption = "大小";
            this.ColumnSize.FieldName = "Size";
            this.ColumnSize.Name = "ColumnSize";
            this.ColumnSize.OptionsFilter.AllowAutoFilter = false;
            this.ColumnSize.OptionsFilter.AllowFilter = false;
            this.ColumnSize.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.ColumnSize.Visible = true;
            this.ColumnSize.VisibleIndex = 6;
            this.ColumnSize.Width = 50;
            // 
            // ColumnEntryID
            // 
            this.ColumnEntryID.Caption = "EntryID";
            this.ColumnEntryID.FieldName = "EntryID";
            this.ColumnEntryID.Name = "ColumnEntryID";
            // 
            // ColumnMessageID
            // 
            this.ColumnMessageID.Caption = "MessageID";
            this.ColumnMessageID.FieldName = "MessageID";
            this.ColumnMessageID.Name = "ColumnMessageID";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.GroupControlConditions);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 268);
            this.panel1.TabIndex = 8;
            // 
            // GroupControlConditions
            // 
            this.GroupControlConditions.Controls.Add(this.label2);
            this.GroupControlConditions.Controls.Add(this.label1);
            this.GroupControlConditions.Controls.Add(this.textEditCustomer);
            this.GroupControlConditions.Controls.Add(this.comboBoxEditArea);
            this.GroupControlConditions.Controls.Add(this.comboBoxEditDateType);
            this.GroupControlConditions.Controls.Add(this.labelControlDate);
            this.GroupControlConditions.Controls.Add(this.comboBoxEditMailType);
            this.GroupControlConditions.Controls.Add(this.textEditMail);
            this.GroupControlConditions.Controls.Add(this.labelControlMail);
            this.GroupControlConditions.Controls.Add(this.labelControlcustomer);
            this.GroupControlConditions.Controls.Add(this.comboBoxEditPhas);
            this.GroupControlConditions.Controls.Add(this.labelControlPhas);
            this.GroupControlConditions.Controls.Add(this.comboBoxEditType);
            this.GroupControlConditions.Controls.Add(this.labelControlType);
            this.GroupControlConditions.Controls.Add(this.comboBoxEditNoType);
            this.GroupControlConditions.Controls.Add(this.textEditNowords);
            this.GroupControlConditions.Controls.Add(this.labelControlNo);
            this.GroupControlConditions.Controls.Add(this.comboBoxEditLocation);
            this.GroupControlConditions.Controls.Add(this.labelControlIn);
            this.GroupControlConditions.Controls.Add(this.textEditwords);
            this.GroupControlConditions.Controls.Add(this.labelControlWords);
            this.GroupControlConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupControlConditions.Location = new System.Drawing.Point(0, 0);
            this.GroupControlConditions.Name = "GroupControlConditions";
            this.GroupControlConditions.Size = new System.Drawing.Size(698, 268);
            this.GroupControlConditions.TabIndex = 0;
            this.GroupControlConditions.Text = "搜索条件";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(691, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------------------------------------" +
    "----------";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(691, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------------------------------------" +
    "----------";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.simpleButtonStop);
            this.panel2.Controls.Add(this.pictureBoxLoding);
            this.panel2.Controls.Add(this.simpleButtonFind);
            this.panel2.Controls.Add(this.simpleButtonsimpleButtonnNewSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(698, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(128, 268);
            this.panel2.TabIndex = 1;
            // 
            // pictureBoxLoding
            // 
            this.pictureBoxLoding.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBoxLoding.Image = global::ICP.Business.Common.UI.Properties.Resources.Loding;
            this.pictureBoxLoding.Location = new System.Drawing.Point(48, 175);
            this.pictureBoxLoding.Name = "pictureBoxLoding";
            this.pictureBoxLoding.Size = new System.Drawing.Size(41, 42);
            this.pictureBoxLoding.TabIndex = 7;
            this.pictureBoxLoding.TabStop = false;
            this.pictureBoxLoding.Visible = false;
            // 
            // simpleButtonFind
            // 
            this.simpleButtonFind.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.simpleButtonFind.Location = new System.Drawing.Point(25, 60);
            this.simpleButtonFind.Name = "simpleButtonFind";
            this.simpleButtonFind.Size = new System.Drawing.Size(93, 27);
            this.simpleButtonFind.TabIndex = 2;
            this.simpleButtonFind.Text = "立即查找";
            this.simpleButtonFind.Click += new System.EventHandler(this.simpleButtonFind_Click);
            // 
            // OEEmailQueryPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 576);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OEEmailQueryPart";
            this.Text = "OEEmailQueryPart";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.OEEmailQueryPart_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OEEmailQueryPart_KeyDown);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.textEditCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMailType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPhas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNowords.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditNoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditwords.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxHasAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxWay)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupControlConditions)).EndInit();
            this.GroupControlConditions.ResumeLayout(false);
            this.GroupControlConditions.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEditCustomer;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditArea;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDateType;
        private DevExpress.XtraEditors.LabelControl labelControlDate;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditMailType;
        private DevExpress.XtraEditors.TextEdit textEditMail;
        private DevExpress.XtraEditors.LabelControl labelControlMail;
        private DevExpress.XtraEditors.LabelControl labelControlcustomer;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPhas;
        private DevExpress.XtraEditors.LabelControl labelControlPhas;
        private DevExpress.XtraEditors.LabelControl labelControlType;
        private DevExpress.XtraEditors.TextEdit textEditNowords;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditType;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditNoType;
        private DevExpress.XtraEditors.LabelControl labelControlNo;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLocation;
        private DevExpress.XtraEditors.LabelControl labelControlIn;
        private DevExpress.XtraEditors.TextEdit textEditwords;
        private DevExpress.XtraEditors.LabelControl labelControlWords;
        private DevExpress.XtraEditors.SimpleButton simpleButtonsimpleButtonnNewSearch;
        private DevExpress.XtraEditors.SimpleButton simpleButtonStop;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonFind;
        private DevExpress.XtraEditors.GroupControl GroupControlConditions;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn ColumnSubject;
        private DevExpress.XtraGrid.Columns.GridColumn ColumnSendFrom;
        private DevExpress.XtraGrid.Columns.GridColumn ColumnReceivingTime;
        private DevExpress.XtraGrid.Columns.GridColumn ColumnEntryID;
        private DevExpress.XtraGrid.Columns.GridColumn MessageID;
        private System.Windows.Forms.PictureBox pictureBoxLoding;
        private GridColumn ColumnMessageID;
        private GridColumn ColumnSize;
        private GridColumn ColumnPriority;
        private GridColumn ColumnHasAttachment;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxPriority;
        private System.Windows.Forms.ImageList imageListPriority;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxWay;
        private System.Windows.Forms.ImageList imageListWay;
        private System.Windows.Forms.ImageList imageListHasAttachment;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxHasAttachment;
        private System.Windows.Forms.ImageList imageListGridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuItemrepl;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAllrepl;
        private System.Windows.Forms.ToolStripMenuItem MenuItemforwardin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private GridColumn ColumnContactStage;
        private GridColumn ColumnSendFromName;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAllAttachment;
    }
}
namespace ICP.FAM.UI.AccReceControl
{
    partial class AccControlEdit
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
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.bsAgingLog = new System.Windows.Forms.BindingSource(this.components);
            this.labDescription = new DevExpress.XtraEditors.LabelControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.linkCustomerName = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labTemplate = new DevExpress.XtraEditors.LabelControl();
            this.cmbSubjectMod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barUpFiles = new DevExpress.XtraBars.BarButtonItem();
            this.barDelFile = new DevExpress.XtraBars.BarButtonItem();
            this.barDown = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labCaution = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.radiosCustomer = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAgingLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSubjectMod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiosCustomer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAgingLog, "Content", true));
            this.txtDescription.Location = new System.Drawing.Point(378, 25);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtDescription.Properties.Appearance.Options.UseBackColor = true;
            this.txtDescription.Size = new System.Drawing.Size(392, 197);
            this.txtDescription.TabIndex = 2;
            // 
            // bsAgingLog
            // 
            this.bsAgingLog.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CustomerAgingLogs);
            // 
            // labDescription
            // 
            this.labDescription.Location = new System.Drawing.Point(378, 3);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(60, 14);
            this.labDescription.TabIndex = 1;
            this.labDescription.Text = "Description";
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
            this.gcMain.Location = new System.Drawing.Point(0, 258);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.linkCustomerName});
            this.gcMain.Size = new System.Drawing.Size(790, 245);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gcMain_MouseDoubleClick);
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CustomerAgingLogAtts);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCreateBy,
            this.colFileName,
            this.colCreateOn});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colCreateBy
            // 
            this.colCreateBy.Caption = "CreateBy";
            this.colCreateBy.FieldName = "CreateByName";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 1;
            this.colCreateBy.Width = 83;
            // 
            // colFileName
            // 
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 0;
            this.colFileName.Width = 275;
            // 
            // colCreateOn
            // 
            this.colCreateOn.Caption = "CreateOn";
            this.colCreateOn.DisplayFormat.FormatString = "d";
            this.colCreateOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateOn.FieldName = "CreateOn";
            this.colCreateOn.Name = "colCreateOn";
            this.colCreateOn.Visible = true;
            this.colCreateOn.VisibleIndex = 2;
            this.colCreateOn.Width = 110;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // linkCustomerName
            // 
            this.linkCustomerName.AutoHeight = false;
            this.linkCustomerName.Name = "linkCustomerName";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labTemplate);
            this.panel1.Controls.Add(this.cmbSubjectMod);
            this.panel1.Controls.Add(this.txtSubject);
            this.panel1.Controls.Add(this.labCaution);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.labDescription);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 232);
            this.panel1.TabIndex = 5;
            // 
            // labTemplate
            // 
            this.labTemplate.Location = new System.Drawing.Point(3, 184);
            this.labTemplate.Name = "labTemplate";
            this.labTemplate.Size = new System.Drawing.Size(52, 14);
            this.labTemplate.TabIndex = 839;
            this.labTemplate.Text = "Template";
            // 
            // cmbSubjectMod
            // 
            this.cmbSubjectMod.Location = new System.Drawing.Point(56, 179);
            this.cmbSubjectMod.Margin = new System.Windows.Forms.Padding(0);
            this.cmbSubjectMod.Name = "cmbSubjectMod";
            this.cmbSubjectMod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSubjectMod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSubjectMod.Size = new System.Drawing.Size(316, 21);
            this.cmbSubjectMod.TabIndex = 838;
            this.cmbSubjectMod.SelectedIndexChanged += new System.EventHandler(this.cmbSubjectMod_SelectedIndexChanged);
            // 
            // txtSubject
            // 
            this.txtSubject.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAgingLog, "Subject", true));
            this.txtSubject.Location = new System.Drawing.Point(56, 201);
            this.txtSubject.MenuManager = this.barManager1;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtSubject.Properties.Appearance.Options.UseBackColor = true;
            this.txtSubject.Size = new System.Drawing.Size(316, 21);
            this.txtSubject.TabIndex = 1;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barUpFiles,
            this.barDelFile,
            this.barDown});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUpFiles),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDown)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "Save";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barUpFiles
            // 
            this.barUpFiles.Caption = "UpLoadFiles";
            this.barUpFiles.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barUpFiles.Id = 1;
            this.barUpFiles.Name = "barUpFiles";
            this.barUpFiles.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barUpFiles.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUpFiles_ItemClick);
            // 
            // barDelFile
            // 
            this.barDelFile.Caption = "DeleteFile";
            this.barDelFile.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barDelFile.Id = 2;
            this.barDelFile.Name = "barDelFile";
            this.barDelFile.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDelFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelFile_ItemClick);
            // 
            // barDown
            // 
            this.barDown.Caption = "DownLoadFile";
            this.barDown.Glyph = global::ICP.FAM.UI.Properties.Resources.Down_16;
            this.barDown.Id = 3;
            this.barDown.Name = "barDown";
            this.barDown.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDown_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(790, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 503);
            this.barDockControlBottom.Size = new System.Drawing.Size(790, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 477);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(790, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 477);
            // 
            // labCaution
            // 
            this.labCaution.Location = new System.Drawing.Point(11, 204);
            this.labCaution.Name = "labCaution";
            this.labCaution.Size = new System.Drawing.Size(42, 14);
            this.labCaution.TabIndex = 3;
            this.labCaution.Text = "Subject";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textEdit2);
            this.groupBox1.Controls.Add(this.textEdit1);
            this.groupBox1.Controls.Add(this.radiosCustomer);
            this.groupBox1.Location = new System.Drawing.Point(3, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 158);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer";
            // 
            // textEdit2
            // 
            this.textEdit2.EditValue = "Past Due is Over 46+ Days!";
            this.textEdit2.Location = new System.Drawing.Point(133, 117);
            this.textEdit2.MenuManager = this.barManager1;
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.textEdit2.Properties.Appearance.Options.UseForeColor = true;
            this.textEdit2.Properties.ReadOnly = true;
            this.textEdit2.Size = new System.Drawing.Size(216, 21);
            this.textEdit2.TabIndex = 7;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "Past Due is Over 30-45 Days";
            this.textEdit1.Location = new System.Drawing.Point(133, 77);
            this.textEdit1.MenuManager = this.barManager1;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(216, 21);
            this.textEdit1.TabIndex = 6;
            // 
            // radiosCustomer
            // 
            this.radiosCustomer.Location = new System.Drawing.Point(6, 21);
            this.radiosCustomer.MenuManager = this.barManager1;
            this.radiosCustomer.Name = "radiosCustomer";
            this.radiosCustomer.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.radiosCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.radiosCustomer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radiosCustomer.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0D, "Regular"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1D, "Yellow Reminder"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2D, "Red Reminder")});
            this.radiosCustomer.Size = new System.Drawing.Size(121, 131);
            this.radiosCustomer.TabIndex = 0;
            // 
            // AccControlEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "AccControlEdit";
            this.Size = new System.Drawing.Size(790, 503);
            this.Closing += new System.EventHandler<System.Windows.Forms.FormClosingEventArgs>(this.AccControlEdit_Closing);
            this.Load += new System.EventHandler(this.AccControlEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAgingLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSubjectMod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiosCustomer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labDescription;
        private DevExpress.XtraGrid.GridControl gcMain;
        private Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkCustomerName;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateOn;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.RadioGroup radiosCustomer;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barUpFiles;
        private DevExpress.XtraBars.BarButtonItem barDelFile;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl labCaution;
        private System.Windows.Forms.BindingSource bsAgingLog;
        private DevExpress.XtraBars.BarButtonItem barDown;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSubjectMod;
        private DevExpress.XtraEditors.LabelControl labTemplate;
    }
}

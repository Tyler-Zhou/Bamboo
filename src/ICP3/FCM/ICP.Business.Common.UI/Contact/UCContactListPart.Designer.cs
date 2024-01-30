using DevExpress.XtraBars;

namespace ICP.Business.Common.UI.Contact
{
    partial class UCContactListPart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barNewCustomer = new DevExpress.XtraBars.BarButtonItem();
            this.barNewStaff = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarSubItem();
            this.barRemoveCustomer = new DevExpress.XtraBars.BarButtonItem();
            this.barRemoveStaff = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barStage = new DevExpress.XtraBars.BarEditItem();
            this.reSelectBox = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnlStaff = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSelectBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStaff)).BeginInit();
            this.pnlStaff.SuspendLayout();
            this.SuspendLayout();
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
            this.barRemove,
            this.barSave,
            this.barRemoveCustomer,
            this.barNewStaff,
            this.barRemoveStaff,
            this.barNewCustomer,
            this.barStage});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 15;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reSelectBox});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewCustomer, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewStaff, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.barStage, "", false, true, true, 69, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barNewCustomer
            // 
            this.barNewCustomer.Caption = "Add Customer";
            this.barNewCustomer.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barNewCustomer.Id = 11;
            this.barNewCustomer.Name = "barNewCustomer";
            this.barNewCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewCustomer_ItemClick);
            // 
            // barNewStaff
            // 
            this.barNewStaff.Caption = "Add Assistant";
            this.barNewStaff.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barNewStaff.GlyphDisabled = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barNewStaff.Id = 13;
            this.barNewStaff.Name = "barNewStaff";
            this.barNewStaff.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewStaff_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "Remove";
            this.barRemove.Enabled = false;
            this.barRemove.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 3;
            this.barRemove.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barRemoveCustomer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRemoveStaff)});
            this.barRemove.Name = "barRemove";
            // 
            // barRemoveCustomer
            // 
            this.barRemoveCustomer.Caption = "Customer";
            this.barRemoveCustomer.Id = 9;
            this.barRemoveCustomer.Name = "barRemoveCustomer";
            this.barRemoveCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemoveCustomer_ItemClick);
            // 
            // barRemoveStaff
            // 
            this.barRemoveStaff.Caption = "Assistant";
            this.barRemoveStaff.Id = 112;
            this.barRemoveStaff.Name = "barRemoveStaff";
            this.barRemoveStaff.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemoveStaff_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "Save";
            this.barSave.Enabled = false;
            this.barSave.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Save;
            this.barSave.Id = 4;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barStage
            // 
            this.barStage.Caption = "Stage";
            this.barStage.Edit = this.reSelectBox;
            this.barStage.Id = 14;
            this.barStage.Name = "barStage";
            this.barStage.EditValueChanged += new System.EventHandler(this.barEditItem1_EditValueChanged);
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.reSelectBox.AutoHeight = false;
            this.reSelectBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.reSelectBox.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(754, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 306);
            this.barDockControlBottom.Size = new System.Drawing.Size(754, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 280);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(754, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 280);
            // 
            // pnlStaff
            // 
            this.pnlStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStaff.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.pnlStaff.Location = new System.Drawing.Point(0, 26);
            this.pnlStaff.Name = "pnlStaff";
            this.pnlStaff.Panel1.Text = "Panel1";
            this.pnlStaff.Panel2.MinSize = 200;
            this.pnlStaff.Panel2.Text = "Panel2";
            this.pnlStaff.Size = new System.Drawing.Size(754, 280);
            this.pnlStaff.SplitterPosition = 349;
            this.pnlStaff.TabIndex = 19;
            this.pnlStaff.Text = "splitContainerControl1";
            // 
            // UCContactListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.pnlStaff);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Name = "UCContactListPart";
            this.Size = new System.Drawing.Size(754, 306);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSelectBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStaff)).EndInit();
            this.pnlStaff.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barRemove;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraEditors.SplitContainerControl pnlStaff;
        private DevExpress.XtraBars.BarButtonItem barRemoveCustomer;
        private DevExpress.XtraBars.BarButtonItem barRemoveStaff;
        private DevExpress.XtraBars.BarButtonItem barNewCustomer;
        private DevExpress.XtraBars.BarButtonItem barNewStaff;
        private BarEditItem barStage;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit reSelectBox;
    }
}

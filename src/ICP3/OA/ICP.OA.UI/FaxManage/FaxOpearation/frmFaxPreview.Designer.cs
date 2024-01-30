namespace ICP.OA.UI.FaxManage
{
    partial class frmFaxPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFaxPreview));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barRevert = new DevExpress.XtraBars.BarButtonItem();
            this.barRevertAll = new DevExpress.XtraBars.BarButtonItem();
            this.barTransfer = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barOpeanFile = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveAll = new DevExpress.XtraBars.BarButtonItem();
            this.imageListAttachment = new System.Windows.Forms.ImageList(this.components);
            this.pnlContent = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContent)).BeginInit();
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
            this.barRevert,
            this.barRevertAll,
            this.barDelete,
            this.barClose,
            this.barTransfer,
            this.barOpeanFile,
            this.barSaveAs,
            this.barSaveAll});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRevert, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRevertAll, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barTransfer, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            this.bar2.Visible = false;
            // 
            // barRevert
            // 
            this.barRevert.Caption = "&Revert";
            this.barRevert.Enabled = false;
            this.barRevert.Glyph = global::ICP.OA.UI.Properties.Resources.R;
            this.barRevert.Id = 0;
            this.barRevert.Name = "barRevert";
            this.barRevert.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRevert_ItemClick);
            // 
            // barRevertAll
            // 
            this.barRevertAll.Caption = "Revert&All";
            this.barRevertAll.Enabled = false;
            this.barRevertAll.Glyph = global::ICP.OA.UI.Properties.Resources.R_All;
            this.barRevertAll.Id = 1;
            this.barRevertAll.Name = "barRevertAll";
            this.barRevertAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRevertAll_ItemClick);
            // 
            // barTransfer
            // 
            this.barTransfer.Caption = "&Transfer";
            this.barTransfer.Enabled = false;
            this.barTransfer.Glyph = global::ICP.OA.UI.Properties.Resources.Ts;
            this.barTransfer.Id = 6;
            this.barTransfer.Name = "barTransfer";
            this.barTransfer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTransfer_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.OA.UI.Properties.Resources.Delete;
            this.barDelete.Id = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.OA.UI.Properties.Resources.Transfer;
            this.barClose.Id = 5;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(800, 42);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 42);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 558);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 42);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 558);
            // 
            // barOpeanFile
            // 
            this.barOpeanFile.Caption = "&Opean";
            this.barOpeanFile.Glyph = global::ICP.OA.UI.Properties.Resources.OpenFile_S;
            this.barOpeanFile.Id = 7;
            this.barOpeanFile.Name = "barOpeanFile";
            // 
            // barSaveAs
            // 
            this.barSaveAs.Caption = "Save &As.";
            this.barSaveAs.Glyph = ((System.Drawing.Image)(resources.GetObject("barSaveAs.Glyph")));
            this.barSaveAs.Id = 8;
            this.barSaveAs.Name = "barSaveAs";
            // 
            // barSaveAll
            // 
            this.barSaveAll.Caption = "&Save All";
            this.barSaveAll.Glyph = global::ICP.OA.UI.Properties.Resources.Save_Blue;
            this.barSaveAll.Id = 9;
            this.barSaveAll.Name = "barSaveAll";
            // 
            // imageListAttachment
            // 
            this.imageListAttachment.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListAttachment.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListAttachment.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 42);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(800, 558);
            this.pnlContent.TabIndex = 4;
            // 
            // frmFaxPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFaxPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Read Fax";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFaxPreview_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barRevert;
        private DevExpress.XtraBars.BarButtonItem barRevertAll;
        private DevExpress.XtraBars.BarButtonItem barDelete;
    
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barTransfer;
        private System.Windows.Forms.ImageList imageListAttachment;
        private DevExpress.XtraBars.BarButtonItem barOpeanFile;
        private DevExpress.XtraBars.BarButtonItem barSaveAs;
        private DevExpress.XtraBars.BarButtonItem barSaveAll;
        private DevExpress.XtraEditors.PanelControl pnlContent;
    }
}
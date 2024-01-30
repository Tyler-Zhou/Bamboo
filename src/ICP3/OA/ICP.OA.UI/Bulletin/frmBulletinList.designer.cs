namespace ICP.OA.UI.Bulletin
{
    partial class frmBulletinList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBulletinList));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItemToday = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barButtonItemAll = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barButtonClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.memoEditSubject = new DevExpress.XtraEditors.MemoEdit();
            this.lblPublisherName = new DevExpress.XtraEditors.LabelControl();
            this.lblPublishTime = new DevExpress.XtraEditors.LabelControl();
            this.lblPriority = new DevExpress.XtraEditors.LabelControl();
            this.txtPublisherName = new DevExpress.XtraEditors.TextEdit();
            this.txtPublishTime = new DevExpress.XtraEditors.TextEdit();
            this.txtPriority = new DevExpress.XtraEditors.TextEdit();
            this.memoEditContent = new DevExpress.XtraEditors.MemoEdit();
            this.lblContent = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublisherName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublishTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPriority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditContent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imageCollection;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemToday,
            this.barButtonItemAll,
            this.barButtonClose});
            this.barManager.MaxItemId = 3;
            this.barManager.ToolTipController = this.toolTipController;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(619, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 351);
            this.barDockControlBottom.Size = new System.Drawing.Size(619, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 351);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(619, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 351);
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "high.gif");
            this.imageCollection.Images.SetKeyName(1, "low.gif");
            this.imageCollection.Images.SetKeyName(2, "all.png");
            this.imageCollection.Images.SetKeyName(3, "close.png");
            this.imageCollection.Images.SetKeyName(4, "today.png");
            // 
            // barButtonItemToday
            // 
            this.barButtonItemToday.Caption = "当天";
            this.barButtonItemToday.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barButtonItemToday.Id = 0;
            this.barButtonItemToday.ImageIndex = 4;
            this.barButtonItemToday.Name = "barButtonItemToday";
            this.barButtonItemToday.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemToday.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemToday_ItemClick);
            // 
            // barButtonItemAll
            // 
            this.barButtonItemAll.Caption = "全部";
            this.barButtonItemAll.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barButtonItemAll.Id = 1;
            this.barButtonItemAll.ImageIndex = 2;
            this.barButtonItemAll.Name = "barButtonItemAll";
            this.barButtonItemAll.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAll_ItemClick);
            // 
            // barButtonClose
            // 
            this.barButtonClose.Caption = "关闭";
            this.barButtonClose.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barButtonClose.Id = 2;
            this.barButtonClose.ImageIndex = 3;
            this.barButtonClose.Name = "barButtonClose";
            this.barButtonClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClose_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemToday),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonClose)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // lblSubject
            // 
            this.lblSubject.Location = new System.Drawing.Point(24, 12);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(36, 14);
            this.lblSubject.TabIndex = 14;
            this.lblSubject.Text = "标题：";
            // 
            // memoEditSubject
            // 
            this.memoEditSubject.Location = new System.Drawing.Point(78, 12);
            this.memoEditSubject.MenuManager = this.barManager;
            this.memoEditSubject.Name = "memoEditSubject";
            this.memoEditSubject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEditSubject.Properties.ReadOnly = true;
            this.memoEditSubject.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoEditSubject.Size = new System.Drawing.Size(524, 35);
            this.memoEditSubject.TabIndex = 0;
            this.memoEditSubject.TabStop = false;
            // 
            // lblPublisherName
            // 
            this.lblPublisherName.Location = new System.Drawing.Point(12, 56);
            this.lblPublisherName.Name = "lblPublisherName";
            this.lblPublisherName.Size = new System.Drawing.Size(48, 14);
            this.lblPublisherName.TabIndex = 16;
            this.lblPublisherName.Text = "发布人：";
            // 
            // lblPublishTime
            // 
            this.lblPublishTime.Location = new System.Drawing.Point(197, 56);
            this.lblPublishTime.Name = "lblPublishTime";
            this.lblPublishTime.Size = new System.Drawing.Size(52, 14);
            this.lblPublishTime.TabIndex = 17;
            this.lblPublishTime.Text = "发布时间:";
            // 
            // lblPriority
            // 
            this.lblPriority.Location = new System.Drawing.Point(439, 56);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(48, 14);
            this.lblPriority.TabIndex = 18;
            this.lblPriority.Text = "优先级：";
            // 
            // txtPublisherName
            // 
            this.txtPublisherName.Location = new System.Drawing.Point(78, 53);
            this.txtPublisherName.MenuManager = this.barManager;
            this.txtPublisherName.Name = "txtPublisherName";
            this.txtPublisherName.Properties.ReadOnly = true;
            this.txtPublisherName.Size = new System.Drawing.Size(100, 21);
            this.txtPublisherName.TabIndex = 1;
            this.txtPublisherName.TabStop = false;
            // 
            // txtPublishTime
            // 
            this.txtPublishTime.Location = new System.Drawing.Point(267, 53);
            this.txtPublishTime.MenuManager = this.barManager;
            this.txtPublishTime.Name = "txtPublishTime";
            this.txtPublishTime.Properties.ReadOnly = true;
            this.txtPublishTime.Size = new System.Drawing.Size(146, 21);
            this.txtPublishTime.TabIndex = 2;
            this.txtPublishTime.TabStop = false;
            // 
            // txtPriority
            // 
            this.txtPriority.Location = new System.Drawing.Point(511, 53);
            this.txtPriority.MenuManager = this.barManager;
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.Properties.ReadOnly = true;
            this.txtPriority.Size = new System.Drawing.Size(91, 21);
            this.txtPriority.TabIndex = 3;
            this.txtPriority.TabStop = false;
            // 
            // memoEditContent
            // 
            this.memoEditContent.Location = new System.Drawing.Point(78, 79);
            this.memoEditContent.MenuManager = this.barManager;
            this.memoEditContent.Name = "memoEditContent";
            this.memoEditContent.Properties.ReadOnly = true;
            this.memoEditContent.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoEditContent.Size = new System.Drawing.Size(524, 256);
            this.memoEditContent.TabIndex = 4;
            this.memoEditContent.TabStop = false;
            // 
            // lblContent
            // 
            this.lblContent.Location = new System.Drawing.Point(24, 82);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(36, 14);
            this.lblContent.TabIndex = 16;
            this.lblContent.Text = "内容：";
            // 
            // frmBulletinList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 351);
            this.Controls.Add(this.memoEditContent);
            this.Controls.Add(this.txtPriority);
            this.Controls.Add(this.txtPublishTime);
            this.Controls.Add(this.txtPublisherName);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.lblPublishTime);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.lblPublisherName);
            this.Controls.Add(this.memoEditSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
             
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBulletinList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "公告";
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublisherName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublishTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPriority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditContent.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem barButtonItemToday;
        private DevExpress.XtraBars.BarLargeButtonItem barButtonItemAll;
        private DevExpress.XtraBars.BarLargeButtonItem barButtonClose;
        private DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraEditors.MemoEdit memoEditContent;
        private DevExpress.XtraEditors.TextEdit txtPriority;
        private DevExpress.XtraEditors.TextEdit txtPublishTime;
        private DevExpress.XtraEditors.TextEdit txtPublisherName;
        private DevExpress.XtraEditors.LabelControl lblPriority;
        private DevExpress.XtraEditors.LabelControl lblPublishTime;
        private DevExpress.XtraEditors.LabelControl lblContent;
        private DevExpress.XtraEditors.LabelControl lblPublisherName;
        private DevExpress.XtraEditors.MemoEdit memoEditSubject;
        private DevExpress.XtraEditors.LabelControl lblSubject;
    }
}
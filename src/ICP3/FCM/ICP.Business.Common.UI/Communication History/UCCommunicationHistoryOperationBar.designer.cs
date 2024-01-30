namespace ICP.Business.Common.UI.Communication
{
    partial class UCCommunicationHistoryOperationBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCommunicationHistoryOperationBar));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barOperation = new DevExpress.XtraBars.Bar();
            this.barsubItemview = new DevExpress.XtraBars.BarSubItem();
            this.barChitemEmail = new DevExpress.XtraBars.BarCheckItem();
            this.barChItemFax = new DevExpress.XtraBars.BarCheckItem();
            this.barChItemEDI = new DevExpress.XtraBars.BarCheckItem();
            this.barItemOpen = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemReply = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItemResend = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barSetStage = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.barItemNew = new DevExpress.XtraBars.BarLargeButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barOperation,
            this.bar1,
            this.bar2});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imageList;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barItemOpen,
            this.barItemReply,
            this.barItemResend,
            this.barsubItemview,
            this.barChitemEmail,
            this.barChItemFax,
            this.barChItemEDI,
            this.barSetStage});
            this.barManager.LargeImages = this.imageList;
            this.barManager.MainMenu = this.barOperation;
            this.barManager.MaxItemId = 24;
            // 
            // barOperation
            // 
            this.barOperation.BarName = "Main menu";
            this.barOperation.DockCol = 0;
            this.barOperation.DockRow = 0;
            this.barOperation.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barOperation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barsubItemview),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemReply),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemResend),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSetStage, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barOperation.OptionsBar.AllowQuickCustomization = false;
            this.barOperation.OptionsBar.MultiLine = true;
            this.barOperation.OptionsBar.UseWholeRow = true;
            this.barOperation.Text = "Main menu";
            // 
            // barsubItemview
            // 
            this.barsubItemview.Caption = "View";
            this.barsubItemview.Id = 9;
            this.barsubItemview.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barChitemEmail),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChItemFax),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChItemEDI)});
            this.barsubItemview.Name = "barsubItemview";
            // 
            // barChitemEmail
            // 
            this.barChitemEmail.Caption = "Email";
            this.barChitemEmail.Id = 20;
            this.barChitemEmail.ImageIndex = 2;
            this.barChitemEmail.Name = "barChitemEmail";
            this.barChitemEmail.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemEmail_Fax_EDI_ItemClick);
            // 
            // barChItemFax
            // 
            this.barChItemFax.Caption = "Fax";
            this.barChItemFax.Id = 21;
            this.barChItemFax.ImageIndex = 3;
            this.barChItemFax.Name = "barChItemFax";
            this.barChItemFax.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemEmail_Fax_EDI_ItemClick);
            // 
            // barChItemEDI
            // 
            this.barChItemEDI.Caption = "EDI";
            this.barChItemEDI.Id = 22;
            this.barChItemEDI.ImageIndex = 1;
            this.barChItemEDI.Name = "barChItemEDI";
            this.barChItemEDI.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemEmail_Fax_EDI_ItemClick);
            // 
            // barItemOpen
            // 
            this.barItemOpen.Caption = "&Open";
            this.barItemOpen.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemOpen.Id = 3;
            this.barItemOpen.LargeImageIndex = 4;
            this.barItemOpen.Name = "barItemOpen";
            this.barItemOpen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barItemOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemOpen_ItemClick);
            // 
            // barItemReply
            // 
            this.barItemReply.Caption = "&Reply";
            this.barItemReply.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemReply.Id = 4;
            this.barItemReply.LargeImageIndex = 5;
            this.barItemReply.Name = "barItemReply";
            this.barItemReply.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barItemReply.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemReply_ItemClick);
            // 
            // barItemResend
            // 
            this.barItemResend.Caption = "Re&send";
            this.barItemResend.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barItemResend.Id = 5;
            this.barItemResend.LargeImageIndex = 0;
            this.barItemResend.Name = "barItemResend";
            this.barItemResend.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barItemResend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemResend_ItemClick);
            // 
            // barSetStage
            // 
            this.barSetStage.Caption = "Set Stage";
            this.barSetStage.Id = 23;
            this.barSetStage.Name = "barSetStage";
            this.barSetStage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSetStage_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Custom 3";
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 4";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 2;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.Text = "Custom 4";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(645, 76);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 25);
            this.barDockControlBottom.Size = new System.Drawing.Size(645, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(645, 76);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "resend.gif");
            this.imageList.Images.SetKeyName(1, "EDI.png");
            this.imageList.Images.SetKeyName(2, "email.gif");
            this.imageList.Images.SetKeyName(3, "fax.gif");
            this.imageList.Images.SetKeyName(4, "open.png");
            this.imageList.Images.SetKeyName(5, "reply.png");
            // 
            // barItemNew
            // 
            this.barItemNew.Caption = "&New";
            this.barItemNew.Id = 6;
            this.barItemNew.Name = "barItemNew";
            this.barItemNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemNew_ItemClick);
            // 
            // UCCommunicationHistoryOperationBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCCommunicationHistoryOperationBar";
            this.Size = new System.Drawing.Size(645, 25);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barOperation;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraBars.BarLargeButtonItem barItemOpen;
        private DevExpress.XtraBars.BarLargeButtonItem barItemReply;
        private DevExpress.XtraBars.BarLargeButtonItem barItemResend;
        private DevExpress.XtraBars.BarLargeButtonItem barItemNew;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarSubItem barsubItemview;
        private DevExpress.XtraBars.BarCheckItem barChitemEmail;
        private DevExpress.XtraBars.BarCheckItem barChItemFax;
        private DevExpress.XtraBars.BarCheckItem barChItemEDI;
        private DevExpress.XtraBars.BarButtonItem barSetStage;

    }
}

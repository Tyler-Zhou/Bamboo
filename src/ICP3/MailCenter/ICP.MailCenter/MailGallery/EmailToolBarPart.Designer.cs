namespace ICP.MailCenter.UI
{
    partial class EmailToolBarPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailToolBarPart));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolNewMail = new System.Windows.Forms.ToolStripButton();
            this.toolReply = new System.Windows.Forms.ToolStripButton();
            this.toolReplyAll = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolReplyAllContainsAttachment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolForward = new System.Windows.Forms.ToolStripButton();
            this.toolSSForward = new System.Windows.Forms.ToolStripSeparator();
            this.toolDeleteMail = new System.Windows.Forms.ToolStripButton();
            this.toolPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolSSSearch = new System.Windows.Forms.ToolStripSeparator();
            this.toolSendAndReceive = new System.Windows.Forms.ToolStripButton();
            this.toolRefershBar = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolRefersh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolEmailArchiving = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSearch = new System.Windows.Forms.ToolStripButton();
            this.toolSBSearch = new System.Windows.Forms.ToolStripButton();
            this.toolSSSendAndRecive = new System.Windows.Forms.ToolStripSeparator();
            this.toolSetting = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolReminder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMailReadTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolRules = new System.Windows.Forms.ToolStripMenuItem();
            this.syncAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolClose = new System.Windows.Forms.ToolStripButton();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.BackColor = System.Drawing.Color.Transparent;
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNewMail,
            this.toolReply,
            this.toolReplyAll,
            this.toolForward,
            this.toolSSForward,
            this.toolDeleteMail,
            this.toolPrintPreview,
            this.toolSSSearch,
            this.toolSearch,
            this.toolSendAndReceive,
            this.toolRefershBar,
            this.toolSSSendAndRecive,
            this.toolSetting,
            this.toolSBSearch,
            this.toolStripSeparator1,
            this.toolClose});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(738, 26);
            this.toolBar.TabIndex = 3;
            this.toolBar.Text = "toolStrip1";
            // 
            // toolNewMail
            // 
            this.toolNewMail.Image = ((System.Drawing.Image)(resources.GetObject("toolNewMail.Image")));
            this.toolNewMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewMail.Name = "toolNewMail";
            this.toolNewMail.Size = new System.Drawing.Size(54, 23);
            this.toolNewMail.Text = "&New";
            this.toolNewMail.ToolTipText = "New(Ctrl+N)";
            this.toolNewMail.Click += new System.EventHandler(this.toolNewMail_Click);
            // 
            // toolReply
            // 
            this.toolReply.Image = ((System.Drawing.Image)(resources.GetObject("toolReply.Image")));
            this.toolReply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReply.Name = "toolReply";
            this.toolReply.Size = new System.Drawing.Size(60, 23);
            this.toolReply.Text = "&Reply";
            this.toolReply.ToolTipText = "Reply(Ctrl+R)";
            this.toolReply.Click += new System.EventHandler(this.toolReply_Click);
            // 
            // toolReplyAll
            // 
            this.toolReplyAll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolReplyAllContainsAttachment});
            this.toolReplyAll.Image = ((System.Drawing.Image)(resources.GetObject("toolReplyAll.Image")));
            this.toolReplyAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReplyAll.Name = "toolReplyAll";
            this.toolReplyAll.Size = new System.Drawing.Size(103, 23);
            this.toolReplyAll.Text = "Reply to Al&l";
            this.toolReplyAll.ToolTipText = "Reply to All(Ctrl+Shift+R)";
            this.toolReplyAll.Click += new System.EventHandler(this.toolReplyAll_Click);       
            this.toolReplyAll.MouseHover += new System.EventHandler(this.toolReplyAll_MouseHover);
            // 
            // toolReplyAllContainsAttachment
            // 
            this.toolReplyAllContainsAttachment.Image = ((System.Drawing.Image)(resources.GetObject("toolReplyAllContainsAttachment.Image")));
            this.toolReplyAllContainsAttachment.Name = "toolReplyAllContainsAttachment";
            this.toolReplyAllContainsAttachment.Size = new System.Drawing.Size(269, 22);
            this.toolReplyAllContainsAttachment.Text = "Reply to All(Contains Attachment)";
            this.toolReplyAllContainsAttachment.Click += new System.EventHandler(this.toolReplyAllContainsAttachment_Click);
            this.toolReplyAllContainsAttachment.MouseLeave += new System.EventHandler(this.toolReplyAllContainsAttachment_MouseLeave);
            // 
            // toolForward
            // 
            this.toolForward.Image = ((System.Drawing.Image)(resources.GetObject("toolForward.Image")));
            this.toolForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolForward.Name = "toolForward";
            this.toolForward.Size = new System.Drawing.Size(76, 23);
            this.toolForward.Text = "F&orward";
            this.toolForward.ToolTipText = "Forward(Ctrl+F)";
            this.toolForward.Click += new System.EventHandler(this.toolForward_Click);
            // 
            // toolSSForward
            // 
            this.toolSSForward.Name = "toolSSForward";
            this.toolSSForward.Size = new System.Drawing.Size(6, 26);
            // 
            // toolDeleteMail
            // 
            this.toolDeleteMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDeleteMail.Image = ((System.Drawing.Image)(resources.GetObject("toolDeleteMail.Image")));
            this.toolDeleteMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeleteMail.Name = "toolDeleteMail";
            this.toolDeleteMail.Size = new System.Drawing.Size(23, 23);
            this.toolDeleteMail.Text = "Delete";
            this.toolDeleteMail.Click += new System.EventHandler(this.toolDeleteMail_Click);
            // 
            // toolPrintPreview
            // 
            this.toolPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolPrintPreview.Image")));
            this.toolPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrintPreview.Name = "toolPrintPreview";
            this.toolPrintPreview.Size = new System.Drawing.Size(23, 23);
            this.toolPrintPreview.Text = "PrintPreview";
            this.toolPrintPreview.Click += new System.EventHandler(this.toolPrintPreview_Click);
            // 
            // toolSSSearch
            // 
            this.toolSSSearch.Name = "toolSSSearch";
            this.toolSSSearch.Size = new System.Drawing.Size(6, 26);
            // 
            // toolSendAndReceive
            // 
            this.toolSendAndReceive.Image = ((System.Drawing.Image)(resources.GetObject("toolSendAndReceive.Image")));
            this.toolSendAndReceive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSendAndReceive.Name = "toolSendAndReceive";
            this.toolSendAndReceive.Size = new System.Drawing.Size(106, 23);
            this.toolSendAndReceive.Tag = "SendAndReceive";
            this.toolSendAndReceive.Text = "Send/Re&ceive";
            this.toolSendAndReceive.Click += new System.EventHandler(this.toolSendAndReceiveSendAndReceive_Click);
            // 
            // toolRefershBar
            // 
            this.toolRefershBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolRefershBar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefersh,
            this.toolEmailArchiving});
            this.toolRefershBar.Image = ((System.Drawing.Image)(resources.GetObject("toolRefershBar.Image")));
            this.toolRefershBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefershBar.Name = "toolRefershBar";
            this.toolRefershBar.Size = new System.Drawing.Size(13, 23);
            this.toolRefershBar.Text = "Refersh";
            // 
            // toolRefersh
            // 
            this.toolRefersh.Name = "toolRefersh";
            this.toolRefersh.Size = new System.Drawing.Size(184, 22);
            this.toolRefersh.Text = "Refersh Folder List";
            this.toolRefersh.Click += new System.EventHandler(this.toolRefersh_Click);
            // 
            // toolEmailArchiving
            // 
            this.toolEmailArchiving.Name = "toolEmailArchiving";
            this.toolEmailArchiving.Size = new System.Drawing.Size(184, 22);
            this.toolEmailArchiving.Text = "Email Archiving";
            this.toolEmailArchiving.Click += new System.EventHandler(this.toolEmailArchiving_Click);
            // 
            // toolSearch
            // 
            this.toolSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolSearch.Image")));
            this.toolSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSearch.Name = "toolSearch";
            this.toolSearch.Size = new System.Drawing.Size(67, 23);
            this.toolSearch.Text = "&Search";
            this.toolSearch.ToolTipText = "Search(Ctrl+Shift+F)";
            this.toolSearch.Click += new System.EventHandler(this.toolSearch_Click);
            // 
            // toolSBSearch
            // 
            this.toolSBSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSBSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolSBSearch.Image")));
            this.toolSBSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSBSearch.Name = "toolSBSearch";
            this.toolSBSearch.Size = new System.Drawing.Size(23, 23);
            this.toolSBSearch.Text = "toolStripSplitButton1";
            this.toolSBSearch.ToolTipText = "Address Book";
            this.toolSBSearch.Click += new System.EventHandler(this.toolStripSplitButton1_Click);
            // 
            // toolSSSendAndRecive
            // 
            this.toolSSSendAndRecive.Name = "toolSSSendAndRecive";
            this.toolSSSendAndRecive.Size = new System.Drawing.Size(6, 26);
            // 
            // toolSetting
            // 
            this.toolSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolReminder,
            this.toolMailReadTime,
            this.toolRules,
            this.syncAddressToolStripMenuItem});
            this.toolSetting.Image = ((System.Drawing.Image)(resources.GetObject("toolSetting.Image")));
            this.toolSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSetting.Name = "toolSetting";
            this.toolSetting.Size = new System.Drawing.Size(77, 23);
            this.toolSetting.Text = "Setting";
            // 
            // toolReminder
            // 
            this.toolReminder.Image = global::ICP.MailCenter.UI.Properties.Resources.toolReminder_Image;
            this.toolReminder.Name = "toolReminder";
            this.toolReminder.Size = new System.Drawing.Size(207, 22);
            this.toolReminder.Text = "Send/Receive Time";
            this.toolReminder.Click += new System.EventHandler(this.toolReminder_Click);
            // 
            // toolMailReadTime
            // 
            this.toolMailReadTime.Image = global::ICP.MailCenter.UI.Properties.Resources.cal_allday;
            this.toolMailReadTime.Name = "toolMailReadTime";
            this.toolMailReadTime.Size = new System.Drawing.Size(207, 22);
            this.toolMailReadTime.Text = "Mail Read Time";
            this.toolMailReadTime.Click += new System.EventHandler(this.toolMailReadTime_Click);
            // 
            // toolRules
            // 
            this.toolRules.Image = global::ICP.MailCenter.UI.Properties.Resources.toolRules_Image;
            this.toolRules.Name = "toolRules";
            this.toolRules.Size = new System.Drawing.Size(207, 22);
            this.toolRules.Text = "Rules/Alerts";
            this.toolRules.ToolTipText = "Rules and alerts";
            this.toolRules.Click += new System.EventHandler(this.toolRules_Click);
            // 
            // syncAddressToolStripMenuItem
            // 
            this.syncAddressToolStripMenuItem.Image = global::ICP.MailCenter.UI.Properties.Resources.contact_dl;
            this.syncAddressToolStripMenuItem.Name = "syncAddressToolStripMenuItem";
            this.syncAddressToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.syncAddressToolStripMenuItem.Text = "Sync Contact From ICP";
            this.syncAddressToolStripMenuItem.Click += new System.EventHandler(this.syncAddressToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolClose
            // 
            this.toolClose.Image = ((System.Drawing.Image)(resources.GetObject("toolClose.Image")));
            this.toolClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClose.Name = "toolClose";
            this.toolClose.Size = new System.Drawing.Size(60, 23);
            this.toolClose.Text = "Close";
            this.toolClose.Click += new System.EventHandler(this.toolClose_Click);
            // 
            // EmailToolBarPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolBar);
            this.Name = "EmailToolBarPart";
            this.Size = new System.Drawing.Size(738, 26);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        public System.Windows.Forms.ToolStripButton toolNewMail;
        public System.Windows.Forms.ToolStripButton toolReply;
        public System.Windows.Forms.ToolStripButton toolForward;
        private System.Windows.Forms.ToolStripSeparator toolSSForward;
        private System.Windows.Forms.ToolStripButton toolSearch;
        private System.Windows.Forms.ToolStripSeparator toolSSSearch;
        private System.Windows.Forms.ToolStripButton toolSBSearch;
        private System.Windows.Forms.ToolStripSeparator toolSSSendAndRecive;
        private System.Windows.Forms.ToolStripButton toolSendAndReceive;
        public System.Windows.Forms.ToolStripDropDownButton toolRefershBar;
        public System.Windows.Forms.ToolStripMenuItem toolRefersh;
        private System.Windows.Forms.ToolStripButton toolPrintPreview;
        private System.Windows.Forms.ToolStripButton toolClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolDeleteMail;
        private System.Windows.Forms.ToolStripDropDownButton toolSetting;
        private System.Windows.Forms.ToolStripMenuItem toolMailReadTime;
        private System.Windows.Forms.ToolStripMenuItem toolReminder;
        private System.Windows.Forms.ToolStripMenuItem toolRules;
        private System.Windows.Forms.ToolStripMenuItem syncAddressToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem toolEmailArchiving;
        public System.Windows.Forms.ToolStripDropDownButton toolReplyAll;
        private System.Windows.Forms.ToolStripMenuItem toolReplyAllContainsAttachment;
    }
}

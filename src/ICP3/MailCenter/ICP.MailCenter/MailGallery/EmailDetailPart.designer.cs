using System.Windows.Forms;

namespace ICP.MailCenter.UI
{
    partial class EmailDetailPart
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
            if (disposing)
            {
                this.currentMail = null;
            }
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
            this.lblRecipients = new System.Windows.Forms.Label();
            this.pnlRecivient = new System.Windows.Forms.Panel();
            this.pnlRecipients = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSendTime = new System.Windows.Forms.Label();
            this.lblSender = new System.Windows.Forms.Label();
            this.pnlSubject = new System.Windows.Forms.Panel();
            this.txtSubject = new ICP.MailCenter.UI.GrowTextBox();
            this.pnlSender = new System.Windows.Forms.Panel();
            this.pnlTime = new System.Windows.Forms.Panel();
            this.lnkTime = new System.Windows.Forms.LinkLabel();
            this.lnkSender = new ICP.MailCenter.UI.AddressControl();
            this.lblCC = new System.Windows.Forms.Label();
            this.pnlCC = new System.Windows.Forms.Panel();
            this.flpCC = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.flpAttachment = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAtt = new System.Windows.Forms.Panel();
            this.pnlSplitter = new System.Windows.Forms.Panel();
            this.splitContainer = new DevExpress.XtraEditors.SplitContainerControl();
            this.webBContent = new System.Windows.Forms.WebBrowser();
            this.txtDescription = new ICP.MailCenter.UI.GrowTextBox();
            this.pnlRecivient.SuspendLayout();
            this.pnlSubject.SuspendLayout();
            this.pnlSender.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.pnlCC.SuspendLayout();
            this.pnlAtt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRecipients
            // 
            this.lblRecipients.AutoSize = true;
            this.lblRecipients.ForeColor = System.Drawing.Color.Blue;
            this.lblRecipients.Location = new System.Drawing.Point(4, 3);
            this.lblRecipients.Name = "lblRecipients";
            this.lblRecipients.Size = new System.Drawing.Size(23, 12);
            this.lblRecipients.TabIndex = 0;
            this.lblRecipients.Text = "To:";
            // 
            // pnlRecivient
            // 
            this.pnlRecivient.Controls.Add(this.lblRecipients);
            this.pnlRecivient.Controls.Add(this.pnlRecipients);
            this.pnlRecivient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecivient.Location = new System.Drawing.Point(0, 65);
            this.pnlRecivient.Name = "pnlRecivient";
            this.pnlRecivient.Size = new System.Drawing.Size(662, 23);
            this.pnlRecivient.TabIndex = 22;
            // 
            // pnlRecipients
            // 
            this.pnlRecipients.BackColor = System.Drawing.Color.White;
            this.pnlRecipients.Location = new System.Drawing.Point(54, 1);
            this.pnlRecipients.Name = "pnlRecipients";
            this.pnlRecipients.Size = new System.Drawing.Size(605, 20);
            this.pnlRecipients.TabIndex = 0;
            // 
            // lblSendTime
            // 
            this.lblSendTime.AutoSize = true;
            this.lblSendTime.ForeColor = System.Drawing.Color.Blue;
            this.lblSendTime.Location = new System.Drawing.Point(3, 4);
            this.lblSendTime.Name = "lblSendTime";
            this.lblSendTime.Size = new System.Drawing.Size(47, 12);
            this.lblSendTime.TabIndex = 0;
            this.lblSendTime.Text = "SentOn:";
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.ForeColor = System.Drawing.Color.Blue;
            this.lblSender.Location = new System.Drawing.Point(4, 5);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(47, 12);
            this.lblSender.TabIndex = 0;
            this.lblSender.Text = "Sender:";
            // 
            // pnlSubject
            // 
            this.pnlSubject.Controls.Add(this.txtSubject);
            this.pnlSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubject.Location = new System.Drawing.Point(0, 0);
            this.pnlSubject.Name = "pnlSubject";
            this.pnlSubject.Size = new System.Drawing.Size(662, 26);
            this.pnlSubject.TabIndex = 37;
            // 
            // txtSubject
            // 
            this.txtSubject.BackColor = System.Drawing.Color.White;
            this.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubject.Font = new System.Drawing.Font("宋体", 13F);
            this.txtSubject.FullHeight = 0;
            this.txtSubject.Location = new System.Drawing.Point(0, 0);
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ReadOnly = true;
            this.txtSubject.Size = new System.Drawing.Size(662, 26);
            this.txtSubject.TabIndex = 0;
            // 
            // pnlSender
            // 
            this.pnlSender.Controls.Add(this.pnlTime);
            this.pnlSender.Controls.Add(this.lblSender);
            this.pnlSender.Controls.Add(this.lnkSender);
            this.pnlSender.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSender.Location = new System.Drawing.Point(0, 43);
            this.pnlSender.Name = "pnlSender";
            this.pnlSender.Size = new System.Drawing.Size(662, 22);
            this.pnlSender.TabIndex = 38;
            // 
            // pnlTime
            // 
            this.pnlTime.BackColor = System.Drawing.Color.White;
            this.pnlTime.Controls.Add(this.lnkTime);
            this.pnlTime.Controls.Add(this.lblSendTime);
            this.pnlTime.Location = new System.Drawing.Point(281, 0);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(381, 22);
            this.pnlTime.TabIndex = 1;
            // 
            // lnkTime
            // 
            this.lnkTime.ActiveLinkColor = System.Drawing.Color.White;
            this.lnkTime.AutoSize = true;
            this.lnkTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lnkTime.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkTime.LinkColor = System.Drawing.SystemColors.ControlText;
            this.lnkTime.Location = new System.Drawing.Point(54, 4);
            this.lnkTime.Name = "lnkTime";
            this.lnkTime.Size = new System.Drawing.Size(29, 12);
            this.lnkTime.TabIndex = 1;
            this.lnkTime.TabStop = true;
            this.lnkTime.Text = "None";
            // 
            // lnkSender
            // 
            this.lnkSender.BackColor = System.Drawing.SystemColors.Window;
            this.lnkSender.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lnkSender.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lnkSender.Location = new System.Drawing.Point(54, 3);
            this.lnkSender.Margin = new System.Windows.Forms.Padding(0);
            this.lnkSender.Name = "lnkSender";
            this.lnkSender.RecipientType = 0;
            this.lnkSender.Size = new System.Drawing.Size(41, 17);
            this.lnkSender.TabIndex = 2;
            this.lnkSender.Text = "[ ]";
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.ForeColor = System.Drawing.Color.Blue;
            this.lblCC.Location = new System.Drawing.Point(5, 3);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(23, 12);
            this.lblCC.TabIndex = 0;
            this.lblCC.Text = "CC:";
            // 
            // pnlCC
            // 
            this.pnlCC.Controls.Add(this.flpCC);
            this.pnlCC.Controls.Add(this.lblCC);
            this.pnlCC.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCC.Location = new System.Drawing.Point(0, 88);
            this.pnlCC.Name = "pnlCC";
            this.pnlCC.Size = new System.Drawing.Size(662, 22);
            this.pnlCC.TabIndex = 40;
            // 
            // flpCC
            // 
            this.flpCC.Location = new System.Drawing.Point(54, 1);
            this.flpCC.Margin = new System.Windows.Forms.Padding(0);
            this.flpCC.Name = "flpCC";
            this.flpCC.Size = new System.Drawing.Size(605, 20);
            this.flpCC.TabIndex = 1;
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.ForeColor = System.Drawing.Color.Blue;
            this.lblAttachment.Location = new System.Drawing.Point(4, 3);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(71, 12);
            this.lblAttachment.TabIndex = 0;
            this.lblAttachment.Text = "Attachment:";
            // 
            // flpAttachment
            // 
            this.flpAttachment.BackColor = System.Drawing.Color.White;
            this.flpAttachment.Location = new System.Drawing.Point(73, 1);
            this.flpAttachment.Name = "flpAttachment";
            this.flpAttachment.Size = new System.Drawing.Size(586, 18);
            this.flpAttachment.TabIndex = 0;
            this.flpAttachment.LostFocus += new System.EventHandler(this.flpAttachment_LostFocus);
            // 
            // pnlAtt
            // 
            this.pnlAtt.Controls.Add(this.lblAttachment);
            this.pnlAtt.Controls.Add(this.flpAttachment);
            this.pnlAtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAtt.Location = new System.Drawing.Point(0, 110);
            this.pnlAtt.Name = "pnlAtt";
            this.pnlAtt.Size = new System.Drawing.Size(662, 23);
            this.pnlAtt.TabIndex = 41;
            // 
            // pnlSplitter
            // 
            this.pnlSplitter.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pnlSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplitter.Location = new System.Drawing.Point(0, 133);
            this.pnlSplitter.Name = "pnlSplitter";
            this.pnlSplitter.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pnlSplitter.Size = new System.Drawing.Size(662, 1);
            this.pnlSplitter.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainer.Horizontal = false;
            this.splitContainer.Location = new System.Drawing.Point(0, 133);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.webBContent);
            this.splitContainer.Panel1.Text = "Panel1";
            this.splitContainer.Panel2.Text = "Panel2";
            this.splitContainer.Size = new System.Drawing.Size(662, 560);
            this.splitContainer.SplitterPosition = 261;
            this.splitContainer.TabIndex = 42;
            this.splitContainer.Text = "splitContainerControl1";
            this.splitContainer.SplitterPositionChanged += new System.EventHandler(this.OnSplitterPositionChanged);
            // 
            // webBContent
            // 
            this.webBContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBContent.Location = new System.Drawing.Point(0, 0);
            this.webBContent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBContent.Name = "webBContent";
            this.webBContent.ScriptErrorsSuppressed = true;
            this.webBContent.Size = new System.Drawing.Size(662, 293);
            this.webBContent.TabIndex = 52;
            // this.webBContent.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBContent_Navigating);
            this.webBContent.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBContent_PreviewKeyDown);
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDescription.FullHeight = 17;
            this.txtDescription.Location = new System.Drawing.Point(0, 26);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(662, 17);
            this.txtDescription.TabIndex = 43;
            this.txtDescription.Text = "  This message has not been sent.";
            this.txtDescription.Visible = false;
            // 
            // EmailDetailPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlSplitter);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlAtt);
            this.Controls.Add(this.pnlCC);
            this.Controls.Add(this.pnlRecivient);
            this.Controls.Add(this.pnlSender);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.pnlSubject);
            this.DoubleBuffered = true;
            this.Name = "EmailDetailPart";
            this.Size = new System.Drawing.Size(662, 693);
            this.SizeChanged += new System.EventHandler(this.EmailDetailPart_SizeChanged);
            this.pnlRecivient.ResumeLayout(false);
            this.pnlRecivient.PerformLayout();
            this.pnlSubject.ResumeLayout(false);
            this.pnlSubject.PerformLayout();
            this.pnlSender.ResumeLayout(false);
            this.pnlSender.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            this.pnlCC.ResumeLayout(false);
            this.pnlCC.PerformLayout();
            this.pnlAtt.ResumeLayout(false);
            this.pnlAtt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        //void webBContent_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        //{
        //    if (e.Url.ToString() != "about:blank")

        //        e.Cancel = true;

        //}
        #endregion

        private Label lblRecipients;
        private Panel pnlRecivient;
        private FlowLayoutPanel pnlRecipients;
        private Label lblSendTime;
        private AddressControl lnkSender;
        private Label lblSender;
        private Panel pnlSubject;
        private Panel pnlSender;
        private LinkLabel lnkTime;
        private Panel pnlTime;
        private Label lblCC;
        private Panel pnlCC;
        private FlowLayoutPanel flpCC;
        private Label lblAttachment;
        public FlowLayoutPanel flpAttachment;
        public Panel pnlAtt;
        private Panel pnlSplitter;
        public DevExpress.XtraEditors.SplitContainerControl splitContainer;
        public WebBrowser webBContent;
        public ICP.MailCenter.UI.GrowTextBox txtSubject;
        private GrowTextBox txtDescription;
    }
}

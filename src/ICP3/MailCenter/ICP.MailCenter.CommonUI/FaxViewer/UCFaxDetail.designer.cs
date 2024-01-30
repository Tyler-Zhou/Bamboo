namespace ICP.MailCenter.CommonUI
{
    partial class UCFaxDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFaxDetail));
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.pnlLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblCC = new System.Windows.Forms.Label();
            this.txtCC = new ICP.MailCenter.CommonUI.FaxViewer.EmailAddressTextBox();
            this.attachmentPanel = new ICP.MailCenter.CommonUI.AttachmentPanel(this.components);
            this.txtSender = new ICP.MailCenter.CommonUI.FaxViewer.EmailAddressTextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblSender = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblSendTime = new System.Windows.Forms.Label();
            this.txtTo = new ICP.MailCenter.CommonUI.FaxViewer.EmailAddressTextBox();
            this.txtSendTime = new System.Windows.Forms.TextBox();
            this.btnFax = new DevExpress.XtraEditors.SimpleButton();
            this.pnlLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLayout
            // 
            this.pnlLayout.AutoSize = true;
            this.pnlLayout.ColumnCount = 2;
            this.pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlLayout.Controls.Add(this.txtSubject, 1, 0);
            this.pnlLayout.Controls.Add(this.lblCC, 0, 4);
            this.pnlLayout.Controls.Add(this.txtCC, 1, 4);
            this.pnlLayout.Controls.Add(this.attachmentPanel, 1, 5);
            this.pnlLayout.Controls.Add(this.txtSender, 1, 1);
            this.pnlLayout.Controls.Add(this.lblSubject, 0, 0);
            this.pnlLayout.Controls.Add(this.lblSender, 0, 1);
            this.pnlLayout.Controls.Add(this.lblTo, 0, 2);
            this.pnlLayout.Controls.Add(this.lblSendTime, 0, 3);
            this.pnlLayout.Controls.Add(this.txtTo, 1, 2);
            this.pnlLayout.Controls.Add(this.txtSendTime, 1, 3);
            this.pnlLayout.Controls.Add(this.btnFax, 0, 5);
            this.pnlLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLayout.Location = new System.Drawing.Point(0, 0);
            this.pnlLayout.Name = "pnlLayout";
            this.pnlLayout.RowCount = 6;
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlLayout.Size = new System.Drawing.Size(300, 179);
            this.pnlLayout.TabIndex = 15;
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubject.Location = new System.Drawing.Point(83, 3);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(214, 15);
            this.txtSubject.TabIndex = 18;
            // 
            // lblCC
            // 
            this.lblCC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCC.AutoSize = true;
            this.lblCC.Location = new System.Drawing.Point(3, 89);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(74, 28);
            this.lblCC.TabIndex = 6;
            this.lblCC.Text = "抄送：";
            this.lblCC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCC
            // 
            this.txtCC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.txtCC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            
            this.txtCC.Location = new System.Drawing.Point(83, 92);
            this.txtCC.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtCC.MaxHeight = 35;
            this.txtCC.MinHeight = 21;
            this.txtCC.MinimumSize = new System.Drawing.Size(4, 21);
            this.txtCC.Multiline = true;
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(207, 22);
            this.txtCC.TabIndex = 13;
            // 
            // attachmentPanel
            // 
            this.attachmentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.attachmentPanel.AutoScroll = true;
            this.attachmentPanel.AutoSize = true;
            this.attachmentPanel.DataSource = ((ICP.Message.ServiceInterface.Message)(resources.GetObject("attachmentPanel.DataSource")));
            this.attachmentPanel.Location = new System.Drawing.Point(83, 127);
            this.attachmentPanel.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.attachmentPanel.MaximumSize = new System.Drawing.Size(0, 80);
            this.attachmentPanel.MinimumSize = new System.Drawing.Size(200, 30);
            this.attachmentPanel.Name = "attachmentPanel";
            this.attachmentPanel.Size = new System.Drawing.Size(207, 49);
            this.attachmentPanel.TabIndex = 14;
            // 
            // txtSender
            // 
            this.txtSender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.txtSender.BorderStyle = System.Windows.Forms.BorderStyle.None;
             
            this.txtSender.Location = new System.Drawing.Point(83, 24);
            this.txtSender.MaxHeight = 35;
            this.txtSender.MinHeight = 21;
            this.txtSender.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(214, 21);
            this.txtSender.TabIndex = 15;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(3, 0);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(43, 14);
            this.lblSubject.TabIndex = 16;
            this.lblSubject.Text = "主题：";
            this.lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(3, 21);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(55, 14);
            this.lblSender.TabIndex = 17;
            this.lblSender.Text = "发件人：";
            this.lblSender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(3, 45);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(74, 23);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "收件人：";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSendTime
            // 
            this.lblSendTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSendTime.AutoSize = true;
            this.lblSendTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSendTime.Location = new System.Drawing.Point(3, 68);
            this.lblSendTime.Name = "lblSendTime";
            this.lblSendTime.Size = new System.Drawing.Size(74, 21);
            this.lblSendTime.TabIndex = 2;
            this.lblSendTime.Text = "发送时间：";
            this.lblSendTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTo
            // 
            this.txtTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.txtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
             
            this.txtTo.Location = new System.Drawing.Point(83, 48);
            this.txtTo.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtTo.MaxHeight = 35;
            this.txtTo.MinHeight = 21;
            this.txtTo.MinimumSize = new System.Drawing.Size(4, 21);
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(207, 21);
            this.txtTo.TabIndex = 12;
            // 
            // txtSendTime
            // 
            this.txtSendTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSendTime.Location = new System.Drawing.Point(83, 71);
            this.txtSendTime.Name = "txtSendTime";
            this.txtSendTime.Size = new System.Drawing.Size(214, 15);
            this.txtSendTime.TabIndex = 11;
            // 
            // btnFax
            // 
            this.btnFax.Image = global::ICP.MailCenter.CommonUI.Properties.Resources.mail2;
            this.btnFax.Location = new System.Drawing.Point(3, 120);
            this.btnFax.Name = "btnFax";
            this.btnFax.Size = new System.Drawing.Size(66, 31);
            this.btnFax.TabIndex = 6;
            this.btnFax.Text = "内容";
            this.btnFax.Click += new System.EventHandler(this.btnFax_Click);
            this.btnFax.MouseHover += new System.EventHandler(this.btnFax_MouseHover);
            // 
            // UCFaxDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlLayout);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(250, 100);
            this.Name = "UCFaxDetail";
            this.Size = new System.Drawing.Size(300, 179);
            this.pnlLayout.ResumeLayout(false);
            this.pnlLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController;
        private System.Windows.Forms.TableLayoutPanel pnlLayout;
        private System.Windows.Forms.Label lblSendTime;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblCC;
        private System.Windows.Forms.TextBox txtSendTime;
        private ICP.MailCenter.CommonUI.FaxViewer.EmailAddressTextBox txtTo;
        private ICP.MailCenter.CommonUI.FaxViewer.EmailAddressTextBox txtCC;
        private AttachmentPanel attachmentPanel;
        private ICP.MailCenter.CommonUI.FaxViewer.EmailAddressTextBox txtSender;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.TextBox txtSubject;
        private DevExpress.XtraEditors.SimpleButton btnFax;
    }
}

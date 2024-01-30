namespace ICP.OA.UI.FaxManage
{
    partial class UCFaxQuery
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
            this.pnlQuery = new BSE.Windows.Forms.Panel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labSearchDateTo = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.labSearchSubject = new DevExpress.XtraEditors.LabelControl();
            this.lwchkAttachment = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.txtMailTo = new DevExpress.XtraEditors.TextEdit();
            this.labSearchDateFrom = new DevExpress.XtraEditors.LabelControl();
            this.labSearchTo = new DevExpress.XtraEditors.LabelControl();
            this.txtMailFrom = new DevExpress.XtraEditors.TextEdit();
            this.labSearchFrom = new DevExpress.XtraEditors.LabelControl();
            this.chkDateTime = new DevExpress.XtraEditors.CheckEdit();
            this.pnlQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDateTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.BackColor = System.Drawing.Color.Transparent;
            this.pnlQuery.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(216)))), ((int)(((byte)(209)))));
            this.pnlQuery.CaptionFont = new System.Drawing.Font("宋体", 10.5F);
            this.pnlQuery.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlQuery.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlQuery.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlQuery.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(222)))), ((int)(((byte)(217)))));
            this.pnlQuery.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.pnlQuery.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(213)))), ((int)(((byte)(206)))));
            this.pnlQuery.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.pnlQuery.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.pnlQuery.Controls.Add(this.btnClear);
            this.pnlQuery.Controls.Add(this.btnQuery);
            this.pnlQuery.Controls.Add(this.dteTo);
            this.pnlQuery.Controls.Add(this.dteFrom);
            this.pnlQuery.Controls.Add(this.labSearchDateTo);
            this.pnlQuery.Controls.Add(this.txtSubject);
            this.pnlQuery.Controls.Add(this.labSearchSubject);
            this.pnlQuery.Controls.Add(this.lwchkAttachment);
            this.pnlQuery.Controls.Add(this.txtMailTo);
            this.pnlQuery.Controls.Add(this.labSearchDateFrom);
            this.pnlQuery.Controls.Add(this.labSearchTo);
            this.pnlQuery.Controls.Add(this.txtMailFrom);
            this.pnlQuery.Controls.Add(this.labSearchFrom);
            this.pnlQuery.Controls.Add(this.chkDateTime);
            this.pnlQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlQuery.Image = null;
            this.pnlQuery.InnerBorderColor = System.Drawing.Color.White;
            this.pnlQuery.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnlQuery.Location = new System.Drawing.Point(0, 0);
            this.pnlQuery.Name = "pnlQuery";
            this.pnlQuery.PanelStyle = BSE.Windows.Forms.PanelStyle.Default;
            this.pnlQuery.ShowExpandIcon = true;
            this.pnlQuery.ShowXPanderPanelProfessionalStyle = true;
            this.pnlQuery.Size = new System.Drawing.Size(299, 182);
            this.pnlQuery.TabIndex = 0;
            this.pnlQuery.Text = "收件箱";
            this.pnlQuery.PanelCollapsing += new System.EventHandler<BSE.Windows.Forms.XPanderStateChangeEventArgs>(this.pnlQuery_PanelCollapsing);
            this.pnlQuery.PanelExpanding += new System.EventHandler<BSE.Windows.Forms.XPanderStateChangeEventArgs>(this.pnlQuery_PanelExpanding);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(196, 149);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 94;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(49, 148);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 93;
            this.btnQuery.Text = "&Query";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dteTo
            // 
            this.dteTo.EditValue = null;
            this.dteTo.Enabled = false;
            this.dteTo.Location = new System.Drawing.Point(196, 121);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.Mask.EditMask = "";
            this.dteTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(92, 21);
            this.dteTo.TabIndex = 92;
            // 
            // dteFrom
            // 
            this.dteFrom.EditValue = null;
            this.dteFrom.Enabled = false;
            this.dteFrom.Location = new System.Drawing.Point(49, 120);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.Mask.EditMask = "";
            this.dteFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(90, 21);
            this.dteFrom.TabIndex = 91;
            // 
            // labSearchDateTo
            // 
            this.labSearchDateTo.Location = new System.Drawing.Point(152, 121);
            this.labSearchDateTo.Name = "labSearchDateTo";
            this.labSearchDateTo.Size = new System.Drawing.Size(15, 14);
            this.labSearchDateTo.TabIndex = 100;
            this.labSearchDateTo.Text = "To";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(50, 63);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(238, 21);
            this.txtSubject.TabIndex = 87;
            // 
            // labSearchSubject
            // 
            this.labSearchSubject.Location = new System.Drawing.Point(7, 64);
            this.labSearchSubject.Name = "labSearchSubject";
            this.labSearchSubject.Size = new System.Drawing.Size(42, 14);
            this.labSearchSubject.TabIndex = 95;
            this.labSearchSubject.Text = "Subject";
            // 
            // lwchkAttachment
            // 
            this.lwchkAttachment.Checked = null;
            this.lwchkAttachment.CheckedText = "TRUE";
            this.lwchkAttachment.Location = new System.Drawing.Point(50, 90);
            this.lwchkAttachment.Name = "lwchkAttachment";
            this.lwchkAttachment.NULLText = "ALL";
            this.lwchkAttachment.Size = new System.Drawing.Size(89, 24);
            this.lwchkAttachment.TabIndex = 89;
            this.lwchkAttachment.UnCheckedText = "FALSE";
            // 
            // txtMailTo
            // 
            this.txtMailTo.Location = new System.Drawing.Point(198, 37);
            this.txtMailTo.Name = "txtMailTo";
            this.txtMailTo.Size = new System.Drawing.Size(90, 21);
            this.txtMailTo.TabIndex = 86;
            // 
            // labSearchDateFrom
            // 
            this.labSearchDateFrom.Location = new System.Drawing.Point(9, 121);
            this.labSearchDateFrom.Name = "labSearchDateFrom";
            this.labSearchDateFrom.Size = new System.Drawing.Size(27, 14);
            this.labSearchDateFrom.TabIndex = 99;
            this.labSearchDateFrom.Text = "From";
            // 
            // labSearchTo
            // 
            this.labSearchTo.Location = new System.Drawing.Point(153, 40);
            this.labSearchTo.Name = "labSearchTo";
            this.labSearchTo.Size = new System.Drawing.Size(15, 14);
            this.labSearchTo.TabIndex = 96;
            this.labSearchTo.Text = "To";
            // 
            // txtMailFrom
            // 
            this.txtMailFrom.Location = new System.Drawing.Point(50, 37);
            this.txtMailFrom.Name = "txtMailFrom";
            this.txtMailFrom.Size = new System.Drawing.Size(90, 21);
            this.txtMailFrom.TabIndex = 85;
            // 
            // labSearchFrom
            // 
            this.labSearchFrom.Location = new System.Drawing.Point(7, 40);
            this.labSearchFrom.Name = "labSearchFrom";
            this.labSearchFrom.Size = new System.Drawing.Size(27, 14);
            this.labSearchFrom.TabIndex = 98;
            this.labSearchFrom.Text = "From";
            // 
            // chkDateTime
            // 
            this.chkDateTime.Location = new System.Drawing.Point(200, 90);
            this.chkDateTime.Name = "chkDateTime";
            this.chkDateTime.Properties.Caption = "Date";
            this.chkDateTime.Size = new System.Drawing.Size(72, 19);
            this.chkDateTime.TabIndex = 90;
            this.chkDateTime.Click += new System.EventHandler(this.chkDateTime_CheckedChanged);
            // 
            // UCFaxQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pnlQuery);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(299, 2);
            this.Name = "UCFaxQuery";
            this.Size = new System.Drawing.Size(299, 182);
            this.pnlQuery.ResumeLayout(false);
            this.pnlQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDateTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BSE.Windows.Forms.Panel pnlQuery;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labSearchDateTo;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl labSearchSubject;
        protected ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkAttachment;
        private DevExpress.XtraEditors.TextEdit txtMailTo;
        private DevExpress.XtraEditors.LabelControl labSearchDateFrom;
        private DevExpress.XtraEditors.LabelControl labSearchTo;
        private DevExpress.XtraEditors.TextEdit txtMailFrom;
        private DevExpress.XtraEditors.LabelControl labSearchFrom;
        private DevExpress.XtraEditors.CheckEdit chkDateTime;


    }
}

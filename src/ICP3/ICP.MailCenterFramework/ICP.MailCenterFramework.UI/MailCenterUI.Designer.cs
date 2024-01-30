namespace ICP.MailCenterFramework.UI
{
    partial class MailCenterUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailCenterUI));
            this.gvList = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOperationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOCopy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBLCopy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APCopy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANCopy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OceanBookingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsValid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactMail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnNewBusiness = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiBtnOE = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsddBtnAssociate = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiBtnAssociate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBtnAssociateAsCustomerMail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBtnAssociateAsCarrierMail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBtnAdvancedquery = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelButton = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvList
            // 
            this.gvList.AllowDrop = true;
            this.gvList.AllowUserToAddRows = false;
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.ColumnHeadersVisible = false;
            this.gvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.NO,
            this.BLNO,
            this.RefNO,
            this.Description,
            this.ColumnOperationType,
            this.ID,
            this.SOCopy,
            this.MBLCopy,
            this.APCopy,
            this.ANCopy,
            this.OceanBookingID,
            this.UpdateDate,
            this.IsValid,
            this.ContactMail,
            this.Column1});
            this.gvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvList.Location = new System.Drawing.Point(194, 0);
            this.gvList.Name = "gvList";
            this.gvList.RowHeadersVisible = false;
            this.gvList.RowTemplate.Height = 23;
            this.gvList.Size = new System.Drawing.Size(550, 40);
            this.gvList.TabIndex = 1;
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Width = 30;
            // 
            // NO
            // 
            this.NO.DataPropertyName = "NO";
            this.NO.HeaderText = "NO";
            this.NO.Name = "NO";
            this.NO.ReadOnly = true;
            this.NO.Width = 105;
            // 
            // BLNO
            // 
            this.BLNO.DataPropertyName = "BLNO";
            this.BLNO.HeaderText = "BLNO";
            this.BLNO.Name = "BLNO";
            this.BLNO.ReadOnly = true;
            this.BLNO.Width = 220;
            // 
            // RefNO
            // 
            this.RefNO.DataPropertyName = "RefNO";
            this.RefNO.HeaderText = "RefNO";
            this.RefNO.Name = "RefNO";
            this.RefNO.ReadOnly = true;
            this.RefNO.Width = 220;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // ColumnOperationType
            // 
            this.ColumnOperationType.DataPropertyName = "OperationType";
            this.ColumnOperationType.HeaderText = "OperationType";
            this.ColumnOperationType.Name = "ColumnOperationType";
            this.ColumnOperationType.Visible = false;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "OceanBookingID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // SOCopy
            // 
            this.SOCopy.DataPropertyName = "SOCopy";
            this.SOCopy.HeaderText = "SOCopy";
            this.SOCopy.Name = "SOCopy";
            this.SOCopy.Visible = false;
            // 
            // MBLCopy
            // 
            this.MBLCopy.DataPropertyName = "MBLCopy";
            this.MBLCopy.HeaderText = "MBLCopy";
            this.MBLCopy.Name = "MBLCopy";
            this.MBLCopy.Visible = false;
            // 
            // APCopy
            // 
            this.APCopy.DataPropertyName = "APCopy";
            this.APCopy.HeaderText = "APCopy";
            this.APCopy.Name = "APCopy";
            this.APCopy.Visible = false;
            // 
            // ANCopy
            // 
            this.ANCopy.DataPropertyName = "ANCopy";
            this.ANCopy.HeaderText = "ANCopy";
            this.ANCopy.Name = "ANCopy";
            this.ANCopy.Visible = false;
            // 
            // OceanBookingID
            // 
            this.OceanBookingID.DataPropertyName = "OceanBookingID";
            this.OceanBookingID.HeaderText = "OceanBookingID";
            this.OceanBookingID.Name = "OceanBookingID";
            this.OceanBookingID.Visible = false;
            // 
            // UpdateDate
            // 
            this.UpdateDate.DataPropertyName = "UpdateDate";
            this.UpdateDate.HeaderText = "UpdateDate";
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.Visible = false;
            // 
            // IsValid
            // 
            this.IsValid.DataPropertyName = "IsValid";
            this.IsValid.HeaderText = "IsValid";
            this.IsValid.Name = "IsValid";
            this.IsValid.Visible = false;
            // 
            // ContactMail
            // 
            this.ContactMail.DataPropertyName = "ContactMail";
            this.ContactMail.HeaderText = "ContactMail";
            this.ContactMail.Name = "ContactMail";
            this.ContactMail.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Selected";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Visible = false;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnNewBusiness,
            this.toolStripLabel3,
            this.tsddBtnAssociate,
            this.tsmiBtnAdvancedquery});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(194, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsBtnNewBusiness
            // 
            this.tsBtnNewBusiness.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnNewBusiness.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBtnOE});
            this.tsBtnNewBusiness.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNewBusiness.Image")));
            this.tsBtnNewBusiness.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNewBusiness.Name = "tsBtnNewBusiness";
            this.tsBtnNewBusiness.Size = new System.Drawing.Size(45, 22);
            this.tsBtnNewBusiness.Text = "新增";
            // 
            // tsmiBtnOE
            // 
            this.tsmiBtnOE.Name = "tsmiBtnOE";
            this.tsmiBtnOE.Size = new System.Drawing.Size(152, 22);
            this.tsmiBtnOE.Text = "海出订舱";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(12, 22);
            this.toolStripLabel3.Text = " ";
            // 
            // tsddBtnAssociate
            // 
            this.tsddBtnAssociate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddBtnAssociate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBtnAssociate,
            this.tsmiBtnAssociateAsCustomerMail,
            this.tsmiBtnAssociateAsCarrierMail});
            this.tsddBtnAssociate.Image = ((System.Drawing.Image)(resources.GetObject("tsddBtnAssociate.Image")));
            this.tsddBtnAssociate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddBtnAssociate.Name = "tsddBtnAssociate";
            this.tsddBtnAssociate.Size = new System.Drawing.Size(45, 22);
            this.tsddBtnAssociate.Text = "关联";
            this.tsddBtnAssociate.ToolTipText = "关联选中的业务";
            // 
            // tsmiBtnAssociate
            // 
            this.tsmiBtnAssociate.Name = "tsmiBtnAssociate";
            this.tsmiBtnAssociate.Size = new System.Drawing.Size(208, 22);
            this.tsmiBtnAssociate.Text = "关联";
            // 
            // tsmiBtnAssociateAsCustomerMail
            // 
            this.tsmiBtnAssociateAsCustomerMail.Name = "tsmiBtnAssociateAsCustomerMail";
            this.tsmiBtnAssociateAsCustomerMail.Size = new System.Drawing.Size(208, 22);
            this.tsmiBtnAssociateAsCustomerMail.Text = "关联并设置为客户邮件";
            // 
            // tsmiBtnAssociateAsCarrierMail
            // 
            this.tsmiBtnAssociateAsCarrierMail.Name = "tsmiBtnAssociateAsCarrierMail";
            this.tsmiBtnAssociateAsCarrierMail.Size = new System.Drawing.Size(208, 22);
            this.tsmiBtnAssociateAsCarrierMail.Text = "关联并设置为承运人邮件";
            // 
            // tsmiBtnAdvancedquery
            // 
            this.tsmiBtnAdvancedquery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiBtnAdvancedquery.Image = ((System.Drawing.Image)(resources.GetObject("tsmiBtnAdvancedquery.Image")));
            this.tsmiBtnAdvancedquery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiBtnAdvancedquery.Name = "tsmiBtnAdvancedquery";
            this.tsmiBtnAdvancedquery.Size = new System.Drawing.Size(36, 22);
            this.tsmiBtnAdvancedquery.Text = "查找";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.toolStrip);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButton.Location = new System.Drawing.Point(0, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(194, 40);
            this.panelButton.TabIndex = 2;
            // 
            // MailCenterUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gvList);
            this.Controls.Add(this.panelButton);
            this.Name = "MailCenterUI";
            this.Size = new System.Drawing.Size(744, 40);
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton tsBtnNewBusiness;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnOE;
        private System.Windows.Forms.ToolStripButton tsmiBtnAdvancedquery;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBLCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn APCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ANCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn OceanBookingID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactMail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.ToolStripDropDownButton tsddBtnAssociate;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnAssociateAsCustomerMail;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnAssociateAsCarrierMail;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnAssociate;
    }
}

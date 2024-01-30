namespace ICP.FAM.UI.Comm
{
    partial class FAMCustomerDescriptionPart
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
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.panelPopup = new DevExpress.XtraEditors.PanelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCustomerFax = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtCustomerTel = new DevExpress.XtraEditors.TextEdit();
            this.labCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerAddress = new DevExpress.XtraEditors.TextEdit();
            this.labCustomerAddress = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.labCustomerTel = new DevExpress.XtraEditors.LabelControl();
            this.labCustomerFax = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.popupContainerEdit1 = new ICP.FAM.UI.Comm.UnClosePopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelPopup)).BeginInit();
            this.panelPopup.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.panelPopup);
            this.popupContainerControl1.Location = new System.Drawing.Point(19, 27);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(246, 134);
            this.popupContainerControl1.TabIndex = 1;
            // 
            // panelPopup
            // 
            this.panelPopup.Controls.Add(this.panel2);
            this.panelPopup.Controls.Add(this.panel1);
            this.panelPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPopup.Location = new System.Drawing.Point(0, 0);
            this.panelPopup.Name = "panelPopup";
            this.panelPopup.Size = new System.Drawing.Size(246, 134);
            this.panelPopup.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtCustomerFax);
            this.panel2.Controls.Add(this.txtCustomerTel);
            this.panel2.Controls.Add(this.labCustomerName);
            this.panel2.Controls.Add(this.txtCustomerAddress);
            this.panel2.Controls.Add(this.labCustomerAddress);
            this.panel2.Controls.Add(this.txtCustomerName);
            this.panel2.Controls.Add(this.labCustomerTel);
            this.panel2.Controls.Add(this.labCustomerFax);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 101);
            this.panel2.TabIndex = 2;
            // 
            // txtCustomerFax
            // 
            this.txtCustomerFax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Fax", true));
            this.txtCustomerFax.Location = new System.Drawing.Point(66, 75);
            this.txtCustomerFax.Name = "txtCustomerFax";
            this.txtCustomerFax.Size = new System.Drawing.Size(170, 21);
            this.txtCustomerFax.TabIndex = 16;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.FAMCustomerDescription);
            // 
            // txtCustomerTel
            // 
            this.txtCustomerTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Tel", true));
            this.txtCustomerTel.Location = new System.Drawing.Point(66, 51);
            this.txtCustomerTel.Name = "txtCustomerTel";
            this.txtCustomerTel.Size = new System.Drawing.Size(170, 21);
            this.txtCustomerTel.TabIndex = 16;
            // 
            // labCustomerName
            // 
            this.labCustomerName.Location = new System.Drawing.Point(8, 6);
            this.labCustomerName.Name = "labCustomerName";
            this.labCustomerName.Size = new System.Drawing.Size(31, 14);
            this.labCustomerName.TabIndex = 1;
            this.labCustomerName.Text = "Name";
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Address", true));
            this.txtCustomerAddress.Location = new System.Drawing.Point(66, 27);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(170, 21);
            this.txtCustomerAddress.TabIndex = 16;
            // 
            // labCustomerAddress
            // 
            this.labCustomerAddress.Location = new System.Drawing.Point(8, 30);
            this.labCustomerAddress.Name = "labCustomerAddress";
            this.labCustomerAddress.Size = new System.Drawing.Size(43, 14);
            this.labCustomerAddress.TabIndex = 1;
            this.labCustomerAddress.Text = "Address";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Name", true));
            this.txtCustomerName.Location = new System.Drawing.Point(66, 3);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(170, 21);
            this.txtCustomerName.TabIndex = 16;
            // 
            // labCustomerTel
            // 
            this.labCustomerTel.Location = new System.Drawing.Point(8, 54);
            this.labCustomerTel.Name = "labCustomerTel";
            this.labCustomerTel.Size = new System.Drawing.Size(17, 14);
            this.labCustomerTel.TabIndex = 1;
            this.labCustomerTel.Text = "Tel";
            // 
            // labCustomerFax
            // 
            this.labCustomerFax.Location = new System.Drawing.Point(8, 78);
            this.labCustomerFax.Name = "labCustomerFax";
            this.labCustomerFax.Size = new System.Drawing.Size(18, 14);
            this.labCustomerFax.TabIndex = 1;
            this.labCustomerFax.Text = "Fax";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(2, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 29);
            this.panel1.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(161, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // popupContainerEdit1
            // 
            this.popupContainerEdit1.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEdit1.Name = "popupContainerEdit1";
            this.popupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit1.Properties.CloseOnLostFocus = false;
            this.popupContainerEdit1.Properties.CloseOnOuterMouseClick = false;
            this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;
            this.popupContainerEdit1.Properties.PopupSizeable = false;
            this.popupContainerEdit1.Properties.ShowPopupCloseButton = false;
            this.popupContainerEdit1.Size = new System.Drawing.Size(19, 21);
            this.popupContainerEdit1.TabIndex = 0;
            this.popupContainerEdit1.Visible = false;
            // 
            // FAMCustomerDescriptionPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.popupContainerEdit1);
            this.Name = "FAMCustomerDescriptionPart";
            this.Size = new System.Drawing.Size(21, 22);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelPopup)).EndInit();
            this.panelPopup.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UnClosePopupContainerEdit popupContainerEdit1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        protected DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.PanelControl panelPopup;
        private DevExpress.XtraEditors.TextEdit txtCustomerFax;
        private DevExpress.XtraEditors.TextEdit txtCustomerTel;
        private DevExpress.XtraEditors.LabelControl labCustomerName;
        private DevExpress.XtraEditors.TextEdit txtCustomerAddress;
        private DevExpress.XtraEditors.LabelControl labCustomerAddress;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl labCustomerTel;
        private DevExpress.XtraEditors.LabelControl labCustomerFax;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}

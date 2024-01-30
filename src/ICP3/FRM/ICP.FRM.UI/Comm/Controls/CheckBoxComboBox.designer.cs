namespace ICP.FRM.UI
{
    partial class CheckBoxComboBox
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
            this.ICPComBox = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.checBoxkList = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnCheck = new DevExpress.XtraEditors.SimpleButton();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ICPComBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checBoxkList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // ICPComBox
            // 
            this.ICPComBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ICPComBox.Location = new System.Drawing.Point(0, 0);
            this.ICPComBox.Name = "ICPComBox";
            this.ICPComBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ICPComBox.Properties.PopupControl = this.popupContainerControl1;
            this.ICPComBox.Properties.PopupSizeable = false;
            this.ICPComBox.Properties.ShowPopupCloseButton = false;
            this.ICPComBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.ICPComBox.Size = new System.Drawing.Size(137, 21);
            this.ICPComBox.TabIndex = 0;
            this.ICPComBox.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.ICPComBox_QueryPopUp);
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.popupContainerControl1.Controls.Add(this.checBoxkList);
            this.popupContainerControl1.Controls.Add(this.pnlBottom);
            this.popupContainerControl1.Location = new System.Drawing.Point(2, 23);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(132, 243);
            this.popupContainerControl1.TabIndex = 1;
            // 
            // checBoxkList
            // 
            this.checBoxkList.CheckOnClick = true;
            this.checBoxkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checBoxkList.Location = new System.Drawing.Point(0, 0);
            this.checBoxkList.Name = "checBoxkList";
            this.checBoxkList.Size = new System.Drawing.Size(132, 215);
            this.checBoxkList.TabIndex = 0;
            this.checBoxkList.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checBoxkList_ItemCheck);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Controls.Add(this.btnUnCheck);
            this.pnlBottom.Controls.Add(this.btnAll);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 215);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(132, 28);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(73, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnUnCheck
            // 
            this.btnUnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnCheck.Location = new System.Drawing.Point(8, 2);
            this.btnUnCheck.Name = "btnUnCheck";
            this.btnUnCheck.Size = new System.Drawing.Size(60, 23);
            this.btnUnCheck.TabIndex = 0;
            this.btnUnCheck.Text = "Un Check";
            this.btnUnCheck.Click += new System.EventHandler(this.btnUnCheck_Click);
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Location = new System.Drawing.Point(-56, 2);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(60, 23);
            this.btnAll.TabIndex = 0;
            this.btnAll.Text = "All Check";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // CheckBoxComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.ICPComBox);
            this.Name = "CheckBoxComboBox";
            this.Size = new System.Drawing.Size(137, 21);
            ((System.ComponentModel.ISupportInitialize)(this.ICPComBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checBoxkList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit ICPComBox;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl checBoxkList;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnAll;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnUnCheck;


    }
}

namespace ICP.FAM.UI.ReleaseBL
{
    partial class ContactListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactListPart));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.speTime = new DevExpress.XtraEditors.SpinEdit();
            this.labText2 = new DevExpress.XtraEditors.LabelControl();
            this.labText = new DevExpress.XtraEditors.LabelControl();
            this.labEmail = new DevExpress.XtraEditors.LabelControl();
            this.labtxt1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.listboxEmailAddress = new DevExpress.XtraEditors.ListBoxControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listboxEmailAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.speTime);
            this.panelControl1.Controls.Add(this.labText2);
            this.panelControl1.Controls.Add(this.labText);
            this.panelControl1.Controls.Add(this.labEmail);
            this.panelControl1.Controls.Add(this.labtxt1);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnRemove);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.listboxEmailAddress);
            this.panelControl1.Controls.Add(this.txtEmail);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(468, 240);
            this.panelControl1.TabIndex = 0;
            // 
            // speTime
            // 
            this.speTime.EditValue = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.speTime.Location = new System.Drawing.Point(97, 174);
            this.speTime.Name = "speTime";
            this.speTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speTime.Properties.IsFloatValue = false;
            this.speTime.Properties.Mask.EditMask = "N00";
            this.speTime.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.speTime.Size = new System.Drawing.Size(63, 21);
            this.speTime.TabIndex = 7;
            // 
            // labText2
            // 
            this.labText2.Location = new System.Drawing.Point(17, 177);
            this.labText2.Name = "labText2";
            this.labText2.Size = new System.Drawing.Size(84, 14);
            this.labText2.TabIndex = 6;
            this.labText2.Text = "期限（小时）：";
            // 
            // labText
            // 
            this.labText.Location = new System.Drawing.Point(17, 155);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(288, 14);
            this.labText.TabIndex = 6;
            this.labText.Text = "超过指定期限不签收放单时，发邮件提醒以上联系人。";
            // 
            // labEmail
            // 
            this.labEmail.Location = new System.Drawing.Point(17, 3);
            this.labEmail.Name = "labEmail";
            this.labEmail.Size = new System.Drawing.Size(60, 14);
            this.labEmail.TabIndex = 5;
            this.labEmail.Text = "联系人邮箱";
            // 
            // labtxt1
            // 
            this.labtxt1.Location = new System.Drawing.Point(17, 47);
            this.labtxt1.Name = "labtxt1";
            this.labtxt1.Size = new System.Drawing.Size(84, 14);
            this.labtxt1.TabIndex = 4;
            this.labtxt1.Text = "联系人邮箱列表";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(317, 122);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 27);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(317, 67);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(87, 27);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(317, 17);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 27);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listboxEmailAddress
            // 
            this.listboxEmailAddress.Location = new System.Drawing.Point(17, 67);
            this.listboxEmailAddress.Name = "listboxEmailAddress";
            this.listboxEmailAddress.Size = new System.Drawing.Size(283, 82);
            this.listboxEmailAddress.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(17, 23);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(283, 21);
            this.txtEmail.TabIndex = 0;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // ContactListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ContactListPart";
            this.Size = new System.Drawing.Size(468, 240);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listboxEmailAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labEmail;
        private DevExpress.XtraEditors.LabelControl labtxt1;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.ListBoxControl listboxEmailAddress;
        private DevExpress.XtraEditors.TextEdit txtEmail;
     
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labText2;
        private DevExpress.XtraEditors.LabelControl labText;
        private DevExpress.XtraEditors.SpinEdit speTime;

    }
}

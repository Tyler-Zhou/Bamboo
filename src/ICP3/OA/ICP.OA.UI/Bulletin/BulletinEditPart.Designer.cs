namespace ICP.OA.UI.Bulletin
{
    partial class BulletinEditPart
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulletinEditPart));
            this.labSubject = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labContent = new DevExpress.XtraEditors.LabelControl();
            this.labFromTime = new DevExpress.XtraEditors.LabelControl();
            this.dteFromTime = new DevExpress.XtraEditors.DateEdit();
            this.labToTime = new DevExpress.XtraEditors.LabelControl();
            this.dteToTime = new DevExpress.XtraEditors.DateEdit();
            this.labPriority = new DevExpress.XtraEditors.LabelControl();
            this.labBulletinType = new DevExpress.XtraEditors.LabelControl();
            this.labPublisher = new DevExpress.XtraEditors.LabelControl();
            this.txtPublisher = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labDepartment = new DevExpress.XtraEditors.LabelControl();
            this.cmbPriority = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbBulletinType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeCheckDep = new ICP.Framework.ClientComponents.Controls.TreeCheckControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublisher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPriority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBulletinType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labSubject
            // 
            this.labSubject.Location = new System.Drawing.Point(9, 24);
            this.labSubject.Name = "labSubject";
            this.labSubject.Size = new System.Drawing.Size(42, 14);
            this.labSubject.TabIndex = 4;
            this.labSubject.Text = "Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Subject", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSubject.Location = new System.Drawing.Point(82, 21);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(349, 21);
            this.txtSubject.TabIndex = 5;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.OA.ServiceInterface.DataObjects.BulletinData);
            // 
            // labContent
            // 
            this.labContent.Location = new System.Drawing.Point(9, 129);
            this.labContent.Name = "labContent";
            this.labContent.Size = new System.Drawing.Size(45, 14);
            this.labContent.TabIndex = 6;
            this.labContent.Text = "Content";
            // 
            // labFromTime
            // 
            this.labFromTime.Location = new System.Drawing.Point(9, 104);
            this.labFromTime.Name = "labFromTime";
            this.labFromTime.Size = new System.Drawing.Size(54, 14);
            this.labFromTime.TabIndex = 8;
            this.labFromTime.Text = "FromTime";
            // 
            // dteFromTime
            // 
            this.dteFromTime.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "FromTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteFromTime.EditValue = null;
            this.dteFromTime.Location = new System.Drawing.Point(82, 101);
            this.dteFromTime.Name = "dteFromTime";
            this.dteFromTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFromTime.Properties.Mask.EditMask = "";
            this.dteFromTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteFromTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFromTime.Size = new System.Drawing.Size(117, 21);
            this.dteFromTime.TabIndex = 9;
            // 
            // labToTime
            // 
            this.labToTime.Location = new System.Drawing.Point(241, 104);
            this.labToTime.Name = "labToTime";
            this.labToTime.Size = new System.Drawing.Size(42, 14);
            this.labToTime.TabIndex = 10;
            this.labToTime.Text = "ToTime";
            // 
            // dteToTime
            // 
            this.dteToTime.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ToTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteToTime.EditValue = null;
            this.dteToTime.Location = new System.Drawing.Point(314, 101);
            this.dteToTime.Name = "dteToTime";
            this.dteToTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteToTime.Properties.Mask.EditMask = "";
            this.dteToTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteToTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteToTime.Size = new System.Drawing.Size(117, 21);
            this.dteToTime.TabIndex = 11;
            // 
            // labPriority
            // 
            this.labPriority.Location = new System.Drawing.Point(9, 78);
            this.labPriority.Name = "labPriority";
            this.labPriority.Size = new System.Drawing.Size(37, 14);
            this.labPriority.TabIndex = 12;
            this.labPriority.Text = "Priority";
            // 
            // labBulletinType
            // 
            this.labBulletinType.Location = new System.Drawing.Point(241, 78);
            this.labBulletinType.Name = "labBulletinType";
            this.labBulletinType.Size = new System.Drawing.Size(67, 14);
            this.labBulletinType.TabIndex = 16;
            this.labBulletinType.Text = "BulletinType";
            // 
            // labPublisher
            // 
            this.labPublisher.Location = new System.Drawing.Point(9, 248);
            this.labPublisher.Name = "labPublisher";
            this.labPublisher.Size = new System.Drawing.Size(48, 14);
            this.labPublisher.TabIndex = 22;
            this.labPublisher.Text = "Publisher";
            // 
            // txtPublisher
            // 
            this.txtPublisher.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Publisher", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPublisher.Enabled = false;
            this.txtPublisher.Location = new System.Drawing.Point(82, 245);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.Size = new System.Drawing.Size(100, 21);
            this.txtPublisher.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(354, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(254, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labDepartment
            // 
            this.labDepartment.Location = new System.Drawing.Point(9, 52);
            this.labDepartment.Name = "labDepartment";
            this.labDepartment.Size = new System.Drawing.Size(66, 14);
            this.labDepartment.TabIndex = 30;
            this.labDepartment.Text = "Department";
            // 
            // cmbPriority
            // 
            this.cmbPriority.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Priority", true));
            this.cmbPriority.Location = new System.Drawing.Point(82, 75);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPriority.Size = new System.Drawing.Size(117, 21);
            this.cmbPriority.TabIndex = 32;
            // 
            // cmbBulletinType
            // 
            this.cmbBulletinType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "BulletinType", true));
            this.cmbBulletinType.Location = new System.Drawing.Point(314, 75);
            this.cmbBulletinType.Name = "cmbBulletinType";
            this.cmbBulletinType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBulletinType.Size = new System.Drawing.Size(117, 21);
            this.cmbBulletinType.TabIndex = 32;
            // 
            // txtContent
            // 
            this.txtContent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Content", true));
            this.txtContent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Content", true));
            this.txtContent.Location = new System.Drawing.Point(82, 128);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(349, 111);
            this.txtContent.TabIndex = 7;
            this.txtContent.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeCheckDep);
            this.groupBox1.Controls.Add(this.txtSubject);
            this.groupBox1.Controls.Add(this.cmbBulletinType);
            this.groupBox1.Controls.Add(this.txtContent);
            this.groupBox1.Controls.Add(this.labPublisher);
            this.groupBox1.Controls.Add(this.cmbPriority);
            this.groupBox1.Controls.Add(this.txtPublisher);
            this.groupBox1.Controls.Add(this.labBulletinType);
            this.groupBox1.Controls.Add(this.labPriority);
            this.groupBox1.Controls.Add(this.labDepartment);
            this.groupBox1.Controls.Add(this.dteToTime);
            this.groupBox1.Controls.Add(this.labToTime);
            this.groupBox1.Controls.Add(this.dteFromTime);
            this.groupBox1.Controls.Add(this.labSubject);
            this.groupBox1.Controls.Add(this.labFromTime);
            this.groupBox1.Controls.Add(this.labContent);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 274);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            // 
            // treeCheckDep
            // 
            this.treeCheckDep.EditText = "";
            this.treeCheckDep.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("treeCheckDep.EditValue")));
            this.treeCheckDep.Location = new System.Drawing.Point(82, 48);
            this.treeCheckDep.Name = "treeCheckDep";
            this.treeCheckDep.ReadOnly = false;
            this.treeCheckDep.Size = new System.Drawing.Size(349, 21);
            this.treeCheckDep.SplitString = ";";
            this.treeCheckDep.TabIndex = 33;
            // 
            // BulletinEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "BulletinEditPart";
            this.Size = new System.Drawing.Size(446, 321);
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFromTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteToTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublisher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPriority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBulletinType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.LabelControl labSubject;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl labContent;
        private DevExpress.XtraEditors.LabelControl labFromTime;
        private DevExpress.XtraEditors.DateEdit dteFromTime;
        private DevExpress.XtraEditors.LabelControl labToTime;
        private DevExpress.XtraEditors.DateEdit dteToTime;
        private DevExpress.XtraEditors.LabelControl labPriority;
        private DevExpress.XtraEditors.LabelControl labBulletinType;
        private DevExpress.XtraEditors.LabelControl labPublisher;
        private DevExpress.XtraEditors.TextEdit txtPublisher;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labDepartment;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbPriority;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBulletinType;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private System.Windows.Forms.GroupBox groupBox1;
        private ICP.Framework.ClientComponents.Controls.TreeCheckControl treeCheckDep;
    }

}

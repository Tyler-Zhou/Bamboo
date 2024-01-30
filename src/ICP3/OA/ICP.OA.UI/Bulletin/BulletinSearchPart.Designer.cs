namespace ICP.OA.UI.Bulletin
{
    partial class BulletinSearchPart
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClean = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labPublisher = new DevExpress.XtraEditors.LabelControl();
            this.labSubject = new DevExpress.XtraEditors.LabelControl();
            this.labBody = new DevExpress.XtraEditors.LabelControl();
            this.labDepartment = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.txtBody = new DevExpress.XtraEditors.TextEdit();
            this.txtDepartment = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.nbarDuration = new DevExpress.XtraNavBar.NavBarGroup();
            this.cmbPublisher = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBody.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClean);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 555);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 59);
            this.panel1.TabIndex = 2;
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(35, 15);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 1;
            this.btnClean.Text = "C&lean";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(136, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Sea&rch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDuration});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(230, 614);
            this.navBarControl1.TabIndex = 1;
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 195;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.cmbPublisher);
            this.navBarGroupBase.Controls.Add(this.labPublisher);
            this.navBarGroupBase.Controls.Add(this.labSubject);
            this.navBarGroupBase.Controls.Add(this.labBody);
            this.navBarGroupBase.Controls.Add(this.labDepartment);
            this.navBarGroupBase.Controls.Add(this.txtSubject);
            this.navBarGroupBase.Controls.Add(this.txtBody);
            this.navBarGroupBase.Controls.Add(this.txtDepartment);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(206, 193);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // labPublisher
            // 
            this.labPublisher.Location = new System.Drawing.Point(7, 147);
            this.labPublisher.Name = "labPublisher";
            this.labPublisher.Size = new System.Drawing.Size(48, 14);
            this.labPublisher.TabIndex = 15;
            this.labPublisher.Text = "Publisher";
            // 
            // labSubject
            // 
            this.labSubject.Location = new System.Drawing.Point(5, 6);
            this.labSubject.Name = "labSubject";
            this.labSubject.Size = new System.Drawing.Size(42, 14);
            this.labSubject.TabIndex = 13;
            this.labSubject.Text = "Subject";
            // 
            // labBody
            // 
            this.labBody.Location = new System.Drawing.Point(5, 53);
            this.labBody.Name = "labBody";
            this.labBody.Size = new System.Drawing.Size(27, 14);
            this.labBody.TabIndex = 13;
            this.labBody.Text = "Body";
            // 
            // labDepartment
            // 
            this.labDepartment.Location = new System.Drawing.Point(5, 100);
            this.labDepartment.Name = "labDepartment";
            this.labDepartment.Size = new System.Drawing.Size(66, 14);
            this.labDepartment.TabIndex = 14;
            this.labDepartment.Text = "Department";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(5, 26);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(194, 21);
            this.txtSubject.TabIndex = 0;
            // 
            // txtBody
            // 
            this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBody.Location = new System.Drawing.Point(5, 73);
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(194, 21);
            this.txtBody.TabIndex = 1;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepartment.Location = new System.Drawing.Point(5, 120);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(194, 21);
            this.txtDepartment.TabIndex = 2;
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.radioGroup1);
            this.navBarGroupControlContainer1.Controls.Add(this.dteTo);
            this.navBarGroupControlContainer1.Controls.Add(this.dteFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.labTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(206, 156);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioGroup1.Location = new System.Drawing.Point(66, 3);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Un known"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Last Month"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "This Month"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Specify")});
            this.radioGroup1.Size = new System.Drawing.Size(133, 89);
            this.radioGroup1.TabIndex = 15;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = null;
            this.dteTo.Enabled = false;
            this.dteTo.Location = new System.Drawing.Point(66, 125);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(133, 21);
            this.dteTo.TabIndex = 2;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = null;
            this.dteFrom.Enabled = false;
            this.dteFrom.Location = new System.Drawing.Point(66, 98);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(133, 21);
            this.dteFrom.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(5, 128);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 12;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(5, 101);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 12;
            this.labFrom.Text = "From";
            // 
            // nbarDuration
            // 
            this.nbarDuration.Caption = "Duration";
            this.nbarDuration.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarDuration.Expanded = true;
            this.nbarDuration.GroupClientHeight = 158;
            this.nbarDuration.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDuration.Name = "nbarDuration";
            // 
            // cmbPublisher
            // 
            this.cmbPublisher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPublisher.EditText = "";
            this.cmbPublisher.EditValue = null;
            this.cmbPublisher.Location = new System.Drawing.Point(7, 166);
            this.cmbPublisher.Name = "cmbPublisher";
            this.cmbPublisher.ReadOnly = false;
            this.cmbPublisher.RefreshButtonToolTip = "";
            this.cmbPublisher.ShowRefreshButton = false;
            this.cmbPublisher.Size = new System.Drawing.Size(192, 21);
            this.cmbPublisher.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPublisher.TabIndex = 3;
            this.cmbPublisher.ToolTip = "";
            // 
            // BulletinSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.navBarControl1);
            this.Name = "BulletinSearchPart";
            this.Size = new System.Drawing.Size(230, 614);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBody.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnClean;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.LabelControl labSubject;
        private DevExpress.XtraEditors.LabelControl labBody;
        private DevExpress.XtraEditors.LabelControl labDepartment;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.TextEdit txtBody;
        private DevExpress.XtraEditors.TextEdit txtDepartment;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraNavBar.NavBarGroup nbarDuration;
        public DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labPublisher;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox cmbPublisher;

    }
}

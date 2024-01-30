namespace ICP.FAM.UI
{
    partial class JournalSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalSearchPart));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClare = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lblCopany = new DevExpress.XtraEditors.LabelControl();
            this.chcCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.gpCompany = new DevExpress.XtraEditors.GroupControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.lblRecord = new DevExpress.XtraEditors.LabelControl();
            this.cheRecord = new DevExpress.XtraEditors.SpinEdit();
            this.lblIsV = new DevExpress.XtraEditors.LabelControl();
            this.gpOthers = new DevExpress.XtraEditors.GroupControl();
            this.ckbValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.cheAmounts = new DevExpress.XtraEditors.CheckEdit();
            this.lblCR = new DevExpress.XtraEditors.LabelControl();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.grpAmount = new System.Windows.Forms.GroupBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.dmdDate = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblDR = new DevExpress.XtraEditors.LabelControl();
            this.numMin = new DevExpress.XtraEditors.SpinEdit();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarTitle = new DevExpress.XtraNavBar.NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpCompany)).BeginInit();
            this.gpCompany.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheRecord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpOthers)).BeginInit();
            this.gpOthers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cheAmounts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            this.grpAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClare);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 585);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(206, 37);
            this.panelControl1.TabIndex = 4;
            // 
            // btnClare
            // 
            this.btnClare.Location = new System.Drawing.Point(22, 10);
            this.btnClare.Name = "btnClare";
            this.btnClare.Size = new System.Drawing.Size(75, 23);
            this.btnClare.TabIndex = 1;
            this.btnClare.Text = "清空(&L)";
            this.btnClare.Click += new System.EventHandler(this.btnClare_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(113, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCopany
            // 
            this.lblCopany.Location = new System.Drawing.Point(10, 35);
            this.lblCopany.Name = "lblCopany";
            this.lblCopany.Size = new System.Drawing.Size(24, 14);
            this.lblCopany.TabIndex = 3;
            this.lblCopany.Text = "公司";
            // 
            // chcCompany
            // 
            this.chcCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chcCompany.EditValue = "";
            this.chcCompany.EnterMoveNextControl = true;
            this.chcCompany.Location = new System.Drawing.Point(74, 32);
            this.chcCompany.Name = "chcCompany";
            this.chcCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcCompany.Size = new System.Drawing.Size(114, 21);
            this.chcCompany.TabIndex = 0;
            // 
            // gpCompany
            // 
            this.gpCompany.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.gpCompany.Controls.Add(this.txtNo);
            this.gpCompany.Controls.Add(this.labNo);
            this.gpCompany.Controls.Add(this.chcCompany);
            this.gpCompany.Controls.Add(this.lblCopany);
            this.gpCompany.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpCompany.Location = new System.Drawing.Point(0, 0);
            this.gpCompany.Name = "gpCompany";
            this.gpCompany.Size = new System.Drawing.Size(206, 89);
            this.gpCompany.TabIndex = 0;
            this.gpCompany.Text = "基础信息";
            // 
            // txtNo
            // 
            this.txtNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNo.Location = new System.Drawing.Point(74, 57);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(114, 21);
            this.txtNo.TabIndex = 4;
            this.txtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNo_KeyDown);
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(10, 59);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(24, 14);
            this.labNo.TabIndex = 5;
            this.labNo.Text = "单号";
            // 
            // lblRecord
            // 
            this.lblRecord.Location = new System.Drawing.Point(10, 62);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(48, 14);
            this.lblRecord.TabIndex = 12;
            this.lblRecord.Text = "每页行数";
            // 
            // cheRecord
            // 
            this.cheRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cheRecord.EditValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.cheRecord.Location = new System.Drawing.Point(74, 59);
            this.cheRecord.Name = "cheRecord";
            this.cheRecord.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cheRecord.Properties.Mask.EditMask = "d";
            this.cheRecord.Size = new System.Drawing.Size(116, 21);
            this.cheRecord.TabIndex = 1;
            // 
            // lblIsV
            // 
            this.lblIsV.Location = new System.Drawing.Point(10, 32);
            this.lblIsV.Name = "lblIsV";
            this.lblIsV.Size = new System.Drawing.Size(36, 14);
            this.lblIsV.TabIndex = 14;
            this.lblIsV.Text = "有效性";
            // 
            // gpOthers
            // 
            this.gpOthers.Controls.Add(this.ckbValid);
            this.gpOthers.Controls.Add(this.lblIsV);
            this.gpOthers.Controls.Add(this.cheRecord);
            this.gpOthers.Controls.Add(this.lblRecord);
            this.gpOthers.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpOthers.Location = new System.Drawing.Point(0, 401);
            this.gpOthers.Name = "gpOthers";
            this.gpOthers.Size = new System.Drawing.Size(206, 119);
            this.gpOthers.TabIndex = 3;
            this.gpOthers.Text = "其他";
            // 
            // ckbValid
            // 
            this.ckbValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbValid.Checked = true;
            this.ckbValid.CheckedText = "TRUE";
            this.ckbValid.Location = new System.Drawing.Point(72, 28);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.NULLText = "ALL";
            this.ckbValid.Size = new System.Drawing.Size(118, 23);
            this.ckbValid.TabIndex = 0;
            this.ckbValid.UnCheckedText = "FALSE";
            // 
            // cheAmounts
            // 
            this.cheAmounts.Location = new System.Drawing.Point(6, 205);
            this.cheAmounts.Name = "cheAmounts";
            this.cheAmounts.Properties.Caption = "金额";
            this.cheAmounts.Size = new System.Drawing.Size(93, 19);
            this.cheAmounts.TabIndex = 1;
            // 
            // lblCR
            // 
            this.lblCR.Location = new System.Drawing.Point(10, 265);
            this.lblCR.Name = "lblCR";
            this.lblCR.Size = new System.Drawing.Size(36, 14);
            this.lblCR.TabIndex = 4;
            this.lblCR.Text = "最大值";
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMax.Enabled = false;
            this.numMax.EnterMoveNextControl = true;
            this.numMax.Location = new System.Drawing.Point(72, 262);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMax.Properties.Mask.EditMask = "n";
            this.numMax.Size = new System.Drawing.Size(116, 21);
            this.numMax.TabIndex = 1;
            // 
            // grpAmount
            // 
            this.grpAmount.Controls.Add(this.groupControl2);
            this.grpAmount.Controls.Add(this.groupControl1);
            this.grpAmount.Controls.Add(this.numMax);
            this.grpAmount.Controls.Add(this.lblCR);
            this.grpAmount.Controls.Add(this.lblDR);
            this.grpAmount.Controls.Add(this.cheAmounts);
            this.grpAmount.Controls.Add(this.numMin);
            this.grpAmount.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAmount.Location = new System.Drawing.Point(0, 89);
            this.grpAmount.Name = "grpAmount";
            this.grpAmount.Size = new System.Drawing.Size(206, 312);
            this.grpAmount.TabIndex = 2;
            this.grpAmount.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.dmdDate);
            this.groupControl2.Controls.Add(this.labTo);
            this.groupControl2.Controls.Add(this.labFrom);
            this.groupControl2.Location = new System.Drawing.Point(1, 11);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(200, 188);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.Text = "创建时间";
            // 
            // dmdDate
            // 
            this.dmdDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dmdDate.From = null;
            this.dmdDate.IsEngish = false;
            this.dmdDate.Location = new System.Drawing.Point(70, 32);
            this.dmdDate.Name = "dmdDate";
            this.dmdDate.Size = new System.Drawing.Size(119, 142);
            this.dmdDate.TabIndex = 44;
            this.dmdDate.To = null;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(7, 148);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 43;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(9, 124);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 42;
            this.labFrom.Text = "From";
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(61, -42);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(89, 42);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "groupControl1";
            // 
            // lblDR
            // 
            this.lblDR.Location = new System.Drawing.Point(10, 236);
            this.lblDR.Name = "lblDR";
            this.lblDR.Size = new System.Drawing.Size(36, 14);
            this.lblDR.TabIndex = 3;
            this.lblDR.Text = "最小值";
            // 
            // numMin
            // 
            this.numMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMin.Enabled = false;
            this.numMin.Location = new System.Drawing.Point(72, 233);
            this.numMin.Name = "numMin";
            this.numMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMin.Properties.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMin.Properties.Mask.EditMask = "n";
            this.numMin.Size = new System.Drawing.Size(116, 21);
            this.numMin.TabIndex = 0;
            this.numMin.TabStop = false;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "BaseInfo";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarTitle
            // 
            this.navBarTitle.ActiveGroup = null;
            this.navBarTitle.Location = new System.Drawing.Point(0, 0);
            this.navBarTitle.Name = "navBarTitle";
            this.navBarTitle.OptionsNavPane.ExpandedWidth = 140;
            this.navBarTitle.Size = new System.Drawing.Size(140, 300);
            this.navBarTitle.TabIndex = 0;
            // 
            // JournalSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gpOthers);
            this.Controls.Add(this.grpAmount);
            this.Controls.Add(this.gpCompany);
            this.Name = "JournalSearchPart";
            this.Size = new System.Drawing.Size(206, 622);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chcCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpCompany)).EndInit();
            this.gpCompany.ResumeLayout(false);
            this.gpCompany.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheRecord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpOthers)).EndInit();
            this.gpOthers.ResumeLayout(false);
            this.gpOthers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cheAmounts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            this.grpAmount.ResumeLayout(false);
            this.grpAmount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;



        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl gpOthers;
        private DevExpress.XtraEditors.LabelControl lblIsV;
        private DevExpress.XtraEditors.SpinEdit cheRecord;
        private DevExpress.XtraEditors.LabelControl lblRecord;
        private DevExpress.XtraEditors.GroupControl gpCompany;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcCompany;
        private DevExpress.XtraEditors.LabelControl lblCopany;
        private System.Windows.Forms.GroupBox grpAmount;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl lblCR;
        private DevExpress.XtraEditors.LabelControl lblDR;
        private DevExpress.XtraEditors.CheckEdit cheAmounts;
        private DevExpress.XtraNavBar.NavBarControl navBarTitle;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraEditors.SpinEdit numMin;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton ckbValid;
        private DevExpress.XtraEditors.SimpleButton btnClare;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dmdDate;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labNo;

    }
}

namespace ICP.Common.UI.Configure.ChargingCode
{
    partial class ChargingCodeSearchPart
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.popGroup = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeListGroup = new DevExpress.XtraTreeList.TreeList();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsChargingGroup = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClosePop = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearPop = new DevExpress.XtraEditors.SimpleButton();
            this.lwchkIsCommission = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsCommission = new DevExpress.XtraEditors.LabelControl();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labGroup = new DevExpress.XtraEditors.LabelControl();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargingGroup)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 374);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(200, 59);
            this.panelControl1.TabIndex = 6;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(22, 18);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(106, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Sea&rch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(200, 374);
            this.panelControl2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.navBarControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 370);
            this.panel1.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(196, 250);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 198;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.popGroup);
            this.navBarGroupBase.Controls.Add(this.popupContainerControl1);
            this.navBarGroupBase.Controls.Add(this.lwchkIsCommission);
            this.navBarGroupBase.Controls.Add(this.labIsCommission);
            this.navBarGroupBase.Controls.Add(this.lwchkIsValid);
            this.navBarGroupBase.Controls.Add(this.labGroup);
            this.navBarGroupBase.Controls.Add(this.labIsValid);
            this.navBarGroupBase.Controls.Add(this.numMax);
            this.navBarGroupBase.Controls.Add(this.labMax);
            this.navBarGroupBase.Controls.Add(this.labName);
            this.navBarGroupBase.Controls.Add(this.labCode);
            this.navBarGroupBase.Controls.Add(this.txtName);
            this.navBarGroupBase.Controls.Add(this.txtCode);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(172, 196);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // popGroup
            // 
            this.popGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.popGroup.Location = new System.Drawing.Point(82, 114);
            this.popGroup.Name = "popGroup";
            this.popGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popGroup.Properties.PopupControl = this.popupContainerControl1;
            this.popGroup.Properties.PopupSizeable = false;
            this.popGroup.Properties.ShowPopupCloseButton = false;
            this.popGroup.Size = new System.Drawing.Size(87, 21);
            this.popGroup.TabIndex = 9;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.treeListGroup);
            this.popupContainerControl1.Controls.Add(this.panel2);
            this.popupContainerControl1.Location = new System.Drawing.Point(24, 168);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(199, 197);
            this.popupContainerControl1.TabIndex = 10;
            // 
            // treeListGroup
            // 
            this.treeListGroup.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCName,
            this.colEName});
            this.treeListGroup.DataSource = this.bsChargingGroup;
            this.treeListGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListGroup.Location = new System.Drawing.Point(0, 0);
            this.treeListGroup.Name = "treeListGroup";
            this.treeListGroup.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeListGroup.OptionsView.ShowIndicator = false;
            this.treeListGroup.Size = new System.Drawing.Size(199, 165);
            this.treeListGroup.TabIndex = 9;
            this.treeListGroup.DoubleClick += new System.EventHandler(this.treeListGroup_DoubleClick);
            // 
            // colCName
            // 
            this.colCName.Caption = "名称";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.OptionsColumn.AllowEdit = false;
            this.colCName.Width = 20;
            // 
            // colEName
            // 
            this.colEName.Caption = "Name";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.OptionsColumn.AllowEdit = false;
            this.colEName.Width = 20;
            // 
            // bsChargingGroup
            // 
            this.bsChargingGroup.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ChargingGroupList);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClosePop);
            this.panel2.Controls.Add(this.btnClearPop);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 165);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(199, 32);
            this.panel2.TabIndex = 1;
            // 
            // btnClosePop
            // 
            this.btnClosePop.Location = new System.Drawing.Point(122, 5);
            this.btnClosePop.Name = "btnClosePop";
            this.btnClosePop.Size = new System.Drawing.Size(75, 23);
            this.btnClosePop.TabIndex = 0;
            this.btnClosePop.Text = "Close";
            this.btnClosePop.Click += new System.EventHandler(this.btnClosePop_Click);
            // 
            // btnClearPop
            // 
            this.btnClearPop.Location = new System.Drawing.Point(41, 5);
            this.btnClearPop.Name = "btnClearPop";
            this.btnClearPop.Size = new System.Drawing.Size(75, 23);
            this.btnClearPop.TabIndex = 0;
            this.btnClearPop.Text = "Clear";
            this.btnClearPop.Click += new System.EventHandler(this.btnClearPop_Click);
            // 
            // lwchkIsCommission
            // 
            this.lwchkIsCommission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsCommission.Checked = null;
            this.lwchkIsCommission.CheckedText = "True";
            this.lwchkIsCommission.Location = new System.Drawing.Point(82, 58);
            this.lwchkIsCommission.Name = "lwchkIsCommission";
            this.lwchkIsCommission.NULLText = "ALL";
            this.lwchkIsCommission.Size = new System.Drawing.Size(87, 21);
            this.lwchkIsCommission.TabIndex = 5;
            this.lwchkIsCommission.UnCheckedText = "False";
            // 
            // labIsCommission
            // 
            this.labIsCommission.Location = new System.Drawing.Point(5, 61);
            this.labIsCommission.Name = "labIsCommission";
            this.labIsCommission.Size = new System.Drawing.Size(71, 14);
            this.labIsCommission.TabIndex = 4;
            this.labIsCommission.Text = "IsCommission";
            // 
            // lwchkIsValid
            // 
            this.lwchkIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Location = new System.Drawing.Point(82, 85);
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.Size = new System.Drawing.Size(87, 21);
            this.lwchkIsValid.TabIndex = 5;
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // labGroup
            // 
            this.labGroup.Location = new System.Drawing.Point(5, 117);
            this.labGroup.Name = "labGroup";
            this.labGroup.Size = new System.Drawing.Size(33, 14);
            this.labGroup.TabIndex = 4;
            this.labGroup.Text = "Group";
            // 
            // labIsValid
            // 
            this.labIsValid.Location = new System.Drawing.Point(5, 86);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(34, 14);
            this.labIsValid.TabIndex = 4;
            this.labIsValid.Text = "IsValid";
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(82, 141);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = "N00";
            this.numMax.Size = new System.Drawing.Size(87, 21);
            this.numMax.TabIndex = 3;
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(5, 144);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(58, 14);
            this.labMax.TabIndex = 1;
            this.labMax.Text = "Max Count";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(5, 34);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 1;
            this.labName.Text = "Name";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(5, 9);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 1;
            this.labCode.Text = "Code";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(82, 31);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(87, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Location = new System.Drawing.Point(82, 6);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(87, 21);
            this.txtCode.TabIndex = 0;
            // 
            // ChargingCodeSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "ChargingCodeSearchPart";
            this.Size = new System.Drawing.Size(200, 433);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChargingGroup)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsCommission;
        private DevExpress.XtraEditors.LabelControl labIsCommission;
        private DevExpress.XtraEditors.LabelControl labGroup;
        private DevExpress.XtraEditors.PopupContainerEdit popGroup;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnClosePop;
        private DevExpress.XtraEditors.SimpleButton btnClearPop;
        private DevExpress.XtraTreeList.TreeList treeListGroup;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private System.Windows.Forms.BindingSource bsChargingGroup;
    }
}

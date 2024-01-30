namespace ICP.WF.Activities
{
	partial class ConditionBrowserDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionBrowserDialog));
            DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem8 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip9 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem9 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip10 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem10 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip11 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem11 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip12 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem12 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip13 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem13 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip14 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem14 = new DevExpress.Utils.ToolTipTitleItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeViewRule = new DevExpress.XtraTreeList.TreeList();
            this.tcNodes = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.ucUserList = new ICP.WF.Activities.UCUserList();
            this.ucJobList = new ICP.WF.Activities.UCJobList();
            this.ucFormRule = new ICP.WF.Activities.UCFormRule();
            this.ucDepartment = new ICP.WF.Activities.UCDepartment();
            this.ucOrganization = new ICP.WF.Activities.UCOrganization();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barNewAnd = new DevExpress.XtraBars.BarButtonItem();
            this.barAddOr = new DevExpress.XtraBars.BarButtonItem();
            this.barNewUserRule = new DevExpress.XtraBars.BarButtonItem();
            this.barNewJobRule = new DevExpress.XtraBars.BarButtonItem();
            this.barNewDepartRule = new DevExpress.XtraBars.BarButtonItem();
            this.btnNewOrganizationRule = new DevExpress.XtraBars.BarButtonItem();
            this.barNewFormExpression = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barNewNotRule = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewRule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeViewRule);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlMain);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.SplitterPosition = 153;
            // 
            // treeViewRule
            // 
            this.treeViewRule.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcNodes});
            resources.ApplyResources(this.treeViewRule, "treeViewRule");
            this.treeViewRule.Name = "treeViewRule";
            this.treeViewRule.OptionsBehavior.Editable = false;
            this.treeViewRule.OptionsView.ShowColumns = false;
            this.treeViewRule.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeViewRule_AfterFocusNode);
            this.treeViewRule.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeViewRule_FocusedNodeChanged);
            // 
            // tcNodes
            // 
            this.tcNodes.FieldName = "Text";
            this.tcNodes.Name = "tcNodes";
            resources.ApplyResources(this.tcNodes, "tcNodes");
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.ucUserList);
            this.pnlMain.Controls.Add(this.ucJobList);
            this.pnlMain.Controls.Add(this.ucFormRule);
            this.pnlMain.Controls.Add(this.ucDepartment);
            this.pnlMain.Controls.Add(this.ucOrganization);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // ucUserList
            // 
            resources.ApplyResources(this.ucUserList, "ucUserList");
            this.ucUserList.FormDataList = null;
            this.ucUserList.Name = "ucUserList";
            this.ucUserList.serviceProvider = null;
            this.ucUserList.treeNode = null;
            // 
            // ucJobList
            // 
            resources.ApplyResources(this.ucJobList, "ucJobList");
            this.ucJobList.FormDataList = null;
            this.ucJobList.Name = "ucJobList";
            this.ucJobList.serviceProvider = null;
            this.ucJobList.treeNode = null;
            // 
            // ucFormRule
            // 
            resources.ApplyResources(this.ucFormRule, "ucFormRule");
            this.ucFormRule.Name = "ucFormRule";
            this.ucFormRule.GetFormExpressionText += new ICP.WF.Activities.ValueTextChang(this.ucFormRule_GetFormExpressionText);
            this.ucFormRule.GetValueText += new ICP.WF.Activities.ValueTextChang(this.ucFormRule_GetValueText);
            this.ucFormRule.GetOperatorText += new ICP.WF.Activities.OperatorTextChang(this.ucFormRule_GetOperatorText);
            // 
            // ucDepartment
            // 
            resources.ApplyResources(this.ucDepartment, "ucDepartment");
            this.ucDepartment.FormDataList = null;
            this.ucDepartment.Name = "ucDepartment";
            this.ucDepartment.serviceProvider = null;
            this.ucDepartment.treeNode = null;
            this.ucDepartment.GetRelationsText += new ICP.WF.Activities.RelationsTextChang(this.ucDepartment_GetRelationsText);
            // 
            // ucOrganization
            // 
            this.ucOrganization.FormDataList = null;
            resources.ApplyResources(this.ucOrganization, "ucOrganization");
            this.ucOrganization.Name = "ucOrganization";
            this.ucOrganization.serviceProvider = null;
            this.ucOrganization.treeNode = null;
            this.ucOrganization.GetOperatorText +=new UCOrganization.OperatorTextChang(ucOrganization_GetOperatorText);
            this.ucOrganization.GetFormExpressionText += new UCOrganization.FormExpressionTextChang(ucOrganization_GetFormExpressionText);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Controls.Add(this.btnCancel);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barNewAnd,
            this.barAddOr,
            this.barNewNotRule,
            this.barNewUserRule,
            this.barNewJobRule,
            this.barNewDepartRule,
            this.barNewFormExpression,
            this.barDelete,
            this.btnNewOrganizationRule});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.KeyTip))), this.barNewAnd, "", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard, "000.000", ""),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddOr, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewUserRule, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewJobRule, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewDepartRule, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNewOrganizationRule),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNewFormExpression, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.Standard)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar2, "bar2");
            // 
            // barNewAnd
            // 
            resources.ApplyResources(this.barNewAnd, "barNewAnd");
            this.barNewAnd.Glyph = global::ICP.WF.Activities.Properties.Resources.and;
            this.barNewAnd.Id = 0;
            this.barNewAnd.Name = "barNewAnd";
            resources.ApplyResources(toolTipTitleItem8, "toolTipTitleItem8");
            superToolTip8.Items.Add(toolTipTitleItem8);
            this.barNewAnd.SuperTip = superToolTip8;
            this.barNewAnd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewAnd_Click);
            // 
            // barAddOr
            // 
            resources.ApplyResources(this.barAddOr, "barAddOr");
            this.barAddOr.Glyph = global::ICP.WF.Activities.Properties.Resources.or;
            this.barAddOr.Id = 1;
            this.barAddOr.Name = "barAddOr";
            resources.ApplyResources(toolTipTitleItem9, "toolTipTitleItem9");
            superToolTip9.Items.Add(toolTipTitleItem9);
            this.barAddOr.SuperTip = superToolTip9;
            this.barAddOr.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddOr_ItemClick);
            // 
            // barNewUserRule
            // 
            resources.ApplyResources(this.barNewUserRule, "barNewUserRule");
            this.barNewUserRule.Glyph = global::ICP.WF.Activities.Properties.Resources.user;
            this.barNewUserRule.Id = 3;
            this.barNewUserRule.Name = "barNewUserRule";
            resources.ApplyResources(toolTipTitleItem10, "toolTipTitleItem10");
            superToolTip10.Items.Add(toolTipTitleItem10);
            this.barNewUserRule.SuperTip = superToolTip10;
            this.barNewUserRule.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewUserRule_ItemClick);
            // 
            // barNewJobRule
            // 
            resources.ApplyResources(this.barNewJobRule, "barNewJobRule");
            this.barNewJobRule.Glyph = global::ICP.WF.Activities.Properties.Resources.role;
            this.barNewJobRule.Id = 4;
            this.barNewJobRule.Name = "barNewJobRule";
            resources.ApplyResources(toolTipTitleItem11, "toolTipTitleItem11");
            superToolTip11.Items.Add(toolTipTitleItem11);
            this.barNewJobRule.SuperTip = superToolTip11;
            this.barNewJobRule.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewJobRule_ItemClick);
            // 
            // barNewDepartRule
            // 
            resources.ApplyResources(this.barNewDepartRule, "barNewDepartRule");
            this.barNewDepartRule.Glyph = global::ICP.WF.Activities.Properties.Resources.dpm;
            this.barNewDepartRule.Id = 5;
            this.barNewDepartRule.Name = "barNewDepartRule";
            resources.ApplyResources(toolTipTitleItem12, "toolTipTitleItem12");
            superToolTip12.Items.Add(toolTipTitleItem12);
            this.barNewDepartRule.SuperTip = superToolTip12;
            this.barNewDepartRule.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewDepartRule_ItemClick);
            // 
            // btnNewOrganizationRule
            // 
            this.btnNewOrganizationRule.Glyph = global::ICP.WF.Activities.Properties.Resources.Decision;
            this.btnNewOrganizationRule.Id = 9;
            this.btnNewOrganizationRule.Name = "btnNewOrganizationRule";
            this.btnNewOrganizationRule.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewOrganizationRule_ItemClick);
            // 
            // barNewFormExpression
            // 
            resources.ApplyResources(this.barNewFormExpression, "barNewFormExpression");
            this.barNewFormExpression.Glyph = global::ICP.WF.Activities.Properties.Resources.DecisionBranch;
            this.barNewFormExpression.Id = 6;
            this.barNewFormExpression.Name = "barNewFormExpression";
            resources.ApplyResources(toolTipTitleItem13, "toolTipTitleItem13");
            superToolTip13.Items.Add(toolTipTitleItem13);
            this.barNewFormExpression.SuperTip = superToolTip13;
            this.barNewFormExpression.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewFormExpression_ItemClick);
            // 
            // barDelete
            // 
            resources.ApplyResources(this.barDelete, "barDelete");
            this.barDelete.Glyph = global::ICP.WF.Activities.Properties.Resources.Delete;
            this.barDelete.GlyphDisabled = global::ICP.WF.Activities.Properties.Resources.Delete;
            this.barDelete.Id = 8;
            this.barDelete.Name = "barDelete";
            resources.ApplyResources(toolTipTitleItem14, "toolTipTitleItem14");
            superToolTip14.Items.Add(toolTipTitleItem14);
            this.barDelete.SuperTip = superToolTip14;
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // barNewNotRule
            // 
            resources.ApplyResources(this.barNewNotRule, "barNewNotRule");
            this.barNewNotRule.Glyph = global::ICP.WF.Activities.Properties.Resources.Not1;
            this.barNewNotRule.Id = 2;
            this.barNewNotRule.Name = "barNewNotRule";
            this.barNewNotRule.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewNotRule_ItemClick);
            // 
            // ConditionBrowserDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConditionBrowserDialog";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeViewRule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

		}

       

		#endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraTreeList.TreeList treeViewRule;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barNewAnd;
        private DevExpress.XtraBars.BarButtonItem barAddOr;
        private DevExpress.XtraBars.BarButtonItem barNewNotRule;
        private DevExpress.XtraBars.BarButtonItem barNewUserRule;
        private DevExpress.XtraBars.BarButtonItem barNewJobRule;
        private DevExpress.XtraBars.BarButtonItem barNewDepartRule;
        private DevExpress.XtraBars.BarButtonItem barNewFormExpression;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private UCFormRule ucFormRule;
        private UCDepartment ucDepartment;
        private UCOrganization ucOrganization;
        private UCJobList ucJobList;
        private UCUserList ucUserList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcNodes;
        private DevExpress.XtraBars.BarButtonItem btnNewOrganizationRule;
	}
}
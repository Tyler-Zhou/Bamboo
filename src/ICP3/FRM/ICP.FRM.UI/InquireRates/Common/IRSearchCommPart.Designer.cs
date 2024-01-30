namespace ICP.FRM.UI.InquireRates
{
    partial class IRSearchCommPart
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
            this.treeComm = new DevExpress.XtraTreeList.TreeList();
            this.colCommEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsComm = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxNewComm = new System.Windows.Forms.GroupBox();
            this.txtComm = new DevExpress.XtraEditors.MemoEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCommFinder = new DevExpress.XtraEditors.TextEdit();
            this.btnCommSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnCommNext = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupEXCEPT = new System.Windows.Forms.GroupBox();
            this.treeExcept = new DevExpress.XtraTreeList.TreeList();
            this.colExceptEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsExceptComm = new System.Windows.Forms.BindingSource(this.components);
            this.txtExcept = new DevExpress.XtraEditors.MemoEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtExceptFind = new DevExpress.XtraEditors.TextEdit();
            this.btnExceptSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnExceptNext = new DevExpress.XtraEditors.SimpleButton();
            this.groupBoxOriginComm = new System.Windows.Forms.GroupBox();
            this.txtOriginComm = new DevExpress.XtraEditors.MemoEdit();
            this.groupErrorComm = new System.Windows.Forms.GroupBox();
            this.txtErrorComm = new DevExpress.XtraEditors.MemoEdit();
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.treeComm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsComm)).BeginInit();
            this.groupBoxNewComm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComm.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommFinder.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.groupEXCEPT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeExcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExceptComm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExcept.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExceptFind.Properties)).BeginInit();
            this.groupBoxOriginComm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOriginComm.Properties)).BeginInit();
            this.groupErrorComm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorComm.Properties)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeComm
            // 
            this.treeComm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeComm.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeComm.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeComm.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeComm.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeComm.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCommEName});
            this.treeComm.DataSource = this.bsComm;
            this.treeComm.Location = new System.Drawing.Point(7, 45);
            this.treeComm.Name = "treeComm";
            this.treeComm.OptionsView.ShowCheckBoxes = true;
            this.treeComm.OptionsView.ShowColumns = false;
            this.treeComm.OptionsView.ShowHorzLines = false;
            this.treeComm.OptionsView.ShowIndicator = false;
            this.treeComm.Size = new System.Drawing.Size(315, 191);
            this.treeComm.TabIndex = 1;
            // 
            // colCommEName
            // 
            this.colCommEName.FieldName = "EName";
            this.colCommEName.Name = "colCommEName";
            this.colCommEName.OptionsColumn.AllowEdit = false;
            this.colCommEName.Visible = true;
            this.colCommEName.VisibleIndex = 0;
            this.colCommEName.Width = 30;
            // 
            // bsComm
            // 
            this.bsComm.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CommodityList);
            // 
            // groupBoxNewComm
            // 
            this.groupBoxNewComm.Controls.Add(this.treeComm);
            this.groupBoxNewComm.Controls.Add(this.txtComm);
            this.groupBoxNewComm.Controls.Add(this.panel1);
            this.groupBoxNewComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxNewComm.Location = new System.Drawing.Point(0, 0);
            this.groupBoxNewComm.Name = "groupBoxNewComm";
            this.groupBoxNewComm.Size = new System.Drawing.Size(605, 243);
            this.groupBoxNewComm.TabIndex = 0;
            this.groupBoxNewComm.TabStop = false;
            this.groupBoxNewComm.Text = "New Comm";
            // 
            // txtComm
            // 
            this.txtComm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComm.Location = new System.Drawing.Point(328, 15);
            this.txtComm.Name = "txtComm";
            this.txtComm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtComm.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtComm.Properties.Appearance.Options.UseBackColor = true;
            this.txtComm.Properties.Appearance.Options.UseForeColor = true;
            this.txtComm.Properties.ReadOnly = true;
            this.txtComm.Size = new System.Drawing.Size(265, 221);
            this.txtComm.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCommFinder);
            this.panel1.Controls.Add(this.btnCommSelect);
            this.panel1.Controls.Add(this.btnCommNext);
            this.panel1.Location = new System.Drawing.Point(7, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 28);
            this.panel1.TabIndex = 0;
            // 
            // txtCommFinder
            // 
            this.txtCommFinder.EditValue = "";
            this.txtCommFinder.Location = new System.Drawing.Point(3, 3);
            this.txtCommFinder.Name = "txtCommFinder";
            this.txtCommFinder.Size = new System.Drawing.Size(198, 21);
            this.txtCommFinder.TabIndex = 0;
            this.txtCommFinder.TabStop = false;
            this.txtCommFinder.TextChanged += new System.EventHandler(this.txtCommFinder_TextChanged);
            // 
            // btnCommSelect
            // 
            this.btnCommSelect.Location = new System.Drawing.Point(261, 3);
            this.btnCommSelect.Name = "btnCommSelect";
            this.btnCommSelect.Size = new System.Drawing.Size(50, 21);
            this.btnCommSelect.TabIndex = 2;
            this.btnCommSelect.Text = "Select";
            this.btnCommSelect.Click += new System.EventHandler(this.btnCommSelect_Click);
            // 
            // btnCommNext
            // 
            this.btnCommNext.Location = new System.Drawing.Point(207, 3);
            this.btnCommNext.Name = "btnCommNext";
            this.btnCommNext.Size = new System.Drawing.Size(50, 21);
            this.btnCommNext.TabIndex = 1;
            this.btnCommNext.Text = "Next";
            this.btnCommNext.Click += new System.EventHandler(this.btnCommNext_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 553);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 47);
            this.panel2.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(699, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(592, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 21);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupBoxNewComm);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupEXCEPT);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(605, 493);
            this.splitContainerControl1.SplitterPosition = 243;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupEXCEPT
            // 
            this.groupEXCEPT.Controls.Add(this.treeExcept);
            this.groupEXCEPT.Controls.Add(this.txtExcept);
            this.groupEXCEPT.Controls.Add(this.panel3);
            this.groupEXCEPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEXCEPT.Location = new System.Drawing.Point(0, 0);
            this.groupEXCEPT.Name = "groupEXCEPT";
            this.groupEXCEPT.Size = new System.Drawing.Size(605, 244);
            this.groupEXCEPT.TabIndex = 0;
            this.groupEXCEPT.TabStop = false;
            this.groupEXCEPT.Text = "EXCEPT Comm";
            // 
            // treeExcept
            // 
            this.treeExcept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeExcept.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeExcept.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeExcept.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeExcept.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeExcept.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colExceptEName});
            this.treeExcept.DataSource = this.bsExceptComm;
            this.treeExcept.Location = new System.Drawing.Point(7, 45);
            this.treeExcept.Name = "treeExcept";
            this.treeExcept.OptionsView.ShowCheckBoxes = true;
            this.treeExcept.OptionsView.ShowColumns = false;
            this.treeExcept.OptionsView.ShowHorzLines = false;
            this.treeExcept.OptionsView.ShowIndicator = false;
            this.treeExcept.Size = new System.Drawing.Size(315, 192);
            this.treeExcept.TabIndex = 1;
            // 
            // colExceptEName
            // 
            this.colExceptEName.FieldName = "EName";
            this.colExceptEName.Name = "colExceptEName";
            this.colExceptEName.OptionsColumn.AllowEdit = false;
            this.colExceptEName.Visible = true;
            this.colExceptEName.VisibleIndex = 0;
            this.colExceptEName.Width = 30;
            // 
            // bsExceptComm
            // 
            this.bsExceptComm.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CommodityList);
            // 
            // txtExcept
            // 
            this.txtExcept.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcept.Location = new System.Drawing.Point(328, 15);
            this.txtExcept.Name = "txtExcept";
            this.txtExcept.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtExcept.Properties.Appearance.Options.UseBackColor = true;
            this.txtExcept.Properties.ReadOnly = true;
            this.txtExcept.Size = new System.Drawing.Size(265, 222);
            this.txtExcept.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtExceptFind);
            this.panel3.Controls.Add(this.btnExceptSelect);
            this.panel3.Controls.Add(this.btnExceptNext);
            this.panel3.Location = new System.Drawing.Point(7, 15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(315, 28);
            this.panel3.TabIndex = 0;
            // 
            // txtExceptFind
            // 
            this.txtExceptFind.EditValue = "";
            this.txtExceptFind.Location = new System.Drawing.Point(3, 3);
            this.txtExceptFind.Name = "txtExceptFind";
            this.txtExceptFind.Size = new System.Drawing.Size(198, 21);
            this.txtExceptFind.TabIndex = 0;
            this.txtExceptFind.TabStop = false;
            // 
            // btnExceptSelect
            // 
            this.btnExceptSelect.Location = new System.Drawing.Point(261, 3);
            this.btnExceptSelect.Name = "btnExceptSelect";
            this.btnExceptSelect.Size = new System.Drawing.Size(50, 21);
            this.btnExceptSelect.TabIndex = 2;
            this.btnExceptSelect.Text = "Select";
            this.btnExceptSelect.Click += new System.EventHandler(this.btnExceptSelect_Click);
            // 
            // btnExceptNext
            // 
            this.btnExceptNext.Location = new System.Drawing.Point(207, 3);
            this.btnExceptNext.Name = "btnExceptNext";
            this.btnExceptNext.Size = new System.Drawing.Size(50, 21);
            this.btnExceptNext.TabIndex = 1;
            this.btnExceptNext.Text = "Next";
            this.btnExceptNext.Click += new System.EventHandler(this.btnExceptNext_Click);
            // 
            // groupBoxOriginComm
            // 
            this.groupBoxOriginComm.Controls.Add(this.txtOriginComm);
            this.groupBoxOriginComm.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOriginComm.Location = new System.Drawing.Point(0, 0);
            this.groupBoxOriginComm.Name = "groupBoxOriginComm";
            this.groupBoxOriginComm.Size = new System.Drawing.Size(800, 60);
            this.groupBoxOriginComm.TabIndex = 0;
            this.groupBoxOriginComm.TabStop = false;
            this.groupBoxOriginComm.Text = "Origin Comm";
            // 
            // txtOriginComm
            // 
            this.txtOriginComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOriginComm.Location = new System.Drawing.Point(3, 18);
            this.txtOriginComm.Name = "txtOriginComm";
            this.txtOriginComm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtOriginComm.Properties.Appearance.Options.UseBackColor = true;
            this.txtOriginComm.Properties.ReadOnly = true;
            this.txtOriginComm.Size = new System.Drawing.Size(794, 39);
            this.txtOriginComm.TabIndex = 0;
            // 
            // groupErrorComm
            // 
            this.groupErrorComm.Controls.Add(this.txtErrorComm);
            this.groupErrorComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupErrorComm.Location = new System.Drawing.Point(0, 0);
            this.groupErrorComm.Name = "groupErrorComm";
            this.groupErrorComm.Size = new System.Drawing.Size(189, 493);
            this.groupErrorComm.TabIndex = 0;
            this.groupErrorComm.TabStop = false;
            this.groupErrorComm.Text = "Error Comm";
            // 
            // txtErrorComm
            // 
            this.txtErrorComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrorComm.EditValue = "";
            this.txtErrorComm.Location = new System.Drawing.Point(3, 18);
            this.txtErrorComm.Name = "txtErrorComm";
            this.txtErrorComm.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtErrorComm.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtErrorComm.Properties.Appearance.Options.UseBackColor = true;
            this.txtErrorComm.Properties.Appearance.Options.UseForeColor = true;
            this.txtErrorComm.Properties.ReadOnly = true;
            this.txtErrorComm.Size = new System.Drawing.Size(183, 472);
            this.txtErrorComm.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.splitContainerControl2);
            this.panelMain.Controls.Add(this.groupBoxOriginComm);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 553);
            this.panelMain.TabIndex = 9;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 60);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupErrorComm);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.splitContainerControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(800, 493);
            this.splitContainerControl2.SplitterPosition = 189;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // OPSearchCommPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel2);
            this.Name = "OPSearchCommPart";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.treeComm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsComm)).EndInit();
            this.groupBoxNewComm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtComm.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCommFinder.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.groupEXCEPT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeExcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExceptComm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExcept.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtExceptFind.Properties)).EndInit();
            this.groupBoxOriginComm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOriginComm.Properties)).EndInit();
            this.groupErrorComm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorComm.Properties)).EndInit();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeComm;
        private System.Windows.Forms.BindingSource bsComm;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private System.Windows.Forms.GroupBox groupBoxNewComm;
        private DevExpress.XtraEditors.MemoEdit txtComm;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCommEName;
        private DevExpress.XtraEditors.SimpleButton btnCommSelect;
        private DevExpress.XtraEditors.SimpleButton btnCommNext;
        private DevExpress.XtraEditors.TextEdit txtCommFinder;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.GroupBox groupBoxOriginComm;
        private DevExpress.XtraEditors.MemoEdit txtOriginComm;
        private System.Windows.Forms.GroupBox groupErrorComm;
        private DevExpress.XtraEditors.MemoEdit txtErrorComm;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsExceptComm;
        private System.Windows.Forms.GroupBox groupEXCEPT;
        private DevExpress.XtraTreeList.TreeList treeExcept;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colExceptEName;
        private DevExpress.XtraEditors.MemoEdit txtExcept;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.TextEdit txtExceptFind;
        private DevExpress.XtraEditors.SimpleButton btnExceptSelect;
        private DevExpress.XtraEditors.SimpleButton btnExceptNext;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
    }
}
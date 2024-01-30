namespace ICP.TaskCenter.UI
{
    partial class UserAssists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAssists));
            this.simpleButtonInsert = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonDelte = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.UserAssistsData = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnStaff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFromDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnToDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControlUpdate = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditToDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControlto = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFromDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControlDuration = new DevExpress.XtraEditors.LabelControl();
            this.labelControlwork = new DevExpress.XtraEditors.LabelControl();
            this.labelControltoassist = new DevExpress.XtraEditors.LabelControl();
            this.labelControlAssign = new DevExpress.XtraEditors.LabelControl();
            this.ComboBoxAssister = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.ComboBoxUser = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserAssistsData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabControlUpdate.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxAssister.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonInsert
            // 
            this.simpleButtonInsert.Location = new System.Drawing.Point(12, 9);
            this.simpleButtonInsert.Name = "simpleButtonInsert";
            this.simpleButtonInsert.Size = new System.Drawing.Size(85, 23);
            this.simpleButtonInsert.TabIndex = 0;
            this.simpleButtonInsert.Text = "新增";
            this.simpleButtonInsert.Click += new System.EventHandler(this.simpleButtonInsert_Click);
            // 
            // simpleButtonDelte
            // 
            this.simpleButtonDelte.Location = new System.Drawing.Point(104, 9);
            this.simpleButtonDelte.Name = "simpleButtonDelte";
            this.simpleButtonDelte.Size = new System.Drawing.Size(90, 23);
            this.simpleButtonDelte.TabIndex = 1;
            this.simpleButtonDelte.Text = "删除";
            this.simpleButtonDelte.Click += new System.EventHandler(this.simpleButtonDelte_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButtonInsert);
            this.panel1.Controls.Add(this.simpleButtonDelte);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 43);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(458, 188);
            this.panel2.TabIndex = 3;
            // 
            // gridControl
            // 
            this.gridControl.DataSource = this.UserAssistsData;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(458, 188);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnStaff,
            this.gridColumnFromDate,
            this.gridColumnToDate});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
            // 
            // gridColumnStaff
            // 
            this.gridColumnStaff.Caption = "工作人员";
            this.gridColumnStaff.FieldName = "Staff";
            this.gridColumnStaff.Name = "gridColumnStaff";
            this.gridColumnStaff.OptionsColumn.AllowEdit = false;
            this.gridColumnStaff.Visible = true;
            this.gridColumnStaff.VisibleIndex = 0;
            this.gridColumnStaff.Width = 180;
            // 
            // gridColumnFromDate
            // 
            this.gridColumnFromDate.Caption = "开始时间";
            this.gridColumnFromDate.FieldName = "FromDate";
            this.gridColumnFromDate.Name = "gridColumnFromDate";
            this.gridColumnFromDate.OptionsColumn.AllowEdit = false;
            this.gridColumnFromDate.Visible = true;
            this.gridColumnFromDate.VisibleIndex = 1;
            this.gridColumnFromDate.Width = 159;
            // 
            // gridColumnToDate
            // 
            this.gridColumnToDate.Caption = "结束时间";
            this.gridColumnToDate.FieldName = "ToDate";
            this.gridColumnToDate.Name = "gridColumnToDate";
            this.gridColumnToDate.OptionsColumn.AllowEdit = false;
            this.gridColumnToDate.Visible = true;
            this.gridColumnToDate.VisibleIndex = 2;
            this.gridColumnToDate.Width = 161;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControlUpdate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 231);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(458, 142);
            this.panel3.TabIndex = 4;
            // 
            // tabControlUpdate
            // 
            this.tabControlUpdate.Controls.Add(this.tabPage1);
            this.tabControlUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlUpdate.Location = new System.Drawing.Point(0, 0);
            this.tabControlUpdate.Name = "tabControlUpdate";
            this.tabControlUpdate.SelectedIndex = 0;
            this.tabControlUpdate.Size = new System.Drawing.Size(458, 142);
            this.tabControlUpdate.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ComboBoxUser);
            this.tabPage1.Controls.Add(this.ComboBoxAssister);
            this.tabPage1.Controls.Add(this.simpleButtonSave);
            this.tabPage1.Controls.Add(this.dateEditToDate);
            this.tabPage1.Controls.Add(this.labelControlto);
            this.tabPage1.Controls.Add(this.dateEditFromDate);
            this.tabPage1.Controls.Add(this.labelControlDuration);
            this.tabPage1.Controls.Add(this.labelControlwork);
            this.tabPage1.Controls.Add(this.labelControltoassist);
            this.tabPage1.Controls.Add(this.labelControlAssign);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(450, 115);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "编辑";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // simpleButtonSave
            // 
            this.simpleButtonSave.Location = new System.Drawing.Point(8, 87);
            this.simpleButtonSave.Name = "simpleButtonSave";
            this.simpleButtonSave.Size = new System.Drawing.Size(85, 23);
            this.simpleButtonSave.TabIndex = 9;
            this.simpleButtonSave.Text = "保存";
            this.simpleButtonSave.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // dateEditToDate
            // 
            this.dateEditToDate.EditValue = null;
            this.dateEditToDate.Location = new System.Drawing.Point(265, 52);
            this.dateEditToDate.Name = "dateEditToDate";
            this.dateEditToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditToDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditToDate.Size = new System.Drawing.Size(126, 21);
            this.dateEditToDate.TabIndex = 8;
            // 
            // labelControlto
            // 
            this.labelControlto.Location = new System.Drawing.Point(199, 55);
            this.labelControlto.Name = "labelControlto";
            this.labelControlto.Size = new System.Drawing.Size(48, 14);
            this.labelControlto.TabIndex = 7;
            this.labelControlto.Text = "结束时间";
            // 
            // dateEditFromDate
            // 
            this.dateEditFromDate.EditValue = null;
            this.dateEditFromDate.Location = new System.Drawing.Point(64, 52);
            this.dateEditFromDate.Name = "dateEditFromDate";
            this.dateEditFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFromDate.Size = new System.Drawing.Size(126, 21);
            this.dateEditFromDate.TabIndex = 6;
            // 
            // labelControlDuration
            // 
            this.labelControlDuration.Location = new System.Drawing.Point(10, 55);
            this.labelControlDuration.Name = "labelControlDuration";
            this.labelControlDuration.Size = new System.Drawing.Size(48, 14);
            this.labelControlDuration.TabIndex = 5;
            this.labelControlDuration.Text = "开始时间";
            // 
            // labelControlwork
            // 
            this.labelControlwork.Location = new System.Drawing.Point(402, 18);
            this.labelControlwork.Name = "labelControlwork";
            this.labelControlwork.Size = new System.Drawing.Size(40, 14);
            this.labelControlwork.TabIndex = 4;
            this.labelControlwork.Text = "的工作.";
            // 
            // labelControltoassist
            // 
            this.labelControltoassist.Location = new System.Drawing.Point(199, 18);
            this.labelControltoassist.Name = "labelControltoassist";
            this.labelControltoassist.Size = new System.Drawing.Size(60, 14);
            this.labelControltoassist.TabIndex = 2;
            this.labelControltoassist.Text = "代理或协助";
            // 
            // labelControlAssign
            // 
            this.labelControlAssign.Location = new System.Drawing.Point(10, 18);
            this.labelControlAssign.Name = "labelControlAssign";
            this.labelControlAssign.Size = new System.Drawing.Size(24, 14);
            this.labelControlAssign.TabIndex = 0;
            this.labelControlAssign.Text = "安排";
            // 
            // ComboBoxAssister
            // 
            this.ComboBoxAssister.Location = new System.Drawing.Point(64, 15);
            this.ComboBoxAssister.Name = "ComboBoxAssister";
            this.ComboBoxAssister.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ComboBoxAssister.Size = new System.Drawing.Size(126, 21);
            this.ComboBoxAssister.TabIndex = 10;
            // 
            // ComboBoxUser
            // 
            this.ComboBoxUser.Location = new System.Drawing.Point(265, 15);
            this.ComboBoxUser.Name = "ComboBoxUser";
            this.ComboBoxUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ComboBoxUser.Size = new System.Drawing.Size(126, 21);
            this.ComboBoxUser.TabIndex = 11;
            // 
            // UserAssists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 373);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserAssists";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UserAssists_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserAssistsData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tabControlUpdate.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxAssister.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxUser.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonInsert;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDelte;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControlUpdate;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private DevExpress.XtraEditors.DateEdit dateEditToDate;
        private DevExpress.XtraEditors.LabelControl labelControlto;
        private DevExpress.XtraEditors.DateEdit dateEditFromDate;
        private DevExpress.XtraEditors.LabelControl labelControlDuration;
        private DevExpress.XtraEditors.LabelControl labelControlwork;
        private DevExpress.XtraEditors.LabelControl labelControltoassist;
        private DevExpress.XtraEditors.LabelControl labelControlAssign;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStaff;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnToDate;
        private System.Windows.Forms.BindingSource UserAssistsData;
        private DevExpress.XtraEditors.ImageComboBoxEdit ComboBoxUser;
        private DevExpress.XtraEditors.ImageComboBoxEdit ComboBoxAssister;
    }
}
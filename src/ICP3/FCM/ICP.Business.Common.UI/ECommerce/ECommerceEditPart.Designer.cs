namespace ICP.Business.Common.UI.ECommerce
{
    partial class ECommerceEditPart
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
            this.panelFill = new DevExpress.XtraEditors.PanelControl();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.stxtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.stxtBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labEndDate = new DevExpress.XtraEditors.LabelControl();
            this.labBeginDate = new DevExpress.XtraEditors.LabelControl();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.stxtOperationNo = new DevExpress.XtraEditors.TextEdit();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSure = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).BeginInit();
            this.panelFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.gcList);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 30);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(900, 340);
            this.panelFill.TabIndex = 0;
            // 
            // gcList
            // 
            this.gcList.DataSource = this.bindingSource;
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(2, 2);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(896, 336);
            this.gcList.TabIndex = 1;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ICP.FCM.Common.ServiceInterface.ECommerceList);
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsSelect,
            this.colNo,
            this.colOperationDate,
            this.colPOLName,
            this.colPODName,
            this.colQuantity,
            this.colWeight,
            this.colMeasurement});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsCustomization.AllowColumnResizing = false;
            this.gvList.OptionsView.ColumnAutoWidth = false;
            this.gvList.OptionsView.ShowFooter = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            // 
            // colIsSelect
            // 
            this.colIsSelect.Caption = "Select";
            this.colIsSelect.FieldName = "IsSelect";
            this.colIsSelect.Name = "colIsSelect";
            this.colIsSelect.Visible = true;
            this.colIsSelect.VisibleIndex = 0;
            this.colIsSelect.Width = 60;
            // 
            // colNo
            // 
            this.colNo.Caption = "Operation No";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowEdit = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 130;
            // 
            // colOperationDate
            // 
            this.colOperationDate.Caption = "Operation Date";
            this.colOperationDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colOperationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOperationDate.FieldName = "OperationDate";
            this.colOperationDate.Name = "colOperationDate";
            this.colOperationDate.OptionsColumn.AllowEdit = false;
            this.colOperationDate.Visible = true;
            this.colOperationDate.VisibleIndex = 2;
            this.colOperationDate.Width = 120;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "POL Name";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.OptionsColumn.AllowEdit = false;
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 3;
            this.colPOLName.Width = 130;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "POD Name";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.OptionsColumn.AllowEdit = false;
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 4;
            this.colPODName.Width = 130;
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 5;
            this.colQuantity.Width = 100;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "Weight";
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.OptionsColumn.AllowEdit = false;
            this.colWeight.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 6;
            this.colWeight.Width = 100;
            // 
            // colMeasurement
            // 
            this.colMeasurement.Caption = "Measurement";
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.OptionsColumn.AllowEdit = false;
            this.colMeasurement.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 7;
            this.colMeasurement.Width = 100;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.stxtEndDate);
            this.panelTop.Controls.Add(this.stxtBeginDate);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.labEndDate);
            this.panelTop.Controls.Add(this.labBeginDate);
            this.panelTop.Controls.Add(this.labOperationNo);
            this.panelTop.Controls.Add(this.stxtOperationNo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.MaximumSize = new System.Drawing.Size(0, 30);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 30);
            this.panelTop.TabIndex = 1;
            // 
            // stxtEndDate
            // 
            this.stxtEndDate.EditValue = null;
            this.stxtEndDate.Location = new System.Drawing.Point(549, 5);
            this.stxtEndDate.Name = "stxtEndDate";
            this.stxtEndDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtEndDate.Properties.Appearance.Options.UseBackColor = true;
            this.stxtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtEndDate.Properties.Mask.EditMask = "";
            this.stxtEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.stxtEndDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.stxtEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtEndDate.Size = new System.Drawing.Size(120, 21);
            this.stxtEndDate.TabIndex = 4;
            this.stxtEndDate.TabStop = false;
            // 
            // stxtBeginDate
            // 
            this.stxtBeginDate.EditValue = null;
            this.stxtBeginDate.Location = new System.Drawing.Point(317, 5);
            this.stxtBeginDate.Name = "stxtBeginDate";
            this.stxtBeginDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtBeginDate.Properties.Appearance.Options.UseBackColor = true;
            this.stxtBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stxtBeginDate.Properties.Mask.EditMask = "";
            this.stxtBeginDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.stxtBeginDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.stxtBeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtBeginDate.Size = new System.Drawing.Size(120, 21);
            this.stxtBeginDate.TabIndex = 4;
            this.stxtBeginDate.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(675, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            // 
            // labEndDate
            // 
            this.labEndDate.Location = new System.Drawing.Point(458, 8);
            this.labEndDate.Name = "labEndDate";
            this.labEndDate.Size = new System.Drawing.Size(51, 14);
            this.labEndDate.TabIndex = 1;
            this.labEndDate.Text = "End Date";
            // 
            // labBeginDate
            // 
            this.labBeginDate.Location = new System.Drawing.Point(226, 8);
            this.labBeginDate.Name = "labBeginDate";
            this.labBeginDate.Size = new System.Drawing.Size(60, 14);
            this.labBeginDate.TabIndex = 1;
            this.labBeginDate.Text = "Begin Date";
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(19, 8);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(73, 14);
            this.labOperationNo.TabIndex = 1;
            this.labOperationNo.Text = "Operation No";
            // 
            // stxtOperationNo
            // 
            this.stxtOperationNo.Location = new System.Drawing.Point(95, 5);
            this.stxtOperationNo.Name = "stxtOperationNo";
            this.stxtOperationNo.Size = new System.Drawing.Size(120, 21);
            this.stxtOperationNo.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnSure);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 370);
            this.panelBottom.MaximumSize = new System.Drawing.Size(0, 30);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(900, 30);
            this.panelBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(758, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(677, 5);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 0;
            this.btnSure.Text = "Sure";
            // 
            // ECommerceEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "ECommerceEditPart";
            this.Size = new System.Drawing.Size(900, 400);
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).EndInit();
            this.panelFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelFill;
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.LabelControl labOperationNo;
        private DevExpress.XtraEditors.TextEdit stxtOperationNo;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSure;
        private DevExpress.XtraEditors.DateEdit stxtBeginDate;
        private DevExpress.XtraEditors.LabelControl labBeginDate;
        private System.Windows.Forms.BindingSource bindingSource;
        protected DevExpress.XtraGrid.GridControl gcList;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelect;
        private DevExpress.XtraEditors.DateEdit stxtEndDate;
        private DevExpress.XtraEditors.LabelControl labEndDate;
    }
}

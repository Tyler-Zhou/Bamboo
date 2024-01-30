namespace ICP.Common.UI.CustomerManager
{
    partial class CustomerManagerCheckForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureTip = new DevExpress.XtraEditors.PictureEdit();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnToAddCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCheck = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mainGridList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTel1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountryProvinceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckedStateDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKeyWord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtErrorInfo = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTip.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtErrorInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureTip);
            this.panel1.Controls.Add(this.lblTip);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnToAddCustomer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 343);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 38);
            this.panel1.TabIndex = 1;
            // 
            // pictureTip
            // 
            this.pictureTip.Location = new System.Drawing.Point(4, 7);
            this.pictureTip.Name = "pictureTip";
            this.pictureTip.Size = new System.Drawing.Size(20, 23);
            this.pictureTip.TabIndex = 6;
            // 
            // lblTip
            // 
            this.lblTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Appearance.Options.UseForeColor = true;
            this.lblTip.Location = new System.Drawing.Point(30, 12);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(48, 14);
            this.lblTip.TabIndex = 5;
            this.lblTip.Text = "信息提示";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(461, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnToAddCustomer
            // 
            this.btnToAddCustomer.Location = new System.Drawing.Point(367, 6);
            this.btnToAddCustomer.Name = "btnToAddCustomer";
            this.btnToAddCustomer.Size = new System.Drawing.Size(75, 23);
            this.btnToAddCustomer.TabIndex = 0;
            this.btnToAddCustomer.Text = "新增客户(&A)";
            this.btnToAddCustomer.Click += new System.EventHandler(this.btnToAddCustomer_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCheck);
            this.splitContainer1.Panel1.Controls.Add(this.txtName);
            this.splitContainer1.Panel1.Controls.Add(this.labelControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mainGridList);
            this.splitContainer1.Size = new System.Drawing.Size(548, 343);
            this.splitContainer1.SplitterDistance = 37;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(402, 9);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 1;
            this.btnCheck.Text = "检查(&S)";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(72, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(314, 21);
            this.txtName.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "名称";
            // 
            // mainGridList
            // 
            this.mainGridList.DataSource = this.bsDataSource;
            this.mainGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGridList.Location = new System.Drawing.Point(0, 0);
            this.mainGridList.MainView = this.mainGridView;
            this.mainGridList.Name = "mainGridList";
            this.mainGridList.Size = new System.Drawing.Size(548, 302);
            this.mainGridList.TabIndex = 0;
            this.mainGridList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainGridView});
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerList);
            // 
            // mainGridView
            // 
            this.mainGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colEName,
            this.colCName,
            this.colTel1,
            this.colFax,
            this.colTypeDescription,
            this.colCountryProvinceName,
            this.colCreateByName,
            this.colCreateDate,
            this.colCheckedStateDescription,
            this.colCAddress,
            this.colEAddress,
            this.colKeyWord});
            this.mainGridView.GridControl = this.mainGridList;
            this.mainGridView.IndicatorWidth = 35;
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.mainGridView.OptionsBehavior.Editable = false;
            this.mainGridView.OptionsBehavior.ReadOnly = true;
            this.mainGridView.OptionsSelection.MultiSelect = true;
            this.mainGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.mainGridView.OptionsView.ColumnAutoWidth = false;
            this.mainGridView.OptionsView.ShowGroupPanel = false;
            this.mainGridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            // 
            // colCode
            // 
            this.colCode.Caption = "代码";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 92;
            // 
            // colEName
            // 
            this.colEName.Caption = "英文名";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 3;
            // 
            // colCName
            // 
            this.colCName.Caption = "中文名";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 2;
            this.colCName.Width = 100;
            // 
            // colTel1
            // 
            this.colTel1.Caption = "电话";
            this.colTel1.FieldName = "Tel1";
            this.colTel1.Name = "colTel1";
            this.colTel1.Visible = true;
            this.colTel1.VisibleIndex = 7;
            // 
            // colFax
            // 
            this.colFax.Caption = "传真";
            this.colFax.FieldName = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 8;
            // 
            // colTypeDescription
            // 
            this.colTypeDescription.Caption = "类型";
            this.colTypeDescription.FieldName = "TypeDescription";
            this.colTypeDescription.Name = "colTypeDescription";
            this.colTypeDescription.OptionsColumn.ReadOnly = true;
            this.colTypeDescription.Visible = true;
            this.colTypeDescription.VisibleIndex = 9;
            // 
            // colCountryProvinceName
            // 
            this.colCountryProvinceName.Caption = "国家";
            this.colCountryProvinceName.FieldName = "CountryProvinceName";
            this.colCountryProvinceName.Name = "colCountryProvinceName";
            this.colCountryProvinceName.Visible = true;
            this.colCountryProvinceName.VisibleIndex = 6;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 10;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 11;
            // 
            // colCheckedStateDescription
            // 
            this.colCheckedStateDescription.Caption = "审核状态";
            this.colCheckedStateDescription.FieldName = "CheckedStateDescription";
            this.colCheckedStateDescription.Name = "colCheckedStateDescription";
            this.colCheckedStateDescription.OptionsColumn.ReadOnly = true;
            this.colCheckedStateDescription.Visible = true;
            this.colCheckedStateDescription.VisibleIndex = 12;
            // 
            // colCAddress
            // 
            this.colCAddress.Caption = "中文地址";
            this.colCAddress.FieldName = "CAddress";
            this.colCAddress.Name = "colCAddress";
            this.colCAddress.Visible = true;
            this.colCAddress.VisibleIndex = 4;
            // 
            // colEAddress
            // 
            this.colEAddress.Caption = "英文地址";
            this.colEAddress.FieldName = "EAddress";
            this.colEAddress.Name = "colEAddress";
            this.colEAddress.Visible = true;
            this.colEAddress.VisibleIndex = 5;
            // 
            // colKeyWord
            // 
            this.colKeyWord.Caption = "关键字";
            this.colKeyWord.FieldName = "KeyWord";
            this.colKeyWord.Name = "colKeyWord";
            this.colKeyWord.Visible = true;
            this.colKeyWord.VisibleIndex = 1;
            // 
            // dtErrorInfo
            // 
            this.dtErrorInfo.ContainerControl = this;
            // 
            // CustomerManagerCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "CustomerManagerCheckForm";
            this.Size = new System.Drawing.Size(548, 381);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTip.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtErrorInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnToAddCustomer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnCheck;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ICP.Framework.ClientComponents.Controls.LWGridControl mainGridList;
        private System.Windows.Forms.BindingSource bsDataSource;
        private DevExpress.XtraGrid.Views.Grid.GridView mainGridView;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colTel1;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryProvinceName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckedStateDescription;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dtErrorInfo;
        private DevExpress.XtraEditors.PictureEdit pictureTip;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraGrid.Columns.GridColumn colCAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colEAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colKeyWord;

    }
}
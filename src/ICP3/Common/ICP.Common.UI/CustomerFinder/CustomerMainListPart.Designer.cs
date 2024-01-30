namespace ICP.Common.UI.CustomerFinder
{
    partial class CustomerMainListPart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTel1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountryProvinceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckedStateDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(944, 248);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colEShortName,
            this.colEName,
            this.colCName,
            this.colTel1,
            this.colFax,
            this.colCountryProvinceName,
            this.colTypeDescription,
            this.colCheckedStateDescription,
            this.colAddress});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            // 
            // colCode
            // 
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 77;
            // 
            // colEShortName
            // 
            this.colEShortName.FieldName = "EShortName";
            this.colEShortName.Name = "colEShortName";
            this.colEShortName.Visible = true;
            this.colEShortName.VisibleIndex = 1;
            this.colEShortName.Width = 80;
            // 
            // colEName
            // 
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 2;
            this.colEName.Width = 76;
            // 
            // colCName
            // 
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 3;
            this.colCName.Width = 76;
            // 
            // colTel1
            // 
            this.colTel1.FieldName = "Tel1";
            this.colTel1.Name = "colTel1";
            this.colTel1.Visible = true;
            this.colTel1.VisibleIndex = 4;
            this.colTel1.Width = 76;
            // 
            // colFax
            // 
            this.colFax.FieldName = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 5;
            this.colFax.Width = 76;
            // 
            // colCountryProvinceName
            // 
            this.colCountryProvinceName.Caption = "Location";
            this.colCountryProvinceName.FieldName = "CountryProvinceName";
            this.colCountryProvinceName.Name = "colCountryProvinceName";
            this.colCountryProvinceName.Visible = true;
            this.colCountryProvinceName.VisibleIndex = 6;
            // 
            // colTypeDescription
            // 
            this.colTypeDescription.Caption = "Type";
            this.colTypeDescription.FieldName = "TypeDescription";
            this.colTypeDescription.Name = "colTypeDescription";
            this.colTypeDescription.Visible = true;
            this.colTypeDescription.VisibleIndex = 7;
            this.colTypeDescription.Width = 76;
            // 
            // colCheckedStateDescription
            // 
            this.colCheckedStateDescription.Caption = "State";
            this.colCheckedStateDescription.FieldName = "CheckedStateDescription";
            this.colCheckedStateDescription.Name = "colCheckedStateDescription";
            this.colCheckedStateDescription.Visible = true;
            this.colCheckedStateDescription.VisibleIndex = 8;
            // 
            // colAddress
            // 
            this.colAddress.Caption = "EAddress";
            this.colAddress.FieldName = "EAddress";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 9;
            // 
            // CustomerMainListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.gcMain);
            this.Name = "CustomerMainListPart";
            this.Size = new System.Drawing.Size(944, 248);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colEShortName;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colTel1;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryProvinceName;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckedStateDescription;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
    }
}

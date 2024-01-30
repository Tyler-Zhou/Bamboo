namespace ICP.Common.UI.Geography.Location
{
    partial class LocationMainListPart
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
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsOcean = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsAir = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsOther = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountryProvinceName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.LocationList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(714, 248);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colEName,
            this.colName,
            this.colCountryProvinceName,
            this.colIsOcean,
            this.colIsAir,
            this.colIsOther,
            this.colCreateByName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colCode
            // 
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            // 
            // colName
            // 
            this.colName.Caption = "CName";
            this.colName.FieldName = "CName";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 2;
             // 
            // colCountryProvinceName
            // 
            this.colCountryProvinceName.Caption = "CountryProvinceName";
            this.colCountryProvinceName.FieldName = "CountryProvinceName";
            this.colCountryProvinceName.Name = "colCountryProvinceName";
            this.colCountryProvinceName.Visible = true;
            this.colCountryProvinceName.VisibleIndex = 3;            
            // 
            // colIsOcean
            // 
            this.colIsOcean.FieldName = "IsOcean";
            this.colIsOcean.Name = "colIsOcean";
            this.colIsOcean.Visible = true;
            this.colIsOcean.VisibleIndex =4;
            // 
            // colIsAir
            // 
            this.colIsAir.FieldName = "IsAir";
            this.colIsAir.Name = "colIsAir";
            this.colIsAir.Visible = true;
            this.colIsAir.VisibleIndex = 5;
            // 
            // colIsOther
            // 
            this.colIsOther.FieldName = "IsOther";
            this.colIsOther.Name = "colIsOther";
            this.colIsOther.Visible = true;
            this.colIsOther.VisibleIndex = 6;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateBy";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 7;
            // 
            // colEName
            // 
            this.colEName.Caption = "EName";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 1;
            // 
            // LocationMainListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "LocationMainListPart";
            this.Size = new System.Drawing.Size(714, 248);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colIsOcean;
        private DevExpress.XtraGrid.Columns.GridColumn colIsAir;
        private DevExpress.XtraGrid.Columns.GridColumn colIsOther;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryProvinceName;
    }
}

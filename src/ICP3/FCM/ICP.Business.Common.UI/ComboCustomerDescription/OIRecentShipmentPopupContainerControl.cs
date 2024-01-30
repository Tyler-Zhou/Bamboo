using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
namespace ICP.Business.Common.UI
{
    /// <summary>
    /// 客户最近业务选择表格控件
    /// </summary>
    public class OIRecentShipmentPopupContainerControl:PopupContainerControl
    {
      private DevExpress.XtraGrid.Views.Grid.GridView gvOrders;

      private DevExpress.XtraGrid.Columns.GridColumn colNo;
      private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
      private DevExpress.XtraGrid.Columns.GridColumn colPODName;
      private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
      private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
      private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
      private DevExpress.XtraGrid.Columns.GridColumn colETA;
      private DevExpress.XtraGrid.Columns.GridColumn colETD;
      private System.Windows.Forms.BindingSource bindingSource;
      private System.ComponentModel.IContainer components;
      private ICP.Framework.ClientComponents.Controls.LWGridControl lwGridControl;
      /// <summary>
      /// 双击列表数据行改变事件
      /// </summary>
      public event EventHandler<CommonEventArgs<OceanOrderInfo>> SelectChanged;
      public object DataSource
      {
          get
          {
              return this.bindingSource.DataSource;
          }
          set
          {
              this.bindingSource.DataSource = value;
              this.bindingSource.ResetBindings(false);
          }
      }
      public OIRecentShipmentPopupContainerControl()
      {
          InitializeComponent();
      }
      private Guid companyID;
      public Guid CompanyID
      {
          get
          {
              return this.companyID;
          }
          set
          {
              this.companyID = value;
              GetCustomerRecentShipments();
          }
      }
      private Guid customerID;
      public Guid CustomerID
      {
          get
          {
              return this.customerID;
          }
          set
          {
              this.customerID = value;
              GetCustomerRecentShipments();
          }
      }
      public IOceanImportService OceanImportService
      {
          get
          {
              return ServiceClient.GetService<IOceanImportService>();
          }
      }
      private void GetCustomerRecentShipments()
      {
          if (this.CustomerID == Guid.Empty || this.CompanyID == Guid.Empty)
          {
              this.bindingSource.Clear();
              this.bindingSource.DataSource = new List<ICP.FCM.OceanImport.ServiceInterface.OceanOrderInfo>();
              this.bindingSource.ResetBindings(false);
              return;
          }
          List<ICP.FCM.OceanImport.ServiceInterface.OceanOrderInfo> orderList = OceanImportService.GetOIRecentlyOrderList(this.CompanyID, this.CustomerID,LocalData.UserInfo.LoginID, 10);
          this.bindingSource.DataSource = orderList;
          this.bindingSource.ResetBindings(false);
      }
      public OceanOrderInfo CurrentOrderList
      {
          get
          {
              if (this.bindingSource.List == null || bindingSource.Current == null)
              {
                  return null;
              }
              return bindingSource.Current as OceanOrderInfo;
          }
      }
      protected override void Dispose(bool disposing)
      {
          if (disposing)
          {
              this.gvOrders.DoubleClick -= this.gvOrders_DoubleClick;
              this.lwGridControl.DataSource = null;
              this.bindingSource.DataSource = null;
              this.bindingSource.Dispose();

          }
          base.Dispose(disposing);
      }
      
      public void SetLanguage(bool isEnglish)
      {
          if (!isEnglish)
          {
              colClosingDate.Caption = "截关日";
              colConsigneeName.Caption = "收货人";
              colShipperName.Caption = "发货人";
              colNo.Caption = "业务号";
              colPOLName.Caption = "装货港";
              colPODName.Caption = "卸货港";
          }
      }
      private void InitializeComponent()
      {
          this.components = new System.ComponentModel.Container();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OIRecentShipmentPopupContainerControl));
          this.lwGridControl = new ICP.Framework.ClientComponents.Controls.LWGridControl();
          this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
          this.gvOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
          this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colClosingDate = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
          ((System.ComponentModel.ISupportInitialize)(this.lwGridControl)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
          this.SuspendLayout();
          // 
          // lwGridControl
          // 
          this.lwGridControl.DataSource = this.bindingSource;
          this.lwGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
          this.lwGridControl.Location = new System.Drawing.Point(0, 0);
          this.lwGridControl.MainView = this.gvOrders;
          this.lwGridControl.Name = "lwGridControl";
          this.lwGridControl.Size = new System.Drawing.Size(400, 200);
          this.lwGridControl.TabIndex = 0;
          this.lwGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrders});
          // 
          // bindingSource
          // 
          this.bindingSource.DataSource = ((object)(resources.GetObject("bindingSource.DataSource")));
          // 
          // gvOrders
          // 
            this.gvOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colPOLName,
            this.colETD,
            this.colPODName,
            this.colETA,
            this.colShipperName,
            this.colConsigneeName});


            this.gvOrders.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
            this.gvOrders.GridControl = this.lwGridControl;
            this.gvOrders.LevelIndent = 0;
            this.gvOrders.Name = "gvOrders";
            this.gvOrders.OptionsSelection.MultiSelect = true;
            this.gvOrders.OptionsView.ColumnAutoWidth = false;
            this.gvOrders.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrders.OptionsView.ShowDetailButtons = false;
            this.gvOrders.OptionsView.ShowGroupPanel = false;
            this.gvOrders.PreviewFieldName = "Date";
            this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);
            // 
            // colNo
            // 
            this.colNo.Caption = "业务号";
            this.colNo.FieldName = "RefNo";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowEdit = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 110;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "装货港";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.OptionsColumn.AllowEdit = false;
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 1;
            this.colPOLName.Width = 100;
            // 
            // colETD
            // 
            this.colETD.Caption = "ETD";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 2;
            this.colETD.Width = 65;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "卸货港";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.OptionsColumn.AllowEdit = false;
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 3;
            this.colPODName.Width = 100;
            // 
            // colETA
            // 
            this.colETA.Caption = "ETA";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 4;
            this.colETA.Width = 65;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "发货人";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.OptionsColumn.AllowEdit = false;
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 5;
            this.colShipperName.Width = 100;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "收货人";
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.OptionsColumn.AllowEdit = false;
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 6;
            this.colConsigneeName.Width = 100;
          // 
          // RecentShipmentPopupContainerControl
          // 
          this.Size = new System.Drawing.Size(658, 173);
          this.Controls.Add(this.lwGridControl);
          ((System.ComponentModel.ISupportInitialize)(this.lwGridControl)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
          this.ResumeLayout(false);

      }

      private void gvOrders_DoubleClick(object sender, EventArgs e)
      {
          if (this.SelectChanged != null)
          {
              this.SelectChanged(sender, new CommonEventArgs<OceanOrderInfo>(this.CurrentOrderList));
          }
      }
   
  }
}

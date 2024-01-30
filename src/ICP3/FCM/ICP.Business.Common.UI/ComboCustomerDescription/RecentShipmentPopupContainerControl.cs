using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface;
namespace ICP.Business.Common.UI
{
    /// <summary>
    /// 客户最近业务选择表格控件
    /// </summary>
    public class RecentShipmentPopupContainerControl:PopupContainerControl
    {
      private DevExpress.XtraGrid.Views.Grid.GridView gvOrders;

      private DevExpress.XtraGrid.Columns.GridColumn colNo;
      private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
      private DevExpress.XtraGrid.Columns.GridColumn colPODName;
      private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
      private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
      private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
      private System.Windows.Forms.BindingSource bindingSource;
      private System.ComponentModel.IContainer components;
      private ICP.Framework.ClientComponents.Controls.LWGridControl lwGridControl;
      /// <summary>
      /// 双击列表数据行改变事件
      /// </summary>
      public event EventHandler<CommonEventArgs<OceanOrderList>> SelectChanged;
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
      public RecentShipmentPopupContainerControl()
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
      public IOceanExportService OceanExportService
      {
          get
          {
              return ServiceClient.GetService<IOceanExportService>();
          }
      }
      private void GetCustomerRecentShipments()
      {
          if (this.CustomerID == Guid.Empty || this.CompanyID == Guid.Empty)
          {
              this.bindingSource.Clear();
              this.bindingSource.DataSource = new List<OceanOrderList>();
              this.bindingSource.ResetBindings(false);
              return;
          }
          List<OceanOrderList> orderList = OceanExportService.GetRecentlyOceanOrderList(this.CompanyID, this.CustomerID, 10);
          this.bindingSource.DataSource = orderList;
          this.bindingSource.ResetBindings(false);
      }
      public OceanOrderList CurrentOrderList
      {
          get
          {
              if (this.bindingSource.List == null || bindingSource.Current == null)
              {
                  return null;
              }
              return bindingSource.Current as OceanOrderList;
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecentShipmentPopupContainerControl));
          this.lwGridControl = new ICP.Framework.ClientComponents.Controls.LWGridControl();
          this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
          this.gvOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
          this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colClosingDate = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
          this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.colPODName,
            this.colClosingDate,
            this.colShipperName,
            this.colConsigneeName});
          this.gvOrders.GridControl = this.lwGridControl;
          this.gvOrders.Name = "gvOrders";
          this.gvOrders.OptionsBehavior.Editable = false;
          this.gvOrders.OptionsBehavior.ReadOnly = true;
          this.gvOrders.OptionsView.ShowGroupPanel = false;
          this.gvOrders.DoubleClick += new System.EventHandler(this.gvOrders_DoubleClick);
          // 
          // colNo
          // 
          this.colNo.FieldName = "RefNo";
          this.colNo.Name = "colNo";
          this.colNo.OptionsColumn.AllowEdit = false;
          this.colNo.Visible = true;
          this.colNo.VisibleIndex = 0;
          this.colNo.Width = 130;
          // 
          // colPOLName
          // 
          this.colPOLName.Caption = "POL";
          this.colPOLName.FieldName = "POLName";
          this.colPOLName.Name = "colPOLName";
          this.colPOLName.OptionsColumn.AllowEdit = false;
          this.colPOLName.Visible = true;
          this.colPOLName.VisibleIndex = 1;
          this.colPOLName.Width = 100;
          // 
          // colPODName
          // 
          this.colPODName.Caption = "POD";
          this.colPODName.FieldName = "PODName";
          this.colPODName.Name = "colPODName";
          this.colPODName.OptionsColumn.AllowEdit = false;
          this.colPODName.Visible = true;
          this.colPODName.VisibleIndex = 2;
          this.colPODName.Width = 100;
          // 
          // colClosingDate
          // 
          this.colClosingDate.Caption = "CLSDate";
          this.colClosingDate.FieldName = "ClosingDate";
          this.colClosingDate.Name = "colClosingDate";
          this.colClosingDate.OptionsColumn.AllowEdit = false;
          this.colClosingDate.Visible = true;
          this.colClosingDate.VisibleIndex = 3;
          this.colClosingDate.Width = 80;
          // 
          // colShipperName
          // 
          this.colShipperName.Caption = "Shipper";
          this.colShipperName.FieldName = "ShipperName";
          this.colShipperName.Name = "colShipperName";
          this.colShipperName.OptionsColumn.AllowEdit = false;
          this.colShipperName.Visible = true;
          this.colShipperName.VisibleIndex = 4;
          this.colShipperName.Width = 100;
          // 
          // colConsigneeName
          // 
          this.colConsigneeName.Caption = "Consignee";
          this.colConsigneeName.FieldName = "ConsigneeName";
          this.colConsigneeName.Name = "colConsigneeName";
          this.colConsigneeName.OptionsColumn.AllowEdit = false;
          this.colConsigneeName.Visible = true;
          this.colConsigneeName.VisibleIndex = 5;
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
              this.SelectChanged(sender, new CommonEventArgs<OceanOrderList>(this.CurrentOrderList));
          }
      }
   
  }
}

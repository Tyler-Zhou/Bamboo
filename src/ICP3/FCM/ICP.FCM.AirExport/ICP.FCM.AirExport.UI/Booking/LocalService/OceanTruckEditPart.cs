using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class AirTruckEditPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        public IAirExportService AirExportService
        {
            get
            {
                return ServiceClient.GetService<IAirExportService>();
            }
        }

        #endregion

        #region 本地变量

        AirTruckInfo _CurrentData = null;
        AirBookingList _AirBooking = null;
        Guid? _RecentTruckerID = null;
        Guid? _RecentShipperID = null;
        string _RecentTruckerName = string.Empty;
        string _RecentShipperName = string.Empty;
        CustomerDescription _RecentTruckerDes = null;
        CustomerDescription _RecentShipperDes = null;
        #endregion

        #region init

        public AirTruckEditPart()
        {
            InitializeComponent();
            Disposed += delegate {
                _AirBooking = null;
                _bookingContainerDescription = null;
                _CurrentData.PropertyChanged -= _CurrentData_PropertyChanged;
                _recentCustomsBrokerDescription = null;
                _recentReturnLocationDescription = null;
                _RecentShipperDes = null;
                _RecentTruckerDes = null;
                _values = null;
                cstxtTrucker.OnOk -= cstxtTrucker_OnOk;
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                bsAirTruckList.PositionChanged -= bsAirTruck_PositionChanged;
                bsAirTruckList.DataSource = null;
                bsAirTruckList.Dispose();
                bsContainers.DataSource = null;
                bsContainers.Dispose();
                bsTruckInfo.DataSource = null;
                bsTruckInfo.Dispose();
                containerDemandControl1.TextChanged -= containerDemandControl1_TextChanged;
                
                containersBindingSource.DataSource = null;
                containersBindingSource.Dispose();
                Saved = null;
               
                _CurrentData = null;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }

            Load += delegate { txtCarrier.Focus(); };
        }

        private void SetCnText()
        {
            labNO.Text = "派车单号";
            labCarrier.Text = "船公司";
            labCreateBy.Text = "派车人";
            labCreateOn.Text = "派车时间";
            labDeliveryAt.Text = "还柜地";
            labCustomsBroker.Text = "报关行";
            labFreigtDescription.Text = "费用描述";
            labRemark.Text = "备注";
            labShipper.Text = "装货地";
            labShippingOrderNo.Text = "订舱号";
            labTrucker.Text = "拖车行";
            labVesselVoyage.Text = "船名航次";
            labContainerDemand.Text = "箱需求";
            labLoadingTime.Text = "要求装货时间";

            chkIsDrivingLicence.Text = "是否需要司机本";

            colTruckNO.Caption = "派车单号";
            colDeliveryAtName.Caption = "还柜地";
            colShipperName .Caption = "装货地";
            colTruckerName.Caption = "拖车行";
            colLoadingTime.Caption = "要求装货时间";

            colCtnNo.Caption = "箱号";
            colSealNo.Caption = "封号";
            colTypeID.Caption = "箱型";
            colDriver.Caption = "司机";
            colCarNo.Caption = "车牌号";
            colDeliveryDate.Caption = "出发时间";
            colArriveDate.Caption = "到达时间";
            colReturnDate.Caption = "还柜时间";
            colShippingOrderNo.Caption = "订舱号";


            groupContainer.Text = "箱信息";

            barAdd.Caption = "新增(&A)";
            barClose.Caption = "关闭(&C)";
            barDelete.Caption = "删除(&D)";
            barSave.Caption = "保存(&S)";
            barPrint.Caption = "打印(&P)";

            barNew.Caption = "新增(&N)";
            barRemove.Caption = "删除(&R)";

            barPrint.Caption = "打印(&P)";
            //barPickupDelivery.Caption = "";
        }

        #endregion 

        #region mothed

        void SearchRegister()
        {
            AirTruckInfo currentData = _CurrentData;
            #region Customer
            List<CountryList> countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            foreach (CountryList c in countryList)
            {
                stxtShipper.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtDeliveryAt.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
            }

            stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
            stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;

            stxtShipper.SetLanguage(LocalData.IsEnglish);
            stxtDeliveryAt.SetLanguage(LocalData.IsEnglish);

            #region Shipper

            //注册客户搜索器

            DataFindClientService.Register(stxtShipper, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    stxtShipper.Text = _CurrentData.ShipperName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtShipper.Tag = _CurrentData.ShipperID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.ShipperDescription);
                    stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
                    txtShipperDescription.Text = _CurrentData.ShipperDescription.ToString(LocalData.IsEnglish);
                },
                delegate()
                {
                    stxtShipper.Text = _CurrentData.ShipperName = string.Empty;
                    stxtShipper.Tag = _CurrentData.ShipperID = Guid.Empty;
                    txtShipperDescription.Text = string.Empty;
                },
                ClientConstants.MainWorkspace);

            stxtShipper.OnOk += delegate
            {
                txtShipperDescription.Text = _CurrentData.ShipperDescription.ToString(LocalData.IsEnglish);
            };

            #endregion

            #region DeliveryAt

            //注册客户搜索器

            DataFindClientService.Register(stxtDeliveryAt, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    stxtDeliveryAt.Text = _CurrentData.DeliveryAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtDeliveryAt.Tag = _CurrentData.DeliveryAtID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.DeliveryAtDescription);
                    stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;
                    txtDeliveryAtDescription.Text = _CurrentData.DeliveryAtDescription.ToString(LocalData.IsEnglish);
                },
                delegate()
                {
                    stxtDeliveryAt.Text = _CurrentData.DeliveryAtName = string.Empty;
                    stxtDeliveryAt.Tag = _CurrentData.DeliveryAtID = Guid.Empty;
                    txtDeliveryAtDescription.Text = string.Empty;
                },
                ClientConstants.MainWorkspace);

            stxtDeliveryAt.OnOk += delegate
            {
                txtDeliveryAtDescription.Text = _CurrentData.DeliveryAtDescription.ToString(LocalData.IsEnglish);
            };

            #endregion

            #endregion
        }

        private void InitControls()
        {
            panelScroll.Click += delegate { panelScroll.Focus(); };
            AirTruckInfo currentData = _CurrentData;

            List<ContainerList> ctnList = TransportFoundationService.GetContainerList(string.Empty, true, 0);
            foreach (ContainerList item in ctnList)
            {
                rcmbContainer.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }

            SetDescription();

            #region truck
            List<CustomerList> truckCustomers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty,
                                                        string.Empty, string.Empty, string.Empty,
                                                        null, null, null,
                                                        CustomerType.Trucker,
                                                        null, null, null, null, null, 1000);
            truckCustomers.Insert(0, null);

            cstxtTrucker.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            cstxtTrucker.ValueMember = "ID";
            cstxtTrucker.EditValueChanged += delegate
            {
                if (cstxtTrucker.EditValue != null && cstxtTrucker.EditValue.ToString().Length > 0)
                {
                    Guid id = new Guid(cstxtTrucker.EditValue.ToString());

                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.TruckerDescription);
                    cstxtTrucker.CustomerDescription = _CurrentData.TruckerDescription;
                    //_CurrentData.TruckerName = _CurrentData.TruckerDescription.Name;
                }
            };

            cstxtTrucker.OnOk += new EventHandler(cstxtTrucker_OnOk);
            cstxtTrucker.SetLanguage(LocalData.IsEnglish);
            cstxtTrucker.CustomerDescription = currentData.TruckerDescription;
            cstxtTrucker.DataSource = truckCustomers;
            //if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(currentData.TruckerID))
                cstxtTrucker.EditValue = _RecentTruckerID;

            #endregion

            if (_CurrentData.ContainerDescription != null)
            {
                containerDemandControl1.Text = _CurrentData.ContainerDescription.ToString();
            }

            #region broker

            List<CustomerList> brokerCustomers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty,
                                                                    string.Empty, string.Empty, string.Empty,
                                                                    null, null, null,
                                                                    CustomerType.Broker,
                                                                    null, null, null, null, null, 1000);

            cstxtCustomerBroker.CustomerDescription = new CustomerDescription()
            {
                Address = "add"
            };
            cstxtCustomerBroker.DisplayMember = "EName";
            cstxtCustomerBroker.ValueMember = "ID";
            cstxtCustomerBroker.EditValueChanged += delegate
            {
                if (cstxtCustomerBroker.EditValue != null && cstxtCustomerBroker.EditValue.ToString().Length > 0)
                {
                    Guid id = new Guid(cstxtCustomerBroker.EditValue.ToString());

                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.CustomsBrokerDescription);
                    cstxtCustomerBroker.CustomerDescription = _CurrentData.CustomsBrokerDescription;
                }
            };
            cstxtCustomerBroker.SetLanguage(LocalData.IsEnglish);
            cstxtCustomerBroker.DataSource = brokerCustomers;
            cstxtTrucker.CustomerDescription = currentData.CustomsBrokerDescription;
            if (ArgumentHelper.GuidIsNullOrEmpty(currentData.CustomsBrokerID))
            {
                cstxtCustomerBroker.EditValue = currentData.CustomsBrokerID = brokerCustomers[0].ID;
            }

            #endregion
        }

        void cstxtTrucker_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData == null) return;
            _CurrentData.TruckerName = _CurrentData.TruckerDescription.Name;
        }

        private void SetDescription()
        {
            AirTruckInfo currentData = _CurrentData;
            if (currentData.ShipperDescription == null)
            {
                currentData.ShipperDescription = new CustomerDescription();
                txtShipperDescription.Text = string.Empty;
            }
            else
            {
                txtShipperDescription.Text = currentData.ShipperDescription.ToString(LocalData.IsEnglish);
            }

            if (currentData.DeliveryAtDescription == null)
            {
                currentData.DeliveryAtDescription = new CustomerDescription();
                txtDeliveryAtDescription.Text = string.Empty;
            }
            else
            {
                txtDeliveryAtDescription.Text = currentData.DeliveryAtDescription.ToString(LocalData.IsEnglish);
            }

            if (currentData.CustomsBrokerDescription == null)
            {
                currentData.CustomsBrokerDescription = new CustomerDescription();
            }

            if (currentData.TruckerDescription == null)
            {
                currentData.TruckerDescription = new CustomerDescription();
            }
            else
            {
                currentData.TruckerName = currentData.TruckerDescription.Name;
            }

            if (currentData.ContainerDescription == null)
            {
                currentData.ContainerDescription = new ContainerDescription();
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            if (isReadOnly)
            {
                bsTruckInfo.DataSource = new AirTruckInfo ();
                groupBase.Enabled = false;
                gridControl2.Enabled = false;
            }
            else
            {
                groupBase.Enabled = true;
                gridControl2.Enabled = true;
            }
        }

        #endregion

        #region event

        void bsAirTruck_PositionChanged(object sender, EventArgs e)
        {
            if (bsTruckInfo.Current == null)
            {
                groupBase.Enabled = false;
                groupContainer.Enabled = false;

                bsTruckInfo.DataSource = typeof(AirTruckInfo);
                bsContainers.DataSource = typeof(List<AirContainerList>);

                return;
            }
            else
            {
                groupBase.Enabled = true;
                groupContainer.Enabled = true;
            }

            if (_CurrentData != null)
            {
                _CurrentData.PropertyChanged -= new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            }
            gvContainer.CellValueChanged -= new CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData = bsAirTruckList.Current as AirTruckInfo;
            if (_CurrentData == null) return;
            bsTruckInfo.DataSource = _CurrentData;
            bsContainers.DataSource = _CurrentData.Containers;
            SetDescription();
            cstxtTrucker.CustomerDescription = _CurrentData.TruckerDescription;
            cstxtCustomerBroker.CustomerDescription = _CurrentData.CustomsBrokerDescription ;
            stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
            stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;
            containerDemandControl1.Text = _CurrentData.ContainerDescription.ToString();

            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            gvContainer.CellValueChanged += new CellValueChangedEventHandler(gvContainer_CellValueChanged);
        }

        void gvContainer_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            SetCurrentDataCahnged();
        }

        private void SetCurrentDataCahnged()
        {
            if (_CurrentData == null) return;
            _CurrentData.PropertyChanged -= new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            gvContainer.CellValueChanged -= new CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData.IsDirty = true;
        }
        private void RegisterCurrentDataCahngedEvent()
        {
            if (_CurrentData == null) return;
            gvContainer.CellValueChanged -= new CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData.PropertyChanged -= new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            gvContainer.CellValueChanged += new CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
        }

        void _CurrentData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetCurrentDataCahnged();
        }

        private void gvContainer_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            SetCurrentDataCahnged();
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BaseDataObject list = gvMain.GetRow(e.RowHandle) as BaseDataObject;
            if (list == null)
            {
                return;
            }

            if (list.IsNew || list.IsDirty)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            }
        }

        #endregion

        #region bar

        #region Save
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            Save();
        }

        bool ValidateData()
        {
            EndEdit();

            List<AirTruckInfo> list = bsAirTruckList.DataSource as List<AirTruckInfo>;

            bool isScrr = true;
            List<Guid> truckIDs = new List<Guid>();
            foreach (AirTruckInfo item in list)
            {
                if (item.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (item.TruckerID != Guid.Empty && truckIDs.Contains(item.TruckerID))
                            e.SetErrorInfo("TruckerID", LocalData.IsEnglish ? "Trucker Exist." : "拖车行重复");

                        if (containerDemandControl1.Text.Trim().Length == 0)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "ContainerDemand Must Input." : "箱信息必须输入.");
                            dxErrorProvider1.SetError(containerDemandControl1, LocalData.IsEnglish ? "ContainerDemand Must Input." : "箱信息必须输入.");
                            isScrr = false;
                        }
                    }
                ) == false) isScrr = false;
            }

            return isScrr;

        }

        private bool Save()
        {
            if (ValidateData() == false) return false;

            List<AirTruckInfo> list = bsAirTruckList.DataSource as List<AirTruckInfo>;

            if (list == null)
            {
                return false;
            }

            if (list.FindAll(o => o.IsDirty).Count == 0)
            {
                return true;
            }

            List<HierarchyManyResult> results = new List<HierarchyManyResult>();

            try
            {
                foreach (AirTruckInfo truckItem in list)
                {
                    #region
                    HierarchyManyResult result = null;
                    //if (truckItem.IsDirty == false)
                    //{
                    //    results.Add(result);
                    //    continue;
                    //}

                    TruckSaveRequest saveRequest = new TruckSaveRequest();

                    saveRequest.id = truckItem.ID;
                    saveRequest.oceanBookingID = _AirBooking.ID;
                    saveRequest.truckerID = truckItem.TruckerID;
                    saveRequest.no = truckItem.NO;
                    saveRequest.sono = truckItem.ShippingOrderNo;
                    saveRequest.truckerDescription = truckItem.TruckerDescription;
                    saveRequest.pickUpAtID = truckItem.PickUpAtID;
                    saveRequest.pickUpAtDescription = truckItem.PickUpAtDescription;
                    saveRequest.shipperID = truckItem.ShipperID;
                    saveRequest.shipperDescription = truckItem.ShipperDescription;
                    saveRequest.loadTime = truckItem.LoadingTime;
                    saveRequest.deliveryAtID = truckItem.DeliveryAtID;
                    saveRequest.deliveryAtDescription = truckItem.DeliveryAtDescription;
                    saveRequest.isDrivingLicence = truckItem.IsDrivingLicence;
                    saveRequest.customsBrokerID = truckItem.CustomsBrokerID;
                    saveRequest.customsBrokerDescription = truckItem.CustomsBrokerDescription;
                    saveRequest.containerDescription = truckItem.ContainerDescription;
                    saveRequest.feeDescription = truckItem.FreigtDescription;
                    saveRequest.remark = truckItem.Remark;
                    saveRequest.updateDate = truckItem.UpdateDate;
                    saveRequest.saveByID = LocalData.UserInfo.LoginID;

                    result = AirExportService.SaveAirTruckServiceInfo(saveRequest);

                    results.Add(result);
                    #endregion
                }

                if (results == null || results.Count == 0) return false;
                for (int i = 0; i < list.Count; i++)
                {
                    if (results[i] == null) continue;
                    list[i].ID = results[i].GetValue<Guid>("ID");
                    list[i].UpdateDate = results[i].GetValue<DateTime?>("UpdateDate");
                    list[i].NO = results[i].GetValue<string>("NO");
                    for (int j = 0; j < results[i].Childs.Count; j++)
                    {
                        list[i].Containers[j].ID = results[i].Childs[j].GetValue<Guid>("ID");
                        list[i].Containers[j].ContainerID = results[i].Childs[j].GetValue<Guid>("AirContainerID");
                        list[i].UpdateDate = results[i].GetValue<DateTime?>("UpdateDate");
                    }
                    list[i].IsDirty = false;
                }
                bsTruckInfo.ResetBindings(false);
                gvMain.RefreshData();
                gvMain.BestFitColumns();
                AfterSave();
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return false; }
        }

        private void AfterSave()
        {
            List<AirTruckInfo> list = bsAirTruckList.DataSource as List<AirTruckInfo>;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].IsDirty = false;
            }
            RegisterCurrentDataCahngedEvent();
            if (Saved != null)
            {
                Saved(new object[] { DataSource });
            }

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }

        #endregion

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddNew();
        }

        //Guid 

        private void AddNew()
        {
            EndEdit();
            gvMain.Focus();

            bsAirTruckList.Insert(0, BulidNewTruck());
            gvMain.RefreshData();
            gvMain.FocusedRowHandle = 0;
            SetReadOnly(false);
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (bsAirTruckList.Current == null) return;

            if (PartLoader.EnquireIsDeleteCurrentData())
            {
                AirTruckInfo currentData = _CurrentData;

                if (currentData.IsNew)
                {
                    bsAirTruckList.RemoveCurrent();
                    if (bsAirTruckList.List == null || bsAirTruckList.List.Count == 0)
                    { SetReadOnly(true); return; }
                    bsAirTruck_PositionChanged(null, null);
                    return;
                }
                try
                {
                    AirExportService.RemoveAirTruckServiceInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
                    bsAirTruckList.RemoveCurrent();

                    if (bsAirTruckList.List == null || bsAirTruckList.List.Count == 0)
                    { SetReadOnly(true); return; }

                    bsAirTruck_PositionChanged(null, null);
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }

            }
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        private void gvContainer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) DeleteContainer();
        }

        private void DeleteContainer()
        {
            if (bsContainers.Current == null) return;
            if (PartLoader.EnquireIsDeleteCurrentData())
            {
                bsContainers.RemoveCurrent();
                SetCurrentDataCahnged();
            }
        }

        private void barNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddNewCtn();
        }

        private void AddNewCtn()
        {
            AirContainerList newData = new AirContainerList();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.ShippingOrderNo = (bsTruckInfo.Current as AirTruckInfo).ShippingOrderNo;

            bsContainers.Insert(0, newData);
            gvContainer.RefreshData();
            SetCurrentDataCahnged();
        }

        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteCtn();
        }

        private void DeleteCtn()
        {
            if (bsContainers.Current == null) return;

            int[] selectedIndex = gvContainer.GetSelectedRows();
            if (selectedIndex.Length <= 0) return;

            if (PartLoader.EnquireIsDeleteCurrentData())
            {

                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDates = new List<DateTime?>();
                foreach (var item in selectedIndex)
                {
                    AirContainerList ctn = gvContainer.GetRow(item) as AirContainerList;
                    if (ctn == null || ctn.ID == Guid.Empty) continue;

                    ids.Add(ctn.ID);
                    updateDates.Add(ctn.UpdateDate);
                }

                try
                {
                    if (ids.Count > 0)
                        AirExportService.RemoveAirTruckContainerInfo(ids.ToArray(), LocalData.UserInfo.LoginID, updateDates.ToArray());
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return; }

                foreach (var item in selectedIndex)
                {
                    gvContainer.DeleteRow(item);
                    gvContainer.RefreshData();
                }
                SetCurrentDataCahnged();
            }
        }

        #endregion

        #region Print

        /// <summary>
        /// TO DO:按钮被屏蔽,等国际化OK后加上
        /// </summary>
        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Print();
        }
        private void Print()
        {
            //if (_CurrentData.IsNew || _CurrentData.IsDirty)
            //{
            //    if (Save() == false) return;
            //}

            //AirExportPrintHelper.PrintPickupCN(_CurrentData.ID);
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            List<AirTruckInfo> truckList = data as List<AirTruckInfo>;
            if (truckList == null) truckList = new List<AirTruckInfo>();

            if (truckList.Count == 0) truckList.Add(BulidNewTruck());

            bsAirTruckList.DataSource = data;
            _CurrentData = bsAirTruckList.Current as AirTruckInfo;
            bsTruckInfo.DataSource = _CurrentData;
            SearchRegister();
            InitControls();

            bsAirTruckList.PositionChanged -= new EventHandler(bsAirTruck_PositionChanged);
            bsAirTruckList.PositionChanged += new EventHandler(bsAirTruck_PositionChanged);

            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);

            containerDemandControl1.TextChanged += new EventHandler(containerDemandControl1_TextChanged);
        }

        private AirTruckInfo BulidNewTruck()
        {
            AirTruckInfo newData = new AirTruckInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.AirBookingID = _AirBooking.ID;
            //newData.ShippingOrderNo = _AirBooking.AirShippingOrderNo;
            //newData.CarrierName = _AirBooking.CarrierName;
            //newData.VesselVoyage = _AirBooking.VesselVoyage;
            newData.LoadingTime = DateTime.Now;

            if (_RecentTruckerID != null)
            {
                newData.TruckerID = _RecentTruckerID.Value;
                newData.TruckerName = _RecentTruckerName;
                newData.TruckerDescription = Utility.Clone<CustomerDescription>(_RecentTruckerDes);
            }
            if (_RecentShipperID != null)
            {
                newData.ShipperID = _RecentShipperID.Value;
                newData.ShipperName = _RecentShipperName;
                newData.ShipperDescription = Utility.Clone<CustomerDescription>(_RecentShipperDes);
            }
            if (_returnLocationId != null)
            {
                newData.DeliveryAtID = _returnLocationId.Value;
                newData.DeliveryAtName = _recentReturnLocationName;
                newData.DeliveryAtDescription = Utility.Clone<CustomerDescription>(_recentReturnLocationDescription);
            }
            if (_bookingContainerDescription != null)
            {
                newData.ContainerDescription = _bookingContainerDescription;

                //this.containerDemandControl1.Text = newData.ContainerDescription.ToString();
            }
            newData.Remark = _recentRemark;
            if (_recentIsDrivingLicence !=null && _recentIsDrivingLicence.HasValue)
            {
                newData.IsDrivingLicence = _recentIsDrivingLicence.Value;
            }
            if (_recentCustomsBrokerID != null)
            {
                newData.CustomsBrokerID = _recentCustomsBrokerID.Value;
                newData.CustomsBrokerName = _recentCustomerBrokerName;
                newData.CustomsBrokerDescription = _recentCustomsBrokerDescription;
            }
            newData.BeginEdit();
            newData.IsDirty = true;
            return newData;
        }

        void BulidRecentData()
        {
            
        }

        void containerDemandControl1_TextChanged(object sender, EventArgs e)
        {
            if (_CurrentData != null)
            {
                _CurrentData.ContainerDescription = new ContainerDescription(containerDemandControl1.Text.Trim());
            }
        }

        public override object DataSource
        {
            get { return bsAirTruckList.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            Validate();
            bsContainers.EndEdit();
            bsTruckInfo.EndEdit();
            bsAirTruckList.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        #region IPart 成员

        IDictionary<string, object> _values;

        string _recentRemark = string.Empty;
        bool? _recentIsDrivingLicence = null;
        Guid? _recentCustomsBrokerID = null;
        Guid? _returnLocationId = null;
        CustomerDescription _recentReturnLocationDescription = null;
        string _recentReturnLocationName = string.Empty;

        ContainerDescription _bookingContainerDescription = null;

        public override void Init(IDictionary<string, object> values)
        {
            _values = values;
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "Booking".ToUpper())
                {
                    _AirBooking = item.Value as AirBookingList;
                }
                else if (item.Key.ToUpper() == "RecentTruckerID".ToUpper())
                {
                    if (item.Value != null)
                    {
                        _RecentTruckerID = new Guid(item.Value.ToString());
                    }
                }
                else if (item.Key.ToUpper() == "RecentShipperID".ToUpper())
                {
                    if (item.Value != null)
                    {
                        _RecentShipperID = new Guid(item.Value.ToString());
                    }
                }
                else if (item.Key.ToUpper() == "CustomsBrokerID".ToUpper())
                {
                    if (item.Value != null)
                    {
                        _recentCustomsBrokerID = new Guid(item.Value.ToString());
                    }
                }
                else if (item.Key.ToUpper() == "IsDrivingLicence".ToUpper())
                {
                    if (item.Value != null)
                    {
                        _recentIsDrivingLicence = (bool?)item.Value;
                    }
                }
            }

            _returnLocationId = (Guid?)values["ReturnLocationID"];

            _bookingContainerDescription = (ContainerDescription)values["ContainerDescription"];
            if (values["Remark"] != null)
            {
                _recentRemark = values["Remark"].ToString();
            }


            if (ArgumentHelper.GuidIsNullOrEmpty(_RecentTruckerID) == false)
            {
                _RecentTruckerDes = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_RecentTruckerID, _RecentTruckerDes);
                if (info != null) _RecentTruckerName = LocalData.IsEnglish ? info.EName : info.CName;
            }
            if (ArgumentHelper.GuidIsNullOrEmpty(_RecentShipperID) == false)
            {
                _RecentShipperDes = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_RecentShipperID, _RecentShipperDes);
                if (info != null)
                {
                    _RecentShipperName = LocalData.IsEnglish ? info.EName : info.CName;
                }
            }
            if (!ArgumentHelper.GuidIsNullOrEmpty(_returnLocationId))
            {
                _recentReturnLocationDescription = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_returnLocationId, _recentReturnLocationDescription);
                if (info != null)
                {
                    _recentReturnLocationName = LocalData.IsEnglish ? info.EName : info.CName;
                }
            }
            if (!ArgumentHelper.GuidIsNullOrEmpty(_recentCustomsBrokerID))
            {
                _recentCustomsBrokerDescription = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_recentCustomsBrokerID, _recentCustomsBrokerDescription);
                if (info != null)
                {
                    _recentCustomerBrokerName = LocalData.IsEnglish ? info.EName : info.CName;
                }
            }
        }

        string _recentCustomerBrokerName = string.Empty;
        CustomerDescription _recentCustomsBrokerDescription = null;
        #endregion

       
    }
}

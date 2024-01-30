using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;

namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class AirTruckEditPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        //[Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        //public AirExportPrintHelper AirExportPrintHelper { get; set; }

        [ServiceDependency]
        public IAirExportService oeService { get; set; }

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
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }

            this.Load += delegate { txtCarrier.Focus(); };
        }

        private void SetCnText()
        {
            this.labNO.Text = "派车单号";
            labCarrier.Text = "船公司";
            labCreateBy.Text = "派车人";
            this.labCreateOn.Text = "派车时间";
            labDeliveryAt.Text = "还柜地";
            labCustomsBroker.Text = "报关行";
            labFreigtDescription.Text = "费用描述";
            labRemark.Text = "备注";
            labShipper.Text = "装货地";
            labShippingOrderNo.Text = "订舱号";
            labTrucker.Text = "拖车行";
            labVesselVoyage.Text = "船名航次";
            labContainerDemand.Text = "箱需求";
            this.labLoadingTime.Text = "要求装货时间";

            chkIsDrivingLicence.Text = "是否需要司机本";

            this.colTruckNO.Caption = "派车单号";
            colDeliveryAtName.Caption = "还柜地";
            colShipperName .Caption = "装货地";
            colTruckerName.Caption = "拖车行";
            this.colLoadingTime.Caption = "要求装货时间";

            colCtnNo.Caption = "箱号";
            colSealNo.Caption = "封号";
            colTypeID.Caption = "箱型";
            this.colDriver.Caption = "司机";
            this.colCarNo.Caption = "车牌号";
            this.colDeliveryDate.Caption = "出发时间";
            this.colArriveDate.Caption = "到达时间";
            this.colReturnDate.Caption = "还柜时间";
            this.colShippingOrderNo.Caption = "订舱号";


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
            List<CountryList> countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            foreach (CountryList c in countryList)
            {
                stxtShipper.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtDeliveryAt.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
            }

            this.stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
            this.stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;

            this.stxtShipper.SetLanguage(LocalData.IsEnglish);
            this.stxtDeliveryAt.SetLanguage(LocalData.IsEnglish);

            #region Shipper

            //注册客户搜索器

            dfService.Register(this.stxtShipper, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtShipper.Text = _CurrentData.ShipperName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtShipper.Tag = _CurrentData.ShipperID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.ShipperDescription);
                    this.stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
                    txtShipperDescription.Text = _CurrentData.ShipperDescription.ToString(LocalData.IsEnglish);
                },
                delegate()
                {
                    this.stxtShipper.Text = _CurrentData.ShipperName = string.Empty;
                    this.stxtShipper.Tag = _CurrentData.ShipperID = Guid.Empty;
                    txtShipperDescription.Text = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            stxtShipper.OnOk += delegate
            {
                txtShipperDescription.Text = _CurrentData.ShipperDescription.ToString(LocalData.IsEnglish);
            };

            #endregion

            #region DeliveryAt

            //注册客户搜索器

            dfService.Register(this.stxtDeliveryAt, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtDeliveryAt.Text = _CurrentData.DeliveryAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtDeliveryAt.Tag = _CurrentData.DeliveryAtID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.DeliveryAtDescription);
                    this.stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;
                    txtDeliveryAtDescription.Text = _CurrentData.DeliveryAtDescription.ToString(LocalData.IsEnglish);
                },
                delegate()
                {
                    this.stxtDeliveryAt.Text = _CurrentData.DeliveryAtName = string.Empty;
                    this.stxtDeliveryAt.Tag = _CurrentData.DeliveryAtID = Guid.Empty;
                    txtDeliveryAtDescription.Text = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            stxtDeliveryAt.OnOk += delegate
            {
                txtDeliveryAtDescription.Text = _CurrentData.DeliveryAtDescription.ToString(LocalData.IsEnglish);
            };

            #endregion

            #endregion
        }

        private void InitControls()
        {
            this.panelScroll.Click += delegate { panelScroll.Focus(); };
            AirTruckInfo currentData = _CurrentData;

            List<ContainerList> ctnList = tfService.GetContainerList(string.Empty, true, 0);
            foreach (ContainerList item in ctnList)
            {
                rcmbContainer.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }

            SetDescription();

            #region truck
            List<CustomerList> truckCustomers = customerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty,
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
            //if (Utility.GuidIsNullOrEmpty(currentData.TruckerID))
                cstxtTrucker.EditValue = _RecentTruckerID;

            #endregion

            if (_CurrentData.ContainerDescription != null)
            {
                this.containerDemandControl1.Text = _CurrentData.ContainerDescription.ToString();
            }

            #region broker

            List<CustomerList> brokerCustomers = customerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty,
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
            if (Utility.GuidIsNullOrEmpty(currentData.CustomsBrokerID))
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
                currentData.ContainerDescription = new ICP.FCM.Common.ServiceInterface.DataObjects.ContainerDescription();
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            if (isReadOnly)
            {
                bsTruckInfo.DataSource = new AirTruckInfo ();
                this.groupBase.Enabled = false;
                this.gridControl2.Enabled = false;
            }
            else
            {
                this.groupBase.Enabled = true;
                this.gridControl2.Enabled = true;
            }
        }

        #endregion

        #region event

        void bsAirTruck_PositionChanged(object sender, EventArgs e)
        {
            if (bsTruckInfo.Current == null)
            {
                this.groupBase.Enabled = false;
                this.groupContainer.Enabled = false;

                bsTruckInfo.DataSource = typeof(AirTruckInfo);
                bsContainers.DataSource = typeof(List<AirContainerList>);

                return;
            }
            else
            {
                this.groupBase.Enabled = true;
                this.groupContainer.Enabled = true;
            }

            if (_CurrentData != null)
            {
                _CurrentData.PropertyChanged -= new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            }
            gvContainer.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData = bsAirTruckList.Current as AirTruckInfo;
            if (_CurrentData == null) return;
            bsTruckInfo.DataSource = _CurrentData;
            bsContainers.DataSource = _CurrentData.Containers;
            SetDescription();
            cstxtTrucker.CustomerDescription = _CurrentData.TruckerDescription;
            cstxtCustomerBroker.CustomerDescription = _CurrentData.CustomsBrokerDescription ;
            this.stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
            this.stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;
            this.containerDemandControl1.Text = _CurrentData.ContainerDescription.ToString();

            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            gvContainer.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvContainer_CellValueChanged);
        }

        void gvContainer_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SetCurrentDataCahnged();
        }

        private void SetCurrentDataCahnged()
        {
            if (_CurrentData == null) return;
            _CurrentData.PropertyChanged -= new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            gvContainer.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData.IsDirty = true;
        }
        private void RegisterCurrentDataCahngedEvent()
        {
            if (_CurrentData == null) return;
            gvContainer.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData.PropertyChanged -= new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
            gvContainer.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvContainer_CellValueChanged);
            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
        }

        void _CurrentData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetCurrentDataCahnged();
        }

        private void gvContainer_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            SetCurrentDataCahnged();
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BaseDataObject list = gvMain.GetRow(e.RowHandle) as BaseDataObject;
            if (list == null)
            {
                return;
            }

            if (list.IsNew || list.IsDirty)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
        }

        #endregion

        #region bar

        #region Save
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            Save();
        }

        bool ValidateData()
        {
            this.EndEdit();

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
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "ContainerDemand Must Input." : "箱信息必须输入.");
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
            if (this.ValidateData() == false) return false;

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

                    result = oeService.SaveAirTruckServiceInfo(saveRequest);

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
                this.bsTruckInfo.ResetBindings(false);
                this.gvMain.RefreshData();
                this.gvMain.BestFitColumns();
                AfterSave();
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
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
                Saved(new object[] { this.DataSource });
            }

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }

        #endregion

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNew();
        }

        //Guid 

        private void AddNew()
        {
            this.EndEdit();
            this.gvMain.Focus();

            bsAirTruckList.Insert(0, BulidNewTruck());
            gvMain.RefreshData();
            gvMain.FocusedRowHandle = 0;
            SetReadOnly(false);
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    oeService.RemoveAirTruckServiceInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
                    bsAirTruckList.RemoveCurrent();

                    if (bsAirTruckList.List == null || bsAirTruckList.List.Count == 0)
                    { SetReadOnly(true); return; }

                    bsAirTruck_PositionChanged(null, null);
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

            }
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
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
            newData.ShippingOrderNo = (this.bsTruckInfo.Current as ICP.FCM.AirExport.ServiceInterface.DataObjects.AirTruckInfo).ShippingOrderNo;

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
                        oeService.RemoveAirTruckContainerInfo(ids.ToArray(), LocalData.UserInfo.LoginID, updateDates.ToArray());
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return; }

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
        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

            this.bsAirTruckList.DataSource = data;
            _CurrentData = bsAirTruckList.Current as AirTruckInfo;
            this.bsTruckInfo.DataSource = _CurrentData;
            SearchRegister();
            InitControls();

            this.bsAirTruckList.PositionChanged -= new EventHandler(bsAirTruck_PositionChanged);
            this.bsAirTruckList.PositionChanged += new EventHandler(bsAirTruck_PositionChanged);

            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);

            this.containerDemandControl1.TextChanged += new EventHandler(containerDemandControl1_TextChanged);
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
            if (this._recentIsDrivingLicence !=null && this._recentIsDrivingLicence.HasValue)
            {
                newData.IsDrivingLicence = this._recentIsDrivingLicence.Value;
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
                _CurrentData.ContainerDescription = new ContainerDescription(this.containerDemandControl1.Text.Trim());
            }
        }

        public override object DataSource
        {
            get { return bsAirTruckList.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            this.Validate();
            bsContainers.EndEdit();
            bsTruckInfo.EndEdit();
            bsAirTruckList.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

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


            if (Utility.GuidIsNullOrEmpty(_RecentTruckerID) == false)
            {
                _RecentTruckerDes = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_RecentTruckerID, _RecentTruckerDes);
                if (info != null) _RecentTruckerName = LocalData.IsEnglish ? info.EName : info.CName;
            }
            if (Utility.GuidIsNullOrEmpty(_RecentShipperID) == false)
            {
                _RecentShipperDes = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_RecentShipperID, _RecentShipperDes);
                if (info != null)
                {
                    _RecentShipperName = LocalData.IsEnglish ? info.EName : info.CName;
                }
            }
            if (!Utility.GuidIsNullOrEmpty(_returnLocationId))
            {
                _recentReturnLocationDescription = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_returnLocationId, _recentReturnLocationDescription);
                if (info != null)
                {
                    _recentReturnLocationName = LocalData.IsEnglish ? info.EName : info.CName;
                }
            }
            if (!Utility.GuidIsNullOrEmpty(_recentCustomsBrokerID))
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

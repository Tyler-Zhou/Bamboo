using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class TruckEditPart  : BaseEditPart
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
        public ICP.Common.ServiceInterface.ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ITransportFoundationService>();
            }
        }

        public ICP.Common.ServiceInterface.IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.IGeographyService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public OtherBusinessPrintHelper OtherBusinessPrintHelper
        {
            get
            {
                return ClientHelper.Get<OtherBusinessPrintHelper, OtherBusinessPrintHelper>();
            }
        }

        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ICP.Common.ServiceInterface.ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICustomerService>();
            }
        }
        #endregion

        #region 本地变量

        TruckInfo _CurrentData = null;
        OtherBusinessInfo _OceanBooking = null;
        Guid? _RecentTruckerID = null;
        Guid? _RecentShipperID = null;
        string _RecentTruckerName = string.Empty;
        string _RecentShipperName = string.Empty;
        CustomerDescription _RecentTruckerDes = null;
        CustomerDescription _RecentShipperDes = null;

        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;
        #endregion

        #region init

        public TruckEditPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._bookingContainerDescription = null;
                this._CurrentData.PropertyChanged -= this._CurrentData_PropertyChanged;
                this._CurrentData = null;
                this._OceanBooking = null;
                this._recentCustomsBrokerDescription = null;
                this._recentReturnLocationDescription = null;
                this._RecentShipperDes = null;
                this._RecentTruckerDes = null;
                this._values = null;
                this.dxErrorContainers.DataSource = null;
                this.dxErrorProvider1.DataSource = null;
                this.gridControl1.DataSource = null;
                this.gridControl2.DataSource = null;
                this.bsContainers.DataSource = null;
                this.bsOceanTruckList.DataSource = null;
                this.bsOceanTruckList.PositionChanged -= this.bsOceanTruck_PositionChanged;
                this.bsTruckInfo.DataSource = null;
                this.bsContainers.Dispose();
                this.bsOceanTruckList.Dispose();
                this.bsTruckInfo.Dispose();
                this.Saved = null;

                
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
            colShipperName.Caption = "装货地";
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
        
        CustomerFinderBridge TruckerBridge;
        CustomerFinderBridge CustomerBrokerBridge;
        CustomerFinderBridge ShipperBridge;
        CustomerFinderBridge DeliveryAtBridge;
        CustomerFinderBridge BillToIDBridge;
        void SearchRegister()
        {
            TruckInfo currentData = _CurrentData;

            #region Customer
            List<CountryList> countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            foreach (CountryList c in countryList)
            { 
                stxtShipper.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtDeliveryAt.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtTrucker.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtCustomerBroker.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtBillToID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                //btnPUEmptyCNTR.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
            }

            if (_CurrentData != null)
            {
                this.stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
                this.stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;
                this.stxtTrucker.CustomerDescription = _CurrentData.TruckerDescription;
                this.stxtCustomerBroker.CustomerDescription = _CurrentData.CustomsBrokerDescription;
                if (_CurrentData.BillToDescription == null)
                {
                    this.stxtBillToID.CustomerDescription = new CustomerDescription();
                }
                else
                {
                    this.stxtBillToID.CustomerDescription = _CurrentData.BillToDescription;
                }
                //this.btnPUEmptyCNTR.CustomerDescription = _CurrentData.PUEmptyCNTRDescription;
            }

            this.stxtShipper.SetLanguage(LocalData.IsEnglish);
            this.stxtDeliveryAt.SetLanguage(LocalData.IsEnglish);
            this.stxtTrucker.SetLanguage(LocalData.IsEnglish);
            this.stxtCustomerBroker.SetLanguage(LocalData.IsEnglish);
            this.stxtBillToID.SetLanguage(LocalData.IsEnglish);
            //this.btnPUEmptyCNTR.SetLanguage(LocalData.IsEnglish);

            #region Truck

            //注册客户搜索器

            DataFindClientService.Register(this.stxtTrucker, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtTrucker.Text = _CurrentData.TruckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtTrucker.Tag = _CurrentData.TruckerID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.TruckerDescription);
                    this.stxtTrucker.CustomerDescription = _CurrentData.TruckerDescription;
                },
                delegate()
                {
                    this.stxtTrucker.Text = _CurrentData.TruckerName = string.Empty;
                    this.stxtTrucker.Tag = _CurrentData.TruckerID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            Utility.SetEnterToExecuteOnec(stxtTrucker, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                TruckerBridge = new CustomerFinderBridge(
               this.stxtTrucker,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.TruckerDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                TruckerBridge.Init();
            });
            stxtTrucker.OnOk += new EventHandler(stxtTrucker_OnOk);
            #endregion

            #region Customs

            //注册客户搜索器

            DataFindClientService.Register(this.stxtCustomerBroker, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtCustomerBroker.Text = _CurrentData.CustomsBrokerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtCustomerBroker.Tag = _CurrentData.CustomsBrokerID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.CustomsBrokerDescription);
                    this.stxtCustomerBroker.CustomerDescription = _CurrentData.CustomsBrokerDescription;
                },
                delegate()
                {
                    this.stxtCustomerBroker.Text = _CurrentData.CustomsBrokerName = string.Empty;
                    this.stxtCustomerBroker.Tag = _CurrentData.CustomsBrokerID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            Utility.SetEnterToExecuteOnec(stxtCustomerBroker, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                CustomerBrokerBridge = new CustomerFinderBridge(
               this.stxtCustomerBroker,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.CustomsBrokerDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                CustomerBrokerBridge.Init();
            });
            stxtCustomerBroker.OnOk += new EventHandler(stxtCustomerBroker_OnOk);
            #endregion

            #region Shipper

            //注册客户搜索器

            DataFindClientService.Register(this.stxtShipper, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
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

            Utility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                ShipperBridge = new CustomerFinderBridge(
               this.stxtShipper,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.ShipperDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                ShipperBridge.Init();
            });

            stxtShipper.OnOk += delegate
            {
                txtShipperDescription.Text = _CurrentData.ShipperDescription.ToString(LocalData.IsEnglish);
                if (_CurrentData != null && stxtShipper.CustomerDescription != null)
                {
                    _CurrentData.ShipperDescription = stxtShipper.CustomerDescription;
                }
            };

            #endregion

            #region DeliveryAt

            //注册客户搜索器

            DataFindClientService.Register(this.stxtDeliveryAt, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
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

            Utility.SetEnterToExecuteOnec(stxtDeliveryAt, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                DeliveryAtBridge = new CustomerFinderBridge(
               this.stxtDeliveryAt,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.DeliveryAtDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                DeliveryAtBridge.Init();
            });
            stxtDeliveryAt.OnOk += delegate
            {
                txtDeliveryAtDescription.Text = _CurrentData.DeliveryAtDescription.ToString(LocalData.IsEnglish);
                if (_CurrentData != null && stxtDeliveryAt.CustomerDescription != null)
                {
                    _CurrentData.DeliveryAtDescription = stxtDeliveryAt.CustomerDescription;
                }
            };

            #endregion

            #region 账单寄送

            DataFindClientService.Register(this.stxtBillToID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtBillToID.Text = _CurrentData.BillToName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtBillToID.Tag = _CurrentData.BillToID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.BillToDescription);
                    this.stxtBillToID.CustomerDescription = _CurrentData.BillToDescription;
                },
                delegate()
                {
                    this.stxtBillToID.Text = _CurrentData.BillToName = string.Empty;
                    this.stxtBillToID.Tag = _CurrentData.BillToID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            Utility.SetEnterToExecuteOnec(stxtBillToID, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                BillToIDBridge = new CustomerFinderBridge(
               this.stxtBillToID,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.BillToDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                BillToIDBridge.Init();
            });
            stxtBillToID.OnOk += delegate
            {

                if (_CurrentData != null && stxtBillToID.CustomerDescription != null)
                {
                    _CurrentData.BillToDescription = stxtBillToID.CustomerDescription;
                }
            };
            #endregion        

            #endregion
        }

        void stxtCustomerBroker_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtCustomerBroker.CustomerDescription != null)
            {
                _CurrentData.CustomsBrokerDescription = stxtCustomerBroker.CustomerDescription;
            }
        }

        void stxtTrucker_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtTrucker.CustomerDescription != null)
            {
                _CurrentData.TruckerDescription = stxtTrucker.CustomerDescription;
            }
        }

        private void InitControls()
        {
            this.panelScroll.Click += delegate { panelScroll.Focus(); };
            TruckInfo currentData = _CurrentData;

            List<ICP.Common.ServiceInterface.DataObjects.ContainerList> ctnList = TransportFoundationService.GetContainerList(string.Empty, true, 0);
            foreach (ICP.Common.ServiceInterface.DataObjects.ContainerList item in ctnList)
            {
                rcmbContainer.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }

            SetDescription();

            if (_CurrentData.ContainerDescription != null)
            {
                this.containerDemandControl1.Text = _CurrentData.ContainerDescription.ToString();
            }
        }

        void cstxtTrucker_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData == null) return;
            _CurrentData.TruckerName = _CurrentData.TruckerDescription.Name;
        }

        private void SetDescription()
        {
            TruckInfo currentData = _CurrentData;
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

            if (currentData.BillToDescription == null)
            {
                currentData.BillToDescription = new CustomerDescription();
            }

            //if (currentData.PUEmptyCNTRDescription == null)
            //{
            //    currentData.PUEmptyCNTRDescription = new CustomerDescription();
            //}
        }

        private void SetReadOnly(bool isReadOnly)
        {
            if (isReadOnly)
            {
                bsTruckInfo.DataSource = new TruckInfo();
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

        void bsOceanTruck_PositionChanged(object sender, EventArgs e)
        {
            if (bsTruckInfo.Current == null)
            {
                this.groupBase.Enabled = false;
                this.groupContainer.Enabled = false;

                bsTruckInfo.DataSource = typeof(TruckInfo);
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
            _CurrentData = bsOceanTruckList.Current as TruckInfo;
            if (_CurrentData == null) return;
            bsTruckInfo.DataSource = _CurrentData;
            bsContainers.DataSource = _CurrentData.Containers;
            SetDescription();
            stxtTrucker.CustomerDescription = _CurrentData.TruckerDescription;
            stxtCustomerBroker.CustomerDescription = _CurrentData.CustomsBrokerDescription;
            this.stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
            this.stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;
            this.stxtBillToID.CustomerDescription = _CurrentData.BillToDescription;
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

            List<TruckInfo> list = bsOceanTruckList.DataSource as List<TruckInfo>;

            bool isScrr = true;
            List<Guid> truckIDs = new List<Guid>();
            foreach (TruckInfo item in list)
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

            List<TruckInfo> list = bsOceanTruckList.DataSource as List<TruckInfo>;

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
                foreach (TruckInfo truckItem in list)
                {
                    #region
                    HierarchyManyResult result = null;               
                    TruckSaveRequest saveRequest = new TruckSaveRequest();

                    saveRequest.id = truckItem.ID;
                    saveRequest.oceanBookingID = _OceanBooking.ID;
                    saveRequest.truckerID = truckItem.TruckerID;
                    saveRequest.no = truckItem.NO;
                    saveRequest.sono = truckItem.ShippingOrderNo;
                    saveRequest.truckerDescription = truckItem.TruckerDescription;
                    //saveRequest.pickUpAtID = truckItem.PickUpAtID;
                    //saveRequest.pickUpAtDescription = truckItem.PickUpAtDescription;
                    saveRequest.shipperID = truckItem.ShipperID;
                    saveRequest.shipperDescription = truckItem.ShipperDescription;
                    //saveRequest.PUEmptyCNTRID = truckItem.PUEmptyCNTRID;
                    //saveRequest.PUEmptyCNTRDescription = truckItem.PUEmptyCNTRDescription;
                    saveRequest.BillToID = truckItem.BillToID;
                    saveRequest.BillToDescription = truckItem.BillToDescription;
                    saveRequest.Commodity = truckItem.Commodity;
                    //saveRequest.TotalPkgs = truckItem.TotalPkgs;
                    saveRequest.loadTime = truckItem.LoadingTime;
                    saveRequest.DeliveryDate = truckItem.DeliveryDate;
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

                    result = OtherBusinessService.SaveOBTruckServiceInfo(saveRequest, LocalData.IsEnglish);

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
                        list[i].Containers[j].ContainerID = results[i].Childs[j].GetValue<Guid>("OceanContainerID");
                        list[i].UpdateDate = results[i].GetValue<DateTime?>("UpdateDate");
                    }
                    list[i].IsDirty = false;
                    if (list[i].CustomsBrokerDescription != null)
                    {
                        list[i].CustomsBrokerDescription.IsDirty = false;
                    }
                    if (list[i].TruckerDescription != null)
                    {
                        list[i].TruckerDescription.IsDirty = false;
                    }
                    if (list[i].CustomsBrokerDescription != null)
                    {
                        list[i].CustomsBrokerDescription.IsDirty = false;
                    }
                    if (list[i].DeliveryAtDescription != null)
                    {
                        list[i].DeliveryAtDescription.IsDirty = false;
                    }
                    if (list[i].ShipperDescription != null)
                    {
                        list[i].ShipperDescription.IsDirty = false;
                    }
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
            List<TruckInfo> list = bsOceanTruckList.DataSource as List<TruckInfo>;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].IsDirty = false;
                if (list[i].CustomsBrokerDescription != null)
                {
                    list[i].CustomsBrokerDescription.IsDirty = false;
                }
                if (list[i].TruckerDescription != null)
                {
                    list[i].TruckerDescription.IsDirty = false;
                }
                if (list[i].CustomsBrokerDescription != null)
                {
                    list[i].CustomsBrokerDescription.IsDirty = false;
                }
                if (list[i].DeliveryAtDescription != null)
                {
                    list[i].DeliveryAtDescription.IsDirty = false;
                }
                if (list[i].ShipperDescription != null)
                {
                    list[i].ShipperDescription.IsDirty = false;
                }
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

            bsOceanTruckList.Insert(0, BulidNewTruck());
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
            if (bsOceanTruckList.Current == null) return;

            if (PartLoader.EnquireIsDeleteCurrentData())
            {
                TruckInfo currentData = _CurrentData;

                if (currentData.IsNew)
                {
                    bsOceanTruckList.RemoveCurrent();
                    if (bsOceanTruckList.List == null || bsOceanTruckList.List.Count == 0)
                    { SetReadOnly(true); return; }
                    bsOceanTruck_PositionChanged(null, null);
                    return;
                }
                try
                {
                    OtherBusinessService.RemoveOBTruckServiceInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate, LocalData.IsEnglish);
                    bsOceanTruckList.RemoveCurrent();

                    if (bsOceanTruckList.List == null || bsOceanTruckList.List.Count == 0)
                    { SetReadOnly(true); return; }

                    bsOceanTruck_PositionChanged(null, null);
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
            //AddNewCtn();
        }        

        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DeleteCtn();
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
            if (_CurrentData.IsNew || _CurrentData.IsDirty)
            {
                if (Save() == false) return;
            }

            //OtherBusinessInfo info = obService.GetOtherBusinessInfo(_OceanBooking.ID, LocalData.IsEnglish);
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_OceanBooking.CompanyID);
            if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
            {              
                //国内派车单
                OtherBusinessPrintHelper.PrintPickupCN(_CurrentData.ID);
            }
            else
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OtherTruckInfo", _CurrentData);
                stateValues.Add("OtherBookingListID", _OceanBooking.ID);
                stateValues.Add("CompanyID", _OceanBooking.CompanyID);
                string title = (LocalData.IsEnglish ? "Delivery Order Print" : "打印派车单");
                PartLoader.ShowEditPart<OBUSPickUpReport>(Workitem, null, stateValues, title, null, null);
            }
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            List<TruckInfo> truckList = data as List<TruckInfo>;
            if (truckList == null) truckList = new List<TruckInfo>();

            if (truckList.Count == 0) truckList.Add(BulidNewTruck());

            this.bsOceanTruckList.DataSource = data;
            _CurrentData = bsOceanTruckList.Current as TruckInfo;
            this.bsTruckInfo.DataSource = _CurrentData;
            SearchRegister();
            InitControls();

            this.bsOceanTruckList.PositionChanged -= new EventHandler(bsOceanTruck_PositionChanged);
            this.bsOceanTruckList.PositionChanged += new EventHandler(bsOceanTruck_PositionChanged);

            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);

            this.containerDemandControl1.TextChanged += new EventHandler(containerDemandControl1_TextChanged);
        }

        private TruckInfo BulidNewTruck()
        {
            TruckInfo newData = new TruckInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.OBBookingID = _OceanBooking.ID;
            newData.ShippingOrderNo = _OceanBooking.SoNo;
            newData.CarrierName = _OceanBooking.CarrierName;
            newData.VesselVoyage = _OceanBooking.VesselVoyage;
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
            if (this._recentIsDrivingLicence != null && this._recentIsDrivingLicence.HasValue)
            {
                newData.IsDrivingLicence = this._recentIsDrivingLicence.Value;
            }
            if (_recentCustomsBrokerID != null)
            {
                newData.CustomsBrokerID = _recentCustomsBrokerID.Value;
                newData.CustomsBrokerName = _recentCustomerBrokerName;
                newData.CustomsBrokerDescription = _recentCustomsBrokerDescription;
            }

            if (_OceanBooking.CompanyID != Guid.Empty)
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_OceanBooking.CompanyID);
                if (configureInfo != null)
                {
                    newData.BillToID = configureInfo.CustomerID;
                    newData.BillToName = configureInfo.CustomerName;
                }
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
            get { return bsOceanTruckList.DataSource; }
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
            bsOceanTruckList.EndEdit();
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
                    OtherBusinessList bookingList = item.Value as OtherBusinessList;
                    _OceanBooking = OtherBusinessService.GetOtherBusinessInfo(bookingList.ID, bookingList.CompanyID);
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


            if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_RecentTruckerID) == false)
            {
                _RecentTruckerDes = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_RecentTruckerID, _RecentTruckerDes);
                if (info != null) _RecentTruckerName = LocalData.IsEnglish ? info.EName : info.CName;
            }
            if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_RecentShipperID) == false)
            {
                _RecentShipperDes = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_RecentShipperID, _RecentShipperDes);
                if (info != null)
                {
                    _RecentShipperName = LocalData.IsEnglish ? info.EName : info.CName;
                }
            }
            if (!ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_returnLocationId))
            {
                _recentReturnLocationDescription = new CustomerDescription();
                CustomerInfo info = ICPCommUIHelper.SetCustomerDesByID(_returnLocationId, _recentReturnLocationDescription);
                if (info != null)
                {
                    _recentReturnLocationName = LocalData.IsEnglish ? info.EName : info.CName;
                }
            }
            if (!ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_recentCustomsBrokerID))
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

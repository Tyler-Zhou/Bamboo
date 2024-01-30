﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;
using System.Linq;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using ICP.Business.Common.UI;
namespace ICP.FCM.OceanExport.UI.Booking
{
    /// <summary>
    /// 拖车编辑界面
    /// </summary>
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OceanTruckEditPart : BaseEditPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            get;
            set;
        }

        private ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {

                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }

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

        public ICP.Common.ServiceInterface.ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICustomerService>();
            }
        }


        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }


        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }


        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region 本地变量

        OceanTruckInfo _CurrentData = null;
        OceanBookingList _OceanBooking = null;
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

        /// <summary>
        /// 客户联系人
        /// </summary>
        List<CustomerCarrierObjects> customerlist = new List<CustomerCarrierObjects>();

        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;
        #endregion

        #region init

        public OceanTruckEditPart()
        {
            InitializeComponent();
            SyncLocalData = true;
            this.Disposed += delegate
            {
                if (_CurrentData != null)
                {
                    _CurrentData.PropertyChanged -= new PropertyChangedEventHandler(_CurrentData_PropertyChanged);
                }
                this.gvContainer.CellValueChanged -= this.gvContainer_CellValueChanged;
                this.gvContainer.InitNewRow -= this.gvContainer_InitNewRow;
                this.gvContainer.KeyDown -= this.gvContainer_KeyDown;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.containerDemandControl1.TextChanged -= new EventHandler(containerDemandControl1_TextChanged);
                this._CurrentData = null;
                this._OceanBooking = null;
                this._recentReturnLocationDescription = null;
                this._RecentShipperDes = null;
                this._RecentShipperDes = null;
                this.dxErrorContainers.DataSource = null;
                this.dxErrorProvider1.DataSource = null;
                this._businessOperationParameter = null;
                this.gridControl1.DataSource = null;
                this.gridControl2.DataSource = null;
                this.bsOceanTruckList.DataSource = null;
                this.bsOceanTruckList = null;
                this.bsTruckInfo.DataSource = null;
                this.bsTruckInfo.Dispose();
                this.bsContainers.DataSource = null;

                this.bsContainers.PositionChanged -= this.bsOceanTruck_PositionChanged;
                this.bsContainers.Dispose();
                this.stxtCustomerBroker.OnOk -= this.stxtCustomerBroker_OnOk;
                this.stxtTrucker.OnOk -= this.stxtTrucker_OnOk;
                this.stxtShipper.OnOk -= this.stxtShipper_OnOk;
                this.stxtDeliveryAt.OnOk -= this.stxtDeliveryAt_OnOk;
                this.stxtBillToID.OnOk -= this.stxtBillToID_OnOk;
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(this);
                    RootWorkItem = null;
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
        CustomerContactFinderBridge checkerCustomerFinderBridge;
        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge deliveryAtBridge;
        CustomerFinderBridge billToBridge;
        CustomerFinderBridge customerBrokerBridge;
        CustomerFinderBridge puEmptyCnterBridge;
        void SearchRegister()
        {
            OceanTruckInfo currentData = _CurrentData;

            #region Customer
            List<CountryList> countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            List<string> countryNames = (from item in countryList select LocalData.IsEnglish ? item.EName : item.CName).ToList();
            this.stxtShipper.Properties.BeginUpdate();
            this.stxtDeliveryAt.Properties.BeginUpdate();
            //this.stxtTrucker.Properties.BeginUpdate();

            this.stxtCustomerBroker.Properties.BeginUpdate();
            this.btnPUEmptyCNTR.Properties.BeginUpdate();
            this.stxtBillToID.Properties.BeginUpdate();
            foreach (var name in countryNames)
            {
                stxtShipper.CountryItems.Add(new ImageComboBoxItem(name));
                stxtDeliveryAt.CountryItems.Add(new ImageComboBoxItem(name));
                //stxtTrucker.d.Add(new ImageComboBoxItem(name));
                stxtCustomerBroker.CountryItems.Add(new ImageComboBoxItem(name));
                stxtBillToID.CountryItems.Add(new ImageComboBoxItem(name));
                btnPUEmptyCNTR.CountryItems.Add(new ImageComboBoxItem(name));
            }
            this.stxtCustomerBroker.Properties.EndUpdate();
            this.btnPUEmptyCNTR.Properties.EndUpdate();
            this.stxtBillToID.Properties.EndUpdate();

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
                this.btnPUEmptyCNTR.CustomerDescription = _CurrentData.PUEmptyCNTRDescription;
            }

            this.stxtShipper.SetLanguage(LocalData.IsEnglish);
            this.stxtDeliveryAt.SetLanguage(LocalData.IsEnglish);
            //this.stxtTrucker.SetLanguage(LocalData.IsEnglish);
            this.stxtCustomerBroker.SetLanguage(LocalData.IsEnglish);
            this.stxtBillToID.SetLanguage(LocalData.IsEnglish);
            this.btnPUEmptyCNTR.SetLanguage(LocalData.IsEnglish);

            #region Truck

            //注册客户搜索器

            DataFindClientService.Register(stxtTrucker, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
          delegate(object inputSource, object[] resultData)
          {
              stxtTrucker.Text = _CurrentData.TruckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
              stxtTrucker.Tag = _CurrentData.TruckerID = new Guid(resultData[0].ToString());
              AddLastestContact(_CurrentData.TruckerID, stxtTrucker, ContactType.Customer);
              Guid id = new Guid(resultData[0].ToString());
              this.stxtTrucker.SetCustomerID(id);
              CustomerDescription _customerDescription = new CustomerDescription();
              this.ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
              this.stxtTrucker.CustomerDescription = _customerDescription;

          }, delegate
          {
              stxtTrucker.Tag = _CurrentData.TruckerID = Guid.Empty;
              stxtTrucker.Text = _CurrentData.TruckerName = string.Empty;
              this.stxtTrucker.SetCustomerID(Guid.Empty);
              stxtTrucker.ContactList = new List<CustomerCarrierObjects>();
              this.stxtTrucker.CustomerDescription = new ICP.Framework.CommonLibrary.Common.CustomerDescription();
          }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);


            OEUtility.SetEnterToExecuteOnec(stxtTrucker, delegate
            {
                List<CustomerCarrierObjects> contactList = customerlist.FindAll(item => item.CustomerID == this._CurrentData.TruckerID);
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(this._CurrentData.TruckerID))
                {
                    this._CurrentData.TruckerID = Guid.Empty;
                }
                checkerCustomerFinderBridge = new CustomerContactFinderBridge(this.stxtTrucker, null, contactList, ContactStage.Trk, this._CurrentData.TruckerID, true, ContactType.Customer);
                checkerCustomerFinderBridge.Init();
            });

            stxtTrucker.OnOk += new EventHandler(stxtTrucker_OnOk);
            stxtTrucker.OnRefresh += new EventHandler(stxtTrucker_OnRefresh);
            stxtTrucker.BeforeEditValueChanged += new DevExpress.XtraEditors.Controls.ChangingEventHandler(stxtTrucker_EditValueChanging);
            stxtTrucker.AfterEditValueChanged += new EventHandler(stxtTrucker_EditValueChanged);

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

            OEUtility.SetEnterToExecuteOnec(stxtCustomerBroker, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                customerBrokerBridge = new CustomerFinderBridge(
               this.stxtCustomerBroker,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.CustomsBrokerDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                customerBrokerBridge.Init();
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

            OEUtility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                shipperBridge = new CustomerFinderBridge(
               this.stxtShipper,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.ShipperDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                shipperBridge.Init();
            });
            stxtShipper.OnOk += new EventHandler(stxtShipper_OnOk);


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

            OEUtility.SetEnterToExecuteOnec(stxtDeliveryAt, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                deliveryAtBridge = new CustomerFinderBridge(
               this.stxtDeliveryAt,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.DeliveryAtDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                deliveryAtBridge.Init();
            });

            stxtDeliveryAt.OnOk += new EventHandler(stxtDeliveryAt_OnOk);


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

            OEUtility.SetEnterToExecuteOnec(stxtBillToID, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                billToBridge = new CustomerFinderBridge(
               this.stxtBillToID,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.BillToDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                billToBridge.Init();
            });

            stxtBillToID.OnOk += new EventHandler(stxtBillToID_OnOk);

            #endregion

            #region 还空地

            DataFindClientService.Register(this.btnPUEmptyCNTR, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.btnPUEmptyCNTR.Text = _CurrentData.PUEmptyCNTRName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.btnPUEmptyCNTR.Tag = _CurrentData.PUEmptyCNTRID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.PUEmptyCNTRDescription);
                    this.btnPUEmptyCNTR.CustomerDescription = _CurrentData.PUEmptyCNTRDescription;
                },
                delegate()
                {
                    this.btnPUEmptyCNTR.Text = _CurrentData.PUEmptyCNTRName = string.Empty;
                    this.btnPUEmptyCNTR.Tag = _CurrentData.PUEmptyCNTRID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            OEUtility.SetEnterToExecuteOnec(btnPUEmptyCNTR, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                puEmptyCnterBridge = new CustomerFinderBridge(
               this.btnPUEmptyCNTR,
               _countryList,
               this.DataFindClientService,
               this.CustomerService,
               _CurrentData.PUEmptyCNTRDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                puEmptyCnterBridge.Init();
            });
            btnPUEmptyCNTR.OnOk += new EventHandler(btnPUEmptyCNTR_OnOk);

            #endregion

            #endregion
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            txtShipperDescription.Text = _CurrentData.ShipperDescription.ToString(LocalData.IsEnglish);
            if (_CurrentData != null && stxtShipper.CustomerDescription != null)
            {
                _CurrentData.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        void stxtDeliveryAt_OnOk(object sender, EventArgs e)
        {
            txtDeliveryAtDescription.Text = _CurrentData.DeliveryAtDescription.ToString(LocalData.IsEnglish);
            if (_CurrentData != null && stxtDeliveryAt.CustomerDescription != null)
            {
                _CurrentData.DeliveryAtDescription = stxtDeliveryAt.CustomerDescription;
            }
        }

        void stxtBillToID_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtBillToID.CustomerDescription != null)
            {
                _CurrentData.BillToDescription = stxtBillToID.CustomerDescription;
            }
        }

        void stxtCustomerBroker_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtCustomerBroker.CustomerDescription != null)
            {
                _CurrentData.CustomsBrokerDescription = stxtCustomerBroker.CustomerDescription;
            }
        }

        void btnPUEmptyCNTR_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && btnPUEmptyCNTR.CustomerDescription != null)
            {
                _CurrentData.PUEmptyCNTRDescription = btnPUEmptyCNTR.CustomerDescription;
            }
        }


        void stxtTrucker_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtTrucker.CustomerDescription != null)
            {
                _CurrentData.TruckerDescription = stxtTrucker.CustomerDescription;
            }

            List<CustomerCarrierObjects> currentList = this.stxtTrucker.ContactList;
            customerlist.RemoveAll(item => item.CustomerID == this._CurrentData.TruckerID && item.Type == ContactType.Customer);
            if (currentList.Count > 0)
            {
                foreach (var item in currentList)
                {
                    item.UpdateByID = LocalData.UserInfo.LoginID;
                }
                customerlist.AddRange(currentList);
                //this.UCBookingOtherPart.InsertContactList(currentList);
            }
        }


        void stxtTrucker_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (this.EditMode == EditMode.New)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanExport, this._OceanBooking.CompanyID, this._CurrentData.TruckerID, ContactType.Customer, ContactStage.Trk);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(this._OceanBooking.ID, OperationType.OceanExport, ContactType.Customer, ContactStage.Trk, this._CurrentData.TruckerID);
            }

            List<CustomerCarrierObjects> contactList = customerlist.FindAll(item => item.CustomerID == this._CurrentData.TruckerID && item.Type == ContactType.Customer);
            customerlist.RemoveAll(item => contactList.Contains(item));
            customerlist.AddRange(temp);
            this.stxtTrucker.ContactList = contactList;

            this.stxtTrucker.ContactList = temp;
        }


        void stxtTrucker_EditValueChanging(object sender, EventArgs e)
        {
            if (!ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.TruckerID))
            {
                RemoveContactList((Guid)this._CurrentData.TruckerID);
            }
        }

        void stxtTrucker_EditValueChanged(object sender, EventArgs e)
        {
            AddLastestContact((Guid)this._CurrentData.TruckerID, stxtTrucker, ContactType.Customer);
        }


        private void InitControls()
        {
            this.panelScroll.Click += delegate { panelScroll.Focus(); };
            OceanTruckInfo currentData = _CurrentData;

            List<ContainerList> ctnList = TransportFoundationService.GetContainerList(string.Empty, true, 0);
            foreach (ContainerList item in ctnList)
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
            OceanTruckInfo currentData = _CurrentData;
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

            if (currentData.PUEmptyCNTRDescription == null)
            {
                currentData.PUEmptyCNTRDescription = new CustomerDescription();
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            if (isReadOnly)
            {
                bsTruckInfo.DataSource = new OceanTruckInfo();
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

        #region 添加对应客户的上一票业务的对应沟通阶段的联系人
        /// <summary>
        /// 添加对应客户的上一票业务的对应沟通阶段的联系人
        /// </summary>
        /// <param name="customerID"></param>
        private void AddLastestContact(Guid customerID, object customerControl, ContactType contactType)
        {
            if (customerID == Guid.Empty)
            {
                return;
            }
            List<CustomerCarrierObjects> contactList = FCMCommonService.GetLatestContactList(OperationType.OceanExport, this._OceanBooking.CompanyID, customerID, contactType, ContactStage.SO);
            if (contactList == null || contactList.Count <= 0)
                return;
            for (int i = 0; i < contactList.Count; i++)
            {
                contactList[i].Id = Guid.Empty;

            }

            List<CustomerCarrierObjects> currentContactList = this.customerlist;
            if (currentContactList == null || currentContactList.Count <= 0)
            {
                this.customerlist.AddRange(contactList);
            }
            else
            {
                List<string> nameList = (from item in currentContactList select item.Name).ToList();
                List<string> emailList = (from item in currentContactList select item.Mail).ToList();

                List<CustomerCarrierObjects> needAddContactList = contactList.FindAll(item => !nameList.Contains(item.Name) && !emailList.Contains(item.Mail));
                this.customerlist.AddRange(needAddContactList);
            }

            customerControl.GetType().GetProperty("ContactList").SetValue(customerControl, contactList, null);

        }
        #endregion

        private void RemoveContactList(Guid changeID)
        {
            if (changeID != Guid.Empty && (this._CurrentData.TruckerID != Guid.Empty && this._CurrentData.TruckerID != changeID))
            {
                RemoveContactList(changeID, null);
            }
        }

        private void RemoveContactList(Guid customerID, ContactType? contactType)
        {
            if (contactType != null)
            {
                this.customerlist.RemoveAll(item => item.CustomerID == customerID && item.Type == contactType);
            }
            else
            {
                this.customerlist.RemoveAll(item => item.CustomerID == customerID);
            }
        }

        #region event

        void bsOceanTruck_PositionChanged(object sender, EventArgs e)
        {
            if (bsTruckInfo.Current == null)
            {
                this.groupBase.Enabled = false;
                this.groupContainer.Enabled = false;

                bsTruckInfo.DataSource = typeof(OceanTruckInfo);
                bsContainers.DataSource = typeof(List<OceanContainerList>);

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
            _CurrentData = bsOceanTruckList.Current as OceanTruckInfo;
            if (_CurrentData == null) return;
            bsTruckInfo.DataSource = _CurrentData;
            bsContainers.DataSource = _CurrentData.Containers;
            SetDescription();
            stxtTrucker.CustomerDescription = _CurrentData.TruckerDescription;
            stxtCustomerBroker.CustomerDescription = _CurrentData.CustomsBrokerDescription;
            this.stxtShipper.CustomerDescription = _CurrentData.ShipperDescription;
            this.stxtDeliveryAt.CustomerDescription = _CurrentData.DeliveryAtDescription;
            this.stxtBillToID.CustomerDescription = _CurrentData.BillToDescription;
            this.btnPUEmptyCNTR.CustomerDescription = _CurrentData.PUEmptyCNTRDescription;
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
            if (Save() == true)
            {
                SaveContact();
            }
        }

        bool ValidateData()
        {
            this.EndEdit();

            List<OceanTruckInfo> list = bsOceanTruckList.DataSource as List<OceanTruckInfo>;

            bool isScrr = true;
            List<Guid> truckIDs = new List<Guid>();
            foreach (OceanTruckInfo item in list)
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

        private bool SaveContact()
        {
            try
            {
                if (customerlist.Count == 0 || customerlist.FindAll(o => o.IsDirty).Count == 0)
                {

                    return true;
                }
                FCMCommonService.SaveContactList(customerlist);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false;
            }
            return true;
        }


        private bool Save()
        {
            if (this.ValidateData() == false) return false;

            List<OceanTruckInfo> list = bsOceanTruckList.DataSource as List<OceanTruckInfo>;

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
                foreach (OceanTruckInfo truckItem in list)
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
                    saveRequest.oceanBookingID = _OceanBooking.ID;
                    saveRequest.truckerID = truckItem.TruckerID;
                    saveRequest.no = truckItem.NO;
                    saveRequest.sono = truckItem.ShippingOrderNo;
                    saveRequest.truckerDescription = truckItem.TruckerDescription;
                    //saveRequest.pickUpAtID = truckItem.PickUpAtID;
                    //saveRequest.pickUpAtDescription = truckItem.PickUpAtDescription;
                    saveRequest.shipperID = truckItem.ShipperID;
                    saveRequest.shipperDescription = truckItem.ShipperDescription;
                    saveRequest.PUEmptyCNTRID = truckItem.PUEmptyCNTRID;
                    saveRequest.PUEmptyCNTRDescription = truckItem.PUEmptyCNTRDescription;
                    saveRequest.BillToID = truckItem.BillToID;
                    saveRequest.BillToDescription = truckItem.BillToDescription;
                    saveRequest.Commodity = truckItem.Commodity;
                    saveRequest.TotalPkgs = truckItem.TotalPkgs;
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
                    saveRequest.TruckingCharge = truckItem.TruckingCharge;

                    result = OceanExportService.SaveOceanTruckServiceInfo(saveRequest);

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
            List<OceanTruckInfo> list = bsOceanTruckList.DataSource as List<OceanTruckInfo>;
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
                if (_businessOperationParameter == null)
                {
                    _businessOperationParameter = new BusinessOperationParameter();
                }
                else
                {
                    _businessOperationParameter.Context.OperationType = OperationType.OceanExport;
                    _businessOperationParameter.Context.FormType = FormType.Customs;
                }
                Saved(new object[] { null, _businessOperationParameter, _businessOperationParameter.Context });
            }

            if (this.EditMode == EditMode.New)
            {
                this.EditMode = EditMode.Edit;
            }

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }

        #endregion

        private BusinessOperationContext GetContext(OceanTruckInfo oceantruckInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = oceantruckInfo.OceanBookingID;
            context.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;
            context.FormId = oceantruckInfo.OceanBookingID;
            context.FormType = ICP.Framework.CommonLibrary.Common.FormType.Truck;
            return context;
        }


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
                OceanTruckInfo currentData = _CurrentData;

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
                    OceanExportService.RemoveOceanTruckServiceInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
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
            AddNewCtn();
        }

        private void AddNewCtn()
        {
            OceanContainerList newData = new OceanContainerList();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.ShippingOrderNo = (this.bsTruckInfo.Current as ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanTruckInfo).ShippingOrderNo;

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
                    OceanContainerList ctn = gvContainer.GetRow(item) as OceanContainerList;
                    if (ctn == null || ctn.ID == Guid.Empty) continue;

                    ids.Add(ctn.ID);
                    updateDates.Add(ctn.UpdateDate);
                }

                try
                {
                    if (ids.Count > 0)
                        OceanExportService.RemoveOceanTruckContainerInfo(ids.ToArray(), LocalData.UserInfo.LoginID, updateDates.ToArray());
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
            if (_CurrentData.IsNew || _CurrentData.IsDirty)
            {
                if (Save() == false) return;
            }

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_OceanBooking.CompanyID);
            if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
            {
                //国内派车单
                OceanExportPrintHelper.PrintPickupCN(_CurrentData.ID, _OceanBooking.ID);
            }
            else
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanTruckInfo", _CurrentData);
                stateValues.Add("OceanBookingListID", _OceanBooking.ID);
                string title = (LocalData.IsEnglish ? "Delivery Order Print" : "打印派车单");
                PartLoader.ShowEditPart<OEUSPickUpReport>(RootWorkItem, null, stateValues, title, null, null);
            }
        }

        #endregion
        private delegate void DataBindingDelegate(List<OceanTruckInfo> truckList);
        #region IEditPart 成员
        public void BindingData(object data)
        {
            if (_values.ContainsKey("Booking"))
            {
                _OceanBooking = _values["Booking"] as OceanBookingList;
            }
            else
            {
                Guid bookingId = (Guid)_values["BookingId"];
                _OceanBooking = OceanExportService.GetOceanBookingListByIds(new Guid[] { bookingId }).First();
                customerlist = FCMCommonService.GetContactList(bookingId, OperationType.OceanExport).CustomerCarrier.FindAll(e => e.Type == ContactType.Customer);
            }
            List<OceanTruckInfo> truckList = data as List<OceanTruckInfo>;
            if (truckList == null)
            {
                truckList = OceanExportService.GetOceanTruckServiceList(_OceanBooking.ID);
                SingleResult recentData = OceanExportService.GetTruckRecentData(_OceanBooking.ID);
                IDictionary<string, object> temp = Convert(recentData);
                InnerInit(temp);
            }

            InnerBindingData(truckList);

        }
        private IDictionary<string, object> Convert(SingleResult recentData)
        {
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            if (recentData != null)
            {
                stateValues.Add("RecentTruckerID", recentData.GetValue<Guid?>("TruckerID"));
                stateValues.Add("RecentShipperID", recentData.GetValue<Guid?>("ShipperID"));
                stateValues.Add("ReturnLocationID", recentData.GetValue<Guid?>("ReturnLocationID"));
                stateValues.Add("ContainerDescription", SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), recentData.GetValue<string>("ContainerDescription")));
                stateValues.Add("CustomsBrokerID", recentData.GetValue<Guid?>("CustomsBrokerID"));
                stateValues.Add("IsDrivingLicence", recentData.GetValue<bool?>("IsDrivingLicence"));
                stateValues.Add("Remark", recentData.GetValue<string>("Remark"));
            }
            return stateValues;
        }
        void InnerBindingData(List<OceanTruckInfo> truckList)
        {

            if (truckList == null) truckList = new List<OceanTruckInfo>();

            if (truckList.Count == 0) truckList.Add(BulidNewTruck());

            this.bsOceanTruckList.DataSource = truckList;
            _CurrentData = bsOceanTruckList.Current as OceanTruckInfo;
            this.bsTruckInfo.DataSource = _CurrentData;
            SearchRegister();
            InitControls();

            this.bsOceanTruckList.PositionChanged -= new EventHandler(bsOceanTruck_PositionChanged);
            this.bsOceanTruckList.PositionChanged += new EventHandler(bsOceanTruck_PositionChanged);

            _CurrentData.PropertyChanged += new PropertyChangedEventHandler(_CurrentData_PropertyChanged);

            this.containerDemandControl1.TextChanged += new EventHandler(containerDemandControl1_TextChanged);
        }

        private OceanTruckInfo BulidNewTruck()
        {
            OceanTruckInfo newData = new OceanTruckInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.OceanBookingID = _OceanBooking.ID;
            newData.ShippingOrderNo = _OceanBooking.OceanShippingOrderNo;
            newData.CarrierName = _OceanBooking.CarrierName;
            newData.VesselVoyage = _OceanBooking.VesselVoyage;
            newData.LoadingTime = DateTime.Now;

            if (_RecentTruckerID != null)
            {
                newData.TruckerID = _RecentTruckerID.Value;
                newData.TruckerName = _RecentTruckerName;
                newData.TruckerDescription = OEUtility.Clone<CustomerDescription>(_RecentTruckerDes);
            }
            if (_RecentShipperID != null)
            {
                newData.ShipperID = _RecentShipperID.Value;
                newData.ShipperName = _RecentShipperName;
                newData.ShipperDescription = OEUtility.Clone<CustomerDescription>(_RecentShipperDes);
            }
            if (_returnLocationId != null)
            {
                newData.DeliveryAtID = _returnLocationId.Value;
                newData.DeliveryAtName = _recentReturnLocationName;
                newData.DeliveryAtDescription = OEUtility.Clone<CustomerDescription>(_recentReturnLocationDescription);
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
        private void InnerInit(IDictionary<string, object> values)
        {
            _values = values;
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "Booking".ToUpper())
                {
                    _OceanBooking = item.Value as OceanBookingList;
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
                else if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
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
            if (values.ContainsKey("ReturnLocationID"))
            {
                _returnLocationId = (Guid?)values["ReturnLocationID"];
            }
            if (values.ContainsKey("ContainerDescription"))
            {
                _bookingContainerDescription = (ContainerDescription)values["ContainerDescription"];
            }
            if (values.ContainsKey("Remark"))
            {
                if (values["Remark"] != null)
                {
                    _recentRemark = values["Remark"].ToString();
                }
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

        public override void Init(IDictionary<string, object> values)
        {
            InnerInit(values);
        }

        string _recentCustomerBrokerName = string.Empty;
        CustomerDescription _recentCustomsBrokerDescription = null;
        #endregion
    }
}

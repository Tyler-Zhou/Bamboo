using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.TMS.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.UI;

namespace ICP.TMS.UI
{
    [ToolboxItem(false)]
    public partial class TruckBookingsEdit : BaseEditPart
    {

        #region 构造
        public TruckBookingsEdit()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(TruckBookingsEdit_Disposed);
            this.Closing += new EventHandler<FormClosingEventArgs>(TruckBookingsEdit_Closing);
        }

        void TruckBookingsEdit_Closing(object sender, FormClosingEventArgs e)
        {
            GetContainerDescription();
            if (IsChanged)
            {
                DialogResult dr = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.SaveTruckBooking())
                    {
                        e.Cancel = true;
                    }
                }
            }

        }

        void TruckBookingsEdit_Disposed(object sender, EventArgs e)
        {
            if (this.vesselNameFinder != null)
            {
                this.vesselNameFinder.Dispose();
                this.vesselNameFinder = null;
            }
            this._countryList = null;
            this.containersList = null;
            this.containerTypeList = null;
            this.gcBox.DataSource = null;
            this.bsList.PositionChanged -= this.bsList_PositionChanged;
            this.bsList.DataSource = null;
            this.bsList.Dispose();
            this.bsTruckInfo.DataSource = null;
            this.bsTruckInfo.Dispose();
            this.Saved = null;
            this.DeleteDataed = null;
            this.stxtDeliveryAtID.OnOk -= this.stxtDeliveryAtID_OnOk;
            this.stxtPickUpAtID.OnOk -= this.stxtPickUpAtID_OnOk;
            this.stxtReturnLocationID.OnOk -= this.stxtReturnLocationID_OnOk;
           
            if (Workitem != null)
            {
                Workitem.Items.Remove(this);
                Workitem = null;
            }
        }
        #endregion

        #region 服务
        /// <summary>
        /// Workitem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 拖车服务
        /// </summary>
        public ITruckBookingService TruckBookingService
        {
            get
            {
                return ServiceClient.GetService<ITruckBookingService>();
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }
        /// <summary>
        /// 搜索器服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 基础信息服务
        /// </summary>
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
        /// <summary>
        /// 客户服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        public TMSUIHelper TMSUIHelper
        {
            get
            {
              return  ClientHelper.Get<TMSUIHelper, TMSUIHelper>();
            }
        }


        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 属性
        /// </summary>
        public TruckBookingEditType EditType
        {
            get;
            set;
        }
        #endregion

        #region 本地变量
        /// <summary>
        /// 业务实体
        /// </summary>
        private TruckBookingsInfo truckBookings = new TruckBookingsInfo();
        /// <summary>
        /// 派车列表
        /// </summary>
        private List<TruckContainersList> containersList = new List<TruckContainersList>();

        private bool isCharge = false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        private bool IsChanged
        {
            get
            {
                if (isCharge)
                {
                    return true;
                }
                else
                {
                    if (truckBookings.IsDirty)
                    {
                        return true;
                    }
                    foreach (TruckContainersList item in containersList)
                    {
                        if (item.IsDirty)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 司机列表
        /// </summary>
        List<DriversDataList> DriverList;
        /// <summary>
        /// 当前选择的箱数据
        /// </summary>
        TruckContainersList CurrentContainer
        {
            get
            {
                return bsList.Current as TruckContainersList;
            }
        }
        List<TruckContainersList> DataList
        {
            get
            {
                return bsList.DataSource as List<TruckContainersList>;
            }
        }

        /// <summary>
        /// 国家列表
        /// </summary>
        List<CountryList> _countryList = null;
        /// <summary>
        /// 提柜地客户描述
        /// </summary>
        CustomerFinderBridge pickUpAtIDBridge = null;
        /// <summary>
        /// 还柜地客户描述
        /// </summary>
        CustomerFinderBridge returnLocationBridge = null;
        /// <summary>
        /// 交货地客户描述
        /// </summary>
        CustomerFinderBridge deliveryBridge = null;

        #endregion

        #region IEditPart

        /// <summary>
        /// 保存事件
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        /// <summary>
        /// 删除事件
        /// </summary>
        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler DeleteDataed;


        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindingData(object data)
        {
            TruckBookingsList list = data as TruckBookingsList;

            if (list == null)
            {
                #region 新增
                truckBookings = new TruckBookingsInfo();
                containersList = new List<TruckContainersList>();

                truckBookings.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                truckBookings.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                truckBookings.Bookingmode = BookingMode.EMail;
                truckBookings.SalesName = string.Empty;
                truckBookings.BookingDate = DateTime.Now;
                truckBookings.CreateByDate = DateTime.Now;
                truckBookings.CreateByID = LocalData.UserInfo.LoginID;
                truckBookings.CreateByName = LocalData.UserInfo.LoginName;
                truckBookings.PickUpAtDescription = new ICP.Framework.CommonLibrary.Common.CustomerDescription();
                truckBookings.DeliveryAtDescription = new ICP.Framework.CommonLibrary.Common.CustomerDescription();
                truckBookings.ReturnLocationDescription = new ICP.Framework.CommonLibrary.Common.CustomerDescription();
                truckBookings.IsValid = true;

                this.EditType = TruckBookingEditType.Edit;

                bsTruckInfo.DataSource = truckBookings;
                bsList.DataSource = containersList;

                bsList.ResetBindings(false);
                bsTruckInfo.ResetBindings(false);

                this.barRefresh.Enabled = false;
                #endregion
            }
            else
            {

                this.EditType = list.EditType;

                RefreshData(list.BookingID);

                #region 复制
                if (list.TruckEditMode == EditMode.Copy)
                {
                    truckBookings.ID = Guid.Empty;
                    truckBookings.No = string.Empty;
                    truckBookings.IsDirty = true;

                    bsList.ResetCurrentItem();

                    foreach (TruckContainersList item in containersList)
                    {
                        item.TruckBookingID = Guid.Empty;
                        item.ID = Guid.Empty;
                    }
                }
                #endregion
            }

            SearchRegister();

        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return SaveTruckBooking();
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                SetDisplay();
            }

        }
        /// <summary>
        /// 初始化消息 
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("CompanyIsNull", LocalData.IsEnglish ? "Company must input" : "口岸公司：必须输入");
            this.RegisterMessage("CustomerIsNull", LocalData.IsEnglish ? "Customer must input" : "客户：必须输入");
            this.RegisterMessage("SalesIsNull", LocalData.IsEnglish ? "Sales must input" : "揽货人：必须输入");
            this.RegisterMessage("SalesTypeIsNull", LocalData.IsEnglish ? "SalesType must input" : "揽货类型：必须输入");
            this.RegisterMessage("BookingTypeIsNull", LocalData.IsEnglish ? "BookingType must input" : "委托方式：必须输入");
            this.RegisterMessage("BookingDateIsNull", LocalData.IsEnglish ? "BookingDate must input" : "委托日期：必须输入");
            this.RegisterMessage("MBLNoIsNull", LocalData.IsEnglish ? "MBLNo must input" : "船东提单号：必须输入");
            this.RegisterMessage("TypeIsNull", LocalData.IsEnglish ? "Type must input" : "类型：必须输入");
            this.RegisterMessage("CarrierIsNull", LocalData.IsEnglish ? "Carrier must input" : "船东：必须输入");
            this.RegisterMessage("ContainerListIsNull", LocalData.IsEnglish ? "ContainerInfo must input" : "箱信息：必须输入");
            this.RegisterMessage("ContainerNoIsNull", LocalData.IsEnglish ? "ContainerNo must input" : "箱号：必须输入");
            this.RegisterMessage("ContainerTypeIsNull", LocalData.IsEnglish ? "ContainerType" : "箱型：必须输入");

            this.RegisterMessage("ContainerList", LocalData.IsEnglish ? "ContainerList" : "箱列表");
            this.RegisterMessage("TruckList", LocalData.IsEnglish ? "TruckList" : "拖车列表");


            this.RegisterMessage("0202070001", LocalData.IsEnglish ? "Only un-dispatched items can be removed" : "选择的列表中,包含状态不为未派车的单,无法删除");
        }

        /// <summary>
        /// “还柜地点”是类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForReturnLocation()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);

            return conditions;
        }

        /// <summary>
        /// “还柜地点”是类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForAllCustomerType()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();

            conditions.AddWithValue("CustomerType", CustomerType.Unknown, false);
            conditions.AddWithValue("CustomerType", CustomerType.Airline, false);
            conditions.AddWithValue("CustomerType", CustomerType.Broker, false);
            conditions.AddWithValue("CustomerType", CustomerType.Carrier, false);
            conditions.AddWithValue("CustomerType", CustomerType.DirectClient, false);
            conditions.AddWithValue("CustomerType", CustomerType.Express, false);
            conditions.AddWithValue("CustomerType", CustomerType.Forwarding, false);
            conditions.AddWithValue("CustomerType", CustomerType.Railway, false);
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);
            conditions.AddWithValue("CustomerType", CustomerType.Trucker, false);
            conditions.AddWithValue("CustomerType", CustomerType.Warehouse, false);
            return conditions;
        }


        private IDisposable vesselNameFinder;
        /// <summary>
        /// 注册搜索器
        /// </summary>
        void SearchRegister()
        {
            #region Voyage

           vesselNameFinder= DataFindClientService.Register(stxtVesselName,
                CommonFinderConstants.VesselVoyageFinder,
                SearchFieldConstants.Vessel,
                SearchFieldConstants.VesselResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    this.stxtVoyageNo.Text = this.truckBookings.VoyageNo = resultData[1].ToString();
                    this.stxtVesselName.Text = truckBookings.VesselName = resultData[2].ToString();
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);


            #endregion

            #region 提柜地

            //stxtPickUpAtID
            Utility.SetEnterToExecuteOnec(stxtPickUpAtID, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                pickUpAtIDBridge = new CustomerFinderBridge(
                this.stxtPickUpAtID,
                _countryList,
                this.DataFindClientService,
                this.CustomerService,
                this.truckBookings.PickUpAtDescription,
                TMSUIHelper,
                GetConditionsForReturnLocation(),
                LocalData.IsEnglish);
                pickUpAtIDBridge.Init();
            });

            stxtPickUpAtID.OnOk += new EventHandler(stxtPickUpAtID_OnOk);
            #endregion

            #region 还柜地
            //stxtReturnLocationID
            Utility.SetEnterToExecuteOnec(stxtReturnLocationID, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                returnLocationBridge = new CustomerFinderBridge(
                this.stxtReturnLocationID,
                _countryList,
                this.DataFindClientService,
                this.CustomerService,
                this.truckBookings.ReturnLocationDescription,
                TMSUIHelper,
                GetConditionsForReturnLocation(),
                LocalData.IsEnglish);
                returnLocationBridge.Init();
            });

            stxtReturnLocationID.OnOk += new EventHandler(stxtReturnLocationID_OnOk);
            #endregion

            #region 交货地
            Utility.SetEnterToExecuteOnec(stxtDeliveryAtID, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                deliveryBridge = new CustomerFinderBridge(
                this.stxtDeliveryAtID,
                _countryList,
                this.DataFindClientService,
                this.CustomerService,
                this.truckBookings.DeliveryAtDescription,
                TMSUIHelper,
                GetConditionsForAllCustomerType(),
                LocalData.IsEnglish);
                deliveryBridge.Init();
            });
            stxtDeliveryAtID.OnOk += new EventHandler(stxtDeliveryAtID_OnOk);

            #endregion
        }

        void stxtDeliveryAtID_OnOk(object sender, EventArgs e)
        {
            if (stxtDeliveryAtID.CustomerDescription != null)
            {
                truckBookings.DeliveryAtDescription = stxtDeliveryAtID.CustomerDescription;
            }
        }

        void stxtReturnLocationID_OnOk(object sender, EventArgs e)
        {
            if (stxtReturnLocationID.CustomerDescription != null)
            {
                truckBookings.ReturnLocationDescription = stxtReturnLocationID.CustomerDescription;
            }
        }

        void stxtPickUpAtID_OnOk(object sender, EventArgs e)
        {
            if (stxtPickUpAtID.CustomerDescription != null)
            {
                truckBookings.PickUpAtDescription = stxtPickUpAtID.CustomerDescription;
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            InitTruckList();
            InitDriverList();
            InitContainerType();

            if (EditType == TruckBookingEditType.Edit)
            {
                this.bgContainer.Caption = NativeLanguageService.GetText(this, "ContainerList");
                this.colIndexNo.Visible = true;

            }
            else
            {
                this.bgContainer.Caption = NativeLanguageService.GetText(this, "TruckList");
                this.colIndexNo.Visible = false;
            }

            //初始化状态下拉框
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<TruckBusinessState>> truckState = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<TruckBusinessState>(LocalData.IsEnglish);
            CmbState.Properties.BeginUpdate();
            CmbState.Properties.Items.Clear();
            foreach (var item in truckState)
            {
                CmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            CmbState.Properties.EndUpdate();

            //绑定客户
            Utility.SetEnterToExecuteOnec(this.cmbCustomer,
                    delegate
                    {
                        InitCustomerByCompany();
                    });


            //绑定公司
            Utility.SetEnterToExecuteOnec(this.cmbCompany,
                delegate
                {
                    Utility.BindComboBoxByCompany(this.cmbCompany);
                });

            //揽货人
            Utility.SetEnterToExecuteOnec(this.cmbSales,
                delegate
                {
                    Guid orgID = (this.truckBookings.CompanyID == null || this.truckBookings.CompanyID == Guid.Empty) ? LocalData.UserInfo.DefaultCompanyID : this.truckBookings.CompanyID;
                    Utility.BindCmbBoxUserByOrg(UserService, this.cmbSales, orgID);
                });

            //揽货类型
            Utility.SetEnterToExecuteOnec(this.cmbSalesType,
                delegate
                {
                    Utility.BindaDictionary(TransportFoundationService, this.cmbSalesType, DataDictionaryType.SalesType);
                });

            //委托方式
            Utility.SetEnterToExecuteOnec(this.cmbBookingMode,
               delegate
               {
                   Utility.SetComboxByEnum<BookingMode>(this.cmbBookingMode, false, false);
               });

            //进出口类型
            Utility.SetEnterToExecuteOnec(this.cmbType,
                       delegate
                       {
                           Utility.SetComboxByEnum<TruckBookingType>(this.cmbType, false, false);
                       });

            //船东
            Utility.SetEnterToExecuteOnec(cmbCarrierID, delegate
            {
                ICPCommUIHelper.BindCustomerList(this.cmbCarrierID, CustomerType.Carrier);
            });
        }
        /// <summary>
        /// 绑定客户列表
        /// </summary>
        private void InitCustomerByCompany()
        {
            List<CustomerList> carriers = CustomerService.GetCustomerListCompany();

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
            col.Add("Code", "代码");
            this.cmbCustomer.InitSource<CustomerList>(carriers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        List<TruckDataList> TruckList;
        /// <summary>
        /// 初始化拖车列表
        /// </summary>
        private void InitTruckList()
        {
            TruckList = TruckBookingService.GetTruckDataList(null, null, TruckDateSeachType.ALL, true, null, null, LocalData.IsEnglish);

            this.cmbTruckID.Properties.BeginUpdate();
            this.cmbTruckID.Properties.Items.Clear();

            cmbTruckID.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, null));

            foreach (TruckDataList item in TruckList)
            {
                cmbTruckID.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.TruckNo, item.ID));
            }

            this.cmbTruckID.Properties.EndUpdate();
        }
        /// <summary>
        /// 初始化司机列表
        /// </summary>
        private void InitDriverList()
        {
            DriverList = TruckBookingService.GeteDriverList(null, null, null, true, null, null, LocalData.IsEnglish);

            this.cmbDriverID.Properties.BeginUpdate();
            this.cmbDriverID.Properties.Items.Clear();

            cmbDriverID.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, null));

            foreach (DriversDataList item in DriverList)
            {
                cmbDriverID.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.ID));
            }

            this.cmbDriverID.Properties.EndUpdate();
        }
        /// <summary>
        /// 箱列表
        /// </summary>
        List<ContainerList> containerTypeList;
        /// <summary>
        /// 初始化箱型
        /// </summary>
        private void InitContainerType()
        {
            containerTypeList = TransportFoundationService.GetContainerList(string.Empty, true, 0);

            cmbContainerType.Properties.BeginUpdate();
            cmbContainerType.Properties.Items.Clear();
            cmbContainerType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, null));
            foreach (ContainerList container in containerTypeList)
            {
                cmbContainerType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(container.Code, container.ID));
            }
            cmbContainerType.Properties.EndUpdate();

        }

        /// <summary>
        /// 设置显示值
        /// </summary>
        private void SetDisplay()
        {

            this.cmbCompany.ShowSelectedValue(truckBookings.CompanyID, truckBookings.CompanyName);
            this.cmbSales.ShowSelectedValue(truckBookings.SalesID, truckBookings.SalesName);
            this.cmbSalesType.ShowSelectedValue(truckBookings.SalesTypeID, truckBookings.SalesTypeName);

            this.cmbBookingMode.ShowSelectedValue(this.truckBookings.Bookingmode,
             EnumHelper.GetDescription<BookingMode>(this.truckBookings.Bookingmode, LocalData.IsEnglish));

            string typeName = EnumHelper.GetDescription<TruckBookingType>(this.truckBookings.TruckType, LocalData.IsEnglish);

            if (!string.IsNullOrEmpty(typeName) && typeName != "0")
            {
                this.cmbType.ShowSelectedValue(this.truckBookings.TruckType, typeName);
            }

            this.cmbCarrierID.ShowSelectedValue(this.truckBookings.CarrierID, this.truckBookings.CarrierName);


        }

        #endregion

        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData(truckBookings.ID);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Refresh Successfully" : "刷新成功");

        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="id"></param>
        private void RefreshData(Guid id)
        {
            truckBookings = TruckBookingService.GetTruckBookingsInfo(id, LocalData.IsEnglish);
            containersList = truckBookings.TruckContainersList;
            if (containersList == null)
            {
                containersList = new List<TruckContainersList>();
            }

            bsTruckInfo.DataSource = truckBookings;
            bsList.DataSource = containersList;

            bsList.ResetBindings(false);
            bsTruckInfo.ResetBindings(false);


            this.Enabled = truckBookings.IsValid;

            RefreshToolButton();

            this.cmbCompany.ShowSelectedValue(truckBookings.CompanyID, truckBookings.CompanyName);
            this.cmbSales.ShowSelectedValue(truckBookings.SalesID, truckBookings.SalesName);
            this.cmbSalesType.ShowSelectedValue(truckBookings.SalesTypeID, truckBookings.SalesTypeName);
            this.cmbBookingMode.ShowSelectedValue(truckBookings.Bookingmode,
                EnumHelper.GetDescription<BookingMode>(truckBookings.Bookingmode, LocalData.IsEnglish));
            this.cmbType.ShowSelectedValue(truckBookings.TruckType,
               EnumHelper.GetDescription<TruckBookingType>(truckBookings.TruckType, LocalData.IsEnglish));
            this.cmbCarrierID.ShowSelectedValue(truckBookings.CarrierID, truckBookings.CarrierName);


            if (truckBookings.PickUpAtDescription == null)
            {
                truckBookings.PickUpAtDescription = new CustomerDescription();
            }
            if (truckBookings.ReturnLocationDescription == null)
            {
                truckBookings.ReturnLocationDescription = new CustomerDescription();
            }
            if (truckBookings.DeliveryAtDescription == null)
            {
                truckBookings.DeliveryAtDescription = new CustomerDescription();
            }

            if (truckBookings.ContainerDescription != null)
            {
                this.stxtContainerDescription.Text = truckBookings.ContainerDescription.ToString();
            }

            truckBookings.IsDirty = false;
            truckBookings.BeginEdit();

            this.isCharge = false;

        }

        /// <summary>
        /// 刷新工具栏按钮
        /// </summary>
        private void RefreshToolButton()
        {
            if (Utility.GuidIsNullOrEmpty(truckBookings.ID))
            {
                this.barRefresh.Enabled = false;
            }
            else
            {
                this.barRefresh.Enabled = true;
            }

        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveTruckBooking();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private bool SaveTruckBooking()
        {
            if (!ValidateData())
            {
                return false;
            }
            if (!IsChanged)
            {
                return false;
            }
            try
            {
                //得到保存实体
                TruckBookingSaveRequest bookingSaveRequest = GetBookingSaveRequest();
                List<TruckContainersSaveRequest> containerSaveRequestList = GetContainersSaveRequest();
                //保存数据
                Dictionary<Guid, SaveResponse> saved = TruckBookingService.SaveTruckBookingWithTrans(
                                                      bookingSaveRequest, containerSaveRequestList);



                if (bookingSaveRequest != null)
                {
                    //更新业务数据
                    SaveResponse.Analyze(new List<SaveRequest> { bookingSaveRequest }, saved, true);
                    SingleResult result = bookingSaveRequest.SingleResult;
                    TruckBookingsInfo currentData = bookingSaveRequest.UnBoxInvolvedObject<TruckBookingsInfo>()[0];
                    currentData.ID = result.GetValue<Guid>("ID");
                    currentData.No = result.GetValue<string>("NO");
                    currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    truckBookings.IsDirty = false;
                    truckBookings.BeginEdit();

                    isCharge = false;
                }

                if (containerSaveRequestList != null && containerSaveRequestList.Count > 0)
                {
                    SaveResponse.Analyze(containerSaveRequestList.Cast<SaveRequest>().ToList(), saved, true);

                    foreach (TruckContainersSaveRequest boxInfo in containerSaveRequestList)
                    {
                        List<TruckContainersList> boxList = boxInfo.UnBoxInvolvedObject<TruckContainersList>();
                        ManyResult result = boxInfo.ManyResult;

                        for (int i = 0; i < boxList.Count; i++)
                        {
                            boxList[i].ID = result.Items[i].GetValue<Guid>("ID");
                            boxList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                            boxList[i].IsDirty = false;
                        }
                    }
                }

                int n = (from d in containersList where d.State == TruckBusinessState.Return select d).Count();

                if (n == containersList.Count)
                {
                    containersList.ForEach(o => o.State = TruckBusinessState.Completed);
                    bsList.ResetBindings(false);
                }


                RefreshListData();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 刷新列表数据
        /// </summary>
        private void RefreshListData()
        {
            if (Saved != null)
            {
                List<TruckBookingsList> list = new List<TruckBookingsList>();

                foreach (TruckContainersList item in containersList)
                {
                    TruckBookingsList booking = new TruckBookingsList();

                    booking.BookingID = truckBookings.ID;
                    booking.ContainerID = item.ID;
                    booking.ContainerNo = item.No;
                    booking.CreateByName = LocalData.UserInfo.LoginName;
                    booking.CreateDate = DateTime.Now;
                    booking.CustomerName = truckBookings.CustomerName;
                    booking.CustomerRefNo = truckBookings.CustomerRefNo;
                    booking.IsValid = truckBookings.IsValid;
                    booking.LastFreeDate = item.LastFreeDate;
                    booking.No = truckBookings.No;
                    booking.Remark = item.Remark;
                    booking.State = item.State;
                    booking.StateDescription = EnumHelper.GetDescription<TruckBusinessState>(item.State, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                    booking.TrayNo = item.TrayNo;
                    booking.TruckDate = item.TruckDate;
                    booking.TruckPlace = item.TruckPlace;
                    booking.Type = truckBookings.TruckType;
                    booking.TypeDescription = EnumHelper.GetDescription<TruckBookingType>(booking.Type, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                    booking.BookingUpdateDate = truckBookings.UpdateDate;
                    booking.ContainerUpdateDate = item.UpdateDate;


                    ContainerList container = containerTypeList.Find(delegate(ContainerList typeList) { return typeList.ID == item.TypeID; });
                    if (container != null)
                    {
                        booking.ContainerType = container.Code;
                    }

                    TruckDataList truckData = TruckList.Find(delegate(TruckDataList truckDatalist) { return truckDatalist.ID == item.CarID; });
                    if (truckData != null)
                    {
                        booking.TruckNo = truckData.TruckNo;
                    }

                    DriversDataList driverData = DriverList.Find(delegate(DriversDataList driverDatalist) { return driverDatalist.ID == item.DriverID; });
                    if (driverData != null)
                    {
                        booking.DriverName = driverData.Name;
                    }

                    list.Add(booking);
                }

                Saved(list.ToArray());

            }
        }
        /// <summary>
        /// 刷新状态
        /// </summary>
        private void RefreshState()
        {
            int i = (from d in containersList where (d.State == TruckBusinessState.Completed || d.State == TruckBusinessState.Return) select d).Count();

            if (i == containersList.Count)
            {
                containersList.ForEach(o => o.State = TruckBusinessState.Completed);

                bsList.ResetBindings(false);
            }

        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            GetContainerDescription();

            truckBookings.EndEdit();
            bsTruckInfo.EndEdit();

            containersList.ForEach(o => o.EndEdit());
            bsList.EndEdit();


            this.txtNo.Focus();

            bool isSure = true;

            if (Utility.GuidIsNullOrEmpty(truckBookings.CompanyID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "CompanyIsNull"), this.cmbCompany);
                isSure = false;
            }
            if (Utility.GuidIsNullOrEmpty(truckBookings.CustomerID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "CustomerIsNull"), this.cmbCustomer);
                isSure = false;
            }
            if (Utility.GuidIsNullOrEmpty(truckBookings.SalesID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "SalesIsNull"), this.cmbSales);
                isSure = false;
            }
            if (string.IsNullOrEmpty(truckBookings.SalesTypeName))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "SalesTypeIsNull"), this.cmbSalesType);
                isSure = false;
            }
            if (string.IsNullOrEmpty(truckBookings.Bookingmode.ToString()) || truckBookings.Bookingmode.ToString() == "0")
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "BookingTypeIsNull"), this.cmbBookingMode);
                isSure = false;
            }
            if (truckBookings.BookingDate == null || truckBookings.BookingDate == DateTime.MaxValue || truckBookings.BookingDate == DateTime.MinValue)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "BookingDateIsNull"), this.dtBookingDate);
                isSure = false;
            }
            if (string.IsNullOrEmpty(truckBookings.MBLNo))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "MBLNoIsNull"), this.txtMBLNo);
                isSure = false;
            }
            if (string.IsNullOrEmpty(truckBookings.TruckType.ToString()) || truckBookings.TruckType.ToString() == "0")
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "TypeIsNull"), this.cmbType);
                isSure = false;
            }
            if (Utility.GuidIsNullOrEmpty(truckBookings.CarrierID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "CarrierIsNull"), this.cmbCarrierID);
                isSure = false;
            }


            if (this.containersList == null || this.containersList.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ContainerListIsNull"));
                isSure = false;
            }

            foreach (TruckContainersList item in containersList)
            {
                if (!item.IsDirty)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(item.No))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ContainerNoIsNull"));
                    isSure = false;
                }
                if (Utility.GuidIsNullOrEmpty(item.TypeID))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ContainerTypeIsNull"));
                    isSure = false;
                }
            }

            truckBookings.BeginEdit();
            containersList.ForEach(o => o.BeginEdit());

            return isSure;
        }

        /// <summary>
        /// 获得箱描述
        /// </summary>
        private void GetContainerDescription()
        {
            if (truckBookings.ContainerDescription == null && !string.IsNullOrEmpty(this.stxtContainerDescription.Text))
            {
                truckBookings.ContainerDescription = new ContainerDescription(this.stxtContainerDescription.Text.Trim());
                return;
            }
            else if (truckBookings.ContainerDescription != null && string.IsNullOrEmpty(this.stxtContainerDescription.Text))
            {
                truckBookings.ContainerDescription = null;
                return;
            }
            else if (truckBookings.ContainerDescription != null && !string.IsNullOrEmpty(this.stxtContainerDescription.Text))
            {
                ContainerDescription cd = new ContainerDescription(this.stxtContainerDescription.Text.Trim());
                if (truckBookings.ContainerDescription.ToString() != cd.ToString())
                {
                    truckBookings.ContainerDescription = cd;
                    return;
                }
            }
        }

        /// <summary>
        /// 获得业务保存实体
        /// </summary>
        /// <returns></returns>
        private TruckBookingSaveRequest GetBookingSaveRequest()
        {

            if (!truckBookings.IsDirty)
            {
                return null;
            }


            TruckBookingSaveRequest request = new TruckBookingSaveRequest();

            request.BookingDate = truckBookings.BookingDate;
            request.Bookingmode = truckBookings.Bookingmode;
            request.CarrierID = truckBookings.CarrierID;
            request.CompanyID = truckBookings.CompanyID;
            request.ContainerDescription = truckBookings.ContainerDescription;
            request.CustomerID = truckBookings.CustomerID;
            request.CustomerRefNo = truckBookings.CustomerRefNo;
            request.DeliveryAtDescription = truckBookings.DeliveryAtDescription;
            request.DeliveryAtID = truckBookings.DeliveryAtID;
            request.DeliveryDate = truckBookings.DeliveryDate;
            request.ID = truckBookings.ID;
            request.IsEnglish = LocalData.IsEnglish;
            request.MBLNo = truckBookings.MBLNo;
            request.No = truckBookings.No;
            request.PickUpAtDate = truckBookings.PickUpAtDate;
            request.PickUpAtDescription = truckBookings.PickUpAtDescription;
            request.PickUpAtID = truckBookings.PickUpAtID;
            request.Remark = truckBookings.Remark;
            request.ReturnLocationDescription = truckBookings.ReturnLocationDescription;
            request.ReturnLocationID = truckBookings.ReturnLocationID;
            request.SalesID = truckBookings.SalesID;
            request.SalesTypeID = truckBookings.SalesTypeID;
            request.SaveByID = LocalData.UserInfo.LoginID;
            request.TruckType = truckBookings.TruckType;
            request.UpdateDate = truckBookings.UpdateDate;
            request.VesselName = truckBookings.VesselName;
            request.VoyageNo = truckBookings.VoyageNo;

            request.AddInvolvedObject(truckBookings);

            return request;
        }

        /// <summary>
        /// 获得箱信息保存实体
        /// </summary>
        /// <returns></returns>
        private List<TruckContainersSaveRequest> GetContainersSaveRequest()
        {
            List<TruckContainersSaveRequest> requestList = new List<TruckContainersSaveRequest>();
            TruckContainersSaveRequest request = new TruckContainersSaveRequest();

            //更新过的数据
            List<TruckContainersList> chargeList = (from c in containersList where c.IsDirty select c).ToList<TruckContainersList>();

            if (chargeList == null || chargeList.Count == 0)
            {
                return requestList;
            }

            List<Guid> idList = new List<Guid>();
            List<String> indexNoList = new List<String>();
            List<TruckBusinessState> stateList = new List<TruckBusinessState>();
            List<String> noList = new List<String>();
            List<Guid?> typeIDList = new List<Guid?>();
            List<String> trayNoList = new List<String>();
            List<DateTime?> truckDateList = new List<DateTime?>();
            List<String> truckPlaceList = new List<String>();
            List<DateTime?> lastFreeDateList = new List<DateTime?>();
            List<DateTime?> pickUpAtDateList = new List<DateTime?>();
            List<DateTime?> deliveryDateList = new List<DateTime?>();
            List<Guid?> driverIDList = new List<Guid?>();
            List<DateTime?> returnDateList = new List<DateTime?>();
            List<Guid?> carIDList = new List<Guid?>();
            List<String> remarkList = new List<String>();
            List<DateTime?> updateList = new List<DateTime?>();


            foreach (TruckContainersList item in chargeList)
            {
                idList.Add(item.ID);
                indexNoList.Add(item.IndexNo);
                stateList.Add(item.State);
                noList.Add(item.No);
                typeIDList.Add(item.TypeID);
                trayNoList.Add(item.TrayNo);
                truckDateList.Add(item.TruckDate);
                truckPlaceList.Add(item.TruckPlace);
                lastFreeDateList.Add(item.LastFreeDate);
                pickUpAtDateList.Add(item.PickUpAtDate);
                deliveryDateList.Add(item.DeliveryDate);
                driverIDList.Add(item.DriverID);
                returnDateList.Add(item.ReturnDate);
                carIDList.Add(item.CarID);
                remarkList.Add(item.Remark);
                updateList.Add(item.UpdateDate);
            }


            request.IDs = idList.ToArray();
            request.IndexNos = indexNoList.ToArray();
            request.States = stateList.ToArray();
            request.Nos = noList.ToArray();
            request.TypeIDs = typeIDList.ToArray();
            request.TrayNos = trayNoList.ToArray();
            request.TruckDates = truckDateList.ToArray();
            request.TruckPlaces = truckPlaceList.ToArray();
            request.LastFreeDates = lastFreeDateList.ToArray();
            request.PickUpAtDates = pickUpAtDateList.ToArray();
            request.DeliveryDates = deliveryDateList.ToArray();
            request.DriverIDs = driverIDList.ToArray();
            request.ReturnDates = returnDateList.ToArray();
            request.CarIDs = carIDList.ToArray();
            request.Remarks = remarkList.ToArray();
            request.UpdateDates = updateList.ToArray();

            request.TruckBookingID = truckBookings.ID;
            request.SaveByID = LocalData.UserInfo.LoginID;
            request.IsEnglish = LocalData.IsEnglish;


            chargeList.ForEach(o => request.AddInvolvedObject(o));

            requestList.Add(request);

            return requestList;
        }


        #endregion

        #region 箱列表的操作
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TruckContainersList item = new TruckContainersList();
            item.IndexNo = (containersList.Count + 1).ToString();
            item.CreateByID = LocalData.UserInfo.LoginID;
            item.State = TruckBusinessState.NoTruck;
            item.BeginEdit();
            item.IsDirty = true;

            bsList.Add(item);

            bsList.ResetBindings(false);

            isCharge = true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = gvBox.GetSelectedRows();
            if (indexs == null || indexs.Length == 0)
            {
                return;
            }

            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDates = new List<DateTime?>();

                bool isSurce = true;

                foreach (var item in indexs)
                {
                    TruckContainersList tager = gvBox.GetRow(item) as TruckContainersList;
                    if (tager.ID != null && tager.ID != Guid.Empty)
                    {
                        ids.Add(tager.ID);
                        updateDates.Add(tager.UpdateDate);

                        if (Utility.GuidIsNullOrEmpty(tager.ID) && tager.State != TruckBusinessState.NoTruck)
                        {
                            isSurce = false;
                        }
                    }
                }
                if (isSurce)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "0202070001"));
                    return;
                }
                if (ids.Count > 0)
                {
                    TruckBookingService.DeleteContainer(ids.ToArray(), truckBookings.ID, updateDates.ToArray(), LocalData.UserInfo.LoginID, LocalData.IsEnglish);

                    gvBox.DeleteSelectedRows();

                    RefreshState();

                    if (DeleteDataed != null)
                    {
                        DeleteDataed(ids);
                    }

                }
                else
                {
                    gvBox.DeleteSelectedRows();
                }



            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }


        }
        /// <summary>
        /// 数据改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBox_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colDriverID)
            {
                #region 司机
                if (DriverList != null && DriverList.Count > 0)
                {
                    if (e.Value != null)
                    {
                        Guid id = new Guid(e.Value.ToString());
                        DriversDataList tager = DriverList.Find(delegate(DriversDataList item) { return item.ID == id; });
                        if (tager != null)
                        {
                            TruckContainersList truckContainers = this.bsList.Current as TruckContainersList;
                            if (truckContainers != null)
                            {
                                truckContainers.CarID = tager.TruckID;
                                bsList.ResetCurrentItem();
                            }
                        }
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 数据发生改变后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBox_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colDeliveryDate ||
               e.Column == this.colTruckDate ||
                e.Column == this.colReturnDate ||
                e.Column == this.colPickUpAtDate)
            {
                CurrentContainer.State = GetState();
            }
        }
        /// <summary>
        /// 获得状态
        /// </summary>
        /// <returns></returns>
        private TruckBusinessState GetState()
        {
            if (CurrentContainer == null || CurrentContainer.TruckDate == null)
            {
                //未派车
                return TruckBusinessState.NoTruck;
            }

            if (CurrentContainer.TruckDate != null &&
                CurrentContainer.PickUpAtDate == null)
            {
                //已派车
                return TruckBusinessState.Trucked;
            }
            if (CurrentContainer.TruckDate != null &&
                CurrentContainer.PickUpAtDate != null &&
                CurrentContainer.DeliveryDate == null)
            {
                //已提柜
                return TruckBusinessState.PickAt;
            }
            if (CurrentContainer.TruckDate != null &&
                CurrentContainer.PickUpAtDate != null &&
                CurrentContainer.DeliveryDate != null &&
                CurrentContainer.ReturnDate == null)
            {
                //已交货
                return TruckBusinessState.Delivery;
            }
            if (CurrentContainer.TruckDate != null &&
                CurrentContainer.PickUpAtDate != null &&
                CurrentContainer.DeliveryDate != null &&
                CurrentContainer.ReturnDate != null)
            {
                //已还柜
                return TruckBusinessState.Return;
            }

            //未派车
            return TruckBusinessState.NoTruck;


        }
        #endregion

        #region 列表中选择的行发生改变
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentContainer == null)
            {
                this.barDelete.Enabled = false;
            }
            else
            {
                if (Utility.GuidIsNullOrEmpty(CurrentContainer.ID))
                {
                    this.barDelete.Enabled = false;
                }
                else
                {
                    if (CurrentContainer.State == TruckBusinessState.NoTruck)
                    {
                        this.barDelete.Enabled = true;
                    }
                    else
                    {
                        this.barDelete.Enabled = false;
                    }
                }
            }
        }
        #endregion

        private void bgcBusiness_Click(object sender, EventArgs e)
        {

        }


    }
}

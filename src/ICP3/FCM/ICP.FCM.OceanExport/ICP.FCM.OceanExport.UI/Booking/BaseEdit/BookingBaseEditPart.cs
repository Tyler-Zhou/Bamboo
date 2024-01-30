using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Common;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.CommonPart;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.UI.Booking.BaseEdit;
using ICP.FCM.OceanExport.UI.Common.Controls;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;
using FormType = ICP.Framework.CommonLibrary.Common.FormType;


namespace ICP.FCM.OceanExport.UI.Booking
{
    /// <summary>
    /// 订舱单编辑界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BookingBaseEditPart : BaseEditPart
    {
        #region DllImport
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndParent"></param>
        /// <param name="lpfn"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate bool CallBack(IntPtr hwnd, int lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="lpszClass"></param>
        /// <param name="lpszWindow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string lpszClass, string lpszWindow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam); 
        #endregion

        #region Service & Fields & Property
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        public static int WM_CHAR = 0x102;
        /// <summary>
        /// 
        /// </summary>
        public static int WM_CLICK = 0x00F5;
        /// <summary>
        /// 贸易条款是否已被初始化
        /// </summary>
        bool _IsInitTradeTerm = false;
        /// <summary>
        /// 委托方式是否已被初始化
        /// </summary>
        bool _IsInitBookingMode = false;
        /// <summary>
        /// 客户类型
        /// </summary>
        CustomerType customerType = CustomerType.Unknown;
        /// <summary>
        /// 原始合约号
        /// </summary>
        Guid? origContract = null;
        /// <summary>
        /// 订舱详细信息
        /// </summary>
        OceanBookingInfo _CurrentData = null;
        /// <summary>
        /// 是否保存订舱
        /// </summary>
        bool isSave = false;
        /// <summary>
        /// 业务信息是否保存成功
        /// </summary>
        private bool isSaveOperationContact = false;
        /// <summary>
        /// 合约号发生改变
        /// </summary>
        bool chargeContract = false;
        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;
        /// <summary>
        /// 操作内容
        /// </summary>
        BusinessOperationContext OperationContext = new BusinessOperationContext();
        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _CacheCountryList = null;
        /// <summary>
        /// 代理下拉数据源
        /// </summary>
        List<CustomerList> _CacheAgentCustomersList = null;
        /// <summary>
        /// 重量单位
        /// </summary>
        List<DataDictionaryList> _CacheWeightUnits;
        /// <summary>
        /// 发送修改记录邮件-订舱单修改前数据组合字符串
        /// </summary>
        StringBuilder _oldstring = new StringBuilder();
        /// <summary>
        /// 发送修改记录邮件-订舱单修改的数据组合字符串
        /// </summary>
        StringBuilder _updatestring = new StringBuilder();
        /// <summary>
        /// 记录原始数据,发送邮件时使用
        /// </summary>
        OceanBookingInfo _old = null;

        string CarrierName = string.Empty;
        /// <summary>
        /// 询价面板实体
        /// </summary>
        InquirePricePartInfo _inquirePricePartInfo = null;

        bool isAddSo = false;

        //控件搜索器
        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge consigneeBridge;
        CustomerFinderBridge notifyPartyBridge;
        CustomerFinderBridge bookingShipperBridge;
        CustomerFinderBridge bookingConsigneeBridge;
        CustomerFinderBridge bookingNotifyPartyBridge;
        CustomerFinderBridge bookingCustomerPartyBridge;
        LocationFinderBridge pfbPlaceOfReceipt;
        CustomerContactFinderBridge bookingCustomerFinderBridge;
        CustomerContactFinderBridge agentOfCarrierFinderBridge;

        #region Thread Save
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime ThreadStartTime;
        /// <summary>
        /// 显示信息
        /// </summary>
        private string _StrMesage = string.Empty;

        private bool isChangeSales = false;
        #endregion

        #endregion

        #region Property
        /// <summary>
        /// 费用列表
        /// </summary>
        List<OceanBookingFeeList> CacheFeelist = null;
        /// <summary>
        /// 委托列表
        /// </summary>
        List<BookingDelegate> _CacheDelegate = null;
        /// <summary>
        /// 委托列表
        /// </summary>
        List<BookingDelegate> CacheDelegate
        {
            get
            {
                if(_CacheDelegate == null)
                {
                    _CacheDelegate = new List<BookingDelegate>();
                }
                return _CacheDelegate;
            }
            set
            {
                _CacheDelegate = value;
            }
        }

        /// <summary>
        /// 由于初次加载的时候IsDirty已经确保为false
        /// 故很可靠。
        /// 要保证这一点：数据最早进来的时候，立刻CancelEdit。因为可能在网格控件中触发过BeginEdit
        /// </summary>
        bool _shown
        {
            get
            {
                return _CurrentData.IsDirty;
            }
        }

        #region 联系人&文档
        /// <summary>
        /// 联系人 & 文档
        /// </summary>
        private UCContactAndDocumentPart bookingOtherPart;
        /// <summary>
        /// 联系人 & 文档
        /// </summary>
        public UCContactAndDocumentPart UCBookingOtherPart
        {
            get
            {
                if (bookingOtherPart != null)
                {
                    return bookingOtherPart;
                }
                else
                {
                    bookingOtherPart = PartWorkItem.Items.AddNew<UCContactAndDocumentPart>();
                    return bookingOtherPart;
                }
            }
        }
        #endregion

        #region 文件名集合
        /// <summary>
        /// 文件名集合
        /// </summary>
        List<string> FilesNames
        {
            get;
            set;
        }
        #endregion
        #endregion

        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem PartWorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 客户端搜索服务
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ISystemService SystemService
        {
            get
            {
                return ServiceClient.GetService<ISystemService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IClientInquireRateService ClientInquireRateService
        {
            get { return ServiceClient.GetClientService<IClientInquireRateService>(); }
        }
        #endregion
        #endregion

        #region 初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public BookingBaseEditPart()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (!LocalData.IsDesignMode)
            {
                Load += (sender, e) =>
                {
                    UCBookingOtherPart.Dock = DockStyle.Fill;
                    navGroupColOther.Controls.Clear();
                    navGroupColOther.Controls.Add(UCBookingOtherPart);
                };
                barSavingTools.Visible = false;
                barSavingClose.ItemClick += barSavingClose_ItemClick;
                barCancel.ItemClick += barCancel_ItemClick;
                barlabMessage.ItemClick += barlabMessage_ItemClick;
                TimerSaveData = new System.Windows.Forms.Timer();
                TimerSaveData.Interval = 1000;
                TimerSaveData.Tick += RefreshTime_Tick;
            }

            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
            Disposed += BookingBaseEditPart_Disposed;

        }
        #endregion

        #region 窗体事件

        #region 搜索器注册
        #region RecentQuotedPrice 报价
        void stxtRecentQuotedPrice_SelectChanged(object sender, CommonEventArgs<QuotedPricePartInfo> e)
        {
            if (e.Data == null)
            {
                return;
            }
            QuotedPricePartInfo currentQP = e.Data;
            DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              );
            if (result == DialogResult.Yes)
            {
                QuotedPricePartInfo qpInfo = FCMCommonService.GetRecentlyQuotedPriceList(currentQP.QuotedPriceID, "", null, null, null, null, 1).SingleOrDefault();
                if (qpInfo == null) return;

                if (_CurrentData != null)
                {
                    _CurrentData.POLID = qpInfo.POLID;
                    _CurrentData.POLName = qpInfo.POLName;
                    _CurrentData.PODID = qpInfo.PODID;
                    _CurrentData.PODName = qpInfo.PODName;
                    _CurrentData.PlaceOfReceiptID = qpInfo.PlaceOfReceiptID;
                    _CurrentData.PlaceOfReceiptName = qpInfo.PlaceOfReceiptName;
                    _CurrentData.PlaceOfDeliveryID = qpInfo.PlaceOfDeliveryID;
                    _CurrentData.PlaceOfDeliveryName = qpInfo.PlaceOfDeliveryName;
                    _CurrentData.TransportClauseID = qpInfo.TransportClauseID;
                    _CurrentData.TransportClauseName = qpInfo.TransportClauseName;
                    _CurrentData.Commodity = qpInfo.Commodity;
                    stxtRecentQuotedPrice.QuotedPriceID = qpInfo.QuotedPriceID.Value;
                    stxtRecentQuotedPrice.QuotedPriceNo = qpInfo.QuotedPriceNo;
                }

                ShowOrderRefresh();
                RunAtOnce();
                ResetDescription();
                EndEdit();
                Invalidate();
            }
        }
        #endregion

        #region AgentOfCarrier  承运人
        void stxtAgentOfCarrier_AfterEditValueChanged(object sender, EventArgs e)
        {
            AddLastestContact(_CurrentData.AgentOfCarrierID, stxtAgentOfCarrier, ContactType.Carrier);
        }

        void stxtAgentOfCarrier_BeforeEditValueChanged(object sender, ChangingEventArgs e)
        {
            RemoveContactList(_CurrentData.AgentOfCarrierID, "AgentOfCarrierID");
        }

        void stxtAgentOfCarrier_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New || EditMode == EditMode.Copy)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentData.CompanyID, _CurrentData.AgentOfCarrierID, ContactType.Carrier, ContactStage.Unknown);
            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_CurrentData.ID, OperationType.OceanExport, ContactType.Carrier, ContactStage.Unknown, _CurrentData.AgentOfCarrierID);
            }
            UCBookingOtherPart.RemoveContactList(_CurrentData.AgentOfCarrierID, ContactType.Carrier);
            UCBookingOtherPart.InsertContactList(temp);
            SetContactList(_CurrentData.AgentOfCarrierID, temp);
        }

        void stxtAgentOfCarrier_OnOk(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> currentList = stxtAgentOfCarrier.ContactList;
            UCBookingOtherPart.RemoveContactList(_CurrentData.AgentOfCarrierID, ContactType.Carrier);

            if (currentList.Count > 0)
            {
                UCBookingOtherPart.InsertContactList(currentList);
            }
            SetContactList(_CurrentData.AgentOfCarrierID, currentList);
        }

        /// <summary>
        /// 判断当前引用的客户是否被其他栏位引用，否则移除相关客户的业务联系人
        /// </summary>
        /// <param name="changeID"></param>
        /// <param name="sourcePropertyName"></param>
        private void RemoveContactList(Guid changeID, string sourcePropertyName)
        {
            if (changeID == null || changeID == Guid.Empty)
                return;
            List<string> relativePropertyNames = new List<string> { "CustomerID", "BookingCustomerID", "AgentID", "AgentOfCarrierID" };
            relativePropertyNames.Remove(sourcePropertyName);
            List<PropertyInfo> properties = typeof(OceanBookingInfo).GetProperties().Where(p => relativePropertyNames.Contains(p.Name)).ToList();
            if (properties == null || properties.Count <= 0)
            {
                return;
            }
            if (!properties.Exists(p => p.GetValue(_CurrentData, null) != null && (Guid)p.GetValue(_CurrentData, null) == changeID))
            {
                UCBookingOtherPart.RemoveContactList(changeID, null);
            }
        }
        #endregion

        #region BookingCustomer 订舱客户
        void stxtBookingCustomer_EditValueChanged(object sender, EventArgs e)
        {
            AddLastestContact(_CurrentData.BookingCustomerID, stxtBookingCustomer, ContactType.Customer);
        }

        void stxtBookingCustomer_EditValueChanging(object sender, ChangingEventArgs e)
        {
            RemoveContactList(_CurrentData.BookingCustomerID, "BookingCustomerID");
        }

        void stxtBookingCustomer_OnOk(object sender, EventArgs e)
        {
            if (stxtBookingCustomer.CustomerDescription != null && _CurrentData != null)
            {
                _CurrentData.BookingCustomerDescription = stxtBookingCustomer.CustomerDescription;
            }
            List<CustomerCarrierObjects> currentList = stxtBookingCustomer.ContactList;

            UCBookingOtherPart.RemoveContactList(_CurrentData.BookingCustomerID, ContactType.Customer);
            if (currentList.Count > 0)
            {
                UCBookingOtherPart.InsertContactList(currentList);
            }
            SetContactList(_CurrentData.BookingCustomerID, currentList);

        }

        void stxtBookingCustomer_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New || EditMode == EditMode.Copy)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentData.CompanyID, _CurrentData.BookingCustomerID, ContactType.Customer, ContactStage.Unknown);
            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_CurrentData.ID, OperationType.OceanExport, ContactType.Customer, ContactStage.Unknown, _CurrentData.BookingCustomerID);
            }

            UCBookingOtherPart.RemoveContactList(_CurrentData.BookingCustomerID, ContactType.Customer);
            UCBookingOtherPart.InsertContactList(temp);
            SetContactList(_CurrentData.BookingCustomerID, temp);
        }
        #endregion

        #region Customer 客户
        void stxtCustomer_EditValueChanging(object sender, ChangingEventArgs e)
        {
            stxtCustomer.EditValueChanging -= stxtCustomer_EditValueChanging;
            RemoveContactList(_CurrentData.CustomerID, "CustomerID");
            stxtCustomer.EditValueChanging += stxtCustomer_EditValueChanging;
        }
        void stxtCustomer_SelectChanged(object sender, CommonEventArgs<OceanOrderList> e)
        {
            if (e.Data == null)
            {
                return;
            }
            OceanOrderList currentOrderList = e.Data;
            DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              );
            if (result == DialogResult.Yes)
            {
                OceanBookingInfo order = OceanExportService.GetOceanBookingInfo(currentOrderList.ID);
                if (order == null) return;

                if (_CurrentData != null && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ID))
                {
                    order.ID = _CurrentData.ID;
                    order.No = _CurrentData.No;
                    order.UpdateDate = _CurrentData.UpdateDate;
                }
                else
                {
                    order.ID = Guid.Empty;
                    order.No = string.Empty;
                    order.State = OEOrderState.NewOrder;
                    order.OceanShippingOrderID = Guid.Empty;
                    order.OceanShippingOrderNo = string.Empty;
                    order.SalesID = LocalData.UserInfo.LoginID;
                    stxtRecentQuotedPrice.SalesID = LocalData.UserInfo.LoginID;
                    order.SalesName = LocalData.UserInfo.LoginName;
                    order.UpdateDate = null;
                    order.CreateDate = DateTime.Now;
                }

                #region 设置默认值, modifyBy pearl for bug1821:从最近10票业务导入，不需要导入:数量，重量，体积

                order.Quantity = 0;
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                order.QuantityUnitID = normalDictionary.ID;
                order.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                order.Weight = 0.0m;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                order.WeightUnitID = normalDictionary.ID;
                order.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                order.Measurement = 0.0m;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                order.MeasurementUnitID = normalDictionary.ID;
                order.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
                #endregion

                _CurrentData = order;
                ShowOrderRefresh();
                RunAtOnce();
                ResetDescription();
                EndEdit();
                Invalidate();
            }
        }

        void stxtCustomer_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New || EditMode == EditMode.Copy)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentData.CompanyID, _CurrentData.CustomerID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_CurrentData.ID, OperationType.OceanExport, ContactType.Customer, ContactStage.Unknown, _CurrentData.CustomerID);
            }

            UCBookingOtherPart.RemoveContactList(_CurrentData.CustomerID, ContactType.Customer);
            UCBookingOtherPart.InsertContactList(temp);
            SetContactList(_CurrentData.CustomerID, temp);
        }

        void stxtCustomer_OnOk(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> currentList = stxtCustomer.ContactList;
            UCBookingOtherPart.RemoveContactList(_CurrentData.CustomerID, ContactType.Customer);

            if (currentList.Count > 0)
            {
                UCBookingOtherPart.InsertContactList(currentList);
            }
            SetContactList(_CurrentData.CustomerID, currentList);
        }

        void stxtCustomer_EditValueChanged(object sender, EventArgs e)
        {
            stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
            customerType = stxtCustomer.CustomerType;
            Guid customerId = stxtCustomer.CustomerID;
            _CurrentData.CustomerID = customerId;
            _CurrentData.CustomerName = stxtCustomer.CustomerName;
            cmbTradeTerm.ShowSelectedValue(stxtCustomer.TradeTermID, stxtCustomer.TradeTermName);

            AddLastestContact(_CurrentData.CustomerID, stxtCustomer, ContactType.Customer);
            stxtCustomer.CustomerID = customerId;
            CustomerDescription _customerDescription = new CustomerDescription();
            ICPCommUIHelper.SetCustomerDesByID(customerId, _customerDescription);
            stxtCustomer.CustomerDescription = _customerDescription;

            if (_CurrentData.CustomerID != Guid.Empty)
            {
                CustomerChanged(customerType);
            }
            else
            {
                CustomerChanged(null);
            }
            stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
        }
        #endregion

        #region Consignee 收货人
        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null && _CurrentData != null)
            {
                _CurrentData.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }
        #endregion

        #region Sales 揽货人
        void mcmbSales_EditValueChanged(object sender, EventArgs e)
        {
            if (this._shown && mcmbSales.EditValue != null)
            {
                stxtRecentQuotedPrice.SalesID = new Guid(mcmbSales.EditValue.ToString());
            }

            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
            if (_CurrentData.OperationDate != null && _CurrentData.OperationDate < configure.AccountingClosingdate)
            {
                List<User2OrganizationJobList> jobList = UserService.GetUser2OrganizationJobList(LocalData.UserInfo.LoginID);
                foreach (User2OrganizationJobList job in jobList)
                {
                    if (job.OrganizationJobName.Contains("操作部经理") || job.OrganizationJobName.Contains("操作经理") || job.OrganizationJobName.Contains("操作部->经理"))
                    {
                        isChangeSales = true;
                    }
                }
            }
        }
        #endregion

        #region NotifyParty 通知方
        void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (stxtNotifyParty.CustomerDescription != null && _CurrentData != null)
            {
                _CurrentData.NotifyPartydescription = stxtNotifyParty.CustomerDescription;
            }
        }
        #endregion

        #region BookingShipper
        void stxtBookingShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtBookingShipper.CustomerDescription != null && _CurrentData != null)
            {
                _CurrentData.BookingShipperdescription = stxtBookingShipper.CustomerDescription;
            }
        }
        #endregion

        #region BookingConsignee
        void stxtBookingConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtBookingConsignee.CustomerDescription != null && _CurrentData != null)
            {
                _CurrentData.BookingConsigneedescription = stxtBookingConsignee.CustomerDescription;
            }
        }
        #endregion

        #region BookingNotifyParty
        void stxtBookingNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (stxtBookingNotifyParty.CustomerDescription != null && _CurrentData != null)
            {
                _CurrentData.BookingNotifyPartydescription = stxtBookingNotifyParty.CustomerDescription;
            }
        }
        #endregion

        #region Shipper
        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null && _CurrentData != null)
            {
                _CurrentData.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }
        #endregion

        private List<CustomerCarrierObjects> GetCurrentContactListByCustomerID(Guid customerID, ContactType contactType)
        {
            List<CustomerCarrierObjects> contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID && item.Type == contactType);
            return contactList;
        }

        /// <summary>
        /// 添加对应客户的上一票业务的对应沟通阶段的联系人
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="customerControl"></param>
        /// <param name="contactType"></param>
        private void AddLastestContact(Guid customerID, object customerControl, ContactType contactType)
        {
            if (customerID == Guid.Empty)
            {
                return;
            }
            List<CustomerCarrierObjects> contactList = new List<CustomerCarrierObjects>();
            if (!UCBookingOtherPart.CurrentContactList.Exists(item => item.CustomerID == customerID))
            {
                contactList = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentData.CompanyID, customerID, contactType, ContactStage.Unknown);
                if (contactList == null || contactList.Count <= 0)
                    return;
                for (int i = 0; i < contactList.Count; i++)
                {
                    contactList[i].Id = Guid.Empty;

                }
                List<CustomerCarrierObjects> currentContactList = UCBookingOtherPart.CurrentContactList;
                if (currentContactList == null || currentContactList.Count <= 0)
                {
                    UCBookingOtherPart.InsertContactList(contactList);
                }
                else
                {
                    List<string> nameList = (from item in currentContactList select item.Name).ToList();
                    List<string> emailList = (from item in currentContactList select item.Mail).ToList();

                    contactList = contactList.FindAll(item => !nameList.Contains(item.Name) && !emailList.Contains(item.Mail));
                    UCBookingOtherPart.InsertContactList(contactList);
                }
            }
            else
            {
                contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID);
            }
            SetContactList(customerID, contactList);
        }

        private void SetContactList(Guid customerID, List<CustomerCarrierObjects> contactList)
        {
            if (_CurrentData.CustomerID == customerID)
            {
                stxtCustomer.ContactList = contactList;

            }
            if (_CurrentData.BookingCustomerID == customerID)
            {
                stxtBookingCustomer.ContactList = contactList;
            }
            if (_CurrentData.AgentID == customerID)
            {
                stxtAgent.ContactList = contactList;
            }
            if (_CurrentData.AgentOfCarrierID == customerID)
            {
                stxtAgentOfCarrier.ContactList = contactList;
            }
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

        void ResetDescription()
        {
            if (shipperBridge != null)
            {
                shipperBridge.SetCustomerDescription(_CurrentData.ShipperDescription);
            }

            if (consigneeBridge != null)
            {
                consigneeBridge.SetCustomerDescription(_CurrentData.ConsigneeDescription);
            }

            if (notifyPartyBridge != null)
            {
                notifyPartyBridge.SetCustomerDescription(_CurrentData.NotifyPartydescription);
            }

            if (bookingShipperBridge != null)
            {
                bookingShipperBridge.SetCustomerDescription(_CurrentData.BookingShipperdescription);
            }

            if (bookingConsigneeBridge != null)
            {
                bookingConsigneeBridge.SetCustomerDescription(_CurrentData.BookingConsigneedescription);
            }

            if (bookingNotifyPartyBridge != null)
            {
                bookingNotifyPartyBridge.SetCustomerDescription(_CurrentData.BookingNotifyPartydescription);
            }

            if (bookingCustomerPartyBridge != null)
            {
                bookingCustomerPartyBridge.SetCustomerDescription(_CurrentData.BookingCustomerDescription);
            }

            if (notifyPartyBridge != null)
            {
                notifyPartyBridge.SetCustomerDescription(_CurrentData.BookingNotifyPartydescription);
            }
        }

        #endregion

        #region 延迟加载的数据源
        void stxtAgent_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (_CurrentData != null && _CurrentData.DownState)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "港后已经下载业务，不能修改代理，请联系此代理单击[业务转移]。" : "It's readonly because the agent has already downloaded the shipment. Please ask the agent to --Transit-- the shipment.");
                stxtAgent.Enabled = false;
                e.Cancel = true;

                return;
            }

            if (_CurrentData.AgentID.HasValue)
            {
                stxtAgent.EditValueChanging -= stxtAgent_EditValueChanging;
                RemoveContactList(_CurrentData.AgentID.Value, "AgentID");
                stxtAgent.EditValueChanging += stxtAgent_EditValueChanging;
            }
        }

        void stxtAgent_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New || EditMode == EditMode.Copy)
            {
                if (_CurrentData.AgentID == null)
                {
                    temp = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentData.CompanyID, _CurrentData.AgentID.Value, ContactType.Customer, ContactStage.Unknown);
                }

            }
            else
            {
                if (_CurrentData.AgentID == null)
                {
                    temp = new List<CustomerCarrierObjects>();
                }
                else
                {
                    temp = FCMCommonService.GetContactListByContactStage(_CurrentData.ID, OperationType.OceanExport, ContactType.Customer, ContactStage.Unknown, _CurrentData.AgentID.Value);
                }
            }
            UCBookingOtherPart.RemoveContactList(_CurrentData.AgentID.Value, ContactType.Customer);
            UCBookingOtherPart.InsertContactList(temp);
            SetContactList(_CurrentData.AgentID.Value, temp);
        }

        void stxtAgent_OnOk(object sender, EventArgs e)
        {
            if (_CurrentData != null && stxtAgent.CustomerDescription != null)
            {
                _CurrentData.AgentDescription = stxtAgent.CustomerDescription;

                _CurrentData.BookingConsigneeID = _CurrentData.AgentID;
                _CurrentData.BookingConsigneeName = _CurrentData.AgentDescription.Name;
                _CurrentData.BookingConsigneedescription = _CurrentData.AgentDescription;

                _CurrentData.BookingNotifyPartyID = _CurrentData.AgentID;
                _CurrentData.BookingNotifyPartyname = _CurrentData.AgentDescription.Name;
                _CurrentData.BookingNotifyPartydescription = _CurrentData.AgentDescription;
            }
            List<CustomerCarrierObjects> currentList = stxtAgent.ContactList;

            UCBookingOtherPart.RemoveContactList(_CurrentData.AgentID.Value, ContactType.Customer);
            if (currentList.Count > 0)
            {
                UCBookingOtherPart.InsertContactList(currentList);
            }
            SetContactList(_CurrentData.AgentID.Value, currentList);
        }

        /// <summary>
        /// 填充“订舱”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbBookinger_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelper.SetComboxUsersByRoles(mcmbBookinger, depID, new string[] { "订舱", "客服" }, false);
        }

        /// <summary>
        /// 填充“文件”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmFiler_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelper.SetComboxUsersByRoles(mcmbFiler, depID, new string[] { "文件", "客服" }, true);
        }

        /// <summary>
        ///填充订舱员的用户列表选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbBookingBy_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }

            ICPCommUIHelper.SetComboxUsersByRoles(mcmbBookingBy, depID, new string[] { "文件", "客服", "订舱" }, true);
        }

        /// <summary>
        /// 填充“海外部客服”的用户列表供选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbOverseasFiler_Enter(object sender, EventArgs e)
        {
            Guid depID = new Guid("FA56E82F-2352-E111-A359-0026551CA87B");
            //if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            //{
            //    depID = (Guid)this.cmbCompany.EditValue;
            //}

            ICPCommUIHelper.SetComboxUsersByRole(mcmbOverseasFiler, depID, "海外拓展", true);
        }

        #endregion

        #region 注册各种联动的事件
        /// <summary>
        /// 
        /// </summary>
        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetContainerDemandByBookingType();
            chkIsTruck.Enabled = (_CurrentData.OEOperationType == FCMOperationType.FCL
                || _CurrentData.OEOperationType == FCMOperationType.LCL);

            if (!chkIsTruck.Enabled)
            {
                chkIsTruck.Checked = _CurrentData.IsTruck = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void stxtBookingCustomer_TextChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _CurrentData.BookingCustomerName = stxtBookingCustomer.Text;
                SetShipperByBookingCustomerAndTradeTerm();
            }
        }
        /// <summary>
        /// 当前用户所在的操作口岸和揽货人所在的部门
        /// </summary>
        void trsSalesDep_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userOrganizationTreeLists = new List<OrganizationList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesID) == false)
            {
                userOrganizationTreeLists = UserService.GetUserCompanyList(_CurrentData.SalesID.Value, null);
            }

            List<OrganizationList> saleOrgrnazitionTreeList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            foreach (OrganizationList dept in saleOrgrnazitionTreeList)
            {
                if (userOrganizationTreeLists.FindAll(o => o.ID == dept.ID).Count == 0)
                {
                    userOrganizationTreeLists.Add(dept);
                }
            }

            trsSalesDep.SetSource<OrganizationList>(userOrganizationTreeLists, LocalData.IsEnglish ? "EShortName" : "CShortName", "HasPermission");
        }
        /// <summary>
        /// 
        /// </summary>
        void cmbTradeTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                _CurrentData.TradeTermName = cmbTradeTerm.Text;
                SetBookingCustomerByCustomerAndTradeTerm();
                SetConsigneeByCustomerAndTradeTerm();
                SetShipperByBookingCustomerAndTradeTerm();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalesType.EditValue == null
                || cmbSalesType.EditValue == DBNull.Value)
            {
                return;
            }
            _CurrentData.SalesTypeName = cmbSalesType.Text;
            _CurrentData.SalesTypeID = (Guid)cmbSalesType.EditValue;
            if (_CurrentData.SalesTypeID.ToString() == "e34bdcaa-4253-41c0-b14b-e38111cf2fc8" || _CurrentData.SalesTypeID.ToString() == "6b74a76c-74c9-4147-a3ec-a602c0f9d49b")//指定货--自揽货
            {
                mcmbOverseasFiler.Enabled = true;
                mcmbOverseasFiler.SpecifiedBackColor = SystemColors.Info;
            }
            else
            {
                mcmbOverseasFiler.Enabled = false;
                _CurrentData.OverSeasFilerID = Guid.Empty;
                mcmbOverseasFiler.SpecifiedBackColor = Color.White;
                _CurrentData.OverSeasFilerName = string.Empty;
                mcmbOverseasFiler.Refresh();
            }

            SetAgentSourceByCompanyID(_CurrentData.CompanyID);
        }

        #region 主要是设置控件的颜色、可使用性等属性

        /// <summary>
        /// 总调用处，会把其它方法都执行一遍
        /// </summary>
        void RunAtOnce()
        {
            cmbType_SelectedIndexChanged(null, null);
            cmbOrderNo_TextChanged(this, null);
            cmbSalesType_SelectedIndexChanged(null, null);
            chkHasContract_CheckedChanged(null, null);

            #region 根据数据 设置 控件可操作

            if (_CurrentData.ID != Guid.Empty)
            {
                // 如果订单.订舱单.SO号已产生，则不允许更改业务类型，否则允许更改业务类型。
                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.OceanShippingOrderID) == false)
                    cmbType.Enabled = false;
                else
                    cmbType.Enabled = true;

                SetContainerDemandByBookingType();
            }

            SetHBLEnabledByIsOnlyMBL(_CurrentData.IsOnlyMBL);
            SetContractBoxByHasContract(_CurrentData.IsContract);
            #endregion

            RefreshBarEnabled();
        }

        #region 有了订舱号后，船公司和确认日期要求必填

        void cmbOrderNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbOrderNo.Text))
            {
                mcmbCarrier.SpecifiedBackColor = Color.White;
                dteSODate.BackColor = Color.White;
                //this.stxtReturnLocation.BackColor = Color.White;
                stxtAgentOfCarrier.BackColor = Color.White;

            }
            else
            {
                mcmbCarrier.SpecifiedBackColor = SystemColors.Info;
                dteSODate.BackColor = SystemColors.Info;
                //this.stxtReturnLocation.BackColor = SystemColors.Info;
                stxtAgentOfCarrier.BackColor = SystemColors.Info;
            }

            if (cmbOrderNo.Text.Contains(","))
            {
                cmbOrderNo.ToolTip = cmbOrderNo.Text;
            }
            if (isAddSo)
            {
                isAddSo = false;
            }
            else
            {
                SendKeys.Send("{Enter}");
            }


        }

        #endregion

        #region 合约选择规则

        private void txtContractNo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //UNDONE:合约选择规则
        }

        #endregion

        #region IsContract合约

        private void chkHasContract_CheckedChanged(object sender, EventArgs e)
        {
            SetContractBoxByHasContract(chkHasContract.Checked);
        }

        private void SetContractBoxByHasContract(bool hasContract)
        {
            txtContractNo.Enabled = hasContract;
            txtContractNo.BackColor = hasContract ? SystemColors.Info : txtNo.BackColor;
            if (hasContract)
            {
                ucInquirePrice.DataSource = new InquirePricePartInfo();
                ucInquirePrice.Enabled = false;
                ucInquirePrice.SetConfirmationEnabled = false;
            }
            else
            {
                ucInquirePrice.Enabled = true;
                ucInquirePrice.SetConfirmationEnabled = true;
            }
        }

        private void chkHasContract_Click(object sender, EventArgs e)
        {
            if (ucInquirePrice.Enabled)
            {
                ucInquirePrice.Enabled = false;
            }
            else
            {
                ucInquirePrice.Enabled = true;
            }
        }

        #endregion

        #region IsOnlyMBL 如果选择只出MBL，则HBL要求录入栏位不可以录入

        void chkIsOnlyMBL_CheckedChanged(object sender, EventArgs e)
        {
            SetHBLEnabledByIsOnlyMBL(chkIsOnlyMBL.Checked);
        }
        /// <summary>
        ///IsOnlyMBL如果为选择状态，则HBL相关输入项设为启用状态，否则设为禁用状态
        /// </summary>
        private void SetHBLEnabledByIsOnlyMBL(bool isOnlyMBL)
        {
            if (isOnlyMBL)
            {
                groupHBL.Enabled = false;
                _CurrentData.HBLPaymentTermID = null;
                _CurrentData.HBLReleaseType = FCMReleaseType.Unknown;
                _CurrentData.HBLRequirements = string.Empty;
            }
            else
            {
                groupHBL.Enabled = true;
                //if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.HBLPaymentTermID))
                //    _oceanBookingInfo.HBLPaymentTermID = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.Payment);
            }
        }

        #endregion

        #region 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑

        /// <summary>
        /// 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑
        /// </summary>
        private void SetContainerDemandByBookingType()
        {
            if (_CurrentData.OEOperationType == FCMOperationType.FCL)
            {
                containerDemandControl1.Enabled = true;
                containerDemandControl1.SpecifiedBackColor = SystemColors.Info;
            }
            else
            {
                containerDemandControl1.Enabled = false;
                containerDemandControl1.Text = string.Empty;
                containerDemandControl1.SpecifiedBackColor = txtNo.BackColor;
            }
        }

        /// <summary>
        /// 如果业务类型不是整箱，那么箱描述就不可编辑，否则可编辑
        /// </summary>
        private void SetLocalServiceByBookingType()
        {
            if (_CurrentData.OEOperationType == FCMOperationType.LCL)
                chkIsTruck.Enabled = true;
            else
            {
                chkIsTruck.Enabled = false;
                chkIsTruck.Checked = false;
            }
        }

        #endregion

        #region 刷新工具栏按钮的可使用性

        void RefreshBarEnabled()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(RefreshBarEnabled));
            }
            else
            {
                barAuditAndSave.Enabled = _CurrentData.State == OEOrderState.NewOrder;

                if (_CurrentData.ID == Guid.Empty)
                {
                    cmbType.Enabled = true;
                    barTruck.Enabled = false;
                    //this.barCustoms.Enabled = false;
                    barApplyAgent.Enabled = false;
                    barRefresh.Enabled = false;

                    barSubPrint.Enabled = false;
                    barPrintBookingConfirm.Enabled = false;
                    barPrintInWarehouse.Enabled = false;
                    barPrintOrder.Enabled = false;
                }
                else
                {
                    barReject.Enabled = _CurrentData.State == OEOrderState.NewOrder;

                    if (string.IsNullOrEmpty(_CurrentData.OceanShippingOrderNo))
                    {
                        barTruck.Enabled = false;
                        //this.barCustoms.Enabled = false;
                        cmbType.Enabled = true;
                    }
                    else
                    {
                        cmbType.Enabled = false;
                        barTruck.Enabled = _CurrentData.OEOperationType == FCMOperationType.FCL
                            || _CurrentData.OEOperationType == FCMOperationType.LCL;
                    }

                    barRefresh.Enabled = true;

                    if (string.IsNullOrEmpty(_CurrentData.OceanShippingOrderNo))
                    {
                        barSubPrint.Enabled = false;
                        barPrintBookingConfirm.Enabled = false;
                        barPrintInWarehouse.Enabled = false;
                        barPrintOrder.Enabled = false;
                    }
                    else
                    {
                        barSubPrint.Enabled = true;
                        barPrintBookingConfirm.Enabled = true;
                        barPrintInWarehouse.Enabled = true;
                        barPrintOrder.Enabled = true;
                    }

                    if (_CurrentData.State == OEOrderState.LoadPreVoyage ||
                        _CurrentData.State == OEOrderState.LoadVoyage ||
                        _CurrentData.State == OEOrderState.Closed ||
                        _CurrentData.State == OEOrderState.Rejected)
                    {
                        barApplyAgent.Enabled = false;
                    }
                    else
                    {
                        barApplyAgent.Enabled = true;
                    }
                }

                txtState.Text = EnumHelper.GetDescription<OEOrderState>(_CurrentData.State, LocalData.IsEnglish);
            }
        }

        #endregion

        #endregion

        #region 控件联动

        #region 数据变动填充控件默认值 客户变了就刷新揽货方式等逻辑

        #region Port And Voyage



        /// <summary>
        /// 交货地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetPlaceOfDeliveryByTransportClause()
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.PlaceOfDeliveryID)
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.TransportClauseID)) return;

            if (_CurrentData.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtPlaceOfDelivery.Tag = _CurrentData.PlaceOfDeliveryID = _CurrentData.PODID;
                stxtPlaceOfDelivery.Text = _CurrentData.PlaceOfDeliveryName = _CurrentData.PODName;
            }
        }

        /// <summary>
        /// 最终目的地 如果目的港运输条款<>Door，那么就为卸货港
        /// </summary>
        private void SetFinalDestinationByTransportClause()
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.FinalDestinationID)
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.TransportClauseID)) return;

            if (_CurrentData.TransportClauseName.Contains("-DOOR") == false)
            {
                stxtFinalDestination.Tag = _CurrentData.FinalDestinationID = _CurrentData.PlaceOfDeliveryID;
                stxtFinalDestination.Text = _CurrentData.FinalDestinationName = _CurrentData.PlaceOfDeliveryName;
            }
        }

        #endregion

        #region SetSalesDepartment

        /// <summary>
        /// 改变失去焦点后刷新揽货部门，如果有多个就清空，否则填充
        /// </summary>
        private void SetSalesDepartment()
        {
            List<UserOrganizationTreeList> userOrganizationTreeLists = new List<UserOrganizationTreeList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesID) == false)
            {
                userOrganizationTreeLists = UserService.GetUserOrganizationTreeList(_CurrentData.SalesID.Value);
                UserOrganizationTreeList orginazation = userOrganizationTreeLists.Find(o => o.IsDefault);
                if (orginazation != null)
                {
                    trsSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _CurrentData.SalesDepartmentID = orginazation.ID;
                    _CurrentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    trsSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _CurrentData.SalesDepartmentID = Guid.Empty;
                    _CurrentData.SalesDepartmentName = string.Empty;
                }
            }
        }

        void cmbSalesDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbSalesDepartment.EditValue == null || cmbSalesDepartment.EditValue.ToString() == string.Empty) return;
            //Guid salesDepartmentID = new Guid(cmbSalesDepartment.EditValue.ToString());
            //SetOperatorBySalesDepartmentID(salesDepartmentID);
        }

        /// <summary>
        /// 清空Sales后,自动清空Sales部门.操作
        /// </summary>
        private void ClearSalesDepartment()
        {
            //cmbSalesDepartment.Properties.Items.Clear();
            _CurrentData.SalesDepartmentID = Guid.Empty;
            //cmbOperator.Properties.Items.Clear();
            _CurrentData.BookingerID = null;
        }

        #endregion

        #region Other

        /// <summary>
        /// 根据公司和客户设置揽货方式
        /// </summary>
        private void SetSalesTypeByCustomerAndCompany()
        {
            if (_CurrentData.CompanyID != Guid.Empty && _CurrentData.CustomerID != Guid.Empty)
            {
                DataDictionaryInfo salesType = OceanExportService.GetSalesType(_CurrentData.CustomerID, _CurrentData.CompanyID);
                if (salesType != null)
                {
                    _CurrentData.SalesTypeID = salesType.ID;
                    _CurrentData.SalesTypeName = LocalData.IsEnglish ? salesType.EName : salesType.CName;

                    cmbSalesType.ShowSelectedValue(_CurrentData.SalesTypeID, _CurrentData.SalesTypeName);
                }
            }
        }

        /// <summary>
        /// 根据操作口岸ID设置操作和文件栏的数据源
        /// </summary>
        private void SetOperatorByCompany()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID))
            {

            }
            else
            {

            }
        }

        #endregion

        #region 设置默认海外部客服

        /// <summary>
        /// 当前客户最近业务所对应的海外部客服or 当前客户为新客户and当前揽货人最近业务所对应的海外部客服
        /// </summary>
        void SetDefaultOverseasFiler()
        {
            List<UserInfo> users = OceanExportService.GetOverseasFilersList(_CurrentData.CustomerID, _CurrentData.SalesID,
                DateTime.Now.AddDays(-30), DateTime.Now, 1);

            if (users.Count > 0)
            {
                mcmbOverseasFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            }
        }

        /// <summary>
        /// 当前客户最近业务所对应的文件or 当前客户为新客户and当前揽货人最近业务所对应的文件
        /// </summary>
        void SetDefaultFiler()
        {
            List<UserInfo> users = OceanExportService.GetFilersList(_CurrentData.CustomerID, _CurrentData.SalesID, _CurrentData.CompanyID,
                   DateTime.Now.AddDays(-30), DateTime.Now, 1);

            if (users.Count > 0)
            {
                mcmbFiler.ShowSelectedValue(users[0].ID, LocalData.IsEnglish ? users[0].EName : users[0].CName);
            }
        }

        /// <summary>
        /// TODO: 和FSD作者仔细核对这个逻辑？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            SetDefaultOverseasFiler();
            SetDefaultFiler();

            SetSalesDepartment();
        }

        #endregion

        #region Customers

        /// <summary>
        /// 公司改变
        /// </summary>
        private void CompanyChanged()
        {
            SetSalesTypeByCustomerAndCompany();
            SetOperatorByCompany();
            SetAgentSourceByCompanyID(_CurrentData.CompanyID);
            //SetAgetnEnabledByPlaceOfDeliveryAndCompany();

            orderFeeEditPart1.SetCompanyID(_CurrentData.CompanyID);
            stxtCustomer.CompanyID = _CurrentData.CompanyID;
        }

        /// <summary>
        /// 客户改变后需设置"订舱客户","收货人","代理","揽货方式","最近业务"
        /// </summary>
        /// <param name="customerType">客户的类型,请在方法外部获取</param>
        private void CustomerChanged(CustomerType? customerType)
        {
            SetBookingCustomerByCustomerAndTradeTerm();
            SetConsigneeByCustomerAndTradeTerm();
            SetSalesTypeByCustomerAndCompany();


            SetAgentSourceByCompanyID(_CurrentData.CompanyID);
            SetAgetnByCustomerAndCompany(customerType);

            SetDefaultOverseasFiler();
            SetDefaultFiler();
            SetShipperByBookingCustomerAndTradeTerm();
        }

        #region Agent
        void stxtAgent_EditValueChanged(object sender, EventArgs e)
        {
            if (stxtAgent.EditValue != null && stxtAgent.EditValue.ToString().Length > 0)
            {
                Guid id = new Guid(stxtAgent.EditValue.ToString());
                BulidAgentDescriqitonByID(id);
                AddLastestContact(id, stxtAgent, ContactType.Customer);
            }
        }

        /// <summary>
        /// 如果客户类型为货代和客户所在国家与操作口岸所在国家不同就填充代理
        /// "填充"的意思,就是把"代理"的ID和描述设置成和"客户"一样
        /// TODO:这里的客户描述信息应该从“客户”处复制，为什么要从服务里面获取？
        /// </summary>
        /// <param name="customerType">客户的类型,请在方法外部获取</param>
        private void SetAgetnByCustomerAndCompany(CustomerType? customerType)
        {
            if (customerType == null
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID)
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CustomerID))
            {
                return;
            }

            if (customerType.Value == CustomerType.Forwarding
                && !OceanExportService.IsCustomerAndCompanySameCountry(_CurrentData.CustomerID, _CurrentData.CompanyID))
            {
                stxtAgent.Text = _CurrentData.AgentName = _CurrentData.CustomerName;
                stxtAgent.EditValue = _CurrentData.AgentID = _CurrentData.CustomerID;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.AgentID, _CurrentData.AgentDescription);
            }
        }

        /// <summary>
        /// 如果交货地所在的国家不存在于公司配置中客户对应的国家，那么就为只读，否则可以输入
        /// </summary>
        private void SetAgetnEnabledByPlaceOfDeliveryAndCompany()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.PlaceOfDeliveryID))
            {
                return;
            }
            // TODO:“指定货”目前在数据库的字典表里面还没有对应的英文
            if (
                    (
                        string.IsNullOrEmpty(_CurrentData.SalesTypeName) == false &&
                        (_CurrentData.SalesTypeName.Contains("指定货") || _CurrentData.SalesTypeName.ToUpper().Contains("AGENT"))
                    )
                || OceanExportService.IsPortCountryExistCompanyConfig(_CurrentData.PlaceOfDeliveryID, null))
            {
                stxtAgent.Enabled = true;
            }
            else
            {
                stxtAgent.Enabled = false;
                stxtAgent.Text = _CurrentData.AgentName = string.Empty;
                stxtAgent.EditValue = _CurrentData.AgentID = Guid.Empty;
                _CurrentData.AgentDescription = new CustomerDescription();
            }
        }

        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID(Guid companyID)
        {
            stxtAgent.DataSource = null;
            if (ArgumentHelper.GuidIsNullOrEmpty(companyID))
            {
                stxtAgent.Enabled = false;
                return;
            }

            bool isFirst = false;
            if (_CacheAgentCustomersList == null)
            {
                _CacheAgentCustomersList = ConfigureService.GetCompanyAgentList(_CurrentData.CompanyID, true);
                isFirst = true;
            }

            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CustomerID) &&
                (string.IsNullOrEmpty(_CurrentData.SalesTypeName) == false &&
                        (_CurrentData.SalesTypeName.Contains("指定货") ||
                        _CurrentData.SalesTypeName.ToUpper().Contains("AGENT"))))
            {
                CustomerList find = (from d in _CacheAgentCustomersList where d.ID == _CurrentData.CustomerID select d).Take(1).SingleOrDefault();
                if (find == null)
                {

                    CustomerList opCustomer = new CustomerList();  //业务的客户
                    opCustomer.ID = _CurrentData.CustomerID;
                    opCustomer.IsDangerous = true;
                    opCustomer.EName = _CurrentData.CustomerName;
                    _CacheAgentCustomersList.Insert(1, opCustomer);
                }
            }
            else
            {
                CustomerList CustomerFind = (from d in _CacheAgentCustomersList where d.IsDangerous == true select d).Take(1).SingleOrDefault();
                if (CustomerFind != null)
                {
                    _CacheAgentCustomersList.Remove(CustomerFind);
                    if (_CurrentData.AgentID == CustomerFind.ID)
                    {
                        _CurrentData.AgentID = null;
                        _CurrentData.AgentName = string.Empty;
                        _CurrentData.AgentDescription = new CustomerDescription();
                    }
                }
            }
            if (isFirst)
            {
                CustomerList emptyCustomer = new CustomerList();
                emptyCustomer.CName = emptyCustomer.EName = string.Empty;
                emptyCustomer.ID = Guid.Empty;
                _CacheAgentCustomersList.Insert(0, emptyCustomer);
            }

            SetAgentSource(_CacheAgentCustomersList);
        }

        private void SetAgentSource(List<CustomerList> agentCustomers)
        {

            stxtAgent.DataSource = agentCustomers;
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.AgentID))
            {
                _CurrentData.AgentID = agentCustomers[0].ID;
                stxtAgent.EditValue = _CurrentData.AgentID;
            }
            stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
            stxtAgent.EditValueChanged += stxtAgent_EditValueChanged;
        }

        /// <summary>
        /// 根据ID生成代理的描述和把描述填充到描述框
        /// </summary>
        int i = 0;
        private void BulidAgentDescriqitonByID(Guid? id)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(id))
            {
                stxtAgent.CustomerDescription = _CurrentData.AgentDescription = new CustomerDescription();
            }
            else
            {
                if (i != 0)
                {
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.AgentDescription);
                }

                stxtAgent.CustomerDescription = _CurrentData.AgentDescription;
            }
            i++;
            //this._oceanBookingInfo.IsDirty = false;
        }

        #endregion


        /// <summary>
        /// 设置发货人 如果贸易条款为CIF，那么就为订舱客户
        /// </summary>
        private void SetShipperByBookingCustomerAndTradeTerm()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ShipperID) == false
                || ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.TradeTermID))
            {
                return;
            }

            if (_CurrentData.TradeTermName == "CIF"
                || _CurrentData.TradeTermName == "C&F")
            {
                stxtShipper.Tag = _CurrentData.ShipperID = _CurrentData.BookingCustomerID;
                stxtShipper.Text = _CurrentData.ShipperName = _CurrentData.BookingCustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.ShipperID, _CurrentData.ShipperDescription);
            }
        }

        /// <summary>
        /// 收货人:如果贸易条款为FOB或EXWORK，那么就为客户
        /// </summary>
        private void SetConsigneeByCustomerAndTradeTerm()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ConsigneeID) == false) return;

            if (_CurrentData.TradeTermName == "FOB"
                || _CurrentData.TradeTermName == "EXW")
            {
                stxtConsignee.Tag = _CurrentData.ConsigneeID = _CurrentData.CustomerID;
                stxtConsignee.Text = _CurrentData.ConsigneeName = _CurrentData.CustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.ConsigneeID, _CurrentData.ConsigneeDescription);
            }

            ResetDescription();
        }

        /// <summary>
        /// 设置订舱客户和发货（订舱客户:如果贸易条款为CIF，那么就为客户
        /// </summary>
        private void SetBookingCustomerByCustomerAndTradeTerm()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingCustomerID) == false)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_CurrentData.TradeTermName)
                && _CurrentData.TradeTermName.Contains("CIF"))
            {
                stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = _CurrentData.CustomerID;
                stxtBookingCustomer.SetCustomerID(_CurrentData.BookingCustomerID);

                stxtBookingCustomer.Text = _CurrentData.BookingCustomerName = _CurrentData.CustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentData.CustomerID, _CurrentData.BookingCustomerDescription);
            }
        }

        #endregion

        #endregion

        #region 基础信息和PO的Tab之间切换时

        /// <summary>
        /// 控制了延迟加载PO的数据源
        /// </summary>
        void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page == tabPagePO)
            {
                xtraTabControl1.SelectedPageChanged -= xtraTabControl1_SelectedPageChanged;
                bookingPOEditPart1.InitData(_CurrentData.ID);
                bookingPOEditPart1.IsOrderPO = false;
            }
        }

        #endregion

        #region 最近十票业务
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbOrderNo_Enter(object sender, EventArgs e)
        {
            List<ShippingOrderList> shippingOrderList = OceanExportService.GetShippingOrderList(_CurrentData.OEOperationType
                                                                                              , _CurrentData.POLID
                                                                                              , _CurrentData.PODID
                                                                                              , _CurrentData.PlaceOfDeliveryID
                                                                                              , LocalData.UserInfo.LoginID
                                                                                              , DateTime.Now.AddDays(-30).Date
                                                                                              , DateTime.Now.DateAttachEndTime()
                                                                                              , 0);

            if (shippingOrderList.Count > 0)
            {
                //Dictionary<Guid, Guid> tempListToAvoidRepeatRecords = new Dictionary<Guid, Guid>();

                cmbOrderNo.Properties.Items.Clear();
                foreach (var item in shippingOrderList)
                {
                    //if (!tempListToAvoidRepeatRecords.ContainsKey(item.ID))
                    //{
                    //tempListToAvoidRepeatRecords.Add(item.ID, item.ID);
                    cmbOrderNo.Properties.Items.Add(item);
                    //}
                }

                cmbOrderNo.Invalidate();
            }
        }


        /// <summary>
        /// 如果是选择已有订舱号失去焦点后，链接确认日期、承运人、船公司、合约号、驳船、大船、离港日、到港日、截关日、截柜日、截文件日、还柜地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_CurrentData.IsDirty)
            {
                if (cmbOrderNo.SelectedItem != null)
                {
                    ShippingOrderList list = cmbOrderNo.SelectedItem as ShippingOrderList;
                    if (list != null)
                    {
                        _CurrentData.SODate = list.SODate;
                        _CurrentData.CarrierID = list.CarrierID;
                        _CurrentData.CarrierName = list.CarrierName;
                        if (list.AgentofcarrierID.HasValue)
                        {
                            _CurrentData.AgentOfCarrierID = list.AgentofcarrierID.Value;
                        }
                        _CurrentData.AgentOfCarrierName = list.AgentOfCarrierName;
                        _CurrentData.ContractID = list.FreightRateID;
                        _CurrentData.ContractNo = list.ContractNo;
                        _CurrentData.PreVoyageID = list.PreVoyageID;
                        _CurrentData.PreVoyageName = list.PreVoyageName;
                        _CurrentData.VoyageID = list.VoyageID;
                        _CurrentData.VoyageName = list.VoyageName;
                        _CurrentData.ETD = list.ETD;
                        _CurrentData.ETA = list.ETA;
                        _CurrentData.ClosingDate = list.ClosingDate;
                        _CurrentData.CYClosingDate = list.CYClosingDate;
                        _CurrentData.DOCClosingDate = list.DOCClosingDate;
                        _CurrentData.ReturnLocationID = list.ReturnLocationID;
                        _CurrentData.ReturnLocationName = list.ReturnLocationName;
                    }
                }
            }

            _CurrentData.OceanShippingOrderNo = cmbOrderNo.Text.Trim();

            if (cmbOrderNo.Text.Trim() == string.Empty)
            {
                return;
            }

            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.PlaceOfDeliveryID)
                 && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.PODID))
            {
                ShippingLineList shippingline = OceanExportService.GetShippingLineForDeliveryAndPOD(_CurrentData.PlaceOfDeliveryID, _CurrentData.PODID);

                if (shippingline != null)
                {
                    _CurrentData.ShippingLineID = shippingline.ID;
                    _CurrentData.ShippingLineName = LocalData.IsEnglish ? shippingline.EName : shippingline.CName;
                    cmbShippingLine.ShowSelectedValue(_CurrentData.ShippingLineID, _CurrentData.ShippingLineName);
                }
            }

            List<ConfigureCustomerInfo> conCustomer = FCMCommonService.GetConfigureCustomers(_CurrentData.CompanyID);
            ConfigureCustomerInfo conCustomerinfo = null;
            if (conCustomer != null & conCustomer.Count > 0)
            {
                conCustomerinfo = conCustomer.FirstOrDefault(con => con.IsDefault == true);

            }

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingPartyID))
            {
                if (conCustomerinfo != null)
                {
                    _CurrentData.BookingPartyID = conCustomerinfo.CustomerID;
                    _CurrentData.BookingPartyName = LocalData.IsEnglish ? conCustomerinfo.CustomerEname : conCustomerinfo.CustomerCname;

                    CustomerDescription bookingPartyDescription = new CustomerDescription();
                    ICPCommUIHelper.SetCustomerDesByID(_CurrentData.BookingPartyID, bookingPartyDescription);
                    _CurrentData.BookingShipperdescription = bookingPartyDescription;
                    _CurrentData.BookingShipperID = _CurrentData.BookingPartyID;
                    _CurrentData.BookingShipperName = _CurrentData.BookingPartyName;
                }
            }

            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ShippingLineID))
            {
                if (conCustomerinfo == null)
                {
                    return;
                }


                if (OEUtility.USAShippingLines.Contains((Guid)_CurrentData.ShippingLineID))
                {
                    _CurrentData.ScacCode = conCustomerinfo.USScacCode;
                }
                else if (OEUtility.CSShippingLines.Contains((Guid)_CurrentData.ShippingLineID))
                {

                    _CurrentData.ScacCode = conCustomerinfo.CAScacCode;
                }
            }
        }




        #endregion

        #region Cargo

        private void cmbCargoType_Enter(object sender, EventArgs e)
        {
            if (cmbCargoType.EditValue == null
                || cmbCargoType.EditValue.ToString() == string.Empty)
            {
                return;
            }
            CargoType cargoType = (CargoType)Enum.Parse(typeof(CargoType), cmbCargoType.EditValue.ToString());
            SetCargo(sender as Control, cargoType);
        }

        private void cmbCargoType_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCargoType.EditValue == null)
            {
                _CurrentData.CargoDescription = null;
                RemoveCargoPart();
                return;
            }

            CargoType cargoType = (CargoType)Enum.Parse(typeof(CargoType), cmbCargoType.EditValue.ToString());
            RemoveCargoPart();
            SetCargo(sender as Control, cargoType);
        }

        private void SetCargo(Control sender, CargoType cargoType)
        {
            if (!cmbCargoType.Focused)
            {
                return;
            }
            if (cargoType == CargoType.Awkward)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is AwkwardCargo == false)
                {
                    AwkwardCargo cargo = new AwkwardCargo();
                    cargo.NetWeightUnit = cmbWeightUnit.Text;
                    cargo.GrossWeightUnit = cmbWeightUnit.Text;
                    cargo.Quantity = (int)numQuantity.Value;
                    _CurrentData.CargoDescription = new CargoDescription(cargo);
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is AwkwardDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new AwkwardDescriptionPart();
                    cargoDescriptionPart1.ShowWeightUnit(_CacheWeightUnits);
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dangerous)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is DangerousCargo == false)
                {
                    _CurrentData.CargoDescription = new CargoDescription(new DangerousCargo());
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is DangerousDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new DangerousDescriptionPart();
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Dry)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is DryCargo == false)
                {
                    _CurrentData.CargoDescription = new CargoDescription(new DryCargo());
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is DryDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new DryDescriptionPart();
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            else if (cargoType == CargoType.Reefer)
            {
                if (_CurrentData.CargoDescription == null
                    || _CurrentData.CargoDescription.Cargo == null || _CurrentData.CargoDescription.Cargo is ReeferCargo == false)
                {
                    _CurrentData.CargoDescription = new CargoDescription(new ReeferCargo());
                    _CurrentData.IsDirty = true;
                }

                if (cargoDescriptionPart1 is ReeferDescriptionPart == false)
                {
                    cargoDescriptionPart1 = new ReeferDescriptionPart();
                    navBarGroupControlContainer2.Controls.Add(cargoDescriptionPart1);
                }
            }
            cargoDescriptionPart1.SetParentControl(sender, _CurrentData.CargoDescription, null);
        }

        private void RemoveCargoPart()
        {
            if (cargoDescriptionPart1 != null)
            {
                cargoDescriptionPart1.Hide();
                navBarGroupControlContainer2.Controls.Remove(cargoDescriptionPart1);
                cargoDescriptionPart1.Dispose();
            }
        }

        #endregion

        #endregion

        #endregion

        #region 其他事件
        /// <summary>
        /// 选择运价合约
        /// </summary>
        private void txtContractNo_Click(object sender, EventArgs e)
        {
            if (!ClientOceanExportService.IsNeedAccept(_CurrentData.ID)) return;
            ClientOceanExportService.SelectContract(_CurrentData, SelectType.Contract, AfterSelectContract);
        }
        /// <summary>
        /// 
        /// </summary>
        private void barEmailbooking_ItemClick(object sender, ItemClickEventArgs e)
        {
            EmailBookingReportPanel emailPanel = new EmailBookingReportPanel();
            emailPanel.BookingID = _CurrentData.ID;
            emailPanel.CarrierID = _CurrentData.CarrierID;
            emailPanel.ShowDialog();
        }
        /// <summary>
        /// 
        /// </summary>
        private void chkOrderThirdPay_CheckedChanged(object sender, EventArgs e)
        {
            stxtPlacePayOrder.Enabled = chkOrderThirdPay.Checked;
            if (chkOrderThirdPay.Checked == false)
            {
                _CurrentData.CollectbyAgentOrderID = Guid.Empty;
                _CurrentData.CollectbyAgentNameOrder = string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrderNo_Leave(object sender, EventArgs e)
        {
            if (cmbOrderNo.ToolTip != cmbOrderNo.Text)
            {
                cmbOrderNo.ToolTip = cmbOrderNo.Text;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSoNo SoNoPart = PartWorkItem.Items.AddNew<AddSoNo>();
            SoNoPart.AddS += delegate(string prams)
            {
                isAddSo = true;
                this.cmbOrderNo.Text += "," + prams.ToUpper();
            };

            PartLoader.FakeShowDialog(PartWorkItem, SoNoPart, (LocalData.IsEnglish ? "Add SoNo" : "添加关单号"), FormWindowState.Normal, this);

            cmbOrderNo_Leave(null, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stxtVoyage_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(stxtVoyage.EditText))
            {
                string[] parameters = CommonUtility.ProcessParameter(stxtVoyage.EditText);
                VoyageETDETAList dates = TransportFoundationService.GetVoyageListETDETA(parameters[0], parameters[1], _CurrentData.CarrierID, _CurrentData.POLID, _CurrentData.PODID);
                if (dates != null)
                {
                    if (_CurrentData.ETA == null && _CurrentData.ETD == null)
                    {
                        _CurrentData.ETA = dates.ETA;
                        _CurrentData.ETD = dates.ETD;
                    }
                    else
                    {
                        bool isunlike = false;
                        if ((_CurrentData.ETA == null ? "19900101" : ((DateTime)_CurrentData.ETA).ToString("yyyyMMdd")) != (dates.ETA == null ? "19900101" : ((DateTime)dates.ETA).ToString("yyyyMMdd")))
                        {
                            isunlike = true;
                        }
                        if ((_CurrentData.ETD == null ? "19900101" : ((DateTime)_CurrentData.ETD).ToString("yyyyMMdd")) != (dates.ETD == null ? "19900101" : ((DateTime)dates.ETD).ToString("yyyyMMdd")))
                        {
                            isunlike = true;
                        }

                        if (isunlike)
                        {
                            DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Reselect the vessel/voyage, whether to import ETD,ETA?" : "重新选择了船名航次，是否导入ETD,ETA?"
                                    , LocalData.IsEnglish ? "Tip" : "提示"
                                    , MessageBoxButtons.YesNo
                                    );
                            if (result == DialogResult.Yes)
                            {
                                _CurrentData.ETA = dates.ETA;
                                _CurrentData.ETD = dates.ETD;
                            }
                        }
                    }
                    bsBookingInfo.ResetBindings(false);
                }
            }
        }
        /// <summary>
        /// 询价面板新增事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInquirePrice_ClickNew(object sender, EventArgs e)
        {
            if (_CurrentData.IsDirty
                || _CurrentData.IsNew
                )
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please save the page data" : "请先保存页面数据！");
                return;
            }
            InquierOceanRate rate = new InquierOceanRate();
            rate.CustomerID = _CurrentData.CustomerID;
            rate.CustomerName = _CurrentData.CustomerName;
            rate.PODID = _CurrentData.PODID;
            rate.PODName = _CurrentData.PODName;
            rate.CarrierID = _CurrentData.CarrierID;
            rate.CarrierName = _CurrentData.CarrierName;
            ClientInquireRateService.InquireOceanRate();
        }

        #endregion
        
        #region 工具栏事件

        #region 验证界面输入

        private bool ValidateData()
        {
            EndEdit();

            dxErrorProvider1.ClearErrors();

            List<bool> isScrrs = new List<bool> { true, true, true,true };

            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesTypeID) && _CurrentData.SalesTypeID.ToString().Equals("e34bdcaa-4253-41c0-b14b-e38111cf2fc8") && ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.OverSeasFilerID))
            {
                devErrorCheck.SetError(mcmbBookinger, LocalData.IsEnglish ? "Please selected OverSeas CS." : "请选择海外客服.");
            }


            isScrrs[0] = _CurrentData.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesTypeID)
                           && _CurrentData.SalesTypeID.ToString().Equals("e34bdcaa-4253-41c0-b14b-e38111cf2fc8")
                           && ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.OverSeasFilerID))
                       {
                           e.SetErrorInfo("OverSeasFilerID", LocalData.IsEnglish ? "Nomination Cargo，G.C.S. Must Input." : "指定货，总客服必须输入.");
                           isScrrs[0] = false;
                       }

                       //如果有订舱号的判断条件
                       if (string.IsNullOrEmpty(_CurrentData.OceanShippingOrderNo) == false)
                       {
                           if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.AgentOfCarrierID))
                           {
                               e.SetErrorInfo("AgentOfCarrierID", LocalData.IsEnglish ? "AgentOfCarrier Must Input" : "有订舱号，承运入必须输入.");
                               isScrrs[0] = false;
                           }

                           if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.VoyageID)
                               && ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.PreVoyageID)
                               && ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingPartyID))
                           {
                               e.SetErrorInfo("VoyageID", LocalData.IsEnglish ? "Voyage or PreVoyage or BookingParty Must Input" : "有订舱号.订舱人,大船和驳船至少要填写一个.");
                               isScrrs[0] = false;
                           }

                           if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CarrierID))
                           {
                               e.SetErrorInfo("CarrierID", LocalData.IsEnglish ? "Carrier Must Input" : "有订舱号，船公司必须输入.");
                               isScrrs[0] = false;
                           }

                           if (!_CurrentData.SODate.HasValue)
                           {
                               isScrrs[0] = false;
                               e.SetErrorInfo("SODate", LocalData.IsEnglish ? "SODate Must Input" : "有订舱号，确认日期必须输入.");
                           }
                       }

                       //有驳船，驳船离港日必须输入
                       if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.PreVoyageID) && !_CurrentData.PORETD.HasValue)
                       {
                           e.SetErrorInfo("PORETD", LocalData.IsEnglish ? "PORETD Must Input" : "有驳船，驳船离港日必须输入.");
                           isScrrs[0] = false;
                       }

                       //有大船 大船离港日 ，到港日必须输入
                       if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.VoyageID))
                       {
                           if (!_CurrentData.ETD.HasValue)
                           {
                               e.SetErrorInfo("ETD", LocalData.IsEnglish ? "ETD Must Input" : "有大船，大船离港日必须输入.");
                               isScrrs[0] = false;
                           }
                           if (!_CurrentData.ETA.HasValue)
                           {
                               e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETA Must Input" : "有大船，大船到港日必须输入.");
                               isScrrs[0] = false;
                           }
                       }

                       //如果选择合约的判断条件
                       if (_CurrentData.IsContract)
                       {
                           if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ContractID))
                           {
                               e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "Contract Must Input" : "合约必须输入.");
                               isScrrs[0] = false;
                           }
                       }

                       if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.ContractID) && string.IsNullOrEmpty(CarrierName))
                       {
                           CustomerInfo cus = CustomerService.GetCustomerInfo((Guid)_CurrentData.CarrierID);
                           CarrierName = cus.Code;

                           bool isUsa = false;
                           if (_CurrentData.FinalDestinationID != null)
                           {
                               LocationInfo FinalDestination = GeographyService.GetLocationInfo((Guid)_CurrentData.FinalDestinationID);
                               if (FinalDestination.CountryID.ToString().ToUpper() == "37F06C2D-E5F6-4A6F-BB55-9DA3EC3B42A4")
                               {
                                   isUsa = true;
                               }
                           }
                           else
                           {
                               if (OEUtility.USAShippingLines.Count(r => r == _CurrentData.ShippingLineID) > 0)
                               {
                                   isUsa = true;
                               }
                           }

                           if (CarrierName == "MSC")
                           {
                               if (isUsa)
                               {
                                   if (_CurrentData.BookingPartyID.ToString().ToUpper() != "0751E34D-6FC6-E511-938F-0026551CA878")
                                   {
                                       e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used DAWU as the booking party" : "MSC的合约，只能用达悟作为订舱人");
                                       isScrrs[0] = false;
                                   }
                                   if (_CurrentData.BookingShipperID.ToString().ToUpper() != "0751E34D-6FC6-E511-938F-0026551CA878")
                                   {
                                       e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used DAWU as the booking shipper" : "MSC的合约，只能用达悟作为订舱发货人");
                                       isScrrs[0] = false;
                                   }
                                   if (_CurrentData.BookingConsigneeID.ToString().ToUpper() != "B8006234-2F00-E611-80D5-2047477D7A58")
                                   {
                                       e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used JDY as the booking consignee" : "MSC的合约，只能用JDY作为订舱收货人");
                                       isScrrs[0] = false;
                                   }
                                   if (_CurrentData.AgentID.ToString().ToUpper() != "B8006234-2F00-E611-80D5-2047477D7A58")
                                   {
                                       e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used DAWU as the agent" : "MSC的合约，只能用JDY作为代理");
                                       isScrrs[0] = false;
                                   }
                               }
                           }
                       }

                       if (_CurrentData.POLID != Guid.Empty && _CurrentData.POLID == _CurrentData.PODID)
                       {
                           isScrrs[0] = false;
                           e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                       }

                       if (_CurrentData.ETA != null && _CurrentData.ETD != null
                           && _CurrentData.ETD >= _CurrentData.ETA)
                       {
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");
                           isScrrs[0] = false;
                       }

                       if (!_CurrentData.OceanShippingOrderNo.IsNullOrEmpty() && _CurrentData.DOCClosingDate == null)
                       {
                           e.SetErrorInfo("DOCClosingDate", LocalData.IsEnglish ? "DOC closing date can't be empty." : "截文件日不能为空.");
                           isScrrs[0] = false;
                       }

                       if (_CurrentData.GateInDate != null && _CurrentData.ETD != null
                       && (_CurrentData.GateInDate > _CurrentData.ETD && ((DateTime)_CurrentData.GateInDate).ToShortDateString() != ((DateTime)_CurrentData.ETD).ToShortDateString()))
                       {
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "GateInDate can't bigger ETD." : "进港日不能大于ETD.");
                           isScrrs[0] = false;
                       }

                       if (_CurrentData.ExpectedShipDate != null && _CurrentData.ExpectedShipDate.Value != null && _CurrentData.ExpectedArriveDate != null
                           && _CurrentData.ExpectedShipDate.Value.Date >= _CurrentData.ExpectedArriveDate.Value.Date)
                       {
                           isScrrs[0] = false;
                           e.SetErrorInfo("ExpectedShipDate", LocalData.IsEnglish ? "ExpectedShipDate can't bigger ExpectedArriveDate." : "期望出运日不能大于期望到达日.");
                       }

                       if (_CurrentData.ContainerDescription != null)
                       {
                           if (_CurrentData.ContainerDescription.ToString() != containerDemandControl1.Text)
                           {
                               _CurrentData.IsDirty = true;
                           }
                       }
                       //果选择整箱业务类型，箱需求必输；箱需求逻辑,点击对应的箱型n次,则显示n*箱型
                       if (_CurrentData.OEOperationType == FCMOperationType.FCL)
                       {
                           if (containerDemandControl1.Text.Trim().Length == 0)
                           {
                               e.SetErrorInfo("ContainerDescription", LocalData.IsEnglish ? "FCL business container needs to be input." : "整箱业务必须输入箱需求.");
                               isScrrs[0] = false;
                           }

                       }

                       //截AMS日必须小于离港日
                       if (_CurrentData.AMSClosingDate.HasValue && _CurrentData.ETD.HasValue)
                       {
                           var amsClosingDate = _CurrentData.AMSClosingDate.Value;
                           var etd = _CurrentData.ETD.Value;
                           if (DateTime.Compare(amsClosingDate, etd) > 0)
                           {
                               e.SetErrorInfo("AMSClosingDate", LocalData.IsEnglish ? "Cut files must be smaller than the departure date." : "截文件日必须小于离港日.");
                               isScrrs[0] = false;
                           }
                       }
                       //宁波口岸需验证关键字段是否包含中文
                       if (("" + _CurrentData.CompanyID).ToUpper() == "A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9")
                       {
                           if (Utility.IsContainsChinese(_CurrentData.Commodity))
                           {
                               isScrrs[0] = false;
                               e.SetErrorInfo("Commodity", LocalData.IsEnglish ? "Commodity include Chinese." : "品名中包含中文.");
                           }
                           if (Utility.IsContainsChinese(_CurrentData.BookingExplanation))
                           {
                               isScrrs[0] = false;
                               e.SetErrorInfo("BookingExplanation", LocalData.IsEnglish ? "Booking explanation include Chinese." : "订舱说明中包含中文.");
                           }
                       }
                       
                       //把箱需求转换成对象
                       _CurrentData.ContainerDescription = new ContainerDescription(containerDemandControl1.Text.Trim());
                   }
               );

            #region childParts
            if (bookingPOEditPart1.ValidateData() == false)
            {
                isScrrs[1] = false;
                xtraTabControl1.SelectedTabPageIndex = 1;
            }
            if (UCBookingOtherPart.ValidateData() == false)
            {
                isScrrs[2] = false;
            }
            if(partDelegate.ValidateData()==false)
            {
                isScrrs[3] = false;
            }

            #endregion

            bool isScrr = true;
            foreach (var item in isScrrs)
                if (item == false) isScrr = false;

            if (isScrrs[0] == false)
                xtraTabControl1.SelectedTabPageIndex = 0;

            if (!orderFeeEditPart1.ValidateData())
            {
                isScrr = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
            if (!partDelegate.ValidateData())
            {
                isScrr = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
            return isScrr;
        }

        #endregion

        #region 保存

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Save(_CurrentData, false) == true)
            {
                SaveOtherPart();
            }
        }

        #region SavingClose
        /// <summary>
        /// 保存并关闭
        /// </summary>
        void barSavingClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!ValidateData())
                    return;
                BeginThreadInit();
                SavingThreadStart(true);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "SavingClose" + ex.Message);
            }
        }

        void barCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                barSavingTools.Visible = false;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "Cancel" + ex.Message);
            }
        }
        void barlabMessage_ItemClick(object sender, ItemClickEventArgs e)
        {
            string labTag = saveState();
            if (!string.IsNullOrEmpty(labTag) && labTag.Equals("success"))
            {
                barSavingTools.Visible = false;
            }
        }
        void RefreshTime_Tick(object sender, EventArgs e)
        {
            if (!barSavingTools.Visible)
                return;
            DateTime curTime = DateTime.Now;
            TimeSpan span = curTime - ThreadStartTime;
            barlabSeconds.Visibility = BarItemVisibility.Always;
            barlabSeconds.Caption = string.Format((LocalData.IsEnglish ? "({0} seconds)" : "({0} 秒)"), (int)span.TotalSeconds);
            if (span.TotalSeconds >= LocalData.AsynchronousSaveTimeout)
            {
                barCancel.Visibility = BarItemVisibility.Always;
            }
        }

        private void BeginThreadInit()
        {
            barlabMessage.Tag = "";
            barlabSeconds.Caption = LocalData.IsEnglish ? "(0 seconds)" : "(0 秒)";
            SetLableMessage(LocalData.IsEnglish ? "Saving is in progress..." : "正在保存...", "saving");
            ThreadStartTime = DateTime.Now;
            TimerSaveData.Start();

            barlabSeconds.Visibility = BarItemVisibility.Always;
            barCancel.Visibility = BarItemVisibility.Never;
            barSavingTools.Visible = true;
        }

        private void SavingThreadStart(bool isCloseThis)
        {
            ThreadSaveData = new Thread(SavingAndClose);
            ThreadSaveData.Name = "SavingBooking";
            ThreadSaveData.Start(isCloseThis);
        }

        private void SavingAndClose(object o)
        {
            try
            {
                ClientHelper.SetApplicationContext();
                bool isCloseThis = (bool)o;
                if (Save(_CurrentData, false, false))
                {
                    SaveOtherPart();
                    SetLableMessage(
                    string.Format((LocalData.IsEnglish ? "Saving is successful at {0}" : "保存成功 于 {0}"), DateTime.Now.ToString("HH:mm:ss"))
                    , "success");
                }
                if (saveState().Equals("success"))
                {
                    if (isCloseThis && FindForm() != null)
                    {
                        FindForm().Close();
                    }
                }
            }
            catch (Exception ex)
            {
                SetLableMessage("", "exception");
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message + "SavingAndClose");
            }
        }

        private string saveState()
        {
            return barlabMessage.Tag == null ? "saving" : barlabMessage.Tag.ToString();
        }
        private void SetLableMessage(string message, string state)
        {
            if (InvokeRequired)
                Invoke(new Action<string, string>(SetLableMessage), message, state);
            else
            {
                string beforeState = saveState();
                switch (state)
                {
                    case "exception":
                        if (beforeState.Equals(state))
                            barlabMessage.Caption += message;
                        else
                            barlabMessage.Caption = string.IsNullOrEmpty(message) ? "保存出现系统错误" : message;
                        break;
                    default:
                        barlabMessage.Caption = message;
                        break;
                }
                if (!beforeState.Equals("exception"))
                    barlabMessage.Tag = state;
                if (!state.Equals("success") && !state.Equals("exception")) return;
                TimerSaveData.Stop();
                barlabSeconds.Visibility = BarItemVisibility.Never;
            }
        }
        #endregion

        /// <summary>
        /// 保存其它面板数据
        /// </summary>
        /// <returns></returns>
        private bool SaveOtherPart()
        {
            if (UCBookingOtherPart.IsChanged || isSaveOperationContact)
            {
                if (_CurrentData != null)
                {
                    //保存联系人列表及附件
                    UCBookingOtherPart.SetContext = GetContext(_CurrentData);
                    UCBookingOtherPart.Save(_CurrentData.UpdateDate);
                    UpdateContactControlData();
                    if (Saved != null)
                    {
                        if (_businessOperationParameter == null)
                        {
                            _businessOperationParameter = new BusinessOperationParameter();
                        }
                        _businessOperationParameter.Context = GetContext(_CurrentData);
                        Saved(new object[] { _CurrentData, _businessOperationParameter,_businessOperationParameter.Context });
                    }
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "数据保存成功");
                }
            }
            return true;
        }

        private void UpdateContactControlData()
        {
            if (stxtBookingCustomer.IsContactDataChanged)
            {
                stxtBookingCustomer.ContactList = GetCurrentContactListByCustomerID(_CurrentData.BookingCustomerID, ContactType.Customer);
            }
            if (stxtAgent.IsContactDataChanged && _CurrentData.AgentID != null)
            {
                stxtAgent.ContactList = GetCurrentContactListByCustomerID(_CurrentData.AgentID.Value, ContactType.Customer);
            }
            if (stxtCustomer.IsContactDataChanged)
            {
                stxtCustomer.ContactList = GetCurrentContactListByCustomerID(_CurrentData.CustomerID, ContactType.Customer);
            }
            if (stxtAgentOfCarrier.IsContactDataChanged)
            {
                stxtAgentOfCarrier.ContactList = GetCurrentContactListByCustomerID(_CurrentData.AgentOfCarrierID, ContactType.Carrier);
            }
        }

        private bool Save(OceanBookingInfo currentData, bool isSavingAs, bool needValidate = true)
        {
            try
            {
                OperationLogID = Guid.NewGuid();
                StopwatchSaveData = Stopwatch.StartNew();
                StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now, BaseFormID,
                    "SAVE-BOOKING", string.Format("保存Booking;Booking ID[{0}]", currentData.ID));
                if (needValidate && ValidateData() == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                    return false;
                }
                barSave.Enabled = false;
                barSavingClose.Enabled = false;
                xtraTabControl1.Enabled = false;

                FilesNames = UCBookingOtherPart.CurrentDocumentName;

                SetLableMessage(LocalData.IsEnglish ? "Bookins Saveing......" : "订舱信息保存中.....", "saving");
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Bookins Saveing......" : "订舱信息保存中.....");


                if (_CurrentData.QuotedPricePartInfo == null)
                    _CurrentData.QuotedPricePartInfo = new QuotedPricePartInfo();
                _CurrentData.QuotedPricePartInfo.QuotedPriceID = stxtRecentQuotedPrice.QuotedPriceID;

                _inquirePricePartInfo = ucInquirePrice.DataSource as InquirePricePartInfo;
                _CurrentData.InquirePricePartInfo = _inquirePricePartInfo;

                BookingSaveRequest originalBooking = null;
                if (ArgumentHelper.GuidIsNullOrEmpty(currentData.ID) ||
                    ArgumentHelper.GuidIsNullOrEmpty(currentData.OceanShippingOrderID))
                {
                    originalBooking = BuildOceanBooking(currentData);
                }
                else if (_CurrentData.IsDirty || _CurrentData.InquirePricePartInfo != null)
                {
                    if (_CurrentData.BookingDate != null)
                        _CurrentData.State = OEOrderState.BookingConfirmed;

                    originalBooking = BuildOceanBooking(_CurrentData);
                }

                List<FeeSaveRequest> originalFees = orderFeeEditPart1.BuildFeeList(currentData.ID, Guid.Empty);
                List<SaveRequestBookingDelegate> originalDelegates = partDelegate.BuildSaveRequest(currentData.ID, OperationType.OceanExport);
                if (isChangeSales)
                {
                    if (_CurrentData.ID != Guid.Empty)
                    {
                        Guid[] checkIds = { _CurrentData.ID };
                        SystemService.SaveUntieLockInfo(UntieLockType.Sales, checkIds, LocalData.UserInfo.LoginID);
                    }
                }


                #region 编辑模式下 数据有改变

                bool compareflg = false;
                if (ValueCompare(currentData) && EditMode == EditMode.Edit)
                {
                    string topcn = "你确定要保存订单的修改吗？操作员将会收到订单内容变更的通知邮件.";
                    string topen =
                        "Are you sure to submit the changing booking? A mail will be sent to OP with the chaning info.";
                    //是否需要向商务员确认询价
                    if (_inquirePricePartInfo != null)
                    {
                        if (_inquirePricePartInfo.NeedConfirmation)
                        {
                            topcn = topcn + "同时询价需要商务员再次确认。";
                            topen = topen + "And a confirmation of Inquire Price will be requested again.";
                        }
                    }
                    string top = LocalData.IsEnglish ? topen : topcn;
                    string caption = LocalData.IsEnglish ? "Change the Order" : "变更订单";
                    if (ShowQuestion(top, caption) != DialogResult.Yes)
                    {
                        SetLableMessage(LocalData.IsEnglish ? "Failed to save: Save cancel orders" : "保存失败:取消保存订单", "exception");
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:取消保存订单");
                        return false;
                    }
                    compareflg = true;
                }

                #endregion

                Stopwatch StopwatchSaveBooking = Stopwatch.StartNew();
                
                Dictionary<Guid, SaveResponse> savedDict = OceanExportService.SaveOceanBookingWithTrans(originalBooking,
                    originalFees, originalDelegates);
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true,
                    string.Format("以事务方式保存订舱单、费用、PO[{0}ms]", StopwatchSaveBooking.ElapsedMilliseconds));

                isSave = true;
                _CurrentData.IsDirty = false;

                if (originalBooking != null)
                {
                    if (originalBooking.freightRateID != origContract)
                    {
                        origContract = originalBooking.freightRateID;
                        chargeContract = true;
                    }

                    _CurrentData.ID = (Guid)originalBooking.id;
                    _CurrentData.OceanShippingOrderNo = originalBooking.oceanShippingOrderNo;
                    SaveResponse.Analyze(new List<SaveRequest> { originalBooking }, savedDict, true);
                    RefreshUI(originalBooking);
                }

                if (originalFees != null)
                {
                    SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), savedDict, true);
                    orderFeeEditPart1.RefreshUI(originalFees);
                }


                #region 合约号发生改变时,更新账单

                if (chargeContract && originalBooking != null && originalBooking.IsCreateBill)
                {

                    SetLableMessage(LocalData.IsEnglish ? "Bill Saveing......" : "正在生成账单.....", "saving");
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),
                        LocalData.IsEnglish ? "Bill Saveing......" : "正在生成账单.....");

                    try
                    {

                        Stopwatch StopwatchStep = Stopwatch.StartNew();
                        SingleResult result = OceanExportService.CreateBill(originalBooking.id.Value,
                            LocalData.UserInfo.LoginID);
                        if (result != null)
                        {
                            int s = result.GetValue<Byte>("State");
                            string title = string.Empty;

                            if (s == 1)
                            {
                                title = NativeLanguageService.GetText(this, "CreateBills");
                                _StrMesage = NativeLanguageService.GetText(this, "CreateBills");
                                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true,
                                    string.Empty, string.Format(title + "[{0}]", StopwatchStep.ElapsedMilliseconds));
                                chargeContract = false;
                                ShowMessage(_StrMesage);
                                //MessageBoxService.ShowInfo( NativeLanguageService.GetText(this, "CreateBills"));
                            }
                            else if (s == 2)
                            {
                                string message = result.GetValue<string>("Message");
                                title = (LocalData.IsEnglish ? "Generate the bill Error:" : "生成账单失败：");
                                _StrMesage = title + message;
                                ShowMessage(_StrMesage);
                                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true,
                                    string.Empty, string.Format(title + "[{0}]", StopwatchStep.ElapsedMilliseconds));

                                //MessageBoxService.ShowInfo(title + message);
                            }

                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), title);
                        }

                    }
                    catch (Exception ex)
                    {
                        SetLableMessage(LocalData.IsEnglish ? "Generate the bill Error" : "生成账单失败", "exception");
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Empty,
                            string.Format("生成账单失败 SessionId[{0}]", LocalData.SessionId));
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "系统根据合约生成账单的时,遇到错误：" + ex.Message);
                    }

                }
                chargeContract = false;

                #endregion

                if (isSavingAs)
                {
                }
                else
                {
                    AfterSave();
                }
                string saveState = string.Format("{0}保存成功", ((EditMode == EditMode.New || EditMode == EditMode.Copy) ? ("[" + currentData.ID + "]") : ""));
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty,
                    string.Empty, saveState);
                if (compareflg)
                {
                    //发送邮件给客服或发送给订舱员

                    ClientOceanExportService.MainBookIngChangedOceanBookingInfo(currentData.ID, _oldstring.ToString(),
                        _updatestring.ToString());
                    saveState += "已发送邮件给客服||订舱员";
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty,
                        string.Empty, saveState);
                    //这里判断是否需要发送给商务员
                    if (_inquirePricePartInfo != null)
                    {
                        if (_inquirePricePartInfo.NeedConfirmation == true &&
                            !string.IsNullOrEmpty(_inquirePricePartInfo.InquirePriceNO))
                        {
                            ClientOceanExportService.MainBookIngChangedInquir(currentData.ID,
                                _inquirePricePartInfo.InquirePriceNO);
                            saveState += "已发送邮件给商务员";
                            StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false,
                                string.Empty, string.Empty, saveState);
                        }
                    }

                }
                ClientOceanExportService.MailCenterRefresh(true);
                return true;
            }
            catch (Exception ex)
            {
                SetLableMessage("", "exception");
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty,
                    string.Empty, string.Format("保存失败 SessionId[{0}]", LocalData.SessionId));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message + "Save_Method");
                return false;
            }
            finally
            {
                barSave.Enabled = true;
                barSavingClose.Enabled = true;
                xtraTabControl1.Enabled = true;
            }
        }

        private void AfterSave()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(AfterSave));
            }
            else
            {
                _CurrentData.CancelEdit();
                _CurrentData.BeginEdit();

                TriggerSavedEvent();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                SetTitle();
                if (EditMode == EditMode.New)
                {
                    EditMode = EditMode.Edit;
                }
            }
        }

        void SetTitle()
        {
            if (_CurrentData.ID == Guid.Empty)
            {
                Title = LocalData.IsEnglish ? "Add Booking" : "新增订舱";
            }
            else
            {
                string titleNo = string.Empty;

                if (_CurrentData.No.Length > 4)
                {
                    titleNo = _CurrentData.No.Substring(_CurrentData.No.Length - 4, 4);
                }
                else
                {
                    titleNo = _CurrentData.No;
                }

                Title = LocalData.IsEnglish ? "Edit Booking " + titleNo : "编辑订舱：" + titleNo;
            }
        }

        void TriggerSavedEvent()
        {
            if (Saved != null)
            {
                _CurrentData.SalesName = _CurrentData.SalesID.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbSales.EditText;
                _CurrentData.CarrierName = _CurrentData.CarrierID == Guid.Empty ?
                    string.Empty : mcmbCarrier.EditText;
                _CurrentData.OEOperationTypeDescription = EnumHelper.GetDescription<FCMOperationType>(_CurrentData.OEOperationType, LocalData.IsEnglish);
                _CurrentData.FilerName = _CurrentData.FilerId.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbFiler.Text;
                _CurrentData.BookingByName = _CurrentData.BookingByID.ToGuid() == Guid.Empty ? string.Empty : mcmbBookingBy.Text;

                _CurrentData.BookingerName = _CurrentData.BookingerID.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbBookinger.Text;
                _CurrentData.OverSeasFilerName = _CurrentData.OverSeasFilerID.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbOverseasFiler.Text;
                _CurrentData.VesselVoyage = stxtPreVoyage.Text + (stxtPreVoyage.Text.Trim().Length > 0 ? ";" : "") + stxtVoyage.Text;

                if (!UCBookingOtherPart.IsChanged && !isSaveOperationContact)
                {
                    if (_businessOperationParameter == null)
                    {
                        _businessOperationParameter = new BusinessOperationParameter();
                    }
                    _businessOperationParameter.Context = GetContext(_CurrentData);


                    Saved(new object[] { _CurrentData,_businessOperationParameter, _businessOperationParameter.Context });
                }

                _CurrentData.IsDirty = false;
            }
        }

        /// <summary>
        /// 客户参考号暂时传空值
        /// </summary>
        /// <param name="currentData"></param>
        private BookingSaveRequest BuildOceanBooking(OceanBookingInfo currentData)
        {
            EndEdit();

            if (currentData.IsDirty == true || currentData.IsNew)
            {
                BookingSaveRequest saveRequest = new BookingSaveRequest();
                saveRequest.id = currentData.ID;
                saveRequest.No = currentData.No;
                saveRequest.customerRefNo = string.Empty;
                saveRequest.customerID = currentData.CustomerID;
                saveRequest.tradeTermID = currentData.TradeTermID;
                saveRequest.oeOperationType = currentData.OEOperationType;
                saveRequest.companyID = currentData.CompanyID;
                saveRequest.bookingerID = currentData.BookingerID;
                saveRequest.filerID = currentData.FilerId;
                saveRequest.bookingById = currentData.BookingByID;
                saveRequest.overSeasFilerId = currentData.OverSeasFilerID;
                saveRequest.salesDepartmentID = currentData.SalesDepartmentID;
                saveRequest.salesID = currentData.SalesID;
                saveRequest.salesTypeID = currentData.SalesTypeID;
                saveRequest.bookingMode = currentData.BookingMode;
                saveRequest.bookingDate = currentData.BookingDate;
                saveRequest.bookingCustomerID = currentData.BookingCustomerID;
                saveRequest.bookingCustomerDescription = currentData.BookingCustomerDescription;
                saveRequest.shipperID = currentData.ShipperID;
                saveRequest.shipperDescription = currentData.ShipperDescription;
                saveRequest.consigneeID = currentData.ConsigneeID;
                saveRequest.consigneeDescription = currentData.ConsigneeDescription;
                saveRequest.placeOfReceiptID = currentData.PlaceOfReceiptID;
                saveRequest.PlaceOfReceiptAddress = currentData.PlaceOfReceiptAddress;
                saveRequest.polID = currentData.POLID;
                saveRequest.podID = currentData.PODID;
                saveRequest.placeOfDeliveryID = currentData.PlaceOfDeliveryID;
                saveRequest.PlaceOfDeliveryAddress = currentData.PlaceOfDeliveryAddress;
                saveRequest.agentID = currentData.AgentID;
                saveRequest.agentDescription = currentData.AgentDescription;
                saveRequest.carrierID = currentData.CarrierID;
                saveRequest.agentOfCarrierID = currentData.AgentOfCarrierID;
                saveRequest.isContract = currentData.IsContract;
                saveRequest.freightRateID = currentData.ContractID;
                saveRequest.oceanShippingOrderID = currentData.OceanShippingOrderID;
                saveRequest.oceanShippingOrderNo = currentData.OceanShippingOrderNo;
                saveRequest.soDate = currentData.SODate;
                saveRequest.estimatedDeliveryDate = currentData.EstimatedDeliveryDate;
                saveRequest.actualDeliveryDate = currentData.DeliveryDate;
                saveRequest.expectedShipDate = currentData.ExpectedShipDate;
                saveRequest.expectedArriveDate = currentData.ExpectedArriveDate;
                saveRequest.closingDate = currentData.ClosingDate;
                saveRequest.paymentTermID = currentData.PaymentTermID;
                saveRequest.transportClauseID = currentData.TransportClauseID;
                saveRequest.shippingLineID = currentData.ShippingLineID;
                saveRequest.preVoyageID = currentData.PreVoyageID;
                saveRequest.voyageID = currentData.VoyageID;
                saveRequest.commodity = currentData.Commodity;
                saveRequest.quantity = currentData.Quantity;
                saveRequest.quantityUnitID = currentData.QuantityUnitID;
                saveRequest.weight = currentData.Weight;
                saveRequest.weightUnitID = currentData.WeightUnitID;
                saveRequest.measurement = currentData.Measurement;
                saveRequest.measurementUnitID = currentData.MeasurementUnitID;
                saveRequest.cargoDescription = currentData.CargoDescription;
                saveRequest.containerDescription = currentData.ContainerDescription;
                saveRequest.mblPaymentTermID = currentData.MBLPaymentTermID;
                saveRequest.hblPaymentTermID = currentData.HBLPaymentTermID;
                saveRequest.isTruck = currentData.IsTruck;
                saveRequest.isCustoms = currentData.IsCustoms;
                saveRequest.isCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.isQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.isWarehouse = currentData.IsWareHouse;
                saveRequest.isOnlyMBL = currentData.IsOnlyMBL;
                saveRequest.mblReleaseType = currentData.MBLReleaseType;
                saveRequest.hblReleaseType = currentData.HBLReleaseType;
                saveRequest.mblRequirements = currentData.MBLRequirements;
                saveRequest.hblRequirements = currentData.HBLRequirements;
                saveRequest.remark = currentData.Remark;
                saveRequest.finalDestinationID = currentData.FinalDestinationID;
                saveRequest.returnLocationID = currentData.ReturnLocationID;
                saveRequest.warehouseID = currentData.WarehouseID;
                saveRequest.closingWarehousedate = currentData.ClosingWarehousedate;
                saveRequest.oceanShippingOrderUpdateDate = currentData.OceanShippingOrderUpdateDate;
                saveRequest.oceanOrderUpdateDate = currentData.UpdateDate;
                saveRequest.ClosingDate = currentData.ClosingDate;
                saveRequest.CYClosingDate = currentData.CYClosingDate;
                saveRequest.DOCClosingDate = currentData.DOCClosingDate;
                saveRequest.VGMCutOffDate = currentData.VGMCutOffDate;
                saveRequest.ETA = currentData.ETA;
                saveRequest.ETD = currentData.ETD;
                saveRequest.GateInDate = currentData.GateInDate;
                saveRequest.PreETD = currentData.PORETD;
                saveRequest.pickupEarliestDate = currentData.PickupEarliestDate;
                saveRequest.IsThirdPlacePay = currentData.IsThirdPlacePay;
                saveRequest.CollectbyAgentID = currentData.CollectbyAgentID;
                saveRequest.CollectbyAgentName = currentData.CollectbyAgentName;

                saveRequest.bookingPartyID = currentData.BookingPartyID;
                saveRequest.bookingShipperID = currentData.BookingShipperID;
                saveRequest.bookingShipperdescription = currentData.BookingShipperdescription;
                saveRequest.bookingConsigneeID = currentData.BookingConsigneeID;
                saveRequest.bookingConsigneedescription = currentData.BookingConsigneedescription;
                saveRequest.bookingNotifyPartyID = currentData.BookingNotifyPartyID;
                saveRequest.bookingNotifyPartydescription = currentData.BookingNotifyPartydescription;
                saveRequest.marks = currentData.Marks;
                saveRequest.pickupRequirement = currentData.PickupRequirement;
                saveRequest.bookingExplanation = currentData.BookingExplanation;
                saveRequest.isInsurance = currentData.IsInsurance;
                saveRequest.isFumigation = currentData.IsFumigation;
                saveRequest.isWoodPacking = currentData.IsWoodPacking;
                saveRequest.isCarrierSendAMS = currentData.IsCarrierSendAMS;
                saveRequest.mBLTransportClauseID = currentData.MBLTransportClauseID;
                saveRequest.AMSClosingDate = currentData.AMSClosingDate;
                saveRequest.IsThirdPlacePayOrder = currentData.IsThirdPlacePayOrder;
                saveRequest.CollectbyAgentOrderID = currentData.CollectbyAgentOrderID;
                saveRequest.NotifyPartyID = currentData.NotifyPartyID;
                saveRequest.ScacCode = currentData.ScacCode;
                saveRequest.RailCutOff = currentData.RailCutOff;
                saveRequest.BookingRefNo = currentData.BookingRefNo;
                saveRequest.OkToSub = currentData.OkToSub;
                saveRequest.CusClearanceNo = currentData.CusClearanceNo;

                saveRequest.InquirePriceOceanId = currentData.InquirePricePartInfo == null ? Guid.Empty : currentData.InquirePricePartInfo.InquirePriceID;
                saveRequest.InquirePriceOceanConfirmedBy = currentData.InquirePricePartInfo == null ? Guid.Empty : currentData.InquirePricePartInfo.ConfirmedByID;

                saveRequest.FreightIncludedIds = orderFeeEditPart1.SelectChargeCodeIds;

                if (!saveRequest.isContract)
                {
                    saveRequest.freightRateID = currentData.ContractID = null;
                }

                saveRequest.QuotedPriceID = currentData.QuotedPricePartInfo.QuotedPriceID;

                if (currentData.IsNew)
                {
                    ///新增的订舱单，不需要产生账单
                    saveRequest.IsCreateBill = false;
                }
                else
                {
                    saveRequest.IsCreateBill = currentData.IsCreateBill;
                }
                saveRequest.AddInvolvedObject(currentData);
                saveRequest.HSCode = currentData.HSCode;
                saveRequest.IsSyncCSP = CacheDelegate.Count > 0;
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                return saveRequest;
            }
            else
            {
                return null;
            }
        }

        void RefreshUI(BookingSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;

            OceanBookingInfo currentData = saveRequest.UnBoxInvolvedObject<OceanBookingInfo>()[0];

            currentData.ID = result.GetValue<Guid>("ID");
            currentData.No = result.GetValue<string>("No");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.OceanShippingOrderUpdateDate = result.GetValue<DateTime?>("OceanShippingOrderUpdateDate");
            currentData.OceanShippingOrderID = result.GetValue<Guid>("OceanShippingOrderID");
            currentData.State = (OEOrderState)result.GetValue<byte>("State");


            currentData.IsDirty = false;
            if (currentData.ShipperDescription != null)
            {
                currentData.ShipperDescription.IsDirty = false;
            }
            if (currentData.ConsigneeDescription != null)
            {
                currentData.ConsigneeDescription.IsDirty = false;
            }
            if (currentData.BookingCustomerDescription != null)
            {
                currentData.BookingCustomerDescription.IsDirty = false;
            }
            RefreshBarEnabled();

            //不工作，而且以前状态丢失，可能会有问题
            //this.Workitem.ID = OEBookingCommandConstants.Command_EditData + currentData.ID.ToString();
        }

        #endregion

        #region 另存为

        private void barSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SaveAs())
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save as a new booking order successfully. Ref. NO. is " + _CurrentData.No + "." : "已成功另存为一票新订单，业务号为" + _CurrentData.No + "。");
                if (Saved != null)
                {
                    Saved(new object[] { _CurrentData });
                }
            }
        }

        bool SaveAs()
        {
            if (ValidateData() == false)
            {
                return false;
            }

            if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的订舱单?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return false;
            }

            OceanBookingInfo orderInfo = OEUtility.Clone<OceanBookingInfo>(_CurrentData);
            orderInfo.ID = Guid.Empty;
            orderInfo.No = string.Empty;

            if (string.IsNullOrEmpty(orderInfo.OceanShippingOrderNo))
            {
                orderInfo.State = OEOrderState.NewOrder;
            }
            else
            {
                orderInfo.State = OEOrderState.BookingConfirmed;
            }

            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.Now;
            orderInfo.BookingDate = DateTime.Now;
            orderInfo.UpdateDate = null;

            if (orderInfo.OEOperationType == FCMOperationType.LCL)
            {
            }
            else
            {
                orderInfo.OceanShippingOrderID = Guid.Empty;
                orderInfo.OceanShippingOrderNo = string.Empty;
            }

            orderInfo.IsDirty = true;
            bookingPOEditPart1.InitData(_CurrentData.ID);

            _CurrentData = orderInfo;

            if (Save(orderInfo, true))
            {
                RefreshData(orderInfo.ID);

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 审核并保存
        private void AfterAuditRemarkSaved(object[] parameters)
        {
            try
            {
                SingleResult result = OceanExportService.ChangeOceanOrderStateWithTargetState(_CurrentData.ID, Guid.Empty
                    , OEOrderState.Checked, parameters[0].ToString(), LocalData.UserInfo.LoginID, _CurrentData.UpdateDate);

                Logger.Log.Info(result);

                _CurrentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentData.State = (OEOrderState)result.GetValue<byte>("State");

                TriggerSavedEvent();

                RefreshBarEnabled();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Audit successfully!" : "审核订单成功！");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 审核并保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAuditAndSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Save(_CurrentData, false))
            {
                return;
            }

            if (_CurrentData.State != OEOrderState.NewOrder)
            {
                return;
            }
            string labRemark = LocalData.IsEnglish ? "Audit memo" : "审核意见";
            bool remarkRequired = true;
            string title = LocalData.IsEnglish ? "Audit Order" : "审核订单";
            ClientOceanExportService.ShowRemarkEditForm(labRemark, remarkRequired, title, AfterAuditRemarkSaved);
        }

        #endregion

        #region 打印

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }
            ClientOceanExportService.PrintBookingConfirm(_CurrentData.ID);
        }

        /// <summary>
        /// 利润表打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barProfit_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (!SaveData())
            //{
            //    return;
            //}
            //OceanExportPrintHelper.PrintOEBookingProfit(_oceanBookingInfo);
        }

        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            ClientOceanExportService.PrintOrder(_CurrentData.ID, _CurrentData.CompanyID);
        }

        private void barPrintInWarehouse_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            throw new NotImplementedException(LocalData.IsEnglish ? "To be defined on next version." : "本版本暂不提供入仓通知单打印功能。");
        }

        #endregion

        #region 打回

        private void barReject_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentData.State == OEOrderState.Rejected) return;
            string labRemark = LocalData.IsEnglish ? "Reject reason" : "打回原因";
            bool remarkRequired = true;

            string title = LocalData.IsEnglish ? "Reject Order" : "打回订单";
            ClientOceanExportService.ShowRemarkEditForm(labRemark, remarkRequired, title, AfterRejectRemarkSaved);
        }
        private void AfterRejectRemarkSaved(object[] parameters)
        {
            try
            {
                bool isDirty = _CurrentData.IsDirty;
                SingleResult result = OceanExportService.ChangeOceanOrderStateWithTargetState(_CurrentData.ID, Guid.Empty,
                    OEOrderState.Rejected, parameters[0].ToString(), LocalData.UserInfo.LoginID,
                    _CurrentData.UpdateDate);

                _CurrentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentData.State = (OEOrderState)result.GetValue<byte>("State");

                TriggerSavedEvent();

                RefreshBarEnabled();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        #endregion

        #region EDI格式
        /// <summary>
        /// EDI格式
        /// </summary>
        private void barE_Booking_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #endregion

        #region 刷新

        /// <summary>
        /// 数据刷新到初始或保存后
        /// </summary>
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DialogResult dialogResult = DevExpress.XtraEditors.MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Sure Refresh Data?" : "是否刷新数据?",
            //                                   LocalData.IsEnglish ? "Tip" : "提示",
            //                                   MessageBoxButtons.YesNo,
            //                                   MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            //{
            try
            {
                RefreshData(_CurrentData.ID);
                chargeContract = false;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh failed." + CommonHelper.BuildExceptionString(ex) : "刷新失败." + CommonHelper.BuildExceptionString(ex));
            }
            //}
        }

        void RefreshData(Guid orderId)
        {
            GetData(_CurrentData.ID);
            ShowOrder();
            RunAtOnce();
            ResetDescription();
            SetTitle();
        }

        #endregion

        /// <summary>
        /// 申请代理
        /// </summary>
        private void barApplyAgent_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.ReplyAgent(_CurrentData.ID, null, null);
        }

        /// <summary>
        /// 派车单
        /// </summary>
        private void barTruck_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.OpenTruckOrder(_CurrentData.ID, _CurrentData.No, null, null, null);
        }

        /// <summary>
        /// View SO History 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButViewSOHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                BookingListQueryCriteria bookingListQueryCriteria = new BookingListQueryCriteria();
                bookingListQueryCriteria.Companys = mcmbCarrier.EditText.ToString();
                bookingListQueryCriteria.Customer = stxtCustomer.EditValue.ToString();
                bookingListQueryCriteria.POD = stxtPOD.EditValue.ToString();
                bookingListQueryCriteria.POL = stxtPOL.EditValue.ToString();
                ClientOceanExportService.OpenBookingList(bookingListQueryCriteria);
            }
        }

        /// <summary>
        /// Inquire Rates 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barInquireRates_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientInquireRateService.InquireOceanRate();
        }


        private void ShowErrorInfo(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "Show Error" + result);
            }
        }

        #region 发送邮件
        /// <summary>
        /// Ask Profit Promise(CHS) 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAskProfitPromiseCHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = ClientOceanExportService.MailSalesManAskForProfitPromise(false, _CurrentData);
            ShowErrorInfo(result);
        }
        /// <summary>
        /// Ask Profit Promise(ENG) 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAskProfitPromiseENG_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = ClientOceanExportService.MailSalesManAskForProfitPromise(true, _CurrentData);
            ShowErrorInfo(result);
        }

        /// <summary>
        /// Confirm Debit Fees (CHS)按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barConfirmDebitFeesCHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConfirmDebitFees(false);
        }

        /// <summary>
        /// Confirm Debit Fees (ENG)按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barConfirmDebitFeesENG_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConfirmDebitFees(true);
        }

        /// <summary>
        /// Ask Customer For SI (CHS) 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAskCustomerForSICHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailCustomerAskForSi(false, _CurrentData);
        }

        /// <summary>
        /// Ask Customer For SI (ENG) 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAskCustomerForSIENG_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailCustomerAskForSi(true, _CurrentData);
        }

        /// <summary>
        ///  Mail SO Copy To Customer (CHS) 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barMailSOCopyToCustomerCHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = ClientOceanExportService.MailSoCopyToCustomer(false, _CurrentData);
            ShowErrorInfo(result);
        }

        /// <summary>
        ///  Mail SO Copy To Customer (ENG) 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barMailSOCopyToCustomerENG_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = ClientOceanExportService.MailSoCopyToCustomer(true, _CurrentData);
            ShowErrorInfo(result);
        }
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailSoConfirmationToCustomer(_CurrentData.ID, false);
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailSoConfirmationToCustomer(_CurrentData.ID, true);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = ClientOceanExportService.MailSoConfirmationToAgent(_CurrentData.ID);
            ShowErrorInfo(result);
        }
        #endregion

        /// <summary>
        /// Order Customs 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barOrderCustoms_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ClientOceanExportService.IsShipmentHasContainer(_CurrentData.ID) == true)
            {
                ClientOceanExportService.OpenCustomsOrder(_CurrentData.ID, null, null);
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),
                     LocalData.IsEnglish ?
                     "Commissioned by business case number, can't do customs declaration."
                     : "业务无箱号，无法进行报关委托.");
            }
        }


        /// <summary>
        /// 审核并保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSopv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Save(_CurrentData, false))
            {
                if (OceanExportService.SetUpdateOceanTrackings(_CurrentData.ID))
                {
                    if (Saved != null)
                    {
                        if (_businessOperationParameter == null)
                        {
                            _businessOperationParameter = new BusinessOperationParameter();
                        }
                        _businessOperationParameter.Context = GetContext(_CurrentData);

                        Saved(new object[] { _CurrentData,_businessOperationParameter, _businessOperationParameter.Context });
                    }
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save success" : "操作成功");
                }
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? " 保存失败" : "操作失败");
            }
        }
        #endregion

        #region 清理资源和避免多余操作

        void BookingBaseEditPart_Disposed(object sender, EventArgs e)
        {
            Saved = null;
            _CacheAgentCustomersList = null;
            _businessOperationParameter = null;
            _CacheCountryList = null;
            _CurrentData = null;
            _CacheWeightUnits = null;
            OperationContext = null;
            stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
            stxtAgent.OnOk -= stxtAgent_OnOk;
            stxtBookingCustomer.OnOk -= stxtBookingCustomer_OnOk;
            stxtBookingCustomer.TextChanged -= stxtBookingCustomer_TextChanged;
            stxtConsignee.OnOk -= stxtConsignee_OnOk;

            stxtShipper.OnOk -= stxtShipper_OnOk;
            stxtNotifyParty.OnOk -= stxtNotifyParty_OnOk;
            cmbOrderNo.Enter -= cmbOrderNo_Enter;
            cmbOrderNo.LostFocus -= cmbOrderNo_SelectedIndexChanged;
            cmbOrderNo.TextChanged -= cmbOrderNo_TextChanged;
            cmbSalesType.SelectedIndexChanged -= cmbSalesType_SelectedIndexChanged;
            cmbTradeTerm.SelectedIndexChanged -= cmbTradeTerm_SelectedIndexChanged;
            cmbType.SelectedIndexChanged -= cmbType_SelectedIndexChanged;

            txtContractNo.Click -= txtContractNo_Click;
            xtraTabControl1.SelectedPageChanged -= xtraTabControl1_SelectedPageChanged;
            trsSalesDep.Enter -= trsSalesDep_Enter;
            chkHasContract.CheckedChanged -= chkHasContract_CheckedChanged;
            chkIsOnlyMBL.CheckedChanged -= chkIsOnlyMBL_CheckedChanged;
            chkHasContract.Click -= chkHasContract_Click;

            barSavingClose.ItemClick -= barSavingClose_ItemClick;
            barCancel.ItemClick -= barCancel_ItemClick;
            barlabMessage.ItemClick -= barlabMessage_ItemClick;
            
            TimerSaveData.Tick -= RefreshTime_Tick;

            stxtRecentQuotedPrice.SelectChanged -= stxtRecentQuotedPrice_SelectChanged;
            _inquirePricePartInfo = null;
            if (bsBookingInfo != null)
            {
                bsBookingInfo.DataSource = null;
                bsBookingInfo = null;
            }
            if (PartWorkItem != null)
            {
                if (bookingOtherPart != null)
                {
                    PartWorkItem.Items.Remove(bookingOtherPart);
                }
                if (bookingPOEditPart1 != null)
                {
                    PartWorkItem.Items.Remove(bookingPOEditPart1);
                }
            }
            if (bookingOtherPart != null)
            {
                bookingOtherPart = null;
            }
            if (bookingPOEditPart1 != null)
            {
                bookingPOEditPart1 = null;
            }

            if (shipperBridge != null)
            {
                shipperBridge.Dispose();
                shipperBridge = null;
            }
            if (consigneeBridge != null)
            {
                consigneeBridge.Dispose();
                consigneeBridge = null;
            }

            if (notifyPartyBridge != null)
            {
                notifyPartyBridge.Dispose();
                notifyPartyBridge = null;
            }

            if (bookingShipperBridge != null)
            {
                bookingShipperBridge.Dispose();
                bookingShipperBridge = null;
            }

            if (bookingConsigneeBridge != null)
            {
                bookingConsigneeBridge.Dispose();
                bookingConsigneeBridge = null;
            }

            if (bookingNotifyPartyBridge != null)
            {
                bookingNotifyPartyBridge.Dispose();
                bookingNotifyPartyBridge = null;
            }

            if (bookingCustomerPartyBridge != null)
            {
                bookingCustomerPartyBridge.Dispose();
                bookingCustomerPartyBridge = null;
            }
            if (pfbPlaceOfReceipt != null)
            {
                pfbPlaceOfReceipt.Dispose();
                pfbPlaceOfReceipt = null;
            }

            SmartPartClosing -= BookingBaseEditPart_SmartPartClosing;
            if (PartWorkItem != null)
            {
                PartWorkItem.Items.Remove(this);
                PartWorkItem = null;
            }
        }

        /// <summary>
        /// 防闪烁
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0014) return;// 禁掉清除背景消息

            base.WndProc(ref m);
        }

        #endregion

        #region 关闭

        void BookingBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (_CurrentData.IsDirty && isSave == false)
            {
                DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save(_CurrentData, false))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        #endregion
        #endregion

        #region  方法

        #region Init
        /// <summary>
        /// 设置中文字符
        /// </summary>
        private void SetCnText()
        {
            if (LocalData.IsEnglish) return;
            lblRemark.Text = "备注";
            labQuotedPriceNo.Text = "报价单号";
            labBookingCustomer.Text = "订舱客户";
            labBookingDate.Text = "委托日期";
            labBookingMode.Text = "委托方式";

            labCargoType.Text = "货物类型";
            labCarrier.Text = "船公司";
            labClosingDate.Text = "截关日";
            labMarks.Text = "唛头";
            labCompany.Text = "操作口岸";
            labConsignee.Text = "收货人";
            labCustomer.Text = "客户";
            labEstimatedDeliveryDate.Text = "估计交货";
            labExpectedArriveDate.Text = "期望到达";
            labExpectedShipDate.Text = "期望出运";

            labHBLPaymentTerm.Text = "付款方式";
            labMBLPaymentTerm.Text = "付款方式";
            labHBLReleaseType.Text = "放单类型";
            labMBLReleaseType.Text = "放单类型";

            labMeasurement.Text = "体积";
            labNo.Text = "业务号";
            labPaymentTerm.Text = "付款方式";
            labPlaceOfDelivery.Text = "交货地";
            labPOD.Text = "卸货港";
            labQuantity.Text = "数量";
            labSales.Text = "揽货人";
            labSalesDepartment.Text = "揽货部门";
            labSalesType.Text = "揽货类型";
            labShipper.Text = "发货人";
            labTradeTerm.Text = "贸易条款";
            labTransportClause.Text = "运输条款";
            labReturnLocation.Text = "还柜地点";
            labType.Text = "业务类型";
            labWeight.Text = "重量";
            labDeliveryDate.Text = "交货日";

            chkIsTruck.Text = "拖车";
            chkIsCustoms.Text = "报关";
            chkIsInsurance.Text = "保险";
            chkIsFumigate.Text = "熏蒸";
            chkIsWarehouse.Text = "仓储";
            chkIsOnlyMBL.Text = "只出MBL";
            chkHasContract.Text = "合约";

            labAgent.Text = "代理";
            labAgentOfCarrier.Text = "承运人";
            labOverseasFiler.Text = "海外部客服";
            labBookinger.Text = "订舱";
            labFiler.Text = "文件";
            labState.Text = "状态";
            labOrderNo.Text = "订舱号";
            labSODate.Text = "确认日期";
            labVoyage.Text = "大船";
            labPreVoyage.Text = "驳船";
            labPlaceOfReceipt.Text = "收货地";
            labFinalDestination.Text = "最终目的地";
            labBookingBy.Text = "订舱员";

            labETD.Text = "离港日";
            labETA.Text = "到港日";
            labPOLETD.Text = "离港日";
            labVGMCutOff.Text = "截VGM日";

            navBarBase.Caption = "基本信息";
            navBarDelegate.Caption = "委托信息";
            navOther.Caption = "联系人信息";
            navBarFee.Caption = "费用信息";

            groupLocalService.Text = "本地服务";

            labShippingLine.Text = "航线";
            labCYClosingDate.Text = "截柜日";
            labDOCClosingDate.Text = "截文件日";
            labGateInDate.Text = "进港日";
            labCloseWarehouse.Text = "截仓日";
            labWarehouse.Text = "仓库";
            labPOL2.Text = "装货港";
            tabPageBase.Text = "基础";

            barRefresh.Caption = "刷新(&R)";
            barSave.Caption = "保存(&S)";
            barSavingClose.Caption = "保存并关闭";
            barCancel.Caption = "取消";
            barSaveAs.Caption = "另存为(&A)";
            barAuditAndSave.Caption = "审核并保存";
            //this.barCustoms.Caption = "报关委托";
            barSubPrint.Caption = "打印";
            barPrintOrder.Caption = "业务联单";
            barPrintBookingConfirm.Caption = "订舱确认书";
            barPrintInWarehouse.Caption = "进仓通知书";
            barClose.Caption = "关闭(&C)";

            barReject.Caption = "打回(&J)";
            barTruck.Caption = "派车";
            barApplyAgent.Caption = "申请代理";
            barE_Booking.Caption = "电子订舱(&E)";

            //新加的按钮
            barMailToCustomer.Caption = "Email";
            barAskCustomerForSICHS.Caption = "提醒客户补料(中文版)";
            barAskCustomerForSIENG.Caption = "提醒客户补料(英文版)";
            barMailSOCopyToCustomerCHS.Caption = "通知客户ADJ SO Copy(中文版)";
            barMailSOCopyToCustomerENG.Caption = "通知客户ADJ SO Copy(英文版)";
            barConfirmDebitFeesCHS.Caption = "确认费用(中文版)";
            barConfirmDebitFeesENG.Caption = "确认费用(英文版)";
            barAskProfitPromiseCHS.Caption = "利润承诺(中文版)";
            barAskProfitPromiseENG.Caption = "利润承诺(英文版)";
            //barLocalServices.Caption = "服  务";
            barOrderCustoms2.Caption = "报关委托";
            barInquireRates2.Caption = "询   价";
            barButViewSOHistory2.Caption = "历史业务";
            barButViewSOHistory2.Hint = LocalData.IsEnglish ? "According to the Customer, POD, POL, the ship company for the filter conditions." : "根据客户,卸货港,装货港,船公司为过滤条件.";
            barSopv.Caption = "审核并保存";

            barButtonItem3.Caption = "通知客户订舱确认书(中文版)";
            barButtonItem4.Caption = "通知客户订舱确认书(英文版)";
            barButtonItem5.Caption = "通知代理订舱确认书";
        }

        /// <summary>
        /// 有些实体性的属性如果为空，将引起界面异常
        /// 在这里统一对其初始化
        /// </summary>
        private void InitData()
        {
            stxtRecentQuotedPrice.RootWorkItem = PartWorkItem;
            InitCargoObject();

            if (_CurrentData.ShipperDescription == null)
            {
                _CurrentData.ShipperDescription = new CustomerDescription();
            }

            if (_CurrentData.ConsigneeDescription == null)
            {
                _CurrentData.ConsigneeDescription = new CustomerDescription();
            }

            if (_CurrentData.AgentDescription == null)
            {
                _CurrentData.AgentDescription = new CustomerDescription();
            }

            if (_CurrentData.BookingCustomerDescription == null)
            {
                _CurrentData.BookingCustomerDescription = new CustomerDescription();
            }

            if (_CurrentData.CargoDescription == null)
            {
                //_oceanBookingInfo.CargoDescription = new CargoDescription();
            }

            if (_CurrentData.NotifyPartydescription == null)
            {
                _CurrentData.NotifyPartydescription = new CustomerDescription();
            }

            if (_CurrentData.BookingShipperdescription == null)
            {
                _CurrentData.BookingShipperdescription = new CustomerDescription();
            }

            if (_CurrentData.BookingConsigneedescription == null)
            {
                _CurrentData.BookingConsigneedescription = new CustomerDescription();
            }

            if (_CurrentData.BookingNotifyPartydescription == null)
            {
                _CurrentData.BookingNotifyPartydescription = new CustomerDescription();
            }
        }

        /// <summary>
        /// 初始化货物对象
        /// </summary>
        private void InitCargoObject()
        {
            if (_CurrentData.CargoType.HasValue
                && _CurrentData.CargoDescription != null
                && _CurrentData.CargoDescription.Cargo != null)
            {
                if (_CurrentData.CargoDescription.Cargo is DangerousCargo)
                    cmbCargoType.EditValue = CargoType.Dangerous;
                else if (_CurrentData.CargoDescription.Cargo is AwkwardCargo)
                    cmbCargoType.EditValue = CargoType.Awkward;
                else if (_CurrentData.CargoDescription.Cargo is ReeferCargo)
                    cmbCargoType.EditValue = CargoType.Reefer;
                else if (_CurrentData.CargoDescription.Cargo is DryCargo)
                    cmbCargoType.EditValue = CargoType.Dry;
            }
        }

        /// <summary>
        /// 根据订舱单信息显示下拉式控件及其它一些控件的值
        /// </summary>
        private void InitControls()
        {
            //业务类型
            cmbType.ShowSelectedValue(_CurrentData.OEOperationType,
                EnumHelper.GetDescription<FCMOperationType>(_CurrentData.OEOperationType, LocalData.IsEnglish, true));
            //委托方式
            cmbBookingMode.ShowSelectedValue(_CurrentData.BookingMode,
                EnumHelper.GetDescription<FCMBookingMode>(_CurrentData.BookingMode, LocalData.IsEnglish));
            //操作口岸
            cmbCompany.ShowSelectedValue(_CurrentData.CompanyID, _CurrentData.CompanyName);
            //贸易条款
            cmbTradeTerm.ShowSelectedValue(_CurrentData.TradeTermID, _CurrentData.TradeTermName);
            //包装
            cmbQuantityUnit.ShowSelectedValue(_CurrentData.QuantityUnitID, _CurrentData.QuantityUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.QuantityUnitID)
                 && cmbMeasurementUnit.EditValue != null && cmbQuantityUnit.EditValue != DBNull.Value)
            {
                _CurrentData.QuantityUnitID = (Guid)cmbQuantityUnit.EditValue;
            }
            //重量
            cmbWeightUnit.ShowSelectedValue(_CurrentData.WeightUnitID, _CurrentData.WeightUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.WeightUnitID)
                 && cmbMeasurementUnit.EditValue != null && cmbWeightUnit.EditValue != DBNull.Value)
            {
                _CurrentData.WeightUnitID = (Guid)cmbWeightUnit.EditValue;
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(_CurrentData.MeasurementUnitID, _CurrentData.MeasurementUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null && cmbMeasurementUnit.EditValue != DBNull.Value)
            {
                _CurrentData.MeasurementUnitID = (Guid)cmbMeasurementUnit.EditValue;
            }
            mcmbSales.ShowSelectedValue(_CurrentData.SalesID, _CurrentData.SalesName);
            //揽货类型
            cmbSalesType.ShowSelectedValue(_CurrentData.SalesTypeID, _CurrentData.SalesTypeName);

            //揽货部门
            trsSalesDep.ShowSelectedValue(_CurrentData.SalesDepartmentID, _CurrentData.SalesDepartmentName);
            //3个付款方式
            cmbPaymentTerm.ShowSelectedValue(_CurrentData.PaymentTermID, _CurrentData.PaymentTermName);
            cmbMBLPaymentTerm.ShowSelectedValue(_CurrentData.MBLPaymentTermID, _CurrentData.MBLPaymentTermName);
            cmbHBLPaymentTerm.ShowSelectedValue(_CurrentData.HBLPaymentTermID, _CurrentData.HBLPaymentTermName);
            //航线
            cmbShippingLine.ShowSelectedValue(_CurrentData.ShippingLineID, _CurrentData.ShippingLineName);
            //运输条款
            cmbTransportClause.ShowSelectedValue(_CurrentData.TransportClauseID, _CurrentData.TransportClauseName);
            cmbMblTransportClause.ShowSelectedValue(_CurrentData.MBLTransportClauseID, _CurrentData.MBLTransportClauseName);
            //船公司
            mcmbCarrier.ShowSelectedValue(_CurrentData.CarrierID, _CurrentData.CarrierName);

            cmbMBLReleaseType.ShowSelectedValue(_CurrentData.MBLReleaseType, EnumHelper.GetDescription<FCMReleaseType>(_CurrentData.MBLReleaseType.Value, LocalData.IsEnglish));
            cmbHBLReleaseType.ShowSelectedValue(_CurrentData.HBLReleaseType, EnumHelper.GetDescription<FCMReleaseType>(_CurrentData.HBLReleaseType.Value, LocalData.IsEnglish));

            //箱需求
            if (_CurrentData.ContainerDescription != null)
            {
                containerDemandControl1.Text = _CurrentData.ContainerDescription.ToString();
            }
            dxErrorProvider1.SetIconAlignment(containerDemandControl1.ErrorHost, ErrorIconAlignment.MiddleRight);

            //驳船
            stxtPreVoyage.ShowSelectedValue(_CurrentData.PreVoyageID, _CurrentData.PreVoyageName);
            //大船
            stxtVoyage.ShowSelectedValue(_CurrentData.VoyageID, _CurrentData.VoyageName);

            orderFeeEditPart1.SetCompanyID(_CurrentData.CompanyID);
            partDelegate.SetContext(new BusinessOperationContext() {OperationID = _CurrentData.ID });

            mcmbFiler.ShowSelectedValue(_CurrentData.FilerId, _CurrentData.FilerName);

            mcmbBookingBy.ShowSelectedValue(_CurrentData.BookingByID, _CurrentData.BookingByName);

            mcmbOverseasFiler.ShowSelectedValue(_CurrentData.OverSeasFilerID, _CurrentData.OverSeasFilerName);

            mcmbBookinger.ShowSelectedValue(_CurrentData.BookingerID, _CurrentData.BookingerName);

            if (_CurrentData.CargoType.HasValue)
            {
                cmbCargoType.ShowSelectedValue(_CurrentData.CargoType,
                    EnumHelper.GetDescription<CargoType>(_CurrentData.CargoType.Value, LocalData.IsEnglish));
            }

            stxtReturnLocation.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;

            if (_CurrentData != null)
            {
                labContranct.Text = _CurrentData.ContractName + Environment.NewLine + _CurrentData.ItemCode;
            }
        }

        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("CreateBills", LocalData.IsEnglish ? "According to the contract system is generated with the bills" : "系统已根据合约生成了应付账单");
        }

        void InitalComboxes()
        {
            _CacheWeightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            //包装
            OEUtility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            {
                ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            });

            //重量
            OEUtility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            {
                _CacheWeightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            });

            //体积
            OEUtility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            {
                List<DataDictionaryList> volUnitss = ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            });
        }

        /// <summary>
        /// 为需要延迟加载数据源的控件注册事件
        /// 这些数据源不会和其它控件发生联动，加载一次即可
        /// </summary>
        void SetLazyLoaders()
        {
            //操作口岸列表   
            OEUtility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID))
                {
                    _CurrentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });

            #region Agent
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _CurrentData.AgentName;
                agentCustomer.ID = _CurrentData.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);
                SetAgentSource(agentCustomers);
            }
            OEUtility.SetEnterToExecuteOnec(stxtAgent, delegate
            {
                SetAgentSourceByCompanyID(_CurrentData.CompanyID);
                stxtAgent.CustomerID = _CurrentData.AgentID ?? Guid.Empty;
                stxtAgent.SetOperationContext(OperationContext);
                stxtAgent.ContactStage = ContactStage.SO;
                stxtAgent.ContactType = ContactType.Customer;
                stxtAgent.CustomerDescription = _CurrentData.AgentDescription;
                List<CustomerCarrierObjects> contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentData.AgentID);
                stxtAgent.ContactList = contactList;
                stxtAgent.OnOk += stxtAgent_OnOk;
                stxtAgent.OnRefresh += stxtAgent_OnRefresh;
                stxtAgent.EditValueChanging += stxtAgent_EditValueChanging;

                stxtAgent.EditValueChanged += delegate
                {
                    stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
                    if (stxtAgent.EditValue != null && stxtAgent.EditValue.ToString().Length > 0)
                    {
                        Guid id = new Guid(stxtAgent.EditValue.ToString());
                        ICPCommUIHelper.SetCustomerDesByID(id, _CurrentData.AgentDescription);
                        stxtAgent.CustomerDescription = _CurrentData.AgentDescription;
                        AddLastestContact(id, stxtAgent, ContactType.Customer);
                    }
                    stxtAgent.EditValueChanged += stxtAgent_EditValueChanged;
                };
            });
            #endregion

            //贸易条款
            OEUtility.SetEnterToExecuteOnec(cmbTradeTerm, delegate
            {
                if (_IsInitTradeTerm)
                    return;
                ICPCommUIHelper.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
            });

            //揽货方式
            OEUtility.SetEnterToExecuteOnec(cmbSalesType, delegate
            {
                List<DataDictionaryList> salesTypes = ICPCommUIHelper.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);
            });

            //3个付款方式的下拉列表
            OEUtility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {

                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });
            OEUtility.SetEnterToExecuteOnec(cmbMBLPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbMBLPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });
            OEUtility.SetEnterToExecuteOnec(cmbHBLPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbHBLPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });
            //航线
            OEUtility.SetEnterToExecuteOnec(cmbShippingLine, delegate
            {
                List<ShippingLineList> shippingLines = ICPCommUIHelper.SetCmbShippingLine(cmbShippingLine);
            });

            //船公司
            OEUtility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
            });

            //运输条款
            OEUtility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
            });

            //MBL运输条款
            OEUtility.SetEnterToExecuteOnec(cmbMblTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbMblTransportClause);
                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.MBLTransportClauseID) && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.TransportClauseID))
                {
                    _CurrentData.MBLTransportClauseID = _CurrentData.TransportClauseID;
                }
            });


            OEUtility.SetEnterToExecuteOnec(cmbMBLReleaseType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMReleaseType>(cmbMBLReleaseType, true);
            });

            OEUtility.SetEnterToExecuteOnec(cmbHBLReleaseType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMReleaseType>(cmbHBLReleaseType, true);
            });

            OEUtility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                //ICPCommUIHelper.SetMcmbUsersByCompanys(this.mcmbSales);
                List<UserList> userList = UserService.GetUnderlingUserList(null, new string[] { "海外拓展", "销售代表","电商顾问", "拓展员", "总裁", "副总裁", "总经理助理", "分公司总经理", "分公司副总经理" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbSales.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

            OEUtility.SetEnterToExecuteOnec(mcmbBookingParty, delegate
            {
                if (_CurrentData.CompanyID == null || _CurrentData.CompanyID == Guid.Empty)
                {
                    return;
                }
                List<ConfigureCustomerInfo> conCustomer = FCMCommonService.GetConfigureCustomers(_CurrentData.CompanyID);
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "CustomerEname" : "CustomerCname", LocalData.IsEnglish ? "CustomerName" : "客户名称");
                //col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbBookingParty.InitSource<ConfigureCustomerInfo>(conCustomer, col, LocalData.IsEnglish ? "CustomerEname" : "CustomerCname", "CustomerID");

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingPartyID) && conCustomer != null & conCustomer.Count > 0)
                {
                    ConfigureCustomerInfo conCustomerinfo = conCustomer.FirstOrDefault(con => con.IsDefault == true);
                    if (conCustomerinfo != null)
                    {
                        _CurrentData.BookingPartyID = conCustomerinfo.CustomerID;
                    }
                }

                mcmbBookingParty.EditValueChanged += delegate
                {
                    BookingPartyChanged();
                };
            });



            //业务类型
            OEUtility.SetEnterToExecuteOnec(cmbType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMOperationType>(cmbType, true);
            });
            //委托方式
            OEUtility.SetEnterToExecuteOnec(cmbBookingMode, delegate
            {
                if (_IsInitBookingMode)
                    return;
                ICPCommUIHelper.SetComboxByEnum<FCMBookingMode>(cmbBookingMode, false);
            });

            //货物描述
            OEUtility.SetEnterToExecuteOnec(cmbCargoType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<CargoType>(cmbCargoType, true, true);
            });

            mcmbFiler.Enter += mcmFiler_Enter;
            mcmbBookinger.Enter += mcmbBookinger_Enter;
            mcmbOverseasFiler.Enter += mcmbOverseasFiler_Enter;
            mcmbBookingBy.Enter += mcmbBookingBy_Enter;
        }

        /// <summary>
        /// 搜索器注册
        /// </summary>
        void SearchRegister()
        {
            ucInquirePrice.ClickNew += ucInquirePrice_ClickNew;

            #region Customer

            //仓库
            DataFindClientService.Register(stxtWarehouse, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                //this.GetConditionsForWarehouse,
                delegate(object inputSource, object[] resultData)
                {
                    stxtWarehouse.Tag = _CurrentData.WarehouseID = (Guid)resultData[0];
                    stxtWarehouse.EditValue = _CurrentData.WarehouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    stxtWarehouse.Tag = _CurrentData.WarehouseID = null;
                    stxtWarehouse.EditValue = _CurrentData.WarehouseName = string.Empty;
                },
                ClientConstants.MainWorkspace);

            DataFindClientService.Register(stxtReturnLocation, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForReturnLocation,
                delegate(object inputSouce, object[] resultData)
                {
                    stxtReturnLocation.Tag = _CurrentData.ReturnLocationID = (Guid)resultData[0];
                    stxtReturnLocation.EditValue = _CurrentData.ReturnLocationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    stxtReturnLocation.Tag = _CurrentData.ReturnLocationID = null;
                    stxtReturnLocation.EditValue = _CurrentData.ReturnLocationName = string.Empty;
                },
            ClientConstants.MainWorkspace);

            DataFindClientService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _CurrentData.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _CurrentData.AgentOfCarrierID = new Guid(resultData[0].ToString());


                   Guid id = new Guid(resultData[0].ToString());
                   stxtAgentOfCarrier.SetCustomerID(id);
                   CustomerDescription _customerDescription = new CustomerDescription();
                   ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
               }, delegate
               {
                   stxtAgentOfCarrier.Tag = _CurrentData.AgentOfCarrierID = Guid.Empty;
                   stxtAgentOfCarrier.Text = _CurrentData.AgentOfCarrierName = string.Empty;
                   stxtAgentOfCarrier.SetCustomerID(Guid.Empty);
                   stxtAgentOfCarrier.ContactList = new List<CustomerCarrierObjects>();
                   stxtAgentOfCarrier.CustomerDescription = new CustomerDescription();
               }, ClientConstants.MainWorkspace);

            OEUtility.SetEnterToExecuteOnec(stxtAgentOfCarrier, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentData.AgentOfCarrierID);
                agentOfCarrierFinderBridge = new CustomerContactFinderBridge(stxtAgentOfCarrier, null, contactList, ContactStage.SO, _CurrentData.AgentOfCarrierID, true, ContactType.Carrier);
                stxtAgentOfCarrier.SetOperationContext(OperationContext);
                agentOfCarrierFinderBridge.Init();
                stxtAgentOfCarrier.OnOk += stxtAgentOfCarrier_OnOk;
                stxtAgentOfCarrier.OnRefresh += stxtAgentOfCarrier_OnRefresh;
                stxtAgentOfCarrier.BeforeEditValueChanged += stxtAgentOfCarrier_BeforeEditValueChanged;
                stxtAgentOfCarrier.AfterEditValueChanged += stxtAgentOfCarrier_AfterEditValueChanged;
            });

            #endregion

            #region 订舱客户
            DataFindClientService.Register(stxtBookingCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldBookingCustomerID = _CurrentData.BookingCustomerID;
                          Guid customerID = new Guid(resultData[0].ToString());
                          if (customerID == oldBookingCustomerID)
                          {
                              return;
                          }
                          CustomerStateType state = (CustomerStateType)resultData[7];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                              {
                                  return;

                              }
                          }

                          CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[8];
                          if (!approved.HasValue
                              || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          {

                              if (approved.Value == CustomerCodeApplyState.Processing)
                              {
                                  DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
                   , LocalData.IsEnglish ? "Tip" : "提示"
                   , MessageBoxButtons.OK);

                                  return;
                              }
                              else if (approved.Value == CustomerCodeApplyState.UnApply)
                              {
                                  if ((resultData[10] == null ||
                                      string.IsNullOrEmpty(resultData[10].ToString())) &&
                                      (resultData[11] == null ||
                                      string.IsNullOrEmpty(resultData[11].ToString())))
                                  {
                                      MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK);

                                      return;
                                  }

                                  DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              );
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                              else if (approved.Value == CustomerCodeApplyState.Unpassed)
                              {
                                  if ((resultData[10] == null ||
                                      string.IsNullOrEmpty(resultData[10].ToString())) &&
                                      (resultData[11] == null ||
                                      string.IsNullOrEmpty(resultData[11].ToString())))
                                  {
                                      MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  );

                                      return;
                                  }

                                  DialogResult result = MessageBoxService.ShowQuestion("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              );
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                          }
                          Guid id = new Guid(resultData[0].ToString());
                          stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = id;
                          stxtBookingCustomer.Text = _CurrentData.BookingCustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                          stxtBookingCustomer.SetCustomerID(id);
                          CustomerDescription _customerDescription = new CustomerDescription();
                          ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
                          stxtBookingCustomer.CustomerDescription = _CurrentData.BookingCustomerDescription = _customerDescription;

                      }, delegate
                      {
                          stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = Guid.Empty;
                          stxtBookingCustomer.Text = _CurrentData.BookingCustomerName = string.Empty;
                          stxtBookingCustomer.SetCustomerID(Guid.Empty);
                          stxtBookingCustomer.ContactList = new List<CustomerCarrierObjects>();
                          stxtBookingCustomer.CustomerDescription = new CustomerDescription();
                      },
                      ClientConstants.MainWorkspace);
            #endregion

            #region SCNA
            //订舱客户
            OEUtility.SetEnterToExecuteOnec(stxtBookingCustomer, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentData.BookingCustomerID);
                stxtBookingCustomer.SetOperationContext(OperationContext);
                bookingCustomerFinderBridge = new CustomerContactFinderBridge(stxtBookingCustomer, _CurrentData.BookingCustomerDescription, contactList, ContactStage.SO, _CurrentData.BookingCustomerID, true, ContactType.Customer);

                bookingCustomerFinderBridge.Init();
                stxtBookingCustomer.OnOk += stxtBookingCustomer_OnOk;
                stxtBookingCustomer.BeforeEditValueChanged += stxtBookingCustomer_EditValueChanging;
                stxtBookingCustomer.OnRefresh += stxtBookingCustomer_OnRefresh;
                stxtBookingCustomer.AfterEditValueChanged += stxtBookingCustomer_EditValueChanged;

            });

            //客户
            OEUtility.SetEnterToExecuteOnec(stxtCustomer, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentData.CustomerID);
                if (_CurrentData.QuotedPricePartInfo != null)
                {
                    stxtRecentQuotedPrice.CustomerID = _CurrentData.CustomerID;
                    stxtRecentQuotedPrice.CustomerName = _CurrentData.CustomerName;
                }
                stxtCustomer.CustomerID = _CurrentData.CustomerID;
                stxtCustomer.SetOperationContext(OperationContext);
                stxtCustomer.CompanyID = _CurrentData.CompanyID;
                stxtCustomer.ContactStage = ContactStage.SO;
                stxtCustomer.ContactType = ContactType.Customer;
                stxtCustomer.ContactList = contactList;
                stxtCustomer.EditValueChanging += stxtCustomer_EditValueChanging;
                stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
                stxtCustomer.SelectChanged += stxtCustomer_SelectChanged;

            });
            stxtCustomer.OnOk += stxtCustomer_OnOk;
            stxtCustomer.OnRefresh += stxtCustomer_OnRefresh;

            mcmbSales.EditValueChanged += mcmbSales_EditValueChanged;

            //shipper
            OEUtility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_CacheCountryList == null) _CacheCountryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                shipperBridge = new CustomerFinderBridge(
                stxtShipper,
                _CacheCountryList,
                DataFindClientService,
                CustomerService,
                _CurrentData.ShipperDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                shipperBridge.Init();
            });
            stxtShipper.OnOk += stxtShipper_OnOk;

            //Consignee
            OEUtility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                if (_CacheCountryList == null) _CacheCountryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                consigneeBridge = new CustomerFinderBridge(
                stxtConsignee,
                _CacheCountryList,
                DataFindClientService,
                CustomerService,
                _CurrentData.ConsigneeDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });
            stxtConsignee.OnOk += stxtConsignee_OnOk;

            OEUtility.SetEnterToExecuteOnec(stxtNotifyParty, delegate
            {
                if (_CacheCountryList == null) _CacheCountryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                notifyPartyBridge = new CustomerFinderBridge(
                stxtNotifyParty,
                _CacheCountryList,
                DataFindClientService,
                CustomerService,
                _CurrentData.NotifyPartydescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                notifyPartyBridge.Init();
            });
            stxtNotifyParty.OnOk += stxtNotifyParty_OnOk;

            //stxtBookingShipper
            OEUtility.SetEnterToExecuteOnec(stxtBookingShipper, delegate
            {
                if (_CacheCountryList == null) _CacheCountryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingShipperID) && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.NotifyPartyID))
                {
                    _CurrentData.BookingShipperID = _CurrentData.NotifyPartyID;
                }

                bookingShipperBridge = new CustomerFinderBridge(
               stxtBookingShipper,
               _CacheCountryList,
               DataFindClientService,
               CustomerService,
               _CurrentData.BookingShipperdescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                bookingShipperBridge.Init();
            });
            stxtBookingShipper.OnOk += stxtBookingShipper_OnOk;

            //stxtBookingConsignee
            OEUtility.SetEnterToExecuteOnec(stxtBookingConsignee, delegate
            {
                if (_CacheCountryList == null) _CacheCountryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingConsigneeID) && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.AgentID))
                {
                    _CurrentData.BookingConsigneeID = _CurrentData.AgentID;
                }

                bookingConsigneeBridge = new CustomerFinderBridge(
                stxtBookingConsignee,
                _CacheCountryList,
                DataFindClientService,
                CustomerService,
                _CurrentData.BookingConsigneedescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                bookingConsigneeBridge.Init();
            });
            stxtBookingConsignee.OnOk += stxtBookingConsignee_OnOk;

            OEUtility.SetEnterToExecuteOnec(stxtBookingNotifyParty, delegate
            {
                if (_CacheCountryList == null) _CacheCountryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.NotifyPartyID) && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.BookingConsigneeID))
                {
                    _CurrentData.NotifyPartyID = _CurrentData.BookingConsigneeID;
                }

                bookingNotifyPartyBridge = new CustomerFinderBridge(
                stxtBookingNotifyParty,
                _CacheCountryList,
                DataFindClientService,
                CustomerService,
                _CurrentData.BookingNotifyPartydescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                bookingNotifyPartyBridge.Init();
            });
            stxtBookingNotifyParty.OnOk += stxtBookingNotifyParty_OnOk;
            #endregion

            #region Port
            pfbPlaceOfReceipt = new LocationFinderBridge(stxtPlaceOfReceipt, DataFindClientService, LocalData.IsEnglish);
            PortFinderBridge pfbPOL = new PortFinderBridge(stxtPOL, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(stxtPOD, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbPlacePay = new PortFinderBridge(stxtPlacePay, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbPlacePayOrder = new PortFinderBridge(stxtPlacePayOrder, DataFindClientService, LocalData.IsEnglish);

            //LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtPlaceOfDelivery, this.dfService, LocalData.IsEnglish);

            DataFindClientService.Register(stxtPlaceOfDelivery, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                     delegate(object inputSource, object[] resultData)
                     {
                         Guid oldPlaceOfDeliveryID = _CurrentData.PlaceOfDeliveryID;
                         Guid newPlaceOfDeliveryID = new Guid(resultData[0].ToString());
                         if (oldPlaceOfDeliveryID == newPlaceOfDeliveryID)
                         {
                             return;
                         }

                         stxtPlaceOfDelivery.Tag = _CurrentData.PlaceOfDeliveryID = newPlaceOfDeliveryID;
                         stxtPlaceOfDelivery.Text = _CurrentData.PlaceOfDeliveryName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                         SetFinalDestinationByTransportClause();
                         SetAgetnEnabledByPlaceOfDeliveryAndCompany();
                     }, delegate
                     {
                         stxtPlaceOfDelivery.Tag = _CurrentData.PlaceOfDeliveryID = Guid.Empty;
                         stxtPlaceOfDelivery.Text = _CurrentData.PlaceOfDeliveryName = string.Empty;
                     },
                      ClientConstants.MainWorkspace);

            LocationFinderBridge pfbFinalDestination = new LocationFinderBridge(stxtFinalDestination, DataFindClientService, LocalData.IsEnglish);

            #endregion

            #region Quoted Price Order
            stxtRecentQuotedPrice.SelectChanged += stxtRecentQuotedPrice_SelectChanged;
            #endregion

            VoyageListGetter getter = new VoyageListGetter();
            VoyageDelegate voyageDelegate = getter.GetVoyageList;
            IAsyncResult asyncResult = voyageDelegate.BeginInvoke(string.Empty, null, null);
            List<VoyageList> curent = voyageDelegate.EndInvoke(asyncResult);

            stxtVoyage.set(curent, _CurrentData.CompanyID);
            stxtPreVoyage.set(curent, _CurrentData.CompanyID);
        }
        #endregion

        #region 事件注册
        /// <summary>
        /// 注册各种联动的事件
        /// </summary>
        void RegisterRelativeEvents()
        {
            
            xtraTabControl1.SelectedPageChanged += xtraTabControl1_SelectedPageChanged;
            //订舱客户,如果贸易条款为CIF，那么就为客户，否则为空白
            cmbTradeTerm.SelectedIndexChanged += cmbTradeTerm_SelectedIndexChanged;

            cmbTransportClause.SelectedIndexChanged += delegate
            {
                if (_shown)
                {
                    _CurrentData.TransportClauseName = cmbTransportClause.Text;
                    SetPlaceOfDeliveryByTransportClause();
                    SetFinalDestinationByTransportClause();
                }
            };

            cmbOrderNo.Enter += cmbOrderNo_Enter;
            cmbOrderNo.LostFocus += cmbOrderNo_SelectedIndexChanged;
            cmbCargoType.Click += cmbCargoType_Enter;
            cmbCargoType.SelectedIndexChanged += cmbCargoType_EditValueChanged;
            mcmbSales.SelectedRow += mcmbSales_SelectedRow;
            trsSalesDep.Enter += trsSalesDep_Enter;

            stxtBookingCustomer.TextChanged += stxtBookingCustomer_TextChanged;
        }

        /// <summary>
        /// 注册界面控件之间联动的事件并立即执行一次
        /// </summary>
        void RegisterRelativeEventsAndRunOnce()
        {
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            cmbOrderNo.TextChanged += cmbOrderNo_TextChanged;
            chkIsOnlyMBL.CheckedChanged += chkIsOnlyMBL_CheckedChanged;
            chkHasContract.CheckedChanged += chkHasContract_CheckedChanged;
            txtContractNo.Click += txtContractNo_Click;
            cmbSalesType.SelectedIndexChanged += cmbSalesType_SelectedIndexChanged;
            chkHasContract.Click += chkHasContract_Click;
            RunAtOnce();
        }
        #endregion

        #region 新订舱单的特殊逻辑

        /// <summary>
        /// 
        /// </summary>
        void ReadyForNew()
        {
            OceanBookingInfo newData = new OceanBookingInfo();
            newData.OEOperationType = FCMOperationType.FCL;
            newData.BookingDate = newData.CreateDate = DateTime.Now;
            //确认日期为今天
            newData.SODate = DateTime.Now;
            newData.BookingMode = FCMBookingMode.Fax;
            newData.State = OEOrderState.NewOrder;
            newData.AgentID = Guid.Empty;
            newData.BookingerID = LocalData.UserInfo.LoginID;
            newData.BookingerName = LocalData.UserInfo.LoginName;
            newData.IsContract = true;
            newData.IsValid = true;

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = normalDictionary.EName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            newData.WeightUnitID = normalDictionary.ID;
            newData.WeightUnitName = normalDictionary.EName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = normalDictionary.EName;
            #endregion

            _CurrentData = newData;

            _CurrentData.HBLReleaseType = _CurrentData.MBLReleaseType = FCMReleaseType.Unknown;

            // TODO: 这种Guard型的逻辑要在最开始的时候完成
            OEUtility.EnsureDefaultCompanyExists(UserService);

            _CurrentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            _CurrentData.CompanyName = LocalData.UserInfo.DefaultCompanyName;

            _CurrentData.QuotedPricePartInfo = new QuotedPricePartInfo();

        }

        #endregion

        #region 复制订舱单时的逻辑

        /// <summary>
        /// 复制订舱单时的逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            _CurrentData.ID = Guid.Empty;
            _CurrentData.No = string.Empty;
            _CurrentData.CreateDate = DateTime.Now;
            _CurrentData.OceanShippingOrderID = Guid.Empty;
            _CurrentData.OceanShippingOrderNo = string.Empty;
            _CurrentData.AgentID = Guid.Empty;
            _CurrentData.IsContract = false;
            if (_CurrentData.BookingerID != null && _CurrentData.BookingerID == LocalData.UserInfo.LoginID)
            {
                _CurrentData.BookingerID = LocalData.UserInfo.LoginID;
            }
        }

        #endregion

        #region 联动更改
        /// <summary>
        /// 订舱人发生改变
        /// </summary>
        private void BookingPartyChanged()
        {
            CustomerDescription bookingPartyDescription = new CustomerDescription();
            ICPCommUIHelper.SetCustomerDesByID(_CurrentData.BookingPartyID, bookingPartyDescription);
            _CurrentData.BookingShipperdescription = bookingPartyDescription;
            _CurrentData.BookingShipperID = _CurrentData.BookingPartyID;
            _CurrentData.BookingShipperName = _CurrentData.BookingPartyName;
        }
        #endregion

        #region IEditPart 成员
        void GetData(Guid orderId)
        {
            _CurrentData = OceanExportService.GetOceanBookingInfo(orderId);
            origContract = _CurrentData.ContractID;
            chargeContract = false;
            CacheFeelist = OceanExportService.GetOceanOrderFeeList(orderId, Guid.Empty);
            CacheDelegate = FCMCommonService.GetBookingDelegateList(new SearchParameterBookingDelegate() { OperationID = orderId });

        }

        void ShowOrder()
        {
            InitData();
            _CurrentData.BeginEdit();
            bsBookingInfo.DataSource = _CurrentData;
            bsBookingInfo.ResetBindings(false);

            InitMessage();
            InitControls();

            if (_CurrentData.ID == Guid.Empty)
            {
                CacheFeelist = new List<OceanBookingFeeList>();
                if(CacheDelegate.Count>0)
                {
                    FillCSPBookingToCurrent();
                }
            }
            else
            {
                OperationContext = GetContext(_CurrentData);
            }

            OperationContext.OperationType = OperationType.OceanExport;

            UCBookingOtherPart.BindData(OperationContext);
            orderFeeEditPart1.SetSource(CacheFeelist);
            partDelegate.DataChanged += (sender, e) =>
            {
                CacheDelegate = sender as List<BookingDelegate>;
                FillCSPBookingToCurrent();
            };
            partDelegate.SetSource(CacheDelegate);

            ucInquirePrice.OceanBookingInfo = _CurrentData;
            ucInquirePrice.DataSource = _CurrentData.InquirePricePartInfo;

            stxtRecentQuotedPrice.SalesID = _CurrentData.SalesID ?? Guid.Empty;
            stxtRecentQuotedPrice.CustomerID = _CurrentData.CustomerID;
            stxtRecentQuotedPrice.CustomerName = _CurrentData.CustomerName;

            if (_CurrentData.QuotedPricePartInfo == null)
                _CurrentData.QuotedPricePartInfo = new QuotedPricePartInfo();
            if (_CurrentData.QuotedPricePartInfo.QuotedPriceID != null)
            {
                stxtRecentQuotedPrice.QuotedPriceID = _CurrentData.QuotedPricePartInfo.QuotedPriceID.Value;
                stxtRecentQuotedPrice.QuotedPriceNo = _CurrentData.QuotedPricePartInfo.QuotedPriceNo;

            }

            orderFeeEditPart1.SetChargeCodeDataSource(_CurrentData.FreightIncludedids, _CurrentData.FreightIncludedString);

        }

        void FillCSPBookingToCurrent()
        {
            BookingDelegate cspPBooking = CacheDelegate.SingleOrDefault(fitem => fitem.IsDefault && fitem.CancelRemark.IsNullOrEmpty());
            if(cspPBooking==null)
                cspPBooking=CacheDelegate.First();
            if (!cspPBooking.CancelRemark.IsNullOrEmpty())
            {
                return;
            }
            
            //初始化
            ICPCommUIHelper.SetComboxByEnum<FCMBookingMode>(cmbBookingMode, false);
            _IsInitBookingMode = true;
            cmbBookingMode.SelectedIndex = 3;

            ICPCommUIHelper.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
            _IsInitTradeTerm = true;
            cmbTradeTerm.EditValue = cspPBooking.IncoTermID;

            dteBookingDate.EditValue = _CurrentData.BookingDate = cspPBooking.BookingDate;
            //Customer
            stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
            stxtCustomer.CustomerID = cspPBooking.CustomerID;
            stxtCustomer.SetOperationContext(OperationContext);
            stxtCustomer.CompanyID = _CurrentData.CompanyID;
            stxtCustomer.ContactStage = ContactStage.SO;
            stxtCustomer.ContactType = ContactType.Customer;
            stxtCustomer.Tag = _CurrentData.CustomerID = cspPBooking.CustomerID;
            stxtCustomer.Text = _CurrentData.CustomerName = cspPBooking.CustomerName;
            stxtCustomer.CustomerDescription = cspPBooking.CustomerDescription;
            stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
            //BookingCustomer
            stxtBookingCustomer.Tag = _CurrentData.BookingCustomerID = cspPBooking.CustomerID;
            stxtBookingCustomer.Text = _CurrentData.BookingCustomerName = cspPBooking.CustomerName;
            stxtBookingCustomer.SetCustomerID(cspPBooking.CustomerID);
            stxtBookingCustomer.CustomerDescription = _CurrentData.BookingCustomerDescription = cspPBooking.CustomerDescription;
            //Shipper
            stxtShipper.Tag = _CurrentData.ShipperID = cspPBooking.ShipperID;
            stxtShipper.Text = _CurrentData.ShipperName = cspPBooking.ShipperName;
            stxtShipper.CustomerDescription = _CurrentData.ShipperDescription = cspPBooking.ShipperDescription;
            //Consignee
            stxtConsignee.Tag =_CurrentData.ConsigneeID =  cspPBooking.ConsigneeID;
            stxtConsignee.Text = _CurrentData.ConsigneeName =  cspPBooking.ConsigneeName;
            stxtConsignee.CustomerDescription = _CurrentData.ConsigneeDescription =  cspPBooking.ConsigneeDescription;

            _CurrentData.TransportClauseID = cspPBooking.TransportClauseID;
            _CurrentData.TransportClauseName = cspPBooking.TransportClauseName;
            cmbTransportClause.ShowSelectedValue(_CurrentData.TransportClauseID, _CurrentData.TransportClauseName);
            mcmbSales.EditValue = _CurrentData.SalesID = cspPBooking.SalesID;
            _CurrentData.SalesName = cspPBooking.SalesName;
            mcmbSales.ShowSelectedValue(_CurrentData.SalesID, _CurrentData.SalesName);
            SetDefaultOverseasFiler();
            SetDefaultFiler();
            SetSalesDepartment();
            _CurrentData.IsContract = false;
            _CurrentData.IsTruck = cspPBooking.IsTruck;
            _CurrentData.IsInsurance = cspPBooking.IsInsurance;
            _CurrentData.IsCustoms = cspPBooking.IsDeclaration;
            if (!cspPBooking.POLID.IsNullOrEmpty())
            {
                _CurrentData.POLID = cspPBooking.POLID.Value;
                _CurrentData.POLName = cspPBooking.POLName;
            }
            _CurrentData.PlaceOfReceiptAddress = cspPBooking.POLAddress;
            if (!cspPBooking.PODID.IsNullOrEmpty())
            {
                _CurrentData.PODID = _CurrentData.PlaceOfDeliveryID = cspPBooking.PODID.Value;
                _CurrentData.PODName = _CurrentData.PlaceOfDeliveryName = cspPBooking.PODName;
            }
            _CurrentData.PlaceOfDeliveryAddress = cspPBooking.PODAddress;
            _CurrentData.ETD = cspPBooking.ETDForPOL;
            if (cspPBooking.ETAForPOD!=null) _CurrentData.ETA = cspPBooking.ETAForPOD.Value;
            cmbQuantityUnit.EditValue = cspPBooking.QuantityUnitID;
            cmbWeightUnit.EditValue = cspPBooking.WeightUnitID;
            cmbMeasurementUnit.EditValue = cspPBooking.MeasurementUnitID;
            List<ICP.FCM.Common.ServiceInterface.DataObjects.Container> containers = new List<FCM.Common.ServiceInterface.DataObjects.Container>();
            foreach (BookingDelegate item in CacheDelegate)
            {
                foreach (ICP.FCM.Common.ServiceInterface.DataObjects.Container itemContainer in item.ContainerDescription.Containers)
                {
                    ICP.FCM.Common.ServiceInterface.DataObjects.Container itemFind = containers.SingleOrDefault(fItem => fItem.Size == itemContainer.Size && fItem.Type == itemContainer.Type);
                    if (itemFind!=null)
                    {
                        itemFind.Quantity += itemContainer.Quantity;
                    }else
                    {
                        containers.Add(itemContainer);
                    }
                }
            }
            if (containers != null && containers.Count > 0)
            {
                ContainerDescription cd = new ContainerDescription();
                cd.Containers.AddRange(containers);
                _CurrentData.ContainerDescription = cd;
                containerDemandControl1.Text = _CurrentData.ContainerDescription.ToString();
            }
            //毛件体汇总
            _CurrentData.Quantity = CacheDelegate.Sum(fitem => fitem.Quantity);
            _CurrentData.Weight = CacheDelegate.Sum(fitem => fitem.Weight);
            _CurrentData.Measurement = CacheDelegate.Sum(fitem => fitem.Measurement);
        }

        void ShowOrderRefresh()
        {
            InitData();
            _CurrentData.BeginEdit();
            bsBookingInfo.DataSource = _CurrentData;
            bsBookingInfo.ResetBindings(false);

            InitMessage();
            InitControls();

            if (_CurrentData.ID == Guid.Empty)
            {
                CacheFeelist = new List<OceanBookingFeeList>();
            }
            orderFeeEditPart1.SetSource(CacheFeelist);
        }

        private void InnerBindData()
        {
            SuspendLayout();
            orderFeeEditPart1.SetService(PartWorkItem);
            bookingPOEditPart1.SetService(PartWorkItem);
            if (EditMode == EditMode.New)
            {
                //新建
                _CurrentData = new OceanBookingInfo();
                ReadyForNew();
            }
            else
            {

                if (EditMode == EditMode.Edit)
                {
                }
                else if (EditMode == EditMode.Copy)
                {
                    PrepareForCopyExistOrder();
                }
            }

            InitalComboxes();

            ShowOrder();

            #region 判断邮件地址是否存在联系人列表中
            if (_businessOperationParameter != null)
            {
                if (_businessOperationParameter.Message != null)
                {
                    if (_businessOperationParameter.Message.Attachments.Count != 0)
                    {
                        List<string> strFiles = new List<string>();
                        foreach (var item in _businessOperationParameter.Message.Attachments)
                        {
                            strFiles.Add(item.ClientPath);
                        }
                        UCBookingOtherPart.AddDocuments(strFiles, DocumentType.BKG);
                    }
                    if (_businessOperationParameter.ActionType ==
                        ActionType.Create)
                    {
                        //默认将接收者带入订舱单编辑界面
                        List<CustomerCarrierObjects> dataList = FCM.Common.ServiceInterface.FCMInterfaceUtility.GetOperationContactsAndMailContacts(
                            _businessOperationParameter.Message, OperationContext, false, false, false);

                        UCBookingOtherPart.InsertContactList(dataList);
                        dataList.Clear();
                        dataList = null;
                        isSaveOperationContact = true;
                    }
                }
                else
                {
                    if (EditMode == EditMode.Copy)
                    {
                        ContactObjects contactInfo = FCMCommonService.GetContactList(_businessOperationParameter.Context.OperationID, _businessOperationParameter.Context.OperationType);
                        if (contactInfo != null && contactInfo.CustomerCarrier != null)
                        {
                            UCBookingOtherPart.InnerBindData(contactInfo.CustomerCarrier);
                            isSaveOperationContact = true;
                        }
                    }
                }
            }
            #endregion

            SearchRegister();
            SetLazyLoaders();

            _CurrentData.CancelEdit();
            _CurrentData.BeginEdit();

            ResumeLayout(true);
            InnerOnLoad();
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
                    break;
                }
                if (item.Key.ToUpper() == "BOOKINGINFOFORCSP".ToUpper())
                {
                    CacheDelegate = item.Value as List<BookingDelegate>;
                    break;
                }
            }
        }

        private void InnerOnLoad()
        {
            SetTitle();
            RegisterRelativeEvents();
            RegisterRelativeEventsAndRunOnce();

            OEUtility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtBookingCustomer,
                stxtAgent.lookUpEdit1,
                stxtAgentOfCarrier,
                stxtWarehouse,
                stxtReturnLocation,
            });

            OEUtility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery,
                stxtPlaceOfReceipt ,
                stxtFinalDestination ,
                stxtPOD,
                stxtPOL,
            });


            SmartPartClosing += BookingBaseEditPart_SmartPartClosing;
            ActivateSmartPartClosingEvent(PartWorkItem);

            _CurrentData.CancelEdit();
        }
        private delegate void DataBindingDelegate();
        public void BindingData(object data)
        {
            if (EditMode != EditMode.New)
            {

                //如果编辑模式不是新增，则需要查询数据,将查询数据在异步线程里处理
                if (EditMode != EditMode.New)
                {

                    EditPartShowCriteria criteria = data as EditPartShowCriteria;
                    GetData((Guid)criteria.BillNo);
                }
                InnerBindData();

            }
            else
            {
                InnerBindData();
            }

        }

        public override object DataSource
        {
            get { return bsBookingInfo.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return Save(_CurrentData, false);
        }

        public override void EndEdit()
        {
            Validate();
            bsBookingInfo.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="parameters"></param>
        private void AfterSelectContract(object[] parameters)
        {
            FreightList selectedContract = parameters[0] as FreightList;
            if (selectedContract != null)
            {
                _CurrentData.BeginEdit();
                _CurrentData.ContractID = selectedContract.ID;
                txtContractNo.Text = _CurrentData.ContractNo = selectedContract.FreightNo;
                labContranct.Text = selectedContract.ContractName + Environment.NewLine + selectedContract.ItemCode;
                if (selectedContract.Carrier == "MSC")
                {
                    if (_CurrentData.FinalDestinationID != null)
                    {
                        LocationInfo FinalDestination = GeographyService.GetLocationInfo((Guid)_CurrentData.FinalDestinationID);
                        if (FinalDestination.CountryID.ToString().ToUpper() == "37F06C2D-E5F6-4A6F-BB55-9DA3EC3B42A4")
                        {
                            ReloadMscInfo();
                        }
                    }
                    else
                    {
                        if (OEUtility.USAShippingLines.Contains((Guid)_CurrentData.ShippingLineID))
                        {
                            ReloadMscInfo();
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 重新加载MSC信息
        /// </summary>
        private void ReloadMscInfo()
        {
            _CurrentData.BookingPartyID = new Guid("0751E34D-6FC6-E511-938F-0026551CA878");
            _CurrentData.BookingShipperID = new Guid("0751E34D-6FC6-E511-938F-0026551CA878");
            _CurrentData.BookingConsigneeID = new Guid("B8006234-2F00-E611-80D5-2047477D7A58");
            _CurrentData.AgentID = new Guid("B8006234-2F00-E611-80D5-2047477D7A58");
            CustomerInfo shipper = CustomerService.GetCustomerInfo((Guid)_CurrentData.BookingShipperID);
            CustomerInfo Consignee = CustomerService.GetCustomerInfo((Guid)_CurrentData.BookingConsigneeID);

            mcmbBookingParty.EditText = shipper.CName;
            mcmbBookingParty.EditValue = _CurrentData.BookingPartyID;

            stxtBookingShipper.Tag = _CurrentData.BookingShipperID = shipper.ID;
            stxtBookingShipper.Text = _CurrentData.BookingShipperName = shipper.CName;
            ICPCommUIHelper.SetCustomerDesByID(_CurrentData.BookingShipperID, _CurrentData.BookingShipperdescription);

            stxtBookingConsignee.Tag = _CurrentData.BookingConsigneeID = Consignee.ID;
            stxtBookingConsignee.Text = _CurrentData.BookingConsigneeName = Consignee.CName;
            ICPCommUIHelper.SetCustomerDesByID(_CurrentData.BookingConsigneeID, _CurrentData.BookingConsigneedescription);
        }
        /// <summary>
        /// 根据Booking信息构建业务操作上下文
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        private BusinessOperationContext GetContext(OceanBookingInfo orderInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext
            {
                OperationID = orderInfo.ID,
                OperationNO = orderInfo.No,
                OperationType = OperationType.OceanExport,
                FormId = orderInfo.ID,
                FormType = FormType.ShippingOrder
            };
            context["UpdateDate"] = orderInfo.UpdateDate;
            return context;
        }


        /// <summary>
        /// 费用确认
        /// </summary>
        /// <param name="isEnglish">是否发送英文版本</param>
        public void ConfirmDebitFees(bool isEnglish)
        {
            //{0}业务号
            //{1}客户名称
            //{2}起运港
            //{3}目的港
            //{4}揽货人名称
            //{5}当前用户
            //{6}箱型
            if (!string.IsNullOrEmpty(stxtCustomer.EditValue.ToString()))
            {
                _CurrentData.CustomerName = stxtCustomer.EditValue.ToString();
            }
            string result = ClientOceanExportService.MailSalesForConfirmDebitFees(isEnglish, _CurrentData);
            ShowErrorInfo(result);


        }

        /// <summary>
        /// 数据值的比较 并且记录修改的值信息
        /// </summary>
        /// <returns></returns>
        public bool ValueCompare(OceanBookingInfo oceanBookingInfo)
        {
            bool flg = false;
            if (oceanBookingInfo.ID != Guid.Empty)
            {

                _old = OceanExportService.GetOceanBookingInfo(oceanBookingInfo.ID);
                if (_old != null && _CurrentData != null)
                {
                    if (_old.POLID != oceanBookingInfo.POLID)
                    {
                        flg = true;
                        _oldstring.Append("POL:" + _old.POLName + "<br/><br/>");
                        _updatestring.Append("POL:" + oceanBookingInfo.POLName + "<br/><br/>");
                    }
                    if (_old.PODID != oceanBookingInfo.PODID)
                    {
                        flg = true;
                        _oldstring.Append("POD:" + _old.PODName + "<br/><br/>");
                        _updatestring.Append("POD:" + oceanBookingInfo.PODName + "<br/><br/>");
                    }
                    if (_old.PlaceOfDeliveryID != oceanBookingInfo.PlaceOfDeliveryID)
                    {
                        flg = true;
                        _oldstring.Append("Place Of Delivery:" + _old.PlaceOfDeliveryName + "<br/><br/>");
                        _updatestring.Append("Place Of Delivery:" + oceanBookingInfo.PlaceOfDeliveryName + "<br/><br/>");
                    }
                    if (_old.FinalDestinationID != oceanBookingInfo.FinalDestinationID)
                    {
                        flg = true;
                        _oldstring.Append("Final Destination:" + _old.FinalDestinationName + "<br/><br/>");
                        _updatestring.Append("Final Destination:" + oceanBookingInfo.FinalDestinationName + "<br/><br/>");
                    }
                    if (_old.CarrierID != oceanBookingInfo.CarrierID)
                    {
                        flg = true;
                        _oldstring.Append("Carrier:" + _old.CarrierName + "<br/><br/>");
                        _updatestring.Append("Carrier:" + mcmbCarrier.EditText + "<br/><br/>");
                    }
                    if (_old.PlaceOfReceiptID != oceanBookingInfo.PlaceOfReceiptID)
                    {
                        flg = true;
                        _oldstring.Append("Place Of Receipt:" + _old.PlaceOfReceiptName + "<br/><br/>");
                        _updatestring.Append("Place Of Receipt:" + oceanBookingInfo.PlaceOfReceiptName + "<br/><br/>");
                    }
                    if (_old.ContainerDescription != null && oceanBookingInfo.ContainerDescription != null)
                    {
                        if (_old.ContainerDescription.ToString() != oceanBookingInfo.ContainerDescription.ToString())
                        {
                            flg = true;
                            _oldstring.Append("Container Description:" + _old.ContainerDescription + "<br/><br/>");
                            _updatestring.Append("Container Description:" + oceanBookingInfo.ContainerDescription + "<br/><br/>");
                        }
                    }
                    if (_old.IsWareHouse != oceanBookingInfo.IsWareHouse)
                    {
                        flg = true;
                        string old = _old.IsWareHouse ? "Yes" : "NO";
                        string update = oceanBookingInfo.IsWareHouse ? "Yes" : "NO";
                        _oldstring.Append("Warehouse:" + old + "<br/><br/>");
                        _updatestring.Append("Warehouse:" + update + "<br/><br/>");
                    }
                    if (_old.IsTruck != oceanBookingInfo.IsTruck)
                    {
                        flg = true;
                        string old = _old.IsTruck ? "Yes" : "NO";
                        string update = oceanBookingInfo.IsTruck ? "Yes" : "NO";
                        _oldstring.Append("Truck:" + old + "<br/><br/>");
                        _updatestring.Append("Truck:" + update + "<br/><br/>");
                    }
                    if (_old.IsCustoms != oceanBookingInfo.IsCustoms)
                    {
                        flg = true;
                        string old = _old.IsCustoms ? "Yes" : "NO";
                        string update = oceanBookingInfo.IsCustoms ? "Yes" : "NO";
                        _oldstring.Append("Customs:" + old + "<br/><br/>");
                        _updatestring.Append("Customs:" + update + "<br/><br/>");
                    }
                    if (_old.IsCommodityInspection != oceanBookingInfo.IsCommodityInspection)
                    {
                        flg = true;
                        string old = _old.IsCommodityInspection ? "Yes" : "NO";
                        string update = oceanBookingInfo.IsCommodityInspection ? "Yes" : "NO";
                        _oldstring.Append("Commodity Inspection:" + old + "<br/><br/>");
                        _updatestring.Append("Commodity Inspection:" + update + "<br/><br/>");
                    }
                    if (_old.IsQuarantineInspection != oceanBookingInfo.IsQuarantineInspection)
                    {
                        flg = true;
                        string old = _old.IsQuarantineInspection ? "Yes" : "NO";
                        string update = oceanBookingInfo.IsQuarantineInspection ? "Yes" : "NO";
                        _oldstring.Append("Quarantine Inspection:" + old + "<br/><br/>");
                        _updatestring.Append("Quarantine Inspection:" + update + "<br/><br/>");
                    }
                    if (_old.Commodity != oceanBookingInfo.Commodity)
                    {
                        flg = true;
                        _oldstring.Append("Commodity:" + _old.Commodity + "<br/><br/>");
                        _updatestring.Append("Commodity:" + oceanBookingInfo.Commodity + "<br/><br/>");
                    }
                    if (_old.Remark != oceanBookingInfo.Remark)
                    {
                        flg = true;
                        _oldstring.Append("Remark:" + _old.Remark + "<br/><br/>");
                        _updatestring.Append("Remark:" + oceanBookingInfo.Remark + "<br/><br/>");
                    }
                    //这里比较询价的信息
                    if (oceanBookingInfo.InquirePricePartInfo != null && _old.InquirePricePartInfo != null)
                    {
                        if (_old.InquirePricePartInfo.InquirePriceID != oceanBookingInfo.InquirePricePartInfo.InquirePriceID)
                        {
                            flg = true;
                            _oldstring.Append("Inquire Price NO:" + _old.InquirePricePartInfo.InquirePriceNO + "<br/><br/>");
                            _updatestring.Append("Inquire Price NO:" + oceanBookingInfo.InquirePricePartInfo.InquirePriceNO + "<br/><br/>");
                        }
                    }
                    //费用的变更
                    if (EditMode == EditMode.Edit)
                    {
                        var oceanOrderFeeList = OceanExportService.GetOceanOrderFeeList(oceanBookingInfo.ID, Guid.Empty);
                        //原来不存在费用信息 现在重新录入费用信息
                        if (oceanOrderFeeList.Count == 0 && orderFeeEditPart1.feePartAR.bsFee.List.Count > 0)
                        {
                            flg = true;
                            _updatestring.Append("Fee <br/><br/>");
                            foreach (var fee in orderFeeEditPart1.feePartAR.bsFee.List)
                            {
                                var fees = fee as OceanBookingFeeList;
                                _updatestring.Append("Code:" + fees.ChargingCodeName + " Amount:" + fees.Amount + "<br/>");
                            }
                        }
                        else
                        {
                            _oldstring.Append("Fee <br/><br/>");
                            _updatestring.Append("Fee <br/><br/>");
                            //数量相等的情况下
                            if (oceanOrderFeeList.Count == orderFeeEditPart1.feePartAR.bsFee.List.Count)
                            {
                                foreach (var item in orderFeeEditPart1.feePartAR.bsFee.List)
                                {
                                    var item1 = item as OceanBookingFeeList;
                                    if (item1.Amount != 0)
                                    {
                                        foreach (var item2 in oceanOrderFeeList)
                                        {
                                            if (item1.ID == item2.ID)
                                            {
                                                //金额，币种，代码比较是否变动
                                                if (item2.Amount != item1.Amount || item2.Currency != item1.Currency || item2.ChargingCodeName != item1.ChargingCodeName)
                                                {
                                                    flg = true;
                                                    _oldstring.Append("Code:" + item1.ChargingCodeName + " Amount:" + item1.Amount + item1.Currency + "<br/>");
                                                    _updatestring.Append("Code:" + item2.ChargingCodeName + " Amount:" + item2.Amount + item2.Currency + "<br/>");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //费用信息数量不相等的情况下，将费用全部列出来
                            else if (oceanOrderFeeList.Count != orderFeeEditPart1.feePartAR.bsFee.List.Count)
                            {
                                flg = true;
                                foreach (var item in orderFeeEditPart1.feePartAR.bsFee.List)
                                {
                                    var item1 = item as OceanBookingFeeList;
                                    _updatestring.Append("Code:" + item1.ChargingCodeName + " Amount:" + item1.Amount + item1.Currency + "<br/>");
                                }
                                foreach (var item2 in oceanOrderFeeList)
                                {
                                    _oldstring.Append("Code:" + item2.ChargingCodeName + " Amount:" + item2.Amount + item2.Currency + "<br/>");
                                }
                            }
                        }
                    }
                }
            }
            return flg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="ch"></param>
        /// <param name="SleepTime"></param>
        public void SendChar(IntPtr hand, char ch, int SleepTime)
        {
            PostMessage(hand, WM_CHAR, ch, 0);
            Thread.Sleep(SleepTime);
        }

        #endregion
    }
}

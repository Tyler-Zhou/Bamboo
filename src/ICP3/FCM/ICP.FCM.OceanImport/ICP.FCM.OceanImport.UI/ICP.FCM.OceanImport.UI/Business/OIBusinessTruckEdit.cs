using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Common;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OceanImport.UI.Report.BL;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SearchFieldConstants = ICP.FCM.Common.UI.SearchFieldConstants;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class OIBusinessTruckEdit : BaseEditPart
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

        public IOIReportDataService OIReportDataService
        {
            get
            {
                return ServiceClient.GetService<IOIReportDataService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
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

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        private UCContactAndDocumentPart ucTruckOtherPart;
        public UCContactAndDocumentPart UCTruckOtherPart
        {
            get
            {
                if (ucTruckOtherPart != null)
                {
                    return ucTruckOtherPart;
                }
                else
                {
                    ucTruckOtherPart = Workitem.Items.AddNew<UCContactAndDocumentPart>();
                    return ucTruckOtherPart;
                }
            }
        }


        #endregion

        #region 属性

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        Guid _businessID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID
        {
            get
            {
                return _businessID;
            }
            set
            {
                _businessID = value;
            }
        }
        /// <summary>
        /// 列表当前行
        /// </summary>
        public OceanImportTruckInfo ListCurrentData
        {
            get
            {
                return bsTruckInfoList.Current as OceanImportTruckInfo;
            }
        }
        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<OceanImportTruckInfo> ListDataSource
        {
            get
            {
                return bsTruckInfoList.DataSource as List<OceanImportTruckInfo>;
            }
        }

        /// <summary>
        /// 编辑行的数据
        /// </summary>
        public OceanImportTruckInfo EditDataSource
        {
            get
            {
                return bsTruckInfoEdit.DataSource as OceanImportTruckInfo;
            }
        }

        public OceanBusinessInfo businessInfo;
        public OceanBusinessMBLList mblInfo;
        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;

        /// <summary>
        /// 邮件
        /// </summary>
        Message.ServiceInterface.Message _message = null;
        public override event SavedHandler Saved;

        #endregion

        #region 窗体变量
        private bool IsDirty
        {
            get
            {
                OceanImportTruckInfo truckItem = EditDataSource;
                if (truckItem == null)
                {
                    return false;
                }

                return truckItem.IsDirty;
            }
        }

        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        private bool IsChanged
        {
            get
            {
                bool isCharge = false;
                if (IsDirty || UCBoxList.IsChanged)
                {
                    isCharge = true;
                }

                return isCharge;
            }
        }

        private bool _isCBoxListChanged = false;

        BusinessOperationContext OperationContext = new BusinessOperationContext();

        #endregion

        #region 初始化数据

        public OIBusinessTruckEdit()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode)
            {
                Disposed += delegate
                {
                    businessInfo = null;
                    mblInfo = null;
                    Saved = null;
                    gcTruck.DataSource = null;
                    gvMain.BeforeLeaveRow -= gvMain_BeforeLeaveRow;
                    bsTruckInfoEdit.DataSource = null;
                    bsTruckInfoEdit.Dispose();
                    bsTruckInfoList.CurrentChanged -= bsTruckInfoList_CurrentChanged;
                    bsTruckInfoList.DataSource = null;
                    bsTruckInfoList.Dispose();
                    SmartPartClosing -= BusinessBaseEditPart_SmartPartClosing;

                    stxtBillToID.OnOk -= stxtBillToID_OnOk;
                    stxtDeliveryAtID.OnOk -= stxtDeliveryAtID_OnOk;
                    stxtPickUpAtID.OnOk -= stxtPickUpAtID_OnOk;
                    stxtTruckerID.OnOk -= stxtTruckerID_OnOk;

                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                };
            }

            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
                }
                if (item.Key.ToUpper() == "Message".ToUpper())
                {
                    _message = item.Value as Message.ServiceInterface.Message;
                }
            }
        }

        private void SetCnText()
        {
            groupContainer.Text = "箱信息";
            barAdd.Caption = "新增(&A)";
            barClose.Caption = "关闭(&C)";
            barDelete.Caption = "删除(&D)";
            barSave.Caption = "保存(&S)";
            barPrint.Caption = "打印(&P)";
            barNew.Caption = "新增(&N)";
            barRemove.Caption = "删除(&R)";
            grbOther.Text = "联系人信息";
        }
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                UCBoxList.SetService(Workitem, false);
                //InitContols();
            }

            SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BusinessBaseEditPart_SmartPartClosing);
            ActivateSmartPartClosingEvent(Workitem);
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitContols()
        {
            //绑定编辑界面中的业务数据
            businessInfo = OceanImportService.GetBusinessInfo(BusinessID);
            mblInfo = businessInfo.MBLInfo;

            txtCarrier.Text = mblInfo.CarrierName;
            txtSubNo.Text = mblInfo.SubNo;
            txtVesselVoyage.Text = mblInfo.PreVoyageName;
            dtpETA.EditValue = businessInfo.ETA;
            txtCreateByName.Text = LocalData.UserInfo.LoginName;
            dtpCreateDate.EditValue = DateTime.Now;
            txtRemark.Text = businessInfo.Remark;

            UCBoxList.MBLID = new Guid(businessInfo.MBLID.ToString());

            ///先绑定集装箱列表，然后再绑定提货通知书，绑定提货通知书的时候，去绑定集装箱的关联信息

            ///绑定集装箱列表
            UCBoxList.BusinessID = BusinessID;
            UCBoxList.BindContainerList(businessInfo.ContainerList);

            OperationContext = GetContext(businessInfo);
            UCTruckOtherPart.SetContext = OperationContext;
            UCTruckOtherPart.BindData(OperationContext);
            UCTruckOtherPart.Dock = DockStyle.Fill;
            grbOther.Controls.Clear();
            grbOther.Controls.Add(UCTruckOtherPart);

            ///绑定提货通知书列表数据
            List<OceanImportTruckInfo> truckList = OceanImportService.GetOceanTruckServiceList(BusinessID);
            bsTruckInfoList.DataSource = truckList;

            if (truckList.Count == 0)
            {
                SetButtonEnabled(false);
            }
            gvMain.BestFitColumns();
            bsTruckInfoList.ResetBindings(false);

            //注册搜索器
            SearchRegister();
        }

        /// <summary>
        /// 搜索类型为“拖车公司”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCustoms()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Trucker, false);
            return conditions;
        }

        /// <summary>
        /// 获取类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForFinalWareHouse()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);
            return conditions;
        }

        /// <summary>
        /// 获取类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsAllType()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Airline, true);
            conditions.AddWithValue("CustomerType", CustomerType.Broker, true);
            conditions.AddWithValue("CustomerType", CustomerType.Carrier, true);
            conditions.AddWithValue("CustomerType", CustomerType.DirectClient, true);
            conditions.AddWithValue("CustomerType", CustomerType.Express, true);
            conditions.AddWithValue("CustomerType", CustomerType.Forwarding, true);
            conditions.AddWithValue("CustomerType", CustomerType.Railway, true);
            conditions.AddWithValue("CustomerType", CustomerType.Storage, true);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, true);
            conditions.AddWithValue("CustomerType", CustomerType.Trucker, true);
            conditions.AddWithValue("CustomerType", CustomerType.Warehouse, true);
            conditions.AddWithValue("CustomerType", CustomerType.Unknown, true);
            return conditions;
        }

        private void BindingData(object value)
        {
            _businessID = (Guid)value;
            InitContols();
        }

        CustomerContactFinderBridge truckerFinderBridge;

        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region Customer
            List<CountryList> countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            foreach (CountryList c in countryList)
            {
                //stxtTruckerID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtPickUpAtID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtDeliveryAtID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtBillToID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
            }

            if (EditDataSource != null)
            {
                stxtTruckerID.CustomerDescription = EditDataSource.TruckerDescription;
                stxtPickUpAtID.CustomerDescription = EditDataSource.PickUpAtDescription;
                stxtDeliveryAtID.CustomerDescription = EditDataSource.DeliveryAtDescription;
                stxtBillToID.CustomerDescription = EditDataSource.BillToDescription;
            }
            stxtTruckerID.SetLanguage(LocalData.IsEnglish);
            stxtPickUpAtID.SetLanguage(LocalData.IsEnglish);
            stxtDeliveryAtID.SetLanguage(LocalData.IsEnglish);
            stxtBillToID.SetLanguage(LocalData.IsEnglish);
            stxtPickUpAtID.OnOk += new EventHandler(stxtPickUpAtID_OnOk);

            #region 拖车行

            DataFindClientService.Register(stxtTruckerID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue, //this.GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    stxtTruckerID.Text = EditDataSource.TruckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtTruckerID.Tag = EditDataSource.TruckerID = id;
                    stxtTruckerID.SetCustomerID(id);

                    CustomerDescription _customerDescription = new CustomerDescription();
                    ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
                    stxtTruckerID.CustomerDescription = EditDataSource.TruckerDescription = _customerDescription;
                },
                delegate()
                {
                    stxtTruckerID.Text = EditDataSource.TruckerName = string.Empty;
                    stxtTruckerID.Tag = EditDataSource.TruckerID = Guid.Empty;
                    stxtTruckerID.SetCustomerID(Guid.Empty);
                    stxtTruckerID.ContactList = new List<CustomerCarrierObjects>();
                    stxtTruckerID.CustomerDescription = EditDataSource.TruckerDescription = new CustomerDescription();
                },
                ClientConstants.MainWorkspace);

            Utility.SetEnterToExecuteOnec(stxtTruckerID, delegate
            {
                List<CustomerCarrierObjects> contactList = UCTruckOtherPart.CurrentContactList.FindAll(item => item.CustomerID == EditDataSource.TruckerID);
                stxtTruckerID.SetOperationContext(OperationContext);
                truckerFinderBridge = new CustomerContactFinderBridge(stxtTruckerID, EditDataSource.TruckerDescription, contactList, ContactStage.Trk, EditDataSource.TruckerID == null ? Guid.Empty : (Guid)EditDataSource.TruckerID, true, ContactType.Customer);
                truckerFinderBridge.Init();
                stxtTruckerID.OnOk += new EventHandler(stxtTruckerID_OnOk);
                stxtTruckerID.BeforeEditValueChanged += new ChangingEventHandler(stxtTruckerID_EditValueChanging);
                stxtTruckerID.OnRefresh += new EventHandler(stxtTruckerID_OnRefresh);
                stxtTruckerID.AfterEditValueChanged += new EventHandler(stxtTruckerID_EditValueChanged);
            });

            #endregion

            #region 提货地

            DataFindClientService.Register(stxtPickUpAtID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue, //GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    stxtPickUpAtID.Text = EditDataSource.PickUpAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtPickUpAtID.Tag = EditDataSource.PickUpAtID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, EditDataSource.PickUpAtDescription);
                    stxtPickUpAtID.CustomerDescription = EditDataSource.PickUpAtDescription;
                },
                delegate()
                {
                    stxtPickUpAtID.Text = EditDataSource.PickUpAtName = string.Empty;
                    stxtPickUpAtID.Tag = EditDataSource.PickUpAtID = Guid.Empty;
                },
                ClientConstants.MainWorkspace);
            stxtDeliveryAtID.OnOk += new EventHandler(stxtDeliveryAtID_OnOk);
            #endregion

            #region 交货地

            DataFindClientService.Register(stxtDeliveryAtID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,// GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    stxtDeliveryAtID.Text = EditDataSource.DeliveryAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtDeliveryAtID.Tag = EditDataSource.DeliveryAtID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, EditDataSource.DeliveryAtDescription);
                    stxtDeliveryAtID.CustomerDescription = EditDataSource.DeliveryAtDescription;
                },
                delegate()
                {
                    stxtDeliveryAtID.Text = EditDataSource.DeliveryAtName = string.Empty;
                    stxtDeliveryAtID.Tag = EditDataSource.DeliveryAtID = Guid.Empty;
                },
                ClientConstants.MainWorkspace);

            #endregion

            #region 账单寄送

            DataFindClientService.Register(stxtBillToID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue, //this.GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    stxtBillToID.Text = EditDataSource.BillToName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtBillToID.Tag = EditDataSource.BillToID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, EditDataSource.BillToDescription);
                    stxtBillToID.CustomerDescription = EditDataSource.BillToDescription;
                },
                delegate()
                {
                    stxtBillToID.Text = EditDataSource.BillToName = string.Empty;
                    stxtBillToID.Tag = EditDataSource.BillToID = Guid.Empty;
                },
                ClientConstants.MainWorkspace);
            stxtBillToID.OnOk += new EventHandler(stxtBillToID_OnOk);
            #endregion

            #endregion
        }

        void stxtBillToID_OnOk(object sender, EventArgs e)
        {

            if (EditDataSource != null && stxtBillToID.CustomerDescription != null)
            {
                EditDataSource.BillToDescription = stxtBillToID.CustomerDescription;
            }
        }

        void stxtDeliveryAtID_OnOk(object sender, EventArgs e)
        {
            if (EditDataSource != null && stxtDeliveryAtID.CustomerDescription != null)
            {
                EditDataSource.DeliveryAtDescription = stxtDeliveryAtID.CustomerDescription;
            }
        }

        void stxtTruckerID_OnOk(object sender, EventArgs e)
        {
            if (EditDataSource == null) return;
            if (stxtTruckerID.CustomerDescription != null)
            {
                EditDataSource.TruckerDescription = stxtTruckerID.CustomerDescription;
            }

            List<CustomerCarrierObjects> currentList = stxtTruckerID.ContactList;

            if (!ArgumentHelper.GuidIsNullOrEmpty(EditDataSource.TruckerID))
            {
                UCTruckOtherPart.RemoveContactList((Guid)EditDataSource.TruckerID, ContactType.Customer);

                if (currentList.Count > 0)
                {
                    UCTruckOtherPart.InsertContactList(currentList);
                }
                SetContactList((Guid)EditDataSource.TruckerID, currentList);
            }
        }

        void stxtTruckerID_EditValueChanging(object sender, EventArgs e)
        {
            if (!ArgumentHelper.GuidIsNullOrEmpty(EditDataSource.TruckerID))
            {
                RemoveContactList((Guid)EditDataSource.TruckerID, "TruckerID");
            }
        }

        void stxtTruckerID_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (ArgumentHelper.GuidIsNullOrEmpty(EditDataSource.TruckerID))
            {
                return;
            }
            if (EditMode == EditMode.New)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanImport, businessInfo.CompanyID, (Guid)EditDataSource.TruckerID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(businessInfo.ID, OperationType.OceanImport, ContactType.Customer, ContactStage.Unknown, (Guid)EditDataSource.TruckerID);
            }
            UCTruckOtherPart.RemoveContactList((Guid)EditDataSource.TruckerID, ContactType.Customer);
            UCTruckOtherPart.InsertContactList(temp);
            SetContactList((Guid)EditDataSource.TruckerID, temp);
        }

        void stxtTruckerID_EditValueChanged(object sender, EventArgs e)
        {
            if (stxtTruckerID.CustomerDescription != null && EditDataSource != null)
            {
                EditDataSource.TruckerDescription = stxtTruckerID.CustomerDescription;
            }
        }

        void stxtPickUpAtID_OnOk(object sender, EventArgs e)
        {
            if (EditDataSource != null && stxtPickUpAtID.CustomerDescription != null)
            {
                EditDataSource.PickUpAtDescription = stxtPickUpAtID.CustomerDescription;
            }
        }

        private void SetDescription()
        {
            if (EditDataSource.TruckerDescription == null)
            {
                EditDataSource.TruckerDescription = new CustomerDescription();
            }
            if (EditDataSource.PickUpAtDescription == null)
            {
                EditDataSource.PickUpAtDescription = new CustomerDescription();
            }
            if (EditDataSource.DeliveryAtDescription == null)
            {
                EditDataSource.DeliveryAtDescription = new CustomerDescription();
            }
            if (EditDataSource.BillToDescription == null)
            {
                EditDataSource.BillToDescription = new CustomerDescription();
            }
        }

        private void SetContactList(Guid customerID, List<CustomerCarrierObjects> contactList)
        {
            if (EditDataSource.TruckerID == customerID)
            {
                stxtTruckerID.ContactList = contactList;

            }
        }

        private List<CustomerCarrierObjects> GetCurrentContactListByCustomerID(Guid customerID, ContactType contactType)
        {
            List<CustomerCarrierObjects> contactList = UCTruckOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID && item.Type == contactType);
            return contactList;
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
            List<string> relativePropertyNames = new List<string> { "TruckerID" };
            relativePropertyNames.Remove(sourcePropertyName);
            List<PropertyInfo> properties = typeof(OceanBusinessInfo).GetProperties().Where(p => relativePropertyNames.Contains(p.Name)).ToList();
            if (properties == null || properties.Count <= 0)
            {
                return;
            }
            if (!properties.Exists(p => p.GetValue(EditDataSource, null) != null && (Guid)p.GetValue(EditDataSource, null) == changeID))
            {
                UCTruckOtherPart.RemoveContactList(changeID, null);
            }

        }

        private void UpdateContactControlData()
        {
            if (stxtTruckerID.IsContactDataChanged)
            {
                stxtTruckerID.ContactList = GetCurrentContactListByCustomerID(EditDataSource.TruckerID, ContactType.Customer);
            }
        }


        #endregion

        #region 关闭、打印
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        #endregion

        #region 选择行发生改变

        /// <summary>
        /// 当前行发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsTruckInfoList_CurrentChanged(object sender, EventArgs e)
        {
            if (bsTruckInfoList.Current != null)
            {
                ///绑定编辑界面的数据
                OceanImportTruckInfo truck = bsTruckInfoList.Current as OceanImportTruckInfo;

                truck.IsDirty = false;
                bsTruckInfoEdit.DataSource = truck;
                bsTruckInfoEdit.ResetBindings(false);
                SetDescription();
                stxtTruckerID.CustomerDescription = truck.TruckerDescription;
                stxtPickUpAtID.CustomerDescription = truck.PickUpAtDescription;
                stxtDeliveryAtID.CustomerDescription = truck.DeliveryAtDescription;
                stxtBillToID.CustomerDescription = truck.BillToDescription;

                if (truck.ContainerIDList == null)
                {
                    truck.ContainerIDList = new List<Guid>();
                }

                ///关联对应的
                UCBoxList.SetRelation(truck.ContainerIDList);

                SetButtonEnabled(true);
            }
            else
            {
                OceanImportTruckInfo nullData = new OceanImportTruckInfo();
                bsTruckInfoEdit.DataSource = nullData;
                bsTruckInfoEdit.ResetBindings(false);
                UCBoxList.SetRelation(new List<Guid>());
                SetButtonEnabled(false);
            }
        }

        /// <summary>
        /// 禁用/启用工具栏按钮与面板
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetButtonEnabled(bool isEnabled)
        {
            groupBase.Enabled = isEnabled;
            groupContainer.Enabled = isEnabled;

            barDelete.Enabled = isEnabled;
            barSave.Enabled = isEnabled;
            barPrint.Enabled = isEnabled;
        }

        #endregion

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            OceanImportTruckInfo newTruck = new OceanImportTruckInfo();
            newTruck.CreateBy = LocalData.UserInfo.LoginID;
            newTruck.CreateByName = LocalData.UserInfo.LoginName;
            newTruck.CreateDate = null;
            if (mblInfo.FinalWareHouseID != null && mblInfo.FinalWareHouseID != Guid.Empty)
            {
                newTruck.PickUpAtID = (Guid)mblInfo.FinalWareHouseID;
                newTruck.PickUpAtName = mblInfo.FinalWareHouseName;
            }

            newTruck.PickUpDate = DateTime.Now;
            newTruck.Commodity = businessInfo.Commodity;
            if (businessInfo.CompanyID != Guid.Empty)
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(businessInfo.CompanyID);
                if (configureInfo != null)
                {
                    newTruck.BillToID = configureInfo.CustomerID;
                    newTruck.BillToName = configureInfo.CustomerName;
                }
            }

            SingleResult result = OceanImportService.GetRecentlyOITruckInfoList(businessInfo.CompanyID, businessInfo.CustomerID);
            if (result != null)
            {
                newTruck.TruckerID = result.GetValue<Guid?>("TruckerID") == null ? Guid.Empty : (Guid)result.GetValue<Guid?>("TruckerID");
                newTruck.TruckerName = result.GetValue<string>("TruckerName");
                newTruck.DeliveryAtID = result.GetValue<Guid?>("DeliveryAtID") == null ? Guid.Empty : (Guid)result.GetValue<Guid?>("DeliveryAtID");
                newTruck.DeliveryAtName = result.GetValue<string>("DeliveryAtName");
            }

            newTruck.IsDirty = true;
            newTruck.BeginEdit();
            bsTruckInfoList.Insert(0, newTruck);
            gvMain.FocusedRowHandle = 0;
        }

        #endregion

        #region 打印

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (EditDataSource.IsNew || EditDataSource.IsDirty)
            {
                if (Save() == false) return;
            }

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(businessInfo.CompanyID);
            if (configureInfo.SolutionID != new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanImportTruckInfo", EditDataSource);
                stateValues.Add("BusinessID", BusinessID);
                if (_message != null)
                {
                    stateValues.Add("Message", _message);
                }
                string title = (LocalData.IsEnglish ? "Delivery Order Print" : "打印提货通知书");
                PartLoader.ShowEditPart<USPickUpReport2>(Workitem, null, stateValues, title, null, null);
            }
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Delete();
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (bsTruckInfoList.Current == null) return;

            if (Utility.EnquireIsDeleteCurrentData())
            {
                OceanImportTruckInfo currentData = ListCurrentData;

                if (currentData.IsNew)
                {
                    bsTruckInfoList.RemoveCurrent();
                }
                else
                {
                    try
                    {
                        OceanImportService.RemoveOceanTruckServiceInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
                        bsTruckInfoList.RemoveCurrent();

                        //if (bsTruckInfoList.List == null || bsTruckInfoList.List.Count == 0)
                        //{
                        ////bsTruckInfoList_CurrentChanged(null, null);
                        //}
                    }
                    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
                }
            }
        }

        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Save() == true)
                {
                    SaveOtherPart();

                }
            }
        }

        private bool SaveOtherPart()
        {
            if (UCTruckOtherPart.IsChanged)
            {
                //保存联系人列表及附件
                UCTruckOtherPart.SetContext = GetContext(businessInfo);
                UCTruckOtherPart.Save(businessInfo.UpdateDate);
                UpdateContactControlData();
                if (Saved != null)
                {
                    if (_businessOperationParameter == null)
                    {
                        _businessOperationParameter = new BusinessOperationParameter();
                    }
                    Saved(null, _businessOperationParameter);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "数据保存成功");

            }
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            if (ValidateData() == false)
            {
                return false;
            }

            OceanImportTruckInfo truckItem = EditDataSource;
            if (truckItem == null)
            {
                return false;
            }
            List<HierarchyManyResult> results = new List<HierarchyManyResult>();

            try
            {
                #region 保存数据派车与集装箱数据
                TruckSaveRequest saveRequest = new TruckSaveRequest();

                saveRequest.ID = truckItem.ID;
                saveRequest.OIBookingID = BusinessID;
                saveRequest.TruckerID = truckItem.TruckerID;
                saveRequest.TruckerDescription = truckItem.TruckerDescription;
                saveRequest.NO = truckItem.NO;
                saveRequest.BillToID = truckItem.BillToID;
                saveRequest.BillToDescription = truckItem.BillToDescription;
                saveRequest.Commodity = truckItem.Commodity;
                saveRequest.DeliveryAtID = truckItem.DeliveryAtID;
                saveRequest.DeliveryAtDescription = truckItem.DeliveryAtDescription;
                saveRequest.DeliveryDate = truckItem.DeliveryDate;
                saveRequest.IsEnglish = LocalData.IsEnglish;
                saveRequest.PickUpAtID = truckItem.PickUpAtID;
                saveRequest.PickUpAtDescription = truckItem.PickUpAtDescription;
                saveRequest.PickUpDate = truckItem.PickUpDate;
                saveRequest.PickUpSendDate = truckItem.PickUpSendDate;
                saveRequest.Remark = truckItem.Remark;
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.UpdateDate = truckItem.UpdateDate;

                List<ContainerSaveRequest> boxList = null;
                if (UCBoxList.IsChanged)
                {
                    boxList = UCBoxList.GetContainerList();
                    _isCBoxListChanged = true;
                }
                Dictionary<Guid, SaveResponse> resultList = OceanImportService.SaveOIOrderWithTrans(mblInfo.ID, saveRequest, boxList);

                SaveResponse result = resultList[saveRequest.RequestId];

                #endregion

                #region 刷新数据
                //刷新数据列表
                OceanImportTruckInfo listCurrentData = ListCurrentData;
                OceanImportTruckInfo editCurrentData = EditDataSource;

                editCurrentData.ID = result.SingleResult.GetValue<Guid>("ID");
                editCurrentData.NO = result.SingleResult.GetValue<string>("No");
                editCurrentData.CreateDate = result.SingleResult.GetValue<DateTime?>("CreateDate");
                editCurrentData.UpdateDate = result.SingleResult.GetValue<DateTime?>("UpdateDate");
                editCurrentData.OIBookingID = _businessID;

                editCurrentData.IsDirty = false;

                if (editCurrentData.BillToDescription != null)
                {
                    editCurrentData.BillToDescription.IsDirty = false;
                }
                if (editCurrentData.DeliveryAtDescription != null)
                {
                    editCurrentData.DeliveryAtDescription.IsDirty = false;
                }
                if (editCurrentData.PickUpAtDescription != null)
                {
                    editCurrentData.PickUpAtDescription.IsDirty = false;
                }
                if (editCurrentData.TruckerDescription != null)
                {
                    editCurrentData.TruckerDescription.IsDirty = false;
                }

                Utility.CopyToValue(editCurrentData, listCurrentData, typeof(OceanImportTruckInfo));

                //刷新集箱列表
                if (boxList != null)
                {
                    SaveResponse.Analyze(boxList.Cast<SaveRequest>().ToList(), resultList, true);

                    UCBoxList.RefreshUI(boxList);

                    //保存集装箱关联
                    List<Guid> boxIDList = UCBoxList.GetRelation();

                    if (boxIDList.Count > 0)
                    {
                        ManyResult mamyResult = OceanImportService.SaveOIContainerAndTruck(editCurrentData.ID, boxIDList.ToArray(), LocalData.UserInfo.LoginID);

                        List<Guid> idList = new List<Guid>();
                        foreach (SingleResult singleResult in mamyResult.Items)
                        {
                            idList.Add(singleResult.GetValue<Guid>("OIContainerID"));
                        }
                        ListCurrentData.ContainerIDList = idList;
                        ListCurrentData.ContainerIDList = idList;
                    }
                }
                #endregion

                bsTruckInfoList.ResetBindings(false);
                gvMain.RefreshData();
                gvMain.BestFitColumns();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully." : "保存成功");
                if (_isCBoxListChanged && Saved != null && UCTruckOtherPart.IsChanged == false)
                {
                    if (_businessOperationParameter == null)
                    {
                        _businessOperationParameter = new BusinessOperationParameter();
                    }
                    Saved(null, _businessOperationParameter);
                }

                //IsChanged = false;
                return true;

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return false; }
        }
        #endregion

        /// <summary>
        /// 构建上下文对象
        /// </summary>
        /// <param name="businessInfo"></param>
        /// <returns></returns>
        private BusinessOperationContext GetContext(OceanBusinessInfo businessInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = businessInfo.ID;
            context.OperationNO = businessInfo.No;
            context.OperationType = OperationType.OceanImport;
            context.FormId = businessInfo.ID;
            context.FormType = FormType.Truck;
            return context;
        }

        #region 验证数据

        /// <summary>
        /// 验证数据    
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            bsTruckInfoEdit.EndEdit();
            OceanImportTruckInfo truck = EditDataSource;
            if (!truck.Validate())
            {
                return false;
            }

            if (!UCBoxList.ValidateData())
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 退出时验证保存
        /// <summary>
        /// 换行时验证是否保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (IsDirty)
            {
                DialogResult dr = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Allow = false;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        e.Allow = false;
                    }
                }
            }
        }
        #endregion

        void BusinessBaseEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (IsChanged)
            {
                DialogResult dr = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}

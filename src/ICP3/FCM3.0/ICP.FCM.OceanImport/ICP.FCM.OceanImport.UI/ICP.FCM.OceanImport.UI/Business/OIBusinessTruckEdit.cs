using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanImport.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Linq;
using ICP.Common.UI;
using ICP.FCM.OceanImport.ServiceInterface.DataObjects.ReportObjects;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.ObjectBuilder;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanImport.UI.Report;
using ICP.FCM.OceanImport.UI.Report.BL;



namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIBusinessTruckEdit : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public IOIReportDataService OIReportSrvice { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        [ServiceDependency]
        public IOceanImportService oiImportService { get;set;}

        [ServiceDependency]
        public IReportViewService ReportViewService { get; set; }

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
                base.DataSource = value;
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
                if (_businessID == Guid.Empty)
                { 
                   _businessID= new Guid(DataSource.ToString());
                }
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
                return this.bsTruckInfoList.Current as OceanImportTruckInfo;
            }
        }
        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<OceanImportTruckInfo> ListDataSource
        {
            get
            {
                return this.bsTruckInfoList.DataSource as List<OceanImportTruckInfo>;
            }
        }

        /// <summary>
        /// 编辑行的数据
        /// </summary>
        public OceanImportTruckInfo EditDataSource
        {
            get
            {
                return this.bsTruckInfoEdit.DataSource as OceanImportTruckInfo;
            }
        }

        public OceanBusinessInfo businessInfo;
        public OceanBusinessMBLList mblInfo;
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
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
                if (IsDirty || this.UCBoxList.IsChanged)
                {
                    isCharge = true;
                }

                return isCharge;
            }
        }

        private bool _isCBoxListChanged = false;

        #endregion

        #region 初始化数据

        public OIBusinessTruckEdit()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (!LocalData.IsEnglish)
            {
                SetCnText();
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
                this.UCBoxList.SetService(Workitem);
                InitContols();

            }

            this.SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BusinessBaseEditPart_SmartPartClosing);
            this.ActivateSmartPartClosingEvent(this.Workitem);
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitContols()
        {
            //绑定编辑界面中的业务数据
             businessInfo= oiImportService.GetBusinessInfo(BusinessID);

             if (businessInfo.MBLID != null)
             {
                 mblInfo = oiImportService.GetOIMBLInfo(new Guid(businessInfo.MBLID.ToString()));

             }
             else
             {
                 mblInfo = new OceanBusinessMBLList();
             }

            this.txtCarrier.Text = mblInfo.CarrierName;
            this.txtSubNo.Text = mblInfo.SubNo;
            this.txtVesselVoyage.Text = mblInfo.PreVoyageName;
            this.dtpETA.EditValue = businessInfo.ETA;
            this.txtCreateByName.Text = LocalData.UserInfo.LoginName;
            this.dtpCreateDate.EditValue = DateTime.Now;


            this.UCBoxList.MBLID = new Guid(businessInfo.MBLID.ToString());

            ///先绑定集装箱列表，然后再绑定提货通知书，绑定提货通知书的时候，去绑定集装箱的关联信息

            ///绑定集装箱列表
            this.UCBoxList.BusinessID = BusinessID;
            this.UCBoxList.BindContainerList();



            ///绑定提货通知书列表数据
            List<OceanImportTruckInfo>  truckList=oiImportService.GetOceanTruckServiceList(BusinessID);
            bsTruckInfoList.DataSource = truckList;

            if (truckList.Count == 0)
            {
                SetButtonEnabled(false);
            }
            ////else
            ////{
            ////    bsTruckInfoList_CurrentChanged(null, null);
            ////}

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


        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region Customer
            List<CountryList> countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            foreach (CountryList c in countryList)
            {
                stxtTruckerID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtPickUpAtID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtDeliveryAtID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                stxtBillToID.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
            }

            if (EditDataSource != null)
            {
                this.stxtTruckerID.CustomerDescription = EditDataSource.TruckerDescription;
                this.stxtPickUpAtID.CustomerDescription = EditDataSource.PickUpAtDescription;
                this.stxtDeliveryAtID.CustomerDescription = EditDataSource.DeliveryAtDescription;
                this.stxtBillToID.CustomerDescription = EditDataSource.BillToDescription;
            }
            this.stxtTruckerID.SetLanguage(LocalData.IsEnglish);
            this.stxtPickUpAtID.SetLanguage(LocalData.IsEnglish);
            this.stxtDeliveryAtID.SetLanguage(LocalData.IsEnglish);
            this.stxtBillToID.SetLanguage(LocalData.IsEnglish);
            stxtPickUpAtID.OnOk += new EventHandler(stxtPickUpAtID_OnOk);

            #region 拖车行

            dfService.Register(this.stxtTruckerID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue, //this.GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtTruckerID.Text = EditDataSource.TruckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtTruckerID.Tag = EditDataSource.TruckerID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, EditDataSource.TruckerDescription);
                    this.stxtTruckerID.CustomerDescription = EditDataSource.TruckerDescription;
                },
                delegate()
                {
                    this.stxtTruckerID.Text = EditDataSource.TruckerName = string.Empty;
                    this.stxtTruckerID.Tag = EditDataSource.TruckerID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            stxtTruckerID.OnOk += new EventHandler(stxtTruckerID_OnOk);
            #endregion

            #region 提货地

            dfService.Register(this.stxtPickUpAtID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue, //GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtPickUpAtID.Text = EditDataSource.PickUpAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtPickUpAtID.Tag = EditDataSource.PickUpAtID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, EditDataSource.PickUpAtDescription);
                    this.stxtPickUpAtID.CustomerDescription = EditDataSource.PickUpAtDescription;
                },
                delegate()
                {
                    this.stxtPickUpAtID.Text = EditDataSource.PickUpAtName = string.Empty;
                    this.stxtPickUpAtID.Tag = EditDataSource.PickUpAtID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            stxtDeliveryAtID.OnOk += new EventHandler(stxtDeliveryAtID_OnOk);
            #endregion

            #region 交货地

            dfService.Register(this.stxtDeliveryAtID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,// GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtDeliveryAtID.Text = EditDataSource.DeliveryAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtDeliveryAtID.Tag = EditDataSource.DeliveryAtID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, EditDataSource.DeliveryAtDescription);
                    this.stxtDeliveryAtID.CustomerDescription = EditDataSource.DeliveryAtDescription;
                },
                delegate()
                {
                    this.stxtDeliveryAtID.Text = EditDataSource.DeliveryAtName = string.Empty;
                    this.stxtDeliveryAtID.Tag = EditDataSource.DeliveryAtID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region 账单寄送

            dfService.Register(this.stxtBillToID, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue, //this.GetConditionsAllType,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this.stxtBillToID.Text = EditDataSource.BillToName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    this.stxtBillToID.Tag = EditDataSource.BillToID = id;
                    ICPCommUIHelper.SetCustomerDesByID(id, EditDataSource.BillToDescription);
                    this.stxtBillToID.CustomerDescription = EditDataSource.BillToDescription;
                },
                delegate()
                {
                    this.stxtBillToID.Text = EditDataSource.BillToName = string.Empty;
                    this.stxtBillToID.Tag = EditDataSource.BillToID = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
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
            if (EditDataSource != null && stxtTruckerID.CustomerDescription != null)
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

        #endregion 

        #region 关闭、打印
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
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
            //if(IsDirty)
            //{

            //}

            if (bsTruckInfoList.Current != null)
            {
                ///绑定编辑界面的数据
                OceanImportTruckInfo truck = bsTruckInfoList.Current as OceanImportTruckInfo;

                truck.IsDirty = false;
                bsTruckInfoEdit.DataSource = truck;
                bsTruckInfoEdit.ResetBindings(false);
                SetDescription();
                this.stxtTruckerID.CustomerDescription = truck.TruckerDescription;
                this.stxtPickUpAtID.CustomerDescription = truck.PickUpAtDescription;
                this.stxtDeliveryAtID.CustomerDescription = truck.DeliveryAtDescription;
                this.stxtBillToID.CustomerDescription = truck.BillToDescription;

                if (truck.ContainerIDList == null)
                {
                    truck.ContainerIDList = new List<Guid>();
                }

                ///关联对应的
                this.UCBoxList.SetRelation(truck.ContainerIDList);
            
                SetButtonEnabled(true);
            }
            else
            {
                OceanImportTruckInfo nullData = new OceanImportTruckInfo();
                bsTruckInfoEdit.DataSource = nullData;           
                bsTruckInfoEdit.ResetBindings(false);
                this.UCBoxList.SetRelation(new List<Guid>());
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

            this.barDelete.Enabled = isEnabled;
            this.barSave.Enabled = isEnabled;
            this.barPrint.Enabled = isEnabled;
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
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(businessInfo.CompanyID);
                if (configureInfo != null)
                {
                    newTruck.BillToID = configureInfo.CustomerID;
                    newTruck.BillToName = configureInfo.CustomerName;
                }
            }

            SingleResult result = oiImportService.GetRecentlyOITruckInfoList(businessInfo.CompanyID, businessInfo.CustomerID);
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

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (EditDataSource.IsNew || EditDataSource.IsDirty)
            {
                if (Save() == false) return;
            }

            ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(businessInfo.CompanyID);          
            if (configureInfo.SolutionID != new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanImportTruckInfo", EditDataSource);
                stateValues.Add("BusinessID", BusinessID);
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
                    //if (bsTruckInfoList.List == null || bsTruckInfoList.List.Count == 0)
                    //{
                    ////bsTruckInfoList_CurrentChanged(null, null);
                    //}
                }
                else
                {
                    try
                    {
                        oiImportService.RemoveOceanTruckServiceInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
                        bsTruckInfoList.RemoveCurrent();

                        //if (bsTruckInfoList.List == null || bsTruckInfoList.List.Count == 0)
                        //{
                           ////bsTruckInfoList_CurrentChanged(null, null);
                        //}
                    }
                    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
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
            Save();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            if (this.ValidateData() == false)
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
                if(UCBoxList.IsChanged)
                {
                    boxList = this.UCBoxList.GetContainerList();
                    _isCBoxListChanged = true;
                }


                Dictionary<Guid, SaveResponse> resultList = oiImportService.SaveOIOrderWithTrans(mblInfo.ID, saveRequest, boxList);

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

                Utility.CopyToValue(editCurrentData,listCurrentData, typeof(OceanImportTruckInfo));

                //刷新集箱列表
                if (boxList != null)
                {
                    SaveResponse.Analyze(boxList.Cast<SaveRequest>().ToList(), resultList, true);

                    this.UCBoxList.RefreshUI(boxList);

                    //保存集装箱关联
                    List<Guid> boxIDList = this.UCBoxList.GetRelation();

                    if (boxIDList.Count > 0)
                    {
                       ManyResult mamyResult=oiImportService.SaveOIContainerAndTruck(editCurrentData.ID, boxIDList.ToArray(), LocalData.UserInfo.LoginID);

                        List<Guid> idList=new List<Guid>();
                        foreach (SingleResult singleResult in mamyResult.Items)
                        {
                            idList.Add(singleResult.GetValue<Guid>("OIContainerID"));   
                        }
                        ListCurrentData.ContainerIDList = idList;
                        ListCurrentData.ContainerIDList = idList;


                    }
                }


                #endregion

                this.bsTruckInfoList.ResetBindings(false);
                this.gvMain.RefreshData();
                this.gvMain.BestFitColumns();


                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully." : "保存成功");
                if (_isCBoxListChanged && Saved != null)
                {
                    Saved(null);
                }

                //IsChanged = false;
                return true;
                    
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }
        #endregion

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

            if (!this.UCBoxList.ValidateData())
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
        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
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
                    if (!this.Save())
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
                    if (!this.Save())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}

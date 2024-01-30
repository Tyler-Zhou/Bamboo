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
using ICP.FCM.AirImport.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.AirImport.UI.Common;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.AirImport.UI
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

        //[ServiceDependency]
        //public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }

        //[ServiceDependency]
        //public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        //[ServiceDependency]
        //public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public IAirImportService oiImportService { get;set;}

        //[Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        //public ICPCommUIHelper ICPCommUIHelper { get; set; }


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
        public AirImportTruckInfo ListCurrentData
        {
            get
            {
                return this.bsTruckInfoList.Current as AirImportTruckInfo;
            }
        }
        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<AirImportTruckInfo> ListDataSource
        {
            get
            {
                return this.bsTruckInfoList.DataSource as List<AirImportTruckInfo>;
            }
        }

        /// <summary>
        /// 编辑行的数据
        /// </summary>
        public AirImportTruckInfo EditDataSource
        {
            get
            {
                return this.bsTruckInfoEdit.DataSource as AirImportTruckInfo;
            }
        }

        public AirBusinessInfo businessInfo;
        public AirBusinessMBLList _mblInfo;
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        #endregion

        #region 窗体变量

        private bool IsDirty
        {
            get
            {
                AirImportTruckInfo truckItem = EditDataSource;
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
                if (IsDirty)
                {
                    isCharge = true;
                }

                return isCharge;
            }
        }

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
                 _mblInfo = oiImportService.GetAIMBLInfo(new Guid(businessInfo.MBLID.ToString()));

             }
             else
             {
                 _mblInfo = new AirBusinessMBLList();
             }

            this.txtAirCompany.Text = _mblInfo.AirCompanyName;
            this.txtSubNo.Text = _mblInfo.SubNo;
            this.txtFlightNo.Text = _mblInfo.FlightNo;
            this.dtpETA.EditValue = businessInfo.ETA;
            this.txtCreateByName.Text = LocalData.UserInfo.LoginName;
            this.dtpCreateDate.EditValue = DateTime.Now;


            //绑定提货通知书列表数据
            List<AirImportTruckInfo>  truckList=oiImportService.GetAirTruckServiceList(BusinessID);
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
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region 拖车行
           
            this.dfService.Register(this.stxtTruckerID, CommonFinderConstants.CustoemrFinder,
                ICP.FCM.AirImport.UI.Common.SearchConstants.CodeName, ICP.FCM.AirImport.UI.Common.SearchConstants.ResultValue,
                this.GetConditionsForCustoms,
                delegate(object inputSource, object[] resultData)
                {
                    stxtTruckerID.Tag = EditDataSource.TruckerID = (Guid)resultData[0];
                    stxtTruckerID.EditValue = EditDataSource.TruckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    stxtTruckerID.Tag = EditDataSource.TruckerID = Guid.Empty;
                    stxtTruckerID.EditValue = EditDataSource.TruckerName = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region 提货地
         
            this.dfService.Register(this.stxtPickUpAtID, CommonFinderConstants.CustoemrFinder,
                SearchConstants.CodeName, SearchConstants.ResultValue,
                GetConditionsForFinalWareHouse,
                delegate(object inputSouce, object[] resultData)
                {
                    stxtPickUpAtID.Tag = EditDataSource.PickUpAtID = (Guid)resultData[0];
                    stxtPickUpAtID.EditValue = EditDataSource.PickUpAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    this.stxtPickUpAtID.Tag = EditDataSource.PickUpAtID = Guid.Empty;
                    this.stxtPickUpAtID.EditValue = EditDataSource.PickUpAtName = string.Empty;
                },
            ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region 交货地

            //LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(this.stxtDeliveryAtID, this.dfService, true);
            this.dfService.Register(this.stxtDeliveryAtID, CommonFinderConstants.CustoemrFinder,
               ICP.FCM.AirImport.UI.Common.SearchConstants.CodeName, ICP.FCM.AirImport.UI.Common.SearchConstants.ResultValue,
               GetConditionsForFinalWareHouse,
               delegate(object inputSouce, object[] resultData)
               {
                   stxtDeliveryAtID.Tag = EditDataSource.DeliveryAtID = (Guid)resultData[0];
                   stxtDeliveryAtID.EditValue = EditDataSource.DeliveryAtName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
               },
               delegate
               {
                   this.stxtDeliveryAtID.Tag = EditDataSource.DeliveryAtID = Guid.Empty;
                   this.stxtDeliveryAtID.EditValue = EditDataSource.DeliveryAtName = string.Empty;
               },
           ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            

            #endregion

            #region 账单寄送
       
            this.dfService.Register(this.stxtBillToID, CommonFinderConstants.CustoemrFinder,
               ICP.FCM.AirImport.UI.Common.SearchConstants.CodeName, ICP.FCM.AirImport.UI.Common.SearchConstants.ResultValue,
               this.GetConditionsForCustoms,
               delegate(object inputSource, object[] resultData)
               {
                   stxtBillToID.Tag = EditDataSource.BillToID = (Guid)resultData[0];
                   stxtBillToID.EditValue = EditDataSource.BillToName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
               },
               delegate
               {
                   stxtBillToID.Tag = EditDataSource.BillToID = Guid.Empty;
                   stxtBillToID.EditValue = EditDataSource.BillToName = string.Empty;
               },
               ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            

            #endregion
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
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (bsTruckInfoList.Current != null)
                {
                    //绑定编辑界面的数据
                    AirImportTruckInfo truck = bsTruckInfoList.Current as AirImportTruckInfo;

                    truck.IsDirty = false;
                    bsTruckInfoEdit.DataSource = truck;
                    bsTruckInfoEdit.ResetBindings(false);

                    SetButtonEnabled(true);
                }
                else
                {
                    AirImportTruckInfo nullData = new AirImportTruckInfo();
                    bsTruckInfoEdit.DataSource = nullData;
                    bsTruckInfoEdit.ResetBindings(false);
                    SetButtonEnabled(false);
                }
            }
        }

        /// <summary>
        /// 禁用/启用工具栏按钮与面板
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetButtonEnabled(bool isEnabled)
        {
            groupBase.Enabled = isEnabled;
           
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
            using (new CursorHelper(Cursors.WaitCursor))
            {
                AirImportTruckInfo newTruck = new AirImportTruckInfo();
                newTruck.CreateBy = LocalData.UserInfo.LoginID;
                newTruck.CreateByName = LocalData.UserInfo.LoginName;
                newTruck.CreateDate = null;
                if (_mblInfo.FinalWareHouseID != null && _mblInfo.FinalWareHouseID != Guid.Empty)
                {
                    newTruck.PickUpAtID = (Guid)_mblInfo.FinalWareHouseID;
                    newTruck.PickUpAtName = _mblInfo.FinalWareHouseName;
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

                SingleResult result = oiImportService.GetRecentlyAITruckInfoList(businessInfo.CompanyID, businessInfo.CustomerID);
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
                AirImportTruckInfo currentData = ListCurrentData;

                if (currentData.IsNew)
                {
                    bsTruckInfoList.RemoveCurrent();                  
                }
                else
                {
                    try
                    {
                        oiImportService.RemoveAirTruckServiceInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
                        bsTruckInfoList.RemoveCurrent();
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delect Successfully!" : "删除成功!");             
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
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
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

            AirImportTruckInfo truckItem = EditDataSource;
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
                saveRequest.NO = truckItem.NO;
                saveRequest.BillToID = truckItem.BillToID;
                saveRequest.Commodity = truckItem.Commodity;
                saveRequest.DeliveryAtID = truckItem.DeliveryAtID;
                saveRequest.DeliveryDate = truckItem.DeliveryDate;
                saveRequest.IsEnglish = LocalData.IsEnglish;
                saveRequest.PickUpAtID = truckItem.PickUpAtID;
                saveRequest.PickUpDate = truckItem.PickUpDate;
                saveRequest.PickUpSendDate = truckItem.PickUpSendDate;

                saveRequest.PickUpRefNo = truckItem.PickUpRefNo;
                saveRequest.PickUpContact = truckItem.PickUpContact;
                saveRequest.DeliveryNo = truckItem.DeliveryNo;
                saveRequest.DeliveryContact = truckItem.DeliveryContact;
                saveRequest.IsDrivingLicence = truckItem.IsDrivingLicence;
                saveRequest.Remark = truckItem.Remark;
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.UpdateDate = truckItem.UpdateDate;

                Dictionary<Guid, SaveResponse> resultList = oiImportService.SaveAIOrderWithTrans(_mblInfo.ID, saveRequest);
                SaveResponse result = resultList[saveRequest.RequestId];

                #endregion

                #region 刷新数据
                //刷新数据列表
                AirImportTruckInfo listCurrentData = ListCurrentData;
                AirImportTruckInfo editCurrentData = EditDataSource;

                editCurrentData.ID = result.SingleResult.GetValue<Guid>("ID");
                editCurrentData.NO = result.SingleResult.GetValue<string>("No");
                editCurrentData.CreateDate = result.SingleResult.GetValue<DateTime?>("CreateDate");
                editCurrentData.UpdateDate = result.SingleResult.GetValue<DateTime?>("UpdateDate");
                editCurrentData.IsDirty = false;

                Utility.CopyToValue(editCurrentData,listCurrentData, typeof(AirImportTruckInfo));

                #endregion

                this.bsTruckInfoList.ResetBindings(false);
                this.gvMain.RefreshData();
                this.gvMain.BestFitColumns();


                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully." : "保存成功");
                if (Saved != null)
                {
                    Saved(null);
                }

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
            AirImportTruckInfo truck = EditDataSource;
            if (!truck.Validate())
            {
                return false;
            }         

            return true;
        }

        #endregion

        #region 打印

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (EditDataSource == null || EditDataSource.ID == Guid.Empty) return;
                if (IsChanged)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                if (LocalData.IsEnglish)
                {
                    //AirImportPrintHelper.PrintPickupEN(EditDataSource.ID);
                }
                else
                {
                    //AirImportPrintHelper.PrintPickupCN(EditDataSource.ID);
                }
            }
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (EditDataSource == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = EditDataSource.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = EditDataSource.ID;

            return message;
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

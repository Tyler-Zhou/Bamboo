using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Crawler.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.Business.Common.UI
{
    public partial class UCOICargoTracking : BaseEditPart, IDataBind
    {
        System.Threading.Thread threadGetTerminalList;
        private static List<ICP.Crawler.ServiceInterface.DataObjects.Terminals> terminalList = new List<ICP.Crawler.ServiceInterface.DataObjects.Terminals>();
        /// <summary>
        /// 箱信息
        /// </summary>
        public CargoTrackingContainerInfo CurrentCargoTrackingContainer
        {
            get
            {
                return bsContainerinfo.Current as CargoTrackingContainerInfo;
            }
        }
        public UCOICargoTracking()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                InitControls();
                Locale();
                this.Disposed += new EventHandler(UCOICargoTracking_Disposed);
            }
            //try
            //{
            //    threadGetTerminalList = new System.Threading.Thread(GetTerminalList);
            //    threadGetTerminalList.Start();
            //}
            //catch (Exception ex)
            //{
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            //}
        }

        private void Locale()
        {
            if (!LocalData.IsDesignMode && LocalData.IsEnglish)
            {
                colStatus.Caption = "Current state";
                btnAvailability.Caption = "Terminal Availability";
                colAvailability.Caption = "Terminal Availability";
            }
        }

        private void GetTerminalList()
        {
            if (DataSource != null)
            {
                string vessel = DataSource.Voyage.Substring(0, DataSource.Voyage.IndexOf('/'));
                terminalList = TerminalService.GeTerminalByContainerList(CurrentCargoTrackingContainer.ContainerID, vessel, DataSource.PODCode, DataSource.PickUpID);
            }
        }

        void UCOICargoTracking_Disposed(object sender, EventArgs e)
        {
            if (warehouseFinder != null)
            {
                warehouseFinder.Dispose();
                warehouseFinder = null;
            }
            this.bsContainerinfo.DataSource = null;
            this.bsOperationinfo.DataSource = null;
            this.Saved = null;
        }

        #region 服务

        //static ITerminalService terminalService = ServiceProxyFactory.Create<ITerminalService>("TerminalService");
        //static ICrawlerService crawlerService = ServiceProxyFactory.Create<ICrawlerService>("CrawlerService");
        public WorkItem Workitem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        private ITerminalService TerminalService
        {
            get
            {
                return ServiceClient.GetService<ITerminalService>();
            }
        }
        private ICrawlerService CrawlerService
        {
            get
            {
                return ServiceClient.GetService<ICrawlerService>();
            }
        }
        private IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        public IFCMCommonService FCMCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IICPCommonOperationService>();
            }
        }

        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }
        }

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }
        #endregion

        BusinessOperationContext context;
        CargoTrackingInfo cargotracking = new CargoTrackingInfo();
        CargoTrackingInfo oldCargoTrackingInfo = new CargoTrackingInfo();

        /// <summary>
        /// 绑定数据源
        /// </summary>
        public CargoTrackingInfo DataSource
        {
            get { return bsOperationinfo.DataSource as CargoTrackingInfo; }
            set { BindingData(value); }
        }
        void BindingData(object data)
        {

            if (data == null)
            {
                this.bsOperationinfo.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.CargoTrackingInfo);

                this.bsContainerinfo.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.CargoTrackingContainerInfo);
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                context = data as BusinessOperationContext;

                if (context.OperationID != null && context.OperationID != Guid.Empty)
                {
                    this.cargotracking = FCMCommonService.GetOperationContainersInfo(context.OperationID, context.OperationType);
                    oldCargoTrackingInfo = FCMCommonService.GetOperationContainersInfo(context.OperationID, context.OperationType);
                }
                if (cargotracking == null)
                {
                    this.cargotracking = new CargoTrackingInfo();
                }
                this.bsOperationinfo.DataSource = this.cargotracking;
                this.bsOperationinfo.ResetBindings(false);

                this.bsContainerinfo.DataSource = cargotracking.Containers;
                this.bsContainerinfo.ResetBindings(false);

            }

            SearchRegister();

            //SetTerminalList();

        }

        private void InitControls()
        {
            ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
            if (this.cargotracking.VoyageID != null)
            {
                this.stxtVoyage.ShowSelectedValue(this.cargotracking.VoyageID, this.cargotracking.Voyage);
            }
            if (this.cargotracking.CarrierID != null)
            {
                this.mcmbCarrier.ShowSelectedValue(this.cargotracking.CarrierID, this.cargotracking.Carrier);
            }
            this.Saved += new SavedHandler(ContainerSaved);
            if (!LocalData.IsEnglish)
            {
                this.barSave.Caption = "保存";
            }

            this.dtETA.EditValueChanged += new EventHandler(dtpETA_EditValueChanged);

            this.dtDETA.EditValueChanged += new EventHandler(dtpDETA_EditValueChanged);
        }

        void ContainerSaved(object[] prams)
        {
            if (prams != null)
            {
                BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
                BusinessOperationContext operationContext = new BusinessOperationContext();
                operationContext.OperationID = context.OperationID;
                operationContext.OperationNO = context.OperationNO;
                businessOperationParameter.Context = operationContext;
                ICPCommonOperationService.AfterContainerInfoSaved(businessOperationParameter);
            }
        }
        private IDisposable warehouseFinder;
        void SearchRegister()
        {
            //提货地
            warehouseFinder = this.DataFindClientService.Register(this.stxtWarehouse, CommonFinderConstants.CustoemrFinder,
                 ICP.Common.UI.SearchFieldConstants.CodeName, ICP.Common.UI.SearchFieldConstants.ResultValue,
                  GetConditionsForWarehouse,
                  delegate(object inputSouce, object[] resultData)
                  {
                      stxtWarehouse.Tag = this.cargotracking.PickUpID = new Guid(resultData[0].ToString());
                      stxtWarehouse.EditValue = this.cargotracking.PickUpPlace = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  },
                  delegate
                  {
                      stxtWarehouse.Tag = this.cargotracking.PickUpID = Guid.Empty;
                      stxtWarehouse.EditValue = this.cargotracking.PickUpPlace = string.Empty;
                  },
              ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        /// <summary>
        /// “提货地”是类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForWarehouse()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);

            return conditions;
        }

        void dtpETA_EditValueChanged(object sender, EventArgs e)
        {
            SetDETA();
        }

        void dtpDETA_EditValueChanged(object sender, EventArgs e)
        {
            SetFETA();
        }


        /// <summary>
        /// 设置默认DETA
        /// </summary>
        private void SetDETA()
        {
            DateTime? eta = this.dtETA.DateTime;
            if (eta == null || eta == DateTime.MinValue || eta == DateTime.MaxValue)
            {
                eta = cargotracking.ETA;
            }
            ////输入的时候，ID是空，只能要怕NAME去判断了
            if (cargotracking.ETA != null && cargotracking.PlaceOfDelivery == cargotracking.POD)
            {
                this.dtDETA.DateTime = Convert.ToDateTime(cargotracking.DETA = cargotracking.ETA = eta);
            }

            if (cargotracking.ETA != null && cargotracking.FinalDestination == cargotracking.POD)
            {
                this.dtFETA.DateTime = Convert.ToDateTime(cargotracking.FETA = cargotracking.ETA = eta);
            }
        }


        private void SetFETA()
        {
            DateTime? deta = this.dtDETA.DateTime;
            if (deta == null || deta == DateTime.MinValue || deta == DateTime.MaxValue)
            {
                deta = cargotracking.DETA;
            }

            //改变DETA时，如果最终目的地=交货地，那么FETA=DETA
            if (cargotracking.DETA != null && cargotracking.FinalDestination == cargotracking.PlaceOfDelivery)
            {
                this.dtFETA.DateTime = Convert.ToDateTime(cargotracking.FETA = cargotracking.DETA = deta);
            }
        }

        #region IDataBind 成员

        public void ControlsReadOnly(bool flg)
        {
            //throw new NotImplementedException();
        }

        public void DataBind(BusinessOperationContext business)
        {
            BindingData(business);
        }

        #endregion

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        private bool Save()
        {
            try
            {
                bsOperationinfo.EndEdit();
                gvBoxList.PostEditor();
                bsContainerinfo.EndEdit();
                CargoTrackingSaveRequest cargoTrackingSaveRequest = new CargoTrackingSaveRequest();
                cargoTrackingSaveRequest.OperationID = this.cargotracking.OperationID;
                cargoTrackingSaveRequest.CarrierID = this.cargotracking.CarrierID;
                cargoTrackingSaveRequest.VoyageID = this.cargotracking.VoyageID;
                cargoTrackingSaveRequest.ETD = this.cargotracking.ETD;
                cargoTrackingSaveRequest.ETA = this.cargotracking.ETA == null ? dtETA.DateTime : this.cargotracking.ETA;
                cargoTrackingSaveRequest.DETA = this.cargotracking.DETA;
                cargoTrackingSaveRequest.FETA = this.cargotracking.FETA;
                cargoTrackingSaveRequest.PickUpID = this.cargotracking.PickUpID;
                cargoTrackingSaveRequest.UpdateDate = this.cargotracking.UpdateDate;
                cargoTrackingSaveRequest.ContainerIds = this.cargotracking.Containers.Select(o => o.ContainerID).ToList();
                cargoTrackingSaveRequest.LastFreeDates = this.cargotracking.Containers.Select(o => o.LastFreeDate).ToList();
                cargoTrackingSaveRequest.PickUpDates = this.cargotracking.Containers.Select(o => o.PickUpDate).ToList();
                cargoTrackingSaveRequest.PickUpNos = this.cargotracking.Containers.Select(o => o.PickUpNo).ToList();
                cargoTrackingSaveRequest.ReturnDates = this.cargotracking.Containers.Select(o => o.ReturnDate).ToList();
                cargoTrackingSaveRequest.AvailableDates = this.cargotracking.Containers.Select(o => o.AvailableDate).ToList();
                //cargoTrackingSaveRequest.AvailableTimes = this.cargotracking.Containers.Select(o => o.AvailableTime).ToList();
                cargoTrackingSaveRequest.DeliveryTimes = this.cargotracking.Containers.Select(o => o.DeliveryTime).ToList();

                SingleResult result = this.FCMCommonService.SaveOperationContainersInfo(cargoTrackingSaveRequest);
                //当前业务的ANSC勾上以后，才会去判断是否有改变数据做对应的处理(ANSC 发送到港通知书给客户)

                OiAftertheSave(oldCargoTrackingInfo, cargotracking, oldCargoTrackingInfo.OperationID);

                this.cargotracking.OperationID = result.GetValue<Guid>("ID");
                this.cargotracking.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                if (Saved != null) Saved(this.cargotracking);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return false;
            }
        }


        #region 箱列表 保存成功后操作
        /// <summary>
        /// 箱列表 保存成功后操作
        /// </summary>
        public void OiAftertheSave(CargoTrackingInfo oldcCargoTrackingInfo, CargoTrackingInfo newCargoTrackingInfo, Guid operationId)
        {
            StringBuilder content = new StringBuilder();
            int pickUpDateCount = 0;
            int returnDateCount = 0;
            int deliveryTimeCount = 0;
            if (oldcCargoTrackingInfo.ETA != newCargoTrackingInfo.ETA)
            {

                if (!string.IsNullOrEmpty("" + context["ANSC"]) && bool.Parse(context["ANSC"].ToString()))
                {
                    ServiceClient.GetClientService<IICPCommonOperationService>().MailEtachange(operationId);
                }
            }
            List<CargoTrackingContainerInfo> cargoTrackingContainer = oldcCargoTrackingInfo.Containers;
            List<CargoTrackingContainerInfo> newCargoTrackingContainerInfos = newCargoTrackingInfo.Containers;

            foreach (CargoTrackingContainerInfo old in cargoTrackingContainer)
            {
                foreach (CargoTrackingContainerInfo news in newCargoTrackingContainerInfos)
                {
                    if (news.ContainerNO == old.ContainerNO)
                    {
                        if (old.PickUpDate != news.PickUpDate)
                        {
                            pickUpDateCount++;
                            Saveevents("FCP", news.ContainerNO, operationId);
                        }
                        if (old.ReturnDate != news.ReturnDate)
                        {
                            returnDateCount++;
                            Saveevents("ECR", news.ContainerNO, operationId);
                        }
                        if (old.DeliveryTime != news.DeliveryTime)
                        {
                            deliveryTimeCount++;
                            Saveevents("FCD", news.ContainerNO, operationId);
                        }
                        if (old.LastFreeDate != news.LastFreeDate)
                        {
                            if (news.LastFreeDate != null)
                            {
                                DateTime LFDate = (DateTime)news.LastFreeDate;
                                content.Append(news.ContainerNO + ":" + LFDate.ToString("yyyy-MM-dd") + "<br/>");
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(content.ToString()))
            {
                if (!string.IsNullOrEmpty(""+context["ANSC"]) && bool.Parse(context["ANSC"].ToString()))
                {
                    ServiceClient.GetClientService<IICPCommonOperationService>()
                        .MailLfdnotice(operationId, string.Empty, content.ToString());
                }
            }
            if (pickUpDateCount > 0)
            {
                pickUpDateCount = pickUpDateCount == cargoTrackingContainer.Count ? 3 : 2;
                Updatestate("FCP", pickUpDateCount, operationId);
            }
            if (returnDateCount > 0)
            {
                returnDateCount = returnDateCount == cargoTrackingContainer.Count ? 3 : 2;
                Updatestate("ECR", returnDateCount, operationId);
            }
            if (deliveryTimeCount > 0)
            {
                deliveryTimeCount = deliveryTimeCount == cargoTrackingContainer.Count ? 3 : 2;
                Updatestate("FCD", deliveryTimeCount, operationId);
            }
        }

        /// <summary>
        /// 产生事件
        /// </summary>
        /// <param name="eventsCode">事件Code</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="operationId"></param>
        public void Saveevents(string eventsCode, string containerNo, Guid operationId)
        {
            var memoList = FCMCommonService.GetMemoList(operationId, null).FirstOrDefault(n => n.Code == eventsCode);
            if (memoList == null)
            {
                EventCode eventCode = EventCodeList(eventsCode);
                var eventObjects = new EventObjects
                {
                    OperationID = operationId,
                    OperationType = OperationType.OceanImport,
                    FormID = operationId,
                    FormType = FormType.Unknown,
                    Code = eventCode.Code,
                    Description = "[" + containerNo + "]" + eventCode.Subject,
                    Subject = eventCode.Subject,
                    Priority = MemoPriority.Normal,
                    UpdateDate = DateTime.Now,
                    Owner = LocalData.UserInfo.LoginName,
                    UpdateBy = LocalData.UserInfo.LoginID,
                    CategoryName = eventCode.Category,
                    IsShowAgent = true,
                    IsShowCustomer = true,
                    Type = MemoType.EmailLog
                };
                FCMCommonService.SaveMemoInfo(eventObjects);
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        public void Updatestate(string action, int updateVlues, Guid operationId)
        {
            if (!string.IsNullOrEmpty(action) && updateVlues > 1)
            {
                List<BusinessSaveParameter> listBusinessParameter = new List<BusinessSaveParameter>();
                BusinessSaveParameter parameter = new BusinessSaveParameter();
                parameter["OceanBookingID"] = operationId;
                parameter["OperationType"] = (int)OperationType.OceanImport;
                parameter[action] = updateVlues;
                listBusinessParameter.Add(parameter);
                ServiceClient.GetService<IBusinessQueryService>().Save(listBusinessParameter);
            }
        }
        /// <summary>
        /// 事件集合列表
        /// </summary>
        private List<EventCode> _eventCodeList = new List<EventCode>();
        /// <summary>
        /// 返回当前CODE的事件详细信息
        /// </summary>
        /// <returns></returns>
        public EventCode EventCodeList(string code)
        {
            if (_eventCodeList.Any() == false)
            {
                _eventCodeList = FCMCommonService.GetEventCodeList(OperationType.OceanImport);
            }
            return _eventCodeList.FirstOrDefault(n => n.Code == code);
        }

        #endregion

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Enabled = true;
            //context = data as BusinessOperationContext;

            if (context != null && context.OperationID != null && context.OperationID != Guid.Empty)
            {
                this.cargotracking = FCMCommonService.GetOperationContainersInfo(context.OperationID, context.OperationType);
            }
            if (cargotracking == null)
            {
                this.cargotracking = new CargoTrackingInfo();
            }
            this.bsOperationinfo.DataSource = this.cargotracking;
            this.bsOperationinfo.ResetBindings(false);

            this.bsContainerinfo.DataSource = cargotracking.Containers;
            this.bsContainerinfo.ResetBindings(false);
        }

        private void gvBoxList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ReturnDate")
            {
                ClientOceanImportService.NoticePod(context.OperationID);
            }
        }

        private void barEmptyReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<CustomerCarrierObjects> mailto = new List<CustomerCarrierObjects>();
            List<OceanImportTruckInfo> truckList = OceanImportService.GetOceanTruckServiceList(cargotracking.OperationID);
            if (truckList != null && truckList.Count > 0)
            {
                ContactObjects dataList = FCMCommonService.GetContactList(cargotracking.OperationID, OperationType.OceanImport);
                if (dataList != null)
                {
                    mailto = dataList.CustomerCarrier;
                    mailto = mailto.FindAll(item => item.CustomerID == truckList[0].TruckerID);
                }
            }

            string messto = string.Empty;

            if (mailto.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "No Trucker contact email was found. Please fill in the mail recipient yourself!" : "未找到拖车行联系人邮箱，请自行填写邮件收件人！");
                messto = LocalData.UserInfo.EmailAddress;
            }
            else
            {
                messto = string.Join(";", (from p in mailto select p.Mail).ToArray());
            }

            int[] selrows = gvBoxList.GetSelectedRows();
            foreach (int sel in selrows)
            {
                CargoTrackingContainerInfo Container = gvBoxList.GetRow(sel) as CargoTrackingContainerInfo;
                ClientOceanImportService.NoticeEmptyReport(cargotracking.OperationID, Container.ContainerNO, messto);
            }

        }

        /// <summary>
        /// 位置对象
        /// </summary>
        Point _location;
        /// <summary>
        /// 大小
        /// </summary>
        Size _size;
        /// <summary>
        /// 是否已经设置
        /// </summary>
        bool _isSet = false;
        /// <summary>
        /// 获取预览窗格位置及其窗体大小
        /// </summary>
        private void GetPositionAndSize()
        {
            if (!_isSet)
            {
                Screen scr = null;
                scr = Screen.FromPoint(this.Location);
                _location = new Point((int)(scr.WorkingArea.Width * 0.4), LocalData.Height);
                int height = scr.WorkingArea.Height - LocalData.Height;
                int width = (int)(scr.WorkingArea.Width * 0.6);
                _size = new Size(width, height);
                _isSet = true;
            }
        }

        void CrawlerAvailability(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentCargoTrackingContainer.ContainerNO))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select a ContainerNO." : "请选择一个箱号.");
                return;
            }
            if (DataSource == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "CargoTrackingInfo is null." : "货物跟踪信息为空.");
                return;
            }
            if (!string.IsNullOrEmpty(DataSource.PODCode))
            {
                LocationInfo location = GeographyService.GetLocationInfo(DataSource.PODID);
                if (location != null &&
                    location.CountryID != new Guid("37F06C2D-E5F6-4A6F-BB55-9DA3EC3B42A4")   //美国
                    && location.CountryID != new Guid("AF94FF33-AFAC-4D2E-B26D-66DF7AD6119E"))//中国
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Only China and the United States can get the CargoTrackingInfo." : "只有中国和美国可以获取箱动态.");
                    return;
                }
            }
            GetPositionAndSize();
            string searching = LocalData.IsEnglish ? "Searching the containerNo:" : "正在查询箱号:";
            int threadID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(string.Format(searching + "{0}", CurrentCargoTrackingContainer.ContainerNO));
            try
            {
                //    WaitDialogForm waitForm = new WaitDialogForm(string.Format(searching + "{0}", "TCNU3205490"), isEnglish ? "Loading..." : "请稍候...");
                //    Thread.Sleep(1500);
                //    waitForm.Close();
                //threadCargoTracking = new Thread(CargoTracking);
                //threadCargoTracking.Start();
                //获得码头列表
                GetTerminalList();
                //terminalList = terminalService.GetTerminalList(SearchUsing.ImportConfig);
                //if (terminalList.Count == 0)
                //{
                //    //MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Loading the Terminal List,Please wait..." : "正在加载码头列表，请稍候...");
                //    //return;
                //    //terminalList = TerminalService.GetTerminalList(ICP.Crawler.CommonLibrary.Enum.SearchUsing.ImportConfig);
                //}
                //保存船名POD默认码头
                ICP.Crawler.ServiceInterface.DataObjects.Terminals obj = null;
                if (terminalList.Count == 0)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Can't find the Terminal." : "找不到码头.");
                    return;
                }
                else if (terminalList.Count == 1)
                    obj = TerminalService.GetTerminalInfo(terminalList[0].ID, Crawler.CommonLibrary.Enum.SearchUsing.ImportConfig);
                    //terminalList.Find(o => o.Code == lstTerminalList.SelectedItem.ToString());
                else
                {
                    contextMenuStrip1.Items.Clear();
                    foreach (var item in terminalList)
                    {
                        contextMenuStrip1.Items.Add(item.Code);
                    }
                    contextMenuStrip1.Show(sender as Control, new Point(e.X, e.Y));
                }
                if (obj != null)
                {
                    //Terminals obj = terminalList.Find(o => o.Code == lstTerminalList.SelectedItem.ToString());
                    string vessel = DataSource.Voyage.Substring(0, DataSource.Voyage.IndexOf('/'));
                    //terminalService.SaveVesselTerminals(vessel, DataSource.PODCode, obj.ID);
                    TerminalService.SaveVesselTerminals(vessel, DataSource.PODCode, obj.ID);
                    //箱动态快捷访问
                    CargoTracking(obj);
                }

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(threadID);
            }
        }

        void CargoTracking(ICP.Crawler.ServiceInterface.DataObjects.Terminals terminal)
        {
            try
            {
                terminal.Reference = CurrentCargoTrackingContainer.ContainerNO;
                object o = null;
                //判断码头的获取动态时间（RipperLastDate）
                ICP.Crawler.ServiceInterface.DataObjects.TerminalContainer terminalContainer = TerminalService.GetTerminalContainersInfo(terminal.ID, CurrentCargoTrackingContainer.ContainerID);
                if (terminalContainer != null && terminalContainer.RipperLastDate.Value.Date == DateTime.Now.Date)
                {
                    o = terminalContainer.CurrentAvailability;
                }
                else
                    o = CrawlerService.StartCrawler(terminal, Crawler.CommonLibrary.Enum.CrawlerMode.Quick);
                //object o = crawlerService.QuickCrawler(CurrentCargoTrackingContainer.ContainerNO, lstTerminalList.SelectedItem.ToString());
                if (o != null)
                {
                    string path = Application.StartupPath + "\\PDF";
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    string filePath = string.Format(path + string.Format("\\{0}.pdf", CurrentCargoTrackingContainer.ContainerNO));
                    //EO.Pdf.PdfDocument result = new EO.Pdf.PdfDocument();
                    //EO.Pdf.HtmlToPdf.ConvertHtml(o.ToString(), result);
                    //result.Save(filePath);
                    //ServiceClient.FilePreviewService.Preview(filePath, _location, _size, true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<!DOCTYPE html>");
                    sb.Append("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
                    sb.Append("<head>");
                    sb.Append("<meta charset=\"utf-8\" />");
                    sb.Append("<title></title>");
                    sb.Append("</head>");
                    sb.Append("<body>");
                    sb.Append(o.ToString());
                    sb.Append("</body>");
                    sb.Append("</html>");
                    String htmlpath = path + string.Format("\\{0}.html", CurrentCargoTrackingContainer.ContainerNO);
                    StreamWriter log = new StreamWriter(htmlpath, true);
                    log.WriteLine(sb.ToString());
                    log.Close();
                    ////PartLoader.ShowEditPart<ShowWeb>(Workitem, sb, isEnglish ? "Cargo Tracking" : "箱货跟踪", null);

                    //Create a pdf document.
                    PdfDocument doc = new PdfDocument();
                    PdfPageSettings pgSt = new PdfPageSettings();
                    pgSt.Size = PdfPageSize.A3;

                    PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
                    htmlLayoutFormat.IsWaiting = false;

                    string source = File.ReadAllText(htmlpath);
                    doc.LoadFromHTML(source, true, pgSt, htmlLayoutFormat);

                    //Save pdf file.
                    doc.SaveToFile("FromHTML.pdf");
                    doc.Close();

                    //Launch the file.
                    //PDFDocumentViewer("FromHTML.pdf");

                    //EO.Pdf.PdfDocument result = new EO.Pdf.PdfDocument();
                    //EO.Pdf.HtmlToPdf.ConvertHtml(o.ToString(), result);
                    //result.Save(filePath);
                    ServiceClient.FilePreviewService.Preview("FromHTML.pdf", _location, _size, true);
                    //删除文件
                    if (!System.IO.Directory.Exists(htmlpath))
                    {
                        File.Delete(htmlpath);
                        File.Delete("FromHTML.pdf");
                    }
                    //CommonUIUtility.OpenWith("D:\\PDF.pdf");
                    //txtReturnLoc.Text = o.ToString();
                    ////ImageHelper.BytesToImage((byte[])o).Save("d:\\eft.jpg");
                    //if (o is string)  //返回异常信息
                    //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), o.ToString());
                    //else
                    //    PartLoader.ShowEditPart<ImageEditPart>(Workitem, o, isEnglish ? "Availability" : "码头箱动态", null);
                }
                else
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Can not find the data" : "没有查到相关数据");
                //}
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        private void gvBoxList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }

        //void SetTerminalList()
        //{
        //    try
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
        //        if (DataSource != null)
        //        {
        //            //获得码头列表
        //            //terminalList = terminalService.GetTerminalList(SearchUsing.ImportConfig);
        //            if (terminalList.Count == 0)
        //            {
        //                GetTerminalList();
        //                //MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Loading the Terminal List,Please wait..." : "正在加载码头列表，请稍候...");
        //                //return;
        //                //terminalList = TerminalService.GetTerminalList(ICP.Crawler.CommonLibrary.Enum.SearchUsing.ImportConfig);
        //            }
        //            List<ICP.Crawler.ServiceInterface.DataObjects.Terminals> list = terminalList.FindAll(o => o.LocationCode == DataSource.PODCode);
        //            if (list != null)
        //            {
        //                lstTerminalList.Items.Clear();
        //                foreach (var item in list)
        //                {
        //                    lstTerminalList.Items.Add(item.Code);
        //                }
        //                //设置默认值
        //                string vessel = DataSource.Voyage.Substring(0, DataSource.Voyage.IndexOf('/'));
        //                ICP.Crawler.ServiceInterface.DataObjects.VesselTerminals vt = TerminalService.GetVesselTerminalsInfo(vessel, DataSource.PODCode);
        //                //VesselTerminals vt = terminalService.GetVesselTerminalsInfo(vessel, DataSource.PODCode);
        //                if (vt != null)
        //                    lstTerminalList.SelectedItem = terminalList.Find(o => o.ID == vt.TerminalID).Code;
        //                else if (lstTerminalList.Items.Count > 0)
        //                    lstTerminalList.SelectedIndex = 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
        //    }
        //    finally
        //    {
        //        this.Cursor = System.Windows.Forms.Cursors.Default;
        //    }
        //}

        private void gvBoxList_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (e.Column == colAvailability)
                    e.Value = LocalData.IsEnglish ? "Get" : "获取";
            }
        }

        private void repositoryItemAvailability_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                //单击左键
                if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
                {
                    CrawlerAvailability(sender, e);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ICP.Crawler.ServiceInterface.DataObjects.Terminals obj = null;
            Terminals t = terminalList.Find(o => o.Code == e.ClickedItem.Text);
            if (t != null)
            {
                obj = TerminalService.GetTerminalInfo(t.ID, Crawler.CommonLibrary.Enum.SearchUsing.ExportConfig);
            }
            if (obj != null)
            {
                //箱动态快捷访问
                CargoTracking(obj);
            }
        }

    }

}

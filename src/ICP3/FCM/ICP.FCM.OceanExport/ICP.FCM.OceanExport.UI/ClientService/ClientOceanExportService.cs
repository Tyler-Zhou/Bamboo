using DevExpress.XtraEditors;
using ICP.Business.Common.ServiceInterface;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Contact;
using ICP.Business.Common.UI.Document;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.BillRevise;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.FCM.OceanExport.UI.BL;
using ICP.FCM.OceanExport.UI.BL.HBL;
using ICP.FCM.OceanExport.UI.Booking;
using ICP.FCM.OceanExport.UI.Booking.BaseEdit;
using ICP.FCM.OceanExport.UI.Common;
using ICP.FCM.OceanExport.UI.Container;
using ICP.FCM.OceanExport.UI.HBL;
using ICP.FCM.OceanExport.UI.MBL;
using ICP.FCM.OceanExport.UI.Order;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Constants = ICP.Operation.Common.ServiceInterface.Constants;
using EnumContactType = ICP.Framework.CommonLibrary.Common.ContactType;
using EditRemarkPart = ICP.FCM.OceanExport.UI.Common.Parts.EditRemarkPart;
using ICP.Framework.ClientComponents;

namespace ICP.FCM.OceanExport.UI
{
    ///<summary>
    /// 海出业务客户端服务实现类
    ///</summary>
    public class ClientOceanExportService : IClientOceanExportService
    {
        #region Fields & Service & Property
        /// <summary>
        /// 海运事件缓存
        /// </summary>
        List<EventCode> _CacheEventCodeList = new List<EventCode>();
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private static DataTable dt = null;
        #endregion

        #region Service

        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// Root WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        ///<summary>
        /// 配置服务接口
        ///</summary>
        public IConfigureService ConfigureService
        {
            get { return ServiceClient.GetService<IConfigureService>(); }

        }
        /// <summary>
        /// 业务代理服务接口
        /// </summary>
        public IOperationAgentService OperationAgentService
        {
            get { return ServiceClient.GetService<IOperationAgentService>(); }
        }

        ///<summary>
        /// EDI发送服务接口
        ///</summary>
        public IEDIClientService EdiClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }

        private IClientBusinessOperationService _ClientBusinessOperationService;
        /// <summary>
        /// 客户端业务操作接口
        /// </summary>
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                try
                {
                    _ClientBusinessOperationService = ServiceClient.GetClientService<IClientBusinessOperationService>();
                }
                catch
                {
                    _ClientBusinessOperationService = ServiceClient.GetService<IClientBusinessOperationService>();
                }

                return _ClientBusinessOperationService;
            }
        }

        ///<summary>
        /// 业务客户端服务接口
        ///</summary>
        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }
        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        ///<summary>
        /// 海出打印服务
        ///</summary>
        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }

        /// <summary>
        /// ICP 通用UI辅助类
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 海运出口服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 财务接口
        /// </summary>
        public static IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();

            }
        }
        /// <summary>
        /// 海运出口报表服务
        /// </summary>
        public IOEReportDataService OEReportDataService
        {
            get
            {
                return ServiceClient.GetService<IOEReportDataService>();
            }

        }
        /// <summary>
        /// 报表服务
        /// </summary>
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }

        }
        /// <summary>
        /// 调用邮件模板发送邮件服务接口
        /// </summary>
        public IMailCenterTemplateService MailCenterTemplateService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();

                return new MailCenterTemplateService();
            }
        }
        /// <summary>
        /// 用户管理服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 客户端操作文件服务
        /// </summary>
        public IClientFileService ClientFileService
        {
            get { return ServiceClient.GetClientService<IClientFileService>(); }

        }
        /// <summary>
        /// 邮件模版服务
        /// </summary>
        public IMainCenterEmailTemplateGetter MainCenterEmailTemplateGetter
        {
            get { return ServiceClient.GetClientService<IMainCenterEmailTemplateGetter>(); }
        }
        /// <summary>
        /// Message服务
        /// </summary>
        public IMessageService MessageService
        {
            get { return ServiceClient.GetService<IMessageService>(); }
        }
        /// <summary>
        /// 海运出口服务
        /// </summary>
        public IOceanExportService OEService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 事件列表保存是修改缓存表的指定字段
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }
        /// <summary>
        /// 保存事件的接口
        /// </summary>
        public IFCMCommonService FcmCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }
        /// <summary>
        /// 沟通历史记录服务
        /// </summary>
        public ICommunicationHistoryService CommunicationHistoryService
        {
            get { return ServiceClient.GetService<ICommunicationHistoryService>(); }
        }
        /// <summary>
        /// Outlook服务
        /// </summary>
        public IOutLookService OutLookService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                return new OutlookService();
            }
        }
        #endregion

        #region Property
        /// <summary>
        /// 美加线
        /// </summary>
        public static List<Guid> NAShippingLines
        {
            get
            {
                List<Guid> idlist = new List<Guid>();

                idlist.Add(new Guid("91B68E82-32A9-4A41-8309-20DE34893C25"));//加拿大
                idlist.Add(new Guid("6F51BA0E-397C-4AF8-A453-617B1051E76B"));//美西
                idlist.Add(new Guid("8F09FD42-3BBA-4EA9-BB5B-80E53770CA84"));//北美区
                idlist.Add(new Guid("FC4361F1-FF7A-4B57-B411-99E106D1B7C0"));//美国航线
                idlist.Add(new Guid("E2D05D39-B9A2-4C7D-838E-C6FA466609EE"));//美东
                return idlist;
            }
        }
        #endregion
        #endregion

        #region Order
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public void AddOrder(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var title = LocalData.IsEnglish ? "New Orders" : "新增订单";
                PartLoader.ShowEditPart<OrderBaseEditPart>(RootWorkItem, null, EditMode.New, values, title, editPartSaved, title);
            }
        }
        /// <summary>
        /// 编辑订单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public void EditOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                Stopwatch stopwatch = Stopwatch.StartNew();
                string title = ((LocalData.IsEnglish) ? "Edit Order: " : "编辑订单: ") + OECommonUtility.GetLineNo(showCriteria.OperationNo);
                PartLoader.ShowEditPart<OrderBaseEditPart>(RootWorkItem, showCriteria, EditMode.Edit, values, title, editPartSaved, showCriteria.BillNo.ToString());
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-ORDER", string.Format("打开海出编辑订单;Order NO[{0}]", showCriteria.OperationNo));
            }
        }
        /// <summary>
        /// 复制订单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public void CopyOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = ((LocalData.IsEnglish) ? "Copy Order: " : "复制订单: ") + OECommonUtility.GetLineNo(showCriteria.OperationNo);
                PartLoader.ShowEditPart<OrderBaseEditPart>(RootWorkItem, showCriteria, EditMode.Copy, values, title, editPartSaved, showCriteria.BillNo.ToString());

            }

        }

        /// <summary>
        /// 取消或激活订单
        /// </summary>
        /// <param name="oceanBookingList">实体对象</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public SingleResult CancelOrder(OceanBookingList oceanBookingList, PartDelegate.EditPartSaved editPartSaved)
        {
            return Cancel(false, oceanBookingList, editPartSaved);
        }
        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="orderId">订单ID或者业务Id</param>
        /// <param name="companyID">订单口岸ID</param>
        public void PrintOrder(Guid orderId, Guid companyID)
        {
            OceanExportPrintHelper.PrintOEOrder(orderId, companyID);
        }
        #endregion

        #region Booking
        /// <summary>
        ///新增订舱单
        /// </summary>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public void AddBooking(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {

            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<BookingBaseEditPart>(RootWorkItem, null, EditMode.New, values, string.Empty, editPartSaved, "CreateSo");
            }
        }
        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public void EditBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                string title = (LocalData.IsEnglish ? "Edit Booking: " : "编辑订舱单: ") + showCriteria.OperationNo;
                string id = showCriteria.BillNo.ToString();
                PartLoader.ShowEditPart<BookingBaseEditPart>(RootWorkItem, showCriteria, EditMode.Edit, values, title, editPartSaved, id);
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-BOOKING", string.Format("编辑订舱单;Booking No[{0}]", showCriteria.OperationNo));
            }
        }
        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public void CopyBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = ((LocalData.IsEnglish) ? "Copy Booking: " : "复制订舱单: ") + OECommonUtility.GetLineNo(showCriteria.OperationNo);
                PartLoader.ShowEditPart<BookingBaseEditPart>(RootWorkItem, showCriteria, EditMode.Copy, values, title, editPartSaved, showCriteria.BillNo.ToString());
            }
        }
        #endregion

        #region BL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void AddBL(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            
        }
        /// <summary>
        /// 编辑提单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void EidtBL(EditPartShowCriteria showCriteria, IDictionary<string, object> values,
            PartDelegate.EditPartSaved editPartSaved)
        {
            
        }

        /// <summary>
        /// 根据舱单ID构建MBL信息
        /// </summary>
        /// <param name="bookingID">订舱单ID</param>
        /// <returns></returns>
        public OceanMBLInfo BuildMBLByBookingInfo(Guid bookingID)
        {
            OceanBookingInfo bookingInfo = OceanExportService.GetOceanBookingInfo(bookingID);
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(bookingInfo.CompanyID, LocalData.IsEnglish);
            OceanMBLInfo currentMBL = new OceanMBLInfo
            {
                ID = GlobalConstants.NewRowID,
                CustomerName = bookingInfo.CustomerName,
                OEOperationType = bookingInfo.OEOperationType,
                BLTitleID =
                    ArgumentHelper.GuidIsNullOrEmpty(bookingInfo.BLTitleID)
                        ? configureInfo.BLTitleID
                        : bookingInfo.BLTitleID,
                BLTitleName =
                    string.IsNullOrEmpty(bookingInfo.BLTitleName)
                        ? configureInfo.BLTitleName
                        : bookingInfo.BLTitleName,
                ContractID = bookingInfo.ContractID,
                ContractNo = bookingInfo.ContractNo,
                IsChargePayOrCon = false,
                CompanyID = bookingInfo.CompanyID,
                CarrierID = bookingInfo.CarrierID,
                RefNo = bookingInfo.No,
                OceanBookingID = bookingInfo.ID,
                IssueType =IssueType.Normal,
                IssueByID = LocalData.UserInfo.LoginID,
                IssueByName = LocalData.UserInfo.LoginName,
                ReleaseType = bookingInfo.MBLReleaseType == null ? FCMReleaseType.Unknown : bookingInfo.MBLReleaseType.Value,
                AgentOfCarrierName = bookingInfo.AgentOfCarrierName,
                SONO = bookingInfo.OceanShippingOrderNo,
                CarrierName = bookingInfo.CarrierName,
                SalesName = bookingInfo.SalesName,
                FilerName = bookingInfo.FilerName,
                BookingerName = bookingInfo.BookingerName,
                OverseasFilerName = bookingInfo.OverSeasFilerName,
            };

            #region Agent  MBL.代理 = 订舱单.代理

            currentMBL.AgentID = bookingInfo.AgentID;
            currentMBL.AgentName = bookingInfo.AgentName;
            currentMBL.AgentDescription = bookingInfo.AgentDescription;

            #endregion

            #region Shipper  MBL.发货人 = 订舱单.发货人

            //只出MBL
            //	MBL.发货人 = 订舱单.发货人,MBL.发货人描述 = 根据MBL.发货人生成
            //	MBL.收货人 = 订舱单.收货人,MBL.收货人描述 = 根据MBL.收货人生成
            if (bookingInfo.IsOnlyMBL)
            {
                #region 发货人
                currentMBL.ShipperID = bookingInfo.BookingShipperID.IsNullOrEmpty() ? bookingInfo.ShipperID : bookingInfo.BookingShipperID;
                currentMBL.ShipperName = bookingInfo.BookingShipperID.IsNullOrEmpty() ? bookingInfo.ShipperName : bookingInfo.BookingShipperName;
                currentMBL.ShipperDescription = (
                    bookingInfo.BookingShipperID.IsNullOrEmpty() 
                    ? bookingInfo.ShipperDescription 
                    : bookingInfo.BookingShipperdescription).ConvertToNew();
                #endregion

                #region 收货人
                currentMBL.ConsigneeID = bookingInfo.BookingConsigneeID.IsNullOrEmpty() ? bookingInfo.ConsigneeID : bookingInfo.BookingConsigneeID;
                currentMBL.ConsigneeName = bookingInfo.BookingConsigneeID.IsNullOrEmpty() ? bookingInfo.ConsigneeName : bookingInfo.BookingConsigneeName;
                currentMBL.ConsigneeDescription = (
                    bookingInfo.BookingConsigneeID.IsNullOrEmpty()
                    ? bookingInfo.ConsigneeDescription
                    : bookingInfo.BookingConsigneedescription).ConvertToNew();

                #endregion
            }
            //	如果该票订舱单.[只出MBL] = false，则导入如下信息到MBL中：
            //	MBL.发货人 = 订舱单.操作口岸.关联的客户 (参照公司配置)，MBL.发货人描述 = 根据MBL.发货人生成
            //	MBL.收货人 = 订舱单.代理，MBL.收货人描述 = 根据MBL.收货人生成
            else
            {
                #region 发货人
                currentMBL.ShipperID = bookingInfo.BookingShipperID.IsNullOrEmpty() ? configureInfo.CustomerID : bookingInfo.BookingShipperID;
                currentMBL.ShipperName = bookingInfo.BookingShipperID.IsNullOrEmpty() ? configureInfo.CustomerName : bookingInfo.BookingShipperName;
                ICPCommUIHelper.SetCustomerDesByID(currentMBL.ShipperID, currentMBL.ShipperDescription);
                #endregion

                #region 收货人
                currentMBL.ConsigneeID = bookingInfo.BookingConsigneeID.IsNullOrEmpty() ? bookingInfo.AgentID : bookingInfo.BookingConsigneeID;
                currentMBL.ConsigneeName = bookingInfo.BookingConsigneeID.IsNullOrEmpty() ? bookingInfo.AgentName : bookingInfo.BookingConsigneeName;
                currentMBL.ConsigneeDescription = (bookingInfo.BookingConsigneeID.IsNullOrEmpty() 
                    ? bookingInfo.AgentDescription 
                    : bookingInfo.BookingConsigneedescription).ConvertToNew();
                #endregion
            }

            #endregion

            #region NotifyParty = 通知人描述 = “SAME AS CONSIGNEE”

            currentMBL.NotifyPartyID = bookingInfo.BookingNotifyPartyID.IsNullOrEmpty() ? Guid.Empty : bookingInfo.BookingNotifyPartyID;
            currentMBL.NotifyPartyName = bookingInfo.BookingNotifyPartyID.IsNullOrEmpty() ? string.Empty : bookingInfo.BookingNotifyPartyname;
            currentMBL.NotifyPartyDescription = (bookingInfo.BookingNotifyPartyID.IsNullOrEmpty() 
                ? new CustomerDescription() 
                : bookingInfo.BookingNotifyPartydescription).ConvertToNew();
            #endregion

            #region BookingParty 订舱人
            currentMBL.BookingPartyID = bookingInfo.BookingPartyID;
            currentMBL.BookingPartyName = bookingInfo.BookingPartyName;
            #endregion

            #region Port 港口

            //	MBL.收货地描述 = 订舱单.收货地.名称 
            //	MBL.装货港描述 = 订舱单. 装货港.名称 
            //	MBL.卸货港描述 = 订舱单. 卸货港.名称 
            //	MBL.交货地描述 = 订舱单. 交货地.名称 
            currentMBL.PlaceOfDeliveryID = bookingInfo.PlaceOfDeliveryID;
            currentMBL.PlaceOfDeliveryCode = bookingInfo.PlaceOfDeliveryName;
            currentMBL.PlaceOfDeliveryName = bookingInfo.PlaceOfDeliveryName;

            currentMBL.POLID = bookingInfo.POLID;
            currentMBL.POLCode = bookingInfo.POLName;
            currentMBL.POLName = bookingInfo.POLName;

            currentMBL.PODID = bookingInfo.PODID;
            currentMBL.PODCode = bookingInfo.PODName;
            currentMBL.PODName = bookingInfo.PODName;

            currentMBL.PlaceOfReceiptID = bookingInfo.PlaceOfReceiptID;
            currentMBL.PlaceOfReceiptCode = bookingInfo.PlaceOfReceiptName;
            currentMBL.PlaceOfReceiptName = bookingInfo.PlaceOfReceiptName;

            //	MBL.最终目的地 = if 主提单.运输条款<>DOOR and 只出MBL=false then 订舱单.交货地 else 订舱单.最终目的地
            //	MBL. 最终目的地描述=最终目的地.名称 
            if (string.IsNullOrEmpty(bookingInfo.TransportClauseName) == false
                && bookingInfo.TransportClauseName.Contains("-DOOR") == false
                && bookingInfo.IsOnlyMBL == false)
            {
                currentMBL.FinalDestinationID = bookingInfo.PlaceOfDeliveryID;
                currentMBL.FinalDestinationCode = bookingInfo.PlaceOfDeliveryName;
                currentMBL.FinalDestinationName = bookingInfo.PlaceOfDeliveryName;
            }
            else
            {
                currentMBL.FinalDestinationID = bookingInfo.FinalDestinationID;
                currentMBL.FinalDestinationCode = bookingInfo.FinalDestinationName;
                currentMBL.FinalDestinationName = bookingInfo.FinalDestinationName;
            }



            #endregion

            #region Voyage, PreVoyage, ETD, ETA

            currentMBL.PreVoyageID = bookingInfo.PreVoyageID;
            currentMBL.PreVesselVoyage = bookingInfo.PreVoyageName;

            currentMBL.VoyageID = bookingInfo.VoyageID;
            currentMBL.VesselVoyage = bookingInfo.VoyageName;

            currentMBL.ETD = bookingInfo.ETD;
            currentMBL.ETA = bookingInfo.ETA;
            #endregion

            #region 付款方式,运输条款,数量,重量,体积
            //	MBL.付款方式 = 订舱单. MBL付款方式
            //	MBL.运输条款 = 订舱单. 运输条款
            //	MBL.数量 = 订舱单. 数量
            //	MBL.数量单位 = 订舱单.数量单位
            //	MBL.重量 = 订舱单. 重量
            //	MBL.重量单位 = 订舱单. 重量单位
            //	MBL.体积 = 订舱单.体积
            //	MBL.体积单位 = 订舱单.体积单位

            currentMBL.TransportClauseID = bookingInfo.MBLTransportClauseID.IsNullOrEmpty() ? bookingInfo.TransportClauseID : (Guid)bookingInfo.MBLTransportClauseID;
            currentMBL.TransportClauseName = bookingInfo.MBLTransportClauseID.IsNullOrEmpty() ? bookingInfo.TransportClauseName : bookingInfo.MBLTransportClauseName;

            currentMBL.IsThirdPlacePayOrder = bookingInfo.IsThirdPlacePayOrder;
            currentMBL.CollectbyAgentOrderID = bookingInfo.CollectbyAgentOrderID;
            currentMBL.CollectbyAgentNameOrder = bookingInfo.CollectbyAgentNameOrder;
            currentMBL.BookingPaymentTermID = currentMBL.PaymentTermID = bookingInfo.MBLPaymentTermID;
            if (string.IsNullOrEmpty(currentMBL.PaymentTermName) == false)
            {
                if (currentMBL.PaymentTermName == "CC" || currentMBL.PaymentTermName == "到付")
                    currentMBL.FreightDescription = "FREIGHT COLLECT";
                else
                    currentMBL.FreightDescription = "FREIGHT PREPAID";
            }
            else
                currentMBL.FreightDescription = string.Empty;
            currentMBL.Quantity = bookingInfo.Quantity;
            currentMBL.QuantityUnitID = bookingInfo.QuantityUnitID == null ? Guid.Empty : bookingInfo.QuantityUnitID.Value;
            currentMBL.Weight = bookingInfo.Weight;
            currentMBL.WeightUnitID = bookingInfo.WeightUnitID.IsNullOrEmpty() ? Guid.Empty : bookingInfo.WeightUnitID.Value;
            currentMBL.Measurement = bookingInfo.Measurement;
            currentMBL.MeasurementUnitID = bookingInfo.MeasurementUnitID == null ? Guid.Empty : bookingInfo.MeasurementUnitID.Value;
            #endregion
            return currentMBL;
        }
        /// <summary>
        /// 预配
        /// </summary>
        /// <param name="blNO"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void DeclarationContainer(string blNO, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            string title = (LocalData.IsEnglish ? "Declaration " : "预配 ") + OECommonUtility.GetLineNo(blNO);
            using (new CursorHelper(Cursors.WaitCursor))
            {

                PartLoader.ShowEditPart<ContainerEditPart>(RootWorkItem, null, values, title, editPartSaved,
                    OEBookingCommandConstants.Command_DeclarationContainer + blNO);
            }
        }
        /// <summary>
        /// 预配
        /// </summary>
        /// <param name="blNO"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void DeclarationImport(string blNO, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            string title = (LocalData.IsEnglish ? "Declaration " : "预配 ") + (blNO.IsNullOrEmpty() ? "" : OECommonUtility.GetLineNo(blNO));
            using (new CursorHelper(Cursors.WaitCursor))
            {

                PartLoader.ShowEditPart<ContainerPart>(RootWorkItem, null, values, title, editPartSaved, OEBookingCommandConstants.Command_DeclarationContainer + blNO);
            }
        }
        #endregion

        #region AMS
        /// <summary>
        /// 确认AMS费用
        /// </summary>
        /// <param name="hblids">提单ID集合</param>
        /// <param name="updateBy">更新人</param>
        public bool ConfirmedAMS(Guid[] hblids, Guid updateBy)
        {
            DialogResult dr =MessageBoxService.ShowWarning("请确认AMS信息是否一致，特别是船名信息", "提示",MessageBoxButtons.YesNo);
            if(dr==DialogResult.Yes)
            {
                return OceanExportService.ConfirmedAMS(hblids, updateBy);
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        public BusinessOperationContext CopyBusiness(BusinessOperationContext context)
        {
            UCCopyBusiness ucCopyBusiness = RootWorkItem.Items.AddNew<UCCopyBusiness>();
            string title = LocalData.IsEnglish ? "Copy Shipment" : "复制业务";
            ucCopyBusiness.BindingData(context);
            BusinessOperationContext newContext = null;
            DialogResult dr = PartLoader.ShowDialog(ucCopyBusiness, title);
            if (dr == DialogResult.OK)
            {
                newContext = new BusinessOperationContext
                {
                    OperationID = ucCopyBusiness.NewOperationID
                    ,
                    OperationNO = ucCopyBusiness.NewOperationNo
                };
            }
            return newContext;
        }

        /// <summary>
        /// EDI方法
        /// </summary>
        /// <param name="oceanBookingList"></param>
        /// <param name="selectRows"></param>
        public bool EBookingCall(OceanBookingList oceanBookingList, List<OceanBookingList> selectRows)
        {
            OceanBookingInfo ob1 = OEService.GetOceanBookingInfo(oceanBookingList.ID);
            Guid[] ids1 = selectRows.Select(book => book.ID).ToArray();
            string[] nos1 = selectRows.Select(book => book.No).ToArray();
            EDISendOption sendOption = new EDISendOption
            {
                EdiMode = EDIMode.Booking,
                CompanyID = ob1.CompanyID,
                CarrierID = ob1.CarrierID != null ? ob1.CarrierID.Value : Guid.Empty,
                AgentOfCarrierID = ob1.AgentOfCarrierID,
                IDs = ids1,
                NOs = nos1,
                FIDs = ids1,
                FNOs = nos1,
                OperationType = OperationType.OceanExport,
                SendByID = LocalData.UserInfo.LoginID,
            };
            EDIConfigItem configOption = EdiClientService.GetEDIConfigByOption(sendOption);
            if(configOption!=null)
            {
                sendOption.Subject = string.Format("{0}{1}({2})", configOption.SubjectPrefix, EnumHelper.GetDescription(EDIMode.Booking, true), ob1.No);
                return EdiClientService.ShowForm(sendOption, false);
            }

            bool flg = false;
            Guid customerID = Guid.Empty;
            List<Guid> inttraList = new List<Guid>();
            if (oceanBookingList == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current operation." : "请选择当前要发送EDI的业务.");
                return flg;
            }
            if (selectRows.Count > 10)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items be less than or equal to 10." : "选择的项应小于等于10！");
                return flg;
            }
            int i = (from d in selectRows group d by d.CarrierID into g select g.Key).Count();
            if (i > 1)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                return flg;
            }
            //int n = (from d in selectRows group d by d.CompanyID into g select g.Key).Count();
            //if (n > 1)
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
            //    return flg;
            //}
            inttraList.Add(new Guid("979B3DA5-2FE2-4895-8F43-DE5610D20599"));//CMA
            inttraList.Add(new Guid("E2A5B70E-9D7A-47B2-9902-082D8A317548"));//UASC
            inttraList.Add(new Guid("FDCA28E3-7673-4803-B3C2-71E7E66B7650"));//APL
            inttraList.Add(new Guid("68797EA6-F0BB-4035-947B-84A731E21245"));//HPL
            inttraList.Add(new Guid("BF072F15-BEE9-4C33-8448-70931FC06FA9"));//MSC
            EDISendOption sendItem = new EDISendOption();


            List<EDIConfigureList> ediList = ConfigureService.GetEDIConfigureList(null, null, true, 0);

            List<EDIConfigureList> findEdiList = (from d in ediList where d.EDIMode == EDIMode.Booking && d.CarrierID == oceanBookingList.CarrierID && d.ReceiverType == 1 select d).ToList();
            string key = string.Empty;

            if (findEdiList.Count > 0)
            {
                List<ConfigureListForEDI> comlist = ConfigureService.GetEDICompanyConfigureListByConfigure(findEdiList[0].ID);
                if (comlist.Find(r => r.CompanyID == oceanBookingList.CompanyID) != null)
                {
                    key = findEdiList[0].Code;
                }
            }

            if (string.IsNullOrEmpty(key))
            {
                OceanBookingInfo ob = OEService.GetOceanBookingInfo(oceanBookingList.ID);
                findEdiList = (from d in ediList where d.EDIMode == EDIMode.Booking && d.CarrierID == ob.AgentOfCarrierID && d.ReceiverType == 2 select d).ToList();
                if (findEdiList.Count > 0)
                {
                    key = findEdiList[0].Code;
                    sendItem.CarrierID = ob.AgentOfCarrierID;
                }
                else
                {
                    if (inttraList.Contains((Guid)oceanBookingList.CarrierID))
                    {
                        key = "InttraSo";
                    }
                    else
                    {
                        string message = "目前EDI订舱只支持下列船东:";
                        List<String> carrierList = (from d in ediList where d.EDIMode == EDIMode.Booking group d by d.CarrierName into g select g.Key).ToList();
                        carrierList.RemoveAll(r => string.IsNullOrEmpty(r));
                        message = message + Environment.NewLine + carrierList.Aggregate((a, b) => (a + Environment.NewLine + b));

                        MessageBoxService.ShowInfo(message);
                        return flg;
                    }
                }
            }

            try
            {
                List<Guid> ids = selectRows.Select<OceanBookingList, Guid>(book => book.ID).ToList();
                List<string> nos = selectRows.Select<OceanBookingList, string>(book => book.No).ToList();

                foreach (var item in selectRows)
                {
                    if (ids.Contains(item.ID) == false)
                    {
                        ids.Add(item.ID);
                    }
                    if (nos.Contains(item.No) == false)
                    {
                        nos.Add(item.No);
                    }

                    OceanBookingInfo bookinfo = OceanExportService.GetOceanBookingInfo(item.ID);
                    if (bookinfo.BookingPartyID == null || bookinfo.BookingPartyID == Guid.Empty)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "There is no booking party in the selected item" : "选择的项中存在没有订舱人的项！");
                        return flg;
                    }

                    if (customerID != Guid.Empty && customerID != bookinfo.BookingPartyID)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                        return flg;
                    }

                    customerID = (Guid)bookinfo.BookingPartyID;
                }

                ConfigureInfo companyConfig = ConfigureService.GetCompanyConfigureInfoByCustomer(customerID);

                i = (from d in selectRows group d by d.CarrierID into g select g.Key).Count();
                if (i > 1)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                    return flg;
                }
                //n = (from d in selectRows group d by d.CompanyID into g select g.Key).Count();
                //if (n > 1)
                //{
                //    MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                //    return flg;
                //}

                sendItem.ServiceKey = key;
                sendItem.EdiMode = EDIMode.Booking;
                sendItem.CompanyID = companyConfig.CompanyID;
                sendItem.Subject = string.Empty;
                sendItem.IDs = ids.ToArray();
                sendItem.FIDs = ids.ToArray();
                sendItem.NOs = nos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;
                if (sendItem.ServiceKey == "InttraSo")
                {
                    sendItem.Subject = "Inttra电子订舱(";
                    foreach (string s in nos)
                        sendItem.Subject += s + ",";
                    sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";
                }
                else
                {
                    sendItem.Subject = "电子订舱(";
                    foreach (string s in nos)
                        sendItem.Subject += s + ",";
                    sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";
                }

                if (sendItem.CarrierID == Guid.Empty)
                {
                    sendItem.CarrierID = oceanBookingList.CarrierID == null ? Guid.Empty : (Guid)oceanBookingList.CarrierID;
                }

                bool isSucc = EdiClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    flg = isSucc;
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
            }
            return flg;
        }

        /// <summary>
        /// EDI方法
        /// </summary>
        /// <param name="oceanBookingList"></param>
        /// <param name="selectRows"></param>
        public bool EBookingConfirm(OceanBookingList oceanBookingList, List<OceanBookingList> selectRows)
        {
            bool flg = false;
            if (oceanBookingList == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current operation." : "请选择当前要发送EDI的业务.");
                return flg;
            }
            if (selectRows.Count > 10)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items be less than or equal to 10." : "选择的项应小于等于10！");
                return flg;
            }
            int i = (from d in selectRows group d by d.CarrierID into g select g.Key).Count();
            if (i > 1)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                return flg;
            }
            int n = (from d in selectRows group d by d.CompanyID into g select g.Key).Count();
            if (n > 1)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                return flg;
            }
            if (selectRows[0].CarrierID.ToString().ToUpper() != "5932EBBB-110A-E711-80BD-141877442141")
            {
                MessageBoxService.ShowInfo("目前EDI订舱只支持SMLINE船东");
                return flg;
            }

            string key = "SMLINE_FFBOOKING";
            try
            {
                List<Guid> ids = selectRows.Select<OceanBookingList, Guid>(book => book.ID).ToList();
                List<string> nos = selectRows.Select<OceanBookingList, string>(book => book.No).ToList();

                foreach (var item in selectRows)
                {
                    if (ids.Contains(item.ID) == false)
                    {
                        ids.Add(item.ID);
                    }
                    if (nos.Contains(item.No) == false)
                    {
                        nos.Add(item.No);
                    }
                }

                i = (from d in selectRows group d by d.CarrierID into g select g.Key).Count();
                if (i > 1)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                    return flg;
                }
                n = (from d in selectRows group d by d.CompanyID into g select g.Key).Count();
                if (n > 1)
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                    return flg;
                }
                EDISendOption sendItem = new EDISendOption();
                sendItem.ServiceKey = key;
                sendItem.EdiMode = EDIMode.Booking;
                sendItem.CompanyID = oceanBookingList.CompanyID;
                sendItem.Subject = string.Empty;
                sendItem.IDs = ids.ToArray();
                sendItem.FIDs = ids.ToArray();
                sendItem.NOs = nos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;
                sendItem.Subject = "订舱确认(";
                foreach (string s in nos)
                    sendItem.Subject += s + ",";
                sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";

                bool isSucc = EdiClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    flg = isSucc;
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
            }
            return flg;
        }

        /// <summary>
        /// 打开申请代理页面
        /// </summary>
        /// <param name="bookingId">BookingID,等于业务ID</param>
        public bool ReplyAgent(Guid bookingId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                WorkItem workItem = ServiceClient.GetClientService<WorkItem>();
                return FCMCommonClientService.OpenAgentRequestPart(bookingId, OperationType.OceanExport, values, editPartSaved);
            }
        }

        /// <summary>
        /// 取消订舱单
        /// </summary>
        /// <param name="operationId">业务Id</param>
        /// <param name="isCancel">是否取消，否为激活</param>
        /// <param name="updateDate">业务更新时间</param>
        /// <returns></returns>
        public SingleResult CancelBooking(Guid operationId, PartDelegate.EditPartSaved editPartSaved)
        {
            List<OceanBookingList> dataList = OceanExportService.GetOceanBookingListByIds(new Guid[] { operationId });
            if (dataList == null || dataList.Count == 0) return null;
            OceanBookingList oceanBookingList = dataList.First();
            return CancelOrder(oceanBookingList, editPartSaved);
        }

        /// <summary>
        /// 取消订舱单
        /// </summary>
        /// <param name="operationIds">业务Id</param>
        /// <returns></returns>
        public ManyResult CancelBookings(Guid[] operationIds, PartDelegate.EditPartSaved editPartSaved)
        {
            List<OceanBookingList> dataList = OceanExportService.GetOceanBookingListByIds(operationIds);
            if (dataList == null || dataList.Count == 0) return null;
            string message = string.Empty;
            string arrMsg = string.Empty;
            string operation = LocalData.IsEnglish ? "Cancel" : "取消";
            ManyResult results = new ManyResult();
            if (dataList[0].IsValid)
                message = LocalData.IsEnglish
                    ? "After cancel the shipment, all of it's invoices will be removed automatically!"
                    : "取消业务，同时系统也会删除该票业务所有的账单！?";
            else
            {
                operation = LocalData.IsEnglish ? "Available" : "恢复";
                message = LocalData.IsEnglish ? "Sure Available Current Shipment?" : "你真的要恢复这笔业务吗?";
            }

            DialogResult dialogResult = XtraMessageBox.Show(message,
                                               LocalData.IsEnglish ? "Cancel Shipment" : "取消业务",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                SingleResult result;
                for (int index = 0; index < dataList.Count; index++)
                {
                    OceanBookingList oceanBookingList = dataList[index];
                    try
                    {
                        result = OceanExportService.CancelOceanOrder(oceanBookingList.ID, oceanBookingList.CompanyID
                                , oceanBookingList.IsValid, LocalData.UserInfo.LoginID, oceanBookingList.UpdateDate);
                        if (result != null)
                        {
                            arrMsg += string.Format("[{0}]", oceanBookingList.No);
                            ServiceClient.GetService<IICPCommonOperationService>().UpdateLocalBusinessData(result.GetValue<Guid>("ID"), OperationType.OceanExport);
                            results.Items.Add(result);
                        }
                        if (result != null && editPartSaved != null)
                        {
                            var business = new BusinessOperationParameter
                            {
                                Context = new BusinessOperationContext
                                {
                                    OperationID = result.GetValue<Guid>("ID"),
                                    OperationNO = oceanBookingList.No,
                                    OperationType = OperationType.OceanExport
                                }
                            };

                            editPartSaved(new object[] { business });
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                        break;
                    }
                }
                message = string.Format(LocalData.IsEnglish ? "{0} {1}Shipment Successfully." : "{0}业务已经成功{1}.", arrMsg, operation);

                if (!string.IsNullOrEmpty(arrMsg))
                {
                    message = arrMsg + string.Format((LocalData.IsEnglish ? "{0} Shipment Successfully." : "业务已经成功{0}."), operation);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, message);
                }
            }
            return results;
        }

        /// <summary>
        /// 取消订舱单
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="editPartSaved"></param>
        /// <param name="isCancel"></param>
        /// <returns></returns>
        public ManyResult CancelBookings(Guid[] operationIds, PartDelegate.EditPartSaved editPartSaved, bool isCancel)
        {
            if (operationIds == null || operationIds.Length == 0) return null;
            string message = string.Empty;
            string arrMsg = string.Empty;
            string operation = LocalData.IsEnglish ? "Cancel" : "取消";
            ManyResult results = new ManyResult();
            if (isCancel)
                message = LocalData.IsEnglish
                    ? "After cancel the shipment, all of it's invoices will be removed automatically!"
                    : "取消业务，同时系统也会删除该票业务所有的账单！?";
            else
            {
                operation = LocalData.IsEnglish ? "Available" : "恢复";
                message = LocalData.IsEnglish ? "Sure Available Current Shipment?" : "你真的要恢复这笔业务吗?";
            }

            DialogResult dialogResult = XtraMessageBox.Show(message,
                                               LocalData.IsEnglish ? "Cancel Shipment" : "取消业务",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                SingleResult result = null;
                for (int index = 0; index < operationIds.Length; index++)
                {
                    try
                    {
                        result = OceanExportService.CancelOceanOrder(operationIds[index], LocalData.UserInfo.DefaultCompanyID,
                                 isCancel, LocalData.UserInfo.LoginID, null);
                        if (result != null)
                        {
                            results.Items.Add(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        OceanBookingInfo data = OceanExportService.GetOceanBookingInfo(operationIds[index]);
                        arrMsg += string.Format(LocalData.IsEnglish ? "{0} {1}Shipment failed The reason is" + ex.Message : "{0}业务{1}失败，原因：" + ex.Message, data.No, operation) + Environment.NewLine;
                        continue;
                    }
                }

                if (!string.IsNullOrEmpty(arrMsg))
                {
                    message = arrMsg;
                }
                else
                    message = LocalData.IsEnglish ? "Cancel Shipment Successfully." : "取消任务成功";

                XtraMessageBox.Show(message,
                    LocalData.IsEnglish ? "Cancel Shipment" : "取消业务",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
            }
            return results;
        }


        public SingleResult CancelOrderForFCM(OceanBookingList oceanBookingList, PartDelegate.EditPartSaved editPaerSaved)
        {
            return Cancel(true, oceanBookingList, editPaerSaved);
        }


        private SingleResult Cancel(bool isFCM, OceanBookingList oceanBookingList, PartDelegate.EditPartSaved editPartSaved)
        {
            string message = string.Empty;

            if (oceanBookingList.IsValid)
                message = LocalData.IsEnglish ? "After cancel the shipment, all of it's invoices will be removed automatically!" : "取消业务，同时系统也会删除该票业务所有的账单！?";
            else
                message = LocalData.IsEnglish ? "Sure Available Current Shipment?" : "你真的要恢复这笔业务吗?";


            DialogResult dialogResult = XtraMessageBox.Show(message,
                                               LocalData.IsEnglish ? "Cancel Shipment" : "取消业务",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                SingleResult result;
                try
                {
                    result = OceanExportService.CancelOceanOrder(oceanBookingList.ID, oceanBookingList.CompanyID
                            , oceanBookingList.IsValid, LocalData.UserInfo.LoginID, oceanBookingList.UpdateDate);
                    if (result != null)
                        ServiceClient.GetService<IICPCommonOperationService>()
                                     .UpdateLocalBusinessData(result.GetValue<Guid>("ID"), OperationType.OceanExport);
                    else
                        return null;

                    if (editPartSaved != null)
                    {
                        if (!isFCM)
                        {
                            var business = new BusinessOperationParameter
                            {
                                Context = new BusinessOperationContext
                                {
                                    OperationID = result.GetValue<Guid>("ID"),
                                    OperationNO = oceanBookingList.No,
                                    OperationType = OperationType.OceanExport
                                }
                            };

                            editPartSaved(new object[] { business });
                        }
                        else
                        {
                            editPartSaved(new object[] { result });
                        }
                    }

                }
                catch (Exception ex)
                {

                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                    return null;
                }
                string arrMsg = string.Empty;
                if (oceanBookingList.IsValid)
                {
                    arrMsg = LocalData.IsEnglish ? "Cancel Shipment Successfully." : "这笔业务已经成功取消.";
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, arrMsg);
                }
                else
                {
                    arrMsg = LocalData.IsEnglish ? "Available Shipment Successfully." : "这笔业务已经成功恢复.";
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, arrMsg);
                }

                return result;

            }

            return null;
        }

        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void LoadContainer(string operationNo, Guid oceanBookingId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            string title = (LocalData.IsEnglish ? "Container " : "装箱 ") + OECommonUtility.GetLineNo(operationNo);
            if (values == null)
            {
                values = new Dictionary<string, object>();
            }
            values["OceanBookingId"] = oceanBookingId;
            using (new CursorHelper(Cursors.WaitCursor))
            {

                PartLoader.ShowEditPart<LoadContainerPart>(RootWorkItem, null, values, title, editPartSaved,
                    OEBookingCommandConstants.Command_LoadContainer + oceanBookingId.ToString());
            }
        }

        

        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="MBLNo">MBL No</param>
        /// <param name="editPartSaved"></param>
        public void LoadShip(string operationNo, string MBLNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            StringBuilder strb = new StringBuilder();
            if (string.IsNullOrEmpty(MBLNo) || MBLNo == "0")
                strb.Append("MBLNO");
            if (strb.Length > 0)
            {
                XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }
            OceanMBLInfo mblInfo = OceanExportService.GetOceanMBLInfo(operationNo, MBLNo);
            if(mblInfo==null)
            {
                XtraMessageBox.Show(string.Format(LocalData.IsEnglish ? "MBL information for [{0}] not found!" : "未找到[{0}]的主提单信息,请检查MBL单号是否包含特殊字符!",MBLNo));
                return;
            }
            if (string.IsNullOrEmpty(mblInfo.POLName))
                strb.Append("POLName");
            if (strb.Length > 0)
            {
                XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }

            if ((mblInfo.OEOperationType == FCMOperationType.BULK || mblInfo.OEOperationType == FCMOperationType.LCL)
                && string.IsNullOrEmpty(mblInfo.ContainerNos))
            {
                string message = LocalData.IsEnglish ? "LCL or BULK groceries in the case of box number can not confirm the shipment" : "拼箱或散杂货在没有箱号的情况下，不能进行确认装船";
                XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }



            #region 列表的数据
            //mbl.SONO = CurrentRow.SONO;
            //mbl.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
            //mbl.CarrierName = CurrentRow.CarrierName;
            //mbl.SalesName = CurrentRow.SalesName;
            //mbl.FilerName = CurrentRow.FilerName;
            //mbl.BookingerName = CurrentRow.BookingerName;
            //mbl.OverseasFilerName = CurrentRow.OverseasFilerName;
            #endregion
            string title = LocalData.IsEnglish ? "Confirm LoadShip" : "确认装船";
            PartLoader.ShowEditPartInDialog<LoadShipConfirm>(RootWorkItem, mblInfo, values, title, editPartSaved);

        }


        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="operationID">业务号</param>
        /// <param name="BLNo">MBL No</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void LoadShip(Guid operationID, string BLNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            StringBuilder strb = new StringBuilder();
            if (string.IsNullOrEmpty(BLNo))
                strb.Append("MBLNO");
            if (strb.Length > 0)
            {
                XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }
            OceanMBLInfo mblInfo = OceanExportService.GetOceanMBLInfo(operationID);
            if (string.IsNullOrEmpty(mblInfo.POLName))
                strb.Append("POLName");
            if (strb.Length > 0)
            {
                XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }

            if ((mblInfo.OEOperationType == FCMOperationType.BULK || mblInfo.OEOperationType == FCMOperationType.LCL)
                && string.IsNullOrEmpty(mblInfo.ContainerNos))
            {
                string message = LocalData.IsEnglish ? "LCL or BULK groceries in the case of box number can not confirm the shipment" : "拼箱或散杂货在没有箱号的情况下，不能进行确认装船";
                XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }
            string title = LocalData.IsEnglish ? "Confirm LoadShip" : "确认装船";
            PartLoader.ShowEditPartInDialog<LoadShipConfirm>(RootWorkItem, mblInfo, values, title, editPartSaved);
        }

        /// <summary>
        /// 订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID，等于业务ID</param>
        public void PrintBookingConfirm(Guid oceanBookingId)
        {
            OceanExportPrintHelper.PrintOEBookingConfirmation(oceanBookingId, null);
        }
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID，等于业务ID</param>
        public void PrintBookingProfit(Guid oceanBookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBookingList oceanBookingList = OceanExportService.GetOceanBookingListByIds(new Guid[] { oceanBookingId }).First();
                OceanExportPrintHelper.PrintOEBookingProfit(oceanBookingList);
            }
        }

        /// <summary>
        /// 打印海出装箱单
        /// </summary>
        /// <param name="listData">提单列表</param>
        /// <param name="iscopy">是否副本</param>
        public void PrintLoadContainers(OceanBLList listData, bool iscopy)
        {
            if (listData == null || listData.MBLID == Guid.Empty) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanExportPrintHelper.PrintOELoadContainers(listData, iscopy);
            }
        }

        /// <summary>
        /// 打印海出提货单
        /// </summary>
        /// <param name="listData"></param>
        public void PrintLoadGoods(OceanBLList listData)
        {
            if (listData == null || listData.MBLID == Guid.Empty) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanExportPrintHelper.PrintOELoadGoods(listData);
            }
        }

        /// <summary>
        /// 打印海出提货单
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="mblNo"></param>
        public void PrintLoadGoods(string operationNo, string mblNo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanExportPrintHelper.PrintOELoadGoods(operationNo, mblNo);
            }
        }

        /// <summary>
        /// 打印订舱确认书(宁波)
        /// </summary>
        /// <param name="operationID"></param>
        public void PrintOEBookingConfirmation4NB(Guid operationID)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanExportPrintHelper.PrintOEBookingConfirmation4NB(operationID);
            }
        }

        /// <summary>
        /// 打印提单
        /// </summary>
        /// <param name="listData"></param>
        public void PrintBillOfLoading(OceanBLList listData)
        {
            if (listData == null) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanBLList", listData);
                string no = listData.No.Length <= 4 ? listData.No : listData.No.Substring(listData.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print BL" : "打印提单") + ("-" + no);
                PartLoader.ShowEditPart<NewBLPrintPart>(RootWorkItem, null, stateValues, title, null, null);
            }
        }
        /// <summary>
        /// 打印提单
        /// </summary>
        public void PrintBillOfLoading(Guid blId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBLList oceanBLList = OceanExportService.GetOceanBLListByIds(new Guid[] { blId }).First();
                PrintBillOfLoading(oceanBLList);
            }
        }
        /// <summary>
        /// 打印提单
        /// </summary>
        /// <param name="listData"></param>
        public void PrintBillOfLoading(string operationNo, string blNo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result = OceanExportService.GetOceanBLList(null, operationNo, blNo, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, DateSearchDispatchType.All, null, null, 10, ReleaseBLSearchStatue.All, ReleaseRCSearchStatue.All, ApplyReleaseSearchStatue.All, ReceiveRCSearchStatue.All, null);
                OceanBLList oceanBLList = JSONSerializerHelper.DeserializeFromJson<List<OceanBLList>>(result).First();
                PrintBillOfLoading(oceanBLList);
            }
        }

        /// <summary>
        /// 邮件-提单确认打印
        /// </summary>
        public void MainPrintBillOfLoading(Guid blId, Message.ServiceInterface.Message message)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBLList oceanBLList = OceanExportService.GetOceanBLListByIds(new Guid[] { blId }).First();
                if (oceanBLList == null) return;
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> stateValues = new Dictionary<string, object>();
                    stateValues.Add("OceanBLList", oceanBLList);
                    string no = oceanBLList.No.Length <= 4 ? oceanBLList.No : oceanBLList.No.Substring(oceanBLList.No.Length - 4, 4);
                    string title = (LocalData.IsEnglish ? "Print BL" : "打印提单") + ("-" + no);
                    PartLoader.ShowEditPart<NewBLPrintPart>(RootWorkItem, message, stateValues, title, null, null);
                }
            }
        }

        /// <summary>
        /// 当前业务下是否有柜
        /// </summary>
        /// <param name="oceanBookingId"></param>
        /// <returns></returns>
        public bool IsShipmentHasContainer(Guid oceanBookingId)
        {
            bool result = false;
            List<OceanContainerList> containerList = OceanExportService.GetOceanContainerList(oceanBookingId);
            if (containerList != null && containerList.Count > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId">业务Id</param>
        /// <param name="operationType">业务类型</param>
        public void OpenBill(Guid operationId, OperationType operationType)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(operationId, operationType);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }
        /// <summary>
        /// 显示核销单列表界面
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationNo"></param>
        public void OpenVerifiSheet(Guid operationId, string operationNo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string no = operationNo.Length <= 4 ? operationNo : operationNo.Substring(operationNo.Length - 4, 4);
                string title = LocalData.IsEnglish ? "Verifi.Sheet" : "核销单" + ("-" + no);
                object[] data = new object[2];
                data[0] = operationId;
                data[1] = operationNo;
                PartLoader.ShowEditPart<VerifiSheetEditPart>(RootWorkItem,
                    data,
                    null,
                    title,
                    null,
                    OEBLCommandConstants.Command_VerifiSheet + operationId.ToString());
            }
        }
        /// <summary>
        /// 新增MBL
        /// </summary>
        /// <param name="editPartSaved"></param>
        /// <param name="values"></param>
        public void AddMBL(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanMBLInfo newData = new OceanMBLInfo();
                //newData.ContactName = Workitem.State["ContractName"] == null ? "" : Workitem.State["ContractName"].ToString();
                //newData.ContactID = Guid.NewGuid();
                newData.IssueByID = newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.IssueByName = newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.NumberOfOriginal = 3;
                newData.VoyageShowType = VoyageShowType.All;
                newData.ReleaseType = FCMReleaseType.Original;
                newData.IssueType = IssueType.Normal;
                newData.IsValid = true;
                newData.WoodPacking = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                newData.State = OEBLState.Draft;
                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                newData.QuantityUnitID = normalDictionary.ID;
                newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                newData.WeightUnitID = normalDictionary.ID;
                newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                newData.MeasurementUnitID = normalDictionary.ID;
                newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                #endregion

                newData.MBLID = Guid.Empty;
                string titleNo = LocalData.IsEnglish ? "Add MBL" : "新增MBL";
                PartLoader.ShowEditPart<MBLEditPart>(RootWorkItem, newData, values, titleNo, editPartSaved, titleNo);


            }
        }
        /// <summary>
        /// CopyAMSTo
        /// </summary>
        public void CopyAMSTo(WorkItem workItem, OceanBLList oceanBLList)
        {
            if (oceanBLList.BLType != FCMBLType.HBL)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                return;
            }
            else
            {
                PartLoader.ShowEditPartInDialog<CopyAMSToHBL>(Workitem, oceanBLList,
                                                                                        "Copy AMS To Other HBL", null);
            }
        }
        /// <summary>
        /// 新增HBL
        /// </summary>
        /// <param name="editPartSaved"></param>
        /// <param name="values"></param>
        public void AddHBL(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanHBLInfo newData = new OceanHBLInfo();
                //newData.ContactName = Workitem.State["ContractName"] == null ? "" : Workitem.State["ContractName"].ToString();
                //newData.ContactID = Guid.NewGuid();
                newData.IssueByID = newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.IssueByName = newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.NumberOfOriginal = 3;
                newData.VoyageShowType = VoyageShowType.All;
                newData.ReleaseType = FCMReleaseType.Original;
                newData.IssueType = IssueType.Normal;
                newData.IsValid = true;
                newData.State = OEBLState.Draft;
                newData.WoodPacking = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                //normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                //newData.PaymentTermID = normalDictionary.ID;
                //newData.PaymentTermName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                newData.QuantityUnitID = normalDictionary.ID;
                newData.QuantityUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                newData.WeightUnitID = normalDictionary.ID;
                newData.WeightUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                newData.MeasurementUnitID = normalDictionary.ID;
                newData.MeasurementUnitName = normalDictionary.EName;
                newData.IsToAgent = true;
                newData.AMSEntry = AMSEntryType.Unknown;
                #endregion

                string titleNo = LocalData.IsEnglish ? "Add HBL" : "新增HBL";
                PartLoader.ShowEditPart<HBLEditPart>(RootWorkItem, newData, values, titleNo, editPartSaved, typeof(HBLEditPart).ToString());
            }
        }
        /// <summary>
        /// 新增HBL
        /// </summary>
        /// <param name="editPartSaved"></param>
        /// <param name="values"></param>
        public void AddDeclareHBL(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                DeclareHBLInfo newData = new DeclareHBLInfo();
                //newData.ContactName = Workitem.State["ContractName"] == null ? "" : Workitem.State["ContractName"].ToString();
                //newData.ContactID = Guid.NewGuid();
                newData.IssueByID = newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.IssueByName = newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.NumberOfOriginal = 3;
                newData.VoyageShowType = VoyageShowType.All;
                newData.ReleaseType = FCMReleaseType.Original;
                newData.IssueType = IssueType.Normal;
                newData.IsValid = true;
                newData.State = OEBLState.Draft;
                newData.WoodPacking = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                //normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                //newData.PaymentTermID = normalDictionary.ID;
                //newData.PaymentTermName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                newData.QuantityUnitID = normalDictionary.ID;
                newData.QuantityUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                newData.WeightUnitID = normalDictionary.ID;
                newData.WeightUnitName = normalDictionary.EName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                newData.MeasurementUnitID = normalDictionary.ID;
                newData.MeasurementUnitName = normalDictionary.EName;
                newData.IsToAgent = true;
                newData.AMSEntry = AMSEntryType.Unknown;
                #endregion

                string titleNo = LocalData.IsEnglish ? "Add DeclareHBL" : "新增DeclareHBL";
                PartLoader.ShowEditPart<DeclareHBLEditPart>(RootWorkItem, newData, values, titleNo, editPartSaved, typeof(DeclareHBLEditPart).ToString());
            }
        }
        /// <summary>
        /// 编辑MBL
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="mblNo">MBL No.</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void EditMBL(string operationNo, string mblNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                MergeDictionary(stateValues, values);
                stateValues["OperationNo"] = operationNo;
                stateValues["MBLNo"] = mblNo;
                string title = LocalData.IsEnglish ? "Edit MBL: " + mblNo : "编辑MBL: " + mblNo;
                PartLoader.ShowEditPart<MBLEditPart>(RootWorkItem, null, stateValues, title, editPartSaved, typeof(MBLEditPart).ToString() + mblNo);
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-MBL", string.Format("编辑MBL;MBL NO[{0}]", mblNo));
            }
        }
        /// <summary>
        /// 编辑HBL
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="hblNo">HBL No.</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved">保存后回调处理方法</param>
        public void EditHBL(string operationNo, string hblNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                MergeDictionary(stateValues, values);
                stateValues["OperationNo"] = operationNo;
                stateValues["HBLNo"] = hblNo;
                string title = LocalData.IsEnglish ? "Edit HBL " + hblNo.TrimStart() : "编辑HBL " + hblNo.TrimStart();
                PartLoader.ShowEditPart<HBLEditPart>(RootWorkItem, null, stateValues, title, editPartSaved, typeof(HBLEditPart).ToString() + hblNo);
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-HBL", string.Format("编辑HBL;HBL NO[{0}]", hblNo));
            }

        }
        /// <summary>
        /// 编辑DeclareHBL
        /// </summary>
        /// <param name="HblID">Hbl ID</param>
        /// <param name="hblNo">HBL No.</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved">保存后回调处理方法</param>
        public void EditDeclareHBL(Guid HblID, string hblNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                MergeDictionary(stateValues, values);
                stateValues["HblID"] = HblID;
                stateValues["HBLNo"] = hblNo;
                string title = LocalData.IsEnglish ? "Edit Declare HBL " + hblNo.TrimStart() : "编辑DeclareHBL " + hblNo.TrimStart();
                PartLoader.ShowEditPart<DeclareHBLEditPart>(RootWorkItem, null, stateValues, title, editPartSaved, typeof(DeclareHBLEditPart).ToString() + hblNo);
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-DeclareHBL", string.Format("编辑DeclareHBL;DeclareHBL NO[{0}]", hblNo));
            }

        }
        private static void MergeDictionary(Dictionary<string, object> target, IDictionary<string, object> source)
        {

            if (source == null || source.Count == 0)
                return;
            foreach (KeyValuePair<string, object> pair in source)
            {
                target.Add(pair.Key, pair.Value);
            }

        }
        /// <summary>
        /// 打开派车单
        /// </summary>
        /// <param name="bookingId">订仓单Id，和业务ID相同</param>
        /// <param name="operationNo">业务号</param>
        ///<param name="bookingInfo">订仓单信息，可为null</param>
        ///<param name="values">自定义参数信息</param>
        public void OpenTruckOrder(Guid bookingId, string operationNo, OceanBookingList bookingInfo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                MergeDictionary(stateValues, values);
                if (bookingInfo != null)
                {
                    stateValues.Add("Booking", bookingInfo);
                }
                else
                {
                    stateValues.Add("BookingId", bookingId);
                }
                string title = LocalData.IsEnglish ? "Truck Service" : "拖车服务";
                PartLoader.ShowEditPart<OceanTruckEditPart>(RootWorkItem,
                    null,
                    stateValues,
                    title + OEUtility.GetLineNo(operationNo),
                    editPartSaved,
                    OEBookingCommandConstants.Command_Truck + bookingId.ToString());
            }
        }

        /// <summary>
        /// 打开指定业务号下的提单列表
        /// </summary>
        public void OpenBillOfLoadingList(string operationNo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (string.IsNullOrEmpty(operationNo)) return;
                OEBLWorkitem blWorkitem = RootWorkItem.WorkItems.AddNew<OEBLWorkitem>();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("RefNo", operationNo);
                blWorkitem.Init(dic);
                blWorkitem.Run();
            }
        }
        /// <summary>
        /// 打开订舱单列表
        /// </summary>
        /// <param name="bookIngListQuery"></param>
        public void OpenBookingList(BookingListQueryCriteria queryCriteria)
        {

            WorkItemBooking bookWorkItem = RootWorkItem.WorkItems.AddNew<WorkItemBooking>();
            bookWorkItem.Show(queryCriteria);
        }

        /// <summary>
        /// 打开报关单
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="editPartSaved"></param>
        /// <param name="values"></param>
        public void OpenCustomsOrder(Guid operationId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {

            string title = LocalData.IsEnglish ? "Customs Order" : "报关委托";
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            MergeDictionary(stateValues, values);
            stateValues["OperationId"] = operationId;
            PartLoader.ShowEditPart<OceanCustomsEditPart>(RootWorkItem,
                null,
                stateValues,
                title,
                editPartSaved, OEBookingCommandConstants.Command_Customs + operationId.ToString());
        }

        //   /// <summary>
        // /// 复制订舱单
        // /// </summary>
        // /// <param name="showCriteria"></param>
        // /// <param name="values"></param>
        // /// <param name="editPartSaved"></param>
        //public void CopyBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        //{
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        string title = (LocalData.IsEnglish ? "Copy Booking: " : "复制订舱单: ") + showCriteria.OperationNo;
        //        string id = showCriteria.BillNo.ToString();
        //        PartLoader.ShowEditPart<Booking.BookingBaseEditPart>(this.RootWorkItem, showCriteria, EditMode.Copy, null, title, editPartSaved, id);
        //    }
        //}
        /// <summary>
        /// 发送邮件信息时候进行判断
        /// </summary>
        /// <param name="oceanBooking">订舱的实体对象</param>
        /// <returns>返回错误信息</returns>
        public string GetEmailSendValidationInfo(OceanBookingInfo oceanBooking)
        {
            string retunStr;
            var promptCh = new StringBuilder();
            var promptEn = new StringBuilder();
            if (string.IsNullOrEmpty(oceanBooking.OceanShippingOrderNo) && oceanBooking.NoCheck == true)
            {
                promptCh.Append(" 订舱号为空 ");
                promptEn.Append(" Business number is empty ");
            }
            if (string.IsNullOrEmpty(oceanBooking.POLName) && oceanBooking.POLCheck == true)
            {
                promptCh.Append(" 装货港为空 ");
                promptEn.Append(" The port of loading is empty ");
            }
            if (string.IsNullOrEmpty(oceanBooking.PODName) && oceanBooking.PODCheck == true)
            {
                promptCh.Append(" 卸货港为空 ");
                promptEn.Append(" Port of discharge is empty ");
            }
            if (string.IsNullOrEmpty(oceanBooking.ContainerDescription == null ?
                                         string.Empty : oceanBooking.ContainerDescription.ToString())
                 && oceanBooking.ContainerCheck == true)
            {
                promptCh.Append(" 箱信息为空 ");
                promptEn.Append(" Container information is empty ");

            }
            if (string.IsNullOrEmpty(oceanBooking.SalesName) && oceanBooking.SalesNameCheck == true)
            {
                promptCh.Append(" 揽货人为空 ");
                promptEn.Append(" Freight for air ");
            }
            if (string.IsNullOrEmpty(oceanBooking.DOCClosingDate == null ?
                                          string.Empty : oceanBooking.DOCClosingDate.ToString())
                && oceanBooking.ClosingDateCheck == true)
            {
                promptCh.Append(" 截文件日为空 ");
                promptEn.Append(" Truncated file is empty ");
            }

            if (string.IsNullOrEmpty(oceanBooking.CustomerName)
                && oceanBooking.CustomerCheck == true)
            {
                promptCh.Append(" 客户信息为空 ");
                promptEn.Append(" Customer information is empty ");
            }
            if ((string.IsNullOrEmpty(oceanBooking.BookingByName)
                || string.IsNullOrEmpty(oceanBooking.BookingByID.ToString()))
                && oceanBooking.BookingByCheck == true)
            {
                promptCh.Append(" 订舱员为空 ");
                promptEn.Append(" Booking for flight ");

            }
            if (!string.IsNullOrEmpty(promptCh.ToString()) || !string.IsNullOrEmpty(promptEn.ToString()))
            {
                promptCh.Append("，无法发送邮件.");
                promptEn.Append(", can't send mail.");
            }
            retunStr = LocalData.IsEnglish ? promptEn.ToString() : promptCh.ToString();
            return retunStr;
        }

        /// <summary>
        /// 返回消息实体类
        /// </summary>
        /// <param name="type">发送类型</param>
        /// <param name="way">发送方向</param>
        /// <param name="sendTo">接收人邮箱</param>
        /// <param name="sendFrom">发送人邮箱</param>
        /// <param name="formType">表单类型</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationId">操作ID</param>
        /// <param name="formId">表单ID</param>
        /// <param name="body">发送内容</param>
        /// <param name="subject">主题</param>
        /// <param name="cc">邮件抄送地址</param>
        /// <param name="action">操作动作</param>
        /// <param name="eventObjects">事件列表实体</param>
        /// <param name="attachmentContents">邮件附件信息</param>
        /// <returns></returns>
        public Message.ServiceInterface.Message
            CreateMessageInfo(MessageType type,
            MessageWay way, string sendTo, string sendFrom, FormType formType,
            OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
            string action, EventObjects eventObjects, List<AttachmentContent> attachmentContents)
        {
            // 邮件发送的消息实体
            var message = new Message.ServiceInterface.Message();

            message.Type = type;
            message.Way = way;
            //收件人邮箱地址
            message.SendTo = sendTo;
            //发件人邮箱地址
            message.SendFrom = sendFrom;
            //邮件抄送地址
            message.CC = cc;

            message.UserProperties = new MessageUserPropertiesObject
            {
                FormType = formType,
                OperationType = operationType,
                OperationId = operationId,
                FormId = formId
            };
            if (!string.IsNullOrEmpty(action))
            {
                //MessageUserPropertiesObject(消息自定义属性类)

                message.UserProperties.Action = action;
            }
            if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
            {
                message.Body = body;
                message.Subject = subject;
            }
            if (attachmentContents != null && attachmentContents.Any())
            {
                message.Attachments = attachmentContents;
            }
            if (eventObjects != null)
                message.UserProperties.EventInfo = eventObjects;
            return message;

        }

        /// <summary>
        /// 根据id读取数据
        /// </summary>
        /// <param name="id">业务号</param>
        /// <param name="type">类别</param>
        /// <param name="customerId">代理的ID</param>
        /// <param name="notequal">需要执行查询当前联系人关联客户不属于当前业务的代理</param>
        /// <param name="equal">需要执行查询当前联系人关联客户属于当前业务的代理</param>
        /// <returns>返回拼接的字符（接收人邮箱地址，接收人名称，抄送人邮箱）</returns>
        public string MailInformation(Guid id, string type, Guid? customerId, bool notequal, bool equal)
        {
            string retuRned = string.Empty;
            string strTerm = string.Empty;
            List<MailInformation> mailInformation = OceanExportService.GetContactEmail(id, type, customerId, notequal, equal);
            if (mailInformation.Count > 0)
            {
                retuRned = GetMailInformation(mailInformation);
            }

            return retuRned;

        }

        /// <summary>
        /// 对数据集合循环，生成字符串
        /// </summary>
        /// <param name="mailInformation">MailInformation对象集合</param>
        /// <returns></returns>
        public string GetMailInformation(List<MailInformation> mailInformation)
        {
            string sendTo = string.Empty;
            string name = string.Empty;
            string cc = string.Empty;
            foreach (var item in mailInformation)
            {
                //当前联系人是接人
                if (!string.IsNullOrEmpty(sendTo) && !string.IsNullOrEmpty(name) && item.CC == false)
                {
                    sendTo = sendTo + ";" + item.Email.Trim();
                    name = name + "," + item.Name.Trim();

                }
                //当前客户是抄送人
                else if (item.CC == true && name.Contains(item.Name) == false)
                {
                    if (!string.IsNullOrEmpty(cc))
                    {
                        cc = cc + ";" + item.Email.Trim();
                    }
                    else
                    {
                        cc = item.Email.Trim();
                    }

                }
                //第一次赋值
                else
                {
                    sendTo = item.Email.Trim();
                    name = item.Name.Trim();
                }
            }
            if (!string.IsNullOrEmpty(sendTo) && !string.IsNullOrEmpty(name))
            {
                return sendTo + "|" + name.TrimEnd(',') + "|" + cc;
            }
            return string.Empty;
        }

        /// <summary>
        /// 发送邮件给客户通知取消订舱
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        public string MailCustomerForCancelBooking(bool isEnglish, Guid oceanBookingId)
        {
            OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            return MailCustomerForCancelBooking(isEnglish, oceanBookingInfo);

        }
        /// <summary>
        /// 发送邮件给客户通知取消订舱
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        public string MailCustomerForCancelBooking(bool isEnglish, OceanBookingInfo oceanBookingInfo)
        {
            var promptCh = new StringBuilder();
            var promptEn = new StringBuilder();
            oceanBookingInfo.POLCheck = true;
            oceanBookingInfo.PODCheck = true;
            oceanBookingInfo.ContainerCheck = true;

            //读取当前用户的邮箱地址信息
            string mailInformation = MailInformation(oceanBookingInfo.ID, "SI", null, false, false);
            string[] strSpit = null;
            if (!string.IsNullOrEmpty(mailInformation))
            {
                strSpit = mailInformation.Split('|');
            }
            else
            {
                strSpit = new string[] { " ", " ", " " };
            }

            string top = GetEmailSendValidationInfo(oceanBookingInfo);
            if (!string.IsNullOrEmpty(top))
            {
                promptCh.Append(top);
                promptEn.Append(top);
            }
            if (!string.IsNullOrEmpty(promptCh.ToString()) || !string.IsNullOrEmpty(promptEn.ToString()))
            {
                return LocalData.IsEnglish ? promptEn.ToString() : promptCh.ToString();
            }
            EventCode eventCodes = EventCodeList("SOAC");
            if (strSpit != null && strSpit.Length > 1)
            {
                // 邮件发送的消息实体

                var eventObjects = new EventObjects
                {
                    OperationID = oceanBookingInfo.ID,
                    OperationType = OperationType.OceanExport,
                    FormID = oceanBookingInfo.ID,
                    FormType = FormType.Unknown,
                    Code = eventCodes.Code,
                    Description = eventCodes.Subject,
                    Subject = eventCodes.Subject,
                    Priority = MemoPriority.Normal,
                    UpdateDate = DateTime.Now,
                    Owner = LocalData.UserInfo.LoginName,
                    UpdateBy = LocalData.UserInfo.LoginID,
                    CategoryName = eventCodes.Category,
                    IsShowAgent = true,
                    IsShowCustomer = true,
                    Type = MemoType.EmailLog
                };
                var message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, strSpit[0],
                                                LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanBookingInfo.ID, Guid.Empty,
                                                string.Empty, string.Empty, string.Empty,
                                                "SOAC", eventObjects, null);
                //传输邮件模版的字符数组
                object[] values = { oceanBookingInfo, strSpit[1] };
                MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, "ConfirCancelBooking", values);

            }
            else
            {
                return LocalData.IsEnglish ? "Supplementary food contact for air, unable to send mail, please add your contact partner." :
                "补料联系人为空，无法发送邮件，请添加补料联系人.";
            }
            return string.Empty;
        }

        /// <summary>
        /// 客户确认补料
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="oceanHblInfo">Hbl提单信息</param>
        /// <param name="oceanMblInfo">Mbl提单信息</param>
        /// <returns></returns>
        public string MailCustomerAskForConfirmSI(bool isEnglish, Guid oceanBookingId, OceanHBLInfo oceanHblInfo,
            OceanMBLInfo oceanMblInfo)
        {
            return MailCustomerAskForConfirmSI(isEnglish, string.Empty, oceanBookingId, oceanHblInfo, oceanMblInfo);
        }

        /// <summary>
        /// 客户确认补料
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="mailInformation">邮箱地址</param>
        /// <param name="oceanBookingId"></param>
        /// <param name="oceanHblInfo">Hbl提单信息</param>
        /// <param name="oceanMblInfo">Mbl提单信息</param>
        /// <returns></returns>
        public string MailCustomerAskForConfirmSI(bool isEnglish, string mailInformation, Guid oceanBookingId, OceanHBLInfo oceanHblInfo, OceanMBLInfo oceanMblInfo)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            return MailCustomerAskForConfirmSI(isEnglish, mailInformation, oceanbookinginfo, oceanHblInfo, oceanMblInfo);
        }

        /// <summary>
        /// 客户确认补料
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="oceanHblInfo">Hbl提单信息</param>
        /// <param name="oceanMblInfo">Mbl提单信息</param>
        /// <returns></returns>
        public string MailCustomerAskForConfirmSI(bool isEnglish, string mailInformation, OceanBookingInfo oceanBookingInfo, OceanHBLInfo oceanHblInfo, OceanMBLInfo oceanMblInfo)
        {
            var promptCh = new StringBuilder();
            var promptEn = new StringBuilder();

            var userName = LocalData.UserInfo.LoginName.Trim();

            oceanBookingInfo.NoCheck = true;
            Guid oceanBookingId = oceanBookingInfo.ID;
            if (string.IsNullOrEmpty(mailInformation))
            {
                //读取当前用户的邮箱地址信息
                mailInformation = MailInformation(oceanBookingId, "SI", null, true, false);
            }

            string[] strSpit = null;
            if (!string.IsNullOrEmpty(mailInformation))
            {
                strSpit = mailInformation.Split('|');
            }
            else
            {
                strSpit = new string[] { " ", " ", " " };
            }
            string top = GetEmailSendValidationInfo(oceanBookingInfo);
            if (!string.IsNullOrEmpty(top))
            {
                promptCh.Append(top);
                promptEn.Append(top);

            }
            if (!string.IsNullOrEmpty(promptCh.ToString()) || !string.IsNullOrEmpty(promptEn.ToString()))
            {
                return LocalData.IsEnglish ? promptEn.ToString() : promptCh.ToString();
            }

            if (strSpit != null && strSpit.Length > 1)
            {
                var bookingBlInfo = oceanBookingInfo.OceanMBLs.FirstOrDefault();
                if (bookingBlInfo == null)
                {
                    return
                         LocalData.IsEnglish ?
                         "The current business without the bill of lading information, unable to send E-mail."
                         : "当前业务无提单信息，无法发送邮件。";

                }
                string subject = string.Empty;
                StringBuilder body = new StringBuilder();
                string no = oceanHblInfo == null ? oceanMblInfo.No : oceanHblInfo.No;
                FCMReleaseType ReleaseType = oceanHblInfo == null ? oceanMblInfo.ReleaseType : oceanHblInfo.ReleaseType;
                Guid id = oceanHblInfo == null ? oceanMblInfo.ID : oceanHblInfo.ID;
                UserDetailInfo userDetail = ServiceClient.GetService<IUserService>().GetUserDetailInfo(LocalData.UserInfo.LoginID);
                if (dt == null)
                {
                    dt = ServiceClient.GetService<IOceanExportService>().GetEmailNotice(LocalData.UserInfo.DefaultDepartmentID);
                }
                string Notice = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Notice = dt.Rows[0][0].ToString();
                }
                string str = GetCompany();
                if (isEnglish == false)
                {
                    //主题
                    subject = "提单确认" + "BL#" + no + "(SO#" + oceanBookingInfo.OceanShippingOrderNo + ")";
                    //内容
                    body.Append("<html>");
                    body.Append("<head>");
                    body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                    body.Append(" <style type=" + "text/css" + ">");
                    body.Append(" .MsoNormal");
                    body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                    body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                    body.Append(" </style>");
                    body.Append("</head>");
                    body.Append("<body><div class=" + "MsoNormal" + ">");
                    body.Append("Hi" + "  " + strSpit[1] + "," + "<br/><br/>");
                    body.Append("附件的提单请尽快确认回复，谢谢." + "<br/>");
                    body.Append("该提单的放单方式为" + ReleaseType + "." + "<br/><br/>");
                    body.Append("B.rgds,<br /><br />");
                    body.Append(userName + "<br />");
                    body.Append("Cityocean Logistics Co.,LTD<br />");
                    body.Append(str + "<br />");
                    body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                    body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                    body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                    body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p>");
                    body.Append("<p>" + Notice + "</p>");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");
                }
                else
                {
                    //主题
                    subject = "BL Confirmation" + "BL#" + no + "(SO#" + oceanBookingInfo.OceanShippingOrderNo + ")";
                    //内容
                    body.Append("<html>");
                    body.Append("<head>");
                    body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                    body.Append(" <style type=" + "text/css" + ">");
                    body.Append(" .MsoNormal");
                    body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                    body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                    body.Append(" </style>");
                    body.Append("</head>");
                    body.Append("<body><div class=" + "MsoNormal" + ">");
                    body.Append("Hi" + "  " + strSpit[1] + "," + "<br/><br/>");
                    body.Append("Please confirm the enclosed BL copy." + "<br/>");
                    body.Append("Enclosed attachment(s) is the SO copy." + "<br/>");
                    body.Append("Note: The BL is " + ReleaseType + "." + "<br/><br/>");
                    body.Append("B.rgds,<br /><br />");
                    body.Append(userName + "<br />");
                    body.Append("Cityocean Logistics Co.,LTD<br />");
                    body.Append(str + "<br />");
                    body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                    body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                    body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                    body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p>");
                    body.Append("<p>" + Notice + "</p>");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");
                }
                EventCode eventCodes = EventCodeList("BLCfm");
                var eventObjects = new EventObjects
                {
                    OperationID = oceanBookingInfo.ID,
                    OperationType = OperationType.OceanExport,
                    FormID = oceanBookingInfo.ID,
                    FormType = FormType.Unknown,
                    Code = eventCodes.Code,
                    Description = eventCodes.Subject,
                    Subject = eventCodes.Subject,
                    Priority = MemoPriority.Normal,
                    UpdateDate = DateTime.Now,
                    Owner = LocalData.UserInfo.LoginName,
                    UpdateBy = LocalData.UserInfo.LoginID,
                    CategoryName = eventCodes.Category,
                    IsShowAgent = true,
                    IsShowCustomer = true,
                    Type = MemoType.EmailLog
                };
                // 邮件发送的消息实体
                Message.ServiceInterface.Message message = null;
                Message.ServiceInterface.Message oldmessage = new Message.ServiceInterface.Message();
                oldmessage = MailSoCopyToAllCustomers(oceanBookingInfo, "SI");
                if (oldmessage == null)
                {
                    message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanBookingId, id,
                                                body.ToString(), subject, strSpit[2], "BLCfm", eventObjects, null);

                }
                else
                {
                    body.Append("<div class=" + "MsoNormal" + ">");
                    body.Append("---------Original message-------<br/>");
                    body.Append("Sender:" + oldmessage.SendFrom + "<br/>");
                    body.Append("Sent On:" + oldmessage.CreateDate + "<br/>");
                    body.Append("CC:" + oldmessage.CC + "<br/>");
                    body.Append("To:" + oldmessage.SendTo + "<br />");
                    body.Append("Subject:" + oldmessage.Subject + "<br />");
                    body.Append("</div>");
                    body.Append(oldmessage.Body);
                    subject = oldmessage.Subject == "" ? subject : (oldmessage.Subject + " && ") + subject;
                    message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanBookingId, id,
                                                body.ToString(), subject, strSpit[2], "BLCfm", eventObjects, null);
                }

                MainPrintBillOfLoading(id, message);
            }
            else
            {
                return LocalData.IsEnglish ? "Supplementary food contact for air, unable to send mail, please add your contact partner." :
                                             "补料联系人为空，无法发送邮件，请添加补料联系人.";
            }
            return string.Empty;
        }

        /// <summary>
        /// 向代理确认提单
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <returns></returns>
        public string MailALLBLCopyToAgent(bool isEnglish, Guid oceanBookingId, PartDelegate.EditPartSaved editPartSaved)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            return MailALLBLCopyToAgent(isEnglish, oceanbookinginfo, editPartSaved);
        }

        public string MailALLBLCopyToAgent(bool isEnglish, OceanBookingInfo oceanBookingInfo, PartDelegate.EditPartSaved editPartSaved)
        {
            if (oceanBookingInfo == null)
                return string.Empty;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string box = oceanBookingInfo.ContainerDescription == null ? string.Empty : oceanBookingInfo.ContainerDescription.ToString();
                string userName;
                userName = LocalData.UserInfo.LoginName.Trim();
                //读取当前代理的邮箱地址信息
                string mailInformation = MailInformation(oceanBookingInfo.ID, "SI", oceanBookingInfo.AgentID, false, true);
                string[] strSpit = null;
                if (!string.IsNullOrEmpty(mailInformation))
                {
                    strSpit = mailInformation.Split('|');
                }
                else
                {
                    strSpit = new string[] { " ", " ", " " };
                }

                if (strSpit != null && strSpit.Length > 0)
                {
                    var subject = string.Empty;
                    StringBuilder body = new StringBuilder();

                    UserDetailInfo userDetail = ServiceClient.GetService<IUserService>().GetUserDetailInfo(LocalData.UserInfo.LoginID);
                    if (dt == null)
                    {
                        dt = ServiceClient.GetService<IOceanExportService>().GetEmailNotice(LocalData.UserInfo.DefaultDepartmentID);
                    }
                    string Notice = string.Empty;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Notice = dt.Rows[0][0].ToString();
                    }
                    DateTime docClosingDate = new DateTime();
                    if (oceanBookingInfo.DOCClosingDate != null)
                    {
                        docClosingDate = DateTime.Parse(oceanBookingInfo.DOCClosingDate.ToString());
                    }
                    string str = GetCompany();
                    if (isEnglish == false)
                    {
                        //主题
                        subject = "提单确认" + "(SO#" + oceanBookingInfo.OceanShippingOrderNo + ")";
                        //内容
                        body.Append("<html>");
                        body.Append("<head>");
                        body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                        body.Append(" <style type=" + "text/css" + ">");
                        body.Append(" .MsoNormal");
                        body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                        body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                        body.Append(" </style>");
                        body.Append("</head>");
                        body.Append("<body><div class=" + "MsoNormal" + ">");
                        body.Append("Hi" + "  " + strSpit[1] + "," + "<br/><br/>");
                        body.Append("附件的提单请尽快确认回复，谢谢." + "<br/>");
                        //body.Append("该提单的放单方式为" + ReleaseType + "." + "<br/><br/>");
                        body.Append("B.rgds,<br /><br />");
                        body.Append(userName + "<br />");
                        body.Append("Cityocean Logistics Co.,LTD<br />");
                        body.Append(str + "<br />");
                        body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                        body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                        body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                        body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p><br/><br/>");
                        body.Append("1. 请务必于" + docClosingDate.ToString("MM/dd HH:mm") + "日期前确认所有提单资料，逾期将产生改单费USD80/BILL." + "<br/>");
                        body.Append("2. MB/L 的放单信息在ETA前10天才能查询，请联系vicki@cityocean.com。" + "<br/>");
                        body.Append("</div>");
                        body.Append("</body>");
                        body.Append("</html>");
                    }
                    else
                    {
                        //主题
                        subject = "BL Confirmation" + "(SO#" + oceanBookingInfo.OceanShippingOrderNo + ")";
                        //内容
                        body.Append("<html>");
                        body.Append("<head>");
                        body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                        body.Append(" <style type=" + "text/css" + ">");
                        body.Append(" .MsoNormal");
                        body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                        body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                        body.Append(" </style>");
                        body.Append("</head>");
                        body.Append("<body><div class=" + "MsoNormal" + ">");
                        body.Append("Hi" + "  " + strSpit[1] + "," + "<br/><br/>");
                        body.Append("Please confirm the enclosed BL copy." + "<br/>");
                        body.Append("Enclosed attachment(s) is the SO copy." + "<br/>");
                        //body.Append("Note: The BL is " + ReleaseType + "." + "<br/><br/>");
                        body.Append("B.rgds,<br /><br />");
                        body.Append(userName + "<br />");
                        body.Append("Cityocean Logistics Co.,LTD<br />");
                        body.Append(str + "<br />");
                        body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                        body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                        body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                        body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p><br/><br/>");
                        body.Append("1.Please confirm draft BL before" + docClosingDate.ToString("MM/dd HH:mm") + ",otherwise it will be occured amendment fee USD80/bill" + "<br/>");
                        body.Append("2.MBL release will be approval within ETA 10 days,you can check MBL release to Vicki directly.E-Mail: vicki@cityocean.com" + "<br/>");
                        body.Append("</div>");
                        body.Append("</body>");
                        body.Append("</html>");
                    }
                    EventCode eventCodes = EventCodeList("BLCfmAgt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBookingInfo.ID,
                        OperationType = OperationType.OceanExport,
                        FormID = oceanBookingInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCodes.Code,
                        Description = eventCodes.Subject,
                        Subject = eventCodes.Subject,
                        Priority = MemoPriority.Normal,
                        UpdateDate = DateTime.Now,
                        Owner = LocalData.UserInfo.LoginName,
                        UpdateBy = LocalData.UserInfo.LoginID,
                        CategoryName = eventCodes.Category,
                        IsShowAgent = true,
                        IsShowCustomer = true,
                        Type = MemoType.EmailLog

                    };
                    // 邮件发送的消息实体

                    List<AttachmentContent> attachmentContents = new List<AttachmentContent>();

                    var uplodHBLFile = ClientFileService.GetDocumentListByDocumentType(oceanBookingInfo.ID, DocumentType.SIHBL);
                    var uplodMBLFile = ClientFileService.GetDocumentListByDocumentType(oceanBookingInfo.ID, DocumentType.SIMBL);
                    if (uplodMBLFile.Any())
                    {
                        foreach (var info in uplodMBLFile)
                        {
                            //Bit 转string 判断当前是否存在 
                            string contents = Encoding.Default.GetString(info.Content);
                            AttachmentContent attachment = new AttachmentContent();
                            attachment.Name = info.Name;
                            if (!string.IsNullOrEmpty(contents))
                            {
                                attachment.Size = BitConverter.ToInt64(info.Content, 0);
                            }
                            attachment.Content = info.Content;
                            attachment.DisplayName = "M";
                            string path = AppDomain.CurrentDomain.BaseDirectory;
                            path = Path.Combine(path, "filetemp");
                            path += "\\" + info.Name;
                            IOHelper.WriteToDisk(path, info.Content);
                            attachment.ClientPath = path;
                            attachmentContents.Add(attachment);
                        }
                    }
                    if (uplodHBLFile.Any())
                    {
                        foreach (var info in uplodHBLFile)
                        {
                            //Bit 转string 判断当前是否存在 
                            string contents = Encoding.Default.GetString(info.Content);
                            AttachmentContent attachment = new AttachmentContent();
                            attachment.Name = info.Name;
                            if (!string.IsNullOrEmpty(contents))
                            {
                                attachment.Size = BitConverter.ToInt64(info.Content, 0);
                            }
                            attachment.Content = info.Content;
                            attachment.DisplayName = "H";
                            string path = AppDomain.CurrentDomain.BaseDirectory;
                            path = Path.Combine(path, "filetemp");
                            path += "\\" + info.Name;
                            IOHelper.WriteToDisk(path, info.Content);
                            attachment.ClientPath = path;
                            attachmentContents.Add(attachment);
                        }
                    }
                    if (attachmentContents == null || attachmentContents.Count == 0)
                    {
                        return LocalData.IsEnglish
                                   ? "Please contact the customer service staff to upload email after SIMBL&SIHBL file."
                                   : "请联系客服人员上传SIMBL&SIHBL文件后发送邮件.";
                    }

                    Message.ServiceInterface.Message message = null;
                    //ICP.Message.ServiceInterface.Message oldmessage = null;
                    //oldmessage = MailSoCopyToAllCustomers(oceanBookingInfo, "SI");

                    //if (oldmessage == null)
                    //{
                    message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanBookingInfo.ID, Guid.Empty,
                                                body.ToString(), subject, strSpit[2], eventCodes.Code, eventObjects, attachmentContents);
                    //}
                    //else
                    //{
                    //    body.Append("<div class=" + "MsoNormal" + ">");
                    //    body.Append("---------Original message-------<br/>");
                    //    body.Append("Sender:" + oldmessage.SendFrom + "<br/>");
                    //    body.Append("Sent On:" + oldmessage.CreateDate + "<br/>");
                    //    body.Append("CC:" + oldmessage.CC + "<br/>");
                    //    body.Append("To:" + oldmessage.SendTo + "<br />");
                    //    body.Append("Subject:" + oldmessage.Subject + "<br />");
                    //    body.Append("</div>");
                    //    body.Append(oldmessage.Body);
                    //    //subject = subject;
                    //    message = CreateMessageInfo(MessageType.Email,
                    //                                MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                    //                                FormType.Booking, OperationType.OceanExport,
                    //                                oceanBookingInfo.ID, Guid.Empty,
                    //                                body.ToString(), subject, strSpit[2], "BLCfmAgt", eventObjects, attachmentContents);
                    //}

                    MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, string.Empty, null);
                    //OceanExportService.SetUpdateOceanTrackings(oceanBookingInfo.ID);


                }
                else
                {
                    return LocalData.IsEnglish ? "SI contact people as empty, unable to send mail, please add SI contacts." : "SI联系人为空，无法发送邮件，请添加SI联系人.";
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        public string MailCustomerAskForSi(bool isEnglish, Guid oceanBookingId)
        {
            return MailCustomerAskForSi(isEnglish, oceanBookingId, string.Empty);
        }
        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="mailInformation"></param>
        public string MailCustomerAskForSi(bool isEnglish, Guid oceanBookingId, string mailInformation)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
                return MailCustomerAskForSi(isEnglish, oceanBookingInfo, mailInformation);
            }
        }

        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        public string MailCustomerAskForSi(bool isEnglish, OceanBookingInfo oceanBookingInfo)
        {
            return MailCustomerAskForSi(isEnglish, oceanBookingInfo, string.Empty);
        }
        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="mailInformation"></param>
        public string MailCustomerAskForSi(bool isEnglish, OceanBookingInfo oceanBookingInfo, string mailInformation)
        {
            if (oceanBookingInfo == null)
                return string.Empty;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var promptCh = new StringBuilder();
                var promptEn = new StringBuilder();
                oceanBookingInfo.ClosingDateCheck = true;
                oceanBookingInfo.NoCheck = true;
                EventCode eventCodes = EventCodeList("SINC");
                if (string.IsNullOrEmpty(mailInformation))
                {
                    //读取当前用户的邮箱地址信息
                    mailInformation = MailInformation(oceanBookingInfo.ID, "SI", null, false, false);
                }

                string[] strSpit = null;
                if (!string.IsNullOrEmpty(mailInformation))
                {
                    strSpit = mailInformation.Split('|');
                }
                else
                {
                    strSpit = new string[] { " ", " ", " " };
                }

                string top = GetEmailSendValidationInfo(oceanBookingInfo);
                if (!string.IsNullOrEmpty(top))
                {
                    promptCh.Append(top);
                    promptEn.Append(top);
                }
                if (!string.IsNullOrEmpty(promptCh.ToString()) || !string.IsNullOrEmpty(promptEn.ToString()))
                {
                    return LocalData.IsEnglish ? promptEn.ToString() : promptCh.ToString();
                }

                if (strSpit != null && strSpit.Length > 1)
                {
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBookingInfo.ID,
                        OperationType = OperationType.OceanExport,
                        FormID = oceanBookingInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCodes.Code,
                        Description = eventCodes.Subject,
                        Subject = eventCodes.Subject,
                        Priority = MemoPriority.Normal,
                        UpdateDate = DateTime.Now,
                        Owner = LocalData.UserInfo.LoginName,
                        UpdateBy = LocalData.UserInfo.LoginID,
                        CategoryName = eventCodes.Category,
                        IsShowAgent = true,
                        IsShowCustomer = true,
                        Type = MemoType.EmailLog
                    };


                    Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email,
                                                                                               MessageWay.Send, strSpit[0],
                                                                                               LocalData.UserInfo.EmailAddress,
                                                                                               FormType.Booking, OperationType.OceanExport,
                                                                                               oceanBookingInfo.ID, Guid.Empty,
                                                                                               string.Empty, string.Empty, strSpit[2],
                                                                                               eventCodes.Code, eventObjects, null);
                    //传输邮件模版的字符数组
                    object[] values = { oceanBookingInfo, strSpit[1] };
                    MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, "ConfirAskCustomerForSI", values);
                }
                else
                {
                    return LocalData.IsEnglish ? "Supplementary food contact for air, unable to send mail, please add your contact partner." :
                                                 "补料联系人为空，无法发送邮件，请添加补料联系人.";
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId">与业务号相同</param>
        public string MailSalesManAskForProfitPromise(bool isEnglish, Guid oceanBookingId)
        {
            return MailSalesManAskForProfitPromise(isEnglish, oceanBookingId, string.Empty);
        }

        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId">与业务号相同</param>
        /// <param name="mailInformation">邮件地址</param>
        public string MailSalesManAskForProfitPromise(bool isEnglish, Guid oceanBookingId, string mailInformation)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
                return MailSalesManAskForProfitPromise(isEnglish, oceanBookingInfo, mailInformation);
            }
        }

        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        public string MailSalesManAskForProfitPromise(bool isEnglish, OceanBookingInfo oceanBookingInfo)
        {
            return MailSalesManAskForProfitPromise(isEnglish, oceanBookingInfo, string.Empty);
        }
        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="mailInformation">邮件地址</param>
        public string MailSalesManAskForProfitPromise(bool isEnglish, OceanBookingInfo oceanBookingInfo, string mailInformation)
        {
            string errormessage = string.Empty;
            if (oceanBookingInfo == null)
                return string.Empty;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                oceanBookingInfo.NoCheck = true;
                oceanBookingInfo.POLCheck = true;
                oceanBookingInfo.PODCheck = true;
                oceanBookingInfo.ContainerCheck = true;
                oceanBookingInfo.SalesNameCheck = true;
                string top = GetEmailSendValidationInfo(oceanBookingInfo);
                if (!string.IsNullOrEmpty(top))
                {
                    errormessage = top;
                }
                else
                {
                    //根据揽货人的ID查询EMAL
                    if (oceanBookingInfo.SalesID != null)
                    {
                        var userInfo = UserService.GetUserInfo((Guid)oceanBookingInfo.SalesID);
                        if (userInfo != null)
                        {
                            EventCode eventCodes = EventCodeList("SOPA");
                            var eventObjects = new EventObjects
                            {
                                OperationID = oceanBookingInfo.ID,
                                OperationType = OperationType.OceanExport,
                                FormID = oceanBookingInfo.ID,
                                FormType = FormType.Unknown,
                                Code = eventCodes.Code,
                                Description = eventCodes.Subject,
                                Subject = eventCodes.Subject,
                                Priority = MemoPriority.Normal,
                                UpdateDate = DateTime.Now,
                                Owner = LocalData.UserInfo.LoginName,
                                UpdateBy = LocalData.UserInfo.LoginID,
                                CategoryName = eventCodes.Category,
                                IsShowAgent = true,
                                IsShowCustomer = true,
                                Type = MemoType.EmailLog
                            };
                            if (string.IsNullOrEmpty(mailInformation))
                            {

                            }
                            // 邮件发送的消息实体
                            var message = CreateMessageInfo(MessageType.Email,
                                                                           MessageWay.Send, userInfo.EMail, LocalData.UserInfo.EmailAddress,
                                                                           FormType.Booking, OperationType.OceanExport,
                                                                           oceanBookingInfo.ID, Guid.Empty,
                                                                           string.Empty, string.Empty, string.Empty, string.Empty, eventObjects, null);
                            //根据发送邮件的版本来读取对应的发送人
                            object[] values = { oceanBookingInfo };
                            SaveEventState(eventObjects, oceanBookingInfo.ID, 1);
                            //MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, "ConfirAskProfitPromise", values);
                            message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, "ConfirAskProfitPromise", values);
                            message.BodyFormat = BodyFormat.olFormatHTML;
                            message.State = MessageState.Success;
                            MessageService.Send(message);

                        }
                        else
                        {
                            errormessage = LocalData.IsEnglish ? "Freight people mail information is not complete, can't send mail." : "揽货人邮箱信息不完整，无法发送邮件.";
                        }
                    }
                }
            }
            return errormessage;
        }
        /// <summary>
        /// 发送邮件给客户通知订舱失败
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        public string MailCustomerForSoFailure(bool isEnglish, Guid oceanBookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
                return MailCustomerForSoFailure(isEnglish, oceanBookingInfo);
            }
        }
        /// <summary>
        /// 发送邮件给客户通知订舱失败
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        public string MailCustomerForSoFailure(bool isEnglish, OceanBookingInfo oceanBookingInfo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var operationId = oceanBookingInfo.ID;
                List<EventObjects> eventObjects = FCMCommonService.GetMemoList(operationId, null);
                if (oceanBookingInfo != null)
                {
                    var promptCh = new StringBuilder();
                    var promptEn = new StringBuilder();
                    oceanBookingInfo.POLCheck = true;
                    oceanBookingInfo.PODCheck = true;
                    oceanBookingInfo.ContainerCheck = true;
                    //读取当前用户的邮箱地址信息
                    string mailInformation = MailInformation(oceanBookingInfo.ID, "SO", null, false, false);
                    string[] strSpit = null;
                    if (!string.IsNullOrEmpty(mailInformation))
                    {
                        strSpit = mailInformation.Split('|');
                    }
                    else
                    {
                        strSpit = new string[] { " ", " ", " " };
                    }

                    string top = GetEmailSendValidationInfo(oceanBookingInfo);
                    if (!string.IsNullOrEmpty(top))
                    {
                        promptCh.Append(top);
                        promptEn.Append(top);
                    }
                    if (!string.IsNullOrEmpty(promptCh.ToString()) || !string.IsNullOrEmpty(promptEn.ToString()))
                    {

                        return LocalData.IsEnglish ? promptEn.ToString() : promptCh.ToString();
                    }


                    if (strSpit != null && strSpit.Length > 1)
                    {
                        EventCode eventCodes = EventCodeList("SOFC");
                        var eventObject = new EventObjects
                        {
                            OperationID = oceanBookingInfo.ID,
                            OperationType = OperationType.OceanExport,
                            FormID = oceanBookingInfo.ID,
                            FormType = FormType.Unknown,
                            Code = eventCodes.Code,
                            Description = eventCodes.Subject,
                            Subject = eventCodes.Subject,
                            Priority = MemoPriority.Normal,
                            UpdateDate = DateTime.Now,
                            Owner = LocalData.UserInfo.LoginName,
                            UpdateBy = LocalData.UserInfo.LoginID,
                            CategoryName = eventCodes.Category,
                            IsShowAgent = true,
                            IsShowCustomer = true,
                            Type = MemoType.EmailLog
                        };
                        if (oceanBookingInfo.SalesID != null)
                        {
                            var userInfo = UserService.GetUserInfo((Guid)oceanBookingInfo.SalesID);
                            var message = CreateMessageInfo(MessageType.Email,
                                                            MessageWay.Send, strSpit[0],
                                                            LocalData.UserInfo.EmailAddress,
                                                            FormType.Booking, OperationType.OceanExport,
                                                            oceanBookingInfo.ID, Guid.Empty,
                                                            string.Empty, string.Empty,
                                                            userInfo.EMail, eventCodes.Code, eventObject, null);

                            List<object> values = new List<object>();
                            values.AddRange(new object[] { oceanBookingInfo, strSpit[1] });

                            if (eventObjects.Any() && eventObjects != null)
                            {
                                foreach (var events in eventObjects)
                                {
                                    values.Add(events);
                                }
                            }
                            MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, "ConfirBookingFailure",
                                                                           values.ToArray());

                        }
                    }
                    else
                    {
                        return LocalData.IsEnglish
                                   ? "Booking contact people as empty, unable to send mail, please add booking contacts."
                                   : "订舱联系人为空，无法发送邮件，请添加订舱联系人.";
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        public string MailSalesForConfirmDebitFees(bool isEnglish, Guid oceanBookingId)
        {
            return MailSalesForConfirmDebitFees(isEnglish, oceanBookingId, string.Empty);
        }
        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="mailInformation"></param>
        public string MailSalesForConfirmDebitFees(bool isEnglish, Guid oceanBookingId, string mailInformation)
        {
            OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            return MailSalesForConfirmDebitFees(isEnglish, oceanBookingInfo);
        }

        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        public string MailSalesForConfirmDebitFees(bool isEnglish, OceanBookingInfo oceanBookingInfo)
        {
            return MailSalesForConfirmDebitFees(isEnglish, oceanBookingInfo, string.Empty);
        }
        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="mailInformation"></param>
        public string MailSalesForConfirmDebitFees(bool isEnglish, OceanBookingInfo oceanBookingInfo, string mailInformation)
        {
            string errormessage = string.Empty;
            oceanBookingInfo.NoCheck = true;
            oceanBookingInfo.CustomerCheck = true;
            oceanBookingInfo.POLCheck = true;
            oceanBookingInfo.PODCheck = true;
            oceanBookingInfo.SalesNameCheck = true;
            oceanBookingInfo.ContainerCheck = true;

            string top = GetEmailSendValidationInfo(oceanBookingInfo);
            if (!string.IsNullOrEmpty(top))
            {
                errormessage = top;
            }
            else
            {
                //根据揽货人的ID查询EMAL
                UserInfo userInfo = UserService.GetUserInfo((Guid)oceanBookingInfo.SalesID);
                if (userInfo != null)
                {
                    EventCode eventCodes = EventCodeList("SOCD");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBookingInfo.ID,
                        OperationType = OperationType.OceanExport,
                        FormID = oceanBookingInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCodes.Code,
                        Description = eventCodes.Subject,
                        Subject = eventCodes.Subject,
                        Priority = MemoPriority.Normal,
                        UpdateDate = DateTime.Now,
                        Owner = LocalData.UserInfo.LoginName,
                        UpdateBy = LocalData.UserInfo.LoginID,
                        CategoryName = eventCodes.Category,
                        IsShowAgent = true,
                        IsShowCustomer = true,
                        Type = MemoType.EmailLog
                    };
                    // 邮件发送的消息实体
                    Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email,
                                                                                     MessageWay.Send, userInfo.EMail, LocalData.UserInfo.EmailAddress,
                                                                                     FormType.Booking, OperationType.OceanExport,
                                                                                     oceanBookingInfo.ID, Guid.Empty, string.Empty,
                                                                                     string.Empty, string.Empty, string.Empty, eventObjects, null);
                    //MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, "ConfirDebitFees", new object[] { oceanBookingInfo });
                    SaveEventState(eventObjects, oceanBookingInfo.ID, 1);
                    message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, "ConfirDebitFees", new object[] { oceanBookingInfo });
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MessageService.Send(message);


                }
                else
                {
                    errormessage = LocalData.IsEnglish == false ? "揽货人邮箱地址为空，无法发送邮件." : "Sales person email address is empty, can't send mail.";
                }
            }
            return errormessage;
        }

        /// <summary>
        /// 通知客户ADJ SO Copy
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <returns></returns>
        public string MailSoCopyToCustomer(bool isEnglish, Guid oceanBookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
                return MailSoCopyToCustomer(isEnglish, oceanBookingInfo, string.Empty);
            }
        }
        /// <summary>
        /// 通知客户ADJ SO Copy
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="mailInformation"></param>
        /// <returns></returns>
        public string MailSoCopyToCustomer(bool isEnglish, Guid oceanBookingId, string mailInformation)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
                return MailSoCopyToCustomer(isEnglish, oceanBookingInfo, mailInformation);
            }
        }

        /// <summary>
        /// 按最后一封SO邮件全部回复
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanbookingInfo"></param>
        /// <returns></returns>
        public Message.ServiceInterface.Message MailSoCopyToAllCustomers(OceanBookingInfo oceanbookingInfo, string Type)
        {
            Message.ServiceInterface.Message message = null;
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = oceanbookingInfo.ID;
            Guid messageID = Guid.Empty;
            CommunicationHistory[] communicationHistory = CommunicationHistoryService.GetCommunicationHistoryList(context).Where(c => c.ContactStage.Contains(Type)).OrderByDescending(nn => nn.CreateDate).ToArray();
            if (communicationHistory.Length > 0) //存在SO邮件
            {
                messageID = communicationHistory[0].Id;
                message = MessageService.GetMessageInfoById(messageID);

            }
            return message;
        }

        /// <summary>
        /// 通知客户ADJ SO Copy
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <returns></returns>
        public string MailSoCopyToCustomer(bool isEnglish, OceanBookingInfo oceanBookingInfo)
        {
            return MailSoCopyToCustomer(isEnglish, oceanBookingInfo, string.Empty);
        }
        /// <summary>
        /// 通知客户ADJ SO Copy
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="mailInformation"></param>
        /// <returns></returns>
        public string MailSoCopyToCustomer(bool isEnglish, OceanBookingInfo oceanBookingInfo, string mailInformation)
        {
            try
            {
                if (oceanBookingInfo == null)
                    return string.Empty;
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    var promptCh = new StringBuilder();
                    var promptEn = new StringBuilder();
                    oceanBookingInfo.NoCheck = true;
                    oceanBookingInfo.POLCheck = true;
                    oceanBookingInfo.PODCheck = true;
                    oceanBookingInfo.ContainerCheck = true;
                    string no = oceanBookingInfo.OceanShippingOrderNo;
                    string polName = oceanBookingInfo.POLName;
                    string podName = oceanBookingInfo.PODName;
                    string box = oceanBookingInfo.ContainerDescription == null ? string.Empty : oceanBookingInfo.ContainerDescription.ToString();
                    string userName;
                    userName = LocalData.UserInfo.LoginName.Trim();
                    if (string.IsNullOrEmpty(mailInformation))
                    {
                        //读取当前用户的邮箱地址信息
                        mailInformation = MailInformation(oceanBookingInfo.ID, "SO", oceanBookingInfo.AgentID, true, false);
                    }
                    string[] strSpit = null;
                    if (!string.IsNullOrEmpty(mailInformation))
                    {
                        strSpit = mailInformation.Split('|');
                    }
                    else
                    {
                        strSpit = new string[] { " ", " ", " " };
                    }
                    string top = GetEmailSendValidationInfo(oceanBookingInfo);
                    if (!string.IsNullOrEmpty(top))
                    {
                        promptCh.Append(top);
                        promptEn.Append(top);

                    }
                    if (!string.IsNullOrEmpty(promptCh.ToString()) || !string.IsNullOrEmpty(promptEn.ToString()))
                    {
                        return LocalData.IsEnglish ? promptEn.ToString() : promptCh.ToString();

                    }

                    if (strSpit != null && strSpit.Length > 0)
                    {
                        var subject = string.Empty;
                        StringBuilder body = new StringBuilder();
                        UserDetailInfo userDetail = ServiceClient.GetService<IUserService>().GetUserDetailInfo(LocalData.UserInfo.LoginID);
                        if (dt == null)
                        {
                            dt = ServiceClient.GetService<IOceanExportService>().GetEmailNotice(LocalData.UserInfo.DefaultDepartmentID);
                        }
                        string Notice = string.Empty;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Notice = dt.Rows[0][0].ToString();
                        }
                        string str = GetCompany();
                        if (isEnglish == false)
                        {
                            //主题
                            subject = " SO#" + no + " " + polName + " " + "to" + " " + podName + " " + "," + box;
                            //内容
                            body.Append("<html>");
                            body.Append("<head>");
                            body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                            body.Append(" <style type=" + "text/css" + ">");
                            body.Append(" .MsoNormal");
                            body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                            body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                            body.Append(" </style>");
                            body.Append("</head>");
                            body.Append("<body><div class=" + "MsoNormal" + ">");
                            body.Append("Hi" + "  " + strSpit[1] + "," + "<br /><br />");
                            body.Append("已成功订舱： 从 " + polName + " 到 " + podName + "，集装箱：" + box + "." + "<br />");
                            body.Append("附件是订舱单副本，请及时打印订舱单并提取集装箱." + "<br /><br />");
                            body.Append("B.rgds,<br /><br />");
                            body.Append(userName + "<br />");
                            body.Append("Cityocean Logistics Co.,LTD<br />");
                            body.Append(str + "<br />");
                            body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                            body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                            body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                            body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p>");
                            body.Append("<p>" + Notice + "</p>");
                            body.Append("</div>");
                            body.Append("</body>");
                            body.Append("</html>");
                        }
                        else
                        {
                            //主题
                            subject = " SO#" + no + " " + polName + " " + "to" + " " + podName + " " + "," + box;
                            //内容
                            body.Append("<html>");
                            body.Append("<head>");
                            body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                            body.Append(" <style type=" + "text/css" + ">");
                            body.Append(" .MsoNormal");
                            body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                            body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                            body.Append(" </style>");
                            body.Append("</head>");
                            body.Append("<body><div class=" + "MsoNormal" + ">");
                            body.Append("Hi" + "  " + strSpit[1] + "," + "<br /><br />");
                            body.Append("The booking, From " + polName + "  to " + podName + "," + box + "." + "<br />");
                            body.Append("Enclosed attachment(s) is the SO copy." + "<br /><br />");
                            body.Append("B.rgds,<br />");
                            body.Append(userName + "<br />");
                            body.Append("Cityocean Logistics Co.,LTD<br />");
                            body.Append(str + "<br />");
                            body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                            body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                            body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                            body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p>");
                            body.Append("<p>" + Notice + "</p><br/>");
                            body.Append("</div>");
                            body.Append("</body>");
                            body.Append("</html>");
                        }
                        EventCode eventCodes = EventCodeList("SOS");
                        var eventObjects = new EventObjects
                        {
                            OperationID = oceanBookingInfo.ID,
                            OperationType = OperationType.OceanExport,
                            FormID = oceanBookingInfo.ID,
                            FormType = FormType.Unknown,
                            Code = eventCodes.Code,
                            Description = eventCodes.Subject,
                            Subject = eventCodes.Subject,
                            Priority = MemoPriority.Normal,
                            UpdateDate = DateTime.Now,
                            Owner = LocalData.UserInfo.LoginName,
                            UpdateBy = LocalData.UserInfo.LoginID,
                            CategoryName = eventCodes.Category,
                            IsShowAgent = true,
                            IsShowCustomer = true,
                            Type = MemoType.EmailLog

                        };
                        // 邮件发送的消息实体

                        List<AttachmentContent> attachmentContents = new List<AttachmentContent>();

                        var uplodFile = ClientFileService.GetDocumentListByDocumentType(oceanBookingInfo.ID, DocumentType.ASO);
                        if (uplodFile.Any())
                        {
                            foreach (var info in uplodFile)
                            {
                                //Bit 转string 判断当前是否存在 
                                string contents = Encoding.Default.GetString(info.Content);
                                AttachmentContent attachment = new AttachmentContent();
                                attachment.Name = info.Name;
                                if (!string.IsNullOrEmpty(contents))
                                {
                                    attachment.Size = BitConverter.ToInt64(info.Content, 0);
                                }
                                attachment.Content = info.Content;
                                attachment.DisplayName = info.Name;
                                string path = AppDomain.CurrentDomain.BaseDirectory;
                                path = Path.Combine(path, "filetemp");
                                path += "\\" + info.Name;
                                IOHelper.WriteToDisk(path, info.Content);
                                attachment.ClientPath = path;
                                attachmentContents.Add(attachment);
                            }
                        }
                        else
                        {
                            return LocalData.IsEnglish
                                       ? "Please upload document ASO first before sending the mail."
                                       : "发邮件前，请先上传ASO文档.";
                        }
                        Message.ServiceInterface.Message message = null;
                        if (MailSoCopyToAllCustomers(oceanBookingInfo, "SO") == null)
                        {
                            message = CreateMessageInfo(MessageType.Email,
                                                        MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                                                        FormType.Booking, OperationType.OceanExport,
                                                        oceanBookingInfo.ID, Guid.Empty,
                                                        body.ToString(), subject, strSpit[2], eventCodes.Code, eventObjects, attachmentContents);
                        }
                        else
                        {
                            Message.ServiceInterface.Message oldmessage = new Message.ServiceInterface.Message();
                            oldmessage = MailSoCopyToAllCustomers(oceanBookingInfo, "SO");

                            body.Append("<div class=" + "MsoNormal" + ">");
                            body.Append("---------Original message-------<br/>");
                            body.Append("Sender:" + oldmessage.SendFrom + "<br/>");
                            body.Append("Sent On:" + oldmessage.CreateDate + "<br/>");
                            body.Append("CC:" + oldmessage.CC + "<br/>");
                            body.Append("To:" + oldmessage.SendTo + "<br />");
                            body.Append("Subject:" + oldmessage.Subject + "<br />");
                            body.Append("</div>");
                            body.Append(oldmessage.Body);
                            subject = oldmessage.Subject == "" ? subject : (oldmessage.Subject + " && ") + subject;
                            message = CreateMessageInfo(MessageType.Email,
                                                        MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                                                        FormType.Booking, OperationType.OceanExport,
                                                        oceanBookingInfo.ID, Guid.Empty,
                                                        body.ToString(), subject, strSpit[2], eventCodes.Code, eventObjects, attachmentContents);
                        }

                        MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, string.Empty, null);
                        OceanExportService.SetUpdateOceanTrackings(oceanBookingInfo.ID);

                    }
                    else
                    {
                        return LocalData.IsEnglish ? "Booking contact people as empty, unable to send mail, please add booking contacts." : "订舱联系人为空，无法发送邮件，请添加订舱联系人.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
            }
            return string.Empty;
        }

        /// <summary>
        /// SOD成功以后发送邮件给客服
        /// </summary>
        /// <param name="oceanBookingId">业务id</param>
        /// <param name="memoType">备注类型</param>
        /// <param name="path">操作路径</param>
        /// <param name="eventFlg">是否生成事件</param>
        public void MainSOCustomerServiceSOD(Guid oceanBookingId, MemoType memoType, string path, bool eventFlg)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            if (oceanbookinginfo != null)
            {
                if (oceanbookinginfo.BookingerID != Guid.Empty)
                {
                    var BookingerID = UserService.GetUserInfo((Guid)oceanbookinginfo.BookingerID);
                    if (!string.IsNullOrEmpty(BookingerID.EMail))
                    {
                        EventObjects eventObjects = null;
                        EventCode eventCodes = EventCodeList("SOD");
                        if (eventFlg)
                        {
                            eventObjects = new EventObjects
                            {
                                OperationID = oceanbookinginfo.ID,
                                OperationType = OperationType.OceanExport,
                                Code = eventCodes.Code,
                                Id = Guid.Empty,
                                FormID = oceanbookinginfo.ID,
                                FormType = FormType.Unknown,
                                IsShowCSPEvent = eventCodes.IsShowCSPEvent,
                                IsShowCSPActivity = eventCodes.IsShowCSPActivity,
                                IsShowAgent = false,
                                IsShowCustomer = true,
                                Subject = string.IsNullOrEmpty(path) ? eventCodes.Subject : eventCodes.Subject + "(" + path + ")",
                                Description = string.IsNullOrEmpty(path) ? eventCodes.Subject : eventCodes.Subject + "(" + path + ")",
                                Priority = MemoPriority.Normal,
                                Type = memoType,
                                UpdateDate = DateTime.Now,
                                UpdateBy = LocalData.UserInfo.LoginID
                            };
                        }

                        // 邮件发送的消息实体
                        var message = CreateMessageInfo(MessageType.Email,
                                                        MessageWay.Send, BookingerID.EMail, LocalData.UserInfo.EmailAddress,
                                                        FormType.Booking, OperationType.OceanExport,
                                                        oceanbookinginfo.ID, Guid.Empty,
                                                        string.Empty, string.Empty, string.Empty, string.Empty, eventObjects, null);
                        //根据发送邮件的版本来读取对应的发送人
                        object[] values = { oceanbookinginfo, oceanbookinginfo.BookingerName };
                        message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, "MainSOCustomerServiceSOD", values);
                        message.BodyFormat = BodyFormat.olFormatHTML;
                        message.State = MessageState.Success;
                        MessageService.Send(message);
                        //#region  修改业务状态
                        //if (memoType == MemoType.EmailLog)
                        //{
                        //    var business = new List<BusinessSaveParameter>();
                        //    var dictionary = new Dictionary<string, object>
                        //    {
                        //        {"OceanBookingID", eventObjects.OperationID},
                        //        {eventObjects.Code,"1"},
                        //        {"OperationType",OperationType.OceanExport}
                        //    };
                        //    var businessSave = new BusinessSaveParameter { items = dictionary };
                        //    business.Add(businessSave);
                        //    BusinessQueryService.Save(business);
                        //}
                        //#endregion
                    }
                }
            }
        }
        /// <summary>
        /// 手动勾选SOD事件以后发送邮件给客服
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="eventObjects">事件实体</param>
        public void CheckMainSOCustomerServiceSOD(Guid oceanBookingId, EventObjects eventObjects)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            if (oceanbookinginfo != null)
            {
                if (oceanbookinginfo.BookingerID != Guid.Empty)
                {
                    var BookingerID = UserService.GetUserInfo((Guid)oceanbookinginfo.BookingerID);
                    if (!string.IsNullOrEmpty(BookingerID.EMail))
                    {
                        EventCode eventCodes = EventCodeList(eventObjects.Code);
                        var eventObject = new EventObjects
                         {
                             OperationID = oceanbookinginfo.ID,
                             OperationType = OperationType.OceanExport,
                             Code = eventCodes.Code,
                             Id = Guid.Empty,
                             FormID = oceanbookinginfo.ID,
                             FormType = FormType.Unknown,
                             Subject = eventObjects.Description,
                             Description = eventObjects.Description,
                             IsShowCSPEvent = eventCodes.IsShowCSPEvent,
                             IsShowCSPActivity = eventCodes.IsShowCSPActivity,
                             Priority = MemoPriority.Normal,
                             Type = MemoType.Manually,
                             UpdateDate = DateTime.Now,
                             UpdateBy = LocalData.UserInfo.LoginID,
                             IsShowAgent = eventObjects.IsShowAgent,
                             IsShowCustomer = eventObjects.IsShowCustomer,
                             ManualImportant = eventObjects.ManualImportant
                         };
                        // 邮件发送的消息实体
                        var message = CreateMessageInfo(MessageType.Email,
                                                        MessageWay.Send, BookingerID.EMail,
                                                        LocalData.UserInfo.EmailAddress,
                                                        FormType.Booking, OperationType.OceanExport,
                                                        oceanbookinginfo.ID, Guid.Empty,
                                                        string.Empty, string.Empty, string.Empty, string.Empty,
                                                        eventObject, null);
                        //根据发送邮件的版本来读取对应的发送人
                        object[] values = { oceanbookinginfo, oceanbookinginfo.BookingerName };
                        message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, "MainSOCustomerServiceSOD", values);
                        message.BodyFormat = BodyFormat.olFormatHTML;
                        message.State = MessageState.Success;
                        MessageService.Send(message);
                    }
                }
            }
        }


        /// <summary>
        /// 发送邮件给订舱员申请订舱
        /// </summary>
        /// <param name="oceanBookingInfo"></param>
        /// <returns></returns>
        public string MailBookingClerkForApplySO(OceanBookingInfo oceanBookingInfo)
        {
            string itemKey = string.Empty;
            oceanBookingInfo.POLCheck = true;
            oceanBookingInfo.PODCheck = true;
            oceanBookingInfo.ContainerCheck = true;
            oceanBookingInfo.SalesNameCheck = true;
            oceanBookingInfo.BookingByCheck = true;
            var top = GetEmailSendValidationInfo(oceanBookingInfo);
            EventCode eventCodes = EventCodeList("SOA");
            if (!string.IsNullOrEmpty(top))
            {
                return top;
            }
            else
            {
                itemKey = "ConfirApplySoS";
                //根据揽货人的ID查询EMAL
                if (oceanBookingInfo.SalesID != null && oceanBookingInfo.BookingByID != null)
                {
                    var salaEMail = UserService.GetUserInfo((Guid)oceanBookingInfo.SalesID);
                    var bookingByEMail = UserService.GetUserInfo((Guid)oceanBookingInfo.BookingByID);
                    string email;
                    if (salaEMail != null)
                    {
                        email = salaEMail.EMail;

                    }
                    else
                    {
                        return LocalData.IsEnglish ? "Freight people mail information is not complete, can't send mail." : "揽货人邮箱信息不完整，无法发送邮件.";

                    }
                    if (bookingByEMail != null)
                    {
                        if (salaEMail.EMail != bookingByEMail.EMail)
                        {
                            email = email + ";" + bookingByEMail.EMail;
                            itemKey = "ConfirApplySo";
                        }
                    }
                    else
                    {
                        return LocalData.IsEnglish ? "Booking agent email address information is not complete, can't send mail." : "订舱员邮箱信息不完整，无法发送邮件.";

                    }
                    if (!string.IsNullOrEmpty(email))
                    {
                        var eventObjects = new EventObjects
                        {
                            OperationID = oceanBookingInfo.ID,
                            OperationType = OperationType.OceanExport,
                            FormID = oceanBookingInfo.ID,
                            FormType = FormType.Unknown,
                            Code = eventCodes.Code,
                            Description = eventCodes.Subject,
                            Subject = eventCodes.Subject,
                            Priority = MemoPriority.Normal,
                            UpdateDate = DateTime.Now,
                            Owner = LocalData.UserInfo.LoginName,
                            UpdateBy = LocalData.UserInfo.LoginID,
                            CategoryName = eventCodes.Category,
                            IsShowAgent = true,
                            IsShowCustomer = true,
                            Type = MemoType.EmailLog
                        };
                        // 邮件发送的消息实体
                        var message = CreateMessageInfo(MessageType.Email,
                                                        MessageWay.Send, email, LocalData.UserInfo.EmailAddress,
                                                        FormType.Booking, OperationType.OceanExport,
                                                        oceanBookingInfo.ID, Guid.Empty,
                                                         string.Empty, string.Empty, string.Empty, string.Empty, eventObjects, null);
                        //根据发送邮件的版本来读取对应的发送人
                        object[] values = { oceanBookingInfo };
                        SaveEventState(eventObjects, oceanBookingInfo.ID, 1);
                        message = MainCenterEmailTemplateGetter.ReturnMessage(message, true, itemKey, values);
                        message.BodyFormat = BodyFormat.olFormatHTML;
                        message.State = MessageState.Success;
                        MessageService.Send(message);

                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 打开快速新增订舱EDI
        /// </summary>
        /// <param name="bookIngListQuery"></param>
        public void OpenAddBookingEdi(WorkItem workItem)
        {
            FastAddBooking voucherPart = workItem.Items.AddNew<FastAddBooking>();
            string title = LocalData.IsEnglish ? "FAST BOOKING EDI" : "快速订舱";
            PartLoader.ShowEditPart<FastAddBooking>(workItem, null, title, null);
        }

        /// <summary>
        /// 发送邮件给订舱员申请订舱
        /// </summary>
        /// <param name="oceanBookingId"></param>
        /// <returns></returns>
        public string MailBookingClerkForApplySO(Guid oceanBookingId)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            return MailBookingClerkForApplySO(oceanbookinginfo);

        }

        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        public void MailSoConfirmationToCustomer(Guid oceanBookingId, bool isEnglish)
        {
            MailSoConfirmationToCustomer(oceanBookingId, string.Empty, isEnglish);
        }
        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="isEnglish">发送邮件版本</param>
        public void MailSoConfirmationToCustomer(Guid oceanBookingId, string mailInformation, bool isEnglish)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            //读取当前用户的邮箱地址信息
            if (string.IsNullOrEmpty(mailInformation))
            {
                mailInformation = MailInformation(oceanbookinginfo.ID, "SO", oceanbookinginfo.AgentID, false, false);
            }
            string[] strSpit = null;
            EventCode eventCodes = EventCodeList("SOS");
            if (!string.IsNullOrEmpty(mailInformation))
            {
                strSpit = mailInformation.Split('|');
            }
            else
            {
                strSpit = new string[] { " ", " ", " " };
            }

            var eventObjects = new EventObjects
            {
                OperationID = oceanbookinginfo.ID,
                OperationType = OperationType.OceanExport,
                FormID = oceanbookinginfo.ID,
                FormType = FormType.Unknown,
                Code = eventCodes.Code,
                Description = eventCodes.Subject,
                Subject = eventCodes.Subject,
                Priority = MemoPriority.Normal,
                UpdateDate = DateTime.Now,
                Owner = LocalData.UserInfo.LoginName,
                UpdateBy = LocalData.UserInfo.LoginID,
                CategoryName = eventCodes.Category,
                IsShowAgent = true,
                IsShowCustomer = true,
                Type = MemoType.EmailLog

            };
            // 邮件发送的消息实体
            var message = CreateMessageInfo(MessageType.Email,
                MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                FormType.Booking, OperationType.OceanExport,
                oceanbookinginfo.ID, Guid.Empty,
                string.Empty, string.Empty, string.Empty, eventCodes.Code, eventObjects,
                null);
            //根据发送邮件的版本来读取对应的发送人
            object[] values = { oceanbookinginfo, strSpit[1] };
            message = MainCenterEmailTemplateGetter.ReturnMessage(message, isEnglish, "MailSoConfirmationToCustomer",
                values);
            OceanExportPrintHelper.PrintOEBookingConfirmation(oceanBookingId, message);
        }
        /// <summary>
        /// 打开备注输入界面
        /// </summary>
        /// <param name="labRemark">备注框说明文本</param>
        /// <param name="isRemarkRequired">备注是否必须输入</param>
        /// <param name="title">窗体标题</param>
        /// <param name="editPartSaved"></param>
        public void ShowRemarkEditForm(string labRemark, bool isRemarkRequired, string title, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                EditRemarkPart editRemarkPart = RootWorkItem.Items.AddNew<EditRemarkPart>();
                editRemarkPart.LabRemark = labRemark;
                editRemarkPart.RemartRequired = isRemarkRequired;
                editRemarkPart.Saved += delegate(object[] prams)
                {
                    editPartSaved(prams);
                };
                PartLoader.ShowDialog(editRemarkPart, title);
            }
        }
        /// <summary>
        /// 选择运价合约
        /// </summary>
        /// <param name="oceanBookingId"></param>
        /// <param name="editPartSaved"></param>
        public void SelectContract(Guid oceanBookingId, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
                SelectContract(oceanBookingInfo, SelectType.Contract, editPartSaved);
            }

        }
        private void InnerSelectContract(Guid? placeOfReceiptID, Guid? polid, Guid? podid, Guid? deliveryid, Guid? placeOfDeliveryID, Guid? finalDestinationID, Guid? freightId, Guid? contractId, string contractNo, Guid? carrierId, DateTime? etd, PartDelegate.EditPartSaved editPartSaved, SelectType type)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(polid) || ArgumentHelper.GuidIsNullOrEmpty(podid) || ArgumentHelper.GuidIsNullOrEmpty(placeOfDeliveryID))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "pol、pod、delivery must  input" : "起运港、目的港、交货地必须填写"); return;
            }

            deliveryid = placeOfDeliveryID;


            using (new CursorHelper(Cursors.WaitCursor))
            {

                FreightDataList freightDataList =
                    OceanExportService.GetFreight(contractNo == "无" ? string.Empty : contractNo,
                    carrierId,
                    placeOfReceiptID,
                    polid,
                    deliveryid,
                    null,
                    string.Empty,
                    DateTime.Now,
                    DateTime.Now,
                    freightId, type);

                FreightInfo freight = RootWorkItem.Items.AddNew<FreightInfo>();

                FreightParameter freightParameter = new FreightParameter();
                freightParameter.prices = freightDataList.DataList;
                freightParameter.unitList = freightDataList.UnitList;
                freightParameter.scno = contractNo;
                freightParameter.shipownerid = carrierId;
                freightParameter.placeOfReceiptID = placeOfReceiptID;
                freightParameter.polid = polid.Value;
                freightParameter.podid = deliveryid;
                freightParameter.finalDestinationID = finalDestinationID;
                freightParameter.goodsdes = string.Empty;
                freightParameter.freightID = contractId;
                freightParameter.etd = etd;
                freightParameter.type = type;

                freight.SetDataSource(freightParameter);

                string title = "";

                if (LocalData.IsEnglish)
                {
                    title = type == SelectType.Contract ? "Select Price" : "Select Inquire Price";
                }
                else
                {
                    title = type == SelectType.Contract ? "选择运价" : "选择询价";
                }

                if (DialogResult.OK !=
                    PartLoader.ShowDialog(freight, title, FormBorderStyle.FixedSingle, FormWindowState.Maximized, true,
                        false)) return;
                FreightList selectedContract = freight.SelectedPrice;
                if (selectedContract == null) return;
                if (editPartSaved != null)
                {
                    editPartSaved(new object[] { selectedContract });
                }
            }
        }
        /// <summary>
        /// 选择运价合约
        /// </summary>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="editPartSaved"></param>
        public void SelectContract(OceanBookingInfo oceanBookingInfo, SelectType type, PartDelegate.EditPartSaved editPartSaved)
        {
            Guid? placeOfReceiptID = Guid.Empty;
            Guid polid = oceanBookingInfo.POLID;
            Guid podid = oceanBookingInfo.PODID;
            Guid? deliveryid = oceanBookingInfo.PlaceOfDeliveryID;
            Guid? finalDestinationID = oceanBookingInfo.FinalDestinationID;
            Guid? freightId = oceanBookingInfo.ContractID;
            Guid? placeOfDeliveryID = oceanBookingInfo.PlaceOfDeliveryID;
            Guid? contractId = oceanBookingInfo.ContractID;
            string contractNo = oceanBookingInfo.ContractNo;
            Guid? carrierId = oceanBookingInfo.CarrierID;
            DateTime? etd;
            if (!ArgumentHelper.GuidIsNullOrEmpty(oceanBookingInfo.ShippingLineID) && NAShippingLines.Contains((Guid)oceanBookingInfo.ShippingLineID))
            {
                etd = oceanBookingInfo.GateInDate;
            }
            else
            {
                etd = oceanBookingInfo.ETD;
            }

            if (!ArgumentHelper.GuidIsNullOrEmpty(oceanBookingInfo.PlaceOfReceiptID))
            {
                placeOfReceiptID = oceanBookingInfo.PlaceOfReceiptID.Value;
            }
            InnerSelectContract(placeOfReceiptID, polid, podid, deliveryid, placeOfDeliveryID, finalDestinationID, freightId, contractId, contractNo, carrierId, etd, editPartSaved, type);

        }

        /// <summary>
        /// 选择运价合约
        /// </summary>
        /// <param name="FreightRateID"></param>
        public void SelectContract(Guid FreightRateID)
        {
            FreightInfo freight = RootWorkItem.Items.AddNew<FreightInfo>();

            FreightDataList freightDataList = OceanExportService.GetFrightByID(FreightRateID);
            FreightParameter freightParameter = new FreightParameter
            {
                prices = freightDataList.DataList,
                unitList = freightDataList.UnitList,
                freightID = FreightRateID,
                type = SelectType.Contract
            };
            freight.SetDataSource(freightParameter);
            freight.setOI(true);
            string title = LocalData.IsEnglish ? "Show Price" : "选择运价";
            PartLoader.ShowDialog(freight, title, FormBorderStyle.FixedSingle, FormWindowState.Maximized, true, false);
        }


        /// <summary>
        /// 选择海运运价合约
        /// </summary>
        /// <param name="mblInfo"></param>
        /// <param name="editPartSaved"></param>
        public void SelectContract(OceanMBLInfo mblInfo, PartDelegate.EditPartSaved editPartSaved)
        {
            Guid? placeOfReceiptID = Guid.Empty;
            Guid polid = mblInfo.POLID;
            Guid podid = mblInfo.PODID;
            Guid? deliveryid = mblInfo.PlaceOfDeliveryID;
            Guid? finalDestinationID = mblInfo.FinalDestinationID;
            Guid? freightId = mblInfo.ContractID;
            Guid? placeOfDeliveryID = mblInfo.PlaceOfDeliveryID;
            Guid? contractId = mblInfo.ContractID;
            string contractNo = mblInfo.ContractNo;
            Guid? carrierId = mblInfo.CarrierID;
            DateTime? etd;
            if (mblInfo.ShippingLineID != Guid.Empty && NAShippingLines.Contains((Guid)mblInfo.ShippingLineID))
            {
                etd = mblInfo.GateInDate;
            }
            else
            {
                etd = mblInfo.ETD;
            }

            if (!ArgumentHelper.GuidIsNullOrEmpty(mblInfo.PlaceOfReceiptID))
            {
                placeOfReceiptID = mblInfo.PlaceOfReceiptID.Value;
            }
            InnerSelectContract(placeOfReceiptID, polid, podid, deliveryid, placeOfDeliveryID, finalDestinationID, freightId, contractId, contractNo, carrierId, etd, editPartSaved, SelectType.Contract);



        }

        /// <summary>
        /// 港后发起修订后，港前需先签收再修改合约
        /// </summary>
        /// <param name="operationID"></param>
        public bool IsNeedAccept(Guid operationID)
        {
            DocumentState documentState = OperationAgentService.GetDispatchAndReviseState(operationID, OperationType.OceanExport);

            if (documentState == DocumentState.Reviseing)
            {
                DialogResult diaResult = XtraMessageBox.Show(LocalData.IsEnglish ?
                     "D/C Fees have been revised by the agent, you must accept the revised fees first. \n\t Clicks Yes to enter the page [Accept Revised D/C Fees From The Agent] \n\t Clicks No to continue to enter the page [Account Info]."
                     : "代理已经修订了代理账单费用，您必须先签收此次修订。\n\t 单击[Yes]进入签收修订页面。\n\t 单击[NO]继续打开账单页面。"
                     , "", MessageBoxButtons.YesNo);

                if (diaResult == DialogResult.Yes)
                {
                    FCM.Common.UI.FCMUIUtility.ShowReviseAccepte(Workitem, operationID);
                }
                return false;
            }
            else return true;

        }

        /// <summary>
        /// 显示代理选择界面
        /// </summary>
        /// <param name="consigneeName">收货人名称</param>
        public void ShowAgentFilerList(string consigneeName)
        {
            AgentFilerListPart agentFilerList = RootWorkItem.Items.AddNew<AgentFilerListPart>();
            agentFilerList.DataSource = consigneeName;
            string title = LocalData.IsEnglish ? "Agent Filer List" : "港后客服联系人列表";
            PartLoader.ShowDialog(agentFilerList, title);
        }
        /// <summary>
        /// 分单
        /// </summary>
        /// <param name="splitBLList">需要分单的提单</param>
        ///<param name="editPartSaved"></param>
        public void SplitBillOfLoading(OceanBLList splitBLList, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            if (splitBLList == null) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Split BL" : "分单";
                PartLoader.ShowEditPart<OESplitBLPart>(RootWorkItem, splitBLList, values, title, editPartSaved, title);
            }
        }
        /// <summary>
        /// 分单
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="editPartSaved"></param>
        public void SplitBillOfLoading(string operationNo, string blNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            MergeDictionary(stateValues, values);
            stateValues["OperationNo"] = operationNo;
            stateValues["BLNo"] = blNo;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Split BL" : "分单";
                PartLoader.ShowEditPart<OESplitBLPart>(RootWorkItem, null, values, title, editPartSaved, title);
            }
        }
        /// <summary>
        /// 合单
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="editPartSaved"></param>
        public void MergeBillOfLoading(List<OceanBLList> selectedList, PartDelegate.EditPartSaved editPartSaved)
        {
            if (selectedList == null) return;


            if (selectedList.Count <= 1)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择两个以上的提单.", LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBLList firstBL = selectedList[0];
                FCMBLType bltype = firstBL.BLType;
                Guid oceanBookingId = firstBL.OceanBookingID;

                Guid mblID = Guid.Empty;
                if (bltype == FCMBLType.HBL) mblID = firstBL.MBLID;

                foreach (var item in selectedList)
                {
                    //IF 选择的提单类型不同
                    if (item.BLType != bltype)
                    {
                        XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择两票以上的MBL或HBL"
                                      , LocalData.IsEnglish ? "Tip" : "提示"
                                      , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                    //IF 选择的提单业务不同
                    if (item.OceanBookingID != oceanBookingId)
                    {
                        XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择相同业务下的提单"
                                      , LocalData.IsEnglish ? "Tip" : "提示"
                                      , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                    if (mblID != Guid.Empty)
                    {
                        //IF 选择的HBL挂的MBL不同
                        if (item.MBLID != mblID)
                        {
                            XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择相同MBL下的HBL"
                                          , LocalData.IsEnglish ? "Tip" : "提示"
                                          , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    //IF 选择的提单.状态=对单完成 或 完成提示：“提单[XXXXXXXXX]已经对单完成，所以不能进行合并。
                    if (item.State == OEBLState.Checked)
                    {
                        XtraMessageBox.Show((LocalData.IsEnglish ? "BL:" : "提单:") + item.No + (LocalData.IsEnglish ? " has been checked,can't merge" : " 已对单完成.不能合并.")
                                        , LocalData.IsEnglish ? "Tip" : "提示"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                    //IF 选择的提单.状态=对单放单：“提单[XXXXXXXXX]已经放单，所以不能进行合并。
                    if (item.State == OEBLState.Release)
                    {
                        XtraMessageBox.Show((LocalData.IsEnglish ? "BL:" : "提单:") + item.No + (LocalData.IsEnglish ? " has been Released,can't merge" : " 已放单.不能合并.")
                                        , LocalData.IsEnglish ? "Tip" : "提示"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                }
                OEMergeBLPart part = RootWorkItem.Items.AddNew<OEMergeBLPart>();
                part.Saved += delegate(object[] parameters)
                {
                    if (editPartSaved != null)
                    {
                        editPartSaved(parameters);
                    }
                };
                part.DataSource = selectedList;
                PartLoader.ShowDialog(part, LocalData.IsEnglish ? "Merge BL" : "合并提单");
            }
        }


        /// <summary>
        /// 弹出海出修订签收比较界面
        /// </summary>
        /// <param name="oeOperationId">订单ID</param>
        public void ShowReviseAccepte(Guid oeOperationId)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            OEBillRevisePart mainSpce = RootWorkItem.SmartParts.AddNew<OEBillRevisePart>();
            mainSpce.NewOperationID = oeOperationId;

            IWorkspace mainWorkspace = RootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            SmartPartInfo smartPartInfo = new SmartPartInfo();
            smartPartInfo.Title = LocalData.IsEnglish ? "OE Revise Accept" : "海出修订签收";
            mainWorkspace.Show(mainSpce, smartPartInfo);
            LoadingServce.CloseLoadingForm(theradID);
            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW", "打开修订签收");
        }

        /// <summary>
        /// 审核当前业务存在利润
        /// </summary>
        /// <param name="OceanBookingID"></param>
        public void SetUpdateOceanTrackings(Guid OceanBookingID)
        {
            if (OceanExportService.SetUpdateOceanTrackings(OceanBookingID))
            {
                EventCode eventCodes = EventCodeList("SOPV");
                var eventObjects = new EventObjects
                {
                    OperationID = OceanBookingID,
                    OperationType = OperationType.OceanExport,
                    Code = eventCodes.Code,
                    Id = Guid.Empty,
                    FormID = OceanBookingID,
                    FormType = FormType.Unknown,
                    IsShowAgent = false,
                    IsShowCustomer = true,
                    Subject = eventCodes.Subject,
                    Description = eventCodes.Subject,
                    Priority = MemoPriority.Normal,
                    Type = MemoType.Memo,
                    UpdateDate = DateTime.Now,
                    UpdateBy = LocalData.UserInfo.LoginID
                };
                ServiceClient.GetService<IFCMCommonService>().SaveMemoInfo(eventObjects);
            }

        }

        /// <summary>
        /// 复制MBL单
        /// </summary>
        /// <param name="listData">提单列表</param>
        /// <param name="editPartSaved"></param>
        public void InnerCopyMBLData(OceanBLList listData, PartDelegate.EditPartSaved editPartSaved)
        {
            OceanMBLInfo CloneData = OEService.GetOceanMBLInfo(listData.ID);

            #region 需清空的数据
            CloneData.ID = Guid.Empty;
            CloneData.State = OEBLState.Draft;
            CloneData.No = string.Empty;
            CloneData.MBLID = Guid.Empty;
            CloneData.HBLNos = string.Empty;
            CloneData.ContainerDescription = string.Empty;
            CloneData.CtnQtyInfo = string.Empty;
            CloneData.Measurement = CloneData.Weight = CloneData.Quantity = 0;
            CloneData.State = OEBLState.Draft;
            #endregion

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
            CloneData.PaymentTermID = normalDictionary.ID;
            CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            CloneData.QuantityUnitID = normalDictionary.ID;
            CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            CloneData.WeightUnitID = normalDictionary.ID;
            CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            CloneData.MeasurementUnitID = normalDictionary.ID;
            CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            #region 列表的数据

            CloneData.SONO = listData.SONO;
            CloneData.AgentOfCarrierName = listData.AgentOfCarrierName;
            CloneData.CarrierName = listData.CarrierName;
            CloneData.SalesName = listData.SalesName;
            CloneData.FilerName = listData.FilerName;
            CloneData.BookingerName = listData.BookingerName;
            CloneData.OverseasFilerName = listData.OverseasFilerName;
            #endregion

            string title = LocalData.IsEnglish ? "Copy MBL" : "复制MBL";
            PartLoader.ShowEditPart<MBLEditPart>(Workitem, CloneData, title, editPartSaved);
        }
        /// <summary>
        /// 复制HBL单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="editPartSaved"></param>
        public void InnerCopyHBLData(OceanBLList listData, PartDelegate.EditPartSaved editPartSaved)
        {
            OceanHBLInfo CloneData = OEService.GetOceanHBLInfo(listData.ID);
            CloneData.CarrierName = listData.CarrierName;
            #region 需清空的数据
            CloneData.ID = Guid.Empty;
            CloneData.AMSNo = string.Empty;
            CloneData.ISFNo = string.Empty;
            CloneData.State = OEBLState.Draft;
            CloneData.No = string.Empty;
            CloneData.ContainerDescription = string.Empty;
            CloneData.CtnQtyInfo = string.Empty;
            CloneData.Measurement = CloneData.Weight = CloneData.Quantity = 0;
            CloneData.ReleaseDate = null;//放单时间不能复制 2013-8-28 Liliang
            #endregion

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
            CloneData.PaymentTermID = normalDictionary.ID;
            CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            CloneData.QuantityUnitID = normalDictionary.ID;
            CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            CloneData.WeightUnitID = normalDictionary.ID;
            CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            CloneData.MeasurementUnitID = normalDictionary.ID;
            CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            #region 列表的数据
            CloneData.SONO = listData.SONO;
            CloneData.AgentOfCarrierName = listData.AgentOfCarrierName;
            CloneData.CarrierName = listData.CarrierName;
            CloneData.SalesName = listData.SalesName;
            CloneData.FilerName = listData.FilerName;
            CloneData.BookingerName = listData.BookingerName;
            CloneData.OverseasFilerName = listData.OverseasFilerName;
            #endregion

            string title = LocalData.IsEnglish ? "Copy HBL" : "复制HBL";
            PartLoader.ShowEditPart<HBLEditPart>(Workitem, CloneData, title, editPartSaved);
        }
        /// <summary>
        /// 复制DeclareHBL单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="editPartSaved"></param>
        public void InnerCopyDeclareHBLData(OceanBLList listData, PartDelegate.EditPartSaved editPartSaved)
        {
            DeclareHBLInfo CloneData = OEService.GetDeclareHBLInfo(listData.ID);
            CloneData.CarrierName = listData.CarrierName;
            #region 需清空的数据
            CloneData.ID = Guid.Empty;
            CloneData.AMSNo = string.Empty;
            CloneData.ISFNo = string.Empty;
            CloneData.State = OEBLState.Draft;
            CloneData.No = string.Empty;
            CloneData.ContainerDescription = string.Empty;
            CloneData.CtnQtyInfo = string.Empty;
            CloneData.Measurement = CloneData.Weight = CloneData.Quantity = 0;
            CloneData.ReleaseDate = null;
            #endregion

            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
            CloneData.PaymentTermID = normalDictionary.ID;
            CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            CloneData.QuantityUnitID = normalDictionary.ID;
            CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            CloneData.WeightUnitID = normalDictionary.ID;
            CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            CloneData.MeasurementUnitID = normalDictionary.ID;
            CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            #region 列表的数据
            CloneData.SONO = listData.SONO;
            CloneData.AgentOfCarrierName = listData.AgentOfCarrierName;
            CloneData.CarrierName = listData.CarrierName;
            CloneData.SalesName = listData.SalesName;
            CloneData.FilerName = listData.FilerName;
            CloneData.BookingerName = listData.BookingerName;
            CloneData.OverseasFilerName = listData.OverseasFilerName;
            #endregion

            string title = LocalData.IsEnglish ? "Copy DeclareHBL" : "复制DeclareHBL";
            PartLoader.ShowEditPart<DeclareHBLEditPart>(Workitem, CloneData, title, editPartSaved);
        }
        /// <summary>
        /// 删除提单
        /// </summary>
        /// <param name="listData">提单列表</param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <returns></returns>
        public bool InnerDelete(OceanBLList listData, object DataSource, object bsList, object businessOperationContext, PartDelegate.EditPartSaved editPartSaved)
        {
            if (listData == null || (listData.State != OEBLState.Draft && listData.State != OEBLState.Checking))
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Status  can be deleted when as a draft or a Checking" : "状态为草稿或对单中才允许删除."
                               , LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            string No = listData.No;

            if (listData.ExistFees)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "UN Done" : "若要执行此操作,请先删除提单下的费用."
                               , LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            if (XtraMessageBox.Show((LocalData.IsEnglish ? "Sure Delete BL " : "你真的要删除提单") + No + "?"
                               , LocalData.IsEnglish ? "Tip" : "提示"
                               , MessageBoxButtons.YesNo
                               , MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }


            if (listData.BLType == FCMBLType.MBL)
            {
                OEService.RemoveOceanMBLInfo(listData.ID, LocalData.UserInfo.LoginID, listData.UpdateDate);
                BindingSource bindingSource = bsList as BindingSource;
                bindingSource.RemoveCurrent();
            }
            else
            {
                OEService.RemoveOceanHBLInfo(listData.ID, LocalData.UserInfo.LoginID, listData.UpdateDate);

                List<OceanBLList> source = DataSource as List<OceanBLList>;
                OceanBLList mbl = source.Find(delegate(OceanBLList item) { return item.ID == listData.MBLID; });
                if (mbl != null)
                {
                    OceanBLList existhbl = source.Find(delegate(OceanBLList item) { return item.BLType == FCMBLType.HBL && item.MBLID == mbl.ID && item.ID != listData.ID; });
                    if (existhbl == null) mbl.HBLCount = 0;
                    source.Remove(listData);
                }
            }
            BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
            businessOperationParameter.Context = businessOperationContext as BusinessOperationContext;
            editPartSaved(new object[] { listData, businessOperationParameter });
            return true;
        }

        /// <summary>
        /// 删除提单
        /// </summary>
        /// <param name="listData">提单列表</param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <returns></returns>
        public bool InnerDeclareDelete(OceanBLList listData, object DataSource, object bsList, object businessOperationContext, PartDelegate.EditPartSaved editPartSaved)
        {
            if (listData == null || (listData.State != OEBLState.Draft && listData.State != OEBLState.Checking))
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Status  can be deleted when as a draft or a Checking" : "状态为草稿或对单中才允许删除."
                               , LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            string No = listData.No;

            if (listData.ExistFees)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "UN Done" : "若要执行此操作,请先删除提单下的费用."
                               , LocalData.IsEnglish ? "Tip" : "提示");
                return false;
            }

            if (XtraMessageBox.Show((LocalData.IsEnglish ? "Sure Delete BL " : "你真的要删除提单") + No + "?"
                               , LocalData.IsEnglish ? "Tip" : "提示"
                               , MessageBoxButtons.YesNo
                               , MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }


            if (listData.BLType == FCMBLType.MBL)
            {
                OEService.RemoveOceanMBLInfo(listData.ID, LocalData.UserInfo.LoginID, listData.UpdateDate);
                BindingSource bindingSource = bsList as BindingSource;
                bindingSource.RemoveCurrent();
            }
            else
            {
                OEService.RemoveDeclareHBLInfo(listData.ID, LocalData.UserInfo.LoginID, listData.UpdateDate);

                List<OceanBLList> source = DataSource as List<OceanBLList>;
                OceanBLList mbl = source.Find(delegate(OceanBLList item) { return item.ID == listData.MBLID; });
                if (mbl != null)
                {
                    OceanBLList existhbl = source.Find(delegate(OceanBLList item) { return item.BLType == FCMBLType.HBL && item.MBLID == mbl.ID && item.ID != listData.ID; });
                    if (existhbl == null) mbl.HBLCount = 0;
                    source.Remove(listData);
                }
            }
            BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
            businessOperationParameter.Context = businessOperationContext as BusinessOperationContext;
            editPartSaved(new object[] { listData, businessOperationParameter });
            return true;
        }

        /// <summary>
        /// 显示EDI数据
        /// </summary>
        /// <param name="ediType">类型</param>
        /// <param name="IDS">ID集合</param>
        /// <returns></returns>
        public bool InnerGetEDIDataSourceForNBEDIInfos(int ediType, Guid[] IDS)
        {
            EDIShow show = RootWorkItem.Items.AddNew<EDIShow>();
            string title = LocalData.IsEnglish ? "EDI INFOS SHOW" : "EDI数据展示";
            List<EDIShowValue> values = OceanExportService.GetEDIDataSourceForNBEDIInfos(ediType, IDS);
            show.DataSourse = values;
            DialogResult result = PartLoader.ShowDialog(show, title);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// MBL补料
        /// </summary>
        /// <param name="ediClientService"></param>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="amsEntryType"></param>
        /// <param name="aciEntryType"></param>
        /// <param name="isSucc"></param>
        public void InnerEMBL(List<OceanBLList> selectedList, Guid companyID, AMSEntryType amsEntryType, ACIEntryType aciEntryType, ref bool isSucc, object businessOperationContext, PartDelegate.EditPartSaved editPartSaved)
        {
            OECommonUtility.InnerEMBL(EdiClientService, selectedList, companyID, amsEntryType, aciEntryType, ref isSucc);
            if (isSucc)
            {
                BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
                businessOperationParameter.Context = businessOperationContext as BusinessOperationContext;
                editPartSaved(new object[] { selectedList, businessOperationParameter });
            }
            #region Comment Code
            //List<OceanBLList> mbls = selectedList;
            //if (mbls == null || mbls.Count == 0)
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
            //    return;
            //}

            //int i = mbls.FindAll(m => m.BLType == FCMBLType.HBL).Count;
            //if (i > 0)
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select MBL BL." : "请选择MBL提单进行发送.");
            //    return;
            //}

            ////韩进
            //OceanBLList hjMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
            ////中海
            //OceanBLList zhMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
            ////泛洋
            //OceanBLList fyMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
            ////中远
            //OceanBLList zyMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("ABBEBCEA-11AF-41C0-AEB0-61F1C9AD0E4F"); });


            //if (hjMBL == null && zhMBL == null && fyMBL == null && zyMBL == null)
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Shipowners are now only supports China Shipping、COSCO、Hanjin、Hainan Pan Ocean Shipping Electronic batch." : "现在只支持船东是 [韩进]、[中海]、[中远]、[海南泛洋] 的电子补料。");
            //    return;
            //}

            //string subjuect = string.Empty;
            //string toEmail = string.Empty;
            //StringBuilder mblNoBuilder = new StringBuilder();
            //List<string> operationNos = new List<string>();
            //List<Guid> mblIds = new List<Guid>();
            //List<Guid> oIds = new List<Guid>();
            //string key = string.Empty;
            //string tip = string.Empty;

            //#region 韩进
            //if (hjMBL != null)
            //{
            //    List<OceanBLList> hjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
            //    foreach (var item in hjMBLs)
            //    {
            //        if (mblNoBuilder.Length > 0)
            //            mblNoBuilder.Append(",");

            //        mblNoBuilder.Append(item.SONO);

            //        operationNos.Add(item.No);

            //        mblIds.Add(item.MBLID);
            //        oIds.Add(item.OceanBookingID);
            //    }
            //    subjuect = LocalData.IsEnglish ? "HANJIN SHIPPING(" + mblNoBuilder.ToString() + ")" : "韩进电子补料(" + mblNoBuilder.ToString() + ")";
            //    key = "HANJIN_SI";
            //    tip = LocalData.IsEnglish ? "HANJIN SHIPPING" : "韩进";
            //}
            //#endregion

            //#region 泛洋
            //else if (fyMBL != null)
            //{
            //    List<OceanBLList> fyMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
            //    foreach (var item in fyMBLs)
            //    {
            //        if (mblNoBuilder.Length > 0)
            //            mblNoBuilder.Append(",");

            //        mblNoBuilder.Append(item.SONO);

            //        operationNos.Add(item.No);
            //        mblIds.Add(item.MBLID);
            //        oIds.Add(item.OceanBookingID);
            //    }
            //    subjuect = LocalData.IsEnglish ? "HAINAN PAN SHIPPING(" + mblNoBuilder.ToString() + ")" : "海南泛洋电子补料(" + mblNoBuilder.ToString() + ")";
            //    key = "FYCW_SI";
            //    tip = LocalData.IsEnglish ? "HAINAN PAN SHIPPING" : "海南泛洋";
            //}
            //#endregion

            //#region 中远
            //else if (zyMBL != null)
            //{
            //    List<OceanBLList> zyMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("ABBEBCEA-11AF-41C0-AEB0-61F1C9AD0E4F"); });
            //    foreach (var item in zyMBLs)
            //    {
            //        if (mblNoBuilder.Length > 0)
            //            mblNoBuilder.Append(",");

            //        mblNoBuilder.Append(item.SONO);

            //        operationNos.Add(item.No);

            //        mblIds.Add(item.MBLID);
            //        oIds.Add(item.OceanBookingID);
            //    }
            //    subjuect = LocalData.IsEnglish ? "COSCO SHIPPING(" + mblNoBuilder.ToString() + ")" : "中远电子补料(" + mblNoBuilder.ToString() + ")";
            //    key = "COSCO_SI";
            //    tip = LocalData.IsEnglish ? "COSCO SHIPPING" : "中远";
            //}
            //#endregion

            //#region 中海
            //else if (zhMBL != null)
            //{
            //    //判断操作员的地理位置（华北/华南）
            //    List<LocalOrganizationInfo> olist = LocalData.UserInfo.UserOrganizationList;
            //    LocalOrganizationInfo curro = olist.Find(delegate(LocalOrganizationInfo o) { return o.ID == LocalData.UserInfo.DefaultCompanyID; });
            //    LocalOrganizationInfo currRegion = olist.Find(delegate(LocalOrganizationInfo o) { return o.ID == curro.ParentID; });
            //    if (currRegion.Code == "HDQ" || currRegion.Code == "HBQ")
            //    {
            //        key = "CSCL_SI_NorthChina";
            //    }
            //    else
            //    {
            //        key = "CSCL_SI";
            //    }
            //    List<OceanBLList> zjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
            //    foreach (var item in zjMBLs)
            //    {
            //        if (mblNoBuilder.Length > 0)
            //            mblNoBuilder.Append(",");

            //        mblNoBuilder.Append(item.SONO);

            //        operationNos.Add(item.No);
            //        mblIds.Add(item.MBLID);
            //        oIds.Add(item.OceanBookingID);
            //    }
            //    subjuect = LocalData.IsEnglish ? "CHINA SHIPPING(" + mblNoBuilder.ToString() + ")" : "中海电子补料(" + mblNoBuilder.ToString() + ")";

            //    tip = LocalData.IsEnglish ? "CHINA SHIPPING" : "中海";
            //}
            //#endregion


            //EDISendOption sendItem = new EDISendOption();
            //sendItem.ServiceKey = key;
            //sendItem.EdiMode = EDIMode.SI;
            //sendItem.CompanyID = companyID;
            //sendItem.Subject = subjuect;
            //sendItem.IDs = oIds.ToArray();
            //sendItem.FIDs = mblIds.ToArray();
            //sendItem.NOs = operationNos.ToArray();
            //sendItem.OperationType = OperationType.OceanExport;
            //sendItem.AMSEntryType = amsEntryType;
            //sendItem.ACIEntryType = aciEntryType;
            //sendItem.SendByID = LocalData.UserInfo.LoginID;

            //if (mblIds.Count > 0)
            //{
            //    isSucc = EdiClientService.SendEDI(sendItem);
            //    BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
            //    businessOperationParameter.Context = businessOperationContext as BusinessOperationContext;
            //    editPartSaved(new object[] { selectedList, businessOperationParameter });

            //} 
            #endregion
        }

        /// <summary>
        /// EDI VGM
        /// </summary>
        /// <param name="ediClientService"></param>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="amsEntryType"></param>
        /// <param name="aciEntryType"></param>
        /// <param name="isSucc"></param>
        public void InnerEVGM(OceanBLList selectedList, Guid companyID, ref bool isSucc, object businessOperationContext, PartDelegate.EditPartSaved editPartSaved)
        {
            OECommonUtility.InnerEVGM(EdiClientService, selectedList, companyID, ref isSucc);
            if (isSucc)
            {
                BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
                businessOperationParameter.Context = businessOperationContext as BusinessOperationContext;
                editPartSaved(new object[] { selectedList, businessOperationParameter });
            }
        }

        /// <summary>
        /// 发送AMS/AIC/ISF的方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subjuect"></param>
        /// <param name="oIds"></param>
        /// <param name="hblIds"></param>
        /// <param name="operationNos"></param>
        /// <param name="isSucc"></param>
        /// <param name="ediMode"></param>
        /// <param name="CompanyID"></param>
        /// <param name="businessOperationContext"></param>
        /// <param name="editPartSaved"></param>
        public void SendEDI(string key, string subjuect, List<Guid> oIds, List<Guid> hblIds, List<string> operationNos,
                            bool isSucc, object ediMode, Guid CompanyID, object businessOperationContext, PartDelegate.EditPartSaved editPartSaved)
        {
            try
            {
                OceanBookingInfo ob1 = OEService.GetOceanBookingInfo(oIds[0]);

                EDISendOption sendOption = new EDISendOption
                {
                    EdiMode = (EDIMode)ediMode,
                    CompanyID = CompanyID,
                    CarrierID = ob1.CarrierID != null ? ob1.CarrierID.Value : Guid.Empty,
                    AgentOfCarrierID = ob1.AgentOfCarrierID,
                    BookingPartyID = ob1.BookingPartyID==null?Guid.Empty:ob1.BookingPartyID.Value,
                    Subject = subjuect,
                    OperationType = OperationType.OceanExport,
                    SendByID = LocalData.UserInfo.LoginID,
                };
                EDIConfigItem configOption = EdiClientService.GetEDIConfigByOption(sendOption);
                if (configOption != null)
                {
                    sendOption.Subject = configOption.SubjectPrefix+ sendOption.Subject;
                    sendOption.IDs = oIds.ToArray();
                    sendOption.FIDs = hblIds.ToArray();
                    sendOption.NOs = operationNos.ToArray();
                    isSucc = EdiClientService.ShowForm(sendOption, false);
                }
                else
                {

                    EDISendOption sendItem = new EDISendOption();
                    sendItem.ServiceKey = key;
                    sendItem.EdiMode = (EDIMode)ediMode;
                    sendItem.CompanyID = CompanyID;
                    sendItem.Subject = subjuect;
                    sendItem.IDs = oIds.ToArray();
                    sendItem.FIDs = hblIds.ToArray();
                    sendItem.NOs = operationNos.ToArray();
                    sendItem.OperationType = OperationType.Customs;
                    isSucc = EdiClientService.SendEDI(sendItem);
                }

                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                    if(businessOperationContext!=null)
                    {
                        BusinessOperationParameter businessOperationParameter = new BusinessOperationParameter();
                        businessOperationParameter.Context = businessOperationContext as BusinessOperationContext;
                        editPartSaved(new object[] { null, businessOperationParameter });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }
        /// <summary>
        /// 返回当前用户的公司名和部门名
        /// </summary>
        /// <returns></returns>
        public string GetCompany()
        {
            string company = string.Empty;
            //取当前用户的公司名
            var firstOrDefault = LocalData.UserInfo.UserOrganizationList.
                FirstOrDefault(n => n.Type == LocalOrganizationType.Company && n.IsDefault == true);
            if (firstOrDefault != null)
            {
                company = firstOrDefault.EShortName;
                //取当前用户的部门名
                var localOrganizationInfo = LocalData.UserInfo.UserOrganizationList.
                    FirstOrDefault(n => n.Type == LocalOrganizationType.Department);
                if (localOrganizationInfo != null)
                {
                    company = company + "/" + localOrganizationInfo.EShortName;
                }
            }
            return company + ".";
        }

        /// <summary>
        /// 保存事件，修改当前订单的业务流程的状态
        /// </summary>
        /// <param name="eventObjects"></param>
        public void SaveEventState(EventObjects eventObjects, Guid oceanbookid, int UpdateValue)
        {
            //FcmCommonService.SaveMemoInfo(eventObjects);

            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                                {
                                    {"OceanBookingID",oceanbookid },
                                    {eventObjects.Code,UpdateValue}
                                };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
        }



        /// <summary>
        /// 弹出新增联系人列表(在关闭窗体后，会重新调用调用当前发送邮件的方法)
        /// </summary>
        /// <param name="parameter">沟通阶段</param>
        /// <param name="oceanBookingInfo">实体</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameterCollection">参数集合</param>
        public void ShowContacts(ContactStage parameter, OceanBookingInfo oceanBookingInfo, string methodName, object[] parameterCollection)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //构造业务参数
                BusinessOperationContext businessOperation = new BusinessOperationContext
                {
                    OperationID = oceanBookingInfo.ID,
                    OperationType = OperationType.OceanExport,
                    OperationNO = oceanBookingInfo.No
                };
                UCContactListEditPart showUcContactListPart = ServiceClient.GetClientService<WorkItem>().SmartParts.AddNew<UCContactListEditPart>();
                showUcContactListPart.TargetStage = parameter;
                showUcContactListPart.operationContext = businessOperation;
                showUcContactListPart.Isreturnvalue = true;
                showUcContactListPart.operationContext = businessOperation;
                showUcContactListPart.MethodName = methodName;
                showUcContactListPart.ParameterCollection = parameterCollection;
                IWorkspace mainWorkspace = ServiceClient.GetClientService<WorkItem>().Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "New Customer" : "新增联系人";
                mainWorkspace.Show(showUcContactListPart, smartPartInfo);
            }
        }

        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID </param>
        public string MailSoConfirmationToAgent(Guid oceanBookingId)
        {
            return MailSoConfirmationToAgent(oceanBookingId, string.Empty);
        }

        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID </param>
        /// <param name="mailInformation">邮件地址</param>
        public string MailSoConfirmationToAgent(Guid oceanBookingId, string mailInformation)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            oceanbookinginfo.POLCheck = true;
            oceanbookinginfo.PODCheck = true;
            oceanbookinginfo.ContainerCheck = true;
            var top = GetEmailSendValidationInfo(oceanbookinginfo);
            if (!string.IsNullOrEmpty(top))
            {
                return top;
            }
            else
            {
                if (string.IsNullOrEmpty(mailInformation))
                {
                    //读取当前用户的邮箱地址信息
                    mailInformation = MailInformation(oceanbookinginfo.ID, "SO", oceanbookinginfo.AgentID, false, true);
                }
                var salaEMail = UserService.GetUserInfo((Guid)oceanbookinginfo.SalesID);
                EventCode eventCodes = EventCodeList("SOS");
                string[] strSpit = null;
                if (!string.IsNullOrEmpty(mailInformation))
                {
                    strSpit = mailInformation.Split('|');
                }
                else
                {
                    strSpit = new string[] { " ", " ", " " };
                }
                if (strSpit != null)
                {
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanbookinginfo.ID,
                        OperationType = OperationType.OceanExport,
                        FormID = oceanbookinginfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCodes.Code,
                        Description = eventCodes.Subject,
                        Subject = eventCodes.Subject,
                        Priority = MemoPriority.Normal,
                        UpdateDate = DateTime.Now,
                        Owner = LocalData.UserInfo.LoginName,
                        UpdateBy = LocalData.UserInfo.LoginID,
                        CategoryName = eventCodes.Category,
                        IsShowAgent = true,
                        IsShowCustomer = true,
                        Type = MemoType.EmailLog
                    };
                    var message = CreateMessageInfo(MessageType.Email,
                                                     MessageWay.Send, strSpit[0], LocalData.UserInfo.EmailAddress,
                                                     FormType.Booking, OperationType.OceanExport,
                                                     oceanbookinginfo.ID, Guid.Empty,
                                                     string.Empty, string.Empty, salaEMail.EMail, eventCodes.Code, eventObjects, null);
                    MailCenterTemplateService.SendMailWithTemplate(message, true, "MailSoConfirmationToAgent", new object[] { oceanbookinginfo, strSpit[1] });

                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 邮件订舱
        /// </summary>
        /// <param name="oceanBookIngidList">业务ID集合</param>
        public void CommunicationMailBooking(List<Guid> oceanBookIngidList)
        {
            string userName = LocalData.UserInfo.LoginName.Trim();
            UserDetailInfo userDetail = ServiceClient.GetService<IUserService>().GetUserDetailInfo(LocalData.UserInfo.LoginID);
            EventCode eventCodes = EventCodeList("SOB");
            string str = GetCompany();
            string subject = "Booking detail";
            StringBuilder body = new StringBuilder();

            #region   邮件订舱为一票业务时候的处理
            if (oceanBookIngidList.Count() == 1)
            {

                OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookIngidList[0]);
                //构建文本信息
                body.Append("<html>");
                body.Append("<head>");
                body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                body.Append(" <style type=" + "text/css" + ">");
                body.Append(" .MsoNormal");
                body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                body.Append(" </style>");
                body.Append("</head>");
                body.Append("<body><div class=" + "MsoNormal" + ">");

                //收货地
                if (!string.IsNullOrEmpty(oceanbookinginfo.PlaceOfReceiptName))
                {
                    body.Append("POR:" + oceanbookinginfo.PlaceOfReceiptName + "<br/>");
                }
                //卸货港
                if (!string.IsNullOrEmpty(oceanbookinginfo.PODName))
                {
                    body.Append("POD:" + oceanbookinginfo.PODName + "<br/>");
                }
                //装货港
                if (!string.IsNullOrEmpty(oceanbookinginfo.POLName))
                {
                    body.Append("Via:" + oceanbookinginfo.POLName + "<br/>");
                }
                //品名
                if (!string.IsNullOrEmpty(oceanbookinginfo.Commodity))
                {
                    body.Append("Comm:" + oceanbookinginfo.Commodity + "<br/>");
                }
                //箱信息
                if (oceanbookinginfo.ContainerDescription != null)
                {
                    body.Append("Vol:" + oceanbookinginfo.ContainerDescription + "<br/>");
                }
                //合约号
                if (!string.IsNullOrEmpty(oceanbookinginfo.ContractNo))
                {
                    body.Append("S/C:" + oceanbookinginfo.ContractNo + "<br/>");
                }
                //截关日或者截铁路日
                body.Append("Cutoff:" + oceanbookinginfo.RailCutOff == null ? oceanbookinginfo.CYClosingDate.ToString() : oceanbookinginfo.RailCutOff.ToString() + "<br/>");
                //备注
                if (!string.IsNullOrEmpty(oceanbookinginfo.Remark))
                {
                    body.Append("Remark:" + oceanbookinginfo.Remark + "<br/>");
                }
                body.Append("<br/>");
                body.Append("B.rgds,<br />");
                body.Append(userName + "<br />");
                body.Append("Cityocean Logistics Co.,LTD<br />");
                body.Append(str + "<br />");
                body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p>");
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");
                var eventObjects = new EventObjects
                {
                    Id = Guid.NewGuid(),
                    OperationID = oceanBookIngidList[0],
                    OperationType = OperationType.OceanExport,
                    FormID = oceanBookIngidList[0],
                    FormType = FormType.Unknown,
                    Code = eventCodes.Code,
                    Description = eventCodes.Subject,
                    Subject = eventCodes.Subject,
                    Priority = MemoPriority.Normal,
                    UpdateDate = DateTime.Now,
                    Owner = LocalData.UserInfo.LoginName,
                    UpdateBy = LocalData.UserInfo.LoginID,
                    CategoryName = eventCodes.Category,
                    IsShowAgent = true,
                    IsShowCustomer = true,
                    Type = MemoType.EmailLog
                };
                var message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, string.Empty, LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanBookIngidList[0], Guid.Empty, body.ToString(), subject,
                                                string.Empty, eventCodes.Code, eventObjects, null);

                MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);

            }

            #endregion
            #region  业务选择为多票邮件订舱时
            else
            {
                //构建文本信息
                body.Append("<html>");
                body.Append("<head>");
                body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                body.Append(" <style type=" + "text/css" + ">");
                body.Append(" .MsoNormal");
                body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                body.Append(" </style>");
                body.Append("</head>");
                body.Append("<body><div class=" + "MsoNormal" + ">");
                foreach (Guid idGuid in oceanBookIngidList)
                {
                    OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(idGuid);
                    //箱信息
                    body.Append(oceanbookinginfo.ContainerDescription + ",&nbsp;");
                    //收货地-交货地
                    body.Append(oceanbookinginfo.PlaceOfReceiptName + "-" + oceanbookinginfo.PlaceOfDeliveryName + ",&nbsp;");
                    //卸货港
                    body.Append(oceanbookinginfo.PODName + ",&nbsp;");
                    //品名
                    body.Append(oceanbookinginfo.Commodity + ",&nbsp;");
                    //截关日或者截铁路日
                    body.Append(oceanbookinginfo.RailCutOff == null ? oceanbookinginfo.CYClosingDate.ToString() : oceanbookinginfo.RailCutOff.ToString());
                    body.Append("<br/>");

                    var eventObjects = new EventObjects
                    {
                        Id = Guid.NewGuid(),
                        OperationID = idGuid,
                        OperationType = OperationType.OceanExport,
                        FormID = idGuid,
                        FormType = FormType.Unknown,
                        Code = eventCodes.Code,
                        Description = eventCodes.Subject,
                        Subject = eventCodes.Subject,
                        Priority = MemoPriority.Normal,
                        UpdateDate = DateTime.Now,
                        Owner = LocalData.UserInfo.LoginName,
                        UpdateBy = LocalData.UserInfo.LoginID,
                        CategoryName = eventCodes.Category,
                        IsShowAgent = true,
                        IsShowCustomer = true,
                        Type = MemoType.EmailLog
                    };
                }
                body.Append("<br/>");
                body.Append("B.rgds,<br />");
                body.Append(userName + "<br />");
                body.Append("Cityocean Logistics Co.,LTD<br />");
                body.Append(str + "<br />");
                body.Append("Tel:&nbsp;" + userDetail.Tel + "<br />");
                body.Append("Customer Complaint:&nbsp;400-622-0122<br />");
                body.Append("E-mail:&nbsp;" + userDetail.EMail + "<br />");
                body.Append("<a href=" + "http://www.cityocean.com" + ">" + "http://www.cityocean.com</a></p>");
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");
            }
            #endregion


        }

        /// <summary>
        /// 返回当前CODE的事件详细信息
        /// </summary>
        /// <returns></returns>
        public EventCode EventCodeList(string code)
        {
            if (_CacheEventCodeList.Any() == false)
            {
                _CacheEventCodeList = FCMCommonService.GetEventCodeList(OperationType.OceanExport);
            }
            return _CacheEventCodeList.FirstOrDefault(n => n.Code == code);
        }

        /// <summary>
        /// 订单变更发送邮件(订单列表)
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <param name="oldstring">原始数据信息</param>
        /// <param name="updatestring">修改的数据信息</param>
        public void MainOrderChangedOrderInfo(Guid orderID, Guid companyID, string oldstring, string updatestring)
        {
            OceanOrderInfo oceanOrder = OceanExportService.GetOceanOrderInfo(orderID, companyID);
            if (oceanOrder == null) return;
            List<UserInfo> userInfos = OceanExportService.GetOceanOperatorEmailAddress(oceanOrder.ID, LocalData.UserInfo.LoginID, 0);
            if (userInfos.Any() == false)
            {
                return;
            }
            string subject = "The bkg is changed," + "#[" + oceanOrder.RefNo + ", from " + oceanOrder.POLName + "to" +
                             oceanOrder.PODName + ",[" + oceanOrder.ContainerDescription + "]";
            string name = string.Empty;
            string email = string.Empty;
            foreach (var item in userInfos)
            {
                name = item.EName + "," + name;
                email = item.EMail + ";" + email;
            }
            string body = BookIngChangedBody(name, oceanOrder.RefNo, oceanOrder.POLName, oceanOrder.PODName,
                                             oceanOrder.ContainerDescription.ToString(), oldstring, updatestring);

            EventCode eventCodes = EventCodeList("SOM");
            var eventObjects = new EventObjects
            {
                OperationID = oceanOrder.ID,
                OperationType = OperationType.OceanExport,
                FormID = oceanOrder.ID,
                FormType = FormType.Unknown,
                Code = eventCodes.Code,
                Description = eventCodes.Subject,
                Subject = eventCodes.Subject,
                Priority = MemoPriority.Normal,
                UpdateDate = DateTime.Now,
                Owner = LocalData.UserInfo.LoginName,
                UpdateBy = LocalData.UserInfo.LoginID,
                CategoryName = eventCodes.Category,
                IsShowAgent = true,
                IsShowCustomer = true,
                Type = MemoType.EmailLog

            };
            Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, email, LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanOrder.ID, Guid.Empty,
                                                body, subject, string.Empty, string.Empty, eventObjects, null);
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.State = MessageState.Success;
            MessageService.Send(message);
            #region  修改业务状态
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", eventObjects.OperationID},
                                {eventObjects.Code,"1"},
                                {"OperationType",OperationType.OceanExport}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
            #endregion

        }

        /// <summary>
        /// 询价-商务员邮件(订单列表)
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <param name="inquiryno">询价号</param>
        public void MainOrderChangedInquir(Guid orderID, Guid companyID, string inquiryno)
        {
            OceanOrderInfo oceanOrder = OceanExportService.GetOceanOrderInfo(orderID, companyID);
            if (oceanOrder == null) return;
            List<UserInfo> userInfos = OceanExportService.GetOceanOperatorEmailAddress(oceanOrder.ID, LocalData.UserInfo.LoginID, 1);
            if (userInfos.Any() == false)
            {
                return;
            }

            string subject = " Inquire Ocean Rates," + "#[" + inquiryno + ", from" + oceanOrder.POLName + "to" +
                             oceanOrder.PODName + ",[" + oceanOrder.ContainerDescription + "]";
            StringBuilder body = new StringBuilder();
            //内容
            body.Append("<html>");
            body.Append("<head>");
            body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
            body.Append(" <style type=" + "text/css" + ">");
            body.Append(" .MsoNormal");
            body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
            body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
            body.Append(" </style>");
            body.Append("</head>");
            body.Append("<body><div class=" + "MsoNormal" + ">");
            body.Append("Hi" + "  " + userInfos[0].EName + "," + "<br/><br/>");
            body.Append("Please reply the rates of the ocean booking, from" + oceanOrder.POLName + "to" + oceanOrder.PODName + ",[" + oceanOrder.ContainerDescription + "]" + "%" + inquiryno + "<br/>");
            body.Append("</div>");
            body.Append("</body>");
            body.Append("</html>");

            EventCode eventCodes = EventCodeList("SOCCD");
            var eventObjects = new EventObjects
            {
                OperationID = oceanOrder.ID,
                OperationType = OperationType.OceanExport,
                FormID = oceanOrder.ID,
                FormType = FormType.Unknown,
                Code = eventCodes.Code,
                Description = eventCodes.Subject,
                Subject = eventCodes.Subject,
                Priority = MemoPriority.Normal,
                UpdateDate = DateTime.Now,
                Owner = LocalData.UserInfo.LoginName,
                UpdateBy = LocalData.UserInfo.LoginID,
                CategoryName = eventCodes.Category,
                IsShowAgent = true,
                IsShowCustomer = true,
                Type = MemoType.EmailLog

            };
            Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, userInfos[0].EMail, LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanOrder.ID, Guid.Empty,
                                                body.ToString(), subject, string.Empty, string.Empty, eventObjects, null);
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.State = MessageState.Success;
            MessageService.Send(message);
            #region  修改业务状态
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", eventObjects.OperationID},
                                {eventObjects.Code,"1"},
                                {"OperationType",OperationType.OceanExport}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
            #endregion
        }

        /// <summary>
        /// 订舱变更发送邮件(订舱列表)
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="oldstring">原始数据信息</param>
        /// <param name="updatestring">修改的数据信息</param>
        public void MainBookIngChangedOceanBookingInfo(Guid oceanBookingId, string oldstring, string updatestring)
        {
            OceanBookingInfo oceanbookinginfo = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            if (oceanbookinginfo == null) return;
            List<UserInfo> userInfos = OceanExportService.GetOceanOperatorEmailAddress(oceanbookinginfo.ID, LocalData.UserInfo.LoginID, 0);
            if (userInfos.Any() == false)
            {
                return;
            }
            string subject = "The bkg is changed," + "#[" + oceanbookinginfo.No + ", from" + oceanbookinginfo.POLName + "to" +
                             oceanbookinginfo.PODName + ",[" + oceanbookinginfo.ContainerDescription + "]";
            string name = string.Empty;
            string email = string.Empty;
            foreach (var item in userInfos)
            {
                name = item.EName + "," + name;
                email = item.EMail + ";" + email;
            }
            string body = BookIngChangedBody(name, oceanbookinginfo.No, oceanbookinginfo.POLName, oceanbookinginfo.PODName,
                                             oceanbookinginfo.ContainerDescription.ToString(), oldstring, updatestring);

            EventCode eventCodes = EventCodeList("SOM");
            var eventObjects = new EventObjects
            {
                OperationID = oceanbookinginfo.ID,
                OperationType = OperationType.OceanExport,
                FormID = oceanbookinginfo.ID,
                FormType = FormType.Unknown,
                Code = eventCodes.Code,
                Description = eventCodes.Subject,
                Subject = eventCodes.Subject,
                Priority = MemoPriority.Normal,
                UpdateDate = DateTime.Now,
                Owner = LocalData.UserInfo.LoginName,
                UpdateBy = LocalData.UserInfo.LoginID,
                CategoryName = eventCodes.Category,
                IsShowAgent = true,
                IsShowCustomer = true,
                Type = MemoType.EmailLog

            };
            Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, email, LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanbookinginfo.ID, Guid.Empty,
                                                body, subject, string.Empty, string.Empty, eventObjects, null);
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.State = MessageState.Success;
            MessageService.Send(message);
            #region  修改业务状态
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", eventObjects.OperationID},
                                {eventObjects.Code,"1"},
                                {"OperationType",OperationType.OceanExport}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
            #endregion

        }

        /// <summary>
        /// 订舱变更邮件正文
        /// </summary>
        /// <returns></returns>
        public string BookIngChangedBody(string name, string no, string pol, string pod, string container, string oldstring, string updatestring)
        {
            StringBuilder body = new StringBuilder();
            //内容
            body.Append("<html>");
            body.Append("<head>");
            body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
            body.Append(" <style type=" + "text/css" + ">");
            body.Append(" .MsoNormal");
            body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
            body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
            body.Append(" </style>");
            body.Append("</head>");
            body.Append("<body><div class=" + "MsoNormal" + ">");
            body.Append("Hi" + "  " + name + "," + "<br/><br/>");
            body.Append("The bkg is changed, # [" + no + "], from" + pol + "TO" + pod + "," + "[" + container + "]" + "<br/>");
            body.Append("<br/><br/>变更的信息：" + "<br/><br/>");
            body.Append(updatestring);
            body.Append("<br/><br/>原始信息：" + "<br/><br/>");
            body.Append(oldstring);
            body.Append("</div>");
            body.Append("</body>");
            body.Append("</html>");
            return body.ToString();
        }
        /// <summary>
        /// 询价-商务员邮件
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="inquiryno">询价号</param>
        public void MainBookIngChangedInquir(Guid oceanBookingId, string inquiryno)
        {
            OceanBookingInfo oceanOrder = OceanExportService.GetOceanBookingInfo(oceanBookingId);
            if (oceanOrder == null) return;
            List<UserInfo> userInfos = OceanExportService.GetOceanOperatorEmailAddress(oceanOrder.ID, LocalData.UserInfo.LoginID, 1);
            if (userInfos.Any() == false)
            {
                return;
            }

            string subject = " Inquire Ocean Rates," + "#[" + inquiryno + ", from" + oceanOrder.POLName + "to" +
                             oceanOrder.PODName + ",[" + oceanOrder.ContainerDescription + "]";
            StringBuilder body = new StringBuilder();
            //内容
            body.Append("<html>");
            body.Append("<head>");
            body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
            body.Append(" <style type=" + "text/css" + ">");
            body.Append(" .MsoNormal");
            body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
            body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
            body.Append(" </style>");
            body.Append("</head>");
            body.Append("<body><div class=" + "MsoNormal" + ">");
            body.Append("Hi" + "  " + userInfos[0].EName + "," + "<br/><br/>");
            body.Append("Please reply the rates of the ocean booking, from" + oceanOrder.POLName + "to" + oceanOrder.PODName + ",[" + oceanOrder.ContainerDescription + "]" + "%" + inquiryno + "<br/>");
            body.Append("</div>");
            body.Append("</body>");
            body.Append("</html>");

            EventCode eventCodes = EventCodeList("SOCCD");
            var eventObjects = new EventObjects
            {
                OperationID = oceanOrder.ID,
                OperationType = OperationType.OceanExport,
                FormID = oceanOrder.ID,
                FormType = FormType.Unknown,
                Code = eventCodes.Code,
                Description = eventCodes.Subject,
                Subject = eventCodes.Subject,
                Priority = MemoPriority.Normal,
                UpdateDate = DateTime.Now,
                Owner = LocalData.UserInfo.LoginName,
                UpdateBy = LocalData.UserInfo.LoginID,
                CategoryName = eventCodes.Category,
                IsShowAgent = true,
                IsShowCustomer = true,
                Type = MemoType.EmailLog

            };
            Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, userInfos[0].EMail, LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanOrder.ID, Guid.Empty,
                                                body.ToString(), subject, string.Empty, string.Empty, eventObjects, null);
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.State = MessageState.Success;
            MessageService.Send(message);
            #region  修改业务状态
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", eventObjects.OperationID},
                                {eventObjects.Code,"1"},
                                {"OperationType",OperationType.OceanExport}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
            #endregion
        }

        #region 邮件中心使用的方法
        /// <summary>
        /// 打开联系人面板
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationNo">业务号</param>
        public void ShowContactsAndAssistants(Guid operationId, OperationType operationType, string operationNo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //构造业务参数
                BusinessOperationContext businessOperation = new BusinessOperationContext
                {
                    OperationID = operationId,
                    OperationType = operationType,
                    OperationNO = operationNo
                };
                UCContactListPartFrom ucContactListPartFrom = new UCContactListPartFrom();
                ucContactListPartFrom.Text = LocalData.IsEnglish ? "Show Contacts and Assistants" : "查看联系人/参与者";
                ucContactListPartFrom.BusinessOperationContext = businessOperation;
                ucContactListPartFrom.Show();
                ucContactListPartFrom.FindForm().TopMost = true;

            }
        }
        public static object obj = new object();
        /// <summary>
        /// 上传附件通用方法，打开上传附件界面
        /// </summary>
        /// <param name="way">附件方式</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="mailSubject">主题</param>
        /// <param name="objMailItem">邮件实体</param>
        /// <param name="type">工作类型</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="addDocList">附件集合</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="upDateTime">修改时间</param>
        /// <param name="message">MESSAGE对象</param>
        public void Upload(UploadWay way, Guid operationId, string mailSubject, object objMailItem,
            SelectionType type, OperationType operationType,
            List<string> addDocList, string operationNo, DateTime? upDateTime, Message.ServiceInterface.Message message, string TemplateCode)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                lock (obj)
                {
                    using (frmUploadSIAttachment frmUploadSiAttachment = new frmUploadSIAttachment())
                    {
                        UploadAttachmentParameter parameter = new UploadAttachmentParameter();
                        if (operationType == OperationType.OceanExport)
                        {
                            OceanBookingInfo oceanbookinginfo =
                                ServiceClient.GetService<IOceanExportService>()
                                             .GetOceanBookingInfo(operationId);
                            if (oceanbookinginfo == null)
                            {
                                MessageBox.Show(LocalData.IsEnglish
                                    ? "The selected shipment is not exsits!"
                                    : "选择的业务不存在!");

                                return;
                            }
                            parameter = CreateUploadAttachmentParameter(way, type, operationType, oceanbookinginfo,
                                        CreateBusinessOperationContext(operationId, operationNo, operationType, upDateTime),
                                        objMailItem, message, TemplateCode);
                        }
                        else if (operationType == OperationType.OceanImport)
                        {

                            parameter = CreateUploadAttachmentParameter(way, type, operationType, null,
                                        CreateBusinessOperationContext(operationId, operationNo, operationType, upDateTime),
                                        objMailItem, message, TemplateCode);

                        }

                        Dictionary<string, object> docValues = new Dictionary<string, object>();
                        //添加附件
                        if (addDocList.Count != 0)
                        {
                            docValues.Add("addDocList", addDocList);
                        }
                        ClientHelper.ActiveMainForm();
                        PartLoader.ShowEditPart<frmUploadSIAttachment>(ServiceClient.GetClientService<WorkItem>(), parameter,
                            docValues, LocalData.IsEnglish ? parameter.OperationContext.OperationNO + " Upload Attachment" : parameter.OperationContext.OperationNO + " 上传附件", null, parameter.OperationContext.OperationNO);
                    }
                }
            }
        }

        /// <summary>
        /// 构造上传附件参数实体
        /// </summary>
        /// <param name="way"></param>
        /// <param name="attachmentType"></param>
        /// <param name="operationType"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="operationContext"></param>
        /// <param name="mailItem"></param>
        /// <returns></returns>
        public UploadAttachmentParameter CreateUploadAttachmentParameter(UploadWay way, SelectionType attachmentType,
                                                                          OperationType operationType,
                                                                          OceanBookingInfo oceanBookingInfo,
                                                                          BusinessOperationContext operationContext,
                                                                          object mailItem, Message.ServiceInterface.Message Message,
                                                                          string TemplateCode)
        {
            return new UploadAttachmentParameter()
            {
                SelectionType = attachmentType,
                OceanBookingInfo = oceanBookingInfo,
                MessageInfo = Message,
                OperationContext = operationContext,
                OperationType = operationType,
                MailItem = mailItem,
                TemplateCode = TemplateCode,
                UploadWay = way
            };
        }


        /// <summary>
        /// 构造上下文实体类
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="upDateTime">修改时间</param>
        /// <returns></returns>
        public BusinessOperationContext CreateBusinessOperationContext(Guid operationId, string operationNo,
            OperationType operationType, DateTime? upDateTime)
        {
            return new BusinessOperationContext
            {
                OperationID = operationId,
                OperationNO = operationNo,
                OperationType = operationType,
                UpdateDate = upDateTime
            };
        }

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        public void ShowContactPart(AssociateType associateType, List<BusinessOperationContext> bussOperationContexts,
         Message.ServiceInterface.Message message)
        {
            if (bussOperationContexts.Count > 0)
            {
                ClientHelper.ActiveMainForm();
                List<object> objects = new List<object> { message, associateType, bussOperationContexts };
                PartLoader.ShowEditPart<UCContactListWithToolbar>(ServiceClient.GetClientService<WorkItem>(), objects,
                    null, LocalData.IsEnglish ? "Set Contact" : "设置联系人", null, typeof(UCContactListWithToolbar).ToString() + bussOperationContexts[0].OperationNO);
            }
            else
            {
                MessageBox.Show(LocalData.IsEnglish ? "selected  at least one shipment!" : "至少选择一票业务!");
            }
        }

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="contactType">联系人类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        public void SaveContactList(EnumContactType contactType
            , List<BusinessOperationContext> bussOperationContexts, Message.ServiceInterface.Message message)
        {
            if (bussOperationContexts != null && bussOperationContexts.Count > 0)
            {
                List<MailContactInfo> mailContactList = MailContactInfo.GetAllExternalContacts(message);
                List<CustomerCarrierObjects> currentShipmentCustomerCarriers = new List<CustomerCarrierObjects>();

                //外部联系人
                for (int index = 0; index < bussOperationContexts.Count; index++)
                {
                    BusinessOperationContext bocItem = bussOperationContexts[index];
                    currentShipmentCustomerCarriers.Clear();
                    //转换当前业务联系人
                    currentShipmentCustomerCarriers.AddRange(mailContactList.Select(mailContactInfo => CreateEntity(bocItem.OperationID, mailContactInfo, bocItem.OperationType, contactType)));

                    //找到当前业务联系人
                    List<CustomerCarrierObjects> searchShipmentCustomerCarriers = FcmCommonService.GetContactList(bocItem.OperationID, bocItem.OperationType).CustomerCarrier;

                    if (searchShipmentCustomerCarriers != null && searchShipmentCustomerCarriers.Count > 0)
                    {
                        //找到第一票业务联系人
                        List<CustomerCarrierObjects> firstShipmentCustomerCarriers =
                            Framework.ClientComponents.Controls.Utility.Clone(currentShipmentCustomerCarriers);
                        if (firstShipmentCustomerCarriers != null && firstShipmentCustomerCarriers.Count > 0)
                        {
                            List<CustomerCarrierObjects> newCustomerCarriers =
                                ReplaceCustomerCarriers(searchShipmentCustomerCarriers, firstShipmentCustomerCarriers, bocItem.OperationID, bocItem.OperationType);
                            FcmCommonService.SaveContactList(newCustomerCarriers);
                        }
                    }
                    else
                    {
                        List<CustomerCarrierObjects> firstShipmentCustomerCarriers =
                           Framework.ClientComponents.Controls.Utility.Clone(currentShipmentCustomerCarriers);
                        foreach (var item in firstShipmentCustomerCarriers)
                        {
                            item.OceanBookingID = bocItem.OperationID;
                            item.OperationType = bocItem.OperationType;
                            item.Id = Guid.NewGuid();
                            item.UpdateDate = null;
                        }

                        FcmCommonService.SaveContactList(firstShipmentCustomerCarriers);
                    }

                    //OperationMessageRelation relation = ICP.FCM.Common.ServiceInterface.Utility.CreateOperationMessageRelationInfo(
                    //Guid.NewGuid(), null,
                    //bocItem.OperationID,
                    //bocItem.OperationType, message.MessageId,
                    //message.Id, ContactStage.Unknown, message.EntryID);
                    //relation.BackupMail = true;
                    //if (index == 0)
                    //    relation.BackupMail = message.BackupMailState;
                    //relation.RelationType = MessageRelationType.Hand;
                    //message.UserProperties = new MessageUserPropertiesObject();
                    //message.UserProperties.OperationId = bocItem.OperationID;
                    //message.UserProperties.OperationType = bocItem.OperationType;
                    //message.UserProperties.FormId = bocItem.OperationID;
                    //message.UserProperties["ContactStage"] = contactType;
                    //message.UserProperties.FormType = bocItem.FormType;
                    //message.Type = MessageType.Email;
                    //message.State = MessageState.Success;
                    //ClientBusinessOperationService.EnsureAndSaveMailMessageReference(relation, message);

                    LocalSave(mailContactList, bocItem);
                }
            }
            else
            {
                MessageBox.Show(LocalData.IsEnglish ? "selected  at least one shipment!" : "至少选择一票业务!");
            }
        }

        private CustomerCarrierObjects CreateEntity(Guid operationID, MailContactInfo mailContactInfo
            , OperationType operationType, EnumContactType contactType)
        {
            CustomerCarrierObjects customerCarrier = new CustomerCarrierObjects();
            #region  根据传递过来的默认选择用户类型的参数选择客户的默认类型
            FCM.Common.ServiceInterface.FCMInterfaceUtility.SetStage(customerCarrier, ContactStage.Unknown);
            #endregion
            customerCarrier.IsCC = (mailContactInfo.ContactType == MailContactType.olCC);
            customerCarrier.CreateDate = DateTime.Now;
            customerCarrier.CreateByID = LocalData.UserInfo.LoginID;
            customerCarrier.UpdateDate = DateTime.Now;
            customerCarrier.UpdateByID = LocalData.UserInfo.LoginID;
            customerCarrier.Type = contactType;
            customerCarrier.OceanBookingID = operationID;
            if (!string.IsNullOrEmpty(mailContactInfo.EmailAddress))
            {
                customerCarrier.Mail = mailContactInfo.EmailAddress;
                customerCarrier.Name = mailContactInfo.Name;
            }
            return customerCarrier;
        }

        private CustomerCarrierObjects ReplaceCustomerCarrierInfo(List<CustomerCarrierObjects> currentShipmentCustomerCariiers, CustomerCarrierObjects newCustomerCariier)
        {
            CustomerCarrierObjects info = currentShipmentCustomerCariiers.Find(item => item.Mail.Equals(newCustomerCariier.Mail.Trim(), StringComparison.OrdinalIgnoreCase));
            if (info != null)
            {
                newCustomerCariier.Id = info.Id;
                newCustomerCariier.UpdateByID = LocalData.UserInfo.LoginID;
                newCustomerCariier.UpdateDate = info.UpdateDate;
            }
            return newCustomerCariier;
        }

        private void LocalSave(IEnumerable<MailContactInfo> mailContactList, BusinessOperationContext OperationContext)
        {
            List<string> emailList = mailContactList.Select(item => item.EmailAddress).ToList();
            List<OperationContactInfo> contactList = ClientBusinessOperationService.GetOperationContactByEmails(emailList);
            ClientBusinessOperationService.LocalSaveOperationContacts(contactList);
            ClientBusinessOperationService.UpdateLocalBusinessData(OperationContext.OperationID, OperationContext.OperationType);
        }

        /// <summary>
        /// 将用户编辑的联系人替换为更新之前的联系人
        /// </summary>
        /// <param name="currentShipmentCustomerCarriers"></param>
        /// <param name="firstShipmentCustomerCarriers"></param>
        /// <returns></returns>
        private List<CustomerCarrierObjects> ReplaceCustomerCarriers(List<CustomerCarrierObjects> currentShipmentCustomerCarriers, List<CustomerCarrierObjects> firstShipmentCustomerCarriers, Guid operationID, OperationType operationType)
        {
            int count = firstShipmentCustomerCarriers.Count;
            for (int j = 0; j < count; j++)
            {
                //如果当前业务联系人包含第一票业务的联系人  
                CustomerCarrierObjects info = currentShipmentCustomerCarriers.Find(item => item.Mail.Trim().Equals(firstShipmentCustomerCarriers[j].Mail.Trim(), StringComparison.CurrentCultureIgnoreCase));
                if (info != null)
                {
                    firstShipmentCustomerCarriers[j].Id = info.Id;
                    //firstShipmentCustomerCarriers[j].IsCC = info.IsCC;
                    //firstShipmentCustomerCarriers[j].SO = info.SO;
                    //firstShipmentCustomerCarriers[j].SI = info.SI;
                    firstShipmentCustomerCarriers[j].OceanBookingID = info.OceanBookingID;
                    firstShipmentCustomerCarriers[j].UpdateDate = info.UpdateDate;
                    firstShipmentCustomerCarriers[j].UpdateByID = info.UpdateByID;
                }
                else
                {
                    firstShipmentCustomerCarriers[j].Id = Guid.NewGuid();
                    firstShipmentCustomerCarriers[j].OperationType = operationType;
                    firstShipmentCustomerCarriers[j].OceanBookingID = operationID;
                }
            }

            return firstShipmentCustomerCarriers;
        }


        /// <summary>
        /// 邮件查询
        /// </summary>
        /// <param name="no">业务号</param>
        /// <param name="noType">业务类型</param>
        /// <param name="types">分类</param>
        /// <param name="dateType">时间类型</param>
        /// <param name="main">邮件地址</param>
        /// <param name="area">时间段</param>
        /// <param name="text">标题</param>
        /// <param name="operationId">业务号</param>
        /// <param name="operationType">业务类型</param>
        public void OeEmailQueryPartShow(string no, int noType, int types, int dateType, string main, int? area, string text, Guid? operationId, OperationType operationType)
        {
            string EmailAddress = string.Empty;
            if (operationId != Guid.Empty && operationId != null && !string.IsNullOrEmpty(main))
            {
                EmailAddress = ServiceClient.GetService<IBusinessQueryService>().GetCustomerMailList((Guid)operationId, main);
                //判断客户联系人的地址信息是否为空,如果为空出现提示信息，不弹出窗体
                if (string.IsNullOrEmpty(EmailAddress))
                {
                    MessageBox.Show(LocalData.IsEnglish
                        ? "Business contact email address was not found, please choose again"
                        : "业务联系人邮件地址不存在,请重新选择");
                    return;
                }
            }
            OEEmailQueryPart oeEmailQueryPart = new OEEmailQueryPart
            {
                No = no,
                NoType = noType,
                Types = types,
                DateType = dateType,
                Mail = EmailAddress,
                StartPosition = FormStartPosition.CenterScreen,
                Text = text,
                Area = area
            };
            oeEmailQueryPart.FindForm().TopMost = true;
            oeEmailQueryPart.Show();
        }
        /// <summary>
        /// 邮件中心是否刷新
        /// </summary>
        /// <returns></returns>
        public bool MailCenterRefresh(bool? flg)
        {
            if (flg != null)
            {
                MailCenterParameter.MailCenterRefresh = (bool)flg;
            }
            return MailCenterParameter.MailCenterRefresh;
        }

        /// <summary>
        /// 高级查询
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        public string Advancedquery(OperationType operationType)
        {
            string advanceQueryString = string.Empty;
            var initValues = new Dictionary<string, object>();
            if (operationType == OperationType.Unknown)
            {
                return advanceQueryString;
            }
            else
            {
                //节点为子节点时操作
                switch (operationType)
                {
                    case OperationType.OceanExport:
                        initValues.Add(Constants.BusinessTypeKey, BusinessType.OE);
                        break;
                    case OperationType.OceanImport:
                        initValues.Add(Constants.BusinessTypeKey, BusinessType.OI);
                        break;
                }
            }
            //高级查找时是否显示可以选择业务（1为可以选择。0为不可选择）
            initValues.Add("ShowOperationType", 0);
            frmAdvanceQuery frmAdvanceQuerys = new frmAdvanceQuery();
            frmAdvanceQuerys.Init(initValues);
            frmAdvanceQuerys.FindForm().TopMost = true;
            var result = frmAdvanceQuerys.ShowDialog();
            if (result == DialogResult.OK)
            {
                advanceQueryString = frmAdvanceQuerys.AdvanceQueryString;
            }
            return advanceQueryString;
        }


        /// <summary>
        /// 判断当前联系人是否存在 
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="message">邮件</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="operation">业务类型</param>
        /// <returns></returns>
        public bool SaveOperationMessage(AssociateType associateType, Message.ServiceInterface.Message message, Guid operationId, OperationType operation)
        {
            bool flg = false;
            //判断邮件中联系人是否是内部邮件或外部邮件
            var externalTempContacts = FCM.Common.ServiceInterface.FCMInterfaceUtility.GetExternalMailToContactList(message, operationId, operation, false, false, false);
            //如何为内部邮件自动关联
            if (externalTempContacts == null || externalTempContacts.Count == 0)
            {
                flg = true;
            }
            else
            {
                //获取集合的Mail地址
                List<string> externalEmails = (from ec in externalTempContacts select ec.Mail).ToList();
                //得到业务联系人
                List<OperationContactInfo> operationContactInfos = FCM.Common.ServiceInterface.FCMInterfaceUtility.GetOperationContactListByEmails(externalEmails);
                if (operationContactInfos.Any())
                {
                    //业务联系人是否和当前的邮件地址数量相等
                    if (operationContactInfos.Count == externalEmails.Count)
                    {
                        var serverAllContacts = ServiceClient.GetService<IFCMCommonService>().GetOceanContactsByEmails(externalEmails);
                        if (serverAllContacts.Any())
                        {
                            flg = true;
                        }
                    }
                }
            }
            return flg;
        }
        #endregion

        #region Comment Code
        /// <summary>
        /// 订舱变更发送邮件(订单列表)
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="oldstring">原始数据信息</param>
        /// <param name="updatestring">修改的数据信息</param>
        public void MainBookIngChangedOrderInfo(Guid oceanBookingId, string oldstring, string updatestring)
        {
            OceanOrderInfo oceanOrder = OceanExportService.GetOceanOrderInfo(oceanBookingId, Guid.Empty);
            if (oceanOrder == null) return;
            List<UserInfo> userInfos = OceanExportService.GetOceanOperatorEmailAddress(oceanOrder.ID, LocalData.UserInfo.LoginID, 0);
            if (userInfos.Any() == false)
            {
                return;
            }
            string subject = "The bkg is changed," + "#[" + oceanOrder.RefNo + ", from " + oceanOrder.POLName + "to" +
                             oceanOrder.PODName + ",[" + oceanOrder.ContainerDescription + "]";
            string name = string.Empty;
            string email = string.Empty;
            foreach (var item in userInfos)
            {
                name = item.EName + "," + name;
                email = item.EMail + ";" + email;
            }
            string body = BookIngChangedBody(name, oceanOrder.RefNo, oceanOrder.POLName, oceanOrder.PODName,
                                             oceanOrder.ContainerDescription.ToString(), oldstring, updatestring);

            EventCode eventCodes = EventCodeList("SOM");
            var eventObjects = new EventObjects
            {
                OperationID = oceanOrder.ID,
                OperationType = OperationType.OceanExport,
                FormID = oceanOrder.ID,
                FormType = FormType.Unknown,
                Code = eventCodes.Code,
                Description = eventCodes.Subject,
                Subject = eventCodes.Subject,
                Priority = MemoPriority.Normal,
                UpdateDate = DateTime.Now,
                Owner = LocalData.UserInfo.LoginName,
                UpdateBy = LocalData.UserInfo.LoginID,
                CategoryName = eventCodes.Category,
                IsShowAgent = true,
                IsShowCustomer = true,
                Type = MemoType.EmailLog

            };
            Message.ServiceInterface.Message message = CreateMessageInfo(MessageType.Email,
                                                MessageWay.Send, email, LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanExport,
                                                oceanOrder.ID, Guid.Empty,
                                                body, subject, string.Empty, string.Empty, eventObjects, null);
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.State = MessageState.Success;
            MessageService.Send(message);
            #region  修改业务状态
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", eventObjects.OperationID},
                                {eventObjects.Code,"1"},
                                {"OperationType",OperationType.OceanExport}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            BusinessQueryService.Save(business);
            #endregion
        }
        #endregion
    }
    class BLReportClientData : BLReportData
    {
        public string EtdString { get; set; }
        public string IssueDateString { get; set; }
        public string NumberOfOriginalString { get; set; }
    }
}

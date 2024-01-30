using ICP.Business.Common.ServiceInterface;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.EventList;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Forms;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;
using Constants = ICP.Operation.Common.ServiceInterface.Constants;
using FormType = ICP.Framework.CommonLibrary.Common.FormType;
using Utility = ICP.FCM.OceanImport.UI.Utility;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 海出命令处理类
    /// </summary>
    public class OceanExportCommandHandler : IBaseComnandHandler
    {
        #region Fields & Property & Services
        /// <summary>
        /// Lock Object
        /// </summary>
        public static object obj = new object();
        /// <summary>
        /// 公共命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonCommandHandler CommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// Work Item
        /// </summary>
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// 海出订单实体
        /// </summary>
        public OceanBookingInfo Oceanbookinginfo;

        /// <summary>
        /// 当前的业务处理面板
        /// </summary>
        public BaseBusinessPart CurrentBaseBusinessPart
        {
            get { return RootWorkItem.State["CurrentBaseBusinessPart"] as BaseBusinessPart; }
            set { RootWorkItem.State["CurrentBaseBusinessPart"] = value; }
        }

        /// <summary>
        /// 消息实体
        /// </summary>
        public Message.ServiceInterface.Message CurrentMessage
        {
            get
            {
                return RootWorkItem.State[Constants.CurrentMessageKey] as Message.ServiceInterface.Message;
            }

        }

        /// <summary>
        /// 业务面板需要传递的信息类
        /// </summary>
        public BusinessPartParam BusinessPartParam
        {
            get
            {
                return new BusinessPartParam
                     {
                         TemplateCode = CurrentBaseBusinessPart.TemplateCode,
                         OperationNo = CurrentBaseBusinessPart.OperationNo,
                         ID = CurrentBaseBusinessPart.OperationID,
                         CompanyID = CurrentBaseBusinessPart.CompanyID,
                         OperationType = OperationType.OceanExport,
                         CurrentMessage = CurrentMessage,
                         CurrentBusinessPart = CurrentBaseBusinessPart,
                         Updatetime = CurrentBaseBusinessPart.Updatetime
                     };
            }

        }

        /// <summary>
        /// FCM 公共服务
        /// </summary>
        public IFCMCommonService FcmCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// FCM 公共服务接口
        /// </summary>
        public IICPCommonOperationService IcpCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }
        /// <summary>
        /// 客户端业务实现接口
        /// </summary>
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IClientBusinessOperationService>();
            }
        }

        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {

            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }

        /// <summary>
        /// 询价服务
        /// </summary>
        private IInquireRatesService _inquireRatesService;
        /// <summary>
        /// 询价服务
        /// </summary>
        public IInquireRatesService InquireRatesService
        {
            get
            {
                return _inquireRatesService ?? (_inquireRatesService = ServiceClient.GetService<IInquireRatesService>());
            }
        }
        #endregion

        /// <summary>
        /// 打印订舱确认书(宁波)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandPrintBookingConfirmation4NB(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
                BusinessPartParam.OperationNo != string.Empty)
            {
                IcpCommonOperationService.PrintOEBookingConfirmation4NB(BusinessPartParam.ID);
            }
        }

        /// <summary>
        /// 复制业务
        /// </summary>
        public void CopyBusiness(IListBaseBusinessPart listPart)
        {
            if (CurrentBaseBusinessPart == null || BusinessPartParam.ID == Guid.Empty ||
                BusinessPartParam.OperationNo == string.Empty) return;

            BusinessOperationContext context = new BusinessOperationContext
            {
                OperationID = CurrentBaseBusinessPart.OperationID,
                OperationNO = CurrentBaseBusinessPart.OperationNo,
                CompanyID = CurrentBaseBusinessPart.CompanyID,
                OperationType = CurrentBaseBusinessPart.OperationType,
                UpdateDate = CurrentBaseBusinessPart.Updatetime
            };
            BusinessOperationContext newContext = IcpCommonOperationService.OceanExportCopyBusiness(context);
            if (newContext!=null)
            {
                listPart.OperationID = newContext.OperationID;
                listPart.OperationNo = newContext.OperationNO;
                listPart.CompanyID = newContext.CompanyID;
                ListRefreshByContext(OperationType.OceanExport, listPart, newContext);
            }
        }

        /// <summary>
        /// 查看订单状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandViewOrderStates(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
                BusinessPartParam.OperationNo != string.Empty)
            {
                UCOrderStates ucOrderStates = RootWorkItem.Items.AddNew<UCOrderStates>();
                ucOrderStates.OperationID = BusinessPartParam.ID;
                string title = LocalData.IsEnglish ? "View Ocean Export Order States" : "查看海出订单状态";
                PartLoader.ShowDialog(ucOrderStates, title);
            }
        }

        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterOpenSo(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
                BusinessPartParam.OperationNo != string.Empty)
            {
                var editPartShowCriteria = new EditPartShowCriteria
                    {
                        BillNo = BusinessPartParam.ID,
                        OperationNo = BusinessPartParam.OperationNo
                    };
                Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
                IcpCommonOperationService.EditBooking(editPartShowCriteria, dictionary);
            }
        }

        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForSoAdd(object sender, EventArgs eventArgs)
        {
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Create, true);
            object value;
            dictionary.TryGetValue("businessOperationParameter", out value);
            if (value != null && value.GetType() == typeof(BusinessOperationParameter))
            {
                BusinessOperationParameter businessOperation = value as BusinessOperationParameter;
                if (businessOperation != null) 
                    businessOperation.ContactStage = ContactStage.SO;
            }
            IcpCommonOperationService.AddBooking(dictionary);
        }

        /// <summary>
        /// 创建一个MBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterSilsMbl(object sender, EventArgs e)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            if (!string.IsNullOrEmpty(listPart.SONO))
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {

                    Dictionary<string, object> dic = CommandHandler.CreateBusinessParameter(ActionType.Create, false);
                    IcpCommonOperationService.AddMBL(dic);
                }
            }

            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "MBL could not be created due to SONO is not filled." : "当前订舱单没有SONO无法建立MBL.");
            }
        }

        /// <summary>
        /// 打开询价
        /// </summary>
        public void CommandOpenInquireRatesListPart()
        {
            IcpCommonOperationService.OpenOceanInquireRateListPart();
        }

        /// <summary>
        /// 创建一个HBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterSilsHbl(object sender, EventArgs e)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            if (!string.IsNullOrEmpty(listPart.SONO))
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {

                    Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Create, false);
                    IcpCommonOperationService.AddHBL(dictionary);
                }
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "HBL could not be created due to SONO is not filled." : "当前订舱单没有SONO无法建立HBL.");
            }

        }
        /// <summary>
        /// 海运询价
        /// </summary>
        /// <param name="inquierType"></param>
        /// <returns></returns>
        public string GetReallyInquireHistoryName(InquierType inquierType)
        {
            int count = 0;
            try
            {
                InquierOceanRatesResult data = InquireRatesService.GetInquireOceanRateList(string.Empty,
                                                                   string.Empty,
                                                                   string.Empty,
                                                                   string.Empty,
                                                                   LocalData.UserInfo.LoginID,
                                                                   true,
                                                                   null,
                                                                   null,
                                                                   LocalData.UserInfo.LoginID);
                if (data != null || data.UnReadCountList != null)
                {
                    UnReadDiscussingCount item = data.UnReadCountList.Find(o => o.Type.Equals(inquierType));
                    if (item != null)
                    {
                        count = item.CountOfUnreply;
                        item = null;
                    }
                    data = null;
                }

                return string.Format("{0}{1}{2}", LocalData.UserInfo.LoginName, (LocalData.IsEnglish ? "'s Inquire History" : "的询价历史"), (count == 0 ? "" : "(" + count.ToString() + ")"));
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 编辑MBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterEditMbl(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
                IcpCommonOperationService.EditMBL(listPart.OperationNo, listPart.CurrentContextMenuItemTag.ToString(), dictionary);
            }
        }

        /// <summary>
        /// 编辑HBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterEditHbl(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                Guid id = BusinessPartParam.ID;
                Dictionary<string, object> dic = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
                IcpCommonOperationService.EditHBL(listPart.OperationNo, listPart.CurrentContextMenuItemTag.ToString(), dic);
            }
        }

        /// <summary>
        /// 打开提单
        /// </summary>
        public void CommandTaskCenterCommunicationBLInfo()
        {
            IcpCommonOperationService.OpenBillOfLoadingList(CurrentBaseBusinessPart.OperationNo);
        }

        /// <summary>
        /// 打开报关单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterOrderCustoms(object sender, EventArgs e)
        {
            Guid id = BusinessPartParam.ID;
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.OpenCustomsOrder(CurrentBaseBusinessPart.OperationID, dictionary);
        }

        /// <summary>
        /// 打开派车单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterOrderTruck(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Guid id = BusinessPartParam.ID;
                Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
                IcpCommonOperationService.OpenTruckOrder(id, BusinessPartParam.OperationNo, dictionary);
            }
        }

        /// <summary>
        /// 打开账单列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterCommunicationAccInfo(object sender, EventArgs e)
        {
            Guid id = CurrentBaseBusinessPart.OperationID;
            IcpCommonOperationService.OpenBill(id, OperationType.OceanExport);
        }

        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForSoApplyAgent(object sender, EventArgs eventArgs)
        {
            var id = BusinessPartParam.ID;
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.ReplyAgent(id, OperationType.OceanExport, dictionary);
        }

        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public void CommandTaskCenterForSoOnBoard(DataRow dataRow)
        {
            if (dataRow == null)
                return;
            string mblno = string.Empty;
            var id = BusinessPartParam.ID;
            if (dataRow.Table.Columns.Contains("BLNO"))
            {
                string blno = dataRow["BLNO"].ToString();
                if (!string.IsNullOrEmpty(blno))
                {
                    mblno = blno.Replace("\n", "*").Split('*')[0].Replace("MBL:", string.Empty);
                }

            }
            IcpCommonOperationService.LoadShip(BusinessPartParam.OperationNo.Trim(), mblno.Trim());
        }

        /// <summary>
        /// 复制订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForSoCopy(object sender, EventArgs eventArgs)
        {
            var id = BusinessPartParam.ID;
            var editPartShowCriteria = new EditPartShowCriteria
                   {
                       BillNo = BusinessPartParam.ID,
                       OperationNo = BusinessPartParam.OperationNo
                   };
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.CopyBooking(editPartShowCriteria, dictionary);
        }

        /// <summary>
        /// 快速订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForFasterAdd(object sender, EventArgs eventArgs)
        {
            //var id = BusinessPartParam.ID;
            //var editPartShowCriteria = new EditPartShowCriteria
            //{
            //    BillNo = BusinessPartParam.ID,
            //    OperationNo = BusinessPartParam.OperationNo
            //};
            ////Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.OpenAddBookingEdi(this.RootWorkItem);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForSoCancel(object sender, EventArgs eventArgs)
        {
            CancelOrder();
        }

        /// <summary>
        /// 取消订单或激活业务
        /// </summary>
        public void CancelOrder()
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            //取消单元格编辑模式
            listPart.bindingSource.EndEdit();
            listPart.gridViewList.CloseEditor();
            IcpCommonOperationService.SetTemplateCode(CurrentBaseBusinessPart.TemplateCode);
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ManyResult results = IcpCommonOperationService.OECancelBusinesss(listPart.TaskCenterSelectedIsValidIDs.ToArray(), (bool)listPart.IsValid);
                //if (results != null)
                //{
                //    ListRefresh(OperationType.OceanExport, listPart);
                //}
            }

            //var id = BusinessPartParam.ID;
            //IcpCommonOperationService.SetTemplateCode(CurrentBaseBusinessPart.TemplateCode);
            //SingleResult result = IcpCommonOperationService.CancelOEBusiness(id);
            //if (result != null)
            //{
            //    ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            //    if (listPart != null)
            //        listPart.IsValid = !listPart.IsValid;
            //}
        }

        /// <summary>
        /// 恢复订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForSoResume(object sender, EventArgs eventArgs)
        {
            var id = BusinessPartParam.ID;
            IcpCommonOperationService.CancelOEBusiness(id);
            CommandHandler.Refersh();
        }

        /// <summary>
        /// 业务联单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForSoPrintOrder(object sender, EventArgs eventArgs)
        {
            var id = BusinessPartParam.ID;
            IcpCommonOperationService.PrintOrder(id, BusinessPartParam.CompanyID);
        }

        /// <summary>
        /// 订舱确认书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterForSoPrint(object sender, EventArgs eventArgs)
        {
            var id = BusinessPartParam.ID;
            IcpCommonOperationService.PrintBookingConfirm(id);
        }

        /// <summary>
        /// 打开Bkg Failed 页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OpenBkgFailed(object sender, EventArgs eventArgs)
        {
            var workItem = RootWorkItem;
            var bkgFailed = new BkgFailed();
            bkgFailed = workItem.Items.AddNew<BkgFailed>();
            var title = LocalData.IsEnglish ? "Bkg Failed" : "订舱失败";
            PartLoader.ShowDialog(bkgFailed, title);
        }

        /// <summary>
        /// 分发文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OpenDispatchDocument(object sender, EventArgs eventArgs)
        {
            IcpCommonOperationService.DispatchDocument(BuildBussinessInfo());
        }

        /// <summary>
        /// 询价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void CommandTaskCenterCommunicationInquireRates(object sender, EventArgs eventArgs)
        {
            IcpCommonOperationService.NewInquireRate();
        }

        /// <summary>
        /// EDI电子订舱
        /// </summary>
        /// <param name="listBase">业务列表面板</param>
        public void CommandEmbl(IListBaseBusinessPart listBase)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            //取消单元格编辑模式
            listPart.bindingSource.EndEdit();
            listPart.gridViewList.CloseEditor();
            bool flg = false;
            //List<Guid> oceanBookingId = new List<Guid>();
            //foreach (Guid guids in listPart.TaskCenterSelectedIDs)
            //{
            //    oceanBookingId.Add(guids);
            //}
            flg = IcpCommonOperationService.EBookingCall(listPart.TaskCenterSelectedIDs);
            if (flg)
            {
                listBase.IsShowLoadingForm = true;
                bool isMergeAdvanceQueryString = !string.IsNullOrEmpty(listBase.AdvanceQueryString);
                listBase.QueryData(isMergeAdvanceQueryString);
            }
            else
            {
                ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// EDI电子订舱确认
        /// </summary>
        /// <param name="listBase">业务列表面板</param>
        public void CommandEBookConfirm(IListBaseBusinessPart listBase)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            //取消单元格编辑模式
            listPart.bindingSource.EndEdit();
            listPart.gridViewList.CloseEditor();
            bool flg = false;
            //List<Guid> oceanBookingId = new List<Guid>();
            //foreach (Guid guids in listPart.TaskCenterSelectedIDs)
            //{
            //    oceanBookingId.Add(guids);
            //}
            flg = IcpCommonOperationService.EBookingConfirm(listPart.TaskCenterSelectedIDs);
            if (flg)
            {
                listBase.IsShowLoadingForm = true;
                bool isMergeAdvanceQueryString = !string.IsNullOrEmpty(listBase.AdvanceQueryString);
                listBase.QueryData(isMergeAdvanceQueryString);
            }
            else
            {
                ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// 业务上下文
        /// </summary>
        /// <returns></returns>
        BusinessOperationContext BuildBussinessInfo()
        {
            var context = BusinessOperationContext.Current;
            context.OperationID = CurrentBaseBusinessPart.OperationID;
            context.OperationType = OperationType.OceanExport;
            context.CompanyID = CurrentBaseBusinessPart.CompanyID;
            context.FormType = FormType.Booking;
            context.FormId = CurrentBaseBusinessPart.FormID;
            return context;
        }

        /// <summary>
        /// 客户补料 邮件发送的方法
        /// </summary>
        /// <param name="isEnglish">是否发送英文版本</param>
        public void AskCustomerForSi(bool isEnglish)
        {
            ShowErrorInfo(IcpCommonOperationService.MailCustomerAskForSi(isEnglish, CurrentBaseBusinessPart.OperationID));
        }

        /// <summary>
        /// 客户订舱成功 邮件发送的方法
        /// </summary>
        /// <param name="isEnglish">是否为英文版本</param>
        public void MailSoCopyToCustomer(bool isEnglish)
        {
            if (CurrentBaseBusinessPart.OperationID != Guid.Empty)
            {
                ShowErrorInfo(IcpCommonOperationService.MailSoCopyToCustomer(isEnglish,
                                                                             CurrentBaseBusinessPart.OperationID));
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "Please select need to operate the business." : "请选择需要操作的业务.");
            }
        }

        /// <summary>
        ///  通知客户订舱失败 邮件发送的方法
        /// </summary>
        /// <param name="isEnglish"></param>
        public void MailSoFailureToCustomer(bool isEnglish)
        {
            if (CurrentBaseBusinessPart.OperationID != Guid.Empty)
            {
                ShowErrorInfo(IcpCommonOperationService.MailCustomerForSoFailure(isEnglish, CurrentBaseBusinessPart.OperationID));
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "Please select need to operate the business." : "请选择需要操作的业务.");
            }
        }

        /// <summary>
        /// 取消订舱 邮件发送的方法
        /// </summary>
        /// <param name="isEnglish">是否为英文版本</param>
        public void MailSoCancelationtoCustomer(bool isEnglish)
        {
            if (CurrentBaseBusinessPart.OperationID != Guid.Empty)
            {
                ShowErrorInfo(IcpCommonOperationService.MailCustomerForCancelBooking(isEnglish, CurrentBaseBusinessPart.OperationID));
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "Please select need to operate the business." : "请选择需要操作的业务.");
            }
        }

        /// <summary>
        /// 费用确认
        /// </summary>
        /// <param name="isEnglish">是否发送英文版本</param>
        public void ConfirmDebitFees(bool isEnglish)
        {
            ShowErrorInfo(IcpCommonOperationService.MailSalesForConfirmDebitFees(isEnglish, CurrentBaseBusinessPart.OperationID));
        }

        /// <summary>
        /// 利润承诺
        /// </summary>
        /// <param name="isEnglish">是否发送英文版本</param>
        public void AskProfitPromise(bool isEnglish)
        {
            ShowErrorInfo(IcpCommonOperationService.MailSalesManAskForProfitPromise(isEnglish, CurrentBaseBusinessPart.OperationID));

        }

        /// <summary>
        /// 客户确认补料发送邮件
        /// </summary>
        /// <param name="isEnglish">是否发送英文版本</param>
        public void MailBlCopyToCustomer(bool isEnglish)
        {
            //ShowErrorInfo(ICPCommonOperationService.MailCustomerAskForConfirmSI(isEnglish, CurrentBaseBusinessPart.OperationID));
            //CurrentBaseBusinessPart.RefreshDetail();
        }

        /// <summary>
        /// 向订舱员发起申请订舱 发送邮件
        /// </summary>
        public void CommandTaskCenterForSoApplySo()
        {
            ShowErrorInfo(IcpCommonOperationService.MailBookingClerkForApplySO(CurrentBaseBusinessPart.OperationID));
        }

        /// <summary>
        /// 通知客户确认补料
        /// </summary>
        public void MailCenterCustomerConfirmSI()
        {

        }

        /// <summary>
        /// 打开商检单
        /// </summary>
        public void MailCenterCommidityInspection()
        {
        }

        /// <summary>
        /// 打开任务中心
        /// </summary>
        /// <param name="operationContext"></param>
        public void OpenTaskCenter(BusinessOperationContext operationContext)
        {
            IcpCommonOperationService.OpenTaskCenter(operationContext);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="result">提示信息内容</param>
        private void ShowErrorInfo(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, result);
            }
        }

        /// <summary>
        /// 装箱
        /// </summary>
        public void LoadContainer()
        {
            IcpCommonOperationService.LoadContainer(BusinessPartParam.OperationNo, BusinessPartParam.ID);
        }

        /// <summary>
        /// 打开事件列表
        /// </summary>
        /// <param name="listBase">数据列表</param>
        public void Open(IListBaseBusinessPart listBase)
        {
            listBase = listBase as ListBaseBusinessPart;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PopupWindow form = null;
                EventEditPart frmEventEditPart = RootWorkItem.SmartParts.AddNew<EventEditPart>();
                form = new PopupWindow();
                frmEventEditPart.BusinessEventDataSource = listBase.EventObjects;
                frmEventEditPart.OperationType = listBase.OperationType;
                frmEventEditPart.isNew = true;
                form.MaximizeBox = form.MinimizeBox = false;
                form.Width = 580;
                form.Height = 400;
                form.ShowInTaskbar = true;
                form.KeyPreview = true;
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.Text = LocalData.IsEnglish ? "New Event" : "新增事件";
                form.StartPosition = FormStartPosition.CenterScreen;
                frmEventEditPart.Dock = DockStyle.Fill;
                form.Controls.Add(frmEventEditPart);
                form.ShowDialog();
                if (RootWorkItem.State["Indicates"] == null) return;
                bool flg = Convert.ToBoolean(RootWorkItem.State["Indicates"].ToString());
                if (!flg) return;
                ListBaseBusinessPart listBaseBusinessPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                ListRefresh(listBase.OperationType, listBase);
            }
        }

        /// <summary>
        /// 列表刷新
        /// </summary>
        /// <param name="operationType">业务列别</param>
        /// <param name="listBase">数据列表</param>
        public void ListRefresh(OperationType operationType, IListBaseBusinessPart listBase)
        {
            try
            {
                if (operationType == OperationType.Unknown || listBase == null) return;
                ListBaseBusinessPart listPart = listBase as ListBaseBusinessPart;
                BusinessOperationParameter business = null;
                business = new BusinessOperationParameter();
                BusinessOperationContext businessOperationContext = new BusinessOperationContext
                {
                    OperationID = listBase.OperationID,
                    OperationNO = listBase.OperationNo,
                    CompanyID = listBase.CompanyID,
                    OperationType = operationType
                };
                //businessOperationContext.Add("CompanyID", listBase.CompanyID);
                //businessOperationContext.Add("CarrierID", BusinessPartParam.CurrentRow["CarrierID"]);
                //businessOperationContext.Add("AgentOfCarrierID", BusinessPartParam.CurrentRow["AgentOfCarrierID"]);
                business.Context = businessOperationContext;
                business.TemplateCode = listBase.TemplateCode;
                string columnQueryString = "1=1";
                columnQueryString = columnQueryString + "and $@" + Constants.OperationIDFieldName + "@='" + listBase.OperationID + "'";
                RootWorkItem.State["columnQueryString"] = columnQueryString;
                if (listPart == null) return;
                listPart.OnBusinessOperationCompleted(null, new CommonEventArgs<BusinessOperationParameter>(business));
                if (RootWorkItem.State["Indicates"] != null)
                {
                    RootWorkItem.State["Indicates"] = null;
                }
                if (listPart.CurrentTabItem != null)
                {
                    var taskCenterDataBind = listPart.CurrentTabItem.Control as IDataBind;
                    if (taskCenterDataBind != null)
                    {
                        taskCenterDataBind.DataBind(businessOperationContext);
                        taskCenterDataBind.ControlsReadOnly(listPart.CurrentTabItem.ReadOnly);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    error += innerException.Message;
                    innerException = innerException.InnerException;
                }
                LogHelper.SaveLog(error);
            }
        }

        private void ListRefreshByContext(OperationType operationType, IListBaseBusinessPart listBase, BusinessOperationContext context)
        {
            try
            {
                if (operationType == OperationType.Unknown || context == null) return;
                ListBaseBusinessPart listPart = listBase as ListBaseBusinessPart;
                BusinessOperationParameter business = null;
                business = new BusinessOperationParameter();
                BusinessOperationContext businessOperationContext = new BusinessOperationContext
                {
                    OperationID = context.OperationID,
                    OperationNO = context.OperationNO,
                    CompanyID = context.CompanyID,
                    OperationType = operationType
                };
                business.Context = businessOperationContext;
                business.TemplateCode = listBase.TemplateCode;
                string columnQueryString = "1=1";
                columnQueryString = columnQueryString + "and $@" + Constants.OperationIDFieldName + "@='" + context.OperationID + "'";
                RootWorkItem.State["columnQueryString"] = columnQueryString;
                if (listPart == null) return;
                listPart.OnBusinessOperationCompleted(null, new CommonEventArgs<BusinessOperationParameter>(business));
                if (RootWorkItem.State["Indicates"] != null)
                {
                    RootWorkItem.State["Indicates"] = null;
                }
                if (listPart.CurrentTabItem != null)
                {
                    var taskCenterDataBind = listPart.CurrentTabItem.Control as IDataBind;
                    if (taskCenterDataBind != null)
                    {
                        taskCenterDataBind.DataBind(businessOperationContext);
                        taskCenterDataBind.ControlsReadOnly(listPart.CurrentTabItem.ReadOnly);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    error += innerException.Message;
                    innerException = innerException.InnerException;
                }
                LogHelper.SaveLog(error);
            }
        }

        ///<summary>
        /// 弹出海出修订签收比较界面
        /// </summary>
        public void ShowReviseAccepte()
        {
            //FcmCommonOperationService.ShowReviseAccepte(BusinessPartParam.ID);
        }

        /// <summary>
        /// 审核当前业务存在利润
        /// </summary>
        public void SetUpdateOceanTrackings(Guid operationId)
        {
            IcpCommonOperationService.SetUpdateOceanTrackings(operationId);
        }

        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="isEnglish">发送邮件版本</param>
        public void MailSoConfirmationToCustomer(bool isEnglish)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            if (!string.IsNullOrEmpty(listPart.SONO))
            {
                IcpCommonOperationService.MailSoConfirmationToCustomer(listPart.OperationID, isEnglish);
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "Could not send mail. You should fill-in the SO NO first." : "SONO为空,无法操作.");
            }
        }

        /// <summary>
        /// SOD成功以后发送邮件给客服(So Copy)
        /// </summary>
        public void MainSOCustomerServiceSOD()
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            IcpCommonOperationService.MainSOCustomerServiceSOD(listPart.OperationID, MemoType.EmailLog, "Upload the So Copy", true);
        }

        /// <summary>
        /// SOD成功以后发送邮件给客服(文档列表上传附件)
        /// </summary>
        public void MainSOCustomerServiceSODDocument()
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            if (listPart.CurrentRow.Table.Columns.Contains("SOD"))
            {
                if (bool.Parse(listPart.CurrentRow["SOD"].ToString()) == false)
                {
                    IcpCommonOperationService.MainSOCustomerServiceSOD(listPart.OperationID, MemoType.EmailLog, "Document", true);
                }
            }
        }

        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        public void MailSoConfirmationToAgent()
        {
            ShowErrorInfo(IcpCommonOperationService.MailSoConfirmationToAgent(CurrentBaseBusinessPart.OperationID));
        }

        /// <summary>
        /// 邮件订舱
        /// </summary>
        public void CommunicationMailBooking()
        {
            List<Guid> id = new List<Guid>();
            id.Add(CurrentBaseBusinessPart.OperationID);
            IcpCommonOperationService.CommunicationMailBooking(id);
        }


        #region 询价使用的方法
        /// <summary>
        /// 邮件-询问人
        /// </summary>
        public void EmailMailtoAskpeople()
        {
            IcpCommonOperationService.EmailMailtoAskpeople(BusinessPartParam.ID);
        }
        /// <summary>
        /// 邮件-承运人
        /// </summary>
        public void EmailMailtoCarrier()
        {
            IcpCommonOperationService.EmailtoCarrier(BusinessPartParam.ID);
        }
        #endregion


        #region 邮件中心命令处理方法

        /// <summary>
        /// 弹出窗体
        /// </summary>
        /// <param name="no">业务号</param>
        /// <param name="noType">业务号类别</param>
        /// <param name="types">分类</param>
        /// <param name="dateType">时间类型</param>
        /// <param name="main">邮件地址</param>
        /// <param name="area">区域</param>
        /// <param name="text">文本</param>
        public void OEEmailQueryPartShow(string no, int noType, int types, int dateType, string main, int? area, string text)
        {
            OEEmailQueryPart oeEmailQueryPart = new OEEmailQueryPart
                {
                    No = no,
                    NoType = noType,
                    Types = types,
                    DateType = dateType,
                    Mail = main,
                    StartPosition = FormStartPosition.CenterScreen,
                    Text = text,
                    Area = area
                };
            oeEmailQueryPart.Show();
        }

        /// <summary>
        /// 此业务的客户历史邮件
        /// </summary>
        public void CommandMailMailofCustomer()
        {
            OEEmailQueryPartShow(BusinessPartParam.OperationNo, 0, 1, 0, string.Empty, null,
                                 LocalData.IsEnglish ? "The shipment's mail history of Customer" : "此业务的客户历史邮件");
        }


        /// <summary>
        /// 此业务的承运人历史邮件
        /// </summary>
        public void CommandMailHistoryofCarrier()
        {
            OEEmailQueryPartShow(BusinessPartParam.OperationNo, 0, 2, 0, string.Empty, null,
                                 LocalData.IsEnglish ? "The shipment's mail history of Carrier" : "此业务的承运人历史邮件");
        }

        /// <summary>
        ///  此业务的代理历史邮件
        /// </summary>
        public void CommandMailHistoryofAgent()
        {
            OEEmailQueryPartShow(BusinessPartParam.OperationNo, 0, 3, 0, string.Empty, null,
                                 LocalData.IsEnglish ? "The shipment's mail history of Agent" : "此业务的代理历史邮件");
        }

        /// <summary>
        /// 此业务的历史邮件
        /// </summary>
        public void CommandMailMailHistory()
        {
            OEEmailQueryPartShow(BusinessPartParam.OperationNo, 0, 0, 0, string.Empty, null,
                                 LocalData.IsEnglish ? "The shipment's mail history" : "此业务的历史邮件");
        }

        /// <summary>
        /// 客户的历史邮件
        /// </summary>
        public void CommandMailHistoryofCustomers()
        {
            string main = BusinessQueryService.GetCustomerMailList(BusinessPartParam.ID, "4,5,6,1");
            OEEmailQueryPartShow(string.Empty, 1, 0, 1, main, 7,
                                 LocalData.IsEnglish ? "Mail history of Customer" : "客户的历史邮件");
        }

        /// <summary>
        /// 业务的承运人历史邮件
        /// </summary>
        public void CommandMailHistoryofCarriers()
        {
            string main = BusinessQueryService.GetCustomerMailList(BusinessPartParam.ID, "3,2,1");
            OEEmailQueryPartShow(string.Empty, 1, 0, 1, main, 7,
                                 LocalData.IsEnglish ? "Mail history of Carrier" : "业务的承运人历史邮件");
        }

        /// <summary>
        /// 业务的代理历史邮件
        /// </summary>
        public void CommandMailHistoryofAgents()
        {
            string main = BusinessQueryService.GetCustomerMailList(BusinessPartParam.ID, "8,1");
            OEEmailQueryPartShow(string.Empty, 1, 0, 1, main, 7,
                                 LocalData.IsEnglish ? "Mail history of Agent" : "业务的代理历史邮件");
        }

        #endregion


        #region  取消订舱 和 订舱失败

        /// <summary>
        /// 弹出输入UI 
        /// </summary>
        /// <param name="theme">邮件主题</param>
        /// <param name="operationId">业务单号</param>
        /// <param name="cancelBooking"></param>
        /// <param name="failureBooking"></param>
        /// <param name="text">表头</param>
        public void BkgFailedShow(string theme, Guid operationId, CancelBooking cancelBooking, FailureBooking failureBooking, string text)
        {

            var OEBkgFailed = new OEBkgFailed
                {
                    Theme = theme,
                    OperationId = operationId,
                    CancelBooking = cancelBooking,
                    FailureBooking = failureBooking,
                    StartPosition = FormStartPosition.CenterScreen,
                    Text = text,
                };
            OEBkgFailed.ShowDialog();
            if (RootWorkItem.State["Indicates"] != null)
            {
                bool flg = Convert.ToBoolean(RootWorkItem.State["Indicates"].ToString());
                if (flg)
                {
                    ListRefresh(OperationType.OceanExport, CurrentBaseBusinessPart as IListBaseBusinessPart);
                }
            }
        }
        /// <summary>
        /// 订舱失败 爆仓
        /// </summary>
        public void CommandFailureBlastingWarehouse()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Booking Failure(Blasting Warehouse)" : "订舱失败原因爆仓";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.Unknown, FailureBooking.BlastingWarehouse, theme);
            }
        }

        /// <summary>
        /// 订舱失败 柜型
        /// </summary>
        public void CommandFailureCubicleType()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Booking Failure(Cubicle Type)" : "订舱失败原因柜型";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.Unknown, FailureBooking.CubicleType, theme);
            }
        }
        /// <summary>
        /// 订舱失败 船期
        /// </summary>
        public void CommandFailureShippingDate()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Booking Failure(Shipping Date)" : "订舱失败原因船期";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.Unknown, FailureBooking.ShippingDate, theme);
            }
        }
        /// <summary>
        /// 订舱失败 其他
        /// </summary>
        public void CommandFailureOther()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Booking Failure(Other)" : "订舱失败原因其他";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.Unknown, FailureBooking.FailureOther, theme);
            }
        }

        /// <summary>
        /// 取消订舱 货源
        /// </summary>
        public void CommandCancelSourcing()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Cancel Booking(Sourcing)" : "取消订舱原因货源";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.Sourcing, FailureBooking.Unknown, theme);
            }
        }

        /// <summary>
        /// 取消订舱 价格
        /// </summary>
        public void CommandCancelPrice()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Cancel Booking(Price)" : "取消订舱原因价格";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.Price, FailureBooking.Unknown, theme);
            }

        }

        /// <summary>
        /// 取消订舱 贸易
        /// </summary>
        public void CommandCancelTrade()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Cancel Booking(Trade)" : "取消订舱原因贸易";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.Trade, FailureBooking.Unknown, theme);
            }

        }

        /// <summary>
        /// 取消订舱 贸易
        /// </summary>
        public void CommandCancelOther()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string theme = LocalData.IsEnglish ? "Cancel Booking(Other)" : "取消订舱原因其他";
                BkgFailedShow(theme, CurrentBaseBusinessPart.OperationID, CancelBooking.CancelOther, FailureBooking.Unknown, theme);
            }
        }
        #endregion


        /// <summary>
        ///  恢复订舱
        /// </summary>
        public void RestoreBooking(string eventCode)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart != null && listPart.OeOrderState == OEOrderState.CancelBooking)
            {
                if (IcpCommonOperationService.RestoreBooking(BusinessPartParam.ID))
                {
                    var business = new List<BusinessSaveParameter>();
                    var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", BusinessPartParam.ID},
                                {eventCode,"0"},
                                {"OperationType",OperationType.OceanExport}
                            };
                    var businessSave = new BusinessSaveParameter { items = dictionary };
                    business.Add(businessSave);
                    BusinessQueryService.Save(business);
                    ShowErrorInfo(LocalData.IsEnglish ? "Operation is successful...." : "操作成功....");
                }
            }
        }

        /// <summary>
        /// 快捷方式打开任务中心
        /// </summary>
        public void QuickOpenTaskCenter()
        {
            RootWorkItem.Commands[ClientCommandConstants.TASKCENTER_QUICKOPEN].Execute();
            //WorkItem rootWorkItem = RootWorkItem;
            //ViewListSmartPart viewListSmart = rootWorkItem.Items.Get<ViewListSmartPart>("ViewListSmartPart");
            //if (viewListSmart == null)
            //{
            //    OpenTaskCenter(null);
            //}
            //else
            //{
            //    IWorkspace mainWorkspace = rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            //    MainWorkSpace mainWorkSpace =
            //        rootWorkItem.Items.Get<MainWorkSpace>("MainWorkSpacePart");
            //    mainWorkspace.Activate(mainWorkSpace);
            //}
        }


    }
}



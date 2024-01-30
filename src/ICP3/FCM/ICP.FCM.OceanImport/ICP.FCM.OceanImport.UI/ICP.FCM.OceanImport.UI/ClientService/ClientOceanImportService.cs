using DevExpress.XtraEditors;
using ICP.Business.Common.ServiceInterface;
using ICP.Business.Common.UI.Contact;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OceanImport.UI.Common;
using ICP.FCM.OceanImport.UI.Report;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ICP.FCM.OceanImport.UI
{
    /// <summary>
    /// 海进客户端实现类
    /// </summary>
    public class ClientOceanImportService : IClientOceanImportService
    {
        #region Fields
        /// <summary>
        /// 事件集合列表
        /// </summary>
        private List<EventCode> _eventCodeList = new List<EventCode>();
        #endregion

        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        /// <summary>
        /// FCM Common
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 海进服务
        /// </summary>
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }
        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 财务服务(客户端)
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }
        /// <summary>
        /// 财务服务(服务端)
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 工作流服务
        /// </summary>
        public IWorkflowClientService WorkflowClientService
        {
            get
            {
                return ServiceClient.GetClientService<IWorkflowClientService>();
            }
        }
        /// <summary>
        /// 海进下载WorkItem
        /// </summary>
        public OIBusinessDownLoadWorkitem OIBusinessDownLoadWorkitem
        {
            get
            {
                return ClientHelper.Get<OIBusinessDownLoadWorkitem, OIBusinessDownLoadWorkitem>();
            }
        }
        /// <summary>
        /// 海进打印服务
        /// </summary>
        public OceanImportPrintHelper OceanImportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanImportPrintHelper, OceanImportPrintHelper>();
            }
        }
        /// <summary>
        /// 客户端文件上传服务
        /// </summary>
        public IClientFileService ClientFileService
        {
            get { return ServiceClient.GetClientService<IClientFileService>(); }

        }
        /// <summary>
        /// 沟通记录服务(历史邮件/Email)
        /// </summary>
        public ICommunicationHistoryService CommunicationHistoryService
        {
            get { return ServiceClient.GetService<ICommunicationHistoryService>(); }
        }
        /// <summary>
        /// 海出服务(客户端)
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }
        /// <summary>
        /// 发送邮件服务
        /// </summary>
        public IMainCenterEmailTemplateGetter MainCenterEmailTemplateGetter
        {
            get { return ServiceClient.GetClientService<IMainCenterEmailTemplateGetter>(); }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 发送邮件模板服务
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
        /// 
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// Message Service
        /// </summary>
        public IMessageService MessageService
        {
            get { return ServiceClient.GetService<IMessageService>(); }
        }

        #endregion

        #region Edit
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        public void AddOrder(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = ((LocalData.IsEnglish) ? "Add Order: " : "新增订单: ");
                PartLoader.ShowEditPart<OIOrderBaseEdit>(RootWorkItem, null, EditMode.New, values, title, editPartSaved, title);
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
                string title = ((LocalData.IsEnglish) ? "Edit Order: " : "编辑订单: ") + Utility.GetLineNo(showCriteria.OperationNo);
                PartLoader.ShowEditPart<OIOrderBaseEdit>(RootWorkItem, showCriteria, EditMode.Edit, values, title, editPartSaved, showCriteria.BillNo.ToString());
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-ORDER", string.Format("打开海进订单编辑;Order No[{0}]", showCriteria.OperationNo));

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
                if (showCriteria == null)
                {
                    return;
                }
                string title = (LocalData.IsEnglish ? "Copy Order:" : "复制订单:") + Utility.GetLineNo(showCriteria.OperationNo);
                PartLoader.ShowEditPart<OIBusinessEdit>(RootWorkItem, showCriteria, EditMode.Copy, values, title, editPartSaved, showCriteria.BillNo.ToString());
            }
        }
        /// <summary>
        /// 编辑业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void EditBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (showCriteria == null)
                {
                    return;
                }

                Stopwatch stopwatch = Stopwatch.StartNew();
                string title = (LocalData.IsEnglish ? "Edit Business:" : "编辑业务:") + Utility.GetLineNo(showCriteria.OperationNo);
                PartLoader.ShowEditPart<OIBusinessEdit>(RootWorkItem, showCriteria, EditMode.Edit, values, title, editPartSaved, showCriteria.BillNo.ToString());
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-BUSINESS", string.Format("打开海进业务编辑;Business No[{0}]", showCriteria.OperationNo));
            }
        }
        /// <summary>
        /// 添加业务
        /// </summary>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void AddBusiness(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = ((LocalData.IsEnglish) ? "Add Business: " : "新增业务: ");
                PartLoader.ShowEditPart<OIBusinessEdit>(RootWorkItem, null, EditMode.New, values, title, editPartSaved, title);
            }
        }
        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void CopyBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (showCriteria == null)
                {
                    return;
                }
                string title = (LocalData.IsEnglish ? "Copy Business:" : "复制业务:") + Utility.GetLineNo(showCriteria.OperationNo);
                PartLoader.ShowEditPart<OIBusinessEdit>(RootWorkItem, showCriteria, EditMode.Copy, values, title, editPartSaved, showCriteria.BillNo.ToString());
            }
        }

        #endregion

        #region Show Form
        /// <summary>
        /// 通用确认窗体
        /// </summary>
        /// <param name="formTitle"></param>
        /// <param name="values"></param>
        /// <param name="title"></param>
        /// <param name="editPartSaved"></param>
        public void CommonConfirmForm(string formTitle, IDictionary<string, object> values, string title, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (string.IsNullOrEmpty(formTitle))
                {
                    return;
                }
                PartLoader.ShowEditPartInDialog<ConfirmBookingForm>(RootWorkItem, formTitle, values, title, editPartSaved);
            }
        }
        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="BusinessID"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void BusinessTransfer(Guid BusinessID, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Business Transfer" : "业务转移";
                PartLoader.ShowEditPartInDialog<OIBusinessTransfer>(RootWorkItem, BusinessID, values, title, editPartSaved);
            }
        }
        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="values"></param>
        /// <param name="title"></param>
        /// <param name="editPartSaved"></param>
        public void OpenDeliveryNotice(Guid businessID, IDictionary<string, object> values, string title, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<OIBusinessTruckEdit>(RootWorkItem, businessID, EditMode.Edit, values, title, editPartSaved, title);
            }
        }
        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void OpenDeliveryNotice(Guid businessID, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(businessID);
                if (businessList == null)
                {
                    return;
                }
                if (businessList.MBLID == null || businessList.MBLID == Guid.Empty)
                {
                    MessageBoxService.ShowInfo(NativeLanguageService.GetText(null, "11091600001"));
                    return;
                }
                string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";
                PartLoader.ShowEditPart<OIBusinessTruckEdit>(RootWorkItem, businessID, EditMode.Edit, values, title, editPartSaved, title);
            }
        }
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="currentFormID"></param>
        public void OpenBill(Guid businessID, Guid currentFormID)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(businessID, OperationType.OceanImport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = currentFormID;
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationid"></param>
        public void OpenBill(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(operationid, OperationType.OceanImport);
                if (operationCommonInfo != null && businessList.MBLID != Guid.Empty)
                {
                    operationCommonInfo.CurrentFormID = (Guid)businessList.MBLID;
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }
        /// <summary>
        /// 打开下载
        /// </summary>
        public void OIDownLoad()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OIBusinessDownLoadWorkitem.Run();
            }
        }
        /// <summary>
        /// 弹出新增联系人列表(在关闭窗体后，会重新调用调用当前发送邮件的方法)
        /// </summary>
        /// <param name="parameter">沟通阶段</param>
        /// <param name="oceanBookingInfo">实体</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameterCollection">参数集合</param>
        public void ShowContacts(ContactStage parameter, OceanBusinessInfo oceanBookingInfo, string methodName, object[] parameterCollection)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //构造业务参数
                BusinessOperationContext businessOperation = new BusinessOperationContext
                {
                    OperationID = oceanBookingInfo.ID,
                    OperationType = OperationType.OceanImport,
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
        #endregion

        #region State Change
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="isCancel"></param>
        /// <param name="datetime"></param>
        /// <param name="editPartSaved"></param>
        public void CancelOIBusiness(Guid operationID, bool isCancel, DateTime? datetime, PartDelegate.EditPartSaved editPartSaved)
        {
            string message = string.Empty;
            if (isCancel)
            {
                message = LocalData.IsEnglish ? "Srue Cancel Current Business?" : "你真的要取消这笔业务吗?";
            }
            else
            {
                message = LocalData.IsEnglish ? "Srue Available Current Business?" : "你真的要恢复这笔业务吗?";
            }
            bool isOK = Utility.ShowResultMessage(message);

            if (!isOK) return;
            List<BillList> billData = FinanceService.GetBillListByOperactioID(operationID);
            if (billData != null && billData.Count > 0)
            {
                string strMessage = string.Empty;
                //包含代理账单不能取消业务
                if (billData.Select(billList => billList.Type == BillType.DC).Any())
                    strMessage = LocalData.IsEnglish ? "Cancelling the shipment is failed, please ask the Agent to remove it's D/C fees, and do the cancelling again after accepting the re-dispatched docs."
                        : "不能取消，请联系代理删除代理账单后重新分发，签收后再取消业务。";
                else//包含账单不能取消业务
                    strMessage = LocalData.IsEnglish ? "Cancelling the shipment is failed, please remove it's account info (Fees) first!"
                        : "不能取消，请先删除所有账单后再取消。";
                if (!string.IsNullOrEmpty(strMessage))
                {
                    MessageBoxService.ShowError(strMessage, LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }
            }
            SingleResult result = OceanImportService.CancelOIOrder(operationID, Guid.Empty, isCancel, LocalData.UserInfo.LoginID, datetime);
            if (editPartSaved != null)
            {
                DateTime? updatedate = result.GetValue<DateTime?>("UpdateDate");
                editPartSaved(new object[] { updatedate });
            }
        }
        /// <summary>
        /// 确认放货(单票)
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void ConfirmDelivery(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                string title = LocalData.IsEnglish ? "Release" : "放货";
                PartLoader.ShowEditPartInDialog<OIBusinessRelease>(RootWorkItem, businessList, title, editPartSaved);
            }
        }
        /// <summary>
        /// 确认放货(批量)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="editPartSaved"></param>
        public void ConfirmDelivery(OceanBusinessList list, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Release" : "放货";
                PartLoader.ShowEditPartInDialog<OIBusinessRelease>(RootWorkItem, list, title, editPartSaved);
            }
        }
        /// <summary>
        /// 取消确认放货(单票)
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void CancelDelivery(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                SingleResultData result = OceanImportService.ChangeOIOrderState(businessList.ID, OIOrderState.Checked, string.Empty, LocalData.UserInfo.LoginID, businessList.UpdateDate);
                if (result != null)
                {
                    OceanImportService.ChangeOITrackingInfo(businessList.ID, businessList.IsReceiveNotice, businessList.IsNoticeRelease, businessList.IsApplyRC, !businessList.IsReleaseCargo, businessList.IsNoticePay, businessList.IsAgreeRC, LocalData.UserInfo.LoginID);
                    if (editPartSaved != null)
                    {
                        BusinessOperationParameter business = new BusinessOperationParameter();
                        business.Context = new BusinessOperationContext { OperationID = operationid, OperationType = OperationType.OceanImport };
                        editPartSaved(new object[] { business });
                    }
                }

            }
        }
        /// <summary>
        /// 取消确认放货(批量)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="editPartSaved"></param>
        public void CancelDelivery(OceanBusinessList list, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SingleResultData result = OceanImportService.ChangeOIOrderState(list.ID, OIOrderState.Checked, string.Empty, LocalData.UserInfo.LoginID, list.UpdateDate);
                if (result != null)
                {
                    OceanImportService.ChangeOITrackingInfo(list.ID, list.IsReceiveNotice, list.IsNoticeRelease, list.IsApplyRC, !list.IsReleaseCargo, list.IsNoticePay, list.IsAgreeRC, LocalData.UserInfo.LoginID);
                    //if (editPartSaved != null)
                    //{
                    //    BusinessOperationParameter business = new BusinessOperationParameter();
                    //    business.Context = new ICP.DataCache.ServiceInterface.BusinessOperationContext { OperationID = list.ID, OperationType = OperationType.OceanImport };
                    //    editPartSaved(new object[] { business });
                    //}
                }
            }
        }
        /// <summary>
        /// 更改放货状态(单票-确认/取消)
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void OIDelivery(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);

                if (!businessList.IsReleaseCargo)
                {
                    try
                    {
                        ConfirmDelivery(businessList, AfterDelivery);
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Release Successfully" : "成功!");
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                    }
                }
                else
                {
                    string message = LocalData.IsEnglish ? "Srue Cancel Release ?" : "确认要取消放货?";
                    bool isOK = Utility.ShowResultMessage(message);
                    if (isOK)
                    {
                        try
                        {
                            CancelDelivery(businessList, AfterDelivery);
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Cancel Successfully" : "取消成功!");
                        }
                        catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex); }
                    }

                }

                if (editPartSaved != null)
                {
                    BusinessOperationParameter business = new BusinessOperationParameter();
                    business.Context = new BusinessOperationContext { OperationID = operationid, OperationType = OperationType.OceanImport };
                    editPartSaved(new object[] { business });
                }

            }
        }
        /// <summary>
        /// 更改放货状态(批量-确认/取消)
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void NewOiDelivery(List<Guid> operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            List<OceanBusinessList> listOceanBusiness = new List<OceanBusinessList>();
            if (operationid.Any())
            {
                listOceanBusiness = OceanImportService.GetBusinessListByIds(operationid.ToArray());
                //去除当前不需要放货的业务信息
                var release = listOceanBusiness.Where(n => n.IsReleaseCargo == false).ToList();
                if (release.Any())
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        string title = LocalData.IsEnglish ? "Release" : "放货";
                        PartLoader.ShowEditPartInDialog<NewOIBusinessRelease>(RootWorkItem, release, title, editPartSaved);
                    }
                }
                else
                {
                    //取消放货的操作
                    var cancelRelease = listOceanBusiness.Where(n => n.IsReleaseCargo == true).ToList();
                    string message = LocalData.IsEnglish ? "Srue Cancel Release ?" : "确认要取消放货?";
                    bool isOK = Utility.ShowResultMessage(message);
                    if (isOK)
                    {
                        try
                        {
                            using (new CursorHelper(Cursors.WaitCursor))
                            {
                                foreach (var item in cancelRelease)
                                {

                                    SingleResultData result = OceanImportService.ChangeOIOrderState(item.ID, OIOrderState.Checked, string.Empty, LocalData.UserInfo.LoginID, item.UpdateDate);
                                    if (result != null)
                                    {
                                        OceanImportService.ChangeOITrackingInfo(item.ID, item.IsReceiveNotice, item.IsNoticeRelease, item.IsApplyRC, !item.IsReleaseCargo, item.IsNoticePay, item.IsAgreeRC, LocalData.UserInfo.LoginID);
                                    }
                                }
                            }
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Cancel Successfully" : "取消成功!");
                        }
                        catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex); }
                    }
                }
            }
        }
        /// <summary>
        /// 放货后
        /// </summary>
        /// <param name="prams"></param>
        private void AfterDelivery(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }
            BusinessReleaseInfo releaseinfo = prams[0] as BusinessReleaseInfo;
            if (prams.Count() > 1)
            {
                OceanBusinessList businessList = prams[1] as OceanBusinessList;
                string message = string.Empty;
                bool isreleasecargo = !businessList.IsReleaseCargo;

                OceanBusinessList currentRow = businessList;
                currentRow.UpdateDate = releaseinfo.UpdateDate;

                if (businessList.State != OIOrderState.Release)
                {
                    currentRow.State = OIOrderState.Release;
                    currentRow.IsTelex = currentRow.ReleaseType == FCMReleaseType.Telex ? true : false;
                    currentRow.ReleaseType = releaseinfo.ReleaseType;
                    currentRow.ReleaseDate = releaseinfo.Releasedate;
                }
                else
                {
                    businessList.State = OIOrderState.Checked;
                    businessList.ReleaseDate = null;
                }

                OceanImportService.ChangeOITrackingInfo(businessList.ID, businessList.IsReceiveNotice, businessList.IsNoticeRelease, businessList.IsApplyRC, !businessList.IsReleaseCargo, currentRow.IsNoticePay, currentRow.IsAgreeRC, LocalData.UserInfo.LoginID);
            }




            //if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }
        /// <summary>
        /// 第三方代理放单--放单
        /// </summary>
        /// <param name="oibookingid">业务ID</param>
        /// <param name="rbld">放单</param>
        /// <param name="changebyid">放单人</param>
        /// <param name="editPartSaved"></param>
        public void ReleaseOIBL(Guid oibookingid, bool rbld, Guid changebyid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    OceanImportService.ReleaseOIBL(oibookingid, rbld, changebyid);
                    if (editPartSaved != null)
                    {
                        BusinessOperationParameter business = new BusinessOperationParameter();
                        business.Context = new BusinessOperationContext { OperationID = oibookingid, OperationType = OperationType.OceanImport };
                        editPartSaved(new object[] { business });
                    }
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "ReleaseBL Successfully" : "放单成功!");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex); }
            }
        }
        /// <summary>
        /// 收到MBL正本
        /// </summary>
        /// <param name="operationid"></param>
        public void OIOMBLRcved(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                string message = LocalData.IsEnglish ? "Mark 'RCVed MBL'?" : "标识'收到MBL正本'?";
                string cancelmessage = LocalData.IsEnglish ? "Un-Mark 'RCVed MBL'?" : "取消标识'收到MBL正本'?";
                bool isOK = Utility.ShowResultMessage(!businessList.IsOMBLRcved ? message : cancelmessage);
                if (!isOK)
                {
                    return;
                }
                Dictionary<string, object> stateValues = BuildStateCollection(businessList, "OMBLRcved");

                OceanImportService.ChangeOITrackingInfo(businessList.ID, stateValues, LocalData.UserInfo.LoginID);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "change '收到MBL正本' Successfully" : "'收到MBL正本'更新成功!");
            }
        }
        /// <summary>
        /// 海进关帐/取消关帐
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="editPartSaved">更新后执行事件</param>
        public void StateAccountingClose(Guid operationID, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationID);
                string message = LocalData.IsEnglish ? "Mark 'Accounting Close'?" : "标识'关帐'?";
                string cancelmessage = LocalData.IsEnglish ? "Un-Mark 'Accounting Close'?" : "取消标识'关帐'?";
                bool isOK = Utility.ShowResultMessage(!businessList.IsACCLOS ? message : cancelmessage);
                if (!isOK)
                {
                    return;
                }

                Dictionary<string, object> stateValues = BuildStateCollection(businessList, "ACCLOS");

                OceanImportService.ChangeOITrackingInfo(businessList.ID, stateValues, LocalData.UserInfo.LoginID);
                if (editPartSaved != null)
                {
                    BusinessOperationParameter business = new BusinessOperationParameter
                    {
                        Context =
                            new BusinessOperationContext
                            {
                                OperationID = operationID,
                                OperationType = OperationType.OceanImport
                            }
                    };
                    editPartSaved(new object[] { business });
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Change 'Accounting Close' Successfully" : "'关帐'状态更新成功!");
            }
        }
        /// <summary>
        /// 签收放单
        /// </summary>
        /// <param name="businesslist"></param>
        public void OIReceiveRN(OceanBusinessList businesslist, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bool receiveRN = !businesslist.IsReceiveNotice;
                if (!receiveRN)
                {
                    return;
                }
                OceanImportService.ChangeOITrackingInfo(businesslist.ID, receiveRN, businesslist.IsNoticeRelease, businesslist.IsApplyRC, businesslist.IsReleaseCargo, businesslist.IsNoticePay, businesslist.IsAgreeRC, LocalData.UserInfo.LoginID);
                if (editPartSaved != null)
                {
                    BusinessOperationParameter business = new BusinessOperationParameter();
                    business.Context = new BusinessOperationContext { OperationID = businesslist.ID, OperationType = OperationType.OceanImport };
                    editPartSaved(new object[] { business });
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Receive ReleaseNotice Successfully" : "成功接收放单通知!");
            }
        }
        /// <summary>
        /// 签收放单
        /// </summary>
        /// <param name="businesslist"></param>
        public void OIReceiveRN(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                bool receiveRN = !businessList.IsReceiveNotice;
                if (!receiveRN)
                {
                    return;
                }

                OceanImportService.ChangeOITrackingInfo(businessList.ID, receiveRN, businessList.IsNoticeRelease, businessList.IsApplyRC, businessList.IsReleaseCargo, businessList.IsNoticePay, businessList.IsAgreeRC, LocalData.UserInfo.LoginID);
                if (editPartSaved != null)
                {
                    BusinessOperationParameter business = new BusinessOperationParameter();
                    business.Context = new BusinessOperationContext { OperationID = businessList.ID, OperationType = OperationType.OceanImport };
                    editPartSaved(new object[] { business });
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Receive ReleaseNotice Successfully" : "成功接收放单通知!");
            }


        }
        /// <summary>
        /// 申请放单
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void ApplyRelease(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                string successful = LocalData.IsEnglish ? "Apply ReleaseRC Successfully" : "成功申请放货!";
                string cancel = LocalData.IsEnglish ? "Cancel ReleaseRC Successfully" : "成功取消申请放货!";
                string strtip = businessList.IsApplyRC == false ? successful : cancel;
                OceanImportService.ChangeOITrackingInfo(businessList.ID, businessList.IsReceiveNotice, businessList.IsNoticeRelease, !businessList.IsApplyRC, businessList.IsReleaseCargo, businessList.IsNoticePay, businessList.IsAgreeRC, LocalData.UserInfo.LoginID);
                if (editPartSaved != null)
                {
                    BusinessOperationParameter business = new BusinessOperationParameter();
                    business.Context = new BusinessOperationContext { OperationID = businessList.ID, OperationType = OperationType.OceanImport };
                    editPartSaved(new object[] { business });
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, strtip);
            }
        }
        /// <summary>
        /// 同意放货
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void AgreeRC(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string message = string.Empty;
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                if (businessList.IsReleaseCargo)
                {
                    message = LocalData.IsEnglish ? "The shipment could not be marked as 'ARC' because it's arelady RC(Released cargo)."
                        : "此业务已经放货，不能取消同意放货。";
                    MessageBoxService.ShowWarning(message, LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }
                message = LocalData.IsEnglish ? "Mark 'AgreeRC'?" : "标识'同意放货'?";
                string cancelmessage = LocalData.IsEnglish ? "Un-Mark 'AgreeRC'?" : "取消标识'同意放货'?";
                bool isOK = Utility.ShowResultMessage(!businessList.IsAgreeRC ? message : cancelmessage);
                if (!isOK)
                {
                    return;
                }

                OceanImportService.ChangeOITrackingInfo(businessList.ID, businessList.IsReceiveNotice, businessList.IsNoticeRelease, businessList.IsApplyRC, businessList.IsReleaseCargo, businessList.IsNoticePay, !businessList.IsAgreeRC, LocalData.UserInfo.LoginID);
                if (editPartSaved != null)
                {
                    BusinessOperationParameter business = new BusinessOperationParameter();
                    business.Context = new BusinessOperationContext { OperationID = businessList.ID, OperationType = OperationType.OceanImport };
                    editPartSaved(new object[] { business });
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "change 'AgreeRC' Successfully" : "'同意放货'更新成功!");
            }
        }
        /// <summary>
        /// 财务寄出MBL正本
        /// </summary>
        /// <param name="operationid"></param>
        public void MailDMBL(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                string message = LocalData.IsEnglish ? "Mark 'MAILD.MBL'?" : "标识'财务寄出MBL正本'?";
                string cancelmessage = LocalData.IsEnglish ? "Un-Mark 'MAILD.MBL'?" : "取消标识'财务寄出MBL正本'?";
                bool isOK = Utility.ShowResultMessage(!businessList.IsMailDMBL ? message : cancelmessage);
                if (!isOK)
                {
                    return;
                }
                Dictionary<string, object> stateValues = BuildStateCollection(businessList, "MAILDMBL");

                OceanImportService.ChangeOITrackingInfo(businessList.ID, stateValues, LocalData.UserInfo.LoginID);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "change '财务寄出MBL正本' Successfully" : "'财务寄出MBL正本'更新成功!");
            }
        }
        /// <summary>
        /// 构建状态集合
        /// </summary>
        /// <param name="businessList">业务对象</param>
        /// <param name="csField">改变状态的字段</param>
        /// <returns></returns>
        private Dictionary<string, object> BuildStateCollection(OceanBusinessList businessList, string csField)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
                {
                    {"RBLRCV", businessList.IsReceiveNotice},
                    {"URB", businessList.IsNoticeRelease},
                    {"UDN", businessList.IsNoticePay},
                    {"BLRCA", businessList.IsApplyRC},
                    {"BLRC", businessList.IsReleaseCargo},
                    {"AGREERC", businessList.IsAgreeRC},
                    {"OMBLRCVED", businessList.IsOMBLRcved},
                    {"MAILDMBL", businessList.IsMailDMBL},
                    {"ACCLOS", businessList.IsACCLOS}
                };
            values[csField] = !(bool)values[csField];
            return values;
        }
        #endregion

        #region Work Flow
        /// <summary>
        /// 放货异常
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationNo"></param>
        public void ExceptionReleaseRC(Guid operationID, string operationNo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                BusinessList business = FinanceService.GetBusinessListByIDs(new Guid[] { operationID }).GetList<BusinessList>()[0];

                WorkflowClientService.StartExceptionRealeaseRCWorkFlow(
                    LocalData.UserInfo.LoginID,
                    business.CompanyID,
                    LocalData.IsEnglish ? "No" : "单号:" + business.OperationNO,
                    LocalData.IsEnglish ? "Exception Realease RC" : "异常放货申请",
                    operationNo,
                    business.CompanyID,
                    operationID,
                    operationNo,
                    business.SalesName != null ? business.SalesName : string.Empty,
                    EnumHelper.GetDescription<OperationType>(OperationType.OceanImport, LocalData.IsEnglish),
                    business.ARDescription,
                    business.APDescription,
                    business.ProfitDescription,
                    LocalData.UserInfo.UserName,
                    business.CustomerID,
                    business.CustomerName,
                    string.Empty);
            }
        }
        #endregion

        #region Mail
        /// <summary>
        /// 催港前放单(发送邮件给港前放单人员和港前客服)
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="editPartSaved"></param>
        public void NoticeRelease(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("RBLD request :   Reference No# ");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    body.Append("Dear Agent,<br/><br />");
                    body.Append("Subject shipment will arrive within 5 days and we found HBL still not yet released.  <br/>");
                    body.Append("1.Please check and advise the reason to hold HBL<br />");
                    body.Append("2.Please have your decision if still to hold or release ?<br />");
                    body.Append("3.If any charge happened due to late release of HBL, please inform if we should collect all payment from consignee before release of the shipment?<br />");
                    body.Append("4.Please do allow us at least 24-48 hours to arrange telex release with carrier as well.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    EventCode eventCode = EventCodeList("RBLNt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //ConfigureInfo config = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);

                    try
                    {
                        //获取人员信息
                        UserDetailInfo polFiler = ServiceClient.GetService<IUserService>()
                                                              .GetUserDetailInfo(new Guid(oceanBusinessInfo.POLFilerID.ToString()));

                        List<OrganizationList> companylist = UserService.GetUserCompanyList((Guid)oceanBusinessInfo.POLFilerID, OrganizationType.Company);
                        OrganizationList company = companylist.Find(r => r.IsDefault);
                        ConfigureInfo config = ConfigureService.GetCompanyConfigureInfo(company.ID, LocalData.IsEnglish);

                        //获取当前登录人员上级人员信息
                        List<UserDetailInfo> polFilersuperior = ServiceClient.GetService<IUserService>()
                                                               .GetSuperior(new Guid(oceanBusinessInfo.POLFilerID.ToString()));


                        //获取当前登录人员上级人员信息
                        UserDetailInfo Sales = ServiceClient.GetService<IUserService>()
                                                               .GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));


                        string superiorEMail = string.Empty;
                        foreach (var s in polFilersuperior)
                        {
                            superiorEMail += s.EMail + ";";
                        }

                        //获取业务员信息
                        UserDetailInfo sales = ServiceClient.GetService<IUserService>().GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));
                        var message = CreateMessageInfo(MessageType.Email,
                            MessageWay.Send, polFiler.EMail + ";" + Sales.EMail + (string.IsNullOrEmpty(config.ReleaserEmail) ? "" : ";" + config.ReleaserEmail), LocalData.UserInfo.EmailAddress,
                                                    FormType.Booking, OperationType.OceanImport,
                                                    oceanBusinessInfo.ID, Guid.Empty,
                                                    body.ToString(), subject.ToString(),
                                                    superiorEMail.TrimEnd(';'),
                                                    eventCode.Code, eventObjects);
                        message.BodyFormat = BodyFormat.olFormatHTML;
                        message.State = MessageState.Success;
                        MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);

                        List<BusinessSaveParameter> listBusinessParameter = new List<BusinessSaveParameter>();
                        BusinessSaveParameter parameter = new BusinessSaveParameter();
                        parameter["OceanBookingID"] = oceanBusinessInfo.ID;

                        parameter["OperationType"] = (int)OperationType.OceanImport;
                        parameter[eventCode.Code] = 3;
                        listBusinessParameter.Add(parameter);
                        ServiceClient.GetService<IBusinessQueryService>().Save(listBusinessParameter);
                    }
                    catch(Exception ex) 
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                    }
                }
            }
        }
        /// <summary>
        /// 发送邮件给港前业务账单的修改人。告知账单已被港后修改。
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="OEBookingID"></param>
        /// <returns></returns>
        public string MailApplyBillRevise(bool isEnglish, Guid OEBookingID)
        {

            OceanBookingInfo oceanBookingInfo = OceanExportService.GetOceanBookingInfo(OEBookingID);
            var promptCh = new StringBuilder();
            var promptEn = new StringBuilder();
            oceanBookingInfo.POLCheck = true;
            oceanBookingInfo.PODCheck = true;
            oceanBookingInfo.ContainerCheck = true;

            //读取当前用户的邮箱地址信息
            //string mailInformation = MailInformation(oceanBookingInfo.ID, "SI");

            string sendFrom = LocalData.UserInfo.EmailAddress;
            //string[] strSpit = null;
            //if (!string.IsNullOrEmpty(mailInformation))
            //{
            //    strSpit = mailInformation.Split('|');
            //}
            //else
            //{
            //    promptCh.Append(" 补料联系人为空，无法发送邮件，请添加补料联系人. ");
            //    promptEn.Append(" Supplementary food contact for air, unable to send mail, please add your contact partner. ");
            //}

            string cc = UserService.GetUserInfo((Guid)oceanBookingInfo.BookingerID).EMail;
            string sendTo = OceanImportService.GetBillUpdateUserEmails(oceanBookingInfo.ID);


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
            else
            {
                // 邮件发送的消息实体
                //if (strSpit != null)
                //{
                var eventObjects = new EventObjects
                {
                    OperationID = oceanBookingInfo.ID,
                    OperationType = OperationType.OceanExport,
                    EventID = new Guid("C352DE0E-4CA4-4682-AC63-4DED8FE67167"),
                    Id = Guid.Empty,
                    FormID = oceanBookingInfo.ID,
                    FormType = FormType.Unknown,
                    IsShowAgent = true,
                    IsShowCustomer = true,
                    Subject = "Email",
                    Description = "Notified the customer for the Bills are Revised.",
                    Priority = MemoPriority.Normal,
                    Type = MemoType.EmailLog,
                    UpdateDate = DateTime.Now,
                    UpdateBy = LocalData.UserInfo.LoginID
                };
                oceanBookingInfo.UpdateByName = LocalData.UserInfo.LoginName;
                oceanBookingInfo.UpdateDate = DateTime.Now;
                var message = CreateMessageInfo(MessageType.Email,
                                                               MessageWay.Send, sendTo,
                                                               sendFrom,
                                                               FormType.Booking, OperationType.OceanExport,
                                                               oceanBookingInfo.ID, oceanBookingInfo.ID,
                                                               string.Empty, string.Empty, cc,
                                                               string.Empty, eventObjects);
                //传输邮件模版的字符数组
                object[] values = { oceanBookingInfo, sendFrom };
                MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, "ConfirApplyBillRevise", values);
                //}

            }
            return string.Empty;
        }
        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="message"></param>
        public void MailOpenDeliveryNotice(Guid businessID, Message.ServiceInterface.Message message)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(businessID);
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanBusinessList", businessList);
                stateValues.Add("Message", message);
                string no = Utility.GetLineNo(businessList.No);
                string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";
                PartLoader.ShowEditPart<OIBusinessTruckEdit>(RootWorkItem, businessID, EditMode.Edit, stateValues, title, null, title);
            }
        }
        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        /// <returns></returns>
        public void MailPickUpToCustomer(Guid operationid, bool isEnglish)
        {
            MailPickUpToCustomer(operationid, string.Empty, isEnglish);
        }
        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="isEnglish">发送邮件版本</param>
        /// <returns></returns>
        public void MailPickUpToCustomer(Guid operationid, string mailInformation, bool isEnglish)
        {
            #region  构造邮件使用的类
            MailOIBusinessDataObjects mailOiBusinessData = new MailOIBusinessDataObjects();
            OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
            string Message = ShowMessage(oceanBusinessInfo);
            if (!string.IsNullOrEmpty(Message))
            {
                XtraMessageBox.Show(Message);
                return;
            }
            mailOiBusinessData.ETA = oceanBusinessInfo.ETA;
            mailOiBusinessData.POLName = oceanBusinessInfo.POLName;
            mailOiBusinessData.PODName = oceanBusinessInfo.PODName;
            mailOiBusinessData.ContainerDescription = oceanBusinessInfo.ContainerDescription;
            mailOiBusinessData.CustomerName = oceanBusinessInfo.CustomerName;
            mailOiBusinessData.No = oceanBusinessInfo.No;

            string NO = string.Empty;
            if (oceanBusinessInfo.ContainerList != null)
            {
                foreach (var con in oceanBusinessInfo.ContainerList)
                {
                    NO += con.No + ",";
                }
            }
            mailOiBusinessData.LFDate = oceanBusinessInfo.ContainerList.Min(n => n.LFDate);
            mailOiBusinessData.ContainerNo = NO.TrimEnd(',');
            string HBLNo = string.Empty;
            if (oceanBusinessInfo.HBLList != null)
            {
                foreach (var con in oceanBusinessInfo.HBLList)
                {
                    HBLNo += con.HBLNo + ",";
                }
            }
            mailOiBusinessData.HBLNo = HBLNo.TrimEnd(',');
            #endregion
            UserInfo userInfo = null;
            if (oceanBusinessInfo.SalesID != null)
            {
                userInfo = UserService.GetUserInfo((Guid)oceanBusinessInfo.SalesID);
            }
            //string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
            string trKmailInformation = string.IsNullOrEmpty(mailInformation) ? MailInformation(oceanBusinessInfo.ID, "TRK", null, false, false) : mailInformation;
            //string[] ANSpit = null;
            string[] TRKSpits = null;
            //if (!string.IsNullOrEmpty(aNmailInformation))
            //{
            //    ANSpit = aNmailInformation.Split('|');
            //}
            if (!string.IsNullOrEmpty(trKmailInformation))
            {
                TRKSpits = trKmailInformation.Split('|');
            }
            else
            {
                TRKSpits = new string[] { " ", " ", " " };
            }
            EventCode eventCode = EventCodeList("DOS");
            var eventObjects = new EventObjects
            {
                OperationID = oceanBusinessInfo.ID,
                OperationType = OperationType.OceanImport,
                FormID = oceanBusinessInfo.ID,
                FormType = FormType.Unknown,
                Code = eventCode.Code,
                Description = eventCode.Subject,
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
            var message = CreateMessageInfo(MessageType.Email,
                                            MessageWay.Send, TRKSpits[0], LocalData.UserInfo.EmailAddress,
                                            FormType.Booking, OperationType.OceanImport,
                                            oceanBusinessInfo.ID, Guid.Empty,
                                            string.Empty, string.Empty, userInfo == null ? string.Empty : userInfo.EMail, eventCode.Code, eventObjects);
            object[] values = { mailOiBusinessData, TRKSpits[1] };
            message = MainCenterEmailTemplateGetter.ReturnMessage(message, isEnglish, "MailPickUpToCustomer", values);
            message.BodyFormat = BodyFormat.olFormatHTML;
            MailOpenDeliveryNotice(operationid, message);
        }
        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        public void MailAnToCustomer(Guid operationid, bool isEnglish)
        {
            MailAnToCustomer(operationid, string.Empty, isEnglish);
        }
        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="isEnglish">发送邮件版本</param>
        public void MailAnToCustomer(Guid operationid, string mailInformation, bool isEnglish)
        {
            #region  构造邮件使用的类
            MailOIBusinessDataObjects mailOiBusinessData = new MailOIBusinessDataObjects();
            OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
            string Message = ShowMessage(oceanBusinessInfo);
            if (!string.IsNullOrEmpty(Message))
            {
                XtraMessageBox.Show(Message);
                return;
            }
            mailOiBusinessData.ETA = oceanBusinessInfo.ETA;
            mailOiBusinessData.POLName = oceanBusinessInfo.POLName;
            mailOiBusinessData.PODName = oceanBusinessInfo.PODName;
            mailOiBusinessData.ContainerDescription = oceanBusinessInfo.ContainerDescription;
            mailOiBusinessData.CustomerName = oceanBusinessInfo.CustomerName;
            mailOiBusinessData.No = oceanBusinessInfo.No;
            if (string.IsNullOrEmpty(oceanBusinessInfo.SalesID.ToString()))
            {
                if (MessageBoxService.ShowQuestion(
                    LocalData.IsEnglish
                        ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?"
                        : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?"
                    , LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?" : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?",
                //                       LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //{
                //    return;
                //}
            }

            string no = string.Empty;
            if (oceanBusinessInfo.ContainerList != null)
            {
                foreach (var con in oceanBusinessInfo.ContainerList)
                {
                    no += con.No + ",";
                }
            }
            mailOiBusinessData.LFDate = oceanBusinessInfo.ContainerList.Min(n => n.LFDate);
            mailOiBusinessData.ContainerNo = no.TrimEnd(',');
            string HBLNo = string.Empty;
            if (oceanBusinessInfo.HBLList != null)
            {
                foreach (var con in oceanBusinessInfo.HBLList)
                {
                    HBLNo += con.HBLNo + ",";
                }
            }
            mailOiBusinessData.HBLNo = HBLNo.TrimEnd(',');
            #endregion

            UserInfo userInfo = null;
            string aNmailInformation = string.IsNullOrEmpty(mailInformation) ? MailInformation(oceanBusinessInfo.ID, "AN", null, false, false) : mailInformation;
            if (oceanBusinessInfo.SalesID != null)
            {
                userInfo = UserService.GetUserInfo((Guid)oceanBusinessInfo.SalesID);
            }
            string[] ANSpit = null;
            if (!string.IsNullOrEmpty(aNmailInformation))
            {
                ANSpit = aNmailInformation.Split('|');
            }
            else
            {
                ANSpit = new string[] { " ", " ", " " };
            }

            StringBuilder body = new StringBuilder();
            StringBuilder subject = new StringBuilder();
            string Containerno = string.Empty;
            if (oceanBusinessInfo.ContainerList.Count() > 3)
            {
                for (int i = 0; i < oceanBusinessInfo.ContainerList.Count(); i++)
                {
                    if (i < 2)
                    {
                        if (string.IsNullOrEmpty(Containerno))
                        {
                            Containerno = oceanBusinessInfo.ContainerList[i].No;
                        }
                        else
                        {
                            Containerno = Containerno + ";" + oceanBusinessInfo.ContainerList[i].No;
                        }
                    }
                }
                Containerno = Containerno + "...";
            }
            else
            {
                foreach (var item in oceanBusinessInfo.ContainerList)
                {
                    if (string.IsNullOrEmpty(Containerno))
                    {
                        Containerno = item.No;
                    }
                    else
                    {
                        Containerno = Containerno + ";" + item.No;
                    }
                }
            }



            if (isEnglish == false)
            {
                //主题
                subject.Append("到货通知：参考No#");
                subject.Append(oceanBusinessInfo.No + "/");
                subject.Append("HBL#");

                subject.Append(mailOiBusinessData.HBLNo + "/");

                string strEta = string.Empty;
                if (oceanBusinessInfo.ETA != null)
                {
                    DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                    strEta = eta.ToString("yyyy-MM-dd");
                    subject.Append(strEta + "/");
                }
                else
                {
                    subject.Append("/");
                }

                subject.Append(oceanBusinessInfo.ContainerDescription + "/");



                subject.Append(Containerno + "/");

                subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                body.Append("亲爱的客户， <br/><br />");
                body.Append("附件为您的到货通知  HBL#" + mailOiBusinessData.HBLNo + ", ETA:" + oceanBusinessInfo.DETA.ToString().Replace("0:00:00", string.Empty) + "<br/><br />");
                body.Append("如有任何问题,请联系我司客服人员. <br />");
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");
            }
            else
            {
                //主题
                subject.Append("ARRIVAL NOTICE:  Reference No#");
                subject.Append(oceanBusinessInfo.No + "/");
                subject.Append("HBL#");

                subject.Append(mailOiBusinessData.HBLNo + "/");

                string strEta = string.Empty;
                if (oceanBusinessInfo.ETA != null)
                {
                    DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                    strEta = eta.ToString("yyyy-MM-dd");
                    subject.Append(strEta + "/");
                }
                else
                {
                    subject.Append("/");
                }

                subject.Append(oceanBusinessInfo.ContainerDescription + "/");



                subject.Append(Containerno + "/");

                subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                body.Append("Dear valued customer, <br/><br />");
                body.Append("Thank you for shipping with us. <br/><br />");
                body.Append("Attached please check the Arrival notice for your shipment HBL#" + mailOiBusinessData.HBLNo + ", ETA:" + oceanBusinessInfo.DETA.ToString().Replace("0:00:00", string.Empty) + "<br/><br />");
                body.Append("Please contact our customer service next if you have any question. <br />");
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");
            }
            EventCode eventCode = EventCodeList("ANSC");
            var eventObjects = new EventObjects
            {
                OperationID = oceanBusinessInfo.ID,
                OperationType = OperationType.OceanImport,
                FormID = oceanBusinessInfo.ID,
                FormType = FormType.Unknown,
                Code = eventCode.Code,
                Description = eventCode.Subject,
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
            var message = CreateMessageInfo(MessageType.Email,
                                          MessageWay.Send, ANSpit[0], LocalData.UserInfo.EmailAddress,
                                          FormType.Booking, OperationType.OceanImport,
                                          oceanBusinessInfo.ID, Guid.Empty,
                                          body.ToString(), subject.ToString(),
                                          userInfo == null ? string.Empty : userInfo.EMail,
                                          eventCode.Code, eventObjects);

            message.BodyFormat = BodyFormat.olFormatHTML;

            MailPrintArrivalNotice(operationid, message);
        }
        /// <summary>
        /// 根据业务ID打印到港通知书
        /// </summary>
        /// <param name="operationid"></param>
        public void MailPrintArrivalNotice(Guid operationid, Message.ServiceInterface.Message message)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessInfo BusinessInfo = OceanImportService.GetBusinessInfo(operationid);

                if (BusinessInfo.CompanyID == new Guid("0501D29D-0EFE-E111-B376-0026551CA87B")) //巴西到港通知
                {
                    Message.ServiceInterface.Message operationInfo = GetOperationInfo(operationid);
                    OceanImportPrintHelper.PrintArrivalNoticeReportForBrazil(BusinessInfo, operationInfo);

                }
                else
                {
                    Dictionary<string, object> stateValues = new Dictionary<string, object>();
                    stateValues.Add("OceanBusinessList", BusinessInfo);
                    stateValues.Add("Message", message);
                    string no = Utility.GetLineNo(BusinessInfo.No);
                    string title = (LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书") + ("-" + no);
                    PartLoader.ShowEditPart<OIArrivalNotice2>(RootWorkItem, BusinessInfo.CompanyID, stateValues, title, null, null);
                }
            }
        }
        /// <summary>
        /// 催代理分发文件信息
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">中英文版本</param>
        /// <param name="editPartSaved"></param>
        public void MailOverseaAgent(Guid operationid, bool isEnglish, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    //获取人员信息
                    UserDetailInfo userdata = ServiceClient.GetService<IUserService>()
                        .GetUserDetailInfo(new Guid(oceanBusinessInfo.POLFilerID.ToString()));

                    //获取当前登录人员上级人员信息
                    List<UserDetailInfo> superior = ServiceClient
                        .GetService<IUserService>()
                        .GetSuperior(new Guid(oceanBusinessInfo.POLFilerID.ToString()));

                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);
                    if (isEnglish == false)
                    {
                        //主题
                        subject.Append("请分发文件：No#");
                        subject.Append(oceanBusinessInfo.No + "/");
                        subject.Append("HBL#");
                        subject.Append(hblNo + "/");
                        string strEta = string.Empty;
                        if (oceanBusinessInfo.ETA != null)
                        {
                            DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                            strEta = eta.ToString("yyyy-MM-dd");
                            subject.Append(strEta + "/");
                        }
                        else
                        {
                            subject.Append("/");
                        }
                        subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                        subject.Append(containerno + "/");
                        subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                        body.Append("亲爱的代理， <br/><br />");
                        body.Append("记录显示货物很快就到了，但我们尚未收到的为任何文件 HBL#" + hblNo + ",<br/><br />");
                        body.Append("请分发文件尽可能避免任何延误。 <br />");
                        body.Append("</div>");
                        body.Append("</body>");
                        body.Append("</html>");
                    }
                    else
                    {
                        //主题
                        subject.Append("Documentation request: Reference No#");
                        subject.Append(oceanBusinessInfo.No + "/");
                        subject.Append("HBL#");
                        subject.Append(hblNo + "/");
                        string strEta = string.Empty;
                        if (oceanBusinessInfo.ETA != null)
                        {
                            DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                            strEta = eta.ToString("yyyy-MM-dd");
                            subject.Append(strEta + "/");
                        }
                        else
                        {
                            subject.Append("/");
                        }
                        subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                        subject.Append(containerno + "/");
                        subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                        body.Append("Dear agent,  <br/><br />");
                        body.Append("Our record shows that subject shipment will arrive soon, but we have not yet received docs for the subject shipment HBL#" + hblNo + ",<br/><br />");
                        body.Append("please email & dispatch all the docs as soon as possible to avoid any delay.  <br />");
                        body.Append("</div>");
                        body.Append("</body>");
                        body.Append("</html>");
                    }

                    string superiorEMail = string.Empty;
                    foreach (var s in superior)
                    {
                        superiorEMail += s.EMail + ";";
                    }

                    EventCode eventCode = EventCodeList("DOCNt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    var message = CreateMessageInfo(MessageType.Email,
                        MessageWay.Send, userdata.EMail, LocalData.UserInfo.EmailAddress,
                        FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty,
                        body.ToString(), subject.ToString(),
                        superiorEMail.TrimEnd(';'),
                        string.Empty, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, isEnglish, string.Empty, null);
                }
            }
        }
        /// <summary>
        ///  ETA 前5天催清关文件
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void MailCustomrequest(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("CUSTOM DOCUMENTATION REQUEST: Reference No#");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    body.Append("Dear Valued customer ,<br/><br />");
                    body.Append("Our record show that you will need us to arrange the custom clearance. <br/>");
                    body.Append("If so, please submit all the custom entry documentations as soon as possible.<br />");
                    body.Append("We will not be responsible for the extra charges due to late submit of documentation.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
                    string[] anSpit = null;
                    anSpit = !string.IsNullOrEmpty(aNmailInformation)
                        ? aNmailInformation.Split('|')
                        : new string[] { " ", " ", " " };

                    EventCode eventCode = EventCodeList("CCNt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //获取业务员信息
                    UserDetailInfo sales =
                        ServiceClient.GetService<IUserService>()
                            .GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));
                    var message = CreateMessageInfo(MessageType.Email,
                        MessageWay.Send, anSpit[0], LocalData.UserInfo.EmailAddress,
                        FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty,
                        body.ToString(), subject.ToString(),
                        sales.EMail, eventCode.Code, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);
                }
            }
        }
        /// <summary>
        ///  ETA 变更通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void MailEtachange(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("ETA CHANGE:  Reference No#");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    body.Append("Dear Valued customer ,<br/><br />");
                    body.Append("Please be advised that ETA for the subject shipment changed to" + strEta + " <br/>");
                    body.Append("Any question, please feel free to contact me.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
                    string[] anSpit = null;
                    anSpit = !string.IsNullOrEmpty(aNmailInformation)
                        ? aNmailInformation.Split('|')
                        : new string[] { " ", " ", " " };

                    EventCode eventCode = EventCodeList("ETANt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //获取业务员信息
                    UserDetailInfo sales = ServiceClient.GetService<IUserService>().GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));

                    var message = CreateMessageInfo(MessageType.Email, MessageWay.Send, anSpit[0],
                        LocalData.UserInfo.EmailAddress, FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty, body.ToString(), subject.ToString(), sales.EMail, string.Empty, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);
                }
            }
        }
        /// <summary>
        /// FULLY RELEASE 之后柜子AVAILABILITY通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void MailContaineravailableforpickup(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bool ccflg = false;
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("CONTAINER AVAILABLE FOR PICK UP: Reference No#");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    if (oceanBusinessInfo.TransportClauseName.LastIndexOf("CY") > 0)
                    {
                        ccflg = true;
                        body.Append("Dear Valued customer,<br/><br />");
                    }
                    else if (oceanBusinessInfo.TransportClauseName.LastIndexOf("DOOR") > 0)
                    {
                        body.Append("Dear Valued Trucker,<br/><br />");
                    }
                    body.Append("Please be advised that containers for subject shipment have been fully released and are now available for pick up. <br/>");
                    body.Append("Please arrange pick up asap to avoid potential charges.<br />");
                    body.Append("Any question, please feel free to contact me.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    //获取客户的邮件地址
                    string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
                    string[] anSpit = null;
                    anSpit = !string.IsNullOrEmpty(aNmailInformation)
                        ? aNmailInformation.Split('|')
                        : new string[] { " ", " ", " " };

                    //获取拖车行的邮件地址
                    string truckCustomersEmail = OceanImportService.GetTruckCustomersEmail(oceanBusinessInfo.ID);

                    //创建事件 
                    EventCode eventCode = EventCodeList("FCPANt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //获取业务员信息
                    UserDetailInfo sales = ServiceClient.GetService<IUserService>().GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));

                    var message = CreateMessageInfo(MessageType.Email, MessageWay.Send, ccflg ? anSpit[0] : truckCustomersEmail,
                        LocalData.UserInfo.EmailAddress, FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty, body.ToString(), subject.ToString(), ccflg ? sales.EMail : string.Empty,
                        string.Empty, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);

                }
            }
        }

        /// <summary>
        /// POD提醒
        /// </summary>
        /// <param name="operationid"></param>
        public void NoticePod(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                if (oceanBusinessInfo == null)
                {
                    return;
                }

                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show(Message);
                    return;
                }
                try
                {
                    if (oceanBusinessInfo != null)
                    {
                        StringBuilder body = new StringBuilder();
                        StringBuilder subject = new StringBuilder();
                        string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                        string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                        //主题
                        subject.Append("Shipment finished : Reference No#");
                        subject.Append(oceanBusinessInfo.No + "/");
                        subject.Append("HBL#");
                        subject.Append(hblNo + "/");
                        subject.Append("Cntr#");
                        subject.Append(containerno + "/");

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
                        body.Append("Dear All,<br/><br />");
                        body.Append("Consignee just received this shipments. <br/>");
                        body.Append("</div>");
                        body.Append("</body>");
                        body.Append("</html>");

                        ConfigureInfo config = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                        string messto = string.Empty;

                        //港前客服
                        UserDetailInfo polFiler = ServiceClient.GetService<IUserService>()
                                                              .GetUserDetailInfo(new Guid(oceanBusinessInfo.POLFilerID.ToString()));
                        //业务员
                        UserDetailInfo Sales = ServiceClient.GetService<IUserService>()
                                                              .GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));
                        if (oceanBusinessInfo.OverSeasFilerId != null)
                        {
                            //总客服
                            UserDetailInfo OverSeasFilerId = ServiceClient.GetService<IUserService>()
                                                                   .GetUserDetailInfo(new Guid(oceanBusinessInfo.OverSeasFilerId.ToString()));
                            if (OverSeasFilerId != null)
                            {
                                messto = polFiler.EMail + ";" + Sales.EMail + ";" + OverSeasFilerId.EMail;
                            }
                        }
                        else
                        {
                            messto = polFiler.EMail + ";" + Sales.EMail;
                        }

                        var message = CreateMessageInfo(MessageType.Email,
                            MessageWay.Send, LocalData.UserInfo.EmailAddress, LocalData.UserInfo.EmailAddress,
                                                    FormType.Booking, OperationType.OceanImport,
                                                    oceanBusinessInfo.ID, Guid.Empty,
                                                    body.ToString(), subject.ToString(),
                                                    LocalData.UserInfo.EmailAddress, null, null);
                        message.BodyFormat = BodyFormat.olFormatHTML;
                        message.State = MessageState.Success;
                        MessageService.Send(message);

                        body = new StringBuilder();
                        subject = new StringBuilder();

                        //主题
                        subject.Append("Must Imput AR/AP/DN completely in 5 working days : Reference No#");
                        subject.Append(oceanBusinessInfo.No + "/");
                        subject.Append("HBL#");
                        subject.Append(hblNo + "/");
                        subject.Append("Cntr#");
                        subject.Append(containerno + "/");

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
                        body.Append("Dear All,<br/><br />");
                        body.Append("You must imput AR/AP/DN with 5 working days completely <br/>");
                        body.Append("</div>");
                        body.Append("</body>");
                        body.Append("</html>");

                        //客服
                        UserDetailInfo CustomerService = ServiceClient.GetService<IUserService>()
                                                              .GetUserDetailInfo(new Guid(oceanBusinessInfo.CustomerService.ToString()));
                        if (CustomerService == null || string.IsNullOrEmpty(CustomerService.EMail))
                        {
                            //DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The business does not have a customer service or customer service does not have an email address" : "该业务没有客服或客服没有配置邮箱地址！");
                            return;
                        }


                        message = CreateMessageInfo(MessageType.Email,
                            MessageWay.Send, LocalData.UserInfo.EmailAddress, LocalData.UserInfo.EmailAddress,
                                                    FormType.Booking, OperationType.OceanImport,
                                                    oceanBusinessInfo.ID, Guid.Empty,
                                                    body.ToString(), subject.ToString(),
                                                    LocalData.UserInfo.EmailAddress, null, null);
                        message.BodyFormat = BodyFormat.olFormatHTML;
                        message.State = MessageState.Success;
                        MessageService.Send(message);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// POD提醒
        /// </summary>
        /// <param name="operationid"></param>
        public void NoticeEmptyReport(Guid OperationID, string ContainerNO, string messto)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();

                    //主题
                    subject.Append("Container No# ");
                    subject.Append(ContainerNO);
                    subject.Append(" is empty now.");

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
                    body.Append("Dear,<br/><br />");
                    body.Append("The container : " + ContainerNO + " has been empty. Please pick up the empty ASAP. <br/>");
                    body.Append("Empty report Time: " + DateTime.Now.ToString() + " <br/>");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    var message = CreateMessageInfo(MessageType.Email,
                        MessageWay.Send, messto, LocalData.UserInfo.EmailAddress,
                                                FormType.Booking, OperationType.OceanImport,
                                                OperationID, Guid.Empty,
                                                body.ToString(), subject.ToString(),
                                                LocalData.UserInfo.EmailAddress, null, null);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// LFD 通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="lastFreeDate"></param>
        /// <param name="editPartSaved"></param>
        /// <param name="content">调用方法已经定义好的内容信息</param>
        public void MailLfdnotice(Guid operationid, string lastFreeDate, PartDelegate.EditPartSaved editPartSaved, string content)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bool ccflg = false;
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("LFD NOTICE:  Reference Reference No# ");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    if (oceanBusinessInfo.TransportClauseName.LastIndexOf("CY") > 0)
                    {
                        ccflg = true;
                        body.Append("Dear Valued customer,<br/><br />");
                    }
                    else if (oceanBusinessInfo.TransportClauseName.LastIndexOf("DOOR") > 0)
                    {
                        body.Append("Dear Valued Trucker,<br/><br />");
                    }
                    body.Append("Please check below LFD for the container ( containers ). <br/>");
                    if (string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(lastFreeDate))
                    {
                        body.Append(containerno + ":" + lastFreeDate + "<br />");
                    }
                    if (!string.IsNullOrEmpty(content))
                    {
                        body.Append(content);
                    }
                    else
                    {
                        return;
                    }
                    body.Append("Any question, please feel free to contact me.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    //获取客户的邮件地址
                    string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
                    string[] anSpit = null;
                    anSpit = !string.IsNullOrEmpty(aNmailInformation)
                        ? aNmailInformation.Split('|')
                        : new string[] { " ", " ", " " };

                    //获取拖车行的邮件地址
                    string truckCustomersEmail = OceanImportService.GetTruckCustomersEmail(oceanBusinessInfo.ID);

                    //创建事件 
                    EventCode eventCode = EventCodeList("LFDNt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //获取业务员信息
                    UserDetailInfo sales = ServiceClient.GetService<IUserService>().GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));

                    var message = CreateMessageInfo(MessageType.Email, MessageWay.Send, ccflg ? anSpit[0] : truckCustomersEmail,
                        LocalData.UserInfo.EmailAddress, FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty, body.ToString(), subject.ToString(), ccflg ? sales.EMail : string.Empty,
                        string.Empty, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);
                }
            }
        }
        /// <summary>
        ///  LFD 前2工作日后每天提柜提醒
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void MailContainerpickupreminder(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bool ccflg = false;
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("CONTAINER PICK UP REMINDER: Reference No#");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    if (oceanBusinessInfo.TransportClauseName.LastIndexOf("CY") > 0)
                    {
                        ccflg = true;
                        body.Append("Dear Valued customer,<br/><br />");
                    }
                    else if (oceanBusinessInfo.TransportClauseName.LastIndexOf("DOOR") > 0)
                    {
                        body.Append("Dear Valued Trucker,<br/><br />");
                    }
                    body.Append("Our record show that containers still yet picked up, please arrange picking up as soon as possible before LFD to avoid any charges . <br/>");
                    body.Append("We will not be responsible for any charge due to your late activity .<br />");
                    body.Append("Any question, please feel free to contact me.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    //获取客户的邮件地址
                    string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
                    string[] anSpit = null;
                    anSpit = !string.IsNullOrEmpty(aNmailInformation)
                        ? aNmailInformation.Split('|')
                        : new string[] { " ", " ", " " };

                    //获取拖车行的邮件地址
                    string truckCustomersEmail = OceanImportService.GetTruckCustomersEmail(oceanBusinessInfo.ID);

                    //创建事件 
                    EventCode eventCode = EventCodeList("FCPNt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //获取业务员信息
                    UserDetailInfo sales = ServiceClient.GetService<IUserService>().GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));

                    var message = CreateMessageInfo(MessageType.Email, MessageWay.Send, ccflg ? anSpit[0] : truckCustomersEmail,
                        LocalData.UserInfo.EmailAddress, FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty, body.ToString(), subject.ToString(), ccflg ? sales.EMail : string.Empty,
                        string.Empty, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);
                }
            }
        }
        /// <summary>
        ///  提柜后第2天开始每天还空提醒
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void MailEmptyreturnnotice(Guid operationid, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bool ccflg = false;
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                string Message = ShowMessage(oceanBusinessInfo);
                if (!string.IsNullOrEmpty(Message))
                {
                    XtraMessageBox.Show(Message);
                    return;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("EMPTY RETURN NOTICE:  Reference No#");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    if (oceanBusinessInfo.TransportClauseName.LastIndexOf("CY") > 0)
                    {
                        ccflg = true;
                        body.Append("Dear Valued customer,<br/><br />");
                    }
                    else if (oceanBusinessInfo.TransportClauseName.LastIndexOf("DOOR") > 0)
                    {
                        body.Append("Dear Valued Trucker,<br/><br />");
                    }
                    body.Append("Our record show that empty still not yet returned, please arrange return as soon as possible to avoid potential charges. <br/>");
                    body.Append("Any question, please feel free to contact me.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");


                    //获取客户的邮件地址
                    string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
                    string[] anSpit = null;
                    anSpit = !string.IsNullOrEmpty(aNmailInformation)
                        ? aNmailInformation.Split('|')
                        : new string[] { " ", " ", " " };

                    //获取拖车行的邮件地址
                    string truckCustomersEmail = OceanImportService.GetTruckCustomersEmail(oceanBusinessInfo.ID);

                    //创建事件 
                    EventCode eventCode = EventCodeList("ECRNt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //获取业务员信息
                    UserDetailInfo sales = ServiceClient.GetService<IUserService>().GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));

                    var message = CreateMessageInfo(MessageType.Email, MessageWay.Send, ccflg ? anSpit[0] : truckCustomersEmail,
                        LocalData.UserInfo.EmailAddress, FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty, body.ToString(), subject.ToString(), ccflg ? sales.EMail : string.Empty,
                        string.Empty, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);
                }
            }
        }
        /// <summary>
        /// ETA变更 服务端发送邮件
        /// </summary>
        /// <param name="operationid">业务的ID</param>
        public void MailEtachangeServerSend(List<Guid> operationid)
        {
            foreach (var itm in operationid)
            {
                OceanBusinessInfo oceanBusinessInfo = OceanImportService.GetBusinessInfo(itm);
                if (oceanBusinessInfo.CustomerService == null)
                {
                    //终止当前循环，直接进入下一次循环
                    continue;
                }
                if (oceanBusinessInfo.SalesID == null)
                {
                    continue;
                }
                if (oceanBusinessInfo != null)
                {
                    StringBuilder body = new StringBuilder();
                    StringBuilder subject = new StringBuilder();
                    string containerno = Returncontainerno(oceanBusinessInfo.ContainerList);
                    string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);

                    //主题
                    subject.Append("ETA CHANGE:  Reference No#");
                    subject.Append(oceanBusinessInfo.No + "/");
                    subject.Append("HBL#");
                    subject.Append(hblNo + "/");
                    string strEta = string.Empty;
                    if (oceanBusinessInfo.ETA != null)
                    {
                        DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                        strEta = eta.ToString("yyyy-MM-dd");
                        subject.Append(strEta + "/");
                    }
                    else
                    {
                        subject.Append("/");
                    }
                    subject.Append(oceanBusinessInfo.ContainerDescription + "/");
                    subject.Append(containerno + "/");
                    subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                    body.Append("Dear Valued customer ,<br/><br />");
                    body.Append("Please be advised that ETA for the subject shipment changed to" + strEta + " <br/>");
                    body.Append("Any question, please feel free to contact me.<br />");
                    body.Append("</div>");
                    body.Append("</body>");
                    body.Append("</html>");

                    EventCode eventCode = EventCodeList("ETANt");
                    var eventObjects = new EventObjects
                    {
                        OperationID = oceanBusinessInfo.ID,
                        OperationType = OperationType.OceanImport,
                        FormID = oceanBusinessInfo.ID,
                        FormType = FormType.Unknown,
                        Code = eventCode.Code,
                        Description = eventCode.Subject,
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

                    //获取客服信息
                    UserDetailInfo customerService = ServiceClient.GetService<IUserService>().
                        GetUserDetailInfo(new Guid(oceanBusinessInfo.CustomerService.ToString()));

                    //业务员的信息
                    UserDetailInfo sales = ServiceClient.GetService<IUserService>().
                        GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));


                    //构建邮件实体
                    var message = CreateMessageInfo(MessageType.Email, MessageWay.Send, customerService.EMail,
                        LocalData.UserInfo.EmailAddress, FormType.Booking, OperationType.OceanImport,
                        oceanBusinessInfo.ID, Guid.Empty, body.ToString(), subject.ToString(), sales.EMail, string.Empty, eventObjects);
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    //服务端发送邮件
                    MessageService.Send(message);
                }
            }
        }
        /// <summary>
        /// 发送催款邮件
        /// </summary>
        /// <param name="operationId">业务ID</param>
        public void PayNtMail(Guid operationId)
        {
            //自动发送邮件
            if (operationId == Guid.Empty)
            {
                List<Guid> operationIDs = OceanImportService.GetPayNtOperationId();
                if (operationIDs.Any())
                {
                    foreach (var id in operationIDs)
                    {
                        PayNtMailSend(OceanImportService.GetBusinessInfo(id));
                    }
                }
            }
            else
            {
                PayNtClickMailSend(OceanImportService.GetBusinessInfo(operationId));
            }
        }
        /// <summary>
        /// 手动点击发送邮件
        /// </summary>
        /// <param name="oceanBusinessInfo">实体对象</param>
        public void PayNtClickMailSend(OceanBusinessInfo oceanBusinessInfo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string hblNo = ReturnhblNo(oceanBusinessInfo.HBLList);
                string containerNo = Returncontainerno(oceanBusinessInfo.ContainerList);
                string sendto = string.Empty;
                StringBuilder stringBuilder = new StringBuilder();
                BusinessOperationContext context = new BusinessOperationContext { OperationID = oceanBusinessInfo.ID };
                List<CommunicationHistory> communicationHistory =
                    CommunicationHistoryService.GetCommunicationHistoryList(context)
                        .Where(c => c.ContactStage.Contains("AN"))
                        .OrderByDescending(nn => nn.CreateDate)
                        .ToList();
                if (communicationHistory.Any())
                {
                    foreach (var item in communicationHistory)
                    {
                        if (item.SendTo.Contains("@citycoean") == false)
                        {
                            if (!string.IsNullOrEmpty(stringBuilder.ToString()))
                            {
                                if (stringBuilder.ToString().Contains(item.SendTo))
                                {
                                    continue;
                                }
                            }
                            stringBuilder.Append(item.SendTo + ";");
                        }
                    }
                    sendto = stringBuilder.ToString();
                }

                //获取业务员信息
                UserDetailInfo sales =
                    ServiceClient.GetService<IUserService>()
                        .GetUserDetailInfo(new Guid(oceanBusinessInfo.SalesID.ToString()));

                StringBuilder body = new StringBuilder();
                StringBuilder subject = new StringBuilder();
                //主题
                subject.Append(" PAYMENT REQUEST & OBL REQUEST: Reference No#");
                subject.Append(oceanBusinessInfo.No + "/");
                subject.Append(" HBL#");
                subject.Append(hblNo + "/ ");
                string strEta = string.Empty;
                if (oceanBusinessInfo.ETA != null)
                {
                    DateTime eta = (DateTime)oceanBusinessInfo.ETA;
                    strEta = eta.ToString("yyyy-MM-dd");
                    subject.Append(strEta + "/");
                }
                else
                {
                    subject.Append("/");
                }

                subject.Append(oceanBusinessInfo.ContainerDescription + "/ ");
                subject.Append(containerNo + "/ ");
                subject.Append(oceanBusinessInfo.POLName + " To " + oceanBusinessInfo.PODName);

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
                body.Append("Dear Valued customer , " + "<br/>");
                body.Append("Kindly reminder that shipment will arrive soon, please arrange payment as soon as possible to avoid any delay on the release , and please allow us at least 24 -48 hours to proceed the release. " + "<br/>");
                body.Append("If HBL issued Original , please also send us Original HBL ( or OBL) in no time " + "<br/>");
                body.Append("We will not be responsible for any potential charges due to late payment or OHBL submit.<br /><br />");
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");
                EventCode eventCodes = EventCodeList("PayNt");
                var eventObjects = new EventObjects
                {
                    OperationID = oceanBusinessInfo.ID,
                    OperationType = OperationType.OceanImport,
                    FormID = oceanBusinessInfo.ID,
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
                    MessageWay.Send, sendto, LocalData.UserInfo.EmailAddress,
                    FormType.Booking, OperationType.OceanImport,
                    oceanBusinessInfo.ID, Guid.Empty,
                    body.ToString(), subject.ToString(),
                    sales.EMail,
                    eventCodes.Code, eventObjects);
                message.BodyFormat = BodyFormat.olFormatHTML;
                message.State = MessageState.Success;
                MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);
            }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="oceanBusinessInfo">实体对象</param>
        public void PayNtMailSend(OceanBusinessInfo oceanBusinessInfo)
        {
            ////获取收件人地址
            //string aNmailInformation = MailInformation(oceanBusinessInfo.ID, "AN", null, false, false);
            //string[] anSpit = null;
            //if (string.IsNullOrEmpty(aNmailInformation))
            //{
            //    return;
            //}
            //else
            //{
            //    anSpit = aNmailInformation.Split('|');
            //}
            //#region  添加邮件附件信息
            //List<AttachmentContent> attachmentContents = new List<AttachmentContent>();
            //var uplodANFile = ClientFileService.GetDocumentListByDocumentType(oceanBusinessInfo.ID, ICP.DataCache.ServiceInterface.DocumentType.AN);
            //if (uplodANFile.Any())
            //{
            //    foreach (var info in uplodANFile)
            //    {
            //        //Bit 转string 判断当前是否存在 
            //        string contents = Encoding.Default.GetString(info.Content);
            //        AttachmentContent attachment = new AttachmentContent();
            //        attachment.Name = info.Name;
            //        if (!string.IsNullOrEmpty(contents))
            //        {
            //            attachment.Size = BitConverter.ToInt64(info.Content, 0);
            //        }
            //        attachment.Content = info.Content;
            //        attachment.DisplayName = info.Name;
            //        string path = System.AppDomain.CurrentDomain.BaseDirectory;
            //        path = Path.Combine(path, "filetemp");
            //        path += "\\" + info.Name;
            //        ICP.Common.ServiceInterface.IOHelper.WriteToDisk(path, info.Content);
            //        attachment.ClientPath = path;
            //        attachmentContents.Add(attachment);
            //    }
            //}
            //if (uplodANFile.Count == 0)
            //{
            //    return;
            //}
            //#endregion
            //string hblNo = string.Empty;
            //if (oceanBusinessInfo.HBLList != null)
            //{
            //    foreach (var con in oceanBusinessInfo.HBLList)
            //    {
            //        hblNo += con.HBLNo + ",";
            //    }
            //}
            //MessageService.Send(ReturnEmailMessage(hblNo, attachmentContents, oceanBusinessInfo.ID, anSpit[0],
            //     string.IsNullOrEmpty(anSpit[2]) ? string.Empty : anSpit[2], anSpit[1]));

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
        /// <returns></returns>
        public Message.ServiceInterface.Message CreateMessageInfo(MessageType type,
            MessageWay way, string sendTo, string sendFrom, FormType formType,
            OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
            string action, EventObjects eventObjects)
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
            message.UserProperties.EventInfo = eventObjects;
            return message;

        }
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
            if (string.IsNullOrEmpty(oceanBooking.No) && oceanBooking.NoCheck == true)
            {
                promptCh.Append(" 业务号为空 ");
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
        /// 根据业务ID读取关联人员邮件地址
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
        /// 提示错误信息
        /// </summary>
        /// <param name="oeOceanBusinessInfo"></param>
        /// <returns></returns>
        public string ShowMessage(OceanBusinessInfo oeOceanBusinessInfo)
        {
            string retunStr;
            var promptCh = new StringBuilder();
            var promptEn = new StringBuilder();
            if (string.IsNullOrEmpty(oeOceanBusinessInfo.No))
            {
                promptCh.Append(" 业务号为空 ");
                promptEn.Append(" Business number is empty ");
            }
            if (string.IsNullOrEmpty(oeOceanBusinessInfo.POLName))
            {
                promptCh.Append(" 装货港为空 ");
                promptEn.Append(" The port of loading is empty ");
            }
            if (string.IsNullOrEmpty(oeOceanBusinessInfo.PODName))
            {
                promptCh.Append(" 卸货港为空 ");
                promptEn.Append(" Port of discharge is empty ");
            }
            if (string.IsNullOrEmpty(oeOceanBusinessInfo.ContainerDescription == null ?
                                         string.Empty : oeOceanBusinessInfo.ContainerDescription.ToString()))
            {
                promptCh.Append(" 箱信息为空 ");
                promptEn.Append(" Container information is empty ");

            }
            if (string.IsNullOrEmpty(oeOceanBusinessInfo.CustomerName))
            {
                promptCh.Append(" 客户信息为空 ");
                promptEn.Append(" Customer information is empty ");
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
        /// 返回当前业务信息的箱号
        /// </summary>
        /// <param name="containerList"></param>
        /// <returns></returns>
        public string Returncontainerno(List<OIBusinessContainerList> containerList)
        {

            string containerno = string.Empty;
            if (containerList.Count() > 3)
            {
                for (int i = 0; i < containerList.Count(); i++)
                {
                    if (i < 2)
                    {
                        if (string.IsNullOrEmpty(containerno))
                        {
                            containerno = containerList[i].No;
                        }
                        else
                        {
                            containerno = containerno + ";" + containerList[i].No;
                        }
                    }
                }
                containerno = containerno + "...";
            }
            else
            {
                foreach (var item in containerList)
                {
                    if (string.IsNullOrEmpty(containerno))
                    {
                        containerno = item.No;
                    }
                    else
                    {
                        containerno = containerno + ";" + item.No;
                    }
                }
            }
            return containerno;
        }
        /// <summary>
        /// 返回当前业务信息的HBL的信息
        /// </summary>
        /// <param name="hblList"></param>
        /// <returns></returns>
        public string ReturnhblNo(List<OceanBusinessHBLList> hblList)
        {
            string hblNo = string.Empty;
            if (hblList != null)
            {
                foreach (var con in hblList)
                {
                    hblNo += con.HBLNo + ",";
                }
            }
            hblNo = hblNo.TrimEnd(',');

            return hblNo;
        }

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

        #region Print
        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="values"></param>
        /// <param name="NO"></param>
        public void PrintReleaseOrder(IDictionary<string, object> values, string NO)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string no = Utility.GetLineNo(NO);
                string title = (LocalData.IsEnglish ? "Print Release Order" : "放货通知书") + ("-" + no);
                PartLoader.ShowEditPart<OIReleaseOrder2>(RootWorkItem, null, values, title, null, null);
            }
        }
        /// <summary>
        /// 打印发货通知书
        /// </summary>
        /// <param name="businessList"></param>
        public void PrintReleaseOrder(OceanBusinessList businessList)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> stateValues = new Dictionary<string, object>();
                    stateValues.Add("OceanBusinessList", businessList);
                    string no = Utility.GetLineNo(businessList.No);
                    string title = (LocalData.IsEnglish ? "Print Release Order" : "放货通知书") + ("-" + no);
                    PartLoader.ShowEditPart<OIReleaseOrder2>(RootWorkItem, null, stateValues, title, null, null);
                }
            }
        }
        /// <summary>
        /// 打印发货通知书
        /// </summary>
        /// <param name="operationid"></param>
        public void PrintReleaseOrder(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                    Dictionary<string, object> stateValues = new Dictionary<string, object>();
                    stateValues.Add("OceanBusinessList", businessList);
                    string no = Utility.GetLineNo(businessList.No);
                    string title = (LocalData.IsEnglish ? "Print Release Order" : "放货通知书") + ("-" + no);
                    PartLoader.ShowEditPart<OIReleaseOrder2>(RootWorkItem, null, stateValues, title, null, null);
                }
            }
        }
        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="values"></param>
        /// <param name="NO"></param>
        public void PrintForwardingBill(IDictionary<string, object> values, string NO)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string no = Utility.GetLineNo(NO);
                string title = (LocalData.IsEnglish ? "Print Forwarding Bill" : "货代提单") + ("-" + no);
                PartLoader.ShowEditPart<OIBLPrintPart2>(RootWorkItem, null, values, title, null, null);
            }
        }
        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="businessList"></param>
        public void PrintForwardingBill(OceanBusinessList businessList)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanBusinessList", businessList);
                string no = Utility.GetLineNo(businessList.No);
                string title = (LocalData.IsEnglish ? "Print Forwarding Bill" : "货代提单") + ("-" + no);
                PartLoader.ShowEditPart<OIBLPrintPart2>(RootWorkItem, null, stateValues, title, null, null);
            }
        }
        /// <summary>
        /// 根据业务ID打印货代提单
        /// </summary>
        /// <param name="operationId"></param>
        public void PrintForwardingBill(Guid operationId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationId);
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("OceanBusinessList", businessList);
                string no = Utility.GetLineNo(businessList.No);
                string title = (LocalData.IsEnglish ? "Print Forwarding Bill" : "货代提单") + ("-" + no);
                PartLoader.ShowEditPart<OIBLPrintPart2>(RootWorkItem, null, stateValues, title, null, null);
            }
        }
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="operationInfo"></param>
        public void PrintProfit(OceanBusinessList list, Message.ServiceInterface.Message operationInfo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanImportPrintHelper.PrintProfit(list, operationInfo);
            }
        }
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="list"></param>
        public void PrintProfit(OceanBusinessList list)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanImportPrintHelper.PrintProfit(list, GetOperationInfo(list.ID));
            }
        }
        /// <summary>
        /// 根据业务ID打印利润表
        /// </summary>
        /// <param name="operaitonid"></param>
        public void PrintProfit(Guid operaitonid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operaitonid);
                OceanImportPrintHelper.PrintProfit(businessList, GetOperationInfo(businessList.ID));
            }
        }
        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="customerName">客户名称</param>
        /// <param name="customerID">客户ID</param>
        public void PrintExportBusinessInfo(string customerName, Guid customerID)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanImportPrintHelper.PrintExportBusinessInfo(customerName, customerID);
            }
        }
        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationid"></param>
        public void PrintExportBusinessInfo(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                OceanImportPrintHelper.PrintExportBusinessInfo(businessList.CustomerName, businessList.CustomerID);
            }
        }
        /// <summary>
        ///海进打印工作表
        /// </summary>
        public void OIPrintWorkSheet(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessList businessList = OceanImportService.GetBusinessInfo(operationid);
                OceanImportPrintHelper.PrintWorkSheet(businessList, GetOperationInfo(businessList.ID));
            }
        }
        /// <summary>
        /// 打印-到港通知书
        /// </summary>
        /// <param name="list"></param>
        public void PrintArrivalNotice(OceanBusinessList list)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (list.IsSentAN == false && string.IsNullOrEmpty(list.SalesName))
                {
                    if (MessageBoxService.ShowQuestion(
                        LocalData.IsEnglish
                            ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?"
                            : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?"
                        , LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    //if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?" : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?",
                    //                    LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //{
                    //    return;
                    //}
                }
                OceanBusinessInfo currentBusinessInfo = OceanImportService.GetBusinessInfo(list.ID);
                if (currentBusinessInfo.CompanyID == new Guid("0501D29D-0EFE-E111-B376-0026551CA87B")) //巴西到港通知
                {
                    Message.ServiceInterface.Message operationInfo = GetOperationInfo(list.ID);
                    OceanImportPrintHelper.PrintArrivalNoticeReportForBrazil(list, operationInfo);
                }
                else
                {
                    Dictionary<string, object> stateValues = new Dictionary<string, object>();
                    stateValues.Add("OceanBusinessList", list);
                    string no = Utility.GetLineNo(list.No);
                    string title = (LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书") + ("-" + no);
                    PartLoader.ShowEditPart<OIArrivalNotice2>(RootWorkItem, currentBusinessInfo.CompanyID, stateValues, title, null, null);
                }
            }
        }
        /// <summary>
        /// 根据业务ID打印到港通知书
        /// </summary>
        /// <param name="operationid"></param>
        public void PrintArrivalNotice(Guid operationid)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanBusinessInfo BusinessInfo = OceanImportService.GetBusinessInfo(operationid);
                if (BusinessInfo.IsSentAN == false && string.IsNullOrEmpty(BusinessInfo.SalesName))
                {
                    if (MessageBoxService.ShowQuestion(
                        LocalData.IsEnglish
                            ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?"
                            : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?"
                        , LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    //if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sales name is null,and Sales can not receive the Arrival Notice ,Sure to continue?" : "由于未填写业务员,港后通知邮件将无法通知到业务员.是否继续?",
                    //                    LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //{
                    //    return;
                    //}
                }

                if (BusinessInfo.CompanyID == new Guid("0501D29D-0EFE-E111-B376-0026551CA87B")) //巴西到港通知
                {
                    Message.ServiceInterface.Message operationInfo = GetOperationInfo(operationid);
                    OceanImportPrintHelper.PrintArrivalNoticeReportForBrazil(BusinessInfo, operationInfo);

                }
                else
                {
                    Dictionary<string, object> stateValues = new Dictionary<string, object>();
                    stateValues.Add("OceanBusinessList", BusinessInfo);
                    string no = Utility.GetLineNo(BusinessInfo.No);
                    string title = (LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书") + ("-" + no);
                    PartLoader.ShowEditPart<OIArrivalNotice2>(RootWorkItem, BusinessInfo.CompanyID, stateValues, title, null, null);
                }
            }
        }
        /// <summary>
        /// 通过业务ID构建Message对象
        /// </summary>
        /// <param name="operationID"></param>
        /// <returns></returns>
        private Message.ServiceInterface.Message GetOperationInfo(Guid operationID)
        {
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = operationID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = operationID;

            return message;
        }
        #endregion
    }
}

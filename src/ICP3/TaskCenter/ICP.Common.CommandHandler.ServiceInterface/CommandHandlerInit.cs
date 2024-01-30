using ICP.Business.Common.UI.Contact;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Forms;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 命令处理模块
    /// </summary>
    public class CommandHandlerInit : ModuleInit
    {
        #region Fields & Property & Services
        /// <summary>
        /// Work Item
        /// </summary>
        public WorkItem workItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// Work Item
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        /// <summary>
        /// 当前的业务处理面板
        /// </summary>
        public BaseBusinessPart CurrentBaseBusinessPart
        {
            get { return workItem.State["CurrentBaseBusinessPart"] as BaseBusinessPart; }
            set { workItem.State["CurrentBaseBusinessPart"] = value; }
        }

        /// <summary>
        /// 当前的列表业务面板
        /// </summary>
        public IListBaseBusinessPart CurrentListBaseBusinessPart
        {
            get
            {
                return CurrentBaseBusinessPart as IListBaseBusinessPart;
            }
        }

        /// <summary>
        /// 当前焦点行
        /// </summary>
        public DataRow FocusedDataRow
        {
            get
            {
                return CurrentListBaseBusinessPart.FocusedDataRow;
            }
        }

        /// <summary>
        /// 海出命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanExportCommandHandler OceanExportCommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 海进命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanImportCommandHandler OceanImportCommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 公共命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonCommandHandler CommonCommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 空运出口命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public AirExportCommandHandler AirExportCommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 空运进口命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public AirImportCommandHandler AirImportCommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 其他业务命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OtherBusinessCommandHandler OtherBusinessCommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 是否英文(中英版本号)
        /// </summary>
        public bool IsEnglish
        {
            get
            {
                return LocalData.IsEnglish;
            }
        } 
        #endregion

        #region 传输数据
        /// <summary>
        /// 传输数据：设置海出、公用命令处理类的业务面板对象为当前业务面板对象
        /// </summary>
        public void TransferData()
        {
            OceanExportCommandHandler.CurrentBaseBusinessPart = CommonCommandHandler.CurrentBaseBusinessPart
            = CurrentBaseBusinessPart;
        } 
        #endregion

        #region 是否回车查询
        /// <summary>
        /// 是否回车查询
        /// </summary>
        /// <param name="flag"></param>
        private void IsEnterKeySearch(bool flag)
        {
            workItem.State["IsEnterKeySearch"] = flag;
        } 
        #endregion

        #region 公共的处理命令（命名规则以Command_Common_事件名称）
        /// <summary>
        /// 测试方法：弹窗显示当前选择业务OperationID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_Common_Test)]
        public void Command_Common_Test(object sender, EventArgs e)
        {
            MessageBox.Show(CurrentBaseBusinessPart.OperationID.ToString());
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_Common_Refresh)]
        public void Command_Common_Refresh(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.Refersh();
        }

        /// <summary>
        /// F5查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_F5_Query")]
        public void Command_F5_Query(object sender, EventArgs e)
        {
            TransferData();
            CurrentBaseBusinessPart.F5Query();
        }

        /// <summary>
        /// 显示联系人列表(客户及其参与者)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenterShowContactsAndAssistants")]
        public void Command_EmailCenterShowContactsAndAssistants(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //构造业务参数
                BusinessOperationContext businessOperation = new BusinessOperationContext
                {
                    OperationID = CurrentBaseBusinessPart.OperationID,
                    OperationType = CurrentBaseBusinessPart.OperationType,
                    OperationNO = CurrentBaseBusinessPart.OperationNo,
                    CompanyID = CurrentBaseBusinessPart.CompanyID,
                };
                UCContactListPart ucContactListPart = ServiceClient.GetClientService<WorkItem>().SmartParts.AddNew<UCContactListPart>();
                PopupWindow form = PartLoader.FakeShowDialog(RootWorkItem, ucContactListPart,
                                                             LocalData.IsEnglish ? "Show Contacts and Assistants" : "查看联系人/参与者",
                                                             FormWindowState.Normal,
                                                             CurrentBaseBusinessPart);
                ucContactListPart.DataBind(businessOperation);
            }
        }

        /// <summary>
        /// 参与者面板初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_AddStaffListPart")]
        public void Command_AddStaffListPart(object sender, EventArgs e)
        {
            CommonCommandHandler.AddStaffPart();
        }
        /// <summary>
        /// 未知业务面板关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_Unknown_EmailCenterOperationMessageRelation")]
        public void Command_Unknown_MailCenterMessageRelation(object sender, EventArgs e)
        {

            try
            {
                CommonCommandHandler.MailLink4UnknownMessageRelation();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                LocalCommonServices.ErrorTrace.SetErrorInfo(CurrentBaseBusinessPart.FindForm(), ex.Message);
            }
        }
        /// <summary>
        /// 参与者面板绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_AssistantBindData")]
        public void Command_AssistantBindData(object sender, EventArgs e)
        {
            CommonCommandHandler.AssistantBindData();
        }

        /// <summary>
        /// 清空参与者面板控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Commmand_ClearStaffListPart")]
        public void Commmand_ClearStaffListPart(object sender, EventArgs e)
        {
            CommonCommandHandler.Clear();
        }

        /// <summary>
        ///  上传SI附件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandUploadSiAttachment)]
        public void Command_UploadSIAttachment(object sender, EventArgs eventArgs)
        {
            TransferData();
            CommonCommandHandler.UploadingSi(MailHelper.MailSubject, MailHelper.ObjMailItem);
        }

        /// <summary>
        ///  上传SO附件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandUploadSOAttachment)]
        public void Command_UploadSOAttachment(object sender, EventArgs eventArgs)
        {
            TransferData();

            CommonCommandHandler.UploadSO(MailHelper.MailSubject, MailHelper.ObjMailItem);
        }
        /// <summary>
        ///  上传海出附件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandUploadAttachment)]
        public void Command_EmailCenterUploadAttachment(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.UploadAttachment(MailHelper.MailSubject, MailHelper.ObjMailItem);
        }
        /// <summary>
        /// 上传SI附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationUploadSIAttachment)]
        public void CommandCommunicationUploadSIAttachment(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.UploadingSi(string.Empty, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandUploadMBLAttachment)]
        public void CommandUploadMBLAttachment(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.UploadMBL(MailHelper.MailSubject, MailHelper.ObjMailItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandUploadAPAttachment)]
        public void CommandUploadAPAttachment(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.UploadAP(MailHelper.MailSubject, MailHelper.ObjMailItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandUploadANAttachment)]
        public void CommandUploadANAttachment(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.UploadAN(MailHelper.MailSubject, MailHelper.ObjMailItem);
        }

        /// <summary>
        ///  上传附件信息(包括拖拽的附件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandUploadDragAttachment)]
        public void Command_EmailCenterUploadDragAttachment(object sender, EventArgs e)
        {
            TransferData();
            List<string> lists = workItem.State["DragDocList"] as List<string>;
            CommonCommandHandler.UploadAttachment(UploadWay.DragDrop, ClientUtility.GetCurrentMailSubject(), ClientProperties.ObjMailItem, lists);
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_Submit)]
        public void Command_Submit(object sender, EventArgs e)
        {
            CurrentListBaseBusinessPart.Save();
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_Refresh)]
        public void Command_Refresh(object sender, EventArgs e)
        {
            IListBaseBusinessPart listBaseBusiness = CurrentListBaseBusinessPart;
            listBaseBusiness.IsShowLoadingForm = true;
            bool isMergeAdvanceQueryString = !string.IsNullOrEmpty(listBaseBusiness.AdvanceQueryString);
            listBaseBusiness.QueryData(isMergeAdvanceQueryString);
        }

        /// <summary>
        /// 保存业务关联信息
        /// </summary>
        /// <param name="messageRelationParameter"></param>
        public void SaveOperationMessageRelation(MessageRelationParameter messageRelationParameter)
        {
            CommonCommandHandler.SaveOperationMessageRelation(messageRelationParameter);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_Common_CSPBooking_Download)]
        public void CSPBooking_Download(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.CSPBooking_Download(sender, e);
        }
        #endregion 公共的处理命令（命名规则以Command_Common_事件名称）

        #region 海出的处理命令（命名规则以 Command_或 Command_OceanExport 事件名称）

        /// <summary>
        /// 打印订舱确认书(宁波)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_OceanExport_Print_BookingConfirmation4NB)]
        public void Command_OceanExport_Print_BookingConfirmation4NB(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandPrintBookingConfirmation4NB(null, null);
        }

        /// <summary>
        /// 查看订单状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_OceanExport_ViewOrderStates)]
        public void Command_OceanExport_ViewOrderStates(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandViewOrderStates(null, null);
        }

        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoAdd)]
        public void Command_ForSoAdd(object sender, EventArgs eventArgs)
        {
            TransferData();
            AddSoForOceanExport();
        }
        /// <summary>
        /// 新增订舱单
        /// </summary>
        private void AddSoForOceanExport()
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoAdd(null, null);
        }

        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoEdit)]
        public void Command_ForSoEdit(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterOpenSo(sender, eventArgs);
        }
        /// <summary>
        /// 复制订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoCopy)]
        public void Command_ForSoCopy(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoCopy(sender, eventArgs);
        }


        /// <summary>
        /// 复制订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.Command_ForFasterAdd)]
        public void CommandTaskCenterForFasterAdd(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForFasterAdd(sender, eventArgs);
        }

        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.Command_OceanExport_CopyBusiness)]
        public void Command_OceanExport_CopyBusiness(object sender, EventArgs eventArgs)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanExportCommandHandler.CopyBusiness(listPart);
        }

        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoApplyAgent)]
        public void Command_ForSoApplyAgent(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoApplyAgent(sender, eventArgs);

        }

        /// <summary>
        /// 向订舱员发起申请订舱 发送邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoApplySo)]
        public void Command_ForSoApplySO(object sender, EventArgs eventArgs)
        {
            TransferData();
            //先置灰按钮在执行方法
            ListBaseBusinessPart listBase = CurrentBaseBusinessPart as ListBaseBusinessPart;
            listBase.ControlButton("SOA", "ForSo_ApplySO", true);
            OceanExportCommandHandler.CommandTaskCenterForSoApplySo();
            OceanExportCommandHandler.ListRefresh(OperationType.OceanExport, CurrentListBaseBusinessPart);
        }
        /// <summary>
        /// 取消订单 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoCancel)]
        public void Command_ForSoCancel(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoCancel(sender, eventArgs);
            Command_Refresh(sender, eventArgs);
        }

        /// <summary>
        /// 恢复订单 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoResume)]
        public void Command_ForSoResume(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoResume(sender, eventArgs);

        }
        /// <summary>
        ///  确认装船
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoOnBoard)]
        public void Command_ForSoOnBoard(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoOnBoard(FocusedDataRow);
        }
        /// <summary>
        /// 业务联单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoPrintOrder)]
        public void Command_ForSoPrintOrder(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoPrintOrder(sender, eventArgs);
        }
        /// <summary>
        /// 订舱确认书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandForSoPrint)]
        public void Command_ForSoPrint(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterForSoPrint(sender, eventArgs);
        }
        /// <summary>
        /// 打开Bkg Failed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationBkgFailed)]
        public void Command_CommunicationBkgFailed(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.OpenBkgFailed(sender, eventArgs);
        }

        /// <summary>
        /// 分发文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationAgentDocs)]
        public void Command_CommunicationAgentDocs(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.OpenDispatchDocument(sender, eventArgs);
        }

        /// <summary>
        /// 询价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationInquireRates)]
        public void Command_CommunicationInquireRates(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterCommunicationInquireRates(sender, eventArgs);
        }
        /// <summary>
        /// 电子订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationOnlineBkg)]
        public void Command_CommunicationOnlineBkg(object sender, EventArgs eventArgs)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanExportCommandHandler.CommandEmbl(listPart);
        }

        /// <summary>
        /// 电子订舱确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.Command_CommunicationOnlineConfirm)]
        public void Command_CommunicationOnlineConfirm(object sender, EventArgs eventArgs)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanExportCommandHandler.CommandEBookConfirm(listPart);
        }

        /// <summary>
        /// 确认费用(中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationConfirmDebitFeesChs)]
        public void Command_CommunicationConfirmDebitFeesCHS(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.ConfirmDebitFees(false);
        }
        /// <summary>
        /// 确认费用(英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationConfirmDebitFeesEng)]
        public void Command_CommunicationConfirmDebitFeesENG(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.ConfirmDebitFees(true);
        }
        /// <summary>
        /// 利润承诺(中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationAskProfitPromiseChs)]
        public void Command_CommunicationAskProfitPromiseCHS(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.AskProfitPromise(false);
        }
        /// <summary>
        /// 利润承诺(英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationAskProfitPromiseEng)]
        public void Command_CommunicationAskProfitPromiseENG(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.AskProfitPromise(true);
        }
        /// <summary>
        /// 取消订舱 邮件中文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationMailSoCancelationtoCustomerChs)]
        public void Command_CommunicationMailSOCancelationtoCustomerChs(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailSoCancelationtoCustomer(false);
        }
        /// <summary>
        /// 取消订舱 邮件英文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationMailSoCancelationtoCustomerEng)]
        public void Command_CommunicationMailSOCancelationtoCustomerEng(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailSoCancelationtoCustomer(true);
        }
        /// <summary>
        /// 客户补料  邮件中文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationAskCustomerForSichs)]
        public void Command_CommunicationAskCustomerForSICHS(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.AskCustomerForSi(false);
        }
        /// <summary>
        /// 客户补料   邮件英文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationAskCustomerForSieng)]
        public void Command_CommunicationAskCustomerForSIENG(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.AskCustomerForSi(true);
        }
        /// <summary>
        /// 客户订舱成功 邮件中文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationMailSoCopyToCustomerChs)]
        public void Command_CommunicationMailSOCopyToCustomerCHS(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailSoCopyToCustomer(false);
        }
        /// <summary>
        /// 客户订舱成功 邮件英文版
        /// </summary>
        [CommandHandler(CommonCommandName.CommandCommunicationMailSoCopyToCustomerEng)]
        public void Command_CommunicationMailSOCopyToCustomerENG(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailSoCopyToCustomer(true);
        }

        /// <summary>
        ///  通知客户订舱失败 邮件中文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationMailSoFailureToCustomerChs)]
        public void Command_CommunicationMailSOFailureToCustomerCHS(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailSoFailureToCustomer(false);
        }

        /// <summary>
        ///  通知客户订舱失败 邮件英文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationMailSoFailureToCustomerEng)]
        public void Command_CommunicationMailSOFailureToCustomerENG(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailSoFailureToCustomer(true);
        }

        /// <summary>
        ///  打开HBL
        /// </summary>
        [CommandHandler(CommonCommandName.CommandCommunicationOpenHbl)]
        public void Command_CommunicationOpenHBL(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterEditHbl(sender, eventArgs);
        }

        /// <summary>
        /// 打开MBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationOpenMbl)]
        public void Command_CommunicationOpenMBL(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterEditMbl(sender, eventArgs);
        }

        /// <summary>
        /// 打开账单列表
        /// </summary>
        [CommandHandler(CommonCommandName.CommandCommunicationAccInfo)]
        public void Command_CommunicationAccInfo(object sender, EventArgs eventArgs)
        {
            TransferData();
            //EmailOpenAccInfo();
            OceanExportCommandHandler.CommandTaskCenterCommunicationAccInfo(sender, eventArgs);
        }

        /// <summary>
        /// 批量新增账单
        /// </summary>
        [CommandHandler(CommonCommandName.Command_CommunicationBatchAddAccInfo)]
        public void Command_CommunicationBatchAddAccInfo(object sender, EventArgs eventArgs)
        {
            TransferData();
            //EmailOpenAccInfo();
            CommonCommandHandler.CommandTaskCenterCommunicationBatchAddAccInfo(sender, eventArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        private void EmailOpenAccInfo()
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterCommunicationAccInfo(null, null);
        }

        /// <summary>
        /// 报关
        /// </summary>
        [CommandHandler(CommonCommandName.CommandCommunicationOrderCustoms)]
        public void Command_CommunicationOrderCustoms(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterOrderCustoms(sender, eventArgs);
        }


        /// <summary>
        /// 派车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationOrderTruck)]
        public void Command_CommunicationOrderTruck(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterOrderTruck(sender, eventArgs);
        }

        /// <summary>
        /// 提单列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationBlInfo)]
        public void Command_CommunicationBLInfo(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterCommunicationBLInfo();
        }


        /// <summary>
        ///  客户确认补料 邮件中文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationMailBlCopyToCustomerChs)]
        public void Command_CommunicationMailBLCopyToCustomerCHS(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailBlCopyToCustomer(false);
        }

        /// <summary>
        ///  客户确认补料 邮件英文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationMailBlCopyToCustomerEng)]
        public void Command_CommunicationMailBLCopyToCustomerENG(object sender, EventArgs eventArgs)
        {
            TransferData();
            OceanExportCommandHandler.MailBlCopyToCustomer(true);
        }

        /// <summary>
        /// 打开事件列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_OpenEvent)]
        public void Command_OpenEvent(object sender, EventArgs e)
        {
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanExportCommandHandler.Open(listPart);
            workItem.State["CurrentBaseBusinessPart"] = null;
        }
        /// <summary>
        /// 刷新列表的单条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_ListRefresh)]
        public void Command_ListRefresh(object sender, EventArgs e)
        {
            if (CurrentBaseBusinessPart == null)
            {
                return;
            }
            ListBaseBusinessPart listBase = CurrentBaseBusinessPart as ListBaseBusinessPart;
            OceanExportCommandHandler.ListRefresh(listBase.OperationType, CurrentListBaseBusinessPart);
            workItem.State["CurrentBaseBusinessPart"] = null;
        }

        /// <summary>
        /// 打开任务中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenterOpenTaskCenter")]
        public void Command_OpenTaskCenter(object sender, EventArgs e)
        {
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            BusinessOperationContext operationContext = new BusinessOperationContext();
            if (listPart != null)
            {
                operationContext.OperationID = listPart.OperationID;
                operationContext.OperationNO = listPart.OperationNo;
                operationContext.OperationType = listPart.OperationType;
                operationContext.CompanyID = listPart.CompanyID;
            }
            OceanExportCommandHandler.OpenTaskCenter(operationContext);
        }
        

        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_MailSoConfirmationToAgent)]
        public void Command_MailSoConfirmationToAgent(object sender, EventArgs e)
        {
            OceanExportCommandHandler.MailSoConfirmationToAgent();
        }

        /// <summary>
        /// 执行业务面板方法
        /// </summary>
        /// <param name="command">1:选择公司,2:查找,3:高级查找,4:关联,5:提交,6:账单列表</param>
        private void MailCenterToolBarCommand(ToolBarCommand command)
        {
            ListBaseBusinessPart listPart = CurrentListBaseBusinessPart as ListBaseBusinessPart;
            if (listPart != null)
            {
                switch (command)
                {
                    //执行选择公司
                    case ToolBarCommand.NewShipment:

                        break;
                    case ToolBarCommand.Relation:

                        break;
                    case ToolBarCommand.SelectedScope:
                        //listPart.EmailSelectScope();
                        break;
                    case ToolBarCommand.KeyWordSearch:
                        IsEnterKeySearch(true);
                        listPart.EmailQuery();
                        break;
                    case ToolBarCommand.AdvanceSearch:
                        listPart.EmailAdvanceQuery();
                        break;
                }
            }
        }

        

        #region 海出业务面板

        #region MailLink4in1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_EmailQuery")]
        public void Command_MailLink4in1_EmailQuery(object sender, EventArgs e)
        {
            MailCenterToolBarCommand(ToolBarCommand.KeyWordSearch);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seder"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_EmailAdvanceQuery")]
        public void Command_MailLink4in1_EmailAdvanceQuery(object seder, EventArgs e)
        {
            MailCenterToolBarCommand(ToolBarCommand.AdvanceSearch);
        }
        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_EmailAddSoForOE")]
        public void Command_MailLink4in1_EmailAddSoForOE(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                AddSoForOceanExport();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_EmailAddAE")]
        public void Command_MailLink4in1_EmailAddAE(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_EmailAddOther")]
        public void Command_MailLink4in1_EmailAddOther(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_SaveAssociateWithNormal")]
        public void Command_AssociateWithNormal(object sender, EventArgs e)
        {
            TransferData();
            MessageRelationParameter messageRelationParameter = new MessageRelationParameter
            {
                AssociateType = AssociateType.Normal,
                MailContactInfos = null,
                RelationType = MessageRelationType.Hand,
                UpdateDataType = UpdateDataType.MainForMessageID,
                MessageID = MailHelper.MessageID
            };
            CommonCommandHandler.InnerSaveOperationMessageRelation(messageRelationParameter);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_SaveAssociateWithStageSI")]
        public void Command_AssociateWithStageSI(object sender, EventArgs e)
        {
            TransferData();
            MessageRelationParameter messageRelationParameter = new MessageRelationParameter
            {
                AssociateType = AssociateType.WithStageSI,
                MailContactInfos = null,
                RelationType = MessageRelationType.Hand,
                UpdateDataType = UpdateDataType.MainForMessageID,
                MessageID = MailHelper.MessageID
            };
            CommonCommandHandler.InnerSaveOperationMessageRelation(messageRelationParameter);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_SaveAssociateWithStageSO")]
        public void Command_AssociateWithStageSO(object sender, EventArgs e)
        {
            TransferData();
            MessageRelationParameter messageRelationParameter = new MessageRelationParameter
            {
                AssociateType = AssociateType.WithStageSO,
                MailContactInfos = null,
                RelationType = MessageRelationType.Hand,
                UpdateDataType = UpdateDataType.MainForMessageID,
                MessageID = MailHelper.MessageID
            };
            CommonCommandHandler.InnerSaveOperationMessageRelation(messageRelationParameter);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_OpenAssistantPartWithNormal")]
        public void Command_MailLink4in1_OpenAssistantPartWithNormal(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.ShowContactPart(AssociateType.Normal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_OpenAssistantPartWithStageSI")]
        public void Command_MailLink4in1_OpenAssistantPartWithStageSI(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.ShowContactPart(AssociateType.WithStageSI);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_MailLink4in1_OpenAssistantPartWithStageSO")]
        public void Command_MailLink4in1_OpenAssistantPartWithStageSO(object sender, EventArgs e)
        {
            TransferData();
            CommonCommandHandler.ShowContactPart(AssociateType.WithStageSO);
        }


        #endregion

        #region 询价业务面板

        /// <summary>
        /// 确认业务询价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_ConfirmInquirePriceToShipment")]
        public void Command_ConfirmInquirePriceToShipment(object sender, EventArgs e) { }

        /// <summary>
        /// 取消确认业务询价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_Un_ConfirmInquirePriceToShipment")]
        public void Command_Un_ConfirmInquirePriceToShipment(object sender, EventArgs e) { }

        #endregion

        #endregion

        #region 内部沟通面板
        #region 通知客服有新订单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_NewBooking_OpenSO")]
        public void Command_NewBooking_OpenSO(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterOpenSo(null, null);
        }

        #endregion
        #region 通知商务员有新询价
        /// <summary>
        /// 打开询价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_InquireHistory")]
        public void Command_InquireHistory(object sender, EventArgs e)
        {
            OceanExportCommandHandler.CommandOpenInquireRatesListPart();
        }
        /// <summary>
        /// 海运询价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_GetInquireHistoryName")]
        public void Command_GetInquireHistoryName(object sender, EventArgs e)
        {
            OceanExportCommandHandler.GetReallyInquireHistoryName(InquierType.TruckingRates);
        }

        #endregion

        #endregion

        /// <summary>
        /// 通知客户确认补料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_EmailCenterCustomerConfirmedSI)]
        public void Command_EmailCenterCustomerConfirmeSI(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.MailCenterCustomerConfirmSI();
        }
        /// <summary>
        /// 打开商检单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationCommidityInspection)]
        public void CommandCommunicationCommidityInspection(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.MailCenterCommidityInspection();
        }

        /// <summary>
        /// 创建一个MBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandEmailCenterAddMbl)]
        public void CommandTaskCenterSilsMbl(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterSilsMbl(sender, e);
        }

        /// <summary>
        /// 打开CargoTracking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandCommunicationCargoTracking)]
        public void CommandCommunicationCargoTracking(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 创建一个HBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandEmailCenterAddHbl)]
        public void CommandTaskCenterSilsHbl(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandTaskCenterSilsHbl(sender, e);
        }


        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandLoadContainer)]
        public void Command_LoadContainer(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.LoadContainer();
        }

        /// <summary>
        /// 审核当前业务存在利润
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_OceanTrackings)]
        public void Command_OceanTrackings(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanExportCommandHandler.SetUpdateOceanTrackings(listPart.OperationID);
            OceanExportCommandHandler.ListRefresh(OperationType.OceanExport, listPart);
        }

        #region 待定按钮
        /// <summary>
        /// 确认AP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CfmAP)]
        public void Command_CfmAP(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 确认AR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CfmAR)]
        public void Command_CfmAR(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 接受代理修订的账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CommunicationAcptRev)]
        public void Command_CommunicationAcptRev(object sender, EventArgs e)
        {
            OceanExportCommandHandler.ShowReviseAccepte();
        }

        #endregion

        /// <summary>
        /// 通知客户订舱确认书(中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_MailSoConfirmationToCustomerCHS)]
        public void Command_MailSoConfirmationToCustomerCHS(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.MailSoConfirmationToCustomer(false);
        }
        /// <summary>
        ///  通知客户订舱确认书(英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_MailSoConfirmationToCustomerENG)]
        public void Command_MailSoConfirmationToCustomerENG(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.MailSoConfirmationToCustomer(true);
        }
        /// <summary>
        /// SOD成功以后发送邮件给客服(So Copy)
        /// </summary>
        [CommandHandler(CommonCommandName.Command_MainSOCustomerServiceSOD)]
        public void Command_MainSOCustomerServiceSOD(object sender, EventArgs e)
        {
            OceanExportCommandHandler.MainSOCustomerServiceSOD();
            OceanExportCommandHandler.ListRefresh(OperationType.OceanExport, CurrentListBaseBusinessPart);
        }



        /// <summary>
        /// SOD成功以后发送邮件给客服(文档列表上传附件)
        /// </summary>
        [CommandHandler(CommonCommandName.Command_MainSOCustomerServiceSODDocument)]
        public void Command_MainSOCustomerServiceSODDocument(object sender, EventArgs e)
        {
            OceanExportCommandHandler.MainSOCustomerServiceSODDocument();
            OceanExportCommandHandler.ListRefresh(OperationType.OceanExport, CurrentListBaseBusinessPart);
        }


        /// <summary>
        /// 邮件订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CommunicationMailBooking)]
        public void Command_CommunicationMailBooking(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommunicationMailBooking();
        }
        
        

        #region    取消订舱 和 订舱失败
        /// <summary>
        /// 订舱失败 爆仓
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_FailureBlastingWarehouse)]
        public void Command_FailureBlastingWarehouse(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandFailureBlastingWarehouse();
        }

        /// <summary>
        /// 订舱失败 柜型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_FailureCubicleType)]
        public void Command_FailureCubicleType(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandFailureCubicleType();
        }

        /// <summary>
        /// 订舱失败 船期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_FailureShippingDate)]
        public void Command_FailureShippingDate(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandFailureShippingDate();
        }

        /// <summary>
        /// 订舱失败 其他
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_FailureOther)]
        public void Command_FailureOther(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandFailureOther();
        }

        /// <summary>
        /// 取消订舱 货源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CancelSourcing)]
        public void Command_CancelSourcing(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandCancelSourcing();
        }

        /// <summary>
        /// 取消订舱 价格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CancelPrice)]
        public void Command_CancelPrice(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandCancelPrice();
        }

        /// <summary>
        /// 取消订舱 贸易
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CancelTrade)]
        public void Command_CancelTrade(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandCancelTrade();
        }


        /// <summary>
        /// 取消订舱 其他
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_CancelOther)]
        public void Command_CancelOther(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandCancelOther();
        }
        #endregion
        /// <summary>
        /// 恢复订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_RestoreBooking)]
        public void Command_RestoreBooking(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.RestoreBooking("SOC");
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listPart);
        }


        /// <summary>
        /// 订舱员恢复订舱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_ClerkRestoreBooking)]
        public void Command_ClerkRestoreBooking(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.RestoreBooking("SOF");
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listPart);
        }


        /// <summary>
        /// 快捷方式打开任务中心k
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_QuickOpenTaskCenter)]
        public void Command_QuickOpenTaskCenter(object sender, EventArgs e)
        {
            OceanExportCommandHandler.QuickOpenTaskCenter();
        }



        #endregion 海出的处理命令（命名规则以Command_OceanExport_事件名称）

        #region 海进的处理命令（命名规则以 OceanImport_事件名称）
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_Download)]
        public void OceanImport_Download(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandDownload(sender, e);
        }

        /// <summary>
        /// 海进编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_Edit)]
        public void OceanImport_Edit(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandEdit(sender, e);
        }

        /// <summary>
        ///海进业务取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_Cancel)]
        public void OceanImport_Cancel(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandCancel(sender, e);
        }


        /// <summary>
        /// 海进业务转移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_Transfer)]
        public void OceanImport_Transfer(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandTransfer(sender, e);
        }

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_PrintArrivalNotice)]
        public void OceanImport_PrintArrivalNotice(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailAnToCustomer(true);
        }

        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_ReleaseOrder)]
        public void OceanImport_ReleaseOrder(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandReleaseOrder(sender, e);
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_PrintProfit)]
        public void OceanImport_PrintProfit(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandPrintProfit(sender, e);
        }

        /// <summary>
        /// 打印工作表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_PrintWorkSheet)]
        public void OceanImport_PrintWorkSheet(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandPrintWorkSheet(sender, e);
        }

        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_PrintForwardingBill)]
        public void OceanImport_PrintForwardingBill(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandPrintForwardingBill(sender, e);
        }

        /// <summary>
        /// 打印出口业务信息报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_PrintExportBusinessInfo)]
        public void OceanImport_PrintExportBusinessInfo(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandPrintExportBusinessInfo(sender, e);
        }


        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_Bill)]
        public void OceanImport_Bill(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CommandOpenBill(sender, e);
        }

        /// <summary>
        /// 打印提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_CargoBook)]
        public void OceanImport_CargoBook(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailPickUpToCustomer(true);
        }

        /// <summary>
        /// 签收放单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_ReceiveRN)]
        public void OceanImport_ReceiveRN(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandReceiveRN(listPart);
        }

        /// <summary>
        /// 申请放单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_ApplyReleaseCargo)]
        public void OceanImport_ApplyReleaseCargo(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandApplyReleaseCargo(listPart);
        }

        /// <summary>
        /// 同意放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_AgreeRC)]
        public void OceanImport_AgreeRC(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandOceanImportAgreeRC(listPart);
        }
        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_Delivery)]
        public void OceanImport_Delivery(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandOceanImportDelivery(listPart);
        }
        /// <summary>
        /// 异常放货申请
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_ExceptionReleaseRC)]
        public void OceanImport_ExceptionReleaseRC(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandExceptionReleaseRC(listPart);
        }
        /// <summary>
        /// 取消放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_CancelDelivery)]
        public void OceanImport_CancelDelivery(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandOceanImportCancelDelivery(listPart);
        }
        /// <summary>
        /// 取消同意放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_CancelAgreeRC)]
        public void OceanImport_CancelAgreeRC(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandOceanImportCancelAgreeRC(listPart);
        }
        /// <summary>
        /// 取消申请放单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_CancelApplyRC)]
        public void OceanImport_CancelApplyRC(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.CommandCancelApplyReleaseCargo(listPart);
        }


        /// <summary>
        /// 发送到港通知书给客户 (中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailANToCustomerCHS)]
        public void OceanImport_MailANToCustomerCHS(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailAnToCustomer(false);
        }
        /// <summary>
        /// 发送到港通知书给客户 (英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailANToCustomerENG)]
        public void OceanImport_MailANToCustomerENG(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailAnToCustomer(true);
        }

        /// <summary>
        /// 发送提货通知书给客户 (中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailPickUpToCustomerCHS)]
        public void OceanImport_MailPickUpToCustomerCHS(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailPickUpToCustomer(false);
        }
        /// <summary>
        /// 发送提货通知书给客户 (英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailPickUpToCustomerENG)]
        public void OceanImport_MailPickUpToCustomerENG(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailPickUpToCustomer(true);
        }


        /// <summary>
        ///  上传海出附件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandName.OceanImport_UploadAttachment)]
        public void OceanImport_UploadAttachment(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.UploadingImportAttachment(ClientUtility.GetCurrentMailSubject(), ClientProperties.ObjMailItem);
        }

        /// <summary>
        ///  上传海出AN附件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_UploadANAttachment)]
        public void OceanImport_CommunicationUploadANAttachment(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.UploadingAN(string.Empty, null);
        }

        /// <summary>
        ///  新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_AddOrder)]
        public void OceanImport_AddOrder(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.AddOrder();
        }


        /// <summary>
        ///  复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_CopyOiBusiness)]
        public void OceanImport_CopyOiBusiness(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.CopyOiBusiness();
        }

        /// <summary>
        ///  收到MBL正本
        /// </summary>
        [CommandHandler(CommonCommandName.OceanImport_OiomblRcved)]
        public void OceanImport_OiomblRcved(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.OiomblRcved(listPart);
        }

        /// <summary>
        ///  财务发送MBL
        /// </summary>
        [CommandHandler(CommonCommandName.OceanImport_MAILDMBL)]
        public void OceanImport_MAILDMBL(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.MailDMBL(listPart);
        }

        /// <summary>
        ///  财务关帐
        /// </summary>
        [CommandHandler(CommonCommandName.OceanImport_State_ACCLOS)]
        public void OceanImport_State_ACCLOS(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.OIStateAccountingClose(listPart);
        }

        /// <summary>
        ///  收到正本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_OioblRcved)]
        public void OceanImport_OioblRcved(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.OioblRcved(listPart);
        }

        /// <summary>
        ///  付款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_OiPayment)]
        public void OceanImport_OiPayment(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.OiPayment(listPart);
        }


        /// <summary>
        ///  发送催款邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailPayNt)]
        public void OceanImport_MailPayNt(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.PayNtMail();
        }

        /// <summary>
        ///  修改放单指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_UpdateIsReleaseOrderRequired)]
        public void OceanImport_UpdateIsReleaseOrderRequired(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.UpdateIsReleaseOrderRequired(listPart);
        }


        /// <summary>
        ///  催港前放单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_NoticeRelease)]
        public void OceanImport_NoticeRelease(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.NoticeRelease(listPart);
        }


        /// <summary>
        /// 催代理分发文件(中文版)
        /// </summary>
        [CommandHandler(CommonCommandName.OceanImport_MailOverseaAgentCHS)]
        public void OceanImport_MailOverseaAgentCHS(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailOverseaAgent(false);
        }

        /// <summary>
        /// 催代理分发文件(英文版)
        /// </summary>
        [CommandHandler(CommonCommandName.OceanImport_MailOverseaAgentENG)]
        public void OceanImport_MailOverseaAgentENG(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailOverseaAgent(true);
        }

        /// <summary>
        /// ETA 前5天催清关文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        [CommandHandler(CommonCommandName.OceanImport_MailCustomrequest)]
        public void OceanImport_MailCustomrequest(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailCustomrequest();
        }


        /// <summary>
        /// ETA 变更通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailEtachange)]
        public void OceanImport_MailEtachange(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailEtachange();
        }

        /// <summary>
        /// LFD 通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailLfdnotice)]
        public void OceanImport_MailLfdnotice(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailLfdnotice();
        }

        /// <summary>
        /// LFD 前2工作日后每天提柜提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailContainerpickupreminder)]
        public void OceanImport_MailContainerpickupreminder(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailContainerpickupreminder();
        }

        /// <summary>
        /// 提柜后第2天开始每天还空提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailEmptyreturnnotice)]
        public void OceanImport_MailEmptyreturnnotice(object sender, EventArgs e)
        {
            TransferData();
            OceanImportCommandHandler.MailEmptyreturnnotice();
        }

        /// <summary>
        /// CONTAINER AVAILABLE FOR PICK UP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OceanImport_MailContaineravailableforpickup)]
        public void OceanImport_MailContaineravailableforpickup(object sender, EventArgs e)
        {
            TransferData();
            IListBaseBusinessPart listPart = CurrentListBaseBusinessPart;
            OceanImportCommandHandler.MailContaineravailableforpickup(listPart);
        }

        #endregion 海进的处理命令（命名规则以 OceanImport_事件名称）

        #region 空出的处理命令（命名规则以 AirExport_事件名称）

        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_AddData)]
        public void AirExport_AddData(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportAddData(sender, e);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_CopyData)]
        public void AirExport_CopyData(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportCopyData(sender, e);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_EditData)]
        public void AirExport_EditData(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportEditData(sender, e);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_CancelData)]
        public void AirExport_CancelData(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportCancelData(sender, e);
            Command_ListRefresh(sender, e);
        }

        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_ReplyAgent)]
        public void AirExport_ReplyAgent(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportReplyAgent(sender, e);
        }

        /// <summary>
        /// 打开提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_OpenBl)]
        public void AirExport_OpenBl(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportOpenBl(sender, e);
        }

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_OpenBill)]
        public void AirExport_OpenBill(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportOpenBill(sender, e);
        }

        /// <summary>
        /// 业务联单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_PrintOrder)]
        public void AirExport_PrintOrder(object sender, EventArgs e)
        {
            TransferData();
            AirExportCommandHandler.AirExportPrintOrder(sender, e);
        }

        #endregion 空出的处理命令（命名规则以 AirExport_事件名称）

        #region 空进的处理命令（命名规则以   AirImport_事件名称）

        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_AddBooking)]
        public void AirImport_AddBooking(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportAddBooking(sender, e);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirExport_CopyBooking)]
        public void AirExport_CopyBooking(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportCopyBooking(sender, e);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_AiDownLoad)]
        public void AirImport_AiDownLoad(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportDownLoad(sender, e);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_EditBooking)]
        public void AirImport_EditBooking(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportEditBooking(sender, e);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_CancelBooking)]
        public void AirImport_CancelBooking(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportCancelBooking(sender, e);
            Command_ListRefresh(sender, e);
        }

        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_BusinessTransfer)]
        public void AirImport_BusinessTransfer(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportBusinessTransfer(sender, e);
        }

        /// <summary>
        /// 到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_PrintArrivalNotice)]
        public void AirImport_PrintArrivalNotice(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportPrintArrivalNotice(sender, e);
        }

        /// <summary>
        /// 放货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_PrintReleaseOrder)]
        public void AirImport_PrintReleaseOrder(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportPrintReleaseOrder(sender, e);
        }

        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_PrintProfit)]
        public void AirImport_PrintProfit(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportPrintProfit(sender, e);
        }


        /// <summary>
        /// 打印Authority To Make Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_PrintAuthority)]
        public void AirImport_PrintAuthority(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportPrintAuthority(sender, e);
        }


        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_OpenBill)]
        public void AirImport_OpenBill(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportOpenBill(sender, e);
        }

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_OpenCargoBook)]
        public void AirImport_OpenCargoBook(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportOpenCargoBook(sender, e);
        }

        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_AiDelivery)]
        public void AirImport_AiDelivery(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportDelivery(sender, e);
        }

        /// <summary>
        /// 取消放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.AirImport_CancelDelivery)]
        public void AirImport_CancelDelivery(object sender, EventArgs e)
        {
            TransferData();
            AirImportCommandHandler.AirImportDelivery(sender, e);
        }

        #endregion 空进的处理命令（命名规则以 AirImport_事件名称）

        #region 其它业务的处理命令（命名规则以Command_Other_事件名称）
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_AddData)]
        public void OtherBusiness_AddData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessAddData(sender, e);
        }


        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_CopyData)]
        public void OtherBusiness_CopyData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessCopyData(sender, e);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_EditData)]
        public void OtherBusiness_EditData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessEditData(sender, e);
        }


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_CancelData)]
        public void OtherBusiness_CancelData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessCancelData(sender, e);
        }

        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_Bill)]
        public void OtherBusiness_Bill(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessBill(sender, e);
        }

        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_VerifiSheet)]
        public void OtherBusiness_VerifiSheet(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessVerifiSheet(sender, e);
        }

        /// <summary>
        /// 业务联系单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_PrintOrder)]
        public void OtherBusiness_PrintOrder(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessPrintOrder(sender, e);
        }

        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_Profit)]
        public void OtherBusiness_Profit(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessProfit(sender, e);
        }

        /// <summary>
        /// 拖车服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.OtherBusiness_PickUp)]
        public void OtherBusiness_PickUp(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessPickUp(sender, e);
        }




        #endregion 其它业务的处理命令（命名规则以Command_Other_事件名称）

        #region 其它业务-电商物流的处理命令（命名规则以Command_Other_ECommerce_事件名称）
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Other_ECommerce_AddData)]
        public void Other_ECommerce_AddData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessECAddData(sender, e);
        }


        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Other_ECommerce_CopyData)]
        public void Other_ECommerce_CopyData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessECCopyData(sender, e);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Other_ECommerce_EditData)]
        public void Other_ECommerce_EditData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessECEditData(sender, e);
        }


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Other_ECommerce_CancelData)]
        public void Other_ECommerce_CancelData(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessECCancelData(sender, e);
        }

        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Other_ECommerce_Bill)]
        public void Other_ECommerce_Bill(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessECBill(sender, e);
        }

        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Other_ECommerce_VerifiSheet)]
        public void Other_ECommerce_VerifiSheet(object sender, EventArgs e)
        {
            TransferData();
            OtherBusinessCommandHandler.OtherBusinessECVerifiSheet(sender, e);
        }

        #endregion 其它业务-电商物流的处理命令（命名规则以Command_Other_ECommerce_事件名称）

        #region 拖车的处理命令（命名规则以Command_Truck_事件名称）

        #endregion 拖车的处理命令（命名规则以Command_Truck_事件名称）

        #region 报关的处理命令（命名规则以Command_Customs_事件名称）

        #endregion 报关的处理命令（命名规则以Command_Customs_事件名称）

        #region 内贸的处理命令（命名规则以Command_Internal_事件名称）

        #endregion 内贸的处理命令（命名规则以Command_Internal_事件名称）

        #region 不确定业务的处理命令（命名规则以Command_Unknown_事件名称）

        #endregion 不确定业务的处理命令（命名规则以Command_Unknown_事件名称）

        #region  邮件中心的处理命令（命名规则以CommandMail_事件名称）
        /// <summary>
        /// 此业务的客户历史邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandMail_MailofCustomer)]
        public void CommandMail_MailofCustomer(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandMailMailofCustomer();
        }

        /// <summary>
        /// 此业务的承运人历史邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandMail_HistoryofCarrier)]
        public void CommandMail_HistoryofCarrier(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandMailHistoryofCarrier();
        }
        /// <summary>
        /// 此业务的代理历史邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandMail_HistoryofAgent)]
        public void CommandMail_HistoryofAgent(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandMailHistoryofAgent();
        }
        /// <summary>
        /// 此业务的历史邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandMail_MailHistory)]
        public void CommandMail_MailHistory(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandMailMailHistory();
        }
        /// <summary>
        /// 客户的历史邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandMail_HistoryofCustomers)]
        public void CommandMail_HistoryofCustomers(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandMailHistoryofCustomers();
        }
        /// <summary>
        /// 业务的承运人历史邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandMail_HistoryofCarriers)]
        public void CommandMail_HistoryofCarriers(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandMailHistoryofCarriers();
        }

        /// <summary>
        /// 业务的代理历史邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.CommandMail_HistoryofAgents)]
        public void CommandMail_HistoryofAgents(object sender, EventArgs e)
        {
            TransferData();
            OceanExportCommandHandler.CommandMailHistoryofAgents();
        }
        #endregion

        #region  询价的处理命令
        /// <summary>
        /// 邮件-询问人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_EmailMailtoAskpeople)]
        public void Command_EmailMailtoAskpeople(object sender, EventArgs e)
        {
            OceanExportCommandHandler.EmailMailtoAskpeople();
        }
        /// <summary>
        /// 邮件-承运人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandName.Command_EmailMailtoCarrier)]
        public void Command_EmailMailtoCarrier(object sender, EventArgs e)
        {
            OceanExportCommandHandler.EmailMailtoCarrier();
        }
        #endregion

        #region Comment Code
        ///// <summary>
        ///// 邮件与业务关联
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="eventArgs"></param>
        //[CommandHandler(CommonCommandName.CommandEmailCenterOperationMessageRelation)]
        //public void Command_EmailCenterOperationMessageRelation(object sender, EventArgs eventArgs)
        //{

        //    CommonMethod(4);
        //}

        ///// <summary>
        ///// 提交
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //[CommandHandler(CommonCommandName.Command_DataCommit)]
        //public void Command_BusinessPart_DataSubmit(object sender, EventArgs e)
        //{
        //    CommonMethod(5);
        //}
        //#region 未知联系人工具栏执行方法

        //[CommandHandler("Command_Unknown_EmailAddSOForOE")]
        //public void Command_Unknown_EmailAddSOForOE(object sender, EventArgs e)
        //{
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        AddSoForOceanExport();
        //    }
        //}

        //[CommandHandler("Command_Unknown_EmailQuery")]
        //public void Command_Unknown_EmailQuery(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(0);
        //    Query();
        //}
        //void Query()
        //{
        //    ListBaseBusinessPart currentPart = this.CurrentListBaseBusinessPart as ListBaseBusinessPart;
        //    if (currentPart != null)
        //    {
        //        using (new CursorHelper(Cursors.WaitCursor))
        //        {
        //            currentPart.EmailQuery();
        //        }
        //    }
        //}

        //[CommandHandler("Command_Unknown_EmailQuery_1")]
        //public void Command_Unknown_EmailQuery_1(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(1);
        //    Query();
        //}

        //[CommandHandler("Command_CancelSO")]
        //public void Command_CancelSO(object sender, EventArgs e)
        //{
        //    workItem.State["CancelOperation"] = true;
        //    TransferData();
        //    OceanExportCommandHandler.CancelOrder();
        //}

        //[CommandHandler("Command_Unknown_EmailAdvanceQuery")]
        //public void Command_Unknown_EmailAdvanceQuery(object sender, EventArgs e)
        //{
        //    CommonMethod(3);
        //}
        //[CommandHandler("Command_Unknown_EmailSelectCompany")]
        //public void Command_Unknown_EmailSelectCompany(object sender, EventArgs e)
        //{
        //    CommonMethod(1);
        //}

        //#endregion

        //#region 客户面板工具栏执行方法
        //[CommandHandler("Command_Customer_EmailSelectCompany")]
        //public void Command_Customer_EmailSelectCompany(object sender, EventArgs e)
        //{
        //    CommonMethod(1);
        //}

        //[CommandHandler("Command_Customer_EmailQuery")]
        //public void Command_Customer_EmailQuery(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(0);
        //    CommonMethod(2);
        //}
        //[CommandHandler("Command_Customer_EmailQuery_1")]
        //public void Command_Customer_EmailQuery_1(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(1);
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_Customer_EmailAdvanceQuery")]
        //public void Command_Customer_EmailAdvanceQuery(object seder, EventArgs e)
        //{
        //    CommonMethod(3);
        //}

        //[CommandHandler("Command_Customer_EmailCenterOperationMessageRelation")]
        //public void Command_Customer_EmailCenterOperationMessageRelation(object sender, EventArgs e)
        //{
        //    CommonMethod(4);
        //}

        //[CommandHandler(CommonCommandName.CommandCommunicationBlInfo)]
        //public void Command_CommunicationBlInfo(object sender, EventArgs e)
        //{
        //    CommonMethod(6);
        //}


        //#endregion

        //#region 承运人面板执行方法

        //[CommandHandler("Command_Carrier_EmailSelectCompany")]
        //public void Command_Carrier_EmailSelectCompany(object sender, EventArgs e)
        //{
        //    CommonMethod(1);
        //}

        //[CommandHandler("Command_Carrier_EmailQuery")]
        //public void Command_Carrier_EmailQuery(object sender, EventArgs e)
        //{
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_Carrier_EmailAdvanceQuery")]
        //public void Command_Carrier_EmailAdvanceQuery(object sender, EventArgs e)
        //{
        //    CommonMethod(3);
        //}

        //[CommandHandler("Command_Carrier_EmailCenterOperationMessageRelation")]
        //public void Command_Carrier_EmailCenterOperationMessageRelation(object sender, EventArgs e)
        //{
        //    ListBaseBusinessPart listPart = this.CurrentListBaseBusinessPart as ListBaseBusinessPart;
        //    if (listPart == null || listPart.CurrentRow == null)
        //    {
        //        return;
        //    }
        //    CommonMethod(4);
        //}
        //[CommandHandler("Command_Carrier_EmailSubmit")]
        //public void Command_Carrier_EmailSubmit(object sender, EventArgs e)
        //{
        //    CommonMethod(5);
        //}

        //#endregion

        //#region 承运人的分配SO面板执行方法
        //[CommandHandler("Command_CarrierSO_EmailSelectCompany")]
        //public void Command_CarrierSO_EmailSelectCompany(object sender, EventArgs e)
        //{
        //    CommonMethod(1);
        //}

        //[CommandHandler("Command_CarrierSO_EmailQuery")]
        //public void Command_CarrierSO_EmailQuery(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(0);
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_CarrierSO_EmailQuery_1")]
        //public void Command_CarrierSO_EmailQuery_1(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(1);
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_CarrierSO_EmailAdvanceQuery")]
        //public void Command_CarrierSO_EmailAdvanceQuery(object sender, EventArgs e)
        //{
        //    CommonMethod(3);
        //}

        //[CommandHandler("Command_CarrierSO_EmailCenterOperationMessageRelation")]
        //public void Command_CarrierSO_EmailCenterOperationMessageRelation(object sender, EventArgs e)
        //{
        //    CommonMethod(4);
        //}

        //[CommandHandler("Command_CarrierSO_EmailSubmit")]
        //public void Command_CarrierSO_EmailSubmit(object sender, EventArgs e)
        //{
        //    CommonMethod(5);
        //    //OceanExportCommandHandler.SaveEventMemo(listPart, "SOD", "SO", (LocalData.IsEnglish ? "SO is Done" : "订舱成功"), (LocalData.IsEnglish ? "SO is Done" : "订舱成功"));
        //}

        //[CommandHandler("Command_CarrierSO_EmailAutoFillSO")]
        //public void Command_CarrierSO_EmailAutoFillSO(object sender, EventArgs e)
        //{
        //    SOSetting.Current.AutoFillSO = SOChecked();
        //}
        //[CommandHandler("Command_CarrierSO_EmailNotifyCS")]
        //public void Command_CarrierSO_EmailNotifyCS(object sender, EventArgs e)
        //{
        //    SOSetting.Current.NotfiyCS = SOChecked();
        //}

        //private bool SOChecked()
        //{
        //    return workItem.State["SOSetting"] == null || bool.Parse(workItem.State["SOSetting"].ToString());
        //}

        //#endregion

        //#region 承运人的上传MBL面板执行方法
        //[CommandHandler("Command_CarrierMBL_EmailSelectCompany")]
        //public void Command_CarrierMBL_EmailSelectCompany(object sender, EventArgs e)
        //{
        //    CommonMethod(1);
        //}

        //[CommandHandler("Command_CarrierMBL_EmailQuery")]
        //public void Command_CarrierMBL_EmailQuery(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(0);
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_CarrierMBL_EmailQuery_1")]
        //public void Command_CarrierMBL_EmailQuery_1(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(1);
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_CarrierMBL_EmailAdvanceQuery")]
        //public void Command_CarrierMBL_EmailAdvanceQuery(object sender, EventArgs e)
        //{
        //    CommonMethod(3);
        //}

        //[CommandHandler("Command_CarrierMBL_EmailCenterOperationMessageRelation")]
        //public void Command_CarrierMBL_EmailCenterOperationMessageRelation(object sender, EventArgs e)
        //{
        //    CommonMethod(4);
        //}
        //[CommandHandler("Command_CarrierMBL_EmailSubmit")]
        //public void Command_CarrierMBL_EmailSubmit(object sender, EventArgs e)
        //{
        //    CommonMethod(5);
        //    //OceanExportCommandHandler.SaveEventMemo(listPart, "MBLR", "SI", (LocalData.IsEnglish ? "Received MBL Copy" : "收到MBL附件"), (LocalData.IsEnglish ? "MBL Copy is received from the carrier" : "承运人已收到MBL附件"));

        //}
        //#endregion

        //#region 承运人的上传A/P面板执行方法
        //[CommandHandler("Command_CarrierAP_EmailSelectCompany")]
        //public void Command_CarrierAP_EmailSelectCompany(object sender, EventArgs e)
        //{
        //    CommonMethod(1);

        //}

        //[CommandHandler("Command_CarrierAP_EmailQuery")]
        //public void Command_CarrierAP_EmailQuery(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(0);
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_CarrierAP_EmailQuery_1")]
        //public void Command_CarrierAP_EmailQuery_1(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(1);
        //    CommonMethod(2);
        //}

        //[CommandHandler("Command_CarrierAP_EmailAdvanceQuery")]
        //public void Command_CarrierAP_EmailAdvanceQuery(object sender, EventArgs e)
        //{
        //    CommonMethod(3);
        //}

        //[CommandHandler("Command_CarrierAP_EmailCenterOperationMessageRelation")]
        //public void Command_CarrierAP_EmailCenterOperationMessageRelation(object sender, EventArgs e)
        //{
        //    CommonMethod(4);
        //}
        //[CommandHandler("Command_CarrierAP_EmailSubmit")]
        //public void Command_CarrierAP_EmailSubmit(object sender, EventArgs e)
        //{
        //    CommonMethod(5);
        //    //OceanExportCommandHandler.SaveEventMemo(listPart, "APR", "Finance", (LocalData.IsEnglish ? "Received AP" : "收到应收账单"), (LocalData.IsEnglish ? "Credit Note is received from the carrier" : "承运人已收到应收账单"));
        //}

        //[CommandHandler("Command_CarrierAP_CommunicationAccInfo")]
        //public void Command_CarrierAP_CommunicationAccInfo(object sender, EventArgs e)
        //{
        //    CommonMethod(6);
        //}

        //#endregion

        //#region 承运人上A/N面板执行方法
        //[CommandHandler("Command_CarrierAN_EmailQuery_1")]
        //public void Command_CarrierAN_EmailQuery_1(object sender, EventArgs e)
        //{
        //    IsEnterKeySearch(1);
        //    CommonMethod(2);
        //}
        //#endregion
        //#region  Old 移除邮件中心的下拉框选择
        //[CommandHandler("Command_MailLink4in1_EmailSelectScope")]
        //public void Command_Customer_EmailSelectScope(object sender, EventArgs e)
        //{
        //    MailCenterToolBarCommand(ToolBarCommand.SelectedScope);
        //}
        //#endregion 
        #endregion
    }
}

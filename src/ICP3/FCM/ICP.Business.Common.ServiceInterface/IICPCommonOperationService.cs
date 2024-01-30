using ICP.DataCache.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Business.Common.ServiceInterface
{
    /// <summary>
    /// 提供给外界调用的公共服务接口
    /// </summary>
    [ICPServiceHost]
    [ServiceContract]
    [ServiceKnownType(typeof(BusinessOperationParameter))]
    [ServiceKnownType(typeof(OceanBookingInfo))]
    [ServiceKnownType(typeof(AirBookingInfo))]
    [ServiceKnownType(typeof(AirBusinessInfo))]
    [ServiceKnownType(typeof(OceanHBLInfo))]
    [ServiceKnownType(typeof(EventObjects))]
    [ServiceKnownType(typeof(MailOIBusinessDataObjects))]
    public interface IICPCommonOperationService
    {
        #region 业务更新相关
        /// <summary>
        /// 新增业务
        /// </summary>
        /// <param name="values"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        [OperationContract]
        void AddBusiness(Dictionary<string, object> values, OperationType operationType, FormType formType);

        /// <summary>
        /// 编辑业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        [OperationContract]
        void EditBusiness(EditPartShowCriteria showCriteria, Dictionary<string, object> values, OperationType operationType, FormType formType);

        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        [OperationContract]
        void CopyBusiness(EditPartShowCriteria showCriteria, Dictionary<string, object> values, OperationType operationType, FormType formType);

        /// <summary>
        /// 取消业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        [OperationContract]
        void CancelBusiness(EditPartShowCriteria showCriteria, OperationType operationType, FormType formType);

        /// <summary>
        /// 批量取消业务
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="isCancel"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        [OperationContract]
        void BatchCancelBusiness(Guid[] operationIds, bool isCancel, OperationType operationType, FormType formType);
        
        #endregion

        #region 业务打印
        /// <summary>
        /// 打印订舱确认书(宁波)
        /// </summary>
        /// <param name="operationID">业务ID</param>
        [OperationContract]
        void PrintOEBookingConfirmation4NB(Guid operationID); 
        #endregion

        #region CSP Booking
        /// <summary>
        /// CSPBooking Download
        /// </summary>
        [OperationContract]
        void CSPBookingDownload();
        #endregion
        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void EditBooking(EditPartShowCriteria showCriteria, Dictionary<string, object> values);
        /// <summary>
        /// 新增MBL
        /// </summary>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void AddMBL(Dictionary<string, object> dicParameters);
        /// <summary>
        /// 打开派车单
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationNo"></param>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void OpenTruckOrder(Guid operationId, string operationNo, Dictionary<string, object> dicParameters);
        /// <summary>
        /// 新增HBL
        /// </summary>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void AddHBL(Dictionary<string, object> dicParameters);
        /// <summary>
        /// 预配提单
        /// </summary>
        /// <param name="mblNo"></param>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void DeclarationBL(string mblNo,Dictionary<string, object> dicParameters);
        /// <summary>
        /// 新增HBL
        /// </summary>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void AddDeclareHBL(Dictionary<string, object> dicParameters);
        /// <summary>
        /// 编辑MBL
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="mblNo"></param>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void EditMBL(string operationNo, string mblNo, Dictionary<string, object> dicParameters);
        /// <summary>
        /// 编辑HBL
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="hblNo"></param>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void EditHBL(string operationNo, string hblNo, Dictionary<string, object> dicParameters);
        /// <summary>
        /// 编辑DeclareHBL
        /// </summary>
        /// <param name="hblid"></param>
        /// <param name="hblNo"></param>
        /// <param name="dicParameters"></param>
        [OperationContract]
        void EditDeclareHBL(Guid hblid, string hblNo, Dictionary<string, object> dicParameters);
        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="values"></param>
        [OperationContract]
        void AddBooking(Dictionary<string, object> values);
        /// <summary>
        /// 复制海出业务
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        [OperationContract]
        BusinessOperationContext OceanExportCopyBusiness(BusinessOperationContext context);
        /// <summary>
        /// 取消或激活海出业务
        /// </summary>
        /// <param name="operationId"></param>
        [OperationContract]
        SingleResult CancelOEBusiness(Guid operationId);
        /// <summary>
        /// 取消或激活海出业务
        /// </summary>
        /// <param name="operationIds">业务ID集合</param>
        [OperationContract]
        ManyResult OECancelBusinesss(Guid[] operationIds);
        /// <summary>
        /// 取消或激活海出业务
        /// </summary>
        /// <param name="operationIds">业务ID集合</param>
        /// <param name="IsCancel">是否作废</param>
        [OperationContract(Name = "OECancelBusinesss2")]
        ManyResult OECancelBusinesss(Guid[] operationIds,bool IsCancel);
        /// <summary>
        /// 打开报关单
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="values"></param>
        [OperationContract]
        void OpenCustomsOrder(Guid operationId, Dictionary<string, object> values);
        /// <summary>
        /// 打印提单
        /// </summary>
        [OperationContract]
        void PrintBillOfLoading(Guid blId);
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID，等于业务ID</param>
        [OperationContract]
        void PrintBookingProfit(Guid oceanBookingId);
        /// <summary>
        /// EDI操作
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID=业务Id</param>
        /// <returns></returns>
        [OperationContract]
        bool EBookingCall(List<Guid> oceanBookingId);

        /// <summary>
        /// EDI订舱确认
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID=业务Id</param>
        /// <returns></returns>
        [OperationContract]
        bool EBookingConfirm(List<Guid> oceanBookingId);

        /// <summary>
        /// EDI数据显示
        /// </summary>
        /// <param name="ediType">类型</param>
        /// <param name="IDS">IDS</param>
        /// <returns></returns>
        [OperationContract]
        bool ExecGetEDIDataSourceForNBEDIInfos(int ediType, Guid[] IDS);
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="values"></param>
        [OperationContract]
        void AddOrder(Dictionary<string, object> values);

        /// <summary>
        /// 打开申请代理的页面
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationType"></param>
        /// <param name="values"></param>
        [OperationContract]
        void ReplyAgent(Guid operationId, OperationType operationType, Dictionary<string, object> values);

        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void CopyBooking(EditPartShowCriteria showCriteria, Dictionary<string, object> values);

        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="operationNo">业务NO</param>
        /// <param name="mblNo">MBL No</param>
        [OperationContract]
        void LoadShip(string operationNo, string mblNo);
        /// <summary>
        /// 获取发送海出相关邮件时的验证信息
        /// </summary>
        /// <param name="oceanBooking"></param>
        /// <returns></returns>
        [OperationContract]
        string GetEmailSendValidationInfo(OceanBookingInfo oceanBooking);

        /// <summary>
        /// 打印业务联单
        /// </summary>
        [OperationContract]
        void PrintOrder(Guid orderId,Guid companyID);

        /// <summary>
        /// 订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID，等于业务ID</param>
        [OperationContract]
        void PrintBookingConfirm(Guid oceanBookingId);

        /// <summary>
        /// 弹出分发文档界面
        /// </summary>
        /// <param name="context"></param>
        [OperationContract]
        void DispatchDocument(BusinessOperationContext context);

        /// <summary>
        /// 保存事件日志
        /// </summary>
        /// <param name="eventobject"></param>
        [OperationContract]
        void SaveEventInfo(EventObjects eventobject);
        /// <summary>
        /// 打开指定业务号下的提单列表
        /// </summary>
        /// <param name="operationNo"></param>
        [OperationContract]
        void OpenBillOfLoadingList(string operationNo);
        /// <summary>
        /// 发送邮件给订舱员申请订舱 
        /// </summary>
        /// <param name="operationId">订单ID,业务Id</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailBookingClerkForApplySO(Guid operationId);
        /// <summary>
        /// 发送邮件给客户确认提单无误,补料OK
        /// </summary>
        /// <param name="isEnglish">是否发送英文版本</param>
        /// <param name="oceanBookingId"></param>
        /// <param name="oceanHblInfo"></param>
        /// <param name="oceanMblInfo"></param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailCustomerAskForConfirmSI(bool isEnglish, Guid oceanBookingId, OceanHBLInfo oceanHblInfo,
                                          OceanMBLInfo oceanMblInfo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <returns></returns>
        [OperationContract]
        string MailALLBLCopyToAgent(bool isEnglish, Guid oceanBookingId);

        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="operationId">与业务号相同</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailSalesManAskForProfitPromise(bool isEnglish, Guid operationId);

        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="operationId"></param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailSalesForConfirmDebitFees(bool isEnglish, Guid operationId);

        /// <summary>
        /// 发送邮件给客户通知取消订舱
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="operationId"></param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailCustomerForCancelBooking(bool isEnglish, Guid operationId);

        /// <summary>
        /// 发送邮件给客户通知订舱失败
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="operationId"></param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailCustomerForSoFailure(bool isEnglish, Guid operationId);

        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="operationId"></param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailCustomerAskForSi(bool isEnglish, Guid operationId);

        /// <summary>
        /// 发送邮件给客户通知订舱成功
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="operationId"></param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        [OperationContract]
        string MailSoCopyToCustomer(bool isEnglish, Guid operationId);
        
        /// <summary>
        /// 批量新增账单
        /// </summary>
        [OperationContract]
        void BatchAddBill();
        /// <summary>
        /// 新增反馈
        /// </summary>
        /// <param name="attachmentPath"></param>
        /// <param name="description"></param>
        [OperationContract]
        void AddFeedback(string attachmentPath, string description);
        /// <summary>
        /// 新增海出询价（打开新增海出询价界面）
        /// </summary>
        [OperationContract]
        void NewInquireRate();
        /// <summary>
        /// 打开海运询价列表界面
        /// </summary>
        [OperationContract]
        void OpenOceanInquireRateListPart();

        /// <summary>
        /// 新增海运进口订单
        /// </summary>
        /// <param name="values"></param>
        [OperationContract]
        void AddOIOrder(IDictionary<string, object> values);

        /// <summary>
        /// 新增海运进口业务
        /// </summary>
        /// <param name="values"></param>
        [OperationContract]
        void AddOIBusiness(IDictionary<string, object> values);

        /// <summary>
        /// 编辑海运进口订单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void EditOIOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values);

        /// <summary>
        /// 编辑海运进口业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void EditOIBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values);

        /// <summary>
        /// 复制海运进口订单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void CopyOIOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values);

        /// <summary>
        /// 复制海运进口业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void CopyOIBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values);

        /// <summary>
        /// 海运进口业务转移
        /// </summary>
        /// <param name="BusinessID"></param>
        /// <param name="values"></param>
        [OperationContract]
        void TransferOIBusiness(Guid BusinessID, IDictionary<string, object> values);

        /// <summary>
        /// 海运进口取消业务
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="isCancel"></param>
        /// <param name="datetime"></param>
        [OperationContract]
        void CancelOIBusiness(Guid operationID, bool isCancel, DateTime? datetime);

        /// <summary>
        /// 确认放货
        /// </summary>
        [OperationContract]
        void ConfirmDelivery(Guid operationid);

        /// <summary>
        /// 取消放货
        /// </summary>
        [OperationContract]
        void CancelDelivery(Guid operation);

        /// <summary>
        /// 海进业务下载
        /// </summary>
        [OperationContract]
        void OIDownLoad();

        /// <summary>
        /// 第三方代理放单
        /// </summary>
        /// <param name="oibookingid"></param>
        /// <param name="rbld"></param>
        /// <param name="changebyid"></param>
        [OperationContract]
        void ReleaseOIBL(Guid oibookingid, bool rbld, Guid changebyid);

        /// <summary>
        /// 海进异常放货申请
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationNo"></param>
        [OperationContract]
        void OIExceptionReleaseRC(Guid operationID, string operationNo);

        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="values"></param>
        [OperationContract]
        void OpenDeliveryNotice(Guid businessID, IDictionary<string, object> values);

        /// <summary>
        /// 签收放单
        /// </summary>
        [OperationContract]
        void OIReceiveRN(List<Guid> operationidList);

        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void OIDelivery(Guid operationid);

        /// <summary>
        /// 申请放单
        /// </summary>
        /// <param name="operationidlist"></param>
        [OperationContract]
        void ApplyRelease(List<Guid> operationidlist);

        /// <summary>
        /// 催港前放单(发送邮件给港前放单人员和港前客服)
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void NoticeRelease(Guid operationid);

        /// <summary>
        /// 同意放货
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void AgreeRC(Guid operationid);

        /// <summary>
        /// 收到MBL正本
        /// </summary>
        /// <param name="operationid"></param>
        void OIOMBLRcved(Guid operationid);

        /// <summary>
        /// 财务寄出MBL正本
        /// </summary>
        /// <param name="operationid"></param>
        void MailDMBL(Guid operationid);

        /// <summary>
        /// 海出业务关帐/取消关帐
        /// </summary>
        /// <param name="operationID">业务ID</param>
        void OIStateAccountingClose(Guid operationID);

        /// <summary>
        /// 打印发货通知书
        /// </summary>
        [OperationContract]
        void PrintReleaseOrder(Guid operaionid);

        /// <summary>
        /// 打印货代提单
        /// </summary>
        [OperationContract]
        void PrintForwardingBill(Guid operationid);

        /// <summary>
        /// 打印利润表
        /// </summary>
        [OperationContract]
        void PrintProfit(Guid operationid);

        /// <summary>
        /// 打印出口业务信息报表
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void PrintExportBusinessInfo(Guid operationid);

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        [OperationContract]
        void PrintArrivalNotice(Guid operationid);

        /// <summary>
        /// 打印海进工作表
        /// </summary>
        [OperationContract]
        void OIPrintWorkSheet(Guid operationid);

        /// <summary>
        /// 打开海运进口账单
        /// </summary>
        /// <param name="businessID"></param>
        [OperationContract]
        void OpenOIBill(Guid businessID);

        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="oceanBookingId"></param>
        [OperationContract]
        void LoadContainer(string operationNo, Guid oceanBookingId);

        ///<summary>
        /// 弹出海出修订签收比较界面
        /// </summary>
        /// <param name="oeOperationId">订单ID</param>
        [OperationContract]
        void ShowReviseAccepte(Guid oeOperationId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prams"></param>
        [OperationContract]
        void AfterEditPart(object[] prams);
        /// <summary>
        /// 审核当前业务存在利润
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        [OperationContract]
        void SetUpdateOceanTrackings(Guid oceanBookingId);

        #region 本地业务数据刷新
        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        [OperationContract]
        [Obsolete("ISyncBusinessService UpdateLocalBusinessData")]
        void UpdateLocalBusinessData(Guid operationId, OperationType operationType);
        /// <summary>
        /// 批量更新本地缓存业务数据
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        [OperationContract(Name = "BatchUpdateLocalBusinessData")]
        void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType);
        /// <summary>
        /// 根据发件人地址获取发件人的类型
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetEmailSourceType")]
        EmailSourceType GetEmailSourceType(string senderAddress);


        /// <summary>
        /// 根据TemplateCode来获取业务数据集
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOperationViewList(BusinessQueryCriteria criteria);
        /// <summary>
        /// 获取本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOperationViewInfo(Guid operationId, OperationType operationType);
        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId, string reference);
        /// <summary>
        /// 获取缓存业务信息和邮件关联信息
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="messageID"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        [OperationContract]
        BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string reference);


        /// <summary>
        ///  保存业务关联业务信息,并确保业务在本地缓存存在
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="isEnsureShipmentExists"></param>
        /// <returns></returns>
        [OperationContract]
        SingleResult SaveOperationMessageRelationAndEnsureShipmentExists(OperationMessageRelation relation, bool isEnsureShipmentExists);
        /// <summary>
        /// 保存邮件与业务的关联信息
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "SaveOperationMessageRelation")]
        ManyResult SaveOperationMessageRelation(OperationMessageRelation[] relationMessages);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="relationMessage"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract(Name = "EnsureAndSaveMessageReference")]
        List<OperationMessageRelation> EnsureAndSaveMailMessageReference(OperationMessageRelation relationMessage,
                                                       Message.ServiceInterface.Message message);


        /// <summary>
        /// 打开任务中心
        /// </summary>
        /// <param name="operationContext"></param>
        [OperationContract]
        void OpenTaskCenter(BusinessOperationContext operationContext);

        /// <summary>
        /// 更改时间代码后回调刷新任务中心
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="parameter"></param>
        [OperationContract(Name = "Update Business AND callback refersh taskcenter")]
        void Save(List<BusinessSaveParameter> parameters, BusinessOperationParameter parameter);

        /// <summary>
        /// 刷新任务中心
        /// </summary>
        /// <param name="parameter"></param>
        [OperationContract]
        void AfterContainerInfoSaved(BusinessOperationParameter parameter);

        /// <summary>
        /// 根据email获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationContactInfo> GetOperationContactByEmails(List<string> emails);
        #endregion
        /// <summary>
        /// 打印装货单
        /// </summary>
        /// <param name="listData"></param>
        [OperationContract]
        void PrintLoadGoods(OceanBLList listData);

        /// <summary>
        /// 打印装货单
        /// </summary>
        /// <param name="listData">提单列表</param>
        /// <param name="iscopy">是否副本</param>
        [OperationContract]
        void PrintLoadContainer(OceanBLList listData, bool iscopy);

        /// <summary>
        /// 复制MBL单
        /// </summary>
        /// <param name="listData"></param>
        [OperationContract]
        void InnerCopyMBLData(OceanBLList listData);
        /// <summary>
        /// 复制HBL单
        /// </summary>
        /// <param name="listData"></param>
        [OperationContract]
        void InnerCopyHBLData(OceanBLList listData);
        /// <summary>
        /// 复制HBL单
        /// </summary>
        /// <param name="listData"></param>
        [OperationContract]
        void InnerCopyDeclareHBLData(OceanBLList listData);
        /// <summary>
        /// 删除提单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <param name="businessOperationContext"></param>
        /// <returns></returns>
        [OperationContract]
        bool InnerDelete(OceanBLList listData, object DataSource, object bsList,
                          object businessOperationContext);

        /// <summary>
        /// 删除提单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <param name="businessOperationContext"></param>
        /// <returns></returns>
        [OperationContract]
        bool InnerDeclareDelete(OceanBLList listData, object DataSource, object bsList,
                          object businessOperationContext);
        /// <summary>
        /// 提单的分单操作
        /// </summary>
        /// <param name="splitBLList"></param>
        /// <param name="values"></param>
        [OperationContract]
        void SplitBillOfLoading(OceanBLList splitBLList, IDictionary<string, object> values);
        /// <summary>
        /// 提单的合单操作
        /// </summary>
        /// <param name="selectedList"></param>
        [OperationContract]
        void MergeBillOfLoading(List<OceanBLList> selectedList);

        /// <summary>
        /// 快速打开EDI
        /// </summary>
        /// <param name="workItem">workItem</param>
        [OperationContract]
        void OpenAddBookingEdi(WorkItem workItem);

        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="amsEntryType"></param>
        /// <param name="aciEntryType"></param>
        /// <param name="isSucc"></param>
        /// <param name="businessOperationContext"></param>
        [OperationContract]
        void InnerEMBL(List<OceanBLList> selectedList, Guid companyID, AMSEntryType amsEntryType,
                       ACIEntryType aciEntryType, ref bool isSucc, object businessOperationContext);

        /// <summary>
        /// EDI VGM
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="isSucc"></param>
        /// <param name="businessOperationContext"></param>
        [OperationContract]
        void InnerEVGM(OceanBLList selectedList, Guid companyID, ref bool isSucc, object businessOperationContext);


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
        [OperationContract]
        void SendEdiamsaicisf(string key, string subjuect, List<Guid> oIds, List<Guid> hblIds,
                                      List<string> operationNos,
                                      bool isSucc, object ediMode, Guid CompanyID, object businessOperationContext);

        /// <summary>
        /// 确认AMS费用
        /// </summary>
        /// <param name="hblids"></param>
        /// <param name="updateBy">更新人</param>
        [OperationContract]
        void ConfirmedAMS(Guid[] hblids, Guid updateBy);
        /// <summary>
        /// 
        /// </summary>
        string TemplateCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateCode"></param>
        [OperationContract]
        void SetTemplateCode(string templateCode);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetTemplateCode();
        /// <summary>
        /// 根据邮件主题获取本地缓存业务信息
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetLocalOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria);
        /// <summary>
        /// 获取本地缓存邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        [OperationContract]
        List<OperationMessageRelation> GetLocalOperationMessageRelationByMessageIdAndReference(string messageId, string reference);

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        [OperationContract(Name = "UpdateLocalBusinessDataByDataTable")]
        void UpdateLocalBusinessData(Guid operationId, OperationType operationType, DataTable dt);



        /// <summary>
        /// 保存本地缓存邮件关联业务信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SaveLocalOperationMessageRelation(OperationMessageRelation[] relationMessages);

        #region 消息相关
        /// <summary>
        /// 显示发送界面
        /// </summary>
        /// <returns>点击发送,返回True，取消发送返回False</returns>
        [OperationContract]
        bool ShowSendForm(Message.ServiceInterface.Message message);
        /// <summary>
        /// 显示预览界面
        /// </summary>
        [OperationContract]
        void ShowReadForm(Message.ServiceInterface.Message message);
        /// <summary>
        /// 发送并保存日志
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendAndSaveLog(Message.ServiceInterface.Message message);
        /// <summary>
        /// 将邮件文件转换为PDF文件
        /// </summary>
        /// <param name="mailFile"></param>
        /// <returns></returns>
        [OperationContract]
        string ConvertMailToPDF(string mailFile);

        /// <summary>
        /// 打开Msg文件
        /// </summary>
        /// <param name="MsgFilePath"></param>
        [OperationContract(Name = "OpenMsg")]
        void Open(string MsgFilePath);
        /// <summary>
        /// 通过传真或者邮件中心打开消息
        /// </summary>
        /// <param name="id">消息主键Id</param>
        [OperationContract]
        void Open(Guid id);
        /// <summary>
        /// 把Message转换为Msg文件，并在邮件客户端显示
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void ConvertMessageToMsg(Message.ServiceInterface.Message message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        ManyResult[] SaveMessage(Message.ServiceInterface.Message message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        SingleResult SendMessage(Message.ServiceInterface.Message message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageID"></param>
        /// <returns></returns>
        [OperationContract]
        Message.ServiceInterface.Message GetMessageInfoById(Guid MessageID);

        #endregion
        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        [OperationContract]
        void MailSoConfirmationToCustomer(Guid oceanBookingId, bool isEnglish);

        /// <summary>
        /// SOD成功以后发送邮件给客服
        /// </summary>
        /// <param name="oceanBookingId">业务id</param>
        /// <param name="memoType">备注类型</param>
        /// <param name="path">操作路径</param>
        /// <param name="eventFlg">是否需要生成事件</param>
        [OperationContract]
        void MainSOCustomerServiceSOD(Guid oceanBookingId, MemoType memoType, string path, bool eventFlg);
        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID </param>
        [OperationContract]
        string MailSoConfirmationToAgent(Guid oceanBookingId);
        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        /// <returns></returns>
        [OperationContract]
        void MailPickUpToCustomer(Guid operationid, bool isEnglish);
        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        [OperationContract]
        void MailAnToCustomer(Guid operationid, bool isEnglish);

        /// <summary>
        /// 判断是否存在关联
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <returns></returns>
        [OperationContract]
        bool ExistsRelation(string messageId);
        /// <summary>
        /// 判断关联是否发送更改,如发送更改则返回关联,如果关联已被删除，则返回的关联中HasData为false，否则返回null
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <param name="updateDate">消息的更改时间</param>
        /// <returns></returns>
        [OperationContract]
        OperationMessageRelation CheckRelationIsChanged(string messageId, DateTime? updateDate);
        /// <summary>
        /// 保存业务联系人和同步本地缓存
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="contactList"></param>
        /// <returns></returns>
        [OperationContract]
        ManyResult SaveAndSyncOperationContacts(List<string> emails, List<CustomerCarrierObjects> contactList);
        /// <summary>
        /// 邮件订舱
        /// </summary>
        /// <param name="oceanBookIngidList"></param>
        [OperationContract]
        void CommunicationMailBooking(List<Guid> oceanBookIngidList);
        /// <summary>
        /// 任务中心手动添加事件
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <param name="eventObjects">事件实体对象</param>
        [OperationContract]
        bool TaskCenterSaveEvent(OperationType operationType, EventObjects eventObjects);

        #region 财务
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operation">业务类别</param>
        /// <param name="formType">表单类型</param>
        [OperationContract]
        void OpenBill(Guid operationId, OperationType operation, FormType formType = FormType.Unknown);
        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="operationType">业务口岸ID</param>
        /// <param name="formType">表单类型</param>
        void VerifiSheet(Guid operationId, string operationNo, OperationType operationType, FormType formType = FormType.Unknown); 
        #endregion

        #region   空出接口
        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="values"></param>
        [OperationContract]
        void AirExportAddData(IDictionary<string, object> values);
        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void AirExportCopyData(EditPartShowCriteria showCriteria, IDictionary<string, object> values);
        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void AirExportEditData(EditPartShowCriteria showCriteria, IDictionary<string, object> values);
        /// <summary>
        /// 取消订舱单
        /// </summary>
        /// <param name="airBookingId"></param>
        [OperationContract]
        void AirExportCancelData(Guid airBookingId);
        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="values"></param>
        [OperationContract]
        void AirExportReplyAgent(Guid bookingId, IDictionary<string, object> values);
        /// <summary>
        /// 打开提单列表
        /// </summary>
        /// <param name="airBookingId"></param>
        [OperationContract]
        void AirExportOpenBl(Guid airBookingId);
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="airBookingId"></param>
        [OperationContract]
        void AirExportOpenBill(Guid airBookingId);
        /// <summary>
        /// 打印联单
        /// </summary>
        /// <param name="airBookingId"></param>
        [OperationContract]
        void AirExportPrintOrder(Guid airBookingId);

        #endregion

        #region 空进接口

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="values"></param>
        [OperationContract]
        void AirImportAddBooking(IDictionary<string, object> values);


        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void AirImportCopyBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values);


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        [OperationContract]
        void AirImportEditBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values);

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportOpenBill(Guid bookingId);

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportPrintArrivalNotice(Guid bookingId);


        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportPrintReleaseOrder(Guid bookingId);

        /// <summary>
        ///  打印利润表
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportPrintProfit(Guid bookingId);


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportCancelBooking(Guid bookingId);

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="values"></param>
        [OperationContract]
        void AirImportOpenCargoBook(Guid bookingId, IDictionary<string, object> values);


        /// <summary>
        /// 下载
        /// </summary>
        [OperationContract]
        void AirImportAiDownLoad();

        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportBusinessTransfer(Guid bookingId);

        /// <summary>
        /// 放货和取消放货
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportAiDelivery(Guid bookingId);
        /// <summary>
        /// 打印Authority To Make Entry
        /// </summary>
        /// <param name="bookingId"></param>
        [OperationContract]
        void AirImportPrintAuthority(Guid bookingId);

        #endregion

        /// <summary>
        /// 恢复订舱
        /// </summary>
        /// <param name="operationId">业务ID</param>
        [OperationContract]
        bool RestoreBooking(Guid operationId);

        #region  询价发送邮件
        /// <summary>
        /// 根据询价ID查询出当前询问人，并新建一份空白的邮件
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        [OperationContract]
        void EmailMailtoAskpeople(Guid inquerireid);
        /// <summary>
        /// 邮件发给承运人
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        [OperationContract]
        void EmailtoCarrier(Guid inquerireid);
        #endregion

        #region  邮件插件方法
        /// <summary>
        /// 打开联系人面板
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationNo">业务号</param>
        [OperationContract]
        void ShowContactsAndAssistants(Guid operationId, OperationType operationType, string operationNo);

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
        [OperationContract]
        void Upload(UploadWay way, Guid operationId, string mailSubject, object objMailItem,
            SelectionType type, OperationType operationType,
            List<string> addDocList, string operationNo, DateTime? upDateTime, Message.ServiceInterface.Message message);
        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        [OperationContract]
        void ShowContactPart(AssociateType associateType, List<BusinessOperationContext> bussOperationContexts,
            Message.ServiceInterface.Message message);

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="contactType">联系人类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        [OperationContract]
        void SaveContactList(ContactType contactType, List<BusinessOperationContext> bussOperationContexts,
            Message.ServiceInterface.Message message);

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
        [OperationContract]
        void OeEmailQueryPartShow(string no, int noType, int types, int dateType, string main, int? area, string text,
            Guid? operationId, OperationType operationType);
        /// <summary>
        /// 邮件中心是否刷新
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool MailCenterRefresh(bool? flg);
        /// <summary>
        /// 高级查询
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        [OperationContract]
        string Advancedquery(OperationType operationType);
        /// <summary>
        /// 判断当前联系人是否存在 
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="message">邮件</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="operation">业务类型</param>
        /// <returns></returns>
        [OperationContract]
        bool SaveOperationMessage(AssociateType associateType, Message.ServiceInterface.Message message,
            Guid operationId, OperationType operation);
        #endregion

        #region 其他业务
        /// <summary>
        /// 新增
        /// </summary>
        [OperationContract]
        void OtherBusinessAddData();

        /// <summary>
        /// 复制其他业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        [OperationContract]
        void OtherBusinessCopyData(Guid operationId,Guid companyID);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        [OperationContract]
        void OtherBusinessEditData(Guid operationId,Guid companyID);
        /// <summary>
        /// 取消业务/恢复业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        [OperationContract]
        void OtherBusinessCancelData(Guid operationId, Guid companyID);

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        [OperationContract]
        void OtherBusinessBill(Guid operationId);
        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        [OperationContract]
        void OtherBusinessVerifiSheet(Guid operationId, Guid companyID);
        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        [OperationContract]
        void OtherBusinessPickUp(Guid operationId, Guid companyID);
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        [OperationContract]
        void OtherBusinessPrintOrder(Guid operationId, Guid companyID);
        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        [OperationContract]
        void OtherBusinessProfit(Guid operationId, Guid companyID);

        #endregion

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="operationguids">业务ID集合</param>
        [OperationContract]
        void OiPayment(List<Guid> operationguids);


        /// <summary>
        /// New 发货
        /// </summary>
        /// <param name="operationguids">业务ID集合</param>
        [OperationContract]
        void OiNewOiDelivery(List<Guid> operationguids);



        /// <summary>
        /// 发送催款邮件
        /// </summary>
        /// <param name="operationId">业务ID</param>
        [OperationContract]
        void PayNtMail(Guid operationId);

        /// <summary>
        /// 发送催款邮件（自动发送）
        /// </summary>
        [OperationContract]
        void PayNtautomaticMail();

        /// <summary>
        /// 催代理分发文件信息
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">中英文版本</param>
        [OperationContract]
        void MailOverseaAgent(Guid operationid, bool isEnglish);

        /// <summary>
        ///  ETA 前5天催清关文件
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void MailCustomrequest(Guid operationid);

        /// <summary>
        /// ETA 变更通知
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void MailEtachange(Guid operationid);


        /// <summary>
        /// FULLY RELEASE 之后柜子AVAILABILITY通知
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void MailContaineravailableforpickup(Guid operationid);


        /// <summary>
        /// LFD 通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="lastFreeDate"></param>
        /// <param name="content"></param>
        [OperationContract]
        void MailLfdnotice(Guid operationid, string lastFreeDate, string content);


        /// <summary>
        /// LFD 前2工作日后每天提柜提醒
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void MailContainerpickupreminder(Guid operationid);


        /// <summary>
        /// 提柜后第2天开始每天还空提醒
        /// </summary>
        /// <param name="operationid"></param>
        [OperationContract]
        void MailEmptyreturnnotice(Guid operationid);


        #region Outlook Plugin
        /// <summary>
        /// 根据邮件ID，获取关联信息，返回Datatable
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>DataTable</returns>
        [OperationContract]
        DataTable GetOperationMessageRelationDataTableByMessageID(string messageID);

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回单个对象
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>OperationMessageRelation</returns>
        [OperationContract]
        OperationMessageRelation GetOperationMessageRelationByMessageID(string messageID);


        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        /// <rereturns>DataTable</rereturns>
        [OperationContract]
        DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria);

        /// <summary>
        /// 联系人是否存在缓存
        /// </summary>
        /// <param name="ExternalEmails">联系人集合</param>
        /// <returns>是否存在:True存在；False不存在；</returns>
        [OperationContract]
        bool IsAllContactExsitCache(List<string> ExternalEmails);

        /// <summary>
        /// 保存联系人
        /// </summary>
        /// <param name="ContactParameters">联系人集合</param>
        [OperationContract]
        void SaveLocalOperationContactMail(List<OperationContactParameters> ContactParameters);

        /// <summary>
        /// 删除服务端数据库关联信息和本地缓存关联信息
        /// </summary>
        /// <param name="messageRelationIds">关联信息ID集合</param>
        /// <param name="updateDates">更新时间</param>
        [OperationContract]
        void RemoveAndSyncOperationMessageRelations(Guid[] messageRelationIds, DateTime?[] updateDates);

        /// <summary>
        /// 根据邮件地址，获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>如果本地不存在，则返回null</returns>
        [OperationContract]
        int? GetContactPersonType(string emailAddress);

        ///// <summary>
        ///// 写入操作日志
        ///// </summary>
        ///// <param name="watchTime">计时器</param>
        ///// <param name="AssemblyNames">组件名称</param>
        ///// <param name="FunctionName">操作内容</param>
        //[OperationContract]
        //void WriteStopwatchLog(string watchTime, string AssemblyNames, string FunctionName);
        #endregion
    }
}

using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 海运进口客户端服务
    /// </summary>
    public interface IClientOceanImportService
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        void AddOrder(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 编辑订单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        void EditOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 复制订单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        void CopyOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 编辑业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void EditBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 新增业务
        /// </summary>
        /// <param name="source"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void AddBusiness(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void CopyBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        

        /// <summary>
        /// 公共确认界面
        /// </summary>
        /// <param name="formTitle"></param>
        /// <param name="values"></param>
        /// <param name="title"></param>
        /// <param name="editPartSaved"></param>
        void CommonConfirmForm(string formTitle, IDictionary<string, object> values, string title, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="BusinessID"></param>
        /// <param name="values"></param>
        /// <param name="title"></param>
        /// <param name="editPartSaved"></param>
        void BusinessTransfer(Guid BusinessID, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="BusinessID"></param>
        /// <param name="values"></param>
        /// <param name="title"></param>
        /// <param name="editPartSaved"></param>
        void OpenDeliveryNotice(Guid BusinessID, IDictionary<string, object> values, string title, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void OpenDeliveryNotice(Guid businessID, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <param name="currentFormID">表单ID</param>
        /// <param name="type"></param>
        void OpenBill(Guid businessID, Guid currentFormID);

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationid"></param>
        void OpenBill(Guid operationid);

        /// <summary>
        /// 取消业务
        /// </summary>
        void CancelOIBusiness(Guid operationID, bool isCancel, DateTime? datetime, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 确认放货
        /// </summary>
        /// <param name="list"></param>
        void ConfirmDelivery(OceanBusinessList list, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void ConfirmDelivery(Guid operationid, PartDelegate.EditPartSaved editPartSaved);


        /// <summary>
        /// 取消放货
        /// </summary>
        /// <param name="list"></param>
        /// <param name="editPartSaved"></param>
        void CancelDelivery(OceanBusinessList list, PartDelegate.EditPartSaved editPartSaved);


        /// <summary>
        /// 取消放货
        /// </summary>
        /// <param name="list"></param>
        /// <param name="editPartSaved"></param>
        void CancelDelivery(Guid operaionid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 异常放货申请流程
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationNo"></param>
        void ExceptionReleaseRC(Guid operationID, string operationNo);

        /// <summary>
        /// 第三方代理放单
        /// </summary>
        /// <param name="oibookingid">业务ID</param>
        /// <param name="rbld">放单</param>
        /// <param name="changebyid">放单人</param>
        void ReleaseOIBL(Guid oibookingid, bool rbld, Guid changebyid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 签收放单
        /// </summary>
        /// <param name="businesslist"></param>
        void OIReceiveRN(OceanBusinessList businesslist, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 签收放单
        /// </summary>
        /// <param name="businesslist"></param>
        void OIReceiveRN(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void OIDelivery(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 申请放单
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void ApplyRelease(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 催港前放单(发送邮件给港前放单人员和港前客服)
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void NoticeRelease(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 同意放货
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void AgreeRC(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

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
        /// 海进关帐/取消关帐
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="editPartSaved">更新后执行事件</param>
        void StateAccountingClose(Guid operationID,PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 业务下载
        /// </summary>
        void OIDownLoad();

        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="values"></param>
        /// <param name="NO"></param>
        void PrintReleaseOrder(IDictionary<string, object> values, string NO);

        /// <summary>
        /// 打印发货通知书
        /// </summary>
        /// <param name="businessList"></param>
        void PrintReleaseOrder(OceanBusinessList businessList);

        /// <summary>
        /// 打印发货通知书
        /// </summary>
        /// <param name="businessList"></param>
        void PrintReleaseOrder(Guid operaionid);

        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="values"></param>
        /// <param name="NO"></param>
        void PrintForwardingBill(IDictionary<string, object> values, string NO);

        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="businessList"></param>
        void PrintForwardingBill(Guid operationid);

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="list"></param>
        void PrintProfit(OceanBusinessList list);

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="operationInfo"></param>
        void PrintProfit(OceanBusinessList list, ICP.Message.ServiceInterface.Message operationInfo);


        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="operationID"></param>
        void PrintProfit(Guid operationID);

        /// <summary>
        /// 打印出口业务信息报表
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="customerID"></param>
        void PrintExportBusinessInfo(string customerName, Guid customerID);

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationid"></param>
        void PrintExportBusinessInfo(Guid operationid);

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="list"></param>
        void PrintArrivalNotice(OceanBusinessList list);

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="list"></param>
        void PrintArrivalNotice(Guid operationid);

        /// <summary>
        ///海进打印工作表
        /// </summary>
        void OIPrintWorkSheet(Guid operationid);

        /// <summary>
        /// 发送邮件给港前业务账单的修改人。告知账单已被港后修改。
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="OEBookingID"></param>
        /// <returns></returns>
        string MailApplyBillRevise(bool isEnglish, Guid OEBookingID);

        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        /// <returns></returns>
        void MailPickUpToCustomer(Guid operationid, bool isEnglish);
        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="isEnglish">发送邮件版本</param>
        /// <returns></returns>
        void MailPickUpToCustomer(Guid operationid, string mailInformation, bool isEnglish);
        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        void MailAnToCustomer(Guid operationid, bool isEnglish);

        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="isEnglish">发送邮件版本</param>
        void MailAnToCustomer(Guid operationid, string mailInformation, bool isEnglish);

        /// <summary>
        /// New 放货
        /// </summary>
        /// <param name="operationid">需要操作的集合</param>
        /// <param name="editPartSaved"></param>
        void NewOiDelivery(List<Guid> operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 发送催款邮件
        /// </summary>
        /// <param name="operationId">业务ID</param>
        void PayNtMail(Guid operationId);

        /// <summary>
        /// 催代理分发文件信息
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">中英文版本</param>
        /// <param name="editPartSaved"></param>
        void MailOverseaAgent(Guid operationid, bool isEnglish, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// ETA 前5天催清关文件
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void MailCustomrequest(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// ETA 变更通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void MailEtachange(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// FULLY RELEASE 之后柜子AVAILABILITY通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void MailContaineravailableforpickup(Guid operationid, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// POD提醒
        /// </summary>
        /// <param name="operationid"></param>
        void NoticePod(Guid operationid);
        /// <summary>
        /// POD提醒
        /// </summary>
        /// <param name="operationid"></param>
        void NoticeEmptyReport(Guid operationid, string ContainerNO, string messto);

        /// <summary>
        /// LFD 通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="lastFreeDate"></param>
        /// <param name="editPartSaved"></param>
        void MailLfdnotice(Guid operationid, string lastFreeDate, PartDelegate.EditPartSaved editPartSaved, string content);

        /// <summary>
        /// LFD 前2工作日后每天提柜提醒
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void MailContainerpickupreminder(Guid operationid, PartDelegate.EditPartSaved editPartSaved);


        /// <summary>
        /// 提柜后第2天开始每天还空提醒
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        void MailEmptyreturnnotice(Guid operationid, PartDelegate.EditPartSaved editPartSaved);


        /// <summary>
        /// ETA变更服务端发送邮件
        /// </summary>
        /// <param name="operationid">业务ID</param>
        void MailEtachangeServerSend(List<Guid> operationid);

    }
}

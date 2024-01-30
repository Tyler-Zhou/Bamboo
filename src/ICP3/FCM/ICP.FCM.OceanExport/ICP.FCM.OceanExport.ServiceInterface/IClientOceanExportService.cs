using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;

namespace ICP.FCM.OceanExport.ServiceInterface
{
    /// <summary>
    ///海运出口客户端服务接口
    /// </summary>
    public interface IClientOceanExportService
    {
        #region Business
        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        BusinessOperationContext CopyBusiness(BusinessOperationContext context);
        #endregion

        #region Order
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
        /// 取消或激活订单
        /// </summary>
        /// <param name="oceanBookingList">实体对象</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        SingleResult CancelOrder(OceanBookingList oceanBookingList, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 取或激活订单
        /// </summary>
        /// <param name="oceanBookingList"></param>
        /// <param name="editPaerSaved"></param>
        /// <returns></returns>
        SingleResult CancelOrderForFCM(OceanBookingList oceanBookingList, PartDelegate.EditPartSaved editPaerSaved);
        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="orderId">订单ID或者业务Id</param>
        /// <param name="companyID">订单口岸ID</param>
        void PrintOrder(Guid orderId, Guid companyID);
        #endregion

        #region Booking
        /// <summary>
        ///新增订舱单
        /// </summary>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        void AddBooking(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        void EditBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria">编辑界面数据源条件类</param>
        /// <param name="values">初始化值集合</param>
        /// <param name="editPartSaved">执行当前方法后事件</param>
        void CopyBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 取消或激活订舱单
        /// </summary>
        /// <param name="operationId">业务Id</param>
        /// <param name="editPartSaved"></param>
        SingleResult CancelBooking(Guid operationId, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 取消或激活订舱单
        /// </summary>
        /// <param name="operationIds">业务Id集合</param>
        /// <param name="editPartSaved"></param>
        ManyResult CancelBookings(Guid[] operationIds, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 取消或激活订舱单
        /// </summary>
        /// <param name="operationIds">业务Id集合</param>
        /// <param name="editPartSaved"></param>
        /// <param name="IsCancel"></param>
        ManyResult CancelBookings(Guid[] operationIds, PartDelegate.EditPartSaved editPartSaved, bool IsCancel);
        /// <summary>
        /// EDI方法
        /// </summary>
        /// <param name="oceanBookingList">实体对象</param>
        /// <param name="selectRows">选择行</param>
        bool EBookingCall(OceanBookingList oceanBookingList, List<OceanBookingList> selectRows);
        /// <summary>
        /// EDI订舱确认
        /// </summary>
        /// <param name="oceanBookingList">实体对象</param>
        /// <param name="selectRows">选择行</param>
        bool EBookingConfirm(OceanBookingList oceanBookingList, List<OceanBookingList> selectRows);
        #endregion

        #region BL
        /// <summary>
        /// 新增MBL
        /// </summary>
        /// <param name="editPartSaved">新增保存后回调处理方法</param>
        /// <param name="values"></param>
        void AddMBL(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 新增HBL
        /// </summary>
        /// <param name="editPartSaved">新增保存后回调处理方法</param>
        /// <param name="values"></param>
        void AddHBL(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 新增DeclareHBL
        /// </summary>
        /// <param name="editPartSaved">新增保存后回调处理方法</param>
        /// <param name="values"></param>
        void AddDeclareHBL(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 根据舱单ID构建MBL信息
        /// </summary>
        /// <param name="bookingID">订舱单ID</param>
        /// <returns></returns>
        OceanMBLInfo BuildMBLByBookingInfo(Guid bookingID);
        /// <summary>
        /// 编辑MBL
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="mblNo">MBL No.</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void EditMBL(string operationNo, string mblNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 编辑HBL
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="hblNo">HBL No.</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved">保存后回调处理方法</param>
        void EditHBL(string operationNo, string hblNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 编辑DeclareHBL
        /// </summary>
        /// <param name="HblID">Hbl ID</param>
        /// <param name="hblNo">HBL No.</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved">保存后回调处理方法</param>
        void EditDeclareHBL(Guid HblID, string hblNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 预配
        /// </summary>
        /// <param name="blNO"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void DeclarationContainer(string blNO, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 预配
        /// </summary>
        /// <param name="blNO"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void DeclarationImport(string blNO, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        #endregion

        #region Agent
        /// <summary>
        /// 打开申请代理页面
        /// </summary>
        /// <param name="bookingId">业务ID</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        bool ReplyAgent(Guid bookingId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        #endregion

        #region AMS
        /// <summary>
        /// 确认AMS费用
        /// </summary>
        /// <param name="hblids">提单ID集合</param>
        /// <param name="updateBy">更新人</param>
        bool ConfirmedAMS(Guid[] hblids, Guid updateBy);
        #endregion

        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="MBLNo">MBL No</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void LoadShip(string operationNo, string MBLNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="mblID">MBLID</param>
        /// <param name="BLNo">提单号</param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void LoadShip(Guid mblID, string BLNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        
        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void LoadContainer(string operationNo, Guid oceanBookingId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId">业务Id</param>
        /// <param name="operationType">业务类型</param>
        void OpenBill(Guid operationId, OperationType operationType);
        /// <summary>
        /// 显示核销单列表界面
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationNo"></param>
        void OpenVerifiSheet(Guid operationId, string operationNo);
        /// <summary>
        /// 打开派车单
        /// </summary>
        /// <param name="bookingId">订仓单Id，和业务ID相同</param>
        /// <param name="operationNo">业务号</param>
        ///<param name="bookingInfo">订仓单信息，可为null</param>
        ///<param name="values">自定义信息</param>
        ///<param name="editPartSaved"></param>
        void OpenTruckOrder(Guid bookingId, string operationNo, OceanBookingList bookingInfo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 打开指定业务号的订舱单列表
        /// </summary>
        /// <param name="operationNo">业务号</param>
        void OpenBillOfLoadingList(string operationNo);
        /// <summary>
        /// 打开订舱单列表
        /// </summary>
        /// <param name="bookIngListQuery"></param>
        void OpenBookingList(ICP.FCM.OceanExport.ServiceInterface.CompositeObjects.BookingListQueryCriteria queryCriteria);
        /// <summary>
        /// 打开报关单
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="editPartSaved"></param>
        /// <param name="values"></param>
        void OpenCustomsOrder(Guid operationId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打印海出提货单
        /// </summary>
        /// <param name="listData">提单列表</param>
        /// <param name="iscopy">是否副本</param>
        void PrintLoadContainers(OceanBLList listData, bool iscopy);

        /// <summary>
        /// 打印海出提货单
        /// </summary>
        /// <param name="listData"></param>
        void PrintLoadGoods(OceanBLList listData);
        /// <summary>
        /// 打印海出提货单
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="mblNo"></param>
        void PrintLoadGoods(string operationNo, string mblNo);

        /// <summary>
        /// 打印订舱确认书(宁波)
        /// </summary>
        /// <param name="operationID"></param>
        void PrintOEBookingConfirmation4NB(Guid operationID);

        /// <summary>
        /// 订舱确认书
        /// </summary>
        /// <param name="id">订舱单ID，等于业务ID</param>
        void PrintBookingConfirm(Guid oceanBookingId);
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID，等于业务ID</param>
        void PrintBookingProfit(Guid oceanBookingId);
        /// <summary>
        /// 打印提单
        /// </summary>
        /// <param name="listData"></param>
        void PrintBillOfLoading(OceanBLList listData);

        /// <summary>
        /// 打印提单
        /// </summary>
        /// <param name="listData"></param>
        void PrintBillOfLoading(Guid listData);
        /// <summary>
        /// 打印提单
        /// </summary>
        /// <param name="listData"></param>
        void PrintBillOfLoading(string operationNo, string blNo);
        /// <summary>
        /// 显示港后客服联系人面板
        /// </summary>
        /// <param name="consigneeName">收货人名称</param>
        void ShowAgentFilerList(string consigneeName);
        /// <summary>
        /// 分单
        /// </summary>
        /// <param name="splitBLList">需要分单的提单</param>
        ///<param name="editPartSaved"></param>
        void SplitBillOfLoading(OceanBLList splitBLList, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 分单
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="editPartSaved"></param>
        void SplitBillOfLoading(string operationNo, string blNo, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 合单
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="editPartSaved"></param>
        void MergeBillOfLoading(List<OceanBLList> selectedList, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 当前业务下是否有柜
        /// </summary>
        /// <param name="oceanBookingId"></param>
        /// <returns></returns>
        bool IsShipmentHasContainer(Guid oceanBookingId);

        /// <summary>
        /// 发送邮件信息时候进行判断
        /// </summary>
        /// <param name="oceanBooking">订舱的实体对象</param>
        /// <returns>返回错误信息</returns>
        string GetEmailSendValidationInfo(OceanBookingInfo oceanBooking);
        /// <summary>
        /// 发送SO Copy给客户
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        string MailSoCopyToCustomer(bool isEnglish, Guid oceanBookingId);
        /// <summary>
        /// 发送SO Copy给客户
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="mailInformation"></param>
        string MailSoCopyToCustomer(bool isEnglish, Guid oceanBookingId, string mailInformation);
        /// <summary>
        ///  发送SO Copy给客户
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        string MailSoCopyToCustomer(bool isEnglish, OceanBookingInfo oceanBookingInfo);
        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId">与业务号相同</param>
        string MailCustomerAskForSi(bool isEnglish, Guid oceanBookingId);

        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId">与业务号相同</param>
        /// <param name="mailInformation">邮箱地址</param>
        string MailCustomerAskForSi(bool isEnglish, Guid oceanBookingId, string mailInformation);
        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        string MailCustomerAskForSi(bool isEnglish, OceanBookingInfo oceanBookingInfo);
        /// <summary>
        /// 向代理确认提单
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <returns></returns>
        string MailALLBLCopyToAgent(bool isEnglish, Guid oceanBookingId, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 向代理确认提单
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <returns></returns>
        string MailALLBLCopyToAgent(bool isEnglish, OceanBookingInfo oceanBookingInfo, PartDelegate.EditPartSaved editPartSaved);


        /// <summary>
        /// 发送邮件给客户确认提单无误,补料OK
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="oceanHblInfo"></param>
        /// <returns></returns>
        string MailCustomerAskForConfirmSI(bool isEnglish, Guid oceanBookingId, OceanHBLInfo oceanHblInfo, OceanMBLInfo oceanMblInfo);
        /// <summary>
        /// 发送邮件给客户确认提单无误,补料OK
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="oceanBookingId"></param>
        /// <param name="oceanHblInfo"></param>
        /// <returns></returns>
        string MailCustomerAskForConfirmSI(bool isEnglish, string mailInformation, Guid oceanBookingId, OceanHBLInfo oceanHblInfo, OceanMBLInfo oceanMblInfo);
        /// <summary>
        /// 发送邮件给客户通知取消订舱
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        string MailCustomerForCancelBooking(bool isEnglish, Guid oceanBookingId);
        /// <summary>
        /// 发送邮件给客户通知取消订舱
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        string MailCustomerForCancelBooking(bool isEnglish, OceanBookingInfo oceanBookingInfo);
        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId">与业务号相同</param>
        string MailSalesManAskForProfitPromise(bool isEnglish, Guid oceanBookingId);
        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId">与业务号相同</param>
        /// <param name="mailInformation">邮件地址</param>
        string MailSalesManAskForProfitPromise(bool isEnglish, Guid oceanBookingId, string mailInformation);

        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        string MailSalesManAskForProfitPromise(bool isEnglish, OceanBookingInfo oceanBookingInfo);

        /// <summary>
        /// 发送邮件给客户通知订舱失败
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="id"></param>
        string MailCustomerForSoFailure(bool isEnglish, Guid oceanBookingInfo);
        /// <summary>
        /// 发送邮件给客户通知订舱失败
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        string MailCustomerForSoFailure(bool isEnglish, OceanBookingInfo oceanBookingInfo);
        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        string MailSalesForConfirmDebitFees(bool isEnglish, Guid oceanBookingId);
        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingId"></param>
        /// <param name="mailInformation"></param>
        string MailSalesForConfirmDebitFees(bool isEnglish, Guid oceanBookingId, string mailInformation);
        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="oceanBookingInfo"></param>
        /// <returns></returns>
        string MailSalesForConfirmDebitFees(bool isEnglish, OceanBookingInfo oceanBookingInfo);
        /// <summary>
        /// 发送邮件给订舱员申请订舱
        /// </summary>
        /// <param name="oceanBookingId">等同于业务Id</param>
        /// <returns></returns>
        string MailBookingClerkForApplySO(Guid oceanBookingId);
        /// <summary>
        /// 发送邮件给订舱员申请订舱
        /// </summary>
        /// <param name="oceanBookingInfo"></param>
        /// <returns></returns>
        string MailBookingClerkForApplySO(OceanBookingInfo oceanBookingInfo);
        /// <summary>
        /// 打开备注输入界面
        /// </summary>
        /// <param name="labRemark">备注框说明文本</param>
        /// <param name="isRemarkRequired">备注是否必须输入</param>
        /// <param name="title">窗体标题</param>
        /// <param name="editPartSaved"></param>
        void ShowRemarkEditForm(string labRemark, bool isRemarkRequired, string title, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 选择运价合约
        /// </summary>
        /// <param name="oceanBookingId"></param>
        /// <param name="editPartSaved"></param>
        void SelectContract(Guid oceanBookingId, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 选择运价合约
        /// </summary>
        /// <param name="oceanBookingInfo"></param>
        /// <param name="editPartSaved"></param>
        void SelectContract(OceanBookingInfo oceanBookingInfo, SelectType type, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 选择进口合约
        /// </summary>
        /// <param name="mblInfo"></param>
        /// <param name="editPartSaved"></param>
        void SelectContract(Guid FreightRateID);
        /// <summary>
        /// 选择海运运价合约
        /// </summary>
        /// <param name="mblInfo"></param>
        /// <param name="editPartSaved"></param>
        void SelectContract(OceanMBLInfo mblInfo, PartDelegate.EditPartSaved editPartSaved);


        ///<summary>
        /// 弹出海出修订签收比较界面
        /// </summary>
        /// <param name="oeOperationId">订单ID</param>
        void ShowReviseAccepte(Guid oeOperationId);


        /// <summary>
        /// 快速打开EDI
        /// </summary>
        /// <param name="workItem">workItem</param>
        void OpenAddBookingEdi(WorkItem workItem);

        /// <summary>
        /// 港后发起修订后，港前需先签收再修改合约
        /// </summary>
        /// <param name="opeationID"></param>
        /// <returns></returns>
        bool IsNeedAccept(Guid opeationID);
        /// <summary>
        /// 审核当前业务存在利润
        /// </summary>
        /// <param name="OceanBookingID">业务ID</param>
        void SetUpdateOceanTrackings(Guid OceanBookingID);

        /// <summary>
        /// 复制MBL
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="editPartSaved"></param>
        void InnerCopyMBLData(OceanBLList listData, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 复制HBL
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="editPartSaved"></param>
        void InnerCopyHBLData(OceanBLList listData, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 复制HBL
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="editPartSaved"></param>
        void InnerCopyDeclareHBLData(OceanBLList listData, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 删除提单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <returns></returns>
        bool InnerDelete(OceanBLList listData, object DataSource, object bsList,
                         object businessOperationContext, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 删除Declare提单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <returns></returns>
        bool InnerDeclareDelete(OceanBLList listData, object DataSource, object bsList,
                         object businessOperationContext, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="amsEntryType"></param>
        /// <param name="aciEntryType"></param>
        /// <param name="isSucc"></param>
        void InnerEMBL(List<OceanBLList> selectedList, Guid companyID, AMSEntryType amsEntryType,
                       ACIEntryType aciEntryType, ref bool isSucc, object businessOperationContext,
                       PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// EDI VGM
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="amsEntryType"></param>
        /// <param name="aciEntryType"></param>
        /// <param name="isSucc"></param>
        void InnerEVGM(OceanBLList selectedList, Guid companyID, ref bool isSucc, object businessOperationContext,
                       PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 显示EDI数据
        /// </summary>
        /// <param name="ediType">类型</param>
        /// <param name="IDS">ID集合</param>
        /// <returns></returns>
        bool InnerGetEDIDataSourceForNBEDIInfos(int ediType, Guid[] IDS);

        /// <summary>
        /// CopyAMSTo
        /// </summary>
        void CopyAMSTo(WorkItem workItem, OceanBLList oceanBLList);


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
        void SendEDI(string key, string subjuect, List<Guid> oIds, List<Guid> hblIds, List<string> operationNos,
                            bool isSucc, object ediMode, Guid CompanyID, object businessOperationContext, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        void MailSoConfirmationToCustomer(Guid oceanBookingId, bool isEnglish);

        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <param name="mailInformation">邮件地址</param>
        /// <param name="isEnglish">发送邮件版本</param>
        void MailSoConfirmationToCustomer(Guid oceanBookingId, string mailInformation, bool isEnglish);

        /// <summary>
        /// SOD成功以后发送邮件给客服
        /// </summary>
        /// <param name="oceanBookingId">业务id</param>
        /// <param name="memoType">备注类型</param>
        /// <param name="path">操作路径</param>
        /// <param name="description">事件描述</param>
        void MainSOCustomerServiceSOD(Guid oceanBookingId, MemoType memoType, string path, bool eventFlg);
        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID </param>
        string MailSoConfirmationToAgent(Guid oceanBookingId);
        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID </param>
        /// <param name="mailInformation">邮件地址</param>
        string MailSoConfirmationToAgent(Guid oceanBookingId, string mailInformation);

        /// <summary>
        /// 邮件订舱
        /// </summary>
        /// <param name="oceanBookIngidList">业务号集合</param>
        void CommunicationMailBooking(List<Guid> oceanBookIngidList);

        /// <summary>
        /// 订单变更发送邮件(订单列表)
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <param name="oldstring">原始数据信息</param>
        /// <param name="updatestring">修改的数据信息</param>
        void MainOrderChangedOrderInfo(Guid orderID, Guid companyID, string oldstring, string updatestring);
        /// <summary>
        /// 询价-商务员邮件(订单列表)
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <param name="inquiryno">询价号</param>
        void MainOrderChangedInquir(Guid orderID, Guid companyID, string inquiryno);

        /// <summary>
        /// 订舱变更发送邮件(订舱列表)
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="oldstring">原始数据信息</param>
        /// <param name="updatestring">修改的数据信息</param>
        void MainBookIngChangedOceanBookingInfo(Guid oceanBookingId, string oldstring, string updatestring);

        /// <summary>
        /// 询价-商务员邮件(订舱列表)
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="inquiryno">询价号</param>
        void MainBookIngChangedInquir(Guid oceanBookingId, string inquiryno);

        /// <summary>
        /// 手动勾选SOD事件以后发送邮件给客服
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="eventObjects">事件实体</param>
        void CheckMainSOCustomerServiceSOD(Guid oceanBookingId, EventObjects eventObjects);




        /// <summary>
        /// 打开联系人面板
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationNo">业务号</param>

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
        /// <param name="TemplateCode">节点名称</param>
        void Upload(UploadWay way, Guid operationId, string mailSubject, object objMailItem,
            SelectionType type, OperationType operationType,
            List<string> addDocList, string operationNo, DateTime? upDateTime, Message.ServiceInterface.Message message, string TemplateCode);
        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>

        void ShowContactPart(AssociateType associateType, List<BusinessOperationContext> bussOperationContexts,
            Message.ServiceInterface.Message message);

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="contactType">联系人类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
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

        void OeEmailQueryPartShow(string no, int noType, int types, int dateType, string main, int? area, string text, Guid? operationId, OperationType operationType);
        /// <summary>
        /// 邮件中心是否刷新
        /// </summary>
        /// <returns></returns>

        bool MailCenterRefresh(bool? flg);
        /// <summary>
        /// 高级查询
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>

        string Advancedquery(OperationType operationType);
        /// <summary>
        /// 判断当前联系人是否存在 
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="message">邮件</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="operation">业务类型</param>
        /// <returns></returns>

        bool SaveOperationMessage(AssociateType associateType, Message.ServiceInterface.Message message,
            Guid operationId, ICP.Framework.CommonLibrary.Common.OperationType operation);

        #region Comment Code
        /// <summary>
        /// 订舱变更发送邮件(订单列表)
        /// </summary>
        /// <param name="oceanBookingId">业务的ID</param>
        /// <param name="oldstring">原始数据信息</param>
        /// <param name="updatestring">修改的数据信息</param>
        void MainBookIngChangedOrderInfo(Guid oceanBookingId, string oldstring, string updatestring);
        #endregion
    }
}

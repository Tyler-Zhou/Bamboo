namespace ICP.FCM.OceanExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
    using System.ServiceModel;
    using System.Data;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.FCM.Common.ServiceInterface.CompositeObjects;

    /// <summary>
    /// 海运出口订舱服务
    /// </summary>
    [ServiceContract]
    public interface IOceanExportBookingService
    {
        /// <summary>
        /// 以事务方式保存订舱单、费用和PO
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="pos"></param>
        /// <param name="fees"></param>
        /// <param name="delegates"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveOceanBookingWithTrans(BookingSaveRequest saveRequest,
            List<FeeSaveRequest> fees,List<SaveRequestBookingDelegate> delegates);

        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveCsclBookingInfo(CSCLBookingSaveRequest csclBookingInfo);

        /// <summary>
        /// 获取中海EDI订舱信息
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        CSCLBookingInfo GetCsclBookingInfo(Guid bookingID);

        /// <summary>
        /// 获取当前客户最近的海外部客服列表
        /// 如果当前客户为空，就返回揽货人最近业务所对应的海外部客服的列表。
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="salesId">揽货人ID</param>
        ///<param name="beginTime">开始时间</param>
        ///<param name="endTime">结束时间</param>
        ///<param name="maxRecords">最大行数</param>
        /// <returns>用户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserInfo> GetOverseasFilersList(Guid? customerId, Guid? salesId, DateTime beginTime, DateTime endTime, int maxRecords);

        /// <summary>
        /// 获取当前客户最近业务所对应的文件or 当前客户为新客户and当前揽货人最近业务所对应的文件
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="salesId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserInfo> GetFilersList(Guid? customerId, Guid? salesId, Guid? companyID, DateTime beginTime, DateTime endTime, int maxRecords);

        /// <summary>
        /// 获取订舱列表
        /// </summary>
        /// <param name="companyIDs">业务所在口岸公司(操作的公司)</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="shippingOrderNo">订舱号</param>
        /// <param name="scno">合约号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="agentOfCarrierName">承运人名</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地</param>
        /// <param name="bookingRefNo">BookingRefNo</param>
        /// <param name="salesID">业务员</param>
        /// <param name="bookingerID">订舱</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBookingList> GetOceanBookingList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string ctnNo,
            string shippingOrderNo,
            string scno,
            string cusClearanceNo,
            string customerName,
            string carrierName,
            string agentOfCarrierName,
            string vesselName,
            string voyageNo,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string bookingRefNo,
            Guid? salesID,
            Guid? bookingerID,
            Guid? overseasFilerID,
            bool? isValid,
            OEOrderState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);

        /// <summary>
        /// 获取订舱列表
        /// </summary>
        /// <param name="bookingIDs">订舱单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBookingList> GetOceanBookingListByIds(Guid[] bookingIDs);

        /// <summary>
        /// 获取订舱列表
        /// </summary>
        /// <param name="companyIDs">业务所在口岸公司(操作的公司)</param>
        /// <param name="noSearchType">单号搜索类型(0:全部,1:业务号,2:提单号,3:箱号,4:订舱号,5:合约号)</param>
        /// <param name="no">号码</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部,1:客户,2:船东,3:承运人,4:发货人,5:收货人,6:通知人,7:对单人)</param>
        /// <param name="customerName">客户名</param>
        /// <param name="portSearchType">港口搜索类型(0:全部,1:收货地,2:装货港,3:卸货港,4:交货地,5:最终目的地)</param>
        /// <param name="portName">港口名</param>
        /// <param name="dateSearchType">日期搜索类型(0:全部,1:离港日,2:到港日,3:订舱日,4:创建日,)</param>
        /// <param name="bookingerId">订舱</param>
        /// <param name="isValid"></param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBookingList> GetOceanBookingListForFaster(
            Guid[] companyIDs,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            DateSearchType dateSearchType,
            Guid? bookingerId,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取订舱信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回订舱信息</returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBookingInfo GetOceanBookingInfo(Guid id);

        /// <summary>
        /// 根据口岸获取航线ID
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Guid GetShippingForPort(Guid port);

        /// <summary>
        /// 保存订舱信息
        /// </summary>
        /// <param name="saveRequest">参数(详见对象注泽)</param>
        /// <returns>返回SingleResultDataWithNo</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveOceanBookingInfo(BookingSaveRequest saveRequest);

        /// <summary>
        /// 获取ShippingOrder列表
        /// </summary>
        /// <param name="oeOperationType">业务类型</param>
        /// <param name="POLID">装货港</param>
        /// <param name="PODID">卸货港</param>
        /// <param name="PlaceOfDeliveryID">交货地</param>
        /// <param name="operatorID">客服</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回ShippingOrder列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ShippingOrderList> GetShippingOrderList(
            FCMOperationType oeOperationType,
            Guid? POLID,
            Guid? PODID,
            Guid? PlaceOfDeliveryID,
            Guid? operatorID,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);

        /// <summary>
        /// 产生账单
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult CreateBill(Guid operationID, Guid saveById);

        /// <summary>
        /// 产生利润
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ProfitContainerObjects GetOceanProfitReportData(Guid OperationID);

        /// <summary>
        /// 根据业务的ID查找联系人的邮箱地址
        /// </summary>
        /// <param name="id">业务号</param>
        /// <param name="tyep">类型</param>
        /// <param name="customerId">代理的ID</param>
        /// <param name="notequal">需要执行查询当前联系人关联客户不属于当前业务的代理</param>
        /// <param name="equal">需要执行查询当前联系人关联客户属于当前业务的代理</param>
        /// <returns>返回当前联系人的邮箱地址</returns>
        [FunctionInfomation]
        [OperationContract]
        List<MailInformation> GetContactEmail(Guid id, string tyep, Guid? customerId, bool notequal, bool equal);
        /// <summary>
        /// 读取邮件模版的注意事项
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DataTable GetEmailNotice(Guid organizationsId);

        /// <summary>
        /// 根据业务ID返回当前业务客服的联系信息
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        UserInfo GetBookingerId(Guid oceanBookingId);
        /// <summary>
        /// 执行修改SOPV的值
        /// </summary>
        /// <param name="oceanBooking">订单的ID</param>
        [FunctionInfomation]
        [OperationContract]
        bool SetUpdateOceanTrackings(Guid oceanBooking);

        /// <summary>
        /// EmailBooking
        /// </summary>
        /// <param name="BookingID"></param>
        /// <returns>DataSet</returns>
        [FunctionInfomation]
        [OperationContract]
        DataSet GetEmailBookingDataSetByBookingID(Guid BookingID, Guid sendID);

        /// <summary>
        /// 获取Email订舱/补料配置
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<EmailBookingSIConfig> GetEmailBookingSIConfig(EDIMode ediMode);

        /// <summary>
        /// 根据卸货港和交货地得到航线
        /// </summary>
        /// <param name="placeOfDeliveryId">交货地</param>
        /// <param name="podId">卸货港</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ShippingLineList GetShippingLineForDeliveryAndPOD(Guid placeOfDeliveryId, Guid podId);


        /// <summary>
        /// 修改订舱单的状态
        /// </summary>
        /// <param name="updateBy">修改人</param>
        /// <param name="operationId">业务的ID</param>
        /// <param name="oeOrderState">业务状态</param>
        [FunctionInfomation]
        [OperationContract]
        bool UpdateOceanBookingsState(Guid updateBy, Guid operationId, OEOrderState oeOrderState);


        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="oceanBookid">业务号</param>
        /// <param name="iscopyorder">复制订舱单</param>
        /// <param name="iscopyshipment">复制提单</param>
        /// <param name="iscopybill">复制账单</param>
        /// <param name="saveby">复制人</param>
        /// <param name="IsEnglish">是否英语</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult CopyOperationInfo(Guid oceanBookid, bool iscopyorder, bool iscopyshipment, bool iscopybill, Guid saveby, bool IsEnglish);


        /// <summary>
        /// 获取业务的客服，订舱员，文件的邮件地址(只针对于变跟订舱的操作使用)
        /// </summary>
        /// <param name="oceanBookid">业务的ID</param>
        /// <param name="userId">当前用户的ID</param>
        /// <param name="logo">0 表示查询业务的操作人的信息，1表示查询商务员的信息</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserInfo> GetOceanOperatorEmailAddress(Guid oceanBookid, Guid userId, int logo);

       /// <summary>
       /// 获得未完成AMS列表
       /// </summary>
       /// <param name="userId">用户ID</param>
       /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<NotAMSList> GetNotAmsListForUser(Guid userId);

        /// <summary>
        /// 更新AMS状态
        /// </summary>
        /// <param name="oceanbookingid">海运业务ID</param>
        /// <param name="isAms">是否AMS</param>
        /// <param name="IsEnglish">是否英文</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void ChangeAmsState(Guid oceanbookingid, bool isAms, bool IsEnglish);
    }
}

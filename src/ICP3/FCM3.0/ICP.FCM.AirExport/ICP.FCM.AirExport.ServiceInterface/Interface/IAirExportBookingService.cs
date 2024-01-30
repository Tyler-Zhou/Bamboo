namespace ICP.FCM.AirExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.AirExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
    using System.ServiceModel;

    /// <summary>
    /// 空运出口订舱服务
    /// </summary>
    [ServiceInfomation("空运出口订舱服务")]
    [ServiceContract]
    public interface IAirExportBookingService
    {
        /// <summary>
        /// 以事务方式保存订舱单、费用和PO
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="pos"></param>
        /// <param name="fees"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveAirBookingWithTrans(BookingSaveRequest saveRequest, 
            List<FeeSaveRequest> fees);

        /// <summary>
        /// 获取当前客户最近的空外部客服列表
        /// 如果当前客户为空，就返回揽货人最近业务所对应的空外部客服的列表。
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="salesId">揽货人ID</param>
        ///<param name="beginTime">开始时间</param>
        ///<param name="endTime">结束时间</param>
        ///<param name="maxRecords">最大行数</param>
        /// <returns>用户列表</returns>
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
        [OperationContract]
        List<UserInfo> GetFilersList(Guid? customerId, Guid? salesId, DateTime beginTime, DateTime endTime, int maxRecords);

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
        List<AirBookingList> GetAirBookingList(
         Guid[] companyIDs,
         string operationNo,
         string blNo,
            //string ctnNo,
            //string shippingOrderNo,
         string scno,
         string customerName,
         string airlineName,
         string agentOfCarrierName,
            //string vesselName,
         string flightNo,
         string polName,
         string podName,
         string finalDestination,
         Guid? salesID,
         Guid? bookingerID,
         bool? isValid,
         AEOrderState? state,
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
        List<AirBookingList> GetAirBookingListByIds(Guid[] bookingIDs);

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
        List<AirBookingList> GetAirBookingListForFaster(
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
        AirBookingInfo GetAirBookingInfo(Guid id);

        /// <summary>
        /// 保存订舱信息
        /// </summary>
        /// <param name="saveRequest">参数(详见对象注泽)</param>
        /// <returns>返回SingleResultDataWithNo</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAirBookingInfo(BookingSaveRequest saveRequest);

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
            Guid? POLID,
            Guid? PODID,
            Guid? PlaceOfDeliveryID,
            Guid? operatorID,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);
    }
}

using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.AirExport.ServiceInterface
{
    /// <summary>
    /// 空运出口订单服务
    /// </summary>
    [ServiceInfomation("空运出口订单服务")]
    [ServiceContract]
    public interface IAirExportOrderService
    {
        #region 以事务方式保存订单、费用和PO
        /// <summary>
        /// 以事务方式保存订单、费用和PO
        /// </summary>
        /// <param name="saveRequest">OrderSaveRequest</param>
        /// <param name="pos">FeeSaveRequest</param>
        /// <param name="fees">POSaveRequest</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveAirOrderWithTrans(OrderSaveRequest saveRequest,
            List<FeeSaveRequest> fees, List<POSaveRequest> pos); 
        #endregion

        #region 根据口岸公司和客户获取最近该客户的业务数据列表
        /// <summary>
        /// 根据口岸公司和客户获取最近该客户的业务数据列表
        /// </summary>
        /// <param name="companyID">口岸公司</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回最近该客户的业务数据列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderList> GetRecentlyAirOrderList(
            Guid companyID,
            Guid customerID,
            int maxRecords); 
        #endregion

        #region 获取订单列表
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="no">业务号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地名</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="isValid">是否有效？</param>
        /// <param name="orderState">订单状态()</param>
        /// <param name="salesID">业务员</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderList> GetAirOrderList(
            Guid[] companyIDs,
            string no,
            string customerName,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string carrierName,
            bool? isValid,
            AEOrderState? orderState,
            Guid? salesID,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords); 
        #endregion

        #region 刷新订单列表
        /// <summary>
        /// 刷新订单列表
        /// </summary>
        /// <param name="orderIDs">orderIDs</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <returns>返回订单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderList> GetAirOrderListById(
            Guid[] orderIDs,Guid[] companyIDs); 
        #endregion

        #region 快速搜索订单列表
        /// <summary>
        /// 快速搜索订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="noSearchType">单号搜索类型(0:全部,1:业务号,2:提单号,3:箱号,4:订舱号,5:合约号)</param>
        /// <param name="no">号码</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部,1:客户,2:船东,3:承运人,4:发货人,5:收货人,6:通知人,7:对单人)</param>
        /// <param name="customerName">客户名</param>
        /// <param name="portSearchType">港口搜索类型(0:全部,1:收货地,2:装货港,3:卸货港,4:交货地,5:最终目的地)</param>
        /// <param name="portName">港口名</param>
        /// <param name="dateSearchType">日期搜索类型(0:全部,1:离港日,2:到港日,3:订舱日,4:创建日,)</param>
        /// <param name="salesId">揽货人</param>
        /// <param name="isValid"></param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderList> GetAirOrderListForFaster(
            Guid[] companyIDs,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            DateSearchType dateSearchType,
            Guid salesId,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords); 
        #endregion

        #region 获取订单信息
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="companyID">companyID</param>
        /// <returns>返回订单信息</returns>
        [FunctionInfomation]
        [OperationContract]
        AirOrderInfo GetAirOrderInfo(Guid id,Guid companyID); 
        #endregion

        #region 保存订单信息
        /// <summary>
        /// 保存订单信息
        /// </summary>
        /// <param name="saveRequest">详见数据对象</param>
        /// <returns>返回SinglieResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAirOrderInfo(OrderSaveRequest saveRequest); 
        #endregion

        /// <summary>
        /// 改变订单状态        
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="isValid">是否取消订舱</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeAirOrderState(
            Guid orderID,Guid companyID,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="isCancel">是否取消(true为取消,false为激活),设置fcm.AirBookings.isValid</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult CancelAirOrder(
            Guid orderID, Guid companyID,
            bool isCancel,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变订单状态        
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="state">状态(2:已打回,3:已审核,4:已订舱,5:已装驳船,6:已装大船,7:已关单)</param>
        /// <param name="reason">原因 ,在备忘录中记录打回原因</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeAirOrderStateWithTargetState(
            Guid orderID, Guid companyID,
            AEOrderState state,
            string reason,
            Guid changeByID,
            DateTime? updateDate);

    }
}
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ServiceModel;

namespace ICP.FCM.AirImport.ServiceInterface
{
    /// <summary>
    /// 空运进口订单服务类
    /// </summary>
    [ServiceInfomation("空运进口订单服务")]
    [ServiceContract]
    public interface IAirImportOrderService
    {
        #region 以事务的方式保存订单、费用、PO
        /// <summary>
        /// 以事务方式保存订单、费用和PO
        /// </summary>
        /// <param name="orderSaveRequest"></param>
        /// <param name="feeList"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveAIOrderWithTransByOrder")]
        Dictionary<Guid, SaveResponse> SaveAIOrderWithTrans(
                         OrderSaveRequest orderSaveRequest,
                         List<FeeSaveRequest> feeList);
       
        #endregion

        #region 获得订单信息
        /// <summary>
        /// 获得订单列表--根据ID集合
        /// </summary>
        /// <param name="orderIds">订单ID集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderList> GetAIOrderListByIds(Guid[] orderIds,Guid[] companyIDs);

        /// <summary>
        /// 获得订单列表--从快速查询
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="noSearchType">单号查询类型(0:全部、1:业务号)</param>
        /// <param name="no">单号</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部、1:客户、2:船公司、3:承运人、4:发货人、5:收货人、6:通知人、7:对单人、8代理:)</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="portSearchType">地点搜索类型(0:全部、1:收货地、2:装货港、3:中转港、4:卸货港、5:交货地、6:最终目的地)</param>
        /// <param name="portName">地点名称</param>
        /// <param name="dateSearchType">时间搜索类型(0:全部、1:创建日、2:放单日、3:离港日、4:、到港日)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderList> GetAIOrderListByFastSearch(
                                            Guid[] companyIDs,
                                            Guid salesID,
                                            OIBusinessNoSearchType noSearchType,
                                            string no,
                                            OIBusinessCustomerSearchType customerSearchType,
                                            string customerName,
                                            OIBusinessPortSearchType portSearchType,
                                            string portName,
                                            OIBusinessDateSearchType dateSearchType,
                                            DateTime? beginTime,
                                            DateTime? endTime,
                                            int maxRecords);

        /// <summary>
        /// 获得订单列表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="no">业务号</param>
        /// <param name="agentName">代理名称</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="carrierName">船公司</param>
        /// <param name="pol">装货港</param>
        /// <param name="pod">卸货港</param>
        /// <param name="placeOfDelivery">交货地</param>
        /// <param name="salesID">揽货人</param>
        /// <param name="state">状态搜索类型(1:新订单、2:已打回、3:已审核、4:已取消、5: 已订舱、6:已装船、7:已发送到港通知、8:已放货、9:已关单)</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <param name="dateSearchType">时间搜索类型(0:全部、1:创建日、2:放单日、3:离港日、4:、到港日)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderList> GetAIOrderList(Guid[] companyIDs,
                                            string no,
                                            string agentName,
                                            string customerName,
                                            string carrierName,
                                            string pol,
                                            string pod,
                                            string placeOfDelivery,
                                            Guid? salesID,
                                            AIOrderState? state,
                                            bool? isValid,                                           
                                            OIBusinessDateSearchType dateSearchType,
                                            DateTime? beginTime,
                                            DateTime? endTime,
                                            int maxRecords);

        /// <summary>
        /// 获得订单详细信息
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AirOrderInfo GetAIOrderInfo(Guid id,Guid companyID);

        #endregion

        #region 取消订单

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="isCancel">是否取消(True为取消;Flase为激活)</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult CancelAIOrder(
               Guid orderID,Guid companyID,
               bool isCancel,
               Guid changeByID,
               DateTime? updateDate);

        #endregion

        #region 保存
        /// <summary>
        /// 保存订单信息
        /// </summary>
        /// <param name="saveRequest">订单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAIOrderInfo(OrderSaveRequest saveRequest);
        #endregion

        #region 改变订单状态
        /// <summary>
        /// 改变订单状态        
        /// </summary>
        /// <param name="orderID">委托ID</param>
        /// <param name="state">状态类型(1:新订单、2:已打回、3:已审核、4:已取消、5: 已订舱、6:已装船、7:已发送到港通知、8:已放货、9:已关单)</param>
        /// <param name="memoContent">备注内容</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeAIOrderState(
            Guid orderID, Guid companyID,  
            AIOrderState state,
            string memoContent,
            Guid changeByID,
            DateTime? updateDate);

        #endregion

        #region 根据客户获得最近的海外部客服列表
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
        List<UserInfo> GetAIOverseasFilersList(Guid? customerId, Guid? salesId, DateTime? beginTime, DateTime? endTime, int maxRecords);

        #endregion

        #region 判断客户和公司是否在同一国家
        /// <summary>
        /// 判断客户和公司是否在同一个国家
        /// </summary>
        /// <param name="customerId">客户ID( 用cusomterId获得客户的国家ID)</param>
        /// <param name="companyId">口岸公司ID(compnyId用于在获取配置里对应的客户的国家ID)</param>
        /// <returns>是否在同一个国家</returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsCustomerAndCompanySameCountry(Guid customerId, Guid companyId);

        #endregion

        #region 根据销售客户和口岸公司获得揽货方式
        /// <summary>
        /// 根据销售客户和口岸公司获取揽货方式
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyId">口岸公司</param>
        /// <returns>返回揽货方式</returns>
        [FunctionInfomation]
        [OperationContract]
        ICP.Common.ServiceInterface.DataObjects.DataDictionaryInfo GetSalesType(
            Guid customerID,
            Guid companyId);

        #endregion

        #region 根据口岸公司和客户获得该客户最近的业务数据列表
        /// <summary>
        /// 根据口岸公司和客户获取最近该客户的业务数据列表
        /// </summary>
        /// <param name="companyID">口岸公司</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回最近该客户的业务数据列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirOrderInfo> GetAIRecentlyOrderList(
            Guid companyID,
            Guid customerID,
            Guid salesID,
            int maxRecords);

        #endregion

    }
}

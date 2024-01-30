#region Comment

/*
 * 
 * FileName:    IQuotedPriceOrderService.cs
 * CreatedOn:   2016/6/15 14:35:56
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

namespace ICP.FCM.Common.ServiceInterface
{
    using System.Collections.Generic;
    using DataObjects;
    using Framework.CommonLibrary.Attributes;
    using Framework.CommonLibrary.Common;
    using System;
    using System.ServiceModel;

    /// <summary>
    /// 报价单管理
    /// </summary>
    [ServiceInfomation("报价单管理")]
    [ServiceContract]
    public interface IQuotedPriceService
    {
        /// <summary>
        /// 以事务方式保存报价单、报价价格信息
        /// </summary>
        /// <param name="saveRequest">OrderSaveRequest</param>
        /// <param name="rates">QPRatesSaveRequest</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveQuotedPriceWithTrans(QPOrderSaveRequest saveRequest,
            List<QPRatesSaveRequest> rates);

        /// <summary>
        /// 保存报价信息
        /// </summary>
        /// <param name="saveRequest">报价实体对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveQuotedPriceOrderInfo(QPOrderSaveRequest saveRequest);

        /// <summary>
        /// 作废报价信息
        /// </summary>
        /// <param name="ID">报价ID</param>
        /// <param name="isCancel">是否作废</param>
        /// <param name="removeByID">作废人</param>
        /// <param name="updateDate">更新时间</param>
        [FunctionInfomation]
        [OperationContract]
        SingleResult RemoveQuotedPriceOrderInfo(Guid ID, bool isCancel, Guid removeByID, DateTime? updateDate);

        /// <summary>
        /// 获取报价信息
        /// </summary>
        /// <param name="quotedPriceID">报价ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        QuotedPriceOrderInfo GetQuotedPriceOrderInfo(Guid quotedPriceID);

        /// <summary>
        /// 获取报价列表
        /// </summary>
        /// <param name="no">业务号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="isSure">是否已经确认</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="quoteBy">报价人</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回报价列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<QuotedPriceOrderList> GetQuotedPriceOrderList(string no, string customerName, bool? isSure, bool? isValid, Guid? quoteBy, DateTime? beginTime, DateTime? endTime, int maxRecords);

        /// <summary>
        /// 报价单快速搜索
        /// </summary>
        /// <param name="noSearchType">查找单号类型</param>
        /// <param name="no">单号</param>
        /// <param name="portSearchType">港口搜索类型</param>
        /// <param name="portName">港口名称</param>
        /// <param name="dateSearchType">日期搜索类型</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录行数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<QuotedPriceOrderList> GetQuotedPriceListForFaster(
            QPNoSearchType noSearchType,
            string no,
            QPPortSearchType portSearchType,
            string portName,
            QPDateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords);
        /// <summary>
        /// 获取报价列表：刷新数据
        /// </summary>
        /// <param name="QuotedPriceIDs">报价ID集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<QuotedPriceOrderList> GetQuotedPriceListByIds(Guid[] QuotedPriceIDs);

        #region 获取最近该客户的报价单列表
        /// <summary>
        /// 获取最近该客户的报价单列表
        /// </summary>
        /// <param name="quotedPriceID">报价单ID</param>
        /// <param name="no">单号</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="quoteBy">揽货人(报价人)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回最近该客户的业务数据列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<QuotedPricePartInfo> GetRecentlyQuotedPriceList(Guid? quotedPriceID, string no, Guid? customerID, Guid? quoteBy, DateTime? beginTime, DateTime? endTime, int maxRecords);

        #endregion
        
        #region 获取报价价格列表

        /// <summary>
        /// 获取报价价格列表
        /// </summary>
        /// <param name="qpOrderID">报价单ID</param>
        /// <returns>返回报价价格列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<QuotedPriceRatesList> GetQuotedPriceRatesList(Guid qpOrderID);
        #endregion

        #region 保存报价价格
        /// <summary>
        /// 保存报价价格
        /// </summary>
        /// <param name="rates">QPRatesSaveRequest</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveQuotedPriceRatesList(QPRatesSaveRequest rates);
        #endregion

        #region 删除报价价格
        /// <summary>
        /// 删除报价价格
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveQuotedPriceRatesList(Guid[] ids, Guid removeByID, DateTime?[] updateDates);
        #endregion

        #region 获取报价单报表数据
        /// <summary>
        /// 获取报价单报表数据
        /// </summary>
        /// <param name="orderID">报价单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        QPOrderReportData GetQPOrderReportData(Guid orderID);   
        #endregion
    }
}

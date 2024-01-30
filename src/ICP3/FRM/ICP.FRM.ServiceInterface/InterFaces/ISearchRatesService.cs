using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 查询运价接口
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ISearchRatesService
    {
        #region 查询海运运价

        #region 获得当前可用的船东、承运人
        /// <summary>
        /// 获得当前可用的船东/承运人
        /// </summary>
        /// <param name="shippingLineIDs">航线集合</param>
        /// <param name="statue">状态</param>
        /// <param name="begingDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>船东列表</returns>
       [FunctionInfomation]  [OperationContract]
        Dictionary<Guid, string> GetOceanRateCarrierList(SearchPriceStatus statue,DateTime? begingDate,DateTime? endDate, Guid[] shippingLineIDs);

        /// <summary>
        /// 获得当前可用的船东/承运人
        /// </summary>
        /// <param name="shippingLineIDs">航线集合</param>
        /// <param name="statue">状态</param>
        /// <returns>船东列表</returns>
       [FunctionInfomation]  [OperationContract]
        Dictionary<Guid, string> GetAirRateCarrierList(SearchPriceStatus statue,DateTime? beginDate,DateTime? endDate ,Guid[] shippingLineIDs);

        /// <summary>
        /// 获得当前可用的船东/承运人
        /// </summary>
        /// <param name="shippingLineIDs">航线集合</param>
        /// <param name="statue">状态</param>
        /// <returns>船东列表</returns>
       [FunctionInfomation]  [OperationContract]
        Dictionary<Guid, string> GetTruckRateCarrierList(SearchPriceStatus statue, DateTime? beginDate, DateTime? endDate, Guid[] shippingLineIDs);

        #endregion

        #region 获得当前可用的POL、POD、Delivery
        /// <summary>
        /// 获得当前可用的POL、POD、Delivery
        /// </summary>
        /// <param name="carrierIDs">船东集合</param>
        /// <param name="shipperIDs">航线集合</param>
        /// <param name="statue">状态</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        List<SearchPortList> GetPortList(SearchPriceStatus statue, DateTime? begingDate,DateTime? endDate,Guid[] shipperIDs, Guid[] carrierIDs);
        #endregion

        #region 获得当前可用的Commodity
        /// <summary>
        /// 获得当前可用的Commodity
        /// </summary>
        /// <param name="statue">状态</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        Dictionary<Guid, string> GetCommodityList(SearchPriceStatus statue,DateTime? beginDate,DateTime? ednDate);
        #endregion

        #region 获得海运运价查询列表

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="polIDs">POL ID集合</param>
        /// <param name="podIDs">POD ID集合</param>
        /// <param name="deliveryIDs">Delivery ID集合</param>
        /// <param name="commoditys">Commodity ID集合</param>
        /// <param name="contractNo">ContractNo</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="rateType">运价类型</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetSearchOceanListByWebCRM")]
        PageList GetSearchOceanListByWebCRM(
             Guid userID,
             Guid[] shiplineIDs,
             Guid[] carrierIDs,
             string[] polNames,
             string[] podNames,
             string[] deliveryNames,
             string[] finalDestinationNames,
             string[] commoditys,
             string contractNo,
             int pageSize,
             DateTime? durationStart,
             DateTime? durationEnd,
             SearchPriceStatus status,
             OceanTypeBySearch rateType,
             DataPageInfo dataPageInfo);

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="polIDs">POL ID集合</param>
        /// <param name="podIDs">POD ID集合</param>
        /// <param name="deliveryIDs">Delivery ID集合</param>
        /// <param name="commoditys">Commodity ID集合</param>
        /// <param name="contractNo">ContractNo</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="rateType">运价类型</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <returns></returns>
        [FunctionInfomation]
       [OperationContract(Name = "GetSearchOceanListBySearch")]
        PageList GetSearchOceanList(
            Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string[] polNames,
            string[] podNames,
            string[] deliveryNames,
            string[] finalDestinationNames,
            string[] commoditys,
            string contractNo,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            OceanTypeBySearch rateType,
            DataPageInfo dataPageInfo);

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>SearchOceanRateList</returns>
       [FunctionInfomation]
       [OperationContract(Name = "GetSearchOceanListByIds")]
        List<SearchOceanRateList> GetSearchOceanList(Guid[] Ids);

        #endregion

        #region 查询海运BaseInfo

        /// <summary>
        /// 查询海运BaseInfo
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="permissionType">权限类型:区分通用、底价</param>
        /// <param name="searchRateType">运价查询:区分合约、询价</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        SearchOceanBaseInfo GetSearchOceanBaseInfo(
                                        Guid id,
                                        SearchRateType searchRateType);

        [FunctionInfomation]
        [OperationContract]
       List<SearchOceanRateList> GetSearchOceanListFromInquirePrice(Guid[] ids);
        #endregion

        #region 查询海运运价SearchOceanContractInfo
        /// <summary>
        /// 查询查询海运运价SearchOceanContractInfo
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="searchRateType">运价查询:区分合约、询价</param>
        /// <returns>SearchOceanContractInfo</returns>
       [FunctionInfomation]  [OperationContract]
        SearchOceanContractInfo GetSearchOceanContractInfo(
                                Guid id,
                                 SearchRateType searchRateType);
        #endregion

       #region 导出到Excel
        /// <summary>
        /// 将运价导出到Excel
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="shiplineIDs"></param>
        /// <param name="carrierIDs"></param>
        /// <param name="polIDs"></param>
        /// <param name="podIDs"></param>
        /// <param name="deliveryIDs"></param>
        /// <param name="commoditys"></param>
        /// <param name="contractNo"></param>
        /// <param name="durationStart"></param>
        /// <param name="durationEnd"></param>
        /// <param name="status"></param>
        /// <param name="rateType"></param>
        /// <param name="dataPageInfo"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract(Name="SearchOcean")]
       OceanRateToExcel ExportOceanRateToExcelBySearchOcean(Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string[] polNames,
            string[] podNames,
            string[] deliveryNames,
            string[] finalDestinationNames,
            string[] commoditys,
            string contractNo,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            OceanTypeBySearch rateType,
            DataPageInfo dataPageInfo);
           #endregion

        #endregion

       #region 查询空运运价

       #region 获得空运运价查询列表
       /// <summary>
        /// 获得空运运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="pol">pol</param>
        /// <param name="pod">pod</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <returns>SearchAirRateList</returns>
       [FunctionInfomation]
       [OperationContract(Name = "GetSearchAirListByList")]
        PageList GetSearchAirList(
            Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string pol,
            string pod,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            DataPageInfo dataPageInfo);
        #endregion

        #region 获得空运运价查询列表
        /// <summary>
        /// 获得空运运价查询列表
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>SearchAirRateList</returns>
        [FunctionInfomation]
       [OperationContract(Name = "GetSearchAirListByIds")]
        List<SearchAirRateList> GetSearchAirList(Guid[] Ids);
        #endregion

        #endregion

        #region 查询空运运价

        #region 获得拖车运价查询列表
        /// <summary>
        /// 获得拖车运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="pol">pol</param>
        /// <param name="pod">pod</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <returns>SearchAirRateList</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetSearchTruckListByList")]
        PageList GetSearchTruckList(
            Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string pol,
            string pod,
            string zipcode,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            DataPageInfo dataPageInfo);
        #endregion

        #region 获得空运运价查询列表
        /// <summary>
        /// 获得拖车运价查询列表
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>SearchAirRateList</returns>
       [FunctionInfomation]
        [OperationContract(Name = "GetSearchTruckListByIds")]
        List<SearchTruckRateList> GetSearchTruckList(Guid[] Ids);
        #endregion

        #endregion
    }
}

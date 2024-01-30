

//-----------------------------------------------------------------------
// <copyright file="ITransportFoundationService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;

    /// <summary>
    /// 基础数据服务管理
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ITransportFoundationService
    {
        /// <summary>
        /// 获取箱信息列表

        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回箱信息列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ContainerList> GetContainerList(
            string code,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取箱详细信息

        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回箱详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ContainerInfo GetContainerInfo(Guid id);

        /// <summary>
        /// 保存箱信息

        /// </summary>
        /// <param name="id">id</param>
        /// <param name="code">代码</param>
        /// <param name="isoCode">标准代码</param>
        /// <param name="description">描述</param>
        /// <param name="teu">系集装箱运量统计单位</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveContainerInfo(
            Guid? id,
            string code,
            string isoCode,
            string description,
            decimal teu,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变箱信有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">修改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeContainerState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取运输条款列表
        /// </summary>
        /// <param name="originalCode">出发地代码</param>
        /// <param name="destinationCode">目的地代码</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回运输条款列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<TransportClauseList> GetTransportClauseList(
            string originalCode,
            string destinationCode,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取运输条款明细信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回运输条款明细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        TransportClauseInfo GetTransportClauseInfo(Guid id);

        /// <summary>
        /// 保存运输条款信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="originalCodeID">出发地代码</param>
        /// <param name="destinationCodeID">目的地代码</param>
        /// <param name="description">描述</param>
        /// <param name="saveById">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveTransportClauseInfo(
            Guid? id,
            Guid originalCodeID,
            Guid destinationCodeID,
            string description,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变运输条款状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeTransportClauseState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取品名列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回品名列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CommodityList> GetCommodityList(
            string name,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取品名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回品名信息</returns>
        [FunctionInfomation]
        [OperationContract]
        CommodityInfo GetCommodityInfo(Guid id);

        /// <summary>
        /// 保存品名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="remark">备注</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SaveCommodityInfo(
            Guid? id,
            Guid? parentID,
            string cName,
            string eName,
            string remark,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 设置品名父节点

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="setById">设置人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SetCommodityParent(
            Guid id,
            Guid? parentID,
            Guid setById,
            DateTime? updateDate);

        /// <summary>
        /// 更改品名有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">更改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData ChangeCommodityState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取航线列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回航线列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetShippingLineListByList")]
        List<ShippingLineList> GetShippingLineList(
            string code,
            string name,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取航线列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isParent">是否总航线</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回航线列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetShippingLineListBySearch")]
        List<ShippingLineList> GetShippingLineList(
            string code,
            string name,
            bool? isValid,
            bool? isParent,
            int maxRecords);

        /// <summary>
        /// 获取航线信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回航线信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ShippingLineInfo GetShippingLineInfo(Guid id);

        /// <summary>
        /// 保存航线信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveShippingLineInfo(
            Guid? id,
            Guid ParentID,
            string code,
            string cName,
            string eName,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变航线有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeShippingLineState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="type">字典类型</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回字典列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DataDictionaryList> GetDataDictionaryList(
            string code,
            string name,
            DataDictionaryType? type,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回字典信息</returns>
        [FunctionInfomation]
        [OperationContract]
        DataDictionaryInfo GetDataDictionaryInfo(Guid id);

        /// <summary>
        /// 保存字典信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="description">描述</param>
        /// <param name="type">字典类型</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveDataDictionaryInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
             string description,
            DataDictionaryType type,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变字典状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeDataDictionaryState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取航班列表
        /// </summary>
        /// <param name="airlineID">航空公司ID</param>
        /// <param name="no">航班号</param>
        /// <param name="polName">始发港ID</param>
        /// <param name="podName">到达港ID</param>
        /// <param name="etdFrom">估计离港日-开始</param>
        /// <param name="etdTo">估计离港日-结束</param>
        /// <param name="etaFrom">估计到港日-开始</param>
        /// <param name="etaTo">估计到港日-结束</param>
        /// <param name="closingDateFrom">截关日-开始</param>
        /// <param name="closingDateTo">截关日-结束</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回航班列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<FlightList> GetFlightList(
            Guid? airlineID,
            string no,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取航班信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回航班信息</returns>
        [FunctionInfomation]
        [OperationContract]
        FlightInfo GetFilghtInfo(Guid id);

        /// <summary>
        /// 保存航班信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="airlineID">航空公司ID</param>
        /// <param name="no">航班号</param>
        /// <param name="polID">始发港ID</param>
        /// <param name="etdDate">估计离港日</param>
        /// <param name="podID">到达港ID</param>
        /// <param name="etaDate">估计到港日</param>
        /// <param name="closingDate">截关日</param>
        /// <param name="docClosingDate">截文件日</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveFlightInfo(
            Guid? id,
            Guid airlineID,
            string no,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变航班有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeFlightState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取被合并的航班列表
        /// </summary>
        /// <param name="mainID">主航班ID</param>
        /// <returns>返回被合并的航班列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<FlightList> GetMergedFlightList(Guid mainID);

        /// <summary>
        /// 合并航班
        /// </summary>
        /// <param name="ids">被合并的航班列表</param>
        /// <param name="preservedID">保留航班ID</param>
        /// <param name="mergeByID">合并人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData MergeFlight(
            Guid[] ids,
            Guid preservedID,
            Guid mergeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 取消合并的航班

        /// </summary>
        /// <param name="ids">取消的航班列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData CancelMergedFlight(
           Guid[] ids,
           Guid cancelByID,
           DateTime?[] updateDates);

        /// <summary>
        /// 获取船名列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回船名列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<VesselList> GetVesselList(
            string code,
            string name,
            string carrierName,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取船名列表
        /// </summary>
        /// <param name="carrierIDs">船东</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns>返回船名列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<VesselList> GetRecentVesselList(DateTime beginDate,
            DateTime EndDate);

        /// <summary>
        /// 获取船名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回船名信息</returns>
        [FunctionInfomation]
        [OperationContract]
        VesselInfo GetVesselInfo(Guid id);

        /// <summary>
        /// 保存船名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="carrierID">船东ID</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <param name="IMO">IMO</param>
        /// <param name="vesselFlag">船籍</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveVesselInfo(
            Guid? id,
            string code,
            string name,
            Guid carrierID,
            Guid saveByID,
            DateTime? updateDate,
            string IMO,
            string UNCode,
            Guid? Registration);

        /// <summary>
        /// 改变船名状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeVesselState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 合并船名
        /// </summary>
        /// <param name="ids">被合并的船名列表</param>
        /// <param name="preservedID">保留航班ID</param>
        /// <param name="mergeByID">合并人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData MergeVessel(
            Guid[] ids,
            Guid preservedID,
            Guid mergeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 获取被合并的船名列表
        /// </summary>
        /// <param name="mainID">主船名ID</param>
        /// <returns>返回被合并的船名列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<VesselList> GetMergedVesselList(Guid mainID);

        /// <summary>
        /// 取消合并的船名

        /// </summary>
        /// <param name="ids">取消的船名列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData CancelMergedVessel(
           Guid[] ids,
           Guid cancelByID,
           DateTime?[] updateDates);

        /// <summary>
        /// 获取船名航次列表
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>返回船名航次列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetRecentVoyageList")]
        List<VoyageList> GetRecentVoyageList(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 获取船名航次列表
        /// </summary>
        /// <param name="vesselName">船名</param>
        /// <param name="no">航次号</param>
        /// <param name="polID">装货港ID</param>
        /// <param name="transhipmentPortID">中转港ID</param>
        /// <param name="podID">卸货港ID</param>
        /// <param name="podName">podName</param>
        /// <param name="polName">polName</param>
        /// <param name="etdFrom">估计离港日-开始</param>
        /// <param name="etdTo">估计离港日-结束</param>
        /// <param name="etaFrom">估计到港日-开始</param>
        /// <param name="etaTo">估计到港日-结束</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回船名航次列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetVoyageListByList")]
        List<VoyageList> GetVoyageList(
            Guid? vesselId,
            string vesselName,
            string no,
            DateTime? createDateFrom,
            DateTime? createDateTo,
            Guid? carrierID,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取船名航次列表
        /// </summary>
        /// <param name="vesselName">船名</param>
        /// <param name="no">航次号</param>
        /// <param name="polID">装货港ID</param>
        /// <param name="transhipmentPortID">中转港ID</param>
        /// <param name="podID">卸货港ID</param>
        /// <param name="podName">podName</param>
        /// <param name="polName">polName</param>
        /// <param name="etdFrom">估计离港日-开始</param>
        /// <param name="etdTo">估计离港日-结束</param>
        /// <param name="etaFrom">估计到港日-开始</param>
        /// <param name="etaTo">估计到港日-结束</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回船名航次列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<VoyageList> GetVoyageList(
            Guid? vesselId,
            Guid? companyId,
            string vesselName,
            string no,
            DateTime? createDateFrom,
            DateTime? createDateTo,
            Guid? carrierID,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取船名航次列表
        /// </summary>
        /// <param name="vesselID">船名ID</param>
        /// <returns>返回船名航次列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetVoyageListByVesselID")]
        List<VoyageList> GetVoyageList(Guid vesselID);

        /// <summary>
        /// 获取航次信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回航次信息</returns>
        [FunctionInfomation]
        [OperationContract]
        VoyageInfo GetVoyageInfo(Guid id);

        /// <summary>
        /// 保存航次信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="vesselID">船名ID</param>
        /// <param name="no">航次号</param>
        /// <param name="polID">装货港ID</param>
        /// <param name="etd">估计离港日</param>
        /// <param name="transhipmentPortID">中转港ID</param>
        /// <param name="podID">卸货港ID</param>
        /// <param name="eta">估计到港日</param>
        /// <param name="closingDate">截关日</param>
        /// <param name="cyClosingDate">截柜日</param>
        /// <param name="docClosingDate">截文件日</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveVoyageInfo(
            Guid? id,
            Guid vesselID,
            string no,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变航次状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeVoyageState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 合并航次
        /// </summary>
        /// <param name="ids">被合并的航次列表</param>
        /// <param name="preservedID">保留航次ID</param>
        /// <param name="mergeByID">合并人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData MergeVoyage(
            Guid[] ids,
            Guid preservedID,
            Guid mergeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 获取被合并的航次列表
        /// </summary>
        /// <param name="mainID">主航次ID</param>
        /// <returns>返回被合并的航次列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<VoyageList> GetMergedVoyageList(Guid mainID);

        /// <summary>
        /// 取消合并的航次
        /// </summary>
        /// <param name="ids">取消的航次列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData CancelMergedVoyage(
           Guid[] ids,
           Guid cancelByID,
           DateTime?[] updateDates);


        /// <summary>
        /// 保存航线与国家关联
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="ShippingLineID"></param>
        /// <param name="CountryPortIDs"></param>
        /// <param name="Types"></param>
        /// <param name="updateDates"></param>
        /// <param name="saveByID"></param>
        /// <param name="ReturnResult"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveShiLineReationCountry
          (
          Guid?[] ids,
          Guid ShippingLineID,
          Guid[] CountryPortIDs,
          DateTime?[] updateDates,
          Guid saveByID,
          bool ReturnResult);

        /// <summary>
        /// 保存航线与口岸关联
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="ShippingLineID"></param>
        /// <param name="PortIDs"></param>
        /// <param name="Types"></param>
        /// <param name="updateDates"></param>
        /// <param name="saveByID"></param>
        /// <param name="ReturnResult"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SavePortReationShippingLine
          (
          Guid?[] ids,
          Guid ShippingLineID,
          Guid[] PortIDs,
          DateTime?[] updateDates,
          Guid saveByID,
          bool ReturnResult);

        /// <summary>
        /// 获取航线下国家口岸列表
        /// </summary>
        /// <param name="shippingLineID"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        CountryPortList GetGetShiLineReationCountryList(
            Guid shippingLineID,
            bool IsEnglish);

        /// <summary>
        /// 删除口岸与航线关联关系
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="removeByID"></param>
        /// <param name="updateDate"></param>
        [FunctionInfomation]
        [OperationContract]
        void RemovePortReationShipping(
          Guid[] Id,  //是memo.id
          Guid removeByID,
          DateTime?[] updateDate);

        /// <summary>
        /// 删除国家与航线的关联关系
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="removeByID"></param>
        /// <param name="updateDate"></param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveCountryReationShipping(
         Guid[] Id,  //是memo.id
         Guid removeByID,
         DateTime?[] updateDate);

        /// <summary>
        /// 获取计价单位
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回计价单位列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DataDictionaryList> GetValuationUnitList(
            string code,
            string name,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取业务费用报销项目列表
        /// </summary>
        /// <returns>CostItemData</returns>
        [FunctionInfomation("获取业务费用报销项目列表")]
        [OperationContract]
        List<CostItemData> GetAllCostItems();

        /// <summary>
        /// 获取MAC地址列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isValid"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        [FunctionInfomation("获取MAC地址列表")]
        [OperationContract]
        List<AuthcodeInfo> GetAuthcodeList(
         string code,
         bool? isValid,
         int maxRecords);

        /// <summary>
        /// 保存MAC地址信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="remark"></param>
        /// <param name="savebyid"></param>
        /// <returns></returns>
        [FunctionInfomation("保存MAC地址信息")]
        [OperationContract]
        SingleResultData SaveAuthcodeInfo(
     Guid? id,
     string code,
     string physicalID,
     string remark,
     Guid savebyid
         );

        /// <summary>
        /// 删除MAC地址信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="removeByID"></param>    
        [FunctionInfomation("删除MAC地址信息")]
        [OperationContract]
        void RemoveAuthcodeInfo(Guid id,
           Guid removeByID);

        /// <summary>
        /// GetVoyageListETDETA
        /// </summary>
        /// <param name="VesselName"></param>
        /// <param name="no"></param>
        /// <param name="CarrierID"></param>
        /// <param name="POL"></param>
        /// <param name="POD"></param>
        /// <returns></returns>
        [FunctionInfomation("GetVoyageListETDETA")]
        [OperationContract]
        VoyageETDETAList GetVoyageListETDETA(
       string VesselName,
           string no,
           Guid? CarrierID,
           Guid? POL,
           Guid? POD);

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="message"></param>
        [FunctionInfomation("SaveLogInfo")]
        [OperationContract]
        void SaveLogInfo(string message);
    }
}

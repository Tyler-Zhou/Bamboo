

//-----------------------------------------------------------------------
// <copyright file="IGeographyService.cs" company="LongWin">
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
    /// 国家，省份，地点信息维护
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IGeographyService
    {
        /// <summary>
        /// 获取国家列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回国家列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CountryList> GetCountryList(
            string code,
            string name,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获得国家列表()区分中英文
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="isValid"></param>
        /// <param name="isEnglish"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CountryList> GetCountryListByFCM(
            string code,
            string name,
            bool? isValid,
            bool isEnglish,
            int maxRecords);

        /// <summary>
        /// 获取国家信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回国家信息</returns>
        [FunctionInfomation]
        [OperationContract]
        CountryInfo GetCountryInfo(Guid id);

        /// <summary>
        /// 保存国家信息
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
        SingleResultData SaveCountryInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变国家有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData ChangeCountryState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="countryId">国家</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回省份列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CountryProvinceList> GetCountryProvinceList(
            string code,
            string name,
            Guid? countryId,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回省份信息</returns>
        [FunctionInfomation]
        [OperationContract]
        CountryProvinceInfo GetProvinceInfo(Guid id);

        /// <summary>
        /// 保存省份信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="countryId">国家</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveProvinceInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
            Guid countryId,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变省份有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData ChangeProvinceState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取地点列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="countryId">国家</param>
        /// <param name="provinceId">省份</param>
        /// <param name="isOcean">海运</param>
        /// <param name="isAir">空运</param>
        /// <param name="isOther">其它</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回地点列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<LocationList> GetLocationList(
            string codeOrName,
            Guid? countryId,
            Guid? provinceId,
            bool? isOcean,
            bool? isAir,
            bool? isOther,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取地点列表
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>返回地点列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<LocationList> GetRecentLocationList(DateTime beginDate,DateTime endDate);

        /// <summary>
        /// 获取地点信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回地点列表</returns>
        [FunctionInfomation]
        [OperationContract]
        LocationInfo GetLocationInfo(Guid id);
        /// <summary>
        /// 街道和邮编信息(AMS)
        /// </summary>
        /// <param name="cityId">城市地址ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PostalCodeInfo> GetPostalCodeList(Guid cityId);
        /// <summary>
        /// 通过城市名称获取街道和邮编信息（AMS)
        /// </summary>
        /// <param name="cityName">城市、州名称</param>
        /// <returns></returns>
        [OperationContract(Name="GetPostalCodeListByCityName")]
        List<PostalCodeInfo> GetPostalCodeList(string cityName);

        /// <summary>
        /// 保存邮编信息(AMS)
        /// </summary>
        /// <param name="cityId">城市ID（LocationID）</param>
        /// <param name="zip">Zip</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(IsOneWay=true)]
        void SavePostalCodeInfo(Guid cityId, string zip);
        /// <summary>
        /// 保存邮编信息(AMS)
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <param name="cityName">城市名称</param>
        /// <param name="zipCode">邮编</param>
        /// <returns></returns>
        [OperationContract(Name="SavePostalCodeInfo2",IsOneWay=true)]
        void SavePostalCodeInfo(string countryName, string cityName, string zipCode);

        /// <summary>
        /// 保存地点信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="countryId">国家</param>
        /// <param name="provinceId">省份</param>
        /// <param name="isOcean">海运</param>
        /// <param name="isAir">空运</param>
        /// <param name="isOther">其它</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveLocationInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
            Guid countryId,
            Guid? provinceId,
            bool isOcean,
            bool isAir,
            bool isOther,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变地点有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeLocationState(
            Guid id,
            bool? isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取合并的地点列表

        /// </summary>
        /// <param name="mainId">主ID</param>
        /// <returns>返回合并的地点列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<LocationList> GetMergedLocationList(Guid mainId);

        /// <summary>
        /// 合并地点
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="preservedId">保留ID</param>
        /// <param name="mergeById">合并人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData MergeLocation(
            Guid[] ids,
            Guid preservedId,
            Guid mergeById,
            DateTime?[] updateDates);

        /// <summary>
        /// 取消合并
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="cancelById">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData CancelMergeLocation(
            Guid[] ids,
            Guid cancelById,
            DateTime?[] updateDates);


        /// <summary>
        /// 根据名称获取港口信息
        /// </summary>
        /// <param name="names">names</param>
        /// <returns>港口信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<PortNames> GetPortForName(string[] names);

        /// <summary>
        /// 根据国家ID得到港口信息
        /// </summary>
        /// <param name="countryIDs"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PortNames> GetPortForCountryID(Guid[] countryIDs);

        /// <summary>
        /// 获取地点列表
        /// </summary>
        /// <param name="codeOrName"></param>
        /// <param name="countryId"></param>
        /// <param name="countryName"></param>
        /// <param name="provinceId"></param>
        /// <param name="isOcean"></param>
        /// <param name="isAir"></param>
        /// <param name="isOther"></param>
        /// <param name="isValid"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetLocationListWithCountryName")]
        List<LocationList> GetLocationList(string codeOrName, Guid? countryId, string countryName, Guid? provinceId, bool? isOcean, bool? isAir, bool? isOther, bool? isValid, int maxRecords);


    }
}

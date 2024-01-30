



//-----------------------------------------------------------------------
// <copyright file="IConfigureService.cs" company="LongWin">
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
    using ICP.Common.ServiceInterface.CompositeObjects;

    /// <summary>
    /// 配置管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IConfigureService
    {
        /// <summary>
        /// 得到前5项用户常用菜单名称
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [OperationContract]
        List<GetTop5MenuOfCommandNameViaUserID> GetTop5Menu(System.Guid UserID);
        /// <summary>
        /// 获取费用代码列表
        /// </summary>
        /// <param name="groupID">组ID</param>
        /// <returns>返回费用代码列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ChargingCodeList> GetChargingCodeListByGroupID(Guid groupID);

        /// <summary>
        /// 获取费用代码列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="groupID">组ID</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回费用代码列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ChargingCodeList> GetChargingCodeListBySearch(
            string code,
            string name,
            Guid? groupID,
            bool? isCommission,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取公司的费用代码列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司的费用代码列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ChargingCodeList> GetCompanyChargingCodeList(
            Guid companyID,
            bool? isValid);

        /// <summary>
        /// 获取费用代码信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回费用代码信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ChargingCodeInfo GetChargingCodeInfo(Guid id);

        /// <summary>
        /// 保存费用代码信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="groupID">组ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleHierarchyResultData SaveChargingCodeInfo(
            Guid? id,
            Guid groupID,
            string code,
            string cName,
            string eName,
            bool isCommission,
            Guid saveByID,
            DateTime? updateDate);

        #region ChargingGroup
        /// <summary>
        /// 改变费用代码状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeChargingCodeState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取费用代码组列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="category">分类(0:管理成本,1:运输成本,2:业务费用)</param>        
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回费用代码组列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ChargingGroupList> GetChargingGroupList(
            string code,
            string name,
            ChargeCodeCategory? category,
            int maxRecords);

        /// <summary>
        /// 获取费用代码组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回费用代码组信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ChargingGroupInfo GetChargingGroupInfo(Guid id);

        /// <summary>
        /// 保存费用代码组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回费用代码组信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SaveChargingGroupInfo(
            Guid? id,
            Guid? parentID,
            string code,
            string cName,
            string eName,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 删除费用代码组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveChargingGroupInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 设置父费用代码组
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="setByID">设置人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SetChargingGroupParent(
            Guid id,
            Guid? parentID,
            Guid setByID,
            DateTime? updateDate); 
        #endregion

        /// <summary>
        /// 获取币种列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="countryID">国家ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回币种列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CurrencyList> GetCurrencyList(
            string code,
            string name,
            Guid? countryID,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回币种信息</returns>
        [FunctionInfomation]
        [OperationContract]
        CurrencyInfo GetCurrencyInfo(Guid id);

        /// <summary>
        /// 保存币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="countryID">国家ID</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveCurrencyInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
            Guid countryID,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变币种状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeCurrencyState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变解决方案状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeSolutionState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取解决方案列表
        /// </summary>
        /// <param name="name">中文名</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回解决方案列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionList> GetSolutionList(
            string name,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取解决方案信息
        /// </summary>
        /// <param name="id">解决方案ID</param>
        /// <returns>返回解决方案信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionInfo GetSolutionInfo(Guid id);

        /// <summary>
        /// 保存解决方案信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="invoiceDateType">账单日期类型（0创建时间、1业务时间）</param>
        /// <param name="isAccountingShare">是否财务共享</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回解决方案信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveSolutionInfo(
            Guid? id,
            string cName,
            string eName,
            InvoiceDateType invoiceDateType,
            bool isAccountingShare,
            string remark,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 拷贝解决方案信息
        /// </summary>
        /// <param name="id">解决方案ID</param>
        /// <returns>返回解决方案信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionInfo CopySolutionInfo(Guid id);

        /// <summary>
        /// 获取代码规则列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回代码规则列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionCodeRuleList> GetSolutionCodeRuleList(
            Guid solutionID,
            bool? isValid);

        /// <summary>
        /// 获取代码规则信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回代码规则信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionCodeRuleInfo GetSolutionCodeRuleInfo(Guid id);

        /// <summary>
        /// 保存代码规则
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID</param>
        /// <param name="configureKeyIDs">配置字典ID列表</param>
        /// <param name="descriptions">描述</param>
        /// <param name="isIncludeCompanyCodes">包括公司代码</param>
        /// <param name="codePrefixs">代码前缀</param>
        /// <param name="codeYears">代码规则年格式</param>
        /// <param name="includeCodeMonths">在生成代码中包括月</param>
        /// <param name="codeSNLengths">序列号长度</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveSolutionCodeRuleInfo(
            Guid solutionID,
            Guid?[] ids,
            Guid[] configureKeyIDs,
            string[] descriptions,
            bool[] isIncludeCompanyCodes,
            string[] codePrefixs,
            CodeYearFormart[] codeYears,
            bool[] includeCodeMonths,
            short[] codeSNLengths,
            Guid saveByID,
            DateTime?[] updateDates);


        /// <summary>
        /// 改变代码规则状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeSolutionCodeRuleState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取公司的会计科目列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司的会计科目列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionGLCodeList> GetCompanyGLCodeList(
            Guid companyID,
            bool? isValid);

        /// <summary>
        /// 获取会计科目组列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回会计科目组列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionGLGroupList> GetSolutionGLGroupList(
            string code,
            string name,
            int maxRecords);

        /// <summary>
        /// 获取会计科目组信息
        /// </summary>
        /// <param name="id">父ID</param>
        /// <returns>返回会计科目组信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionGLGroupInfo GetSolutionGLGroupInfo(Guid id);

        /// <summary>
        /// 保存会计科目组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleHierarchyResultData SaveSolutionGLGroupInfo(
            Guid? id,
            Guid? parentID,
            string code,
            string cName,
            string eName,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 删除快捷科目组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveSolutionGLGroupInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 设置父会计科目
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="setByID">设置人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetSolutionGLGroupParent(
            Guid id,
            Guid? parentID,
            Guid setByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取解决方案的汇率列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的汇率列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionExchangeRateList> GetSolutionExchangeRateList(
            Guid solutionID,
            bool? isValid);


        /// <summary>
        /// 获取公司的汇率列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司的汇率列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionExchangeRateList> GetCompanyExchangeRateList(
            Guid companyID,
            bool? isValid);

        /// <summary>
        /// 获取解决方案的汇率信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的汇率信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionExchangeRateInfo GetSolutionExchangeRateInfo(Guid id);

        /// <summary>
        /// 保存解决方案的汇率信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="exchangeType"></param>
        /// <param name="ids">ID</param>
        /// <param name="sourceCurrencyIDs">源币种ID</param>
        /// <param name="targetCurrencyIDs">目标币种ID</param>
        /// <param name="fromDates">开始时间</param>
        /// <param name="toDates">结束时间</param>
        /// <param name="rates">汇率</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveSolutionExchangeRateInfo(
            Guid solutionID,
            ExchangeType exchangeType,
            Guid?[] ids,
            Guid[] sourceCurrencyIDs,
            Guid[] targetCurrencyIDs,
            DateTime[] fromDates,
            DateTime[] toDates,
            decimal[] rates,
            Guid saveByID,
            DateTime?[] updateDates);


        /// <summary>
        /// 更改解决方案的汇率有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeSolutionExchangeRateState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取解决方案的会计科目列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的会计科目列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionGLCodeList> GetSolutionGLCodeList(
            Guid solutionID,
            bool? isValid);

        /// <summary>
        /// 获取解决方案的会计科目信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的会计科目信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionGLCodeInfo GetSolutionGLCodeInfo(Guid id);


        /// <summary>
        /// 保存解决方案的会计科目信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID</param>
        /// <param name="glGroupID">会计科目组ID</param>
        /// <param name="codes">代码</param>
        /// <param name="cNames">中文名</param>
        /// <param name="eNames">英文名</param>
        /// <param name="descriptions">描述</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回解决方案的会计科目信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveSolutionGLCodeInfo(
            Guid solutionID,
            Guid?[] ids,
            Guid glGroupID,
            string[] codes,
            string[] cNames,
            string[] eNames,
            string[] descriptions,
            Guid saveByID,
            DateTime?[] updateDates);



        /// <summary>
        /// 改变解决方案的会计科目状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeSolutionGLCodeState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取解决方案的币种列表
        /// </summary>
        /// <param name="companyID">操作口岸ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的币种列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionCurrencyList> GetCompanyCurrencyList( Guid companyID, bool? isValid);

        /// <summary>
        /// 获取解决方案的币种列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的币种列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionCurrencyList> GetSolutionCurrencyList(
            Guid solutionID,
            bool? isValid);

        /// <summary>
        /// 获取解决方案的币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的币种信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionCurrencyInfo GetSolutionCurrencyInfo(Guid id);

        /// <summary>
        /// 保存解决方案的币种信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="currencyIDs">币种ID列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回解决方案的币种信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveSolutionCurrencyInfo(
            Guid solutionID,
            Guid[] ids,
            Guid[] currencyIDs,
            Guid saveByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除解决方案下的币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveSolutionCurrencyInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取解决方案的费用代码列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的费用代码列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionChargingCodeList> GetSolutionChargingCodeListBySolutionID(
            Guid solutionID,
            bool? isValid);

        /// <summary>
        /// 获取解决方案的费用代码列表
        /// </summary>
        ///<param name="isAgent">是否代于理</param>
        ///<param name="Name">名称</param>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的费用代码列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionChargingCodeList> GetSolutionChargingCodeListByList(
            Guid solutionID,
            string Name,
            bool? isAgent,
            bool? isCommission,
            bool? isValid);

        /// <summary>
        /// 获取解决方案的费用代码信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的费用代码信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionChargingCodeInfo GetSolutionChargeCodeInfo(Guid id);

        /// <summary>
        /// 保存解决方案的费用代码
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="chargeCodeIDs">费用代码ID列表</param>
        /// <param name="isAgents">是否代收代付列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveSolutionChargingCodeInfo(
            Guid solutionID,
            Guid?[] ids,
            Guid[] chargeCodeIDs,
            bool[] isAgents,
            Guid saveByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除解决方案下的费用代码
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本列表</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveSolutionChargingCodeInfo(
             Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);


        /// <summary>
        /// 获取公司下的代理列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司下的代理列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerList> GetCompanyAgentList(
            Guid companyID,
            bool? isValid);

        /// <summary>
        /// 获取解决方案下的代理列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案下的代理列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionAgentList> GetSolutionAgentList(
            Guid solutionID,
            bool? isValid);


        /// <summary>
        /// 获取解决方案下的代理的详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案下的代理的详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionAgentInfo GetSolutionAgentInfo(Guid id);

        /// <summary>
        /// 保存解决方下的代理信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="agentIDs">代理人ID列表</param>
        /// <param name="remarks">备注列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本列表</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveSolutionAgentInfo(
            Guid solutionID,
            Guid[] ids,
            Guid[] agentIDs,
            string[] remarks,
            Guid saveByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 改变解决方案的代理状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeSolutionAgentState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取解决方案的会计配置列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的会计配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionGLConfigList> GetSolutionGLConfigList(
            Guid solutionID,
            bool? isValid);

        /// <summary>
        /// 获取会计科目配置类型
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<GLConfigType> GetGLConfigTypes();

        /// <summary>
        /// 获取解决方案的会计配置信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的会计配置信息</returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionGLConfigInfo GetSolutionGLConfigInfo(Guid id);

        /// <summary>
        /// 保存解决方案下的会计配置信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID</param>
        /// <param name="types">类型(0费用项目、1主营业收入、2预收预付、3代收代付)</param>
        /// <param name="chargeCodeIDs">费用代码ID</param>
        /// <param name="currencyIDs">币种ID</param>
        /// <param name="drGLCodeIDs">代收会计科目ID</param>
        /// <param name="crGLCodeIDs">代付会计科目ID</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveSolutionGLConfigInfo(
            Guid solutionID,
            Guid?[] ids,
            int[] types,
            Guid?[] chargeCodeIDs,
            Guid[] currencyIDs,
            Guid[] drGLCodeIDs,
            Guid[] crGLCodeIDs,
            Guid saveByID,
            DateTime?[] updateDates);


        /// <summary>
        /// 改变解决方案下会计配置有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeSolutionGLConfigState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取公司配置列表
        /// </summary>
        /// <param name="isVaid">是否有效</param>
        /// <returns>返回公司配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureList> GetConfigureListByVaid(bool? isVaid);

        /// <summary>
        /// 获取公司配置列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="solutionID">解决方案</param>
        /// <param name="isVaid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回公司配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureList> GetConfigureListByList(
            Guid? companyID,
            Guid? solutionID,
            bool? isVaid,
            int maxRecords);

        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns>返回公司配置信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ConfigureInfo GetCompanyConfigureInfo(Guid companyID);

        /// <summary>
        /// 根据公司对应客户获取公司配置信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns>返回公司配置信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ConfigureInfo GetCompanyConfigureInfoByCustomer(Guid customerID);


        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="companyID">CompanyID</param>
        /// <returns>返回公司配置信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ConfigureInfo GetConfigureInfoByCompanyID(Guid companyID);

        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回公司配置信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ConfigureInfo GetConfigureInfo(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "CompanyConfigInfo")]
        ConfigureInfo GetCompanyConfigureInfo(Guid companyID, bool isEnglish);

        /// <summary>
        /// 保存公司配置信息
        /// </summary>
        /// <param name="configureSaveRequest">保存对象</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveConfigureInfo(ConfigureSaveRequest configureSaveRequest);

        /// <summary>
        /// 更改公司配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeConfigureState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取公司配置的字典列表信息
        /// </summary>
        /// <param name="configureID">配置ID</param>
        /// <returns>返回公司配置的字典列表信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureKeyValueList> GetConfigureKeyValueList(Guid configureID);

        /// <summary>
        /// 获取公司配置字典信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回公司配置字典信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ConfigureKeyValueInfo GetConfigureKeyValueInfo(Guid id);

        /// <summary>
        /// 保存公司配置字典信息
        /// </summary>
        /// <param name="configureID">配置ID</param>
        /// <param name="id">字典ID</param>
        /// <param name="configureKeyID">关键字ID</param>
        /// <param name="value">值</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveConfigureKeyValueInfo(
            Guid configureID,
            Guid? id,
            Guid configureKeyID,
            string value,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 保存税控版本号
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="taxControlVersion">值</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveTaxControlVersion(
            Guid companyID,
            string taxControlVersion,
            Guid saveByID);

        #region Configure Key
        /// <summary>
        /// 删除公司配置字典信息
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveConfigureKeyValueInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取系统配置的关键字列表
        /// </summary>
        /// <returns>返回系统配置的关键字列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureKeyList> GetConfigureKeyList();


        /// <summary>
        /// 获取系统配置的关键字列表
        /// </summary>
        /// <returns>返回系统配置的关键字列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureKeyList> GetConfigureKeyListForType(ConfigureType type);


        [FunctionInfomation]
        [OperationContract]
        List<ConfigureKeyList> GetConfigureKeyListForBLTitle();


        /// <summary>
        /// 获取公司配置字典值
        /// </summary>
        /// <param name="companyId">公司</param>
        /// <param name="key">关键字</param>
        /// <returns>返回公司配置字典值</returns>
        [FunctionInfomation]
        [OperationContract]
        string GetConfigureKeyValue(Guid companyId, string key); 
        #endregion

        #region 生成单号
        /// <summary>
        /// 生成单号
        /// </summary>
        /// <param name="companyID">公司</param>
        /// <param name="key">关键字</param>
        /// <param name="generateDate">日期</param>
        /// <returns>返回新生成的单号</returns>
        [FunctionInfomation]
        [OperationContract]
        string GenerateNO(Guid companyID, string key, DateTime generateDate); 
        #endregion

        #region EDIConfigure
        /// <summary>
        /// 获取默认单位列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<DataDictionaryList> GetCompanyDefaultUnitList(Guid companyID);

        /// <summary>
        /// 获取EID配置列表
        /// </summary>
        /// <param name="serviceConfigureKeyID"></param>
        /// <param name="carrierID"></param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords"></param>
        /// <returns>返回EID配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<EDIConfigureList> GetEDIConfigureList(
            Guid? serviceConfigureKeyID,
            Guid? carrierID,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 保存EDI配置信息
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <param name="Components"></param>
        /// <param name="FTPs"></param>
        /// <param name="FileFormats"></param>
        /// <param name="DataFormats"></param>
        /// <param name="RegularFiles"></param>
        /// <param name="StoredProcedures"></param>
        /// <param name="configureKeyIDs">配置KEY</param>
        /// <param name="carrierIDs">船公司ID</param>
        /// <param name="modes">上传模式</param>
        /// <param name="itemType">订舱/补料</param>
        /// <param name="serverAddresses">服务器地址</param>
        /// <param name="userNames">帐号</param>
        /// <param name="passwords">密码</param>
        /// <param name="receiveAddresses">反馈地址</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="ReceiverType"></param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveEDIConfigureInfo(
            Guid?[] ids,
            //string[] codes,
            string[] Components,
            string[] FTPs,
            string[] FileFormats,
            string[] DataFormats,
            string[] RegularFiles,
            string[] StoredProcedures,
            Guid[] configureKeyIDs,
            Guid?[] carrierIDs,
            EDIUploadMode[] modes,
            EDIMode[] itemType,
            string[] serverAddresses,
            string[] userNames,
            string[] passwords,
            string[] receiveAddresses,
            Guid saveByID,
            byte ReceiverType,
            DateTime?[] updateDates);

        /// <summary>
        /// 改变EDI配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeEDIConfigureState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取公司配置下的EDI配置列表
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司配置下的EDI配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CompanyEDIConfigureList> GetCompanyEDIConfigureList(
            Guid configureID,
            bool? isValid);

        /// <summary>
        /// 保存公司EDI配置信息
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="ediConfigureIDs">EDI配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveCompanyEDIConfigureInfo(
            Guid configureID,
            Guid[] ediConfigureIDs,
            Guid saveByID); 
        #endregion

        #region EDICompanyConfigure
        /// <summary>
        /// 保存EDI公司配置信息
        /// </summary>
        /// <param name="ediConfigureID">EDI配置ID</param>
        /// <param name="configureIDs"></param>
        /// <param name="toAddress"></param>
        /// <param name="csclWebURL"></param>
        /// <param name="csclLoginName"></param>
        /// <param name="csclPassword"></param>
        /// <param name="companyAddress"></param>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveEDICompanyConfigureInfo(
             Guid ediConfigureID,
             Guid[] configureIDs,
             string[] toAddress,
            string[] csclWebURL,
            string[] csclLoginName,
            string[] csclPassword,
            string[] companyAddress,
             Guid saveByID);

        /// <summary>
        /// 删除公司EDI配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveCompanyEDIConfigure(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取EDI配置下的公司配置数据列表
        /// </summary>
        /// <param name="ediConfigureID">EDI配置</param>
        /// <returns>返回EDI配置下的公司配置数据列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureListForEDI> GetEDICompanyConfigureListByConfigure(Guid ediConfigureID);

        /// <summary>
        /// 获取公司EDI配置列表
        /// </summary>
        /// <param name="configureID">公司</param>
        /// <returns>返回公司EDI配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CompanyEDIConfigureList> GetCompanyEDIConfigureListByCompany(Guid configureID); 
        #endregion

        #region ReportConfigure
        /// <summary>
        /// 获取报表类型列表
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ReportType> GetReportTypes();

        /// <summary>
        /// 获取报表配置列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cDescription"></param>
        /// <param name="eDescription"></param>
        /// <param name="reportTypeID"></param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords"></param>
        /// <returns>返回报表配置列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetReportConfigureListByList")]
        List<ReportConfigureList> GetReportConfigureList(string code,
        string cDescription,
        string eDescription,
        int? reportTypeID,
        bool? isValid,
        int maxRecords);

        /// <summary>
        /// 保存报表配置信息
        /// </summary>
        /// <param name="id">ID列表</param>
        /// <param name="code">代码</param>
        /// <param name="cDescription"></param>
        /// <param name="eDescription"></param>
        /// <param name="reportParameters"></param>
        /// <param name="reportTypeID"></param>
        /// <param name="cName">中文名称</param>
        /// <param name="eName">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="fileName">报表文件</param>
        /// <param name="parameter">参数</param>
        /// <param name="type">报表类型</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveReportConfigureInfo(
            Guid? id,
            string code,
            string cDescription,
            string eDescription,
            string reportParameters,
            int? reportTypeID,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变报表配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeReportConfigureState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取单个报表配置，根据公司ID和报表配置的Code(string型，完全匹配)
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="reportCode"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetReportConfigureListByCompany")]
        CompanyReportConfigureList GetReportConfigureList(Guid companyID, string reportCode);

        /// <summary>
        /// 获取公司配置下的报表配置列表
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司配置下的报表配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CompanyReportConfigureList> GetCompanyReportConfigureList(
            Guid configureID,
            bool? isValid);

        /// <summary>
        /// 保存公司报表配置信息
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="reportParameters">报表参数</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveCompanyReportConfigureInfo(
            Guid configureID,
            string reportParameters,
            Guid saveByID);

        /// <summary>
        /// 保存报表公司配置信息
        /// </summary>
        /// <param name="reportID">报表配置ID</param>
        /// <param name="reportParameters">公司配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveReportCompanyConfigureInfo(
             Guid reportID,
             string reportParameters,
             Guid saveByID);

        /// <summary>
        /// 删除公司报表配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveReportConfigure(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取报表所挂公司列表
        /// </summary>
        /// <param name="reportID">报表ID</param>
        /// <returns>返回报表所挂公司列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ReportCompanyConfigureList> GetReportCompanyConfigureList(Guid reportID);
        #endregion

        

        /// <summary>
        /// 获取公司配置下对应的客户列表
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureCustomerList> GetConfigureCustomerList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportID"></param>
        /// <param name="reportParameterID"></param>
        /// <param name="companyID">公司ID</param>
        /// <param name="solutionID">解决方案</param>
        /// <returns>返回公司配置列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureList> GetCompanyForReportParameterIsUsed(Guid reportID, Guid reportParameterID);

        /// <summary>
        /// 获得EDI配置列表
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<EDIDictCodeList> GetEDIDictCodeList(EDIDicType? ediDicType);


        /// <summary>
        /// 保存会计科目配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名</param>
        /// <param name="ename">英文名</param>
        /// <param name="type">类型</param>
        /// <param name="ledgerStyle">账页格式</param>
        /// <param name="gLCodeProperty">余额方向</param>
        /// <param name="isForeignCheck">是否外币核算</param>
        /// <param name="foreignCurrencyID">币种</param>
        /// <param name="isNumberCheck">是否数量核算</param>
        /// <param name="unitName">计量单位</param>
        /// <param name="isDepartmentCheck">部门核算</param>
        /// <param name="isPersonalCheck">个人核算</param>
        /// <param name="isCustomerCheck">客户往来</param>
        /// <param name="isJournal">日记帐</param>
        /// <param name="isBankAccount">银行帐</param>
        /// <param name="isFee">是否为费用</param>
        /// <param name="years">年份</param>
        /// <param name="parentID">父级ID</param>
        /// <param name="description">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveSolutionGLCodeInfoNew(
            Guid solutionID,
            Guid id,
            string code,
            string cname,
            string ename,
            GLCodeType type,
            GLCodeLedgerStyle ledgerStyle,
            GLCodeProperty gLCodeProperty,
            bool isForeignCheck,
            Guid? foreignCurrencyID,
            bool isNumberCheck,
            string unitName,
            bool isDepartmentCheck,
            bool isPersonalCheck,
            bool isCustomerCheck,
            bool isJournal,
            bool isBankAccount,
            bool? isFee,
            Guid? parentID,
            string description,
            Guid saveByID,
            DateTime? updateDate,
            Guid? companyID);

        /// <summary>
        /// 获得会计科目详细设置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SolutionGLCodeList GetSolutionGLCodeInfoNew(Guid id, bool isEnglish);

        /// <summary>
        /// 获得会计科目列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="companyIds"></param>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="isValid">有效性</param>
        /// <param name="IsDepartmentCheck"></param>
        /// <param name="IsPersonalCheck"></param>
        /// <param name="IsCustomerCheck"></param>
        /// <param name="IsJournal"></param>
        /// <param name="IsBankAccount"></param>
        /// <param name="IsFee"></param>
        /// <param name="isDepartmentCheck">部门核算</param>
        /// <param name="isPersonalCheck">个人往来</param>
        /// <param name="isCustomerCheck">客户往来</param>
        /// <param name="isJournal">日记账</param>
        /// <param name="isBankAccount">银行帐</param>
        /// <param name="isFee">流程报销费用</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetSolutionGLCodeListNew2")]
        List<SolutionGLCodeList> GetSolutionGLCodeListNew(
                                    Guid solutionID,
                                    Guid[] companyIds,
                                    string code,
                                    string name,
                                    GLCodeType type,
                                    bool? isValid,
                                    bool? IsDepartmentCheck,
                                    bool? IsPersonalCheck,
                                    bool? IsCustomerCheck,
                                    bool? IsJournal,
                                    bool? IsBankAccount,
                                    bool? IsFee,
                                    bool isEnglish);

        /// <summary>
        /// 获得会计科目列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="companyIds"></param>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="isValid">有效性</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetSolutionGLCodeListNew")]
        List<SolutionGLCodeList> GetSolutionGLCodeListNew(
                                    Guid solutionID,
                                    Guid[] companyIds,
                                    string code,
                                    string name,
                                    GLCodeType type,
                                    bool? isValid,
                                    bool isEnglish);
        /// <summary>
        /// 根据ID集合获得科目列表
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="companyIds"></param>
        /// <param name="solutionID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionGLCodeList> GetSolutionGLCodeListByIds(Guid[] ids, Guid[] companyIds, Guid solutionID);
        /// <summary>
        /// 是否为叶子节点的会计科目
        /// </summary>
        /// <param name="id">会计科目ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns>bool</returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsLeafGLCode(Guid id, bool isEnglish);

        /// <summary>
        /// 通过公司ID获得所有isfee=1的会计科目
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionGLCodeList> GetFeeGLCodeList(Guid companyID, bool isEnglish);

        /// <summary>
        /// 通过科目ID获得所属公司列表
        /// </summary>
        /// <param name="GLID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<GL2COMPANY> GetGLOrgbyId(Guid GLID);

        /// <summary>
        /// 删除科目公司联系
        /// </summary>
        /// <param name="IDs"></param>
        /// <param name="createBy"></param>
        /// <param name="dataList"></param>
        [FunctionInfomation]
        [OperationContract]
        void DelGL2Company(Guid[] IDs, Guid createBy);

        /// <summary>
        /// 保存科目公司联系
        /// </summary>
        /// <param name="IDs"></param>
        /// <param name="GLID"></param>
        /// <param name="Coms"></param>
        /// <param name="Codes"></param>
        /// <param name="dataList"></param>
        /// <param name="createBy"></param>
        [FunctionInfomation]
        [OperationContract]
        void SaveGL2Company(Guid[] IDs, Guid GLID, Guid[] Coms, string[] Codes, Guid createBy);


        /// <summary>
        /// 获得指定时间的汇率
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="sourceCurrencyID"></param>
        /// <param name="targetCurrencyID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        decimal GetCompanyStandardCurrencyRate(Guid companyID, Guid sourceCurrencyID, DateTime date);
        /// <summary>
        /// 获取用户上次更新密码时间或设置更新时间
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ReadWrite"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<GetUserPasswordUpdate> UserPasswordUpdate(Guid UserID, Int16 ReadWrite);
    }
}

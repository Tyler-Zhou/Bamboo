

namespace ICP.FRM.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FRM.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;

    /// <summary>
    /// 海运运价管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IOceanPriceService
    {
        #region 合约管理

        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <param name="contractNo">合约号</param>
        /// <param name="contractName">合约名称</param>
        /// <param name="carrier">船东</param>
        /// <param name="shippinglineID">航线</param>
        /// <param name="pol">装货港</param>
        /// <param name="pod">卸货港</param>
        ///<param name="delivery">卸货港</param>
        ///<param name="via">中转港</param>
        /// <param name="rateType">运价类型(1:Contract,2:Market)</param>
        /// <param name="state">状态(1,Draft2.Published,3.Invalidated,4.Expired</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="publisher">发布人</param>
        /// <param name="userID">当前用户ID，用来判断是否有权限</param>
        /// <returns>返回合约列表</returns>
       [FunctionInfomation] 
        [OperationContract]
        List<OceanList> GetOceanList(
            string contractNo,
            string contractName,
            string carrier,
            Guid? shippinglineID,
            string pol,
            string via,
            string pod,
            string delivery,
            RateType? rateType,
            OceanState? state,
            DateTime? fromDate,
            DateTime? toDate,
            string publisher,
            Guid userID);

        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <param name="ids">ids</param>
        /// <param name="userID">当前用户ID，用来判断是否有权限</param>
        /// <returns>返回合约列表</returns>
       [FunctionInfomation]
        [OperationContract(Name="GetOceanList2")]
        List<OceanList> GetOceanListByIds(Guid[] ids, Guid userID);

        /// <summary>
        /// 获取合约详细信息
        /// </summary>
        /// <param name="id">合约ID</param>
        /// <returns>返回合约详细信息</returns>
       [FunctionInfomation] 
        [OperationContract]
        OceanInfo GetOceanInfo(Guid id);

        /// <summary>
        /// 拷贝合约信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="copyByID">拷贝人ID</param>
        /// <returns>返回合约详细信息</returns>
       [FunctionInfomation] 
        [OperationContract]
       OceanInfo CopyOceanInfo(
            Guid oceanID,
            Guid copyByID);

        /// <summary>
        /// 保存合约信息 
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="contractNo">合约号</param>
        /// <param name="contractName">合约名</param>
        /// <param name="contractType">contractType</param>
        /// <param name="carrierID">船东</param>
        /// <param name="shippingLineID">航线</param>
        /// <param name="paymentTermID">付款方式</param>
        /// <param name="currencyID">币种</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="rateType">运价类型</param>
        /// <param name="shipperIDs">发货人</param>
        /// <param name="consigneeIDs">收货人</param>
        /// <param name="notifyIDs">通知人</param>
        /// <param name="unitIDs">运价单位</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResult</returns>
       [FunctionInfomation]  
        [OperationContract]
        SingleResultData SaveOceanInfo(
                Guid? id,
                string contractNo,
                string contractName,
                ContractType contractType,
                Guid? carrierID,
                Guid? shippingLineID,
                Guid? paymentTermID,
                Guid currencyID,
                DateTime fromDate,
                DateTime toDate,
                RateType rateType,
                Guid?[] shipperIDs,
                Guid?[] consigneeIDs,
                Guid?[] notifyIDs,
                Guid[] unitIDs,
                string remark,
                Guid saveByID,
                DateTime? updateDate);

        /// <summary>
        /// 删除合约
        /// </summary>
        /// <param name="id">合约ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
       [FunctionInfomation] 
        [OperationContract]
        void RemoveOceanInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 更改合约状态
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="state">状态</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
       [FunctionInfomation] 
        [OperationContract]
        SingleResultData ChangeOceanState(
            Guid oceanID,
            OceanState state,
            Guid changeByID,
            DateTime? updateDate);

       /// <summary>
       /// 将一个合约下的所有的运价导出到Excel
       /// </summary>
       /// <param name="oceanID"></param>
       /// <param name="isEnglish"></param>
       /// <returns></returns>
       [FunctionInfomation]
       [OperationContract(Name="OceanPrice")]
       OceanRateToExcel ExportOceanRateToExcel(Guid oceanID, bool isEnglish);

        #endregion

        #region BasePorts

        /// <summary>
        /// 获取OceanBasePorts列表
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <returns>返回合约的运价列表</returns>
       [FunctionInfomation]  
        [OperationContract]
       BasePortRateList GetOceanBasePorts(Guid oceanID);


        /// <summary>
        /// 获取OceanBasePorts列表(压缩后的)
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <returns>返回合约的运价列表</returns>
      // [FunctionInfomation]  
       // [OperationContract]
        //Byte[] GetOceanBasePortsZip(Guid oceanID);


        /// <summary>
        /// 保存运价信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="itemCollect">运价XML内容</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
       [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOceanBasePorts(
            Guid oceanID,
            BasePortCollect itemCollect,
            bool isCopyBasePort,
            Guid saveByID);

        /// <summary>
        /// 保存运价信息(压缩后的)
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="oceanID"></param>
        /// <param name="itemCollects"></param>
        /// <param name="saveByID"></param>
        /// <returns></returns>
       [FunctionInfomation] 
        [OperationContract]
        ManyResult SaveOceanBasePortsZip(
            Guid oceanID,
            byte[] itemCollects,
            Guid saveByID);


        /// <summary>
        /// 删除 BasePorts
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">更改人</param>
        /// <param name="updateDates">数据版本</param>
       [FunctionInfomation] 
        [OperationContract]
        ManyResultData RemoveBasePorts(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);

       /// <summary>
       /// 将一个BasePort关联的所有AdditionalFee关联到另外一个BasePort中
       /// </summary>
       /// <param name="Ids"></param>
       /// <param name="copyIds"></param>
       /// <param name="isEnglish"></param>
       [FunctionInfomation]
       [OperationContract]
       void SaveOceanItem2AdditionalFeeByBasePort(Guid[] ids, Guid[] copyIds,Guid saveById , bool isEnglish);

        #endregion

        #region Arbitrary

        /// <summary>
        /// 获取OceanArbitrary列表
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <returns>返回合约的运价列表</returns>
       [FunctionInfomation]  
        [OperationContract]
        List<ArbitraryList> GetOceanArbitrarys(Guid oceanID);


        /// <summary>
        /// 保存运价信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="itemCollect">运价XML内容</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
       [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOceanArbitrarys(
            Guid oceanID,
            ArbitraryCollect itemCollect,
            Guid saveByID);

        ///// <summary>
        ///// 更改运价状态
        ///// </summary>
        ///// <param name="ids">ID列表</param>
        ///// <param name="isValid">是否有效</param>
        ///// <param name="changeByID">更改人</param>
        ///// <param name="updateDates">数据版本</param>
        //[FunctionInfomation]
        //ManyResultData ChangeArbitraryState(
        //    Guid[] ids,
        //    bool isValid,
        //    Guid changeByID,
        //    DateTime?[] updateDates);

        /// <summary>
        /// 删除 Arbitrarys
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">更改人</param>
        /// <param name="updateDates">数据版本</param>
       [FunctionInfomation] 
        [OperationContract]
        ManyResultData RemoveArbitrarys(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// GetOceanArbitrarysByBasePortID
        /// </summary>
        /// <param name="basePortID">basePortID</param>
        /// <returns>ArbitraryList</returns>
       [FunctionInfomation] 
        [OperationContract]
        List<ArbitraryList> GetOceanArbitrarysByBasePortID(Guid basePortID);

        /// <summary>
        /// 生成运价
        /// </summary>
        /// <param name="oceanID"></param>
       [FunctionInfomation]
       [OperationContract]
       void BuilerBaseItemForOceanID(Guid oceanID);
        #endregion

        #region SaveOceanDatas

        /// <summary>
        /// 保存所有关于合约的信息(BasePorts,Arbitrarys,AdditionalFee,Permission
        /// </summary>
        /// <param name="oceanID">oceanID</param>
        /// <param name="basePorts">basePorts</param>
        /// <param name="arbitrarys">arbitrarys</param>
        /// <param name="fees">fees</param>
        /// <param name="permissionMode">permissionMode</param>
        /// <param name="permissions">permissions</param>
        /// <param name="saveByID">saveByID</param>
        /// <returns></returns>
       [FunctionInfomation] 
        [OperationContract]
        OceanSavedResult SaveOceanDatas(
                                    Guid oceanID
                                    , BasePortCollect basePorts
                                    , bool isCopyBasePort
                                    , ArbitraryCollect arbitrarys
                                    , AdditionalFeeCollect fees
                                    , PermissionsModeCollect permissionMode
                                    , PermissionsCollect permissions
                                    , Guid saveByID);

        #endregion

        #region AdditionalFees

        /// <summary>
        /// GetOceanAdditionalFees
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <returns>AdditionalFeeList</returns>
       [FunctionInfomation]
        [OperationContract]
        List<AdditionalFeeList> GetOceanAdditionalFees(Guid oceanID);

       /// <summary>
       /// 根据BasePort获得关联的Additional
       /// </summary>
       /// <param name="oceanPort"></param>
       /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       List<AdditionalFeeList> GetOceanAdditionalFeesByBasePort(Guid oceanPort);

        /// <summary>
        /// SaveOceanAdditionalFees
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="fees">费用列表</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>ManyResult</returns>
       [FunctionInfomation] 
        [OperationContract]
        ManyResult SaveOceanAdditionalFees(
        Guid oceanID,
        AdditionalFeeCollect fees,
        Guid saveByID);


        /// <summary>
        /// RemoveAdditionalFeeInfo
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
       [FunctionInfomation] 
        [OperationContract]
        void RemoveAdditionalFeeInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);


        /// <summary>
        /// SetOceanAdditionalFee2ItemInfo
        /// </summary>
        /// <param name="feeIDs">对应的费用ID列表</param>
        /// <param name="rateIDs">运价ID列表</param>
        /// <param name="isAdditionals">标识是删除还是新增</param>
        /// <param name="setByID">设置人</param>
       [FunctionInfomation]
        [OperationContract]
        ManyResult SetOceanAdditionalFee2ItemInfo(
            Guid[] feeIDs,
            Guid[] rateIDs,
            bool[] isAdditionals,
            Guid setByID);

        /// <summary>
        /// 获取其他费用下关联的运价列表
        /// </summary>
        /// <param name="additionalFeeIDs">其他费用ID列表</param>
        /// <returns>返回其他费用下关联的运价列表</returns>
       [FunctionInfomation]  
        [OperationContract]
        List<AdditionalFee2ItemList> GetOceanAdditionalFee2ItemList(Guid[] additionalFeeIDs);

        /// <summary>
        /// 获取运价下关联的其他费用列表
        /// </summary>
        /// <param name="oceanItemIDs">运价ID列表</param>
        /// <returns>返回运价下关联的其他费用列表</returns>
       [FunctionInfomation] 
        [OperationContract]
        List<AdditionalFee2ItemList> GetOceanItem2AdditionalFeeList(Guid[] oceanItemIDs);

        #endregion

        #region BaseRates

        /// <summary>
        /// 获取BaseRates列表
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="pols">pols</param>
        /// <param name="isExclPols">isExclpols</param>
        /// <param name="vias">vias</param>
        /// <param name="isExclVias">isExclvias</param>
        /// <param name="pods">pods</param>
        /// <param name="isExclPods">isExclpods</param>
        /// <param name="deliverys">deliverys</param>
        /// <param name="isExclDeliverys">isExcldeliverys</param>
        /// <param name="itemCodes">itemCodes</param>
        /// <param name="isExclItemCodes">isExclitemCodes</param>
        /// <param name="carriers">carriers</param>
        /// <param name="isExclCarriers">isExclcarriers</param>
        /// <param name="comm">comm</param>
        /// <param name="isExclComm">isExclcomm</param>
        /// <param name="terms">terms</param>
        /// <param name="isExclTerms">isExclterms</param>
        /// <param name="surCharges">surCharges</param>
        /// <param name="isExclSurCharges">isExclsurCharges</param>
        /// <param name="description">description</param>
        /// <param name="isExclDescription">isExcldescription</param>
        /// <returns>BaseRatesList</returns>
       [FunctionInfomation]
       [OperationContract]
       BaseRateList GetOceanBaseRates(Guid oceanID
                                            , string pols, bool isExclPols
                                            , string vias, bool isExclVias
                                            , string pods, bool isExclPods
                                            , string deliverys, bool isExclDeliverys
                                            , string finalDestinations, bool isExclfinalDestinations
                                            , string itemCodes, bool isExclItemCodes
                                            , string carriers, bool isExclCarriers
                                            , string comm, bool isExclComm
                                            , string terms, bool isExclTerms
                                            , string surCharges, bool isExclSurCharges
                                            , string description, bool isExclDescription);

        /// <summary>
        /// 获取BaseRates列表
        /// </summary>
        /// <param name="ids">BaseRatesIDs</param>
        /// <returns>BaseRatesList</returns>
       [FunctionInfomation] 
       [OperationContract(Name="GetOceanBaseRates2")]
       BaseRateList GetOceanBaseRatesByIds(Guid[] ids);

        #endregion

        #region Permission

        /// <summary>
        /// 获取文档权限列表
        /// </summary>        
        /// <returns>返回权限列表</returns>
       [FunctionInfomation] 
        [OperationContract]
        List<OceanPermissionList> GetOceanPermissionList(Guid oceanID);

        /// <summary>
        /// RemoveOceanPermissionInfo
        /// </summary>
        /// <param name="ids">ids</param>
        /// <param name="updateDates">updateDates</param>
        /// <param name="removeByID">removeByID</param>
       [FunctionInfomation] 
        [OperationContract]
        void RemoveOceanPermissionInfo(
        Guid[] ids,
        DateTime?[] updateDates,
        Guid removeByID);


        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="oceanID">oceanID</param>
        /// <param name="permissionIDs">permissionIDs</param>
        /// <param name="organizationIDs">organizationIDs</param>
        /// <param name="userObjectIDs">userObjectIDs</param>
        /// <param name="userObjectTypes">userObjectTypes</param>
        /// <param name="permissions">permissions</param>
        /// <param name="updateDates">updateDates</param>
        /// <param name="setById">setById</param>
        /// <returns>ManyResult</returns>
       [FunctionInfomation]
       [OperationContract]
        ManyResult SetOceanPermissionInfo(
            Guid oceanID,
            Guid?[] permissionIDs,
            Guid?[] organizationIDs,
            Guid?[] userObjectIDs,
            UserObjectType?[] userObjectTypes,
            OceanPermission[] permissions,
            DateTime?[] updateDates,
            Guid setById);

        #endregion

        #region 合约文件管理

        /// <summary>
        /// 获取合约下文件列表 
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="userID">当前用户ID</param>
        /// <returns>返回合约下文件列表 </returns>
       [FunctionInfomation] 
        [OperationContract]
        List<OceanFileList> GetOceanFileList(
            Guid oceanID,
            Guid userID);


        /// <summary>
        /// 保存合约下文件信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="id">ID</param>
        /// <param name="fileID">文件ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileContent">文件内容</param>
        /// <param name="remarks">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回SingleResult</returns>
       [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOceanFileInfo(
            Guid oceanID,
            Guid?[] ids,
            string[] names,
            string[] descriptions,
            //byte[] fileContent,
            Guid saveByID
            , DateTime?[] updateDates);

        /// <summary>
        /// 删除合约下文件信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
       [FunctionInfomation] 
        [OperationContract]
        void RemoveOceanFileInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 获取FTP服务器信息
        /// </summary>
        /// <returns>返回</returns>
       [FunctionInfomation]
       [OperationContract]
        FTPServerConfig GetFTPServerConfig();

        #endregion

        #region 获得合约比较列表
        /// <summary>
        /// 获得合约比较信息列表
        /// </summary>
        /// <param name="contract1ID">合约1ID</param>
        /// <param name="contract2ID">合约2ID</param>
        /// <returns></returns>
       [FunctionInfomation] 
       [OperationContract]
       OceanContractComparDataList GetComparList(Guid contract1ID, Guid contract2ID);
       #endregion

        #region 反向更新账单
        /// <summary>
        /// 反向更新账单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="SaveByID"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
        HierarchyManyResult BackUpOceanHistotry(Guid ID, Guid SaveByID, bool IsEnglish);

        #endregion

        #region 获得一条运价的费用列表
        /// <summary>
        /// 获得一条运价的费用列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanRateFeeDetail GetOceanRateFeeDetail(Guid id);
        #endregion

    }
}

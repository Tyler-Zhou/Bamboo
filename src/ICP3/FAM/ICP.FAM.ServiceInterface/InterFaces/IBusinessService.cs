namespace ICP.FAM.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FAM.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;
    using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;

    /// <summary>
    /// 业务信息服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBusinessService
    {

        /// <summary>
        /// 获取业务列表
        /// </summary>
        /// <param name="companyIDs">公司ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="customer">客户</param>
        /// <param name="sales">业务员</param>
        /// <param name="filer">文件</param>
        /// <param name="hasBills">是否有帐单</param>
        /// <param name="minProfit">最少利润</param>
        /// <param name="maxProfit">最大利润</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="parameter">不同的业务类型有不同的参数</param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns>业务列表对象</returns>
        [FunctionInfomation]
        [OperationContract]
        PageList GetBusinessListByList(Guid[] companyIDs
                                            , string operationNo
                                            , string blNo
                                            , string ctnNo
                                            , string customer
                                            , string sales
                                            , string filer
                                            , decimal? minProfit
                                            , decimal? maxProfit
                                            , bool? hasBills
                                            , OperationType? operationType
                                            , OperationParameter parameter
                                           , DataPageInfo dataPageInfo
                                            );
        /// <summary>
        /// 获取业务列表
        /// </summary>
        /// <param name="businessIDs">业务ID</param>
        /// <returns>业务列表对象</returns>
        [FunctionInfomation]
        [OperationContract]
        PageList GetBusinessListByIDs(Guid[] businessIDs);


        /// <summary>
        /// 获取后续业务列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="BillTo">往来单位</param>
        /// <returns>业务列表对象</returns>
        [FunctionInfomation]
        [OperationContract]
        List<FollowingBusinessList> GetFollowingBusinessList
            (
            Guid operationID,
            Guid BillTo
            );


        /// <summary>
        /// 批量帐单
        /// </summary>
        /// <param name="operationIDs">业务ID</param>
        /// <param name="bankAccountID">银行IDs</param>
        /// <param name="currencyIDs">币种</param>
        /// <param name="chargingCodeIDs">会计科目</param>
        /// <param name="feeWays">方向</param>
        /// <param name="amounts">金额</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>ManyResult{ID,UpdateDate}</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult BatchSaveBillInfo(Guid[] operationIDs
            , Guid bankAccountID
            , Guid[] currencyIDs
            , Guid[] chargingCodeIDs
            , FeeWay[] feeWays
            , decimal[] amounts
            , Guid saveByID
            );

        /// <summary>
        /// 根据业务ID、业务类型获得对账单的状态
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AgentBillCheckStatusEnum GetABCStatue(Guid operationID, bool isEnglish);

        ///更改后续业务信息
        [FunctionInfomation]
        [OperationContract]
        SingleResult UpdateFollowingBusinessList(FollowBusinessSaveRequest followBusinessSaveRequest);

        /// <summary>
        /// 得到运费已包含的费用项目
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        FreightIncludedInfo GetOperationFreightIncluded(Guid operationId, OperationType type);

        /// <summary>
        /// 更新业务的运费已包含项目
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="freightIncluded"></param>
        /// <param name="type"></param>
        [FunctionInfomation]
        [OperationContract]
        void UpdateOperationFreightIncluded(Guid operationId, string freightIncluded, OperationType type);
    }
}

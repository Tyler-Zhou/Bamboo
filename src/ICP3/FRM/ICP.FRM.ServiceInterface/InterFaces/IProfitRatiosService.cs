
namespace ICP.FRM.ServiceInterface
{
    using System.Collections.Generic;
    using ICP.FRM.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using System.ServiceModel;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 利润配比
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IProfitRatiosService
    {
        /// <summary>
        /// 获取业务统计列表
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns>返回业务统计列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<BusinessStatisticsList> GetBusinessStatisticsList(QueryCriteria4ProfitRatios queryParamater);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ProfitRatiosAdjustment> GetProfitRatiosAdjustmentList(QueryCriteria4Adjustment queryParamater);

        /// <summary>
        /// 保存利润配比调整
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveProfitRatiosAdjustment(ProfitRatiosAdjustmentSaveRequest saveRequest);

        /// <summary>
        /// 重置利润配比调整
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        int ResetProfitRatiosAdjustment(ProfitRatiosAdjustmentSaveRequest saveRequest);
    }
}

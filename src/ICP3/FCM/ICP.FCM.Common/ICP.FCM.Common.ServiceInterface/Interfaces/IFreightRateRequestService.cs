namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using DataObjects;
    using Framework.CommonLibrary.Attributes;
    using Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using System.ServiceModel;

    /// <summary>
    /// 运价申请接口
    /// </summary>
    [ServiceInfomation("运价申请接口")]
    [ServiceContract]
    public interface IFreightRateRequestService
    {
        #region FreightRateRequest

        /// <summary>
        /// 获取运价申请记录
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <returns>返回运价申请记录</returns>
        [FunctionInfomation]
        [OperationContract]
        List<FreightRateRequestInfo> GetFreightRateRequestList(Guid oceanBookingID);

        /// <summary>
        /// 申请运价
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="oceanBookingID">订单ID</param>
        /// <param name="senderID">申请人</param>
        /// <param name="senderDate">申请时间</param>
        /// <param name="senderRemark">申请备注</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult RequestFreightRate(
            Guid? id,
            Guid oceanBookingID,
            Guid senderID,
            DateTime senderDate,
            string senderRemark,
            DateTime? updateDate);

        /// <summary>
        /// 回复运价
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="solverID">回复人</param>
        /// <param name="solveDate">回复时间</param>
        /// <param name="solverRemark">回复备注</param>
        /// <param name="freightRateID">运价ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult AnswerOceanFreightRate(
            Guid id,
            Guid solverID,
            DateTime solveDate,
            string solverRemark,
            Guid? freightRateID,
            DateTime? updateDate);

        /// <summary>
        /// 删除运价申请请求
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult RemoveFreightRateRequest(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        #endregion
    }
}

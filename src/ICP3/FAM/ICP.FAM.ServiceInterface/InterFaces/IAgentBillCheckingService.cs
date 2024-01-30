using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 代理对账服务 
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IAgentBillCheckingService
    {
        /// <summary>
        /// 查询对账单列表
        /// </summary>
        /// <param name="no">对账单号</param>
        /// <param name="companyIDs">发起公司ID集合</param>
        /// <param name="checkType">对账类型(1:内部代理对账)</param>
        /// <param name="isCompleted">是否已完成对账</param>
        /// <param name="createID">创建人ID</param>
        /// <param name="beginDate">创建开始时间</param>
        /// <param name="endDate">创建结束时间</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        PageList GetAgnetBillCheckList(
                                 string no,
                                  Guid[] companyIDs,
                                 AgentBillCheckType checkType,
                                 bool isCompleted,
                                 Guid? createID,
                                 DateTime? beginDate,
                                 DateTime? endDate,
                                 DataPageInfo dataPageInfo,
                                 bool isEnglish);

        /// <summary>
        /// 代理对账单详细信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        AgnetBillCheckList GetAgnetBillCheckInfo(Guid id, bool isEnglish);

        /// <summary>
        /// 删除代理对账单
        /// </summary>
        /// <param name="id">代理对账单ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="UpdateDate">到最后更新时间</param>
        ///  <param name="isEnglish">是否英文版本</param>
       [FunctionInfomation]  [OperationContract]
        void RemoveAgnetBillCheck(
                                 Guid id,
                                 Guid removeByID,
                                 DateTime? UpdateDate,
                                 bool isEnglish);

        /// <summary>
        /// 更新代理对账单状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="statue">状态</param>
        /// <param name="memoContent">备注内容</param>
        /// <param name="changeID">更新人</param>
        /// <param name="updateDate">更新时间--控件数据版本使用</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult ChangeAgentBillCheckStatus(
                                 Guid id,
                                 AgentBillCheckStatusEnum statue,
                                 string memoContent,
                                 Guid changeID,
                                 DateTime? updateDate,
                                 bool isEnglish);

        /// <summary>
        /// 保存代理对账单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">单号</param>
        /// <param name="launchCompanyID">发起公司ID</param>
        /// <param name="launchUserID">发起人ID</param>
        /// <param name="checkCmpanyID">核对公司ID</param>
        /// <param name="checkUserID">核对人ID</param>
        /// <param name="operTypes">业务类型</param>
        /// <param name="endingETD">截止ETD</param>
        /// <param name="createID">创建人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult SaveAgentBillCheck(
                                Guid id,
                                Guid launchCompanyID,
                                Guid launchUserID,
                                Guid checkCmpanyID,
                                Guid checkUserID,
                                string operTypes,
                                DateTime endingETD,
                                Guid createID,
                                DateTime? updateDate,
                                bool isEnglish);
        /// <summary>
        /// 获得代理对账单的明细
        /// </summary>
        /// <param name="agenBillCheckID">代理对账单ID</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        List<AgentBillCheckDetail> GetAgentBillCheckDetailList(Guid agenBillCheckID, bool isEnglish);

        /// <summary>
        /// 获得指定对账单的所有账单费用列表
        /// </summary>
        /// <param name="agenBillCheckID">对账单ID</param>
        /// <param name="type">类型(1为发起代理，2为核对代理)</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        List<WriteOffBill> GetBillFeeByAgentBillCheck(Guid agenBillCheckID, int type, bool isEnglish);

    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Agilelabs.Framework;

namespace LongWin.ReportCenter.ServiceInterface
{
    /// <summary>
    /// 为CRM提供业务数据记录
    /// </summary>
    [ServiceInfomation("IREPPUBDataService", Agilelabs.Framework.ServiceType.Business)]
    public interface IREPPUBDataService
    {
        ///// <summary>
        ///// 一个特定的客户和客户的一个特定的负责人在一段时间内的业务数据
        ///// </summary>
        ///// <param name="BeginTime">开始时间</param>
        ///// <param name="EndTime">结束时间</param>
        ///// <param name="CustomerId">指定的客户</param>
        ///// <param name="SaleId">指定的业务员</param>
        ///// <returns></returns>
        //[Agilelabs.Framework.FunctionInfomation("一个特定的客户和客户的一个特定的负责人在一段时间内的业务数据")]
        //JobInfoSet.JobInfoDataTable GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid CustomerId, Guid SaleId);
        ///// <summary>
        ///// 一个特定客户组客户在一段时间内的业务数据
        ///// </summary>
        ///// <param name="BeginTime"></param>
        ///// <param name="EndTime"></param>
        ///// <param name="CustomerIds"></param>
        ///// <param name="SaleId"></param>
        ///// <returns></returns>
        //[Agilelabs.Framework.FunctionInfomation("一个特定客户组客户在一段时间内的业务数据")]
        //JobInfoSet.JobInfoDataTable GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid[] CustomerIds, Guid SaleId);
        ///// <summary>
        ///// 获取一个委托的所有费用明细
        ///// </summary>
        ///// <param name="consignId">委托的Id</param>
        ///// <returns></returns>
        //[Agilelabs.Framework.FunctionInfomation("获取一个委托的所有费用明细")]
        //JobInfoSet.FeeOfJobDataTable GetFeesOfJob(Guid consignId);

        /// <summary>
        /// 获取一个用户是否可以查看应付费用的往来单位
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Agilelabs.Framework.FunctionInfomation("获取一个用户是否可以查看应付费用的往来单位")]
        bool GetIsViewShipper();

        /// <summary>
        /// 通过部门获取员工
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [FunctionInfomation("GetStafferByCompanyID")]
        System.Data.DataSet GetStafferByCompanyID(Guid CompanyID);
    }
}

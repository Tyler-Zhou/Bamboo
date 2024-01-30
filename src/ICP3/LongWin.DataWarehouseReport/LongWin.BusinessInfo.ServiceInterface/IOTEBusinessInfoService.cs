using System;
using System.Collections.Generic;
using System.Text;
using LongWin.BusinessInfo.ServiceInterface.DataObject;
using Agilelabs.Framework;
namespace LongWin.BusinessInfo.ServiceInterface
{
    [ServiceInfomation("IOTEBusinessInfoService", Agilelabs.Framework.ServiceType.Business)]
    public interface IOTEBusinessInfoService
    {
        /// <summary>
        /// 获取一个用户是否可以查看应付费用的往来单位
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [FunctionInfomation("获取一个用户是否可以查看应付费用的往来单位")]
        bool GetIsViewShipper(Guid userID);


        /// <summary>
        /// 一个特定的客户和客户的一个特定的负责人在一段时间内的业务数据
        /// </summary>
        /// <param name="BeginTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="CustomerId">指定的客户</param>
        /// <param name="SaleId">指定的业务员</param>
        /// <returns></returns>
        [Agilelabs.Framework.FunctionInfomation("一个特定的客户和客户的一个特定的负责人在一段时间内的业务数据")]
        List<JobInfoData> GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid CustomerId, Guid SaleId);
        
        /// <summary>
        /// 获取一个委托的所有费用明细
        /// </summary>
        /// <param name="consignId">委托的Id</param>
        /// <returns></returns>
        [Agilelabs.Framework.FunctionInfomation("获取一个委托的所有费用明细")]
        List<JobFeeData> GetFeesOfJob(Guid consignId);

        


       
    }
}

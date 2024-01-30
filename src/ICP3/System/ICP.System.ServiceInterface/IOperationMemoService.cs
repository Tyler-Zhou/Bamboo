
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.ReportCenter.ServiceInterface;
using System.Data;

namespace ICP.Sys.ServiceInterface
{
    /// <summary>
    /// 记录系统操作日志
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IOperationMemoService
    {
        /// <summary>
        /// 获取邮件中心所有操作日志
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [OperationContract]
        ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject GetAllList(Guid companyId);
        /// <summary>
        /// <summary>
        /// 获取系统错误日志列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<SystemErrorLogObject> GetSystemErrorLogList(string userName, DateTime? fromDate, DateTime? toDate);
        /// <summary>
        /// 获取系统错误日志详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SystemErrorLogObject GetSystemErrorLogInfoById(Guid id);
    }
}

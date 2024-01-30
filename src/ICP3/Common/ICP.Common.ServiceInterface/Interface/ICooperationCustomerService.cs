namespace ICP.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using System.ServiceModel;

    /// <summary>
    /// 合作客户管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ICooperationCustomerService
    {
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="name">name</param>
        /// <param name="sales">sales</param>
        /// <param name="shipLine">shipLine</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回客户列表</returns>
        [FunctionInfomation]  [OperationContract]
        List<CooperationCustomerList> GetCooperationCustomerList(
            Guid[] companyIDs,
            string name,
            string sales,
            Guid? shipLine,
            int maxRecords);
    }
}

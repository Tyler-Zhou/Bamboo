using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.ServiceModel;

namespace ICP.EDI.ServiceInterface
{
    /// <summary>
    /// EDI 配置服务
    /// </summary>
    [ServiceContract]
    public interface IEDIConfigService
    {
        /// <summary>
        /// 通过EDI发送选项获取EDI配置
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        EDIConfigItem GetEDIConfigByOption(EDISendOption sendOption);

        /// <summary>
        /// edi配置
        /// </summary>
        /// <param name="companyId">公司id</param>
        /// <param name="key">关键字</param>
        /// <param name="carrierID">承运人</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        EDIConfigItem GetEDIConfig(Guid companyId, string key, Guid carrierID); 

        /// <summary>
        /// edi配置
        /// </summary>
        /// <param name="companyId">口岸ID</param>
        /// <param name="customerIds"></param>
        /// <param name="ediMode">EDI模式</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetEDIConfig4CompanyAndCustomer")]
        EDIConfigItem GetEDIConfig(Guid companyId, Guid[] customerIds, EDIMode ediMode);

        
    }
}

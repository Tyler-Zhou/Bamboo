using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// CSP财务接口
    /// </summary>
    [ServiceInfomation("CSP财务接口")]
    [ServiceContract]
    public interface ICSPFinanceService
    {
        /// <summary>
        /// 保存CSP财务映射信息
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        [FunctionInfomation("保存CSP财务映射信息")]
        [OperationContract]
        SingleResult SaveMappingInfo(SaveRequestFinanceMapping saveRequest);
    }
}

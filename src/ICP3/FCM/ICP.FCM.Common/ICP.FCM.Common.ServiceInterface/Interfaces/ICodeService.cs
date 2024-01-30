using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICodeService
    {
        /// <summary>
        /// 获取CodeList
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CodeObject> CodeList(string Type);
    }
}

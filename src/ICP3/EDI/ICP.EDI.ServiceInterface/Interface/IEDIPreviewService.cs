using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.EDI.ServiceInterface
{
    /// <summary>
    /// EDI预览服务
    /// </summary>
    [ServiceContract]
    public interface IEDIPreviewService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEDIMode"></param>
        /// <param name="IDS"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<EDIPreviewValue> GetEDIPreviewValueList(EDIMode pEDIMode, Guid[] IDS);
    }
}

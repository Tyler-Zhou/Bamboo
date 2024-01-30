using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.Common.ServiceInterface
{
    /// <summary>
    /// 映射接口
    /// </summary>
    [ServiceInfomation("映射接口")]
    [ServiceContract]
    public interface IMappingService
    {
        /// <summary>
        /// 保存映射信息
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        [FunctionInfomation("保存映射信息")]
        [OperationContract]
        SingleResult SaveMappingInfo(SaveRequestMapping saveRequest);
    }
}

using ICP.Framework.CommonLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.Common.ServiceInterface
{
    /// <summary>
    /// 公用服务
    /// </summary>
    [ServiceInfomation("公用服务")]
    [ServiceContract]
    public interface ICommonService : IOCRService, IMappingService
    {
    }
}

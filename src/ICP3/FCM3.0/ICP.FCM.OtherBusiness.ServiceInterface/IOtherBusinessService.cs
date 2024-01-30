using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// <summary>
    /// 其他业务服务
    /// </summary>
    [ServiceInfomation("其他业务服务")]
    [ServiceContract]
    public interface IOtherBusinessService : IOtherBusiness, IOBFeeService, IOBContainerService,IOBTruckService
    {
    }
}

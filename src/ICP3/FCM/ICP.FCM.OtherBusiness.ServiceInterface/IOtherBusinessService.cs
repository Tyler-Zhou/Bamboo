using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// <summary>
    /// 其他业务服务
    /// </summary>
    [ServiceInfomation("其他业务服务")]
    [ServiceContract]
    public interface IOtherBusinessService : IOBBookingService, IOBFeeService, IOBContainerService,IOBTruckService
    {
    }
}

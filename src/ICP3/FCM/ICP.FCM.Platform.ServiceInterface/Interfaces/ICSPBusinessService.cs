using ICP.FCM.Platform.ServiceInterface;
using ICP.Framework.CommonLibrary.Attributes;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.Common.ServiceInterface.Interfaces
{
    /// <summary>
    /// CSP业务服务
    /// </summary>
    [ServiceInfomation("CSP业务服务")]
    [ServiceContract]
    public interface ICSPBusinessService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        [OperationContract]
        List<CSPBookingInfo> GetALLCSPBookingList(SearchParameterBooking searchParameter);
    }
}

using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 费用服务
    /// </summary>
    [ServiceInfomation("费用服务")]
    [ServiceContract]
    public interface IFeeService
    {
        /// <summary>
        ///  获得海进分文件比较业务时海出业务详细信息
        ///  2013-07-10 joe
        /// </summary>
        /// <param name="OIBookingID">海进业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Fee> GetCompareBillAndChargeInfo(Guid OIBookingID, OperationType OperationType);
    }
}

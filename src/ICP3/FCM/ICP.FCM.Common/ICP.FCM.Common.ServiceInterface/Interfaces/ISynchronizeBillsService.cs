using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 同步账单服务
    /// </summary>
    [ServiceInfomation("同步账单服务")]
    [ServiceContract]
    public interface ISynchronizeBillsService
    {
        /// <summary>
        ///  获得分文件比较详细信息
        /// </summary>
        /// <param name="OEBookingID">出口业务ID</param>
        /// <param name="OIBookingID">进口业务ID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Fee> DispatchCompareBillAndCharge(Guid OEBookingID, Guid OIBookingID, Guid DispatchFileLogID, OperationType OperationType);
    }
}

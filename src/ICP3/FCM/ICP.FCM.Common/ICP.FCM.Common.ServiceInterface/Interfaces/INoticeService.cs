using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 通知服务
    /// </summary>
    [ServiceInfomation("业务信息-通知服务")]
    [ServiceContract]
    public interface INoticeService
    {
        /// <summary>
        /// 获取未确认AMS业务
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<NoticeAMSBL> GetNoticeBLByAMSState();
        /// <summary>
        /// 通知所有未确认AMS提单
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        void NoticeUnconfirmedAMS();
    }
}

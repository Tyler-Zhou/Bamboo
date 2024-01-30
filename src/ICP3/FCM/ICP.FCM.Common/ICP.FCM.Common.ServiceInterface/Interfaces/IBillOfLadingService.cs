using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 提单服务(任务中心)
    /// </summary>
    [ServiceInfomation("任务中心-提单列表")]
    [ServiceContract]
    public interface IBillOfLadingService
    {
        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="businessContext">业务操作上下文</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BillOfLadingList> GetBillOfLadingList(BusinessOperationContext businessContext);

        /// <summary>
        /// 确认AMS费用
        /// </summary>
        /// <param name="saveRequest"></param>
        [FunctionInfomation]
        [OperationContract]
        bool SaveOceanBLAMSState(SaveRequestBLState saveRequest);
    }
}

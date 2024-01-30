
using ICP.Framework.CommonLibrary.Common;

namespace ICP.TaskCenter.ServiceInterface
{
    /// <summary>
    /// 任务中心客户端服务接口
    /// </summary>
    public interface IClientTaskCenterService
    {
        /// <summary>
        /// 打开任务中心
        /// </summary>
        /// <param name="operationContext">业务上下文</param>
        void OpenTaskCenter(BusinessOperationContext operationContext);

    }
}

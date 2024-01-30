using System.ServiceModel;

namespace ICP.EDIManager.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IEDIManagerService
    {
        /// <summary>
        /// 开始下载
        /// </summary>
        [OperationContract]
        void Download();

        /// <summary>
        /// 释放
        /// </summary>
        [OperationContract]
        void Dispose();
    }
}

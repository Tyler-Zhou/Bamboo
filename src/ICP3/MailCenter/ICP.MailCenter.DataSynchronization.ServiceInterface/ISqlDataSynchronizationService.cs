using System.ServiceModel;
using Microsoft.Synchronization.Data;

namespace ICP.DataSynchronization.ServiceInterface
{
    /// <summary>
    /// Sqlserver数据同步服务接口
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ISqlDataSynchronizationService: IRelationalDataSynchronizationService
    {
        [OperationContract]
        [FaultContract(typeof(DataSyncFaultException))]
        DbSyncScopeDescription GetScopeDescription();
    }
}

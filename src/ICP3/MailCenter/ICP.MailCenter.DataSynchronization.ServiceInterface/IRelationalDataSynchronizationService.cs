using System;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;

namespace ICP.DataSynchronization.ServiceInterface
{   
    /// <summary>
    /// 数据同步服务基接口
    /// </summary>
    [ServiceContract(SessionMode=SessionMode.Required)]
    [ServiceKnownType(typeof(SyncIdFormatGroup))]
    [ServiceKnownType(typeof(DbSyncContext))]
    [ServiceKnownType(typeof(SyncSchema))]
    [ServiceKnownType(typeof(DataSyncFaultException))]
    [ServiceKnownType(typeof(SyncBatchParameters))]
    [ServiceKnownType(typeof(GetChangesParameters))]
    public interface IRelationalDataSynchronizationService
    {
        [OperationContract(IsInitiating=true)]
        void Initialize(String scopeName,Guid userId);

        [OperationContract]
        void BeginSession(SyncProviderPosition position);

        [OperationContract]
        SyncBatchParameters GetKnowledge();

        [OperationContract]
        GetChangesParameters GetChanges(uint batchSize, SyncKnowledge destinationKnowledge);

        [OperationContract]
        SyncSessionStatistics ApplyChanges(ConflictResolutionPolicy resolutionPolicy, ChangeBatch sourceChanges, object changeData);

        [OperationContract]
        bool HasUploadedBatchFile(String batchFileid, String remotePeerId);

        [OperationContract]
        void UploadBatchFile(String batchFileid, byte[] batchFile, String remotePeerId);

        [OperationContract]
        byte[] DownloadBatchFile(String batchFileId);

        [OperationContract]
        void EndSession();

        [OperationContract(IsTerminating= true)]
        void Cleanup();
    }

    [DataContract]
    public class SyncBatchParameters
    {
        [DataMember]
        public SyncKnowledge DestinationKnowledge;

        [DataMember]
        public uint BatchSize;
    }

    [DataContract]
    [KnownType(typeof(DataSet))]
    public class GetChangesParameters
    {
        [DataMember]
        public object DataRetriever;

        [DataMember]
        public ChangeBatch ChangeBatch;
    }
}

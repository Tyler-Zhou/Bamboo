using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using ICP.DataSynchronization.ServiceInterface;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;

namespace ICP.DataSynchronization.ServiceComponent
{   
    /// <summary>
    /// 数据同步服务基类
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, AutomaticSessionShutdown = true)]
    public abstract class RelationalDataSynchronizationService: IRelationalDataSynchronizationService
    {
        protected bool isProxyToCompactDatabase;
        protected RelationalSyncProvider peerProvider;
        protected DirectoryInfo sessionBatchingDirectory = null;
        protected Dictionary<String, String> batchIdToFileMapper;
        public Guid id = Guid.NewGuid();
        int batchCount = 0;

        public void Initialize(String scopeName,Guid userId)
        {
            this.peerProvider = this.ConfigureProvider(scopeName,userId);
            this.batchIdToFileMapper = new Dictionary<String, String>();
        }

        public void Cleanup()
        {
            if (this.peerProvider != null)
            {
                this.peerProvider.Connection.Close();
                this.peerProvider.Dispose();
                this.peerProvider = null;
            }
            if (sessionBatchingDirectory != null)
            {
                sessionBatchingDirectory.Refresh();

                if (sessionBatchingDirectory.Exists)
                {
                    try
                    {
                        sessionBatchingDirectory.Delete(true);
                    }
                    catch
                    {
                        //Ignore 
                    }
                }
            }
        }

        public void BeginSession(SyncProviderPosition position)
        {
   
            this.batchIdToFileMapper = new Dictionary<String, String>();

            this.peerProvider.BeginSession(position, null);
            this.batchCount = 0;
        }

        public SyncBatchParameters GetKnowledge()
        {
         
            SyncBatchParameters destParameters = new SyncBatchParameters();
            this.peerProvider.GetSyncBatchParameters(out destParameters.BatchSize, out destParameters.DestinationKnowledge);
            return destParameters;
        }

        public GetChangesParameters GetChanges(uint batchSize, SyncKnowledge destinationKnowledge)
        {
          
            GetChangesParameters changesWrapper = new GetChangesParameters();
            changesWrapper.ChangeBatch  = this.peerProvider.GetChangeBatch(batchSize, destinationKnowledge, out changesWrapper.DataRetriever);

            DbSyncContext context = changesWrapper.DataRetriever as DbSyncContext;
           
            if (context != null && context.IsDataBatched)
            {
               
               
                String fileName = new FileInfo(context.BatchFileName).Name;
                this.batchIdToFileMapper[fileName] = context.BatchFileName;
                context.BatchFileName = fileName;
            }
            return changesWrapper;
        }

        public SyncSessionStatistics ApplyChanges(ConflictResolutionPolicy resolutionPolicy, ChangeBatch sourceChanges, object changeData)
        {
        

            DbSyncContext dataRetriever = changeData as DbSyncContext;

            if (dataRetriever != null && dataRetriever.IsDataBatched)
            {
                String remotePeerId = dataRetriever.MadeWithKnowledge.ReplicaId.ToString();
                String localBatchFileName = null;
                if (!this.batchIdToFileMapper.TryGetValue(dataRetriever.BatchFileName, out localBatchFileName))
                {
                   throw new FaultException<DataSyncFaultException>(new DataSyncFaultException("No batch file uploaded for id " + dataRetriever.BatchFileName, null));
                }
                dataRetriever.BatchFileName = localBatchFileName;
            }

            SyncSessionStatistics sessionStatistics = new SyncSessionStatistics();
            this.peerProvider.ProcessChangeBatch(resolutionPolicy, sourceChanges, changeData, new SyncCallbacks(), sessionStatistics);
            return sessionStatistics;
        }

        public void EndSession()
        {
            this.peerProvider.EndSession(null);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batchFileId"></param>
        /// <param name="remotePeerId"></param>
        /// <returns></returns>
        public bool HasUploadedBatchFile(String batchFileId, String remotePeerId)
        {
            this.CheckAndCreateBatchingDirectory(remotePeerId);

            FileInfo fileInfo = new FileInfo(Path.Combine(this.sessionBatchingDirectory.FullName, batchFileId));
            if (fileInfo.Exists && !this.batchIdToFileMapper.ContainsKey(batchFileId))
            {
                
                this.batchIdToFileMapper.Add(batchFileId, fileInfo.FullName);
            }
            return fileInfo.Exists;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="batchFileId"></param>
       /// <param name="batchContents"></param>
       /// <param name="remotePeerId"></param>
        public void UploadBatchFile(String batchFileId, byte[] batchContents, String remotePeerId)
        {
          
            try
            {
                if (HasUploadedBatchFile(batchFileId, remotePeerId))
                {
                    return;
                }
                
                String localFileLocation = Path.Combine(sessionBatchingDirectory.FullName, batchFileId);
                FileStream fs = new FileStream(localFileLocation, FileMode.Create, FileAccess.Write);
                using (fs)
                {
                        fs.Write(batchContents, 0, batchContents.Length);
                }
                this.batchIdToFileMapper[batchFileId] = localFileLocation;
            }
            catch (Exception e)
            {
                throw new FaultException<DataSyncFaultException>(new DataSyncFaultException("Unable to save batch file.", e));
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="batchFileId"></param>
       /// <returns></returns>
        public byte[] DownloadBatchFile(String batchFileId)
        {
            try
            {
               
                Stream localFileStream = null;

                String localBatchFileName = null;

                if (!this.batchIdToFileMapper.TryGetValue(batchFileId, out localBatchFileName))
                {
                    throw new FaultException<DataSyncFaultException>(new DataSyncFaultException("Unable to retrieve batch file for id." + batchFileId, null));
                }

                using (localFileStream = new FileStream(localBatchFileName, FileMode.Open, FileAccess.Read))
                {

                    using (Stream stream = DataSynchronizationUtility.DecompressStream(localFileStream))
                    {
                        byte[] contents = new byte[stream.Length];
                        stream.Read(contents, 0, contents.Length);
                        return contents;
                    }
                }                
            }
            catch (Exception e)
            {
                throw new FaultException<DataSyncFaultException>(new DataSyncFaultException("Unable to read batch file for id " + batchFileId, e));
            }
        }
        protected abstract RelationalSyncProvider ConfigureProvider(String scopeName,Guid userId);

        private void CheckAndCreateBatchingDirectory(String remotePeerId)
        {
           
            if (sessionBatchingDirectory == null)
            {
               
                String sessionDir = Path.Combine(this.peerProvider.BatchingDirectory, "DataSync_" + remotePeerId);
                sessionBatchingDirectory = new DirectoryInfo(sessionDir);
                if (!sessionBatchingDirectory.Exists)
                {
                    sessionBatchingDirectory.Create();
                }
            }
        }
    }
}

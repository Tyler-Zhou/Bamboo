using System;
using System.IO;
using System.ServiceModel;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;

namespace ICP.DataSynchronization.ServiceInterface
{
    public abstract class RelationalProviderProxy : KnowledgeSyncProvider, IDisposable
    {
        protected IRelationalDataSynchronizationService proxy;
        protected SyncIdFormatGroup idFormatGroup;
        protected String scopeName;
        protected DirectoryInfo localBatchingDirectory;
        protected bool disposed = false;

        private String batchingDirectory = Environment.ExpandEnvironmentVariables("%TEMP%");

        public String BatchingDirectory
        {
            get { return batchingDirectory; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("value cannot be null or empty");
                }
                try
                {
                    Uri uri = new Uri(value);
                    if (!uri.IsFile || uri.IsUnc)
                    {
                        throw new ArgumentException("value must be a local directory");
                    }
                    batchingDirectory = value;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Invalid batching directory.", e);
                }
            }
        }

        public RelationalProviderProxy(String scopeName, Guid userId)
        {
            this.scopeName = scopeName;
            this.CreateProxy();
            this.proxy.Initialize(scopeName, userId);
        }

        public override void BeginSession(SyncProviderPosition position, SyncSessionContext syncSessionContext)
        {
            this.proxy.BeginSession(position);
        }


        public override void EndSession(SyncSessionContext syncSessionContext)
        {
            proxy.EndSession();
            if (this.localBatchingDirectory != null)
            {
                this.localBatchingDirectory.Refresh();

                if (this.localBatchingDirectory.Exists)
                {
                    this.localBatchingDirectory.Delete(true);
                }
            }
        }

        public override ChangeBatch GetChangeBatch(uint batchSize, SyncKnowledge destinationKnowledge, out object changeDataRetriever)
        {
            GetChangesParameters changesWrapper = proxy.GetChanges(batchSize, destinationKnowledge);

            changeDataRetriever = changesWrapper.DataRetriever;

            DbSyncContext context = changeDataRetriever as DbSyncContext;
            try
            {
                if (context != null && context.IsDataBatched)
                {
                    if (this.localBatchingDirectory == null)
                    {
                        String remotePeerId = context.MadeWithKnowledge.ReplicaId.ToString();

                        String sessionDir = Path.Combine(this.batchingDirectory, "DataSync_" + remotePeerId);
                        this.localBatchingDirectory = new DirectoryInfo(sessionDir);

                        if (!this.localBatchingDirectory.Exists)
                        {
                            this.localBatchingDirectory.Create();
                        }
                    }

                    String localFileName = Path.Combine(this.localBatchingDirectory.FullName, context.BatchFileName);
                    FileInfo localFileInfo = new FileInfo(localFileName);


                    if (!localFileInfo.Exists)
                    {
                        byte[] remoteFileContents = this.proxy.DownloadBatchFile(context.BatchFileName);

                        using (MemoryStream compressedStream = new MemoryStream(remoteFileContents))
                        {
                            using (Stream stream = DataSynchronizationUtility.UncompressStream(compressedStream))
                            {
                                byte[] buffer = new byte[stream.Length];
                                int checkCounter = stream.Read(buffer, 0, buffer.Length);
                                using (FileStream localFileStream = new FileStream(localFileName, FileMode.Create, FileAccess.Write))
                                {
                                    localFileStream.Write(buffer, 0, buffer.Length);
                                }
                            }

                        }
                    }

                    context.BatchFileName = localFileName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return changesWrapper.ChangeBatch;
        }

        public override FullEnumerationChangeBatch GetFullEnumerationChangeBatch(uint batchSize, SyncId lowerEnumerationBound, SyncKnowledge knowledgeForDataRetrieval, out object changeDataRetriever)
        {
            throw new NotImplementedException();
        }

        public override void GetSyncBatchParameters(out uint batchSize, out SyncKnowledge knowledge)
        {
            SyncBatchParameters wrapper = proxy.GetKnowledge();
            batchSize = wrapper.BatchSize;
            knowledge = wrapper.DestinationKnowledge;
        }

        public override SyncIdFormatGroup IdFormats
        {
            get
            {
                if (idFormatGroup == null)
                {
                    idFormatGroup = new SyncIdFormatGroup();

                    //
                    // 1 byte change unit id (Harmonica default before flexible ids)
                    //
                    idFormatGroup.ChangeUnitIdFormat.IsVariableLength = false;
                    idFormatGroup.ChangeUnitIdFormat.Length = 1;

                    //
                    // Guid replica id
                    //
                    idFormatGroup.ReplicaIdFormat.IsVariableLength = false;
                    idFormatGroup.ReplicaIdFormat.Length = 16;


                    //
                    // Sync global id for item ids
                    //
                    idFormatGroup.ItemIdFormat.IsVariableLength = true;
                    idFormatGroup.ItemIdFormat.Length = 10 * 1024;
                }

                return idFormatGroup;
            }
        }

        public override void ProcessChangeBatch(ConflictResolutionPolicy resolutionPolicy, ChangeBatch sourceChanges, object changeDataRetriever, SyncCallbacks syncCallbacks, SyncSessionStatistics sessionStatistics)
        {
            DbSyncContext context = changeDataRetriever as DbSyncContext;
            if (context != null && context.IsDataBatched)
            {
                String fileName = new FileInfo(context.BatchFileName).Name;

                String peerId = context.MadeWithKnowledge.ReplicaId.ToString();

                if (!this.proxy.HasUploadedBatchFile(fileName, peerId))
                {
                    FileStream stream = new FileStream(context.BatchFileName, FileMode.Open, FileAccess.Read);
                    byte[] contents = new byte[stream.Length];
                    using (stream)
                    {
                        stream.Read(contents, 0, contents.Length);
                    }
                    this.proxy.UploadBatchFile(fileName, contents, peerId);
                }

                context.BatchFileName = fileName;
            }

            SyncSessionStatistics stats = this.proxy.ApplyChanges(resolutionPolicy, sourceChanges, changeDataRetriever);
            sessionStatistics.ChangesApplied += stats.ChangesApplied;
            sessionStatistics.ChangesFailed += stats.ChangesFailed;

        }

        public override void ProcessFullEnumerationChangeBatch(ConflictResolutionPolicy resolutionPolicy, FullEnumerationChangeBatch sourceChanges, object changeDataRetriever, SyncCallbacks syncCallbacks, SyncSessionStatistics sessionStatistics)
        {
            throw new NotImplementedException();
        }

        protected abstract void CreateProxy();


        ~RelationalProviderProxy()
        {
            Dispose(false);
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (proxy != null)
                    {
                        CloseProxy();
                    }
                }

                disposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void CloseProxy()
        {
            if (proxy != null)
            {
                proxy.Cleanup();
                ICommunicationObject channel = proxy as ICommunicationObject;
                if (channel != null)
                {
                    try
                    {
                        channel.Close();
                    }
                    catch (TimeoutException)
                    {
                        channel.Abort();
                    }
                    catch (CommunicationException)
                    {
                        channel.Abort();
                    }
                }

                proxy = null;
            }
        }

        #endregion
    }
}

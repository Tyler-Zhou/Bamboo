using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.SubscriptionPublish.ServiceInterface;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.DataCache.ServiceComponent
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public class FileService : PublishService<ISubscriptionPublishNotifyService>, IFileService
    {

        string methodName = "Upload";

        public bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }

        public Guid ClientId
        {
            get
            {
                return ApplicationContext.Current.ClientId;
            }
        }

        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void Save(List<DocumentInfo> newDocuments, List<string> deleteFileNames, List<Guid> operationIds)
        {
            ApplicationContext context = ApplicationContext.Current;
            FileSaveEntity fileSaveEntity = new FileSaveEntity(newDocuments.ToArray(), context);

            try
            {
                int deleteCount = deleteFileNames.Count;
                if (deleteFileNames != null && operationIds != null && deleteCount > 0 && deleteCount == operationIds.Count)
                {
                    ListDictionary<Guid, string> dic = new ListDictionary<Guid, string>();
                    for (int i = 0; i < deleteCount; i++)
                    {
                        dic.Add(operationIds[i], deleteFileNames[i]);
                    }
                    foreach (var item in dic)
                    {
                        Delete(item.Value, item.Key);
                    }
                }

                if (newDocuments == null || newDocuments.Count <= 0)
                {
                    FireEvent("Upload", context.ClientId, NotifyType.Sucessed, newDocuments.ToArray(), new ManyResult());
                    return;
                }
                else
                {
                    InnerUpload(fileSaveEntity);
                }

            }
            catch (Exception ex)
            {
                if (fileSaveEntity.Documents == null)
                    return;
                Guid[] ids = (from d in fileSaveEntity.Documents
                              select d.Id).ToArray<Guid>();

                FireEvent(methodName, context.ClientId, NotifyType.Failed, ids.ToArray(), ex.Message);
            }
        }
        public void Save(List<DocumentInfo> newDocuments, List<Guid> listDeleteIds, List<DateTime?> updateDates)
        {
            try
            {
                Delete(listDeleteIds, updateDates);
                ApplicationContext context = ApplicationContext.Current;
                FileSaveEntity fileSaveEntity = new FileSaveEntity(newDocuments.ToArray(), context);
                if (newDocuments == null || newDocuments.Count <= 0)
                {
                    FireEvent("Upload", context.ClientId, NotifyType.Sucessed, newDocuments.ToArray(), new ManyResult());
                    return;
                }
                else
                {
                    InnerUpload(fileSaveEntity);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangeBind(Guid[] Ids, Guid?[] FormIds, Guid saveby)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspChangeOperationFileBind]");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, Ids.Join());
                db.AddInParameter(dbCommand, "@FormIds", DbType.String, FormIds.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveby);

                int num = db.ExecuteNonQuery(dbCommand);
                if (num > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region MessageDoc
        /// <summary>
        /// 保存邮件到MessageDoc
        /// </summary>
        /// <param name="document"></param>
        public void SaveMailItemToMessageDoc(DocumentInfo document)
        {
            //WaitCallback callback = data =>
            //{
            //    FileSaveEntity entity = data as FileSaveEntity;
            //    SaveMailItem(entity);

            //};
            //ApplicationContext context = ApplicationContext.Current;
            //FileSaveEntity fileSaveEntity = new FileSaveEntity(new[] { document }, context);
            //ThreadPool.QueueUserWorkItem(callback, fileSaveEntity);
            SaveMailItem(document);
        }

        private void SaveMailItem(DocumentInfo document)
        {
            if (document == null)
                return;
            System.Diagnostics.Stopwatch stopwatchTotaltime = StopwatchHelper.StartStopwatch();
            
            ApplicationContext context = ApplicationContext.Current;
            DataTable dtBody = DataCacheUtility.CreateAttachmentTable("Bodies");

            dtBody.Rows.Add(document.Id, document.Name, document.Content);

            ManyResult results = null;
            //bool isSaveFailed = false;
            string errorMessage = string.Empty;
            //List<Guid> documentIds = new List<Guid>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.SaveMailFiles");

            SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
            parameterBodies.Direction = ParameterDirection.Input;
            parameterBodies.SqlDbType = SqlDbType.Structured;
            parameterBodies.TypeName = "oa.uttFiles";
            dbCommand.Parameters.Add(parameterBodies);

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                //isSaveFailed = true;
                errorMessage = string.Format("SaveMailItem Failed DocumentID:[{0}] [Total:{1}ms]  \r\n{2}", document.Id, stopwatchTotaltime.ElapsedMilliseconds, ex.Message);
                //documentIds.Add(document.Id);
                Framework.CommonLibrary.LogHelper.SaveLog(errorMessage);
                throw new Exception(errorMessage);
            }
            //string methodName = "SaveMailItem";
            //if (!isSaveFailed)
            //{
            //    document.Content = document.HtmlContent = null;
            //    FireEvent(methodName, context.ClientId, NotifyType.Sucessed, documentIds.ToArray(), results);
            //}
            //else
            //{
            //    FireEvent(methodName, context.ClientId, NotifyType.Failed, documentIds.ToArray(), errorMessage);
            //}
        }

        private void SaveMailItem(FileSaveEntity saveEntity)
        {
            if (saveEntity.Documents == null || saveEntity.Documents.Length <= 0)
                return;
            DocumentInfo[] documents = saveEntity.Documents;
            ApplicationContext context = saveEntity.Context;
            
            DataTable dtBody = DataCacheUtility.CreateAttachmentTable("Bodies");

            foreach (DocumentInfo document in documents)
            {
                dtBody.Rows.Add(document.Id, document.Name, document.Content);
            }

            DateTime?[] updateDates = (from d in documents
                                       select d.UpdateDate).ToArray<DateTime?>();
            int count = documents.Length;
            ManyResult results = null;
            bool isSaveFailed = false;
            string errorMessage = string.Empty;
            List<Guid> documentIds = new List<Guid>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.SaveMailFiles");

            SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
            parameterBodies.Direction = ParameterDirection.Input;
            parameterBodies.SqlDbType = SqlDbType.Structured;
            parameterBodies.TypeName = "oa.uttFiles";
            dbCommand.Parameters.Add(parameterBodies);

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                isSaveFailed = true;
                errorMessage = ex.Message;
                Array.ForEach(documents, document => { documentIds.Add(document.Id); });
            }
            string methodName = "SaveMailItem";
            if (!isSaveFailed)
            {
                for (int i = 0; i < documents.Length; i++)
                {
                    documents[i].Content = documents[i].HtmlContent = null;
                }
                FireEvent(methodName, context.ClientId, NotifyType.Sucessed, documents, results);
            }
            else
            {
                FireEvent(methodName, context.ClientId, NotifyType.Failed, documentIds.ToArray(), errorMessage);
            }
        }
        #endregion

        #region OADoc
        /// <summary>
        /// 保存文档到OADoc
        /// </summary>
        /// <param name="document"></param>
        public void SaveDocumentToOADoc(DocumentInfo document)
        {
            DataTable dtBody = DataCacheUtility.CreateAttachmentTable("Bodies");
            dtBody.Rows.Add(document.Id, document.Name, document.Content);

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.SaveOADocumentFiles");

            SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
            parameterBodies.Direction = ParameterDirection.Input;
            parameterBodies.SqlDbType = SqlDbType.Structured;
            parameterBodies.TypeName = "oa.uttFiles";
            dbCommand.Parameters.Add(parameterBodies);

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// 获取OA文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContentInfo GetOADocumentContent(Guid id)
        {
            return InnerOAGetContent(new Guid[] { id }, false).FirstOrDefault();
        }
        private IEnumerable<ContentInfo> InnerOAGetContent(Guid[] ids, bool isCopy)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetOADocumentInfo");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@IsCopy", DbType.Boolean, isCopy);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<ContentInfo>();
            List<ContentInfo> contents = (from c in set.Tables[0].AsEnumerable()
                                          select new ContentInfo
                                          {
                                              Content = c.Field<byte[]>("Body"),
                                              Id = c.Field<Guid>("Id"),
                                              Name = c.Field<String>("Name")
                                          }).ToList();
            return contents;
        }
        #endregion

        public void Upload(DocumentInfo document)
        {
            Upload(new DocumentInfo[] { document });
        }
        public void Upload(DocumentInfo[] documents)
        {
            WaitCallback callback = data =>
            {
                FileSaveEntity entity = data as FileSaveEntity;
                InnerUpload(entity);

            };
            ApplicationContext context = ApplicationContext.Current;
            FileSaveEntity fileSaveEntity = new FileSaveEntity(documents, context);
            ThreadPool.QueueUserWorkItem(callback, fileSaveEntity);
        }

        private void InnerUpload(FileSaveEntity saveEntity)
        {

            if (saveEntity.Documents == null || saveEntity.Documents.Length <= 0)
                return;
            DocumentInfo[] documents = saveEntity.Documents;
            ApplicationContext context = saveEntity.Context;

            bool isSaveFailed = false;
            string errorMessage = string.Empty;
            ManyResult results = null;
            List<Guid> documentIds = new List<Guid>();
            try
            {
                Guid[] ids = (from d in documents
                    select d.Id).ToArray<Guid>();
                Guid[] operationIds = (from d in documents
                    select d.OperationID).ToArray<Guid>();
                int[] formTypes = (from d in documents
                    select d.FormType.GetHashCode()).ToArray<int>();
                int[] documentTypes = (from d in documents
                    select d.DocumentType.GetHashCode()).ToArray<int>();
                int[] fileSources = (from d in documents
                    select d.FileSources.GetHashCode()).ToArray<int>();
                String[] names = (from d in documents
                    select d.Name).ToArray<String>();
                Guid?[] formid = (from d in documents
                    select d.FormId).ToArray<Guid?>();

                DataTable dtBody = DataCacheUtility.CreateAttachmentTable("Bodies");
                DataTable dtCopy = DataCacheUtility.CreateAttachmentTable("Copies");


                foreach (DocumentInfo document in documents)
                {

                    dtBody.Rows.Add(document.OperationID, document.Name, document.Content);
                    dtCopy.Rows.Add(document.OperationID, document.Name, document.HtmlContent);
                }

                String[] remarks = (from d in documents
                    select d.Remark).ToArray<String>();

                DateTime?[] updateDates = (from d in documents
                    select d.UpdateDate).ToArray<DateTime?>();
                int count = documents.Length;



                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOperationFileInfo");
                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.ToArray().Join());
                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIds.Join());
                db.AddInParameter(dbCommand, "@FormTypes", DbType.String, formTypes.Join());
                db.AddInParameter(dbCommand, "@FormIds", DbType.String, formid.Join());
                db.AddInParameter(dbCommand, "@DocumentTypes", DbType.String, documentTypes.Join());
                db.AddInParameter(dbCommand, "@Names", DbType.String, names.Join());
                SqlParameter parameterBodies = new SqlParameter("@Bodies", dtBody);
                parameterBodies.Direction = ParameterDirection.Input;
                parameterBodies.SqlDbType = SqlDbType.Structured;
                parameterBodies.TypeName = "oa.uttFiles";
                dbCommand.Parameters.Add(parameterBodies);

                SqlParameter parameterCopies = new SqlParameter("@CopyBodies", dtCopy);
                parameterBodies.Direction = ParameterDirection.Input;
                parameterBodies.SqlDbType = SqlDbType.Structured;
                parameterBodies.TypeName = "oa.uttFiles";
                dbCommand.Parameters.Add(parameterCopies);

                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarks.Join());
                db.AddInParameter(dbCommand, "@FileSources", DbType.String, fileSources.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, context.IsEnglish);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, context.UserId);

                results = db.ManyResult(dbCommand, new String[] {"ID", "UpdateDate"});

            }
            catch (Exception ex)
            {
                isSaveFailed = true;
                errorMessage = ex.Message;
                Array.ForEach(documents, document => { documentIds.Add(document.Id); });
            }
            string methodName = "Upload";
            if (!isSaveFailed)
            {
                for (int i = 0; i < documents.Length; i++)
                {
                    documents[i].Content = documents[i].HtmlContent = null;
                }
                FireEvent(methodName, context.ClientId, NotifyType.Sucessed, documents, results);
            }
            else
            {
                FireEvent(methodName, context.ClientId, NotifyType.Failed, documentIds.ToArray(), errorMessage);
            }

        }
        private void saveLog(string path)
        {

            string appDataDir = AppDomain.CurrentDomain.BaseDirectory;
            string logFile = System.IO.Path.Combine(appDataDir, "serviceLog.txt");

            using (var sw = new System.IO.StreamWriter(logFile, true))
            {
                sw.WriteLine("Log entry at {0},{1}", DateTime.Now, path);
            }


        }

        public ContentInfo GetDocumentHtmlContent(Guid id)
        {
            return InnerGetContent(new Guid[] { id }, true).FirstOrDefault();
        }
        public ContentInfo GetDocumentContent(Guid id)
        {
            return InnerGetContent(new Guid[] { id }, false).FirstOrDefault();
        }
        public List<ContentInfo> GetDocumentContents(List<Guid> ids)
        {
            return InnerGetContent(ids.ToArray(), false);
        }
        private List<ContentInfo> InnerGetContent(Guid[] ids, bool isCopy)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileInfo");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@IsCopy", DbType.Boolean, isCopy);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<ContentInfo>();
            List<ContentInfo> contents = (from c in set.Tables[0].AsEnumerable()
                                          select new ContentInfo
                                          {
                                              Content = c.Field<byte[]>("Body"),
                                              Id = c.Field<Guid>("Id"),
                                              Name = c.Field<String>("Name"),
                                              OperationID = c.Field<Guid>("OperationID"),   //业务操作ID
                                              TypeCode =(DocumentType?)c.Field<byte>("DocumentType")    //文档类型：单证类型
                                          }).ToList();
            return contents;
        }

        public ListDictionary<Guid, string> GetBusinessDocumentsName(BusinessOperationContext context)
        {
            DocumentState documentState = DocumentState.Pending;
            if (context.ContainsKey("DocumentState"))
            {
                documentState = (DocumentState) context["DocumentState"];
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileList");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, context.OperationID);
            db.AddInParameter(dbCommand, "@OnlyDispatched", DbType.Boolean, documentState == DocumentState.Dispatched ? true : false);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new ListDictionary<Guid, string>();

            List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                            select new DocumentInfo
                                            {
                                                Id = document.Field<Guid>("Id"),
                                                Name = document.Field<String>("Name")
                                            }).ToList();

            ListDictionary<Guid, string> listdictionary = new ListDictionary<Guid, string>();
            foreach (var item in documents)
            {
                listdictionary.Add(item.Id, item.Name);
            }

            return listdictionary;

        }

        public void Delete(List<Guid> ids, List<DateTime?> updateDates)
        {
            if (ids == null || ids.Count <= 0)
                return;
            ApplicationContext context = ApplicationContext.Current;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationFileInfo");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.ToArray().Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, this.UserId);

            ManyResult result = db.ManyResult(dbCommand,new string[]{"OperationID","OperationType"});
            
            
            string methodName = "Upload";

            FireEvent(methodName, context.ClientId, NotifyType.Delete, ids.ToArray(), result);
        }

        public void Delete(List<string> fileNames, Guid operationID)
        {
            if (fileNames == null || fileNames.Count <= 0) { return; }
            if (operationID == null || operationID == Guid.Empty) { return; }
            ApplicationContext context = ApplicationContext.Current;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationFile");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
            db.AddInParameter(dbCommand, "@Names", DbType.String, fileNames.ToArray().Join());
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);


            ManyResult result = db.ManyResult(dbCommand, new string[] { "OperationID", "OperationType" });
            

            string methodName = "Upload";

            FireEvent(methodName, context.ClientId, NotifyType.Delete, fileNames.ToArray(), result);

        }


        public List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context)
        {
            DocumentState documentState = DocumentState.Pending;
            if (context.ContainsKey("DocumentState"))
            {
                documentState = (DocumentState)context["DocumentState"];
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileList");
            db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, context.OperationID.ToString());
            db.AddInParameter(dbCommand, "@OnlyDispatched", DbType.Boolean, documentState == DocumentState.Dispatched ? true : false);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<DocumentInfo>();

            List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                            select new DocumentInfo
                                            {
                                                CreateBy = document.Field<Guid>("CreateByID"),
                                                CreateByName = document.Field<String>("CreateByName"),
                                                // CreateDate = document.Field<DateTime>("CreateDate"),
                                                DocumentType = (DocumentType)document.Field<byte>("DocumentType"),
                                                FormType = (FormType)document.Field<byte>("FormType"),
                                                Id = document.Field<Guid>("Id"),
                                                Name = document.Field<String>("Name"),
                                                OperationID = document.Field<Guid>("OperationID"),
                                                Remark = document.Field<String>("Remark"),
                                                State = UploadState.Successed,
                                                Type = (OperationType)document.Field<byte>("OperationType"),
                                                UpdateDate = document.Field<DateTime?>("UpdateDate"),
                                                DocumentTypeName = document.Field<String>("DocumentTypeName"),
                                                // UpdateBy = document.Field<Guid?>("UpdateBy"),
                                                CreateDate = document.Field<DateTime>("CreateDate"),
                                                FileSources = document.IsNull("FileSource") ? FileSource.FDocument : (FileSource)document.Field<Byte>("FileSource"),
                                                FormId = document.Field<Guid?>("FormId")
                                            }).ToList();
            return documents;
        }

        #region GetBusinessDocumentList By DataSearchType
        public List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context, DataSearchType dataSearchType)
        {
            DataSet dsResult = null;
            DataSet dds = null;
            DataSet sds = null;
            dds = GetBusinessDocumentDataSet(context, dataSearchType == DataSearchType.ALL?DataSearchType.Local:dataSearchType, dataSearchType == DataSearchType.Agent ? null : (bool?)true);
            sds = GetBusinessDocumentDataSet(context, dataSearchType == DataSearchType.ALL ? DataSearchType.Agent : dataSearchType, dataSearchType == DataSearchType.Local ? null : (bool?)false);
            dsResult = DataSetHelper.MergeSet(dds, sds);
            if (dsResult == null || dsResult.Tables.Count < 1)
            {
                return null;
            }


            List<DocumentInfo> documents = (from document in dsResult.Tables[0].AsEnumerable()
                                            select new DocumentInfo
                                            {
                                                CreateBy = document.Field<Guid>("CreateByID"),
                                                CreateByName = document.Field<String>("CreateByName"),
                                                DocumentType = (DocumentType)document.Field<byte>("DocumentType"),
                                                FormType = (FormType)document.Field<byte>("FormType"),
                                                Id = document.Field<Guid>("Id"),
                                                Name = document.Field<String>("Name"),
                                                OperationID = document.Field<Guid>("OperationID"),
                                                Remark = document.Field<String>("Remark"),
                                                State = UploadState.Successed,
                                                Type = (OperationType)document.Field<byte>("OperationType"),
                                                UpdateDate = document.Field<DateTime?>("UpdateDate"),
                                                DocumentTypeName = document.Field<String>("DocumentTypeName"),
                                                CreateDate = document.Field<DateTime>("CreateDate"),
                                                FileSources = document.IsNull("FileSource") ? FileSource.FDocument : (FileSource)document.Field<Byte>("FileSource"),
                                                FormId = document.Field<Guid?>("FormId")
                                            }).ToList();
            return documents;
        }

        private DataSet GetBusinessDocumentDataSet(BusinessOperationContext context, DataSearchType dataSearchType, bool? isDefaultDB)
        {
            if (isDefaultDB == null)
                return null;
            DocumentState documentState = DocumentState.Pending;
            if (context.ContainsKey("DocumentState"))
            {
                documentState = (DocumentState)context["DocumentState"];
            }
            Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB.Value);
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileList");
            db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, context.OperationID.ToString());
            db.AddInParameter(dbCommand, "@DataSearchType", DbType.Int16, dataSearchType);
            db.AddInParameter(dbCommand, "@OnlyDispatched", DbType.Boolean, documentState == DocumentState.Dispatched ? true : false);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return null;
            return set;
        }
        #endregion

        /// <summary>
        /// 获取多票业务下的文档
        /// </summary>
        /// <param name="contextlist"></param>
        /// <returns></returns>
        public List<DocumentInfo> GetBusinessDocumentList(List<BusinessOperationContext> contextlist)
        {
            List<Guid> conlist = new List<Guid>();
            contextlist.ForEach(c=>conlist.Add(c.OperationID));
            DocumentState documentState = DocumentState.Pending;
            if (contextlist[0].ContainsKey("DocumentState"))
            {
                documentState = (DocumentState)contextlist[0]["DocumentState"];
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileList");
            db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, conlist.ToArray().Join());
            db.AddInParameter(dbCommand, "@OnlyDispatched", DbType.Boolean, documentState == DocumentState.Dispatched ? true : false);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<DocumentInfo>();

            List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                            select new DocumentInfo
                                            {
                                                CreateBy = document.Field<Guid>("CreateByID"),
                                                CreateByName = document.Field<String>("CreateByName"),
                                                // CreateDate = document.Field<DateTime>("CreateDate"),
                                                DocumentType = (DocumentType)document.Field<byte>("DocumentType"),
                                                FormType = (FormType)document.Field<byte>("FormType"),
                                                Id = document.Field<Guid>("Id"),
                                                Name = document.Field<String>("Name"),
                                                OperationID = document.Field<Guid>("OperationID"),
                                                Remark = document.Field<String>("Remark"),
                                                State = UploadState.Successed,
                                                Type = (OperationType)document.Field<byte>("OperationType"),
                                                UpdateDate = document.Field<DateTime?>("UpdateDate"),
                                                DocumentTypeName = document.Field<String>("DocumentTypeName"),
                                                // UpdateBy = document.Field<Guid?>("UpdateBy"),
                                                CreateDate = document.Field<DateTime>("CreateDate"),
                                                FileSources = document.IsNull("FileSource") ? FileSource.FDocument : (FileSource)document.Field<Byte>("FileSource"),
                                                FormId = document.Field<Guid?>("FormId")
                                            }).ToList();
            return documents;
        }

        /// <summary>
        /// 获取文档分发历史列表
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        public List<DocumentInfo> GetDocumentDispatchHistoryList(Guid OperationID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetHistoryDispatchFileList]");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<DocumentInfo>();

            List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                            select new DocumentInfo
                                            {
                                                CreateBy = document.Field<Guid>("CreateByID"),
                                                CreateByName = document.Field<String>("CreateByName"),
                                                DocumentType = (DocumentType)document.Field<byte>("DocumentType"),
                                                Id = document.Field<Guid>("Id"),
                                                Name = document.Field<String>("Name"),
                                                OperationID = document.Field<Guid>("OperationID"),
                                                Type = (OperationType)document.Field<byte>("OperationType"),
                                                UpdateDate = document.Field<DateTime?>("UpdateDate"),
                                                DocumentTypeName = document.Field<String>("DocumentTypeName"),
                                                CreateDate = document.Field<DateTime>("CreateDate")
                                            }).ToList();
            return documents;
        }

        /// <summary>
        /// 获取分发文档
        /// </summary>
        /// <param name="FileLogsID">分发记录ID</param>
        /// <returns></returns>
        public List<DocumentInfo> GetDispatchFiles(Guid FileLogsID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchFiles]");
            db.AddInParameter(dbCommand, "@FileLogsID", DbType.Guid, FileLogsID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<DocumentInfo>();

            List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                            select new DocumentInfo
                                            {
                                                CreateBy = document.Field<Guid>("CreateByID"),
                                                CreateByName = document.Field<String>("CreateByName"),
                                                DocumentType = (DocumentType)document.Field<byte>("DocumentType"),
                                                Id = document.Field<Guid>("Id"),
                                                Name = document.Field<String>("Name"),
                                                OperationID = document.Field<Guid>("OperationID"),
                                                Type = (OperationType)document.Field<byte>("OperationType"),
                                                UpdateDate = ChangeTime(document.Field<DateTime?>("UpdateDate")),
                                                DocumentTypeName = document.Field<String>("DocumentTypeName"),
                                                CreateDate = ChangeTime(document.Field<DateTime>("CreateDate"))
                                            }).ToList();
            return documents;
        }

        private DateTime ChangeTime(DateTime orgdt)
        {
            DateTime dt = DateTime.ParseExact(orgdt.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
            return dt.ToLocalTime();
        }
        private DateTime? ChangeTime(DateTime? orgdt)
        {
            if (orgdt != null)
            {
                DateTime dt = DateTime.ParseExact(((DateTime)orgdt).ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                return dt.ToLocalTime();
            }
            else
                return orgdt;
        }


        public List<ContentInfo> GetDocumentCopyContents(List<Guid> ids)
        {
            return InnerGetContent(ids.ToArray(), true);
        }

        public void Dispatch(DispatchStateParam param)
        {
            if (param == null) return;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspUpdateDispatchStates");
                db.AddInParameter(dbCommand, "@IDs", DbType.String, param.DocumentIds.ToArray().Join());
                db.AddInParameter(dbCommand, "@States", DbType.Byte, param.States.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.ExecuteNonQuery(dbCommand);

                FireEvent(methodName, ClientId, NotifyType.Dispatched, param.DocumentIds.ToArray(), param.UpdateDates.ToArray());

            }
            catch (Exception ex)
            {
                FireEvent(methodName, ClientId, NotifyType.Error, param.DocumentIds.ToArray(), this.IsEnglish ? "Dispatch Documents failure." : "分发文档失败.");
            }
        }

        public bool Accepted(AgentDispatchParam param)
        {
            bool isSuccess = false;
            if (param == null) isSuccess = false;
            try
            {
                int isAccepted = (param.DocumentState == DocumentState.Accepted ? 1 : 2);

                Database db = DatabaseFactory.CreateDatabase();
             //   DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanAgentDispatchInfo4Accept");
                DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspSaveOceanAgentDispatchInfo4Accept");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, param.OceanAgentDispatchId);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, param.LoginId);

                db.AddInParameter(dbCommand, "@IsAccept", DbType.Int32, isAccepted);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.ExecuteNonQuery(dbCommand);

                isSuccess = true;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }

        public bool AssignTo(AgentDispatchParam param)
        {
            bool isSuccess = false;
            if (param == null) isSuccess = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanAgentDispatchInfo4Assign");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, param.OceanAgentDispatchId);
                db.AddInParameter(dbCommand, "@AssignTo", DbType.Guid, param.AssignTo);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, param.LoginId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.ExecuteNonQuery(dbCommand);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public AgentDispatchInfo GetAgentDispatchInfo(Guid oceanAgentDispatchID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanAgentDispatchInfo4OI");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, oceanAgentDispatchID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet set = db.ExecuteDataSet(dbCommand);
                if (set == null || set.Tables.Count < 1)
                    return new AgentDispatchInfo();
                AgentDispatchInfo info = (from d in set.Tables[0].AsEnumerable()
                                          select new AgentDispatchInfo
                                                {
                                                    AcceptByName = d.Field<string>("AcceptByName"),
                                                    AcceptOn = d.IsNull("AcceptOn") ? (DateTime?)null : d.Field<DateTime?>("AcceptOn"),
                                                    DispatchByName = d.Field<string>("DispatchByName"),
                                                    DispatchOn = d.IsNull("DispatchOn") ? (DateTime?)null : d.Field<DateTime?>("DispatchOn"),
                                                    AssignToName = d.Field<string>("AssignToName")
                                                }).SingleOrDefault();

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 验证当前业务下是否包含传入的文件名集合
        /// </summary>
        /// <param name="FileNames"></param>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public List<DocumentInfo> IsExistFileNames(List<string> fileNames, Guid operationId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileListForNames");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@Names", DbType.String, fileNames.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet set = db.ExecuteDataSet(dbCommand);
                if (set == null || set.Tables.Count < 1)
                    return new List<DocumentInfo>();
                List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                                select new DocumentInfo
                                                {
                                                    Id = document.Field<Guid>("Id"),
                                                    Name = document.Field<String>("Name"),
                                                    DocumentType = DocumentType.Other,
                                                    UpdateDate = document.Field<DateTime?>("UpdateDate")
                                                }).ToList();
                return documents;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
            /// <summary>
        /// 获取指定业务号下的同属于所指定文档类型的所有文档
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="documentType">文档类型</param>
        /// <returns></returns>
        public List<ContentInfo> GetDocumentListByDocumentType(Guid operationId, DocumentType documentType)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileInfoByDocumentType");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
            db.AddInParameter(dbCommand, "@DocumentType", DbType.Int32, documentType.GetHashCode());
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<ContentInfo>();
            List<ContentInfo> contents = (from c in set.Tables[0].AsEnumerable()
                                          select new ContentInfo
                                          {
                                              Content = c.Field<byte[]>("Body"),
                                              Id = c.Field<Guid>("Id"),
                                              Name = c.Field<String>("Name")
                                          }).ToList();
            return contents;
        }
        /// <summary>
        /// 得到上传文件类型列表
        /// </summary>
        /// <param name="operateType">业务类型</param>
        /// <returns></returns>
        public Dictionary<string, UploadColumnType> GetUploadColumnType(int operateType)
        {
            Dictionary<string, UploadColumnType> dict = new Dictionary<string, UploadColumnType>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand("select  DocColumnName,DocumentType,OperateType from fcm.UploadColumnType where  OperateType=@OperateType ");
            db.AddInParameter(dbCommand, "@OperateType", DbType.Int32, operateType);
            DbDataReader reader = null;
            try
            {
                reader = db.ExecuteReader(dbCommand) as DbDataReader;

                while (reader.Read())
                {
                    UploadColumnType uct = new UploadColumnType();
                    uct.DocColumnName = reader.GetString(0);
                    uct.DocumentType = reader.GetInt32(1);
                    uct.OperateType = reader.GetInt32(2);
                    dict.Add(uct.DocColumnName, uct);
                }
                return dict;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return new Dictionary<string, UploadColumnType>();
        }
    }

    public sealed class FileSaveEntity
    {
        public DocumentInfo[] Documents { get; set; }
        public ApplicationContext Context { get; set; }

        public FileSaveEntity(DocumentInfo[] documents, ApplicationContext context)
        {
            this.Documents = documents;
            this.Context = context;

        }
    }
}

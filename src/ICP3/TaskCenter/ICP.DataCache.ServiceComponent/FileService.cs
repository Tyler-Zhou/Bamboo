using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Helper;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading;
using ICP.SubscriptionPublish.ServiceInterface;
using Microsoft.Practices.CompositeUI.Utility;
namespace ICP.DataCache.ServiceComponent1
{
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
            Guid[] ids = (from d in documents
                          select d.Id).ToArray<Guid>();
            Guid[] operationIds = (from d in documents
                                   select d.OperationID).ToArray<Guid>();
            int[] formTypes = (from d in documents
                               select d.FormType.GetHashCode()).ToArray<int>();
            int[] documentTypes = (from d in documents
                                   select d.DocumentType.GetHashCode()).ToArray<int>();

            // joe 2013-05-27 添加，
            // 原因：为解决各种业务使用同样的上传功能
            if (documentTypes.Length<1)
            {
                documentTypes=(from d in documents select d.DocumentTypeValue).ToArray<int>();
            }
            int[] fileSources = (from d in documents
                                 select d.FileSources.GetHashCode()).ToArray<int>();
            String[] names = (from d in documents
                              select d.Name).ToArray<String>();

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
            bool isSaveFailed = false;
            string errorMessage = string.Empty;
            ManyResult results = null;
            List<Guid> documentIds = new List<Guid>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOperationFileInfo");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.ToArray().Join());
            db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIds.Join());
            db.AddInParameter(dbCommand, "@FormTypes", DbType.String, formTypes.Join());
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

            try
            {
                results = db.ManyResult(dbCommand, new String[] { "ID", "UpdateDate" });

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
                                              Name = c.Field<String>("Name")
                                          }).ToList();
            return contents;
        }

        public ListDictionary<Guid, string> GetBusinessDocumentsName(BusinessOperationContext context)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileList");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, context.OperationID);
            db.AddInParameter(dbCommand, "@OnlyDispatched", DbType.Boolean, context.State == DocumentState.Dispatched ? true : false);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new  ListDictionary<Guid, string>();

            List<DocumentInfo> documents = (from document in set.Tables[0].AsEnumerable()
                                            select new DocumentInfo
                                            {
                                                Id = document.Field<Guid>("Id"),
                                                Name = document.Field<String>("Name")
                                            }).ToList();

            ListDictionary<Guid, string> listdictionary = new ListDictionary<Guid, string>();
            foreach (var item in documents)
            {
                listdictionary.Add(item.Id,item.Name);
            }

            return listdictionary;

        }

        public void Delete(List<Guid> ids, List<DateTime?> updateDates)
        {
            if (ids == null || ids.Count <= 0)
                return;

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationFileInfo");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.ToArray().Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, this.UserId);
            db.ExecuteNonQuery(dbCommand);
        }

        public void Delete(List<string> fileNames, Guid operationID)
        {
            if (fileNames == null || fileNames.Count <= 0) { return; }
            if (operationID == null || operationID == Guid.Empty) { return; }

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationFile");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
            db.AddInParameter(dbCommand, "@Names", DbType.String, fileNames.ToArray().Join());
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            db.ExecuteNonQuery(dbCommand);
        }


        public List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFileList");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, context.OperationID);
            db.AddInParameter(dbCommand, "@OnlyDispatched", DbType.Boolean, context.State == DocumentState.Dispatched ? true : false);
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
                                                DocumentType = (ICP.DataCache.ServiceInterface1.DocumentType)document.Field<byte>("DocumentType"),
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
                                                FileSources = document.IsNull("FileSource") ? FileSource.FDocument : (FileSource)document.Field<Byte>("FileSource")

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
                                                DocumentType = (ICP.DataCache.ServiceInterface1.DocumentType)document.Field<byte>("DocumentType"),
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanAgentDispatchInfo4Accept");
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
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
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

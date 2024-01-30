using System;
using System.Collections.Generic;
using System.Linq;
using ICP.DataCache.ServiceInterface1;
using System.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.DataOperation.SqlCE1;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using System.Text;
using System.IO;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface.DataObjects;
namespace ICP.DataCache.LocalOperation1
{
    /// <summary>
    /// 业务缓存数据操作实现类
    /// </summary>
    public class LocalBusinessCacheDataOperation : ILocalBusinessCacheDataOperation
    {
        [ServiceDependency]
        public IDataOperation CacheOperation { get; set; }



        public List<ContentInfo> GetDocumentCopyContent(List<Guid> documentIds)
        {
            List<ContentInfo> copyContents = new List<ContentInfo>();
            foreach (Guid documentId in documentIds)
            {
                ContentInfo copyContent = GetDocumentHtmlContent(documentId);
                if (copyContent == null)
                    continue;
                copyContents.Add(copyContent);
            }
            return copyContents;
        }
        public DocumentInfo GetDocumentDetailInfo(Guid id)
        {
            string queryString = "select opFile.*,fs.* from OperationFiles opFile left outer join [fcm.FileStorage] fs on opFile.Id=fs.Id where opFile.Id=@Id";
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter parameterId = CacheOperation.GetParameter("@Id", id);
            parameters.Add(parameterId);
            DataTable dt = CacheOperation.Get(queryString, parameters);
            DocumentInfo document = DataCacheUtility.ConvertTableToDocumentInfoList(dt)[0];
            document.CreateByName = LocalData.UserInfo.UserName;
            document.Content = dt.Rows[0].Field<byte[]>("Content");
            if (dt.Rows[0]["HtmlContent"] != DBNull.Value)
            {
                document.HtmlContent = dt.Rows[0].Field<byte[]>("HtmlContent");
            }
            return document;
        }

        public ContentInfo GetDocumentHtmlContent(Guid id)
        {
            return InnerGetDocumentContent(id, true);
        }

        private ContentInfo InnerGetDocumentContent(Guid id, bool isHtmlCopy)
        {
            string contentColumnName = GetContentColumnName(isHtmlCopy);
            String queryString = string.Format("select * from [fcm.FileStorage] where Id=@Id", contentColumnName);

            DbParameter parameterId = CacheOperation.GetParameter();
            parameterId.ParameterName = "@Id";
            parameterId.Value = id;
            parameterId.DbType = DbType.Guid;

            DataTable dt = CacheOperation.Get(queryString, parameterId);

            if (dt == null || dt.Rows.Count <= 0 || dt.Rows[0][contentColumnName] == DBNull.Value)
                return null;
            ContentInfo info = new ContentInfo();
            info.Id = id;
            info.UploadState = (UploadState)dt.Rows[0].Field<byte>("UploadState");
            info.Content = dt.Rows[0].Field<Byte[]>(contentColumnName);
            string orginalFileName = dt.Rows[0].Field<String>("Name");
            info.Name = GetFileName(orginalFileName, isHtmlCopy);
            return info;
        }
        private string GetFileName(string orginalFileName, bool isHtmlCopy)
        {
            if (isHtmlCopy)
            {
                return Path.ChangeExtension(orginalFileName, ".pdf");
            }
            else
                return orginalFileName;
        }

        public ContentInfo GetDocumentContent(Guid id)
        {
            return InnerGetDocumentContent(id, false);
        }
        public String GetDocumentName(Guid id)
        {
            String queryString = "select Name from [fcm.FileStorage] where Id=@Id";

            DbParameter parameterId = CacheOperation.GetParameter();
            parameterId.ParameterName = "@Id";
            parameterId.Value = id;
            parameterId.DbType = DbType.Guid;
            DataTable dt = CacheOperation.Get(queryString, parameterId);

            if (dt == null || dt.Rows.Count <= 0)
                return null;
            String name = dt.Rows[0].Field<String>("Name");
            return name;
        }

        public void SaveDocumentList(List<DocumentInfo> documents)
        {
            if (documents == null || documents.Count <= 0)
                return;

            Dictionary<String, String> dic = new Dictionary<String, String>();
            String tableName = "[fcm.FileStorage]";
            foreach (var item in documents)
            {

                dic.Clear();
                dic = DataCacheUtility.GetTypePropertyNameAndValue(item);
                CacheOperation.Insert(tableName, dic);
            }
        }
        public void SaveHtmlDocument(ContentInfo info)
        {
            InnerSaveContent(info, true);
        }
        public bool IsDocumentExists(Guid documentId)
        {
            String selectString = "select Id from [fcm.FileStorage] where Id=@Id";
            DbParameter parameterId = CacheOperation.GetParameter("@Id", documentId);
            string result = CacheOperation.ExecuteScalar(selectString, new List<DbParameter> { parameterId });
            if (string.IsNullOrEmpty(result))
                return false;
            else
                return true;

        }
        public void SaveDocumentContent(ContentInfo info)
        {
            InnerSaveContent(info, false);
        }
        private void InnerSaveContent(ContentInfo info, bool isHtmlCopy)
        {
            if (!CheckContent(info))
                return;
            string sqlString;
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter parameterId = CacheOperation.GetParameter("@Id", info.Id);
            parameters.Add(parameterId);
            DbParameter parameterContent = CacheOperation.GetParameter("@Content", info.Content);
            //parameterContent.DbType = DbType.Binary;
            parameters.Add(parameterContent);
            DbParameter parameterName = CacheOperation.GetParameter("@Name", info.Name);
            parameters.Add(parameterName);
            DbParameter parameterCreateData = CacheOperation.GetParameter("@CreateDate", DateTime.Now);
            parameters.Add(parameterCreateData);
            DbParameter parameterUploadState = CacheOperation.GetParameter("@UploadState", info.UploadState);
            parameters.Add(parameterUploadState);

            string contentColumnName = GetContentColumnName(isHtmlCopy);
            if (IsDocumentExists(info.Id))
            {
                sqlString = string.Format("update [fcm.FileStorage] set {0}=@Content where Id=@Id", contentColumnName);

            }
            else
            {
                sqlString = string.Format("insert into [fcm.FileStorage](Id,{0},Name,CreateDate,UploadState) values(@Id,@Content,@Name,@CreateDate,@UploadState)", contentColumnName);
            }
            CacheOperation.ExecuteNonQuery(sqlString, parameters);
        }
        private string GetContentColumnName(bool isHtmlCopy)
        {
            return isHtmlCopy ? "HtmlContent" : "Content";
        }
        private bool CheckContent(ContentInfo info)
        {

            if (info == null)
                return false;
            if (info.Content == null)
                return false;
            return true;
        }

        public void DeleteDocument(Guid id)
        {

            String deleteString = "delete from [fcm.FileStorage] where Id=@Id";
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter parameterId = CacheOperation.GetParameter("@Id", id);
            parameters.Add(parameterId);
            DbTransaction transaction = null;
            try
            {
                transaction = CacheOperation.BeginTransaction();
                CacheOperation.ExecuteNonQuery(deleteString, parameters);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        public void DeleteDocument(List<Guid> ids)
        {
            foreach (Guid id in ids)
            {
                DeleteDocument(id);
            }


        }

        public void UpdateDocumentRelation(DocumentInfo[] documents, ManyResult results)
        {
            DbTransaction transaction = null;
            try
            {
                transaction = CacheOperation.BeginTransaction();
                for (int i = 0; i < documents.Length; i++)
                {

                    String updateString = "Update [fcm.FileStorage] set Id=@newId,UploadState=@UploadState where Id=@oldId";
                    List<DbParameter> parameters = GenerateUpdateIdParameters(documents[i], results.Items[i]);
                    CacheOperation.ExecuteNonQuery(updateString, parameters);
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }
        private List<DbParameter> GenerateUpdateIdParameters(DocumentInfo document, SingleResult result)
        {
            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter parameter = CacheOperation.GetParameter("@oldId", document.Id);
            parameterList.Add(parameter);
            parameter = CacheOperation.GetParameter("@newId", result.GetValue<Guid>("Id"));
            parameterList.Add(parameter);
            parameter = CacheOperation.GetParameter("@UploadState", UploadState.Successed.GetHashCode());
            parameterList.Add(parameter);
            return parameterList;
        }

        public void ChangeDocumentUploadState(Guid[] ids, UploadState state)
        {
            DbTransaction transaction = CacheOperation.BeginTransaction();
            try
            {
                foreach (Guid id in ids)
                {
                    String updateString = "update [fcm.FileStorage] set UploadState=@UploadState where Id=@Id";
                    List<DbParameter> parameterList = new List<DbParameter>();
                    DbParameter parameter = CacheOperation.GetParameter("@Id", id);
                    parameterList.Add(parameter);
                    parameter = CacheOperation.GetParameter("@UploadState", state.GetHashCode());
                    parameterList.Add(parameter);
                    CacheOperation.ExecuteNonQuery(updateString, parameterList);
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new ICPException("ChangeDocumentUploadState", ex);
            }


        }


        public bool SaveDocument(DocumentInfo document)
        {


            DbTransaction transaction = null;
            try
            {
                transaction = CacheOperation.BeginTransaction();
                String insertString = "insert into [fcm.FileStorage](Id,HtmlContent,Content,CreateDate,UploadState,Name) values(@Id,@HtmlContent,@Content,@CreateDate,@UploadState,@Name);";
                List<DbParameter> parameters = GenerateContentParameter(document);
                CacheOperation.ExecuteNonQuery(insertString, parameters);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }



        }

        public void Save(List<DocumentInfo> newDocuments, List<Guid> deleteIds)
        {
            DbTransaction transaction = null;
            try
            {
                transaction = CacheOperation.BeginTransaction();
                DeleteDocument(deleteIds);
                SaveDocument(newDocuments.ToArray());
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        public bool SaveDocument(DocumentInfo[] documents)
        {
            foreach (DocumentInfo document in documents)
            {
                SaveDocument(document);
            }
            return true;
        }

        private List<DbParameter> GenerateContentParameter(DocumentInfo document)
        {
            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@HtmlContent";
            parameter.Value = DBNull.Value;
            if (document.HtmlContent != null)
                parameter.Value = document.HtmlContent;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Content";
            parameter.Value = DBNull.Value;
            if (document.Content != null)
                parameter.Value = document.Content;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = DBNull.Value;
            if (document.Content != null)
                parameter.Value = document.Name;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter("@Id", document.Id);
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter("@CreateDate", document.CreateDate);
            parameterList.Add(parameter);
            if (document.State == UploadState.LocalSaving)
                document.State = UploadState.LocalSaved;
            if (document.State == UploadState.Uploading)
            {
                document.State = UploadState.Successed;
            }
            parameter = CacheOperation.GetParameter("@UploadState", document.State.GetHashCode());
            parameterList.Add(parameter);


            return parameterList;
        }

        public int? GetContactPersonType(string emailAddress)
        {
            String queryString = "select Type from contactPersonInfo where EmailAddress=@EmailAddress";

            DbParameter parameterEmailAddress = CacheOperation.GetParameter();
            parameterEmailAddress.ParameterName = "@EmailAddress";
            parameterEmailAddress.Value = emailAddress;
            parameterEmailAddress.DbType = DbType.String;
            DataTable dt = CacheOperation.Get(queryString, parameterEmailAddress);

            if (dt == null || dt.Rows.Count <= 0)
                return null;
            int type = dt.Rows[0].Field<int>("Type");
            return type;

        }

        public void SaveContactPersonType(string emailAddress, int type)
        {

            try
            {
                string sql;
                if (IsContactPersonInfoExists(emailAddress))
                {
                    sql = "update contactPersonInfo set Type=@Type where EmailAddress=@EmailAddress";
                }
                else
                {
                    sql = "insert into contactPersonInfo(EmailAddress,Type) values(@EmailAddress,@Type)";
                    
                }
                List<DbParameter> parameters = GenerateSaveContactParameters(emailAddress, type);
                CacheOperation.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<DbParameter> GenerateSaveContactParameters(string emailAddress, int type)
        {
            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@EmailAddress";
            parameter.DbType = DbType.String;
            parameter.Value = emailAddress;

            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Type";
            parameter.DbType = DbType.Int32;
            parameter.Value = type;
            parameterList.Add(parameter);
            return parameterList;
        }
        public UserCustomGridInfo GetCustomGridInfo(Guid userId, ListFormType listType)
        {
            DataTable dt = InnerGetCustomGridInfo(userId, listType);
            if (dt == null)
            {
                dt = InnerGetCustomGridInfo(null, listType);
            }
            return DataCacheUtility.ConvertTableToUserCustomGridInfo(dt);
        }

        public UserCustomGridInfo GetCustomGridInfo(Guid userId, string templateCode)
        {
            DataTable dt = InnerGetCustomGridInfo(userId, templateCode);
            if (dt == null)
            {
                dt = InnerGetCustomGridInfo(null, templateCode);
            }
            return DataCacheUtility.ConvertTableToUserCustomGridInfo(dt);
        }

        private DataTable InnerGetCustomGridInfo(Guid? userId, string templateCode)
        {
            String queryString;
            if (userId.HasValue)
            {
                queryString = "select Id,TemplateCode,UserId,ColumnData,UpdateDate from UserCustomColumns where UserId=@UserId and TemplateCode=@TemplateCode";
            }
            else
            {
                queryString = "select Id,TemplateCode,UserId,ColumnData,UpdateDate from UserCustomColumns where UserId is null and TemplateCode=@TemplateCode";
            }
            List<DbParameter> parameters = GetCustomGridQueryParameters(userId, templateCode);
            DataTable dt = CacheOperation.Get(queryString, parameters);

            if (dt == null || dt.Rows.Count <= 0)
                return null;
            return dt;

        }

        private DataTable InnerGetCustomGridInfo(Guid? userId, ListFormType listType)
        {
            String queryString;
            if (userId.HasValue)
            {
                queryString = "select Id,TemplateCode,UserId,ColumnData,UpdateDate from UserCustomColumns where UserId=@UserId and TemplateCode=@TemplateCode";
            }
            else
            {
                queryString = "select Id,TemplateCode,UserId,ColumnData,UpdateDate from UserCustomColumns where UserId is null and TemplateCode=@TemplateCode";
            }
            List<DbParameter> parameters = GetCustomGridQueryParameters(userId, listType);
            DataTable dt = CacheOperation.Get(queryString, parameters);

            if (dt == null || dt.Rows.Count <= 0)
                return null;
            return dt;

        }
        private List<DbParameter> GetCustomGridQueryParameters(Guid? userId, ListFormType listType)
        {
            List<DbParameter> listParameter = new List<DbParameter>();


           DbParameter  parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@TemplateCode";
            parameter.Value = listType.GetHashCode();
            parameter.DbType = DbType.Int32;
            listParameter.Add(parameter);

            if (userId.HasValue)
            {
                parameter = CacheOperation.GetParameter();
                parameter.ParameterName = "@UserId";
                parameter.Value = userId.Value;
                parameter.DbType = DbType.Guid;
          
                listParameter.Add(parameter);
            }
            return listParameter;

        }


        private List<DbParameter> GetCustomGridQueryParameters(Guid? userId, string templateCode)
        {
            List<DbParameter> listParameter = new List<DbParameter>();


            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@TemplateCode";
            parameter.Value = templateCode;
            parameter.DbType = DbType.String;
            listParameter.Add(parameter);

            if (userId.HasValue)
            {
                parameter = CacheOperation.GetParameter();
                parameter.ParameterName = "@UserId";
                parameter.Value = userId.Value;
                parameter.DbType = DbType.Guid;

                listParameter.Add(parameter);
            }
            return listParameter;

        }

        private bool IsCustomGridInfoExists(Guid id)
        {
            string queryString = "select Id from UserCustomColumns where Id=@Id";
            List<DbParameter> listParameter = new List<DbParameter>();
            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;
            parameter.DbType = DbType.Guid;

            listParameter.Add(parameter);

            string result = CacheOperation.ExecuteScalar(queryString, listParameter);
            if (string.IsNullOrEmpty(result))
                return false;
            return true;

        }

        private bool IsCustomGridInfoExists(Guid? userid,string templateCode)
        {
            string queryString = "select Id from UserCustomColumns where UserID=@UserID and TemplateCode=@TemplateCode ";
            List<DbParameter> listParameter = new List<DbParameter>();
            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UserID";
            parameter.Value = userid;
            parameter.DbType = DbType.Guid;

            listParameter.Add(parameter);

            DbParameter parameter2 = CacheOperation.GetParameter();
            parameter2.ParameterName = "@TemplateCode";
            parameter2.Value = templateCode;
            parameter2.DbType = DbType.String;

            listParameter.Add(parameter2);

            string result = CacheOperation.ExecuteScalar(queryString, listParameter);
            if (string.IsNullOrEmpty(result))
                return false;
            return true;

        }
        private bool IsContactPersonInfoExists(string senderAddress)
        {
            string queryString = "select count(*) from contactPersonInfo where EmailAddress=@EmailAddress";
            List<DbParameter> listParameter = new List<DbParameter>();
            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@EmailAddress";
            parameter.Value = senderAddress;
            parameter.DbType = DbType.String;

            listParameter.Add(parameter);

            string result = CacheOperation.ExecuteScalar(queryString, listParameter);
            if (string.IsNullOrEmpty(result))
                return false;
            return true;
        }

        /// <summary>
        /// 保存用户自定义列表信息
        /// </summary>
        /// <param name="customInfo"></param>
        public void SaveCustomGridInfo(UserCustomGridInfo customInfo)
        {
            try
            {
                String queryString;
                if (!IsCustomGridInfoExists(customInfo.Id))
                {
                    if (customInfo.UserId.HasValue)
                    {
                        queryString = "insert into UserCustomColumns(Id,TemplateCode,UserId,ColumnData,UpdateDate) values(@Id,@TemplateCode,@UserId,@ColumnData,@UpdateDate)";
                    }
                    else
                    {
                        queryString = "insert into UserCustomColumns(Id,TemplateCode,ColumnData,UpdateDate) values(@Id,@TemplateCode,@ColumnData,@UpdateDate)";
                    }

                }
                else
                {
                   
                    if (customInfo.UserId.HasValue)
                    {
                        queryString = "update userCustomColumns set UserId=@UserId,ColumnData=@ColumnData,UpdateDate=@UpdateDate where Id=@Id";
                    }
                    else
                    {
                        queryString = "update userCustomColumns set ColumnData=@ColumnData,UpdateDate=@UpdateDate where Id=@Id";
                    }
                }
                List<DbParameter> parameters = GenerateSaveCustomGridInfoParameters(customInfo);
                CacheOperation.ExecuteNonQuery(queryString, parameters);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<DbParameter> GenerateSaveCustomGridInfoParameters(UserCustomGridInfo customInfo)
        {
            List<DbParameter> listParameter = new List<DbParameter>();


            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = customInfo.Id;
            parameter.DbType = DbType.Guid;
            listParameter.Add(parameter);

            //parameter = CacheOperation.GetParameter();
            //parameter.ParameterName = "@UpdateBy";
            //parameter.Value = customInfo.UserId;
            //parameter.DbType = DbType.Guid;
            //listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateDate";
            parameter.Value = DBNull.Value;
            if (customInfo.UpdateDate.HasValue)
            {
                parameter.Value = customInfo.UpdateDate;
            }
            parameter.DbType = DbType.DateTime;
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@ColumnData";
            parameter.Value = SerializerHelper.SerializeToString<UserCustomGridInfo>(customInfo);
           // parameter.DbType =DbType.Object;
            listParameter.Add(parameter);

            listParameter.AddRange(GetCustomGridQueryParameters(customInfo.UserId, customInfo.TemplateCode));
            return listParameter;

        }




    }
}

using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.DataOperation.SqlCE;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.MailCenterFramework.ServiceInterface;
using ICP.MailCenterFramework.UI;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.TaskCenter.ServiceInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Logger = ICP.Framework.CommonLibrary.Logger;
using ICP.FileSystem.ServiceInterface;

namespace ICP.DataCache.LocalOperation
{
    /// <summary>
    /// 业务缓存数据操作实现类
    /// </summary>
    public class LocalBusinessCacheDataOperation : ILocalBusinessCacheDataOperation
    {
        #region Member & Variables
        public const string Table_OperationViewOECache = "OperationViewOECache";    //业务数据
        public const string Table_OperationContactCache = "OperationContactCache";  //操作联系人
        public const string Table_OperationMessages = "OperationMessages";          //关联数据
        public const string Table_UserOperationLog = "UserOperationLog";            //用户操作日志表名
        public const string Table_Languages = "Languages";                          //语言
        public const string Table_FileStorage = "fcm.FileStorage";                  //文档缓存
        public const string Table_UserCustomColumns = "UserCustomColumns";          //用户自定列
        public const string DBSeparator = ";";                                      //分隔符
        public const string keyWordSearch = "KeyWord";                              //查询关键字
        public const string autoSearch = "Auto";                                    //自动查询

        /// <summary>
        /// wcf服务
        /// </summary>
        public IFileSystemService FileServiceWCF
        {
            get
            {
                return ServiceClient.GetService<IFileSystemService>();
            }
        }
        /// <summary>
        /// 是否正在备份邮件
        /// </summary>
        static bool _IsUploadMail = false;
        /// <summary>
        /// 是否正在上传操作日志
        /// </summary>
        static bool _IsUploadUserOperationLog = false;
        /// <summary>
        /// 是否正在上传关联信息
        /// </summary>
        static bool _IsUploadMessageRelation = false;
        /// <summary>
        /// 是否读取本地关联信息
        /// </summary>
        static bool _IsReadFileOperationMssage = false;
        /// <summary>
        /// 是否读取本地业务关联信息
        /// </summary>
        static bool _IsReadFileOperationContact = false;
        /// <summary>
        /// 批量上传关联信息最大数
        /// </summary>
        public const int MaxCount = 25;

        /// <summary>
        /// 用户默认所属的公司Id
        /// </summary>
        public Guid DefaultCompanyId
        {
            get { return ApplicationContext.Current.DefaultCompanyId; }
        }

        /// <summary>
        /// 是否英文
        /// </summary>
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }

        /// <summary>
        /// 登录用户GUID
        /// </summary>
        public Guid DefaultUserID
        {
            get { return ApplicationContext.Current.UserId; }

        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        private string AssamblyName
        {
            get
            {
                MethodBase methodother = MethodBase.GetCurrentMethod();
                if (methodother.DeclaringType != null)
                    return methodother.DeclaringType.FullName;
                return "ICP.DataCache.LocalOperation";
            }
        }

        #region Services

        private IDataOperation _CacheOperation;
        /// <summary>
        /// 缓存文件操作类
        /// </summary>
        public IDataOperation CacheOperation
        {
            get { return new SqlCEOperation(); }
        }

        /// <summary>
        /// Outlook Service
        /// </summary>
        public IOutlookOperateService OutlookService
        {
            get
            {
                return ServiceClient.GetService<IOutlookOperateService>();
            }
        }

        /// <summary>
        /// Outlook Entity
        /// </summary>
        public OutlookOperateService OutlookEntity
        {
            get
            {
                return new OutlookOperateService();
            }
        }


        /// <summary>
        /// 客户端文件服务
        /// </summary>
        public IFileService FileService
        {
            get { return ServiceClient.GetService<IFileService>(); }
        }

        /// <summary>
        /// 任务中心视图服务：获取当前用户的协助用户
        /// </summary>
        public IOperationViewService OperationViewService
        {
            get { return ServiceClient.GetService<IOperationViewService>(); }
        }


        /// <summary>
        /// 关联服务操作服务
        /// </summary>
        public IOperationMessageRelationService OperationMessageRelationService
        {
            get { return ServiceClient.GetService<IOperationMessageRelationService>(); }
        }

        /// <summary>
        /// 操作日志服务
        /// </summary>
        public IOperationLogService OperationLogService
        {
            get { return ServiceClient.GetService<IOperationLogService>(); }
        }

        public IMessageService MessageService
        {
            get { return ServiceClient.GetService<IMessageService>(); }
        }

        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }

        #endregion

        #endregion

        #region Document

        #region fcm.OperationFiles

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DocumentInfo GetDocumentDetailInfo(Guid id)
        {
            string queryString =
                string.Format("select opFile.*,fs.* from [fcm.OperationFiles] opFile left outer join [{0}] fs on opFile.Id=fs.Id WHERE opFile.Id=@Id", Table_FileStorage);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperationId"></param>
        /// <returns></returns>
        public List<DocumentInfo> GetDocumentListInfo(Guid OperationId)
        {
            List<DocumentInfo> docList = new List<DocumentInfo>();
            String queryString = "SELECT ID FROM [fcm.OperationFiles] WHERE [OperationId] = @OperationId";

            DbParameter parameterId = CacheOperation.GetParameter();
            parameterId.ParameterName = "@OperationId";
            parameterId.Value = OperationId;
            parameterId.DbType = DbType.Guid;
            DataTable dt = CacheOperation.Get(queryString, parameterId);
            if (dt == null || dt.Rows.Count <= 0)
                return docList;
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                Guid fileid = dt.Rows[0].Field<Guid>("Id");
                docList.Add(GetDocumentDetailInfo(fileid));
            }
            return docList;

            //DataTable dt = CacheOperation.Get(queryString, parameterId);

            //if (dt == null || dt.Rows.Count <= 0 || dt.Rows[0][contentColumnName] == DBNull.Value)
            //    return null;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentIds"></param>
        /// <returns></returns>
        public List<ContentInfo> GetDocumentCopyContent(List<Guid> documentIds)
        {
            return documentIds.Select(GetDocumentHtmlContent).Where(copyContent => copyContent != null).ToList();
        }

        /// <summary>
        /// 获取文档信息：文档内容、名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContentInfo GetDocumentHtmlContent(Guid id)
        {
            return InnerGetDocumentContent(id, true);
        }

        /// <summary>
        /// 获取文档信息：文档内容、名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isHtmlCopy"></param>
        /// <returns></returns>
        private ContentInfo InnerGetDocumentContent(Guid id, bool isHtmlCopy)
        {
            ContentInfo info = null;
            try
            {
                string contentColumnName = GetContentColumnName(isHtmlCopy);
                String queryString = string.Format("SELECT * FROM [{0}] WHERE [Id]=@Id", Table_FileStorage);

                DbParameter parameterId = CacheOperation.GetParameter();
                parameterId.ParameterName = "@Id";
                parameterId.Value = id;
                parameterId.DbType = DbType.Guid;

                DataTable dt = CacheOperation.Get(queryString, parameterId);

                if (dt == null || dt.Rows.Count <= 0 || dt.Rows[0][contentColumnName] == DBNull.Value)
                    return null;
                info = new ContentInfo();
                info.Id = id;
                info.UploadState = (UploadState)dt.Rows[0].Field<byte>("UploadState");
                info.Content = dt.Rows[0].Field<Byte[]>(contentColumnName);
                string orginalFileName = dt.Rows[0].Field<String>("Name");
                info.Name = GetFileName(orginalFileName, isHtmlCopy);
            }
            catch (Exception ex)
            {
                info = null;
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return info;
        }

        /// <summary>
        /// 获取文件名称：含拓展名称
        /// </summary>
        /// <param name="orginalFileName"></param>
        /// <param name="isHtmlCopy"></param>
        /// <returns></returns>
        private string GetFileName(string orginalFileName, bool isHtmlCopy)
        {
            if (isHtmlCopy)
            {
                return Path.ChangeExtension(orginalFileName, ".pdf");
            }
            else
                return orginalFileName;
        }

        /// <summary>
        /// 获取文档信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContentInfo GetDocumentContent(Guid id)
        {
            return InnerGetDocumentContent(id, false);
        }

        /// <summary>
        /// 获取文档名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public String GetDocumentName(Guid id)
        {
            String name = string.Empty;

            try
            {
                String queryString = string.Format("SELECT NAME FROM [{0}] WHERE [Id]=@Id", Table_FileStorage);

                DbParameter parameterId = CacheOperation.GetParameter();
                parameterId.ParameterName = "@Id";
                parameterId.Value = id;
                parameterId.DbType = DbType.Guid;
                DataTable dt = CacheOperation.Get(queryString, parameterId);

                if (dt == null || dt.Rows.Count <= 0)
                    return null;
                name = dt.Rows[0].Field<String>("Name");
            }
            catch (Exception ex)
            {
                name = string.Empty;
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return name;
        }

        /// <summary>
        /// 保存文档列表
        /// </summary>
        /// <param name="documents"></param>
        public void SaveDocumentList(List<DocumentInfo> documents)
        {
            try
            {
                if (documents == null || documents.Count <= 0)
                    return;

                Dictionary<String, String> dic = new Dictionary<String, String>();
                foreach (var item in documents)
                {
                    dic.Clear();
                    dic = DataCacheUtility.GetTypePropertyNameAndValue(item);
                    CacheOperation.Insert(Table_FileStorage, dic);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        public void SaveHtmlDocument(ContentInfo info)
        {
            InnerSaveContent(info, true);
        }

        public bool IsDocumentExists(Guid documentId)
        {
            bool returnValue = false;
            try
            {
                String selectString = string.Format(" SELECT ID FROM [{0}] WHERE [Id]=@Id", Table_FileStorage);
                DbParameter parameterId = CacheOperation.GetParameter("@Id", documentId);
                string result = CacheOperation.ExecuteScalar(selectString, new List<DbParameter> { parameterId });
                returnValue = !string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return returnValue;
        }

        public void SaveDocumentContent(ContentInfo info)
        {
            InnerSaveContent(info, false);
        }

        private void InnerSaveContent(ContentInfo info, bool isHtmlCopy)
        {
            try
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
                //业务操作ID
                DbParameter parameterOperationID = CacheOperation.GetParameter("@OperationID", info.OperationID);
                parameters.Add(parameterOperationID);
                //单证类型
                DbParameter parameterTypeCode = CacheOperation.GetParameter("@TypeCode", info.TypeCode.HasValue ? info.TypeCode : 0);
                parameters.Add(parameterTypeCode);

                string contentColumnName = GetContentColumnName(isHtmlCopy);
                if (IsDocumentExists(info.Id))
                {
                    sqlString = string.Format("UPDATE [{0}] SET {1}=@Content WHERE [Id] = @Id", Table_FileStorage, contentColumnName);

                }
                else
                {
                    sqlString =
                        string.Format(
                            "INSERT INTO [{0}]([Id],[{1}],[Name],[CreateDate],[UploadState],[OperationID],[TypeCode]) values(@Id,@Content,@Name,@CreateDate,@UploadState,@OperationID,@TypeCode)",
                            Table_FileStorage, contentColumnName);
                }
                CacheOperation.ExecuteNonQuery(sqlString, parameters);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
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
            bool isBeginTransaction = false;
            try
            {
                isBeginTransaction = (_CacheOperation != null);
                if (_CacheOperation == null)
                    _CacheOperation = new SqlCEOperation();
                String deleteString = string.Format("DELETE FROM [{0}] WHERE [Id]=@Id", Table_FileStorage);
                List<DbParameter> parameters = new List<DbParameter>();
                DbParameter parameterId = _CacheOperation.GetParameter("@Id", id);
                parameters.Add(parameterId);
                _CacheOperation.ExecuteNonQuery(deleteString, parameters);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                if (!isBeginTransaction)
                    _CacheOperation = null;
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
            try
            {
                _CacheOperation = new SqlCEOperation();
                _CacheOperation.BeginTransaction();
                int length = documents.Length;
                for (int i = 0; i < length; i++)
                {
                    String updateString =
                        string.Format("UPDATE [{0}] SET [Id] = @newId,[UploadState] = @UploadState WHERE [Id]=@oldId ", Table_FileStorage);
                    List<DbParameter> parameters = GenerateUpdateIdParameters(documents[i], results.Items[i]);
                    _CacheOperation.ExecuteNonQuery(updateString, parameters);
                }
                _CacheOperation.CommitTransaction();
            }
            catch (Exception ex)
            {
                _CacheOperation.RollBackTransaction();
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                if (_CacheOperation != null)
                {
                    _CacheOperation.EndTransaction();
                }
                _CacheOperation = null;
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
            try
            {
                _CacheOperation = new SqlCEOperation();
                _CacheOperation.BeginTransaction();
                foreach (Guid id in ids)
                {
                    String updateString = string.Format("UPDATE [{0}] SET [UploadState]= @UploadState WHERE [Id]=@Id",
                        Table_FileStorage);
                    List<DbParameter> parameterList = new List<DbParameter>();
                    DbParameter parameter = CacheOperation.GetParameter("@Id", id);
                    parameterList.Add(parameter);
                    parameter = CacheOperation.GetParameter("@UploadState", state.GetHashCode());
                    parameterList.Add(parameter);

                    _CacheOperation.ExecuteNonQuery(updateString, parameterList);
                }
                _CacheOperation.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (_CacheOperation != null) _CacheOperation.RollBackTransaction();
            }
            finally
            {
                if (_CacheOperation != null)
                {
                    _CacheOperation.EndTransaction();
                }
                _CacheOperation = null;
            }
        }

        public bool SaveDocument(DocumentInfo document)
        {
            bool isBeginTransaction = false;
            try
            {
                DeleteDocument(document.Id);
                isBeginTransaction = (_CacheOperation != null);
                if (_CacheOperation == null)
                    _CacheOperation = new SqlCEOperation();
                String insertString =
                    string.Format("INSERT INTO [{0}](Id,HtmlContent,Content,CreateDate,UploadState,Name) VALUES(@Id,@HtmlContent,@Content,@CreateDate,@UploadState,@Name);", Table_FileStorage);
                List<DbParameter> parameters = GenerateContentParameter(document);
                _CacheOperation.ExecuteNonQuery(insertString, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                if (!isBeginTransaction)
                    _CacheOperation = null;
            }
            return false;
        }

        private List<DbParameter> GenerateFileParameter(DocumentInfo document)
        {
            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter parameter = CacheOperation.GetParameter();

            parameter = CacheOperation.GetParameter("@OperationId", document.OperationID);
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter("@Id", document.Id);
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter("@CreateBy", document.CreateBy);
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Remark";
            parameter.Value = DBNull.Value;
            parameter.Value = "test";
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter("@FormType", document.FormType.GetHashCode());
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateBy";
            parameter.Value = DBNull.Value;
            if (document.UpdateBy != null)
                parameter.Value = document.UpdateBy;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = DBNull.Value;
            if (document.Name != null)
                parameter.Value = document.Name;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UploadState";
            parameter.Value = DBNull.Value;
            parameter.Value = document.State.GetHashCode();
            parameterList.Add(parameter);



            parameter = CacheOperation.GetParameter("@Type", document.Type.GetHashCode());
            parameterList.Add(parameter);


            parameter = CacheOperation.GetParameter("@DocumentType", document.DocumentType.GetHashCode());
            parameterList.Add(parameter);


            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateDate";
            parameter.Value = DBNull.Value;
            if (document.UpdateDate != null)
                parameter.Value = document.UpdateDate;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter("@CreateDate", document.CreateDate);
            parameterList.Add(parameter);

            return parameterList;
        }

        public void Save(List<DocumentInfo> newDocuments, List<Guid> deleteIds)
        {
            try
            {
                _CacheOperation = new SqlCEOperation();
                _CacheOperation.BeginTransaction();
                DeleteDocument(deleteIds);
                SaveDocument(newDocuments.ToArray());
                _CacheOperation.CommitTransaction();

            }
            catch (Exception ex)
            {
                _CacheOperation.RollBackTransaction();
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                if (_CacheOperation != null)
                {
                    _CacheOperation.EndTransaction();
                }
                _CacheOperation = null;
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
        #endregion

        #region Operation Contact

        /// <summary>
        /// 获取所有联系人
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllContact()
        {
            DataTable dtResult = null;
            try
            {
                dtResult = CacheOperation.Get(string.Format("SELECT oc.[ID],oc.[CustomerID],oc.[Mail],oc.[IsCustomer],oc.[IsCarrier],oc.[IsOE],oc.[IsOI],oc.[IsAE],oc.[IsAI],oc.[IsTrk],oc.[IsOther],oc.[UpdateDate] FROM [{0}] AS oc ",
                                Table_OperationContactCache));
            }
            catch (Exception ex)
            {
                dtResult = null;
            }
            return dtResult;
        }
        /// <summary>
        /// 通过邮件域名获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public int? GetContactPersonTypeByMailDomain(string emailAddress)
        {
            int? type = null;
            try
            {
                int index = emailAddress.LastIndexOf('@');
                string mailDomain = emailAddress.Substring(index);
                string queryString = "SELECT CASE WHEN IsCustomer=1 AND IsCarrier=1 THEN 3  WHEN IsCustomer=1 THEN 1  WHEN IsCarrier=1 THEN 2 else 0 END Type FROM OperationContactDomainCaches WHERE MailDomain=@MailDomain";
                DbParameter parameterMailDomain = CacheOperation.GetParameter();
                parameterMailDomain.ParameterName = "@MailDomain";
                parameterMailDomain.Value = mailDomain;
                parameterMailDomain.DbType = DbType.String;
                DataTable dt = CacheOperation.Get(queryString, parameterMailDomain);

                if (dt == null || dt.Rows.Count <= 0)
                    return null;
                type = dt.Rows[0].Field<int>("Type");

            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return type;
        }

        /// <summary>
        /// 通过邮件地址获取联系人类型：客户、承运人、客户AND承运人
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public int? GetContactPersonType(string emailAddress)
        {
            int? type = null;
            try
            {
                type = GetContactPersonTypeByMailDomain(emailAddress);
                if (!type.HasValue)
                {
                    String queryString =
                        string.Format(
                            "SELECT CASE WHEN IsCustomer=1 AND IsCarrier=1 THEN 3  WHEN IsCustomer=1 THEN 1  WHEN IsCarrier=1 THEN 2 else 0 END  Type,ID From {0} WHERE Mail=@EmailAddress",
                            Table_OperationContactCache);

                    DbParameter parameterEmailAddress = CacheOperation.GetParameter();
                    parameterEmailAddress.ParameterName = "@EmailAddress";
                    parameterEmailAddress.Value = emailAddress;
                    parameterEmailAddress.DbType = DbType.String;
                    DataTable dt = CacheOperation.Get(queryString, parameterEmailAddress);

                    if (dt == null || dt.Rows.Count <= 0)
                        return null;
                    type = dt.Rows[0].Field<int>("Type");
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return type;
        }

        /// <summary>
        /// 保存邮件地址标识类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="type"></param>
        public void SaveContactPersonType(string emailAddress, int type)
        {
            string temp = string.Empty;
            if (type == 1)
            {
                temp = "[IsCustomer] = 1 ";
            }
            else if (type == 2)
            {
                temp = "[IsCarrier] = 1 ";
            }
            else if (type == 3)
            {
                temp = "[IsCustomer] = 1,[IsCarrier] = 1";
            }
            try
            {
                string sql;
                bool isInsert = false;
                if (IsContactPersonInfoExists(emailAddress))
                {
                    sql = string.Format("UPDATE {0} SET {1} WHERE [Mail] = @EmailAddress",
                                        Table_OperationContactCache, temp);
                }
                else
                {
                    sql =
                        string.Format(
                            "INSERT INTO {0}(ID,Mail,CustomerID,IsCustomer,IsCarrier,IsOE,IsOI,IsAE,IsAI,IsTrk,IsOther,UpdateDate,F4) VALUES(@ID,@EmailAddress,NULL,@IsCustomer,@IsCarrier,1,0,0,0,0,0,getdate(),1)",
                            Table_OperationContactCache);
                    isInsert = true;
                }
                List<DbParameter> parameters = GenerateSaveContactParameters(emailAddress, type, isInsert);
                CacheOperation.ExecuteNonQuery(sql, parameters);

            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }
        /// <summary>
        /// 构建保存邮箱地址标识参数
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="type"></param>
        /// <param name="isInsert"></param>
        /// <returns></returns>
        private List<DbParameter> GenerateSaveContactParameters(string emailAddress, int type, bool isInsert)
        {
            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@EmailAddress";
            parameter.DbType = DbType.String;
            parameter.Value = emailAddress;

            parameterList.Add(parameter);

            if (isInsert)
            {
                parameter = CacheOperation.GetParameter();
                parameter.ParameterName = "@ID";
                parameter.DbType = DbType.Guid;
                parameter.Value = Guid.NewGuid();
                parameterList.Add(parameter);

                parameter = CacheOperation.GetParameter();
                parameter.ParameterName = "@IsCustomer";
                parameter.DbType = DbType.Boolean;
                parameter.Value = type & 1;
                parameterList.Add(parameter);

                parameter = CacheOperation.GetParameter();
                parameter.ParameterName = "@IsCarrier";
                parameter.DbType = DbType.Boolean;
                parameter.Value = (type & 2) >> 1;
                parameterList.Add(parameter);
            }
            return parameterList;
        }

        private bool DeleteContacts(List<string> mails)
        {
            bool returnValue = false;

            try
            {
                const string parmeterPrefix = "Mail";
                string inClauseString = GetInParameterString(mails == null ? 0 : mails.Count, parmeterPrefix);
                string queryString = string.Format("DELETE FROM {0} WHERE Mail IN ({1})", Table_OperationContactCache, inClauseString);
                List<DbParameter> parameterList = GetInParameters(mails, parmeterPrefix);
                CacheOperation.ExecuteNonQuery(queryString, parameterList);
                returnValue = true;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return returnValue;
        }

        public void SaveContacts(List<OperationContactInfo> contactList)
        {
            if (contactList == null || contactList.Count <= 0)
            {
                return;
            }
            string insertString = string.Format("INSERT INTO {0}(ID,Mail,IsCustomer,IsCarrier,IsOE,IsOI,IsAE,IsAI,IsTrk,IsOther,UpdateDate,CustomerID,F4) values(@ID,@Mail,@IsCustomer,@IsCarrier,@IsOE,@IsOI,@IsAE,@IsAI,@IsTrk,@IsOther,@UpdateDate,@CustomerID,@F4)", Table_OperationContactCache);
            List<string> mails = new List<string>();
            contactList.ForEach(item => mails.Add(item.Mail));
            try
            {
                _CacheOperation = new SqlCEOperation();
                bool deleted = DeleteContacts(mails);
                if (deleted)
                {
                    _CacheOperation.BeginTransaction();
                    foreach (OperationContactInfo contactInfo in contactList)
                    {
                        List<DbParameter> parameters = GetOperationContactAddParameters(contactInfo.ID, contactInfo.Mail, contactInfo.UpdateDate, contactInfo.Customer, contactInfo.Carrier, contactInfo.OE, contactInfo.OI, contactInfo.Trk, contactInfo.Other, contactInfo.AE, contactInfo.AI, contactInfo.CustomerID);
                        CacheOperation.ExecuteNonQuery(insertString, parameters);
                    }
                    _CacheOperation.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (_CacheOperation != null)
                {
                    _CacheOperation.RollBackTransaction();
                }
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                _CacheOperation = null;
            }
        }

        public bool IsAllContactExsitCache(List<string> ExternalEmails)
        {
            bool returnValue = false;
            if (ExternalEmails.Count <= 0) return false;
            string query = string.Empty;
            string sql = string.Empty;
            try
            {
                query = ExternalEmails.Aggregate(query, (current, item) => current + ("'" + item + "'" + ","));
                query = query.Substring(0, query.Length - 1);
                sql = string.Format("SELECT COUNT([Mail]) FROM {0} WHERE [Mail] IN({1})", Table_OperationContactCache, query);
                returnValue = Int32.Parse(CacheOperation.ExecuteScalar(sql)) == ExternalEmails.Count;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(sql);
            }
            return returnValue;
        }

        private List<DbParameter> GetOperationContactAddParameters(Guid contactId, string mail,
          DateTime? updateDate, bool? isCustomer, bool? isCarrier, bool? isOE, bool? isOI, bool? isTrk, bool? isOth,
           bool? isAE, bool? isAI, Guid? customerId)
        {
            List<DbParameter> parameterList = new List<DbParameter>();
            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@ID";
            parameter.DbType = DbType.Guid;
            parameter.Value = contactId;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Mail";
            parameter.Value = mail;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@IsCustomer";
            parameter.DbType = DbType.Boolean;
            parameter.Value = isCustomer;
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@IsCarrier";
            parameter.DbType = DbType.Boolean;
            parameter.Value = isCarrier;
            parameterList.Add(parameter);

            parameter = GetBooleanParameter("@IsOE", isOE);
            parameterList.Add(parameter);

            parameter = GetBooleanParameter("@IsOI", isOI);
            parameterList.Add(parameter);

            parameter = GetBooleanParameter("@IsAE", isAE);
            parameterList.Add(parameter);

            parameter = GetBooleanParameter("@IsAI", isAI);
            parameterList.Add(parameter);

            parameter = GetBooleanParameter("@IsTrk", isTrk);
            parameterList.Add(parameter);

            parameter = GetBooleanParameter("@IsOther", isOth);
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateDate";

            parameter.Value = DBNull.Value;
            if (updateDate.HasValue)
            {
                parameter.Value = updateDate;
            }
            parameterList.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@CustomerID";


            parameter.Value = DBNull.Value;
            if (customerId.HasValue)
            {
                parameter.Value = customerId;
            }
            parameterList.Add(parameter);
            parameter = CacheOperation.GetParameter();

            parameter.ParameterName = "@F4";
            parameter.Value = true; //默认值为True
            parameterList.Add(parameter);

            return parameterList;
        }

        private DbParameter GetBooleanParameter(string name, bool? value)
        {
            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = name;
            parameter.Value = DBNull.Value;
            parameter.DbType = DbType.Boolean;
            if (value.HasValue)
            {
                parameter.Value = value.Value;
            }
            return parameter;
        }
        /// <summary>
        /// 清除内部联系人邮箱地址
        /// </summary>
        public void ClearOperationContactEMail()
        {
            try
            {
                string queryString = string.Format("UPDATE {0} SET Mail='' WHERE {1}", Table_OperationContactCache,
                                                   GetMailQueryString());
                CacheOperation.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }
        /// <summary>
        /// 构建所有内部联系人LIKE条件
        /// </summary>
        /// <returns></returns>
        private string GetMailQueryString()
        {
            string queryString = string.Empty;
            try
            {
                string internalDomain = ClientHelper.GetAppSettingValue("InternalDomain");
                if (!string.IsNullOrEmpty(internalDomain))
                {
                    string[] arrDomains = internalDomain.Split(new char[1] { ',' });
                    StringBuilder strBuf = new StringBuilder();
                    foreach (string item in arrDomains)
                    {
                        strBuf.Append(string.Format(" [Mail] LIKE '%{0}%' OR ", item));
                    }
                    queryString = strBuf.ToString(0, strBuf.Length - 4);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }

            return queryString;
        }
        #endregion

        #region UserCustomGrid
        /// <summary>
        /// 获取用户自定义列表信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
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
            DataTable dtResult = null;
            try
            {
                String queryString;
                if (userId.HasValue)
                {
                    queryString =
                        string.Format(
                            "SELECT [Id],[TemplateCode],[UserId],[ColumnData],[UpdateDate] FROM {0} WHERE [UserId] = @UserId AND [TemplateCode] = @TemplateCode ",
                            Table_UserCustomColumns);
                }
                else
                {
                    queryString =
                        string.Format(
                            "SELECT [Id],[TemplateCode],[UserId],[ColumnData],[UpdateDate] FROM {0} WHERE [UserId] IS NULL AND [TemplateCode] = @TemplateCode ",
                            Table_UserCustomColumns);
                }
                List<DbParameter> parameters = GetCustomGridQueryParameters(userId, templateCode);
                dtResult = CacheOperation.Get(queryString, parameters);

                if (dtResult == null || dtResult.Rows.Count <= 0)
                    return null;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return dtResult;

        }

        private List<DbParameter> GetCustomGridQueryParameters(Guid? userId, ListFormType listType)
        {
            List<DbParameter> listParameter = new List<DbParameter>();


            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@ListFormType";
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

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UserId";
            parameter.Value = DBNull.Value;
            parameter.DbType = DbType.Guid;

            if (userId.HasValue)
            {
                parameter.Value = userId.Value;
            }
            listParameter.Add(parameter);
            return listParameter;

        }

        private bool IsCustomGridInfoExists(Guid id)
        {
            bool returnValue = false;
            try
            {
                string queryString = string.Format("SELECT [ID] FROM {0} WHERE [Id] = @Id ", Table_UserCustomColumns);
                List<DbParameter> listParameter = new List<DbParameter>();
                DbParameter parameter = CacheOperation.GetParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                parameter.DbType = DbType.Guid;

                listParameter.Add(parameter);

                string result = CacheOperation.ExecuteScalar(queryString, listParameter);
                returnValue = !string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        private bool IsCustomGridInfoExists(Guid? userid, string templateCode)
        {
            bool returnValue = false;
            try
            {
                string queryString = string.Format(
                        "SELECT [Id] FROM {0} WHERE [UserID] = @UserID AND [TemplateCode] = @TemplateCode ", Table_UserCustomColumns);
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
                returnValue = !string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        private bool IsContactPersonInfoExists(string senderAddress)
        {
            bool returnValue = false;
            try
            {
                string queryString = string.Format("SELECT COUNT(*) FROM {0} WHERE [Mail] = @EmailAddress ", Table_OperationContactCache);
                List<DbParameter> listParameter = new List<DbParameter>();
                DbParameter parameter = CacheOperation.GetParameter();
                parameter.ParameterName = "@EmailAddress";
                parameter.Value = senderAddress;
                parameter.DbType = DbType.String;

                listParameter.Add(parameter);

                string result = CacheOperation.ExecuteScalar(queryString, listParameter);
                if (string.IsNullOrEmpty(result) || result.Equals("0"))
                    returnValue = false;
                else
                    returnValue = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
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

                    queryString =
                        string.Format(
                            "insert into {0}(Id,TemplateCode,UserId,ColumnData,UpdateDate) values(@Id,@TemplateCode,@UserId,@ColumnData,@UpdateDate)",
                            Table_UserCustomColumns);
                }
                else
                {
                    queryString =
                        string.Format(
                            "update {0} set UserId=@UserId,ColumnData=@ColumnData,UpdateDate=@UpdateDate WHERE Id=@Id",
                            Table_UserCustomColumns);
                }
                List<DbParameter> parameters = GenerateSaveCustomGridInfoParameters(customInfo);
                CacheOperation.ExecuteNonQuery(queryString, parameters);

            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
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

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateBy";
            parameter.Value = customInfo.UserId;
            parameter.DbType = DbType.Guid;
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateDate";
            parameter.Value = DBNull.Value;
            if (customInfo.UpdateDate.HasValue)
            {
                parameter.Value = customInfo.UpdateDate;
            }
            parameter.DbType = DbType.DateTime;
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter("@ColumnData", SqlDbType.NText);
            parameter.Value = SerializerHelper.SerializeToString<UserCustomGridInfo>(customInfo);
            listParameter.Add(parameter);
            listParameter.AddRange(GetCustomGridQueryParameters(customInfo.UserId, customInfo.TemplateCode));


            return listParameter;

        }
        #endregion

        #region Language
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullNames"></param>
        /// <param name="formNames"></param>
        /// <returns></returns>
        public DataTable GetMultiLanguageList(string[] fullNames, string[] formNames)
        {
            string queryString = string.Empty;
            string fullNameParameterPrefix = "FullName";
            string formNameParameterPrefix = "FormName";
            try
            {
                if (fullNames == null && formNames == null)
                {
                    queryString =
                        string.Format(
                            " SELECT ID,FullName,FormName, ControlName,ChineseValue, EnglishValue, ChineseToolTip,EnglishToolTip, UpdateDate,Type from {0}",
                            Table_Languages);

                    return CacheOperation.Get(queryString);
                }
                else
                {
                    string namespaceNames = GetInParameterString(fullNames.Length, fullNameParameterPrefix);
                    string formName = GetInParameterString(formNames.Length, formNameParameterPrefix);

                    queryString =
                        string.Format(
                            " SELECT a.ID,a.FullName,a.FormName, a.ControlName,a.ChineseValue, a.EnglishValue, a.ChineseToolTip,a.EnglishToolTip, a.UpdateDate,a.Type from {0} as a WHERE a.FullName in (" +
                            namespaceNames + ") and a.FormName in (" + formName + ")", Table_Languages);
                    List<DbParameter> parameters = GetInParameters(fullNames.ToList(), fullNameParameterPrefix);
                    parameters.AddRange(GetInParameters(formNames.ToList(), formNameParameterPrefix));
                    return CacheOperation.Get(queryString, parameters);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return null;
        }
        /// <summary>
        /// 如果值确保不会出现SQL特殊字符，则可使用此方法，否则使用GetInParameterString方法
        /// </summary>
        /// <param name="nameArray"></param>
        /// <returns></returns>
        private string GetInClause(string[] nameArray)
        {
            string result = string.Empty;
            if (nameArray.Length == 1)
            {
                result = "'" + nameArray[0] + "'";
            }
            else
            {
                List<string> temp = new List<string>();
                nameArray.ToList().ForEach(item =>
                    {

                        temp.Add("'" + item + "'");
                    });
                result = temp.Aggregate((a, b) => a + "," + b);
            }
            return result;
        }
        private string GetInParameterString(int count, string parameterPrefix)
        {
            if (count == 0)
            {
                return string.Empty;
            }
            string result = string.Empty;
            string prefix = "@" + (string.IsNullOrEmpty(parameterPrefix) ? "Parameter" : parameterPrefix);
            if (count == 1)
            {
                result = prefix + "0";
            }
            else
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    temp.Add(prefix + i);
                }

                result = temp.Aggregate((a, b) => a + "," + b);
            }
            return result;
        }
        private List<DbParameter> GetInParameters(List<string> valueList, string parameterPrefix)
        {
            if (valueList == null || valueList.Count <= 0)
            {
                return null;
            }
            string prefix = "@" + (string.IsNullOrEmpty(parameterPrefix) ? "Parameter" : parameterPrefix);
            List<DbParameter> parameterList = new List<DbParameter>();
            for (int i = 0; i < valueList.Count; i++)
            {
                DbParameter parameter = CacheOperation.GetParameter();
                parameter.ParameterName = prefix + i;
                parameter.Value = valueList[i];
                parameterList.Add(parameter);
            }
            return parameterList;
        }

        public void SaveMultiLanguageList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return;
            List<string> ids = (from item in dt.AsEnumerable()
                                select item.Field<Guid>("ID").ToString()).ToList();
            string[] fullNames = (from item in dt.AsEnumerable()
                                  select item.Field<string>("FullName")).ToArray();
            string[] formNames = (from item in dt.AsEnumerable()
                                  select item.Field<string>("FormName")).ToArray();
            DataTable temp = GetMultiLanguageList(fullNames, formNames);
            if (temp == null || temp.Rows.Count <= 0)
            {
                List<DataRow> rows = (from item in dt.AsEnumerable()
                                      select item).ToList();
                InnerSaveLanguageList(new List<DataRow>(), rows);
            }
            else
            {
                List<Guid> updateIds = (from item in temp.AsEnumerable()
                                        select item.Field<Guid>("ID")).ToList();
                List<DataRow> updateRows = (from row in dt.AsEnumerable()
                                            where updateIds.Contains(row.Field<Guid>("ID"))
                                            select row).ToList();
                List<DataRow> insertRows = (from row in dt.AsEnumerable()
                                            where !updateIds.Contains(row.Field<Guid>("ID"))
                                            select row).ToList();
                InnerSaveLanguageList(updateRows, insertRows);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateRows"></param>
        /// <param name="insertRows"></param>
        private void InnerSaveLanguageList(IEnumerable<DataRow> updateRows, IEnumerable<DataRow> insertRows)
        {
            string updateString =
                string.Format(
                    "UPDATE {0} SET FullName=@FullName,FormName=@FormName,ControlName=@ControlName,ChineseValue=@ChineseValue,EnglishValue=@EnglishValue,Type=@Type,CreateBy=@CreateBy,CreateDate=@CreateDate,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate,ChineseToolTip=@ChineseToolTip,EnglishToolTip=@EnglishToolTip WHERE ID=@ID ",
                    Table_Languages);
            string insertString =
                string.Format(
                    "INSERT INTO {0}(ID,FullName,FormName,ControlName,ChineseValue,EnglishValue,Type,CreateBy,CreateDate,UpdateBy,UpdateDate,ChineseToolTip,EnglishToolTip) values(@ID,@FullName,@FormName,@ControlName,@ChineseValue,@EnglishValue,@Type,@CreateBy,@CreateDate,@UpdateBy,@UpdateDate,@ChineseToolTip,@EnglishToolTip)",
                    Table_Languages);

            try
            {
                _CacheOperation = new SqlCEOperation();
                _CacheOperation.BeginTransaction();
                foreach (DataRow row in updateRows)
                {
                    List<DbParameter> parameters = GenerateSaveLanguageListParameters(row);
                    _CacheOperation.ExecuteNonQuery(updateString, parameters);
                }
                foreach (DataRow row in insertRows)
                {
                    List<DbParameter> parameters = GenerateSaveLanguageListParameters(row);
                    _CacheOperation.ExecuteNonQuery(insertString, parameters);
                }
                _CacheOperation.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (_CacheOperation != null) _CacheOperation.RollBackTransaction();
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                if (_CacheOperation != null)
                    _CacheOperation.EndTransaction();
                _CacheOperation = null;
            }

        }

        private List<DbParameter> GenerateSaveLanguageListParameters(DataRow row)
        {
            List<DbParameter> listParameter = new List<DbParameter>();

            DbParameter parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = row.Field<Guid>("ID");
            parameter.DbType = DbType.Guid;
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@FullName";
            parameter.Value = row.Field<string>("FullName");
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@FormName";
            parameter.Value = row.Field<string>("FormName");
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@ControlName";
            parameter.Value = row.Field<string>("ControlName");
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@ChineseValue";
            parameter.Value = row.Field<string>("ChineseValue");
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@EnglishValue";
            parameter.Value = row.Field<string>("EnglishValue");
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@Type";
            parameter.Value = row.Field<Byte>("Type");
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@ChineseToolTip";
            parameter.Value = row.Field<string>("ChineseToolTip");
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@EnglishToolTip";
            parameter.Value = row.Field<string>("EnglishToolTip");
            listParameter.Add(parameter);


            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateBy";
            parameter.Value = row.Field<Guid?>("UpdateBy");
            parameter.DbType = DbType.Guid;
            if (parameter.Value == null)
            {
                parameter.Value = DBNull.Value;
            }
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@UpdateDate";
            parameter.Value = DBNull.Value;
            if (row.Field<DateTime?>("UpdateDate").HasValue)
            {
                parameter.Value = row.Field<DateTime?>("UpdateDate");
            }
            parameter.DbType = DbType.DateTime;
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@CreateBy";
            parameter.Value = row.Field<Guid?>("CreateBy");
            parameter.DbType = DbType.Guid;
            if (parameter.Value == null)
            {
                parameter.Value = DBNull.Value;
            }
            listParameter.Add(parameter);

            parameter = CacheOperation.GetParameter();
            parameter.ParameterName = "@CreateDate";
            parameter.Value = DBNull.Value;
            if (row.Field<DateTime?>("CreateDate").HasValue)
            {
                parameter.Value = row.Field<DateTime?>("CreateDate");
            }
            parameter.DbType = DbType.DateTime;
            listParameter.Add(parameter);
            return listParameter;
        }

        public void DeleteMultiLanguageList(Guid[] ids)
        {
            if (ids == null)
                return;
            string[] temp = (from id in ids
                             select id.ToString()).ToArray();
            string temp2 = GetInClause(temp);
            string sql = string.Format("DELETE FROM {0} WHERE [ID] IN (" + temp2 + ")", Table_Languages);
            try
            {
                CacheOperation.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {

                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }
        #endregion

        #region OperationMessageRelation

        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllOperationMessageRelation()
        {
            DataTable dtResult = null;
            try
            {
                //获取一月内创建，一周内更新的业务数据
                dtResult = CacheOperation.Get(string.Format("SELECT om.ID,om.FormID, om.FormType,om.OperationId,om.OperationType,om.IMessageID,om.MessageID,om.UpdateDate,om.UpdateBy,om.CreateBy,om.CreateDate,om.StageType AS ContactStage,'' AS OperationNO,om.Autoassociative,om.[F1] AS Contacts,om.[F2] AS XmlMessageInfo,om.[F3] AS Body,om.F4 AS UploadServer,om.[F5] AS BackupMail,om.[F8] AS EntryID FROM {0} om WHERE om.CreateDate > '{1}' OR om.UpdateDate > '{2}' ",
                                Table_OperationMessages, DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
            }
            catch (Exception ex)
            {
                dtResult = null;
            }
            return dtResult;
        }

        /// <summary>
        /// 批量上传邮件与业务的关联信息，邮件外部联系人也业务的关联信息
        /// </summary>
        public void UploadMessageRelation4Batch()
        {
            if (_IsUploadMessageRelation)
                return;
            _IsUploadMessageRelation = true;
            Guid OperationLogID = Guid.NewGuid();
            Stopwatch stopwatchTotaltime = StopwatchHelper.StartStopwatch();
            int length = 0;
            OperationMessageRelation[] operationMessageRelations = null;
            string strException = string.Empty;

            try
            {
                operationMessageRelations = GetLocalMessageRealtionsByUploadServer();
                if (operationMessageRelations == null || operationMessageRelations.Length <= 0) return;
                length = operationMessageRelations.Length;
                AddOperationLog(OperationLogID, DateTime.Now,
                AssamblyName, "UPLOAD-MR", "Begin Upload Message Relation"
                , stopwatchTotaltime.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

                int count = 0;
                do
                {
                    List<OperationMessageRelation> sectionMessageRelations = null;
                    sectionMessageRelations = count == 0 ? operationMessageRelations.Take(MaxCount).ToList() : operationMessageRelations.Skip(count).Take(MaxCount).ToList();
                    try
                    {
                        Guid[] messageRelationIDs =
                            OperationMessageRelationService.SaveMessageRelations(sectionMessageRelations.Serialize());
                        if (messageRelationIDs != null && messageRelationIDs.Length > 0)
                        {
                            RemoveOperationMessageRelations(messageRelationIDs);

                            Array.ForEach(messageRelationIDs,
                                relationID => sectionMessageRelations.RemoveAll(item => item.ID == relationID));
                        }
                        //将本地F4设置为True，F1和F2值清空
                        sectionMessageRelations.ForEach(item =>
                        {
                            item.Contacts = null;
                            item.XmlMessageInfo = null;
                            item.UploadServer = true;
                        });

                        SaveOperationMessageRelation(sectionMessageRelations.ToArray());

                    }
                    catch (Exception ex)
                    {
                        //将保存失败的数据中重复的数据删除，以便下次上传使用
                        sectionMessageRelations.ForEach(item =>
                        {
                            item.UploadServer = false;
                        });
                        SaveOperationMessageRelation(
                            sectionMessageRelations.ToArray());
                    }
                    finally
                    {
                        count = count + MaxCount;
                    }
                } while (length > count);
            }
            catch (Exception ex)
            {
                strException = string.Format("{0}\r\nOperationLogID:[{1}]\r\nLocalVersionNo:[{2}]", CommonHelper.BuildExceptionString(ex), OperationLogID, LocalData.LocalVersionNo);
                Logger.Log.Error(strException);
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName
                                , LocalData.SessionId, new byte[0], strException, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
            finally
            {
                operationMessageRelations = null;
                _IsUploadMessageRelation = false;

                stopwatchTotaltime.Stop();
                if (length > 0)
                {

                    UpdateOperationLog(OperationLogID,
                        (string.IsNullOrEmpty(strException) ? string.Format("Total[{0}]", length) : string.Format("Exception SessionID:{0}", LocalData.SessionId))
                        , string.Empty, string.Empty, false, false
                        , stopwatchTotaltime.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                }
                stopwatchTotaltime = null;
            }
        }

        /// <summary>
        /// 根据 MessageID 查询关联信息
        /// </summary>
        /// <param name="messageId">Mail MessageID</param>
        /// <returns>关联信息Datatable</returns>
        public DataTable GetOperationMessageRelationByMessageId(string messageId)
        {
            DataTable dtResult = null;
            DbParameter parameter = CreateDBParameter("@MessageID", messageId, DbType.String);
            try
            {
                dtResult = CacheOperation.Get(BuildSqlQueryForMessageID(), parameter);
            }
            catch (Exception ex)
            {
                dtResult = null;
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return dtResult;
        }

        /// <summary>
        /// 根据 MessageID 去查找关联信息，如果没有找到，再通过 Reference 查找元邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId,
                                                                                                string reference)
        {
            List<OperationMessageRelation> relations = null;
            if (string.IsNullOrEmpty(messageId) && string.IsNullOrEmpty(reference))
                return new List<OperationMessageRelation>();

            try
            {

                if (!string.IsNullOrEmpty(messageId))
                {
                    relations = InnerGetOperationMessageRelationByMessageId(messageId);
                    if (relations != null && relations.Count > 0)
                        return relations;
                    else
                    {
                        if (string.IsNullOrEmpty(reference))
                            return relations;

                        relations = InnerGetOperationMessageRelationByMessageId(reference);
                    }
                }
                else
                    relations = InnerGetOperationMessageRelationByMessageId(reference);

            }
            catch (Exception ex)
            {

                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return relations;
        }

        /// <summary>
        /// 通过 MessageID、OperationID 集合查询关联信息
        /// </summary>
        /// <param name="messageID">Mail MessageID</param>
        /// <param name="operationIds">OperationIDs</param>
        /// <returns>关联信息集合对象</returns>
        public List<OperationMessageRelation> GetOperationMessages(string messageID, Guid[] operationIds)
        {
            if ((operationIds == null || operationIds.Length <= 0) || string.IsNullOrEmpty(messageID))
                return new List<OperationMessageRelation>();

            try
            {
                string queryString = string.Format("SELECT om.ID,om.FormID, om.FormType,om.OperationId,om.OperationType,om.IMessageID,om.MessageID,om.UpdateDate,om.UpdateBy,om.CreateBy,om.CreateDate,om.StageType AS ContactStage,'' AS OperationNO,om.Autoassociative FROM {0} om WHERE om.OperationID IN ({1}) AND om.MessageID='{2}'"
                        , Table_OperationMessages, ConvertIDAsString(operationIds), messageID);
                DataTable dt = CacheOperation.Get(queryString);
                return MessageUtility.ConvertDataTableToOperationMessageRelation(dt);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return new List<OperationMessageRelation>();
        }

        /// <summary>
        /// 本地关联信息缓存表是否存在关联信息
        /// </summary>
        /// <param name="messageID">Mail MessageID</param>
        /// <returns>存在结果[ture：存在、false：不存在]</returns>
        public bool HasLocalOperationMessageRelation(string messageID)
        {
            if (string.IsNullOrEmpty(messageID))
                return false;

            try
            {
                string queryString = string.Format("Select count(*) from {0} om WHERE om.MessageID=@MessageID", Table_OperationMessages);
                var dbParameters = new List<DbParameter>();
                dbParameters.Add(CreateDBParameter("@MessageID", messageID, DbType.String));

                string result = CacheOperation.ExecuteScalar(queryString, dbParameters);
                if (!string.IsNullOrEmpty(result) && !result.Equals("0"))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }

            return false;
        }

        /// <summary>
        /// 通过MessageID获取单个关联信息
        /// </summary>
        /// <param name="messageId">Mail MessageID</param>
        /// <returns>关联信息</returns>
        public OperationMessageRelation GetOperationMessageRelationByMessageID(string messageId)
        {
            try
            {
                if (string.IsNullOrEmpty(messageId))
                    return new OperationMessageRelation() { HasData = false };
                string queryString = string.Format("SELECT * FROM {0} AS om WHERE om.MessageID=@MessageID", Table_OperationMessages);
                List<DbParameter> dbParameters = new List<DbParameter>();
                dbParameters.Add(CreateDBParameter("@MessageID", messageId, DbType.String));
                DataTable dt = CacheOperation.Get(queryString, dbParameters);
                if (dt == null | dt.Rows.Count <= 0)
                    return new OperationMessageRelation() { HasData = false };
                else
                {
                    if (dt.Rows.Count == 1)
                    {
                        return DataTableConvert(dt).SingleOrDefault();
                    }
                    else
                    {
                        //如果找到两条以上记录，返回服务端下载下来的数据(更新时间不为空记录为服务器下载数据)
                        DataRow[] rows = dt.Select("F4='True'");
                        if (rows != null && rows.Length > 0)
                        {
                            foreach (var row in rows)
                            {
                                if (row.Field<DateTime?>("UpdateDate") != null)
                                {
                                    return
                                        DataTableConvert(FCMInterfaceUtility.SingleRowAsTable(row))
                                            .SingleOrDefault();
                                }
                            }
                        }
                        else
                        {
                            return
                                DataTableConvert(FCMInterfaceUtility.SingleRowAsTable(dt, 0))
                                    .SingleOrDefault();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return new OperationMessageRelation() { HasData = false };
        }

        /// <summary>
        /// 获取还未上传到服务端的关联数据
        /// </summary>
        /// <returns></returns>
        public OperationMessageRelation[] GetLocalMessageRealtionsByUploadServer()
        {
            OperationMessageRelation[] operationMessageRelations = null;
            try
            {
                DataTable dt = CacheOperation.Get(string.Format("SELECT * FROM {0} WHERE [F4]='False' AND [MessageID]<>''  ORDER BY [CreateDate] DESC ", Table_OperationMessages));
                if (dt != null && dt.Rows.Count > 0)
                {
                    operationMessageRelations = OperationMessageRealtionsFilter(DataTableConvert(dt));
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return operationMessageRelations;
        }

        /// <summary>
        /// 通过是否备份状态获取没有上传到服务端的所有所有关联信息
        /// </summary>
        /// <returns></returns>
        public OperationMessageRelation[] GetLocalMessageRealtionsByBackupMail()
        {
            OperationMessageRelation[] operationMessageRelations = null;
            try
            {
                DataTable dt = GetNeedBackupMail("Normal");
                if (dt != null && dt.Rows.Count > 0)
                {
                    operationMessageRelations = DataTableConvert(dt).ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return operationMessageRelations;
        }

        /// <summary>
        /// 获取缓存业务信息和邮件关联信息
        /// </summary>
        /// <param name="criteria"></param>
        ///<param name="messageID">消息中的MessageID特性值</param>
        ///<param name="reference">消息中的Reference特性值</param>
        /// <returns></returns>
        public BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string reference)
        {
            BusinessQueryResult result = new BusinessQueryResult();
            try
            {
                //表示邮件主题匹配到两个关键字
                result.Dt = GetOperationListBySubjectInNo(criteria, true);
                result.Relations = GetOperationMessageRelationByMessageIdAndReference(messageID, reference);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return result;
        }

        public DataTable GetOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria)
        {
            return GetOperationListBySubjectInNo(criteria, false);
        }

        /// <summary>
        /// 批量保存关联信息
        /// </summary>
        /// <param name="relations">关联数据集合</param>
        public void SaveOperationMessageRelation(OperationMessageRelation[] relations)
        {
            if (relations == null || relations.Length <= 0)
                return;
            try
            {
                switch (relations[0].UpdateDataType)
                {
                    case UpdateDataType.AddNew:
                        if (!relations[0].UploadServer)
                        {
                            if (relations.Length > 5)
                            {
                                //数据库存在邮件且关联MessageID一致则不需重复保存
                                Message.ServiceInterface.Message messageInfo =
                                    MessageService.Get(relations[0].IMessageId);
                                if (messageInfo != null && !messageInfo.MessageId.Equals(relations[0].MessageId))
                                    return;
                            }
                        }
                        break;
                    case UpdateDataType.MainForMessageID:
                        List<string> messageIDs = (from r in relations
                                                   select r.MessageId).ToList();
                        RemoveOperationMessageRelation(messageIDs);
                        break;
                    case UpdateDataType.MainForOperationID:
                        Guid[] operationIds = (from r in relations
                                               select r.OperationID).ToArray();
                        RemoveOperationMessageRelation(operationIds);
                        break;
                }

                //过滤重复的关联信息
                List<String> tempMessageIDs = new List<string>();
                string arrText = string.Empty;
                foreach (var item in relations)
                {
                    arrText = string.Format("{0}{1}", item.OperationID, item.MessageId).ToLower();
                    if (!tempMessageIDs.Contains(arrText))
                    {
                        tempMessageIDs.Add(arrText);
                        SingleSaveLocalOperationMessageRelation(item);
                    }
                    else
                        RemoveOperationMessageRelations(new Guid[1] { item.ID });
                }
                tempMessageIDs = null;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }

        }

        /// <summary>
        /// 移除相同的Xml MessageInfo
        /// </summary>
        /// <param name="imessageId"></param>
        /// <param name="xmlMessageInfo"></param>
        /// <returns></returns>
        public string RemoveSameMessageInfoByIMessageID(Guid imessageId, string xmlMessageInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlMessageInfo))
                    return xmlMessageInfo;

                string queryString = string.Format("SELECT F4 UploadServer,F2 AS XmlMessageInfo FROM {0} WHERE IMessageID=@IMessageID",
                                                   Table_OperationMessages);
                List<DbParameter> dbParameters = new List<DbParameter>();
                dbParameters.Add(CreateDBParameter("@IMessageID", imessageId, DbType.Guid));
                DataTable dt = CacheOperation.Get(queryString, dbParameters);
                if (dt == null || dt.Rows.Count <= 0)
                    return xmlMessageInfo;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存邮件备份状态
        /// </summary>
        /// <param name="relations"></param>
        /// <returns></returns>                
        public void SaveBackUpMailState(OperationMessageRelation[] relations)
        {
            try
            {
                if (relations == null || relations.Length <= 0)
                    return;
                foreach (var item in relations)
                {
                    SingleSaveBackUpMailState(item);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 移除相同的关联信息
        /// </summary>
        /// <param name="updateDate">更新时间</param>
        /// <param name="relationID">RelationID</param>
        /// <param name="messageID">Mail MessageID</param>
        /// <param name="operationID">OperationID</param>
        public void RemoveSameOperationMessageRelation(bool updateDate, Guid relationID, string messageID, Guid operationID)
        {
            try
            {
                if (string.IsNullOrEmpty(messageID) || operationID == Guid.Empty)
                {
                    return;
                }

                string queryString = string.Empty;
                //新增数据时，判断是否有重复的关联信息
                if (!updateDate)
                {
                    queryString =
                       string.Format("DELETE FROM {0} WHERE MessageID='{1}' AND OperationID='{2}' AND F4='False' ",
                                     Table_OperationMessages, messageID, operationID);

                    CacheOperation.ExecuteNonQuery(queryString);
                }
                else
                {
                    //更改数据时判断是否有重复的关联信息
                    queryString = string.Format("SELECT ID AS RelationID,OperationID from {0} WHERE MessageID='{1}' AND OperationID='{2}' AND F4='False' ", Table_OperationMessages, messageID, operationID);
                    DataTable dt = CacheOperation.Get(queryString);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row.Field<Guid>("RelationID") != relationID)
                            {
                                CacheOperation.ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE ID='{1}'",
                                                                             Table_OperationMessages,
                                                                             row.Field<Guid>("RelationID")));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 删除相同业务号和相同联系人的联系人
        /// </summary>
        /// <param name="relationID">RelationID</param>
        /// <param name="operationId">OperationID</param>
        /// <param name="xmlContacts"></param>
        public string RemoveSameOperationContacts(Guid? relationID, Guid operationId, string xmlContacts)
        {
            try
            {
                if (!string.IsNullOrEmpty(xmlContacts))
                {
                    string queryString = string.Format("SELECT F1 AS Contacts,ID AS RelationID FROM {0} WHERE [OperationID] = @OperationID AND F4='False'", Table_OperationMessages);
                    List<DbParameter> dbParameters = new List<DbParameter>(1);
                    dbParameters.Add(CreateDBParameter("@OperationID", operationId, DbType.Guid));

                    DataTable dt = CacheOperation.Get(queryString, dbParameters);
                    if (dt == null || dt.Rows.Count <= 0)
                        return xmlContacts;
                    else
                    {
                        bool isConvert = false;
                        MatchCollection collection = Regex.Matches(xmlContacts, "<row .*?/>", RegexOptions.IgnoreCase);
                        List<string> contacts = collection.Cast<Match>().Select(m => m.Value).ToList();

                        foreach (DataRow row in dt.Rows)
                        {
                            string _xmlContacts = row.Field<string>("Contacts");
                            if (!string.IsNullOrEmpty(_xmlContacts))
                            {
                                MatchCollection _collection = Regex.Matches(_xmlContacts, "Mail=\".*?\"", RegexOptions.IgnoreCase);
                                IEnumerator enumerator = _collection.GetEnumerator();
                                while (enumerator.MoveNext())
                                {
                                    string singleContact = ((Match)enumerator.Current).Value;
                                    List<string> result = contacts.Where(item => item.Contains(singleContact)).ToList();
                                    if (result != null && result.Count > 0)
                                    {
                                        if (relationID.HasValue)
                                        {
                                            //更改业务数据,不清空相同RelationID的业务联系人
                                            if (relationID.Value == row.Field<Guid>("RelationID"))
                                                continue;
                                        }
                                        contacts.Remove(result[0]);
                                        isConvert = true;
                                    }
                                }
                            }
                        }

                        if (!isConvert)
                            return xmlContacts;
                        else
                        {
                            if (contacts.Count == 0)
                                return null;
                            else
                                return string.Join(string.Empty, contacts.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据MessageID删除邮件与业务的关联信息
        /// </summary>
        /// <param name="messageIDs"></param>
        public void RemoveOperationMessageRelation(List<string> messageIDs)
        {
            try
            {
                if (messageIDs == null || messageIDs.Count <= 0)
                {
                    return;
                }

                string removeSql = string.Format("DELETE FROM {0} WHERE MessageID IN ({1})", Table_OperationMessages, ConvertListToString(messageIDs));
                CacheOperation.ExecuteNonQuery(removeSql);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }

        }
        /// <summary>
        /// 根据主键ID删除关联信息
        /// </summary>
        /// <param name="messageRelationIDs"></param>
        public void RemoveOperationMessageRelations(Guid[] messageRelationIDs)
        {
            try
            {
                if (messageRelationIDs == null || messageRelationIDs.Length <= 0)
                    return;
                string queryString = string.Format("delete from {0} WHERE ID in ({1})", Table_OperationMessages, ConvertIDAsString(messageRelationIDs));
                CacheOperation.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 根据OperationID来删除邮件与业务的关联信息
        /// </summary>
        /// <param name="operationIDs"></param>
        public void RemoveOperationMessageRelation(Guid[] operationIDs)
        {
            try
            {
                if (operationIDs == null && operationIDs.Length <= 0)
                    return;

                string removeSql = string.Format("DELETE FROM {0} WHERE OperationID IN ({1})", Table_OperationMessages, ConvertIDAsString(operationIDs));
                CacheOperation.ExecuteNonQuery(removeSql);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        public void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType, DataTable dt)
        {
            try
            {
                if (operationIds == null || operationIds.Count <= 0 || dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                if (operationIds.Count == 1)
                {
                    SingleUpdateLocalBusinessData(operationIds[0], operationType, dt);
                }
                else
                {
                    foreach (Guid operationId in operationIds)
                    {
                        DataRow[] rows = dt.Select(string.Format("OceanBookingID='{0}'", operationId));
                        if (rows != null && rows.Length > 0)
                        {
                            SingleUpdateLocalBusinessData(operationId, operationType, rows.CopyToDataTable());
                        }
                    }
                }
                LocalData.OperationViewInfo = GetConciseOperationViewInfo();
                PushBusinessDataToOutlook(operationIds.ToArray());
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        public void SaveOperationContactMail(List<OperationContactParameters> ContactParameters)
        {
            try
            {
                if (ContactParameters == null || ContactParameters.Count <= 0)
                    return;

                foreach (var item in ContactParameters)
                {
                    if (item.Mails == null || item.Mails.Count <= 0)
                        continue;

                    string contactMail = GetSingleOperationContactMail(item.OceanBookingID, item.OperationType);
                    if (string.IsNullOrEmpty(contactMail))
                    {
                        contactMail = string.Join(";", item.Mails.ToArray());
                    }
                    else
                    {
                        foreach (string mail in item.Mails)
                        {
                            if (!string.IsNullOrEmpty(mail))
                            {
                                if (!contactMail.Contains(mail))
                                {
                                    contactMail = string.Format("{0};{1}", contactMail, mail);
                                }
                            }
                        }
                    }

                    SingleSaveOperationContactMail(item.OceanBookingID, item.OperationType, contactMail);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }


        public void SingleSaveOperationContactMail(Guid operationID, OperationType operationType, string contactMail)
        {
            try
            {
                string queryString = string.Format("Update {0} SET ContactMail ='{1}' WHERE [OceanBookingID]='{2}' ", Table_OperationViewOECache, contactMail, operationID);
                CacheOperation.ExecuteNonQuery(queryString);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        public string GetSingleOperationContactMail(Guid operationId, OperationType operationType)
        {
            string strResult = string.Empty;
            try
            {
                string tableName = GetBusinessTableName(operationType);
                string queryString = string.Format("SELECT [ContactMail] FROM {0} WHERE {1}='{2}'", tableName, "OceanBookingID", operationId);
                strResult = CacheOperation.ExecuteScalar(queryString);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
                strResult = string.Empty;
            }
            return strResult;
        }


        public DataTable GetOperationViewInfo(Guid operationID, OperationType type)
        {
            DataTable dtResult = null;
            try
            {
                if (operationID != Guid.Empty)
                {
                    string tableName = GetBusinessTableName(type);
                    string queryString = string.Empty;
                    if (type == OperationType.OceanExport)
                    {
                        queryString = string.Format("SELECT ov.APCopy,ov.MBLCopy,ov.CompanyID,ov.SOCopy,ov.SONO,ov.BLNO,{0} AS Carrier,ov.ContactMail,ov.ContainerDesc ,ov.OceanBookingID,ov.IsValid ,ov.NO AS NO,ov.POD_EName AS POD,ov.POL_EName AS POL,ov.RefNO,ov.UpdateDate,ov.Vessel,ov.Voyage FROM {1} ov WHERE OceanBookingID='{2}'",
                                                           GetCarrierName(), tableName, operationID);

                        dtResult = CacheOperation.Get(queryString);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
                dtResult = null;
            }
            return dtResult;
        }

        public DataTable GetOperationAssistantInfo(Guid operationID, OperationType operationType)
        {
            DataTable dtResult = null;

            try
            {
                if (operationID != Guid.Empty && OperationType.OceanExport.Equals(operationType))
                {
                    string queryString =
                        string.Format(
                            "SELECT OceanBookingID,SOByID,CustomerServiceID,SalesID,DocByID,CompanyID FROM {0}  WHERE OceanBookingID='{1}'",
                            Table_OperationViewOECache, operationID);
                    dtResult = CacheOperation.Get(queryString);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
                dtResult = null;
            }
            return dtResult;
        }

        public bool RemoveJunkOperationMessageRelationData()
        {

            string queryString =
                string.Format(
                    "DELETE FROM {0} WHERE  F4='False'  AND   [CreateDate]>'2014-05-12 00:00:00.000' AND [CreateDate]<'2014-05-13 23:59:59.000'", Table_OperationMessages);
            try
            {
                CacheOperation.ExecuteNonQuery(queryString);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 重置异常数据
        /// </summary>
        /// <returns>直接结果</returns>
        public bool ResetExceptionData()
        {
            string sqlExc = string.Format(
                                    @" UPDATE [{0}] SET [F5]='false' WHERE [CreateDate] > '2015-02-01' ", Table_OperationMessages);
            try
            {
                CacheOperation.ExecuteNonQuery(sqlExc);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void SingleUpdateLocalBusinessData(Guid operationID, OperationType operationType, DataTable dt)
        {
            string queryString = string.Empty;
            string tableName = GetBusinessTableName(operationType);
            List<DbParameter> parameters;
            string setClause = string.Empty;

            if (IsShipmentExists(operationID, operationType))
            {
                setClause = GetSetClause(dt, true);
                queryString = string.Format("UPDATE {0} SET {1} WHERE OceanBookingID='{2}'", tableName, setClause, operationID);
                parameters = GetUpdateBusinessDataParameters(dt, true);
                CacheOperation.ExecuteNonQuery(queryString, parameters);
            }
            else
            {
                List<string> columnNames = GetColumnNames(dt);
                string columnNameString = columnNames.Count == 1 ? columnNames[0] : columnNames.Aggregate((a, b) => a + "," + b);
                string parameterNamesString = columnNames.Count == 1 ? "@" + columnNames[0] : string.Format("@{0}", string.Join(",@", columnNames.ToArray()));
                queryString = string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, columnNameString, parameterNamesString);
                parameters = GetUpdateBusinessDataParameters(dt, false);
                CacheOperation.ExecuteNonQuery(queryString, parameters);
            }
        }

        private List<DbParameter> GetUpdateBusinessDataParameters(DataTable dt, bool isUpdate)
        {
            DataRow row = dt.Rows[0];
            List<DbParameter> parameters = new List<DbParameter>();
            int count = dt.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string columnName = dt.Columns[i].ColumnName;
                if (isUpdate && columnName.Equals("ID"))
                {
                    continue;
                }

                DbParameter parameter = CacheOperation.GetParameter();

                parameter.ParameterName = "@" + columnName;
                object value = row[columnName];
                if (value == null || value == DBNull.Value)
                {
                    parameter.Value = DBNull.Value;
                }
                else
                {
                    parameter.Value = value;
                }

                parameters.Add(parameter);

            }
            return parameters;
        }

        private List<string> GetColumnNames(DataTable dt)
        {
            List<string> columnNames = new List<string>();
            int count = dt.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                columnNames.Add(dt.Columns[i].ColumnName);
            }
            return columnNames;
        }

        private Dictionary<string, string> GetFieldNameAndValuePairs(DataTable dt)
        {
            DataRow row = dt.Rows[0];
            Dictionary<string, string> fieldNameAndValuePairs = new Dictionary<string, string>();
            int count = dt.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string columnName = dt.Columns[i].ColumnName;
                fieldNameAndValuePairs.Add(columnName, row[columnName] == null ? string.Empty : row[columnName].ToString());

            }
            return fieldNameAndValuePairs;

        }

        private string GetSetClause(DataTable dt, bool isUpdate)
        {
            DataRow row = dt.Rows[0];
            List<string> items = new List<string>();
            int count = dt.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string columnName = dt.Columns[i].ColumnName;
                if (isUpdate && columnName.Equals("ID"))
                    continue;
                items.Add(columnName + "=@" + columnName);
            }
            return items.Aggregate((a, b) => a + "," + b);
        }

        private string GetBusinessTableName(OperationType operationType)
        {
            string tableName = string.Empty;
            switch (operationType)
            {
                case OperationType.OceanExport:
                    tableName = Table_OperationViewOECache;
                    break;
                case OperationType.OceanImport:
                    tableName = Table_OperationViewOECache;
                    break;
                case OperationType.InquireRate:
                    tableName = Table_OperationViewOECache;
                    break;
            }
            if (tableName.IsNullOrEmpty())
                throw new Exception("不支持当前业务类型");
            return tableName;
        }

        private void SingleSaveBackUpMailState(OperationMessageRelation relation)
        {
            string queryString = string.Empty;
            try
            {
                List<DbParameter> parameters = new List<DbParameter>();
                //更改关联信息
                queryString =
                    string.Format(
                        "UPDATE [{0}] SET [F5]=@BackupMail WHERE [ID]=@ID",
                        Table_OperationMessages);
                parameters.Add(CreateDBParameter("@ID", relation.ID, DbType.Guid));
                parameters.Add(CreateDBParameter("@BackupMail", relation.BackupMail, DbType.Boolean));
                CacheOperation.ExecuteNonQuery(queryString, parameters);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(queryString);
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 构建查询语句
        /// </summary>
        /// <returns></returns>
        private string BuildSqlQueryForMessageID()
        {
            return string.Format("SELECT om.ID,om.FormID, om.FormType,om.OperationId,om.OperationType,om.IMessageID,om.MessageID,om.UpdateDate,om.UpdateBy,om.CreateBy,om.CreateDate,om.StageType AS ContactStage,ov.NO AS OperationNO,om.Autoassociative,om.F4 as UploadServer,om.[F5] AS BackupMail,om.[F8] AS EntryID FROM {0} om LEFT JOIN {1} ov on om.OperationID=ov.OceanBookingID WHERE om.MessageID=@MessageID",
                                Table_OperationMessages, Table_OperationViewOECache);
        }

        /// <summary>
        /// 根据 MessageID 查询关联信息
        /// </summary>
        /// <param name="messageId">Mail MessageID</param>
        /// <returns>关联信息集合对象</returns>
        private List<OperationMessageRelation> InnerGetOperationMessageRelationByMessageId(string messageId)
        {
            DataTable dt = GetOperationMessageRelationByMessageId(messageId);
            return MessageUtility.ConvertDataTableToOperationMessageRelation(dt);
        }

        /// <summary>
        /// 保存单个业务关联信息
        /// </summary>
        /// <param name="relation"></param>
        private void SingleSaveLocalOperationMessageRelation(OperationMessageRelation relation)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            string queryString = string.Empty;
            bool updateDate = false;
            try
            {
                //查询相同的关联信息
                DataTable dtResult = ExsitsOperationMessageRelation(relation.ID, relation.OperationID,
                                                                             relation.MessageId);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    if (dtResult.Rows.Count == 1)
                    {
                        //更改关联信息
                        queryString =
                            string.Format(
                                "Update {0} SET ID=@ID, OperationID=@OperationID, OperationType=@OperationType, FormType=@FormType, FormID=@FormID,IMessageID=@IMessageID,CreateBy=@CreateBy,CreateDate=@CreateDate,MessageID=@MessageID,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate,StageType=@StageType,Autoassociative=@Autoassociative,F1=@Contacts,F2=@XmlMessageInfo,F4=@UploadServer,F5=@BackupMail,F8=@EntryID WHERE ID=@ID OR (OperationID=@OperationID AND MessageID=@MessageID)",
                                Table_OperationMessages);
                    }
                    else
                    {
                        //移除重复的关联信息
                        RemoveSameOperationMessageRelation(true, relation.ID, relation.MessageId, relation.OperationID);
                        queryString =
                           string.Format(
                               "Update {0} SET ID=@ID, OperationID=@OperationID, OperationType=@OperationType, FormType=@FormType, FormID=@FormID,IMessageID=@IMessageID,CreateBy=@CreateBy,CreateDate=@CreateDate,MessageID=@MessageID,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate,StageType=@StageType,Autoassociative=@Autoassociative,F1=@Contacts,F2=@XmlMessageInfo,F4=@UploadServer,F5=@BackupMail,F8=@EntryID WHERE ID=@ID",
                               Table_OperationMessages);
                    }
                    updateDate = true;
                }
                else
                {
                    queryString =
                          string.Format(@"INSERT INTO {0}([ID],[OperationID],[OperationType],[FormID],[FormType],[IMessageID],[CreateBy],[CreateDate],[UpdateBy],[UpdateDate],[MessageID],[StageType],[Autoassociative],[F1],[F2],[F4],[F5],[F8])
                                            values(@ID,@OperationID,@OperationType,@FormID,@FormType,@IMessageID,@CreateBy,@CreateDate,@UpdateBy,@UpdateDate,@MessageID,@StageType,@Autoassociative,@Contacts,@XmlMessageInfo,@UploadServer,@BackupMail,@EntryID)",
                                        Table_OperationMessages);
                    updateDate = false;
                }
                parameters.Add(CreateDBParameter("@OperationID", relation.OperationID, DbType.Guid));
                parameters.Add(CreateDBParameter("@OperationType", relation.OperationType.GetHashCode(), DbType.Int32));
                parameters.Add(CreateDBParameter("@FormType", relation.FormType.GetHashCode(), DbType.Int32));
                parameters.Add(CreateDBParameter("@FormID", relation.FormID, DbType.Guid));
                parameters.Add(CreateDBParameter("@UpdateDate", relation.UpdateDate, DbType.DateTime));
                parameters.Add(CreateDBParameter("@ID", relation.ID, DbType.Guid));
                parameters.Add(CreateDBParameter("@IMessageID", relation.IMessageId, DbType.String));
                parameters.Add(CreateDBParameter("@CreateBy", relation.CreateBy, DbType.Guid));
                parameters.Add(CreateDBParameter("@CreateDate", relation.CreateDate ?? DateTime.Now, DbType.DateTime));
                parameters.Add(CreateDBParameter("@UpdateBy", relation.UpdateBy, DbType.Guid));
                parameters.Add(CreateDBParameter("@MessageID", relation.MessageId, DbType.String));
                parameters.Add(CreateDBParameter("@StageType", relation.ContactStage.GetHashCode(), DbType.Int32));
                parameters.Add(CreateDBParameter("@Autoassociative", relation.RelationType == MessageRelationType.Auto, DbType.Boolean));
                string contacts = null, xmlMessageInfo = null;
                if (updateDate)
                {
                    if (dtResult.Rows[0].Field<bool>("F4").Equals(relation.UploadServer))
                    {
                        contacts = RemoveSameOperationContacts(relation.ID, relation.OperationID, relation.Contacts);
                        xmlMessageInfo = relation.XmlMessageInfo;
                    }
                    else
                    {
                        contacts = RemoveSameOperationContacts(null, relation.OperationID, relation.Contacts);
                        xmlMessageInfo = RemoveSameMessageInfoByIMessageID(relation.IMessageId, relation.XmlMessageInfo);
                    }
                }
                else
                {
                    RemoveSameOperationMessageRelation(false, relation.ID, relation.MessageId, relation.OperationID);
                    contacts = RemoveSameOperationContacts(null, relation.OperationID, relation.Contacts);
                    xmlMessageInfo = RemoveSameMessageInfoByIMessageID(relation.IMessageId, relation.XmlMessageInfo);
                }
                parameters.Add(CreateDBParameter("@Contacts", contacts, DbType.String));
                parameters.Add(CreateDBParameter("@XmlMessageInfo", xmlMessageInfo, DbType.String));
                parameters.Add(CreateDBParameter("@UploadServer", relation.UploadServer, DbType.Boolean)); //F4
                parameters.Add(CreateDBParameter("@BackupMail", relation.BackupMail, DbType.Boolean));     //F5
                parameters.Add(CreateDBParameter("@EntryID", relation.EntryID, DbType.String));            //F8

                CacheOperation.ExecuteNonQuery(queryString, parameters);
            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("SingleSaveLocalOperationMessageRelation Relation\r\n");
                str.AppendFormat("RelationID:{0};MessageId:{1}\r\n", relation.ID, relation.MessageId);
                Logger.Log.Info(str.ToString());
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 获取供应商字段名
        /// </summary>
        /// <returns></returns>
        private string GetCarrierName()
        {
            return (IsEnglish ? "Carrier_EName" : "Carrier_CName");
        }

        private EnumerableRowCollection<OperationMessageRelation> DataTableConvert(DataTable dt)
        {
            return (from message in dt.AsEnumerable()
                    select new OperationMessageRelation
                    {
                        HasData = true,
                        MessageId =
                            message.Field<string>("MessageID"),
                        ID = message.Field<Guid>("ID"),
                        OperationID =
                            message.Field<Guid>("OperationID"),
                        IMessageId =
                            message.Field<Guid>("IMessageID"),
                        FormID = message.Field<Guid?>("FormID"),
                        FormType =
                            message.IsNull("FormType")
                                ? FormType.Unknown
                                : (FormType)
                                  message.Field<byte>("FormType"),
                        OperationType =
                                message.IsNull("OperationType")
                                ? OperationType.Unknown
                                : (OperationType)
                                message.Field<byte>("OperationType"),
                        UpdateDate =
                            message.Field<DateTime?>(
                                "UpdateDate"),
                        UpdateBy =
                            message.Field<Guid?>("UpdateBy"),
                        CreateBy =
                            message.Field<Guid>("CreateBy"),
                        CreateDate =
                            message.Field<DateTime?>(
                                "CreateDate"),
                        ContactStage =
                            (message.IsNull("StageType") || (string.IsNullOrEmpty(message.Field<string>("StageType").Trim())) || message.Field<string>("StageType").Contains(","))
                                ? ContactStage.Unknown
                                : (ContactStage?)Int32.Parse(message.Field<string>("StageType")),
                        RelationType =
                            message.IsNull("Autoassociative")
                                ? MessageRelationType.Auto
                                : (message.Field<bool>("Autoassociative") ? MessageRelationType.Auto
                                       : MessageRelationType.Hand),
                        UploadServer = true, //F4
                        Contacts = message.Field<string>("F1"), //联系人序列化成xml
                        XmlMessageInfo = message.Field<string>("F2"), //邮件序列化成xml
                        BackupMail = !message.IsNull("F5") && message.Field<bool>("F5"),   //F5:是否上传邮件
                        EntryID = message.IsNull("F8") ? "" : message.Field<string>("F8")   //F8：邮件EntryID
                    });
        }

        /// <summary>
        /// 获取需要上传备份的邮件
        /// </summary>
        /// <param name="strRange"></param>
        /// <returns></returns>
        private DataTable GetNeedBackupMail(string strRange)
        {
            DataTable dt = null;
            const string selectField = "[ID],[MessageID],[OperationID],[IMessageID],[UpdateBy],[UpdateDate],[CreateBy],[CreateDate],[Autoassociative],[F5],[F8],[FormID],[FormType],[OperationType],[StageType],'' AS F1,'' AS F2";
            string strSql =
                string.Format(
                    "SELECT {0} FROM {1} WHERE ([F5]='false' OR [F5] IS NULL) AND  F4='True' AND [MessageID]<>'' AND ([MessageID] NOT LIKE '<system%' AND [MessageID] NOT LIKE 'system%') ",
                    selectField, Table_OperationMessages);
            string strExecSql = string.Empty;

            switch (strRange)
            {
                case "Normal":
                    strExecSql = string.Format("{0} AND ([CreateDate] > '{1}') ORDER BY [CreateDate] DESC", strSql, "2014-12-01");
                    break;
                default:
                    strExecSql = string.Format("{0} ORDER BY [CreateDate] DESC", strSql);
                    break;
            }
            dt = CacheOperation.Get(strExecSql);
            return dt;
        }

        /// <summary>
        /// 未上传到服务端的关联数据过滤,过滤掉发件人为空的信息
        /// </summary>
        /// <param name="infoList"></param>
        /// <returns></returns>
        private OperationMessageRelation[] OperationMessageRealtionsFilter(EnumerableRowCollection<OperationMessageRelation> infoList)
        {
            if (infoList != null && infoList.Count() > 0)
            {
                var OperationMessageRelationList = new List<OperationMessageRelation>();
                foreach (var item in infoList)
                {
                    if (string.IsNullOrEmpty(item.XmlMessageInfo))
                    {
                        OperationMessageRelationList.Add(item);
                    }
                    else
                    {
                        var regex = new Regex("<row.*?SendFrom=\"(.*?)\">");
                        var match = regex.Match(item.XmlMessageInfo);
                        if (match.Success && !string.IsNullOrEmpty(match.Groups[1].Value))
                        {
                            OperationMessageRelationList.Add(item);
                        }
                    }
                }
                return OperationMessageRelationList.ToArray();
            }
            else
            {
                return null;
            }
        }


        private bool IsDataListContainsBusinessInfo(DataTable dt, string queryString)
        {
            if (!string.IsNullOrEmpty(queryString) && queryString.Contains("AND"))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    queryString = queryString.Trim().Substring(3, queryString.Trim().Length - 3).Trim();

                    DataRow[] rows = dt.Select(queryString);
                    if (rows != null && rows.Length > 0)
                        return true;
                }
            }
            return false;
        }

        private QueryStringTemplateData GetQueryStringEntity(string templateCode)
        {
            return QueryStringFileLoder.Current[templateCode];
        }

        private DbParameter CreateDBParameter(string parameterName, object value, DbType type)
        {
            DbParameter parameter = CacheOperation.GetParameter(parameterName, type);
            if (value == null || value == DBNull.Value)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = value;
            return parameter;
        }

        /// <summary>
        /// 是否存在操作关联数据
        /// </summary>
        /// <param name="messageRelationID">关联Guid</param>
        /// <param name="oceanBookingID">OperationID</param>
        /// <param name="messageID">Mail MessageID</param>
        /// <returns>关联数据集合:DataTable</returns>
        private DataTable ExsitsOperationMessageRelation(Guid messageRelationID, Guid oceanBookingID, string messageID)
        {
            List<DbParameter> lstParameter = new List<DbParameter>();
            string queryString = string.Format("SELECT * FROM {0} WHERE [ID]=@ID OR ([OperationID]=@OperationID AND [MessageID]=@MessageID)", Table_OperationMessages);
            lstParameter.Add(CreateDBParameter("@ID", messageRelationID, DbType.Guid));
            lstParameter.Add(CreateDBParameter("@OperationID", oceanBookingID, DbType.Guid));
            lstParameter.Add(CreateDBParameter("@MessageID", messageID, DbType.String));
            return CacheOperation.Get(queryString, lstParameter);
        }
        #endregion

        #region Business

        /// <summary>
        /// 获取业务数据(部分列)：ID,OperationID,No,RefNo
        /// </summary>
        /// <returns></returns>
        public DataTable GetConciseOperationViewInfo()
        {
            DataTable dtResult = null;
            try
            {
                //获取一月内创建，一周内更新的业务数据
                dtResult = CacheOperation.Get(string.Format("SELECT {0} FROM {1} AS ovc WHERE ovc.[OPD]='0' ",
                                "ovc.ID,ovc.OceanBookingID,ovc.NO,ovc.RefNO,ovc.BLNo,ovc.OperationType", Table_OperationViewOECache));
                dtResult.PrimaryKey = new[] { dtResult.Columns["ID"] };
            }
            catch (Exception ex)
            {
                dtResult = null;
            }
            return dtResult;
        }

        /// <summary>
        /// 获取所有本地缓存业务数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllOperationViewInfo()
        {
            DataTable dtResult = null;
            try
            {
                //获取一月内创建，一周内更新的业务数据
                dtResult = CacheOperation.Get(string.Format("SELECT  {0} FROM {1} AS ovc WHERE ovc.[CreateDate] > '{2}' OR ovc.[UpdateDate] >'{3}' ",
                                OperationViewSelectField(), Table_OperationViewOECache, DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                dtResult.PrimaryKey = new [] { dtResult.Columns["ID"] };
            }
            catch (Exception ex)
            {
                dtResult = null;
            }
            return dtResult;
        }

        /// <summary>
        /// 通过OperationID、OperationType获取本地缓存业务数据
        /// </summary>
        /// <param name="operationIDs">OperationID集合</param>
        /// <returns></returns>
        public DataTable GetOperationViewInfoByOperationIDs(Guid[] operationIDs)
        {
            DataTable dtResult = null;
            try
            {
                if (operationIDs != null && operationIDs.Length > 0)
                {
                    string queryString = string.Empty;
                    queryString = string.Format("SELECT {0} FROM {1} AS ovc WHERE ovc.OceanBookingID IN({2})",
                                                           OperationViewSelectField(), Table_OperationViewOECache, ConvertIDAsString(operationIDs));

                    dtResult = CacheOperation.Get(queryString);
                }
            }
            catch (Exception ex)
            {
                dtResult = null;
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return dtResult;
        }

        private string OperationViewSelectField()
        {
            return " CAST(0 AS BIT) AS Selected,ovc.ID,ovc.OceanBookingID,ovc.NO,ovc.SONO,ovc.CarrierID,ovc.CarrierCode,ovc.Carrier_EName,ovc.Carrier_CName,ovc.POLID,ovc.POL_EName,ovc.PODID,ovc.POD_EName,ovc.VesselID,ovc.Vessel,ovc.VoyageID,ovc.Voyage,ovc.IsValid,ovc.Type,ovc.CompanyID,ovc.CustomerServiceID,ovc.DocByID,ovc.SOByID,ovc.SalesID,ovc.OPD,ovc.SOPV,ovc.APR,ovc.MBLR,ovc.RefNO,ovc.Description,ovc.MBLCopy,ovc.APCopy,ovc.SOCopy,ovc.BLNO,ovc.ContainerDesc,ovc.ContainerNO,ovc.ContactMail,ovc.ETD,ovc.CreateDate,ovc.UpdateDate,ovc.AdjSOCopy,ovc.OperationID,ovc.OperationType,ovc.ETA,ovc.ANCopy,ovc.ContactMailDomain,ovc.OverSeasFilerID ";
        }

        /// <summary>
        /// 查找业务集合：任务中心
        /// </summary>
        /// <param name="criteria">查询信息实体</param>
        /// <returns></returns>
        public DataTable GetOperationViewList(BusinessQueryCriteria criteria)
        {
            DataTable dtResult = null;
            try
            {
                if (criteria != null)
                {
                    string queryString = string.Empty;
                    QueryStringTemplateData entity = null;

                    entity = GetQueryStringEntity(GetTemplateCode(criteria.SearchType));
                    if (entity != null && !string.IsNullOrEmpty(entity.QueryString))
                    {
                        switch (criteria.SearchType)
                        {
                            case SearchActionType.Auto:
                                queryString = string.Format(entity.QueryString, criteria.EmailAddress, GetIDsAsString(criteria.companyIDs));
                                break;
                            case SearchActionType.KeyWord:
                                queryString = string.Format(entity.QueryString, GetIDsAsString(criteria.companyIDs), criteria.AdvanceQueryString);
                                break;
                            case SearchActionType.MessageRelation:
                                queryString = string.Format(entity.QueryString, GetIDsAsString(criteria.companyIDs), ConvertIDAsString(criteria.OperationIDs));
                                break;
                            case SearchActionType.SubjectInNO:
                                queryString = string.Format(entity.QueryString, GetIDsAsString(criteria.companyIDs), criteria.AdvanceQueryString);
                                break;
                            case SearchActionType.Contact:
                                queryString = string.Format(entity.QueryString, GetIDsAsString(criteria.companyIDs), GetMailDomain(criteria.EmailAddress), criteria.EmailAddress);
                                break;
                            case SearchActionType.Advance:
                                break;
                        }
                        dtResult = CacheOperation.Get(queryString);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
                dtResult = new DataTable("UnknownDataTable");
            }

            return dtResult;
        }

        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        /// <param name="criteria">查询信息实体</param>
        public DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria)
        {
            DataTable dtResult = null;
            try
            {
                string sql = string.Empty;
                switch (criteria.SearchType)
                {
                    case SearchActionType.SubjectInNO:
                        if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
                        {
                            criteria.AdvanceQueryString = criteria.AdvanceQueryString.Replace("|  AND", " OR ");
                            criteria.AdvanceQueryString = string.Format("{0})", criteria.AdvanceQueryString.Insert(5, "("));
                        }
                        sql = @"SELECT CAST(0 AS BIT) AS Selected,SOCopy,MBLCopy,APCopy,ANCopy, ov.BLNO,ov.Description,ov.ContactMail,
                           ov.OceanBookingID,ov.IsValid,ov.NO,ov.RefNO,ov.UpdateDate,ov.OperationType  
                          FROM [{0}] ov WHERE  ov.CompanyID IN ({1}) {2}  Order  by CreateDate DESC";
                        sql = string.Format(sql, Table_OperationViewOECache, GetIDsAsString(criteria.companyIDs), criteria.AdvanceQueryString);
                        break;
                    case SearchActionType.KeyWord:
                        sql = @"SELECT CAST(0 AS BIT) AS Selected,SOCopy,MBLCopy,APCopy,ANCopy, ov.BLNO,ov.Description,ov.ContactMail,
                   ov.OceanBookingID,ov.IsValid,ov.NO,ov.RefNO,ov.UpdateDate,ov.OperationType  FROM [{0}] ov
                          WHERE ov.CompanyID IN ({1})  {2}  Order By ov.CreateDate DESC";
                        sql = string.Format(sql, Table_OperationViewOECache, GetIDsAsString(criteria.companyIDs), criteria.AdvanceQueryString);
                        break;
                    case SearchActionType.MessageRelation:
                        sql = @"SELECT CAST(0 AS BIT) AS Selected,SOCopy,MBLCopy,APCopy,ANCopy, ov.BLNO,ov.Description,ov.ContactMail,
                                ov.OceanBookingID,ov.IsValid,ov.NO,ov.RefNO,ov.UpdateDate,ov.OperationType  
                                FROM [{0}] ov WHERE ov.CompanyID IN ({1}) AND ov.OceanBookingID IN ({2})";
                        sql = string.Format(sql, Table_OperationViewOECache, GetIDsAsString(criteria.companyIDs), ConvertIDAsString(criteria.OperationIDs));
                        break;
                }

                dtResult = CacheOperation.Get(sql);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
                dtResult = null;
            }
            return dtResult;
        }

        /// <summary>
        /// 根据主题单号查找业务集合
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="isNeedSearch"></param>
        /// <returns></returns>
        private DataTable GetOperationListBySubjectInNo(BusinessQueryCriteria criteria, bool isNeedSearch)
        {
            DataTable dt = null;
            try
            {
                if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
                {
                    criteria.AdvanceQueryString = criteria.AdvanceQueryString.Replace("|  AND", " OR ");
                    criteria.AdvanceQueryString = string.Format("{0})", criteria.AdvanceQueryString.Insert(5, "("));
                    dt = GetOperationViewList(criteria);
                }
                else
                {
                    if (isNeedSearch)
                        dt = GetOperationViewList(criteria);
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return dt;
        }

        /// <summary>
        /// 业务信息是否存在
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public bool IsShipmentExists(Guid operationId, OperationType operationType)
        {
            if (operationId == Guid.Empty)
                return false;
            string tableName = GetBusinessTableName(operationType);
            string queryString = string.Format("SELECT ID FROM {0} WHERE OceanBookingID='{1}'", tableName, operationId);
            DataTable dt = CacheOperation.Get(queryString);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 推送业务数据到Outlook
        /// </summary>
        /// <param name="operationIDs">业务ID集合</param>
        public void PushBusinessDataToOutlook(Guid[] operationIDs)
        {
            try
            {
                DataTable dtResult = GetOperationViewInfoByOperationIDs(operationIDs.ToArray());
                if (OutlookService != null && dtResult != null && dtResult.Rows.Count > 0)
                    OutlookService.SearchOperationView(dtResult);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 构建SQL查询条件字符串
        /// </summary>
        /// <param name="Ids">OperationIDs</param>
        /// <returns>字符串：为空时返回公司GUID</returns>
        private string GetIDsAsString(Guid[] Ids)
        {
            if (Ids == null)
                return string.Format("'{0}'", DefaultCompanyId);
            StringBuilder strBuf = new StringBuilder();
            foreach (var guid in Ids)
            {
                strBuf.Append(string.Format("'{0}',", guid));
            }
            return strBuf.ToString(0, strBuf.Length - 1);
        }

        /// <summary>
        /// 构建SQL查询条件字符串
        /// </summary>
        /// <param name="ids">OperationIDs</param>
        /// <returns>为空时空Guid</returns>
        public string ConvertIDAsString(Guid[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return Guid.Empty.ToString();
            StringBuilder strBuf = new StringBuilder();
            foreach (var guid in ids)
            {
                strBuf.Append(string.Format("'{0}',", guid));
            }
            return strBuf.ToString(0, strBuf.Length - 1);
        }
        /// <summary>
        /// 构建SQL查询条件字符串
        /// </summary>
        /// <param name="data">字符串集合</param>
        /// <returns>字符串</returns>
        public string ConvertListToString(List<string> data)
        {
            if (data == null || data.Count <= 0)
                return string.Empty;
            else
            {

                StringBuilder strBuf = new StringBuilder();
                foreach (var item in data)
                {
                    strBuf.Append(string.Format("'{0}',", item));
                }
                return strBuf.ToString(0, strBuf.Length - 1);
            }
        }

        /// <summary>
        /// 获取邮件地址的域名
        /// </summary>
        /// <param name="emailAddress">邮箱地址</param>
        /// <returns>带@符号的邮箱域名：邮箱地址为空或不存在@符号时只返回@</returns>
        private string GetMailDomain(string emailAddress)
        {
            string domain = "@";
            if (string.IsNullOrEmpty(emailAddress) || !emailAddress.Contains("@"))
                return domain;
            string[] arrEmailAddress = emailAddress.Split(new char[] { '@' });
            if (arrEmailAddress != null && arrEmailAddress.Length == 2)
                domain = string.Format("@{0}", arrEmailAddress[arrEmailAddress.Length - 1]);
            return domain;
        }
        /// <summary>
        /// 获取TemplateCode
        /// </summary>
        /// <param name="searchType">查找类型</param>
        /// <returns>TemplateCode格式：自定义列表类型_查询类型</returns>
        private string GetTemplateCode(SearchActionType searchType)
        {
            return string.Format("{0}_{1}", ListFormType.MailLink4in1.ToString(), searchType.ToString());
        }

        /// <summary>
        /// 获取TemplateCode
        /// </summary>
        /// <param name="filter">筛选字符串</param>
        /// <returns>自定义列表类型_传入参数</returns>
        private string GetTemplateCode(string filter)
        {
            return string.Format("{0}_{1}", ListFormType.MailLink4in1.ToString(), filter);
        }
        #endregion

        #region Operation Log

        /// <summary>
        /// 批量保存用户操作日志到数据库
        /// </summary>
        public void UploadUserOperationLog()
        {
            if (_IsUploadUserOperationLog)
                return;
            _IsUploadUserOperationLog = true;
            Stopwatch stopwatchTotaltime = StopwatchHelper.StartStopwatch();
            StringBuilder strLog = new StringBuilder();
            try
            {
                UploadPendingOperationLog();
                strLog.AppendFormat("Upload User Operation:Begin[{0}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                const string strWhere = " 1=1 AND ([F4] IS NULL OR [F4]='False')";
                DataTable dt = GetNeedUploadOperationLog(strWhere);
                if (dt != null && dt.Rows.Count > 0)
                {
                    OperationLogService.BatchAdd(dt);
                    ClearOperationLog(strWhere);
                }
                dt = null;
            }
            catch (Exception ex)
            {
                strLog.AppendFormat("Exception SessionID:{0}", LocalData.SessionId);
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                _IsUploadUserOperationLog = false;
                stopwatchTotaltime.Stop();
                AddOperationLog(Guid.Empty, DateTime.Now, AssamblyName, "UPLOAD-LOG", strLog.ToString()
                    , stopwatchTotaltime.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
            }
        }


        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logID">日志ID</param>
        /// <param name="OperationTime">操作时间</param>
        /// <param name="AssemblyNames">dll类</param>
        /// <param name="ExecuteType">操作类型</param>
        /// <param name="OperationContent">操作内容</param>
        /// <param name="OperationDuration">操作时长[ms](F8)</param>
        public void AddOperationLog(Guid logID, DateTime OperationTime, string AssemblyNames, string ExecuteType, string OperationContent
            , string OperationDuration)
        {
            try
            {
                string sqlString;
                List<DbParameter> parameters = new List<DbParameter>();
                bool isPending = false;
                if (logID == Guid.Empty)
                    logID = Guid.NewGuid();
                else
                    isPending = true;

                DbParameter parameterlogID = CacheOperation.GetParameter("@ID", logID);
                parameters.Add(parameterlogID);


                DbParameter parameterUserId = CacheOperation.GetParameter("@UserID", LocalData.UserInfo.LoginID);
                parameters.Add(parameterUserId);

                DbParameter parameterInternetIP = CacheOperation.GetParameter("@InternetIP", LocalData.UserInfo.PublicIpAddress ?? "");
                parameters.Add(parameterInternetIP);

                DbParameter parameterIntarnetIP = CacheOperation.GetParameter("@IntranetIP", LocalData.UserInfo.LocalIpAddress);
                parameters.Add(parameterIntarnetIP);

                DbParameter parameterMacAddress = CacheOperation.GetParameter("@MacAddress", LocalData.UserInfo.MacAddress);
                parameters.Add(parameterMacAddress);

                DbParameter parameterOperationTime = CacheOperation.GetParameter("@OperationDate", OperationTime);
                parameters.Add(parameterOperationTime);

                DbParameter parameterAssemblyNames = CacheOperation.GetParameter("@AssemblyName", AssemblyNames);
                parameters.Add(parameterAssemblyNames);

                DbParameter parameterExecuteType = CacheOperation.GetParameter("@FunctionName", ExecuteType);
                parameters.Add(parameterExecuteType);

                DbParameter parameterOperationContent = CacheOperation.GetParameter("@OperationContent", OperationContent);
                parameters.Add(parameterOperationContent);

                DbParameter parameterF4 = CacheOperation.GetParameter("@IsPending", isPending);
                parameters.Add(parameterF4);

                DbParameter parameterF6 = CacheOperation.GetParameter("@OperationDuration", OperationDuration);
                parameters.Add(parameterF6);

                sqlString =
                    string.Format(
                        @"insert into {0} (ID,UserID,InternetIP,IntranetIP,MacAddress,OperationDate,AssemblyName,FunctionName,OperationContent,[F4],[F6])
                                  values(@ID,@UserID,@InternetIP,@IntranetIP,@MacAddress,@OperationDate,@AssemblyName,@FunctionName,@OperationContent,@IsPending,@OperationDuration)"
                        , Table_UserOperationLog);

                CacheOperation.ExecuteNonQuery(sqlString, parameters);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(string.Format("Record Log:\r\nLog ID {5}\r\nAssemblyNames:{0}\r\nExecuteType:{1}\r\nOperationContent:{2}\r\nOperationDuration:{3}\r\n Server SessionID:{4}", AssemblyNames, ExecuteType, OperationContent, OperationDuration, LocalData.SessionId, logID));
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logID">日志ID</param>
        /// <param name="F1">操作内容(F1)</param>
        /// <param name="F2">操作内容2(F2)</param>
        /// <param name="F3">操作内容3(F3)</param>
        /// <param name="F4">是否待定(F4)</param>
        /// <param name="F5">F5</param>
        /// <param name="F6">操作时长[ms](F8))</param>
        /// <param name="F7">F7</param>
        /// <param name="F8">F8</param>
        /// <param name="F9">F9</param>
        /// <param name="F10">F10</param>
        public void UpdateOperationLog(Guid logID, string F1 = "", string F2 = "", string F3 = "", bool F4 = false, bool F5 = false, string F6 = "", string F7 = "", string F8 = "", string F9 = "", string F10 = "")
        {
            try
            {
                if (logID == Guid.Empty)
                    return;
                string sqlString;

                sqlString =
                    string.Format(@"UPDATE {0} SET  [F4]  = '{1}',[F5] = '{2}',[F6] = '{3}' {4}{5}{6}{7}{8}{9}{10} WHERE [ID] ='{11}'  "
                    , Table_UserOperationLog
                    , F4, F5, F6
                    , string.IsNullOrEmpty(F1) ? "" : (",[F1]='" + F1 + "'")
                    , string.IsNullOrEmpty(F2) ? "" : (",[F2]='" + F2 + "'")
                    , string.IsNullOrEmpty(F3) ? "" : (",[F3]='" + F3 + "'")
                    , string.IsNullOrEmpty(F7) ? "" : (",[F7]='" + F7 + "'")
                    , string.IsNullOrEmpty(F8) ? "" : (",[F8]='" + F8 + "'")
                    , string.IsNullOrEmpty(F9) ? "" : (",[F9]='" + F9 + "'")
                    , string.IsNullOrEmpty(F10) ? "" : (",[F10]='" + F10 + "'")
                    , logID);

                CacheOperation.ExecuteNonQuery(sqlString);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(string.Format("Update Record Log:\r\\nLog ID:{0}\r\nExecuteDescription:{1}\r\nExecuteDescription2:{2}\r\nExecuteDescription3:{3}\r\nOperationDuration:{4}\r\n Server SessionID:{5}", logID, F1, F2, F3, F6, LocalData.SessionId));
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 获取所有操作日志
        /// </summary>
        public DataTable GetOperationLog()
        {
            DataTable dtResult = null;
            try
            {
                string strExecSql = string.Format("SELECT * FROM {0} ; ", Table_UserOperationLog);
                dtResult = CacheOperation.Get(strExecSql);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return dtResult;
        }

        /// <summary>
        /// 更新UserOperationLog待定(F4=true)记录
        /// </summary>
        private void UploadPendingOperationLog()
        {
            try
            {
                //更新1小时之前待定记录
                string updateString = string.Format("UPDATE {0} SET [F4]='False' WHERE [F4]='True' AND [OperationDate]<'{1}'", Table_UserOperationLog, DateTime.Now.AddHours(-1));
                CacheOperation.ExecuteNonQuery(updateString);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        private DataTable GetNeedUploadOperationLog(string strWhere)
        {
            DataTable dtResult = null;
            try
            {
                const string strColumns = "[ID],[UserID],[InternetIP],[IntranetIP],[MACAddress],[FunctionName],[OperationContent],[OperationDate],[AssemblyName],[F1],[F2],[F3],ISNULL([F4],0) AS F4,[F5],[F6],[F7],[F8],[F9],[F10]";
                string strExecSql = string.Format("SELECT {0} FROM {1} WHERE {2}; ", strColumns, Table_UserOperationLog, strWhere);
                dtResult = CacheOperation.Get(strExecSql);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            return dtResult;
        }

        /// <summary>
        /// 清除所有操作日志
        /// </summary>
        /// <param name="strWhere">Where 条件</param>
        public void ClearOperationLog(string strWhere)
        {
            try
            {
                string strExecSql = string.Format("DELETE FROM {0} WHERE {1} ;", Table_UserOperationLog, strWhere);
                CacheOperation.Get(strExecSql);
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
        }
        #endregion

        #region Other

        /// <summary>
        /// 读取本地文件：关联信息
        /// </summary>
        public void ReadLocalFileOperationMessage()
        {
            try
            {
                if (_IsReadFileOperationMssage)
                    return;
                _IsReadFileOperationMssage = true;
                string messageRelationPath = ICPPathUtility.TempPathMessageRelation();

                DirectoryInfo dir = new DirectoryInfo(messageRelationPath);
                foreach (FileSystemInfo item in dir.GetFileSystemInfos())
                {
                    OperationMessageRelation obj =
                        SerializationUtility.Deserialize4File(item.FullName) as OperationMessageRelation;
                    if (obj != null)
                    {
                        SaveOperationMessageRelation(new[] { obj });
                        obj = null;
                        File.Delete(item.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                _IsReadFileOperationMssage = false;
            }
        }

        /// <summary>
        /// 读取本地文件：操作联系人
        /// </summary>
        public void ReadLocalFileOperationContact()
        {
            try
            {
                if (_IsReadFileOperationContact)
                    return;
                _IsReadFileOperationContact = true;
                string messageRelationPath = ICPPathUtility.TempPathContactMail();
                DirectoryInfo dir = new DirectoryInfo(messageRelationPath);
                foreach (FileSystemInfo item in dir.GetFileSystemInfos())
                {
                    OperationContactParameters obj =
                        SerializationUtility.Deserialize4File(item.FullName) as OperationContactParameters;
                    if (obj != null)
                    {
                        SaveOperationContactMail(new List<OperationContactParameters> { obj });
                        obj = null;
                        File.Delete(item.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                _IsReadFileOperationContact = false;
            }
        }


        /// <summary>
        /// 批量上传邮件备份到服务器
        /// </summary>
        public void UploadMailEntity4Batch()
        {
            if (_IsUploadMail)
                return;
            _IsUploadMail = true;
            //总时间
            Stopwatch stopwatchTotaltime = StopwatchHelper.StartStopwatch();

            int failedCount = 0;
            int notMailCount = 0;
            OperationMessageRelation[] operationMessageRelations = null;
            Guid OperationLogID = Guid.NewGuid();
            string strException = string.Empty;

            int length = 0;
            try
            {
                operationMessageRelations = GetLocalMessageRealtionsByBackupMail();
                if (operationMessageRelations != null && operationMessageRelations.Length > 0)
                {
                    AddOperationLog(OperationLogID, DateTime.Now,
                    AssamblyName, "UPLOAD-MAILENTITY", "Begin Upload BackupMail"
                    , stopwatchTotaltime.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

                    length = operationMessageRelations.Length;
                    int count = 0;
                    const int tempCount = 1; //每次上传一封邮件
                    do
                    {
                        List<OperationMessageRelation> sectionMessageRelations = null;
                        sectionMessageRelations = count == 0 ? operationMessageRelations.Take(tempCount).ToList() : operationMessageRelations.Skip(count).Take(tempCount).ToList();
                        try
                        {
                            #region Transaction Upload Email
                            #region 上传邮件

                            //0.查找邮件
                            byte[] mailbyte = OutlookEntity.GetByteByMessageID(sectionMessageRelations[0].EntryID,
                                sectionMessageRelations[0].MessageId, sectionMessageRelations[0].IMessageId.ToString());

                            //1.构建上传实体并上传邮件
                            if (mailbyte != null && mailbyte.Any())
                            {
                                //1.0 构建上传实体
                                DocumentInfo dInfo = new DocumentInfo();
                                dInfo.Content = mailbyte;
                                dInfo.Id = sectionMessageRelations[0].IMessageId;
                                dInfo.Name = sectionMessageRelations[0].IMessageId + ".msg";
                                dInfo.CreateBy = LocalData.UserInfo.LoginID;
                                dInfo.CreateByName = LocalData.UserInfo.LoginName;

                                //1.1 开始上传
                                //FileService.SaveMailItemToMessageDoc(dInfo);

                                try
                                {
                                    //先上传文件流到服务器
                                    DocumentStream dstream = new DocumentStream();
                                    dstream.Content = new MemoryStream(mailbyte);
                                    dstream.Id = dInfo.Id;
                                    dstream.OperationID = dInfo.Id;
                                    dstream.Name = dInfo.Name;
                                    dstream.IncludePDF = false;
                                    FileServiceWCF.UploadOperationFileByStream(dstream);
                                    //在服务器读取文件流到数据库
                                    FileServiceWCF.SaveMailItemToMessageDoc(dInfo);
                                }
                                catch(Exception ex)
                                {
                                    SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName
                                , LocalData.SessionId, new byte[0]
                                , string.Format("FileServiceWCF邮件备份\r\n{0}\r\nOperationLogID[{1}]\r\nIMessageID[{2}]\r\nLocalVersionNo[{3}]"
                                                    , CommonHelper.BuildExceptionString(ex), OperationLogID, sectionMessageRelations[0].IMessageId
                                                    , LocalData.LocalVersionNo)
                                , DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                                }
                            }
                            else
                            {
                                notMailCount++;
                                ////2.未找到邮件将本地F5设置为True，标记为已上传备份，下次不再上传
                                //sectionMessageRelations.ForEach(item =>
                                //{
                                //    item.BackupMail = true;
                                //});

                                //SaveBackUpMailState(
                                //    sectionMessageRelations.ToArray());
                            }

                            #endregion

                            //2.上传完成后将本地F5设置为True，标记为已上传备份，下次不再上传
                            sectionMessageRelations.ForEach(item =>
                            {
                                item.BackupMail = true;
                            });

                            SaveBackUpMailState(
                                sectionMessageRelations.ToArray());
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            failedCount++;

                            //将保存失败的数据状态值更改为false，以便下次上传
                            sectionMessageRelations.ForEach(item =>
                            {
                                item.BackupMail = false;
                            });
                            SaveBackUpMailState(sectionMessageRelations.ToArray());

                            SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName
                                , LocalData.SessionId, new byte[0]
                                , string.Format("{0}\r\nOperationLogID[{1}]\r\nIMessageID[{2}]\r\nLocalVersionNo[{3}]"
                                                    , CommonHelper.BuildExceptionString(ex), OperationLogID, sectionMessageRelations[0].IMessageId
                                                    , LocalData.LocalVersionNo)
                                , DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                        }
                        finally
                        {
                            sectionMessageRelations = null;
                            count = count + tempCount;
                        }
                    } while (length > count);
                }
            }
            catch (Exception ex)
            {
                strException = string.Format("{0}\r\nOperationLogID:[{1}]\r\nLocalVersionNo:[{2}]", CommonHelper.BuildExceptionString(ex), OperationLogID, LocalData.LocalVersionNo);
                Logger.Log.Error(strException);
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName
                                , LocalData.SessionId, new byte[0], strException, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
            finally
            {
                operationMessageRelations = null;
                stopwatchTotaltime.Stop();
                _IsUploadMail = false;
                if (length > 0)
                {
                    UpdateOperationLog(OperationLogID,
                        string.Format(" Total[{0}],Null[{1}],Failure[{2}]{3}", length, notMailCount, failedCount, (!string.IsNullOrEmpty(strException) || (failedCount > 0) ? " SessionId:" + LocalData.SessionId : ""))
                        , string.Empty, string.Empty, false, false
                        , stopwatchTotaltime.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                }
                stopwatchTotaltime = null;
            }
        }

        /// <summary>
        /// 返回协助同事列表信息
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="date">当前时间</param>
        /// <returns></returns>
        public List<Guid> GetUserAssistsList(Guid userId, DateTime date)
        {
            List<Guid> listResult = new List<Guid>();
            try
            {
                List<UserAssistsType> uatList = OperationViewService.GetUserAssistsList(userId, date);
                listResult.AddRange(uatList.Select(item => item.UserId));
            }
            catch (Exception ex)
            {
                listResult = new List<Guid>();
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
            return listResult;
        }
        #endregion

        #region Comment Code
        //        /// <summary>
        //        /// 记录操作日志
        //        /// </summary>
        //        /// <param name="OperationTime">操作时间</param>
        //        /// <param name="AssemblyNames">dll类</param>
        //        /// <param name="FunctionName">操作内容</param>
        //        /// <param name="Content">操作时长(ms)</param>
        //        public void Add(DateTime OperationTime, string AssemblyNames, string FunctionName, string Content)
        //        {
        //            try
        //            {
        //                string sqlString;
        //                List<DbParameter> parameters = new List<DbParameter>();
        //                DbParameter parameterUserId = CacheOperation.GetParameter("@UserID", LocalData.UserInfo.LoginID);
        //                parameters.Add(parameterUserId);

        //                DbParameter parameterInternetIP = CacheOperation.GetParameter("@InternetIP", LocalData.UserInfo.PublicIpAddress ?? "");
        //                parameters.Add(parameterInternetIP);

        //                DbParameter parameterIntarnetIP = CacheOperation.GetParameter("@IntranetIP",
        //                    LocalData.UserInfo.LocalIpAddress);
        //                parameters.Add(parameterIntarnetIP);

        //                DbParameter parameterMacAddress = CacheOperation.GetParameter("@MacAddress",
        //                    LocalData.UserInfo.MacAddress);
        //                parameters.Add(parameterMacAddress);

        //                DbParameter parameterOperationTime = CacheOperation.GetParameter("@OperationDate", OperationTime);
        //                parameters.Add(parameterOperationTime);

        //                DbParameter parameterAssemblyNames = CacheOperation.GetParameter("@AssemblyName", AssemblyNames);
        //                parameters.Add(parameterAssemblyNames);

        //                DbParameter parameterFunctionName = CacheOperation.GetParameter("@FunctionName", FunctionName);
        //                parameters.Add(parameterFunctionName);

        //                DbParameter parameterContent = CacheOperation.GetParameter("@OperationContent", Content);
        //                parameters.Add(parameterContent);

        //                sqlString =
        //                    string.Format(
        //                        @"insert into {0} (UserID,InternetIP,IntranetIP,MacAddress,OperationDate,AssemblyName,FunctionName,OperationContent)
        //                                  values(@UserID,@InternetIP,@IntranetIP,@MacAddress,@OperationDate,@AssemblyName,@FunctionName,@OperationContent)"
        //                        , Table_UserOperationLog);

        //                CacheOperation.ExecuteNonQuery(sqlString, parameters);
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Log.Info(string.Format("Record Log:\r\nAssemblyNames:{0}\r\nFunctionName:{1}\r\nContent:{2}\r\n Server SessionID:{3}", AssemblyNames, FunctionName, Content, LocalData.SessionId));
        //                Logger.Log.Info(CommonHelper.BuildExceptionString(ex));
        //            }
        //        }
        #endregion
    }
}

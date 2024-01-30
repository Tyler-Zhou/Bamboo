
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Server;


namespace ICP.WF.ServiceComponent
{
    /// <summary>
    /// 工作流配置服务
    /// </summary>
    public class WorkFlowConfigService : IWorkFlowConfigService
    {


        #region 流程配置
        /// <summary>
        /// 获取指定流程的明细信息
        /// </summary>
        public WorkFlowConfigInfo GetWorkFlowConfigInfoByID(Guid id, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkFlowConfigInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }


                WorkFlowConfigInfo info = (from b in ds.Tables[0].AsEnumerable()
                                           orderby b.Field<String>("Version") descending
                                           select new WorkFlowConfigInfo
                                           {
                                               Id = b.Field<Guid>("ID"),
                                               WorkFlowFileContent = b.Field<String>("WorkFlowFileContent"),
                                               RuleFileContent = b.Field<String>("RuleFileContent"),
                                               CDescription = b.Field<String>("CDescription"),
                                               EDescription = b.Field<String>("EDescription"),
                                               CategoryID = b.Field<Guid>("CategoryID"),
                                               CategoryName = b.Field<String>("CategoryName"),
                                               CPrintTitle = b.Field<String>("CPrintTitle"),
                                               EPrintTitle = b.Field<String>("EPrintTitle"),
                                               IsOA = b.Field<Boolean>("IsOA"),
                                               Days = (int)b.Field<Byte>("Days"),
                                               Version = b.Field<String>("Version"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate")

                                           }).FirstOrDefault();

                info.UserPermissions = (from u in ds.Tables[1].AsEnumerable()
                                        select new WorkFlowConfigUserPermissionInfo
                                        {
                                            ID = u.Field<Guid>("ID"),
                                            UserID = u.Field<Guid>("UserID"),
                                            UserName = u.Field<String>("UserName"),
                                            WorkflowConfigID = u.Field<Guid>("WorkflowConfigID"),
                                            UpdateDate = u.Field<DateTime?>("UpdateDate")
                                        }).ToList();


                info.JobPermissions = (from j in ds.Tables[2].AsEnumerable()
                                       select new WorkFlowConfigJobPermissionInfo
                                       {
                                           ID = j.Field<Guid>("ID"),
                                           JobID = j.Field<Guid?>("JobID"),
                                           JobName = j.Field<String>("JobName"),
                                           OrganizationID = j.Field<Guid?>("OrganizationID"),
                                           OrganizationName = j.Field<String>("OrganizationName"),
                                           WorkflowConfigID = j.Field<Guid>("WorkflowConfigID"),
                                           UpdateDate = j.Field<DateTime?>("UpdateDate")
                                       }).ToList();



                return info;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkFlowConfigInfo GetWorkFlowConfigInfoByKey(string key, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkFlowConfigInfoByKey");

                db.AddInParameter(dbCommand, "@Key", DbType.String, key);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                WorkFlowConfigInfo info = (from b in ds.Tables[0].AsEnumerable()
                                           orderby b.Field<String>("Version") descending
                                           select new WorkFlowConfigInfo
                                           {
                                               Id = b.Field<Guid>("ID"),
                                               WorkFlowFileContent = b.Field<String>("WorkFlowFileContent"),
                                               RuleFileContent = b.Field<String>("RuleFileContent"),
                                               CDescription = b.Field<String>("CDescription"),
                                               EDescription = b.Field<String>("EDescription"),
                                               CategoryID = b.Field<Guid>("CategoryID"),
                                               CategoryName = b.Field<String>("CategoryName"),
                                               CPrintTitle = b.Field<String>("CPrintTitle"),
                                               EPrintTitle = b.Field<String>("EPrintTitle"),
                                               IsOA = b.Field<Boolean>("IsOA"),
                                               Days = (int)b.Field<Byte>("Days"),
                                               Version = b.Field<String>("Version"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate")
                                           }).FirstOrDefault();
                return info;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定用户可发起流程列表
        /// </summary>
        public List<WorkFlowConfigInfo> GetWorkFlowConfigList(Guid? userID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkFlowConfigList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WorkFlowConfigInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new WorkFlowConfigInfo
                                                    {
                                                        Id = b.Field<Guid>("ID"),
                                                        CategoryID = b.Field<Guid>("CategoryID"),
                                                        CategoryName = b.Field<String>("CategoryName"),
                                                        CDescription = b.Field<String>("CDescription"),
                                                        EDescription = b.Field<String>("EDescription"),
                                                        CPrintTitle = b.Field<String>("CPrintTitle"),
                                                        EPrintTitle = b.Field<String>("EPrintTitle"),
                                                        IsOA = b.Field<Boolean>("IsOA"),
                                                        Days = (int)b.Field<byte>("Days"),
                                                        KeyWord = b.Field<String>("KeyWord"),
                                                        Version = b.Field<String>("Version"),
                                                        RuleFileContent = b.Field<String>("RuleData"),
                                                        WorkFlowFileContent = b.Field<String>("WorkFlowData"),
                                                        UpdateDate = b.Field<DateTime?>("UpdateDate")
                                                    }).ToList();

                return results;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得指定用户可以发起的流程列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public Byte[] GetWorkFlowConfigListZip(Guid? userID, bool isEnglish)
        {
            List<WorkFlowConfigInfo> list = GetWorkFlowConfigList(userID,isEnglish);

            byte[] zipBytes = DataZipStream.CompressionArrayList(list);

            return zipBytes;
        }


        /// <summary>
        /// 保存指定流程配置
        /// </summary>
        public Guid SaveWorkFlowConfigInfo(
            Guid id,
            Guid categoryID,
            string workflowKey,
            string workFlowFileContent,
            string ruleFileContent,
            string cdesc,
            string edesc,
            string eprinttitle,
            string cprinttitle,
            int days,
            bool? isOA,
            Guid saveByID,
            DateTime? updateDate,
            string cName,
            string eName,
            string version, bool isEnglish
         )
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveWorkFlowConfigInfo");


                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Guid, categoryID);
                db.AddInParameter(dbCommand, "@KeyWord", DbType.String, workflowKey);
                db.AddInParameter(dbCommand, "@CDescription", DbType.String, cdesc);
                db.AddInParameter(dbCommand, "@EDescription", DbType.String, edesc);
                db.AddInParameter(dbCommand, "@CPrintTitle", DbType.String, cprinttitle);
                db.AddInParameter(dbCommand, "@EPrintTitle", DbType.String, eprinttitle);
                db.AddInParameter(dbCommand, "@Days", DbType.Int16, days);
                db.AddInParameter(dbCommand, "@IsOA", DbType.Boolean, isOA);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@WorkFlowData", DbType.String, workFlowFileContent);
                db.AddInParameter(dbCommand, "@RuleData", DbType.String, ruleFileContent);
                db.AddInParameter(dbCommand, "@Version", DbType.String, version);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);


                SingleResultData result = db.SingleResult(dbCommand);
                return result.ID;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除流程配置信息
        /// </summary>
        public void RemoveWorkflowConfigInfo(
            Guid id,
            Guid removeByID, 
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveWorkflowConfigInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                db.ExecuteNonQuery(dbCommand);

            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 检测流程对应KEY的版本是否是大于现在已存在的版本,如果存在返回现在最大的版本
        /// </summary>
        public string GetWorkflowConfigLatestVersion(
            string key,
            string version, 
            bool isEnglish)
        {
            return string.Empty;
        }


        /// <summary>
        /// 设置流程配置的职位权限
        /// </summary>
        public Guid[] SetWorkflowJobPermissionInfo(
            Guid workflowConfigID,
            Guid?[] organizationIDs,
            Guid?[] jobIDs,
            Guid setByID, 
            bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");

            #region 转换
            string oIDList = string.Empty;
            string jIDList = string.Empty;
            Byte userType = 1;
            string dateList = string.Empty;

            ///部门、职位 
            oIDList = organizationIDs.ToArray().Join();
            jIDList = jobIDs.ToArray().Join();


            #endregion

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSetWorkflowPermissionInfo");

                db.AddInParameter(dbCommand, "@WorkflowConfigID", DbType.Guid, workflowConfigID);
                db.AddInParameter(dbCommand, "@OrganizationIDs", DbType.String, oIDList);
                db.AddInParameter(dbCommand, "@UseObjectIDs", DbType.String, jIDList);
                db.AddInParameter(dbCommand, "@UserObjectType", DbType.Byte, userType);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.String, isEnglish);


                SingleResultData result = db.SingleResult(dbCommand);
                return new Guid[0];
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置流程配置的用户权限
        /// </summary>
        public Guid[] SetWorkflowUserPermissionInfo(
           Guid workflowConfigID,
           Guid?[] userIDs,
           Guid setByID,
           bool isEnglish)
        {

            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");



            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSetWorkflowPermissionInfo");

                string uIDList = userIDs.Join();
                byte userType = 2;
                string oIDlist = (new Guid[userIDs.Length]).Join();

                db.AddInParameter(dbCommand, "@WorkflowConfigID", DbType.Guid, workflowConfigID);
                db.AddInParameter(dbCommand, "@OrganizationIDs", DbType.String, oIDlist);
                db.AddInParameter(dbCommand, "@UseObjectIDs", DbType.String, uIDList);
                db.AddInParameter(dbCommand, "@UserObjectType", DbType.Byte, userType);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.String, isEnglish);


                SingleResultData result = db.SingleResult(dbCommand);
                return new Guid[0];
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion


        #region 表单文件配置管理

        /// <summary>
        /// 获取表单文件的配置列表
        /// </summary>
        public List<FormProfileList> GetFormProfileList(
            string name,
            string version, 
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetFormProfileList");

                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@Version", DbType.String, version);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.String, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<FormProfileList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new FormProfileList
                                                 {
                                                     Id = b.Field<Guid>("ID"),
                                                     PorfileContent = b.Field<String>("PorfileContent"),
                                                     ProfileName = b.Field<String>("ProfileName"),
                                                     CName = b.Field<String>("CName"),
                                                     EName = b.Field<String>("EName"),
                                                     Version = b.Field<String>("Version"),
                                                     KeyWord = b.Field<String>("KeyWord")
                                                 }).ToList();


                return results;

            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public byte[] GetGetFormProfileListZip(string name,
            string version,
            bool isEnglish)
        {
            List<FormProfileList> list = GetFormProfileList(name,version,isEnglish);

            byte[] zipBytes = DataZipStream.CompressionArrayList(list);

            return zipBytes;
        }


        /// <summary>
        /// 根据表单名获取对应表单的数据源
        /// </summary>
        public FormProfileInfo GetFormProfileInfo(string name, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetFormProfileInfo");

                db.AddInParameter(dbCommand, "@KeyWord", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                FormProfileInfo result = (from b in ds.Tables[0].AsEnumerable()
                                          select new FormProfileInfo
                                          {
                                              Id = b.Field<Guid>("ID"),
                                              DataSchame = b.Field<String>("DataScheme"),
                                              ProfileName = b.Field<String>("ProfileName"),
                                              PorfileContent = b.Field<String>("PorfileContent"),
                                              Version = b.Field<String>("Version"),
                                          }).SingleOrDefault();

                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 保存表单配置文件
        /// </summary>
        /// <param name="id">如果为null则新增加,否则修改对应的表单数据</param>
        /// <param name="cName">中文名称</param>
        /// <param name="eName">英文名称</param>
        /// <param name="formData">表单文件</param>
        /// <param name="dataScheme">数据源文件</param>
        /// <param name="version">版本</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <returns></returns>
        public SingleResultData SaveFormProfile(
            Guid? id,
            string key,
            string cName,
            string eName,
            string formData,
            string dataScheme,
            string version,
            Guid saveByID,
            DateTime? updateDate,
            bool isEnglish
            )
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveFormProfileInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@KeyWord", DbType.String, key);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@FormData", DbType.String, formData);
                db.AddInParameter(dbCommand, "@DataScheme", DbType.String, dataScheme);
                db.AddInParameter(dbCommand, "@Version", DbType.String, version);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 删除表单配置文件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">ID</param>
        public void RemoveFormProfile(
            Guid id,
            Guid removeByID, 
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveFormProfile");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                db.ExecuteNonQuery(dbCommand);
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}

//-----------------------------------------------------------------------
// <copyright file="JobService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Sys.ServiceComponent
{


    /// <summary>
    /// 职位管理服务 
    /// </summary>
    public class JobService : IJobService
    {
        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;
        public JobService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <param name="code">中文名称</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回列表对象</returns>
        public List<JobList> GetJobList(
            string code,
            string name,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetJobList");

                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@Name", DbType.String, name);
                db.AddInParameter(command, "@IsEnglish", DbType.Byte, this.IsEnglish);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<JobList>();
                }

                List<JobList> results = (from b in ds.Tables[0].AsEnumerable()
                                         select new JobList
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             Code = b.Field<string>("Code"),
                                             CName = b.Field<string>("CName"),
                                             EName = b.Field<string>("EName"),
                                             Description = b.Field<string>("Description"),
                                             ParentID = b.Field<Guid?>("ParentID"),
                                             IsValid = b.Field<bool>("IsValid"),
                                             CreateBy = b.Field<string>("CreateBy"),
                                             CreateDate = b.Field<DateTime>("CreateDate"),
                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                             HierarchyCode = b.Field<string>("HierarchyCode")
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
        /// 获取信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回编辑信息</returns>
        public JobInfo GetJobInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetJobInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                JobInfo result = (from b in ds.Tables[0].AsEnumerable()
                                  select new JobInfo
                                  {
                                      ID = b.Field<Guid>("ID"),
                                      Code = b.Field<string>("Code"),
                                      CName = b.Field<string>("CName"),
                                      EName = b.Field<string>("EName"),
                                      Description = b.Field<string>("Description"),
                                      ParentID = b.Field<Guid?>("ParentID"),
                                      ParentName = b.Field<string>("ParentName"),
                                      IsValid = b.Field<bool>("IsValid"),
                                      CreateById = b.Field<Guid>("CreateByID"),
                                      CreateBy = b.Field<string>("CreateBy"),
                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                      HierarchyCode = b.Field<string>("HierarchyCode")
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
        /// 获取组织结构下岗位列表
        /// </summary>
        /// <param name="organizationID">组织结构</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回组织结构下岗位列表</returns>
        public List<Organization2JobList> GetOrganization2JobList(
            Guid? organizationID,
            bool? isValid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetOrganizationJobList");

                db.AddInParameter(dbCommand, "@OrganizationID", DbType.Guid, organizationID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<Organization2JobList>();
                }

                List<Organization2JobList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new Organization2JobList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          Code = b.Field<string>("Code"),
                                                          Name =this.IsEnglish? b.Field<String>("EName"):b.Field<String>("CName"),
                                                          RelationID = b.Field<Guid>("RelationID"),
                                                          Type = (OrganizationJobType)(short)b.Field<byte>("Type"),
                                                          ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                          ParentName = b.Field<String>("ParentName"),
                                                          CreateByName = b.Field<String>("CreateBy"),
                                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                          StructureFullName = b.Field<string>("FullName"),
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
        /// 删除组织结构下岗位信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间</param>
        public void RemovetOrganization2JobInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspRemoveOrganizationJob");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
        /// 设置组织结构下岗位信息
        /// </summary>
        /// <param name="organizationID">组织结构ID</param>
        /// <param name="ids">ID</param>
        /// <param name="jobIds">岗位列表</param>
        /// <param name="updateDates">更新时间</param>
        /// <param name="setByID">设置人</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetOrganization2JobInfo(
            Guid organizationID,
            Guid?[] ids,
            Guid[] jobIds,
            DateTime?[] updateDates,
            Guid setByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(organizationID, "organizationID");
            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");
            ArgumentHelper.AssertGuidNotEmpty(jobIds, "jobIds");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspSetOrganizationJob");

                string tempIds = ids.Join();
                string tempJobIds = jobIds.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@OrganizationID", DbType.Guid, organizationID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@JobIDs", DbType.String, tempJobIds);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData results = db.ManyResult(dbCommand);
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
        /// 保存信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="parentId">parentId==null或则parentId==Guid.Empty该职位为顶级节点,否则为对应父结点的子节点</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="saveById">修改人Id</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SaveJobInfo(
            Guid? id,
            Guid? parentId,
            string code,
            string cname,
            string ename,
            string description,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            ArgumentHelper.AssertStringNotEmpty(cname, "cname");
            ArgumentHelper.AssertStringNotEmpty(ename, "ename");
            ArgumentHelper.AssertStringNotEmpty(code, "code");

            try
            {
                Guid tempParentID = parentId.ToGuid();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveJobInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@ParentID", DbType.Guid, tempParentID);
                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@CName", DbType.String, cname);
                db.AddInParameter(command, "@EName", DbType.String, ename);
                db.AddInParameter(command, "@Description", DbType.String, description);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, false);

                ManyHierarchyResultData result = db.ManyHierarchyResult(command, Guid.Empty);
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
        /// 改变数据状态
        /// </summary>
        /// <param name="id">对应唯一键值</param>
        /// <param name="isValid">是否有效状态</param>
        /// <param name="changeById">修改人</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData ChangeJobState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangeJobState");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@ChangeByID", DbType.Guid, changeById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, false);

                ManyResultData result = db.ManyResult(command);
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
        /// 设置职位的父结点
        /// </summary>
        /// <param name="id">当前结点</param>
        /// <param name="parentId">如果parentId==null 或则parentId==Guid.Empt则,当前结点为顶级节点,否则设置为parentId的子节点</param>
        /// <param name="setById">设置人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SetParentJob(
            Guid id,
            Guid? parentId,
            Guid setById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetParentJob");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@ParentID", DbType.Guid, parentId);
                db.AddInParameter(command, "@SetByID", DbType.Guid, setById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyHierarchyResultData result = db.ManyHierarchyResult(command, Guid.Empty);
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
        /// 获取角色下所挂岗位列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>返回角色下所挂岗位列表</returns>
        public List<Role2OrganizationJobList> GetRole2OrganizationJobList(Guid organizationJobID)
        {
            ArgumentHelper.AssertGuidNotEmpty(organizationJobID, "organizationJobID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetOrganizationJobRoleList");

                db.AddInParameter(dbCommand, "@OrganizationJobID", DbType.Guid, organizationJobID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return new List<Role2OrganizationJobList>();

                List<Role2OrganizationJobList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new Role2OrganizationJobList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              RoleID = b.Field<Guid>("RoleID"),
                                                              RoleName = b.Field<String>("RoleName"),
                                                              OrganizationJobID = b.Field<Guid>("OrganizationJobID"),
                                                              OrganizationJobName = b.Field<String>("OrganizationJobName"),
                                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 保存角色岗位信息
        /// </summary>
        /// <param name="organizationJobID">角色ID</param>
        /// <param name="ids">角色岗位ID</param>
        /// <param name="roleIDs">岗位ID</param>
        /// <param name="updateDates">更新时间</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetOrganizationJob2RoleInfo(
            Guid organizationJobID,
            Guid?[] ids,
            Guid[] roleIDs,
            DateTime?[] updateDates,
            Guid setByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(organizationJobID, "organizationJobID");
            ArgumentHelper.AssertGuidNotEmpty(roleIDs, "roleIDs");

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspSetOrganizationJobRole");

                string tempIds = ids.Join();
                string temproleIDs = roleIDs.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@OrganizationJobID", DbType.Guid, organizationJobID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RoleIDs", DbType.String, temproleIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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
        /// 删除角色岗位信息
        /// </summary>
        /// <param name="ids">角色岗位ID</param>
        /// <param name="updateDates">更新时间</param>
        /// <param name="removeByID">删除人</param>
        public void RemoveOrganizationJob2RoleInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspRemoveRoleOrganizationJob");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

    }
}

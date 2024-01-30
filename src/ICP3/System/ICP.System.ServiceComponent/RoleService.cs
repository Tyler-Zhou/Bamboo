//-----------------------------------------------------------------------
// <copyright file="RoleService.cs" company="LongWin">
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
    /// 角色管理服务 
    /// </summary>
    public class RoleService : IRoleService
    {

        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;
        public RoleService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
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
        /// 获取角色列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回角色列表对象</returns>
        public List<RoleList> GetRoleList(
            string name,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetRoleList");

                db.AddInParameter(command, "@Name", DbType.String, name);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<RoleList>();
                }

                List<RoleList> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new RoleList
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              CName = b.Field<string>("CName"),
                                              EName = b.Field<string>("EName"),
                                              Description = b.Field<string>("Description"),
                                              IsValid = b.Field<bool>("IsValid"),
                                              CreateByName = b.Field<string>("CreateBy"),
                                              CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取角色信息
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>返回角色编辑信息</returns>
        public RoleInfo GetRoleInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetRoleInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                RoleInfo result = (from b in ds.Tables[0].AsEnumerable()
                                   select new RoleInfo
                                   {
                                       ID = b.Field<Guid>("ID"),
                                       CName = b.Field<string>("CName"),
                                       EName = b.Field<string>("EName"),
                                       Description = b.Field<string>("Description"),
                                       IsValid = b.Field<bool>("IsValid"),
                                       CreateById = b.Field<Guid>("CreateByID"),
                                       CreateByName = b.Field<string>("CreateBy"),
                                       CreateDate = b.Field<DateTime>("CreateDate"),
                                       UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 保存角色信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增角色数据,否则修改对应键的角色信息</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="saveById">修改人</param>
        /// <param name="updateDate">版本(更新时间)</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveRoleInfo(
            Guid? id,
            string cname,
            string ename,
            string description,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            ArgumentHelper.AssertStringNotEmpty(cname, "cname");
            ArgumentHelper.AssertStringNotEmpty(ename, "ename");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveRoleInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@CName", DbType.String, cname);
                db.AddInParameter(command, "@EName", DbType.String, ename);
                db.AddInParameter(command, "@Description", DbType.String, description);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
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
        /// 改变角色数据状态
        /// </summary>
        /// <param name="id">对应角色唯一键值</param>
        /// <param name="isValid">是否有效状态</param>
        /// <param name="changeById">修改人</param>
        /// <param name="updateDate">版本(更新时间)</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeRoleState(
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
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangeRoleState");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@ChangeByID", DbType.Guid, changeById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, false);

                SingleResultData result = db.SingleResult(command);
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
        public List<Role2OrganizationJobList> GetRole2OrganizationJobList(Guid roleID)
        {
            ArgumentHelper.AssertGuidNotEmpty(roleID, "roleID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetRoleOrganizationJobList");

                db.AddInParameter(dbCommand, "@RoleID", DbType.Guid, roleID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return new List<Role2OrganizationJobList>();

                List<Role2OrganizationJobList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new Role2OrganizationJobList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              RoleID = roleID,
                                                              OrganizationJobID = b.Field<Guid>("OrganizationJobID"),
                                                              OrganizationJobName = b.Field<String>("OrganizationJobName"),
                                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                              CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified),
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
        /// <param name="roleID">角色ID</param>
        /// <param name="ids">角色岗位ID</param>
        /// <param name="organizationJobIDs">岗位ID</param>
        /// <param name="updateDates">更新时间</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetRole2OrganizationJobInfo(
            Guid roleID,
            Guid?[] ids,
            Guid[] organizationJobIDs,
            Guid setByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(roleID, "roleID");
            ArgumentHelper.AssertGuidNotEmpty(organizationJobIDs, "organizationJobIDs");

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspSetRoleOrganizationJob");

                string tempIds = ids.Join();
                string tempOrganizationJobIDs = organizationJobIDs.Join();

                db.AddInParameter(dbCommand, "@RoleID", DbType.Guid, roleID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@OrganizationJobIDs", DbType.String, tempOrganizationJobIDs);
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
        public void RemoveRole2OrganizationJobInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");


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


        /// <summary>
        /// 获取功能或则动作的角色和用户权限列表
        /// </summary>
        /// <param name="id">功能或则动作ID</param>
        /// <returns>返回动作的角色和用户权限列表</returns>
        public List<RolePermissionList> GetRolePermissionListByRoleID(Guid roleID)
        {
            ArgumentHelper.AssertGuidNotEmpty(roleID, "roleID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetRolePermissionList");

                db.AddInParameter(command, "@RoleID", DbType.Guid, roleID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<RolePermissionList> rolePermissionList = (from b in ds.Tables[0].AsEnumerable()
                                                               select new RolePermissionList
                                                               {
                                                                   ID = b.Field<Guid>("ID"),
                                                                   PermissionID = b.Field<Guid>("PermissionID"),
                                                                   PermissionName = b.Field<string>("PermissionName"),
                                                                   RoleID = b.Field<Guid>("RoleID"),
                                                                   RoleName = b.Field<string>("RoleName"),
                                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                               }).ToList();

                return rolePermissionList;
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

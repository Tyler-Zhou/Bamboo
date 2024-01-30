//-----------------------------------------------------------------------
// <copyright file="PermissionService.cs" company="LongWin">
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
    /// 菜单配置管理服务
    /// </summary>
    public class PermissionService : IPermissionService
    {
       
        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;
        public PermissionService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
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
        /// 获取功能项列表
        /// </summary>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回功能项列表</returns>
        public List<FunctionList> GetFunctionList(bool? isValid)
        {
            return this.GetFunctionList(null, null, isValid);
        }

        /// <summary>
        /// 获取角色有权访问的功能列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回角色有权访问的功能列表</returns>
        public List<FunctionList> GetRoleFunctionList(
            Guid roleId,
            bool? isValid)
        {
            return this.GetFunctionList(roleId, null, isValid);
        }

        /// <summary>
        /// 获取用户有权访问的功能列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回角色有权访问的功能列表</returns>
        public List<FunctionList> GetUserFunctionList(
            Guid userId,
            bool? isValid)
        {
            List < FunctionList > functions = this.GetFunctionList(null, userId, isValid);
            functions = functions.Where<FunctionList>(f => f.FunctionType != FunctionType.Module).ToList();
            return functions;
        }

        /// <summary>
        /// 获取有权限访问该功能模块的用户列表

        /// </summary>
        /// <param name="moduleCode">命令代码</param>
        /// <param name="organizationID">用户所在的组织结构</param>
        /// <param name="maxRecords">最大记录数量</param>
        /// <returns></returns>
        public List<ModuleUserList> GetModuleUserList(
            string commandName,
            Guid? organizationID,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetModuleUserList");

                db.AddInParameter(command, "@CommandName", DbType.String, commandName);
                db.AddInParameter(command, "@OrganizationID", DbType.Guid, organizationID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<ModuleUserList>();
                }

                List<ModuleUserList> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new ModuleUserList
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              Code = b.Field<string>("Code"),
                                              CName = b.Field<string>("CName"),
                                              EName = b.Field<string>("EName"),
                                              Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                                              EMail = b.Field<string>("Email"),
                                              Tel = b.Field<string>("Tel"),
                                              Fax = b.Field<string>("Fax"),
                                              Mobile = b.Field<string>("Mobile"),
                                              IsValid = b.Field<bool>("IsValid"),
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
        /// 获取UI配置项列表

        /// </summary>
        /// <param name="siteType">容器类型</param>
        /// <returns>返回UI配置列表</returns>
        public List<UIItemList> GetUIConfigurationList(SiteType siteType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUIConfigList");

                db.AddInParameter(command, "@SiteType", DbType.Int16, (short)siteType);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, true);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<UIItemList> permissionList = (from b in ds.Tables[0].AsEnumerable()
                                                   where b.Field<byte>("FunctionType") < 3
                                                   select new UIItemList
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       Code = b.Field<string>("Code"),
                                                       CName = b.Field<string>("CName"),
                                                       EName = b.Field<string>("EName"),
                                                       HierarchyCode = b.Field<string>("HierarchyCode"),
                                                       ParentName = b.Field<string>("ParentName"),
                                                       ParentID = b.IsNull("ParentID") ? (Guid?)null : b.Field<Guid>("ParentID"),
                                                       FunctionID = b.IsNull("FunctionID") ? (Guid?)null : b.Field<Guid>("FunctionID"),
                                                       FunctionType = (UIConfigItemType)b.Field<byte>("FunctionType"),
                                                       CreateDate = b.Field<DateTime>("CreateDate"),
                                                       UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                   }).ToList();

                return permissionList;
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
        /// 获取UI配置项详细信息

        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回UI配置项详细信息</returns>
        public UIItemInfo GetUIConfigurationInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUIConfigInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                UIItemInfo result = (from b in ds.Tables[0].AsEnumerable()
                                     select new UIItemInfo
                                     {
                                         ID = b.Field<Guid>("ID"),
                                         FunctionID = b.IsNull("FunctionID") ? (Guid?)null : b.Field<Guid>("FunctionID"),
                                         Code = b.Field<string>("Code"),
                                         CName = b.Field<string>("CName"),
                                         EName = b.Field<string>("EName"),
                                         Description = b.Field<string>("Description"),
                                         FunctionType = (UIConfigItemType)b.Field<byte>("FunctionType"),
                                         CommandName = b.Field<string>("CommandName"),
                                         CreateByID = b.Field<Guid>("CreateByID"),
                                         CreateByName = b.Field<string>("CreateByName"),
                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                         ParentID = b.IsNull("ParentID") ? (Guid?)null : b.Field<Guid>("ParentID"),
                                         ParentName = b.Field<string>("ParentName"),
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
        /// 保存UI配置项信息

        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="parentId">父节点Id</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="functionId">功能Id</param>
        /// <param name="saveById">保存人Id</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        public SingleHierarchyResultData SaveUIConfigurationInfo(
            Guid? id,
            Guid? parentId,
            string code,
            string cname,
            string ename,
            string description,
            Guid? functionId,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(cname, "cname");
            ArgumentHelper.AssertStringNotEmpty(ename, "ename");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveUIConfigInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@ParentID", DbType.Guid, parentId);
                db.AddInParameter(command, "@FunctionID", DbType.Guid, functionId);
                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@CName", DbType.String, cname);
                db.AddInParameter(command, "@EName", DbType.String, ename);
                db.AddInParameter(command, "@Description", DbType.String, description);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleHierarchyResultData result = db.SingleHierarchyResult(command);

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
        /// 删除UI配置项
        /// </summary>
        /// <param name="id">UI配置项ID</param>
        /// <param name="removeByID">删除任ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        public void RemoveUIConfigurationInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspRemoveUIConfigInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@RemoveByID", DbType.Guid, removeByID);

                db.ExecuteNonQuery(command);
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
        /// 设置UI配置项的位置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="preID">兄弟前一节点</param>
        /// <param name="setByID">设置人ID</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SetUIConfigurationPosition(
            Guid id,
            Guid? parentID,
            Guid? preID,
            Guid setByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspSetUIConfigPosition");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@PreID", DbType.Guid, preID);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyHierarchyResultData result = db.ManyHierarchyResult(dbCommand,id);
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
        /// 获取功能或则动作的角色权限列表
        /// </summary>
        /// <param name="permissionId">功能或则动作ID</param>
        /// <returns>返回动作的角色和用户权限列表</returns>
        public List<RolePermissionList> GetRolePermissionList(Guid permissionId)
        {
            ArgumentHelper.AssertGuidNotEmpty(permissionId, "permissionId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetRoleAndUserPermissionList");

                db.AddInParameter(command, "@ID", DbType.Guid, permissionId);
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

        /// <summary>
        /// 获取功能或则动作的角色和用户权限列表
        /// </summary>
        /// <param name="id">功能或则动作ID</param>
        /// <returns>返回动作的用户权限列表</returns>
        public List<UserPermissionList> GetUserPermissionList(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetRoleAndUserPermissionList");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<UserPermissionList> userPermissionList = (from b in ds.Tables[1].AsEnumerable()
                                                               select new UserPermissionList
                                                               {
                                                                   ID = b.Field<Guid>("ID"),
                                                                   PermissionID = b.Field<Guid>("PermissionID"),
                                                                   UserID = b.Field<Guid>("UserID"),
                                                                   UserName = b.Field<string>("UserName"),
                                                                   OrganizationID = b.Field<Guid>("OrganizationID"),
                                                                   OrganizationName = b.Field<string>("OrganizationName"),
                                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                               }).ToList();

                return userPermissionList;
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
        /// 设置功能或则动作角色权限
        /// </summary>
        /// <param name="permissionId">Action ID</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <param name="saveById">保存人ID</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetActionRoles(
            Guid permissionId,
            Guid[] roleIds,
            Guid saveById)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
           // ArgumentHelper.AssertGuidNotEmpty(roleIds, "roleIds");

            string tempRoleIDs = roleIds.Join();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetFunctionRoles");

                db.AddInParameter(command, "@FunctionID", DbType.Guid, permissionId);
                db.AddInParameter(command, "@RoleIDs", DbType.String, tempRoleIDs);
                db.AddInParameter(command, "@SetByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@IsAction", DbType.Boolean, true);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
        /// 设置功能角色权限
        /// </summary>
        /// <param name="permissionRoleIds">角色功能关系ID列表</param>
        /// <param name="permissionIds">功能ID列表</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <param name="saveById">保存人ID</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetFunctionRoles(
            Guid permissionId,
            Guid[] roleIds,
            Guid saveById)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            //ArgumentHelper.AssertGuidNotEmpty(roleIds, "roleIds");

            string tempRoleIDs = roleIds.Join();
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetFunctionRoles");

                db.AddInParameter(command, "@FunctionID", DbType.Guid, permissionId);
                db.AddInParameter(command, "@RoleIDs", DbType.String, tempRoleIDs);
                db.AddInParameter(command, "@SetByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@IsAction", DbType.Boolean, false);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
        /// 设置功能角色权限
        /// </summary>
        /// <param name="roleID">功能ID列表</param>
        /// <param name="functionIDs">角色ID列表</param>
        /// <param name="isActions">是否Action列表</param>
        /// <param name="saveById">保存人ID</param>
        /// <returns>返回ManyResultData</returns>
        public  ManyResultData SetRoleFunctions(
            Guid roleID,
            Guid[] functionIDs,
            bool[] isActions,
            Guid saveById)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            string tempfunctionIDs = functionIDs.Join();
            string tempIsActions = isActions.Join();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetRoleFunctions");

                db.AddInParameter(command, "@RoleID", DbType.Guid, roleID);
                db.AddInParameter(command, "@FunctionIDs", DbType.String, tempfunctionIDs);
                db.AddInParameter(command, "@SetByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@IsActions", DbType.String, tempIsActions);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
        /// 删除功能项角色权限
        /// </summary>
        /// <param name="permissionRoleIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        public void RemoveFunctionRolePermission(
            Guid[] permissionRoleIds,
            Guid removeById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertArrayLengthMatch(permissionRoleIds, updateDates);
            ArgumentHelper.AssertGuidNotEmpty(permissionRoleIds, "permissionRoleIds");
            ArgumentHelper.AssertGuidNotEmpty(removeById, "removeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspRemoveRolePermission");

                string tempPermissionRoleIds = permissionRoleIds.Join();
                string tempupdateDate = updateDates.Join();

                db.AddInParameter(command, "@IDs", DbType.String, tempPermissionRoleIds);
                db.AddInParameter(command, "@RemoveByID", DbType.Guid, removeById);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempupdateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
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
        /// 删除功能项角色权限
        /// </summary>
        /// <param name="permissionRoleIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        public void RemoveActionRolePermission(
            Guid[] permissionRoleIds,
            Guid removeById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertArrayLengthMatch(permissionRoleIds, updateDates);
            ArgumentHelper.AssertGuidNotEmpty(permissionRoleIds, "permissionRoleIds");
            ArgumentHelper.AssertGuidNotEmpty(removeById, "removeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspRemoveRolePermission");

                string tempPermissionRoleIds = permissionRoleIds.Join();
                string tempupdateDate = updateDates.Join();

                db.AddInParameter(command, "@IDs", DbType.String, tempPermissionRoleIds);
                db.AddInParameter(command, "@RemoveByID", DbType.Guid, removeById);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempupdateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
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
        /// 设置功能或则动作特殊用户权限
        /// </summary>
        /// <param name="permissionUserIds">关联关系ID</param>
        /// <param name="permissionIds">功能或则动作ID</param>
        /// <param name="userIds">用户列表</param>
        /// <param name="orangnizationIds">组织节点列表</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDates">版本控制</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetFunctionUserPermission(
            Guid?[] permissionUserIds,
            Guid[] permissionIds,
            Guid[] userIds,
            Guid[] orangnizationIds,
            Guid saveById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(userIds, "userIds");
            ArgumentHelper.AssertGuidNotEmpty(orangnizationIds, "orangnizationIds");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            ArgumentHelper.AssertArrayLengthMatch(permissionUserIds, permissionIds, userIds, orangnizationIds, updateDates);

            string tempPermissionUserIds = permissionUserIds.Join();
            string tempPermissionIds = permissionIds.Join();
            string tempUserIDs = userIds.Join();
            string tempOrganizationIDs = orangnizationIds.Join();
            string tempupdateDate = updateDates.Join();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetUserSpecialPermission");

                db.AddInParameter(command, "@IDs", DbType.String, tempPermissionUserIds);
                db.AddInParameter(command, "@PermissionIDs", DbType.String, tempPermissionIds);
                db.AddInParameter(command, "@UserIDs", DbType.String, tempUserIDs);
                db.AddInParameter(command, "@OrganizationIDs", DbType.String, tempOrganizationIDs);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempupdateDate);
                db.AddInParameter(command, "@SetByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@IsAction", DbType.Boolean, false);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
        /// 设置功能或则动作特殊用户权限
        /// </summary>
        /// <param name="permissionUserIds">关联关系ID</param>
        /// <param name="permissionIds">动作ID列表</param>
        /// <param name="userIds">用户列表</param>
        /// <param name="orangnizationIds">组织节点列表</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDates">版本控制</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetActionUserPermission(
            Guid?[] permissionUserIds,
            Guid[] permissionIds,
            Guid[] userIds,
            Guid[] orangnizationIds,
            Guid saveById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(userIds, "userIds");
            ArgumentHelper.AssertGuidNotEmpty(orangnizationIds, "orangnizationIds");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            ArgumentHelper.AssertArrayLengthMatch(permissionUserIds, userIds, orangnizationIds, updateDates);

            string tempPermissionUserIds = permissionUserIds.Join();
            string tempUserIDs = userIds.Join();
            string tempOrganizationIDs = orangnizationIds.Join();
            string tempPermissionIds = permissionIds.Join();
            string tempupdateDate = updateDates.Join();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetUserSpecialPermission");

                db.AddInParameter(command, "@IDs", DbType.String, tempPermissionUserIds);
                db.AddInParameter(command, "@PermissionIDs", DbType.String, tempPermissionIds);
                db.AddInParameter(command, "@UserIDs", DbType.String, tempUserIDs);
                db.AddInParameter(command, "@OrganizationIDs", DbType.String, tempOrganizationIDs);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempupdateDate);
                db.AddInParameter(command, "@SetByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@IsAction", DbType.Boolean, true);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
        /// 删除动作项用户权限
        /// </summary>
        /// <param name="permissionUserIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        public void RemoveActionUserPermission(
            Guid[] permissionUserIds,
            Guid removeById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(permissionUserIds, "permissionUserIds");
            ArgumentHelper.AssertGuidNotEmpty(removeById, "removeById");
            ArgumentHelper.AssertArrayLengthMatch(permissionUserIds, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspRemoveUserSpecialPermission");

                string tempPermissionUserIds = permissionUserIds.Join();
                string tempupdateDate = updateDates.Join();

                db.AddInParameter(command, "@IDs", DbType.String, tempPermissionUserIds);
                db.AddInParameter(command, "@RemoveByID", DbType.Guid, removeById);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempupdateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
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
        /// 删除功能项项用户权限
        /// </summary>
        /// <param name="permissionUserIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        public void RemoveFunctionUserPermission(
            Guid[] permissionUserIds,
            Guid removeById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(permissionUserIds, "permissionUserIds");
            ArgumentHelper.AssertGuidNotEmpty(removeById, "removeById");
            ArgumentHelper.AssertArrayLengthMatch(permissionUserIds, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspRemoveUserSpecialPermission");

                string tempPermissionUserIds = permissionUserIds.Join();
                string tempupdateDate = updateDates.Join();

                db.AddInParameter(command, "@IDs", DbType.String, tempPermissionUserIds);
                db.AddInParameter(command, "@RemoveByID", DbType.Guid, removeById);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempupdateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
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
        /// 获取指定用户对某功能的操作范围
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="commandName">命令名</param>
        /// <param name="permissionRange">权限范围</param>
        /// <returns>返回用户对某功能的操作范围</returns>
        public List<OrganizationList> GetPermissionOrganizationList(
            Guid userId,
            string commandName,
            PermissionRangeType permissionRange)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");
            ArgumentHelper.AssertStringNotEmpty(commandName, "commandName");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetPermissionOrganizationList");

                db.AddInParameter(command, "@UserID", DbType.Guid, userId);
                db.AddInParameter(command, "@CommandName", DbType.String, commandName);
                db.AddInParameter(command, "@Type", DbType.Byte, (short)permissionRange);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<OrganizationList>();
                }

                List<OrganizationList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new OrganizationList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
                                                      CShortName = b.Field<string>("CName"),
                                                      EShortName = b.Field<string>("EName"),
                                                      Type = (OrganizationType)b.Field<short>("Type"),
                                                      ParentID =  b.Field<Guid?>("ParentID"),
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
        /// 获取角色有权访问的功能列表

        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回角色有权访问的功能列表</returns>
        private List<FunctionList> GetFunctionList(
            Guid? roleId,
            Guid? userId,
            bool? isValid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetFunctionList");

                db.AddInParameter(command, "@RoleId", DbType.Guid, roleId);
                db.AddInParameter(command, "@UserId", DbType.Guid, userId);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<FunctionList>();
                }

                List<FunctionList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new FunctionList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  Code = b.Field<string>("Code"),
                                                  CName = b.Field<string>("CName"),
                                                  FunctionType = (FunctionType)(short)b.Field<byte>("FunctionType"),
                                                  ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                  EName = b.Field<string>("EName"),
                                                  CommandName = b.Field<string>("CommandName"),
                                                  Description = b.Field<string>("Description"),
                                                  Enable = b.Field<bool>("Enable"),
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

        #region WorkSpace
        /// <summary>
        /// 得到WorkSpace列表
        /// </summary>
        /// <param name="workSpaceID"></param>
        /// <returns></returns>
        public List<WorkSpaceList> GetWorkSpaceList()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspGetWorkSpaceList");

                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<WorkSpaceList>();
                }

                List<WorkSpaceList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new WorkSpaceList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   Code = b.Field<string>("Code"),
                                                   CName = b.Field<string>("CName"),
                                                   EName=b.Field<string>("EName"),
                                                   CreateBy=b.Field<Guid>("CreateBy"),
                                                   CreateByName=b.Field<string>("CreateByName"),
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
        public SingleResultData SaveWorkSpaceInfo(Guid id, string code, string cName, string eName, Guid saveByID, DateTime? updatedate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspSaveWorkSpaceInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@CName", DbType.String, cName);
                db.AddInParameter(command, "@EName", DbType.String, eName);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updatedate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

               return db.SingleResult(command);
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
        /// 得到OperationViewList列表
        /// </summary>
        /// <param name="workSpaceID"></param>
        /// <returns></returns>
        public List<OperationViewList> GetOperationViewList(Guid? workSpaceID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationViewList");
                db.AddInParameter(command, "@WorkSpaceID", DbType.Guid, workSpaceID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<OperationViewList>();
                }

                List<OperationViewList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new OperationViewList
                                               {
                                                   IsCheck=b.Field<Boolean>("IsCheck"),
                                                   ShowIndex = b.Field<Int32>("ShowIndex"),
                                                   ID = b.Field<Guid>("ID"),
                                                   Code = b.Field<string>("Code"),
                                                   CName = b.Field<string>("CName"),
                                                   EName = b.Field<string>("EName"),
                                                   TooltiopCN = b.Field<string>("TooltiopCN"),
                                                   TooltiopEN = b.Field<string>("TooltiopEN"),
                                                   OperationType=(OperationType)b.Field<Byte>("OperationType"),
                                                   SelectedColumn=b.Field<string>("SelectedColumn"),
                                                   BaseCriteria=b.Field<string>("BaseCriteria"),
                                                   UpdateBy=b.Field<string>("UpdateBy"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               }).ToList();

                results = (from d in results orderby d.IsCheck descending,d.Code ascending select d).ToList();

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
        /// 保存OperationViewInfo
        /// </summary>
        /// <returns></returns>
        public SingleResultData SaveOperationViewInfo(Guid workSpaceID,
                                                       Guid id,
                                                       OperationType type,
                                                       string code,
                                                       string cname,
                                                       string ename,
                                                       string tooltipcn,
                                                       string tooltipen,
                                                       string selectedColumn,
                                                       string baseCriteria,
                                                       Guid saveByID,
                                                       DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOperationViewInfo");

                db.AddInParameter(command, "@WorkSpaceID", DbType.Guid, workSpaceID);
                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@OperationType", DbType.Byte, type);
                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@CName", DbType.String, cname);
                db.AddInParameter(command, "@EName", DbType.String, ename);
                db.AddInParameter(command, "@ToolTipCN", DbType.String, tooltipcn);
                db.AddInParameter(command, "@ToolTipEN", DbType.String, tooltipen);
                db.AddInParameter(command, "@SelectedColumn", DbType.String, selectedColumn);
                db.AddInParameter(command, "@BaseCriteria", DbType.String, baseCriteria);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                return db.SingleResult(command);
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

        public void SaveWorkSpace2OperationViewList(Guid workSpaceID,
                                            Guid[] opIDs,
                                            Int32[] showIndexs,
                                            Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspSaveWorkSpace2OperationViews");
                db.AddInParameter(command, "@WorkSpaceID", DbType.Guid, workSpaceID);
                db.AddInParameter(command, "@OperationViewIDs", DbType.String, opIDs.Join());
                db.AddInParameter(command, "@ShowIndexs", DbType.String, showIndexs.Join());
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
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
        /// 得到WorkSpace的用户列表
        /// </summary>
        /// <param name="workSpaceID"></param>
        /// <returns></returns>
        public List<UserList> GetWorkSpaceUserList(Guid workSpaceID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspGetWorkSpaceUserList");
                db.AddInParameter(command, "@WorkSpaceID", DbType.Guid, workSpaceID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new UserList
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       Code = b.Field<string>("Code"),
                                                       CName = b.Field<string>("CName"),
                                                       EName = b.Field<string>("EName"),
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

        public void SaveWorkSpaceUserList(Guid workSpaceID, Guid[] userIDs,Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspSaveWorkSpace2Users");
                db.AddInParameter(command, "@WorkSpaceID", DbType.Guid, workSpaceID);
                db.AddInParameter(command, "@UserIDs", DbType.String, userIDs.Join());
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
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

        public List<Guid> GetWorkSpaceRoleList(Guid workSpaceID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspGetWorkSpace2RoleList");
                db.AddInParameter(command, "@WorkSpaceID", DbType.Guid, workSpaceID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<Guid>();
                }

                List<Guid> List = (from b in ds.Tables[0].AsEnumerable()
                                          select b.Field<Guid>("RoleID")).ToList();

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveWorkSpacewRoleList(Guid workSpaceID, Guid[] roles, Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveWorkSpace2Roles]");
                db.AddInParameter(command, "@WorkSpaceID", DbType.Guid, workSpaceID);
                db.AddInParameter(command, "@RoleIDs", DbType.String, roles.Join());
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

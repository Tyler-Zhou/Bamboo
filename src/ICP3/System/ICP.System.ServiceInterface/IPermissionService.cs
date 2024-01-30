//-----------------------------------------------------------------------
// <copyright file="IPermissionService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ServiceModel;

namespace ICP.Sys.ServiceInterface
{
 

    /// <summary>
    /// 菜单配置管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IPermissionService
    {
        /// <summary>
        /// 获取功能项列表
        /// </summary>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回功能项列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<FunctionList> GetFunctionList(bool? isValid);

        /// <summary>
        /// 获取角色有权访问的功能列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回角色有权访问的功能列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<FunctionList> GetRoleFunctionList(
            Guid roleId,
            bool? isValid);

        /// <summary>
        /// 获取用户有权访问的功能列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回角色有权访问的功能列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<FunctionList> GetUserFunctionList(
            Guid userId,
            bool? isValid);

        /// <summary>
        /// 获取UI配置项列表
        /// </summary>
        /// <param name="siteType">容器类型</param>
        /// <returns>返回UI配置项列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UIItemList> GetUIConfigurationList(SiteType siteType);
        
        /// <summary>
        /// 获取UI配置项详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回UI配置项详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        UIItemInfo GetUIConfigurationInfo(Guid id);

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
        [FunctionInfomation]
        [OperationContract]
        SingleHierarchyResultData SaveUIConfigurationInfo(
            Guid? id,
            Guid? parentId,
            string code,
            string cname,
            string ename,
            string description,
            Guid? functionId,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 删除UI配置项
        /// </summary>
        /// <param name="id">UI配置项ID</param>
        /// <param name="removeByID">删除任ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveUIConfigurationInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 设置UI配置项的位置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="preID">兄弟前一节点</param>
        /// <param name="setByID">设置人ID</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SetUIConfigurationPosition(
            Guid id,
            Guid? parentID,
            Guid? preID,
            Guid setByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取功能或则动作的角色权限列表
        /// </summary>
        /// <param name="id">功能或则动作ID</param>
        /// <returns>返回动作的角色和用户权限列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<RolePermissionList> GetRolePermissionList(Guid id);

        /// <summary>
        /// 获取功能或则动作的用户权限列表
        /// </summary>
        /// <param name="id">功能或则动作ID</param>
        /// <returns>返回动作的用户权限列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserPermissionList> GetUserPermissionList(Guid id);

       /// <summary>
        /// 设置Action下的角色
        /// </summary>
        /// <param name="permissionId">Action ID</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <param name="saveById">保存人ID</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetActionRoles(
            Guid permissionId,
            Guid[] roleIds,
            Guid saveById);

        /// <summary>
        /// 设置功能下角色
        /// </summary>
        /// <param name="permissionIds">功能ID列表</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <param name="saveById">保存人ID</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetFunctionRoles(
             Guid permissionIds,
             Guid[] roleIds,
             Guid saveById);

        /// <summary>
        /// 删除动作项角色权限
        /// </summary>
        /// <param name="permissionRoleIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveActionRolePermission(
            Guid[] permissionRoleIds,
            Guid removeById,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除功能项角色权限
        /// </summary>
        /// <param name="permissionRoleIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveFunctionRolePermission(
            Guid[] permissionRoleIds,
            Guid removeById,
            DateTime?[] updateDates);

        /// <summary>
        /// 设置动作特殊用户权限
        /// </summary>
        /// <param name="permissionUserIds">关联关系ID</param>
        /// <param name="permissionIds">功能或则动作ID</param>
        /// <param name="userIds">用户列表</param>
        /// <param name="orangnizationIds">组织节点列表</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDates">版本控制</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetActionUserPermission(
            Guid?[] permissionUserIds,
            Guid[] permissionIds,
            Guid[] userIds,
            Guid[] orangnizationIds,
            Guid saveById,
            DateTime?[] updateDates);

        /// <summary>
        /// 设置功能特殊用户权限
        /// </summary>
        /// <param name="permissionUserIds">关联关系ID</param>
        /// <param name="permissionIds">功能或则动作ID</param>
        /// <param name="userIds">用户列表</param>
        /// <param name="orangnizationIds">组织节点列表</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDates">版本控制</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetFunctionUserPermission(
            Guid?[] permissionUserIds,
            Guid[] permissionIds,
            Guid[] userIds,
            Guid[] orangnizationIds,
            Guid saveById,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除动作项用户权限
        /// </summary>
        /// <param name="permissionUserIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveActionUserPermission(
            Guid[] permissionUserIds,
            Guid removeById,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除功能项项用户权限
        /// </summary>
        /// <param name="permissionUserIds">用户权限关系ID列表</param>
        /// <param name="removeById">删除人ID</param>
        /// <param name="updateDates">版本控制</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveFunctionUserPermission(
            Guid[] permissionUserIds,
            Guid removeById,
            DateTime?[] updateDates);

        /// <summary>
        /// 获取指定用户对某功能的操作范围
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="commandName">命令名</param>
        /// <param name="permissionRange">权限范围</param>
        /// <returns>返回用户对某功能的操作范围</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OrganizationList> GetPermissionOrganizationList(
            Guid userId,
            string commandName,
            PermissionRangeType permissionRange);


         /// <summary>
        /// 获取有权限访问该功能模块的用户列表
        /// </summary>
        /// <param name="commandName">命令代码</param>
        /// <param name="organizationID">用户所在的组织结构</param>
        /// <param name="maxRecords">最大记录数量</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ModuleUserList> GetModuleUserList(
            string commandName,
            Guid? organizationID,
            int maxRecords);

        /// <summary>
        /// 设置角色下的功能
        /// </summary>
        /// <param name="roleID">功能ID列表</param>
        /// <param name="functionIDs">角色ID列表</param>
        /// <param name="isActions">类型列表</param>
        /// <param name="saveById">保存人ID</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetRoleFunctions(
            Guid roleID,
            Guid[] functionIDs,
            bool[] isActions,
            Guid saveById);

        #region WorkSpace
        /// <summary>
        /// 得到WorkSpace列表
        /// </summary>
        /// <param name="workSpaceID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<WorkSpaceList> GetWorkSpaceList();

        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveWorkSpaceInfo(Guid id, string code, string cName, string eName, Guid saveByID, DateTime? updatedate);

        /// <summary>
        /// 得到OperationViewList列表
        /// </summary>
        /// <param name="workSpaceID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OperationViewList> GetOperationViewList(Guid? workSpaceID);

        /// <summary>
        /// 保存OperationViewInfo
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveOperationViewInfo(Guid workSpaceID,
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
                                               DateTime? updateDate);
        [FunctionInfomation]
        [OperationContract]
        void SaveWorkSpace2OperationViewList(Guid workSpaceID,
                                            Guid[] opIDs,
                                            Int32[] showIndexs,
                                            Guid saveByID);

        /// <summary>
        /// 得到WorkSpace的用户列表
        /// </summary>
        /// <param name="workSpaceID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetWorkSpaceUserList(Guid workSpaceID);
        [FunctionInfomation]
        [OperationContract]
        void SaveWorkSpaceUserList(Guid workSpaceID, Guid[] userIDs,Guid saveByID);


        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetWorkSpaceRoleList(Guid workSpaceID);
        
        [FunctionInfomation]
        [OperationContract]
        void SaveWorkSpacewRoleList(Guid workSpaceID,Guid[] roles,Guid saveByID);
        #endregion
    }
}
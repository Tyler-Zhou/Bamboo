//-----------------------------------------------------------------------
// <copyright file="IRoleService.cs" company="LongWin">
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
    /// 角色管理服务 
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IRoleService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回角色列表对象</returns>
        [FunctionInfomation]
        [OperationContract]
        List<RoleList> GetRoleList(
            string name,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>返回角色编辑信息</returns>
        [FunctionInfomation]
        [OperationContract]
        RoleInfo GetRoleInfo(Guid id);


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
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveRoleInfo(
            Guid? id,
            string cname,
            string ename,
            string description,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变角色数据状态
        /// </summary>
        /// <param name="id">对应角色唯一键值</param>
        /// <param name="isValid">是否有效状态</param>
        /// <param name="changeById">修改人</param>
        /// <param name="updateDate">版本(更新时间)</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeRoleState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 获取角色下所挂岗位列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>返回角色下所挂岗位列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<Role2OrganizationJobList> GetRole2OrganizationJobList(Guid roleID);


       /// <summary>
        /// 保存角色岗位信息
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="ids">角色岗位ID</param>
        /// <param name="organizationJobIDs">岗位ID</param>
        /// <param name="setByID">更新时间</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetRole2OrganizationJobInfo(
            Guid roleID,
            Guid?[] ids,
            Guid[] organizationJobIDs,
            Guid setByID);


        /// <summary>
        /// 删除角色岗位信息
        /// </summary>
        /// <param name="ids">角色岗位ID</param>
        /// <param name="updateDates">更新时间</param>
        /// <param name="removeByID">删除人</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveRole2OrganizationJobInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID);

        /// <summary>
        /// 获取功能或则动作的角色和用户权限列表
        /// </summary>
        /// <param name="id">功能或则动作ID</param>
        /// <returns>返回动作的角色和用户权限列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<RolePermissionList> GetRolePermissionListByRoleID(Guid id);
    }
}
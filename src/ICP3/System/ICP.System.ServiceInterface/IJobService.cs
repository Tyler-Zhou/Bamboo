//-----------------------------------------------------------------------
// <copyright file="IJobService.cs" company="LongWin">
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
    /// 职位管理服务 
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IJobService
    {
        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <param name="code">中文名称</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回列表对象</returns>
        [FunctionInfomation]
        [OperationContract]
        List<JobList> GetJobList(
            string code,
            string name,
            bool? isValid,
            int maxRecords);
       

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回编辑信息</returns>
        [FunctionInfomation]
        [OperationContract]
        JobInfo GetJobInfo(Guid id);
        

        /// <summary>
        /// 获取组织结构下岗位列表
        /// </summary>
        /// <param name="organizationID">组织结构</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回组织结构下岗位列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<Organization2JobList> GetOrganization2JobList(
            Guid? organizationID,
            bool? isValid);
       

        /// <summary>
        /// 删除组织结构下岗位信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">更新时间</param>
        [FunctionInfomation]
        [OperationContract]
        void RemovetOrganization2JobInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID);
       


        /// <summary>
        /// 设置组织结构下岗位信息
        /// </summary>
        /// <param name="organizationID">组织结构ID</param>
        /// <param name="ids">ID</param>
        /// <param name="jobIds">岗位列表</param>
        /// <param name="updateDates">更新时间</param>
        /// <param name="setByID">设置人</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
             ManyResultData SetOrganization2JobInfo(
            Guid organizationID,
            Guid?[] ids,
            Guid[] jobIds,
            DateTime?[] updateDates,
            Guid setByID);
       
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
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SaveJobInfo(
            Guid? id,
            Guid? parentId,
            string code,
            string cname,
            string ename,
            string description,
            Guid saveById,
            DateTime? updateDate);
        

        /// <summary>
        /// 改变数据状态
        /// </summary>
        /// <param name="id">对应唯一键值</param>
        /// <param name="isValid">是否有效状态</param>
        /// <param name="changeById">修改人</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData ChangeJobState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);
       

        /// <summary>
        /// 设置职位的父结点
        /// </summary>
        /// <param name="id">当前结点</param>
        /// <param name="parentId">如果parentId==null 或则parentId==Guid.Empt则,当前结点为顶级节点,否则设置为parentId的子节点</param>
        /// <param name="setById">设置人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SetParentJob(
            Guid id,
            Guid? parentId,
            Guid setById,
            DateTime? updateDate);
       

         /// <summary>
        /// 保存角色岗位信息
        /// </summary>
        /// <param name="organizationJobID">角色ID</param>
        /// <param name="ids">角色岗位ID</param>
        /// <param name="roleIDs">岗位ID</param>
        /// <param name="updateDates">更新时间</param>
        /// <param name="setByID">设置人</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetOrganizationJob2RoleInfo(
            Guid organizationJobID,
            Guid?[] ids,
            Guid[] roleIDs,
            DateTime?[] updateDates,
            Guid setByID);
        


        /// <summary>
        /// 删除角色岗位信息
        /// </summary>
        /// <param name="ids">角色岗位ID</param>
        /// <param name="updateDates">更新时间</param>
        /// <param name="removeByID">删除人</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOrganizationJob2RoleInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID);


        
        /// <summary>
        /// 获取角色下所挂岗位列表
        /// </summary>
        /// <param name="organizationJobID">角色ID</param>
        /// <returns>返回角色下所挂岗位列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<Role2OrganizationJobList> GetRole2OrganizationJobList(Guid organizationJobID);
    }
}

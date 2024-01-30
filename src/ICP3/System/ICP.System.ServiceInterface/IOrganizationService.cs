//-----------------------------------------------------------------------
// <copyright file="IOrganizationService.cs" company="LongWin">
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
    /// 组织结构管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IOrganizationService
    {
        /// <summary>
        /// 获取组织结构列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回组织结构列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OrganizationList> GetOrganizationList(
            string code,
            string name,
            bool? isValid,
            int maxRecords);



        /// <summary>
        /// 获取办事处和公司列表
        /// </summary>
        /// <returns>返回组织结构列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OrganizationList> GetOfficeList();


        /// <summary>
        /// 获得指定组织结构以及子节点列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OrganizationList> GetOrganizationAndChildList(Guid[] ids);


        /// <summary>
        /// 获得指定组织结构上级公司信息
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OrganizationInfo GetOrganizationParentCompanyID(Guid deptID);

        /// <summary>
        /// 获取组织结构信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回组织结构信息</returns>
        [FunctionInfomation]
        [OperationContract]
        OrganizationInfo GetOrganizationInfo(Guid id);

        /// <summary>
        /// 指定指定组织结构的部门信息
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OrganizationInfo GetCompanyInfo(Guid orgID);

        /// <summary>
        /// 保存组织结构信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="parentId">父节点Id</param>
        /// <param name="type">组织结构类型</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="saveById">保存人Id</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SaveOrganizationInfo(
            Guid? id,
            Guid? parentId,
            OrganizationType type,
            string code,
            string cname,
            string ename,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 改变组织结构状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData ChangeOrganizationState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 设置组织结构节点的父节点
        /// </summary>
        /// <param name="childId">子节点Id</param>
        /// <param name="parentId">父节点Id</param>
        /// <param name="setById">设置人Id</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyHierarchyResultData SetParentOrganization(
            Guid childId,
            Guid? parentId,
            Guid setById,
            DateTime? updateDate);




    }
}
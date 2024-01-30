
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.WF.ServiceInterface
{

    /// <summary>
    /// 工作流配置服务接口
    /// </summary>
    [ServiceInfomation("工作流配置服务接口", ServiceType.Business)]
    [ServiceContract]
    public interface IWorkFlowConfigService
    {
        #region 流程配置
        /// <summary>
        /// 获取指定流程的明细信息
        /// </summary>
        /// <param name="id">流程配置ID</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        WorkFlowConfigInfo GetWorkFlowConfigInfoByID(Guid id,bool isEnglish);


        /// <summary>
        /// 获取指定流程的明细信息
        /// </summary>
        /// <param name="key">流程配置ID</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        WorkFlowConfigInfo GetWorkFlowConfigInfoByKey(string key, bool isEnglish);

        /// <summary>
        /// 获取指定用户可发起流程列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>返回指定用户可发起流程列表</returns>
       [FunctionInfomation]  [OperationContract]
        List<WorkFlowConfigInfo> GetWorkFlowConfigList(Guid? userID, bool isEnglish);

        /// <summary>
        /// 获得用户可以发起的流程列表(压缩后)
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
        Byte[] GetWorkFlowConfigListZip(Guid? userID, bool isEnglish);

        /// <summary>
        /// 保存指定流程配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="workflowKey">流程关键字(对于业务发起流程时候使用该关键字)</param>
        /// <param name="workFlowFileContent">流程xoml文件内容</param>
        /// <param name="ruleFileContent">对应的规则文件内容</param>
        /// <param name="cdesc">中文描述</param>
        /// <param name="edesc">英文描述</param>
        /// <param name="days">截至一月的有效期</param>
        /// <param name="isOA">是否只有在业务中可以使用</param>
        /// <param name="eprinttitle">英文打印标题</param>
        /// <param name="cprinttitle">中文打印标题</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="cName">中文名称</param>
        /// <param name="eName">英文名称</param>
        /// <param name="version">流程版本号</param>
        /// <param name="updateDate">保存版本号</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        Guid SaveWorkFlowConfigInfo(
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
            string version, 
            bool isEnglish);

        /// <summary>
        /// 删除流程配置信息
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <param name="removeByID">删除人</param>
       [FunctionInfomation]  [OperationContract]
        void RemoveWorkflowConfigInfo(
            Guid id,
            Guid removeByID, 
            bool isEnglish);

        /// <summary>
        /// 检测流程对应KEY的版本是否是大于现在已存在的版本,如果存在返回现在最大的版本
        /// </summary>
        /// <param name="key">流程Key</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        string GetWorkflowConfigLatestVersion(
            string key,
            string version, 
            bool isEnglish);


        /// <summary>
        /// 设置流程配置的职位权限
        /// </summary>
        /// <param name="workflowConfigID">流程配置ID</param>
        /// <param name="organizationIDs">组织结构ID</param>
        /// <param name="jobIDs">岗位ID</param>
        /// <param name="setByID">设置人</param>
        /// <returns>返回ManyResultWithRowIndex</returns>
       [FunctionInfomation]  [OperationContract]
        Guid[] SetWorkflowJobPermissionInfo(
            Guid workflowConfigID,
            Guid?[] organizationIDs,
            Guid?[] jobIDs,
            Guid setByID,
            bool isEnglish);

        /// <summary>
        /// 设置流程配置的用户权限
        /// </summary>
        /// <param name="workflowConfigID">文件ID</param>
        /// <param name="userIDs">岗位ID</param>
        /// <param name="setByID">设置人</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        Guid[] SetWorkflowUserPermissionInfo(
            Guid workflowConfigID,
            Guid?[] userIDs,
            Guid setByID, bool isEnglish);


        #endregion


        #region 表单文件配置管理

        /// <summary>
        /// 获取表单文件的配置列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="version">版本</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        List<FormProfileList> GetFormProfileList(
            string name,
            string version,
            bool isEnglish);

        /// <summary>
        /// 获得表单文件列表(压缩后的)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
        byte[] GetGetFormProfileListZip(string name,
            string version,
            bool isEnglish);



        /// <summary>
        /// 根据表单名获取对应表单的数据源
        /// </summary>
        /// <param name="name">表单名</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        FormProfileInfo GetFormProfileInfo(string name, bool isEnglish);

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
       [FunctionInfomation]  [OperationContract]
        SingleResultData SaveFormProfile(
            Guid? id,
            string key,
            string cName,
            string eName,
            string formData,
            string dataScheme,
            string version,
            Guid saveByID,
            DateTime? updateDate, bool isEnglish);


        /// <summary>
        /// 删除表单配置文件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
       [FunctionInfomation]  [OperationContract]
        void RemoveFormProfile(
            Guid id,
            Guid removeByID, bool isEnglish);

        #endregion
    }
}

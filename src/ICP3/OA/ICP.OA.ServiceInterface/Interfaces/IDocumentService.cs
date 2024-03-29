﻿//-----------------------------------------------------------------------
// <copyright file="IEMailService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.OA.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.OA.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;

    /// <summary>
    /// 文档管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IDocumentService
    {
        /// <summary>
        /// 获取FTP服务器信息
        /// </summary>
        /// <returns>返回FTP服务器信息</returns>
        [FunctionInfomation]  [OperationContract]
        FTPServerConfig GetFTPServerConfig(FolderType folderType);

        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentFolderFileList> GetDocumentFolderFileListByUserID(Guid? userID);


        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentFolderList> GetDocumentFolderList(Guid? userID);
        
        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentFolderList> GetFoldersListForBookingOrBusiness(Guid? userID, Guid? folderID, FolderType Foldertype, bool isNeedFileList);
        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentFolderFileList> GetDocumentFolderFileList(Guid? userID,Guid? folderID, FolderType folderType);


        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <param name="userID">用户</param>
        /// <param name="folderID">文件夹ID</param>
        /// <param name="folderType">文件夹类型</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentFolderList> GetFoldersList(Guid? userID,Guid? folderID, FolderType folderType);
       
        /// <summary>
        /// 获得文件列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="folderID">文件夹ID</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentFolderFileList> GetFileList(Guid? userID, Guid folderID);

        /// <summary>
        /// 保存文件夹信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="cname">中文名</param>
        /// <param name="ename">英文名</param>
        /// <param name="parentID">父接点</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultWithHierarchyCode</returns>
        [FunctionInfomation]  [OperationContract]
        SingleHierarchyResultData SaveFolderInfo(
            Guid? id,
            string name,
            Guid? parentID,
            Guid saveByID,
            DateTime? updateDate);
        

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]  [OperationContract]
        void RemoveFolderInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);
        

        /// <summary>
        /// 保存文件信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="folderID">文件夹ID</param>
        /// <param name="names">文件名</param>
        /// <param name="descriptions">描述</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultWithHierarchyCode</returns>
        [FunctionInfomation]  [OperationContract]
        ManyResultData SaveFileInfo(
            Guid?[] ids,
            Guid folderID,
            string[] names,
            string[] descriptions,
            Guid saveByID,
            DateTime?[] updateDates);


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]  [OperationContract]
        void RemoveFileInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回文件信息</returns>
        [FunctionInfomation]  [OperationContract]
        DocumentFileInfo GetFileInfo(Guid id);
        
        ///// <summary>
        ///// 获取文档权限的岗位列表
        ///// </summary>
        ///// <param name="fileID">文件ID</param>
        ///// <returns>返回文档权限的岗位列表</returns>
        //[FunctionInfomation]  [OperationContract]
        //List<DocumentFilePermissionList> GetJobFilePermissionList(Guid fileID);


        /// <summary>
        /// 获取文档权限的用户列表
        /// </summary>
        /// <param name="fileID">文件ID</param>
        /// <returns>返回文档权限的岗位列表</returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentUserPermissionList> GetDocumentUserPermissionList(Guid fileID);
        


        
        /// <summary>
        /// 删除文件下用户权限信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="removeByID">删除人</param>
        [FunctionInfomation]  [OperationContract]
        void RemoveUserDocumentPermissions(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID);
        
        /// <summary>
        /// 获取文件夹权限的岗位列表
        /// </summary>
        /// <param name="documentID">文件ID</param>
        /// <returns>返回文件夹权限的岗位列表</returns>
        [FunctionInfomation]  [OperationContract]
        List<DocumentOrganizationJobPermissionList> GetDocumentOrganizationJobPermissionList(Guid documentID);
        

        /// <summary>
        /// 设置岗位的文件夹权限
        /// </summary>
        /// <param name="fileID">文件ID</param>
        /// <param name="filePermissionIds">文件权限ID</param>
        /// <param name="organizationIds">组织结构ID</param>
        /// <param name="jobIds">岗位ID</param>
        /// <param name="permissions">权限列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="setById">设置人</param>
        /// <returns>返回ManyResultWithRowIndex</returns>
        [FunctionInfomation]  [OperationContract]
        ManyResultData SetJobDocumentPermissionInfo(
            Guid fileID,
            Guid?[] filePermissionIds,
            Guid?[] organizationIds,
            Guid?[] jobIds,
            DocuentPermission[] permissions,
            DateTime?[] updateDates,
            Guid setById);
        


        /// <summary>
        /// 删除文件夹下岗位权限信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="removeByID">删除人</param>
        [FunctionInfomation]  [OperationContract]
        void RemoveJobDocumentPermissionInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID);
        


        /// <summary>
        /// 设置用户的文件夹权限
        /// </summary>
        /// <param name="fileID">文件ID</param>
        /// <param name="filePermissionIds">文件权限ID</param>
        /// <param name="userIds">岗位ID</param>
        /// <param name="permissions">权限列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="setById">设置人</param>
        /// <returns>返回ManyResultWithRowIndex</returns>
        [FunctionInfomation]  [OperationContract]
        ManyResultData SetUserDocumentPermissionInfo(
            Guid fileID,
            Guid?[] filePermissionIds,
            Guid?[] userIds,
            DocuentPermission[] permissions,
            DateTime?[] updateDates,
            Guid setById);



        /// <summary>
        /// 设置所在文件夹
        /// </summary>
        /// <param name="ids">文件ID或文件夹ID</param>
        /// <param name="types">类型(1:文件夹，2：文件)</param>
        /// <param name="parentId">文件夹ID</param>
        /// <param name="setById">设置人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyHierarchyResultData</returns>
        [FunctionInfomation]  [OperationContract]
        ManyHierarchyResultData SetParentFolder(
                Guid[] ids,
                OADocumentType[] types,
                Guid parentId,
                Guid setById,
                DateTime?[] updateDates);



         /// <summary>
        /// 获取指定用户对文档的访问权限
        /// </summary>
        /// <param name="documentID">文档ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>返回</returns>
        [FunctionInfomation]  [OperationContract]
        DocuentPermission GetDocumentPermission(
            Guid documentID,
            Guid userID);

        /// <summary>
        /// 获取所有文档信息
        /// </summary>
        /// <returns>返回</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DocumentFileInfo> GetAllDocumentFileInfos();
    }
}

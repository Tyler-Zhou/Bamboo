//-----------------------------------------------------------------------
// <copyright file="DocumentClient.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.OA.UI.Service
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.OA.ServiceInterface;
    using ICP.OA.ServiceInterface.Client;
    using ICP.OA.ServiceInterface.DataObjects;
    using Microsoft.Practices.CompositeUI;
    using ICP.FileSystem.ServiceInterface;
    using ICP.Sys.ServiceInterface;
    using ICP.DataCache.ServiceInterface;

    /// <summary>
    /// 文档管理客户端服务(服务端的一个代理)
    /// </summary>
    public class DocumentClientService : IDocumentClientService
    {

        #region 属性
        /// <summary>
        /// 文档类型
        /// </summary>
        public FolderType Foldertype
        {
            get;
            set;
        }
        #endregion
        FTPServerConfig _ftpServerConfig = null;
        /// <summary>
        /// FTP服务器配置信息
        /// </summary>
        private FTPServerConfig FTPServerConfig
        {
            get
            {
                if (_ftpServerConfig == null)
                {
                    _ftpServerConfig = this.documentService.GetFTPServerConfig(Foldertype);
                }

                return _ftpServerConfig;
            }
        }

        List<DocumentFolderFileList> _userDocumentlist;
        private List<DocumentFolderFileList> UserDocumentlist
        {
            get
            {
                if (_userDocumentlist == null)
                {
                    _userDocumentlist = this.documentService.GetDocumentFolderFileListByUserID(LocalData.UserInfo.LoginID);
                }

                return _userDocumentlist;
            }
        }
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDocumentService documentService { get; set; }

        [ServiceDependency]
        public IUserService userService { get; set; }

        #region Client File Service
        public static IClientFileService ClientFileService
        {
            get { return ServiceClient.GetClientService<IClientFileService>(); }
        }
        #endregion
        #endregion


        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        public List<DocumentFolderFileList> GetDocumentFolderFileList(Guid? userID)
        {
            List<DocumentFolderFileList> documentlist = documentService.GetDocumentFolderFileListByUserID(userID);

            //缓存当前文档列表
            _userDocumentlist = documentlist;

            return documentlist;
        }


        /// <summary>
        /// 获取文档列表(文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        public List<DocumentFolderList> GetDocumentFolderList(Guid? userID)
        {
            List<DocumentFolderList> folders = documentService.GetDocumentFolderList(userID);
            return folders;
        }


        #region 获得文件夹列表
        /// <summary>
        /// 获取文档列表(文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        public List<DocumentFolderList> GetFoldersList(Guid? userID, Guid? folderID, FolderType folderType)
        {
            List<DocumentFolderList> folders = documentService.GetFoldersList(userID, folderID, folderType);
            return folders;
        }
        #endregion

        #region 获得文件列表
        /// <summary>
        /// 获得文件列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="folderID">文件夹ID</param>
        /// <returns></returns>
        public List<DocumentFolderFileList> GetFileList(Guid? userID, Guid folderID)
        {
            List<DocumentFolderFileList> folders = documentService.GetFileList(userID, folderID);
            return folders;
        }
        #endregion

        #region 获得文档列表
        public List<DocumentFolderFileList> GetDocumentFolderFileList(Guid? userID, Guid? folderID, FolderType folderType)
        {
            List<DocumentFolderFileList> folderFileList = documentService.GetDocumentFolderFileList(userID, folderID, folderType);
            return folderFileList;
        }
        #endregion

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
        public SingleHierarchyResultData SaveFolderInfo(
            Guid? id,
            string name,
            Guid? parentID,
            Guid saveByID,
            DateTime? updateDate)
        {
            SingleHierarchyResultData result = documentService.SaveFolderInfo(
                id,
                name,
                parentID,
                saveByID,
                updateDate);

            return result;
        }


        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveFolderInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            documentService.RemoveFolderInfo(
                id,
                removeByID,
                updateDate);

            DocumentFolderFileList folderInfo = this.UserDocumentlist.Find(delegate(DocumentFolderFileList document)
            {
                return document.ID == id;
            });

            if (folderInfo == null)
            {
                throw new Exception("不存在该文件夹");
            }

            List<DocumentFolderFileList> files = this.UserDocumentlist.FindAll(delegate(DocumentFolderFileList document)
            {
                return document.HierarchyCode.StartsWith(folderInfo.HierarchyCode)
                    && document.DocumentType == OADocumentType.File;
            });

            #region Comment Code
            ////删除FTP服务器上的文件
            //string ownerAccountFolderPath = this.BuildOwnerFolderPath(
            //    removeByID,
            //    this.FTPServerConfig.BasePath);



            //FTPClient ftp = new FTPClient();
            //try
            //{
            //    ftp.RemoteHost = this.FTPServerConfig.Host;
            //    ftp.RemotePath = ownerAccountFolderPath;
            //    ftp.UserName = this.FTPServerConfig.User;
            //    ftp.Password = this.FTPServerConfig.Password;
            //    ftp.Login();

            //    //
            //    string tempMailContentFileName;
            //    foreach (DocumentFolderFileList file in files)
            //    {
            //        tempMailContentFileName = this.BuildOwnerFileName(
            //            id,
            //            file.Name);
            //        ftp.DeleteRemoteFile(tempMailContentFileName);
            //    }
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    ftp.Close();
            //} 
            #endregion
        }


        /// <summary>
        /// 保存文件信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="folderID">文件夹ID</param>
        /// <param name="names">文件名</param>
        /// <param name="descriptions">描述</param>
        /// <param name="contents">内容</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultWithHierarchyCode</returns>
        public ManyResultData SaveFileInfo(
            Guid?[] ids,
            Guid folderID,
            string[] names,
            string[] descriptions,
            byte[][] contents,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            //保存信息到数据库
            ManyResultData result = documentService.SaveFileInfo(
                ids,
                folderID,
                names,
                descriptions,
                saveByID,
                updateDates);

            UplaodOADocumentThread tdsenf = new UplaodOADocumentThread();
            tdsenf._result = result;
            tdsenf._names = names;
            tdsenf._contents = contents;
            //不使用多线程上传，需要返回值做判断
            //Thread tdsenfFile = new Thread(new ThreadStart(tdsenf._UplaodOADocument));
            //tdsenfFile.Start();
            tdsenf._UplaodOADocument();


            #region Comment Code(FTP)
            ////上传到FTP服务器
            //string ownerAccountFolderPath = this.BuildOwnerFolderPath(
            //    saveByID,
            //    this.FTPServerConfig.BasePath);
            //FTPClient ftp = new FTPClient();
            //ftp.RemoteHost = this.FTPServerConfig.Host;
            //ftp.RemotePath = ownerAccountFolderPath;
            //ftp.UserName = this.FTPServerConfig.User;
            //ftp.Password = this.FTPServerConfig.Password;
            //ftp.Login();

            //for (int i = 0; i < result.ChildResults.Count;i++ )
            //{
            //    string tempMailContentFileName = this.BuildOwnerFileName(
            //        result.ChildResults[i].ID,
            //        names[i]);

            //    ftp.Upload(tempMailContentFileName, contents[i]);
            //}

            //ftp.Close(); 
            #endregion

            return result;
        }
        public class UplaodOADocumentThread
        {
            public ManyResultData _result { get; set; }

            public byte[][] _contents { get; set; }

            public string[] _names { get; set; }

            public DocumentInfo[] dInfo;

            public void _UplaodOADocument()
            {
                dInfo = new DocumentInfo[_result.ChildResults.Count];
                for (int i = 0; i < _result.ChildResults.Count; i++)
                {
                    dInfo[i] = new DocumentInfo();
                    dInfo[i].Content = _contents[i];
                    dInfo[i].Id = _result.ChildResults[i].ID;
                    dInfo[i].Name = _names[i];
                    dInfo[i].CreateBy = LocalData.UserInfo.LoginID;
                    dInfo[i].CreateByName = LocalData.UserInfo.LoginName;
                }
                ClientFileService.UplaodOADocument(dInfo);
            }
        }
        /// <summary>
        /// 保存文件信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="descriptions">描述</param>
        /// <param name="parentID">父节点</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultWithHierarchyCode</returns>
        public SingleResultData SaveFileInfo(Guid? id,
                                                    string name,
                                                    string descriptions,
                                                    Guid parentID,
                                                    Guid saveByID,
                                                    DateTime? updateDate)
        {
            ManyResultData result = documentService.SaveFileInfo(
               new Guid?[] { id }
               , parentID
               , new string[] { name }
               , new string[] { descriptions }
               , saveByID
               , new DateTime?[] { updateDate });

            return result.ChildResults[0];
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveFileInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {

            DocumentFileInfo fileInfo = documentService.GetFileInfo(id);
            if (fileInfo == null)
            {
                throw new Exception("服务器不存在该文件.");
            }

            documentService.RemoveFileInfo(
                id,
                removeByID,
                fileInfo.UpdateDate);
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回文件信息</returns>
        public DocumentFileInfo GetFileInfo(Guid id)
        {
            DocumentFileInfo fileInfo = documentService.GetFileInfo(id);

            if (fileInfo.StreamID != null)
            {
                ContentInfo doInfo = ClientFileService.DownloadOADocument(id);
                if (doInfo != null)
                    fileInfo.Content = doInfo.Content;
            }
            else
            {
                string remotefileName = this.BuildOwnerFileName(
                    id,
                    fileInfo.FileName);
                string ownerAccountFolderPath = this.BuildOwnerFolderPath(
                   LocalData.UserInfo.LoginID,
                   this.FTPServerConfig.BasePath);
                fileInfo.Content = FtpHelper.Download(
                    this.FTPServerConfig.Host,
                    ownerAccountFolderPath,
                    this.FTPServerConfig.User,
                    this.FTPServerConfig.Password,
                    remotefileName);
            }

            return fileInfo;
        }


        /// <summary>
        /// 获取文档权限的用户列表
        /// </summary>
        /// <param name="documentID">文件ID</param>
        /// <returns>返回文档权限的岗位列表</returns>
        public List<DocumentUserPermissionList> GetDocumentUserPermissionList(Guid documentID)
        {
            List<DocumentUserPermissionList> results = documentService.GetDocumentUserPermissionList(documentID);
            return results;
        }




        /// <summary>
        /// 删除文件下用户权限信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="removeByID">删除人</param>
        public void RemoveUserDocumentPermissions(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            documentService.RemoveUserDocumentPermissions(
                ids,
                updateDates,
                removeByID);
        }

        /// <summary>
        /// 获取文件夹权限的岗位列表
        /// </summary>
        /// <param name="documentID">文件ID</param>
        /// <returns>返回文件夹权限的岗位列表</returns>
        public List<DocumentOrganizationJobPermissionList> GetDocumentOrganizationJobPermissionList(Guid documentID)
        {
            List<DocumentOrganizationJobPermissionList> results = documentService.GetDocumentOrganizationJobPermissionList(documentID);
            return results;
        }


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
        public ManyResultData SetJobDocumentPermissionInfo(
            Guid fileID,
            Guid?[] filePermissionIds,
            Guid?[] organizationIds,
            Guid?[] jobIds,
            DocuentPermission[] permissions,
            DateTime?[] updateDates,
            Guid setById)
        {
            ManyResultData result = this.documentService.SetJobDocumentPermissionInfo(
                fileID,
                filePermissionIds,
                organizationIds,
                jobIds,
                permissions,
                updateDates,
                setById);

            return result;
        }



        /// <summary>
        /// 删除文件夹下岗位权限信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="removeByID">删除人</param>
        public void RemoveJobDocumentPermissionInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            this.documentService.RemoveJobDocumentPermissionInfo(
                ids,
                updateDates,
                removeByID);
        }



        /// <summary>
        /// 设置用户的文件夹权限
        /// </summary>
        /// <param name="fileID">文件ID</param>
        /// <param name="filePermissionIds">文件权限ID</param>
        /// <param name="organizationIds">组织结构ID</param>
        /// <param name="userIds">岗位ID</param>
        /// <param name="permissions">权限列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="setById">设置人</param>
        /// <returns>返回ManyResultWithRowIndex</returns>
        public ManyResultData SetUserDocumentPermissionInfo(
            Guid fileID,
            Guid?[] filePermissionIds,
            Guid?[] userIds,
             DocuentPermission[] permissions,
            DateTime?[] updateDates,
            Guid setById)
        {
            ManyResultData result = documentService.SetUserDocumentPermissionInfo(
                fileID,
                filePermissionIds,
                userIds,
                permissions,
                updateDates,
                setById);

            return result;
        }



        /// <summary>
        /// 设置所在文件夹
        /// </summary>
        /// <param name="ids">文件ID或文件夹ID</param>
        /// <param name="types">类型(1:文件夹，2：文件)</param>
        /// <param name="parentId">文件夹ID</param>
        /// <param name="setById">设置人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyHierarchyResultData</returns>
        public ManyHierarchyResultData SetParentFolder(
                Guid[] ids,
                OADocumentType[] types,
                Guid parentId,
                Guid setById,
                DateTime?[] updateDates)
        {
            ManyHierarchyResultData result = documentService.SetParentFolder(
                ids,
                types,
                parentId,
                setById,
                updateDates);

            return result;
        }



        /// <summary>
        /// 获取指定用户对文档的访问权限
        /// </summary>
        /// <param name="documentID">文档ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>返回</returns>
        public DocuentPermission GetDocumentPermission(
             Guid documentID,
             Guid userID)
        {
            DocuentPermission permission = documentService.GetDocumentPermission(
                documentID,
                userID);

            return permission;
        }

        /// <summary>
        /// 获取所有文档信息
        /// </summary>
        /// <returns>返回</returns>
        public List<DocumentFileInfo> GetAllDocumentFileInfos()
        {
            return documentService.GetAllDocumentFileInfos();
        }

        /// <summary>
        /// 升级到云服务
        /// </summary>
        /// <param name="docItem">文档列表</param>
        public string SingleUpgradeCloud(DocumentFileInfo docItem)
        {
            string upgradeResult = "Success";
            try
            {
                string remotefileName = this.BuildOwnerFileName(
                            docItem.ID,
                            docItem.FileName);
                string ownerAccountFolderPath = this.BuildOwnerFolderPath(
                    docItem.CreateByID,
                    this.FTPServerConfig.BasePath);

                DocumentInfo dInfo = new DocumentInfo();
                dInfo.Content = FtpHelper.Download(FTPServerConfig.Host, ownerAccountFolderPath
                    , FTPServerConfig.User, FTPServerConfig.Password, remotefileName);
                dInfo.Id = docItem.ID;
                dInfo.Name = docItem.FileName;
                dInfo.CreateBy = docItem.CreateByID;
                dInfo.CreateByName = docItem.CreateByName;
                ClientFileService.UplaodOADocument(dInfo);
            }
            catch (Exception ex)
            {
                upgradeResult = "Failure; Exception:" + ex.Message;
            }
            return upgradeResult;
        }

        /*构造用户邮件帐号对应的FTP日志文件夹路径(每个用户在FTP只有一个文件夹，避免文件夹新增，改变增加开销)*/
        private string BuildOwnerFolderPath(Guid owner, string basePath)
        {
            //if (string.IsNullOrEmpty(basePath))
            //{
            //    return owner.ToString();
            //}
            //else
            //{
            //    return basePath + "\\" + owner.ToString();
            //}
            return basePath;
        }

        /*构造特殊的文件名(消息日志ID_真实文件名),避免重复*/
        private string BuildOwnerFileName(Guid fileID, string fileName)
        {
            return fileID.ToString();

        }
    }
}

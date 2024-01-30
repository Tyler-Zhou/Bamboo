//-----------------------------------------------------------------------
// <copyright file="EMailService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.OA.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.OA.ServiceInterface;
    using ICP.OA.ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 文档管理服务
    /// </summary>
    public class DocumentService : IDocumentService
    {
        string _ftpHost = "192.168.1.100";
        string _ftpUser = "xxd";
        string _ftpPassword = "xxd";
        string _ftpDocumentPath = "";
        string _ftpBusinessPath = "";
        string _ftpBookingPath = "";
        string _ftpBasePath = "";
        public DocumentService(
            string ftpHost,
            string ftpDocumentPath,
            string ftpBusinessPath,
            string ftpBookingPath,
            string ftpUser,
            string ftpPassword)
        {
            _ftpHost = ftpHost;
            _ftpPassword = ftpPassword;
            _ftpDocumentPath = ftpDocumentPath;
            _ftpBusinessPath = ftpBusinessPath;
            _ftpBookingPath = ftpBookingPath;
            _ftpUser = ftpUser;
        }

        public DocumentService() { }
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            { 
                 return ApplicationContext.Current.IsEnglish;
            }
        }

        /// <summary>
        /// 获取FTP服务器信息
        /// </summary>
        /// <returns>返回</returns>
        public FTPServerConfig GetFTPServerConfig(FolderType folderType)
        {
            switch (folderType)
            {
                case FolderType.Booking:
                    _ftpBasePath = _ftpBookingPath;
                    break;
                case FolderType.Business:
                    _ftpBasePath = _ftpBusinessPath;
                    break;
                case FolderType.Private:
                    _ftpBasePath = _ftpDocumentPath;
                    break;
                case FolderType.Public:
                    _ftpBasePath = _ftpDocumentPath;
                    break;
                default:
                    _ftpBasePath = _ftpDocumentPath;
                    break;
            }

            return new FTPServerConfig(
                _ftpHost,
                _ftpUser,
                _ftpPassword,
                _ftpBasePath);
        }


        #region 获取文档列表(文件和文件夹列表)
        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<DocumentFolderFileList> GetDocumentFolderFileListByUserID(Guid? userID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetDocumentList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DocumentFolderFileList> results = (from b in ds.Tables[0].AsEnumerable()
                                                        select new DocumentFolderFileList
                                                        {
                                                            ID = b.Field<Guid>("ID"),
                                                            ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                            ParentName = b.Field<String>("ParentName"),
                                                            Permission = (DocuentPermission)b.Field<byte>("Permission"),
                                                            Name = b.Field<String>("Name"),
                                                            HierarchyCode = b.Field<String>("HierarchyCode"),
                                                            Description = b.Field<String>("Description"),
                                                            DocumentType = (OADocumentType)b.Field<byte>("DocumentType"),
                                                            CreateByID = b.Field<Guid>("CreateByID"),
                                                            CreateByName = b.Field<String>("CreateByName"),
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
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        public List<DocumentFolderList> GetDocumentFolderList(Guid? userID)
        {
            List<DocumentFolderFileList> folderfiles = this.GetDocumentFolderFileListByUserID(userID);
            List<DocumentFolderList> folders = (from f in folderfiles
                                                where f.DocumentType == OADocumentType.Folder
                                                select new DocumentFolderList
                                                {
                                                    ID = f.ID,
                                                    CreateByID = f.CreateByID,
                                                    CreateByName = f.CreateByName,
                                                    CreateDate = f.CreateDate,
                                                    Name = f.Name,
                                                    ParentID = f.ParentID,
                                                    ParentName = f.ParentName,
                                                    UpdateDate = f.UpdateDate,
                                                   Permission=f.Permission
                                                }).ToList();

            return folders;
        }

        #endregion
        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <param name="folderID">文件夹ID</param>
        /// <param name="folderType">文件夹类型</param>
        /// <returns></returns>
        public List<DocumentFolderList> GetFoldersListForBookingOrBusiness(Guid? userID, Guid? folderID, FolderType folderType,bool isNeedFileList)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetDocumentList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@FolderID", DbType.Guid, folderID);
                db.AddInParameter(dbCommand, "@FolderType", DbType.Int32, folderType);
                db.AddInParameter(dbCommand, "@IsNeedFile", DbType.Boolean, isNeedFileList);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DocumentFolderList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DocumentFolderList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                        Permission = (DocuentPermission)b.Field<byte>("Permission"),
                                                        Name = b.Field<String>("Name"),
                                                        HierarchyCode = b.Field<String>("HierarchyCode"),
                                                        CreateByID = b.Field<Guid>("CreateByID"),
                                                        CreateByName = b.Field<String>("CreateByName"),
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
        #region 获得文件夹列表
        /// <summary>
        /// 根据当前用户与文件夹类型 获得文件夹列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <param name="folderID">文件夹ID</param>
        /// <param name="folderType">文件夹类型</param>
        /// <returns></returns>
        public List<DocumentFolderList> GetFoldersList(Guid? userID,Guid? folderID, FolderType folderType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetFoldertList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@FolderID", DbType.Guid, folderID);
                db.AddInParameter(dbCommand, "@FolderType", DbType.Int32, folderType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DocumentFolderList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DocumentFolderList
                                                        {
                                                            ID = b.Field<Guid>("ID"),
                                                            ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                            Permission = (DocuentPermission)b.Field<byte>("Permission"),
                                                            Name = b.Field<String>("Name"),
                                                            HierarchyCode = b.Field<String>("HierarchyCode"),
                                                            CreateByID=b.Field<Guid>("CreateByID"),
                                                            CreateByName = b.Field<String>("CreateByName"),
                                                            CreateDate=b.Field<DateTime>("CreateDate"),
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
        #endregion

        #region 获得文件列表
        /// <summary>
        /// 根据当前用户与文件夹ID获得文件列表
        /// </summary>
        /// <param name="userID">用户</param>
        /// <param name="folderID">文件夹GUID</param>
        /// <returns></returns>
        public List<DocumentFolderFileList> GetFileList(Guid? userID, Guid folderID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetFileList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@FolderID", DbType.Guid, folderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DocumentFolderFileList> results = (from b in ds.Tables[0].AsEnumerable()
                                                        select new DocumentFolderFileList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                        Permission = (DocuentPermission)b.Field<byte>("Permission"),
                                                        Name = b.Field<String>("FileName"),
                                                        DocumentType=OADocumentType.File,
                                                        CreateByID = b.Field<Guid>("CreateByID"),
                                                        CreateByName=b.Field<String>("CreateByName"),
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
        #endregion

        #region 获得文档列表(文件和文件夹)

        /// <summary>
        /// 获取文档列表(文件和文件夹列表)
        /// </summary>
        /// <param name="userID">用户</param>
        /// <returns></returns>
        public List<DocumentFolderFileList> GetDocumentFolderFileList(Guid? userID, Guid? folderID, FolderType folderType)
        {
            //获得指定目录下的文件列表
            List<DocumentFolderFileList> fileList = GetFileList(userID, folderID.ToGuid());
            //获得指定目录下的文件夹列表
            List<DocumentFolderList> folderList=GetFoldersList(userID, folderID, folderType);
            //将文件夹类转换为文件类型
            List<DocumentFolderFileList> newList = (from d in folderList
                                                    select new DocumentFolderFileList
                                                    {
                                                        ID =d.ID,
                                                        ParentID =d.ParentID,
                                                        Permission = d.Permission,
                                                        Name =d.Name,
                                                        HierarchyCode = d.HierarchyCode,
                                                        DocumentType = OADocumentType.Folder,
                                                        CreateByID = d.CreateByID,
                                                        CreateByName=d.CreateByName,
                                                        CreateDate=d.CreateDate,
                                                        UpdateDate =d.UpdateDate,
                                                    }).ToList();


            foreach (DocumentFolderFileList item in newList)
            {
                fileList.Add(item);
            }

            return fileList;

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
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspSaveFolderInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleHierarchyResultData result = db.SingleHierarchyResult(dbCommand);
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
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                //删除数据库中的文件夹信息
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspRemoveFolderInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                //删除服务器中对应的文件夹信息
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
        /// 保存文件信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="folderID">文件夹ID</param>
        /// <param name="names">文件名</param>
        /// <param name="descriptions">描述</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultWithHierarchyCode</returns>
        public ManyResultData SaveFileInfo(
            Guid?[] ids,
            Guid folderID,
            string[] names,
            string[] descriptions,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(folderID, "folderID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspSaveFileInfo");

                string tempIDs = ids.Join();
                string tempNames = names.Join();
                string tempDescriptions = descriptions.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@FolderID", DbType.Guid, folderID);
                db.AddInParameter(dbCommand, "@FileNames", DbType.String, tempNames);
                db.AddInParameter(dbCommand, "@FileDescriptions", DbType.String, tempDescriptions);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
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
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                //删除数据库中的文件信息
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspRemoveFileInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                //删除服务器中对应的文件信息


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
        /// 获取文件信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回文件信息</returns>
        public DocumentFileInfo GetFileInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetFileInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DocumentFileInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new DocumentFileInfo
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               FolderID = b.Field<Guid>("FolderID"),
                                               FolderName = b.Field<String>("FolderName"),
                                               FileName = b.Field<String>("FileName"),
                                               CreateByID = b.Field<Guid>("CreateByID"),
                                               CreateByName = b.Field<String>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               StreamID = b.Field<Guid?>("StreamID"),
                                               FileDescription = b.Field<String>("FileDescription"),
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
        /// 获取文档权限的用户列表
        /// </summary>
        /// <param name="fileID">文件ID</param>
        /// <returns>返回文档权限的岗位列表</returns>
        public List<DocumentUserPermissionList> GetDocumentUserPermissionList(Guid documentID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetDocumentPermissionList");

                db.AddInParameter(dbCommand, "@DocumentID", DbType.Guid, documentID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DocumentUserPermissionList> results = (from b in ds.Tables[0].AsEnumerable()
                                                            where b.Field<byte>("UserObjectType") == (byte)UserObjectType.User
                                                            select new DocumentUserPermissionList
                                                        {
                                                            ID = b.Field<Guid>("ID"),
                                                            UserID = b.Field<Guid>("UseObjectID"),
                                                            UserName = b.Field<String>("UseObjectName"),
                                                            Permission = (DocuentPermission)b.Field<byte>("Permission"),
                                                            CreateByID = b.Field<Guid>("CreateByID"),
                                                            CreateByName = b.Field<String>("CreateByName"),
                                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                                            IsParentPermission = b.Field<bool>("IsParentPermission"),
                                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                            IsDirty = false
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
            this.RemoveDocumentPermissionInfo(
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
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetDocumentPermissionList");

                db.AddInParameter(dbCommand, "@DocumentID", DbType.Guid, documentID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DocumentOrganizationJobPermissionList> results = (from b in ds.Tables[0].AsEnumerable()
                                                                       where b.Field<byte>("UserObjectType") == (byte)UserObjectType.Job
                                                                       select new DocumentOrganizationJobPermissionList
                                                                        {
                                                                            ID = b.Field<Guid>("ID"),
                                                                            OrganizationID = b.Field<Guid?>("OrganizationID"),
                                                                            OrganizationName = b.Field<string>("OrganizationName"),
                                                                            JobID = b.Field<Guid?>("UseObjectID"),
                                                                            JobName = b.Field<String>("UseObjectName"),
                                                                            Permission = (DocuentPermission)b.Field<byte>("Permission"),
                                                                            CreateByID = b.Field<Guid>("CreateByID"),
                                                                            CreateByName = b.Field<String>("CreateByName"),
                                                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                                                            IsParentPermission = b.Field<bool>("IsParentPermission"),
                                                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                            IsDirty = false
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
            UserObjectType?[] uots = new UserObjectType?[permissions.Length];
            for (int i = 0; i < permissions.Length; i++)
            {
                uots[i] = UserObjectType.Job;
            }

            return this.SetDocumentPermissionInfo(
                 fileID,
                 filePermissionIds,
                 organizationIds,
                 jobIds,
                 uots,
                 permissions,
                 updateDates,
                 setById);
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
            this.RemoveDocumentPermissionInfo(
                ids,
                updateDates,
                removeByID);
        }


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
        public ManyResultData SetUserDocumentPermissionInfo(
            Guid fileID,
            Guid?[] filePermissionIds,
            Guid?[] userIds,
            DocuentPermission[] permissions,
            DateTime?[] updateDates,
            Guid setById)
        {
            UserObjectType?[] uots = new UserObjectType?[permissions.Length];
            Guid?[] organizationIds = new Guid?[permissions.Length];
            for (int i = 0; i < permissions.Length; i++)
            {
                uots[i] = UserObjectType.User;
                organizationIds[i] = null;
            }

            return this.SetDocumentPermissionInfo(
                 fileID,
                 filePermissionIds,
                 organizationIds,
                 userIds,
                 uots,
                 permissions,
                 updateDates,
                 setById);
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
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(parentId, "parentId");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("oa.uspSetParentFolder");


                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (OADocumentType type in types)
                {
                    if (sb.Length > 0) sb.Append(",");
                    sb.Append((short)type);
                }
                db.AddInParameter(command, "@IDs", DbType.String, tempIds);
                db.AddInParameter(command, "@Types", DbType.String, sb.ToString());
                db.AddInParameter(command, "@ParentID", DbType.Guid, parentId);
                db.AddInParameter(command, "@SetByID", DbType.Guid, setById);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempUpdateDates);
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
        /// 获取指定用户对文档的访问权限
        /// </summary>
        /// <param name="documentID">文档ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>返回</returns>
        public DocuentPermission GetDocumentPermission(
            Guid documentID,
            Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(documentID, "documentID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("oa.uspGetDocumentPermission");

                db.AddInParameter(command, "@DocumentID", DbType.Guid, documentID);
                db.AddInParameter(command, "@UserID", DbType.Guid, userID);

                db.AddOutParameter(command, "@Permission", DbType.Byte, 2);
                db.ExecuteNonQuery(command);

                DocuentPermission permission = (DocuentPermission)db.GetParameterValue(command, "@Permission");
                return permission;
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
        /// 设置岗位的文档权限
        /// </summary>
        /// <param name="documentID">文件ID</param>
        /// <param name="documentPermissionIDs">文件权限ID</param>
        /// <param name="organizationIDs">组织结构ID</param>
        /// <param name="userObjectIDs">岗位ID或则用户ID</param>
        /// <param name="userObjectTypes">标志岗位或则用户</param>
        /// <param name="permissions">权限列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="setById">设置人</param>
        /// <returns>返回ManyResultWithRowIndex</returns>
        ManyResultData SetDocumentPermissionInfo(
            Guid documentID,
            Guid?[] documentPermissionIDs,
            Guid?[] organizationIDs,
            Guid?[] userObjectIDs,
            UserObjectType?[] userObjectTypes,
            DocuentPermission[] permissions,
            DateTime?[] updateDates,
            Guid setById)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspSetDocumentPermissionInfo");

                string tempFilePermissionIds = documentPermissionIDs.Join();
                string tempOrganizationIds = organizationIDs.Join();
                string tempUserObjectIDs = userObjectIDs.Join();
                string tempUserObjectTypes = userObjectTypes.Join<UserObjectType?>();
                string tempPermissions = permissions.Join<DocuentPermission>();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@DocumentID", DbType.Guid, documentID);
                db.AddInParameter(dbCommand, "@DocumentPermissionIds", DbType.String, tempFilePermissionIds);
                db.AddInParameter(dbCommand, "@OrganizationIds", DbType.String, tempOrganizationIds);
                db.AddInParameter(dbCommand, "@UseObjectIDs", DbType.String, tempUserObjectIDs);
                db.AddInParameter(dbCommand, "@UseObjectTypes", DbType.String, tempUserObjectTypes);
                db.AddInParameter(dbCommand, "@Permissions", DbType.String, tempPermissions);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@SetById", DbType.Guid, setById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                //DataSet ds = null;
                //ds = db.ExecuteDataSet(dbCommand);
                //if (ds == null || ds.Tables.Count < 1)
                //{
                //    return null;
                //}

                ManyResultData result = db.ManyResult(dbCommand);

                //ManyResultWithRowIndex results = (from b in ds.Tables[0].AsEnumerable()
                //                                  select new ManyResultWithRowIndex
                //                                  {
                //                                      Results = (from c in ds.Tables[1].AsEnumerable()
                //                                                 select new SingleResultWithRowIndex
                //                                                 {
                //                                                     ID = c.Field<Guid>("ID"),
                //                                                     UpdateDate = c.Field<DateTime?>("UpdateDate"),
                //                                                 }).ToList(),
                //                                  }).SingleOrDefault();

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
        /// 删除问档权限信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="updateDates">数据版本</param>
        /// <param name="removeByID">删除人</param>
        void RemoveDocumentPermissionInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspRemoveDocumentPermissionInfo");

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
        /// 获取所有文档信息
        /// </summary>
        /// <returns>返回</returns>
        public List<DocumentFileInfo> GetAllDocumentFileInfos()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string strQuery = @"SELECT oaf.id,oaf.FolderID,oaf.[FileName],oaf.CreateBy AS CreateByID,smv.Code AS CreateByName,oaf.CreateDate,oaf.UpdateDate,oaf.StreamID,oaf.FileDescription 
                                        FROM oa.Files oaf INNER JOIN sm.VUsers smv ON smv.ID=oaf.CreateBy WHERE [StreamID] IS NULL";
                DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DocumentFileInfo> result = (from b in ds.Tables[0].AsEnumerable()
                                           select new DocumentFileInfo
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               FolderID = b.Field<Guid>("FolderID"),
                                               FileName = b.Field<String>("FileName"),
                                               CreateByID = b.Field<Guid>("CreateByID"),
                                               CreateByName = b.Field<String>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               StreamID = b.Field<Guid?>("StreamID"),
                                               FileDescription = b.Field<String>("FileDescription"),
                                           }).ToList();

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

    }
}

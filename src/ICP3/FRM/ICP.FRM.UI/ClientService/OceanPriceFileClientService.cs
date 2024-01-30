using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;

namespace ICP.FRM.UI.ClientService
{
    /// <summary>
    /// 运价文件客户端服务
    /// </summary>
    public class OceanPriceFileClientService : IOceanPriceFileClientService
    {
        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 运价服务
        /// </summary>
        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        
        }
        /// <summary>
        /// 客户端文件上传服务
        /// </summary>
        public IClientFileService ClientFileService
        {
            get { return ServiceClient.GetClientService<IClientFileService>(); }
        }

        #endregion

        #region IOceanPriceFileClientService 成员
        /// <summary>
        /// 获取运价文件列表
        /// </summary>
        /// <param name="oceanID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<OceanFileList> GetOceanFileList(Guid oceanID, Guid userID)
        {
            return OceanPriceService.GetOceanFileList(oceanID, userID);
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public byte[] DownloadOceanFileList(Guid fileID)
        {
            try
            {
                ContentInfo doInfo = ClientFileService.DownloadOADocument(fileID);
                if (doInfo != null)
                    return doInfo.Content;
                throw new Exception(LocalData.IsEnglish ? "file does not exist" : "文件不存在");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}:{1}", LocalData.IsEnglish ? "download failed" : "下载失败",ex.Message));
            }
            #region 从FTP下载文件
            //string ownerAccountFolderPath = FTPServerConfig.BasePath;

            //FTPClient ftp = new FTPClient();
            //try
            //{
            //    ftp.RemoteHost = FTPServerConfig.Host;
            //    ftp.RemotePath = ownerAccountFolderPath;
            //    ftp.UserName = FTPServerConfig.User;
            //    ftp.Password = FTPServerConfig.Password;
            //    ftp.Login();
            //    return ftp.DownloadContent(fileID.ToString(), false);
            //}
            //catch (Exception ex)
            //{
            //    ftp.Close();
            //    throw ex;
            //}
            //finally { ftp.Close(); } 
            #endregion
        }
        /// <summary>
        /// 移除运价文件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="removeByID"></param>
        /// <param name="updateDates"></param>
        public void RemoveOceanFileInfo(Guid[] ids, Guid removeByID, DateTime?[] updateDates)
        {
            OceanPriceService.RemoveOceanFileInfo(ids, removeByID, updateDates);

            #region 删除FTP服务器上的文件
            //string ownerAccountFolderPath = FTPServerConfig.BasePath;

            //FTPClient ftp = new FTPClient();
            //try
            //{
            //    ftp.RemoteHost = FTPServerConfig.Host;
            //    ftp.RemotePath = ownerAccountFolderPath;
            //    ftp.UserName = FTPServerConfig.User;
            //    ftp.Password = FTPServerConfig.Password;
            //    ftp.Login();

            //    foreach (Guid item in ids)
            //    {
            //        ftp.DeleteRemoteFile(item.ToString());
            //    }
            //}
            //catch(Exception ex)
            //{
            //    ftp.Close(); 
            //    throw ex;
            //}
            //finally { ftp.Close(); } 
            #endregion
        }

        public ManyResult SaveOceanFileInfo(Guid oceanID, Guid?[] ids, string[] names, string[] descriptions, byte[][] fileContent, Guid saveByID, DateTime?[] updateDates)
        {

            List<Guid> savedIds = new List<Guid>();
            //FTPClient ftp = new FTPClient();
            try
            {
                ManyResult result = OceanPriceService.SaveOceanFileInfo(oceanID, ids, names, descriptions, saveByID, updateDates);

                for (int i = 0; i < result.Items.Count; i++)
                {
                    savedIds.Add(result.Items[i].GetValue<Guid>("ID"));
                }

                DocumentInfo[] dInfo = new DocumentInfo[result.Items.Count];
                for (int i = 0; i < result.Items.Count; i++)
                {
                    dInfo[i] = new DocumentInfo
                    {
                        Content = fileContent[i],
                        Id = result.Items[i].GetValue<Guid>("FileID"),
                        Name = names[i],
                        CreateBy = LocalData.UserInfo.LoginID,
                        CreateByName = LocalData.UserInfo.LoginName
                    };
                }
                ClientFileService.UplaodOADocument(dInfo);

                #region 上传到FTP服务器
                //string ownerAccountFolderPath = FTPServerConfig.BasePath;

                //ftp.RemoteHost = FTPServerConfig.Host;
                //ftp.RemotePath = ownerAccountFolderPath;
                //ftp.UserName = FTPServerConfig.User;
                //ftp.Password = FTPServerConfig.Password;
                //ftp.Login();

                //for (int i = 0; i < result.ChildResults.Count; i++)
                //{
                //    ftp.Upload(result.ChildResults[i].ID.ToString(), fileContent[i]);
                //}

                //ftp.Close(); 
                #endregion

                return result;
            }
            catch (Exception ex)
            {
                OceanPriceService.RemoveOceanFileInfo(savedIds.ToArray(), saveByID, updateDates);
                //ftp.Close(); 
                throw ex;
            }
            //finally { ftp.Close(); }
        }

        #endregion

        #region Comment Code

        /// <summary>
        /// FTP升级到云服务
        /// </summary>
        public void UpgradeCloud()
        {
            try
            {
//                Database db = DatabaseFactory.CreateDatabase();
//                string strQuery =
//                    @"SELECT T1.ID AS FileID,T2.ID,T2.[FileName],T2.[FileDescription],T2.[CreateBy],T2.CreateDate FROM frm.OceanFiles T1
//                                    INNER JOIN oa.Files T2 ON T1.FileID = T2.ID
//                                    WHERE T2.[StreamID] IS NULL  ORDER BY T2.CreateDate DESC";
//                DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

//                DataSet ds = null;
//                ds = db.ExecuteDataSet(dbCommand);
//                if (ds == null || ds.Tables.Count < 1)
//                {
//                    return;
//                }
//                Guid fileID = Guid.Empty;
                
//                foreach (var row in ds.Tables[0].AsEnumerable())
//                {
//                    fileID = row.Field<Guid>("FileID");
//                    DocumentInfo dInfo = new DocumentInfo
//                    {
//                        Id = row.Field<Guid>("ID"),
//                        Name = row.Field<String>("FileName"),
//                        CreateBy = LocalData.UserInfo.LoginID,
//                        CreateByName = LocalData.UserInfo.LoginName
//                    };

//                    string ownerAccountFolderPath = FTPServerConfig.BasePath;
//                    FTPClient ftp = new FTPClient();
//                    try
//                    {
//                        ftp.RemoteHost = FTPServerConfig.Host;
//                        ftp.RemotePath = ownerAccountFolderPath;
//                        ftp.UserName = FTPServerConfig.User;
//                        ftp.Password = FTPServerConfig.Password;
//                        ftp.Login();
//                        ftp.SetBinaryMode(false);
//                        dInfo.Content = ftp.DownloadContent(fileID.ToString(), false);
//                        ClientFileService.UplaodOADocument(new[] { dInfo });
//                    }
//                    catch (Exception ex)
//                    {
//                        ftp.Close();
//                    }
//                    finally
//                    {
//                        ftp.Close();
//                    }
//                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //FTPServerConfig _ftpServerConfig = null;
        ///// <summary>
        ///// FTP服务器配置信息
        ///// </summary>
        //private FTPServerConfig FTPServerConfig
        //{
        //    get
        //    {
        //        if (_ftpServerConfig == null)
        //        {
        //            _ftpServerConfig = OceanPriceService.GetFTPServerConfig();
        //        }

        //        return _ftpServerConfig;
        //    }
        //} 
        #endregion
    }
}

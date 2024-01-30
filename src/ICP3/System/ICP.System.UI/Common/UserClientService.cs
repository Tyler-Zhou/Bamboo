using System;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using System.Transactions;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.UI.Common
{
    public class UserClientService : IUserClientService
    {
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
                    _ftpServerConfig = this.UserService.GetFTPServerConfig();
                }

                return _ftpServerConfig;
            }
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion

        #region IUserClientService 成员

        /// <summary>
        /// 下载文件
        /// </summary>
        public byte[] DownloadUserPhoto(Guid fileID)
        {
            string ownerAccountFolderPath = this.FTPServerConfig.BasePath;

            FTPClient ftp = new FTPClient();
            try
            {
                ftp.RemoteHost = this.FTPServerConfig.Host;
                ftp.RemotePath = ownerAccountFolderPath;
                ftp.UserName = this.FTPServerConfig.User;
                ftp.Password = this.FTPServerConfig.Password;
                ftp.Login();
                return ftp.DownloadContent(fileID.ToString(), false);
            }
            catch (Exception ex)
            {
                ftp.Close();
                throw ex;
            }
            finally { ftp.Close(); }
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="gender">性别</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="mobile">移动电话</param>
        /// <param name="saveById">修改人Id</param>
        /// <param name="address">地址</param>
        /// <param name="remark">remark</param>
        /// <param name="birthday">birthday</param>
        /// <param name="photo">photo</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        public  SingleResultData SaveUserDetailInfo(
            Guid id,
            string cname,
            string ename,
            GenderType gender,
            string tel,
            string fax,
            string mobile,
            string address,
            string remark,
            DateTime? birthday,
            byte[] photo,
            Guid saveById,
            DateTime? updateDate)
        {

            FTPClient ftp = new FTPClient();

            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    SingleResultData result = UserService.SaveUserDetailInfo(
                        id, cname, ename, gender, tel, fax, mobile, address, remark, birthday, LocalData.UserInfo.LoginID, updateDate);

                    #region 上传到FTP服务器
                    if (photo != null)
                    {

                        string ownerAccountFolderPath = this.FTPServerConfig.BasePath;
                        ftp.RemoteHost = this.FTPServerConfig.Host;
                        ftp.RemotePath = ownerAccountFolderPath;
                        ftp.UserName = this.FTPServerConfig.User;
                        ftp.Password = this.FTPServerConfig.Password;
                        ftp.Login();
                        ftp.Upload(id.ToString(), photo);
                        ftp.Upload(id.ToString()+".jpg", photo);
                    }
                    ftp.Close();
                    #endregion

                    scope.Complete();
                    return result;

                }
            }
            catch (Exception ex) { ftp.Close(); throw ex; }
            finally { ftp.Close(); }
        }

        #endregion
    }
}

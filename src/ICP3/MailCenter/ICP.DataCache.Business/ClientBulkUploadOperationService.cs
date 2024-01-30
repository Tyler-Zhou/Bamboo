using ICP.DataCache.ServiceInterface;

namespace ICP.DataCache.LocalOperation
{
    /// <summary>
    /// 批量上传数据类
    /// </summary>
    public class ClientBulkUploadOperationService : IClientBulkUploadOperationService
    {
        #region 成员变量

        #endregion

        #region Service

        #endregion

        /// <summary>
        /// 批量保存用户操作日志到数据库
        /// </summary>
        public void BulkUploadUserOperationLog()
        {
        }

        /// <summary>
        /// 批量上传关联信息
        /// </summary>
        public void BulkUploadMessageRelation()
        {
        }

        /// <summary>
        /// 批量上传邮件备份到服务器
        /// </summary>
        public void BulkUploadBackupMail()
        {
        }

        /// <summary>
        /// 读取本地文件：关联信息
        /// </summary>
        public void ReadLocalFileOperationMessage()
        {
            
        }

        /// <summary>
        /// 读取本地文件：操作联系人
        /// </summary>
        public void ReadLocalFileOperationContact()
        {
            
        }


        /// <summary>
        /// 重置异常数据
        /// </summary>
        /// <returns>执行结果</returns>
        public bool ResetExceptionData()
        {
            return true;
        }

    }
}

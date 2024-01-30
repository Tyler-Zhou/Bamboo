namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 批量上传数据到服务端接口
    /// </summary>
    public interface IClientBulkUploadOperationService
    {
        /// <summary>
        /// 批量上传用户操作日志到数据库
        /// </summary>
        void BulkUploadUserOperationLog();

        /// <summary>
        /// 批量上传关联信息
        /// </summary>
        void BulkUploadMessageRelation();

        /// <summary>
        /// 批量上传邮件备份到服务器
        /// </summary>
        void BulkUploadBackupMail();

        /// <summary>
        /// 读取本地文件：关联信息
        /// </summary>
        void ReadLocalFileOperationMessage();

        /// <summary>
        /// 读取本地文件：操作联系人
        /// </summary>
        void ReadLocalFileOperationContact();

        /// <summary>
        /// 重置异常数据
        /// </summary>
        bool ResetExceptionData();
    }
}

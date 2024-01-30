namespace ICP.FileSystem.ServiceInterface
{
    /// <summary>
    /// 文件系统服务Host
    /// </summary>
    public interface IFileSystemServiceHost
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        void StartService();
        /// <summary>
        /// 停止服务
        /// </summary>
        void StopService();
    }
}

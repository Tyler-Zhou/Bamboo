#region Comment

/*
 * 
 * FileName:    FileSystemServiceHost.cs
 * CreatedOn:   2016/1/6 9:52:02
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.ServiceModel;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FileSystem.ServiceComponent
{
    /// <summary>
    /// 文件系统服务Host
    /// </summary>
    public class FileSystemServiceHost : IFileSystemServiceHost
    {
        /// <summary>
        /// 
        /// </summary>
        private ServiceHost _myServiceHost;
        /// <summary>
        /// 
        /// </summary>
        public void StartService()
        {
            _myServiceHost = new ServiceHost(typeof(FileSystemService));//实例化WCF服务对象
            _myServiceHost.Open();
        }
        /// <summary>
        /// 
        /// </summary>
        public void StopService()
        {
            if (_myServiceHost.State != CommunicationState.Closed) 
                _myServiceHost.Close();
        }
    }
}

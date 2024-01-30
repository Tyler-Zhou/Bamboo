using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ICP.FileSystem.Service
{
    /// <summary>
    /// 文件系统安装服务
    /// </summary>
    [RunInstaller(true)]
    public partial class FileSystemServiceInstaller : Installer
    {
        private ServiceProcessInstaller spInstaller;
        private ServiceInstaller sInstaller;

        /// <summary>
        /// 
        /// </summary>
        public FileSystemServiceInstaller()
        {
            InitializeComponent();
            InitInstaller();
        }

        /// <summary>
        /// 初始化安装服务
        /// </summary>
        private void InitInstaller()
        {
            spInstaller = new ServiceProcessInstaller();

            sInstaller = new ServiceInstaller();

            // 设定ServiceProcessInstaller对象的帐号、用户名和密码等信息

            spInstaller.Account = ServiceAccount.LocalSystem;
            spInstaller.Username = null;
            spInstaller.Password = null;
            // 设定服务名称
            sInstaller.ServiceName = "ICPFileSystemService";

            // 设定服务的启动方式
            sInstaller.StartType = ServiceStartMode.Automatic;
            Installers.AddRange(new Installer[] { spInstaller, sInstaller });
        }
    }
}

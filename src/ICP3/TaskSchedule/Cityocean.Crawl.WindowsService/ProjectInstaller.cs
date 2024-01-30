using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using Microsoft.Win32;

namespace Cityocean.Crawl.WindowsService
{
    /// <summary>
    /// 
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        /// <summary>
        /// 
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
            spInstaller.Account = ServiceAccount.LocalSystem;
            spInstaller.Username = null;
            spInstaller.Password = null;
            sInstaller.ServiceName = "ICPCrawlService";
            sInstaller.DisplayName = "ICP Crawl Service";
            sInstaller.Description = "爬虫服务：抓取并解析货物动态、码头船期、码头箱状态";
            // 设定服务的启动方式
            sInstaller.StartType = ServiceStartMode.Automatic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateSaver"></param>
        public override void Install(IDictionary stateSaver)
        {
            RegistryKey system;
            //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
            RegistryKey currentControlSet = null;
            //...\Services
            RegistryKey services = null;
            //...\<Service Name>
            RegistryKey service = null;
            //...\Parameters - this is where you can put service-specific configuration
            RegistryKey config;
            try
            {
                //Let the project installer do its job
                base.Install(stateSaver);

                //Open the HKEY_LOCAL_MACHINE\SYSTEM key
                system = Registry.LocalMachine.OpenSubKey("System");
                //Open CurrentControlSet
                if (system != null) currentControlSet = system.OpenSubKey("CurrentControlSet");
                //Go to the services key
                if (currentControlSet != null) services = currentControlSet.OpenSubKey("Services");
                //Open the key for your service, and allow writing
                if (services != null) service = services.OpenSubKey(sInstaller.ServiceName, true);
                //Add your service's description as a REG_SZ value named "Description"
                if (service != null)
                {
                    service.SetValue("Description", "爬虫服务：抓取并解析货物动态、码头船期、码头箱状态");
                    //(Optional) Add some custom information your service will use...
                    //允许服务与桌面交互
                    service.SetValue("Type", 0x00000110);
                    config = service.CreateSubKey("Parameters");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown during service installation:\n" + e.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedState"></param>
        public override void Uninstall(IDictionary savedState)
        {
            RegistryKey system = null;
            RegistryKey currentControlSet = null;
            RegistryKey services = null;
            RegistryKey service = null;

            try
            {
                //Drill down to the service key and open it with write permission
                system = Registry.LocalMachine.OpenSubKey("System");
                if (system != null) currentControlSet = system.OpenSubKey("CurrentControlSet");
                if (currentControlSet != null) services = currentControlSet.OpenSubKey("Services");
                if (services != null) service = services.OpenSubKey(sInstaller.ServiceName, true);
                //Delete any keys you created during installation (or that your service created)
                if (service != null) service.DeleteSubKeyTree("Parameters");
                //...
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception encountered while uninstalling service:\n" + e);
            }
            finally
            {
                //Let the project installer do its job
                base.Uninstall(savedState);
            }
        }
    }
}

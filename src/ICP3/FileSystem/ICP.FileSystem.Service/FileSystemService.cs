using System;
using System.ServiceProcess;
using ICP.FileSystem.ServiceComponent;
using ICP.FileSystem.ServiceInterface;
using ICP.EDIManager.ServiceComponent;
using ICP.EDIManager.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FileSystem.Service
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ICPFileSystemService : ServiceBase
    {
        IFileSystemServiceHost hostBase = null;
        //IEDIManagerServiceHost edihost = null;
        IEDIManagerService EDIManager = null;

        /// <summary>
        /// 
        /// </summary>
        public ICPFileSystemService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                if (hostBase == null)
                {
                    hostBase = new FileSystemServiceHost();

                }
                hostBase.StartService();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("FileSystem", CommonHelper.BuildExceptionString(ex));
            }

            try
            {
                EDIManager = new EDIManagerService();
                EDIManager.Download();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("EDIManager", CommonHelper.BuildExceptionString(ex));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                if (hostBase != null)
                {
                    hostBase.StopService();
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("FileSystem", CommonHelper.BuildExceptionString(ex));
            }

            try
            {
                if (EDIManager != null)
                {
                    EDIManager.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("FileSystem", CommonHelper.BuildExceptionString(ex));
            }
        }
    }
}

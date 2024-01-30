using System;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary;
using System.Threading;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using System.IO;
using ICP.Framework.CommonLibrary.Helper;

namespace ICPMailCenter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] parameters)
        {
            try
            {
                //Thread.Sleep(10000);

                //很难把控最小线程数，在不必要的情况下增加这些值，
                //可能会导致性能问题。 如果同时启动的任务过多，则所有任务的处理速度看起来都可能很慢
                //大多数情况下，线程池使用自己的分配线程的算法将能够更好地工作。 
                //将空闲线程数的最小值减少到小于处理器的数目也会影响性能
                //http://msdn.microsoft.com/zh-cn/library/system.threading.threadpool.setminthreads(v=vs.110).aspx
                //ThreadPool.SetMinThreads(100, 20);

                //记录打开邮件中心花费时间
                //MailUtility.StartStopwatch();
                MailUtility.AppStartArgs = parameters;//邮件中心程序启动参数
                LocalData.IsDesignMode = false;//全局变量，当前程序是否处于设计模式
                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.EnableVisualStyles();//启用应用程式的可视样式
                Application.SetCompatibleTextRenderingDefault(false);
                InitializeEnvironment();
                MainApplication application = new MainApplication();
                application.Run();
            }
            catch (Exception ex)
            {
                ClientHelper.ReleaseWaitHandle(ClientHelper.GetAppSettingValue(ClientConstants.EmailCenterNameKey));
                ShowError(ex);
            }



        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowError(e.ExceptionObject as Exception);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowError(e.Exception);
        }
        /// <summary>
        /// 对InnerException做一次递归，以记录尽可能多的异常信息
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowError(Exception ex)
        {

            MailUtility.ReleaseWaitHandle();

            string message = CommonHelper.BuildExceptionString(ex);

            Logger.Log.Error(DateTime.Now.ToString() + Environment.NewLine + message);
            if (LocalCommonServices.ErrorTrace != null && LocalCommonServices.ErrorTrace.CurrentOwner != null)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
            }
            try
            {
                ISystemErrorLogService errorLogService = ServiceClient.GetService<ISystemErrorLogService>();
                using (MemoryStream stream = new MemoryStream())
                {
                    (new SnapScreenHelper()).SnapFullScreen().Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    errorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName, LocalData.SessionId, stream.ToArray(), message, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }
            }
            catch
            {

            }
        }

        private static void SetLoginUserInfo()
        {

            ILoginUserInfoService loginUserInfoService = ServiceClient.GetService<ILoginUserInfoService>();//根据接口类型获取服务代理实例
            LoginUserInfo userInfo = loginUserInfoService.Get();//获取登陆用户信息
            LocalData.AdministratorId = userInfo.AdministratorId;
            LocalData.ClientId = userInfo.ClientId;
            LocalData.CultureName = userInfo.CultureName;
            LocalData.EmailHost = userInfo.EmailHost;
            LocalData.EnableCustomDataGrid = userInfo.EnableCustomDataGrid;
            LocalData.Height = userInfo.Height;
            LocalData.IsDesignMode = userInfo.IsDesignMode;
            LocalData.PortalType = userInfo.PortalType;
            LocalData.SessionId = userInfo.SessionId;
            LocalData.SkinName = userInfo.SkinName;
            LocalData.SystemConfigInfoList = userInfo.SystemConfigInfoList;
            LocalData.SystemNGenVersionNo = userInfo.SystemNGenVersionNo;
            LocalData.SystemUpdateVersionNo = userInfo.SystemUpdateVersionNo;
            LocalData.SystemVersionNo = userInfo.SystemVersionNo;
            LocalData.UserInfo = userInfo.UserInfo;
            LocalData.DataSyncFinished = userInfo.DataSyncFinished;
            LocalCommonServices.PermissionService = userInfo.PermissionPackage;
        }
        private static void InitializeEnvironment()
        {
            SetLoginUserInfo();//获取用户信息赋给全局变量
            ClientHelper.SetApplicationContext();  //为处理多线程情况，在当前线程设置应用程序上下文

        }

    }
}

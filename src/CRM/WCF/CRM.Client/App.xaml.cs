using AutoMapper;
using CRM.Client.Common;
using CRM.Client.DataAccess;
using CRM.Client.Interfaces;
using CRM.Client.Services;
using CRM.Client.ViewModels;
using CRM.Client.Views;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CRM.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        #region 成员(Member)
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 事件(Event)
        /// <summary>
        /// 调度程序未处理的异常
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //通常全局异常捕捉的都是致命信息
            _Logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
        }

        /// <summary>
        /// 未观察到的任务异常
        /// </summary>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _Logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
        }
        /// <summary>
        /// 未处理的异常
        /// </summary>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
                _Logger.LogCritical($"{ex.StackTrace},{ex.Message}");
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 创建外壳
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            //多线程异常
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            return Container.Resolve<MainView>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            //var service = Current.MainWindow.DataContext as IConfigureService;
            //if (service != null)
            //{
            //    service.ConfigureContent();
            //}
            var result = Task.Run(() => InitSetting().Result).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        async Task<bool> InitSetting()
        {
            AppSession.UserId = 1;
            //GenerateEntities();
            return true;
        }

        void GenerateEntities()
        {
            //实体生成的路径
            var modelpath = @"CRM.Client\..\Entities";
            var path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.IndexOf(@"\bin"));
            //声明实体生成的绝对路径
            path = path.Substring(0, path.LastIndexOf(@"\") + 1) + modelpath;


            var db = new SqlSugarClient(new ConnectionConfig()
            {
                //连接字符串
                ConnectionString = @"Data Source=.;Initial Catalog=CRM;User=sa;Password=DATABASE",
                //连接的数据库的类型
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            //获取所有表
            List<DbTableInfo> list = db.DbMaintenance.GetTableInfoList();
            //获取所有视图
            List<DbTableInfo> viewList = db.DbMaintenance.GetViewInfoList();

            var allList = list.Concat(viewList);

            //循环所有的表和视图 他们属于同一个类 DbTableInfo
            foreach (DbTableInfo table in allList)
            {
                //首字母转大写 
                string table_name = table.Name.Substring(0, 1).ToUpper() + table.Name.Substring(1);
                //映射表增加 实体名称 和表名称
                db.MappingTables.Add(table_name, table.Name);
                //根据表名 获取表所有的字段
                List<DbColumnInfo> dd = db.DbMaintenance.GetColumnInfosByTableName(table.Name);
                foreach (DbColumnInfo item in dd)
                {
                    //映射字段添加 （字段名，字段名，表名）
                    db.MappingColumns.Add(item.DbColumnName, item.DbColumnName, table_name);
                }
                db.DbFirst.
                SettingClassTemplate(old =>
                {
                    return old;
                    //这里是自定义的模板 当你定义模板时 请注意格式的缩减距离 要不然会导致生成的模板格式很奇怪
                    // return GetClassTemplate();

                })
                .SettingNamespaceTemplate(old =>
                {
                    return old;
                })
                .SettingPropertyDescriptionTemplate(old =>
                {
                    return old;

                    //这里是自定义的模板
                    //return GetPropertyDescriptionTemplate();
                })
                .SettingPropertyTemplate(old =>
                {
                    return old;
                })
                .SettingConstructorTemplate(old =>
                {
                    return old;
                }).IsCreateAttribute().Where(table.Name).CreateClassFile(path, "CRM.Client.Entities");
            }
        }

        /// <summary>
        /// 这是自定义的命名空间的模板
        /// </summary>
        /// <returns></returns>
        string GetClassTemplate()
        {
            return @"
            {using}
            namespace {Namespace}
            {
            {ClassDescription}{SugarTable}
            public partial class {ClassName}:ModelContext
            {
                 public {ClassName}()
                 {
                     {Constructor}}{PropertyName}}
                  }";
        }

        /// <summary>
        /// 这是自定义的字段的模板
        /// </summary>
        /// <returns></returns>
        string GetPropertyDescriptionTemplate()
        {
            return @"        
                /// <summary>
                /// Remark:{PropertyDescription}
                /// Default:{DefaultValue}
                /// Nullable:{IsNullable}
                /// </summary>";
        }

        /// <summary>
        /// 注册 (视图、视图模型、服务)
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAutoMapperProvider, AutoMapperProvider>();
            containerRegistry.Register(typeof(IMapper), GetMapper);

            var factory = new NLogLoggerFactory();
            _Logger = factory.CreateLogger("CRM");
            //注入到Prism DI容器中
            containerRegistry.RegisterInstance(_Logger);
            //Repository
            containerRegistry.RegisterSingleton<UserRepository, UserRepository>();
            
            //Service
            //containerRegistry.Register<IConfigureService, MainViewModel>();
            containerRegistry.RegisterSingleton<IWindowService, MainViewModel>();
            containerRegistry.Register<INavigationService, MainViewModel>();
            containerRegistry.Register<IUserService, UserService>();

            //View & ViewModel
            containerRegistry.RegisterForNavigation<UserView, UserViewModel>();
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private IMapper GetMapper(IContainerProvider container)
        {
            var provider = container.Resolve<IAutoMapperProvider>();
            return provider.GetMapper();
        }
        #endregion
    }
}

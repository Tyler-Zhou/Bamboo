﻿using CRM.Client.Interfaces;
using CRM.Client.Models;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace CRM.Client.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        #region 成员(Member)

        #region 集合
        private ObservableCollection<UserInfo> _UserInfos = new ObservableCollection<UserInfo>();
        /// <summary>
        /// 集合
        /// </summary>
        public ObservableCollection<UserInfo> UserInfos
        {
            get { return _UserInfos; }
            set { _UserInfos = value; RaisePropertyChanged(); }
        }
        #endregion

        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        /// <summary>
        /// 应用程序服务
        /// </summary>
        public readonly IUserService _UserService;
        #endregion

        #region 命令(Command)

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cacheService"></param>
        /// <param name="windowService">主窗体服务</param>
        /// <param name="logger"></param>
        public UserViewModel(IContainerProvider provider, ILogger logger) : base(provider)
        {
            _Logger = logger;
            _UserService = ContainerProvider.Resolve<IUserService>();
            InitData();
        }
        #endregion

        #region 事件(Event)

        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            GetDataAsync();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        void GetDataAsync()
        {
            try
            {
                var returnResult =  _UserService.GetAll();

                if (returnResult!=null && returnResult.Count>0)
                {
                    UserInfos.Clear();
                    foreach (var item in returnResult)
                    {
                        UserInfos.Add(item);
                    }
                }
                else
                {
                }
            }
            finally
            {
            }
        }
        #endregion
    }
}
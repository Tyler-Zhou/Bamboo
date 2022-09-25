using DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Reader.Client.Events;
using Reader.Client.Extensions;
using Reader.Client.Interfaces;
using Reader.Client.Models;
using Reader.Client.Services;
using Reader.Client.Spider;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 下载视图模型
    /// </summary>
    public class DownloadViewModel : BaseViewModel
    {
        #region 成员(Member)
        #region 当前下载进程
        TaskProgress _CurrentTaskProgress;
        /// <summary>
        /// 当前下载进程
        /// </summary>
        TaskProgress CurrentTaskProgress
        {
            get
            {
                if (TaskProgresss != null && TaskProgresss.Count > 0)
                {
                    _CurrentTaskProgress = TaskProgresss.FirstOrDefault();
                }
                return _CurrentTaskProgress;
            }
            set
            {
                _CurrentTaskProgress = value;
                RaisePropertyChanged(nameof(CurrentTaskProgress));
            }
        }
        #endregion

        #region 下载进程集合
        ObservableCollection<TaskProgress> _TaskProgresss;
        /// <summary>
        /// 下载进程集合
        /// </summary>
        public ObservableCollection<TaskProgress> TaskProgresss
        {
            get
            {
                if (_TaskProgresss == null)
                    _TaskProgresss = new ObservableCollection<TaskProgress>();
                return _TaskProgresss;
            }
            set
            {
                _TaskProgresss = value;
                RaisePropertyChanged(nameof(TaskProgresss));
            }
        }
        #endregion
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供者(DryIOC)
        /// </summary>
        IContainerProvider _ContainerProvider;
        #endregion

        #region 命令(Commands)

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 下载视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public DownloadViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            _ContainerProvider = containerProvider;
            _EventAggregator.ResgiterTask(arg =>
            {
                AddTaskProgress(arg.Key,arg.Name);
            });
        }
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
        /// 初始化
        /// </summary>
        void InitData()
        {
            
        }

        void AddTaskProgress(string taskKey,string taskName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(taskKey))
                    return;
                if (string.IsNullOrWhiteSpace(taskName))
                    return;

                TaskProgress taskProgress = TaskProgresss.SingleOrDefault(item => item.Key.Equals(taskKey));
                if (taskProgress != null)
                    return;
                TaskProgress model = new TaskProgress(_ContainerProvider, taskKey, taskName);
                System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    TaskProgresss.Add(model);
                }));
                
                RaisePropertyChanged(nameof(TaskProgresss));
            }
            catch (Exception ex)
            {
                ShowError($"添加任务发生异常:{ex.Message}");
            }
        }
        #endregion
    }
}

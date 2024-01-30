#region Comment

/*
 * 
 * FileName:    .cs
 * CreatedOn:   2015/5/18 16:43:01
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

using Castle.Core;
using Castle.Core.Internal;
using ICP.DataOperation.SqlCE;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.DataCache
{
    /// <summary>
    /// 缓存数据库操作中心
    /// </summary>
    public partial class FormDataChache : Form, IServiceContainer
    {
        DefaultContainer container;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormDataChache()
        {
            InitializeComponent();
            Load += FormDataChache_Load;
            FormClosing += FormDataChache_FormClosing;
            Activated += FormDataChache_Activated;
            SetVisibleCore(false);
        }

        #region 事件处理

        void FormDataChache_Activated(object sender, EventArgs e)
        {
            Hide();
        }

        protected override sealed void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);
        }
        private void FormDataChache_Load(object sender, EventArgs e)
        {
            try
            {
                Text = LocalData.IsEnglish ? Text : "缓存数据操作中心";
                if (container == null)
                {
                    container = new DefaultContainer();
                    if (!container.Kernel.HasComponent("serviceContainer"))
                    {
                        container.Register(
                            Castle.MicroKernel.Registration.Component.For(typeof (IServiceContainer))
                                .Instance(this)
                                .Named("serviceContainer"));
                    }

                    object[] activatingComponents = ActivatingComponents();
                }
                SetLoginUserInfo();
                ClientHelper.SetApplicationContext(); //为处理多线程情况，在当前线程设置应用程序上下文
                FormClosing += SynchronizationHelper.Current.OnApplicationExit;
                SynchronizationHelper.Current.Synchronize();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                TopMost = false;
            }
        }

        private object[] ActivatingComponents()
        {

            GraphNode[] nodes = container.Kernel.GraphNodes;
            ArrayList arrayList = new ArrayList();

            foreach (var graphNode in nodes)
            {
                var node = (ComponentModel) graphNode;
                Type type = null;
                try
                {
                    if (node != null)
                    {
                        IEnumerator<Type> enumator = node.Services.GetEnumerator();
                        bool result = enumator.MoveNext();
                        type = enumator.Current;
                        if (type != null && !type.IsGenericTypeDefinition)
                            arrayList.Add(container.Kernel.Resolve(type));
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.SaveLog("在激活组件 [" + type + "] 的时候发生错误\r\n"+CommonHelper.BuildExceptionString(ex));
                }
            }
            return arrayList.ToArray();
        }

        /// <summary>
        /// 关闭窗体时保存面板位置，激发程序退出自定义事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDataChache_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (container != null)
                {
                    container.Dispose();
                    container = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
            }
        }
        #endregion

        #region IServiceContainer 成员

        public T GetService<T>()
        {
            return (T)container.Resolve(typeof(T));
        }

        /// <summary>
        /// 如果服务器上的config没有正确配置，要能返回空对象并记录日志
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public new object GetService(Type type)
        {
            if (container.Kernel.HasComponent(type))
            {
                return container.Resolve(type);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 登录
        /// <summary>
        /// 设置登录信息
        /// </summary>
        private void SetLoginUserInfo()
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
            LocalData.NeedBackUpMail =bool.Parse(AddInConfig.Current.GetValue("NeedBackUpMail", "False"));
            LocalCommonServices.PermissionService = userInfo.PermissionPackage;
        }
        #endregion
    }
}

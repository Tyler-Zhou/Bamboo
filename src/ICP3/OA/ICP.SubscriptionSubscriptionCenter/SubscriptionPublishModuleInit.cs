using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.SubscriptionPublish.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System.Configuration;

namespace ICP.SubscriptionPublish.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class SubscriptionPublishModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {
         #region init
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        List<Guid> userDepartmentIds = new List<Guid>();

        public SubscriptionPublishModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;    
        }

        public override void AddServices()
        {
            base.AddServices();
            _rootWorkItem.Services.AddNew<SubscriptionPublishNotifyClientService, ISubscriptionPublishNotifyService>();
            _rootWorkItem.Services.AddNew<ICPSubscriptionService,IICPSubscriptionService>();
            
        }

        public override void Load()
        {
            base.Load();
            GetLoginUserDepartmentInfo();
            RegisterCallbackService();
        }

        #endregion 
        #region 注册回调服务

        /// <summary>
        /// 获取用户组织信息，提供给回调服务使用 
        /// 备注：回调服务需要用到的用户信息，暂时为用户所属组织信息
        /// </summary>
        private void GetLoginUserDepartmentInfo()
        {
            List<LocalOrganizationInfo> orgList = ServiceClient.GetService<IFrameworkInitializeService>().GetDefaultOrganizationList(LocalData.UserInfo.LoginID, LocalData.IsEnglish);
            LocalData.UserInfo.UserOrganizationList = orgList;
            userDepartmentIds = (from info in LocalData.UserInfo.UserOrganizationList select info.ID).ToList<Guid>();
        }

        /// <summary>
        /// 注册回调服务集合
        /// </summary>
        private void RegisterCallbackService()
        {
            CallbackServiceConfigurationSection callbackServiceSection = ConfigurationManager.GetSection("CallbackServiceSection") as CallbackServiceConfigurationSection;
            if (callbackServiceSection == null || callbackServiceSection.Services == null || callbackServiceSection.Services.Count <= 0)
                return;
            ServiceCollection collection = callbackServiceSection.Services;
            Item[] items = GetItems(collection);
            Action<Item> addCallbackService = (item) =>
            {
                AddCallbackServiceToRootWorkItem(item);
            };
            Array.ForEach<Item>(items, addCallbackService);
            

        }
        /// <summary>
        /// 将配置中的回调服务添加到RootWorkItem
        /// </summary>
        /// <param name="item"></param>
        private void AddCallbackServiceToRootWorkItem(Item item)
        {
            if (item == null)
                return;
            Boolean isInstanceRegistered = item.IsCallbackInstanceExists;
            Type serviceType = Type.GetType(item.ServiceType);
            Type callbackType = Type.GetType(item.CallbackType);

            object instance = null;
            if (isInstanceRegistered)
            {
                instance = _rootWorkItem.Services.Get(callbackType);
                if (instance == null)
                {
                    throw new ICPException("无法从根WorkItem找到回调实现类型:" + callbackType);
                }
            }
            else
            {
                Type callbackInstanceType = Type.GetType(item.CallbackInstanceType);
                instance = _rootWorkItem.Services.AddNew(callbackInstanceType, callbackType);
            }
            object serviceInstance = ServiceClient.GetService(serviceType, instance);
            _rootWorkItem.Services.Add(serviceType, serviceInstance);

            ISubscriptionService subscriptionService = serviceInstance as ISubscriptionService;
            if (subscriptionService == null)
            {
                throw new ICPException("回调服务类型必须派生于ISubscriptionService");
            }
            string eventName = item.EventName;
            subscriptionService.Subscribe(eventName, userDepartmentIds,LocalData.ClientId);

        }

        private Item[] GetItems(ServiceCollection collection)
        {

            if (collection == null && collection.Count <= 0)
                return null;
            int count = collection.Count;
            Item[] items = new Item[count];
            for (int i = 0; i < count; i++)
            {
                items[i] = collection[i];
            }
            return items;
        }
        #endregion
    }
}

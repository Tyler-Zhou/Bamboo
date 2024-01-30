using System;
using ICP.Framework.CommonLibrary.Client;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.DataCache.BusinessOperation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true,UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ClientCustomDataGridService : IClientCustomDataGridService
    {
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;

        public ICustomDataGridService CustomDataGridService
        {
            get
            {
                return ServiceClient.GetService<ICustomDataGridService>();
            }
        }


        public ILocalBusinessCacheDataOperation LocalBusinessCacheDataOperation
        {
            get
            {
                return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>();
            }
        }
        #region IClientCustomDataGridService 成员

        public void Save(UserCustomGridInfo customInfo)
        {
            SingleResult result = CustomDataGridService.Save(customInfo);
            customInfo.Id = result.GetValue<Guid>("ID");
            customInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            LocalBusinessCacheDataOperation.SaveCustomGridInfo(customInfo);
        }

        public UserCustomGridInfo Get(Guid userId, string templateCode)
        {
            UserCustomGridInfo customInfo = LocalBusinessCacheDataOperation.GetCustomGridInfo(userId, templateCode);
            if (customInfo == null)
            {
                customInfo = CustomDataGridService.Get(userId, templateCode);
                if (customInfo != null)
                    LocalBusinessCacheDataOperation.SaveCustomGridInfo(customInfo);
            }
            return customInfo;

        }


        public UserCustomGridInfo Get(string templateCode)
        {
            return Get(LocalData.UserInfo.LoginID, templateCode);
        }


        #endregion
    }
}

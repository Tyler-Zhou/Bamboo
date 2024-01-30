using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.DataCache.ServiceInterface1;
using ICP.DataCache.LocalOperation1;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using System.ServiceModel;
using System.Data;

namespace ICP.DataCache.BusinessOperation1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
   public class ClientCustomDataGridService : IClientCustomDataGridService
    {
       string rootPath = AppDomain.CurrentDomain.BaseDirectory;
       [ServiceDependency]
       public ICustomDataGridService CustomDataGridService { get; set; }

       [ServiceDependency]
       public ILocalBusinessCacheDataOperation LocalBusinessCacheDataOperation { get; set; }
        #region IClientCustomDataGridService 成员

        public void Save(UserCustomGridInfo customInfo)
        {
            SingleResult result = CustomDataGridService.Save(customInfo);
            customInfo.Id = result.GetValue<Guid>("ID");
            customInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            LocalBusinessCacheDataOperation.SaveCustomGridInfo(customInfo);
        }

        public UserCustomGridInfo Get(Guid userId, ListFormType listType)
        {
            UserCustomGridInfo customInfo = LocalBusinessCacheDataOperation.GetCustomGridInfo(userId, listType);

            return customInfo; 
           
        }
  

        public UserCustomGridInfo Get(ListFormType listType)
        {
            return Get(LocalData.UserInfo.LoginID, listType);
        }

        #region joe 2013-05-20 添加

        public UserCustomGridInfo Get(Guid userId, string templateCode)
        {
            UserCustomGridInfo customInfo = LocalBusinessCacheDataOperation.GetCustomGridInfo(userId, templateCode);
            if (customInfo==null)
            {
               DataTable dt= CustomDataGridService.GetUserColumns(userId, templateCode);
               if (dt==null)
               {
                   dt = CustomDataGridService.GetUserColumns(null, templateCode);
               }
               if (dt != null)
               {
                   customInfo = DataCacheUtility.ConvertTableToUserCustomGridInfo(dt);
                   LocalBusinessCacheDataOperation.SaveCustomGridInfo(customInfo);
               }

            }
            return customInfo;

        }


        public UserCustomGridInfo Get(string templateCode)
        {
            return Get(LocalData.UserInfo.LoginID, templateCode);
        }
        #endregion  
        #endregion
    }
}

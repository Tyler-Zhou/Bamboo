using System;
using System.Collections.Generic;
using ICP.DataCache.LocalOperation1;
using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface1;
using System.ServiceModel;
namespace ICP.DataCache.BusinessOperation1
{  
    [ServiceBehavior(IncludeExceptionDetailInFaults=true,InstanceContextMode=InstanceContextMode.Single)]
   public class ClientBusinessContactService:IClientBusinessContactService,IDisposable
    {

       static Dictionary<string, EmailSourceType> types = new Dictionary<string, EmailSourceType>();
       [ServiceDependency]
       public ILocalBusinessCacheDataOperation LocalCacheDataOperation { get; set; }

       [ServiceDependency]
       public IBusinessContactService BusinessContactService { get; set; }
       public EmailSourceType GetContactPersonType(string senderAddress)
        {
            return BusinessContactService.GetContactPersonType(senderAddress);
        }



        #region IDisposable 成员

        public void Dispose()
        {
            if (types != null)
            {
                types.Clear();
                types = null;
            }
        }

        #endregion

        #region IClientBusinessContactService 成员

        public EmailSourceType Get(string senderAddress)
        {
            if (string.IsNullOrEmpty(senderAddress))
                return EmailSourceType.Unknown;

         
            if (types.ContainsKey(senderAddress))
                return types[senderAddress];
            int? type = LocalCacheDataOperation.GetContactPersonType(senderAddress);
            if (type != null)
            {
                EmailSourceType value = (EmailSourceType)type;
                types.Add(senderAddress, value);
                return value;
            }
            else
            {
               
                EmailSourceType result = BusinessContactService.GetContactPersonType(senderAddress);

                LocalCacheDataOperation.SaveContactPersonType(senderAddress, result.GetHashCode());
                types.Add(senderAddress,result);
                return result;

            }
        }
        public void Save(string senderAddress, EmailSourceType sourceType)
        {
            LocalCacheDataOperation.SaveContactPersonType(senderAddress, sourceType.GetHashCode());
        }

        #endregion

    }
}

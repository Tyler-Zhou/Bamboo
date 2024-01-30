using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using ICP.DataCache.LocalOperation1;

namespace ICP.DataCache.BusinessOperation1
{
   public  class BusinessServices
    {
       private static IClientFileService _clientFileService;
       public static IClientFileService ClientFileService
       {
           get {

               if (_clientFileService == null)
               {
                   _clientFileService = new ClientFileService();
               }
               return _clientFileService;
               
           }
       }
       private static ILocalBusinessCacheDataOperation _localBusinessOperation;
       public static ILocalBusinessCacheDataOperation LocalBusinessOperation
       {
           get
           {

               if (_localBusinessOperation == null)
               {
                   _localBusinessOperation = new LocalBusinessCacheDataOperation();
               }
               return _localBusinessOperation;

           }
        
       }
       
    }
}

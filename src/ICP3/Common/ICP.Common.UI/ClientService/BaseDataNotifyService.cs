using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI
{  
    /// <summary>
    /// 基础数据通知服务实现类
    /// </summary>
   public class BaseDataNotifyService:IBaseDataNotifyService
    {

       private IClientBaseDataService ClientBaseDataService
       {
           get
           {
               return ServiceClient.GetClientService<IClientBaseDataService>();
           }
       }

        public void UpdateBaseData(BaseDataType dataType, List<BaseDataInfo> items, List<Guid> deleteIds)
        {
            ClientBaseDataService.Update(dataType, items, deleteIds);
        }

       
    }
}

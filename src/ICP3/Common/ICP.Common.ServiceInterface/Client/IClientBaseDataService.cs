
using System.Collections.Generic;
using System;
namespace ICP.Common.ServiceInterface
{  
    /// <summary>
    /// 客户端基础数据服务接口
    /// </summary>
   public interface IClientBaseDataService:IBaseDataService
    {  
              /// <summary>
       /// 更新指定键值的缓存数据
       /// 数据必须至少包含Id属性
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dataType"></param>
       /// <param name="deleteIds">需删除的基础数据Id列表</param>
       /// <param name="items"></param>
       void Update(BaseDataType dataType, List<BaseDataInfo> items, List<Guid> deleteIds);
    }
}

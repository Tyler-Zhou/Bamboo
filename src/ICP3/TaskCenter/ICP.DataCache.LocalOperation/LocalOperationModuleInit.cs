using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace ICP.DataCache.LocalOperation1
{
   public class LocalOperationModuleInit:ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// 注入客户端服务
        /// </summary>
        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<LocalBusinessCacheDataOperation, ILocalBusinessCacheDataOperation>();


        }
    }
}

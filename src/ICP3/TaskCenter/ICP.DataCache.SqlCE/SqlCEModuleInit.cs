using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataOperation.SqlCE1
{
   public class SqlCEModuleInit:ModuleInit
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
            SynchronizeData();
            RootWorkItem.Services.AddOnDemand<SqlCEOperation, IDataOperation>();
        }

        private void SynchronizeData()
        {
            // WaitCallback callback = (data) =>
            // {

            //ClientHelper.SetApplicationContext();
            SynchronizationHelper.Current.Synchronize();
            // };

            // ThreadPool.QueueUserWorkItem(callback);
        }

  
    }
}

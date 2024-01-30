using ICP.DataCache.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.DataOperation.Sqlite3
{
   public class Sqlite3ModuleInit:ModuleInit
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
            RootWorkItem.Services.AddNew<SqliteOperation, IDataOperation>();
        }
    }
}

using Microsoft.Practices.CompositeUI;

namespace ICP.DataCache.FileSystem
{
    public class FileSystemModuleInit : ModuleInit
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
            RootWorkItem.Services.AddNew<FileStoreOperation, IFileStoreOperation>();
            

        }
    }
}

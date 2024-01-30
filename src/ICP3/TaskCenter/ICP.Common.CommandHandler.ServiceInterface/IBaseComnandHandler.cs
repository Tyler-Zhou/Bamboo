using ICP.Operation.Common.UI;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 命令接口
    /// </summary>
      public  interface  IBaseComnandHandler
    {
        /// <summary>
        /// 当前的业务处理面板
        /// </summary>
         BaseBusinessPart CurrentBaseBusinessPart { get; set; }
    }
}

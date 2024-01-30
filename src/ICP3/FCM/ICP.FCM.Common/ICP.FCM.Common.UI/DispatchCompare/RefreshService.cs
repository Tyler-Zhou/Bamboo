using System;

namespace ICP.FCM.Common.UI
{
    /// <summary>
    /// 刷新下载列表文档状态服务
    /// </summary>
   public  class RefreshService
    {

        /// <summary>
        /// 刷新下载列表文档状态事件
        /// </summary>
       public  Action Refresh;

       public Action RefreshAcceptDispatchState;

       public Action RefreshAcceptReviseState;
       

       public EventHandler  RefreshHandler;
    }
   // public delegate  
}

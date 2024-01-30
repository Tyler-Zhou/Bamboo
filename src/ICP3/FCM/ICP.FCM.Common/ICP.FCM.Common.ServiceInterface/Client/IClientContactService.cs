using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.ServiceInterface
{  
    /// <summary>
    /// 客户端联系人更改通知服务接口
    /// </summary>
   public interface IClientContactNotifyService
    {
        /// <summary>
        /// 业务联系人保存成功后,推送更改到其他界面事件
        /// </summary>
        event EventHandler<CommonEventArgs<List<CustomerCarrierObjects>>> ContactChanged;

        
        /// <summary>
        /// 消息发送成功后，调用邮件中心和任务中心相关联处理动作
        /// </summary>
        /// <param name="source">触发事件的源控件</param>
        ///<param name="contactList">更改的业务联系人列表</param>
        void SendContactChange(object source, List<CustomerCarrierObjects> contactList);
    }
}

using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 客户端联系人更改通知服务实现类
    /// </summary>
    public class ClientContactNotifyService : IClientContactNotifyService
    {

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<CommonEventArgs<List<CustomerCarrierObjects>>> ContactChanged;

        /// <summary>
        /// 消息发送成功后，调用邮件中心和任务中心相关联处理动作
        /// </summary>
        /// <param name="source">触发事件的源控件</param>
        ///<param name="contactList">更改的业务联系人列表</param>
        public void SendContactChange(object source, List<CustomerCarrierObjects> contactList)
        {
            ContactChanged(source, new CommonEventArgs<List<CustomerCarrierObjects>>(contactList));
        }
    }
}

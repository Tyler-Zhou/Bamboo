using ICP.Message.ServiceInterface;
using System.ServiceModel;
using ICP.OA.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
namespace ICP.Message.Client
{   
    /// <summary>
    /// 消息客户端回调服务类
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
   public class MessageNotifyClientService:IMessageNotifyService
    {

        public IFaxClientService FaxClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFaxClientService>();
            }
        }

        #region IMessageNotifyService 成员

        public void ChangeState(ICP.Message.ServiceInterface.Message[] messages,MessageType type)
        {
            if (type == MessageType.Fax)
            {
                FaxClientService.ChangeState(messages);
            }

        }

        #endregion
    }
}

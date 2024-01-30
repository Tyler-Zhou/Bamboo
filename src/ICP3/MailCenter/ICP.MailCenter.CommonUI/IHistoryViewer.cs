
namespace ICP.MailCenter.CommonUI
{
   public interface IHistoryViewer
    {
       void Open(ICP.Message.ServiceInterface.Message message);
       void Reply(ICP.Message.ServiceInterface.Message message);
       void Resend(ICP.Message.ServiceInterface.Message message);
    }
 
}

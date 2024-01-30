using System;
using DevExpress.XtraEditors;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenter.CommonUI
{
   public class EventUtility
    {
       public static Action<AttachmentLabel,AttachmentContent> PreviewAction;
       public static Action<SimpleButton> ShowBodyAction;

    }
}

using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.DataCache.ServiceInterface;

namespace ICP.Business.Common.UI.Communication
{
    public interface ICommunicationHistoryList
    {
        CommunicationHistory Current
       {
           get;
       }
        List<CommunicationHistory> DataSource
       {
           get;
           set;
       }
        event EventHandler<CommonEventArgs<ICP.Message.ServiceInterface.Message>> CurrentChanged;
    }
}

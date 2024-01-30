using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.DataCache.ServiceInterface1
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DocumentNotifyService : IDocumentNotifyService
    {  
        [Dependency(NotPresentBehavior=NotPresentBehavior.CreateNew)]
        public DocumentNotifyClientService ClientService { get; set; }
        #region IDocumentNotifyService 成员
        public void Upload(NotifyType type, object data, object generateIds)
        {
            try
            {
                ClientService.Notify(type, data, generateIds);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    error += innerException.Message;
                    innerException = innerException.InnerException;
                }
                ICP.Framework.CommonLibrary.LogHelper.SaveLog(error);

            }
        }
        #endregion
    }
}

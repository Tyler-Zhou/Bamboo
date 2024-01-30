using System;
using System.ServiceModel;
using ICP.Framework.CommonLibrary;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.DataCache.ServiceInterface
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
                LogHelper.SaveLog(error);

            }
        }
        #endregion
    }
}

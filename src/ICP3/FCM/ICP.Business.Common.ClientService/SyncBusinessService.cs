using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Business.Common.ClientService
{
    public class SyncBusinessService : ISyncBusinessService
    {
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IClientBusinessOperationService>();
            }
        }

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="businessContext">业务内容</param>
        public void UpdateLocalBusinessData(BusinessOperationContext businessContext)
        {
            ClientBusinessOperationService.UpdateLocalBusinessData(businessContext.OperationID, businessContext.OperationType);
        }
    }
}

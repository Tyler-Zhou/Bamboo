using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 传真消息服务接口
    /// </summary>
    [ServiceContract]
    public interface IFaxMessageService
    {
        /// <summary>
        /// 保存传真实体
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        [OperationContract]
        ManyResult[] SaveFaxMessage(FaxMessageObjects entry);         

    }
}

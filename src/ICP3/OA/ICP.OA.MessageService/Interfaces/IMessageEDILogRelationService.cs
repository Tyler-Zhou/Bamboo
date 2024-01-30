using System;
using System.ServiceModel;
namespace ICP.Message.ServiceInterface
{  
    /// <summary>
    /// 消息与EDI信息关联服务接口
    /// </summary>
   [ServiceContract]
   public interface IMessageEDILogRelationService
    {  
       /// <summary>
       /// 根据消息Id获取关联记录
       /// </summary>
       /// <param name="iMessageId"></param>
       /// <returns></returns>
       [OperationContract]
       MessageEDILogRelation Get(Guid iMessageId);
       /// <summary>
       /// 保存关联记录
       /// </summary>
       /// <param name="relation"></param>
       [OperationContract]
       void Save(MessageEDILogRelation relation);
    }
}

using ICP.OA.ServiceInterface.DataObjects;
using System.ServiceModel;
namespace ICP.OA.ServiceInterface
{  
    /// <summary>
    /// 公告发布通知接口
    /// </summary>
   [ServiceContract]
   public interface IBulletinNotifyService
    {  
       /// <summary>
       /// 通知公告发布
       /// <param name="data">公共信息</param>
       /// </summary>
       [OperationContract(IsOneWay=true)]
       void Notify(BulletinData data);
    }
}

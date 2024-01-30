using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Operation.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Attributes;
namespace ICP.MailCenterFramework.ServiceInterface
{
    [ServiceInfomation("手动关联接口")]
    [ServiceContract]
   public interface IOperationSaveController
    {
        void InnerSaveOperationMessageRelation(MessageRelationParameter messageRelationParameter);
    }
}

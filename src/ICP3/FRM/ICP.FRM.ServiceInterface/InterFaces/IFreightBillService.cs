using System;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 运费
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IFreightBillService
    {
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAirOrderInfo(Guid OperationID, Guid SaveByID, bool IsEnglish);
    }
}

using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// FRM服务类
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBusinessInfoService : IBusinessWeeklyReportService
    {

    }
}

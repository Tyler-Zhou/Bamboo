using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;


namespace ICP.EDI.ServiceInterface
{
    /// <summary>
    /// EDI服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IEDIService: IEDILogService, IEDIConfigService, IEDISendService, IEDIPreviewService
    {
        
    }
}

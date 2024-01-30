

//-----------------------------------------------------------------------
// <copyright file="IFCMCommonService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.Common.ServiceInterface
{
    using Framework.CommonLibrary.Attributes;
    using System.ServiceModel;

    /// <summary>
    /// FCM公共服务
    /// </summary>
    [ServiceInfomation("FCM公共服务")]
    [ServiceContract]
    public interface IFCMCommonService : IBillOfLadingService, IAgentRequestService, IDocumentService, IFreightRateRequestService, IMailFaxLogService
        , IMemoService, IOperationCommonService, IBusinessService, IContactService, IStaffService
        , ICargoTrackingService, IQuotedPriceService, IECommerceService, ISynchronizeBillsService
        , IFeeService, INoticeService, IBookingDelegateService, ICSPBusinessService, IPurchaseOrderService
    {

    }
}

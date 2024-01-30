using ICP.FAM.ServiceInterface.InterFaces;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 财务服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IFinanceService : IWriteOffService, IReleaseBlService,
        IInvoiceService, IBusinessService, IBankService, ITelexApplyService,
        ICustomerBillService, IAgentBillCheckingService, IMonthlyClosingAgreementService,
        IBillService, IJournalService, IReleaseRCService, ILedgerService, IBankReceiptService,
        IBankDirectServie, ICSPFinanceService
    {

    }
}

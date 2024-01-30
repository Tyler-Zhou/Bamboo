using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using CommonConstants = ICP.Operation.Common.ServiceInterface.CommonConstants;

namespace ICP.FRM.UI.InquireRates
{
    public class InquirePricesBusinessInfoExtractor : IBusinessInfoExtractor
    {
        #region IBusinessInfoExtractor 成员

        public BusinessOperationContext Extract(object parameter, bool getRealTemplateCode)
        {
            ClientInquierOceanRate inquierRate = parameter as ClientInquierOceanRate;
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationType = OperationType.OceanExport;
            context[CommonConstants.AdvanceQueryStringKey] = string.Empty;
            context[CommonConstants.TemplateCodeKey] = UIConstants.ShipmentTemplateCode;
            return context;
        }

        #endregion
    }
}

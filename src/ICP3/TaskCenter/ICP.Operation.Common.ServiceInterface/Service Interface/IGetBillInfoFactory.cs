namespace ICP.Operation.Common.ServiceInterface
{
   public interface IGetBillInfoFactory
    {
       IGetRefNoCommand GetRefNoCommand(object billInfo);
       IGetDescriptionCommand GetDescriptionCommand(object billInfo);
    }
}

using DevExpress.XtraEditors;
namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 业务面板工厂接口
    /// </summary>
   public interface IBusinessPartFactory
    {
       T Get<T>(string templateCode, object parameter) where T : XtraUserControl, IBaseBusinessPart_New;
    }
}

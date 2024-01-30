using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace ICP.FAM.Contract
{
    /// <summary>
    /// 财务API服务接口
    /// </summary>
    public interface IFinanceAPIService
    {
        /// <summary>
        /// 批量支付
        /// </summary>
        /// <returns>付款结果JSON对象</returns>
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare
            , UriTemplate = "ALLBankList")]
        Message GetBankList();
    }
}

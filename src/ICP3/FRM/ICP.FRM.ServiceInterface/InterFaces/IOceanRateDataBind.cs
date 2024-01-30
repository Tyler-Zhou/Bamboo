
using ICP.FRM.ServiceInterface.DataObjects;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 询价绑定数据
    /// </summary>
    public interface IInquierRateDataBind
    {
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">绑定数据</param>
        void DataSourceBind(object data);
    }
}

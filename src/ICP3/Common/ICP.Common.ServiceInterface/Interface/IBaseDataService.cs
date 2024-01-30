using System.ServiceModel;
using System.Data;

namespace ICP.Common.ServiceInterface
{   
    /// <summary>
    /// 基础数据服务接口
    /// </summary>
    [ServiceContract]
   public interface IBaseDataService
    {   
        /// <summary>
        /// 获取基础数据
        /// 用于列表下拉数据源
        /// </summary>
        /// <param name="dataType">基础数据类型</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetBaseData(BaseDataType dataType);
    }
    /// <summary>
    /// 基础数据枚举
    /// </summary>
    public enum BaseDataType
    {
     //1,国家，2航线，3，用户，4 地点)
        /// <summary>
        /// 国家
        /// </summary>
      Country=1,
     /// <summary>
     /// 航线
     /// </summary>
      ShipLine=2,
        /// <summary>
        /// 用户
        /// </summary>
      User=3,
        /// <summary>
        /// 地点
        /// </summary>
      Location=4
    }
}

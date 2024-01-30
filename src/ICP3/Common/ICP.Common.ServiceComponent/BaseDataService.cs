using System;
using ICP.Common.ServiceInterface;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Helper;
using System.Data.Common;
namespace ICP.Common.ServiceComponent
{  
    /// <summary>
    /// 基础数据服务实现类
    /// </summary>
    public class BaseDataService : IBaseDataService
    {
        #region IBaseDataService 成员
        /// <summary>
        /// 获取基础数据
        /// 用于列表下拉数据源
        /// </summary>
        /// <param name="dataType">基础数据类型</param>
        /// <returns></returns>
        public DataTable GetBaseData(BaseDataType dataType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetBaseDataItems");
                db.AddInParameter(dbCommand, "@BaseDataType", DbType.Int32, dataType.GetHashCode());
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new DataTable("tblBaseData");
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

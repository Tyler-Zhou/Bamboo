using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ICP.Common.ServiceComponent
{
    /// <summary>
    /// 映射服务
    /// </summary>
    public partial class CommonService
    {
        /// <summary>
        /// 保存映射信息
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        public SingleResult SaveMappingInfo(SaveRequestMapping saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.ID, "ID");
            ArgumentHelper.AssertStringNotEmpty(saveRequest.MapType, "MapType");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveMappingInfo");
                //设置60秒超时
                dbCommand.CommandTimeout = 60;

                #region 构建参数
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@MapID", DbType.Int32, saveRequest.MapID);
                db.AddInParameter(dbCommand, "@MapType", DbType.String, saveRequest.MapType);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveBy);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                #endregion

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID"});
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

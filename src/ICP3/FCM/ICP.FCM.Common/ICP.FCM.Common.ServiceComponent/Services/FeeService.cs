using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.FCM.Common.ServiceComponent
{
    public partial class FCMCommonService
    {
        /// <summary>
        ///  获得海进分文件比较业务时海出账单详细信息
        /// </summary>
        /// <param name="OIBookingID"></param>
        /// <param name="OperationType"></param>
        /// <returns></returns>
        public List<Fee> GetCompareBillAndChargeInfo(Guid OIBookingID, OperationType OperationType)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = null;
                if (OperationType == OperationType.OceanExport)
                {
                    dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspGetCompareBillAndCharge]");
                }
                else
                {
                    dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspGetReviseCompareBillAndCharge]");
                }

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookingID);
                db.AddInParameter(dbCommand, "@operationType", DbType.Int16, OperationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return CompareCharage(ds);
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

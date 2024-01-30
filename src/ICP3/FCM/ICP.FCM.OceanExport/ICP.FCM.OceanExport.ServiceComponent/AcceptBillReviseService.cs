using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace ICP.FCM.OceanExport.ServiceComponent
{
    partial class OceanExportService
    {
        #region 签收海进修订
        /// <summary>
        ///  通过海出订单ID签收海进修订账单信息
        ///  2013-07-22 joe 
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <param name="remark">签收备注</param>
        /// <returns></returns>

         public    int AcceptBillRevise(Guid OEBookingID,string remark,Guid UserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspOIBillToUpateOEBill]");
                db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid,OEBookingID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, UserID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddOutParameter(dbCommand, "@Result", DbType.Int16, 2);

                db.ExecuteNonQuery(dbCommand);
               return  int.Parse(db.GetParameterValue(dbCommand,"@Result").ToString());
                
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
        #endregion
    }
}

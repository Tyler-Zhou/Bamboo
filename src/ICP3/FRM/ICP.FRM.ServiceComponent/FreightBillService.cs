using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FRM.ServiceComponent
{
    /// <summary>
    /// 运费管理
    /// </summary>
    public class FreightBillService : IFreightBillService
    {
        public SingleResult SaveAirOrderInfo(Guid OperationID, Guid SaveByID,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(OperationID, "OperationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBillInfoForOceanContractNO");
                db.AddInParameter(dbCommand, "@OperationID", DbType.DateTime, OperationID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.DateTime, @SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "State", "OperationID", "OperationNo" });
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

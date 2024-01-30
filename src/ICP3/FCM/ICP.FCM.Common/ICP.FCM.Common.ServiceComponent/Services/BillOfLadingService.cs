using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceComponent.JSONObjects;
using ICP.Framework.CommonLibrary.Platform;
using ICP.Common.ServiceInterface.CompositeObjects;

namespace ICP.FCM.Common.ServiceComponent
{
    public partial class FCMCommonService
    {
        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="businessContext"></param>
        /// <returns></returns>
        public List<BillOfLadingList> GetBillOfLadingList(BusinessOperationContext businessContext)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetBillOfLadingListForTaskCenter");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, businessContext.OperationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Int32, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<BillOfLadingList> results = BulidBillOfLadingListByDataSet(ds);

                return results;
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

        #region 将结果集合DataSet转换成列表对象
        /// <summary>
        ///将结果集合DataSet转换成列表对象
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        private List<BillOfLadingList> BulidBillOfLadingListByDataSet(DataSet ds)
        {
            List<BillOfLadingList> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new BillOfLadingList
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               No = b.Field<string>("BLNo"),
                                               ParentID = b.Field<Guid>("ParentID"),
                                               OperationID = b.Field<Guid>("OperationID"),
                                               OperationNo = b.Field<string>("OperationNo"),
                                               BLType = (BillOfLadingType)b.Field<byte>("BLType"),
                                               ShipperID = b.Field<Guid>("ShipperID"),
                                               ShipperName = b.Field<string>("ShipperName"),
                                               ConsigneeID = b.Field<Guid>("ShipperID"),
                                               ConsigneeName = b.Field<string>("ShipperName"),
                                               NotifyPartyID = b.Field<Guid>("ShipperID"),
                                               NotifyPartyName = b.Field<string>("ShipperName"),
                                               ReleaseState = (OceanReleaseState)b.Field<byte>("ReleaseState"),
                                               ReleaseType = (OceanReleaseType)b.Field<byte>("ReleaseType"),
                                               TelexNo = b.Field<string>("TelexNo"),
                                               BLCFM = b.IsNull("BLCFM") ? false : b.Field<bool>("BLCFM"),
                                               MBLD = b.IsNull("MBLD") ? false : b.Field<bool>("MBLD"),
                                               RBLA = b.IsNull("RBLA") ? false : b.Field<bool>("RBLA"),
                                               RBLH = b.IsNull("RBLH") ? false : b.Field<bool>("RBLH"),
                                               RBLD = b.IsNull("RBLD") ? false : b.Field<bool>("RBLD"),
                                               RBLRcv = b.IsNull("RBLRcv") ? false : b.Field<bool>("RBLRcv"),
                                               BLRC = b.IsNull("BLRC") ? false : b.Field<bool>("BLRC"),
                                               AMS = b.IsNull("AMS") ? false : b.Field<bool>("AMS"),
                                               ISF = b.IsNull("ISF") ? false : b.Field<bool>("ISF"),
                                               IsDirty = false
                                           }).ToList();
            return results;
        }
        #endregion

        /// <summary>
        /// 确认AMS费用
        /// </summary>
        /// <param name="saveRequest"></param>
        public bool SaveOceanBLAMSState(SaveRequestBLState saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[fcm].[uspSaveOceanBLAMS]");

                db.AddInParameter(dbCommand, "@HBLIDs", DbType.String, saveRequest.HBLIDs.Join());
                db.AddInParameter(dbCommand, "@AMSState", DbType.Int16, saveRequest.AMSState);
                db.AddInParameter(dbCommand, "@UpdateBy", DbType.Guid, saveRequest.SaveBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.ExecuteNonQuery(dbCommand);
                return true;
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

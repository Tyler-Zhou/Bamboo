namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ServiceInterface.DataObjects;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Helper;
    using System.Data;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    public partial class FCMCommonService
    {
        #region FreightRate Repuest

        /// <summary>
        /// 获取运价申请记录
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <returns>返回运价申请记录</returns>
        public List<FreightRateRequestInfo> GetFreightRateRequestList(Guid oceanBookingID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetFreightRateRequestList");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<FreightRateRequestInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                        select new FreightRateRequestInfo
                                                        {
                                                            SolverID = b.Field<Guid?>("SolverID"),
                                                            SenderID = b.Field<Guid>("SenderID"),
                                                            SenderRemark = b.Field<string>("SenderRemark"),
                                                            SolverRemark = b.Field<string>("SolverRemark"),
                                                            FreightRateID = b.Field<Guid?>("FreightRateID"),
                                                            FreightRateName = b.Field<string>("FreightRateName"),
                                                            ID = b.Field<Guid>("ID"),
                                                            OceanShippingOrderID = b.Field<Guid>("OceanShippingOrderID"),
                                                            SenderName = b.Field<string>("SenderName"),
                                                            SenderDate = b.Field<DateTime>("SenderDate"),
                                                            SolverName = b.Field<string>("SolverName"),
                                                            SolveDate = b.Field<DateTime?>("SolveDate"),
                                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                        }).ToList();

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

        /// <summary>
        /// 申请运价
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="oceanBookingID">订单ID</param>
        /// <param name="senderID">申请人</param>
        /// <param name="senderDate">申请时间</param>
        /// <param name="senderRemark">申请备注</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult RequestFreightRate(
            Guid? id,
            Guid oceanBookingID,
            Guid senderID,
            DateTime senderDate,
            string senderRemark,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(senderID, "senderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRequestOceanFreightRate");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@SenderID", DbType.Guid, senderID);
                db.AddInParameter(dbCommand, "@SenderDate", DbType.DateTime, senderDate);
                db.AddInParameter(dbCommand, "@SenderRemark", DbType.String, senderRemark);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
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

        /// <summary>
        /// 回复运价
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="solverID">回复人</param>
        /// <param name="solveDate">回复时间</param>
        /// <param name="solverRemark">回复备注</param>
        /// <param name="freightRateID">运价ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult AnswerOceanFreightRate(
            Guid id,
            Guid solverID,
            DateTime solveDate,
            string solverRemark,
            Guid? freightRateID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(solverID, "solverID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspAnswerOceanFreightRate");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@SolverID", DbType.Guid, solverID);
                db.AddInParameter(dbCommand, "@SolveDate", DbType.DateTime, solveDate);
                db.AddInParameter(dbCommand, "@SolverRemark", DbType.String, solverRemark);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@FreightRateID", DbType.Guid, freightRateID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
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

        /// <summary>
        /// 删除运价申请请求
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult RemoveFreightRateRequest(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveFreightRateRequest");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
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

        #endregion
    }
}

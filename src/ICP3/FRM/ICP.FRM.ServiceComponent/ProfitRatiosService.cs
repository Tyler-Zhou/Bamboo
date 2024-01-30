

namespace ICP.FRM.ServiceComponent
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using ICP.FRM.ServiceInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.FRM.ServiceInterface.DataObjects;
    using System.Collections.Generic;
    using System.Linq;

    public class ProfitRatiosService : IProfitRatiosService
    {
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }

        #region 业务统计列表(BusinessStatisticsList)
        /// <summary>
        /// 获取业务统计列表
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns>返回业务统计列表</returns>
        public List<BusinessStatisticsList> GetBusinessStatisticsList(QueryCriteria4ProfitRatios queryParamater)
        {
            try
            {
                ArgumentHelper.AssertGuidNotEmpty(queryParamater.CompanyIDs, "CompanyIDs");
                string tempCompanyIDs = queryParamater.CompanyIDs.Join();
                string tempCarrierIDs = queryParamater.CarrierIDs.Join();
                string tempVesselIDs = queryParamater.VesselIDs.Join();
                string tempShippingLineIDs = queryParamater.ShippingLineIDs.Join();
                string tempPOLIDs = queryParamater.POLIDs.Join();
                string tempPODIDs = queryParamater.PODIDs.Join();
                string tempPlaceOfDeliveryIDs = queryParamater.PlaceOfDeliveryIDs.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[frm].[uspGetBusinessStatisticsList]");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, tempCarrierIDs);
                db.AddInParameter(dbCommand, "@VesselIDs", DbType.String, tempVesselIDs);
                db.AddInParameter(dbCommand, "@ShippingLineIDs", DbType.String, tempShippingLineIDs);
                db.AddInParameter(dbCommand, "@POLIDs", DbType.String, tempPOLIDs);
                db.AddInParameter(dbCommand, "@PODIDs", DbType.String, tempPODIDs);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryIDs", DbType.String, tempPlaceOfDeliveryIDs);
                db.AddInParameter(dbCommand, "@ContractNo", DbType.String, queryParamater.ContractNo);
                db.AddInParameter(dbCommand, "@IsNonContractNo", DbType.Boolean, queryParamater.IsNonContractNo);
                db.AddInParameter(dbCommand, "@BookingNo", DbType.String, queryParamater.BookingNo);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, queryParamater.OperationNo);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, queryParamater.BeginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, queryParamater.EndTime);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<BusinessStatisticsList> results = BulidBusinessStatisticsListByDataSet(ds);
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
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static List<BusinessStatisticsList> BulidBusinessStatisticsListByDataSet(DataSet ds)
        {
            List<BusinessStatisticsList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new BusinessStatisticsList
                                                    {
                                                        CompanyName = b.Field<string>("CompanyName"),
                                                        ContractNo = b.Field<string>("ContractNo"),
                                                        CarrierName = b.Field<string>("CarrierName"),
                                                        VesselName = b.Field<string>("VesselName"),
                                                        VoyageName = b.Field<string>("VoyageName"),
                                                        ContainerDescription = b.Field<string>("ContainerDescription"),
                                                        OperationDate = b.Field<DateTime>("OperationDate"),
                                                        POLName = b.Field<string>("POLName"),
                                                        PODName = b.Field<string>("PODName"),
                                                        PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                                        CompanyID = b.Field<Guid>("CompanyID"),
                                                        ContractID = b.Field<Guid>("ContractID"),
                                                        ContractBaseItemID = b.Field<Guid>("ContractBaseItemID"),
                                                        VesselID = b.Field<Guid>("VesselID"),
                                                        OperationNo = b.Field<string>("OperationNo"),
                                                        BookingNo = b.Field<string>("BookingNo"),
                                                        OperationID = b.Field<Guid>("OperationID"),
                                                        AdjustmentAmount = b.Field<decimal>("AdjustmentAmount"),
                                                        IsDirty = false
                                                    }).ToList();
            return results;
        }
        #endregion

        #region 利润配比调整(ProfitRatiosAdjustment)
        /// <summary>
        /// 获取利润配比调整列表
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns>返回润配比调整列表</returns>
        public List<ProfitRatiosAdjustment> GetProfitRatiosAdjustmentList(QueryCriteria4Adjustment queryParamater)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[frm].[uspGetProfitRatiosAdjustment]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, queryParamater.OperationID);
                db.AddInParameter(dbCommand, "@ContractBaseItemID", DbType.Guid, queryParamater.ContractBaseItemID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }
                List<ProfitRatiosAdjustment> result = BulidProfitRatiosAdjustmentListByDataSet(ds);
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
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        public ManyResult SaveProfitRatiosAdjustment(ProfitRatiosAdjustmentSaveRequest saveRequest)
        {
            try
            {
                string tempOperationIDs = saveRequest.OperationIDs.Join();
                string tempContractBaseItemIDs = saveRequest.ContractBaseItemIDs.Join();
                string tempContainerTypeIDs = saveRequest.ContainerTypeIDs.Join();
                string tempAmounts = saveRequest.Amounts.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[frm].[uspSaveProfitRatiosAdjustment]");



                db.AddInParameter(dbCommand, "@AdjustmnetType", DbType.Int16, saveRequest.AdjustmnetType);
                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, tempOperationIDs);
                db.AddInParameter(dbCommand, "@ContractBaseItemIDs", DbType.String, tempContractBaseItemIDs);
                db.AddInParameter(dbCommand, "@ContainerTypeIDs", DbType.String, tempContainerTypeIDs);
                db.AddInParameter(dbCommand, "@Amounts", DbType.String, tempAmounts);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
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
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        public int ResetProfitRatiosAdjustment(ProfitRatiosAdjustmentSaveRequest saveRequest)
        {
            try
            {
                string tempOperationIDs = saveRequest.OperationIDs.Join();
                string tempContractBaseItemIDs = saveRequest.ContractBaseItemIDs.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[frm].[uspResetProfitRatiosAdjustment]");



                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, tempOperationIDs);
                db.AddInParameter(dbCommand, "@ContractBaseItemIDs", DbType.String, tempContractBaseItemIDs);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                int result=db.ExecuteNonQuery(dbCommand);
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
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static List<ProfitRatiosAdjustment> BulidProfitRatiosAdjustmentListByDataSet(DataSet ds)
        {
            List<ProfitRatiosAdjustment> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new ProfitRatiosAdjustment
                                                    {
                                                        ContractBaseItemID = b.Field<Guid>("ContractBaseItemID"),
                                                        ContainerTypeName = b.Field<string>("ContainerTypeName"),
                                                        OriginalAmount = b.Field<decimal>("OriginalAmount"),
                                                        Amount = b.Field<decimal>("Amount"),
                                                        AdjustmentAfterAmount = b.Field<decimal>("AdjustmentAfterAmount"),
                                                        ContainerTypeID = b.Field<Guid>("ContainerTypeID"),
                                                        IsDirty = false
                                                    }).ToList();
            return results;
        }
        #endregion
    }
}

#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/20 星期五 14:53:53
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

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

namespace ICP.FCM.Common.ServiceComponent
{
    public partial class FCMCommonService
    {
        #region 获取已关联业务
        /// <summary>
        /// 获取已关联业务
        /// </summary>
        /// <param name="qcParameter">查询参数</param>
        /// <returns></returns>
        public List<ECommerceList> GetAssociatedECommerceList(QueryCriteria4ECommerce qcParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAssociatedECommerceList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, qcParameter.OperationID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ECommerceList> results = BulidECommerceListByDataSet(ds);

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
        #endregion

        /// <summary>
        /// 获取自拼电商业务列表
        /// </summary>
        /// <param name="qcParameter">查询参数</param>
        /// <returns></returns>
        public List<ECommerceList> GetSelfSpellingECommerceList(QueryCriteria4ECommerce qcParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetSelfSpellingECommerceList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, qcParameter.CompanyID);
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, qcParameter.OperationID);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, qcParameter.OperationNo);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, qcParameter.BeginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, qcParameter.EndDate);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, qcParameter.MaxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ECommerceList> results = BulidECommerceListByDataSet(ds);

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

        #region 改变关联业务
        /// <summary>
        /// 改变关联业务
        /// </summary>
        /// <param name="saveRequest">保存关联的对象</param>
        /// <returns></returns>
        public SingleResult ChangeAssociationBusiness(ECommerceSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveByID, "更新人");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeECommerceAssociationState");
                string tempisAssociateds = saveRequest.IsAssociateds.Join();
                string tempassociationids = saveRequest.AssociationIDs.Join();

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@IsAssociateds", DbType.String, tempisAssociateds);
                db.AddInParameter(dbCommand, "@AssociationIDs", DbType.String, tempassociationids);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "NO", "UpdateDate" });
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

        #region 将结果集合DataSet转换成列表对象
        /// <summary>
        ///将结果集合DataSet转换成列表对象
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        private List<ECommerceList> BulidECommerceListByDataSet(DataSet ds)
        {
            List<ECommerceList> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new ECommerceList
                                                  {
                                                      IsSelect = b.Field<bool>("IsSelect"),
                                                      ID = b.Field<Guid>("ID"),
                                                      SalesName = b.Field<string>("SalesName"),
                                                      No = b.Field<string>("No"),
                                                      OperationDate = b.Field<DateTime>("OperationDate"),
                                                      POLName = b.Field<string>("POLName"),
                                                      PODName = b.Field<string>("PODName"),
                                                      WarehouseNo = b.Field<string>("WarehouseNo"),
                                                      TransferNo = b.Field<string>("TransferNo"),
                                                      Quantity = b.Field<int>("Quantity"),
                                                      Weight = b.Field<decimal>("Weight"),
                                                      Measurement = b.Field<decimal>("Measurement"),
                                                      ProfitAmount = b.Field<decimal>("ProfitAmount"),
                                                      IsDirty = false
                                                  }).ToList();
            return results;
        }
        #endregion
    }
}

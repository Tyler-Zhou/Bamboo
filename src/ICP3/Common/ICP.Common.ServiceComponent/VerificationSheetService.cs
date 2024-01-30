using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.Common.ServiceComponent
{
    public class VerificationSheetService : IVerificationSheetService
    {
        private ISessionService _sessionService;


        public VerificationSheetService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }


        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }

        #region 通过业务Id获取核销单列表
        /// <summary>
        /// 通过业务Id获取核销单列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<VerificationSheet> GetVerificationSheetListByIds(Guid operationId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetVerificationSheetListByOperationID");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetSheetList(ds);
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

        #region 通过查询面板输入条件获取核销单列表
        /// <summary>
        /// 通过查询面板输入条件获取核销单列表
        /// </summary>     
        public List<VerificationSheet> GetVerificationSheetList(
                         string operationNO,     //业务号
                         string sheetNo,         //核销单号
                         string customer,        //经营单位
                         string expressNo,       //快递单号
                         bool? isFreightArrive,   //运费是否到帐
                         int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetVerificationSheetList");

                db.AddInParameter(dbCommand, "@OperationNO", DbType.String, operationNO);
                db.AddInParameter(dbCommand, "@SheetNo", DbType.String, sheetNo);
                db.AddInParameter(dbCommand, "@Customer", DbType.String, customer);
                db.AddInParameter(dbCommand, "@ExpressNo", DbType.String, expressNo);
                db.AddInParameter(dbCommand, "@IsFreightArrive", DbType.Boolean, isFreightArrive);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetSheetList(ds);
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

        #region 转换数据
        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private List<VerificationSheet> GetSheetList(DataSet ds)
        {
            try
            {
                List<VerificationSheet> list = (from b in ds.Tables[0].AsEnumerable()
                                                select new VerificationSheet
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    SheetNo = b.Field<String>("NO"),
                                                    OperationId = b.Field<Guid>("OperationID"),
                                                    OperationNo = b.Field<String>("OperationNO"),
                                                    CustomerId = b.Field<Guid?>("CustomerID"),
                                                    CustomerName = b.Field<String>("CustomerName"),
                                                    ReceiptDate = b.Field<DateTime?>("ReceiveDate"),
                                                    ReturnDate = b.Field<DateTime?>("ReturnDate"),
                                                    ExpressNO = b.Field<String>("ExpressNo"),
                                                    IsFreightArrive = b.Field<bool>("IsFreightArrive"),
                                                    Remark = b.Field<String>("Remark"),
                                                    CreateByID = b.Field<Guid>("CreateBy"),
                                                    //CreateByName = b.Field<String>("CreateByName"),
                                                    CreateDate = b.Field<DateTime>("CreateDate"),
                                                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                }).ToList<VerificationSheet>();

                return list;
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

        #region 保存核销单
        /// <summary>
        /// 保存核销单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sheetNo">核销单号</param>
        /// <param name="operationId">业务Id</param>
        /// <param name="customerId">经营单位</param>
        /// <param name="receiptDate"></param>
        /// <param name="returnDate"></param>
        /// <param name="expressNo">快递单号</param>
        /// <param name="isFreightArrive"></param>
        /// <param name="remark"></param>
        /// <param name="saveByID"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        public SingleResult SaveVerificationSheet(
            Guid id,
            string sheetNo,
            Guid operationId,
            Guid? customerId,
            DateTime? receiptDate,
            DateTime? returnDate,
            string expressNo,
            bool isFreightArrive,
            string remark,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationId");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveVerificationSheet");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);    //核销单Id
                db.AddInParameter(dbCommand, "@NO", DbType.String, sheetNo);    //核销单号
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);  //业务ID
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@ReceiptDate", DbType.DateTime, receiptDate);
                db.AddInParameter(dbCommand, "@ReturnDate", DbType.DateTime, returnDate);
                db.AddInParameter(dbCommand, "@ExpressNO", DbType.String, expressNo);
                db.AddInParameter(dbCommand, "@IsFreightArrive", DbType.Boolean, isFreightArrive);
                db.AddInParameter(dbCommand, "@Remark ", DbType.String, remark);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        #region 删除核销单

        /// <summary>
        /// 删除核销单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveVerificationSheet(
                     Guid id,
                     Guid removeByID,
                     DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveVerificationSheet");

                db.AddInParameter(dbCommand, "@SheetId", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(dbCommand);
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

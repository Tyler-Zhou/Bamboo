using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.FAM.ServiceComponent.Common;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System.Transactions;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using System.Threading;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.ServiceComponent
{
    public partial class FinanceService
    {
        #region 查询水单

        /// <summary>
        /// 查询水单
        /// </summary>
        /// <param name="searchParameter">查询条件</param>
        /// <returns></returns>
        public List<BankReceiptListInfo> GetBankReceiptList(BankReceiptSearchParameter searchParameter)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankReceiptList");

                db.AddInParameter(dbCommand, "@ReceiptNO", DbType.String, searchParameter.ReceiptNO);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, searchParameter.CompanyIDs.Join());
                db.AddInParameter(dbCommand, "@StatusList", DbType.Byte, searchParameter.Status);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, searchParameter.IsValid);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, searchParameter.FromDate);
                db.AddInParameter(dbCommand, "@Todate", DbType.DateTime, searchParameter.ToDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetList1(ds);

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

        List<BankReceiptListInfo> GetList1(DataSet ds)
        {
            return IListDataSet.DataSetToIList<BankReceiptListInfo>(ds, 0).ToList();
        }
        #endregion

        #region 根据ID获取水单信息

        public BankReceiptInfo GetBankReceiptInfo(Guid id,
                          bool isEnglish)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankReceiptInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                BankReceiptInfo result = (from b in ds.Tables[0].AsEnumerable()
                                          select new BankReceiptInfo
                                      {
                                          CompanyID = b.Field<Guid>("CompanyID"),
                                          //CreateId= b.Field<Guid>("CreateBy"),
                                          ID = b.Field<Guid>("ID"),
                                          No = b.Field<String>("No"),
                                          CompanyName = b.Field<String>("CompanyName"),
                                          CustomerID = b.Field<Guid?>("CustomerID"),
                                          CustomerName=b.Field<String>("CustomerName"),
                                          Amount = b.Field<Decimal>("Amount"),
                                          Status = (BankReceiptStatus)b.Field<Byte>("Status"),
                                          CreateBy = b.Field<Guid>("CreateBy"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),//此处需使用DataType?类型,否则为空时会报错
                                          IsValid = b.Field<Boolean>("IsValid"),
                                          Remark = b.Field<String>("Remark")
                                      }).SingleOrDefault();
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

        #region 保存水单信息
        /// <summary>
        /// 保存水单信息
        /// </summary>
        /// <param name="hdObj">主表对象</param>
        /// <param name="dtlList">明细列表</param>
        public SingleResult SaveBankReceiptInfo(BankReceiptSaveRequest saveRequest)
        {

            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBankReceiptInfo");

                    db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.Id);
                    db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyId);
                    db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, saveRequest.CustomerId);
                    db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, saveRequest.Amount);
                    db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                    db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                    SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "No", "UpdateDate" });

                    scope.Complete();

                    return result;

                }
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

        #region 根据ID作废水单信息
        /// <summary>
        /// 根据ID作废水单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCancel">是否作废(True为作废,False为激活)</param>
        /// <param name="cancelByID">操作人ID</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <returns></returns>
        public SingleResult CancelBankReceiptList(
                         Guid id,
                         bool isCancel,
                         Guid cancelByID,
                         DateTime? updateDate,
                         bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCancelBankReceipt");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@isCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@cancelByID", DbType.Guid, cancelByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #region 审核/取消审核
        public ManyResult AuditorBankReceipt(Guid[] ids, DateTime?[] updateDates, Guid auditorById, bool isCheck)
        {
            try
            {
                string idList = ids.Join();
                string updateList = updateDates.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAuditorBankReceipt");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, idList);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateList);
                db.AddInParameter(dbCommand, "@AuditorById", DbType.Guid, auditorById);
                db.AddInParameter(dbCommand, "@IsCheck", DbType.Boolean, isCheck);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "ApprovalDate", "UpdateDate","Status" });
                return result;
            }
            catch(SqlException sqlException)
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

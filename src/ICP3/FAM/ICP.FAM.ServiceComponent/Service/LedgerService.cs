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
using ICP.Common.ServiceInterface.CompositeObjects;

namespace ICP.FAM.ServiceComponent
{
    public partial class FinanceService
    {

        #region 查询凭证

        /// <summary>
        /// 查询凭证
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public List<LedgerListInfo> GetLedgerList(Guid[] companyIDs, 
                                                    int? minVoucherSeqNo, 
                                                    int? maxVoucherSeqNo,
                                                    string refNo,
                                                    LedgerSearchAmountType amountType,
                                                    decimal? minAmount,
                                                    decimal? maxAmount,
                                                    string remark, 
                                                    Guid? createBy,
                                                    Guid? auditorID, 
                                                    Guid? cashierID, 
                                                    LedgerMasterType[] typeList, 
                                                    LedgerMasterStatus status, 
                                                    bool? isValid, 
                                                    DateTime? fromDate, 
                                                    DateTime? todate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLedgerList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@MinVoucherSeqNo", DbType.Int32, minVoucherSeqNo);
                db.AddInParameter(dbCommand, "@MaxVoucherSeqNo", DbType.Int32, maxVoucherSeqNo);
                db.AddInParameter(dbCommand, "@RefNo", DbType.String, refNo);
                db.AddInParameter(dbCommand, "@AmountType", DbType.Byte, amountType);
                db.AddInParameter(dbCommand, "@MinAmount", DbType.String, minAmount.ToString());
                db.AddInParameter(dbCommand, "@MaxAmount", DbType.String, maxAmount.ToString());
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, createBy);
                db.AddInParameter(dbCommand, "@AuditorID", DbType.Guid, auditorID);
                db.AddInParameter(dbCommand, "@CashierID", DbType.Guid, cashierID);
                db.AddInParameter(dbCommand, "@MasterTypes", DbType.String, typeList.Join());
                db.AddInParameter(dbCommand, "@MasterStatus", DbType.Byte, status);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@Todate", DbType.DateTime, todate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
               
                return GetList(ds);

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
        /// 根据ID查询凭证
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<LedgerListInfo> GetLedgerListByID(Guid[] id)
        { 
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLedgerListByID");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, id.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
               
                return GetList(ds);

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

        List<LedgerListInfo> GetList(DataSet ds)
        {
            return IListDataSet.DataSetToIList<LedgerListInfo>(ds, 0).ToList();
        }

        List<GLBlance> GetGLBlanceList(DataSet ds)
        {
            return IListDataSet.DataSetToIList<GLBlance>(ds, 0).ToList();
        }


        #endregion

        #region 获得凭证详细信息
        /// <summary>
        /// 获得凭证详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LedgerMasters GetLedgerMastersInfo(Guid id, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLedgerMastersInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                LedgerMasters master = GetLedgerMasterInfo(ds);
              
                return master;
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
        public LedgerMasters GetLedgerMasterInfo(DataSet ds)
        {
            LedgerMasters master = (from d in ds.Tables[0].AsEnumerable()
                                    select new LedgerMasters
                                    {
                                        ID = d.Field<Guid>("ID"),
                                        No = d.Field<String>("No"),
                                        Status = (LedgerMasterStatus)d.Field<Byte>("Status"),
                                        ReceiptQty = d.Field<Int32?>("ReceiptQty"),
                                        Type = (LedgerMasterType)d.Field<Byte>("Type"),
                                        RefId = d.Field<Guid?>("RefID"),
                                        RefNo = d.Field<String>("RefNo"),
                                        OperationNo = d.Field<String>("OperationNo"),
                                        CompanyID = d.Field<Guid>("CompanyID"),
                                        CompanyName = d.Field<String>("CompanyName"),
                                        AccountingID = d.Field<Guid?>("AccountingID"),
                                        Accountant = d.Field<String>("Accountant"),
                                        AuditID = d.Field<Guid?>("AuditID"),
                                        Auditor = d.Field<String>("Auditor"),
                                        CashierID = d.Field<Guid?>("CashierID"),
                                        Cashier = d.Field<String>("Cashier"),
                                        CreateBy = d.Field<Guid>("CreateBy"),
                                        Creator = d.Field<String>("Creator"),
                                        UpdateDate = d.Field<DateTime?>("UpdateDate"),
                                        DATE = d.Field<DateTime?>("Date"),
                                        IsValid = d.Field<Boolean>("IsValid"),
                                        MaxNo = d.Field<String>("MaxNo"),
                                        DetailList = (from b in ds.Tables[1].AsEnumerable()
                                                      select new Ledgers
                                                      {
                                                          Id = b.Field<Guid>("ID"),
                                                          CustomerId = b.Field<Guid?>("CustomerId"),
                                                          Customer = b.Field<String>("CustomerName"),
                                                          GLId = b.Field<Guid>("GLID"),
                                                          GLName = b.Field<String>("GLName"),
                                                          GLFullName = b.Field<String>("GLFullName"),
                                                          GLCodeProperty = (GLCodeProperty)b.Field<Byte>("GLCodeProperty"),
                                                          Date = b.Field<DateTime>("Date"),
                                                          CRAmt = b.Field<Decimal>("CRAmt"),
                                                          DRAmt = b.Field<Decimal>("DRAmt"),
                                                          OrgAmt = b.Field<Decimal>("OrgAmt"),
                                                          Rate = b.Field<Decimal>("Rate"),
                                                          Remark = b.Field<String>("Remark"),
                                                          Type = (LedgerDetailType)b.Field<Byte>("Type"),
                                                          RefId = b.Field<Guid>("RefId"),
                                                          RefNo = b.Field<String>("RefNo"),
                                                          CompanyId = b.Field<Guid>("CompanyID"),
                                                          OperationNo = b.Field<String>("OperationNo"),
                                                          CreateBy = b.Field<Guid>("CreateBy"),
                                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                                          UpdateBy = b.Field<Guid?>("UpdateBy"),
                                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                          MasterID = b.Field<Guid>("MasterID"),
                                                          DepID = b.Field<Guid?>("DepID"),
                                                          Dept = b.Field<String>("DeptName"),
                                                          UserID = b.Field<Guid?>("UserID"),
                                                          UserName = b.Field<String>("UserName"),
                                                          IsCustomerCheck = b.Field<Boolean>("IsCustomerCheck"),
                                                          IsDepartmentCheck = b.Field<Boolean>("IsDepartmentCheck"),
                                                          IsPersonalCheck = b.Field<Boolean>("IsPersonalCheck"),
                                                          ForeignCurrencyID = b.Field<Guid?>("ForeignCurrencyID"),
                                                          IsCarryOver = b.Field<Boolean>("IsCarryOver"),
                                                      }).ToList()
                                    }).SingleOrDefault();

            int count = (from d in master.DetailList where d.IsCarryOver select d).Count();
            if (count > 0)
            {
                master.IsCarryOver = true;
            }
            else
            {
                master.IsCarryOver = false;
            }

            return master;
        }
        #endregion

        #region 查询凭证
        public LedgerMasters GetLedgerMastersByNo(Guid companyID, string no,DateTime date, VoucherSearchType type)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLedgerMastersInfoByNo");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@Date", DbType.DateTime, date);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, type);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                LedgerMasters master = GetLedgerMasterInfo(ds);

                return master;
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

        #region 作废凭证列表
        /// <summary>
        /// 作废凭证列表
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="IsValid">是否有效</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public void CancelLedgerList(
                         Guid id,
                         bool IsValid,
                         Guid changeByID,
                         DateTime? updateDate,
                         bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCancelLedgerList");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, IsValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #region 出纳签字/取消

        /// <summary>
        /// 出纳签字/取消
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">状态</param>
        /// <param name="cashierID">cashierID出纳员ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ManyResult CashierCheckedLedgerList(Guid[] ids, LedgerMasterStatus status, Guid cashierID, DateTime?[] updateDates, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCashierCheckedLedgerList");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@Status", DbType.Int32, status);
                db.AddInParameter(dbCommand, "@CashierID", DbType.Guid, cashierID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #endregion

        #region 财务主管签字/取消

        /// <summary>
        /// 财务主管签字/取消
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">状态</param>
        /// <param name="financeManagerID">财务主管ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ManyResult FinanceManagerCheckedLedgerList(Guid[] ids, LedgerMasterStatus status, Guid financeManagerID, DateTime?[] updateDates, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspFinanceManagerCheckedLedgerList");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@Status", DbType.Int32, status);
                db.AddInParameter(dbCommand, "@FinanceManagerID", DbType.Guid, financeManagerID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #endregion

        #region 审核/取消审核凭证

        /// <summary>
        /// 审核/取消审核凭证
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">状态</param>
        /// <param name="auditID">修改人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ManyResult AduitLedgerList(Guid[] ids, LedgerMasterStatus status, Guid auditID, DateTime?[] updateDates, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAuditLedgerList");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@Status", DbType.Int32, status);
                db.AddInParameter(dbCommand, "@AuditID", DbType.Guid, auditID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #endregion

        #region 凭证记账/取消凭证记账

        /// <summary>
        /// 凭证记账/取消凭证记账
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">状态</param>
        /// <param name="accountingID">记账人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ManyResult KeepAccountsLedgerList(Guid[] ids, LedgerMasterStatus status, Guid accountingID, DateTime?[] updateDates, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspKeepAccountsLedgerList");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@Status", DbType.Int32, status);
                db.AddInParameter(dbCommand, "@AccountingID", DbType.Guid, accountingID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #endregion

        #region 保存凭证信息
        /// <summary>
        /// 保存凭证列表信息
        /// </summary>
        /// <param name="hdObj">主表对象</param>
        /// <param name="dtlList">明细列表</param>
        public HierarchyManyResult SaveLedgerInfo(LedgerSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveLedgerInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@ReceiptQty", DbType.Int32, saveRequest.ReceiptQty);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, saveRequest.Type);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@AccountingID", DbType.Guid, saveRequest.AccountingID);
                db.AddInParameter(dbCommand, "@CashierID", DbType.Guid, saveRequest.CashierID);
                db.AddInParameter(dbCommand, "@DATE", DbType.Date, saveRequest.DATE);
                db.AddInParameter(dbCommand, "@IsCarryOver", DbType.Boolean, saveRequest.IsCarryOver);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.Date, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@DetailIDS", DbType.String, saveRequest.DetailIDS.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, saveRequest.CustomerIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, saveRequest.GLIDs.Join());
                db.AddInParameter(dbCommand, "@Dates", DbType.String, saveRequest.Dates.Join());
                db.AddInParameter(dbCommand, "@DetailTypes", DbType.String, saveRequest.DetailTypes.Join());
                db.AddInParameter(dbCommand, "@RefIDs", DbType.String, saveRequest.RefIDs.Join());
                db.AddInParameter(dbCommand, "@RefNos", DbType.String, saveRequest.RefNos.Join());
                db.AddInParameter(dbCommand, "@OperationNos", DbType.String, saveRequest.OperationNos.Join());              
                db.AddInParameter(dbCommand, "@CRAmts", DbType.String, saveRequest.CRAmts.Join());
                db.AddInParameter(dbCommand, "@DRAmts", DbType.String, saveRequest.DRAmts.Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, saveRequest.Remarks.Join());
                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, saveRequest.CompanyIds.Join());
                db.AddInParameter(dbCommand, "@OrgAmts", DbType.String, saveRequest.OrgAmts.Join());
                db.AddInParameter(dbCommand, "@Rates", DbType.String, saveRequest.Rates.Join());
                db.AddInParameter(dbCommand, "@DepIDs", DbType.String, saveRequest.DepIDs.Join());
                db.AddInParameter(dbCommand, "@UserIDs", DbType.String, saveRequest.UserIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, saveRequest.UpdateDates == null ? string.Empty : saveRequest.UpdateDates.Join());
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, saveRequest.SaveBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "No","UpdateDate" }, 
                    new string[] { "ID", "UpdateDate" } });


                if (results == null
                || results.Length < 2
                || results[0].Items.Count == 0)
                {
                    return null;
                }
                HierarchyManyResult result = new HierarchyManyResult(results[0].Items[0].Copy());
                if (results[1] != null)
                {
                    foreach (SingleResult s in results[1].Items)
                    {
                        result.Childs.Add(new HierarchyManyResult(s));
                    }
                }

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

        #region 获得凭证打印数据
        /// <summary>
        /// 凭证列表上打印凭证
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        public PrintLedgerMasterReports GetPrintLedgerReportDate(Guid id, bool isEnglish)
        { 
             try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLedgerReport");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetReportData(ds);
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
        /// 批量打印凭证
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<PrintLedgerMasterReports> GetBulkPrintLedgerReportDate(Guid[] ids, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLedgerBulkReport");
                
                db.AddInParameter(dbCommand, "@IDS", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                //List<PrintLedgerMasterReports> list = new List<PrintLedgerMasterReports>();
                List<PrintLedgerMasterReports> hdList = IListDataSet.DataSetToIList<PrintLedgerMasterReports>(ds, 0).ToList();
                //List<Ledgers> dtlList = IListDataSet.DataSetToIList<Ledgers>(ds, 1).ToList();
                //foreach (PrintLedgerMasterReports hdObj in hdList)
                //{
                //    hdObj.DetailList = dtlList.Where(o => o.MasterID == hdObj.ID).ToList();
                //    //大写金额
                //    decimal amount = 0;
                //    foreach (Ledgers dtl in hdObj.DetailList)
                //    {
                //        amount += dtl.DRAmt;
                //    }
                //    hdObj.FiguresAmount = Utility.MoneyToString(amount);
                //    list.Add(hdObj);
                //}
                return hdList;
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
        /// 根据参考ID打印凭证
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        public PrintLedgerMasterReports GetPrintLedgerReportDateByRefID(Guid refID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLedgerReportByRefID");

                db.AddInParameter(dbCommand, "@RefID", DbType.Guid, refID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetReportData(ds);

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
        /// 获得凭证打印报表数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private PrintLedgerMasterReports GetReportData(DataSet ds)
        {
            //PrintLedgerMasterReports reportData = (from d in ds.Tables[0].AsEnumerable()
            //                                       select new PrintLedgerMasterReports
            //                                       {
            //                                           ID = d.Field<Guid>("ID"),
            //                                           No = d.Field<String>("No"),
            //                                           ReceiptQty = d.Field<Int32>("ReceiptQty"),
            //                                           Type = (LedgerMasterType)d.Field<Byte>("Type"),
            //                                           CompanyID = d.Field<Guid>("CompanyID"),
            //                                           Accountant = d.Field<String>("Accountant"),
            //                                           Auditor = d.Field<String>("Auditor"),
            //                                           Cashier = d.Field<String>("Cashier"),
            //                                           CreateBy = d.Field<Guid>("CreateBy"),
            //                                           DATE = d.Field<DateTime?>("Date"),
            //                                           FinanceManager = d.Field<String>("FinanceManager"),
            //                                           Transactor = d.Field<String>("Transactor"),
            //                                           Organization = d.Field<String>("CustomerName"),

            //                                           DetailList = (from b in ds.Tables[1].AsEnumerable()
            //                                                         select new Ledgers
            //                                                         {
            //                                                             Id = b.Field<Guid>("ID"),
            //                                                             CustomerId = b.Field<Guid?>("CustomerId"),
            //                                                             Customer = b.Field<String>("CustomerName"),
            //                                                             GLId = b.Field<Guid>("GLID"),
            //                                                             GLName = b.Field<String>("GLName"),
            //                                                             GLFullName = b.Field<String>("GLFullName"),
            //                                                             Date = b.Field<DateTime>("Date"),
            //                                                             CRAmt = b.Field<Decimal>("CRAmt"),
            //                                                             DRAmt = b.Field<Decimal>("DRAmt"),
            //                                                             OrgAmt = b.Field<Decimal>("OrgAmt"),
            //                                                             Rate = b.Field<Decimal>("Rate"),
            //                                                             Remark = b.Field<String>("Remark"),
            //                                                             Type = (LedgerDetailType)b.Field<Byte>("Type"),
            //                                                             RefId = b.Field<Guid>("RefId"),
            //                                                             RefNo = b.Field<String>("RefNo"),
            //                                                             CompanyId = b.Field<Guid>("CompanyID"),
            //                                                             OperationNo = b.Field<String>("OperationNo"),
            //                                                             CreateBy = b.Field<Guid>("CreateBy"),
            //                                                             CreateDate = b.Field<DateTime>("CreateDate"),
            //                                                             UpdateBy = b.Field<Guid?>("UpdateBy"),
            //                                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
            //                                                             MasterID = b.Field<Guid>("MasterID"),
            //                                                             DepID = b.Field<Guid?>("DepID"),
            //                                                             Dept = b.Field<String>("DeptName"),
            //                                                             UserID = b.Field<Guid?>("UserID"),
            //                                                             UserName = b.Field<String>("UserName"),
            //                                                         }).ToList()
            //                                       }).SingleOrDefault();


            PrintLedgerMasterReports reportData = IListDataSet.DataSetToIList<PrintLedgerMasterReports>(ds, 0).SingleOrDefault();
            if (reportData == null)
            {
                return null;
            }
            reportData.DetailList = IListDataSet.DataSetToIList<Ledgers>(ds, 1).ToList();
            return reportData;
        }

        /// <summary>
        /// 删除凭证明细
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        /// <param name="removeById"></param>
        /// <param name="isEnglish"></param>
        public void RemoveLedgerInfo(Guid[] ids, DateTime?[] updateDates, Guid removeById, bool isEnglish)
        {
            if (ids == null || ids.Length == 0)
            {
                return;
            }
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeById, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveLedgerInfo");


                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #region 保存汇率
        /// <summary>
        /// 保存汇率 
        /// 2014/12/04 周任平 取消在更改汇率时产生汇兑损益的凭证，改为由会计关帐时去生成
        /// </summary>
        /// <param name="request"></param>
        public ManyResultData SaveRateList(AdjustRateSaveRequest request)
        {
            ManyResultData result = new ManyResultData();

            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                #region 拼装数据
                //汇率列表
                List<SolutionExchangeRateList> DataList = _configureService.GetSolutionExchangeRateList(request.SolutionID,true);

                //构造一个新的实体
                AdjustRateSaveRequest lastRequest=new AdjustRateSaveRequest();
                lastRequest.Ids = new List<Guid?>();
                lastRequest.SourceCurrencyIDs = new List<Guid>();
                lastRequest.TargetCurrencyIDs = new List<Guid>();
                lastRequest.Versions = new List<DateTime?>();
                lastRequest.FromDates = new List<DateTime>();
                lastRequest.ToDates = new List<DateTime>();
                lastRequest.Rates = new List<decimal>();
                lastRequest.SolutionID = request.SolutionID;
                lastRequest.SaveByID = request.SaveByID;

                //实体的属性
                Guid? Id;
                Guid SourceCurrencyID, TargetCurrencyID;
                DateTime? UpdateDate;
                DateTime FromDate, ToDate, EndDate = new DateTime(2000, 1, 1);
                decimal Rate;

                for(int i=0;i<request.Ids.Count;i++)
                {
                    Id = request.Ids[i];
                    SourceCurrencyID = request.SourceCurrencyIDs[i];
                    TargetCurrencyID = request.TargetCurrencyIDs[i];
                    UpdateDate = request.Versions[i];
                    FromDate = request.FromDates[i];
                    ToDate = request.ToDates[i];
                    Rate = request.Rates[i];

                    if (EndDate < FromDate)
                    {
                        EndDate = FromDate;
                    }

                    //找到这个币种的最后一条数据，将结束日期更新为本条数据开始日期的前一天
                    List<SolutionExchangeRateList> searchList = (from d in DataList where 
                                                                      d.SourceCurrencyID == SourceCurrencyID
                                                                   && d.TargetCurrencyID == TargetCurrencyID
                                                                 orderby d.FromDate descending
                                                                 select d).ToList();

                    if (searchList != null && searchList.Count > 0)
                    {
                        lastRequest.Ids.Add(searchList[0].ID);
                        lastRequest.SourceCurrencyIDs.Add(searchList[0].SourceCurrencyID);
                        lastRequest.TargetCurrencyIDs.Add(searchList[0].TargetCurrencyID);
                        lastRequest.Versions.Add(searchList[0].UpdateDate);
                        lastRequest.FromDates.Add(searchList[0].FromDate);
                        lastRequest.ToDates.Add(FromDate.AddDays(-1));//上次的结束日期为这次的开始日期减1天
                        lastRequest.Rates.Add(searchList[0].Rate);
                    }
                }
                #endregion

                #region 更新之前数据的结束日期
                try
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionExchangeRateInfo");

                    db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, lastRequest.SolutionID);
                    db.AddInParameter(dbCommand, "@Type", DbType.Byte, 1);
                    db.AddInParameter(dbCommand, "@Ids", DbType.String, lastRequest.Ids.ToArray().Join());
                    db.AddInParameter(dbCommand, "@SourceCurrencyIDs", DbType.String, lastRequest.SourceCurrencyIDs.ToArray().Join());
                    db.AddInParameter(dbCommand, "@TargetCurrencyIDs", DbType.String, lastRequest.TargetCurrencyIDs.ToArray().Join());
                    db.AddInParameter(dbCommand, "@FromDates", DbType.String, lastRequest.FromDates.ToArray().Join());
                    db.AddInParameter(dbCommand, "@ToDates", DbType.String, lastRequest.ToDates.ToArray().Join());
                    db.AddInParameter(dbCommand, "@Rates", DbType.String, lastRequest.Rates.ToArray().Join());
                    db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, lastRequest.Versions.ToArray().Join());
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, lastRequest.SaveByID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                    result = db.ManyResult(dbCommand);
                  

                }
                catch (SqlException sqlException)
                {
                    throw new ApplicationException(sqlException.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region 保存本次更改的汇率数据
                try
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionExchangeRateInfo");

                    db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, request.SolutionID);
                    db.AddInParameter(dbCommand, "@Type", DbType.Byte, 1);
                    db.AddInParameter(dbCommand, "@Ids", DbType.String, request.Ids.ToArray().Join());
                    db.AddInParameter(dbCommand, "@SourceCurrencyIDs", DbType.String, request.SourceCurrencyIDs.ToArray().Join());
                    db.AddInParameter(dbCommand, "@TargetCurrencyIDs", DbType.String, request.TargetCurrencyIDs.ToArray().Join());
                    db.AddInParameter(dbCommand, "@FromDates", DbType.String, request.FromDates.ToArray().Join());
                    db.AddInParameter(dbCommand, "@ToDates", DbType.String, request.ToDates.ToArray().Join());
                    db.AddInParameter(dbCommand, "@Rates", DbType.String, request.Rates.ToArray().Join());
                    db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, request.Versions.ToArray().Join());
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, request.SaveByID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                    result = db.ManyResult(dbCommand);
                }
                catch (SqlException sqlException)
                {
                    throw new ApplicationException(sqlException.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                //提交事务
                scope.Complete();
            }

           return result;
        }
        #endregion

        #region 获得用友与ICP数据的关联
        /// <summary>
        /// 获得用友与ICP关联的数据
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public List<UFCode2ICP> GetUFCode2ICPList(Guid[] CompanyIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetUFCode2ICPList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, CompanyIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }


                List<UFCode2ICP> list = (from d in ds.Tables[0].AsEnumerable()
                                         select new UFCode2ICP{
                                            CompanyID=d.Field<Guid>("CompanyID"),
                                            DataType = d.Field<Byte>("Type"),
                                            UFCode = d.Field<String>("UFCode"),
                                            ICPName=d.Field<String>("ICPName")
                                         }).ToList();



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

        #region 保存期初余额
        public ManyResult SaveBeginBalance(BeginBalanceSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBeginBalance");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, saveRequest.DetailIDS.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, saveRequest.CustomerIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, saveRequest.GLIDs.Join());
                db.AddInParameter(dbCommand, "@CRAmts", DbType.String, saveRequest.CRAmts.Join());
                db.AddInParameter(dbCommand, "@DRAmts", DbType.String, saveRequest.DRAmts.Join());
                db.AddInParameter(dbCommand, "@OrgAmts", DbType.String, saveRequest.OrgAmts.Join());
                db.AddInParameter(dbCommand, "@Rates", DbType.String, saveRequest.Rates.Join());
                db.AddInParameter(dbCommand, "@DeptIDs", DbType.String, saveRequest.DepIDs.Join());
                db.AddInParameter(dbCommand, "@PersonalIDs", DbType.String, saveRequest.UserIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, saveRequest.UpdateDates.Join());
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32, saveRequest.Year);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, saveRequest.SaveBy);               
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                ManyResult results = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" } );

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
        /// 删除期初余额
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        public void RemoveBeginBalance(Guid[] ids, DateTime?[] updateDates,Guid removeByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveBeginBalance");


                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 获得期初余额列表
        /// <summary>
        ///获得指定公司的期初余额
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public List<BeginBalances> GetBeginBalance(Guid companyID,int year)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBeginBalanceList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32, year);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BeginBalances> DetailList = (from b in ds.Tables[0].AsEnumerable()
                                            select new BeginBalances
                                            {
                                                Id = b.Field<Guid>("ID"),
                                                GLId = b.Field<Guid>("GLID"),
                                                GLCode = b.Field<String>("GLCode"),
                                                GLName = b.Field<String>("GLName"),
                                                CRAmt = b.Field<Decimal>("CRAmt"),
                                                DRAmt = b.Field<Decimal>("DRAmt"),
                                                OrgAmt = b.Field<Decimal>("OrgAmt"),
                                                Rate = b.Field<Decimal>("Rate"),
                                                CompanyId=b.Field<Guid>("CompanyID"),
                                                Year=b.Field<Int32>("Year"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                GLCodeProperty = (GLCodeProperty)b.Field<Byte>("GLCodeProperty"),
                                                Balance = b.Field<Decimal>("Balance"),
                                                IsCustomerCheck = b.Field<bool>("IsCustomerCheck"),
                                                IsDepartmentCheck = b.Field<bool>("IsDepartmentCheck"),
                                                IsPersonalCheck = b.Field<bool>("IsPersonalCheck"),
                                                GLCodeType = (GLCodeType)b.Field<byte>("GLCodeType"),
                                                RowCount = b.Field<Int32>("RowCount"),
                                                IsDirty=false
                                            }).ToList();



                return DetailList;
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
        /// 获得指定科目的期初余额详细信息
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="glID"></param>
        /// <returns></returns>
        public List<BeginBalances> GetGLBeginBalance(Guid companyID, int year,Guid glID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetGLBeginBalanceList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32,year);
                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, glID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BeginBalances> DetailList = (from b in ds.Tables[0].AsEnumerable()
                                                  select new BeginBalances
                                            {
                                                Id = b.Field<Guid>("ID"),
                                                GLId = b.Field<Guid>("GLID"),
                                                GLCode = b.Field<String>("GLCode"),
                                                GLName = b.Field<String>("GLName"),
                                                CRAmt = b.Field<Decimal>("CRAmt"),
                                                DRAmt = b.Field<Decimal>("DRAmt"),
                                                OrgAmt = b.Field<Decimal>("OrgAmt"),
                                                Rate = b.Field<Decimal>("Rate"),
                                                CompanyId=b.Field<Guid>("CompanyID"),
                                                Year=b.Field<Int32>("Year"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                GLCodeProperty = (GLCodeProperty)b.Field<Byte>("GLCodeProperty"),
                                                Balance = b.Field<Decimal>("Balance"),
                                                CustomerId = b.Field<Guid?>("CustomerId"),
                                                CustomerCode=b.Field<String>("CustomerCode"),
                                                CustomerName=b.Field<String>("CustomerName"),
                                                DeptID = b.Field<Guid?>("DeptID"),
                                                DeptName=b.Field<String>("DeptName"),
                                                PersonalID = b.Field<Guid?>("PersonalID"),
                                                PersonalName=b.Field<String>("PersonalName"),
                                                IsDirty = false
                                            }).ToList();



                return DetailList;
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

        #region 生成记账凭证
        /// <summary>
        /// 生成记账凭证
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="generateBy"></param>
        public void GenerateBillVoucher(Guid companyID, DateTime startDate, DateTime endDate, Guid generateBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGenerateBillVoucher");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@GenerateById", DbType.Guid, generateBy);                
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        //#region 先产生汇兑损益凭证，再进行会计关帐
        ///// <summary>
        ///// 先产生汇兑损益凭证，再进行会计关帐
        ///// </summary>
        ///// <param name="companyID"></param>
        ///// <param name="accountingDate"></param>
        ///// <param name="saveByID"></param>
        //public void SaveAccountingData(Guid companyID, DateTime accountingDate, Guid saveByID)
        //{
        //    try
        //    {
        //        TransactionOptions option = new TransactionOptions();
        //        option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
        //        {

        //            #region 产生汇兑损益
        //            Database db = DatabaseFactory.CreateDatabase();
        //            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAdjustRateForVoucher");

        //            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
        //            db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, accountingDate);
        //            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
        //            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

        //            db.ExecuteNonQuery(dbCommand);

        //            #endregion

        //            #region 保存关帐信息
        //            ConfigureInfo configureInfo = _configureService.GetCompanyConfigureInfo(companyID, IsEnglish);
        //            //同时更新一下计费关帐时间，避免有些人因为一次性设置会计关帐日而造成计费关帐日不同
        //            configureInfo.ChargingClosingdate = accountingDate;
        //            configureInfo.AccountingClosingdate = accountingDate;

        //            _configureService.SaveConfigureInfo(configureInfo.ID,
        //                                                   configureInfo.CompanyID,
        //                                                   configureInfo.CustomerID,
        //                                                   configureInfo.StandardCurrencyID,
        //                                                   configureInfo.DefaultCurrencyID,
        //                                                   configureInfo.SolutionID,
        //                                                   configureInfo.IssuePlaceID,
        //                                                   configureInfo.ChargingClosingdate,
        //                                                   configureInfo.AccountingClosingdate,
        //                                                   configureInfo.ShortCode,
        //                                                   configureInfo.DefaultAgentDescription,
        //                                                   configureInfo.BLTitleID,
        //                                                   configureInfo.IsVATinvoice,
        //                                                   configureInfo.VATFeeID,
        //                                                   configureInfo.VATrate,
        //                                                   saveByID,
        //                                                   configureInfo.UpdateDate);

        //            #endregion

        //            //提交事务
        //            scope.Complete();
        //        }    
             
        //    }
        //    catch (SqlException sqlException)
        //    {
        //        throw new ApplicationException(sqlException.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //#endregion

        #region 先产生汇兑损益凭证，再进行会计关帐
        /// <summary>
        /// 先产生汇兑损益凭证，再进行会计关帐
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="accountingDate"></param>
        /// <param name="saveByID"></param>
        public void SaveAccountingData(Guid companyID, DateTime accountingDate, Guid saveByID)
        {
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {

                    #region 产生汇兑损益
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAdjustRateForVoucher");

                    db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                    db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, accountingDate);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                    db.ExecuteNonQuery(dbCommand);

                    #endregion

                    #region 保存关帐信息
                    ConfigureInfo configureInfo = _configureService.GetCompanyConfigureInfo(companyID, IsEnglish);
                    //同时更新一下计费关帐时间，避免有些人因为一次性设置会计关帐日而造成计费关帐日不同
                    configureInfo.ChargingClosingdate = accountingDate;
                    configureInfo.AccountingClosingdate = accountingDate;

                    ConfigureSaveRequest saveRequest = new ConfigureSaveRequest()
                    {
                        ID = configureInfo.ID,
                        CompanyID = configureInfo.CompanyID,
                        CustomerID = configureInfo.CustomerID,
                        StandardCurrencyID = configureInfo.StandardCurrencyID,
                        DefaultCurrencyID = configureInfo.DefaultCurrencyID,
                        SolutionID = configureInfo.SolutionID,
                        IssuePlaceID = configureInfo.IssuePlaceID,
                        ChargingClosingDate = configureInfo.ChargingClosingdate,
                        AccountingClosingDate = configureInfo.AccountingClosingdate,
                        ShortCode = configureInfo.ShortCode,
                        DefaultAgentDescription = configureInfo.DefaultAgentDescription,
                        BLTitleID = configureInfo.BLTitleID,
                        IsVATinvoice = configureInfo.IsVATinvoice,
                        VATFEEID = configureInfo.VATFeeID,
                        VATrateAP = configureInfo.VATrate,
                        CMBNetComUserID = configureInfo.CMBNetComUserID,
                        SaveByID = saveByID,
                        UpdateDate = configureInfo.UpdateDate,
                    };

                    _configureService.SaveConfigureInfo(saveRequest);

                    #endregion

                    //提交事务
                    scope.Complete();
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

        #region 产生汇兑损益
        /// <summary>
        /// 产生汇兑损益
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="accountingDate"></param>
        /// <param name="saveByID"></param>
        public void AdjustRateForVoucher(Guid companyID, DateTime accountingDate, Guid saveByID)
        {
            try
            {
                    #region 产生汇兑损益
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAdjustRateForVoucher");

                    db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                    db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, accountingDate);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                    db.ExecuteNonQuery(dbCommand);

                    #endregion

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

        #region 关帐时，判断亏损的业务是否已申请过流程
        public void SendDeficitOperationEMail(Guid companyID, DateTime formDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDeficitOperationList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@FormDate", DbType.DateTime, formDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return ;
                }
                //文件邮箱列表
                List<UserEMailList> mailList = (from d in ds.Tables[0].AsEnumerable()
                                                select new UserEMailList 
                                                {
                                                    UserID=d.Field<Guid>("UserID"),
                                                    CName=d.Field<String>("CName"),
                                                    EMail = d.Field<String>("Email")
                                                }).ToList();
                //亏损业务列表
                List<DeficitOperationInfo> opList = (from d in ds.Tables[1].AsEnumerable()
                                                     select new DeficitOperationInfo
                                                     {
                                                         FilerID = d.Field<Guid>("FilerID"),
                                                         OperationNo = d.Field<String>("OperationNo")
                                                     }).ToList();

                foreach (UserEMailList item in mailList)
                {
                    List<DeficitOperationInfo> list = (from d in opList where d.FilerID == item.UserID select d).ToList();

                    DeficitEmailNotifyInfo info = new DeficitEmailNotifyInfo();
                    info.ToEMail = item.EMail;
                    info.OpNos = GetOperationNo(list);
                    info.Subject = formDate.Year.ToString() + "年" + formDate.Month.ToString()+"月末申请亏损的业务";

                    SendDeficitEmail(info);
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
        private string GetOperationNo(List<DeficitOperationInfo> list)
        {
            string operationNo = string.Empty;
            foreach (DeficitOperationInfo item in list)
            {
                operationNo = operationNo+System.Environment.NewLine + item.OperationNo;
            }
            return operationNo;
        }
        private void SendDeficitEmail(DeficitEmailNotifyInfo info)
        {
            WaitCallback fire = (notifyInfo) =>
            {

                DeficitEmailNotifyInfo tempInfo = notifyInfo as DeficitEmailNotifyInfo;
                if (tempInfo == null)
                {
                    return;
                }
                try
                {
                    //以管理员的身价，向各位文件发送亏损单但末申请流程的单号
                    ICP.Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
                    message.CreateBy = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
                    message.SendTo = tempInfo.ToEMail;
                    message.SendFrom = "icpsystem@cityocean.com";
                    message.Subject = tempInfo.Subject;
                    message.Body = tempInfo.OpNos;
                    message.Type = MessageType.Email;

                    _emailService.Send(message);
                }
                catch (Exception ex)
                {
                    ICP.Framework.CommonLibrary.LogHelper.SaveLog(ex.Message + ex.StackTrace);
                }
            };

            ThreadPool.QueueUserWorkItem(fire, info);
        }
        #endregion

        #region 解锁凭证
        public void UntieLockLedger(Guid checkId,Guid chargeByID)
        {
            try
            {
                Guid[] checkIds = { checkId };
                //插入解锁数据
                _systemService.SaveUntieLockInfo(UntieLockType.Ledger, checkIds, chargeByID);               
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

        #region 整理凭证
        public string ArrangeLedger(Guid Comid,string date)
        {
            try
            {
                string Massage = string.Empty;
                object result;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("uspArrangeLedgerNo");

                db.AddInParameter(dbCommand, "@SMonth", DbType.String, date);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.Guid, Comid);
                result =  db.ExecuteScalar(dbCommand);
                while (!string.IsNullOrEmpty(result.ToString()))
                {
                    dbCommand = db.GetStoredProcCommandWithTimeout("uspArrangeLedgerNo");

                    db.AddInParameter(dbCommand, "@SMonth", DbType.String, date);
                    db.AddInParameter(dbCommand, "@CompanyIDs", DbType.Guid, Comid);
                    result = db.ExecuteScalar(dbCommand);
                    Massage += result.ToString();
                }
                return Massage;
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

        #region 获得科目余额
        public List<GLBlance> GetBalance(Guid Comid, Guid GLID,Guid? Cusid,Guid? Depid,Guid? Userid,DateTime enddate)
        {
            try
            {
                DataSet myds = new DataSet();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBalance");

                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, GLID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, Comid);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, Cusid);
                db.AddInParameter(dbCommand, "@DepID", DbType.Guid, Depid);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, Userid);
                db.AddInParameter(dbCommand, "@Date", DbType.DateTime, enddate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                myds = db.ExecuteDataSet(dbCommand);

                return GetGLBlanceList(myds);
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

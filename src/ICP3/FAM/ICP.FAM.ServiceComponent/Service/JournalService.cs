using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Helper;
using System.Data;
using System.Data.SqlClient;

namespace ICP.FAM.ServiceComponent
{
    /// <summary>
    /// 日记帐
    /// </summary>
    public partial class FinanceService
    {
        #region 获得日记帐列表
        /// <summary>
        /// 获得日记帐列表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="minAmount">最小金额</param>
        /// <param name="maxAmount">最大金额</param>
        /// <param name="isValid">有效性</param>
        /// <param name="maxRecords">dataPageInfo</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        public PageList GetJournalList(
                          string NO,
                          Guid[] companyIDs,
                          DateTime? startDate,
                          DateTime? endDate,
                          Decimal? minAmount,
                          Decimal? maxAmount,
                          bool? isValid,
                          DataPageInfo dataPageInfo,
                          bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetJournalList");
                db.AddInParameter(dbCommand, "@NO", DbType.String, NO);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@MinAmount", DbType.String, minAmount);
                db.AddInParameter(dbCommand, "@MaxAmount", DbType.String, maxAmount);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@currentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<JournalList> results = (from b in ds.Tables[0].AsEnumerable()
                                             select new JournalList
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 No = b.Field<String>("No"),
                                                 CompanyName = b.Field<String>("CompanyName"),
                                                 DRAmount = b.Field<String>("DRAmount"),
                                                 CRAmount = b.Field<String>("CRAmount"),
                                                 PostDate = b.Field<DateTime>("PostDate"),
                                                 CreateName = b.Field<String>("CreateName"),
                                                 CreateDate = b.Field<DateTime>("CreateDate"),
                                                 UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                 IsValid = b.Field<Boolean>("IsValid"),
                                                 Remark = b.Field<String>("Remark"),
                                             }).ToList();
                dataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());

                PageList list = PageList.Create<JournalList>(results, dataPageInfo);

                return list;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 作废日记帐
        /// <summary>
        /// 作废日记帐
        /// </summary>
        /// <param name="id">日记帐ID</param>
        /// <param name="isCancel">是否作废(True为作废,False为激活)</param>
        /// <param name="cancelByID">操作人ID</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <returns></returns>
        public SingleResult CancelJournal(
                         Guid id,
                         bool isCancel,
                         Guid cancelByID,
                         DateTime? updateDate,
                         bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCancelJournal");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@CancelByID", DbType.Guid, cancelByID);
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

        #region 获得日记帐详细信息
        /// <summary>
        /// 获得日记帐详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JournalInfo GetJournalInfo(Guid id,
                          bool isEnglish)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetJournalInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                JournalInfo result = (from b in ds.Tables[0].AsEnumerable()
                                      select new JournalInfo
                                      {
                                          CompanyID = b.Field<Guid>("CompanyID"),
                                          //CreateId= b.Field<Guid>("CreateBy"),
                                          ID = b.Field<Guid>("ID"),
                                          No = b.Field<String>("No"),
                                          CompanyName = b.Field<String>("CompanyName"),
                                          DRAmount = b.Field<String>("DRAmount"),
                                          CRAmount = b.Field<String>("CRAmount"),
                                          PostDate = b.Field<DateTime>("PostDate"),
                                          CreateName = b.Field<String>("CreateName"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),//此处需使用DataType?类型,否则为空时会报错
                                          IsValid = b.Field<Boolean>("IsValid"),
                                          Remark = b.Field<String>("Remark"),
                                      }).SingleOrDefault();

                result.DetailList = (from b in ds.Tables[1].AsEnumerable()
                                     select new JournalDetail
                                     {
                                         ID = b.Field<Guid>("ID"),
                                         JournalID = b.Field<Guid>("JournalID"),
                                         CustomerID = b.Field<Guid?>("CustomerID"),
                                         CustomerName = b.Field<string>("CustomerName"),
                                         GLID = b.Field<Guid>("GLID"),
                                         CurrencyID = b.Field<Guid>("CurrencyID"),
                                         DRAmount = b.Field<Decimal>("DRAmount"),
                                         CRAmount = b.Field<Decimal>("CRAmount"),
                                         Remark = b.Field<String>("Remark"),
                                         UpdateDates = b.Field<DateTime?>("UpdateDate"),
                                     }).ToList();


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

        #region 保存日记帐信息

        /// <summary>
        /// 保存日记帐信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">单号</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="postDate">日期</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <returns></returns>
        public SingleResult SaveJournal(JournalSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveJournal");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@PostDate", DbType.DateTime, saveRequest.PostDate);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "No", "UpdateDate" });

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

        #region 获得日记帐明细列表

        /// <summary>
        /// 获得日记帐明细列表
        /// </summary>
        /// <param name="journalID"></param>
        /// <returns></returns>
        public List<JournalDetail> GetJournalDetailList(Guid journalID,
                          bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetJournalDetailList");

                db.AddInParameter(dbCommand, "@JournalID", DbType.Guid, journalID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<JournalDetail> list = (from b in ds.Tables[0].AsEnumerable()
                                            select new JournalDetail
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                JournalID = b.Field<Guid>("JournalID"),
                                                GLID = b.Field<Guid>("GLID"),
                                                CurrencyID = b.Field<Guid>("CurrencyID"),
                                                DRAmount = b.Field<Decimal>("DRAmount"),
                                                CRAmount = b.Field<Decimal>("CRAmount"),
                                                Remark = b.Field<String>("Remark"),
                                                CustomerID = b.Field<Guid?>("CustomerID"),
                                                CustomerName = b.Field<string>("CustomerName")
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

        #region 删除日记帐明细

        /// <summary>
        /// 删除日记帐明细
        /// </summary>
        /// <param name="id">明细ID</param>
        /// <param name="removeByID">删除人ID</param>
        public void RemoveJournalDetailList(Guid? id, Guid removeByID,
                          bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveJournalDetailList");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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

        #region 保存日记帐明细
        /// <summary>
        /// 保存日记帐明细
        /// </summary>
        /// <param name="journalID">日记帐ID</param>
        /// <param name="ids">明细ID集合</param>
        /// <param name="glIDs">会计科目ID集合</param>
        /// <param name="currencyIDs">币种ID集合</param>
        /// <param name="DRAmount">应收金额</param>
        /// <param name="CRAmount">应付金额</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public ManyResult SaveJournalDetail(JournalDetailSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveJournalDetail");

                string idsList = saveRequest.IDs.Join();
                string glIdList = saveRequest.GLIDs.Join();
                string CustomerIdList = saveRequest.Customers.Join();
                string currencyIdList = saveRequest.CurrencyIDs.Join();
                string drAmountList = saveRequest.DRAmounts.Join();
                string crAmountList = saveRequest.CRAmounts.Join();
                string remarkList = saveRequest.Remarks.Join();
                string updateDateList = saveRequest.UpdateDates.Join();

                db.AddInParameter(dbCommand, "@JournalID", DbType.Guid, saveRequest.JournalID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, idsList);
                db.AddInParameter(dbCommand, "@GlIDs", DbType.String, glIdList);
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, CustomerIdList);
                db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, currencyIdList);
                db.AddInParameter(dbCommand, "@DRAmount", DbType.String, drAmountList);
                db.AddInParameter(dbCommand, "@CRAmount", DbType.String, crAmountList);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remarkList);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, updateDateList);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);


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

        #region 以事务的方式保存
        /// <summary>
        /// 以事务的方式保存
        /// </summary>
        /// <param name="journalSaveRequest"></param>
        /// <param name="detailSaveRequestList"></param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveJournalWithTrans(
                   JournalSaveRequest journalSaveRequest,
                   List<JournalDetailSaveRequest> detailSaveRequestList)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();
                Guid journalID = Guid.Empty;
                ///保存日记帐信息
                if (journalSaveRequest != null)
                {
                    result.Add(journalSaveRequest.RequestId,
                        new SaveResponse { RequestId = journalSaveRequest.RequestId, SingleResult = this.SaveJournal(journalSaveRequest) });

                    journalID = result[journalSaveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                }
                else
                {
                    journalID = journalSaveRequest.ID;
                }
                //保存明细
                if (detailSaveRequestList.Count > 0 && detailSaveRequestList != null)
                {
                    foreach (JournalDetailSaveRequest item in detailSaveRequestList)
                    {
                        if (item.JournalID == Guid.Empty)
                        {
                            item.JournalID = journalID;
                            //item.IDs = new Guid?[item.IDs.Length];
                        }
                        result.Add(item.RequestId,
                     new SaveResponse { RequestId = item.RequestId, ManyResult = this.SaveJournalDetail(item) });
                    }
                }


                scope.Complete();

                return result;

            }

        }
        #endregion

        #region 保存管理费用月预算
        public ManyResult SaveFeeMonthBudgets(Guid[] ids,
                                Guid[] glIds,
                                decimal[] amounts,
                                string[] remarks,
                                DateTime?[] updateDates,
                                Guid companyID,
                                int year,
                                FeeMonthBudgetType type,
                                Guid saveBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveFeeMonthBudge");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32, year);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, type);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@GlIDs", DbType.String, glIds.Join());
                db.AddInParameter(dbCommand, "@Amounts", DbType.String, amounts.Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarks.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveBy);
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
        #endregion

        #region 获得管理费用月预算列表
        /// <summary>
        /// 获得管理费用月预算列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<FeeYearMonthBudgetList> GetFeeMonthBudgetList(
                                         Guid companyID,
                                         int year,
                                         FeeMonthBudgetType type)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetFeeMonthBudgetList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32, year);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, type);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<FeeYearMonthBudgetList> list = (from b in ds.Tables[0].AsEnumerable()
                                                      select new FeeYearMonthBudgetList
                                                      {
                                                            ID = b.Field<Guid>("ID"),                                                     
                                                            GLID = b.Field<Guid>("GLID"),
                                                            GLCode=b.Field<String>("GLCode"),
                                                            ParentID = b.Field<Guid>("ParentID"),
                                                            GLName=b.Field<String>("GLName"),
                                                            Remark = b.Field<String>("Remark"),
                                                            Amount=b.Field<Decimal>("Amount"),
                                                            CreateName=b.Field<String>("CreateBy"),
                                                            CreateDate=b.Field<DateTime>("CreateDate"),
                                                            ChildCount = b.Field<int>("ChildCount"),
                                                            UpdateDate=b.Field<DateTime?>("UpdateDate"),
                                                            IsDirty=false,
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

        #region 解锁凭证
        public void UntieLockLedgerForJournal(Guid checkId, Guid chargeByID)
        {
            try
            {
                Guid[] checkIds = { checkId };
                //插入解锁数据
                _systemService.SaveUntieLockInfo(ICP.Sys.ServiceInterface.DataObjects.UntieLockType.Journal, checkIds, chargeByID);
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

using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.FAM.ServiceComponent
{
     public partial class FinanceService
    {
       
       #region 获得对账单列表
        /// <summary>
        /// 查询对账单列表
        /// </summary>
        /// <param name="no">对账单号</param>
        /// <param name="companyID">发起公司ID</param>
        /// <param name="checkType">对账类型(1:内部代理对账)</param>
        /// <param name="isCompleted">是否已完成对账</param>
        /// <param name="createID">创建人ID</param>
        /// <param name="beginDate">创建开始时间</param>
        /// <param name="endDate">创建结束时间</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public PageList GetAgnetBillCheckList(
                                 string no,
                                 Guid[] companyIDs,
                                 AgentBillCheckType checkType,
                                 bool isCompleted,
                                 Guid? createID,
                                 DateTime? beginDate,
                                 DateTime? endDate,
                                 DataPageInfo dataPageInfo,
                                 bool isEnglish)
         {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAgnetBillCheckList");

                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@CheckType", DbType.Int32, checkType);
                db.AddInParameter(dbCommand, "@IsCompleted", DbType.Boolean, isCompleted);
                db.AddInParameter(dbCommand, "@CreateID", DbType.Guid, createID);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AgnetBillCheckList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new AgnetBillCheckList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        No = b.Field<String>("No"),
                                                        LaunchCompanyID = new Guid(b.Field<String>("LaunchCompanyID")),
                                                        LaunchCompanyName = b.Field<String>("LaunchCompanyName"),
                                                        LaunchUserID = b.Field<Guid>("LaunchUserID"),
                                                        LaunchUserName = b.Field<String>("LaunchUserName"),
                                                        CheckCompanyID = new Guid(b.Field<String>("CheckCompanyID")),
                                                        CheckCompanyName = b.Field<String>("CheckCompanyName"),
                                                        CheckUserID = b.Field<Guid>("CheckUserID"),
                                                        CheckUserName = b.Field<String>("CheckUserName"),
                                                        OperValues = b.Field<String>("OperValues"),
                                                        OperTexts=b.Field<String>("OperTexts"),
                                                        EndingETD = b.Field<DateTime>("EndingETD"),
                                                        Status = (AgentBillCheckStatusEnum)b.Field<Byte>("Status"),
                                                        CreateID = b.Field<Guid>("CreateID"),
                                                        CreateName = b.Field<String>("CreateName"),
                                                        CreateDate = b.Field<DateTime>("CreateDate"),
                                                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                    }).ToList();

                dataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());

                PageList list =PageList.Create<AgnetBillCheckList>(results,dataPageInfo);

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

       #region 获得代理对账单详细信息
       /// <summary>
       /// 获得代理对账单详细信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public AgnetBillCheckList GetAgnetBillCheckInfo(Guid id, bool isEnglish)
       {
           try
           {
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAgnetBillCheckInfo");

               db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
               db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

               DataSet ds = null;
               ds = db.ExecuteDataSet(dbCommand);
               if (ds == null || ds.Tables.Count < 1)
               {
                   return null;
               }

               AgnetBillCheckList result = (from b in ds.Tables[0].AsEnumerable()
                                            select new AgnetBillCheckList
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                No = b.Field<String>("No"),
                                                LaunchCompanyID = new Guid(b.Field<String>("LaunchCompanyID")),
                                                LaunchCompanyName = b.Field<String>("LaunchCompanyName"),
                                                LaunchUserID = b.Field<Guid>("LaunchUserID"),
                                                LaunchUserName = b.Field<String>("LaunchUserName"),
                                                CheckCompanyID = new Guid(b.Field<String>("CheckCompanyID")),
                                                CheckCompanyName = b.Field<String>("CheckCompanyName"),
                                                CheckUserID = b.Field<Guid>("CheckUserID"),
                                                CheckUserName = b.Field<String>("CheckUserName"),
                                                OperValues = b.Field<String>("OperValues"),
                                                OperTexts = b.Field<String>("OperTexts"),
                                                EndingETD = b.Field<DateTime>("EndingETD"),
                                                Status = (AgentBillCheckStatusEnum)b.Field<Byte>("Status"),
                                                CreateID = b.Field<Guid>("CreateID"),
                                                CreateName = b.Field<String>("CreateName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
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

       #region 删除代理对账单
       /// <summary>
         /// 删除代理对账音
         /// </summary>
         /// <param name="id">代理对账单ID</param>
         /// <param name="removeByID">删除人ID</param>
         /// <param name="UpdateDate">最后更新时间</param>
         /// <param name="isEnglish">是否英文版本</param>
       public void RemoveAgnetBillCheck(
                       Guid id,
                       Guid removeByID,
                       DateTime? updateDate,
                       bool isEnglish)
       {
           try
           {
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveAgnetBillCheck");

               db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
               db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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

       #region 更新代理对账单状态
       /// <summary>
       /// 更新代理对账单状态
       /// </summary>
       /// <param name="id">ID</param>
       /// <param name="statue">状态</param>
       /// <param name="memoContent">备注内容</param>
       /// <param name="changeID">更新人</param>
       /// <param name="updateDate">更新时间--控件数据版本使用</param>
       /// <returns></returns>
       public SingleResult ChangeAgentBillCheckStatus(
                                Guid id,
                                AgentBillCheckStatusEnum statue,
                                string memoContent,
                                Guid changeID,
                                DateTime? updateDate,
                                bool isEnglish)
       {
           try
           {
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspChangeAgentBillCheckStatus");

               db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
               db.AddInParameter(dbCommand, "@Statue", DbType.Int16, statue);
               db.AddInParameter(dbCommand, "@MemoContent", DbType.String, memoContent);
               db.AddInParameter(dbCommand, "@ChangeID", DbType.Guid, changeID);
               db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
               db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

               SingleResult result = db.SingleResult(dbCommand,new string[]{"ID","UpdateDate"});
              
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

       #region 保存代理对账单的信息
       /// <summary>
       /// 保存代理对账单信息
       /// </summary>
       /// <param name="id"></param>
       /// <param name="no"></param>
       /// <param name="launchCompanyID"></param>
       /// <param name="launchUserID"></param>
       /// <param name="checkCmpanyID"></param>
       /// <param name="checkUserID"></param>
       /// <param name="operTypes"></param>
       /// <param name="endingETD"></param>
       /// <param name="createID"></param>
       /// <param name="createDate"></param>
       /// <returns></returns>
       public SingleResult SaveAgentBillCheck(
                              Guid id,
                              Guid launchCompanyID,
                              Guid launchUserID,
                              Guid checkCmpanyID,
                              Guid checkUserID,
                              string operTypes,
                              DateTime endingETD,
                              Guid createID,
                              DateTime? updateDate,
                              bool isEnglish)
       {
           try
           {
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveAgentBillCheck");

               db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
               db.AddInParameter(dbCommand, "@LaunchCompanyIDs", DbType.String, launchCompanyID.ToString());
               db.AddInParameter(dbCommand, "@LaunchUserID", DbType.Guid, launchUserID);
               db.AddInParameter(dbCommand, "@CheckCmpanyIDs", DbType.String, checkCmpanyID.ToString());
               db.AddInParameter(dbCommand, "@CheckUserID", DbType.Guid, checkUserID);
               db.AddInParameter(dbCommand, "@OperationTypes", DbType.String, operTypes);
               db.AddInParameter(dbCommand, "@EndingETD", DbType.DateTime, endingETD);
               db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, createID);
               db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
               db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

               SingleResult result = db.SingleResult(dbCommand, new string[]{"ID","No","UpdateDate"});
               
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

       #region  获得代理对账单明细列表
      /// <summary>
      /// 获得代理对账单的明细
      /// </summary>
      /// <param name="agenBillCheckID"></param>
      /// <returns></returns>
       public List<AgentBillCheckDetail> GetAgentBillCheckDetailList(Guid agenBillCheckID, bool isEnglish)
       {
           try
           {
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAgentBillCheckDetailList");

               db.AddInParameter(dbCommand, "@AgenBillCheckID", DbType.Guid, agenBillCheckID);
               db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

               DataSet ds = null;
               ds = db.ExecuteDataSet(dbCommand);
               if (ds == null || ds.Tables.Count < 1)
               {
                   return null;
               }

               List<AgentBillCheckDetail> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new AgentBillCheckDetail
                                                     {
                                                         ETD = b.Field<DateTime>("ETD"),
                                                         BLNO = b.Field<String>("BLNO"),
                                                         CurrencyID = b.Field<Guid>("CurrencyID"),
                                                         CurrencyName = b.Field<String>("CurrencyName"),
                                                         LaunchBillNOs = b.Field<String>("LaunchBillNOs"),
                                                         LaunchBillNOsCount = b.Field<Int32>("LaunchBillNOsCount"),
                                                         LaunchDebit = b.Field<Decimal>("LaunchDebit"),
                                                         LaunchCredit = b.Field<Decimal>("LaunchCredit"),
                                                         LaunchBalance = b.Field<Decimal>("LaunchBalance"),
                                                         Gap =System.Math.Abs(b.Field<Decimal>("Gap")),
                                                         CheckBillNOs = b.Field<String>("CheckBillNOs"),
                                                         CheckBillNOsCount = b.Field<Int32>("CheckBillNOsCount"),
                                                         CheckDebit = b.Field<Decimal>("CheckDebit"),
                                                         CheckCredit = b.Field<Decimal>("CheckCredit"),
                                                         CheckBalance = b.Field<Decimal>("CheckBalance"),
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
       #endregion

        #region 获得指定对账单的账单费用列表
       /// <summary>
       /// 获得指定对账单的账单费用列表
       /// </summary>
       /// <param name="agenBillCheckID">对账单ID</param>
       /// <param name="type">类型(1为发起代理，2为核对代理)</param>
       /// <param name="isEnglish"></param>
       /// <returns></returns>
       public List<WriteOffBill> GetBillFeeByAgentBillCheck(Guid agenBillCheckID, int type, bool isEnglish)
       {
           try
           {
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillFeeByAgentBillCheck");

               db.AddInParameter(dbCommand, "@AgenBillCheckID", DbType.Guid, agenBillCheckID);
               db.AddInParameter(dbCommand, "@Type", DbType.Int32, type);
               db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

               DataSet ds = null;
               ds = db.ExecuteDataSet(dbCommand);
               if (ds == null || ds.Tables.Count < 1)
               {
                   return null;
               }

               List<WriteOffBill> results = (from b in ds.Tables[0].AsEnumerable()
                                             select new WriteOffBill
                                             {
                                                 Way=(FeeWay)b.Field<Byte>("Way"),
                                                 ChargeID = b.Field<Guid>("ChargeID"),
                                                 ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                 ChargeName = b.Field<String>("ChargeName"),
                                                 BillID = b.Field<Guid>("BillID"),
                                                 BillNo = b.Field<String>("BillNo"),
                                                 CurrencyID = b.Field<Guid>("CurrencyID"),
                                                 CurrencyName = b.Field<String>("CurrencyName"),
                                                 Amount = ((FeeWay)b.Field<Byte>("Way") == FeeWay.AR) ? (b.Field<decimal>("Amount")): - b.Field<decimal>("Amount"),
                                                 ChargeAmount = ((FeeWay)b.Field<Byte>("Way") == FeeWay.AR) ?(b.Field<decimal>("Amount")) : -b.Field<decimal>("Amount"),
                                                 AvailbeWriteOffAmount = ((FeeWay)b.Field<Byte>("Way") == FeeWay.AR) ? (b.Field<decimal>("AvailbeWriteOffAmount")) : -b.Field<decimal>("AvailbeWriteOffAmount"),
                                                 WriteOffAmount = ((FeeWay)b.Field<Byte>("Way") == FeeWay.AR) ? (b.Field<decimal>("AvailbeWriteOffAmount")):-b.Field<decimal>("AvailbeWriteOffAmount"),
                                                 ExchangeRate = b.Field<decimal>("WriteOffRate"),
                                                 FinalAmount = ((FeeWay)b.Field<Byte>("Way") == FeeWay.AR) ? (b.Field<decimal>("AvailbeWriteOffAmount")) : -b.Field<decimal>("AvailbeWriteOffAmount"),
                                                 ChargeUpdateDate = b.Field<DateTime?>("ChargeUpdateDate")
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
        #endregion
    }
}

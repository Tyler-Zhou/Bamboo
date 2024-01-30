namespace ICP.FAM.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Common;
    using System.Data;
    using ICP.FAM.ServiceInterface;
    using ICP.FAM.ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.FAM.ServiceInterface.Common;


    public partial class FinanceReportService : IFinanceReportService
    {

        #region 科目余额表
        /// <summary>
        /// 科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="fromGLCode">开始科目</param>
        /// <param name="toGLCode">结束科目</param>
        /// <param name="glCodeType">科目类型(1资产;2负债;3权益;4成本;5损益)</param>
        /// <param name="fromGLLevel">开始级别</param>
        /// <param name="toGLLevel">结束级别</param>
        /// <param name="showEndLevel">只显示未级科目</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="showCumulation">本期无发生额，累计有发生显示</param>
        /// <returns></returns>
        public List<GLBalanceData> GetGLBalanceDataList(Guid[] companyIDs,
                                                        string fromGLCode,
                                                        string toGLCode,
                                                        GLCodeType glCodeType,
                                                        int fromGLLevel,
                                                        int toGLLevel,
                                                        bool showEndLevel,
                                                        DateTime fromDate,
                                                        DateTime toDate,
                                                        bool showCumulation,
                                                        Guid? CurrencyID,
                                                        GLCodeLedgerStyle format)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetFCGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@FromGLCode", DbType.String, fromGLCode);
                db.AddInParameter(dbCommand, "@ToGLCode", DbType.String, toGLCode);
                db.AddInParameter(dbCommand, "@FromGLLevel", DbType.Int32, fromGLLevel);
                db.AddInParameter(dbCommand, "@ToGLLevel", DbType.Int32, toGLLevel);
                db.AddInParameter(dbCommand, "@GlCodeType", DbType.Byte, glCodeType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@ShowCumulation", DbType.Boolean, showCumulation);
                db.AddInParameter(dbCommand, "@IsLeaf", DbType.Boolean, showEndLevel);
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, CurrencyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<GLBalanceData> list = (from d in ds.Tables[0].AsEnumerable()
                                            select new GLBalanceData
                                            {
                                                GLID = d.Field<Guid?>("GLID"),
                                                GLCode = d.Field<String>("GLCode"),
                                                GLName = d.Field<String>("GLName"),
                                                ParentID = d.Field<Guid?>("ParentID"),
                                                GLCodeType = (GLCodeType)d.Field<Byte>("GLCodeType"),
                                                GLCodeTypeName = d.Field<String>("GLCodeTypeName"),
                                                LevelCode = d.Field<int>("LevelCode"),
                                                BeginningDir = EnumHelper.GetDescription<GLCodeProperty>((GLCodeProperty)d.Field<Byte>("BeginningDir"), LocalData.IsEnglish).Replace("方", ""),
                                                BeginningDebit = d.Field<Decimal?>("BeginningDebit"),
                                                BeginningCredit = d.Field<Decimal?>("BeginningCredit"),
                                                BeginningOrgAmt = d.Field<Decimal?>("BeginningOrgAmtBalance"),
                                                BeginningBalance = d.Field<Decimal?>("BeginningBalance"),
                                                Debit = d.Field<Decimal?>("Debit"),
                                                Credit = d.Field<Decimal?>("Credit"),
                                                DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                TermEndDir = EnumHelper.GetDescription<GLCodeProperty>((GLCodeProperty)d.Field<Byte>("TermEndDir"), LocalData.IsEnglish).Replace("方", ""),
                                                TermEndDebit = d.Field<Decimal?>("TermEndDebit"),
                                                TermEndCredit = d.Field<Decimal?>("TermEndCredit"),
                                                TermEndOrgAmt = d.Field<Decimal?>("TermEndOrgAmt"),
                                                TermEndBalance = d.Field<Decimal?>("TermEndBalance"),
                                                ChildCount = d.Field<int>("ChildCount"),
                                                IsFCTotal=d.Field<Boolean>("IsFCTotal"),
                                            }).ToList();

                if (showEndLevel)
                {
                    //只显末级科目
                    list = (from d in list where (d.ChildCount == 0 || d.LevelCode == 0) select d).ToList();
                }

                if (format == GLCodeLedgerStyle.AMOUNT)
                {
                    //金额式的报表，不用显示外币合计
                    list = (from d in list where d.IsFCTotal == false select d).ToList();
                }

                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 科目明细表
        /// <summary>
        /// 科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <param name="orderByGL">按科目排序</param>
        /// <returns></returns>
        public List<GLDetailData> GetGLDetailDataList(Guid[] companyIDs,
                                                      Guid glID,
                                                      DateTime fromDate,
                                                      DateTime toDate)
        {
            try
            {
                List<GLDetailData> list = GetGLDetailDataList(companyIDs,glID,fromDate,toDate);
                
                List<GLDetailData> dataList = new List<GLDetailData>();
                int count = list.Count;
                decimal? monthDebit = 0.0m, monthCredit = 0.0m, totalDebit = 0.0m, totalCredit = 0.0m;
                if(list.Count>1)
                {
                    int i = 0;
                    foreach (GLDetailData item in list)
                    {
                        i++;
                        if (i==1)
                        {
                            if (item.Direction == GLCodeProperty.Debit)
                            {
                                totalDebit = item.Balance;
                            }
                            else if (item.Direction == GLCodeProperty.Debit)
                            {
                                totalCredit = item.Balance;
                            } 
                            dataList.Add(item);
                            //第一行期初余额不处理数据
                            continue;
                        }
                        //上一行数据
                        GLDetailData lastitem = list[i - 2];

                        #region 本月合计&累计
                        GLDetailData monthTotal = new GLDetailData();
                        GLDetailData totalInfo = new GLDetailData();
                        if (i > 2)
                        {
                            if (item.Month != lastitem.Month)
                            {
                                //本月合计   
                                monthTotal = new GLDetailData();
                                monthTotal.Remark = lastitem.Month + "月合计";
                                monthTotal.Direction = lastitem.Direction;
                                monthTotal.Balance = lastitem.Balance;
                                monthTotal.Debit = monthDebit;
                                monthTotal.Credit = monthCredit;
                                if (monthTotal.Debit > monthTotal.Credit)
                                {
                                    monthTotal.Direction = GLCodeProperty.Debit;
                                    monthTotal.Balance = monthTotal.Debit - monthTotal.Credit;
                                }
                                else if (monthTotal.Debit < monthTotal.Credit)
                                {
                                    monthTotal.Direction = GLCodeProperty.Debit;
                                    monthTotal.Balance = monthTotal.Credit - monthTotal.Debit;
                                }
                                else
                                {
                                    monthTotal.Direction = GLCodeProperty.Unknown;
                                    monthTotal.Balance = 0;
                                }
                                monthDebit = 0.0m;
                                monthCredit = 0.0m;
                                //累计    
                                totalInfo = new GLDetailData();
                                totalInfo.Remark = "累计";
                                totalInfo.Direction = lastitem.Direction;
                                totalInfo.Balance = lastitem.Balance;
                                totalInfo.Debit = totalDebit;
                                totalInfo.Credit = totalCredit;
                                if (totalInfo.Debit > totalInfo.Credit)
                                {
                                    totalInfo.Direction = GLCodeProperty.Debit;
                                    totalInfo.Balance = totalInfo.Debit - totalInfo.Credit;
                                }
                                else if (totalInfo.Debit < totalInfo.Credit)
                                {
                                    totalInfo.Direction = GLCodeProperty.Debit;
                                    totalInfo.Balance = totalInfo.Credit - totalInfo.Debit;
                                }
                                else
                                {
                                    totalInfo.Direction = GLCodeProperty.Unknown;
                                    totalInfo.Balance = 0;
                                }
                                dataList.Add(monthTotal);
                                dataList.Add(totalInfo);
                            }
                        }
                        #endregion

                        monthDebit += item.Debit;
                        monthCredit += item.Credit;
                        totalDebit += item.Debit;
                        totalCredit += item.Credit;

                    
                        if (item.Direction==GLCodeProperty.Credit)
                        {
                            #region 当前行的余额方向是贷

                            if (lastitem.Direction == GLCodeProperty.Credit)
                            {
                                //上一行的余额方向是贷: 本次余额方向是贷方，余额=上一行的余额+本次余额
                                item.Balance =Utility.GetDecimal(lastitem.Balance) + Utility.GetDecimal(item.Balance);
                            }
                            else if (lastitem.Direction == GLCodeProperty.Debit)
                            {
                                //上一行的余额方向是借
                                if (Utility.GetDecimal(lastitem.Balance) > Utility.GetDecimal(item.Balance))
                                {
                                    //上一行的余额>本次的余额：方向为借方，余额=上一行的余额-本次的余额
                                    item.Direction = GLCodeProperty.Debit;
                                    item.Balance =Utility.GetDecimal(lastitem.Balance) - Utility.GetDecimal(item.Balance);
                                }
                                else if (Utility.GetDecimal(lastitem.Balance) < Utility.GetDecimal(item.Balance))
                                {
                                    //上一行的余额<本次的余额：方向为贷方，余额=本次的余额-上一行的余额
                                    item.Direction = GLCodeProperty.Credit;
                                    item.Balance = Utility.GetDecimal(item.Balance) - Utility.GetDecimal(lastitem.Balance);
                                }
                                else if (Utility.GetDecimal(lastitem.Balance) == Utility.GetDecimal(item.Balance))
                                {
                                    //上一行行的余额=本次的余额
                                    item.Direction = GLCodeProperty.Unknown;
                                    item.Balance = 0;
                                }

                            }
                            #endregion
                        }
                        else if(item.Direction==GLCodeProperty.Debit)
                        {
                            #region 当前行的余额方向是借
                            if (lastitem.Direction == GLCodeProperty.Debit)
                            {
                                //上一行的余额方向是借: 余额为借方，余额=上次的余额+本次的余额
                                item.Balance = Utility.GetDecimal(lastitem.Balance) + Utility.GetDecimal(item.Balance);
                            }
                            else if (lastitem.Direction == GLCodeProperty.Credit)
                            {
                                //上一行的余额方向是贷
                                if (Utility.GetDecimal(lastitem.Balance) > Utility.GetDecimal(item.Balance))
                                {
                                    //上一行的余额>本次的余额：方向为借贷方,余额=上次的余额-本次的余额
                                    item.Direction = GLCodeProperty.Credit;
                                    item.Balance = Utility.GetDecimal(lastitem.Balance) - Utility.GetDecimal(item.Balance);
                                }
                                else if (Utility.GetDecimal(lastitem.Balance) < Utility.GetDecimal(item.Balance))
                                {
                                    //上一行的余额<本次的余额：方向为贷方，余额=本次余额-上次的余额
                                    item.Direction = GLCodeProperty.Debit;
                                    item.Balance = Utility.GetDecimal(item.Balance) - Utility.GetDecimal(lastitem.Balance);
                                }
                                else if ( Utility.GetDecimal(lastitem.Balance)== Utility.GetDecimal(item.Balance))
                                {
                                    item.Direction = GLCodeProperty.Unknown;
                                    item.Balance = 0;
                                }

                            }
                            #endregion
                        }

                        dataList.Add(item);

                       #region 最后一行的月合计&累计
                        if (i == count)
                        {
                                //本月合计   
                                monthTotal = new GLDetailData();
                                monthTotal.Remark = lastitem.Month + "月合计";
                                monthTotal.Direction = lastitem.Direction;
                                monthTotal.Balance = lastitem.Balance;
                                monthTotal.Debit = monthDebit;
                                monthTotal.Credit = monthCredit;
                                if (monthTotal.Debit > monthTotal.Credit)
                                {
                                    monthTotal.Direction = GLCodeProperty.Debit;
                                    monthTotal.Balance = monthTotal.Debit - monthTotal.Credit;
                                }
                                else if (monthTotal.Debit < monthTotal.Credit)
                                {
                                    monthTotal.Direction = GLCodeProperty.Debit;
                                    monthTotal.Balance = monthTotal.Credit - monthTotal.Debit;
                                }
                                else
                                {
                                    monthTotal.Direction = GLCodeProperty.Unknown;
                                    monthTotal.Balance = 0;
                                }
                                monthDebit = 0.0m;
                                monthCredit = 0.0m;
                                //累计    
                                totalInfo = new GLDetailData();
                                totalInfo.Remark = "累计";
                                totalInfo.Direction = lastitem.Direction;
                                totalInfo.Balance = lastitem.Balance;
                                totalInfo.Debit = totalDebit;
                                totalInfo.Credit = totalCredit;
                                if (totalInfo.Debit > totalInfo.Credit)
                                {
                                    totalInfo.Direction = GLCodeProperty.Debit;
                                    totalInfo.Balance = totalInfo.Debit - totalInfo.Credit;
                                }
                                else if (totalInfo.Debit < totalInfo.Credit)
                                {
                                    totalInfo.Direction = GLCodeProperty.Debit;
                                    totalInfo.Balance = totalInfo.Credit - totalInfo.Debit;
                                }
                                else
                                {
                                    totalInfo.Direction = GLCodeProperty.Unknown;
                                    totalInfo.Balance = 0;
                                }
                                dataList.Add(monthTotal);
                                dataList.Add(totalInfo);
                            }
                    }
                        #endregion
                }

                return dataList;

            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 科目明细帐
        /// <summary>
        /// 科目明细帐
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        public List<GLDetailData> GetFCGLDetailBalanceList(Guid[] companyIDs,
                                                      Guid glID,
                                                      DateTime fromDate,
                                                      DateTime toDate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetGLDetailDataList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@GlID", DbType.Guid, glID);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<GLDetailData> list = (from d in ds.Tables[1].AsEnumerable()
                                            select new GLDetailData
                                            {
                                                ID = d.Field<Guid?>("ID"),
                                                GLID = d.Field<Guid?>("GLID"),
                                                GLCode=d.Field<String>("GLCode"),
                                                Date = d.Field<DateTime?>("Date"),
                                                VoucherNo = d.Field<String>("VoucherNo"),
                                                Remark = d.Field<String>("Remark"),
                                                FC_Rate = d.Field<String>("FC_Rate"),
                                                DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                Debit = d.Field<Decimal?>("Debit"),
                                                CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                Credit = d.Field<Decimal?>("Credit"),
                                                Direction = (GLCodeProperty)d.Field<Byte>("Direction"),
                                                BalanceOrgAmt = d.Field<Decimal?>("BalanceOrgAmt"),
                                                BalanceRate = d.Field<Decimal?>("BalanceRate"),
                                                Balance = d.Field<Decimal?>("Balance")
                                            }).ToList();
                return list;

            }
            catch (Exception ex) { throw ex; }
        }



        #endregion

        #region 客户余额表
        /// <summary>
        /// 客户余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="propertys">余额方向(1借方,2贷方)</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <returns></returns>
        public List<CustomerGLBalance> GetCustomerGLBalanceList(Guid[] companyIDs,
                                                   Guid[] customerIDs,
                                                   Guid[] glIDs,
                                                   DateTime fromDate,
                                                   DateTime toDate,
                                                   GLCodeProperty[] propertys,
                                                   GLCodeLedgerStyle format)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, customerIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIDs.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerGLBalance> list = (from d in ds.Tables[0].AsEnumerable()
                                                select new CustomerGLBalance
                                                {
                                                    GLID = d.Field<Guid?>("GLID"),
                                                    GLCode = d.Field<String>("GLCode"),
                                                    GLName = d.Field<String>("GLName"),
                                                    CustomerID = d.Field<Guid?>("CustomerID"),
                                                    CustomerCode = d.Field<String>("CustomerCode"),
                                                    CustomerName = d.Field<String>("CustomerName"),
                                                    BeginningDirection = (GLCodeProperty)d.Field<Byte>("BeginningDirection"),
                                                    BeginningOrgAmt = d.Field<Decimal?>("BeginningOrgAmt"),
                                                    BeginningBalance = d.Field<Decimal?>("BeginningBalance"),
                                                    PeriodCredit = d.Field<Decimal?>("PeriodCredit"),
                                                    PeriodDebit = d.Field<Decimal?>("PeriodDebit"),
                                                    PeriodCreditOrgAmt = d.Field<Decimal?>("PeriodCreditOrgAmt"),
                                                    PeriodDebitOrgAmt = d.Field<Decimal?>("PeriodDebitOrgAmt"),
                                                    PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                    PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                    PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                                }).ToList();
                //外币合计
                if (format ==  GLCodeLedgerStyle.OUTGOAMOUNT)
                {
                    List<CustomerGLBalance> fcList = (from d in ds.Tables[1].AsEnumerable()
                                                    select new CustomerGLBalance
                                                    {
                                                        GLID = d.Field<Guid?>("GLID"),
                                                        GLCode = d.Field<String>("GLCode"),
                                                        GLName = d.Field<String>("GLName"),
                                                        CustomerID = d.Field<Guid?>("CustomerID"),
                                                        CustomerCode = d.Field<String>("CustomerCode"),
                                                        CustomerName = d.Field<String>("CustomerName"),
                                                        BeginningDirection = (GLCodeProperty)d.Field<Byte>("BeginningDirection"),
                                                        BeginningOrgAmt = d.Field<Decimal?>("BeginningOrgAmt"),
                                                        BeginningBalance = d.Field<Decimal?>("BeginningBalance"),
                                                        PeriodCredit = d.Field<Decimal?>("PeriodCredit"),
                                                        PeriodDebit = d.Field<Decimal?>("PeriodDebit"),
                                                        PeriodCreditOrgAmt = d.Field<Decimal?>("PeriodCreditOrgAmt"),
                                                        PeriodDebitOrgAmt = d.Field<Decimal?>("PeriodDebitOrgAmt"),
                                                        PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                        PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                        PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                                    }).ToList();
                    foreach (CustomerGLBalance obj in fcList)
                    {
                        list.Add(obj);
                    }
                }
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region  客户三栏余额表
        /// <summary>
        /// 客户三栏余额表(不需要)
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="glID">科目ID</param>
        /// <param name="years">年份</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <returns></returns>
        public List<Customer3ColumnGLBalance> GetCustomer3ColumnGLBalanceList(Guid[] companyIDs,
                                                                              Guid customerID,
                                                                              Guid glID,
                                                                              int years,
                                                                              bool noAccounting)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomer3ColumnGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, glID);
                db.AddInParameter(dbCommand, "@Years", DbType.Int32, years);
                db.AddInParameter(dbCommand, "@NoAccounting", DbType.Boolean, noAccounting);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Customer3ColumnGLBalance> list = (from d in ds.Tables[0].AsEnumerable()
                                                       select new Customer3ColumnGLBalance
                                                       {
                                                           Date=d.Field<DateTime?>("Date"),
                                                           Remark=d.Field<String>("Remark"),
                                                           Credit = d.Field<Decimal?>("Credit"),
                                                           Debit = d.Field<Decimal?>("Debit"),
                                                           PeriodEndDirection = d.Field<Byte?>("PeriodEndDirection")==null?null:(GLCodeProperty?)d.Field<Byte>("PeriodEndDirection"),
                                                           PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                                       }).ToList();

                return list;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 客户科目明细表
        /// <summary>
        /// 客户科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="glIDs">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <returns></returns>
        public List<CustomerGLDetail> GetCustomerGLDetailList(Guid[] companyIDs,
                                                 Guid[] customerIDs,
                                                 Guid[] glIDs,
                                                 DateTime fromDate,
                                                 DateTime toDate,
                                                 GLCodeLedgerStyle format)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerGLDetailList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, customerIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIDs.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsForeignCurrencyAmount", DbType.Boolean, format == GLCodeLedgerStyle.OUTGOAMOUNT ? true : false);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                #region 期初数据
                List<CustomerGLDetail> beginningList = (from d in ds.Tables[0].AsEnumerable()
                                               select new CustomerGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   CustomerID = d.Field<Guid?>("CustomerID"),
                                                   CustomerCode = d.Field<String>("CustomerCode"),
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                   GroupNo="1",
                                                   IndexNo=100
                                               }).ToList();
                #endregion

                #region 本期数据
                List<CustomerGLDetail> list = (from d in ds.Tables[1].AsEnumerable()
                                               select new CustomerGLDetail
                                                       {
                                                          ID=d.Field<Guid?>("ID"),
                                                          GLID=d.Field<Guid?>("GLID"),
                                                          GLCode=d.Field<String>("GLCode"),
                                                          GLName=d.Field<String>("GLName"),
                                                          Date=d.Field<DateTime?>("Date"),
                                                          VoucherNo = d.Field<String>("VoucherNo"),
                                                          CustomerID=d.Field<Guid?>("CustomerID"),
                                                          CustomerCode = d.Field<String>("CustomerCode"),
                                                          CustomerName = d.Field<String>("CustomerName"),
                                                          Remark=d.Field<String>("Remark"),
                                                          FC_Rate = d.Field<String>("FC_Rate"),
                                                          DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                          Debit = d.Field<Decimal?>("Debit"),
                                                          CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                          Credit = d.Field<Decimal?>("Credit"),
                                                          PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                          PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                          PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                          GroupNo="2"
                                                       }).ToList();
                #endregion

                #region 小计数据
                List<CustomerGLDetail> totalList = (from d in ds.Tables[2].AsEnumerable()
                                               select new CustomerGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   CustomerID = d.Field<Guid?>("CustomerID"),
                                                   CustomerCode = d.Field<String>("CustomerCode"),
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   FC_Rate = d.Field<String>("FC_Rate"),
                                                   DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                   Debit = d.Field<Decimal?>("Debit"),
                                                   CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                   Credit = d.Field<Decimal?>("Credit"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                   GroupNo="3"
                                               }).ToList();
                #endregion

                #region 合并结果
                List<CustomerGLDetail> DataList = new List<CustomerGLDetail>();
                foreach (CustomerGLDetail beginInfo in beginningList)
                {
                    //期初值
                    beginInfo.IndexNo = 0;
                    DataList.Add(beginInfo);


                    //查询出所有的明细数据
                    List<CustomerGLDetail> searchList = (from d in list where d.CustomerID == beginInfo.CustomerID && d.GLID == beginInfo.GLID select d).ToList();
                    int i = 0;
                    foreach (CustomerGLDetail item in searchList)
                    {
                        i++;
                        //上一行的数据
                        CustomerGLDetail lastitem = DataList[DataList.Count - 1];

                        //更新本行的余额方向、余额
                        UpdateCurrentData(item,lastitem);
                       
                        item.GroupNo = "2";
                        item.IndexNo = i;
                        item.IsAddList = true;
                        DataList.Add(item);
                    }
                }
                #endregion

                #region 处理没有期初的数据
                List<CustomerGLDetail> noAddList = (from d in list where d.IsAddList == false orderby d.GLID,d.CustomerID,d.Date select d).ToList();
                int n = 0;
                foreach (CustomerGLDetail item in noAddList)
                {
                    n++;
                    if (n == 1)
                    {
                        //第一行不处理
                        DataList.Add(item);
                        continue;
                    }
                    CustomerGLDetail lastitem = noAddList[n - 2];
                    if (item.GLID == lastitem.GLID && item.CustomerID == lastitem.CustomerID)
                    {
                        UpdateCurrentData(item, lastitem);
                    }
                    item.IndexNo = n;
                    DataList.Add(item);
                }
                #endregion
                //加入客户+科目的 小计
                DataList.AddRange(totalList);
          
                //排序 (科目+客户)1期初、2本期、3小计、4合计
                DataList = (from b in DataList orderby b.GLCode, b.CustomerName, b.GroupNo, b.IndexNo select b).ToList();

                #region 合计
                if (DataList.Count > 0)
                {
                    CustomerGLDetail totalInfo = (from d in ds.Tables[3].AsEnumerable()
                                                        select new CustomerGLDetail
                                                        {
                                                            ID = d.Field<Guid?>("ID"),
                                                            GLID = d.Field<Guid?>("GLID"),
                                                            GLCode = d.Field<String>("GLCode"),
                                                            GLName = d.Field<String>("GLName"),
                                                            Date = d.Field<DateTime?>("Date"),
                                                            VoucherNo = d.Field<String>("VoucherNo"),
                                                            CustomerID = d.Field<Guid?>("CustomerID"),
                                                            CustomerCode = d.Field<String>("CustomerCode"),
                                                            CustomerName = d.Field<String>("CustomerName"),
                                                            Remark = d.Field<String>("Remark"),
                                                            FC_Rate = d.Field<String>("FC_Rate"),
                                                            DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                            Debit = d.Field<Decimal?>("Debit"),
                                                            CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                            Credit = d.Field<Decimal?>("Credit"),
                                                            PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                            PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                            PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                            GroupNo = "4"
                                                        }).SingleOrDefault();
                    DataList.Add(totalInfo);
                }
                #endregion

                #region 外币合计
                if (DataList.Count > 0 && format== GLCodeLedgerStyle.OUTGOAMOUNT)
                {
                    List<CustomerGLDetail> fctotalList = (from d in ds.Tables[4].AsEnumerable()
                                                        select new CustomerGLDetail
                                                        {
                                                            ID = d.Field<Guid?>("ID"),
                                                            GLID = d.Field<Guid?>("GLID"),
                                                            GLCode = d.Field<String>("GLCode"),
                                                            GLName = d.Field<String>("GLName"),
                                                            Date = d.Field<DateTime?>("Date"),
                                                            VoucherNo = d.Field<String>("VoucherNo"),
                                                            CustomerID = d.Field<Guid?>("CustomerID"),
                                                            CustomerCode = d.Field<String>("CustomerCode"),
                                                            CustomerName = d.Field<String>("CustomerName"),
                                                            Remark = d.Field<String>("Remark"),
                                                            FC_Rate = d.Field<String>("FC_Rate"),
                                                            DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                            Debit = d.Field<Decimal?>("Debit"),
                                                            CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                            Credit = d.Field<Decimal?>("Credit"),
                                                            PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                            PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                            PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                            GroupNo = "5"
                                                        }).ToList();
                    foreach (CustomerGLDetail obj in fctotalList)
                    {
                        DataList.Add(obj);
                    }
                }
                #endregion

                return DataList;
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// 更新本行的数据
        /// </summary>
        /// <param name="item"></param>
        /// <param name="lastitem"></param>
        private void UpdateCurrentData(CustomerGLDetail item, CustomerGLDetail lastitem)
        {
            //更新本行的余额方向、余额
            if (item.PeriodEndDirection == GLCodeProperty.Credit)
            {
                #region 当前行的余额方向是贷

                if (lastitem.PeriodEndDirection == GLCodeProperty.Credit)
                {
                    //上一行的余额方向是贷: 本次余额方向是贷方，余额=上一行的余额+本次余额
                    item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) + Utility.GetDecimal(item.PeriodEndBalance);
                    if (string.IsNullOrEmpty(item.FC_Rate))
                        item.PeriodEndOrgAmt = 0;
                    else
                        item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) + Utility.GetDecimal(item.PeriodEndOrgAmt);
                        
                }
                else if (lastitem.PeriodEndDirection == GLCodeProperty.Debit)
                {
                    //上一行的余额方向是借
                    if (Utility.GetDecimal(lastitem.PeriodEndBalance) > Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额>本次的余额：方向为借方，余额=上一行的余额-本次的余额
                        item.PeriodEndDirection = GLCodeProperty.Debit;
                        item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) - Utility.GetDecimal(item.PeriodEndBalance);
                        if (string.IsNullOrEmpty(item.FC_Rate))
                            item.PeriodEndOrgAmt = 0;
                        else
                            item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) - Utility.GetDecimal(item.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) < Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额<本次的余额：方向为贷方，余额=本次的余额-上一行的余额
                        item.PeriodEndDirection = GLCodeProperty.Credit;
                        item.PeriodEndBalance = Utility.GetDecimal(item.PeriodEndBalance) - Utility.GetDecimal(lastitem.PeriodEndBalance);
                        if (string.IsNullOrEmpty(item.FC_Rate))
                            item.PeriodEndOrgAmt = 0;
                        else
                            item.PeriodEndOrgAmt = Utility.GetDecimal(item.PeriodEndOrgAmt) - Utility.GetDecimal(lastitem.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) == Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行行的余额=本次的余额
                        item.PeriodEndDirection = GLCodeProperty.Unknown;
                        item.PeriodEndBalance = 0;
                        item.PeriodEndOrgAmt = 0;
                    }

                }
                #endregion
            }
            else if (item.PeriodEndDirection == GLCodeProperty.Debit)
            {
                #region 当前行的余额方向是借
                if (lastitem.PeriodEndDirection == GLCodeProperty.Debit)
                {
                    //上一行的余额方向是借: 余额为借方，余额=上次的余额+本次的余额
                    item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) + Utility.GetDecimal(item.PeriodEndBalance);
                    if (string.IsNullOrEmpty(item.FC_Rate))
                        item.PeriodEndOrgAmt = 0;
                    else
                        item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) + Utility.GetDecimal(item.PeriodEndOrgAmt);
                }
                else if (lastitem.PeriodEndDirection == GLCodeProperty.Credit)
                {
                    //上一行的余额方向是贷
                    if (Utility.GetDecimal(lastitem.PeriodEndBalance) > Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额>本次的余额：方向为借贷方,余额=上次的余额-本次的余额
                        item.PeriodEndDirection = GLCodeProperty.Credit;
                        item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) - Utility.GetDecimal(item.PeriodEndBalance);
                        if (string.IsNullOrEmpty(item.FC_Rate))
                            item.PeriodEndOrgAmt = 0;
                        else
                            item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) - Utility.GetDecimal(item.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) < Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额<本次的余额：方向为贷方，余额=本次余额-上次的余额
                        item.PeriodEndDirection = GLCodeProperty.Debit;
                        item.PeriodEndBalance = Utility.GetDecimal(item.PeriodEndBalance) - Utility.GetDecimal(lastitem.PeriodEndBalance);
                        if (string.IsNullOrEmpty(item.FC_Rate))
                            item.PeriodEndOrgAmt = 0;
                        else
                            item.PeriodEndOrgAmt = Utility.GetDecimal(item.PeriodEndOrgAmt) - Utility.GetDecimal(lastitem.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) == Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        item.PeriodEndDirection = GLCodeProperty.Unknown;
                        item.PeriodEndBalance = 0;
                        item.PeriodEndOrgAmt = 0;
                    }

                }
                #endregion
            }
        }
        #endregion

        #region 个人科目余额表
        /// <summary>
        /// 个人科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="property">统计方向(0全部;1借方;2贷方)</param>
        /// <returns></returns>
        public List<PersonalGLBalance> GetPersonalGLBalanceList(Guid[] companyIDs,
                                                   Guid[] departmentIDs,
                                                   Guid[] personalIDs,
                                                   Guid[] glIds,
                                                   DateTime fromDate,
                                                   DateTime toDate,
                                                   GLCodeProperty property)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPersonalGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@DepartmentIDs", DbType.String, departmentIDs.Join());
                db.AddInParameter(dbCommand, "@PersonalIDs", DbType.String, personalIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIds.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<PersonalGLBalance> list = (from d in ds.Tables[0].AsEnumerable()
                                                select new PersonalGLBalance
                                                       {
                                                           GLID = d.Field<Guid?>("GLID"),
                                                           GLCode = d.Field<String>("GLCode"),
                                                           GLName = d.Field<String>("GLName"),
                                                           DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                           DepartmentName = d.Field<String>("DepartmentName"),
                                                           PersonalID = d.Field<Guid?>("PersonalID"),
                                                           PersonalName = d.Field<String>("PersonalName"),
                                                           BeginningDirection = d.Field<Byte?>("BeginningDirection") == null ? null : (GLCodeProperty?)d.Field<Byte?>("BeginningDirection"),
                                                           BeginningOrgAmt = d.Field<Decimal?>("BeginningOrgAmt"),
                                                           BeginningBalance = d.Field<Decimal?>("BeginningBalance"),
                                                           PeriodDebitOrgAmt = d.Field<Decimal?>("PeriodDebitOrgAmt"),
                                                           PeriodDebit = d.Field<Decimal?>("PeriodDebit"),
                                                           PeriodCreditOrgAmt = d.Field<Decimal?>("PeriodCreditOrgAmt"),
                                                           PeriodCredit = d.Field<Decimal?>("PeriodCredit"),
                                                           PeriodEndDirection = (GLCodeProperty?)d.Field<Byte?>("PeriodEndDirection") == null ? null : (GLCodeProperty?)d.Field<Byte?>("PeriodEndDirection"),
                                                           PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                           PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                                       }).ToList();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 个人三栏余额表
        /// <summary>
        /// 个人三栏余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门部门ID</param>
        /// <param name="personalID">个人ID</param>
        /// <param name="glId">科目ID</param>
        /// <param name="years">年份</param>
        /// <param name="property">余额方向</param>
        /// <param name="noAccounting"></param>
        /// <returns></returns>
        public List<Personal3ColumnGLBalance> GetPersonal3ColumnGLBalanceList(Guid[] companyIDs,
                                                                Guid[] departmentIDs,
                                                                Guid personalID,
                                                                Guid glId,
                                                                DateTime fromDate,
                                                                DateTime toDate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPersonal3ColumnGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@DepartmentIDs", DbType.String, departmentIDs.Join());
                db.AddInParameter(dbCommand, "@PersonalID", DbType.Guid, personalID);
                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, glId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Personal3ColumnGLBalance> list = (from d in ds.Tables[0].AsEnumerable()
                                                       select new Personal3ColumnGLBalance
                                                       {
                                                           Date = d.Field<DateTime?>("Date"),
                                                           Remark = d.Field<String>("Remark"),
                                                           Credit = d.Field<Decimal?>("Credit"),
                                                           Debit = d.Field<Decimal?>("Debit"),
                                                           Direction = d.Field<Byte?>("Direction") == null ? null : (GLCodeProperty?)d.Field<Byte?>("Direction"),
                                                           Balance = d.Field<Decimal?>("Balance")
                                                       }).ToList();

                return list;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 个人科目明细表
        /// <summary>
        /// 个人科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <param name="orderByDebit">借方在前,贷方在后</param>
        /// <returns></returns>
        public List<PersonalGLDetail> GetPersonalGLDetailList(Guid[] companyIDs,
                                                 Guid[] departmentIDs,
                                                 Guid[] personalIDs,
                                                 Guid[] glIds,
                                                 DateTime fromDate,
                                                 DateTime toDate,
                                                 bool orderByDebit)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPersonalGLDetailList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@DepartmentIDs", DbType.String, departmentIDs.Join());
                db.AddInParameter(dbCommand, "@PersonalIDs", DbType.String, personalIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIds.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                //db.AddInParameter(dbCommand, "@OrderByDebit", DbType.Boolean, orderByDebit);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                #region 期初数据
                List<PersonalGLDetail> beginningList = (from d in ds.Tables[0].AsEnumerable()
                                               select new PersonalGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                   DepartmentName = d.Field<String>("DepartmentName"),
                                                   PersonalID = d.Field<Guid?>("PersonalID"),
                                                   PersonalName = d.Field<String>("PersonalName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   FC_Rate = d.Field<String>("FC_Rate"),
                                                   DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                   Debit = d.Field<Decimal?>("Debit"),
                                                   CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                   Credit = d.Field<Decimal?>("Credit"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                   GroupNo="1",
                                                   IndexNo=100
                                               }).ToList();

                #endregion

                #region 本期数据
                List<PersonalGLDetail> list = (from d in ds.Tables[1].AsEnumerable()
                                               select new PersonalGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                   DepartmentName = d.Field<String>("DepartmentName"),
                                                   PersonalID = d.Field<Guid?>("PersonalID"),
                                                   PersonalName = d.Field<String>("PersonalName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   FC_Rate = d.Field<String>("FC_Rate"),
                                                   DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                   Debit = d.Field<Decimal?>("Debit"),
                                                   CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                   Credit = d.Field<Decimal?>("Credit"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                   GroupNo="2"
                                               }).ToList();
                #endregion

                #region 个人小计
                List<PersonalGLDetail> personalTotallist = (from d in ds.Tables[2].AsEnumerable()
                                               select new PersonalGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                   DepartmentName = d.Field<String>("DepartmentName"),
                                                   PersonalID = d.Field<Guid?>("PersonalID"),
                                                   PersonalName = d.Field<String>("PersonalName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   FC_Rate = d.Field<String>("FC_Rate"),
                                                   DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                   Debit = d.Field<Decimal?>("Debit"),
                                                   CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                   Credit = d.Field<Decimal?>("Credit"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                   GroupNo="3",
                                               }).ToList();
                #endregion

                #region 部门小计
                List<PersonalGLDetail> departmentTotallist = (from d in ds.Tables[3].AsEnumerable()
                                               select new PersonalGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                   DepartmentName = d.Field<String>("DepartmentName"),
                                                   PersonalID = d.Field<Guid?>("PersonalID"),
                                                   PersonalName = d.Field<String>("PersonalName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   FC_Rate = d.Field<String>("FC_Rate"),
                                                   DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                   Debit = d.Field<Decimal?>("Debit"),
                                                   CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                   Credit = d.Field<Decimal?>("Credit"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                   GroupNo="4"
                                               }).ToList();
                #endregion

                #region 合并结果
                List<PersonalGLDetail> DataList = new List<PersonalGLDetail>();
                foreach (PersonalGLDetail beginInfo in beginningList)
                {
                    //期初值
                    DataList.Add(beginInfo);

                    #region 查询出所有的明细数据
                    List<PersonalGLDetail> searchList = (from d in list where 
                                                              d.PersonalID == beginInfo.PersonalID 
                                                           && d.GLID == beginInfo.GLID 
                                                           && d.DepartmentID==beginInfo.DepartmentID
                                                              select d).ToList();
                    int i = 0;
                    foreach (PersonalGLDetail item in searchList)
                    {
                        i++;
                        //上一行的数据
                        PersonalGLDetail lastitem = DataList[DataList.Count - 1];

                        //更新本行的余额方向、余额
                        UpdateCurrentDataByPersonal(item,lastitem);

                        item.IndexNo = i;
                        item.IsAddList = true;
                        DataList.Add(item);
                    }

                    #endregion
                }
                #endregion

                #region 处理没有期初的数据
                List<PersonalGLDetail> noAddList = (from d in list where d.IsAddList == false orderby d.GLID, d.DepartmentID,d.PersonalID, d.Date select d).ToList();
                int n = 0;
                foreach (PersonalGLDetail item in noAddList)
                {
                    n++;
                    if (n == 1)
                    {
                        //第一行不处理
                        DataList.Add(item);
                        continue;
                    }
                    PersonalGLDetail lastitem = noAddList[n - 2];
                    if (item.GLID == lastitem.GLID && item.DepartmentID == lastitem.DepartmentID && item.PersonalID == lastitem.PersonalID)
                    {
                        UpdateCurrentDataByPersonal(item, lastitem);
                    }
                    item.IndexNo = n;
                    DataList.Add(item);
                }
                #endregion

                #region 合计
                //加入个人小计
                DataList.AddRange(personalTotallist);
                DataList = (from d in DataList orderby d.GLCode, d.DepartmentName, d.PersonalName, d.GroupNo, d.IndexNo, d.Date select d).ToList();
                #region 加入部门科目小计
                List<PersonalGLDetail> SourceList = new List<PersonalGLDetail>();
                Guid? glID = Guid.Empty, departmentID = Guid.Empty;
                n = 0;
                foreach (PersonalGLDetail item in DataList)
                {
                    n++;
                    if (n == 1)
                    {
                        glID = item.GLID;
                        departmentID = item.DepartmentID;
                        SourceList.Add(item);
                        continue;
                    }
                    
                    if (item.GLID != glID||item.DepartmentID!=departmentID)
                    { 
                        //本次的科目+部门不等于上一行的科目

                        PersonalGLDetail orderItem = departmentTotallist.Find(delegate(PersonalGLDetail d) { return d.GLID == glID && d.DepartmentID == departmentID; });
                        if (orderItem != null)
                        {
                            SourceList.Add(orderItem);
                        }
                    }
                    SourceList.Add(item);
                  
                    glID = item.GLID;
                    departmentID = item.DepartmentID;

                    if (n == DataList.Count)
                    {
                        //最后一行的合计
                        //本次的科目+部门不等于上一行的科目
                        PersonalGLDetail totalItem = departmentTotallist.Find(delegate(PersonalGLDetail d) { return d.GLID == item.GLID && d.DepartmentID == item.DepartmentID; });
                        if (totalItem != null)
                        {
                            SourceList.Add(totalItem);
                        }
                    }
                }
                #endregion

                #region 加入合计

                #endregion

                PersonalGLDetail totalInfo = (from d in ds.Tables[4].AsEnumerable()
                                                    select new PersonalGLDetail
                                                    {
                                                        ID = d.Field<Guid?>("ID"),
                                                        GLID = d.Field<Guid?>("GLID"),
                                                        GLCode = d.Field<String>("GLCode"),
                                                        GLName = d.Field<String>("GLName"),
                                                        Date = d.Field<DateTime?>("Date"),
                                                        VoucherNo = d.Field<String>("VoucherNo"),
                                                        DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                        DepartmentName = d.Field<String>("DepartmentName"),
                                                        PersonalID = d.Field<Guid?>("PersonalID"),
                                                        PersonalName = d.Field<String>("PersonalName"),
                                                        Remark = d.Field<String>("Remark"),
                                                        FC_Rate = d.Field<String>("FC_Rate"),
                                                        DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                        Debit = d.Field<Decimal?>("Debit"),
                                                        CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                        Credit = d.Field<Decimal?>("Credit"),
                                                        PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                        PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                        PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance"),
                                                        GroupNo = "5",
                                                    }).SingleOrDefault();

                SourceList.Add(totalInfo);
                #endregion

                return SourceList;
            }
            catch (Exception ex) { throw ex; }
        }
        private void UpdateCurrentDataByPersonal(PersonalGLDetail item, PersonalGLDetail lastitem)
        {
            //更新本行的余额方向、余额
            if (item.PeriodEndDirection == GLCodeProperty.Credit)
            {
                #region 当前行的余额方向是贷

                if (lastitem.PeriodEndDirection == GLCodeProperty.Credit)
                {
                    //上一行的余额方向是贷: 本次余额方向是贷方，余额=上一行的余额+本次余额
                    item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) + Utility.GetDecimal(item.PeriodEndBalance);
                    item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) + Utility.GetDecimal(item.PeriodEndOrgAmt);
                }
                else if (lastitem.PeriodEndDirection == GLCodeProperty.Debit)
                {
                    //上一行的余额方向是借
                    if (Utility.GetDecimal(lastitem.PeriodEndBalance) > Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额>本次的余额：方向为借方，余额=上一行的余额-本次的余额
                        item.PeriodEndDirection = GLCodeProperty.Debit;
                        item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) - Utility.GetDecimal(item.PeriodEndBalance);
                        item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) - Utility.GetDecimal(item.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) < Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额<本次的余额：方向为贷方，余额=本次的余额-上一行的余额
                        item.PeriodEndDirection = GLCodeProperty.Credit;
                        item.PeriodEndBalance = Utility.GetDecimal(item.PeriodEndBalance) - Utility.GetDecimal(lastitem.PeriodEndBalance);
                        item.PeriodEndOrgAmt = Utility.GetDecimal(item.PeriodEndOrgAmt) - Utility.GetDecimal(lastitem.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) == Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行行的余额=本次的余额
                        item.PeriodEndDirection = GLCodeProperty.Unknown;
                        item.PeriodEndBalance = 0;
                    }
                }
                #endregion
            }
            else if (item.PeriodEndDirection == GLCodeProperty.Debit)
            {
                #region 当前行的余额方向是借
                if (lastitem.PeriodEndDirection == GLCodeProperty.Debit)
                {
                    //上一行的余额方向是借: 余额为借方，余额=上次的余额+本次的余额
                    item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) + Utility.GetDecimal(item.PeriodEndBalance);
                    item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) + Utility.GetDecimal(item.PeriodEndOrgAmt);
                }
                else if (lastitem.PeriodEndDirection == GLCodeProperty.Credit)
                {
                    //上一行的余额方向是贷
                    if (Utility.GetDecimal(lastitem.PeriodEndBalance) > Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额>本次的余额：方向为借贷方,余额=上次的余额-本次的余额
                        item.PeriodEndDirection = GLCodeProperty.Credit;
                        item.PeriodEndBalance = Utility.GetDecimal(lastitem.PeriodEndBalance) - Utility.GetDecimal(item.PeriodEndBalance);
                        item.PeriodEndOrgAmt = Utility.GetDecimal(lastitem.PeriodEndOrgAmt) - Utility.GetDecimal(item.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) < Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        //上一行的余额<本次的余额：方向为贷方，余额=本次余额-上次的余额
                        item.PeriodEndDirection = GLCodeProperty.Debit;
                        item.PeriodEndBalance = Utility.GetDecimal(item.PeriodEndBalance) - Utility.GetDecimal(lastitem.PeriodEndBalance);
                        item.PeriodEndOrgAmt = Utility.GetDecimal(item.PeriodEndOrgAmt) - Utility.GetDecimal(lastitem.PeriodEndOrgAmt);
                    }
                    else if (Utility.GetDecimal(lastitem.PeriodEndBalance) == Utility.GetDecimal(item.PeriodEndBalance))
                    {
                        item.PeriodEndDirection = GLCodeProperty.Unknown;
                        item.PeriodEndBalance = 0;
                    }

                }
                #endregion
            }
        }

        #endregion

    }
}

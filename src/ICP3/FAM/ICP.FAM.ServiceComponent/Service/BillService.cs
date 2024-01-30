using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ICP.FAM.ServiceComponent
{
    public partial class FinanceService
    {
        #region  获得账单列表GetBillListByList
        /// <summary>
        /// 获得账单列表
        /// </summary>
        /// <param name="auditorState">审核状态(0:全部、1:已审核、2:未审核)</param>
        /// <param name="writeOffStatue">核销状态(0:全部、1:已核销、2:未核销)</param>
        /// <param name="feeWay">收付类型(0:全部、1:应收、2:应付)</param>
        /// <param name="invoceStatue">发票状态(0:全部、1:已开发票、2:未开发票)</param>
        /// <param name="isCommission">是否为业务管理成本</param>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="billNo">账单号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="refNo">参考号</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="salesID">业务员</param>
        /// <param name="operateID">操作员</param>
        /// <param name="billingStartDate">计费开始时间</param>
        /// <param name="billingEndDate">计费结束时间</param>
        /// <param name="amountMin">核销金额最小值</param>
        /// <param name="amountMax">核销金额最大值</param>
        /// <param name="maxRecords">返回最大行行数</param>
        /// <param name="operType">业务类型</param>
        /// <param name="operationParameter">其他条件XML</param>
        /// <returns></returns>
        public BillListAllData GetBillListByList(
                                           BillSearchAuditorStatue auditorState,
                                           BillSearchWriteOffStatue writeOffStatue,
                                           BillSearchFeeWay feeWay,
                                           BillSearchInvoiceStatue invoceStatue,
                                           bool? isCommission,
                                           string companyIDs,
                                           string billNo,
                                           string customerName,
                                           string refNo,
                                           string invoiceNo,
                                           Guid? salesID,
                                           Guid? operateID,
                                           DateTime? billingStartDate,
                                           DateTime? billingEndDate,
                                           Decimal? amountMin,
                                           Decimal? amountMax,
                                           DataPageInfo dataPageInfo,
                                           OperationType operType,
                                           OperationParameter operationParameter,
                                           bool isEnglish)
        {
            try
            {
                string tempOperationParameter = string.Empty;

                if (operationParameter != null)
                {
                    System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();
                    System.Xml.XmlWriter w = doc.CreateWriter();
                    System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(operationParameter.GetType());
                    s.Serialize(w, operationParameter);
                    w.Flush();
                    w.Close();
                    tempOperationParameter = doc.ToString();
                }

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillListByBillList");

                db.AddInParameter(dbCommand, "@AuditorState", DbType.Int32, auditorState);
                db.AddInParameter(dbCommand, "@WriteOffStatue", DbType.Int32, writeOffStatue);
                db.AddInParameter(dbCommand, "@FeeWay", DbType.Int32, feeWay);
                db.AddInParameter(dbCommand, "@InvoceStatue", DbType.Int32, invoceStatue);
                db.AddInParameter(dbCommand, "@IsCommission", DbType.Boolean, isCommission);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs);
                db.AddInParameter(dbCommand, "@BillNo", DbType.String, billNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@RefNo", DbType.String, refNo);
                db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, invoiceNo);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@OperateID", DbType.Guid, operateID);
                db.AddInParameter(dbCommand, "@BillingStartDate", DbType.DateTime, billingStartDate);
                db.AddInParameter(dbCommand, "@BillingEndDate", DbType.DateTime, billingEndDate);
                db.AddInParameter(dbCommand, "@AmountMin", DbType.Decimal, amountMin);
                db.AddInParameter(dbCommand, "@AmountMax", DbType.Decimal, amountMax);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@OperType", DbType.Int32, operType);
                db.AddInParameter(dbCommand, "@FilterXml", DbType.Xml, tempOperationParameter);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CurrencyBillList> list = (from d in ds.Tables[0].AsEnumerable()
                                               select new CurrencyBillList
                                               {
                                                   ID = d.Field<Guid>("ID"),
                                                   BillNO = d.Field<String>("BillNo"),
                                                   BillType = (BillType)d.Field<Byte>("BillType"),
                                                   OperationNO = d.Field<String>("OperationNO"),
                                                   OperationID = d.Field<Guid>("OperationID"),
                                                   OperType = (OperationType)d.Field<Byte>("OperationType"),
                                                   CurrencyID = d.Field<Guid>("CurrencyID"),
                                                   CurrencyName = d.Field<String>("CurrencyName"),
                                                   CustomerID = d.Field<Guid>("CustomerID"),
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   CompanyID = d.Field<Guid>("CompanyID"),
                                                   CompanyName = d.Field<String>("CompanyName"),
                                                   Amount = d.Field<Decimal>("Amount"),
                                                   WriteOffAmount = d.Field<Decimal>("WriteOffAmount"),
                                                   BillRefNO = d.Field<String>("BillRefNO"),
                                                   CustomerRefNo = d.Field<String>("CustomerRefNo"),
                                                   CheckBy = d.Field<String>("CheckBy"),
                                                   CheckDate = d.Field<DateTime?>("CheckDate"),
                                                   Checked = d.Field<Boolean>("Checked"),
                                                   Paid = (PaidStatus)d.Field<Int32>("Paid"),
                                                   AccountDate = d.Field<DateTime?>("AccountDate"),
                                                   CreateBy = d.Field<Guid>("CreateBy"),
                                                   CreateByName = d.Field<String>("CreateByName"),
                                                   InvoiceNo = d.Field<String>("InvoiceNo"),
                                                   ExpressNo = d.Field<String>("ExpressNo"),
                                                   State = (BillState)d.Field<Byte>("State"),
                                                   Way = (FeeWay)d.Field<Byte>("Way"),
                                                   IsCommission = d.Field<Boolean>("IsCommission"),
                                                   UpdateDate = d.Field<DateTime?>("UpdateDate"),
                                                   PayCurrencyID = d.Field<Guid?>("PayCurrencyID"),
                                                   PayCurrencyName = d.Field<String>("PayCurrencyName"),
                                                   CreditDate = d.Field<int?>("CreditDate")
                                               }).ToList();

                dataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());


                List<BillListTotalInfo> totalInfoList = (from d in ds.Tables[2].AsEnumerable()
                                                         select new BillListTotalInfo
                                                         {
                                                             CurrencyID = d.Field<Guid>("CurrencyID"),
                                                             CurrencyName = d.Field<string>("CurrencyName"),
                                                             DRAmount = d.Field<Decimal>("DRAmount"),
                                                             CRAmount = d.Field<Decimal>("CRAmount"),
                                                             Balance = d.Field<Decimal>("Balance")
                                                         }).ToList();

                PageList pageList = PageList.Create<CurrencyBillList>(list, dataPageInfo);

                BillListAllData billListAllData = new BillListAllData();
                billListAllData.PageList = pageList;
                billListAllData.TotalInfoList = totalInfoList;
                return billListAllData;

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

        #region 获得账单列表GetBillListById    
        /// <summary>
        /// 获得账单列表
        /// </summary>
        /// <param name="id">业务ID</param>
        /// <returns></returns>
        public List<CurrencyBillList> GetBillListById(Guid id)
        {
            List<CurrencyBillList> result = new List<CurrencyBillList>();
            List<Guid> operationids = new List<Guid>();
            operationids.Add(id);
            result = GetBillListByOperID(null, operationids.ToArray(), LocalData.IsEnglish);
            if (result != null)
            {
                return result.FindAll(delegate(CurrencyBillList item) { return item.BillType == BillType.AR || item.BillType == BillType.AP; });
            }
            else return new List<CurrencyBillList>();
        }
        #endregion

        #region  获得账单列表
        /// <summary>
        /// 获得账单列表
        /// </summary>
        /// <param name="auditorState">审核状态(0:全部、1:已审核、2:未审核)</param>
        /// <param name="writeOffStatue">核销状态(0:全部、1:已核销、2:未核销)</param>
        /// <param name="feeWay">收付类型(0:全部、1:应收、2:应付)</param>
        /// <param name="invoceStatue">发票状态(0:全部、1:已开发票、2:未开发票)</param>
        /// <param name="isCommission">是否为业务管理成本</param>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="billNo">账单号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="refNo">参考号</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="salesID">业务员</param>
        /// <param name="operateID">操作员</param>
        /// <param name="billingStartDate">计费开始时间</param>
        /// <param name="billingEndDate">计费结束时间</param>
        /// <param name="amountMin">核销金额最小值</param>
        /// <param name="amountMax">核销金额最大值</param>
        /// <param name="maxRecords">返回最大行行数</param>
        /// <param name="operType">业务类型</param>
        /// <param name="currencyID">币种</param>
        /// <param name="operationParameter">其他条件XML</param>
        /// <returns></returns>
        public BillListAllData GetBillListByCurrency(
                                           BillSearchAuditorStatue auditorState,
                                           BillSearchWriteOffStatue writeOffStatue,
                                           BillSearchFeeWay feeWay,
                                           BillSearchInvoiceStatue invoceStatue,
                                           bool? isCommission,
                                           string companyIDs,
                                           string operationIDs,
                                           string billNo,
                                           string customerName,
                                           string refNo,
                                           string invoiceNo,
                                           Guid? salesID,
                                           Guid? operateID,
                                           DateTime? billingStartDate,
                                           DateTime? billingEndDate,
                                           Decimal? amountMin,
                                           Decimal? amountMax,
                                           DataPageInfo dataPageInfo,
                                           OperationType operType,
                                           Guid? currencyID,
                                           OperationParameter operationParameter,
                                           bool isEnglish)
        {
            try
            {
                string tempOperationParameter = string.Empty;

                if (operationParameter != null)
                {
                    System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();
                    System.Xml.XmlWriter w = doc.CreateWriter();
                    System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(operationParameter.GetType());
                    s.Serialize(w, operationParameter);
                    w.Flush();
                    w.Close();
                    tempOperationParameter = doc.ToString();
                }

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillListByBillList");

                db.AddInParameter(dbCommand, "@AuditorState", DbType.Int32, auditorState);
                db.AddInParameter(dbCommand, "@WriteOffStatue", DbType.Int32, writeOffStatue);
                db.AddInParameter(dbCommand, "@FeeWay", DbType.Int32, feeWay);
                db.AddInParameter(dbCommand, "@InvoceStatue", DbType.Int32, invoceStatue);
                db.AddInParameter(dbCommand, "@IsCommission", DbType.Boolean, isCommission);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs);
                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIDs);
                db.AddInParameter(dbCommand, "@BillNo", DbType.String, billNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@RefNo", DbType.String, refNo);
                db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, invoiceNo);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@OperateID", DbType.Guid, operateID);
                db.AddInParameter(dbCommand, "@BillingStartDate", DbType.DateTime, billingStartDate);
                db.AddInParameter(dbCommand, "@BillingEndDate", DbType.DateTime, billingEndDate);
                db.AddInParameter(dbCommand, "@AmountMin", DbType.Decimal, amountMin);
                db.AddInParameter(dbCommand, "@AmountMax", DbType.Decimal, amountMax);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@OperType", DbType.Int32, operType);
                db.AddInParameter(dbCommand, "@FilterXml", DbType.Xml, tempOperationParameter);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, currencyID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CurrencyBillList> list = (from d in ds.Tables[0].AsEnumerable()
                                               select new CurrencyBillList
                                               {
                                                   ID = d.Field<Guid>("ID"),
                                                   BillNO = d.Field<String>("BillNo"),
                                                   BillType = (BillType)d.Field<Byte>("BillType"),
                                                   OperationNO = d.Field<String>("OperationNO"),
                                                   OperationID = d.Field<Guid>("OperationID"),
                                                   OperType = (OperationType)d.Field<Byte>("OperationType"),
                                                   CurrencyID = d.Field<Guid>("CurrencyID"),
                                                   CurrencyName = d.Field<String>("CurrencyName"),
                                                   CustomerID = d.Field<Guid>("CustomerID"),
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   CustomerCName = d.Field<String>("CustomerCName"),
                                                   CustomerEName = d.Field<String>("CustomerEName"),
                                                   CustomerBankAccountNo = d.Field<String>("CustomerBankAccountNo"),
                                                   CustomerTaxNo = d.Field<String>("CustomerTaxIdNo"),
                                                   CustomerAddressTel = d.Field<String>("CustomerAddress") + " " + d.Field<String>("CustomerTel"),
                                                   CompanyID = d.Field<Guid>("CompanyID"),
                                                   CompanyName = d.Field<String>("CompanyName"),
                                                   Amount = d.Field<Decimal>("Amount"),
                                                   WriteOffAmount = d.Field<Decimal>("WriteOffAmount"),
                                                   BillRefNO = d.Field<String>("BillRefNO"),
                                                   CustomerRefNo = d.Field<String>("CustomerRefNo"),
                                                   CheckBy = d.Field<String>("CheckBy"),
                                                   CheckDate = d.Field<DateTime?>("CheckDate"),
                                                   Checked = d.Field<Boolean>("Checked"),
                                                   Paid = (PaidStatus)d.Field<Int32>("Paid"),
                                                   AccountDate = d.Field<DateTime?>("AccountDate"),
                                                   CreateBy = d.Field<Guid>("CreateBy"),
                                                   CreateByName = d.Field<String>("CreateByName"),
                                                   InvoiceNo = d.Field<String>("InvoiceNo"),
                                                   ExpressNo = d.Field<String>("ExpressNo"),
                                                   State = (BillState)d.Field<Byte>("State"),
                                                   Way = (FeeWay)d.Field<Byte>("Way"),
                                                   RBLD = d.Field<Boolean>("RBLD"),
                                                   IsCommission = d.Field<Boolean>("IsCommission"),
                                                   UpdateDate = d.Field<DateTime?>("UpdateDate"),
                                                   PayCurrencyID = d.Field<Guid?>("PayCurrencyID"),
                                                   PayCurrencyName = d.Field<String>("PayCurrencyName")
                                               }).ToList();

                dataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());


                List<BillListTotalInfo> totalInfoList = (from d in ds.Tables[2].AsEnumerable()
                                                         select new BillListTotalInfo
                                                         {
                                                             CurrencyID = d.Field<Guid>("CurrencyID"),
                                                             CurrencyName = d.Field<string>("CurrencyName"),
                                                             DRAmount = d.Field<Decimal>("DRAmount"),
                                                             CRAmount = d.Field<Decimal>("CRAmount"),
                                                             Balance = d.Field<Decimal>("Balance")
                                                         }).ToList();

                PageList pageList = PageList.Create<CurrencyBillList>(list, dataPageInfo);

                pageList.DataPageInfo = dataPageInfo;
                BillListAllData billListAllData = new BillListAllData();
                billListAllData.PageList = pageList;
                billListAllData.TotalInfoList = totalInfoList;
                return billListAllData;

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

        #region 根据账单ID获得账单的数据
        /// <summary>
        /// 根据账单ID集合获得账单的信息
        /// </summary>
        /// <param name="ids">账单ID集合</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<CurrencyBillList> GetBillListByIds(
                                            Guid[] ids,
                                            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillListByBillIDs");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CurrencyBillList> list = (from d in ds.Tables[0].AsEnumerable()
                                               select new CurrencyBillList
                                               {
                                                   ID = d.Field<Guid>("ID"),
                                                   BillNO = d.Field<String>("BillNo"),
                                                   BillType = (BillType)d.Field<Byte>("BillType"),
                                                   OperationNO = d.Field<String>("OperationNO"),
                                                   OperationID = d.Field<Guid>("OperationID"),
                                                   CurrencyID = d.Field<Guid>("CurrencyID"),
                                                   CurrencyName = d.Field<String>("CurrencyName"),
                                                   CustomerID = d.Field<Guid>("CustomerID"),
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   CompanyID = d.Field<Guid>("CompanyID"),
                                                   CompanyName = d.Field<String>("CompanyName"),
                                                   Amount = d.Field<Decimal>("Amount"),
                                                   WriteOffAmount = d.Field<Decimal>("WriteOffAmount"),
                                                   BillRefNO = d.Field<String>("BillRefNO"),
                                                   CheckBy = d.Field<String>("CheckBy"),
                                                   CheckDate = d.Field<DateTime?>("CheckDate"),
                                                   Checked = d.Field<Boolean>("Checked"),
                                                   Paid = (PaidStatus)d.Field<Int32>("Paid"),
                                                   AccountDate = d.Field<DateTime?>("AccountDate"),
                                                   CreateBy = d.Field<Guid>("CreateBy"),
                                                   CreateByName = d.Field<String>("CreateByName"),
                                                   InvoiceNo = d.Field<String>("InvoiceNo"),
                                                   State = (BillState)d.Field<Byte>("State"),
                                                   Way = (FeeWay)d.Field<Byte>("Way"),
                                                   BLNo = d.Field<String>("BLNo"),
                                                   IsCommission = d.Field<Boolean>("IsCommission"),
                                                   UpdateDate = d.Field<DateTime?>("UpdateDate"),
                                                   PayCurrencyID = d.Field<Guid?>("PayCurrencyID"),
                                                   PayCurrencyName = d.Field<String>("PayCurrencyName"),
                                                   BillOrCustomerRefNo = d.Field<String>("CustomerRefNo"),
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

        #region 审核/反审 业务单

        /// <summary>
        /// 审核业务单
        /// </summary>
        /// <param name="billIds">业务ID集合</param>
        /// <param name="isAuditor">是否审核(True为审核,False为取消审核)</param>
        /// <param name="changeByID">操作人</param>
        /// <param name="updateDates">最后更新时间集合</param>
        /// <returns></returns>
        public ManyResult AuditorBill(Guid[] billIds,
                                 bool isAuditor,
                                 Guid changeByID,
                                 DateTime?[] updateDates,
                                 bool isEnglish)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAuditorBill");

                string idList = billIds.Join();

                db.AddInParameter(dbCommand, "@BillIds", DbType.String, idList);
                db.AddInParameter(dbCommand, "@IsAuditor", DbType.Boolean, isAuditor);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, updateDates.Join());
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

        #region 根据账单号获得核销列表
        /// <summary>
        /// 根据帐单号获得核销列表
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<WriteOffItemList> GetWriteOffListByBill(
            Guid billID,
            Guid currencyID,
            FeeWay way,
            bool isCommission,
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetWriteOffListByBill");

                db.AddInParameter(dbCommand, "@BillID", DbType.Guid, billID);
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, currencyID);
                db.AddInParameter(dbCommand, "@Way", DbType.Byte, way);
                db.AddInParameter(dbCommand, "@IsCommission", DbType.Boolean, isCommission);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WriteOffItemList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new WriteOffItemList
                                                  {
                                                      No = b.Field<String>("No"),
                                                      WriteOffDate = b.Field<DateTime?>("WriteOffDate"),
                                                      CreatedByName = b.Field<String>("WriteOffName"),
                                                      Amount = b.Field<Decimal>("Amount"),
                                                      CurrencyID = b.Field<Guid>("CurrencyID"),
                                                      Currency = b.Field<String>("Currency"),
                                                      BankByName = b.Field<String>("PostName"),
                                                      ReachedDate = b.Field<DateTime?>("BankDate")
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

        #region 根据业务ID获得账单数据
        /// <summary>
        /// 获得业务ID获得账单数据
        /// </summary>
        /// <param name="billIds">账单ID集合</param>
        /// <returns></returns>
        public List<CurrencyBillList> GetBillListByOperID(Guid[] billIds, Guid[] operIds, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillListByOperID");

                string billIDList = string.Empty;
                string operationIDList = string.Empty;

                if (billIds != null && billIds.Count() > 0)
                {
                    billIDList = billIds.Join();
                }

                if (operIds != null && operIds.Count() > 0)
                {
                    operationIDList = operIds.Join();
                }

                if (string.IsNullOrEmpty(billIDList) && string.IsNullOrEmpty(operationIDList))
                {
                    return null;
                }

                db.AddInParameter(dbCommand, "@BillIDs", DbType.String, billIDList);
                db.AddInParameter(dbCommand, "@OperationIDS", DbType.String, operationIDList);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CurrencyBillList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new CurrencyBillList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      BillNO = b.Field<String>("BillNO"),
                                                      BLNo = b.Field<String>("BLNo"),
                                                      OperType = (OperationType)b.Field<Byte>("OperType"),
                                                      BillType = (BillType)b.Field<Byte>("BillType"),
                                                      Way = (FeeWay)b.Field<Byte>("Way"),
                                                      State = (BillState)b.Field<Byte>("State"),
                                                      OperationID = b.Field<Guid>("OperationID"),
                                                      OperationNO = b.Field<String>("OperationNO"),
                                                      CurrencyID = b.Field<Guid>("CurrencyID"),
                                                      CustomerID = b.Field<Guid>("CustomerID"),
                                                      CustomerName = b.Field<String>("CustomerName"),
                                                      CurrencyName = b.Field<String>("CurrencyName"),
                                                      Amount = b.Field<Decimal>("Amount"),
                                                      WriteOffAmount = b.Field<Decimal>("WriteOffAmount"),
                                                      BillRefNO = b.Field<String>("BillRefNO"),
                                                      IsCommission = b.Field<Boolean>("IsCommission"),
                                                      Paid = (PaidStatus)b.Field<Int32>("Paid"),
                                                      Rate = b.Field<Decimal>("Rate"),
                                                      PayTypeName = b.Field<String>("Payment"),
                                                      CompanyID = b.Field<Guid>("CompanyID"),
                                                      InvoiceNo = b.Field<String>("InvoiceNo"),
                                                      CheckDate = b.Field<DateTime?>("CheckDate"),
                                                      CheckBy = b.Field<String>("CheckByName"),
                                                      AccountDate = b.Field<DateTime?>("AccountDate"),
                                                      CreateByName = b.Field<String>("CreateByName")
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

        #region 转换退佣数据
        /// <summary>
        /// 获得退佣数据
        /// </summary>
        /// <param name="billID">账单ID集合</param>
        /// <param name="customersID">客户ID</param>
        /// <param name="operateRate">手续费比例</param>
        /// <returns></returns>
        public ComissionData GetComissionDataByCodition(List<Guid> operationIDList, Guid customersID, decimal operateRate, bool isEnglish)
        {
            List<CurrencyBillList> dataList = this.GetBillListByOperID(null, operationIDList.ToArray(), isEnglish);

            ComissionData data = new ComissionData();

            if (dataList == null)
            {
                dataList = new List<CurrencyBillList>();
            }

            // 封装帐单号
            List<string> billNos = (from c in dataList
                                    orderby c.BillNO
                                    select c.BillNO).Distinct().ToList();
            data.DcNoteNos = ToStr(billNos);
            if (data.DcNoteNos == null)
            {
                data.DcNoteNos = string.Empty;
            }

            // 封装业务号
            List<string> opnos = (from c in dataList
                                  orderby c.OperationNO
                                  select c.OperationNO).Distinct().ToList();
            data.OperationNos = ToStr(opnos);
            if (data.OperationNos == null)
            {
                data.OperationNos = string.Empty;
            }

            // 封装提单号
            List<string> blnos = (from c in dataList
                                  orderby c.BLNo
                                  select c.BLNo).Distinct().ToList();
            data.BlNos = ToStr(blnos);
            if (data.BlNos == null)
            {
                data.BlNos = string.Empty;
            }

            // 统计应付的币种金额列表
            var creditAmt = from c in dataList
                            where c.IsCommission == false && c.Way == FeeWay.AP
                            group c by c.CurrencyName into g
                            select new
                            {
                                Currency = g.Key,
                                Amount = g.Sum(f => f.Amount)
                            };
            foreach (var a in creditAmt)
            {
                if (string.IsNullOrEmpty(data.Credit))
                {
                    data.Credit = a.Currency + ":" + a.Amount.ToString("F2");
                    continue;
                }

                data.Credit = data.Credit + "," + a.Currency + ":" + a.Amount.ToString("F2");
            }

            // 统计应收的币种金额列表
            var dedbitAmt = from c in dataList
                            where c.IsCommission == false && c.Way == FeeWay.AR
                            group c by c.CurrencyName into g
                            select new
                            {
                                Currency = g.Key,
                                Amount = g.Sum(f => f.Amount)
                            };
            foreach (var a in dedbitAmt)
            {
                if (string.IsNullOrEmpty(data.Debit))
                {
                    data.Debit = a.Currency + ":" + a.Amount.ToString("F2");
                    continue;
                }

                data.Debit = data.Debit + "," + a.Currency + ":" + a.Amount.ToString("F2");
            }

            //统计利润
            foreach (var da in dataList)
            {
                da.Amount = da.Way == FeeWay.AR ? da.Amount : -da.Amount;
            }

            var profitAmt = from c in dataList
                            group c by c.CurrencyName into g
                            select new
                            {
                                Currency = g.Key,
                                Amount = g.Sum(f => f.Amount)
                                //Amount = g.Sum(f => f.Way == FeeWay.AR ? f.Amount : -(f.Amount))
                            };

            foreach (var a in profitAmt)
            {
                if (string.IsNullOrEmpty(data.Profit))
                {
                    data.Profit = a.Currency + ":" + a.Amount.ToString("F2");
                    continue;
                }

                data.Profit = data.Profit + "," + a.Currency + ":" + a.Amount.ToString("F2");
            }
            ////var profitAmt = (from d in dataList group d by d.CurrencyName into g select new { Currency = g.Key, Amount = g.Sum(p => p.Way == FeeWay.AR ? p.Amount : -p.Amount) }).ToDictionary(c => c.Currency, c => c.Amount);
            ////foreach (KeyValuePair<string, Decimal> a in profitAmt)
            ////{
            ////    if (string.IsNullOrEmpty(data.Profit))
            ////    {
            ////        data.Profit = a.Key + ":" + a.Value.ToString("F2");
            ////        continue;
            ////    }

            ////    data.Profit = data.Profit + "," + a.Key + ":" + a.Value.ToString("F2");
            ////}


            if (data.Profit == null)
            {
                data.Profit = string.Empty;
            }

            //统计佣金的币种金额列表
            var commissionAmts = from c in dataList
                                 where c.IsCommission == true && c.CustomerID == customersID
                                 group c by c.CurrencyName into g
                                 select new
                                 {
                                     Currency = g.Key,
                                     Amount = g.Sum(f => f.Amount)
                                 };

            foreach (var a in commissionAmts)
            {
                if (string.IsNullOrEmpty(data.StrcommissionAmount))
                {
                    data.StrcommissionAmount = a.Currency + ":" + a.Amount.ToString("F2");
                    continue;
                }

                data.StrcommissionAmount = data.StrcommissionAmount + "," + a.Currency + ":" + a.Amount.ToString("F2");
            }

            var commissionttlAmt = (from c in dataList
                                    where c.IsCommission == true && c.CustomerID == customersID && c.Way == FeeWay.AP
                                    select c.Amount * c.Rate).Sum();


            data.CommissionAmount = System.Math.Abs(commissionttlAmt) * (1.0m - operateRate);

            data.Remark = "Proportion:" + operateRate.ToString("F2");

            if (data.Credit == null)
            {
                data.Credit = string.Empty;
            }
            if (data.Debit == null)
            {
                data.Debit = string.Empty;
            }

            int p = (from d in dataList where d.Way == FeeWay.AR && !d.IsPaid select d).Count();
            if (p > 0)
            {
                data.IsPaid = false;
            }
            else
            {
                data.IsPaid = true;
            }

            data.PaymentType = dataList[0].PayType;
            data.CompanyID = dataList[0].CompanyID;

            return data;
        }
        /// <summary>
        /// 转换退佣数据
        /// </summary>
        /// <param name="operationIDList"></param>
        /// <param name="customersID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ComissionData GetComissionDataByWF(List<Guid> operationIDList, List<Guid> currencyIDList, Guid customersID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("fam.uspGetComissionData");

                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIDList.ToArray().Join());
                db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, currencyIDList.ToArray().Join());
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customersID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ComissionData result = (from b in ds.Tables[0].AsEnumerable()
                                        select new ComissionData
                                        {
                                            OperationNos = b.Field<String>("OperationNos"),
                                            BlNos = b.Field<String>("BLNos"),
                                            PaymentType = (short)b.Field<int>("PaymentType"),
                                            Credit = "USD:" + b.Field<Decimal>("DRAmount").ToString(),
                                            Debit = "USD:" + b.Field<Decimal>("CRAmount").ToString(),
                                            Profit = "USD:" + b.Field<Decimal>("ProfitAmount").ToString(),
                                            CurrencyName = b.Field<String>("CurrencyName"),
                                            CommissionAmount = b.Field<Decimal>("ComissionAmount"),
                                            Remark = b.Field<String>("Remark"),
                                            IsPaid = b.Field<Boolean>("IsPaid"),
                                            CompanyID = b.Field<Guid>("CompanyID"),
                                            Goods = b.Field<String>("Goods"),
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
        /// <summary>
        /// 获得字符串
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private string ToStr(List<string> items)
        {
            string rlt = string.Empty;

            foreach (string i in items)
            {
                if (string.IsNullOrEmpty(rlt))
                {
                    rlt = i;
                    continue;
                }

                rlt = rlt + "," + i;
            }
            if (rlt == null)
            {
                rlt = string.Empty;
            }
            return rlt;
        }
        #endregion

        #region 获得币种账单的费用明细列表
        public List<ChargeList> GetChargeList(
                 Guid billID,
                 Guid currenyID,
                 FeeWay feeWay,
                 bool isCommission)
        {

            List<ChargeList> chargeList = this.GetChargeList(new Guid[1] { billID });

            List<ChargeList> list = (from c in chargeList where c.CurrencyID == currenyID && c.Way == feeWay && c.IsCommission == isCommission select c).ToList();
            return list;


        }
        #endregion

        #region 获得业务信息(开发票时使用)
        public BusinessByInvoice GetBusinessByInvoice(Guid[] operationIDs, Guid[] billIDs, bool isisEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBusinessByBillInvoice");

                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIDs.Join());
                db.AddInParameter(dbCommand, "@BillIDs", DbType.String, billIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isisEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BusinessByInvoice> list = (from b in ds.Tables[0].AsEnumerable()
                                                select new BusinessByInvoice
                                                {
                                                    OperationID = b.Field<Guid>("OperationID"),
                                                    OperationNo = b.Field<String>("OperationNo"),
                                                    Opertype = (OperationType)b.Field<Byte>("OperationType"),
                                                    CustomerID = b.Field<Guid>("CustomerID"),
                                                    CustomerCName = b.Field<String>("CustomerCName"),
                                                    CustomerEName = b.Field<String>("CustomerEName"),
                                                    CustomerAddressTel = b.Field<String>("CustomerAddress") + " " + b.Field<String>("CustomerTel"),
                                                    CustomerBankAccountNo = b.Field<String>("CustomerBankAccountNo"),
                                                    CustomerTaxIDNo = b.Field<String>("CustomerTaxIdNo"),
                                                    ETA = b.Field<DateTime?>("ETA"),
                                                    ETD = b.Field<DateTime?>("ETD"),
                                                    CompanyID = b.Field<Guid>("CompanyID"),
                                                    CompanyName = b.Field<String>("CompanyName"),
                                                    POLName = b.Field<String>("POLName"),
                                                    PODName = b.Field<String>("PODName"),
                                                    CtnNo = b.Field<String>("CtnNo"),
                                                    CtnTypeName = b.Field<String>("CtnTypeName"),
                                                    SoNo = b.Field<String>("SoNo"),
                                                    PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                                    BLNo = b.Field<String>("BLNo"),
                                                    Vessel = b.Field<String>("VesselName"),
                                                    Voyage = b.Field<String>("VesselVoyage")
                                                }).ToList();

                BusinessByInvoice result = new BusinessByInvoice();
                int i = 0;
                foreach (BusinessByInvoice item in list)
                {
                    if (i == 0)
                    {
                        result.OperationID = item.OperationID;
                        result.OperationNo = item.OperationNo;
                        result.Opertype = item.Opertype;
                        result.CustomerID = item.CustomerID;
                        result.CustomerCName = item.CustomerCName;
                        result.CustomerEName = item.CustomerEName;
                        result.ETA = item.ETA;
                        result.ETD = item.ETD;
                        result.CompanyID = item.CompanyID;
                        result.CompanyName = item.CompanyName;

                        result.POLName = item.POLName;
                        result.PODName = item.PODName;
                        result.PlaceOfDeliveryName = item.PlaceOfDeliveryName;
                        result.CtnNo = item.CtnNo;
                        result.CtnTypeName = item.CtnTypeName;
                        result.SoNo = item.SoNo;
                        result.BLNo = item.BLNo;
                        result.Vessel = item.Vessel;
                        result.VesselVoyage = item.Voyage;
                        result.Voyage = item.Voyage;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(result.POLName))
                        {
                            result.POLName = item.POLName;
                        }
                        if (string.IsNullOrEmpty(result.PODName))
                        {
                            result.PODName = item.PODName;
                        }
                        if (string.IsNullOrEmpty(result.PlaceOfDeliveryName))
                        {
                            result.PlaceOfDeliveryName = item.PlaceOfDeliveryName;
                        }
                        if (string.IsNullOrEmpty(result.CtnNo))
                        {
                            result.CtnNo = item.CtnNo;
                        }
                        if (string.IsNullOrEmpty(result.CtnTypeName))
                        {
                            result.CtnTypeName = item.CtnTypeName;
                        }
                        if (string.IsNullOrEmpty(result.SoNo))
                        {
                            result.SoNo = item.SoNo;
                        }
                        if (string.IsNullOrEmpty(result.BLNo))
                        {
                            result.BLNo = item.BLNo;
                        }

                        if (!string.IsNullOrEmpty(result.POLName) && !string.IsNullOrEmpty(item.POLName) && !result.POLName.Contains(item.POLName))
                        {
                            result.POLName = result.POLName + "," + item.POLName;
                        }
                        if (!string.IsNullOrEmpty(result.PODName) && !string.IsNullOrEmpty(result.PODName) && !result.PODName.Contains(item.PODName))
                        {
                            result.PODName = result.PODName + "," + item.PODName;
                        }
                        if (!string.IsNullOrEmpty(result.PlaceOfDeliveryName) && !string.IsNullOrEmpty(result.PlaceOfDeliveryName) && !result.PlaceOfDeliveryName.Contains(item.PlaceOfDeliveryName))
                        {
                            result.PlaceOfDeliveryName = result.PlaceOfDeliveryName + " " + item.PlaceOfDeliveryName;
                        }
                        if (!string.IsNullOrEmpty(result.CtnNo) && !string.IsNullOrEmpty(result.CtnNo) && !result.CtnNo.Contains(item.CtnNo))
                        {
                            result.CtnNo = result.CtnNo + "," + item.CtnNo;
                        }
                        if (!string.IsNullOrEmpty(result.CtnTypeName) && !string.IsNullOrEmpty(result.CtnTypeName) && !result.CtnTypeName.Contains(item.CtnTypeName))
                        {
                            result.CtnTypeName = result.CtnTypeName + "," + item.CtnTypeName;
                        }
                        if (!string.IsNullOrEmpty(result.SoNo) && !string.IsNullOrEmpty(result.SoNo) && !result.SoNo.Contains(item.SoNo))
                        {
                            result.SoNo = result.SoNo + "," + item.SoNo;
                        }
                        if (!string.IsNullOrEmpty(result.BLNo) && !string.IsNullOrEmpty(result.BLNo) && !result.BLNo.Contains(item.BLNo))
                        {
                            result.BLNo = result.BLNo + "," + item.BLNo;
                        }

                    }

                    i++;
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

        #region 获得币种账单的发票列表
        /// <summary>
        /// 获得币种账单的发票列表 
        /// </summary>
        /// <param name="billID">账单ID</param>
        /// <param name="currenyID"></param>
        /// <param name="feeWay"></param>
        /// <param name="isCommission"></param>
        /// <returns></returns>
        public List<InvoiceList> GetInvoiceList(
                 Guid billID,
                 Guid currenyID,
                 FeeWay feeWay,
                 bool isCommission)
        {
            List<ChargeList> chargeList = GetChargeList(billID, currenyID, feeWay, isCommission);

            List<Guid> idList = (from d in chargeList group d by d.ID into g select g.Key).ToList();

            List<InvoiceList> list = GetInvoiceListByFeeID(idList.ToArray());

            list = (from d in list where d.IsValid select d).ToList();

            return list;
        }
        #endregion

        #region 更新账单的客户参考号
        /// <summary>
        /// 更新账单的客户参考号
        /// </summary>
        /// <param name="id">账单ID</param>
        /// <param name="customerRefNo">客户参考号</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="saveById">更新人</param>
        /// <returns></returns>
        public SingleResultData SaveBillCustomerRefNo(
                            Guid id,
                            string customerRefNo,
                            DateTime? updateDate,
                            Guid saveById)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBillCustomerRefNo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, customerRefNo);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData results = db.SingleResult(dbCommand);

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

        #region  得到代理账单是否修改  joe 2013-08-20

        /// <summary>
        /// 得到代理账单是否修改
        /// </summary>
        /// <param name="operationID">海出业务号</param>
        /// <returns></returns>
        public bool GetAgentBillState(Guid operationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT top 1  [ReRvs] FROM [fcm].[OceanTrackings]  ot INNER JOIN fcm.V_OEJobID2OIJobID vw ON ot.OceanBookingID=vw.OceanBookingID WHERE vw.OIBookingID=@OperateID");

                db.AddInParameter(dbCommand, "@OperateID", DbType.Guid, operationID);

                object results = db.ExecuteScalar(dbCommand);
                if (results == null) return false;
                bool res = false;
                bool.TryParse(results.ToString(), out res);
                return res;
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

        #region 获取口岸下对应客户最近一次应收账单需要要开增值税发票的税率
        /// <summary>
        /// 获取口岸下对应客户最近一次应收账单需要要开增值税发票的税率
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public Decimal GetAPBillTariff(Guid companyID, Guid customerID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAPBillTariff");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                object results = db.ExecuteScalar(dbCommand);

                if (results == null)
                {
                    return Decimal.Zero;
                }

                return (Decimal)results;
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

        #region 关帐之前判断是否有业务没有签收
        /// <summary>
        /// 关帐之前判断是否有业务没有签收
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="AccountingClosingDate"></param>
        /// <returns></returns>
        public bool IsAcceptForAccountingClosing(Guid CompanyId, DateTime AccountingClosingDate) 
        {
            try
            {
              
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspIsAcceptForAccountingClosing");
                db.AddInParameter(dbCommand, "@CompanyId", DbType.Guid, CompanyId);
                db.AddInParameter(dbCommand, "@AccountingClosingDate", DbType.DateTime, CompanyId);

                db.AddParameter(dbCommand, "@Return", DbType.Boolean, ParameterDirection.ReturnValue, "Return", DataRowVersion.Default, null);
                db.ExecuteNonQuery(dbCommand);
                object result = db.GetParameterValue(dbCommand, "@Return");

                return ((int)result) > 0 ? true : false;


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

        #region 生成交货地手续费
        /// <summary>
        /// 生成交货地手续费
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="saveById">保存人</param>
        /// <returns></returns>
        public SingleResult SaveRegenerateDeliveryHandlingFee(Guid operationID, Guid saveById)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspSaveBillInfoForBLTransportClause]");

                dbCommand.CommandTimeout = 60;

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);


                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count == 0)
                {
                    return null;
                }

                SingleResult result = new SingleResult();
                result.Add("State", true);
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

        #region 应收账单转代理
        /// <summary>
        /// 应收账单转代理
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        public SingleResult ConvertBillFromARToDN(SaveRequestBillConvert saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspSaveBillInfoForConvertARToDN]");

                db.AddInParameter(dbCommand, "@BillID", DbType.Guid, saveRequest.BillID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.String, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
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
    }
}

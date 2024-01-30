namespace ICP.FAM.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.FAM.ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.FAM.ServiceInterface.DataObjects.Report;
    using ICP.Common.ServiceInterface.DataObjects;


    public partial class FinanceService
    {
        #region getList

        /// <summary>
        /// 获得发票列表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerName">客户</param>
        /// <param name="titleCName">中文抬头</param>
        /// <param name="titleEName">英文抬关</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大返回行数</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="billNo">帐单号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="expressNo">快递号</param>
        /// <param name="remark">备注</param>
        /// <param name="amountMin">最小金额</param>
        /// <param name="amountMax">最大金额</param>
        /// <param name="invoiceBeginTime">开票开始日期</param>
        /// <param name="invoiceEndTime">开票结束日期</param>
        /// <param name="etdBeginTime">ETD开始日期</param>
        /// <param name="etdEndTime">ETD结束日期</param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns>InvoiceData</returns>
        public PageList GetInvoiceListByList(
                                        Guid[] companyIDs,
                                        string customerName,
                                        string titleCName,
                                        string titleEName,
                                        bool? isValid,
                                        string invoiceNo,
                                        string operationNo,
                                        string billNo,
                                        string blNo,
                                        string ctnNo,
                                        string expressNo,
                                        string remark,
                                        decimal? amountMin,
                                        decimal? amountMax,
                                        DateTime? invoiceBeginTime,
                                        DateTime? invoiceEndTime,
                                        DateTime? etdBeginTime,
                                        DateTime? etdEndTime,
                                        DataPageInfo dataPageInfo)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceList");
                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@customerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@titleCName", DbType.String, titleCName);
                db.AddInParameter(dbCommand, "@titleEName", DbType.String, titleEName);
                db.AddInParameter(dbCommand, "@isValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@invoiceNo", DbType.String, invoiceNo);
                db.AddInParameter(dbCommand, "@operationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@billNo", DbType.String, billNo);
                db.AddInParameter(dbCommand, "@blNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ctnNo", DbType.String, ctnNo);
                db.AddInParameter(dbCommand, "@expressNo", DbType.String, expressNo);
                db.AddInParameter(dbCommand, "@remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@AmountMin", DbType.Decimal, amountMin);
                db.AddInParameter(dbCommand, "@AmountMax", DbType.Decimal, amountMax);
                db.AddInParameter(dbCommand, "@invoiceBeginTime", DbType.DateTime, invoiceBeginTime);
                db.AddInParameter(dbCommand, "@invoiceEndTime", DbType.DateTime, invoiceEndTime);
                db.AddInParameter(dbCommand, "@etdBeginTime", DbType.DateTime, etdBeginTime);
                db.AddInParameter(dbCommand, "@etdEndTime", DbType.DateTime, etdEndTime);

                db.AddInParameter(dbCommand, "@pageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@currentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<InvoiceList> results = BulidInvoiceListByDataSet(ds);

                dataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());

                PageList list = PageList.Create<InvoiceList>(results, dataPageInfo);
                return list;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <param name="ids">Ids</param>
        /// <returns>InvoiceList</returns>
        public PageList GetInvoiceListByIds(Guid[] ids, DataPageInfo dataPageInfo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceListByIDs");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<InvoiceList> results = BulidInvoiceListByDataSet(ds);
                PageList list = PageList.Create<InvoiceList>(results, dataPageInfo);
                return list;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        private List<InvoiceList> BulidInvoiceListByDataSet(DataSet ds)
        {

            List<InvoiceList> results = (from b in ds.Tables[0].AsEnumerable()
                                         select new InvoiceList
                                          {
                                              ID = b.Column<Guid>("ID"),
                                              No = b.Column<String>("No"),
                                              InvoiceNo = b.Column<String>("InvoiceNo"),
                                              InvoiceDate = b.Column<DateTime>("InvoiceDate"),
                                              CustomerName = b.Column<String>("CustomerName"),
                                              InvoiceTitle = b.Column<String>("InvoiceTitle"),
                                              Amounts = b.Column<String>("Amounts"),
                                              BillNo = b.Column<String>("BillNo"),
                                              CreateByName = b.Column<String>("CreateByName"),
                                              CreateByID = b.Column<Guid>("CreateByID"),
                                              CreateDate = b.Column<DateTime>("CreateDate"),
                                              IsValid = b.Column<bool>("IsValid"),
                                              UpdateDate = b.Column<DateTime?>("UpdateDate"),
                                              BLNo = b.Column<String>("BLNo"),
                                              ETD = b.Column<DateTime?>("ETD"),
                                              ExpressNo = b.Column<String>("ExpressNo"),
                                              ExpressDate = b.Column<DateTime?>("ExpressDate"),
                                              Remark = b.Column<String>("Remark"),
                                              Selected = false,
                                              IsDirty = false,
                                          }).ToList();
            return results;
        }

        private List<ShortInvoiceInfo> BulidShortInvoiceListByDataSet(DataSet ds)
        {

            List<ShortInvoiceInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new ShortInvoiceInfo
                                         {

                                             InvoiceNo = b.Column<String>("InvoiceNo"),
                                             Amount = b.Column<String>("Amount"),
                                         }).ToList();
            return results;
        }

        /// <summary>
        /// 获取某公司一定时间内发票信息
        /// </summary>
        /// <param name="Company">公司</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">Ids</param>
        /// <returns>InvoiceList</returns>
        public List<ShortInvoiceInfo> GetCompanyInvoiceListByDate(Guid Company, DateTime start, DateTime end)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCompanyInvoiceListByDate");

                db.AddInParameter(dbCommand, "@Company", DbType.Guid, Company);
                db.AddInParameter(dbCommand, "@Start", DbType.DateTime, start);
                db.AddInParameter(dbCommand, "@End", DbType.DateTime, start);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<ShortInvoiceInfo> results = BulidShortInvoiceListByDataSet(ds);
                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region

        /// <summary>
        /// 获取发票信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>InvoiceInfo</returns>
        public InvoiceInfo GetInvoiceInfo(Guid id, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 4)
                {
                    return null;
                }

                InvoiceInfo result = (from b in ds.Tables[0].AsEnumerable()
                                      select new InvoiceInfo
                                      {
                                          CompanyID = b.Field<Guid>("CompanyID"),
                                          InvoiceNo = b.Field<string>("InvoiceNo"),
                                          InvoiceDate = b.Field<DateTime>("InvoiceDate"),
                                          ExpressNo = b.Field<string>("ExpressNo"),
                                          ExpressDate = b.Field<DateTime?>("ExpressDate"),
                                          Bank1ID = b.IsNull("Bank1Id") ? Guid.Empty : b.Field<Guid>("Bank1Id"),
                                          Bank2ID = b.Field<Guid?>("Bank2Id"),
                                          Bank1Name = b.Field<string>("Bank1Name"),
                                          Bank2Name = b.Field<string>("Bank2Name"),
                                          BLNo = b.Field<string>("BLNO"),
                                          SONo = b.Field<string>("SONo"),
                                          CtnTypeName = b.Field<string>("CtnTypeName"),
                                          ID = b.Field<Guid>("ID"),
                                          Tax = b.Field<Decimal>("Tax"),
                                          CustomerName = b.Field<string>("CustomerName"),
                                          CompanyName = b.Field<String>("CompanyName"),
                                          POLName = b.Field<String>("POLName"),
                                          PODName = b.Field<string>("PODName"),
                                          PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                          ETD = b.Field<DateTime?>("ETD"),
                                          Vessel = b.Field<string>("VesselName"),
                                          Voyage = b.Field<string>("VoyageNo"),
                                          ContainerNo = b.Field<string>("ContainerNo"),
                                          CustomerID = b.Field<Guid?>("CustomerID"),
                                          TitleCName = b.Field<string>("TitleCName"),
                                          TitleEName = b.Field<string>("TitleEName"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                          IsValid = b.Field<Boolean>("IsValid"),
                                          Remark = b.Field<String>("Remark"),
                                          BankAccountNo = b.Field<string>("AccountNo"),//银行账号,发票打印用
                                          BusinessNo = b.Field<string>("BusinessNo"),//银行账号,发票打印用
                                          TaxNo = b.Field<string>("TaxNo"),     //银行账号,发票打印用
                                          CustomerAddressTel = b.Field<String>("CustomerAddressTel"),
                                          CustomerBankAccountNo = b.Field<String>("CustomerBankAccountNo"),
                                          No = b.Field<String>("No"),
                                          CustomerTaxIDNo = b.Field<String>("CustomerTaxNo"),
                                          ReceivablesName = b.Field<String>("ReceivablesName"),
                                          ReviewName = b.Field<String>("ReviewName"),
                                          InvoiceType = (CustomerInvoiceType)b.Field<Byte>("InvoiceType"),
                                          BillList = (from c in ds.Tables[1].AsEnumerable()
                                                      select new BillList
                                                      {

                                                          ID = c.Field<Guid>("ID"),
                                                          No = c.Field<string>("BillNO"),
                                                          CustomerName = c.Field<string>("CustomerName"),
                                                          Fees = (from d in ds.Tables[2].AsEnumerable()
                                                                  where d.Field<Guid>("BillID") == c.Field<Guid>("ID")
                                                                  select new ChargeList
                                                                  {
                                                                      ID = d.Field<Guid>("ID"),
                                                                      BillID = d.Field<Guid>("BillID"),
                                                                      ChargingCode = d.Field<string>("ChargingCode"),
                                                                      CurrencyName = d.Field<string>("CurrencyName"),
                                                                      Amount = d.Field<decimal>("Amount"),
                                                                      Remark = d.Field<string>("Remark"),

                                                                  }).ToList()

                                                      }).ToList(),

                                          Fees = (from e in ds.Tables[3].AsEnumerable()
                                                  select new InvoiceFeeDate
                                                  {
                                                      BillFeeId = e.Field<Guid?>("BillFeeId"),
                                                      ID = e.Field<Guid>("ID"),
                                                      ChargingCode = e.Field<string>("ChargingCodeName"),
                                                      ChargingCodeID = e.Field<Guid>("ChargingCodeID"),//费用项目ID
                                                      CurrencyID = e.Field<Guid>("CurrencyID"),
                                                      CurrencyName = e.Field<string>("CurrencyName"),
                                                      Rate = e.Field<decimal>("Rate"),
                                                      Quantity = e.Field<decimal>("Quantity"),
                                                      Amount = e.Field<decimal>("Amount"),
                                                      Remark = e.Field<string>("Remark"),
                                                      UpdateDate = e.Field<DateTime?>("UpdateDate"),
                                                      CreateDate = e.Field<DateTime>("DetailCreateDate"),
                                                  }).ToList()

                                      }).SingleOrDefault();

                #region 保留账单费用之选择状态
                List<Guid> addIds = new List<Guid>();
                foreach (var item in result.Fees)
                {
                    if (item.BillFeeId != null)
                    {
                        addIds.Add(item.BillFeeId.Value);
                    }
                }

                foreach (var bill in result.BillList)
                {
                    foreach (var item in bill.Fees)
                    {
                        if (addIds.Contains(item.ID)) item.Selected = true;
                    }
                }
                #endregion

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

        #region Save


        /// <summary>
        /// 保存发票
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="bank1Id">银行1</param>
        /// <param name="bank2Id">银行2</param>
        /// <param name="tax">税金</param>
        /// <param name="customerId">客户</param>
        /// <param name="titleCName">中文抬头</param>
        /// <param name="titleEName">英文抬头</param>
        /// <param name="soNo">订舱号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="containerTypeId">箱型</param>
        /// <param name="blNo">提单号</param>
        /// <param name="etd">离港日</param>
        /// <param name="polId">装货港</param>
        /// <param name="podId">卸货港</param>
        /// <param name="placeOfDeliveryId">交货地</param>
        /// <param name="voyageId">船名航次</param>
        /// <param name="remark">备注</param>
        /// <param name="updateDate">更新日期</param>
        /// <param name="billFeeIds">关联的费用IDs</param>
        /// <param name="feeIds">费用IDs</param>
        /// <param name="feeCurrencyIds">费用币种</param>
        /// <param name="feeChargingCodeIds">费用-费用代码</param>
        /// <param name="feeRates">费用汇率</param>
        /// <param name="feeQuantities">费用数量</param>
        /// <param name="feeAmounts">费用金额</param>
        /// <param name="feeRemarks">费用备注</param>
        /// <param name="feeUpdateDates">费用更新日期</param>
        /// <param name="changeByID">保存人</param>
        /// <param name="isEnglish">是否英文环境</param>
        /// <returns>HierarchyManyResult 表0"ID","Upadate","invoiceNo" 表1 "ID","Upadate"</returns>
        public HierarchyManyResult SaveInvoiceInfo(
            Guid id,
            string invoiceNo,
            DateTime invoiceDate,
            string expressNo,
            DateTime? expressDate,
            Guid companyId,
            Guid bank1Id,
            Guid? bank2Id,
            decimal tax,
            Guid? customerId,
            string titleCName,
            string titleEName,
            Guid? taxCustomerID,
            Guid? taxCompanyID,
            string customerAddressTel,
            string customerTaxNo,
            string customerAccountNo,
            string receivablesName,
            string reviewName,
            CustomerInvoiceType invoiceType,
            string soNo,
            string containerNo,
            string containerTypeId,
            string blNo,
            DateTime? etd,
            string polId,
            string podId,
            string placeOfDeliveryId,
            string voyageId,
            string VoyageNo,
            string remark,
            DateTime? updateDate,
            Guid[] billFeeIds,
            Guid[] feeIds,
            Guid[] feeCurrencyIds,
            Guid[] feeChargingCodeIds,
            decimal[] feeRates,
            decimal[] feeQuantities,
            decimal[] feeAmounts,
            string[] feeRemarks,
            DateTime?[] feeUpdateDates,
            Guid changeByID,
            bool isEnglish)
        {
            try
            {
                ICP.FAM.ServiceInterface.DataObjects.InvoiceInfo info = new InvoiceInfo();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveInvoiceInfo");

                #region 传递参数

                db.AddInParameter(dbCommand, "@id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@TaxCompanyID", DbType.Guid, taxCompanyID);
                db.AddInParameter(dbCommand, "@TaxCustomerID", DbType.Guid, taxCustomerID);
                db.AddInParameter(dbCommand, "@CustomerAddressTel", DbType.String, customerAddressTel);
                db.AddInParameter(dbCommand, "@CustomerTaxNo", DbType.String, customerTaxNo);
                db.AddInParameter(dbCommand, "@BankAccountNo", DbType.String, customerAccountNo);
                db.AddInParameter(dbCommand, "@ReceivablesName", DbType.String, receivablesName);
                db.AddInParameter(dbCommand, "@ReviewName", DbType.String, reviewName);
                db.AddInParameter(dbCommand, "@InvoiceType", DbType.Byte, invoiceType);
                db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, invoiceNo);
                db.AddInParameter(dbCommand, "@InvoiceDate", DbType.DateTime, invoiceDate);
                db.AddInParameter(dbCommand, "@ExpressNo", DbType.String, expressNo);
                db.AddInParameter(dbCommand, "@ExpressDate", DbType.DateTime, expressDate);
                db.AddInParameter(dbCommand, "@CompanyId", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@Account1Id", DbType.Guid, bank1Id);
                db.AddInParameter(dbCommand, "@Account2Id", DbType.Guid, bank2Id);
                db.AddInParameter(dbCommand, "@TaxRate", DbType.Decimal, tax);
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@TitleCName", DbType.String, titleCName);
                db.AddInParameter(dbCommand, "@TitleEName", DbType.String, titleEName);
                db.AddInParameter(dbCommand, "@SoNo", DbType.String, soNo);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerNo);
                db.AddInParameter(dbCommand, "@ContainerType", DbType.String, containerTypeId);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@Etd", DbType.DateTime, etd);
                db.AddInParameter(dbCommand, "@LoadPortName", DbType.String, polId);
                db.AddInParameter(dbCommand, "@DiscPortName", DbType.String, podId);
                db.AddInParameter(dbCommand, "@DestinationName", DbType.String, placeOfDeliveryId);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, voyageId);//船名
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, VoyageNo);//航次
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@BillFeeIds", DbType.String, billFeeIds.Join());
                db.AddInParameter(dbCommand, "@FeeIds", DbType.String, feeIds.Join());
                db.AddInParameter(dbCommand, "@FeeCurrencyIds", DbType.String, feeCurrencyIds.Join());
                db.AddInParameter(dbCommand, "@FeeChargingCodeIds", DbType.String, feeChargingCodeIds.Join());
                db.AddInParameter(dbCommand, "@FeeRates", DbType.String, feeRates.Join());
                db.AddInParameter(dbCommand, "@FeeQuantities", DbType.String, feeQuantities.Join());
                db.AddInParameter(dbCommand, "@FeeAmounts", DbType.String, feeAmounts.Join());
                db.AddInParameter(dbCommand, "@FeeRemarks", DbType.String, feeRemarks.Join());
                db.AddInParameter(dbCommand, "@FeeUpdateDates", DbType.String, feeUpdateDates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);
                #endregion
                ManyResult[] result = db.ManyResults(dbCommand, new string[][] { new string[] { "ID" ,"UpdateDate","No","InvoiceNo" }, //主表
                    new string[] { "ID", "UpdateDate" } });//费用明细表

                if (result == null
                    || result.Length < 2
                    || result[0].Items.Count == 0)
                {
                    return null;
                }

                HierarchyManyResult results = new HierarchyManyResult(result[0].Items[0].Copy());
                if (result[1] != null)
                {
                    foreach (SingleResult s in result[1].Items)
                    {
                        results.Childs.Add(new HierarchyManyResult(s));
                    }
                }

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

        #region CancelInvoice SetInvoiceExpressInfo

        /// <summary>
        /// 改变发票状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="IsValid">是否有效</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult CancelInvoice(
            Guid id,
            bool IsValid,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish
            )
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCancelInvoice");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, IsValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
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
        #region 删除账单

        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="ids">IDS</param>
        /// <param name="updateDate">updateDate</param>
        /// <param name=" removeByID">removeByID</param>
        /// <param name="isEnglish">isEnglish</param>
        public void RemoveBillInfo(Guid[] ids, string updateDate, Guid removeByID,
                          bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveBillInfo");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@updateDates", DbType.String, updateDate);
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

        #region 删除费用明细

        /// <summary>
        /// 删除费用明细
        /// </summary>
        /// <param name="ids">IDS</param>
        /// <param name="updateDate">updateDate</param>
        /// <param name=" removeByID">removeByID</param>
        /// <param name="isEnglish">isEnglish</param>
        public void RemoveChargesInfo(Guid[] ids, DateTime[] updateDate, Guid removeByID,
                          bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveChargeInfo");

                db.AddInParameter(dbCommand, "@IDS", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@updateDates", DbType.String, updateDate.Join());
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
        /// <summary>
        /// 设置发票的快递信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="expressNo">快递号</param>
        /// <param name="expressDate">快递日期</param>
        /// <param name="changeByID">更新人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult SetInvoiceExpressInfo(
            Guid id,
            string expressNo,
            DateTime? expressDate,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSetInvoiceExpressInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ExpressNo", DbType.String, expressNo);
                db.AddInParameter(dbCommand, "@ExpressDate", DbType.DateTime, expressDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
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

        #region 设置发票号和快递单号

        /// <summary>
        /// 设置发票号和快递单号
        /// </summary>
        /// <param name="billIDs"></param>
        /// <param name="currencyIDs"></param>
        /// <param name="ways"></param>
        /// <param name="isCommissions"></param>
        /// <param name="invoiceIds"></param>
        /// <param name="invoiceNos"></param>
        /// <param name="expressNos"></param>
        /// <param name="changeByID"></param>
        /// <param name="invoiceUpdateDates"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
        public void SaveInvoiceNoAndExpressNoForBills(
            Guid[] billIDs,
            Guid[] currencyIDs,
            FeeWay[] ways,
            bool[] isCommissions,
            string[] invoiceIds,
            string[] invoiceNos,
            string[] expressNos,
            string[] invoiceTimes,
            Guid changeByID,
            string[] invoiceUpdateDates,
            bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveInvoiceNoForBill");

                db.AddInParameter(dbCommand, "@BillIDs", DbType.String, billIDs.Join());
                db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, currencyIDs.Join());
                db.AddInParameter(dbCommand, "@BillWays", DbType.String, ways.Join());
                db.AddInParameter(dbCommand, "@IsCommissions", DbType.String, isCommissions.Join());
                db.AddInParameter(dbCommand, "@InvoiceIDs", DbType.String, invoiceIds.Join());
                db.AddInParameter(dbCommand, "@InvoiceNos", DbType.String, invoiceNos.Join());
                db.AddInParameter(dbCommand, "@ExpressNos", DbType.String, expressNos.Join());
                db.AddInParameter(dbCommand, "@InvoiceDates", DbType.String, invoiceTimes.Join());
                db.AddInParameter(dbCommand, "@InvoiceUpdateDates", DbType.String, invoiceUpdateDates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, changeByID);
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

        #endregion
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns>CustomerInvoiceInfo</returns>
        public CustomerInvoiceInfo GetCustomerInfo(string CustomerName, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceCustomer");

                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, CustomerName);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CustomerInvoiceInfo result = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerInvoiceInfo
                                      {
                                          CustomerName = b.Field<string>("CustomerName").ToString(),
                                          CustomerAddress = b.Field<string>("Address").ToString(),
                                          CustomerTel = b.Field<string>("Tel").ToString(),
                                          CustomerFax = b.Field<string>("Fax").ToString(),
                                          CustomerType = b.Field<byte>("Type").ToString(),
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

        #region 发票统计

        public InvoieCountBaseReport GetInvoieCountReport(Guid companyID, DateTime beginDate, DateTime endDate, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceCountList");

                beginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0);
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<InvoiceCountReport> dataList = (from b in ds.Tables[0].AsEnumerable()
                                                     select new InvoiceCountReport
                                                       {
                                                           InvoiceDate = b.Field<DateTime>("InvoiceDate").ToString(),
                                                           CustomerName = b.Field<string>("CustomerName"),
                                                           Currency = b.Field<string>("Currency"),
                                                           Amount = b.Field<decimal>("Amount"),
                                                           InvoiceNo = b.Field<string>("InvoiceNo"),
                                                           Remark = b.Field<string>("Remark")
                                                       }).ToList();

                List<InvoiceCountReportTotal> totalList = (from b in ds.Tables[1].AsEnumerable()
                                                           select new InvoiceCountReportTotal
                                                     {
                                                         Currency = b.Field<string>("Currency"),
                                                         TotalAmount = b.Field<decimal>("TotalAmount"),
                                                     }).ToList();


                InvoieCountBaseReport baseReport = new InvoieCountBaseReport();
                baseReport.DataList = dataList;
                baseReport.TotalList = totalList;

                return baseReport;

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

        #region 保存发票号
        public List<InvoiceInfo> SaveInvoiceNo(string[] nos, string[] invoiceNos, Guid saveById, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveInvoiceNo");

                db.AddInParameter(dbCommand, "@SystemNos", DbType.String, nos.Join());
                db.AddInParameter(dbCommand, "@InvoiceNos", DbType.String, invoiceNos.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<InvoiceInfo> list = (from d in ds.Tables[0].AsEnumerable()
                                          select new InvoiceInfo
                                          {
                                              ID = d.Field<Guid>("ID"),
                                              No = d.Field<String>("No"),
                                              InvoiceNo = d.Field<String>("InvoiceNo"),
                                              UpdateDate = d.Field<DateTime?>("UpdateDate")
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

        #region 发票汇率
        /// <summary>
        /// 获取发票的汇率列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回发票的汇率列表</returns>
        public List<SolutionExchangeRateList> GetInvoiceExchangeRateList(bool? isValid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceExchangeRateList");

                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<SolutionExchangeRateList>();
                }

                List<SolutionExchangeRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new SolutionExchangeRateList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              ExchangeType = ExchangeType.Invoice,
                                                              SourceCurrency = b.Field<string>("SourceCurrency"),
                                                              TargetCurrency = b.Field<string>("TargetCurrency"),
                                                              FromDate = b.Field<DateTime>("FromDate"),
                                                              ToDate = b.Field<DateTime>("ToDate"),
                                                              Rate = b.Field<decimal>("Rate"),
                                                              IsValid = b.Field<bool>("IsValid"),
                                                              CreateByName = b.Field<string>("CreateByName"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                              CreateByID = b.Field<Guid>("CreateByID"),
                                                              SolutionID = Guid.NewGuid(),
                                                              SourceCurrencyID = b.Field<Guid>("SourceCurrencyID"),
                                                              TargetCurrencyID = b.Field<Guid>("TargetCurrencyID")
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
        /// 保存解决方案的汇率信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="sourceCurrencyIDs">源币种ID</param>
        /// <param name="targetCurrencyIDs">目标币种ID</param>
        /// <param name="fromDates">开始时间</param>
        /// <param name="toDates">结束时间</param>
        /// <param name="rates">汇率</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData SaveInvoiceExchangeRateInfo(
            Guid?[] ids,
            Guid[] sourceCurrencyIDs,
            Guid[] targetCurrencyIDs,
            DateTime[] fromDates,
            DateTime[] toDates,
            decimal[] rates,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveInvoiceExchangeRateInfo");

                string tempIds = ids.Join();
                string tempsourceCurrencyIDs = sourceCurrencyIDs.Join();
                string temptargetCurrencyIDs = targetCurrencyIDs.Join();
                string tempfromDates = fromDates.Join();
                string temptoDates = toDates.Join();
                string temprates = rates.Join(5);
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@SourceCurrencyIDs", DbType.String, tempsourceCurrencyIDs);
                db.AddInParameter(dbCommand, "@TargetCurrencyIDs", DbType.String, temptargetCurrencyIDs);
                db.AddInParameter(dbCommand, "@FromDates", DbType.String, tempfromDates);
                db.AddInParameter(dbCommand, "@ToDates", DbType.String, temptoDates);
                db.AddInParameter(dbCommand, "@Rates", DbType.String, temprates);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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
        /// 更改解决方案的汇率有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeInvoiceExchangeRateState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspChangeInvoiceExchangeRateState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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

        #region 发票免税明细报表
        public InvoiceFreeReportData GetInvoiceFreeList(Guid companyID, DateTime beginDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceFreeList");

                beginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0);
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<InvoiceFreeReportInfo> dataList = (from b in ds.Tables[0].AsEnumerable()
                                                        select new InvoiceFreeReportInfo
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         IndexNo = b.Field<Int32>("IndexNo"),
                                                         InvoiceNo = b.Field<string>("InvoiceNo"),
                                                         InvoiceDate = b.Field<DateTime>("InvoiceDate"),
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         BLNo = b.Field<string>("BLNo"),
                                                         BillNo = b.Field<string>("BillNo"),
                                                         ContractNo = b.Field<string>("ContractNo"),
                                                         VesselName = b.Field<string>("VesselName"),
                                                         POL = b.Field<string>("POL"),
                                                         POD = b.Field<string>("POD"),
                                                         Delivery = b.Field<string>("Delivery"),
                                                         ExpressNo = b.Field<string>("ExpressNo"),
                                                         Amount = b.Field<decimal>("Amount"),
                                                         TotalAmount = b.Field<decimal>("TotalAmount"),
                                                         USDAmount = b.Field<decimal>("USDAmount"),
                                                         RMBAmount = b.Field<decimal>("RMBAmount"),
                                                         OtherAmount = b.Field<decimal>("OtherAmount"),
                                                     }).ToList();

                InvoiceFreeReportTotal totalInfo = (from b in ds.Tables[1].AsEnumerable()
                                                    select new InvoiceFreeReportTotal
                                                    {
                                                        TaxNo = b.Field<string>("TaxNo"),
                                                        CompanyName = b.Field<string>("CompanyName"),
                                                        Date = b.Field<String>("Date"),
                                                        TotalCount = b.Field<Int32>("TotalCount"),
                                                    }).SingleOrDefault();

                if (totalInfo == null)
                {
                    totalInfo = new InvoiceFreeReportTotal();
                }

                totalInfo.TotalUSDAmount = (from d in dataList select d.USDAmount).Sum();
                totalInfo.TotalRMBAmount = (from d in dataList select d.RMBAmount).Sum();
                totalInfo.TotalOtherAmount = (from d in dataList select d.OtherAmount).Sum();

                InvoiceFreeReportData baseReport = new InvoiceFreeReportData();
                baseReport.DataList = dataList;
                baseReport.TotalInfo = totalInfo;

                return baseReport;

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

        #region 发票合同表
        public InvoiceFreeReportData GetInvoiceContractReportt(Guid[] bills, Guid currencyID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceContractReport");

                db.AddInParameter(dbCommand, "@Bills", DbType.String, bills.Join());
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, currencyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<InvoiceFreeReportInfo> dataList = (from b in ds.Tables[0].AsEnumerable()
                                                        select new InvoiceFreeReportInfo
                                                     {
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         BLNo = b.Field<string>("BLNo"),
                                                         VesselName = b.Field<string>("VesselName"),
                                                         POL = b.Field<string>("POLName"),
                                                         POD = b.Field<string>("PODName"),
                                                         Delivery = b.Field<string>("DeliveryName"),
                                                         USDAmount = b.Field<decimal>("USDAmount"),
                                                         RMBAmount = b.Field<decimal>("RMBAmount"),
                                                         OtherAmount = b.Field<decimal>("OtherAmount"),
                                                     }).ToList();

                InvoiceFreeReportTotal totalInfo = (from b in ds.Tables[1].AsEnumerable()
                                                    select new InvoiceFreeReportTotal
                                                    {
                                                        CustomerName = b.Field<string>("CustomerName"),
                                                        CompanyName = b.Field<string>("CompanyName"),
                                                        Date = b.Field<String>("Date"),
                                                    }).SingleOrDefault();

                if (totalInfo == null)
                {
                    totalInfo = new InvoiceFreeReportTotal();
                }

                totalInfo.TotalUSDAmount = 0.0m;
                totalInfo.TotalRMBAmount = 0.0m;
                totalInfo.TotalOtherAmount = 0.0m;
                totalInfo.DateMonth = DateTime.Now.ToString("yyyyMM");


                InvoiceFreeReportData baseReport = new InvoiceFreeReportData();
                baseReport.DataList = dataList;
                baseReport.TotalInfo = totalInfo;

                return baseReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 开票统计
        /// <summary>
        /// 开票统计
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="formDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<InvoiceCountReport> GetOperationInvoiceReport(Guid companyID, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetOperationInvoice");


                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<InvoiceCountReport> dataList = (from b in ds.Tables[0].AsEnumerable()
                                                     select new InvoiceCountReport
                                                        {
                                                            InvoiceDate = b.Field<string>("InvoiceDate"),
                                                            CustomerName = b.Field<String>("OperationNo"),
                                                            InvoiceNo = b.Field<String>("InvoiceNo"),
                                                            Currency = b.Field<String>("SalesName"),
                                                            Remark = b.Field<String>("CustomerServiceName")
                                                        }).ToList();



                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public string GetCompanyTaxNo(Guid companyID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            object TaxNo = db.ExecuteScalar(CommandType.Text, string.Format("SELECT taxno FROM pub.Configures c where companyid = '{0}' ", companyID.ToString()));

            if (TaxNo != null && TaxNo != DBNull.Value)
            {
                return TaxNo.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion

    }
}

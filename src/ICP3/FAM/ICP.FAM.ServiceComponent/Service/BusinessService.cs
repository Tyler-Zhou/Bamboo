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
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;

    /// <summary>
    /// 业务部分
    /// </summary>
    public partial class FinanceService
    {
        /// <summary>
        /// 获取业务列表
        /// </summary>
        /// <param name="companyIDs">公司ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="customer">客户</param>
        /// <param name="sales">业务员</param>
        /// <param name="filer">文件</param>
        /// <param name="hasBills">是否有帐单</param>
        /// <param name="minProfit">最少利润</param>
        /// <param name="maxProfit">最大利润</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="parameter">不同的业务类型有不同的参数</param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns>业务列表对象</returns>
        public PageList GetBusinessListByList(Guid[] companyIDs
                                            , string operationNo
                                            , string blNo
                                            , string ctnNo
                                            , string customer
                                            , string sales
                                            , string filer
                                            , decimal? minProfit
                                            , decimal? maxProfit
                                            , bool? hasBills
                                            , OperationType? operationType
                                            , OperationParameter parameter
                                           , DataPageInfo dataPageInfo
                                            )
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBusinessList");
                string tempCompanyIDs = companyIDs.Join();

                string tempOperationParameter = string.Empty;

                if (parameter != null)
                {
                    System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();
                    System.Xml.XmlWriter w = doc.CreateWriter();
                    System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(parameter.GetType());
                    s.Serialize(w, parameter);
                    w.Flush();
                    w.Close();
                    tempOperationParameter = doc.ToString();
                }

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@operationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ctnNo", DbType.String, ctnNo);
                db.AddInParameter(dbCommand, "@customer", DbType.String, customer);
                db.AddInParameter(dbCommand, "@sales", DbType.String, sales);
                db.AddInParameter(dbCommand, "@filer", DbType.String, filer);
                db.AddInParameter(dbCommand, "@HasBills ", DbType.Boolean, hasBills);

                db.AddInParameter(dbCommand, "@minProfit", DbType.String, minProfit);
                db.AddInParameter(dbCommand, "@maxProfit", DbType.String, maxProfit);
                db.AddInParameter(dbCommand, "@operationTypes", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@OperationParameter", DbType.String, tempOperationParameter);

                db.AddInParameter(dbCommand, "@pageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@currentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<BusinessList> results = BulidBusinessListByDataSet(ds);
                dataPageInfo.TotalCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                PageList PageList = PageList.Create<BusinessList>(results, dataPageInfo);
                return PageList;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }



        /// <summary>
        /// 获取业务列表
        /// </summary>
        /// <param name="businessIDs">业务ID</param>
        /// <returns>业务列表对象</returns>
        public PageList GetBusinessListByIDs(Guid[] businessIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBusinessListByIDs");

                string tempIDs = businessIDs.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<BusinessList> results = BulidBusinessListByDataSet(ds);
                PageList PageList = PageList.Create<BusinessList>(results, null);
                return PageList;
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

        private List<BusinessList> BulidBusinessListByDataSet(DataSet ds)
        {
            List<BusinessList> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new BusinessList
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              CompanyID = b.Field<Guid>("CompanyID"),
                                              CustomerName = b.Field<string>("CustomerName"),
                                              DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
                                              DefaultCurrencyName = b.Field<string>("DefaultCurrencyName"),
                                              AP = b.Field<decimal>("CR"),
                                              AR = b.Field<decimal>("DR"),
                                              Profit = b.Field<decimal>("DR") - b.Field<decimal>("CR"),
                                              APDescription = b.Field<string>("DefaultCurrencyName") + ":" + b.Field<decimal>("CR").ToString(),
                                              ARDescription = b.Field<string>("DefaultCurrencyName") + ":" + b.Field<decimal>("DR").ToString(),
                                              ProfitDescription = b.Field<string>("DefaultCurrencyName") + ":" + (b.Field<decimal>("DR") - b.Field<decimal>("CR")).ToString(),
                                              OperationDescription = b.Field<string>("OperationDescription"),
                                              OperationNO = b.Field<string>("OperationNO"),
                                              OperationType = (OperationType)b.Field<byte>("OperationType"),
                                              RefNO = b.Field<string>("RefNO"),
                                              SolutionID = b.Field<Guid>("SolutionID"),
                                              StateDescription = GetStateDescription((OperationType)b.Field<byte>("OperationType"), b.Field<byte>("State")),
                                              //StateDescription = b.Field<Byte>("State").ToString(),
                                              CompanyName = b.Field<string>("CompanyName"),
                                              SalesName = b.Field<string>("SalesName"),
                                              SONO = b.Field<string>("SONO"),
                                              ContainerNos = b.Field<string>("ContainerNos"),
                                              CustomerID = b.Field<Guid>("CustomerID"),
                                              GoodsNmae = b.Field<string>("GoodsNmae"),
                                              HBLs = b.Field<string>("HBLs"),
                                              MBLs = b.Field<string>("MBLs"),
                                              MBLID = b.Field<string>("MBLID"),
                                              HBLID = b.Field<string>("HBLID"),
                                              //AgentID = b.Field<Guid?>("AgentID"),
                                              Selected = false,
                                              IsDirty = false,
                                          }).ToList();
            return results;
        }

        #region

        private string GetStateDescription(OperationType operationType, byte state)
        {
            if (operationType == OperationType.OceanExport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OEOrderState>((OEOrderState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.OceanImport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OIOrderState>((OIOrderState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.AirExport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<AEOrderState>((AEOrderState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.AirImport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<AIOrderState>((AIOrderState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.Other)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OBBLState>((OBBLState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.Internal)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DTBLState>((DTBLState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.Truck)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<TruckBusinessState>((TruckBusinessState)state, this.IsEnglish);
            }


            else
            {
                return "未找到描述.请在 BusinessService.StateDescription方法中增加该描述的获取实现.";
            }
        }
        /// <summary>
        /// 拖车业务状态
        /// </summary>
        public enum TruckBusinessState
        {
            /// <summary>
            /// 未派车
            /// </summary>
            [MemberDescription("未派车", "No Truck")]
            NoTruck = 1,
            /// <summary>
            /// 已派车
            /// </summary>
            [MemberDescription("已派车", "Trucked")]
            Trucked = 2,
            /// <summary>
            /// 已提柜
            /// </summary>
            [MemberDescription("已提柜", "PickAt")]
            PickAt = 3,
            /// <summary>
            /// 已交货
            /// </summary>
            [MemberDescription("已交货", "Delivery")]
            Delivery = 4,
            /// <summary>
            /// 已还柜
            /// </summary>
            [MemberDescription("已还柜", "Return")]
            Return = 5,
            /// <summary>
            /// 已完成
            /// </summary>
            [MemberDescription("已完成", "Completed")]
            Completed = 6
        }

        #endregion


        /// <summary>
        /// 获取后续业务列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="BillTo">往来单位</param>
        /// <param name="operation">业务类型</param>
        /// <returns>业务列表对象</returns>
        public List<FollowingBusinessList> GetFollowingBusinessList(Guid operationID,
                                                                    Guid BillTo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetFollowingBusinessList");

                db.AddInParameter(dbCommand, "@operationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@BillTo", DbType.Guid, BillTo);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<FollowingBusinessList> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new FollowingBusinessList
                                                        {
                                                            ID = b.Field<Guid>("ID"),
                                                            CompanyID = b.Field<Guid>("CompanyID"),
                                                            CustomerName = b.Field<string>("CustomerName"),
                                                            DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
                                                            DefaultCurrencyName = b.Field<string>("DefaultCurrencyName"),
                                                            AP = b.Field<decimal>("CR"),
                                                            AR = b.Field<decimal>("DR"),
                                                            Profit = b.Field<decimal>("DR") - b.Field<decimal>("CR"),
                                                            APDescription = b.Field<string>("DefaultCurrencyName") + ":" + b.Field<decimal>("CR").ToString(),
                                                            ARDescription = b.Field<string>("DefaultCurrencyName") + ":" + b.Field<decimal>("DR").ToString(),
                                                            ProfitDescription = b.Field<string>("DefaultCurrencyName") + ":" + (b.Field<decimal>("DR") - b.Field<decimal>("CR")).ToString(),
                                                            OperationDescription = b.Field<string>("OperationDescription"),
                                                            OperationNO = b.Field<string>("OperationNO"),
                                                            OperationType = (OperationType)b.Field<byte>("OperationType"),
                                                            RefNO = b.Field<string>("RefNO"),
                                                            SolutionID = b.Field<Guid>("SolutionID"),
                                                            BillToID = b.Field<Guid>("BillToID"),
                                                            BillToName = b.Field<string>("BillToName"),
                                                            SIBy=b.Field<string>("SIBy"),
                                                            CustomerService = b.Field<string>("CustomerService"),
                                                            Selected = false,
                                                            IsDirty = false,
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
        /// 批量帐单
        /// </summary>
        /// <param name="operationIDs">业务ID</param>
        /// <param name="bankAccountID">银行IDs</param>
        /// <param name="currencyIDs">币种</param>
        /// <param name="chargingCodeIDs">会计科目</param>
        /// <param name="feeWays">方向</param>
        /// <param name="amounts">金额</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>ManyResult{ID,UpdateDate}</returns>
        public ManyResult BatchSaveBillInfo(Guid[] operationIDs
            , Guid bankAccountID
            , Guid[] currencyIDs
            , Guid[] chargingCodeIDs
            , FeeWay[] feeWays
            , decimal[] amounts
            , Guid saveByID
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            ArgumentHelper.AssertArrayLengthMatch(
                operationIDs,
                currencyIDs,
                chargingCodeIDs,
                feeWays,
                amounts);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspBatchSaveBillInfo");

                string tempoperationIDs = operationIDs.Join();
                string tempCurrencyIDs = currencyIDs.Join();
                string tempChargingCodeIDs = chargingCodeIDs.Join();
                string tempFeeWays = feeWays.Join();
                string tempAmounts = amounts.Join();

                db.AddInParameter(dbCommand, "@operationIDs", DbType.String, tempoperationIDs);
                db.AddInParameter(dbCommand, "@BankAccountID", DbType.Guid, bankAccountID);
                db.AddInParameter(dbCommand, "@currencyIDs", DbType.String, tempCurrencyIDs);
                db.AddInParameter(dbCommand, "@chargingCodeIDs", DbType.String, tempChargingCodeIDs);
                db.AddInParameter(dbCommand, "@feeWays", DbType.String, tempFeeWays);
                db.AddInParameter(dbCommand, "@feeAmounts", DbType.String, tempAmounts);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID" });
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

        #region 获得业务对应的对账单状态
        /// <summary>
        /// 获得业务对应的对账单状态
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public AgentBillCheckStatusEnum GetABCStatue(Guid operationID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetOperationAgentBillCheckStatus");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return AgentBillCheckStatusEnum.None;
                }


                AgentBillCheckStatusEnum result = (AgentBillCheckStatusEnum)(from c in ds.Tables[0].AsEnumerable() select c.Column<Byte>("AgentBillCheckStatus")).SingleOrDefault();


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

        #region 保存后续业务信息

        public SingleResult UpdateFollowingBusinessList(FollowBusinessSaveRequest followBusinessSaveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, followBusinessSaveRequest.ID);
                db.AddInParameter(dbCommand, "@MBLCopy", DbType.String, followBusinessSaveRequest.MBLCopy);
                db.AddInParameter(dbCommand, "@CNCopy", DbType.String, followBusinessSaveRequest.CNCopy);
                db.AddInParameter(dbCommand, "@VesselVoyage", DbType.String, followBusinessSaveRequest.VesselVoyage);
                db.AddInParameter(dbCommand, "@SOCopy", DbType.String, followBusinessSaveRequest.SOCopy);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, followBusinessSaveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@UpdateBy", DbType.Guid, followBusinessSaveRequest.UpdateBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion


        /// <summary>
        /// 得到业务运费已包含的费用项目
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public FreightIncludedInfo GetOperationFreightIncluded(Guid operationId ,OperationType type) 
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationFreightIncluded");
            db.AddInParameter(dbCommand, "@OperationId", DbType.Guid, operationId);
            db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, (int)type);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            {
                return null;
            }

            return new FreightIncludedInfo()
            {
                 FreightIncludedIds = ds.Tables[0].Rows[0][0].ToString(),             
                 FreightIncludedCodes =ds.Tables[0].Rows[0][1].ToString(),
            };

        }


        /// <summary>
        /// 更新业务的运费已包含项目
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="freightIncluded"></param>
        /// <param name="type"></param>
        public void UpdateOperationFreightIncluded(Guid operationId, string freightIncluded, OperationType type)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspUpdateOperationFreightIncluded");
            db.AddInParameter(dbCommand, "@OperationId", DbType.Guid, operationId);
            db.AddInParameter(dbCommand, "@FreightIncluded", DbType.String, freightIncluded);
            db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, (int)type);
            try
            {
               db.SingleResult(dbCommand);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

    }
}

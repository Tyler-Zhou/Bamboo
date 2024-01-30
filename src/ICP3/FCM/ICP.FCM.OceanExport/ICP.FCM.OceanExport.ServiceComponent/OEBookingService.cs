using System.Diagnostics;
using System.Globalization;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Xml;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    partial class OceanExportService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="fees"></param>
        /// <param name="pos"></param>
        /// <param name="delegates"></param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveOceanBookingWithTrans(BookingSaveRequest saveRequest,
            List<FeeSaveRequest> fees, List<SaveRequestBookingDelegate> cspDelegates)
        {
            Stopwatch stopwatchBooking = Stopwatch.StartNew();
            System.Text.StringBuilder operationLog = new System.Text.StringBuilder();

            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();
                try
                {
                    operationLog.Append("以事务方式保存Booking");
                    Guid newBookingOrderId = Guid.Empty;

                    if (saveRequest != null)
                    {
                        result.Add(saveRequest.RequestId,
                            new SaveResponse
                            {
                                RequestId = saveRequest.RequestId,
                                SingleResult = this.SaveOceanBookingInfo(saveRequest)
                            });

                        if (saveRequest.id == Guid.Empty)
                        {
                            newBookingOrderId = result[saveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                            saveRequest.No = result[saveRequest.RequestId].SingleResult.GetValue<string>("No");
                        }
                        operationLog.AppendFormat("BookingID{0}[{1}ms]",
                            newBookingOrderId == Guid.Empty ? saveRequest.id : newBookingOrderId,
                            stopwatchBooking.ElapsedMilliseconds);
                    }

                    if (fees != null)
                    {
                        Stopwatch stopwatFees = Stopwatch.StartNew();
                        foreach (FeeSaveRequest fee in fees)
                        {
                            if (saveRequest != null && saveRequest.id == Guid.Empty)
                            {
                                fee.orderID = newBookingOrderId;
                                fee.ids = new Guid?[fee.ids.Length];
                            }
                            result.Add(fee.RequestId,
                                new SaveResponse
                                {
                                    RequestId = fee.RequestId,
                                    ManyResult = SaveOceanOrderFeeList(fee)
                                });
                        }
                        operationLog.AppendFormat("Save Fee[{0}ms]", stopwatFees.ElapsedMilliseconds);
                    }

                    if (cspDelegates != null)
                    {
                        Stopwatch stopwatFees = Stopwatch.StartNew();
                        foreach (SaveRequestBookingDelegate cspDelegate in cspDelegates)
                        {
                            if (saveRequest != null && saveRequest.id == Guid.Empty)
                            {
                                cspDelegate.OperationID = newBookingOrderId;
                                cspDelegate.ItemIDs = new Guid?[cspDelegate.ItemIDs.Length];
                            }
                            result.Add(cspDelegate.RequestId,
                                new SaveResponse
                                {
                                    RequestId = cspDelegate.RequestId,
                                    ManyResult = _FCMCommonService.SaveBookingDelegateList(cspDelegate),
                                });
                        }
                        operationLog.AppendFormat("Save Delegate[{0}ms]", stopwatFees.ElapsedMilliseconds);
                    }
                    
                    operationLog.Append("保存完成");
                    _OperationLogService.Add(DateTime.Now, "SAVE-BOOKING-DB", operationLog.ToString(), stopwatchBooking.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    operationLog.Append("保存失败");
                    _OperationLogService.Add(DateTime.Now, "SAVE-BOOKING-DB", operationLog.ToString(), stopwatchBooking.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    throw ex;
                }
                return result;
            }
        }

        #region 获取订舱数据

        /// <summary>
        /// 获取ShippingOrder列表
        /// </summary>
        /// <param name="oeOperationType">业务类型</param>
        /// <param name="POLID">装货港</param>
        /// <param name="PODID">卸货港</param>
        /// <param name="PlaceOfDeliveryID">交货地</param>
        /// <param name="operatorID">客服</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回ShippingOrder列表</returns>
        public List<ShippingOrderList> GetShippingOrderList(
            FCMOperationType oeOperationType,
            Guid? POLID,
            Guid? PODID,
            Guid? PlaceOfDeliveryID,
            Guid? operatorID,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetShippingOrderList");


                db.AddInParameter(dbCommand, "@BookingType", DbType.Int16, oeOperationType);
                db.AddInParameter(dbCommand, "@POLID", DbType.Guid, POLID);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, PODID);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, PlaceOfDeliveryID);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, operatorID);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ShippingOrderList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new ShippingOrderList
                                                   {
                                                       ID = b.Column<Guid>("ID"),
                                                       No = b.Field<string>("NO"),
                                                       AgentofcarrierID = b.Column<Guid?>("AgentofcarrierID"),
                                                       AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                       SODate = b.Column<DateTime>("SODate"),
                                                       CarrierID = b.Column<Guid>("CarrierID"),
                                                       CarrierName = b.Field<string>("CarrierName"),
                                                       ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                                       FreightRateID = b.Column<Guid?>("FreightRateID"),
                                                       ContractNo = b.Field<string>("ContractNo"),
                                                       CreateByID = b.Column<Guid>("CreateByID"),
                                                       CreateByName = b.Field<string>("CreateByName"),
                                                       CreateDate = b.Field<DateTime>("CreateDate"),
                                                       CYClosingDate = b.Field<DateTime?>("CYClosingDate"),
                                                       DOCClosingDate = b.Field<DateTime?>("DOCClosingDate"),
                                                       ETA = b.Field<DateTime?>("ETA"),
                                                       ETD = b.Field<DateTime?>("ETD"),
                                                       PreVoyageID = b.Field<Guid?>("PreVoyageID"),
                                                       PreVoyageName = b.Field<string>("PreVoyageName"),
                                                       ReturnLocationID = b.Column<Guid?>("ReturnLocationID"),
                                                       ReturnLocationName = b.Field<string>("ReturnLocationName"),
                                                       VoyageID = b.Column<Guid?>("VoyageID"),
                                                       VoyageName = b.Field<string>("VoyageName"),
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
        /// 获取CSCL中海EDI订舱数据
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        public CSCLBookingInfo GetCsclBookingInfo(Guid bookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetBookingCSCLInfo");

                db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, bookingID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CSCLBookingInfo results = (from b in ds.Tables[0].AsEnumerable()
                                           select new CSCLBookingInfo
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               BookingRemarksCN = b.Field<string>("BookingRemarksCN"),
                                               CargoDescUS = b.Field<string>("CargoDescUS"),
                                               Consignee = b.Field<string>("Consignee"),
                                               HBLNO = b.Field<string>("HBLNO"),
                                               IsDirty = false,
                                               Notify = b.Field<string>("Notify"),
                                               OceanBookingID = b.Field<Guid>("OceanBookingID"),
                                               RealConsignee = b.Field<string>("RealConsignee"),
                                               RealNotify = b.Field<string>("RealNotify"),
                                               RealShipper = b.Field<string>("RealShipper"),
                                               RemarksCN = b.Field<string>("RemarksCN"),
                                               SCACCode = b.Field<string>("SCACCode"),
                                               SCNO = b.Field<string>("SCNO"),
                                               Shipper = b.Field<string>("Shipper"),
                                               Marks = b.Field<string>("Marks"),
                                               BookingNO = b.Field<string>("BookingNO"),
                                               ReleaseCargoType = b.Field<string>("ReleaseCargoType"),
                                               DeliveryTerm = b.Field<string>("DeliveryTerm"),
                                               HSCode = b.Field<string>("HSCode"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate")
                                           }).SingleOrDefault();

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
        /// 获取当前客户最近业务所对应的文件or 当前客户为新客户and当前揽货人最近业务所对应的文件
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="salesId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        public List<UserInfo> GetFilersList(Guid? customerId, Guid? salesId, Guid? companyID, DateTime beginTime, DateTime endTime, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanFilerList");

                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@SalesId", DbType.Guid, salesId);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<UserInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new UserInfo
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              CName = b.Field<string>("CName"),
                                              EName = b.Field<string>("EName")
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
        /// 获取当前客户最近的海外部客服列表
        /// 如果当前客户为空，就返回揽货人最近业务所对应的海外部客服的列表。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="salesId"></param>
        /// <returns></returns>
        public List<UserInfo> GetOverseasFilersList(Guid? customerId, Guid? salesId, DateTime beginTime, DateTime endTime, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOverSeasFilerList");

                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@SalesId", DbType.Guid, salesId);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<UserInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new UserInfo
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              CName = b.Field<string>("CName"),
                                              EName = b.Field<string>("EName")
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
        /// 获取订舱列表
        /// </summary>
        /// <param name="companyIDs">业务所在口岸公司(操作的公司)</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="shippingOrderNo">订舱号</param>
        /// <param name="scno">合约号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="agentOfCarrierName">承运人名</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地</param>
        /// <param name="bookingRefNo">BookingRefNo</param>
        /// <param name="salesID">业务员</param>
        /// <param name="bookingerID">订舱</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        public List<OceanBookingList> GetOceanBookingList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string ctnNo,
            string shippingOrderNo,
            string scno,
            string cusClearanceNo,
            string customerName,
            string carrierName,
            string agentOfCarrierName,
            string vesselName,
            string voyageNo,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string bookingRefNo,
            Guid? salesID,
            Guid? bookingerID,
            Guid? overseasFilerID,
            bool? isValid,
            OEOrderState? state,
            ICP.FCM.OceanExport.ServiceInterface.DataObjects.DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBookingList");

                string tempCompanyIDs = companyIDs.Join();

                #region 构建参数
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@CtnNo", DbType.String, ctnNo);
                db.AddInParameter(dbCommand, "@ShippingOrderNo", DbType.String, shippingOrderNo);
                db.AddInParameter(dbCommand, "@Scno", DbType.String, scno);
                db.AddInParameter(dbCommand, "@CusClearanceNo", DbType.String, cusClearanceNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@CarrierName", DbType.String, carrierName);
                db.AddInParameter(dbCommand, "@AgentOfCarrierName", DbType.String, agentOfCarrierName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
                db.AddInParameter(dbCommand, "@BookingRefNo", DbType.String, bookingRefNo);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, bookingerID);
                db.AddInParameter(dbCommand, "@OverseasFilerID", DbType.Guid, overseasFilerID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish); 
                #endregion

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBookingList> results = BulidOceanBookingListByDataSet(ds);
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
        /// 获取订舱列表
        /// </summary>
        /// <param name="bookingIDs">订舱单ID</param>
        /// <returns></returns>
        public List<OceanBookingList> GetOceanBookingListByIds(Guid[] bookingIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(bookingIDs, "bookingIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBookingListByIDs");

                string tempBookingIDs = bookingIDs.Join();

                db.AddInParameter(dbCommand, "@BookingIDs", DbType.String, tempBookingIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBookingList> results = BulidOceanBookingListByDataSet(ds);

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
        /// 获取订舱列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="noSearchType">单号搜索类型(0:全部,1:业务号,2:提单号,3:箱号,4:订舱号,5:合约号)</param>
        /// <param name="no">号码</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部,1:客户,2:船东,3:承运人,4:发货人,5:收货人,6:通知人,7:对单人)</param>
        /// <param name="customerName">客户名</param>
        /// <param name="portSearchType">港口搜索类型(0:全部,1:收货地,2:装货港,3:卸货港,4:交货地,5:最终目的地)</param>
        /// <param name="portName">港口名</param>
        /// <param name="dateSearchType">日期搜索类型(0:全部,1:离港日,2:到港日,3:订舱日,4:创建日,)</param>
        /// <param name="bookingerId"></param>
        /// <param name="isValid"></param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        public List<OceanBookingList> GetOceanBookingListForFaster(
            Guid[] companyIDs,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            ICP.FCM.OceanExport.ServiceInterface.DataObjects.DateSearchType dateSearchType,
            Guid? bookingerId,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBookingListForFaster");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@NoSearchType", DbType.Int16, noSearchType);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerSearchType", DbType.Int16, customerSearchType);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@LocationSearchType", DbType.Int16, portSearchType);
                db.AddInParameter(dbCommand, "@LocationName", DbType.String, portName);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, bookingerId);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBookingList> results = BulidOceanBookingListByDataSet(ds);

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

        private static List<OceanBookingList> BulidOceanBookingListByDataSet(DataSet ds)
        {
            List<OceanBookingList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new OceanBookingList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  CompanyID = b.Field<Guid>("CompanyID"),
                                                  OceanShippingOrderID = b.Field<Guid?>("OceanShippingOrderID"),
                                                  State = (OEOrderState)b.Field<byte>("State"),
                                                  No = b.Field<string>("No"),
                                                  MBLNo = b.Field<string>("MBLNo"),
                                                  HBLNo = b.Field<string>("HBLNo"),
                                                  Paid = b.Field<bool>("Paid"),
                                                  OceanShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                                  CustomerName = b.Field<string>("CustomerName"),
                                                  BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                                  CarrierName = b.Field<string>("CarrierName"),
                                                  CarrierID = b.Field<Guid?>("CarrierID"),
                                                  AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                  VesselVoyage = b.Field<string>("VesselVoyage"),
                                                  PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                                  POLName = b.Field<string>("POLName"),
                                                  PODName = b.Field<string>("PODName"),
                                                  PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                                  ETD = b.Field<DateTime?>("ETD"),
                                                  ShipperName = b.Field<string>("ShipperName"),
                                                  ConsigneeName = b.Field<string>("ConsigneeName"),
                                                  AgentName = b.Field<string>("AgentName"),
                                                  OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                                  OverSeasFilerName = b.Field<string>("OverSeasFilerName"),
                                                  BookingerName = b.Field<string>("BookingerName"),
                                                  SalesName = b.Field<string>("SalesName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  UpdateByName = b.Field<string>("UpdateByName"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  ContainerNo = b.Field<string>("ContainerNo"),
                                                  BookingDate = b.Field<DateTime>("BookingDate"),
                                                  SODate = b.Field<DateTime?>("SODate"),
                                                  ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                                  CreateByID = b.Field<Guid>("CreateByID"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  FilerName = b.Field<string>("FilerName"),
                                                  PODContact = b.Field<string>("AgentFilerName"),
                                                  ETA = b.Field<DateTime?>("ETA"),
                                                  IsValid = b.Field<bool>("IsValid"),
                                                  IsAllRBLD = b.Field<bool>("IsAllRBLD"),
                                                  IsDirty = false,
                                                  OceanMBLs = (from m in ds.Tables[1].AsEnumerable()
                                                               where b.Field<Guid>("ID") == m.Field<Guid>("OceanBookingID")
                                                               select new BookingBLInfo
                                                               {
                                                                   OceanBookingID = m.Field<Guid>("OceanBookingID"),
                                                                   ID = m.Field<Guid>("ID"),
                                                                   NO = m.Field<string>("No"),
                                                                   OperationNo = b.Field<string>("No"),
                                                                   State = (OEBLState)m.Field<byte>("State"),
                                                                   UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                               }).ToList(),
                                                  OceanHBLs = (from h in ds.Tables[2].AsEnumerable()
                                                               where b.Field<Guid>("ID") == h.Field<Guid>("OceanBookingID")
                                                               select new BookingBLInfo
                                                               {
                                                                   OceanBookingID = h.Field<Guid>("OceanBookingID"),
                                                                   ID = h.Field<Guid>("ID"),
                                                                   NO = h.Field<string>("No"),
                                                                   OperationNo = b.Field<string>("No"),
                                                                   State = (OEBLState)h.Field<byte>("State"),
                                                                   UpdateDate = h.Field<DateTime?>("UpdateDate"),
                                                               }).ToList(),
                                                  BookingContainers = (from c in ds.Tables[3].AsEnumerable()
                                                                       where b.Field<Guid>("ID") == c.Field<Guid>("OceanBookingID")
                                                                       select new BookingContainerInfo
                                                                       {
                                                                           OceanBookingID = c.Field<Guid>("OceanBookingID"),
                                                                           ID = c.Field<Guid>("ID"),
                                                                           NO = c.Field<string>("No"),
                                                                           TypeId = c.Field<Guid>("TypeId"),
                                                                           TypeName = c.Field<string>("TypeName")
                                                                       }).ToList(),

                                              }).ToList();

#if DEBUG

            int kk = results.FindAll(o => o.BookingContainers.Count > 0).Count;
            ICP.Framework.CommonLibrary.Logger.Log.Info(kk);
#endif

            return results;
        }

        /// <summary>
        /// 根据港口获取航线
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public Guid GetShippingForPort(Guid port)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT ShippingLineID  FROM  pub.PortReationCountry WHERE PortID=@PortID");

                db.AddInParameter(dbCommand, "@PortID", DbType.Guid, port);

                object results = db.ExecuteScalar(dbCommand);

                if (results == null)
                {
                    return Guid.Empty;
                }

                return (Guid)results;
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
        /// 获取订舱信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回订舱信息</returns>
        public OceanBookingInfo GetOceanBookingInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Stopwatch stopwatchBooking = Stopwatch.StartNew();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBookingInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                if (ds.Tables[0].Rows.Count > 1)
                {
                    throw new Exception("Found more than 1 booking orders. It's impossible!");
                }

                #region 构建实体
                OceanBookingInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new OceanBookingInfo
                                           {
                                               BookingDate = b.Field<DateTime>("BookingDate"),
                                               SODate = b.Field<DateTime?>("SODate"),
                                               BookingMode = b.IsNull("BookingMode") ? FCMBookingMode.Fax : (FCMBookingMode)b.Field<byte>("BookingMode"),
                                               TradeTermID = b.Field<Guid>("TradeTermID"),
                                               TradeTermName = b.Field<string>("TradeTermName"),
                                               CompanyID = b.Field<Guid>("CompanyID"),
                                               CompanyName = b.Field<string>("CompanyName"),
                                               SalesID = b.Field<Guid?>("SalesID"),
                                               SalesDepartmentID = b.Field<Guid>("SalesDepartmentID"),
                                               SalesDepartmentName = b.Field<string>("SalesDepartmentName"),
                                               BookingerID = b.Field<Guid?>("BookingerID"),
                                               CustomerID = b.Field<Guid>("CustomerID"),
                                               BookingCustomerID = b.Field<Guid>("BookingCustomerID"),
                                               BookingCustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("BookingCustomerDescription")),
                                               BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                               ShipperID = b.Field<Guid?>("ShipperID"),
                                               ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                               ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                               ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                               ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                               EstimatedDeliveryDate = b.Field<DateTime?>("EstimatedDeliveryDate"),
                                               DeliveryDate = b.Field<DateTime?>("DeliveryDate"),
                                               ExpectedShipDate = b.Field<DateTime?>("ExpectedShipDate"),
                                               ExpectedArriveDate = b.Field<DateTime?>("ExpectedArriveDate"),
                                               TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                               TransportClauseName = b.Field<string>("TransportClauseName"),
                                               PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                               PaymentTermName = b.Field<string>("PaymentTermName"),
                                               Commodity = b.Field<string>("Commodity"),
                                               Quantity = b.Field<int>("Quantity"),
                                               QuantityUnitID = b.Field<Guid?>("QuantityUnitID"),
                                               QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                               Weight = b.Field<decimal>("Weight"),
                                               WeightUnitID = b.Field<Guid?>("WeightUnitID"),
                                               WeightUnitName = b.Field<string>("WeightUnitName"),
                                               Measurement = b.Field<decimal>("Measurement"),
                                               MeasurementUnitID = b.Field<Guid?>("MeasurementUnitID"),
                                               MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                               CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")),
                                               ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                               IsTruck = b.Field<bool>("IsTruck"),
                                               IsCustoms = b.Field<bool>("IsCustoms"),
                                               IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                               IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                               IsOnlyMBL = b.Field<bool>("IsOnlyMBL"),
                                               MBLPaymentTermID = b.Field<Guid?>("MBLPaymentTermID"),
                                               MBLPaymentTermName = b.Field<string>("MBLPaymentTermName"),
                                               MBLReleaseType = b.IsNull("MBLReleaseType") ? (FCMReleaseType?)null : (FCMReleaseType)b.Field<byte>("MBLReleaseType"),
                                               MBLRequirements = b.Field<string>("MBLRequirements"),
                                               HBLPaymentTermID = b.Field<Guid?>("HBLPaymentTermID"),
                                               HBLPaymentTermName = b.Field<string>("HBLPaymentTermName"),
                                               HBLReleaseType = b.IsNull("HBLReleaseType") ? (FCMReleaseType?)null : (FCMReleaseType)b.Field<byte>("HBLReleaseType"),
                                               HBLRequirements = b.Field<string>("HBLRequirements"),
                                               Remark = b.Field<string>("Remark"),
                                               OverSeasFilerID = b.Field<Guid?>("OverSeasFilerID"),
                                               OverSeasFilerName = b.Field<string>("OverSeasFilerName"),
                                               ContainerNo = b.Field<string>("ContainerNo"),
                                               ContractName = b.Field<string>("ContractName"),
                                               ItemCode = b.Field<string>("ItemCode"),
                                               CreateByName = b.Field<string>("CreateByName"),
                                               SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                               SalesTypeName = b.Field<string>("SalesTypeName"),
                                               MBLNo = b.Field<string>("MBLNo"),
                                               HBLNo = b.Field<string>("HBLNo"),
                                               OceanShippingOrderID = b.Field<Guid?>("OceanShippingOrderID"),
                                               OceanShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                               CarrierID = b.IsNull("CarrierID") ? Guid.Empty : b.Field<Guid>("CarrierID"),
                                               AgentOfCarrierID = b.IsNull("AgentOfCarrierID") ? Guid.Empty : b.Field<Guid>("AgentOfCarrierID"),
                                               AgentID = b.Field<Guid?>("AgentID"),
                                               AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                               ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                               ShippingLineName = b.Field<string>("ShippingLineName"),
                                               IsContract = b.IsNull("IsContract") ? false : b.Field<bool>("IsContract"),
                                               PreVoyageID = b.Field<Guid?>("PreVoyageID"),
                                               PreVoyageName = b.Field<string>("PreVoyageName"),
                                               VoyageID = b.Field<Guid?>("VoyageID"),
                                               VoyageName = b.Field<string>("VoyageName"),
                                               ID = b.Field<Guid>("ID"),
                                               State = (OEOrderState)b.Field<byte>("State"),
                                               No = b.Field<string>("No"),
                                               CustomerName = b.Field<string>("CustomerName"),
                                               CarrierName = b.Field<string>("CarrierName"),
                                               AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                               PlaceOfDeliveryID = b.IsNull("PlaceOfDeliveryID") ? Guid.Empty : b.Field<Guid>("PlaceOfDeliveryID"),
                                               IsWareHouse = b.IsNull("IsWareHouse") ? false : b.Field<bool>("IsWareHouse"),
                                               FilerId = b.Field<Guid?>("FilerId"),
                                               FilerName = b.Field<string>("FilerName"),
                                               PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                               ETD = b.Field<DateTime?>("ETD"),
                                               PORETD = b.Field<DateTime?>("PreETD"),
                                               ShipperName = b.Field<string>("ShipperName"),
                                               ConsigneeName = b.Field<string>("ConsigneeName"),
                                               AgentName = b.Field<string>("AgentName"),
                                               OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                               BookingerName = b.Field<string>("BookingerName"),
                                               SalesName = b.Field<string>("SalesName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               ContractID = b.Field<Guid?>("ContractID"),
                                               ContractNo = b.Field<string>("ContractNo"),
                                               CYClosingDate = b.Field<DateTime?>("CYClosingDate"),
                                               DOCClosingDate = b.Field<DateTime?>("DOCClosingDate"),
                                               ETA = b.Field<DateTime?>("ETA"),
                                               PODID = b.Field<Guid>("PODID"),
                                               POLID = b.Field<Guid>("POLID"),
                                               POLName = b.Field<string>("POLName"),
                                               PODName = b.Field<string>("PODName"),
                                               PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                               PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                               ReturnLocationName = b.Field<string>("ReturnLocationName"),
                                               ReturnLocationID = b.Field<Guid?>("ReturnLocationID"),
                                               FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                               FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               OceanShippingOrderUpdateDate = b.Field<DateTime?>("OceanShippingOrderUpdateDate"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               WarehouseName = b.Field<string>("WarehouseName"),
                                               WarehouseID = b.Field<Guid?>("WarehouseID"),
                                               ClosingWarehousedate = b.Field<DateTime?>("ClosingWarehousedate"),
                                               IsDirty = false,
                                               BLTitleID = b.Field<Guid?>("BLTitleID"),
                                               BLTitleName = b.Field<string>("BLTitleName"),
                                               PickupEarliestDate = b.Field<DateTime?>("PickupEarliestDate"),
                                               BookingByID = b.Field<Guid?>("BookingByID"),
                                               BookingByName = b.Field<string>("BookingByName"),
                                               CarrierCode = b.Field<string>("Code"),
                                               IsThirdPlacePay = b.IsNull("IsThirdPlacePay") ? false : b.Field<bool>("IsThirdPlacePay"),
                                               CollectbyAgentID = b.IsNull("CollectbyAgentID") ? Guid.Empty : b.Field<Guid?>("CollectbyAgentID"),
                                               CollectbyAgentName = b.IsNull("CollectbyAgentName") ? string.Empty : b.Field<string>("CollectbyAgentName"),

                                               BookingPartyID = b.Field<Guid?>("BookingPartyID"),
                                               BookingPartyName = b.Field<string>("BookingPartyName"),
                                               BookingShipperID = b.Field<Guid?>("BookingShipperID"),
                                               BookingShipperdescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("BookingShipperdescription")),
                                               BookingConsigneeID = b.Field<Guid?>("BookingConsigneeID"),
                                               BookingConsigneedescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("BookingConsigneedescription")),
                                               BookingNotifyPartyID = b.Field<Guid?>("BookingNotifyPartyID"),
                                               BookingNotifyPartydescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("BookingNotifyPartydescription")),
                                               PickupRequirement = b.Field<String>("PickupRequirement"),
                                               BookingExplanation = b.Field<String>("BookingExplanation"),
                                               IsThirdPlacePayOrder = b.IsNull("IsThirdPlacePayOrder") ? false : b.Field<bool>("IsThirdPlacePayOrder"),
                                               CollectbyAgentNameOrder = b.IsNull("CollectbyAgentNameOrder") ? string.Empty : b.Field<string>("CollectbyAgentNameOrder"),
                                               CollectbyAgentOrderID = b.IsNull("CollectbyAgentIDOrder") ? Guid.Empty : b.Field<Guid?>("CollectbyAgentIDOrder"),
                                               Marks = b.Field<String>("Marks"),
                                               ScacCode = b.Field<String>("ScacCode"),
                                               BookingShipperName = b.Field<String>("BookingShipperName"),
                                               BookingConsigneeName = b.Field<String>("BookingConsigneeName"),
                                               BookingNotifyPartyname = b.Field<String>("BookingNotifyPartyName"),
                                               NotifyPartyname = b.Field<String>("NotifyPartyName"),

                                               IsInsurance = b.IsNull("IsInsurance") ? false : b.Field<bool>("IsInsurance"),
                                               IsFumigation = b.IsNull("IsFumigation") ? false : b.Field<bool>("IsFumigation"),
                                               IsWoodPacking = b.IsNull("IsWoodPacking") ? false : b.Field<bool>("IsWoodPacking"),
                                               IsCarrierSendAMS = b.IsNull("IsCarrierSendAMS") ? false : b.Field<bool>("IsCarrierSendAMS"),
                                               MBLTransportClauseID = b.Field<Guid?>("MBLTransportClauseID"),
                                               AMSClosingDate = b.Field<DateTime?>("AMSClosingDate"),
                                               NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                               MBLTransportClauseName = b.Field<String>("MBLTransportClauseName"),
                                               NotifyPartydescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartydescription")),
                                               BookingRefNo = b.Field<String>("BookingRefNo"),
                                               OkToSub = b.IsNull("OkToSub") ? false : b.Field<bool>("OkToSub"),
                                               RailCutOff = b.Field<DateTime?>("RailCutOff"),
                                               CusClearanceNo = b.Field<String>("CusClearanceNo"),
                                               DownState = b.Field<bool>("DownState"),
                                               FreightIncludedids = b.Field<string>("FreightIncluded"),
                                               FreightIncludedString = b.Field<string>("FreightIncludedName"),
                                               GateInDate = b.Field<DateTime?>("GateInDate"),
                                               VGMCutOffDate = b.Field<DateTime?>("VGMCutOffDate"),
                                               OperationDate = b.Field<DateTime?>("OperationDate"),
                                               HSCode = b.Field<string>("HSCode"),
                                               PlaceOfReceiptAddress = b.Field<string>("PlaceOfReceiptAddress"),
                                               PlaceOfDeliveryAddress = b.Field<string>("PlaceOfDeliveryAddress"),
                                               OceanMBLs = (from m in ds.Tables[1].AsEnumerable()
                                                            where b.Field<Guid>("ID") == m.Field<Guid>("OceanBookingID")
                                                            select new BookingBLInfo
                                                            {
                                                                OceanBookingID = m.Field<Guid>("OceanBookingID"),
                                                                ID = m.Field<Guid>("ID"),
                                                                NO = m.Field<string>("No"),
                                                                State = (OEBLState)m.Field<byte>("State"),
                                                                UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                            }).ToList(),
                                               OceanHBLs = (from h in ds.Tables[2].AsEnumerable()
                                                            where b.Field<Guid>("ID") == h.Field<Guid>("OceanBookingID")
                                                            select new BookingBLInfo
                                                            {
                                                                OceanBookingID = h.Field<Guid>("OceanBookingID"),
                                                                ID = h.Field<Guid>("ID"),
                                                                NO = h.Field<string>("No"),
                                                                State = (OEBLState)h.Field<byte>("State"),
                                                                UpdateDate = h.Field<DateTime?>("UpdateDate"),
                                                            }).ToList(),
                                               BookingContainers = (from h in ds.Tables[3].AsEnumerable()
                                                                    where b.Field<Guid>("ID") == h.Field<Guid>("OceanBookingID")
                                                                    select new BookingContainerInfo
                                                                    {
                                                                        OceanBookingID = h.Field<Guid>("OceanBookingID"),
                                                                        ID = h.Field<Guid>("ID"),
                                                                        NO = h.Field<string>("No"),
                                                                        TypeId = h.Field<Guid>("TypeId"),
                                                                        TypeName = h.Field<string>("TypeName"),
                                                                    }).ToList(),
                                               InquirePricePartInfo = (from i in ds.Tables[4].AsEnumerable()
                                                                       select new InquirePricePartInfo()
                                                                           {
                                                                               ConfirmedByID = i.Field<Guid>("InquirePriceOceanConfirmedBy"),
                                                                               ConfirmedByEame = i.Field<string>("Ename"),
                                                                               InquirePriceID = i.Field<Guid>("InquirePriceOceanID"),
                                                                               InquirePriceNO = i.Field<string>("NO")
                                                                           }).FirstOrDefault(),
                                               QuotedPricePartInfo = (from i in ds.Tables[5].AsEnumerable()
                                                                      select new QuotedPricePartInfo()
                                                                      {
                                                                          QuotedPriceID = i.Field<Guid>("QuotedPriceID"),
                                                                          QuotedPriceNo = i.Field<string>("QuotedPriceNo")
                                                                      }).FirstOrDefault(),
                                           }).SingleOrDefault(); 
                #endregion
                _OperationLogService.Add(DateTime.Now, "GET-Booking-DB", string.Format("获取Booking ID[{0}]", id), stopwatchBooking.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

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

        /// <summary>
        /// 保存订舱信息
        /// </summary>
        /// <param name="saveRequest">用于保存订舱单的对象</param>
        /// <returns>返回SingleResultDataWithNo</returns>
        public SingleResult SaveOceanBookingInfo(BookingSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.bookingCustomerID, "bookingCustomerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.companyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.salesDepartmentID, "salesDepartmentID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.tradeTermID, "tradeTermID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");

            try
            {
                //保存Booking
                Stopwatch stopwatchBooking = Stopwatch.StartNew();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanBookingInfo");
                //设置60秒超时
                dbCommand.CommandTimeout = 60;

                #region 构建参数
                string tempBookingCustomerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.bookingCustomerDescription, true, false);
                string tempShipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.shipperDescription, true, false);
                string tempConsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.consigneeDescription, true, false);
                string tempAgentDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.agentDescription, true, false);
                string tempContainerDescription = (saveRequest.containerDescription == null || saveRequest.containerDescription.Containers.Count == 0) ? null : SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.containerDescription, true, false);
                string tempCargoDescription = SerializerHelper.SerializeToString<CargoDescription>(saveRequest.cargoDescription, true, false);

                string tempNotifyPartydescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.NotifyPartydescription, true, false);
                string tempBookingShipperdescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.bookingShipperdescription, true, false);
                string tempBookingConsigneedescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.bookingConsigneedescription, true, false);
                string tempBookingNotifyPartydescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.bookingNotifyPartydescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.id);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, saveRequest.customerRefNo);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.customerID);
                db.AddInParameter(dbCommand, "@TradeTermID", DbType.Guid, saveRequest.tradeTermID);
                db.AddInParameter(dbCommand, "@OEOperationType", DbType.Int16, saveRequest.oeOperationType);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.companyID);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, saveRequest.filerID);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, saveRequest.bookingerID);
                db.AddInParameter(dbCommand, "@BookingByID", DbType.Guid, saveRequest.bookingById);
                db.AddInParameter(dbCommand, "@OverSeasFilerId", DbType.Guid, saveRequest.overSeasFilerId);
                db.AddInParameter(dbCommand, "@SalesDepartmentID", DbType.Guid, saveRequest.salesDepartmentID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, saveRequest.salesID);
                db.AddInParameter(dbCommand, "@SalesTypeID", DbType.Guid, saveRequest.salesTypeID);
                db.AddInParameter(dbCommand, "@BookingMode", DbType.Int16, saveRequest.bookingMode);
                db.AddInParameter(dbCommand, "@BookingDate", DbType.DateTime, saveRequest.bookingDate);
                db.AddInParameter(dbCommand, "@BookingCustomerID", DbType.Guid, saveRequest.bookingCustomerID);
                db.AddInParameter(dbCommand, "@BookingCustomerDescription", DbType.Xml, tempBookingCustomerDescription);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, saveRequest.shipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempShipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, saveRequest.consigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempConsigneeDescription);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, saveRequest.placeOfReceiptID);
                db.AddInParameter(dbCommand, "@POLID", DbType.Guid, saveRequest.polID);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, saveRequest.podID);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, saveRequest.placeOfDeliveryID);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, saveRequest.agentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, tempAgentDescription);
                db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, saveRequest.agentOfCarrierID);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, saveRequest.carrierID);
                db.AddInParameter(dbCommand, "@IsContract", DbType.Boolean, saveRequest.isContract);
                db.AddInParameter(dbCommand, "@FreightRateID", DbType.Guid, saveRequest.freightRateID);
                db.AddInParameter(dbCommand, "@ShippingOrderNo", DbType.String, saveRequest.oceanShippingOrderNo);
                db.AddInParameter(dbCommand, "@OceanShippingOrderID", DbType.Guid, saveRequest.oceanShippingOrderID);
                db.AddInParameter(dbCommand, "@SODate", DbType.DateTime, saveRequest.soDate);
                db.AddInParameter(dbCommand, "@ClosingDate", DbType.DateTime, saveRequest.closingDate);
                db.AddInParameter(dbCommand, "@EstimatedDeliveryDate", DbType.DateTime, saveRequest.estimatedDeliveryDate);
                db.AddInParameter(dbCommand, "@DevliveryDate", DbType.DateTime, saveRequest.actualDeliveryDate);
                db.AddInParameter(dbCommand, "@ExpectedShipDate", DbType.DateTime, saveRequest.expectedShipDate);
                db.AddInParameter(dbCommand, "@ExpectedArriveDate", DbType.DateTime, saveRequest.expectedArriveDate);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, saveRequest.paymentTermID);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, saveRequest.transportClauseID);
                db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, saveRequest.shippingLineID);
                db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, saveRequest.preVoyageID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, saveRequest.voyageID);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.commodity);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, saveRequest.quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, saveRequest.quantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, saveRequest.weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, saveRequest.weightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, saveRequest.measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, saveRequest.measurementUnitID);
                db.AddInParameter(dbCommand, "@CargoDescription", DbType.Xml, tempCargoDescription);
                db.AddInParameter(dbCommand, "@ContainerDescription", DbType.Xml, tempContainerDescription);
                db.AddInParameter(dbCommand, "@MBLPaymentTermID", DbType.Guid, saveRequest.mblPaymentTermID);
                db.AddInParameter(dbCommand, "@HBLPaymentTermID", DbType.Guid, saveRequest.hblPaymentTermID);
                db.AddInParameter(dbCommand, "@IsTruck", DbType.Boolean, saveRequest.isTruck);
                db.AddInParameter(dbCommand, "@IsCustoms", DbType.Boolean, saveRequest.isCustoms);
                db.AddInParameter(dbCommand, "@IsCommodityInspection", DbType.Boolean, saveRequest.isCommodityInspection);
                db.AddInParameter(dbCommand, "@IsQuarantineInspection", DbType.Boolean, saveRequest.isQuarantineInspection);
                db.AddInParameter(dbCommand, "@IsWarehouse", DbType.Boolean, saveRequest.isWarehouse);
                db.AddInParameter(dbCommand, "@IsOnlyMBL", DbType.Boolean, saveRequest.isOnlyMBL);
                db.AddInParameter(dbCommand, "@MblReleaseType", DbType.Int16, saveRequest.mblReleaseType);
                db.AddInParameter(dbCommand, "@HblReleaseType", DbType.Int16, saveRequest.hblReleaseType);
                db.AddInParameter(dbCommand, "@MblRequirements", DbType.String, saveRequest.mblRequirements);
                db.AddInParameter(dbCommand, "@HblRequirements", DbType.String, saveRequest.hblRequirements);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.remark);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.finalDestinationID);
                db.AddInParameter(dbCommand, "@ReturnLocationID", DbType.Guid, saveRequest.returnLocationID);
                db.AddInParameter(dbCommand, "@WarehouseID", DbType.Guid, saveRequest.warehouseID);
                db.AddInParameter(dbCommand, "@ClosingWarehousedate", DbType.DateTime, saveRequest.closingWarehousedate);
                db.AddInParameter(dbCommand, "@OceanShippingOrderUpdateDate", DbType.DateTime, saveRequest.oceanShippingOrderUpdateDate);
                db.AddInParameter(dbCommand, "@OceanOrderUpdateDate", DbType.DateTime, saveRequest.oceanOrderUpdateDate);

                db.AddInParameter(dbCommand, "@PickupEarliestDate", DbType.DateTime, saveRequest.pickupEarliestDate);
                db.AddInParameter(dbCommand, "@CYClosingDate", DbType.DateTime, saveRequest.CYClosingDate);
                db.AddInParameter(dbCommand, "@DOCClosingDate", DbType.DateTime, saveRequest.DOCClosingDate);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);
                db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, saveRequest.PreETD);
                db.AddInParameter(dbCommand, "@IsThirdPlacePay", DbType.Boolean, saveRequest.IsThirdPlacePay);
                db.AddInParameter(dbCommand, "@CollectbyAgentID", DbType.Guid, saveRequest.CollectbyAgentID);

                //新增加
                db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, saveRequest.NotifyPartyID);
                db.AddInParameter(dbCommand, "@NotifyPartydescription", DbType.Xml, tempNotifyPartydescription);
                db.AddInParameter(dbCommand, "@BookingPartyID", DbType.Guid, saveRequest.bookingPartyID);
                db.AddInParameter(dbCommand, "@BookingShipperID", DbType.Guid, saveRequest.bookingShipperID);
                db.AddInParameter(dbCommand, "@BookingShipperdescription", DbType.Xml, tempBookingShipperdescription);
                db.AddInParameter(dbCommand, "@BookingConsigneeID", DbType.Guid, saveRequest.bookingConsigneeID);
                db.AddInParameter(dbCommand, "@BookingConsigneedescription", DbType.Xml, tempBookingConsigneedescription);
                db.AddInParameter(dbCommand, "@BookingNotifyPartyID", DbType.Guid, saveRequest.bookingNotifyPartyID);
                db.AddInParameter(dbCommand, "@BookingNotifyPartydescription", DbType.Xml, tempBookingNotifyPartydescription);
                db.AddInParameter(dbCommand, "@PickupRequirement", DbType.String, saveRequest.pickupRequirement);
                db.AddInParameter(dbCommand, "@BookingExplanation", DbType.String, saveRequest.bookingExplanation);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, saveRequest.marks);
                db.AddInParameter(dbCommand, "@IsInsurance", DbType.Boolean, saveRequest.isInsurance);
                db.AddInParameter(dbCommand, "@IsFumigation", DbType.Boolean, saveRequest.isFumigation);
                db.AddInParameter(dbCommand, "@IsWoodPacking", DbType.Boolean, saveRequest.isWoodPacking);
                db.AddInParameter(dbCommand, "@IsCarrierSendAMS", DbType.Boolean, saveRequest.isCarrierSendAMS);
                db.AddInParameter(dbCommand, "@MBLTransportClauseID", DbType.Guid, saveRequest.mBLTransportClauseID);
                db.AddInParameter(dbCommand, "@AMSClosingDate", DbType.DateTime, saveRequest.AMSClosingDate);
                db.AddInParameter(dbCommand, "@IsThirdPlacePayOrder", DbType.Boolean, saveRequest.IsThirdPlacePayOrder);
                db.AddInParameter(dbCommand, "@CollectbyAgentOrderID", DbType.Guid, saveRequest.CollectbyAgentOrderID);
                db.AddInParameter(dbCommand, "@ScacCode", DbType.String, saveRequest.ScacCode);
                db.AddInParameter(dbCommand, "@BookingRefNo", DbType.String, saveRequest.BookingRefNo);
                db.AddInParameter(dbCommand, "@OkToSub", DbType.Boolean, saveRequest.OkToSub);
                db.AddInParameter(dbCommand, "@RailCutOff", DbType.DateTime, saveRequest.RailCutOff);
                db.AddInParameter(dbCommand, "@CusClearanceNo", DbType.String, saveRequest.CusClearanceNo);
                db.AddInParameter(dbCommand, "@InquirePriceOceanID", DbType.Guid, saveRequest.InquirePriceOceanId);
                db.AddInParameter(dbCommand, "@InquirePriceOceanConfirmedBy", DbType.Guid, saveRequest.InquirePriceOceanConfirmedBy);
                db.AddInParameter(dbCommand, "@FreightIncluded", DbType.String, saveRequest.FreightIncludedIds);
                db.AddInParameter(dbCommand, "@GateInDate", DbType.DateTime, saveRequest.GateInDate);
                db.AddInParameter(dbCommand, "@VGMCutOffDate", DbType.DateTime, saveRequest.VGMCutOffDate);
                db.AddInParameter(dbCommand, "@QuotedPriceID", DbType.Guid, saveRequest.QuotedPriceID);
                db.AddInParameter(dbCommand, "@HSCode", DbType.String, saveRequest.HSCode);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptAddress", DbType.String, saveRequest.PlaceOfReceiptAddress);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryAddress", DbType.String, saveRequest.PlaceOfDeliveryAddress);

                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                #endregion

                stopwatchBooking.Start();
                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "OceanShippingOrderUpdateDate", "OceanShippingOrderID", "No", "State" });
                _OperationLogService.Add(DateTime.Now, "SAVE-BOOKING-DB"
                    , string.Format("数据库保存;BookingID[{0}]", result.GetValue<Guid>("ID"))
                            , stopwatchBooking.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
#if DEBUG
                if (saveRequest != null && saveRequest.IsSyncCSP && !saveRequest.oceanShippingOrderNo.IsNullOrEmpty())
                {
                    SaveRequestShipmentInfoForCSP sqShipmentInfo = new SaveRequestShipmentInfoForCSP()
                    {
                        OperationID = result.GetValue<Guid>("ID"),
                        OperationType = OperationType.OceanExport,
                        SaveBy = saveRequest.saveByID,
                        UpdateDate = DateTime.Now,
                    };
                    _FCMCommonService.SaveShipmentInfoForCSP(sqShipmentInfo);
                }
#endif
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
        /// 保存CsclBookingInfo
        /// </summary>
        /// <param name="csclBookingInfo"></param>
        /// <returns></returns>
        public SingleResult SaveCsclBookingInfo(CSCLBookingSaveRequest csclBookingInfo)
        {
            ArgumentHelper.AssertGuidNotEmpty(csclBookingInfo.OceanBookingID, "OceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(csclBookingInfo.SaveByID, "SaveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveCSCLBookingInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, csclBookingInfo.ID);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, csclBookingInfo.OceanBookingID);
                db.AddInParameter(dbCommand, "@BookingRemarksCN", DbType.String, csclBookingInfo.BookingRemarksCN);
                db.AddInParameter(dbCommand, "@CargoDescUS", DbType.String, csclBookingInfo.CargoDescUS);
                db.AddInParameter(dbCommand, "@Consignee", DbType.String, csclBookingInfo.Consignee);
                db.AddInParameter(dbCommand, "@HBLNO", DbType.String, csclBookingInfo.HBLNO);
                db.AddInParameter(dbCommand, "@Notify", DbType.String, csclBookingInfo.Notify);
                db.AddInParameter(dbCommand, "@RealConsignee", DbType.String, csclBookingInfo.RealConsignee);
                db.AddInParameter(dbCommand, "@RealNotify", DbType.String, csclBookingInfo.RealNotify);
                db.AddInParameter(dbCommand, "@RealShipper", DbType.String, csclBookingInfo.RealShipper);
                db.AddInParameter(dbCommand, "@RemarksCN", DbType.String, csclBookingInfo.RemarksCN);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, csclBookingInfo.SaveByID);
                db.AddInParameter(dbCommand, "@SCACCode", DbType.String, csclBookingInfo.SCACCode);
                db.AddInParameter(dbCommand, "@SCNO", DbType.String, csclBookingInfo.SCNO);
                db.AddInParameter(dbCommand, "@Shipper", DbType.String, csclBookingInfo.Shipper);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, csclBookingInfo.Marks);
                db.AddInParameter(dbCommand, "@BookingNO", DbType.String, csclBookingInfo.BookingNO);
                db.AddInParameter(dbCommand, "@ReleaseCargoType", DbType.String, csclBookingInfo.ReleaseCargoType);
                db.AddInParameter(dbCommand, "@DeliveryTerm", DbType.String, csclBookingInfo.DeliveryTerm);
                db.AddInParameter(dbCommand, "@HSCode", DbType.String, csclBookingInfo.HSCode);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, csclBookingInfo.UpdateDate);
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

        #region 产生账单
        /// <summary>
        /// 生产账单
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="saveById"></param>
        /// <returns></returns>
        public SingleResult CreateBill(Guid operationID, Guid saveById)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBillInfoForOceanContractNO");

                dbCommand.CommandTimeout = 60;

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsResult", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);


                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count == 0)
                {
                    return null;
                }

                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault<DataRow>();
                if (row == null)
                {
                    return null;
                }

                SingleResult result = new SingleResult();
                result.Add("State", row.Field<Byte>("State"));
                result.Add("OperationNo", row.Field<string>("OperationNo"));

                string strMessage = row.Field<string>("ErrorMessage");
                if (!string.IsNullOrEmpty(strMessage))
                {
                    ErrorMessages messagecs = SerializerHelper.DeserializeFromString<ErrorMessages>(typeof(ContainerDescription), strMessage);
                    if (messagecs != null && messagecs.Messages.Count > 0)
                    {
                        result.Add("message", messagecs.Messages[0].Message);
                    }
                    else
                    {
                        result.Add("message", string.Empty);
                    }
                }
                else
                {
                    result.Add("message", string.Empty);
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

        #region 产生利润

        public ProfitContainerObjects GetOceanProfitReportData(Guid OperationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(OperationID, "OperationID");



            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanProfitReportData");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null && ds.Tables.Count == 0)
                {
                    return null;
                }
                ProfitContainerObjects profitData = new ProfitContainerObjects();


                profitData.ConfigureInfo = (from b in ds.Tables[0].AsEnumerable()
                                            select new ConfigureInfo
                                            {
                                                DefaultCurrency = b.Field<string>("CurrencyName"),

                                            }).ToList();


                profitData.BillInfoList = (from b in ds.Tables[1].AsEnumerable()
                                           select new ICP.FCM.OceanExport.ServiceInterface.DataObjects.BillTotalInfo
                                           {
                                               Way = (FeeWay)b.Field<Byte>("Way"),
                                               Amount = b.Field<Decimal>("Amount"),
                                               No = b.Field<String>("INVNO"),
                                               CustomerName = b.Field<String>("Company"),
                                               DueDate = b.Field<DateTime>("DueDate"),
                                               Type = (BillType)b.Field<byte>("Type"),
                                               PayAmountDescription = b.Field<string>("ChargeDescription"),
                                           }
                                            ).ToList();

                return profitData;
            }

            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
        }

        #endregion

        #region 发送邮件使用的方法

        /// <summary>
        /// 根据业务的ID查找联系人的邮箱地址
        /// </summary>
        /// <param name="id">业务号</param>
        /// <param name="tyep">类型</param>
        /// <param name="customerId">代理的ID</param>
        /// <param name="notequal">需要执行查询当前联系人关联客户不属于当前业务的代理</param>
        /// <param name="equal">需要执行查询当前联系人关联客户属于当前业务的代理</param>
        /// <returns>返回当前联系人的邮箱地址</returns>
        public List<MailInformation> GetContactEmail(Guid id, string tyep, Guid? customerId, bool notequal, bool equal)
        {
            try
            {
                string StrEmail = string.Empty;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanContactsEmail");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Type", DbType.String, tyep);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@Notequal", DbType.Boolean, notequal);
                db.AddInParameter(dbCommand, "@Equal ", DbType.Boolean, equal);
                DataTable dt = null;
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
                List<MailInformation> MailInformation = new List<MailInformation>();
                if (dt.Rows.Count > 0)
                {
                    MailInformation = (from a in dt.AsEnumerable()
                                       select new MailInformation
                                       {
                                           Email = a.Field<string>("Mail"),
                                           Name = a.Field<string>("NAME"),
                                           CC = a.Field<bool>("IsCC")
                                       }).ToList();
                }
                return MailInformation;
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
        /// 读取邮件模版的注意事项
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmailNotice(Guid organizationsId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetEmailNotice");
                db.AddInParameter(dbCommand, "@OrganizationsID", DbType.Guid, organizationsId);
                DataTable dt = null;
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
                return dt;
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
        /// 根据业务ID返回当前业务客服的联系信息
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <returns></returns>
        public UserInfo GetBookingerId(Guid oceanBookingId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetBookingerID");
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, oceanBookingId);
                DataTable dt = null;
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
                UserInfo userInfos = (from user in dt.AsEnumerable()
                                      select new UserInfo
                                          {
                                              CName = user.Field<string>("CName"),
                                              EName = user.Field<string>("Ename"),
                                              Tel = user.Field<string>("Tel"),
                                              Fax = user.Field<string>("Fax"),
                                              Mobile = user.Field<string>("Mobile"),
                                              Code = user.Field<string>("Code")
                                          }).FirstOrDefault();
                return userInfos;
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

        #region   修改OceanTrackings
        /// <summary>
        /// 执行修改SOPV的值
        /// </summary>
        /// <param name="oceanBooking">订单的ID</param>
        public bool SetUpdateOceanTrackings(Guid oceanBooking)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspUpdateOceanTrackings");
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBooking);
                DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
                return Convert.ToBoolean(dt.Rows[0]["IsSOPV"]);
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

        #region Email订舱/补料

        /// <summary>
        /// GetEmailBookingDataSource
        /// </summary>
        /// <param name="BookingID">BookingID</param>
        /// <returns>DataSet</returns>
        public DataSet GetEmailBookingDataSetByBookingID(Guid BookingID, Guid sendID)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode checknode = CreateXmlNode(xmldoc, "EDI", "");
            CreateXmlNode(xmldoc, "ID", ((Guid)BookingID).ToString(), ref checknode);
            CreateXmlNode(xmldoc, "SendID", ((Guid)sendID).ToString(), ref checknode);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspGetDataSetForEmailBooking");

            db.AddInParameter(dbCommand, "@Condition", DbType.String, checknode.OuterXml);

            DataSet ds = db.ExecuteDataSet(dbCommand);

            return ds;
        }

        /// <summary>
        /// 获取Email订舱/补料配置信息
        /// </summary>
        /// <returns></returns>
        public List<EmailBookingSIConfig> GetEmailBookingSIConfig(EDIMode ediMode)
        {
            try
            {
                string StrEmail = string.Empty;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetEmailBookingSIConfig");
                db.AddInParameter(dbCommand, "@EDIMode", DbType.Byte, (byte)ediMode);

                List<EmailBookingSIConfig> list = null;
                DataTable dt = null;
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
                List<MailInformation> MailInformation = new List<MailInformation>();
                if (dt.Rows.Count > 0)
                {
                    list = (from a in dt.AsEnumerable()
                            select new EmailBookingSIConfig
                            {
                                CarrierID = a.Field<Guid>("CarrierID"),
                                CompanyID = a.Field<Guid>("CompanyID"),
                                EDIMode = (EDIMode)Enum.ToObject(typeof(EDIMode), a.Field<byte>("EDIMode")),
                                EmailBookingSICode = (EmailBookingSICode)Enum.ToObject(typeof(EmailBookingSICode), a.Field<byte>("EmailBookingSICode")),
                                ID = a.Field<Guid>("ID"),
                                ReportName = a.Field<string>("ReportName"),
                                StoredProcedure = a.Field<string>("StoredProcedure")
                            }).ToList();
                }

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

        #region XML操作


        void AddNodeToParent(XmlDocument doc, XmlNode parent, string nodename, string nodevalue)
        {
            XmlNode tmpNode = doc.CreateNode(XmlNodeType.Element, nodename, "");
            if (!string.IsNullOrEmpty(nodevalue)) tmpNode.InnerText = nodevalue;
            parent.AppendChild(tmpNode);
        }

        XmlNode CreateXmlNode(XmlDocument xmlDoc, string nodeName, string nodeValue)
        {
            XmlNode tmpNode = xmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            if (!string.IsNullOrEmpty(nodeValue)) tmpNode.InnerText = nodeValue;
            return tmpNode;
        }


        void CreateXmlNode(XmlDocument xmlDoc, string nodeName, string nodeValue, ref XmlNode parentNode)
        {
            XmlNode tmpNode = xmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            if (!string.IsNullOrEmpty(nodeValue)) tmpNode.InnerText = nodeValue;
            parentNode.AppendChild(tmpNode);
        }
        #endregion

        /// <summary>
        ///通过卸货港和交货地得到航线
        /// </summary>
        /// <param name="placeOfDeliveryId">交货地ID</param>
        /// <param name="podId">卸货港ID</param>
        /// <returns>返回一个航线对象</returns>
        public ShippingLineList GetShippingLineForDeliveryAndPOD(Guid placeOfDeliveryId, Guid podId)
        {
            ArgumentHelper.AssertGuidNotEmpty(placeOfDeliveryId, "placeOfDeliveryId");
            ArgumentHelper.AssertGuidNotEmpty(podId, "podId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetShippingLineForDeliveryAndPOD");
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, placeOfDeliveryId);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, podId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ShippingLineList result = (from b in ds.Tables[0].AsEnumerable()
                                           select new ShippingLineList
                                           {
                                               ID = b.Column<Guid>("ShippingLineID"),
                                               CName = b.Field<string>("CName"),
                                               EName = b.Field<string>("EName")
                                           }).FirstOrDefault();

                return result;
            }

            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }

        }


        #region  修改订舱单的状态
        /// <summary>
        /// 修改订舱单的状态
        /// </summary>
        /// <param name="updateBy">修改人</param>
        /// <param name="operationId">业务的ID</param>
        /// <param name="oeOrderState">业务状态</param>
        public bool UpdateOceanBookingsState(Guid updateBy, Guid operationId, OEOrderState oeOrderState)
        {
            try
            {
                bool flg = false;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspOceanBookingsStateUpdate");
                db.AddInParameter(dbCommand, "@UpdateBy", DbType.Guid, updateBy);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, (int)oeOrderState);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    flg = true;
                }
                return flg;
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

        #region 复制业务
        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="oceanBookid">业务号</param>
        /// <param name="iscopyorder">复制订舱单</param>
        /// <param name="iscopyshipment">复制提单</param>
        /// <param name="iscopybill">复制账单</param>
        /// <param name="saveby">复制人</param>
        /// <param name="IsEnglish">是否英语</param>
        /// <returns></returns>
        public SingleResult CopyOperationInfo(Guid oceanBookid, bool iscopyorder, bool iscopyshipment, bool iscopybill, Guid saveby, bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookid, "oceanBookid");
            ArgumentHelper.AssertGuidNotEmpty(saveby, "saveby");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspCopyOperationInfo]");
                db.AddInParameter(dbCommand, "@OceanBookid", DbType.Guid, oceanBookid);
                db.AddInParameter(dbCommand, "@CopyBookOrder", DbType.Boolean, iscopyorder);
                db.AddInParameter(dbCommand, "@CopyShipOrder", DbType.Boolean, iscopyshipment);
                db.AddInParameter(dbCommand, "@CopyBill", DbType.Boolean, iscopybill);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, saveby);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "OceanShippingOrderUpdateDate", "OceanShippingOrderID", "No", "State" });
                return result;

            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }


        #endregion

        #region  获取业务的客服，订舱员，文件的邮件地址

        /// <summary>
        /// 获取业务的客服，订舱员，文件的邮件地址(只针对于变跟订舱的操作使用)
        /// </summary>
        /// <param name="oceanBookid">业务的ID</param>
        /// <param name="userId">当前用户的ID</param>
        /// <param name="logo">0 表示查询业务的操作人的信息，1表示查询商务员的信息</param>
        /// <returns></returns>
        public List<UserInfo> GetOceanOperatorEmailAddress(Guid oceanBookid, Guid userId, int logo)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookid, "oceanBookid");
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanOperatorEmailAddress");
                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, oceanBookid);
                db.AddInParameter(dbCommand, "@Logo", DbType.Int16, logo);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                DataTable dataTable = ds.Tables[0];
                List<UserInfo> userInfos = new List<UserInfo>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo.EName = string.IsNullOrEmpty(dataTable.Rows[i]["EName"].ToString()) ? string.Empty : dataTable.Rows[i]["EName"].ToString();
                    userInfo.EMail = string.IsNullOrEmpty(dataTable.Rows[i]["Email"].ToString()) ? string.Empty : dataTable.Rows[i]["Email"].ToString();
                    Guid id = new Guid(dataTable.Rows[i]["UserID"].ToString());
                    if (id == Guid.Empty)
                    {
                        userInfo.ID = Guid.Empty;
                    }
                    else
                    {
                        userInfo.ID = id;
                    }
                    if (userInfo.ID != userId)
                    {
                        userInfos.Add(userInfo);
                    }
                }
                return userInfos;

            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }


        #endregion

        #region  获得未完成AMS列表

        /// <summary>
       /// 获得未完成AMS列表
       /// </summary>
       /// <param name="userId">用户ID</param>
       /// <returns></returns>
        public List<NotAMSList> GetNotAmsListForUser(Guid userId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetNotAMSListForUser");
                db.AddInParameter(dbCommand, "@UserId", DbType.Guid, userId);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                DataTable dataTable = ds.Tables[0];
                List<NotAMSList> AMSInfos = new List<NotAMSList>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    NotAMSList AMSInfo = new NotAMSList();
                    AMSInfo.ID = (Guid)dataTable.Rows[i]["ID"];
                    AMSInfo.No = string.IsNullOrEmpty(dataTable.Rows[i]["No"].ToString()) ? string.Empty : dataTable.Rows[i]["No"].ToString();
                    AMSInfo.EndAmsDate = Convert.ToDateTime(dataTable.Rows[i]["EndAmsDate"]);
                    AMSInfos.Add(AMSInfo);
                }
                return AMSInfos;

            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }


        #endregion

        #region  更新AMS状态

        /// <summary>
        /// 更新AMS状态
        /// </summary>
        /// <param name="oceanbookingid">海运业务ID</param>
        /// <param name="isAms">是否AMS</param>
        /// <param name="IsEnglish">是否英文</param>
        /// <returns></returns>
        public void ChangeAmsState(Guid oceanbookingid, bool isAms, bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeAmsState");
                db.AddInParameter(dbCommand, "@OceanBookingsID", DbType.Guid, oceanbookingid);
                db.AddInParameter(dbCommand, "@IsAms", DbType.Boolean, isAms);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);
                
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }


        #endregion
    }
}

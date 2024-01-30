using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
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

namespace ICP.FCM.AirExport.ServiceComponent
{
    /// <summary>
    /// Booking服务
    /// </summary>
    partial class AirExportService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="fees"></param>
        /// <param name="delegates"></param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveAirBookingWithTrans(BookingSaveRequest saveRequest,
            List<FeeSaveRequest> fees, List<SaveRequestBookingDelegate> delegates)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                Guid newBookingOrderId = Guid.Empty;

                if (saveRequest != null)
                {
                    result.Add(saveRequest.RequestId,
                        new SaveResponse { RequestId = saveRequest.RequestId, SingleResult = this.SaveAirBookingInfo(saveRequest) });

                    if (saveRequest.id == Guid.Empty)
                    {
                        newBookingOrderId = result[saveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                    }
                }

                if (fees != null)
                {
                    foreach (FeeSaveRequest fee in fees)
                    {
                        if (saveRequest != null && saveRequest.id == Guid.Empty)
                        {
                            fee.orderID = newBookingOrderId;
                            fee.ids = new Guid?[fee.ids.Length];
                        }
                        result.Add(fee.RequestId,
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveAirOrderFeeList(fee) });
                    }
                }
                if (delegates != null)
                {
                    foreach (SaveRequestBookingDelegate bdelegate in delegates)
                    {
                        if (saveRequest != null && saveRequest.id == Guid.Empty)
                        {
                            bdelegate.OperationID = newBookingOrderId;
                            bdelegate.ItemIDs = new Guid?[bdelegate.ItemIDs.Length];
                        }
                        result.Add(bdelegate.RequestId,
                            new SaveResponse
                            {
                                RequestId = bdelegate.RequestId,
                                ManyResult = _FCMCommonService.SaveBookingDelegateList(bdelegate),
                            });
                    }
                }
                scope.Complete();
                return result;
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
        public List<UserInfo> GetFilersList(Guid? customerId, Guid? salesId, DateTime beginTime, DateTime endTime, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirFilerList");

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
        ///// <param name="ctnNo">箱号</param>
        ///// <param name="shippingOrderNo">订舱号</param>
        ///// <param name="scno">合约号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="airlineName">航空公司</param>
        /// <param name="agentOfCarrierName">承运人名</param>
        ///// <param name="vesselName">船名</param>
        /// <param name="flightNo">航班号</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="finalDestination">最终目的地</param>
        /// <param name="salesID">业务员</param>
        /// <param name="bookingerID">订舱</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        public List<AirBookingList> GetAirBookingList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            //string ctnNo,
            //string shippingOrderNo,
            string scno,
            string customerName,
            string airlineName,
            string agentOfCarrierName,
            //string vesselName,
            string flightNo,
            string polName,
            string podName,
            string finalDestination,
            Guid? salesID,
            Guid? bookingerID,
            bool? isValid,
            AEOrderState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBookingList");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                //db.AddInParameter(dbCommand, "@CtnNo", DbType.String, ctnNo);
                //db.AddInParameter(dbCommand, "@ShippingOrderNo", DbType.String, shippingOrderNo);
                //db.AddInParameter(dbCommand, "@Scno", DbType.String, scno);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@AirlineName", DbType.String, airlineName);
                db.AddInParameter(dbCommand, "@AgentOfCarrierName", DbType.String, agentOfCarrierName);
                db.AddInParameter(dbCommand, "@FlightNo", DbType.String, flightNo);
                //db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, finalDestination);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, bookingerID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
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

                List<AirBookingList> results = BulidAirBookingListByDataSet(ds);

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
        public List<AirBookingList> GetAirBookingListByIds(Guid[] bookingIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(bookingIDs, "bookingIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBookingListByIDs");

                string tempBookingIDs = bookingIDs.Join();

                db.AddInParameter(dbCommand, "@BookingIDs", DbType.String, tempBookingIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBookingList> results = BulidAirBookingListByDataSet(ds);

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
        public List<AirBookingList> GetAirBookingListForFaster(
            Guid[] companyIDs,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            DateSearchType dateSearchType,
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBookingListForFaster");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@NoSearchType", DbType.Int16, noSearchType);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerSearchType", DbType.Int16, customerSearchType);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@LocationSearchType", DbType.Int16, portSearchType);
                db.AddInParameter(dbCommand, "@LocationName", DbType.String, portName);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, null);
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

                List<AirBookingList> results = BulidAirBookingListByDataSet(ds);

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

        private static List<AirBookingList> BulidAirBookingListByDataSet(DataSet ds)
        {
            List<AirBookingList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new AirBookingList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  //AirShippingOrderID = b.Field<Guid?>("AirShippingOrderID"),
                                                  State = (AEOrderState)b.Field<byte>("State"),
                                                  No = b.Field<string>("No"),
                                                  MBLNo = b.Field<string>("MAWBNo"),
                                                  HBLNo = b.Field<string>("HAWBNo"),
                                                  //Paid = b.Field<bool>("Paid"),
                                                  //AirShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                                  CustomerName = b.Field<string>("CustomerName"),
                                                  BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                                  AirCompanyName = b.Field<string>("AirlineName"),
                                                  FilightNo = b.Field<string>("FlightNo"),
                                                  AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                  DepartureName = b.Field<string>("POLName"),
                                                  DetinationName = b.Field<string>("PODName"),
                                                  PlaceOfDeliveryName = b.Field<string>("FinalDestinationName"),
                                                  ETD = b.Field<DateTime?>("ETD"),
                                                  ShipperName = b.Field<string>("ShipperName"),
                                                  ConsigneeName = b.Field<string>("ConsigneeName"),
                                                  AgentName = b.Field<string>("AgentName"),
                                                  //OEOperationType = (OEOperationType)b.Field<byte>("OEOperationType"),
                                                  FilerName = b.Field<string>("FilerName"),
                                                  BookingerName = b.Field<string>("BookingerName"),
                                                  SalesName = b.Field<string>("SalesName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  UpdateByName=b.Field<string>("UpdateByName"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  //ContainerNo = b.Field<string>("ContainerNo"),
                                                  BookingDate = b.Field<DateTime>("BookingDate"),
                                                  SODate = b.Field<DateTime?>("SODate"),
                                                  ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                                  CreateByID = b.Field<Guid>("CreateByID"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  //PODContact = b.Field<string>("AgentFilerName"),
                                                  ETA = b.Field<DateTime?>("ETA"),
                                                  IsValid = b.Field<bool>("IsValid"),
                                                  IsDirty = false,
                                                  AirMBLs = (from m in ds.Tables[1].AsEnumerable()
                                                               where b.Field<Guid>("ID") == m.Field<Guid>("AirBookingID")
                                                               select new BookingBLInfo
                                                               {
                                                                   AirBookingID = m.Field<Guid>("AirBookingID"),
                                                                   ID = m.Field<Guid>("ID"),
                                                                   NO = m.Field<string>("No"),
                                                                   State = (AEBLState)m.Field<byte>("State"),
                                                                   UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                               }).ToList(),
                                                  AirHBLs = (from h in ds.Tables[2].AsEnumerable()
                                                               where b.Field<Guid>("ID") == h.Field<Guid>("AirBookingID")
                                                               select new BookingBLInfo
                                                               {
                                                                   AirBookingID = h.Field<Guid>("AirBookingID"),
                                                                   ID = h.Field<Guid>("ID"),
                                                                   NO = h.Field<string>("No"),
                                                                   State = (AEBLState)h.Field<byte>("State"),
                                                                   UpdateDate = h.Field<DateTime?>("UpdateDate"),
                                                               }).ToList(),
                                                 
                                              }).ToList();

            return results;
        }

        /// <summary>
        /// 获取订舱信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回订舱信息</returns>
        public AirBookingInfo GetAirBookingInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBookingInfo");

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

                AirBookingInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new AirBookingInfo
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
                                               WeightUnitID = b.IsNull("WeightUnitID")?Guid.Empty:b.Field<Guid?>("WeightUnitID"),
                                               WeightUnitName =b.Field<string>("WeightUnitName"),
                                               Measurement = b.Field<decimal>("Measurement"),
                                               MeasurementUnitID = b.Field<Guid?>("MeasurementUnitID"),
                                               MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                               CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")),
                                               IsTruck = b.Field<bool>("IsTruck"),
                                               IsCustoms = b.Field<bool>("IsCustoms"),
                                               IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                               IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                               IsOnlyMBL = b.Field<bool>("IsOnlyMBL"),
                                               Remark = b.Field<string>("Remark"),
                                               CreateByName = b.Field<string>("CreateByName"),
                                               SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                               SalesTypeName = b.Field<string>("SalesTypeName"),
                                               AirShippingOrderUpdateDate=b.Field<DateTime?>("AirOrderUpdateDate"),//装货单更新时间
                                               AirShippingOrderID=b.Field<Guid?>("AirwayBillID"),
                                               AgentOfCarrierID =b.IsNull("AgentOfCarrierID")?Guid.Empty:b.Field<Guid>("AgentOfCarrierID"),
                                               AgentID = b.Field<Guid?>("AgentID"),
                                               AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                               ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                               ShippingLineName = b.Field<string>("ShippingLineName"),
                                               IsContract = b.IsNull("IsContract") ? false : b.Field<bool>("IsContract"),
                                               AirCompanyId=b.Field<Guid?>("AirlineID"),
                                               AirCompanyName=b.Field<string>("CarrierName"),
                                               FilightNo = b.Field<string>("FlightNO"),
                                               FilightId = b.Field<Guid?>("FlightId"),
                                               ID = b.Field<Guid>("ID"),
                                               State = (AEOrderState)b.Field<byte>("State"),
                                               No = b.Field<string>("No"),
                                               CustomerName = b.Field<string>("CustomerName"),
                                               AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                               IsWareHouse = b.IsNull("IsWareHouse") ? false : b.Field<bool>("IsWareHouse"),
                                               FilerId = b.Field<Guid?>("FilerId"),
                                               FilerName = b.Field<string>("FilerName"),
                                               ETD = b.Field<DateTime?>("ETD"),
                                               ShipperName = b.Field<string>("ShipperName"),
                                               ConsigneeName = b.Field<string>("ConsigneeName"),
                                               AgentName = b.Field<string>("AgentName"),
                                               BookingerName = b.Field<string>("BookingerName"),
                                               SalesName = b.Field<string>("SalesName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               ContractID = b.Field<Guid?>("ContractID"),
                                               ContractNo = b.Field<string>("ContractNo"),
                                               DOCClosingDate = b.Field<DateTime?>("DOCClosingDate"),
                                               ETA = b.Field<DateTime?>("ETA"),
                                               PODID = b.Field<Guid>("PODID"),
                                               POLID = b.Field<Guid>("POLID"),
                                               DepartureName = b.Field<string>("POLName"),
                                               DetinationName= b.Field<string>("PODName"),
                                               PlaceOfDeliveryName = b.Field<string>("FinalDestinationName"),
                                               PlaceOfDeliveryID = b.Field<Guid>("FinalDestinationID"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               IsDirty = false,
                                               AirMBLs = (from m in ds.Tables[1].AsEnumerable()
                                                            where b.Field<Guid>("ID") == m.Field<Guid>("AirBookingID")
                                                            select new BookingBLInfo
                                                            {
                                                                AirBookingID = m.Field<Guid>("AirBookingID"),
                                                                ID = m.Field<Guid>("ID"),
                                                                NO = m.Field<string>("No"),
                                                                State = (AEBLState)m.Field<byte>("State"),
                                                                UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                            }).ToList(),
                                               AirHBLs = (from h in ds.Tables[2].AsEnumerable()
                                                            where b.Field<Guid>("ID") == h.Field<Guid>("AirBookingID")
                                                            select new BookingBLInfo
                                                            {
                                                                AirBookingID = h.Field<Guid>("AirBookingID"),
                                                                ID = h.Field<Guid>("ID"),
                                                                NO = h.Field<string>("No"),
                                                                State = (AEBLState)h.Field<byte>("State"),
                                                                UpdateDate = h.Field<DateTime?>("UpdateDate"),
                                                            }).ToList(),
                                              
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
        /// 保存订舱信息
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回SingleResultDataWithNo</returns>
        public SingleResult SaveAirBookingInfo(BookingSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.bookingCustomerID.Value, "bookingCustomerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.shipperID.Value, "shipperID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.companyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.salesDepartmentID, "salesDepartmentID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.tradeTermID, "tradeTermID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");

            try
            {
                SingleResult result = null;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAirBookingInfo");

                #region Parameter
                string tempBookingCustomerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.bookingCustomerDescription, true, false);
                string tempShipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.shipperDescription, true, false);
                string tempConsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.consigneeDescription, true, false);
                string tempAgentDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.agentDescription, true, false);
                string tempContainerDescription = (saveRequest.containerDescription == null || saveRequest.containerDescription.Containers.Count == 0) ? null : SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.containerDescription, true, false);
                string tempCargoDescription = SerializerHelper.SerializeToString<CargoDescription>(saveRequest.cargoDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.id);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, saveRequest.customerRefNo);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.customerID);
                db.AddInParameter(dbCommand, "@TradeTermID", DbType.Guid, saveRequest.tradeTermID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.companyID);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, saveRequest.filerID);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, saveRequest.bookingerID);//订舱ID
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
                db.AddInParameter(dbCommand, "@DepartureID", DbType.Guid, saveRequest.polID);
                db.AddInParameter(dbCommand, "@DestinationID", DbType.Guid, saveRequest.podID);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, saveRequest.agentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, tempAgentDescription);//代理描述
                db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, saveRequest.agentOfCarrierID);//承运人
                db.AddInParameter(dbCommand, "@IsContract", DbType.Boolean, saveRequest.isContract);//是否有合约
                db.AddInParameter(dbCommand, "@FreightRateID", DbType.Guid, saveRequest.freightRateID);//合约ID
                db.AddInParameter(dbCommand, "@SODate", DbType.DateTime, saveRequest.soDate);//确认日期
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);
                db.AddInParameter(dbCommand, "@DOCClosingDate", DbType.DateTime, saveRequest.dOCClosingDate);
                db.AddInParameter(dbCommand, "@ClosingDate", DbType.DateTime, saveRequest.closingDate);
                db.AddInParameter(dbCommand, "@EstimatedDeliveryDate", DbType.DateTime, saveRequest.estimatedDeliveryDate);
                db.AddInParameter(dbCommand, "@DevliveryDate", DbType.DateTime, saveRequest.actualDeliveryDate);
                db.AddInParameter(dbCommand, "@ExpectedShipDate", DbType.DateTime, saveRequest.expectedShipDate);
                db.AddInParameter(dbCommand, "@ExpectedArriveDate", DbType.DateTime, saveRequest.expectedArriveDate);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, saveRequest.paymentTermID);//客户付款方式
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, saveRequest.transportClauseID);
                db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, saveRequest.shippingLineID);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.commodity);//品名
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, saveRequest.quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, saveRequest.quantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, saveRequest.weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, saveRequest.weightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, saveRequest.measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, saveRequest.measurementUnitID);
                db.AddInParameter(dbCommand, "@CargoDescription", DbType.Xml, tempCargoDescription);
                db.AddInParameter(dbCommand, "@IsTruck", DbType.Boolean, saveRequest.isTruck);
                db.AddInParameter(dbCommand, "@IsCustoms", DbType.Boolean, saveRequest.isCustoms);
                db.AddInParameter(dbCommand, "@IsCommodityInspection", DbType.Boolean, saveRequest.isCommodityInspection);
                db.AddInParameter(dbCommand, "@IsQuarantineInspection", DbType.Boolean, saveRequest.isQuarantineInspection);
                db.AddInParameter(dbCommand, "@IsWarehouse", DbType.Boolean, saveRequest.isWarehouse);
                db.AddInParameter(dbCommand, "@IsOnlyMBL", DbType.Boolean, saveRequest.isOnlyMBL);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.remark);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.finalDestinationID);//收货地
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@AirwayBillUpdateDate", DbType.DateTime, saveRequest.airShippingOrderUpdateDate);
                db.AddInParameter(dbCommand, "@AirOrderUpdateDate", DbType.DateTime, saveRequest.oceanOrderUpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@AirlineID", DbType.Guid, saveRequest.AirlineID);
                db.AddInParameter(dbCommand, "@FlightID", DbType.Guid, saveRequest.FlightID);
                db.AddInParameter(dbCommand, "@AirwayBillID", DbType.Guid, saveRequest.airWayBillID);//此处待定,不知道@AirwayBillID代表什么 
                #endregion

                result = db.SingleResult(dbCommand, new string[] { "ID", "No", "AirwayBillID", "UpdateDate", "state", "AirwayBillIDUpdateDate" });

#if DEBUG
                if (saveRequest != null && saveRequest.IsSyncCSP && !saveRequest.FlightID.IsNullOrEmpty())
                {
                    SaveRequestShipmentInfoForCSP sqShipmentInfo = new SaveRequestShipmentInfoForCSP()
                    {
                        OperationID = result.GetValue<Guid>("ID"),
                        OperationType = OperationType.AirExport,
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


                //db.AddInParameter(dbCommand, "@BookingType", DbType.Int16, oeOperationType);
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
    }
}

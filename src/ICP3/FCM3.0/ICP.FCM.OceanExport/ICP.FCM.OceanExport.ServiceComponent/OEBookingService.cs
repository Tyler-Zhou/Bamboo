using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface.DataObjects;

using System.Transactions;
using System.Reflection;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    partial class OceanExportService
    {
        public Dictionary<Guid, SaveResponse> SaveOceanBookingWithTrans(BookingSaveRequest saveRequest,
            List<FeeSaveRequest> fees, List<POSaveRequest> pos)
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
                        new SaveResponse { RequestId = saveRequest.RequestId, SingleResult = this.SaveOceanBookingInfo(saveRequest) });

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
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveOceanOrderFeeList(fee) });
                    }
                }

                if (pos != null)
                {
                    foreach (POSaveRequest po in pos)
                    {
                        if (saveRequest != null && saveRequest.id == Guid.Empty)
                        {
                            po.orderID = newBookingOrderId;
                            po.id = Guid.Empty;
                            po.itemIDs = new Guid?[po.itemIDs.Length];
                        }
                        result.Add(po.RequestId,
                            new SaveResponse { RequestId = po.RequestId, HierarchyManyResult = this.SaveOceanOrderPOInfo(po) });
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanFilerList");

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
            string customerName,
            string carrierName,
            string agentOfCarrierName,
            string vesselName,
            string voyageNo,
            string polName,
            string podName,
            string placeOfDeliveryName,
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

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@CtnNo", DbType.String, ctnNo);
                db.AddInParameter(dbCommand, "@ShippingOrderNo", DbType.String, shippingOrderNo);
                db.AddInParameter(dbCommand, "@Scno", DbType.String, scno);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@CarrierName", DbType.String, carrierName);
                db.AddInParameter(dbCommand, "@AgentOfCarrierName", DbType.String, agentOfCarrierName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
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
                                                  IsDirty = false,
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
        /// 获取订舱信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回订舱信息</returns>
        public OceanBookingInfo GetOceanBookingInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
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
                                               CreateByName = b.Field<string>("CreateByName"),
                                               SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                               SalesTypeName = b.Field<string>("SalesTypeName"),
                                               MBLNo = b.Field<string>("MBLNo"),
                                               HBLNo = b.Field<string>("HBLNo"),
                                               //CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                               OceanShippingOrderID = b.Field<Guid?>("OceanShippingOrderID"),
                                               OceanShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                               CarrierID = b.IsNull("CarrierID") ? Guid.Empty : b.Field<Guid>("CarrierID"),
                                               AgentOfCarrierID = b.IsNull("AgentOfCarrierID") ? Guid.Empty : b.Field<Guid>("AgentOfCarrierID"),
                                               AgentID = b.Field<Guid?>("AgentID"),
                                               AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                               ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                               ShippingLineName = b.Field<string>("ShippingLineName"),
                                               IsContract = b.IsNull("IsContract") ? false : b.Field<bool>("IsContract"),
                                               //ConfirmedDate = b.Field<DateTime?>("ConfirmedDate"),
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
                                               //TranshipmentPortName = b.Field<string>("TranshipmentPortName"),
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
        /// <param name="id">ID</param>
        /// <param name="customerRefNo">客户参考号[20]</param>
        /// <param name="customerID">客户</param>
        /// <param name="tradeTermID">贸易条款</param>
        /// <param name="oeOperationType">订舱类型(0:整箱,1:拼箱,2:散货)</param>
        /// <param name="companyID">口岸公司</param>
        /// <param name="bookingerID">订舱</param>
        /// <param name="filerId">文件</param>
        /// <param name="OverSeasFilerId">海外部客服</param>
        /// <param name="salesDepartmentID">业务部门</param>
        /// <param name="salesID">业务员</param>
        /// <param name="salesTypeID">揽货方式</param>
        /// <param name="bookingMode">委托方式</param>
        /// <param name="orderDate">委托时间</param>
        /// <param name="bookingCustomerID">订舱客户</param>
        /// <param name="bookingCustomerDescription">订舱客户描述</param>
        /// <param name="shipperID">发货人</param>
        /// <param name="shipperDescription">发货人描述</param>
        /// <param name="consigneeID">收货人</param>
        /// <param name="consigneeDescription">收货人描述</param>
        /// <param name="placeOfReceiptID">收货地</param>
        /// <param name="polID">装货港</param>
        /// <param name="podID">卸货港</param>
        /// <param name="placeOfDeliveryID">交货地</param>
        /// <param name="agentID">代理</param>
        /// <param name="agentDescription">代理描述</param>
        /// <param name="carrierID">船东</param>
        /// <param name="agentOfCarrierID">承运人</param>
        /// <param name="isContract">是否有合约</param>
        /// <param name="freightRateID">合约ID</param>
        /// <param name="oceanShippingOrderID">订舱ID</param>
        /// <param name="oceanShippingOrderNo">订舱号 OceanShippingOrders.oceanShippingOrderNo[20]</param>
        /// <param name="confirmedDate">订舱确认时间</param>
        /// <param name="estimatedDeliveryDate">估计交货时间</param>
        /// <param name="actualDeliveryDate">实际交货时间</param>
        /// <param name="expectedShipDate">期望出运时间</param>
        /// <param name="expectedArriveDate">期望到达时间</param>
        /// <param name="closingDate">关单时间</param>
        /// <param name="paymentTermID">客户付款方式</param>
        /// <param name="transportClauseID">运输条款</param>
        /// <param name="shippingLineID">航线</param>
        /// <param name="preVoyageID">驳船船名航次</param>
        /// <param name="voyageID">大船船名航次</param>
        /// <param name="commodity">货物品名</param>
        /// <param name="quantity">货物数量</param>
        /// <param name="quantityUnitID">货物数量单位</param>
        /// <param name="weight">货物重量</param>
        /// <param name="weightUnitID">货物重量单位</param>
        /// <param name="measurement">货物体积</param>
        /// <param name="measurementUnitID">货物体积单位</param>
        /// <param name="cargoDescription">货物描述</param>
        /// <param name="containerDescription">箱描述</param>
        /// <param name="mblPaymentTermID">MBL付款方式</param>
        /// <param name="hblPaymentTermID">HBL付款方式</param>
        /// <param name="isTruck">是否需要派车</param>
        /// <param name="isCustoms">是否需要报关</param>
        /// <param name="isCommodityInspection">是否需要商检</param>
        /// <param name="isQuarantineInspection">是否需要质检</param>
        /// <param name="isWarehouse">是否需要仓储</param>
        /// <param name="isOnlyMBL">是否只出MBL</param>
        /// <param name="mblReleaseType">MBL放单类型</param>
        /// <param name="hblReleaseType">HBL放单类型</param>
        /// <param name="mblRequirements">MBL要求</param>
        /// <param name="hblRequirements">HBL要求</param>
        /// <param name="remark">备注</param>
        /// <param name="finalDestinationID">最终目的地要求</param>
        /// <param name="returnLocationID">还柜地点</param>
        /// <param name="warehouseID">仓库</param>
        /// <param name="closingWarehousedate">截仓日</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="oceanShippingOrderUpdateDate">更新时间-做数据版本用</param>
        /// <param name="oceanOrderUpdateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResultDataWithNo</returns>
        public SingleResult SaveOceanBookingInfo(BookingSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.bookingCustomerID, "bookingCustomerID");
            //ArgumentHelper.AssertGuidNotEmpty(saveRequest.shipperID, "shipperID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.companyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.salesDepartmentID, "salesDepartmentID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.tradeTermID, "tradeTermID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanBookingInfo");

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
                db.AddInParameter(dbCommand, "@OEOperationType", DbType.Int16, saveRequest.oeOperationType);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.companyID);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, saveRequest.filerID);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, saveRequest.bookingerID);
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
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@OceanShippingOrderUpdateDate", DbType.DateTime, saveRequest.oceanShippingOrderUpdateDate);
                db.AddInParameter(dbCommand, "@OceanOrderUpdateDate", DbType.DateTime, saveRequest.oceanOrderUpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                // db.AddInParameter(dbCommand, "@ClosingDate", DbType.DateTime, saveRequest.ClosingDate);
                db.AddInParameter(dbCommand, "@CYClosingDate", DbType.DateTime, saveRequest.CYClosingDate);
                db.AddInParameter(dbCommand, "@DOCClosingDate", DbType.DateTime, saveRequest.DOCClosingDate);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);
                db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, saveRequest.PreETD);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "OceanShippingOrderUpdateDate", "OceanShippingOrderID", "No", "State" });
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
    }
}

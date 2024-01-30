using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using System.Transactions;

namespace ICP.FCM.AirExport.ServiceComponent
{
    public partial class AirExportService
    {
        public Dictionary<Guid, SaveResponse> SaveAirOrderWithTrans(OrderSaveRequest saveRequest, List<FeeSaveRequest> fees, List<POSaveRequest> pos)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();


                Guid newOrderId = Guid.Empty;
                if (saveRequest != null)
                {
                    result.Add(saveRequest.RequestId,
                        new SaveResponse { RequestId = saveRequest.RequestId, SingleResult = this.SaveAirOrderInfo(saveRequest) });

                    if (saveRequest.id == Guid.Empty)
                    {
                        newOrderId = result[saveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                    }
                }

                if (fees != null)
                {
                    foreach (FeeSaveRequest fee in fees)
                    {
                        if (saveRequest != null && saveRequest.id == Guid.Empty)
                        {
                            fee.orderID = newOrderId;
                            fee.ids = new Guid?[fee.ids.Length];
                        }
                        result.Add(fee.RequestId,
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveAirOrderFeeList(fee) });
                    }
                }

                if (pos != null)
                {
                    foreach (POSaveRequest po in pos)
                    {
                        if (saveRequest != null && saveRequest.id == Guid.Empty)
                        {
                            po.orderID = newOrderId;
                            po.id = Guid.Empty;
                            po.itemIDs = new Guid?[po.itemIDs.Length];
                        }
                        result.Add(po.RequestId,
                            new SaveResponse { RequestId = po.RequestId, HierarchyManyResult = this.SaveAirOrderPOInfo(po) });
                    }
                }

                scope.Complete();
                return result;
            }
        }

        /// <summary>
        /// 根据口岸公司和客户获取最近该客户的业务数据列表
        /// </summary>
        /// <param name="companyID">口岸公司</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回最近该客户的业务数据列表</returns>
        public List<AirOrderList> GetRecentlyAirOrderList(
            Guid companyID,
            Guid customerID,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetRecentlyAirOrderList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirOrderList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new AirOrderList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  State = (AEOrderState)b.Field<byte>("State"),
                                                  RefNo = b.Field<string>("RefNo"),
                                                  CustomerName = b.Field<string>("CustomerName"),
                                                  ShipperName = b.Field<string>("ShipperName"),
                                                  ConsigneeName = b.Field<string>("ConsigneeName"),
                                                  PlaceOfReceiptName = b.Field<string>("FinalDestinationName"),
                                                  POLName = b.Field<string>("POLName"),
                                                  ETD = b.Field<DateTime?>("ETD"),
                                                  PODName = b.Field<string>("PODName"),
                                                  ETA = b.Field<DateTime?>("ETA"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  //PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                                  //OEOperationType = (OEOperationType)b.Field<byte>("OEOperationType"),
                                                  SalesName = b.Field<string>("SalesName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  //ClosingDate = b.Field<DateTime?>("ClosingDate"),

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
        /// 获取订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="no">业务号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地名</param>
        /// <param name="carrierName">航空公司</param>
        /// <param name="orderState">订单状态()</param>
        /// <param name="salesID">业务员</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订单列表</returns>
        public List<AirOrderList> GetAirOrderList(
            Guid[] companyIDs,
            string no,
            string customerName,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string carrierName,
            bool? isValid,
            AEOrderState? orderState,
            Guid? salesID,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderList");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
                db.AddInParameter(dbCommand, "@AirlineName", DbType.String, carrierName);
                db.AddInParameter(dbCommand, "@OrderState", DbType.Int16, orderState);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.Date, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.Date, endTime);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<AirOrderList> results = BulidOrderListByDataSet(ds);

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
        /// 获取订单列表
        /// </summary>
        /// <param name="orderIDs">orderIDs</param>
        /// <returns>返回订单列表</returns>
        public List<AirOrderList> GetAirOrderListById(
            Guid[] orderIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderIDs, "orderIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderListByIDs");

                string tempOrderIDs = orderIDs.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempOrderIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirOrderList> results = BulidOrderListByDataSet(ds);

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
        /// 获取订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="noSearchType">单号搜索类型(0:全部,1:业务号,2:提单号,3:箱号,4:订舱号,5:合约号)</param>
        /// <param name="no">号码</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部,1:客户,2:船东,3:承运人,4:发货人,5:收货人,6:通知人,7:对单人)</param>
        /// <param name="customerName">客户名</param>
        /// <param name="portSearchType">港口搜索类型(0:全部,1:收货地,2:装货港,3:卸货港,4:交货地,5:最终目的地)</param>
        /// <param name="portName">港口名</param>
        /// <param name="dateSearchType">日期搜索类型(0:全部,1:离港日,2:到港日,3:订舱日,4:创建日,)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订单列表</returns>
        public List<AirOrderList> GetAirOrderListForFaster(
            Guid[] companyIDs,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            DateSearchType dateSearchType,
            Guid salesId,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(salesId, "salesId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderListForFaster");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@NoSearchType", DbType.Int16, noSearchType);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerSearchType", DbType.Int16, customerSearchType);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@LocationSearchType", DbType.Int16, portSearchType);
                db.AddInParameter(dbCommand, "@LocationName", DbType.String, portName);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesId);
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

                List<AirOrderList> results = BulidOrderListByDataSet(ds);

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

        private List<AirOrderList> BulidOrderListByDataSet(DataSet ds)
        {
            List<AirOrderList> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new AirOrderList
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              State = (AEOrderState)b.Field<byte>("State"),
                                              RefNo = b.Field<string>("No"),
                                              CustomerName = b.Field<string>("CustomerName"),
                                              ShipperName = b.Field<string>("ShipperName"),
                                              ConsigneeName = b.Field<string>("ConsigneeName"),
                                              POLName = b.Field<string>("POLName"),
                                              ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                              ETD = b.Field<DateTime?>("ETD"),
                                              PODName = b.Field<string>("PODName"),
                                              ETA = b.Field<DateTime?>("ETA"),
                                              SODate = b.Field<DateTime?>("SODate"),
                                              PlaceOfReceiptName = b.Field<string>("DestinationName"),
                                              //OEOperationType = (OEOperationType)b.Field<byte>("OEOperationType"),
                                              SalesName = b.Field<string>("SalesName"),
                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                              IsValid = b.Field<bool>("IsValid"),
                                              UpdateByName = b.Field<string>("UpdateByName"),
                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                              BookingerName = b.Field<string>("BookingerName"),
                                              Filer = b.Field<string>("FilerName"),
                                              //Reason = b.Field<string>("Reason"),

                                              IsDirty = false
                                          }).ToList();
            return results;
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回订单信息</returns>
        public AirOrderInfo GetAirOrderInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderInfo");

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
                    throw new Exception("Found more than 1 orders. It's impossible!");
                }

                AirOrderInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new AirOrderInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           RefNo = b.Field<string>("RefNo"),
                                           BookingDate = b.Field<DateTime>("BookingDate"),
                                           //SODate = b.Field<DateTime?>("SODate"),
                                           //Orderdate = b.Field<DateTime>("Orderdate"),
                                           TradeTermID = b.Field<Guid>("TradeTermID"),
                                           TradeTermName = b.Field<string>("TradeTermName"),
                                           CompanyID = b.Field<Guid>("CompanyID"),
                                           CompanyName = b.Field<string>("CompanyName"),
                                           SalesID = b.Field<Guid?>("SalesID"),
                                           SalesDepartmentID = b.Field<Guid>("SalesDepartmentID"),
                                           SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                           SalesTypeName = b.Field<string>("SalesTypeName"),
                                           SalesDepartmentName = b.Field<string>("SalesDepartmentName"),
                                           BookingerID = b.Field<Guid?>("BookingerID"),
                                           BookingerName = b.Field<string>("BookingerName"),
                                           OverSeasFilerId = b.Field<Guid?>("FilerId"),
                                           OverSeasFilerName = b.Field<string>("FilerName"),
                                           CustomerID = b.Field<Guid>("CustomerID"),
                                           BookingCustomerID = b.Field<Guid>("BookingCustomerID"),
                                           BookingCustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("BookingCustomerDescription")),
                                           BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                           ShipperID = b.Field<Guid>("ShipperID"),
                                           ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                           ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                           ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                           PlaceOfReceiptID = b.Field<Guid>("FinalDestinationId"),
                                           PlaceOfReceiptName = b.Field<string>("FinalDestinationName"),
                                           //FinalDestinationId = b.Column<Guid>("FinalDestinationId", true),
                                           //FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                           POLID = b.Field<Guid>("POLID"),
                                           PODID = b.Field<Guid>("PODID"),
                                           //PlaceOfDeliveryID = b.Field<Guid>("PlaceOfDeliveryID"),
                                           //PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                           CarrierID = b.Field<Guid?>("AirlineID"),
                                           CarrierName = b.Field<string>("AirlineName"),
                                           ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                           EstimatedDeliveryDate = b.Field<DateTime?>("EstimatedDeliveryDate"),
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
                                           //ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                           IsTruck = b.Field<bool>("IsTruck"),
                                           IsCustoms = b.Field<bool>("IsCustoms"),
                                           IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                           IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                           IsWarehouse = b.Field<bool>("IsWarehouse"),
                                           IsOnlyMBL = b.Field<bool>("IsOnlyMBL"),
                                           //MBLPaymentTermID = b.Field<Guid?>("MBLPaymentTermID"),
                                           //MBLPaymentTermName = b.Field<string>("MBLPaymentTermName"),
                                           //MBLReleaseType = (ReleaseType)b.Field<byte>("MBLReleaseType"),
                                           //MBLRequirements = b.Field<string>("MBLRequirements"),
                                           //HBLPaymentTermID = b.Field<Guid?>("HBLPaymentTermID"),
                                           //HBLPaymentTermName = b.Field<string>("HBLPaymentTermName"),
                                           //HBLReleaseType = b.IsNull("HBLReleaseType") ? (ReleaseType?)null : (ReleaseType)b.Field<byte>("HBLReleaseType"),
                                           //HBLRequirements = b.Field<string>("HBLRequirements"),
                                           Remark = b.Field<string>("Remark"),
                                           State = (AEOrderState)b.Field<byte>("State"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CustomerName = b.Field<string>("CustomerName"),
                                           ShipperName = b.Field<string>("ShipperName"),
                                           ConsigneeName = b.Field<string>("ConsigneeName"),
                                           POLName = b.Field<string>("POLName"),
                                           PODName = b.Field<string>("PODName"),
                                           //CargoType = (CargoType)b.Field<byte)("CargoType"),
                                           //OEOperationType = (OEOperationType)b.Field<byte>("OEOperationType"),
                                           BookingMode = (FCMBookingMode)b.Field<byte>("BookingMode"),
                                           ExpectedShipDate = b.Field<DateTime?>("ExpectedShipDate"),
                                           SalesName = b.Field<string>("SalesName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           AgentName = b.Column<string>("AgentName"),
                                           FilerId = b.Column<Guid?>("FilerId"),
                                           FilerName = b.Column<string>("FilerName"),
                                           IsDirty = false


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
        /// 保存订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="refNo">业务号</param>
        /// <param name="companyID">口岸公司</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="bookingCustomerID">订舱客户</param>
        /// <param name="bookingCustomerDescription">订舱客户描述</param>
        /// <param name="oeOperationType">订舱类型</param>
        /// <param name="operatorID">操作</param>
        /// <param name="salesDepartmentID">业务部门</param>
        /// <param name="salesID">业务员</param>
        /// <param name="salesTypeID">揽货方式</param>
        /// <param name="transportClauseID">运输条款</param>
        /// <param name="shipperID">发货人</param>
        /// <param name="shipperDescription">发货人描述</param>
        /// <param name="consigneeID">收货人</param>
        /// <param name="consigneeDescription">收货人描述</param>
        /// <param name="carrierID">承运人</param>
        /// <param name="placeOfReceiptID">收货地</param>
        /// <param name="polID">装货港</param>
        /// <param name="podID">卸货港</param>
        /// <param name="placeOfDeliveryID">交货地</param>
        /// <param name="tradeTermID">贸易条款</param>
        /// <param name="closingDate">截关时间</param>
        /// <param name="estimatedDeliveryDate">估计交货</param>
        /// <param name="expectedShipDate">期望出运时间</param>
        /// <param name="expectedArriveDate">期望到达时间</param>
        /// <param name="mblpaymentTermID">MBL付款方式</param>
        /// <param name="hblpaymentTermID">HBL付款方式</param>
        /// <param name="paymentTermID">客户付款方式</param>
        /// <param name="isTruck">是否派车</param>
        /// <param name="isCustoms">是否报关</param>
        /// <param name="isCommodityInspection">是否商检</param>
        /// <param name="isQuarantineInspection">是否质检</param>
        /// <param name="isWarehouse">是否仓储</param>
        /// <param name="isOnlyMBL">是否只出MBL</param>
        /// <param name="mblReleaseType">MBL放单类型</param>
        /// <param name="hblReleaseType">HBL放单类型</param>
        /// <param name="mblRequirements">MBL要求</param>
        /// <param name="hblRequirements">HBL要求</param>
        /// <param name="containerDescription">想描述</param>
        /// <param name="commodity">品名</param>
        /// <param name="quantity">货物数量</param>
        /// <param name="quantityUnitID">货物数量单位</param>
        /// <param name="weight">货物重量</param>
        /// <param name="weightUnitID">货物重量单位</param>
        /// <param name="measurement">货物体积</param>
        /// <param name="measurementUnitID">货物体积单位</param>
        /// <param name="cargoDescription">货物描述</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="bookingMode">委托方式/订舱方式</param>
        /// <param name="finalDestinationID">最终目的地</param>
        /// <param name="orderDate">委托日期</param>
        /// <param name="overseasOperatorId">海外部客服</param>
        /// <returns>返回SinglieResultData</returns>
        public SingleResult SaveAirOrderInfo(OrderSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.bookingCustomerID, "bookingCustomerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.salesTypeID, "salesTypeID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.tradeTermID, "tradeTermID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.transportClauseID, "transportClauseID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.shipperID, "shipperID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.podID, "podID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.polID, "polID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.companyID, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAirOrderInfo");

                string tempbookingCustomerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.bookingCustomerDescription, true, false);
                string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.shipperDescription, true, false);
                string tempconsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.consigneeDescription, true, false);
                string tempcontainerDescription = (saveRequest.containerDescription == null || saveRequest.containerDescription.Containers.Count == 0) ? null : SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.containerDescription, true, false);
                string tempcargoDescription = SerializerHelper.SerializeToString<CargoDescription>(saveRequest.cargoDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.id);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.refNo);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.customerID);
                db.AddInParameter(dbCommand, "@BookingCustomerID", DbType.Guid, saveRequest.bookingCustomerID);
                db.AddInParameter(dbCommand, "@BookingCustomerDescription", DbType.Xml, tempbookingCustomerDescription);
                //db.AddInParameter(dbCommand, "@OEOperationType", DbType.Byte, saveRequest.oeOperationType);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, saveRequest.bookingerID);
                db.AddInParameter(dbCommand, "@SalesDepartmentID", DbType.Guid, saveRequest.salesDepartmentID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, saveRequest.salesID);
                db.AddInParameter(dbCommand, "@SalesTypeID", DbType.Guid, saveRequest.salesTypeID);
                //db.AddInParameter(dbCommand, "@State", DbType.Int32, saveRequest.State);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, saveRequest.transportClauseID);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, saveRequest.shipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, saveRequest.consigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempconsigneeDescription);
                db.AddInParameter(dbCommand, "@AirlineID", DbType.Guid, saveRequest.carrierID);
                //db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, saveRequest.placeOfReceiptID);
                db.AddInParameter(dbCommand, "@DepartureID", DbType.Guid, saveRequest.polID);//起运港
                db.AddInParameter(dbCommand, "@DestinationID", DbType.Guid, saveRequest.podID);//目的港
                //db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, saveRequest.placeOfDeliveryID);
                db.AddInParameter(dbCommand, "@TradeTermID", DbType.Guid, saveRequest.tradeTermID);
                db.AddInParameter(dbCommand, "@EstimatedDeliveryDate", DbType.DateTime, saveRequest.estimatedDeliveryDate);
                db.AddInParameter(dbCommand, "@ExpectedShipDate", DbType.DateTime, saveRequest.expectedShipDate);
                db.AddInParameter(dbCommand, "@ExpectedArriveDate", DbType.DateTime, saveRequest.expectedArriveDate);
                //db.AddInParameter(dbCommand, "@MblPaymentTermID", DbType.Guid, saveRequest.mblpaymentTermID);
                //db.AddInParameter(dbCommand, "@HblPaymentTermID", DbType.Guid, saveRequest.hblpaymentTermID);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, saveRequest.paymentTermID);
                db.AddInParameter(dbCommand, "@IsTruck", DbType.Boolean, saveRequest.isTruck);
                db.AddInParameter(dbCommand, "@IsCustoms", DbType.Boolean, saveRequest.isCustoms);
                db.AddInParameter(dbCommand, "@IsCommodityInspection", DbType.Boolean, saveRequest.isCommodityInspection);
                db.AddInParameter(dbCommand, "@IsQuarantineInspection", DbType.Boolean, saveRequest.isQuarantineInspection);
                db.AddInParameter(dbCommand, "@IsWarehouse", DbType.Boolean, saveRequest.isWarehouse);
                db.AddInParameter(dbCommand, "@IsOnlyMBL", DbType.Boolean, saveRequest.isOnlyMBL);


                //db.AddInParameter(dbCommand, "@Isvalid", DbType.Boolean, saveRequest.isValid);

                //db.AddInParameter(dbCommand, "@MBLReleaseType", DbType.Int16, saveRequest.mblReleaseType);
                //db.AddInParameter(dbCommand, "@HBLReleaseType", DbType.Int16, saveRequest.hblReleaseType);
                //db.AddInParameter(dbCommand, "@MBLRequirements", DbType.String, saveRequest.mblRequirements);
                //db.AddInParameter(dbCommand, "@HBLRequirements", DbType.String, saveRequest.hblRequirements);
                //db.AddInParameter(dbCommand, "@ContainerDescription", DbType.Xml, tempcontainerDescription);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.commodity);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, saveRequest.quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, saveRequest.quantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, saveRequest.weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, saveRequest.weightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, saveRequest.measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, saveRequest.measurementUnitID);
                db.AddInParameter(dbCommand, "@CargoDescription", DbType.Xml, tempcargoDescription);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@BookingMode", DbType.Byte, saveRequest.bookingMode);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.finalDestinationID);//交货地
                db.AddInParameter(dbCommand, "@BookingDate", DbType.DateTime, saveRequest.bookingDate);
                //db.AddInParameter(dbCommand, "@OverSeasFilerId", DbType.Guid, saveRequest.overSeasFilerId);//客服
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "NO", "UpdateDate", "State" });
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
        /// 改变订单状态        
        /// </summary>
        /// <param name="orderID">委托ID</param>
        /// <param name="isValid">是否取消订舱</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeAirOrderState(
            Guid orderID,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeAirOrderState");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "State", "UpdateDate" });
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
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回PO列表</returns>
        public List<AirBookingPOList> GetAirOrderPOList(Guid orderID)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderPOList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBookingPOList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new AirBookingPOList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      RelationID = b.Field<Guid>("RelationID"),
                                                      OwnerID = b.Field<Guid>("OwnerID"),
                                                      No = b.Field<string>("No"),
                                                      PODescription = b.Field<string>("PODescription"),
                                                      VendorID = b.Field<Guid?>("VendorID"),
                                                      VendorName = b.Field<string>("VendorName"),
                                                      BuyerID = b.Field<Guid?>("BuyerID"),
                                                      BuyerName = b.Field<string>("BuyerName"),
                                                      FinalDestination = b.Field<string>("FinalDestination"),
                                                      InWarehouseDate = b.Field<DateTime?>("InWarehouseDate"),
                                                      OrderDate = b.Field<DateTime?>("OrderDate"),
                                                      State = (FCMPOState)b.Field<byte>("State"),
                                                      CreateByID = b.Field<Guid>("CreateByID"),
                                                      CreateByName = b.Field<string>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      IsDirty = false,
                                                      Items = (from m in ds.Tables[1].AsEnumerable()
                                                               where m.Field<Guid>("POID") == b.Field<Guid>("ID")
                                                               select new AirPOItemList
                                                               {
                                                                   ID = m.Field<Guid>("ID"),
                                                                   RelationID = m.Field<Guid>("RelationID"),
                                                                   POID = m.Field<Guid>("POID"),
                                                                   No = m.Field<string>("No"),
                                                                   Description = m.Field<string>("Description"),
                                                                   Color = m.Field<string>("Color"),
                                                                   Size = m.Field<string>("Size"),
                                                                   Volume = m.Field<decimal>("Volume"),
                                                                   Weight = m.Field<decimal>("Weight"),
                                                                   Cartons = m.Field<int>("Cartons"),
                                                                   Units = m.Field<int>("Units"),
                                                                   HTSCode = m.Field<string>("HTSCode"),
                                                                   CreateByID = m.Field<Guid>("CreateByID"),
                                                                   CreateByName = m.Field<string>("CreateByName"),
                                                                   CreateDate = m.Field<DateTime>("CreateDate"),
                                                                   UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                                   IsDirty = false,
                                                                   // Cartons 
                                                               }).ToList(),
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
        /// 保存PO信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="poID">PO ID</param>
        /// <param name="orderID">订单ID</param>
        /// <param name="no">PO号</param>
        /// <param name="podc">订单描述</param>
        /// <param name="vendorID">卖主</param>
        /// <param name="vendor">卖主描述</param>
        /// <param name="buyerID">买主</param>
        /// <param name="buyer">买主描述</param>
        /// <param name="finalDestination">最终目的地</param>
        /// <param name="inWarehouseDate">入仓时间</param>
        /// <param name="orderDate">处理时间</param>
        /// <param name="updateDate">数据版本</param>
        /// <param name="itemIDs">ItemID列表</param>
        /// <param name="itemNos">Item号列表</param>
        /// <param name="itemDescriptions">Item描述列表</param>
        /// <param name="itemColors">Item颜色列表</param>
        /// <param name="itemSizes">Item尺寸列表</param>
        /// <param name="itemVolumes">Item体积列表</param>
        /// <param name="itemWeights">Item重量列表</param>
        /// <param name="itemCartons">Item装箱数量列表</param>
        /// <param name="itemUnits">Item件数列表</param>
        /// <param name="itemHTSCodes">Item海关编码列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="itemUpdateDates">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public HierarchyManyResult SaveAirOrderPOInfo(POSaveRequest request
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(request.orderID, "orderID");
            ArgumentHelper.AssertGuidNotEmpty(request.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 request.itemIDs,
                 request.itemNos,
                 request.itemDescriptions,
                 request.itemColors,
                 request.itemSizes,
                 request.itemVolumes,
                 request.itemWeights,
                 request.itemCartons,
                 request.itemUnits,
                 request.itemHTSCodes);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAirOrderPOInfo");

                string tempItemIDs = request.itemIDs.Join();
                string tempItemNos = request.itemNos.Join();
                string tempItemDescriptions = request.itemDescriptions.Join();
                string tempItemColors = request.itemColors.Join();
                string tempItemSizes = request.itemSizes.Join();
                string tempItemVolumes = request.itemVolumes.Join(3);
                string tempItemWeights = request.itemWeights.Join(3);
                string tempItemCartons = request.itemCartons.Join();
                string tempItemUnits = request.itemUnits.Join();
                string tempItemHTSCodes = request.itemHTSCodes.Join();
                string tempItemUpdateDates = request.itemUpdateDates.Join();

                db.AddInParameter(dbCommand, "@POID", DbType.Guid, request.id);
                db.AddInParameter(dbCommand, "@RelationID", DbType.Guid, request.relationID);
                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, request.orderID);
                db.AddInParameter(dbCommand, "@No", DbType.String, request.no);
                db.AddInParameter(dbCommand, "@Podc", DbType.String, request.podc);
                db.AddInParameter(dbCommand, "@VendorID", DbType.Guid, request.vendorID);
                db.AddInParameter(dbCommand, "@Vendor", DbType.String, request.vendor);
                db.AddInParameter(dbCommand, "@BuyerID", DbType.Guid, request.buyerID);
                db.AddInParameter(dbCommand, "@Buyer", DbType.String, request.buyer);
                db.AddInParameter(dbCommand, "@FinalDestination", DbType.String, request.finalDestination);
                db.AddInParameter(dbCommand, "@InWarehouseDate", DbType.DateTime, request.inWarehouseDate);
                db.AddInParameter(dbCommand, "@OrderDate", DbType.DateTime, request.orderDate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, request.updateDate);
                db.AddInParameter(dbCommand, "@ItemIDs", DbType.String, tempItemIDs);
                db.AddInParameter(dbCommand, "@ItemNos", DbType.String, tempItemNos);
                db.AddInParameter(dbCommand, "@ItemDescriptions", DbType.String, tempItemDescriptions);
                db.AddInParameter(dbCommand, "@ItemColors", DbType.String, tempItemColors);
                db.AddInParameter(dbCommand, "@ItemSizes", DbType.String, tempItemSizes);
                db.AddInParameter(dbCommand, "@ItemVolumes", DbType.String, tempItemVolumes);
                db.AddInParameter(dbCommand, "@ItemWeights", DbType.String, tempItemWeights);
                db.AddInParameter(dbCommand, "@ItemCartons", DbType.String, tempItemCartons);
                db.AddInParameter(dbCommand, "@ItemUnits", DbType.String, tempItemUnits);
                db.AddInParameter(dbCommand, "@ItemHTSCodes", DbType.String, tempItemHTSCodes);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, request.saveByID);
                db.AddInParameter(dbCommand, "@ItemUpdateDates", DbType.String, tempItemUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "RelationID", "POID", "UpdateDate" }, new string[] { "RelationID", "POID", "ItemID", "UpdateDate" } });
                if (results == null
                    || results.Length < 2
                    || results[0].Items.Count == 0)
                {
                    return null;
                }

                HierarchyManyResult result = new HierarchyManyResult(results[0].Items[0]);
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

        /// <summary>
        /// 改变PO状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="state">状态（0待处理、1已确认、2全部发货、3部分发货、4取消订单）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeAirOrderPOState(
            Guid id,
            FCMPOState state,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeAirOrderPOState");

                db.AddInParameter(dbCommand, "@POID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
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

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveAirOrderPOInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "ID");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAirOrderPOInfo");

                string tempUpdateDates = updateDate == null ? string.Empty : updateDate.Value.ToString();

                db.AddInParameter(dbCommand, "@ID", DbType.String, id.ToString());
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="isValid">是否取消(true为取消,false为激活)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult CancelAirOrder(
            Guid orderID,
            bool isCancel,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "ID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCancelAirOrder");

                db.AddInParameter(dbCommand, "@OrderID", DbType.String, orderID.ToString());
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);

                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);

                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
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

        /// <summary>
        /// 改变订单状态        
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="state">状态(2:已打回,3:已订舱,4:已放单,5:已关单)</param>
        /// <param name="reason">原因</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeAirOrderStateWithTargetState(
            Guid Id,
            AEOrderState state,
            string reason,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(Id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeAirOrderState");

                db.AddInParameter(dbCommand, "@OrderId", DbType.Guid, Id);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@Reason", DbType.String, reason);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "State", "UpdateDate" });
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
    }
}

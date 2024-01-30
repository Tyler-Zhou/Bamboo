using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace ICP.FCM.DomesticTrade.ServiceComponent
{
    partial class DomesticTradeService
    {
        #region 以事务方式保存订单、费用
        /// <summary>
        /// 以事务方式保存订单、费用
        /// </summary>
        /// <param name="saveRequest">OrderSaveRequest</param>
        /// <param name="pos">FeeSaveRequest</param>
        /// <param name="fees">POSaveRequest</param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveDTOrderWithTrans(OrderSaveRequest saveRequest, List<FeeSaveRequest> fees)
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
                        new SaveResponse { RequestId = saveRequest.RequestId, SingleResult = this.SaveDTOrderInfo(saveRequest) });

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
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveDTOrderFeeList(fee) });
                    }
                }


                scope.Complete();
                return result;
            }
        } 
        #endregion

        #region 根据口岸公司和客户获取最近该客户的业务数据列表
        /// <summary>
        /// 根据口岸公司和客户获取最近该客户的业务数据列表
        /// </summary>
        /// <param name="companyID">口岸公司</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回最近该客户的业务数据列表</returns>
        public List<DTOrderList> GetRecentlyDTOrderList(
            Guid companyID,
            Guid customerID,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetRecentlyOceanOrderList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DTOrderList> results = (from b in ds.Tables[0].AsEnumerable()
                                             select new DTOrderList
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 State = (DTOrderState)b.Field<byte>("State"),
                                                 RefNo = b.Field<string>("RefNo"),
                                                 CustomerName = b.Field<string>("CustomerName"),
                                                 ShipperName = b.Field<string>("ShipperName"),
                                                 ConsigneeName = b.Field<string>("ConsigneeName"),
                                                 PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                                 POLName = b.Field<string>("POLName"),
                                                 ETD = b.Field<DateTime?>("ETD"),
                                                 PODName = b.Field<string>("PODName"),
                                                 ETA = b.Field<DateTime?>("ETA"),
                                                 UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                 PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                                 DTOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                                 SalesName = b.Field<string>("SalesName"),
                                                 CreateDate = b.Field<DateTime>("CreateDate"),
                                                 ClosingDate = b.Field<DateTime?>("ClosingDate"),
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
        #endregion

        #region 获取订单列表
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="no">业务号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地名</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="orderState">订单状态()</param>
        /// <param name="salesID">业务员</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订单列表</returns>
        public List<DTOrderList> GetDTOrderList(
            Guid[] companyIDs,
            string no,
            string customerName,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string carrierName,
            bool? isValid,
            DTOrderState? orderState,
            Guid? salesID,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                DataSet dsResult = null;
                DataSet dds = null;
                DataSet sds = null;
                bool? isDefaultDB = CompanyHelper.IsDefaultServer(companyIDs, FrameworkInitializeService);

                dds = GetDTOrderDataSet(companyIDs, no, customerName, polName, podName,
                    placeOfDeliveryName, carrierName, isValid, orderState, salesID, dateSearchType,
                    beginTime, endTime, maxRecords,true);
                sds = GetDTOrderDataSet(companyIDs, no, customerName, polName, podName,
                    placeOfDeliveryName, carrierName, isValid, orderState, salesID, dateSearchType,
                    beginTime, endTime, maxRecords,false);
                dsResult = DataSetHelper.MergeSet(dds, sds);
                if (dsResult == null || dsResult.Tables.Count < 1)
                {
                    return null;
                }
                List<DTOrderList> results = BulidOrderListByDataSet(dsResult);

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

        private DataSet GetDTOrderDataSet(Guid[] companyIDs,
            string no,
            string customerName,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string carrierName,
            bool? isValid,
            DTOrderState? orderState,
            Guid? salesID,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords, bool isDefaultDB)
        {
            Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalOrderList");

            string tempCompanyIDs = companyIDs.Join();

            db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
            db.AddInParameter(dbCommand, "@No", DbType.String, no);
            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
            db.AddInParameter(dbCommand, "@POLName", DbType.String, polName);
            db.AddInParameter(dbCommand, "@PODName", DbType.String, podName);
            db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
            db.AddInParameter(dbCommand, "@CarrierName", DbType.String, carrierName);
            db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
            db.AddInParameter(dbCommand, "@OrderState", DbType.Int16, orderState);
            db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
            db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
            db.AddInParameter(dbCommand, "@BeginTime", DbType.Date, beginTime);
            db.AddInParameter(dbCommand, "@EndTime", DbType.Date, endTime);
            db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            return ds;
        }
        #endregion

        #region 刷新订单列表
        /// <summary>
        /// 刷新订单列表
        /// </summary>
        /// <param name="orderIDs">orderIDs</param>
        /// <returns>返回订单列表</returns>
        public List<DTOrderList> GetDTOrderListByIds(
            Guid[] orderIDs, Guid[] companyIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderIDs, "orderIDs");

            try
            {
                DataSet dsResult = null;
                DataSet dds = null;
                DataSet sds = null;
                bool? isDefaultDB = CompanyHelper.IsDefaultServer(companyIDs, FrameworkInitializeService);
                dds = GetDTOrderDataSetByIds(orderIDs, isDefaultDB == false ? null : (bool?)true);
                sds = GetDTOrderDataSetByIds(orderIDs, isDefaultDB == true ? null : (bool?)false);
                dsResult = DataSetHelper.MergeSet(dds, sds);
                if (dsResult == null || dsResult.Tables.Count < 1)
                {
                    return null;
                }

                List<DTOrderList> results = BulidOrderListByDataSet(dsResult);

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

        private DataSet GetDTOrderDataSetByIds(Guid[] orderIDs, bool? isDefaultDB)
        {
            if (isDefaultDB == null)
                return null;
            Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB.Value);
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalOrderListByIDs");

            string tempOrderIDs = orderIDs.Join();

            db.AddInParameter(dbCommand, "@IDs", DbType.String, tempOrderIDs);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            return null;
        }
        #endregion

        #region 快速搜索订单列表
        /// <summary>
        /// 快速搜索订单列表
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
        public List<DTOrderList> GetDTOrderListForFaster(
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
                DataSet dsResult = null;
                DataSet dds = null;
                DataSet sds = null;

                dds = GetDTOrderDataSetForFaster(companyIDs, noSearchType, no, customerSearchType, customerName, portSearchType, portName, dateSearchType, salesId, beginTime, endTime, isValid, maxRecords, true);
                sds = GetDTOrderDataSetForFaster(companyIDs, noSearchType, no, customerSearchType, customerName, portSearchType, portName, dateSearchType, salesId, beginTime, endTime, isValid, maxRecords, false);
                dsResult = DataSetHelper.MergeSet(dds, sds);
                if (dsResult == null || dsResult.Tables.Count < 1)
                {
                    return null;
                }

                List<DTOrderList> results = BulidOrderListByDataSet(dsResult);

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

        private DataSet GetDTOrderDataSetForFaster(Guid[] companyIDs,
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
            int maxRecords, bool isDefaultDB)
        {
            Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalOrderListForFaster");

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
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            return ds;
        }
        #endregion

        private List<DTOrderList> BulidOrderListByDataSet(DataSet ds)
        {
            List<DTOrderList> results = (from b in ds.Tables[0].AsEnumerable()
                                         select new DTOrderList
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             State = (DTOrderState)b.Field<byte>("State"),
                                             RefNo = b.Field<string>("No"),
                                             CustomerName = b.Field<string>("CustomerName"),
                                             ShipperName = b.Field<string>("ShipperName"),
                                             ConsigneeName = b.Field<string>("ConsigneeName"),
                                             PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                             POLName = b.Field<string>("POLName"),
                                             //ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                             ETD = b.Field<DateTime?>("ETD"),
                                             PODName = b.Field<string>("PODName"),
                                             ETA = b.Field<DateTime?>("ETA"),
                                             SODate = b.Field<DateTime?>("SODate"),
                                             PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                             DTOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                             SalesName = b.Field<string>("SalesName"),
                                             CreateDate = b.Field<DateTime>("CreateDate"),
                                             IsValid = b.Field<bool>("IsValid"),
                                             UpdateByName = b.Field<string>("UpdateByName"),
                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                             BookingerName = b.Field<string>("BookingerName"),
                                             Filer = b.Field<string>("FilerName"),
                                             Reason = b.Field<string>("Reason"),
                                             CompanyID = b.Field<Guid>("CompanyID"),
                                             IsDirty = false
                                         }).ToList();
            return results;
        }

        #region 获取订单信息
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回订单信息</returns>
        public DTOrderInfo GetDTOrderInfo(Guid id, Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalOrderInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

                DTOrderInfo result = (from b in ds.Tables[0].AsEnumerable()
                                      select new DTOrderInfo
                                      {
                                          ID = b.Field<Guid>("ID"),
                                          RefNo = b.Field<string>("RefNo"),
                                          BookingDate = b.Field<DateTime>("BookingDate"),
                                          SODate = b.Field<DateTime?>("SODate"),
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
                                          PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                          PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                          POLID = b.Field<Guid>("POLID"),
                                          PODID = b.Field<Guid>("PODID"),
                                          PlaceOfDeliveryID = b.Field<Guid>("PlaceOfDeliveryID"),
                                          PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                          CarrierID = b.Field<Guid?>("CarrierID"),
                                          CarrierName = b.Field<string>("CarrierName"),
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
                                          ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                          IsTruck = b.Field<bool>("IsTruck"),
                                          IsWarehouse = b.Field<bool>("IsWarehouse"),
                                          Remark = b.Field<string>("Remark"),
                                          State = (DTOrderState)b.Field<byte>("State"),
                                          CreateByID = b.Field<Guid>("CreateByID"),
                                          CreateByName = b.Field<string>("CreateByName"),
                                          CustomerName = b.Field<string>("CustomerName"),
                                          ShipperName = b.Field<string>("ShipperName"),
                                          ConsigneeName = b.Field<string>("ConsigneeName"),
                                          POLName = b.Field<string>("POLName"),
                                          PODName = b.Field<string>("PODName"),
                                          DTOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                          BookingMode = (FCMBookingMode)b.Field<byte>("BookingMode"),
                                          ExpectedShipDate = b.Field<DateTime?>("ExpectedShipDate"),
                                          SalesName = b.Field<string>("SalesName"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          IsValid = b.Field<bool>("IsValid"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                          AgentName = b.Column<string>("AgentName"),
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
        #endregion

        #region 保存订单信息
        /// <summary>
        /// 保存订单信息
        /// </summary>
        /// <param name="saveRequest">ID</param>
        /// <returns>返回SinglieResultData</returns>
        public SingleResult SaveDTOrderInfo(OrderSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.bookingCustomerID, "bookingCustomerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.salesTypeID, "salesTypeID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.transportClauseID, "transportClauseID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.shipperID, "shipperID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.podID, "podID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.polID, "polID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.companyID, "companyID");

            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(saveRequest.companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveInternalOrderInfo");

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
                db.AddInParameter(dbCommand, "@InOperationType", DbType.Byte, saveRequest.dtOperationType);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, saveRequest.bookingerID);
                db.AddInParameter(dbCommand, "@SalesDepartmentID", DbType.Guid, saveRequest.salesDepartmentID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, saveRequest.salesID);
                db.AddInParameter(dbCommand, "@SalesTypeID", DbType.Guid, saveRequest.salesTypeID);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, saveRequest.transportClauseID);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, saveRequest.shipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, saveRequest.consigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempconsigneeDescription);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, saveRequest.carrierID);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, saveRequest.placeOfReceiptID);
                db.AddInParameter(dbCommand, "@PolID", DbType.Guid, saveRequest.polID);
                db.AddInParameter(dbCommand, "@PodID", DbType.Guid, saveRequest.podID);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, saveRequest.placeOfDeliveryID);
                //db.AddInParameter(dbCommand, "@TradeTermID", DbType.Guid, saveRequest.tradeTermID);
                //db.AddInParameter(dbCommand, "@ClosingDate", DbType.DateTime, saveRequest.closingDate);
                db.AddInParameter(dbCommand, "@EstimatedDeliveryDate", DbType.DateTime, saveRequest.estimatedDeliveryDate);
                db.AddInParameter(dbCommand, "@ExpectedShipDate", DbType.DateTime, saveRequest.expectedShipDate);
                db.AddInParameter(dbCommand, "@ExpectedArriveDate", DbType.DateTime, saveRequest.expectedArriveDate);
                //db.AddInParameter(dbCommand, "@MblPaymentTermID", DbType.Guid, saveRequest.mblpaymentTermID);
                //db.AddInParameter(dbCommand, "@HblPaymentTermID", DbType.Guid, saveRequest.hblpaymentTermID);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, saveRequest.paymentTermID);
                db.AddInParameter(dbCommand, "@IsTruck", DbType.Boolean, saveRequest.isTruck);
                //db.AddInParameter(dbCommand, "@IsCustoms", DbType.Boolean, saveRequest.isCustoms);
                //.AddInParameter(dbCommand, "@IsCommodityInspection", DbType.Boolean, saveRequest.isCommodityInspection);
                //db.AddInParameter(dbCommand, "@IsQuarantineInspection", DbType.Boolean, saveRequest.isQuarantineInspection);
                db.AddInParameter(dbCommand, "@IsWarehouse", DbType.Boolean, saveRequest.isWarehouse);
                //db.AddInParameter(dbCommand, "@IsOnlyMBL", DbType.Boolean, saveRequest.isOnlyMBL);
                //db.AddInParameter(dbCommand, "@MBLReleaseType", DbType.Int16,saveRequest.mblReleaseType);
                //db.AddInParameter(dbCommand, "@HBLReleaseType", DbType.Int16,saveRequest.hblReleaseType);
                //db.AddInParameter(dbCommand, "@MBLRequirements", DbType.String, saveRequest.mblRequirements);
                //db.AddInParameter(dbCommand, "@HBLRequirements", DbType.String, saveRequest.hblRequirements);
                db.AddInParameter(dbCommand, "@ContainerDescription", DbType.Xml, tempcontainerDescription);
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
                //db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.finalDestinationID);
                db.AddInParameter(dbCommand, "@BookingDate", DbType.DateTime, saveRequest.bookingDate);
                //db.AddInParameter(dbCommand, "@OverSeasFilerId", DbType.Guid, saveRequest.overSeasFilerId);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        #endregion

        #region 改变订单状态
        /// <summary>
        /// 改变订单状态        
        /// </summary>
        /// <param name="orderID">委托ID</param>
        /// <param name="isValid">是否取消订舱</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeDTOrderState(
            Guid orderID, Guid companyID,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeInternalOrderState");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        #endregion

        #region 取消订单
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="isValid">是否取消(true为取消,false为激活)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult CancelDTOrder(
            Guid orderID, Guid companyID,
            bool isCancel,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "ID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "removeByID");

            try
            {
                Database db = null;
                if (companyID == Guid.Empty)
                {
                    db = DatabaseFactory.CreateDatabase();
                }
                else
                {
                    bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                    db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                }
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCancelInternalOrder");

                db.AddInParameter(dbCommand, "@OrderID", DbType.String, orderID.ToString());
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);

                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);

                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
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
        #endregion

        #region 改变订单状态
        /// <summary>
        /// 改变订单状态        
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="state">状态(2:已打回,3:已订舱,4:已放单,5:已关单)</param>
        /// <param name="reason">原因</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeDTOrderStateWithTargetState(
            Guid Id, Guid companyID,
            DTOrderState state,
            string reason,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(Id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = null;
                if (companyID == Guid.Empty)
                {
                    db = DatabaseFactory.CreateDatabase();
                }
                else
                {
                    bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                    db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                }
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeInternalOrderState");

                db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, Id);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@Reason", DbType.String, reason);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        #endregion
    }
}

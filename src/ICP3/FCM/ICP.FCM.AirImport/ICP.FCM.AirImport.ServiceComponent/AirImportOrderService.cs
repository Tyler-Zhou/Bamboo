using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirImport.ServiceInterface;
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

namespace ICP.FCM.AirImport.ServiceComponent
{
    partial class AirImportService
    {
        #region 以事务的方式保存订单、费用、PO

        /// <summary>
        /// 以事务方式保存订单、费用和PO
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="pos"></param>
        /// <param name="fees"></param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveAIOrderWithTrans(
                                          OrderSaveRequest orderSaveRequest,
                                          List<FeeSaveRequest> feeList)
        {

            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                if (orderSaveRequest != null)
                {
                    result.Add(orderSaveRequest.RequestId,
                        new SaveResponse { RequestId = orderSaveRequest.RequestId, SingleResult = this.SaveAIOrderInfo(orderSaveRequest) });
                }

                Guid newOrderID = Guid.Empty;

                if (orderSaveRequest.ID == Guid.Empty)
                {
                    newOrderID = result[orderSaveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                }

                if (feeList != null)
                {
                    foreach (FeeSaveRequest fee in feeList)
                    {
                        if (orderSaveRequest.ID == Guid.Empty)
                        {
                            fee.orderID = newOrderID;
                            fee.ids = new Guid?[fee.ids.Length];
                        }
                        result.Add(fee.RequestId,
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveAIOrderFeeList(fee) });
                    }
                }

                //if (poList != null)
                //{
                //    foreach (POSaveRequest po in poList)
                //    {
                //        if (orderSaveRequest.ID == Guid.Empty)
                //        {
                //            po.orderID = newOrderID;
                //            po.id = Guid.Empty;
                //            po.itemIDs = new Guid?[po.itemIDs.Length];
                //        }
                //        result.Add(po.RequestId,
                //            new SaveResponse { RequestId = po.RequestId, HierarchyManyResult = this.SaveOIOrderPOInfo(po) });
                //    }
                //}


                scope.Complete();
                return result;

            }
        }

        #endregion

        #region 刷新订单列表
        /// <summary>
        /// 刷新订单列表
        /// </summary>
        /// <param name="orderIds">订单ID集合</param>
        /// <returns></returns>
        public List<AirOrderList> GetAIOrderListByIds(Guid[] orderIds,Guid [] companyIDs)
        {
            try
            {
                DataSet dsResult = null;
                DataSet dds = null;
                DataSet sds = null;
                bool? isDefaultDB = CompanyHelper.IsDefaultServer(companyIDs, FrameworkInitializeService);
                dds = GetAIOrderDataSetByIds(orderIds, isDefaultDB == false ? null : (bool?)true);
                sds = GetAIOrderDataSetByIds(orderIds, isDefaultDB == true ? null : (bool?)false);
                dsResult = DataSetHelper.MergeSet(dds, sds);
                if (dsResult == null || dsResult.Tables.Count < 1)
                {
                    return null;
                }

                return GetOrderList(dsResult);
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

        private DataSet GetAIOrderDataSetByIds(Guid[] orderIds,bool? isDefaultDB)
        {
            if (isDefaultDB == null)
                return null;
            Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB.Value);
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIOrderListByIds");

            string ids = orderIds.Join();

            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            return ds;
        }
        #endregion

        #region 从快速查询

        /// <summary>
        /// 获得订单列表--从快速查询
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="noSearchType">单号查询类型(0:全部、1:业务号)</param>
        /// <param name="no">单号</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部、1:客户、2:船公司、3:承运人、4:发货人、5:收货人、6:通知人、7:对单人、8代理:)</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="portSearchType">地点搜索类型(0:全部、1:收货地、2:装货港、3:中转港、4:卸货港、5:交货地、6:最终目的地)</param>
        /// <param name="portName">地点名称</param>
        /// <param name="dateSearchType">时间搜索类型(0:全部、1:创建日、2:放单日、3:离港日、4:、到港日)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        public List<AirOrderList> GetAIOrderListByFastSearch(
                                            Guid[] companyIDs,
                                            Guid salesID,
                                            OIBusinessNoSearchType noSearchType,
                                            string no,
                                            OIBusinessCustomerSearchType customerSearchType,
                                            string customerName,
                                            OIBusinessPortSearchType portSearchType,
                                            string portName,
                                            OIBusinessDateSearchType dateSearchType,
                                            DateTime? beginTime,
                                            DateTime? endTime,
                                            int maxRecords)
        {
            try
            {
                List<AirOrderList> dsResult = new List<AirOrderList>();
                DataSet dds = null;
                DataSet sds = null;
                dds = GetAIOrderDataSetByFastSearch(companyIDs, salesID, noSearchType, no, customerSearchType, customerName, portSearchType, portName, dateSearchType, beginTime, endTime, maxRecords, true);
                sds = GetAIOrderDataSetByFastSearch(companyIDs, salesID, noSearchType, no, customerSearchType, customerName, portSearchType, portName, dateSearchType, beginTime, endTime, maxRecords, false);
                //dsResult = DataSetHelper.MergeSet(dds, sds);
                //if (dsResult == null || dsResult.Tables.Count < 1)
                //{
                //    return null;
                //}
                List<AirOrderList> ddlist = GetOrderList(dds);
                List<AirOrderList> sdlist = GetOrderList(sds);
                ddlist.ForEach(r => {
                    if (dsResult.Count(j => j.ID == r.ID) == 0)
                    {
                        dsResult.Add(r);
                    }
                });
                sdlist.ForEach(r =>
                {
                    if (dsResult.Count(j => j.ID == r.ID) == 0)
                    {
                        dsResult.Add(r);
                    }
                });

                return dsResult;
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

        private DataSet GetAIOrderDataSetByFastSearch(Guid[] companyIDs,
                                            Guid salesID,
                                            OIBusinessNoSearchType noSearchType,
                                            string no,
                                            OIBusinessCustomerSearchType customerSearchType,
                                            string customerName,
                                            OIBusinessPortSearchType portSearchType,
                                            string portName,
                                            OIBusinessDateSearchType dateSearchType,
                                            DateTime? beginTime,
                                            DateTime? endTime,
                                            int maxRecords, bool isDefaultDB)
        {
            Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIOrderListForFaster");

            string companyIDList = companyIDs.Join();

            db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDList);
            db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
            db.AddInParameter(dbCommand, "@NoSearchType", DbType.Byte, noSearchType);
            db.AddInParameter(dbCommand, "@No", DbType.String, no);
            db.AddInParameter(dbCommand, "@CustomerSearchType", DbType.Byte, customerSearchType);
            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
            db.AddInParameter(dbCommand, "@LocationSearchType", DbType.Byte, portSearchType);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, portName);
            db.AddInParameter(dbCommand, "@DateSearchType", DbType.Byte, dateSearchType);
            db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            return ds;
        }
        #endregion

        #region 从查询界面
        /// <summary>
        /// 获得订单列表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="no">业务号</param>
        /// <param name="agentName">代理名称</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="carrierName">船公司</param>
        /// <param name="pol">装货港</param>
        /// <param name="pod">卸货港</param>
        /// <param name="placeOfDelivery">交货地</param>
        /// <param name="salesID">揽货人</param>
        /// <param name="state">状态搜索类型(1:新订单、2:已打回、3:已审核、4:已取消、5: 已订舱、6:已装船、7:已发送到港通知、8:已放货、9:已关单)</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <param name="dateSearchType">时间搜索类型(0:全部、1:创建日、2:放单日、3:离港日、4:、到港日)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<AirOrderList> GetAIOrderList(Guid[] companyIDs,
                                            string no,
                                            string agentName,
                                            string customerName,
                                            string carrierName,
                                            string pol,
                                            string pod,
                                            string placeOfDelivery,
                                            Guid? salesID,
                                            AIOrderState? state,
                                            bool? isValid,
                                            OIBusinessDateSearchType dateSearchType,
                                            DateTime? beginTime,
                                            DateTime? endTime,
                                            int maxRecords)
        {
            try
            {

                DataSet dsResult = null;
                DataSet dds = null;
                DataSet sds = null;
                dds = GetAIOrderDataSet(companyIDs, no, agentName, customerName, carrierName, pol, pod, placeOfDelivery, salesID, state, isValid, dateSearchType, beginTime, endTime, maxRecords, true);
                sds = GetAIOrderDataSet(companyIDs, no, agentName, customerName, carrierName, pol, pod, placeOfDelivery, salesID, state, isValid, dateSearchType, beginTime, endTime, maxRecords, false);
                dsResult = DataSetHelper.MergeSet(dds, sds);
                if (dsResult == null || dsResult.Tables.Count < 1)
                {
                    return null;
                }
                return GetOrderList(dsResult);
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

        private DataSet GetAIOrderDataSet(Guid[] companyIDs,
                                            string no,
                                            string agentName,
                                            string customerName,
                                            string carrierName,
                                            string pol,
                                            string pod,
                                            string placeOfDelivery,
                                            Guid? salesID,
                                            AIOrderState? state,
                                            bool? isValid,
                                            OIBusinessDateSearchType dateSearchType,
                                            DateTime? beginTime,
                                            DateTime? endTime,
                                            int maxRecords,
                                            bool isDefaultDB)
        {
            Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIOrderList");

            string companyIDList = companyIDs.Join();

            db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDList);
            db.AddInParameter(dbCommand, "@No", DbType.String, no);
            db.AddInParameter(dbCommand, "@AgentName", DbType.String, agentName);
            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
            db.AddInParameter(dbCommand, "@AirlineName", DbType.String, carrierName);
            db.AddInParameter(dbCommand, "@PolName", DbType.String, pol);
            db.AddInParameter(dbCommand, "@PodName", DbType.String, pod);
            db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDelivery);
            db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
            db.AddInParameter(dbCommand, "@OrderState", DbType.Byte, state);
            db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
            db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
            db.AddInParameter(dbCommand, "@DateSearchType", DbType.Byte, dateSearchType);
            db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            return ds;
        }
        #endregion

        #region 转换列表数据
        public List<AirOrderList> GetOrderList(DataSet ds)
        {
            try
            {
                List<AirOrderList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new AirOrderList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  State = (AIOrderState)b.Field<Byte>("State"),
                                                  RefNo = b.Field<String>("REFNO"),
                                                  AgentName = b.Field<String>("AgentName"),
                                                  CustomerName = b.Field<String>("CustomerName"),
                                                  AirCompanyName = b.Field<String>("AirlineName"),
                                                  ShipperName = b.Field<String>("ShipperName"),
                                                  POLName = b.Field<String>("DepartrueName"),
                                                  PODName = b.Field<String>("DestinationName"),
                                                  PlaceOfDeliveryName = b.Field<String>("FinalDestinationName"),
                                                  ConsigneeName = b.Field<String>("ConsigneeName"),
                                                  SalesName = b.Field<String>("SalesName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  ArriveDate = b.Field<DateTime?>("FETA"),
                                                  DeliveryOfGoodsDate = b.Field<DateTime?>("ReleaseDate"),
                                                  ETD = b.Field<DateTime?>("ETD"),
                                                  ETA = b.Field<DateTime?>("ETA"),
                                                  //CustomerContactID = b.Field<Guid?>("FilerId"),
                                                  //CustomerContactName = b.Field<String>("FilerName"),
                                                  CustomerContactName = b.Field<String>("CustomerServiceName"),
                                                  IsValid = b.Field<Boolean>("IsValid"),
                                                  UpdateByName = b.Field<String>("UpdateByName"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  //CreateByName = b.Field<String>("CreateByName"),
                                                  //CreateByID = b.Field<Guid>("CreateByID"),
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

        #region 获得订单的详细信息

        /// <summary>
        /// 获得订单详细信息
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public AirOrderInfo GetAIOrderInfo(Guid id,Guid companyID)
        {
            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIOrderInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AirOrderInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new AirOrderInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           RefNo = b.Field<String>("RefNo"),
                                           BookingDate = b.Field<DateTime>("BookingDate"),
                                           BookingMode = (FCMBookingMode)b.Field<Byte>("BookingMode"),
                                           AgentID = b.Field<Guid?>("AgentID"),
                                           AgentName = b.Field<String>("AgentName"),
                                           AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                           TradeTermID = b.Field<Guid>("TradeTermID"),
                                           TradeTermName = b.Field<String>("TradeTermName"),
                                           CompanyID = b.Field<Guid>("CompanyID"),
                                           CompanyName = b.Field<String>("CompanyName"),
                                           SalesID = b.Field<Guid?>("SalesID"),
                                           SalesDepartmentID = b.Field<Guid?>("SalesDepartmentID"),
                                           SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                           SalesTypeName = b.Field<String>("SalesTypeName"),
                                           SalesDepartmentName = b.Field<String>("SalesDepartmentName"),
                                           CustomerContactID = b.Field<Guid?>("FilerId"),
                                           CustomerContactName = b.Field<String>("FilerName"),
                                           CustomerID = b.Field<Guid>("CustomerID"),
                                           CustomerName = b.Field<String>("CustomerName"),
                                           ShipperID = b.Field<Guid?>("ShipperID"),
                                           ShipperName = b.Field<String>("ShipperName"),
                                           ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                           ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                           ConsigneeName = b.Field<String>("ConsigneeName"),
                                           ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                           POLID = b.Field<Guid?>("POLID"),
                                           PODID = b.Field<Guid?>("PODID"),
                                           PlaceOfDeliveryID = b.Field<Guid?>("FinalDestinationID"),
                                           AirCompanyID = b.Field<Guid?>("AirlineID"),
                                           AirCompanyName = b.Field<String>("AirlineName"),
                                           ETD = b.Field<DateTime?>("ETD"),
                                           ETA = b.Field<DateTime?>("ETA"),
                                           ExpectedShipDate = b.Field<DateTime?>("ExpectedShipDate"),
                                           ExpectedArriveDate = b.Field<DateTime?>("ExpectedArriveDate"),
                                           TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                           TransportClauseName = b.Field<String>("TransportClauseName"),
                                           PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                           PaymentTermName = b.Field<String>("PaymentTermName"),
                                           Commodity = b.Field<String>("Commodity"),
                                           Quantity = b.Field<Int32>("Quantity"),
                                           QuantityUnitID = b.Field<Guid?>("QuantityUnitID"),
                                           QuantityUnitName = b.Field<String>("QuantityUnitName"),
                                           Weight = b.Field<Decimal>("Weight"),
                                           WeightUnitID = b.Field<Guid?>("WeightUnitID"),
                                           WeightUnitName = b.Field<String>("WeightUnitName"),
                                           Measurement = b.Field<Decimal>("Measurement"),
                                           MeasurementUnitID = b.Field<Guid?>("MeasurementUnitID"),
                                           MeasurementUnitName = b.Field<String>("MeasurementUnitName"),
                                           CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")),
                                           //ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                           IsTruck = b.Field<Boolean>("IsTruck"),
                                           IsCustoms = b.Field<Boolean>("IsCustoms"),
                                           IsCommodityInspection = b.Field<Boolean>("IsCommodityInspection"),
                                           IsQuarantineInspection = b.Field<Boolean>("IsQuarantineInspection"),
                                           IsWarehouse = b.Field<Boolean>("IsWarehouse"),
                                           HBLReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                           Remark = b.Field<String>("Remark"),
                                           State = (AIOrderState)b.Field<Byte>("State"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<String>("CreateByName"),
                                           POLName = b.Field<String>("POLName"),
                                           PODName = b.Field<String>("PODName"),
                                           PlaceOfDeliveryName = b.Field<String>("FinalDestinationName"),
                                           SalesName = b.Field<String>("SalesName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           ArriveDate = b.Field<DateTime?>("FETA"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           IsValid = b.Field<Boolean>("IsValid"),
                                           IsDirty = false,
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

        #region 取消订单
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="isCancel">是否取消(True为取消;Flase为激活)</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns></returns>
        public SingleResult CancelAIOrder(
               Guid orderID, Guid companyID,
               bool isCancel,
               Guid changeByID,
               DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "ID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "ChangeByID");

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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCancelAIOrder");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

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

        #region 保存
        /// <summary>
        /// 保存订单信息
        /// </summary>
        /// <returns></returns>
        public SingleResult SaveAIOrderInfo(OrderSaveRequest saveRequest)
        {
            try
            {

                string customerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.CustomerDescription, true, false);
                string shipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ShipperDescription, true, false);
                string consigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ConsigneeDescription, true, false);
                string agentDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.AgentDescription, true, false);
                string cargoDescription = SerializerHelper.SerializeToString<CargoDescription>(saveRequest.CargoDescription, true, false);

                bool isDefaultDB = CompanyHelper.IsDefaultServer(saveRequest.CompanyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAIOrderInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.RefNo);
                db.AddInParameter(dbCommand, "@Companyid", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.CustomerID);
                db.AddInParameter(dbCommand, "@CustomerDescription", DbType.Xml, customerDescription);
                db.AddInParameter(dbCommand, "@TradetermID", DbType.Guid, saveRequest.TradeTermID);
                db.AddInParameter(dbCommand, "@SalestypeID", DbType.Guid, saveRequest.SalesTypeID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, saveRequest.SalesID);
                db.AddInParameter(dbCommand, "@SalesDepartmentID", DbType.Guid, saveRequest.SalesDepartmentID);
                db.AddInParameter(dbCommand, "@TransportclauseID", DbType.Guid, saveRequest.TransportClauseID);
                db.AddInParameter(dbCommand, "@PaymenttermID", DbType.Guid, saveRequest.PaymentTermID);
                db.AddInParameter(dbCommand, "@FilerId", DbType.Guid, saveRequest.FilerID);
                db.AddInParameter(dbCommand, "@BookingMode", DbType.Byte, saveRequest.BookingMode);
                db.AddInParameter(dbCommand, "@Bookingdate", DbType.DateTime, saveRequest.BookingDate);
                db.AddInParameter(dbCommand, "@Agentid", DbType.Guid, saveRequest.AgentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, agentDescription);
                db.AddInParameter(dbCommand, "@AirlineID", DbType.Guid, saveRequest.AirCompanyID);
                db.AddInParameter(dbCommand, "@Cargodescription", DbType.Xml, cargoDescription);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, saveRequest.ShipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, shipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, saveRequest.ConsigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, consigneeDescription);
                db.AddInParameter(dbCommand, "@PolID", DbType.Guid, saveRequest.POLID);
                db.AddInParameter(dbCommand, "@PodID", DbType.Guid, saveRequest.PODID);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.PlaceOfDeliveryID);
                db.AddInParameter(dbCommand, "@ExpectedshipDate", DbType.DateTime, saveRequest.ExpectedShipDate);
                db.AddInParameter(dbCommand, "@ExpectedArriveDate", DbType.DateTime, saveRequest.ExpectedArriveDate);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Byte, saveRequest.HBLReleaseType);
                db.AddInParameter(dbCommand, "@IsWareHouse", DbType.Boolean, saveRequest.IsWarehouse);
                db.AddInParameter(dbCommand, "@IsCustoms", DbType.Boolean, saveRequest.IsCustoms);
                db.AddInParameter(dbCommand, "@IsTruck", DbType.Boolean, saveRequest.IsTruck);
                db.AddInParameter(dbCommand, "@IsCommodityInspection", DbType.Boolean, saveRequest.IsCommodityInspection);
                db.AddInParameter(dbCommand, "@IsQuarantineInspection", DbType.Boolean, saveRequest.IsQuarantineInspection);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.Commodity);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, saveRequest.Quantity);
                db.AddInParameter(dbCommand, "@QuantityunitID", DbType.Guid, saveRequest.QuantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, saveRequest.Weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, saveRequest.WeightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, saveRequest.Measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, saveRequest.MeasurementUnitID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "State" });
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
        /// <param name="state">状态类型(1:新订单、2:已打回、3:已审核、4:已取消、5: 已订舱、6:已装船、7:已发送到港通知、8:已放货、9:已关单)</param>
        /// <param name="memoContent">备注内容</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResultData ChangeAIOrderState(
             Guid orderID, Guid companyID,
             AIOrderState state,
             string memoContent,
             Guid changeByID,
             DateTime? updateDate)
        {
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeAIOrderState");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@MemoContent", DbType.String, memoContent);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #region 获得客户最近的海外部客服列表
        /// <summary>
        /// 获取当前客户最近的海外部客服列表
        /// 如果当前客户为空，就返回揽货人最近业务所对应的海外部客服的列表。
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="salesId">揽货人ID</param>
        ///<param name="beginTime">开始时间</param>
        ///<param name="endTime">结束时间</param>
        ///<param name="maxRecords">最大行数</param>
        /// <returns>用户列表</returns>
        public List<UserInfo> GetAIOverseasFilersList(Guid? customerId, Guid? salesId, DateTime? beginTime, DateTime? endTime, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIOverseasFilersList");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesId);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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
                                              EName = b.Field<String>("EName"),
                                              CName = b.Field<String>("CName"),
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

        #region 判断客户与公司是否在同一国家
        /// <summary>
        /// 判断客户和公司是否在同一个国家
        /// </summary>
        /// <param name="customerId">客户ID( 用cusomterId获得客户的国家ID)</param>
        /// <param name="companyId">口岸公司ID(compnyId用于在获取配置里对应的客户的国家ID)</param>
        /// <returns>是否在同一个国家</returns>
        public bool IsCustomerAndCompanySameCountry(Guid customerId, Guid companyId)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerId, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspIsCustomerAndCompanySameCountry");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return false;
                }

                bool isSame = ds.Tables[0].Rows[0][0].ToString() == "1";
                return isSame;
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

        #region 根据销售客户和口岸公司获得揽货方式
        /// <summary>
        /// 根据销售客户和口岸公司获取揽货方式
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyId">口岸公司</param>
        /// <returns>返回揽货方式</returns>
        public ICP.Common.ServiceInterface.DataObjects.DataDictionaryInfo GetSalesType(
            Guid customerID,
            Guid companyId)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetSalesType");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DataDictionaryInfo result = (from b in ds.Tables[0].AsEnumerable()
                                             select new ICP.Common.ServiceInterface.DataObjects.DataDictionaryInfo
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 EName = b.Field<string>("EName"),
                                                 CName = b.Field<string>("CName")
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

        #region 根据口岸公司和客户获得该客户最近的业务数据
        /// <summary>
        /// 根据口岸公司和客户获取最近该客户的业务数据列表
        /// </summary>
        /// <param name="companyID">口岸公司</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回最近该客户的业务数据列表</returns>
        public List<AirOrderInfo> GetAIRecentlyOrderList(
            Guid companyID,
            Guid customerID,
            Guid salesID,
            int maxRecords)
        {
            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIRecentlyOrderList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirOrderInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new AirOrderInfo
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  State = (AIOrderState)b.Field<Byte>("State"),
                                                  RefNo = b.Field<String>("RefNo"),
                                                  CustomerName = b.Field<String>("CustomerName"),
                                                  ShipperName = b.Field<String>("ShipperName"),
                                                  ConsigneeName = b.Field<String>("ConsigneeName"),
                                                  POLName = b.Field<String>("POLName"),
                                                  ETD = b.Field<DateTime?>("ETD"),
                                                  PODName = b.Field<String>("PODName"),
                                                  ETA = b.Field<DateTime?>("ETA"),
                                                  PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                                  BookingDate = b.Field<DateTime>("BookingDate"),
                                                  AirCompanyID = b.Field<Guid?>("AirCompanyID"),
                                                  ExpectedShipDate = b.Field<DateTime?>("ExpectedShipDate"),
                                                  SalesName = b.Field<String>("SalesName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  CustomerContactName = b.Field<String>("FilerName"),
                                                  CustomerContactID = b.Field<Guid?>("FilerId"),
                                                  Reason = b.Field<String>("Reason"),
                                                  WareHouseID = b.Field<Guid?>("WareHouseID"),
                                                  WareHouseName = b.Field<String>("WareHouseName"),
                                                  CustomsID = b.Field<Guid?>("CustomsBrokerID"),
                                                  CustomsName = b.Field<String>("CustomsBrokerName"),
                                                  IsValid = b.Field<Boolean>("IsValid"),
                                                  CreateByID = b.Field<Guid>("CreateByID"),
                                                  CreateByName = b.Field<String>("CreateByName"),
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

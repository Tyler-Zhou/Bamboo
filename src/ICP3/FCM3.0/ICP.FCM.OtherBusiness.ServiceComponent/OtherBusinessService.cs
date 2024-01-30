using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using System.Transactions;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
namespace ICP.FCM.OtherBusiness.ServiceComponent
{
    partial class OtherBusinessService
    {
        /// <summary>
        /// 获取业务列表
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
        public List<OtherBusinessList> GetOtherBusinessListForFaster(
            Guid[] companyIDs,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.NoSearchType noSearchType,
            string no,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.CustomerSearchType customerSearchType,
            string customerName,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.PortSearchType portSearchType,
            string portName,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.DateSearchType dateSearchType,
            Guid salesId,
            Guid? OperatorID,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords,
            bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(salesId, "salesId");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOtherOrderListForFaster");

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
                db.AddInParameter(dbCommand, "@OperatorID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OtherBusinessList> results = BulidOrderListByDataSet(ds);

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
        /// 获取其他业务列表
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
        /// <param name="ContainerNo">箱号</param>
        /// <param name="MBLNo">MBL</param>
        /// <param name="HBLNo">HBL</param>
        /// <param name="Consignee">发货人</param>
        /// <param name="Shipper">收货人</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订单列表</returns>
        public List<OtherBusinessList> GetOtherBusinessList(
            Guid[] companyIDs,
            Guid[] tempIDs,
            string no,
            string MBLNo,
            string HBLNo,
            string ContainerNo,
            string Consignee,
            string Shipper,
            string NotifyPart,
            string customerName,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string VesselName,
            string VoyageNo,
            bool? isValid,
            OBOrderState? orderState,
            Guid? salesID,
            Guid? overseasFilerID,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords,
            bool isEnglish)
        {
            //ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOtherOrderList");

                //string tempCompanyIDs = companyIDs.Join();
                //string tempDepartIDs = tempsID.Join();
                string tempCompanyIDs = companyIDs.Join();
                string tempDepartIDs = tempIDs.Join();//待之后处理/(订单/其他业务）

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@SalesDepartMentIDS", DbType.String, tempDepartIDs);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@MBLNo", DbType.String, MBLNo);
                db.AddInParameter(dbCommand, "@HBLNo", DbType.String, HBLNo);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, ContainerNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@Consignee", DbType.String, Consignee);
                db.AddInParameter(dbCommand, "@Shipper", DbType.String, Shipper);
                db.AddInParameter(dbCommand, "@NotifyPart", DbType.String, NotifyPart);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, VesselName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, VoyageNo);
                db.AddInParameter(dbCommand, "@Pol", DbType.String, polName);
                db.AddInParameter(dbCommand, "@Pod", DbType.String, podName);
                db.AddInParameter(dbCommand, "@FinalDestination", DbType.String, placeOfDeliveryName);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, orderState);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@OverseasFilerID", DbType.Guid, overseasFilerID);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.Date, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.Date, endTime);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<OtherBusinessList> results = BulidOrderListByDataSet(ds);

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
        /// 获取其他业务列表（刷新）
        /// </summary>
        /// <param name="orderIDs">orderIDs</param>
        /// <returns>返回订单列表</returns>
        public List<OtherBusinessList> GetOtherBusinessListById(
            Guid[] orderIDs, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderIDs, "orderIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOTOrderListByIds");

                string tempOrderIDs = orderIDs.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempOrderIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OtherBusinessList> results = BulidOrderListByDataSet(ds);

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

        private List<OtherBusinessList> BulidOrderListByDataSet(DataSet ds)
        {
            try
            {
                List<OtherBusinessList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new OtherBusinessList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  State = (OBOrderState)b.Field<byte>("State"),
                                                  NO = b.Field<string>("NO"),//业务号
                                                  Mblno = b.Field<string>("MBLNO"),
                                                  Hblno = b.Field<string>("HBLNO"),
                                                  CustomerID = b.Field<Guid?>("CustomerID"),
                                                  CustomerName = b.Field<string>("CustomerName"),
                                                  PolName = b.Field<string>("POLName"),
                                                  PodName = b.Field<string>("PODName"),
                                                  ShipperName = b.Field<string>("ShipperName"),
                                                  ConsigneeName = b.Field<string>("ConsigneeName"),
                                                  Etd = b.Field<DateTime?>("ETD"),
                                                  Eta = b.Field<DateTime?>("ETA"),
                                                  Feta = b.Field<DateTime?>("FETA"),
                                                  FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                                  CarrierName = b.Field<string>("CarrierName"),
                                                  AgentofCarrierID = b.Field<Guid?>("AgentofCarrierID"),
                                                  AgengofCarrierName = b.Field<string>("AgengofCarrierName"),
                                                  NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                                  NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                                  Quantity = b.Field<int>("Quantity"),
                                                  QuantityUnitID = b.Field<Guid?>("QuantityUnitID"),
                                                  QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                                  Weight = b.Field<decimal>("Weight"),
                                                  WeightUnitID = b.Field<Guid?>("WeightUnitID"),
                                                  WeightUnitName = b.Field<string>("WeightUnitName"),
                                                  VesselVoyage = b.Field<string>("VesselVoyage"),
                                                  Measurement = b.Field<decimal>("Measurement"),
                                                  MeasurementUnitID = b.Field<Guid?>("MeasurementUnitID"),
                                                  MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                                  SalesName = b.Field<string>("SalesName"),
                                                  SalesID = b.Field<Guid?>("SalesID"),
                                                  ContainerNo = b.Field<string>("ContainerNo"),
                                                  CreateDate = b.Field<DateTime?>("CreateDate"),
                                                  IsValid = b.Field<bool>("IsValid"),
                                                  UpdateByName = b.Field<string>("UpdateByName"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),

                                                  IsDirty = false
                                              }).ToList();
                return results;
            }
            catch (Exception ex) { throw ex; }

        }

        /// <summary>
        /// 业务作废
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="isValid">是否取消(true为取消,false为激活)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult CancelOtherBusiness(
            Guid orderID,
            bool isCancel,
            Guid changeByID,
            DateTime? updateDate,
            bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "ID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCancelOtherOrder");

                db.AddInParameter(dbCommand, "@OrderID", DbType.String, orderID.ToString());
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);

                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);

                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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
        /// 获取业务信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns>返回业务信息</returns>
        public OtherBusinessInfo GetOtherBusinessInfo(Guid id, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOtherOrderInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

                OtherBusinessInfo result = (from b in ds.Tables[0].AsEnumerable()
                                            select new OtherBusinessInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           AgengofCarrierName = b.Field<string>("AgentOfCarrierName"),
                                           AgentofCarrierID = b.Field<Guid?>("AgentOfCarrierID"),
                                           SoNo = b.Field<string>("SONO"),
                                           CarrierID = b.Field<Guid?>("CarrierID"),
                                           CarrierName = b.Field<string>("CarrierName"),
                                           Commodity = b.Field<string>("Commodity"),
                                           CompanyID = b.Field<Guid>("CompanyID"),
                                           CompanyName = b.Field<string>("CompanyName"),
                                           ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                           ConsigneeName = b.Field<string>("ConsigneeName"),
                                           ContractNo = b.Field<string>("ContractNo"),
                                           CreateByID = b.Field<Guid?>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime?>("CreateDate"),
                                           CustomerID = b.Field<Guid?>("CustomerID"),
                                           CustomerName = b.Field<string>("CustomerName"),
                                           CustomsBrokerID = b.Field<Guid?>("CustomsBrokerID"),
                                           CustomsBrokerName = b.Field<string>("CustomsBrokerName"),
                                           Eta = b.Field<DateTime?>("ETA"),
                                           Etd = b.Field<DateTime?>("ETD"),
                                           Feta = b.Field<DateTime?>("FETA"),
                                           FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                           FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                           Hblno = b.Field<string>("Hblno"),
                                           IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                           IsCustoms = b.Field<bool>("IsCustoms"),
                                           IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                           IsTruck = b.Field<bool>("IsTruck"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           IsWareHouse = b.Field<bool>("IsWareHouse"),
                                           Mblno = b.Field<string>("Mblno"),
                                           ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                           ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                           NotifyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                           Measurement = b.Field<decimal>("Measurement"),
                                           MeasurementUnitID = b.Field<Guid?>("MeasurementUnitID"),
                                           MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                           NO = b.Field<string>("NO"),
                                           NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                           NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                           OperationID = b.Field<Guid?>("OperationID"),
                                           OperationNo = b.Field<string>("OperationNo"),
                                           OperatorName = b.Field<string>("OperatorName"),
                                           OperatorID = b.Field<Guid?>("OperatorID"),
                                           OperationDate = b.Field<DateTime?>("OperationDate"),
                                           OtOperationType = (OtOperationType)b.Field<byte>("OtOperationType"),
                                           PaymentTypeID = b.Field<Guid?>("PaymentTypeID"),
                                           PaymentTypeName = b.Field<string>("PaymentTypeName"),
                                           PodID = b.Field<Guid?>("PodID"),
                                           PodName = b.Field<string>("PodName"),
                                           PolID = b.Field<Guid?>("PolID"),
                                           PolName = b.Field<string>("PolName"),
                                           Quantity = b.Field<int>("Quantity"),

                                           QuantityUnitID = b.Field<Guid?>("QuantityUnitID"),
                                           QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                           Remark = b.Field<string>("Remark"),
                                           SalesDepartmentID = b.Field<Guid>("SalesDepartmentID"),
                                           SalesDepartmentName = b.Field<string>("SalesDepartmentName"),
                                           SalesID = b.Field<Guid?>("SalesID"),
                                           SalesName = b.Field<string>("SalesName"),
                                           OverseasFilerID = b.Field<Guid?>("OverseasFilerID"),
                                           OverseasFilerName = b.Field<string>("OverseasFilerName"),
                                           ShipperID = b.Field<Guid?>("ShipperID"),
                                           ShipperName = b.Field<string>("ShipperName"),
                                           State = (OBOrderState)b.Field<byte>("State"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           VesselVoyage = b.Field<string>("VoyageName"),
                                           VoyageID = b.Field<Guid?>("VoyageID"),
                                           WarehouseID = b.Field<Guid?>("WarehouseID"),
                                           WarehouseName = b.Field<string>("WarehouseName"),
                                           Weight = b.Field<decimal>("Weight"),
                                           WeightUnitID = b.Field<Guid?>("WeightUnitID"),
                                           WeightUnitName = b.Field<string>("WeightUnitName"),

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

        public SingleResult SaveOtherBusinessInfo(OtherBusinessSaveRequest saveRequest)
        {
            //ArgumentHelper.AssertGuidNotEmpty(saveRequest.customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOtherBookingInfo");
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.id);
                db.AddInParameter(dbCommand, "@MBLNO", DbType.String, saveRequest.MBLNO);
                db.AddInParameter(dbCommand, "@HBLNO", DbType.String, saveRequest.HBLNO);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.CustomerID);
                db.AddInParameter(dbCommand, "@OTOperationType", DbType.Int16, saveRequest.OTOperationType);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyID);

                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, saveRequest.ShipperID);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, saveRequest.ConsigneeID);
                db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, saveRequest.NotifyPartyID);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, saveRequest.CarrierID);
                db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, saveRequest.AgentOfCarrierID);
                db.AddInParameter(dbCommand, "@POLID", DbType.Guid, saveRequest.POLID);

                db.AddInParameter(dbCommand, "@POLName", DbType.String, saveRequest.POLName);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, saveRequest.PODID);
                db.AddInParameter(dbCommand, "@PODName", DbType.String, saveRequest.PODName);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);
                db.AddInParameter(dbCommand, "@FETA", DbType.DateTime, saveRequest.FETA);

                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.FinalDestinationID);
                db.AddInParameter(dbCommand, "@FinalDestinationName", DbType.String, saveRequest.FinalDestinationName);
                db.AddInParameter(dbCommand, "@PaymentTypeID", DbType.Guid, saveRequest.PaymentTypeID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, saveRequest.VoyageID);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.Commodity);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, saveRequest.Quantity);

                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, saveRequest.QuantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, saveRequest.Weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, saveRequest.WeightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, saveRequest.Measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, saveRequest.MeasurementUnitID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);

                db.AddInParameter(dbCommand, "@SalesDepartmentID", DbType.Guid, saveRequest.SalesDepartmentID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, saveRequest.SalesID);
                db.AddInParameter(dbCommand, "@OverseasFilerID", DbType.Guid, saveRequest.OverseasFilerID);
                db.AddInParameter(dbCommand, "@OperatorID", DbType.Guid, saveRequest.OperatorID);
                db.AddInParameter(dbCommand, "@OperationDate", DbType.DateTime, saveRequest.OperationDate);
                db.AddInParameter(dbCommand, "@IsTruck", DbType.Boolean, saveRequest.IsTruck);
                db.AddInParameter(dbCommand, "@IsCustoms", DbType.Boolean, saveRequest.IsCustoms);

                db.AddInParameter(dbCommand, "@IsCommodityInspection", DbType.Boolean, saveRequest.IsCommodityInspection);
                db.AddInParameter(dbCommand, "@IsQuarantineInspection", DbType.Boolean, saveRequest.IsQuarantineInspection);
                db.AddInParameter(dbCommand, "@IsWarehouse", DbType.Boolean, saveRequest.IsWarehouse);
                db.AddInParameter(dbCommand, "@CustomsBrokerID", DbType.Guid, saveRequest.CustomsBrokerID);
                db.AddInParameter(dbCommand, "@WarehouseID", DbType.Guid, saveRequest.WarehouseID);
                db.AddInParameter(dbCommand, "@SONO", DbType.String, saveRequest.SONO);

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, saveRequest.OperationNo);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "No", "UpdateDate", "State" });
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

        public Dictionary<Guid, SaveResponse> SaveOtherBusinessWithTrans(OtherBusinessSaveRequest saveRequest,
            List<FeeSaveRequest> fees, List<ContainerSaveRequest> _container, bool IsEnglish)
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
                        new SaveResponse { RequestId = saveRequest.RequestId, SingleResult = this.SaveOtherBusinessInfo(saveRequest) });

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
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveOBOrderFeeList(fee, IsEnglish) });
                    }
                }
                if (_container != null)
                {
                    foreach (ContainerSaveRequest con in _container)
                    {
                        if (saveRequest != null && saveRequest.id == Guid.Empty)
                        {
                            con.OtherBookingID = newBookingOrderId;
                            con.IDs = new Guid[con.IDs.Length];
                        }
                        result.Add(con.RequestId,
                            new SaveResponse { RequestId = con.RequestId, ManyResult = this.SaveOtherContanerList(con, IsEnglish) });
                    }
                }

                scope.Complete();
                return result;
            }
        }

        /// <summary>
        /// 获取利润表信息
        /// </summary>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public ProfitContainerObjects GetOtherProfitReportData(Guid OperationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(OperationID, "OperationID");



            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOtherProfitReportData");
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
    }
}

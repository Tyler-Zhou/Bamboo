//-----------------------------------------------------------------------
// <copyright file="ReportDataService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.FCM.DomesticTrade.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.FCM.Common.ServiceInterface.Common;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.DomesticTrade.ServiceInterface;
    using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
    using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.ReportObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;

    /// <summary>
    /// 报表数据获取服务
    /// </summary>
    public class DTReportDataService : IDTReportDataService
    {

        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }

        /// <summary>
        /// 获取订单报表数据
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns></returns>
        public DTOrderReportData GetDTOrderReportData(Guid orderID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalOrderReportData");

                db.AddInParameter(dbCommand, "@InternalTradeBookingID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DTOrderReportData result = (from b in ds.Tables[0].AsEnumerable()
                                            select new DTOrderReportData
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                OrderNo = b.Field<string>("OrderNo"),
                                                //PONo = b.Field<string>("PONo"),
                                                SONo = b.Field<string>("SONo"),
                                                CompanyName = b.Field<string>("CompanyName"),
                                                CustomerName = b.Field<string>("CustomerName"),
                                                CustomerDescription = b.Field<string>("CustomerDescription"),
                                                CarrierName = b.Field<string>("CarrierName"),
                                                ShipperName = b.Field<string>("ShipperName"),
                                                ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                AgentName = b.Field<string>("AgentName"),
                                                AgentDescription = b.IsNull("AgentDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")).ToString(this.IsEnglish),
                                                BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                                ConsigneeName = b.Field<string>("ConsigneeName"),
                                                ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                POLName = b.Field<string>("POLName"),
                                                PODName = b.Field<string>("PODName"),
                                                PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                VesselVoyageName = b.Field<string>("VesselVoyage"),
                                                ETD = b.IsNull("ExpectedShipDate") ? (DateTime?)null : b.Field<DateTime>("ExpectedShipDate"),
                                                //ClosingDate = b.IsNull("ClosingDate") ? (DateTime?)null : b.Field<DateTime>("ClosingDate"),
                                                TransportClause = b.Field<string>("TransportClause"),
                                                OperationType = EnumHelper.GetDescription<FCMOperationType>((FCMOperationType)b.Field<Byte>("OperationType"), this.IsEnglish),
                                                //IsCustoms = b.Field<Boolean>("IsCustoms"),
                                                IsTruck = b.Field<Boolean>("IsTruck"),
                                                //IsCommodityInspection = b.Field<Boolean>("IsCommodityInspection"),
                                                //IsQuarantineInspection = b.Field<Boolean>("IsQuarantineInspection"),
                                                IsWarehouse = b.Field<Boolean>("IsWarehouse"),
                                                //IsOutHBL = !b.Field<Boolean>("IsOnlyMBL"),
                                                //TradeTerm = b.Field<string>("TradeTerm"),
                                                //ReleaseType = BuildRealseType((ReleaseType)b.Field<Byte>("MBLReleaseType"), (ReleaseType)b.Field<Byte>("HBLReleaseType")),
                                                //MBLPaymentType = b.Field<string>("MBLPaymentTerm"),
                                                //MBLRequest = b.Field<string>("MBLRequirements"),
                                                //HBLPaymentType = b.Field<string>("HBLPaymentTerm"),
                                                //HBLRequest = b.Field<string>("HBLRequirements"),
                                                SalesName = b.Field<string>("SalesName"),
                                                GoodsMarks = b.Field<string>("Commodity"),
                                                GoodsQty = b.Field<int>("Quantity").ToString() + b.Field<string>("QuantityUnitName"),
                                                GoodsWeight = b.Field<decimal>("Weight").ToString("F3"),
                                                GoodsMeasurement = b.Field<decimal>("Measurement").ToString("F3"),
                                                ContainerRequest = b.IsNull("ContainerDescription") ? string.Empty : SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")).ToString(),
                                                Remark = b.Field<string>("Remark"),
                                                OperatorName = b.Field<string>("OperatorName"),
                                                AgentOfCarrierName =b.Field<string>("AgentOfCarrierName"),
                                                //IsOutMBL = b.Field<bool>("IsOnlyMBL"),
                                                Fees = (from f in ds.Tables[1].AsEnumerable()
                                                        select new DTOrderFeeReportData
                                                     {
                                                         Way = f.Field<byte>("Way") > 0 ? "应付" : "应收",
                                                         FeeName = f.Field<string>("FeeName"),
                                                         CustomerName = f.Field<string>("CustomerName"),
                                                         Currency = f.Field<string>("Currency"),
                                                         Amount = f.Field<decimal>("Amount"),
                                                         Remark = f.Field<string>("Remark"),
                                                     }).ToList()
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

        private string BuildRealseType(FCMReleaseType mblReleaseType, FCMReleaseType hblReleaseType)
        {
            string mblReleaseTypeDesc = EnumHelper.GetDescription<FCMReleaseType>(mblReleaseType, this.IsEnglish);
            string hblReleaseTypeDesc = EnumHelper.GetDescription<FCMReleaseType>(hblReleaseType, this.IsEnglish);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("MBL:" + mblReleaseTypeDesc);
            sb.AppendLine("HBL:" + hblReleaseTypeDesc);
            return sb.ToString();
        }
      

        /// <summary>
        /// 获取当票委托的利润数据
        /// </summary>
        /// <param name="bookingID">订舱委托ID</param>
        /// <returns>返回当票委托的利润数据</returns>
        public ProfitReportData GetProfitReportData(Guid bookingID)
        {
            return new ProfitReportData();
        }

        /// <summary>
        /// 获取订舱确认报表数据
        /// </summary>
        /// <param name="bookingID">订舱委托ID</param>
        /// <returns>返回订舱确认报表数据</returns>
        public BookingConfirmationReportData GetBookingConfirmationReportData(Guid bookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalBookingConfirmationReportData");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, bookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                BookingConfirmationReportData result = (from b in ds.Tables[0].AsEnumerable()
                                                        select new BookingConfirmationReportData
                                                        {
                                                            CompanyName = b.Field<string>("CompanyName"),
                                                            CompanyAddress = b.Field<string>("CompanyAddress"),
                                                            CompanyTel = b.Field<string>("CompanyTel"),
                                                            CompanyFax = b.Field<string>("CompanyFax"),
                                                            ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                            Consignee = b.Field<string>("ConsigneeName"),
                                                            Agent = b.Field<string>("AgentName"),
                                                            VesselVoyage = b.Field<string>("VesselVoyage"),
                                                            Carrier = b.Field<string>("Carrier"),
                                                            SONo = b.Field<string>("SONo"),
                                                            PortOfLoading = b.Field<string>("POLName"),
                                                            ETD = b.IsNull("ETD") ? (DateTime?)null : b.Field<DateTime>("ETD"),
                                                            PortOfDischarge = b.Field<string>("PODName"),
                                                            ETA = b.IsNull("ETA") ? (DateTime?)null : b.Field<DateTime>("ETA"),
                                                            PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                            //FinalDestination = b.Field<string>("FinalDestination"),
                                                            //ClosingDate = b.IsNull("ClosingDate") ? (DateTime?)null : b.Field<DateTime>("ClosingDate"),
                                                            TransportClause = b.Field<string>("TransportClause"),
                                                            PickupLocation = b.IsNull("PickUpAtDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("PickUpAtDescription")).ToString(this.IsEnglish),
                                                            Remark = b.Field<string>("Remark"),
                                                            Quanity = b.Field<int>("Quantity"),
                                                            QuanityUnit = b.Field<string>("QuanityUnit"),
                                                            Weight = b.Field<decimal>("Weight"),
                                                            WeightUnit = b.Field<string>("WeightUnit"),
                                                            Measurement = b.Field<decimal>("Measurement"),
                                                            MeasurementUnit = b.Field<string>("MeasurementUnit"),
                                                            Marks = b.Field<string>("Marks"),
                                                            ContainerSize =b.Field<string>("ContainerSize")
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
        /// 获取装货单报表数据
        /// </summary>
        /// <param name="mblID">订舱委托ID</param>
        /// <returns>返回装货单报表数据</returns>
        public ShippingOrderReportData GetShippingOrderReportData(Guid mblID)
        {
            ArgumentHelper.AssertGuidNotEmpty(mblID, "blID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanShippingOrderReportData");

                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ShippingOrderReportData result = (from b in ds.Tables[0].AsEnumerable()
                                                  select new ShippingOrderReportData
                                                  {
                                                      BLNo = b.Field<string>("BLNo"),
                                                      Vessel = b.Field<string>("Vessel"),
                                                      Voyage = b.Field<string>("Voyage"),
                                                      POL = b.Field<string>("POL"),
                                                      POD = b.Field<string>("POD"),
                                                      PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                      FinalDestination = b.Field<string>("FinalDestination"),
                                                      GoodsDescription = b.Field<string>("GoodsDescription"),
                                                      ContainerType = b.Field<string>("ContainerType"),
                                                      Qty = b.Field<int>("Qty"),
                                                      QtyUnit = b.Field<string>("QtyUnit"),
                                                      GrossWeight = b.Field<decimal>("GrossWeight"),
                                                      Weight = b.Field<decimal>("Weight"),
                                                      WeightUnit = b.Field<string>("WeightUnit"),
                                                      Measurement = b.Field<decimal>("Measurement"),
                                                      MeasurementUnit = b.Field<string>("MeasurementUnit"),
                                                      Remark = b.Field<string>("Remark"),
                                                      Marks = b.Field<string>("Marks"),
                                                      GrossWeightDescription = b.Field<string>("GoodsDescription"),
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
        /// 获取装箱报表
        /// </summary>
        /// <param name="containerID">箱ID</param>
        /// <returns>ContainerPackingReportData</returns>
        public ContainerPackingReportData GetContainerPackingReportData(Guid containerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(containerID, "containerID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanPackingListReportData");

                db.AddInParameter(dbCommand, "@ContainerID", DbType.Guid, containerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ContainerPackingReportData result = (from b in ds.Tables[0].AsEnumerable()
                                                     select new ContainerPackingReportData
                                                     {
                                                         ShipperName = b.Field<string>("ShipperName"),
                                                         ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                         ConsigneeName = b.Field<string>("ConsigneeName"),
                                                         ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                         NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                                         NotifyPartyDescription = b.IsNull("NotifyPartyDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")).ToString(this.IsEnglish),
                                                         Marks = b.Field<string>("Marks"),
                                                         BLNo = b.Field<string>("BLNo"),
                                                         Vessel = b.Field<string>("Vessel"),
                                                         Voyage = b.Field<string>("Voyage"),
                                                         PlaceOfReceipt = b.Field<string>("PlaceOfReceipt"),
                                                         POL = b.Field<string>("POL"),
                                                         POD = b.Field<string>("POD"),
                                                         PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                         FinalDestination = b.Field<string>("FinalDestination"),
                                                         ContainerInfo = b.Field<string>("ContainerInfo"),
                                                         GoodsDescription = b.Field<string>("GoodsDescription"),
                                                         ContainerQtyDescription = b.Field<string>("ContainerQtyDescription"),
                                                         Quantity = b.Field<int>("Quantity"),
                                                         QuantityUnit = b.Field<string>("QuantityUnit"),
                                                         Weight = b.Field<decimal>("Weight"),
                                                         GrossWeight = b.Field<decimal>("GrossWeight"),
                                                         WeightUnit = b.Field<string>("WeightUnit"),
                                                         Measurement = b.Field<decimal>("Measurement"),
                                                         MeasurementUnit = b.Field<string>("MeasurementUnit"),
                                                         Remark = b.Field<string>("Remark"),
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
        /// 派车国外报表数据对象(短格式)
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupENShortReportData</returns>
        public PickupENShortReportData GetPickupENShortReportData(Guid truckID)
        {
            ArgumentHelper.AssertGuidNotEmpty(truckID, "truckID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetPickupENShortReportData");

                db.AddInParameter(dbCommand, "@TruckID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                PickupENShortReportData result = new PickupENShortReportData();

                CustomerDescription deliveryToInfo = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription)
                    , ds.Tables[0].Rows[0].Field<string>("DeliveryToInfo"));
                if (deliveryToInfo != null) result.DeliveryToInfo = deliveryToInfo.ToString(true);

                ContainerDescription ctn = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription)
                    , ds.Tables[0].Rows[0].Field<string>("ContainerDescription"));
                if (ctn != null) result.ContainerNOs = ctn.ToString();

                result.BookingNO = ds.Tables[0].Rows[0].Field<string>("BookingNO");
                result.CreateTime = ds.Tables[0].Rows[0].Field<string>("CreateTime");
                result.CurrentUserName = ds.Tables[0].Rows[0].Field<string>("CurrentUserName");
                result.CustomerRefNo = ds.Tables[0].Rows[0].Field<string>("CustomerRefNo");
                result.CYLocation = ds.Tables[0].Rows[0].Field<string>("CYLocation");
                result.Destination = ds.Tables[0].Rows[0].Field<string>("Destination");
                result.ETA = ds.Tables[0].Rows[0].Field<DateTime?>("ETA");
                result.GoodsDescription = ds.Tables[0].Rows[0].Field<string>("GoodsDescription");
                result.HBLNO = ds.Tables[0].Rows[0].Field<string>("HBLNO");
                result.LastFreeDate = ds.Tables[0].Rows[0].Field<string>("LastFreeDate");
                result.Measurement = ds.Tables[0].Rows[0].Field<decimal>("Measurement");
                result.PaymentTypeName = ds.Tables[0].Rows[0].Field<string>("PaymentTypeName");
                result.PKGS = ds.Tables[0].Rows[0].Field<string>("PKGS");
                result.ReferenceNO = ds.Tables[0].Rows[0].Field<string>("ReferenceNO");
                result.Weigh = ds.Tables[0].Rows[0].Field<decimal>("Weigh");

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
        /// 获取派车国外报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupENReportData</returns>
        public PickupENReportData GetPickupENReportData(Guid truckID)
        {
            ArgumentHelper.AssertGuidNotEmpty(truckID, "truckID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetPickupENReportData");

                db.AddInParameter(dbCommand, "@TruckID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }

                PickupENReportData result = new PickupENReportData();

                CustomerDescription billTo =SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription)
                    , ds.Tables[0].Rows[0].Field<string>("BillTo"));
                if (billTo != null) result.BillToInfo = billTo.ToString(true);

                CustomerDescription pickupAtInfo = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription)
                    , ds.Tables[0].Rows[0].Field<string>("PickupAtInfo"));
                if (pickupAtInfo != null) result.PickupAtInfo = pickupAtInfo.ToString(true);

                CustomerDescription deliveryToInfo = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription)
                    , ds.Tables[0].Rows[0].Field<string>("DeliveryToInfo"));
                if (deliveryToInfo != null) result.DeliveryToInfo = deliveryToInfo.ToString(true);

                ContainerDescription ctn = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription)
                    , ds.Tables[0].Rows[0].Field<string>("ContainerDescription"));
                if (ctn != null) result.ContainerNOs = ctn.ToString();

                result.BookingNO = ds.Tables[0].Rows[0].Field<string>("BookingNO");
                result.Carrier = ds.Tables[0].Rows[0].Field<string>("Carrier");
                result.Commodity = ds.Tables[0].Rows[0].Field<string>("Commodity");
                result.CreateTime = ds.Tables[0].Rows[0].Field<string>("CreateTime");
                result.DeliveryContact = ds.Tables[0].Rows[0].Field<string>("DeliveryContact");
                result.DeliveryDate = ds.Tables[0].Rows[0].Field<DateTime?>("DeliveryDate");
                result.DeliveryRefrenceNo = ds.Tables[0].Rows[0].Field<string>("DeliveryRefrenceNo");
                result.ETD = ds.Tables[0].Rows[0].Field<DateTime?>("ETD");
                result.GoodsDescription = ds.Tables[0].Rows[0].Field<string>("GoodsDescription");
                result.HBLNo = ds.Tables[0].Rows[0].Field<string>("HBLNo");
                result.LastFreeDate = ds.Tables[0].Rows[0].Field<DateTime?>("LastFreeDate");
                result.Marks = ds.Tables[0].Rows[0].Field<string>("Marks");
                result.Measurement = ds.Tables[0].Rows[0].Field<decimal>("Measurement");
                result.PickupContact = ds.Tables[0].Rows[0].Field<string>("PickupContact");
                result.PickupDate = ds.Tables[0].Rows[0].Field<DateTime?>("PickupDate");
                result.PKGS = ds.Tables[0].Rows[0].Field<int>("PKGS");
                result.POL = ds.Tables[0].Rows[0].Field<string>("POL");
                result.PortofReceipt = ds.Tables[0].Rows[0].Field<string>("PortofReceipt");
                result.BillToRefNO = ds.Tables[0].Rows[0].Field<string>("BillToRefNO");
                result.CustomerRefNo = ds.Tables[0].Rows[0].Field<string>("CustomerRefNo");
                result.TotalPKGS = ds.Tables[0].Rows[0].Field<string>("TotalPKGS");
                result.TruckerInfo = ds.Tables[0].Rows[0].Field<string>("TruckerInfo");
                result.VesselName = ds.Tables[0].Rows[0].Field<string>("VesselName");
                result.Weight = ds.Tables[0].Rows[0].Field<decimal>("Weight");

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
        /// 获派车国内报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupCNReportData</returns>
        public PickupCNReportData GetPickupCNReportData(Guid truckID)
        {
            ArgumentHelper.AssertGuidNotEmpty(truckID, "truckID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanPickupCNReportData");

                db.AddInParameter(dbCommand, "@TruckID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                PickupCNReportData result = new PickupCNReportData();

                ContainerDescription ctn = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription)
                    , ds.Tables[0].Rows[0].Field<string>("ContainerDescription"));
                if (ctn != null) result.ContainerInfo = ctn.ToString();

                result.Carrier = ds.Tables[0].Rows[0].Field<string>("Carrier");
                result.Charges = ds.Tables[0].Rows[0].Field<string>("Charges");
                result.LoadAddress = ds.Tables[0].Rows[0].Field<string>("LoadAddress");
                result.LoadDate = ds.Tables[0].Rows[0].Field<DateTime?>("LoadDate");
                result.NeedBook = ds.Tables[0].Rows[0].Field<bool>("NeedBook")?"带司机本":string.Empty;
                result.Remark = ds.Tables[0].Rows[0].Field<string>("Remark");
                result.SONO = ds.Tables[0].Rows[0].Field<string>("SONO");
                result.Title = ds.Tables[0].Rows[0].Field<string>("Title");
                result.To = ds.Tables[0].Rows[0].Field<string>("To");
                result.ToATTN = ds.Tables[0].Rows[0].Field<string>("ToATTN");
                result.ToFax = ds.Tables[0].Rows[0].Field<string>("ToFax");
                result.ToTel = ds.Tables[0].Rows[0].Field<string>("ToTel");
                result.VesselVoyage = ds.Tables[0].Rows[0].Field<string>("VesselVoyage");

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

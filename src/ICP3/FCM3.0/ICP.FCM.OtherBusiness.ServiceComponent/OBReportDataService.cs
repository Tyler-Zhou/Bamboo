using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ReportObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;


namespace ICP.FCM.OtherBusiness.ServiceComponent
{
    partial class OBReportDataService : IOBReportDataService
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
        public OBOrderReportData GetOBOrderReportData(Guid orderID, bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOhterOrderReportData");

                db.AddInParameter(dbCommand, "@OtherBookingID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OBOrderReportData result = (from b in ds.Tables[0].AsEnumerable()
                                            select new OBOrderReportData
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                OrderNo = b.Field<string>("OrderNo"),
                                                CompanyName = b.Field<string>("CompanyName"),
                                                CustomerName = b.Field<string>("CustomerName"),
                                                CustomerDescription = b.Field<string>("CustomerDescription"),
                                                CarrierName = b.Field<string>("CarrierName"),
                                                ShipperName = b.Field<string>("ShipperName"),
                                                ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString                                                                 <CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                                ConsigneeName = b.Field<string>("ConsigneeName"),
                                                ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString                                                             <CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                POLName = b.Field<string>("POLName"),
                                                PODName = b.Field<string>("PODName"),
                                                PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                VesselVoyageName = b.Field<string>("VesselVoyage"),
                                                ETD = b.IsNull("ETD") ? (DateTime?)null : b.Field<DateTime>("ETD"),
                                                OperationType = EnumHelper.GetDescription<OtOperationType>((OtOperationType)b.Field<Byte>("OperationType"), this.IsEnglish),
                                                IsCustoms = b.Field<Boolean>("IsCustoms"),
                                                IsTruck = b.Field<Boolean>("IsTruck"),
                                                IsCommodityInspection = b.Field<Boolean>("IsCommodityInspection"),
                                                IsQuarantineInspection = b.Field<Boolean>("IsQuarantineInspection"),
                                                IsWarehouse = b.Field<Boolean>("IsWarehouse"),
                                                SalesName = b.Field<string>("SalesName"),
                                                GoodsMarks = b.Field<string>("Commodity"),
                                                GoodsQty = b.Field<int>("Quantity").ToString() + b.Field<string>("QuantityUnitName"),
                                                GoodsWeight = b.Field<decimal>("Weight").ToString("F3"),
                                                GoodsMeasurement = b.Field<decimal>("Measurement").ToString("F3"),
                                                ContainerRequest = b.IsNull("ContainerDescription") ? string.Empty : SerializerHelper.DeserializeFromString                                                                 <ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")).ToString(),
                                                Remark = b.Field<string>("Remark"),
                                                OperatorName = b.Field<string>("OperatorName"),
                                                AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                Fees = (from f in ds.Tables[1].AsEnumerable()
                                                        select new OBOrderFeeReportData
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
                result.Weigh = ds.Tables[0].Rows[0].Field<decimal>("Weight");

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

                CustomerDescription billTo = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription)
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOTTruckReportData");

                db.AddInParameter(dbCommand, "@TruckServiceID", DbType.Guid, truckID);
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
                result.NeedBook = ds.Tables[0].Rows[0].Field<bool>("NeedBook") ? "带司机本" : string.Empty;
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

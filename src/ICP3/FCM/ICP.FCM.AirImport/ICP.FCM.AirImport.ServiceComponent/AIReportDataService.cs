//-----------------------------------------------------------------------
// <copyright file="OIReportDataService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.AirImport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ICP.FCM.AirImport.ServiceComponent
{
    /// <summary>
    /// 空运进口服务
    /// </summary>
    public class AIReportDataService : IAIReportDataService
    {
        #region Fields
        /// <summary>
        /// 初始化服务
        /// </summary>
        IFrameworkInitializeService FrameworkInitializeService;
        #endregion

        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                return true;
            }
        }

        #region Init Service
        /// <summary>
        /// 报表数据获取服务
        /// </summary>
        /// <param name="frameworkInitializeService">初始化服务</param>
        public AIReportDataService(IFrameworkInitializeService frameworkInitializeService)
        {
            FrameworkInitializeService = frameworkInitializeService;
        }  
        #endregion

        /// <summary>
        /// 获取订单报表数据
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns></returns>
        public OIOrderReportData GetAIOrderReportData(Guid orderID, Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIOrderReportData");

                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OIOrderReportData result = (from b in ds.Tables[0].AsEnumerable()
                                            select new OIOrderReportData
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                OrderNo = b.Field<string>("OrderNo"),
                                                CompanyName = b.Field<string>("CompanyName"),
                                                CustomerName = b.Field<string>("CustomerName"),
                                                CustomerDescription = b.Field<string>("CustomerDescription"),
                                                AirCompanyName = b.Field<string>("AirlineName"),
                                                ShipperName = b.Field<string>("ShipperName"),
                                                ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                AgentName = b.Field<string>("AgentName"),
                                                AgentDescription = b.IsNull("AgentDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")).ToString(this.IsEnglish),
                                                ConsigneeName = b.Field<string>("ConsigneeName"),
                                                ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                POLName = b.Field<string>("DepartrueName"),
                                                PODName = b.Field<string>("DestinationName"),
                                                PlaceOfDelivery = b.Field<string>("FinalDestinationName"),
                                                FlightNo = b.Field<string>("FlightNo"),
                                                ETD = b.IsNull("ExpectedShipDate") ? (DateTime?)null : b.Field<DateTime>("ExpectedShipDate"),
                                                TransportClause = b.Field<string>("TransportClause"),
                                                IsCustoms = b.Field<Boolean>("IsCustoms"),
                                                IsTruck = b.Field<Boolean>("IsTruck"),
                                                IsCommodityInspection = b.Field<Boolean>("IsCommodityInspection"),
                                                IsQuarantineInspection = b.Field<Boolean>("IsQuarantineInspection"),
                                                IsWarehouse = b.Field<Boolean>("IsWarehouse"),
                                                TradeTerm = b.Field<string>("TradeTerm"),
                                                ReleaseType = b.Field<string>("ReleaseType"),
                                                SalesName = b.Field<string>("SalesName"),
                                                GoodsMarks = b.Field<string>("Commodity"),
                                                GoodsQty = b.Field<string>("GoodsQty"),
                                                GoodsWeight = b.Field<string>("GrossWeight"),
                                                GoodsMeasurement = b.Field<string>("Measurement"),
                                                Remark = b.Field<string>("Remark"),
                                                OperatorName = b.Field<string>("OperatorName"),
                                                AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                Fees = (from f in ds.Tables[1].AsEnumerable()
                                                        select new OIOrderFeeReportData
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

        /// <summary>
        /// 获取放货通知书数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        public ReleaseOrderReportData GetReleaseOrderInfoByBusinessID(Guid businessID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetReleaseOrderInfoByAIBusinessID");

                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ReleaseOrderData releaseOrderData = (from b in ds.Tables[0].AsEnumerable()
                                                     select new ReleaseOrderData
                                                {
                                                    ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                    FinalWareHouseDescription = b.IsNull("FinalWareHouseDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("FinalWareHouseDescription")).ToString(this.IsEnglish),
                                                    CompanyAddress = b.Field<string>("CompanyAddress"),
                                                    CompanyName = b.Field<string>("CompanyName"),
                                                    CompanyTelFax = b.Field<string>("CompanyDescription"),
                                                    CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                                    MasterBLNo = b.Field<string>("MasterBLNo"),
                                                    SubBLNo = b.Field<string>("ISFNo"),
                                                    //AMSHouseBLNo = b.Field<string>("AMSHouseBLNo"),
                                                    ETD = b.IsNull("ETD") ? string.Empty : b.Field<DateTime>("ETD").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                    ETA = b.IsNull("ETA") ? string.Empty : b.Field<DateTime>("ETA").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                    ITNo = b.Field<string>("ITNo"),
                                                    NoOfPackages = b.Field<string>("NoOfPackages"),
                                                    LoadPortName = b.Field<string>("LoadPortName"),
                                                    HouseBLNo = b.Field<string>("HBLNo"),
                                                    CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("GoodsDescription")),
                                                    FlightNo = b.Field<string>("FlightNo"),
                                                    Remark = b.Field<string>("Remark"),
                                                }).SingleOrDefault();

                ReleaseOrderReportData result = new ReleaseOrderReportData();
                result.ReleaseOrderData = releaseOrderData;
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
        /// 获取到港通知书数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        public ArrivalNoticeReportData GetArrivalNoticeReportData(Guid businessID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetArrivalNoticeReportDataForAI");

                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ArrivalNoticeData arrivalNoticeData = (from b in ds.Tables[0].AsEnumerable()
                                                       select new ArrivalNoticeData
                                                      {
                                                          companyName = b.Field<string>("CompanyName"),
                                                          companyAddress = b.Field<string>("companyAddress"),
                                                          CompanyTelFax = b.Field<string>("CompanyDescription"),
                                                          PRemark = b.Field<string>("PRemark"),
                                                          ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                          ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                          NotifyPartyDescription = b.IsNull("NotifyPartyDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")).ToString(this.IsEnglish),
                                                          CustomerBrokerDescription = b.IsNull("AgentDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")).ToString(this.IsEnglish),
                                                          CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                                          MasterBLNo = b.Field<string>("MasterBLNo"),
                                                          //SubBLNo = b.Field<string>("SubBLNo"),    即：isfNO
                                                          //AMSHouseBLNo = b.Field<string>("AMSNo"),
                                                          HouseBLNo = b.Field<string>("HBLNo"),
                                                          ReferenceNO = b.Field<string>("ReferenceNO"),
                                                          LoadPortName = b.Field<string>("LoadPortName"),
                                                          ETD = b.Field<DateTime?>("ETD") == null ? string.Empty : b.Field<DateTime>("ETD").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          DiscPortName = b.Field<string>("DiscPortName"),
                                                          ETA = b.Field<DateTime?>("ETA") == null ? string.Empty : b.Field<DateTime>("ETA").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          PlaceOfDeliveryName = b.Field<string>("DestinationPortName"),
                                                          PETA = b.IsNull("FETA") ? string.Empty : b.Field<DateTime>("FETA").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          //DestinationPortName = b.Field<string>("DestinationPortName"),
                                                          //FETA = b.IsNull("FETA") ? string.Empty : b.Field<DateTime>("FETA").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          FlightNo = b.Field<string>("FlightNo"),
                                                          //LastFreeDate = b.IsNull("LastFreeDate") ? string.Empty : b.Field<DateTime>("LastFreeDate").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          GODate = b.IsNull("GODate") ? string.Empty : b.Field<DateTime>("GODate").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          // CNTRReturnName = b.Field<string>("CNTRReturnName"),
                                                          ITNo = b.Field<string>("ITNo"),
                                                          ITDate = b.IsNull("ITDate") ? string.Empty : b.Field<DateTime>("ITDate").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          ITPlace = b.Field<string>("ITPlace"),
                                                          Marks = b.Field<string>("Marks"),
                                                          NoOfPackages = b.Field<string>("NoOfPackages"),
                                                          CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("GoodsDescription")),
                                                          GrossWeight = b.Field<string>("GrossWeight"),
                                                          Measurement = b.Field<string>("Measurement"),
                                                          DeliveryTermName = b.Field<string>("DeliveryTermName"),
                                                          Remark = b.Field<string>("Remark"),
                                                          CYLocationDescription = b.Field<string>("CYLocationDescription"),
                                                          Prepared = b.Field<string>("PreparedName"),
                                                          PreparedEmail = b.Field<string>("PreparedEmail"),
                                                          LeadSealing = b.Field<string>("LeadSealing"),
                                                          POInfo = b.Field<string>("POInfo"),
                                                          ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                                          OBLReceived = b.IsNull("OBLReceived") ? false : b.Field<bool>("OBLReceived"),
                                                          //ContainerInfo = b.Field<string>("ContainerInfo"),
                                                          ScheduledFlight = b.Field<string>("ScheduledFlight"),
                                                          ReleaseOrderRequired = b.IsNull("ReleaseOrderRequired") ? string.Empty : (b.Field<bool>("ReleaseOrderRequired") ? "Release Order Required" : "")
                                                      }).SingleOrDefault();

                List<ArrivalNoticeFee> arrivalNoticeFees = (from f in ds.Tables[1].AsEnumerable()
                                                            select new ArrivalNoticeFee
                                                            {
                                                                ChargeItemDescription = f.Field<string>("ChargeItemDescription"),
                                                                PAmount = f.Field<decimal?>("CAmount") == 0.00M ? null : f.Field<decimal?>("CAmount"),
                                                                CAmount = f.Field<decimal?>("PAmount") == 0.00M ? null : f.Field<decimal?>("PAmount"),
                                                                EName = f.Field<string>("CurrencyName"),
                                                                BillNo = f.Field<string>("BillNo"),
                                                            }).ToList();

                List<ArrivalNoticeFeeAmount> feeAmountList = (from f in ds.Tables[2].AsEnumerable()
                                                              select new ArrivalNoticeFeeAmount
                                                              {
                                                                  PAmount = f.Field<decimal?>("CAmount") == 0.00M ? null : f.Field<decimal?>("CAmount"),
                                                                  CAmount = f.Field<decimal?>("PAmount") == 0.00M ? null : f.Field<decimal?>("PAmount"),
                                                                  EName = f.Field<string>("CurrencyName"),
                                                              }).ToList();

                ArrivalNoticeReportData result = new ArrivalNoticeReportData();
                result.ArrivalNoticeData = arrivalNoticeData;
                result.ArrivalNoticeFees = arrivalNoticeFees;
                result.ArrivalNoticeFeeAmounts = feeAmountList;
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
        /// 此方法的动作是把该业务的IsSentAN = true和记录当前发送时间
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        public SingleResultData ArrivalNoticeSent(Guid businessID, Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(businessID, "businessID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspConfirmArrivalNoticeSendForAI");

                db.AddInParameter(dbCommand, "@BusinessId", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@SaveBYID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        /// <summary>
        /// 获取利润表数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        public ProfitReportData GetProfitReportData(Guid businessID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetProfitReportDataForAI");

                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ProfitReportData profitReportData = (from b in ds.Tables[0].AsEnumerable()
                                                     select new ProfitReportData
                                                      {
                                                          ReferenceNo = b.Field<string>("No"),
                                                          MasterBLNo = b.Field<string>("MAWBNo"),
                                                          HouseBLNo = b.Field<string>("HAMWNo"),
                                                          LoadPortName = b.Field<string>("DepAirPortName"),
                                                          DiscPortName = b.Field<string>("DestAirPortName"),
                                                          FlightNo = b.Field<string>("FlightNo"),
                                                          ETD = b.Field<DateTime?>("ETD") == null ? string.Empty : b.Field<DateTime>("ETD").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          ETA = b.Field<DateTime?>("ETA") == null ? string.Empty : b.Field<DateTime>("ETA").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          AgentName = b.Field<string>("AgentName"),
                                                      }).SingleOrDefault();

                List<ProfitReportFeeData> fees = (from f in ds.Tables[1].AsEnumerable()
                                                  select new ProfitReportFeeData
                                                            {
                                                                InvNo = f.Field<string>("InvNo"),
                                                                PostDate = f.Field<DateTime?>("PostDate") == null ? string.Empty : f.Field<DateTime>("PostDate").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                                company = f.Field<string>("CompanyName"),
                                                                ChargeItemDescription = f.Field<string>("ChargeDescrption"),
                                                                Revenue = f.Field<decimal?>("Revenue"),
                                                                Cost = f.Field<decimal?>("Cost"),
                                                                agent = f.Field<decimal?>("agent"),
                                                                pmt = (f.Field<bool?>("pmt")) == true ? "Y" : "N",
                                                                currency = f.Field<string>("currencyName"),
                                                            }).ToList();

                profitReportData.Fees = fees;

                return profitReportData;
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
        /// 获取海进业务提单列表报表数据对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>OIBusinessReportData</returns>
        public OIBusinessReportData GetAIBusinessReportData(Guid operationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIBusinessReportData");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                OIBusinessReportData result = (from b in ds.Tables[0].AsEnumerable()
                                               select new OIBusinessReportData
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                OperationNo = b.Field<string>("OperationNo"),
                                                State = EnumHelper.GetDescription<OIOrderState>((OIOrderState)b.Field<byte>("State"), this.IsEnglish),
                                                Company = b.Field<string>("Company"),
                                                //OperationType = EnumHelper.GetDescription<OIOperationType>((OIOperationType)b.Field<byte>("OperationType"), this.IsEnglish),
                                                Customer = b.Field<string>("Customer"),
                                                TransportClause = b.Field<string>("TransportClause"),
                                                TradeTerm = b.Field<string>("TradeTerm"),
                                                PaymentTerm = b.Field<string>("PaymentTerm"),
                                                Sales = b.Field<string>("Sales"),
                                                SalesDep = b.Field<string>("SalesDep"),
                                                SalesType = b.Field<string>("SalesType"),
                                                BookingMode = EnumHelper.GetDescription<FCMBookingMode>((FCMBookingMode)b.Field<byte>("BookingMode"), this.IsEnglish),
                                                BookingDate = b.Field<DateTime>("BookingDate").ToShortDateString(),
                                                //OverSeasFiler = b.Field<string>("OverSeasFiler"),
                                                //Bookinger = b.Field<string>("Bookinger"),
                                                CustomerService = b.Field<string>("CustomerService"),
                                                Filer = b.Field<string>("Filer"),
                                                PlaceOfReceipt = b.Field<string>("PlaceOfReceipt"),
                                                POL = b.Field<string>("POL"),
                                                POD = b.Field<string>("POD"),
                                                PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                FinalDestination = b.Field<string>("FinalDestination"),
                                                AgentOfCarrier = b.Field<string>("AgentOfCarrier"),
                                                Carrier = b.Field<string>("Carrier"),
                                                PreVoyage = b.Field<string>("PreVoyage"),
                                                Voyage = b.Field<string>("Voyage"),
                                                ETD = b.Field<DateTime?>("ETD") == null ? string.Empty : b.Field<DateTime?>("ETD").Value.ToShortDateString(),
                                                ETA = b.Field<DateTime?>("ETA") == null ? string.Empty : b.Field<DateTime?>("ETA").Value.ToShortDateString(),
                                                DETA = b.Field<DateTime?>("DETA") == null ? string.Empty : b.Field<DateTime?>("DETA").Value.ToShortDateString(),
                                                FETA = b.Field<DateTime?>("FETA") == null ? string.Empty : b.Field<DateTime?>("FETA").Value.ToShortDateString(),
                                                ShippingLine = b.Field<string>("ShippingLine"),
                                                blListReportDatas = (from m in ds.Tables[1].AsEnumerable()
                                                                     select new BLListReportData
                                                                     {
                                                                         ID = m.Field<Guid>("ID"),
                                                                         BLNO = m.Field<string>("BLNO"),
                                                                         MBLNO = m.Field<string>("MBLNO"),
                                                                         Shipper = m.Field<string>("Shipper"),
                                                                         Consignee = m.Field<string>("Consignee"),
                                                                         NotifyParty = m.Field<string>("NotifyParty"),
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
        /// 获取Authority To Make Entry数据
        /// </summary>
        /// <param name="hblId">HBL ID</param>
        /// <returns></returns>
        public AIArrivalNoticeReportData GetAuthorityToMakeEntry(Guid hblId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAuthorityToMakeEntry");

                db.AddInParameter(dbCommand, "@AIHAWBID", DbType.Guid, hblId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AIArrivalNoticeData arrivalNoticeData = (from b in ds.Tables[0].AsEnumerable()
                                                         select new AIArrivalNoticeData
                                                       {
                                                           companyName = b.Field<string>("CompanyName"),
                                                           companyAddress = string.IsNullOrEmpty(b.Field<string>("companyAddress")) ? string.Empty : b.Field<string>("companyAddress").Replace("\r\n", "\r"),
                                                           CompanyTelFax = b.Field<string>("CompanyDescription"),
                                                           CompanyEmail = b.Field<string>("CompanyEmail"),
                                                           ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                           ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                           NotifyPartyDescription = b.IsNull("NotifyPartyDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")).ToString(this.IsEnglish),
                                                           MasterBLNo = b.Field<string>("MAWBNo"),
                                                           ManifestNo = b.Field<string>("ManifestNo"),
                                                           SubBLNo = b.Field<string>("SUBMAWBNO"),
                                                           HouseBLNo = b.Field<string>("HAMWNo"),
                                                           ReferenceNO = b.Field<string>("NO"),
                                                           ETD = b.IsNull("ETD") ? string.Empty : b.Field<DateTime>("ETD").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                           DiscPortName = b.Field<string>("DepAirPortName"),
                                                           ETA = b.IsNull("ETA") ? string.Empty : b.Field<DateTime>("ETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                           PlaceOfDeliveryName = b.Field<string>("FinalAirPortName"),
                                                           DestinationPortName = b.Field<string>("DestAirPortName"),
                                                           FETA = b.IsNull("FETA") ? string.Empty : b.Field<DateTime>("FETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                           FlightNo = b.Field<string>("FlightNo"),
                                                           FreightlocName = b.IsNull("FreightLoc") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("FreightLoc")).ToString(this.IsEnglish),
                                                           ITNo = b.Field<string>("ITNo"),
                                                           ITDate = b.IsNull("ITDate") ? string.Empty : b.Field<DateTime>("ITDate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                           ITPlace = b.Field<string>("ITPlace"),
                                                           Marks = b.Field<string>("Marks"),
                                                           NoOfPackages = b.Field<string>("NoOfPackages"),
                                                           GoodsName = (b.IsNull("CargoDescription") || string.IsNullOrEmpty(b.Field<string>("CargoDescription"))) ? string.Empty : (SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")).Cargo == null ? string.Empty : SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")).Cargo.ToString(true)),
                                                           //GoodsName = b.Field<string>("CargoDescription"),
                                                           EffctiveStorageDate = b.IsNull("StorageStartDate") ? string.Empty : b.Field<DateTime>("StorageStartDate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                           GrossWeight = b.Field<string>("CROSSWEIGHT"),
                                                           Measurement = b.Field<string>("VOLWEIGHT"),
                                                           Prepared = b.Field<string>("PREPBY"),
                                                           // POInfo = b.Field<string>("POInfo"),
                                                           CustomsName = b.Field<string>("CustomsName"),
                                                           FIRMCODE = b.Field<string>("FIRMCODE"),
                                                       }).SingleOrDefault();

                List<ArrivalNoticeFee> arrivalNoticeFees = (from f in ds.Tables[1].AsEnumerable()
                                                            select new ArrivalNoticeFee
                                                            {
                                                                CAmount = f.Field<decimal?>("Amount"),
                                                                EName = f.Field<string>("CurrencyName"),
                                                                BillNo = f.Field<string>("NO"),
                                                            }).ToList();

                AIArrivalNoticeReportData result = new AIArrivalNoticeReportData();
                result.ArrivalNoticeData = arrivalNoticeData;
                result.ArrivalNoticeFees = arrivalNoticeFees;
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
        /// 获取派车国内报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupCNReportData</returns>
        public PickupCNReportData GetPickupCNReportData(Guid truckID)
        {
            ArgumentHelper.AssertGuidNotEmpty(truckID, "truckID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAITruckReportData");

                db.AddInParameter(dbCommand, "@TruckServiceID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                PickupCNReportData result = new PickupCNReportData();

                result.Airline = ds.Tables[0].Rows[0].Field<string>("Airline");
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
                result.FlightNo = ds.Tables[0].Rows[0].Field<string>("FlightNo");

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



//-----------------------------------------------------------------------
// <copyright file="OIReportDataService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.FCM.Common.ServiceInterface.Common;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanImport.ServiceInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.FCM.OceanImport.ServiceInterface.DataObjects;
    using ICP.FCM.OceanImport.ServiceInterface.DataObjects.ReportObjects;

    /// <summary>
    /// 海运进口服务
    /// </summary>
    public class OIReportDataService : IOIReportDataService
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
        public OIOrderReportData GetOIOrderReportData(Guid orderID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIOrderReportData");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, orderID);
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
                                                PONo = b.Field<string>("PONo"),
                                                CompanyName = b.Field<string>("CompanyName"),
                                                CustomerName = b.Field<string>("CustomerName"),
                                                CustomerDescription = b.Field<string>("CustomerDescription"),
                                                CarrierName = b.Field<string>("CarrierName"),
                                                ShipperName = b.Field<string>("ShipperName"),
                                                ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                AgentName = b.Field<string>("AgentName"),
                                                AgentDescription = b.IsNull("AgentDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")).ToString(this.IsEnglish),
                                                ConsigneeName = b.Field<string>("ConsigneeName"),
                                                ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                POLName = b.Field<string>("POLName"),
                                                PODName = b.Field<string>("PODName"),
                                                PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                                VesselVoyageName = b.Field<string>("VesselVoyage"),
                                                ETD = b.IsNull("ExpectedShipDate") ? (DateTime?)null : b.Field<DateTime>("ExpectedShipDate"),
                                                TransportClause = b.Field<string>("TransportClause"),
                                                //OperationType = EnumHelper.GetDescription<OIOperationType>((OIOperationType)b.Field<Byte>("OperationType"), this.IsEnglish),
                                                OperationType = b.Field<string>("OperationType"),
                                                IsCustoms = b.Field<Boolean>("IsCustoms"),
                                                IsTruck = b.Field<Boolean>("IsTruck"),
                                                IsCommodityInspection = b.Field<Boolean>("IsCommodityInspection"),
                                                IsQuarantineInspection = b.Field<Boolean>("IsQuarantineInspection"),
                                                IsWarehouse = b.Field<Boolean>("IsWarehouse"),
                                                TradeTerm = b.Field<string>("TradeTerm"),
                                                ReleaseType = b.Field<string>("ReleaseType"),
                                                SalesName = b.Field<string>("SalesName"),
                                                GoodsMarks = b.Field<string>("Commodity"),
                                                //GoodsQty = b.Field<int>("Quantity").ToString() + b.Field<string>("QuantityUnitName"),
                                                GoodsQty = b.Field<string>("GoodsQty"),
                                                GoodsWeight = b.Field<string>("GrossWeight"),
                                                //GoodsWeight = b.Field<decimal>("GrossWeight").ToString("F3"),
                                                //GoodsMeasurement = b.Field<decimal>("Measurement").ToString("F3"),
                                                GoodsMeasurement = b.Field<string>("Measurement"),
                                                ContainerRequest = b.IsNull("ContainerRequest") ? string.Empty : SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerRequest")).ToString(),
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
        /// 获取提单报表数据
        /// </summary>
        /// <param name="blID">提单ID</param>
        /// <param name="blType">提单类型(0:HBL,1:MBL)</param>
        /// <returns>返回提单报表数据</returns>
        public OIBLReportData GetOIBLReportData(Guid hblId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DataSet ds = null;
                DbCommand dbCommand = null;

                dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIBLReportData");
                db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, hblId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OIBLReportData reportData = new OIBLReportData();
                reportData.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                reportData.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                reportData.CompanyTel = ds.Tables[0].Rows[0]["CompanyTel"].ToString();
                reportData.CompanyFax = ds.Tables[0].Rows[0]["CompanyFax"].ToString();
                reportData.NumberOfOriginal = byte.Parse(ds.Tables[0].Rows[0]["NumberOfOriginalPrint"].ToString());
                reportData.BLNo = ds.Tables[0].Rows[0]["HBLNO"].ToString();
                //reportData.ShipperOrderNo = ds.Tables[0].Rows[0]["ShipperOrderNo"].ToString();

                CustomerDescription shipperDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["ShipperDescription"].ToString());
                if (shipperDscription != null) reportData.Shipper = shipperDscription.ToString(this.IsEnglish);

                CustomerDescription consigneeDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["ConsigneeDescription"].ToString());
                if (consigneeDscription != null) reportData.Consignee = consigneeDscription.ToString(this.IsEnglish);

                CustomerDescription notifyPartyDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["NotifyPartyDescription"].ToString());
                if (notifyPartyDscription != null) reportData.NotifyParty = notifyPartyDscription.ToString(this.IsEnglish);

                if (string.IsNullOrEmpty(reportData.NotifyParty)) reportData.NotifyParty = "SAME AS CONSIGNEE";


                CustomerDescription agentDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["AgentDescription"].ToString());
                if (agentDscription != null) reportData.Agent = agentDscription.ToString(this.IsEnglish);

                //if (reportData.BLType == BLType.MBL) reportData.AgentText = ds.Tables[0].Rows[0]["AgentText"].ToString();

                reportData.Carrier = ds.Tables[0].Rows[0]["CarrierName"].ToString();
                reportData.PreCarriage = ds.Tables[0].Rows[0]["PreCarriage"].ToString();
                reportData.FreightPayable = ds.Tables[0].Rows[0]["FreightPayable"].ToString();
                reportData.VesselVoyage = ds.Tables[0].Rows[0]["VesselVoyage"].ToString();
                reportData.PreVesselVoyage = string.Empty;// ds.Tables[0].Rows[0]["PreVesselVoyage"].ToString();
                reportData.PlaceOfReceipt = ds.Tables[0].Rows[0]["PlaceOfReceipt"].ToString();
                reportData.PortOfLoading = ds.Tables[0].Rows[0]["PortOfLoading"].ToString();
                reportData.PortOfDischarge = ds.Tables[0].Rows[0]["PortOfDischarge"].ToString();
                reportData.PlaceOfDelivery = ds.Tables[0].Rows[0]["PlaceOfDelivery"].ToString();
                reportData.Marks = ds.Tables[0].Rows[0]["Marks"].ToString();
                reportData.Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString();
                ////reportData.DescriptionOfGoods = ds.Tables[0].Rows[0]["DescriptionOfGoods"].ToString();
                reportData.DescriptionOfContainer = ds.Tables[0].Rows[0]["ContainerDescription"].ToString();
                reportData.GrossWeight = ds.Tables[0].Rows[0]["GrossWeight"].ToString();
                reportData.Measurement = ds.Tables[0].Rows[0]["Measurement"].ToString();
                reportData.CtnQtyInfo = ds.Tables[0].Rows[0]["CtnQtyInfo"].ToString();

                reportData.RefNO = ds.Tables[0].Rows[0]["RefNO"].ToString();
                reportData.TransportClause = ds.Tables[0].Rows[0]["TransportClause"].ToString();


                //if (blType == BLType.HBL) 
                reportData.MBLNo = ds.Tables[0].Rows[0]["No"].ToString(); 
                //else reportData.MBLNo = reportData.BLNo;

                if (ds.Tables[0].Rows[0]["ETD"] != null && ds.Tables[0].Rows[0]["ETD"].ToString().Length > 0)
                    reportData.ETD = DateTime.Parse(ds.Tables[0].Rows[0]["ETD"].ToString());

                reportData.FreightAndCharges = ds.Tables[0].Rows[0]["FreightAndCharges"].ToString();
                reportData.PaymentTerm = ds.Tables[0].Rows[0]["PaymentTerm"].ToString();
                reportData.PlaceOfIssue = ds.Tables[0].Rows[0]["PlaceOfIssue"].ToString();
                reportData.IssueBy = ds.Tables[0].Rows[0]["IssueBy"].ToString();
                if (ds.Tables[0].Rows[0]["IssueDate"] != null && ds.Tables[0].Rows[0]["IssueDate"].ToString().Length > 0)
                    reportData.IssueDate = DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());

                reportData.AMSNO = ds.Tables[0].Rows[0]["AMSNO"].ToString();
                reportData.ISFNO = ds.Tables[0].Rows[0]["ISFNO"].ToString();

                return reportData;
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetReleaseOrderInfoByBusinessID");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, businessID);
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
                                                    CompanyAddress = b.Field<string>("CompanyAddress").Replace("\r\n", "\r"), 
                                                    CompanyName = b.Field<string>("CompanyName"),
                                                    CompanyTelFax = b.Field<string>("CompanyDescription"),
                                                    CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                                    MasterBLNo = b.Field<string>("MasterBLNo"),
                                                    SubBLNo = b.Field<string>("ISFNo"),
                                                    AMSHouseBLNo = b.Field<string>("AMSHouseBLNo"),
                                                    ETD = b.IsNull("ETD") ? string.Empty : b.Field<DateTime>("ETD").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                    ETA = b.IsNull("ETA") ? string.Empty : b.Field<DateTime>("ETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                    POD = b.Field<string>("PODName"),
                                                    FinalDest = b.Field<string>("FinalDestination"),
                                                    FETA = b.IsNull("FETA") ? string.Empty : b.Field<DateTime>("FETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                    VesselName = b.Field<string>("VesselName"),
                                                    ITNo = b.Field<string>("ITNo"),
                                                    NoOfPackages = b.Field<string>("NoOfPackages"),
                                                    LoadPortName = b.Field<string>("LoadPortName"),
                                                    HouseBLNo = b.Field<string>("HBLNo"),
                                                    CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("GoodsDescription")),
                                                    //GoodsDescription = b.IsNull("GoodsDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("GoodsDescription")).ToString(),
                                                    VoyageNo = b.Field<string>("VoyageNo"),
                                                    Remark = b.Field<string>("Remark"),
                                                }).SingleOrDefault();

                List<ContainerInfoReportData> containerInfoList = (from f in ds.Tables[1].AsEnumerable()
                                                                   select new ContainerInfoReportData
                                                                   {
                                                                       ContainerNo = f.Field<string>("ContainerNo"),
                                                                       Size = f.Field<string>("SIZE"),
                                                                       Type = f.Field<string>("TYPE"),
                                                                       SealNo = f.Field<string>("SEALNO"),
                                                                       PickupNo = f.Field<string>("PICKUPNO"),
                                                                       //Quantity = f.Field<string>("Quantity"),
                                                                       LastFreeDate = f.IsNull("LastFreeDate") ? string.Empty : f.Field<DateTime>("LastFreeDate").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                                       GODate = f.IsNull("GODate") ? string.Empty : f.Field<DateTime>("GODate").ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                                   }).ToList();

                ReleaseOrderReportData result = new ReleaseOrderReportData();
                result.ReleaseOrderData = releaseOrderData;
                result.ContainerInfos = containerInfoList;
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
        public ArrivalNoticeReportData GetArrivalNoticeReportData(Guid businessID,Guid? hblID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetArrivalNoticeReportData");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, hblID);
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
                                                          companyAddress = b.Field<string>("companyAddress").Replace("\r\n", "\r"),
                                                          CompanyTelFax = b.Field<string>("CompanyDescription"),
                                                          PRemark = b.Field<string>("PRemark"),
                                                          ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                          ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                          NotifyPartyDescription = b.IsNull("NotifyPartyDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")).ToString(this.IsEnglish),
                                                          CustomerBrokerDescription = b.IsNull("AgentDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")).ToString(this.IsEnglish),
                                                          CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                                          MasterBLNo = b.Field<string>("MasterBLNo"),
                                                          SubBLNo = b.Field<string>("SubBLNo"),
                                                          AMSHouseBLNo = b.Field<string>("AMSNo"),
                                                          HouseBLNo = b.Field<string>("HBLNo"),
                                                          ReferenceNO = b.Field<string>("ReferenceNO"),
                                                          LoadPortName = b.Field<string>("LoadPortName"),
                                                          ETD = b.IsNull("ETD")?string.Empty:b.Field<DateTime>("ETD").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          DiscPortName = b.Field<string>("DiscPortName"),
                                                          ETA =b.IsNull("ETA")?string.Empty: b.Field<DateTime>("ETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                                          PETA = b.IsNull("PETA") ? string.Empty : b.Field<DateTime>("PETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          DestinationPortName = b.Field<string>("DestinationPortName"),
                                                          FETA = b.IsNull("FETA") ? string.Empty : b.Field<DateTime>("FETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          VesselVoyageNo = b.Field<string>("VesselVoyageNo"),
                                                          LastFreeDate = b.IsNull("LastFreeDate") ? string.Empty : b.Field<DateTime>("LastFreeDate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          GODate = b.IsNull("GODate") ? string.Empty : b.Field<DateTime>("GODate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          CNTRReturnName = b.Field<string>("CNTRReturnName"),
                                                          ITNo = b.Field<string>("ITNo"),
                                                          ITDate = b.IsNull("ITDate") ? string.Empty : b.Field<DateTime>("ITDate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                          ITPlace = b.Field<string>("ITPlace"),
                                                          Marks = b.Field<string>("Marks"),
                                                          NoOfPackages = b.Field<string>("NoOfPackages"),
                                                          CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("GoodsDescription")),
                                                          GrossWeight = b.Field<string>("GrossWeight"),
                                                          Measurement = b.Field<string>("Measurement"),
                                                          DeliveryTermName = b.Field<string>("DeliveryTermName"),
                                                          Remark = b.Field<string>("Remark").Replace("\n", " "), 
                                                          CYLocationDescription = b.Field<string>("CYLocationDescription"),
                                                          Prepared = b.Field<string>("PreparedName"),
                                                          PreparedEmail = b.Field<string>("PreparedEmail"),
                                                          LeadSealing = b.Field<string>("LeadSealing"),
                                                          POInfo = b.Field<string>("POInfo"),
                                                          ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                                          OBLReceived = b.IsNull("OBLReceived") ? false : b.Field<bool>("OBLReceived"),
                                                          ContainerInfo = b.Field<string>("ContainerInfo"),
                                                          ScheduledFlight = b.Field<string>("ScheduledFlight"),
                                                          //ReleaseOrderRequired = b.Field<string>("ReleaseOrderRequired"),
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

                List<ContainerInfoReportData> containerInfoList = (from f in ds.Tables[3].AsEnumerable()
                                                                   select new ContainerInfoReportData
                                                                   {
                                                                       ContainerNo = f.Field<string>("ContainerNo"),
                                                                       Size = f.Field<string>("SIZE"),
                                                                       Type = f.Field<string>("TYPE"),
                                                                       SealNo = f.Field<string>("SEALNO"),
                                                                       PickupNo = f.Field<string>("PICKUPNO"),
                                                                       LastFreeDate = f.IsNull("LastFreeDate") ? string.Empty : f.Field<DateTime>("LastFreeDate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                                       GODate = f.IsNull("GODate") ? string.Empty : f.Field<DateTime>("GODate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                                   }).ToList();

                ArrivalNoticeReportData result = new ArrivalNoticeReportData();
                result.ArrivalNoticeData = arrivalNoticeData;
                result.ArrivalNoticeFees = arrivalNoticeFees;
                result.ArrivalNoticeFeeAmounts = feeAmountList;
                result.ContainerList = containerInfoList;
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspConfirmArrivalNoticeSend");

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
        /// 获取巴西到港通知书报表数据
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        public ArrivalNoticeReportDataForBrazil GetOIArrivalNoticeReportDataForBrazil(Guid businessID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIArrivalNoticeReportDataForBrazil");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ArrivalNoticeReportDataForBrazil result = (from b in ds.Tables[0].AsEnumerable()
                                                                      select new ArrivalNoticeReportDataForBrazil
                                                       {
                                                           CustomerName = b.Field<string>("CustomerName"),
                                                           ShipperName = b.Field<string>("ShipperName"),
                                                           ConsigneeName = b.Field<string>("ConsigneeName"),
                                                           HBLNO = b.Field<string>("HBLNO"),
                                                           CNTRNO = b.Field<string>("CNTRNO"),
                                                           INCOTERMS = b.Field<string>("INCOTERMS"),
                                                           Volume = b.Field<string>("VOLUME"),
                                                           POLName = b.Field<string>("POLName"),
                                                           PODName = b.Field<string>("PODName"),
                                                           ETD = b.IsNull("ETD") ? string.Empty : b.Field<DateTime>("ETD").ToString("dd/MM", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                           ETA = b.IsNull("ETA") ? string.Empty : b.Field<DateTime>("ETA").ToString("dd/MM", System.Globalization.DateTimeFormatInfo.InvariantInfo), 
                                                           VesselName = b.Field<string>("VesselName"),
                                                           VoyageName = b.Field<string>("VoyageName"),                                                            
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
        /// 获取海进业务提单列表报表数据对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>OIBusinessReportData</returns>
        public OIBusinessReportData GetOIBusinessReportData(Guid operationID)
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
                                                OperationType = EnumHelper.GetDescription<FCMOperationType>((FCMOperationType)b.Field<byte>("OperationType"), this.IsEnglish),
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
                                                ETD = b.Field<DateTime?>("ETD")==null? string.Empty :b.Field<DateTime?>("ETD").Value.ToShortDateString(),
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
        /// 获取派车委托单(预约)报表数据
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupCNReportData</returns>
        public OIPickupCNReportData GetOIPickupCNReportData(Guid truckID)
        {
            ArgumentHelper.AssertGuidNotEmpty(truckID, "truckID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIPickupCNReportData");

                db.AddInParameter(dbCommand, "@TruckID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OIPickupCNReportData result = new OIPickupCNReportData();
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

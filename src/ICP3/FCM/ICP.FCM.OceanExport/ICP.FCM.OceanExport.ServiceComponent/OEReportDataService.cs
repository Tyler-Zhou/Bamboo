//-----------------------------------------------------------------------
// <copyright file="ReportDataService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Text;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.Comm;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    /// <summary>
    /// 报表数据获取服务
    /// </summary>
    public class OEReportDataService : IOEReportDataService
    {
        #region Fields
        /// <summary>
        /// 初始化服务
        /// </summary>
        IFrameworkInitializeService FrameworkInitializeService;
        #endregion

        #region Property
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
        #endregion

        #region Init Service
        /// <summary>
        /// 报表数据获取服务
        /// </summary>
        /// <param name="frameworkInitializeService">初始化服务</param>
        public OEReportDataService(IFrameworkInitializeService frameworkInitializeService)
        {
            FrameworkInitializeService = frameworkInitializeService;
        }  
        #endregion

        /// <summary>
        /// 获取订单报表数据
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns></returns>
        public OEOrderReportData GetOEOrderReportData(Guid orderID, Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanOrderReportData");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OEOrderReportData result = (from b in ds.Tables[0].AsEnumerable()
                                            select new OEOrderReportData
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                OrderNo = b.Field<string>("OrderNo"),
                                                PONo = b.Field<string>("PONo"),
                                                SONo = b.Field<string>("SONo"),
                                                CompanyName = b.Field<string>("CompanyName"),
                                                CustomerName = b.Field<string>("CustomerName"),
                                                CustomerDescription = b.Field<string>("CustomerDescription"),
                                                CarrierName = b.Field<string>("CarrierName"),
                                                ShipperName = b.Field<string>("ShipperName"),
                                                ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(IsEnglish),
                                                AgentName = b.Field<string>("AgentName"),
                                                AgentDescription = b.IsNull("AgentDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")).ToString(IsEnglish),
                                                BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                                ConsigneeName = b.Field<string>("ConsigneeName"),
                                                ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(IsEnglish),
                                                POLName = b.Field<string>("POLName"),
                                                PODName = b.Field<string>("PODName"),
                                                PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                                VesselVoyageName = b.Field<string>("VesselVoyage"),
                                                ETD = b.IsNull("ExpectedShipDate") ? (DateTime?)null : b.Field<DateTime>("ExpectedShipDate"),
                                                ClosingDate = b.IsNull("ClosingDate") ? (DateTime?)null : b.Field<DateTime>("ClosingDate"),
                                                TransportClause = b.Field<string>("TransportClause"),
                                                OperationType = EnumHelper.GetDescription<FCMOperationType>((FCMOperationType)b.Field<Byte>("OperationType"), IsEnglish),
                                                IsCustoms = b.Field<Boolean>("IsCustoms"),
                                                IsTruck = b.Field<Boolean>("IsTruck"),
                                                IsCommodityInspection = b.Field<Boolean>("IsCommodityInspection"),
                                                IsQuarantineInspection = b.Field<Boolean>("IsQuarantineInspection"),
                                                IsWarehouse = b.Field<Boolean>("IsWarehouse"),
                                                IsOutHBL = !b.Field<Boolean>("IsOnlyMBL"),
                                                TradeTerm = b.Field<string>("TradeTerm"),
                                                ReleaseType = BuildRealseType((FCMReleaseType)b.Field<Byte>("MBLReleaseType"), (FCMReleaseType)b.Field<Byte>("HBLReleaseType")),
                                                MBLPaymentType = b.Field<string>("MBLPaymentTerm"),
                                                MBLRequest = b.Field<string>("MBLRequirements"),
                                                HBLPaymentType = b.Field<string>("HBLPaymentTerm"),
                                                HBLRequest = b.Field<string>("HBLRequirements"),
                                                SalesName = b.Field<string>("SalesName"),
                                                GoodsMarks = b.Field<string>("Commodity"),
                                                GoodsQty = b.Field<int>("Quantity").ToString() + b.Field<string>("QuantityUnitName"),
                                                GoodsWeight = b.Field<decimal>("Weight").ToString("F3"),
                                                GoodsMeasurement = b.Field<decimal>("Measurement").ToString("F3"),
                                                ContainerRequest = b.IsNull("ContainerDescription") ? string.Empty : SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")).ToString(),
                                                Remark = b.Field<string>("Remark"),
                                                OperatorName = b.Field<string>("OperatorName"),
                                                AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                IsOutMBL = true,
                                                Fees = (from f in ds.Tables[1].AsEnumerable()
                                                        select new OEOrderFeeReportData
                                                     {
                                                         Way = IsEnglish ? (f.Field<byte>("Way") > 1 ? "AP" : "AR") : (f.Field<byte>("Way") > 1 ? "应付" : "应收"),
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
            string mblReleaseTypeDesc = EnumHelper.GetDescription<FCMReleaseType>(mblReleaseType, IsEnglish);
            string hblReleaseTypeDesc = EnumHelper.GetDescription<FCMReleaseType>(hblReleaseType, IsEnglish);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MBL:" + mblReleaseTypeDesc);
            sb.AppendLine("HBL:" + hblReleaseTypeDesc);
            return sb.ToString();
        }
        /// <summary>
        /// 获取提单报表数据
        /// </summary>
        /// <param name="blID">提单ID</param>
        /// <param name="blType">提单类型</param>
        /// <returns>返回提单报表数据</returns>
        public BLReportData GetBLReportData(
                Guid blID,
                FCMBLType blType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                DataSet ds = null;
                DbCommand dbCommand = null;

                if (blType == FCMBLType.MBL)
                {
                    dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanMBLReportData");
                    db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, blID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                }
                else
                {
                    dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanHBLReportData");

                    db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, blID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                }

                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                BLReportData reportData = new BLReportData();
                reportData.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString().ToUpper();
                reportData.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString().ToUpper();
                reportData.CompanyTel = ds.Tables[0].Rows[0]["CompanyTel"].ToString().ToUpper();
                reportData.CompanyFax = ds.Tables[0].Rows[0]["CompanyFax"].ToString().ToUpper();
                reportData.NumberOfOriginal = byte.Parse(ds.Tables[0].Rows[0]["NumberOfOriginal"].ToString());
                reportData.BLNo = ds.Tables[0].Rows[0]["BLNo"].ToString().ToUpper();
                reportData.ShipperOrderNo = ds.Tables[0].Rows[0]["ShipperOrderNo"].ToString().ToUpper();
                reportData.BLType = (FCMBLType)Enum.Parse(typeof(FCMBLType), ds.Tables[0].Rows[0]["BLType"].ToString());

                CustomerDescription shipperDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["Shipper"].ToString());
                if (shipperDscription != null) reportData.Shipper = shipperDscription.ToString(IsEnglish).ToUpper();

                CustomerDescription consigneeDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["Consignee"].ToString());
                if (consigneeDscription != null) reportData.Consignee = consigneeDscription.ToString(IsEnglish).ToUpper();

                CustomerDescription notifyPartyDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["NotifyParty"].ToString());
                if (notifyPartyDscription != null) reportData.NotifyParty = notifyPartyDscription.ToString(IsEnglish).ToUpper();

                if (string.IsNullOrEmpty(reportData.NotifyParty)) reportData.NotifyParty = "SAME AS CONSIGNEE";


                CustomerDescription agentDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["Agent"].ToString());
                if (agentDscription != null) reportData.Agent = agentDscription.ToString(IsEnglish).ToUpper();

                if (reportData.BLType == FCMBLType.MBL) reportData.AgentText = ds.Tables[0].Rows[0]["AgentText"].ToString().ToUpper();

                if (ds.Tables[0].Rows[0]["Carrier"] != null)
                {
                    reportData.Carrier = ds.Tables[0].Rows[0]["Carrier"].ToString().ToUpper();
                }

                reportData.PreCarriage = ds.Tables[0].Rows[0]["PreCarriage"].ToString().ToUpper();
                reportData.FreightPayable = ds.Tables[0].Rows[0]["FreightPayable"].ToString().ToUpper();
                reportData.VesselVoyage = ds.Tables[0].Rows[0]["VesselVoyage"].ToString().ToUpper();
                reportData.PreVesselVoyage = string.Empty;// ds.Tables[0].Rows[0]["PreVesselVoyage"].ToString();
                reportData.PlaceOfReceipt = ds.Tables[0].Rows[0]["PlaceOfReceipt"].ToString().ToUpper();
                reportData.PortOfLoading = ds.Tables[0].Rows[0]["PortOfLoading"].ToString().ToUpper();
                reportData.PortOfDischarge = ds.Tables[0].Rows[0]["PortOfDischarge"].ToString().ToUpper();
                reportData.PlaceOfDelivery = ds.Tables[0].Rows[0]["PlaceOfDelivery"].ToString().ToUpper();
                reportData.FinalDestinationName = ds.Tables[0].Rows[0]["FinalDestinationName"].ToString().ToUpper();
                reportData.Marks = ds.Tables[0].Rows[0]["Marks"].ToString().ToUpper();
                reportData.Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString().ToUpper();
                reportData.DescriptionOfGoods = ds.Tables[0].Rows[0]["GoodsDescription"].ToString().ToUpper();
                reportData.DescriptionOfContainer = ds.Tables[0].Rows[0]["ContainerDescription"].ToString().ToUpper();
                reportData.GrossWeight = ds.Tables[0].Rows[0]["GrossWeight"].ToString().ToUpper();
                reportData.Measurement = ds.Tables[0].Rows[0]["Measurement"].ToString().ToUpper();
                reportData.CtnQtyInfo = ds.Tables[0].Rows[0]["CtnQtyInfo"].ToString().ToUpper();
                reportData.VoyageShowType = (VoyageShowType)(int.Parse(ds.Tables[0].Rows[0]["VoyageShowType"].ToString()));
                reportData.RefNO = ds.Tables[0].Rows[0]["RefNO"].ToString().ToUpper();
                reportData.TransportClause = ds.Tables[0].Rows[0]["TransportClause"].ToString().ToUpper();
                reportData.ContainerNO = ds.Tables[0].Rows[0]["ContainerNO"].ToString().ToUpper();

                if (blType == FCMBLType.HBL)
                {
                    reportData.MBLNo = ds.Tables[0].Rows[0]["MBLNo"].ToString().ToUpper();
                    reportData.CarrierCode = ds.Tables[0].Rows[0]["CarrierCode"].ToString();
                }
                else
                {
                    reportData.MBLNo = reportData.BLNo.ToUpper();
                }
                if (ds.Tables[0].Rows[0]["ETD"] != null && ds.Tables[0].Rows[0]["ETD"].ToString().Length > 0)
                {
                    reportData.ETD = DateTime.Parse(ds.Tables[0].Rows[0]["ETD"].ToString());
                }
                reportData.FreightAndCharges = ds.Tables[0].Rows[0]["FreightAndCharges"].ToString().ToUpper();
                reportData.PaymentTerm = ds.Tables[0].Rows[0]["PaymentTerm"].ToString().ToUpper();
                reportData.PlaceOfIssue = ds.Tables[0].Rows[0]["PlaceOfIssue"].ToString().ToUpper();
                reportData.IssueBy = ds.Tables[0].Rows[0]["IssueBy"].ToString().ToUpper();
                if (ds.Tables[0].Rows[0]["IssueDate"] != null && ds.Tables[0].Rows[0]["IssueDate"].ToString().Length > 0)
                {
                    reportData.IssueDate = DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
                }
                reportData.AMSNO = ds.Tables[0].Rows[0]["AMSNO"].ToString().Replace("\r", "\r\n").ToUpper();
                reportData.ISFNO = ds.Tables[0].Rows[0]["ISFNO"].ToString().ToUpper();

                if (blType == FCMBLType.HBL)
                {
                    if (ds.Tables[0].Rows[0]["ShippingLineID"] != null && ds.Tables[0].Rows[0]["ShippingLineID"].ToString().Length > 0)
                    {
                        reportData.ShippingLineID = new Guid(ds.Tables[0].Rows[0]["ShippingLineID"].ToString());
                    }
                }

                if (blType == FCMBLType.MBL && ds.Tables[0].Rows[0]["ReleaseType"] != null)
                {
                    reportData.FCMReleaseType = (FCMReleaseType)(int.Parse(ds.Tables[0].Rows[0]["ReleaseType"].ToString()));
                }

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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBookingConfirmationReportData");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, bookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
                                                            ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(IsEnglish),
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
                                                            FinalDestination = b.Field<string>("FinalDestination"),
                                                            ClosingDate = b.IsNull("ClosingDate") ? (DateTime?)null : b.Field<DateTime>("ClosingDate"),
                                                            TransportClause = b.Field<string>("TransportClause"),
                                                            PickupLocation = b.IsNull("PickUpAtDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("PickUpAtDescription")).ToString(IsEnglish),
                                                            Remark = b.Field<string>("Remark"),
                                                            Quanity = b.Field<int>("Quantity"),
                                                            QuanityUnit = b.Field<string>("QuanityUnit"),
                                                            Weight = b.Field<decimal>("Weight"),
                                                            WeightUnit = b.Field<string>("WeightUnit"),
                                                            Measurement = b.Field<decimal>("Measurement"),
                                                            MeasurementUnit = b.Field<string>("MeasurementUnit"),
                                                            Marks = b.Field<string>("Marks"),
                                                            ContainerSize = b.Field<string>("ContainerSize"),
                                                            OperationNO = b.Field<string>("OperationNO"),
                                                            BookingerName = b.Field<string>("BookingerName"),
                                                            BookingerEmail = b.Field<string>("BookingerEmail"),
                                                            BookingerTel = b.Field<string>("BookingerTel"),
                                                            NotifyPartyDescription = b.Field<string>("NotifyPartyDescription"),
                                                            ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(IsEnglish),
                                                            SODate = b.Field<DateTime>("SODate"),
                                                            CYClosingDate = b.Field<DateTime?>("CYClosingDate"),
                                                            DOCClosingDate = b.Field<DateTime?>("DOCClosingDate"),
                                                            ReturnLocation = b.IsNull("ReturnLocation") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ReturnLocation")).ToString(IsEnglish),
                                                            PickupEarliestDate = b.Field<DateTime?>("PickupEarliestDate"),
                                                            PlaceOfReceipt = b.Field<string>("PlaceOfReceipt"),
                                                            OkToSub = b.Field<string>("OkToSub"),
                                                            CarrierCode = b.Field<string>("CarrierCode"),
                                                            RailCutOff = b.Field<DateTime?>("RailCutOff"),
                                                            VGMCutOffDate = b.Field<DateTime?>("VGMCutOffDate")
                                                        }).SingleOrDefault();

                result.ContainerInfoList = (from b in ds.Tables[1].AsEnumerable()
                                            select new ContainerInfo
                                            {
                                                Commodity = b.Field<string>("Commodity"),
                                                ContainerSize = b.Field<string>("ContainerSize"),
                                                OktoSub = b.Field<string>("OktoSub"),
                                                TransportClause = b.Field<string>("MoveType"),
                                                Weight = b.Field<string>("GrossWeight")
                                            }
                                            ).ToList();



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
        /// 获取订舱确认报表数据(宁波)
        /// </summary>
        /// <param name="operationID">业务号</param>
        /// <returns>返回订舱确认报表数据(宁波)</returns>
        public BookingConfirmation4NBReportData GetBookingConfirmation4NBReportData(Guid operationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBookingConfirmation4NBReportData");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                BookingConfirmation4NBReportData result = (from b in ds.Tables[0].AsEnumerable()
                                                           select new BookingConfirmation4NBReportData
                                               {
                                                   ReportTitle = b.Field<string>("ReportTitle"),
                                                   Vessel = b.Field<string>("Vessel"),
                                                   Voyage = b.Field<string>("Voyage"),
                                                   SailingDate = b.Field<string>("SailingDate"),
                                                   ContactInfo = b.Field<string>("ContactInfo"),
                                                   BLInfoList = (from b1 in ds.Tables[1].AsEnumerable()
                                                                 select new BLInfo4NBReportData
                                                                        {
                                                                            BLNo = b1.Field<string>("BLNo"),
                                                                            Cut_OffDate = b1.Field<string>("Cut_OffDate"),
                                                                            PlaceofDelivery = b1.Field<string>("PlaceofDelivery"),
                                                                            ShippingAgent = b1.Field<string>("ShippingAgent"),
                                                                            Quantity = b1.Field<string>("Quantity"),
                                                                            Weight = b1.Field<string>("Weight"),
                                                                            Measurement = b1.Field<string>("Measurement"),
                                                                            Carrier = b1.Field<string>("Carrier"),
                                                                            PaymentTerm = b1.Field<string>("PaymentTerm"),
                                                                            TransportClause = b1.Field<string>("TransportClause"),
                                                                            ExternalNumber = b1.Field<string>("ExternalNumber"),
                                                                            Operator = b1.Field<string>("Operator"),
                                                                            OperatorTel = b1.Field<string>("OperatorTel"),
                                                                            OperatorEMail = b1.Field<string>("OperatorEMail"),
                                                                        }).ToList(),
                                                   ContainerInfoList = (from b2 in ds.Tables[2].AsEnumerable()
                                                                 select new ContainerInfo4NBReportData
                                                                 {
                                                                     ContainerType = b2.Field<string>("ContainerType"),
                                                                     ContainerQuantity = b2.Field<string>("ContainerQuantity"),
                                                                     BLType = b2.Field<string>("BLType"),
                                                                     Quantity = b2.Field<string>("Quantity"),
                                                                     Weight = b2.Field<string>("Weight"),
                                                                     Measurement = b2.Field<string>("Measurement"),
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
        /// 获取订舱提单列表报表数据对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>GetBookingReportData</returns>
        public OEBusinessReportData GetOEBusinessReportData(Guid operationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOEBusinessReportData");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                OEBusinessReportData result = (from b in ds.Tables[0].AsEnumerable()
                                               select new OEBusinessReportData
                                                        {
                                                            ID = b.Field<Guid>("ID"),
                                                            OperationNo = b.Field<string>("OperationNo"),
                                                            State = EnumHelper.GetDescription<OEOrderState>((OEOrderState)b.Field<byte>("State"), IsEnglish),
                                                            Company = b.Field<string>("Company"),
                                                            OperationType = EnumHelper.GetDescription<FCMOperationType>((FCMOperationType)b.Field<byte>("OperationType"), IsEnglish),
                                                            Customer = b.Field<string>("Customer"),
                                                            TransportClause = b.Field<string>("TransportClause"),
                                                            TradeTerm = b.Field<string>("TradeTerm"),
                                                            PaymentTerm = b.Field<string>("PaymentTerm"),
                                                            Sales = b.Field<string>("Sales"),
                                                            SalesDep = b.Field<string>("SalesDep"),
                                                            SalesType = b.Field<string>("SalesType"),
                                                            BookingMode = EnumHelper.GetDescription<FCMBookingMode>((FCMBookingMode)b.Field<byte>("BookingMode"), IsEnglish),
                                                            BookingDate = b.Field<DateTime>("BookingDate").ToShortDateString(),
                                                            OverSeasFiler = b.Field<string>("OverSeasFiler"),
                                                            Bookinger = b.Field<string>("Bookinger"),
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
                                                            ETD = b.Field<DateTime?>("ETD") == null ? string.Empty : b.Field<DateTime>("ETD").ToShortDateString(),
                                                            ETA = b.Field<DateTime?>("ETA") == null ? string.Empty : b.Field<DateTime>("ETA").ToShortDateString(),
                                                            ShippingLine = b.Field<string>("ShippingLine"),
                                                            SONO = b.Field<string>("SONo"),
                                                            ContainerNos = b.Field<string>("ContainerNos"),
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
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
                                                         ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(IsEnglish),
                                                         ConsigneeName = b.Field<string>("ConsigneeName"),
                                                         ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(IsEnglish),
                                                         NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                                         NotifyPartyDescription = b.IsNull("NotifyPartyDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")).ToString(IsEnglish),
                                                         ContainerType = b.Field<string>("Type"),
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
                                                         //Weight = b.Field<string>("Weight"),
                                                         GrossWeight = b.Field<decimal>("GrossWeight"),
                                                         WeightUnit = b.Field<string>("WeightUnit"),
                                                         //Measurement = b.Field<string>("Measurement"),
                                                         MeasurementUnit = b.Field<string>("MeasurementUnit"),
                                                         Remark = b.Field<string>("Remark"),
                                                         GoodsList = (from f in ds.Tables[1].AsEnumerable()
                                                                      select new GoodsListReportData
                                                           {
                                                               BLNo = f.Field<string>("BLNo"),
                                                               Commodity = f.Field<string>("Commodity"),
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
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanPickupCNReportData");

                db.AddInParameter(dbCommand, "@TruckID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        /// <summary>
        /// 获报关委托国内报表数据对象
        /// </summary>
        /// <param name="customsID">报关单ID</param>
        /// <returns>CustomsCNReportData</returns>
        public CustomsCNReportData GetCustomsCNReportData(Guid customsID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customsID, "customsID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanCustomsCNReportData");

                db.AddInParameter(dbCommand, "@CustomsID", DbType.Guid, customsID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return IListDataSet.DataSetToIList<CustomsCNReportData>(ds, 0).SingleOrDefault();
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

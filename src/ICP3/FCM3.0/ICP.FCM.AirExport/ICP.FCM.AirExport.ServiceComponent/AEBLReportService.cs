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
using System.Transactions;
using ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects;

namespace ICP.FCM.AirExport.ServiceComponent
{
    partial class AirExportService
    {
        /// <summary>
        /// 提单MBL报表
        /// </summary>
        /// <param name="MBLId">主提单ID</param>
        /// <param name="isEnglish">是否英文环境</param>
        /// <returns></returns>
        public BLReportData_HBL GetMBLReportData(Guid Id, bool isEnglish, BLType type)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = null;
            DbCommand dbCommand = null;


            if (type == BLType.MAWB)
            {
                dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirMBLReportData");
                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, Id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            }
            else
            {
                dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirHBLReportData");

                db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, Id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            }
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null)
            {
                return null;
            }
            BLReportData_HBL result = new BLReportData_HBL();
            //HAWBNO =string.Empty,//MBL无HBL号
            result.MAWBNO = ds.Tables[0].Rows[0]["MAWBNO"].ToString();
            if (type == BLType.HAWB) { result.HAWBNO = ds.Tables[0].Rows[0]["HAWBNO"].ToString(); }


            CustomerDescription customerDescription = new CustomerDescription();
            if (ds.Tables[0].Rows[0]["Shipper"] != null)
            {
                CustomerDescription shipperDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["Shipper"].ToString());
                if (shipperDscription != null) result.Shipper = shipperDscription.ToString(this.IsEnglish).ToUpper();
            }
            else
            {
                result.Shipper = customerDescription.ToString();
            }

            //result.Shipper = ds.Tables[0].Rows[0]["Shipper"].ToString();
            result.ShipperAccountNo = ds.Tables[0].Rows[0]["ShipperAccountNo"].ToString();

            if (ds.Tables[0].Rows[0]["Consignee"] != null)
            {
                CustomerDescription consigneeDscription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), ds.Tables[0].Rows[0]["Consignee"].ToString());
                if (consigneeDscription != null) result.Consignee = consigneeDscription.ToString(this.IsEnglish).ToUpper();
            }
            else
            {
                result.Consignee = customerDescription.ToString();
            }
            //result.Consignee = ds.Tables[0].Rows[0]["Consignee"].ToString();
            result.ConsigneeAccountNo = ds.Tables[0].Rows[0]["ConsigneeAccountNo"].ToString();
            result.AgentIATACode = ds.Tables[0].Rows[0]["AgentIATACode"].ToString();
            result.DepartureName = ds.Tables[0].Rows[0]["DepartureName"].ToString();
            result.TranshipmentPort1 = ds.Tables[0].Rows[0]["TranshipmentPort1"].ToString();
            result.Airline = ds.Tables[0].Rows[0]["Airline"].ToString();
            result.TranshipmentPort1By = ds.Tables[0].Rows[0]["TranshipmentPort1By"].ToString();
            result.TranshipmentPort2 = ds.Tables[0].Rows[0]["TranshipmentPort2"].ToString();
            result.TranshipmentPort2By = ds.Tables[0].Rows[0]["TranshipmentPort2By"].ToString();
            result.TranshipmentPort3 = ds.Tables[0].Rows[0]["TranshipmentPort3"].ToString();
            result.TranshipmentPort3By = ds.Tables[0].Rows[0]["TranshipmentPort3By"].ToString();
            result.DestinationName = ds.Tables[0].Rows[0]["DestinationName"].ToString();
            result.FlightNo = ds.Tables[0].Rows[0]["FlightNo"].ToString();
            result.FlightDate = ds.Tables[0].Rows[0]["FlightDate"].ToString();
            result.NumberOfOriginal = ds.Tables[0].Rows[0]["NumberOfOriginal"].ToString();
            result.AccountInformation = ds.Tables[0].Rows[0]["AccountInformation"].ToString();
            result.Currency = ds.Tables[0].Rows[0]["Currency"].ToString();
            result.CHGSCode = ds.Tables[0].Rows[0]["CHGSCode"].ToString();
            result.PaymentTerm = ds.Tables[0].Rows[0]["PaymentTerm"].ToString();
            result.OtherPaymentTerm = ds.Tables[0].Rows[0]["OtherPaymentTerm"].ToString();
            result.DeclaredValueForCarriage = ds.Tables[0].Rows[0]["DeclaredValueForCarriage"].ToString();
            result.DeclaredValueForCustoms = ds.Tables[0].Rows[0]["DeclaredValueForCustoms"].ToString();
            result.AmountofInsurance = ds.Tables[0].Rows[0]["AmountofInsurance"].ToString();
            result.NoofPiecesRCP = ds.Tables[0].Rows[0]["NoofPiecesRCP"].ToString();
            result.GrossWeight = ds.Tables[0].Rows[0]["GrossWeight"].ToString();
            result.Kglb = ds.Tables[0].Rows[0]["Kglb"].ToString();
            result.RateClass = ds.Tables[0].Rows[0]["RateClass"].ToString();
            result.CommodityItemNo = ds.Tables[0].Rows[0]["CommodityItemNo"].ToString();
            result.ChargeableWeight = ds.Tables[0].Rows[0]["ChargeableWeight"].ToString();
            result.RateCharge = ds.Tables[0].Rows[0]["RateCharge"].ToString();
            result.Total = ds.Tables[0].Rows[0]["Total"].ToString();
            result.GoodsDescription = ds.Tables[0].Rows[0]["GoodsDescription"].ToString();
            result.PrepaidWeightCharge = ds.Tables[0].Rows[0]["PrepaidWeightCharge"].ToString();
            result.CollectWeightCharge = ds.Tables[0].Rows[0]["CollectWeightCharge"].ToString();
            result.PrepaidValuationCharge = ds.Tables[0].Rows[0]["PrepaidValuationCharge"].ToString();
            result.CollectValuationCharge = ds.Tables[0].Rows[0]["CollectValuationCharge"].ToString();
            result.PrepaidTax = ds.Tables[0].Rows[0]["PrepaidTax"].ToString();
            result.CollectTax = ds.Tables[0].Rows[0]["CollectTax"].ToString();
            result.PrepaidTotalotherAgentCharges = ds.Tables[0].Rows[0]["PrepaidTotalotherAgentCharges"].ToString();
            result.CollectTotalotherAgentCharges = ds.Tables[0].Rows[0]["CollectTotalotherAgentCharges"].ToString();
            result.PrepaidTotalotherCarrierCharges = ds.Tables[0].Rows[0]["PrepaidTotalotherCarrierCharges"].ToString();
            result.CollectTotalotherCarrierCharges = ds.Tables[0].Rows[0]["CollectTotalotherCarrierCharges"].ToString();
            result.TotalPrepaid = ds.Tables[0].Rows[0]["TotalPrepaid"].ToString();
            result.TotalCollect = ds.Tables[0].Rows[0]["TotalCollect"].ToString();
            result.OtherChargers = ds.Tables[0].Rows[0]["OtherChargers"].ToString();
            result.CurrencyConversionRate = ds.Tables[0].Rows[0]["CurrencyConversionRate"].ToString();
            result.DestinationCurrencyAmount = ds.Tables[0].Rows[0]["DestinationCurrencyAmount"].ToString();
            result.ChargesAtDestination = ds.Tables[0].Rows[0]["ChargesAtDestination"].ToString();
            result.HandlingInformation = ds.Tables[0].Rows[0]["HandlingInformation"].ToString();
            result.IssueDate = ds.Tables[0].Rows[0]["IssueDate"].ToString();
            result.IssuePlace = ds.Tables[0].Rows[0]["IssuePlace"].ToString();
            result.Agent = ds.Tables[0].Rows[0]["Agent"].ToString();
            result.Marks = ds.Tables[0].Rows[0]["Marks"].ToString();

            return result;
        }
        /// <summary>
        /// 提单HBL报表
        /// </summary>
        /// <param name="HBLId">分提单ID</param>
        /// <param name="isEnglish">是否英文环境</param>
        /// <returns></returns>
        public BLReportData_HBL GetHBLReportData(Guid HBLId, bool isEnglish)
        {

            return null;
        }

        /// <summary>
        /// 获取空运出口业务联单报表数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public AEOrderReportData GetAEOrderReportData(Guid bookID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderReportData");

                db.AddInParameter(dbCommand, "@AirBookingID", DbType.Guid, bookID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AEOrderReportData result = (from b in ds.Tables[0].AsEnumerable()
                                            select new AEOrderReportData
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
                                                ShipperDescription = b.IsNull("ShipperDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")).ToString(this.IsEnglish),
                                                AgentName = b.Field<string>("AgentName"),
                                                AgentDescription = b.IsNull("AgentDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")).ToString(this.IsEnglish),
                                                BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                                ConsigneeName = b.Field<string>("ConsigneeName"),
                                                ConsigneeDescription = b.IsNull("ConsigneeDescription") ? string.Empty : SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")).ToString(this.IsEnglish),
                                                POLName = b.Field<string>("POLName"),
                                                PODName = b.Field<string>("PODName"),
                                                PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                                                VesselVoyageName = b.Field<string>("VesselVoyageName"),
                                                ETD = b.IsNull("ExpectedShipDate") ? (DateTime?)null : b.Field<DateTime>("ExpectedShipDate"),
                                                ClosingDate = b.IsNull("ClosingDate") ? (DateTime?)null : b.Field<DateTime>("ClosingDate"),
                                                IsCustoms = b.Field<Boolean>("IsCustoms"),
                                                IsTruck = b.Field<Boolean>("IsTruck"),
                                                IsCommodityInspection = b.Field<Boolean>("IsCommodityInspection"),
                                                TransportClause = b.Field<string>("TransportClause"),
                                                TradeTerm = b.Field<string>("TradeTerm"),
                                                MBLPaymentType = b.Field<string>("MBLPaymentType"),
                                                MBLRequest = b.Field<string>("MBLRequest"),
                                                HBLPaymentType = b.Field<string>("HBLPaymentType"),
                                                HBLRequest = b.Field<string>("HBLRequest"),
                                                SalesName = b.Field<string>("SalesName"),
                                                GoodsMarks = b.Field<string>("GoodsMarks"),
                                                GoodsQty = b.Field<int>("Quantity").ToString() + b.Field<string>("GoodsQty"),
                                                GoodsWeight = b.Field<decimal>("Weight").ToString("F3") + b.Field<string>("GoodsWeight"),
                                                GoodsMeasurement = b.Field<decimal>("Measurement").ToString("F3") + b.Field<string>("GoodsMeasurement"),
                                                ContainerRequest = b.Field<string>("ContainerRequest"),
                                                Remark = b.Field<string>("Remark"),
                                                OperatorName = b.Field<string>("OperatorName"),
                                                AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                IsOutMBL = b.Field<bool>("IsOutMBL"),
                                                IsQuarantineInspection = b.Field<Boolean>("IsQuarantineInspection"),
                                                IsWarehouse = b.Field<Boolean>("IsWarehouse"),
                                                IsOutHBL = b.Field<int>("IsOutHBL") == 0 ? false : true,
                                                OperationType = string.Empty,
                                                ReleaseType = BuildRealseType((FCMReleaseType)b.Field<int>("ReleaseType"), (FCMReleaseType)b.Field<int>("ReleaseType")),
                                                Fees = (from f in ds.Tables[1].AsEnumerable()
                                                        select new AEOrderFeeReportData
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
            sb.AppendLine("MAWB:" + mblReleaseTypeDesc);
            sb.AppendLine("HAWB:" + hblReleaseTypeDesc);
            return sb.ToString();
        }
    }
}

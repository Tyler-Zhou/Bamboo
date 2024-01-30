using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using System.Transactions;

namespace ICP.FCM.AirExport.ServiceComponent
{
    partial class AirExportService
    {
        #region get List
        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="blIDs">blIDs</param>
        /// <returns>返回提单列表</returns>
        public List<AirBLList> GetAirBLListByIDs(
            Guid[] blIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(blIDs, "blIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBLListByIDs");

                string tempBlIDs = blIDs.Join();

                db.AddInParameter(dbCommand, "@BLIDs", DbType.String, tempBlIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBLList> results = BulidBLListByDataSet(ds);

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
        /// 获取提单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="shippingOrderNo">订舱号</param>
        /// <param name="scno">合约号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="agentOfCarrierName">承运人名</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">目的港名</param>
        /// <param name="salesID">业务员</param>
        /// <param name="filerID">文件</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回提单列表</returns>
        public List<AirBLList> GetAirBLList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string filightNo,
            string scno,
            string customerName,
            string checkerName,
            string airCompanyName,
            string agentOfCarrierName,
            string departureName,
            string detinationName,
            string placeOfDelivery,
            Guid? salesID,
            Guid? filerID,
            AEBLState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBLList");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@RefNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@FlightNo", DbType.String, filightNo);
                db.AddInParameter(dbCommand, "@Scno", DbType.String, scno);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@CheckerName", DbType.String, string.Empty);
                db.AddInParameter(dbCommand, "@AirlineName", DbType.String, airCompanyName);
                db.AddInParameter(dbCommand, "@AgentOfCarrierName", DbType.String, agentOfCarrierName);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, departureName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, detinationName);
                db.AddInParameter(dbCommand, "@FinalDestinationName", DbType.String, placeOfDelivery);

                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, filerID);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBLList> results = BulidBLListByDataSet(ds);

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
        /// 获取提单列表
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
        /// <returns>返回提单列表</returns>
        public List<AirBLList> GetAirBLListForFaster(
            Guid[] companyIDs,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBLListForFaster");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@NoSearchType", DbType.Int16, noSearchType);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerSearchType", DbType.Int16, customerSearchType);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@LocationSearchType", DbType.Int16, portSearchType);
                db.AddInParameter(dbCommand, "@LocationName", DbType.String, portName);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBLList> results = BulidBLListByDataSet(ds);

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

        private static List<AirBLList> BulidBLListByDataSet(DataSet ds)
        {
            List<AirBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                       select new AirBLList
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           MBLID = b.IsNull("MBLID") ? Guid.Empty : b.Field<Guid>("MBLID"),
                                           AirBookingID = b.Field<Guid>("AirBookingID"),
                                           CompanyID = b.Field<Guid>("CompanyID"),
                                           RefNo = b.Field<string>("RefNo"),
                                           No = b.Field<string>("BLNo"),
                                           CheckerName = b.Field<string>("CheckerName"),
                                           ShipperName = b.Field<string>("ShipperName"),
                                           ConsigneeName = b.Field<string>("ConsigneeName"),
                                           AgentName = b.Field<string>("AgentName"),
                                           AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                           AirCompanyName = b.Field<string>("AirlineName"),
                                           DepartureName = b.Field<string>("POLName"),
                                           DetinationName = b.Field<string>("PODName"),
                                           PlaceOfDeliveryName = b.Field<string>("FinalDestinationName"),
                                           FilightNo = b.Field<string>("FlightNo"),
                                           ETD = b.Field<DateTime?>("ETD"),
                                           ETA = b.Field<DateTime?>("ETA"),
                                           State = (AEBLState)b.Field<byte>("State"),
                                           BookingerName = b.Field<string>("BookingerName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           UpdateByName = b.Field<string>("UpdateByName"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           ShipperID = b.Field<Guid?>("ShipperID"),
                                           ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                           CustomerName = b.Field<string>("CustomerName"),
                                           ExistFees = b.Field<bool>("ExistFees"),
                                           FilerName = b.Field<string>("FilerName"),
                                           NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                           SalesName = b.Field<string>("SalesName"),
                                           NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                           HBLCount = b.Field<int>("HBLCount"),
                                           Quantity = b.Field<int>("Quantity"),
                                           GrossKGS = b.Field<decimal>("Weight"),
                                           GrossLBS = (b.Field<decimal>("Weight")) * 2.20462M,
                                           ChargeKGS = b.Field<decimal>("ChargeableWeight"),
                                           ChargeLBS = (b.Field<decimal>("ChargeableWeight")) * 2.20462M,
                                           IsClosed = b.Field<bool>("IsClosed"),
                                           RBLD=b.Field<bool>("RBLD"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           AirOrderID = b.Field<Guid?>("AirOrderID"),
                                           AirOrderUpdateDate = b.Field<DateTime?>("AirOrderUpdateDate"),
                                           IsDirty = false
                                       }).ToList();
            return results;
        }

        #endregion

        #region MBL

        /// <summary>
        /// 获取MBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回MBL提单信息</returns>
        public AirMBLInfo GetAirMBLInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirMBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AirMBLInfo result = (from b in ds.Tables[0].AsEnumerable()
                                     select new AirMBLInfo
                                     {
                                         ID = b.Field<Guid>("ID"),
                                         MBLID = b.Column<Guid>("ID"),
                                         CompanyID = b.Column<Guid>("CompanyID"),
                                         AirBookingID = b.Field<Guid>("AirBookingID"),
                                         No = b.Field<string>("MAWBNo"),
                                         HAWBNos = b.Field<string>("HAWBNo"),
                                         RefNo = b.Field<string>("ReferenceNo"),
                                         FilightNoID = b.Field<Guid?>("FlightID"),
                                         FilightNo = b.Field<string>("FlightNo"),
                                         ShipperName = b.Field<string>("ShipperName"),
                                         ConsigneeName = b.Field<string>("ConsigneeName"),
                                         NumberOfOriginal = (short?)b.Field<byte>("NumberOfOriginal"),
                                         CheckerID = b.Field<Guid?>("CheckerID"),
                                         CheckerName = b.Field<string>("CheckerName"),
                                         ShipperID = b.Column<Guid>("ShipperID"),
                                         ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                         ShipperAccountNo = b.Field<string>("ShipperAccountNo"),
                                         ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                         ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                         ConsigneeAccountNo = b.Field<string>("ConsigneeAccountNo"),
                                         NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                         NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                         NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                         AgentID = b.Field<Guid?>("AgentID"),
                                         AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                         AgentAccountNo = b.Field<string>("AgentAccountNo"),
                                         AgentName = b.Field<string>("AgentName"),
                                         ETD = b.Field<DateTime?>("ETD"),
                                         TranshipmentPort1 = b.Field<string>("TranshipmentPort1"),
                                         TranshipmentPort1By = b.Field<string>("TranshipmentPort1By"),
                                         TranshipmentPort2 = b.Field<string>("TranshipmentPort2"),
                                         TranshipmentPort2By = b.Field<string>("TranshipmentPort2By"),
                                         TranshipmentPort3 = b.Field<string>("TranshipmentPort3"),
                                         TranshipmentPort3By = b.Field<string>("TranshipmentPort3By"),
                                         DetinationCode = b.Field<string>("DestinationCode"),
                                         DepartureCode = b.Field<string>("DepartureCode"),
                                         DetinationID = b.Column<Guid>("DestinationID"),
                                         DepartureID = b.Column<Guid>("DepartureID"),
                                         DepartureName = b.Field<string>("DepartureName"),
                                         DetinationName = b.Field<string>("DestinationName"),
                                         ETA = b.Field<DateTime?>("ETA"),
                                         PlaceOfDeliveryID = b.Field<Guid?>("FinalDestinationID"),
                                         PlaceOfDeliveryCode = b.Field<string>("FinalDestinationCode"),
                                         PlaceOfDeliveryName = b.Field<string>("FinalDestinationName"),
                                         PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                         PaymentTermName = b.Field<string>("PaymentTermName"),
                                         OtherPaymentTermName = b.Field<string>("OtherPaymentTermName"),
                                         OtherPaymentTermID = b.Field<Guid?>("OtherPaymentTermID"),
                                         InsuranceAmount = b.Field<string>("InsuranceAmount"),
                                         FreightDescription = b.Field<string>("AccountInformation"),
                                         ReleaseType = (FCMReleaseType)b.Field<byte>("ReleaseType"),
                                         ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                         Quantity = b.Field<int>("Quantity"),
                                         QuantityUnitID = b.Column<Guid>("QuantityUnitID"),
                                         QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                         GrossKGS = b.Field<decimal>("Weight"),
                                         GrossLBS = (b.Field<decimal>("Weight")) * 2.20462M,
                                         ChargeKGS = b.Field<decimal>("ChargeableWeight"),
                                         ChargeLBS = (b.Field<decimal>("ChargeableWeight")) * 2.20462M,
                                         AgentOfCarrierID = b.Column<Guid?>("AgentOfCarrierID"),
                                         AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                         Measurement = b.Field<decimal>("Measurement"),
                                         MeasurementUnitID = b.Column<Guid>("MeasurementUnitID"),
                                         MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                         Marks = b.Field<string>("Marks"),
                                         GoodsDescription = b.Field<string>("GoodsDescription"),
                                         HandingInformation = b.Field<string>("HandlingInformation"),
                                         //AccountInformation = b.Field<string>("AccountInformation"),
                                         OtherChargeDescription = b.Field<string>("OtherChargeDescription"),
                                         IssuePlaceID = b.Column<Guid>("IssuePlaceID"),
                                         IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                         IssueByID = b.Column<Guid>("IssueByID"),
                                         IssueByName = b.Field<string>("IssueByName"),
                                         IssueDate = b.Field<DateTime?>("IssueDate"),
                                         State = (AEBLState)b.Field<byte>("State"),
                                         CurrencyID = b.Field<Guid>("CurrencyID"),
                                         IATACurrencyID = b.Field<Guid?>("IATACurrencyID"),
                                         RateCharge = b.Field<decimal>("RateCharge"),
                                         IATARateCharge = b.Field<decimal>("IATARateCharge"),
                                         RateAmount = b.Field<decimal>("RateAmount"),
                                         IATARateAmount = b.Field<decimal>("IATARateAmount"),
                                         AirCompanyName = b.Field<string>("AirlineName"),
                                         AirCompanyID = b.Field<Guid>("AirlineID"),
                                         //CustomerName = b.Field<string>("CustomerName"),
                                         //FilerName = b.Field<string>("FilerName"),
                                         //BookingerName = b.Field<string>("BookingerName"),
                                         //SalesName = b.Field<string>("SalesName"),
                                         DeclaredValueForCarriage = b.Field<string>("DeclaredValueForCarriage"),
                                         DeclaredValueForCustoms = b.Field<string>("DeclaredValueForCustoms"),
                                         ChargeableWeightUnitIsKGS = b.Field<bool>("ChargeableWeightUnitIsKGS"),
                                         IATAChargeableWeightUnitIsKGS = b.Field<bool>("IATAChargeableWeightUnitIsKGS"),
                                         ExistFees = b.Field<bool>("ExistFees"),
                                         CreateByID = b.Column<Guid>("CreateByID"),
                                         CreateByName = b.Field<string>("CreateByName"),
                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                         RateClass = (RateClass)b.Field<byte>("RateClass"),
                                         Tax = b.Field<decimal>("Tax"),
                                         ValuationCharge = b.Field<decimal>("ValuationCharge"),
                                         AgentCharger = b.Field<decimal>("AgentCharger"),
                                         CarrierCharger = b.Field<decimal>("CarrierCharger"),
                                         CurrencyConversionRate = b.Field<string>("CurrencyConversionRate"),
                                         DestinationCurrencyAmount = b.Field<string>("DestinationCurrencyAmount"),
                                         ChargesAtDestination = b.Field<string>("ChargesAtDestination"),
                                         AgentIATACode = b.Field<string>("AgentIATACode"),
                                         AirOrderID = b.Column<Guid?>("AirOrderID"),
                                         AirOrderUpdateDate = b.Field<DateTime?>("AirOrderUpdateDate"),
                                         IsRequestAgent = b.Field<bool>("IsRequestAgent"),
                                         //IsValid = b.Field<bool>("IsValid"),
                                         BookingPaymentTermID = b.Field<Guid?>("BookingPaymentTermID"),
                                         IsDirty = false,
                                         OtherChargeList = b.IsNull("OtherChargers") ? new List<OtherChargeItem>() : (from d in ICP.Framework.CommonLibrary.Helper.SerializerHelper.DeserializeFromString<OtherChargeXML>(typeof(OtherChargeXML), b.Field<string>("OtherChargers")).Items
                                                                                                                      select new OtherChargeItem
                                                                                                                       {
                                                                                                                           ChargeName = d.ChargeName,
                                                                                                                           Amount = Decimal.Parse(d.Amount)
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
        /// 保存MBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="oceanBookingID">订舱ID</param>
        /// <param name="mblNo">MBL号</param>
        /// <param name="numberOfOriginal">提单份数</param>
        /// <param name="voyageShowType">航次显示类型</param>
        /// <param name="checkerID">对单人ID</param>
        /// <param name="shipperID">发货人ID</param>
        /// <param name="shipperDescription">发货人描述</param>
        /// <param name="consigneeID">收货人ID</param>
        /// <param name="consigneeDescription">收货人描述</param>
        /// <param name="notifyPartyID">通知人ID</param>
        /// <param name="notifyPartyDescription">通知人描述</param>
        /// <param name="agentID">代理ID</param>
        /// <param name="agentDescription">代理描述</param>
        /// <param name="placeOfReceiptID">收货地ID</param>
        /// <param name="placeOfReceiptName">收货地名</param>
        /// <param name="preVoyageID">头程船名航次ID</param>
        /// <param name="voyageID">船名航次ID</param>
        /// <param name="polID">装货港ID</param>
        /// <param name="polName">二程装货港名</param>
        /// <param name="podID">二程卸货港ID</param>
        /// <param name="podName">二程卸货港名</param>
        /// <param name="placeOfDeliveryID">交货地ID</param>
        /// <param name="placeOfDeliveryName">交货地名</param>
        /// <param name="finalDestinationID">最终目的地ID</param>
        /// <param name="finalDestinationName">最终目的地名</param>
        /// <param name="transportClauseID">运输条款ID</param>
        /// <param name="paymentTermID">付款方式ID</param>
        /// <param name="freightDescription">费用描述</param>
        /// <param name="releaseType">电放方式</param>
        /// <param name="releaseDate">电放日期</param>
        /// <param name="quantity">数量</param>
        /// <param name="quantityUnitID">数量单位</param>
        /// <param name="weight">重量</param>
        /// <param name="weightUnitID">重量单位</param>
        /// <param name="measurement">体积</param>
        /// <param name="measurementUnitID">体积单位</param>
        /// <param name="marks">货物标示</param>
        /// <param name="goodsDescription">货物描述</param>
        /// <param name="isWoodPacking">是否木质包装</param>
        /// <param name="ctnQtyInfo">箱数或件数合计</param>
        /// <param name="issuePlaceID">签发地ID</param>
        /// <param name="issueByID">签发人ID</param>
        /// <param name="issueDate">签发日期</param>
        /// <param name="woodPacking">木质包装的字符串</param>
        /// <param name="issueType">签单类型</param>
        /// <param name="agentText">代理文本(用于打印)</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        SingleResult SaveAirMBLInfo(
            Guid? id,
            Guid oceanBookingID,
            string mblNo,
            short? numberOfOriginal,
            Guid? checkerID,
            Guid? shipperID,
            CustomerDescription shipperDescription,
            string shipperAccountNo,
            Guid? consigneeID,
            CustomerDescription consigneeDescription,
            string consigneeAccountNo,
            Guid? notifyPartyID,
            CustomerDescription notifyPartyDescription,
            Guid? agentID,
            CustomerDescription agentDescription,
            string agentAccountNo,
            string iATACode,
            Guid? agentOfCarrierID,
            Guid departureID,
            string departureName,
            DateTime? etd,
            Guid destinationID,
            string destinationName,
            DateTime? eta,
            Guid? placeOfDeliveryID,
            string transhipmentPort1,
            string transhipmentPort1By,
            string transhipmentPort2,
            string transhipmentPort2By,
            string transhipmentPort3,
            string transhipmentPort3By,
            Guid? paymentTermID,
            Guid? otherPaymentTermID,
            string declaredValueForCarriage,
            string declaredValueForCustoms,
            string insuranceAmount,
            int quantity,
            Guid quantityUnitID,
            decimal weight,
            decimal measurement,
            Guid measurementUnitID,
            RateClass? rateClass,
            decimal chargeableWeight,
            Guid? currencyID,
            decimal rateCharge,
            decimal rateAmount,
            decimal iATARateCharge,
            Guid? iATACurrencyID,
            decimal iATARateAmount,
            Guid? airCompanyID,
            Guid? flightID,
            string freightDescription,
            string handlingInformation,
            FCMReleaseType releaseType,
            DateTime? releaseDate,
            DateTime? bLDate,
            string marks,
            string goodsDescription,
            string otherChargers,
            string otherChargeDescription,
            decimal valuationCharge,
            decimal tax,
            decimal agentCharger,
            decimal carrierCharger,
            string currencyConversionRate,
            string destinationCurrencyAmount,
            string chargesAtDestination,
            Guid issuePlaceID,
            Guid issueByID,
            DateTime? issueDate,
            bool chargeableWeightUnitIsKGS,
            bool iATAChargeableWeightUnitIsKGS,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanShippingOrderID");
            ArgumentHelper.AssertGuidNotEmpty(quantityUnitID, "quantityUnitID");
            ArgumentHelper.AssertGuidNotEmpty(measurementUnitID, "measurementUnitID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAirMBLInfo");

                string tempagentDescription = SerializerHelper.SerializeToString<CustomerDescription>(agentDescription, true, false);
                string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(shipperDescription, true, false);
                string tempconsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(consigneeDescription, true, false);
                string tempnotifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(notifyPartyDescription, true, false);

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@AirBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@No", DbType.String, mblNo);
                db.AddInParameter(dbCommand, "@NumberOfOriginal", DbType.Int32, numberOfOriginal);
                db.AddInParameter(dbCommand, "@CheckerID", DbType.Guid, checkerID);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, shipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                db.AddInParameter(dbCommand, "@ShipperAccountNo", DbType.String, shipperAccountNo);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, consigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempconsigneeDescription);
                db.AddInParameter(dbCommand, "@ConsigneeAccountNo", DbType.String, consigneeAccountNo);
                db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, notifyPartyID);
                db.AddInParameter(dbCommand, "@NotifyPartyDescription", DbType.Xml, tempnotifyPartyDescription);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, agentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, tempagentDescription);
                db.AddInParameter(dbCommand, "@AgentAccountNo", DbType.String, agentAccountNo);
                db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, agentOfCarrierID);
                db.AddInParameter(dbCommand, "@IATACode", DbType.String, iATACode);
                db.AddInParameter(dbCommand, "@DepartureID", DbType.Guid, departureID);
                db.AddInParameter(dbCommand, "@DepartureName", DbType.String, departureName);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, etd);
                db.AddInParameter(dbCommand, "@DestinationID", DbType.Guid, destinationID);
                db.AddInParameter(dbCommand, "@DestinationName", DbType.String, destinationName);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, eta);  
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, placeOfDeliveryID);
                db.AddInParameter(dbCommand, "@TranshipmentPort1", DbType.String, transhipmentPort1);
                db.AddInParameter(dbCommand, "@TranshipmentPort1By", DbType.String, transhipmentPort1By);
                db.AddInParameter(dbCommand, "@TranshipmentPort2", DbType.String, transhipmentPort2);
                db.AddInParameter(dbCommand, "@TranshipmentPort2By", DbType.String, transhipmentPort2By);
                db.AddInParameter(dbCommand, "@TranshipmentPort3", DbType.String, transhipmentPort3);
                db.AddInParameter(dbCommand, "@TranshipmentPort3By", DbType.String, transhipmentPort3By);

                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, paymentTermID);
                db.AddInParameter(dbCommand, "@OtherPaymentTermID", DbType.Guid, otherPaymentTermID);
                db.AddInParameter(dbCommand, "@DeclaredValueForCarriage", DbType.String, declaredValueForCarriage);
                db.AddInParameter(dbCommand, "@DeclaredValueForCustoms", DbType.String, declaredValueForCustoms);
                db.AddInParameter(dbCommand, "@InsuranceAmount", DbType.String, insuranceAmount);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, quantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, weight);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, measurementUnitID);
                db.AddInParameter(dbCommand, "@RateClass", DbType.Int16, (short?)rateClass);
                db.AddInParameter(dbCommand, "@ChargeableWeight", DbType.Decimal, chargeableWeight);
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, currencyID);
                db.AddInParameter(dbCommand, "@RateCharge", DbType.Decimal, rateCharge);
                db.AddInParameter(dbCommand, "@RateAmount", DbType.Decimal, rateAmount);
                db.AddInParameter(dbCommand, "@IATARateCharge", DbType.Decimal, iATARateCharge);
                db.AddInParameter(dbCommand, "@IATACurrencyID", DbType.Guid, iATACurrencyID);
                db.AddInParameter(dbCommand, "@IATARateAmount", DbType.Decimal, iATARateAmount);

                db.AddInParameter(dbCommand, "@HandlingInformation", DbType.String, handlingInformation);
                db.AddInParameter(dbCommand, "@AccountInformation", DbType.String, freightDescription);
                db.AddInParameter(dbCommand, "@OtherChargers", DbType.String, otherChargers);
                db.AddInParameter(dbCommand, "@OtherChargeDescription", DbType.String, otherChargeDescription);
                db.AddInParameter(dbCommand, "@ValuationCharge", DbType.Decimal, valuationCharge);
                db.AddInParameter(dbCommand, "@TAX", DbType.Decimal, tax);
                db.AddInParameter(dbCommand, "@AgentCharger", DbType.Decimal, agentCharger);
                db.AddInParameter(dbCommand, "@CarrierCharger", DbType.Decimal, carrierCharger);
                db.AddInParameter(dbCommand, "@CurrencyConversionRate", DbType.String, currencyConversionRate);
                db.AddInParameter(dbCommand, "@DestinationCurrencyAmount", DbType.String, destinationCurrencyAmount);
                db.AddInParameter(dbCommand, "@ChargesAtDestination", DbType.String, chargesAtDestination);
                db.AddInParameter(dbCommand, "@ChargeableWeightUnitIsKGS", DbType.Boolean, chargeableWeightUnitIsKGS);
                db.AddInParameter(dbCommand, "@IATAChargeableWeightUnitIsKGS", DbType.Boolean, iATAChargeableWeightUnitIsKGS);
                db.AddInParameter(dbCommand, "@AirLineID", DbType.Guid, airCompanyID);
                db.AddInParameter(dbCommand, "@FlightID", DbType.Guid, flightID);

                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Int16, releaseType);
                db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, releaseDate);
                db.AddInParameter(dbCommand, "@BLDate", DbType.DateTime, bLDate);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, marks);
                db.AddInParameter(dbCommand, "@GoodsDescription", DbType.String, goodsDescription);
                db.AddInParameter(dbCommand, "@IssuePlaceID", DbType.Guid, issuePlaceID);
                db.AddInParameter(dbCommand, "@IssueBy", DbType.Guid, issueByID);
                db.AddInParameter(dbCommand, "@IssueDate", DbType.DateTime, issueDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@BLUpdateDate", DbType.DateTime, updateDate);
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
        /// 删除MBL提单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveAirMBLInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAirMBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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
        /// 改变MBL状态
        /// </summary>
        /// <param name="id">MBL ID</param>
        /// <param name="state">状态（0草稿、1对单中、2、对单确认、3已完成）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeAirMBLState(
            Guid id,
            AEBLState state,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeAirMBLState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
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
        /// 更改订单的状态
        /// </summary>
        /// <param name="id">MBL ID</param>
        /// <param name="state">状态（0草稿、1对单中、2、对单确认、3已完成）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeBLAirOrderState(
            Guid orderID,
            AEOrderState state,
            string reason,
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
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@Reason", DbType.String, reason);
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

        ///// <summary>
        ///// 确认装船类型
        ///// </summary>
        ///// <param name="shippingOrderID">ShippingOrderID</param>
        ///// <param name="preVoyageID">preVoyageID</param>
        ///// <param name="voyageID">voyageID</param>
        ///// <param name="onBoardType">确认装船类型（0不确定，1前程确定，2确定）</param>
        ///// <param name="confirmByID">确认人</param>
        ///// <param name="updateDate">更新时间-做数据版本用</param>
        ///// <param name="shippingOrderUpdateDate">更新时间-做数据版本用</param>
        ///// <returns>返回SingleResult</returns>
        //public SingleResult ConfirmOnBoardType(
        //    Guid shippingOrderID,
        //    Guid? preVoyageID,
        //    Guid? voyageID,
        //    ConfirmOnBoardType onBoardType,
        //    Guid confirmByID,
        //    DateTime? shippingOrderUpdateDate)
        //{
        //    ArgumentHelper.AssertGuidNotEmpty(shippingOrderID, "shippingOrderID");
        //    ArgumentHelper.AssertGuidNotEmpty(confirmByID, "confirmByID");

        //    try
        //    {
        //        Database db = DatabaseFactory.CreateDatabase();
        //        DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspConfirmOnBoardType");

        //        db.AddInParameter(dbCommand, "@ShippingOrderID", DbType.Guid, shippingOrderID);
        //        db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, preVoyageID);
        //        db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, voyageID);
        //        db.AddInParameter(dbCommand, "@ShippingOrderUpdateDate", DbType.DateTime, shippingOrderUpdateDate);
        //        db.AddInParameter(dbCommand, "@OnBoardType", DbType.Int16, onBoardType);
        //        db.AddInParameter(dbCommand, "@ConfirmByID", DbType.Guid, confirmByID);
        //        db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

        //        SingleResult result = db.SingleResult(dbCommand, new string[] { "UpdateDate", "ShippingOrderUpdateDate" });
        //        return result;
        //    }
        //    catch (SqlException sqlException)
        //    {
        //        throw new ApplicationException(sqlException.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region HBL

        /// <summary>
        /// 获取HBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回HBL提单信息</returns>
        public AirHBLInfo GetAirHBLInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirHBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AirHBLInfo result = (from b in ds.Tables[0].AsEnumerable()
                                     select new AirHBLInfo
                                     {
                                         ID = b.Field<Guid>("ID"),
                                         MBLID = b.Column<Guid>("AirMAWBID"),
                                         CompanyID = b.Column<Guid>("CompanyID"),
                                         AirBookingID = b.Field<Guid>("AirBookingID"),
                                         MBLNo = b.Field<string>("MAWBNo"),
                                         No = b.Field<string>("HAWBNo"),
                                         RefNo = b.Field<string>("ReferenceNo"),
                                         FilightNoID = b.Field<Guid?>("FlightID"),
                                         FilightNo = b.Field<string>("FlightNo"),
                                         ShipperName = b.Field<string>("ShipperName"),
                                         ConsigneeName = b.Field<string>("ConsigneeName"),
                                         NumberOfOriginal = (short?)b.Field<byte>("NumberOfOriginal"),
                                         CheckerID = b.Field<Guid?>("CheckerID"),
                                         CheckerName = b.Field<string>("CheckerName"),
                                         ShipperID = b.Column<Guid>("ShipperID"),
                                         ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                         ShipperAccountNo = b.Field<string>("ShipperAccountNo"),
                                         ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                         ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                         ConsigneeAccountNo = b.Field<string>("ConsigneeAccountNo"),
                                         NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                         NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                         NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                         AgentID = b.Field<Guid?>("AgentID"),
                                         AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                         AgentAccountNo = b.Field<string>("AgentAccountNo"),
                                         AgentName = b.Field<string>("AgentName"),
                                         ETD = b.Field<DateTime?>("ETD"),
                                         TranshipmentPort1 = b.Field<string>("TranshipmentPort1"),
                                         TranshipmentPort1By = b.Field<string>("TranshipmentPort1By"),
                                         TranshipmentPort2 = b.Field<string>("TranshipmentPort2"),
                                         TranshipmentPort2By = b.Field<string>("TranshipmentPort2By"),
                                         TranshipmentPort3 = b.Field<string>("TranshipmentPort3"),
                                         TranshipmentPort3By = b.Field<string>("TranshipmentPort3By"),
                                         DetinationCode = b.Field<string>("DestinationCode"),
                                         DepartureCode = b.Field<string>("DepartureCode"),
                                         DetinationID = b.Column<Guid>("DestinationID"),
                                         DepartureID = b.Column<Guid>("DepartureID"),
                                         DepartureName = b.Field<string>("DepartureName"),
                                         DetinationName = b.Field<string>("DestinationName"),
                                         ETA = b.Field<DateTime?>("ETA"),
                                         PlaceOfDeliveryID = b.Field<Guid?>("FinalDestinationID"),
                                         PlaceOfDeliveryCode = b.Field<string>("FinalDestinationCode"),
                                         PlaceOfDeliveryName = b.Field<string>("FinalDestinationName"),
                                         PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                         PaymentTermName = b.Field<string>("PaymentTermName"),
                                         OtherPaymentTermName = b.Field<string>("OtherPaymentTermName"),
                                         OtherPaymentTermID = b.Field<Guid?>("OtherPaymentTermID"),
                                         InsuranceAmount = b.Field<string>("InsuranceAmount"),
                                         FreightDescription = b.Field<string>("AccountInformation"),
                                         ReleaseType = (FCMReleaseType)b.Field<byte>("ReleaseType"),
                                         ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                         Quantity = b.Field<int>("Quantity"),
                                         QuantityUnitID = b.Column<Guid>("QuantityUnitID"),
                                         QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                         GrossKGS = b.Field<decimal>("Weight"),
                                         GrossLBS = (b.Field<decimal>("Weight")) * 2.20462M,
                                         ChargeKGS = b.Field<decimal>("ChargeableWeight"),
                                         ChargeLBS = (b.Field<decimal>("ChargeableWeight")) * 2.20462M,
                                         AgentOfCarrierID = b.Column<Guid?>("AgentOfCarrierID"),
                                         AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                         Measurement = b.Field<decimal>("Measurement"),
                                         MeasurementUnitID = b.Column<Guid>("MeasurementUnitID"),
                                         MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                         Marks = b.Field<string>("Marks"),
                                         GoodsDescription = b.Field<string>("GoodsDescription"),
                                         HandingInformation = b.Field<string>("HandlingInformation"),
                                         //AccountInformation = b.Field<string>("AccountInformation"),
                                         OtherChargeDescription = b.Field<string>("OtherChargeDescription"),
                                         IssuePlaceID = b.Column<Guid>("IssuePlaceID"),
                                         IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                         IssueByID = b.Column<Guid>("IssueByID"),
                                         IssueByName = b.Field<string>("IssueByName"),
                                         IssueDate = b.Field<DateTime?>("IssueDate"),
                                         State = (AEBLState)b.Field<byte>("State"),
                                         CurrencyID = b.Field<Guid>("CurrencyID"),
                                         RateCharge = b.Field<decimal>("RateCharge"),
                                         RateAmount = b.Field<decimal>("RateAmount"),
                                         AirCompanyName = b.Field<string>("AirlineName"),
                                         AirCompanyID = b.Field<Guid>("AirlineID"),
                                         DeclaredValueForCarriage = b.Field<string>("DeclaredValueForCarriage"),
                                         DeclaredValueForCustoms = b.Field<string>("DeclaredValueForCustoms"),
                                         ChargeableWeightUnitIsKGS = b.Field<bool>("ChargeableWeightUnitIsKGS"),
                                         ExistFees = b.Field<bool>("ExistFees"),
                                         CreateByID = b.Column<Guid>("CreateByID"),
                                         CreateByName = b.Field<string>("CreateByName"),
                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                         RateClass = (RateClass)b.Field<byte>("RateClass"),
                                         Tax = b.Field<decimal>("Tax"),
                                         ValuationCharge = b.Field<decimal>("ValuationCharge"),
                                         AgentCharger = b.Field<decimal>("AgentCharger"),
                                         CarrierCharger = b.Field<decimal>("CarrierCharger"),
                                         CurrencyConversionRate = b.Field<string>("CurrencyConversionRate"),
                                         DestinationCurrencyAmount = b.Field<string>("DestinationCurrencyAmount"),
                                         ChargesAtDestination = b.Field<string>("ChargesAtDestination"),
                                         AgentIATACode = b.Field<string>("AgentIATACode"),
                                         AirOrderID = b.Column<Guid?>("AirOrderID"),
                                         AirOrderUpdateDate = b.Field<DateTime?>("AirOrderUpdateDate"),
                                         IsRequestAgent = b.Field<bool>("IsRequestAgent"),
                                         //IsValid = b.Field<bool>("IsValid"),
                                         BookingPaymentTermID = b.Field<Guid?>("BookingPaymentTermID"),
                                         IsDirty = false,
                                         OtherChargeList = b.IsNull("OtherChargers") ? new List<OtherChargeItem>() : (from d in ICP.Framework.CommonLibrary.Helper.SerializerHelper.DeserializeFromString<OtherChargeXML>(typeof(OtherChargeXML), b.Field<string>("OtherChargers")).Items
                                                                                                                      select new OtherChargeItem
                                                                                                                      {
                                                                                                                          ChargeName = d.ChargeName,
                                                                                                                          Amount = Decimal.Parse(d.Amount)
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
        /// 保存HBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="oceanBookingID">订舱ID</param>
        /// <param name="mblID">mblID</param>
        /// <param name="mblNO">mblNO,如果传入新的MBLNO则在系统自动生成一个新的MBL</param>
        /// <param name="hblNo">HBL提单号</param>
        /// <param name="numberOfOriginal">提单份数</param>
        /// <param name="voyageShowType">航次显示类型</param>
        /// <param name="checkerID">对单人ID</param>
        /// <param name="shipperID">发货人ID</param>
        /// <param name="shipperDescription">发货人描述</param>
        /// <param name="consigneeID">收货人ID</param>
        /// <param name="consigneeDescription">收货人描述</param>
        /// <param name="notifyPartyID">通知人ID</param>
        /// <param name="notifyPartyDescription">通知人描述</param>
        /// <param name="agentID">通知人ID</param>
        /// <param name="agentDescription">通知人描述</param>
        /// <param name="placeOfReceiptID">收货地ID</param>
        /// <param name="placeOfReceiptName">收货地名</param>
        /// <param name="preVoyageID">头程船名航次</param>
        /// <param name="voyageID">二程航次ID</param>
        /// <param name="polID">二程装货港ID</param>
        /// <param name="polName">二程装货港名</param>
        /// <param name="podID">二程卸货港ID</param>
        /// <param name="podName">二程卸货港名</param>
        /// <param name="placeOfDeliveryID">交货地ID</param>
        /// <param name="placeOfDeliveryName">交货地名</param>
        /// <param name="finalDestinationID">最终目的地ID</param>
        /// <param name="finalDestinationName">最终目的地名</param>
        /// <param name="transportClauseID">运输条款ID</param>
        /// <param name="paymentTermID">付款方式ID</param>
        /// <param name="freightDescription">费用描述</param>
        /// <param name="releaseType">电放方式</param>
        /// <param name="releaseDate">电放日期</param>
        /// <param name="quantity">数量</param>
        /// <param name="quantityUnitID">数量单位</param>
        /// <param name="weight">重量</param>
        /// <param name="weightUnitID">重量单位</param>
        /// <param name="measurement">体积</param>
        /// <param name="measurementUnitID">体积单位</param>
        /// <param name="marks">货物标示</param>
        /// <param name="goodsDescription">货物描述</param>
        /// <param name="isWoodPacking">是否木质包装</param>
        /// <param name="ctnQtyInfo">箱数或件数合计</param>
        /// <param name="issuePlaceID">签发地ID</param>
        /// <param name="issueByID">签发人D</param>
        /// <param name="issueDate">签发日期</param>
        /// <param name="amsNo">AMS No</param>
        /// <param name="amsShipperDescription">AMS发货人描述</param>
        /// <param name="amsConsigneeDescription">AMS收货人描述</param>
        /// <param name="amsNotifyPartyDescription">AMS通知人描述</param>
        /// <param name="isfNo">ISF No</param>
        /// <param name="aciEntryType">ACI 进口类型</param>
        /// <param name="amsEntryType">AMS 进口类型</param>
        /// <param name="woodPacking">木质包装的字符串</param>
        /// <param name="issueType">签单类型</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回 "ID", "UpdateDate", "No" ,"AirMBLID"</returns>
        SingleResult SaveAirHBLInfo(
            Guid? id,
            Guid oceanBookingID,
            Guid? mblId,
            string mblNo,
            string hblNo,
            short? numberOfOriginal,
            Guid? checkerID,
            Guid? shipperID,
            CustomerDescription shipperDescription,
            string shipperAccountNo,
            Guid? consigneeID,
            CustomerDescription consigneeDescription,
            string consigneeAccountNo,
            Guid? notifyPartyID,
            CustomerDescription notifyPartyDescription,
            Guid? agentID,
            CustomerDescription agentDescription,
            string agentAccountNo,
            string iATACode,
            Guid? agentOfCarrierID,
            Guid departureID,
            string departureName,
            DateTime? etd,
            Guid destinationID,
            string destinationName,
            DateTime? eta,
            Guid? placeOfDeliveryID,
            string transhipmentPort1,
            string transhipmentPort1By,
            string transhipmentPort2,
            string transhipmentPort2By,
            string transhipmentPort3,
            string transhipmentPort3By,
            Guid? paymentTermID,
            Guid? otherPaymentTermID,
            string declaredValueForCarriage,
            string declaredValueForCustoms,
            string insuranceAmount,
            int quantity,
            Guid quantityUnitID,
            decimal weight,
            decimal measurement,
            Guid measurementUnitID,
            RateClass? rateClass,
            decimal chargeableWeight,
            Guid? currencyID,
            decimal rateCharge,
            decimal rateAmount,
            Guid? airCompanyID,
            Guid? flightID,
            string freightDescription,
            string handlingInformation,
            FCMReleaseType releaseType,
            DateTime? releaseDate,
            DateTime? bLDate,
            string marks,
            string goodsDescription,
            string otherChargers,
            string otherChargeDescription,
            decimal valuationCharge,
            decimal tax,
            decimal agentCharger,
            decimal carrierCharger,
            string currencyConversionRate,
            string destinationCurrencyAmount,
            string chargesAtDestination,
            Guid issuePlaceID,
            Guid issueByID,
            DateTime? issueDate,
            bool chargeableWeightUnitIsKGS,
            Guid saveByID,
            DateTime? updateDate,
            DateTime? mblUpdateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanShippingOrderID");
            ArgumentHelper.AssertGuidNotEmpty(quantityUnitID, "quantityUnitID");
            ArgumentHelper.AssertGuidNotEmpty(measurementUnitID, "measurementUnitID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                SingleResult result = null;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAirHBLInfo");

                    #region Parameter
                    string tempagentDescription = SerializerHelper.SerializeToString<CustomerDescription>(agentDescription, true, false);
                    string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(shipperDescription, true, false);
                    string tempconsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(consigneeDescription, true, false);
                    string tempnotifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(notifyPartyDescription, true, false);

                    db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                    db.AddInParameter(dbCommand, "@AirBookingID", DbType.Guid, oceanBookingID);
                    db.AddInParameter(dbCommand, "@AirMAWBID", DbType.Guid, mblId);
                    db.AddInParameter(dbCommand, "@MAWBNo", DbType.String, mblNo);
                    db.AddInParameter(dbCommand, "@No", DbType.String, hblNo);
                    db.AddInParameter(dbCommand, "@NumberOfOriginal", DbType.Int32, numberOfOriginal);
                    db.AddInParameter(dbCommand, "@CheckerID", DbType.Guid, checkerID);
                    db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, shipperID);
                    db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                    db.AddInParameter(dbCommand, "@ShipperAccountNo", DbType.String, shipperAccountNo);
                    db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, consigneeID);
                    db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempconsigneeDescription);
                    db.AddInParameter(dbCommand, "@ConsigneeAccountNo", DbType.String, consigneeAccountNo);
                    db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, notifyPartyID);
                    db.AddInParameter(dbCommand, "@NotifyPartyDescription", DbType.Xml, tempnotifyPartyDescription);
                    db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, agentID);
                    db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, tempagentDescription);
                    db.AddInParameter(dbCommand, "@AgentAccountNo", DbType.String, agentAccountNo);
                    db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, agentOfCarrierID);
                    db.AddInParameter(dbCommand, "@IATACode", DbType.String, iATACode);
                    db.AddInParameter(dbCommand, "@DepartureID", DbType.Guid, departureID);
                    db.AddInParameter(dbCommand, "@DepartureName", DbType.String, departureName);
                    db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, etd);
                    db.AddInParameter(dbCommand, "@DestinationID", DbType.Guid, destinationID);
                    db.AddInParameter(dbCommand, "@DestinationName", DbType.String, destinationName);
                    db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, eta);
                    db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, placeOfDeliveryID);
                    db.AddInParameter(dbCommand, "@TranshipmentPort1", DbType.String, transhipmentPort1);
                    db.AddInParameter(dbCommand, "@TranshipmentPort1By", DbType.String, transhipmentPort1By);
                    db.AddInParameter(dbCommand, "@TranshipmentPort2", DbType.String, transhipmentPort2);
                    db.AddInParameter(dbCommand, "@TranshipmentPort2By", DbType.String, transhipmentPort2By);
                    db.AddInParameter(dbCommand, "@TranshipmentPort3", DbType.String, transhipmentPort3);
                    db.AddInParameter(dbCommand, "@TranshipmentPort3By", DbType.String, transhipmentPort3By);

                    db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, paymentTermID);
                    db.AddInParameter(dbCommand, "@OtherPaymentTermID", DbType.Guid, otherPaymentTermID);
                    db.AddInParameter(dbCommand, "@DeclaredValueForCarriage", DbType.String, declaredValueForCarriage);
                    db.AddInParameter(dbCommand, "@DeclaredValueForCustoms", DbType.String, declaredValueForCustoms);
                    db.AddInParameter(dbCommand, "@InsuranceAmount", DbType.String, insuranceAmount);
                    db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, quantity);
                    db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, quantityUnitID);
                    db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, weight);
                    db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, measurement);
                    db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, measurementUnitID);
                    db.AddInParameter(dbCommand, "@RateClass", DbType.Int16, (short?)rateClass);
                    db.AddInParameter(dbCommand, "@ChargeableWeight", DbType.Decimal, chargeableWeight);
                    db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, currencyID);
                    db.AddInParameter(dbCommand, "@RateCharge", DbType.Decimal, rateCharge);
                    db.AddInParameter(dbCommand, "@RateAmount", DbType.Decimal, rateAmount);

                    db.AddInParameter(dbCommand, "@HandlingInformation", DbType.String, handlingInformation);
                    db.AddInParameter(dbCommand, "@AccountInformation", DbType.String, freightDescription);
                    db.AddInParameter(dbCommand, "@OtherChargers", DbType.String, otherChargers);
                    db.AddInParameter(dbCommand, "@OtherChargeDescription", DbType.String, otherChargeDescription);
                    db.AddInParameter(dbCommand, "@ValuationCharge", DbType.Decimal, valuationCharge);
                    db.AddInParameter(dbCommand, "@TAX", DbType.Decimal, tax);
                    db.AddInParameter(dbCommand, "@AgentCharger", DbType.Decimal, agentCharger);
                    db.AddInParameter(dbCommand, "@CarrierCharger", DbType.Decimal, carrierCharger);
                    db.AddInParameter(dbCommand, "@CurrencyConversionRate", DbType.String, currencyConversionRate);
                    db.AddInParameter(dbCommand, "@DestinationCurrencyAmount", DbType.String, destinationCurrencyAmount);
                    db.AddInParameter(dbCommand, "@ChargesAtDestination", DbType.String, chargesAtDestination);
                    db.AddInParameter(dbCommand, "@ChargeableWeightUnitIsKGS", DbType.Boolean, chargeableWeightUnitIsKGS);
                    db.AddInParameter(dbCommand, "@AirLineID", DbType.Guid, airCompanyID);
                    db.AddInParameter(dbCommand, "@FlightID", DbType.Guid, flightID);

                    db.AddInParameter(dbCommand, "@ReleaseType", DbType.Int16, releaseType);
                    db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, releaseDate);
                    db.AddInParameter(dbCommand, "@BLDate", DbType.DateTime, bLDate);
                    db.AddInParameter(dbCommand, "@Marks", DbType.String, marks);
                    db.AddInParameter(dbCommand, "@GoodsDescription", DbType.String, goodsDescription);
                    db.AddInParameter(dbCommand, "@IssuePlaceID", DbType.Guid, issuePlaceID);
                    db.AddInParameter(dbCommand, "@IssueBy", DbType.Guid, issueByID);
                    db.AddInParameter(dbCommand, "@IssueDate", DbType.DateTime, issueDate);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                    db.AddInParameter(dbCommand, "@MWBUpdateDate", DbType.DateTime, mblUpdateDate);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                    #endregion

                    result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "AirMBLID", "MWBUpdateDate" });
#if DEBUG
                    _FCMCommonService.SaveShipmentItemForCSP(
                        new SaveRequestShipmentItemForCSP()
                        {
                            OperationID = oceanBookingID,
                            OperationType = OperationType.AirExport,
                            ID = result.GetValue<Guid>("ID"),
                            SaveBy = saveByID,
                            UpdateDate = DateTime.Now
                        });
#endif

                    scope.Complete();
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
        /// 删除HBL提单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveAirHBLInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAirHBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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
        /// 改变HBL状态
        /// </summary>
        /// <param name="id">HBL ID</param>
        /// <param name="state">状态（0草稿、1对单中、2、对单确认、3已完成）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回true</returns>
        public SingleResult ChangeAirHBLState(
            Guid id,
            AEBLState state,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeAirHBLState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "UpdateDate", DbType.DateTime, updateDate);
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

        #region Other

        /// <summary>
        /// 获取提单的货物信息列表
        /// </summary>
        /// <param name="blIDs">提单ID</param>
        /// <returns>计量信息列表</returns>
        public List<AirContainerCargoList> GetAirBLMeasureInfoList(Guid[] blIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirBLMeasureInfoListByBLIDs");

                db.AddInParameter(dbCommand, "@BLIDs", DbType.String, blIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirContainerCargoList> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new AirContainerCargoList
                                                       {
                                                           ID = b.Field<Guid>("ID"),
                                                           Measurement = b.Field<decimal>("Measurement"),
                                                           Quantity = b.Field<int>("Quantity"),
                                                           Weight = b.Field<decimal>("Weight"),
                                                           MeasurementUnitID = b.Field<Guid>("MeasurementUnitID"),
                                                           QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                                                           WeightUnitID = b.Field<Guid>("WeightUnitID"),
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

        #endregion

        #region 分合单
        /// <summary>
        /// 合单
        /// </summary>
        /// <param name="id">保留的提单</param>
        /// <param name="blID">要合并的提单</param>
        /// <param name="saveByID">saveByID</param>
        /// <returns>返回保留的提单的ID,更新时间</returns>
        public SingleResult MergeAirBL(Guid id, Guid[] blID, Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspMergeAirBL");

                string tempBLIds = blID.Join();
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@BLIDs", DbType.String, tempBLIds);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
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
        /// 分单
        /// </summary>
        /// <param name="xml">特定的XML结构</param>
        /// <param name="saveById">保存人</param>
        /// <returns>ManyResult { "ID" }</returns>
        public ManyResult SplitAirBL(string xml, Guid saveById)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSplitAirBL");

                db.AddInParameter(dbCommand, "@XML", DbType.String, xml);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "AirMBLID" });
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

        #region 转换保存

        /// <summary>
        /// 保存MBL信息
        /// </summary>
        public SingleResult SaveAirMBLInfo(SaveMBLInfoParameter parameter)
        {
            return SaveAirMBLInfo(parameter.id,
                                    parameter.airBookingID,
                                    parameter.mblNo,
                                    parameter.numberOfOriginal,
                                    parameter.checkerID,
                                    parameter.shipperID,
                                    parameter.shipperDescription,
                                    parameter.ShipperAccountNo,
                                    parameter.consigneeID,
                                    parameter.consigneeDescription,
                                    parameter.ConsigneeAccountNo,
                                    parameter.notifyPartyID,
                                    parameter.notifyPartyDescription,
                                    parameter.agentID,
                                    parameter.agentDescription,
                                    parameter.AgentAccountNo,
                                    parameter.AgentIATACode,
                                    parameter.AgentOfCarrierID,
                                    parameter.DepartureID,
                                    parameter.DepartureName,
                                    parameter.ETD,
                                    parameter.DetinationID,
                                    parameter.DetinationName,
                                    parameter.ETA,
                                    parameter.placeOfDeliveryID,
                                    parameter.TranshipmentPort1,
                                    parameter.TranshipmentPort1By,
                                    parameter.TranshipmentPort2,
                                    parameter.TranshipmentPort2By,
                                    parameter.TranshipmentPort3,
                                    parameter.TranshipmentPort3By,
                                    parameter.paymentTermID,
                                    parameter.OtherPaymentTermID,
                                    parameter.DeclaredValueForCarriage,
                                    parameter.DeclaredValueForCustoms,
                                    parameter.InsuranceAmount,
                                    parameter.quantity,
                                    parameter.quantityUnitID,
                                    parameter.weight,
                                    parameter.measurement,
                                    parameter.measurementUnitID,
                                    parameter.RateClass,
                                    parameter.ChargeableWeight,
                                    parameter.CurrencyID,
                                    parameter.RateCharge,
                                    parameter.RateAmount,
                                    parameter.IATARateCharge,
                                    parameter.IATACurrencyID,
                                    parameter.IATARateAmount,
                                    parameter.AirCompanyID,
                                    parameter.FilightNoID,
                                    parameter.freightDescription,
                                    parameter.HandingInformation,
                                    parameter.releaseType,
                                    parameter.releaseDate,
                                    parameter.BLDate,
                                    parameter.marks,
                                    parameter.goodsDescription,
                                    parameter.OtherChargeList,
                                    parameter.OtherChargeDescription,
                                    parameter.ValuationCharge,
                                    parameter.Tax,
                                    parameter.AgentCharger,
                                    parameter.CarrierCharger,
                                    parameter.CurrencyConversionRate,
                                    parameter.DestinationCurrencyAmount,
                                    parameter.ChargesAtDestination,
                                    parameter.issuePlaceID,
                                    parameter.issueByID,
                                    parameter.issueDate,
                                    parameter.ChargeableWeightUnitIsKGS,
                                    parameter.IATAChargeableWeightUnitIsKGS,
                                    parameter.saveByID,
                                    parameter.updateDate);
        }

        /// <summary>
        /// 保存HBL信息
        /// </summary>
        public SingleResult SaveAirHBLInfo(SaveHBLInfoParameter parameter)
        {
            return SaveAirHBLInfo(parameter.id,
                                    parameter.airBookingID,
                                    parameter.mblId,
                                    parameter.mblNo,
                                    parameter.hblNo,
                                    parameter.numberOfOriginal,
                                    parameter.checkerID,
                                    parameter.shipperID,
                                    parameter.shipperDescription,
                                    parameter.ShipperAccountNo,
                                    parameter.consigneeID,
                                    parameter.consigneeDescription,
                                    parameter.ConsigneeAccountNo,
                                    parameter.notifyPartyID,
                                    parameter.notifyPartyDescription,
                                    parameter.agentID,
                                    parameter.agentDescription,
                                    parameter.AgentAccountNo,
                                    parameter.AgentIATACode,
                                    parameter.AgentOfCarrierID,
                                    parameter.DepartureID,
                                    parameter.DepartureName,
                                    parameter.ETD,
                                    parameter.DetinationID,
                                    parameter.DetinationName,
                                    parameter.ETA,
                                    parameter.placeOfDeliveryID,
                                    parameter.TranshipmentPort1,
                                    parameter.TranshipmentPort1By,
                                    parameter.TranshipmentPort2,
                                    parameter.TranshipmentPort2By,
                                    parameter.TranshipmentPort3,
                                    parameter.TranshipmentPort3By,
                                    parameter.paymentTermID,
                                    parameter.OtherPaymentTermID,
                                    parameter.DeclaredValueForCarriage,
                                    parameter.DeclaredValueForCustoms,
                                    parameter.InsuranceAmount,
                                    parameter.quantity,
                                    parameter.quantityUnitID,
                                    parameter.weight,
                                    parameter.measurement,
                                    parameter.measurementUnitID,
                                    parameter.RateClass,
                                    parameter.ChargeableWeight,
                                    parameter.CurrencyID,
                                    parameter.RateCharge,
                                    parameter.RateAmount,
                                    parameter.AirCompanyID,
                                    parameter.FilightNoID,
                                    parameter.freightDescription,
                                    parameter.HandingInformation,
                                    parameter.releaseType,
                                    parameter.releaseDate,
                                    parameter.BLDate,
                                    parameter.marks,
                                    parameter.goodsDescription,
                                    parameter.OtherChargeList,
                                    parameter.OtherChargeDescription,
                                    parameter.ValuationCharge,
                                    parameter.Tax,
                                    parameter.AgentCharger,
                                    parameter.CarrierCharger,
                                    parameter.CurrencyConversionRate,
                                    parameter.DestinationCurrencyAmount,
                                    parameter.ChargesAtDestination,
                                    parameter.issuePlaceID,
                                    parameter.issueByID,
                                    parameter.issueDate,
                                    parameter.ChargeableWeightUnitIsKGS,
                                    parameter.saveByID,
                                    parameter.updateDate,
                                    parameter.mblUpdateDate);
        }

        #endregion
    }
}

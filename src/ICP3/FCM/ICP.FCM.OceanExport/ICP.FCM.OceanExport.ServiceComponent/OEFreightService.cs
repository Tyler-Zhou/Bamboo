using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace ICP.FCM.OceanExport.ServiceComponent
{
    partial class OceanExportService
    {
        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <param name="ContractNo">合约号</param>
        /// <param name="CarrierID">船东</param>
        /// <param name="POLID">POL</param>
        /// <param name="PODID">POD</param>
        /// <param name="PlaceOfReceiptID">目的地</param>
        /// <param name="FinalDestinationID">目的地</param>
        /// <param name="Comm">箱型</param>
        /// <param name="FromDate">fromdate</param>
        /// <param name="ToDate">todate</param>
        /// <param name="freightId">已有的合约明细ID</param>
        /// <returns></returns>
        public FreightDataList GetFreight(string ContractNo,
          Guid? CarrierID,
          Guid? PlaceOfReceiptID,
          Guid? POLID,
          Guid? PODID,
          Guid? FinalDestinationID,
          string Comm,
          DateTime? FromDate,
          DateTime? ToDate,
          Guid? freightId, SelectType type)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanContractInfo");
                db.AddInParameter(dbCommand, "@ContractNo", DbType.String, ContractNo);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, CarrierID);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, PlaceOfReceiptID);
                db.AddInParameter(dbCommand, "@POLID", DbType.Guid, POLID);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, PODID);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, FinalDestinationID);
                db.AddInParameter(dbCommand, "@Comm", DbType.String, Comm);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@FreightId", DbType.Guid, freightId);
                db.AddInParameter(dbCommand, "@IsRow2Col", DbType.Boolean, true);

                if (type == SelectType.Contract)
                {
                    db.AddInParameter(dbCommand, "@IsOceans", DbType.Boolean, true);
                    db.AddInParameter(dbCommand, "@IsInquirePrices", DbType.Boolean, false);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@IsOceans", DbType.Boolean, false);
                    db.AddInParameter(dbCommand, "@IsInquirePrices", DbType.Boolean, true);
                }

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    FreightDataList nulldataList = new FreightDataList();
                    nulldataList.DataList = new List<FreightList>();
                    nulldataList.UnitList = new List<string>();

                    return nulldataList;
                }


                List<string> UnitList = (from d in ds.Tables[1].AsEnumerable() select d.Field<string>("UnitName")).ToList();

                List<FreightList> freightList = (from d in ds.Tables[0].AsEnumerable()
                                                 select new FreightList
                                                 {
                                                     ID = d.Field<Guid>("ID"),
                                                     OceanId = d.Field<Guid>("OceanId"),
                                                     Carrier = d.Field<String>("CarrierName"),
                                                     FreightNo = d.Field<String>("ContractNo"),
                                                     ContractName = d.Field<String>("ContractName"),
                                                     ItemCode = d.Field<String>("ItemCode"),
                                                     Pol = d.Field<String>("POLName"),
                                                     Pod = d.Field<String>("PODName"),
                                                     DeliveryName = d.Field<String>("DeliveryName"),
                                                     Destination = d.Field<String>("FinalDestinationName"),
                                                     BeginDate = d.Field<DateTime?>("EffectiveFromDate"),
                                                     EndDate = d.Field<DateTime?>("EffectiveToDate"),
                                                     Shipper = d.Field<String>("ShipperName"),
                                                     Consignee = d.Field<String>("ConsigneeName"),
                                                     Goods = d.Field<String>("Commodity"),
                                                     Account = d.Field<String>("Account"),
                                                     AdditionalCharges = d.Field<String>("SurCharge"),
                                                     NotifyPart = d.Field<String>("NotifypartyName"),
                                                     PaymentTreaty = d.Field<String>("PaymentTermName"),
                                                     Remark = d.Field<String>("Remark"),
                                                     RemarkDetails = d.Field<String>("OceanRemark"),
                                                     Term = d.Field<String>("Term"),
                                                     TransitTime = d.Field<string>("TransitTime"),
                                                     Rate_20FR = UnitList.Contains("20FR") ? d.Field<Decimal?>("20FR") : 0,
                                                     Rate_20GP = UnitList.Contains("20GP") ? d.Field<Decimal?>("20GP") : 0,
                                                     Rate_20HQ = UnitList.Contains("20HQ") ? d.Field<Decimal?>("20HQ") : 0,
                                                     Rate_20HT = UnitList.Contains("20HT") ? d.Field<Decimal?>("20HT") : 0,
                                                     Rate_20NOR = UnitList.Contains("20NOR") ? d.Field<Decimal?>("20NOR") : 0,
                                                     Rate_20OT = UnitList.Contains("20OT") ? d.Field<Decimal?>("20OT") : 0,
                                                     Rate_20RF = UnitList.Contains("20RF") ? d.Field<Decimal?>("20RF") : 0,
                                                     Rate_20RH = UnitList.Contains("20RH") ? d.Field<Decimal?>("20RH") : 0,
                                                     Rate_20TK = UnitList.Contains("20TK") ? d.Field<Decimal?>("20TK") : 0,
                                                     Rate_40FR = UnitList.Contains("40FR") ? d.Field<Decimal?>("40FR") : 0,
                                                     Rate_40GP = UnitList.Contains("40GP") ? d.Field<Decimal?>("40GP") : 0,
                                                     Rate_40HQ = UnitList.Contains("40HQ") ? d.Field<Decimal?>("40HQ") : 0,
                                                     Rate_40HT = UnitList.Contains("40HT") ? d.Field<Decimal?>("40HT") : 0,
                                                     Rate_40NOR = UnitList.Contains("40NOR") ? d.Field<Decimal?>("40NOR") : 0,
                                                     Rate_40OT = UnitList.Contains("40OT") ? d.Field<Decimal?>("40OT") : 0,
                                                     Rate_40RF = UnitList.Contains("40RF") ? d.Field<Decimal?>("40RF") : 0,
                                                     Rate_40RH = UnitList.Contains("40RH") ? d.Field<Decimal?>("40RH") : 0,
                                                     Rate_40TK = UnitList.Contains("40TK") ? d.Field<Decimal?>("40TK") : 0,
                                                     Rate_45FR = UnitList.Contains("45FR") ? d.Field<Decimal?>("45FR") : 0,
                                                     Rate_45GP = UnitList.Contains("45GP") ? d.Field<Decimal?>("45GP") : 0,
                                                     Rate_45HQ = UnitList.Contains("45HQ") ? d.Field<Decimal?>("45HQ") : 0,
                                                     Rate_45HT = UnitList.Contains("45HT") ? d.Field<Decimal?>("45HT") : 0,
                                                     Rate_45OT = UnitList.Contains("45OT") ? d.Field<Decimal?>("45OT") : 0,
                                                     Rate_45RF = UnitList.Contains("45RF") ? d.Field<Decimal?>("45RF") : 0,
                                                     Rate_45RH = UnitList.Contains("45RH") ? d.Field<Decimal?>("45RH") : 0,
                                                     Rate_45TK = UnitList.Contains("45TK") ? d.Field<Decimal?>("45TK") : 0,
                                                     Rate_53HQ = UnitList.Contains("53HQ") ? d.Field<Decimal?>("53HQ") : 0,

                                                 }).ToList();

                FreightDataList dataList = new FreightDataList();
                dataList.DataList = freightList;
                dataList.UnitList = UnitList;

                return dataList;
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
        /// 获取合约列表
        /// </summary>
        /// <param name="ContractNo">合约号</param>
        /// <param name="CarrierID">船东</param>
        /// <param name="POLID">POL</param>
        /// <param name="PODID">POD</param>
        /// <param name="PlaceOfReceiptID">目的地</param>
        /// <param name="FinalDestinationID">最终目的地</param>
        /// <param name="Comm">箱型</param>
        /// <param name="FromDate">fromdate</param>
        /// <param name="ToDate">todate</param>
        /// <returns></returns>
        public FreightDataList GetFright(string ContractNo,
            Guid? CarrierID,
            Guid? PlaceOfReceiptID,
            Guid? POLID,
            Guid? PODID,
            Guid? FinalDestinationID,
            string Comm,
            DateTime? FromDate,
            DateTime? ToDate
            )
        {
            return GetFreight(ContractNo, CarrierID, PlaceOfReceiptID, POLID, PODID, FinalDestinationID, Comm, FromDate, ToDate, null, SelectType.Contract);

        }


        /// <summary>
        /// 获得合约
        /// </summary>
        /// <param name="FreightRateID"></param>
        /// <returns></returns>
        public FreightDataList GetFrightByID(Guid FreightRateID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanContractInfo");
                db.AddInParameter(dbCommand, "@FreightRateID", DbType.Guid, FreightRateID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    FreightDataList nulldataList = new FreightDataList
                    {
                        DataList = new List<FreightList>(),
                        UnitList = new List<string>()
                    };

                    return nulldataList;
                }

                List<FreightList> freightList = (from d in ds.Tables[0].AsEnumerable()
                                                 select new FreightList
                                                 {
                                                     ID = d.Field<Guid>("ID"),
                                                     OceanId = d.Field<Guid>("OceanId"),
                                                     Carrier = d.Field<String>("CarrierName"),
                                                     FreightNo = d.Field<String>("ContractNo"),
                                                     ContractName = d.Field<String>("ContractName"),
                                                     ItemCode = d.Field<String>("ItemCode"),
                                                     Pol = d.Field<String>("POLName"),
                                                     Pod = d.Field<String>("PODName"),
                                                     DeliveryName = d.Field<String>("DeliveryName"),
                                                     Destination = d.Field<String>("FinalDestinationName"),
                                                     BeginDate = d.Field<DateTime?>("EffectiveFromDate"),
                                                     EndDate = d.Field<DateTime?>("EffectiveToDate"),
                                                     Shipper = d.Field<String>("ShipperName"),
                                                     Consignee = d.Field<String>("ConsigneeName"),
                                                     Goods = d.Field<String>("Commodity"),
                                                     Account = d.Field<String>("Account"),
                                                     AdditionalCharges = d.Field<String>("SurCharge"),
                                                     NotifyPart = d.Field<String>("NotifypartyName"),
                                                     PaymentTreaty = d.Field<String>("PaymentTermName"),
                                                     Remark = d.Field<String>("Remark"),
                                                     RemarkDetails = d.Field<String>("OceanRemark"),
                                                     Term = d.Field<String>("Term"),
                                                     TransitTime = d.Field<String>("TransitTime"),
                                                 }).ToList();

                FreightDataList dataList = new FreightDataList {DataList = freightList};

                return dataList;
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

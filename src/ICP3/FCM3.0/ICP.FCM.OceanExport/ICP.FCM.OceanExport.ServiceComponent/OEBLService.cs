using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface.DataObjects;
using System.Transactions;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    partial class OceanExportService
    {

        #region get List
        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="blIDs">blIDs</param>
        /// <returns>返回提单列表</returns>
        public List<OceanBLList> GetOceanBLListByIds(
            Guid[] blIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(blIDs, "blIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBLListByIDs");

                string tempBlIDs = blIDs.Join();

                db.AddInParameter(dbCommand, "@BLIDs", DbType.String, tempBlIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLList> results = BulidBLListByDataSet(ds);

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
        /// <param name="consigneeName">收货人</param>
        /// <param name="salesID">业务员</param>
        /// <param name="filerID">文件</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回提单列表</returns>
        public List<OceanBLList> GetOceanBLList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string containerNo,
            string shippingOrderNo,
            string scno,
            string customerName,
            string carrierName,
            string agentOfCarrierName,
            string vesselName,
            string voyageNo,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string consigneeName,
            Guid? salesID,
            Guid? filerID,
            Guid? overseasFilerID,
            OEBLState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBLList");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@RefNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ShippingOrderNo", DbType.String, shippingOrderNo);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerNo);
                db.AddInParameter(dbCommand, "@Scno", DbType.String, scno);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@CarrierName", DbType.String, carrierName);
                db.AddInParameter(dbCommand, "@AgentOfCarrierName", DbType.String, agentOfCarrierName);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
                db.AddInParameter(dbCommand, "@ConsigneeName", DbType.String, consigneeName);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@OverseasFilerID", DbType.Guid, overseasFilerID);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, filerID);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLList> results = BulidBLListByDataSet(ds);

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
        public List<OceanBLList> GetOceanBLListForFaster(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBLListForFaster");

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

                List<OceanBLList> results = BulidBLListByDataSet(ds);

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
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="ownerID">是否自己的单</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        public List<OceanBLList> GetOceanMBLList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string shippingOrderNo,
            string scno,
            string customerName,
            string carrierName,
            string agentOfCarrierName,
            string vesselName,
            string voyageNo,
            string polName,
            string podName,
            string placeOfDeliveryName,
            Guid? salesID,
            OEBLState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            Guid? ownerID,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanMBLList");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@RefNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ShippingOrderNo", DbType.String, shippingOrderNo);
                db.AddInParameter(dbCommand, "@Scno", DbType.String, scno);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@CarrierName", DbType.String, carrierName);
                db.AddInParameter(dbCommand, "@AgentOfCarrierName", DbType.String, agentOfCarrierName);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@OwnerID", DbType.Guid, ownerID);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLList> results = BulidBLListByDataSet(ds);

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

        private static List<OceanBLList> BulidBLListByDataSet(DataSet ds)
        {
            List<OceanBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                         select new OceanBLList
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             MBLID = b.IsNull("MBLID") ? Guid.Empty : b.Field<Guid>("MBLID"),
                                             OceanBookingID = b.Field<Guid>("OceanBookingID"),
                                             CompanyID = b.Field<Guid>("CompanyID"),
                                             RefNo = b.Field<string>("RefNo"),
                                             No = b.Field<string>("BLNo"),
                                             ReleaseType = b.IsNull("ReleaseType") ? FCMReleaseType.Unknown : (FCMReleaseType)b.Field<byte>("ReleaseType"),
                                             CheckerName = b.Field<string>("CheckerName"),
                                             ShipperName = b.Field<string>("ShipperName"),
                                             ConsigneeName = b.Field<string>("ConsigneeName"),
                                             AgentName = b.Field<string>("AgentName"),
                                             AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                             CarrierID = b.Field<Guid?>("CarrierID"),
                                             CarrierName = b.Field<string>("CarrierName"),
                                             VesselVoyage = b.Field<string>("VesselVoyage"),
                                             POLName = b.Field<string>("POLName"),
                                             PODName = b.Field<string>("PODName"),
                                             PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                             FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                             ETD = b.Field<DateTime?>("ETD"),
                                             ETA = b.Field<DateTime?>("ETA"),
                                             IssueType = b.IsNull("IssueType") ? IssueType.Unknown : (IssueType)b.Field<byte>("IssueType"),
                                             State = (OEBLState)b.Field<byte>("State"),
                                             OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                             BookingerName = b.Field<string>("BookingerName"),
                                             CreateDate = b.Field<DateTime>("CreateDate"),
                                             UpdateByName = b.Field<string>("UpdateByName"),
                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                             ShipperID = b.Field<Guid?>("ShipperID"),
                                             ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                             ContainerNos = b.Field<string>("ContainerNos"),
                                             CustomerName = b.Field<string>("CustomerName"),
                                             ExistFees = b.Field<bool>("ExistFees"),
                                             FilerName = b.Field<string>("FilerName"),
                                             OverseasFilerName = b.Field<string>("OverseasFilerName"),
                                             NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                             SalesName = b.Field<string>("SalesName"),
                                             SONO = b.Field<string>("SONO"),
                                             NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                             HBLCount = b.Field<int>("HBLCount"),
                                             IsValid = b.Field<bool>("IsValid"),
                                             ShippingOrderID = b.Field<Guid?>("ShippingOrderID"),
                                             ShippingOrderUpdateDate = b.Field<DateTime?>("ShippingOrderUpdateDate"),
                                             IsDirty = false
                                         }).ToList();
            return results;
        }

        #endregion

        #region MBL

        #region 验证MBLNo是否存在
        /// <summary>
        /// 验证MBLNo是否存在
        /// </summary>
        /// <param name="mblID"></param>
        /// <param name="mblNo"></param>
        /// <returns></returns>
        public bool IsExistsMBLNo(Guid? mblID, string mblNo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspverifyOceanMBLNO");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@MBLNo", DbType.String, mblNo);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return false;
                }

                return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);

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

        /// <summary>
        /// 获取MBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回MBL提单信息</returns>
        public OceanMBLInfo GetOceanMBLInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanMBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanMBLInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new OceanMBLInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           MBLID = b.Column<Guid>("ID"),
                                           CompanyID = b.Column<Guid>("CompanyID"),
                                           OceanBookingID = b.Field<Guid>("OceanBookingID"),
                                           No = b.Field<string>("MBLNo"),
                                           RefNo = b.Field<string>("RefNo"),
                                           ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                           ShipperName = b.Field<string>("ShipperName"),
                                           ConsigneeName = b.Field<string>("ConsigneeName"),
                                           NumberOfOriginal = (short?)b.Field<byte>("NumberOfOriginal"),
                                           VoyageShowType = (VoyageShowType)b.Field<byte>("VoyageShowType"),
                                           CheckerID = b.Field<Guid?>("CheckerID"),
                                           CheckerName = b.Field<string>("CheckerName"),
                                           ShipperID = b.Column<Guid?>("ShipperID"),
                                           ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                           ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                           ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                           NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                           NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                           NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                           AgentID = b.Field<Guid?>("AgentID"),
                                           AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                           AgentName = b.Field<string>("AgentName"),
                                           ETD = b.Field<DateTime?>("ETD"),
                                           PreETD = b.Field<DateTime?>("PreETD"),
                                           PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                           PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                           PlaceOfReceiptCode = b.Field<string>("PlaceOfReceiptCode"),
                                           PreVoyageID = b.Field<Guid?>("PreVoyageID"),
                                           PreVesselVoyage = b.Field<string>("PreVoyageName"),
                                           PODCode = b.Field<string>("PODCode"),
                                           POLCode = b.Field<string>("POLCode"),
                                           PODID = b.Column<Guid>("PODID"),
                                           POLID = b.Column<Guid>("POLID"),
                                           SONO = b.Field<string>("SONO"),
                                           AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                           OverseasFilerName = b.Field<string>("OverseasFilerName"),
                                           VoyageID = b.Field<Guid?>("VoyageID"),
                                           VesselVoyage = b.Field<string>("VoyageName"),
                                           POLName = b.Field<string>("POLName"),
                                           PODName = b.Field<string>("PODName"),
                                           ETA = b.Field<DateTime?>("ETA"),
                                           PlaceOfDeliveryID = b.Field<Guid?>("PlaceOfDeliveryID"),
                                           PlaceOfDeliveryCode = b.Field<string>("PlaceOfDeliveryCode"),
                                           PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                           FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                           FinalDestinationCode = b.Field<string>("FinalDestinationCode"),
                                           FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                           TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                           TransportClauseName = b.Field<string>("TransportClauseName"),
                                           PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                           PaymentTermName = b.Field<string>("PaymentTermName"),
                                           FreightDescription = b.Field<string>("FreightDescription"),
                                           ReleaseType = (FCMReleaseType)b.Field<byte>("ReleaseType"),
                                           ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                           Quantity = b.Field<int>("Quantity"),
                                           QuantityUnitID = b.Column<Guid>("QuantityUnitID"),
                                           QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                           Weight = b.Field<decimal>("Weight"),
                                           WeightUnitID = b.Column<Guid>("WeightUnitID"),
                                           WeightUnitName = b.Field<string>("WeightUnitName"),
                                           Measurement = b.Field<decimal>("Measurement"),
                                           MeasurementUnitID = b.Column<Guid>("MeasurementUnitID"),
                                           MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                           Marks = b.Field<string>("Marks").Replace("&#;", "\r\n"),
                                           GoodsDescription = b.Field<string>("GoodsDescription").Replace("&#;", "\r\n"),
                                           IsWoodPacking = b.Field<bool>("IsWoodPacking"),
                                           ContainerDescription = b.Field<string>("ContainerDescription") == null ? string.Empty : b.Field<string>("ContainerDescription").Replace(char.ConvertFromUtf32(10), "\r\n"),
                                           IssuePlaceID = b.Column<Guid>("IssuePlaceID"),
                                           IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                           IssueByID = b.Column<Guid>("IssueByID"),
                                           IssueByName = b.Field<string>("IssueByName"),
                                           IssueDate = b.Field<DateTime?>("IssueDate"),
                                           State = (OEBLState)b.Field<byte>("State"),
                                           ConfirmOnBoardType = (ConfirmOnBoardType)b.Field<byte>("ConfirmOnBoardType"),
                                           IssueType = b.IsNull("IssueType") ? IssueType.Unknown : (IssueType)b.Field<byte>("IssueType"),
                                           WoodPacking = b.Field<string>("WoodPackingDescription"),
                                           CarrierID = b.Field<Guid?>("CarrierID"),
                                           CarrierName = b.Field<string>("CarrierName"),
                                           ContainerNos = b.Field<string>("ContainerNos"),
                                           CustomerName = b.Field<string>("CustomerName"),
                                           FilerName = b.Field<string>("FilerName"),
                                           BookingerName = b.Field<string>("BookingerName"),
                                           SalesName = b.Field<string>("SalesName"),
                                           ExistFees = b.Field<bool>("ExistFees"),
                                           CreateByID = b.Column<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                           CtnQtyInfo = b.Field<string>("CtnQtyInfo"),
                                           HBLNos = b.Field<string>("HBLNos"),
                                           ShippingOrderID = b.Column<Guid?>("ShippingOrderID"),
                                           ShippingOrderUpdateDate = b.Field<DateTime?>("ShippingOrderUpdateDate"),
                                           IsRequestAgent = b.Field<bool>("IsRequestAgent"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           BookingPaymentTermID = b.Field<Guid?>("BookingPaymentTermID"),
                                           ContainerQtyDescription = b.Field<string>("ContainerQtyDescription"),
                                           AgentText = b.Field<string>("AgentText"),
                                           ContractNo = b.Field<string>("ContractNo"),
                                           ContractID = b.Field<Guid?>("ContractID"),
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
        /// <param name="FreightRateID">合约ID</param>
        /// <returns>返回SingleResult</returns>
        SingleResult SaveOceanMBLInfo(
            Guid? id,
            Guid oceanBookingID,
            string mblNo,
            short? numberOfOriginal,
            VoyageShowType voyageShowType,
            Guid? checkerID,
            Guid? shipperID,
            CustomerDescription shipperDescription,
            Guid? consigneeID,
            CustomerDescription consigneeDescription,
            Guid? notifyPartyID,
            CustomerDescription notifyPartyDescription,
            Guid? agentID,
            CustomerDescription agentDescription,
            Guid? placeOfReceiptID,
            string placeOfReceiptName,
            Guid? preVoyageID,
            Guid? voyageID,
            Guid polID,
            string polName,
            Guid podID,
            string podName,
            Guid? placeOfDeliveryID,
            string placeOfDeliveryName,
            Guid? finalDestinationID,
            string finalDestinationName,
            Guid transportClauseID,
            Guid? paymentTermID,
            string freightDescription,
            FCMReleaseType releaseType,
            DateTime? releaseDate,
            int quantity,
            Guid quantityUnitID,
            decimal weight,
            Guid weightUnitID,
            decimal measurement,
            Guid measurementUnitID,
            string marks,
            string goodsDescription,
            bool isWoodPacking,
            string ctnQtyInfo,
            Guid issuePlaceID,
            Guid issueByID,
            DateTime? issueDate,
            string woodPacking,
            IssueType issueType,
            Guid saveByID,
            string agentText,
            Guid? FreightRateID,
            DateTime? updateDate,
            DateTime? eta,
            DateTime? etd,
            DateTime? preETD
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanShippingOrderID");
            ArgumentHelper.AssertGuidNotEmpty(transportClauseID, "transportClauseID");
            ArgumentHelper.AssertGuidNotEmpty(quantityUnitID, "quantityUnitID");
            ArgumentHelper.AssertGuidNotEmpty(measurementUnitID, "measurementUnitID");
            ArgumentHelper.AssertGuidNotEmpty(weightUnitID, "weightUnitID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanMBLInfo");

                string tempagentDescription = SerializerHelper.SerializeToString<CustomerDescription>(agentDescription, true, false);
                string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(shipperDescription, true, false);
                string tempconsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(consigneeDescription, true, false);
                string tempnotifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(notifyPartyDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@MblNo", DbType.String, mblNo);
                db.AddInParameter(dbCommand, "@NumberOfOriginal", DbType.Int32, numberOfOriginal);
                db.AddInParameter(dbCommand, "@VoyageShowType", DbType.Int16, voyageShowType);
                db.AddInParameter(dbCommand, "@CheckerID", DbType.Guid, checkerID);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, shipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, consigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempconsigneeDescription);
                db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, notifyPartyID);
                db.AddInParameter(dbCommand, "@NotifyPartyDescription", DbType.Xml, tempnotifyPartyDescription);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, agentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, tempagentDescription);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, placeOfReceiptID);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptName", DbType.String, placeOfReceiptName);
                db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, preVoyageID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, voyageID);
                db.AddInParameter(dbCommand, "@POLID", DbType.Guid, polID);
                db.AddInParameter(dbCommand, "@POLName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, podID);
                db.AddInParameter(dbCommand, "@PODName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, placeOfDeliveryID);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, finalDestinationID);
                db.AddInParameter(dbCommand, "@FinalDestinationName", DbType.String, finalDestinationName);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, transportClauseID);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, paymentTermID);
                db.AddInParameter(dbCommand, "@FreightDescription", DbType.String, freightDescription);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Int16, releaseType);
                db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, releaseDate);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, quantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, weightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, measurementUnitID);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, marks);
                db.AddInParameter(dbCommand, "@GoodsDescription", DbType.String, goodsDescription);
                db.AddInParameter(dbCommand, "@IsWoodPacking", DbType.Boolean, isWoodPacking);
                db.AddInParameter(dbCommand, "@IssuePlaceID", DbType.Guid, issuePlaceID);
                db.AddInParameter(dbCommand, "@IssueByID", DbType.Guid, issueByID);
                db.AddInParameter(dbCommand, "@IssueDate", DbType.DateTime, issueDate);
                db.AddInParameter(dbCommand, "@WoodPackingDescription", DbType.String, woodPacking);
                db.AddInParameter(dbCommand, "@ctnQtyInfo", DbType.String, ctnQtyInfo);
                db.AddInParameter(dbCommand, "@AgentText", DbType.String, agentText);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IssueType", DbType.Int16, issueType);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@FreightRateID", DbType.Guid, FreightRateID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, eta);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, etd);
                db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, preETD);


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

                //DataSet ds = db.ExecuteDataSet(dbCommand);
                //if (ds == null || ds.Tables.Count == 0)
                //{
                //    return null;
                //}


                //DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault<DataRow>();
                //if (row == null)
                //{
                //    return null;
                //}

                //SingleResult result = new SingleResult();
                //result.Add("ID", row.Field<Guid>("ID"));
                //result.Add("UpdateDate", row.Field<DateTime?>("UpdateDate"));


                //DataRow row2 = ds.Tables[1].AsEnumerable().FirstOrDefault<DataRow>();
                //if (row2 == null)
                //{
                //    return result;
                //}

                //result.Add("IsFirstMBL", row2.Field<bool>("IsFirstMBL"));
                //result.Add("HasContainer", row2.Field<bool>("HasContainer"));

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
        public void RemoveOceanMBLInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanMBLInfo");

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
        public SingleResult ChangeOceanMBLState(
            Guid id,
            OEBLState state,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeOceanMBLState");

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
        /// 确认装船类型
        /// </summary>
        /// <param name="shippingOrderID">ShippingOrderID</param>
        /// <param name="preVoyageID">preVoyageID</param>
        /// <param name="voyageID">voyageID</param>
        /// <param name="onBoardType">确认装船类型（0不确定，1前程确定，2确定）</param>
        /// <param name="confirmByID">确认人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="shippingOrderUpdateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ConfirmOnBoardType(
            Guid shippingOrderID,
            Guid? preVoyageID,
            Guid? voyageID,
            ConfirmOnBoardType onBoardType,
            Guid confirmByID,
            DateTime? shippingOrderUpdateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(shippingOrderID, "shippingOrderID");
            ArgumentHelper.AssertGuidNotEmpty(confirmByID, "confirmByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspConfirmOnBoardType");

                db.AddInParameter(dbCommand, "@ShippingOrderID", DbType.Guid, shippingOrderID);
                db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, preVoyageID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, voyageID);
                db.AddInParameter(dbCommand, "@ShippingOrderUpdateDate", DbType.DateTime, shippingOrderUpdateDate);
                db.AddInParameter(dbCommand, "@OnBoardType", DbType.Int16, onBoardType);
                db.AddInParameter(dbCommand, "@ConfirmByID", DbType.Guid, confirmByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "UpdateDate", "ShippingOrderUpdateDate" });
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

        #region HBL

        /// <summary>
        /// 获取HBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回HBL提单信息</returns>
        public OceanHBLInfo GetOceanHBLInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanHBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanHBLInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new OceanHBLInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           OceanBookingID = b.Field<Guid>("OceanBookingID"),
                                           MBLID = b.IsNull("OceanMBLID") ? Guid.Empty : b.Field<Guid>("OceanMBLID"),
                                           CompanyID = b.Field<Guid>("CompanyID"),
                                           No = b.Field<string>("HBLNo"),
                                           RefNo = b.Field<string>("RefNo"),
                                           NumberOfOriginal = b.IsNull("NumberOfOriginal") ? (short?)null : (short)b.Field<byte>("NumberOfOriginal"),
                                           VoyageShowType = (VoyageShowType)b.Field<byte>("VoyageShowType"),
                                           CheckerID = b.Field<Guid?>("CheckerID"),
                                           ShipperID = b.Field<Guid?>("ShipperID"),
                                           ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                           ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                           ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                           NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                           NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                           PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                           PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                           PreVoyageID = b.Field<Guid?>("PreVoyageID"),
                                           PreVesselVoyage = b.Field<string>("PreVoyageName"),
                                           PODID = b.Field<Guid>("PODID"),
                                           POLID = b.Field<Guid>("POLID"),
                                           VoyageID = b.Field<Guid?>("VoyageID"),
                                           VesselVoyage = b.Field<string>("VoyageName"),
                                           POLName = b.Field<string>("POLName"),
                                           PODName = b.Field<string>("PODName"),
                                           PlaceOfDeliveryID = b.Field<Guid?>("PlaceOfDeliveryID"),
                                           PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                           FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                           FinalDestinationName = b.Field<string>("FinalDestinationName"),
                                           TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                           TransportClauseName = b.Field<string>("TransportClauseName"),
                                           PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                           PaymentTermName = b.Field<string>("PaymentTermName"),
                                           FreightDescription = b.Field<string>("FreightDescription"),
                                           ReleaseType = (FCMReleaseType)b.Field<byte>("ReleaseType"),
                                           ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                           Quantity = b.Field<int>("Quantity"),
                                           QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                                           QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                           Weight = b.Field<decimal>("Weight"),
                                           WeightUnitID = b.Field<Guid?>("WeightUnitID"),
                                           WeightUnitName = b.Field<string>("WeightUnitName"),
                                           Measurement = b.Field<decimal>("Measurement"),
                                           MeasurementUnitID = b.Field<Guid?>("MeasurementUnitID"),
                                           MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                           Marks = b.Field<string>("Marks"),
                                           GoodsDescription = b.Field<string>("GoodsDescription"),
                                           IsWoodPacking = b.Field<bool>("IsWoodPacking"),
                                           ContainerDescription = b.Field<string>("ContainerDescription") == null ? string.Empty : b.Field<string>("ContainerDescription").Replace(char.ConvertFromUtf32(10), "\r\n"),
                                           IssuePlaceID = b.Field<Guid>("IssuePlaceID"),
                                           IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                           IssueByID = b.Field<Guid>("IssueByID"),
                                           IssueByName = b.Field<string>("IssueByName"),
                                           IssueDate = b.Field<DateTime?>("IssueDate"),
                                           State = (OEBLState)b.Field<byte>("State"),
                                           AMSNo = b.Field<string>("AMSNo"),
                                           AMSShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AMSShipperDescription")),
                                           AMSConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AMSConsigneeDescription")),
                                           AMSNotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AMSNotifyPartyDescription")),
                                           ISFNo = b.Field<string>("ISFNo"),
                                           ACIEntryType = (ACIEntryType)b.Field<byte>("ACIEntryType"),
                                           AMSEntry = (AMSEntryType)b.Field<byte>("AMSEntryType"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                           AgentID = b.Field<Guid?>("AgentID"),
                                           AgentName = b.Field<string>("AgentName"),
                                           CheckerName = b.Field<string>("CheckerName"),
                                           ConsigneeName = b.Field<string>("ConsigneeName"),
                                           PreETD = b.Field<DateTime?>("PreETD"),
                                           ETA = b.Field<DateTime?>("ETA"),
                                           ETD = b.Field<DateTime?>("ETD"),
                                           FinalDestinationCode = b.Field<string>("FinalDestinationCode"),
                                           MBLNo = b.Field<string>("MBLNo"),
                                           NotifyPartyName = b.Field<string>("NotifyPartyName"),
                                           PlaceOfDeliveryCode = b.Field<string>("PlaceOfDeliveryCode"),
                                           PlaceOfReceiptCode = b.Field<string>("PlaceOfReceiptCode"),
                                           PODCode = b.Field<string>("PODCode"),
                                           POLCode = b.Field<string>("POLCode"),
                                           ShipperName = b.Field<string>("ShipperName"),
                                           SONO = b.Field<string>("SONO"),
                                           TranshipmentPortName = b.Field<string>("TranshipmentPortName"),
                                           IssueType = b.IsNull("IssueType") ? IssueType.Unknown : (IssueType)b.Field<byte>("IssueType"),
                                           WoodPacking = b.Field<string>("WoodPackingDescription"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           CarrierName = b.Field<string>("CarrierName"),
                                           IsRequestAgent = b.Field<bool>("IsRequestAgent"),
                                           ContainerNos = b.Field<string>("ContainerNos"),
                                           CustomerName = b.Field<string>("CustomerName"),
                                           FilerName = b.Field<string>("FilerName"),
                                           BookingerName = b.Field<string>("BookingerName"),
                                           SalesName = b.Field<string>("SalesName"),
                                           AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                           OverseasFilerName = b.Field<string>("OverseasFilerName"),

                                           ExistFees = b.Field<bool>("ExistFees"),
                                           CtnQtyInfo = b.Field<string>("CtnQtyInfo"),
                                           OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                           ShippingOrderID = b.Field<Guid?>("ShippingOrderID"),
                                           ShippingOrderUpdateDate = b.Field<DateTime?>("ShippingOrderUpdateDate"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           BookingPaymentTermID = b.Field<Guid?>("BookingPaymentTermID"),
                                           ContainerQtyDescription = b.Field<string>("ContainerQtyDescription"),
                                           ContractNo = b.Field<string>("ContractNo"),
                                           ContractID = b.Field<Guid?>("ContractID"),
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
        /// <returns>返回 "ID", "UpdateDate", "No" ,"OceanMBLID"</returns>
        SingleResult SaveOceanHBLInfo(
            Guid? id,
            Guid oceanBookingID,
            Guid mblID,
            string mblNO,
            string hblNo,
            short? numberOfOriginal,
            VoyageShowType voyageShowType,
            Guid? checkerID,
            Guid? shipperID,
            CustomerDescription shipperDescription,
            Guid? consigneeID,
            CustomerDescription consigneeDescription,
            Guid? notifyPartyID,
            CustomerDescription notifyPartyDescription,
            Guid? agentID,
            CustomerDescription agentDescription,
            Guid? placeOfReceiptID,
            string placeOfReceiptName,
            Guid? preVoyageID,
            Guid? voyageID,
            Guid polID,
            string polName,
            Guid podID,
            string podName,
            Guid? placeOfDeliveryID,
            string placeOfDeliveryName,
            Guid? finalDestinationID,
            string finalDestinationName,
            Guid transportClauseID,
            Guid? paymentTermID,
            string freightDescription,
            FCMReleaseType releaseType,
            DateTime? releaseDate,
            int quantity,
            Guid quantityUnitID,
            decimal? weight,
            Guid? weightUnitID,
            decimal? measurement,
            Guid? measurementUnitID,
            string marks,
            string goodsDescription,
            bool isWoodPacking,
            string ctnQtyInfo,
            Guid issuePlaceID,
            Guid issueByID,
            DateTime? issueDate,
            string amsNo,
            CustomerDescription amsShipperDescription,
            CustomerDescription amsConsigneeDescription,
            CustomerDescription amsNotifyPartyDescription,
            string isfNo,
            ACIEntryType? aciEntryType,
            AMSEntryType? amsEntryType,
            string woodPacking,
            IssueType issueType,
            Guid saveByID,
            DateTime? updateDate,
            DateTime? mblUpdateDate,
            bool IsSynToMBL,
            DateTime? preETD,
            DateTime? ETD,
            DateTime? ETA
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(transportClauseID, "transportClauseID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanHBLInfo");

                string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(shipperDescription, true, false);
                string tempconsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(consigneeDescription, true, false);
                string tempnotifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(notifyPartyDescription, true, false);
                string tempagentDescription = SerializerHelper.SerializeToString<CustomerDescription>(agentDescription, true, false);
                string tempamsShipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(amsShipperDescription, true, false);
                string tempamsConsigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(amsConsigneeDescription, true, false);
                string tempamsNotifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(amsNotifyPartyDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@MblNo", DbType.String, mblNO);
                db.AddInParameter(dbCommand, "@HblNo", DbType.String, hblNo);
                db.AddInParameter(dbCommand, "@NumberOfOriginal", DbType.Int32, numberOfOriginal);
                db.AddInParameter(dbCommand, "@VoyageShowType", DbType.Int16, voyageShowType);
                db.AddInParameter(dbCommand, "@CheckerID", DbType.Guid, checkerID);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, shipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, consigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempconsigneeDescription);
                db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, notifyPartyID);
                db.AddInParameter(dbCommand, "@NotifyPartyDescription", DbType.Xml, tempnotifyPartyDescription);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, agentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, tempagentDescription);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, placeOfReceiptID);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptName", DbType.String, placeOfReceiptName);
                db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, preVoyageID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, voyageID);
                db.AddInParameter(dbCommand, "@PolID", DbType.Guid, polID);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodID", DbType.Guid, podID);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, placeOfDeliveryID);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDeliveryName);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, finalDestinationID);
                db.AddInParameter(dbCommand, "@FinalDestinationName", DbType.String, finalDestinationName);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, transportClauseID);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, paymentTermID);
                db.AddInParameter(dbCommand, "@FreightDescription", DbType.String, freightDescription);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Int16, releaseType);
                db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, releaseDate);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, quantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, weightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, measurementUnitID);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, marks);
                db.AddInParameter(dbCommand, "@GoodsDescription", DbType.String, goodsDescription);
                db.AddInParameter(dbCommand, "@IsWoodPacking", DbType.Boolean, isWoodPacking);
                db.AddInParameter(dbCommand, "@IssuePlaceID", DbType.Guid, issuePlaceID);
                db.AddInParameter(dbCommand, "@IssueByID", DbType.Guid, issueByID);
                db.AddInParameter(dbCommand, "@IssueDate", DbType.DateTime, issueDate);
                db.AddInParameter(dbCommand, "@AmsNo", DbType.String, amsNo);
                db.AddInParameter(dbCommand, "@AmsShipperDescription", DbType.Xml, tempamsShipperDescription);
                db.AddInParameter(dbCommand, "@AmsConsigneeDescription", DbType.Xml, tempamsConsigneeDescription);
                db.AddInParameter(dbCommand, "@AmsNotifyPartyDescription", DbType.Xml, tempamsNotifyPartyDescription);
                db.AddInParameter(dbCommand, "@IsfNo", DbType.String, isfNo);
                db.AddInParameter(dbCommand, "@AciEntryType", DbType.Byte, aciEntryType);
                db.AddInParameter(dbCommand, "@AmsEntryType", DbType.Byte, amsEntryType);
                db.AddInParameter(dbCommand, "@WoodPackingDescription", DbType.String, woodPacking);
                db.AddInParameter(dbCommand, "@ctnQtyInfo", DbType.String, ctnQtyInfo);
                db.AddInParameter(dbCommand, "@IssueType", DbType.Int16, issueType);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);

                db.AddInParameter(dbCommand, "@MBLUpdateDate", DbType.DateTime, mblUpdateDate);
                db.AddInParameter(dbCommand, "@IsSyn", DbType.Boolean, IsSynToMBL);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, preETD);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, ETA);


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "AMSNo", "OceanMBLID", "MBLUpdateDate" });


                //DataSet ds = db.ExecuteDataSet(dbCommand);
                //if (ds == null || ds.Tables.Count == 0)
                //{
                //    return null;
                //}


                //DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault<DataRow>();
                //if (row == null)
                //{
                //    return null;
                //}

                //SingleResult result = new SingleResult();
                //result.Add("ID", row.Field<Guid>("ID"));
                //result.Add("UpdateDate", row.Field<DateTime?>("UpdateDate"));
                //result.Add("No", row.Field<string>("No"));
                //result.Add("OceanMBLID", row.Field<Guid>("OceanMBLID"));
                //result.Add("MBLUpdateDate", row.Field<DateTime?>("MBLUpdateDate"));


                //DataRow row2 = ds.Tables[1].AsEnumerable().FirstOrDefault<DataRow>();
                //if (row2 == null)
                //{
                //    return result;
                //}

                //result.Add("HasContainer", row2.Field<bool>("HasContainer"));

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
        public void RemoveOceanHBLInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanHBLInfo");

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
        public SingleResult ChangeOceanHBLState(
            Guid id,
            OEBLState state,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeOceanHBLState");

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


        #region Container

        /// <summary>
        /// 获取MBL箱列表
        /// </summary>
        /// <param name="mblID">MBL ID</param>
        /// <returns>返回MBL箱列表</returns>
        public List<OceanBLContainerList> GetOceanMBLContainerList(Guid mblID)
        {
            ArgumentHelper.AssertGuidNotEmpty(mblID, "mblID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanMBLContainerList");

                db.AddInParameter(dbCommand, "@MblID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLContainerList> results = BulidBLContainerListByDataSet(ds);

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
        /// 获取HBL单的箱列表 
        /// </summary>
        /// <param name="hblID">HBL ID</param>
        /// <returns>返回箱列表</returns>
        public List<OceanBLContainerList> GetOceanHBLContainerList(Guid hblID)
        {
            ArgumentHelper.AssertGuidNotEmpty(hblID, "hblID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanHBLContainerList");

                db.AddInParameter(dbCommand, "@HblID", DbType.Guid, hblID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLContainerList> results = BulidBLContainerListByDataSet(ds);

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

        private static List<OceanBLContainerList> BulidBLContainerListByDataSet(DataSet ds)
        {
            List<OceanBLContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new OceanBLContainerList
                                                  {
                                                      ID = b.IsNull("ID") ? Guid.Empty : b.Field<Guid>("ID"),
                                                      ShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                                      OceanBookingID = b.Field<Guid>("OceanBookingID"),
                                                      BLID = b.Field<Guid?>("BLID"),
                                                      Relation = b.Field<Guid?>("BLID") == null ? false : true,
                                                      No = b.Field<string>("No"),
                                                      TypeID = b.Field<Guid>("TypeID"),
                                                      TypeName = b.Field<string>("TypeName"),
                                                      SealNo = b.Field<string>("SealNo"),
                                                      PONO = b.Field<string>("PONO"),
                                                      IsPartOf = b.Field<bool>("IsPartOf"),
                                                      IsSOC = b.Field<bool>("IsSOC"),
                                                      Marks = b.Field<string>("Marks"),
                                                      Commodity = b.Field<string>("Commodity"),
                                                      Quantity = b.Field<int>("Quantity"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      Weight = b.Field<decimal>("Weight"),
                                                      Measurement = b.Field<decimal>("Measurement"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      CargoID = b.Field<Guid?>("CargoID"),
                                                      CargoCreateBy = b.Field<Guid?>("CargoCreateBy"),
                                                      CargoCreateDate = b.Field<DateTime?>("CargoCreateDate"),
                                                      CargoUpdateDate = b.Field<DateTime?>("CargoUpdateDate"),
                                                      IsDirty = false,
                                                  }).ToList();
            return results;
        }

        /// <summary>
        /// 保存MBL的箱信息
        /// </summary>
        /// <param name="oceanBookingID">订舱ID</param>
        /// <param name="mblID">MBL ID</param>
        /// <param name="relation">关联</param>
        /// <param name="ids">OceanContainers箱ID列表</param>
        /// <param name="cargoIds">OceanBLContainersID列表</param>
        /// <param name="containerNos">箱号列表</param>
        /// <param name="containerSOs">装货单号列表</param>
        /// <param name="containerTypeIDs">箱型列表</param>
        /// <param name="containerSealNos">封条号列表</param>
        /// <param name="containerMarks">埋头列表</param>
        /// <param name="containerCommoditys">品名列表</param>
        /// <param name="containerQuantitys">数量列表</param>
        /// <param name="containerWeights">箱重量列表</param>
        /// <param name="containerMeasurements">箱体积列表</param>
        /// <param name="containerIsSOCs">是否客户自有箱列表</param>
        /// <param name="containerIsPartOfs">是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">OceanContainers更新时间-做数据版本用</param>
        /// <param name="cargoUpdateDates">货物更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveOceanMBLContainerInfo(
            Guid oceanBookingID,
            Guid mblID,
            bool[] relations,
            Guid?[] ids,
            Guid?[] cargoIds,
            string[] containerNos,
            string[] containerSOs,
            Guid[] containerTypeIDs,
            string[] containerSealNos,
            string[] containerMarks,
            string[] containerCommoditys,
            int[] containerQuantitys,
            decimal[] containerWeights,
            decimal[] containerMeasurements,
            bool[] containerIsSOCs,
            bool[] containerIsPartOfs,
            Guid saveByID,
            DateTime?[] updateDates,
            DateTime?[] cargoUpdateDates,
            bool IsSynToHBL)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(mblID, "mblID");
            ArgumentHelper.AssertGuidNotEmpty(containerTypeIDs, "containerTypeIDs");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 relations,
                 ids,
                 cargoIds,
                 containerNos,
                 containerSOs,
                 containerTypeIDs,
                 containerSealNos,
                 containerCommoditys,
                 containerQuantitys,
                 containerWeights,
                 containerMeasurements,
                 containerIsSOCs,
                 containerIsPartOfs
                 , cargoUpdateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanMBLContainerInfo");

                string tempRelations = relations.Join();
                string tempIds = ids.Join();
                string tempCargoIds = cargoIds.Join();

                string tempcontainerNos = containerNos.Join();
                string tempcontainerSOs = containerSOs.Join();
                string tempcontainerTypeIDs = containerTypeIDs.Join();
                string tempcontainerSealNos = containerSealNos.Join();
                string tempcontainerMarks = containerMarks.Join();
                string tempcontainerCommoditys = containerCommoditys.Join();
                string tempcontainerQuantitys = containerQuantitys.Join();
                string tempcontainerWeights = containerWeights.Join(3);
                string tempcontainerMeasurements = containerMeasurements.Join(3);
                string tempcontainerIsSOCs = containerIsSOCs.Join();
                string tempcontainerIsPartOfs = containerIsPartOfs.Join();
                string tempupdateDates = updateDates.Join();
                string tempcargoUpdateDates = cargoUpdateDates.Join();

                db.AddInParameter(dbCommand, "@Relations", DbType.String, tempRelations);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CargoIds", DbType.String, tempCargoIds);


                db.AddInParameter(dbCommand, "@ContainerNos", DbType.String, tempcontainerNos);
                db.AddInParameter(dbCommand, "@OceanContainerSOs", DbType.String, tempcontainerSOs);
                db.AddInParameter(dbCommand, "@ContainerTypeIDs", DbType.String, tempcontainerTypeIDs);
                db.AddInParameter(dbCommand, "@OceanContainerSealNos", DbType.String, tempcontainerSealNos);
                db.AddInParameter(dbCommand, "@ContainerIsSOCs", DbType.String, tempcontainerIsSOCs);
                db.AddInParameter(dbCommand, "@ContainerIsPartOfs", DbType.String, tempcontainerIsPartOfs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempupdateDates);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, tempcontainerMarks);
                db.AddInParameter(dbCommand, "@Commodities", DbType.String, tempcontainerCommoditys);
                db.AddInParameter(dbCommand, "@Quantities", DbType.String, tempcontainerQuantitys);
                db.AddInParameter(dbCommand, "@Weights", DbType.String, tempcontainerWeights);
                db.AddInParameter(dbCommand, "@Measurements", DbType.String, tempcontainerMeasurements);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsSyn", DbType.Boolean, IsSynToHBL);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@CargoUpdateDates", DbType.String, tempcargoUpdateDates);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "CargoID", "UpdateDate", "CargoUpdateDate" });
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
        /// 保存HBL的箱信息
        /// </summary>
        /// <param name="oceanBookingID">订舱ID</param>
        /// <param name="hblID">HBL ID</param>
        /// <param name="relation">关联</param>
        /// <param name="ids">OceanContainers箱ID列表</param>
        /// <param name="cargoIds">OceanBLContainersID列表</param>
        /// <param name="containerNos">箱号列表</param>
        /// <param name="containerSOs">装货单号列表</param>
        /// <param name="containerTypeIDs">箱型列表</param>
        /// <param name="containerSealNos">封条号列表</param>
        /// <param name="containerMarks">埋头列表</param>
        /// <param name="containerCommoditys">品名列表</param>
        /// <param name="containerQuantitys">数量列表</param>
        /// <param name="containerWeights">箱重量列表</param>
        /// <param name="containerMeasurements">箱体积列表</param>
        /// <param name="containerIsSOCs">是否客户自有箱列表</param>
        /// <param name="containerIsPartOfs">是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">OceanContainers更新时间-做数据版本用</param>
        /// <param name="cargoUpdateDates">货物更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveOceanHBLContainerInfo(
            Guid oceanBookingID,
            Guid hblID,
            bool[] relations,
            Guid?[] ids,
            Guid?[] cargoIds,
            string[] containerNos,
            string[] containerSOs,
            Guid[] containerTypeIDs,
            string[] containerSealNos,
            string[] containerMarks,
            string[] containerCommoditys,
            int[] containerQuantitys,
            decimal[] containerWeights,
            decimal[] containerMeasurements,
            bool[] containerIsSOCs,
            bool[] containerIsPartOfs,
            Guid saveByID,
            DateTime?[] updateDates,
            DateTime?[] cargoUpdateDates,
            bool IsSynToMBL
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(hblID, "hblID");
            ArgumentHelper.AssertGuidNotEmpty(containerTypeIDs, "containerTypeIDs");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 relations,
                 ids,
                 cargoIds,
                 containerNos,
                 containerSOs,
                 containerTypeIDs,
                 containerSealNos,
                 containerCommoditys,
                 containerQuantitys,
                 containerWeights,
                 containerMeasurements,
                 containerIsSOCs,
                 containerIsPartOfs,
                 updateDates,
                 cargoUpdateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanHBLContainerInfo");

                string tempRelations = relations.Join();
                string tempIds = ids.Join();
                string tempCargoIds = cargoIds.Join();

                string tempcontainerNos = containerNos.Join();
                string tempcontainerSOs = containerSOs.Join();
                string tempcontainerTypeIDs = containerTypeIDs.Join();
                string tempcontainerSealNos = containerSealNos.Join();
                string tempcontainerMarks = containerMarks.Join();
                string tempcontainerCommoditys = containerCommoditys.Join();
                string tempcontainerQuantitys = containerQuantitys.Join();
                string tempcontainerWeights = containerWeights.Join(3);
                string tempcontainerMeasurements = containerMeasurements.Join(3);
                string tempcontainerIsSOCs = containerIsSOCs.Join();
                string tempcontainerIsPartOfs = containerIsPartOfs.Join();
                string tempupdateDates = updateDates.Join();
                string tempcargoUpdateDates = cargoUpdateDates.Join();

                db.AddInParameter(dbCommand, "@Relations", DbType.String, tempRelations);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@HBLID", DbType.Guid, hblID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CargoIds", DbType.String, tempCargoIds);


                db.AddInParameter(dbCommand, "@ContainerNos", DbType.String, tempcontainerNos);
                db.AddInParameter(dbCommand, "@OceanContainerSOs", DbType.String, tempcontainerSOs);
                db.AddInParameter(dbCommand, "@ContainerTypeIDs", DbType.String, tempcontainerTypeIDs);
                db.AddInParameter(dbCommand, "@OceanContainerSealNos", DbType.String, tempcontainerSealNos);
                db.AddInParameter(dbCommand, "@ContainerIsSOCs", DbType.String, tempcontainerIsSOCs);
                db.AddInParameter(dbCommand, "@ContainerIsPartOfs", DbType.String, tempcontainerIsPartOfs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempupdateDates);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, tempcontainerMarks);
                db.AddInParameter(dbCommand, "@Commodities", DbType.String, tempcontainerCommoditys);
                db.AddInParameter(dbCommand, "@Quantities", DbType.String, tempcontainerQuantitys);
                db.AddInParameter(dbCommand, "@Weights", DbType.String, tempcontainerWeights);
                db.AddInParameter(dbCommand, "@Measurements", DbType.String, tempcontainerMeasurements);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsSyn", DbType.Boolean, IsSynToMBL);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@CargoUpdateDates", DbType.String, tempcargoUpdateDates);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "CargoID", "UpdateDate", "CargoUpdateDate", "MBLUpdateDate" });
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

        #region Other

        /// <summary>
        /// 获取提单的货物信息列表
        /// </summary>
        /// <param name="blIDs">提单ID</param>
        /// <returns>计量信息列表</returns>
        public List<OceanContainerCargoList> GetOceanBLMeasureInfoList(Guid[] blIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBLMeasureInfoListByBLIDs");

                db.AddInParameter(dbCommand, "@BLIDs", DbType.String, blIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanContainerCargoList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new OceanContainerCargoList
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
        public SingleResult MergeOceanBL(Guid id, Guid[] blID, Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspMergeOceanBL");

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
        public ManyResult SplitOceanBL(string xml, Guid saveById)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSplitOceanBL");

                db.AddInParameter(dbCommand, "@XML", DbType.String, xml);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "OceanMBLID" });
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

        #region 需要以事务方式保存方法

        /// <summary>
        /// 以事务的方式保存MBL跟箱信息
        /// </summary>
        /// <param name="blParameter"></param>
        /// <param name="ctnParameter"></param>
        /// <returns></returns>
        public SingleResult SaveOceanMBLAndContainerWithTrans(SaveMBLInfoParameter blParameter, SaveBLContainerParameter ctnParameter, List<Guid> ctnIDList, List<DateTime?> ctnDateList, bool isSynToHBL)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                SingleResult result = new SingleResult();
                //保存MBL
                SingleResult blReslut = SaveOceanMBLInfo(blParameter);
                ctnParameter.blID = blReslut.GetValue<Guid>("ID");

                //删除箱列表
                if (ctnIDList != null && ctnIDList.Count > 0)
                {
                    RemoveOceanContaierInfo(ctnIDList.ToArray(), blParameter.saveByID, ctnDateList.ToArray());
                }

                //保存箱列表
                if (ctnParameter != null)
                {
                    result.Add("ContainerResult", SaveOceanMBLContainerInfo(ctnParameter, isSynToHBL));
                    result.Add("BLResult", blReslut);
                }

                scope.Complete();
                return result;
            }
        }

        /// <summary>
        /// 以事务的方式保存HBL跟箱信息
        /// </summary>
        /// <param name="blParameter"></param>
        /// <param name="ctnParameter"></param>
        /// <returns></returns>
        public SingleResult SaveOceanHBLAndContainerWithTrans(SaveHBLInfoParameter blParameter, SaveBLContainerParameter ctnParameter, List<Guid> ctnIDList, List<DateTime?> ctnDateList, bool IsSynToMBL)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                SingleResult result = new SingleResult();
                //保存HBL
                SingleResult blReslut = SaveOceanHBLInfo(blParameter, IsSynToMBL);
                if (ctnParameter != null)
                {
                    ctnParameter.blID = blReslut.GetValue<Guid>("ID");
                }

                //删除箱列表
                if (ctnIDList != null && ctnIDList.Count > 0)
                {
                    RemoveOceanContaierInfo(ctnIDList.ToArray(), blParameter.saveByID, ctnDateList.ToArray());
                }

                //保存箱列表
                if (ctnParameter != null)
                {
                    result.Add("ContainerResult", SaveOceanHBLContainerInfo(ctnParameter, IsSynToMBL));                  
                }

                result.Add("BLResult", blReslut);
                scope.Complete();
                return result;
            }
        }

        #region 转换保存

        /// <summary>
        /// 保存MBL的箱信息
        /// </summary>
        public SingleResult SaveOceanMBLInfo(SaveMBLInfoParameter parameter)
        {
            return SaveOceanMBLInfo(parameter.id,
                                    parameter.oceanBookingID,
                                    parameter.mblNo,
                                    parameter.numberOfOriginal,
                                    parameter.voyageShowType,
                                    parameter.checkerID,
                                    parameter.shipperID,
                                    parameter.shipperDescription,
                                    parameter.consigneeID,
                                    parameter.consigneeDescription,
                                    parameter.notifyPartyID,
                                    parameter.notifyPartyDescription,
                                    parameter.agentID,
                                    parameter.agentDescription,
                                    parameter.placeOfReceiptID,
                                    parameter.placeOfReceiptName,
                                    parameter.preVoyageID,
                                    parameter.voyageID,
                                    parameter.polID,
                                    parameter.polName,
                                    parameter.podID,
                                    parameter.podName,
                                    parameter.placeOfDeliveryID,
                                    parameter.placeOfDeliveryName,
                                    parameter.finalDestinationID,
                                    parameter.finalDestinationName,
                                    parameter.transportClauseID,
                                    parameter.paymentTermID,
                                    parameter.freightDescription,
                                    parameter.releaseType,
                                    parameter.releaseDate,
                                    parameter.quantity,
                                    parameter.quantityUnitID,
                                    parameter.weight,
                                    parameter.weightUnitID,
                                    parameter.measurement,
                                    parameter.measurementUnitID,
                                    parameter.marks,
                                    parameter.goodsDescription,
                                    parameter.isWoodPacking,
                                    parameter.ctnQtyInfo,
                                    parameter.issuePlaceID,
                                    parameter.issueByID,
                                    parameter.issueDate,
                                    parameter.woodPacking,
                                    parameter.issueType,
                                    parameter.saveByID,
                                    parameter.AgentText,
                                    parameter.FreightRateID,
                                    parameter.updateDate,
                                    parameter.ETA,
                                    parameter.ETD,
                                    parameter.PreETD);
        }

        /// <summary>
        /// 保存HBL的箱信息
        /// </summary>
        public SingleResult SaveOceanHBLInfo(SaveHBLInfoParameter parameter, bool IsSynToMBL)
        {
            return SaveOceanHBLInfo(
                                    parameter.id,
                                    parameter.oceanBookingID,
                                    parameter.mblID,
                                    parameter.mblNO,
                                    parameter.hblNo,
                                    parameter.numberOfOriginal,
                                    parameter.voyageShowType,
                                    parameter.checkerID,
                                    parameter.shipperID,
                                    parameter.shipperDescription,
                                    parameter.consigneeID,
                                    parameter.consigneeDescription,
                                    parameter.notifyPartyID,
                                    parameter.notifyPartyDescription,
                                    parameter.agentID,
                                    parameter.agentDescription,
                                    parameter.placeOfReceiptID,
                                    parameter.placeOfReceiptName,
                                    parameter.preVoyageID,
                                    parameter.voyageID,
                                    parameter.polID,
                                    parameter.polName,
                                    parameter.podID,
                                    parameter.podName,
                                    parameter.placeOfDeliveryID,
                                    parameter.placeOfDeliveryName,
                                    parameter.finalDestinationID,
                                    parameter.finalDestinationName,
                                    parameter.transportClauseID,
                                    parameter.paymentTermID,
                                    parameter.freightDescription,
                                    parameter.releaseType,
                                    parameter.releaseDate,
                                    parameter.quantity,
                                    parameter.quantityUnitID,
                                    parameter.weight,
                                    parameter.weightUnitID,
                                    parameter.measurement,
                                    parameter.measurementUnitID,
                                    parameter.marks,
                                    parameter.goodsDescription,
                                    parameter.isWoodPacking,
                                    parameter.ctnQtyInfo,
                                    parameter.issuePlaceID,
                                    parameter.issueByID,
                                    parameter.issueDate,
                                    parameter.amsNo,
                                    parameter.amsShipperDescription,
                                    parameter.amsConsigneeDescription,
                                    parameter.amsNotifyPartyDescription,
                                    parameter.isfNo,
                                    parameter.aciEntryType,
                                    parameter.amsEntryType,
                                    parameter.woodPacking,
                                    parameter.issueType,
                                    parameter.saveByID,
                                    parameter.updateDate,
                                    parameter.mblUpdateDate,
                                    IsSynToMBL,
                                    parameter.PreETD,
                                    parameter.ETD,
                                    parameter.ETA
                                    );
        }

        /// <summary>
        /// 保存HBL的箱信息
        /// </summary>
        ManyResult SaveOceanHBLContainerInfo(SaveBLContainerParameter parameter, bool IsSynToMBL)
        {
            return SaveOceanHBLContainerInfo(parameter.oceanBookingID,
                                            parameter.blID,
                                            parameter.relations,
                                            parameter.ids,
                                            parameter.cargoIds,
                                            parameter.containerNos,
                                            parameter.containerSOs,
                                            parameter.containerTypeIDs,
                                            parameter.containerSealNos,
                                            parameter.containerMarks,
                                            parameter.containerCommoditys,
                                            parameter.containerQuantitys,
                                            parameter.containerWeights,
                                            parameter.containerMeasurements,
                                            parameter.containerIsSOCs,
                                            parameter.containerIsPartOfs,
                                            parameter.saveByID,
                                            parameter.updateDates,
                                            parameter.cargoUpdateDates,
                                            IsSynToMBL
                                            );
        }

        /// <summary>
        /// 保存MBL的箱信息
        /// </summary>
        ManyResult SaveOceanMBLContainerInfo(SaveBLContainerParameter parameter, bool isSynToHBL)
        {
            return SaveOceanMBLContainerInfo(parameter.oceanBookingID,
                                            parameter.blID,
                                            parameter.relations,
                                            parameter.ids,
                                            parameter.cargoIds,
                                            parameter.containerNos,
                                            parameter.containerSOs,
                                            parameter.containerTypeIDs,
                                            parameter.containerSealNos,
                                            parameter.containerMarks,
                                            parameter.containerCommoditys,
                                            parameter.containerQuantitys,
                                            parameter.containerWeights,
                                            parameter.containerMeasurements,
                                            parameter.containerIsSOCs,
                                            parameter.containerIsPartOfs,
                                            parameter.saveByID,
                                            parameter.updateDates,
                                            parameter.cargoUpdateDates,
                                            isSynToHBL);
        }

        #endregion

        #endregion
    }
}

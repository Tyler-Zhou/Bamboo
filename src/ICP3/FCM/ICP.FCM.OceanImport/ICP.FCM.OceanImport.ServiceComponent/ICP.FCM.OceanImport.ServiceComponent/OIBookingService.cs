using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.OceanImport.ServiceInterface;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using System.Transactions;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.SubscriptionPublish.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using System.Diagnostics;
using System.Globalization;
using EnumIsolationLevel = System.Transactions.IsolationLevel;
using ICP.FileSystem.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;

namespace ICP.FCM.OceanImport.ServiceComponent
{
    partial class OceanImportService : PublishService<ISubscriptionPublishNotifyService>
    {

        #region 根据ID集合查询列表
        /// <summary>
        /// 根据ID集合获得业务列表数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<OceanBusinessList> GetBusinessListByIds(Guid[] ids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIBusinessListByIds");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetBusinessList(ds);
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

        #region 从快速查询界面查询业务数据
        /// <summary>
        /// 从快速查询界面查询业务数据
        /// </summary>
        /// <param name="companyIDs"></param>
        /// <param name="noSearchType"></param>
        /// <param name="no"></param>
        /// <param name="customerSearchType"></param>
        /// <param name="customerName"></param>
        /// <param name="portSearchType"></param>
        /// <param name="portName"></param>
        /// <param name="dateSearchType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        public List<OceanBusinessList> GetBusinessListByFastSearch(
                         Guid[] companyIDs,
                         OIBusinessNoSearchType noSearchType,
                         string no,
                         OIBusinessCustomerSearchType customerSearchType,
                         string customerName,
                         OIBusinessPortSearchType portSearchType,
                         string portName,
                         OIBusinessDateSearchType dateSearchType,
                         DateTime? beginTime,
                         DateTime? endTime,
                         Guid? filerID,
                         int maxRecords)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIBusinessListByFastSearch");

                string companyIDList = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDList);
                db.AddInParameter(dbCommand, "@NoSearchType", DbType.Byte, noSearchType);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerSearchType", DbType.Byte, customerSearchType);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@PortSearchType", DbType.Byte, portSearchType);
                db.AddInParameter(dbCommand, "@PortName", DbType.String, portName);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Byte, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, filerID);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetBusinessList(ds);
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

        #region 从查询界面查询数据
        /// <summary>
        /// 从查询界面获得业务数据
        /// </summary>
        /// <returns></returns>
        public List<OceanBusinessList> GetBusinessList(
                                         Guid[] companyIDs,
                                         string no,
                                         string blNo,
                                         string containerNo,
                                         string telexNo,
                                         string clearanceNo,
                                         string customerName,
                                         string agent,
                                         string consignee,
                                         string shipper,
                                         string notifyPart,
                                         string pol,
                                         string pod,
                                         string placeOfDelivery,
                                         string vesselName,
                                         string voyageNo,
                                         Guid? customerServiceID,
                                         Guid? filerID,
                                         Guid? salesID,
                                         OIOrderState? state,
                                         bool? isValid,
                                         int maxRecords,
                                         OIBusinessDateSearchType dateSearchType,
                                         DateTime? beginTime,
                                         DateTime? endTime,
                                         ReleaseBLSearchStatue releasestate,
                                         ReceiveRCSearchStatue receivercstate,
                                         ApplyRCSearchStatue applyrcstate,
                                         ReleaseRCSearchStatue releasercstate,
                                         URBSearchStatue urbstate,
                                         UDNSearchStatue udnstate,
                                         ARCSearchStatue arcstate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIBusinessList");

                string companyIDList = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDList);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@BLNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerNo);
                db.AddInParameter(dbCommand, "@TelexNo", DbType.String, telexNo);
                db.AddInParameter(dbCommand, "@ClearanceNo", DbType.String, clearanceNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@Agent", DbType.String, agent);
                db.AddInParameter(dbCommand, "@Consignee", DbType.String, consignee);
                db.AddInParameter(dbCommand, "@Shipper", DbType.String, shipper);
                db.AddInParameter(dbCommand, "@NotifyPart", DbType.String, notifyPart);
                db.AddInParameter(dbCommand, "@Pol", DbType.String, pol);
                db.AddInParameter(dbCommand, "@Pod", DbType.String, pod);
                db.AddInParameter(dbCommand, "@PlaceOfDelivery", DbType.String, placeOfDelivery);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@CustomerServiceID", DbType.Guid, customerServiceID);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, filerID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Byte, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@Releasestatue", DbType.Byte, releasestate);
                db.AddInParameter(dbCommand, "@ReceiveRCState", DbType.Byte, receivercstate);
                db.AddInParameter(dbCommand, "@ApplyRCState", DbType.Byte, applyrcstate);
                db.AddInParameter(dbCommand, "@ReleaseRCState", DbType.Byte, releasercstate);
                db.AddInParameter(dbCommand, "@URBState", DbType.Byte, urbstate);
                db.AddInParameter(dbCommand, "@UDNState", DbType.Byte, udnstate);
                db.AddInParameter(dbCommand, "@ARCState", DbType.Byte, arcstate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return GetBusinessList(ds);
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

        #region 转换数据
        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private List<OceanBusinessList> GetBusinessList(DataSet ds)
        {
            try
            {
                List<OceanBusinessList> list = (from b in ds.Tables[0].AsEnumerable()
                                                select new OceanBusinessList
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    No = b.Field<String>("NO"),
                                                    State = (OIOrderState)b.Field<Byte>("State"),
                                                    MBLID = b.Field<Guid?>("OIMBLID"),
                                                    MBLNo = b.Field<String>("MBLNO"),
                                                    SubNo = b.Field<String>("HBLNO"),
                                                    ContainerNo = b.Field<String>("ContainerNo"),
                                                    AgentName = b.Field<String>("AgentName"),
                                                    CustomerName = b.Field<String>("CustomerName"),
                                                    CustomerID = b.Field<Guid>("CustomerID"),
                                                    FETA = b.Field<DateTime?>("FETA"),
                                                    VesselVoyage = b.Field<String>("VesselVoyage"),
                                                    Paid = b.Field<bool?>("Paid"),
                                                    OBLRcved = b.Field<bool?>("OBLRcved"),
                                                    POLName = b.Field<String>("POLName"),
                                                    PODName = b.Field<String>("PODName"),
                                                    PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                                    DETA = b.Field<DateTime?>("DETA"),
                                                    ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                                    IsTelex = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Telex ? true : false,
                                                    FilerName = b.Field<String>("FilerName"),
                                                    POLFilerName = b.Field<String>("POLFilerName"),
                                                    CustomerServiceName = b.Field<String>("CustomerServiceName"),
                                                    OverSeasFilerName = b.Field<String>("OverSeasFilerName"),
                                                    ITNO = b.Field<String>("ITNO"),
                                                    FinalDestinationName = b.Field<String>("FinalDestinationName"),
                                                    ShipperName = b.Field<String>("ShipperName"),
                                                    ConsigneeName = b.Field<String>("ConsigneeName"),
                                                    Quantity = b.Field<Int32>("Quantity"),
                                                    QuantityUnitName = b.Field<String>("QuantityUnitName"),
                                                    Weight = b.Field<Decimal>("Weight"),
                                                    WeightUnitName = b.Field<String>("WeightUnitName"),
                                                    Measurement = b.Field<Decimal>("Measurement"),
                                                    MeasurementUnitName = b.Field<String>("MeasurementUnitName"),
                                                    SalesName = b.Field<String>("SalesName"),
                                                    CustomerNo = b.Field<String>("CustomerNo"),
                                                    ETD = b.Field<DateTime?>("ETD"),
                                                    ETA = b.Field<DateTime?>("ETA"),
                                                    CreateDate = b.Field<DateTime>("CreateDate"),
                                                    ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                                    ReleaseBLDate = b.Field<DateTime?>("ReleaseBLDate"),
                                                    IsRelease = b.Field<bool>("IsRelease"),
                                                    IsApplyRC = b.Field<bool>("IsApplyRC"),
                                                    IsNoticePay = b.Field<bool>("IsNoticePay"),
                                                    IsReceiveNotice = b.Field<bool>("IsReceiveNotice"),
                                                    IsReleaseCargo = b.Field<bool>("IsReleaseCargo"),
                                                    IsAgreeRC = b.Field<bool>("IsAgreeRC"),
                                                    IsNoticeRelease = b.Field<bool>("IsNoticeRelease"),
                                                    UpdateByName = b.Field<String>("UpdateByName"),
                                                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                    IsSentAN = b.Field<bool?>("IsSentAN"),
                                                    IsValid = b.Field<Boolean>("IsValid"),
                                                    IsPaid = b.Field<bool>("IsPaid"),
                                                    IsWriteOff = b.Field<bool>("IsWriteOff"),
                                                    AgentID = b.Field<Guid?>("AgentID"),
                                                    TelexNo = b.Field<string>("TelexNo"),
                                                }).ToList<OceanBusinessList>();

                return list;
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

        #region 获得业务数据的详细信息
        /// <summary>
        /// 获得业务详细信息--编辑界面使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OceanBusinessInfo GetBusinessInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIBusinessInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanBusinessInfo result = (from b in ds.Tables[0].AsEnumerable()
                                            select new OceanBusinessInfo
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                No = b.Field<String>("RefNo"),
                                                BookingMode = (FCMBookingMode)b.Field<Byte>("BookingMode"),
                                                BookingDate = b.Field<DateTime>("BookingDate"),
                                                TradeTermID = b.Field<Guid>("TradeTermID"),
                                                GetBookingDate = b.Field<DateTime?>("GetBookingDate"),
                                                AgentNo = b.Field<String>("AgentRefNo"),
                                                OIOperationType = (FCMOperationType)b.Field<Byte>("OIOperationType"),
                                                TradeTermName = b.Field<String>("TradeTermName"),
                                                CompanyID = b.Field<Guid>("CompanyID"),
                                                CompanyName = b.Field<String>("CompanyName"),
                                                SalesID = b.Field<Guid?>("SalesID"),
                                                SalesDepartmentID = b.Field<Guid>("SalesDepartmentID"),
                                                SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                                SalesTypeName = b.Field<String>("SalesTypeName"),
                                                SalesDepartmentName = b.Field<String>("SalesDepartmentName"),
                                                FilerId = b.Field<Guid?>("FilerID"),
                                                FilerName = b.Field<String>("FilerName"),
                                                CustomerID = b.Field<Guid>("CustomerID"),
                                                ShipperID = b.Field<Guid?>("ShipperID"),
                                                ShipperName = b.Field<String>("ShipperName"),
                                                ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                                ConsigneeName = b.Field<String>("ConsigneeName"),
                                                ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                                NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                                NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                                NotifyPartyName = b.Field<String>("NotifyPartyName"),
                                                //IsTelex = b.Field<bool?>("IsTelex"),
                                                CustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomerDescription")),
                                                PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                                POLID = b.Field<Guid>("POLID"),
                                                PODID = b.Field<Guid>("PODID"),
                                                PlaceOfDeliveryID = b.Field<Guid?>("PlaceOfDeliveryID"),
                                                ContainerNo = b.Field<String>("ContainerNo"),
                                                TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                                TransportClauseName = b.Field<String>("TransportClauseName"),
                                                PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                                PaymentTermName = b.Field<String>("PaymentTermName"),
                                                Commodity = b.Field<String>("Commodity"),
                                                Quantity = b.Field<Int32>("Quantity"),
                                                QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                                                QuantityUnitName = b.Field<String>("QuantityUnitName"),
                                                Weight = b.Field<Decimal>("Weight"),
                                                WeightUnitID = b.Field<Guid>("WeightUnitID"),
                                                WeightUnitName = b.Field<String>("WeightUnitName"),
                                                Measurement = b.Field<Decimal>("Measurement"),
                                                MeasurementUnitID = b.Field<Guid>("MeasurementUnitID"),
                                                MeasurementUnitName = b.Field<String>("MeasurementUnitName"),
                                                CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")),
                                                ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                                IsTruck = b.Field<bool>("IsTruck"),
                                                IsCustoms = b.Field<bool>("IsCustoms"),
                                                IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                                IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                                IsWareHouse = b.Field<bool>("IsWarehouse"),
                                                IsReleaseNotify = b.Field<bool>("IsReleaseNotify"),
                                                IsTransport = b.Field<bool>("IsTransport"),
                                                IsClearance = b.Field<bool>("IsClearance"),
                                                ClearanceDate = b.Field<DateTime?>("ClearanceDate"),
                                                ClearanceNo = b.Field<String>("ClearanceNo"),
                                                ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                                DETA = b.Field<DateTime?>("DETA"),
                                                FETA = b.Field<DateTime?>("FETA"),
                                                ETD = b.Field<DateTime?>("ETD"),
                                                ETA = b.Field<DateTime?>("ETA"),
                                                WareHouseID = b.Field<Guid?>("WareHouseID"),
                                                WareHouseName = b.Field<String>("WareHouseName"),
                                                CustomsID = b.Field<Guid?>("CustomsID"),
                                                CustomsName = b.Field<String>("CustomsBrokerName"),
                                                CustomsDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomsDescription")),
                                                //ReleaseType = (ReleaseType)b.Field<Byte>("ReleaseType"),
                                                IsTelex = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Telex ? true : false,
                                                Remark = b.Field<String>("Remark"),
                                                State = (OIOrderState)b.Field<Byte>("State"),
                                                CreateID = b.Field<Guid>("CreateByID"),
                                                CreateByName = b.Field<String>("CreateByName"),
                                                CustomerNo = b.Field<String>("CustomerRefNo"),
                                                CustomerName = b.Field<String>("CustomerName"),
                                                PlaceOfReceiptName = b.Field<String>("PlaceOfReceiptName"),
                                                POLName = b.Field<String>("POLName"),
                                                PODName = b.Field<String>("PODName"),
                                                PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                                FinalDestinationName = b.Field<String>("FinalDestinationName"),
                                                SalesName = b.Field<String>("SalesName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                AgentID = b.Field<Guid>("AgentID"),
                                                AgentName = b.Field<String>("AgentName"),
                                                AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                                OverSeasFilerId = b.Field<Guid?>("OverSeasFilerId"),
                                                FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                                OverSeasFilerName = b.Field<String>("OverSeasFilerName"),
                                                MBLID = b.Field<Guid?>("OIMBLID"),
                                                CustomerService = b.Field<Guid?>("CustomerServiceID"),
                                                CustomerServiceName = b.Field<String>("CustomerServiceName"),
                                                LocalCSName = b.Field<String>("LocalCSName"),
                                                LocalCSId = b.Field<Guid?>("LocalCSID"),
                                                POLFilerName = b.Field<String>("POLFilerName"),
                                                POLFilerID = b.Field<Guid?>("POLFilerID"),
                                                IsSentAN = b.Field<bool?>("IsSentAN"),
                                                IsNoticeRelease = b.Field<bool>("IsNoticeRelease"),
                                                GoodDescription = b.Field<string>("GoodDescription"),
                                                TelexNo = b.Field<string>("TelexNo"),
                                                IsApplyRC = b.Field<bool>("BLRCA"),
                                                IsAgreeRC = b.Field<bool>("AgreeRC"),
                                                IsOMBLRcved = b.Field<bool>("IsOMBLRcved"),
                                                IsMailDMBL = b.Field<bool>("IsMailDMBL"),
                                                IsReceiveNotice = b.Field<bool>("RBLRcv"),
                                                IsReleaseCargo = b.Field<bool>("BLRC"),
                                                IsACCLOS = b.Field<bool>("ACCLOS"),
                                                FreightIncludedIds = b.Field<string>("FreightIncludedIds"),
                                                FreightIncludedCodes = b.Field<string>("FreightIncludedCodes")
                                            }).SingleOrDefault();
                result.MBLInfo = ConvertDataRowToOceanBusinessMBLList(ds.Tables[1]);
                result.ContainerList = ConvertTableToOIBusinessContainerList(ds.Tables[2]);
                result.HBLList = ConvertTableToOceanBusinessHBLList(ds.Tables[3]);
                result.FeeList = ConvertTableToOceanImportFeeList(ds.Tables[4]);
                result.MBLNo = result.MBLInfo.MBLNo;
                result.ReleaseType = result.MBLInfo.ReleaseType;
                foreach (var r in result.HBLList)
                {
                    if (string.IsNullOrEmpty(result.SubNo))
                    {
                        result.SubNo = r.HBLNo;
                    }
                    else
                    {
                        result.SubNo = result.SubNo + "," + r.HBLNo;
                    }

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
        /// 根据ID获得业务编辑界面的详细数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OceanBusinessInfo GetBusinessInfoByEdit(Guid id)
        {

            OceanBusinessInfo businessInfo = GetBusinessInfo(id);
            return businessInfo;
        }

        #endregion

        #region  下载
        /// <summary>
        /// 获得下载清单列表
        /// </summary>
        /// <param name="bLNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次 </param>
        /// <param name="eta">离港日</param>
        /// <param name="etd">到港日</param>
        /// <param name="polName">装货港</param>
        /// <param name="podName">卸货港</param>
        /// <param name="placeOfDeliveryName">交货地</param>
        /// <param name="consigneeName">收货人</param>
        /// <param name="podCompanyID">代理公司(港前)</param>
        /// <param name="companyID">操作公司(港后)</param>
        /// <param name="state">状态</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        public List<OceanBusinessDownLoadList> GetOceanExportList(
                      string bLNo,
                      string containerNo,
                      string vesselName,
                      string voyageNo,
                      DateType? dateType,
                      DateTime? beginDate,
                      DateTime? endDate,
                      string polName,
                      string podName,
                      string placeOfDelivery,
                      string consigneeName,
                      Guid? polCompanyID,
                      string companyIDs,
                      DocumentState? docState,
                      OEBLState? state,
                      bool? isValid,
                      ReleaseCarogoState? isreleaseBL,
                      int maxRecords)
        {
            try
            {
                Stopwatch stopwatchSearch = Stopwatch.StartNew();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIDownLoadOceanExportList");

                db.AddInParameter(dbCommand, "@BLNo", DbType.String, bLNo);
                db.AddInParameter(dbCommand, "@IsReleaseBL", DbType.Byte, isreleaseBL);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerNo);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@DataType", DbType.Byte, dateType);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@PolName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@PodName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, placeOfDelivery);
                db.AddInParameter(dbCommand, "@ConsigneeName", DbType.String, consigneeName);
                db.AddInParameter(dbCommand, "@POLCompanyIDS", DbType.Guid, polCompanyID);    //代理公司 (港前)
                db.AddInParameter(dbCommand, "@PODCompanyIDs", DbType.String, companyIDs);    //操作口岸 (港后) 
                db.AddInParameter(dbCommand, "@DispatchState", DbType.Byte, docState);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBusinessDownLoadList> results = (from b in ds.Tables[0].AsEnumerable()
                                                           select new OceanBusinessDownLoadList
                                                           {
                                                               ID = b.Field<Guid>("ID"),
                                                               No = b.Field<string>("POLOperationNo"),
                                                               PODRefNo = b.Field<String>("RefNo"),
                                                               RefID = b.IsNull("RefID") ? Guid.Empty : b.Field<Guid>("RefID"),
                                                               DownLoadState = (DownLoadState)b.Field<Byte>("State"),
                                                               DownLoadDate = b.Field<DateTime?>("DownDate"),
                                                               DocumentState =
                                                               b.IsNull("DispatchState") ? DocumentState.Pending : (DocumentState)b.Field<byte>("DispatchState"),
                                                               IsAgainDispatch = b.IsNull("IsAgainDispatch") ? false : b.Field<Boolean>("IsAgainDispatch"),
                                                               AssignToName = b.Field<String>("AssignToName"),
                                                               OceanAgentDispatchID =
                                                               b.IsNull("OceanAgentDispatchID") ? Guid.Empty : b.Field<Guid>("OceanAgentDispatchID"),
                                                               OceanBookingID = b.IsNull("OceanBookingID") ? Guid.Empty : b.Field<Guid>("OceanBookingID"),
                                                               HBLState = (OEBLState)b.Field<Byte>("HBLState"),
                                                               MBLNo = b.Field<String>("MBLNo"),
                                                               HBLNo = b.Field<String>("HBLNos"),
                                                               HBLIDs = b.Field<String>("HBLIDS"),
                                                               ContainerNos = b.Field<String>("ContainerNos"),
                                                               VesselVoyage = b.Field<String>("VesselVoyage"),
                                                               POLName = b.Field<String>("POLName"),
                                                               ETD = b.Field<DateTime?>("ETD"),
                                                               PODName = b.Field<String>("PODName"),
                                                               ETA = b.Field<DateTime?>("ETA"),
                                                               PlaceOfDeliveryNames = b.Field<String>("PlaceOfDeliveryNames"),
                                                               PlaceofdeliveryDates = b.Field<DateTime?>("PlaceofdeliveryDates"),
                                                               ConsigneeID = b.Field<Guid>("ConsigneeID"),
                                                               ConsigneeName = b.Field<String>("ConsigneeName"),
                                                               POLFilerName = b.Field<String>("POLFiler"),
                                                               AgentName = b.Field<String>("POLCompanyName"),  //代理公司 (港前)
                                                               CompanyID = b.Field<Guid?>("POLCompanyID"),     //操作口岸 (港前)
                                                               AgentID = b.Field<Guid?>("POLAgentID"),//港后代理ID
                                                               IsReleaseBL = b.Field<bool>("IsReleaseBL"),
                                                               //  CreateDate = b.Field<DateTime>("CreateDate"),
                                                           }).ToList();
                _OperationLogService.Add(DateTime.Now, "GET-OILIST-DB", string.Format("获得下载列表"), stopwatchSearch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

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
        /// 下载业务单
        /// </summary>
        /// <param name="mblIDs">MBLID集合</param>
        /// <param name="consigneeIDs">收货人集合</param>
        /// <param name="hblIDs">HBLID集合</param>
        /// <param name="saveByID">保存人</param>
        /// <returns></returns>
        public OIAfterDownLoadRerurnData DownLoadBusiness(
                    Guid[] mblIDs,
                    Guid[] consigneeIDs,
                    DateTime?[] detas,
                    string[] hblIDs,
                    Guid saveByID,
                    Guid? agentID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspOIDownLoadBusiness");

                string mblIDList = mblIDs.Join();
                string consigneeIDList = consigneeIDs.Join();
                string detaList = detas.Join();
                string hblIDList = hblIDs.Join();

                db.AddInParameter(dbCommand, "@OEMBLIDs", DbType.String, mblIDList);
                db.AddInParameter(dbCommand, "@ConsigneeIDs", DbType.String, consigneeIDList);
                db.AddInParameter(dbCommand, "@DETAs", DbType.String, detaList);
                db.AddInParameter(dbCommand, "@OEHBLIDS", DbType.String, hblIDList);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OIAfterDownLoadRerurnData data = new OIAfterDownLoadRerurnData();
                List<string> podRefNoList = (from b in ds.Tables[0].AsEnumerable()
                                             select b.Field<String>("No")).ToList();

                data.PODRefNoList = podRefNoList;

                List<Guid> ids = (from b in ds.Tables[0].AsEnumerable()
                                  select new Guid(b.Field<Guid>("OIBookingID").ToString()))
                                                           .ToList();

                data.PODRefIdList = ids;
                //List<DocumentInfo> documentInfos = (from document in ds.Tables[1].AsEnumerable()
                //                             select new DocumentInfo
                //                             {
                //                                 CreateBy = document.Field<Guid>("CreateByID"),
                //                                 CreateByName = document.Field<String>("CreateByName"),
                //                                 // CreateDate = document.Field<DateTime>("CreateDate"),
                //                                 DocumentType = (ICP.DataCache.ServiceInterface.DocumentType)document.Field<byte>("DocumentType"),
                //                                 FormType = (ICP.Framework.CommonLibrary.Common.FormType)document.Field<byte>("FormType"),
                //                                 Id = document.Field<Guid>("Id"),
                //                                 Name = document.Field<String>("Name"),
                //                                 OperationID = document.Field<Guid>("OperationID"),
                //                                 Remark = document.Field<String>("Remark"),
                //                                 State = UploadState.Successed,
                //                                 DocumentState=DocumentState.Pending,
                //                                 Type = (OperationType)document.Field<byte>("OperationType"),
                //                                 UpdateDate = document.Field<DateTime?>("UpdateDate"),
                //                                 DocumentTypeName = document.Field<String>("DocumentTypeName"),
                //                                 // UpdateBy = document.Field<Guid?>("UpdateBy"),
                //                                 CreateDate = document.Field<DateTime>("CreateDate"),
                //                             }).ToList();

                data.BusinessList = GetBusinessListByIds(ids.ToArray());
                //string methodName = "Upload";
                //FireEvent(methodName, NotifyType.Download, documentInfos.ToList(), null);
                //return GetBusinessListByIds(ids.ToArray());

                return data;
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
        /// 通过出口分文件产生进口业务
        /// </summary>
        /// <param name="mblIDs">MBLID集合</param>
        /// <param name="consigneeIDs">收货人集合</param>
        /// <param name="consigneeIDs">DETA集合</param>
        /// <param name="hblIDs">HBLID集合</param>
        /// <param name="saveByID">保存人</param>
        /// <returns></returns>
        public OIAfterDownLoadRerurnData DownLoadBusinessFromDispatchFile(
                    Guid[] mblIDs,
                    Guid[] consigneeIDs,
                    DateTime?[] detas,
                    string[] hblIDs,
                    Guid saveByID,
                    Guid? agentID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspBuilderOIJobFromDispatchFile");

                string mblIDList = mblIDs.Join();
                string consigneeIDList = consigneeIDs.Join();
                string detaList = detas.Join();
                string hblIDList = hblIDs.Join();

                db.AddInParameter(dbCommand, "@OEMBLIDs", DbType.String, mblIDList);
                db.AddInParameter(dbCommand, "@ConsigneeIDs", DbType.String, consigneeIDList);
                db.AddInParameter(dbCommand, "@DETAs", DbType.String, detaList);
                db.AddInParameter(dbCommand, "@OEHBLIDS", DbType.String, hblIDList);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OIAfterDownLoadRerurnData data = new OIAfterDownLoadRerurnData();
                List<string> podRefNoList = (from b in ds.Tables[0].AsEnumerable()
                                             select b.Field<String>("No")).ToList();

                data.PODRefNoList = podRefNoList;

                List<Guid> ids = (from b in ds.Tables[0].AsEnumerable()
                                  select new Guid(b.Field<Guid>("OIBookingID").ToString()))
                                                           .ToList();

                data.PODRefIdList = ids;


                data.BusinessList = GetBusinessListByIds(ids.ToArray());

                return data;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (TimeoutException timeExceptiom)
            {
                _OperationLogService.Add(DateTime.Now, "SAVE-DOWNLOAD-DB", string.Format("下载业务 MBL[{0}] 超时", mblIDs.ToString()), "0");
                throw new ApplicationException(timeExceptiom.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 取消业务

        /// <summary>
        /// 取消业务单
        /// </summary>
        /// <param name="businessID">业务单ID</param>
        /// <param name="isCancel">是否取消(True为取消;Flase为激活)</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns></returns>
        public SingleResult CancelOceanBusiness(
                    Guid businessID,
                    bool isCancel,
                    Guid changeByID,
                    DateTime? updateDate)
        {
            return new SingleResult();
        }


        #endregion

        #region 业务转移
        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="BusinessID">业务ID</param>
        /// <param name="newCompanyID">新公司</param>
        /// <param name="changeByID">操作人</param>
        /// <param name="updateDate">更新时间</param>
        /// <returns></returns>

        public SingleResult TransferBusiness(
                     Guid businessID,
                     Guid newCompanyID,
                     Guid changeByID,
                     DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspOITransferBusiness");

                db.AddInParameter(dbCommand, "@BusinessID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@NewCompanyID", DbType.Guid, newCompanyID);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                //放单的通知联系人，此业务.海出业务 .客服，此业务.海出业务.所有海进业务.文件和客服
                MailOIBusinessDataObjects mailInfo = GetTranserEmailInfoByID(businessID);
                mailInfo.OIBookingID = businessID;
                SendEmail(mailInfo);
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
        /// 根据业务ID获取业务转移所需的邮件信息
        /// </summary>
        /// <param name="OIBookingID"></param>
        /// <returns></returns>
        public MailOIBusinessDataObjects GetTranserEmailInfoByID(Guid OIBookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTranserEmailInfoByID");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                MailOIBusinessDataObjects results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new MailOIBusinessDataObjects
                                                      {
                                                          OperationNo = b.Field<string>("OperationNo"),
                                                          ReleaseEmail = b.Field<string>("ReleaseEmail"),
                                                          POLFiler = b.Field<string>("POLFiler"),
                                                          AllFiler = b.Field<string>("AllFiler"),
                                                          BLNos = b.Field<string>("BLNos"),
                                                          Agent = b.Field<string>("Agent")
                                                      }).FirstOrDefault();

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

        //  6. 发送邮件
        //TO:  放单的通知联系人，此业务.海出业务 .客服，此业务.海出业务.所有海进业务.文件和客服
        //SUBJECT: This import shipments (%业务.海出业务.所有海进业务.提单号) has been transited to another agent (目标代理)
        //CONTEXT: 
        //Hi All, 
        //This import shipments (%业务.海出业务.所有海进业务.提单号) have been transited to another agent (%目标代理).
        //And the export shipment (%(%业务.海出业务.业务号) is also adjusted for the filled agent of D/C.

        /// <summary>
        /// 发送Email,邮件不能关联多个业务。事件暂时由数据库生成。BY LL
        /// </summary>
        /// <param name="sendTo">接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        public void SendEmail(MailOIBusinessDataObjects mailInfo)
        {
            try
            {
                if (mailInfo == null) return;
                UserInfo userInfo = new UserInfo();
                userInfo = _UserService.GetUserInfo(ApplicationContext.Current.UserId);
                ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
                message.CreateBy = ApplicationContext.Current.UserId;
                message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                message.HasAttachment = false;
                message.SendFrom = userInfo.EMail;
                message.SendTo = mailInfo.ReleaseEmail + ";" + mailInfo.POLFiler + ";" + mailInfo.AllFiler;
                message.CC = userInfo.EMail;
                message.Subject = "This import shipments [ " + mailInfo.BLNos + " ] has been transited to another agent [ " + mailInfo.Agent + " ]";
                message.Body = " Hi All,\r\n This import shipments [ " + mailInfo.BLNos + " ] have been transited to another agent [ " + mailInfo.Agent + " ].\r\n And the export shipment [ " + mailInfo.OperationNo + " ] is also adjusted for the filled agent of D/C.";
                message.Type = MessageType.Email;
                message.BodyFormat = BodyFormat.olFormatPlain;
                message.UserProperties = new Message.ServiceInterface.MessageUserPropertiesObject
                {
                    OperationType = OperationType.OceanImport,
                    OperationId = mailInfo.OIBookingID,
                };
                message.State = MessageState.Success;

                _MessageService.Send(message);
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
        }

        #endregion

        #region 提货通知书

        /// <summary>
        /// 获取派车信息
        /// </summary>
        /// <param name="businessID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        public List<OceanImportTruckInfo> GetOceanTruckServiceList(Guid businessID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOITruckList");

                db.AddInParameter(dbCommand, "@BusinessID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanImportTruckInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new OceanImportTruckInfo
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          OIBookingID = b.Field<Guid>("OIBookingID"),
                                                          TruckerID = b.Field<Guid>("TruckerID"),
                                                          TruckerName = b.Field<String>("TruckerName"),
                                                          TruckerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("TruckerDescription")),
                                                          PickUpAtID = b.Field<Guid>("PickUpAtID"),
                                                          PickUpAtName = b.Field<String>("PickUpAtName"),
                                                          PickUpAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("PickUpAtDescription")),
                                                          PickUpDate = b.Field<DateTime?>("LoadingTime"),
                                                          DeliveryAtID = b.Field<Guid>("DeliveryAtID"),
                                                          DeliveryAtName = b.Field<String>("DeliveryAtName"),
                                                          DeliveryAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("DeliveryAtDescription")),
                                                          DeliveryDate = b.Field<DateTime?>("DelivertyDate"),
                                                          BillToID = b.Field<Guid>("BillToID"),
                                                          BillToName = b.Field<String>("BillToName"),
                                                          BillToDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("BillToDescription")),
                                                          IsDrivingLicence = b.Field<Boolean>("IsDrivingLicence"),
                                                          NO = b.Field<String>("NO"),
                                                          Remark = b.Field<String>("Remark"),
                                                          Commodity = b.Field<String>("Commodity"),
                                                          CreateByName = b.Field<String>("CreateByName"),
                                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                          ContainerIDList = (from c in ds.Tables[1].AsEnumerable()
                                                                             where c.Field<Guid>("OITruckServiceID") == b.Field<Guid>("ID")
                                                                             select c.Field<Guid>("OIContainerID")).ToList(),

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

        /// <summary>
        /// 保存派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="businessID">业务ID</param>
        /// <param name="no">派车单号</param>
        /// <param name="shippingOrderNo">订舱号</param>
        /// <param name="truckerID">拖车公司ID</param>
        /// <param name="truckerDescription">拖车公司描述</param>
        /// <param name="loadingTime">装货时间</param>
        /// <param name="pickUpAtID">装货地</param>
        /// <param name="packUpAtDescription">装货地描述</param>
        /// <param name="deliveryAtID">还柜地</param>
        /// <param name="deliveryAtDescription">还柜地描述</param>
        /// <param name="truckTime">派车时间</param>
        /// <param name="customsBrokerID">报关行ID</param>
        /// <param name="cusomersDescription">报关行描述</param>
        /// <param name="isDrivingLicence">是否需要司机本</param>
        /// <param name="freigtDescription">费用描述</param>
        /// <param name="remark">备注</param>
        /// <param name="containerDescription">箱需求</param>
        /// <param name="saveByID">创建人</param>
        /// <param name="containerIdList">关联的集装箱ID</param>
        /// <returns></returns>
        public SingleResult SaveOceanTruckServiceInfo(TruckSaveRequest saveRequest)
        {
            try
            {
                string truckerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.TruckerDescription, true, false);
                string pickUpAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.PickUpAtDescription, true, false);
                string deliveryAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.DeliveryAtDescription, true, false);
                string billToDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.BillToDescription, true, false);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOITruckInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, saveRequest.OIBookingID);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.NO);
                db.AddInParameter(dbCommand, "@TruckerID", DbType.Guid, saveRequest.TruckerID);
                db.AddInParameter(dbCommand, "@TruckerDescription", DbType.Xml, truckerDescription);

                db.AddInParameter(dbCommand, "@PickUpDate", DbType.DateTime, saveRequest.PickUpDate);
                db.AddInParameter(dbCommand, "@PickUpAtID", DbType.Guid, saveRequest.PickUpAtID);
                db.AddInParameter(dbCommand, "@PickUpAtDescription", DbType.Xml, pickUpAtDescription);

                db.AddInParameter(dbCommand, "@DeliveryAtID", DbType.Guid, saveRequest.DeliveryAtID);
                db.AddInParameter(dbCommand, "@DeliveryAtDescription", DbType.Xml, deliveryAtDescription);

                db.AddInParameter(dbCommand, "@DeliveryDate", DbType.DateTime, saveRequest.DeliveryDate);
                db.AddInParameter(dbCommand, "@IsDrivingLicence", DbType.Boolean, false);
                db.AddInParameter(dbCommand, "@BillToID", DbType.Guid, saveRequest.BillToID);
                db.AddInParameter(dbCommand, "@BillToDescription", DbType.Xml, billToDescription);

                db.AddInParameter(dbCommand, "@PickUpSendDate", DbType.DateTime, saveRequest.PickUpSendDate);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.Commodity);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "CreateDate" });


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
        /// 删除派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveOceanTruckServiceInfo(
                     Guid id,
                     Guid removeByID,
                     DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOITruckInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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
        /// 获取指定客户的最近提货通知书下的拖车行和交货地
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public SingleResult GetRecentlyOITruckInfoList(Guid companyID, Guid customerID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetRecentlyOITruckInfoList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                //DataSet ds = null;
                //ds = db.ExecuteDataSet(dbCommand);
                //if (ds == null || ds.Tables.Count < 1)
                //{
                //    return null;
                //}

                //OceanImportTruckInfo result = (from b in ds.Tables[0].AsEnumerable()
                //                               select new OceanImportTruckInfo
                //                            {
                //                                TruckerID = b.Field<Guid>("TruckerID"),
                //                                TruckerName = b.Field<String>("TruckerName"),
                //                                DeliveryAtID = b.Field<Guid>("DeliveryAtID"),
                //                                DeliveryAtName = b.Field<String>("DeliveryAtName"),
                //                            }).SingleOrDefault();

                SingleResult result = db.SingleResult(dbCommand, new string[] { "TruckerID", "TruckerName", "DeliveryAtID", "DeliveryAtName" });

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

        #region 保存业务
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">业务号</param>
        /// <param name="companyid">操作口岸</param>
        /// <param name="oIOperationType">业务类型</param>
        /// <param name="customerID">客户</param>
        /// <param name="customerDesciption">客户描述</param>
        /// <param name="customerNo">客户参考号</param>
        /// <param name="tradetermID">贸易条款</param>
        /// <param name="salestypeID">揽货类型</param>
        /// <param name="salesID">揽货人</param>
        /// <param name="sealsDepartmentID">揽货部门</param>
        /// <param name="transportclauseID">运输条款</param>
        /// <param name="paymenttermID">付款方式</param>
        /// <param name="portAgoCustomerService">港前客服</param>
        /// <param name="overSeasFilerId">海外部客服</param>
        /// <param name="customerServiceID">客服</param>
        /// <param name="bookingmode">订舱类型</param>
        /// <param name="agentid">代理</param>
        /// <param name="agentDescription">代理描述</param>
        /// <param name="agentNo">代理参考号</param>
        /// <param name="shipperID">发货人</param>
        /// <param name="shipperDescription">发货人描述</param>
        /// <param name="consigneeID">收货人</param>
        /// <param name="consigneeDescription">收货人描述</param>
        /// <param name="notifyPartyID">通知人</param>
        /// <param name="notifyPartyDescription">通知人描述</param>
        /// <param name="placeOfReceiptID">收货地</param>
        /// <param name="polID">装货港</param>
        /// <param name="etd">ETD</param>
        /// <param name="podID">卸货港</param>
        /// <param name="eta">ETA</param>
        /// <param name="placeOfDeliveryID">交货地</param>
        /// <param name="deta">D.ETA</param>
        /// <param name="finalDestinationID">最终目的地</param>
        /// <param name="feta">F>ETA</param>
        /// <param name="commodity">品名</param>
        /// <param name="quantity">数量</param>
        /// <param name="quantityunitID">数量单位</param>
        /// <param name="weight">重量</param>
        /// <param name="weightUnitID">重量单位</param>
        /// <param name="measurement">体积</param>
        /// <param name="measurementUnitID">体积单位</param>
        /// <param name="isWareHouse">仓储</param>
        /// <param name="isCustoms">报关</param>
        /// <param name="isTruck">拖车</param>
        /// <param name="isCommodityInspection">商检</param>
        /// <param name="isQuarantineInspection">质检</param>
        /// <param name="isTelex">电放</param>
        /// <param name="isReleaseNotify">需要放货通知书</param>
        /// <param name="isTransport">转运</param>
        /// <param name="wareHouseID">仓库ID</param>
        /// <param name="wareHouseDescription">仓库描述</param>
        /// <param name="customsID">报关行ID</param>
        /// <param name="customsDescription">报关行描述</param>
        /// <param name="isClearance">清关</param>
        /// <param name="clearanceDate">清关日期</param>
        /// <param name="remark">备注</param>
        /// <param name="mblID">MBLID</param>
        /// <param name="saveByID">保存人</param>
        /// <returns></returns>
        public SingleResult SaveOIBusinessInfo(BusinessSaveRequest saveRequest)
        {
            try
            {
                SingleResult result = null;
               

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIBusinessInfo");

                #region Parameter

                string customerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.CustomerDescription, true, false);
                string shipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ShipperDescription, true, false);
                string consigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ConsigneeDescription, true, false);
                string agentDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.AgentDescription, true, false);
                string notifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.NotifyPartyDescription, true, false);
                string wareHouseDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.WareHouseDescription, true, false);
                string customsDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.CustomsDescription, true, false);

                string containerDescription = (saveRequest.ContainerDescription == null || saveRequest.ContainerDescription.Containers.Count == 0) 
                    ? null 
                    : SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.ContainerDescription, true, false);
                string cargoDescription = SerializerHelper.SerializeToString<CargoDescription>(saveRequest.CargoDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.No);
                db.AddInParameter(dbCommand, "@Companyid", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, saveRequest.OperationType);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.CustomerID);
                db.AddInParameter(dbCommand, "@CustomerDescription", DbType.Xml, customerDescription);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, saveRequest.CustomerNo);
                db.AddInParameter(dbCommand, "@TradetermID", DbType.Guid, saveRequest.TradeTermID);
                db.AddInParameter(dbCommand, "@SalestypeID", DbType.Guid, saveRequest.SalesTypeID);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, saveRequest.SalesID);
                db.AddInParameter(dbCommand, "@SalesDepartmentID", DbType.Guid, saveRequest.SalesDepartmentID);
                db.AddInParameter(dbCommand, "@TransportclauseID", DbType.Guid, saveRequest.TransportClauseID);
                db.AddInParameter(dbCommand, "@PaymenttermID", DbType.Guid, saveRequest.PaymentTermID);
                db.AddInParameter(dbCommand, "@POLFilerID", DbType.Guid, saveRequest.POLFilerID);
                db.AddInParameter(dbCommand, "@POLFilerName", DbType.String, saveRequest.POLFilerName);
                db.AddInParameter(dbCommand, "@OverSeasFilerId", DbType.Guid, saveRequest.OverSeasFilerId);
                db.AddInParameter(dbCommand, "@CustomerServiceID", DbType.Guid, saveRequest.CustomerServiceID);
                db.AddInParameter(dbCommand, "@LocalCSID", DbType.Guid, saveRequest.LocalCSID);
                db.AddInParameter(dbCommand, "@FilerID", DbType.Guid, saveRequest.FilerId);
                db.AddInParameter(dbCommand, "@Bookingmode", DbType.Byte, saveRequest.BookingMode);
                db.AddInParameter(dbCommand, "@Agentid", DbType.Guid, saveRequest.AgentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, agentDescription);
                db.AddInParameter(dbCommand, "@AgentRefNo", DbType.String, saveRequest.AgentNo);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, saveRequest.ShipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, shipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, saveRequest.ConsigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, consigneeDescription);
                db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, saveRequest.NotifyPartyID);
                db.AddInParameter(dbCommand, "@NotifyPartyDescription", DbType.Xml, notifyPartyDescription);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, saveRequest.PlaceOfReceiptID);
                db.AddInParameter(dbCommand, "@PolID", DbType.Guid, saveRequest.POLID);
                db.AddInParameter(dbCommand, "@PodID", DbType.Guid, saveRequest.PODID);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, saveRequest.PlaceOfDeliveryID);
                db.AddInParameter(dbCommand, "@Deta", DbType.DateTime, saveRequest.DETA);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.FinalDestinationID);
                db.AddInParameter(dbCommand, "@Feta", DbType.DateTime, saveRequest.FETA);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.Commodity);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, saveRequest.Quantity);
                db.AddInParameter(dbCommand, "@QuantityunitID", DbType.Guid, saveRequest.QuantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.String, saveRequest.Weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, saveRequest.WeightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.String, saveRequest.Measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, saveRequest.MeasurementUnitID);
                db.AddInParameter(dbCommand, "@IsWareHouse", DbType.Boolean, saveRequest.IsWareHouse);
                db.AddInParameter(dbCommand, "@IsCustoms", DbType.Boolean, saveRequest.IsCustoms);
                db.AddInParameter(dbCommand, "@IsTruck", DbType.Boolean, saveRequest.IsTruck);
                db.AddInParameter(dbCommand, "@IsCommodityInspection", DbType.Boolean, saveRequest.IsCommodityInspection);
                db.AddInParameter(dbCommand, "@IsQuarantineInspection", DbType.Boolean, saveRequest.IsQuarantineInspection);
                db.AddInParameter(dbCommand, "@IsReleaseOrderRequired", DbType.Boolean, saveRequest.IsReleaseNotify);
                db.AddInParameter(dbCommand, "@IsDoorMove", DbType.Boolean, saveRequest.IsTransport);
                db.AddInParameter(dbCommand, "@WareHouseID", DbType.Guid, saveRequest.WareHouseID);
                db.AddInParameter(dbCommand, "@WareHouseDescription", DbType.String, wareHouseDescription);
                db.AddInParameter(dbCommand, "@CustomsBrokerID", DbType.Guid, saveRequest.CustomsID);
                db.AddInParameter(dbCommand, "@CustomsBrokerDescription", DbType.Xml, customsDescription);
                db.AddInParameter(dbCommand, "@IsClearance", DbType.Boolean, saveRequest.IsClearance);
                db.AddInParameter(dbCommand, "@ClearanceDate", DbType.DateTime, saveRequest.ClearanceDate);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Byte, saveRequest.ReleaseType);
                db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, saveRequest.ReleaseDate);
                db.AddInParameter(dbCommand, "@BookingDate", DbType.DateTime, saveRequest.BookingDate);
                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, saveRequest.MBLID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);
                db.AddInParameter(dbCommand, "@CargoDescription", DbType.Xml, cargoDescription);
                db.AddInParameter(dbCommand, "@GoodDescription", DbType.String, saveRequest.GoodDescription);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, saveRequest.CarrierID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.Updatedate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);

                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);
                db.AddInParameter(dbCommand, "@ClearanceNo", DbType.String, saveRequest.ClearanceNo);
                db.AddInParameter(dbCommand, "@FreightIncluded", DbType.String, saveRequest.FreightIncludedIds);
                #endregion

#if DEBUG
                result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "State", "ANSC" });
                if (saveRequest != null)
                {
                    SaveRequestShipmentInfoForCSP sqShipmentInfo = new SaveRequestShipmentInfoForCSP()
                    {
                        OperationID = result.GetValue<Guid>("ID"),
                        OperationType = OperationType.OceanImport,
                        SaveBy = saveRequest.saveByID,
                        UpdateDate = DateTime.Now,
                    };
                    _FCMCommonService.SaveShipmentInfoForCSP(sqShipmentInfo);
                }
#endif
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
        private List<OceanBusinessHBLList> ConvertTableToOceanBusinessHBLList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<OceanBusinessHBLList>();
            }
            List<OceanBusinessHBLList> results = (from b in dt.AsEnumerable()
                                                  select new OceanBusinessHBLList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      OIBookingID = b.Field<Guid>("OIBookingID"),
                                                      HBLNo = b.Field<String>("No"),
                                                      ShipperID = b.Field<Guid?>("ShipperID"),
                                                      ShipperName = b.Field<String>("ShipperName"),
                                                      ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                      AMSNo = b.Field<String>("AMSNo"),
                                                      ISFNo = b.Field<String>("ISFNo"),
                                                      ReceiveOBLDate = b.Field<DateTime?>("ReceiveOBLDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      GoodsInfo = b.Field<String>("DescriptionOfGood"),
                                                      Qty = b.Field<Int32?>("Quantity"),
                                                      Weight = b.Field<Decimal?>("Weight"),
                                                      Measurement = b.Field<Decimal?>("Measurement"),
                                                      IsRelease = b.Field<bool>("IsRelease"),
                                                  }).ToList();
            return results;
        }
        /// <summary>
        /// 获取HBL信息
        /// </summary>
        /// <param name="oiBookingID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        public List<OceanBusinessHBLList> GetOIBookingHBLList(Guid oiBookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIHBLList");

                db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, oiBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return ConvertTableToOceanBusinessHBLList(ds.Tables[0]);
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
        /// 保存HBL信息
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="oiBookingID">业务ID</param>
        /// <param name="nos">分提单号集合</param>
        /// <param name="amsNos">AMSNo集合</param>
        /// <param name="isfNos">ISFNo集合</param>
        /// <param name="shipperIDs">发货人ID集合</param>
        /// <param name="shipperDescriptions">发货人描述集合</param>
        /// <param name="receiveOBLDates">收到正本时间集合</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <returns></returns>
        public ManyResult SaveOIBookingHBLInfo(HBLInfoSaveRequest saveRequest)
        {
            try
            {
                ManyResult result = null;

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIBookingHBLInfo");


                string ids = saveRequest.IDs.Join();
                string nos = saveRequest.BLNos.Join();
                string amsNos = saveRequest.AMSNos.Join();
                string isfNos = saveRequest.ISFNos.Join();
                string shipperIDs = saveRequest.ShipperIDs.Join();
                string updateDates = saveRequest.UpdateDates.Join();
                string receiveOBLDates = saveRequest.ReceiveOBLDates.Join();

                string shipperDescriptions = saveRequest.ShipperDescriptions.Join();

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, saveRequest.OIBookingID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids);
                db.AddInParameter(dbCommand, "@Nos", DbType.String, nos);
                db.AddInParameter(dbCommand, "@AmsNos", DbType.String, amsNos);
                db.AddInParameter(dbCommand, "@IsfNos", DbType.String, isfNos);
                db.AddInParameter(dbCommand, "@ShipperIDs", DbType.String, shipperIDs);
                db.AddInParameter(dbCommand, "@ShipperDescriptions", DbType.String, shipperDescriptions);
                db.AddInParameter(dbCommand, "@ReceiveOBLDates", DbType.String, receiveOBLDates);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates);
                db.AddInParameter(dbCommand, "@DescriptionOfGoods", DbType.String, saveRequest.GoodsInfos.Join());
                db.AddInParameter(dbCommand, "@Quantitys", DbType.String, saveRequest.Qtys.Join());
                db.AddInParameter(dbCommand, "@Weights", DbType.String, saveRequest.Weights.Join());
                db.AddInParameter(dbCommand, "@Measurements", DbType.String, saveRequest.Measurements.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);

                result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
#if DEBUG
                foreach (SingleResult item in result.Items)
                {
                    _FCMCommonService.SaveShipmentItemForCSP(
                    new SaveRequestShipmentItemForCSP()
                    {
                        OperationID = saveRequest.OIBookingID,
                        OperationType = OperationType.OceanImport,
                        ID = item.GetValue<Guid>("ID"),
                        SaveBy = saveRequest.SaveByID,
                        UpdateDate = DateTime.Now
                    });
                }
#endif

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
        /// 删除HBL信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveOIBookingHBLInfo(
                            Guid id,
                            Guid removeByID,
                            DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOIHBLInfo");



                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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


        #endregion

        #region MBL

        #region 获得MBL列表信息
        public List<OceanBusinessMBLList> GetOIMBLList()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIMBLList");

                db.AddInParameter(dbCommand, "@MblNo", DbType.String, string.Empty);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, FCMOperationType.LCL);
                db.AddInParameter(dbCommand, "@FROMETD", DbType.DateTime, Convert.ToDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(-1).ToString("yyyy-MM-dd 0:00:00")));
                db.AddInParameter(dbCommand, "@TOETD", DbType.DateTime, Convert.ToDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("yyyy-MM-dd 23:59:59")));
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, 0);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBusinessMBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new OceanBusinessMBLList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          MBLNo = b.Field<String>("MBLNO"),
                                                          SubNo = b.Field<String>("HBLNO"),
                                                          CarrierName = b.Field<String>("CarrierName"),
                                                          AgentOfCarrierName = b.Field<String>("AgentOfCarrierName"),
                                                          VoyageName = b.Field<String>("VoyageName"),
                                                          PreVoyageName = b.Field<String>("PreVoyageName"),
                                                          FinalWareHouseName = b.Field<String>("FreightLocationName"),
                                                          ReturnLocationName = b.Field<String>("ReturnLocationName"),
                                                          ITDate = b.Field<DateTime?>("ITDate"),
                                                          ITNO = b.Field<String>("ITNO"),
                                                          ITPalce = b.Field<String>("ITPlace"),
                                                          ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                                          MBLTransportClauseID = b.Field<Guid>("TransportClauseID"),
                                                          MBLTransportClauseName = b.Field<String>("TransportClauseName"),
                                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
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

        #region 获得MBL信息
        private OceanBusinessMBLList ConvertDataRowToOceanBusinessMBLList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new OceanBusinessMBLList();
            }
            OceanBusinessMBLList result = (from b in dt.AsEnumerable()
                                           select new OceanBusinessMBLList
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               MBLNo = b.Field<String>("MBLNO"),
                                               SubNo = b.Field<String>("SubNo"),
                                               CarrierID = b.Field<Guid?>("CarrierID"),
                                               CarrierName = b.Field<string>("CarrierName"),
                                               AgentOfCarrierID = b.Field<Guid>("AgentOfCarrierID"),
                                               AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                               VoyageID = b.Field<Guid>("VoyageID"),
                                               VoyageName = b.Field<string>("VoyageName"),
                                               PreVoyageID = b.Field<Guid?>("PreVoyageID"),
                                               PreVoyageName = b.Field<string>("PreVoyageName"),
                                               FinalWareHouseID = b.Field<Guid?>("FreightLocationID") == null ? Guid.Empty : (Guid)b.Field<Guid?>("FreightLocationID"),
                                               FinalWareHouseName = b.Field<string>("FreightLocationName"),
                                               ReturnLocationID = b.Field<Guid?>("ReturnLocationID"),
                                               ReturnLocationName = b.Field<string>("ReturnLocationName"),
                                               MBLTransportClauseName = b.Field<string>("TransportClauseName"),
                                               ITDate = b.Field<DateTime?>("ITDate"),
                                               ITNO = b.Field<String>("ITNO"),
                                               ITPalce = b.Field<String>("ITPlace"),
                                               ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                               MBLTransportClauseID = b.Field<Guid>("TransportClauseID"),
                                               SaveByID = b.Field<Guid>("SaveBYID"),
                                               FreightRateID = b.Field<Guid?>("FreightRateID"),
                                               ContractNo = b.Field<string>("ContractNo"),
                                               OPNotes = b.Field<string>("OPNotes"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               GateInDate = b.Field<DateTime?>("GateInDate")
                                           }).SingleOrDefault();
            return result;
        }
        private OceanBusinessInfo ConvertDataRowToOIBussionList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new OceanBusinessInfo();
            }

            OceanBusinessInfo result = (from b in dt.AsEnumerable()
                                        select new OceanBusinessInfo
                                        {
                                            ID = b.Field<Guid>("ID"),
                                            No = b.Field<String>("RefNo"),
                                            BookingMode = (FCMBookingMode)b.Field<Byte>("BookingMode"),
                                            BookingDate = b.Field<DateTime>("BookingDate"),
                                            TradeTermID = b.Field<Guid>("TradeTermID"),
                                            GetBookingDate = b.Field<DateTime?>("GetBookingDate"),
                                            AgentNo = b.Field<String>("AgentRefNo"),
                                            OIOperationType = (FCMOperationType)b.Field<Byte>("OIOperationType"),
                                            TradeTermName = b.Field<String>("TradeTermName"),
                                            CompanyID = b.Field<Guid>("CompanyID"),
                                            CompanyName = b.Field<String>("CompanyName"),
                                            SalesID = b.Field<Guid?>("SalesID"),
                                            SalesDepartmentID = b.Field<Guid>("SalesDepartmentID"),
                                            SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                            SalesTypeName = b.Field<String>("SalesTypeName"),
                                            SalesDepartmentName = b.Field<String>("SalesDepartmentName"),
                                            FilerId = b.Field<Guid?>("FilerID"),
                                            FilerName = b.Field<String>("FilerName"),
                                            CustomerID = b.Field<Guid>("CustomerID"),
                                            ShipperID = b.Field<Guid?>("ShipperID"),
                                            ShipperName = b.Field<String>("ShipperName"),
                                            ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                            ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                            ConsigneeName = b.Field<String>("ConsigneeName"),
                                            ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                            NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                            NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                            NotifyPartyName = b.Field<String>("NotifyPartyName"),
                                            //IsTelex = b.Field<bool?>("IsTelex"),
                                            CustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomerDescription")),
                                            PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                            POLID = b.Field<Guid>("POLID"),
                                            PODID = b.Field<Guid>("PODID"),
                                            PlaceOfDeliveryID = b.Field<Guid?>("PlaceOfDeliveryID"),
                                            ContainerNo = b.Field<String>("ContainerNo"),
                                            TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                            TransportClauseName = b.Field<String>("TransportClauseName"),
                                            PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                            PaymentTermName = b.Field<String>("PaymentTermName"),
                                            Commodity = b.Field<String>("Commodity"),
                                            Quantity = b.Field<Int32>("Quantity"),
                                            QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                                            QuantityUnitName = b.Field<String>("QuantityUnitName"),
                                            Weight = b.Field<Decimal>("Weight"),
                                            WeightUnitID = b.Field<Guid>("WeightUnitID"),
                                            WeightUnitName = b.Field<String>("WeightUnitName"),
                                            Measurement = b.Field<Decimal>("Measurement"),
                                            MeasurementUnitID = b.Field<Guid>("MeasurementUnitID"),
                                            MeasurementUnitName = b.Field<String>("MeasurementUnitName"),
                                            CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")),
                                            ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                            IsTruck = b.Field<bool>("IsTruck"),
                                            IsCustoms = b.Field<bool>("IsCustoms"),
                                            IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                            IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                            IsWareHouse = b.Field<bool>("IsWarehouse"),
                                            IsReleaseNotify = b.Field<bool>("IsReleaseNotify"),
                                            IsTransport = b.Field<bool>("IsTransport"),
                                            IsClearance = b.Field<bool>("IsClearance"),
                                            ClearanceDate = b.Field<DateTime?>("ClearanceDate"),
                                            ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                            DETA = b.Field<DateTime?>("DETA"),
                                            FETA = b.Field<DateTime?>("FETA"),
                                            ETD = b.Field<DateTime?>("ETD"),
                                            ETA = b.Field<DateTime?>("ETA"),
                                            WareHouseID = b.Field<Guid?>("WareHouseID"),
                                            WareHouseName = b.Field<String>("WareHouseName"),
                                            CustomsID = b.Field<Guid?>("CustomsID"),
                                            CustomsName = b.Field<String>("CustomsBrokerName"),
                                            CustomsDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomsDescription")),
                                            //ReleaseType = (ReleaseType)b.Field<Byte>("ReleaseType"),
                                            IsTelex = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Telex ? true : false,
                                            Remark = b.Field<String>("Remark"),
                                            State = (OIOrderState)b.Field<Byte>("State"),
                                            CreateID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<String>("CreateByName"),
                                            CustomerNo = b.Field<String>("CustomerRefNo"),
                                            CustomerName = b.Field<String>("CustomerName"),
                                            PlaceOfReceiptName = b.Field<String>("PlaceOfReceiptName"),
                                            POLName = b.Field<String>("POLName"),
                                            PODName = b.Field<String>("PODName"),
                                            PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                            FinalDestinationName = b.Field<String>("FinalDestinationName"),
                                            SalesName = b.Field<String>("SalesName"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                            IsValid = b.Field<bool>("IsValid"),
                                            AgentID = b.Field<Guid>("AgentID"),
                                            AgentName = b.Field<String>("AgentName"),
                                            AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                            OverSeasFilerId = b.Field<Guid?>("OverSeasFilerId"),
                                            FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                            OverSeasFilerName = b.Field<String>("OverSeasFilerName"),
                                            MBLID = b.Field<Guid?>("OIMBLID"),
                                            CustomerService = b.Field<Guid?>("CustomerServiceID"),
                                            CustomerServiceName = b.Field<String>("CustomerServiceName"),
                                            POLFilerName = b.Field<String>("POLFilerName"),
                                            POLFilerID = b.Field<Guid?>("POLFilerID"),
                                            IsSentAN = b.Field<bool?>("IsSentAN"),
                                            IsNoticeRelease = b.Field<bool>("IsNoticeRelease"),

                                        }).SingleOrDefault();
            return result;
        }


        /// <summary>
        /// 获得MBL信息
        /// </summary>
        /// <returns></returns>
        public OceanBusinessMBLList GetOIMBLInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIMBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return ConvertDataRowToOceanBusinessMBLList(ds.Tables[0]);

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

        #region 保存MBL信息
        /// <summary>
        /// 保存MBL信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="mblNo">主提单号</param>
        /// <param name="subNo">分提单号</param>
        /// <param name="carrierID">船公司ID</param>
        /// <param name="carrierDescription">船公司描述</param>
        /// <param name="agentOfCarrierID">承运人ID</param>
        /// <param name="agentOfCarrierDescription">承运人描述</param>
        /// <param name="vesselID">大船ID</param>
        /// <param name="vesselDescription">大船描述</param>
        /// <param name="preVoyageID">驳船ID</param>
        /// <param name="preVoyageDescription">驳船描述</param>
        /// <param name="finalWareHouseID">提货地ID</param>
        /// <param name="finalWareHouseDescription">提货地描述</param>
        /// <param name="returnLocationID">还柜地ID</param>
        /// <param name="returnLocationDescription">还柜地描述</param>
        /// <param name="itno">转关号</param>
        /// <param name="itDate">转关日期</param>
        /// <param name="itPalce">转关地点</param>
        /// <param name="releaseType">放货类型</param>
        /// <param name="mblTransportClauseID">运输类型</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <returns></returns>
        public SingleResult SaveOIMBLInfo(MBLInfoSaveRequest saveRequest)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIMBLInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@MblNo", DbType.String, saveRequest.MBLNo);
                db.AddInParameter(dbCommand, "@SubNo", DbType.String, saveRequest.SubNo);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, saveRequest.CarrierID);
                db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, saveRequest.AgentOfCarrierID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, saveRequest.VesselID);
                db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, saveRequest.PreVoyageID);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);
                db.AddInParameter(dbCommand, "@FreightLocationID", DbType.Guid, saveRequest.FinalWareHouseID);
                db.AddInParameter(dbCommand, "@ReturnLocationID", DbType.Guid, saveRequest.ReturnLocationID);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Byte, saveRequest.ReleaseType);
                db.AddInParameter(dbCommand, "@Itno", DbType.String, saveRequest.ITNO);
                db.AddInParameter(dbCommand, "@ItDate", DbType.DateTime, saveRequest.ITDate);
                db.AddInParameter(dbCommand, "@ItPlace", DbType.String, saveRequest.ITPalce);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, saveRequest.MBLTransportClauseID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);
                db.AddInParameter(dbCommand, "@GateInDate", DbType.DateTime, saveRequest.GateInDate);


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
        #endregion

        #endregion

        #region 确认放单/放货

        /// <summary>
        /// 修改放货信息
        /// </summary>
        /// <param name="oibookingid">业务id</param>
        /// <param name="rblrcv">接收放单</param>
        /// <param name="urb">催港前放单</param>
        /// <param name="blrca">申请放货</param>
        /// <param name="blrc">已放货</param>
        /// <param name="changebyid">操作人</param>
        /// <param name="udn">已催客户进行付款</param>
        /// <param name="agreeRC">同意放货</param>
        public void ChangeOITrackingInfo(
            Guid oibookingid, bool rblrcv, bool urb, bool blrca, bool blrc, bool udn, bool agreeRC, Guid changebyid)
        {
            ChangeOITrackingInfo(oibookingid, rblrcv, urb, blrca, blrc, udn, agreeRC, false, false, changebyid);
        }

        /// <summary>
        /// 修改放货信息
        /// </summary>
        /// <param name="oibookingid">业务id</param>
        /// <param name="rblrcv">接收放单</param>
        /// <param name="urb">催港前放单</param>
        /// <param name="blrca">申请放货</param>
        /// <param name="blrc">已放货</param>
        /// <param name="changebyid">操作人</param>
        /// <param name="udn">已催客户进行付款</param>
        /// <param name="agreeRC">同意放货</param>
        /// <param name="omblrcved">收到MBL正本</param>
        /// <param name="maildmbl">财务寄出MBL</param>
        public void ChangeOITrackingInfo(
           Guid oibookingid, bool rblrcv, bool urb, bool blrca, bool blrc, bool udn, bool agreeRC, bool omblrcved, bool maildmbl, Guid changebyid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeOITrackingInfo");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, oibookingid);
                db.AddInParameter(dbCommand, "@RBLRcv", DbType.Boolean, rblrcv);
                db.AddInParameter(dbCommand, "@URB", DbType.Boolean, urb);
                db.AddInParameter(dbCommand, "@UDN", DbType.Boolean, udn);
                db.AddInParameter(dbCommand, "@BLRCA", DbType.Boolean, blrca);
                db.AddInParameter(dbCommand, "@BLRC", DbType.Boolean, blrc);
                db.AddInParameter(dbCommand, "@AgreeRC", DbType.Boolean, agreeRC);
                db.AddInParameter(dbCommand, "@Mac", DbType.String, ApplicationContext.Current.MacAddress);
                db.AddInParameter(dbCommand, "@OMBLRCVED", DbType.Boolean, omblrcved);
                db.AddInParameter(dbCommand, "@MAILDMBL", DbType.Boolean, maildmbl);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changebyid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);
                db.ExecuteDataSet(dbCommand);

                if (!urb) return;
                OceanBusinessInfo businessInfo = GetBusinessInfo(oibookingid);
                if (businessInfo.IsNoticeRelease) return;
                List<UserList> emaillist = GetGetPOLBillingUserList(oibookingid);
                if (emaillist == null || emaillist.Count < 1)
                {
                    return;
                }

                string email = emaillist.Aggregate("", (current, item) => current + (item.EMail + ";"));

                List<OceanBusinessHBLList> hbllist = GetOIBookingHBLList(oibookingid);
                string hblno = hbllist.Aggregate("", (current, item) => current + (item.HBLNo + "&"));
                hblno = hblno.Substring(0, hblno.Length - 1);
                if (string.IsNullOrEmpty(email)) return;
                string subject = isEnglish ? "Release Notice" : "放单通知";
                string content = "请尽快放单,需要放单的分提单号为：" + hblno;
                SendEmail(email, subject, content);
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
        /// 修改放货信息
        /// </summary>
        /// <param name="oibookingid">业务id</param>
        /// <param name="values">values</param>
        /// <param name="changebyid">更改人</param>
        public void ChangeOITrackingInfo(Guid oibookingid, IDictionary<string, object> values, Guid changebyid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeOITrackingInfo");

                bool rblrcv = values.ContainsKey("RBLRCV") && (bool)values["RBLRCV"];           //接收放单
                bool urb = values.ContainsKey("URB") && (bool)values["URB"];                    //催港前放单
                bool udn = values.ContainsKey("UDN") && (bool)values["UDN"];                    //已催客户进行付款
                bool blrca = values.ContainsKey("BLRCA") && (bool)values["BLRCA"];              //申请放货
                bool blrc = values.ContainsKey("BLRC") && (bool)values["BLRC"];                 //已放货
                bool agreeRC = values.ContainsKey("AGREERC") && (bool)values["AGREERC"];        //同意放货
                bool omblrcved = values.ContainsKey("OMBLRCVED") && (bool)values["OMBLRCVED"];  //收到MBL正本
                bool maildmbl = values.ContainsKey("MAILDMBL") && (bool)values["MAILDMBL"];     //财务寄出MBL
                bool acclos = values.ContainsKey("ACCLOS") && (bool)values["ACCLOS"];           //关帐

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, oibookingid);
                db.AddInParameter(dbCommand, "@RBLRcv", DbType.Boolean, rblrcv);
                db.AddInParameter(dbCommand, "@URB", DbType.Boolean, urb);
                db.AddInParameter(dbCommand, "@UDN", DbType.Boolean, udn);
                db.AddInParameter(dbCommand, "@BLRCA", DbType.Boolean, blrca);
                db.AddInParameter(dbCommand, "@BLRC", DbType.Boolean, blrc);
                db.AddInParameter(dbCommand, "@AgreeRC", DbType.Boolean, agreeRC);
                db.AddInParameter(dbCommand, "@OMBLRCVED", DbType.Boolean, omblrcved);
                db.AddInParameter(dbCommand, "@MAILDMBL", DbType.Boolean, maildmbl);
                db.AddInParameter(dbCommand, "@ACCLOS", DbType.Boolean, acclos);
                db.AddInParameter(dbCommand, "@Mac", DbType.String, ApplicationContext.Current.MacAddress);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changebyid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);
                db.ExecuteDataSet(dbCommand);

                if (!urb) return;
                OceanBusinessInfo businessInfo = GetBusinessInfo(oibookingid);
                if (businessInfo.IsNoticeRelease) return;
                List<UserList> emaillist = GetGetPOLBillingUserList(oibookingid);
                if (emaillist == null || emaillist.Count < 1)
                {
                    return;
                }

                string email = emaillist.Aggregate("", (current, item) => current + (item.EMail + ";"));

                List<OceanBusinessHBLList> hbllist = GetOIBookingHBLList(oibookingid);
                string hblno = hbllist.Aggregate("", (current, item) => current + (item.HBLNo + "&"));
                hblno = hblno.Substring(0, hblno.Length - 1);
                if (string.IsNullOrEmpty(email)) return;
                string subject = isEnglish ? "Release Notice" : "放单通知";
                string content = "请尽快放单,需要放单的分提单号为：" + hblno;
                SendEmail(email, subject, content);
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
        /// 第三方代理放单
        /// </summary>
        /// <param name="oibookingid"></param>
        /// <param name="rbld"></param>
        /// <param name="changebyid"></param>
        public void ReleaseOIBL(
           Guid oibookingid, bool rbld, Guid changebyid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspReleaseOIBL");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, oibookingid);
                db.AddInParameter(dbCommand, "@RBLD", DbType.Boolean, rbld);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changebyid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                db.ExecuteDataSet(dbCommand);
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
        /// 确认放单
        /// </summary>
        /// <param name="oiBookingID"></param>
        /// <param name="releaseDate"></param>
        /// <param name="updateTime"></param>
        /// <param name="saveByID"></param>
        /// <returns></returns>
        public SingleResult SetOIReleaseData(
                        Guid oiBookingID,
                        DateTime releaseDate,
                        DateTime? updateTime,
                        Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSetOIReleaseData");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, oiBookingID);
                db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, releaseDate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateTime);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "ErrorMessage" });
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
        /// 是否第3方代理
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public bool CheckIsInternalAgent(Guid? agentid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCheckIsInternalAgent");

                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, agentid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return false; }
                return (bool)ds.Tables[0].Rows[0][0];
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
        /// 催港前操作放单
        /// </summary>
        /// <param name="oibookingid"></param>
        /// <returns>返回港前操作的Email</returns> 
        public string GetPOLOperatorEmail(Guid oibookingid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetPOLOperatorEmail");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, oibookingid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                return (string)ds.Tables[0].Rows[0][0];
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
        /// 获取港前计费用户列表
        /// </summary>
        /// <param name="oibookingid"></param>
        /// <returns></returns>
        public List<UserList> GetGetPOLBillingUserList(Guid oibookingid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetPOLBillingUserList");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, oibookingid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = BulidUserListByDataSet(ds);

                if (results == null)
                {
                    return null;
                }
                List<UserList> billUserList = results.FindAll(b => b.JobName == "计费");

                return billUserList;
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
        /// 获取港前放单人信息List
        /// </summary>
        /// <param name="oibookingid"></param>
        /// <returns></returns>
        public List<string> GetGetPOLReleaseByUserList(Guid oibookingid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetPOLReleaseByEmail");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, oibookingid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<string>();
                }

                List<string> results = (from b in ds.Tables[0].AsEnumerable()
                                        select b.Field<String>("UserEmail")).ToList();

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


        private static List<UserList> BulidUserListByDataSet(DataSet ds)
        {
            List<UserList> results = (from b in ds.Tables[0].AsEnumerable()
                                      select new UserList
                                      {
                                          ID = b.Field<Guid>("ID"),
                                          Code = b.Field<string>("Code"),
                                          CName = b.Field<string>("CName"),
                                          EName = b.Field<string>("EName"),
                                          Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                                          EMail = b.Field<string>("Email"),
                                          Tel = b.Field<string>("Tel"),
                                          Fax = b.Field<string>("Fax"),
                                          Mobile = b.Field<string>("Mobile"),
                                          IsValid = b.Field<bool>("IsValid"),
                                          OrganizationName = b.Field<string>("OrganizationName"),
                                          JobName = b.Field<string>("JobName"),
                                          CreateBy = b.Field<string>("CreateBy"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                      }).ToList();
            return results;
        }


        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="sendTo">接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        public void SendEmail(string sendTo, string subject, string content)
        {
            try
            {
                UserInfo userInfo = new UserInfo();
                userInfo = _UserService.GetUserInfo(ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId);
                ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
                message.CreateBy = ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId;
                message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                message.HasAttachment = false;
                message.SendFrom = userInfo.EMail;
                message.SendTo = sendTo;
                message.CC = userInfo.EMail;
                message.Subject = subject;
                message.Body = content;
                message.Type = MessageType.Email;
                message.BodyFormat = BodyFormat.olFormatPlain;
                _MailBeeService.Send(message);
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
        }

        #endregion

        #region 获得出口业务的费用列表
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        public List<OceanImportFeeList> GetOceanExportFeeList(Guid orderID)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanOrderFeeList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanImportFeeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new OceanImportFeeList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         BookingFeeID = b.Field<Guid>("BookingFeeID"),
                                                         CurrencyID = b.Field<Guid>("CurrencyID"),
                                                         CustomerID = b.Field<Guid>("CustomerID"),
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                         ChargingCodeName = b.Field<string>("ChargingCodeName"),
                                                         Currency = b.Field<string>("Currency"),
                                                         Quantity = b.Field<decimal>("Quantity"),
                                                         UnitPrice = b.Field<decimal>("UnitPrice"),
                                                         Way = (FeeWay)b.Field<byte>("Way"),
                                                         Amount = b.Field<decimal>("Amount"),
                                                         Remark = b.Field<string>("Remark"),
                                                         CreateByID = b.Field<Guid>("CreateByID"),
                                                         CreateByName = b.Field<string>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
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

        #region 以事务的方式保存 业务编辑界面信息

        /// <summary>
        /// 以事务的方式保存业务、HBL、集装箱、费用、PO信息
        /// </summary>
        /// <param name="businessInfo">业务信息</param>
        /// <param name="hblList">HBL列表</param>
        /// <param name="containerList">集装箱列表</param>
        /// <param name="feeList">费用列表</param>
        /// <param name="poList">PO列表</param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveOIBusinessWithTrans(
                        BusinessSaveRequest businessSaveRequest,
                        MBLInfoSaveRequest mblSaveRequest,
                        List<HBLInfoSaveRequest> hblList,
                        List<ContainerSaveRequest> containerList,
                        List<FeeSaveRequest> feeList,
                        List<POSaveRequest> poList)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = EnumIsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                Guid mblID = Guid.Empty;
                ///保存MBL信息
                if (mblSaveRequest != null)
                {
                    result.Add(mblSaveRequest.RequestId,
                     new SaveResponse { RequestId = mblSaveRequest.RequestId, SingleResult = this.SaveOIMBLInfo(mblSaveRequest) });

                    mblID = result[mblSaveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                }

                if (mblID != Guid.Empty && (businessSaveRequest.MBLID == Guid.Empty || businessSaveRequest.MBLID != mblID))
                {
                    //新增时MBL为空，或者编辑时，MBL信息发生改变时，将MBLID绑定到业务中
                    businessSaveRequest.MBLID = mblID;
                }
                ///保存业务信息
                if (businessSaveRequest != null)
                {
                    result.Add(businessSaveRequest.RequestId,
                        new SaveResponse { RequestId = businessSaveRequest.RequestId, SingleResult = this.SaveOIBusinessInfo(businessSaveRequest) });
                }

                Guid newOrderID = Guid.Empty;
                if (businessSaveRequest.ID == Guid.Empty)
                {
                    newOrderID = result[businessSaveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                }
                else
                {
                    newOrderID = businessSaveRequest.ID;
                }

                ///保存HBL信息
                if (hblList != null)
                {
                    foreach (HBLInfoSaveRequest hbl in hblList)
                    {
                        if (hbl.OIBookingID == Guid.Empty)
                        {
                            hbl.OIBookingID = newOrderID;
                            hbl.IDs = new Guid?[hbl.IDs.Length];
                        }
                        result.Add(hbl.RequestId,
                         new SaveResponse { RequestId = hbl.RequestId, ManyResult = this.SaveOIBookingHBLInfo(hbl) });
                    }
                }

                if (containerList != null)
                {
                    //保存集装箱
                    foreach (ContainerSaveRequest box in containerList)
                    {
                        if (box.MBLID == Guid.Empty)
                        {
                            box.MBLID = businessSaveRequest.MBLID;

                        }
                        if (businessSaveRequest.ID == Guid.Empty)
                        {
                            box.IDs = new Guid?[box.IDs.Length];
                        }
                        result.Add(box.RequestId,
                     new SaveResponse { RequestId = box.RequestId, ManyResult = this.SaveOIContainerInfo(box) });
                    }

                    //////保存箱号与业务的关联
                    ////foreach (ContainerSaveRequest item in containerList)
                    ////{
                    ////    this.SaveOIContainerAndBusiness(newOrderID, item.RelatedContainerIDs, item.saveByID);
                    ////}
                }

                ///保存费用 
                if (feeList != null)
                {
                    foreach (FeeSaveRequest fee in feeList)
                    {
                        if (businessSaveRequest.ID == Guid.Empty)
                        {
                            fee.orderID = newOrderID;
                            fee.ids = new Guid?[fee.ids.Length];
                        }
                        result.Add(fee.RequestId,
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveOIOrderFeeList(fee) });
                    }
                }
                ///保存PO
                if (poList != null)
                {
                    foreach (POSaveRequest po in poList)
                    {
                        if (businessSaveRequest.ID == Guid.Empty)
                        {
                            po.orderID = newOrderID;
                            po.id = Guid.Empty;
                            po.itemIDs = new Guid?[po.itemIDs.Length];
                        }
                        result.Add(po.RequestId,
                            new SaveResponse { RequestId = po.RequestId, HierarchyManyResult = this.SaveOIOrderPOInfo(po) });
                    }
                }


                scope.Complete();
                return result;

            }
        }

        #endregion

        #region 以事务的方式保存派车信息
        /// <summary>
        /// 以事务的方式保存派车、集装箱列表、关联信息
        /// </summary>
        /// <param name="truckSave">派车信息</param>
        /// <param name="boxList">集装箱列表</param>
        /// <param name="boxIDList">关联信息</param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveOIOrderWithTrans(
                            Guid mBLID,
                            TruckSaveRequest truckSave,
                            List<ContainerSaveRequest> boxList)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = EnumIsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                ///保存业务信息
                if (truckSave != null)
                {
                    result.Add(truckSave.RequestId,
                        new SaveResponse { RequestId = truckSave.RequestId, SingleResult = this.SaveOceanTruckServiceInfo(truckSave) });
                }
                Guid newTruckID = Guid.Empty;
                if (truckSave.ID == Guid.Empty)
                {
                    newTruckID = result[truckSave.RequestId].SingleResult.GetValue<Guid>("ID");
                }
                //保存集装箱
                if (boxList != null)
                {
                    foreach (ContainerSaveRequest box in boxList)
                    {
                        if (box.MBLID == Guid.Empty)
                        {
                            box.MBLID = mBLID;
                            box.IDs = new Guid?[box.IDs.Length];
                        }
                        result.Add(box.RequestId,
                     new SaveResponse { RequestId = box.RequestId, ManyResult = this.SaveOIContainerInfo(box) });
                    }
                }

                scope.Complete();
                return result;

            }
        }
        #endregion

        #region 获得MBL信息
        /// <summary>
        /// 获得海进分文件签收比较时的海出MBL信息
        /// author：joe
        /// initial date:2013-07-08
        /// </summary>
        /// <returns></returns>
        public OceanBusinessMBLList GetOECompareMBLInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCompareOceanMBLInfo]");

                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanBusinessMBLList result = (from b in ds.Tables[0].AsEnumerable()
                                               select new OceanBusinessMBLList
                                               {
                                                   ID = id,
                                                   MBLNo = b.Field<String>("MBlNO"),
                                                   SubNo = b.Field<String>("HblNo"),
                                                   CarrierID = string.IsNullOrEmpty(b["CarrierID"].ToString()) ? Guid.Empty : b.Field<Guid?>("CarrierID"),
                                                   CarrierName = b.Field<string>("CarrierName"),
                                                   AgentOfCarrierID = string.IsNullOrEmpty(b["AgentOfCarrierID"].ToString()) ? Guid.Empty : b.Field<Guid>("AgentOfCarrierID"),
                                                   AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                   VoyageID = string.IsNullOrEmpty(b["VoyageID"].ToString()) ? Guid.Empty : b.Field<Guid>("VoyageID"),
                                                   VoyageName = b.Field<string>("VoyageName"),
                                                   PreVoyageID = string.IsNullOrEmpty(b["PreVoyageID"].ToString()) ? Guid.Empty : b.Field<Guid?>("PreVoyageID"),
                                                   PreVoyageName = b.Field<string>("PreVoyageName"),
                                                   ReturnLocationID = b.Field<Guid?>("ReturnLocationID"),
                                                   ReturnLocationName = b.Field<string>("ReturnLocationName"),
                                                   MBLTransportClauseID = string.IsNullOrEmpty(b["TransportClauseID"].ToString()) ? Guid.Empty : b.Field<Guid>("TransportClauseID"),
                                                   MBLTransportClauseName = b.Field<string>("TransportClauseName"),
                                                   ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                               }).FirstOrDefault();

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

        #region 获得MBL信息
        /// <summary>
        /// 获取分文档MBL信息
        /// </summary>
        /// <param name="id">MBLID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <returns></returns>
        public OceanBusinessMBLList GetDispatchMBLInfo(Guid id, Guid DispatchFileLogID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchMBLInfo]");

                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanBusinessMBLList result = (from b in ds.Tables[0].AsEnumerable()
                                               select new OceanBusinessMBLList
                                               {
                                                   ID = id,
                                                   MBLNo = b.Field<String>("MBlNO"),
                                                   SubNo = b.Field<String>("HblNo"),
                                                   CarrierID = string.IsNullOrEmpty(b["CarrierID"].ToString()) ? Guid.Empty : b.Field<Guid?>("CarrierID"),
                                                   CarrierName = b.Field<string>("CarrierName"),
                                                   AgentOfCarrierID = string.IsNullOrEmpty(b["AgentOfCarrierID"].ToString()) ? Guid.Empty : b.Field<Guid>("AgentOfCarrierID"),
                                                   AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                   VoyageID = string.IsNullOrEmpty(b["VoyageID"].ToString()) ? Guid.Empty : b.Field<Guid>("VoyageID"),
                                                   VoyageName = b.Field<string>("VoyageName"),
                                                   PreVoyageID = string.IsNullOrEmpty(b["PreVoyageID"].ToString()) ? Guid.Empty : b.Field<Guid?>("PreVoyageID"),
                                                   PreVoyageName = b.Field<string>("PreVoyageName"),
                                                   ReturnLocationID = b.Field<Guid?>("ReturnLocationID"),
                                                   ReturnLocationName = b.Field<string>("ReturnLocationName"),
                                                   MBLTransportClauseID = string.IsNullOrEmpty(b["TransportClauseID"].ToString()) ? Guid.Empty : b.Field<Guid>("TransportClauseID"),
                                                   MBLTransportClauseName = b.Field<string>("TransportClauseName"),
                                                   ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                               }).FirstOrDefault();

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
        /// 获得海进分文件签收比较时的海出HBL信息
        /// author：joe
        /// initial date:2013-07-08
        /// </summary>
        /// <param name="oiBookingID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        public List<OceanBusinessHBLList> GetOceanCompareHBLList(Guid oeBookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCompareOceanHBLInfo]");

                db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, oeBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBusinessHBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new OceanBusinessHBLList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          OIBookingID = b.Field<Guid>("OceanBookingID"),
                                                          HBLNo = b.Field<String>("No"),
                                                          ShipperID = b.Field<Guid?>("ShipperID"),
                                                          ShipperName = b.Field<String>("ShipperName"),
                                                          //  ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                          AMSNo = b.Field<String>("AMSNo"),
                                                          ISFNo = b.Field<String>("ISFNo"),
                                                          //ReceiveOBLDate = b.Field<DateTime?>("ReceiveOBLDate"),
                                                          //UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                          //GoodsInfo = b.Field<String>("DescriptionOfGood"),
                                                          //Qty = b.Field<Int32?>("Quantity"),
                                                          //Weight = b.Field<Decimal?>("Weight"),
                                                          //Measurement = b.Field<Decimal?>("Measurement"),
                                                          //IsRelease = b.Field<bool>("IsRelease"),
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

        #region HBL
        /// <summary>
        /// 获得分文件HBL信息
        /// </summary>
        /// <param name="OperationID">业务单ID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <returns>返回派车信息</returns>
        public List<OceanBusinessHBLList> GetDispatchHBLInfo(Guid OperationID, Guid DispatchFileLogID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchHBLInfo]");

                db.AddInParameter(dbCommand, "@Opertionid", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBusinessHBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new OceanBusinessHBLList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          OIBookingID = b.Field<Guid>("OceanBookingID"),
                                                          HBLNo = b.Field<String>("No"),
                                                          ShipperID = b.Field<Guid?>("ShipperID"),
                                                          ShipperName = b.Field<String>("ShipperName"),
                                                          AMSNo = b.Field<String>("AMSNo"),
                                                          ISFNo = b.Field<String>("ISFNo"),
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

        /// <summary>
        ///  获得海进分文件比较业务时海出业务详细信息
        ///  2013-07-10 joe
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <param name="OIBookingID">海进业务ID</param>
        /// <param name="DETA">最终到港日</param>
        /// <param name="ConsigneeID">收货人ID</param>
        /// <returns></returns>
        public OceanBusinessInfo GetCompareBusinessInfo(Guid OEBookingID, Guid OIBookingID, DateTime? DETA, Guid ConsigneeID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetCompareBookingInfo");

                db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid, OEBookingID);
                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookingID);
                db.AddInParameter(dbCommand, "@DETA", DbType.DateTime, DETA);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, ConsigneeID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanBusinessInfo result = (from b in ds.Tables[0].AsEnumerable()
                                            select new OceanBusinessInfo
                                            {
                                                //ID = b.Field<Guid>("ID"),
                                                //No = b.Field<String>("RefNo"),
                                                BookingMode = (FCMBookingMode)b.Field<Byte>("BookingMode"),
                                                BookingDate = b.Field<DateTime>("BookingDate"),
                                                TradeTermID = b.Field<Guid>("TradeTermID"),
                                                // GetBookingDate = b.Field<DateTime?>("GetBookingDate"),
                                                AgentNo = b.Field<String>("AgentRefNo"),
                                                OIOperationType = (FCMOperationType)b.Field<Byte>("Type"),
                                                TradeTermName = b.Field<String>("TradeTermName"),
                                                CompanyID = b.Field<Guid>("CompanyID"),
                                                CompanyName = b.Field<String>("CompanyName"),
                                                SalesID = b.Field<Guid?>("SalesID"),
                                                SalesDepartmentID = b.Field<Guid>("SalesDepartmentID"),
                                                SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                                SalesTypeName = b.Field<String>("SalesTypeName"),
                                                SalesDepartmentName = b.Field<String>("SalesDepartmentName"),
                                                //FilerId = b.Field<Guid?>("FilerID"),
                                                //FilerName = b.Field<String>("FilerName"),
                                                CustomerID = b.Field<Guid>("CustomerID"),
                                                ShipperID = b.Field<Guid?>("ShipperID"),
                                                ShipperName = b.Field<String>("ShipperName"),
                                                //  ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                                ConsigneeName = b.Field<String>("ConsigneeName"),
                                                // ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                                NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                                //  NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                                NotifyPartyName = b.Field<String>("NotifyPartyName"),
                                                //IsTelex = b.Field<bool?>("IsTelex"),
                                                //  CustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomerDescription")),
                                                PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                                POLID = b.Field<Guid>("POLID"),
                                                PODID = b.Field<Guid>("PODID"),
                                                PlaceOfDeliveryID = b.Field<Guid?>("PlaceOfDeliveryID"),
                                                // ContainerNo = b.Field<String>("ContainerNo"),
                                                TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                                TransportClauseName = b.Field<String>("TransportClauseName"),
                                                PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                                PaymentTermName = b.Field<String>("PaymentTermName"),
                                                Commodity = b.Field<String>("Commodity"),
                                                Quantity = b.Field<Int32>("Quantity"),
                                                QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                                                QuantityUnitName = b.Field<String>("QuantityUnitName"),
                                                Weight = b.Field<Decimal>("Weight"),
                                                WeightUnitID = b.Field<Guid>("WeightUnitID"),
                                                WeightUnitName = b.Field<String>("WeightUnitName"),
                                                Measurement = b.Field<Decimal>("Measurement"),
                                                MeasurementUnitID = b.Field<Guid>("MeasurementUnitID"),
                                                MeasurementUnitName = b.Field<String>("MeasurementUnitName"),
                                                //  CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")),
                                                //  ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                                //IsTruck = b.Field<bool>("IsTruck"),
                                                //IsCustoms = b.Field<bool>("IsCustoms"),
                                                //IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                                //IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                                //IsWareHouse = b.Field<bool>("IsWarehouse"),
                                                //IsReleaseNotify = b.Field<bool>("IsReleaseNotify"),
                                                //IsTransport = b.Field<bool>("IsTransport"),
                                                //IsClearance = b.Field<bool>("IsClearance"),
                                                // ClearanceDate = b.Field<DateTime?>("ClearanceDate"),
                                                // ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                                DETA = b.Field<DateTime?>("DETA"),
                                                FETA = b.Field<DateTime?>("FETA"),
                                                ETD = b.Field<DateTime?>("ETD"),
                                                ETA = b.Field<DateTime?>("ETA"),
                                                WareHouseID = b.Field<Guid?>("WareHouseID"),
                                                WareHouseName = b.Field<String>("WareHouseName"),
                                                CustomsID = b.Field<Guid?>("CustomsBrokerID"),
                                                CustomsName = b.Field<String>("CustomsBrokerName"),
                                                //  CustomsDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomsDescription")),
                                                //ReleaseType = (ReleaseType)b.Field<Byte>("ReleaseType"),
                                                IsTelex = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Telex ? true : false,
                                                // Remark = b.Field<String>("Remark"),
                                                State = (OIOrderState)b.Field<Byte>("State"),
                                                //CreateID = b.Field<Guid>("CreateByID"),
                                                //CreateByName = b.Field<String>("CreateByName"),
                                                //  CustomerNo = b.Field<String>("CustomerRefNo"),
                                                CustomerName = b.Field<String>("CustomerName"),
                                                PlaceOfReceiptName = b.Field<String>("PlaceOfReceiptName"),
                                                POLName = b.Field<String>("POLName"),
                                                PODName = b.Field<String>("PODName"),
                                                PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                                FinalDestinationName = b.Field<String>("FinalDestinationName"),
                                                SalesName = b.Field<String>("SalesName"),
                                                // CreateDate = b.Field<DateTime>("CreateDate"),
                                                //  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                // IsValid = b.Field<bool>("IsValid"),
                                                AgentID = b.Field<Guid>("AgentID"),
                                                AgentName = b.Field<String>("AgentName"),
                                                // AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                                OverSeasFilerId = b.Field<Guid?>("OverSeasFilerId"),
                                                FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                                OverSeasFilerName = b.Field<String>("OverSeasFilerName"),
                                                MBLID = b.Field<Guid?>("OIMBLID"),
                                                CustomerService = b.Field<Guid?>("CustomerServiceID"),
                                                CustomerServiceName = b.Field<String>("CustomerServiceName"),
                                                //POLFilerName = b.Field<String>("POLFilerName"),
                                                //POLFilerID = b.Field<Guid?>("POLFilerID"),
                                                //IsSentAN = b.Field<bool?>("IsSentAN"),

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
        /// 获得分文档业务详细信息
        /// </summary>
        /// <param name="OperationID">海出业务ID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <param name="SaveByID">执行人</param>
        /// <returns></returns>
        public OceanBusinessInfo GetDispatchBookingInfo(Guid OperationID, Guid DispatchFileLogID, Guid SaveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDispatchBookingInfo");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanBusinessInfo result = (from b in ds.Tables[0].AsEnumerable()
                                            select new OceanBusinessInfo
                                            {
                                                //ID = b.Field<Guid>("ID"),
                                                //No = b.Field<String>("RefNo"),
                                                BookingMode = (FCMBookingMode)b.Field<Byte>("BookingMode"),
                                                BookingDate = b.Field<DateTime>("BookingDate"),
                                                TradeTermID = b.Field<Guid>("TradeTermID"),
                                                // GetBookingDate = b.Field<DateTime?>("GetBookingDate"),
                                                AgentNo = b.Field<String>("AgentRefNo"),
                                                OIOperationType = (FCMOperationType)b.Field<Byte>("Type"),
                                                TradeTermName = b.Field<String>("TradeTermName"),
                                                CompanyID = b.Field<Guid>("CompanyID"),
                                                CompanyName = b.Field<String>("CompanyName"),
                                                SalesID = b.Field<Guid?>("SalesID"),
                                                SalesDepartmentID = b.Field<Guid>("SalesDepartmentID"),
                                                SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                                SalesTypeName = b.Field<String>("SalesTypeName"),
                                                SalesDepartmentName = b.Field<String>("SalesDepartmentName"),
                                                //FilerId = b.Field<Guid?>("FilerID"),
                                                //FilerName = b.Field<String>("FilerName"),
                                                CustomerID = b.Field<Guid>("CustomerID"),
                                                ShipperID = b.Field<Guid?>("ShipperID"),
                                                ShipperName = b.Field<String>("ShipperName"),
                                                //  ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                                ConsigneeName = b.Field<String>("ConsigneeName"),
                                                // ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ConsigneeDescription")),
                                                NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                                //  NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("NotifyPartyDescription")),
                                                NotifyPartyName = b.Field<String>("NotifyPartyName"),
                                                //IsTelex = b.Field<bool?>("IsTelex"),
                                                //  CustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomerDescription")),
                                                PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                                POLID = b.Field<Guid>("POLID"),
                                                PODID = b.Field<Guid>("PODID"),
                                                PlaceOfDeliveryID = b.Field<Guid?>("PlaceOfDeliveryID"),
                                                // ContainerNo = b.Field<String>("ContainerNo"),
                                                TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                                TransportClauseName = b.Field<String>("TransportClauseName"),
                                                PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                                PaymentTermName = b.Field<String>("PaymentTermName"),
                                                Commodity = b.Field<String>("Commodity"),
                                                Quantity = b.Field<Int32>("Quantity"),
                                                QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                                                QuantityUnitName = b.Field<String>("QuantityUnitName"),
                                                Weight = b.Field<Decimal>("Weight"),
                                                WeightUnitID = b.Field<Guid>("WeightUnitID"),
                                                WeightUnitName = b.Field<String>("WeightUnitName"),
                                                Measurement = b.Field<Decimal>("Measurement"),
                                                MeasurementUnitID = b.Field<Guid>("MeasurementUnitID"),
                                                MeasurementUnitName = b.Field<String>("MeasurementUnitName"),
                                                //  CargoDescription = SerializerHelper.DeserializeFromString<CargoDescription>(typeof(CargoDescription), b.Field<string>("CargoDescription")),
                                                //  ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                                //IsTruck = b.Field<bool>("IsTruck"),
                                                //IsCustoms = b.Field<bool>("IsCustoms"),
                                                //IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                                //IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                                //IsWareHouse = b.Field<bool>("IsWarehouse"),
                                                //IsReleaseNotify = b.Field<bool>("IsReleaseNotify"),
                                                //IsTransport = b.Field<bool>("IsTransport"),
                                                //IsClearance = b.Field<bool>("IsClearance"),
                                                // ClearanceDate = b.Field<DateTime?>("ClearanceDate"),
                                                // ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                                DETA = b.Field<DateTime?>("DETA"),
                                                FETA = b.Field<DateTime?>("FETA"),
                                                ETD = b.Field<DateTime?>("ETD"),
                                                ETA = b.Field<DateTime?>("ETA"),
                                                WareHouseID = b.Field<Guid?>("WareHouseID"),
                                                WareHouseName = b.Field<String>("WareHouseName"),
                                                CustomsID = b.Field<Guid?>("CustomsBrokerID"),
                                                CustomsName = b.Field<String>("CustomsBrokerName"),
                                                //  CustomsDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomsDescription")),
                                                //ReleaseType = (ReleaseType)b.Field<Byte>("ReleaseType"),
                                                IsTelex = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Telex ? true : false,
                                                // Remark = b.Field<String>("Remark"),
                                                State = (OIOrderState)b.Field<Byte>("State"),
                                                //CreateID = b.Field<Guid>("CreateByID"),
                                                //CreateByName = b.Field<String>("CreateByName"),
                                                //  CustomerNo = b.Field<String>("CustomerRefNo"),
                                                CustomerName = b.Field<String>("CustomerName"),
                                                PlaceOfReceiptName = b.Field<String>("PlaceOfReceiptName"),
                                                POLName = b.Field<String>("POLName"),
                                                PODName = b.Field<String>("PODName"),
                                                PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                                FinalDestinationName = b.Field<String>("FinalDestinationName"),
                                                SalesName = b.Field<String>("SalesName"),
                                                // CreateDate = b.Field<DateTime>("CreateDate"),
                                                //  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                // IsValid = b.Field<bool>("IsValid"),
                                                AgentID = b.Field<Guid>("AgentID"),
                                                AgentName = b.Field<String>("AgentName"),
                                                // AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                                OverSeasFilerId = b.Field<Guid?>("OverSeasFilerId"),
                                                FinalDestinationID = b.Field<Guid?>("FinalDestinationID"),
                                                OverSeasFilerName = b.Field<String>("OverSeasFilerName"),
                                                MBLID = b.Field<Guid?>("OIMBLID"),
                                                CustomerService = b.Field<Guid?>("CustomerServiceID"),
                                                CustomerServiceName = b.Field<String>("CustomerServiceName"),
                                                //POLFilerName = b.Field<String>("POLFilerName"),
                                                //POLFilerID = b.Field<Guid?>("POLFilerID"),
                                                //IsSentAN = b.Field<bool?>("IsSentAN"),

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

        #region 签收分发文件时，同步海出数据到海进

        /// <summary>
        /// 以事务的方式签收同步海出业务、HBL、集装箱、账单费用
        /// joe 2013-07-17
        /// </summary>
        /// <param name="businessSaveRequest">业务信息</param>
        /// <param name="mblSaveRequest">MBL信息</param>
        ///<param name="OIBookingID">海进业务ID</param>
        ///<param name="userID">当前用户ID</param>
        ///<param name="IsSaveHBL">是否需要保存HBL信息</param>
        ///<param name="IsSaveContainer">是否需要保存集装箱信息</param>
        ///<param name="IsSaveBill">是否需要保存账单费用信息</param>
        /// <returns></returns>
        public void AcceptDispatchWithTrans(
                        BusinessSaveRequest businessSaveRequest,
                        MBLInfoSaveRequest mblSaveRequest,
                        Guid OIBookingID, Guid userID, bool IsSaveHBL, bool IsSaveContainer, bool IsSaveBill)
        {
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = EnumIsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                    Guid mblID = Guid.Empty;
                    ///保存MBL信息
                    if (mblSaveRequest != null)
                    {
                        result.Add(mblSaveRequest.RequestId,
                         new SaveResponse { RequestId = mblSaveRequest.RequestId, SingleResult = this.SaveOIMBLInfo(mblSaveRequest) });

                        mblID = result[mblSaveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                    }

                    if (mblID != Guid.Empty && (businessSaveRequest.MBLID == Guid.Empty || businessSaveRequest.MBLID != mblID))
                    {
                        //新增时MBL为空，或者编辑时，MBL信息发生改变时，将MBLID绑定到业务中
                        businessSaveRequest.MBLID = mblID;
                    }
                    ///保存业务信息
                    if (businessSaveRequest != null)
                    {
                        result.Add(businessSaveRequest.RequestId,
                            new SaveResponse { RequestId = businessSaveRequest.RequestId, SingleResult = this.SaveOIBusinessInfo(businessSaveRequest) });
                    }

                    Guid newOrderID = Guid.Empty;
                    if (businessSaveRequest.ID == Guid.Empty)
                    {
                        newOrderID = result[businessSaveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                    }
                    else
                    {
                        newOrderID = businessSaveRequest.ID;
                    }

                    //保存HBL，集装箱，账单费用信息
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspAcceptDispatch");

                    db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, OIBookingID);
                    db.AddInParameter(dbCommand, "@CurrUserId", DbType.Guid, userID);
                    db.AddInParameter(dbCommand, "@IsSaveHBL", DbType.Boolean, IsSaveHBL);
                    db.AddInParameter(dbCommand, "@IsSaveContainer", DbType.Boolean, IsSaveContainer);
                    db.AddInParameter(dbCommand, "@IsSaveBill", DbType.Boolean, IsSaveBill);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);
                    db.ExecuteNonQuery(dbCommand);

                    //}
                    //catch (SqlException sqlException)
                    //{
                    //    throw new ApplicationException(sqlException.Message);
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //}

                    scope.Complete();
                    //   return result;
                }
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
        ///   港前业务的账单的所有代理账单的修改人邮箱（用";"分开）
        ///  2013-08-12 joe 
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <returns></returns>
        public string GetBillUpdateUserEmails(Guid OEBookingID)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetBillUpdateUserEmails]");

                db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid, OEBookingID);



                StringBuilder sb = new StringBuilder();

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.Append(dr[0].ToString() + ";");
                }

                return sb.Remove(sb.Length - 1, 1).ToString();

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
        ///  保存签收数据到海海进历史数据
        ///  2013-08-30 joe 
        /// </summary>
        /// <param name="OEBookingIDs">海出业务iD</param>
        /// <param name="RefIDs">签收记录ID</param>
        /// <param name="Types">签收类型（1为签收前，2为签收后）</param>
        /// <param name="userID">保存ID</param>
        /// <param name="isEnglish">是否中英文</param>
        /// <returns></returns>
        public string SaveOIInfoToHistory(string OEBookingIDs, string RefIDs, string Types, Guid userID, bool isEnglish)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveOIInfoToHistory]");

                db.AddInParameter(dbCommand, "@OEBookingIDs", DbType.String, OEBookingIDs);
                db.AddInParameter(dbCommand, "@Types", DbType.String, Types);

                db.AddOutParameter(dbCommand, "@RefIDs", DbType.String, 1000);
                db.AddInParameter(dbCommand, "@TempRefIDs", DbType.String, RefIDs);

                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, userID);

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);


                db.ExecuteNonQuery(dbCommand);
                object ss = db.GetParameterValue(dbCommand, "@RefIDs");

                return ss.ToString();
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
        /// 根据份文件记录去签收分发的记录
        /// </summary>
        /// <param name="CurrentOperationID">当前打开的业务ID</param>
        /// <param name="DispatchFileLogID">分发的文件版本记录ID</param>
        /// <param name="userID">执行人</param>
        /// <param name="isEnglish">语言</param>
        /// <returns></returns>
        public void AcceptDispatchFiles(Guid CurrentOperationID, Guid DispatchFileLogID, Guid userID, bool isEnglish)
        {
            try
            {
                Stopwatch stopwatchAccept = Stopwatch.StartNew();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspAcceptDispatchFiles]");

                db.AddInParameter(dbCommand, "@CurrentOperationID", DbType.Guid, CurrentOperationID);
                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);


                db.ExecuteNonQuery(dbCommand);
                _OperationLogService.Add(DateTime.Now, "SAVE-ACCEPT-DB", string.Format("签收分文档业务 ID[{0}]", CurrentOperationID), stopwatchAccept.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);

            }
            catch (TimeoutException timeExceptiom)
            {
                _OperationLogService.Add(DateTime.Now, "SAVE-ACCEPT-DB", string.Format("签收分文档业务 ID[{0}] 超时", CurrentOperationID), "0");
                throw new ApplicationException(timeExceptiom.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得历史海进业务详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OceanBusinessInfo GetHistoryBusinessInfo(Guid OIBookID, Guid ApplyID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOIHistoryBusinessInfo]");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, OIBookID);
                db.AddInParameter(dbCommand, "@ApplyID", DbType.Guid, ApplyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanBusinessInfo result = ConvertDataRowToOIBussionList(ds.Tables[0]);

                result.MBLInfo = ConvertDataRowToOceanBusinessMBLList(ds.Tables[1]);
                result.ContainerList = ConvertTableToOIBusinessContainerList(ds.Tables[2]);
                result.HBLList = ConvertTableToOceanBusinessHBLList(ds.Tables[3]);
                result.FeeList = ConvertTableToOceanImportFeeList(ds.Tables[4]);
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
        /// 获得历史账单比较账详细信息
        /// </summary>
        /// <param name="OIBookID">海进业务ID</param>
        /// <param name="BeforeApplyID">上次签收记录ID</param>
        /// <param name="AfterApplyID">本次签收记录ID</param>
        /// <returns></returns>

        public List<Fee> GetHistoryBillAndCharages(Guid OIBookID, Guid BeforeApplyID, Guid AfterApplyID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspGetHistoryCompareBillAndCharge]");

                //  db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid, OEBookingID);
                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookID);
                db.AddInParameter(dbCommand, "@BeforeApplyID", DbType.Guid, BeforeApplyID);
                db.AddInParameter(dbCommand, "@AfterApplyID", DbType.Guid, AfterApplyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }


                return CompareCharage(ds);
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
        /// 获得海出签收海进账单修订历史比较账详细信息
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <param name="BeforeApplyID">上次签收记录ID</param>
        /// <param name="AfterApplyID">本次签收记录ID</param>
        /// <returns></returns>

        public List<Fee> uspGetHistoryCompareReviseBillAndCharge(Guid OEBookingID, Guid BeforeApplyID, Guid AfterApplyID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspGetHistoryCompareReviseBillAndCharge]");

                //  db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid, OEBookingID);
                db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid, @OEBookingID);
                db.AddInParameter(dbCommand, "@BeforeApplyID", DbType.Guid, BeforeApplyID);
                db.AddInParameter(dbCommand, "@AfterApplyID", DbType.Guid, AfterApplyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }


                return CompareCharage(ds);
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

        private List<Fee> CompareCharage(DataSet ds)
        {

            List<Fee> lstFee = (
                                  from c in ds.Tables[0].AsEnumerable()
                                  //where c.Field<Guid>("BillID").ToString() == b.ID
                                  select new Fee
                                  {
                                      BillID = c.Field<Guid>("billID").ToString(),
                                      ID = c.Field<Guid>("ID").ToString(),
                                      IsAgent = c.Field<Boolean>("Agent") ? 1 : 0,
                                      Way = int.Parse(c["Way"].ToString()),
                                      ChargeCode = c.Field<String>("Code"),
                                      ChargeName = c.Field<String>("ChargeName"),
                                      OldSumMoney = c.Field<String>("OIAmount"),
                                      NewSumMoney = c.Field<String>("OeAmount"),
                                      OldRemark = c.Field<String>("oiremark"),
                                      NewRemark = c.Field<String>("oeremark"),
                                      UpdateState = int.Parse(c.Field<int>("state").ToString()),
                                  }
                                ).ToList();
            List<Fee> OldFee = lstFee.FindAll(r => r.UpdateState == 1);
            //更改状态
            foreach (Fee f in lstFee.FindAll(r => r.UpdateState == 2))
            {
                if (OldFee.Count(r => r.ChargeCode == f.ChargeCode) == 0)
                {
                    f.UpdateState = 1;
                }
                else if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
                {
                    f.UpdateState = 0;
                    if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                    {
                        OldFee.Find(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                    }
                }
                else if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
                {
                    if (lstFee.FindAll(r => r.UpdateState == 2).Count(s => s.ChargeCode == f.ChargeCode) > 1)
                    {
                        f.UpdateState = 1;
                        if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                        {
                            OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                        }
                    }
                    else
                    {
                        f.UpdateState = 2;
                        if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                        {
                            OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                        }
                    }
                }

            }
            if (OldFee.Count(r => r.UpdateState == 1) > 0)
            {
                foreach (Fee f in OldFee.FindAll(r => r.UpdateState == 1))
                {
                    f.UpdateState = 3;
                }
            }

            return lstFee;
        }

        private List<Fee> CompareCharageForOI(DataSet ds)
        {

            List<Fee> lstFee = (
                                  from c in ds.Tables[0].AsEnumerable()
                                  //where c.Field<Guid>("BillID").ToString() == b.ID
                                  select new Fee
                                  {
                                      BillID = c.Field<Guid>("billID").ToString(),
                                      ID = c.Field<Guid>("ID").ToString(),
                                      IsAgent = c.Field<Boolean>("Agent") ? 1 : 0,
                                      Way = int.Parse(c["Way"].ToString()),
                                      ChargeCode = c.Field<String>("Code"),
                                      ChargeName = c.Field<String>("ChargeName"),
                                      OldSumMoney = c.Field<String>("OiAmount"),
                                      NewSumMoney = c.Field<String>("OeAmount"),
                                      OldRemark = c.Field<String>("oiremark"),
                                      NewRemark = c.Field<String>("oeremark"),
                                      UpdateState = int.Parse(c.Field<int>("state").ToString()),
                                  }
                                ).ToList();
            List<Fee> OldFee = lstFee.FindAll(r => r.UpdateState == 1);
            //更改状态
            foreach (Fee f in lstFee.FindAll(r => r.UpdateState == 2))
            {
                if (OldFee.Count(r => r.ChargeCode == f.ChargeCode) == 0)
                {
                    f.UpdateState = 1;
                }
                else if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
                {
                    f.UpdateState = 0;
                    if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                    {
                        OldFee.Find(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                    }
                }
                else if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
                {
                    if (lstFee.FindAll(r => r.UpdateState == 2).Count(s => s.ChargeCode == f.ChargeCode) > 1)
                    {
                        f.UpdateState = 1;
                        if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                        {
                            OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                        }
                    }
                    else
                    {
                        f.UpdateState = 2;
                        if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                        {
                            OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                        }
                    }
                }
            }
            if (OldFee.Count(r => r.UpdateState == 1) > 0)
            {
                foreach (Fee f in OldFee.FindAll(r => r.UpdateState == 1))
                {
                    f.UpdateState = 3;
                }
            }

            return lstFee;
        }

        /// <summary>
        /// 得到内部代理信息
        /// </summary>
        /// <param name="OIBookID">海进业务ID</param>
        /// <param name="ApplyID">签收记录ID</param>
        /// <returns></returns>
        public List<Guid> GetInnerCustomers()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetInnerCustomer]");
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<Guid>();

            List<Guid> documents = (from document in set.Tables[0].AsEnumerable()
                                    select new Guid(document.Field<Guid>("ID").ToString())
                                          ).ToList();
            return documents;
        }

        #region 通过客户得到最近一票业务的报关行

        /// <summary>
        /// 通过客户得到最近一票业务的报关行
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public CustomerInfo GetCustomsBrokerForCustomerID(Guid customerid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetCustomsBrokerForCustomerID");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CustomerInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new CustomerInfo
                                       {
                                           ID = b.Field<Guid>("CustomsBrokerID"),
                                           CName = b.Field<string>("CName"),
                                           EName = b.Field<string>("EName"),
                                       }).FirstOrDefault();

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

        #region 更改海进业务的承运人
        /// <summary>
        ///海进账单保存更改业务承运人
        /// </summary>
        public void ChangeOIMBLforCarrier(Guid operationid, Guid carrierid)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationid, "operationid");
            ArgumentHelper.AssertGuidNotEmpty(carrierid, "carrierid");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeOIMBLforCarrier");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, operationid);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, carrierid);

                db.SingleHierarchyResult(dbCommand);
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            { throw ex; }
        }

        #endregion

        /// <summary>
        /// 返回符合发送催款邮件的业务id集合
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetPayNtOperationId()
        {
            List<Guid> operationIDs = new List<Guid>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetPayNtOperationID]");
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<Guid>();
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                operationIDs.Add((Guid)set.Tables[0].Rows[i]["OIBookingID"]);
            }
            return operationIDs;
        }

        /// <summary>
        /// 返回催港前放单所需要的邮箱地址（收件人，CC 地址）
        /// </summary>
        /// <param name="oiBookingId">业务ID</param>
        /// <returns></returns>
        public List<string> GetNoticeReleaseEmail(Guid oiBookingId)
        {
            List<string> str = new List<string>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSetNoticeReleaseEmail]");
            db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, oiBookingId);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<string>();
            string sento = string.Empty;
            string cc = string.Empty;
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(sento) && !string.IsNullOrEmpty(set.Tables[0].Rows[i]["RC"].ToString()))
                {
                    sento = set.Tables[0].Rows[i]["RC"].ToString();
                }
                else if (!string.IsNullOrEmpty(set.Tables[0].Rows[i]["RC"].ToString()))
                {
                    sento = sento + ";" + set.Tables[0].Rows[i]["RC"].ToString();
                }
                if (string.IsNullOrEmpty(cc) && !string.IsNullOrEmpty(set.Tables[0].Rows[i]["Filer"].ToString()))
                {
                    cc = set.Tables[0].Rows[i]["Filer"].ToString();
                }
                else if (!string.IsNullOrEmpty(set.Tables[0].Rows[i]["Filer"].ToString()))
                {
                    cc = cc + ";" + set.Tables[0].Rows[i]["Filer"].ToString();
                }
            }
            str.Add(cc);
            str.Add(sento);
            return str;
        }

        /// <summary>
        /// 返回拖车公司的邮件地址
        /// </summary>
        /// <param name="operationid"></param>
        /// <returns></returns>
        public string GetTruckCustomersEmail(Guid operationid)
        {
            string sql = "SELECT Email FROM pub.Customers AS c " +
                         "WHERE c.ID =(SELECT TruckerID FROM fcm.OITruckServices AS os WHERE os.OIBookingID='" + operationid + "')";
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return string.Empty;
            return ds.Tables[0].Rows[0]["Email"].ToString();
        }

        #region 获得分文件比较详细信息
        /////// <summary>
        ///////  获得分文件比较详细信息
        /////// </summary>
        /////// <param name="OEBookingID">出口业务ID</param>
        /////// <param name="OIBookingID">进口业务ID</param>
        /////// <param name="DispatchFileLogID">分文档日志ID</param>
        /////// <param name="OperationType">业务类型</param>
        /////// <returns></returns>
        ////public List<Fee> DispatchCompareBillAndCharge(Guid OEBookingID, Guid OIBookingID, Guid DispatchFileLogID, OperationType OperationType)
        ////{
        ////    try
        ////    {

        ////        Database db = DatabaseFactory.CreateDatabase();
        ////        DbCommand dbCommand = null;
        ////        if (OperationType == OperationType.OceanExport || OperationType == OperationType.AirExport)
        ////        {
        ////            dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspDispatchCompareBillAndCharge]");
        ////        }
        ////        else
        ////        {
        ////            dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspDispatchReviseCompareBillAndCharge]");
        ////        }

        ////        db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid, OEBookingID);
        ////        db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookingID);
        ////        db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);
        ////        db.AddInParameter(dbCommand, "@operationType", DbType.Int16, OperationType);
        ////        db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

        ////        DataSet ds = null;
        ////        ds = db.ExecuteDataSet(dbCommand);
        ////        if (ds == null || ds.Tables.Count < 1)
        ////        {
        ////            return null;
        ////        }
        ////        if (OperationType == OperationType.OceanExport || OperationType == OperationType.AirExport)
        ////        {
        ////            return CompareCharage(ds);
        ////        }
        ////        else
        ////        {
        ////            return CompareCharageForOI(ds);
        ////        }
        ////    }
        ////    catch (SqlException sqlException)
        ////    {
        ////        throw new ApplicationException(sqlException.Message);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }


        ////} 
        #endregion

    }
}

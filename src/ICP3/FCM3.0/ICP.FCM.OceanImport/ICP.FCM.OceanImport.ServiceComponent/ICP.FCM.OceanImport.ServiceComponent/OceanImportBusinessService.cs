using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.OceanImport.ServiceInterface;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using System.Transactions;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.ServiceComponent
{
    partial class OceanImportService
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
                                         DateTime? endTime)
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
                                                    UpdateByName = b.Field<String>("UpdateByName"),
                                                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                    IsSentAN = b.Field<bool?>("IsSentAN"),
                                                    IsValid = b.Field<Boolean>("IsValid"),
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
                                                IsSentAN = b.Field<bool?>("IsSentAN"),
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
        /// 根据ID获得业务编辑界面的详细数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OceanBusinessInfo GetBusinessInfoByEdit(Guid id)
        {
            OceanBusinessInfo businessInfo = GetBusinessInfo(id);
            if (businessInfo.MBLID != null)
            {
                businessInfo.MBLInfo = this.GetOIMBLInfo(businessInfo.MBLID.ToGuid());
                businessInfo.ContainerList = this.GetOIContainerList(businessInfo.ID);
            }
            else
            {
                businessInfo.MBLInfo = new OceanBusinessMBLList();
                businessInfo.ContainerList = new List<OIBusinessContainerList>();
            }
            businessInfo.HBLList = this.GetOIBookingHBLList(businessInfo.ID);

            businessInfo.FeeList = this.GetOIOrderFeeList(businessInfo.ID);
            businessInfo.POList = this.GetOIOrderPOList(businessInfo.ID);

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
                      OEBLState? state,
                      bool? isValid,
                      int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIDownLoadOceanExportList");

                db.AddInParameter(dbCommand, "@BLNo", DbType.String, bLNo);
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
                                                               //IsCheck=false,
                                                               ID = b.Field<Guid>("ID"),
                                                               PODRefNo = b.Field<String>("RefNo"),
                                                               DownLoadState = (DownLoadState)b.Field<Byte>("State"),
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
                    Guid saveByID)
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
                data.BusinessList = GetBusinessListByIds(ids.ToArray());
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
                string customerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.CustomerDescription, true, false);
                string shipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ShipperDescription, true, false);
                string consigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ConsigneeDescription, true, false);
                string agentDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.AgentDescription, true, false);
                string notifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.NotifyPartyDescription, true, false);
                string wareHouseDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.WareHouseDescription, true, false);
                string customsDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.CustomerDescription, true, false);

                string containerDescription = (saveRequest.ContainerDescription == null || saveRequest.ContainerDescription.Containers.Count == 0) ? null : SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.ContainerDescription, true, false);
                string cargoDescription = SerializerHelper.SerializeToString<CargoDescription>(saveRequest.CargoDescription, true, false);


                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIBusinessInfo");

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
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, saveRequest.CarrierID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.Updatedate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);

                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "State" });

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

                List<OceanBusinessHBLList> results = (from b in ds.Tables[0].AsEnumerable()
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
                                                          Measurement = b.Field<Decimal?>("Measurement")
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

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
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

                OceanBusinessMBLList result = (from b in ds.Tables[0].AsEnumerable()
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
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
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

        #region 确认放单
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
                        FCMReleaseType releaseType,
                        DateTime releaseDate,
                        DateTime? updateTime,
                        Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSetOIReleaseData");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, oiBookingID);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Byte, releaseType);
                db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, releaseDate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateTime);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
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
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
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
    }
}

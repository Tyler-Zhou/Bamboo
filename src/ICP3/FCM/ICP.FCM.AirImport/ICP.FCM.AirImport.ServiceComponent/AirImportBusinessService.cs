using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace ICP.FCM.AirImport.ServiceComponent
{
    partial class AirImportService
    {

        #region 根据ID集合查询列表
        /// <summary>
        /// 根据ID集合获得业务列表数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<AirBusinessList> GetBusinessListByIds(Guid[] ids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIBusinessListByIds");

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
        public List<AirBusinessList> GetBusinessListByFastSearch(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIBusinessListByFastSearch");

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
                //db.AddInParameter(dbCommand, "@FilerId", DbType.Guid, filerID);
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
        public List<AirBusinessList> GetBusinessList(
                                         Guid[] companyIDs,
                                         string no,
                                         string blNo,
                                         string clearanceNo,
                                         string customerName,
                                         string agent,
                                         string consignee,
                                         string shipper,
                                         string notifyPart,
                                         string pol,
                                         string pod,
                                         string placeOfDelivery,
                                         string flightNo,
                                         Guid? customerServiceID,
                                         Guid? filerID,
                                         Guid? salesID,
                                         AIOrderState? state,
                                         bool? isValid,
                                         int maxRecords,
                                         OIBusinessDateSearchType dateSearchType,
                                         DateTime? beginTime,
                                         DateTime? endTime)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIBusinessList");

                string companyIDList = companyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDList);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@BLNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ClearanceNo", DbType.String, clearanceNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@AgentName", DbType.String, agent);
                db.AddInParameter(dbCommand, "@Consignee", DbType.String, consignee);
                db.AddInParameter(dbCommand, "@Shipper", DbType.String, shipper);
                db.AddInParameter(dbCommand, "@NotifyPart", DbType.String, notifyPart);
                db.AddInParameter(dbCommand, "@Pol", DbType.String, pol);
                db.AddInParameter(dbCommand, "@Pod", DbType.String, pod);
                db.AddInParameter(dbCommand, "@FinalDestination", DbType.String, placeOfDelivery);
                db.AddInParameter(dbCommand, "@FlightNo", DbType.String, flightNo);
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
        private List<AirBusinessList> GetBusinessList(DataSet ds)
        {
            try
            {
                List<AirBusinessList> list = (from b in ds.Tables[0].AsEnumerable()
                                              select new AirBusinessList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  No = b.Field<String>("NO"),
                                                  State = (AIOrderState)b.Field<Byte>("State"),
                                                  MBLID = b.Field<Guid?>("AIMAWBID"),
                                                  MBLNo = b.Field<String>("MBLNO"),
                                                  SubNo = b.Field<String>("HBLNO"),
                                                  AgentName = b.Field<String>("AgentName"),
                                                  CustomerName = b.Field<String>("CustomerName"),
                                                  FlightNo = b.Field<String>("FlightNo"),
                                                  Paid = b.Field<bool?>("Paid"),
                                                  OBLRcved = b.Field<bool?>("OBLRcved"),
                                                  POLName = b.Field<String>("POLName"),
                                                  PODName = b.Field<String>("PODName"),
                                                  PlaceOfDeliveryName = b.Field<String>("FinalDestinationName"),
                                                  DETA = b.Field<DateTime?>("FETA"),
                                                  ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType"),
                                                  IsTelex = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Telex ? true : false,
                                                  FilerName = b.Field<String>("FilerName"),
                                                  POLFilerName = b.Field<String>("POLFilerName"),
                                                  CustomerServiceName = b.Field<String>("CustomerServiceName"),
                                                  ITNO = b.Field<String>("ITNO"),
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
                                                  IsValid = b.Field<Boolean>("IsValid"),
                                                  CompanyID = b.Field<Guid>("CompanyID"),
                                              }).ToList<AirBusinessList>();

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
        public AirBusinessInfo GetBusinessInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIBusinessInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AirBusinessInfo result = (from b in ds.Tables[0].AsEnumerable()
                                          select new AirBusinessInfo
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              No = b.Field<String>("No"),
                                              BookingMode = (FCMBookingMode)b.Field<Byte>("BookingMode"),
                                              BookingDate = b.Field<DateTime>("BookingDate"),
                                              TradeTermID = b.Field<Guid>("TradeTermID"),
                                              AgentNo = b.Field<String>("AgentRefNo"),
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
                                              CustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomerDescription")),
                                              POLID = b.Field<Guid>("DepartureID"),
                                              PODID = b.Field<Guid>("DestinationID"),
                                              PlaceOfDeliveryID = b.Field<Guid>("FinalDestinationID"),
                                              DOCPickupDate = b.Field<DateTime?>("DOCPickupDate"),
                                              DOCPickupBy = b.Field<String>("DOCPickupBy"),
                                              StorageStartDate = b.Field<DateTime?>("StorageStartDate"),
                                              WarehouseArrivedON = b.Field<DateTime?>("WarehouseArrivedON"),
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
                                              IsTruck = b.Field<bool>("IsTruck"),
                                              IsCustoms = b.Field<bool>("IsCustoms"),
                                              IsCommodityInspection = b.Field<bool>("IsCommodityInspection"),
                                              IsQuarantineInspection = b.Field<bool>("IsQuarantineInspection"),
                                              IsWareHouse = b.Field<bool>("IsWarehouse"),
                                              IsReleaseNotify = b.Field<bool>("IsReleaseOrderRequired"),
                                              IsTransport = b.Field<bool>("IsDoorMove"),
                                              IsClearance = b.Field<bool>("IsClearance"),
                                              ClearanceDate = b.Field<DateTime?>("ClearanceDate"),
                                              ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                              DETA = b.Field<DateTime?>("FETA"),
                                              ETD = b.Field<DateTime?>("ETD"),
                                              ETA = b.Field<DateTime?>("ETA"),
                                              WareHouseID = b.Field<Guid?>("WareHouseID"),
                                              WareHouseName = b.Field<String>("WareHouseName"),
                                              WareHouseDate = b.Field<DateTime?>("WareHouseDate"),
                                              CustomsID = b.Field<Guid?>("CustomsBrokerID"),
                                              CustomsName = b.Field<String>("CustomerBrokerName"),
                                              IsTelex = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Telex ? true : false,
                                              Remark = b.Field<String>("Remark"),
                                              State = (AIOrderState)b.Field<Byte>("State"),
                                              CreateID = b.Field<Guid>("CreateByID"),
                                              CreateByName = b.Field<String>("CreateByName"),
                                              CustomerNo = b.Field<String>("CustomerRefNo"),
                                              CustomerName = b.Field<String>("CustomerName"),
                                              POLName = b.Field<String>("DepartureName"),
                                              PODName = b.Field<String>("DestinationName"),
                                              PlaceOfDeliveryName = b.Field<String>("FinalDestinationName"),
                                              SalesName = b.Field<String>("SalesName"),
                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                              IsValid = b.Field<bool>("IsValid"),
                                              AgentID = b.Field<Guid?>("AgentID"),
                                              AgentName = b.Field<String>("AgentName"),
                                              AgentDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("AgentDescription")),
                                              MBLID = b.Field<Guid?>("MAWBID"),
                                              CustomerService = b.Field<Guid?>("CustomerServiceID"),
                                              CustomerServiceName = b.Field<String>("CustomerServiceName"),
                                              POLFilerName = b.Field<String>("POLFilerName"),
                                              IsSentAN = b.Field<bool?>("IsSentAN"),
                                              ClearanceNo = b.Field<string>("ClearanceNo"),
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
        public AirBusinessInfo GetBusinessInfoByEdit(Guid id)
        {
            AirBusinessInfo businessInfo = GetBusinessInfo(id);
            if (businessInfo.MBLID != null)
            {
                businessInfo.MBLInfo = this.GetAIMBLInfo(businessInfo.MBLID.ToGuid());
            }
            else
            {
                businessInfo.MBLInfo = new AirBusinessMBLList();
            }

            businessInfo.HBLList = this.GetAIBookingHBLList(businessInfo.ID);

            businessInfo.FeeList = this.GetAIOrderFeeList(businessInfo.ID, Guid.Empty);

            return businessInfo;
        }

        #endregion

        #region  下载
         /// <summary>
            /// 获得下载清单列表
         /// </summary>
         /// <param name="bLNo"></param>
         /// <param name="flightNo"></param>
         /// <param name="dateType"></param>
         /// <param name="beginDate"></param>
         /// <param name="endDate"></param>
         /// <param name="polName"></param>
         /// <param name="podName"></param>
         /// <param name="placeOfDelivery"></param>
         /// <param name="consigneeName"></param>
         /// <param name="polCompanyID"></param>
         /// <param name="companyIDs"></param>
         /// <param name="state"></param>
         /// <param name="isValid"></param>
         /// <param name="maxRecords"></param>
         /// <returns></returns>
        public List<AirBusinessDownLoadList> GetAirExportList(
                      string bLNo,
                      string flightNo,
                      DateType? dateType,
                      DateTime? beginDate,
                      DateTime? endDate,
                      string polName,
                      string podName,
                      string placeOfDelivery,
                      string consigneeName,
                      Guid? polCompanyID,
                      string companyIDs,
                      AIBLState? state,
                      bool? isValid,
                      int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetAIDownLoadAirExportList]");

                db.AddInParameter(dbCommand, "@BLNo", DbType.String, bLNo);
                db.AddInParameter(dbCommand, "@FlightNo", DbType.String, flightNo);
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

                List<AirBusinessDownLoadList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new AirBusinessDownLoadList
                                                         {
                                                             ID = b.Field<Guid>("ID"),
                                                             PODRefNo = b.Field<String>("RefNo"),
                                                             DownLoadState = (DownLoadState)b.Field<Byte>("State"),
                                                             HBLState = (AIBLState)b.Field<Byte>("HBLState"),
                                                             MBLNo = b.Field<String>("MBLNo"),
                                                             HBLNo = b.Field<String>("HBLNos"),
                                                             HBLIDs = b.Field<String>("HBLIDS"),
                                                             FlightNo = b.Field<String>("FlightNo"),
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

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspAcceptDispatchFilesForAir]");

                db.AddInParameter(dbCommand, "@CurrentOperationID", DbType.Guid, CurrentOperationID);
                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, userID);
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
        /// 下载业务单
        /// </summary>
        /// <param name="mblIDs">MBLID集合</param>
        /// <param name="consigneeIDs">收货人集合</param>
        /// <param name="hblIDs">HBLID集合</param>
        /// <param name="saveByID">保存人</param>
        /// <returns></returns>
        public List<AirBusinessList> DownLoadBusiness(
                    Guid[] mblIDs,
                    Guid[] consigneeIDs,
                    DateTime?[] detas,
                    string[] hblIDs,
                    Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspAIDownLoadBusiness");

                string mblIDList = mblIDs.Join();
                string consigneeIDList = consigneeIDs.Join();
                string detaList = detas.Join();
                string hblIDList = hblIDs.Join();

                db.AddInParameter(dbCommand, "@AEMBLIDs", DbType.String, mblIDList);
                db.AddInParameter(dbCommand, "@ConsigneeIDs", DbType.String, consigneeIDList);
                db.AddInParameter(dbCommand, "@FETAs", DbType.String, detaList);
                db.AddInParameter(dbCommand, "@AEHBLIDS", DbType.String, hblIDList);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> ids = (from b in ds.Tables[1].AsEnumerable()
                                  select new Guid(b.Field<Guid>("AIBookingID").ToString()))
                                                           .ToList();
                return GetBusinessListByIds(ids.ToArray());
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
        public List<AirBusinessList> DownLoadBusinessFromDispatchFile(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.[uspBuilderAIJobFromDispatchFile]");

                string mblIDList = mblIDs.Join();
                string consigneeIDList = consigneeIDs.Join();
                string detaList = detas.Join();
                string hblIDList = hblIDs.Join();

                db.AddInParameter(dbCommand, "@AirMBLIDs", DbType.String, mblIDList);
                db.AddInParameter(dbCommand, "@ConsigneeIDs", DbType.String, consigneeIDList);
                db.AddInParameter(dbCommand, "@DETAs", DbType.String, detaList);
                db.AddInParameter(dbCommand, "@AirHBLIDS", DbType.String, hblIDList);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> ids = (from b in ds.Tables[1].AsEnumerable()
                                  select new Guid(b.Field<Guid>("OIBookingID").ToString()))
                                                                         .ToList();
                return GetBusinessListByIds(ids.ToArray());
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
        public SingleResult CancelAirBusiness(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspAITransferBusiness");

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
        public List<AirImportTruckInfo> GetAirTruckServiceList(Guid businessID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAITruckServiceList");
                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, businessID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirImportTruckInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new AirImportTruckInfo
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        //OIBookingID = b.Field<Guid>("AIBookingID"),
                                                        TruckerID = b.Field<Guid>("TruckerID"),
                                                        TruckerName = b.Field<String>("TruckerName"),
                                                        PickUpAtID = b.Field<Guid>("PickUpAtID"),
                                                        PickUpAtName = b.Field<String>("PickUpAtName"),
                                                        PickUpDate = b.Field<DateTime?>("PickUpDate"),
                                                        PickUpRefNo = b.Field<String>("PickUpRefNo"),
                                                        PickUpContact = b.Field<String>("PickUpContact"),
                                                        DeliveryNo = b.Field<String>("DeliveryNo"),
                                                        DeliveryContact = b.Field<String>("DeliveryContact"),
                                                        DeliveryAtID = b.Field<Guid>("DeliveryAtID"),
                                                        DeliveryAtName = b.Field<String>("DeliveryAtName"),
                                                        DeliveryDate = b.Field<DateTime?>("DeliveryDate"),
                                                        BillToID = b.Field<Guid>("BillToID"),
                                                        BillToName = b.Field<String>("BillTaName"),
                                                        IsDrivingLicence = b.Field<Boolean>("IsDrivingLicence"),
                                                        NO = b.Field<String>("NO"),
                                                        Remark = b.Field<String>("Remark"),
                                                        Commodity = b.Field<String>("Commodity"),
                                                        CreateByName = b.Field<String>("CreateBy"),
                                                        CreateDate = b.Field<DateTime>("CreateDate"),
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
        public SingleResult SaveAirTruckServiceInfo(TruckSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAITruckInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, saveRequest.OIBookingID);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.NO);
                db.AddInParameter(dbCommand, "@TruckerID", DbType.Guid, saveRequest.TruckerID);
                db.AddInParameter(dbCommand, "@PickUpDate", DbType.DateTime, saveRequest.PickUpDate);
                db.AddInParameter(dbCommand, "@PickUpRefNo", DbType.String, saveRequest.PickUpRefNo);
                db.AddInParameter(dbCommand, "@PickUpContact", DbType.String, saveRequest.PickUpContact);
                db.AddInParameter(dbCommand, "@DeliveryNo", DbType.String, saveRequest.DeliveryNo);
                db.AddInParameter(dbCommand, "@DeliveryContact", DbType.String, saveRequest.DeliveryContact);
                db.AddInParameter(dbCommand, "@IsDrivingLicence", DbType.Boolean, saveRequest.IsDrivingLicence);
                db.AddInParameter(dbCommand, "@PickUpAtID", DbType.Guid, saveRequest.PickUpAtID);
                db.AddInParameter(dbCommand, "@DeliveryAtID", DbType.Guid, saveRequest.DeliveryAtID);
                db.AddInParameter(dbCommand, "@DeliveryDate", DbType.DateTime, saveRequest.DeliveryDate);
                db.AddInParameter(dbCommand, "@BillTAID", DbType.Guid, saveRequest.BillToID);
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
        public void RemoveAirTruckServiceInfo(
                     Guid id,
                     Guid removeByID,
                     DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAITruckInfo");

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
        public SingleResult GetRecentlyAITruckInfoList(Guid companyID, Guid customerID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetRecentlyAITruckInfoList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        public SingleResult SaveAIBusinessInfo(AirBookingSaveRequest saveRequest)
        {
            try
            {
                SingleResult result = null;



                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAIBusinessInfo");

                #region Parameter
                string customerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.CustomerDescription, true, false);
                string shipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ShipperDescription, true, false);
                string consigneeDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.ConsigneeDescription, true, false);
                string agentDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.AgentDescription, true, false);
                string notifyPartyDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.NotifyPartyDescription, true, false);
                string wareHouseDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.WareHouseDescription, true, false);
                string customsDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.CustomerDescription, true, false);

                string cargoDescription = SerializerHelper.SerializeToString<CargoDescription>(saveRequest.CargoDescription, true, false);
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.No);
                db.AddInParameter(dbCommand, "@Companyid", DbType.Guid, saveRequest.CompanyID);
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

                db.AddInParameter(dbCommand, "@WareHouseDate", DbType.DateTime, saveRequest.WareHouseDate);
                db.AddInParameter(dbCommand, "@StorageStartDate", DbType.DateTime, saveRequest.StorageStartDate);
                db.AddInParameter(dbCommand, "@WarehouseArrivedON", DbType.DateTime, saveRequest.WarehouseArrivedON);
                db.AddInParameter(dbCommand, "@DOCPickupDate", DbType.DateTime, saveRequest.DOCPickupDate);
                db.AddInParameter(dbCommand, "@DOCPickupBy", DbType.String, saveRequest.DOCPickupBy);

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
                db.AddInParameter(dbCommand, "@PolID", DbType.Guid, saveRequest.POLID);
                db.AddInParameter(dbCommand, "@PodID", DbType.Guid, saveRequest.PODID);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, saveRequest.PlaceOfDeliveryID);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);
                db.AddInParameter(dbCommand, "@Feta", DbType.DateTime, saveRequest.DETA);
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
                db.AddInParameter(dbCommand, "@AirlineID", DbType.Guid, saveRequest.AirCompanyID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.Updatedate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.IsEnglish);
                db.AddInParameter(dbCommand, "@ClearanceNo", DbType.String, saveRequest.ClearanceNo);
                #endregion

                result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "State" });
#if DEBUG
                if (saveRequest != null)
                {
                    SaveRequestShipmentInfoForCSP sqShipmentInfo = new SaveRequestShipmentInfoForCSP()
                    {
                        OperationID = result.GetValue<Guid>("ID"),
                        OperationType = OperationType.AirImport,
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
        /// <summary>
        /// 获取HBL信息
        /// </summary>
        /// <param name="oiBookingID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        public List<AirBusinessHBLList> GetAIBookingHBLList(Guid oiBookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIHBLList");

                db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, oiBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBusinessHBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new AirBusinessHBLList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        OIBookingID = b.Field<Guid>("AIBookingID"),
                                                        HBLNo = b.Field<String>("No"),
                                                        ShipperID = b.Field<Guid?>("ShipperID"),
                                                        ShipperName = b.Field<String>("ShipperName"),
                                                        ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                        ReceiveOBLDate = b.Field<DateTime?>("ReceiveOBLDate"),
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
        public ManyResult SaveAIBookingHBLInfo(HBLInfoSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAIHBLInfo");


                string ids = saveRequest.IDs.Join();
                string nos = saveRequest.BLNos.Join();
                string shipperIDs = saveRequest.ShipperIDs.Join();
                string updateDates = saveRequest.UpdateDates.Join();
                string receiveOBLDates = saveRequest.ReceiveOBLDates.Join();

                string shipperDescriptions = saveRequest.ShipperDescriptions.Join();

                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, saveRequest.OIBookingID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids);
                db.AddInParameter(dbCommand, "@Nos", DbType.String, nos);
                db.AddInParameter(dbCommand, "@ShipperIDs", DbType.String, shipperIDs);
                db.AddInParameter(dbCommand, "@ShipperDescriptions", DbType.String, shipperDescriptions);
                db.AddInParameter(dbCommand, "@ReceiveOBLDates", DbType.String, receiveOBLDates);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates);
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
        public void RemoveAIBookingHBLInfo(
                            Guid id,
                            Guid removeByID,
                            DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAIHBLInfo");



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
        public List<AirBusinessMBLList> GetAIMBLList()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIMBLList");

                db.AddInParameter(dbCommand, "@MblNo", DbType.String, string.Empty);
                db.AddInParameter(dbCommand, "@FROMETD", DbType.DateTime, Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 0:00:00")));
                db.AddInParameter(dbCommand, "@TOETD", DbType.DateTime, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59")));
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, 0);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBusinessMBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new AirBusinessMBLList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        MBLNo = b.Field<String>("MBLNO"),
                                                        SubNo = b.Field<String>("SUBMAWBNO"),
                                                        AirCompanyName = b.Field<String>("AirlineName"),
                                                        AgentOfCarrierName = b.Field<String>("AgentOfCarrierName"),
                                                        FlightNo = b.Field<String>("FlightNo"),
                                                        FinalWareHouseName = b.Field<String>("FreightLocationName"),
                                                        FlightFlag = b.Field<String>("FlightFlag"),
                                                        FlightCountry = b.Field<String>("FlightCountry"),
                                                        ManifestNO = b.Field<String>("ManifestNO"),
                                                        GODate = b.Field<DateTime?>("GODate"),
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
        public AirBusinessMBLList GetAIMBLInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAIMBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AirBusinessMBLList result = (from b in ds.Tables[0].AsEnumerable()
                                             select new AirBusinessMBLList
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 MBLNo = b.Field<String>("MBLNO"),
                                                 SubNo = b.Field<String>("SubNo"),
                                                 AirCompanyID = b.Field<Guid>("AirlineID"),
                                                 AirCompanyName = b.Field<string>("AirlineName"),
                                                 FlightID = b.Field<Guid?>("FlightID"),
                                                 FlightNo = b.Field<String>("FlightNo"),
                                                 AgentOfCarrierID = b.Field<Guid>("AgentOfCarrierID"),
                                                 AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                 FlightFlag = b.Field<String>("FlightFlag"),
                                                 FlightCountry = b.Field<String>("FlightCountry"),
                                                 ManifestNO = b.Field<String>("ManifestNO"),
                                                 GODate = b.Field<DateTime?>("GODate"),
                                                 FinalWareHouseID = b.Field<Guid?>("FreightLocationID") == null ? Guid.Empty : (Guid)b.Field<Guid?>("FreightLocationID"),
                                                 FinalWareHouseName = b.Field<string>("FreightLocationName"),
                                                 MBLTransportClauseName = b.Field<string>("TransportClauseName"),
                                                 ITDate = b.Field<DateTime?>("ITDate"),
                                                 ITNO = b.Field<String>("ITNO"),
                                                 ITPalce = b.Field<String>("ITPlace"),
                                                 ReleaseType = (FCMReleaseType)b.Field<Byte>("ReleaseType") == FCMReleaseType.Unknown ? FCMReleaseType.Telex : (FCMReleaseType)b.Field<Byte>("ReleaseType"), 
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
        public SingleResult SaveAIMBLInfo(MBLInfoSaveRequest saveRequest)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAIMBLInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.MBLNo);
                db.AddInParameter(dbCommand, "@SUBMAWBNO", DbType.String, saveRequest.SubNo);

                db.AddInParameter(dbCommand, "@ManifestNo", DbType.String, saveRequest.ManifestNO);
                db.AddInParameter(dbCommand, "@FlightFlag", DbType.String, saveRequest.FlightFlag);
                db.AddInParameter(dbCommand, "@FlightCountry", DbType.String, saveRequest.FlightCountry);
                db.AddInParameter(dbCommand, "@GODate", DbType.String, saveRequest.GODate);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, saveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, saveRequest.ETA);

                db.AddInParameter(dbCommand, "@AirlineID", DbType.Guid, saveRequest.AirCompanyID);
                db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, saveRequest.AgentOfCarrierID);
                db.AddInParameter(dbCommand, "@FlightID", DbType.Guid, saveRequest.FlightID);
                db.AddInParameter(dbCommand, "@FreightLocationID", DbType.Guid, saveRequest.FinalWareHouseID);
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
        public SingleResult SetAIReleaseData(
                        Guid oiBookingID,
                        FCMReleaseType releaseType,
                        DateTime releaseDate,
                        DateTime? updateTime,
                        Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSetAIReleaseData");

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
        public List<AirImportFeeList> GetAirExportFeeList(Guid orderID)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderFeeList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirImportFeeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new AirImportFeeList
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
        public Dictionary<Guid, SaveResponse> SaveAIBusinessWithTrans(
                        AirBookingSaveRequest businessSaveRequest,
                        MBLInfoSaveRequest mblSaveRequest,
                        List<HBLInfoSaveRequest> hblList,
                        List<FeeSaveRequest> feeList)
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
                     new SaveResponse { RequestId = mblSaveRequest.RequestId, SingleResult = this.SaveAIMBLInfo(mblSaveRequest) });

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
                        new SaveResponse { RequestId = businessSaveRequest.RequestId, SingleResult = this.SaveAIBusinessInfo(businessSaveRequest) });
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
                         new SaveResponse { RequestId = hbl.RequestId, ManyResult = this.SaveAIBookingHBLInfo(hbl) });
                    }
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
                            new SaveResponse { RequestId = fee.RequestId, ManyResult = this.SaveAIOrderFeeList(fee) });
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
        public Dictionary<Guid, SaveResponse> SaveAIOrderWithTrans(
                            Guid mBLID,
                            TruckSaveRequest truckSave)
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
                        new SaveResponse { RequestId = truckSave.RequestId, SingleResult = this.SaveAirTruckServiceInfo(truckSave) });
                }
                Guid newTruckID = Guid.Empty;
                if (truckSave.ID == Guid.Empty)
                {
                    newTruckID = result[truckSave.RequestId].SingleResult.GetValue<Guid>("ID");
                }

                scope.Complete();
                return result;

            }
        }
        #endregion
    }
}

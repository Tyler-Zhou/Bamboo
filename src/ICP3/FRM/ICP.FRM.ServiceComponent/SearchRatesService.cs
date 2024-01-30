using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using System.Transactions;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FRM.ServiceComponent
{
    public class SearchRatesService : ISearchRatesService
    {

        #region 初始化

        private ISessionService _sessionService;
        private IGeographyService _GeographyService;

        public SearchRatesService(
            ISessionService sessionService,
            IGeographyService geographyService)
        {
             _sessionService = sessionService;
             _GeographyService = geographyService;
        }

        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }

        #endregion

        #region 查询海运运价

        #region 获得海运运价详细信息
        /// <summary>
        /// 查询海运BaseInfo
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public SearchOceanBaseInfo GetSearchOceanBaseInfo(Guid id, SearchRateType searchRateType)
        {
            try
            {
                if (searchRateType == SearchRateType.Inquiry)
                    return GetSearchOceanBaseInfoByInquirePrice(id);
                else
                    return GetSearchOceanBaseInfoByContract(id);
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// 查询海运BaseInfo
        /// </summary>
        /// <returns></returns>
        private SearchOceanBaseInfo GetSearchOceanBaseInfoByContract(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchOceanBaseInfoByContract");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                SearchOceanBaseInfo results = (from b in ds.Tables[0].AsEnumerable()
                                               select new SearchOceanBaseInfo
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   Type = (SearchRateType)b.Field<byte>("Type"),
                                                   OceanID = b.Field<Guid>("OceanID"),
                                                   BasePortNumber = b.Field<String>("BasePortNumber"),
                                                   OriginArbNumber = b.Field<String>("OriginArbNumber"),
                                                   DestArbNumber = b.Field<String>("DestArbNumber"),
                                                   POL = b.Field<String>("POLName"),
                                                   VIA = b.Field<String>("VIAName"),
                                                   POD = b.Field<String>("PODName"),
                                                   Delivery = b.Field<String>("DeliveryName"),
                                                   ContractNo = b.Field<String>("ContractNo"),
                                                   CLS = b.Field<String>("CLS"),
                                                   Surcharge = b.Field<String>("SurCharge"),
                                                   TT = b.Field<String>("TT"),
                                                   Term = b.Field<String>("Term"),
                                                   Remark = b.Field<String>("Remark"),
                                                   Updater = b.Field<String>("UpdateBy"),
                                                   Currency = b.Field<String>("Currency"),
                                                   Carrier = b.Field<String>("CarrierName"),
                                                   ItemCode = b.Field<String>("ItemCode"),
                                                   Commodity = b.Field<String>("Commodity"),
                                                   RemarkDetails = b.Field<String>("RemarkDetail"),
                                                   FilesCount = b.Field<Int32>("FilesCount"),

                                                   UnitList = (from u in ds.Tables[1].AsEnumerable()
                                                               where u.Field<Guid>("ParentID") == b.Field<Guid>("ID")
                                                               select new FrmUnitRateInfo
                                                               {
                                                                   ID = u.Field<Guid>("ID"),
                                                                   UnitID = u.Field<Guid>("UnitID"),
                                                                   UnitName = u.Field<String>("UnitName"),
                                                                   Rate = u.Field<Decimal>("Rate"),
                                                                   SalesRate = u.Field<Decimal>("SalesRate"),
                                                                   ReserveRate = u.Field<Decimal>("Rate"),
                                                               }).ToList()

                                               }).SingleOrDefault();


                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 查询海运BaseInfo
        /// </summary>
        /// <returns></returns>
        private SearchOceanBaseInfo GetSearchOceanBaseInfoByInquirePrice(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchOceanBaseInfoByInquirePrice");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                SearchOceanBaseInfo results = (from b in ds.Tables[0].AsEnumerable()
                                               select new SearchOceanBaseInfo
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   OceanID = b.Field<Guid>("OceanID"),
                                                   BasePortNumber = b.Field<String>("BasePortNumber"),
                                                   OriginArbNumber = b.Field<String>("OriginArbNumber"),
                                                   DestArbNumber = b.Field<String>("DestArbNumber"),
                                                   POL = b.Field<String>("POLName"),
                                                   VIA = b.Field<String>("VIAName"),
                                                   POD = b.Field<String>("PODName"),
                                                   Delivery = b.Field<String>("DeliveryName"),
                                                   ContractNo = b.Field<String>("ContractNo"),
                                                   CLS = b.Field<String>("CLS"),
                                                   Surcharge = b.Field<String>("SurCharge"),
                                                   TT = b.Field<String>("TT"),
                                                   Term = b.Field<String>("Term"),
                                                   Remark = b.Field<String>("RemarkDetail"),
                                                   Updater = b.Field<String>("UpdateBy"),
                                                   Currency = b.Field<String>("Currency"),
                                                   Carrier = b.Field<String>("CarrierName"),
                                                   ItemCode = b.Field<String>("ItemCode"),
                                                   Commodity = b.Field<String>("Commodity"),
                                                   RemarkDetails = b.Field<String>("RemarkDetail"),
                                                   FilesCount = b.Field<Int32>("FilesCount"),

                                                   UnitList = (from u in ds.Tables[1].AsEnumerable()
                                                               where u.Field<Guid>("ParentID") == b.Field<Guid>("ID")
                                                               select new FrmUnitRateInfo
                                                               {
                                                                   ID = u.Field<Guid>("ID"),
                                                                   UnitID = u.Field<Guid>("UnitID"),
                                                                   UnitName = u.Field<String>("UnitName"),
                                                                   Rate = u.Field<Decimal>("Rate"),
                                                                   SalesRate = u.Field<Decimal>("Rate"),
                                                                   ReserveRate = u.Field<Decimal>("Rate"),
                                                               }).ToList()

                                               }).SingleOrDefault();

                results.Type = SearchRateType.Inquiry;

                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion


        #region 查询海运运价SearchOceanContractInfo
        /// <summary>
        /// 查询查询海运运价SearchOceanContractInfo
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="searchRateType">运价查询:区分合约、询价</param>
        /// <returns></returns>
        public SearchOceanContractInfo GetSearchOceanContractInfo(
                                Guid id,
                                 SearchRateType searchRateType)
        {
            try
            {
                if (searchRateType == SearchRateType.Inquiry)
                    return GetSearchOceanContractInfoByInquirePrice(id);
                else
                    return GetSearchOceanContractInfoByContract(id);
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 查询查询海运运价GetSearchOceanContractInfoByContract
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        private SearchOceanContractInfo GetSearchOceanContractInfoByContract(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchContractInfoByContract");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }


                SearchOceanContractInfo results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new SearchOceanContractInfo
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       Carrier = b.Field<String>("CarrierName"),
                                                       ContractNo = b.Field<String>("ContractNo"),
                                                       ShiplineName = b.Field<String>("ShippingName"),
                                                       Account = b.Field<String>("Account"),
                                                       AccountType = b.Field<Byte?>("AccountType") == null ? null : EnumHelper.GetDescription<AccountType>((AccountType)b.Field<Byte>("AccountType"), IsEnglish),
                                                       SCNList = (
                                                       from u in ds.Tables[1].AsEnumerable()
                                                       select new SCNInfo
                                                       {
                                                           ConsigneeName = u.Field<String>("ConsigneeName"),
                                                           ShipperName = u.Field<String>("ShipperName"),
                                                           NotifyName = u.Field<String>("NotifyPartyName")
                                                       }
                                                       ).ToList()
                                                   }).SingleOrDefault();


                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 查询查询海运运价SearchOceanContractInfoByInquirePrice
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        private SearchOceanContractInfo GetSearchOceanContractInfoByInquirePrice(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchContractInfoByInquirePrice");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }


                SearchOceanContractInfo results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new SearchOceanContractInfo
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       Carrier = b.Field<String>("CarrierName"),
                                                       ContractNo = b.Field<String>("ContractNo"),
                                                       ShiplineName = b.Field<String>("ShippingName"),
                                                       Account = b.Field<String>("Account"),
                                                       AccountType = string.Empty,
                                                       SCNList = new List<SCNInfo>(),
                                                   }).SingleOrDefault();


                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region UI数据

        #region 获得当前可用的船东、承运人
        /// <summary>
        /// 获得当前可用的船东/承运人
        /// </summary>
        /// <param name="shippingLineIDs">航线集合</param>
        /// <param name="statue">状态</param>
        /// <returns></returns>
        public Dictionary<Guid, string> GetOceanRateCarrierList(SearchPriceStatus statue, DateTime? begingDate, DateTime? endDate, Guid[] shippingLineIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetRatesCarrierListFromOcean");

                db.AddInParameter(dbCommand, "@Status", DbType.Byte, statue);
                db.AddInParameter(dbCommand, "@beginDate", DbType.DateTime, begingDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@ShippingLineIDs", DbType.String, shippingLineIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                Dictionary<Guid, String> list = (from b in ds.Tables[0].AsEnumerable()
                                                 select new
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     Name = b.Field<String>("Name")
                                                 }
                                                     ).ToDictionary(c => c.ID, c => c.Name); ;

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

        #region 获得当前可用的船东、承运人
        /// <summary>
        /// 获得当前可用的船东/承运人
        /// </summary>
        /// <param name="shippingLineIDs">航线集合</param>
        /// <param name="statue">状态</param>
        /// <returns></returns>
        public Dictionary<Guid, string> GetAirRateCarrierList(SearchPriceStatus statue, DateTime? beginDate, DateTime? endDate, Guid[] shippingLineIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetRatesCarrierListFromAir");

                db.AddInParameter(dbCommand, "@Status", DbType.Byte, statue);
                db.AddInParameter(dbCommand, "@beginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@ShippingLineIDs", DbType.String, shippingLineIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                Dictionary<Guid, String> list = (from b in ds.Tables[0].AsEnumerable()
                                                 select new
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     Name = b.Field<String>("Name")
                                                 }
                                                     ).ToDictionary(c => c.ID, c => c.Name); ;

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


        #region 获得当前可用的船东、承运人
        /// <summary>
        /// 获得当前可用的船东/承运人
        /// </summary>
        /// <param name="shippingLineIDs">航线集合</param>
        /// <param name="statue">状态</param>
        /// <returns></returns>
        public Dictionary<Guid, string> GetTruckRateCarrierList(SearchPriceStatus statue, DateTime? beginDate, DateTime? endDate, Guid[] shippingLineIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetRatesCarrierListFromTruck");

                db.AddInParameter(dbCommand, "@Status", DbType.Byte, statue);
                db.AddInParameter(dbCommand, "@ShippingLineIDs", DbType.String, shippingLineIDs.Join());
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                Dictionary<Guid, String> list = (from b in ds.Tables[0].AsEnumerable()
                                                 select new
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     Name = b.Field<String>("Name")
                                                 }
                                                     ).ToDictionary(c => c.ID, c => c.Name);

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


        #region 获得当前可用的POL、POD、Delivery
        /// <summary>
        /// 获得当前可用的POL、POD、Delivery
        /// </summary>
        /// <param name="shipperIDs">航线集合</param>
        /// <param name="carrierIDs">船东集合</param>
        /// <param name="statue">状态</param>
        /// <returns></returns>
        public List<SearchPortList> GetPortList(SearchPriceStatus statue, DateTime? begingDate, DateTime? endDate, Guid[] shipperIDs, Guid[] carrierIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanRatesPortList");

                db.AddInParameter(dbCommand, "@Status", DbType.Byte, statue);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, begingDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@ShippingLineIDs", DbType.String, shipperIDs.Join());
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, carrierIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SearchPortList> list = (from b in ds.Tables[0].AsEnumerable()
                                             select new SearchPortList
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 PortName = b.Field<String>("Name"),
                                                 Porttype = (PortType)b.Field<Int32>("PortType")
                                             }).ToList();

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

        #region 获得当前可用的Commodity
        /// <summary>
        /// 获得当前可用的Commodity
        /// </summary>
        /// <returns></returns>
        public Dictionary<Guid, string> GetCommodityList(SearchPriceStatus statue, DateTime? beginDate, DateTime? ednDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanRatesCommodityList");

                db.AddInParameter(dbCommand, "@Status", DbType.Byte, statue);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, ednDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                Dictionary<Guid, String> list = (from b in ds.Tables[0].AsEnumerable()
                                                 select new
                                                 {
                                                     ID = Guid.NewGuid(),
                                                     Name = b.Field<String>("Name")
                                                 }
                                                     ).ToDictionary(c => c.ID, c => c.Name); ;

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

        #endregion

        #endregion

        #region 获得海运运价查询列表

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="polIDs">POL ID集合</param>
        /// <param name="podIDs">POD ID集合</param>
        /// <param name="deliveryIDs">Delivery ID集合</param>
        /// <param name="commoditys">Commodity ID集合</param>
        /// <param name="contractNo">ContractNo</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <param name="permissionType">权限类型：1为通用;2为底价</param>
        /// <returns></returns>
        public PageList GetSearchOceanListByWebCRM(
            Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string[] polNames,
            string[] podNames,
            string[] deliveryNames,
            string[] finalDestinationNames,
            string[] commoditys,
            string contractNo,
             int pageSize,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            OceanTypeBySearch rateType,
            DataPageInfo dataPageInfo)
        {
            try
            {

                #region 根据港口名称找到ID

                List<Guid> polIDs = new List<Guid>();
                List<Guid> podIDs = new List<Guid>();
                List<Guid> deliveryIDs = new List<Guid>();
                List<Guid> finalDestinationIDs = new List<Guid>();

                #region 查询出港口的ID
                List<string> strList = new List<string>();
                foreach (string str in polNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                foreach (string str in podNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                foreach (string str in deliveryNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                foreach (string str in finalDestinationNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                #endregion

                List<PortNames> portName = new List<PortNames>();
                if (strList.Count() > 0)
                {
                    portName = _GeographyService.GetPortForName(strList.ToArray());
                }

                #region 用ID替换名称
                if (portName.Count > 0)
                {
                    foreach (string str in polNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !polIDs.Contains(pn.ID.Value))
                        {
                            polIDs.Add(pn.ID.Value);
                        }
                    }
                    foreach (string str in podNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !podIDs.Contains(pn.ID.Value))
                        {
                            podIDs.Add(pn.ID.Value);
                        }
                    }
                    foreach (string str in deliveryNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !deliveryIDs.Contains(pn.ID.Value))
                        {
                            deliveryIDs.Add(pn.ID.Value);
                        }
                    }
                    foreach (string str in finalDestinationNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !finalDestinationIDs.Contains(pn.ID.Value))
                        {
                            finalDestinationIDs.Add(pn.ID.Value);
                        }
                    }
                }

                #endregion


                #endregion

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchOceanRateList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@ShiplineIDs", DbType.String, shiplineIDs.Join());
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, carrierIDs.Join());
                db.AddInParameter(dbCommand, "@PolIDs", DbType.String, polIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@PodIDs", DbType.String, podIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@DeliveryIDs", DbType.String, deliveryIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@FinalDestinationIDs", DbType.String, finalDestinationIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@Commoditys", DbType.String, commoditys.Join());
                db.AddInParameter(dbCommand, "@ContractNo", DbType.String, contractNo);
                db.AddInParameter(dbCommand, "@DurationStart", DbType.DateTime, durationStart);
                db.AddInParameter(dbCommand, "@DurationEnd", DbType.DateTime, durationEnd);
                db.AddInParameter(dbCommand, "@Status", DbType.Int16, status);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, pageSize);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, rateType);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int16, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, "[" + dataPageInfo.SortByName + "] " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<SearchOceanRateList> results = BulidOceanRateListByDs(ds);

                foreach (var item in results)
                {
                    if (item.DurationFrom.Date <= DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date && item.DurationTo.Date >= DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date)
                    {
                        item.Statue = SearchPriceStatus.EFFECTIVE;
                    }
                    else if (item.DurationFrom.Date > DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date)
                    {
                        item.Statue = SearchPriceStatus.WILLBEEFFECTIVE;
                    }
                    else
                    {
                        item.Statue = SearchPriceStatus.EXPIRED;
                    }
                }

                dataPageInfo.TotalCount = ((from c in ds.Tables[2].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());
                dataPageInfo.PageSize = 500;
                // PageList<SearchOceanRateList> list = new PageList<SearchOceanRateList>(results, dataPageInfo);
                PageList list = PageList.Create<SearchOceanRateList>(results, dataPageInfo);
                return list;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="polIDs">POL ID集合</param>
        /// <param name="podIDs">POD ID集合</param>
        /// <param name="deliveryIDs">Delivery ID集合</param>
        /// <param name="commoditys">Commodity ID集合</param>
        /// <param name="contractNo">ContractNo</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <param name="permissionType">权限类型：1为通用;2为底价</param>
        /// <returns></returns>
        public PageList GetSearchOceanList(
            Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string[] polNames,
            string[] podNames,
            string[] deliveryNames,
            string[] finalDestinationNames,
            string[] commoditys,
            string contractNo,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            OceanTypeBySearch rateType,
            DataPageInfo dataPageInfo)
        {

            return GetSearchOceanListByWebCRM(userID, shiplineIDs, carrierIDs, polNames, podNames, deliveryNames, finalDestinationNames, commoditys, contractNo, 0
                , durationStart, durationEnd, status, rateType, dataPageInfo);

        }


        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>SearchOceanRateList</returns>
        public List<SearchOceanRateList> GetSearchOceanList(Guid[] ids)
        {
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    List<SearchOceanRateList> result = new List<SearchOceanRateList>();

                    List<SearchOceanRateList> result1 = GetSearchOceanListFromRates(ids);
                    List<SearchOceanRateList> result2 = GetSearchOceanListFromInquirePrice(ids);

                    if (result1 != null) result.AddRange(result1);

                    if (result2 != null) result.AddRange(result2);

                    scope.Complete();
                    return result;

                }
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>SearchOceanRateList</returns>
        private List<SearchOceanRateList> GetSearchOceanListFromRates(Guid[] ids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchOceanRateListByIDs");

                db.AddInParameter(dbCommand, "@IDS", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<SearchOceanRateList> results = BulidOceanRateListByDs(ds);
                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>SearchOceanRateList</returns>
        public List<SearchOceanRateList> GetSearchOceanListFromInquirePrice(Guid[] ids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchOceanRateListFromInquirePriceByIDs");

                db.AddInParameter(dbCommand, "@IDS", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<SearchOceanRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new SearchOceanRateList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         //OceanID = b.Field<Guid>("OceanID"),
                                                         //BasePortID = b.Field<Guid>("BasePortID"),
                                                         //OriginalArbitraryID = b.Field<Guid?>("OriginalArbitraryID"),
                                                         //DestinationArbitraryID = b.Field<Guid?>("DestinationArbitraryID"),
                                                         //OceanState = (Int32)b.Field<Byte>("State"),
                                                         //SearchrateType = (SearchRateType)b.Field<Byte>("SearchrateType"),
                                                         CarrierName = b.Field<String>("CarrierName"),
                                                         POLName = b.Field<String>("POLName"),
                                                         VIAName = b.Field<String>("VIAName"),
                                                         PODName = b.Field<String>("PODName"),
                                                         DeliveryName = b.Field<String>("DeliveryName"),
                                                         IsCheck = false,
                                                         Commodity = (string.IsNullOrEmpty(b.Field<String>("Commodity")) ?
                                                             string.Empty :
                                                             b.Field<String>("Commodity").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),

                                                         Term = b.Field<String>("Term"),
                                                         SurCharge = b.Field<String>("SurCharge"),
                                                         CLS = b.Field<String>("CLS"),
                                                         TT = b.Field<String>("TT"),
                                                         DurationFrom = b.Field<DateTime>("DurationFrom"),
                                                         DurationTo = b.Field<DateTime>("DurationTo"),
                                                         Description = b.Field<String>("Description"),
                                                         Currency = b.Field<String>("Currency"),
                                                         Remark = b.Field<String>("Remark"),
                                                         UnitList = (from u in ds.Tables[1].AsEnumerable()
                                                                     where u.Field<Guid>("ParentID") == b.Field<Guid>("ID")
                                                                     select new FrmUnitRateInfo
                                                                     {
                                                                         ID = u.Field<Guid>("ID"),
                                                                         UnitID = u.Field<Guid>("UnitID"),
                                                                         UnitName = u.Field<String>("UnitName"),
                                                                         ReserveRate = u.Field<Decimal>("Rate"),
                                                                         SalesRate = u.Field<Decimal>("SalesRate"),
                                                                         Rate = u.Field<Decimal>("SalesRate"),
                                                                         Currency = b.Field<String>("Currency"),
                                                                     }).ToList()
                                                     }).ToList();
                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        private List<SearchOceanRateList> BulidOceanRateListByDs(DataSet ds)
        {
            List<SearchOceanRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new SearchOceanRateList
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     //OceanID = b.Field<Guid>("OceanID"),
                                                     //BasePortID = b.Field<Guid>("BasePortID"),
                                                     //OriginalArbitraryID = b.Field<Guid?>("OriginalArbitraryID"),
                                                     //DestinationArbitraryID = b.Field<Guid?>("DestinationArbitraryID"),
                                                     //OceanState = (Int32)b.Field<Byte>("State"),
                                                     SearchrateType = (SearchRateType)b.Field<Byte>("SearchrateType"),
                                                     CarrierName = b.Field<String>("CarrierName"),
                                                     POLName = b.Field<String>("POLName"),
                                                     VIAName = b.Field<String>("VIAName"),
                                                     PODName = b.Field<String>("PODName"),
                                                     DeliveryName = b.Field<String>("DeliveryName"),
                                                     FinalDestinationName = b.Field<String>("FinalDestinationName"),
                                                     IsCheck = false,

                                                     Commodity = (string.IsNullOrEmpty(b.Field<String>("Commodity")) ?
                                                         string.Empty :
                                                         b.Field<String>("Commodity").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),

                                                     Term = b.Field<String>("Term"),
                                                     SurCharge = b.Field<String>("SurCharge"),
                                                     CLS = b.Field<String>("CLS"),
                                                     TT = b.Field<String>("TT"),
                                                     DurationFrom = b.Field<DateTime>("DurationFrom"),
                                                     DurationTo = b.Field<DateTime>("DurationTo"),
                                                     Description = b.Field<String>("Description"),
                                                     Currency = b.Field<String>("Currency"),
                                                     UnitList = (from u in ds.Tables[1].AsEnumerable()
                                                                 where u.Field<Guid>("ParentID") == b.Field<Guid>("ID")
                                                                 select new FrmUnitRateInfo
                                                                 {
                                                                     ID = u.Field<Guid>("ID"),
                                                                     UnitID = u.Field<Guid>("UnitID"),
                                                                     UnitName = u.Field<String>("UnitName"),
                                                                     ReserveRate = u.Field<Decimal>("Rate"),
                                                                     SalesRate = u.Field<Decimal>("SalesRate"),
                                                                     Rate = u.Field<Decimal>("SalesRate"),
                                                                 }).ToList(),

                                                     PROCount = (from n in ds.Tables[1].AsEnumerable()
                                                                 where b.Field<Guid>("ID") == n.Field<Guid>("ParentID")
                                                                 && (n.Field<Decimal>("SalesRate") != n.Field<Decimal>("Rate"))
                                                                 select n).Count()

                                                 }).ToList();
            return results;
        }

        #endregion

        #region 查询空运运价列表

        #region 获得空运运价查询列表
        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="pol">pol</param>
        /// <param name="pod">pod</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <param name="permissionType">权限类型：1为通用;2为底价</param>
        /// <returns></returns>
        public PageList GetSearchAirList(
            Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string pol,
            string pod,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            DataPageInfo dataPageInfo)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchAirRateList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@ShiplineIDs", DbType.String, shiplineIDs.Join());
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, carrierIDs.Join());
                db.AddInParameter(dbCommand, "@Pol", DbType.String, pol);
                db.AddInParameter(dbCommand, "@Pod", DbType.String, pod);
                db.AddInParameter(dbCommand, "@DurationStart", DbType.DateTime, durationStart);
                db.AddInParameter(dbCommand, "@DurationEnd", DbType.DateTime, durationEnd);
                db.AddInParameter(dbCommand, "@Status", DbType.Int16, status);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int16, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, "[" + dataPageInfo.SortByName + "] " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<SearchAirRateList> results = BulidAirRateListByDs(ds);

                foreach (var item in results) { item.Statue = status; }

                dataPageInfo.TotalCount = ((from c in ds.Tables[2].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());
                dataPageInfo.PageSize = 50;

                // PageList<SearchAirRateList> list = new PageList<SearchAirRateList>(results, dataPageInfo);
                PageList list = PageList.Create<SearchAirRateList>(results, dataPageInfo);
                return list;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获得空运运价查询列表
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>SearchAirRateList</returns>
        public List<SearchAirRateList> GetSearchAirList(Guid[] ids)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchAirRateListByIDs");

                db.AddInParameter(dbCommand, "@IDS", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<SearchAirRateList> results = BulidAirRateListByDs(ds);

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        private static List<SearchAirRateList> BulidAirRateListByDs(DataSet ds)
        {
            List<SearchAirRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new SearchAirRateList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   From = b.Field<string>("From"),
                                                   To = b.Field<string>("To"),
                                                   CarrierName = b.Field<string>("CarrierName"),
                                                   Commodity = (string.IsNullOrEmpty(b.Field<String>("Commodity")) ?
                                                     string.Empty :
                                                     b.Field<String>("Commodity").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),
                                                   Currency = b.Field<string>("Currency"),
                                                   DurationFrom = b.Field<DateTime>("DurationFrom"),
                                                   DurationTo = b.Field<DateTime>("DurationTo"),
                                                   Remark = b.Field<string>("Remark"),
                                                   RespondBy = b.Field<string>("RespondBy"),
                                                   Routing = b.Field<string>("Routing"),
                                                   Schedule = b.Field<string>("Schedule"),
                                                   UnitList = (from u in ds.Tables[1].AsEnumerable()
                                                               where u.Field<Guid>("InquirePriceID") == b.Field<Guid>("ID")
                                                               select new FrmUnitRateList
                                                               {
                                                                   ID = u.Field<Guid>("ID"),
                                                                   ParentID = u.Field<Guid>("InquirePriceID"),
                                                                   UnitID = u.Field<Guid>("UnitID"),
                                                                   UnitName = u.Field<String>("UnitName"),
                                                                   Rate = u.Field<Decimal>("Rate")
                                                               }).ToList(),
                                                   IsDirty = false
                                               }).ToList();
            return results;
        }


        #endregion

        #endregion

        #region 查询拖车运价列表

        /// <summary>
        /// 获得海运运价查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="shiplineIDs">航线ID集合</param>
        /// <param name="carrierIDs">船东ID集合</param>
        /// <param name="pol">pol</param>
        /// <param name="pod">pod</param>
        /// <param name="durationStart">DurationStart</param>
        /// <param name="durationEnd">durationEnd</param>
        /// <param name="status">状态</param>
        /// <param name="dataPageInfo">分页信息</param>
        /// <param name="permissionType">权限类型：1为通用;2为底价</param>
        /// <returns></returns>
        public PageList GetSearchTruckList(
            Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string pol,
            string pod,
            string zipcode,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            DataPageInfo dataPageInfo)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchTruckRateList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@ShiplineIDs", DbType.String, shiplineIDs.Join());
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, carrierIDs.Join());
                db.AddInParameter(dbCommand, "@Pol", DbType.String, pol);
                db.AddInParameter(dbCommand, "@Pod", DbType.String, pod);
                db.AddInParameter(dbCommand, "@ZipCode", DbType.String, zipcode);
                db.AddInParameter(dbCommand, "@DurationStart", DbType.DateTime, durationStart);
                db.AddInParameter(dbCommand, "@DurationEnd", DbType.DateTime, durationEnd);
                db.AddInParameter(dbCommand, "@Status", DbType.Int16, status);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int16, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, "[" + dataPageInfo.SortByName + "] " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<SearchTruckRateList> results = BulidTruckRateListByDs(ds);

                foreach (var item in results) { item.Statue = status; }

                dataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());
                dataPageInfo.PageSize = 50;

                PageList list = PageList.Create<SearchTruckRateList>(results, dataPageInfo);
                return list;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        private List<SearchTruckRateList> BulidTruckRateListByDs(DataSet ds)
        {
            List<SearchTruckRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new SearchTruckRateList
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     From = b.Field<string>("From"),
                                                     To = b.Field<string>("To"),
                                                     CarrierName = b.Field<string>("CarrierName"),
                                                     Currency = b.Field<string>("Currency"),
                                                     DurationFrom = b.Field<DateTime?>("DurationFrom"),
                                                     DurationTo = b.Field<DateTime?>("DurationTo"),
                                                     Remark = b.Field<string>("Remark"),
                                                     RespondBy = b.Field<string>("RespondBy"),
                                                     Rate = b.Field<decimal?>("Rate"),
                                                     FUEL = b.Field<decimal?>("FUEL"),
                                                     Total = b.Field<decimal>("Total"),
                                                     ZipCode = b.Field<string>("ZipCode"),
                                                     IsDirty = false
                                                 }).ToList();
            return results;
        }

        /// <summary>
        /// 获得空运运价查询列表
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>SearchTruckRateList</returns>
        public List<SearchTruckRateList> GetSearchTruckList(Guid[] ids)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchTruckRateListByIDs");

                db.AddInParameter(dbCommand, "@IDS", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<SearchTruckRateList> results = BulidTruckRateListByDs(ds);

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


        #endregion

        #region 导出海运运价
        /// <summary>
        /// 将一个合约下所有的运价明细导出到Excel
        /// </summary>
        /// <param name="oceanID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public OceanRateToExcel ExportOceanRateToExcelBySearchOcean(Guid userID,
            Guid[] shiplineIDs,
            Guid[] carrierIDs,
            string[] polNames,
            string[] podNames,
            string[] deliveryNames,
            string[] finalDestinationNames,
            string[] commoditys,
            string contractNo,
            DateTime? durationStart,
            DateTime? durationEnd,
            SearchPriceStatus status,
            OceanTypeBySearch rateType,
            DataPageInfo dataPageInfo)
        {

            try
            {

                #region 根据港口名称找到ID

                List<Guid> polIDs = new List<Guid>();
                List<Guid> podIDs = new List<Guid>();
                List<Guid> deliveryIDs = new List<Guid>();
                List<Guid> finalDestinationIDs = new List<Guid>();

                #region 查询出港口的ID
                List<string> strList = new List<string>();
                foreach (string str in polNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                foreach (string str in podNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                foreach (string str in deliveryNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                foreach (string str in finalDestinationNames)
                {
                    if (!strList.Contains(str))
                    {
                        strList.Add(str);
                    }
                }
                #endregion

                List<PortNames> portName = new List<PortNames>();
                if (strList.Count() > 0)
                {
                    portName = _GeographyService.GetPortForName(strList.ToArray());
                }

                #region 用ID替换名称
                if (portName.Count > 0)
                {
                    foreach (string str in polNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !polIDs.Contains(pn.ID.Value))
                        {
                            polIDs.Add(pn.ID.Value);
                        }
                    }
                    foreach (string str in podNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !podIDs.Contains(pn.ID.Value))
                        {
                            podIDs.Add(pn.ID.Value);
                        }
                    }
                    foreach (string str in deliveryNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !deliveryIDs.Contains(pn.ID.Value))
                        {
                            deliveryIDs.Add(pn.ID.Value);
                        }
                    }
                    foreach (string str in finalDestinationNames)
                    {
                        PortNames pn = portName.Find(p => p.OriginName == str);
                        if (pn != null && !pn.ID.IsNullOrEmpty() && !finalDestinationIDs.Contains(pn.ID.Value))
                        {
                            finalDestinationIDs.Add(pn.ID.Value);
                        }
                    }
                }

                #endregion


                #endregion

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchOceanRateList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@ShiplineIDs", DbType.String, shiplineIDs.Join());
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, carrierIDs.Join());
                db.AddInParameter(dbCommand, "@PolIDs", DbType.String, polIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@PodIDs", DbType.String, podIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@DeliveryIDs", DbType.String, deliveryIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@FinalDestinationIDs", DbType.String, finalDestinationIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@Commoditys", DbType.String, commoditys.Join());
                db.AddInParameter(dbCommand, "@ContractNo", DbType.String, contractNo);
                db.AddInParameter(dbCommand, "@DurationStart", DbType.DateTime, durationStart);
                db.AddInParameter(dbCommand, "@DurationEnd", DbType.DateTime, durationEnd);
                db.AddInParameter(dbCommand, "@Status", DbType.Int16, status);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, 0);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, rateType);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int16, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, "[" + dataPageInfo.SortByName + "] " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@IsRow2Col", DbType.Boolean, true);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);


                OceanRateToExcel toExcel = new OceanRateToExcel();

                if (ds == null || ds.Tables.Count < 1)
                {
                    toExcel.DataList = new List<OceanRateDataObject>();
                    toExcel.UnitList = new List<string>();
                    return toExcel;
                }

                #region linq

                List<string> unitList = (from b in ds.Tables[0].AsEnumerable()
                                         select b.Field<string>("UnitName")).ToList();

                List<OceanRateDataObject> results = (from b in ds.Tables[1].AsEnumerable()
                                                     select new OceanRateDataObject
                                                     {

                                                         CarrierName = b.Field<String>("CarrierName"),
                                                         POLName = b.Field<String>("POLName"),
                                                         VIAName = b.Field<String>("VIAName"),
                                                         PODName = b.Field<String>("PODName"),
                                                         DeliveryName = b.Field<String>("DeliveryName"),
                                                         ItemCode = b.Field<String>("ItemCode"),
                                                         Comm = b.Field<String>("Commodity"),
                                                         Term = b.Field<String>("Term"),
                                                         SurCharge = b.Field<String>("SurCharge"),
                                                         CLS = b.Field<String>("CLS"),
                                                         TT = b.Field<String>("TT"),
                                                         DurationForm = b.Field<DateTime?>("DurationFrom"),
                                                         DurationTo = b.Field<DateTime?>("DurationTo"),
                                                         CurrencyName = b.Field<String>("Currency"),
                                                         Description = b.Field<String>("Description"),


                                                         Rate_20FR = unitList.Contains("20FR") ? b.Field<Decimal?>("20FR") : 0,
                                                         Rate_20GP = unitList.Contains("20GP") ? b.Field<Decimal?>("20GP") : 0,
                                                         Rate_20HQ = unitList.Contains("20HQ") ? b.Field<Decimal?>("20HQ") : 0,
                                                         Rate_20HT = unitList.Contains("20HT") ? b.Field<Decimal?>("20HT") : 0,
                                                         Rate_20NOR = unitList.Contains("20NOR") ? b.Field<Decimal?>("20NOR") : 0,
                                                         Rate_20OT = unitList.Contains("20OT") ? b.Field<Decimal?>("20OT") : 0,
                                                         Rate_20RF = unitList.Contains("20RF") ? b.Field<Decimal?>("20RF") : 0,
                                                         Rate_20RH = unitList.Contains("20RH") ? b.Field<Decimal?>("20RH") : 0,
                                                         Rate_20TK = unitList.Contains("20TK") ? b.Field<Decimal?>("20TK") : 0,
                                                         Rate_40FR = unitList.Contains("40FR") ? b.Field<Decimal?>("40FR") : 0,
                                                         Rate_40GP = unitList.Contains("40GP") ? b.Field<Decimal?>("40GP") : 0,
                                                         Rate_40HQ = unitList.Contains("40HQ") ? b.Field<Decimal?>("40HQ") : 0,
                                                         Rate_40HT = unitList.Contains("40HT") ? b.Field<Decimal?>("40HT") : 0,
                                                         Rate_40NOR = unitList.Contains("40NOR") ? b.Field<Decimal?>("40NOR") : 0,
                                                         Rate_40OT = unitList.Contains("40OT") ? b.Field<Decimal?>("40OT") : 0,
                                                         Rate_40RF = unitList.Contains("40RF") ? b.Field<Decimal?>("40RF") : 0,
                                                         Rate_40RH = unitList.Contains("40RH") ? b.Field<Decimal?>("40RH") : 0,
                                                         Rate_40TK = unitList.Contains("40TK") ? b.Field<Decimal?>("40TK") : 0,
                                                         Rate_45FR = unitList.Contains("45FR") ? b.Field<Decimal?>("45FR") : 0,
                                                         Rate_45GP = unitList.Contains("45GP") ? b.Field<Decimal?>("45GP") : 0,
                                                         Rate_45HQ = unitList.Contains("45HQ") ? b.Field<Decimal?>("45HQ") : 0,
                                                         Rate_45HT = unitList.Contains("45HT") ? b.Field<Decimal?>("45HT") : 0,
                                                         Rate_45OT = unitList.Contains("45OT") ? b.Field<Decimal?>("45OT") : 0,
                                                         Rate_45RF = unitList.Contains("45RF") ? b.Field<Decimal?>("45RF") : 0,
                                                         Rate_45RH = unitList.Contains("45RH") ? b.Field<Decimal?>("45RH") : 0,
                                                         Rate_45TK = unitList.Contains("45TK") ? b.Field<Decimal?>("45TK") : 0,
                                                         Rate_53HQ = unitList.Contains("53HQ") ? b.Field<Decimal?>("53HQ") : 0,
                                                     }).ToList();
                #endregion

                toExcel.DataList = results;
                toExcel.UnitList = unitList;
                return toExcel;

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
    }
}

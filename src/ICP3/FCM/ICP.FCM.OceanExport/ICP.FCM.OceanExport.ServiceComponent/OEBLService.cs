using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Transactions;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    partial class OceanExportService
    {
        #region BL  List
        public List<OceanBLList> GetOceanBLListByOperationInfo(BusinessOperationContext context)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanBLListByOperationInfo");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, context.OperationID);
                db.AddInParameter(dbCommand, "@FormType", DbType.Int16, context.FormType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLList> results = new List<OceanBLList>();
                //得到集合中所有的MBL
                List<OceanBLList> MblList = BulidBLListByDataSet(ds).Where(n => n.BLType == FCMBLType.MBL).ToList();
                //得到集合中所有的HBL
                List<OceanBLList> HblList = BulidBLListByDataSet(ds).Where(n => n.BLType == FCMBLType.HBL).ToList();

                foreach (var mbl in MblList)
                {
                    results.Add(mbl);
                    //找到符合当前MBLID的HBL单据
                    var hList = (from hbl in HblList where hbl.MBLID == mbl.ID select hbl).ToList();
                    if (hList.Any())
                    {
                        results.AddRange(hList);
                    }

                }
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
        /// <param name="OceanBookID">业务ID</param>
        /// <returns>返回提单列表</returns>
        public List<OceanBLList> GetDeclareBLListByIds(Guid OceanBookID)
        {
            ArgumentHelper.AssertGuidNotEmpty(OceanBookID, "oceanBookingID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDeclareBLList");

                db.AddInParameter(dbCommand, "@OceanBookID", DbType.Guid, OceanBookID);
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
        /// <param name="telexNo"></param>
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
        /// <param name="overseasFilerID"></param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <param name="releasestatue"></param>
        /// <param name="releaseRCstate"></param>
        /// <param name="applyRelease"></param>
        /// <param name="receiveRCstate"></param>
        /// <param name="documnetState"></param>
        /// <returns>返回提单列表</returns>
        public string GetOceanBLList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string containerNo,
            string shippingOrderNo,
            string scno,
            string telexNo,
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
            DateSearchDispatchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords,
            ReleaseBLSearchStatue releasestatue,
            ReleaseRCSearchStatue releaseRCstate,
            ApplyReleaseSearchStatue applyRelease,
            ReceiveRCSearchStatue receiveRCstate,
            DocumentState? documnetState)
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
                db.AddInParameter(dbCommand, "@TelexNo", DbType.String, telexNo);
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
                db.AddInParameter(dbCommand, "@Releasestatue", DbType.Byte, releasestatue);
                db.AddInParameter(dbCommand, "@ReleaseRCstate", DbType.Byte, releaseRCstate);
                db.AddInParameter(dbCommand, "@ApplyRelease", DbType.Byte, applyRelease);
                db.AddInParameter(dbCommand, "@ReceiveRCstate", DbType.Byte, receiveRCstate);
                db.AddInParameter(dbCommand, "@DispatchState", DbType.Int16, documnetState);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLList> results = BulidBLListByDataSet(ds);


                return JSONSerializerHelper.SerializeToJson(results);
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
            List<OceanBLList> results = (ds.Tables[0].AsEnumerable().Select(b => new OceanBLList
                {
                    ID = b.Field<Guid>("ID"),
                    MBLID = b.IsNull("MBLID") ? Guid.Empty : b.Field<Guid>("MBLID"),
                    OceanBookingID = b.Field<Guid>("OceanBookingID"),
                    CompanyID = b.Field<Guid>("CompanyID"),
                    RefNo = b.Field<string>("RefNo"),
                    No = b.Field<string>("BLNo"),
                    ReleaseType =
                        b.IsNull("ReleaseType") ? FCMReleaseType.Unknown : (FCMReleaseType)b.Field<byte>("ReleaseType"),
                    CheckerName = b.Field<string>("CheckerName"),
                    ShipperName = b.Field<string>("ShipperName"),
                    ConsigneeName = b.Field<string>("ConsigneeName"),
                    AgentName = b.Field<string>("AgentName"),
                    BookingPartyID = b.Field<Guid?>("BookingPartyID"),
                    AgentOfCarrierID = b.Table.Columns.Contains("AgentOfCarrierID") ? b.Field<Guid?>("AgentOfCarrierID") : null,
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
                    BLTitleName = b.Field<string>("BLTitleName"),
                    ReleaseState = (FCMReleaseState)b.Field<byte>("ReleaseState"),
                    RBLA = b.Field<bool>("RBLA"),
                    RBLD = b.Field<bool>("RBLD"),
                    RBLRcv = b.Field<bool>("RBLRcv"),
                    BLRC = b.Field<bool>("BLRC"),
                    DispatchUpdateDate = b.Field<DateTime?>("DispatchUpdateDate"),
                    DocumentState =
                        b.IsNull("DispatchState")
                            ? DocumentState.Pending
                            : (DocumentState)b.Field<byte>("DispatchState"),
                    ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                    TelexNo = b.Field<string>("TelexNo"),
                    IsDirty = false,
                    IsToAgent = b.Field<bool>("IsToAgent"),
                    MBLD = b.IsNull("MBLD") ? (byte)1 : b.Field<byte>("MBLD"),
                    RBLH = b.IsNull("RBLH") ? false : b.Field<bool>("RBLH"),
                    //MAMS = b.IsNull("omAMS") ? (byte)1 : b.Field<byte>("omAMS"),
                    //HAMS = b.IsNull("ohAMS") ? false : b.Field<bool>("ohAMS"),
                    AMSState = b.IsNull("AMSState") ? BLAMSState.Unknown : (BLAMSState)b.Field<byte>("AMSState"),
                    MACI = b.IsNull("omACI") ? (byte)1 : b.Field<byte>("omACI"),
                    MISF = b.IsNull("omISF") ? (byte)1 : b.Field<byte>("omISF"),
                    MBLCFM = b.IsNull("omBLCFM") ? false : b.Field<bool>("omBLCFM"),
                    HISF = b.IsNull("ohISF") ? false : b.Field<bool>("ohISF"),
                    HACI = b.IsNull("ohACI") ? false : b.Field<bool>("ohACI"),
                    HBLCFM = b.IsNull("ohBLCFM") ? false : b.Field<bool>("ohBLCFM")
                })).ToList();
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
        /// <param name="operationNo"></param>
        /// <param name="mblNo"></param>
        /// <returns></returns>
        public OceanMBLInfo GetOceanMBLInfo(string operationNo, string mblNo)
        {
            ArgumentHelper.AssertStringNotEmpty(operationNo, "operationNo");
            ArgumentHelper.AssertStringNotEmpty(mblNo, "mblNo");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOceanMBLInfo_Email]");

                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@MBLNo", DbType.String, mblNo);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return ConvertTableToMBLInfo(ds.Tables[0]);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private OceanMBLInfo ConvertTableToMBLInfo(DataTable dt)
        {
            OceanMBLInfo result = (from b in dt.AsEnumerable()
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
                                       ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForNew>(b.Field<string>("ShipperDescription")),
                                       ConsigneeID = b.Field<Guid?>("ConsigneeID"),
                                       ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForNew>(typeof(CustomerDescriptionForNew), b.Field<string>("ConsigneeDescription")),
                                       NotifyPartyID = b.Field<Guid?>("NotifyPartyID"),
                                       NotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForNew>(typeof(CustomerDescriptionForNew), b.Field<string>("NotifyPartyDescription")),
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
                                       NBPODCode = b.Field<string>("NBPODCode"),
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
                                       NSITBLNotes = b.Field<string>("NSITBLNotes"),
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
                                       BLTitleID = b.Field<Guid?>("BLTitleID"),
                                       BLTitleName = b.Field<string>("BLTitleName"),
                                       IsDirty = false,
                                       IsCarrierSendAMS = b.Field<bool>("IsCarrierSendAMS"),
                                       BookingPartyID = b.Field<Guid?>("BookingPartyID"),
                                       BookingPartyName = b.Field<string>("BookingPartyName"),
                                       IsThirdPlacePayOrder = b.Field<bool>("IsThirdPlacePayOrder"),
                                       CollectbyAgentOrderID = b.Field<Guid?>("CollectbyAgentOrderID"),
                                       CollectbyAgentNameOrder = b.Field<string>("CollectbyAgentNameOrder"),
                                       ReallyShipper = b.Field<string>("ReallyShipper"),
                                       ReallyConsignee = b.Field<string>("ReallyConsignee"),
                                       ReallyNotifyParty = b.Field<string>("ReallyNotifyParty"),
                                       ReleaseBy = b.Field<string>("ReleaseBy"),
                                       TelexNo = b.Field<string>("TelexNo"),
                                       HSCODE = b.Field<string>("HSCODE"),
                                       Commodity = b.Field<string>("Commodity"),
                                       Container = b.Field<string>("Container"),
                                       NotifyParty2 = b.Field<string>("NotifyParty2"),
                                       HasFee = b.Field<bool>("HasFee"),
                                       CargoDescription = SerializerHelper.DeserializeFromString<SpclCargoDescription>(typeof(SpclCargoDescription), b.Field<string>("SpclCargoDescription")),
                                       GateInDate = b.Field<DateTime?>("GateInDate")
                                   }).SingleOrDefault();

            return result;
        }

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
                Stopwatch stopwatchMbl = Stopwatch.StartNew();
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
                _OperationLogService.Add(DateTime.Now, "GET-MBL-DB", string.Format("获取MBL ID[{0}]", id), stopwatchMbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                return ConvertTableToMBLInfo(ds.Tables[0]);
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

        

        private VGMInfo ConvertTableToVGMInfo(DataTable dt)
        {
            VGMInfo result = (from b in dt.AsEnumerable()
                              select new VGMInfo
                              {
                                  ID = b.Field<Guid>("ID"),
                                  OceanMblID = b.Field<Guid>("OceanMblID"),
                                  ResponsibleParty = b.Field<string>("ResponsibleParty"),
                                  ResponsiblePerson = b.Field<string>("ResponsiblePerson"),
                                  WeightSite = b.Field<Guid?>("WeightSite"),
                                  VerifiedPerson = b.Field<string>("VerifiedPerson"),
                                  VerifiedDate = b.Field<DateTime?>("VerifiedDate"),
                                  WeightDate = b.Field<DateTime?>("WeightDate"),
                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                  CreateBy = b.Field<Guid>("CreateBy"),
                                  UpdateBy = b.Field<Guid?>("UpdateBy"),
                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                  WeightSiteCode = b.Field<string>("siteCode"),
                                  WeightSiteName = b.Field<string>("siteName"),
                                  CreateByName = b.Field<string>("CreateByName"),
                                  UpdateByName = b.Field<string>("UpdateByName"),
                              }).SingleOrDefault();

            return result;
        }

        /// <summary>
        /// 获取VGM信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="mblid"></param>
        /// <returns>返回VGM信息</returns>
        public VGMInfo GetVGMInfo(Guid id, Guid mblid)
        {
            //ArgumentHelper.AssertGuidNotEmpty(id, "id");
            try
            {
                //Stopwatch stopwatchMbl = Stopwatch.StartNew();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetVGMInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@MBLId", DbType.Guid, mblid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                //OperationLogService.Add(DateTime.Now, "GET-MBL-DB", string.Format("获取MBL ID[{0}]", id), stopwatchMbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                return ConvertTableToVGMInfo(ds.Tables[0]);
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
        /// <param name="etd"></param>
        /// <param name="eta"></param>
        /// <param name="preETD"></param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ConfirmOnBoardType(
            Guid shippingOrderID,
            Guid? preVoyageID,
            Guid? voyageID,
            ConfirmOnBoardType onBoardType,
            Guid confirmByID,
            DateTime? shippingOrderUpdateDate,
            DateTime? etd,
            DateTime? eta,
            DateTime? preETD)
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
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, etd);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, eta);
                db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, preETD);

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

        private OceanHBLInfo ConvertTableToHBLInfo(DataSet ds)
        {
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
                                       AMSShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("AMSShipperDescription")),
                                       AMSConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("AMSConsigneeDescription")),
                                       AMSNotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("AMSNotifyPartyDescription")),
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
                                       BLTitleID = b.Field<Guid?>("BLTitleID"),
                                       BLTitleName = b.Field<string>("BLTitleName"),
                                       MBLUpdateDate = b.Field<DateTime?>("MBLUpdateDate"),

                                       //CSCLGateIn = b.Field<DateTime?>("CSCLGateIn"),
                                       IsToAgent = b.Field<bool>("IsToAgent"),
                                       IsDirty = false,
                                       BookingPartyID = b.Field<Guid?>("BookingPartyID"),
                                       BookingPartyName = b.Field<string>("BookingPartyName"),
                                       IsThirdPlacePayOrder = b.Field<bool>("IsThirdPlacePayOrder"),
                                       CollectbyAgentOrderID = b.Field<Guid?>("CollectbyAgentOrderID"),
                                       CollectbyAgentNameOrder = b.Field<string>("CollectbyAgentNameOrder"),
                                       ReleaseBy = b.Field<string>("ReleaseBy"),
                                       TelexNo = b.Field<string>("TelexNo"),
                                       ScacCode = b.Field<string>("ScacCode"),
                                       DeclareNo = b.Field<string>("DeclareNo"),
                                       GateInDate = b.Field<DateTime?>("GateInDate")
                                   }).SingleOrDefault();

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                result.BookingInfo = (from b in ds.Tables[1].AsEnumerable()
                                      select new OceanBookingInfo
                                      {
                                          CustomerID = b.Field<Guid>("CustomerID"),
                                          SalesTypeName = b.Field<string>("SalesTypeName"),
                                          CustomerName = b.Field<string>("CustomerName"),
                                          OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                          CarrierCode = b.Field<string>("CarrierCode"),
                                          SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                          TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                          PlaceOfDeliveryID = b.IsNull("PlaceOfDeliveryID") ? Guid.Empty : b.Field<Guid>("PlaceOfDeliveryID"),
                                          ShippingLineName = b.Field<string>("ShippingLineName"),
                                          ID = b.Field<Guid>("ID"),
                                          No = b.Field<string>("No"),
                                      }).SingleOrDefault();
            }
            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                result.BookingInfo.OceanMBLs = (from m in ds.Tables[2].AsEnumerable()
                                                select new BookingBLInfo
                                                {
                                                    OceanBookingID = m.Field<Guid>("OceanBookingID"),
                                                    ID = m.Field<Guid>("ID"),
                                                    NO = m.Field<string>("No"),
                                                    State = (OEBLState)m.Field<byte>("State"),
                                                    UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                }).ToList();
            }
            return result;
        }

        private DeclareHBLInfo ConvertTableToDeclareHBLInfo(DataSet ds)
        {
            DeclareHBLInfo result = (from b in ds.Tables[0].AsEnumerable()
                                     select new DeclareHBLInfo
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
                                         AMSShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("AMSShipperDescription")),
                                         AMSConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("AMSConsigneeDescription")),
                                         AMSNotifyPartyDescription = SerializerHelper.DeserializeFromString<CustomerDescriptionForAMS>(typeof(CustomerDescriptionForAMS), b.Field<string>("AMSNotifyPartyDescription")),
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
                                         BLTitleID = b.Field<Guid?>("BLTitleID"),
                                         BLTitleName = b.Field<string>("BLTitleName"),
                                         MBLUpdateDate = b.Field<DateTime?>("MBLUpdateDate"),

                                         IsToAgent = b.Field<bool>("IsToAgent"),
                                         IsDirty = false,
                                         BookingPartyID = b.Field<Guid?>("BookingPartyID"),
                                         BookingPartyName = b.Field<string>("BookingPartyName"),
                                         IsThirdPlacePayOrder = b.Field<bool>("IsThirdPlacePayOrder"),
                                         CollectbyAgentOrderID = b.Field<Guid?>("CollectbyAgentOrderID"),
                                         CollectbyAgentNameOrder = b.Field<string>("CollectbyAgentNameOrder"),
                                         ReleaseBy = b.Field<string>("ReleaseBy"),
                                         TelexNo = b.Field<string>("TelexNo"),
                                         ScacCode = b.Field<string>("ScacCode"),
                                         DeclareNo = b.Field<string>("DeclareNo"),
                                         HSCODE = b.Field<string>("HSCODE"),
                                         GateInDate = b.Field<DateTime?>("GateInDate")
                                     }).SingleOrDefault();

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                result.BookingInfo = (from b in ds.Tables[1].AsEnumerable()
                                      select new OceanBookingInfo
                                      {
                                          CustomerID = b.Field<Guid>("CustomerID"),
                                          SalesTypeName = b.Field<string>("SalesTypeName"),
                                          CustomerName = b.Field<string>("CustomerName"),
                                          OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                          CarrierCode = b.Field<string>("CarrierCode"),
                                          SalesTypeID = b.Field<Guid>("SalesTypeID"),
                                          TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                          PlaceOfDeliveryID = b.IsNull("PlaceOfDeliveryID") ? Guid.Empty : b.Field<Guid>("PlaceOfDeliveryID"),
                                          ShippingLineName = b.Field<string>("ShippingLineName"),
                                          ID = b.Field<Guid>("ID"),
                                          No = b.Field<string>("No"),
                                      }).SingleOrDefault();
            }
            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                result.BookingInfo.OceanMBLs = (from m in ds.Tables[2].AsEnumerable()
                                                select new BookingBLInfo
                                                {
                                                    OceanBookingID = m.Field<Guid>("OceanBookingID"),
                                                    ID = m.Field<Guid>("ID"),
                                                    NO = m.Field<string>("No"),
                                                    State = (OEBLState)m.Field<byte>("State"),
                                                    UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                }).ToList();
            }
            return result;
        }
        /// <summary>
        /// 获取HBL提单信息
        /// </summary>
        /// <param name="operationNo">业务号</param>
        /// <param name="hblNo">HBL号</param>
        /// <returns></returns>
        public OceanHBLInfo GetOceanHBLInfo(string operationNo, string hblNo)
        {
            ArgumentHelper.AssertStringNotEmpty(operationNo, "operationNo");
            ArgumentHelper.AssertStringNotEmpty(hblNo, "hblNo");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOceanHBLInfo_Email]");

                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@HBLNo", DbType.String, hblNo);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return ConvertTableToHBLInfo(ds);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
                Stopwatch stopwatchHbl = Stopwatch.StartNew();
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
                _OperationLogService.Add(DateTime.Now, "GET-HBL-DB", string.Format("获取HBL ID[{0}]", id), stopwatchHbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                return ConvertTableToHBLInfo(ds);
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
        /// 获取DeclareHBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回HBL提单信息</returns>
        public DeclareHBLInfo GetDeclareHBLInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Stopwatch stopwatchHbl = Stopwatch.StartNew();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDeclareHBLInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                _OperationLogService.Add(DateTime.Now, "GET-HBL-DB", string.Format("获取HBL ID[{0}]", id), stopwatchHbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                return ConvertTableToDeclareHBLInfo(ds);
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
        /// 获取发送给代理HBL提单ID列表
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回提单ID列表</returns>
        public List<OceanHBLInfo> GetOceanHBLToAgentHbls(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(string.Format("select * from fcm.OceanHBLs where oceanbookingid = '{0}' and IsToAgent =1", id.ToString()));

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<OceanHBLInfo> result = (from b in ds.Tables[0].AsEnumerable()
                                             select new OceanHBLInfo
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 No = b.Field<string>("No"),
                                             }).ToList();
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
        /// <param name="mblUpdateDate"></param>
        /// <param name="IsSynToMBL"></param>
        /// <param name="preETD"></param>
        /// <param name="ETD"></param>
        /// <param name="ETA"></param>
        /// <param name="IsToAgent"></param>
        /// <param name="IsBuildCSCLMemo"></param>
        /// <param name="bookingPartyID"></param>
        /// <param name="collectbyAgentOrderID"></param>
        /// <param name="isThirdPlacePayOrder"></param>
        /// <param name="scaccode"></param>
        /// <param name="DeclareNo"></param>
        /// <param name="GateInDate"></param>
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
            CustomerDescriptionForAMS amsShipperDescription,
            CustomerDescriptionForAMS amsConsigneeDescription,
            CustomerDescriptionForAMS amsNotifyPartyDescription,
            string isfNo,
            ACIEntryType? aciEntryType,
            AMSEntryType? amsEntryType,
            string woodPacking,
            IssueType issueType,
            //Guid? bLTitleID,
            Guid saveByID,
            DateTime? updateDate,
            DateTime? mblUpdateDate,
            bool IsSynToMBL,
            DateTime? preETD,
            DateTime? ETD,
            DateTime? ETA,
            //DateTime? CsclGateIn,
            bool IsToAgent,
            bool IsBuildCSCLMemo,
            Guid? bookingPartyID,
            Guid? collectbyAgentOrderID,
            bool isThirdPlacePayOrder,
            string scaccode,
            string DeclareNo,
            DateTime? GateInDate
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(transportClauseID, "transportClauseID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                SingleResult result = null;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    //保存MBL

                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanHBLInfo");

                    dbCommand.CommandTimeout = 60;

                    #region 添加参数
                    string tempshipperDescription = SerializerHelper.SerializeToString(shipperDescription, true, false);
                    string tempconsigneeDescription = SerializerHelper.SerializeToString(consigneeDescription, true, false);
                    string tempnotifyPartyDescription = SerializerHelper.SerializeToString(notifyPartyDescription, true, false);
                    string tempagentDescription = SerializerHelper.SerializeToString(agentDescription, true, false);
                    string tempamsShipperDescription = SerializerHelper.SerializeToString(amsShipperDescription, true, false);
                    string tempamsConsigneeDescription = SerializerHelper.SerializeToString(amsConsigneeDescription, true, false);
                    string tempamsNotifyPartyDescription = SerializerHelper.SerializeToString(amsNotifyPartyDescription, true, false);

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
                    db.AddInParameter(dbCommand, "@MBLUpdateDate", DbType.DateTime, mblUpdateDate);
                    db.AddInParameter(dbCommand, "@IsSyn", DbType.Boolean, IsSynToMBL);
                    db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, preETD);
                    db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, ETD);
                    db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, ETA);
                    db.AddInParameter(dbCommand, "@IsToAgent", DbType.Boolean, IsToAgent);
                    db.AddInParameter(dbCommand, "@IsBuildCSCLMemo", DbType.Boolean, IsBuildCSCLMemo);
                    db.AddInParameter(dbCommand, "@BookingPartyID", DbType.Guid, bookingPartyID);
                    db.AddInParameter(dbCommand, "@CollectbyAgentOrderID", DbType.Guid, collectbyAgentOrderID);
                    db.AddInParameter(dbCommand, "@IsThirdPlacePayOrder", DbType.Boolean, isThirdPlacePayOrder);
                    db.AddInParameter(dbCommand, "@SCACCode", DbType.String, scaccode);
                    db.AddInParameter(dbCommand, "@DeclareNo", DbType.String, DeclareNo);
                    db.AddInParameter(dbCommand, "@GateInDate", DbType.DateTime, GateInDate);


                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                    #endregion

                    Stopwatch stopwatchhbl = Stopwatch.StartNew();
                    result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "AMSNo", "OceanMBLID", "MBLUpdateDate" });
#if DEBUG

                    _FCMCommonService.SaveShipmentItemForCSP(
                        new SaveRequestShipmentItemForCSP()
                        {
                            OperationID = oceanBookingID,
                            OperationType = OperationType.OceanExport,
                            ID = result.GetValue<Guid>("ID"),
                            SaveBy = saveByID,
                            UpdateDate = DateTime.Now
                        });
#endif


                    _OperationLogService.Add(DateTime.Now, "SAVE-HBL-DB"
                        , string.Format("HBL ID[{0}];OceanBookingID[{1}]", result.GetValue<Guid>("ID"), oceanBookingID)
                                , stopwatchhbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
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
        /// 保存DeclareHBL信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="oceanBookingID">业务ID</param>
        /// <param name="mblID">MBLID</param>
        /// <param name="mblNO">MBL编号</param>
        /// <param name="hblNo">HBL编号</param>
        /// <param name="quantity">件数</param>
        /// <param name="quantityUnitID">件数单位</param>
        /// <param name="weight">重量</param>
        /// <param name="weightUnitID">重量单位</param>
        /// <param name="measurement">体积</param>
        /// <param name="measurementUnitID">体积单位</param>
        /// <param name="marks">唛头</param>
        /// <param name="goodsDescription">货描</param>
        /// <param name="isWoodPacking">是否木质包装</param>
        /// <param name="ctnQtyInfo"></param>
        /// <param name="woodPacking">木质包装描述</param>
        /// <param name="HSCODE"></param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="mblUpdateDate">MBL更新时间</param>
        /// <param name="IsSynToMBL">是否更新到MBL</param>
        /// <returns>返回值</returns>
        SingleResult SaveDeclareHBLInfo(
        Guid? id,
        Guid oceanBookingID,
        Guid mblID,
        string mblNO,
        string hblNo,
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
        string woodPacking,
        string HSCODE,
        Guid saveByID,
        DateTime? updateDate,
        DateTime? mblUpdateDate,
        bool IsSynToMBL
        )
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                //保存MBL

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveDeclareHBLInfo");

                dbCommand.CommandTimeout = 60;

                #region 添加参数

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@MblNo", DbType.String, mblNO);
                db.AddInParameter(dbCommand, "@HblNo", DbType.String, hblNo);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, quantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, weightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, measurementUnitID);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, marks);
                db.AddInParameter(dbCommand, "@GoodsDescription", DbType.String, goodsDescription);
                db.AddInParameter(dbCommand, "@IsWoodPacking", DbType.Boolean, isWoodPacking);
                db.AddInParameter(dbCommand, "@WoodPackingDescription", DbType.String, woodPacking);
                db.AddInParameter(dbCommand, "@HSCODE", DbType.String, HSCODE);
                db.AddInParameter(dbCommand, "@ctnQtyInfo", DbType.String, ctnQtyInfo);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);

                db.AddInParameter(dbCommand, "@MBLUpdateDate", DbType.DateTime, mblUpdateDate);
                db.AddInParameter(dbCommand, "@IsSyn", DbType.Boolean, IsSynToMBL);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                #endregion

                Stopwatch stopwatchhbl = Stopwatch.StartNew();
                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No", "OceanMBLID", "MBLUpdateDate" });
                _OperationLogService.Add(DateTime.Now, "SAVE-DeclareHBL-DB"
                    , string.Format("HBL ID[{0}];OceanBookingID[{1}]", result.GetValue<Guid>("ID"), oceanBookingID)
                            , stopwatchhbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
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
        /// 保存VGM信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="mblID">MBL ID</param>
        /// <param name="ResponsibleParty">责任方</param>
        /// <param name="ResponsiblePerson">责任人</param>
        /// <param name="WeightSite">称重地点</param>
        /// <param name="VerifiedPerson">核实人</param>
        /// <param name="VerifiedDate">核实时间</param>
        /// <param name="WeightDate"></param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <returns></returns>
        SingleResult SaveVGMInfo(
        Guid id,
        Guid mblID,
        string ResponsibleParty,
        string ResponsiblePerson,
        Guid? WeightSite,
        string VerifiedPerson,
        DateTime? VerifiedDate,
        DateTime? WeightDate,
        Guid saveByID,
        DateTime? updateDate
        )
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveVGMInfo");

                dbCommand.CommandTimeout = 60;

                #region 添加参数

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@OceanMBLID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@ResponsibleParty", DbType.String, ResponsibleParty);
                db.AddInParameter(dbCommand, "@ResponsiblePerson", DbType.String, ResponsiblePerson);
                db.AddInParameter(dbCommand, "@WeightSite", DbType.Guid, WeightSite);
                db.AddInParameter(dbCommand, "@VerifiedPerson", DbType.String, VerifiedPerson);
                db.AddInParameter(dbCommand, "@VerifiedDate", DbType.DateTime, VerifiedDate);
                db.AddInParameter(dbCommand, "@WeightDate", DbType.DateTime, WeightDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                #endregion

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate"});
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

        /// 删除DeclareHBL提单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveDeclareHBLInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveDeclareHBLInfo");

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

        /// <summary>
        /// 获取DeclareHBL单的箱列表 
        /// </summary>
        /// <param name="hblID">HBL ID</param>
        /// <returns>返回箱列表</returns>
        public List<DeclareBLContainerList> GetDeclareHBLContainerList(Guid hblID)
        {
            ArgumentHelper.AssertGuidNotEmpty(hblID, "hblID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDeclareHBLContainerList");

                db.AddInParameter(dbCommand, "@HblID", DbType.Guid, hblID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DeclareBLContainerList> results = BulidDeclareContainerListByDataSet(ds);

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
                                                      VGMCrossWeight = b.Field<decimal>("VGMCrossWeight"),
                                                      VGMMethod = b.Field<string>("VGMMethod"),
                                                      CTNOper = b.Field<string>("CTNOper"),
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

        private static List<DeclareBLContainerList> BulidDeclareContainerListByDataSet(DataSet ds)
        {
            List<DeclareBLContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DeclareBLContainerList
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
                                                        VGMCrossWeight = b.Field<decimal>("VGMCrossWeight"),
                                                        VGMMethod = b.Field<string>("VGMMethod"),
                                                        CTNOper = b.Field<string>("CTNOper"),
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
        /// <param name="relations"></param>
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
        /// <param name="containerVGMCrossWeights">箱VGM重量列表</param>
        /// <param name="containerVGMMethods">箱称重方式列表</param>
        /// <param name="containerCTNOpers">箱主列表</param>
        /// <param name="containerMeasurements">箱体积列表</param>
        /// <param name="containerIsSOCs">是否客户自有箱列表</param>
        /// <param name="containerIsPartOfs">是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">OceanContainers更新时间-做数据版本用</param>
        /// <param name="cargoUpdateDates">货物更新时间-做数据版本用</param>
        /// <param name="IsSynToHBL"></param>
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
            decimal[] containerVGMCrossWeights,
            string[] containerVGMMethods,
            string[] containerCTNOpers,
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
                 containerIsPartOfs,
                 cargoUpdateDates,
                 containerVGMCrossWeights,
                 containerVGMMethods,
                 containerCTNOpers);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanMBLContainerInfo");

                dbCommand.CommandTimeout = 60;

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
                string tempcontainerVGMCrossWeights = containerVGMCrossWeights.Join();
                string tempcontainerVGMMethods = containerVGMMethods.Join();
                string tempcontainerCTNOpers = containerCTNOpers.Join();
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
                db.AddInParameter(dbCommand, "@VGMCrossWeights", DbType.String, tempcontainerVGMCrossWeights);
                db.AddInParameter(dbCommand, "@VGMMethods", DbType.String, tempcontainerVGMMethods);
                db.AddInParameter(dbCommand, "@CTNOpers", DbType.String, tempcontainerCTNOpers);
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
        /// <param name="relations"></param>
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
        /// <param name="containerVGMCrossWeights">箱VGM重量列表</param>
        /// <param name="containerVGMMethods">箱称重方式列表</param>
        /// <param name="containerCTNOpers">箱主列表</param>
        /// <param name="containerMeasurements">箱体积列表</param>
        /// <param name="containerIsSOCs">是否客户自有箱列表</param>
        /// <param name="containerIsPartOfs">是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">OceanContainers更新时间-做数据版本用</param>
        /// <param name="cargoUpdateDates">货物更新时间-做数据版本用</param>
        /// <param name="IsSynToMBL"></param>
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
            decimal[] containerVGMCrossWeights,
            string[] containerVGMMethods,
            string[] containerCTNOpers,
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
                 cargoUpdateDates,
                 containerVGMCrossWeights,
                 containerVGMMethods,
                 containerCTNOpers);

            try
            {
                ManyResult result = null;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanHBLContainerInfo");

                    dbCommand.CommandTimeout = 60;

                    #region Parameter
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
                    string tempcontainerVGMCrossWeights = containerVGMCrossWeights.Join();
                    string tempcontainerVGMMethods = containerVGMMethods.Join();
                    string tempcontainerCTNOpers = containerCTNOpers.Join();
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
                    db.AddInParameter(dbCommand, "@VGMCrossWeights", DbType.String, tempcontainerVGMCrossWeights);
                    db.AddInParameter(dbCommand, "@VGMMethods", DbType.String, tempcontainerVGMMethods);
                    db.AddInParameter(dbCommand, "@CTNOpers", DbType.String, tempcontainerCTNOpers);
                    db.AddInParameter(dbCommand, "@Measurements", DbType.String, tempcontainerMeasurements);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@IsSyn", DbType.Boolean, IsSynToMBL);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                    db.AddInParameter(dbCommand, "@CargoUpdateDates", DbType.String, tempcargoUpdateDates); 
                    #endregion

                    result = db.ManyResult(dbCommand, new string[] { "ID", "CargoID", "UpdateDate", "CargoUpdateDate", "MBLUpdateDate" });
#if DEBUG
                    _FCMCommonService.SaveShipmentContainerForCSP(new SaveRequestShipmentContainerForCSP()
                    {
                        OperationID = oceanBookingID,
                        OperationType = OperationType.OceanExport,
                        OperationContainerIDs = result.Items.Select(fItem => fItem.GetValue<Guid>("ID")).ToArray(),
                        SaveBy = saveByID,
                        UpdateDate = DateTime.Now
                    });
                    _FCMCommonService.SaveShipmentItemContainerForCSP(new SaveRequestShipmentItemContainerForCSP()
                    {
                        OperationID = oceanBookingID,
                        OperationType = OperationType.OceanExport,
                        BLID = hblID,
                        OperationContainerIDs = result.Items.Select(fItem => fItem.GetValue<Guid>("ID")).ToArray(),
                        BLContainerIDs = result.Items.Select(fItem => fItem.GetValue<Guid>("CargoID")).ToArray(),
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
        /// 保存DeclareHBL的箱信息
        /// </summary>
        /// <param name="oceanBookingID">订舱ID</param>
        /// <param name="hblID">HBL ID</param>
        /// <param name="relations"></param>
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
        /// <param name="containerVGMCrossWeights">箱VGM重量列表</param>
        /// <param name="containerVGMMethods">箱称重方式列表</param>
        /// <param name="containerCTNOpers">箱主列表</param>
        /// <param name="containerMeasurements">箱体积列表</param>
        /// <param name="containerIsSOCs">是否客户自有箱列表</param>
        /// <param name="containerIsPartOfs">是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">OceanContainers更新时间-做数据版本用</param>
        /// <param name="cargoUpdateDates">货物更新时间-做数据版本用</param>
        /// <param name="IsSynToMBL"></param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveDeclareHBLContainerInfo(
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
            decimal[] containerVGMCrossWeights,
            string[] containerVGMMethods,
            string[] containerCTNOpers,
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
                 cargoUpdateDates,
                 containerVGMCrossWeights,
                 containerVGMMethods,
                 containerCTNOpers);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveDeclareHBLContainerInfo");

                dbCommand.CommandTimeout = 60;

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
                string tempcontainerVGMCrossWeights = containerVGMCrossWeights.Join();
                string tempcontainerVGMMethods = containerVGMMethods.Join();
                string tempcontainerCTNOpers = containerCTNOpers.Join();
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
                db.AddInParameter(dbCommand, "@DeclareContainerSOs", DbType.String, tempcontainerSOs);
                db.AddInParameter(dbCommand, "@ContainerTypeIDs", DbType.String, tempcontainerTypeIDs);
                db.AddInParameter(dbCommand, "@DeclareContainerSealNos", DbType.String, tempcontainerSealNos);
                db.AddInParameter(dbCommand, "@ContainerIsSOCs", DbType.String, tempcontainerIsSOCs);
                db.AddInParameter(dbCommand, "@ContainerIsPartOfs", DbType.String, tempcontainerIsPartOfs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempupdateDates);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, tempcontainerMarks);
                db.AddInParameter(dbCommand, "@Commodities", DbType.String, tempcontainerCommoditys);
                db.AddInParameter(dbCommand, "@Quantities", DbType.String, tempcontainerQuantitys);
                db.AddInParameter(dbCommand, "@Weights", DbType.String, tempcontainerWeights);
                db.AddInParameter(dbCommand, "@VGMCrossWeights", DbType.String, tempcontainerVGMCrossWeights);
                db.AddInParameter(dbCommand, "@VGMMethods", DbType.String, tempcontainerVGMMethods);
                db.AddInParameter(dbCommand, "@CTNOpers", DbType.String, tempcontainerCTNOpers);
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

        #region AMS State
        /// <summary>
        /// 确认AMS费用
        /// </summary>
        /// <param name="hblids">提单ID集合</param>
        /// <param name="updateBy">更新人</param>
        public bool ConfirmedAMS(Guid[] hblids, Guid updateBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspConfirmedAMS");

                db.AddInParameter(dbCommand, "@HBLIDs", DbType.String, hblids.Join());
                db.AddInParameter(dbCommand, "@UpdateBy", DbType.Guid, updateBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.ExecuteNonQuery(dbCommand);
                return true;
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
        /// <param name="ctnIDList"></param>
        /// <param name="ctnDateList"></param>
        /// <param name="isSynToHBL"></param>
        /// <returns></returns>
        public SingleResult SaveOceanMBLAndContainerWithTrans(SaveMBLInfoParameter blParameter, SaveBLContainerParameter ctnParameter, List<Guid> ctnIDList, List<DateTime?> ctnDateList, bool isSynToHBL)
        {
            Stopwatch stopwatchTotal = Stopwatch.StartNew();
            stopwatchTotal.Start();

            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            SingleResult result = new SingleResult();

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {

                SingleResult blReslut = new SingleResult();
                System.Text.StringBuilder operationLog = new System.Text.StringBuilder();

                try
                {
                    operationLog.Append("以事务方式保存MBL");
                    blReslut = SaveOceanMBLInfo(blParameter);
                    operationLog.AppendFormat("MBL ID[{0}]OceanBookingID[{1}][{2}ms]", blReslut.GetValue<Guid>("ID"), blParameter.OceanBookingID, stopwatchTotal.ElapsedMilliseconds);
                    //删除箱列表
                    if (ctnIDList != null && ctnIDList.Count > 0)
                    {
                        Stopwatch stopwatRemovectn = Stopwatch.StartNew();
                        RemoveOceanContaierInfo(ctnIDList.ToArray(), blParameter.SaveByID, ctnDateList.ToArray());
                        operationLog.AppendFormat("删除箱{0}", stopwatRemovectn.ElapsedMilliseconds);
                    }

                    //保存箱列表
                    if (ctnParameter != null)
                    {
                        Stopwatch stopwatchctn = Stopwatch.StartNew();
                        ctnParameter.blID = blReslut.GetValue<Guid>("ID");
                        result.Add("ContainerResult", SaveOceanMBLContainerInfo(ctnParameter, isSynToHBL));
                        result.Add("BLResult", blReslut);
                        operationLog.AppendFormat("保存箱{0}", stopwatchctn.ElapsedMilliseconds);
                    }
                    operationLog.Append("保存完成");
                    _OperationLogService.Add(DateTime.Now, "SAVE-MBL-DB", operationLog.ToString(), stopwatchTotal.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    operationLog.Append("保存失败");
                    _OperationLogService.Add(DateTime.Now, "SAVE-MBL-DB"
                        , operationLog.ToString(), stopwatchTotal.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 以事务的方式保存HBL跟箱信息
        /// </summary>
        /// <param name="blParameter"></param>
        /// <param name="ctnParameter"></param>
        /// <param name="ctnIDList"></param>
        /// <param name="ctnDateList"></param>
        /// <param name="IsSynToMBL"></param>
        /// <returns></returns>
        public SingleResult SaveOceanHBLAndContainerWithTrans(SaveHBLInfoParameter blParameter, SaveBLContainerParameter ctnParameter, List<Guid> ctnIDList, List<DateTime?> ctnDateList, bool IsSynToMBL)
        {
            Stopwatch stopwatchTotal = Stopwatch.StartNew();
            stopwatchTotal.Start();
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                SingleResult result = new SingleResult();
                System.Text.StringBuilder operationLog = new System.Text.StringBuilder();
                try
                {
                    //保存HBL
                    operationLog.Append("以事务方式保存HBL:");

                    SingleResult blReslut = SaveOceanHBLInfo(blParameter, IsSynToMBL);
                    if (ctnParameter != null)
                    {
                        ctnParameter.blID = blReslut.GetValue<Guid>("ID");
                    }
                    operationLog.AppendFormat("ID[{0}]NO[{1}]", blReslut.GetValue<Guid>("ID"), blReslut.GetValue<string>("No"));
                    //删除箱列表
                    if (ctnIDList != null && ctnIDList.Count > 0)
                    {
                        Stopwatch stopwatRemovectn = Stopwatch.StartNew();
                        RemoveOceanContaierInfo(ctnIDList.ToArray(), blParameter.saveByID, ctnDateList.ToArray());
                        operationLog.AppendFormat("删除箱[{0}ms]", stopwatRemovectn.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    }

                    //保存箱列表
                    if (ctnParameter != null)
                    {
                        Stopwatch stopwatchctn = Stopwatch.StartNew();
                        ctnParameter.blID = blReslut.GetValue<Guid>("ID");
                        result.Add("ContainerResult", SaveOceanHBLContainerInfo(ctnParameter, IsSynToMBL));
                        operationLog.AppendFormat("保存箱[{0}ms]", stopwatchctn.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    }

                    result.Add("BLResult", blReslut);

                    operationLog.Append("保存完成");
                    _OperationLogService.Add(DateTime.Now, "SAVE-HBL-DB"
                        , operationLog.ToString(), stopwatchTotal.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    operationLog.Append("保存失败");
                    _OperationLogService.Add(DateTime.Now, "SAVE-HBL-DB"
                        , operationLog.ToString(), stopwatchTotal.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    throw ex;
                }
                return result;
            }
        }

        /// <summary>
        /// 以事务的方式保存DeclareHBL跟箱信息
        /// </summary>
        /// <param name="blParameter"></param>
        /// <param name="ctnParameter"></param>
        /// <param name="ctnIDList"></param>
        /// <param name="ctnDateList"></param>
        /// <param name="IsSynToMBL"></param>
        /// <returns></returns>
        public SingleResult SaveDeclareHBLAndContainerWithTrans(SaveDeclareHBLInfoParameter blParameter, SaveBLContainerParameter ctnParameter, List<Guid> ctnIDList, List<DateTime?> ctnDateList, bool IsSynToMBL)
        {
            Stopwatch stopwatchTotal = Stopwatch.StartNew();
            stopwatchTotal.Start();
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                SingleResult result = new SingleResult();
                System.Text.StringBuilder operationLog = new System.Text.StringBuilder();
                try
                {
                    //保存HBL
                    operationLog.Append("以事务方式保存HBL:");

                    SingleResult blReslut = SaveDeclareHBLInfo(blParameter, IsSynToMBL);
                    if (ctnParameter != null)
                    {
                        ctnParameter.blID = blReslut.GetValue<Guid>("ID");
                    }
                    operationLog.AppendFormat("ID[{0}]NO[{1}]", blReslut.GetValue<Guid>("ID"), blReslut.GetValue<string>("No"));
                    //删除箱列表
                    if (ctnIDList != null && ctnIDList.Count > 0)
                    {
                        Stopwatch stopwatRemovectn = Stopwatch.StartNew();
                        RemoveDeclareContaierInfo(ctnIDList.ToArray(), blParameter.saveByID, ctnDateList.ToArray());
                        operationLog.AppendFormat("删除箱[{0}ms]", stopwatRemovectn.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    }
                    //保存箱列表
                    if (ctnParameter != null)
                    {
                        Stopwatch stopwatchctn = Stopwatch.StartNew();
                        ctnParameter.blID = blReslut.GetValue<Guid>("ID");
                        result.Add("ContainerResult", SaveDeclareHBLContainerInfo(ctnParameter, IsSynToMBL));
                        operationLog.AppendFormat("保存箱[{0}ms]", stopwatchctn.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    }
                    result.Add("BLResult", blReslut);
                    operationLog.Append("保存完成");
                    _OperationLogService.Add(DateTime.Now, "SAVE-DeclareHBL-DB"
                        , operationLog.ToString(), stopwatchTotal.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    operationLog.Append("保存失败");
                    _OperationLogService.Add(DateTime.Now, "SAVE-DeclareHBL-DB"
                        , operationLog.ToString(), stopwatchTotal.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    throw ex;
                }
                return result;
            }
        }

        #region 转换保存

        /// <summary>
        /// 保存MBL的箱信息
        /// </summary>
        /// <param name="parameter">保存MBL的参数对象</param>
        /// <returns></returns>
        public SingleResult SaveOceanMBLInfo(SaveMBLInfoParameter parameter)
        {

            ArgumentHelper.AssertGuidNotEmpty(parameter.OceanBookingID, "oceanShippingOrderID");
            ArgumentHelper.AssertGuidNotEmpty(parameter.TransportClauseID, "transportClauseID");
            ArgumentHelper.AssertGuidNotEmpty(parameter.QuantityUnitID, "quantityUnitID");
            ArgumentHelper.AssertGuidNotEmpty(parameter.MeasurementUnitID, "measurementUnitID");
            ArgumentHelper.AssertGuidNotEmpty(parameter.WeightUnitID, "weightUnitID");
            ArgumentHelper.AssertGuidNotEmpty(parameter.SaveByID, "saveByID");

            try
            {
                //保存MBL
                Stopwatch stopwatchMbl = Stopwatch.StartNew();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanMBLInfo");

                dbCommand.CommandTimeout = 60;

                string tempagentDescription = SerializerHelper.SerializeToString(parameter.AgentDescription, true, false);
                string tempshipperDescription = SerializerHelper.SerializeToString(parameter.ShipperDescription, true, false);
                string tempconsigneeDescription = SerializerHelper.SerializeToString(parameter.ConsigneeDescription, true, false);
                string tempnotifyPartyDescription = SerializerHelper.SerializeToString(parameter.NotifyPartyDescription, true, false);
                string tempCargoDescription = SerializerHelper.SerializeToString(parameter.CargoDescription, true, false);
                #region 添加参数
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, parameter.ID);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, parameter.OceanBookingID);
                db.AddInParameter(dbCommand, "@MblNo", DbType.String, parameter.MBLNo);
                db.AddInParameter(dbCommand, "@NumberOfOriginal", DbType.Int32, parameter.NumberOfOriginal);
                db.AddInParameter(dbCommand, "@VoyageShowType", DbType.Int16, parameter.VoyageShowType);
                db.AddInParameter(dbCommand, "@CheckerID", DbType.Guid, parameter.CheckerID);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, parameter.ShipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                db.AddInParameter(dbCommand, "@ConsigneeID", DbType.Guid, parameter.ConsigneeID);
                db.AddInParameter(dbCommand, "@ConsigneeDescription", DbType.Xml, tempconsigneeDescription);
                db.AddInParameter(dbCommand, "@NotifyPartyID", DbType.Guid, parameter.NotifyPartyID);
                db.AddInParameter(dbCommand, "@NotifyPartyDescription", DbType.Xml, tempnotifyPartyDescription);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, parameter.AgentID);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.Xml, tempagentDescription);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptID", DbType.Guid, parameter.PlaceOfReceiptID);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptName", DbType.String, parameter.PlaceOfReceiptName);
                db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, parameter.PreVoyageID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, parameter.VoyageID);
                db.AddInParameter(dbCommand, "@POLID", DbType.Guid, parameter.POLID);
                db.AddInParameter(dbCommand, "@POLName", DbType.String, parameter.POLName);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, parameter.PODID);
                db.AddInParameter(dbCommand, "@PODName", DbType.String, parameter.PODName);
                db.AddInParameter(dbCommand, "@NBPODCode", DbType.String, parameter.NBPODCode);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryID", DbType.Guid, parameter.PlaceOfDeliveryID);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryName", DbType.String, parameter.PlaceOfDeliveryName);
                db.AddInParameter(dbCommand, "@FinalDestinationID", DbType.Guid, parameter.FinalDestinationID);
                db.AddInParameter(dbCommand, "@FinalDestinationName", DbType.String, parameter.FinalDestinationName);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, parameter.TransportClauseID);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, parameter.PaymentTermID);
                db.AddInParameter(dbCommand, "@FreightDescription", DbType.String, parameter.FreightDescription);
                db.AddInParameter(dbCommand, "@NSITBLNotes", DbType.String, parameter.NSITBLNotes);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Int16, parameter.ReleaseType);
                db.AddInParameter(dbCommand, "@ReleaseDate", DbType.DateTime, parameter.ReleaseDate);
                db.AddInParameter(dbCommand, "@Quantity", DbType.Int32, parameter.Quantity);
                db.AddInParameter(dbCommand, "@QuantityUnitID", DbType.Guid, parameter.QuantityUnitID);
                db.AddInParameter(dbCommand, "@Weight", DbType.Decimal, parameter.Weight);
                db.AddInParameter(dbCommand, "@WeightUnitID", DbType.Guid, parameter.WeightUnitID);
                db.AddInParameter(dbCommand, "@Measurement", DbType.Decimal, parameter.Measurement);
                db.AddInParameter(dbCommand, "@MeasurementUnitID", DbType.Guid, parameter.MeasurementUnitID);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, parameter.Marks);
                db.AddInParameter(dbCommand, "@GoodsDescription", DbType.String, parameter.GoodsDescription);
                db.AddInParameter(dbCommand, "@IsWoodPacking", DbType.Boolean, parameter.IsWoodPacking);
                db.AddInParameter(dbCommand, "@IssuePlaceID", DbType.Guid, parameter.IssuePlaceID);
                db.AddInParameter(dbCommand, "@IssueByID", DbType.Guid, parameter.IssueByID);
                db.AddInParameter(dbCommand, "@IssueDate", DbType.DateTime, parameter.IssueDate);
                db.AddInParameter(dbCommand, "@WoodPackingDescription", DbType.String, parameter.WoodPacking);
                db.AddInParameter(dbCommand, "@ctnQtyInfo", DbType.String, parameter.CTNQtyInfo);
                db.AddInParameter(dbCommand, "@AgentText", DbType.String, parameter.AgentText);
                db.AddInParameter(dbCommand, "@IssueType", DbType.Int16, parameter.IssueType);
                db.AddInParameter(dbCommand, "@FreightRateID", DbType.Guid, parameter.FreightRateID);

                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, parameter.ETA);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, parameter.ETD);
                db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, parameter.PreETD);

                db.AddInParameter(dbCommand, "@IsCarrierSendAMS", DbType.Boolean, parameter.IsCarrierSendAMS);
                db.AddInParameter(dbCommand, "@BookingPartyID", DbType.Guid, parameter.BookingPartyID);
                db.AddInParameter(dbCommand, "@CollectbyAgentOrderID", DbType.Guid, parameter.CollectbyAgentOrderID);
                db.AddInParameter(dbCommand, "@IsThirdPlacePayOrder", DbType.Boolean, parameter.IsThirdPlacePayOrder);
                db.AddInParameter(dbCommand, "@ReallyShipper", DbType.String, parameter.ReallyShipper);
                db.AddInParameter(dbCommand, "@ReallyConsignee", DbType.String, parameter.ReallyConsignee);
                db.AddInParameter(dbCommand, "@ReallyNotifyParty", DbType.String, parameter.ReallyNotifyParty);
                db.AddInParameter(dbCommand, "@GateInDate", DbType.DateTime, parameter.GateInDate);
                db.AddInParameter(dbCommand, "@HSCODE", DbType.String, parameter.HSCODE);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, parameter.Commodity);
                db.AddInParameter(dbCommand, "@Container", DbType.String, parameter.Container);
                db.AddInParameter(dbCommand, "@NotifyParty2", DbType.String, parameter.NotifyParty2);
                db.AddInParameter(dbCommand, "@Hasfee", DbType.Boolean, parameter.HasFee);
                db.AddInParameter(dbCommand, "@CargoDescription", DbType.Xml, tempCargoDescription);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, parameter.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, parameter.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                #endregion


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

                _OperationLogService.Add(DateTime.Now, "SAVE-MBL-DB"
                            , string.Format("保存完成;MBL ID[{0}];OceanBookingID[{1}]", result.GetValue<Guid>("ID"), parameter.OceanBookingID)
                            , stopwatchMbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
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
                //parameter.bLTitleID,
                                    parameter.saveByID,
                                    parameter.updateDate,
                                    parameter.mblUpdateDate,
                                    IsSynToMBL,
                                    parameter.PreETD,
                                    parameter.ETD,
                                    parameter.ETA,
                // parameter.CSCLGateIn,
                                    parameter.IsToAgent,
                                    parameter.IsBuildCSCLMemo,
                                    parameter.bookingPartyID,
                                    parameter.collectbyAgentOrderID,
                                    parameter.isThirdPlacePayOrder,
                                    parameter.scacCode,
                                    parameter.DeclareNo,
                                    parameter.GateInDate
                                    );
        }

        /// <summary>
        /// 保存HBL的箱信息
        /// </summary>
        public SingleResult SaveDeclareHBLInfo(SaveDeclareHBLInfoParameter parameter, bool IsSynToMBL)
        {
            return SaveDeclareHBLInfo(
                                    parameter.id,
                                    parameter.oceanBookingID,
                                    parameter.mblID,
                                    parameter.mblNO,
                                    parameter.hblNo,
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
                                    parameter.woodPacking,
                                    parameter.HSCODE,
                                    parameter.saveByID,
                                    parameter.updateDate,
                                    parameter.mblUpdateDate,
                                    IsSynToMBL
                                    );
        }

        /// <summary>
        /// 保存HBL的箱信息
        /// </summary>
        public SingleResult SaveVGMInfo(SaveVGMInfoParameter parameter)
        {
            return SaveVGMInfo(
                                    parameter.id,
                                    parameter.mblID,
                                    parameter.ResponsibleParty,
                                    parameter.ResponsiblePerson,
                                    parameter.WeightSite,
                                    parameter.VerifiedPerson,
                                    parameter.VerifiedDate,
                                    parameter.WeightDate,
                                    parameter.saveByID,
                                    parameter.updateDate
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
                                            parameter.containerVGMCrossWeights,
                                            parameter.containerVGMMethods,
                                            parameter.containerCTNOpers,
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
        /// 保存HBL的箱信息
        /// </summary>
        ManyResult SaveDeclareHBLContainerInfo(SaveBLContainerParameter parameter, bool IsSynToMBL)
        {
            return SaveDeclareHBLContainerInfo(parameter.oceanBookingID,
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
                                            parameter.containerVGMCrossWeights,
                                            parameter.containerVGMMethods,
                                            parameter.containerCTNOpers,
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
                                            parameter.containerVGMCrossWeights,
                                            parameter.containerVGMMethods,
                                            parameter.containerCTNOpers,
                                            parameter.containerMeasurements,
                                            parameter.containerIsSOCs,
                                            parameter.containerIsPartOfs,
                                            parameter.saveByID,
                                            parameter.updateDates,
                                            parameter.cargoUpdateDates,
                                            isSynToHBL);
        }

        #endregion

        /// <summary>
        /// 保存MBL和预配信息
        /// </summary>
        /// <param name="blParameter">MBL数据</param>
        /// <param name="containers">预配箱数据</param>
        /// <param name="bls">预配提单数据</param>
        /// <param name="cargos">货物品名数据</param>
        /// <returns>返回 BLResult,ContainerResult</returns>
        public SingleResult SaveOceanDeclarationContainerWithTrans(SaveMBLInfoParameter blParameter,
            ContainerSaveRequest containers
            , BLSaveRequest bls, CargoSaveRequest cargos)
        {
            TransactionOptions option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            SingleResult result = new SingleResult();

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {

                SingleResult blReslut = new SingleResult();
                try
                {
                    blReslut = SaveOceanMBLInfo(blParameter);
                    Guid MBLID = blReslut.GetValue<Guid>("ID");
                    //保存箱列表
                    ManyResult cmResult = SaveOceanContainerInfo(containers);
                    Dictionary<string, Guid> dicContainers = cmResult.Items.ToDictionary(sResult => sResult.GetValue<string>("No"), sResult => sResult.GetValue<Guid>("ID"));
                    //保存提单
                    bls.MBLID = MBLID;
                    ManyResult blmResult = SaveDeclarationOceanBLInfo(bls);
                    Dictionary<string, Guid> dicBLs = blmResult.Items.ToDictionary(mResult => mResult.GetValue<string>("NO"), mResult => mResult.GetValue<Guid>("ID"));
                    //保存品名
                    cargos.MBLID = MBLID;
                    //SET HBL INFO
                    for (int csrIndex = 0; csrIndex < cargos.hblnos.Length; csrIndex++)
                    {
                        cargos.hblids[csrIndex] = dicBLs[cargos.hblnos[csrIndex]];
                    }
                    //SET CONTAINER INFO
                    for (int csrIndex = 0; csrIndex < cargos.containerids.Length; csrIndex++)
                    {

                        cargos.containerids[csrIndex] = dicContainers[cargos.containernos[csrIndex]];
                    }
                    SaveOceanDeclarationCargo(cargos);
                    result.Add("BLResult", blReslut);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        #endregion

        public List<EDIShowValue> GetEDIDataSourceForNBEDIInfos(int EdiType, Guid[] IDS)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetEDIDataSourceForNBEDIInfos");

                dbCommand.CommandTimeout = 60;

                string tempIds = IDS.Join();


                db.AddInParameter(dbCommand, "@EdiType", DbType.Byte, EdiType);
                db.AddInParameter(dbCommand, "@IDS", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count == 1)
                {
                    return ConvertEDIShowValue(ds.Tables[0], null, EdiType);
                }
                else if (ds.Tables.Count == 2)
                {
                    return ConvertEDIShowValue(ds.Tables[0], ds.Tables[1], EdiType);
                }

                return null;
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

        private List<EDIShowValue> ConvertEDIShowValue(DataTable BLInfos, DataTable ContainerInfos, int EdiType)
        {
            if (BLInfos == null)
            {
                return null;
            }

            List<EDIShowValue> values = new List<EDIShowValue>();
            EDIShowValue value;

            List<EDIValue> BLs = (from b in BLInfos.AsEnumerable()
                                  select new EDIValue
                                  {
                                      BookingNO = b.Field<string>("BookingNO"),
                                      CagoType = b.Field<string>("CagoType"),
                                      CargoDESC = b.Field<string>("CargoDESC"),
                                      Centigrade = b.Field<string>("Centigrade"),
                                      ConsigneeAddress = b.Field<string>("ConsigneeAddress"),
                                      ConsigneeFax = b.Field<string>("ConsigneeFax"),
                                      ConsigneeName = b.Field<string>("ConsigneeName"),
                                      ConsigneeTel = b.Field<string>("ConsigneeTel"),
                                      Container = b.Field<string>("Container"),
                                      DangerousClass = b.Field<string>("DangerousClass"),
                                      DangerousNo = b.Field<string>("DangerousNo"),
                                      DeliveryPort = b.Field<string>("DeliveryPort"),
                                      DeliveryPortCode = b.Field<string>("DeliveryPortCode"),
                                      DischargePort = b.Field<string>("DischargePort"),
                                      DischargePortCode = b.Field<string>("DischargePortCode"),
                                      ETD = b.Field<DateTime>("ETD").ToShortDateString(),
                                      LoadPort = b.Field<string>("LoadPort"),
                                      LoadPortCode = b.Field<string>("LoadPortCode"),
                                      Marks = b.Field<string>("Marks"),
                                      NotifyAddress = b.Field<string>("NotifyAddress"),
                                      NotifyFax = b.Field<string>("NotifyFax"),
                                      NotifyName = b.Field<string>("NotifyName"),
                                      NotifyTel = b.Field<string>("NotifyTel"),
                                      PaymentTerm = b.Field<string>("PaymentTerm"),
                                      Qty = b.Field<decimal>("Qty").ToString("F0"),
                                      Receipt = b.Field<string>("Receipt"),
                                      ReceiptCode = b.Field<string>("ReceiptCode"),
                                      Remarks = b.Field<string>("Remarks"),
                                      SCNO = b.Field<string>("SCNO"),
                                      ReleaseType = b.Field<string>("ReleaseType"),
                                      HSCode = b.Field<string>("HSCode"),
                                      ShipperAddress = b.Field<string>("ShipperAddress"),
                                      ShipperFax = b.Field<string>("ShipperFax"),
                                      ShipperName = b.Field<string>("ShipperName"),
                                      ShipperTel = b.Field<string>("ShipperTel"),
                                      TransportClauseName = b.Field<string>("TransportClauseName"),
                                      UNCode = b.Field<string>("UNCode"),
                                      Vessel = b.Field<string>("Vessel"),
                                      Volume = b.Field<decimal>("Volume").ToString("F2"),
                                      Voyage = b.Field<string>("Voyage"),
                                      Weight = b.Field<decimal>("Weight").ToString("F2"),
                                      No = b.Field<string>("No"),
                                  }).ToList();

            List<EDIContainerValue> Containers = null;
            if (ContainerInfos != null)
            {
                Containers = (from b in ContainerInfos.AsEnumerable()
                              select new EDIContainerValue
                              {
                                  ContainerNo = b.Field<string>("ContainerNo"),
                                  ContainerType = b.Field<string>("ContainerType"),
                                  Qty = b.Field<decimal>("Qty").ToString("F0"),
                                  SealNo = b.Field<string>("SealNo"),
                                  Volume = b.Field<decimal>("Volume").ToString("F2"),
                                  Weight = b.Field<decimal>("Weight").ToString("F2"),
                                  No = b.Field<string>("No"),
                              }).ToList();
            }

            if (BLs != null)
            {
                foreach (EDIValue BL in BLs)
                {
                    Type t = typeof(EDIValue);
                    System.Reflection.PropertyInfo[] propertyInfos = t.GetProperties();
                    foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
                    {
                        value = new EDIShowValue();
                        value.No = BL.No;
                        value.ContainerNo = null;
                        object[] objarr = propertyInfo.GetCustomAttributes(true);
                        if (objarr != null && objarr.Length > 0)
                        {
                            GuidRequiredAttribute att = objarr[0] as GuidRequiredAttribute;
                            value.Node = att.CMessage;
                        }
                        else
                            value.Node = propertyInfo.Name;

                        value.Sourse = "";
                        value.Value = propertyInfo.GetValue(BL, null) == null ? "" : propertyInfo.GetValue(BL, null).ToString();
                        values.Add(value);
                    }

                }
            }

            if (Containers != null)
            {
                foreach (EDIContainerValue Container in Containers)
                {
                    Type t = typeof(EDIContainerValue);
                    System.Reflection.PropertyInfo[] propertyInfos = t.GetProperties();
                    foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
                    {
                        object[] objarr = propertyInfo.GetCustomAttributes(true);
                        if (objarr != null && objarr.Length > 0)
                        {
                            GuidRequiredAttribute att = objarr[0] as GuidRequiredAttribute;

                            value = new EDIShowValue();
                            value.No = Container.No;
                            value.ContainerNo = Container.ContainerNo;
                            value.Node = att.CMessage;
                            value.Sourse = "";
                            value.Value = propertyInfo.GetValue(Container, null) == null ? "" : propertyInfo.GetValue(Container, null).ToString();
                            values.Add(value);
                        }
                    }

                }
            }

            foreach (EDIShowValue show in values)
            {
                switch (show.Node)
                {
                    case "提单编号":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "船编号":
                        show.Sourse = "MBL";
                        break;
                    case "船名":
                        show.Sourse = "MBL";
                        break;
                    case "航次":
                        show.Sourse = "MBL";
                        break;
                    case "离港日":
                        show.Sourse = "MBL";
                        break;
                    case "收货地编号":
                        show.Sourse = "MBL";
                        break;
                    case "收货地":
                        show.Sourse = "MBL";
                        break;
                    case "装货港编号":
                        show.Sourse = "MBL";
                        break;
                    case "装货港":
                        show.Sourse = "MBL";
                        break;
                    case "卸货港编号":
                        show.Sourse = "MBL";
                        break;
                    case "卸货港":
                        show.Sourse = "MBL";
                        break;
                    case "交货地编号":
                        show.Sourse = "MBL";
                        break;
                    case "交货地":
                        show.Sourse = "MBL";
                        break;
                    case "订舱号":
                        show.Sourse = "MBL";
                        break;
                    case "付款类型":
                        show.Sourse = "MBL";
                        break;
                    case "运输条款":
                        show.Sourse = "MBL";
                        break;
                    case "合约号":
                        show.Sourse = "MBL";
                        break;
                    case "发货人名称":
                        show.Sourse = "MBL";
                        break;
                    case "发货人地址":
                        show.Sourse = "MBL";
                        break;
                    case "发货人电话":
                        show.Sourse = "MBL";
                        break;
                    case "发货人传真":
                        show.Sourse = "MBL";
                        break;
                    case "收货人名称":
                        show.Sourse = "MBL";
                        break;
                    case "收货人地址":
                        show.Sourse = "MBL";
                        break;
                    case "收货人电话":
                        show.Sourse = "MBL";
                        break;
                    case "收货人传真":
                        show.Sourse = "MBL";
                        break;
                    case "通知人名称":
                        show.Sourse = "MBL";
                        break;
                    case "通知人地址":
                        show.Sourse = "MBL";
                        break;
                    case "通知人电话":
                        show.Sourse = "MBL";
                        break;
                    case "通知人传真":
                        show.Sourse = "MBL";
                        break;
                    case "件数":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "箱号":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "封条号":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "重量":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "体积":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "品名":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "备注":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "唛头":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                    case "箱型":
                        show.Sourse = (EdiType == 0 || EdiType == 2) ? "MBL" : "报关单";
                        break;
                }
            }


            return values;
        }

        #region BLInfo

        /// <summary>
        /// 保存预配提单信息
        /// </summary>
        /// <param name="blSaveRequest">保存提单对象</param>
        /// <returns>返回提单列表</returns>
        public ManyResult SaveDeclarationOceanBLInfo(BLSaveRequest blSaveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(blSaveRequest.oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(blSaveRequest.MBLID, "MBLID");
            ArgumentHelper.AssertGuidNotEmpty(blSaveRequest.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 blSaveRequest.BLIDs,
                 blSaveRequest.BLNos,
                 blSaveRequest.Markss,
                 blSaveRequest.GoodsDescriptions,
                 blSaveRequest.QuantityUnitIDs,
                 blSaveRequest.WeightUnitIDs,
                 blSaveRequest.MeasurementUnitIDs,
                 blSaveRequest.containerUpdateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveOceanDeclarationBLInfo]");

                string tempconBLIDs = blSaveRequest.BLIDs.Join();
                string tempBLNos = blSaveRequest.BLNos.Join();
                string tempMarkss = blSaveRequest.Markss.Join();
                string tempGoodsDescriptions = blSaveRequest.GoodsDescriptions.Join();
                string tempQuantityUnitIDs = blSaveRequest.QuantityUnitIDs.Join();
                string tempWeightUnitIDs = blSaveRequest.WeightUnitIDs.Join();
                string tempMeasurementUnitIDs = blSaveRequest.MeasurementUnitIDs.Join();
                string tempcontainerUpdateDates = blSaveRequest.containerUpdateDates.Join();

                db.AddInParameter(dbCommand, "@OceanMBLID", DbType.Guid, blSaveRequest.MBLID);

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempconBLIDs);
                db.AddInParameter(dbCommand, "@Nos", DbType.String, tempBLNos);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, tempMarkss);
                db.AddInParameter(dbCommand, "@GoodsDescriptions", DbType.String, tempGoodsDescriptions);
                db.AddInParameter(dbCommand, "@QuantityUnitIDs", DbType.String, tempQuantityUnitIDs);
                db.AddInParameter(dbCommand, "@WeightUnitIDs", DbType.String, tempWeightUnitIDs);
                db.AddInParameter(dbCommand, "@MeasurementUnitIDs", DbType.String, tempMeasurementUnitIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempcontainerUpdateDates);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, blSaveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new[] { "ID","No", "UpdateDate" });
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
        /// 删除业务下预配提单信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public void RemoveDeclarationOceanBLInfo(Guid[] ids, Guid removeByID, DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanDeclarationBLInfo");

                string tempIDs = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 获取提单列表
        /// </summary>
        /// <param name="blSaveRequests">保存提单对象集合</param>
        /// <returns>返回提单列表</returns>
        public Dictionary<Guid, SaveResponse> SaveDeclarationOceanBLInfoList(List<BLSaveRequest> blSaveRequests)
        {
            try
            {
                TransactionOptions option = new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                    if (blSaveRequests != null)
                    {
                        foreach (BLSaveRequest saveRequest in blSaveRequests)
                        {
                            ManyResult temp = SaveDeclarationOceanBLInfo(saveRequest);
                            result.Add(saveRequest.RequestId, new SaveResponse { RequestId = saveRequest.RequestId, ManyResult = temp });
                        }
                    }
                    scope.Complete();
                    return result;
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
        /// 获取提单详细信息列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="MBLID">主提单ID</param>
        /// <param name="fcmBLType">提单类型</param>
        /// <returns></returns>
        public List<OceanBLInfo> GetBLInfoListByOperationID(Guid operationID, Guid? MBLID, FCMBLType fcmBLType)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "oceanBookingID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetBLInfoListByOperationID");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, MBLID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanBLInfo> results = BulidOceanBLInfoListByDataSet(ds);

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

        List<OceanBLInfo> BulidOceanBLInfoListByDataSet(DataSet ds)
        {
            List<OceanBLInfo> results = (ds.Tables[0].AsEnumerable().Select(b => new OceanBLInfo
            {
                ID = b.Field<Guid>("ID"),
                MBLID = b.IsNull("MBLID") ? Guid.Empty : b.Field<Guid>("MBLID"),
                OceanBookingID = b.Field<Guid>("OceanBookingID"),
                No = b.Field<string>("BLNo"),
                GoodsDescription = b.Field<string>("GoodsDescription"),
                Marks = b.Field<string>("Marks"),
                QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                QuantityUnitName = b.Field<string>("QuantityUnitName"),
                WeightUnitID = b.Field<Guid>("WeightUnitID"),
                WeightUnitName = b.Field<string>("WeightUnitName"),
                MeasurementUnitID = b.Field<Guid>("MeasurementUnitID"),
                MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                CreateByName = b.Field<string>("CreateByName"),
                CreateDate = b.Field<DateTime>("CreateDate"),
                UpdateByName = b.Field<string>("UpdateByName"),
                UpdateDate = b.Field<DateTime?>("UpdateDate"),
            })).ToList();
            return results;
        }
        #endregion

        #region Obsolete
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
        /// <param name="nbPODCode"></param>
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
        /// <param name="eta"></param>
        /// <param name="etd"></param>
        /// <param name="preETD"></param>
        /// <param name="isCarrierSendAMS"></param>
        /// <param name="bookingPartyID"></param>
        /// <param name="collectbyAgentOrderID"></param>
        /// <param name="isThirdPlacePayOrder"></param>
        /// <param name="reallyNotifyParty"></param>
        /// <param name="reallyConsignee"></param>
        /// <param name="reallyShipper"></param>
        /// <param name="gateindate"></param>
        /// <param name="HSCODE"></param>
        /// <param name="Commodity"></param>
        /// <param name="Container"></param>
        /// <param name="NotifyParty2"></param>
        /// <param name="HasFee"></param>
        /// <param name="CargoDescription"></param>
        /// <param name="FreightRateID">合约ID</param>
        /// <returns>返回SingleResult</returns>
        [Obsolete("新方法SaveOceanMBLInfo(SaveMBLInfoParameter)")]
        SingleResult SaveOceanMBLInfo(
            Guid? id,
            Guid oceanBookingID,
            string mblNo,
            short? numberOfOriginal,
            VoyageShowType voyageShowType,
            Guid? checkerID,
            Guid? shipperID,
            CustomerDescriptionForNew shipperDescription,
            Guid? consigneeID,
            CustomerDescriptionForNew consigneeDescription,
            Guid? notifyPartyID,
            CustomerDescriptionForNew notifyPartyDescription,
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
            string nbPODCode,
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
            //Guid? bLTitleID,
            Guid saveByID,
            string agentText,
            Guid? FreightRateID,
            DateTime? updateDate,
            DateTime? eta,
            DateTime? etd,
            DateTime? preETD,
            bool isCarrierSendAMS,
            Guid? bookingPartyID,
            Guid? collectbyAgentOrderID,
            bool isThirdPlacePayOrder,
            string reallyNotifyParty,
            string reallyConsignee,
            string reallyShipper,
            DateTime? gateindate,
            string HSCODE,
            string Commodity,
            string Container,
            string NotifyParty2,
            bool HasFee,
            SpclCargoDescription CargoDescription
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
                //保存MBL
                Stopwatch stopwatchMbl = Stopwatch.StartNew();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanMBLInfo");

                dbCommand.CommandTimeout = 60;

                string tempagentDescription = SerializerHelper.SerializeToString(agentDescription, true, false);
                string tempshipperDescription = SerializerHelper.SerializeToString(shipperDescription, true, false);
                string tempconsigneeDescription = SerializerHelper.SerializeToString(consigneeDescription, true, false);
                string tempnotifyPartyDescription = SerializerHelper.SerializeToString(notifyPartyDescription, true, false);
                string tempCargoDescription = SerializerHelper.SerializeToString(CargoDescription, true, false);
                #region 添加参数
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
                db.AddInParameter(dbCommand, "@NBPODCode", DbType.String, nbPODCode);
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

                db.AddInParameter(dbCommand, "@IsCarrierSendAMS", DbType.Boolean, isCarrierSendAMS);
                db.AddInParameter(dbCommand, "@BookingPartyID", DbType.Guid, bookingPartyID);
                db.AddInParameter(dbCommand, "@CollectbyAgentOrderID", DbType.Guid, collectbyAgentOrderID);
                db.AddInParameter(dbCommand, "@IsThirdPlacePayOrder", DbType.Boolean, isThirdPlacePayOrder);
                db.AddInParameter(dbCommand, "@ReallyShipper", DbType.String, reallyShipper);
                db.AddInParameter(dbCommand, "@ReallyConsignee", DbType.String, reallyConsignee);
                db.AddInParameter(dbCommand, "@ReallyNotifyParty", DbType.String, reallyNotifyParty);
                db.AddInParameter(dbCommand, "@GateInDate", DbType.DateTime, gateindate);
                db.AddInParameter(dbCommand, "@HSCODE", DbType.String, HSCODE);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, Commodity);
                db.AddInParameter(dbCommand, "@Container", DbType.String, Container);
                db.AddInParameter(dbCommand, "@NotifyParty2", DbType.String, NotifyParty2);
                db.AddInParameter(dbCommand, "@Hasfee", DbType.Boolean, HasFee);
                db.AddInParameter(dbCommand, "@CargoDescription", DbType.Xml, tempCargoDescription);
                #endregion


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

                _OperationLogService.Add(DateTime.Now, "SAVE-MBL-DB"
                            , string.Format("保存完成;MBL ID[{0}];OceanBookingID[{1}]", result.GetValue<Guid>("ID"), oceanBookingID)
                            , stopwatchMbl.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
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
    }
}

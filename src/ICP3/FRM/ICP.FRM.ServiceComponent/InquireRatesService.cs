using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary;

namespace ICP.FRM.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ServiceInterface;
    using ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Helper;
    using System.Transactions;
    using Framework.CommonLibrary.Server;
    using System.Threading;
    using Sys.ServiceInterface.DataObjects;
    using Sys.ServiceInterface;
    using Framework.CommonLibrary.Client;
    using Message.ServiceInterface;
    using FCM.Common.ServiceInterface;
    using FCM.Common.ServiceInterface.DataObjects;
    using System.Text;
using MailCenter.ServiceInterface;

    /// <summary>
    /// 海运运价管理服务
    /// </summary>
    public class InquireRatesService : IInquireRatesService
    {
        #region 初始化

        private ISessionService _sessionService;
        /// <summary>
        /// 邮件发送服务
        /// </summary>
        private readonly IMessageService _messageService;
        /// <summary>
        /// 用户管理服务
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// FCM公用服务
        /// </summary>
        private readonly IFCMCommonService _FCMCommonService;
        /// <summary>
        /// 邮件模板获取对象
        /// </summary>
        private EmailTemplateGetter _EmailTemplateGetter;
        /// <summary>
        /// 邮件关联服务
        /// </summary>
        public IOperationMessageRelationService _OperationMessageRelationService;

        /// <summary>
        /// 海出业务员单
        /// </summary>
        public IOceanExportService _OceanExportService;
        /// <summary>
        /// Sender: 管理员用户ID
        /// </summary>
        readonly Guid administratorId = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
        /// <summary>
        /// Sender: 管理员邮件地址
        /// </summary>
        string administratorEmailAddress = string.Empty;
        /// <summary>
        /// 事件代码集合
        /// </summary>
        private List<EventCode> eventCodeList = new List<EventCode>();

        /// <summary>
        /// 通过构造函数初始化服务
        /// </summary>
        /// <param name="sessionService"></param>
        /// <param name="messageService"></param>
        /// <param name="userService"></param>
        /// <param name="FCMCommonService"></param>
        /// <param name="OMRelationService"></param>
        /// <param name="OExportService"></param>
        public InquireRatesService(ISessionService sessionService, IMessageService messageService
            , IUserService userService, IFCMCommonService FCMCommonService
            , IOperationMessageRelationService OMRelationService, IOceanExportService OExportService)
        {
            _sessionService = sessionService;
            _messageService = messageService;
            _userService = userService;
            _FCMCommonService = FCMCommonService;
            _OperationMessageRelationService = OMRelationService;
            _OceanExportService = OExportService;
        }

        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        bool IsEnglish
        {
            get
            {
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }
        Guid UserId
        {
            get {
                return ApplicationContext.Current.UserId;
            }
        }

        #endregion

        #region Search

        #region Common

        /// <summary>
        /// 通过询价ID获取询价询问人列表
        /// </summary>
        /// <param name="inquirePriceID">询价ID</param>
        /// <param name="parentID">询价ID(父询价ID)</param>
        /// <returns></returns>
        public List<InquirePriceInquireBys> GetInquirePriceInquireBys(Guid inquirePriceID, Guid? parentID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquirePriceID, "inquirePriceID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceInquireBys");
                //父询价ID为空时设置当前询价ID
                db.AddInParameter(dbCommand, "@InquirePriceID", DbType.Guid, parentID ?? inquirePriceID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }

                return (from b in ds.Tables[0].AsEnumerable()
                        select new InquirePriceInquireBys
                        {
                            ID = b.Field<Guid>("Id"),
                            InquireByID = b.Field<Guid>("InquireByID"),
                            InquireBy = b.Field<String>("InquireBy"),
                            Handled = b.Field<Boolean>("Handled"),  //询问人询价状态
                            InquireByEName = b.Field<String>("InquireByEName"),
                            InquireByCName = b.Field<String>("InquireByCName"),
                            InquireDate = b.Field<DateTime>("InquireDate"),
                        }).ToList();
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 处理询价信息
        /// </summary>
        /// <param name="ID">询价人信息ID</param>
        /// <param name="Handled">询价人询价状态</param>
        /// <returns>处理结果</returns>
        public void HandledInquirePriceInquireBys(Guid ID, Boolean Handled)
        {
            ArgumentHelper.AssertGuidNotEmpty(ID, "ID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspHandledInquireRate");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, ID);
                db.AddInParameter(dbCommand, "@Handled", DbType.Boolean, Handled);
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
        /// 询价替换
        /// </summary>
        /// <param name="oldID">待替换询价ID</param>
        /// <param name="newID">新询价ID</param>
        /// <returns></returns>
        public SingleResultData ReplaceInquirePrice(Guid oldID, Guid newID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oldID, "oldID");
            ArgumentHelper.AssertGuidNotEmpty(newID, "newID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspReplaceInquirePrice");

                db.AddInParameter(dbCommand, "@OldID", DbType.Guid, oldID);
                db.AddInParameter(dbCommand, "@NewID", DbType.Guid, newID);

                SingleResultData result = db.SingleResult(dbCommand);
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

        #region Ocean

        /// <summary>
        /// 获取海运询价列表
        /// </summary>
        /// <param name="pol">POL</param>
        /// <param name="delivery">Delivery</param>
        /// <param name="pod">POD</param>
        /// <param name="commodity">Commodity</param>
        /// <param name="inquireOrRespondBy">InquireBy OR RespondBy</param>
        /// <param name="isUnReply">isUnReply</param>
        /// <param name="durationFrom">Duration From</param>
        /// <param name="durationTo">Duration To</param>
        /// <param name="currentUserID">Current UserID</param>
        /// <returns></returns>
        public InquierOceanRatesResult GetInquireOceanRateList(
            string pol,
            string delivery,
            string pod,
            string commodity,
            Guid? inquireOrRespondBy,
            bool isUnReply,
            DateTime? durationFrom,
            DateTime? durationTo,
            Guid currentUserID)
        {
            return GetInquireOceanRateList(string.Empty, pol, delivery, pod, commodity, inquireOrRespondBy
                , isUnReply, durationFrom, durationTo,"", currentUserID);
        }

        /// <summary>
        /// 获取海运询价列表
        /// </summary>
        /// <param name="no">NO</param>
        /// <param name="pol">POL</param>
        /// <param name="delivery">Delivery</param>
        /// <param name="pod">POD</param>
        /// <param name="commodity">Commodity</param>
        /// <param name="inquireOrRespondBy">InquireBy OR RespondBy</param>
        /// <param name="isUnReply">isUnReply</param>
        /// <param name="durationFrom">Duration From</param>
        /// <param name="durationTo">Duration To</param>
        /// <param name="StrQuery"></param>
        /// <param name="currentUserID">Current UserID</param>
        /// <returns></returns>
        public InquierOceanRatesResult GetInquireOceanRateList(
          string no,
          string pol,
          string delivery,
          string pod,
          string commodity,
          Guid? inquireOrRespondBy,
          bool? isUnReply,
          DateTime? durationFrom,
          DateTime? durationTo, string StrQuery,
          Guid currentUserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceList");

                db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.OceanRates);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@From", DbType.String, pol);
                db.AddInParameter(dbCommand, "@To", DbType.String, delivery);
                db.AddInParameter(dbCommand, "@POD", DbType.String, pod);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, commodity);
                db.AddInParameter(dbCommand, "@InquireOrRespondByID", DbType.Guid, inquireOrRespondBy);
                db.AddInParameter(dbCommand, "@UnReply", DbType.Boolean, isUnReply);
                db.AddInParameter(dbCommand, "@InquireDate_From", DbType.DateTime, durationFrom);
                db.AddInParameter(dbCommand, "@InquireDate_To", DbType.DateTime, durationTo);
                db.AddInParameter(dbCommand, "@StrQuery", DbType.AnsiString, StrQuery);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, currentUserID);
                //db.AddInParameter(dbCommand, "@CountOfUnReply_For_AllType", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierOceanRatesResult result = BulidInquireOceanRateListByDataSet(ds, isUnReply);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取海运历史询价列表
        /// </summary>
        /// <param name="inquirePriceID">历史记录不包含自身ID，排除</param>
        /// <param name="parentID">历史记录不包含自身的parentID，排除</param>
        /// <param name="polID"></param>
        /// <param name="podID"></param>
        /// <param name="deliveryID"></param>
        /// <param name="carrierID"></param>
        /// <returns></returns>
        public InquierOceanRatesResult GetInquireOceanRateHistoryList(Guid inquirePriceID, Guid? parentID, Guid? polID, Guid? podID, Guid? deliveryID, Guid? carrierID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceHistoryList");

                db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.OceanRates);
                db.AddInParameter(dbCommand, "@InquirePriceID", DbType.Guid, inquirePriceID);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@FromID", DbType.Guid, polID);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, podID);
                db.AddInParameter(dbCommand, "@ToID", DbType.Guid, deliveryID);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, carrierID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierOceanRatesResult result = BulidInquireOceanRateListByDataSet(ds, false);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取General Info面板的数据
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public InquierOceanRate GetInquierOceanRateInfoForInquireBy(Guid inquireRateID, Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceInfoForInquireBy");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierOceanRate result = (from b in ds.Tables[0].AsEnumerable()
                                           select new InquierOceanRate
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               No = b.Field<String>("No"),
                                               ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                               ShippingLineName = b.Field<String>("ShippingLine"),
                                               RespondByID = b.Field<Guid?>("RespondByID"),
                                               RespondByName = b.Field<String>("RespondBy"),
                                               POLID = b.Field<Guid?>("POLID"),
                                               POLName = b.Field<String>("POL"),
                                               PODID = b.Field<Guid?>("PODID"),
                                               PODName = b.Field<String>("POD"),
                                               PlaceOfDeliveryID = b.Field<Guid?>("DeliveryID"),
                                               PlaceOfDeliveryName = b.Field<String>("Delivery"),
                                               CargoReady = b.Field<String>("CargeReady"),
                                               CargoWeight = b.Field<String>("CargoWeight"),
                                               CustomerID = b.Field<Guid?>("CustomerID"),
                                               CustomerName = b.Field<String>("Customer"),
                                               //DiscussingWhenNew = b.Field<String>("DiscussingWhenNew"),
                                               EstimateTimeOfDelivery = b.Field<String>("ETOD"),
                                               ExpCarrierName = b.Field<String>("ExpCarrier"),
                                               ExpCommodity = b.Field<String>("ExpCommodity"),
                                               ExpPrice = b.Field<String>("ExpPrice"),
                                               ExpTransportClauseID = b.Field<Guid?>("ExpTermID"),
                                               ExpTransportClauseName = b.Field<String>("Term"),
                                               //IsWillBooking = b.Field<bool>("PB"),
                                               Measurement = b.Field<String>("Measurement"),
                                               ////InquireByID = b.Field<Guid?>("InquireByID"),
                                               ////InquireByName = b.Field<String>("InquireBy"),
                                               CreateDate = b.Field<DateTime>("InquireDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           }).SingleOrDefault();

                result.InquireBysList = (from b in ds.Tables[1].AsEnumerable()
                                         select new InquirePriceInquireBys
                                         {
                                             ID = b.Field<Guid>("Id"),
                                             InquireByID = b.Field<Guid>("InquireByID"),
                                             InquireBy = b.Field<String>("InquireBy"),
                                             Handled = b.Field<Boolean>("Handled"),   //询价处理状态
                                             InquireByEName = b.Field<String>("InquireByEName"),
                                             InquireByCName = b.Field<String>("InquireByCName"),
                                             InquireDate = b.Field<DateTime>("InquireDate"),
                                         }).ToList();


                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 通过询价ID集获取海运询价集合
        ///     构建询价邮件内容
        /// </summary>
        /// <param name="ids">询价ID集</param>
        /// <param name="currentUserID">当前用户ID</param>
        /// <returns>海运询价</returns>
        InquierOceanRatesResult GetInquireOceanRateListByIds(Guid[] ids, Guid currentUserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceListByIds");

                string tempids = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempids);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, currentUserID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierOceanRatesResult result = BulidInquireOceanRateListByDataSet(ds, false);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 构建海运询价列表对象
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="isUnReply">Is Un Reply</param>
        /// <returns>海运查询结果</returns>
        private static InquierOceanRatesResult BulidInquireOceanRateListByDataSet(DataSet ds, bool? isUnReply)
        {
            InquierOceanRatesResult result = new InquierOceanRatesResult();

            List<InquierOceanRate> inquierOceanRateList = (from b in ds.Tables[0].AsEnumerable()
                                                           select new InquierOceanRate
                                                           {
                                                               ID = b.Field<Guid>("ID"),
                                                               MainRecordID = b.Field<Guid?>("ParentID"),
                                                               No = b.Field<String>("No"),
                                                               HasUnconfirmedShipment = b.Field<Boolean>("HasUnconfirmedShipment"),
                                                               //HasUnRead = b.Field<bool>("HasUnReadDiscussing"),
                                                               //IsNoPriceAll = b.Field<bool>("HasNoRates"),
                                                               RespondByID = b.Field<Guid?>("RespondByID"),
                                                               RespondByName = b.Field<String>("RespondByName"),
                                                               CarrierID = b.Field<Guid?>("CarrierID"),
                                                               CarrierName = b.Field<String>("Carrier"),
                                                               DurationFrom = b.Field<DateTime?>("DurationFrom"),
                                                               DurationTo = b.Field<DateTime?>("DurationTo"),
                                                               POLID = b.Field<Guid?>("POLID"),
                                                               POLName = b.Field<String>("POL"),
                                                               PODID = b.Field<Guid?>("PODID"),
                                                               PODName = b.Field<String>("POD"),
                                                               PlaceOfDeliveryID = b.Field<Guid?>("DeliveryID"),
                                                               PlaceOfDeliveryName = b.Field<String>("Delivery"),
                                                               Commodity = (string.IsNullOrEmpty(b.Field<String>("Commodity")) ?
                                                                        string.Empty :
                                                                        b.Field<String>("Commodity").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),
                                                               TransportClauseID = b.Field<Guid?>("TermID"),
                                                               TransportClauseName = b.Field<String>("Term"),
                                                               ExpCommodity = b.Field<String>("ExpCommodity"),
                                                               Measurement = b.Field<String>("Measurement"),
                                                               CargoWeight = b.Field<String>("CargoWeight"),
                                                               CargoReady = b.Field<String>("CargoReady"),
                                                               SurCharge = b.Field<String>("SurCharge"),
                                                               ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                                               ShippingLineName = b.Field<String>("ShippingLine"),
                                                               CurrencyID = b.Field<Guid?>("CurrencyID"),
                                                               CurrencyName = b.Field<String>("Currency"),
                                                               //CustomerID = b.Field<Guid>("CustomerID"),
                                                               //CustomerName = b.Field<String>("Customer"),
                                                               Remark = b.Field<String>("Remark"),
                                                               Shared = b.Field<bool>("IsShare"),
                                                               ////InquireByID = b.Field<Guid?>("InquireByID"),
                                                               ////InquireByName = b.Field<String>("InquireByName"),
                                                               //CreateDate = b.Field<DateTime>("CreateDate"), // 可能不要
                                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                               DTime = b.Field<DateTimeOffset?>("DTime"),
                                                               UnitRates = (from u in ds.Tables[1].AsEnumerable()
                                                                            where u.Field<Guid>("InquirePriceID") == b.Field<Guid>("ID")
                                                                            select new InquireUnit
                                                                            {
                                                                                ID = u.Field<Guid>("ID"),
                                                                                //ParentID = u.Field<Guid>("InquierOceanRateID"),
                                                                                Rate = u.Field<decimal>("Rate"),
                                                                                UnitID = u.Field<Guid>("UnitID"),
                                                                                UnitName = u.Field<string>("Unit")
                                                                            }).ToList(),
                                                               IsDirty = false

                                                           }).ToList();

            result.InquierOceanRateList = inquierOceanRateList;


            if (ds.Tables.Count > 2)
            {
                List<InquireUnit> maxUnits = (from u in ds.Tables[2].AsEnumerable()
                                              select new InquireUnit
                                              {
                                                  //ID = u.Field<Guid>("ID"),
                                                  //OceanID = u.Field<Guid>("OceanID"),
                                                  //RowPosition = u.Field<short>("RowPosition"),
                                                  UnitID = u.Field<Guid>("UnitID"),
                                                  UnitName = u.Field<string>("Unit")
                                              }).ToList();

                List<UnReadDiscussingCount> unReadList = new List<UnReadDiscussingCount>();
                //if (isUnReply)
                //{
                //    unReadList = (from g in ds.Tables[3].AsEnumerable()
                //                  select new UnReadDiscussingCount
                //                  {
                //                      Type = (InquierType)g.Field<byte>("InquirePriceType"),
                //                      CountOfUnreply = g.Field<int>("CountOfUnreply"),
                //                  }).ToList();
                //}

                result.MaxUnits = maxUnits;
                result.UnReadCountList = unReadList;
            }

            return result;
        }

        #endregion

        #region Air

        /// <summary>
        /// 获取海运询价列表
        /// </summary>
        /// <param name="pol"></param>
        /// <param name="delivery"></param>
        /// <param name="pod"></param>
        /// <param name="commodity"></param>
        /// <param name="inquireOrRespondBy"></param>
        /// <param name="isUnReply"></param>
        /// <param name="durationFrom"></param>
        /// <param name="durationTo"></param>
        /// <param name="StrQuery"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public InquierAirRatesResult GetInquireAirRateList(
          string pol,
          string delivery,
          string pod,
          string commodity,
          Guid? inquireOrRespondBy,
          bool? isUnReply,
          DateTime? durationFrom,
          DateTime? durationTo, string StrQuery, 
          Guid currentUserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceList");

                db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.AirRates);
                db.AddInParameter(dbCommand, "@From", DbType.String, pol);
                db.AddInParameter(dbCommand, "@To", DbType.String, delivery);
                db.AddInParameter(dbCommand, "@POD", DbType.String, pod);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, commodity);
                db.AddInParameter(dbCommand, "@InquireOrRespondByID", DbType.Guid, inquireOrRespondBy);
                db.AddInParameter(dbCommand, "@UnReply", DbType.Boolean, isUnReply);
                db.AddInParameter(dbCommand, "@InquireDate_From", DbType.DateTime, durationFrom);
                db.AddInParameter(dbCommand, "@InquireDate_To", DbType.DateTime, durationTo);
                db.AddInParameter(dbCommand, "@StrQuery", DbType.AnsiString, StrQuery);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, currentUserID);
                //db.AddInParameter(dbCommand, "@CountOfUnReply_For_AllType", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierAirRatesResult result = BulidInquireAirRateListByDataSet(ds, isUnReply);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取General Info面板的数据
        /// </summary>
        /// <param name="inquireRateID">询价ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public InquierAirRate GetInquierAirRateInfoForInquireBy(Guid inquireRateID, Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceInfoForInquireBy");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierAirRate result = (from b in ds.Tables[0].AsEnumerable()
                                         select new InquierAirRate
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             ShippingLineID = b.Field<Guid>("ShippingLineID"),
                                             ShippingLineName = b.Field<String>("ShippingLine"),
                                             RespondByID = b.Field<Guid>("RespondByID"),
                                             RespondByName = b.Field<String>("RespondBy"),
                                             POLID = b.Field<Guid>("POLID"),
                                             POLName = b.Field<String>("POL"),
                                             PlaceOfDeliveryID = b.Field<Guid?>("DeliveryID"),
                                             PlaceOfDeliveryName = b.Field<String>("Delivery"),
                                             CargoReady = b.Field<String>("CargeReady"),
                                             CargoWeight = b.Field<String>("CargoWeight"),
                                             CustomerID = b.Field<Guid?>("CustomerID"),
                                             CustomerName = b.Field<String>("Customer"),
                                             EstimateTimeOfDelivery = b.Field<String>("ETOD"),
                                             ExpCarrierName = b.Field<String>("ExpCarrier"),
                                             ExpCommodity = b.Field<String>("ExpCommodity"),
                                             ExpPrice = b.Field<String>("ExpPrice"),
                                             CartonsOrPallets = b.Field<String>("COP"),
                                             MAWB = b.Field<String>("MAWB"),
                                             HAWB = b.Field<String>("HAWB"),
                                             ExpTransportClauseID = b.Field<Guid?>("ExpTermID"),
                                             ExpTransportClauseName = b.Field<String>("Term"),
                                             //IsWillBooking = b.Field<bool>("PB"),
                                             Measurement = b.Field<String>("Measurement"),
                                             ////InquireByID = b.Field<Guid>("InquireByID"),
                                             ////InquireByName = b.Field<String>("InquireBy"),
                                             CreateDate = b.Field<DateTime>("InquireDate"),
                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                         }).SingleOrDefault();
                result.InquireBysList = (from b in ds.Tables[1].AsEnumerable()
                                         select new InquirePriceInquireBys
                                         {
                                             ID = b.Field<Guid>("Id"),
                                             InquireByID = b.Field<Guid>("InquireByID"),
                                             InquireBy = b.Field<String>("InquireBy"),
                                             Handled = b.Field<Boolean>("Handled"),   //询价处理状态
                                             InquireByEName = b.Field<String>("InquireByEName"),
                                             InquireByCName = b.Field<String>("InquireByCName"),
                                             InquireDate = b.Field<DateTime>("InquireDate"),
                                         }).ToList();

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 通过询价ID集获取空运询价集合
        ///     构建询价邮件内容
        /// </summary>
        /// <param name="ids">询价ID集</param>
        /// <param name="currentUserID">当前用户ID</param>
        /// <returns>空运询价</returns>
        InquierAirRatesResult GetInquireAirRateListByIds(Guid[] ids, Guid currentUserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceListByIds");

                string tempids = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempids);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, currentUserID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierAirRatesResult result = BulidInquireAirRateListByDataSet(ds, false);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 构建空运询价列表
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isUnReply"></param>
        /// <returns></returns>
        private static InquierAirRatesResult BulidInquireAirRateListByDataSet(DataSet ds, bool? isUnReply)
        {

            List<InquierAirRate> inquierAirRateList = (from b in ds.Tables[0].AsEnumerable()
                                                       select new InquierAirRate
                                                       {
                                                           ID = b.Field<Guid>("ID"),
                                                           MainRecordID = b.Field<Guid?>("ParentID"),
                                                           No = b.Field<String>("No"),
                                                           HasUnconfirmedShipment = b.Field<Boolean>("HasUnconfirmedShipment"),
                                                           //HasUnRead = b.Field<bool>("HasUnReadDiscussing"),
                                                           //IsNoPriceAll = b.Field<bool>("HasNoRates"),
                                                           RespondByID = b.Field<Guid?>("RespondByID"),
                                                           RespondByName = b.Field<String>("RespondByName"),
                                                           CarrierID = b.Field<Guid?>("CarrierID"),
                                                           CarrierName = b.Field<String>("Carrier"),
                                                           DurationFrom = b.Field<DateTime?>("DurationFrom"),
                                                           DurationTo = b.Field<DateTime?>("DurationTo"),
                                                           POLID = b.Field<Guid?>("POLID"),
                                                           POLName = b.Field<String>("POL"),
                                                           PlaceOfDeliveryID = b.Field<Guid?>("DeliveryID"),
                                                           PlaceOfDeliveryName = b.Field<String>("Delivery"),
                                                           Commodity = (string.IsNullOrEmpty(b.Field<String>("Commodity")) ?
                                                                    string.Empty :
                                                                    b.Field<String>("Commodity").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),
                                                           Schedule = b.Field<String>("Schedule"),
                                                           Routing = b.Field<String>("Routing"),
                                                           ExpCommodity = b.Field<String>("ExpCommodity"),
                                                           Measurement = b.Field<String>("Measurement"),
                                                           CargoWeight = b.Field<String>("CargoWeight"),
                                                           CargoReady = b.Field<String>("CargoReady"),
                                                           ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                                           ShippingLineName = b.Field<String>("ShippingLine"),
                                                           CurrencyID = b.Field<Guid?>("CurrencyID"),
                                                           CurrencyName = b.Field<String>("Currency"),
                                                           //CustomerID = b.Field<Guid>("CustomerID"),
                                                           //CustomerName = b.Field<String>("Customer"),
                                                           Remark = b.Field<String>("Remark"),
                                                           Shared = b.Field<bool>("IsShare"),
                                                           ////InquireByID = b.Field<Guid?>("InquireByID"),
                                                           ////InquireByName = b.Field<String>("InquireByName"),
                                                           //CreateDate = b.Field<DateTime>("CreateDate"), // 可能不要
                                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                           DTime = b.Field<DateTimeOffset?>("DTime"),
                                                           UnitRates = (from u in ds.Tables[1].AsEnumerable()
                                                                        where u.Field<Guid>("InquirePriceID") == b.Field<Guid>("ID")
                                                                        select new InquireUnit
                                                                        {
                                                                            ID = u.Field<Guid>("ID"),
                                                                            Rate = u.Field<decimal>("Rate"),
                                                                            UnitID = u.Field<Guid>("UnitID"),
                                                                            UnitName = u.Field<string>("Unit")
                                                                        }).ToList(),
                                                           IsDirty = false

                                                       }).ToList();

            InquierAirRatesResult result = new InquierAirRatesResult();
            result.InquierAirRateList = inquierAirRateList;

            if (ds.Tables.Count > 2)
            {
                List<InquireUnit> maxUnits = (from u in ds.Tables[2].AsEnumerable()
                                              select new InquireUnit
                                              {
                                                  UnitID = u.Field<Guid>("UnitID"),
                                                  UnitName = u.Field<string>("Unit")
                                              }).ToList();

                List<UnReadDiscussingCount> unReadList = new List<UnReadDiscussingCount>();
                //if (isUnReply)
                //{
                //    unReadList = (from g in ds.Tables[3].AsEnumerable()
                //                  select new UnReadDiscussingCount
                //                  {
                //                      Type = (InquierType)g.Field<byte>("InquirePriceType"),
                //                      CountOfUnreply = g.Field<int>("CountOfUnreply"),
                //                  }).ToList();
                //}

                result.MaxUnits = maxUnits;
                result.UnReadCountList = unReadList;
            }

            return result;
        }

        #endregion

        #region Trucking

        /// <summary>
        /// 获取拖车询价列表
        /// </summary>
        /// <param name="no">No</param>
        /// <param name="pol"></param>
        /// <param name="delivery"></param>
        /// <param name="pod"></param>
        /// <param name="commodity"></param>
        /// <param name="inquireOrRespondBy"></param>
        /// <param name="isUnReply"></param>
        /// <param name="durationFrom"></param>
        /// <param name="durationTo"></param>
        /// <param name="StrQuery"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public InquierTruckingRatesResult GetInquireTruckingRateList(string no,
          string pol,
          string delivery,
          string pod,
          string commodity,
          Guid? inquireOrRespondBy,
          bool? isUnReply,
          DateTime? durationFrom,
          DateTime? durationTo, string StrQuery, 
          Guid currentUserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceList");

                db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.TruckingRates);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@From", DbType.String, pol);
                db.AddInParameter(dbCommand, "@To", DbType.String, delivery);
                db.AddInParameter(dbCommand, "@POD", DbType.String, pod);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, commodity);
                db.AddInParameter(dbCommand, "@InquireOrRespondByID", DbType.Guid, inquireOrRespondBy);
                db.AddInParameter(dbCommand, "@UnReply", DbType.Boolean, isUnReply);
                db.AddInParameter(dbCommand, "@InquireDate_From", DbType.DateTime, durationFrom);
                db.AddInParameter(dbCommand, "@InquireDate_To", DbType.DateTime, durationTo);
                db.AddInParameter(dbCommand, "@StrQuery", DbType.AnsiString, StrQuery);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, currentUserID);
                //db.AddInParameter(dbCommand, "@CountOfUnReply_For_AllType", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierTruckingRatesResult result = BulidInquireTruckingRateListByDataSet(ds, isUnReply);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取拖车历史询价列表
        /// </summary>
        /// <param name="inquirePriceID">历史记录不包含自身ID，排除</param>
        /// <param name="parentID">历史记录不包含自身的parentID，排除</param>
        /// <param name="polID"></param>
        /// <param name="podID"></param>
        /// <param name="deliveryID"></param>
        /// <param name="carrierID"></param>
        /// <returns></returns>
        public InquierTruckingRatesResult GetInquireTruckingRateHistoryList(Guid inquirePriceID, Guid? parentID, Guid? polID, Guid? podID, Guid? deliveryID, Guid? carrierID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceHistoryList");

                db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.OceanRates);
                db.AddInParameter(dbCommand, "@InquirePriceID", DbType.Guid, inquirePriceID);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@FromID", DbType.Guid, polID);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, podID);
                db.AddInParameter(dbCommand, "@ToID", DbType.Guid, deliveryID);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, carrierID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierTruckingRatesResult result = BulidInquireTruckingRateListByDataSet(ds, false);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取General Info面板的数据
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public InquierTruckingRate GetInquierTruckingRateInfoForInquireBy(Guid inquireRateID, Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceInfoForInquireBy");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierTruckingRate result = (from b in ds.Tables[0].AsEnumerable()
                                              select new InquierTruckingRate
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  No = b.Field<string>("No"),
                                                  ShippingLineID = b.Field<Guid>("ShippingLineID"),
                                                  ShippingLineName = b.Field<String>("ShippingLine"),
                                                  RespondByID = b.Field<Guid>("RespondByID"),
                                                  RespondByName = b.Field<String>("RespondBy"),
                                                  POLID = b.Field<Guid>("POLID"),
                                                  POLName = b.Field<String>("POL"),
                                                  //PODID = b.Field<Guid>("PODID"),
                                                  //PODName = b.Field<String>("POD"),
                                                  PlaceOfDeliveryID = b.Field<Guid?>("DeliveryID"),
                                                  PlaceOfDeliveryName = b.Field<String>("Delivery"),
                                                  CargoReady = b.Field<String>("CargeReady"),
                                                  CargoWeight = b.Field<String>("CargoWeight"),
                                                  CustomerID = b.Field<Guid?>("CustomerID"),
                                                  CustomerName = b.Field<String>("Customer"),
                                                  CartonsOrPallets = b.Field<String>("COP"),
                                                  EstimateTimeOfDelivery = b.Field<String>("ETOD"),
                                                  ExpCarrierName = b.Field<String>("ExpCarrier"),
                                                  ExpCommodity = b.Field<String>("ExpCommodity"),
                                                  ExpPrice = b.Field<String>("ExpPrice"),
                                                  ExpTransportClauseID = b.Field<Guid?>("ExpTermID"),
                                                  ExpTransportClauseName = b.Field<String>("Term"),
                                                  //IsWillBooking = b.Field<bool>("PB"),
                                                  Measurement = b.Field<String>("Measurement"),
                                                  ////InquireByID = b.Field<Guid>("InquireByID"),
                                                  ////InquireByName = b.Field<String>("InquireBy"),
                                                  CreateDate = b.Field<DateTime>("InquireDate"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                              }).SingleOrDefault();
                result.InquireBysList = (from b in ds.Tables[1].AsEnumerable()
                                         select new InquirePriceInquireBys
                                         {
                                             ID = b.Field<Guid>("Id"),
                                             InquireByID = b.Field<Guid>("InquireByID"),
                                             InquireBy = b.Field<String>("InquireBy"),
                                             Handled = b.Field<Boolean>("Handled"),   //询价处理状态
                                             InquireByEName = b.Field<String>("InquireByEName"),
                                             InquireByCName = b.Field<String>("InquireByCName"),
                                             InquireDate = b.Field<DateTime>("InquireDate"),
                                         }).ToList();


                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 通过询价ID集获取拖车询价集合
        ///     构建询价邮件内容
        /// </summary>
        /// <param name="ids">询价ID集</param>
        /// <param name="currentUserID">当前用户ID</param>
        /// <returns>拖车询价</returns>
        InquierTruckingRatesResult GetInquireTruckingRateListByIds(Guid[] ids, Guid currentUserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquirePriceListByIds");

                string tempids = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempids);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, currentUserID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                InquierTruckingRatesResult result = BulidInquireTruckingRateListByDataSet(ds, false);

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 构建拖车询价对象
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isUnReply"></param>
        /// <returns></returns>
        private static InquierTruckingRatesResult BulidInquireTruckingRateListByDataSet(DataSet ds, bool? isUnReply)
        {

            List<InquierTruckingRate> inquierTruckingRateList = (from b in ds.Tables[0].AsEnumerable()
                                                                 select new InquierTruckingRate
                                                                 {
                                                                     ID = b.Field<Guid>("ID"),
                                                                     MainRecordID = b.Field<Guid?>("ParentID"),
                                                                     No = b.Field<String>("No"),
                                                                     HasUnconfirmedShipment = b.Field<Boolean>("HasUnconfirmedShipment"),
                                                                     //HasUnRead = b.Field<bool>("HasUnReadDiscussing"),
                                                                     //IsNoPriceAll = b.Field<bool>("HasNoRates"),
                                                                     RespondByID = b.Field<Guid>("RespondByID"),
                                                                     RespondByName = b.Field<String>("RespondByName"),
                                                                     CarrierID = b.Field<Guid?>("CarrierID"),
                                                                     CarrierName = b.Field<String>("Carrier"),
                                                                     DurationFrom = b.Field<DateTime?>("DurationFrom"),
                                                                     DurationTo = b.Field<DateTime?>("DurationTo"),
                                                                     POLID = b.Field<Guid>("POLID"),
                                                                     POLName = b.Field<String>("POL"),
                                                                     PlaceOfDeliveryID = b.Field<Guid?>("DeliveryID"),
                                                                     PlaceOfDeliveryName = b.Field<String>("Delivery"),
                                                                     Commodity = (string.IsNullOrEmpty(b.Field<String>("Commodity")) ?
                                                                              string.Empty :
                                                                              b.Field<String>("Commodity").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),
                                                                     TransportClauseID = b.Field<Guid?>("TermID"),
                                                                     TransportClauseName = b.Field<String>("Term"),
                                                                     ExpCommodity = b.Field<String>("ExpCommodity"),
                                                                     Measurement = b.Field<String>("Measurement"),
                                                                     CargoWeight = b.Field<String>("CargoWeight"),
                                                                     CargoReady = b.Field<String>("CargoReady"),
                                                                     Rate = b.Field<decimal?>("TruckingRate"),
                                                                     FUEL = b.Field<decimal?>("FUEL"),
                                                                     Total = b.Field<decimal?>("Total"),
                                                                     ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                                                     ShippingLineName = b.Field<String>("ShippingLine"),
                                                                     CurrencyID = b.Field<Guid?>("CurrencyID"),
                                                                     CurrencyName = b.Field<String>("Currency"),
                                                                     ZipCode = b.Field<String>("ZIPCode"),
                                                                     Remark = b.Field<String>("Remark"),
                                                                     Shared = b.Field<bool>("IsShare"),
                                                                     UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                     IsDirty = false

                                                                 }).ToList();

            InquierTruckingRatesResult result = new InquierTruckingRatesResult();
            result.InquierTruckingRateList = inquierTruckingRateList;

            if (ds.Tables.Count > 2)
            {
                List<InquireUnit> maxUnits = (from u in ds.Tables[2].AsEnumerable()
                                              select new InquireUnit
                                              {
                                                  UnitID = u.Field<Guid>("UnitID"),
                                                  UnitName = u.Field<string>("Unit")
                                              }).ToList();

                List<UnReadDiscussingCount> unReadList = new List<UnReadDiscussingCount>();
                result.MaxUnits = maxUnits;
                result.UnReadCountList = unReadList;
            }
            return result;
        }

        #endregion

        #endregion

        #region Bulid

        #endregion

        #region Update

        #region Common

        #region 新增询价

        /// <summary>
        /// 发起一个新的海运询价
        /// </summary>
        /// <param name="id">询价ID</param>
        /// <param name="no">NO</param>
        /// <param name="type">询价类型</param>
        /// <param name="rateIDs">箱型的ID</param>
        /// <param name="rateUnitIDs">箱型的ID</param>
        /// <param name="rateRates">箱型对应询价</param>
        /// <param name="shippingLineID">航线ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="expCarrier">期望承运人</param>
        /// <param name="polID">POL ID</param>
        /// <param name="podID">POD ID</param>
        /// <param name="placeOfDeliveryID">Place Of Delivery ID</param>
        /// <param name="expCommodity">期望Commodity</param>
        /// <param name="expTransportClauseID">Exp Transport Clause ID</param>
        /// <param name="cargoWeight">Cargo Weight</param>
        /// <param name="measurement">measurement</param>
        /// <param name="cargoReady"></param>
        /// <param name="cartonsOrPallets"></param>
        /// <param name="mAWB"></param>
        /// <param name="hAWB"></param>
        /// <param name="zipCode"></param>
        /// <param name="estimateTimeOfDelivery"></param>
        /// <param name="isWillBooking"></param>
        /// <param name="expPrice"></param>
        /// <param name="discussingWhenNew"></param>
        /// <param name="respondByID"></param>
        /// <param name="inquireByID"></param>
        /// <param name="sentTime"></param>
        /// <param name="updateDate"></param>
        /// <param name="companyID"></param>
        /// <param name="currentUserID"></param>
        /// <returns>返回新询价的 "ID","No"</returns>
        SingleResult IInquireRatesService.SaveInquireRateInfo(
               Guid id,string no,
               InquierType type,
               Guid[] rateIDs,
               Guid[] rateUnitIDs,
               decimal[] rateRates,
               Guid? shippingLineID,
               Guid? customerID,
               string expCarrier,
               Guid? polID,
               Guid? podID,
               Guid? placeOfDeliveryID,
               string expCommodity,
               Guid? expTransportClauseID,
               string cargoWeight,
               string measurement,
               string cargoReady,
               string cartonsOrPallets,
               string mAWB,
               string hAWB,
               string zipCode,
               string estimateTimeOfDelivery,
               bool isWillBooking,
               string expPrice,
               string discussingWhenNew,
               Guid? respondByID,
               Guid? inquireByID,
               DateTimeOffset sentTime,
               DateTime? updateDate,
               Guid companyID,Guid currentUserID)
        {
            if (mAWB == null) throw new ArgumentNullException("mAWB");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveInquirePriceInfoForInquireBy");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, type);
                db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, shippingLineID);
                db.AddInParameter(dbCommand, "@POLID", DbType.Guid, polID);
                db.AddInParameter(dbCommand, "@PODID", DbType.Guid, podID);
                db.AddInParameter(dbCommand, "@DeliveryID", DbType.Guid, placeOfDeliveryID);
                db.AddInParameter(dbCommand, "@ExpCarrier", DbType.String, expCarrier);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@ExpCommodity", DbType.String, expCommodity);
                db.AddInParameter(dbCommand, "@ExpTermID", DbType.Guid, expTransportClauseID);
                db.AddInParameter(dbCommand, "@CargoWeight", DbType.String, cargoWeight);
                db.AddInParameter(dbCommand, "@Measurement", DbType.String, measurement);
                db.AddInParameter(dbCommand, "@CargeReady", DbType.String, cargoReady);
                db.AddInParameter(dbCommand, "@COP", DbType.String, cartonsOrPallets);
                db.AddInParameter(dbCommand, "@MAWB", DbType.String, mAWB);
                db.AddInParameter(dbCommand, "@HAWB", DbType.String, hAWB);
                db.AddInParameter(dbCommand, "@ZipCode", DbType.String, zipCode);
                db.AddInParameter(dbCommand, "@ETOD", DbType.String, estimateTimeOfDelivery);
                db.AddInParameter(dbCommand, "@PB", DbType.Boolean, isWillBooking);
                db.AddInParameter(dbCommand, "@ExpPrice", DbType.String, expPrice);
                db.AddInParameter(dbCommand, "@InquireByID", DbType.Guid, inquireByID);
                db.AddInParameter(dbCommand, "@RespondByID", DbType.Guid, respondByID);

                db.AddInParameter(dbCommand, "@RateIDs", DbType.String, rateIDs.Join());
                db.AddInParameter(dbCommand, "@UnitIDs", DbType.String, rateUnitIDs.Join());
                db.AddInParameter(dbCommand, "@Rates", DbType.String, rateRates.Join());

                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@sentTime", DbType.DateTimeOffset, sentTime);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "No", "UpdateDate" });
                if (id == Guid.Empty)   //新增时从返回结果获取ID
                {
                    id = result.GetValue<Guid>("ID");
                }
                //发送邮件到商务
                SendEmail2RespondByWithInquireRate(id, type, currentUserID);
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

        #region 增加和删除箱型
        /// <summary>
        /// 增加和删除箱型(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="unitIDs"></param>
        /// <param name="changeByID"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        public SingleResultData ChangeRateUnit(Guid inquireRateID,
            Guid[] unitIDs,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspChangeRateUnit");

                db.AddInParameter(dbCommand, "@InquireRateID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@UnitIDs", DbType.String, unitIDs.Join());
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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

        #region 移交询价到另一回复人(商务)
        /// <summary>
        /// 更改Respond by，商务员将此inquire rate 移交给另一个商务员(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="inquireRateID">询价ID</param>
        /// <param name="inquireByID">询价人ID</param>
        /// <param name="oldRespondByID">回复人ID</param>
        /// <param name="newRespondByID">新回复人ID</param>
        /// <param name="postscript"></param>
        /// <param name="sentTime"></param>
        /// <param name="saveByID"></param>
        /// <returns></returns>
        public void TransitRespondMan(Guid inquireRateID,
            Guid? inquireByID,
            Guid? oldRespondByID,
            Guid newRespondByID,
            string postscript,
            DateTimeOffset sentTime,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveInquirePriceInfoForTransition");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@InquireByID", DbType.Guid, inquireByID);
                db.AddInParameter(dbCommand, "@OriginalRespondByID", DbType.Guid, oldRespondByID);
                db.AddInParameter(dbCommand, "@CurrentRespondByID", DbType.Guid, newRespondByID);
                db.AddInParameter(dbCommand, "@Discussing", DbType.String, postscript);
                db.AddInParameter(dbCommand, "@sentTime", DbType.DateTimeOffset, sentTime);
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
        #endregion

        #region 删除询价(海运，空运，拖车询价通用)

        /// <summary>
        /// 删除询价(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        public void RemoveInquireRate(
            Guid[] inquireRateIDs,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateIDs, "inquireRateIDs");
            ArgumentHelper.AssertArrayLengthMatch(inquireRateIDs, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveInquirePrice");

                db.AddInParameter(dbCommand, "@IDS", DbType.String, inquireRateIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
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

        #endregion

        #region 批量修改拖车询价的FUEL(燃料)

        /// <summary>
        /// 批量修改拖车询价的FUEL(燃料)
        /// </summary>
        /// <param name="inquireRateIDs"></param>
        /// <param name="updateDates"></param>
        /// <param name="fuel"></param>
        /// <param name="saveByID"></param>
        public ManyResultData BatchUpdateChargeFuelForInquirePrices(
            Guid[] inquireRateIDs,
            DateTime?[] updateDates,
            Decimal fuel,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateIDs, "inquireRateIDs");
            ArgumentHelper.AssertArrayLengthMatch(inquireRateIDs, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspBatchUpdateChargeFuelForInquirePrices");

                db.AddInParameter(dbCommand, "@InquirePriceIds", DbType.String, inquireRateIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@fuelAmount", DbType.Decimal, fuel);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                ManyResultData result = db.ManyResult(dbCommand);
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

        #region 检查是否存在相同港口而且没有过期的拖车询价
        /// <summary>
        /// 检查是否存在相同港口而且没有过期的拖车询价
        /// </summary>
        /// <param name="fromID"></param>
        /// <param name="toID"></param>
        /// <returns></returns>
        public bool CheckExistTruckingRateSamePort(Guid fromID, Guid toID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspCheckExistTruckingRateSamePort");

                db.AddInParameter(dbCommand, "@FromID", DbType.Guid, fromID);
                db.AddInParameter(dbCommand, "@ToID", DbType.Guid, toID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, UserId);

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
        #endregion

        #region 确认业务询价
        /// <summary>
        /// 确认业务询价
        /// </summary>
        /// <param name="oceanBookingID">业务ID</param>
        /// <param name="userID">当前用户ID</param>
        /// <param name="isConfirm">true确认，false取消确认</param>
        /// <returns></returns>
        public SingleResultData ConfirmInquirePriceToShipment(Guid oceanBookingID, Guid userID, Boolean isConfirm)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspConfirmInquirePriceToShipment");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsConfirm", DbType.Boolean, isConfirm);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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

        #region Ocean
        /// <summary>
        /// 用事务的方式保存海运同一个询价列表
        ///     商务保存询价
        /// </summary>
        /// <param name="datas">询价集合</param>
        /// <param name="sentTime">发送时间</param>
        /// <param name="companyID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public ManyResultData SaveOceanInquireRateWithTrans(List<InquierOceanRate> datas
            , DateTimeOffset sentTime, Guid companyID, Guid currentUserID)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                ManyResultData result = new ManyResultData();
                if (datas != null)
                {
                    try
                    {
                        result.ChildResults = new List<SingleResultData>();
                        foreach (InquierOceanRate item in datas)
                        {
                            SingleResultData itemResult = new SingleResultData();

                            Guid[] rateIDs = new Guid[item.UnitRates.Count];
                            Guid[] rateUnitIDs = new Guid[item.UnitRates.Count];
                            decimal[] rateRates = new decimal[item.UnitRates.Count];
                            for (int i = 0; i < item.UnitRates.Count; i++)
                            {
                                rateIDs[i] = item.UnitRates[i].ID;
                                rateUnitIDs[i] = item.UnitRates[i].UnitID;
                                rateRates[i] = item.UnitRates[i].Rate;
                            }

                            Database db = DatabaseFactory.CreateDatabase();
                            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveInquirePriceInfoForRespondBy");

                            db.AddInParameter(dbCommand, "@ID", DbType.Guid, item.ID);
                            db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.OceanRates);
                            db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, item.MainRecordID);
                            db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, item.ShippingLineID);
                            db.AddInParameter(dbCommand, "@POLID", DbType.Guid, item.POLID);
                            db.AddInParameter(dbCommand, "@PODID", DbType.Guid, item.PODID);
                            db.AddInParameter(dbCommand, "@DeliveryID", DbType.Guid, item.PlaceOfDeliveryID);
                            db.AddInParameter(dbCommand, "@InquireByID", DbType.Guid, item.InquireByID);
                            db.AddInParameter(dbCommand, "@RespondByID", DbType.Guid, item.RespondByID);
                            db.AddInParameter(dbCommand, "@ZipCode", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@Schedule", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@Routing", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@TermID", DbType.Guid, item.TransportClauseID);
                            db.AddInParameter(dbCommand, "@Commodity", DbType.String, item.Commodity);
                            db.AddInParameter(dbCommand, "@SurCharge", DbType.String, item.SurCharge);
                            db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, item.CarrierID);
                            db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, item.CurrencyID);
                            db.AddInParameter(dbCommand, "@TruckingRate", DbType.Decimal, 0.00M);
                            db.AddInParameter(dbCommand, "@FUEL", DbType.Decimal, 0.00M);
                            db.AddInParameter(dbCommand, "@Total", DbType.Decimal, 0.00M);
                            db.AddInParameter(dbCommand, "@IsShare", DbType.Boolean, item.Shared);
                            db.AddInParameter(dbCommand, "@Remark", DbType.String, item.Remark);
                            db.AddInParameter(dbCommand, "@DurationFrom", DbType.DateTime, item.DurationFrom);
                            db.AddInParameter(dbCommand, "@DurationTo", DbType.DateTime, item.DurationTo);

                            db.AddInParameter(dbCommand, "@RateIDs", DbType.String, rateIDs.Join());
                            db.AddInParameter(dbCommand, "@UnitIDs", DbType.String, rateUnitIDs.Join());
                            db.AddInParameter(dbCommand, "@Rates", DbType.String, rateRates.Join());
                            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, item.UpdateDate);
                            db.AddInParameter(dbCommand, "@sentTime", DbType.DateTimeOffset, sentTime);
                            db.AddInParameter(dbCommand, "@IsCreateDiscussing", DbType.Boolean, true);
                            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                            itemResult = db.SingleResult(dbCommand);

                            result.ChildResults.Add(itemResult);
                        }

                        scope.Complete();

                        //注释,改成多个InquireByID——后面改
                        //发邮件通知业务员。
                        SendEmail2InquireByWithInquireRate(InquierType.OceanRates, datas,currentUserID);

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
                else
                    return null;
            }
        }  
        #endregion

        #region Air
        /// <summary>
        /// 用事务的方式保存空运同一个询价列表
        /// </summary>
        /// <param name="datas">询价列表</param>
        /// <param name="sentTime">发送时间</param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public ManyResultData SaveAirInquireRateWithTrans(List<InquierAirRate> datas
            , DateTimeOffset sentTime, Guid companyID)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                ManyResultData result = new ManyResultData();
                if (datas != null)
                {
                    try
                    {
                        result.ChildResults = new List<SingleResultData>();
                        foreach (InquierAirRate item in datas)
                        {
                            SingleResultData itemResult = new SingleResultData();


                            Guid[] rateIDs = new Guid[item.UnitRates.Count];
                            Guid[] rateUnitIDs = new Guid[item.UnitRates.Count];
                            decimal[] rateRates = new decimal[item.UnitRates.Count];
                            for (int i = 0; i < item.UnitRates.Count; i++)
                            {
                                rateIDs[i] = item.UnitRates[i].ID;
                                rateUnitIDs[i] = item.UnitRates[i].UnitID;
                                rateRates[i] = item.UnitRates[i].Rate;
                            }

                            Database db = DatabaseFactory.CreateDatabase();
                            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveInquirePriceInfoForRespondBy");

                            db.AddInParameter(dbCommand, "@ID", DbType.Guid, item.ID);
                            db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.AirRates);
                            db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, item.MainRecordID);
                            db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, item.ShippingLineID);
                            db.AddInParameter(dbCommand, "@POLID", DbType.Guid, item.POLID);
                            db.AddInParameter(dbCommand, "@PODID", DbType.Guid, item.PODID);
                            db.AddInParameter(dbCommand, "@DeliveryID", DbType.Guid, item.PlaceOfDeliveryID);
                            db.AddInParameter(dbCommand, "@InquireByID", DbType.Guid, item.InquireByID);
                            db.AddInParameter(dbCommand, "@RespondByID", DbType.Guid, item.RespondByID);
                            db.AddInParameter(dbCommand, "@ZipCode", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@Schedule", DbType.String, item.Schedule);
                            db.AddInParameter(dbCommand, "@Routing", DbType.String, item.Routing);
                            db.AddInParameter(dbCommand, "@TermID", DbType.Guid, item.TransportClauseID);
                            db.AddInParameter(dbCommand, "@Commodity", DbType.String, item.Commodity);
                            db.AddInParameter(dbCommand, "@SurCharge", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, item.CarrierID);
                            db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, item.CurrencyID);
                            db.AddInParameter(dbCommand, "@TruckingRate", DbType.Decimal, 0.00M);
                            db.AddInParameter(dbCommand, "@FUEL", DbType.Decimal, 0.00M);
                            db.AddInParameter(dbCommand, "@Total", DbType.Decimal, 0.00M);
                            db.AddInParameter(dbCommand, "@IsShare", DbType.Boolean, item.Shared);
                            db.AddInParameter(dbCommand, "@Remark", DbType.String, item.Remark);
                            db.AddInParameter(dbCommand, "@DurationFrom", DbType.DateTime, item.DurationFrom);
                            db.AddInParameter(dbCommand, "@DurationTo", DbType.DateTime, item.DurationTo);

                            db.AddInParameter(dbCommand, "@RateIDs", DbType.String, rateIDs.Join());
                            db.AddInParameter(dbCommand, "@UnitIDs", DbType.String, rateUnitIDs.Join());
                            db.AddInParameter(dbCommand, "@Rates", DbType.String, rateRates.Join());
                            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, item.UpdateDate);
                            db.AddInParameter(dbCommand, "@IsCreateDiscussing", DbType.Boolean, true);
                            db.AddInParameter(dbCommand, "@SentTime", DbType.DateTimeOffset, sentTime);
                            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                            itemResult = db.SingleResult(dbCommand);
                            result.ChildResults.Add(itemResult);
                        }

                        scope.Complete();

                        //发邮件通知业务员。
                        //SendEmail2InquireByWithInquireRate(datas[0].InquireByID.Value, datas[0].RespondByID.Value, InquierType.AirRates, datas);

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
                else
                    return null;
            }
        } 
        #endregion

        #region Trucking
        /// <summary>
        /// 用事务的方式保存Trucking同一个询价列表
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="sentTime"></param>
        /// <param name="companyID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public ManyResultData SaveTruckingInquireRateWithTrans(List<InquierTruckingRate> datas
            , DateTimeOffset sentTime, Guid companyID, Guid currentUserID)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                ManyResultData result = new ManyResultData();
                if (datas != null)
                {
                    try
                    {
                        result.ChildResults = new List<SingleResultData>();
                        foreach (InquierTruckingRate item in datas)
                        {
                            SingleResultData itemResult = new SingleResultData();


                            Guid[] rateIDs = new Guid[item.UnitRates.Count];
                            Guid[] rateUnitIDs = new Guid[item.UnitRates.Count];
                            decimal[] rateRates = new decimal[item.UnitRates.Count];
                            for (int i = 0; i < item.UnitRates.Count; i++)
                            {
                                rateIDs[i] = item.UnitRates[i].ID;
                                rateUnitIDs[i] = item.UnitRates[i].UnitID;
                                rateRates[i] = item.UnitRates[i].Rate;
                            }

                            Database db = DatabaseFactory.CreateDatabase();
                            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveInquirePriceInfoForRespondBy");

                            db.AddInParameter(dbCommand, "@ID", DbType.Guid, item.ID);
                            db.AddInParameter(dbCommand, "@Type", DbType.Byte, InquierType.TruckingRates);
                            db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, item.MainRecordID);
                            db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, item.ShippingLineID);
                            db.AddInParameter(dbCommand, "@POLID", DbType.Guid, item.POLID);
                            db.AddInParameter(dbCommand, "@PODID", DbType.Guid, item.PODID);
                            db.AddInParameter(dbCommand, "@DeliveryID", DbType.Guid, item.PlaceOfDeliveryID);
                            db.AddInParameter(dbCommand, "@InquireByID", DbType.Guid, item.InquireByID);
                            db.AddInParameter(dbCommand, "@RespondByID", DbType.Guid, item.RespondByID);
                            db.AddInParameter(dbCommand, "@ZipCode", DbType.String, item.ZipCode);
                            db.AddInParameter(dbCommand, "@Schedule", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@Routing", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@TermID", DbType.Guid, item.TransportClauseID);
                            db.AddInParameter(dbCommand, "@Commodity", DbType.String, item.Commodity);
                            db.AddInParameter(dbCommand, "@SurCharge", DbType.String, string.Empty);
                            db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, item.CarrierID);
                            db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, item.CurrencyID);
                            db.AddInParameter(dbCommand, "@TruckingRate", DbType.Decimal, item.Rate);
                            db.AddInParameter(dbCommand, "@FUEL", DbType.Decimal, item.FUEL);
                            db.AddInParameter(dbCommand, "@Total", DbType.Decimal, item.Total);
                            db.AddInParameter(dbCommand, "@IsShare", DbType.Boolean, item.Shared);
                            db.AddInParameter(dbCommand, "@Remark", DbType.String, item.Remark);
                            db.AddInParameter(dbCommand, "@DurationFrom", DbType.DateTime, item.DurationFrom);
                            db.AddInParameter(dbCommand, "@DurationTo", DbType.DateTime, item.DurationTo);

                            db.AddInParameter(dbCommand, "@RateIDs", DbType.String, rateIDs.Join());
                            db.AddInParameter(dbCommand, "@UnitIDs", DbType.String, rateUnitIDs.Join());
                            db.AddInParameter(dbCommand, "@Rates", DbType.String, rateRates.Join());
                            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, item.UpdateDate);
                            db.AddInParameter(dbCommand, "@sentTime", DbType.DateTimeOffset, sentTime);

                            db.AddInParameter(dbCommand, "@IsCreateDiscussing", DbType.Boolean, true);
                            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                            itemResult = db.SingleResult(dbCommand);

                            result.ChildResults.Add(itemResult);
                        }

                        scope.Complete();
                        //发邮件通知业务员。
                        SendEmail2InquireByWithInquireRate(InquierType.TruckingRates, datas, currentUserID);
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
                else
                    return null;
            }
        } 
        #endregion

        #endregion

        #region Common

        /// <summary>
        /// 获取Email Address
        ///     通过用户ID获取用户邮件地址，未找到用户则返回空文本
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <returns>Email Address:未找到则返回空文本</returns>
        private string GetEmailAddress(Guid? ID)
        {
            //获取邮箱地址
            string strSendEmailAddress = "";
            if (ID != null)
            {
                //获取用户详细信息
                UserInfo inquireByInfo = GetUserInfo(ID);
                if (inquireByInfo != null)
                    strSendEmailAddress = inquireByInfo.EMail;
            }
            strSendEmailAddress = strSendEmailAddress == string.Empty ? "" : strSendEmailAddress;
            return strSendEmailAddress;
        }

        private UserInfo GetUserInfo(Guid? ID)
        {
            //获取用户详细信息
            UserInfo inquireByInfo = _userService.GetUserInfo(new Guid(ID.ToString()));
            return inquireByInfo ?? null;
        }

        /// <summary>
        /// 获取发送邮件
        /// </summary>
        /// <returns></returns>
        private string GetSendEmail()
        {
            if (!string.IsNullOrEmpty(administratorEmailAddress))
                return administratorEmailAddress;
            UserInfo sendUser = _userService.GetUserInfo(administratorId);
            administratorEmailAddress = sendUser.EMail;
            return administratorEmailAddress;
        }

        /// <summary>
        /// 返回当前CODE的事件详细信息
        /// </summary>
        /// <returns></returns>
        public EventCode EventCodeList(string code)
        {
            if (eventCodeList.Any() == false)
            {
                eventCodeList = _FCMCommonService.GetEventCodeList(OperationType.OceanExport);
            }
            return eventCodeList.FirstOrDefault(n => n.Code == code);
        }

        /// <summary>
        /// 返回消息实体类
        /// </summary>
        /// <param name="currentUserID">当前用户ID</param>
        /// <param name="type">发送类型</param>
        /// <param name="way">发送方向</param>
        /// <param name="sendTo">接收人邮箱</param>
        /// <param name="sendFrom">发送人邮箱</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="operationId">操作ID</param>
        /// <param name="formId">表单ID</param>
        /// <param name="body">发送内容</param>
        /// <param name="subject">主题</param>
        /// <param name="cc">邮件抄送地址</param>
        /// <param name="action">操作动作</param>
        /// <param name="eventObjects">事件对象</param>
        /// <param name="attachmentContents">邮件附件信息</param>
        /// <returns></returns>
        public Message
            CreateMessageInfo(Guid currentUserID, MessageType type,
            MessageWay way, string sendTo, string sendFrom, Framework.CommonLibrary.Common.FormType formType,
            OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
            string action, EventObjects eventObjects, List<AttachmentContent> attachmentContents)
        {
            // 邮件发送的消息实体
            var message = new Message();
            message.CreateBy = currentUserID;
            message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            message.Type = type;                //消息类型
            message.Way = way;                  //消息方向
            message.SendTo = sendTo;            //收件人邮箱地址
            message.SendFrom = sendFrom;        //发件人邮箱地址
            message.CC = cc;                    //邮件抄送地址

            message.UserProperties = new MessageUserPropertiesObject
            {
                FormType = formType,
                OperationType = operationType,
                OperationId = operationId,
                FormId = formId
            };
            if (!string.IsNullOrEmpty(action))
            {
                message.UserProperties.Action = action;
            }
            if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
            {
                message.Body = body;
                message.Subject = subject;
            }
            if (attachmentContents != null && attachmentContents.Any())
            {
                message.Attachments = attachmentContents;
            }
            if (eventObjects != null)
                message.UserProperties.EventInfo = eventObjects;

            return message;

        }

        /// <summary>
        /// 商务回复询价
        ///     发送邮件到询价人
        /// </summary>
        /// <param name="inquierType"></param>
        /// <param name="data"></param>
        /// <param name="currentUserID"></param>
        private void SendEmail2InquireByWithInquireRate(InquierType inquierType, object data, Guid currentUserID)
        {
            WaitCallback fire = (notifyInfo) =>
            {
                if (currentUserID == Guid.Empty)
                    return;
                //发送人Email地址
                string sendEmail = string.Empty;
                //询价人集合
                string strInquireBys = string.Empty;
                //询价人邮件集合
                string strInquireByEmails = string.Empty;
                //邮件模板Key
                string itemKey = string.Empty;
                //主询价
                BaseInquireRate inquireObj;
                //询价明细
                StringBuilder items = null;
                //消息
                Message message = null;
                //未处理询价人集合
                List<InquirePriceInquireBys> unHandledInquierBys = null;
                //询价集合
                List<BaseInquireRate> inquireRateList = null;
                try
                {
                    UserInfo LoginUserInfo = GetUserInfo(currentUserID);
                    if (LoginUserInfo == null)
                        return;
                    ApplicationContext.Current.UserId = LoginUserInfo.ID;
                    ApplicationContext.Current.EmailAddress = LoginUserInfo.EMail;
                    ApplicationContext.Current.Username = LoginUserInfo.EName;

                    itemKey = "InquireRateResult";
                    //转换询价数据为BaseInquireRate对象
                    if (inquierType == InquierType.OceanRates)
                        inquireRateList = BuildInquireOceanContent((List<InquierOceanRate>)data);
                    else if(inquierType==InquierType.TruckingRates)
                        inquireRateList = BuildInquireTruckingContent((List<InquierTruckingRate>)data);
                    else if(inquierType==InquierType.AirRates)
                        inquireRateList = BuildInquireAirContent((List<InquierAirRate>)data);
                    //发送邮件地址
                    sendEmail = GetEmailAddress(currentUserID);
                    #region Verification Data
                    if (inquireRateList == null) return;
                    //获取主询价
                    inquireObj = inquireRateList.Find(p => p.MainRecordID == null);
                    if (inquireObj == null)
                        return;
                    //无询价人则查询询价人
                    if (inquireObj.InquireBysList == null)
                    {
                        inquireObj.InquireBysList=GetInquirePriceInquireBys(inquireObj.ID, null);
                    }
                    //获取未处理询价人集合AND邮箱地址
                    unHandledInquierBys = inquireObj.InquireBysList.Where(item => !item.Handled).ToList();
                    //无未处理询价人
                    if (unHandledInquierBys.Count <= 0)
                        return; 
                    #endregion

                    #region Build InquierBy

                    //初始化
                    strInquireBys = "";
                    strInquireByEmails = "";
                    //获取未处理询价人集合 AND 邮箱地址
                    foreach (InquirePriceInquireBys item in unHandledInquierBys)
                    {
                        //使用逗号拼接询价人名称
                        strInquireBys += item.InquireByCName + ",";
                        strInquireByEmails += GetEmailAddress(item.InquireByID) + ";";
                    }
                    //移除询价人字符串最后逗号
                    strInquireBys = strInquireBys.Substring(0, strInquireBys.Length - 1);
                    if (strInquireByEmails == "")
                        return;
                    inquireObj.InquireByName = strInquireBys;
                    #endregion

                    #region Build Items
                    items = new StringBuilder();
                    for (var index = 0; index < inquireRateList.Count; index++)
                    {
                        BaseInquireRate item = inquireRateList[index];
                        if (item.IsMain)
                        {
                            if (inquierType == InquierType.OceanRates)
                                continue;
                        }
                        items.Append("    ");
                        items.AppendFormat(
                            "Item{0}: Carrier={1}, Currency={2}, {3}, Commodity={4}, Term={5}, Surcharge={6}, Duration={7} <br />"
                            , index + 1                       //项索引
                            , item.CarrierName              //承运人名称
                            , item.CurrencyName             //币种
                            , item.UnitRateString                  //集装箱尺寸集合
                            , item.Commodity                //Commodity
                            , item.TransportClauseName      //TransportClauseName
                            , item.TotalSurcharge           //总附加费
                            ,                               //DurationFrom - DurationTo
                            (item.DurationFrom.HasValue ? Convert.ToDateTime(item.DurationFrom).ToString("dd/MM/yyyy") : "") +
                            "-"
                            + (item.DurationTo.HasValue ? Convert.ToDateTime(item.DurationTo).ToString("dd/MM/yyyy") : "")
                            );
                    }

                    #endregion

                    #region Build Message

                    if (_EmailTemplateGetter == null)
                        _EmailTemplateGetter = new EmailTemplateGetter();
                    message = CreateMessageInfo(currentUserID,MessageType.Email, MessageWay.Send,
                                                                strInquireByEmails,
                                                                sendEmail,
                                                                Framework.CommonLibrary.Common.FormType.Unknown,
                                                                OperationType.InquireRate,
                                                                inquireObj.ID, Guid.Empty,
                                                               string.Empty, string.Empty,
                                                                 GetEmailAddress(currentUserID),
                                                                string.Empty, null, null);
                    object[] values = { inquireObj, items };
                    message = _EmailTemplateGetter.ReturnMessage(message, true, itemKey, values);
                    #endregion

                    #region Send Email
                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    _messageService.Send(message);
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.SaveLog(ex.Message + ex.StackTrace);
                }
                finally
                {
                    if (inquireRateList != null)
                    {
                        inquireRateList.Clear();
                        inquireRateList = null;
                    }
                    if (unHandledInquierBys != null)
                    {
                        unHandledInquierBys.Clear();
                        unHandledInquierBys = null;
                    }
                    inquireObj = null;
                    items = null;
                    strInquireBys = string.Empty;
                    strInquireByEmails = string.Empty;
                    message = null;
                }
            };

            ThreadPool.QueueUserWorkItem(fire);
        }

        /// <summary>
        /// 询价邮件发送
        ///     发送邮件到商务
        /// </summary>
        /// <param name="inquireID"></param>
        /// <param name="inquierType"></param>
        /// <param name="currentUserID"></param>
        private void SendEmail2RespondByWithInquireRate(Guid inquireID, InquierType inquierType
            , Guid currentUserID)
        {
            WaitCallback fire = (notifyInfo) =>
            {
                if (inquireID == Guid.Empty)
                    return;
                if (currentUserID == Guid.Empty)
                    return;
                //询价对象
                BaseInquireRate paramInquireObj = null;
                //邮件对象
                Message message = null;
                //邮件模板Key
                string itemKey = string.Empty;
                //发送人Email地址
                string sendFromEmail = string.Empty;
                //接收人Email地址
                string sendTomail = string.Empty;
                //抄送Email地址
                string ccMail = string.Empty;
                try
                {
                    UserInfo LoginUserInfo = GetUserInfo(currentUserID);
                    if (LoginUserInfo == null)
                        return;
                    ApplicationContext.Current.UserId = LoginUserInfo.ID;
                    ApplicationContext.Current.EmailAddress = LoginUserInfo.EMail;
                    ApplicationContext.Current.Username = LoginUserInfo.EName;

                    //设置模板Key
                    itemKey = "InquireRateRequest";
                    //由于对象只用于保存，没有港口名称、term名称。所以需要重新获取询价列表。
                    if (inquierType == InquierType.OceanRates)
                    {
                        InquierOceanRate data = GetInquierOceanRateInfoForInquireBy(inquireID, currentUserID);
                            paramInquireObj = BuildInquireOceanContent(data);
                    }else if (inquierType == InquierType.TruckingRates)
                    {
                        InquierTruckingRate data = GetInquierTruckingRateInfoForInquireBy(inquireID, currentUserID);
                        paramInquireObj = BuildInquireTruckingContent(data);
                    }
                    else if (inquierType == InquierType.AirRates)
                    {
                        InquierAirRate data = GetInquierAirRateInfoForInquireBy(inquireID, currentUserID);
                        paramInquireObj = BuildInquireAirContent(data);
                    }
                    #region 验证数据
                    //询价对象为空则返回
                    if (paramInquireObj == null)
                        return;
                    //无询价人则返回 
                    if (paramInquireObj.InquireBysList == null)
                    {
                        paramInquireObj.InquireBysList = GetInquirePriceInquireBys(inquireID, null);
                    }
                    //设置邮件主题中显示的用户名(English Name)
                    
                    paramInquireObj.InquireByName = LoginUserInfo.EName;
                    //发送邮件地址
                    sendFromEmail = GetEmailAddress(currentUserID);
                    //接受邮件地址
                    sendTomail = GetEmailAddress(paramInquireObj.RespondByID);
                    //CC
                    ccMail = GetEmailAddress(currentUserID);

                    #endregion

                    #region Build Message

                    if (_EmailTemplateGetter == null)
                        _EmailTemplateGetter = new EmailTemplateGetter();
                    message = CreateMessageInfo(currentUserID,MessageType.Email,
                        MessageWay.Send,
                        sendTomail,
                        sendFromEmail,
                        Framework.CommonLibrary.Common.FormType.Unknown,
                        OperationType.InquireRate,
                        paramInquireObj.ID, Guid.Empty, string.Empty, string.Empty,
                        ccMail,
                        string.Empty, null, null);
                    object[] values = {paramInquireObj};
                    message = _EmailTemplateGetter.ReturnMessage(message, true, itemKey, values);
                    #endregion

                    #region Send Message

                    message.BodyFormat = BodyFormat.olFormatHTML;
                    message.State = MessageState.Success;
                    _messageService.Send(message);

                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.SaveLog(ex.Message + ex.StackTrace);
                }
                finally
                {
                    paramInquireObj = null;
                    message = null;
                }
            };

            ThreadPool.QueueUserWorkItem(fire);
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        List<BaseInquireRate> BuildInquireOceanContent(IEnumerable<InquierOceanRate> data)
        {
            return data.Select(item => BuildInquireOceanContent(item)).ToList();
        }

        BaseInquireRate BuildInquireOceanContent(InquierOceanRate item)
        {
            BaseInquireRate baseItem = new BaseInquireRate();
            baseItem.ID = item.ID;
            baseItem.No = item.No;
            baseItem.RespondByID = item.RespondByID;
            baseItem.RespondByName = item.RespondByName;
            //Exp
            baseItem.ExpCarrierName = item.ExpCarrierName;
            baseItem.ExpCommodity = item.ExpCommodity;

            baseItem.CurrencyName = item.CurrencyName;
            baseItem.IsWillBooking = item.IsWillBooking;
            baseItem.CustomerID = item.CustomerID;
            baseItem.CustomerName = item.CustomerName;
            baseItem.POLName = item.POLName;
            baseItem.PODName = item.PODName;
            baseItem.CarrierName = item.CarrierName;
            baseItem.Commodity = item.Commodity;
            baseItem.TransportClauseName = item.TransportClauseName;
            baseItem.TotalSurcharge = item.TotalSurcharge;
            baseItem.DurationFrom = item.DurationFrom;
            baseItem.DurationTo = item.DurationTo;
            
            //InquireBysList
            baseItem.InquireBysList = item.InquireBysList;
            //UnitRates
            baseItem.UnitRates = item.UnitRates;
            if (baseItem.UnitRates != null && baseItem.UnitRates.Count > 0)
            {
                baseItem.UnitRateString = item.UnitRates.Aggregate("",
                                (current, itemUnit) => current + (itemUnit.UnitName + "=" + itemUnit.Rate.ToString() + " , "));
            }
            return baseItem;
        }

        List<BaseInquireRate> BuildInquireAirContent(IEnumerable<InquierAirRate> data)
        {
            return data.Select(item => BuildInquireAirContent(item)).ToList();
        }

        BaseInquireRate BuildInquireAirContent(InquierAirRate item)
        {
            BaseInquireRate baseItem = new BaseInquireRate();
            baseItem.ID = item.ID;
            baseItem.No = item.No;
            baseItem.RespondByID = item.RespondByID;
            baseItem.RespondByName = item.RespondByName;
            //Exp
            baseItem.ExpCarrierName = item.ExpCarrierName;
            baseItem.ExpCommodity = item.ExpCommodity;

            baseItem.CurrencyName = item.CurrencyName;
            baseItem.IsWillBooking = item.IsWillBooking;
            baseItem.CustomerID = item.CustomerID;
            baseItem.CustomerName = item.CustomerName;
            baseItem.POLName = item.POLName;
            baseItem.PODName = item.PODName;
            baseItem.CarrierName = item.CarrierName;
            baseItem.Commodity = item.Commodity;
            baseItem.TransportClauseName = item.TransportClauseName;
            baseItem.TotalSurcharge = item.TotalSurcharge;
            baseItem.DurationFrom = item.DurationFrom;
            baseItem.DurationTo = item.DurationTo;
            //InquireBysList
            baseItem.InquireBysList = item.InquireBysList;
            //UnitRates
            baseItem.UnitRates = item.UnitRates;
            return baseItem;
        }

        List<BaseInquireRate> BuildInquireTruckingContent(IEnumerable<InquierTruckingRate> data)
        {
            return data.Select(item => BuildInquireTruckingContent(item)).ToList();
        }

        BaseInquireRate BuildInquireTruckingContent(InquierTruckingRate item)
        {
            BaseInquireRate baseItem = new BaseInquireRate();
            baseItem.ID = item.ID;
            baseItem.No = item.No;
            baseItem.RespondByID = item.RespondByID;
            baseItem.RespondByName = item.RespondByName;
            //Exp
            baseItem.ExpCarrierName = item.ExpCarrierName;
            baseItem.ExpCommodity = item.ExpCommodity;

            baseItem.CurrencyName = item.CurrencyName;
            baseItem.IsWillBooking = item.IsWillBooking;
            baseItem.CustomerID = item.CustomerID;
            baseItem.CustomerName = item.CustomerName;
            baseItem.POLName = item.POLName;
            baseItem.PODName = item.PODName;
            baseItem.CarrierName = item.CarrierName;
            baseItem.Commodity = item.Commodity;
            baseItem.TransportClauseName = item.TransportClauseName;
            baseItem.TotalSurcharge = item.TotalSurcharge;
            baseItem.DurationFrom = item.DurationFrom;
            baseItem.DurationTo = item.DurationTo;
            baseItem.UnitRateString = string.Format("Rate= {0}, FUEL = {1} , Total={2}",item.Rate,item.FUEL,item.Total);
            //InquireBysList
            baseItem.InquireBysList = item.InquireBysList;
            //UnitRates
            baseItem.UnitRates = item.UnitRates;
            return baseItem;
        }
        #endregion

        #region Comment Code - Discussing UnEnable
        /// <summary>
        /// 获取Discussing列表
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<InquireDiscussing> GetInquireRateDiscussingList(Guid inquireRateID, Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetInquireDiscussingList");

                db.AddInParameter(dbCommand, "@InquirePriceID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<InquireDiscussing> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new InquireDiscussing
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       Content = b.Field<string>("Content"),
                                                       IsRead = b.Field<bool>("IsRead"),
                                                       //DiscussingTOID = b.Field<Guid>("ReceiverID"),似乎获取时不需要
                                                       DiscussingTOName = b.Field<string>("Receiver"),
                                                       DiscussingFromName = b.Field<string>("CreateBy"),
                                                       SentTime = (b.Table.Columns["SentTime"] == null ? DateTimeOffset.MinValue : b.Field<DateTimeOffset>("SentTime"))
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
        /// 把该条询价指定人的未读消息改成已读(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="userID">指定人</param>
        public void ChangeDiscussingToHadRead(
            Guid inquireRateID,
            Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveInquireDiscussingMarkReaded");

                db.AddInParameter(dbCommand, "@InquirePriceID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@ReceiverID", DbType.Guid, userID);
                //db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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
        /// 新增发送Discussings(海运，空运，拖车询价通用)
        /// </summary>
        /// <returns>返回询价的 "UpdateDate"  返回服务器的当前日期(因为有可能在发起此类消息时客户端的日期不对,需刷新页面)"SaveDate"</returns>
        public void SendDiscussings(
            Guid id,
            Guid inquireRateID,
            Guid fromID,
            Guid? toID,
            DateTimeOffset sentTime,
            string content)
        {
            ArgumentHelper.AssertGuidNotEmpty(inquireRateID, "inquireRateID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveInquireDiscussing");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@InquirePriceID", DbType.Guid, inquireRateID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, fromID);
                db.AddInParameter(dbCommand, "@ReceiverID", DbType.Guid, toID);
                db.AddInParameter(dbCommand, "@Content", DbType.String, content);
                db.AddInParameter(dbCommand, "@SentTime", DbType.DateTimeOffset, sentTime);
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
        #endregion
    }
}

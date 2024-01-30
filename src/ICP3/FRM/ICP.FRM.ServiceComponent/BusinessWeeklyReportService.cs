using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FRM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FRM.ServiceComponent
{
    public partial class BusinessInfoService
    {

        #region 获得商务周报表列表
        /// <summary>
        /// 获得商务周报表列表
        /// </summary>
        /// <param name="divisionID">口岸公司ID</param>
        /// <param name="weeklyDate">周日期</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<BusinessWeeklyReportInfo> GetBusinessWeeklyReportList(Guid? divisionID, string weeklyDate, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetBusinessWeeklyReportList");

                db.AddInParameter(dbCommand, "@DivisionID", DbType.Guid, divisionID);
                db.AddInParameter(dbCommand, "@WeeklyDate", DbType.String, weeklyDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BusinessWeeklyReportInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new BusinessWeeklyReportInfo
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              WeeklyDate = b.Field<String>("WeeklyDate"),
                                                              DivisionID = b.Field<Guid>("DivisionID"),
                                                              DivisionName = b.Field<String>("DivisionName"),
                                                              ShiplineID = b.Field<Guid?>("ShiplineID"),
                                                              ShiplineName = b.Field<String>("ShiplineName"),
                                                              CarrierID = b.Field<Guid?>("CarrierID"),
                                                              CarrierName = b.Field<String>("CarrierName"),
                                                              CarrierCode = b.Field<String>("CarrierCode"),
                                                              Rates = b.Field<String>("Rates"),
                                                              ShippingSpace = b.Field<String>("ShippingSpace"),
                                                              Description = b.Field<String>("Description"),
                                                              CreateByID = b.Field<Guid>("CreateByID"),
                                                              CreateByName = b.Field<String>("CreateByName"),
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
        #endregion

        #region 获得指定月份商务周报的数据
        public List<BusinessWeeklyReportData> GetBusinessWeeklyReportDataList(string weeklyData, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetBusinessWeeklyReportList");

                db.AddInParameter(dbCommand, "@DivisionID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@WeeklyDate", DbType.String, weeklyData);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                Guid YDOfficeID = new Guid("1D95B3C1-CB53-4719-9ED3-B8D2B98CA8A5");

                #region 商务周报信息
                List<BusinessWeeklyReportInfo> weeklyDataList = (from b in ds.Tables[0].AsEnumerable()
                                                                 select new BusinessWeeklyReportInfo
                                                                 {
                                                                     ID = b.Field<Guid>("ID"),
                                                                     WeeklyDate = b.Field<String>("WeeklyDate"),
                                                                     DivisionID = b.Field<Guid>("DivisionID"),
                                                                     DivisionName = b.Field<String>("DivisionName"),
                                                                     ShiplineID = b.Field<Guid?>("ShiplineID"),
                                                                     ShiplineName = b.Field<String>("ShiplineName"),
                                                                     CarrierID = b.Field<Guid?>("CarrierID"),
                                                                     CarrierName = b.Field<String>("CarrierName"),
                                                                     CarrierCode = b.Field<String>("CarrierCode"),
                                                                     Rates = b.Field<String>("Rates"),
                                                                     ShippingSpace = b.Field<String>("ShippingSpace"),
                                                                     Description = b.Field<String>("Description"),
                                                                     CreateByID = b.Field<Guid>("CreateByID"),
                                                                     CreateByName = b.Field<String>("CreateByName"),
                                                                     CreateDate = b.Field<DateTime>("CreateDate"),
                                                                     UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                 }).ToList();
                #endregion

                #region 经理批注
                List<BusinessWeeklyReportList_Manager> managerDataList = (from b in ds.Tables[1].AsEnumerable()
                                                                          select new BusinessWeeklyReportList_Manager
                                                                 {
                                                                     ID = b.Field<Guid>("ID"),
                                                                     WeeklyDate = b.Field<String>("WeeklyDate"),
                                                                     Description = b.Field<String>("Description"),
                                                                     CompanyID = b.Field<Guid>("CompanyID"),
                                                                     CompanyName = b.Field<String>("CompanyName"),
                                                                     ShipLineID = b.Field<Guid>("ShipLineID"),
                                                                     ShipLineName = b.Field<String>("ShipLineName"),
                                                                     CreateByID = b.Field<Guid>("CreateByID"),
                                                                     CreateByName = b.Field<String>("CreateByName"),
                                                                     CreateDate = b.Field<DateTime>("CreateDate"),
                                                                     UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                 }).ToList();

                if (managerDataList == null)
                {
                    managerDataList = new List<BusinessWeeklyReportList_Manager>();
                }
                #endregion

                List<BusinessWeeklyReportData> DataList = new List<BusinessWeeklyReportData>();

                DateTime? upDateTime = null;

                #region 拼装数据
                foreach (BusinessWeeklyReportInfo datainfo in weeklyDataList)
                {
                    BusinessWeeklyReportData item = DataList.Find(delegate(BusinessWeeklyReportData bwreportInfo) { return bwreportInfo.ShiplineID == datainfo.ShiplineID && bwreportInfo.CompanyID == datainfo.DivisionID; });
                    if (item == null)
                    {
                        item = new BusinessWeeklyReportData();

                        item.ShiplineID = datainfo.ShiplineID.Value;
                        item.ShiplineName = datainfo.ShiplineName;
                        item.CompanyID = datainfo.DivisionID;
                        item.CompanyName = datainfo.DivisionName;


                        upDateTime = GetIsDataUpdate(datainfo.CreateDate, datainfo.UpdateDate);

                        if (upDateTime != null)
                        {
                            if (!string.IsNullOrEmpty(datainfo.ShippingSpace))
                            {
                                item.Marketing = "#&;" + upDateTime.Value.ToShortDateString() + " " + datainfo.ShippingSpace.Replace("\r\n", System.Environment.NewLine) + "#&^";
                            }
                            if (!string.IsNullOrEmpty(datainfo.Description))
                            {
                                item.SellingGuide = "#&;" + upDateTime.Value.ToShortDateString() + " " + datainfo.Description + "#&^";
                            }
                            item.IsUpdate = true;
                        }
                        else
                        {
                            item.Marketing = datainfo.ShippingSpace;
                            item.SellingGuide = datainfo.Description;
                        }

                        #region 拼装经理批注
                        //经理批注按时间倒序
                        List<BusinessWeeklyReportList_Manager> fileManagerList = (from d in managerDataList where d.CompanyID == datainfo.DivisionID && d.ShipLineID == datainfo.ShiplineID orderby d.CreateDate descending select d).ToList();
                        if (fileManagerList != null)
                        {
                            foreach (BusinessWeeklyReportList_Manager managerInfo in fileManagerList)
                            {
                                if (!string.IsNullOrEmpty(managerInfo.Description) && managerInfo.Description.Trim().Length > 0)
                                {
                                    string str = string.Empty;
                                    upDateTime = GetIsDataUpdate(managerInfo.CreateDate, managerInfo.UpdateDate);
                                    if (upDateTime != null)
                                    {
                                        str = "#&;" + upDateTime.Value.ToShortDateString() + " " + managerInfo.CreateByName + ":" + managerInfo.Description;
                                        item.IsUpdate = true;
                                    }
                                    else
                                    {
                                        str = managerInfo.CreateByName + ":" + managerInfo.Description;
                                    }

                                    if (!string.IsNullOrEmpty(item.MangerComments) && item.MangerComments.Trim().Length > 0)
                                    {
                                        item.MangerComments = item.MangerComments + System.Environment.NewLine + str;
                                    }
                                    else
                                    {
                                        item.MangerComments = str;

                                    }
                                }
                            }
                        }
                        #endregion

                        DataList.Add(item);
                    }
                    else
                    {
                        #region 追加数据
                        string marketing = string.Empty;
                        string sellingGuide = string.Empty;

                        upDateTime = GetIsDataUpdate(datainfo.CreateDate, datainfo.UpdateDate);
                        if (upDateTime != null)
                        {
                            marketing = item.Marketing + System.Environment.NewLine + "#&;" + upDateTime.Value.ToShortDateString() + " " + datainfo.ShippingSpace + "#&^";
                            sellingGuide = item.SellingGuide + System.Environment.NewLine + "#&;" + upDateTime.Value.ToShortDateString() + " " + datainfo.Description + "#&^";

                            item.IsUpdate = true;
                        }
                        else
                        {
                            marketing = item.Marketing + System.Environment.NewLine + datainfo.ShippingSpace;
                            sellingGuide = item.SellingGuide + System.Environment.NewLine + datainfo.Description;
                        }

                        if (!string.IsNullOrEmpty(datainfo.ShippingSpace) && datainfo.ShippingSpace.Trim().Length > 0)
                        {
                            item.Marketing = marketing;
                        }
                        if (!string.IsNullOrEmpty(datainfo.Description) && datainfo.Description.Trim().Length > 0)
                        {
                            item.SellingGuide = sellingGuide;
                        }
                        #endregion
                    }

                    if (item.CompanyID == YDOfficeID)
                    {
                        item.OrderByCode = "1";
                    }
                    else
                    {
                        item.OrderByCode = "0";
                    }


                }
                #endregion

                #region 处理数据
                SautinSoft.HtmlToRtf obj = new SautinSoft.HtmlToRtf();
                string rtfText = string.Empty;
                foreach (BusinessWeeklyReportData item in DataList)
                {
                    if (!item.Marketing.IsNullOrEmpty())
                    {
                        item.Marketing = item.Marketing.Replace(System.Environment.NewLine, "<br>");
                        item.Marketing = item.Marketing.Replace("#&;", "<span  style='color:red'>");
                        item.Marketing = item.Marketing.Replace("#&^", "</span>");
                        item.Marketing = @"<span style='font-size:13px;font-family:SimHei'>"
                                                        + item.Marketing +
                                                    @"</span>";

                        rtfText = obj.ConvertString(item.Marketing);
                        rtfText = rtfText.Substring(0, rtfText.Length - 320);
                        rtfText += "}";
                        item.Marketing = rtfText;
                    }
                    if (!item.SellingGuide.IsNullOrEmpty())
                    {
                        item.SellingGuide = item.SellingGuide.Replace(System.Environment.NewLine, "<br>");
                        item.SellingGuide = item.SellingGuide.Replace("#&;", "<span  style='color:red'>");
                        item.SellingGuide = item.SellingGuide.Replace("#&^", "</span>");
                        item.SellingGuide = @"<span style='font-size:13px;font-family:SimHei'>"
                                                            + item.SellingGuide +
                                                        @"</span>";


                        rtfText = obj.ConvertString(item.SellingGuide);
                        rtfText = rtfText.Substring(0, rtfText.Length - 320);
                        rtfText += "}";
                        item.SellingGuide = rtfText;
                    }
                    if (!item.MangerComments.IsNullOrEmpty())
                    {
                        item.MangerComments = item.MangerComments.Replace(System.Environment.NewLine, "<br>");
                        item.MangerComments = item.MangerComments.Replace("#&;", "<span  style='color:red'>");
                        item.MangerComments = item.MangerComments.Replace("#&^", "</span>");
                        item.MangerComments = @"<span style='font-size:13px;font-family:SimHei'>"
                                                                    + item.MangerComments +
                                                        @"</span>";

                        rtfText = obj.ConvertString(item.MangerComments);
                        rtfText = rtfText.Substring(0, rtfText.Length - 320);
                        rtfText += "}";
                        item.MangerComments = rtfText;
                    }
                }
                #endregion

                return DataList;
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
        /// 判断是否最近更新(最近24个小时内更新的)
        /// </summary>
        /// <param name="createDate"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        private DateTime? GetIsDataUpdate(DateTime createDate, DateTime? updateDate)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            if (updateDate == null)
            {
                dt1 = createDate;
            }
            else
            {
                dt1 = updateDate.Value;
            }
            TimeSpan ts = dt2.Subtract(dt1);
            if (ts.TotalHours <= 24)
            {
                return dt1;
            }

            return null;
        }
        #endregion

        #region 获得商务周报经理批注
        public List<BusinessWeeklyReportList_Manager> GetBusinessWeeklyReportManagerList(string weeklyData, Guid shiplineID, Guid companyID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetBusinessWeeklyReport_Manager");

                db.AddInParameter(dbCommand, "@WeeklyDate", DbType.String, weeklyData);
                db.AddInParameter(dbCommand, "@DivisionID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@ShiplineID", DbType.Guid, shiplineID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BusinessWeeklyReportList_Manager> managerDataList = (from b in ds.Tables[0].AsEnumerable()
                                                                          select new BusinessWeeklyReportList_Manager
                                                                          {
                                                                              ID = b.Field<Guid>("ID"),
                                                                              WeeklyDate = b.Field<String>("WeeklyDate"),
                                                                              Description = b.Field<String>("Description"),
                                                                              CompanyID = b.Field<Guid>("CompanyID"),
                                                                              CompanyName = b.Field<String>("CompanyName"),
                                                                              ShipLineID = b.Field<Guid>("ShipLineID"),
                                                                              ShipLineName = b.Field<String>("ShipLineName"),
                                                                              CreateByID = b.Field<Guid>("CreateByID"),
                                                                              CreateByName = b.Field<String>("CreateByName"),
                                                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                          }).ToList();

                if (managerDataList == null)
                {
                    managerDataList = new List<BusinessWeeklyReportList_Manager>();
                }



                return managerDataList;
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

        #region 保存商务周报表信息
        /// <summary>
        /// 保存商务周报表信息
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="weeklyDate">周日期</param>
        /// <param name="divisionIDs">口岸公司ID</param>
        /// <param name="shiplineIDs">航线</param>
        /// <param name="carrierIDs">船公司</param>
        /// <param name="rates">Rates</param>
        /// <param name="shippingSpace">ShippingSpace</param>
        /// <param name="descriptions">Descriptions</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="createByID">创建人</param>
        /// <param name="createDate">创建时间</param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveBusinessWeeklyReport(BusinessWeeklyReportSaveRequest saveRequest)
        {
            try
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                Database db = DatabaseFactory.CreateDatabase();

                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveBusinessWeeklyReport");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, saveRequest.IDs.Join());
                db.AddInParameter(dbCommand, "@WeeklyDate", DbType.String, saveRequest.WeeklyDate);
                db.AddInParameter(dbCommand, "@DivisionIDs", DbType.String, saveRequest.DivisionIDs.Join());
                db.AddInParameter(dbCommand, "@ShiplineIDs", DbType.String, saveRequest.ShiplineIDs.Join());
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, saveRequest.CarrierIDs.Join());
                db.AddInParameter(dbCommand, "@Rates", DbType.String, saveRequest.Rates.Join());
                db.AddInParameter(dbCommand, "@ShippingSpaces", DbType.String, saveRequest.ShippingSpace.Join());
                db.AddInParameter(dbCommand, "@Descriptions", DbType.String, saveRequest.Descriptions.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, saveRequest.UpdateDates.Join());
                db.AddInParameter(dbCommand, "@CreateByID", DbType.Guid, saveRequest.CreateByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.isEnglish);

                ManyResult manyResult = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });

                result.Add(saveRequest.RequestId, new SaveResponse { RequestId = saveRequest.RequestId, ManyResult = manyResult });

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

        #region 删除商务周报表信息
        /// <summary>
        /// 删除商务周报表信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="removeID">删除人</param>
        /// <returns></returns> 
        public bool RemoveBusinessWeeklyReport(
            Guid id,
            DateTime? updateDate,
            Guid removeID,
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveBusinessWeeklyReport");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveID", DbType.Guid, removeID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #region 获得服务器的时间
        /// <summary>
        /// 获得服务器的时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerDate()
        {
            return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        }
        #endregion

        #region 发帖及更新帖子
        public bool PostBusinessWeeklyToICP2(string Subject, string Body, string IpAddress, string WeekName, Guid SaveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspPostBusinessWeeklyToICP2");

                db.AddInParameter(dbCommand, "@Subject", DbType.String, Subject);
                db.AddInParameter(dbCommand, "@Body", DbType.String, Body);
                db.AddInParameter(dbCommand, "@IpAddress", DbType.String, IpAddress);
                db.AddInParameter(dbCommand, "@WeekName", DbType.String, WeekName);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, SaveByID);


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

        #region 保存商务周报经理批注
        /// <summary>
        /// 保存商务周报经理批注
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        public ManyResult SaveWeeklyReportManager(BusinessWeeklyReport_ManagerSaveRequest saveRequest)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();

                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveBusinessWeeklyReport_Manager");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, saveRequest.IDs.Join());
                db.AddInParameter(dbCommand, "@MonthDate", DbType.String, saveRequest.MonthDate);
                db.AddInParameter(dbCommand, "@WeeklyDate", DbType.String, saveRequest.WeeklyDate);
                db.AddInParameter(dbCommand, "@DivisionIDs", DbType.String, saveRequest.DivisionIDs.Join());
                db.AddInParameter(dbCommand, "@ShiplineIDs", DbType.String, saveRequest.ShiplineIDs.Join());
                db.AddInParameter(dbCommand, "@JobIDs", DbType.String, saveRequest.JobIDs.Join());
                db.AddInParameter(dbCommand, "@Descriptions", DbType.String, saveRequest.Descriptions.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, saveRequest.UpdateDates.Join());
                db.AddInParameter(dbCommand, "@CreateByID", DbType.Guid, saveRequest.CreateByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, saveRequest.isEnglish);

                ManyResult manyResult = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });

                return manyResult;
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

        #region 删除商务周报经理批注
        /// <summary>
        /// 删除商务周报经理批注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDate"></param>
        /// <param name="removeID"></param>
        /// <param name="isEnglish"></param>
        public void RemoveWeeklyReportManager(Guid id,
            DateTime? updateDate,
            Guid removeID,
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveBusinessWeeklyRport_Manager");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveID", DbType.Guid, removeID);
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

        #region 获得订舱统计数据
        /// <summary>
        /// 获得订舱统计数据
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="shipLineIDs">航线ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="salesType">揽货类型</param>
        /// <param name="isValid">有效性</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public List<BookingReportData> GetBookingReportDataList(Guid[] companyIDs, Guid[] shipLineIDs, string customerIDs, string carrierIDs, Guid? salesType, bool? isValid, DateTime? beginDate, DateTime? endDate, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("frm.uspGetBookingDataReport");

                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@ShipLineIds", DbType.String, shipLineIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, customerIDs);
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, carrierIDs);
                db.AddInParameter(dbCommand, "@SalesType", DbType.Guid, salesType);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@isValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BookingReportData> results = (from d in ds.Tables[0].AsEnumerable()
                                                   select new BookingReportData
                                                   {
                                                       CancelRemark = d.Field<string>("CancelRemark"),
                                                       ClosingDate = d.Field<DateTime?>("ClosingDate"),
                                                       ETD = d.Field<DateTime?>("ETD"),
                                                       Commodities = d.Field<string>("Commodities"),
                                                       ContainerCount = d.Field<decimal>("ContainerCount"),
                                                       ContainerType = d.Field<string>("ContainerType"),
                                                       ContractNo = d.Field<string>("ContractNo"),
                                                       CustomerName = d.Field<string>("CustomerName"),
                                                       CustomerService = d.Field<string>("CustomerService"),
                                                       FilerName = d.Field<string>("FilerName"),
                                                       ID = d.Field<Guid>("ID"),
                                                       PODName = d.Field<string>("PODName"),
                                                       POLName = d.Field<string>("POLName"),
                                                       Profit = d.Field<decimal>("Profit"),
                                                       Remark = d.Field<string>("Remark"),
                                                       SalesName = d.Field<string>("SalesName"),
                                                       SalesType = d.Field<string>("SalesType"),
                                                       SONO = d.Field<string>("SONO"),
                                                       VoyageName = d.Field<string>("VoyageName"),
                                                       CompanyName = d.Field<string>("CompanyName"),
                                                       ShipLineName = d.Field<string>("ShipLineName"),
                                                       CarrierName = d.Field<string>("CarrierName")
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

    }
}

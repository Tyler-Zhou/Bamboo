using System;
using System.Collections.Generic;
using System.Linq;
using ICP.TMS.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Helper;
using System.Transactions;

namespace ICP.TMS.ServiceComponent
{
    public class TruckBookingService : ITruckBookingService
    {
        #region 拖车业务

        #region 获得拖车业务列表
        /// <summary>
        /// 获得拖车业务列表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="no">业务事情</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="mblNo">提单号</param>
        /// <param name="customerRefNo">客户参考单号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="truckType">业务类型</param>
        /// <param name="state">状态</param>
        /// <param name="maxRowCount">最大行数</param>
        /// <param name="dateSearchType">时间查询类型</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<TruckBookingsList> GetTruckBookingsList(
            Guid[] companyIDs,
            string no,
            string containerNo,
            string mblNo,
            string customerRefNo,
            string customerName,
            SearchTruckBookingType truckType,
            Int32 state,
            bool? valid,
            int maxRowCount,
            TruckBusinessDateSeachType dateSearchType,
            DateTime? beginDate,
            DateTime? endDate,
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTruckBusinessList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerNo);
                db.AddInParameter(dbCommand, "@MblNO", DbType.String, mblNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, customerRefNo);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, truckType);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Byte, dateSearchType);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@Valid", DbType.Boolean, valid);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<TruckBookingsList> list = (from d in ds.Tables[0].AsEnumerable()
                                                select new TruckBookingsList
                                                {
                                                    BookingID = d.Field<Guid>("ID"),
                                                    ContainerID = d.Field<Guid?>("ContainerID"),
                                                    State = (TruckBusinessState)d.Field<Byte>("State"),
                                                    No = d.Field<String>("No"),
                                                    Type = (TruckBookingType)d.Field<Byte>("Type"),
                                                    ContainerNo = d.Field<String>("ContainerNo"),
                                                    ContainerType = d.Field<String>("ContainerType"),
                                                    TrayNo = d.Field<String>("TrayNo"),
                                                    TruckDate = d.Field<DateTime?>("TruckDate"),
                                                    TruckPlace = d.Field<String>("TruckPlace"),
                                                    LastFreeDate = d.Field<DateTime?>("LastFreeDate"),
                                                    DriverName = d.Field<String>("DriverName"),
                                                    TruckNo = d.Field<String>("CarNo"),
                                                    CustomerName = d.Field<String>("CustomerName"),
                                                    CustomerRefNo = d.Field<String>("CustomerRefNo"),
                                                    CreateByName = d.Field<String>("CreateByName"),
                                                    CreateDate = d.Field<DateTime>("CreateDate"),
                                                    Remark = d.Field<String>("Remark"),
                                                    UpdateByName = d.Field<String>("UpdateByName"),
                                                    BookingUpdateDate = d.Field<DateTime?>("BookingUpdateDate"),
                                                    IsValid = d.Field<Boolean>("IsValid")
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

        #region 获得拖车业务列表
        /// <summary>
        /// 获得拖车列表
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<TruckBookingsList> GetTruckBookingsListByIds(Guid[] ids, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTruckBusinessListByIds");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<TruckBookingsList> list = (from d in ds.Tables[0].AsEnumerable()
                                                select new TruckBookingsList
                                                {
                                                    BookingID = d.Field<Guid>("ID"),
                                                    ContainerID = d.Field<Guid>("ContainerID"),
                                                    State = (TruckBusinessState)d.Field<Byte>("State"),
                                                    No = d.Field<String>("No"),
                                                    Type = (TruckBookingType)d.Field<Byte>("Type"),
                                                    ContainerNo = d.Field<String>("ContainerNo"),
                                                    ContainerType = d.Field<String>("ContainerType"),
                                                    TrayNo = d.Field<String>("TrayNo"),
                                                    TruckDate = d.Field<DateTime?>("TruckDate"),
                                                    TruckPlace = d.Field<String>("TruckPlace"),
                                                    LastFreeDate = d.Field<DateTime?>("LastFreeDate"),
                                                    DriverName = d.Field<String>("DriverName"),
                                                    TruckNo = d.Field<String>("CarNo"),
                                                    CustomerName = d.Field<String>("CustomerName"),
                                                    CustomerRefNo = d.Field<String>("CustomerRefNo"),
                                                    CreateByName = d.Field<String>("CreateByName"),
                                                    CreateDate = d.Field<DateTime>("CreateDate"),
                                                    Remark = d.Field<String>("Remark"),
                                                    IsValid = d.Field<Boolean>("IsValid")
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

        #region 获得拖车业务详细信息
        /// <summary>
        /// 获得拖车业务详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public TruckBookingsInfo GetTruckBookingsInfo(Guid id, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTruckBusinessInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                TruckBookingsInfo truckInfo = (from d in ds.Tables[0].AsEnumerable()
                                               select new TruckBookingsInfo
                                               {
                                                   ID = d.Field<Guid>("ID"),
                                                   No = d.Field<String>("No"),
                                                   CompanyID = d.Field<Guid>("CompanyID"),
                                                   CompanyName = d.Field<String>("CompanyName"),
                                                   CustomerID = d.Field<Guid>("CustomerID"),
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   CustomerRefNo = d.Field<String>("CustomerRefNo"),
                                                   SalesID = d.Field<Guid>("SalesID"),
                                                   SalesName = d.Field<String>("SalesName"),
                                                   SalesTypeID = d.Field<Guid>("SalesTypeID"),
                                                   SalesTypeName = d.Field<String>("SalesTypeName"),
                                                   Bookingmode = (BookingMode)d.Field<Byte>("BookingMode"),
                                                   BookingDate = d.Field<DateTime>("BookingDate"),
                                                   MBLNo = d.Field<String>("MBLNo"),
                                                   TruckType = (TruckBookingType)d.Field<Byte>("Type"),
                                                   CarrierID = d.Field<Guid?>("CarrierID"),
                                                   CarrierName = d.Field<String>("CarrierName"),
                                                   VesselName = d.Field<String>("VesselName"),
                                                   VoyageNo = d.Field<String>("VoyageNo"),
                                                   DeliveryAtID = d.Field<Guid?>("DeliveryAtID"),
                                                   DeliveryAtName = d.Field<String>("DeliveryAtName"),
                                                   DeliveryAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), d.Field<string>("DeliveryAtDescription")),
                                                   DeliveryDate = d.Field<DateTime?>("DeliveryDate"),
                                                   PickUpAtID = d.Field<Guid?>("PickUpAtID"),
                                                   PickUpAtName = d.Field<String>("PickAtName"),
                                                   PickUpAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), d.Field<string>("PickUpAtDescription")),
                                                   PickUpAtDate = d.Field<DateTime?>("PickUpAtDate"),
                                                   ReturnLocationID = d.Field<Guid?>("ReturnLocationID"),
                                                   ReturnLocationName = d.Field<String>("ReturnLocationName"),
                                                   ReturnLocationDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), d.Field<string>("ReturnLocationDescription")),
                                                   ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), d.Field<string>("ContainerDescription")),
                                                   Remark = d.Field<String>("Remark"),
                                                   UpdateDate = d.Field<DateTime?>("UpdateDate"),
                                                   CreateByDate = d.Field<DateTime>("CreateDate"),
                                                   CreateByID = d.Field<Guid>("CreateByID"),
                                                   CreateByName = d.Field<String>("CreateByName"),
                                                   IsValid = d.Field<Boolean>("IsValid"),
                                                   IsDirty = false,


                                                   TruckContainersList = (from c in ds.Tables[1].AsEnumerable()
                                                                          select new TruckContainersList
                                                                          {
                                                                              ID = c.Field<Guid>("ID"),
                                                                              TruckBookingID = c.Field<Guid>("TruckBookingID"),
                                                                              State = (TruckBusinessState)c.Field<Byte>("State"),
                                                                              No = c.Field<String>("No"),
                                                                              IndexNo = c.Field<String>("SerialNo"),
                                                                              TypeID = c.Field<Guid>("TypeID"),
                                                                              TrayNo = c.Field<String>("TrayNo"),
                                                                              TruckDate = c.Field<DateTime?>("TruckDate"),
                                                                              TruckPlace = c.Field<String>("TruckPlace"),
                                                                              LastFreeDate = c.Field<DateTime?>("LastFreeDate"),
                                                                              DriverID = c.Field<Guid?>("DriverID"),
                                                                              CarID = c.Field<Guid?>("CarID"),
                                                                              PickUpAtDate = c.Field<DateTime?>("PickUpAtDate"),
                                                                              DeliveryDate = c.Field<DateTime?>("DeliveryDate"),
                                                                              ReturnDate = c.Field<DateTime?>("ReturnDate"),
                                                                              Remark = c.Field<String>("Remark"),
                                                                              UpdateDate = c.Field<DateTime?>("UpdateDate"),
                                                                              IsDirty = false,

                                                                          }).ToList()

                                               }).SingleOrDefault();


                return truckInfo;

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

        #region 作废/激活拖车业务
        /// <summary>
        /// 作废/激活拖车业务
        /// </summary>
        /// <param name="id">业务ID</param>
        /// <param name="isCancel">是否作废,True为作废;False为激活</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult CancelTruckBookings(Guid id,
               bool isCancel,
               Guid changeByID,
               DateTime? updateDate,
               bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCancelTruckBusiness");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);
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

        #region 保存业务信息
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">业务号</param>
        /// <param name="truckType">业务类型</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="customerRefNo">客户参考号</param>
        /// <param name="salesID">揽货人</param>
        /// <param name="salesTypeID">揽货类型</param>
        /// <param name="bookingMode">委托类型</param>
        /// <param name="bookingDate">委托日期</param>
        /// <param name="mblNo">提单号</param>
        /// <param name="carrierID">船东</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="containerDescription">箱需求</param>
        /// <param name="remark">备注</param>
        /// <param name="pickUpAtID">提柜地</param>
        /// <param name="pickUpAtDescription">提柜地描述</param>
        /// <param name="pickUpAtDate">提柜时间</param>
        /// <param name="deliveryAtID">交货地</param>
        /// <param name="deliveryAtDescription">交货地描述</param>
        /// <param name="deliveryAtDate">交货时间</param>
        /// <param name="returnLocationID">还柜地</param>
        /// <param name="returnLocationDescription">还柜地描述</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult SaveTruckBookings(TruckBookingSaveRequest truckBooking)
        {
            try
            {

                string strcontainerDescription = (truckBooking.ContainerDescription == null || truckBooking.ContainerDescription.Containers.Count == 0) ? null : SerializerHelper.SerializeToString<ContainerDescription>(truckBooking.ContainerDescription, true, false);
                string strpickUpAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(truckBooking.PickUpAtDescription, true, false);
                string strreturnLocationDescription = SerializerHelper.SerializeToString<CustomerDescription>(truckBooking.ReturnLocationDescription, true, false);
                string strdeliveryAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(truckBooking.ReturnLocationDescription, true, false);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveTruckBookingsInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, truckBooking.ID);
                db.AddInParameter(dbCommand, "@No", DbType.String, truckBooking.No);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, truckBooking.TruckType);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, truckBooking.CompanyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, truckBooking.CustomerID);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, truckBooking.CustomerRefNo);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, truckBooking.SalesID);
                db.AddInParameter(dbCommand, "@SalesTypeID", DbType.Guid, truckBooking.SalesTypeID);
                db.AddInParameter(dbCommand, "@BookingMode", DbType.Byte, truckBooking.Bookingmode);
                db.AddInParameter(dbCommand, "@BookingDate", DbType.DateTime, truckBooking.BookingDate);
                db.AddInParameter(dbCommand, "@MBLNo", DbType.String, truckBooking.MBLNo);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, truckBooking.CarrierID);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, truckBooking.VoyageNo);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, truckBooking.VesselName);
                db.AddInParameter(dbCommand, "@ContainerDescription", DbType.String, strcontainerDescription);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, truckBooking.Remark);
                db.AddInParameter(dbCommand, "@PickUpAtID", DbType.Guid, truckBooking.PickUpAtID);
                db.AddInParameter(dbCommand, "@PickUpAtDescription", DbType.String, strpickUpAtDescription);
                db.AddInParameter(dbCommand, "@PickUpAtDate", DbType.DateTime, truckBooking.PickUpAtDate);
                db.AddInParameter(dbCommand, "@DeliveryAtID", DbType.Guid, truckBooking.DeliveryAtID);
                db.AddInParameter(dbCommand, "@DeliveryAtDescription", DbType.String, strdeliveryAtDescription);
                db.AddInParameter(dbCommand, "@DeliveryDate", DbType.DateTime, truckBooking.DeliveryDate);
                db.AddInParameter(dbCommand, "@ReturnLocationID", DbType.Guid, truckBooking.ReturnLocationID);
                db.AddInParameter(dbCommand, "@ReturnLocationDescription", DbType.String, strreturnLocationDescription);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, truckBooking.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, truckBooking.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, truckBooking.IsEnglish);


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "No" });

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

        #region 获得派车箱列表
        /// <summary>
        /// 获得派车箱列表
        /// </summary>
        /// <param name="bookingID">拖车业务ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<TruckContainersList> GetTruckContainersList(Guid bookingID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTruckContainerList");

                db.AddInParameter(dbCommand, "@TruckBookingID", DbType.Guid, bookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<TruckContainersList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new TruckContainersList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         TruckBookingID = b.Field<Guid>("TruckBookingID"),
                                                         State = (TruckBusinessState)b.Field<Byte>("State"),
                                                         No = b.Field<String>("No"),
                                                         ContainerTypeName = b.Field<String>("ContainerTypeName"),
                                                         TrayNo = b.Field<String>("TrayNo"),
                                                         LastFreeDate = b.Field<DateTime>("LastFreeDate"),
                                                         DriverID = b.Field<Guid>("DriverID"),
                                                         DriverName = b.Field<String>("DriverName"),
                                                         CarID = b.Field<Guid>("CarID"),
                                                         CarNo = b.Field<String>("CarNo"),
                                                         PickUpAtDate = b.Field<DateTime>("PickUpAtDate"),
                                                         DeliveryDate = b.Field<DateTime>("DeliveryDate"),
                                                         ReturnDate = b.Field<DateTime>("ReturnDate"),
                                                         Remark = b.Field<String>("Remark"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         IndexNo = b.Field<String>("SerialNo")
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

        #region 保存派车箱列表

        /// <summary>
        /// 保存派车箱列表信息
        /// </summary>
        /// <param name="bookingID">业务ID</param>
        /// <param name="ids">ID集合</param>
        /// <param name="nos">箱号集合</param>
        /// <param name="states">状态集合</param>
        /// <param name="typeIDs">箱型ID集合</param>
        /// <param name="trayNos">托盘号集合</param>
        /// <param name="truckDates">派车时间集合</param>
        /// <param name="truckPlaces">地点集合</param>
        /// <param name="lastFreeDates">免堆日集合</param>
        /// <param name="pickUpAtDates">提柜日集合</param>
        /// <param name="deliveryDates">交货日集合</param>
        /// <param name="returnDates">还柜日集合</param>
        /// <param name="driverIDs">司机ID集合</param>
        /// <param name="carIDs">车辆ID集合</param>
        /// <param name="remarks">备注集合</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public ManyResult SaveContainersList(TruckContainersSaveRequest containers)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveTruckContainersInfo");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, containers.IDs.Join());
                db.AddInParameter(dbCommand, "@TruckBookingID", DbType.Guid, containers.TruckBookingID);
                db.AddInParameter(dbCommand, "@Nos", DbType.String, containers.Nos.Join());
                db.AddInParameter(dbCommand, "@TypeIDs", DbType.String, containers.TypeIDs.Join());
                db.AddInParameter(dbCommand, "@TrayNos", DbType.String, containers.TrayNos.Join());
                db.AddInParameter(dbCommand, "@States", DbType.String, containers.States.Join());
                db.AddInParameter(dbCommand, "@TruckDates", DbType.String, containers.TruckDates.Join());
                db.AddInParameter(dbCommand, "@TruckPlaces", DbType.String, containers.TruckPlaces.Join());
                db.AddInParameter(dbCommand, "@LastFreeDates", DbType.String, containers.LastFreeDates.Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, containers.Remarks.Join());
                db.AddInParameter(dbCommand, "@PickUpAtDates", DbType.String, containers.PickUpAtDates.Join());
                db.AddInParameter(dbCommand, "@DeliveryDates", DbType.String, containers.DeliveryDates.Join());
                db.AddInParameter(dbCommand, "@ReturnDates", DbType.String, containers.ReturnDates.Join());
                db.AddInParameter(dbCommand, "@DriverIDs", DbType.String, containers.DriverIDs.Join());
                db.AddInParameter(dbCommand, "@CarIDs", DbType.String, containers.CarIDs.Join());
                db.AddInParameter(dbCommand, "@SerialNos", DbType.String, containers.IndexNos.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, containers.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, containers.UpdateDates.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, containers.IsEnglish);


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
        #endregion

        #region 删除派车箱列表
        /// <summary>
        /// 删除箱列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="deleteByID">删除人</param>
        /// <param name="isEnglish">是否英文版本</param>
        public void DeleteContainer(Guid[] ids, Guid bookingID, DateTime?[] updateDates, Guid deleteByID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveTruckContainerInfo");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, bookingID);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, deleteByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
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

        #region 以事务的方式保存拖车业务信息跟箱信息
        /// <summary>
        /// 以事务的方式保存拖车业务信息跟箱信息
        /// </summary>
        /// <param name="truckBooking">业务保存实体</param>
        /// <param name="containerList">箱列表</param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveTruckBookingWithTrans(TruckBookingSaveRequest truckBooking, List<TruckContainersSaveRequest> containerList)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                ///保存业务信息
                if (truckBooking != null)
                {
                    result.Add(truckBooking.RequestId,
                        new SaveResponse { RequestId = truckBooking.RequestId, SingleResult = this.SaveTruckBookings(truckBooking) });
                }
                Guid bookingID = Guid.Empty;
                if (truckBooking != null && truckBooking.ID == Guid.Empty)
                {
                    bookingID = result[truckBooking.RequestId].SingleResult.GetValue<Guid>("ID");
                }
                //保存集装箱
                if (containerList != null)
                {
                    foreach (TruckContainersSaveRequest box in containerList)
                    {
                        if (box.TruckBookingID == Guid.Empty)
                        {
                            box.TruckBookingID = bookingID;
                        }
                        result.Add(box.RequestId,
                     new SaveResponse { RequestId = box.RequestId, ManyResult = this.SaveContainersList(box) });
                    }
                }

                scope.Complete();
                return result;

            }
        }
        #endregion

        #region 获得下载列表
        /// <summary>
        /// 获得下载列表
        /// </summary>
        /// <param name="truckType">类型</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="customerRefNo">客户参考单号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="state">状态</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="maxRecords">返回行数</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<DownLoadOceanBusinessList> GetOceandBusinessList(
                                 TruckBookingType? truckType,
                                 Guid companyID,
                                 string customerRefNo,
                                 string containerNo,
                                 string vesselName,
                                 string voyageNo,
                                 Int32 state,
                                 DateTime? beginDate,
                                 DateTime? endDate,
                                 int maxRecords,
                                 Guid UserID,
                                 bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTruckDownLoadOceanList");

                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, truckType);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, customerRefNo);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerNo);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@VoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, UserID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DownLoadOceanBusinessList> results = (from b in ds.Tables[0].AsEnumerable()
                                                           select new DownLoadOceanBusinessList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   //BookingID=b.Field<Guid>("BookingID"),
                                                   BusinessType = (TruckBookingType)b.Field<Byte>("OperationType"),
                                                   ContainerNo = b.Field<String>("ContainerNos"),
                                                   VesselVoyage = b.Field<String>("VesselVoyage"),
                                                   CustomerName = b.Field<String>("CustomerName"),
                                                   CustomerRefNo = b.Field<String>("CustomerRefNo"),
                                                   DownloadState = (DownLoadState)b.Field<Byte>("State"),
                                                   PortDate = b.Field<DateTime?>("PortDate"),
                                                   PortName = b.Field<String>("PorTNAME"),
                                                   RefNo = b.Field<String>("RefNo")
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

        #region 下载业务数据
        public List<TruckBookingsList> DownLoadTruckList(
                                TruckBookingType[] types,
                                Guid[] ids,
                                Guid companyID,
                                Guid saveByID,
                                bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspTruckDownLoadBusiness");

                db.AddInParameter(dbCommand, "@OperationTypes", DbType.String, types.Join());
                db.AddInParameter(dbCommand, "@OperationIDS", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 2)
                {
                    return null;
                }

                List<TruckBookingsList> list = (from d in ds.Tables[1].AsEnumerable()
                                                select new TruckBookingsList
                                                {
                                                    BookingID = d.Field<Guid>("ID"),
                                                    ContainerID = d.Field<Guid?>("ContainerID"),
                                                    State = (TruckBusinessState)d.Field<Byte>("State"),
                                                    No = d.Field<String>("No"),
                                                    Type = (TruckBookingType)d.Field<Byte>("Type"),
                                                    ContainerNo = d.Field<String>("ContainerNo"),
                                                    ContainerType = d.Field<String>("ContainerType"),
                                                    TrayNo = d.Field<String>("TrayNo"),
                                                    TruckDate = d.Field<DateTime?>("TruckDate"),
                                                    TruckPlace = d.Field<String>("TruckPlace"),
                                                    LastFreeDate = d.Field<DateTime?>("LastFreeDate"),
                                                    DriverName = d.Field<String>("DriverName"),
                                                    TruckNo = d.Field<String>("CarNo"),
                                                    CustomerName = d.Field<String>("CustomerName"),
                                                    CustomerRefNo = d.Field<String>("CustomerRefNo"),
                                                    CreateByName = d.Field<String>("CreateByName"),
                                                    CreateDate = d.Field<DateTime>("CreateDate"),
                                                    Remark = d.Field<String>("Remark"),
                                                    IsValid = d.Field<Boolean>("IsValid")
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

        #endregion

        #region 拖车资料

        #region 获得拖车列表
        /// <summary>
        /// 获得拖车资料列表
        /// </summary>
        /// <param name="no">车牌号</param>
        /// <param name="carTypeName">型号</param>
        /// <param name="dateSerachType">时间查询类型(1为创建时间;2为购买时间)</param>
        /// <param name="isvalid">有效性</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnglish">英文版本</param>
        /// <returns></returns>
        public List<TruckDataList> GetTruckDataList(
                            string no,
                            string carTypeName,
                            TruckDateSeachType dateSerachType,
                            bool? isvalid,
                            DateTime? beginTime,
                            DateTime? endTime,
                            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCarList");

                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@TypeName", DbType.String, carTypeName);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSerachType);
                db.AddInParameter(dbCommand, "@Isvalid", DbType.Boolean, isvalid);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<TruckDataList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new TruckDataList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   TruckNo = b.Field<String>("CarNo"),
                                                   TypeName = b.Field<String>("TypeName"),
                                                   BuyDate = b.Field<DateTime>("BuyDate"),
                                                   IsValid = b.Field<Boolean>("Isvalid"),
                                                   CreateByDate = b.Field<DateTime>("CreateDate"),
                                                   CreateByID = b.Field<Guid>("CreateBy"),
                                                   Remark = b.Field<String>("Remark"),
                                                   CreateByName = b.Field<String>("CreateByName"),
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

        #region 保存拖车资料
        /// <summary>
        /// 保存拖车资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">车牌号</param>
        /// <param name="typeName">型号</param>
        /// <param name="buyDate">购买日期</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新日期</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult SaveCarInfo(
                            Guid id,
                            string no,
                            string typeName,
                            DateTime buyDate,
                            string remark,
                            Guid saveByID,
                            DateTime? updateDate,
                            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCarInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@TypeName", DbType.String, typeName);
                db.AddInParameter(dbCommand, "@BuyDate", DbType.DateTime, buyDate);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
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

        #region 作废/激活拖车资料
        /// <summary>
        /// 作废/激活司机资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCancel">是否为作废，True为作废,False为激活</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult CancelTruck(
                     Guid id,
                     bool isCancel,
                     Guid changeByID,
                     DateTime? updateDate,
                     bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelCar");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);
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

        #endregion

        #region 司机资料

        #region 查询司机列表
        /// <summary>
        /// 获得拖车列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="moblie">手机</param>
        /// <param name="carID">身份ID</param>
        /// <param name="isvalid">有效性</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<DriversDataList> GeteDriverList(
                                string name,
                                string moblie,
                                string carID,
                                bool? isvalid,
                                DateTime? beginTime,
                                DateTime? endTime,
                                bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetDriverList");

                db.AddInParameter(dbCommand, "@Moblie", DbType.String, moblie);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@CardNO", DbType.String, carID);
                db.AddInParameter(dbCommand, "@Isvalid", DbType.Boolean, isvalid);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DriversDataList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new DriversDataList
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     Name = b.Field<String>("DriverName"),
                                                     Adress = b.Field<String>("Adress"),
                                                     Mobile = b.Field<String>("Mobile"),
                                                     CityID = b.Field<Guid?>("CityID"),
                                                     CityName = b.Field<String>("CityName"),
                                                     ProvinceID = b.Field<Guid?>("ProvinceID"),
                                                     ProvinceName = b.Field<String>("ProvinceName"),
                                                     CountryID = b.Field<Guid?>("CountryID"),
                                                     CountryName = b.Field<String>("CountryName"),
                                                     CardNo = b.Field<String>("CardNo"),
                                                     TruckID = b.Field<Guid?>("CarID"),
                                                     TruckNo = b.Field<String>("CarNo"),
                                                     Remark = b.Field<String>("Remark"),
                                                     CreateDate = b.Field<DateTime>("CreateDate"),
                                                     CreateBy = b.Field<Guid>("CreateBy"),
                                                     CreateByName = b.Field<String>("CreateByName"),
                                                     UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                     IsValid = b.Field<bool>("IsValid")
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

        #region 保存司机资料
        /// <summary>
        /// 保存司机资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">姓名</param>
        /// <param name="mobile">手机</param>
        /// <param name="address">地址</param>
        /// <param name="cardID">符合ID</param>
        /// <param name="provinceID">省份</param>
        /// <param name="cityID">城市</param>
        /// <param name="carID">默认拖车</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult SaveDriverInfo(
                            Guid id,
                            string name,
                            string mobile,
                            string address,
                            string cardID,
                            Guid? countryID,
                            Guid? provinceID,
                            Guid? cityID,
                            Guid? carID,
                            string remark,
                            Guid saveByID,
                            DateTime? updateDate,
                            bool isEnglish)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveDriverInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CName", DbType.String, name);
                db.AddInParameter(dbCommand, "@EName", DbType.String, name);
                db.AddInParameter(dbCommand, "@Adress", DbType.String, address);
                db.AddInParameter(dbCommand, "@Mobile", DbType.String, mobile);
                db.AddInParameter(dbCommand, "@CardNo", DbType.String, cardID);
                db.AddInParameter(dbCommand, "@CountryID", DbType.Guid, countryID);
                db.AddInParameter(dbCommand, "@ProvinceID", DbType.Guid, provinceID);
                db.AddInParameter(dbCommand, "@CityID", DbType.Guid, cityID);
                db.AddInParameter(dbCommand, "@CarID", DbType.Guid, carID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                SingleResult results = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
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

        #region 作废/激活司机资料
        /// <summary>
        /// 作废/激活司机资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCancel">是否为作废，True为作废,False为激活</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult CancelDriver(
                     Guid id,
                     bool isCancel,
                     Guid changeByID,
                     DateTime? updateDate,
                     bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelDriver");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                SingleResult results = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

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
    }
}

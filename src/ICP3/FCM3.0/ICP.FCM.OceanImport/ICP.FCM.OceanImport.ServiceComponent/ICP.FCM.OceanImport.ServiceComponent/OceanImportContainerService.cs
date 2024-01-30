using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.OceanImport.ServiceComponent
{
    partial class OceanImportService 
    {

        #region 根据业务ID获得集装箱列表
        /// <summary>
        /// 获得集装箱列表
        /// </summary>
        /// <param name="ioBookingID">业务单ID</param>
        /// <returns></returns>
        public List<OIBusinessContainerList> GetOIContainerList(Guid ioBookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIContainerList");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, ioBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OIBusinessContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new OIBusinessContainerList
                                                         {
                                                             IsRelation = b.Field<Guid?>("OIBookingID") == null ? false : true,
                                                             //IsRelation = b.Field<Guid?>("OIBookingID") == ioBookingID ? true : false,
                                                             ID = b.Field<Guid>("ID"),
                                                             No=b.Field<String>("No"),
                                                             ContainerTypeID = b.Field<Guid>("ContainerTypeID"),
                                                             ContainerTypeName = b.Field<String>("TypeName"),
                                                             SealNo = b.Field<String>("SealNo"),
                                                             GODate = b.Field<DateTime?>("GODate"),
                                                             BLNo = b.Field<String>("BLNO"),
                                                             Quantity = b.Field<Int32>("Quantity"),
                                                             LFDate = b.Field<DateTime?>("LastFreeDate"),
                                                             Remark = b.Field<String>("Remark"),
                                                             Location = b.Field<String>("Location"),
                                                             AvailableDate = b.Field<DateTime?>("AvailableDate"),
                                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                             IsPartOf = b.Field<Boolean>("IsPartOf"),
                                                             PickUpDate = b.Field<DateTime?>("PickUpDate"),
                                                             ReturnDate=b.Field<DateTime?>("ReturnDate"),
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

        #region 根据MBLID获得集装箱列表
        /// <summary>
        /// 获得集装箱列表
        /// </summary>
        /// <param name="mblID">MBLID</param>
        /// <returns></returns>
        public List<OIBusinessContainerList> GetOIContainerListByMBL(Guid mblID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIContainerListByMBL");

                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, mblID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OIBusinessContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new OIBusinessContainerList
                                                         {
                                                             ID = b.Field<Guid>("ID"),
                                                             No = b.Field<String>("No"),
                                                             ContainerTypeID = b.Field<Guid>("ContainerTypeID"),
                                                             ContainerTypeName = b.Field<String>("TypeName"),
                                                             SealNo = b.Field<String>("SealNo"),
                                                             GODate = b.Field<DateTime?>("GODate"),
                                                             BLNo = b.Field<String>("BLNO"),
                                                             Quantity = b.Field<Int32>("Quantity"),
                                                             LFDate = b.Field<DateTime?>("LastFreeDate"),
                                                             Remark = b.Field<String>("Remark"),
                                                             Location = b.Field<String>("Location"),
                                                             AvailableDate = b.Field<DateTime?>("AvailableDate"),
                                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                             IsPartOf = b.Field<Boolean>("IsPartOf"),
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

        #region 保存集装箱信息
        /// <summary>
        /// 保存集装箱信息
        /// </summary>
        /// <param name="IDs">ID</param>
        /// <param name="ContainerTypeIDs"> 箱型ID</param>
        /// <param name="SealNos">封条号</param>
        /// <param name="Quantitys">数量</param>
        /// <param name="BLNos">提单</param>
        /// <param name="GODates">G.O.Date</param>
        /// <param name="LFDates">L.F.Date</param>
        /// <param name="ValidDates">有效日期</param>
        /// <param name="TruckDates">运送日期</param>
        /// <param name="Addresss">地点</param>
        /// <param name="Remarks">备注</param>
        /// <param name="PickUpdates">提柜时间</param>
        /// <param name="ReturnDates">还空时间</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="containerUpdateDates">新时间</param>
        /// <returns></returns>
        public ManyResult SaveOIContainerInfo(ContainerSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIContainerInfo");

                string idList = saveRequest.IDs.Join();
                string noList = saveRequest.Nos.Join();
                string typeList = saveRequest.ContainerTypeIDs.Join();
                string sealNoList = saveRequest.SealNos.Join();
                string qtyList = saveRequest.Quantitys.Join();
                string locationsList = saveRequest.Locations.Join();
                string pickUpNoList = saveRequest.BLNos.Join();
                string godateList = saveRequest.GODates.Join();
                string alDateList = saveRequest.ValidDates.Join();
                string lfDateList = saveRequest.LFDates.Join();
                string remarkList = saveRequest.Remarks.Join();
                string updateDateList = saveRequest.UpdateDates.Join();
                string isPartOfList = saveRequest.IsPartOfs.Join();
                string pickUpDateList = saveRequest.PickUpdates.Join();
                string returnDateList = saveRequest.Returndates.Join();


                db.AddInParameter(dbCommand, "@OIMBLID", DbType.Guid, saveRequest.MBLID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String,idList);
                db.AddInParameter(dbCommand, "@Nos", DbType.String, noList);
                db.AddInParameter(dbCommand, "@TypeIDs", DbType.String, typeList);
                db.AddInParameter(dbCommand, "@SealNos", DbType.String, sealNoList);
                db.AddInParameter(dbCommand, "@Quantities", DbType.String, qtyList);
                db.AddInParameter(dbCommand, "@Locations", DbType.String, locationsList);
                db.AddInParameter(dbCommand, "@PickUpNos", DbType.String, pickUpNoList);
                db.AddInParameter(dbCommand, "@GODates", DbType.String, godateList);
                db.AddInParameter(dbCommand, "@AvailableDates", DbType.String, alDateList);
                db.AddInParameter(dbCommand, "@LastFreeDates", DbType.String, lfDateList);
                db.AddInParameter(dbCommand, "@IsPartOfs", DbType.String, isPartOfList);
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarkList);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDateList);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@PickUpDates", DbType.String, pickUpDateList);
                db.AddInParameter(dbCommand, "@ReturnDates", DbType.String, returnDateList);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #region 删除集装箱信息
        /// <summary>
        /// 删除集装箱信息
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        public void RemoveOIContainerInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOIContainerInfo");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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

        #region 根据业务ID集装箱ID列表
        /// <summary>
        /// 根据业务ID获得集装箱号列表(不需要些方法，可以从直接从列表中获得)
        /// </summary>
        /// <param name="bookingID">业务单ID</param>
        /// <returns></returns>
        public List<Guid> GetOIContainerIdsByBooking(Guid bookingID)
        {
            return new List<Guid>();
        }
        #endregion

        #region 根据派车ID获得集箱号列表
        /// <summary>
        /// 根据派车号获得集装箱号列表
        /// </summary>
        /// <param name="truckID">派车ID</param>
        /// <returns></returns>
        public List<Guid> GetOIContainerIdsByTruck(Guid truckID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIContainerIdsByTruck");

                db.AddInParameter(dbCommand, "@TruckID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> results = (from b in ds.Tables[0].AsEnumerable() select  b.Field<Guid>("OceanContainerID") ).ToList();

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

        #region 保存箱号与业务的关联
        /// <summary>
        /// 保存箱号与业务的关联
        /// </summary>
        /// <param name="bookingID">业务号</param>
        /// <param name="containerIDs">箱ID集合</param>
        /// <returns></returns>
        public ManyResult SaveOIContainerAndBusiness(
                        Guid bookingID,
                        Guid[] containerIDs,
                        Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIContainerAndBusiness");

                string containerIDList = containerIDs.Join();

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, bookingID);
                db.AddInParameter(dbCommand, "@ContainerIDs", DbType.String, containerIDList);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID"});


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

        #region 保存派车与业务的关联
        /// <summary>
        /// 保存派车与业务的关联
        /// </summary>
        /// <param name="truckID">派车号</param>
        /// <param name="containerIDs">箱ID集合</param>
        /// <returns></returns>
        public ManyResult SaveOIContainerAndTruck(
                        Guid truckID,
                        Guid[] containerIDs,
                        Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIContainerAndTruck");

                string containerIDList = containerIDs.Join();

                db.AddInParameter(dbCommand, "@TruckServiceID", DbType.Guid, truckID);
                db.AddInParameter(dbCommand, "@ContainerIDs", DbType.String, containerIDList);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "OIContainerID" });


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

        #region  获得海进HBL与箱信息关联列表
        /// <summary>
        /// 获得海进HBL与箱信息关联列表
        /// </summary>
        /// <param name="hblID"></param>
        /// <returns></returns>
        public List<OIBusinessContainerList> GetOIHBL2Container(Guid hblID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspGetOIHBLContainerList");

                db.AddInParameter(dbCommand, "@OIHBLID", DbType.Guid, hblID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OIBusinessContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new OIBusinessContainerList
                                                         {
                                                             IsSelected = b.Field<Boolean>("IsHBLContainer"),
                                                             ID = b.Field<Guid>("ID"),
                                                             No = b.Field<String>("No"),
                                                             ContainerTypeName = b.Field<String>("TypeName"),
                                                             SealNo = b.Field<String>("SealNo"),
                                                             GODate = b.Field<DateTime?>("GODate"),
                                                             BLNo = b.Field<String>("PickUpNo"),
                                                             Quantity = b.Field<Int32>("Quantity"),
                                                             LFDate = b.Field<DateTime?>("LastFreeDate"),
                                                             Remark = b.Field<String>("Remark"),
                                                             Location = b.Field<String>("Location"),
                                                             AvailableDate = b.Field<DateTime?>("AvailableDate"),
                                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                             IsPartOf = b.Field<Boolean>("IsPartOf"),
                                                             IsDirty=false,
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

        #region 保存海进HBL与箱信息关联列表
        /// <summary>
        /// 保存海进HBL与箱信息关联列表
        /// </summary>
        /// <param name="hblID"></param>
        /// <returns></returns>
        public ManyResult SaveOIHBL2Container(Guid hblID, Guid[] ctnIDs, Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIHBL2Container");

                string containerIDList = ctnIDs.Join();

                db.AddInParameter(dbCommand, "@OIHBLID", DbType.Guid, hblID);
                db.AddInParameter(dbCommand, "@CtnIDs", DbType.String, containerIDList);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID"});


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

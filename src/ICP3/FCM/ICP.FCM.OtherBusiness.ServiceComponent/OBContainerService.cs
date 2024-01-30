﻿using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ICP.FCM.OtherBusiness.ServiceComponent
{
    /// <summary>
    /// 箱信息管理
    /// </summary>
    partial class OtherBusinessService
    {
        #region 获取箱列表

        /// <summary>
        /// 获取箱列表
        /// </summary>
        /// <param name="BookingID">业务ID</param>
        /// <returns></returns>
        public List<OBContainerList> GetOtherContainerList(Guid BookingID)
        {
            ArgumentHelper.AssertGuidNotEmpty(BookingID, "BookingID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOtherContainerList");
                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, BookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<OBContainerList> results = BulidContainerListByDataSet(ds);

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
        /// 获取箱列表
        /// </summary>
        /// <param name="BookingID">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns></returns>
        public List<OBContainerList> GetOtherContainerList(Guid BookingID, Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(BookingID, "BookingID");

            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOtherContainerList");
                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, BookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<OBContainerList> results = BulidContainerListByDataSet(ds);

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

        private List<OBContainerList> BulidContainerListByDataSet(DataSet ds)
        {
            List<OBContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new OBContainerList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   No = b.Field<string>("No"),
                                                   SealNo = b.Field<string>("SealNo"),
                                                   SoNo = b.Field<string>("SoNo"),
                                                   TypeID = b.Field<Guid?>("TypeID"),
                                                   TypeName = b.Field<string>("TypeName"),
                                                   Quantity = b.Field<int>("Quantity"),
                                                   QuantityName = b.Field<string>("QuantityName"),
                                                   Weight = b.Field<decimal>("Weight"),
                                                   Measurement = b.Field<decimal>("Measurement"),
                                                   MeasurementUnitID = b.Field<Guid?>("MeasurementUnitID"),
                                                   QuantityUnitID = b.Field<Guid?>("QuantityUnitID"),
                                                   WeightUnitID = b.Field<Guid?>("WeightUnitID"),
                                                   Commodity = b.Field<string>("Commodity"),

                                                   //CreateByID = b.Field<Guid?>("CreateByID"),
                                                   //CreateByName = b.Field<string>("CreateByName"),
                                                   //UpdateByName = b.Field<string>("UpdateByName"),
                                                   //UpdateByID = b.Field<Guid?>("UpdateByID"),
                                                   //CreateDate = b.Field<DateTime?>("CreateDate"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                   IsDirty = false
                                               }).ToList();
            return results;

        }
        #endregion

        #region 保存箱信息
        /// <summary>
        /// 保存箱信息
        /// </summary>
        /// <param name="ContainerInfo">箱信息</param>
        /// <returns></returns>
        public ManyResult SaveOtherContanerList(ContainerSaveRequest ContainerInfo)
        {

            try
            {
                Database db = null;
                if (ContainerInfo.companyID == Guid.Empty)
                {
                    db = DatabaseFactory.CreateDatabase();
                }
                else
                {
                    bool isDefaultDB = CompanyHelper.IsDefaultServer(ContainerInfo.companyID, FrameworkInitializeService);
                    db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                }
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOtherContainerInfo");

                string IDs = ContainerInfo.IDs.Join();
                string Commoditys = ContainerInfo.Commoditys.Join();
                string Measurements = ContainerInfo.Measurements.Join();
                string MeasurementUnitIDs = ContainerInfo.MeasurementUnitIDs.Join();
                string Nos = ContainerInfo.Nos.Join();
                string Quantitys = ContainerInfo.Quantitys.Join();
                string QuantityUnitIDs = ContainerInfo.QuantityUnitIDs.Join();
                string SealNos = ContainerInfo.SealNos.Join();
                string SONOS = ContainerInfo.SONOS.Join();
                string TypeIDs = ContainerInfo.TypeIDs.Join();
                string UpdateDates = ContainerInfo.UpdateDates.Join();
                string Weights = ContainerInfo.Weights.Join();
                string WeightUnitIDs = ContainerInfo.WeightUnitIDs.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, IDs);
                db.AddInParameter(dbCommand, "@OtherBookingID", DbType.Guid, ContainerInfo.OtherBookingID);
                db.AddInParameter(dbCommand, "@Commoditys", DbType.String, Commoditys);
                db.AddInParameter(dbCommand, "@Measurements", DbType.String, Measurements);
                db.AddInParameter(dbCommand, "@MeasurementUnitIDs", DbType.String, MeasurementUnitIDs);
                db.AddInParameter(dbCommand, "@Nos", DbType.String, Nos);
                db.AddInParameter(dbCommand, "@Quantitys", DbType.String, Quantitys);
                db.AddInParameter(dbCommand, "@QuantityUnitIDs", DbType.String, QuantityUnitIDs);
                db.AddInParameter(dbCommand, "@SealNos", DbType.String, SealNos);
                db.AddInParameter(dbCommand, "@SONOS", DbType.String, SONOS);
                db.AddInParameter(dbCommand, "@TypeIDs", DbType.String, TypeIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, UpdateDates);
                db.AddInParameter(dbCommand, "@Weights", DbType.String, Weights);
                db.AddInParameter(dbCommand, "@WeightUnitIDs", DbType.String, WeightUnitIDs);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, ContainerInfo.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "NO", "UPDATEDATE" });
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

        #region 删除箱信息
        /// <summary>
        /// 删除箱信息
        /// </summary>
        /// <param name="ids">箱ID</param>
        /// <param name="removeByID">操作人</param>
        /// <param name="updateDates">更新时间</param>
        public void RemoveOtherContainerList(Guid?[] ids, Guid removeByID, DateTime?[] updateDates)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOTContainerInfo");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());

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
        /// 删除箱信息
        /// </summary>
        /// <param name="ids">箱ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="removeByID">操作人</param>
        /// <param name="updateDates">更新时间</param>
        public void RemoveOtherContainerList(Guid?[] ids, Guid companyID, Guid removeByID, DateTime?[] updateDates)
        {
            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOTContainerInfo");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());

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

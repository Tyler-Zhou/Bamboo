using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.OceanExport.ServiceInterface.Comm;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    /// <summary>
    /// 海运出口报关委托服务
    /// </summary>
    partial class OceanExportService
    {
        /// <summary>
        /// 获取报关信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回报关信息</returns>
        public OceanCustoms GetOceanCustomsInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanCustomsInfo");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return IListDataSet.DataSetToIList<OceanCustoms>(ds, 0).SingleOrDefault();
               
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
        /// 获取报关信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回报关信息</returns>
        //public OceanCustoms GetOceanCustomsInfo(Guid id)
        //{
        //    try
        //    {
        //        using (DBContext dbc = DBMLFty.dbc)
        //        {
        //            return dbc.OceanCustoms.SingleOrDefault(o => o.ID == id);
        //        }
        //    }
        //    catch (SqlException sqlException)
        //    {
        //        throw new ApplicationException(sqlException.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public bool IsExisted(Guid oceanBookingID, Guid oceanContainerID, String title)
        {
            try
            {
                //using (DBContext dbc = DBMLFty.dbc)
                //{
                //    return dbc.V_OceanCustoms.Count
                //        (o => o.OceanBookingID == oceanBookingID && o.OceanContainerID == oceanContainerID && o.Title == title) > 0 ? true : false;
                //}
                return false;
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
        /// 获取报关信息
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <returns>返回报关信息</returns>
        public List<OceanCustoms> GetOceanCustomsServiceList(Guid oceanBookingID, Guid oceanContainerID)
        {
            try
            {
                ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanCustomsList");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@OceanContainerID", DbType.Guid, oceanContainerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return IListDataSet.DataSetToIList<OceanCustoms>(ds, 0).ToList();
                
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

        public Boolean SaveOceanCustomsInfo(OceanCustoms obj)
        {
            try
            {
                ArgumentHelper.AssertGuidNotEmpty(obj.ID, "ID");
                ArgumentHelper.AssertGuidNotEmpty(obj.OceanBookingID, "OceanBookingID");
                ArgumentHelper.AssertGuidNotEmpty(obj.OceanContainerID, "OceanContainerID");
                ArgumentHelper.AssertGuidNotEmpty(obj.CustomsID, "CustomsID");
                ArgumentHelper.AssertGuidNotEmpty(obj.CreateBy, "CreateBy");
                ArgumentHelper.AssertGuidNotEmpty(obj.UpdateBy, "UpdateBy");

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanCustomsInfo");

                //string temptruckerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.truckerDescription, true, false);
                ////string temppickUpAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.pickUpAtDescription, true, false);
                //string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.shipperDescription, true, false);
                //string tempdeliveryAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.deliveryAtDescription, true, false);
                //string tempcustomsBrokerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.customsBrokerDescription, true, false);
                //string tempcontainerDescription = SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.containerDescription, true, false);
                //string tempBillToDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.BillToDescription, true, false);
                //string tempPUEmptyCNTRDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.PUEmptyCNTRDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, obj.ID);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, obj.OceanBookingID);
                db.AddInParameter(dbCommand, "@OceanContainerID", DbType.Guid, obj.OceanContainerID);
                db.AddInParameter(dbCommand, "@CustomsID", DbType.Guid, obj.CustomsID);
                db.AddInParameter(dbCommand, "@NO", DbType.String, obj.NO);
                db.AddInParameter(dbCommand, "@Title", DbType.String, obj.Title);
                db.AddInParameter(dbCommand, "@PortToCustoms", DbType.String, obj.PortToCustoms);
                db.AddInParameter(dbCommand, "@Way", DbType.String, obj.Way);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, obj.Remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, obj.UpdateBy);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, obj.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                
                //return db.ExecuteNonQuery(dbCommand) > 0 ? true : false;
                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID",  "UpdateDate","NO","CreateDate" }, new string[] { "ID", "OceanContainerID", "OceanBookingID", "CustomsID", "UpdateDate" } });
                if (results == null
                    || results.Length < 2
                    || results[0].Items.Count == 0)
                {
                    return false;
                }

                HierarchyManyResult result = new HierarchyManyResult(results[0].Items[0]);
                if (results[1] != null)
                {
                    foreach (SingleResult s in results[1].Items)
                    {
                        result.Childs.Add(new HierarchyManyResult(s));
                    }
                }
                return result == null ? false : true;
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
        /// 删除报关信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public Boolean RemoveOceanCustomsInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanCustomsInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                return db.ExecuteNonQuery(dbCommand) == -1 ? false : true;
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
    }
}

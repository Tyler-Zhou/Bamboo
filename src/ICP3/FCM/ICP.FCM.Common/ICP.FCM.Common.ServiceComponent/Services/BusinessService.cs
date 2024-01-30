using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
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

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 业务服务接口
    /// </summary>
    partial class FCMCommonService
    {
        /// <summary>
        /// 当前登陆用户ID
        /// </summary>
        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="voyagelIds"></param>
        /// <returns></returns>
        public List<VoyageDateInfo> GetVoyageDateInfo(List<Guid?> voyagelIds)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[pub].[uspGetVoyageDateInfo]");
                db.AddInParameter(dbCommand, "@VoyageIDs", DbType.String, voyagelIds.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                List<VoyageDateInfo> result = new List<VoyageDateInfo>();
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return result;
                }
                result = (from item in ds.Tables[0].AsEnumerable()
                          select new VoyageDateInfo
                          {
                              ETD = item.Field<DateTime?>("ETD"),
                              ETA = item.Field<DateTime?>("ETA"),
                              ClosingDate = item.Field<DateTime?>("ClosingDate"),
                              CYClosingDate = item.Field<DateTime?>("CYClosingDate"),
                              DOCClosingDate = item.Field<DateTime?>("DOCClosingDate"),
                              PortId = item.Field<Guid?>("PortId"),
                              VoyageId = item.Field<Guid?>("VoyageId")
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
        /// 获取订舱列表
        /// </summary>
        /// <param name="operationTypes"></param>
        /// <param name="companyIDs">业务所在口岸公司(操作的公司)</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="salesID">业务员</param>
        /// <param name="isValid"></param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        public List<BusinessData> GetBusinessListForDataFinder(
            OperationType[] operationTypes,
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string customerName,
            string polName,
            string podName,
            Guid? salesID,
            bool? isValid,
            OrderState? state,
            DateSearchTypeForDataFinder dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetBusinessListForWriteOff");

                string tempCompanyIDs = companyIDs.Join();
                string operationTypesString = string.Empty;
                int? intState = (int?)state;
                if (operationTypes != null)
                {
                    operationTypesString = operationTypes.Join();
                    if (state != null)
                    {
                        intState = null;
                    }
                    if (operationTypes.Length > 1 && state != null)
                    {
                        intState = ConvertToIntEmpty(OperationType.OceanExport, state);
                    }
                    else
                    {
                        intState = ConvertToIntEmpty(operationTypes[0], state);
                    }
                }
                else
                {
                    intState = ConvertToIntEmpty(OperationType.OceanExport, state);
                }

                db.AddInParameter(dbCommand, "@operationTypes", DbType.String, operationTypesString);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@FromName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@ToName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BusinessData> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new BusinessData
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      OperationType = (OperationType)b.Field<byte>("OperationType"),
                                                      CompanyID = b.Field<Guid>("CompanyID"),
                                                      State = ConvertToOrderState(((OperationType)b.Field<byte>("OperationType")), b.Field<byte>("State")),
                                                      No = b.Field<string>("No"),
                                                      MBLNo = b.Field<string>("MBLNo"),
                                                      HBLNo = b.Field<string>("HBLNo"),
                                                      CustomerName = b.Field<string>("CustomerName"),
                                                      POLName = b.Field<string>("Fromlocation"),
                                                      PODName = b.Field<string>("Tolocation"),
                                                      ETD = b.Field<DateTime?>("ETD"),
                                                      SalesName = b.Field<string>("SalesName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      ETA = b.Field<DateTime?>("ETA"),
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        OrderState ConvertToOrderState(OperationType operationType, byte stateValue)
        {
            OrderState orderState = OrderState.Unknown;
            switch (operationType)
            {
                case OperationType.OceanExport:
                    orderState = ((OEOrderState)stateValue).ToOState();
                    break;
                case OperationType.OceanImport:
                    orderState = ((OIOrderState)stateValue).ToOState();
                    break;
                default:
                    orderState = ((OEOrderState)stateValue).ToOState();
                    break;
            }
            return orderState;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="orderState"></param>
        /// <returns></returns>
        int? ConvertToIntEmpty(OperationType operationType, OrderState? orderState)
        {
            int? intState = null;
            switch (operationType)
            {
                case OperationType.OceanExport:
                    intState = orderState == null ? null : ((int?)orderState.Value.ToOEState());
                    break;
                case OperationType.OceanImport:
                    intState = orderState == null ? null : ((int?)orderState.Value.ToOIState());
                    break;
                default:
                    intState = orderState == null ? null : ((int?)orderState.Value.ToOEState());
                    break;
            }
            return intState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public SingleResult UpdateSoForOperation(FileCopyParameters parameter)
        {
            SingleResult result = null;

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspUpdateSoForOperation");
            db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, parameter.OperationIDs.Distinct().ToArray().Join());
            db.AddInParameter(dbCommand, "@SoNos", DbType.String, parameter.SONOs.Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, parameter.UpdateDates.Join());
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            try
            {
                result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        ///批量更新业务的ETA,提货地
        /// </summary>
        /// <returns></returns>
        public SingleResult UpdateOceanEDAandWareHouse(UpdateETAInfo updateInfo)
        {
            SingleResult result = null;

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspUpdateOceanEDAandWareHouse");
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid,updateInfo.CompanyID);
            db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, updateInfo.VoyageID);
            db.AddInParameter(dbCommand, "@IsETA", DbType.Boolean, updateInfo.IsETA);
            db.AddInParameter(dbCommand, "@IsWareHouse", DbType.Boolean, updateInfo.IsWareHouse);
            db.AddInParameter(dbCommand, "@ETA", DbType.String, updateInfo.ETA);
            db.AddInParameter(dbCommand, "@WareHouseID", DbType.Guid, updateInfo.WareHouseID);           
            try
            {
                result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取批量更新的业务号
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        public List<Guid> GetOperationIdsForUpdate(UpdateETAInfo updateInfo) 
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationIdsForUpdateOceanEDAandWareHouse");

            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, updateInfo.CompanyID);
            db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, updateInfo.VoyageID);
            db.AddInParameter(dbCommand, "@IsETA", DbType.Boolean, updateInfo.IsETA);
            db.AddInParameter(dbCommand, "@IsWareHouse", DbType.Boolean, updateInfo.IsWareHouse);
            db.AddInParameter(dbCommand, "@ETA", DbType.String, updateInfo.ETA);
            db.AddInParameter(dbCommand, "@WareHouseID", DbType.Guid, updateInfo.WareHouseID);      

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }

            List<Guid> result = new List<Guid>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                result.Add(new Guid(dr[0].ToString()));                     
            }

            return result;
        }

        /// <summary>
        ///  通过海出订单ID得到所有相关联的海进业务ID  
        ///  2013-07-22 joe 
        /// </summary>
        /// <param name="OIBookingID"></param>
        /// <returns></returns>
        public List<SimpleBusinnessInfo> GetSimpleBusinessByBookingID(Guid OIBookingID)
        {
            try
            {
                List<SimpleBusinnessInfo> result = new List<SimpleBusinnessInfo>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSimpleBusinessByBookingID]");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, OIBookingID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new SimpleBusinnessInfo()
                    {
                        OIBusinessID = dr.Field<Guid>("OIBookingID"),
                        OIBusinessNO = dr.Field<String>("OINo"),
                        OEBusinessID = dr.Field<Guid>("OEBookingID"),
                        OEBusinessNO = dr.Field<String>("OENo"),
                        DispatchUserName = dr.Field<String>("DispatchName"),
                        DispatchDate = dr.Field<DateTime>("DispatchOn"),
                        Remark = dr.Field<String>("Remark"),
                        AcceptUserName = dr.Field<String>("AcceptName"),
                        //  AcceptDate = dr.Field<DateTime?>("AcceptOn")
                    });
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
        ///  获得上次分发日志的ID  
        /// </summary>
        /// <returns></returns>
        public string GetDispatchLastLogID(Guid OIBookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchLastLogID]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OIBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return string.Empty;
                }

                if (ds.Tables[0].Rows[0][0] == DBNull.Value)
                {
                    return string.Empty;
                }
                string reslut = JSONSerializerHelper.SerializeToJson(ds.Tables[0].Rows[0][0]);
                return reslut;
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
        ///  获得最新分发日志的ID  
        /// </summary>
        /// <returns></returns>
        public string GetDispatchNewLogID(Guid OIBookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchNewLogID]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OIBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return string.Empty;
                }

                if (ds.Tables[0].Rows[0][0] == DBNull.Value)
                {
                    return string.Empty;
                }
                string reslut = JSONSerializerHelper.SerializeToJson(ds.Tables[0].Rows[0][0]);
                return reslut;
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
        ///  通过海出订单ID得到所有相关联的海进业务ID  
        ///  2013-08-02 joe 
        /// </summary>
        /// <param name="OIBookingID">海进业务ID</param>
        /// <param name="userID">当前用户ID</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public int ApplyRevise(Guid OIBookingID, Guid userID, string remark)
        {
            try
            {
                List<SimpleBusinnessInfo> result = new List<SimpleBusinnessInfo>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspApplyResive]");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookingID);
                db.AddInParameter(dbCommand, "@userID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddOutParameter(dbCommand, "@Result", DbType.Int16, -1);




                db.ExecuteNonQuery(dbCommand);
                object ss = db.GetParameterValue(dbCommand, "@Result");

                return int.Parse(ss.ToString());
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
        ///  通过海进业务ID得到所有相关联的海出业务ID  
        ///  2013-08-12 joe 
        /// </summary>
        /// <returns></returns>
        public List<SimpleBusinnessInfo> GetOEIDByOIID(Guid OIBookingID)
        {
            try
            {
                List<SimpleBusinnessInfo> result = new List<SimpleBusinnessInfo>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSimpleBusinessByOperationID]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OIBookingID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new SimpleBusinnessInfo()
                    {
                        OIBusinessID = dr.Field<Guid>("OIBookingID"),
                        OIBusinessNO = dr.Field<String>("OINo"),
                        OEBusinessID = dr.Field<Guid>("OEBookingID"),
                        OEBusinessNO = dr.Field<String>("OENo"),
                        DispatchUserName = dr.Field<String>("DispatchName"),
                        DispatchDate = dr.Field<DateTime>("CreateDate"),
                        Remark = dr.Field<String>("Remark"),
                        AcceptUserName = dr.Field<String>("AcceptName"),
                        Type = (OperationType)dr.Field<byte>("Type")
                    });
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

        
    }
}
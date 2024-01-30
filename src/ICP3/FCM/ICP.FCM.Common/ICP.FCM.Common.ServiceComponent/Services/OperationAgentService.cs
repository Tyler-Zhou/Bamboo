using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceInterface;
    using ServiceInterface.DataObjects;
    using Framework.CommonLibrary.Helper;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data;
    using System.Data.SqlClient;
    using DataCache.ServiceInterface;
    using Framework.CommonLibrary.Common;
    using ICP.FileSystem.ServiceInterface;

    /// <summary>
    /// 业务代理服务实现
    /// </summary>
    public partial class OperationAgentService : IOperationAgentService
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsDispatch = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public Guid GetOceanimportBusinessID(Guid operationId)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanimportBusinessID");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return Guid.Empty;
                }
                Guid result = new Guid(ds.Tables[0].Rows[0]["OceanBookingID"].ToString());
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
        /// 得到业务代理信息
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <returns>业务代理信息对象</returns>
        public OperationAgentInfo GetOperationAgentInfo(Guid operationId)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanAgentInfo");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OperationAgentInfo result = (from b in ds.Tables[0].AsEnumerable()
                    select new OperationAgentInfo
                    {
                        Name = b.Field<string>("Name"),
                        AgentID = b.Field<Guid>("AgentID"),
                        IsBranch = b.Field<bool>("IsBranch"),
                        OperationID = b.Field<Guid>("OperationID"),
                        SONO = b.Field<string>("SONO"),
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
        /// 根据业务ID得到业务分文档数据
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <returns></returns>
        public DocumentDispatchContainerObjects GetOperationDocumentDispatchData(Guid operationId)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanAgentDispatchInfo");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DocumentDispatchContainerObjects DispatchData = new DocumentDispatchContainerObjects();
                if (ds.Tables.Count == 4)
                {
                    DispatchData.AgentInfo = (from b in ds.Tables[0].AsEnumerable()
                                              select new OperationAgentInfo
                                              {
                                                  Name = b.Field<string>("Name"),
                                                  AgentID = b.Field<Guid>("AgentID"),
                                                  IsBranch = Convert.ToBoolean((b.Field<int>("IsBranch"))),
                                                  OperationID = b.Field<Guid>("OceanBookingID"),
                                                  SONO = b.Field<string>("SONO"),
                                              }).SingleOrDefault();


                    DispatchData.DispatchInfo = (from b in ds.Tables[1].AsEnumerable()
                                                 select new DocumentDispatchInfo
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     AgentMail = b.Field<string>("AgentMail"),
                                                     OverseasCSID = b.Field<Guid?>("OverseasCSID"),
                                                     OverseasCSName = b.Field<string>("OverseasCSName"),
                                                     RecipientType = (RecipientType)b.Field<Byte>("RecipientType"),
                                                     UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                 }).SingleOrDefault();

                    DispatchData.DocumentListInfo = (from b in ds.Tables[2].AsEnumerable()
                                                     select new DocumentDispatchSelectListInfo
                                                     {
                                                         ID = b.Field<Guid?>("ID"),
                                                         OperationFileID = b.Field<Guid?>("OperationFileID"),
                                                         OceanAgentDispatchID = b.Field<Guid?>("OceanAgentDispatchID"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                     }).ToList();

                    DispatchData.Description = (from b in ds.Tables[3].AsEnumerable()
                                                select new DescriptionInfo
                                                {
                                                    Description = b.Field<String>("LogDesc"),
                                                }).ToList();
                }
               
                return DispatchData;
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
        /// 获取业务分发信息
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public DocumentDispatchInfo GetDocumentDispatchInfo(Guid operationId)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDocumentDispatchInfo");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DocumentDispatchInfo dispatchInfo = new DocumentDispatchInfo();

                dispatchInfo = (from b in ds.Tables[0].AsEnumerable()
                    select new DocumentDispatchInfo
                    {
                        ID = b.Field<Guid>("ID"),
                        AgentMail = b.Field<string>("AgentMail"),
                        OverseasCSID = b.Field<Guid?>("OverseasCSID"),
                        OverseasCSName = b.Field<string>("OverseasCSName"),
                        RecipientType = (RecipientType) b.Field<Byte>("RecipientType"),
                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
                        Remark = b.Field<string>("Remark"),
                    }).SingleOrDefault();
                return dispatchInfo;
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
        /// 保存分文档信息
        /// </summary>
        /// <param name="ContainerObjects"></param>
        /// <param name="DispatchFileIDs"></param>
        /// <param name="FileIDs"></param>
        /// <param name="FileUpdateDates"></param>
        public bool SaveOperationDocumentDispatchData(DocumentDispatchContainerObjects ContainerObjects,
            Guid?[] DispatchFileIDs, Guid[] FileIDs, DateTime?[] FileUpdateDates)
        {
            bool isSuccess = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanAgentDispatchInfo4Submit");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, ContainerObjects.DispatchInfo.ID);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid,
                    ContainerObjects.DispatchInfo.OceanBookingID);
                db.AddInParameter(dbCommand, "@IsAgainDispatch", DbType.Boolean,
                    ContainerObjects.DispatchInfo.IsAgainDispatch);
                db.AddInParameter(dbCommand, "@AgentMail", DbType.String, ContainerObjects.DispatchInfo.AgentMail);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, ContainerObjects.DispatchInfo.Remark);
                db.AddInParameter(dbCommand, "@OverseasCSID", DbType.Guid, ContainerObjects.DispatchInfo.OverseasCSID);
                db.AddInParameter(dbCommand, "@RecipientType", DbType.Byte,
                    ContainerObjects.DispatchInfo.RecipientType.GetHashCode());
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, ContainerObjects.DispatchInfo.UpdateDate);

                string tmp = ContainerObjects.DispatchInfo.UpdateDate.ToString();

                //List<Guid> tempID = new List<Guid>();
                //foreach (var item in ContainerObjects.DocumentListInfo)
                //{
                //    tempID.Add(item.ID);
                //}

                db.AddInParameter(dbCommand, "@OceanAgentDispatchFileIDs", DbType.String, DispatchFileIDs.Join());
                db.AddInParameter(dbCommand, "@OperationFileIDs", DbType.String, FileIDs.Join());
                db.AddInParameter(dbCommand, "@OperationFileUpdateDates", DbType.String, FileUpdateDates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, ContainerObjects.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish ", DbType.Boolean, IsEnglish);

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

            return isSuccess;
        }

        /// <summary>
        /// 港前分发文件
        /// </summary>
        /// <param name="OceanBookingID"></param>
        /// <param name="DispatchFlieType"></param>
        /// <param name="FileIDs"></param>
        /// <param name="SaveByID"></param>
        public bool DicpatchFilesForOE(Guid OceanBookingID, byte DispatchFlieType, Guid[] FileIDs, Guid SaveByID)
        {
            bool isSuccess = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspDicpatchFilesForOE");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, OceanBookingID);
                db.AddInParameter(dbCommand, "@DispatchFlieType", DbType.Byte, DispatchFlieType);
                db.AddInParameter(dbCommand, "@OperationFileIDs", DbType.String, FileIDs.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, SaveByID);
                db.AddInParameter(dbCommand, "@IsPrepareing", DbType.Boolean, false);
                db.AddInParameter(dbCommand, "@IsEnglish ", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                return isSuccess;
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
        /// 空运港前分发文件
        /// </summary>
        /// <param name="AirBookingID"></param>
        /// <param name="DispatchFlieType"></param>
        /// <param name="FileIDs"></param>
        /// <param name="SaveByID"></param>
        public bool DicpatchFilesForAir(Guid AirBookingID, byte DispatchFlieType, Guid[] FileIDs, Guid SaveByID)
        {
            bool isSuccess = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspDicpatchFilesForAE");

                db.AddInParameter(dbCommand, "@AirBookingID", DbType.Guid, AirBookingID);
                db.AddInParameter(dbCommand, "@DispatchFlieType", DbType.Byte, DispatchFlieType);
                db.AddInParameter(dbCommand, "@OperationFileIDs", DbType.String, FileIDs.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, SaveByID);
                db.AddInParameter(dbCommand, "@IsPrepareing", DbType.Boolean, false);
                db.AddInParameter(dbCommand, "@IsEnglish ", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                return isSuccess;
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
        /// 港后分发文件
        /// </summary>
        /// <param name="OceanBookingID"></param>
        /// <param name="SaveByID"></param>
        public bool DicpatchFilesForOI(Guid OceanBookingID, Guid SaveByID)
        {
            bool isSuccess = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspDicpatchFilesForOI");

                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OceanBookingID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish ", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                return isSuccess;
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
        /// 空运港后分发文件
        /// </summary>
        /// <param name="AIBookingID"></param>
        /// <param name="SaveByID"></param>
        public bool DicpatchFilesForAI(Guid AIBookingID, Guid SaveByID)
        {
            bool isSuccess = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspDicpatchFilesForAI");

                db.AddInParameter(dbCommand, "@AIBookingID", DbType.Guid, AIBookingID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish ", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                return isSuccess;
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
        /// 重新分发文件
        /// </summary>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        public bool CopyDispatchFileLogInfo(Guid DispatchFileLogID)
        {
            bool isSuccess = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.[uspGetCopyDispatchFileLogInfo]");

                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);

                db.ExecuteNonQuery(dbCommand);

                return isSuccess;
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
        /// 分文件签收记录列表
        /// 2013-09-03 joe
        /// </summary>
        /// <param name="BookingID">业务ID</param>
        /// <param name="Type">操作类型（1为海出2为海进）</param>
        /// <returns></returns>
        public List<HistoryOceanRecord> GetHistoryOceanRecord(Guid BookingID, int Type)
        {
            try
            {
                List<HistoryOceanRecord> result = new List<HistoryOceanRecord>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetApplyHistoryRecord]");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, BookingID);
                db.AddInParameter(dbCommand, "@Type", DbType.Int16, Type);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }

                result = (from b in ds.Tables[0].AsEnumerable()
                    select new HistoryOceanRecord
                    {
                        ID = b.Field<Guid>("ID"),
                        OEBookingID = b.Field<Guid>("OEBookingID"),
                        Type = b.Field<int>("Type"),
                        OperationType = (int) b.Field<Byte>("OperationType"),
                        DispatchBy = b.Field<string>("DispatchBy"),
                        DispatchOn = b.Field<DateTime?>("DispatchOn"),
                        AcceptBy = b.Field<String>("AcceptBy"),
                        AcceptOn = b.Field<DateTime?>("AcceptOn"),
                        Remark = b.Field<String>("Remark"),
                        RefID = b.Field<Guid?>("RefID"),
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
        /// 分文件和修订状态
        /// 2013-09-13 joe
        /// </summary>
        /// <returns></returns>

        public DocumentState GetDispatchAndReviseState(Guid operationID,
            OperationType operationtype)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchReviseState]");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationtype);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return DocumentState.Pending;
                }
                object res = ds.Tables[0].Rows[0]["State"];
                return res == null ? DocumentState.Pending : (DocumentState) (byte) res;


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
        /// <param name="operationID"></param>
        /// <returns></returns>
        public DocumentState GetDispatchAndReviseState(Guid operationID)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchReviseState]");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return DocumentState.Pending;
                }
                object res = ds.Tables[0].Rows[0]["State"];
                return res == null ? DocumentState.Pending : (DocumentState) (byte) res;


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
        /// 分文件和修订状态账单正式使用时间
        /// 2013-09-17 joe
        /// </summary>
        /// <returns></returns>

        public DateTime GetStartDateForReviseAgentBill()
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.ufnGetStartDateForReviseAgentBill");
                // db.AddParameter(dbCommand, "@Return", DbType.DateTime, ParameterDirection.ReturnValue, "",DataRowVersion.Default, null);

                db.AddParameter(dbCommand, "@Return", DbType.DateTime, ParameterDirection.ReturnValue, "Return",
                    DataRowVersion.Default, null);



                db.ExecuteNonQuery(dbCommand);
                object result = db.GetParameterValue(dbCommand, "@Return");

                return (DateTime) result;


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
        /// 判定业务是否在已下载但还没有分发文件的状态，在这个状态返回1，否则返回0
        /// 2013-10-08 joe
        /// </summary>
        /// <returns></returns>

        public bool GetIsAcceptDispatch(Guid operationID)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspIsAcceptDispatch");
                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, operationID);

                db.AddParameter(dbCommand, "@Return", DbType.Boolean, ParameterDirection.ReturnValue, "Return",
                    DataRowVersion.Default, null);
                db.ExecuteNonQuery(dbCommand);
                object result = db.GetParameterValue(dbCommand, "@Return");

                return ((int) result) > 0 ? true : false;


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
        /// 得到海进业务的创建时间
        /// 2013-10-10 joe
        /// </summary>
        /// <returns></returns>

        public DateTime GetCreateDateOIBusiness(Guid BookingID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[ufnGetCreateDateOIBusiness]");
                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, BookingID);
                db.AddParameter(dbCommand, "@Return", DbType.DateTime, ParameterDirection.ReturnValue, "Return",
                    DataRowVersion.Default, null);
                db.ExecuteNonQuery(dbCommand);
                object result = db.GetParameterValue(dbCommand, "@Return");

                DateTime re = new DateTime(9999, 1, 1);
                if (!DateTime.TryParse(result.ToString(), out re))
                    return new DateTime(9999, 1, 1);

                return re;
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
        /// 业务是否下载过
        /// </summary>
        /// <returns></returns>
        public bool BusinessIsDownLoad(Guid operationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand =
                    db.GetSqlStringCommand(
                        "SELECT 1 FROM fcm.V_OEJobID2OIJobID  WHERE OceanBookingID=@OperationID OR OIBookingID=@OperationID ");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                object results = db.ExecuteScalar(dbCommand);

                if (results == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 拒签分发文件或账单修订
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <param name="strReason">拒签原因</param>
        /// <returns></returns>

        public bool RejectDispatchOrRevise(Guid OperationID, OperationType OperationType, string strReason)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspRejectDispatchOrRevise]");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, OperationType);
                db.AddInParameter(dbCommand, "@Reason", DbType.String, strReason);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        /// <summary>
        /// 业务的代理，文件员，客服信息  joe
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <returns></returns>
        public BusinessAgentAndCustomInfoObject GetOperationAgentNameAndCustomer(Guid OperationID,
            OperationType OperationType)
        {
            try
            {
                List<BusinessAgentAndCustomInfoObject> result = new List<BusinessAgentAndCustomInfoObject>();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationAgentNameAndCustomer]");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, (int) OperationType);
                db.AddInParameter(dbCommand, "@IsEnglish ", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }

                result = (from b in ds.Tables[0].AsEnumerable()
                    select new BusinessAgentAndCustomInfoObject
                    {
                        AgentName = b.Field<string>("AgentName"),
                        FilerName = b.Field<string>("FilerName"),
                        CustomerName = b.Field<string>("CustomerName"),

                    }).ToList();

                return result[0];
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        /// <summary>
        /// 得到当前用户拥有的操作节点类型
        /// </summary>
        /// <returns></returns>
        public List<OperationType> GetUserOperationViewType(Guid userId)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetUserOperationViewType");
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                //List<int> results//; = (from b in ds.Tables[0].AsEnumerable()
                //                               select
                //                                  b.Field<int>("OperationType")
                //                                       ).ToList();
                List<OperationType> results = new List<OperationType>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    results.Add((OperationType) int.Parse(dr[0].ToString()));
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


        /// <summary>
        /// 得到当前用户拥有的操作节点类型
        /// </summary>
        /// <param name="OceanBookingID">海出操作ID</param>
        /// <param name="OIBookingID">海出操作ID</param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public ProfitCompare GetProfitCompare(Guid OceanBookingID, Guid OIBookingID, OperationType operationType)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.ufnGetCompareBusinessProfit");
            db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, OceanBookingID);
            db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookingID);
            db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, (int) operationType);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            {
                return null;
            }

            return new ProfitCompare()
            {
                OldProfit = decimal.Parse(ds.Tables[0].Rows[0][0].ToString()),
                NewProfit = decimal.Parse(ds.Tables[0].Rows[0][1].ToString()),
            };

        }

        /// <summary>
        /// 验证两边网络是否正常，数据是否完全同步(海进使用)
        /// </summary>
        /// <param name="OceanBookingID"></param>
        /// <param name="IsRelease"></param>
        public DataSet CompareOceanBookingCheckSum4LACo(Guid OceanBookingID, bool IsRelease)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCompareOceanBookingCheckSum4LACo");
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, OceanBookingID);
                db.AddInParameter(dbCommand, "@IsRelease", DbType.Boolean, IsRelease);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }

                return ds;
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
        /// 验证业务是否可以分发文件(已关帐,已销帐  已审核的代理账单不能分发文件)
        /// </summary>
        /// <param name="operationID">业务号</param>
        /// <returns></returns>
        public bool VerifyDispatch(Guid operationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspVerifyAgentBillIsClosedOrChargeOff");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);

                db.AddParameter(dbCommand, "@Return", DbType.Boolean, ParameterDirection.ReturnValue, "Return",
                    DataRowVersion.Default, null);
                db.ExecuteNonQuery(dbCommand);
                object result = db.GetParameterValue(dbCommand, "@Return");

                return ((int) result) > 0 ? true : false;

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
        /// 获取一个用户的分文件记录
        /// </summary>
        /// <param name="IsTransTo"></param>
        /// <param name="State"></param>
        /// <param name="userid">用户</param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public List<DispathLogData> GetDispatchFileLogForUser(bool? IsTransTo, string State, Guid userid,
            DateTime? StartDate, DateTime? EndDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDispatchFileLogForUser");

                db.AddInParameter(dbCommand, "@IsTransTo", DbType.Boolean, IsTransTo);
                db.AddInParameter(dbCommand, "@States", DbType.String, State);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userid);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, StartDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, EndDate);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<DispathLogData> results = new List<DispathLogData>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DispathLogData result = new DispathLogData();
                    result.ID = row.Field<Guid>("ID");
                    result.OperationID = row.Field<Guid>("OperationID");
                    result.OperationNo = row.Field<string>("OperationNo");
                    result.OperationType = row.Field<byte>("OperationType");
                    result.CreateBy = row.Field<Guid>("CreateBy");
                    result.CreateDate = row.Field<DateTime>("CreateDate");
                    result.AcceptBy = row.Field<Guid?>("AcceptBy");
                    result.AcceptDate = row.Field<DateTime?>("AcceptDate");
                    result.IsTransTo = row.Field<bool>("IsTransTo");
                    result.State = row.Field<byte>("State");

                    results.Add(result);
                }

                // List<DispathLogData> results = (ds.Tables[0].AsEnumerable().Select(b => new DispathLogData
                //{
                //    ID = b.Field<Guid>("ID"),
                //    OperationID = b.Field<Guid>("OperationID"),
                //    OperationNo = b.Field<string>("OperationNo"),
                //    CreateBy = b.Field<Guid>("CreateBy"),
                //    CreateDate = b.Field<DateTime>("CreateDate"),
                //    IsTransTo = b.Field<bool>("IsTransTo"),
                //    State = ((byte)b["State"] > (byte)1 ? true : false),

                //})).ToList();

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
        /// 获取一个用户的未分文件个数
        /// </summary>
        /// <param name="userid">用户</param>
        /// <returns></returns>
        public int uspGetNotDispatchedLogForUser(Guid userid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetNotDispatchedLogForUser");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return 0;
                }

                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
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
        /// 获取一个业务最新分文件记录
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        public DispathLogData GetDispatchFileLogByOperation(Guid OperationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetDispatchFileLogByOperation]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DispathLogData result = (ds.Tables[0].AsEnumerable().Select(b => new DispathLogData
                {
                    ID = b.Field<Guid>("ID"),
                    OperationID = b.Field<Guid>("OperationID"),
                    OperationNo = b.Field<string>("OperationNo"),
                    OperationType = b.Field<byte>("OperationType"),
                    CreateBy = b.Field<Guid>("CreateBy"),
                    CreateDate = b.Field<DateTime>("CreateDate"),
                    AcceptBy = b.Field<Guid?>("AcceptBy"),
                    AcceptDate = b.Field<DateTime?>("AcceptDate"),
                    IsTransTo = b.Field<bool>("IsTransTo"),
                    State = b.Field<byte>("State"),

                })).ToList()[0];

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
        /// 获取一个业务分文件状态
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        public int GetDispatchState(Guid OperationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.UspGetDispatchState");

                db.AddInParameter(dbCommand, "@CurrentOperationID", DbType.Guid, OperationID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return 0;
                }

                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
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
        /// 获取一个业务分文件状态
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        public int UspGetDispatchLogState(Guid OperationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.UspGetDispatchLogState");

                db.AddInParameter(dbCommand, "@CurrentOperationID", DbType.Guid, OperationID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return 0;
                }

                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
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
        /// 获取一个业务分发的文件
        /// </summary>
        /// <param name="LogsID">日志ID</param>
        /// <returns></returns>
        public List<DispatchFile> GetDispatchFiles(Guid LogsID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetDispatchFiles");

                db.AddInParameter(dbCommand, "@FileLogsID", DbType.Guid, LogsID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DispatchFile> results = (ds.Tables[0].AsEnumerable().Select(b => new DispatchFile
                {
                    OperationFileID = b.Field<Guid>("ID"),
                    OperationID = b.Field<Guid>("OperationID"),
                    DocumentTypeName = b.Field<string>("DocumentTypeName"),
                    OperationType = b.Field<byte>("OperationType"),
                    CreateByID = b.Field<Guid>("CreateByID"),
                    CreateDate = b.Field<DateTime>("CreateDate"),
                    CreateByName = b.Field<string>("CreateByName"),
                    DocumentType = b.Field<byte>("DocumentType"),
                    FileSource = b.Field<byte>("FileSource"),
                    FormType = b.Field<byte>("FormType"),
                    Name = b.Field<string>("Name"),
                    Remark = b.Field<string>("Remark"),
                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
                })).ToList();

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

        #region 自动分发文档
        /// <summary>
        /// 通过UserID获取未分发文档记录
        /// </summary>
        /// <returns></returns>
        public List<DispathLogData> Auto_GetNoDispatchLog()
        {
            try
            {
                //TODO_TAYLOR:通过UserID获取未分发文档记录:未创建存储过程
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTest_DispLogs");

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<DispathLogData> results = new List<DispathLogData>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DispathLogData result = new DispathLogData();
                    result.ID = row.Field<Guid>("DispatchFileLogID");
                    result.OperationID = row.Field<Guid>("operationid");
                    results.Add(result);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logID">日志ID</param>
        /// <param name="operationID"></param>
        /// <returns></returns>
        public DispatchEntityObjects Auto_GetDispatchEntityObjectsByOperationID(Guid logID, Guid operationID)
        {
            if (logID == Guid.Empty || operationID == Guid.Empty)
                return null;
            Stopwatch getEntity=Stopwatch.StartNew();
            DispatchEntityObjects returnValue = new DispatchEntityObjects();
            returnValue.LogID = logID;
            try
            {
                //获取分发文档
                returnValue.DispatchFileList = GetDispatchFiles(logID);
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTest_DispContents");

                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, logID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    returnValue.DataList = ds;
                }
                LogHelper.SaveLog("DispatchFileBuildingEntity", string.Format("Dispatch File:LogID[{0}];Building Entity[{1}ms]", logID, getEntity.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                returnValue = null;
                throw ex;
            }
            return returnValue;
        }

        /// <summary>
        /// 分发：分发文档及其数据
        /// </summary>
        /// <param name="dispatchEntity">分发文档实体</param>
        /// <returns></returns>
        public bool Auto_DispatchEntity(DispatchEntityObjects dispatchEntity)
        {
            try
            {
                //System.Diagnostics.Stopwatch saveFile = Stopwatch.StartNew();
                ////TODO_TAYLOR:保存分发文档
                //OperationLogService.Add(DateTime.Now, "DISPATCHFILE", string.Format("Dispatch File:LogID[{0}];Save File[{1}ms]", dispatchEntity.LogID, saveFile.ElapsedMilliseconds));

                //System.Diagnostics.Stopwatch saveData = Stopwatch.StartNew();
                ////TODO_TAYLOR:保存分发数据
                //OperationLogService.Add(DateTime.Now, "DISPATCHFILE", string.Format("Dispatch File:LogID[{0}];Save Data[{1}ms]", dispatchEntity.LogID, saveData.ElapsedMilliseconds));
                //TODO_TAYLOR:
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("DispatchFile", ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 更新分发文档状态
        /// </summary>
        /// <param name="logID">业务ID</param>
        /// <param name="timeSpan">时间</param>
        /// <returns></returns>
        public bool UpdateDispatchLogState(Guid logID, int timeSpan)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspInsertDispLogs");

                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, logID);
                db.AddInParameter(dbCommand, "@IsDone", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@TimeSpan", DbType.Int32, timeSpan);

                int intCount = db.ExecuteNonQuery(dbCommand);
                if (intCount > 1)
                {
                    return true;
                }
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
        /// 自动分发文件
        /// </summary>
        /// <returns></returns>
        public void AutoDispathFiles()
        {
            try
            {
                if (IsDispatch)
                    return;
                IsDispatch = true;
                //未分发记录
                List<DispathLogData> noDispathRecord = Auto_GetNoDispatchLog();
                if (noDispathRecord != null && noDispathRecord.Count > 0)
                {
                    foreach (DispathLogData logData in noDispathRecord)
                    {
                        Stopwatch saveDisaptchEntity = Stopwatch.StartNew();
                        DispatchEntityObjects dispatchEntity = Auto_GetDispatchEntityObjectsByOperationID(logData.ID,
                            logData.OperationID);
                        if (dispatchEntity == null)
                            continue;
                        //TODO_TAYLOR:获取国外服务
                        try
                        {
                            IOperationAgentService LAOperationAgentService =
                                                ServiceClient.GetService<IOperationAgentService>();
                            bool dispathResult = LAOperationAgentService.Auto_DispatchEntity(dispatchEntity);
                            if (dispathResult)
                                UpdateDispatchLogState(logData.ID, (int)saveDisaptchEntity.ElapsedMilliseconds);
                            LogHelper.SaveLog("DispatchFile", string.Format("Dispatch File:LogID[{0}]Sizeof[{1}KB];Total[{2}ms][{3}]\r\n", dispatchEntity.LogID, ObjectSize(dispatchEntity), saveDisaptchEntity.ElapsedMilliseconds, dispathResult ? "Success" : "Failure"));
                        }
                        catch (Exception ex)
                        {
                            LogHelper.SaveLog("DispatchFile", string.Format("Dispatch File:LogID[{0}];SizeOf[{1}KB]Total[{2}ms]\r\nException[{3}]\r\n", dispatchEntity.LogID, ObjectSize(dispatchEntity), saveDisaptchEntity.ElapsedMilliseconds, ex.Message));
                        }
                    }
                }
                IsDispatch = false;
            }
            catch (Exception ex)
            {
                IsDispatch = false;
                LogHelper.SaveLog("DispatchFile", ex.Message);
            }
        }

        long ObjectSize(object obj)
        {
            long size = 0;
            try
            {
                using (Stream s = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(s, obj);
                    size = s.Length / 1024;
                }
            }
            catch
            {
            }
            return size;
        }
        #endregion

    }
}

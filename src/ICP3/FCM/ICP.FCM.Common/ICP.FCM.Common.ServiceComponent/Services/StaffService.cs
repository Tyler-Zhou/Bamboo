using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace ICP.FCM.Common.ServiceComponent
{

    public partial class FCMCommonService
    {

        /// <summary>
        /// 获取员工列表数据
        /// </summary>
        /// <param name="operationID"></param>
        /// <returns></returns>
        public List<StaffObjects> GetStaffList(Guid operationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationEventList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<StaffObjects> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new StaffObjects
                                              {
                                                  //Id = b.Field<Guid>("Id"),
                                                  //OperationID = b.Field<Guid>("OperationID"),
                                                  //Code = b.Field<String>("Code"),
                                                  //Description = b.Field<String>("Description"),
                                                  //RelatedColumn = (ICP.Message.ServiceInterface.MessageType)b.Field<Byte>("RelatedColumn"),
                                                  //FormID = b.Field<Guid>("FormID"),
                                                  //FormType = (ICP.Framework.CommonLibrary.Common.FormType)b.Field<Byte>("FormType"),
                                                  //Owner = b.Field<String>("Owner"),
                                                  //CreateDate = b.Field<DateTime>("CreateDate"),
                                                  //UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// <param name="userID"></param>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public List<StaffObjects> GetAssistantList(Guid userID, Guid operationID, OperationType operationType)
        {
            return GetStaff(userID, operationID, operationType, true) as List<StaffObjects>;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public StaffObjects GetStaffInfo(Guid userID, Guid operationID, OperationType operationType)
        {
            return GetStaff(userID, operationID, operationType, false) as StaffObjects;
        }

        private object GetStaff(Guid userID, Guid operationID, OperationType operationType, bool isReturnDataList)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "OperationID");            
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationAssistantInfo]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                if (!isReturnDataList)
                {
                    StaffObjects result = (from b in ds.Tables[0].AsEnumerable()
                                           select new StaffObjects
                                               {
                                                   Id = b.Field<Guid>("ID"),
                                                   OperationID = b.Field<Guid>("OperationID"),
                                                   Mail = b.Field<string>("Mail"),
                                                   Tel = b.Field<String>("Tel"),
                                                   Name = b.Field<string>("Name"),
                                                   Ownersource = OperationType.OceanExport,
                                                   Role = b.Field<string>("RoleName"),
                                                   RoleID = b.Field<Guid>("RoleID"),
                                                   UserID = b.Field<Guid>("UserID"),
                                                   UpdateBy = b.Field<Guid?>("UpdateBy"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                   OperationUpdateDate = b.Field<DateTime?>("OperationUpdateDate")
                                               }).SingleOrDefault();

                    return result;
                }
                else
                {
                    List<StaffObjects> dataList = (from b in ds.Tables[0].AsEnumerable()
                                                   select new StaffObjects
                                                   {
                                                       Id = b.Field<Guid>("ID"),
                                                       OperationID = b.Field<Guid>("OperationID"),
                                                       Mail = b.Field<string>("Mail"),
                                                       Tel = b.Field<String>("Tel"),
                                                       Name = b.Field<string>("Name"),
                                                       Ownersource = OperationType.OceanExport,
                                                       Role = b.Field<string>("RoleName"),
                                                       RoleID = b.Field<Guid>("RoleID"),
                                                       UserID = b.Field<Guid>("UserID"),
                                                       UpdateBy = b.Field<Guid?>("UpdateBy"),
                                                       UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                       OperationUpdateDate = b.Field<DateTime?>("OperationUpdateDate")
                                                   }).ToList();

                    return dataList;
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
        /// 
        /// </summary>
        /// <param name="staff"></param>
        public void SaveAssistantInfo(StaffObjects staff)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("FCM.uspSaveOperationAssistantInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, staff.Id);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, staff.UserID);
                db.AddInParameter(dbCommand, "@RoleID", DbType.Guid, staff.RoleID);
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, staff.OperationID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, staff.UpdateDate);
                db.AddInParameter(dbCommand, "@ContactMail", DbType.String, staff.Mail);
                db.AddInParameter(dbCommand, "@OperationUpdateDate", DbType.DateTime, staff.OperationUpdateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, staff.UpdateBy);
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
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetOceanFixedRoles()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOceanFixedRoles]");
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
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
    }
}

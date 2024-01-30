using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;


namespace ICP.EDI.ServiceComponent
{
    partial class EDIService
    {
        /// <summary>
        /// 通过EDI发送选项获取EDI配置
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <returns></returns>
        public EDIConfigItem GetEDIConfigByOption(EDISendOption sendOption)
        {
            ArgumentHelper.AssertGuidNotEmpty(sendOption.CompanyID, "CompanyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetEDIConfigInfoByOption");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, sendOption.CompanyID);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, sendOption.CarrierID);
                db.AddInParameter(dbCommand, "@AgentOfCarrierID", DbType.Guid, sendOption.AgentOfCarrierID);
                db.AddInParameter(dbCommand, "@BookingPartyID", DbType.Guid, sendOption.BookingPartyID);
                db.AddInParameter(dbCommand, "@EDIType", DbType.Byte, sendOption.EdiMode);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count != 1)
                {
                    return null;
                }

                EDIConfigItem results = (from b in ds.Tables[0].AsEnumerable()
                                         select new EDIConfigItem
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             Code = b.Field<string>("Code"),
                                             SubjectPrefix = b.Field<string>("SubjectPrefix"),
                                             EDIMode = (EDIMode)b.Field<byte>("EDIType"),
                                             StoredProcedure = b.Field<string>("StoredProcedure"),
                                             DataFormat = b.Field<string>("DataFormat"),
                                             RegularFile = b.Field<string>("RegularFile"),
                                             PluginType = (EDIPluginType)b.Field<byte>("PluginType"),
                                             Component = b.Field<string>("Component"),
                                             FileFormat = b.Field<string>("FileFormat"),
                                             UploadMode = (EDIUploadMode)b.Field<byte>("UploadMode"),
                                             ServerAddress = b.Field<string>("ServerAddress"),
                                             UserName = b.Field<string>("UserName"),
                                             Password = b.Field<string>("PASSWORD"),
                                             ReceiveAddress = b.Field<string>("ReceiveAddress"),
                                             DiskPath = b.Field<string>("DiskPath"),
                                         }).SingleOrDefault();

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
        /// 获得EDI配置信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="key"></param>
        /// <param name="carrierID"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 GetEDIConfigByOption(EDISendOption sendOption) 代替")]
        public EDIConfigItem GetEDIConfig(Guid companyId, string key, Guid carrierID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetEDIConfigInfo");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@Key", DbType.String, key);
                db.AddInParameter(dbCommand, "@Carrier", DbType.Guid, carrierID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                EDIConfigItem results = (from b in ds.Tables[0].AsEnumerable()
                                         select new EDIConfigItem
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             Code = b.Field<string>("Code"),
                                             Component = b.Field<string>("Component"),
                                             FTPPath = b.Field<string>("FTP"),
                                             FileFormat = b.Field<string>("FileFormat"),
                                             DataFormat = b.Field<string>("DataFormat"),
                                             RegularFile = b.Field<string>("RegularFile"),
                                             StoredProcedure = b.Field<string>("StoredProcedure"),
                                             ServiceConfigureKeyName = b.Field<string>("ServiceConfigureKeyName"),
                                             CarrierName = b.Field<string>("CarrierName"),
                                             UploadMode = (EDIUploadMode)b.Field<byte>("UploadMode"),
                                             EDIMode = (EDIMode)b.Field<byte>("Type"),
                                             ServerAddress = b.Field<string>("ServerAddress"),
                                             UserName = b.Field<string>("UserName"),
                                             Password = b.Field<string>("PASSWORD"),
                                             ReceiveAddress = b.Field<string>("ReceiveAddress"),
                                             ToAddress = b.Field<string>("ToAddress"),
                                             CSCLLoginName = b.Field<string>("CSCLLoginName"),
                                             CSCLWebURL = b.Field<string>("CSCLWebURL"),
                                             CSCLPassword = b.Field<string>("CSCLPassword"),
                                             ComapnyAddress = b.Field<string>("CompanyAddress")
                                         }).SingleOrDefault();

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
        /// edi配置
        /// </summary>
        /// <param name="companyId">口岸ID</param>
        /// <param name="customerIds">客户集合</param>
        /// <param name="ediMode">EDI模式</param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 GetEDIConfigByOption(EDISendOption sendOption) 代替")]
        public EDIConfigItem GetEDIConfig(Guid companyId, Guid[] customerIds, EDIMode ediMode)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                string tempCustomerIDs = customerIds.Join();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetEDIConfigInfoForNew");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, tempCustomerIDs);
                db.AddInParameter(dbCommand, "@EDIMode", DbType.Byte, ediMode);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count != 1)
                {
                    return null;
                }

                EDIConfigItem results = (from b in ds.Tables[0].AsEnumerable()
                                         select new EDIConfigItem
                                         {
                                             ID = b.Field<Guid>("ID"),
                                             Code = b.Field<string>("Code"),
                                             Component = b.Field<string>("Component"),
                                             FTPPath = b.Field<string>("FTP"),
                                             FileFormat = b.Field<string>("FileFormat"),
                                             DataFormat = b.Field<string>("DataFormat"),
                                             RegularFile = b.Field<string>("RegularFile"),
                                             StoredProcedure = b.Field<string>("StoredProcedure"),
                                             UploadMode = (EDIUploadMode)b.Field<byte>("UploadMode"),
                                             EDIMode = (EDIMode)b.Field<byte>("EDIMode"),
                                             ServerAddress = b.Field<string>("ServerAddress"),
                                             UserName = b.Field<string>("UserName"),
                                             Password = b.Field<string>("PASSWORD"),
                                             ReceiveAddress = b.Field<string>("ReceiveAddress"),
                                             ToAddress = b.Field<string>("ToAddress"),
                                         }).SingleOrDefault();

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
        
    }
}

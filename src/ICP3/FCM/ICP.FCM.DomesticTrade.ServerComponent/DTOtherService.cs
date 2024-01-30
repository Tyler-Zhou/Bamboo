using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.ServiceComponent
{         
    partial class DomesticTradeService
    {  
        /// <summary>
        /// 根据销售客户和口岸公司获取揽货方式
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="opCompanyID">口岸公司</param>
        /// <returns>返回揽货方式</returns>
        public ICP.Common.ServiceInterface.DataObjects.DataDictionaryInfo GetSalesType(
            Guid customerID,
            Guid companyId,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetSalesType");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DataDictionaryInfo result = (from b in ds.Tables[0].AsEnumerable()
                                             select new ICP.Common.ServiceInterface.DataObjects.DataDictionaryInfo
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 EName = b.Field<string>("EName"),
                                                 CName = b.Field<string>("CName")
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
        /// 船公司是否存在于该订舱单的口岸公司的电子订舱EDI配置
        /// </summary>
        /// <param name="carrierId">船公司GUID</param>
        /// <param name="companyId">口岸公司GUID</param>
        /// <returns></returns>
        public bool IsCarrierInCompanyEdiBooking(Guid carrierId, Guid companyId,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(carrierId, "carrierId");
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspIsCarrierInCompanyEdiBooking");

                db.AddInParameter(dbCommand, "@CarrierId", DbType.Guid, carrierId);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return false;
                }

                bool isSame = ds.Tables[0].Rows[0][0].ToString() == "0";
                return isSame;
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
        /// 判断客户和公司是否在同一个国家
        /// </summary>
        /// <param name="customerId">客户ID( 用cusomterId获得客户的国家ID)</param>
        /// <param name="companyId">口岸公司ID(compnyId用于在获取配置里对应的客户的国家ID)</param>
        /// <returns>是否在同一个国家</returns>
        public bool IsCustomerAndCompanySameCountry(Guid customerId, Guid companyId,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerId, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspIsCustomerAndCompanySameCountry");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return false;
                }

                bool isSame = ds.Tables[0].Rows[0][0].ToString() == "1";
                return isSame;
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
        /// 判断港口的国家是否存在公司配置中的客户的国家
        /// </summary>
        /// <param name="portId">港口ID</param>
        /// <returns>是否存在</returns>
        public bool IsPortCountryExistCompanyConfig(Guid portId, Guid companyId,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(portId, "portId");
            ArgumentHelper.AssertGuidNotEmpty(companyId, "companyId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspIsPortCountryExistCompanyConfig");

                db.AddInParameter(dbCommand, "@PortID", DbType.Guid, portId);
                db.AddInParameter(dbCommand, "@CompanyId", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return false;
                }

                bool isExist = ds.Tables[0].Rows[0][0].ToString() == "0";
                return isExist;
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
        /// 获取指定口岸公司的内贸订舱单中所有揽货人(默认是有职位的用户)
        /// </summary>
        /// <param name="organizationIDs">部门ID(包括子部门的业务)</param>
        /// <param name="isEnglish"></param>
        public List<UserList> GetOrderSalesByCompanyIDs(
            Guid[] organizationIDs, bool isEnglish)
        {

            ArgumentHelper.AssertGuidNotEmpty(organizationIDs, "organizationIDs");
            try
            {

                string tempOrganizationIDs = organizationIDs.Join();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspGetOrderSalesByCompanyIDs");

                db.AddInParameter(command, "@CompanyIDS", DbType.String, tempOrganizationIDs);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, isEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new UserList
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              Code = b.Field<string>("Code"),
                                              CName = b.Field<string>("CName"),
                                              EName = b.Field<string>("EName"),
                                              Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                                              EMail = b.Field<string>("Email"),
                                              Tel = b.Field<string>("Tel"),
                                              Fax = b.Field<string>("Fax"),
                                              Mobile = b.Field<string>("Mobile"),
                                              IsValid = b.Field<bool>("IsValid"),
                                              CreateBy = b.Field<string>("CreateBy"),
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
    }
}

namespace ICP.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;

    /// <summary>
    /// 合作客户管理服务
    /// </summary>
    public class CooperationCustomerService : ICooperationCustomerService
    {

        #region init
        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;

        public CooperationCustomerService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="name">name</param>
        /// <param name="sales">sales</param>
        /// <param name="shipLine">shipLine</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回客户列表</returns>
        public List<CooperationCustomerList> GetCooperationCustomerList(
            Guid[] companyIDs,
            string name,
            string sales,
            Guid? shipLine,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCooperationCustomerList");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@companyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@sales", DbType.String, sales);
                db.AddInParameter(dbCommand, "@shipLine", DbType.Guid, shipLine);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<CooperationCustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new CooperationCustomerList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  Contact = b.Field<string>("Contact"),
                                                  BusinessInfo = b.Field<string>("BusinessInfo"),
                                                  CountryName = b.Field<string>("CountryName"),
                                                  DebtInfo = b.Field<string>("DebtInfo"),
                                                  Name = b.Field<string>("Name"),
                                                  SalesName = b.Field<string>("SalesName"),
                                                  IsDirty = false,
                                                  BusinessList = (from c in ds.Tables[1].AsEnumerable()
                                                                  where c.Field<Guid>("CustomerID") == b.Field<Guid>("ID")
                                                                  select new CompanyDescriptionList
                                                                  {
                                                                      CompanyName = c.Field<string>("CompanyName"),
                                                                      Description = c.Field<string>("Description"),

                                                                  }).ToList(),

                                                  DebtList = (from d in ds.Tables[2].AsEnumerable()
                                                              where d.Field<Guid>("CustomerID") == b.Field<Guid>("ID")
                                                              select new CompanyDescriptionList
                                                              {
                                                                  CompanyName = d.Field<string>("CompanyName"),
                                                                  Description = d.Field<string>("Description"),

                                                              }).ToList(),


                                              }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


    }
}

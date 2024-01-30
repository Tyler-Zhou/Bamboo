namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using System.Data;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using ICP.Framework.CommonLibrary.Client;

    public partial class FCMCommonService
    {
        /// <summary>
        /// 获取业务公共信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="isMBL">是否MBL</param>
        /// <param name="operationType">表单类型</param>
        /// <returns>返回业务公共信息</returns>
        public OperationCommonInfo GetOperationCommonInfo(Guid operationID, OperationType operationType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationCommonInfo");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OperationCommonInfo result = (from b in ds.Tables[0].AsEnumerable()
                                              select new OperationCommonInfo
                                              {
                                                  OperationID = b.Field<Guid>("OperationID"),
                                                  OperationNo = b.Field<string>("OperationNo"),
                                                  FreightID = b.Field<Guid?>("FreightID"),
                                                  OperationDate = b.Field<DateTime>("OperationDate"),
                                                  CompanyID = b.Field<Guid>("CompanyID"),
                                                  OperationType = operationType,
                                                  Forms = (from f in ds.Tables[1].AsEnumerable()
                                                           select new FormData
                                                           {
                                                               ID = f.Field<Guid>("FormID"),
                                                               No = f.Field<string>("No"),
                                                               Type = (FormType)f.Field<byte>("FormType"),
                                                               CntQty = f.Field<Int32>("ContainerQty")
                                                           }).ToList(),
                                                  TradeCustomers = (from c in ds.Tables[2].AsEnumerable()
                                                                    select new OperationCustomer
                                                                    {
                                                                        ID = c.Field<Guid>("ID"),
                                                                        CName = c.Field<String>("CName"),
                                                                        EName = c.Field<String>("EName"),
                                                                        Term = c.Field<int>("Term"),
                                                                        Type = (ICP.Common.ServiceInterface.DataObjects.CustomerType)c.Field<byte>("CustomerType"),
                                                                        IsAgent =c.Field<bool>("IsAgent"),
                                                                        IsCustomer = c.Field<bool>("IsCustomer")
                                                                    }).ToList()
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
    }
}

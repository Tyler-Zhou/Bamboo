using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;


namespace ICP.FCM.Common.ServiceComponent.Services
{
    class CodeService : ICodeService
    {
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }
        public List<CodeObject> CodeList(string Type) 
        {
            //ArgumentHelper.AssertGuidNotEmpty(Type, "Type");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetCodeList");

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CodeObject> contact = new List<CodeObject>();

                contact = (from b in ds.Tables[0].AsEnumerable()
                           select new CodeObject
                                            {
                                                Code = b.Field<string>("Code"),
                                                CodeType = b.Field<string>("CodeType")
                                          }).ToList();

                return contact;
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


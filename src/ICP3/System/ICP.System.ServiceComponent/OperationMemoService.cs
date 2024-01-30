using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using ICP.Framework.CommonLibrary.Common;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.ReportCenter.ServiceInterface;
using ICP.Sys.ServiceInterface;

namespace ICP.Sys.ServiceComponent
{
    public class OperationMemoService : IOperationMemoService
    {

        public bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }
        public Guid UserId
        {
            get
            {
                return ApplicationContext.Current.UserId;
            }
        }


        public Guid CompanyID
        {
            get
            {
                return ApplicationContext.Current.DefaultCompanyId;
            }
        }


        public SystemErrorLogObject GetSystemErrorLogInfoById(Guid id)
        {
            ICP.Framework.CommonLibrary.Helper.ArgumentHelper.AssertGuidNotEmpty(id, "Id");
            SystemErrorLogObject info = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[sm].[uspGetSystemErrorLogInfoByID]");
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    info = (from f in ds.Tables[0].AsEnumerable()
                            select new SystemErrorLogObject()
                             {
                                 ID = f.Field<Guid>("ID"),
                                 ScreenCapture = f.Field<byte[]>("ScreenCapture"),
                                 UserID = f.Field<Guid>("UserID"),
                                 UserName = f.Field<string>("UserName"),
                                 SessionID = f.Field<Guid>("UserID"),
                                 Description = f.Field<string>("Description"),
                                 CreateTime = f.Field<DateTime>("CreateTime")
                             }
                      ).SingleOrDefault();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return info;
        }

        public List<SystemErrorLogObject> GetSystemErrorLogList(string userName, DateTime? fromDate, DateTime? toDate)
        {
            List<SystemErrorLogObject> dataList = new List<SystemErrorLogObject>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[sm].[uspGetSystemErrorLogList]");
                db.AddInParameter(dbCommand, "@UserName", DbType.String, userName);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count == 0)
                    return dataList;
                dataList = (from f in ds.Tables[0].AsEnumerable()
                            select new SystemErrorLogObject()
                             {
                                 ID = f.Field<Guid>("ID"),
                                 SessionID = f.Field<Guid>("SessionID"),
                                 UserID = f.Field<Guid>("UserID"),
                                 UserName = f.Field<string>("UserName"),
                                 Description = f.Field<string>("Description"),
                                 CreateTime = f.Field<DateTime>("CreateTime")

                             }).ToList();
                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject GetAllList(Guid companyId)
        {

            ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject result = new ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[OA].[uspGetMailCenterOperationList]");
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables == null || ds.Tables[0] == null)
                {
                    return result;
                }

                result.FullDepartmentList = (from d in ds.Tables[0].AsEnumerable()
                                             select new ICP.ReportCenter.ServiceInterface.DataObjects.FullDepartmentObject
                                             {
                                                 ID = d.Field<Guid>("ID"),
                                                 ParentID = d.Field<Guid>("ParentID"),
                                                 Type = d.Field<byte>("Type"),
                                                 FullName = d.Field<string>("FullCName"),
                                                 CompanyAddress = d.Field<string>("CompanyAddress"),
                                                 CompanyFax = d.Field<string>("CompanyFax"),
                                                 CompanyTel = d.Field<string>("CompanyTel"),

                                             }).ToList();

                result.UserDetailInfoList = (from u in ds.Tables[1].AsEnumerable()
                                             select new ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo
                                             {
                                                 ID = u.Field<Guid>("ID"),
                                                 CName = u.Field<string>("CName"),
                                                 EName = u.Field<string>("EName"),
                                                 EMail = u.Field<string>("Email"),
                                                 Tel = u.Field<string>("Tel"),
                                                 //Fax = u.Field<string>("Fax"),
                                                 //Address = u.Field<string>("Address"),
                                                 ParentID = u.Field<Guid>("ParentID"),
                                                 Mobile = u.Field<string>("Mobile"),
                                                 RoleName = u.Field<string>("RoleName"),
                                                 CreateDate = u.Field<DateTime>("CreateDate"),
                                                 CreateBy = u.Field<string>("UserName")
                                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}

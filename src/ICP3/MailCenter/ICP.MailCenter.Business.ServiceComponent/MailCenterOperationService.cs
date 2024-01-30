using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenter.Business.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.MailCenter.Business.ServiceComponent
{
    public class MailCenterOperationService : IMailCenterOperationService
    {
        #region IMailCenterOperationService 成员
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

        public bool GetMailCenterOperationMemoByUseTime(string useTime)
        {
            bool hasRecord = true;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[OA].[uspGetMailCenterOperationMemoByUseTime]");
            db.AddInParameter(dbCommand, "@UserID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@UseTime", DbType.String, useTime);

            try
            {
                object result = db.ExecuteScalar(dbCommand);
                if (result == null || result == "")
                {
                    hasRecord = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hasRecord;
        }

        public bool SaveOperationMemo(string userName, DateTime createDate, int useDay)
        {
            bool result = false;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[OA].[uspSaveMailCenterOperationMemo]");
            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, userName);
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, this.CompanyID);
            db.AddInParameter(dbCommand, "@UseDay", DbType.Int32, useDay);
            db.AddInParameter(dbCommand, "@CreateDate", DbType.DateTime, createDate);
            try
            {
                result = db.ExecuteNonQuery(dbCommand) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion

        #region IMailCenterOperationService 成员


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

        #endregion
    }
}

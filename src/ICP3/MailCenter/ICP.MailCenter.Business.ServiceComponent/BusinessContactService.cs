using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace ICP.MailCenter.Business.ServiceComponent
{
    /// <summary>
    ///业务联系人信息服务实现类
    /// </summary>
    public class BusinessContactService : IBusinessContactService
    {
        public bool IsEnglish
        {
            get
            {
                return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.IsEnglish;
            }
        }

        public Guid UserID
        {
            get { return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId; }

        }


        /// <summary>
        /// 根据email获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public List<OperationContactInfo> GetOperationContactByEmails(List<string> emails)
        {

            if (emails == null || emails.Count <= 0)
            {
                throw new ICPException("参数emails不允许为空");
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationContactListByMail]");
            db.AddInParameter(dbCommand, "@Mails", DbType.String, emails.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count == 0)
                {
                    //List<OperationContactInfo> contactList = new List<OperationContactInfo>();
                    //foreach (string mail in emails)
                    //{
                    //    OperationContactInfo contactInfo = new OperationContactInfo();
                    //    contactInfo.Mail = mail;
                    //    contactList.Add(contactInfo);
                    //}
                    return new List<OperationContactInfo>();
                }
                else
                    return ConvertTableToOperationContactInfos(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取业务联系人信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public OperationContactInfo GetOperationContactInfo(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ICPException("方法GetOperationContactInfo参数email不允许为空");
            }
            List<OperationContactInfo> contactList = GetOperationContactByEmails(new List<string> { email });
            if (contactList == null || contactList.Count <= 0)
            {
                OperationContactInfo contactInfo = new OperationContactInfo();
                contactInfo.Mail = email;
                return contactInfo;
            }
            else
            {
                return contactList[0];
            }
        }

        private List<OperationContactInfo> ConvertTableToOperationContactInfos(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<OperationContactInfo>();
            }
            List<OperationContactInfo> contactInfos = (from row in dt.AsEnumerable()
                                                       select new OperationContactInfo()
                                             {
                                                 ID = row.Field<Guid>("Id"),
                                                 Mail = row.Field<String>("Mail"),
                                                 CustomerID = row.Field<Guid?>("CustomerID"),
                                                 AE = row.Field<bool?>("IsAE"),
                                                 AI = row.Field<bool?>("IsAI"),
                                                 Carrier = row.Field<bool>("IsCarrier"),
                                                 Customer = row.Field<bool>("IsCustomer"),
                                                 OE = row.Field<bool?>("IsOE"),
                                                 Other = row.Field<bool?>("IsOther"),
                                                 OI = row.Field<bool?>("IsOI"),
                                                 Trk = row.Field<bool?>("IsTrk"),
                                                 UpdateDate = row.Field<DateTime?>("UpdateDate"),

                                             }).ToList();
            return contactInfos;
        }

        /// <summary>
        /// 获取所有邮件联系人对象
        /// </summary>
        /// <returns>邮件联系人对象集合</returns>
        public List<EmailContactInfo> GetEmailContactList()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspGetEmailContactList]");
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count == 0)
                {
                    return new List<EmailContactInfo>();
                }
                else
                    return ConvertTableToEmailContactInfo(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 集成查询结果至集合
        /// </summary>
        /// <param name="dt">结果集</param>
        /// <returns></returns>
        private List<EmailContactInfo> ConvertTableToEmailContactInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<EmailContactInfo>();
            }
            List<EmailContactInfo> contactInfos = (from row in dt.AsEnumerable()
                                                   select new EmailContactInfo()
                                                   {
                                                       EnName = row.Field<String>("EName"),
                                                       CnName = row.Field<String>("CName"),
                                                       Email1Address = row.Field<String>("Email"),
                                                       BusinessTelephoneNumber = row.Field<String>("Tel"),
                                                       BusinessFaxNumber = row.Field<String>("Fax"),
                                                       MobileTelephoneNumber = row.Field<String>("Mobile"),
                                                       Department = row.Field<String>("FullName"),
                                                       BusinessAddressStreet = row.Field<String>("Address"),
                                                       JobTitle = row.Field<String>("JobTitle"),
                                                       IsInOffice = row.Field<Boolean>("IsValid"),
                                                   }).ToList();
            return contactInfos;
        }
    }
}

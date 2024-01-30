using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 联系人列表数据服务实现
    /// </summary>
    public partial class FCMCommonService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="contactType"></param>
        /// <param name="contactStage"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public List<CustomerCarrierObjects> GetContactListByContactStage(Guid operationId, OperationType operationType, ContactType contactType, ContactStage contactStage, Guid? CustomerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationId");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationContactListByContactStage");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, operationType);
                db.AddInParameter(dbCommand, "@ContactType", DbType.Byte, contactType.GetHashCode());
                db.AddInParameter(dbCommand, "@ContactStage", DbType.Byte, contactStage.GetHashCode());
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, CustomerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return new List<CustomerCarrierObjects>();
                }


                List<CustomerCarrierObjects> contactList = ConvertTableToCustomerCarrierList(ds.Tables[0]);

                return contactList;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取指定客户最近一票的业务联系人信息
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <param name="companyID">操作口岸</param>
        /// <param name="customerId">客户ID</param>
        /// <param name="contactType">联系人类型(客户/承运人)</param>
        /// <param name="contactStage">沟通阶段</param>
        /// <returns></returns>
        public List<CustomerCarrierObjects> GetLatestContactList(OperationType operationType, Guid companyID, Guid customerId, ContactType contactType, ContactStage contactStage)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetLatestOperationContactList");
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, operationType);
                db.AddInParameter(dbCommand, "@ContactType", DbType.Byte, contactType.GetHashCode());
                db.AddInParameter(dbCommand, "@ContactStage", DbType.Byte, contactStage.GetHashCode());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return new List<CustomerCarrierObjects>();
                }
                List<CustomerCarrierObjects> contactList = ConvertTableToCustomerCarrierList(ds.Tables[0]);

                return contactList;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定客户最近一票的业务联系人信息
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <param name="companyID">操作口岸</param>
        /// <param name="customerId">客户ID</param>
        /// <param name="contactType">联系人类型(客户/承运人)</param>
        /// <param name="contactStage">沟通阶段</param>
        /// <returns></returns>
        public List<CustomerCarrierObjects> GetLatesterContactList(OperationType operationType, Guid? companyID, Guid? customerId, ContactType contactType, ContactStage contactStage)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetLatestOperationContactList");
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, operationType);
                db.AddInParameter(dbCommand, "@ContactType", DbType.Byte, contactType.GetHashCode());
                db.AddInParameter(dbCommand, "@ContactStage", DbType.Byte, contactStage.GetHashCode());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return new List<CustomerCarrierObjects>();
                }
                List<CustomerCarrierObjects> contactList = ConvertTableToCustomerCarrierList(ds.Tables[0]);

                return contactList;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<CustomerCarrierObjects> ConvertTableToCustomerCarrierList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<CustomerCarrierObjects>();
            }
            List<CustomerCarrierObjects> contactList = new List<CustomerCarrierObjects>();
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow b = dt.Rows[i];
                CustomerCarrierObjects contactInfo = new CustomerCarrierObjects
                                                             {
                                                                 Id = b.Field<Guid>("Id"),
                                                                 OceanBookingID = b.Field<Guid>("OceanBookingID"),
                                                                 CreateByID = b.Field<Guid?>("CreateBy"),
                                                                 CreateDate = b.Field<DateTime?>("CreateDate"),
                                                                 BL = b.Field<bool>("IsBL"),
                                                                 IsCC = b.Field<bool>("IsCC"),
                                                                 CF = b.Field<bool>("IsCF"),
                                                                 FU = b.Field<bool>("IsFU"),
                                                                 IN = b.Field<bool>("IsIN"),
                                                                 SI = b.Field<bool>("IsSI"),
                                                                 SO = b.Field<bool>("IsSO"),
                                                                 AN = b.Field<bool>("IsAN"),
                                                                 Trk = b.Field<bool>("IsTrk"),
                                                                 Whs = b.Field<bool>("IsWhs"),
                                                                 AR = b.Field<bool>("IsAR"),
                                                                 Release = b.Field<bool>("IsRelease"),
                                                                 Fax = b.Field<string>("Fax"),
                                                                 Mail = b.Field<string>("Mail"),
                                                                 Mobile = b.Field<string>("Mobile"),
                                                                 Name = b.Field<string>("Name"),
                                                                 Tel = b.Field<string>("Tel"),
                                                                 BillID = b.Field<Guid?>("BillID"),
                                                                 UpdateByID = b.Field<Guid?>("UpdateBy"),
                                                                 UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                 Type = (ContactType)b.Field<Byte>("Type"),
                                                                 CustomerID = b.Field<Guid?>("CustomerID"),
                                                                 CustomerName = b.Field<string>("CustomerName")
                                                               
                                                             };

                contactInfo.Stage = FCMInterfaceUtility.GetStage(contactInfo);
                contactList.Add(contactInfo);
            }

            return contactList;
        }

        /// <summary>
        /// 通过邮件地址获取业务
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public List<CustomerCarrierObjects> GetOceanContactsByEmails(List<string> emails)
        {
            if (emails == null || emails.Count <= 0)
                return new List<CustomerCarrierObjects>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.[uspGetOceanContactsByEmails]");
                db.AddInParameter(dbCommand, "@Emails", DbType.String, emails.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 0)
                {
                    return new List<CustomerCarrierObjects>();
                }


                return ConvertTableToCustomerCarrierList(ds.Tables[0]);
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
        /// 根据获取联系人列表
        /// </summary>
        /// <returns></returns>
        public ContactObjects GetContactList(Guid OperationID, OperationType operationType)
        {
            ArgumentHelper.AssertGuidNotEmpty(OperationID, "OperationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationContactList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, operationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    ContactObjects temp = new ContactObjects();
                    temp.CustomerCarrier = new List<CustomerCarrierObjects>();
                    temp.StaffList = new List<StaffObjects>();
                    return temp;
                }

                ContactObjects contact = new ContactObjects();
                contact.CustomerCarrier = ConvertTableToCustomerCarrierList(ds.Tables[0]);
                contact.StaffList = (from b in ds.Tables[1].AsEnumerable()
                                     select new StaffObjects
                                     {
                                         CanUpdate = false,
                                         IsDirty = false,
                                         Role = b.Field<string>("Role"),
                                         Name = b.Field<string>("Name"),
                                         Mail = b.Field<string>("Mail"),
                                         Tel = b.Field<string>("Tel")
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

        /// <summary>
        /// 根据获取联系人列表
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        public List<ReleaseAndArContact> GetReleaseAndArContactList(Guid OperationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(OperationID, "OperationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationReleaseAndArContactList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                List<ReleaseAndArContact> contact = new List<ReleaseAndArContact>();
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds != null)
                {
                    contact = ConvertTableToReleaseAndArList(ds.Tables[0]);
                }

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

        private List<ReleaseAndArContact> ConvertTableToReleaseAndArList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<ReleaseAndArContact>();
            }
            List<ReleaseAndArContact> contactList = new List<ReleaseAndArContact>();
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow b = dt.Rows[i];
                ReleaseAndArContact contactInfo = new ReleaseAndArContact
                {
                    OperationID = b.Field<Guid>("OperationID"),
                    CustomerID = b.Field<Guid?>("CustomerID"),
                    IsAR = b.Field<bool>("IsAR"),
                    IsRelease = b.Field<bool>("IsRelease"),
                    Mail = b.Field<string>("Mail"),
                    IsLast = b.Field<bool>("IsLast")
                };
                contactList.Add(contactInfo);
            }

            return contactList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="OceanBookingIDs"></param>
        /// <param name="operationTypes"></param>
        /// <param name="Types"></param>
        /// <param name="SOs"></param>
        /// <param name="Trks"></param>
        /// <param name="CFs"></param>
        /// <param name="CIs"></param>
        /// <param name="Whss"></param>
        /// <param name="QIs"></param>
        /// <param name="SIs"></param>
        /// <param name="BLs"></param>
        /// <param name="ANs"></param>
        /// <param name="FUs"></param>
        /// <param name="INs"></param>
        /// <param name="CCs"></param>
        /// <param name="Names"></param>
        /// <param name="Emails"></param>
        /// <param name="Tels"></param>
        /// <param name="Faxs"></param>
        /// <param name="ARs"></param>
        /// <param name="Releases"></param>
        /// <param name="UpdateDates"></param>
        /// <param name="CreateByIDs"></param>
        /// <param name="CreateDates"></param>
        /// <param name="CustomerIDs"></param>
        /// <param name="BillIDs"></param>
        /// <returns></returns>
        public ManyResult SaveContactList(Guid?[] Ids,
                    Guid[] OceanBookingIDs,
                   OperationType[] operationTypes,
            ContactType[] Types,
                   bool[] SOs,
                   bool[] Trks,
                   bool[] CFs,
                   bool[] CIs,
                   bool[] Whss,
                   bool[] QIs,
                   bool[] SIs,
                   bool[] BLs,
                   bool[] ANs,
                   bool[] FUs,
                   bool[] INs,
                   bool[] CCs,
                   string[] Names,
                   string[] Emails,
                   string[] Tels,
                   string[] Faxs,
                   bool[] ARs,
                   bool[] Releases,
                   DateTime?[] UpdateDates,
                   Guid?[] CreateByIDs,
                   DateTime?[] CreateDates,
                   Guid?[] CustomerIDs,
                   Guid?[] BillIDs)
        {
            if (Ids == null || Ids.Length <= 0)
                return null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOperationContactList");

                int length = Types.Length;
                int[] listTypes = new int[length];
                int[] _operationTypes = new int[length];

                for (int i = 0; i < length; i++)
                {
                    listTypes[i] = Types[i].GetHashCode();
                    _operationTypes[i] = operationTypes[i].GetHashCode();
                }

                db.AddInParameter(dbCommand, "@Ids", DbType.String, Ids.Join());
                db.AddInParameter(dbCommand, "@OceanBookingIDs", DbType.String, OceanBookingIDs.Join());
                db.AddInParameter(dbCommand, "@OperationTypes", DbType.String, _operationTypes.Join());
                db.AddInParameter(dbCommand, "@IsSOs", DbType.String, SOs.Join());
                db.AddInParameter(dbCommand, "@IsTrks", DbType.String, Trks.Join());
                db.AddInParameter(dbCommand, "@IsCFs", DbType.String, CFs.Join());
                db.AddInParameter(dbCommand, "@IsCIs", DbType.String, CIs.Join());
                db.AddInParameter(dbCommand, "@IsWhss", DbType.String, Whss.Join());
                db.AddInParameter(dbCommand, "@IsQIs", DbType.String, QIs.Join());
                db.AddInParameter(dbCommand, "@IsSIs", DbType.String, SIs.Join());
                db.AddInParameter(dbCommand, "@IsBLs", DbType.String, BLs.Join());
                db.AddInParameter(dbCommand, "@IsANs", DbType.String, ANs.Join());
                db.AddInParameter(dbCommand, "@IsCCs", DbType.String, CCs.Join());
                db.AddInParameter(dbCommand, "@IsFUs", DbType.String, FUs.Join());
                db.AddInParameter(dbCommand, "@IsINs", DbType.String, INs.Join());
                db.AddInParameter(dbCommand, "@Types ", DbType.String, listTypes.Join());
                db.AddInParameter(dbCommand, "@Names", DbType.String, Names.Join());
                db.AddInParameter(dbCommand, "@Mails", DbType.String, Emails.Join());
                db.AddInParameter(dbCommand, "@Tels", DbType.String, Tels.Join());
                db.AddInParameter(dbCommand, "@Faxs", DbType.String, Faxs.Join());
                db.AddInParameter(dbCommand, "@ARs", DbType.String, ARs.Join());
                db.AddInParameter(dbCommand, "@Releases", DbType.String, Releases.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, CustomerIDs.Join());
                db.AddInParameter(dbCommand, "@BillIDs", DbType.String, BillIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, UpdateDates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, UserId);
                db.AddInParameter(dbCommand, "@IsEnglish ", DbType.Boolean, IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate", "Type" });
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
        /// 保存多个联系人
        /// </summary>
        /// <returns></returns>
        public ManyResult SaveContactList(List<CustomerCarrierObjects> customerList)
        {
            if (customerList == null || customerList.Count <= 0)
                return null;

            int listcount = customerList.Count;

            Guid?[] Ids = new Guid?[listcount];
            bool[] SOs = new bool[listcount];
            bool[] Trks = new bool[listcount];
            bool[] CFs = new bool[listcount];
            bool[] SIs = new bool[listcount];
            bool[] BLs = new bool[listcount];
            bool[] CCs = new bool[listcount];
            bool[] FUs = new bool[listcount];
            bool[] Whss = new bool[listcount];
            bool[] INs = new bool[listcount];
            bool[] ANs = new bool[listcount];
            string[] Names = new string[listcount];
            string[] Emails = new string[listcount];
            string[] Tels = new string[listcount];
            string[] Faxs = new string[listcount];
            bool[] ARs = new bool[listcount];
            bool[] Releases = new bool[listcount];
            DateTime?[] UpdateDates = new DateTime?[listcount];
            DateTime?[] CreateDates = new DateTime?[listcount];
            Guid?[] CreateByIDs = new Guid?[listcount];
            Guid?[] customerIds = new Guid?[listcount];
            Guid?[] BillIDs = new Guid?[listcount];
            Guid[] operationIds = new Guid[listcount];
            OperationType[] operationTypes = new OperationType[listcount];
            ContactType[] contactTypes =
                new ContactType[listcount];

            for (int i = 0; i < listcount; i++)
            {
                CustomerCarrierObjects cid = customerList[i];
                Ids[i] = cid.Id;
                contactTypes[i] = cid.Type;
                operationTypes[i] = cid.OperationType == null ? OperationType.OceanExport : cid.OperationType.Value;
                SOs[i] = cid.SO;
                Trks[i] = cid.Trk;
                CFs[i] = cid.CF;
                operationIds[i] = cid.OceanBookingID;
                SIs[i] = cid.SI;
                BLs[i] = cid.BL;
                ANs[i] = cid.AN;

                CCs[i] = cid.IsCC;
                FUs[i] = cid.FU;
                Whss[i] = cid.Whs;
                INs[i] = cid.IN;
                Names[i] = cid.Name.Trim();
                Emails[i] = cid.Mail.Trim();
                Tels[i] = string.IsNullOrEmpty(cid.Tel) ? string.Empty : cid.Tel.Trim();
                Faxs[i] = string.IsNullOrEmpty(cid.Fax) ? string.Empty : cid.Fax.Trim();
                ARs[i] = cid.AR;
                Releases[i] = cid.Release;
                CreateDates[i] = cid.CreateDate;
                CreateByIDs[i] = cid.CreateByID;
                BillIDs[i] = cid.BillID;
                UpdateDates[i] = cid.UpdateDate;
                customerIds[i] = cid.CustomerID;
            }

            return SaveContactList(Ids, operationIds, operationTypes, contactTypes, SOs, Trks, CFs, FUs,
                Whss, INs, SIs, BLs, ANs, FUs, INs, CCs, Names, Emails, Tels, Faxs, ARs, Releases, UpdateDates, CreateByIDs, CreateDates, customerIds, BillIDs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="removeByID"></param>
        /// <param name="updateDate"></param>
        public void RemoveContactInfo(
             Guid Id,  //
             Guid removeByID,
             DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(Id, "Id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationContactInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, Id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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
    }
}

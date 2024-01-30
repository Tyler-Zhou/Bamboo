using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FAM.ServiceComponent
{
    public partial class FinanceService
    {
        #region 获得电放列表
        /// <summary>
        /// 获得电放列表
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="consigneeName"></param>
        /// <param name="applicantName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<TelexApplyList> GetTelexApplyList(
            Guid[] companyIds,
            string customerName,
            string consigneeName,
            string applicantName,
            DateTime? from, 
            DateTime? to, 
            int totalRecords)
        {
            List<TelexApplyList> result = new List<TelexApplyList>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetTelexApplyList");

                string companyIdList = companyIds.Join();

                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, companyIdList);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@ConsigneeName", DbType.String, consigneeName);
                db.AddInParameter(dbCommand, "@ApplicantName", DbType.String, applicantName);
                db.AddInParameter(dbCommand, "@FromTime", DbType.DateTime, from);
                db.AddInParameter(dbCommand, "@ToTime", DbType.DateTime, to);
                db.AddInParameter(dbCommand, "@TotalRecords", DbType.Int32, totalRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<TelexApplyList> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new TelexApplyList
                                          {
                                              ID = b.Column<Guid>("ID"),
                                              CompanyName = b.Column<string>("CompanyName"),
                                              CustomerCName = b.Column<string>("CustomerCName"),
                                              CustomerEName = b.Column<string>("CustomerEName"),
                                              ConsigneeName = b.Column<string>("ConsigneeName"),
                                              ApplyTime = b.Column<DateTime>("ApplyTime"),
                                              ValidDate = b.Column<DateTime>("ValidDate"),
                                              Remark = b.Column<string>("Remark"),
                                              CreateByID = b.Column<Guid>("CreateBy"),
                                              CreateByName = b.Column<string>("CreateByName"),
                                              CustomerId = b.Column<Guid>("CustomerId"),
                                              IsValid = b.Column<bool>("IsValid"),
                                              TelexType = (TelexType)b.Column<byte>("TelexType"),
                                              UpdateDate = b.Column<DateTime?>("UpdateDate")
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

        #endregion

        #region 获得电放详细信息
        /// <summary>
        /// 获得电放详细信息
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public TelexApplyInfo GetTelexApply(Guid requestId)
        {
            ArgumentHelper.AssertGuidNotEmpty(requestId, "requestId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetTelexApply");

                db.AddInParameter(dbCommand, "@RequestId", DbType.Guid, requestId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                if (ds.Tables[0].Rows.Count > 1)
                {
                    throw new Exception("Found more than 1 booking orders. It's impossible!");
                }

                TelexApplyInfo result = (from b in ds.Tables[0].AsEnumerable()
                                         select new TelexApplyInfo
                                         {
                                             IsValid = b.Column<bool>("IsValid"),
                                             CompanyId = b.Column<Guid>("CompanyId"),
                                             CompanyName = b.Column<string>("CompanyName"),
                                             CustomerId = b.Column<Guid>("CustomerId"),
                                             CustomerCName = b.Column<string>("CustomerCName"),
                                             CustomerEName = b.Column<string>("CustomerEName"),
                                             CustomerDescription = b.Column<string>("CustomerDescription"),
                                             ApplyTime = b.Column<DateTime>("ApplyTime"),
                                             Remark = b.Column<string>("Remark"),
                                             ID = b.Column<Guid>("ID"),
                                             CreateByID = b.Column<Guid>("CreateBy"),
                                             CreateByName = b.Column<string>("CreateByName"),
                                             ValidDate = b.Column<DateTime>("ValidDate"),
                                             TelexType = (TelexType)b.Column<byte>("TelexType"),
                                             UpdateDate = b.Column<DateTime?>("UpdateDate"),
                                             Consignees = (from h in ds.Tables[1].AsEnumerable()
                                                           select new TelexConsignee
                                                           {
                                                               CustomerId = h.Column<Guid>("ConsigneeId"),
                                                               CustomerName = h.Column<string>("ConsigneeName")
                                                           }).ToList(),
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
        #endregion

        #region 改变电放有效性
        /// <summary>
        /// 改变电放有效性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <param name="saveById"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        public SingleResult ChangeTelexRequestValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.ChangeTelexValidity");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@isCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
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
        #endregion

        #region 保存电放信息
        /// <summary>
        /// 保存电放信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        public SingleResult SaveTelexRequest(TelexRequestSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.CustomerId, "CustomerId");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.CompanyId, "CompanyId");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.CreateById, "CreateById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveTelexRequest");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.Id);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.CustomerId);
                db.AddInParameter(dbCommand, "@CustomerDescription", DbType.String, saveRequest.CustomerDescription);
                db.AddInParameter(dbCommand, "@TelexType", DbType.Byte, saveRequest.TelexType);
                db.AddInParameter(dbCommand, "@consigneeID", DbType.String, saveRequest.ConsigneeIds.ToArray().Join());
                db.AddInParameter(dbCommand, "@ApplyTime", DbType.DateTime, saveRequest.ApplyTime);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyId);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveRequest.CreateById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@ValidDate", DbType.DateTime, saveRequest.ValidDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, saveRequest.IsValid);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

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
        #endregion
    }
}

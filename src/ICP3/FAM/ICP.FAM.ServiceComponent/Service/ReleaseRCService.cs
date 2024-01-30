using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;


namespace ICP.FAM.ServiceComponent
{
    public partial class FinanceService
    {

        #region GetList

        /// <summary>
        /// 获取放货列表
        /// </summary>
        /// <param name="RCCompany">放货分公司</param>
        /// <param name="releaseBLState">放单状态(已创建=1、已签收=2、已申请 =3已放单=4、已接收=5)多选数组</param>
        /// <param name="releaseType">放单状态(正本 1 ,电放 2)可为空</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="consignee">收货人</param>
        /// <param name="vessel">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="releaseBeginTime">放单开始时间</param>
        /// <param name="releaseEndTime">放单结束时间</param>
        ///  <param name="releaseECBeginTime">放货开始时间</param>
        /// <param name="releaseRCEndTime">放货结束时间</param>
        /// <param name="IsEnglish">是否英文环境</param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns>ReleaseBLList</returns>
        public ReleasePageList GetReleaseRCList(Guid[] RCCompany
                                            ,ReleaseRCState? releaseBLState
                                            , ReleaseType? releaseType
                                            , string blNo
                                            , string ctnNo
                                            , string consignee
                                            , string vessel
                                            , string voyageNo                                          
                                            , DateTime? releaseBeginTime
                                            , DateTime? releaseEndTime
                                            , DateTime? releaseECBeginTime
                                            , DateTime? releaseRCEndTime
                                            , bool IsEnglish
                                            , DataPageInfo dataPageInfo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetReleaseCargoList");

                string companys = RCCompany.Join();
                //string tempReleaseBLState = releaseBLState.Join();

                db.AddInParameter(dbCommand, "@RcCompanyIDs", DbType.String, companys);
                db.AddInParameter(dbCommand, "@ReleaseBLState", DbType.Byte, releaseBLState);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ctnNo", DbType.String, ctnNo);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Byte, releaseType);
                db.AddInParameter(dbCommand, "@ConsigneeName", DbType.String, consignee);
                db.AddInParameter(dbCommand, "@vessel", DbType.String, vessel);
                db.AddInParameter(dbCommand, "@voyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@RcBeginTime", DbType.DateTime, releaseECBeginTime);
                db.AddInParameter(dbCommand, "@RcEndTime", DbType.DateTime, releaseRCEndTime);
                db.AddInParameter(dbCommand, "@ReleaseBeginTime", DbType.DateTime, releaseBeginTime);
                db.AddInParameter(dbCommand, "@ReleaseEndTime", DbType.DateTime, releaseEndTime);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@currentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<ReleaseRCList> results = BulidReleaseRCListByDataSet(ds);
                dataPageInfo.TotalCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                ReleasePageList PageList =ReleasePageList.Create<ReleaseRCList>(results, dataPageInfo);


                return PageList;
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

        private List<ReleaseRCList> BulidReleaseRCListByDataSet(DataSet ds)
        {
            List<ReleaseRCList> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new ReleaseRCList
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               State = (ReleaseRCState)b.Field<byte>("State"),
                                               FETA = b.Field<DateTime?>("FETA"),
                                               BlNo = b.Field<string>("BlNo"),
                                               AgentName = b.Field<string>("AgentName"),
                                               BLType = (ICP.Framework.CommonLibrary.Common.FormType)b.Field<byte>("BLType"),
                                               ContainerNos = b.Field<string>("ContainerNos"),
                                               VesselVoyage = b.Field<string>("VesselVoyage"),
                                               ETA = b.Field<DateTime?>("ETA"),
                                               ReleaseType = (ReleaseType)b.Field<byte>("ReleaseType"),
                                               ConsigneeName=b.Field<string>("ConsigneeName"),
                                               ConsigneeContact=b.Field<string>("ConsigneeContact"),
                                               ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                               ReleaseBy = b.Field<string>("ReleaseBy"),
                                               RcBy=b.Field<string>("RcBy"),
                                               RcDate=b.Field<DateTime?>("RcDate"),
                                               RcCompanyID=b.Field<Guid>("RcCompanyID"),
                                               RcCompanyName=b.Field<string>("RcCompanyName"),
                                               TelexNo = b.Field<string>("TelexNo"),
                                               RcRemark = b.Field<string>("RcRemark"),
                                               CreateByID = b.Field<Guid>("CreateByID"),
                                               CreateByName = b.Field<string>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               OperationID=b.Field<Guid>("OperationID"),
                                               OperationNo = b.Field<string>("OperationNo"),
                                               IsDirty = false
                                           }).ToList();
            return results;
        }

        /// <summary>
        /// 获取放单列表
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns>ReleaseBLList</returns>
        public PageList GetReleaseRCList(Guid[] ids)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetReleaseBLListByIDs");

                string tempIds = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ReleaseRCList> results = BulidReleaseRCListByDataSet(ds);
                PageList PageList = PageList.Create<ReleaseRCList>(results, null);
                return PageList;
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


        /// <summary>
        /// 是否已收到放单通知
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public ManyResult ChangeReleaseRCState(
            Guid[] ids,
            Guid changeByID,
            DateTime?[] updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeReceivedCargoState");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, updateDate.Join());
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// 标识是否已放货
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">状态(正本 1 ,电放 2)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeReleaseRC(
             Guid id,
            bool IsValid,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeReleaseCargoState");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Int16, IsValid);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate"});
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        ///修改备注
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="remark">备注信息</param>
        /// <param name="savaByID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult SaveReleaseRemark(
             Guid id,
            string remark,
            DateTime? updateDate,
            Guid savaByID,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(savaByID, "savaByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveReleaseCargoRemark");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RcRemark", DbType.String,remark);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, savaByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        ///转移放货公司
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="NewCompanyID">新公司</param>
        /// <param name="savaByID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult SaveReleaseCompany(
             Guid id,
            Guid NewCompanyID,
            DateTime? updateDate,
            Guid savaByID,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(savaByID, "savaByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspTransferReleaseCargo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@NewCompanyID", DbType.Guid, NewCompanyID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, savaByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }

    }
}

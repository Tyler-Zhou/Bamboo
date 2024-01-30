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
    using ICP.Common.ServiceInterface.DataObjects;

    public partial class FCMCommonService
    {
        #region Memo

        /// <summary>
        /// 获取备注列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="ownerID">所有者ID</param>
        /// <returns>返回备注列表</returns>
        public List<CommonMemoList> GetMemoList(
            Guid operationID,
            //ICP.Framework.CommonLibrary.Client.OperationType operationType,
            Guid? ownerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationMemoList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@FormID", DbType.Guid, ownerID);         
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CommonMemoList> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new CommonMemoList
                                                  {  
                                                      ID = b.Field<Guid>("ID"),
                                                      OperationID = b.Field<Guid>("OperationID"),
                                                      //OwnerID = 
                                                      Subject = b.Field<String>("Subject"),
                                                      Content = b.Field<String>("Content"),
                                                      IsShowAgent = b.Field<bool>("IsShowAgent"),
                                                      IsShowCustomer = b.Field<bool>("IsShowCustomer"),
                                                      //Priority = (MemoPriority)b.Field<Byte>("Priority"),
                                                      //Type = (MemoType)b.Field<Byte>("Type"),
                                                      FormID=b.Field<Guid>("FormID"),
                                                      FormType=(ICP.Framework.CommonLibrary.Common.FormType)b.Field<Byte>("FormType"),
                                                      CreateByID = b.Field<Guid>("CreateBy"),
                                                      CreateByName = b.Field<String>("CreateByName"),
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

        /// <summary>
        /// 获取备注信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回备注信</returns>
        public CommonMemoInfo GetMemoInfo(Guid id)
        {
            return new CommonMemoInfo();
        }

        /// <summary>
        /// 保存备注信息
        /// </summary>
        /// <param name="ownerID">所有者ID</param>
        /// <param name="ownerSource">所属业务(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="id">ID</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <param name="attachName">附件名</param>
        /// <param name="attachment">附件内容</param>
        /// <param name="memoType">备注类型</param>
        /// <param name="keyID">字典ID</param>
        /// <param name="isShowAgent">是否代理可见</param>
        /// <param name="isShowCustomer">是否客户可见</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveMemoList(
             Guid operationID,
            ICP.Framework.CommonLibrary.Common.OperationType operationType,
            Guid?[] ids,
            Guid?[] formIDs,
            ICP.Framework.CommonLibrary.Common.FormType[] formTypes,
            bool[] isShowAgents,
            bool[] isShowCustomers,
            string[] subjects,
            string[] contents,
            CommonData.MemoType[] memoTypes,
            MemoPriority[] prioritys,
            //string[] attachName,
            //byte[][] attachment,        
            //Guid?[] keyID,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOperationMemoInfo");

                string tempids = ids.Join();
                string tempFormIDs = formIDs.Join();
                string tempFormTypes = formTypes.Join<ICP.Framework.CommonLibrary.Common.FormType>();
                string tempIsShowAgents = isShowAgents.Join();
                string tempIsShowCustomers = isShowCustomers.Join();
                string tempSubjects = subjects.Join();
                string tempContents = contents.Join();
                string tempTypes = memoTypes.Join<CommonData.MemoType>();
                string tempPrioeritys = prioritys.Join<MemoPriority>();
                string tempUpdateDates = updateDates.Join();         

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);   //业务ID
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, operationType); //业务类型
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempids);   //ID(),即备注ID
                db.AddInParameter(dbCommand, "@FormIDs", DbType.String, tempFormIDs);   //表单ID,如订舱ID，提单ID
                db.AddInParameter(dbCommand, "@FormTypes", DbType.String, tempFormTypes); //表单类型
                db.AddInParameter(dbCommand, "@IsShowAgents", DbType.String, tempIsShowAgents);
                db.AddInParameter(dbCommand, "@IsShowCustomers", DbType.String, tempIsShowCustomers);
                db.AddInParameter(dbCommand, "@Subjects", DbType.String, tempSubjects);
                db.AddInParameter(dbCommand, "@Contents", DbType.String, tempContents);
                db.AddInParameter(dbCommand, "@Types", DbType.String, tempTypes);
                db.AddInParameter(dbCommand, "@Prioritys", DbType.String, tempPrioeritys);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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
        /// 获取备注附件内容
        /// </summary>
        /// <param name="memoID">备注ID</param>
        /// <param name="attachName">附件名</param>
        /// <returns>返回附件内容</returns>
        public byte[] GetMemoAttachmentContent(
            Guid memoID,
            string attachName)
        {
            return null;
        }

        /// <summary>
        /// 删除备注信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <returns>返回SingleResult</returns>
        public void RemoveMemoInfo(
            Guid memoId,  //是memo.id
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(memoId, "memoId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationMemoInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, memoId);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        #endregion
    }
}

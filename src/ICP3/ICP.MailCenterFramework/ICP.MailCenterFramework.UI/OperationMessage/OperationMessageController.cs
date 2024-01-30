using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// [业务与邮件关联]的获取保存类
    /// </summary>
    public class OperationMessageController
    {
        #region Propetry & Services
        /// <summary>
        /// 用户默认所属的公司Id
        /// </summary>
        public Guid DefaultCompanyId
        {
            get { return ApplicationContext.Current.DefaultCompanyId; }
        }

        ///// <summary>
        ///// 缓存数据库服务接口
        ///// </summary>
        //public IDataCacheOperationService DataCacheOperationService
        //{
        //    get
        //    {
        //        IDataCacheOperationService temp = null;
        //        try
        //        {
        //            temp = ServiceClient.GetService<IDataCacheOperationService>();
        //            //temp = null;
        //        }
        //        catch (Exception ex)
        //        {
        //            ToolUtility.WriteLog("OperationMessageController DataCacheOperationService", ex);
        //            temp = null;
        //        }
        //        return temp;
        //    }
        //}

        public IBusinessQueryService QueryService
        {
            get
            {
                IBusinessQueryService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IBusinessQueryService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("OperationMessageController IBusinessQueryService", ex);
                    temp = null;
                }
                return temp;
            }
        }

        
        #endregion

        #region 根据邮件MessageID去查找关联信息，如果没有找到，再查找元邮件关联信息(Reference)
        /// <summary>
        /// 根据邮件MessageID去查找关联信息，如果没有找到，再查找元邮件关联信息(Reference)
        /// 从关联中查找
        /// </summary>
        /// <param name="messageId">Mail MessageID</param>
        /// <param name="reference">Mail Reference</param>
        /// <returns></returns>
        public List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId,
                                                                                                string reference)
        {
            List<OperationMessageRelation> relations = null;
            if (string.IsNullOrEmpty(messageId) && string.IsNullOrEmpty(reference))
                return new List<OperationMessageRelation>();

            try
            {
                if (!string.IsNullOrEmpty(messageId))
                {
                    relations = InnerGetOperationMessageRelationByMessageId(messageId);
                    if (relations != null && relations.Count > 0)
                        return relations;
                    else
                    {
                        if (string.IsNullOrEmpty(reference))
                            return relations;

                        relations = InnerGetOperationMessageRelationByMessageId(reference);
                    }
                }
                else
                    relations = InnerGetOperationMessageRelationByMessageId(reference);

            }
            catch (Exception ex)
            {
                relations = new List<OperationMessageRelation>();
                ToolUtility.WriteLog("OperationMessageController GetOperationMessageRelationByMessageIdAndReference", ex);
            }
            return relations;
        }
        
        #endregion

        #region 根据邮件ID，获取关联信息
        /// <summary>
        /// 根据邮件ID，获取关联信息，返回列表对象
        /// </summary>
        /// <param name="messageId">Mail MessageID</param>
        /// <returns>关联信息集合</returns>
        public List<OperationMessageRelation> GetOperationMessageRelationListByMessageID(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                return new List<OperationMessageRelation>();
            List<OperationMessageRelation> listResult = InnerGetOperationMessageRelationByMessageId(messageId);
            if (listResult == null || listResult.Count <= 0)
                return new List<OperationMessageRelation>();
            return listResult;
        }

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回单个对象
        /// </summary>
        /// <param name="messageId">MessageID</param>
        /// <returns>关联信息对象</returns>
        public OperationMessageRelation GetOperationMessageRelationSingleByMessageID(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                return new OperationMessageRelation() { HasData = false };
            List<OperationMessageRelation> listResult = InnerGetOperationMessageRelationByMessageId(messageId);
            if (listResult == null || listResult.Count <= 0)
                return new OperationMessageRelation() { HasData = false };
            if (listResult.Count == 1)
                return listResult[0];
            //获取已上传且最新的一条关联记录
            List<OperationMessageRelation> tempResult = listResult.Where(item => item.UploadServer && item.UpdateDate != null).ToList();
            return tempResult.Count > 0 ? tempResult[0] : new OperationMessageRelation() { HasData = false };
        } 
        #endregion

        #region 业务数据查询
        /// <summary>
        /// 根据关联信息查找业务信息
        /// </summary>
        public DataTable GetOperationListByMessageRelations(BusinessQueryCriteria criteria,
            OperationMessageRelation[] messageRelations)
        {
            criteria.SearchType = SearchActionType.MessageRelation;
            criteria.OperationIDs = (from mr in messageRelations select mr.OperationID).ToArray();
            return GetOperationViewListFixed(criteria);
        }

        /// <summary>
        /// 根据关键字查询业务信息，如果查不到再到服务端查询
        /// </summary>
        public DataTable GetOperationListByKeyWord(BusinessQueryCriteria criteria)
        {
            DataTable dt = null;
            criteria.SearchType = SearchActionType.KeyWord;
            dt = GetOperationListBySubjectInNo(criteria, false);//根据主题单号查找业务集合
            criteria.AdvanceQueryString = criteria.ServerQueryString;
            criteria.OperationType = OperationType.Unknown;
            if (dt == null || dt.Rows.Count == 0)
                dt = QueryService.Get(criteria);
            return dt;
        }

        /// <summary>
        /// 根据主题单号查找业务集合
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="isNeedSearch"></param>
        /// <returns></returns>
        public DataTable GetOperationListBySubjectInNo(BusinessQueryCriteria criteria, bool isNeedSearch)
        {
            DataTable dt = null;
            if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
            {
                criteria.AdvanceQueryString = criteria.AdvanceQueryString.Replace("|  AND", " OR ");
                criteria.AdvanceQueryString = string.Format("{0})", criteria.AdvanceQueryString.Insert(5, "("));
                dt = GetOperationViewListFixed(criteria);
                //dt = DataCacheOperationService.GetOperationViewListFixed(criteria);
            }
            else
            {
                if (isNeedSearch)
                    dt = GetOperationViewListFixed(criteria);
                //dt = DataCacheOperationService.GetOperationViewListFixed(criteria);
                else
                {
                    return null;
                }
            }

            return dt;
        }

        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        /// <param name="criteria">查询信息实体</param>
        public DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria)
        {
            DataTable dtResult = null;
            try
            {
                string strCondition = string.Empty;
                switch (criteria.SearchType)
                {
                    case SearchActionType.SubjectInNO:
                    case SearchActionType.KeyWord:
                        if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
                        {
                            criteria.AdvanceQueryString = criteria.AdvanceQueryString.Replace("|  AND", " OR ");
                            criteria.AdvanceQueryString = string.Format("{0})", criteria.AdvanceQueryString.Insert(5, "("));
                        }
                        strCondition = string.Format(" 1=1 {0}", criteria.AdvanceQueryString);
                        break;
                    case SearchActionType.MessageRelation:
                        strCondition = string.Format(" OceanBookingID IN ({0}) ", ConvertIDAsString(criteria.OperationIDs));
                        break;
                }
                //复制内存表结构
                dtResult = HelpMailStore.TableBusiness.Clone();
                DataRow[] newRow = HelpMailStore.TableBusiness.Select(strCondition);
                for (int index = 0; index < newRow.Length; index++)
                {
                    dtResult.ImportRow(newRow[index]);
                }
                dtResult.AcceptChanges();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("OperationMessageController GetOperationViewListFixed", ex);
                dtResult = null;
            }
            return dtResult;
        }

        /// <summary>
        /// 高级搜索
        /// </summary>
        public DataTable AdvanceSearch(BusinessQueryCriteria criteria, Message.ServiceInterface.Message message)
        {
            //高级搜索直接从服务器上搜索
            if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
            {
                BusinessQueryResult result = BusinessQueryResult.CreateInstance();
                criteria.OperationType = OperationType.Unknown;
                result = QueryService.Get(criteria, message.MessageId, message.UserProperties == null ? string.Empty : message.UserProperties.Reference);
                DataTable dtRelation = null;
                if (UIHelper.MessageRelationOperationList != null)
                {
                    dtRelation = UIHelper.MessageRelationOperationList.Copy();
                }

                result.Dt = MergeDataTable(dtRelation, result.Dt);
                return result.Dt;
            }
            else
                return null;
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取关联数据
        /// </summary>
        /// <param name="messageId">Mail MessageID</param>
        /// <param name="onlyMR">仅从关联信息查找</param>
        /// <returns></returns>
        private List<OperationMessageRelation> InnerGetOperationMessageRelationByMessageId(string messageId)
        {
            return HelpMailStore.TableMessageRelation.Where(item => messageId.Equals(item.MessageId)).ToList();
        }

        /// <summary>
        /// 构建SQL查询条件字符串
        /// </summary>
        /// <param name="ids">OperationIDs</param>
        /// <returns>为空时空Guid</returns>
        private string ConvertIDAsString(Guid[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return Guid.Empty.ToString();
            StringBuilder strBuf = new StringBuilder();
            foreach (var guid in ids)
            {
                strBuf.Append(string.Format("CONVERT('{0}','System.Guid'),", guid));
            }
            return strBuf.ToString(0, strBuf.Length - 1);
        }

        /// <summary>
        /// 合并数据集
        /// </summary>
        /// <param name="oldTable"></param>
        /// <param name="newTable"></param>
        /// <returns></returns>
        private static DataTable MergeDataTable(DataTable oldTable, DataTable newTable)
        {
            if (oldTable == null || oldTable.Rows.Count <= 0)
                return newTable;
            if (newTable != null && newTable.Rows.Count > 0)
            {
                //合并历史关联业务及其查询业务结果时移除历史关联业务DataTable的约束，以便结果合并
                oldTable.Constraints.Clear();
                foreach (DataColumn column in oldTable.Columns)
                {
                    column.AllowDBNull = true;
                }
                oldTable.Merge(newTable);
                oldTable = oldTable.DefaultView.ToTable(true, (from DataColumn col in oldTable.Columns
                    select col.ColumnName).ToArray());
                oldTable = MergeByNO(oldTable);
                return oldTable;
            }

            return oldTable;
        }
        /// <summary>
        /// 合并业务单号相同的行
        /// </summary>
        private static DataTable MergeByNO(DataTable dt)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                bool flag = false;
                foreach (DataRow rown in newdt.Rows)
                {
                    if (rown["NO"].ToString() == row["NO"].ToString())
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    DataRow rowNew = newdt.NewRow();
                    rowNew["NO"] = row["NO"];
                    rowNew["Selected"] = row["Selected"];
                    rowNew["SOCopy"] = row["SOCopy"];
                    rowNew["MBLCopy"] = row["MBLCopy"];
                    rowNew["APCopy"] = row["APCopy"];
                    rowNew["ANCopy"] = row["ANCopy"];
                    rowNew["BLNO"] = row["BLNO"];
                    rowNew["Description"] = row["Description"];
                    rowNew["ContactMail"] = row["ContactMail"];
                    rowNew["OceanBookingID"] = row["OceanBookingID"];
                    rowNew["IsValid"] = row["IsValid"];
                    rowNew["RefNO"] = row["RefNO"];
                    rowNew["UpdateDate"] = row["UpdateDate"];
                    rowNew["OperationType"] = row["OperationType"];
                    newdt.Rows.Add(rowNew);
                }
            }
            return newdt;
        } 
        #endregion

        #region Comment Code关联业务数据
        ///// <summary>
        ///// 数据库视图CODE的名称
        ///// </summary>
        //public string TemplateCode { get { return "MailLink4in1"; } } 

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Ids"></param>
        ///// <returns></returns>
        //private string GetIDsAsString(Guid[] Ids)
        //{
        //    if (Ids == null)
        //        return string.Format("'{0}'", DefaultCompanyId);
        //    StringBuilder strBuf = new StringBuilder();
        //    foreach (var guid in Ids)
        //    {
        //        strBuf.Append(string.Format("CONVERT('{0}','System.Guid'),", guid));
        //    }
        //    return strBuf.ToString(0, strBuf.Length - 1);
        //}

        ///// <summary>
        ///// 根据邮件查询和自动关联
        ///// </summary>
        ///// <param name="currentItem">当前邮件或报文对象</param>
        ///// <param name="associateResult">关联结果</param>
        //public bool AssociateOperationBusiness(object currentItem, ref string associateResult)
        //{
        //    bool resultValue = false;
        //    DataTable dt = null;
        //    BusinessQueryCriteria criteria = null;
        //    OperationSaveController operationSaveCtr = null;
        //    Message.ServiceInterface.Message messageEntity = null;
        //    BusinessQueryResult result = null;
        //    List<OperationMessageRelation> messageRelations = null;
        //    try
        //    {
        //        criteria = new BusinessQueryCriteria();
        //        operationSaveCtr = new OperationSaveController();
        //        criteria.TemplateCode = TemplateCode;

        //        #region 1.当前邮件转换成Message对象

        //        if (currentItem is MailItem) //当前为邮件
        //        {
        //            MailItem currentMail = currentItem as MailItem;
        //            if (currentMail.Sent)
        //            {
        //                messageEntity = OutlookUtility.ConvertMailItemToMessageInfo(currentMail);
        //                messageEntity.IsMailItem = true;
        //            }
        //        }
        //        else if (currentItem is ReportItem) //当前为报文
        //        {
        //            _ReportItem reportItem = currentItem as ReportItem;
        //            messageEntity = OutlookUtility.ConvertReportItemToMessageInfo(reportItem);
        //            messageEntity.IsMailItem = false;
        //        }

        //        #endregion

        //        if (messageEntity == null)
        //        {
        //            associateResult = "Mail convert message entry failed";
        //            return false;
        //        }

        //        if (!operationSaveCtr.IsSaveExternalMail(messageEntity))
        //        {
        //            associateResult = "It contains external mail addresses";
        //            return false;
        //        }

        //        criteria.companyIDs = string.IsNullOrEmpty(GetQueryConditions.SelectedCompanyIds)
        //                ? null
        //                : GetQueryConditions.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
        //        string messageReference = string.Empty;
        //        if (messageEntity.UserProperties != null)
        //        {
        //            messageReference = messageEntity.UserProperties.Reference;
        //        }
        //        messageRelations =
        //            GetBusinessOperationMessageRelationByMessageIdAndReference(messageEntity.MessageId, messageReference);
        //        if (messageRelations != null && messageRelations.Count > 0)
        //            dt = GetOperationListByMessageRelations(criteria, messageRelations.ToArray());
        //        else
        //        {
        //            //关联信息没有找到，再根据主题单号查找
        //            criteria.AdvanceQueryString = GetQueryConditions.AppendAdvanceStringToSQL(messageEntity.Subject);
        //            if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
        //            {
        //                criteria.SearchType = SearchActionType.SubjectInNO;
        //                dt = operationSaveCtr.GetOperationListBySubjectInNO(criteria, messageEntity);
        //            }
        //        }

        //        if (dt == null || dt.Rows.Count <= 0)
        //        {
        //            associateResult = "No business information";
        //            return false;
        //        }

        //        result = new BusinessQueryResult();
        //        result.Relations = messageRelations;
        //        result.Dt = dt;
        //        operationSaveCtr.PostHandle(result, messageEntity, currentItem);
        //        resultValue = OutlookUtility.IsRelationOperation(currentItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        messageEntity = null;
        //        messageRelations = null;
        //        criteria = null;
        //        operationSaveCtr = null;
        //        result = null;
        //        dt = null;
        //    }
        //    return resultValue;
        //}
        #endregion
    }
}

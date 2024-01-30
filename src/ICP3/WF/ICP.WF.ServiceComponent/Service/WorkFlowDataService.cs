using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using System.ServiceModel;

namespace ICP.WF.ServiceComponent
{
    /// <summary>
    /// 工作流业务数据服务
    /// </summary>
    public class WorkFlowDataService : IWorkFlowDataService
    {
        #region 构造函数
        private ISessionService _sessionService;

        public WorkFlowDataService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #endregion

        #region 本地变量

        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        bool IsEnglish
        {
            get
            {
                try
                {
                    return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 当前用户名
        /// </summary>
        Guid CurrentUserID
        {
            get
            {
                try
                {
                    return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId;
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }
        #endregion

        #region 插入费用纪录
        /// <summary>
        /// 插入报销费用纪录
        /// </summary>
        /// <param name="workFlowID">流程ID</param>
        /// <param name="happenDate">费用日期</param>
        /// <param name="deptID">部门ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="bankOrglID">银行帐号/会计科目ID(isPay=1时为银行帐号;isPay=0时为会计科目ID)</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="cashierID">出纳ID</param>
        /// <param name="financeManagerID">财务经理ID<ID/param>
        /// <param name="receiptQty">附据数量</param>
        /// <param name="isPay">是否需要支付</param>
        /// <param name="amount">金额</param>
        /// <param name="feeProperty">报销性质</param>
        /// <param name="no">单号</param>
        /// <param name="costItemIDs">费用集合</param>
        /// <param name="currencys">币种集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="remarks">备注集合</param>
        /// <param name="glID">会计科目ID</param>
        /// <param name="departmentID">部门核算ID</param>
        /// <param name="personalID">个人往来ID</param>
        /// <param name="customerID">客户往来ID</param>    
        /// <returns></returns>
        public Guid SaveFee(Guid workFlowID,
                             DateTime happenDate,
                             Guid deptID,
                             Guid userID,
                             Guid bankOrglID,
                             Guid accountingID,
                             Guid generalManagerID,
                             Guid cashierID,
                             Guid financeManagerID,
                             bool isPay,
                             decimal? receiptQty,
                             decimal amount,
                             short feeProperty,
                             string no,
                             Guid[] costItemIDs,
                             string[] currencys,
                             decimal[] amounts,
                             string[] remarks,
                             Guid? glID,
                             Guid? departmentID,
                             Guid? personalID,
                             Guid? customerID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveCostFee");

                Guid[] detailIDs = new Guid[costItemIDs.Length];

                if (receiptQty == null)
                {
                    receiptQty = 0;
                }

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, Guid.Empty);
                db.AddInParameter(dbCommand, "@WorkFlowID", DbType.Guid, workFlowID);
                db.AddInParameter(dbCommand, "@HappenDate", DbType.DateTime, happenDate.ToUniversalTime());  //转UTC时间
                db.AddInParameter(dbCommand, "@HappenPeriod", DbType.Int32, Convert.ToInt32(happenDate.ToString("yyyyMM")));
                db.AddInParameter(dbCommand, "@DeptID", DbType.Guid, deptID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@AccountId", DbType.Guid, bankOrglID);
                db.AddInParameter(dbCommand, "@AccountingID", DbType.Guid, accountingID);
                db.AddInParameter(dbCommand, "@GeneralManagerID", DbType.Guid, generalManagerID);
                db.AddInParameter(dbCommand, "@CashierID", DbType.Guid, cashierID);
                db.AddInParameter(dbCommand, "@FinanceManagerID", DbType.Guid, financeManagerID);
                db.AddInParameter(dbCommand, "@IsPay", DbType.Boolean, isPay);
                db.AddInParameter(dbCommand, "@ReceiptQty", DbType.Int32, receiptQty);
                db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, amount);
                db.AddInParameter(dbCommand, "@FeeProperty", DbType.Byte, feeProperty);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@DetailIDs", DbType.String, detailIDs.Join());
                db.AddInParameter(dbCommand, "@CostItemIDs", DbType.String, costItemIDs.Join());
                db.AddInParameter(dbCommand, "@Amounts", DbType.String, amounts.Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarks.Join());
                db.AddInParameter(dbCommand, "@Currencys", DbType.String, currencys.Join());
                db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, glID);
                db.AddInParameter(dbCommand, "@DepartmentID", DbType.Guid, departmentID);
                db.AddInParameter(dbCommand, "@PersonalID", DbType.Guid, personalID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);

                return result.ID;
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
        /// 插入报销费用纪录(不需要支付)
        /// </summary>
        /// <param name="workFlowID">流程ID</param>
        /// <param name="happenDate">费用日期</param>
        /// <param name="deptID">部门ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="deptIDs">部门ID集合</param>
        /// <param name="userIDs">用户ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>        
        /// <param name="glIDs">会计科目ID集合(isPay=1时为银行帐号;isPay=0时为会计科目ID)</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="cashierID">出纳ID</param>
        /// <param name="financeManagerID">财务经理ID<ID/param>
        /// <param name="receiptQty">附据数量</param>
        /// <param name="isPay">是否需要支付</param>
        /// <param name="amount">金额</param>
        /// <param name="feeProperty">报销性质</param>
        /// <param name="no">单号</param>
        /// <param name="costItemIDs">费用集合</param>
        /// <param name="currencys">币种集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="remarks">备注集合</param>
        /// <returns></returns>
        public Guid SaveFee_DoNotPay(Guid workFlowID,
                             DateTime happenDate,
                             Guid deptID,
                             Guid userID,
                             Guid[] deptIDs,
                             Guid[] userIDs,
                             Guid[] customerIDs,
                             Guid[] glIDs,
                             Guid accountingID,
                             Guid generalManagerID,
                             Guid cashierID,
                             Guid financeManagerID,
                             bool isPay,
                             decimal? receiptQty,
                             decimal amount,
                             short feeProperty,
                             string no,
                             Guid[] costItemIDs,
                             string[] currencys,
                             decimal[] amounts,
                             string[] remarks)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveCostFee_DoNotPay");

                Guid[] detailIDs = new Guid[costItemIDs.Length];

                if (receiptQty == null)
                {
                    receiptQty = 0;
                }

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, Guid.Empty);
                db.AddInParameter(dbCommand, "@WorkFlowID", DbType.Guid, workFlowID);
                db.AddInParameter(dbCommand, "@HappenDate", DbType.DateTime, happenDate.ToUniversalTime());  //转UTC时间
                db.AddInParameter(dbCommand, "@HappenPeriod", DbType.Int32, Convert.ToInt32(happenDate.ToString("yyyyMM")));
                db.AddInParameter(dbCommand, "@DeptID", DbType.Guid, deptID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@DeptIDs", DbType.String, deptIDs.Join());
                db.AddInParameter(dbCommand, "@UserIDs", DbType.String, userIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, customerIDs.Join());
                db.AddInParameter(dbCommand, "@GLIds", DbType.String, glIDs.Join());
                db.AddInParameter(dbCommand, "@AccountingID", DbType.Guid, accountingID);
                db.AddInParameter(dbCommand, "@GeneralManagerID", DbType.Guid, generalManagerID);
                db.AddInParameter(dbCommand, "@CashierID", DbType.Guid, cashierID);
                db.AddInParameter(dbCommand, "@FinanceManagerID", DbType.Guid, financeManagerID);
                db.AddInParameter(dbCommand, "@IsPay", DbType.Boolean, isPay);
                db.AddInParameter(dbCommand, "@ReceiptQty", DbType.Int32, receiptQty);
                db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, amount);
                db.AddInParameter(dbCommand, "@FeeProperty", DbType.Byte, feeProperty);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@DetailIDs", DbType.String, detailIDs.Join());
                db.AddInParameter(dbCommand, "@CostItemIDs", DbType.String, costItemIDs.Join());
                db.AddInParameter(dbCommand, "@Amounts", DbType.String, amounts.Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarks.Join());
                db.AddInParameter(dbCommand, "@Currencys", DbType.String, currencys.Join());
                db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);

                return result.ID;
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

        #region 删除取消的工作申请费用记录
        /// <summary>
        /// 删除取消的工作申请费用记录
        /// </summary>
        /// <param name="wordFlowID">流程ID</param>
        /// <returns></returns>
        public void DeleteCostFee(Guid wordFlowID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveCostFee");

                db.AddInParameter(dbCommand, "@WorkFlowID", DbType.Guid, wordFlowID);
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
        #endregion

        #region 保存亏损审批流程日志
        /// <summary>
        /// 保存亏损审批流程日志
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="workflowId">流程ID</param>
        /// <param name="createBy">创建人</param>
        public void SaveDeficitOperationWorkFlowLog(Guid? operationId, string operationType, Guid workflowId, Guid createBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveOperationWorkFlowLog");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, Guid.Empty);
                db.AddInParameter(dbCommand, "@OperationId", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@OperationType", DbType.String, operationType);
                db.AddInParameter(dbCommand, "@WorkflowID", DbType.Guid, workflowId);
                db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, createBy);
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
        #endregion

        #region 删除亏损审批流程日志
        /// <summary>
        /// 删除亏损审批流程日志
        /// </summary>
        /// <param name="operationId"></param>
        public void RemoveDeficitOperationWorkFlowLog(Guid operationId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveOperationWorkFlowLog");

                db.AddInParameter(dbCommand, "@OperationId", DbType.Guid, operationId);
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
        #endregion

        #region 根据业务ID获得对应的流程ID
        /// <summary>
        /// 根据业务ID获得对应的流程ID
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public Guid? GetWorkFlowIdByOperationId(Guid operationId)
        {
            return Guid.Empty;
        }
        #endregion

        #region 获得业务务列表信息
        /// <summary>
        /// 获得业务务列表信息
        /// </summary>
        /// <param name="oprationNos">业务单号集合</param>
        /// <param name="companyIds">公司ID集合</param>
        /// <returns></returns>
        public List<OperationSearchResult> GetOperationIdByOperationNo(string[] oprationNos, Guid[] companyIds)
        {
            return new List<OperationSearchResult>();
        }
        #endregion

        #region 根据流程ID得到支票报表打印数据
        /// <summary>
        /// 根据流程ID得到支票报表打印数据
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public CashReportData GetCheckReportData(Guid workflowId)
        {
            ArgumentHelper.AssertGuidNotEmpty(workflowId, "workflowId");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetWorkInfoFeeReportData");

                db.AddInParameter(dbCommand, "@WorkInfoIDs", DbType.Guid, workflowId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count == 0) { return null; }
                CashReportData check = new CashReportData();
                check.BaseReportData = (from b in ds.Tables[0].AsEnumerable()
                                        select new CashBaseReportData
                                   {
                                       Amount = b.Field<string>("Amount"),
                                       CheckNO = b.Field<string>("CheckNO"),
                                       CustomerName = b.Field<string>("CustomerName"),
                                       CustomerEAddress = b.Field<string>("CustomerEAddress"),
                                       CheckDate = b.Field<string>("CheckDate"),
                                       //Total = b.Field<string>("Total")
                                   }).SingleOrDefault();

                check.BillList = (from b in ds.Tables[1].AsEnumerable()
                                  select new CashBillReportData
                                  {
                                      RefNo = b.Field<string>("RefNo"),
                                      WriteOffAmount = b.Field<string>("WriteOffAmount")
                                  }).ToList();
                return check;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 保存流程费用信息_New
        /// <summary>
        /// 保存费用信息
        /// </summary>
        /// <param name="workFlowId">流程ID</param>
        /// <param name="no">单号</param>
        /// <param name="feeDate">费用时间</param>
        /// <param name="departmentId">申请部门</param>
        /// <param name="userId">申请人</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="financeManagerID">财务经理</param>
        /// <param name="cashierID">出纳</param>
        /// <param name="receiptQty">单据数</param>
        /// <param name="feeProperty">费用类型</param>
        /// <param name="glIds">科目ID</param>
        /// <param name="currencyCodes">币种ID</param>
        /// <param name="dramounts">借方金额</param>
        /// <param name="cramounts">贷方金额</param>
        /// <param name="remarks">摘要</param>
        /// <param name="customerIDs">客户ID</param>
        /// <param name="depIDs">部门ID</param>
        /// <param name="userIDs">个人ID</param>
        public Guid SaveCostFeeNew(
            Guid workFlowId,
            string no,
            DateTime feeDate,
            Guid departmentId,
            Guid userId,
            Guid movieProjectID,
            Guid accountingID,
            Guid generalManagerID,
            Guid financeManagerID,
            Guid cashierID,
            string receiptQty,
            short feeProperty,
            Guid[] glIds,
            string[] currencyCodes,
            decimal[] dramounts,
            decimal[] cramounts,
            string[] remarks,
            string[] customerIDs,
            string[] depIDs,
            string[] userIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveCostFeeNew");

                Guid[] detailIDs = new Guid[glIds.Length];
                int qty = 0;
                try
                {
                    qty = Convert.ToInt32(receiptQty);
                }
                catch
                {
                    qty = 0;
                }

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, Guid.Empty);
                db.AddInParameter(dbCommand, "@WorkFlowID", DbType.Guid, workFlowId);
                db.AddInParameter(dbCommand, "@HappenDate", DbType.DateTime, feeDate.ToUniversalTime());  //转UTC时间
                db.AddInParameter(dbCommand, "@HappenPeriod", DbType.Int32, Convert.ToInt32(feeDate.ToUniversalTime().ToString("yyyyMM")));
                db.AddInParameter(dbCommand, "@DeptID", DbType.Guid, departmentId);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userId);
                db.AddInParameter(dbCommand, "@MovieProjectID", DbType.Guid, movieProjectID);                
                db.AddInParameter(dbCommand, "@AccountingID", DbType.Guid, accountingID);
                db.AddInParameter(dbCommand, "@GeneralManagerID", DbType.Guid, generalManagerID);
                db.AddInParameter(dbCommand, "@CashierID", DbType.Guid, cashierID);
                db.AddInParameter(dbCommand, "@FinanceManagerID", DbType.Guid, financeManagerID);
                db.AddInParameter(dbCommand, "@ReceiptQty", DbType.Int32, qty);
                db.AddInParameter(dbCommand, "@FeeProperty", DbType.Byte, feeProperty);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@DetailIDs", DbType.String, detailIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIds.Join());
                db.AddInParameter(dbCommand, "@DRAmt", DbType.String, dramounts.Join());
                db.AddInParameter(dbCommand, "@CRAmt", DbType.String, cramounts.Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarks.Join());
                db.AddInParameter(dbCommand, "@Currencys", DbType.String, currencyCodes.Join());
                db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, userId);
                db.AddInParameter(dbCommand, "@DepartmentIDs", DbType.String, depIDs.Join());
                db.AddInParameter(dbCommand, "@PersonalIDs", DbType.String,userIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, customerIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);

                return result.ID;
            }
            catch (SqlException sqlException)
            {
                LogHelper.SaveLog("||" + no + ":" + sqlException.Message);
                throw sqlException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 复制流程
        /// <summary>
        /// 复制流程信息
        /// </summary>
        /// <param name="workFlowID"></param>
        /// <param name="departmentID"></param>
        /// <param name="userID"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public SingleResult CopyWorkFlowData(Guid workFlowID,
                                 Guid departmentID,
                                 Guid userID,
                                 DateTime startDate)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspCopyWorkFlowData");

                db.AddInParameter(dbCommand, "@WorkFlowID", DbType.Guid, workFlowID);
                db.AddInParameter(dbCommand, "@DepartmentID", DbType.Guid, departmentID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, startDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "No" });

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

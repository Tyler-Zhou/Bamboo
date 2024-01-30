using System;
using System.Linq;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.FAM.ServiceInterface;
using System.Collections.Generic;
using System.Transactions;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.WF.ServiceComponent
{
    /// <summary>
    /// 与业务交互服务
    /// </summary>
    public class BusinessDataExchangeService : IBusinessDataExchangeService
    {

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

        #region 构造函数


        private ISessionService _sessionService;
        private IUserService _userService;
        private IWorkFlowDataService _workFlowDataService;
        private IFinanceService _finService;

        public BusinessDataExchangeService(
             ISessionService sessionService,
             IUserService userService,
             IWorkFlowDataService workFlowDataService,
             IFinanceService finservice)
        {
            _sessionService = sessionService;
            _workFlowDataService = workFlowDataService;
            _userService = userService;
            _finService = finservice;
        }


        #endregion

        #region 与ICP交互接口

        #region 请假流程业务交互接口
        /// <summary>
        /// 请假流程
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="days">开数</param>
        /// <param name="type">请假类型</param>
        /// <param name="reason">请假原因</param>
        public void HolidayEffective(Guid userId,
            Guid workflowInstanceId,
            DateTime fromDate,
            DateTime toDate,
            decimal days,
            string type,
            string reason)
        {
            // _stafferInfoService.HolidayEffective(userId, workflowInstanceId, fromDate, toDate, days, type, reason, CurrentUserId);
        }
        #endregion

        #region 职位调动
        /// <summary>
        /// 职位调动
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="outDepartmentId">调出部门</param>
        /// <param name="inDepartmentId">调入部门</param>
        /// <param name="outPostId">调出岗位</param>
        /// <param name="inPostId">调入岗位</param>
        public void RedeployEffective(Guid userId,
             Guid workflowInstanceId,
             Guid outDepartmentId,
             Guid inDepartmentId,
             Guid outPostId,
             Guid inPostId)
        {
            //_stafferInfoService.RedeployEffective(userId, workflowInstanceId, CurrentUserId, outDepartmentId, inDepartmentId, outPostId, inPostId);
        }
        #endregion

        #region 员工入职

        /// <summary>
        /// 入职
        /// </summary>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="workName">工作名</param>
        /// <param name="flowName">流程名</param>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="birthday">生日</param>
        /// <param name="nativePlace">籍贯</param>
        /// <param name="culture">文化</param>
        /// <param name="specialty">专业</param>
        /// <param name="identityCard">身份证</param>
        /// <param name="familyAddress">家庭地址</param>
        /// <param name="emolumentDate">开始计薪日期</param>
        /// <param name="departmentId">部门</param>
        /// <param name="postId">岗位</param>
        /// <param name="manageRemark">备注</param>
        /// <param name="approveBy">审核人</param>
        /// <param name="mainManageRemark">审批备注</param>
        /// <param name="hrRemark">HR备注</param>
        /// <param name="email">邮箱</param>
        /// <param name="ename">英文名</param>
        /// <param name="phone">手机</param>
        /// <param name="stafferId">员工ID</param>
        /// <param name="tel">电话</param>
        public void EmployeeEffective(
           Guid workflowInstanceId,
           string workName,
           string flowName,
           Guid stafferId,
           string name,
           string ename,
           string sex,
           DateTime birthday,
           string tel,
           string phone,
           string email,
           string nativePlace,
           string culture,
           string specialty,
           string identityCard,
           string familyAddress,
           DateTime emolumentDate,
           Guid departmentId,
           Guid postId,
           string manageRemark,
           Guid approveBy,
           string mainManageRemark,
           string hrRemark)
        {
            try
            {
                _userService.SaveUserInfoForWF(
                    name,
                    ename,
                    tel,
                    phone,
                    email,
                    sex,
                    departmentId,
                    postId,
                    birthday,
                    nativePlace,
                    familyAddress,
                    culture,
                    specialty, identityCard,
                    emolumentDate,
                    approveBy);
            }
            catch { }
        }
        #endregion

        #region 辞退流程业务接口
        /// <summary>
        /// 辞退流程
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="postId">岗位ID</param>
        public void DisemployEffective(Guid userId,
            Guid workflowInstanceId,
            Guid departmentId,
            Guid postId)
        {
            try
            {
                UserInfo userInfo = _userService.GetUserInfo(userId);
                if (userInfo.IsValid)
                {
                    _userService.ChangeUserState(userId, false, userId, userInfo.UpdateDate);
                }
            }
            catch { }
        }
        #endregion

        #region 辞职流程业务接口
        /// <summary>
        /// 辞职流程
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="postId">岗位ID</param>
        public void DemissionEffective(Guid userId,
            Guid workflowInstanceId,
            Guid departmentId,
            Guid postId)
        {
            try
            {
                UserInfo userInfo = _userService.GetUserInfo(userId);
                if (userInfo.IsValid)
                {

                    _userService.ChangeUserState(userId, false, userId, userInfo.UpdateDate);
                }
            }
            catch
            {

            }
        }

        #endregion

        #region 业务报销流程日志接口
        /// <summary>
        /// 保存业务报销日志
        /// </summary>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="customerId">客户ID</param>
        /// <param name="workName">工作名称</param>
        /// <param name="workFlowName">流程名称</param>
        /// <param name="departmentId">部门</param>
        /// <param name="applyer">申请人</param>
        /// <param name="applyTime">申请时间</param>
        /// <param name="amounts">金额集合</param>
        public void SaveCustomerExpense(
            Guid workflowInstanceId,
            Guid customerId,
            Guid louchLogId,
            string workName,
            string workFlowNo,
            Guid departmentId,
            Guid applyer,
            DateTime applyTime,
           decimal[] amounts)
        {
            try
            {
                decimal amount = 0;
                if (amounts != null && amounts.Length > 0)
                {
                    amount = amounts.Sum();
                }

                //if (louchLogId == null || louchLogId==Guid.Empty)
                //{
                //    string message = IsEnglish ? "Please select the louch data" : "请选择客户跟进纪录";
                //    throw new ApplicationException(message);
                //}


                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveCRMCustomerExpenseLog");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@WorkFlowId", DbType.Guid, workflowInstanceId);
                db.AddInParameter(dbCommand, "@CustomerTouchID", DbType.Guid, louchLogId);

                db.AddInParameter(dbCommand, "@Workname", DbType.String, workName);
                db.AddInParameter(dbCommand, "@Flowname", DbType.String, workFlowNo);
                db.AddInParameter(dbCommand, "@Money", DbType.Decimal, amount);
                db.AddInParameter(dbCommand, "@Status", DbType.String, "Complete");
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@DepartMentId", DbType.Guid, departmentId);
                db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, applyer);

                db.ExecuteDataSet(dbCommand);

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

        #region 保存退佣信息
        /// <summary>
        /// 保存退佣日志
        /// </summary>
        /// <param name="workflowInstanceId"></param>
        /// <param name="customerId"></param>
        /// <param name="workName"></param>
        /// <param name="workflowNo"></param>
        /// <param name="blNo"></param>
        /// <param name="departmentId"></param>
        /// <param name="applyer"></param>
        /// <param name="applyDate"></param>
        public void SaveCustomerCommision(Guid workflowInstanceId,
            Guid customerId,
            string operactionIDs,
            string workName,
            string workflowNo,
            string blNo,
            Guid departmentId,
            Guid applyer,
            DateTime applyDate)
        {
            try
            {
                if (string.IsNullOrEmpty(operactionIDs))
                {
                    string message = IsEnglish ? "Please select the commision data" : "请选择退佣的业务纪录";
                    throw new ApplicationException(message);
                }


                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveCRMCustomerCommissionLog");


                operactionIDs = operactionIDs.Replace(",", ICP.Framework.CommonLibrary.Common.GlobalConstants.DividedSymbol);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@WorkFlowId", DbType.Guid, workflowInstanceId);
                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operactionIDs);
                db.AddInParameter(dbCommand, "@Workname", DbType.String, workName);
                db.AddInParameter(dbCommand, "@Flowname", DbType.String, workflowNo);
                db.AddInParameter(dbCommand, "@OrderNum", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@Status", DbType.String, "Complete");
                db.AddInParameter(dbCommand, "@AimPort", DbType.String, "");
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@DepartMentId", DbType.Guid, departmentId);
                db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, applyer);

                db.ExecuteDataSet(dbCommand);

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

        #region 保存费用多条明细
        /// <summary>
        /// 保存费用
        /// </summary>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="no">单号</param>
        /// <param name="feeDate">费用日期</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="bankID">银行ID</param>
        /// <param name="feeProperty">报销性质</param>
        /// <param name="costItemIds">费用项目ID集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="currencys">币种集合</param>
        /// <param name="remarks">备注</param>
        public void SaveCostFee(Guid workflowInstanceId,
            string no,
            DateTime feeDate,
            Guid departmentId,
            Guid userId,
            Guid bankID,
            Guid accountingID,
            Guid generalManagerID,
            Guid financeManagerID,
            Guid cashierID,
            string receiptQty,
            string feeProperty,
            Guid[] costItemIds,
            decimal[] amounts,
            string[] currencys,
            string[] remarks)
        {
            decimal ttlAmt = amounts.Sum();
            short property = 0;
            if (feeProperty.Contains("Company"))
            {
                property = 1;
            }
            decimal ReceiptQty = 0;
            try
            {
                ReceiptQty = decimal.Parse(receiptQty);
            }
            catch
            {

            }
            _workFlowDataService.SaveFee(workflowInstanceId, feeDate, departmentId, userId, bankID, accountingID, generalManagerID, cashierID, financeManagerID, true, ReceiptQty, ttlAmt, property, no, costItemIds, currencys, amounts, remarks, null, null, null, null);
        }
        #endregion

        #region 保存费用-不需要支付

        /// <summary>
        /// 保存费用(不需要支付)
        /// </summary>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="no">单号</param>
        /// <param name="feeDate">费用日期</param>
        /// <param name="departmentId">申请部门ID</param>
        /// <param name="userId">申请用户ID</param>
        /// <param name="deptIDs">部门ID集合</param>
        /// <param name="userIDs">用户ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>        
        /// <param name="glIDs">会计科目ID集合(isPay=1时为银行帐号;isPay=0时为会计科目ID)</param>
        /// <param name="feeProperty">报销性质</param>
        /// <param name="costItemIds">费用项目ID集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="currencys">币种集合</param>
        /// <param name="remarks">备注</param>
        /// <param name="departmentID">部门ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="customerID">客户ID</param>
        public void SaveCostFee_DoNotPay(Guid workflowInstanceId,
            string no,
            DateTime feeDate,
            Guid departmentId,
            Guid userId,
            Guid[] deptIDs,
            Guid[] userIDs,
            Guid[] customerIDs,
            Guid[] glIDs,
            Guid accountingID,
            Guid generalManagerID,
            Guid financeManagerID,
            Guid cashierID,
            string receiptQty,
            string feeProperty,
            Guid[] costItemIds,
            decimal[] amounts,
            string[] currencys,
            string[] remarks)
        {
            decimal ttlAmt = amounts.Sum();
            short property = 0;
            if (feeProperty.Contains("Company"))
            {
                property = 1;
            }
            decimal ReceiptQty = 0;
            try
            {
                ReceiptQty = decimal.Parse(receiptQty);
            }
            catch
            {

            }
            _workFlowDataService.SaveFee_DoNotPay(workflowInstanceId, feeDate, departmentId, userId, deptIDs, userIDs, customerIDs, glIDs, accountingID, generalManagerID, cashierID, financeManagerID, false, ReceiptQty, ttlAmt, property, no, costItemIds, currencys, amounts, remarks);
        }

        #endregion

        #region 保存单条费用
        /// <summary>
        /// 保存单条费用
        /// </summary>
        /// <param name="workFlowId">流程ID</param>
        /// <param name="no">流程单号</param>
        /// <param name="feeDate">费用日期</param>
        /// <param name="departmentId">部门</param>
        /// <param name="userId">申请人</param>
        /// <param name="bankID">银行帐号ID</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="financeManagerID">财务经理ID</param>
        /// <param name="cashierID">出纳ID</param>
        /// <param name="receiptQty">单据数</param>
        /// <param name="glID">会计科目</param>
        /// <param name="amount">金额</param>
        /// <param name="currencyID">币种</param>
        /// <param name="remark">备注</param>
        public void SaveSingleCostFee(
         Guid workFlowId,
         string no,
         DateTime feeDate,
         Guid departmentId,
         Guid userId,
         Guid bankID,
         Guid accountingID,
         Guid generalManagerID,
         Guid cashierID,
         Guid financeManagerID,
         string receiptQty,
         Guid glID,
         decimal amount,
         Guid currencyID,
         string remark)
        {
            short property = 0;
            decimal ReceiptQty = 0;
            string memo = string.Empty;
            try
            {
                if (receiptQty.EndsWith("."))
                {
                    receiptQty.Replace(".", "");
                }
                ReceiptQty = decimal.Parse(receiptQty);
            }
            catch
            {

            }
            _workFlowDataService.SaveFee(workFlowId, feeDate, departmentId, userId, bankID, accountingID, generalManagerID, cashierID, financeManagerID, true, ReceiptQty, amount, property, no, new Guid[1] { glID }, new string[1] { currencyID.ToString() }, new decimal[1] { amount }, new string[1] { remark }, glID, null, null, null);
        }

        /// <summary>
        /// 保存单条费用
        /// </summary>
        /// <param name="workFlowId">流程ID</param>
        /// <param name="no">流程单号</param>
        /// <param name="feeDate">费用日期</param>
        /// <param name="departmentId">部门</param>
        /// <param name="userId">申请人</param>
        /// <param name="drGLID">借方会计科目ID</param>
        /// <param name="accountingID">会计ID</param>
        /// <param name="generalManagerID">总经理ID</param>
        /// <param name="financeManagerID">财务经理ID</param>
        /// <param name="cashierID">出纳ID</param>
        /// <param name="receiptQty">单据数</param>
        /// <param name="crGLID">贷方会计科目ID</param>
        /// <param name="amount">金额</param>
        /// <param name="currencyID">币种</param>
        /// <param name="remark">备注</param>
        /// <param name="departmentID">部门核算ID</param>
        /// <param name="personalID">个人往来ID</param>
        /// <param name="customerID">客户往来ID</param>  
        public void SaveUniqueCostFee(
         Guid workFlowId,
         string no,
         DateTime feeDate,
         Guid deptID,
         Guid userID,
         Guid drGLID,
         Guid accountingID,
         Guid generalManagerID,
         Guid cashierID,
         Guid financeManagerID,
         string receiptQty,
         Guid crGLID,
         decimal amount,
         Guid currencyID,
         string remark,
         Guid departmentID,
         Guid personalID,
         Guid customerID)
        {
            short property = 0;
            decimal ReceiptQty = 0;
            try
            {
                if (receiptQty.EndsWith("."))
                {
                    receiptQty.Replace(".", "");
                }
                ReceiptQty = decimal.Parse(receiptQty);
            }
            catch
            {

            }
            //_workFlowDataService.SaveFee(workFlowId, feeDate, deptID, userID, bankID, accountingID, generalManagerID, cashierID, financeManagerID, true, ReceiptQty, amount, property, no, new Guid[1] { glID }, new string[1] { currencyID.ToString() }, new decimal[1] { amount }, new string[1] { remark }, glID, departmentID, personalID, customerID);

            _workFlowDataService.SaveFee_DoNotPay(
                workFlowId,
                feeDate,
                deptID,
                userID,
                new Guid[1] { departmentID },
                new Guid[1] { personalID },
                new Guid[1] { customerID },
                new Guid[1] { crGLID },
                accountingID,
                generalManagerID,
                cashierID,
                financeManagerID,
                false,
                ReceiptQty,
                amount,
                1,
                no,
                new Guid[1] { drGLID },
                new string[1] { currencyID.ToString() },
                new decimal[1] { amount },
                new string[1] { remark });
        }

        #endregion

        #region 保存运输费用 
        /// <summary>
        /// 保存运输费用
        /// </summary>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="no">单号</param>
        /// <param name="feeDate">费用日期</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="costItemIds">费用项目ID集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="currencys">币种集合</param>
        /// <param name="remarks">备注集合</param>
        public void SaveTransportationCostFee(Guid workflowInstanceId,
         string no,
         DateTime feeDate,
         Guid departmentId,
         Guid userId,
         Guid[] costItemIds,
         decimal[] amounts,
         string[] currencys,
         string[] remarks)
        {
            //decimal ttlAmt = amounts.Sum();
            //_workFlowDataService.SaveFee(Guid.Empty, workflowInstanceId, feeDate, departmentId, userId, ttlAmt, 1, no, costItemIds, currencys, amounts, remarks);
        }
        #endregion

        #region 删除费用信息
        /// <summary>
        /// 删除费用
        /// </summary>
        /// <param name="workflowInstanceId">流程ID</param>
        public void DeleteCostFee(Guid workflowInstanceId)
        {
            _workFlowDataService.DeleteCostFee(workflowInstanceId);
        }
        #endregion

        #region 保存亏损操作日志
        /// <summary>
        /// 保存亏损操作日志
        /// </summary>
        /// <param name="workflowInstanceId">流程ID</param>
        /// <param name="operationType">精力类型</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="createBy">创建人</param>
        public void SaveDeficitOperationWorkFlowLog(Guid workflowInstanceId,
        string operationType,
        Guid? operationId,
        Guid createBy)
        {
            if (operationId == null) return;

            _workFlowDataService.SaveDeficitOperationWorkFlowLog(operationId, operationType, workflowInstanceId, createBy);
        }
        #endregion

        #region 保存月结协议流程
        /// <summary>
        /// 保存月结协议流程
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="companyID"></param>
        /// <param name="beginDate"></param>
        /// <param name="enddate"></param>
        /// <param name="createBy"></param>
        /// <param name="financeBy"></param>
        /// <param name="workNo"></param>
        /// <param name="CreditAmount">信用额度</param>
        /// <param name="PayType">付款类型</param>
        /// <param name="CridetTerm">信用期限</param>
        /// <param name="PaymentDate">付款日</param>
        public void SaveMonthlyClosingEntries(
            Guid customerID,
            Guid companyID,
            DateTime beginDate,
            DateTime enddate,
            Guid createBy,
            Guid financeBy,
            string workNo,
            decimal CreditAmount,
            decimal PayType,
            decimal CridetTerm,
            decimal PaymentDate
            )
        {
            MonthlyClosingEntrySaveRequest saveRequest = new MonthlyClosingEntrySaveRequest();

            saveRequest.Id = Guid.NewGuid();
            saveRequest.CustomerId = customerID;
            saveRequest.CreateById = createBy;
            saveRequest.CustomerTypes = "BillCustomer;Shipper";

            saveRequest.ApplyTimes = new List<DateTime?>();
            saveRequest.monthlyCompanyIDs = new List<Guid>();
            saveRequest.ValidDates = new List<DateTime?>();
            saveRequest.IsValids = new List<bool?>();
            saveRequest.Remarks = new List<string>();
            saveRequest.ApplyByIDs = new List<Guid?>();
            saveRequest.UserIDs = new List<Guid?>();
            saveRequest.CreditDate = new List<int>();
            saveRequest.CreditAmount = new List<decimal?>();
            saveRequest.Estimatedvalue = new List<decimal?>();
            saveRequest.CalculateTermTypes = new List<CalculateTermType>();
            saveRequest.PaymentDates = new List<int>();

            if (financeBy == Guid.Empty || financeBy == null)
            {
                financeBy = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
            }
            saveRequest.ApplyTimes.Add(beginDate);
            saveRequest.monthlyCompanyIDs.Add(companyID);
            saveRequest.ValidDates.Add(enddate);
            saveRequest.IsValids.Add(true);
            saveRequest.Remarks.Add("流程单号:" + workNo);
            saveRequest.ApplyByIDs.Add(createBy);
            saveRequest.UserIDs.Add(financeBy);
            saveRequest.Estimatedvalue.Add(0);

            saveRequest.CreditDate.Add((int)Math.Round(CridetTerm));
            saveRequest.PaymentDates.Add((int)Math.Round(PaymentDate));
            saveRequest.CreditAmount.Add(CreditAmount);
            saveRequest.CalculateTermTypes.Add((CalculateTermType)(Math.Round(PayType) + 1));

            _finService.SaveMonthlyClosingEntryInfo(saveRequest);
        }
        #endregion

        #region 保存费用信息
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
        public void SaveCostFeeNew(
            Guid workFlowId,
            string no,
            DateTime feeDate,
            Guid departmentId,
            Guid userId,
            Guid accountingID,
            Guid generalManagerID,
            Guid financeManagerID,
            Guid cashierID,
            string receiptQty,
            string feeProperty,
            Guid[] glIds,
            string[] currencyCodes,
            decimal[] dramounts,
            decimal[] cramounts,
            string[] remarks,
            string[] customerIDs,
            string[] depIDs,
            string[] userIDs)
        {

            short property = 0;
            if (feeProperty.Contains("Company"))
            {
                property = 1;
            }
            decimal ReceiptQty = 0;
            try
            {
                ReceiptQty = decimal.Parse(receiptQty);
            }
            catch
            {

            }

            _workFlowDataService.SaveCostFeeNew(
                workFlowId,
                no, feeDate,
                departmentId,
                userId,
                Guid.Empty,
                accountingID,
                generalManagerID,
                financeManagerID,
                cashierID,
                receiptQty,
                property,
                glIds,
                currencyCodes,
                dramounts,
                cramounts,
                remarks,
                customerIDs,
                depIDs,
                userIDs);
        }

        public void SaveMovieCostCostFee(
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
                    string feeProperty,
                    Guid[] glIds,
                    string[] currencyCodes,
                    decimal[] dramounts,
                    decimal[] cramounts,
                    string[] remarks,
                    string[] customerIDs,
                    string[] depIDs,
                    string[] userIDs)
        {
            short property = 0;
            if (feeProperty.Contains("Company"))
            {
                property = 1;
            }
            decimal ReceiptQty = 0;
            try
            {
                ReceiptQty = decimal.Parse(receiptQty);
            }
            catch
            {

            }
            _workFlowDataService.SaveCostFeeNew(
               workFlowId,
               no, feeDate,
               departmentId,
               userId,
               movieProjectID,
               accountingID,
               generalManagerID,
               financeManagerID,
               cashierID,
               receiptQty,
               property,
               glIds,
               currencyCodes,
               dramounts,
               cramounts,
               remarks,
               customerIDs,
               depIDs,
               userIDs);
        }
        #endregion

        #region 保存资金调拨流程
        public void SaveAllocationWorkFLow(
                    Guid workFlowId,
                    string no,
                    DateTime feeDate,
                    Guid inDepartmentId,
                    Guid inUserId,
                    Guid[] inGlIds,
                    string[] inCurrencyCodes,
                    decimal[] inDramounts,
                    decimal[] inCramounts,
                    string[] inRemarks,
                    string[] inCustomerIDs,
                    Guid outDepartmentId,
                    Guid outUserId,
                    Guid[] outGlIds,
                    string[] outCurrencyCodes,
                    decimal[] outDramounts,
                    decimal[] outCramounts,
                    string[] outRemarks,
                    string[] outCustomerIDs)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                #region 先保存当前流程的CostFee信息
                _workFlowDataService.SaveCostFeeNew(workFlowId,
                    no,
                    feeDate,
                    inDepartmentId,
                    inUserId,
                    Guid.Empty,
                    Guid.Empty,
                    Guid.Empty,
                    Guid.Empty,
                    Guid.Empty,
                    "0",
                    1,
                    inGlIds,
                    inCurrencyCodes,
                    inDramounts,
                    inCramounts,
                    inRemarks,
                    inCustomerIDs,
                    new string[inCustomerIDs.Length],
                    new string[inCustomerIDs.Length]);
                #endregion

                #region 保存新的流程的CostFee信息
                // 建造新的WorkInfo跟WorkItem
                SingleResult result = _workFlowDataService.CopyWorkFlowData(workFlowId, outDepartmentId, outUserId, feeDate);
                //保存新的流程的CostFee信息
                _workFlowDataService.SaveCostFeeNew(
                    result.GetValue<Guid>("ID"),
                    result.GetValue<String>("No"),
                   feeDate,
                   outDepartmentId,
                   outUserId,
                   Guid.Empty,
                   Guid.Empty,
                   Guid.Empty,
                   Guid.Empty,
                   Guid.Empty,
                   "0",
                   1,
                   outGlIds,
                   outCurrencyCodes,
                   outDramounts,
                   outCramounts,
                   outRemarks,
                   outCustomerIDs,
                   new string[outCustomerIDs.Length],
                   new string[outCustomerIDs.Length]);
                #endregion

                scope.Complete();
            }

        }
        #endregion

        #endregion
    }
}

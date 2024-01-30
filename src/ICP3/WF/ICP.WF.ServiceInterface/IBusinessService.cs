
namespace ICP.WF.ServiceInterface
{
    using System;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.WF.ServiceInterface.DataObject;
    using System.ServiceModel;

    [ServiceInfomation("工作流与业务通信的接口", ServiceType.Business)]
    [ServiceContract]
    public interface IBusinessDataExchangeService
    {


        [OperationContract]
        [ExternalMethod("记录请假日志", "记录请假日志")]
        void HolidayEffective([ExternalMetodParameter("申请人ID", "申请人ID")]Guid userId,
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("开始时间", "开始时间")]DateTime fromDate,
            [ExternalMetodParameter("结束时间", "结束时间")]DateTime toDate,
            [ExternalMetodParameter("请假天数", "请假天数")]decimal days,
            [ExternalMetodParameter("请假类型", "请假类型(0.年假 1.事假 2.病假 3.婚假  4.产假  5.陪产假 6.年假 7.丧假 8.其它假 )")]string type,
            [ExternalMetodParameter("请假原因", "请假原因")]string reason);


        [OperationContract]
        [ExternalMethod("记录调动日志", "记录调动日志")]
        void RedeployEffective([ExternalMetodParameter("员工ID", "员工ID")]Guid userId,
             [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
             [ExternalMetodParameter("调出部门Id", "调出部门Id")]Guid outDepartmentId,
             [ExternalMetodParameter("调入部门Id", "调入部门Id")]Guid inDepartmentId,
             [ExternalMetodParameter("调出职位Id", "调入职位Id")]Guid outPostId,
             [ExternalMetodParameter("调入职位Id", "调入职位Id")]Guid inPostId);


        [OperationContract]
        [ExternalMethod("记录入职日志", "记录入职日志")]
        void EmployeeEffective([ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("工作名", "工作名")]string workName,
            [ExternalMetodParameter("流程名", "流程名")]string flowName,
            [ExternalMetodParameter("员工Id", "员工Id")] Guid stafferId,
            [ExternalMetodParameter("姓名", "姓名")]string name,
            [ExternalMetodParameter("英文名", "英文名")]string ename,
            [ExternalMetodParameter("性别", "性别")]string sex,
            [ExternalMetodParameter("出生日期", "出生日期")]DateTime birthday,
            [ExternalMetodParameter("电话", "电话")]string tel,
            [ExternalMetodParameter("手机", "手机")]string phone,
            [ExternalMetodParameter("邮箱", "邮箱")]string email,
            [ExternalMetodParameter("户籍", "户籍")]string nativePlace,
            [ExternalMetodParameter("文化程度", "文化程度")]string culture,
            [ExternalMetodParameter("专业", "专业")]string specialty,
            [ExternalMetodParameter("身份证号", "身份证号")]string identityCard,
            [ExternalMetodParameter("家庭住址", "家庭住址")]string familyAddress,
            [ExternalMetodParameter("入职日期", "入职日期")]DateTime emolumentDate,
            [ExternalMetodParameter("部门Id", "部门Id")]Guid department,
            [ExternalMetodParameter("职位Id", "职位Id")]Guid post,
            [ExternalMetodParameter("经理批注", "经理批注")]string manageRemark,
            [ExternalMetodParameter("经理Id", "经理Id")]Guid approveBy,
            [ExternalMetodParameter("总经理批注", "总经理批注")]string mainManageRemark,
            [ExternalMetodParameter("HR批注", "HR批注")]string hrRemark);


        [OperationContract]
        [ExternalMethod("记录辞退日志", "记录辞退日志")]
        void DisemployEffective([ExternalMetodParameter("员工Id", "员工Id")]Guid userId,
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("员工部门Id", "员工部门Id")]Guid departmentId,
            [ExternalMetodParameter("员工职位Id", "员工职位Id")]Guid postId);


        [OperationContract]
        [ExternalMethod("记录辞职日志", "记录辞职日志")]
        void DemissionEffective([ExternalMetodParameter("员工Id", "员工Id")]Guid userId,
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("员工部门Id", "员工部门Id")]Guid departmentId,
            [ExternalMetodParameter("员工职位Id", "员工职位Id")]Guid postId);


        [OperationContract]
        [ExternalMethod("保存业务报销日志", "保存业务报销日志")]
        void SaveCustomerExpense(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("客户Id", "客户Id")]Guid customerId,
            [ExternalMetodParameter("跟进纪录Id", "跟进纪录Id")]Guid louchLogId,
            [ExternalMetodParameter("工作名", "工作名")]string workName,
            [ExternalMetodParameter("工作单号", "工作单号")]string workFlowNo,
            [ExternalMetodParameter("部门Id", "部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid applyer,
            [ExternalMetodParameter("审批时间", "审批时间")]DateTime applyTime,
            [ExternalMetodParameter("金额", "金额")]decimal[] amounts
            );


        [OperationContract]
        [ExternalMethod("保存退佣日志", "保存退佣日志")]
        void SaveCustomerCommision(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("客户Id", "客户Id")]Guid customerId,
            [ExternalMetodParameter("业务ID", "业务ID")]string operactionIDs,
            [ExternalMetodParameter("工作名", "工作名")]string workName,
            [ExternalMetodParameter("工作单号", "工作单号")]string workflowNo,
            [ExternalMetodParameter("提单号", "提单号")]string blNo,
            [ExternalMetodParameter("申请部门Id", "申请部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid applyer,
            [ExternalMetodParameter("申请时间", "申请时间")]DateTime applyDate);


        [OperationContract]
        [ExternalMethod("保存费用记录", "保存费用记录")]
        void SaveCostFee(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("申请部门Id", "申请部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid userId,
            [ExternalMetodParameter("银行帐号", "银行帐号Id")]Guid bankID,
            [ExternalMetodParameter("会计ID", "会计ID")]Guid accountingID,
            [ExternalMetodParameter("总经理ID", "总经理ID")]Guid generalManagerID,
            [ExternalMetodParameter("财务经理ID", "财务经理ID")]Guid financeManagerID,            
            [ExternalMetodParameter("出纳ID", "出纳ID")]Guid cashierID,            
            [ExternalMetodParameter("单据数量", "单据数量")]string receiptQty,
            [ExternalMetodParameter("报销类型", "报销类型")]string feeProperty,
            [ExternalMetodParameter("费用项目Id", "费用项目Id")]Guid[] costItemIds,
            [ExternalMetodParameter("费用金额", "费用金额")]decimal[] amounts,
            [ExternalMetodParameter("费用币种", "费用币种")]string[] currencys,
            [ExternalMetodParameter("费用批注", "费用批注")]string[] remarks);

        [OperationContract]
        [ExternalMethod("保存费用记录_不支付", "保存费用记录_不支付")]
        void SaveCostFee_DoNotPay(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("申请部门Id", "申请部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid userId,
            [ExternalMetodParameter("部门核算Id", "部门核算Id")]Guid[] deptIDs,
            [ExternalMetodParameter("个人往来Id", "个人往来Id")]Guid[] userIDs,
            [ExternalMetodParameter("客户往来Id", "客户往来Id")]Guid[] customerIDs,
            [ExternalMetodParameter("会计科目Id", "会计科目Id")]Guid[] glIDs,
            [ExternalMetodParameter("会计ID", "会计ID")]Guid accountingID,
            [ExternalMetodParameter("总经理ID", "总经理ID")]Guid generalManagerID,
            [ExternalMetodParameter("财务经理ID", "财务经理ID")]Guid financeManagerID,
            [ExternalMetodParameter("出纳ID", "出纳ID")]Guid cashierID,
            [ExternalMetodParameter("单据数量", "单据数量")]string receiptQty,
            [ExternalMetodParameter("报销类型", "报销类型")]string feeProperty,
            [ExternalMetodParameter("费用项目Id", "费用项目Id")]Guid[] costItemIds,
            [ExternalMetodParameter("费用金额", "费用金额")]decimal[] amounts,
            [ExternalMetodParameter("费用币种", "费用币种")]string[] currencys,
            [ExternalMetodParameter("费用批注", "费用批注")]string[] remarks);

        [OperationContract]
        [ExternalMethod("保存单条费用记录", "保存单条费用记录")]
        void SaveSingleCostFee(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("申请部门Id", "申请部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid userId,
            [ExternalMetodParameter("银行帐号", "银行帐号Id")]Guid bankID,
            [ExternalMetodParameter("会计ID", "会计ID")]Guid accountingID,
            [ExternalMetodParameter("总经理ID", "总经理ID")]Guid generalManagerID,
            [ExternalMetodParameter("出纳ID", "出纳ID")]Guid cashierID,
            [ExternalMetodParameter("财务经理ID", "财务经理ID")]Guid financeManagerID,
            [ExternalMetodParameter("单据数量", "单据数量")]string receiptQty,
            [ExternalMetodParameter("会计科目Id", "会计科目Id")]Guid glID,
            [ExternalMetodParameter("费用金额", "费用金额")]decimal amount,
            [ExternalMetodParameter("费用币种", "费用币种")]Guid currencyID,
            [ExternalMetodParameter("费用批注", "费用批注")]string remark);

        [OperationContract]
        [ExternalMethod("保存单条费用记录_其他支出", "保存单条费用记录_其他支出")]
        void SaveUniqueCostFee(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("申请部门ID", "申请部门ID")]Guid deptID,
            [ExternalMetodParameter("申请人ID", "申请人ID")]Guid userID,
            [ExternalMetodParameter("借方会计科目Id", "借方会计科目Id")]Guid drGLID,
            [ExternalMetodParameter("会计ID", "会计ID")]Guid accountingID,
            [ExternalMetodParameter("总经理ID", "总经理ID")]Guid generalManagerID,
            [ExternalMetodParameter("出纳ID", "出纳ID")]Guid cashierID,
            [ExternalMetodParameter("财务经理ID", "财务经理ID")]Guid financeManagerID,
            [ExternalMetodParameter("单据数量", "单据数量")]string receiptQty,
            [ExternalMetodParameter("贷方会计科目Id", "贷方会计科目Id")]Guid crGLID,
            [ExternalMetodParameter("费用金额", "费用金额")]decimal amount,
            [ExternalMetodParameter("费用币种", "费用币种")]Guid currencyID,
            [ExternalMetodParameter("费用批注", "费用批注")]string remark,
            [ExternalMetodParameter("部门核算Id", "部门核算Id")]Guid departmentID,
            [ExternalMetodParameter("个人往来Id", "个人往来Id")]Guid personalID,
            [ExternalMetodParameter("客户往来Id", "客户往来Id")]Guid customerID);

        [OperationContract]
        [ExternalMethod("保存运输成本费用记录", "保存运输成本费用记录")]
        void SaveTransportationCostFee(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("申请部门Id", "申请部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid userId,
            [ExternalMetodParameter("费用项目Id", "费用项目Id")]Guid[] costItemIds,
            [ExternalMetodParameter("费用金额", "费用金额")]decimal[] amounts,
            [ExternalMetodParameter("费用币种", "费用币种")]string[] currencys,
            [ExternalMetodParameter("费用批注", "费用批注")]string[] remarks);


        [OperationContract]
        [ExternalMethod("保存亏损单日志记录", "保存亏损单日志记录")]
        void SaveDeficitOperationWorkFlowLog(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("业务类型", "业务类型")]string operationType,
            [ExternalMetodParameter("业务Id", "业务Id")]Guid? operationId,
            [ExternalMetodParameter("创建人", "创建人")]Guid createBy);


        [OperationContract]
        [ExternalMethod("删除工作流申请的费用记录", "删除工作流申请的费用记录")]
        void DeleteCostFee([ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId);


        [OperationContract]
        [ExternalMethod("保存月结协议", "保存月结协议")]
        void SaveMonthlyClosingEntries(
            [ExternalMetodParameter("客户ID", "客户ID")]Guid customerID,
            [ExternalMetodParameter("公司ID", "公司ID")]Guid companyID,
            [ExternalMetodParameter("开始时间", "开始时间")]DateTime beginDate,
            [ExternalMetodParameter("结束时间", "结束时间")]DateTime enddate,
            [ExternalMetodParameter("创建人", "创建人")]Guid createBy,
            [ExternalMetodParameter("财务归档人", "财务归档人")]Guid financeBy,
            [ExternalMetodParameter("流程单号", "流程单号")]string workNo,
            [ExternalMetodParameter("信用额度", "信用额度")]decimal CreditAmount,
            [ExternalMetodParameter("付款方式", "付款方式")]decimal PayType,
            [ExternalMetodParameter("信用期限", "信用期限")]decimal CridetTerm,
            [ExternalMetodParameter("付款日", "付款日")]decimal PaymentDate
            );



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
        [OperationContract]
        [ExternalMethod("保存费用记录New", "保存费用记录New")]
        void SaveCostFeeNew(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("申请部门Id", "申请部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid userId,
            [ExternalMetodParameter("会计ID", "会计ID")]Guid accountingID,
            [ExternalMetodParameter("总经理ID", "总经理ID")]Guid generalManagerID,
            [ExternalMetodParameter("财务经理ID", "财务经理ID")]Guid financeManagerID,
            [ExternalMetodParameter("出纳ID", "出纳ID")]Guid cashierID,
            [ExternalMetodParameter("单据数量", "单据数量")]string receiptQty,
            [ExternalMetodParameter("报销类型", "报销类型")]string feeProperty,
            [ExternalMetodParameter("科目Id集合", "科目Id集合")]Guid[] glIds,
            [ExternalMetodParameter("币种集合", "币种集合")]string[] currencyCodes,
            [ExternalMetodParameter("借方金额集合", "借方金额集合")]decimal[] dramounts,
            [ExternalMetodParameter("贷方金额集合", "贷方金额集合")]decimal[] cramounts,
            [ExternalMetodParameter("摘要集合", "摘要集合")]string[] remarks,
            [ExternalMetodParameter("客户集合", "客户集合")]string[] customerIDs,
            [ExternalMetodParameter("部门集合", "部门集合")]string[] depIDs,
            [ExternalMetodParameter("个人集合", "个人集合")]string[] userIDs);

        [OperationContract]
        [ExternalMethod("保存影视公司费用记录", "保存影视公司费用记录")]
        void SaveMovieCostCostFee(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("申请部门Id", "申请部门Id")]Guid departmentId,
            [ExternalMetodParameter("申请人Id", "申请人Id")]Guid userId,
            [ExternalMetodParameter("影视项目", "影视项目")]Guid movieProjectID,
            [ExternalMetodParameter("会计ID", "会计ID")]Guid accountingID,
            [ExternalMetodParameter("总经理ID", "总经理ID")]Guid generalManagerID,
            [ExternalMetodParameter("财务经理ID", "财务经理ID")]Guid financeManagerID,
            [ExternalMetodParameter("出纳ID", "出纳ID")]Guid cashierID,
            [ExternalMetodParameter("单据数量", "单据数量")]string receiptQty,
            [ExternalMetodParameter("报销类型", "报销类型")]string feeProperty,
            [ExternalMetodParameter("科目Id集合", "科目Id集合")]Guid[] glIds,
            [ExternalMetodParameter("币种集合", "币种集合")]string[] currencyCodes,
            [ExternalMetodParameter("借方金额集合", "借方金额集合")]decimal[] dramounts,
            [ExternalMetodParameter("贷方金额集合", "贷方金额集合")]decimal[] cramounts,
            [ExternalMetodParameter("摘要集合", "摘要集合")]string[] remarks,
            [ExternalMetodParameter("客户集合", "客户集合")]string[] customerIDs,
            [ExternalMetodParameter("部门集合", "部门集合")]string[] depIDs,
            [ExternalMetodParameter("个人集合", "个人集合")]string[] userIDs);



        [OperationContract]
        [ExternalMethod("保存资金调拨流程费用纪录", "保存资金调拨流程费用纪录")]
        void SaveAllocationWorkFLow(
            [ExternalMetodParameter("工作流Id", "工作流Id")]Guid workFlowId,
            [ExternalMetodParameter("单号", "单号")]string no,
            [ExternalMetodParameter("费用生成日期", "费用生成日期")]DateTime feeDate,
            [ExternalMetodParameter("调入部门Id", "调入部门Id")]Guid inDepartmentId,
            [ExternalMetodParameter("调入人Id", "调入人Id")]Guid inUserId,
            [ExternalMetodParameter("调入科目Id集合", "调入科目Id集合")]Guid[] inGlIds,
            [ExternalMetodParameter("调入币种集合", "调入币种集合")]string[] inCurrencyCodes,
            [ExternalMetodParameter("调入借方金额集合", "调入借方金额集合")]decimal[] inDramounts,
            [ExternalMetodParameter("调入贷方金额集合", "调入贷方金额集合")]decimal[] inCramounts,
            [ExternalMetodParameter("调入摘要集合", "调入摘要集合")]string[] inRemarks,
            [ExternalMetodParameter("调入客户集合", "调入客户集合")]string[] inCustomerIDs,
            [ExternalMetodParameter("调出部门Id", "调出部门Id")]Guid outDepartmentId,
            [ExternalMetodParameter("调出人Id", "调出人Id")]Guid outUserId,
            [ExternalMetodParameter("调出科目Id集合", "调出科目Id集合")]Guid[] outGlIds,
            [ExternalMetodParameter("调出币种集合", "调出币种集合")]string[] outCurrencyCodes,
            [ExternalMetodParameter("调出借方金额集合", "调出借方金额集合")]decimal[] outDramounts,
            [ExternalMetodParameter("调出贷方金额集合", "调出贷方金额集合")]decimal[] outCramounts,
            [ExternalMetodParameter("调出摘要集合", "调出摘要集合")]string[] outRemarks,
            [ExternalMetodParameter("调出客户集合", "调出客户集合")]string[] outCustomerIDs);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ICP.WF.ServiceInterface
{
    /// <summary>
    /// 这是一个客户端服务接口，只能在客户端调用
    /// </summary>
    public interface IFormDesignClientService
    {
        /// <summary>
        ///根据xml生成表单控件
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        Control BuildFormFromXml(string xmldata);
    }


    /// <summary>
    /// 这是一个客户端服务接口，只能在客户端调用
    /// </summary>
    public interface IWorkflowClientService
    {
        #region 直接进入申请表单
        /// <summary>
        /// 直接进入申请表单
        /// </summary>
        /// <param name="workFlowId"></param>
        /// <returns></returns>
        Control CreateWorkFlowEntryForm(Guid workFlowId);
        #endregion

        #region 获取流程图控件
        /// <summary>
        /// 获取流程图控件
        /// </summary>
        /// <param name="workFlowId"></param>
        /// <returns></returns>
        Control GetWorkflowChatControl(Guid workFlowId);
        #endregion

        #region 启动入职申请流程

        /// <summary>
        /// 启动入职申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="stafferId">员工ID(重新入职用)</param>
        /// <param name="stafferName">员工名字</param>
        /// <param name="birthDate">出生日期</param>
        /// <param name="sex">性别(0:男,1:女)</param>
        /// <param name="birthPlace">籍贯</param>
        /// <param name="degree">学历</param>
        /// <param name="speciality">专业</param>
        /// <param name="identityCard">身份证</param>
        /// <param name="registeredPlace">户口所在地</param>
        /// <param name="salaryDate">入职记薪日期</param>
        /// <param name="departmentId">入职部门</param>
        /// <param name="postID">岗位</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Guid StartOnTheJobWorkFlow(Guid ownerId,
            Guid ownerDeptId,
            string workName,
            string formTitle,
            Guid stafferId,
            string stafferName,
            DateTime birthDate,
            short sex,
            string birthPlace,
            string degree,
            string speciality,
            string identityCard,
            string registeredPlace,
            DateTime salaryDate,
            Guid departmentId,
            Guid postID,
            string remark);
        #endregion

        #region 启动业务费用申请流程


        /// <summary>
        /// 启动业务费用申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="no">单号</param>
        /// <param name="departmentId">业务所属部门</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="customerId">客户Id</param>
        /// <param name="actionContent">实施类容</param>
        /// <param name="applicant">申请人</param>
        /// <param name="date">申请日期</param>
        /// <param name="property">报销类型(Personal-个人报销,Company-公司报销)</param>
        /// <param name="chargeCodeIds">费用代码</param>
        /// <param name="currencys">币种</param>
        /// <param name="amounts">金额</param>
        /// <param name="remarks">备注</param>
        /// <returns></returns>
        Guid StartBussinessExpenseWorkFlow(Guid ownerId,
            Guid ownerDeptId,
            string workName,
            string formTitle,
            string no,
            Guid departmentId,
            string customerName,
            Guid customerId,
            string actionContent,
            string applicant,
            DateTime date,
            string property,
            Guid[] chargeCodeIds,
            string[] currencys,
            decimal[] amounts,
            string[] remarks);

        #endregion

        #region 启动业务管理成本申请流程

        /// <summary>
        /// 启动业务管理成本申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="no">单号</param>
        /// <param name="date">申请日期</param>
        /// <param name="companyId">业务所属公司</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="freightType">付款方式(0-预付，1-到付)</param>
        /// <param name="paymentCommission">应付业务管理费用</param>
        /// <param name="currency">货币</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="customerId">客户Id</param>
        /// <param name="blno">BL NO</param>
        /// <param name="goods">货量</param>
        /// <param name="profit">利润</param>
        /// <param name="isPaid">是否到帐(0-已到，1-未到)</param>
        /// <param name="sales">业务员</param>
        /// <param name="collectFreightCharges">应收费用</param>
        /// <param name="paymentFreightCharges">应付费用</param>
        /// <param name="remark">备注</param>
        /// <param name="showMaintable">是否显示主信息表</param>
        /// <returns></returns>
        Guid StartReturnCommissionWorkFlow(Guid ownerId,
                   Guid ownerDeptId,
                   string workName,
                   string operactionIds,
                   string formTitle,
                    string no,
                    DateTime date,
                    Guid companyId,
                    string operationNo,
                    short freightType,
                    decimal paymentCommission,
                    string currency,
                    string customerName,
                    Guid customerId,
                    string blno,
                    string goods,
                    string profit,
                    bool isPaid,
                    string sales,
                    string collectFreightCharges,
                    string paymentFreightCharges,
                    string remark,
                    bool showMaintable=false);

        #endregion

        #region 启动付款通知申请流程
         
        /// <summary>
        /// 启动付款通知申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="no">单号</param>
        /// <param name="date">申请时间</param>
        /// <param name="companyId">公司</param>
        /// <param name="blno">提单号</param>
        /// <param name="depositBank">开户行</param>
        /// <param name="amount">金额</param>
        /// <param name="currencyName">币种名称</param>
        /// <param name="original">原币金额</param>
        /// <param name="customerName">客户名</param>
        /// <param name="customerId">客户Id</param>
        /// <param name="dn">DN</param>
        /// <param name="billTo">收款单位</param>
        /// <param name="accountNumber">帐号</param>
        /// <param name="product">品名</param>
        /// <param name="remark">备注</param>
        /// <param name="operatorName">操作</param>
        /// <returns></returns>
        Guid StartNoticeOfPaymentWorkFlow(Guid ownerId,
                                        Guid ownerDeptId,
                                        string workName,
                                        string formTitle,
                                        string no,
                                        DateTime date,
                                        Guid companyId,
                                        string blno,
                                        string depositBank,
                                        string amount,
                                        string currencyName,
                                        string original,
                                        string customerName,
                                        Guid customerId,
                                        string dn,
                                        string billTo,
                                        string accountNumber,
                                        string product,
                                        string remark,
                                        string operatorName);

        #endregion

        #region 启动付款通知(报关)申请流程
        /// <summary> 
        /// 启动付款通知（报关）申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="no">单号</param>
        /// <param name="date">申请时间</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="reason">原因</param>
        /// <param name="applicant">申请人</param>
        /// <param name="customerName">客户名</param>
        /// <param name="customerId">客户Id</param>
        /// <param name="type">类型（0-港建费申请,1-其它费申请,2-付鹏集费用申请）</param>
        /// <param name="operationNos">业务号</param>
        /// <param name="serialNos">序列号</param>
        /// <param name="containerNos">箱号</param>
        /// <param name="billNos">帐单号</param>
        /// <param name="feeNames">费用名</param>
        /// <param name="amounts">金额</param>
        /// <param name="currencys">币种</param>
        /// <returns></returns>
        Guid StartNoticeOfPaymentForCustomsWorkFlow(Guid ownerId,
                               Guid ownerDeptId,
                               string workName,
                               string formTitle,
                               string no,
                                DateTime date,
                                Guid companyId,
                                string reason,
                                string applicant,
                                string customerName,
                                Guid customerId,
                                string type,
                                string[] operationNos,
                                string[] serialNos,
                                string[] containerNos,
                                string[] billNos,
                                string[] feeNames,
                                string[] amounts,
                                string[] currencys);

        #endregion

        #region 启动亏损申请流程
        /// <summary>
        /// 启动亏损申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="no">单号</param>
        /// <param name="companyId">业务发生地</param>
        /// <param name="operationId">业务Id</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="salesName">业务员</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="receivable">应收</param>
        /// <param name="payable">应付</param>
        /// <param name="profit">利润</param>
        /// <param name="requestorName">申请人</param>
        /// <param name="customerId">客户Id</param>
        /// <param name="customer">客户名称</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Guid StartDeficitWorkFlow(Guid ownerId,
                                Guid ownerDeptId,
                                string workName,
                                string formTitle,
                                string no,
                                Guid companyId,
                                Guid operationId,
                                string operationNo,
                                string salesName,
                                string operationType,
                                string receivable,
                                string payable,
                                string profit,
                                string requestorName,
                                Guid customerId,
                                string customer,
                                string remark);

        #endregion

        #region 启动异常放货申请流程

        /// <summary>
        /// 启动异常放货申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="no">单号</param>
        /// <param name="companyId">业务发生地</param>
        /// <param name="operationId">业务Id</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="salesName">业务员</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="receivable">应收</param>
        /// <param name="payable">应付</param>
        /// <param name="profit">利润</param>
        /// <param name="requestorName">申请人</param>
        /// <param name="customerId">客户Id</param>
        /// <param name="customer">客户名称</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Guid StartExceptionRealeaseRCWorkFlow(Guid ownerId,
                                Guid ownerDeptId,
                                string workName,
                                string formTitle,
                                string no,
                                Guid companyId,
                                Guid operationId,
                                string operationNo,
                                string salesName,
                                string operationType,
                                string receivable,
                                string payable,
                                string profit,
                                string requestorName,
                                Guid customerId,
                                string customer,
                                string remark);
        #endregion

        #region 启动额外支付流程
        /// <summary>
        /// 启动额外支付流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="no">单号</param>
        /// <param name="ar">AR金额</param>
        /// <param name="isCompleteAR">是否录入完成</param>
        /// <param name="pr">PR金额</param>
        /// <param name="isCompletePR">是否录入完成</param>
        /// <param name="isAlreadyreceiving">是否已收款</param>
        /// <param name="isShowsubBusiness">是否显示后续业务</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Guid StartAdditionalPaymentsWorkFlow(Guid ownerId,
                                Guid ownerDeptId,
                                 string workName,
                                string formTitle,
                                DateTime date,
                                string no,
                                string ar,
                                bool isCompleteAR,
                                string pr,
                                bool isCompletePR,
                                bool isAlreadyreceiving,
                                bool isShowsubBusiness,
                                string remark
            );
        #endregion

     

    }

    public interface ITaskWorkClientService
    {
        #region 从任务中心得到流程列表的主界面
        /// <summary>
        /// 从任务中心得到流程列表的主界面
        /// </summary>
        /// <returns></returns>
        Control TaskGetWorkList(string viewCode,string strQuery);
        #endregion
    }
}

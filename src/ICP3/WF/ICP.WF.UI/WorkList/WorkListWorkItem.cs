
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerWorkItem.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.UI
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ICP.Framework.ClientComponents.UIFramework;
    using ICP.WF.ServiceInterface;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using System;
    using System.Data;
    using ICP.WF.Activities;
    using System.Linq;
    using ICP.WF.ServiceInterface.DataObject;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 流程设计器WorkItem
    /// </summary>
    public class WorkListWorkItem : WorkItem, IWorkflowClientService
    {


        #region 服务
        public IWorkflowService WorkflowService
        {
            get
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }
        /// <summary>
        /// 工作流配置服务
        /// </summary>
        IWorkFlowConfigService WorkFlowConfigService
        {
            get
            {
                return ServiceClient.GetService<IWorkFlowConfigService>();
            }
        }

        /// <summary>
        /// 工作流拓展服务
        /// </summary>  
        public IWorkFlowExtendService WorkFlowExtendService
        {
            get
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }


        /// <summary>
        /// 根据布局生成UI界面服务
        /// </summary>
        public IUIBuilder UIBuilder
        {
            get
            {
                return ServiceClient.GetClientService<IUIBuilder>();
            }
        }

        #endregion
        private UILayout mainLayout;
        #region 本地方法

        /// <summary>
        /// 显示当前场景
        /// </summary>
        /// <param name="workSpace">WorkSpace</param>
        /// <param name="title">标题</param>
        public void Show(IWorkspace workSpace, string title)
        {
            mainLayout = new UILayout();

            //构建界面布局
            mainLayout.Layout = new PanelLayout
            {
                Childs = new List<BaseLayout>{
                            new SimpleControlLayout
                            {
                                 ControlType=typeof(WorkListToolBarPart),
                                 Properties=new ControlLayoutProperty{ 
                                     Dock = DockStyle.Top, 
                                     Height=22f,
                                     Name=typeof(WorkListToolBarPart).Name, 
                                     Text=LocalData.IsEnglish?"Tools": "工具栏"}
                            },
                            new PanelLayout{
                               Childs =new List<BaseLayout>{
                                          new DockPanelLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(WorkListSearch),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(WorkListSearch).Name,
                                                                            Text=LocalData.IsEnglish?"Search":"查询"}
                                                                    }
                                                                },
                                                                 
                                                                Properties=new DockPanelControlLayoutProperty{
                                                                    Name=typeof(WorkListSearch).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish?"Search":"查询",Dock= DockStyle.Left, Width=200f}
                                                            },
                                        new PanelLayout{
                                                Childs=new List<BaseLayout>{
                                                     new DockPanelLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(WorkListFlowChatPart),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(WorkListFlowChatPart).Name,}
                                                                    }
                                                                },
                                                                         
                                                                Properties=new DockPanelControlLayoutProperty{
                                                                    Name=typeof(WorkListFlowChatPart).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish?"WorkFlow":"流程图",
                                                                    Dock= DockStyle.Right}
                                                            },

                                                             new PanelLayout{
                                                Childs=new List<BaseLayout>{
                                                      new SimpleControlLayout
                                                                {
                                                                     ControlType=typeof(WorkListPart),
                                                                     Properties=new ControlLayoutProperty{ 
                                                                         Dock = DockStyle.Fill, 
                                                                         Height=600f,
                                                                         Name=typeof(WorkListPart).Name, 
                                                                         Text=LocalData.IsEnglish?"List": "列表"}
                                                                }
                                                    },

                                                    Properties=new PanelLayoutProperty{Dock= DockStyle.Fill}
                                                },
                                  
                                                     
                                                },

                                                    Properties=new PanelLayoutProperty{Dock= DockStyle.Fill}
                                             },
                                        },
                               Properties=new PanelLayoutProperty{Dock= DockStyle.Fill}
                            }},
                Properties = new PanelLayoutProperty { Dock = DockStyle.Fill }
            };

            //构建面版之间处理逻辑
            mainLayout.Relations = new List<IPartRelation>
            {
                 new PartRelation{ 
                     Controller=typeof(WorkListMediator), 
                     Name="ShellMediator", 
                     PartNames=new List<string>{
                         typeof(WorkListPart).Name,
                         typeof(WorkListSearch).Name,
                         typeof(WorkListToolBarPart).Name,
                     }
                 }
            };

            //生成界面
            UIBuilder.Build(
                this,
                workSpace,
                title,
                mainLayout);



        }

        #endregion

        #region IWorkFormService Members

        #region 获得工作项编辑控件
        /// <summary>
        /// 获取工作项编辑控件
        /// </summary>
        /// <param name="workid">流程Id</param>
        /// <returns>编辑流程数据的控件</returns>
        public Control CreateWorkFlowEntryForm(Guid workFlowId)
        {
            WorkItemInfo item = WorkflowService.GetWorkitemInfo(workFlowId);
            if (item != null)
            {
                WorkListEditPart newPart = this.Items.AddNew<WorkListEditPart>(Guid.NewGuid().ToString());
                Microsoft.Practices.CompositeUI.SmartParts.IWorkspace workSpace = this.RootWorkItem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

                newPart.WFFormType = WorkFlowFormType.Add;
                newPart.WorkFlowConfigID = item.ID;

                SmartPartInfo sm = new SmartPartInfo();
                sm.Description = LocalData.IsEnglish ? "Application Tasks" : "申请任务";
                sm.Title = LocalData.IsEnglish ? "Application Tasks" : "申请任务";
                workSpace.Show(newPart, sm);
            }

            return null;

        }
        #endregion

        #region 获得流程图显示控件
        /// <summary>
        /// 获取流程图显示控件
        /// </summary>
        /// <param name="workid">流程Id</param>
        /// <returns>返回显示流程图的控件</returns>
        public Control GetWorkflowChatControl(Guid workFlowId)
        {
            ViewSequeueWorkflowView viewer = this.Items.AddNew<ViewSequeueWorkflowView>();
            viewer.ShowWorkFlowChart(WorkflowService, workFlowId);
            return viewer;
        }

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
        public Guid StartOnTheJobWorkFlow(Guid ownerId,
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
            string remark)
        {
            DataSet ds = WorkFlowDataSetBuilder.BuildOnTheJobDataSet(stafferId,
            stafferName,
            birthDate,
            sex,
            birthPlace,
            degree,
            speciality,
            identityCard,
            registeredPlace,
            salaryDate,
            departmentId,
            postID,
            remark);

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WWF_TheEmployeeOnTheJob;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId);

        }
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
        public Guid StartBussinessExpenseWorkFlow(Guid ownerId,
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
                    string[] remarks)
        {

            property = LocalData.IsEnglish ? "Personal" : "个人报销";
            DataSet ds = WorkFlowDataSetBuilder.BuildBussinessExpenseDataSet(no,
                    departmentId,
                    customerName,
                    customerId,
                    actionContent,
                    applicant,
                    date,
                    property,
                    chargeCodeIds,
                    currencys,
                    amounts,
                    remarks);

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WWF_TheBussinessExpense;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId);

        }

        #endregion

        #region 启动业务管理成本申请流程
        /// <summary>
        /// 启动业务管理成本申请流程
        /// </summary>
        /// <param name="ownerId">申请人</param>
        /// <param name="ownerDeptId">申请人所在部门</param>
        /// <param name="workName">工作名</param>
        /// <param name="formTitle">窗体标题</param>
        /// <param name="operactionIds">业务ID集合</param>
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
        public Guid StartReturnCommissionWorkFlow(
                    Guid ownerId,
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
                    bool showMaintable = false
            )
        {
            if (isPaid == null)
            {
                isPaid = false;
            }
            DataSet ds = WorkFlowDataSetBuilder.BuildReturnCommissionDataSet(no,
                    date,
                    companyId,
                    operactionIds,
                    operationNo,
                    freightType,
                    paymentCommission,
                    currency,
                    customerName,
                    customerId,
                    blno,
                    goods,
                    profit,
                    isPaid,
                    sales,
                    collectFreightCharges,
                    paymentFreightCharges,
                    remark);

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WWF_ReturnCommission;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId, showMaintable);
        }

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
        public Guid StartDeficitWorkFlow(Guid ownerId,
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
                                string remark)
        {
            DataSet ds = WorkFlowDataSetBuilder.BuildDeficitDataSet(no,
                  companyId,
                  operationId,
                  operationNo,
                  salesName,
                  operationType,
                  receivable,
                  payable,
                  profit,
                  requestorName,
                  customerId,
                  customer,
                  remark);

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WWF_TheDeficitApprove;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId);

        }


        #endregion

        #region 启动付款通知（报关）申请流程
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
        public Guid StartNoticeOfPaymentForCustomsWorkFlow(Guid ownerId,
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
                                string[] currencys)
        {
            DataSet ds = WorkFlowDataSetBuilder.BuildNoticeOfPaymentForCustomsDataSet(no,
                date,
                companyId,
                reason,
                applicant,
                customerName,
                customerId,
                type,
                operationNos,
                serialNos,
                containerNos,
                billNos,
                feeNames,
                amounts,
                currencys);

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WWF_NoticeOfPayment_Customs;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId);

        }

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
        public Guid StartNoticeOfPaymentWorkFlow(Guid ownerId,
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
                                        string operatorName)
        {
            DataSet ds = WorkFlowDataSetBuilder.BuildNoticeOfPaymentDataSet(no,
                            date,
                            companyId,
                            blno,
                            depositBank,
                            amount,
                            currencyName,
                            original,
                            customerName,
                            customerId,
                            dn,
                            billTo,
                            accountNumber,
                            product,
                            remark,
                            operatorName);

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WWF_NoticeOfPayment;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId);

        }

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
        public Guid StartExceptionRealeaseRCWorkFlow(Guid ownerId,
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
                                string remark)
        {
            DataSet ds = WorkFlowDataSetBuilder.BuildExceptionRealeaseRCDataSet(no,
                  companyId,
                  operationId,
                  operationNo,
                  salesName,
                  operationType,
                  receivable,
                  payable,
                  profit,
                  requestorName,
                  customerId,
                  customer,
                  remark);

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WF_ExceptionReleaseRC;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId);

        }


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
        public Guid StartAdditionalPaymentsWorkFlow(Guid ownerId,
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
             )
        {
            DataSet ds = WorkFlowDataSetBuilder.BuildAdditionalPaymentsDataSet(
                    ownerDeptId,
                    date,
                    no,
                    ar,
                    isCompleteAR,
                    pr,
                    isCompletePR,
                    isAlreadyreceiving,
                    isShowsubBusiness,
                    remark
                   );

            string key = WF.ServiceInterface.DataObject.WorkFlowConstants.WWF_AdditionalPaymentsApprove;

            return StartWorkFlow(key, workName, formTitle, ds, ownerId, ownerDeptId);

        }
        #endregion

        #region 发起一个新的流程
        /// <summary>
        /// 发起一个新的流程
        /// </summary>
        /// <param name="key"></param>
        /// <param name="workName"></param>
        /// <param name="formTitle"></param>
        /// <param name="ds"></param>
        /// <param name="ownerId"></param>
        /// <param name="ownerDeptId"></param>
        /// <param name="showMaintable">是否显示主信息</param>
        /// <returns></returns>
        private Guid StartWorkFlow(string key,
                                   string workName,
                                   string formTitle,
                                   DataSet ds,
                                   Guid ownerId,
                                   Guid ownerDeptId,
                                   bool showMaintable=false)
        {
            WorkItemInfo itemInfo = WorkflowService.StartWorkflow(
                          key,
                          workName,
                          ds,
                          ownerId,
                          ownerDeptId,
                          true);

            if (itemInfo == null)
            {
                throw new ApplicationException("没有找到对应的流程，请联系系统管理员");
            }

            WorkFlowData wfData = WorkflowService.GetWorkFlowData(itemInfo.ID, LocalData.UserInfo.LoginID);
            wfData.No = itemInfo.WorkNo;

            WorkListEditPart newPart = this.Items.AddNew<WorkListEditPart>();
            Microsoft.Practices.CompositeUI.SmartParts.IWorkspace workSpace = this.RootWorkItem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
            newPart.WFFormType = WorkFlowFormType.Edit;
            newPart.WorkInfoID = wfData.Id;
            newPart.ShowMainTable = showMaintable;
            newPart.WorkFlowConfigID = WorkFlowConfigService.GetWorkFlowConfigInfoByKey(key, false).Id;
            newPart.CurrentID = wfData.CurrentWorkItemId;
            newPart.workflowData = wfData;
            newPart.Dock = DockStyle.Fill;
            SmartPartInfo sm = new SmartPartInfo();
            sm.Description = workName;
            sm.Title = formTitle;
            
            workSpace.Show(newPart, sm);

            return itemInfo.ID;

        }
        #endregion


        #endregion
        /// <summary>
        /// 关闭当前场景(在这里处理释放所有该场景创建的资源)
        /// </summary>
        public void Close()
        {
            if (this.mainLayout != null)
            {
                this.mainLayout.Dispose();
                this.mainLayout = null;
            }
            if (this.Status != WorkItemStatus.Terminated)
            {
                this.Terminate();
            }

        }
    }

    /// <summary>
    /// 获得关联的流程的表单
    /// </summary>
    public class WorkFlowDataSetBuilder
    {

        #region 获得入职流程的表单
        public static DataSet BuildOnTheJobDataSet(Guid stafferId,
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
            string remark)
        {
            DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable("MainTable");

            dataTable.Columns.Add("StafferId", typeof(Guid));
            dataTable.Columns.Add("StafferName", typeof(string));
            dataTable.Columns.Add("BirthDate", typeof(DateTime));
            dataTable.Columns.Add("Sex", typeof(string));
            dataTable.Columns.Add("BirthPlace", typeof(string));
            dataTable.Columns.Add("Degree", typeof(string));
            dataTable.Columns.Add("Speciality", typeof(string));
            dataTable.Columns.Add("IdentityCard", typeof(string));
            dataTable.Columns.Add("RegisteredPlace", typeof(string));
            dataTable.Columns.Add("SalaryDate", typeof(DateTime));
            dataTable.Columns.Add("DepartmentDepartmentId", typeof(Guid));
            dataTable.Columns.Add("PostID", typeof(Guid));
            dataTable.Columns.Add("Remark", typeof(string));


            DataRow row = dataTable.NewRow();
            row["StafferId"] = stafferId;
            row["StafferName"] = stafferName;
            row["BirthDate"] = birthDate;
            row["Sex"] = sex.ToString();
            row["BirthPlace"] = birthPlace;
            row["Degree"] = degree;
            row["Speciality"] = speciality;
            row["IdentityCard"] = identityCard;
            row["RegisteredPlace"] = registeredPlace;
            row["SalaryDate"] = salaryDate;
            row["DepartmentDepartmentId"] = departmentId;
            row["PostID"] = postID;
            row["Remark"] = remark;
            dataTable.Rows.Add(row);


            dataset.Tables.Add(dataTable);

            return dataset;
        }
        #endregion

        #region 获得业务费用申请流程
        /// <summary>
        /// 业务费用申请流程
        /// </summary>
        /// <param name="no"></param>
        /// <param name="departmentId"></param>
        /// <param name="customerName"></param>
        /// <param name="customerId"></param>
        /// <param name="actionContent"></param>
        /// <param name="applicant"></param>
        /// <param name="date"></param>
        /// <param name="property"></param>
        /// <param name="chargeCodeIds"></param>
        /// <param name="currencys"></param>
        /// <param name="amounts"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public static DataSet BuildBussinessExpenseDataSet(string no,
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
                    string[] remarks)
        {
            System.Data.DataSet vExpense = new DataSet();
            System.Data.DataTable dtExpense = new DataTable("MainTable");

            System.Data.DataColumn vCol = new DataColumn();
            vCol.ColumnName = "No";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "DepartmentDepartmentId";
            vCol.DataType = typeof(Guid);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Customer";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "CustomerId";
            vCol.DataType = typeof(Guid);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "ActionContent";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "FinalManager";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);


            vCol = new DataColumn();
            vCol.ColumnName = "Cashier";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Applicant";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Date";
            vCol.DataType = typeof(DateTime);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Property";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "GeneralManager";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Accountant";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Recipients";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Manager";
            vCol.DataType = typeof(string);
            dtExpense.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "TotalAmount";
            vCol.DataType = typeof(decimal);
            dtExpense.Columns.Add(vCol);


            DataRow row = dtExpense.NewRow();
            row["No"] = no;
            row["DepartmentDepartmentId"] = departmentId;
            row["Customer"] = customerName;
            row["CustomerId"] = customerId;
            row["ActionContent"] = actionContent;
            row["FinalManager"] = string.Empty;
            row["Cashier"] = string.Empty;
            row["Applicant"] = applicant;
            row["Date"] = date;
            row["Property"] = property;
            row["GeneralManager"] = string.Empty;
            row["Accountant"] = string.Empty;
            row["Recipients"] = string.Empty;
            row["Manager"] = string.Empty;
            row["TotalAmount"] = amounts.Sum();
            dtExpense.Rows.Add(row);

            vExpense.Tables.Add(dtExpense);



            System.Data.DataTable dtFees = new DataTable("Fees");
            vCol = new DataColumn();
            vCol.ColumnName = "ChargeCode";
            vCol.DataType = typeof(Guid);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Currency";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Amount";
            vCol.DataType = typeof(decimal);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Remark";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            for (int r = 0; r < chargeCodeIds.Length; r++)
            {
                DataRow dr = dtFees.NewRow();
                dr["ChargeCode"] = chargeCodeIds[r];
                dr["Currency"] = currencys[r];
                dr["Amount"] = amounts[r];
                dr["Remark"] = remarks[r];
                dtFees.Rows.Add(dr);
            }
            dtFees.AcceptChanges();
            vExpense.Tables.Add(dtFees);

            return vExpense;
        }
        #endregion

        #region 获得业务管理成本表单
        /// <summary>
        /// 获得业务管理成本表单
        /// </summary>
        /// <param name="no"></param>
        /// <param name="date"></param>
        /// <param name="companyId"></param>
        /// <param name="operationNo"></param>
        /// <param name="freightType"></param>
        /// <param name="paymentCommission"></param>
        /// <param name="currency"></param>
        /// <param name="customerName"></param>
        /// <param name="customerId"></param>
        /// <param name="blno"></param>
        /// <param name="goods"></param>
        /// <param name="profit"></param>
        /// <param name="isPaid"></param>
        /// <param name="sales"></param>
        /// <param name="collectFreightCharges"></param>
        /// <param name="paymentFreightCharges"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static DataSet BuildReturnCommissionDataSet(string no,
                    DateTime date,
                    Guid companyId,
                    string operactionIds,
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
                    string remark)
        {
            DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable("MainTable");

            dataTable.Columns.Add("OperactionIDs", typeof(String));
            dataTable.Columns.Add("No", typeof(String));
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("DepartmentID", typeof(Guid));
            dataTable.Columns.Add("OperationNo", typeof(String));
            dataTable.Columns.Add("FreightType", typeof(String));
            dataTable.Columns.Add("PaymentCommission", typeof(Decimal));
            dataTable.Columns.Add("Currency", typeof(String));
            dataTable.Columns.Add("CustomerID", typeof(Guid));
            dataTable.Columns.Add("Customer", typeof(String));
            dataTable.Columns.Add("BLNO", typeof(String));
            dataTable.Columns.Add("Goods", typeof(String));
            dataTable.Columns.Add("Profit", typeof(String));
            dataTable.Columns.Add("IsPaid", typeof(String));
            dataTable.Columns.Add("Remark", typeof(String));
            dataTable.Columns.Add("Sales", typeof(String));
            dataTable.Columns.Add("FinalManager", typeof(String));
            dataTable.Columns.Add("CollectFreightCharges", typeof(String));
            dataTable.Columns.Add("PaymentFreightCharges", typeof(String));
            dataTable.Columns.Add("Charger", typeof(String));
            dataTable.Columns.Add("BusinessManager", typeof(String));
            dataTable.Columns.Add("GenerralManger", typeof(String));
            dataTable.Columns.Add("Cashier", typeof(String));


            DataRow row = dataTable.NewRow();
            row["OperactionIDs"] = operactionIds;
            row["No"] = string.Empty;
            row["Date"] = date;
            row["DepartmentID"] = companyId;
            row["OperationNo"] = operationNo;
            row["FreightType"] = freightType.ToString();
            row["PaymentCommission"] = paymentCommission;
            row["Currency"] = currency;
            row["CustomerID"] = customerId;
            row["Customer"] = customerName;
            row["BLNO"] = blno;
            row["Goods"] = goods;
            row["Profit"] = profit;
            row["IsPaid"] = isPaid ? "0" : "1";
            row["Sales"] = sales;
            row["Remark"] = remark;
            row["CollectFreightCharges"] = collectFreightCharges;
            row["PaymentFreightCharges"] = paymentFreightCharges;
            row["FinalManager"] = string.Empty;
            row["Charger"] = string.Empty;
            row["BusinessManager"] = string.Empty;
            row["GenerralManger"] = string.Empty;
            row["Cashier"] = string.Empty;

            dataTable.Rows.Add(row);

            dataset.Tables.Add(dataTable);

            return dataset;
        }
        #endregion

        #region 获得亏损流程表单
        public static DataSet BuildDeficitDataSet(string no,
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
                  string remark)
        {
            DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable("MainTable");

            dataTable.Columns.Add("No", typeof(string));
            dataTable.Columns.Add("CompanyIdDepartmentId", typeof(Guid));
            dataTable.Columns.Add("OperationNo", typeof(string));
            dataTable.Columns.Add("SalesName", typeof(string));
            dataTable.Columns.Add("Remark", typeof(string));
            dataTable.Columns.Add("ManagerName", typeof(string));
            dataTable.Columns.Add("OperationType", typeof(string));
            dataTable.Columns.Add("Receivable", typeof(string));
            dataTable.Columns.Add("Payable", typeof(string));
            dataTable.Columns.Add("Profit", typeof(string));
            dataTable.Columns.Add("RequestorName", typeof(string));
            dataTable.Columns.Add("GeneralManager", typeof(string));
            dataTable.Columns.Add("Customer", typeof(string));
            dataTable.Columns.Add("CustomerId", typeof(Guid));
            dataTable.Columns.Add("ChargerName", typeof(string));
            dataTable.Columns.Add("OperationId", typeof(Guid));



            DataRow row = dataTable.NewRow();
            row["No"] = string.Empty;
            row["CompanyIdDepartmentId"] = companyId;
            row["OperationNo"] = operationNo;
            row["SalesName"] = salesName == null ? string.Empty : salesName;
            row["Remark"] = remark == null ? string.Empty : remark;
            row["OperationType"] = operationType;
            row["Receivable"] = receivable == null ? "0" : receivable;
            row["Payable"] = payable == null ? "0" : payable;
            row["Profit"] = profit == null ? "0" : profit;
            row["RequestorName"] = requestorName == null ? string.Empty : requestorName;
            row["GeneralManager"] = string.Empty;
            row["Customer"] = customer;
            row["CustomerId"] = customerId;
            row["ChargerName"] = string.Empty;
            row["OperationId"] = operationId;
            row["ManagerName"] = string.Empty;

            dataTable.Rows.Add(row);

            dataset.Tables.Add(dataTable);

            return dataset;
        }

        #endregion

        #region 获得异常放货流程表单
        public static DataSet BuildExceptionRealeaseRCDataSet(string no,
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
                  string remark)
        {
            DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable("MainTable");

            dataTable.Columns.Add("No", typeof(string));
            dataTable.Columns.Add("CompanyIDDepartmentID", typeof(Guid));
            dataTable.Columns.Add("OperationNo", typeof(string));
            dataTable.Columns.Add("SalesName", typeof(string));
            dataTable.Columns.Add("Remark", typeof(string));
            dataTable.Columns.Add("ManagerName", typeof(string));
            dataTable.Columns.Add("OperationType", typeof(string));
            dataTable.Columns.Add("Receivable", typeof(string));
            dataTable.Columns.Add("Payable", typeof(string));
            dataTable.Columns.Add("Profit", typeof(string));
            dataTable.Columns.Add("RequestorName", typeof(string));
            dataTable.Columns.Add("GeneralManager", typeof(string));
            dataTable.Columns.Add("Customer", typeof(string));
            dataTable.Columns.Add("CustomerId", typeof(Guid));
            dataTable.Columns.Add("ChargerName", typeof(string));
            dataTable.Columns.Add("OperationId", typeof(Guid));



            DataRow row = dataTable.NewRow();
            row["No"] = string.Empty;
            row["CompanyIDDepartmentID"] = companyId;
            row["OperationNo"] = operationNo;
            row["SalesName"] = salesName == null ? string.Empty : salesName;
            row["Remark"] = remark == null ? string.Empty : remark;
            row["OperationType"] = operationType;
            row["Receivable"] = receivable == null ? "0" : receivable;
            row["Payable"] = payable == null ? "0" : payable;
            row["Profit"] = profit == null ? "0" : profit;
            row["RequestorName"] = requestorName == null ? string.Empty : requestorName;
            row["GeneralManager"] = string.Empty;
            row["Customer"] = customer;
            row["CustomerId"] = customerId;
            row["OperationId"] = operationId;

            dataTable.Rows.Add(row);

            dataset.Tables.Add(dataTable);

            return dataset;
        }

        #endregion

        #region 获得付款通知（报关）申请流程表单
        /// <summary>
        /// 启动付款通知（报关）申请流程
        /// </summary>
        /// <param name="no"></param>
        /// <param name="date"></param>
        /// <param name="companyId"></param>
        /// <param name="reason"></param>
        /// <param name="applicant"></param>
        /// <param name="customerName"></param>
        /// <param name="customerId"></param>
        /// <param name="type"></param>
        /// <param name="operationNos"></param>
        /// <param name="serialNos"></param>
        /// <param name="containerNos"></param>
        /// <param name="billNos"></param>
        /// <param name="feeNames"></param>
        /// <param name="amounts"></param>
        /// <param name="currencys"></param>
        /// <returns></returns>
        public static DataSet BuildNoticeOfPaymentForCustomsDataSet(string no,
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
                string[] currencys)
        {
            DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable("MainTable");

            dataTable.Columns.Add("No", typeof(string));
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("DepartmentDepartmentId", typeof(Guid));
            dataTable.Columns.Add("Reason", typeof(string));
            dataTable.Columns.Add("Cashier", typeof(string));
            dataTable.Columns.Add("Recipients", typeof(string));
            dataTable.Columns.Add("FinalManager", typeof(string));
            dataTable.Columns.Add("GeneralManager", typeof(string));
            dataTable.Columns.Add("Applicant", typeof(string));
            dataTable.Columns.Add("Manager", typeof(string));
            dataTable.Columns.Add("Customer", typeof(string));
            dataTable.Columns.Add("CustomerId", typeof(Guid));
            dataTable.Columns.Add("Type", typeof(string));
            dataTable.Columns.Add("TotalAmount", typeof(string));


            DataRow row = dataTable.NewRow();
            row["No"] = no;
            row["Date"] = date;
            row["DepartmentDepartmentId"] = companyId;
            row["Reason"] = reason;
            row["Cashier"] = string.Empty;
            row["Recipients"] = string.Empty;
            row["FinalManager"] = string.Empty;
            row["GeneralManager"] = string.Empty;
            row["Applicant"] = applicant == null ? string.Empty : applicant;
            row["Manager"] = string.Empty;
            row["Customer"] = customerName;
            row["CustomerId"] = customerId;
            row["Type"] = type;



            System.Data.DataTable dtFees = new DataTable("Fees");
            DataColumn vCol = new DataColumn();
            vCol.ColumnName = "OperationNo";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "SerialNo";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "ContainerNo";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "BillNo";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "FeeName";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);


            vCol = new DataColumn();
            vCol.ColumnName = "Amount";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            vCol = new DataColumn();
            vCol.ColumnName = "Currency";
            vCol.DataType = typeof(string);
            dtFees.Columns.Add(vCol);

            decimal totalAmt = 0m;
            for (int r = 0; r < operationNos.Length; r++)
            {
                DataRow dr = dtFees.NewRow();
                dr["OperationNo"] = operationNos[r];
                dr["SerialNo"] = serialNos[r];
                dr["ContainerNo"] = containerNos[r];
                dr["BillNo"] = billNos[r];

                dr["FeeName"] = feeNames[r];
                dr["Amount"] = amounts[r];
                dr["Currency"] = currencys[r];

                totalAmt += decimal.Parse(amounts[r]);
                dtFees.Rows.Add(dr);
            }

            row["TotalAmount"] = totalAmt.ToString("F2");
            dataTable.Rows.Add(row);
            dataset.Tables.Add(dataTable);

            dtFees.AcceptChanges();
            dataset.Tables.Add(dtFees);
            return dataset;
        }

        #endregion

        #region 获得付款通知申请流程
        /// <summary>
        /// 获得付款通知申请流程静音
        /// </summary>
        /// <param name="no"></param>
        /// <param name="date"></param>
        /// <param name="companyId"></param>
        /// <param name="blno"></param>
        /// <param name="depositBank"></param>
        /// <param name="amount"></param>
        /// <param name="original"></param>
        /// <param name="customerName"></param>
        /// <param name="customerId"></param>
        /// <param name="dn"></param>
        /// <param name="billTo"></param>
        /// <param name="accountNumber"></param>
        /// <param name="product"></param>
        /// <param name="remark"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public static DataSet BuildNoticeOfPaymentDataSet(string no,
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
                            string operatorName)
        {
            DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable("MainTable");

            dataTable.Columns.Add("No", typeof(string));
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("DepartmentDepartmentId", typeof(Guid));
            dataTable.Columns.Add("BLNO", typeof(string));
            dataTable.Columns.Add("DepositBank", typeof(string));
            dataTable.Columns.Add("FinalManager", typeof(string));
            dataTable.Columns.Add("Cashier", typeof(string));
            dataTable.Columns.Add("Amount", typeof(string));
            dataTable.Columns.Add("AmountName", typeof(string));
            dataTable.Columns.Add("Original", typeof(string));
            dataTable.Columns.Add("Customer", typeof(string));
            dataTable.Columns.Add("CustomerId", typeof(Guid));
            dataTable.Columns.Add("DN", typeof(string));
            dataTable.Columns.Add("BillTo", typeof(string));
            dataTable.Columns.Add("AccountNumber", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Charger", typeof(string));
            dataTable.Columns.Add("Remark", typeof(string));
            dataTable.Columns.Add("Operator", typeof(string));
            dataTable.Columns.Add("GeneralManager", typeof(string));


            DataRow row = dataTable.NewRow();
            row["No"] = string.Empty;
            row["Date"] = date;
            row["DepartmentDepartmentId"] = companyId;
            row["BLNO"] = blno == null ? string.Empty : blno;
            row["DepositBank"] = depositBank == null ? string.Empty : depositBank;
            row["FinalManager"] = string.Empty;
            row["Cashier"] = string.Empty;
            row["Amount"] = amount;
            row["AmountName"] = currencyName == null ? string.Empty : currencyName;
            row["Original"] = original;
            row["Customer"] = customerName == null ? string.Empty : customerName;
            row["CustomerId"] = customerId;
            row["DN"] = dn == null ? string.Empty : dn;
            row["BillTo"] = billTo == null ? string.Empty : billTo;
            row["AccountNumber"] = accountNumber == null ? string.Empty : accountNumber;
            row["Product"] = product == null ? string.Empty : product;
            row["Charger"] = string.Empty;
            row["Remark"] = remark == null ? string.Empty : remark;
            row["Operator"] = operatorName == null ? string.Empty : operatorName;
            row["GeneralManager"] = string.Empty;

            dataTable.Rows.Add(row);
            dataset.Tables.Add(dataTable);


            return dataset;
        }
        #endregion

        #region 获得额外支付流程表单
        /// <summary>
        /// 获得额外支付流程表单
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="no">表单</param>
        /// <param name="ar">AR金额</param>
        /// <param name="isCompleteAR">AR是否录入完成</param>
        /// <param name="pr">PR金额</param>
        /// <param name="isCompletePR">PR是否录入完成</param>
        /// <param name="isAlreadyreceiving">是否已付款</param>
        /// <param name="isShowsubBusiness">是否显示后续业务</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public static DataSet BuildAdditionalPaymentsDataSet(
                                Guid companyId,
                                DateTime date,
                                string no,
                                string ar,
                                bool isCompleteAR,
                                string pr,
                                bool isCompletePR,
                                bool isAlreadyreceiving,
                                bool isShowsubBusiness,
                                string remark
                                )
        {
            DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable("MainTable");

            dataTable.Columns.Add("No", typeof(string));
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("DepartmentDepartmentId", typeof(Guid));
            dataTable.Columns.Add("AmountAR", typeof(string));
            dataTable.Columns.Add("CompleteEntryAR", typeof(bool));
            dataTable.Columns.Add("AmountPR", typeof(string));
            dataTable.Columns.Add("CompleteEntryPR", typeof(bool));
            dataTable.Columns.Add("SubBusiness", typeof(bool));
            dataTable.Columns.Add("AlreadyReceiving", typeof(bool));
            dataTable.Columns.Add("Remark", typeof(string));

            DataRow row = dataTable.NewRow();
            row["No"] = string.Empty;
            row["Date"] = date;
            row["DepartmentDepartmentId"] = companyId;
            row["AmountAR"] = ar == null ? string.Empty : ar;
            row["CompleteEntryAR"] = isCompleteAR ? true : false;
            row["AmountPR"] = pr == null ? string.Empty : pr;
            row["CompleteEntryPR"] = isCompletePR ? true : false;
            row["AlreadyReceiving"] = isAlreadyreceiving ? true : false;
            row["SubBusiness"] = isCompletePR ? true : false;
            row["Remark"] = remark == null ? string.Empty : remark;

            dataTable.Rows.Add(row);
            dataset.Tables.Add(dataTable);

            return dataset;
        }
        #endregion
    }


}

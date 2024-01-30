
namespace ICP.WF.ServiceInterface.DataObject
{
    public static class WWFConstants
    {
        //部门ID
        public static readonly string ProposerDepartmentIDCode = "PROPOSERDEPARTMENTCODE";
        public static readonly string ProposerDepartmentId_C = "申请人所在部门ID";
        public static readonly string ProposerDepartmentId_E = "Department ID of proposer";

        //部门名称
        public static readonly string ProposerDepartmentNameCode = "PROPOSERDEPARTMENTCNAME";
        public static readonly string ProposerDepartmentName_C = "申请人所在部门中文名";
        public static readonly string ProposerDepartmentName_E = "Department EName of proposer";

        //部门全称
        public static readonly string ProposerDepartmentFullNameCode = "PROPOSERDEPARTMENTFULLNAME";
        public static readonly string ProposerDepartmentFullName_C = "申请人所在部门的中文全称";
        public static readonly string ProposerDepartmentFullName_E = "Department EFullName of proposer";

        //公司ID
        public static readonly string ProposerCompanyIDCode = "PROPOSERCOMPANYCode";
        public static readonly string ProposerCompanyId_C = "申请人所在公司ID";
        public static readonly string ProposerCompanyId_E = "Company ID of proposer";

        //公司名称
        public static readonly string ProposerCompanyNameCode = "PROPOSERCOMPANYNAME";
        public static readonly string ProposerCompanyName_C = "申请人所在公司中文名";
        public static readonly string ProposerCompanyName_E = "Company EName of proposer";

        //公司全称
        public static readonly string ProposerCompanyFullNameCode = "PROPOSERCOMPANYEFULLNAME";
        public static readonly string ProposerCompanyFullName_C = "申请人所在公司的中文全称";
        public static readonly string ProposerCompanyFullName_E = "Company EFullName of proposer";


        //申请人ID
        public static readonly string ProposerIDCode = "PROPOSERID";
        public static readonly string ProposerID_C = "申请人ID";
        public static readonly string ProposerID_E = "Proposer ID";

        //申请人名
        public static readonly string ProposerNameCode = "PROPOSERName";
        public static readonly string ProposerName_E = "Proposer Name";
        public static readonly string ProposerName_C = "申请人名";

        //流程ID
        public static readonly string WorkflowNameCode = "WORKFLOWCNAME";
        public static readonly string WorkflowName_C = "流程名中文名";
        public static readonly string WorkflowName_E = "Workflow English name";

        //工作单号
        public static readonly string WorkflowNoCode = "WORKFLOWNO";
        public static readonly string WorkflowNo_C = "工作单号";
        public static readonly string WorkflowNo_E = "Work No";


        //工作名
        public static readonly string WorkNameCode = "WORKNAME";
        public static readonly string WorkName_C = "工作名";
        public static readonly string WorkName_E = "Work Name";

        //流程ID
        public static readonly string WorkflowIdCode = "WORKFLOWID";
        public static readonly string WorkflowId_C = "流程唯一标识";
        public static readonly string WorkflowId_E = "The only identification of workflow";

        //任务名
        public static readonly string WorkItemNameCode = "WORKITEMNAME";
        public static readonly string WorkItemName_C = "任务名";
        public static readonly string WorkItemName_E = "Task Name";

        //当前步执行人ID
        public static readonly string CurrentExcutorID = "CURRENTEXCUTORID";
        public static readonly string CurrentExcutorID_C = "当前步执行人ID";
        public static readonly string CurrentExcutorID_E = "Current Step Excutor ID";

        //当前步执行人
        public static readonly string CurrentExcutor = "CURRENTEXCUTOR";
        public static readonly string CurrentExcutor_C = "当前步执行人";
        public static readonly string CurrentExcutor_E = "Current Step Excutor";

        //当前步执行完成时间
        public static readonly string CurrentFinishDate = "CURRENTFINISHDATE";
        public static readonly string CurrentFinishDate_C = "当前步执完成时间";
        public static readonly string CurrentFinishDate_E = "Current Step Finish Time";

        //申请时间
        public static readonly string CurrentDate = "CURRENTDATE";
        public static readonly string CurrentDate_C = "申请时间";
        public static readonly string CurrentDate_E = "Apply Date";


        public static readonly string WorkFlowTitle = "WorkFlowTitle";
        public static readonly string MainWorkItemDataSet = "MainWorkItemDataSet";
        public static readonly string RuleFile = "RuleFile";


        public static readonly string DataCollection = "DataCollection";
        public static readonly string Proposer = "Proposer";
        public static readonly string ProposerDepartment = "ProposerDepartment";
        public static readonly string WorkFlowKey = "WorkFlowKey";


        public static readonly string WorkFlowState = "WorkFlowState";

        public static readonly string InitDataId = "InitDataId";

        public static readonly string DepartmentLike = "DepartmentLike";
        public static readonly string DepartmentLike_C = "部门及下属";
        public static readonly string DepartmentLike_E = "Department Like";

        public static readonly string DepartmentEqual = "DepartmentEqual";
        public static readonly string DepartmentEqual_C = "部门等于";
        public static readonly string DepartmentEqual_E = "Department Equal";


        public static readonly string CompanyLike = "CompanyLike";
        public static readonly string CompanyLike_C = "公司及下属";
        public static readonly string CompanyLike_E = "Company Like";

        public static readonly string CompanyEqual = "CompanyEqual";
        public static readonly string CompanyEqual_C = "公司等于";
        public static readonly string CompanyEqual_E = "Company Equal";



        public static readonly string SectionLike = "SectionLike";
        public static readonly string SectionLike_C = "区域及下属";
        public static readonly string SectionLike_E = "Section Like";

        public static readonly string SectionEqual = "SectionEqual";
        public static readonly string SectionEqual_C = "区域等于";
        public static readonly string SectionEqual_E = "Section Equal";

        public static readonly string ELike = "Like";
        public static readonly string CLike = "包含";
        public static readonly string ENotLike = "Not Like";
        public static readonly string CNotLike = "不包含";


        public static readonly string DepartmentID = "DepartmentID";
        public static readonly string DepartmentName = "DepartmentName";

        public static readonly string UserID = "UserID";
        public static readonly string UserName = "UserName";

        public static readonly string JobID = "JobID";
        public static readonly string JobName = "JobName";


        /// <summary>
        ///  退佣搜索器
        /// </summary>
        public const string CommissionFinder = "CommissionFinder";
        
        /// <summary>
        /// 客户跟进纪录搜索器
        /// </summary>
        public const string CustomerExpenseTouchFinder = "CustomerExpenseTouchFinder";

    }
}

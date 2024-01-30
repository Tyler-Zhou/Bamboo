using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.ServiceInterface
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum WorkItemState
    {
        /// <summary>
        /// 无法
        /// </summary>
        None = 0,

        /// <summary>
        /// 待办
        /// </summary>
        [MemberDescription("待办", "Waiting")]
        Waiting=1,

        /// <summary>
        /// 在办
        /// </summary>
        [MemberDescription("在办", "Processing")]
        Processing=2,

        /// <summary>
        /// 完成
        /// </summary>
        [MemberDescription("完成", "Finished")]
        Finished=3,

    }


    /// <summary>
    /// 流程状态
    /// </summary>
    public enum WorkflowState
    {
        None=0,

        /// <summary>
        /// 活动状态
        /// </summary>
        [MemberDescription("活动状态", "Activated")]
        Activated = 1,


        /// <summary>
        ///上级打回
        /// </summary>
        [MemberDescription("上级打回", "Return")]
        Return = 2,

        /// <summary>
        /// 完成
        /// </summary>
        [MemberDescription("完成", "Finished")]
        Finished = 3,

        /// <summary>
        ///自己撤销 
        /// </summary>
        [MemberDescription("自己撤销", "Cancel")]
        Cancel = 4,


    }

    /// <summary>
    /// 任务查询状态
    /// </summary>
    public enum WorkItemSearchStatus
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 0,

        /// <summary>
        /// 待办
        /// </summary>
        [MemberDescription("待办", "Waiting")]
        Waiting = 1,

        /// <summary>
        /// 在办
        /// </summary>
        [MemberDescription("在办", "Processing")]
        Processing = 2,

        /// <summary>
        ///已办
        /// </summary>
        [MemberDescription("已办", "OnceProcess")]
        OnceProcess = 6,
     
        /// <summary>
        /// 完成
        /// </summary>
        [MemberDescription("完成", "Finished")]
        Finished = 3,

        ///
        ///自己撤销 
        /// </summary>
        [MemberDescription("自己撤销", "Cancel")]
        Cancel = 4,

        /// <summary>
        ///上级打回
        /// </summary>
        [MemberDescription("上级打回", "Return")]
        Return = 5,
 
    }

    /// <summary>
    /// 数据列类型()
    /// </summary>
    public enum DataItemType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        [MemberDescription("字符串", "String")]
        String = 0,

        /// <summary>
        /// 整型数字
        /// </summary>
        [MemberDescription("整型数字", "String")]
        Int = 1,

        /// <summary>
        /// 货币类型
        /// </summary>
        [MemberDescription("货币类型", "Decimal")]
        Decimal = 2,

        /// <summary>
        /// 布尔类型
        /// </summary>
        [MemberDescription("布尔类型", "Boolean")]
        Boolean = 3,

        /// <summary>
        /// Guid类型
        /// </summary>
        [MemberDescription("Guid类型", "Guid")]
        Guid = 4,

        /// <summary>
        /// 日期类型
        /// </summary>
        [MemberDescription("日期类型", "DateTime")]
        DateTime=5,

        /// <summary>
        /// 短整型
        /// </summary>
        [MemberDescription("短整型", "Short")]
        Short = 6,

    }
    

    /// <summary>
    /// 性别
    /// </summary>
    public enum SexType
    {
        None = 0,
        /// <summary>
        /// 女性,
        /// </summary>
        Female,

        /// <summary>
        /// 男性
        /// </summary>
        Male
    }

    /// <summary>
    /// 重写方式
    /// </summary>
    public enum RewriteMode
    {
        None = 0,
        /// <summary>
        /// 追加
        /// </summary>
        [MemberDescription("追加", "Append")]
        Append,

        /// <summary>
        /// 替换
        /// </summary>
        [MemberDescription("替换", "Replace")]
        Replace,

        /// <summary>
        /// 追加信息，带有执行人名
        /// </summary>
        [MemberDescription("追加(带有执行人名)", "AppendAndExcutor")]
        AppendAndExcutor
    }

    /// <summary>
    /// 文档类型
    /// </summary>
    public enum DocumentNodeType
    {
        None = 0,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder=1,

        /// <summary>
        /// 文件
        /// </summary>
        File=2
    }

    /// <summary>
    /// 工作流窗体类型
    /// </summary>
    public enum WorkFlowFormType
    { 
        None=0,
        /// <summary>
        /// 新增
        /// </summary>
        Add=1,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit=2,
        /// <summary>
        /// 查看
        /// </summary>
        View=3
    }

    /// <summary>
    /// 工作列表查询类型
    /// </summary>
    public enum WorkListSearchType
    { 
        /// <summary>
        /// 全部
        /// </summary>
        All=0,
        /// <summary>
        /// 我创建的
        /// </summary>
        Me=1,
        /// <summary>
        /// 我参与的
        /// </summary>
        CY=2,
        /// <summary>
        /// 我创建的
        /// </summary>
        XS=3
    }

    /// <summary>
    /// 流程类型
    /// </summary>
    public enum WorkInfoType
    { 
        业务费用报销=1,

        非业务费用报销=2,

        非业务费用报销黄晖=3,

        其它业务支出=4,

        借款申请 = 5,

        提成发放=6,        

        固定资产申报=7

    }
    /// <summary>
    /// 流程环节类型
    /// </summary>
    public enum WorkItemType
    { 
        申请表单=1,

        财务审批=2,
        
        财务经理=3,

        总经理=4,
        
        出纳支付=5,
            
    }
}

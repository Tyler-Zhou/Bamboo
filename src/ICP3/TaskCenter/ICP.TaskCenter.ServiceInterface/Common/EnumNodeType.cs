namespace ICP.TaskCenter.ServiceInterface.Common
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 一般性节点
        /// </summary>
        General,
        /// <summary>
        ///  视图操作节点
        /// </summary>
        CodeLeaf,
        /// <summary>
        /// 视图操作类型节点
        /// </summary>
        OperateType,
        /// <summary>
        /// 人员父节点
        /// </summary>
        ParentStaff,
        /// <summary>
        /// 人员节点
        /// </summary>
        Staff,

        /// <summary>
        ///  总部
        /// </summary>
        Root ,

        /// <summary>
        ///     区域
        /// </summary>
        Section ,
        /// <summary>
        ///      公司
        /// </summary>
        Company ,
        /// <summary>
        ///    部门 
        /// </summary>
        Department,

        /// <summary>
        ///  组
        /// </summary>
        Group ,
        /// <summary>
        /// 协助同事
        /// </summary>
        AssistStaff

    }
}

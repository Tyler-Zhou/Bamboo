using ICP.Framework.CommonLibrary.Common;
using ICP.TaskCenter.ServiceInterface.Common;
using System;

namespace ICP.TaskCenter.ServiceInterface
{
    /// <summary>
    /// 视图节点类
    /// </summary>
    public class NodeInfo
    {
        /// <summary>
        /// 构造函数
        ///<remarks>HasChildren，HasFetchChildrenData设置默认值为false </remarks>
        /// </summary>
        public NodeInfo()
        {
            HasChildren = false;
            HasFetchChildrenData = false;
        }
        /// <summary>
        /// View ID
        /// </summary>

        public Guid Id { get; set; }
        /// <summary>
        /// 父节点Id
        /// <remarks>为null时代表为顶级节点</remarks>
        /// </summary>

        public Guid? ParentId { get; set; }
        /// <summary>
        /// 视图模板代码
        /// </summary>

        public string ViewCode { get; set; }
        /// <summary>
        /// 节点显示文本
        /// <remarks>根据当前语言获取</remarks>
        /// </summary>

        public string Caption { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>

        public bool HasChildren { get; set; }
        /// <summary>
        /// 是否已获取子节点
        /// </summary>

        public bool HasFetchChildrenData { get; set; }

        /// <summary>
        /// 用于查询Code
        /// </summary>
        public Guid SearchCode { get; set; }
        /// <summary>
        /// 锁定查询条件--口岸集合
        /// </summary>
        public string LockCompanyIDs { get; set; }

        /// <summary>
        ///  节点的拥有者ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>

        public NodeType NodeType
        {
            get;
            set;
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string AdvanceQueryString { get; set; }

        /// <summary>
        /// 层级数
        /// </summary>
        public int Hierarchy { get; set; }

        /// <summary>
        /// 数据库的ID
        /// </summary>
        public Guid SqlId { get; set; }
        /// <summary>
        /// 树节点的中文备注
        /// </summary>
        public string TooltiopCn { get; set; }
        /// <summary>
        /// 树节点的英文备注
        /// </summary>
        public string TooltiopEn { get; set; }
        /// <summary>
        /// 节点业务类型
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 返回数据条数
        /// </summary>
        public int TopCount { get; set; }

        /// <summary>
        /// 节点是否保留
        /// </summary>
        public bool Keep { get; set; }
        /// <summary>
        /// 基础查询条件
        /// </summary>
        public string BaseCriteria { get; set; }
    }
}

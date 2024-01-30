using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 接点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// And 组
        /// </summary>
        And,

        /// <summary>
        /// Or 组
        /// </summary>
        Or,

        /// <summary>
        /// Not组
        /// </summary>
        Not,

        /// <summary>
        /// 用户
        /// </summary>
        User,

        /// <summary>
        /// 部门
        /// </summary>
        Department,

        /// <summary>
        /// 组织结构
        /// </summary>
        Organization,

        /// <summary>
        /// 职位
        /// </summary>
        Job,

        /// <summary>
        /// 表单表单式
        /// </summary>
        FormExpression
    }
}

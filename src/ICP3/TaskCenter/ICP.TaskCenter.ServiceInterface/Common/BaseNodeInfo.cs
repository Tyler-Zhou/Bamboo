using System;
using System.Collections.Generic;

namespace ICP.TaskCenter.ServiceInterface.Common
{
    /// <summary>
    /// 操作管理视图基础节点信息
    /// </summary>
    public class BaseNodeInfo
    {
        /// <summary>
        /// 子节点集合
        /// </summary>
        private List<BaseNodeInfo> childNode = new List<BaseNodeInfo>();

        /// <summary>
        /// 构造函数
        ///<remarks>HasChildren，HasFetchChildrenData设置默认值为false </remarks>
        /// </summary>
        public BaseNodeInfo()
        {
            HasChildren = false;
            HasFetchChildrenData = false;
        }
        /// <summary>
        /// Id
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
        /// 节点类型
        /// </summary>
        public NodeType NodeType
        {
            get;
            set;
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public BaseNodeInfo ParentNode
        {
            get;
            set;
        }
        
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<BaseNodeInfo> ChildNodes
        {
            get { return childNode; }
            set { childNode = value; }
        }
        /// <summary>
        /// 重载Equals方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Id == ((BaseNodeInfo)obj).Id;
        }

        /// <summary>
        /// 重载==操作符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(BaseNodeInfo a, BaseNodeInfo b)
        {
            return a.Id == b.Id;
        }
        /// <summary>
        /// 重载!=操作符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(BaseNodeInfo a, BaseNodeInfo b)
        {
            return a.Id != b.Id;
        }
        /// <summary>
        /// 得到子节点信息
        /// </summary>
        /// <returns></returns>
        public void GetChildNodes()
        {
            HasFetchChildrenData = true;
            childNode = AddChildNodes();
            HasChildren = childNode.Count > 0 ? true : false;
            // return HasChildren;
        }

        /// <summary>
        /// 获取几层的孩子节点结合
        /// </summary>
        /// <param name="parentNode">上级节点</param>
        /// <param name="depth">节点层数(默认为1)</param>
        /// <returns></returns>
        public void GetChildNodes(BaseNodeInfo parentNode, int depth)
        {
            try
            {
                parentNode.GetChildNodes();
                if (parentNode.ChildNodes.Count > 0 && depth > 1)
                {
                    depth--;
                    foreach (BaseNodeInfo node in parentNode.ChildNodes)
                    {
                        GetChildNodes(node, depth);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 添加子节点集合
        /// </summary>
        /// <returns></returns>
        public virtual List<BaseNodeInfo> AddChildNodes()
        {
            return new List<BaseNodeInfo>();
        }

    }
}

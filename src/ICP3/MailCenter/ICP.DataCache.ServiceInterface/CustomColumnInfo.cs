using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache.ServiceInterface
{


    /// <summary>
    /// 客户自定义列信息类
    /// </summary>
    [Serializable]
    public class CustomColumnInfo
    {
        private string name;
        /// <summary>
        /// 列名
        /// </summary>
        public string Name
        {
            get
            {

                return name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ICPException("列名不允许为空!");
                }
                name = value;

            }
        }
        /// <summary>
        /// 可见位置索引
        /// </summary>
        public int VisibleIndex { get; set; }
        private int width = 100;
        /// <summary>
        /// 宽度(默认为100)
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
    
        private bool visible = true;
        /// <summary>
        /// 可见性(默认为可见)
        /// </summary>
        public bool Visible
        {

            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        /// <summary>
        /// 绝对索引
        /// </summary>
        public int AbsoluteIndex
        {
            get;
            set;
        }
        private ColumnFixedStyle fixedStyle = ColumnFixedStyle.None;
        /// <summary>
        /// 固定方式
        /// </summary>
        public ColumnFixedStyle Fixed
        {
            get
            {
                return fixedStyle;
            }
            set {
                fixedStyle = value;
            }
        }
        private SortOrder sortOrder = SortOrder.None;
        /// <summary>
        /// 排序规则
        /// </summary>
        public SortOrder SortOrder
        {
            get {
                return sortOrder;
            }

            set
            {
                sortOrder = value;
            }
        }
    }
    public enum ColumnFixedStyle
    {
        None = 0,
        Left = 1,
        Right = 2,
    }
    /// <summary>
    /// 排序规则
    /// </summary>
    public enum SortOrder
    {
        None = 0,
        Ascending = 1,
        Descending = 2,

    }
}

using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Business.Common.UI.Document
{
    /// <summary>
    ///自定义枚举实体
    /// </summary>
    public partial class CustomEnumInfo : ChildrenEnumInfo
    {
        public CustomEnumInfo()
        {
            this.Checked = false;
        }

        public CustomEnumInfo(string cnCaption, string enCaption)
        {
            this.cCaption = cnCaption;
            this.eCaption = enCaption;
            this.Checked = false;
        }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 英文字幕
        /// </summary>
        public string eCaption { get; set; }
        /// <summary>
        /// 中文字幕
        /// </summary>
        public string cCaption { get; set; }
        /// <summary>
        /// 是否包含子节点
        /// </summary>
        public bool HasChildNodes { get; set; }
        /// <summary>
        /// 是否被选中（附加属性）
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid ParentID { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType Type { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<ChildrenEnumInfo> ChildrenNodes { get; set; }

    }
    /// <summary>
    /// 子节点枚举实体类
    /// </summary>
    public partial class ChildrenEnumInfo
    {
        /// <summary>
        /// 主键标识（现没有用到）
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 字幕
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 中文提示说明
        /// </summary>
        public string Ctip { get; set; }
        /// <summary>
        /// 英文提示说明
        /// </summary>
        public string Etip { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType Type { get; set; }

    }

}

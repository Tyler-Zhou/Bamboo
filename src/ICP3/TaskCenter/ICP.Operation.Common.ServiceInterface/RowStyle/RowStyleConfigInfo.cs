using System.Drawing;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 列表样式配置类
    /// </summary>
    public class RowStyleConfigInfo
    {
        /// <summary>
        /// 节点Code
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        ///  判断是行改变颜色还是单元格改变颜色(Bool)
        /// </summary>
        public bool RowStyle { get; set; }
        /// <summary>
        /// 判断条件 (必须是Bool)
        /// </summary>
        public string JudgeConditions { get; set; }
        /// <summary>
        /// 条件为真时颜色
        /// </summary>
        public Color TrueColor { get; set; }

        /// <summary>
        /// 条件为假时颜色
        /// </summary>
        public Color FalseColor { get; set; }
        /// <summary>
        /// 需要改变的字段名称
        /// </summary>
        public string Field { get; set; }

        public string ToolTipCn { get; set; }
        public string ToolTipEn { get; set; }

        /// <summary>
        /// 是改变字体颜色还是改变背景色
        /// </summary>
        public bool Font { get; set; }
    }
}

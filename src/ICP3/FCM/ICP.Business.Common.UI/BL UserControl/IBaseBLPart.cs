using ICP.Framework.CommonLibrary.Common;

namespace ICP.Business.Common.UI.BL
{

    /// <summary>
    /// 提单列表接口
    /// </summary>
    public interface IBaseBLPart
    {
        /// <summary>
        /// 设置数据源
        /// </summary>
        object DataSource { get; set; }
        /// <summary>
        /// 列表选择行的业务信息
        /// </summary>
        BusinessOperationContext CurrentContext { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        string TemplateCode { get; set; }
    }
}

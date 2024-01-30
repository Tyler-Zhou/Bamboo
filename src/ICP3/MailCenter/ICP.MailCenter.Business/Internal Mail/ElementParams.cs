using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 内部沟通面板参数类
    /// </summary>
    public partial class ElementParams
    {
        public ElementParams(BusinessOperationContext operationContext, BindingSource source, bool editable)
        {
            this.OperationContext = operationContext;
            this.Source = source;
            this.Editable = editable;   
        }  

        /// <summary>
        /// 业务上下文
        /// </summary>
        public BusinessOperationContext OperationContext { get; set; }
        /// <summary>
        /// 业务数据源
        /// </summary>
        public BindingSource Source { get; set; }
        /// <summary>
        /// 内部沟通面板是否可以编辑
        /// </summary>
        public bool Editable { get; set; }

        public object Data { get; set; }
    }
}

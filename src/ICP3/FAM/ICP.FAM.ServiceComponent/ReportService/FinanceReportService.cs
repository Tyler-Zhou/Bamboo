using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceComponent
{
    /// <summary>
    /// 财务服务
    /// </summary>
    public partial class FinanceReportService : IFinanceReportService
    {
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            { 
                return  ApplicationContext.Current.IsEnglish;
            }
        }
    }
}

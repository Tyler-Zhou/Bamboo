using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.ServiceComponent
{
    public partial class CommonService : ICommonService
    {
        public bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }
    }
}

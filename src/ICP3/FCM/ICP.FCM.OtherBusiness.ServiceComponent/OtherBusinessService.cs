using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.FCM.OtherBusiness.ServiceComponent
{
    /// <summary>
    /// 其他业务服务
    /// </summary>
    partial class OtherBusinessService : IOtherBusinessService
    {
        #region Fields
        /// <summary>
        /// 初始化服务
        /// </summary>
        IFrameworkInitializeService FrameworkInitializeService;
        /// <summary>
        /// FCM公共服务
        /// </summary>
        IFCMCommonService _FCMCommonService;
        #endregion

        #region Init Service
        /// <summary>
        /// 其他业务服务
        /// </summary>
        /// <param name="frameworkInitializeService"></param>
        public OtherBusinessService(IFrameworkInitializeService frameworkInitializeService, IFCMCommonService FCMCommonService)
        {
            FrameworkInitializeService = frameworkInitializeService;
            _FCMCommonService = FCMCommonService;
        }
        #endregion

        #region Property
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }
        #endregion
    }
}

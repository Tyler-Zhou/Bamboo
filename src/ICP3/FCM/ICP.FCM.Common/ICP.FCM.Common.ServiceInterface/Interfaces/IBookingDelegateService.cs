using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// CSP业务委托
    /// </summary>
    [ServiceInfomation("CSP业务委托")]
    [ServiceContract]
    public interface IBookingDelegateService
    {
        #region 查询舱单委托列表
        /// <summary>
        /// 查询舱单委托列表
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns>舱单委托显示列表</returns>
        [FunctionInfomation("查询舱单委托列表")]
        [OperationContract]
        List<BookingDelegateList> GetBookingDelegateListBySearch(SearchParameterBookingDelegate searchParameter);
        #endregion

        #region 下载CSP舱单
        /// <summary>
        /// 下载CSP舱单
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns>舱单委托列表</returns>
        [FunctionInfomation("下载CSP舱单")]
        [OperationContract]
        List<BookingDelegate> DownloadBookingDelegate(SearchParameterDownloadBookingDelegate searchParameter);
        #endregion

        #region 获取舱单委托列表
        /// <summary>
        /// 获取舱单委托列表
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns>舱单委托列表</returns>
        [FunctionInfomation("获取舱单委托列表")]
        [OperationContract]
        List<BookingDelegate> GetBookingDelegateList(SearchParameterBookingDelegate searchParameter);
        #endregion

        #region 保存舱单委托列表
        /// <summary>
        /// 保存舱单委托列表
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation("保存CSP委托列表")]
        [OperationContract]
        ManyResult SaveBookingDelegateList(SaveRequestBookingDelegate saveRequest);

        #endregion

        #region 保存舱单委托列表
        /// <summary>
        /// 删除CSP委托
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation("删除CSP委托")]
        [OperationContract]
        void RemoveBookingDelegate(SaveRequestBookingDelegate saveRequest);

        #endregion
    }
}

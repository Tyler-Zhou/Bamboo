#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/19 星期四 10:53:31
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 电商业务列表
    /// </summary>
    [ServiceInfomation("电商业务列表")]
    [ServiceContract]
    public interface IECommerceService
    {
        /// <summary>
        /// 获取已关联业务
        /// </summary>
        /// <param name="qcParameter">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ECommerceList> GetAssociatedECommerceList(QueryCriteria4ECommerce qcParameter);

        /// <summary>
        /// 获取自拼电商业务列表
        /// </summary>
        /// <param name="qcParameter">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ECommerceList> GetSelfSpellingECommerceList(QueryCriteria4ECommerce qcParameter);

        /// <summary>
        /// 改变关联业务
        /// </summary>
        /// <param name="saveRequest">保存关联的对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeAssociationBusiness(ECommerceSaveRequest saveRequest);
    }
}

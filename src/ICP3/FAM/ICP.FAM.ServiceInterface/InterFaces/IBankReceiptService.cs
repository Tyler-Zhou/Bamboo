using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.ServiceInterface.InterFaces
{
    /// <summary>
    /// 水单服务类
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBankReceiptService
    {
        #region 获得水单表信息

        /// <summary>
        /// 根据查询条件获得水单表信息
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankReceiptListInfo> GetBankReceiptList(BankReceiptSearchParameter searchParameter);

        #endregion

        #region 根据ID获取水单信息
        /// <summary>
        /// 根据ID获取水单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BankReceiptInfo GetBankReceiptInfo(Guid id, bool isEnglish);
        #endregion

        #region 保存水单信息
        /// <summary>
        /// 保存水单信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveBankReceiptInfo(BankReceiptSaveRequest saveRequest);
        #endregion

        #region 根据ID作废水单信息
        /// <summary>
        /// 根据ID作废水单信息
        /// </summary>
        /// <param name="id">水单列表ID</param>
        /// <param name="isCancel">是否作废(True为作废,False为激活)</param>
        /// <param name="cancelByID">操作人ID</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult CancelBankReceiptList(
                         Guid id,
                         bool isCancel,
                         Guid cancelByID,
                         DateTime? updateDate,
                          bool isEnglish);
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="auditorById">操作人</param>
        /// <param name="isCheck">是否审核:True为审核,False为取消审核</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult AuditorBankReceipt(Guid[] ids,
            DateTime?[] updateDates,
            Guid auditorById,
            bool isCheck);
        #endregion

    }
}
using System;

using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 放货列表interface
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IReleaseRCService
    {
        /// <summary>
        /// 获取放货列表
        /// </summary>
        /// <param name="RCCompany"></param>
        /// <param name="releaseBLState"></param>
        /// <param name="releaseType"></param>
        /// <param name="blNo"></param>
        /// <param name="ctnNo"></param>
        /// <param name="consignee"></param>
        /// <param name="vessel"></param>
        /// <param name="voyageNo"></param>
        /// <param name="releaseBeginTime"></param>
        /// <param name="releaseEndTime"></param>
        /// <param name="releaseECBeginTime"></param>
        /// <param name="releaseRCEndTime"></param>
        /// <param name="IsEnglish"></param>
        /// <param name="dataPageInfo"></param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        ReleasePageList GetReleaseRCList(Guid[] RCCompany
                                            , ReleaseRCState? releaseBLState
                                            , ReleaseType? releaseType
                                            , string blNo
                                            , string ctnNo
                                            , string consignee
                                            , string vessel
                                            , string voyageNo
                                            , DateTime? releaseBeginTime
                                            , DateTime? releaseEndTime
                                            , DateTime? releaseECBeginTime
                                            , DateTime? releaseRCEndTime
                                            , bool IsEnglish
                                            , DataPageInfo dataPageInfo);
        /// <summary>
        /// 是否已收到放单通知
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeByID"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       ManyResult ChangeReleaseRCState(
           Guid[] ids,
           Guid changeByID,
           DateTime?[] updateDate);

        /// <summary>
        /// 标识是否已放货
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsValid"></param>
        /// <param name="changeByID"></param>
        /// <param name="updateDate"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult ChangeReleaseRC(
             Guid id,
            bool IsValid,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish);
        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <param name="updateDate"></param>
        /// <param name="savaByID"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult SaveReleaseRemark(
             Guid id,
            string remark,
            DateTime? updateDate,
            Guid savaByID,
            bool IsEnglish);

        /// <summary>
        ///转移放货公司
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="NewCompanyID">新公司</param>
        /// <param name="savaByID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
       [FunctionInfomation]
       [OperationContract]
           SingleResult SaveReleaseCompany(
             Guid id,
            Guid NewCompanyID,
            DateTime? updateDate,
            Guid savaByID,
            bool IsEnglish);
    }
}

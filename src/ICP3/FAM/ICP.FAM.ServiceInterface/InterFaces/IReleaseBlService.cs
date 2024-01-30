using System;
using System.Collections.Generic;

using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 放单用到的方法
    /// 创建时间：2011-07-11 15:44
    /// 作者：熊中方
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IReleaseBlService
    {
        /// <summary>
        /// 获取放单列表
        /// </summary>
        /// <param name="CompanyIds"></param>
        /// <param name="releaseBLSearchStatue"></param>
        /// <param name="releaseRCSearchStatue"></param>
        /// <param name="applyReleaseSearchStatue"></param>
        /// <param name="receiveRCSearchStatue"></param>
        /// <param name="ReceiveOBL"></param>
        /// <param name="releaseType">放单状态(正本 1 ,电放 2)可为空</param>
        /// <param name="blNo">提单号</param>
        /// <param name="soNo"></param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="customerName">客户</param>
        /// <param name="vessel">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="ChangedReleaseType"></param>
        /// <param name="applyBeginTime">申请放单时间</param>
        /// <param name="applyEndTime">申请放单时间</param>
        /// <param name="releaseBeginTime">放单时间</param>
        /// <param name="releaseEndTime">放单时间</param>
        /// <param name="etdBeginTime">ETD</param>
        /// <param name="etdEndTime">ETD</param>
        /// <param name="acceptBeginDate"></param>
        /// <param name="acceptEndDate"></param>
        /// <param name="etaBeginTime"></param>
        /// <param name="etaEndTime"></param>
        /// <param name="isAirandOther"></param>
        /// <param name="isMonthlyPay"></param>
        /// <param name="isTrustRelease"></param>
        /// <param name="isSWBRelease"></param>
        /// <param name="isOverSeaMBL"></param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns>ReleaseBLList</returns>
        [FunctionInfomation]
        [OperationContract]
        ReleasePageList GetReleaseBLListByList(Guid[] CompanyIds
                                            , ReleaseBLSearchStatue releaseBLSearchStatue
                                            , ReleaseRCSearchStatue releaseRCSearchStatue
                                            , ApplyReleaseSearchStatue applyReleaseSearchStatue
                                            , ReceiveRCSearchStatue receiveRCSearchStatue
                                            , int ReceiveOBL
                                            , ReleaseType? releaseType
                                            , string blNo
                                            , string soNo
                                            , string ctnNo
                                            , string operationNo
                                            , string customerName
                                            , string vessel
                                            , string voyageNo
                                            , int ChangedReleaseType
                                            , DateTime? applyBeginTime
                                            , DateTime? applyEndTime
                                            , DateTime? releaseBeginTime
                                            , DateTime? releaseEndTime
                                            , DateTime? etdBeginTime
                                            , DateTime? etdEndTime
                                            , DateTime? acceptBeginDate
                                            , DateTime? acceptEndDate
                                            , DateTime? etaBeginTime
                                            , DateTime? etaEndTime
                                            , bool isMonthlyPay
                                            , bool isTrustRelease
                                            , bool isSWBRelease
                                            , bool isOverSeaMBL
                                            , bool isAirandOther
                                            , DataPageInfo dataPageInfo);

        /// <summary>
        /// 获取未放单列表
        /// </summary>
        /// <returns>ReleaseAndArList</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ReleaseAndArList> GetReleaseAndArList(Guid? OperationID);

        /// <summary>
        /// 获取放单列表
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns>ReleaseBLList</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ReleaseBLList> GetReleaseBLListByIds(Guid[] ids);

        /// <summary>
        /// 保存放单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="customerId">客户</param>
        /// <param name="customerContact">客户联系人</param>
        /// <param name="type">类型(正本 1 ,电放 2)</param>
        /// <param name="state">状态(已创建=1、已签收=2、已放单=3、已接收=4)</param>
        /// <param name="telexNo">电放号</param>
        /// <param name="expressOrderNo">快递单号</param>
        /// <param name="remark">备注</param>
        /// <param name="savebyID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveReleaseBLInfo(
            Guid id,
            Guid customerId,
            string customerContact,
            ReleaseType type,
            ReleaseBLState state,
            string telexNo,
            string expressOrderNo,
            string remark,
            Guid savebyID,
            DateTime? updateDate,
            DateTime? blUpdateDate);

        /// <summary>
        /// 根据客户从历史放单找出客户联系人
        /// </summary>
        /// <param name="customerID">customerID</param>
        /// <returns>ContactID</returns>
        [FunctionInfomation]
        [OperationContract]
        string GetReleaseBLRecentlyCustomerContact(Guid customerID);

        /// <summary>
        /// 改变放单状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">电放号或快递号</param>
        /// <param name="state">状态(已创建=1、已签收=2、已放单=3、已接收=4)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <param name="agentId">发邮件用：代理ID</param>
        /// <param name="blNo">发邮件用：提单号</param>
        /// <param name="name">发邮件用：当前用户名</param>
        /// <param name="FilerEmail">发邮件用：客服Email</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeReleaseBLState(
            Guid id,
            ReleaseBLState state,
            bool rbld,
            string no,
            Guid changeByID,
            DateTime? updateDate,
            DateTime? blUpdateDate,
            bool isCancelRelease,
            Guid? agentId,
            string blNo,
            string name,
            string FilerEmail,
            string podFilerEmail,
            bool isCRelease,
            bool isOverSeaMBL,
            Guid operationID);

        /// <summary>
        /// 改变放单类型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">类型(正本 1 ,电放 2)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeReleaseBLType(
            Guid id,
            ReleaseType type,
            Guid changeByID,
            DateTime? updateDate,
            DateTime? blUpdateDate);

        /// <summary>
        /// 改变放单类型(放单管理专用)
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">状态(正本 1 ,电放 2)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <param name="state">发邮件用：状态</param>
        /// <param name="agentId">发邮件用：代理ID</param>
        /// <param name="blNo">发邮件用：提单号</param>
        /// <param name="name">发邮件用：当前用户名</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeReleaseTypeToTel(
            Guid id,
            ReleaseType type,
            Guid changeByID,
            DateTime? updateDate,
            DateTime? blUpdateDate,
            ReleaseBLState state,
            string filerEmail,
            Guid? agentId,
            string blNo,
            string name,
            Guid operationID);

        /// <summary>
        /// 申请放单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="applyDate">applyDate</param>
        /// <param name="changeByID">changeByID</param>
        /// <param name="updateDate">updateDate</param>
        /// <returns>返回SingleResult  "ID", "UpdateDate"</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ApplyReleaseBL(Guid id
                                        , DateTime? applyDate
                                        , Guid changeByID
                                        , DateTime? updateDate);

        /// <summary>
        /// 获取代理指定的联系人
        /// </summary>
        /// <param name="AgentID">代理</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ContactList GetContactListOfAgent(Guid? AgentID);

        /// <summary>
        /// 保存代理指定的联系人
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="agentID">代理</param>
        /// <param name="contactEmail">联系人邮箱</param>
        /// <param name="saveByID">操作人id</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void SaveContactListOfAgent(Guid id
                                            , Guid? agentID
                                            , string contactEmail
                                            , int emailsendtime
                                             , Guid saveByID);

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="sendTo">接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        [FunctionInfomation]
        [OperationContract]
        void SendEmail(string sendTo, string subject, string content);

        /// <summary>
        /// 更改接收正本状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="state">0 未收到 1 收到</param>
        /// <param name="saveByID">执行人</param>
        [FunctionInfomation]
        [OperationContract]
        void ChangeRecState(Guid id, int state, Guid saveByID, DateTime? UpdateDate);

         /// </summary>
        /// 保存特殊放单
        /// </summary>
        /// <param name="releaseid">放单ID</param>
        /// <param name="saveByID"></param>
        [FunctionInfomation]
        [OperationContract]
        void SaveExRelease(Guid releaseid, Guid savebyid);

        /// </summary>
        /// 检查特殊放单
        /// </summary>
        /// <param name="releaseid">放单ID</param>
        [FunctionInfomation]
        [OperationContract]
        List<ExRelease> CheckExRelease(Guid releaseid);

        /// <summary>
        /// 发送催款邮件
        /// </summary>
        /// <param name="waitsend">催款实体类</param>
        /// <param name="iscc">是否抄送</param>
        /// <param name="isAuto">是否自动发送</param>
        [FunctionInfomation]
        [OperationContract]
        string SendAREmail(ReleaseAndArList waitsend, bool iscc, bool isAuto);
    }
}

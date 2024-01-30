using ICP.DataCache.ServiceInterface;

namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using Framework.CommonLibrary.Attributes;
    using System.ServiceModel;
    using DataObjects;
    using System.Collections.Generic;
    using Framework.CommonLibrary.Common;
    using System.Data;
    using ICP.FileSystem.ServiceInterface;

    /// <summary>
    /// 业务代理服务接口
    /// </summary>
    [ServiceInfomation("业务代理服务接口")]
    [ServiceContract]
    public interface IOperationAgentService
    {
        /// <summary>
        /// 通过业务ID得到代理信息
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OperationAgentInfo GetOperationAgentInfo(Guid operationId);

        /// <summary>
        /// 通过港前业务ID 得到港后业务ID
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Guid GetOceanimportBusinessID(Guid operationId);

        /// <summary>
        /// 通过业务ID得到业务文档分发数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DocumentDispatchContainerObjects GetOperationDocumentDispatchData(Guid operationId);

        /// <summary>
        /// 通过业务代理信息
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DocumentDispatchInfo GetDocumentDispatchInfo(Guid operationId);

        /// <summary>
        /// 保存分文档信息
        /// </summary>
        /// <param name="ContainerObjects"></param>
        /// <param name="DispatchFileIDs"></param>
        /// <param name="FileIDs"></param>
        /// <param name="FileUpdateDates"></param>
        [FunctionInfomation]
        [OperationContract]
        bool SaveOperationDocumentDispatchData(DocumentDispatchContainerObjects ContainerObjects, Guid?[] DispatchFileIDs, Guid[] FileIDs, DateTime?[] FileUpdateDates);

        /// <summary>
        /// 港前分发文件
        /// </summary>
        /// <param name="OceanBookingID">港前业务ID</param>
        /// <param name="DispatchFlieType">(1:分所有文件,2:只分了账单)</param>
        /// <param name="FileIDs">需要指派的文件</param>
        /// <param name="SaveByID">执行用户</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool DicpatchFilesForOE(Guid OceanBookingID, byte DispatchFlieType, Guid[] FileIDs, Guid SaveByID);


        /// <summary>
        /// 空运港前分发文件
        /// </summary>
        /// <param name="AirBookingID">空运业务ID</param>
        /// <param name="DispatchFlieType">(1:分所有文件,2:只分了账单)</param>
        /// <param name="FileIDs">需要指派的文件</param>
        /// <param name="SaveByID">执行用户</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool DicpatchFilesForAir(Guid AirBookingID, byte DispatchFlieType, Guid[] FileIDs, Guid SaveByID);


        /// <summary>
        /// 港后分发文件
        /// </summary>
        /// <param name="OceanBookingID">港前业务ID</param>
        /// <param name="SaveByID">执行用户</param>
        [FunctionInfomation]
        [OperationContract]
        bool DicpatchFilesForOI(Guid OceanBookingID, Guid SaveByID);
        
        /// <summary>
        /// 空运港后分发文件
        /// </summary>
        /// <param name="AIBookingID">空运港后业务ID</param>
        /// <param name="SaveByID">执行用户</param>
        [FunctionInfomation]
        [OperationContract]
        bool DicpatchFilesForAI(Guid AIBookingID, Guid SaveByID);


        /// <summary>
        /// 重新分发文件
        /// </summary>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        [FunctionInfomation]
        [OperationContract]
        bool CopyDispatchFileLogInfo(Guid DispatchFileLogID);


        /// <summary>
        /// 分文件签收记录列表
        /// 2013-09-03 joe
        /// </summary>
        /// <param name="BookingID">业务ID</param>
        /// <param name="Type">操作类型（1为海出2为海进）</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<HistoryOceanRecord> GetHistoryOceanRecord(Guid BookingID, int Type);

        /// <summary>
        /// 分文件和修订状态
        /// 2013-09-13 joe
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationtype">业务类型</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetDispatchAndReviseStateForOperationType")]
        DocumentState GetDispatchAndReviseState(Guid operationID, OperationType operationtype);

        /// <summary>
        /// 分文件和修订状态
        /// 2013-09-13 joe
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DocumentState GetDispatchAndReviseState(Guid operationID);

        /// <summary>
        /// 分文件和修订状态账单正式使用时间
        /// 2013-09-17 joe
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DateTime GetStartDateForReviseAgentBill();

        /// <summary>
        /// 判定业务是否在已下载但还没有分发文件的状态，在这个状态返回1，否则返回0
        /// 2013-10-08 joe
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool GetIsAcceptDispatch(Guid operationID);

        /// <summary>
        /// 得到海进业务的创建时间
        /// 2013-10-10 joe
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DateTime GetCreateDateOIBusiness(Guid OIBookingID);

        /// <summary>
        /// 业务是否下载过
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool BusinessIsDownLoad(Guid operationID);

        /// <summary>
        /// 拒签分发文件或账单修订
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <param name="strReason">拒签原因</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool RejectDispatchOrRevise(Guid OperationID, OperationType OperationType, string strReason);

        /// <summary>
        /// 业务的代理，文件员，客服信息   joe
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BusinessAgentAndCustomInfoObject GetOperationAgentNameAndCustomer(Guid OperationID, OperationType OperationType);

        /// <summary>
        /// 得到当前用户拥有的操作节点类型
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OperationType> GetUserOperationViewType(Guid userId);

        /// <summary>
        /// 得到当前用户拥有的操作节点类型
        /// </summary>
        /// <param name="OceanBookingID">海出操作ID</param>
        /// <param name="OIBookingID">海出操作ID</param>
        /// <param name="OperationType">操作类型</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ProfitCompare GetProfitCompare(Guid OceanBookingID, Guid OIBookingID, OperationType OperationType);

        /// <summary>
        /// 验证两边网络是否正常，数据是否完全同步(海进使用)
        /// </summary>
        /// <param name="OceanBookingID"></param>
        /// <param name="IsRelease"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DataSet CompareOceanBookingCheckSum4LACo(Guid OceanBookingID, bool IsRelease);

        /// <summary>
        /// 验证业务是否可以分发文件
        /// </summary>
        /// <param name="operationID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool VerifyDispatch(Guid operationID);


        ///<summary>
        /// 获取一个用户的分文件记录
        /// </summary>
        /// <param name="IsTransTo"></param>
        /// <param name="State"></param>
        /// <param name="userid">用户</param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<DispathLogData> GetDispatchFileLogForUser(bool? IsTransTo, string State, Guid userid, DateTime? StartDate, DateTime? EndDate);

        ///<summary>
        /// 获取一个用户的未分文件个数
        /// </summary>
        /// <param name="userid">用户</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        int uspGetNotDispatchedLogForUser(Guid userid);

        /// <summary>
        /// 获取一个业务最新的分文件记录
        /// </summary>
        /// <param name="OperationID">业务号ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DispathLogData GetDispatchFileLogByOperation(Guid OperationID);

        /// <summary>
        /// 获取一个业务分文件状态
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        int GetDispatchState(Guid OperationID);

             /// <summary>
        /// 获取一个业务分文件状态
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        int UspGetDispatchLogState(Guid OperationID);

        /// <summary>
        /// 获取一个业务分发的文件
        /// </summary>
        /// <param name="LogsID">日志ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<DispatchFile> GetDispatchFiles(Guid LogsID);

        #region 自动分发文件
        /// <summary>
        /// 通过UserID获取未分发文档记录
        /// </summary>
        /// <returns>分发文件记录</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DispathLogData> Auto_GetNoDispatchLog();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logID"></param>
        /// <param name="operationID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DispatchEntityObjects Auto_GetDispatchEntityObjectsByOperationID(Guid logID, Guid operationID);

        /// <summary>
        /// 分发：分发文档及其数据
        /// </summary>
        /// <param name="dispatchEntity">分发文档实体</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool Auto_DispatchEntity(DispatchEntityObjects dispatchEntity);

        /// <summary>
        /// 更新分发文档状态
        /// </summary>
        /// <param name="logID">业务ID</param>
        /// <param name="timeSpan">时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool UpdateDispatchLogState(Guid logID,int timeSpan);
        /// <summary>
        /// 自动分发文件
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void AutoDispathFiles(); 
        #endregion
    }
}

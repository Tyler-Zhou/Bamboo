using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;

namespace ICP.FAM.ServiceInterface.InterFaces
{
    /// <summary>
    /// 凭证服务类
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ILedgerService
    {

        #region 获得凭证联合表信息

        /// <summary>
        /// 根据查询条件获得凭证联合表信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<LedgerListInfo> GetLedgerList(Guid[] companyIDs, 
                            int? minVoucherSeqNo, 
                            int? maxVoucherSeqNo,
                            string refNo, 
                            LedgerSearchAmountType amountType,
                            decimal? minAmount,
                            decimal? maxAmount,
                            string remark,
                            Guid? createBy, 
                            Guid? auditorID, 
                            Guid? cashierID, 
                            LedgerMasterType[] typeList, 
                            LedgerMasterStatus status, 
                            bool? isValid, 
                            DateTime? fromDate, 
                            DateTime? todate);

        /// <summary>
        /// 根据ID获得凭证信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<LedgerListInfo> GetLedgerListByID(Guid[] id);
        #endregion

        #region 获得凭证主表信息
        /// <summary> 
        /// 获得凭证主表信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        LedgerMasters GetLedgerMastersInfo(Guid id, bool isEnglish);
        #endregion

        #region 作废凭证列表
        /// <summary>
        /// 作废凭证列表
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="IsValid">是否有效</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        void CancelLedgerList(
                         Guid id,
                         bool IsValid,
                         Guid changeByID,
                         DateTime? updateDate,
                          bool isEnglish);
        #endregion

        #region 出纳签字/取消
        /// <summary>
        /// 出纳签字/取消
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <param name="status">状态</param>
        /// <param name="cashierID">出纳员ID</param>
        /// <param name="updateDates">更新时间s-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult CashierCheckedLedgerList(Guid[] ids, LedgerMasterStatus status, Guid cashierID, DateTime?[] updateDates, bool isEnglish);
        #endregion

        #region 财务主管签字/取消
        /// <summary>
        /// 财务主管签字/取消
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <param name="status">状态</param>
        /// <param name="financeManagerID">修改人</param>
        /// <param name="updateDates">更新时间s-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult FinanceManagerCheckedLedgerList(Guid[] ids, LedgerMasterStatus status, Guid financeManagerID, DateTime?[] updateDates, bool isEnglish);
        #endregion

        #region 审核/取消审核凭证
        /// <summary>
        /// 审核/取消审核凭证
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <param name="status">状态</param>
        /// <param name="auditID">修改人</param>
        /// <param name="updateDates">更新时间s-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult AduitLedgerList(Guid[] ids, LedgerMasterStatus status, Guid auditID, DateTime?[] updateDates, bool isEnglish);
        #endregion

        #region 凭证记账/取消凭证记账
        /// 凭证记账/取消凭证记账
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">状态</param>
        /// <param name="accountingID">记账人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult KeepAccountsLedgerList(Guid[] ids, LedgerMasterStatus status, Guid accountingID, DateTime?[] updateDates, bool isEnglish);
        #endregion

        #region 保存凭证列表信息
        /// <summary>
        /// 保存凭证列表信息
        /// </summary>
        /// <param name="hdObj">主表对象</param>
        /// <param name="dtlList">明细列表</param>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveLedgerInfo(LedgerSaveRequest saveRequest);
        #endregion

        #region 获得打印凭证数据
        /// <summary>
        /// 获得打印凭证数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        PrintLedgerMasterReports GetPrintLedgerReportDate(Guid id, bool isEnglish);
        #endregion

        #region 获得批量打印凭证数据
        /// <summary>
        /// 获得批量打印凭证数据
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PrintLedgerMasterReports> GetBulkPrintLedgerReportDate(Guid[] ids, bool isEnglish);
        #endregion

        #region 根据参考ID获得打印数据
        /// <summary>
        /// 根据参考ID获得打印数据
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        PrintLedgerMasterReports GetPrintLedgerReportDateByRefID(Guid refID, bool isEnglish);
        #endregion

        #region 删除凭证明细
        /// <summary>
        /// 删除凭证明细
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        /// <param name="removeById"></param>
        /// <param name="isEnglish"></param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveLedgerInfo(Guid[] ids, DateTime?[] updateDates, Guid removeById, bool isEnglish);
        #endregion

        #region 保存汇率
        /// <summary>
        /// 保存汇率
        /// </summary>
        /// <param name="request"></param>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveRateList(AdjustRateSaveRequest request);
        #endregion

        #region 获得用友与ICP关联的数据
        /// <summary>
        /// 获得用友与ICP关联的数据
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<UFCode2ICP> GetUFCode2ICPList(Guid[] CompanyIDs);
        #endregion

        #region 保存期初余额
        /// <summary>
        /// 保存期初余额
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveBeginBalance(BeginBalanceSaveRequest saveRequest);
        /// <summary>
        /// 删除期初余额
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveBeginBalance(Guid[] ids, DateTime?[] updateDates, Guid removeByID);
        #endregion

        #region 获得期初余额
        /// <summary>
        ///获得指定公司的期初余额
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BeginBalances> GetBeginBalance(Guid companyID,int year);
        /// <summary>
        /// 获得指定科目的期初余额数据
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="glID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BeginBalances> GetGLBeginBalance(Guid companyID, int year,Guid glID);
        #endregion

        #region 生成账单凭证
        /// <summary>
        /// 生成账单凭证
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="generateBy">生成人</param>
        [FunctionInfomation]
        [OperationContract]
        void GenerateBillVoucher(Guid companyID, DateTime startDate, DateTime endDate, Guid generateBy);
        #endregion

        #region 先产生汇兑损益凭证，再进行会计关帐
        /// <summary>
        /// 先产生汇兑损益凭证，再进行会计关帐
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="accountingDate"></param>
        /// <param name="saveByID"></param>
        [FunctionInfomation]
        [OperationContract]
        void SaveAccountingData(Guid companyID, DateTime accountingDate, Guid saveByID);
        #endregion

        #region 产生汇兑损益
        /// <summary>
        /// 产生汇兑损益
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="accountingDate"></param>
        /// <param name="saveByID"></param>
        [FunctionInfomation]
        [OperationContract]
        void AdjustRateForVoucher(Guid companyID, DateTime accountingDate, Guid saveByID);
        #endregion

        #region 关帐期内亏损的单，但末申请过亏损流程的单
        [FunctionInfomation]
        [OperationContract]
        void SendDeficitOperationEMail(Guid companyID, DateTime formDate, DateTime toDate);
        #endregion

        #region 查询凭证
        /// <summary>
        /// 查询凭证
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="no"></param>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        LedgerMasters GetLedgerMastersByNo(Guid companyID, string no,DateTime date, VoucherSearchType type);
        #endregion

        /// <summary>
        /// 解锁凭证
        /// </summary>
        /// <param name="checkIds"></param>
        /// <param name="chargeByID"></param>
        #region 解锁凭证
        [FunctionInfomation]
        [OperationContract]
        void UntieLockLedger(Guid checkIds, Guid chargeByID);
        #endregion

        /// <summary>
        /// 整理凭证
        /// </summary>
        /// <param name="ComID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        #region 整理凭证
        [FunctionInfomation]
        [OperationContract]
        string ArrangeLedger(Guid ComID,string date);
        #endregion


        /// <summary>
        /// 获得科目余额
        /// </summary>
        /// <param name="Comid"></param>
        /// <param name="GLID"></param>
        /// <returns></returns>
        #region 获得科目余额
        [FunctionInfomation]
        [OperationContract]
        List<GLBlance> GetBalance(Guid Comid, Guid GLID, Guid? Cusid, Guid? Depid, Guid? Userid, DateTime enddate);
        #endregion
    }
}

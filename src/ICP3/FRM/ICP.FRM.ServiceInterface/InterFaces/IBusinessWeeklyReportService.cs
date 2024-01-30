using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 商务周报表业务接口
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBusinessWeeklyReportService
    {

        #region 获得口岸商务信息
        /// <summary>
        /// 获得商务周报表列表
        /// </summary>
        /// <param name="divisionID">口岸公司ID</param>
        /// <param name="weeklyDate">周日期</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BusinessWeeklyReportInfo> GetBusinessWeeklyReportList(Guid? divisionID, string weeklyDate, bool isEnglish);
        #endregion

        #region 获得所有的商务周报信息
        /// <summary>
        /// 获得商务周报列表数据
        /// </summary>
        /// <param name="weeklyData"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BusinessWeeklyReportData> GetBusinessWeeklyReportDataList(string weeklyData, bool isEnglish);

        #endregion

        #region 保存商务周报信息
        /// <summary>
        /// 保存商务周报表信息
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="weeklyDate">周日期</param>
        /// <param name="divisionIDs">口岸公司ID</param>
        /// <param name="shiplineIDs">航线</param>
        /// <param name="carrierIDs">船公司</param>
        /// <param name="rates">Rates</param>
        /// <param name="shippingSpace">ShippingSpace</param>
        /// <param name="descriptions">Descriptions</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="createByID">创建人</param>
        /// <param name="createDate">创建时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveBusinessWeeklyReport(BusinessWeeklyReportSaveRequest saveRequest);
        #endregion

        #region 删除商务周报信息
        /// <summary>
        /// 删除商务周报表信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="removeID">删除人</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns> 
        [FunctionInfomation]
        [OperationContract]
        bool RemoveBusinessWeeklyReport(
            Guid id,
            DateTime? updateDate,
            Guid removeID,
            bool isEnglish);
        #endregion

        #region 获得服务器的时间
        /// <summary>
        /// 获得服务器的时间
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DateTime GetServerDate();
        #endregion

        #region 更新帖子
        /// <summary>
        /// 发帖及更新帖子
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="ipAddress"></param>
        /// <param name="weekName"></param>
        /// <param name="saveByID"></param>
        [FunctionInfomation]
        [OperationContract]
        bool PostBusinessWeeklyToICP2(string subject, string body, string ipAddress, string weekName,Guid saveByID);
        #endregion

        #region 获得商务周报经理批注
        /// <summary>
        /// 获得商务周报经理批注
        /// </summary>
        /// <param name="weeklyData"></param>
        /// <param name="shiplineID"></param>
        /// <param name="companyID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BusinessWeeklyReportList_Manager> GetBusinessWeeklyReportManagerList(string weeklyData, Guid shiplineID, Guid companyID, bool isEnglish);
        #endregion

        #region 保存商务周报经理批注
        /// <summary>
        /// 保存商务周报经理批注
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveWeeklyReportManager(BusinessWeeklyReport_ManagerSaveRequest saveRequest);
        #endregion

        #region 删除商务周报经理批注
        /// <summary>
        /// 删除商务周报经理批注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDate"></param>
        /// <param name="removeID"></param>
        /// <param name="isEnglish"></param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveWeeklyReportManager(Guid id,
            DateTime? updateDate,
            Guid removeID,
            bool isEnglish);
        #endregion

        #region 获得订舱统计数据
        /// <summary>
        /// 获得订舱统计数据
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="shipLineIDs">航线ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="salesType">揽货类型</param>
        /// <param name="isValid">有效性</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BookingReportData> GetBookingReportDataList(Guid[] companyIDs,
            Guid[] shipLineIDs,
            string customerIDs,
            string carrierIDs,
            Guid? salesType, 
            bool? isValid, 
            DateTime? beginDate, 
            DateTime? endDate, 
            bool isEnglish);
        #endregion

    }
}

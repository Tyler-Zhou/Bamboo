using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;

namespace ICP.FCM.AirExport.ServiceInterface
{
    /// <summary>
    /// 空运出口客户端服务接口 
    /// </summary>
    public interface IClientAirExportService
    {
        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void AddData(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void CopyData(EditPartShowCriteria showCriteria, IDictionary<string, object> values,
                      PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void EditData(EditPartShowCriteria showCriteria, IDictionary<string, object> values,
                            PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 取消订舱单
        /// </summary>
        /// <param name="airBookingId"></param>
        /// <param name="editPartSaved"></param>
        void CancelData(Guid airBookingId, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void ReplyAgent(Guid bookingId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 打开提单列表
        /// </summary>
        /// <param name="airBookingId"></param>
        void OpenBl(Guid airBookingId);
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="airBookingId"></param>
        void OpenBill(Guid airBookingId);
        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="airBookingId"></param>
        void PrintOrder(Guid airBookingId);
    }
}

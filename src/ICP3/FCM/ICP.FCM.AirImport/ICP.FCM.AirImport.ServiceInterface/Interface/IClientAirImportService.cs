using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;

namespace ICP.FCM.AirImport.ServiceInterface
{
    public interface IClientAirImportService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void AddBooking(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void CopyBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values,
                         PartDelegate.EditPartSaved editPartSaved);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        void EditBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values,
                         PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="bookingId"></param>
        void OpenBill(Guid bookingId);


        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="bookingId"></param>
        void PrintArrivalNotice(Guid bookingId);


        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        void PrintReleaseOrder(Guid bookingId);

        /// <summary>
        ///  打印利润表
        /// </summary>
        /// <param name="bookingId"></param>
        void PrintProfit(Guid bookingId);

        /// <summary>
        /// 打印Authority To Make Entry
        /// </summary>
        /// <param name="bookingId"></param>
        void PrintAuthority(Guid bookingId);

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="editPartSaved"></param>
        void CancelBooking(Guid bookingId, PartDelegate.EditPartSaved editPartSaved);


        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        void OpenCargoBook(Guid bookingId, IDictionary<string, object> values,
                                  PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 下载
        /// </summary>
        void AiDownLoad();

        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="bookingId"></param>
        void BusinessTransfer(Guid bookingId);


        /// <summary>
        /// 放货和取消放货
        /// </summary>
        /// <param name="bookingId"></param>
        void AiDelivery(Guid bookingId);

    }
}

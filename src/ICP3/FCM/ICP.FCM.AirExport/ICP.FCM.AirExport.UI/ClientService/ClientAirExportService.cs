using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.UI.BL;
using ICP.FCM.AirExport.UI.Booking;
using ICP.FCM.AirExport.UI.Common;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirExport.UI
{
    /// <summary>
    /// 空运出口服务方法类
    /// </summary>
    public class ClientAirExportService : IClientAirExportService
    {
        #region 服务方法
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        public IAirExportService AirExportService
        {
            get
            {
                return ServiceClient.GetService<IAirExportService>();
            }
        }


        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        public AirExportPrintHelper AirExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<AirExportPrintHelper, AirExportPrintHelper>();
            }
        }
        #endregion

        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void AddData(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Add Booking" : "新增订舱";
                PartLoader.ShowEditPart<BookingBaseEditPart>(RootWorkItem, null, EditMode.New, values, title, editPartSaved, "AddData");
            }
        }

        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void CopyData(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string titleNo = showCriteria.OperationNo.Substring(showCriteria.OperationNo.Length - 4, 4);
                string title = ((LocalData.IsEnglish) ? "Copy Booking: " : "复制订舱单: ") + titleNo;
                Guid ID = new Guid(showCriteria.BillNo.ToString());
                AirBookingList booking = AirExportService.GetAirBookingListByIds(new Guid[] { ID })[0];
                booking.EditMode = EditMode.Copy;
                PartLoader.ShowEditPart<BookingBaseEditPart>(RootWorkItem, booking, EditMode.Copy, values, title, editPartSaved, showCriteria.OperationNo);
            }
        }

        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void EditData(EditPartShowCriteria showCriteria, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string titleNo = showCriteria.OperationNo.Substring(showCriteria.OperationNo.Length - 4, 4);
                string title = ((LocalData.IsEnglish) ? "Edit Order: " : "编辑订单: ") + titleNo;
                Guid ID = new Guid(showCriteria.BillNo.ToString());
                AirBookingList booking = AirExportService.GetAirBookingListByIds(new Guid[] { ID })[0];
                booking.EditMode = EditMode.Edit;
                PartLoader.ShowEditPart<BookingBaseEditPart>(RootWorkItem, booking, EditMode.Edit, values, title, editPartSaved, showCriteria.OperationNo);

            }
        }

        /// <summary>
        /// 取消订舱单
        /// </summary>
        /// <param name="airBookingId">业务ID</param>
        /// <param name="editPartSaved"></param>
        public void CancelData(Guid airBookingId, PartDelegate.EditPartSaved editPartSaved)
        {
            AirBookingList booking = AirExportService.GetAirBookingListByIds(new Guid[] { airBookingId })[0];
            bool isValid = booking.IsValid;
            string message = string.Empty;
            if (isValid)
                message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔订舱单吗?";
            else
                message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔订舱单吗?";

            string failureMessage = string.Empty;

            if (isValid)
                failureMessage = LocalData.IsEnglish ? "Cancel Booking State Failed." : "取消订舱单失败.";
            else
                failureMessage = LocalData.IsEnglish ? "Available Booking State Failed." : "恢复订舱单失败.";


            DialogResult dialogResult = XtraMessageBox.Show(message,
                                                LocalData.IsEnglish ? "Tip" : "提示",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    List<AirBookingFeeList> info = AirExportService.GetAirOrderFeeList(booking.ID,Guid.Empty);
                    if (info.Count > 0)
                    {
                        message = LocalData.IsEnglish ?
                            "The order has bring fees. It can not be cancelled."
                            :
                            "这笔订舱单不能取消，因为操作部门已经开始做单并产生了费用！";

                        XtraMessageBox.Show(message,
                            LocalData.IsEnglish ? "Warning" : "警告",
                             MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);

                        return;
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, failureMessage + ex.Message);
                }

                try
                {
                    SingleResult result = AirExportService.CancelAirOrder(booking.ID,Guid.Empty,
                        isValid,
                        LocalData.UserInfo.LoginID, booking.UpdateDate);

                    AirBookingList currentRow = booking;
                    currentRow.IsValid = !isValid;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    if (editPartSaved != null)
                    {
                        var business = new BusinessOperationParameter
                        {
                            Context = new BusinessOperationContext
                            {
                                OperationID = result.GetValue<Guid>("ID"),
                                OperationNO = booking.No,
                                OperationType = OperationType.AirExport
                            }
                        };
                        editPartSaved(new object[] { business });
                    }
                    if (isValid)
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Cancel Booking Successfully." : "这笔订舱单已经成功取消.");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, failureMessage + ex.Message);
                }
            }
        }

        /// <summary>
        /// 申请代理
        /// </summary>
        public void ReplyAgent(Guid bookingId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                FCMCommonClientService.OpenAgentRequestPart(bookingId, OperationType.AirExport, null, null);
            }
        }

        /// <summary>
        /// 打开提单列表
        /// </summary>
        public void OpenBl(Guid airBookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (airBookingId == Guid.Empty) return;
                AirBookingList booking = AirExportService.GetAirBookingInfo(airBookingId);
                AEBLWorkitem blWorkitem = RootWorkItem.WorkItems.AddNew<AEBLWorkitem>();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("RefNo", booking.No);
                blWorkitem.Init(dic);
                blWorkitem.Run();
            }
        }

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="airBookingId">业务ID</param>
        public void OpenBill(Guid airBookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (airBookingId == null) return;
                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(airBookingId, OperationType.AirExport);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }

        }


        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="airBookingId">业务ID</param>
        public void PrintOrder(Guid airBookingId)
        {
            if (airBookingId == Guid.Empty) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                AirExportPrintHelper.PrintAEOrder(airBookingId,Guid.Empty);
            }
        }

    }
}

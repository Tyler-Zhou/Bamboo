using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.AirImport.UI.Common;
using ICP.FCM.AirImport.UI.Report;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirImport.UI
{
    /// <summary>
    /// 空运进口方法类
    /// </summary>
    public class ClientAirImportService : IClientAirImportService
    {
        #region 服务方法
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }


        public IAirImportService AirImportBusinessService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }

        public AirImportPrintHelper AirImportPrintHelper
        {
            get
            {
                return ClientHelper.Get<AirImportPrintHelper, AirImportPrintHelper>();
            }
        }

        public IAirImportService AirImportService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }


        #endregion


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void AddBooking(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Add Booking" : "新增订舱";
                PartLoader.ShowEditPart<OIBusinessEdit>(this.RootWorkItem, null, EditMode.New, values, title, editPartSaved, "AddBooking");
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void CopyBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values,
                                PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string titleNo = showCriteria.OperationNo.Substring(showCriteria.OperationNo.Length - 4, 4);
                string title = ((LocalData.IsEnglish) ? "Copy Booking: " : "复制订舱单: ") + titleNo;
                Guid ID = new Guid(showCriteria.BillNo.ToString());
                AirBusinessInfo newData = AirImportService.GetBusinessInfo(ID);
                newData.ID = Guid.Empty;
                newData.State = AIOrderState.NewOrder;
                newData.No = string.Empty;
                newData.CreateID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.Now;
                newData.SalesID = LocalData.UserInfo.LoginID;
                newData.SalesName = LocalData.UserInfo.LoginName;
                PartLoader.ShowEditPart<OIBusinessEdit>(RootWorkItem, newData, EditMode.Copy, values, title, editPartSaved, showCriteria.OperationNo);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public void EditBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values,
                                PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string titleNo = showCriteria.OperationNo.Substring(showCriteria.OperationNo.Length - 4, 4);
                string title = ((LocalData.IsEnglish) ? "Edit Booking: " : "编辑订单: ") + titleNo;
                Guid ID = new Guid(showCriteria.BillNo.ToString());
                AirBusinessInfo newData = AirImportService.GetBusinessInfo(ID);
                PartLoader.ShowEditPart<OIBusinessEdit>(RootWorkItem, newData, EditMode.Edit, values, title, editPartSaved, showCriteria.OperationNo);
            }
        }

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="bookingId"></param>
        public void OpenBill(Guid bookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var airimport = AirImportBusinessService.GetBusinessInfo(bookingId);
                if (airimport != null && !ArgumentHelper.GuidIsNullOrEmpty(airimport.MBLID))
                {
                    OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(bookingId, ICP.Framework.CommonLibrary.Common.OperationType.AirImport);
                    if (operationCommonInfo != null)
                    {
                        operationCommonInfo.CurrentFormID = (Guid)airimport.MBLID;
                        FinanceClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                    }
                    else
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                    }
                }

            }
        }

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="bookingId"></param>
        public void PrintArrivalNotice(Guid bookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var airimport = AirImportBusinessService.GetBusinessInfo(bookingId);
                if (airimport == null) return;
                Dictionary<string, object> stateValues = new Dictionary<string, object> { { "AirBusinessList", airimport } };
                string no = airimport.No.Length <= 4 ? airimport.No : airimport.No.Substring(airimport.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书") + ("-" + no);
                PartLoader.ShowEditPart<OIArrivalNotice2>(RootWorkItem, null, stateValues, title, null, null);
            }
        }
        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        public void PrintReleaseOrder(Guid bookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var airimport = AirImportBusinessService.GetBusinessInfo(bookingId);
                if (airimport == null) return;
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("AirBusinessList", airimport);
                string no = airimport.No.Length <= 4 ? airimport.No : airimport.No.Substring(airimport.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print Release Order" : "放货通知书") + ("-" + no);
                PartLoader.ShowEditPart<OIReleaseOrder2>(RootWorkItem, null, stateValues, title, null, null);
            }
        }
        /// <summary>
        ///  打印利润表
        /// </summary>
        /// <param name="bookingId"></param>
        public void PrintProfit(Guid bookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                AirImportPrintHelper.PrintProfit(bookingId);
            }
        }
        /// <summary>
        /// 打印Authority To Make Entry
        /// </summary>
        /// <param name="bookingId">业务ID</param>
        public void PrintAuthority(Guid bookingId)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var airimport = AirImportBusinessService.GetBusinessInfo(bookingId);
                if (airimport == null) return;
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("AirBusinessList", airimport);
                string no = airimport.No.Length <= 4 ? airimport.No : airimport.No.Substring(airimport.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print Authority To Make Entry" : "利润打印") + ("-" + no);
                PartLoader.ShowEditPart<OIBLPrintPart2>(RootWorkItem, null, stateValues, title, null, null);
            }
        }


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="bookingId"></param>
        public void CancelBooking(Guid bookingId, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var airimport = AirImportBusinessService.GetBusinessInfo(bookingId);
                if (airimport == null) return;
                try
                {
                    bool isCancel = airimport.IsValid;

                    string message = string.Empty;
                    if (isCancel)
                        message = LocalData.IsEnglish ? "Srue Cancel Current Business?" : "你真的要取消这笔业务吗?";
                    else
                        message = LocalData.IsEnglish ? "Srue Available Current Business?" : "你真的要恢复这笔业务吗?";


                    bool isOK = Utility.ShowResultMessage(message);

                    if (isOK)
                    {
                        SingleResult result = AirImportService.CancelAIOrder(airimport.ID,Guid.Empty, isCancel, 
                                                                             LocalData.UserInfo.LoginID, 
                                                                             airimport.UpdateDate);
                        if (editPartSaved != null)
                        {
                            var business = new BusinessOperationParameter
                            {
                                Context = new BusinessOperationContext
                                {
                                    OperationID = result.GetValue<Guid>("ID"),
                                    OperationNO = airimport.No,
                                    OperationType = OperationType.AirImport
                                }
                            };
                            editPartSaved(new object[] { business });
                        }
                        if (isCancel)
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Cancel Booking Successfully." : "这笔订舱单已经成功取消.");
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, (LocalData.IsEnglish ? "Changed Order State Failed" : "更改订单状态失败") + ex.Message);
                }
            }
        }

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        public void OpenCargoBook(Guid bookingId, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";
                PartLoader.ShowEditPart<OIBusinessTruckEdit>(RootWorkItem, bookingId, EditMode.Edit, values, title, editPartSaved, title);
            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        public void AiDownLoad()
        {
            OIBusinessDownLoadWorkitem workItem = this.RootWorkItem.WorkItems.AddNew<OIBusinessDownLoadWorkitem>();
            workItem.Run();
        }
        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="bookingId"></param>
        public void BusinessTransfer(Guid bookingId)
        {
            var airimport = AirImportBusinessService.GetBusinessInfo(bookingId);
            if (airimport != null)
            {
                OIBusinessTransfer bsTransfer = RootWorkItem.Items.AddNew<OIBusinessTransfer>();
                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                bsTransfer.BusinessID = airimport.ID;

                string title = LocalData.IsEnglish ? "Business Transfer" : "业务转移";

                if (Utility.ShowDialog(bsTransfer, title) == DialogResult.OK)
                {
                    AirBusinessList currentRow = airimport;
                    currentRow.UpdateDate = bsTransfer.UpdateDate;
                }
            }
            else
            {
                string message = LocalData.IsEnglish ? "Not Select Business Data" : "请选择一个业务单";
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
            }
        }

        /// <summary>
        /// 放货和取消放货
        /// </summary>
        /// <param name="bookingId"></param>
        public void AiDelivery(Guid bookingId)
        {
            var airimport = AirImportBusinessService.GetBusinessInfo(bookingId);
            if (airimport != null)
            {
                if (airimport.State != AIOrderState.Release)
                {
                    #region 放货

                    OIBusinessRelease bsRelease = RootWorkItem.Items.AddNew<OIBusinessRelease>();
                    bsRelease.list = airimport;

                    string title = LocalData.IsEnglish ? "Release" : "放货";

                    if (Utility.ShowDialog(bsRelease, title) == DialogResult.OK)
                    {
                        AirBusinessList currentRow = airimport;
                        currentRow.State = AIOrderState.Release;
                        currentRow.ReleaseType = bsRelease.releaseType;
                        currentRow.IsTelex = currentRow.ReleaseType == FCMReleaseType.Telex ? true : false;
                        currentRow.UpdateDate = bsRelease.UpdateTime;
                        currentRow.ReleaseDate = bsRelease.ReleaseDate;
                    }
                    #endregion
                }
                else
                {
                    #region 取消放货
                    string message = LocalData.IsEnglish ? "Srue Cancel Release ?" : "确认要取消放货?";
                    bool isOK = Utility.ShowResultMessage(message);
                    if (isOK)
                    {
                        try
                        {
                            SingleResultData result = AirImportService.ChangeAIOrderState(airimport.ID,Guid.Empty, AIOrderState.Checked, string.Empty,
                                                                                          LocalData.UserInfo.LoginID, airimport.UpdateDate);
                            airimport.State = AIOrderState.Checked;
                            airimport.UpdateDate = result.UpdateDate;

                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Cancel Successfully" : "取消成功!");

                        }
                        catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex); }
                    }
                    #endregion
                }
            }
        }
    }
}

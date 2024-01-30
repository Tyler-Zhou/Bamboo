using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.UI.Business;
using ICP.FCM.OtherBusiness.UI.Common;
using ICP.FCM.OtherBusiness.UI.ECommerce;
using ICP.FCM.OtherBusiness.UI.Order;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OtherBusiness.UI
{
    /// <summary>
    /// 其他业务-客户端服务
    /// </summary>
    public class ClientOtherBusinessService : IClientOtherBusinessService
    {
        #region 服务方法
        /// <summary>
        /// 
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public OtherUIHelper ExportUIHelper
        {
            get
            {
                return ClientHelper.Get<OtherUIHelper, OtherUIHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public OtherBusinessPrintHelper OtherBusinessPrintHelper
        {
            get
            {
                return ClientHelper.Get<OtherBusinessPrintHelper, OtherBusinessPrintHelper>();
            }
        }

        #endregion

        #region Order / Business
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="addType">新增类型</param>
        /// <param name="editPartSaved">新增类型</param>
        public void AddData(AddType addType, PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                RootWorkItem.State["AddType"] = addType;
                if (addType == AddType.OtherBusiness)
                {
                    PartLoader.ShowEditPart<OBBaseEditPart>(RootWorkItem, null, LocalData.IsEnglish ? "Add Business" : "新增业务信息", editPartSaved);
                }
                else
                {
                    PartLoader.ShowEditPart<OBOrderBaseEditPart>(RootWorkItem, null, LocalData.IsEnglish ? "Add Order" : "新增订单信息", editPartSaved);
                }
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="addType">新增类型</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>

        public void CopyData(AddType addType, Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved)
        {
            if (operationId == Guid.Empty) return;
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (list.Any())
            {
                OtherBusinessList otherBusinessList = list[0];
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    otherBusinessList.EditMode = EditMode.Copy;
                    RootWorkItem.State["AddType"] = addType;
                    if (addType == AddType.OtherBusiness)
                    {
                        PartLoader.ShowEditPart<OBBaseEditPart>(RootWorkItem, otherBusinessList, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", editPartSaved,
                            OBBCommandConstants.Command_EditData + otherBusinessList.ID);
                    }
                    else
                    {
                        PartLoader.ShowEditPart<OBOrderBaseEditPart>(RootWorkItem, otherBusinessList, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", editPartSaved,
                            OBOCommandConstants.Command_EditData + otherBusinessList.ID);
                    }
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="addType">新增类型</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        public void EditData(AddType addType, Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved)
        {
            if (operationId == Guid.Empty) return;
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (list.Any())
            {
                OtherBusinessList otherBusinessList = list[0];
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    otherBusinessList.EditMode = EditMode.Edit;
                    RootWorkItem.State["AddType"] = addType;
                    if (addType == AddType.OtherBusiness)
                    {
                        PartLoader.ShowEditPart<OBBaseEditPart>(RootWorkItem, otherBusinessList,
                            LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", editPartSaved,
                            OBBCommandConstants.Command_EditData + otherBusinessList.ID);
                    }
                    else
                    {
                        PartLoader.ShowEditPart<OBOrderBaseEditPart>(RootWorkItem, otherBusinessList,
                            LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", editPartSaved,
                            OBOCommandConstants.Command_EditData + otherBusinessList.ID);
                    }
                }
            }
        }

        /// <summary>
        /// 取消业务/恢复业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        public void CancelData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved)
        {
            if (operationId == Guid.Empty) return;
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (list.Any())
            {
                OtherBusinessList otherBusinessList = list[0];
                bool isCancel = otherBusinessList.IsValid;
                string message = string.Empty;
                if (isCancel)
                    message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔业务吗?";
                else
                    message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔业务吗?";


                DialogResult dialogResult = XtraMessageBox.Show( message, LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SingleResult result = OtherBusinessService.CancelOtherBusiness(otherBusinessList.ID, otherBusinessList.CompanyID, isCancel,
                        LocalData.UserInfo.LoginID, otherBusinessList.UpdateDate);
                    if (editPartSaved != null)
                    {
                        var business = new ICP.Operation.Common.ServiceInterface.BusinessOperationParameter
                        {
                            Context = new BusinessOperationContext
                            {
                                OperationID = result.GetValue<Guid>("ID"),
                                OperationNO = otherBusinessList.NO,
                                OperationType = OperationType.Other
                            }
                        };
                        editPartSaved(new object[] { business });
                    }
                    if (isCancel)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Changed  State Successfully" : "更改状态成功");
                    }
                }
            }
        }

        
        #endregion

        #region 电商物流(E-Commerce Logistics)
        /// <summary>
        /// 电商物流-新增
        /// </summary>
        /// <param name="editPartSaved">新增类型</param>
        public void ECommerceAddData(IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            PartLoader.ShowEditPart<OBECEditPart>(RootWorkItem, null, EditMode.New, values, LocalData.IsEnglish ? "Add E-Commerce Business" : "新增电商物流业务信息", editPartSaved,"NEWECBUSINESS");
        }
        /// <summary>
        /// 电商物流-复制
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        public void ECommerceCopyData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved)
        {
            if (operationId == Guid.Empty) return;
            string title = LocalData.IsEnglish ? "Copy E-Commerce Business" : "复制电商物流业务信息";
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (!list.Any()) return;
            OtherBusinessList currentRow = list[0];
            using (new CursorHelper(Cursors.WaitCursor))
            {
                currentRow.EditMode = EditMode.Copy;
                PartLoader.ShowEditPart<OBECEditPart>(RootWorkItem, currentRow, title, editPartSaved, OBECCommandConstants.Command_EditData + currentRow.ID);
            }
        }

        /// <summary>
        /// 电商物流-编辑
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        public void ECommerceEditData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved)
        {
            if (operationId == Guid.Empty) return;
            string title = LocalData.IsEnglish ? "Edit E-Commerce Business" : "编辑电商物流业务信息";
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (!list.Any()) return;
            OtherBusinessList otherBusinessList = list[0];
            using (new CursorHelper(Cursors.WaitCursor))
            {
                otherBusinessList.EditMode = EditMode.Edit;
                PartLoader.ShowEditPart<OBECEditPart>(RootWorkItem, otherBusinessList, title, editPartSaved, OBECCommandConstants.Command_EditData + otherBusinessList.ID);
            }
        }

        /// <summary>
        /// 电商物流-取消业务/恢复业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        /// <param name="editPartSaved">数据保存后执行事件</param>
        public void ECommerceCancelData(Guid operationId, Guid companyID, PartDelegate.EditPartSaved editPartSaved)
        {
            if (operationId == Guid.Empty) return;
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (!list.Any()) return;
            OtherBusinessList otherBusinessList = list[0];
            bool isCancel = otherBusinessList.IsValid;
            string message = string.Empty;
            string tip = LocalData.IsEnglish ? "Tip" : "提示";
            if (isCancel)
                message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔业务吗?";
            else
                message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔业务吗?";

            DialogResult dialogResult = XtraMessageBox.Show(message, tip, MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes) return;
            SingleResult result = OtherBusinessService.CancelOtherBusiness(otherBusinessList.ID, otherBusinessList.CompanyID, isCancel,
                LocalData.UserInfo.LoginID, otherBusinessList.UpdateDate);
            if (editPartSaved != null)
            {
                var business = new ICP.Operation.Common.ServiceInterface.BusinessOperationParameter
                {
                    Context = new BusinessOperationContext
                    {
                        OperationID = result.GetValue<Guid>("ID"),
                        OperationNO = otherBusinessList.NO,
                        OperationType = OperationType.Other
                    }
                };
                editPartSaved(new object[] { business });
            }
            if (isCancel)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Changed  State Successfully" : "更改状态成功");
            }
        }

        
        #endregion

        #region Common
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        public void Bill(Guid operationId)
        {
            if (operationId == Guid.Empty) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(operationId, OperationType.Other);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? @"No found" : @"无对应的账单", LocalData.IsEnglish ? "Tip" : "提示",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        public void VerifiSheet(Guid operationId, Guid companyID)
        {
            if (operationId == Guid.Empty) return;
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (!list.Any()) return;
            OtherBusinessList otherBusinessList = list[0];
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string no = otherBusinessList.NO.Length <= 4 ? otherBusinessList.NO : otherBusinessList.NO.Substring(otherBusinessList.NO.Length - 4, 4);
                string title = LocalData.IsEnglish ? "Verifi.Sheet" : "核销单" + ("-" + no);
                object[] data = new object[2];
                data[0] = otherBusinessList.ID;
                data[1] = otherBusinessList.NO;
                PartLoader.ShowEditPart<VerifiSheetEditPart>(RootWorkItem,
                    data,
                    null,
                    title,
                    null,
                    "" + otherBusinessList.ID);
            }
        }

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        public void PickUp(Guid operationId, Guid companyID)
        {
            if (operationId == Guid.Empty) return;
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (list.Any())
            {
                OtherBusinessList otherBusinessList = list[0];
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";
                    ExportUIHelper.ShowTruckEdit(otherBusinessList.ID, otherBusinessList,
                            RootWorkItem, OtherBusinessService,
                           Utility.GetLineNo(otherBusinessList));
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        public void PrintOrder(Guid operationId, Guid companyID)
        {
            if (operationId == Guid.Empty) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OtherBusinessPrintHelper.PrintOBOrder(operationId, companyID);
            }
        }

        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        public void Profit(Guid operationId, Guid companyID)
        {
            if (operationId == Guid.Empty) return;
            Guid[] ids = { operationId };
            Guid[] companyIDs = { companyID };
            List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyIDs);
            if (list.Any())
            {
                OtherBusinessList otherBusinessList = list[0];
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    OtherBusinessPrintHelper.PrintOEBookingProfit(otherBusinessList);
                }
            }
        } 
        /// <summary>
        /// 其他业务-核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="NO">业务口岸ID</param>
        public void VerifiSheet(Guid operationId, string NO)
        {
            if (operationId == Guid.Empty) return;
            string no = NO.Length <= 4 ? NO : NO.Substring(NO.Length - 4, 4);
            string title = LocalData.IsEnglish ? "Verifi.Sheet" : "核销单" + ("-" + no);
            object[] data = new object[2];
            data[0] = operationId;
            data[1] = NO;
            PartLoader.ShowEditPart<VerifiSheetEditPart>(RootWorkItem, data, null, title, null, "" + operationId);
        }
        #endregion
    }
}

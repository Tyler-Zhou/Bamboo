using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.UI;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 海出命令处理类
    /// </summary>
    public class OceanImportCommandHandler : IBaseComnandHandler
    {
        #region Fields & Property & Services
        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// 公共命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonCommandHandler CommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 海出命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanExportCommandHandler OceanExportCommandHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }

        }

        /// <summary>
        /// BaseBusinessPart 面板
        /// </summary>
        public BaseBusinessPart CurrentBaseBusinessPart
        {
            get { return RootWorkItem.State["CurrentBaseBusinessPart"] as BaseBusinessPart; }
            set { RootWorkItem.State["CurrentBaseBusinessPart"] = value; }
        }
        /// <summary>
        /// FCM 公共服务接口
        /// </summary>
        public IICPCommonOperationService IcpCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }

        /// <summary>
        /// 业务面板需要传递的信息类
        /// </summary>
        public BusinessPartParam BusinessPartParam
        {
            get
            {
                return new BusinessPartParam
                {
                    TemplateCode = CurrentBaseBusinessPart.TemplateCode,
                    OperationNo = CurrentBaseBusinessPart.OperationNo,
                    ID = CurrentBaseBusinessPart.OperationID,
                    OperationType = OperationType.OceanImport,
                    CurrentBusinessPart = CurrentBaseBusinessPart,
                    Updatetime = CurrentBaseBusinessPart.Updatetime
                };
            }
        }
        #endregion


        /// <summary>
        /// 海进下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandDownload(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                IcpCommonOperationService.OIDownLoad();
            }

        }

        /// <summary>
        /// 海运进口编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandEdit(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
                BusinessPartParam.OperationNo != string.Empty)
            {
                var editPartShowCriteria = new EditPartShowCriteria
                {
                    BillNo = BusinessPartParam.ID,

                    OperationNo = BusinessPartParam.OperationNo
                };
                Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
                IcpCommonOperationService.EditOIBusiness(editPartShowCriteria, dictionary);
            }

        }

        /// <summary>
        /// 取消业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandCancel(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
                    BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listBase = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listBase == null) return;
                if (listBase.FocusedDataRow != null)
                {
                    if (listBase.FocusedDataRow.Table.Columns.Contains("State"))
                    {
                        OEOrderState oeOrderState =
                            (OEOrderState)Enum.Parse(typeof(OEOrderState), listBase.FocusedDataRow["State"].ToString());
                        if (oeOrderState == OEOrderState.NewOrder)
                        {
                            var id = BusinessPartParam.ID;
                            IcpCommonOperationService.CancelOIBusiness(id, true, BusinessPartParam.Updatetime);
                            OiListRefresh(listBase, BusinessPartParam.ID);
                        }
                        else
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "The current business cannot be cancelled." : "当前业务不能够取消.");
                        }
                    }
                    else
                    {
                        var id = BusinessPartParam.ID;
                        IcpCommonOperationService.CancelOIBusiness(id, true, BusinessPartParam.Updatetime);
                        OiListRefresh(listBase, BusinessPartParam.ID);
                    }

                }
            }
        }

        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTransfer(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
        BusinessPartParam.OperationNo != string.Empty)
            {
                Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);

                IcpCommonOperationService.TransferOIBusiness(BusinessPartParam.ID, dictionary);
            }
        }

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandPrintArrivalNotice(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
        BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.PrintArrivalNotice(id);
            }
        }

        /// <summary>
        /// 打印发货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandReleaseOrder(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
        BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.PrintReleaseOrder(id);
            }
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandPrintProfit(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
                BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.PrintProfit(id);
            }
        }

        /// <summary>
        /// 打印工作表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandPrintWorkSheet(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
        BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.OIPrintWorkSheet(id);
            }
        }

        /// <summary>
        /// 打印货单提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandPrintForwardingBill(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
         BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.PrintForwardingBill(id);
            }
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandPrintExportBusinessInfo(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
        BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.PrintExportBusinessInfo(id);
            }
        }

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandOpenBill(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.OpenOIBill(id);
            }
        }

        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandPrintCargoBook(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
                IcpCommonOperationService.OpenDeliveryNotice(id, dictionary);
            }
        }

        /// <summary>
        /// 签收放单
        /// </summary>
        /// <param name="listBase"></param>
        public void CommandReceiveRN(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                //取消单元格编辑模式
                listPart.bindingSource.EndEdit();
                listPart.gridViewList.CloseEditor();
                IcpCommonOperationService.OIReceiveRN(listPart.TaskCenterSelectedIDs);
                foreach (Guid guids in listPart.TaskCenterSelectedIDs)
                {
                    OiListRefresh(listBase, guids);
                }
            }
        }

        /// <summary>
        /// 申请放单
        /// </summary>
        public void CommandApplyReleaseCargo(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                //取消单元格编辑模式
                listPart.bindingSource.EndEdit();
                listPart.gridViewList.CloseEditor();
                IcpCommonOperationService.ApplyRelease(listPart.TaskCenterSelectedIDs);
                foreach (Guid guids in listPart.TaskCenterSelectedIDs)
                {
                    OiListRefresh(listBase, guids);
                }
            }
        }

        /// <summary>
        /// 取消申请放单
        /// </summary>
        public void CommandCancelApplyReleaseCargo(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                //取消单元格编辑模式
                listPart.bindingSource.EndEdit();
                listPart.gridViewList.CloseEditor();
                IcpCommonOperationService.ApplyRelease(listPart.TaskCenterSelectedIDs);
                foreach (Guid guids in listPart.TaskCenterSelectedIDs)
                {
                    OiListRefresh(listBase, guids);
                }
            }
        }

        /// <summary>
        /// 同意放货
        /// </summary>
        public void CommandOceanImportAgreeRC(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.AgreeRC(id);
                OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// 取消同意放货
        /// </summary>
        public void CommandOceanImportCancelAgreeRC(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.AgreeRC(id);
                OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// 放货
        /// </summary>
        public void CommandOceanImportDelivery(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                //取消单元格编辑模式
                listPart.bindingSource.EndEdit();
                listPart.gridViewList.CloseEditor();
                IcpCommonOperationService.OiNewOiDelivery(listPart.TaskCenterSelectedIDs);
                foreach (Guid guids in listPart.TaskCenterSelectedIDs)
                {
                    OiListRefresh(listBase, guids);
                }
            }
        }

        /// <summary>
        /// 取消放货
        /// </summary>
        public void CommandOceanImportCancelDelivery(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                //取消单元格编辑模式
                listPart.bindingSource.EndEdit();
                listPart.gridViewList.CloseEditor();
                IcpCommonOperationService.OiNewOiDelivery(listPart.TaskCenterSelectedIDs);
                foreach (Guid guids in listPart.TaskCenterSelectedIDs)
                {
                    OiListRefresh(listBase, guids);
                }
            }
        }

        /// <summary>
        /// 异常放货申请
        /// </summary>
        public void CommandExceptionReleaseRC(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                var no = BusinessPartParam.OperationNo;
                IcpCommonOperationService.OIExceptionReleaseRC(id, no);
                OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="isEnglish"></param>
        public void MailPickUpToCustomer(bool isEnglish)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                IcpCommonOperationService.MailPickUpToCustomer(CurrentBaseBusinessPart.OperationID, isEnglish);
            }
        }

        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="isEnglish"></param>
        public void MailAnToCustomer(bool isEnglish)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                IcpCommonOperationService.MailAnToCustomer(CurrentBaseBusinessPart.OperationID, isEnglish);
            }
        }

        /// <summary>
        /// 上传海进AN附件
        /// </summary>
        /// <param name="mailSubject"></param>
        /// <param name="objMailItem"></param>
        public void UploadingAN(string mailSubject, object objMailItem)
        {
            CommandHandler.Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.AN, OperationType.OceanImport);

        }

        /// <summary>
        /// 上传海进附件
        /// </summary>
        /// <param name="mailSubject"></param>
        /// <param name="objMailItem"></param>
        public void UploadingImportAttachment(string mailSubject, object objMailItem)
        {

            CommandHandler.Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.Normal, OperationType.OceanImport);

        }


        /// <summary>
        /// 新增
        /// </summary>
        public void AddOrder()
        {
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Create, true);
            object value;
            dictionary.TryGetValue("businessOperationParameter", out value);
            if (value != null && value.GetType() == typeof(BusinessOperationParameter))
            {
                BusinessOperationParameter businessOperation = value as BusinessOperationParameter;
                if (businessOperation != null) businessOperation.ContactStage = ContactStage.SO;
            }
            IcpCommonOperationService.AddOIBusiness(dictionary);

        }

        /// <summary>
        /// 复制
        /// </summary>
        public void CopyOiBusiness()
        {
            var id = BusinessPartParam.ID;
            var editPartShowCriteria = new EditPartShowCriteria
            {
                BillNo = BusinessPartParam.ID,
                OperationNo = BusinessPartParam.OperationNo
            };
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.CopyOIBusiness(editPartShowCriteria, dictionary);
        }


        /// <summary>
        /// 列表刷新
        /// </summary>
        /// <param name="listBase"></param>
        /// <param name="operationId">业务ID</param>
        public void OiListRefresh(IListBaseBusinessPart listBase, Guid operationId)
        {
            ListBaseBusinessPart listPart = listBase as ListBaseBusinessPart;
            BusinessOperationParameter business = null;
            business = new BusinessOperationParameter();
            BusinessOperationContext businessOperationContext = new BusinessOperationContext
            {
                OperationID = operationId,
                OperationType = OperationType.OceanImport,
                CompanyID = listBase.CompanyID,
            };
            business.Context = businessOperationContext;
            business.TemplateCode = listBase.TemplateCode;
            if (listPart == null) return;
            listPart.OnBusinessOperationCompleted(null, new CommonEventArgs<BusinessOperationParameter>(business));
        }


        /// <summary>
        /// 收到MBL正本
        /// </summary>
        public void OiomblRcved(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.OIOMBLRcved(id);
                OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// 财务已发送MBL
        /// </summary>
        public void MailDMBL(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.MailDMBL(id);
                OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// 财务已关帐
        /// </summary>
        public void OIStateAccountingClose(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.OIStateAccountingClose(id);
                OceanExportCommandHandler.ListRefresh(OperationType.OceanImport, listBase);
            }
        }

        /// <summary>
        /// 收到正本
        /// </summary>
        public void OioblRcved(IListBaseBusinessPart listBase)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;

            bool isOBLRcved = false;
            if (listPart != null && !string.IsNullOrEmpty(listPart.CurrentRow["IsOBLRcved"].ToString()))
                isOBLRcved = Convert.ToBoolean(listPart.CurrentRow["IsOBLRcved"].ToString());
            if (isOBLRcved)
            {
                NewOIBusinessReceived newOiBusinessReceived = new NewOIBusinessReceived();
                newOiBusinessReceived.BusinessID = BusinessPartParam.ID;
                if (newOiBusinessReceived.CancelRcved())
                {
                    newOiBusinessReceived.Dispose();
                    OiListRefresh(listBase, BusinessPartParam.ID);
                }
            }
            else
            {
                NewOIBusinessReceived newOiBusinessReceived = RootWorkItem.Items.AddNew<NewOIBusinessReceived>();
                newOiBusinessReceived.BusinessID = BusinessPartParam.ID;
                string title = LocalData.IsEnglish ? "Set OBLRcved Date" : "设置收到正本时间";

                if (PartLoader.ShowDialog(newOiBusinessReceived, title) == DialogResult.OK)
                {
                    OiListRefresh(listBase, BusinessPartParam.ID);
                }
            }
        }

        

        /// <summary>
        /// 付款
        /// </summary>
        public void OiPayment(IListBaseBusinessPart listBase)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            IcpCommonOperationService.OiPayment(listPart.TaskCenterSelectedIDs);
            foreach (var item in listPart.TaskCenterSelectedIDs)
            {
                OiListRefresh(listBase, item);
            }
        }

        /// <summary>
        /// 发送催款邮件
        /// </summary>
        public void PayNtMail()
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                var id = BusinessPartParam.ID;
                IcpCommonOperationService.PayNtMail(id);
            }
        }

        /// <summary>
        /// 发送催款邮件（自动发送）
        /// </summary>
        public void PayNtautomaticMail()
        {
            IcpCommonOperationService.PayNtautomaticMail();
        }

        /// <summary>
        /// 修改放单指令
        /// </summary>
        public void UpdateIsReleaseOrderRequired(IListBaseBusinessPart listBase)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            bool updateValues = !bool.Parse(listPart.CurrentRow["IsReleaseOrderRequired"].ToString());
            var business = new List<BusinessSaveParameter>();
            var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID",  BusinessPartParam.ID},
                                { "SQLIsReleaseOrderRequired", updateValues?"1":"0" },
                                {"OperationType",OperationType.OceanImport}
                            };
            var businessSave = new BusinessSaveParameter { items = dictionary };
            business.Add(businessSave);
            DataTable dt = BusinessQueryService.Save(business);
            if (dt.Rows.Count > 0)
            {
                OiListRefresh(listBase, BusinessPartParam.ID);
            }
        }
        /// <summary>
        /// 催港前放单
        /// </summary>
        public void NoticeRelease(IListBaseBusinessPart listBase)
        {
            ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
            if (listPart == null) return;
            DataRow dataRow = listPart.CurrentRow;
            if (!dataRow.IsNull(0))
            {
                Guid operationid = BusinessPartParam.ID;
                IcpCommonOperationService.NoticeRelease(operationid);
                OiListRefresh(listBase, operationid);
            }
        }


        /// <summary>
        /// 催代理分发文件
        /// </summary>
        /// <param name="isEnglish">中英文版本</param>
        public void MailOverseaAgent(bool isEnglish)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                IcpCommonOperationService.MailOverseaAgent(CurrentBaseBusinessPart.OperationID, isEnglish);
            }
        }

        /// <summary>
        /// ETA 前5天催清关文件
        /// </summary>
        public void MailCustomrequest()
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                IcpCommonOperationService.MailCustomrequest(CurrentBaseBusinessPart.OperationID);
            }
        }

        /// <summary>
        /// ETA 变更通知
        /// </summary>
        public void MailEtachange()
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart != null)
                {
                    DataRow dataRow = listPart.CurrentRow;
                    if (bool.Parse(dataRow["ANSC"].ToString()))
                    {
                        IcpCommonOperationService.MailEtachange(CurrentBaseBusinessPart.OperationID);
                    }
                }
            }
        }

        /// <summary>
        /// CONTAINER AVAILABLE FOR PICK UP
        /// </summary>
        public void MailContaineravailableforpickup(IListBaseBusinessPart listBase)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart == null) return;
                Guid id = CurrentBaseBusinessPart.OperationID;
                IcpCommonOperationService.MailContaineravailableforpickup(id);
                OiListRefresh(listBase, id);
            }
        }

        /// <summary>
        /// LFD 通知
        /// </summary>
        public void MailLfdnotice()
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                ListBaseBusinessPart listPart = CurrentBaseBusinessPart as ListBaseBusinessPart;
                if (listPart != null)
                {
                    DataRow dataRow = listPart.CurrentRow;
                    if (bool.Parse(dataRow["ANSC"].ToString()))
                    {
                        IcpCommonOperationService.MailLfdnotice(CurrentBaseBusinessPart.OperationID, dataRow["LastFreeDate"].ToString(), string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// LFD 前2工作日后每天提柜提醒
        /// </summary>
        public void MailContainerpickupreminder()
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                IcpCommonOperationService.MailContainerpickupreminder(CurrentBaseBusinessPart.OperationID);
            }
        }

        /// <summary>
        /// 提柜后第2天开始每天还空提醒
        /// </summary>
        public void MailEmptyreturnnotice()
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty && BusinessPartParam.OperationNo != string.Empty)
            {
                IcpCommonOperationService.MailEmptyreturnnotice(CurrentBaseBusinessPart.OperationID);
            }
        }
    }
}

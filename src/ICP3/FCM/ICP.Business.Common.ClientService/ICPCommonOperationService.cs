using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirImport.ServiceInterface;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FRM.ServiceInterface;
using System.Data;
using ICP.Business.Common.ServiceInterface;
using ContactType = ICP.Framework.CommonLibrary.Common.ContactType;
using FCMInterfaceUtility = ICP.FCM.Common.ServiceInterface.FCMInterfaceUtility;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.Common;

namespace ICP.Business.Common.ClientService
{
    /// <summary>
    /// FCM提供给外界调用的公共服务实现类
    /// </summary>
    [ServiceKnownType(typeof(BusinessOperationParameter))]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Single)]
    public class ICPCommonOperationService : PublishService<IBusinessOperationCallbackService>, IICPCommonOperationService
    {
        #region Service Injection
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateCode"></param>
        public void SetTemplateCode(string templateCode)
        {
            TemplateCode = templateCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTemplateCode()
        {
            return TemplateCode;
        }
        /// <summary>
        /// 
        /// </summary>
        public ICPCommUIHelper CommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IClientInquireRateService ClientInquireRateService
        {
            get
            {
                return ServiceClient.GetClientService<IClientInquireRateService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IClientOtherBusinessService ClientOtherBusiness
        {
            get
            {
                return ServiceClient.GetClientService<IClientOtherBusinessService>();
            }
        }
        ///<summary>
        ///</summary>
        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }

        }
        /// <summary>
        /// 海运出口业务数据接口
        /// </summary>
        public static IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 报表数据服务接口
        /// </summary>
        public IOEReportDataService OEReportDataSrvice
        {
            get
            {
                return ServiceClient.GetService<IOEReportDataService>();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
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
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }

        }
        /// <summary>
        /// 方法接口
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }

        }
        /// <summary>
        ///海进业务接口
        /// </summary>
        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public IMailCenterTemplateService MailCenterTemplateService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                return new MailCenterTemplateService();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IClientBusinessOperationService>();
            }
        }
        public IClientBusinessContactService ClientBusinessContactService
        {
            get
            {
                return ServiceClient.GetClientService<IClientBusinessContactService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }

        }
        public IBusinessContactService BusinessContactService
        {
            get
            {
                return ServiceClient.GetService<IBusinessContactService>();
            }
        }
        public IOperationMessageRelationService OperationMessageRelationService
        {
            get
            {
                return ServiceClient.GetService<IOperationMessageRelationService>();
            }
        }
        /// <summary>
        /// 空出的方法
        /// </summary>
        public IClientAirExportService AirExportService
        {
            get { return ServiceClient.GetClientService<IClientAirExportService>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public IClientAirImportService AirImportService
        {
            get { return ServiceClient.GetClientService<IClientAirImportService>(); }
        }
        /// <summary>
        /// IFinanceClientService 
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        #endregion

        #region 业务更新相关

        /// <summary>
        /// 新增业务
        /// </summary>
        /// <param name="values"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        public void AddBusiness(Dictionary<string, object> values, OperationType operationType, FormType formType)
        {
            ClientHelper.ActiveMainForm();
            switch (operationType)
            {
                case OperationType.Other:
                    if(formType==FormType.ECommerceOrder)
                        ClientOtherBusiness.ECommerceAddData(null,AfterEditPartSaved);
                    break;
            }
        }

        /// <summary>
        /// 编辑业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        public void EditBusiness(EditPartShowCriteria showCriteria, Dictionary<string, object> values, OperationType operationType, FormType formType)
        {
            ClientHelper.ActiveMainForm();
            switch (operationType)
            {
                case OperationType.Other:
                    if (formType == FormType.ECommerceOrder)
                        ClientOtherBusiness.ECommerceEditData(showCriteria.OperationID, showCriteria.CompanyID, AfterEditPartSaved);
                    break;
            }
        }

        /// <summary>
        /// 复制业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        public void CopyBusiness(EditPartShowCriteria showCriteria, Dictionary<string, object> values, OperationType operationType, FormType formType)
        {
            ClientHelper.ActiveMainForm();
            switch (operationType)
            {
                case OperationType.Other:
                    if (formType == FormType.ECommerceOrder)
                        ClientOtherBusiness.ECommerceCopyData(showCriteria.OperationID, showCriteria.CompanyID, AfterEditPartSaved);
                    break;
            }
        }

        /// <summary>
        /// 取消业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="operationType"></param>
        /// <param name="formType"></param>
        public void CancelBusiness(EditPartShowCriteria showCriteria, OperationType operationType, FormType formType)
        {
            ClientHelper.ActiveMainForm();
            switch (operationType)
            {
                case OperationType.Other:
                    if (formType == FormType.ECommerceOrder)
                        ClientOtherBusiness.ECommerceCancelData(showCriteria.OperationID, showCriteria.CompanyID, AfterEditPartSaved);
                    break;
            }
        }

        /// <summary>
        /// 批量取消业务
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        /// <param name="isCancel"></param>
        /// <param name="formType"></param>
        public void BatchCancelBusiness(Guid[] operationIds, bool isCancel, OperationType operationType, FormType formType)
        {
            ClientHelper.ActiveMainForm();
            switch (operationType)
            {
                case OperationType.Other:
                    if (formType == FormType.ECommerceOrder)
                    {
                        //ClientOtherBusiness.ECommerceCancelData(showCriteria.OperationID, showCriteria.CompanyID, AfterEditPartSaved);
                    }
                    break;
            }
        }
        #region 其他业务-电商物流

        /// <summary>
        /// 其他业务-电商物流-新增
        /// </summary>
        public void OtherBusinessECAddData()
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.ECommerceAddData(null,AfterEditPartSaved);
        }

        /// <summary>
        /// 其他业务-电商物流-复制
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        public void OtherBusinessECCopyData(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
        }

        /// <summary>
        /// 其他业务-电商物流-编辑
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        public void OtherBusinessECEditData(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.ECommerceEditData(operationId, companyID, AfterEditPartSaved);
        }

        /// <summary>
        /// 其他业务-电商物流-取消业务/恢复业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">业务口岸ID</param>
        public void OtherBusinessECCancelData(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.ECommerceCancelData(operationId, companyID, AfterEditPartSaved);
        }
        #endregion
        #endregion

        #region 业务打印

        /// <summary>
        /// 打印订舱确认书(宁波)
        /// </summary>
        /// <param name="operationID">业务ID</param>
        public void PrintOEBookingConfirmation4NB(Guid operationID)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.PrintOEBookingConfirmation4NB(operationID);
        }
        #endregion

        #region  海出方法
        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        public void EditBooking(EditPartShowCriteria showCriteria, Dictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.EditBooking(showCriteria, values, AfterEditPartSaved);

        }
        /// <summary>
        /// 新增订单
        /// </summary>
        public void AddOrder(Dictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.AddOrder(values, AfterEditPartSaved);

        }

        /// <summary>
        /// 审核当前业务存在利润
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        public void SetUpdateOceanTrackings(Guid oceanBookingId)
        {
            ClientOceanExportService.SetUpdateOceanTrackings(oceanBookingId);
        }


        /// <summary>
        /// 创建MBL
        /// </summary>
        /// <param name="dicParameters"></param>
        public void AddMBL(Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.AddMBL(dicParameters, AfterEditPartSaved);
        }
        /// <summary>
        /// 打开任务中心
        /// </summary>
        /// <param name="operationContext"></param>
        public void OpenTaskCenter(BusinessOperationContext operationContext)
        {

            ClientHelper.ActiveMainForm();
            workitem.State["BusinessOperationContext"] = operationContext;
            workitem.Commands[ClientCommandConstants.TASKCENTER_QUICKOPEN].Execute();
            workitem.State["BusinessOperationContext"] = null;
        }


        /// <summary>
        /// 创建HBL
        /// </summary>
        /// <param name="dicParameters"></param>
        public void AddHBL(Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.AddHBL(dicParameters, AfterEditPartSaved);
        }

        /// <summary>
        /// 预配提单
        /// </summary>
        /// <param name="mblNo"></param>
        /// <param name="dicParameters"></param>
        public void DeclarationBL(string mblNo, Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.DeclarationImport(mblNo, dicParameters, null);
        }

        /// <summary>
        /// 创建HBL
        /// </summary>
        /// <param name="dicParameters"></param>
        public void AddDeclareHBL(Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.AddDeclareHBL(dicParameters, AfterEditPartSaved);
        }

        /// <summary>
        /// 编辑MBL
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="mblNo"></param>
        public void EditMBL(string operationNo, string mblNo, Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.EditMBL(operationNo, mblNo, dicParameters, AfterEditPartSaved);


        }
        /// <summary>
        /// 编辑HBL
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="hblNo"></param>
        public void EditHBL(string operationNo, string hblNo, Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.EditHBL(operationNo, hblNo, dicParameters, AfterEditPartSaved);

        }
        /// <summary>
        /// 编辑HBL
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="hblNo"></param>
        public void EditDeclareHBL(Guid hblid, string hblNo, Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.EditDeclareHBL(hblid, hblNo, dicParameters, AfterEditPartSaved);

        }

        /// <summary>
        /// 复制海出业务
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        public BusinessOperationContext OceanExportCopyBusiness(BusinessOperationContext context)
        {
            return ClientOceanExportService.CopyBusiness(context);
        }
        /// <summary>
        /// 打印提单
        /// </summary>
        public void PrintBillOfLoading(Guid blId)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.PrintBillOfLoading(blId);
        }
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="oceanBookingId">订舱单ID，等于业务ID</param>
        public void PrintBookingProfit(Guid oceanBookingId)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.PrintBookingProfit(oceanBookingId);
        }
        /// <summary>
        /// 打印装货单
        /// </summary>
        /// <param name="listData"></param>
        public void PrintLoadGoods(OceanBLList listData)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.PrintLoadGoods(listData);
        }
        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="iscopy"></param>
        public void PrintLoadContainer(OceanBLList listData, bool iscopy)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.PrintLoadContainers(listData, iscopy);
        }
        /// <summary>
        /// 复制MBL单
        /// </summary>
        /// <param name="listData"></param>
        public void InnerCopyMBLData(OceanBLList listData)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.InnerCopyMBLData(listData, AfterEditPartSaved);
        }
        /// <summary>
        /// 复制HBL单
        /// </summary>
        /// <param name="listData"></param>
        public void InnerCopyHBLData(OceanBLList listData)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.InnerCopyHBLData(listData, AfterEditPartSaved);
        }
        /// <summary>
        /// 复制HBL单
        /// </summary>
        /// <param name="listData"></param>
        public void InnerCopyDeclareHBLData(OceanBLList listData)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.InnerCopyDeclareHBLData(listData, AfterEditPartSaved);
        }
        /// <summary>
        /// 删除提单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <returns></returns>
        public bool InnerDelete(OceanBLList listData, object DataSource, object bsList,
                         object businessOperationContext)
        {
            //ClientHelper.ActiveMainForm();
            return ClientOceanExportService.InnerDelete(listData, DataSource, bsList, businessOperationContext, AfterEditPartSaved);
        }

        /// <summary>
        /// 删除Declare提单
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="DataSource"></param>
        /// <param name="bsList"></param>
        /// <returns></returns>
        public bool InnerDeclareDelete(OceanBLList listData, object DataSource, object bsList,
                         object businessOperationContext)
        {
            //ClientHelper.ActiveMainForm();
            return ClientOceanExportService.InnerDeclareDelete(listData, DataSource, bsList, businessOperationContext, AfterEditPartSaved);
        }

        /// <summary>
        /// 提单的分单操作
        /// </summary>
        /// <param name="splitBLList"></param>
        /// <param name="values"></param>
        public void SplitBillOfLoading(OceanBLList splitBLList, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.SplitBillOfLoading(splitBLList, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 提单的合单操作
        /// </summary>
        /// <param name="selectedList"></param>
        public void MergeBillOfLoading(List<OceanBLList> selectedList)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.MergeBillOfLoading(selectedList, AfterEditPartSaved);
        }
        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="amsEntryType"></param>
        /// <param name="aciEntryType"></param>
        /// <param name="isSucc"></param>
        public void InnerEMBL(List<OceanBLList> selectedList, Guid companyID, AMSEntryType amsEntryType,
                              ACIEntryType aciEntryType, ref bool isSucc, object businessOperationContext)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.InnerEMBL(selectedList, companyID, amsEntryType, aciEntryType, ref isSucc,
                                               businessOperationContext, AfterEditPartSaved);
        }

        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="amsEntryType"></param>
        /// <param name="aciEntryType"></param>
        /// <param name="isSucc"></param>
        public void InnerEVGM(OceanBLList selectedList, Guid companyID,ref bool isSucc, object businessOperationContext)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.InnerEVGM(selectedList, companyID, ref isSucc,businessOperationContext, AfterEditPartSaved);
        }


        /// <summary>
        /// 发送AMS/AIC/ISF的方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subjuect"></param>
        /// <param name="oIds"></param>
        /// <param name="hblIds"></param>
        /// <param name="operationNos"></param>
        /// <param name="isSucc"></param>
        /// <param name="ediMode"></param>
        public void SendEdiamsaicisf(string key, string subjuect, List<Guid> oIds, List<Guid> hblIds,
                                     List<string> operationNos,
                                     bool isSucc, object ediMode, Guid CompanyID, object businessOperationContext)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, ediMode, CompanyID,
                                            businessOperationContext, AfterEditPartSaved);

        }

        /// <summary>
        /// 发送AMS/AIC/ISF的方法
        /// </summary>
        /// <param name="hblids">提单ID集合</param>
        /// <param name="updateBy">更新人</param>
        public void ConfirmedAMS(Guid [] hblids, Guid updateBy)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.ConfirmedAMS(hblids,updateBy);
        }

        private static void MergeDictionary(Dictionary<string, object> target, Dictionary<string, object> source)
        {

            if (source != null || source.Count > 0)
                return;
            foreach (KeyValuePair<string, object> pair in source)
            {
                target.Add(pair.Key, pair.Value);
            }

        }

        /// <summary>
        /// 打开派车单
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="dicParameters"></param>
        /// <param name="operationNo"></param>
        public void OpenTruckOrder(Guid operationId, string operationNo, Dictionary<string, object> dicParameters)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.OpenTruckOrder(operationId, operationNo, null, dicParameters, AfterEditPartSaved);


        }
        
        /// <summary>
        /// 批量新增账单
        /// </summary>
        public void BatchAddBill()
        {
            workitem.Commands["FAM_BATCH_BILL_MANAGE"].Execute();
        }
        /// <summary>
        /// 打开指定业务号下的提单列表
        /// </summary>
        public void OpenBillOfLoadingList(string operationNo)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.OpenBillOfLoadingList(operationNo);

        }
        /// <summary>
        /// 发送邮件给订舱员申请订舱 
        /// </summary>
        /// <param name="operationId">订单ID,业务Id</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        public string MailBookingClerkForApplySO(Guid operationId)
        {
            return ClientOceanExportService.MailBookingClerkForApplySO(operationId);
        }

        /// <summary>
        /// 发送邮件给客户确认提单无误,补料OK
        /// </summary>
        /// <param name="isEnglish">是否发送英文版本</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="oceanHblInfo">Hbl实体对象</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        public string MailCustomerAskForConfirmSI(bool isEnglish, Guid operationId, OceanHBLInfo oceanHblInfo, OceanMBLInfo oceanMblInfo)
        {
            return ClientOceanExportService.MailCustomerAskForConfirmSI(isEnglish, operationId, oceanHblInfo, oceanMblInfo);
        }
        /// <summary>
        /// 发送邮件信息时候进行判断
        /// </summary>
        /// <param name="oceanBooking">订舱的实体对象</param>
        /// <returns>返回错误信息</returns>
        public string GetEmailSendValidationInfo(OceanBookingInfo oceanBooking)
        {
            return ClientOceanExportService.GetEmailSendValidationInfo(oceanBooking);
        }
        /// <summary>
        /// 发送邮件给业务员要求确认业务盈利
        /// </summary>
        /// <param name="isEnglish">是否发送英文邮件</param>
        /// <param name="operationId">业务Id</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        public string MailSalesManAskForProfitPromise(bool isEnglish, Guid operationId)
        {
            return ClientOceanExportService.MailSalesManAskForProfitPromise(isEnglish, operationId);
        }

        /// <summary>
        /// 发送邮件给揽货人确认应收费用
        /// </summary>
        /// <param name="isEnglish">是否发送英文邮件</param>
        /// <param name="operationId">业务ID</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        public string MailSalesForConfirmDebitFees(bool isEnglish, Guid operationId)
        {
            return ClientOceanExportService.MailSalesForConfirmDebitFees(isEnglish, operationId);
        }

        /// <summary>
        /// 发送邮件给客户通知取消订舱
        /// </summary>
        /// <param name="isEnglish">是否发送英文邮件</param>
        /// <param name="operationId">业务Id</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        public string MailCustomerForCancelBooking(bool isEnglish, Guid operationId)
        {
            return ClientOceanExportService.MailCustomerForCancelBooking(isEnglish, operationId);
        }

        /// <summary>
        /// 发送邮件给客户通知订舱失败
        /// </summary>
        /// <param name="isEnglish">是否发送英文邮件</param>
        /// <param name="operationId">业务ID</param>
        public string MailCustomerForSoFailure(bool isEnglish, Guid operationId)
        {
            return ClientOceanExportService.MailCustomerForSoFailure(isEnglish, operationId);
        }



        /// <summary>
        /// 发送邮件给客户要求客户提供补料信息
        /// </summary>
        /// <param name="isEnglish">是否发送英文邮件</param>
        /// <param name="operationId">业务ID</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        public string MailCustomerAskForSi(bool isEnglish, Guid operationId)
        {
            return ClientOceanExportService.MailCustomerAskForSi(isEnglish, operationId);
        }
        /// <summary>
        /// 发送邮件给客户通知订舱成功
        /// </summary>
        /// <param name="isEnglish">是否发送英文邮件</param>
        /// <param name="operationId">业务ID</param>
        /// <returns>验证信息，如果不为空，则代表发送有应用异常</returns>
        public string MailSoCopyToCustomer(bool isEnglish, Guid operationId)
        {
            return ClientOceanExportService.MailSoCopyToCustomer(isEnglish, operationId);
        }

        public string MailALLBLCopyToAgent(bool isEnglish, Guid operationId)
        {
            return ClientOceanExportService.MailALLBLCopyToAgent(isEnglish, operationId, AfterEditPartSaved);
        }
        /// <summary>
        /// 打开报关单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="values"></param>
        public void OpenCustomsOrder(Guid operationId, Dictionary<string, object> values)
        {
            ClientOceanExportService.OpenCustomsOrder(operationId, values, AfterEditPartSaved);
            ClientHelper.ActiveMainForm();
        }
        private void SaveMessageAndRelation(BusinessOperationParameter operationParameter)
        {
            try
            {
                if (operationParameter.Message != null)
                {
                    if(operationParameter.Context.OperationType!=OperationType.OceanExport && operationParameter.Context.OperationType != OperationType.OceanImport)
                    {
                        return;
                    }
                    OperationMessageRelation relation = FCMInterfaceUtility.CreateOperationMessageRelationInfo(
                    Guid.NewGuid(), null,
                    operationParameter.Context.OperationID,
                    operationParameter.Context.OperationType, operationParameter.Message.MessageId,
                    operationParameter.Message.Id, ContactStage.Unknown);
                    operationParameter.Message.UserProperties = new MessageUserPropertiesObject();
                    operationParameter.Message.UserProperties.OperationId = operationParameter.Context.OperationID;
                    operationParameter.Message.UserProperties.OperationType = operationParameter.Context.OperationType;
                    operationParameter.Message.UserProperties.FormId = operationParameter.Context.OperationID;
                    operationParameter.Message.UserProperties["ContactStage"] = operationParameter.ContactStage;
                    operationParameter.Message.UserProperties.FormType = operationParameter.Context.FormType;
                    operationParameter.Message.Type = MessageType.Email;
                    operationParameter.Message.State = MessageState.Success;
                    EnsureAndSaveMailMessageReference(relation, operationParameter.Message);
                }
                if (operationParameter.Context != null)
                {
                    string queryString = string.Empty;
                    if (!string.IsNullOrEmpty(operationParameter.Context.OperationNO))
                    {
                        queryString = string.Format("$@NO@='{0}'", operationParameter.Context.OperationNO);
                    }
                    else if (!string.IsNullOrEmpty(operationParameter.Context.OperationID.ToString()))
                    {
                        queryString = string.Format("$@OceanBookingID@='{0}'", operationParameter.Context.OperationID);
                    }
                    ServiceClient.GetClientService<WorkItem>().State["columnQueryString"] = queryString;
                    operationParameter.Context["ServerQueryString"] = queryString;
                    Fire(operationParameter);
                }

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }
        }
        /// <summary>
        /// 验证邮件业务是否关联
        /// </summary>
        /// <returns></returns>
        private DialogResult ValidationMessageRelation(OperationMessageRelation relation, string newOperationNO)
        {
            string tip = LocalData.IsEnglish ? "" : "";

            if (relation.OperationNo == newOperationNO)
            {

                return DialogResult.OK;
            }
            tip = LocalData.IsEnglish ? string.Format("The current mail is connected to {0},", relation.OperationNo) + Environment.NewLine + string.Format("Do you want to re-connect it to {0}?", newOperationNO) : string.Format("当前邮件已关联到业务:{0},", relation.OperationNo) + Environment.NewLine + string.Format("你想重新关联到业务:{0}吗?", newOperationNO);


            return XtraMessageBox.Show(tip, "Tips", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
        }

        private void Fire(BusinessOperationParameter parameter)
        {
            string methodName = "HandleBusinessOperation";
            FireEvent(methodName, parameter);
        }

        public void Save(List<BusinessSaveParameter> parameters, BusinessOperationParameter parameter)
        {
            BusinessQueryService.Save(parameters);
            if (parameter.Context != null && LocalData.ApplicationType == ApplicationType.ICP)
            {
                TemplateCode = parameter.TemplateCode;
                Fire(parameter);
            }
        }

        public void AfterContainerInfoSaved(BusinessOperationParameter parameter)
        {
            if (parameter.Context != null && LocalData.ApplicationType == ApplicationType.ICP)
            {
                //this.TemplateCode = parameter.TemplateCode;
                Fire(parameter);
            }
        }

        public void AfterEditPartSaved(object[] prams)
        {
            if (prams == null || !prams.Any())
            {
                return;
            }
            object tempParameter = prams.SingleOrDefault(item => item is BusinessOperationParameter);
            if (tempParameter != null)
            {
                BusinessOperationParameter operationParameter = tempParameter as BusinessOperationParameter;
                SaveMessageAndRelation(operationParameter);
            }
        }

        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="values"></param>
        public void AddBooking(Dictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.AddBooking(values, AfterEditPartSaved);


        }

        /// <summary>
        /// 取消或激活海出业务
        /// </summary>
        /// <param name="operationIds">业务ID集合</param>
        public SingleResult CancelOEBusiness(Guid operationId)
        {
            return ClientOceanExportService.CancelBooking(operationId, AfterEditPartSaved);
        }

        /// <summary>
        ///  取消或激活海出业务
        /// </summary>
        /// <param name="operationIds"></param>
        public ManyResult OECancelBusinesss(Guid[] operationIds)
        {
            return ClientOceanExportService.CancelBookings(operationIds, AfterEditPartSaved);
        }

        /// <summary>
        ///  取消或激活海出业务
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="IsCancel"></param>
        public ManyResult OECancelBusinesss(Guid[] operationIds,bool IsCancel)
        {
            return ClientOceanExportService.CancelBookings(operationIds, AfterEditPartSaved, IsCancel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        public void OpenAddBookingEdi(WorkItem workItem)
        {
            ClientOceanExportService.OpenAddBookingEdi(workItem);
        }

        /// <summary>
        /// EDI操作
        /// </summary>
        public bool EBookingCall(List<Guid> oceanBookingId)
        {
            bool flg = false;
            List<OceanBookingList> oceanbookinginfo = OceanExportService.GetOceanBookingListByIds(oceanBookingId.ToArray());
            if (oceanbookinginfo != null)
            {
                flg = ClientOceanExportService.EBookingCall(oceanbookinginfo[0], oceanbookinginfo);
            }
            return flg;
        }

        /// <summary>
        /// EDI操作
        /// </summary>
        public bool EBookingConfirm(List<Guid> oceanBookingId)
        {
            bool flg = false;
            List<OceanBookingList> oceanbookinginfo = OceanExportService.GetOceanBookingListByIds(oceanBookingId.ToArray());
            if (oceanbookinginfo != null)
            {
                flg = ClientOceanExportService.EBookingConfirm(oceanbookinginfo[0], oceanbookinginfo);
            }
            return flg;
        }

        public bool ExecGetEDIDataSourceForNBEDIInfos(int ediType, Guid[] IDS)
        {
            return ClientOceanExportService.InnerGetEDIDataSourceForNBEDIInfos(ediType, IDS);
        }

        /// <summary>
        /// 打开申请代理的页面
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="values"></param>
        public void ReplyAgent(Guid operationId, OperationType operationType, Dictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            FCMCommonClientService.OpenAgentRequestPart(operationId, operationType, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        public void CopyBooking(EditPartShowCriteria showCriteria, Dictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.CopyBooking(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 确认装船
        /// </summary>
        /// <param name="operationNo">业务NO</param>
        /// <param name="mblNo">MBL No</param>
        public void LoadShip(string operationNo, string mblNo)
        {
            ClientOceanExportService.LoadShip(operationNo, mblNo, null, AfterEditPartSaved);
        }

        /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="orderId">订单ID或者业务Id</param>
        public void PrintOrder(Guid orderId,Guid companyID)
        {
            ClientOceanExportService.PrintOrder(orderId,companyID);
        }

        /// <summary>
        /// 订舱确认书
        /// </summary>
        /// <param name="id">订舱单ID，等于业务Id</param>
        public void PrintBookingConfirm(Guid oceanBookingId)
        {
            ClientOceanExportService.PrintBookingConfirm(oceanBookingId);
        }

        /// <summary>
        /// 打开分发文档界面
        /// </summary>
        /// <param name="context"></param>
        public void DispatchDocument(BusinessOperationContext context)
        {
            FCMCommonClientService.DispatchDocument(context, null);
        }

        /// <summary>
        /// 保存事件日志
        /// </summary>
        /// <param name="eventobject"></param>
        public void SaveEventInfo(EventObjects eventobject)
        {
            try
            {
                ServiceClient.GetService<IFCMCommonService>().SaveMemoInfo(eventobject);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }
        }

        /// <summary>
        /// 单独处理回调使用的方法
        /// </summary>
        /// <param name="prams"></param>
        public void AfterEditPart(object[] prams)
        {
            AfterEditPartSaved(prams);
        }

        /// <summary>
        /// 新增海出询价（打开新增海出询价界面）
        /// </summary>
        public void NewInquireRate()
        {
            ClientHelper.ActiveMainForm();
            ClientInquireRateService.InquireOceanRate();

        }

        /// <summary>
        /// 打开海运询价列表界面
        /// </summary>
        public void OpenOceanInquireRateListPart()
        {
            ClientHelper.ActiveMainForm();
            ClientInquireRateService.OpenOceanInquireRateListPart();

        }

        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="operationNo"></param>
        /// <param name="oceanBookingId"></param>
        public void LoadContainer(string operationNo, Guid oceanBookingId)
        {
            ClientOceanExportService.LoadContainer(operationNo, oceanBookingId, null, null);
        }

        /// <summary>
        /// 邮件订舱
        /// </summary>
        /// <param name="oceanBookIngidList"></param>
        public void CommunicationMailBooking(List<Guid> oceanBookIngidList)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanExportService.CommunicationMailBooking(oceanBookIngidList);
        }

        /// <summary>
        /// 新增反馈
        /// </summary>
        /// <param name="attachmentPath"></param>
        /// <param name="description"></param>
        public void AddFeedback(string attachmentPath, string description)
        {
            ClientHelper.ActiveMainForm();
            workitem.State["ScreenImagePath"] = attachmentPath;
            workitem.State["TotalErrorMessage"] = description;
            workitem.Commands["SYSTEM_NEWFEEDBACK"].Execute();

        }

        /// <summary>
        /// 根据email获取OperationContact业务联系人信息
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public List<OperationContactInfo> GetOperationContactByEmails(List<string> emails)
        {
            return ClientBusinessContactService.GetOperationContactByEmails(emails);
        }

        ///<summary>
        /// 弹出海出修订签收比较界面
        /// </summary>
        /// <param name="oeOperationId">订单ID</param>
        public void ShowReviseAccepte(Guid oeOperationId)
        {
            //ClientOceanExportService.ShowReviseAccepte(oeOperationId);
        }

        /// <summary>
        /// 根据邮件主题获取业务信息
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public DataTable GetLocalOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria)
        {
            return ClientBusinessOperationService.GetLocalOperationViewListBySubjectKeyWord(criteria);
        }
        /// <summary>
        /// 获取邮件关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<OperationMessageRelation> GetLocalOperationMessageRelationByMessageIdAndReference(string messageId, string reference)
        {
            return ClientBusinessOperationService.GetLocalOperationMessageRelationByMessageIdAndReference(messageId, reference);
        }

        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        public void UpdateLocalBusinessData(Guid operationId, OperationType operationType, DataTable dt)
        {
            ClientBusinessOperationService.UpdateLocalBusinessData(operationId, operationType, dt);
        }

        /// <summary>
        /// 保存本地缓存邮件关联业务信息
        /// </summary>
        /// <param name="relationMessage"></param>
        /// <returns></returns>
        public void SaveLocalOperationMessageRelation(OperationMessageRelation[] relation)
        {
            ClientBusinessOperationService.SaveLocalOperationMessageRelation(relation);
        }

        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        public void MailSoConfirmationToCustomer(Guid oceanBookingId, bool isEnglish)
        {
            ClientOceanExportService.MailSoConfirmationToCustomer(oceanBookingId, isEnglish);
        }

        /// <summary>
        /// SOD成功以后发送邮件给客服
        /// </summary>
        /// <param name="oceanBookingId">业务id</param>
        /// <param name="memoType">备注类型</param>
        /// <param name="path">操作路径</param>
        /// <param name="eventFlg">是否生成事件</param>
        public void MainSOCustomerServiceSOD(Guid oceanBookingId, MemoType memoType, string path, bool eventFlg)
        {
            ClientOceanExportService.MainSOCustomerServiceSOD(oceanBookingId, memoType, path, eventFlg);
        }
        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        /// <param name="oceanBookingId">业务ID </param>
        public string MailSoConfirmationToAgent(Guid oceanBookingId)
        {
            return ClientOceanExportService.MailSoConfirmationToAgent(oceanBookingId);
        }

        /// <summary>
        /// 判断是否存在关联
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <returns></returns>
        public bool ExistsRelation(string messageId)
        {
            return ClientBusinessOperationService.ExistsRelation(messageId);
        }
        /// <summary>
        /// 判断关联是否发送更改,如发送更改则返回关联，否则返回null.如果发送更改，则删除本地缓存的旧有关联，将新的关联保存到本地
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <param name="updateDate">消息的更改时间</param>
        /// <returns></returns>
        public OperationMessageRelation CheckRelationIsChanged(string messageId, DateTime? updateDate)
        {
            return ClientBusinessOperationService.CheckRelationIsChanged(messageId, updateDate);
        }

        /// <summary>
        /// 恢复订舱
        /// </summary>
        /// <param name="operationId">业务ID</param>
        public bool RestoreBooking(Guid operationId)
        {
            return OceanExportService.UpdateOceanBookingsState(LocalData.UserInfo.LoginID, operationId, OEOrderState.NewOrder);
        }

        #endregion

        #region   海进方法
        // <summary>
        /// 新增海运进口订单
        /// </summary>
        /// <param name="values"></param>
        public void AddOIOrder(IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.AddOrder(values, AfterEditPartSaved);

        }

        /// <summary>
        /// 新增海运进口业务
        /// </summary>
        /// <param name="values"></param>
        public void AddOIBusiness(IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.AddBusiness(values, AfterEditPartSaved);

        }

        /// <summary>
        /// 编辑海运进口订单
        /// </summary>
        /// <param name="showCriteria"></param>
        public void EditOIOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.EditOrder(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 编辑海运进口业务
        /// </summary>
        /// <param name="showCriteria"></param>
        public void EditOIBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.EditBusiness(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 复制海运进口订单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        public void CopyOIOrder(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.CopyOrder(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 复制海运进口业务
        /// </summary>
        /// <param name="showCriteria"></param>
        /// 
        /// <param name="values"></param>
        public void CopyOIBusiness(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.CopyBusiness(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 海运进口业务转移
        /// </summary>
        /// <param name="BusinessID"></param>
        /// <param name="values"></param>
        public void TransferOIBusiness(Guid BusinessID, IDictionary<string, object> values)
        {
            ClientOceanImportService.BusinessTransfer(BusinessID, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 海运进口取消业务
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="isCancel"></param>
        /// <param name="datetime"></param>
        public void CancelOIBusiness(Guid operationID, bool isCancel, DateTime? datetime)
        {
            ClientOceanImportService.CancelOIBusiness(operationID, isCancel, datetime, AfterEditPartSaved);
        }

        /// <summary>
        /// 打开海运进口账单
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="currentFormID"></param>
        public void OpenOIBill(Guid businessID)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.OpenBill(businessID);
        }

        /// <summary>
        /// 海进业务下载
        /// </summary>
        public void OIDownLoad()
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.OIDownLoad();
        }

        /// <summary>
        /// 打开提货通知书
        /// </summary>
        /// <param name="businessID">业务号</param>
        /// <param name="values"></param>
        public void OpenDeliveryNotice(Guid businessID, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.OpenDeliveryNotice(businessID, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 确认放货
        /// </summary>
        /// <param name="list"></param>
        public void ConfirmDelivery(Guid opertionid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.ConfirmDelivery(opertionid, AfterEditPartSaved);
        }

        /// <summary>
        /// 取消放货
        /// </summary>
        /// <param name="list"></param>
        public void CancelDelivery(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.CancelDelivery(operationid, AfterEditPartSaved);
        }

        /// <summary>
        /// 第三方代理放单
        /// </summary>
        /// <param name="oibookingid"></param>
        /// <param name="rbld"></param>
        /// <param name="changebyid"></param>
        public void ReleaseOIBL(Guid oibookingid, bool rbld, Guid changebyid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.ReleaseOIBL(oibookingid, rbld, changebyid, AfterEditPartSaved);
        }

        /// <summary>
        /// 海进异常放货申请
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationNo"></param>
        public void OIExceptionReleaseRC(Guid operationID, string operationNo)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.ExceptionReleaseRC(operationID, operationNo);
        }

        /// <summary>
        /// 签收放单
        /// </summary>
        /// <param name="operationidList"></param>
        public void OIReceiveRN(List<Guid> operationidList)
        {
            ClientHelper.ActiveMainForm();
            foreach (var operationid in operationidList)
            {
                ClientOceanImportService.OIReceiveRN(operationid, AfterEditPartSaved);
            }
        }

        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void OIDelivery(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.OIDelivery(operationid, AfterEditPartSaved);
        }

        /// <summary>
        /// 申请放单
        /// </summary>
        /// <param name="editPartSaved"></param>
        public void ApplyRelease(List<Guid> operationidlist)
        {
            ClientHelper.ActiveMainForm();
            foreach (var operationid in operationidlist)
            {
                ClientOceanImportService.ApplyRelease(operationid, AfterEditPartSaved);
            }
        }

        /// <summary>
        /// 催港前放单(发送邮件给港前放单人员和港前客服)
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        /// <param name="hblno">Hbl号</param>
        /// <param name="hblRelease">HBL放单方式</param>
        /// <param name="mblNo">mbl号</param>
        /// <param name="mblRelease">MBL放单方式</param>
        /// <param name="eta">ETA</param>
        public void NoticeRelease(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.NoticeRelease(operationid, AfterEditPartSaved);
        }

        /// <summary>
        /// 同意放货
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="editPartSaved"></param>
        public void AgreeRC(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.AgreeRC(operationid, AfterEditPartSaved);
        }

        /// <summary>
        /// 收到MBL正本
        /// </summary>
        /// <param name="operationid"></param>
        public void OIOMBLRcved(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.OIOMBLRcved(operationid);
        }

        /// <summary>
        /// 财务寄出MBL正本
        /// </summary>
        /// <param name="operationid"></param>
        public void MailDMBL(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.MailDMBL(operationid);
        }

        /// <summary>
        /// 海出业务关帐/取消关帐
        /// </summary>
        /// <param name="operationID">业务ID</param>
        public void OIStateAccountingClose(Guid operationID)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.StateAccountingClose(operationID, AfterEditPartSaved);
        }

        /// <summary>
        /// 打印发货通知书
        /// </summary>
        /// <param name="businessList"></param>
        public void PrintReleaseOrder(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.PrintReleaseOrder(operationid);
        }

        /// <summary>
        /// 打印货代提单
        /// </summary>
        /// <param name="businessList"></param>
        public void PrintForwardingBill(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.PrintForwardingBill(operationid);
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="list"></param>
        public void PrintProfit(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.PrintProfit(operationid);
        }

        /// <summary>
        /// 打印出口业务信息报表
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="customerID"></param>
        public void PrintExportBusinessInfo(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.PrintExportBusinessInfo(operationid);
        }

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="list"></param>
        public void PrintArrivalNotice(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.PrintArrivalNotice(operationid);
        }

        /// <summary>
        /// 打印海进工作表
        /// </summary>
        /// <param name="businessInfo"></param>
        public void OIPrintWorkSheet(Guid operationid)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.OIPrintWorkSheet(operationid);
        }

        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        /// <returns></returns>
        public void MailPickUpToCustomer(Guid operationid, bool isEnglish)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.MailPickUpToCustomer(operationid, isEnglish);
        }

        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">发送邮件版本</param>
        public void MailAnToCustomer(Guid operationid, bool isEnglish)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.MailAnToCustomer(operationid, isEnglish);
        }


        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="operationguids">业务ID</param>
        public void OiPayment(List<Guid> operationguids)
        {
            BillListQueryCriteria buBillListQueryCriteria =
                new BillListQueryCriteria();
            buBillListQueryCriteria.OperationIds = operationguids;
            FinanceClientService.ShowBillList(buBillListQueryCriteria);
        }

        /// <summary>
        /// NEW 发货
        /// </summary>
        /// <param name="operationguids">需要操作放货的集合</param>
        public void OiNewOiDelivery(List<Guid> operationguids)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.NewOiDelivery(operationguids, AfterEditPartSaved);
        }


        /// <summary>
        /// 发送催款邮件
        /// </summary>
        /// <param name="operationId">业务ID</param>
        public void PayNtMail(Guid operationId)
        {
            ClientHelper.ActiveMainForm();
            ClientOceanImportService.PayNtMail(operationId);
        }

        /// <summary>
        /// 发送催款邮件（自动发送）
        /// </summary>
        public void PayNtautomaticMail()
        {
            ClientOceanImportService.PayNtMail(Guid.Empty);
        }

        /// <summary>
        /// 催代理分发文件信息
        /// </summary>
        /// <param name="operationid">业务ID</param>
        /// <param name="isEnglish">中英文版本</param>
        public void MailOverseaAgent(Guid operationid, bool isEnglish)
        {
            ClientOceanImportService.MailOverseaAgent(operationid, isEnglish, AfterEditPartSaved);
        }

        /// <summary>
        /// ETA 前5天催清关文件
        /// </summary>
        /// <param name="operationid"></param>
        public void MailCustomrequest(Guid operationid)
        {
            ClientOceanImportService.MailCustomrequest(operationid, AfterEditPartSaved);
        }

        /// <summary>
        /// ETA 变更通知
        /// </summary>
        public void MailEtachange(Guid operationid)
        {
            ClientOceanImportService.MailEtachange(operationid, AfterEditPartSaved);
        }

        /// <summary>
        /// FULLY RELEASE 之后柜子AVAILABILITY通知
        /// </summary>
        /// <param name="operationid"></param>

        public void MailContaineravailableforpickup(Guid operationid)
        {
            ClientOceanImportService.MailContaineravailableforpickup(operationid, AfterEditPartSaved);
        }

        /// <summary>
        ///  LFD 通知
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="lastFreeDate"></param>

        public void MailLfdnotice(Guid operationid, string lastFreeDate, string content)
        {
            ClientOceanImportService.MailLfdnotice(operationid, lastFreeDate, AfterEditPartSaved, content);
        }

        /// <summary>
        /// LFD 前2工作日后每天提柜提醒
        /// </summary>
        /// <param name="operationid"></param>
        public void MailContainerpickupreminder(Guid operationid)
        {
            ClientOceanImportService.MailContainerpickupreminder(operationid, AfterEditPartSaved);
        }

        /// <summary>
        /// 提柜后第2天开始每天还空提醒
        /// </summary>
        /// <param name="operationid"></param>
        public void MailEmptyreturnnotice(Guid operationid)
        {
            ClientOceanImportService.MailEmptyreturnnotice(operationid, AfterEditPartSaved);
        }

        #endregion

        #region   空出方法
        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="values"></param>
        public void AirExportAddData(IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.AddData(values, AfterEditPartSaved);
        }

        /// <summary>
        /// 复制订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        public void AirExportCopyData(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.CopyData(showCriteria, values, AfterEditPartSaved);
        }
        /// <summary>
        /// 编辑订舱单
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        public void AirExportEditData(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.EditData(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 取消订舱单
        /// </summary>
        /// <param name="airBookingId"></param>
        public void AirExportCancelData(Guid airBookingId)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.CancelData(airBookingId, AfterEditPartSaved);
        }
        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="values"></param>
        public void AirExportReplyAgent(Guid bookingId, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.ReplyAgent(bookingId, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 打开提单列表
        /// </summary>
        /// <param name="airBookingId"></param>
        public void AirExportOpenBl(Guid airBookingId)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.OpenBl(airBookingId);
        }
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="airBookingId"></param>
        public void AirExportOpenBill(Guid airBookingId)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.OpenBill(airBookingId);
        }
        /// <summary>
        /// 打印联单
        /// </summary>
        /// <param name="airBookingId"></param>
        public void AirExportPrintOrder(Guid airBookingId)
        {
            ClientHelper.ActiveMainForm();
            AirExportService.PrintOrder(airBookingId);
        }

        #endregion

        #region  空进方法

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="values"></param>
        public void AirImportAddBooking(IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.AddBooking(values, AfterEditPartSaved);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        public void AirImportCopyBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.CopyBooking(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="showCriteria"></param>
        /// <param name="values"></param>
        public void AirImportEditBooking(EditPartShowCriteria showCriteria, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.EditBooking(showCriteria, values, AfterEditPartSaved);
        }

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportOpenBill(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.OpenBill(bookingId);
        }

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportPrintArrivalNotice(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.PrintArrivalNotice(bookingId);
        }
        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportPrintReleaseOrder(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.PrintReleaseOrder(bookingId);
        }
        /// <summary>
        ///  打印利润表
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportPrintProfit(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.PrintProfit(bookingId);
        }
        /// <summary>
        /// 打印Authority To Make Entry
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportPrintAuthority(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.PrintAuthority(bookingId);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportCancelBooking(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.CancelBooking(bookingId, AfterEditPartSaved);
        }

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="values"></param>
        public void AirImportOpenCargoBook(Guid bookingId, IDictionary<string, object> values)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.OpenCargoBook(bookingId, values, AfterEditPartSaved);
        }
        /// <summary>
        /// 下载
        /// </summary>
        public void AirImportAiDownLoad()
        {
            ClientHelper.ActiveMainForm();
            AirImportService.AiDownLoad();
        }
        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportBusinessTransfer(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.BusinessTransfer(bookingId);
        }

        /// <summary>
        /// 放货和取消放货
        /// </summary>
        /// <param name="bookingId"></param>
        public void AirImportAiDelivery(Guid bookingId)
        {
            ClientHelper.ActiveMainForm();
            AirImportService.AiDelivery(bookingId);
        }

        #endregion

        #region 其他业务

        /// <summary>
        /// 新增
        /// </summary>
        public void OtherBusinessAddData()
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.AddData(AddType.OtherBusiness, AfterEditPartSaved);
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        public void OtherBusinessCopyData(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.CopyData(AddType.OtherBusiness, operationId, companyID, AfterEditPartSaved);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        public void OtherBusinessEditData(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.EditData(AddType.OtherBusiness, operationId, companyID, AfterEditPartSaved);
        }

        /// <summary>
        /// 取消业务/恢复业务
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        public void OtherBusinessCancelData(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.CancelData(operationId, companyID, AfterEditPartSaved);
        }

        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        public void OtherBusinessBill(Guid operationId)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.Bill(operationId);
        }

        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        public void OtherBusinessVerifiSheet(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.VerifiSheet(operationId, companyID);
        }

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        public void OtherBusinessPickUp(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.PickUp(operationId, companyID);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        public void OtherBusinessPrintOrder(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.PrintOrder(operationId, companyID);
        }

        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="companyID">订单口岸ID</param>
        public void OtherBusinessProfit(Guid operationId, Guid companyID)
        {
            ClientHelper.ActiveMainForm();
            ClientOtherBusiness.Profit(operationId, companyID);
        }

        #endregion

        #region 通用
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="operationId">业务Id</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="formType">表单类型</param>
        public void OpenBill(Guid operationId, OperationType operationType, FormType formType = FormType.Unknown)
        {
            ClientHelper.ActiveMainForm();
            switch (operationType)
            {
                case OperationType.OceanExport:
                    ClientOceanExportService.OpenBill(operationId, operationType);
                    break;
                case OperationType.Other:
                    ClientOtherBusiness.Bill(operationId);
                    break;
            }

        }
        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="formType">表单类型</param>
        public void VerifiSheet(Guid operationId, string operationNo, OperationType operationType, FormType formType = FormType.Unknown)
        {
            ClientHelper.ActiveMainForm();
            switch (operationType)
            {
                case OperationType.Other:
                    ClientOtherBusiness.VerifiSheet(operationId, operationNo);
                    break;
            }
        }
        #endregion

        #region CSP Booking
        /// <summary>
        /// CSP Booking Download
        /// </summary>
        public void CSPBookingDownload()
        {
            ClientHelper.ActiveMainForm();
            workitem.Commands[FCMCommonContants.COMMAND_CSPBOOKING_DOWNLOAD].Execute();
        }
        #endregion

        #region 任务中心手动添加事件
        /// <summary>
        /// 任务中心手动添加事件
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <param name="eventObjects">事件实体对象</param>
        public bool TaskCenterSaveEvent(OperationType operationType, EventObjects eventObjects)
        {
            bool evaentFlg = false;
            if (operationType != OperationType.Unknown && eventObjects != null)
            {
                var business = new List<BusinessSaveParameter>();
                var dictionary = new Dictionary<string, object>
                            {
                                {"OceanBookingID", eventObjects.OperationID},
                                {eventObjects.Code,eventObjects.ModifyValue},
                                {"OperationType",operationType}
                            };
                switch (eventObjects.Code)
                {
                    case "CFA":
                        dictionary.Add("CFAManual", 1);
                        break;
                    case "CFD":
                        dictionary.Add("CFDManual", 1);
                        break;
                    case "TrkA":
                        dictionary.Add("TRKAManual", 1);
                        break;
                    case "TrkD":
                        dictionary.Add("TRKDManual", 1);
                        break;
                    default:
                        break;
                }

                var businessSave = new BusinessSaveParameter { items = dictionary };
                business.Add(businessSave);
                BusinessQueryService.Save(business);
                BusinessState businessState = ReturnBusinessState(eventObjects.Code, operationType);
                if (!string.IsNullOrEmpty(businessState.AssociatedEvent))
                {
                    BusinessState busines = ReturnBusinessState(businessState.AssociatedEvent, operationType);
                    if (!string.IsNullOrEmpty(busines.Methods))
                    {
                        ServiceClient.GetClientService<WorkItem>().Commands[busines.Methods].Execute();
                    }
                }
                evaentFlg = true;
                //事件代码如果为SOD 执行发送邮件并且刷新列表
                if (eventObjects.Code.ToUpper() == "SOD")
                {
                    ClientOceanExportService.CheckMainSOCustomerServiceSOD(eventObjects.OperationID, eventObjects);
                    ServiceClient.GetClientService<WorkItem>().Commands["Command_ListRefresh"].Execute();
                }
            }
            return evaentFlg;
        }
        /// <summary>
        /// 返回可以修改的事件实体
        /// </summary>
        /// <param name="caption">事件名称</param>
        /// <returns></returns>
        public BusinessState ReturnBusinessState(string caption, OperationType operationType)
        {
            List<BusinessState> businessStates = null;
            BusinessStateReadXml businessStateReadXml = new BusinessStateReadXml();
            //接收可手动修改的字段信息 根据业务类型过滤

            businessStates = businessStateReadXml.GetBusinessState().Where(n => n.OperationType == operationType).ToList();

            //当前单元格是否可以编辑
            BusinessState records =
                (from a in businessStates where a.Name.ToUpper() == caption.ToUpper() select a)
                    .FirstOrDefault();
            return records;
        }

        #endregion

        #region 消息相关
        /// <summary>
        /// 显示发送界面
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>点击发送,返回True，取消发送返回False</returns>

        public bool ShowSendForm(Message.ServiceInterface.Message message)
        {
            return ClientMessageService.ShowSendForm(message);
        }
        /// <summary>
        /// 显示预览界面
        /// </summary>
        /// <param name="entry"></param>

        public void ShowReadForm(Message.ServiceInterface.Message message)
        {
            ClientMessageService.ShowReadForm(message);
        }
        /// <summary>
        /// 发送并保存日志
        /// </summary>
        /// <param name="message"></param>

        public void SendAndSaveLog(Message.ServiceInterface.Message message)
        {
            ClientMessageService.SendAndSaveLog(message);
        }
        /// <summary>
        /// 将邮件文件转换为PDF文件
        /// </summary>
        /// <param name="mailFile"></param>
        /// <returns></returns>

        public string ConvertMailToPDF(string mailFile)
        {
            return ClientMessageService.ConvertMailToPDF(mailFile);
        }

        /// <summary>
        /// 打开Msg文件
        /// </summary>
        /// <param name="MsgFilePath"></param>

        public void Open(string msgFilePath)
        {
            ClientMessageService.Open(msgFilePath);
        }
        /// <summary>
        /// 通过传真或者邮件中心打开消息
        /// </summary>
        /// <param name="id">消息主键Id</param>
        public void Open(Guid id)
        {
            ClientMessageService.Open(id);
        }
        /// <summary>
        /// 把Message转换为Msg文件，并在邮件客户端显示
        /// </summary>
        /// <param name="message"></param>

        public void ConvertMessageToMsg(Message.ServiceInterface.Message message)
        {
            ClientMessageService.ConvertMessageToMsg(message);
        }


        public ManyResult[] SaveMessage(Message.ServiceInterface.Message message)
        {
            return ClientBusinessOperationService.SaveMessage(message);
        }
        private void HandleMessageRelationChange(OperationMessageRelation relation)
        {
            Guid operationID = relation.OperationID;
            OperationType operationType = relation.OperationType;
            UpdateLocalBusinessData(operationID, operationType);
            string tip = LocalData.IsEnglish ? "Other user has archived current mail,PLS select other mail in mail list and then select the current mail again to refresh data." : "其他用户已归档此邮件，请在邮件列表选择其他邮件并再次选择当前邮件以刷新数据!";
            throw new ICPException(tip);
        }


        public SingleResult SendMessage(Message.ServiceInterface.Message message)
        {
            return ClientMessageService.Send(message);
        }

        public Message.ServiceInterface.Message GetMessageInfoById(Guid MessageID)
        {
            return ClientMessageService.GetMessageInfoById(MessageID);
        }


        #endregion

        #region 刷新本地缓存数据
        /// <summary>
        /// 更新本地缓存业务数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="dt"></param>
        public void UpdateLocalBusinessData(Guid operationId, OperationType operationType)
        {
            UpdateLocalBusinessData(new List<Guid> { operationId }, operationType);
        }


        /// <summary>
        /// 根据发件人地址获取发件人的类型
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <returns></returns>
        public EmailSourceType GetEmailSourceType(string senderAddress)
        {
            return ClientBusinessOperationService.GetEmailType(senderAddress);
        }
        /// <summary>
        /// 保存业务和联系人到FCM.OceanContacts, 保存业务联系人FCM.OperationContactCache
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="contactList"></param>
        /// <returns></returns>
        public ManyResult SaveAndSyncOperationContacts(List<string> emails, List<CustomerCarrierObjects> contactList)
        {
            contactList = contactList.GroupBy(s => s.Id).Select(s => s.First()).ToList();

            ManyResult results = FCMCommonService.SaveContactList(contactList);
            List<OperationContactInfo> contactInfos = BusinessContactService.GetOperationContactByEmails(emails);
            ClientBusinessContactService.LocalSaveOperationContacts(contactInfos);
            return results;
        }

        #endregion

        #region IICPCommonOperationService 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public DataTable GetOperationViewList(BusinessQueryCriteria criteria)
        {
            return ClientBusinessOperationService.GetLocalOperationViewList(criteria);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<OperationMessageRelation> GetOperationMessageRelationByMessageIdAndReference(string messageId, string reference)
        {
            return ClientBusinessOperationService.GetOperationMessageRelationByMessageIdAndReference(messageId, reference);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="messageID"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string reference)
        {
            return ClientBusinessOperationService.Get(criteria, messageID, reference);
        }
        /// <summary>
        /// 保存业务与消息的关系信息，并确保本地缓存存在此业务
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="isEnsureShipmentExists"></param>
        /// <returns></returns>
        public SingleResult SaveOperationMessageRelationAndEnsureShipmentExists(OperationMessageRelation relation, bool isEnsureShipmentExists)
        {
            if (isEnsureShipmentExists)
            {
                UpdateLocalBusinessData(relation.OperationID, relation.OperationType);
            }
            return SaveOperationMessageRelation(new OperationMessageRelation[1] { relation });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationIds"></param>
        /// <param name="operationType"></param>
        public void UpdateLocalBusinessData(List<Guid> operationIds, OperationType operationType)
        {
            ClientBusinessOperationService.UpdateLocalBusinessData(operationIds, operationType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationMessages"></param>
        /// <returns></returns>
        public ManyResult SaveOperationMessageRelation(OperationMessageRelation[] relationMessages)
        {
            return ClientBusinessOperationService.SaveOperationMessageRelation(relationMessages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationMessage"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<OperationMessageRelation> EnsureAndSaveMailMessageReference(OperationMessageRelation relationMessage, Message.ServiceInterface.Message message)
        {
            return ClientBusinessOperationService.EnsureAndSaveMailMessageReference(relationMessage, message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool[] GetBoolArray()
        {
            return new bool[1] { false };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public DataTable GetOperationViewInfo(Guid operationId, OperationType operationType)
        {
            return ClientBusinessOperationService.GetLocalOperationViewInfo(operationId, operationType);
        }

        #endregion

        #region    询价发送邮件
        /// <summary>
        /// 根据询价ID查询出当前询问人，并新建一份空白的邮件
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        public void EmailMailtoAskpeople(Guid inquerireid)
        {
            ClientInquireRateService.EmailMailtoAskpeople(inquerireid);
        }
        /// <summary>
        /// 邮件发给承运人
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        public void EmailtoCarrier(Guid inquerireid)
        {
            ClientInquireRateService.EmailtoCarrier(inquerireid);
        }
        #endregion

        #region 邮件中心使用的方法
        /// <summary>
        /// 打开联系人面板
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationNo">业务号</param>
        public void ShowContactsAndAssistants(Guid operationId, OperationType operationType, string operationNo)
        {
            ClientOceanExportService.ShowContactsAndAssistants(operationId, operationType, operationNo);
        }

        /// <summary>
        /// 上传附件通用方法，打开上传附件界面
        /// </summary>
        /// <param name="way">附件方式</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="mailSubject">主题</param>
        /// <param name="objMailItem">邮件实体</param>
        /// <param name="type">工作类型</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="addDocList">附件集合</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="upDateTime">修改时间</param>
        /// <param name="message">MESSAGE对象</param>
        public void Upload(UploadWay way, Guid operationId, string mailSubject, object objMailItem,
            SelectionType type, OperationType operationType,
            List<string> addDocList, string operationNo, DateTime? upDateTime, Message.ServiceInterface.Message message)
        {
            ClientOceanExportService.Upload(way, operationId, mailSubject, objMailItem, type, operationType, addDocList,
                operationNo, upDateTime, message, TemplateCode);
        }

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        public void ShowContactPart(AssociateType associateType, List<BusinessOperationContext> bussOperationContexts,
         Message.ServiceInterface.Message message)
        {
            ClientOceanExportService.ShowContactPart(associateType, bussOperationContexts, message);
        }

        /// <summary>
        /// 关联的方法
        /// </summary>
        /// <param name="contactType">联系人类型</param>
        /// <param name="bussOperationContexts">需要关联的业务集合</param>
        /// <param name="message">Message对象</param>
        public void SaveContactList(ContactType contactType, List<BusinessOperationContext> bussOperationContexts,
            Message.ServiceInterface.Message message)
        {
            ClientOceanExportService.SaveContactList(contactType, bussOperationContexts, message);
        }


        /// <summary>
        /// 邮件查询
        /// </summary>
        /// <param name="no">业务号</param>
        /// <param name="noType">业务类型</param>
        /// <param name="types">分类</param>
        /// <param name="dateType">时间类型</param>
        /// <param name="main">邮件地址</param>
        /// <param name="area">时间段</param>
        /// <param name="text">标题</param>
        /// <param name="operationId">业务号</param>
        /// <param name="operationType">业务类型</param>
        public void OeEmailQueryPartShow(string no, int noType, int types, int dateType, string main, int? area, string text, Guid? operationId, OperationType operationType)
        {
            ClientOceanExportService.OeEmailQueryPartShow(no, noType, types, dateType, main, area, text, operationId, operationType);
        }
        /// <summary>
        /// 邮件中心是否刷新
        /// </summary>
        /// <returns></returns>
        public bool MailCenterRefresh(bool? flg)
        {
            return ClientOceanExportService.MailCenterRefresh(flg);
        }

        /// <summary>
        /// 高级查询
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        public string Advancedquery(OperationType operationType)
        {
            return ClientOceanExportService.Advancedquery(operationType);
        }


        /// <summary>
        /// 判断当前联系人是否存在 
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <param name="message">邮件</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="operation">业务类型</param>
        /// <returns></returns>
        public bool SaveOperationMessage(AssociateType associateType, Message.ServiceInterface.Message message, Guid operationId, OperationType operation)
        {
            return ClientOceanExportService.SaveOperationMessage(associateType, message, operationId, operation);
        }

        #region Outlook Plugin

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回Datatable
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>DataTable</returns>
        public DataTable GetOperationMessageRelationDataTableByMessageID(string messageID)
        {
            return ClientBusinessOperationService.GetOperationMessageRelationByMessageId(messageID);
        }

        /// <summary>
        /// 根据邮件ID，获取关联信息，返回单个对象
        /// </summary>
        /// <param name="messageID">邮件MessageID</param>
        /// <returns>OperationMessageRelation</returns>
        public OperationMessageRelation GetOperationMessageRelationByMessageID(string messageID)
        {
            return ClientBusinessOperationService.GetOperationMessageRelationByMessageID(messageID);
        }


        /// <summary>
        /// 查找业务集合：固定SQL
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        /// <rereturns>DataTable</rereturns>
        public DataTable GetOperationViewListFixed(BusinessQueryCriteria criteria)
        {
            return ClientBusinessOperationService.GetOperationViewListFixed(criteria);
        }

        /// <summary>
        /// 联系人是否存在缓存
        /// </summary>
        /// <param name="ExternalEmails">联系人集合</param>
        /// <returns>是否存在:True存在；False不存在；</returns>
        public bool IsAllContactExsitCache(List<string> ExternalEmails)
        {
            return ClientBusinessOperationService.IsAllContactExsitCache(ExternalEmails);
        }

        /// <summary>
        /// 保存联系人
        /// </summary>
        /// <param name="ContactParameters">联系人集合</param>
        public void SaveLocalOperationContactMail(List<OperationContactParameters> ContactParameters)
        {
            ClientBusinessOperationService.SaveLocalOperationContactMail(ContactParameters);
        }

        /// <summary>
        /// 删除服务端数据库关联信息和本地缓存关联信息
        /// </summary>
        /// <param name="messageRelationIds">关联信息ID集合</param>
        /// <param name="updateDates">更新时间</param>
        public void RemoveAndSyncOperationMessageRelations(Guid[] messageRelationIds, DateTime?[] updateDates)
        {
            ClientBusinessOperationService.RemoveAndSyncOperationMessageRelations(messageRelationIds, updateDates);
        }

        /// <summary>
        /// 根据邮件地址，获取联系人类型
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>如果本地不存在，则返回null</returns>
        public int? GetContactPersonType(string emailAddress)
        {
            return ClientBusinessOperationService.GetContactPersonType(emailAddress);
        }
        ///// <summary>
        ///// 写入操作日志
        ///// </summary>
        ///// <param name="watchTime">计时器</param>
        ///// <param name="AssemblyNames">组件名称</param>
        ///// <param name="FunctionName">操作内容</param>
        //public void WriteStopwatchLog(string watchTime, string AssemblyNames, string FunctionName)
        //{
        //}
        #endregion


        #endregion
    }
}

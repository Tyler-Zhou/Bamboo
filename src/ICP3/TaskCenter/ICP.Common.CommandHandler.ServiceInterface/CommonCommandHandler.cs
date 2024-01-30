using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using ICP.Business.Common.ServiceInterface;
using ICP.Business.Common.UI.Contact;
using ICP.Business.Common.UI.Document;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Forms;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 公共命令处理类
    /// </summary>
    public class CommonCommandHandler : IBaseComnandHandler
    {
        #region Fields & Property & Service
        /// <summary>
        /// Lock Object
        /// </summary>
        public static object obj = new object();

        /// <summary>
        /// Work Item
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        /// <summary>
        /// 当前的业务处理面板
        /// </summary>
        public BaseBusinessPart CurrentBaseBusinessPart { get; set; }

        #region CompanyIDs
        /// <summary>
        /// 公司ID列表
        /// </summary>
        private Guid[] _CompanyIDs;
        /// <summary>
        /// 公司ID列表
        /// </summary>
        public Guid[] CompanyIDs
        {
            get
            {
                if (_CompanyIDs == null)
                {
                    string[] arrCompanyIDs = listBaseBusinessPart.SelectedCompanyIds.Split(new char[] { ',' }).ToArray();
                    _CompanyIDs = Array.ConvertAll<string, Guid>(arrCompanyIDs,
                                                                 delegate(string n) { return new Guid(n); });
                }

                return _CompanyIDs;
            }
        }
        #endregion

        /// <summary>
        /// 基础业务面板
        /// </summary>
        public IBaseBusinessPart_New BusinessPartNew { get; set; }

        /// <summary>
        /// 业务列表面板
        /// </summary>
        public ListBaseBusinessPart listBaseBusinessPart
        {
            get { return BusinessPartNew as ListBaseBusinessPart; }
        }

        /// <summary>
        /// 单选客户或承运人角色的联系人列表
        /// </summary>
        private UCSelectedContactListPart _contactListPart;
        /// <summary>
        /// 单选客户或者承运人面板
        /// </summary>
        public UCSelectedContactListPart ContactListPart
        {
            get
            {
                if (_contactListPart == null)
                    _contactListPart = RootWorkItem.Items.AddNew<UCSelectedContactListPart>();
                return _contactListPart;
            }
        }

        /// <summary>
        /// 参与者列表面板
        /// </summary>
        private UCContactStaffListPart _StaffListPart;

        /// <summary>
        /// 参与者列表面板
        /// </summary>
        public UCContactStaffListPart StaffListPart
        {
            get
            {
                if (_StaffListPart == null)
                    _StaffListPart = RootWorkItem.Items.AddNew<UCContactStaffListPart>();
                return _StaffListPart;
            }
        }

        /// <summary>
        /// 是否显示的是参与者面板
        /// </summary>
        private bool? isShowAssistantPart { get; set; }

        /// <summary>
        /// 客户端业务操作接口
        /// </summary>
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get { return ServiceClient.GetClientService<IClientBusinessOperationService>(); }
        }

        /// <summary>
        /// 业务联系人信息服务接口
        /// </summary>
        public IBusinessContactService BusinessContactService
        {
            get { return ServiceClient.GetService<IBusinessContactService>(); }
        }


        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IFCMCommonService FcmCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }

        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }

        /// <summary>
        /// 消息(Mail/EDI/Fax)服务
        /// </summary>
        public IMessageService MessageService
        {
            get { return ServiceClient.GetService<IMessageService>(); }
        }

        /// <summary>
        /// 业务联系信息服务客户端接口
        /// </summary>
        public IClientBusinessContactService ClientBusinessContactService
        {
            get { return ServiceClient.GetClientService<IClientBusinessContactService>(); }
        }

        /// <summary>
        /// 公共服务接口
        /// </summary>
        public IICPCommonOperationService CommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }
        }
        #endregion

        /// <summary>
        /// 返回业务上下文子菜单项所关联的业务号（比如SO,MBL No.,HBL NO.等)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <returns>业务号</returns>
        public string GetContextMenuItemBusinessRelatedNo(object sender, bool isThrowException)
        {
            Command command = sender as Command;
            if (command == null)
                return string.Empty;
            if (command.Adapters.Count < 1)
                return string.Empty;

            List<ToolStripItem> items =
                command.FindAdapters<ToolStripItemCommandAdapter>().Last().Invokers.Keys.ToList();
            if (items == null || items.Count <= 0)
                return string.Empty;
            ToolStripItem item = items.Last();
            if (item == null || item.Tag == null)
            {
                if (!isThrowException)
                    return string.Empty;
                else
                {
                    throw new NullReferenceException();
                }
            }
            return item.Tag as string;
        }

        /// <summary>
        /// 创造业务参数
        /// </summary>
        /// <param name="actionType">动作类型</param>
        /// <param name="isNewOrder">是否新订单</param>
        /// <returns></returns>
        public Dictionary<string, object> CreateBusinessParameter(
            ActionType actionType, bool isNewOrder)
        {
            BusinessOperationParameter businessOperation = new BusinessOperationParameter();

            businessOperation.TemplateCode = CommonOperationService.GetTemplateCode();
            businessOperation.Message = MailHelper.GetMailInfo();

            if (actionType == ActionType.Edit)
            {
                businessOperation.Context = CreateBusinessOperationContext();
            }
            else
            {
                if (isNewOrder)
                    businessOperation.Context = new BusinessOperationContext();
                else
                {
                    businessOperation.Context = CreateBusinessOperationContext();
                }
            }
            businessOperation.ActionType = actionType;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("businessOperationParameter", businessOperation);
            return dic;
        }

        /// <summary>
        /// 刷新数据源
        /// </summary>
        public void Refersh()
        {
            CurrentBaseBusinessPart.Refresh();
        }

        /// <summary>
        /// 上传SI附档
        /// </summary>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件实体</param>
        public void UploadingSi(string mailSubject, object objMailItem)
        {
            Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.SI,
                   OperationType.OceanExport);
        }
        /// <summary>
        /// 上传MBL copy       
        /// </summary>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件实体</param>
        public void UploadMBL(string mailSubject, object objMailItem)
        {
            Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.MBL, OperationType.OceanExport);
        }

        /// <summary>
        /// 上传AN Copy
        /// </summary>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件实体</param>
        public void UploadAN(string mailSubject, object objMailItem)
        {
            Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.AN, OperationType.OceanImport);
        }
        /// <summary>
        /// 上传AN Copy
        /// </summary>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件实体</param>
        public void UploadAP(string mailSubject, object objMailItem)
        {
            Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.AP, OperationType.OceanExport);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件实体</param>
        public void UploadAttachment(string mailSubject, object objMailItem)
        {
            Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem,
                   SelectionType.Normal, CurrentBaseBusinessPart.OperationType);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件实体</param>
        public void UploadAttachment(UploadWay uploadWay, string mailSubject, object objMailItem, List<string> addDocList)
        {
            Upload(uploadWay, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.Normal, CurrentBaseBusinessPart.OperationType, addDocList);
        }

        /// <summary>
        /// 上传SO附件
        /// </summary>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件实体</param>
        public void UploadSO(string mailSubject, object objMailItem)
        {
            Upload(UploadWay.DirectOpen, CurrentBaseBusinessPart.OperationID, mailSubject, objMailItem, SelectionType.SO,
                   OperationType.OceanExport);
        }

        /// <summary>
        /// 上传附件通用方法，打开上传附件界面
        /// </summary>
        /// <param name="way">上传附件方式</param>
        /// <param name="operationID">业务ID</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件对象</param>
        /// <param name="type">上传附件选择工作类型</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="addDocList">附件名列表</param>
        public void Upload(UploadWay way, Guid operationID, string mailSubject, object objMailItem, SelectionType type, OperationType operationType, List<string> addDocList)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    lock (obj)
                    {
                        if (CurrentBaseBusinessPart != null)
                        {
                            using (frmUploadSIAttachment frmUploadSiAttachment = new frmUploadSIAttachment())
                            {
                                if (operationType == OperationType.OceanExport)
                                {

                                    OceanBookingInfo oceanbookinginfo =
                                        ServiceClient.GetService<IOceanExportService>()
                                                     .GetOceanBookingInfo(operationID);
                                    if (oceanbookinginfo == null)
                                    {
                                        LocalCommonServices.ErrorTrace.SetErrorInfo(CurrentBaseBusinessPart.FindForm(),
                                                                                    (LocalData.IsEnglish
                                                                                         ? "The selected shipment is not exsits!"
                                                                                         : "选择的业务不存在!"));
                                        return;
                                    }

                                    UploadAttachmentParameter parameter = CreateUploadAttachmentParameter(way, type,
                                                                                                          operationType,
                                                                                                          oceanbookinginfo,
                                                                                                          null,
                                                                                                          objMailItem,
                                                                                                          CurrentBaseBusinessPart
                                                                                                              .TemplateCode);
                                    frmUploadSiAttachment.BindingData(parameter);
                                }
                                else if (operationType == OperationType.OceanImport)
                                {

                                    UploadAttachmentParameter parameter = CreateUploadAttachmentParameter(way, type,
                                                                                                          operationType,
                                                                                                          null,
                                                                                                          CreateBusinessOperationContext
                                                                                                              (),
                                                                                                          objMailItem,
                                                                                                          CurrentBaseBusinessPart
                                                                                                              .TemplateCode);
                                    frmUploadSiAttachment.BindingData(parameter);
                                }
                                frmUploadSiAttachment.Show();
                                if (addDocList.Count != 0)  //添加附件
                                    frmUploadSiAttachment.UCBusinessDocumentList.AddDocuments(addDocList, null, true, null);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
                }
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="way">上传附件方式</param>
        /// <param name="operationID">业务ID</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="objMailItem">邮件对象</param>
        /// <param name="type">上传附件选择工作类型</param>
        /// <param name="operationType">业务类型</param>
        public void Upload(UploadWay way, Guid operationID, string mailSubject, object objMailItem, SelectionType type, OperationType operationType)
        {
            Upload(way, operationID, mailSubject, objMailItem, type, operationType, new List<string>());
        }

        /// <summary>
        /// 构造上传附件参数实体
        /// </summary>
        /// <param name="way">上传附件方式</param>
        /// <param name="attachmentType">上传附件选择工作类型</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="oceanBookingInfo">订舱详细信息</param>
        /// <param name="operationContext">业务操作上下文类</param>
        /// <param name="mailItem">邮件实体</param>
        /// <param name="templateCode">模板编码</param>
        /// <returns></returns>
        private UploadAttachmentParameter CreateUploadAttachmentParameter(UploadWay way, SelectionType attachmentType,
                                                                          OperationType operationType,
                                                                          OceanBookingInfo oceanBookingInfo,
                                                                          BusinessOperationContext operationContext,
                                                                          object mailItem, string templateCode)
        {
            return new UploadAttachmentParameter()
                {
                    SelectionType = attachmentType,
                    OceanBookingInfo = oceanBookingInfo,
                    MessageInfo = MailHelper.GetMailInfo(),
                    OperationContext = operationContext,
                    OperationType = operationType,
                    MailItem = mailItem,
                    TemplateCode = templateCode,
                    UploadWay = way
                };
        }

        /// <summary>
        /// 打开设置联系人面板
        /// </summary>
        /// <param name="associateType">关联类型</param>
        public void ShowContactPart(AssociateType associateType)
        {
            if (listBaseBusinessPart.SelectedDataRowCount > 0)
            {
                UCContactListWithToolbar contactList = RootWorkItem.Items.AddNew<UCContactListWithToolbar>();

                List<BusinessOperationContext> operationContexts = GetSelectedShipmentOperationContext();
                PopupWindow form = PartLoader.FakeShowDialog(RootWorkItem, contactList,
                                                             LocalData.IsEnglish ? "Set Contact" : "设置联系人",
                                                             FormWindowState.Minimized,
                                                             CurrentBaseBusinessPart);
                form.ShowInTaskbar = false;
                form.FormClosing += (sender, e) => contactList.Closed(e);
                contactList.BindData(associateType, operationContexts, MailHelper.GetMailInfo());

            }
            else
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(CurrentBaseBusinessPart.FindForm(), (LocalData.IsEnglish ? "selected  at least one shipment!" : "至少选择一票业务!"));
            }
        }


        /// <summary>
        /// 初始化联系人面板
        /// </summary>
        /// <param name="contactPart">联系人面板</param>
        private void InitContactPart(Control contactPart)
        {
            ClearControls();
            contactPart.Dock = DockStyle.Fill;
            listBaseBusinessPart.splitBusinessPart.Panel2.Controls.Add(contactPart);
            contactPart.BringToFront();
            //当查询过多数据时，存储过程是不会返回数据
            if (listBaseBusinessPart.OperationID == Guid.Empty)
                return;

            if (isShowAssistantPart.Value == false)
            {
                ContactListPart.AddCheckEventHandler();
                BusinessOperationContext operationContext = new BusinessOperationContext
                    {
                        OperationID = listBaseBusinessPart.OperationID,
                        OperationType = listBaseBusinessPart.OperationType,
                        CompanyID = listBaseBusinessPart.CompanyID,
                };
                (contactPart as UCSelectedContactListPart).SetDataSource(operationContext);
            }
            else
            {
                (contactPart as UCContactStaffListPart).Init(true, listBaseBusinessPart.OperationID,
                                                             listBaseBusinessPart.OperationType);
            }
        }

        /// <summary>
        /// 参与者面板绑定数据
        /// </summary>
        public void AssistantBindData()
        {
            if (isShowAssistantPart == true &&
                listBaseBusinessPart.TemplateCode.Equals(ListFormType.MailLink4Unknown.ToString()))
            {
                if (listBaseBusinessPart.OperationID == Guid.Empty)
                    return;
                StaffListPart.SetDataSource(listBaseBusinessPart.OperationID, listBaseBusinessPart.OperationType, true);
                ToolbarManager.VisibleBarItem(listBaseBusinessPart.barManager, "btnSaveAssistant",
                                              BarItemVisibility.Never);
            }
        }

        /// <summary>
        /// 清空参与者面板控件
        /// </summary>
        public void Clear()
        {
            ClearControls();
            RootWorkItem.State["ClearedDataSource"] = true;
        }

        /// <summary>
        /// 初始化参与者面板
        /// </summary>
        public void AddStaffPart()
        {
            if (ClearedDataSource())
            {
                InitContactPart(StaffListPart);
                RootWorkItem.State["ClearedDataSource"] = false;
            }
        }

        /// <summary>
        /// ClearDataSource是一个全局的标识，是否清空了参与者面板数据
        /// </summary>
        /// <returns></returns>
        private bool ClearedDataSource()
        {
            bool cleared = false;
            if (RootWorkItem.State["ClearedDataSource"] != null && RootWorkItem.State["ClearedDataSource"] != "")
            {
                bool.TryParse(RootWorkItem.State["ClearedDataSource"].ToString(), out cleared);
            }
            return cleared;
        }

        /// <summary>
        /// 是否显示参与者面板
        /// </summary>
        /// <param name="_isShowAssistantPart">是否显示参与者面板</param>
        public void IsShowAssistantPart(bool? _isShowAssistantPart)
        {
            isShowAssistantPart = _isShowAssistantPart;
        }

        /// <summary>
        /// 根据条件，选择是否加载联系人面板或参与者面板
        /// </summary>
        /// <param name="_baseBusinessPart">业务面板接口</param>
        /// <param name="count"></param>
        /// <param name="templateCode">模板编码</param>
        /// <param name="sendFrom"></param>
        public void InitControl(IBaseBusinessPart_New _baseBusinessPart, int count, string templateCode, string sendFrom)
        {
            BusinessPartNew = _baseBusinessPart;
            if (count == 0)
            {
                ClearControls();
                isShowAssistantPart = null;
                return;
            }
            if (templateCode.Equals(ListFormType.MailLink4Unknown.ToString()))
            {
                string[] splitAddress = sendFrom.Split('@');
                if (splitAddress != null && splitAddress.Length > 1)
                {
                    if (FCMInterfaceUtility.ExsitsInternalContact(splitAddress[1].ToLower()))
                    {
                        isShowAssistantPart = true;
                        InitContactPart(StaffListPart);
                    }
                    else
                    {
                        isShowAssistantPart = false;
                        InitContactPart(ContactListPart);
                    }
                }
            }
        }

        /// <summary>
        /// 清空参与者面板
        /// </summary>
        private void ClearControls()
        {
            if (listBaseBusinessPart != null)
                listBaseBusinessPart.splitBusinessPart.Panel2.Controls.Clear();
        }

        /// <summary>
        /// 未知业务面板关联
        /// </summary>
        public void MailLink4UnknownMessageRelation()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (listBaseBusinessPart == null)
                    return;
                try
                {
                    ContactListPart.RemoveCheckEventHandler();
                    listBaseBusinessPart.FocusedRowChangedHandler -= OnListPartFocusedRowChanged;

                    if (listBaseBusinessPart.OperationID == Guid.Empty)
                        return;

                    if (isShowAssistantPart.HasValue && !isShowAssistantPart.Value)
                    {
                        if (!ContactListPart.ValidateData())
                            return;

                        DateTime? updateDate = listBaseBusinessPart.Updatetime;
                        ///保存联系人
                        if (!ContactListPart.SaveData(listBaseBusinessPart.OperationID,
                                                      listBaseBusinessPart.OperationType, updateDate))
                            return;
                    }


                    bool isSaved = false; //this.EmailCenterOperationMessageRelation(listBaseBusinessPart);
                    if (!isSaved)
                    {
                        return;
                    }

                    EmailSourceType customerType = ContactListPart.Type;
                    string commandName = string.Empty;
                    if (customerType == EmailSourceType.Customer)
                    {
                        commandName = "Command_EmailCenter_Customer";
                    }
                    else if (customerType == EmailSourceType.Shipper)
                    {
                        commandName = "Command_EmailCenter_Carrier";
                    }
                    RootWorkItem.Commands[commandName].Execute();

                }
                finally
                {
                    ContactListPart.AddCheckEventHandler();
                    listBaseBusinessPart.FocusedRowChangedHandler += OnListPartFocusedRowChanged;
                }

            }
        }

        /// <summary>
        /// 订阅FocuseRowChangedHandler事件
        /// </summary>
        public void RegisterFocusedRowChanged()
        {
            if (listBaseBusinessPart != null)
            {
                listBaseBusinessPart.FocusedRowChangedHandler += OnListPartFocusedRowChanged;
            }
        }

        /// <summary>
        /// 取消订阅FocuseRowChangedHandler事件
        /// </summary>
        public void UnRegisterFocusedRowChanged()
        {
            if (listBaseBusinessPart != null)
            {
                listBaseBusinessPart.FocusedRowChangedHandler -= OnListPartFocusedRowChanged;
            }
        }

        /// <summary>
        /// 根据业务列表行改变事件，去设置联系人面板数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnListPartFocusedRowChanged(object sender,
                                                FocusedRowChangedEventArgs e)
        {
            BusinessPartNew = sender as IBaseBusinessPart_New;
            if (listBaseBusinessPart != null)
            {
                listBaseBusinessPart.IsShowAssistantPart = isShowAssistantPart;
                if (_contactListPart != null && isShowAssistantPart.HasValue && isShowAssistantPart.Value == false)
                {
                    BusinessOperationContext operationContext = new BusinessOperationContext
                        {
                            OperationID = listBaseBusinessPart.OperationID,
                            OperationType = listBaseBusinessPart.OperationType,
                            CompanyID = listBaseBusinessPart.CompanyID,
                    };
                    ContactListPart.SetDataSource(operationContext);
                }
            }
        }
        /// <summary>
        /// 保存关联信息
        /// </summary>
        /// <param name="messageRelationParameter">邮件关联参数</param>
        public void InnerSaveOperationMessageRelation(MessageRelationParameter messageRelationParameter)
        {
            if (CurrentBaseBusinessPart != null)
            {
                try
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        messageRelationParameter.OperationIDs = CurrentBaseBusinessPart.SelctedOperationIDs;
                        messageRelationParameter.OperationTypes = CurrentBaseBusinessPart.SelectedOperationTypes;
                        messageRelationParameter.OperationNOs = CurrentBaseBusinessPart.SelectedOperationNos;
                        SaveOperationMessageRelation(messageRelationParameter);
                    }
                    AfterSaveRelation(listBaseBusinessPart, CurrentBaseBusinessPart.SelectedOperationNos, "IsAssociated");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(CurrentBaseBusinessPart.FindForm(), ex.Message);
                }
            }
        }

        /// <summary>
        /// 保存关联信息
        /// </summary>
        /// <param name="messageRelationParameter">邮件关联参数</param>
        public void SaveOperationMessageRelation(MessageRelationParameter messageRelationParameter)
        {
            if (messageRelationParameter.OperationIDs == null || messageRelationParameter.OperationIDs.Length <= 0)
                return;

            try
            {
                lock (obj)
                {
                    //if (messageRelationParameter.RelationType == MessageRelationType.Auto)
                    //{
                    //    ClientBusinessOperationService.UpdateLocalBusinessData(
                    //        messageRelationParameter.OperationIDs.ToList(),
                    //        messageRelationParameter.OperationTypes[0]);
                    //}

                    Guid iMessageId = SaveMailInfo(messageRelationParameter.RelationType, messageRelationParameter.MessageID, Guid.NewGuid());
                    SaveMessageRelation(iMessageId, messageRelationParameter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
            }
        }

        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <param name="iMessageId">消息(Mail/EDI/Fax)ID</param>
        /// <param name="messageRelationParameter">邮件关联参数</param>
        private void SaveMessageRelation(Guid iMessageId, MessageRelationParameter messageRelationParameter)
        {
            List<OperationContactParameters> contactParameters = null;
            List<OperationMessageRelation> messageRelations = GetOperationMessageRelations(iMessageId, messageRelationParameter, ref contactParameters);
            if (messageRelations != null && messageRelations.Count > 0)
            {
                if (messageRelationParameter.RelationType == MessageRelationType.Hand)
                {
                    messageRelations.ForEach(item =>
                        {
                            item.Contacts = null;
                            item.XmlMessageInfo = null;
                        });
                    //关联和同步业务数据到本地缓存
                    ManyResult manyResult =
                        ClientBusinessOperationService.SaveOperationMessageRelation(messageRelations.ToArray());
                    if (manyResult != null && manyResult.Items.Count > 0)
                    {
                        int count = manyResult.Items.Count;
                        for (int r = 0; r < count; r++)
                        {
                            messageRelations[r].ID = manyResult.Items[r].GetValue<Guid>("ID");
                            messageRelations[r].UpdateDate = manyResult.Items[r].GetValue<DateTime?>("UpdateDate");
                        }
                    }
                }
                else
                {
                    //保存本地业务关联信息
                    ClientBusinessOperationService.SaveLocalOperationMessageRelation(messageRelations.ToArray());
                }
                //保存本地业务联系人
                ClientBusinessOperationService.SaveLocalOperationContactMail(contactParameters);
            }
            UIHelper.CurrentMessageRelation = messageRelations;
            //更改邮件类别图标为绿色
            RootWorkItem.Commands["Command_ChangeEmailCategories"].Execute();
        }


        /// <summary>
        /// 保存邮件
        /// </summary>
        /// <param name="relationType">消息关联类型</param>
        /// <param name="messageID">邮件MessageID</param>
        /// <param name="imessageID">消息(Mail/EDI/Fax)ID</param>
        /// <returns>最新的消息(Mail/EDI/Fax)ID</returns>
        private Guid SaveMailInfo(MessageRelationType relationType, string messageID, Guid imessageID)
        {
            //手动关联,需要确保邮件是否保存到服务端数据库
            if (relationType != MessageRelationType.Auto)
            {
                Message.ServiceInterface.Message messageInfo = ServiceClient.GetService<IMessageService>().GetMessageByMessageId(messageID);
                if (messageInfo == null)
                {
                    Message.ServiceInterface.Message message = MailHelper.GetMessageInfo();
                    message.Id = Guid.NewGuid();
                    return MessageService.Save(message)[0].Items[0].GetValue<Guid>("ID");
                }
                return messageInfo.Id;
            }
            return imessageID;
        }

        /// <summary>
        /// 设置当前关联信息集合
        /// </summary>
        /// <param name="iMessageId">消息(Mail/EDI/Fax)ID</param>
        /// <param name="messageRelationParameter">邮件关联参数</param>
        /// <param name="contactParameterses">联系人参数列表</param>
        /// <returns>操作日志实体列表</returns>
        private List<OperationMessageRelation> GetOperationMessageRelations(Guid iMessageId, MessageRelationParameter messageRelationParameter, ref List<OperationContactParameters> contactParameterses)
        {
            bool directReturn = true;
            var currentRelations = GetCurrentMailRelations(iMessageId, messageRelationParameter, ref directReturn);
            if (!directReturn)
                return currentRelations;

            //手动关联时，删除需要移除关联信息
            RemoveUnnecessaryOperationMessages(messageRelationParameter.OperationIDs, currentRelations);

            //自动关联时，获取邮件所有外部邮件的业务联系人
            List<string> mails = null;
            List<CustomerCarrierObjects> contacts = GetContacts(messageRelationParameter, ref mails);
            List<OperationMessageRelation> newMessageRelations = new List<OperationMessageRelation>();
            int length = messageRelationParameter.OperationIDs.Length;
            contactParameterses = new List<OperationContactParameters>(length);
            for (int i = 0; i < length; i++)
            {
                OperationMessageRelation relationInfo = currentRelations.Find(item => item.OperationID == messageRelationParameter.OperationIDs[i]);
                if (relationInfo == null)
                {
                    OperationMessageRelation newMessageRelation = FCMInterfaceUtility.CreateOperationMessageRelationInfo(Guid.NewGuid(), null, messageRelationParameter.OperationIDs[i], messageRelationParameter.OperationTypes[i], messageRelationParameter.MessageID, iMessageId, GetContactStage(messageRelationParameter.AssociateType));
                    newMessageRelation.UpdateDataType = messageRelationParameter.UpdateDataType;
                    newMessageRelation.RelationType = messageRelationParameter.RelationType;
                    newMessageRelation.Contacts = ConvertOceanContactsToXml(messageRelationParameter.RelationType, contacts, messageRelationParameter.OperationIDs[i], messageRelationParameter.OperationTypes[i]); ;
                    //邮件只需要保存一次即可
                    if (i == 0)
                        newMessageRelation.XmlMessageInfo = ConvertMessageInfoToXml(iMessageId, messageRelationParameter.RelationType);
                    else
                        newMessageRelation.XmlMessageInfo = null;
                    newMessageRelation.UploadServer = false;
                    newMessageRelation.OperationNo = messageRelationParameter.OperationNOs[i];
                    newMessageRelations.Add(newMessageRelation);
                }
                else
                {
                    //relationInfo.Contacts = xmlContacts;
                    newMessageRelations.Add(relationInfo);
                }

                if (messageRelationParameter.RelationType == MessageRelationType.Auto)
                {
                    if (mails != null && mails.Count > 0)
                    {
                        contactParameterses.Add(new OperationContactParameters()
                            {
                                OceanBookingID = messageRelationParameter.OperationIDs[i],
                                OperationType = messageRelationParameter.OperationTypes[i],
                                Mails = mails
                            });
                    }
                }
            }

            return newMessageRelations;
        }

        /// <summary>
        /// 获取当前邮件的关联信息
        /// </summary>
        /// <param name="iMessageId">消息(Mail/EDI/Fax)ID</param>
        /// <param name="messageRelationParameter">邮件关联参数</param>
        /// <param name="directReturn">是否直接返回</param>
        /// <returns>操作日志实体类</returns>
        private List<OperationMessageRelation> GetCurrentMailRelations(Guid iMessageId, MessageRelationParameter messageRelationParameter, ref bool directReturn)
        {
            List<OperationMessageRelation> currentRelations = UIHelper.CurrentMessageRelation;
            if (currentRelations == null)
            {
                currentRelations = new List<OperationMessageRelation>();
            }
            if (currentRelations.Count > 0)
            {
                if (!currentRelations.Any(item => item.MessageId.ToLower().Equals(messageRelationParameter.MessageID.ToLower())))
                {
                    directReturn = false;
                    return currentRelations;
                }
                currentRelations.ForEach(delegate(OperationMessageRelation item)
                {
                    item.IMessageId = iMessageId;
                    item.RelationType = messageRelationParameter.RelationType;
                    item.UpdateDataType = messageRelationParameter.UpdateDataType;
                    item.ContactStage = GetContactStage(messageRelationParameter.AssociateType);
                    item.UpdateBy = LocalData.UserInfo.LoginID;
                    item.UploadServer = false;
                    item.UpdateDate = null;
                });
            }

            return currentRelations;
        }

        /// <summary>
        /// 将业务联系人实体转换成Xml
        /// </summary>
        /// <param name="relationType">消息关联类型</param>
        /// <param name="contacts">客户承运实体列表</param>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <returns>业务联系人XML字符串</returns>
        private string ConvertOceanContactsToXml(MessageRelationType relationType, List<CustomerCarrierObjects> contacts, Guid operationID, OperationType operationType)
        {
            if (relationType == MessageRelationType.Auto && contacts != null)
            {
                contacts.ForEach(delegate(CustomerCarrierObjects item)
                {
                    item.OceanBookingID = operationID;
                    item.OperationType = operationType;
                });
                return FCMInterfaceUtility.ConvertContactsToXml(contacts);
            }
            else
                return null;
        }

        /// <summary>
        /// 将邮件实体转换成Xml形式
        /// </summary>
        /// <param name="iMessageID">消息(Mail/EDI/Fax)ID</param>
        /// <param name="relationType">消息关联类型</param>
        /// <returns>邮件实体XML字符串</returns>
        private string ConvertMessageInfoToXml(Guid iMessageID, MessageRelationType relationType)
        {
            if (relationType == MessageRelationType.Auto)
            {
                Message.ServiceInterface.Message messageInfo = MailHelper.GetMessageInfo();
                messageInfo.Id = iMessageID;
                return messageInfo.GetXmlDataNode(false);
            }
            else
                return null;
        }

        /// <summary>
        /// 移除多余的服务端数据库关联信息
        /// </summary>
        /// <param name="operationIDs">业务ID列表</param>
        /// <param name="operationMessageRelations">操作日志实体列表</param>
        private void RemoveUnnecessaryOperationMessages(Guid[] operationIDs, List<OperationMessageRelation> operationMessageRelations)
        {
            List<Guid> messageRelationIds = new List<Guid>();
            List<DateTime?> updateDates = new List<DateTime?>();
            int totalCount = operationMessageRelations.Count;
            for (int i = 0; i < totalCount; i++)
            {
                if (!operationIDs.Contains(operationMessageRelations[i].OperationID))
                {
                    messageRelationIds.Add(operationMessageRelations[i].ID);
                    updateDates.Add(operationMessageRelations[i].UpdateDate);
                }
            }

            if (messageRelationIds.Count > 0)
                ClientBusinessOperationService.RemoveAndSyncOperationMessageRelations(messageRelationIds.ToArray(), updateDates.ToArray());
        }

        /// <summary>
        /// 获取邮件所有外部业务联系人
        /// </summary>
        /// <param name="messageRelationParameter">邮件关联参数</param>
        /// <param name="mails">邮件地址列表</param>
        /// <returns>客户承运实体列表</returns>
        private List<CustomerCarrierObjects> GetContacts(MessageRelationParameter messageRelationParameter, ref List<string> mails)
        {
            List<CustomerCarrierObjects> contacts = null;
            if (messageRelationParameter.RelationType == MessageRelationType.Auto)
            {
                contacts = FCMInterfaceUtility.ConvertContacts(messageRelationParameter.MailContactInfos);

                if (contacts != null && contacts.Count > 0)
                {
                    mails = (from c in contacts select c.Mail).ToList();
                }
            }
            return contacts;
        }

        /// <summary>
        /// 获取联系人阶段
        /// </summary>
        /// <param name="associateType">关联类型</param>
        /// <returns>沟通阶段</returns>
        private ContactStage GetContactStage(AssociateType? associateType)
        {
            if (!associateType.HasValue)
                return ContactStage.Unknown;
            else
            {
                if (associateType == AssociateType.WithStageSI)
                    return ContactStage.SI;
                else if (associateType == AssociateType.WithStageSO)
                    return ContactStage.SO;
                else
                    return ContactStage.Unknown;
            }
        }

        /// <summary>
        /// 关联后将关联的业务置顶，并且字体加粗
        /// </summary>
        /// <param name="basePart">业务列表面板</param>
        /// <param name="operationNos">业务号列表</param>
        /// <param name="isAssociatedFieldName">是否关联字段名称</param>
        private void AfterSaveRelation(ListBaseBusinessPart basePart, string[] operationNos, string isAssociatedFieldName)
        {
            if ((operationNos != null) && (operationNos.Length > 0))
            {
                StringBuilder strBuf = new StringBuilder();
                Array.ForEach<string>(operationNos, delegate(string item)
                {
                    strBuf.Append(string.Format(" {0}='{1}' OR", "NO", item));
                });
                string expression = strBuf.ToString(0, strBuf.Length - 2).Trim();
                basePart.SetFilterRowCellValue(expression, isAssociatedFieldName, 1);
                basePart.SetFocusedRowHandle(0);
                basePart.AcceptChanges();
            }
        }

        /// <summary>
        /// 构造业务上下文实体(根据当前选择单个业务)
        /// </summary>
        /// <returns>业务上下文实体</returns>
        private BusinessOperationContext CreateBusinessOperationContext()
        {
            return new BusinessOperationContext
            {
                OperationID = CurrentBaseBusinessPart.OperationID,
                OperationNO = CurrentBaseBusinessPart.OperationNo,
                CompanyID = CurrentBaseBusinessPart.CompanyID,
                OperationType = CurrentBaseBusinessPart.OperationType,
                UpdateDate = CurrentBaseBusinessPart.Updatetime
            };
        }
        /// <summary>
        /// 构造业务上下文实体列表(根据当前选择业务列表)
        /// </summary>
        /// <returns>业务上下文实体</returns>
        private List<BusinessOperationContext> GetSelectedShipmentOperationContext()
        {
            List<BusinessOperationContext> operationContexts = new List<BusinessOperationContext>();

            int count = listBaseBusinessPart.SelectedDataRowCount;
            for (int i = 0; i < count; i++)
            {
                operationContexts.Add(new BusinessOperationContext()
                {
                    OperationType = listBaseBusinessPart.SelectedOperationTypes[i],
                    OperationID = listBaseBusinessPart.SelectedOperationIDs[i],
                    OperationNO = listBaseBusinessPart.SelectedOperationNos[i],
                    CompanyID = listBaseBusinessPart.SelectedCompanyIDs[i],
                    UpdateDate = listBaseBusinessPart.SelectedUpdateDates[i]
                });
            }


            return operationContexts;
        }

        /// <summary>
        /// 批量新增账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandTaskCenterCommunicationBatchAddAccInfo(object sender, EventArgs e)
        {
            //Guid id = CurrentBaseBusinessPart.OperationID;
            CommonOperationService.BatchAddBill();
        }

        /// <summary>
        /// 下载CSP业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CSPBooking_Download(object sender, EventArgs e)
        {
            CommonOperationService.CSPBookingDownload();
        }

        #region Comment Code
        /// <summary>
        /// 保存业务联系人
        /// </summary>
        /// <param name="judgeContactExsits"></param>
        /// <param name="emailAddress"></param>
        /// <param name="senderName"></param>
        /// <param name="contactType"></param>
        /// <param name="operationIDs"></param>
        /// <param name="updateDates"></param>
        /// <param name="operationTypes"></param>
        /// <param name="list"></param>
        private void AddOceanContactsAndOperationContact(string emailAddress, string senderName, ContactType? contactType, Guid[] operationIDs, OperationType[] operationTypes, List<CustomerCarrierObjects> list)
        {
            List<CustomerCarrierObjects> customerList = new List<CustomerCarrierObjects>();
            int length = operationIDs.Length;
            for (int j = 0; j < length; j++)
            {
                CustomerCarrierObjects customerInfo = new CustomerCarrierObjects();
                customerInfo.Mail = emailAddress;
                customerInfo.Id = Guid.NewGuid();
                customerInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                customerInfo.CreateByID = LocalData.UserInfo.LoginID;
                customerInfo.Type = contactType == null ? ContactType.Carrier : contactType.Value;
                customerInfo.Stage = ContactStage.Unknown.ToString();
                customerInfo.Name = senderName;
                customerInfo.OceanBookingID = operationIDs[j];
                customerInfo.OperationType = operationTypes[j];
                customerInfo.UpdateDate = null;
                customerList.Add(customerInfo);
            }

            //保存业务联系人(FCM.OceanContacts,FCM.OperationContactCache,FCM.OperationViewOECache ContactMail)
            CommonOperationService.SaveAndSyncOperationContacts(new List<string>(1) { emailAddress }, customerList);
        }


        /// <summary>
        /// 根据联系人最近业务，设置联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="operationIDs"></param>
        /// <param name="operationTypes"></param>
        /// <param name="operationNos"></param>
        /// <param name="contactList"></param>
        private void SaveOceanContactAndOperationContacts(string sender, Guid[] operationIDs,
                                                          OperationType[] operationTypes,
                                                          List<CustomerCarrierObjects> contactList)
        {

            List<CustomerCarrierObjects> newContactList = new List<CustomerCarrierObjects>();
            CustomerCarrierObjects latestContactInfo = contactList[0];
            int length = operationIDs.Length;
            for (int j = 0; j < length; j++)
            {
                //没有找到勾选业务的联系人，复制联系人获取最近一票业务的联系人
                if (!contactList.Any(item => item.OceanBookingID == operationIDs[j]))
                {
                    latestContactInfo.Id = Guid.NewGuid();
                    latestContactInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    latestContactInfo.CreateByID = LocalData.UserInfo.LoginID;
                    latestContactInfo.OceanBookingID = operationIDs[j];
                    latestContactInfo.OperationType = operationTypes[j];
                    latestContactInfo.UpdateDate = null;
                    latestContactInfo.UpdateByID = LocalData.UserInfo.LoginID;
                    newContactList.Add(latestContactInfo);
                    #region Invalidate Code
                    ////找到最近的一票业务
                    //BusinessQueryCriteria criteria = GetQueryCriteria(sender, OperationType.Unknown, operationNos[j]);
                    //DataTable dt = BusinessQueryService.Get(criteria);

                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    Guid operationID = dt.Rows[0].Field<Guid>("OceanBookingID");
                    //    OperationType operationType = (OperationType)dt.Rows[0].Field<byte>("OperationType");
                    //    List<CustomerCarrierObjects> newContactList =
                    //        FcmCommonService.GetContactList(operationID, operationType).CustomerCarrier;
                    //    if (newContactList != null && newContactList.Count > 0)
                    //    {
                    //        newContactList.ForEach(item =>
                    //            {
                    //                item.Id = Guid.NewGuid();
                    //                item.CreateDate = DateTime.Now;
                    //                item.CreateByID = LocalData.UserInfo.LoginID;
                    //                item.OceanBookingID = operationIDs[j];
                    //                item.OperationType = operationTypes[j];
                    //                item.UpdateDate = null;
                    //                item.UpdateByID = LocalData.UserInfo.LoginID;
                    //            });

                    //        //保存联系人与业务的关联
                    //        CommonOperationService.SaveAndSyncOperationContacts(new List<string> { sender },
                    //                                                            newContactList);
                    //    }
                    //}
                    #endregion
                }
            }
            //保存联系人与业务的关联
            if (newContactList.Count > 0)
                CommonOperationService.SaveAndSyncOperationContacts(new List<string>(1) { sender },
                                                                    newContactList);
        }

        /// <summary>
        /// 构造查询参数实体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="operationType"></param>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        private BusinessQueryCriteria GetQueryCriteria(string sender, OperationType operationType, string operationNo)
        {
            BusinessQueryCriteria criteria = new BusinessQueryCriteria();
            criteria.OperationType = operationType;
            criteria.OperationNo = operationNo;
            criteria.EmailAddress = sender;
            criteria.companyIDs = CompanyIDs;
            criteria.TemplateCode = ListFormType.MailLink4in1.ToString();
            criteria.AdvanceQueryString =
                string.Format("1=1 AND $@ContactMail@ LIKE '%{0}%' ORDER BY $@CreateDate@", sender);
            return criteria;
        }

        ///// <summary>
        ///// 返回消息实体类
        ///// </summary>
        ///// <param name="type">发送类型</param>
        ///// <param name="way">发送方向</param>
        ///// <param name="sendTo">接收人邮箱</param>
        ///// <param name="sendFrom">发送人邮箱</param>
        ///// <param name="formType">表单类型</param>
        ///// <param name="operationType">业务类型</param>
        ///// <param name="operationId">操作ID</param>
        ///// <param name="formId">表单ID</param>
        ///// <param name="body">发送内容</param>
        ///// <param name="subject">主题</param>
        ///// <param name="cc">邮件抄送地址</param>
        ///// <param name="action">操作动作</param>
        ///// <param name="eventObjects">事件列表实体</param>
        ///// <returns></returns>
        //public Message.ServiceInterface.Message CreateMessageInfo(Message.ServiceInterface.MessageType type,
        //    Message.ServiceInterface.MessageWay way, string sendTo, string sendFrom, FormType formType,
        //    OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
        //    string action,EventObjects eventObjects)
        //{
        //    // 邮件发送的消息实体
        //    var message = new Message.ServiceInterface.Message();

        //    message.Type = type;
        //    message.Way = way;
        //    //收件人邮箱地址
        //    message.SendTo = sendTo;
        //    //发件人邮箱地址
        //    message.SendFrom = sendFrom;
        //    //邮件抄送地址
        //    message.CC = cc;

        //    message.UserProperties = new Message.ServiceInterface.MessageUserPropertiesObject
        //        {
        //            FormType = formType,
        //            OperationType = operationType,
        //            OperationId = operationId,
        //            FormId = formId
        //        };
        //    if (!string.IsNullOrEmpty(action))
        //    {
        //        //MessageUserPropertiesObject(消息自定义属性类)

        //        message.UserProperties.Action = action;
        //    }
        //    if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
        //    {
        //        message.Body = body;
        //        message.Subject = subject;
        //    }
        //    message.UserProperties.EventInfo = eventObjects;
        //    return message;

        //}
        ///// <summary>
        ///// Mail SO Copy To Customer附件邮件发送方法
        ///// </summary>
        ///// <param name="message">消息实体类</param>
        ///// <param name="oceanBookingInfo">订舱详细信息实体</param>
        ///// <param name="isEnglish">是否发送的附件为英文版本(true 为英文,false 为中文)</param>
        //public void GenerateSendReport(ICP.Message.ServiceInterface.Message message, OceanBookingInfo oceanBookingInfo, bool isEnglish)
        //{
        //    //模板路径
        //    string fileName = OceanExportPrintHelper.GetOEReportPath();

        //    if (isEnglish == true)
        //    {
        //        fileName = System.IO.Path.Combine(fileName, "OE_OrderInfo_EN.frx");
        //    }
        //    else
        //    {
        //        //国内，用中文模板
        //        fileName = System.IO.Path.Combine(fileName, "OE_OrderInfo_CN.frx");
        //    }
        //    //数据源
        //    var data = OeReportSrvice.GetOEOrderReportData(oceanBookingInfo.ID);
        //    if (data == null) return;

        //    Dictionary<string, object> reportSource = new Dictionary<string, object>();

        //    reportSource.Add("ReportSource", data);
        //    reportSource.Add("OrderFee", data.Fees);
        //    //生成pdf附件，后发送邮件
        //    ReportService.SendReport(message, fileName, reportSource);

        //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Sent Successfully..." : "发送成功...");
        //}
        ///// <summary>
        ///// 根据id读取数据
        ///// </summary>
        ///// <param name="id">业务号</param>
        ///// <param name="type">类别</param>
        ///// <returns>返回拼接的字符（接收人邮箱地址，接收人名称，抄送人邮箱）</returns>
        //public string MailInformation(Guid id, string type)
        //{
        //    string retuRned = string.Empty;
        //    string strTerm = string.Empty;
        //    List<MailInformation> mailInformation = OceanExportService.GetContactEmail(id, type);
        //    if (mailInformation.Count > 0)
        //    {
        //        retuRned = GetMailInformation(mailInformation);
        //    }
        //    return retuRned;

        //}

        ///// <summary>
        ///// 对数据集合循环，生成字符串
        ///// </summary>
        ///// <param name="mailInformation">MailInformation对象集合</param>
        ///// <returns></returns>
        //public string GetMailInformation(List<MailInformation> mailInformation)
        //{
        //    string sendTo = string.Empty;
        //    string name = string.Empty;
        //    string cc = string.Empty;
        //    foreach (var item in mailInformation)
        //    {
        //        //当前联系人是接人
        //        if (!string.IsNullOrEmpty(sendTo) && !string.IsNullOrEmpty(name) && item.CC == false)
        //        {
        //            sendTo = sendTo + ";" + item.Email.Trim();
        //            name = name + "," + item.Name.Trim();

        //        }
        //        //当前客户是抄送人
        //        else if (item.CC == true && name.Contains(item.Name) == false)
        //        {
        //            if (!string.IsNullOrEmpty(cc))
        //            {
        //                cc = cc + ";" + item.Email.Trim();
        //            }
        //            else
        //            {
        //                cc = item.Email.Trim();
        //            }

        //        }
        //        //第一次赋值
        //        else
        //        {
        //            sendTo = item.Email.Trim();
        //            name = item.Name.Trim();
        //        }
        //    }
        //    return sendTo + "|" + name + "|" + cc;
        //}



        ///// <summary>
        ///// Mail BL Copy To Customer附件邮件发送方法
        ///// </summary>
        ///// <param name="message">消息实体类</param>
        ///// <param name="oceanBookingInfo">订舱详细信息实体</param>
        //public void GetMailBlCopy(ICP.Message.ServiceInterface.Message message, OceanBookingInfo oceanBookingInfo)
        //{
        //    BookingBLInfo bookingBlInfo = oceanBookingInfo.OceanMBLs.FirstOrDefault();
        //    //模板路径
        //    string fileName = System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanExport\\" + "OE_BL_Original_Report_FarEast.frx";
        //    BLReportData data = OeReportSrvice.GetBLReportData(bookingBlInfo.ID, FCMBLType.MBL);
        //    BLReportClientData blReportData = new BLReportClientData();
        //    Utility.CopyToValue(data, blReportData, typeof(BLReportClientData));
        //    blReportData.ReportStyle = "COPY";
        //    blReportData.Header = string.Empty;
        //    blReportData.CompanyName = "CITY OCEAN LOGISTICS CO.,LTD.";
        //    blReportData.BLType = FCMBLType.MBL;
        //    blReportData.MBLNo = string.Empty;
        //    if (blReportData.ETD != null)
        //        blReportData.EtdString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        //    blReportData.Agent = string.Empty;
        //    blReportData.DescriptionOfContainer = blReportData.DescriptionOfContainer.Replace("/0.000CBM", "");
        //    //数据源
        //    BindingSource bs = new BindingSource();
        //    bs.DataSource = blReportData;
        //    Dictionary<string, object> reportSource = new Dictionary<string, object>();
        //    reportSource.Add("ReportSource", bs);
        //    //生成pdf附件，后发送邮件
        //    ReportService.SendReport(message, fileName, reportSource);

        //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Sent Successfully..." : "发送成功...");
        //}

        //class BLReportClientData : BLReportData
        //{
        //    public string EtdString { get; set; }
        //    public string IssueDateString { get; set; }
        //    public string NumberOfOriginalString { get; set; }
        //}

        #endregion
    }


}

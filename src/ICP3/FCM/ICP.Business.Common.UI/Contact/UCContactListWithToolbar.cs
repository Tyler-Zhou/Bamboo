using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenterFramework.UI;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.Linq;
using ICP.Business.Common.ServiceInterface;
using FCMInterfaceUtility = ICP.FCM.Common.ServiceInterface.FCMInterfaceUtility;
using ICP.Message.ServiceInterface;

namespace ICP.Business.Common.UI.Contact
{
    /// <summary>
    /// 设置联系人面板  （未完待续）
    /// </summary>
    [ToolboxItem(false)]
    public partial class UCContactListWithToolbar : BaseEditPart
    {
        #region 属性

        public WorkItem workItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }

        }

        public IFCMCommonService FcmCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }

        public IICPCommonOperationService CommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }
        }

        /// <summary>
        /// 关联类型
        /// </summary>
        public AssociateType AssociateType { get; set; }
        /// <summary>
        /// 业务上下文
        /// </summary>
        public List<BusinessOperationContext> _OperationContexts { get; set; }
        /// <summary>
        /// 使用的业务上下文
        /// </summary>
        private BusinessOperationContext UsingOperationContext { get; set; }

        /// <summary>
        /// 当前邮件实体对象
        /// </summary>
        public Message.ServiceInterface.Message Currentmessage { get; set; }
        /// <summary>
        /// 业务信息集合
        /// </summary>
        List<BusinessOperationContext> OperationContexts { get; set; }
        /// <summary>
        /// 关联使用的类
        /// </summary>
        public OperationSaveController OperationSaveController = new OperationSaveController();
        /// <summary>
        /// 联系人类型 (默认为客户)
        /// </summary>
        public ContactType ContactType = ContactType.Customer;

        private List<string> emailList = new List<string>();

        /// <summary>
        /// 用户修改联系人后的联系人集合
        /// </summary>
        public List<CustomerCarrierObjects> EditedContactList
        {
            get
            {
                return ucCustomerList.DataSourceList;
            }
        }
        /// <summary>
        /// 避免重复多次保存
        /// </summary>
        private bool hasSaved = false;
        /// <summary>
        /// 业务上下文
        /// </summary>
        private int operationCount
        {
            get
            {
                if (_OperationContexts == null)
                    return 0;
                else
                    return _OperationContexts.Count;


            }
        }
        /// <summary>
        /// 传过来的集合信息
        /// </summary>
        public override object DataSource { get; set; }

        #region 数据绑定使用的变量
        private Message.ServiceInterface.Message DataSourceMessage { get; set; }

        private AssociateType DataSourceAssociateType { get; set; }

        private List<BusinessOperationContext> DataSourceBusinessOperationContext { get; set; }
        #endregion
        #endregion

        #region 构造函数
        public UCContactListWithToolbar()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate { DisposedCompoment(); };
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
                Init();
        }

        public void Closed(FormClosingEventArgs e)
        {
            //if (this.ucCustomerList.IsChanged)
            //{
            //    DialogResult dialogResult = Utility.ShowMessageBox("Tip",
            //                             (LocalData.IsEnglish
            //                                  ? "The operation contacts data has changed,Are you sure to save?"
            //                                  : "业务联系人数据有变更,确认保存?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        Saved();
            //    }
            //}
            e.Cancel = false;
        }

        private void Init()
        {
            ucCustomerToolbar.Added += OnContactAdded;
            ucCustomerToolbar.Deleted += OnContactDeleted;
            ucCustomerToolbar.Saved += OnContactSaved;
            ucCustomerToolbar.ContactTypeChanged += OnContactTypeChanged;
            ucCustomerToolbar.ReadOnlyChanged += ReadOnlyChanged;
            ucCustomerToolbar.SetStage += OnSetStage;
            hasSaved = false;
            #region 数据绑定
            if (DataSource != null)
            {
                List<object> objects = DataSource as List<object>;
                foreach (var i in objects)
                {
                    if (i is Message.ServiceInterface.Message)
                    {
                        DataSourceMessage = i as Message.ServiceInterface.Message;
                    }
                    else if (i is AssociateType)
                    {
                        DataSourceAssociateType = i is AssociateType ? (AssociateType)i : AssociateType.Normal;
                    }
                    else
                    {
                        DataSourceBusinessOperationContext = i as List<BusinessOperationContext>;
                    }
                }
                BindData(DataSourceAssociateType, DataSourceBusinessOperationContext, DataSourceMessage);
            }
            #endregion
        }



        private void OnContactSaved(object sender, EventArgs e)
        {
            if (!hasSaved)
            {
                hasSaved = true;
                InnerSaved(true);
            }
        }

        public void Saved(bool saveContact)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    Save(saveContact);
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
            }
        }

        private void InnerSaved(bool validateContact)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //WaitCallback callback = (data) => this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                //{
                try
                {
                    if (Save(validateContact))
                    {
                        MailCenterParameter.MailCenterRefresh = true;
                        ClientHelper.OutLookSetForegroundWindow();
                        //保存完成后默认将界面关闭                        
                        this.FindForm().Close();
                    }
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
                //弹出对话框信息后设置当前保存是可用的
                if (hasSaved)
                {
                    hasSaved = false;
                }
                //});
                //ThreadPool.QueueUserWorkItem(callback);
            }
        }

        private bool Save(bool saveContact)
        {
            bool validateContactList = true;
            if (saveContact)
            {
                if (!ucCustomerList.IsNullOrEmtpyDataSource())
                {
                    validateContactList = ucCustomerList.ValidateData();
                    if (validateContactList)
                    {
                        //保存选择的第一票业务的联系人
                        ucCustomerList.InnerSaved(ContactType);
                        //保存其余的业务联系人
                        SaveOtherOperationContacts();
                        //更新本地缓存业务
                        UpdateLocalOperationData();
                        //保存关联信息
                        SaveOperationMessageRelation();
                    }
                }
            }
            else
                //保存关联信息
                SaveOperationMessageRelation();
            return validateContactList;
        }

        private void SaveOperationMessageRelation()
        {
            workItem.State["AssociateContactType"] = this.ContactType;
            //保存关联信息
            MessageRelationParameter messageRelationParameter = new MessageRelationParameter();
            switch (AssociateType)
            {
                case AssociateType.Normal:
                    messageRelationParameter = GetMessageRelationParameter(Currentmessage, AssociateType.Normal);
                    break;
                case AssociateType.WithStageSI:
                    messageRelationParameter = GetMessageRelationParameter(Currentmessage, AssociateType.WithStageSI);
                    break;
                case AssociateType.WithStageSO:
                    messageRelationParameter = GetMessageRelationParameter(Currentmessage, AssociateType.WithStageSO);
                    break;
            }
            if (messageRelationParameter != null)
            {
                OperationSaveController.InnerSaveOperationMessageRelation(messageRelationParameter, OperationContexts);
            }
            else
            {
                MessageBox.Show(LocalData.IsEnglish ? "error, please contact your administrator!" : "关联出错,请联系管理员!");
            }

        }
        /// <summary>
        /// 构造关联参数
        /// </summary>
        /// <param name="messageinfo">邮件实体</param>
        /// <param name="associateType">关联类型</param>
        /// <returns></returns>
        public MessageRelationParameter GetMessageRelationParameter(Message.ServiceInterface.Message messageinfo, AssociateType associateType)
        {
            if (messageinfo != null)
            {
                MessageRelationParameter messageRelationParameter = new MessageRelationParameter
                {
                    AssociateType = associateType,
                    MailContactInfos = null,
                    RelationType = MessageRelationType.Hand,
                    UpdateDataType = UpdateDataType.MainForMessageID,
                    MessageID = Currentmessage.MessageId,
                    MessageInfo = Currentmessage
                };
                return messageRelationParameter;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存其他业务的联系人（与第一票业务的联系人相同）
        /// </summary>
        private void SaveOtherOperationContacts()
        {
            if (operationCount > 0 && EditedContactList != null && EditedContactList.Count > 0)
            {
                for (int i = 1; i < operationCount; i++)
                {
                    //找到当前业务联系人
                    List<CustomerCarrierObjects> currentShipmentCustomerCarriers = FcmCommonService.GetContactList(_OperationContexts[i].OperationID, _OperationContexts[i].OperationType).CustomerCarrier;
                    if (currentShipmentCustomerCarriers != null && currentShipmentCustomerCarriers.Count > 0)
                    {
                        //找到第一票业务联系人
                        List<CustomerCarrierObjects> firstShipmentCustomerCarriers =
                            ICP.Framework.ClientComponents.Controls.Utility.Clone(EditedContactList);
                        if (firstShipmentCustomerCarriers != null && firstShipmentCustomerCarriers.Count > 0)
                        {
                            List<CustomerCarrierObjects> newCustomerCarriers =
                                ReplaceCustomerCarriers(currentShipmentCustomerCarriers, firstShipmentCustomerCarriers, _OperationContexts[i].OperationID, _OperationContexts[i].OperationType);
                            FcmCommonService.SaveContactList(newCustomerCarriers);
                        }
                    }
                    else
                    {
                        List<CustomerCarrierObjects> firstShipmentCustomerCarriers =
                           ICP.Framework.ClientComponents.Controls.Utility.Clone(EditedContactList);
                        foreach (var item in firstShipmentCustomerCarriers)
                        {
                            item.OceanBookingID = _OperationContexts[i].OperationID;
                            item.OperationType = _OperationContexts[i].OperationType;
                            item.Id = Guid.NewGuid();
                            item.UpdateDate = null;
                        }

                        FcmCommonService.SaveContactList(firstShipmentCustomerCarriers);
                    }
                }
            }
        }

        /// <summary>
        /// 将用户编辑的联系人替换为更新之前的联系人
        /// </summary>
        /// <param name="currentShipmentCustomerCarriers"></param>
        /// <param name="firstShipmentCustomerCarriers"></param>
        /// <returns></returns>
        private List<CustomerCarrierObjects> ReplaceCustomerCarriers(List<CustomerCarrierObjects> currentShipmentCustomerCarriers, List<CustomerCarrierObjects> firstShipmentCustomerCarriers, Guid operationID, OperationType operationType)
        {
            int count = firstShipmentCustomerCarriers.Count;
            for (int j = 0; j < count; j++)
            {
                //如果当前业务联系人包含第一票业务的联系人  
                CustomerCarrierObjects info = currentShipmentCustomerCarriers.Find(item => item.Mail.Trim().Equals(firstShipmentCustomerCarriers[j].Mail.Trim(), StringComparison.CurrentCultureIgnoreCase));
                if (info != null)
                {
                    firstShipmentCustomerCarriers[j].Id = info.Id;
                    //firstShipmentCustomerCarriers[j].IsCC = info.IsCC;
                    //firstShipmentCustomerCarriers[j].SO = info.SO;
                    //firstShipmentCustomerCarriers[j].SI = info.SI;
                    firstShipmentCustomerCarriers[j].OceanBookingID = info.OceanBookingID;
                    firstShipmentCustomerCarriers[j].UpdateDate = info.UpdateDate;
                    firstShipmentCustomerCarriers[j].UpdateByID = info.UpdateByID;
                }
                else
                {
                    firstShipmentCustomerCarriers[j].Id = Guid.NewGuid();
                    firstShipmentCustomerCarriers[j].OperationType = operationType;
                    firstShipmentCustomerCarriers[j].OceanBookingID = operationID;
                }
            }

            return firstShipmentCustomerCarriers;
        }

        void ReadOnlyChanged(object sender, CommonEventArgs<bool> e)
        {
            bool readOnly = e.Data;
        }

        private void OnContactTypeChanged(object sender, CommonEventArgs<ContactType> e)
        {
            ContactType = e.Data;
            ucCustomerList.ContactType = ContactType;
            ucCustomerList.ChangeDataRecordContactType(ContactType);
        }

        private void OnContactDeleted(object sender, EventArgs e)
        {
            ucCustomerList.Remove();
        }

        private void OnContactAdded(object sender, EventArgs e)
        {
            ucCustomerList.AddNewDataRecord(UsingOperationContext.OperationID, string.Empty, string.Empty);
        }

        private void OnSetStage(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            DevExpress.XtraBars.BarEditItem be = sender as DevExpress.XtraBars.BarEditItem;

            foreach (var item in ucCustomerList.DataSourceList)
            {
                item.Stage = be.EditValue.ToString();
            }

            ucCustomerList.ResetBindings();
        }


        public void BindData(AssociateType associateType, List<BusinessOperationContext> operationContexts, Message.ServiceInterface.Message messageInfo)
        {
            this.AssociateType = associateType;
            if (operationContexts != null && operationContexts.Count > 0)
            {
                _OperationContexts = operationContexts;
                this.ucCustomerList.OperationContext = UsingOperationContext = this._OperationContexts[0];
                if (messageInfo != null)
                {
                    ucCustomerToolbar.barVisibility();
                    Currentmessage = messageInfo;
                    OperationContexts = operationContexts;
                }
                InnerBindData(associateType, messageInfo);

                ucCustomerToolbar.SetStageComboBoxItems(UsingOperationContext.OperationType);
            }
        }

        /// <summary>
        ///绑定邮件的联系人与业务的关联的数据
        /// </summary>
        /// <param name="associateType"></param>
        /// <param name="messageInfo"></param>
        /// <param name="operationContexts"></param>
        public void InnerBindData(AssociateType associateType, Message.ServiceInterface.Message messageInfo)
        {
            //判断邮件中联系人是否是内部邮件或外部邮件
            var externalTempContacts = FCMInterfaceUtility.GetExternalMailToContactList(messageInfo, UsingOperationContext.OperationID, UsingOperationContext.OperationType, (associateType == AssociateType.WithStageSO), (associateType == AssociateType.WithStageSI), false);
            //外部联系人
            if (externalTempContacts != null && externalTempContacts.Count > 0)
            {
                List<string> externalEmails = GetAllExternalMails(externalTempContacts);
                //判断发件人是否存在联系人列表     
                List<OperationContactInfo> operationContactInfos = FCMInterfaceUtility.GetOperationContactListByEmails(externalEmails);
                if (IsContactExsitsOperationContactList(operationContactInfos))
                {
                    //在联系人列表表中（OperationContact）找到联系人跟邮件外部联系人对比，如果个数相同，发件人存在于联系人列表
                    if (operationContactInfos.Count == externalEmails.Count)
                    {
                        //判断发件人是否存在与当前选择的业务的联系人列表（OceanContact）
                        //业务联系人跟业务是多对多的关系
                        #region 保存过邮件中所有外部联系人(FCM.OperationContactCache表)
                        //根据邮件外部联系人地址找到所有联系人的业务集合
                        var serverAllContacts = FcmCommonService.GetOceanContactsByEmails(externalEmails);
                        if (IsContactExsitsOceanContactList(serverAllContacts))
                        {
                            bool needSetContact = false;
                            //得到历史业务联系人集合
                            var copyContacts = GetCopyHistoryOperationContact(externalEmails, serverAllContacts,
                                                                              ref needSetContact);
                            if (needSetContact)
                            {
                                SetDataSourceAndDefaultCheckBox(associateType, UsingOperationContext.OperationID,
                                                                UsingOperationContext.OperationType,
                                                                externalTempContacts);
                                return;
                            }

                            if (copyContacts.Count > 0)
                            {
                                FcmCommonService.SaveContactList(copyContacts);
                                //更新本地缓存业务
                                UpdateLocalOperationData();
                            }
                            //直接关联当前选择的业务
                            DefaultSaveOperationMessageAndCloseForm();

                        }
                        else
                        {
                            //存在联系人，但没有联系人跟业务关联，需要重新设置联系人
                            SetDataSourceAndDefaultCheckBox(associateType, UsingOperationContext.OperationID,
                                                               UsingOperationContext.OperationType,
                                                               externalTempContacts);
                        }

                        #endregion
                    }
                    else
                    {
                        //设置联系人
                        SetDataSourceAndDefaultCheckBox(associateType, UsingOperationContext.OperationID,
                                                        UsingOperationContext.OperationType, externalTempContacts);

                    }
                }
                else
                {
                    //不存在联系人列表，设置联系人
                    SetFormStateAndDataSource(externalTempContacts);
                    ucCustomerToolbar.InitType(true, true);
                    //默认设置gridview的联系人类型
                    ucCustomerList.ChangeDataRecordContactType(ContactType.Customer);
                }
            }
            else
                //直接关联当前选中的业务，不需要查询和设置联系人
                DefaultSaveOperationMessageAndCloseForm();

        }

        /// <summary>
        /// 更新本地缓存业务
        /// </summary>
        private void UpdateLocalOperationData()
        {
            List<Guid> operationIds = new List<Guid>();
            for (int i = 1; i < operationCount; i++)
            {
                operationIds.Add(_OperationContexts[i].OperationID);
            }
            CommonOperationService.UpdateLocalBusinessData(operationIds, UsingOperationContext.OperationType);
        }

        /// <summary>
        /// 设置联系人列表数据源和默认勾选CheckBox
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <param name="extenalContacts"></param>
        private void SetDataSourceAndDefaultCheckBox(AssociateType associateType, Guid operationID, OperationType operationType, List<CustomerCarrierObjects> extenalTempContacts)
        {
            ContactType contactType = ContactType.Customer;
            ContactObjects list = FcmCommonService.GetContactList(operationID, operationType);
            if (list != null && list.CustomerCarrier != null)
            {
                //勾选的第一票业务的联系人
                List<CustomerCarrierObjects> firstOperationContacts = list.CustomerCarrier;
                if (firstOperationContacts != null && firstOperationContacts.Count > 0)
                {
                    int count = extenalTempContacts.Count;
                    for (int i = 0; i < count; i++)
                    {
                        //找到业务数据库中的联系人后，替换成新的联系人
                        var contactInfo = firstOperationContacts.Find(item => item.Mail.Trim().Equals(extenalTempContacts[i].Mail, StringComparison.OrdinalIgnoreCase));
                        if (contactInfo != null)
                        {
                            contactType = contactInfo.Type;
                            string stage = contactInfo.Stage;
                            bool isCC = extenalTempContacts[i].IsCC;
                            extenalTempContacts[i] = contactInfo;
                            extenalTempContacts[i].SO = (associateType == AssociateType.WithStageSO);
                            extenalTempContacts[i].SI = (associateType == AssociateType.WithStageSI);
                            extenalTempContacts[i].IsCC = isCC;
                            extenalTempContacts[i].UpdateByID = LocalData.UserInfo.LoginID;
                            FCMInterfaceUtility.AppendContactStage(extenalTempContacts[i], new ContactStage?[1] { GetContactStage(associateType) });
                        }
                    }
                }
            }

            SetFormStateAndDataSource(extenalTempContacts);
            //设置默认勾选角色按钮
            ucCustomerToolbar.InitType(true, (contactType == ContactType.Customer));
            //默认设置gridview的联系人类型
            ucCustomerList.ChangeDataRecordContactType(ContactType.Customer);
        }

        /// <summary>
        ///将关联类型转换成联系人阶段
        /// </summary>
        /// <param name="associateType"></param>
        /// <returns></returns>
        private ContactStage GetContactStage(AssociateType associateType)
        {
            switch (associateType)
            {
                case AssociateType.Normal:
                    return ContactStage.Unknown;
                case AssociateType.WithStageSI:
                    return ContactStage.SI;
                case AssociateType.WithStageSO:
                    return ContactStage.SO;
                default:
                    return ContactStage.Unknown;
            }
        }

        /// <summary>
        /// 设置窗体状态和联系人数据源
        /// </summary>
        /// <param name="extenalContacts"></param>
        private void SetFormStateAndDataSource(List<CustomerCarrierObjects> extenalContacts)
        {
            Form mainForm = this.FindForm();
            if (mainForm != null)
            {
                mainForm.WindowState = FormWindowState.Normal;
                mainForm.Refresh();
            }
            ucCustomerList.InsertList(extenalContacts);
        }

        /// <summary>
        /// 判断是否存在业务联系人
        /// </summary>
        /// <param name="operationContactInfos"></param>
        /// <returns></returns>
        private bool IsContactExsitsOperationContactList(List<OperationContactInfo> operationContactInfos)
        {
            if (operationContactInfos != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 得到所有外部邮件地址
        /// </summary>
        /// <param name="externalContacts"></param>
        /// <returns></returns>
        private List<string> GetAllExternalMails(List<CustomerCarrierObjects> externalContacts)
        {
            return (from ec in externalContacts
                    select ec.Mail).ToList();
        }
        /// <summary>
        /// 判断存在业务联系人
        /// </summary>
        /// <param name="allMailContacts"></param>
        /// <returns></returns>
        private bool IsContactExsitsOceanContactList(List<CustomerCarrierObjects> serverContacts)
        {
            //根据邮件地址找到所有联系人的业务集合
            if (serverContacts != null && serverContacts.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 得到需要复制的历史业务联系人集合
        /// </summary>
        /// <param name="externalEmails"></param>
        /// <param name="externalContacts"></param>
        /// <param name="allMailContacts"></param>
        /// <returns></returns>
        private List<CustomerCarrierObjects> GetCopyHistoryOperationContact(List<String> externalEmails, List<CustomerCarrierObjects> serverAllContacts, ref bool needSaveContact)
        {
            var copyContacts = new List<CustomerCarrierObjects>();
            bool[] arrNeedSaveContacts = new bool[operationCount];
            for (int i = 0; i < operationCount; i++)
            {
                //根据业务去匹配联系人集合                               
                GetSingleCopyHistoryOperationContactByOperation(_OperationContexts[i].OperationID,
                                                            _OperationContexts[i].OperationType,
                                                            externalEmails,
                                                            serverAllContacts,
                                                            ref  copyContacts,
                                                            ref needSaveContact);

                arrNeedSaveContacts[i] = needSaveContact;
            }

            needSaveContact = arrNeedSaveContacts.Any(item => item);

            return copyContacts;
        }

        /// <summary>
        /// 通过单票业务获取需要根据历史复制的联系人
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="operationType"></param>
        /// <param name="emails"></param>
        /// <param name="operationContacts"></param>
        /// <param name="allMailContacts"></param>
        /// <param name="copyContacts"></param>
        private void GetSingleCopyHistoryOperationContactByOperation(Guid operationId,
                                                                    OperationType operationType,
                                                                    List<string> emails,
                                                                    List<CustomerCarrierObjects> serverAllContacts,
                                                                    ref List<CustomerCarrierObjects> copyContacts,
                                                                    ref bool needSaveContact)
        {
            needSaveContact = false;
            int count = emails.Count;
            for (int j = 0; j < count; j++)
            {
                string emailAddress = emails[j].Trim();
                //当前邮件地址的联系人

                var operationContactInfo = GetCurrentMailExsitsServerShipment(operationId, emailAddress, serverAllContacts);
                //根据业务历史联系人，设置业务联系人                    
                if (operationContactInfo == null)
                {
                    var mailContact = serverAllContacts.FirstOrDefault(item => item.Mail.Trim().Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
                    if (mailContact != null)
                    {
                        //找到历史联系人
                        CustomerCarrierObjects copyMailContactInfo =
                            ICP.Framework.ClientComponents.Controls.Utility.Clone(mailContact);
                        copyMailContactInfo.OceanBookingID = operationId;
                        copyMailContactInfo.OperationType = operationType;
                        copyMailContactInfo.Id = Guid.NewGuid();
                        copyMailContactInfo.UpdateByID = copyMailContactInfo.CreateByID = LocalData.UserInfo.LoginID;
                        copyMailContactInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now,
                                                                              DateTimeKind
                                                                                  .Unspecified);
                        copyMailContactInfo.UpdateDate = null;
                        FCMInterfaceUtility.AppendContactStage(copyMailContactInfo,
                                                   new ContactStage?[1] { GetContactStage(AssociateType) });
                        copyContacts.Add(copyMailContactInfo);
                    }
                    else
                        //如果在OceanContacts表中没有找到联系人的历史业务，就需要弹出界面设置联系人                     
                        needSaveContact = true;
                }
                else
                {
                    FCMInterfaceUtility.AppendContactStage(operationContactInfo, new ContactStage?[1] { GetContactStage(AssociateType) });
                    operationContactInfo.UpdateByID = LocalData.UserInfo.LoginID;
                    copyContacts.Add(operationContactInfo);
                }
            }
        }

        /// <summary>
        /// 判断联系人是否存在业务中
        /// </summary>
        /// <param name="operationContacts"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private CustomerCarrierObjects GetCurrentMailExsitsServerShipment(Guid operationId, string email, List<CustomerCarrierObjects> serverAllContacts)
        {
            return serverAllContacts.Find(item => item.Mail.Trim().Equals(email, StringComparison.OrdinalIgnoreCase) && item.OceanBookingID == operationId);
        }

        /// <summary>
        /// 系统默认保存业务与邮件的关联后关闭窗口
        /// </summary>
        /// <param name="sameContact"></param>
        private void DefaultSaveOperationMessageAndCloseForm()
        {
            Form form = this.FindForm();
            if (form != null)
            {
                form.ShowInTaskbar = false;
                form.Hide();
                Saved(false);
                form.Close();
            }
        }


        #region IDisposable 成员

        void DisposedCompoment()
        {
            ucCustomerToolbar.Added -= OnContactAdded;
            ucCustomerToolbar.Deleted -= OnContactDeleted;
            ucCustomerToolbar.Saved -= OnContactSaved;
            ucCustomerToolbar.ContactTypeChanged -= OnContactTypeChanged;
            ucCustomerToolbar.ReadOnlyChanged -= ReadOnlyChanged;
            OperationSaveController = null;
            OperationContexts.Clear();
            if (this.workItem != null)
            {
                this.workItem.Items.Remove(this);
            }
        }

        #endregion
    }
}

using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.Contact
{
    public partial class UCCustomerList : XtraUserControl
    {
        #region Service
        public WorkItem Workitem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }


        public IICPCommonOperationService ICPCommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }

        }
        private IClientBusinessOperationService clientBusinessOperationService;
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                if (clientBusinessOperationService == null)
                {
                    clientBusinessOperationService = ServiceClient.GetClientService<IClientBusinessOperationService>();
                }
                return clientBusinessOperationService;
            }
        }
        private IFCMCommonService _fcmCommonService;
        public IFCMCommonService FCMCommonService
        {
            get
            {
                if (_fcmCommonService == null)
                    _fcmCommonService = ServiceClient.GetService<IFCMCommonService>();
                return _fcmCommonService;
            }

        }
        public IClientContactNotifyService ClientContactNotifyService
        {
            get
            {
                return ServiceClient.GetClientService<IClientContactNotifyService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {

                return ServiceClient.GetClientService<IDataFindClientService>();

            }
        }


        public IDataFinderFactory DataFinderFactory
        {
            get
            {

                return ServiceClient.GetClientService<IDataFinderFactory>();

            }
        }
        /// <summary>
        /// 联系人所属的默认沟通阶段
        /// </summary>
        public ContactStage ContactStage { get; set; }

        /// <summary>
        /// 新增联系人默认所属的客户ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 新增联系人默认的客户类型
        /// </summary>
        public ContactType ContactType
        {
            get
            {
                return contactType;
            }
            set
            {
                contactType = value;
            }


        }
        private ContactType contactType = ContactType.Unknown;
        /// <summary>
        /// 新增联系人时默认所属的客户名称
        /// </summary>
        public string CustomerName { get; set; }
        #endregion

        /// <summary>
        /// 联系人类型
        /// </summary>
        public ContactType Type = ContactType.Customer;

        public bool AllowColumnEditable
        {
            get;
            set;
        }


        private bool[] _conditions;
        public bool[] conditions
        {
            get
            {
                return _conditions ??
                       (_conditions = new bool[9] { false, false, false, false, false, false, false, false, false });
            }
            set { _conditions = value; }

        }

        private BusinessOperationContext operationContext;
        public BusinessOperationContext OperationContext
        {
            get
            {
                return operationContext;
            }
            set
            {
                operationContext = value;
                SetStageComboBoxDataSource();
            }
        }
        /// <summary>
        /// 业务类型(默认为海出)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(OperationType.OceanExport)]
        public OperationType OperationType
        {
            get
            {
                if (operationContext != null && operationContext.OperationType != OperationType.Unknown)
                {
                    return operationContext.OperationType;
                }
                else
                {
                    return OperationType.OceanExport;
                }
            }

        }

        /// <summary>
        /// 设置沟通阶段数据源
        /// </summary>
        private void SetStageComboBoxDataSource()
        {
            if (repositoryItemCheckedComboBoxEdit != null)
            {
                repositoryItemCheckedComboBoxEdit.DataSource = FCMInterfaceUtility.GetStageInfoSource(OperationType);
            }
        }



        private void SetStageComboBoxItems()
        {

            if (repositoryItemCheckedComboBoxEdit != null)
            {
                List<ContactStageInfo> list = FCMInterfaceUtility.GetStageInfoSource(OperationType);
                foreach (ContactStageInfo item in list)
                {
                    repositoryItemCheckedComboBoxEdit.Items.Add(item.StageName, false);
                }
            }
        }

        public Guid CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示客户名称列，默认不显示
        /// </summary>
        private bool isShowCustomerNameColumn = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsShowCustomerNameColumn
        {
            get
            {
                return isShowCustomerNameColumn;
            }
            set
            {

                isShowCustomerNameColumn = value;
                if (!isShowCustomerNameColumn)
                {
                    HideCustomerNameColumn();
                }


            }
        }
        /// <summary>
        /// 是否显示控件的标题
        /// </summary>
        public bool IsShowCaption
        {
            get
            {
                return groupCustomer.ShowCaption;
            }
            set
            {

                groupCustomer.ShowCaption = value;

            }
        }

        private void HideCustomerNameColumn()
        {
            colCustomerName.Visible = false;
            colCustomerName.GroupIndex = -1;
            colEMail.Width = 100;
            colTel.Width = 40;
            colFax.Width = 40;
            colCC.Width = 30;
            colStage.Width = 50;
            colName.Width = 85;
        }

        public EventHandler<CommonEventArgs<string>> CustomerErrorEventHandler;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UCCustomerList()
        {
            InitializeComponent();

            Disposed += delegate
            {
                CustomerErrorEventHandler = null;

                _fcmCommonService = null;
                gcCustomer.DataSource = null;
                if (bsCustomerCarrier != null)
                {
                    bsCustomerCarrier.DataSource = null;
                    bsCustomerCarrier = null;
                }
                if (ClientContactNotifyService != null)
                {
                    ClientContactNotifyService.ContactChanged -= ClientContactNotifyService_ContactChanged;
                }
                customerCarrierList = null;
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                }
            };

            AllowColumnEditable = true;


            if (!LocalData.IsDesignMode)
            {
                Load += delegate
                {
                    if (LocalData.ApplicationType == ApplicationType.EmailCenter)
                        ColType.OptionsColumn.AllowEdit = false;
                    else
                        ColType.OptionsColumn.AllowEdit = true;

                    IsShowCustomerNameColumn = true;
                    repositoryItemCheckEditType.ValueChecked = ContactType.Customer;
                    repositoryItemCheckEditType.ValueUnchecked = ContactType.Carrier;
                    SetCustomerGridListToolTip();
                    if (bsCustomerCarrier.DataSource == null)
                    {
                        bsCustomerCarrier.DataSource = new List<CustomerCarrierObjects>();
                    }
                    if (LocalData.ApplicationType == ApplicationType.ICP)
                    {
                        ClientContactNotifyService.ContactChanged += new EventHandler<CommonEventArgs<List<CustomerCarrierObjects>>>(ClientContactNotifyService_ContactChanged);
                    }
                    RegisterSearch();


                };
            }


        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)

            { }
            //Init();
        }

        private IDisposable customerFinder;
        private void RegisterSearch()
        {
            customerFinder = DataFindClientService.RegisterGridColumnFinder(colCustomerName
                                   , CommonFinderConstants.CustoemrFinder
                                    , _sourcefields
                                   , _resultfields);
        }

        private string[] _sourcefields = new string[] { "CustomerID", "CustomerName" };
        private string[] _resultfields = new string[] { "ID", "EName" };

        private ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 设置客户类型列是否只读
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetCustomerTypeColumnReadOnly(bool readOnly)
        {
            ColType.OptionsColumn.ReadOnly = readOnly;
        }

        void ClientContactNotifyService_ContactChanged(object sender, CommonEventArgs<List<CustomerCarrierObjects>> args)
        {
            List<CustomerCarrierObjects> contactList = args.Data;
            if (contactList == null || contactList.Count <= 0)
            {
                return;
            }
            if (ReferenceEquals(this, sender))
                return;
            //判断当前操作对象是否为空
            if (operationContext == null)
                return;
            if (contactList[0].OceanBookingID != operationContext.OperationID)
            {
                return;
            }
            List<Guid> ids = (from item in contactList select item.Id).ToList();
            DataSourceList.RemoveAll(item => ids.Contains(item.Id));
            DataSourceList.AddRange(contactList);
            bsCustomerCarrier.ResetBindings(false);

        }

        public void SetCustomerGridListToolTip()
        {
            colStage.ToolTip = LocalData.IsEnglish ? @"AN: Indicates that the customer will be communicated during Sending Arrive Notes phase of the shipment." + Environment.NewLine +
                                                          "SO: Indicates that the customer will be communicated during Booking phase of the shipment, eg. accepting booking docs from the customer, booking confirmation, solving the exceptions like re-booking/changing container size" + Environment.NewLine +
                                                           "Trk: Indicates that the customer will be communicated during Trucking phase of the shipment, eg. confirming pickup time, solving the exceptions like delay products/delay arrival" + Environment.NewLine +
                                                           "CF: Indicates that the customer will be communicated during Customs Clearance phase of the shipment, eg. providing the address to receive the stuff of Customs Clearance. reporting if release afte" + Environment.NewLine +
                                                          "FU: Indicates that the customer will be communicated during Fumigation phase of the shipment" + Environment.NewLine +
                                                          "Whs: Indicates that the customer will be communicated during Warehouse phase of the shipment" + Environment.NewLine +
                                                          "IN: Indicates that the customer will be communicated during Insurance phase of the shipment" + Environment.NewLine +
                                                          "SI: Indicates that the customer will be communicated during Shipping Instruction phase of the shipment, eg. Reminding the customer for SI, accepting SI stuff, confirming SI and revising BL" + Environment.NewLine +
                                                          "BL: Indicates that the customer will be communicated during last phase of the shipment, eg. Confirming debit notes, Releaseing BL, Dispatching Ref.Docs" + Environment.NewLine +
                                                          "IQ:Inquire Price" :
                                                          "AN: 标识此客户属于到港通知书的沟通阶段，如向此客户发送到港通知书." + Environment.NewLine +
                                                          "SO: 标识此客户属于订舱的沟通阶段，如接受此客户的订舱指令，通知客户确认订舱，跟客户讨论订舱异常（改柜形、船期、船公司)" + Environment.NewLine +
                                                          "Trk: 标识此客户属于委派拖车的沟通阶段，如向客户询问装柜时间，跟客户讨论拖车异常（货未能按时准备，拖车晚点...)" + Environment.NewLine +
                                                          "CF: 标识此客户属于委托报关的沟通阶段，如向客户提供寄报关资料地址，通知海关已放行.." + Environment.NewLine +
                                                          "FU: 标识此客户属于熏蒸的沟通阶段" + Environment.NewLine +
                                                          "Whs: 标识此客户属于仓储的沟通阶段" + Environment.NewLine +
                                                         "IN: 标识此客户属于保险的沟通阶段" + Environment.NewLine +
                                                         "SI: 标识此客户属于核对提单的沟通阶段，如提醒客户补料，客户提供补料，确认补料，更改提单." + Environment.NewLine +
                                                         "BL: 标识此客户属于作单的最后沟通阶段，如向客户确认账单、通知放单、分文件(第三方代理)." + Environment.NewLine +
                                                         "IQ: 标识属于询价的沟通阶段";
            colCC.ToolTip = LocalData.IsEnglish ? "Indicates that the customer is only the role of spectators during any phase of the shipment" : "标识此客户在此单的任何沟通阶段，只是旁观或知会的沟通的对象.";
            ColType.ToolTip = LocalData.IsEnglish ? "Indicates that the property of the contact. checked if the contact is Customer. unchecked if the contact is Carrier." : "标识此联系人性质（客户、承运人），勾选状态是客户，不勾选状态是承运人";

        }






        /// <summary>
        /// 获取当前选择项的数据
        /// </summary>
        CustomerCarrierObjects CurrentCustomerCarrierObject
        {
            get { return bsCustomerCarrier.Current as CustomerCarrierObjects; }
        }

        /// <summary>
        /// 数据是否验证通过
        /// </summary>
        public bool IsValidatePass { get; set; }

        bool _isChanged = false;

        /// <summary>
        /// 列表数据是否有修改
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (DataSourceList != null)
                {
                    return DataSourceList.Exists(o => o.IsDirty || o.IsNew);

                }
                return _isChanged;
            }
            set { _isChanged = value; }
        }

        public void SetGroupControlText(string text)
        {
            groupCustomer.Text = text;
        }

        public void ShowCaption(bool showCaption)
        {
            groupCustomer.ShowCaption = showCaption;
        }

        /// <summary>
        /// 获取控件数据源List集合
        /// </summary>
        public List<CustomerCarrierObjects> DataSourceList
        {
            get { return bsCustomerCarrier.DataSource as List<CustomerCarrierObjects>; }
        }

        /// <summary>
        /// 从列表中移除当前项
        /// </summary>
        public void RemoveCurrent()
        {
            if (bsCustomerCarrier.Current != null)
            {
                bsCustomerCarrier.RemoveCurrent();
            }
        }
        public void RemoveContactList(Guid customerID,
            ContactType? contactType)
        {
            if (contactType != null)
            {
                DataSourceList.RemoveAll(item => item.CustomerID == customerID && item.Type == contactType);
            }
            else
            {
                DataSourceList.RemoveAll(item => item.CustomerID == customerID);
            }
            bsCustomerCarrier.ResetBindings(false);
        }


        /// <summary>
        /// 插入一列到列表尾
        /// </summary>
        /// <param name="indx"></param>
        /// <param name="value"></param>
        public void Insert(CustomerCarrierObjects contactInfo)
        {
            if (bsCustomerCarrier.DataSource == null)
            {
                bsCustomerCarrier.DataSource = new List<CustomerCarrierObjects>();
            }
            bsCustomerCarrier.Insert(bsCustomerCarrier.Count, contactInfo);

            bsCustomerCarrier.ResetBindings(false);
        }
        public void InsertList(List<CustomerCarrierObjects> contactList)
        {
            if (bsCustomerCarrier.DataSource == null)
            {
                bsCustomerCarrier.DataSource = new List<CustomerCarrierObjects>();
            }
            DataSourceList.AddRange(contactList);

            ResetBind();
        }

        /// <summary>
        /// 获取列表中的当前项
        /// </summary>
        public object Current
        {
            get
            {
                return bsCustomerCarrier.Current;
            }
        }
        /// <summary>
        /// 向列表添加一行记录
        /// </summary>
        public void AddNewDataRecord()
        {
            AddNewDataRecord(string.Empty, string.Empty);
        }

        public void AddNewDataRecord(Guid operatonID, string emailAddress, string senderName)
        {
            Insert(CreateCustomerCarrierInfo(operatonID, emailAddress, senderName));
            MoveFirst();
            ResetBind();
        }

        /// <summary>
        /// 向列表添加一行记录
        /// </summary>
        /// <param name="emailAddress">客户邮件地址</param>
        /// <param name="senderName">客户名称</param>
        public void AddNewDataRecord(string emailAddress, string senderName)
        {
            Insert(CreateCustomerCarrierInfo(Guid.Empty, emailAddress, senderName));

            MoveFirst();
            ResetBind();
        }

        public void SetContactListDataSource(BusinessOperationContext operationContext)
        {
            OperationContext = operationContext;
            ContactObjects dataList = FCMCommonService.GetContactList(this.operationContext.OperationID, this.operationContext.OperationType);
            if (dataList != null && dataList.CustomerCarrier != null)
            {
                DataSource = dataList.CustomerCarrier;
            }
        }
        /// <summary>
        /// 邮件中心，当用户勾选新的联系人类型时，需要切换列表中已添加数据的联系人类型
        /// </summary>
        public void ChangeDataRecordContactType(ContactType contactType)
        {
            if (DataSourceList != null && DataSourceList.Count > 0)
            {
                for (int i = 0; i < DataSourceList.Count; i++)
                {
                    DataSourceList[i].Type = contactType;
                }
            }
            ResetBind();
        }

        private CustomerCarrierObjects CreateEntity(Guid operationID, string emailAddress, string senderName)
        {
            CustomerCarrierObjects customerCarrier = new CustomerCarrierObjects();
            #region  根据传递过来的默认选择用户类型的参数选择客户的默认类型
            FCMInterfaceUtility.SetStage(customerCarrier, ContactStage);
            #endregion
            customerCarrier.CreateDate = DateTime.Now;
            customerCarrier.CreateByID = LocalData.UserInfo.LoginID;
            customerCarrier.UpdateDate = DateTime.Now;
            customerCarrier.UpdateByID = LocalData.UserInfo.LoginID;
            customerCarrier.Type = Type;
            customerCarrier.OceanBookingID = operationID;
            if (!string.IsNullOrEmpty(emailAddress))
            {
                if (SetContactListEditable())
                {
                    customerCarrier.Mail = emailAddress;

                    customerCarrier.Name = senderName;
                }
            }

            if (customerCarrier.CustomerID == null || customerCarrier.CustomerID == Guid.Empty)
            {
                if (CustomerID != Guid.Empty)
                {
                    customerCarrier.CustomerID = CustomerID;
                    customerCarrier.Type = ContactType;
                    customerCarrier.CustomerName = CustomerService.GetCustomerInfo(CustomerID).EName;
                }
                else
                {
                    int rowHandle = gvCustomer.FocusedRowHandle;
                    DataRow row = null;
                    if (rowHandle >= 0)
                    {
                        customerCarrier.CustomerID = CurrentCustomerCarrierObject.CustomerID;
                        customerCarrier.CustomerName = CurrentCustomerCarrierObject.CustomerName;
                        return customerCarrier;
                    }
                    else
                    {
                        if (gvCustomer.IsGroupRow(rowHandle))
                        {
                            int childCount = gvCustomer.GetChildRowCount(rowHandle);
                            int childIndex = gvCustomer.GetChildRowHandle(rowHandle, childCount - 1);
                            int temp = gvCustomer.GetDataSourceRowIndex(childIndex);
                            row = gvCustomer.GetDataRow(temp);

                        }
                    }
                    if (row != null)
                    {
                        customerCarrier.CustomerID = row.Field<Guid?>("CustomerID");
                        customerCarrier.CustomerName = row.Field<string>("CustomerName");
                    }
                }
            }

            return customerCarrier;
        }

        /// <summary>
        /// 添加一行数据的方法
        /// </summary>
        /// <param name="isFillEmailAddress">是否自动填写Email</param>
        /// <returns></returns>
        private CustomerCarrierObjects CreateCustomerCarrierInfo(Guid operationID, string emailAddress, string senderName)
        {
            return CreateEntity(operationID, emailAddress, senderName);
        }


        private bool SetContactListEditable()
        {
            AllowColumnEditable = true;
            return true;
        }

        private void SetEmailAndNameColumnAllowEdit(bool allowEdit)
        {
            colEMail.OptionsColumn.AllowEdit = true;
            colName.OptionsColumn.AllowEdit = true;
        }

        /// <summary>
        /// 移至列表第一项
        /// </summary>
        public void MoveFirst()
        {
            if (bsCustomerCarrier.Current != null)
            {
                gvCustomer.UpdateCurrentRow();
                bsCustomerCarrier.MoveFirst();
            }
        }

        public void ResetBindings()
        {
            bsCustomerCarrier.ResetBindings(false);
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            get
            {
                return bsCustomerCarrier.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        List<CustomerCarrierObjects> customerCarrierList;
        private void BindingData(object value)
        {
            if (value != null)
            {
                customerCarrierList = value as List<CustomerCarrierObjects>;
            }

            if (customerCarrierList != null && customerCarrierList.Count != 0)
            {
                bsCustomerCarrier.DataSource = customerCarrierList;
            }
            else
            {
                bsCustomerCarrier.DataSource = new List<CustomerCarrierObjects>();
            }
            //SetStageComboBoxDataSource();
            bsCustomerCarrier.ResetBindings(false);
        }

        #region 设置控件是否可编辑,默认都是可以编辑的
        public void Init(bool? editable, bool? showCaption)
        {
            GridColumnsEditable(editable);
            GroupControlShowCaption(showCaption);
        }

        private void GridColumnsEditable(bool? readOnly)
        {
            if (readOnly == null || !readOnly.Value)
            {
                gvCustomer.OptionsBehavior.Editable = false;
            }
            else
            {
                gvCustomer.OptionsBehavior.Editable = true;
            }
        }

        private void GroupControlShowCaption(bool? showCaption)
        {
            if (showCaption == null || !showCaption.Value)
            {
                groupCustomer.ShowCaption = false;
            }
            else
            {
                groupCustomer.ShowCaption = true;
            }
        }

        #endregion

        /// <summary>
        /// 是否是客户列表界面
        /// </summary>
        public bool IsCustomerPart
        {
            set
            {
                groupCustomer.Text = value ? LocalData.IsEnglish ? "Customer List" : "客户列表" : LocalData.IsEnglish ? "Carrier List" : "承运人列表";
            }
        }

        private void gcCustomer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);

        }

        /// <summary>
        /// 右键新建一个邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSendEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentCustomerCarrierObject != null)
            {
                Message.ServiceInterface.Message message = InitMessage();
                ICPCommonOperationService.SendMessage(message);
            }
        }

        private Message.ServiceInterface.Message InitMessage()
        {
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            message.SendTo = CurrentCustomerCarrierObject.Mail;
            message.Type = MessageType.Email;
            return message;
        }


        void barNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddNewDataRecord();
        }


        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            RemoveCurrent();
        }

        /// <summary>
        /// 列选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCustomer_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.UnboundType == UnboundColumnType.Boolean)
            {
                if (e.Column == colCC)
                {
                    CurrentCustomerCarrierObject.IsCC = !CurrentCustomerCarrierObject.IsCC;
                }
                bsCustomerCarrier.ResetCurrentItem();
            }
        }

        ///// <summary>
        ///// 刷新显示值
        ///// </summary>
        public void ResetBind()
        {
            bsCustomerCarrier.ResetBindings(false);
        }

        public bool IsNullOrEmtpyDataSource()
        {

            if (bsCustomerCarrier != null && bsCustomerCarrier.DataSource != null)
            {
                if (DataSourceList == null || DataSourceList.Count <= 0)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        public bool ValidateData()
        {
            errorString = string.Empty;
            Validate();
            bsCustomerCarrier.EndEdit();

            if (bsCustomerCarrier.DataSource == null)
            {
                return true;
            }
            bool isPassed1 = Validated();
            if (!isPassed1)
            {
                return isPassed1;
            }

            //bool isPassed2 = ValidateStage();
            //if (!isPassed2)
            //{
            //    return isPassed2;
            //}

            return true;
        }
        public string ValidateData2()
        {
            ValidateData();
            return errorString;
        }



        private bool ValidateStage(string stage)
        {
            if (string.IsNullOrEmpty(stage))
            {
                errorString = (LocalData.IsEnglish ? "At least one stage should be selected for the contact!" : "至少选择一个沟通阶段！");
                if (!ValidateReturnErrorString)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), errorString);
                }
                return false;
            }
            return true;

        }
        private string errorString = string.Empty;
        private bool validateReturnErrorString = false;
        /// <summary>
        /// 设置验证数据是提示错误还是返回错误描述字符串
        /// </summary>
        public bool ValidateReturnErrorString
        {
            get
            {
                return validateReturnErrorString;
            }
            set
            {
                validateReturnErrorString = value;
            }
        }
        private bool Validated()
        {

            for (int i = 0; i <= DataSourceList.Count - 1; i++)
            {
                CustomerCarrierObjects contactItem = DataSourceList[i];
                if (contactItem.Validate() == false)
                    return false;

                if (contactItem.Name == null || contactItem.Name.Trim() == "")
                {
                    errorString = LocalData.IsEnglish ? "Contact NAME IS NULL" : "联系人姓名为空！";
                    SetErrorInfo(contactItem.GetType().FullName, "Name");
                    return false;
                }

                string mail = contactItem.Mail;
                if (mail == null || !ValidateEmail(mail.Trim()))
                {
                    errorString = LocalData.IsEnglish ? "Contact Email address is incorrect" : "联系人邮件地址有误！";
                    SetErrorInfo(contactItem.GetType().FullName, "Mail");
                    return false;

                }
                if (!string.IsNullOrEmpty(mail) && ValidateEmailIsInternalDomain(mail))
                {
                    errorString = LocalData.IsEnglish ? "The input contact email address should belong to the customer or supplier instead of our company, such as @Cityocean.com, @Topshipping.com or etc. " : "输入的联系人邮箱必须属于客户或供应商，而不是公司同事，如： @Cityocean.com、@Topshipping.com...";
                    SetErrorInfo(contactItem.GetType().FullName, "Mail");
                    return false;
                }

                if (DataSourceList.FindAll(item => item.Mail.Equals(mail, StringComparison.OrdinalIgnoreCase)).Count > 1)
                {
                    errorString = LocalData.IsEnglish
                            ? mail.Trim() + "Repeat e-mail address."
                            : mail.Trim() + "邮件地址重复！";
                    SetErrorInfo(contactItem.GetType().FullName, "Mail");
                    return false;
                }

                //if (string.IsNullOrEmpty(contactItem.Stage))
                //{
                //    errorString = (LocalData.IsEnglish ? "At least one stage should be selected for the contact!" : "至少选择一个沟通阶段！");
                //    SetErrorInfo(contactItem.GetType().FullName, "Stage");
                //    return false;
                //}

                if (!string.IsNullOrEmpty(contactItem.Tel))
                {
                    if (Regex.Matches(contactItem.Tel, "[a-zA-Z]").Count > 0)
                    {
                        errorString = LocalData.IsEnglish
                           ? "The input tel can't contains letters"
                           : "Tel不能包含字母！";
                        SetErrorInfo(contactItem.GetType().FullName, "Tel");
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(contactItem.Fax))
                {
                    if (Regex.Matches(contactItem.Fax, "[a-zA-Z]").Count > 0)
                    {
                        errorString = LocalData.IsEnglish
                           ? "The input fax can't contains letters"
                           : "Fax不能包含字母！";
                        SetErrorInfo(contactItem.GetType().FullName, "Fax");
                        return false;
                    }
                }
            }

            return true;
        }

        private void SetErrorInfo(string typeName, string filedName)
        {
            if (!ValidateReturnErrorString)
            {
                //LocalCommonServices.ErrorTrace.SetErrorInfo(
                //    null,
                //    errorString,
                //    typeName,
                //    filedName);
                MessageBox.Show(errorString);
            }
        }

        /// <summary>
        /// 验证输入的客户或者承运人邮箱地址是否为内部邮箱
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        private bool ValidateEmailIsInternalDomain(string mail)
        {
            string domains = ClientHelper.GetAppSettingValue("InternalDomain");
            if (string.IsNullOrEmpty(domains))
                return false;
            List<string> list = domains.Split(',').Where(item => !string.IsNullOrEmpty(item)).ToList();
            string emails = string.Empty;
            if (list.Count <= 0)
            {
                return false;
            }
            else if (list.Count == 1)
            {
                emails = "(" + list[0] + ")";
            }
            else
            {
                emails = list.Aggregate((a, b) => "(" + a + ")" + "(" + b + ")");
            }
            emails = emails.Replace(")(", ")|(");

            string regex = emails + "$";
            return Regex.IsMatch(mail, regex);



        }

        /// <summary>
        /// 保存业务联系人
        /// </summary>
        /// <param name="contactType"></param>
        /// <returns></returns>
        public bool InnerSaved(ContactType? contactType)
        {
            return Saved(contactType);
        }

        /// <summary>
        /// 保存列表数据到远程数据库，更新本地联系人数据表
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return Saved(null);
        }

        private bool Saved(ContactType? contactType)
        {
            if (bsCustomerCarrier.DataSource == null || DataSourceList.Count == 0)
            {
                return false;
            }

            try
            {
                // 保存之前，做好数据的准备
                List<string> emailList = PrepareDataSource(contactType);
                // 保存业务联系人后刷新数据源
                SaveOperationContactAndRefershDataSource();
                //更新本地缓存业务联系人
                LocalSave(emailList);

                CallBackChangedContactNotify();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void CallBackChangedContactNotify()
        {
            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                ClientContactNotifyService.SendContactChange(this, DataSourceList);
            }
        }

        /// <summary>
        /// 保存之前，做好数据的准备
        /// </summary>
        /// <param name="contactType"></param>
        /// <returns></returns>
        private List<string> PrepareDataSource(ContactType? contactType)
        {
            List<string> emailList = new List<string>();

            //找到业务的原有联系人
            bool exsitsOperationContacts = false;

            List<CustomerCarrierObjects> currentShipmentCustomerCarriers = null;
            if (contactType.HasValue)
            {
                currentShipmentCustomerCarriers = FCMCommonService.GetContactList(operationContext.OperationID, operationContext.OperationType)
                                    .CustomerCarrier;
                if (currentShipmentCustomerCarriers != null && currentShipmentCustomerCarriers.Count > 0)
                    exsitsOperationContacts = true;
            }
            int rowCount = DataSourceList.Count;
            for (int i = 0; i < rowCount; i++)
            {
                CustomerCarrierObjects contact = DataSourceList[i];
                FCMInterfaceUtility.SetStage(contact, contact.Stage);
                contact.UpdateByID = LocalData.UserInfo.LoginID;
                contact.OceanBookingID = operationContext.OperationID;
                if (contactType.HasValue)
                {
                    contact.Type = contactType.Value;
                    if (exsitsOperationContacts)
                    {
                        contact = ReplaceCustomerCarrierInfo(currentShipmentCustomerCarriers, contact);
                    }
                }
                emailList.Add(contact.Mail.Trim());
            }

            return emailList;
        }

        /// <summary>
        /// 保存业务联系人后刷新数据源
        /// </summary>
        private void SaveOperationContactAndRefershDataSource()
        {
            ManyResult mans = FCMCommonService.SaveContactList(DataSourceList);
            int count = mans.Items.Count;
            for (int i = 0; i < count; i++)
            {
                DataSourceList[i].Id = mans.Items[i].GetValue<Guid>("ID");

                DataSourceList[i].UpdateDate = mans.Items[i].GetValue<DateTime?>("UpdateDate");
                DataSourceList[i].Type =
                    (ContactType)mans.Items[i].GetValue<Byte>("Type");
                DataSourceList[i].IsDirty = false;
            }

            bsCustomerCarrier.ResetBindings(false);
        }

        private CustomerCarrierObjects ReplaceCustomerCarrierInfo(List<CustomerCarrierObjects> currentShipmentCustomerCariiers, CustomerCarrierObjects newCustomerCariier)
        {
            CustomerCarrierObjects info = currentShipmentCustomerCariiers.Find(item => item.Mail.Equals(newCustomerCariier.Mail.Trim(), StringComparison.OrdinalIgnoreCase));
            if (info != null)
            {
                newCustomerCariier.Id = info.Id;
                newCustomerCariier.UpdateByID = LocalData.UserInfo.LoginID;
                newCustomerCariier.UpdateDate = info.UpdateDate;
            }
            return newCustomerCariier;
        }

        private void LocalSave(List<string> emailList)
        {
            List<OperationContactInfo> contactList = ClientBusinessOperationService.GetOperationContactByEmails(emailList);
            ClientBusinessOperationService.LocalSaveOperationContacts(contactList);
            clientBusinessOperationService.UpdateLocalBusinessData(OperationContext.OperationID, OperationContext.OperationType);

            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                IClientBusinessContactService clientBusinessContactService = ServiceClient.GetClientService<IClientBusinessContactService>();
                clientBusinessContactService.RemoveMemCacheContacts(emailList);
            }

        }

        private List<bool> CheckOperationType(OperationType operationType, int count)
        {
            bool value = false;
            if (OperationType == operationType)
            {
                value = true;

            }
            List<bool> items = new List<bool>();
            for (int i = 0; i < count; i++)
            {
                items.Add(value);
            }
            return items;
        }

        private void TriggerCustomerErrorEvent(string errorMessage)
        {

            if (CustomerErrorEventHandler != null)
            {
                CustomerErrorEventHandler(this, new CommonEventArgs<string>(errorMessage));
            }

        }

        /// <summary>
        /// 验证Email地址
        /// </summary>
        /// <param name="strEmailAddress"></param>
        /// <returns></returns>
        private bool ValidateEmail(string strEmailAddress)
        {
            if (string.IsNullOrEmpty(strEmailAddress))
                return false;
            string EmailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";//E-Mail地址格式的正则表达式
            return Regex.IsMatch(strEmailAddress, EmailPattern);
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="strTel"></param>
        /// <returns></returns>
        private bool ValidateTel(string strTel)
        {
            string TelPattern = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)";//Tel地址格式的正则表达式
            return Regex.IsMatch(strTel, TelPattern);

        }

        /// <summary>
        /// 验证传真地址
        /// </summary>
        /// <param name="faxNo"></param>
        /// <returns></returns>
        private bool ValidFaxNo(string faxNo)
        {
            string patten = @"^[0-9\-]{7,20}$";

            return Regex.IsMatch(faxNo, patten);

        }




        private void ValidColumn(ColumnView columnView, GridColumn gridColumn)
        {
            switch (gridColumn.FieldName)
            {
                case "Name":
                    if (columnView.EditingValue.ToString() == "null" || columnView.EditingValue.ToString() == "")
                    {
                        IsValidatePass = false;
                        columnView.SetColumnError(gridColumn, "请输入客户姓名！");
                    }
                    else
                    {
                        //columnView.ClearColumnErrors();
                    }
                    break;
                case "Mail":
                    if (!ValidateEmail(columnView.EditingValue.ToString()))
                    {
                        IsValidatePass = false;
                        columnView.SetColumnError(gridColumn, "您输入的邮件地址有误！");
                    }
                    else
                    {
                        //columnView.ClearColumnErrors();
                    }
                    break;
                case "Tel":
                    if (!ValidateTel(columnView.EditingValue.ToString()))
                    {
                        IsValidatePass = false;
                        //olumnView.SetColumnError(gridColumn, "您输入的电话有误！");
                    }
                    else
                    {
                        columnView.ClearColumnErrors();
                    }
                    break;
                case "Fax":
                    if (!ValidFaxNo(columnView.EditingValue.ToString()))
                    {
                        IsValidatePass = false;
                        columnView.SetColumnError(gridColumn, "您输入的传真有误！");
                    }
                    else
                    {
                        //columnView.ClearColumnErrors();
                    }
                    break;
            }
        }

        [CommandHandler("Command_AddCustomerInfo")]
        public void Command_AddCustomerInfo(object sender, EventArgs e)
        {
            Insert(CreateCustomerCarrierInfo());
        }

        public CustomerCarrierObjects CreateCustomerCarrierInfo()
        {
            CustomerCarrierObjects customerCarrier = new CustomerCarrierObjects();
            customerCarrier.CreateDate = DateTime.Now;
            customerCarrier.CreateByID = LocalData.UserInfo.LoginID;
            customerCarrier.UpdateDate = DateTime.Now;
            customerCarrier.UpdateByID = LocalData.UserInfo.LoginID;
            return customerCarrier;
        }

        [CommandHandler("Command_RemoveCustomerInfo")]
        public void Command_RemoveCustomerInfo(object sender, EventArgs e)
        {
            Remove();
        }

        public void Remove()
        {
            if (Current != null)
            {
                DialogResult dlg =
                    XtraMessageBox.Show(
                        LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?",
                        LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dlg != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    var varEvent = Current as CustomerCarrierObjects;
                    if (varEvent.Id != Guid.Empty)
                    {
                        FCMCommonService.RemoveContactInfo(varEvent.Id, LocalData.UserInfo.LoginID, varEvent.UpdateDate);
                    }
                    RemoveCurrent();
                    MoveFirst();
                    ResetBindings();
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),
                                                                   LocalData.IsEnglish
                                                                       ? "List Data is null."
                                                                  : "列表数据为空！");

            }
        }
    }

}


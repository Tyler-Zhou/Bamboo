using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Business.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using System.Threading;
using ICP.Framework.CommonLibrary.Client;
using ICP.Business.Common.UI.Contact;
using ICP.Business.Common.UI.Document;
using EnumDocumentType = ICP.FileSystem.ServiceInterface.DocumentType;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Business.Common.UI.Common
{
    /// <summary>
    /// 联系人和文档组合控件
    /// </summary>
    public partial class UCContactAndDocumentPart : BaseEditPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        public IICPCommonOperationService FcmCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }
        private UCBusinessDocumentList ucBusinessDocumentList;

        public UCBusinessDocumentList UCBusinessDocumentList
        {
            get
            {
                if (ucBusinessDocumentList != null)
                {
                    return ucBusinessDocumentList;

                }
                else
                {
                    ucBusinessDocumentList = Workitem.Items.AddNew<UCBusinessDocumentList>();
                    return ucBusinessDocumentList;
                }
            }
        }

        private UCCustomerToolbar _ucCustomerToolbar;
        public UCCustomerToolbar UCCustomerToolbar
        {
            get
            {
                if (_ucCustomerToolbar != null)
                {
                    return _ucCustomerToolbar;
                }
                else
                {
                    _ucCustomerToolbar = Workitem.Items.AddNew<UCCustomerToolbar>();
                    return _ucCustomerToolbar;
                }
            }
        }

        /// <summary>
        /// 联系人信息列表
        /// </summary>
        public List<CustomerCarrierObjects> CurrentContactList
        {
            get
            {
                return UCCustomerList.DataSourceList;
            }
        }
        private UCCustomerList ucCustomerList;
        public UCCustomerList UCCustomerList
        {
            get
            {
                if (ucCustomerList != null)
                {
                    return ucCustomerList;
                }
                else
                {
                    ucCustomerList = Workitem.Items.AddNew<UCCustomerList>();
                    ucCustomerList.Type = ContactType.Customer;
                    return ucCustomerList;
                }
            }
        }
        /// <summary>
        /// 使用的业务上下文
        /// </summary>
        private BusinessOperationContext UsingOperationContext { get; set; }

        #endregion

        private BusinessOperationContext context;

        ContactObjects contactSoure = null;

        /// <summary>
        /// 数据是否改变
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (UCBusinessDocumentList.IsChanged == true || UCCustomerList.IsChanged == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public BusinessOperationContext SetContext
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
                UCBusinessDocumentList.Context = value;
                UCCustomerList.OperationContext = value;
            }
        }

        public List<string> CurrentDocumentName
        {
            get
            {
                if (UCBusinessDocumentList.DataSource != null &&
                    UCBusinessDocumentList.DataSource.Count != 0)
                {
                    List<string> strlist = new List<string>();
                    foreach (var DocumentInfo in UCBusinessDocumentList.DataSource)
                    {
                        strlist.Add(DocumentInfo.Name);
                    }
                    return strlist;
                }
                return null;
            }
        }


        public List<DocumentInfo> CurrentDocumentList
        {
            get
            {
                return UCBusinessDocumentList.DataSource;
            }
        }

        /// <summary>
        /// 客户保存异常事件
        /// </summary>
        public EventHandler<CommonEventArgs<string>> CustomerFailedEventHandler;

        /// <summary>
        /// 文档上传保存异常事件
        /// </summary>
        public EventHandler<CommonEventArgs<string>> UploadFailedEventHandler;

        public UCContactAndDocumentPart()
        {
            InitializeComponent();



            Disposed += delegate
            {

                contactSoure = null;
                if (Workitem != null)
                {

                    Workitem.Items.Remove(ucCustomerList);
                    Workitem.Items.Remove(ucBusinessDocumentList);
                    if (UCBusinessDocumentList != null)
                    {
                        UCBusinessDocumentList.UploadFailedEventHandler -= TriUploadFailedEvent;
                        Workitem.Items.Remove(ucBusinessDocumentList);
                        ucBusinessDocumentList = null;
                    }
                    if (UCCustomerList != null)
                    {

                        UCCustomerList.CustomerErrorEventHandler -= TriCustomerFailedEvent;
                        Workitem.Items.Remove(ucCustomerList);
                        ucCustomerList = null;
                    }
                    if (UCCustomerToolbar != null)
                    {

                        UCCustomerToolbar.Added -= new EventHandler(OnContactAdded);
                        UCCustomerToolbar.Deleted -= new EventHandler(OnContactDeleted);

                        Workitem.Items.Remove(UCCustomerToolbar);
                        _ucCustomerToolbar = null;
                    }
                    Workitem.Items.Remove(this);
                    Workitem = null;

                }



            };

            if (!LocalData.IsDesignMode)
            {

                Load += (sender, e) =>
                {
                    pnlCustomerToolBar.Controls.Clear();
                    UCCustomerToolbar.Dock = DockStyle.Fill;
                    pnlCustomerToolBar.Controls.Add(UCCustomerToolbar);

                    UCBusinessDocumentList.Dock = DockStyle.Fill;
                    grpAttachment.Controls.Add(UCBusinessDocumentList);

                    pnlCustomerList.Controls.Clear();
                    UCCustomerList.IsCustomerPart = true;
                    UCCustomerList.Dock = DockStyle.Fill;
                    pnlCustomerList.Controls.Add(UCCustomerList);
                    UCCustomerList.CustomerErrorEventHandler += TriCustomerFailedEvent;
                    UCBusinessDocumentList.UploadFailedEventHandler += TriUploadFailedEvent;

                    UCCustomerToolbar.Added += new EventHandler(OnContactAdded);
                    UCCustomerToolbar.Deleted += new EventHandler(OnContactDeleted);
                    UCCustomerToolbar.SetStage += new EventHandler(OnStage);
                };

            }


        }

        void OnContactDeleted(object sender, EventArgs e)
        {
            UCCustomerList.Remove();
        }


        void OnStage(object sender, EventArgs e) 
        {
            if (sender == null)
            {
                return;
            }

            BarEditItem be = sender as BarEditItem;

            foreach (var item in ucCustomerList.DataSourceList)
            {
                item.Stage = be.EditValue.ToString();
            }

            ucCustomerList.ResetBindings();
        }

        void OnContactAdded(object sender, EventArgs e)
        {
            if (UCCustomerList != null)
            {
                if (UsingOperationContext != null)
                    UCCustomerList.AddNewDataRecord(UsingOperationContext.OperationID, string.Empty, string.Empty);
                else
                    UCCustomerList.AddNewDataRecord(string.Empty, string.Empty);
            }
        }

        /// <summary>
        /// 保存控件数据
        /// </summary>
        /// <returns></returns>
        public void Save(DateTime? updateDate)
        {


            try
            {
                if (UCCustomerList.IsChanged)
                {
                    UCCustomerList.IsChanged = false;
                    UCCustomerList.Save();
                }
            }
            catch (Exception ex)
            {
                UCCustomerList.IsChanged = true;
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }

            WaitCallback callback = (obj) =>
            {
                try
                {
                    if (UCBusinessDocumentList.IsChanged)
                    {
                        UCBusinessDocumentList.IsChanged = false;
                        UCBusinessDocumentList.Save();
                    }
                }
                catch (Exception ex)
                {
                    UCBusinessDocumentList.IsChanged = true;
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
        }

        public bool ValidateData()
        {
            return UCCustomerList.ValidateData();
        }


        /// <summary>
        /// 触发客户列表保存失败事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriCustomerFailedEvent(object sender, EventArgs e)
        {
            if (CustomerFailedEventHandler != null)
            {
                CustomerFailedEventHandler(this, new CommonEventArgs<string>(e.ToString()));

            }
        }

        /// <summary>
        /// 向客户列表插入数据
        /// </summary>
        /// <param name="customerCarrier"></param>
        public void InsertCustomerInfo(CustomerCarrierObjects customerCarrier)
        {
            //新建订舱单时，发件人都是客户
            if (customerCarrier.Type == ContactType.Unknown)
                customerCarrier.Type = ContactType.Customer;
            UCCustomerList.Insert(customerCarrier);
        }
        /// <summary>
        /// 批量插入联系人信息
        /// </summary>
        /// <param name="contactList"></param>
        public void InsertContactList(List<CustomerCarrierObjects> contactList)
        {
            if (contactList == null || contactList.Count <= 0)
                return;
            //新建订舱单时，发件人都是客户
            foreach (CustomerCarrierObjects obj in contactList)
            {
                if (obj.Type == ContactType.Unknown)
                    obj.Type = ContactType.Customer;
            }
            UCCustomerList.InsertList(contactList);
        }
        public void RemoveContactList(Guid customerID, ContactType? contactType)
        {
            UCCustomerList.RemoveContactList(customerID, contactType);
        }

        /// <summary>
        /// 向业务文档添加附件
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="documentType"></param>
        public void AddDocuments(List<string> filePaths, List<string> previewPaths, EnumDocumentType? documentType)
        {
            Add(filePaths, previewPaths, documentType);
        }

        public void AddDocuments(List<string> filePaths, EnumDocumentType? documentType)
        {
            Add(filePaths, null, documentType);
        }
        public void Add(List<string> filePaths, List<string> previewPaths, EnumDocumentType? documentType)
        {
            UCBusinessDocumentList.AddDocuments(filePaths, previewPaths, null, documentType);
        }
        /// <summary>
        /// 获取联系人列表邮件地址集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetCustomerEmail()
        {
            List<string> lstcustomer = new List<string>();
            List<CustomerCarrierObjects> customerlist = UCCustomerList.DataSource as List<CustomerCarrierObjects>;
            foreach (var item in customerlist)
            {
                lstcustomer.Add(item.Mail);
            }

            return lstcustomer;
        }

        /// <summary>
        /// 触发文档上传失败事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriUploadFailedEvent(object sender, EventArgs e)
        {

            if (UploadFailedEventHandler != null)
            {
                UploadFailedEventHandler(this, new CommonEventArgs<string>(e.ToString()));
            }
        }

        private delegate void BindDataDelegate(List<CustomerCarrierObjects> customerList);

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mail"></param>
        public void BindData(BusinessOperationContext context)
        {
            this.context = context;
            if (context == null || context.OperationID == Guid.Empty)
            {
                UCCustomerList.DataSource = new List<CustomerCarrierObjects>();
            }
            else
            {
                WaitCallback callback = (temp) =>
                {
                    try
                    {
                        ClientHelper.SetApplicationContext();
                        contactSoure = FCMCommonService.GetContactList(context.OperationID, context.OperationType);
                        if (ucCustomerList!=null)
                            ucCustomerList.OperationContext = UsingOperationContext = context;
                        if (IsDisposed)
                            return;
                        BindDataDelegate bindDelegate = new BindDataDelegate(InnerBindData);
                        Invoke(bindDelegate, contactSoure.CustomerCarrier);
                    }
                    catch (Exception ex)
                    {
                        if (!IsDisposed && !Parent.IsDisposed)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                        }
                    }
                };
                ThreadPool.QueueUserWorkItem(callback);

            }

        }


        public void InnerBindData(List<CustomerCarrierObjects> customerList)
        {
            //this.ucCustomerList.OperationContext = this.context;
            UCCustomerList.DataSource = customerList;
            UCBusinessDocumentList.Context = context;
            UCBusinessDocumentList.BindData(context);
            UCCustomerToolbar.SetStageComboBoxItems(context.OperationType);
        }


    }
}
